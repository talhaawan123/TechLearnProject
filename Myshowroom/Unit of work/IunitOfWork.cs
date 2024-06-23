using Myshowroom.Business_logic.Contract;
using TechLearn.Business_logic.Contract;

namespace Myshowroom.Unit_of_work
{
    public interface IunitOfWork
    {
        INotesBusinessLogic NotesRepository { get; }
        ITestQuestionsBusinessLogic TestQuestionsRepository { get; }
        IDropDownsBusinesslogic LanguagesDropdown { get; }
        Task CommitAsync();

    }
}
