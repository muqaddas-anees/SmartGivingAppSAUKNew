using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ProjectPlanningScope : System.Web.UI.Page
{
    public string projectplanid;
    ProjectPipelineClass ppclass = new ProjectPipelineClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        //Master.PageHead = "Project Proposal";
        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["ProjectPlanID"] != null)
                {
                    if (Request.QueryString["ProjectPlanID"] == "")
                    {
                        HiddenField2.Value = "0";
                    }
                    else
                    {
                        HiddenField2.Value = Request.QueryString["ProjectPlanID"].ToString();
                    }

                    if (Convert.ToInt32(HiddenField2.Value) > 0)
                    {
                        SelectProjectScope(Convert.ToInt32(HiddenField2.Value));
                    }
                }
            }
            else
            {
                lblError.Text = "Please enter Project Plan Details";
            }
        }    
    }
    //to check list order
    // 0.[ProjectPlanID], 1.[ProjectTitle], 2.[ProjectDescription], 3.[StratergicObjective],4.[Justification],5.[BusinessBenefit],6.[CountryID]
    //7.[CityID],8.[SiteID],9.[Building],10.[Department],11.[BudgetaryCost],12.[CostCenter],13.[ExpectedStartDate],14.[ExpectedEndDate]
    //15.[FinacialStatusID],16.[StatusID]17.[ContributionID],18.[RiskScore],19.[FinancialScore],20.[BusinessScore],21.[RiskToBussiness]
    //22.[OwnerID],23.[Priority],24.[RAGstatus],25.[Assumptions],26.[Contraints],27.[Approvals],28.[Dependencies]29.[RelatedDocumentLinks]
    //30.[FundingRequired],31.[CriteriaForFunding],32.[AvailabilityExternalFunding],33.[GeneralBusinessRequirements],34.[ReportingRequirements]
    //35.[OperatingRequirements],36.[DocumentationAndTraining],37.[BusinessContinuity],38.[Testing]
    private void SelectProjectScope(int ProjectPlanID)
    {
        List<object> list1 = ppclass.GetProjectProjectPlan(Convert.ToInt32(Request.QueryString["ProjectPlanID"].ToString()));
        if (list1.Count > 0)
        {
            txtAssum.Text = list1[25].ToString();
            txtConstraint.Text = list1[26].ToString();
            txtApprove.Text = list1[27].ToString();
            txtDepend.Text = list1[28].ToString();
            txtRelat.Text = list1[29].ToString();
        }
        list1.Clear();

    }    

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 0)
        {

            if (HiddenField3.Value == "0")
            {
                InsertandUpdatePlanScope();
                Response.Redirect("~/WF/ProjectPlan/ProjectPlanningScope.aspx?ProjectPlanID=" + HiddenField2.Value);
            }
            else
            {

                InsertandUpdatePlanScope();
                Response.Redirect("~/WF/ProjectPlan/ProjectPlanningScope.aspx?ProjectPlanID=" + HiddenField2.Value);

            }
        }
        else
        {

            lblError.Text = "Please enter Project Plan Details";
        }


    }


    private void InsertandUpdatePlanScope()
    {
        try
        {

            string[] ppscope = new string[] { txtAssum.Text.Trim(), txtConstraint.Text.Trim(),txtApprove.Text.Trim(), txtDepend.Text.Trim(), txtRelat.Text.Trim() };
            string PPId="";
            PPId = HiddenField2.Value.ToString();
            ppclass.PlanScopeInsertandUpdate(ppscope, PPId);

            
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "PPPlanscope - Scope(Insert and Update)");
        }  
    }
   
    
    protected void lnkattachments_Click(object sender, EventArgs e)
    {
        
        string stemp2 = HiddenField2.Value;
        if (Request.QueryString.Count > 0)
        {
            if (Convert.ToInt32(stemp2) > 0)
            {
                Response.Redirect("attachment.aspx?ProjectPlanID=" + HiddenField2.Value + "&PageNav=" + "Scope");
            }
            else
            {
                lblError.Text = "Please enter Project Plan Details";
            }
        }
        else
        {
            lblError.Text = "Please enter Project Plan Details";
        }

    }
  
}
