using DocumentFormat.OpenXml.Bibliography;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeffinityAppDev
{
    /// <summary>
    /// Summary description for OrgHandler
    /// </summary>
    public class OrgHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["orgguid"] != null)
            {
                var fOrg = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == context.Request.QueryString["orgguid"]).FirstOrDefault();
                sessionKeys.PortfolioID = fOrg.ID;
                sessionKeys.PortfolioName = fOrg.PortFolio;
                
                context.Response.Redirect("~/OrgHomeNew.aspx?orgguid=" + fOrg.OrgarnizationGUID+"&ngid="+ DateTime.Now.Ticks, false);
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}