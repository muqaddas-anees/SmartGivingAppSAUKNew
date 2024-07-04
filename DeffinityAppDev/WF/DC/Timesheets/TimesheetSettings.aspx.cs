using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimesheetMgt.BAL;

namespace DeffinityAppDev.WF.DC.Timesheets
{
    public partial class TimesheetSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindUsers();

                SetApproverValue();
            }

            if (Request.QueryString["back"] != null)
            {
                if (Request.QueryString["pnl"] != null)
                    linkBack.NavigateUrl = Request.QueryString["back"] + "#" + Request.QueryString["pnl"].ToString();
                else
                    linkBack.NavigateUrl = Request.QueryString["back"];
                linkBack.Text = "<i class='fa fa-arrow-left'></i> Return to Settings";
                linkBack.Visible = true;
            }
        }

        private void SetApproverValue()
        {
            try
            {
                var e = TimesheetApproverBAL.TimesheetApproverBAL_Select(sessionKeys.PortfolioID);
                if (e != null)
                    ddlUsers.SelectedValue = e.ApproverID.ToString();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindUsers()
        {
            var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAdmins();
            ddlUsers.DataSource = ulist.OrderBy(o => o.ContractorName);
            ddlUsers.DataTextField = "ContractorName";
            ddlUsers.DataValueField = "ID";
            ddlUsers.DataBind();
            ddlUsers.Items.Insert(0, new ListItem("Please select..", "0"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlUsers.SelectedValue != "0")
                {
                    TimesheetApproverBAL.TimesheetApproverBAL_Update(Convert.ToInt32(ddlUsers.SelectedValue), sessionKeys.PortfolioID);
                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}