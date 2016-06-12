using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using API.DAL.Interfaces;
using API.DAL.Repositories;
using Newtonsoft.Json.Linq;

using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace API_DAL
{
    public class UOW : IUOW, IDisposable
    {

        private readonly IDictionary<Type, Func<HttpClient, object>> _repositoryFactories;

        private readonly IDictionary<Type, object> _repositories = new Dictionary<Type, object>();

        private readonly HttpClient _httpClient = new HttpClient();


        public UOW()
        {
            // initialize list of repo factories
            _repositoryFactories = GetCustomFactories();

            // set up httpclient
            var baseAddr = ConfigurationManager.AppSettings["WebApi_BaseUri"];
            if (string.IsNullOrWhiteSpace(baseAddr))
            {
                throw new KeyNotFoundException("WebApi_BaseUri not defined in config!");
            }
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.BaseAddress = new Uri(baseAddr);

        }

        private static IDictionary<Type, Func<HttpClient, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<HttpClient, object>>
            {
              
                {typeof (IAnswerRepository), (httpClient) => new AnswerRepository(
                    httpClient,
                    ConfigurationManager.AppSettings["WebApi_EndPoint_Answers"]
                    )
                },
                {typeof (IQuestionRepository), (httpClient) => new QuestionRepository(
                    httpClient,
                    ConfigurationManager.AppSettings["WebApi_EndPoint_Questions"]
                    )
                }
            };
        }

        /// <summary>
        /// Returns repo instance by repo interface
        /// Repo is first searched from cache, if not found then its created new and stored into cache
        /// </summary>
        /// <typeparam name="TRepo">Repo interface</typeparam>
        /// <returns></returns>
        public TRepo GetRepository<TRepo>() where TRepo : class
        {
            var repo = GetWebApiRepo<TRepo>() as TRepo;
            if (repo == null)
            {
                throw new NotImplementedException("No repository for type, " + typeof(TRepo).FullName);
            }
            return repo;
        }

        private TRepo GetWebApiRepo<TRepo>() where TRepo : class
        {

            // Look for TRepo in dictionary cache under typeof(TRepo).
            object repo;
            _repositories.TryGetValue(typeof(TRepo), out repo);
            if (repo != null)
            {
                return (TRepo)repo;
            }

            return MakeRepository<TRepo>();
        }

        protected virtual TRepo MakeRepository<TRepo>() where TRepo : class
        {
            // repo factory (delegate), not yet initialized
            Func<HttpClient, object> repoFactory;

            // try to get factroy for this repo type
            _repositoryFactories.TryGetValue(typeof(TRepo), out repoFactory);
            if (repoFactory == null)
            {
                throw new NotImplementedException("No factory for repository type: " + typeof(TRepo).FullName);
            }

            

            // make the repo
            var repo = (TRepo)repoFactory(_httpClient);

            // save it to dictionary
            _repositories[typeof(TRepo)] = repo;

            //return it
            return repo;
        }


        /// <summary>
        /// Not used in Web API
        /// </summary>
        public void Commit()
        {

        }

        public void Dispose()
        {
        }

    }
}