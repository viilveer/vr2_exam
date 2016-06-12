using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API.DAL.Interfaces;
using Arvestus.ViewModels;
using NLog;

namespace Arvestus.Controllers
{
    public class HomeController : Controller
    {
        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();
        private readonly IUOW _uow;

        public HomeController(ILogger logger, IUOW uow)
        {
            _logger = logger;
            _uow = uow;

            _logger.Debug("InstanceId: " + _instanceId);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}