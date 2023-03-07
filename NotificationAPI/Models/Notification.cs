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
        public DateTime DataProcessamento { get; set; } = DateTime.Now;
        [Required]
        public DateTime DataExclusao { get; set; }
        public bool Excluido { get; set; } = false;
    }

}
