using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NotificationAPI.Models
{
    public class Notification
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage ="A notificação deve possuir um título")]
        [Range(3, 40)]
        [DisplayName("Título")]
        public string Titulo { get; set; }
        [Required(ErrorMessage ="A notificação deve possuir uma descrição")]
        [DisplayName("Descrição")]
        [Range(3, 300)]
        public string Descricao { get; set; }
        [Required(ErrorMessage ="A notificação deve possuir uma data de processamento")]
        [DisplayName("Data de processamento")]
        public string DataProcessamento { get; set; } = DateTime.UtcNow.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
        [DisplayName("Data de exclusão")]
        public string? DataExclusao { get; set; }
        public bool Excluido { get; set; }
    }

}
