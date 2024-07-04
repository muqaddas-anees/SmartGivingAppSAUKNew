using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DeffinityAppDev.WF.Admin
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            //write your handler implementation here.
            if (!string.IsNullOrEmpty(QueryStringValues.Type))
            {
                int id = 0;
                if (context.Request["id"] != null)
                    id = Convert.ToInt32(context.Request["id"].ToString());
              
                if (QueryStringValues.Type.ToLower() == "user")
                {
                    if (id == 0)
                    {
                        if (context.Request["v"] != null)
                        {
                            id = -99;
                        }
                        else
                            id = sessionKeys.UID;
                    }
                    

                    //user image display
                    HttpResponse r = context.Response;
                    r.ContentType = "image/png";
                    string defaultImg = context.Server.MapPath("~/WF/UploadData/Users/ThumbNailsMedium/user_0.png");
                    //string userImg = context.Server.MapPath(string.Format("~/WF/UploadData/Users/ThumbNailsMedium/user_{0}.png", id));
                    string userImg = context.Server.MapPath(string.Format("~/WF/UploadData/Users/ThumbNails/user_{0}.png", id));
                    if (File.Exists(userImg))
                        r.WriteFile(userImg);
                    else
                        r.WriteFile(defaultImg);
                }
                else if (QueryStringValues.Type.ToLower() == "customer")
                {
                    if (id == 0)
                    {
                        id = sessionKeys.PortfolioID;
                    }
                    //user image display
                    HttpResponse r = context.Response;
                    r.ContentType = "image/png";
                    string defaultImg = context.Server.MapPath("~/WF/UploadData/Customers/portfolio_0.png");
                    string userImg = context.Server.MapPath(string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", id));
                    string userImgGif = context.Server.MapPath(string.Format("~/WF/UploadData/Customers/portfolio_{0}.gif", id));
                    if (File.Exists(userImg))
                        r.WriteFile(userImg);
                    else if (File.Exists(userImgGif))
                        r.WriteFile(userImgGif);
                    else
                        r.WriteFile(defaultImg);
                }
                else if (QueryStringValues.Type.ToLower() == "loginlogo")
                {
                    //user image display
                    HttpResponse r = context.Response;
                    r.ContentType = "image/png";
                    string defaultImg = context.Server.MapPath("~/Content/assets/images/logo-white-bg@2x.png");
                    //string instanceImg = string.Empty;
                    //instanceImg = context.Server.MapPath(string.Format("~/WF/UploadData/Instance/logo-white-bg@2x_{0}.png", 0));
                    ////if instance logo not exists
                    //if (!File.Exists(instanceImg))
                    //{
                    //    WriteLogo(instanceImg);
                    //}

                    //if (File.Exists(instanceImg))
                    //{
                    //    r.WriteFile(instanceImg);
                    //}
                    //else
                    //{
                        r.WriteFile(defaultImg);
                   // }
                }
                else if (QueryStringValues.Type.ToLower() == "logo")
                {
                    //user image display
                    HttpResponse r = context.Response;
                    r.ContentType = "image/png";
                    string defaultImg = context.Server.MapPath("~/Content/assets/images/logo@2x.png");
                    //string instanceImg = string.Empty;
                    //instanceImg = context.Server.MapPath(string.Format("~/WF/UploadData/Instance/logo@2x_{0}.png", 0));
                    ////if instance logo not exists
                    //if (!File.Exists(instanceImg))
                    //{
                    //    WriteLogo(instanceImg);
                    //}

                    //if (File.Exists(instanceImg))
                    //{
                    //    r.WriteFile(instanceImg);
                    //}
                    //else
                    //{
                        r.WriteFile(defaultImg);
                   // }
                }
                else if (QueryStringValues.Type.ToLower() == "contact")
                {
                    HttpResponse r = context.Response;
                    r.ContentType = "image/png";
                    string defaultImg = context.Server.MapPath("~/WF/UploadData/Users/ThumbNailsMedium/user_0.png");
                    string userImg = context.Server.MapPath(string.Format("~/WF/UploadData/Users/ThumbNailsMedium/contact_{0}.png", id));
                    if (File.Exists(userImg))
                        r.WriteFile(userImg);
                    else
                        r.WriteFile(defaultImg);
                    string img = string.Empty;
                }

            }
        }

        private static void WriteLogo(string instanceImg)
        {
            //IUserRepository<UserMgt.Entity.InstanceDetail> insRepository = new UserRepository<UserMgt.Entity.InstanceDetail>();
            //var instanceDetails = insRepository.GetAll().Where(o => o.InstanceID == sessionKeys.InstanceID).FirstOrDefault();
            //if (instanceDetails != null)
            //{
            //    if (instanceDetails.Image != null)
            //    {
            //        Byte[] bytes = (Byte[])instanceDetails.Image.ToArray();
            //        System.IO.File.WriteAllBytes(instanceImg, bytes);
            //    }
            //}
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