using FlatRockProject.Infrastructure.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FlatRockProject.Infrastructure.Entities
{
    public class Quote : BaseDeletableModel<long>
    {
        [Required]
        public string Content { get; set; }
        public virtual ICollection<BinaryQuestion> BinaryChoiceQuestions { get; set; }
            = new HashSet<BinaryQuestion>();

        public virtual ICollection<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
            = new HashSet<MultipleChoiceQuestion>();
    }
}
