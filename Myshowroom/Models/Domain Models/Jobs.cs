using TechLearn.Enums;

namespace TechLearn.Models.Domain_Models
{
    public class Jobs
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public JobType JobType { get; set; }

    }
}
