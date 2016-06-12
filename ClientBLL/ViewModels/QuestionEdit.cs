using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain;

namespace ClientBLL.ViewModels
{
    public class QuestionEdit
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Question { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }
        public QuestionActivity IsActive { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Question UpdateQuestion(Question question, QuestionEdit questionCreate)
        {
            question.QuestionName = questionCreate.Question;
            question.Description = questionCreate.Description;
            question.QuestionDateTime = questionCreate.Date;
            question.IsActive = questionCreate.IsActive;

            return question;
        }
    }

    public class QuestionFactroy
    {
        public static Question CreateFromQuestionCreate(QuestionEdit questionCreate)
        {
            return new Question()
            {
                QuestionName = questionCreate.Question,
                Description = questionCreate.Description,
                QuestionDateTime = questionCreate.Date,
                IsActive = questionCreate.IsActive
            };
        }


    }
}