using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain;

namespace ArvestusAPI.DTO
{
    public class AnswerEdit
    {
        [Required]
        [MaxLength(255)]
        public string Answer { get; set; }

        [Required]
        public int QuestionId { get; set; }


        public Answer UpdateAnswer(Answer answer, AnswerEdit answerEdit)
        {
            answer.AnswerText = answerEdit.Answer;
            answer.QuestionId = answerEdit.QuestionId;

            return answer;
        }
    }


    public class AnswerFactory
    {
        public static Answer CreateAnswerFromAnswerEdit(AnswerEdit answerEdit)
        {
            return new Answer()
            {
                AnswerText = answerEdit.Answer,
                QuestionId = answerEdit.QuestionId,
            };
        }


    }
}