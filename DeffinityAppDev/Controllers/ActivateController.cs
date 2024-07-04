using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeffinityAppDev.Controllers
{
    public class ActivateController : Controller
    {
        // GET: Activate
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

            return Redirect("~/Activate.aspx?UNID="+ orgname);
            //var returnurl = "";
            //var fEvent = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectAll().Where(o => o.QRcode == orgname).FirstOrDefault();
            //var fOrg = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == fEvent.OrganizationID).FirstOrDefault();
            //if (fEvent != null)
            //{
            //    // var fOrg = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == context.Request.QueryString["orgguid"]).FirstOrDefault();
            //    sessionKeys.PortfolioID = fOrg.ID;
            //    sessionKeys.PortfolioName = fOrg.PortFolio;

            //    //EventDetailsNew.aspx?unid=b34266d6-d035-4bbc-ad12-3da5fd7a907c
            //    returnurl = "~/EventDetailsNew.aspx?unid=" + fEvent.unid + "&ngid=" + DateTime.Now.Ticks;
            //    return Redirect(returnurl);
            //    // Response.Redirect("~/OrgHandler.ashx?orgguid=" + fOrg.OrgarnizationGUID+"&dt="+DateTime.Now.Ticks, false);
            //}
            //else
            //{
            //    // returnurl = "~/Default.aspx";
            //    return Redirect("~/Default.aspx");
            //}
        }
    }
}