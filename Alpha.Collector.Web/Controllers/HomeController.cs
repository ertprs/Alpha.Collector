using Alpha.Collector.Dao;
using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Alpha.Collector.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}