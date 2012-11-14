using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webstep.People.WebClient.Models;

namespace Webstep.Fagkomiteen.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult TechEvening()
        {
            return View();
        }

        public ActionResult TechWeekend()
        {
            return View();
        }

        public ActionResult PlayGround()
        {
        
            return View();
        }

        public string Recreate()
        {
            new WebstepContext().Recreate();
            return "Recreated";
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
