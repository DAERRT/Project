using System.ComponentModel.DataAnnotations;

namespace kek.Models
{
    public class EditIdeaViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string? IdeaName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? Problem { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? Solution { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? ExpectedResult { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? NecessaryResourses { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? Stack { get; set; }

        [Required]
        public string? Customer { get; set; }
    }
}
