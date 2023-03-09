using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NotificationAPI.Data.Dtos
{
    public class ReadNotificationDto
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        
        public string DataProcessamento { get; set; } = DateTime.UtcNow.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));

        public string? DataExclusao { get; set; }
    }
}
