using NotificationAPI.Data.Dtos;

namespace NotificationAPI.RabbitMqClient
{
    public interface IRabbitMqClient
    {
        void PublishNotification(ReadNotificationDto notificationDto);
    }
}
