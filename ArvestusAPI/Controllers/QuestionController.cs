using System.Web.Http;
using ArvestusAPI.DTO;
using ArvestusAPI.Services;
using DAL.Interfaces;


namespace ArvestusAPI.Controllers
{
    [RoutePrefix("api/Question")]
    public class QuestionController : ApiController
    {
        private readonly IUOW _uow;

        public QuestionController(IUOW uow)
        {
            _uow = uow;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Index(string name = "", string description = "")
        {
            QuestionService service = new QuestionService(_uow.GetRepository<IQuestionRepository>());

            return Ok(service.GetQuestions(name, description));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(QuestionEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            (new QuestionService(_uow.GetRepository<IQuestionRepository>())).CreateQuestion(model);
            _uow.Commit();

            return Ok(model);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetOne(int id)
        {
            QuestionView model = (new QuestionService(_uow.GetRepository<IQuestionRepository>())).GetOne(id);
            if (model != null)
            {
                return Ok(model);
            }
            return BadRequest("Not found");
        }

        [HttpPost]
        [Route("Answer")]
        public IHttpActionResult CreateAnswer(AnswerEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            (new AnswerService(_uow.GetRepository<IAnswerRepository>())).CreateAnswer(model);
            _uow.Commit();

            return Ok(model);
        }

        [HttpGet]
        [Route("{questionId}/Answers")]
        public IHttpActionResult Index(int questionId)
        {
            AnswerService service = new AnswerService(_uow.GetRepository<IAnswerRepository>());

            return Ok(service.GetAnswers(questionId));
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           (new QuestionService(_uow.GetRepository<IQuestionRepository>())).Delete(id);
            _uow.Commit();

            return Ok();
        }


        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Edit(int id, QuestionEdit model)
        {
            QuestionService service = new QuestionService(_uow.GetRepository<IQuestionRepository>());
            if (service.CanEdit(id))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                service.EditQuestion(id, model);
                _uow.Commit();

                return Ok(model);
            }
            ModelState.AddModelError("IsActive", "Question must be inactive to edit");
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("toggle-status/{id}")]
        public IHttpActionResult EToggleStatus(int id)
        {
            QuestionService service = new QuestionService(_uow.GetRepository<IQuestionRepository>());

            if (service.ToggleStatus(id))
            {
                _uow.Commit();
                return Ok();
            }
            return BadRequest();

           
        }
    }
}
