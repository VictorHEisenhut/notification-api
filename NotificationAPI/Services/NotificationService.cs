using AutoMapper;
using NotificationAPI.Data;
using NotificationAPI.Data.Dtos;
using NotificationAPI.Models;

namespace NotificationAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationContext _context;
        private readonly IMapper _mapper;

        public NotificationService(NotificationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void CreateNotification(Notification notification)
        {
            _context.Notifications.Add(notification);      
        }

        public void DeleteNotification(int id)
        {
            var notificationExcluida = GetNotificationByID(id);
            notificationExcluida.Excluido = true;
            notificationExcluida.DataExclusao = DateTime.Now;
            //_context.Notifications.Remove(_context.Notifications.FirstOrDefault(n => n.ID == id));
        }

        public Notification GetNotificationByID(int id)
        {
            return _context.Notifications.FirstOrDefault(n => n.ID == id);
        }

        public IEnumerable<ReadNotificationDto> GetNotifications(int skip = 0, int take = 50)
        {
            return _mapper.Map<List<ReadNotificationDto>>(_context.Notifications.Skip(skip).Take(take).Where(n => n.Excluido != true).ToList());
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
