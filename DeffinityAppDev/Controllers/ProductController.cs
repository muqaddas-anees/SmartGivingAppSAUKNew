using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeffinityAppDev.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
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

            IPortfolioRepository<PortfolioMgt.Entity.ProductDetail> pd = new PortfolioRepository<PortfolioMgt.Entity.ProductDetail>();
            var p = pd.GetAll().Where(o => o.ShortCode == orgname).FirstOrDefault();
            var returnurl = "";
            //var fEvent = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectAll().Where(o => o.QRcode == orgname).FirstOrDefault();
            var fOrg = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == p.PortfolioID).FirstOrDefault();
            if (p != null)
            {
                // var fOrg = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == context.Request.QueryString["orgguid"]).FirstOrDefault();
                sessionKeys.PortfolioID = p.ID;
                sessionKeys.PortfolioName = fOrg.PortFolio;

                //EventDetailsNew.aspx?unid=b34266d6-d035-4bbc-ad12-3da5fd7a907c
                returnurl = "~/ProductView.aspx?unid=" + p.ProductGuid + "&ngid=" + DateTime.Now.Ticks;
              //  Response.Redirect(returnurl, false);

                return Redirect(returnurl);
                // Response.Redirect("~/OrgHandler.ashx?orgguid=" + fOrg.OrgarnizationGUID+"&dt="+DateTime.Now.Ticks, false);
            }
            else
            {
                // returnurl = "~/Default.aspx";
              //  Response.Redirect("~/Default.aspx", false);

                return Redirect("~/Default.aspx");
            }

            //ViewBag.Title = "Home Page";

           //return View();
        }
    }
}