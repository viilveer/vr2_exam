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

        public List<Answer> GetListByQuestion(int questionId)
        {
            return DbSet.Where(x => x.QuestionId == questionId).Include(p => p.Question).ToList();
        }

        public List<Answer> GetListByAnswerText(string answer)
        {
            return DbSet.Where(x => x.AnswerText.Contains(answer)).ToList();
        }
    }
}
