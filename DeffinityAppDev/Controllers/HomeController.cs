using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeffinityAppDev.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //string actionName = ControllerContext.RouteData.GetRequiredString("action");
            //string controllerName = ControllerContext.RouteData.GetRequiredString("controller");
            //string orgname= "";
            //if(ControllerContext.RouteData.Values.Keys.Count == 3)
            //orgname = ControllerContext.RouteData.GetRequiredString("ID");
            //var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            //if (routeValues != null)
            //{
            //    if (routeValues.ContainsKey("action"))
            //    {
            //        var actionName = routeValues["action"].ToString();
            //    }
            //    if (routeValues.ContainsKey("controller"))
            //    {
            //        var controllerName = routeValues["controller"].ToString();
            //    }
            //}
            //if (orgname.Length > 0)
            //{
            //    var fOrg = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgUniqID == orgname).FirstOrDefault();
            //    if (fOrg != null)
            //    {
            //        Response.Redirect("~/OrgHomeNew.aspx?orgguid=" + fOrg.OrgarnizationGUID, false);
            //    }
            //    else
            //    {
            //        Response.Redirect("~/Default.aspx", false);
            //    }
            //}
            //else
            //{
            //    Response.Redirect("~/Default.aspx", false);
            //}
            Response.Redirect("~/Default.aspx", false);
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
