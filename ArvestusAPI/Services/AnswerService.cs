using System;
using System.Collections.Generic;
using System.Linq;
using ArvestusAPI.DTO;
using DAL.Interfaces;
using Domain;

namespace ArvestusAPI.Services
{
    public class AnswerService

    {
        private readonly IAnswerRepository _repo;

        public AnswerService(IAnswerRepository repo)
        {
            _repo = repo;
        }

        public void CreateAnswer(AnswerEdit model)
        {
            _repo.Add(AnswerFactory.CreateAnswerFromAnswerEdit(model));
        }

        public void EditAnswer(int questionId, AnswerEdit model)
        {
            Answer answer = _repo.GetById(questionId);
            answer = model.UpdateAnswer(answer, model);
            _repo.Update(answer);
        }


        public List<AnswerView> GetAnswers(int questionId)
        {
            return _repo.GetListByQuestion(questionId).Select(AnswerViewFactory.CreateFromAnswer).ToList();
        }


        public void Delete(int answerId)
        {
           _repo.Delete(answerId);
        }
    }
}