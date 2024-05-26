using Microsoft.EntityFrameworkCore;
using Myshowroom.Business_logic.Contract;
using Myshowroom.DataContext;
using Myshowroom.Models;

namespace Myshowroom.Business_logic.Concrete
{
    public class NotesBusinessLogic : INotesBusinessLogic
    {
        private readonly dataContext dataContext;

        public NotesBusinessLogic(dataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<Notes> CreateAsync(Notes notes)
        {
            await dataContext.LearningNotes.AddAsync(notes);
            await dataContext.SaveChangesAsync();
            return notes;
        }

        public async Task<bool> DeleteAsync(Notes notes)
        {
            var result = await dataContext.LearningNotes.FirstOrDefaultAsync(c => c.Id == notes.Id);
            if (result != null)
            {
                dataContext.LearningNotes.Remove(result);
                await dataContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Notes>> GetAllAsync()
        {
            await dataContext.LearningNotes.ToListAsync();
            return dataContext.LearningNotes;

        }
        public async Task<int> GetNotesCountAsync()
        {
            int countOfNotes = await dataContext.LearningNotes.CountAsync();
            return countOfNotes;

        }

        public async Task<Notes> GetByIdAsync(int id)
        {
            return await dataContext.LearningNotes.FindAsync(id);
        }

        public async Task<Notes> UpdateAsync(Notes note)
        {
            var result = await dataContext.LearningNotes.FirstOrDefaultAsync(c => c.Id == note.Id);
            if (result != null)
            {
                result.Title = note.Title;
                result.Subject = note.Subject;
                result.Body = note.Body;
                return result;
            }
            return null;
        }
    }
}
