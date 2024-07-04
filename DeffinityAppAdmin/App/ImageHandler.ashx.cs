using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DeffinityAppDev.App
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandlerTop : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            int imageId = Convert.ToInt32(context.Request.QueryString["id"]);

            string type = QueryStringValues.Type;

            byte[] imageData;

            if(type == "user")
            {
                IUserRepository<UserMgt.Entity.Contractor> pRep = new UserRepository<UserMgt.Entity.Contractor>();
                var p = pRep.GetAll().Where(o => o.ID == imageId).FirstOrDefault();
                if (p != null)
                {
                    if (p.ImageData != null)
                    {
                        imageData = (byte[])p.ImageData.ToArray();
                        context.Response.ContentType = "image/png";
                        context.Response.BinaryWrite(imageData);
                    }
                   
                }
            }
            else
            {
                IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();
                var p = pRep.GetAll().Where(o => o.ID == imageId).FirstOrDefault();
                if (p != null)
{
                    if (p.ImageData != null)
                    {
                        imageData = (byte[])p.ImageData.ToArray();
                        context.Response.ContentType = "image/png";
                        context.Response.BinaryWrite(imageData);
                    }
                }
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