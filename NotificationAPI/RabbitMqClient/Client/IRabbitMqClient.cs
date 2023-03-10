using NotificationAPI.Data.Dtos;

namespace NotificationAPI.RabbitMqClient.Client
{
    public interface IRabbitMqClient
    {
        void PublishNotification(ReadNotificationDto notificationDto);
        void DeleteNotification(ReadNotificationDto notificationDto);
    }
}
