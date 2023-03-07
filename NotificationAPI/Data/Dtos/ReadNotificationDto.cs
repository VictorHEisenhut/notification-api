using System.ComponentModel.DataAnnotations;

namespace NotificationAPI.Data.Dtos
{
    public class ReadNotificationDto
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataProcessamento { get; set; } = DateTime.Now;
        public DateTime DataExclusao { get; set; }
    }
}
