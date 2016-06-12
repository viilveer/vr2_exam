using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Net.Http;
using API.DAL.Interfaces;
using ClientBLL.ViewModels;
using Domain;

namespace API.DAL.Repositories
{
    class QuestionRepository : ApiRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }

        public List<QuestionView> GetList(String name, String description)
        {
            var response = HttpClient.GetAsync(EndPoint + $"?name={name}&description={description}").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<QuestionView>>().Result;
                return res;
            }

            throw new ObjectNotFoundException("Not found");

        }

        public QuestionView GetOne(int id)
        {
            var response = HttpClient.GetAsync(EndPoint + $"{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<QuestionView>().Result;
                return res;
            }

            throw new ObjectNotFoundException("Not found");
        }

        public QuestionEdit UpdateQuestion(int id, QuestionEdit model)
        {
            var response = HttpClient.PutAsJsonAsync(EndPoint + $"{id}", model).Result;
            if (!response.IsSuccessStatusCode)
            {

                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
            return response.Content.ReadAsAsync<QuestionEdit>().Result;
        }

        public QuestionEdit CreateQuestion(QuestionEdit model)
        {
            var response = HttpClient.PostAsJsonAsync(EndPoint, model).Result;
            if (!response.IsSuccessStatusCode)
            {

                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
            return response.Content.ReadAsAsync<QuestionEdit>().Result;
        }

        public void ToggleQuestionStatus(int id)
        {
            var response = HttpClient.PutAsync(EndPoint + $"toggle-status/{id}", null).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
        }


        public List<AnswerView> GetListByQuestion(int questionId)
        {
            var response = HttpClient.GetAsync(EndPoint + $"{questionId}/Answers").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<AnswerView>>().Result;
                return res;
            }

            throw new ObjectNotFoundException("Not found");
        }


        public AnswerEdit CreateAnswer(AnswerEdit model)
        {
            var response = HttpClient.PostAsJsonAsync(EndPoint + "Answer", model).Result;
            if (!response.IsSuccessStatusCode)
            {

                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
            return response.Content.ReadAsAsync<AnswerEdit>().Result;
        }
    }
}
