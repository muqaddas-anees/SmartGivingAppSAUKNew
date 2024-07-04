using PortfolioMgt.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class tpdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                GeneratePolicy.SendPolicyMail(Convert.ToInt32(txtaddress.Text));
                //PdfGenerator.PdfGenerator.HtmlToPdf("~/WF/UploadData/", "first", new string[] { @"http://localhost:55390/WF/DC/EmailTemplates/Policy.html" });
                //PdfGenerator.PdfGenerator.HtmlToPdf("~/WF/UploadData/", "first", new string[] { @"http://localhost:55390/WF/DC/EmailTemplates/Policy.html" }, new string[] { "--header-html " + "'http://localhost:55390/WF/DC/EmailTemplates/Header.html'", "--footer-html " + "'http://localhost:55390/WF/DC/EmailTemplates/footer.html'" });
                //PdfGenerator.PdfGenerator.HtmlToPdf("~/WF/UploadData/Policy", "first", new string[] { @"http://localhost:55390/WF/UploadData/Policy/62/Policy.html" }, new string[] { "--print-media-type" });         
            }
            catch(Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }
    }
}