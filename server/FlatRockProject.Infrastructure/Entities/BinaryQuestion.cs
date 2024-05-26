using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlatRockProject.Infrastructure.Entities
{
    public class BinaryQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuoteId { get; set; }
        [JsonIgnore]
        public virtual Quote Quote { get; set; }

        [Required]
        public int QuestionableAuthorId { get; set; }
        public virtual Author QuestionableAuthor { get; set; }

        [Required]
        public int CorrectAuthorId { get; set; }
        public virtual Author CorrectAuthor { get; set; }

        [NotMapped]
        public bool IsTrue => QuestionableAuthorId == CorrectAuthorId;
    }
}
