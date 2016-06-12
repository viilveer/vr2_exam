using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    class QuestionRepository : EFRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IDbContext dbContext) : base(dbContext)
        {
        
        }

        public List<Question> GetList(String name, String description)
        {

            //start building up the query
            var res = DbSet.Include(p => p.Answers);

            if (name != null)
            {
                res = res.Where(x => x.QuestionName.Contains(name));
            }

            if (description != null)
            {
                res = res.Where(x => x.QuestionName.Contains(description));
            }


            var reslist = res.ToList();

            return reslist;
        }
    }
}
