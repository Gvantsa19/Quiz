using FlatRockProject.Application.Models;
using FlatRockProject.Application.Services.Abstract;
using FlatRockProject.Infrastructure.Entities;
using FlatRockProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Optional;
using Optional.Async;
using Optional.Async.Extensions;

namespace FlatRockProject.Application.Services.Quiz
{
    public class QuizQuestionService : BaseService, IQuizQuestionService
    {
        public QuizQuestionService(FRPDbContext context) : base(context)
        {

        }

        public async Task<int> CreateQuizAsync(Quote request)
        {
            var quote = new Quote
            {
                Content = request.Content,
                BinaryChoiceQuestions = request.BinaryChoiceQuestions,
                MultipleChoiceQuestions = request.MultipleChoiceQuestions,
                CreateDate = DateTime.Now,
            };

            DbContext.Quote.Add(quote);
            await DbContext.SaveChangesAsync();

            return quote.Id;
        }
        public async Task<int> AddAuthor(string name)
        {
            var author = new Infrastructure.Entities.Author
            {
                Name = name,
                CreateDate = DateTime.UtcNow
            };

            DbContext.Authors.Add(author);
            DbContext.SaveChanges();

            return author.Id;
        }
        public async Task<int> AddQuote(string content, string authorName)
        {
            var author = DbContext.Authors.FirstOrDefault(a => a.Name == authorName);

            if (author != null)
            {
                var quote = new Quote
                {
                    Content = content,
                    CreateDate = DateTime.UtcNow
                };

                DbContext.Quote.Add(quote);
                DbContext.SaveChanges();

                return quote.Id;
            }
            return 0;
        }
        public async Task<int> AddBinaryQuestion(
            string quoteContent,
            string questionableAuthorName,
            string correctAuthorName)
        {
            var quote = DbContext.Quote.FirstOrDefault(q => q.Content == quoteContent);
            var questionableAuthor = DbContext.Authors.FirstOrDefault(a => a.Name == questionableAuthorName);
            var correctAuthor = DbContext.Authors.FirstOrDefault(a => a.Name == correctAuthorName);

            if (quote != null && questionableAuthor != null && correctAuthor != null)
            {
                var binaryChoiceQuestion = new BinaryQuestion
                {
                    QuoteId = quote.Id,
                    QuestionableAuthorId = questionableAuthor.Id,
                    CorrectAuthorId = correctAuthor.Id
                };

                DbContext.BinaryQuestions.Add(binaryChoiceQuestion);
                DbContext.SaveChanges();

                return binaryChoiceQuestion.Id;
            }
            return 0;
        }

        public async Task<int> AddMultipleChoiceQuestion(
            string quoteContent,
            string correctAuthorName)
        {
            var quote = DbContext.Quote.FirstOrDefault(q => q.Content == quoteContent);
            var correctAuthor = DbContext.Authors.FirstOrDefault(a => a.Name == correctAuthorName);

            if (quote != null && correctAuthor != null)
            {
                var multipleChoiceQuestion = new MultipleChoiceQuestion
                {
                    QuoteId = quote.Id,
                    CorrectAuthorId = correctAuthor.Id
                };

                DbContext.MultipleChoiceQuestions.Add(multipleChoiceQuestion);
                DbContext.SaveChanges();
                return multipleChoiceQuestion.Id;
            }

            return 0;
        }

        public async Task<BinaryChoiceQuestionModel> GetLastBinaryChoiceQuestionAsync()
        {
            var lastQuestion = await DbContext
                .BinaryQuestions
                .OrderBy(question => question.Id)
                .Include(question => question.Quote)
                .LastOrDefaultAsync();

            var quote = DbContext.Quote.FindAsync(lastQuestion.QuoteId).Result;

            return new BinaryChoiceQuestionModel
            {
                Id = lastQuestion.Id,
                QuestionableAuthor = lastQuestion.QuestionableAuthor?.Name,
                QuestionableAuthorId = lastQuestion.QuestionableAuthorId,
                CorrectAuthor = lastQuestion.CorrectAuthor?.Name,
                CorrectAuthorId = lastQuestion.CorrectAuthorId,
                IsTrue = lastQuestion.IsTrue,
                Quote = quote.Content,
                QuoteId = quote.Id
            };
        }

        public async Task<MultipleChoiceQuestion> GetLastMultipleChoiceQuestionAsync()
        {
            var lastQuestion = await DbContext
                .MultipleChoiceQuestions
                .OrderByDescending(question => question.Id)
                .LastOrDefaultAsync();

            return lastQuestion;
        }

