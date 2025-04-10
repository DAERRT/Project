using System.ComponentModel.DataAnnotations;

namespace kek.Models
{
    public class AddingIdeaViewModel
    {
        [Required(ErrorMessage ="Необходимо добавить заголовок проекта.")]
        [MaxLength(200)]
        public string? IdeaName { get; set; }

        [Required(ErrorMessage = "Необходимо добавить проблему проекта.")]
        [MaxLength(1000)]
        public string? Problem { get; set; }

        [Required(ErrorMessage = "Необходимо добавить предпологаемое решение проблемы.")]
        [MaxLength(1000)]
        public string? Solution { get; set; }

        [Required(ErrorMessage = "Необходимо добавить ожидаемый результат проекта.")]
        [MaxLength(1000)]
        public string? ExpectedResult { get; set; }

        [Required(ErrorMessage = "Необходимо добавить необходимые ресурсы для проекта.")]
        [MaxLength(1000)]
        public string? NecessaryResourses { get; set; }

        [Required(ErrorMessage = "Необходимо добавить предлогаемый стек проекта.")]
        [MaxLength(1000)]
        public string? Stack { get; set; }

        [Required(ErrorMessage = "Необходимо добавить заказчика проекта.")]
        public string? Customer { get; set; }
    }
}
