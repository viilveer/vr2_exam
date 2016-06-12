using System;
using System.Collections.Generic;
using System.Linq;
using ArvestusAPI.DTO;
using DAL.Interfaces;
using Domain;

namespace ArvestusAPI.Services
{
    public class QuestionService

    {
        private readonly IQuestionRepository _repo;

        public QuestionService(IQuestionRepository repo)
        {
            _repo = repo;
        }

        public void CreateQuestion(QuestionEdit model)
        {
            _repo.Add(QuestionFactroy.CreateFromQuestionCreate(model));
        }

        public void EditQuestion(int questionId, QuestionEdit model)
        {
            Question question = _repo.GetById(questionId);
            question = model.UpdateQuestion(question, model);
            _repo.Update(question);
        }


        public List<QuestionView> GetQuestions(String question, String description)
        {
            return _repo.GetList(question, description).Select(QuestionViewFactory.CreateFromQuestion).ToList();
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

        public bool ToggleStatus(int id)
        {
            Question question = _repo.GetById(id);

            if (question != null)
            {
                question.IsActive = question.IsActive == QuestionActivity.Active
                    ? QuestionActivity.Inactive
                    : QuestionActivity.Active;
                _repo.Update(question);
                return true;
            }

            return false;
        }

        public QuestionView GetOne(int id)
        {
            return QuestionViewFactory.CreateFromQuestion(_repo.GetById(id));
        }
    }
}