using System.ComponentModel.DataAnnotations;

namespace FlatRockProject.Infrastructure.Entities
{
    public class MultipleChoiceAnswer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MultipleChoiceQuestionId { get; set; }
        public virtual MultipleChoiceQuestion MultipleChoiceQuestion { get; set; }
        [Required]
        public int AuthorChoiceId { get; set; }
        public virtual Author AuthorChoice { get; set; }
    }
}
