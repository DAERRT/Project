using System.ComponentModel.DataAnnotations;

namespace kek.Entities
{
    public class Idea
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

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

        [Required]
        public string? Ininiator { get; set; }

        [Required]
        public int Status { get; set; } 

        [Required]
        public string TeamId { get; set; } = null!;
    }
}
