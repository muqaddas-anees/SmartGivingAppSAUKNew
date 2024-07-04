using DeffinityAppDev.App.controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AngleSharp;

namespace DeffinityAppDev.Controllers
{
    public class savecontrentController : Controller
    {
        // GET: savecontrent
        public ActionResult Index()
        {
            return View();
        }
        public string ModifiedData(string content,string orgid)
        {
            string retval = Server.HtmlEncode(content.Trim());

            var str_faitheducation = "&lt;img src=&quot;snippets/img/faitheducation.jpg&quot; width=&quot;&quot; height=&quot;&quot; style=&quot;display: inline-block;&quot;&gt;";
            var str_tithing = "&lt;img src=&quot;snippets/img/tithing.jpg&quot; width=&quot;&quot; height=&quot;&quot; style=&quot;display: inline-block;&quot;&gt;";
            var str_activites = "&lt;img src=&quot;snippets/img/activities.jpg&quot; width=&quot;&quot; height=&quot;&quot; style=&quot;display: inline-block;&quot;&gt;";
            var str_faithgiving = "&lt;img src=&quot;snippets/img/faithgiving.jpg&quot; width=&quot;&quot; height=&quot;&quot; style=&quot;display: inline-block;&quot;&gt;";
            var str_onlineshop = "&lt;img src=&quot;snippets/img/onlineshop.jpg&quot; width=&quot;&quot; height=&quot;&quot; style=&quot;display: inline-block;&quot;&gt;";
            var str_quicklink = "&lt;img src=&quot;snippets/img/quicklinks.jpg&quot; width=&quot;&quot; height=&quot;&quot; style=&quot;display: inline-block;&quot;&gt;";
            var str_banner = "&lt;img src=&quot;snippets/img/banner1.jpg&quot;width=&quot;100%&quot;height=&quot;&quot;style=&quot;display:inline-block;width:100%&quot;&gt;";
            var str_signup = "&lt;img src=&quot;snippets/img/signup.jpg&quot; width=&quot;&quot; height=&quot;&quot; style=&quot;display: inline-block;&quot;&gt;";

            retval = retval.Replace(str_tithing, "[tithing]");
            retval = retval.Replace(str_activites, "[activites]");
            retval = retval.Replace(str_faitheducation, "[faitheducation]");
            //retval = retval.Replace(str_faithgiving, "[faithgiving]");
            retval = retval.Replace(str_faithgiving, "[faithgiving]");
            retval = retval.Replace(str_onlineshop, "[onlineshop]");
            retval = retval.Replace(str_quicklink, "[quicklinks]");
            retval = retval.Replace(str_banner, "[banner]");
            retval = retval.Replace(str_signup, "[signup]");
            retval = Server.HtmlDecode(retval);

            retval = retval.Replace("[tithing]", "<div id='tithing'></div>");
            retval = retval.Replace("[activites]", "<div id='activites'></div>");
            retval = retval.Replace("[faitheducation]", "<div id='faitheducation'></div>");
            retval = retval.Replace("[faithgiving]", "<div id='faithgiving'></div>");
            retval = retval.Replace("[onlineshop]", "<div id='onlineshop'></div>");
            retval = retval.Replace("[quicklinks]", "<div id='quicklinks'></div>");
            retval = retval.Replace("[signup]", "<div id='signup'></div>");
            // retval = retval.Replace("[banner]", "<div id='banner'></div>");

            retval = retval.Replace("[logo]", string.Format("<img class='mt-10 img-fluid' src='ImageHandler.ashx?id={0}&s={1}'>", orgid, ImageManager.file_section_portfolio));
            retval = retval.Replace("snippets/img/banner1.jpg", "Content/default_back_600.jpg");
           // retval = retval.Replace("snippets/img/banner1.jpg", string.Format("ImageHandler.ashx?id={0}&s={1}", orgid, ImageManager.file_section_banner));

            retval = retval.Replace("youtube-wrapper", "youtube-wrapper card mb-6");
            retval = retval.Replace("vimeo-wrapper", "vimeo-wrapper card mb-6");
            retval = retval.Replace("photo-panel", "photo-panel card mb-6");
            retval = retval.Replace("page-header", "photo-panel card mb-6");
            retval = retval.Replace("embed-responsive embed-responsive-16by9", "embed-responsive embed-responsive-16by9 card-body");
            retval = retval.Replace("embed-responsive embed-responsive-4by3", "embed-responsive-4by3 card-body");
            //retval = retval.Replace("img-responsive", "img-responsive img-fluid");

            return Server.HtmlDecode(retval);

        }

