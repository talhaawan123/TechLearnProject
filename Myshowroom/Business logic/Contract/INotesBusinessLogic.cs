using Myshowroom.Models;

namespace Myshowroom.Business_logic.Contract
{
    public interface INotesBusinessLogic
    {
        Task <Notes> CreateAsync (Notes notes);
        Task<Notes> UpdateAsync (Notes notes);
        Task<bool> DeleteAsync (Notes notes);
        Task<Notes> GetByIdAsync (int id);
        Task <IEnumerable<Notes>> GetAllAsync ();
        Task<int> GetNotesCountAsync();
    }
}
