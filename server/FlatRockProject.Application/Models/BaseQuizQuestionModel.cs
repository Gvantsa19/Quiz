using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRockProject.Application.Models
{
    public class BaseQuizQuestionModel
    {
        public int Id { get; set; }

        public int QuoteId { get; set; }
        public string Quote { get; set; }
    }
}
