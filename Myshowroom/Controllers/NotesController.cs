using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myshowroom.Business_logic.Contract;
using Myshowroom.Models;
using Myshowroom.Unit_of_work;

namespace Myshowroom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IunitOfWork unitOfWork;
        private readonly INotesBusinessLogic notesRepository;

        public NotesController(IunitOfWork unitOfWork, INotesBusinessLogic notesRepository)
        {
            this.unitOfWork = unitOfWork;
            this.notesRepository = notesRepository;
        }

        [HttpGet("GetLearningNotes")]
        public async Task<IActionResult> GetallCars()
        {
            var result = await notesRepository.GetAllAsync();
            unitOfWork.CommitAsync();
            return Ok(result);
        }

        [HttpPost("AddLearningNotes")]
        public async Task<IActionResult> AddLearningNotes([FromBody] Notes note)
        {
            try
            {
                await notesRepository.CreateAsync(note);
                await unitOfWork.CommitAsync();

                return Ok(new { message = "Learning note added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the learning note" });
            }
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

        [HttpDelete("DeleteLearningNote/{id}")]
        public async Task<IActionResult> DeleteLearningNote(int id)
        {
            var result = await notesRepository.GetByIdAsync(id);
            if (result != null)
            {
                var note = await notesRepository.DeleteAsync(result);
            }
            await unitOfWork.CommitAsync();
            return Ok("Deleted successfully");
        }


    }
}
