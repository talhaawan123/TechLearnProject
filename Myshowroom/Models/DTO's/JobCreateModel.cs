using TechLearn.Enums;

namespace TechLearn.Models.DTO_s
{
    public class JobCreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public JobType JobType { get; set; }
        public string Company { get; set; }
    }
}
