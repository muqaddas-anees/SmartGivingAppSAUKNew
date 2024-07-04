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


public partial class ProjectPlanFunding : System.Web.UI.Page
{
    ProjectPipelineClass ppclass = new ProjectPipelineClass();
    public string projectplanid;
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
                        SelectProjectFunding(Convert.ToInt32(HiddenField2.Value));
                    }

                }

            }
            else
            {
                lblError.Text = "Please Enter Project Plan Details";

            }
        }
    }
    // 0.[ProjectPlanID], 1.[ProjectTitle], 2.[ProjectDescription], 3.[StratergicObjective],4.[Justification],5.[BusinessBenefit],6.[CountryID]
    //7.[CityID],8.[SiteID],9.[Building],10.[Department],11.[BudgetaryCost],12.[CostCenter],13.[ExpectedStartDate],14.[ExpectedEndDate]
    //15.[FinacialStatusID],16.[StatusID]17.[ContributionID],18.[RiskScore],19.[FinancialScore],20.[BusinessScore],21.[RiskToBussiness]
    //22.[OwnerID],23.[Priority],24.[RAGstatus],25.[Assumptions],26.[Contraints],27.[Approvals],28.[Dependencies]29.[RelatedDocumentLinks]
    //30.[FundingRequired],31.[CriteriaForFunding],32.[AvailabilityExternalFunding],33.[GeneralBusinessRequirements],34.[ReportingRequirements]
    //35.[OperatingRequirements],36.[DocumentationAndTraining],37.[BusinessContinuity],38.[Testing]
    private void SelectProjectFunding(int ProjectPlanID)
    {
        List<object> list1 = ppclass.GetProjectProjectPlan(Convert.ToInt32(Request.QueryString["ProjectPlanID"].ToString()));
          if (list1.Count > 0)
          {
              txtFund.Text = list1[30].ToString(); 
              txtCriteria.Text = list1[31].ToString(); 
              txtAvailable.Text = list1[32].ToString();
          }
           
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (Request.QueryString.Count > 0)
        {
            if (HiddenField3.Value == "0")
            {
                if (chekvalidations() == true)
                {
                    InsertandUpdateFunding();
                    //InsertFunding();
                    Response.Redirect("~/WF/ProjectPlan/ProjectPlanningFunding.aspx?ProjectPlanID=" + HiddenField2.Value);
                }

            }
            else 
            {
                if (chekvalidations() == true)
                {
                    InsertandUpdateFunding();
                    //UpdateFunding();
                    Response.Redirect("~/WF/ProjectPlan/ProjectPlanningFunding.aspx?ProjectPlanID=" + HiddenField2.Value);
                }
            }
        }
        else
        {

            lblError.Text = "Please Enter Project Plan Details";
        }
       
       
    }
    private void InsertandUpdateFunding()
    {
        try
        {
            string[] PPFunding = new string[] { txtFund.Text.Trim(),txtCriteria.Text.Trim(),txtAvailable.Text.Trim() };
            string PPId = "";
            PPId = HiddenField2.Value.ToString();
            ppclass.PPFundingInsertandUpdate(PPFunding, PPId);
            
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "PPFunding - Funding(Insert and update)");
        }
        
    
    }
   
    protected void btnAttachments_Click(object sender, ImageClickEventArgs e)
    {
       // Response.Redirect("AssetAttachments.aspx?ProjectPlanID=" + projectplanid +"&PageNav="+"Funding");
    }
    protected void lnkattachments_Click(object sender, EventArgs e)
    {

        if (Request.QueryString.Count > 0)
        {

            
            string stemp2 = HiddenField2.Value;
            if (Convert.ToInt32(stemp2) > 0)
            {
                Response.Redirect("attachment.aspx?ProjectPlanID=" + HiddenField2.Value + "&PageNav=" + "Funding");
            }
            else
            {

                lblError.Text = "Please Enter Project Plan Details";
            }
        }
        else
        {

            lblError.Text = "Please Enter Project Plan Details";
        }

    }
    private bool chekvalidations()
    {
        bool Myval = true;
        string strTemp = "";
        int i = 1;
        lblError.Text = "";

        if (i == 1)
        {
            if (txtFund.Text == "")
            {
                strTemp += "Please enter data in Funding field <br />";
                Myval = false;
                //return false;
            }
            
            i = 2;
        }
        lblError.Text = strTemp;
        return Myval;

    }
}
