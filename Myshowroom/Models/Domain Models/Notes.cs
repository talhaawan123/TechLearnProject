using TechLearn.Models.Domain_Models;

namespace Myshowroom.Models
{
    public class Notes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public ProgrammingLanguages ProgrammingLanguage { get; set; }

    }

    public class CodeRequest
    {
        public string Code { get; set; }
        public string Language { get; set; }
    }
}
