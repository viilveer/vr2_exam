using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace ArvestusAPI.DTO
{
    public class QuestionView
    {
        public int Id { get; set; }

        public string Question { get; set; }
        public string Description { get; set; }

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
                QuestionDate = question.QuestionDateTime
            };
        }
    }
}