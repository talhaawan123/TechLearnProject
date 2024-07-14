using Myshowroom.Models;
using TechLearn.Models.DTO_s;

namespace Myshowroom.Business_logic.Contract
{
    public interface INotesBusinessLogic
    {
        Task <Notes> CreateAsync (NotesCreateModel notesCreateModel);
        Task<Notes> UpdateAsync (Notes notes);
        Task<bool> DeleteAsync (Notes notes);
        Task<NotesReadModel> GetByIdAsync (int id);
        Task<IEnumerable<NotesReadModel>> GetAllNotes(int? programmingLanguageId = null);
        IQueryable<Notes> GetAllNotesAnalytics(int? programmingLanguageId = null);
        Task<int> GetNotesCountAsync();
        Task<Dictionary<int, int>> GetNotesCountByProgrammingLanguageAsync();
    }
}
