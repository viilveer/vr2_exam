using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Arvestus.ViewModels;
using DAL.Interfaces;
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
            TestIndexModel model = new TestIndexModel()
            {
            };
            return View(model);
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