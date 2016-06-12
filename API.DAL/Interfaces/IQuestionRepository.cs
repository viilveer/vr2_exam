using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientBLL.ViewModels;
using Domain;

namespace API.DAL.Interfaces
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        List<QuestionView> GetList(String name, String description);
        QuestionView GetOne(int id);

        QuestionEdit UpdateQuestion(int id, QuestionEdit model);
        QuestionEdit CreateQuestion(QuestionEdit model);
        void ToggleQuestionStatus(int id);

        List<AnswerView> GetListByQuestion(int questionId);

        AnswerEdit CreateAnswer(AnswerEdit model);
    }
}
