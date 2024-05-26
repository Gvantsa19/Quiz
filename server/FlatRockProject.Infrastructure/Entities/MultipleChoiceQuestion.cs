using System.ComponentModel.DataAnnotations;

namespace FlatRockProject.Infrastructure.Entities
{
    public class MultipleChoiceQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuoteId { get; set; }
        public virtual Quote Quote { get; set; }

        [Required]
        public int CorrectAuthorId { get; set; }
        public virtual Author CorrectAuthor { get; set; }

        public virtual ICollection<MultipleChoiceAnswer> Answers { get; set; } = new HashSet<MultipleChoiceAnswer>();
    }
}
