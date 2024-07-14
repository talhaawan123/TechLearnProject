//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Myshowroom.Business_logic.Contract;
//using Myshowroom.DataContext;
//using Myshowroom.Models;

//namespace Myshowroom.Business_logic.Concrete
//{
//    public class TestQuestionsBusinessLogic : ITestQuestionsBusinessLogic
//    {
//        private readonly dataContext dbcontext;

//        public TestQuestionsBusinessLogic(dataContext dbcontext)
//        {
//            this.dbcontext = dbcontext;
//        }

//        public async Task<List<Question>> GetAllQuestionsAsync()
//        {
//            return await dbcontext.Questions.ToListAsync();
//        }

//        public async Task<Question> GetQuestionByIdAsync(int questionId)
//        {
//            return await dbcontext.Questions.FindAsync(questionId);
//        }

//        public async Task AddQuestionAsync(Question question)
//        {
//            dbcontext.Questions.Add(question);
//            await dbcontext.SaveChangesAsync();
//        }

//        public async Task UpdateQuestionAsync(Question question)
//        {
//            dbcontext.Entry(question).State = EntityState.Modified;
//            await dbcontext.SaveChangesAsync();
//        }

//        public async Task DeleteQuestionAsync(int questionId)
//        {
//            var question = await dbcontext.Questions.FindAsync(questionId);
//            if (question != null)
//            {
//                dbcontext.Questions.Remove(question);
//                await dbcontext.SaveChangesAsync();
//            }
//        }

//        public async Task<List<Answer>> GetAnswersForQuestionAsync(int questionId)
//        {
//            return await dbcontext.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
//        }

//        public async Task<Answer> GetAnswerByIdAsync(int answerId)
//        {
//            return await dbcontext.Answers.FindAsync(answerId);
//        }

//        public async Task AddAnswerToQuestionAsync(int questionId, Answer answer)
//        {
//            var question = await dbcontext.Questions.FindAsync(questionId);
//            if (question != null)
//            {
//                question.Answers.Add(answer);
//                await dbcontext.SaveChangesAsync();
//            }
//        }

//        public async Task UpdateAnswerAsync(Answer answer)
//        {
//            dbcontext.Entry(answer).State = EntityState.Modified;
//            await dbcontext.SaveChangesAsync();
//        }

//        public async Task DeleteAnswerAsync(int answerId)
//        {
//            var answer = await dbcontext.Answers.FindAsync(answerId);
//            if (answer != null)
//            {
//                dbcontext.Answers.Remove(answer);
//                await dbcontext.SaveChangesAsync();
//            }
//        }
//    }
//}
