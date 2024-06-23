using Myshowroom.Business_logic.Concrete;
using Myshowroom.Business_logic.Contract;
using Myshowroom.DataContext;
using TechLearn.Business_logic.Concrete;
using TechLearn.Business_logic.Contract;

namespace Myshowroom.Unit_of_work
{
    public class UnitOfWork : IunitOfWork
    {
        private readonly dataContext dataContext;
        private readonly INotesBusinessLogic notesRepository;
        private readonly ITestQuestionsBusinessLogic testQuestionsRepository;
        private readonly IDropDownsBusinesslogic dropDownsBusinessLogic;

        public UnitOfWork(dataContext datacontext)
        {
            this.dataContext = datacontext;
            notesRepository = new NotesBusinessLogic(dataContext);
            testQuestionsRepository = new TestQuestionsBusinessLogic(dataContext);
            dropDownsBusinessLogic = new DropDownsBusinesslogic(dataContext);
        }


        public INotesBusinessLogic NotesRepository => notesRepository;
        public IDropDownsBusinesslogic LanguagesDropdown => dropDownsBusinessLogic;
        public ITestQuestionsBusinessLogic TestQuestionsRepository => testQuestionsRepository;

        public async Task CommitAsync()
        {
            await dataContext.SaveChangesAsync();
        }

    }
}
