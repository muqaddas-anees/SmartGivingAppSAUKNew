using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using UserMgt.DAL;
using UserMgt.Entity;
using System.IO;

public partial class MailControls_IssueCustomerSignOffMail : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void CustomerSignOffMail(int issueId,string receiver,string signOffBy,string projectTitle,string issueTitle,string dateSignOff)
    {
        try
        {

            using (projectTaskDataContext pt = new projectTaskDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    int projectRef = QueryStringValues.Project;
                    var x = ud.Contractors.Select(c => c).ToList();
                    var y = pt.ProjectIssueComments.Where(p => p.ProjectRef == projectRef && p.IssueRef == issueId).Select(p => p).ToList();

                    var result = (from p in y
                                  join c in x on p.SubmitteBy equals c.ID
                                  where p.ProjectRef == projectRef && p.IssueRef == issueId
                                  select new { p.ID, p.ProjectRef, p.IssueRef, p.Comments, p.CommentDate, c.ContractorName }).ToList();
                    Gridview_Comments.DataSource = result;
                    Gridview_Comments.DataBind();

                    lblReciver.InnerText=receiver;
                    lblSignOffBy.InnerText = signOffBy;
                    lblProjectTitle.Text = projectTitle;
                    lblIssue.Text = issueTitle;
                    lblDateSignedOff.Text =  dateSignOff.ToString().Remove(16);

                       
                 
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        lnkRef.HRef = System.Configuration.ConfigurationManager.AppSettings["Weburl"];

        imgLogo.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];
    }
}
