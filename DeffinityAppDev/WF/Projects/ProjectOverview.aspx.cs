using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Deffinity.Bindings;
using Deffinity.ProjectMangers;
using Deffinity.RagSectionProjectManager;
using Deffinity.RagSectionProjectEntity;
using Microsoft.ApplicationBlocks.Data;
using UserMgt.DAL;
using UserMgt.Entity;
using POMgt.DAL;
using POMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
//using TimesheetMgt.DAL;
//using TimesheetMgt.Entity;
using Deffinity.ProgrammeManagers;
using ProjectMgt.BAL;
//using DeffinityManager.Marketplace.BLL;
public partial class ProjectOverview1 : BasePage
{
    
    DisBindings getdata = new DisBindings();
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    string error = "";
    Hashtable ht = new Hashtable();
    public Database dbSysDef;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        //Master.ErrorMsg = "";
        //Master.PageHead = "Project Management";
        Lbllocation.Text = "";
        BindPlaceholderFields();
        if (!IsPostBack)
        {
            BindProjectManager();
            if ((Request.QueryString["Project"] != null))
            {
                //if project is exist
                BindSalesExecutiveddl();
                defaultBindings();
                DipalyDaysFieds();
                //bind Project RAGs
                InsertProjectRags();
                BindGrid();
                BindUsers();
                ProjectGoal();
                CheckUserRole();
                //BindPlaceholderData(QueryStringValues.Project);
            }
            else
            {
                try
                {

                   
                    //hide some when new project is creating
                    ProjectRef1.Visible = false;
                    DisableDaysFieds();
                    //check fro max limit
                    
                    Boolean getVal = ProjectManager.CheckMaxLimit();
                    if (getVal)
                    {
                        if (QueryStringValues.ProjectPlanID > 0)
                        {
                            step1_Bindings();
                            SelectFromProjectPlan(QueryStringValues.ProjectPlanID);
                        }
                        else
                        {

                            step1_Bindings();
                            if ((Request.QueryString["type"] != null))
                            {
                                if (Session["ht"] != null)
                                {
                                    ht = (Hashtable)Session["ht"];
                                    getHash(ht);
                                }
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Maximum limit is reached. To create projects further, Contact Administrator";
                    }
                    //Project RAGs
                    InsertProjectRags();
                    
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
            //VisibilityChecking();
        }
       

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "hideCalendar", @"function hideCalendar(calExtender) { calExtender.hide(); }", true);

    }

    //public void VisibilityChecking()
    //{
    //    try
    //    {
    //        Market_BLL M_BLL = new Market_BLL();
    //        DivProjectClass.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.ProgrammeManagement);
    //        DivProgrammeSubProgramme.Visible = DivProjectClass.Visible;
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}


    private void BindProjectManager() //new
    {
        try
        {
            using (UserDataContext udc = new UserDataContext())
            {
                var Clist = udc.Contractors.Where(a => a.SID == 1 || a.SID == 2 || a.SID == 3).ToList();
                ddlProjectManager.DataSource = Clist.Where(a => a.Status == "Active").ToList().OrderBy(o => o.ContractorName);
                ddlProjectManager.DataTextField = "ContractorName";
                ddlProjectManager.DataValueField = "ID";
                ddlProjectManager.DataBind();
                ddlProjectManager.Items.Insert(0, new ListItem("Please select...", "0"));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindSalesExecutiveddl()
    {
        string P_value = string.Empty;
        using (UserDataContext Udc = new UserDataContext())
        {
            ddlAssignedSalesExecutive.DataSource = (from a in Udc.Contractors
                                                    where (a.SID == 1 || a.SID == 2 || a.SID == 3) && a.Status.ToLower() == "active"
                                                    select new
                                                    {
                                                        id = a.ID,
                                                        Name = a.ContractorName
                                                    }).OrderBy(a => a.Name).ToList();
            ddlAssignedSalesExecutive.DataValueField = "id";
            ddlAssignedSalesExecutive.DataTextField = "Name";
            ddlAssignedSalesExecutive.DataBind();
            ddlAssignedSalesExecutive.Items.Insert(0, new ListItem("Please select...", "0"));
            using (projectTaskDataContext Pdc = new projectTaskDataContext())
            {
                using (PortfolioDataContext Portdc = new PortfolioDataContext())
                {
                    P_value = Pdc.Projects.Where(a => a.ProjectReference == QueryStringValues.Project).Select(a => a.AssignedSalesExecutive.HasValue ? a.AssignedSalesExecutive.Value.ToString() : "").FirstOrDefault();
                    if (string.IsNullOrEmpty(P_value) || P_value == "0")
                    {
                        P_value = Portdc.ProjectPortfolios.Where(a => a.ID == sessionKeys.PortfolioID).Select(a => a.AssignedSalesExecutive.HasValue ? a.AssignedSalesExecutive.Value.ToString() : "0").FirstOrDefault();
                    }
                }
            }
            ddlAssignedSalesExecutive.SelectedValue = P_value;
        }
    }
    //chage visiblity of days and actualcost
    private void DipalyDaysFieds()
    {
        //PanleAcost.Visible = true;
        //PanleDays.Visible = true;
    }
    private void DisableDaysFieds()
    {
        //PanleAcost.Visible = false;
        //PanleDays.Visible = false;
    }

    #region bindings
    private void defaultBindings()
    {
        try
        {
            step1_Bindings();
            //Add custom fields

            
            if (Request.QueryString["Project"] != null)
            {
                step1_selectProject(Convert.ToInt32(Request.QueryString["Project"].ToString()));
               
            }
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void Owner() //new
    {
        DisBindings getdata = new DisBindings();
        DataTable dt = new DataTable();
        //dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select distinct ID,ContractorName from Contractors where  SID in (1,2,3) and SID <>99 and status ='Active' and  ID in (select OwnerID from projects where ProjectReference in (select distinct ID from dbo.fnGetProjectReferences(@UserID)))  union select ID,ContractorName from Contractors where  SID in (1,2,3) and SID <>99 and  status ='Active'and ID=@UserID  order by ContractorName",
        //    new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_AssignedTo_ProjectPlan", new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
        ddlOwner.DataSource = dt;
        ddlOwner.DataTextField = "ContractorName";
        ddlOwner.DataValueField = "ID";
        ddlOwner.DataBind();
        ddlOwner.Items.Insert(0, new ListItem("Please select...", "0"));
        //getdata.DdlBindSelect(ddlowner, "select ID,ContractorName from Contractors  where  SID in (1,2,3) and SID <>99 and  Status = 'ACTIVE'and ID in (select OwnerID from projects where ProjectReference in (select distinct ID from dbo.fnGetProjectReferences(" + sessionKeys.UID + "))) order by ContractorName", "ID", "ContractorName", false, false, true);
    }
    private void step1_Bindings()
    {
        Owner();
        //getdata.DdlBindSelect(ddlOwner, "select ID,ContractorName from Contractors where (SID = 1 or SID = 2 or SID = 3) and Status = 'ACTIVE' order by ContractorName", "ID", "ContractorName", false, false, true);
       // getdata.DdlBindSelect(ddlOwner, "select ID,ContractorName from Contractors where (SID = 1 or SID = 2 or SID = 3) and Status = 'ACTIVE'and ID in (select OwnerID from projects where ProjectReference in (select distinct ID from dbo.fnGetProjectReferences("+sessionKeys.UID+"))) order by ContractorName", "ID", "ContractorName", false, false, true);
        BindPortfolio();
        //BindQA();
        BindCountry();
        BindCity();
        BindSite();
        BindProjectGroup();
        BindQA();
        //Bind custome lables
        getCustomeNames();
        //ddlGroup.Items.Insert(0, Constants.ddlDefaultBind(true));
        //ddlPrimQA.Items.Insert(0, Constants.ddlDefaultBind(true));
        BindSubProgramme();
        BindBaseCurrency();
        ddlCurrency.SelectedValue = "157";
        ProjectStatus();
        BindCatagory();
        BindPONubers();
        BindCustomerUser();
    }
    private void BindBaseCurrency()
    {
        ddlCurrency.DataSource = ProjectManager.BaseCurrency();
        ddlCurrency.DataTextField = "CurrencyName";
        ddlCurrency.DataValueField = "ID";
        ddlCurrency.DataBind();
    }
    private void ProjectStatus()
    {
        //ddlStatus.DataSource = ProjectManager.ProjectStatus();
        //ddlStatus.DataTextField = "Status";
        //ddlStatus.DataValueField = "ID";
        //ddlStatus.DataBind();
        projectTaskDataContext db = new projectTaskDataContext();
        if (sessionKeys.SID == 2 || sessionKeys.SID == 3)
        {
            int[] ids = { 4, 5 };
            var projects = (from r in db.ProjectStatus
                            where !ids.Contains(r.ID)
                            select r);
            if (projects != null)
            {
                ddlStatus.DataSource = projects;
                ddlStatus.DataTextField = "Status";
                ddlStatus.DataValueField = "ID";
                ddlStatus.DataBind();
            }

        }
        else
        {

            ddlStatus.DataSource = Deffinity.ProjectMangers.ProjectManager.ProjectStatus();
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "ID";
            ddlStatus.DataBind();
        }
    }

    #region dropdown_SelectedIndexChanged events
    protected void ddlOwner_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtEmail.Text = getdata.exeScalar("Select EmailAddress from Contractors where ID=" + ddlOwner.SelectedValue, false);
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCity();
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSite();
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubProgramme();
        if (QueryStringValues.Project > 0)
        {
            InsertProjectRags();
            //BindGrid();
        }
    }
    #endregion
    #region Dropdown bindings
    private void BindCountry()
    {
       
        getdata.DdlBindSelect(ddlCountry, string.Format("Select Distinct CountryID as ID,(select Country from Country where ID = AssignedSitesToPortfolio.CountryID) as Country from AssignedSitesToPortfolio where Portfolio ={0}", ddlPortfolio.SelectedValue), "ID", "Country", false, false, true);

        if (QueryStringValues.Project == 0)
        {
            if (ddlCountry.Items.Count > 1)
                ddlCountry.SelectedIndex = 1;
        }
    
    }
    private void BindCity()
    {
       
        getdata.DdlBindSelect(ddlCity, string.Format("Select Distinct CityID as ID,(select City from City where ID = AssignedSitesToPortfolio.CityID) as City from AssignedSitesToPortfolio where Portfolio ={0} And CountryID={1}", ddlPortfolio.SelectedValue, ddlCountry.SelectedValue), "ID", "City", false, false, true);
        if (QueryStringValues.Project == 0)
        {
            if (ddlCity.Items.Count > 1)
                ddlCity.SelectedIndex = 1;
        }
    }
    private void BindSite()
    {
       
        //Bind data
        getdata.DdlBindSelect(ddlSite, string.Format("Select Distinct SiteID as ID,(select Site from Site where ID = AssignedSitesToPortfolio.SiteID) as Site from AssignedSitesToPortfolio where Portfolio ={0} And CityID={1}", ddlPortfolio.SelectedValue, ddlCity.SelectedValue), "ID", "Site", false, false, true);
        //if (QueryStringValues.Project == 0)
        //{
        //    if (ddlSite.Items.Count > 1)
        //        ddlSite.SelectedIndex = 1;
        //}
    }
    private void BindProjectGroup()
    {
        ddlGroup.DataBind();
       // getdata.DdlBindSelect(ddlGroup, string.Format("SELECT ID, OperationsOwners FROM  OperationsOwners where Level=1 "), "ID", "OperationsOwners", false, false, true);
       
    }
    private void BindPortfolio()
    {
        ddlPortfolio.DataBind();
        //getdata.DdlBindSelect(ddlPortfolio, "SELECT ID,PortFolio FROM ProjectPortfolio where visible='true' order by PortFolio", "ID", "PortFolio", false, false, true);
        //getdata.DdlBindSelect(ddlCustomers, "SELECT ID,PortFolio FROM ProjectPortfolio where visible='true' order by PortFolio", "ID", "PortFolio", false, false, true);
        if (QueryStringValues.Project == 0)
        {
            if (ddlPortfolio.Items.Count > 1)
                ddlPortfolio.SelectedValue = sessionKeys.PortfolioID.ToString();
            //if (ddlCustomers.Items.Count > 1)
            //    ddlCustomers.SelectedValue = sessionKeys.PortfolioID.ToString();
        }
    
    }
    //private void BindQA()
    //{

    //    getdata.DdlBindSelect(ddlPrimQA, "Select ID,ContractorName from Contractors where SID in (1,3,5) and Status ='Active'", "ID", "ContractorName", false, false, true);

    //}
    private void BindQA()
    {
        ddlPrimQA.Items.Clear();
        ddlPrimQA.DataSource = ProjectManager.GetQABasedOnPortfolio(int.Parse(ddlPortfolio.SelectedValue));
        ddlPrimQA.DataTextField = "ContractorName";
        ddlPrimQA.DataValueField = "ID";
        ddlPrimQA.DataBind();
        ddlPrimQA.Items.Insert(0, Constants.ddlDefaultBind(true));
        //getdata.DdlBindSelect(ddlPrimQA, "Select ID,ContractorName from Contractors where SID in(1,3,5) and Status ='Active'", "ID", "ContractorName", false, false, true);


    }
    private void BindUsers() //Raised By...
    {
        try
        {
            UserDataContext users = new UserDataContext();
            var sid = new int[] { 10, 8, 6 };
            var raisedBy = from r in users.Contractors
                           where !sid.Contains(r.SID.Value)
                           orderby r.ContractorName
                           select r;
            //ddlRaisedBy.DataSource = raisedBy;
            //ddlRaisedBy.DataValueField = "ID";
            //ddlRaisedBy.DataTextField = "ContractorName";
            //ddlRaisedBy.DataBind();
            //ddlRaisedBy.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void BindCatagory()
    {
        projectTaskDataContext dt = new projectTaskDataContext();
        ddlCategory.DataSource = (from p in dt.ProjectClasses
                               select new { p.ID, p.ClassName }).ToList();
        ddlCategory.DataValueField = "ID";
        ddlCategory.DataTextField = "ClassName";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("Please select...", "0"));

        //getdata.DdlBindSelect(ddlCategory, "Select CategoryID,CategoryName FROM ProjectCategory  ORDER BY CategoryName", "CategoryID", "CategoryName", false, false, true);
    }

    private void ApplyProjectClassChecklist(int projectreference,int ProjectCalssID)
    {
        //projectTaskDataContext dt = new projectTaskDataContext();
        //var pct = from p1 in dt.ProjectClassByProjectReferences
        //            where p1.ProjectReference == projectreference && p1.ClassID== ProjectCalssID
        //         select p1;
        //if (pct != null)
        //{
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "projectclassbyprojectreference_insert", new SqlParameter("@ClassID", ProjectCalssID), new SqlParameter("@ProjectReference", projectreference));
        //}
    }


    private void BindSubProgramme()
    {
        try
        {
            ddlSubprogramme.DataBind();
            //ddlSubprogramme.DataSource = DefaultDatabind.GetProgrammes(int.Parse(ddlGroup.SelectedValue));
            //ddlSubprogramme.DataTextField = "OPERATIONSOWNERS";
            //ddlSubprogramme.DataValueField = "ID";
            //ddlSubprogramme.DataBind();
            //ddlSubprogramme.Items.Insert(0, Constants.ddlDefaultBind(true));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion
    protected void getCustomeNames()
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetSqlStringCommand("SELECT Custom1,Custom2 FROM ProjectDefaults");
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    lblcustom1.Text = dr[0].ToString();
                    lblCustom2.Text = dr[1].ToString();
                }
            }

            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    #endregion
    private void SelectFromProjectPlan(int pid)
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            // Initialize the Stored Procedure
            DbCommand cmd = db.GetStoredProcCommand("DN_SelectProjectPlan");
            db.AddInParameter(cmd, "@ProjectPlanID", DbType.Int32, pid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    txtProjectTitle.Text = dr["ProjectTitle"].ToString();

                    txtDesc.Text = dr["ProjectDescription"].ToString();
                    txtCostCenter.Text = dr["CostCenter"].ToString();
                    txtStartDate.Text = string.Format("{0:d}", dr["ExpectedStartDate"]);
                    txtDateRaised.Text = string.Format("{0:d}", dr["ExpectedStartDate"]);
                    txtEndDate.Text = string.Format("{0:d}", dr["ExpectedEndDate"]);
                    txtPOExpDate.Text = string.Format("{0:d}", dr["ExpectedEndDate"]);

                    //DateTime dt1 = DateTime.Parse(txtDateRaised.Text);
                    //DateTime dt2 = DateTime.Parse(txtPOExpDate.Text);

                    //txtDurationDays.Text = (dt1.Day - dt2.Day).ToString();

                    ddlStatus.SelectedValue = "1";//pending status
                    ddlOwner.SelectedValue = dr["OwnerID"].ToString();
                    ddlPortfolio.SelectedValue = dr["PortfolioID"].ToString();
                    //ddlCustomers.SelectedValue = dr["PortfolioID"].ToString();
                    BindCountry();
                    ddlCountry.SelectedValue = dr["CountryID"].ToString();
                    BindCity();
                    ddlCity.SelectedValue = dr["CityID"].ToString();
                    BindSite();
                    ddlSite.SelectedValue = dr["SiteID"].ToString();
                    BindProjectGroup();
                    ddlGroup.SelectedValue = string.IsNullOrEmpty(dr["ProgrammeID"].ToString()) ? "0" : dr["ProgrammeID"].ToString();
                    BindSubProgramme();
                    //ddlSubprogramme.SelectedValue = string.IsNullOrEmpty(dr["SubProgrammeID"].ToString()) ? "0" : dr["SubProgrammeID"].ToString();
                    ddlPriority.SelectedValue = string.IsNullOrEmpty(dr["Priority"].ToString()) ? "0" : dr["Priority"].ToString();
                    ddlRagstatus.SelectedValue = string.IsNullOrEmpty(dr["RAGstatus"].ToString()) ? "0" : dr["RAGstatus"].ToString();
                    txtCustom1.Text = dr["CustomerReference"].ToString();
                    BindProjectManager();
                    ddlProjectManager.SelectedValue = string.IsNullOrEmpty(dr["ProjectManager"].ToString()) ? "0" : dr["ProjectManager"].ToString();
                }
                dr.Close();
            }
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "select from project plan ");
        }
    }
    private void step1_selectProject(int Pref)
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            // Initialize the Stored Procedure  
            DbCommand cmd = db.GetStoredProcCommand("DN_ProjectSelect");
            db.AddInParameter(cmd, "@ID", DbType.Int32, Pref);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    txtProjectTitle.Text = dr["ProjectTitle"].ToString();
                    txtEmail.Text = dr["OwnerEmail"].ToString();
                    txtDesc.Text = dr["ProjectDescription"].ToString();
                    ddlStatus.SelectedValue = dr["ProjectStatusID"].ToString();
                    //HiddenStatus.Value = ddlStatus.SelectedValue;
                    txtCostCenter.Text = dr["CostCentre"].ToString();
                    txtStartDate.Text = getdata.getDate(dr["StartDate"].ToString());
                    txtEndDate.Text = getdata.getDate(dr["ProjectEndDate"].ToString());
                    txtQAdate.Text = getdata.getDate(dr["ScheduledQADate"].ToString());
                    txtActualCost.Text = string.Format("{0:F2}", dr["ActualCost"]);
                  
                    txtCustom2.Text = dr["Custom2"].ToString();
                    ddlCurrency.SelectedValue = dr["BaseCurrency"].ToString();
                    ddlOwner.SelectedValue = dr["OwnerID"].ToString();
                    BindPortfolio();
                    ddlPortfolio.SelectedValue = dr["Portfolio"].ToString();
                    //bind county,city,site accordingly to portfolio
                    BindCountry();
                    ddlCountry.SelectedValue = dr["CountryID"].ToString();
                    BindCity();
                    ddlCity.SelectedValue = dr["CityID"].ToString();
                    BindSite();
                    ddlSite.SelectedValue = dr["SiteID"].ToString();
                    //bind owners & according data to portfolio                    
                    BindProjectGroup();
                    ddlGroup.SelectedValue = dr["OwnerGroupID"].ToString();
                    BindSubProgramme();
                    ddlSubprogramme.SelectedValue = string.IsNullOrEmpty(dr["SubProgramme"].ToString()) ? "0" : dr["SubProgramme"].ToString();
                    txtCustom1.Text = dr["CustomerReference"].ToString();
                    ddlPriority.SelectedValue = dr["Priority"].ToString();
                    ddlRagstatus.SelectedValue = dr["RAGstatus"].ToString();
                    BindCatagory();
                    ddlCategory.SelectedValue = dr["CategoryID"].ToString();

                    txtRequestName.Text = dr["RequestorName"].ToString();
                    txtRequestEmail.Text = dr["RequestorEmail"].ToString();
                    //bind QA based on selected portfolio
                    BindQA();
                    ddlPrimQA.SelectedValue = dr["QADesignate"].ToString();
                    chkCustomer.Checked = Convert.ToBoolean(string.IsNullOrEmpty(dr["ViewCustomer"].ToString()) ? "false" : dr["ViewCustomer"].ToString());
                    int val = int.Parse(string.IsNullOrEmpty(dr["POCheck"].ToString()) ? "0" : dr["POCheck"].ToString());
                    if (val == 1)
                    {
                        chkPO.Checked = true; //Convert.ToBoolean(string.IsNullOrEmpty(dr["ViewCustomer"].ToString()) ? "false" : "true");
                    }
                    else
                    {
                        chkPO.Checked = false;
                    }
                    BindCustomerUser();
                    ddlCustomerUsers.SelectedValue = string.IsNullOrEmpty(dr["CustomerUserID"].ToString()) ? "0" : dr["CustomerUserID"].ToString();
                    BindSalesStaff();
                    ddlSalesStaff.SelectedValue = string.IsNullOrEmpty(dr["SalesStaffID"].ToString()) ? "0" : dr["SalesStaffID"].ToString();
                }
                dr.Close();
            }

            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Select from projects table");
        }
    }
    //check for QA date must grater than project end date
    private bool ChekQAData(string edate, string QADate)
    {
        bool retVal = false;
        if (QADate != "")
        {
            if (Convert.ToDateTime(edate) <= Convert.ToDateTime(QADate))
            {
                retVal = true;
            }
        }
        else
        {
            retVal = true;
        }
        return retVal;
    }
    private void insertProject()
    {
        try
        {
            //check qa shedule data
            int ProgSele = 0;
            if (ChekQAData(txtEndDate.Text.Trim(), txtQAdate.Text.Trim()))
            {
                dbSysDef = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd2 = dbSysDef.GetSqlStringCommand("SELECT * FROM ProjectDefaults");
                using (IDataReader dr = dbSysDef.ExecuteReader(cmd2))
                {
                        if (dr.Read())
                            {
                                ProgSele = Convert.ToInt16(string.IsNullOrEmpty(dr["ProgSele"].ToString()) ? "0" : dr["ProgSele"].ToString());// dr["AnnualYearEnd"].ToString(); ; 

                                if ((ProgSele == 0) && (ddlGroup.SelectedValue.ToString() !="0"))
                                {

                                    Database db = DatabaseFactory.CreateDatabase("DBstring");
                                    DbCommand cmd = db.GetStoredProcCommand("DN_InsertProject");
                                    //add parameters
                                    db.AddInParameter(cmd, "@ProjectTitle", DbType.String, txtProjectTitle.Text.Trim());
                                    db.AddInParameter(cmd, "@ProjectDescription", DbType.String, txtDesc.Text.Trim());
                                    db.AddInParameter(cmd, "@CostCentre", DbType.String, txtCostCenter.Text.Trim());
                                    db.AddInParameter(cmd, "@OwnerID", DbType.Int32, Convert.ToInt32(ddlOwner.SelectedValue));
                                    db.AddInParameter(cmd, "@OwnerEmail", DbType.String, txtEmail.Text.Trim());
                                    db.AddInParameter(cmd, "@OwnerGroupID", DbType.Int32, Convert.ToInt32(ddlGroup.SelectedValue));
                                    db.AddInParameter(cmd, "@CountryID", DbType.Int32, Convert.ToInt32(ddlCountry.SelectedValue));
                                    db.AddInParameter(cmd, "@CityID", DbType.Int32, Convert.ToInt32(ddlCity.SelectedValue));
                                    db.AddInParameter(cmd, "@SiteID", DbType.Int32, Convert.ToInt32(ddlSite.SelectedValue));
                                    db.AddInParameter(cmd, "@StartDate", DbType.Date, Convert.ToDateTime(txtStartDate.Text));
                                    db.AddInParameter(cmd, "@ProjectStatusID", DbType.Int32, Convert.ToInt32(ddlStatus.SelectedValue));
                                    db.AddInParameter(cmd, "@ProjectEndDate", DbType.Date, Convert.ToDateTime(txtEndDate.Text));
                                    //optional parameter
                                    int QA = 0;
                                    if (ddlPrimQA.Items.Count > 0)
                                    {
                                        if (ddlPrimQA.SelectedValue != "Please select...")
                                        {
                                            QA = Convert.ToInt32(ddlPrimQA.SelectedValue);
                                        }
                                    }
                                    db.AddInParameter(cmd, "@QADesignate", DbType.Int32, QA);
                                    string QADate = "";
                                    if (txtQAdate.Text != "")
                                    {
                                        QADate = txtQAdate.Text;
                                    }
                                    db.AddInParameter(cmd, "@ScheduledQADate", DbType.String, QADate);
                                    db.AddInParameter(cmd, "@BaseCurrency", DbType.Int32, Convert.ToInt32(ddlCurrency.SelectedValue));
                                    db.AddInParameter(cmd, "@Portfolio", DbType.Int32, Convert.ToInt32(ddlPortfolio.SelectedValue));
                                    db.AddInParameter(cmd, "@Custom1", DbType.String, txtCustom1.Text);
                                    db.AddInParameter(cmd, "@Custom2", DbType.String, txtCustom2.Text);
                                    db.AddInParameter(cmd, "@Priority", DbType.String, ddlPriority.SelectedValue.Trim());
                                    db.AddInParameter(cmd, "@RAGstatus", DbType.String, ddlRagstatus.SelectedValue.Trim());
                                    db.AddInParameter(cmd, "@CategoryID", DbType.Int32, 0);
                                    db.AddInParameter(cmd, "@ViewCustomer", DbType.Boolean, Convert.ToBoolean(chkCustomer.Checked ? "true" : "false"));
                                    db.AddInParameter(cmd, "@RequestorName", DbType.String, txtRequestName.Text);
                                    db.AddInParameter(cmd, "@RequestorEmail", DbType.String, txtRequestEmail.Text);
                                    db.AddInParameter(cmd, "@SubProgramme ", DbType.Int32, ddlSubprogramme.SelectedValue);
                                    db.AddInParameter(cmd, "@POCheck", DbType.Int32, Convert.ToInt32(chkPO.Checked ? "1" : "0"));
                                    db.AddInParameter(cmd, "@CustomerReference", DbType.String, txtCustom1.Text);
                                    //@ViewCustomer
                                    db.AddOutParameter(cmd, "@OutIdt", DbType.Int32, 4);
                                    db.AddOutParameter(cmd, "@Outval", DbType.Int32, 4);
                                    db.ExecuteNonQuery(cmd);
                                    //if getVal = 1 item already exists
                                    int getVal = (int)db.GetParameterValue(cmd, "Outval");
                                    if (getVal != 1)
                                    {
                                        //project reference
                                        int identity = (int)db.GetParameterValue(cmd, "@OutIdt");
                                        cmd.Dispose();
                                        
                                        //divProject.Visible = true;
                                        //lblProject.Text = identity.ToString();
                                        //insert project plan task items into projecttask items
                                        if (QueryStringValues.ProjectPlanID > 0)
                                        {
                                            insertprojecttaskpid();
                                        }
                                        //insert into programe dependency
                                        //InsertProgrammeDepency(identity);

                                        //Navigations(identity.ToString());
                                    }
                                    else
                                    {
                                        //insertprojecttaskpid();                    
                                        lblMsg.Text = "Please check project title already exist.";
                                        cmd.Dispose();
                                    }
                                }
                                else
                                {
                                    lblMsg.Text = "Please select a Programme for this project.";
                                }            
                            }

                }

                
            }
            else
            {
                lblMsg.Text = "Please ensure scheduled QA date is equal or after the expected completion date.";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    //private void InsertProgrammeDepency(int pref)
    //{
    //    try
    //    {
    //        Database db = DatabaseFactory.CreateDatabase("DBstring");
    //        DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_INSERT_USERPROJPERMISSIONS");
    //        //add parameters
    //        db.AddInParameter(cmd, "@PROJECTREFERENCE", DbType.Int32, pref);
    //        db.AddInParameter(cmd, "@PORTFOLIOID", DbType.Int32, Convert.ToInt32(ddlPortfolio.SelectedValue));
    //        db.AddInParameter(cmd, "@OWNERID", DbType.Int32, Convert.ToInt32(ddlOwner.SelectedValue));
    //        db.ExecuteNonQuery(cmd);
    //        cmd.Dispose();
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.LogException(ex.Message, "Insert dependency");
    //    }
    //}
    private int updateProject(int Pref)
    {
        int Status = 0;
        try
        {
            
            int ProgSele = 0;
            if (ChekQAData(txtEndDate.Text.Trim(), txtQAdate.Text.Trim()))
            {
                dbSysDef = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd2 = dbSysDef.GetSqlStringCommand("SELECT * FROM ProjectDefaults");
                using (IDataReader dr = dbSysDef.ExecuteReader(cmd2))
                {
                    if (dr.Read())
                    {
                        ProgSele = Convert.ToInt16(string.IsNullOrEmpty(dr["ProgSele"].ToString()) ? "0" : dr["ProgSele"].ToString());// dr["AnnualYearEnd"].ToString(); ; 

                        if ((ProgSele == 1) && (ddlGroup.SelectedValue.ToString() == "0"))
                        {
                            lblMsg.Text = "Please select a programme for this project.";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            Status = 0;
                        }
                        else if ((ProgSele == 0)||((ProgSele ==1) && (ddlGroup.SelectedValue.ToString()!="0"))) 
                        {
                            

                            Database db = DatabaseFactory.CreateDatabase("DBstring");
                            DbCommand cmd = db.GetStoredProcCommand("DN_UpdateProjects");
                            //add parameters
                            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Pref);
                            db.AddInParameter(cmd, "@ProjectTitle", DbType.String, txtProjectTitle.Text.Trim());
                            db.AddInParameter(cmd, "@ProjectDescription", DbType.String, txtDesc.Text.Trim());
                            db.AddInParameter(cmd, "@CostCentre", DbType.String, txtCostCenter.Text.Trim());
                            db.AddInParameter(cmd, "@OwnerID", DbType.Int32, Convert.ToInt32(ddlOwner.SelectedValue));
                            db.AddInParameter(cmd, "@OwnerEmail", DbType.String, txtEmail.Text.Trim());
                            db.AddInParameter(cmd, "@OwnerGroupID", DbType.Int32, Convert.ToInt32(ddlGroup.SelectedValue));
                            db.AddInParameter(cmd, "@CountryID", DbType.Int32, Convert.ToInt32(ddlCountry.SelectedValue));
                            db.AddInParameter(cmd, "@CityID", DbType.Int32, Convert.ToInt32(ddlCity.SelectedValue));
                            db.AddInParameter(cmd, "@SiteID", DbType.Int32, Convert.ToInt32(ddlSite.SelectedValue));
                            db.AddInParameter(cmd, "@StartDate", DbType.Date, Convert.ToDateTime(txtStartDate.Text));
                            db.AddInParameter(cmd, "@ProjectStatusID", DbType.Int32, Convert.ToInt32(ddlStatus.SelectedValue));
                            db.AddInParameter(cmd, "@ProjectEndDate", DbType.Date, Convert.ToDateTime(txtEndDate.Text));
                            //optional parameter
                            int QA = 0;
                            if (ddlPrimQA.Items.Count > 0)
                            {
                                if (ddlPrimQA.SelectedValue != "Please select...")
                                {
                                    QA = Convert.ToInt32(ddlPrimQA.SelectedValue);
                                }
                            }
                            db.AddInParameter(cmd, "@QADesignate", DbType.Int32, QA);
                            string QADate = "";
                            if (txtQAdate.Text != "")
                            {
                                QADate = txtQAdate.Text;
                            }
                            db.AddInParameter(cmd, "@ScheduledQADate", DbType.String, QADate);
                            db.AddInParameter(cmd, "@BaseCurrency", DbType.Int32, Convert.ToInt32(ddlCurrency.SelectedValue));
                            double Actual = 0;
                            if (txtActualCost.Text.Trim() != "")
                            {
                                Actual = Convert.ToDouble(txtActualCost.Text.Trim());
                            }
                            db.AddInParameter(cmd, "@ActualCost", DbType.Double, Actual);
                            db.AddInParameter(cmd, "@Portfolio", DbType.Int32, Convert.ToInt32(ddlPortfolio.SelectedValue));
                            db.AddInParameter(cmd, "@Custom1", DbType.String, txtCustom1.Text);
                            db.AddInParameter(cmd, "@Custom2", DbType.String, txtCustom2.Text);
                            db.AddInParameter(cmd, "@Priority", DbType.String, ddlPriority.SelectedValue.Trim());
                            db.AddInParameter(cmd, "@RAGstatus", DbType.String, ddlRagstatus.SelectedValue.Trim());
                            db.AddInParameter(cmd, "@CategoryID", DbType.Int32, ddlCategory.SelectedValue);
                            db.AddInParameter(cmd, "@ViewCustomer ", DbType.Boolean, Convert.ToBoolean(chkCustomer.Checked ? "true" : "false"));
                            db.AddInParameter(cmd, "@RequestorName ", DbType.String, txtRequestName.Text.Trim());
                            db.AddInParameter(cmd, "@RequestorEmail ", DbType.String, txtRequestEmail.Text.Trim());
                            db.AddInParameter(cmd, "@SubProgramme ", DbType.Int32, Convert.ToInt32(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue));
                            db.AddInParameter(cmd, "@POCheck", DbType.Int32, Convert.ToInt32(chkPO.Checked ? "1" : "0"));
                            db.AddInParameter(cmd, "@CustomerReference", DbType.String, txtCustom1.Text);
                            db.AddInParameter(cmd, "@CustomerUserID", DbType.Int32, int.Parse(ddlCustomerUsers.SelectedValue));
                            db.AddInParameter(cmd, "@DescriptionofWorks", DbType.String, string.Empty);
                            db.AddInParameter(cmd, "@SalesStaffID", DbType.Int32, int.Parse(ddlSalesStaff.SelectedValue));
                            db.AddInParameter(cmd, "@ProjectManager", DbType.Int32, Convert.ToInt32(ddlProjectManager.SelectedValue));
                            db.AddInParameter(cmd, "@AssignedSalesExecutive", DbType.Int32, Convert.ToInt32(ddlAssignedSalesExecutive.SelectedValue));
                            //@ViewCustomer 
                            db.AddOutParameter(cmd, "@Outval", DbType.Int32, 4);
                            db.ExecuteNonQuery(cmd);
                            //if getVal = 1 item already exists
                            int getVal = (int)db.GetParameterValue(cmd, "Outval");
                            cmd.Dispose();
                            UpdateOpsStaus(Pref, Convert.ToInt32(ddlStatus.SelectedValue));
                            if (getVal == 1)
                            {
                                //project title already exist
                                lblMsg.Text = "Please check project title already exist.";
                            }
                            else
                            {
                                //updatet the journal update
                                Project project = ProjectJournalBAL.GetProjectsByReference(QueryStringValues.Project);
                                ProjectJournalBAL.InsertProjectJournal(project);
                                if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
                                {
                                    //send a mail if project goes to live
                                    //SendMail(Pref);

                                }
                                //insert dependency
                                //InsertProgrammeDepency(Pref);
                                // Navigations(Pref.ToString());
                            }
                            Status = 1;
                        }
                    }
                }
            }
            else
            {
                lblMsg.Text = "Please ensure scheduled QA date is equal or after the expected completion date.";
                Status = 0;
            }
            
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        return Status;

    }
    public void insertprojecttaskpid()
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand("DN_taskinsertpid");
            db.AddInParameter(cmd, "@ProjectPlanID", DbType.Int32, QueryStringValues.ProjectPlanID);
            db.ExecuteNonQuery(cmd);
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }

    //private void SendMail(int Pref)
    //{
    //    sendMail.sendMail(Pref, 0, 1);
    //}
    private string[] retArray(int Pref)
    {
        ArrayList ar = new ArrayList();
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd = db.GetStoredProcCommand("DN_GetResourceEmail");
        db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Pref);

        using (IDataReader dr = db.ExecuteReader(cmd))
        {
            while (dr.Read())
            {
                ar.Add(dr["EmailAddress"].ToString());
            }
        }
        cmd.Dispose();
        string[] array1 = (string[])ar.ToArray(typeof(string));

        return array1;
    }
    private void Navigations(string Pref)
    {
        //inset into journal
        JornalInsert(int.Parse(Pref), sessionKeys.UID);
        //Response.Redirect("~/ProjectTasks.aspx?Project=" + Pref);
        Response.Redirect(PermissionManager.GetNextURL(PermissionManager.Feature.ProjectOverview) + "?Project=" + Pref.ToString());
    }

    private void UpdateOpsStaus(int pref, int status)
    {
        //DN_UpdateAC2Pstaus 
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand("DN_UpdateAC2Pstaus");
            db.AddInParameter(cmd, "@Pref", DbType.Int32, pref);
            db.AddInParameter(cmd, "@StatusID", DbType.Int32, status);
            db.ExecuteNonQuery(cmd);
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    protected void btnDays_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlDataReader dr;
        try
        {
            if (txtdays.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand("Deffinity_ProjDays", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Days", Convert.ToInt32(txtdays.Text)));
                cmd.Parameters.Add(new SqlParameter("@Pref", QueryStringValues.Project));
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                cmd = new SqlCommand("Deffinity_ProjItemDays", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Days", Convert.ToInt32(txtdays.Text)));
                cmd.Parameters.Add(new SqlParameter("@Pref", QueryStringValues.Project));
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                //bind default data
                defaultBindings();
            }
            else
            {
                error = "Please enter days";
            }

            txtdays.Text = "";
        }

        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);

        }
        finally
        {
            con.Close();
        }
    }
    protected void btnNext1_Click(object sender, EventArgs e)
    {
        try
        {
            
            lblMsg.Text = "";
            if ((Request.QueryString["Project"] != null))
            {
                if (!PermissionManager.IsPermitted(Convert.ToInt32(Request.QueryString["Project"].ToString()), Convert.ToInt32(Session["UID"]), PermissionManager.PermissionsTo.ModifyProject))
                {
                    //Master.ErrorMsg = "User doesn't have rights to modify project";
                    return;
                }
                updateProject(Convert.ToInt32(Request.QueryString["Project"].ToString()));
                //update project class section
                if (int.Parse(ddlCategory.SelectedValue) > 0)
                    ApplyProjectClassChecklist(QueryStringValues.Project, int.Parse(ddlCategory.SelectedValue));
                //bind Project RAGs
                InsertProjectRags();
                //Insert update milestone to project taks items
                InsertMilestoneToTask();
                
            }
            else
            {
                insertProject();
                //bind Project RAGs
                InsertProjectRags();
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    #region session hashtable
    private Hashtable insertHash()
    {
        try
        {
            ht.Add("ProjectTitle", txtProjectTitle.Text);
            ht.Add("OwnerEmail", txtEmail.Text);
            ht.Add("ProjectDescription", txtDesc.Text);
            ht.Add("ProjectStatusID", ddlStatus.SelectedValue);
            ht.Add("CostCentre", txtCostCenter.Text);
            ht.Add("StartDate", txtStartDate.Text);
            ht.Add("ProjectEndDate", txtEndDate.Text);
            ht.Add("ScheduledQADate", txtQAdate.Text);
            ht.Add("ActualCost", txtActualCost.Text);
            ht.Add("custom1", txtCustom1.Text);
            ht.Add("custom2", txtCustom2.Text);
            ht.Add("BaseCurrency", ddlCurrency.SelectedValue);
            ht.Add("CountryID", ddlCountry.SelectedValue);
            ht.Add("CityID", ddlCity.SelectedValue);
            ht.Add("SiteID", ddlSite.SelectedValue);
            ht.Add("OwnerID", ddlOwner.SelectedValue);
            ht.Add("OwnerGroupID", ddlGroup.SelectedValue);
            ht.Add("Portfolio", ddlPortfolio.SelectedValue);
            ht.Add("Ragstatus", ddlRagstatus.SelectedValue);
            ht.Add("Priority", ddlPriority.SelectedValue);
            ht.Add("Category", 0);
            ht.Add("SubProgramme", ddlSubprogramme.SelectedValue);
            ht.Add("PrimQA", ddlPrimQA.SelectedValue);

        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

        return ht;

    }
    private void getHash(Hashtable ht)
    {
        try
        {
            txtProjectTitle.Text = ht["ProjectTitle"].ToString();
            txtEmail.Text = ht["OwnerEmail"].ToString();
            txtDesc.Text = ht["ProjectDescription"].ToString();
            txtCostCenter.Text = ht["CostCentre"].ToString();
            txtStartDate.Text = ht["StartDate"].ToString();
            txtEndDate.Text = ht["ProjectEndDate"].ToString();
            txtQAdate.Text = ht["ScheduledQADate"].ToString();
            txtActualCost.Text = ht["ActualCost"].ToString();
            txtCustom1.Text = ht["custom1"].ToString();
            txtCustom2.Text = ht["custom1"].ToString();
            ddlCurrency.SelectedValue = ht["BaseCurrency"].ToString();
            ddlStatus.SelectedValue = ht["ProjectStatusID"].ToString();
            ddlOwner.SelectedValue = ht["OwnerID"].ToString();
            //Bind portfolio 
            BindPortfolio();
            ddlPortfolio.SelectedValue = ht["Portfolio"].ToString();
            //bind owners and groups accordingly
            BindProjectGroup();
            ddlGroup.SelectedValue = ht["OwnerGroupID"].ToString();
            BindSubProgramme();
            ddlSubprogramme.SelectedValue = ht["SubProgramme"].ToString();
            BindCountry();
            ddlCountry.SelectedValue = ht["CountryID"].ToString();
            BindCity();
            ddlCity.SelectedValue = ht["CityID"].ToString();
            BindSite();
            ddlSite.SelectedValue = ht["SiteID"].ToString();
            ddlRagstatus.SelectedValue = ht["Ragstatus"].ToString();
            ddlPriority.SelectedValue = ht["Priority"].ToString();
            //BindCatagory();
            //ddlCategory.SelectedValue = ht["Category"].ToString();
            BindQA();
            ddlPrimQA.SelectedValue = ht["PrimQA"].ToString();
            //make session empty
            Session["ht"] = null;
        }

        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "get data from hashtable in build project");
        }
        finally
        {
            ht.Clear();
        }
    }
    #endregion
    #region navigations

    protected void btnCountry_Click(object sender, EventArgs e)
    {
        if (ddlPortfolio.SelectedValue != "Please select...")
        {
            sessionKeys.PortfolioID = int.Parse(ddlPortfolio.SelectedValue);
            sessionKeys.PortfolioName = ddlPortfolio.SelectedItem.Text;
        }
        if (Request.QueryString["Project"] != null)
        {
            Response.Redirect("~/WF/CustomerAdmin/Locations.aspx?project=" + QueryStringValues.Project.ToString());
        }
        else
        {
            Session["ht"] = insertHash();
            Response.Redirect("~/WF/CustomerAdmin/Locations.aspx?type=project");
        }

    }
    protected void btnCity_Click(object sender, EventArgs e)
    {
        if (ddlPortfolio.SelectedValue != "Please select...")
        {
            sessionKeys.PortfolioID = int.Parse(ddlPortfolio.SelectedValue);
            sessionKeys.PortfolioName = ddlPortfolio.SelectedItem.Text;
        }
        if (Request.QueryString["Project"] != null)
        {
            Response.Redirect("~/WF/CustomerAdmin/Locations.aspx?project=" + QueryStringValues.Project.ToString());
        }
        else
        {
            Session["ht"] = insertHash();
            Response.Redirect("~/WF/CustomerAdmin/Locations.aspx?type=project");
        }
    }
    protected void btnSite_Click(object sender, EventArgs e)
    {
        if (ddlPortfolio.SelectedValue != "Please select...")
        {
            sessionKeys.PortfolioID = int.Parse(ddlPortfolio.SelectedValue);
            sessionKeys.PortfolioName = ddlPortfolio.SelectedItem.Text;
        }
        if (Request.QueryString["Project"] != null)
        {
            Response.Redirect("~/WF/CustomerAdmin/Locations.aspx?project=" + QueryStringValues.Project.ToString());
        }
        else
        {
            Session["ht"] = insertHash();
            Response.Redirect("~/WF/CustomerAdmin/Locations.aspx?type=project");
        }

    }
    protected void btnOwners_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Project"] != null)
        {
            Response.Redirect("~/WF/Admin/ProgrammeManagement.aspx?project=" + QueryStringValues.Project.ToString());
        }
        else
        {
            Session["ht"] = insertHash();
            Response.Redirect("~/WF/Admin/ProgrammeManagement.aspx?type=project");
        }

    }
    protected void btnPortfolio_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Project"] != null)
        {
            Response.Redirect("~/WF/CustomerAdmin/Portfolio.aspx?tab=1&Project=" + QueryStringValues.Project.ToString());
        }
        else
        {
            Session["ht"] = insertHash();
            Response.Redirect("~/WF/CustomerAdmin/Portfolio.aspx?tab=1&type=project");
        }

    }
    protected void btnCategory_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Project"] != null)
        {
            Response.Redirect("~/WF/Admin/AdminDropDown.aspx?project=" + QueryStringValues.Project.ToString() + "&type=category");
        }
        else
        {
            Session["ht"] = insertHash();
            Response.Redirect("~/WF/Admin/AdminDropDown.aspx?type=category");
        }

    }
    protected void btnProgramme_Click(object sender, EventArgs e)
    {
        if (QueryStringValues.Project == 0)
        {
            Session["ht"] = insertHash();
        }
        Response.Redirect(string.Format("~/WF/Admin/ProgrammeManagement.aspx?type=project&project={0}", QueryStringValues.Project.ToString()));
    }
    #endregion

    protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectCountryPortfolio();
        BindQA();
        //Bind Project Rags
        if (QueryStringValues.Project > 0)
        {
            InsertProjectRags();
            //BindGrid();
        }
    }
    //bind contry's based on portfolio
    private void SelectCountryPortfolio()
    {
        if (ddlPortfolio.SelectedValue != "Please select...")
        {
            sessionKeys.PortfolioID = int.Parse(ddlPortfolio.SelectedValue);
            sessionKeys.PortfolioName = ddlPortfolio.SelectedItem.Text;
            BindCountry();
            BindCity();
            BindSite();
            //BindProjectGroup();
        }
    }

    #region Project RAGs
    private void BindGrid()
    {
        try
        {
            if (int.Parse(ddlGroup.SelectedValue) > 0)
                GridViewProjectRag.DataSource = RagSectiontoProjectManager.RagSectionProjectSelectAll(QueryStringValues.Project, int.Parse(ddlPortfolio.SelectedValue), int.Parse(ddlGroup.SelectedValue));
            else
                GridViewProjectRag.DataSource = RagSectiontoProjectManager.RagSectionProjectSelectAll(QueryStringValues.Project, int.Parse(ddlPortfolio.SelectedValue));
            GridViewProjectRag.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    private void InsertProjectRags()
    {
        try
        {
            //RagSectiontoProjectEntity rsp = new RagSectiontoProjectEntity();
            //rsp.ProjectReference = QueryStringValues.Project;
            //rsp.PortfolioID = int.Parse(ddlPortfolio.SelectedValue);
            //rsp.ProgrammeID = int.Parse(ddlGroup.SelectedValue);
            //RagSectiontoProjectManager.RagSectionProjectBulkInsert(rsp);
            //BindGrid();
        }

        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    private void InsertMilestoneToTask()
    {
       // RagSectiontoProjectManager.InsertMilestoneToTask(QueryStringValues.Project,int.Parse(ddlGroup.SelectedValue));
    }
    private void SaveAsTasks()
    {
        
    }
    protected void GridViewProjectRag_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewProjectRag.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void GridViewProjectRag_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewProjectRag.EditIndex = -1;
        BindGrid();
    }
    protected void GridViewProjectRag_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //int id = (int)GridViewProjectRag.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0];
            if (e.CommandName == "Update")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                int retval = 0;
                RagSectiontoProjectEntity rsp = new RagSectiontoProjectEntity();
                rsp = RagSectiontoProjectManager.RagSectionProjectSelect(id);
                int i = GridViewProjectRag.EditIndex;

                GridViewRow Row = GridViewProjectRag.Rows[i];
                rsp.ID = id;
                rsp.KeyIssue = ((TextBox)Row.FindControl("txtKeyIssue")).Text;
                rsp.ActualDate = DateTime.Parse(((TextBox)Row.FindControl("txtActualDate")).Text);
                rsp.PlannedDate = DateTime.Parse(((TextBox)Row.FindControl("txtPlannedDate")).Text);
                rsp.LatestDate = DateTime.Now;
                rsp.RAGStatus = (((DropDownList)Row.FindControl("ddlRAGStatus")).SelectedValue)=="0"?"GREEN":((DropDownList)Row.FindControl("ddlRAGStatus")).SelectedValue;
                rsp.ProjectReference = QueryStringValues.Project;
                rsp.PortfolioID = int.Parse(ddlPortfolio.SelectedValue);
                rsp.ProgrammeID = int.Parse(ddlGroup.SelectedValue);
                RagSectiontoProjectManager.RagSectionProjectUpdate(rsp);
                GridViewProjectRag.EditIndex = -1;
                BindGrid();
            }
            else if (e.CommandName == "Delete")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                RagSectiontoProjectManager.RagSectionProjectDelete(id);
                GridViewProjectRag.EditIndex = -1;
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    protected void GridViewProjectRag_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            e.ExceptionHandled = false;
        }
    }

    protected void GridViewProjectRag_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewProjectRag.EditIndex = -1;
        BindGrid();
    }
    protected void GridViewProjectRag_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridViewProjectRag_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];
                if (e.Row.FindControl("lblRAGName1") != null)
                {

                    Label lblRAGName = e.Row.FindControl("lblRAGName1") as Label;
                    lblRAGName.Text = objList[6].ToString();
                    TextBox txtKeyIssue = e.Row.FindControl("txtKeyIssue") as TextBox;
                    txtKeyIssue.Text = objList[4].ToString();
                    TextBox txtPlannedDate = e.Row.FindControl("txtPlannedDate") as TextBox;
                    txtPlannedDate.Text = string.Format("{0:d}", objList[7].ToString().Contains("01/01/1900")?"":objList[7]);
                    //TextBox txtLatestDate = e.Row.FindControl("txtLatestDate") as TextBox;
                    //txtLatestDate.Text = string.Format("{0:d}",objList[8]);
                    TextBox txtActualDate = e.Row.FindControl("txtActualDate") as TextBox;
                    txtActualDate.Text = string.Format("{0:d}",objList[9].ToString().Contains("01/01/1900")?"":objList[9]);
                    DropDownList ddlRAGStatus = e.Row.FindControl("ddlRAGStatus") as DropDownList;
                    ddlRagstatus.SelectedValue = objList[5].ToString();
                }
                else if (e.Row.FindControl("lblRAGName") != null)
                {
                    Label lblRAGName = e.Row.FindControl("lblRAGName") as Label;
                    lblRAGName.Text = objList[6].ToString();
                    Label lblKeyIssue = e.Row.FindControl("lblKeyIssue") as Label;
                    lblKeyIssue.Text = objList[4].ToString();
                    Label lblPlannedDate = e.Row.FindControl("lblPlannedDate") as Label;
                    lblPlannedDate.Text = string.Format("{0:d}",objList[7].ToString().Contains("01/01/1900")?"":objList[7]);
                    //Label lblLatestDate = e.Row.FindControl("lblLatestDate") as Label;
                    //lblLatestDate.Text = string.Format("{0:d}",objList[8]);
                    Label lblActualDate = e.Row.FindControl("lblActualDate") as Label;
                    lblActualDate.Text = string.Format("{0:d}",objList[9].ToString().Contains("01/01/1900")?"":objList[9]);
                    
                    Image imgStatus = e.Row.FindControl("imgStatus") as Image;
                    if (GetImages(objList[5].ToString()) == string.Empty || GetImages(objList[5].ToString()) == "0")
                    {
                        imgStatus.Visible = false;
                    }
                    else
                    {
                        imgStatus.ImageUrl = GetImages(objList[5].ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private string GetImages(string status)
    {
        string returnColour = "";
        if (status != null)
        {
            switch (status.ToUpper())
            {
                case "RED":
                    returnColour = @"~\images\indcate_red.png";
                    break;
                case "GREEN":
                    returnColour = @"~\images\indcate_green.png";
                    break;
                case "AMBER":
                    returnColour = @"~\images\indcate_yellow.png";
                    break;
            }

        }
        return returnColour;
    }
    #endregion
    private void JornalInsert(int projectreference, int UserID)
    {
        try
        {
            ProjectManager.ProjectJournalInsert(projectreference, UserID);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    #region send Mail
    private void SendMail(int project)
    {
        try
        {
            ArrayList ac2pid_list = new ArrayList();
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "DN_GetAC2PID", new SqlParameter("@ProjectReference", project));
            while (dr.Read())
            {
                ac2pid_list.Add(new AC2PID_details(int.Parse(dr["ID"].ToString()), int.Parse(dr["ContractorID"].ToString()), dr["ContractorName"].ToString(), dr["EmailAddress"].ToString()));
            }
            dr.Close();
            ProjectTasklist1.Visible = true;
            foreach (AC2PID_details details in ac2pid_list)
            {
                ProjectTasklist1.setdata(project, details.AC2PID, details.ContractorName);
                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                ProjectTasklist1.RenderControl(htmlWrite);
                Email ToEmail = new Email();
                ToEmail.SendingMail(details.ContractorEmail, "Live Project", htmlWrite.InnerWriter.ToString());
            }
            ProjectTasklist1.Dispose();
            ProjectTasklist1.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    public class AC2PID_details
    {
        int _AC2PID;
        int _ContractorID;
        string _ContractorName;
        string _ContractorEmail;
        public int AC2PID
        {
            get { return _AC2PID; }
            set { _AC2PID = value; }
        }
        public int ContractorID
        {
            get { return _ContractorID; }
            set { _ContractorID = value; }
        }
        public string ContractorName
        {
            get { return _ContractorName; }
            set { _ContractorName = value; }
        }
        public string ContractorEmail
        {
            get { return _ContractorEmail; }
            set { _ContractorEmail = value; }
        }
        public AC2PID_details(int a_AC2PID, int a_ContractorID, string a_ContractorName, string a_ContractorEmail)
        {
            AC2PID = a_AC2PID;
            ContractorID = a_ContractorID;
            ContractorName = a_ContractorName;
            ContractorEmail = a_ContractorEmail;
        }

    }
    #endregion
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnPOnumber_Click(object sender, EventArgs e)
    {
        try
        {
            lblPOPError.Visible = false;
            lblPOMsg.Visible = false;
            mdlPopViewInvoice.Show();
            BindPONubers();
        //ddlRaisedBy.SelectedValue = sessionKeys.UID.ToString();
        SelectFromProjectPlan1(QueryStringValues.Project);
        RetriveData();
       
         }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void RetriveData()
    {
        if (txtCustom1.Text != "")
        {

            projectTaskDataContext projects = new projectTaskDataContext();
            var PD = (from r in projects.ProjectDetails
                      where r.ProjectReference == QueryStringValues.Project
                      select r).ToList().FirstOrDefault();

            PurchaseOrderMgtDataContext POInsert = new PurchaseOrderMgtDataContext();
            var data =( from r in POInsert.Customer_PODatabases
                       where r.PONumber == txtCustom1.Text.Trim() && r.ProjectRef==QueryStringValues.Project
                       select r).ToList().FirstOrDefault();
            
            if (data != null && PD!=null)
            {
                txtPONumber.Text = PD.CustomerReference;
                txtValue.Text = data.Value.ToString();
            }
            else
            {
                if (PD != null)
                {
                    txtCustom1.Text = PD.CustomerReference;
                }
                else
                {
                    txtCustom1.Text = "";
                }
                mdlPopViewInvoice.Show();
            }
        }
    }
    protected void imgSave_Click(object sender, EventArgs e)
    {
        try
        {
            projectTaskDataContext project = new projectTaskDataContext();
            PurchaseOrderMgtDataContext POInsert = new PurchaseOrderMgtDataContext();
           
            var PO1 = (from r in project.Projects
                       where r.ProjectReference == Convert.ToInt32(Request.QueryString["Project"].ToString())
                       select r).ToList().FirstOrDefault();

            var chk = (from r in POInsert.Customer_PODatabases
                       where r.PONumber == txtPONumber.Text && r.ProjectRef == QueryStringValues.Project
                       select r).ToList();

            if (chk.Count == 0)
            {
                Customer_PODatabase insert = new Customer_PODatabase();
                if (Request.QueryString["Project"] != null)
                {
                    if (PO1 != null)
                    {
                        if (PO1.CustomerReference ==string.Empty || PO1.CustomerReference==null)
                        {
                            Project update =
                              project.Projects.Single(P => P.ProjectReference ==
                                 Convert.ToInt32(Request.QueryString["Project"].ToString()));
                            update.CustomerReference = txtPONumber.Text.Trim();
                            update.Custom1 = txtPONumber.Text.Trim();
                            project.SubmitChanges();
                        }
                        //else
                        //{
                        //    Project update =
                        //      project.Projects.Single(P => P.ProjectReference ==
                        //         Convert.ToInt32(Request.QueryString["Project"].ToString()));
                        //    update.CustomerReference = txtPONumber.Text.Trim();
                        //    update.Custom1 = txtPONumber.Text.Trim();
                        //    project.SubmitChanges();
                        //}
                    }


                    insert.CustomerID = int.Parse(ddlPortfolio.SelectedValue);// int.Parse(ddlCustomers.SelectedValue);
                    insert.DateRaised = Convert.ToDateTime(string.IsNullOrEmpty(txtDateRaised.Text) ? DateTime.Now.ToShortDateString() : txtDateRaised.Text);
                    insert.DetailsOfPO = txtDetails.Text;
                    insert.PaymentMethod = 0;// int.Parse(radPaymentType.SelectedValue);
                    insert.PaymentMode = 0;// int.Parse(chkPaymentMode.SelectedValue);
                    insert.POExpiryDate = Convert.ToDateTime(string.IsNullOrEmpty(txtPOExpDate.Text) ? DateTime.Now.ToShortDateString() : txtPOExpDate.Text);
                    insert.PONumber = txtPONumber.Text;
                    insert.RaisedBy = sessionKeys.UID;// int.Parse(ddlRaisedBy.SelectedValue);
                    insert.RelatedToPO = "";// txtRelatedToPO.Text;
                    insert.Value = Convert.ToDouble(string.IsNullOrEmpty(txtValue.Text) ? "0" : txtValue.Text);
                    insert.DDays = Convert.ToDouble(txtDurationDays.Text);
                    insert.ProjectRef = int.Parse(Request.QueryString["Project"].ToString());

                    POInsert.Customer_PODatabases.InsertOnSubmit(insert);
                    POInsert.SubmitChanges();
                    Customer_PODatabase poID = new Customer_PODatabase();
                    int ID = insert.ID;


                }

               
            }
            else
            {
                Project update =
                                project.Projects.Single(P => P.ProjectReference ==
                                   Convert.ToInt32(Request.QueryString["Project"].ToString()));
                update.CustomerReference = txtPONumber.Text.Trim();
                update.Custom1 = txtPONumber.Text.Trim();
                project.SubmitChanges();

                Customer_PODatabase updatePO = POInsert.Customer_PODatabases.Single(P => P.PONumber ==
                     txtPONumber.Text.Trim() && P.ProjectRef == QueryStringValues.Project);
                updatePO.CustomerID = int.Parse(ddlPortfolio.SelectedValue);
                updatePO.DateRaised = Convert.ToDateTime(txtDateRaised.Text);
                updatePO.DetailsOfPO = txtDetails.Text;
                updatePO.PaymentMethod = 0;// int.Parse(radPaymentType.SelectedValue);
                updatePO.PaymentMode = 0;// int.Parse(chkPaymentMode.SelectedValue);
                updatePO.POExpiryDate = Convert.ToDateTime(txtPOExpDate.Text);
                //update.PONumber = txtPONumber.Text;
                //updatePO.RaisedBy = int.Parse(ddlRaisedBy.SelectedValue);
                updatePO.RelatedToPO = "";// txtRelatedToPO.Text;
                updatePO.Value = Convert.ToDouble(txtValue.Text);
                if (Request.QueryString["Project"] != null)
                {

                    updatePO.ProjectRef = int.Parse(Request.QueryString["Project"].ToString());
                }
                updatePO.DDays = Convert.ToDouble(txtDurationDays.Text);
                //POInsert.Customer_PODatabases.InsertOnSubmit(insert);
                POInsert.SubmitChanges();
            }
            if (txtDurationDays.Text == "0")
            {
                lblNote.Visible = true;
                lblNote.Text = "Please note, the PO counter has been enabled for this project and requires you to add the number of days for the PO in order for it to work correctly.";
                BindPONubers();
                mdlPopViewInvoice.Show();
            }
            else
            {
                Response.Redirect("~/WF/Projects/ProjectOverview.aspx?project=" + QueryStringValues.Project);
            }

            //mdlPopViewInvoice.Hide();
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindPOToText()
    {
        projectTaskDataContext project = new projectTaskDataContext();
        var PO = (from r in project.Projects
                  where r.ProjectReference == Convert.ToInt32(Request.QueryString["Project"].ToString())
                  select r).ToList().FirstOrDefault();
        if (PO != null)
        {
            if (PO.CustomerReference != string.Empty || PO.CustomerReference != null)
            {
                txtCustom1.Text = PO.CustomerReference.ToString();
            }
            //else
            //{
            //    txtCustom1.Text = txtPONumber.Text;
            //}
        }
    }
    private void BindPONubers()
    {
        PurchaseOrderMgtDataContext POInsert = new PurchaseOrderMgtDataContext();
         var poList = (from r in POInsert.Customer_PODatabases
                    where r.ProjectRef == QueryStringValues.Project
                    select r).ToList();
         gridPO.DataSource = poList;
         gridPO.DataBind();

    }

    //private void Future()
    //{
    //    try
    //    {
           
    //        var chk = (from r in POInsert.Customer_PODatabases
    //                   where r.PONumber == txtPONumber.Text
    //                   select r).ToList();

    //        if (chk.Count == 0)
    //        {
                

    //        }
    //        else
    //        {
    //            Project update =
    //                        project.Projects.Single(P => P.ProjectReference ==
    //                           Convert.ToInt32(Request.QueryString["Project"].ToString()));
    //            update.CustomerReference = txtPONumber.Text.Trim();
    //            update.Custom1 = txtPONumber.Text.Trim();
    //            project.SubmitChanges();

    //            Customer_PODatabase updatePO = POInsert.Customer_PODatabases.Single(P => P.PONumber ==
    //                 txtPONumber.Text.Trim());
    //          //  updatePO.CustomerID = int.Parse(ddlCustomers.SelectedValue);
    //            updatePO.DateRaised = Convert.ToDateTime(txtDateRaised.Text);
    //            updatePO.DetailsOfPO = txtDetails.Text;
    //            updatePO.PaymentMethod = 0;// int.Parse(radPaymentType.SelectedValue);
    //            updatePO.PaymentMode = 0;// int.Parse(chkPaymentMode.SelectedValue);
    //            updatePO.POExpiryDate = Convert.ToDateTime(txtPOExpDate.Text);
    //            //update.PONumber = txtPONumber.Text;
    //            //updatePO.RaisedBy = int.Parse(ddlRaisedBy.SelectedValue);
    //            updatePO.RelatedToPO = "";// txtRelatedToPO.Text;
    //            updatePO.Value = Convert.ToDouble(txtValue.Text);
    //            if (Request.QueryString["Project"] != null)
    //            {

    //                updatePO.ProjectRef = int.Parse(Request.QueryString["Project"].ToString());
    //            }
    //            updatePO.DDays = Convert.ToDouble(txtDurationDays.Text);
    //            //POInsert.Customer_PODatabases.InsertOnSubmit(insert);
    //            POInsert.SubmitChanges();
    //        }
    //        //mdlPopViewInvoice.Hide();

    //        //Response.Redirect("POJournal.aspx");

    //        //BindPODetails();
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    private void SelectFromProjectPlan1(int pid)
    {
        try
        {


            projectTaskDataContext projects = new projectTaskDataContext();
            var PD = (from r in projects.ProjectDetails
                      where r.ProjectReference == pid
                      select r).ToList().FirstOrDefault();
            PurchaseOrderMgtDataContext POInsert = new PurchaseOrderMgtDataContext();
           
            if (PD != null)
            {

                var data = (from r in POInsert.Customer_PODatabases
                            where r.PONumber == PD.CustomerReference
                            && r.ProjectRef==pid
                            select r).ToList().FirstOrDefault();
                if (data != null)
                {
                    txtDurationDays.Text = string.IsNullOrEmpty(data.DDays.ToString()) ? "0" : data.DDays.ToString();
                    txtDetails.Text = data.DetailsOfPO;
                    txtDateRaised.Text = string.Format("{0:d}", PD.StartDate);

                    txtPOExpDate.Text = string.Format("{0:d}", PD.ProjectEndDate);
                }
                else
                {
                    txtDetails.Text = PD.ProjectTitle;
                    txtDateRaised.Text = string.Format("{0:d}", PD.StartDate);

                    txtPOExpDate.Text = string.Format("{0:d}", PD.ProjectEndDate);

                    DateTime dt1 = DateTime.Parse(txtDateRaised.Text);
                    DateTime dt2 = DateTime.Parse(txtPOExpDate.Text);
                    TimeSpan ts = dt2.Subtract(dt1);
                    txtDurationDays.Text = ts.Days.ToString();
                }
                //lblNote.Visible = true;
                //lblNote.Text = "Please note, the PO counter has been enabled for this project and requires you to add the number of days for the PO in order for it to work correctly.";
                //if (txtDurationDays.Text == "0")
                //{
                //    lblNote.Visible = true;
                //    lblNote.Text = "Please note, the PO counter has been enabled for this project and requires you to add the number of days for the PO in order for it to work correctly.";
                //}
                //ddlCustomers.SelectedValue = PD.Portfolio.ToString();
            } 
              
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "select from project plan ");
        }
    }
    protected void imgSaveCopy_Click(object sender, EventArgs e)
    {
        try
        {
            int enable=0;
            int stat = 0;
            projectTaskDataContext projectDef = new projectTaskDataContext();
            var getValue = (from r in projectDef.ProjectDefaults
                            select r).ToList().FirstOrDefault();
            if (getValue != null)
            {
                enable =Convert.ToInt16(string.IsNullOrEmpty(getValue.ProjectCancel.ToString())?"0":getValue.ProjectCancel.ToString());
            }

            lblMsg.Text = "";
            if ((Request.QueryString["Project"] != null))
            {
                if (!PermissionManager.IsPermitted(Convert.ToInt32(Request.QueryString["Project"].ToString()), Convert.ToInt32(Session["UID"]), PermissionManager.PermissionsTo.ModifyProject))
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "User doesn't have rights to modify project";
                    //Master.ErrorMsg = "User doesn't have rights to modify project";
                    return;
                }
                if ((sessionKeys.SID == 2 || sessionKeys.SID == 3) && (int.Parse(ddlStatus.SelectedValue) == 4) && enable==0)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Sorry but you do not have rights to cancel this project. Please see your system administrator";
                    //Master.ErrorMsg = "Sorry but you do not have rights to cancel this project. Please see your system administrator";
                    return;
                }

                stat = updateProject(Convert.ToInt32(Request.QueryString["Project"].ToString()));
                //update the additional field information
                SavePlaceholderData(QueryStringValues.Project);
                if (stat == 1)
                {
                    //add a entry in Project Journal
                    ProjectJournal(QueryStringValues.Project);
                    //ProjectManager.ProjectJournalInsert(QueryStringValues.Project, sessionKeys.UID);
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {

                    
                }
                //bind Project RAGs
                InsertProjectRags();
                //update project class section
                if (int.Parse(ddlCategory.SelectedValue) > 0)
                    ApplyProjectClassChecklist(QueryStringValues.Project, int.Parse(ddlCategory.SelectedValue));
                //insert into task
                InsertMilestoneToTask();

            }
            else
            {
                insertProject();
                //bind Project RAGs
                InsertProjectRags();
            }
           // AddPO();
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    private void AddPO()
    {
        projectTaskDataContext project = new projectTaskDataContext();
        PurchaseOrderMgtDataContext POInsert = new PurchaseOrderMgtDataContext();

        var PO1 = (from r in project.Projects
                   where r.ProjectReference == Convert.ToInt32(Request.QueryString["Project"].ToString())
                   select r).ToList().FirstOrDefault();

        var chk = (from r in POInsert.Customer_PODatabases
                   where r.PONumber == txtCustom1.Text && r.ProjectRef == QueryStringValues.Project
                   select r).ToList();
        if(txtCustom1.Text!="")
        {
            if (chk.Count == 0)
            {
                Customer_PODatabase insert = new Customer_PODatabase();
                if (Request.QueryString["Project"] != null)
                {
                    if (PO1 != null)
                    {
                        if (PO1.CustomerReference == string.Empty || PO1.CustomerReference == null)
                        {
                            Project update =
                              project.Projects.Single(P => P.ProjectReference ==
                                 Convert.ToInt32(Request.QueryString["Project"].ToString()));
                            update.CustomerReference = txtCustom1.Text.Trim();
                            update.Custom1 = txtCustom1.Text.Trim();
                            project.SubmitChanges();
                        }
                        //else
                        //{
                        //    Project update =
                        //      project.Projects.Single(P => P.ProjectReference ==
                        //         Convert.ToInt32(Request.QueryString["Project"].ToString()));
                        //    update.CustomerReference = txtPONumber.Text.Trim();
                        //    update.Custom1 = txtPONumber.Text.Trim();
                        //    project.SubmitChanges();
                        //}
                    }


                    insert.CustomerID = int.Parse(ddlPortfolio.SelectedValue);// int.Parse(ddlCustomers.SelectedValue);
                    insert.DateRaised = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Now.ToShortDateString() : txtStartDate.Text);
                    insert.DetailsOfPO = txtProjectTitle.Text;
                    insert.PaymentMethod = 0;// int.Parse(radPaymentType.SelectedValue);
                    insert.PaymentMode = 0;// int.Parse(chkPaymentMode.SelectedValue);
                    insert.POExpiryDate = Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? DateTime.Now.ToShortDateString() : txtEndDate.Text);
                    insert.PONumber = txtCustom1.Text;
                    insert.RaisedBy = sessionKeys.UID;// int.Parse(ddlRaisedBy.SelectedValue);
                    insert.RelatedToPO = "";// txtRelatedToPO.Text;
                    insert.Value = 0;//Convert.ToDouble(string.IsNullOrEmpty(txtValue.Text) ? "0" : txtValue.Text);

                    DateTime dt1 = DateTime.Parse(txtStartDate.Text);
                    DateTime dt2 = DateTime.Parse(txtEndDate.Text);
                    TimeSpan ts = dt2.Subtract(dt1);
                    // txtDurationDays.Text = ts.Days.ToString();
                    insert.DDays = Convert.ToDouble(ts.Days.ToString());
                    insert.ProjectRef = int.Parse(Request.QueryString["Project"].ToString());

                    POInsert.Customer_PODatabases.InsertOnSubmit(insert);
                    POInsert.SubmitChanges();
                    Customer_PODatabase poID = new Customer_PODatabase();
                    int ID = insert.ID;


                }
            }


        }
        //else
        //{
        //    Project update =
        //                    project.Projects.Single(P => P.ProjectReference ==
        //                       Convert.ToInt32(Request.QueryString["Project"].ToString()));
        //    update.CustomerReference = txtPONumber.Text.Trim();
        //    update.Custom1 = txtPONumber.Text.Trim();
        //    project.SubmitChanges();

        //    Customer_PODatabase updatePO = POInsert.Customer_PODatabases.Single(P => P.PONumber ==
        //         txtPONumber.Text.Trim() && P.ProjectRef == QueryStringValues.Project);
        //    updatePO.CustomerID = int.Parse(ddlPortfolio.SelectedValue);
        //    updatePO.DateRaised = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Now.ToShortDateString() : txtStartDate.Text); 
        //    updatePO.DetailsOfPO ="";
        //    updatePO.PaymentMethod = 0;// int.Parse(radPaymentType.SelectedValue);
        //    updatePO.PaymentMode = 0;// int.Parse(chkPaymentMode.SelectedValue);
        //    updatePO.POExpiryDate = Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? DateTime.Now.ToShortDateString() : txtEndDate.Text);
        //    //update.PONumber = txtPONumber.Text;
        //    //updatePO.RaisedBy = int.Parse(ddlRaisedBy.SelectedValue);
        //    updatePO.RelatedToPO = "";// txtRelatedToPO.Text;
        //    updatePO.Value = 0;
        //    if (Request.QueryString["Project"] != null)
        //    {

        //        updatePO.ProjectRef = int.Parse(Request.QueryString["Project"].ToString());
        //    }
        //    DateTime dt1 = DateTime.Parse(txtStartDate.Text);
        //    DateTime dt2 = DateTime.Parse(txtEndDate.Text);
        //    TimeSpan ts = dt2.Subtract(dt1);
        //    updatePO.DDays = Convert.ToDouble(ts.Days.ToString());
        //    //POInsert.Customer_PODatabases.InsertOnSubmit(insert);
        //    POInsert.SubmitChanges();
        
    }
    protected void gridPO_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblPOPError.Visible = false;
        gridPO.EditIndex = e.NewEditIndex;
        BindPONubers();
    }
    protected void gridPO_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        lblPOPError.Visible = false;
        gridPO.EditIndex = -1;
        BindPONubers();
    }
    protected void gridPO_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        lblPOPError.Visible = false;
        gridPO.EditIndex = -1;
        BindPONubers();
    }
   
    protected void gridPO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                lblPOPError.Visible = false;
                int CountRow = gridPO.EditIndex;
                GridViewRow row = gridPO.Rows[CountRow];
                TextBox txtDDays = (TextBox)row.FindControl("txtDDays");
                TextBox txtFromDate = (TextBox)row.FindControl("txtFromDateGrid");
                TextBox txtToDate = (TextBox)row.FindControl("txtToDateGrid");
                TextBox txtNotes = (TextBox)row.FindControl("txtDetailsOfPO");
                TextBox txtValues = (TextBox)row.FindControl("txtValues");
                
                DateTime dt1 = DateTime.Parse(txtFromDate.Text);
                DateTime dt2 = DateTime.Parse(txtToDate.Text);
                TimeSpan ts = dt2.Subtract(dt1);
                string s = ts.Days.ToString();
                projectTaskDataContext projectDB = new projectTaskDataContext();
                PurchaseOrderMgtDataContext POInsert = new PurchaseOrderMgtDataContext();
                Customer_PODatabase POUpdate =
                            POInsert.Customer_PODatabases.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                POUpdate.CustomerID = int.Parse(ddlPortfolio.SelectedValue);
                POUpdate.DateRaised = Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? DateTime.Now.ToShortDateString() : txtFromDate.Text); //Convert.ToDateTime(txtFromDate.Text);
                POUpdate.DDays = int.Parse(string.IsNullOrEmpty(txtDDays.Text) ? s : txtDDays.Text);
                POUpdate.DetailsOfPO = txtNotes.Text;
                POUpdate.Value = Convert.ToDouble(string.IsNullOrEmpty(txtValues.Text) ? "0" : txtValues.Text);
                POUpdate.POExpiryDate = Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? DateTime.Now.ToShortDateString() : txtToDate.Text);               
                POInsert.SubmitChanges();
                lblPOMsg.Text = "Updated Successfully";
                lblPOMsg.Visible = true;
                //txtPONumber.Text=
                var getPOdetails = (from r in POInsert.Customer_PODatabases
                                    where r.ID == int.Parse(e.CommandArgument.ToString())
                                    select r).ToList().FirstOrDefault();
                if(getPOdetails!=null)
                {
                    var list = (from r in projectDB.ProjectDetails
                                where r.CustomerReference == getPOdetails.PONumber.ToString()
                                && r.ProjectReference == sessionKeys.Project
                                select r).ToList();
                    if (list.Count != 0)
                    {
                        txtPONumber.Text = getPOdetails.PONumber.ToString();
                        txtPOExpDate.Text = string.Format("{0:d}", getPOdetails.POExpiryDate);
                        txtDateRaised.Text = string.Format("{0:d}", getPOdetails.DateRaised);
                        // txtDDays.Text = getPOdetails.DDays.ToString();
                        txtDurationDays.Text = getPOdetails.DDays.ToString();

                        txtValue.Text = getPOdetails.Value.ToString();
                        txtDetails.Text = getPOdetails.DetailsOfPO.ToString();
                    }
                }
                mdlPopViewInvoice.Show();
                //lblPOMsg.ForeColor = System.Drawing.Color.Green;


            }
            if (e.CommandName == "Delete")
            {
                lblPOMsg.Visible = false;
                //TimeSheetDataContext timeSheet=new TimeSheetDataContext();
                PurchaseOrderMgtDataContext PODelete = new PurchaseOrderMgtDataContext();
                var po = (from r in PODelete.Customer_PODatabases
                          where r.ID == int.Parse(e.CommandArgument.ToString())
                          select r).ToList().FirstOrDefault();



                if (po != null)
                {
                //    var list = (from r in timeSheet.TimesheetEntries
                //                where r.PONumber == po.PONumber
                //                select r).ToList();

                    DataTable dt = new DataTable();
                    dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select * from  TimesheetEntry where PONumber=@PoNumber and ProjectReference=@ProjectReference",
                        new SqlParameter("@PoNumber", po.PONumber),new SqlParameter("@ProjectReference",QueryStringValues.Project)).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        Customer_PODatabase pv = PODelete.Customer_PODatabases.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                        PODelete.Customer_PODatabases.DeleteOnSubmit(pv);
                        PODelete.SubmitChanges();
                        lblPOPError.Visible = false;
                        updateProjectPO(po.PONumber);
                        txtCustom1.Text = "";
                        //Response.Redirect("ProjectOverview.aspx?project=" + QueryStringValues.Project);
                    }
                    else
                    {
                        mdlPopViewInvoice.Show();
                        lblPOPError.Text = "This PO cannot be deleted as time as been booked to it";
                        lblPOPError.Visible = true;
                        lblPOPError.ForeColor = System.Drawing.Color.Red;
                        
                    }
                }


               
            }
            mdlPopViewInvoice.Show();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    //update customer reference in project table

    private void updateProjectPO(int POID)
    {
        projectTaskDataContext pt = new projectTaskDataContext();
        Project update = pt.Projects.Single(P => P.ProjectReference == QueryStringValues.Project);

        PurchaseOrderMgtDataContext PONumber = new PurchaseOrderMgtDataContext();
        var PO = (from r in PONumber.Customer_PODatabases
                  where r.ID == POID && r.ProjectRef == QueryStringValues.Project
                  select r).ToList().FirstOrDefault();
        if (PO != null)
        {
            update.Custom1 = string.Empty;
            update.CustomerReference = string.Empty;
            pt.SubmitChanges();
        }
    }
    private void updateProjectPO(string PONumber)
    {
        projectTaskDataContext pt = new projectTaskDataContext();
       Project update = pt.Projects.Single(P => P.ProjectReference == QueryStringValues.Project);

        //PurchaseOrderMgtDataContext PONumber = new PurchaseOrderMgtDataContext();
        var PO = (from r in pt.Projects
                  where r.CustomerReference == PONumber && r.ProjectReference == QueryStringValues.Project
                  select r).ToList().FirstOrDefault();
        if (PO != null)
        {
            update.Custom1 = string.Empty;
            update.CustomerReference = string.Empty;
            pt.SubmitChanges();
            Response.Redirect("~/WF/Projects/ProjectOverview.aspx?project=" + QueryStringValues.Project);
        }
    }

    protected bool IsExist(string PONumber)
    {
        bool visible = false;
          PurchaseOrderMgtDataContext PODelete = new PurchaseOrderMgtDataContext();
               
                

                    DataTable dt = new DataTable();
                    dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select * from  TimesheetEntry where PONumber=@PoNumber and ProjectReference=@ProjectReference",
                        new SqlParameter("@PoNumber", PONumber), new SqlParameter("@ProjectReference", QueryStringValues.Project)).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        visible = true;
                    }
                
                return visible;
    }


    protected void gridPO_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      
        gridPO.EditIndex = -1;
        BindPONubers();
    }
    protected void gridPO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
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
                    role = Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                    if (role == 3)
                    {

                        vis = false;
                        //  Disable();

                    }
                    role = Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
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
                role = Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {
                   // Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";
                    Disable();

                }
                role = Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
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
        imgSave.Enabled = false;
        imgSaveCopy.Enabled = false;
        btnCategory.Enabled = false;
        btnCity.Enabled = false;
        btnCountry.Enabled = false;
        btnDays.Enabled = false;
        btnOwners.Enabled = false;
        btnPortfolio.Enabled = false;
        btnProgramme.Enabled = false;
        btnSite.Enabled = false;

    }
    #endregion 
    
    #region "Project Goal Section-15Sept2011-Sani" 

    //Bind Project Goal Grid
    private void ProjectGoal()
    {
        DataTable dt = new DataTable();
        dt=SqlHelper.ExecuteDataset(Constants.DBString,CommandType.StoredProcedure,
            "ProjectGoal_Select",new SqlParameter("@ProjectReference",sessionKeys.Project)).Tables[0];
        if (dt.Rows.Count != 0)
        {
            gridProjectGoal.DataSource = dt;
            gridProjectGoal.DataBind();
        }
    }
    //Bind resource ddl
    private void BindResource(DropDownList ddlResource,int setVal)
    {
        try
        {
            int[] id = { 7, 8 };
            UserDataContext user = new UserDataContext();
            var resource = from res in user.Contractors
                           where res.Status == "Active"
                           && !id.Contains(res.SID.Value)
                           orderby res.ContractorName
                           select res;
            ddlResource.DataSource = resource;
            ddlResource.DataTextField = "ContractorName";
            ddlResource.DataValueField = "ID";
            ddlResource.DataBind();
            ddlResource.Items.Insert(0, new ListItem("Please select...", "0"));
            ddlResource.SelectedValue = setVal.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //Bind Roles ddl
    private void BindRoles(DropDownList ddlRoles, int setVal)
    {
        try
        {
            int[] id = { 7, 8 };
            projectTaskDataContext projectsRoles = new projectTaskDataContext();
            var Roles = from res in projectsRoles.ProjectGoalRoles
                          
                          
                           orderby res.RoleName
                           select res;
            ddlRoles.DataSource = Roles;
            ddlRoles.DataTextField = "RoleName";
            ddlRoles.DataValueField = "ID";
            ddlRoles.DataBind();
            ddlRoles.Items.Insert(0, new ListItem("Please select...", "0"));
            ddlRoles.SelectedValue = setVal.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void gridProjectGoal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               // ProjectBOMDetils de = (ProjectBOMDetils)e.Row.DataItem;
                Label lblID = (Label)e.Row.FindControl("lblID");
                Label Label1 = (Label)e.Row.FindControl("lblID");
                
                if (lblID != null)
                {
                    if (int.Parse(lblID.Text) == -99)
                    {
                        e.Row.Visible = false;
                    }
                }

                    DropDownList ddlPGResourceNameGrid = (DropDownList)e.Row.FindControl("ddlPGResourceNameGrid");
                    Label lblPGResourceID = (Label)e.Row.FindControl("lblPGResourceID");
                    if (ddlPGResourceNameGrid != null)
                    {
                        BindResource(ddlPGResourceNameGrid, int.Parse(lblPGResourceID.Text));
                    }
                    DropDownList ddlPGRoleGrid = (DropDownList)e.Row.FindControl("ddlPGRoleGrid");
                    Label lblPGRoleID = (Label)e.Row.FindControl("lblPGRoleID");
                    if (ddlPGRoleGrid != null)
                    {
                        BindRoles(ddlPGRoleGrid, int.Parse(lblPGRoleID.Text));
                    }
                
               

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlPGResourceNameFooter = (DropDownList)e.Row.FindControl("ddlPGResourceNameFooter");
               
                if (ddlPGResourceNameFooter != null)
                {
                    BindResource(ddlPGResourceNameFooter, 0);
                }
                DropDownList ddlPGRoleFooter = (DropDownList)e.Row.FindControl("ddlPGRoleFooter");

                if (ddlPGRoleFooter != null)
                {
                    BindRoles(ddlPGRoleFooter, 0);
                }
               
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridProjectGoal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            projectTaskDataContext InsertProjectGoal= new projectTaskDataContext();
            if (e.CommandName == "Insert")
            {
                DropDownList ddlPGRoleFooter = (DropDownList)gridProjectGoal.FooterRow.FindControl("ddlPGRoleFooter");
                DropDownList ddlPGResourceNameFooter = (DropDownList)gridProjectGoal.FooterRow.FindControl("ddlPGResourceNameFooter");
                TextBox txtNotesPGFooter = (TextBox)gridProjectGoal.FooterRow.FindControl("txtNotesPGFooter");

                if (ddlPGResourceNameFooter.SelectedValue != "0" && ddlPGRoleFooter.SelectedValue != "0")
                {




                    ProjectGoal add = new ProjectGoal();
                    add.Notes = txtNotesPGFooter.Text;
                    add.ResourceID = int.Parse(string.IsNullOrEmpty(ddlPGResourceNameFooter.SelectedValue) ? "0" : ddlPGResourceNameFooter.SelectedValue);
                    add.ProjectReference = QueryStringValues.Project;
                    //add.ResourceName = ddlPGResourceNameFooter.SelectedItem.Text;
                    add.RoleID = int.Parse(string.IsNullOrEmpty(ddlPGRoleFooter.SelectedValue) ? "0" : ddlPGRoleFooter.SelectedValue);
                    InsertProjectGoal.ProjectGoals.InsertOnSubmit(add);
                    InsertProjectGoal.SubmitChanges();

                    lblError.Text = "Inserted Successfully";
                    lblError.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Green;



                }
                else
                {
                    //Please select resource and role type
                    lblError.Text = "Please select resource and role type";
                    lblError.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
                gridProjectGoal.EditIndex = -1;
                ProjectGoal();
            }
            if (e.CommandName == "Update")
            {
                lblError.Visible = false;
                int i = gridProjectGoal.EditIndex;
                GridViewRow row = gridProjectGoal.Rows[i];
                TextBox txtNotesPG = (TextBox)row.FindControl("txtNotesPG");
                DropDownList ddlPGRoleGrid = (DropDownList)row.FindControl("ddlPGRoleGrid");
                DropDownList ddlPGResourceNameGrid = (DropDownList)row.FindControl("ddlPGResourceNameGrid");
                if (ddlPGRoleGrid.SelectedValue != "0" && ddlPGResourceNameGrid.SelectedValue != "0")
                {
                   
                    ProjectGoal Update =
                  InsertProjectGoal.ProjectGoals.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));

                    Update.Notes = txtNotesPG.Text;
                    Update.RoleID = Convert.ToInt32(string.IsNullOrEmpty(ddlPGRoleGrid.SelectedValue) ? "0" : ddlPGRoleGrid.SelectedValue);
                    Update.ResourceID = Convert.ToInt32(string.IsNullOrEmpty(ddlPGResourceNameGrid.SelectedValue) ? "0" : ddlPGResourceNameGrid.SelectedValue);
                    //Update.Mics = Convert.ToDouble(string.IsNullOrEmpty(txtMisc.Text) ? "0" : txtMisc.Text);

                    InsertProjectGoal.SubmitChanges();

                    lblError.Text = "Updated Successfully";
                    lblError.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblError.Text = "Please select resource and role type";
                    lblError.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
                gridProjectGoal.EditIndex = -1;
                ProjectGoal();
            }

            if (e.CommandName == "Delete")
            {
                ProjectMgt.Entity.ProjectGoal pvi = InsertProjectGoal.ProjectGoals.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                InsertProjectGoal.ProjectGoals.DeleteOnSubmit(pvi);
                InsertProjectGoal.SubmitChanges();
                gridProjectGoal.EditIndex = -1;
                ProjectGoal();
            }

            if (e.CommandName == "Add")
            {
                //mdlSimpleDetails.Show();
            }





        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void gridProjectGoal_RowEditing(object sender, GridViewEditEventArgs e)
    {

        gridProjectGoal.EditIndex = e.NewEditIndex;
       // gridProjectGoal.EditIndex = -1;
        ProjectGoal();
    }
    protected void gridProjectGoal_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        lblError.Visible = false;
        gridProjectGoal.EditIndex = -1;
        ProjectGoal();
    }
    protected void gridProjectGoal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lblError.Visible = false;
        gridProjectGoal.EditIndex = -1;
        ProjectGoal();
    }


    protected void gridProjectGoal_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        lblError.Visible = false;
        gridProjectGoal.EditIndex = -1;
        ProjectGoal();
    }
    #endregion

    #region Customer user

    private void BindCustomerUser()
    {
        UserDataContext udt = new UserDataContext();
        PortfolioDataContext pdc = new PortfolioDataContext();
        var PortUserlist = (from q in pdc.AssignedCustomerToPortfolios
                            where q.Portfolio == int.Parse(ddlPortfolio.SelectedValue)
                            select new { q.CustomerID }).ToList();

        var Userlist = (from p in udt.Contractors
                        where p.SID == 7
                        select new { p.ID, p.ContractorName }).ToList();

        var result = (from p in Userlist
                      join pu in PortUserlist on p.ID equals pu.CustomerID
                      select new { p.ID, p.ContractorName }).ToList();

        ddlCustomerUsers.DataSource = result;
        ddlCustomerUsers.DataTextField = "ContractorName";
        ddlCustomerUsers.DataValueField = "ID";
        ddlCustomerUsers.DataBind();
        ddlCustomerUsers.Items.Insert(0, new ListItem("Please select...", "0"));



    }

    #endregion 

    private void BindSalesStaff()
    {
        try
        {
            using (UserDataContext ud = new UserDataContext())
            {
                using (projectTaskDataContext pt = new projectTaskDataContext())
                {
                    var contractorsList = ud.Contractors.Where(c => c.Status.ToLower() == "active").ToList();
                    var salesStaffList = pt.ProjectsSalesStaffs.Select(p => p).ToList();
                    var result = (from p in contractorsList
                                  join s in salesStaffList on p.ID equals s.UserID
                                  orderby p.ContractorName
                                  select new { ID = s.ID, SalesStaff = p.ContractorName }).ToList();

                    ddlSalesStaff.DataSource = result;
                    ddlSalesStaff.DataValueField = "ID";
                    ddlSalesStaff.DataTextField = "SalesStaff";
                    ddlSalesStaff.DataBind();
                    ddlSalesStaff.Items.Insert(0, new ListItem("Please select...", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region Project Journal
    private static void ProjectJournal(int projectreference)
    {
        try
        {
            //project  journal insert
            Project project = ProjectJournalBAL.GetProjectsByReference(projectreference);
            ProjectJournalBAL.InsertProjectJournal(project);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    #region Project Place holder
    TextBox txt = null;
    DropDownList ddl = null;
    Label lbl = null;
    int txtid = 1;
    int ddlid = 1;
    int lblid = 1;
    CustomFieldsBAL cb = null;
    public void BindPlaceholderFields()
    {
        try
        {
            bool Isfirsttime = false;
            if (ViewState["state"] == null)
            {
                ViewState["state"] = 1;
                Isfirsttime = true;
            }
            else
            {
                Isfirsttime = false;
            }

            pb = new ProjectAdditionalInfoBAL();
            List<ProjectAdditionalInfo> pi_new = pb.ProjectAdditionalInfo_SelectByProject(QueryStringValues.Project).ToList();
            
            cb = new CustomFieldsBAL();
            List<CustomField> clist = cb.CustomFields_SelectAll().ToList();
            Table tbl = new Table();
            tbl.EnableViewState = true;
            tbl.Style.Add("width", "100%");
            TableRow tr = new TableRow();
            TableCell td = null;
            int cnt = 0;
            int jcnt = 1;
            int totalCnt = clist.Count();
            foreach (CustomField c in clist)
            {
                var val = pi_new.Where(p => p.CustomFieldID == c.ID).Select(p => p.CustomFieldValue).FirstOrDefault();
                string rval = string.Empty;
                if (val != null)
                    rval = val.ToString();
                
                if (tr == null)
                    tr = new TableRow();

                if (c.FieldType == "textbox")
                {
                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.FieldLable));
                    td.Style.Add("width", "20%");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateTextbox(c.ID.ToString(), rval, Isfirsttime));
                    td.Style.Add("width", "30%");
                    tr.Cells.Add(td);
                }
                else if (c.FieldType == "list")
                {
                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.FieldLable));
                    td.Style.Add("width", "20%");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateDropDown(c.FieldValue, c.ID.ToString(),rval,Isfirsttime));
                    td.Style.Add("width", "30%");
                    tr.Cells.Add(td);
                }
                cnt = cnt + 1;
                if (cnt == 2)
                {
                    tbl.Rows.Add(tr);
                    tr = null;
                    cnt = 0;
                }
                if (jcnt == totalCnt && cnt == 1)
                {
                    tbl.Rows.Add(tr);
                    tr = null;
                }
                jcnt = jcnt + 1;
            }
            ph.Controls.Add(tbl);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public TextBox GenerateTextbox(string id, string setvalue,bool Isfirsttime)
    {
        txt = new TextBox();
        txt.ID = id;
        txt.Width = 200;
        txt.Text = setvalue;
        txt.SkinID = "txt_80";
        txt.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            txt.Text = setvalue;
        txtid = txtid + 1;
        return txt;
    }
    public DropDownList GenerateDropDown(string Items,string id,string setvalue,bool Isfirsttime)
    {
        ddl = new DropDownList();
        ddl.ID = id;
        ddl.Width = 200;
        ddl.SkinID = "ddl_80";
        string[] str = Items.Split(',').ToArray();
        foreach (string s in str)
            ddl.Items.Add(s);
        ddl.Items.Insert(0, new ListItem("Please select...", "0"));
        ddl.EnableViewState = true;
        ddl.SelectedIndexChanged += ddl_SelectedIndexChanged;
        ddl.AutoPostBack = true;
        if (Isfirsttime && !string.IsNullOrEmpty(setvalue))
            ddl.SelectedValue = setvalue;
        ddlid = ddlid + 1;
        return ddl;
    }

    public void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dval = (DropDownList)sender;
        if (dval.SelectedIndex > 0)
        {
            string s = dval.SelectedValue;
        }
    }
    public Label GenerateLable(string lblName)
    {
        lbl = new Label();
        lbl.ID = "lbl" + lblid.ToString();
        lbl.Text = lblName;
        lbl.EnableViewState = true;
        lblid = lblid + 1;
        return lbl;
    }
    ProjectAdditionalInfoBAL pb = null;
    ProjectAdditionalInfo pi = null;
    public void SavePlaceholderData(int Project)
    {
        try
        {
            ViewState["state"] = "2";
            pb = new ProjectAdditionalInfoBAL();
            cb = new CustomFieldsBAL();
            List<CustomField> clist = cb.CustomFields_SelectAll().ToList();

            foreach (CustomField c in clist)
            {
                pi = pb.ProjectAdditionalInfo_SelectByProject(Project).Where(p => p.CustomFieldID == c.ID).FirstOrDefault();
                if (pi == null)
                {
                    pi = new ProjectAdditionalInfo();
                    pi.ProjectReference = Project;
                    if (c.FieldType == "textbox")
                    {
                        var txt = ph.FindControl(c.ID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            pi.CustomFieldValue = txt.Text;
                        }

                    }
                    else
                        if (c.FieldType == "list")
                        {
                            var ddl = ph.FindControl(c.ID.ToString()) as DropDownList;
                            if (ddl != null)
                            {
                                pi.CustomFieldValue = ddl.SelectedValue;
                            }
                        }
                    pi.CustomFieldID = c.ID;
                    pb.ProjectAdditionalInfo_Insert(pi);

                }
                else
                {

                    if (c.FieldType == "textbox")
                    {
                        var txt = ph.FindControl(c.ID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            pi.CustomFieldValue = txt.Text;
                        }

                    }
                    else
                        if (c.FieldType == "list")
                        {
                            var ddl = ph.FindControl(c.ID.ToString()) as DropDownList;
                            if (ddl != null)
                            {
                                pi.CustomFieldValue = ddl.SelectedValue;
                            }
                        }
                    pb.ProjectAdditionalInfo_Update(pi);
                }
            }
        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }

    }
    public void BindPlaceholderData(int Project)
    {
        pb = new ProjectAdditionalInfoBAL();
        List<ProjectAdditionalInfo> pi_new = pb.ProjectAdditionalInfo_SelectByProject(Project).ToList();

        foreach (ProjectAdditionalInfo c in pi_new)
         {
             try
             {
                 if (c.CustomField.FieldType == "textbox")
                 {
                     var txt = ph.FindControl(c.CustomFieldID.ToString()) as TextBox;
                     if (txt != null)
                     {
                         txt.Text = pi.CustomFieldValue;
                     }
                 }
                 else if (c.CustomField.FieldType == "list")
                 {
                     var ddl = ph.FindControl(c.CustomFieldID.ToString()) as DropDownList;
                     if (ddl != null)
                     {
                         ddl.SelectedValue = pi.CustomFieldValue;
                     }
                 }
             }
            catch(Exception ex)
             { LogExceptions.WriteExceptionLog(ex); }
         }
       
    }
    #endregion
}
