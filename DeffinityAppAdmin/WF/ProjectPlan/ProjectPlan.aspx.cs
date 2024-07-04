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
using System.Web.SessionState;
using Deffinity.Bindings;
using Deffinity.ProgrammeManagers;
public partial class ProjectPlan : System.Web.UI.Page
{
    ProjectPipelineClass PPclass = new ProjectPipelineClass();
    DisBindings getdata = new DisBindings();
    public string projectplanid;
    
    protected void Page_Load(object sender, EventArgs e)
    {
       // Master.PageHead = "Project Proposal";
        if(!IsPostBack)
        {
            lblError.Text = "";
            try
            {
                CheckUserRole();
                dataBindings();
                if (Request.QueryString["ProjectPlanID"] != null)
                {

                    if ((Request.QueryString["ProjectPlanID"] == "") || (Request.QueryString["ProjectPlanID"] == "0"))
                    {
                        HiddenField2.Value = "0";
                    }
                    else
                    {
                        HiddenField2.Value = Request.QueryString["ProjectPlanID"].ToString();
                    }
                    if (Convert.ToInt32(HiddenField2.Value) > 0)
                    {
                        SelectProjectPlanDetails(Convert.ToInt32(HiddenField2.Value));
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message);
            }
        }
    }


    #region Default Bindings
    private void dataBindings()
    {
        getdata.DdlBindSelect(ddlOwner, "select ID,ContractorName from Contractors where (SID = 1 or SID = 2 or SID = 3) and Status = 'ACTIVE' order by ContractorName", "ID", "ContractorName", false, false,true);
        getdata.DdlBindSelect(ddlFstatus, "DN_SelectFinancialStatus", "FinancialStatusID", "Status", true, false, true);
        getdata.DdlBindSelect(ddlPlanStatus, "DN_SelectProjectPlanStatus", "StatusID", "Status", true, false, true);
        getdata.DdlBindSelect(ddlContribut, "DN_SelectContribution", "ContributionID", "Contribution", true, false, true); 
        DisplayRadioListDetails();
        BindPortfolio();
        BindProgramme();
        ddlContry.Items.Add(Constants.ddlDefaultBind(true));
        ddlCity.Items.Add(Constants.ddlDefaultBind(true));
        ddlSiti.Items.Add(Constants.ddlDefaultBind(true));
        ddlProgramme.Items.Add(Constants.ddlDefaultBind(true));
        ddlSubProgramme.Items.Add(Constants.ddlDefaultBind(true));

    }

    private void BindCountry()
    {        
        getdata.DdlBindSelect(ddlContry, string.Format("Select Distinct CountryID as ID,(select Country from Country where ID = AssignedSitesToPortfolio.CountryID) as Country from AssignedSitesToPortfolio where Portfolio ={0}", ddlPortfolio.SelectedValue), "ID", "Country", false, false,true);
    }
    private void BindCity()
    {
        getdata.DdlBindSelect(ddlCity, string.Format("Select Distinct CityID as ID,(select City from City where ID = AssignedSitesToPortfolio.CityID) as City from AssignedSitesToPortfolio where Portfolio ={0} And CountryID={1}", ddlPortfolio.SelectedValue, ddlContry.SelectedValue), "ID", "City", false, false, true);
    }
    private void BindSite()
    {
        getdata.DdlBindSelect(ddlSiti, string.Format("Select Distinct SiteID as ID,(select Site from Site where ID = AssignedSitesToPortfolio.SiteID) as Site from AssignedSitesToPortfolio where Portfolio ={0} And CityID={1}", ddlPortfolio.SelectedValue, ddlCity.SelectedValue), "ID", "Site", false, false, true);
    }
    private void BindProgramme()
    {
        getdata.DdlBindSelect(ddlProgramme, string.Format("SELECT ID, OperationsOwners FROM  OperationsOwners where  Level=1"), "ID", "OperationsOwners", false, false, true);
    }
    private void BindPortfolio()
    {
        getdata.DdlBindSelect(ddlPortfolio, "SELECT ID,PortFolio FROM ProjectPortfolio where visible='true'", "ID", "PortFolio", false, false, true);
    }
    private void BindSubProgramme()
    {
        ddlSubProgramme.DataSource= DefaultDatabind.GetProgrammes(int.Parse(ddlProgramme.SelectedValue));
        ddlSubProgramme.DataTextField = "OPERATIONSOWNERS";
        ddlSubProgramme.DataValueField = "ID";
        ddlSubProgramme.DataBind();              
    }
    #endregion
    #region dropdown SelectIndexChage events
    protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindProgramme();
        BindCountry();
    }
    protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubProgramme();
    }   
    protected void ddlContry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCity();
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSite();
    }

    #endregion

    // 0.[ProjectPlanID], 1.[ProjectTitle], 2.[ProjectDescription], 3.[StratergicObjective],4.[Justification],5.[BusinessBenefit],6.[CountryID]
    //7.[CityID],8.[SiteID],9.[Building],10.[Department],11.[BudgetaryCost],12.[CostCenter],13.[ExpectedStartDate],14.[ExpectedEndDate]
    //15.[FinacialStatusID],16.[StatusID]17.[ContributionID],18.[RiskScore],19.[FinancialScore],20.[BusinessScore],21.[RiskToBussiness]
    //22.[OwnerID],23.[Priority],24.[RAGstatus],25.[Assumptions],26.[Contraints],27.[Approvals],28.[Dependencies]29.[RelatedDocumentLinks]
    //30.[FundingRequired],31.[CriteriaForFunding],32.[AvailabilityExternalFunding],33.[GeneralBusinessRequirements],34.[ReportingRequirements]
    //35.[OperatingRequirements],36.[DocumentationAndTraining],37.[BusinessContinuity],38.[Testing],39.ProgrammeID,40.PortfolioID,41.SubProgrammeID
    private void SelectProjectPlanDetails(int projectplanid)
    {
        List<object> list1 = PPclass.GetProjectProjectPlan(Convert.ToInt32(Request.QueryString["ProjectPlanID"].ToString()));
        if (list1.Count > 0)
        {
            txtProjectTitle.Text = list1[1].ToString();
            txtDesc.Text = list1[2].ToString(); 
            txtStrater.Text = list1[3].ToString();
            txtJustifi.Text = list1[4].ToString();
            txtBnf.Text = list1[5].ToString();
            txtBuild.Text = list1[9].ToString();
            txtRisk.Text = list1[21].ToString();
            txtDepartment.Text = list1[10].ToString();
            txtBudget.Text = list1[11].ToString();
            txtCost.Text = list1[12].ToString();
            txtStartDate.Text = DateTime.Parse(list1[13].ToString()).ToShortDateString();
            txtFinishDate.Text = DateTime.Parse(list1[14].ToString()).ToShortDateString();
            ddlFstatus.SelectedValue = list1[15].ToString();
            ddlPlanStatus.SelectedValue = list1[16].ToString();
            ddlContribut.SelectedValue = list1[17].ToString(); 
            rdbtnlstRiskScore.SelectedValue = list1[18].ToString();
            rdbtnlstFinancialImpact.SelectedValue = list1[19].ToString();
            rdbtnlstBusinessImpact.SelectedValue = list1[20].ToString();
            ddlOwner.SelectedValue = list1[22].ToString();
            ddlPriority.SelectedValue = list1[23].ToString();
            ddlRagstatus.SelectedValue = list1[24].ToString();
            ddlPortfolio.SelectedValue = list1[40].ToString();
            //bind Portfolio
            BindCountry();
            ddlContry.SelectedValue = list1[6].ToString();
            BindCity();
            ddlCity.SelectedValue = list1[7].ToString();
            BindSite();
            ddlSiti.SelectedValue = list1[8].ToString();           
            //Bind Programme
            BindProgramme();
            ddlProgramme.SelectedValue = list1[39].ToString();
            BindSubProgramme();
            ddlSubProgramme.SelectedValue = list1[41].ToString();     
        
        }
        list1.Clear();       
        
    }
    //bind radio buttons
    public void DisplayRadioListDetails()
    {
        for (int i=0;i<5;i++)
        {
            int value;
            value = i + 1;
            rdbtnlstRiskScore.Items.Insert(i, value.ToString());
            rdbtnlstRiskScore.Items[i].Value = value.ToString();
            rdbtnlstBusinessImpact.Items.Insert(i, value.ToString());
            rdbtnlstBusinessImpact.Items[i].Value = value.ToString();
            rdbtnlstFinancialImpact.Items.Insert(i, value.ToString());
            rdbtnlstFinancialImpact.Items[i].Value = value.ToString();           
        }
    }
    private string RetVal(int i)
    {
        string strtemp = "";
        if (i == 0)
        { strtemp = "Low"; }
        else if (i == 4)
        { strtemp = "High"; }
        else
        { strtemp = ""; }
        return strtemp;
    }
    
   
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        
        try
        {
            if (HiddenField2.Value == "0")
            {
                InsertProjectPlan();               
            }
            else 
            {
                UpdateProjectPlan();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message); 
        }
       
    }
       
    private void InsertProjectPlan()
    {

        try
        {
            string[] ProjectPiepline = new string[] { txtProjectTitle.Text.Trim(), txtDesc.Text.Trim(), txtStrater.Text.Trim(), txtJustifi.Text.Trim(), txtBnf.Text.Trim(), ddlContry.SelectedValue.ToString(), ddlCity.SelectedValue.ToString(), ddlSiti.SelectedValue.ToString(), txtBuild.Text.Trim(), txtDepartment.Text.Trim(), ddlOwner.SelectedValue.ToString(), txtBudget.Text.Trim(), txtCost.Text.Trim(), txtStartDate.Text.Trim(), txtFinishDate.Text.Trim(), ddlFstatus.SelectedValue.ToString(), ddlPlanStatus.SelectedValue.ToString(), ddlContribut.SelectedValue.ToString(), rdbtnlstRiskScore.SelectedValue.ToString(), rdbtnlstFinancialImpact.SelectedValue.ToString(), rdbtnlstBusinessImpact.SelectedValue.ToString(), txtRisk.Text.Trim(), ddlRagstatus.SelectedValue.ToString(), ddlPriority.SelectedValue.ToString() };
            string[] ProjectPieplineNew = new string[] { ddlProgramme.SelectedValue, ddlPortfolio.SelectedValue, ddlSubProgramme.SelectedValue };
            int ppid;
            int getresult = PPclass.InsertPipeProjectPlan(ProjectPiepline,ProjectPieplineNew, out ppid);

            if (getresult == 1)
            {
                lblExistError.Text = "Please check Project title Already Exists";
            }
            else
            {
                Response.Redirect(string.Format("ProjectPlan.aspx?ProjectPlanID={0}", ppid), false);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "insert project plan");
        }
    }
    private void UpdateProjectPlan()
    {
            try
            {
                string[] ProjectPiepline = new string[] { txtProjectTitle.Text.Trim(), txtDesc.Text.Trim(), txtStrater.Text.Trim(), txtJustifi.Text.Trim(), txtBnf.Text.Trim(), ddlContry.SelectedValue.ToString(), ddlCity.SelectedValue.ToString(), ddlSiti.SelectedValue.ToString(), txtBuild.Text.Trim(), txtDepartment.Text.Trim(), ddlOwner.SelectedValue.ToString(), txtBudget.Text.Trim(), txtCost.Text.Trim(), txtStartDate.Text.Trim(), txtFinishDate.Text.Trim(), ddlFstatus.SelectedValue.ToString(), ddlPlanStatus.SelectedValue.ToString(), ddlContribut.SelectedValue.ToString(), rdbtnlstRiskScore.SelectedValue.ToString(), rdbtnlstFinancialImpact.SelectedValue.ToString(), rdbtnlstBusinessImpact.SelectedValue.ToString(), txtRisk.Text.Trim(), ddlRagstatus.SelectedValue.ToString(), ddlPriority.SelectedValue.ToString()};
                string[] ProjectPieplineNew = new string[] { ddlProgramme.SelectedValue, ddlPortfolio.SelectedValue, ddlSubProgramme.SelectedValue };
                PPclass.UpdatePipeProjectPlan(ProjectPiepline,ProjectPieplineNew, HiddenField2.Value);
                Response.Redirect(string.Format("ProjectPlan.aspx?ProjectPlanID={0}", HiddenField2.Value));
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message,"Update project praposal");
            }         

    }
   
    #region Navigations

    protected void btnPortfolio_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/WF/CustomerAdmin/Portfolio.aspx?tab=1&type=projectplan&projectplanid={0}", QueryStringValues.ProjectPlanID.ToString()));
    }
    protected void btnProgramme_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/WF/CustomerAdmin/WF/Admin/ProgrammeManagement.aspx?type=projectplan&projectplanid={0}", QueryStringValues.ProjectPlanID.ToString()));
    }
    protected void btnSubprogramme_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/WF/Admin/ProgrammeManagement.aspx?type=projectplan&projectplanid={0}", QueryStringValues.ProjectPlanID.ToString()));
    }
    protected void btnCountry_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/WF/CustomerAdmin/Portfolio.aspx?tab=1&type=projectplan&projectplanid={0}", QueryStringValues.ProjectPlanID.ToString()));
    }
    protected void btnCity_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/WF/CustomerAdmin/Portfolio.aspx?tab=2&type=projectplan&projectplanid={0}", QueryStringValues.ProjectPlanID.ToString()));
    }
    protected void btnSite_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/WF/CustomerAdmin/Portfolio.aspx?tab=1&type=projectplan&projectplanid={0}", QueryStringValues.ProjectPlanID.ToString()));
    }
    #endregion
    #region "Permission Code Here"
    protected bool CommandField()
    {
        bool vis = true;
        try
        {
            if ((Request.QueryString["Project"] != null))
            {
                if (sessionKeys.SID != 1)
                {
                    int role = 0;
                    role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                    if (role == 3)
                    {

                        vis = false;
                        //  Disable();

                    }
                    role = Admin.GetTeamID(sessionKeys.UID);
                    if (role == 3)
                    {
                        vis = false;

                        // Disable();

                    }

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;

    }
    private void CheckUserRole()
    {
        if ((Request.QueryString["Project"] != null))
        {
            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {
                   // Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";
                    Disable();

                }
                role = Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {
                   // Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";
                    Disable();

                }

            }
        }
    }
    private void Disable()
    {
        btnCity.Enabled = false;
        btnCountry.Enabled = false;
        btnPortfolio.Enabled = false;
        btnProgramme.Enabled = false;
        btnSite.Enabled = false;
        btnsubmit.Enabled = false;
        btnSubprogramme.Enabled = false;
    }
    #endregion 
}
