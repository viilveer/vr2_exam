using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace ClientBLL.ViewModels
{
    public class QuestionView
    {
        public int Id { get; set; }

        public string Question { get; set; }
        public string Description { get; set; }
        public QuestionActivity IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public DateTime QuestionDate { get; set; }
    }

    public class QuestionViewFactory
    {
        public static QuestionView CreateFromQuestion(Question question)
        {
            return new QuestionView()
            {
                Id = question.QuestionId,
                Question = question.QuestionName,
                Description = question.Description,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt,
                IsActive = question.IsActive,
                QuestionDate = question.QuestionDateTime
            };
        }
    }
}