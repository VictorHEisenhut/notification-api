using AutoMapper;
using NotificationAPI.Data.Dtos;
using NotificationAPI.Models;
using NotificationAPI.Services;
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

        public void Processa(string msg)
        {
            using var scope = _scopeFactory.CreateScope();

           var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

            var notificationDto = JsonSerializer.Deserialize<ReadNotificationDto>(msg);

            var notification = _mapper.Map<Notification>(notificationDto);

            if (notification != null)
            {
                notificationService.CreateNotification(notification);
                notificationService.SaveChanges();
            }


        }
    }
}
