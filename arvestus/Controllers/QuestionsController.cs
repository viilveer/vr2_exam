using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using API.DAL.Interfaces;
using ClientBLL.ViewModels;
using NLog;

namespace arvestus.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly string _instanceId = Guid.NewGuid().ToString();
        private readonly IUOW _uow;

        public QuestionsController(ILogger logger, IUOW uow)
        {
            _uow = uow;

            logger.Debug("InstanceId: " + _instanceId);
        }

        // GET: Questions
        public ActionResult Index(string name = "", string description = "")
        {
            return View(_uow.GetRepository<IQuestionRepository>().GetList(name, description).ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            QuestionView question = _uow.GetRepository<IQuestionRepository>().GetOne(id.Value);
            if (question == null)
            {
                return HttpNotFound();
            }
            QuestionDetails model = new QuestionDetails()
            {
                Model = question,
                Answers = _uow.GetRepository<IQuestionRepository>().GetListByQuestion(id.Value)
            };
            return View(model);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            QuestionEdit questionEdit = new QuestionEdit();
            return View(questionEdit);
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionEdit editModel)
        {
            if (ModelState.IsValid)
            {
                _uow.GetRepository<IQuestionRepository>().CreateQuestion(editModel);
                return RedirectToAction("Index");
            }

            return View(editModel);
        }

        // GET: Questions/Create
        public ActionResult CreateAnswer(int questionId)
        {
            AnswerEdit questionEdit = new AnswerEdit();
            return View(questionEdit);
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAnswer(int questionId, AnswerEdit editModel)
        {
            QuestionView question = _uow.GetRepository<IQuestionRepository>().GetOne(questionId);
            if (question == null)
            {
                return HttpNotFound();
            }
            editModel.QuestionId = questionId;
            if (ModelState.IsValid)
            {
                _uow.GetRepository<IQuestionRepository>().CreateAnswer(editModel);
                return RedirectToAction("Index");
            }

            return View(editModel);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionView question = _uow.GetRepository<IQuestionRepository>().GetOne(id.Value);

            if (question == null)
            {
                return HttpNotFound();
            }

            QuestionEdit editModel = new QuestionEdit()
            {
                Date = question.QuestionDate,
                Description = question.Description,
                Id = question.Id,
                IsActive = question.IsActive,
                Question = question.Question
            };
           
            return View(editModel);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, QuestionEdit model)
        {
            if (ModelState.IsValid)
            {
                _uow.GetRepository<IQuestionRepository>().UpdateQuestion(id, model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionView question = _uow.GetRepository<IQuestionRepository>().GetOne(id.Value);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionView question = _uow.GetRepository<IQuestionRepository>().GetOne(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            _uow.GetRepository<IQuestionRepository>().Delete(id);
            return RedirectToAction("Index");
        }

        // POST: Questions/Delete/5
        [HttpGet, ActionName("ToggleStatus")]
        public ActionResult ToggleStatus(int id)
        {
            QuestionView question = _uow.GetRepository<IQuestionRepository>().GetOne(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            _uow.GetRepository<IQuestionRepository>().ToggleQuestionStatus(id);
            return RedirectToAction("Index");
        }

    }
}
