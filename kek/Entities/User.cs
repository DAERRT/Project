using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace kek.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }

        [Required] 
        public string? LastName { get; set; }

        [Required]
        public int? Status {  get; set; }

        [Required]
        public string? StudyGroup { get; set; }

        [Required]
        public DateTime? DateCreated { get; set; } = DateTime.UtcNow.Date;


        public ICollection<Teams> Teams { get; set; }
    }
}
