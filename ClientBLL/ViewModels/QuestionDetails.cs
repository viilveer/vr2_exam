using System;
using System.Collections.Generic;


namespace ClientBLL.ViewModels
{
    public class QuestionDetails
    {
        public QuestionView Model { get; set; }

        public List<AnswerView> Answers { get; set; }
    }
}
