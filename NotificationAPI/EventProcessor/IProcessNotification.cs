using NotificationAPI.Models;

namespace NotificationAPI.EventProcessor
{
    public interface IProcessNotification
    {
        void Processa(string msg);
        void Deleta(string msg);

    }
}
