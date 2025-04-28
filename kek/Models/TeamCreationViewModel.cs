using kek.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace kek.Models
{
    public class TeamCreationViewModel
    {

        [Required(ErrorMessage = "Введите название команды.")]
        [MaxLength(255)]
        public string? TeamName { get; set; }

        [Required(ErrorMessage = "Введите описание команды.")]
        [MaxLength(2000)]
        public string? TeamDescription { get; set; }

        public List<string?>? TeamMembers { get; set; }

        public IEnumerable<User>? Users { get; set; }
    }
}
