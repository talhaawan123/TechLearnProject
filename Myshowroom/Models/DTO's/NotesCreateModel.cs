namespace TechLearn.Models.DTO_s
{
    public class NotesCreateModel
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
