using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Deffinity.IssuesManager;

public partial class MailControls_ProjectIssue : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void setdata(int IssueID)
    {

        BindData(IssueID, QueryStringValues.Project,string.Empty);
    }
    public void setdata(int IssueID,int ProjectReference,string ReciverName)
    {
        BindData(IssueID, ProjectReference,ReciverName);
    }
    
    private void BindData(int IssueID, int ProjectReference,string ReciverName)
    {

        BindIssueMail(IssueID, ProjectReference, ReciverName,false);
            

        
    }

    private void BindIssueMail(int IssueID, int ProjectReference, string ReciverName,bool isCustomer)
    {
        linkWebsite.Text = Deffinity.systemdefaults.GetWebSiteName();
        linkWebsite.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        linkWebsiteFooter.Text = Deffinity.systemdefaults.GetWebSiteName();
        linkWebsiteFooter.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        imgLogo.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];

        if (isCustomer)
        { lblProjectReference.InnerHtml = "";
        pnlHideCustomer1.Visible = false;
        pnlHideCustomer2.Visible = false;
        pnlHideCustomer3.Visible = false;
        pnlHideCustomer4.Visible = false;
        
        }
        else
        { lblProjectReference.InnerHtml = "With in Project <b>" + sessionKeys.Prefix + ProjectReference.ToString() + "</b> there is a Issue assigned to you."; }
       
        
        lblReciver.InnerText = ReciverName;

        SqlDataReader dr = IssuesManager.GetIssueDetails(IssueID);
        while (dr.Read())
        {
            lblIssue.InnerText = dr["Issue"].ToString();
            lblAssignTo.InnerText = dr["AssignToName"].ToString();
            lblExpectedOutcome.InnerText = dr["ExpectedOutcome"].ToString();
            lblIssueRaisedBy.InnerText = dr["RaisedByName"].ToString();
            //if(lblIssueType != null)
            //lblIssueType.InnerText = dr["IssueTypeName"].ToString();
            //if(lblStatus != null)
            //lblStatus.InnerText = dr["StatusName"].ToString();
            lblDateraised.InnerText = DateTime.Parse(string.Format("{0:d}", dr["ScheduledDate"].ToString())).ToShortDateString();
        }
        dr.Close();
        dr.Dispose();
    }
    public void BindData(int IssueID, int ProjectReference, string ReciverName,bool isCustomer)
    {
        BindIssueMail(IssueID, ProjectReference, ReciverName, true);
    }
}
