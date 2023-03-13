using AutoMapper;
using NotificationAPI.Data;
using NotificationAPI.Data.Dtos;
using NotificationAPI.Models;
using NotificationAPI.RabbitMqClient.Client;
using System.Globalization;

namespace NotificationAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationContext _context;
        private readonly IMapper _mapper;
        private readonly IRabbitMqClient _rabbitMqClient;

        public NotificationService(NotificationContext context, IMapper mapper, IRabbitMqClient rabbitMqClient)
        {
            _context = context;
            _mapper = mapper;
            _rabbitMqClient = rabbitMqClient;
        }

        public void CreateNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
        }

        public void CreateNotificationForRMq(Notification notification)
        {
            var notificationDto = _mapper.Map<ReadNotificationDto>(notification);
            _rabbitMqClient.PublishNotification(notificationDto);
        }

        public void DeleteNotification(Notification notification)
        {
            var excluido = GetNotificationByID(notification.ID);
            excluido.Excluido = true;
            excluido.DataExclusao = DateTime.UtcNow.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
        }

        public void DeleteNotificationForRMq(int id)
        {
            var notificationExcluida = GetNotificationByID(id);
            var notificationDto = _mapper.Map<ReadNotificationDto>(notificationExcluida);
            _rabbitMqClient.DeleteNotification(notificationDto);
        }

        public bool Existe(Notification notification)
        {
            return _context.Notifications.Any(n => n.ID == notification.ID);
        }

        public Notification GetNotificationByID(int id)
        {
            return _context.Notifications.FirstOrDefault(n => n.ID == id);
        }

        public IEnumerable<ReadNotificationDto> GetNotifications(int skip = 0, int take = 50)
        {
            return _mapper.Map<List<ReadNotificationDto>>(_context.Notifications.Skip(skip).Take(take).Where(n => !n.Excluido).ToList());
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
