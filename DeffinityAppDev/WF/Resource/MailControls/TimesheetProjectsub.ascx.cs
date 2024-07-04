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
using Microsoft.ApplicationBlocks.Data;

public partial class MailControls_TimesheetProjectsub : System.Web.UI.UserControl
{
    int GetDATA = 0;
    int TUserID = 0;
    DateTime DateEnter;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (sessionKeys.Project > 0)
        {
            litprojectname.Text = "in Project:" + sessionKeys.Prefix + sessionKeys.Project;
        }
        else
            litprojectname.Text = "";
        
    }
    public void ProjectOwner_BindData(int UserID, int Projectreference, int ApproverId, int WCDateID, string text, string Date)
    {

        lblUserName.Text = sessionKeys.UName;
        GetDATA = ApproverId;
        TUserID = UserID;
        string GetResourename = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, string.Format("select ContractorName  from Contractors where ID={0}", ApproverId)).ToString();
        resourecName.Text = GetResourename;
       // litprojectname.Text = text;
        Gridview1.DataSource = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimesheetMailsendtoProjectOwner", new SqlParameter("@ContractorID", UserID), new SqlParameter("@ProjectReference", Projectreference), new SqlParameter("@WCDateID", WCDateID)).Tables[0];
        Gridview1.DataBind();
        DateTime dt = Convert.ToDateTime(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "SELECT dbo.udf_PreviousThursday('" + Date + "')"));
        lblWCDate.Text = dt.ToShortDateString(); //Convert.ToDateTime(dt);  
        DateEnter = dt;

        ByProjectOwnerbindlinks(Projectreference);
    }

    private void ByProjectOwnerbindlinks(int ProjectReference)
    {
        lblUserName.Text = sessionKeys.UName;
        int GetID = 0;

        GetID = Convert.ToInt32(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "DN_selectWCDate", new SqlParameter("@ContractorID", TUserID), new SqlParameter("@ApproverID", GetDATA), new SqlParameter("@WCDate", DateEnter)));


        string yes = "Y";
        string NO = "N";
        linkWebsite.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        linkWebsiteFooter.Text = Deffinity.systemdefaults.GetWebSiteName();
        linkWebsiteFooter.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        imgLogo.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];
        //Hyp_AcceptTimeSheet.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + "/" + "TimeSheetStatus.aspx?WCD=" + GetID + "&Status=" + yes + "&UID=" + TUserID + "&Pref=" + ProjectReference;
        //Hyp_DeclineTimesheet.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + "/" + "TimeSheetStatus.aspx?WCD=" + GetID + "&Status=" + NO + "&UID=" + TUserID + "&Pref=" + ProjectReference;

    }
    public string ChangeHoues(string GetHours)
    {
        string GetActivity = "";
        char[] comm1 = { '.' };
        string[] displayTime = GetHours.Split(comm1);


        GetActivity = displayTime[0] + ":" + displayTime[1];
        
        return GetActivity;
    }
}
