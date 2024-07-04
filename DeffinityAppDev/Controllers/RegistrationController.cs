using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeffinityAppDev.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            string controllerName = ControllerContext.RouteData.GetRequiredString("controller");
            //string orgname = ControllerContext.RouteData.GetRequiredString("ID");
           var  returnurl = "~/Registration.aspx?ngid=" + DateTime.Now.Ticks;


            return Redirect(returnurl);

            //ViewBag.Title = "Home Page";

            // return View();
        }
    }
}