        public async Task<BinaryChoiceQuestionModel> GetBinaryChoiceQuestionAsync(int initialId = 0)
        {
            var lastQuestion = await GetLastBinaryChoiceQuestionAsync();
            initialId = 0;

            if (initialId >= lastQuestion.Id)
            {
                throw new Exception("Question limit has been reached!");
            }

            return (await DbContext
                    .BinaryQuestions
                    .AsNoTracking()
                    .Include(question => question.QuestionableAuthor)
                    .Include(question => question.Quote)
                    .Where(question => question.Id > initialId)
                    .Select(question => new BinaryChoiceQuestionModel
                    {
                        Id = question.Id,
                        QuoteId = question.QuoteId,
                        Quote = question.Quote.Content,
                        QuestionableAuthorId = question.QuestionableAuthorId,
                        QuestionableAuthor = question.QuestionableAuthor.Name,
                        CorrectAuthorId = question.CorrectAuthorId,
                        CorrectAuthor = question.CorrectAuthor.Name,
                        IsTrue = question.IsTrue
                    })
                    .FirstOrDefaultAsync())
                ;

        }

        public async Task<MultipleChoiceQuizQuestionModel> GetMultipleChoiceQuizQuestionAsync(int initialId = 0)
        {
            var lastQuestion = await GetLastMultipleChoiceQuestionAsync();
            if (initialId >= lastQuestion.Id)
            {
                throw new Exception("No multiple choice question found!");
            }

            var multipleChoiceQuestion = await DbContext
                .MultipleChoiceQuestions
                .AsNoTracking()
                .Include(question => question.Quote)
                .Include(question => question.CorrectAuthor)
                .Include(question => question.Answers)
                .ThenInclude(answer => answer.AuthorChoice)
                .Where(question => question.Id > initialId)
                .FirstOrDefaultAsync();

            if (multipleChoiceQuestion == null)
            {
                throw new Exception("No multiple choice question found!");
            }

            var correctAuthor = new AuthorDto
            {
                Id = multipleChoiceQuestion.CorrectAuthorId,
                Name = multipleChoiceQuestion.CorrectAuthor.Name
            };

            var answers = multipleChoiceQuestion.Answers
                .Select(answer => new AuthorDto
                {
                    Id = answer.AuthorChoice.Id,
                    Name = answer.AuthorChoice.Name
                })
                .ToList();

            answers.Add(correctAuthor);

            var questionModel = new MultipleChoiceQuizQuestionModel
            {
                Id = multipleChoiceQuestion.Id,
                QuoteId = multipleChoiceQuestion.QuoteId,
                Quote = multipleChoiceQuestion.Quote.Content,
                CorrectAuthorId = multipleChoiceQuestion.CorrectAuthorId,
                Authors = answers
            };

            return questionModel;
        }

        public async Task<List<BinaryChoiceQuestionModel>> GetBinaryChoiceQuestionsAsync(int initialId = 0)
        {
            var lastQuestion = await GetLastBinaryChoiceQuestionAsync();
            initialId = 0;

            if (initialId >= lastQuestion.Id)
            {
                throw new Exception("Question limit has been reached!");
            }

            return await DbContext
                .BinaryQuestions
                .AsNoTracking()
                .Include(question => question.QuestionableAuthor)
                .Include(question => question.Quote)
                .Where(question => question.Id > initialId)
                .OrderBy(question => question.Id)
                .Select(question => new BinaryChoiceQuestionModel
                {
                    Id = question.Id,
                    QuoteId = question.QuoteId,
                    Quote = question.Quote.Content,
                    QuestionableAuthorId = question.QuestionableAuthorId,
                    QuestionableAuthor = question.QuestionableAuthor.Name,
                    CorrectAuthorId = question.CorrectAuthorId,
                    CorrectAuthor = question.CorrectAuthor.Name,
                    IsTrue = question.IsTrue
                })
                .ToListAsync();
        }

        public async Task<List<MultipleChoiceQuizQuestionModel>> GetMultipleChoiceQuizQuestionsAsync(int initialId = 0)
        {
            var lastQuestion = await GetLastMultipleChoiceQuestionAsync();

            if (initialId >= lastQuestion.Id)
            {
                throw new Exception("No multiple choice question found!");
            }
            var authors = await DbContext.Authors.Select(x => new AuthorDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            var multipleChoiceQuestion = await DbContext
                .MultipleChoiceQuestions
                .AsNoTracking()
                .Include(question => question.Quote)
                .Include(question => question.CorrectAuthor)
                .Include(question => question.Answers)
                .ThenInclude(answer => answer.AuthorChoice)
                .Where(question => question.Id > initialId)
                .Select(x => new MultipleChoiceQuizQuestionModel
                {
                    Id = x.Id,
                    Quote = x.Quote.Content,
                    QuoteId = x.QuoteId,
                    CorrectAuthorId = x.CorrectAuthorId,
                    Authors = authors
                })
                .ToListAsync();


            return multipleChoiceQuestion;
        }

    }
}
