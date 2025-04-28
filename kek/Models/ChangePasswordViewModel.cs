using System.ComponentModel.DataAnnotations;

namespace kek.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Введите Email.")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Пароль должен содержать не менее {2} и не более {1} символов.", MinimumLength = 6)]
        public string? NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают.")]
        public string? ConfirmPassword { get; set; }

    }
}
