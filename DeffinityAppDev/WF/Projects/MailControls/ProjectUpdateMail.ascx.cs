using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Deffinity.IssuesManager;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using Microsoft.ApplicationBlocks.Data;
using Deffinity.ProjectMeetingEntitys;
using Deffinity.ProjectMeetingManager;
public partial class MailControls_ProjectUpdateMail : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void setdata(int IssueID)
    {

        BindData(IssueID, QueryStringValues.Project, string.Empty);
    }
    public void setdata(int meetingID, int ProjectReference, string ReciverName)
    {
        BindData(meetingID, ProjectReference, ReciverName);
    }

    private void BindData(int meetingID, int ProjectReference, string ReciverName)
    {
        projectTaskDataContext projectDB = new projectTaskDataContext();
        var projectName = (from r in projectDB.ProjectDetails
                           where r.ProjectReference == ProjectReference
                           select r).ToList().FirstOrDefault();

        linkWebsite.Text = Deffinity.systemdefaults.GetWebSiteName();
        linkWebsite.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        linkWebsiteFooter.Text = Deffinity.systemdefaults.GetWebSiteName();
        linkWebsiteFooter.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        imgLogo.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];
        if (projectName != null)
        {
            lblProjectReference.InnerText = sessionKeys.Prefix + ProjectReference.ToString() + ":" + projectName.ProjectTitle + " .";
        }

        lblReciver.InnerText = ReciverName;
        //DataTable dt = new DataTable();
        //dt=SqlHelper.ExecuteDataset(Constants.DBString,CommandType.StoredProcedure,
        //    "Deffinity_ProjectissueSelect", new SqlParameter("@IssueID", IssueID)).Tables[0];
       // SqlDataReader dr = IssuesManager.GetIssueDetails(int meetingID);
        ProjectMeetingEntity ProjectMeeting = new ProjectMeetingEntity();
        ProjectMeeting = ProjectMeetingManager.ProjectMeetingSelect(meetingID);

        //DataTable dt = new DataTable();
        //while (dr.Read())
        //{
         lblIssue.InnerText = ProjectMeeting.GeneralNotes;// dr["Issue"].ToString();
            // lblAssignTo.InnerText = dr["AssignToName"].ToString();
            lblExpectedOutcome.InnerText =ProjectMeeting.LessonsLearnt; //dr["ExpectedOutcome"].ToString();
           // lblIssueRaisedBy.InnerText = //dr["RaisedByName"].ToString();
            lblIssueType.InnerText = ProjectMeeting.MeetingTime;//dr["IssuseTypeName"].ToString();
            lblStatus.InnerText = RAG(ProjectMeeting.RAGStatus);
            lblAchivement.InnerText = ProjectMeeting.KeyAchievements;
            lblThisWeek.InnerText = ProjectMeeting.KeyTasks;
            lblDateraised.InnerText = DateTime.Parse(string.Format("{0:d}", ProjectMeeting.MeetingDate)).ToShortDateString();
            ltlDisplay.InnerHtml = GetImage(ProjectMeeting.RAGStatus.ToString());




    }
    private string GetImage(string status)
    {
        string url = "";
        if (status == "1")
            url = "<img src='" + Deffinity.systemdefaults.GetWebUrl() + "/WF/media/indcate_green.png' />"; //"~/media/indcate_green.png";
        if (status == "2")
            url = "<img  src='" + Deffinity.systemdefaults.GetWebUrl() + "/WF/media/indcate_red.png' />";// "~/media/indcate_red.png";
        if (status == "3")
            url = "<img  src='" + Deffinity.systemdefaults.GetWebUrl() + "/WF/media/indcate_amber.gif' />"; //"~/media/indcate_amber.gif";
        return url;
    }
    private string RAG(int Status)
    {
        string val="";
        if (Status == 1)
            val = "GREEN";
        if (Status == 3)
            val = "AMBER";
        if (Status == 2)
            val = "RED";
        return val;
    }
}
