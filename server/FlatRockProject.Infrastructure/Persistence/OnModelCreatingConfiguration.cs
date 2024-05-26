using FlatRockProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRockProject.Infrastructure.Persistence
{
    internal static class OnModelCreatingConfiguration
    {
        internal static void ConfigureBinaryChoiceQuestionRelations(this ModelBuilder builder)
        {
            builder
                .Entity<BinaryQuestion>()
                .HasIndex(question => new
                {
                    question.QuoteId,
                    question.CorrectAuthorId,
                    question.QuestionableAuthorId
                })
                .IsUnique();

            builder
                .Entity<BinaryQuestion>()
                .HasOne(question => question.Quote)
                .WithMany(quote => quote.BinaryChoiceQuestions)
                .HasForeignKey(question => question.QuoteId);
        }

        internal static void ConfigureAuthorBinaryChoiceQuestionRelations(this ModelBuilder builder)
        {
            builder.Entity<BinaryQuestion>()
        .HasOne(question => question.QuestionableAuthor)
        .WithMany(author => author.QuestionableInBinaryChoiceQuestions)
        .HasForeignKey(question => question.QuestionableAuthorId)
        .OnDelete(DeleteBehavior.NoAction)
        .IsRequired();

            builder.Entity<BinaryQuestion>()
                .HasOne(question => question.CorrectAuthor)
                .WithMany(author => author.CorrectInBinaryChoiceQuestions)
                .HasForeignKey(question => question.CorrectAuthorId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

        }

        internal static void ConfigureMultipleChoiceQuestionRelations(this ModelBuilder builder)
        {
            builder
                .Entity<MultipleChoiceQuestion>()
                .HasOne(question => question.Quote)
                .WithMany(quote => quote.MultipleChoiceQuestions)
                .HasForeignKey(question => question.QuoteId);
        }

        internal static void ConfigureCorrectInMultipleChoiceQuestionsRelations(this ModelBuilder builder)
        {
            builder
                .Entity<MultipleChoiceQuestion>()
                .HasOne(question => question.CorrectAuthor)
                .WithMany(quote => quote.CorrectInMultipleChoiceQuestions)
                .HasForeignKey(question => question.CorrectAuthorId);
        }

        internal static void ConfigureMultipleChoiceAnswerRelations(this ModelBuilder builder)
        {
            builder
                .Entity<MultipleChoiceAnswer>()
                .HasOne(question => question.AuthorChoice)
                .WithMany(quote => quote.ChoiceInMultipleAnswers)
                .HasForeignKey(question => question.AuthorChoiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
