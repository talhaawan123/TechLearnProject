using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myshowroom.DataContext;
using TechLearn.Models.Domain_Models;
using TechLearn.Models.DTO_s;

namespace TechLearn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly dataContext _dataContext;

        public JobsController(dataContext _dataContext) { 
          this._dataContext = _dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var jobs = await _dataContext.Jobs.ToListAsync();
            return Ok(jobs);
        }

        [HttpPost]

        public async Task<IActionResult> PostJob([FromBody] JobCreateModel model )
        {
            var job = new Jobs
            {
                Title = model.Title,
                Description = model.Description,
                JobType = model.JobType,
                Company = model.Company,
            };

            await _dataContext.Jobs.AddAsync(job);
            _dataContext.SaveChanges();
            return Ok(job);
        }
    }
}
