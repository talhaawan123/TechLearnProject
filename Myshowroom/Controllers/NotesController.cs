using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myshowroom.Business_logic.Contract;
using Myshowroom.Models;
using Myshowroom.Unit_of_work;
using TechLearn.Business_logic.Contract;
using TechLearn.Models.DTO_s;

namespace Myshowroom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IunitOfWork unitOfWork;
        private readonly INotesBusinessLogic notesRepository;
        private readonly IDropDownsBusinesslogic dropDownsBusinesslogic;

        public NotesController(IunitOfWork unitOfWork, INotesBusinessLogic notesRepository, IDropDownsBusinesslogic dropDownsBusinesslogic)
        {
            this.unitOfWork = unitOfWork;
            this.notesRepository = notesRepository;
            this.dropDownsBusinesslogic = dropDownsBusinesslogic;
        }

        [HttpGet("GetLearningNotes")]
        public async Task<IActionResult> GetallLearningNotes(int? programmingLanguageId = null)
        {
            var result = await notesRepository.GetAllNotes(programmingLanguageId);
            unitOfWork.CommitAsync();
            return Ok(result);
        }

        [HttpPost("AddLearningNotes")]
        public async Task<IActionResult> AddLearningNotes([FromBody] NotesCreateModel note)
        {
            
                await notesRepository.CreateAsync(note);
                await unitOfWork.CommitAsync();

                return Ok(new { message = "Learning note added successfully" });
            
        }


        [HttpGet("GetTotalPublishedNotesCount")]
        public async Task<IActionResult> GetTotalPublishedNotesCount()
        {

            var count = await notesRepository.GetNotesCountAsync();
            return Ok(new { count });


        }

        [HttpPut("UpdateLearningNote/{id}")]
        public async Task<IActionResult> UpdateLearningNote(int id, [FromBody] Notes updatedNote)
        {
            if (id != updatedNote.Id)
            {
                return BadRequest();
            }

            var result = await notesRepository.UpdateAsync(updatedNote);
            if (result != null)
            {
                await unitOfWork.CommitAsync();
                return Ok("Learning Note updated successfully");
            }
            else
            {
                return NotFound("Learning Note not found");
            }
        }
        [HttpGet("GetLearningNote/{id}")]
        public async Task<IActionResult> GetLearningNoteById(int id)
        {
            var note = await notesRepository.GetByIdAsync(id);
            if (note != null)
            {
                return Ok(note);
            }
            else
            {
                return NotFound("Note not found");
            }
        }

        //[HttpDelete("DeleteLearningNote/{id}")]
        //public async Task<IActionResult> DeleteLearningNote(int id)
        //{
        //    var result = await notesRepository.GetByIdAsync(id);
        //    if (result != null)
        //    {
        //        var note = await notesRepository.DeleteAsync(result);
        //    }
        //    await unitOfWork.CommitAsync();
        //    return Ok("Deleted successfully");
        //}

        [HttpGet ("ProgrammingLanguageDropDown")]
         public async Task<IActionResult> ProgrammingLanguageDropdown()
         {
           var dropdownOptions= await unitOfWork.LanguagesDropdown.Get_ProgrammingLanguages_Dropdown();
            return Ok(dropdownOptions);
         }
    }
}
