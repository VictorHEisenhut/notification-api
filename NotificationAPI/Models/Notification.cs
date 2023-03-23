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
        [StringLength(70, ErrorMessage = "Título deve conter de 3 a 70 caracteres"), MinLength(3)]
        [DisplayName("Título")]
        public string Titulo { get; set; }
        [Required(ErrorMessage ="A notificação deve possuir uma descrição")]
        [DisplayName("Descrição")]
        [StringLength(200, ErrorMessage = "Descrição deve conter de 3 a 200 caracteres"), MinLength(3)]
        public string Descricao { get; set; }
        [Required(ErrorMessage ="A notificação deve possuir uma data de processamento")]
        [DisplayName("Data de processamento")]
        public string DataProcessamento { get; set; } = DateTime.UtcNow.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
        [DisplayName("Data de exclusão")]
        public string? DataExclusao { get; set; }
        public bool Excluido { get; set; }
    }

}
