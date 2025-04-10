using System.ComponentModel.DataAnnotations;

namespace kek.Models
{
    public class RegViewModel
    {
        [Required(ErrorMessage = "Введите имя.")]
        [MaxLength(255)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию.")]
        [MaxLength(255)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Введите Email.")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 40 символов.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Пароли не равны")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Введите подтверждение пароля.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
