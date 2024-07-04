using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;
using System.Globalization;
using System.Web.UI.WebControls.WebParts;

public partial class CCApproval : System.Web.UI.Page
{

    #region Sql Query Variables

    string _sqlChangeControl = string.Empty;
    string _sqlUpdateChangeControl = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlQueries();
        if (!IsPostBack)
        {
            BindChangeControlGrid(_sqlChangeControl);
           // Master.PageHead = "Change Control";
        }
    }

    private void SqlQueries()
    {
        _sqlChangeControl = "SELECT cc.Id,cc.Title, ChangeDescription, Justification," +
            "convert(varchar(10),DateRaised,103) DateRaised,convert(varchar(10),TargetReleaseDate,103) TargetReleaseDate,ResourceImpact, RequesterName,RequesterEmailID from changecontrol cc inner join changecontrol_approval cca on cca.changecontrolid=cc.id" +
            " where cca.approvalid=" + sessionKeys.UID + " ORDER BY RequesterName";
        _sqlUpdateChangeControl = "UPDATE ChangeControl_Approval SET Approved={0},DateApproved='{1}' WHERE ChangeControlID={2} AND ApprovalID={3}";
    }

    private void BindChangeControlGrid(string sqlStatement)
    {
        DataTable table = DataHelperClass.GenericSelectMethodHelp(_sqlChangeControl);
        gridChanges.DataSource = table;
        gridChanges.DataBind();
    }

    protected void gridChanges_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int changeControlID = 0;
        if(e.CommandArgument!=null)
            changeControlID = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName.ToLower())
        { 
            case "approve":
                approveOrDeny(true, changeControlID);
                break;
            case "deny":
                approveOrDeny(false, changeControlID);
                break;
        }
    }

    private void approveOrDeny(bool hasApproved,int changeControlID)
    {
        DataHelperClass.GenericInsertHelper(string.Format(_sqlUpdateChangeControl, Convert.ToInt32(hasApproved), DateTime.Now.ToUniversalTime().ToString("s", DateTimeFormatInfo.InvariantInfo) + "Z"
            , changeControlID, sessionKeys.UID));
    }
}
