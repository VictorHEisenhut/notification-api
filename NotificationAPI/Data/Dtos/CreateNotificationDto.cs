using System.ComponentModel.DataAnnotations;

namespace NotificationAPI.Data.Dtos
{
    public class CreateNotificationDto
    {
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descricao { get; set; }
    }
}
