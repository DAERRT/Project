﻿namespace kek.Entities
{
    public class Teams
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? TeamName { get; set; }

        public string? TeamDescription { get; set; }

        public List<string?>? TeamMembers { get; set; }

        public string? TeamLead {  get; set; }

        public string? TeamCreator { get; set; }

        public string IdeaId { get; set; } = null!;
    }
}
