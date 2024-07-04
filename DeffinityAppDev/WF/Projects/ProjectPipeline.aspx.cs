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
using System.Data.Common;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Linq;
using ProgrammeMgt.Entity;
using ProgrammeMgt.DAL;
using Deffinity.Bindings;

public partial class Projects_ProjectPipleline : BasePage
{
    public string Status;
    public string ProjectApproval;
    
    protected void Page_Load(object sender, EventArgs e)
    {
       // gviewclientprojectstatus.Columns[5].HeaderText = "Hello";//8-status
        //Response.Buffer = true;
        //Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        //Response.Expires = -1500;
        //Response.CacheControl = "no-cache";

       // Master.PageHead = Resources.DeffinityRes.ProjectPipeline;//"Project Pipeline";
        if (!Page.IsPostBack)
        {
            try
            {
                //dlt
                Project_qa_checkpoint_Visibility();
                // set default user
                Owner();
                SetOwner();
                //set prefix
                setProjectPrefix();
                //check list query string parmaeter
                if (Request.QueryString["list"] != null)
                {
                    if (Request.QueryString["list"] == "all")
                    {
                        sessionKeys.PortfolioID = 0;
                    }
                }
                BindProgramme();
              
                ObjDS_Site.SelectParameters["PortfolioID"].DefaultValue = sessionKeys.PortfolioID.ToString();
                //ObjDS_Programme.SelectParameters["PortfolioID"].DefaultValue = sessionKeys.PortfolioID.ToString();
                
                if (Request.QueryString["Status"] != null)
                {
                    int status1 = Convert.ToInt32(Request.QueryString["Status"].ToString());
                    //bind the portfolio dropdown
                    BindPortfolio();
                    BindCountry();
                    if (status1 == 8)
                    {
                        pnlProjectGrid.Visible = true;
                        //PanelPln.Visible = true;
                        PanelCpl.Visible = false;
                        //pnlsearch.Visible = false;
                        PanlePending.Visible = false;
                        //ddlbind_Contribution();
                        //lblProjectPlanMsg.Visible = false;
                       // GetProjectPlanData();
                    }
                    else if (status1 == 3)
                    {
                        pnlProjectGrid.Visible = false;
                       // PanelPln.Visible = false;
                        PanelCpl.Visible = true;
                        PanlePending.Visible = false;
                        //pnlsearch.Visible = true;
                        gridview1();

                    }
                    else
                    {
                        pnlProjectGrid.Visible = true;
                        //PanelPln.Visible = false;
                        PanelCpl.Visible = false;
                        PanlePending.Visible = true;
                        SetHeader();
                        Search();
                        //gviewclientprojectstatus1();
                    }
                    
                }
                else
                {
                    Response.Redirect("~/WF/Default.aspx", false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        string JavascriptBlock = @"
            <script type='text/javascript'>
            var checkedObject = null;
            function isChecked(what){
                if(checkedObject == null)
                {
                    checkedObject = what;
                    return true;
                }
                else{
                    if(what==checkedObject)
                    {
                        if(!what.checked)
                        {
                            checkedObject = null;
                            return true;
                        }
                    }
                    else{
                        alert('Only one project proposal can select!!!');
                        return false;
                    }
                }       
            }
            </script>
        ";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "JavascriptBlock", JavascriptBlock);

        //        string JavascriptBlock = @"
        //            <script type='text/javascript'>
        //            var checkedObject = null;
        //            function pro(what){
        //                if(checkedObject == null)
        //                {
        //                    checkedObject = what;
        //                    return true;
        //                }
        //                else{
        //                    if(what==checkedObject)
        //                    {
        //                        if(!what.checked)
        //                        {
        //                            checkedObject = null;
        //                            return true;
        //                        }
        //                    }
        //                    else{
        //                        alert('Only one project proposal can select!!!');
        //                        return false;
        //                    }
        //                }       
        //            }
        //            </script>
        //        ";
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "JavascriptBlock", JavascriptBlock);

    }
    #region DLT
    private void Project_qa_checkpoint_Visibility()
    {
        try
        {
            //Check Project class feature is enabled
            string[] str = PermissionManager.GetFeatures();
            if (!Page.IsPostBack)
            {
                //link_qa.Visible = Convert.ToBoolean(str[96]);
                //link_checkpoints.Visible = Convert.ToBoolean(str[97]);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion
    private void BindProgramme()
    {
        //var programmeLevel = (from r in operationOwners.OperationsOwners
        //                      where r.Level == 1
        //                      select new { r.ID, r.OperationsOwners }).ToList();
        //ddlprogram.DataSource = programmeLevel;
        //ddlprogram.DataTextField = "OperationsOwners";
        //ddlprogram.DataValueField = "ID";
        //ddlprogram.DataBind();
        //ddlprogram.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    //check the logged user is project owner or now if not set dropdown list to all
    private void SetOwner()
    {
        try {
            //ddlowner.DataBind();
            ddlowner.SelectedValue = sessionKeys.UID.ToString();
        }
        catch { ddlowner.SelectedIndex = 0; }
    }
    private void setProjectPrefix()
    {
        lblProjectPrefix.Value = sessionKeys.Prefix;
    }
    private void BindPortfolio()
    {
        //ddlPortfolio.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
        //ddlPortfolio.DataTextField = "PortFolio";
        //ddlPortfolio.DataValueField = "ID";
        ddlPortfolio.DataBind();
        if (ddlPortfolio.Items.Count > 0)
        {
            ddlPortfolio.SelectedValue = sessionKeys.PortfolioID.ToString();
        }
    }
    private void Owner() //new
    {
        DisBindings getdata = new DisBindings();
        DataTable dt = new DataTable();
        //dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select distinct ID,ContractorName from Contractors where  SID in (1,2,3) and SID <>99 and status ='Active' and  ID in (select OwnerID from projects where ProjectReference in (select distinct ID from dbo.fnGetProjectReferences(@UserID)))  union select ID,ContractorName from Contractors where  SID in (1,2,3) and SID <>99 and  status ='Active'and ID=@UserID  order by ContractorName",
        //    new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_AssignedTo_ProjectPlan", new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
        ddlowner.DataSource = dt;
        ddlowner.DataTextField = "ContractorName";
        ddlowner.DataValueField = "ID";
        ddlowner.DataBind();
        ddlowner.Items.Insert(0,new ListItem("Please select...","0"));
        //getdata.DdlBindSelect(ddlowner, "select ID,ContractorName from Contractors  where  SID in (1,2,3) and SID <>99 and  Status = 'ACTIVE'and ID in (select OwnerID from projects where ProjectReference in (select distinct ID from dbo.fnGetProjectReferences(" + sessionKeys.UID + "))) order by ContractorName", "ID", "ContractorName", false, false, true);
    }
    protected string LoadPriority(string p)
    {
        string retVal = @"~\images\icon_priority_low.gif";
        if (!string.IsNullOrEmpty(p))
        {
            retVal = @"~\images\icon_priority_" + p.ToLower().Trim() + ".gif";
        }
        return retVal;
    }
    protected string LoadOverBudget(double overBudget)
    {
        string retVal = "<span style='color:green;'> <i class='fa-circle'></i></span>";
        if (overBudget > 0)
        {
            retVal = "<span style='color:red;'><i class='fa-circle'></i></span>";
        }
        return retVal;

    }
    protected string LoadLate(int late)
    {
        string retVal = "<span style='color:green;'><i class='fa-circle'></i></span>";
        if (late > 1)
        {
            retVal = "<span style='color:red;'><i class='fa-circle'></i></span>";
        }
        return retVal;
    }
    //public void ddlbind_Contribution()
    //{
    //    SqlConnection cn = new SqlConnection(Constants.DBString);
    //    SqlDataAdapter adp_Contribution = new SqlDataAdapter("SELECT ContributionID,Contribution FROM Contribution", cn);
    //    DataSet ds_Contribution = new DataSet();
    //    adp_Contribution.Fill(ds_Contribution);
    //    ddlContribution.DataSource = ds_Contribution;
    //    ddlContribution.DataTextField = "Contribution";
    //    ddlContribution.DataValueField = "ContributionID";
    //    ddlContribution.DataBind();
    //    ListItem _selectTemplates = new ListItem("Select...", "0");
    //    ddlContribution.Items.Insert(0, _selectTemplates);
    //    ds_Contribution.Dispose();

    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //GetProjectPlanData();
    }
    //private void GetProjectPlanData()
    //{
    //    try
    //    {
    //        SqlConnection cn = new SqlConnection(Constants.DBString);
    //        SqlDataAdapter adp_search = new SqlDataAdapter("DN_ProjectPipelineSearch", cn);
    //        adp_search.SelectCommand.CommandType = CommandType.StoredProcedure;
    //        adp_search.SelectCommand.Parameters.Add("@ContributionID", SqlDbType.Int).Value = Convert.ToInt32(ddlContribution.SelectedItem.Value);
    //        adp_search.SelectCommand.Parameters.Add("@RiskScore", SqlDbType.Int).Value = Convert.ToInt32(ddlRisk.SelectedItem.Value);
    //        adp_search.SelectCommand.Parameters.Add("@FinancialScore", SqlDbType.Int).Value = Convert.ToInt32(ddlFinance.SelectedItem.Value);
    //        adp_search.SelectCommand.Parameters.Add("@BusinessScore", SqlDbType.Int).Value = Convert.ToInt32(ddlBiz.SelectedItem.Value);
    //        DataSet ds = new DataSet();
    //        adp_search.Fill(ds);
    //        GridView1.DataSource = ds;
    //        GridView1.DataBind();
    //        ds.Dispose();
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.LogException(ex.Message);
    //    }

    //}
    protected bool getVisible(string i)
    {
        bool val = false;
        try
        {
            //staus 3 is approved
            if (Convert.ToInt32(i) == 3)
            {
                val = true;
            }

        }
        catch (Exception ex) { }
        return val;
    }
    private void SetHeader()
    {
        string Pstauts = Request.QueryString["Status"].ToString();
        if (Pstauts == "0")
        {
            lblHead.InnerText = Resources.DeffinityRes.ListofAllProjects;// "List of All Projects ";
            ddlowner.SelectedIndex = 0;
        }
        else if (Pstauts == "2")
        {
            lblHead.InnerText = Resources.DeffinityRes.ListofLiveProjects;//"List of Live Projects ";
        }
        else if (Pstauts == "1")
        {
            lblHead.InnerText = Resources.DeffinityRes.ListofPendingProjects;//"List of Pending Projects ";
        }
        else if (Pstauts == "4")
        {
            lblHead.InnerText = Resources.DeffinityRes.ListofCancelledProjects;//" List of Cancelled Projects";
        }
        else if (Pstauts == "9")
        {
            lblHead.InnerText = Resources.DeffinityRes.CustomerOrders;//" Customer Orders";
        }
        else if (Pstauts == "5")
        {
            //lblHead.InnerText = Resources.DeffinityRes.ListofArchivedProjects;//" List of Archived Projects";
        }
        else
        {
            lblHead.InnerText = Resources.DeffinityRes.ListofOnHoldProjects; //" List of On Hold Projects";
        }
    }
    public void gviewclientprojectstatus1()
    {
       
        SqlConnection con = new SqlConnection(Constants.DBString);
        string cmd1 = "";
        SetHeader();
        string Pstauts = Request.QueryString["Status"].ToString();
        if (Convert.ToInt32(Pstauts) > 0)
        {
            cmd1 = "DN_ProjectPipeline";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(cmd1, con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Status", Convert.ToInt32(Pstauts));
            cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(sessionKeys.UID));
            cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
            cmd.Parameters.AddWithValue("@CountryID", Convert.ToInt32(ddlCountry.SelectedValue));
            SqlDataAdapter myadapter = new SqlDataAdapter(cmd);
            myadapter.Fill(dt);

            if (PanelCpl.Visible == true)
            {
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
            else
            {
                gviewclientprojectstatus.DataSource = dt;
                gviewclientprojectstatus.DataBind();
            }
            Gridview_colums();
            //hide coments if not status is on hold
            if (QueryStringValues.ProjectStatusID != 7)
                HideShowColumns(gviewclientprojectstatus);
            dt.Dispose();
        }
    }
    private void Gridview_colums()
    {
       
        //if (PermissionManager.IsEnabled(PermissionManager.Feature.ProjectRisks))
        //{
        //    gviewclientprojectstatus.Columns[16].Visible = true; 
        //}
        //else
        //{
        //    gviewclientprojectstatus.Columns[16].Visible = false;
        //}
        //if (PermissionManager.IsEnabled(PermissionManager.Feature.ProjectIssues))
        //{
        //    gviewclientprojectstatus.Columns[15].Visible = true;
        //}
        //else
        //{
        //    gviewclientprojectstatus.Columns[15].Visible = false;
        //}
        //if (PermissionManager.IsEnabled(PermissionManager.Feature.ProjectBOM))
        //{
        //    gviewclientprojectstatus.Columns[14].Visible = true;
        //}
        //else
        //{
        //    gviewclientprojectstatus.Columns[14].Visible = false;
        //}

    }
    public void gridview1()
    {
        string Pstauts = Request.QueryString["Status"].ToString();
        SqlConnection con = new SqlConnection(Constants.DBString);
        //string cmd1 = "SELECT *,ProjectDefaults.ProjectReferencePrefix + ''+convert(varchar,Projects.ProjectReference) as Project, ProjectReference as ProjectReference, Site.Site AS SiteName, ProjectDefaults.ProjectReferencePrefix,Projects.ProjectStatusID FROM Projects INNER JOIN Site ON Projects.SiteID = Site.ID, ProjectDefaults WHERE Projects.ProjectStatusID in (3,5) ";
        //DataTable dt = new DataTable();
        //SqlCommand cmd = new SqlCommand(cmd1, con);
        //cmd.CommandType = CommandType.Text;
        string cmd1 = "DN_ProjectPipeline";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(cmd1, con);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Status", Convert.ToInt32(Pstauts));
        cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(sessionKeys.UID));
        cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
        cmd.Parameters.AddWithValue("@CountryID", Convert.ToInt32(ddlCountry.SelectedValue));
        SqlDataAdapter myadapter = new SqlDataAdapter(cmd);
        myadapter.Fill(dt);
        GridView2.DataSource = dt;
        GridView2.DataBind();
        dt.Dispose();
    }

    protected string Archive(string ProjectReference)
    {
        string Status;
        string Archive;
        Status = "3";
        if ((Status == "1") || (Status == "2") || (Status == "5"))
        {
            Archive = "";
        }
        else
        {
            Archive = "<a href='ClientProjectStatus.aspx?Status=" + Status + "&Archive=" + ProjectReference + "'><img src=images/template/archivesmall.gif border=0 alt='Archive this project'></a>";
        }

        return Archive;

    }

    public string Approval(string ProjectReference, string StatusID)
    {
        string Status, StatusID1;
        Status = StatusID;
        if ((Status == "1") || (Status == "2") || (Status == "4"))
        {
            ProjectApproval = "";
            return ProjectApproval;
        }
        else
        {
            StatusID1 = StatusID;
            if (StatusID1 == "3")
            {
                ProjectApproval = "<a href='WF/Projects/QA/QAProjectSummary.aspx?Project=" + ProjectReference + "'>Requires Approval</a>";
                return ProjectApproval;
            }
            else if ((StatusID1 == "6") || (Status == "5"))
            {
                ProjectApproval = "<a href='WF/Projects/QA/QAProjectSummary.aspx?Project=" + ProjectReference + "'>Approved</a>";
                return ProjectApproval;
            }
        }
        return ProjectApproval;
    }
    protected void lbtnPlan_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/ProjectPlan/projectplan.aspx?ProjectPlanID=0");
    }
    protected void gviewclientprojectstatus_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //int i = e.NewSelectedIndex;
        //HiddenField HID = (HiddenField)gviewclientprojectstatus.Rows[i].FindControl("HID");        
        //getVariations(Convert.ToInt32(HID.Value));
    }
    #region grid function
    protected bool getImage(string status)
    {
        bool s = false;

        if (Convert.ToInt32(status) > 0)
        {
            s = true;
        }


        return s;
    }
    #endregion

    private void HideShowColumns(GridView dg)
    {
        gviewclientprojectstatus.Columns[gviewclientprojectstatus.Columns.Count - 1].Visible = false;

    }

    protected void ddlprogram_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlsubprogram.DataSourceID = "SqlDataSource1";
        ddlsubprogram.DataBind();
        //ddlsubprogram.DataTextField = "OPERATIONSOWNERS";
        //ddlsubprogram.DataValueField = "ID";

    }

    protected void btn_Searchprojects_Click(object sender, EventArgs e)
    {
        Search();

    }
    private DataTable GetSearchData()
    {
        string Pstauts = Request.QueryString["Status"].ToString();

      return  SqlHelper.ExecuteDataset(Constants.DBString,CommandType.StoredProcedure,"DN_ProjectPipeline_Search",
            new SqlParameter("@Status", Convert.ToInt32(Pstauts))
            ,new SqlParameter("@UserID",Convert.ToInt32(sessionKeys.UID)),new SqlParameter("@PortfolioID",sessionKeys.PortfolioID),
            new SqlParameter("@Site",(string.IsNullOrEmpty(ddlsite.SelectedValue) ? " Please Select..." : ddlsite.SelectedValue))
            ,new SqlParameter("@Owner",Convert.ToInt32((string.IsNullOrEmpty(ddlowner.SelectedValue) ? "0" : ddlowner.SelectedValue)))
            ,new SqlParameter("@Programme",Convert.ToInt32((string.IsNullOrEmpty(ddlprogram.SelectedValue) ? "0" : ddlprogram.SelectedValue))),
            new SqlParameter("@SubProgramme",Convert.ToInt32((string.IsNullOrEmpty(ddlsubprogram.SelectedValue) ? "0" : ddlsubprogram.SelectedValue))),new SqlParameter("@Projectdesc",txtprojectdesc.Text.Trim())
            ,new SqlParameter("@ProjectReference",Convert.ToInt32((string.IsNullOrEmpty(txtProjectReference.Text.Trim())?"0":txtProjectReference.Text.Trim())))
            ,new SqlParameter("@CountryID",Convert.ToInt32(ddlCountry.SelectedValue.ToString()))).Tables[0];
        
        //SqlConnection con = new SqlConnection(Constants.DBString);
        //string cmd1;
        //cmd1 = "DN_ProjectPipeline_Search";
        //DataTable dt = new DataTable();
        //SqlCommand cmd = new SqlCommand(cmd1, con);

        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@Status", Convert.ToInt32(Pstauts));
        //cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(sessionKeys.UID));
        //cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
        //cmd.Parameters.AddWithValue("@Site", (string.IsNullOrEmpty(ddlsite.SelectedValue) ? " Please Select..." : ddlsite.SelectedValue));
        //cmd.Parameters.AddWithValue("@Owner", Convert.ToInt32((string.IsNullOrEmpty(ddlowner.SelectedValue) ? "0" : ddlowner.SelectedValue)));
        //cmd.Parameters.AddWithValue("@Programme", Convert.ToInt32((string.IsNullOrEmpty(ddlprogram.SelectedValue) ? "0" : ddlprogram.SelectedValue)));
        //cmd.Parameters.AddWithValue("@SubProgramme", Convert.ToInt32((string.IsNullOrEmpty(ddlsubprogram.SelectedValue) ? "0" : ddlsubprogram.SelectedValue)));
        //cmd.Parameters.AddWithValue("@Projectdesc", txtprojectdesc.Text.Trim());
        //cmd.Parameters.AddWithValue("@ProjectReference", Convert.ToInt32((string.IsNullOrEmpty(txtProjectReference.Text.Trim())?"0":txtProjectReference.Text.Trim())));
        //cmd.Parameters.AddWithValue("@CountryID", Convert.ToInt32(ddlCountry.SelectedValue.ToString()));
        //SqlDataAdapter myadapter = new SqlDataAdapter(cmd);
        //myadapter.Fill(dt);

        //return dt;
        
    }

    protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjDS_Site.SelectParameters["PortfolioID"].DefaultValue = ddlPortfolio.SelectedValue;        
        //ObjDS_Programme.SelectParameters["PortfolioID"].DefaultValue = ddlPortfolio.SelectedValue;
        sessionKeys.PortfolioID =Convert.ToInt32( ddlPortfolio.SelectedValue);
        SqlDataSource1.SelectParameters[0].DefaultValue = ddlprogram.SelectedValue;

    }
    private void Search()
    {
        try
        {
            if (ddlPortfolio.Items.Count > 0)
            {
                sessionKeys.PortfolioID = Convert.ToInt32(ddlPortfolio.SelectedValue);
                sessionKeys.PortfolioName = ddlPortfolio.SelectedItem.Text;
            }

            if (PanelCpl.Visible == true)
            {
                GridView2.DataSource = GetSearchData();
                GridView2.DataBind();
            }
            else 
            {
                gviewclientprojectstatus.DataSource = GetSearchData();
                gviewclientprojectstatus.DataBind();

                //if(gviewclientprojectstatus.Rows.Count == 0)
                //{
                //    pnlCreateProject.CssClass = "col-xs-6 pull-right";
                //}
                //else
                //{
                //    pnlCreateProject.CssClass = "col-xs-2 pull-right";
                //}
            }
            

            Gridview_colums();

            //hide coments if not status is on hold
            if (QueryStringValues.ProjectStatusID != 7)
                HideShowColumns(gviewclientprojectstatus);

           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        ddlsite.SelectedIndex = 0;
        ddlprogram.SelectedIndex = 0;
        ddlsubprogram.SelectedIndex = 0;
        ddlowner.SelectedIndex = 0;
        txtprojectdesc.Text = string.Empty;
        ddlPortfolio.SelectedIndex = 0;
        txtProjectReference.Text = string.Empty;
        //bind with default data
        Search();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //((CheckBox)e.Row.FindControl("rdbGVRow")).Attributes.Add("onclick", "javascript:selectone('" + ((CheckBox)e.Row.FindControl("rdbGVRow")).ClientID + "','" + GridView1.ClientID + "');");
        //    CheckBox chkh = new CheckBox();
        //    chkh = (CheckBox)e.Row.FindControl("rdbGVRow");
        //    chkh.Attributes.Add("onclick", "javascript:return Selectone(this,'" + GridView1.ClientID + "');");

        //}
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox c1 = (CheckBox)e.Row.Cells[0].FindControl("rdbGVRow");
            c1.Attributes.Add("onclick", "return isChecked(this)");
        }

    }
   
    protected void GridView2_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        gviewclientprojectstatus1();
    }
    //protected void GridQuoted_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    //{
    //    GridQuoted.PageIndex = e.NewPageIndex;
    //    gviewclientprojectstatus1();
    //}
    protected void gviewclientprojectstatus_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox c1 = (CheckBox)e.Row.Cells[0].FindControl("rdbGVRow1");
            c1.Attributes.Add("onclick", "return isChecked(this)");
        }

    }
    protected void gviewclientprojectstatus_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gviewclientprojectstatus.DataSource = SortDataTable(GetSearchData(), true);
        gviewclientprojectstatus.PageIndex = e.NewPageIndex;
        gviewclientprojectstatus.DataBind();
    }
    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}",
                    GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}",
                   GridViewSortExpression, GetSortDirection());
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }
    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }

    /// Gets or Sets the gridview sortdirection property
    private string GridViewSortDirection
    {
        get
        {
            return ViewState["SortDirection"] as string ?? "ASC";
        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }
    /// Gets or Sets the gridview sortexpression property
    private string GridViewSortExpression
    {
        get
        {
            return ViewState["SortExpression"] as string ?? string.Empty;
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    /// Returns the direction of the sorting
    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }
    protected void btncopyproject_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = string.Empty;
            int Projectreference = 0;
            int x = gviewclientprojectstatus.Rows.Count;
            for (int i = 0; i < gviewclientprojectstatus.Rows.Count; i++)
            {
                GridViewRow row = gviewclientprojectstatus.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("rdbGVRow1")).Checked;

                if (isChecked)
                {
                    HiddenField lbl = ((HiddenField)row.FindControl("HID"));
                    if (lbl.Value.ToString().Contains(','))
                    {
                        var d = lbl.Value.Split(',');
                        Projectreference = Convert.ToInt32(d[0]);
                    }
                    else
                    {
                        Projectreference = Convert.ToInt32(lbl.Value);
                    }

                }
            }
            if (Projectreference == 0)
            {
                lblmsg.Text = Resources.DeffinityRes.Plsselproject;// "Please select a project ";
            }
            else
            {
                int copiedProjectref = CopyProject(Projectreference);
                if (copiedProjectref > 0)
                {
                    lblmsg.Text = "The project has been copied successfully to Project Ref: " + sessionKeys.Prefix + copiedProjectref.ToString(); //Resources.DeffinityRes.Projectcopiedsuccessfully;//"Project copied successfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblmsg.Text = Resources.DeffinityRes.Cannotcopyproject;// "Cannot copy project";
                }
            }
            Search();
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    private int CopyProject(int ProjectReference)
    {

        

        SqlConnection cn = new SqlConnection(Constants.DBString);
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("DEFFINITY_CopyProject", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProjectReference", ProjectReference);
            SqlParameter outProject = new SqlParameter("@outProject", DbType.Int32);
            outProject.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outProject);
            cmd.ExecuteNonQuery();

            //cmd.ExecuteNonQuery();
            return Convert.ToInt32(outProject.Value);

        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
            return 0;
        }
        finally { cn.Close(); }

    }
    //protected void gviewclientprojectstatus_RowCreated1(object sender, GridViewRowEventArgs e)
    //{
    //    if (sessionKeys.PortfolioID == 0)
    //    {
    //        gviewclientprojectstatus.Columns[5].Visible = true;
    //    }
    //    else
    //    {
    //        gviewclientprojectstatus.Columns[5].Visible = true;
    //    }
    //}
    protected void gviewclientprojectstatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if ((Label)e.Row.FindControl("lblProgress") != null)
            {
                Label lblProgress = (Label)e.Row.FindControl("lblProgress");
                Label lblCR = (Label)e.Row.FindControl("lblCR");
                Label lblPer = (Label)e.Row.FindControl("lblPer");
                Label lblDyas = (Label)e.Row.FindControl("lblDyas");
                Label lblDDays = (Label)e.Row.FindControl("lblDDays");
                Label lblTotalHrs = (Label)e.Row.FindControl("lblTotalDays");

                //lblProgress.Visible = true;
                if (lblDyas.Text != "-")
                {
                    SetProgress(lblProgress, Math.Round(Convert.ToDecimal(lblPer.Text), 2), Convert.ToDouble(lblDyas.Text), Convert.ToDouble(lblDDays.Text), Convert.ToDouble(lblTotalHrs.Text), lblCR.Text);
                }
                //SetProgress(lblProgress,100);
                // progressBar(lblProgress, Convert.ToDecimal(lblPer.Text));
            }

        }
       
        // check when customer is in session
        if (sessionKeys.PortfolioID == 0)
        {
            gviewclientprojectstatus.Columns[8].Visible = true;
        }
        else
        {
            gviewclientprojectstatus.Columns[8].Visible = false;
        }

        // check the selected staus is all
        string Pstauts = Request.QueryString["status"].ToString();
        if (Pstauts == "0")
        {
            //gviewclientprojectstatus.Columns[6].Visible = true;
            //gviewclientprojectstatus.Columns[6].HeaderStyle.CssClass = "header_bg_r";

        }
        else
        {
            //gviewclientprojectstatus.Columns[6].HeaderStyle.CssClass = "header_bg_r";
            gviewclientprojectstatus.Columns[9].Visible = false;
        }

      

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (sessionKeys.PortfolioID == 0)
        {
            GridView2.Columns[4].Visible = true;
        }
        else
        {
            GridView2.Columns[4].Visible = false;
        }
    }

    protected void gviewclientprojectstatus_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridViewSortExpression = e.SortExpression;
        //get the pageindex of the grid.
        int pageIndex = gviewclientprojectstatus.PageIndex;
        gviewclientprojectstatus.DataSource = SortDataTable(GetSearchData(), false);
        gviewclientprojectstatus.DataBind();
        gviewclientprojectstatus.PageIndex = pageIndex;

    }

    protected void gviewclientprojectstatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Watch")
        {
            try
            {

                projectTaskDataContext project = new projectTaskDataContext();

                ProjectsWatched insert = new ProjectsWatched();
                insert.ProjectReference = int.Parse(e.CommandArgument.ToString());
                insert.UserID = sessionKeys.UID;

                project.ProjectsWatcheds.InsertOnSubmit(insert);
                project.SubmitChanges();
                Search();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
   
    #region "Added on 15/02/2011"
    protected string ReturnDays(string days, string CR)
    {
        //, string DDays
        string stemp = string.Empty;
        if (CR != "")
        {
           
                double val = Convert.ToDouble(days);
                if (val > 0.0)
                {
                    stemp = Math.Round(val, 2).ToString();
                }
                else if (val < 0)
                {
                    stemp = " ";
                }
                else
                {
                    stemp = "-";
                }
            
        }
        return stemp;
        
    }
    protected string ReturnDays(string days, string CR,string DDays,string totalHours)
    {
        string stemp = string.Empty;
        try
        {
            //, string DDays

            if (CR != "")
            {

                double val = Convert.ToDouble(days);
                if (val > 0.0)
                {
                    stemp = Math.Round(val, 2).ToString();
                }
                else if (Convert.ToDouble(DDays) == 0)
                {
                    stemp = " ";
                }
                else if (Convert.ToDouble(DDays) < Convert.ToDouble(totalHours))
                {
                    stemp = Math.Round(val, 2).ToString();
                }
                else
                {
                    stemp = "-";
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return stemp;
    }

    protected System.Drawing.Color foreColor(string days)
    {
        System.Drawing.Color stemp = System.Drawing.Color.Gray;
        try
        {

            double val = Convert.ToDouble(days);
            if (val < 0)
            {
                stemp = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
        return stemp;
    }

    public void SetProgress(Label lblProgress, decimal percent, double days, double ddays, double totldays,string CR)
    {
        if (CR != "")
        {
            if (ddays == 0)
            {
                lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD style=color:Red bgcolor=#CCCCCC width=100% forecolor=red  align=center>Setup&nbsp;Required</TD</TR></TABLE>";
            }
            if (ddays < totldays && days < 0 && ddays != 0)
            {
                lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD style=color:Red bgcolor=#CCCCCC width=100% forecolor=red  align=center>0%</TD</TR></TABLE>";
            }

            if (days != 0 && days > 0)
            {
                if (percent == 0)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD bgcolor=#FFF7CE width=100% align=center>100%</TD</TR></TABLE>";
                }
                if (percent >= 50)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD  bgcolor=#99FF66 width=" + percent.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>" + percent.ToString() + "%</TD></TR></TABLE>";
                }
                if (percent <= 49 && percent > 10)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100%  height=7><TR><TD  bgcolor=#FFCC00  width=" + percent.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>" + percent.ToString() + "%</TD></TR></TABLE>";
                }
                if (percent <= 10 && percent > 0)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100%  height=7><TR><TD bgcolor=red width=" + percent.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>" + percent.ToString() + "%</TD></TR></TABLE>";
                }
            }
        }

       
    }
    public void BindCountry()
    {
        DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ID,Country from Country where Visible='Y' and ID in (select CountryID from projects) order by Country").Tables[0];

        ddlCountry.DataSource = dt;
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataTextField = "Country";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("Please Select...","0"));
        if (Request.QueryString["Country"] != null)
        {
            ddlCountry.SelectedValue = Request.QueryString["Country"].ToString();
        }
    }


    protected bool POVisible(string val)
    {
       
        bool visible = false;
        if (int.Parse(val) != 0)
        {
            visible = true;
        }

        return visible;
    }

    #endregion
  
}
