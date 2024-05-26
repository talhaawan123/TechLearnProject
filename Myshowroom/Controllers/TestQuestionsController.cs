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

        [HttpGet("questions")]
        public async Task<ActionResult<IEnumerable<Question>>> GetAllQuestions()
        {
            var questions = await _testQuestionsRepository.GetAllQuestionsAsync();
            return Ok(questions);
        }

        [HttpGet("questions/{questionId}")]
        public async Task<ActionResult<Question>> GetQuestionById(int questionId)
        {
            var question = await _testQuestionsRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpPost("questions")]
        public async Task<ActionResult> AddQuestion(Question question)
        {
            await _testQuestionsRepository.AddQuestionAsync(question);
            return Ok();
        }

        [HttpPut("questions/{questionId}")]
        public async Task<ActionResult> UpdateQuestion(int questionId, Question question)
        {
            if (questionId != question.Id)
            {
                return BadRequest();
            }
            await _testQuestionsRepository.UpdateQuestionAsync(question);
            return Ok();
        }

        [HttpDelete("questions/{questionId}")]
        public async Task<ActionResult> DeleteQuestion(int questionId)
        {
            await _testQuestionsRepository.DeleteQuestionAsync(questionId);
            return Ok();
        }

        [HttpGet("questions/{questionId}/answers")]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswersForQuestion(int questionId)
        {
            var answers = await _testQuestionsRepository.GetAnswersForQuestionAsync(questionId);
            return Ok(answers);
        }

        [HttpGet("answers/{answerId}")]
        public async Task<ActionResult<Answer>> GetAnswerById(int answerId)
        {
            var answer = await _testQuestionsRepository.GetAnswerByIdAsync(answerId);
            if (answer == null)
            {
                return NotFound();
            }
            return Ok(answer);
        }

        [HttpPost("questions/{questionId}/answers")]
        public async Task<ActionResult> AddAnswerToQuestion(int questionId, Answer answer)
        {
            await _testQuestionsRepository.AddAnswerToQuestionAsync(questionId, answer);
            return Ok();
        }

        [HttpPut("answers/{answerId}")]
        public async Task<ActionResult> UpdateAnswer(int answerId, Answer answer)
        {
            if (answerId != answer.Id)
            {
                return BadRequest();
            }
            await _testQuestionsRepository.UpdateAnswerAsync(answer);
            return Ok();
        }

        [HttpDelete("answers/{answerId}")]
        public async Task<ActionResult> DeleteAnswer(int answerId)
        {
            await _testQuestionsRepository.DeleteAnswerAsync(answerId);
            return Ok();
        }
    }
}
