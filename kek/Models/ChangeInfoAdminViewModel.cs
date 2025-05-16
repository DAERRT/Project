using System.ComponentModel.DataAnnotations;

namespace kek.Models
{
    public class ChangeInfoAdminViewModel
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

        [Display(Name = "Роль")]
        public string SelectedRoleId { get; set; }

        public List<RoleViewModel> AvailableRoles { get; set; } = new List<RoleViewModel>();
    }

    public class RoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
