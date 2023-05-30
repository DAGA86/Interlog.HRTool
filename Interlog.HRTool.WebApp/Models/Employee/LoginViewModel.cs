using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Interlog.HRTool.WebApp.Models.Employee
{
    public class LoginViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Username")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [MaxLength(320)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Insira uma senha")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [MaxLength(32)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$", ErrorMessage = "A sua password precisa de conter pelo menos 8 caracteres, uma letra maiúscula, uma minuscula, um número e um caractere.")]
        public string Password { get; set; }
    }
}
