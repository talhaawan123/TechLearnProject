using TechLearn.Models.Domain_Models;

namespace TechLearn.Business_logic.Contract
{
    public interface IDropDownsBusinesslogic
    {
         Task <IEnumerable<ProgrammingLanguages>> Get_ProgrammingLanguages_Dropdown();
    }
}
