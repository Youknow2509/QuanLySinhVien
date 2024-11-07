
using System.ComponentModel.DataAnnotations;

namespace QLDT_WPF.Dto
{
    public class UpdatePasswordDto
    {
        [Required]
        public string IdUser { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }
}