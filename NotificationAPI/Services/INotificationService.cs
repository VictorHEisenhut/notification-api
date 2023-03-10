using NotificationAPI.Data.Dtos;
using NotificationAPI.Models;

namespace NotificationAPI.Services
{
    public interface INotificationService
    {
        void SaveChanges();

        IEnumerable<ReadNotificationDto> GetNotifications(int skip = 0, int take = 50);
        bool Existe(Notification notification);
        Notification GetNotificationByID(int id);
        void CreateNotificationForRMq(Notification notification);
        void CreateNotification(Notification notification);
        void DeleteNotificationForRMq(int id);
        void DeleteNotification(Notification notification);
    }
}
