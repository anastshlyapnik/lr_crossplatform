using System.Text.Json.Serialization;

namespace Shlyapnikova_lr.Models
{
    public class Volunteer
    {
        public int VolunteerId { get; set; }
        public string VolunteerName { get; set; }
        public float VolunteerPhone { get; set; }
        public string VolunteerGroup { get; set; }
        public int VolunteerPriority { get; set; }

        public List<int> StudentIds { get; set; } = new List<int>();
    }
}
