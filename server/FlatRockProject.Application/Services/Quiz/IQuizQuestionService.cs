using FlatRockProject.Application.Models;
using FlatRockProject.Infrastructure.Entities;
using Optional;

namespace FlatRockProject.Application.Services.Quiz
{
    public interface IQuizQuestionService
    {
        Task<BinaryChoiceQuestionModel> GetLastBinaryChoiceQuestionAsync();
        Task<MultipleChoiceQuestion> GetLastMultipleChoiceQuestionAsync();
        Task<BinaryChoiceQuestionModel> GetBinaryChoiceQuestionAsync(int initialId = 0);
        Task<MultipleChoiceQuizQuestionModel> GetMultipleChoiceQuizQuestionAsync(int initialId = 0);
        Task<List<BinaryChoiceQuestionModel>> GetBinaryChoiceQuestionsAsync(int initialId = 0);
        Task<List<MultipleChoiceQuizQuestionModel>> GetMultipleChoiceQuizQuestionsAsync(int initialId = 0);
        Task<int> CreateQuizAsync(Quote request);
        Task <int> AddAuthor(string name);
        Task<int> AddQuote(string content, string authorName);
        Task<int> AddBinaryQuestion(
            string quoteContent,
            string questionableAuthorName,
            string correctAuthorName);
        Task<int> AddMultipleChoiceQuestion(
           string quoteContent,
           string correctAuthorName);
    }
}
