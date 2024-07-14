using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Myshowroom.Business_logic.Contract;
using Myshowroom.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Myshowroom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestQuestionsController : ControllerBase
    {
        private readonly ITestQuestionsBusinessLogic _testQuestionsRepository;

        public TestQuestionsController(ITestQuestionsBusinessLogic testQuestionsRepository)
        {
            _testQuestionsRepository = testQuestionsRepository;
        }

    }
}
