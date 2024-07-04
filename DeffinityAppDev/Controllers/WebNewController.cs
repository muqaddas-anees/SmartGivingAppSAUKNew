using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeffinityAppDev.Controllers
{
    public class WebNewController : Controller
    {
        // GET: WebNew
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
            if (fOrg != null)
            {
                // var fOrg = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == context.Request.QueryString["orgguid"]).FirstOrDefault();
                sessionKeys.PortfolioID = fOrg.ID;
                sessionKeys.PortfolioName = fOrg.PortFolio;
                returnurl = "~/OrgHomeNewV2.aspx?orgguid=" + fOrg.OrgarnizationGUID + "&ngid=" + DateTime.Now.Ticks;
                Response.Redirect(returnurl, false);
                // Response.Redirect("~/OrgHandler.ashx?orgguid=" + fOrg.OrgarnizationGUID+"&dt="+DateTime.Now.Ticks, false);
            }
            else
            {
                // returnurl = "~/Default.aspx";
                Response.Redirect("~/Default.aspx", false);
            }

            return View();
        }
    }
}