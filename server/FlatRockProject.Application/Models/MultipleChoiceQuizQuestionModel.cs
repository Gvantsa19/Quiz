using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRockProject.Application.Models
{
    public class MultipleChoiceQuizQuestionModel : BaseQuizQuestionModel
    {
        public long CorrectAuthorId { get; set; }

        public IEnumerable<AuthorDto> Authors { get; set; }
    }
}
