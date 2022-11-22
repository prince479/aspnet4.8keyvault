using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnvVariablesV4._8.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var appsettings = ConfigurationManager.AppSettings;
            ViewBag.SMTP_URL = appsettings["SMTP_URL"];
            ViewBag.SMTP_EMAIL = appsettings["SMTP_EMAIL"];
            ViewBag.URI = appsettings["uri"];
            ViewBag.Secret = appsettings["ACOM--DSN--AstrologyDotCom"];
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