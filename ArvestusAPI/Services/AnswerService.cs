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

        public void CreateAnswer(QuestionEdit model)
        {
            //_repo.Add(QuestionFactroy.CreateFromQuestionCreate(model));
        }

        public void EditAnswer(int questionId, QuestionEdit model)
        {
            //Question question = _repo.GetById(questionId);
            //question = model.UpdateQuestion(question, model);
            //_repo.Update(question);
        }


        public List<QuestionView> GetAnswers(int questionId)
        {
            //return _repo.GetList(question, description).Select(QuestionViewFactory.CreateFromQuestion).ToList();
        }

        public bool CanEdit(int questionId)
        {
            Question question = _repo.GetById(questionId);

            return question?.IsActive == QuestionActivity.Active;
        }

        public void Delete(int questionId)
        {
            if (CanEdit(questionId))
            {
                _repo.Delete(questionId);
            }
        }
    }
}