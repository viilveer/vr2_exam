using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Answer : BaseEntity
    {
        public int AnswerId { get; set; }

        public Question Question { get; set; }

        public int QuestionId { get; set; }

        public string AnswerText { get; set; }
    }
}
