using System;
using System.Web.Http;
using ArvestusAPI.DTO;
using ArvestusAPI.Services;
using DAL.Interfaces;


namespace ArvestusAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/Answer")]
    public class AnswerController : ApiController
    {
        private readonly IUOW _uow;

        public AnswerController(IUOW uow)
        {
            _uow = uow;
        }

        [HttpGet]
        [Route("")]
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

           (new AnswerService(_uow.GetRepository<IAnswerRepository>())).Delete(id);
            _uow.Commit();

            return Ok();
        }


        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Edit(int id, AnswerEdit model)
        {
            AnswerService service = new AnswerService(_uow.GetRepository<IAnswerRepository>());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            service.EditAnswer(id, model);
            _uow.Commit();

            return Ok(model);
          
        }

    }
}
