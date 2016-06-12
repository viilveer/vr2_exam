using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Answer : BaseEntity
    {
        [Key]
        public int AnswerId { get; set; }
        
        public Question Question { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        [MaxLength(255)]
        public string AnswerText { get; set; }
    }
}
