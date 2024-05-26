using FlatRockProject.Infrastructure.Entities;
using FlatRockProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;

namespace FlatRockProject.API.Config
{
    public static class DbContextExtensions
    {
        private const string StephenKingQuote = "I try to create sympathy for my characters, then turn the monsters loose.";
        private const string ErnestHemingwayQuote = "Prose is architecture, not interior decoration.";
        private const string JohnBrownFalseQuote =
            "It’s none of their business that you have to learn to write. Let them think you were born that way.";
        private const string MarkTwainQuote
            = "Most writers regard the truth as their most valuable possession, and therefore are most economical in its use.";
        private const string IvanIvanovFalseQuote = "To produce a mighty book, you must choose a mighty theme.";

        public static void Seed(this FRPDbContext dbContext)
        {
            dbContext.Database.Migrate();
            if (!Enumerable.Any(dbContext.Quote))
            {
                AddQuote(dbContext, StephenKingQuote);
                AddQuote(dbContext, ErnestHemingwayQuote);
                AddQuote(dbContext, JohnBrownFalseQuote);
                AddQuote(dbContext, MarkTwainQuote);
                AddQuote(dbContext, IvanIvanovFalseQuote);

            }

            if (!Enumerable.Any(dbContext.Authors))
            {
                AddAuthor(dbContext, "Stephen King");
                AddAuthor(dbContext, "Ernest Hemingway");
                AddAuthor(dbContext, "John Brown");
                AddAuthor(dbContext, "Ivan Ivanov");
                AddAuthor(dbContext, "Mark Twain");
            }

            if (!Enumerable.Any(dbContext.BinaryQuestions))
            {
                AddBinaryQuestion(dbContext, StephenKingQuote, "Stephen King", "Stephen King");
                AddBinaryQuestion(dbContext, ErnestHemingwayQuote, "Ernest Hemingway", "Ernest Hemingway");
                AddBinaryQuestion(dbContext, JohnBrownFalseQuote, "John Brown", "Ernest Hemingway");
                AddBinaryQuestion(dbContext, MarkTwainQuote, "Mark Twain", "Mark Twain");
                AddBinaryQuestion(dbContext, IvanIvanovFalseQuote, "Ivan Ivanov", "Stephen King");

            }

            if (!Enumerable.Any(dbContext.MultipleChoiceQuestions))
            {
                AddMultipleChoiceQuestion(dbContext, StephenKingQuote, "Stephen King");
                AddMultipleChoiceQuestion(dbContext, MarkTwainQuote, "Mark Twain");
            }
        }
      
        private static void AddAuthor(FRPDbContext dbContext, string name)
        {
            var author = new Author
            {
                CreateDate = DateTime.UtcNow,
                Name = name
            };

            dbContext.Authors.Add(author);
            dbContext.SaveChanges(true);
        }

        private static void AddQuote(FRPDbContext dbContext, string content)
        {
            var quote = new Quote
            {
                CreateDate = DateTime.UtcNow,
                Content = content
            };

            dbContext.Quote.Add(quote);
            dbContext.SaveChanges(true);
        }

        private static void AddBinaryQuestion(
            FRPDbContext dbContext,
            string quote,
            string questionableAuthor,
            string correctAuthor)
        {
            var quoteId = dbContext.Quote.FirstOrDefault(q => q.Content == quote)?.Id;
            var questionableAuthorId = dbContext
                .Authors
                .FirstOrDefault(a => a.Name == questionableAuthor)?
                .Id;

            var correctAuthorId = dbContext
                .Authors
                .FirstOrDefault(a => a.Name == correctAuthor)?
                .Id;

            var binaryChoiceQuestion = new BinaryQuestion
            {
                QuoteId = quoteId.Value,
                QuestionableAuthorId = questionableAuthorId.Value,
                CorrectAuthorId = correctAuthorId.Value
            };

            dbContext.BinaryQuestions.Add(binaryChoiceQuestion);
            dbContext.SaveChanges(true);
        }

        private static void AddMultipleChoiceQuestion(
            FRPDbContext dbContext,
            string quote,
            string correctAuthor)
        {
            var quoteId = dbContext.Quote.FirstOrDefault(q => q.Content == quote)?.Id;
            var authorId = dbContext.Authors.FirstOrDefault(a => a.Name == correctAuthor)?.Id;

            var firstIncrAuthor = dbContext.Authors.FirstOrDefault(a => a.Id != authorId);
            var secIncrAuthor = dbContext.Authors.FirstOrDefault(a => a.Id != authorId &&
                                                                       a.Id != firstIncrAuthor.Id);

            var multipleChoiceQuestion = new MultipleChoiceQuestion
            {
                QuoteId = quoteId.Value,
                CorrectAuthorId = authorId.Value
            };

            dbContext.MultipleChoiceQuestions.Add(multipleChoiceQuestion);
            dbContext.SaveChanges(true);

            var answerX = new MultipleChoiceAnswer
            {
                MultipleChoiceQuestionId = multipleChoiceQuestion.Id,
                AuthorChoiceId = firstIncrAuthor.Id
            };

            var answerY = new MultipleChoiceAnswer
            {
                MultipleChoiceQuestionId = multipleChoiceQuestion.Id,
                AuthorChoiceId = secIncrAuthor.Id
            };

            dbContext.MultipleChoiceAnswers.AddRange(new List<MultipleChoiceAnswer>
            {
                answerY,
                answerX
            });

            dbContext.SaveChanges();
        }
    }
}
