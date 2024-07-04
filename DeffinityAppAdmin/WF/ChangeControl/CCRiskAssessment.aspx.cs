using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Incidents.Entity;
using Deffinity.EmailService;
using System.Collections;
using Incidents.DAL;
using Incidents.StateManager;

public partial class CCRiskAssessment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Change Control";
        if (sessionKeys.ChangeControlID == 0)
        {
            lblPageTitle.InnerText = "Change Control Risks-  Log New Change Control Request";

        }
        else
        {
            lblPageTitle.InnerText = "Change Control Risks - Reference " + sessionKeys.ChangeControlID.ToString();

        }
    }

    private void InsertRisks()
    {
        try
        {
            if (sessionKeys.ChangeControlID == 0)
            {
                lblRiskMessage.Text = "Needs to add the Change Control, before you assign the Risk.";
            }
            else
            {
                Risk risk = new Risk();
                risk.AssignedTo = Convert.ToInt16(ddlAssignedTo.SelectedValue);
                risk.ChangeControlID = sessionKeys.ChangeControlID;
                risk.RiskDescription = txtRisk.Text.Trim();
                risk.RollBackPlan = chkRoleBackPlan.Checked;
                risk.TestPlan = chkTestPlan.Checked;
                RiskHelper.Insert(risk);
                GridView2.DataBind();
                ClearRisk();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void ClearRisk()
    {
        txtRisk.Text = string.Empty;
        chkRoleBackPlan.Checked = false;
        chkTestPlan.Checked = false;
        ddlAssignedTo.ClearSelection();
        
    }

    protected void btnAddRisk_Click(object sender, EventArgs e)
    {
        InsertRisks();
    }
}
