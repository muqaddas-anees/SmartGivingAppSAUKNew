using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeffinityAppDev.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
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
            IPortfolioRepository<PortfolioMgt.Entity.FundraisersInfo> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersInfo>();
            var f = fRep.GetAll().Where(o => o.ShortCode == orgname).FirstOrDefault();
            var returnurl = "";
            var fEvent = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == f.MainFundUNID).FirstOrDefault();
            var fOrg = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == fEvent.OrganizationID).FirstOrDefault();
            if (f != null)
            {
                // var fOrg = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == context.Request.QueryString["orgguid"]).FirstOrDefault();
                sessionKeys.PortfolioID = fOrg.ID;
                sessionKeys.PortfolioName = fOrg.PortFolio;

                //EventDetailsNew.aspx?unid=b34266d6-d035-4bbc-ad12-3da5fd7a907c
                returnurl = "~/Register.aspx?unid=" + f.ShortCode + "&ngid=" + DateTime.Now.Ticks;


                return Redirect(returnurl);
                return null;
                // Response.Redirect("~/OrgHandler.ashx?orgguid=" + fOrg.OrgarnizationGUID+"&dt="+DateTime.Now.Ticks, false);
            }
            else
            {
                // returnurl = "~/Default.aspx";
                return Redirect("~/Default.aspx");
              //  return null;
            }

            //ViewBag.Title = "Home Page";

           // return View();
        }
    }
}