        [HttpPost]
        public JsonResult insertcontentdata(string varContent, string orgid)
        {
            try
            {
                

                if(Request.QueryString["type"] != null)
                {
                    orgid = "0";
                }
                //save data to data base
              
                string path = Server.MapPath("~/WF/UploadData/OrgTemplate");
                string strFIleName = "org_" + orgid + ".html";
                string strFIleNamenew = "org_new_" + orgid + ".html";


                var m = varContent;

                var newM = ModifiedData(varContent, orgid);


                try
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(m);
                    byte[] bytes_new = Encoding.UTF8.GetBytes(newM);
                    ImageManager.FileDBSave(bytes, bytes_new, orgid.ToString(), ImageManager.file_section_landing, ".html", "org_new_" + orgid, "text/html");
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }



                //if (!Directory.Exists(path))
                //{

                //    Directory.CreateDirectory(path);
                //}
                //else
                //{
                   

                //    if (m.Length > 0)
                //    {
                //        //StringBuilder sb = new StringBuilder();
                //        //sb.AppendLine("<html>");
                //        //sb.AppendLine("<head>");
                //        //sb.AppendLine("<title>Index of c:\\dir</title>");
                //        //sb.AppendLine("</head>");
                //        //sb.AppendLine("<body>");
                //        //sb.AppendLine("<ul>");

                //        //string[] filePaths = Directory.GetFiles(@"c:\dir");
                //        //for (int i = 0; i < filePaths.Length; ++i)
                //        //{
                //        //    string name = Path.GetFileName(filePaths[i]);

                //        //    sb.AppendLine(string.Format("<li><a href=\"{0}\">{1}</a></li>",
                //        //        HttpUtility.HtmlEncode(HttpUtility.UrlEncode(name)),
                //        //        HttpUtility.HtmlEncode(name)));
                //        //}

                //        //sb.AppendLine("</ul>");
                //        //sb.AppendLine("</body>");
                //        //sb.AppendLine("</html>");
                //        //string result = sb.ToString();

                //        //string FullPath = path + "\\" + strFIleName;
                //        //if (System.IO.File.Exists(FullPath))
                //        //{
                //        //    System.IO.File.Delete(FullPath);

                //        //}
                //        //using (StreamWriter sw = new StreamWriter(FullPath, false))
                //        //{
                //        //    sw.WriteLine(m);
                //        //}


                //        //string FullPathNew = path + "\\" + strFIleNamenew;
                //        //if (System.IO.File.Exists(FullPathNew))
                //        //{
                //        //    System.IO.File.Delete(FullPathNew);

                //        //}
                //        //using (StreamWriter sw1 = new StreamWriter(FullPathNew, false))
                //        //{
                //        //    sw1.WriteLine(newM);
                //        //}
                //    }
                //}
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        
            return Json("");
        }




        [HttpPost]
        public ActionResult viewcontentdata(string userID)
        {
            string filepath = Server.MapPath("~/WF/UploadData/OrgTemplate");
            string strFIleName = "org_" + sessionKeys.PortfolioID + ".html";

            string strfullpath = filepath + strFIleName;
            var fileread = System.IO.File.ReadAllText(strfullpath, Encoding.UTF8);

            return Json(fileread, JsonRequestBehavior.AllowGet);
            //return View(fileread);
        }
    }
}