using FlatRockProject.Infrastructure.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FlatRockProject.Infrastructure.Entities
{
    public class Author : BaseDeletableModel<long>
    {
        [Required]
        public string Name { get; set; }
        public virtual ICollection<BinaryQuestion> QuestionableInBinaryChoiceQuestions { get; set; }
                  = new HashSet<BinaryQuestion>();

        public virtual ICollection<BinaryQuestion> CorrectInBinaryChoiceQuestions { get; set; }
            = new HashSet<BinaryQuestion>();

        public virtual ICollection<MultipleChoiceQuestion> CorrectInMultipleChoiceQuestions { get; set; }
            = new HashSet<MultipleChoiceQuestion>();

        public virtual ICollection<MultipleChoiceAnswer> ChoiceInMultipleAnswers { get; set; }
            = new HashSet<MultipleChoiceAnswer>();
    }
}
