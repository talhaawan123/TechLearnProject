using Myshowroom.Business_logic.Concrete;
using Myshowroom.Business_logic.Contract;
using Myshowroom.DataContext;

namespace Myshowroom.Unit_of_work
{
    public class UnitOfWork : IunitOfWork
    {
        private readonly dataContext dataContext;
        private readonly INotesBusinessLogic notesRepository;
        private readonly ITestQuestionsBusinessLogic testQuestionsRepository;

        public UnitOfWork(dataContext datacontext)
        {
            this.dataContext = datacontext;
            notesRepository = new NotesBusinessLogic(dataContext);
            testQuestionsRepository = new TestQuestionsBusinessLogic(dataContext);
        }


        public INotesBusinessLogic NotesRepository => notesRepository;
        public ITestQuestionsBusinessLogic TestQuestionsRepository => testQuestionsRepository;
        public async Task CommitAsync()
        {
            await dataContext.SaveChangesAsync();
        }

    }
}
