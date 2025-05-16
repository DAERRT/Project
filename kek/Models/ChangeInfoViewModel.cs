using System.ComponentModel.DataAnnotations;

namespace kek.Models
{
    public class ChangeInfoViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Группа")]
        public string StudyGroup { get; set; }
    }
}
