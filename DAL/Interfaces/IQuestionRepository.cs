using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Interfaces
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        List<Question> GetList(String name, String description);
    }
}
