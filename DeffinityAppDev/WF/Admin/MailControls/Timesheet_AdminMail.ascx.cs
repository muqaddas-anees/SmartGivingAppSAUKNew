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
public partial class MailControls_Timesheet_AdminMail : System.Web.UI.UserControl
{
    int GetDATA = 0;
    int TUserID = 0;
    SqlConnection con = new SqlConnection(Constants.DBString);

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void TimeSheetApproval_BindData(int WCDateID,string Notes, string ApprovalType, int status, int contractorID, string TimesheetID)
    {
       
     //   litapprovereject.Text = text;

        DataSet ds = new DataSet();
        ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimesheetApproveInfo", new SqlParameter("@WCDateID", WCDateID), new SqlParameter("@Status", status), new SqlParameter("@ContractorID", contractorID), new SqlParameter("@TimesheetID", TimesheetID));
        if (ds.Tables.Count > 0)
        {
            Getsubmitt.Visible = false;
            GetApprove.Visible = true;
            lblNotes.Visible = true;
            lblNotes.Text = "Notes :" + Notes;
            resourecName.Text = GetContractorName(contractorID).ToString();
            lblapproval.Text = ApprovalType;
            lblWCDate.Text = TimeGetDate(WCDateID).ToString();
            Heading.Visible = true;
            Headingtwo.Visible = false;
            //Gridview1.DataSource = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimesheetApproveInfo", new SqlParameter("@WCDateID", WCDateID), new SqlParameter("@Status", status), new SqlParameter("@ContractorID", contractorID), new SqlParameter("@TimesheetID", TimesheetID));
            Gridview1.DataSource = ds;
            Gridview1.DataBind();
            bindlinks();
            //linkWebsite.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
            linkWebsite.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
            linkWebsiteFooter.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
            linkWebsiteFooter.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
            imgLogo.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
            ImgBorder.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];
        }

    }

    //public void TimeSheetApproval_pending_BindData(int WCDateID, int status, int contractorID,int TimesheetApproverID)
    //{
    //    Getsubmitt.Visible = true;
    //    GetApprove.Visible = false;
    //    resourecName.Text = GetContractorName(contractorID).ToString();
    //    //lblapproval.Text = contractorName;
    //    lblweekCDate11.Text = TimeGetDate(WCDateID).ToString();
    //    //   litapprovereject.Text = text;
    //  //  Gridview1.DataSource = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimesheetApproveInfo_All", new SqlParameter("@WCDateID", WCDateID), new SqlParameter("@Status", status), new SqlParameter("@ContractorID", contractorID), new SqlParameter("@TimesheetApprover", TimesheetApproverID));
    //   // Gridview1.DataBind();
    //    bindlinks();
    //    Headingtwo.Visible = true;
    //    Heading.Visible = false;
    //    GetFooter.Visible = false;
    //    //linkWebsite.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
    //    getlogin.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
    //    linkWebsiteFooter.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
    //    linkWebsiteFooter.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
    //    imgLogo.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + "/MailLogo/deffinity_emailer_logo.png";
    //    ImgBorder.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + "/MailLogo/emailer_bg_bott.gif";

    //}

    private void bindlinks()
    {
      

      
        linkWebsite.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        linkWebsiteFooter.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        linkWebsiteFooter.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        imgLogo.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];
      
    }

    private string TimeGetDate(int WCDATE)
    {
        return SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select convert(Varchar(10),WCDate,103) as WCDate from TimesheetWCDate where WCDateID =@WCDateID", new SqlParameter("@WCDateID", WCDATE)).ToString();
 

    }

    private string GetContractorName(int contractorID)
    {
        return SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select  ContractorName from Contractors  where ID=@ID", new SqlParameter("@ID", contractorID)).ToString();
    }

    public string ChangeHoues(string GetHours)
    {
        string GetActivity = "";
        char[] comm1 = { '.' };
        string[] displayTime = GetHours.Split(comm1);


        GetActivity = displayTime[0] + ":" + displayTime[1];



        return GetActivity;
    }


    #region  Propeties
    int hours, id1;

    public int Id1
    {
        get { return id1; }
        set { id1 = value; }
    }

    public int Hours
    {
        get { return hours; }
        set { hours = value; }
    }

    //string ProjetTitle, ResourceName, Status, ApproverName, ResourceEmailAddress, DateRaise,  EntryType, Notes, ApproverEmailAddress;

    //int Hours_Time, Timesheet_ID;

    string projectName, resourcename, status, approvername, approveremail, dateraised, typeofhours, notes;

    public string Notes
    {
        get { return notes; }
        set { notes = value; }
    }

    public string Typeofhours
    {
        get { return typeofhours; }
        set { typeofhours = value; }
    }

    public string Dateraised
    {
        get { return dateraised; }
        set { dateraised = value; }
    }

    public string Approveremail
    {
        get { return approveremail; }
        set { approveremail = value; }
    }

    public string Approvername
    {
        get { return approvername; }
        set { approvername = value; }
    }

    public string Status
    {
        get { return status; }
        set { status = value; }
    }

    public string Resourcename
    {
        get { return resourcename; }
        set { resourcename = value; }
    }

    public string ProjectName
    {
        get { return projectName; }
        set { projectName = value; }
    }



    #endregion
}
