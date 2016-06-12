using System.Collections.Generic;
using Domain;

namespace DAL.Interfaces
{
    public interface IAnswerRepository : IBaseRepository<Answer>
    {
        List<Answer> GetListByQuestion(int questionId);
    }
}
