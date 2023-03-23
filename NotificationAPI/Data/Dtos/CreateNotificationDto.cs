using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NotificationAPI.Data.Dtos
{
    public class CreateNotificationDto
    {
        [Required(ErrorMessage = "A notificação deve possuir um título")]
        [StringLength(70, ErrorMessage ="Título deve conter de 3 a 70 caracteres"), MinLength(3)]
        [DisplayName("Título")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "A notificação deve possuir uma descrição")]
        [DisplayName("Descrição")]
        [StringLength(200, ErrorMessage ="Descrição deve conter de 3 a 200 caracteres"), MinLength(3)]
        public string Descricao { get; set; }
    }
}
