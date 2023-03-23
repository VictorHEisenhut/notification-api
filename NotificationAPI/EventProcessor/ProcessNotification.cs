using AutoMapper;
using NotificationAPI.Data;
using NotificationAPI.Data.Dtos;
using NotificationAPI.Models;
using NotificationAPI.Services;
using System.Globalization;
using System.Text.Json;

namespace NotificationAPI.EventProcessor
{
    public class ProcessNotification : IProcessNotification
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        public ProcessNotification(IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        public void Deleta(string msg)
        {
            using var scope = _scopeFactory.CreateScope();

            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

            var notification = Deserialize(msg);

            notificationService.DeleteNotification(notification);
            notificationService.SaveChanges();
        }
        

        public void Processa(string msg)
        {
            using var scope = _scopeFactory.CreateScope();

            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

            var notification = Deserialize(msg);

            if (!notificationService.Existe(notification))
            {
                notificationService.CreateNotification(notification);
                notificationService.SaveChanges();
            }

        }

        public Notification Deserialize(string msg)
        {
            var notificationDto = JsonSerializer.Deserialize<ReadNotificationDto>(msg);

            return _mapper.Map<Notification>(notificationDto);
        }
        
    }
}
