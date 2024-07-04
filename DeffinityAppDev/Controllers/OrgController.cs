using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeffinityAppDev.Controllers
{
    public class OrgController : Controller
    {
        // GET: Org
        public ActionResult Index()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            string controllerName = ControllerContext.RouteData.GetRequiredString("controller");
            string orgname = ControllerContext.RouteData.GetRequiredString("ID");
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
            var returnurl = "";
            var fOrg = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgUniqID == orgname).FirstOrDefault();
            if(fOrg != null)
            {
               // Response.Redirect("~/OrgHomeNew.aspx?orgguid="+ fOrg.OrgarnizationGUID,false);
                returnurl = "~/OrgHomeNew.aspx?orgguid=" + fOrg.OrgarnizationGUID;
            }
            else
            {
              //  Response.Redirect("~/Default.aspx",false);
                returnurl = "~/Default.aspx";
            }
            
            ViewBag.Title = "Home Page";

            // return View();
            return new RedirectResult(returnurl,false);
        }
    }
}