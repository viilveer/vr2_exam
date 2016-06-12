using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Net.Http;
using API.DAL.Interfaces;
using ClientBLL.ViewModels;
using Domain;

namespace API.DAL.Repositories
{
    class AnswerRepository : ApiRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }

    }
}
