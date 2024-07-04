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

public partial class ProjectPlanningBizReq : System.Web.UI.Page
{
    public string projectplanid;
    ProjectPipelineClass ppclass = new ProjectPipelineClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Project Proposal";
        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["ProjectPlanID"] != null)
                {
                    HiddenField2.Value = Request.QueryString["ProjectPlanID"].ToString();
                    if (Convert.ToInt32(HiddenField2.Value) > 0)
                    {
                        SelectProjectBiz(Convert.ToInt32(HiddenField2.Value));
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
    protected void SelectProjectBiz(int projectplanid)
    {

        List<object> list1 = ppclass.GetProjectProjectPlan(Convert.ToInt32(Request.QueryString["ProjectPlanID"].ToString()));
         if (list1.Count > 0)
         {
             txtBizReq.Text = list1[33].ToString(); 
             txtRptReq.Text = list1[34].ToString(); 
             txtOprReq.Text = list1[35].ToString(); 
             txtDocReq.Text = list1[36].ToString(); 
             txtBizPlan.Text = list1[37].ToString();
             txtTestReq.Text = list1[38].ToString();
         }
    
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 0)
        {
            if (HiddenField3.Value == "0")
            {
                if (chekvalidations() == true)
                {
                    InsertandUpdateBizReq();
                    Response.Redirect("~/WF/ProjectPlan/ProjectPlanningBizReq.aspx?ProjectPlanID=" + HiddenField2.Value);
                }

            }
            else 
            {
                if (chekvalidations() == true)
                {
                    InsertandUpdateBizReq();
                    Response.Redirect("~/WF/ProjectPlan/ProjectPlanningBizReq.aspx?ProjectPlanID=" + HiddenField2.Value);
                }
            }
        }
        else
        {

            lblError.Text = "Please Enter Project Plan Details";
        }
       
    }
    private void InsertandUpdateBizReq()
    {
        try
        {
            string PPId="";
            PPId= HiddenField2.Value.ToString();
            string[] Bizreq = new string[] {txtBizReq.Text.Trim(),txtRptReq.Text.Trim(),txtOprReq.Text.Trim(),txtDocReq.Text.Trim(),txtBizPlan.Text.Trim(),txtTestReq.Text.Trim()};
            ppclass.BizReqInserandUpdate(Bizreq, PPId);
           
        }

        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "PPBizReq - Insert function");
        }
        
    }
    
    protected void btnAttachments_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("AssetAttachments.aspx?ProjectPlanID=" + projectplanid + "&PageNav=" + "BizReq");
    }
    
    protected void lnkAttachments_Click(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 0)
        {
            
            string stemp2 = HiddenField2.Value;
            if (Convert.ToInt32(stemp2) > 0)
            {
                Response.Redirect("attachment.aspx?&ProjectPlanID=" + HiddenField2.Value + "&PageNav=" + "BizReq");
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
            if (txtBizReq.Text == "")
            {
                strTemp += "Please enter data in General Business Requirements field <br />";
                Myval = false;
                //return false;
            }
            if (txtRptReq.Text == "")
            {
                strTemp += "Please enter data in Reporting Requirements field <br />";
                Myval = false;
                //return false;
            }

            i = 2;
        }
        lblError.Text = strTemp;
        return Myval;

    }

}
