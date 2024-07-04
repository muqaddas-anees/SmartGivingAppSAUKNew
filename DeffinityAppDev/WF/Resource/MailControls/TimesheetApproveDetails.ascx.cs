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



public partial class MailControls_TimesheetApproveDetails : System.Web.UI.UserControl
{
    int GetDATA = 0;
    int TUserID = 0;
    DateTime  DateEnter;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (sessionKeys.Project > 0)
        {
            litprojectname.Text = "in Project:" + sessionKeys.Prefix + sessionKeys.Project;
        }
        else
            litprojectname.Text = "";
        
    }
    public void BindData(int UserID, string EntryIds, int ApproverId, int Status, string text)
    {
        GetDATA = ApproverId;
        TUserID = UserID;
        litapprovereject.Text = text;
        Gridview1.DataSource = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_TimeSheetApproveSendMail", new SqlParameter("@ApproverID", ApproverId), new SqlParameter("@EntryIds", EntryIds), new SqlParameter("@Status", Status)).Tables[0];
        Gridview1.DataBind();
        bindlinks();
    }
    public void BindData(int UserID, string EntryIds, int ApproverId, int Status, string text, string Date)
    {
        GetDATA = ApproverId;
        TUserID = UserID;
        litapprovereject.Text = text;
        string GetResourename = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, string.Format("select ContractorName  from Contractors where ID={0}", ApproverId)).ToString();
        resourecName.Text = GetResourename;

         Gridview1.DataSource = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_TimeSheetApproveSendMail", new SqlParameter("@ApproverID", ApproverId), new SqlParameter("@EntryIds", EntryIds), new SqlParameter("@Status", Status)).Tables[0];
        Gridview1.DataBind();
        DateTime dt =Convert.ToDateTime(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "SELECT dbo.udf_PreviousThursday('" + Date + "')"));
        lblWCDate.Text = dt.ToShortDateString(); //Convert.ToDateTime(dt);  
        DateEnter = dt;
       
        bindlinks();
    }

    public void BindData(int WCDateID, int UserID, string Date, int ApproverId)
    {
        DateTime dt = Convert.ToDateTime(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "SELECT dbo.udf_PreviousThursday('" + Date + "')"));
        lblWCDate.Text =  dt.ToShortDateString(); //Convert.ToDateTime(dt); 
        string GetResourename = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, string.Format("select ContractorName  from Contractors where ID={0}", ApproverId)).ToString();
        resourecName.Text = GetResourename;
        Gridview1.DataSource = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_TimesheetEntryByWCID", new SqlParameter("@WCID", WCDateID)).Tables[0];
        Gridview1.DataBind();
        int ProjectReference = 0;

        linkWebsite.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        linkWebsiteFooter.Text = Deffinity.systemdefaults.GetWebSiteName();
        linkWebsiteFooter.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        imgLogo.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];
        //Hyp_AcceptTimeSheet.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + "/" + "TimeSheetStatus.aspx?WCD=" + WCDateID.ToString() + "&Status=Y&UID=" + UserID.ToString() + "&Pref=" + ProjectReference;
        //Hyp_DeclineTimesheet.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + "/" + "TimeSheetStatus.aspx?WCD=" + WCDateID.ToString() + "&Status=n&UID=" + UserID.ToString() + "&Pref=" + ProjectReference;
    }
    //DEFFINITY_TimesheetEntryByWCID
    private void bindlinks()
    {
        int GetID = 0;

        GetID = Convert.ToInt32(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "DN_selectWCDate", new SqlParameter("@ContractorID", TUserID), new SqlParameter("@ApproverID", GetDATA), new SqlParameter("@WCDate", DateEnter)));


        string yes ="Y";
        string NO ="N";
        int ProjectReference = 0;
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
