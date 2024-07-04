using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF
{
    public partial class PageBuilder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(QueryStringValues.Type.Length >0)
            {
                myIframe.Src = "~/examples/WebForm1.aspx?type=1&dt=" + DateTime.Now.Ticks;
            }
            else
            {
                myIframe.Src = "~/examples/WebForm1.aspx?dt=" + DateTime.Now.Ticks;
            }



            string contents1 = string.Empty;
            string contents2 = string.Empty;
            string FILENAME = string.Empty;
            IPortfolioRepository<PortfolioMgt.Entity.FileData> frep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

            var f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == ImageManager.file_section_landing).FirstOrDefault();
            if (f == null)
            {
                string path1 = Server.MapPath("~/WF/UploadData/OrgTemplate/"+"org_" + "0" + ".html");
                string path2 = Server.MapPath("~/WF/UploadData/OrgTemplate/" + "org_new_" + "0" + ".html");
              

                FILENAME = path1;

                using (StreamReader objstreamreader = File.OpenText(FILENAME))
                {
                    contents1 = objstreamreader.ReadToEnd();
                }

                FILENAME = path2;

                using (StreamReader objstreamreader = File.OpenText(FILENAME))
                {
                    contents2 = objstreamreader.ReadToEnd();
                }

                byte[] bytes = Encoding.UTF8.GetBytes(contents1);
                byte[] bytes_new = Encoding.UTF8.GetBytes(contents2);
                ImageManager.FileDBSave(bytes, bytes_new, "0", ImageManager.file_section_landing, ".html", "org_new_" + "0", "text/html");
            }

        }
    }
}