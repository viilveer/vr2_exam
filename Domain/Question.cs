using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum QuestionActivity
    {
        Inactive,
        Active,
    }

    public class Question : BaseEntity
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        [MaxLength(255)]
        public string QuestionName { get; set; }
        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }
        [Required]
        public QuestionActivity IsActive { get; set; }
        [Required]
        [Index("IX_QuestionDateTime", 1, IsUnique = true)]
        public DateTime QuestionDateTime { get; set; }

        public ICollection<Answer> Answers { get; set; }

        
    }
}
