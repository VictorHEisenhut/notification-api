using System.ComponentModel.DataAnnotations;

namespace NotificationAPI.Models
{
    public class Notification
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataProcessamento { get; set; }
        [Required]
        public DateTime DataExclusao { get; set; }
    }
}
