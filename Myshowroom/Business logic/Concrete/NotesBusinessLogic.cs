using Microsoft.EntityFrameworkCore;
using Myshowroom.Business_logic.Contract;
using Myshowroom.DataContext;
using Myshowroom.Models;
using TechLearn.Models.DTO_s;

namespace Myshowroom.Business_logic.Concrete
{
    public class NotesBusinessLogic : INotesBusinessLogic
    {
        private readonly dataContext dataContext;

        public NotesBusinessLogic(dataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<Notes> CreateAsync(NotesCreateModel notesCreateModel)
        {
            var notes = new Notes
            {
                Title = notesCreateModel.Title,
                Subject = notesCreateModel.Subject,
                Body = notesCreateModel.Body,
                ProgrammingLanguageId = notesCreateModel.ProgrammingLanguageId,
                CreatedAt = DateTime.UtcNow
            };
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

        public IQueryable<Notes> GetAllNotesAnalytics(int? programmingLanguageId = null)
        {
            IQueryable<Notes> query = dataContext.LearningNotes;

            query = FilterNotes(programmingLanguageId, query);

            return query;
        }

        public async Task<IEnumerable<NotesReadModel>> GetAllNotes(int? programmingLanguageId = null)
        {
            IQueryable<Notes> query = dataContext.LearningNotes;

            query = FilterNotes(programmingLanguageId, query);

            var notes = await query.ToListAsync();

            var notesReadModels = notes.Select(note => new NotesReadModel
            {
                Title = note.Title,
                Subject = note.Subject,
                Body = note.Body,
                CreatedAt = DateTime.UtcNow,
                ProgrammingLanguageId = note.ProgrammingLanguageId
            }).ToList();

            return notesReadModels;
        }

        private static IQueryable<Notes> FilterNotes(int? programmingLanguageId, IQueryable<Notes> query)
        {
            if (programmingLanguageId != null)
            {
                query = query.Where(n => n.ProgrammingLanguageId == programmingLanguageId);
            }

            return query;
        }

        public async Task<int> GetNotesCountAsync()
        {
            int countOfNotes = await dataContext.LearningNotes.CountAsync();
            return countOfNotes;

        }
        public async Task<Dictionary<int, int>> GetNotesCountByProgrammingLanguageAsync()
        {
            var counts = await dataContext.LearningNotes
                .GroupBy(n => n.ProgrammingLanguageId)
                .Select(g => new { ProgrammingLanguageId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.ProgrammingLanguageId, x => x.Count);

            return counts;
        }
        public async Task<NotesReadModel> GetByIdAsync(int id)
        {
            var note = await dataContext.LearningNotes.FindAsync(id);
            if (note != null)
            {
                return new NotesReadModel
                {
                    Title = note.Title,
                    Subject = note.Subject,
                    Body = note.Body,
                    ProgrammingLanguageId = note.ProgrammingLanguageId
                };
            }
            else
            {
                return null;
            }
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
