using kek.Entities;

namespace kek.Models
{
    public class TeamEditViewModel
    {
        public string Id { get; set; }

        public string? TeamName { get; set; }

        public string? TeamDescription { get; set; }

        public List<string?>? TeamMembers { get; set; }

        public string? TeamLead { get; set; }

        public string? TeamCreator { get; set; }

        public string IdeaId { get; set; } = null!;

        public IEnumerable<User> Users { get; set; }
    }
}
