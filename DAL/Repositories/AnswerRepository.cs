using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using DAL.Repositories;
using Domain;

namespace DAL.Repositories
{
    class AnswerRepository : EFRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(IDbContext dbContext) : base(dbContext)
        {


        }

        public List<Question> GetListByQuestion(int questionId)
        {
            //start building up the query
            return DbSet.Where(x => x.QuestionId == questionId).Include(p => p.Question).ToList();
        }
    }
}
