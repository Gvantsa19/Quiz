using FlatRockProject.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlatRockProject.Infrastructure.Persistence
{
    public class FRPDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public FRPDbContext(DbContextOptions<FRPDbContext> options) : base(options)
        {
            
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Quote> Quote { get; set; }
        public DbSet<BinaryQuestion> BinaryQuestions { get; set; }

        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }

        public DbSet<MultipleChoiceAnswer> MultipleChoiceAnswers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureBinaryChoiceQuestionRelations();
            modelBuilder.ConfigureAuthorBinaryChoiceQuestionRelations();
            modelBuilder.ConfigureMultipleChoiceQuestionRelations();
            modelBuilder.ConfigureCorrectInMultipleChoiceQuestionsRelations();
            modelBuilder.ConfigureMultipleChoiceAnswerRelations();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges() => SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
