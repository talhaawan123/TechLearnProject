using Microsoft.EntityFrameworkCore;
using Myshowroom.DataContext;
using TechLearn.Business_logic.Contract;
using TechLearn.Models.Domain_Models;

namespace TechLearn.Business_logic.Concrete
{
    public class DropDownsBusinesslogic : IDropDownsBusinesslogic
    {
        private readonly dataContext _dataContext;
        public DropDownsBusinesslogic(dataContext _dataContext)
        {
            this._dataContext = _dataContext;
        }
        public async Task<IEnumerable<ProgrammingLanguages>> Get_ProgrammingLanguages_Dropdown()
        {
            var dropDownValues= await _dataContext.ProgrammingLanguages.ToListAsync();
            return dropDownValues;
        }
    }
}
