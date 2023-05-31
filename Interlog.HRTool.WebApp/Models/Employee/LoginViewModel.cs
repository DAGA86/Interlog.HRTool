using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Interlog.HRTool.WebApp.Models.Employee
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter Username")]
        [MaxLength(320)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Insira uma senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
