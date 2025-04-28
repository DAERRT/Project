using kek.Controllers;
using kek.Entities;

namespace kek.Models
{
    public class PersonalAccountViewModel : User
    {
        public IEnumerable<Teams> Teams { get; set; }

        public IEnumerable<Idea> Ideas { get; set; }
    }
}
