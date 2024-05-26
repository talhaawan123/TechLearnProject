using System.Collections.Generic;
using System.Threading.Tasks;
using Myshowroom.Models;

namespace Myshowroom.Business_logic.Contract
{

        public interface ITestQuestionsBusinessLogic
        {
        Task<List<Question>> GetAllQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int questionId);
        Task AddQuestionAsync(Question question);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int questionId);

        Task<List<Answer>> GetAnswersForQuestionAsync(int questionId);
        Task<Answer> GetAnswerByIdAsync(int answerId);
        Task AddAnswerToQuestionAsync(int questionId, Answer answer);
        Task UpdateAnswerAsync(Answer answer);
        Task DeleteAnswerAsync(int answerId);
    }

    
}
