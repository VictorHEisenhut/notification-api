using NotificationAPI.Data.Dtos;
using NotificationAPI.Models;

namespace NotificationAPI.Services
{
    public interface INotificationService
    {
        void SaveChanges();

        IEnumerable<ReadNotificationDto> GetNotifications(int pagina = 0, int qtdPorPagina = 50);
        bool Existe(Notification notification);
        Notification GetNotificationByID(int id);
        void CreateNotificationForRMq(Notification notification);
        void CreateNotification(Notification notification);
        void DeleteNotificationForRMq(int id);
        void DeleteNotification(Notification notification);
    }
}
