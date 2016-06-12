using System;
using System.Web.Http;
using ArvestusAPI.DTO;
using ArvestusAPI.Services;
using DAL.Interfaces;
using NLog;


namespace ArvestusAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/Question")]
    public class QuestionController : ApiController
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IUOW _uow;

        public QuestionController(IUOW uow)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Index(string description = "", string question = "")
        {
            QuestionService service = new QuestionService(_uow.GetRepository<IQuestionRepository>());

            return Ok(service.GetQuestions(question, description));
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
            //if (service.CanEdit(id))
            //{
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                service.EditQuestion(id, model);
                _uow.Commit();

                return Ok(model);
            //}
            ////ModelState.AddModelError("IsActive", "Question must be inactive to edit");
            //return BadRequest(ModelState);
        }

    }
}
