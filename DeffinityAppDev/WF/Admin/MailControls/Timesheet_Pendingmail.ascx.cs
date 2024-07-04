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
public partial class MailControls_Timesheet_Pendingmail : System.Web.UI.UserControl
{
    int GetDATA = 0;
    int TUserID = 0;
    SqlConnection con = new SqlConnection(Constants.DBString);

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void TimeSheetApproval_pending_BindData(int WCDateID, int status, int contractorID, int TimesheetApproverID)
    {
        Getsubmitt.Visible = true;
       
        resourecName.Text = GetContractorName(contractorID).ToString();
        //lblapproval.Text = contractorName;
        lblweekCDate11.Text = TimeGetDate(WCDateID).ToString();
        //   litapprovereject.Text = text;
        //  Gridview1.DataSource = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimesheetApproveInfo_All", new SqlParameter("@WCDateID", WCDateID), new SqlParameter("@Status", status), new SqlParameter("@ContractorID", contractorID), new SqlParameter("@TimesheetApprover", TimesheetApproverID));
        // Gridview1.DataBind();
        bindlinks();
        Headingtwo.Visible = true;
       
      
        //linkWebsite.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        getlogin.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        linkWebsiteFooter.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        linkWebsiteFooter.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        imgLogo.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];

    }
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
}
