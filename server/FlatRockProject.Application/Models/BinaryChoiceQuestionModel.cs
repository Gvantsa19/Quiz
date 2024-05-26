using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRockProject.Application.Models
{
    public class BinaryChoiceQuestionModel : BaseQuizQuestionModel
    {
        public long QuestionableAuthorId { get; set; }
        public virtual string QuestionableAuthor { get; set; }

        public long CorrectAuthorId { get; set; }
        public virtual string CorrectAuthor { get; set; }

        public bool IsTrue { get; set; }
    }
}
