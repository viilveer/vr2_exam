using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace ArvestusAPI.DTO
{
    public class AnswerView
    {
        public int Id { get; set; }

        public string Answer { get; set; }
    }

    public class AnswerViewFactory
    {
        public static AnswerView CreateFromAnswer(Answer answer)
        {
            return new AnswerView()
            {
                Id = answer.AnswerId,
                Answer = answer.AnswerText,
            };
        }
    }
}