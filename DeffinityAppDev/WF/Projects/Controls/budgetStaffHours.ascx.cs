using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using Finance.DAL;
using Finance.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.IO;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using Deffinity.ProgrammeManagers;
using System.Drawing;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using CustomerLogicPermissions;
using Deffinity.ServiceCatalogManager;
using System.Globalization;
public partial class controls_budgetStaffHours : System.Web.UI.UserControl
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    DbCommand cmd;
    Email mail = new Email();
    bool enable = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = ServiceCatalogManager.ProjectBudget_SelectProject(QueryStringValues.Project);
        int statusId = Convert.ToInt32(dt.Rows[0]["ProjectStatusID"]);
        if (statusId != 1) // Pending
            enable = false;

        if (!IsPostBack)
        {
            BindingTextboxses();
            SetDates();
            BindData();
            BindTopSection();
            CommandField();
            BindUserDropDown();
            BindAdditionalHours();
           
        }
    }
    private void SetDates()
    {
        hfStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString();
        int numberOfDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        hfEndDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, numberOfDays).ToString();
        hfMonth.Value = DateTime.Now.ToString("Y");

    }
    public void DisableTotalControlsInPage(bool status)
    {
        try
        {
            foreach (Control c in this.Page.Controls)
            {
                foreach (Control ctrl in c.Controls)
                {
                    if (c is TextBox)
                    {
                        ((TextBox)ctrl).Enabled = status;
                    }
                 
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindData()
    {
        BindPMHoursGrid();
        GetDetails(int.Parse(Request.QueryString["project"].ToString()));
    }
    private void CommandField()
    {

        try
        {
            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {

                    imgUpdate.Visible = false;
                    //  Disable();

                }
                role = Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {
                    imgUpdate.Visible = false;

                    // Disable();

                }

            }
            if (Request.Url.ToString().ToLower().Contains("projecttracker_actuals.aspx"))
            {
                imgUpdate.Visible = false;
                pnlBreakdownHours.Visible = false;
            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    private void BindPMHoursGrid()
    {
        try
        {
            //int[] userTypeIds = new[] { 1, 2, 3 };
            int projectReference = int.Parse(Request.QueryString["project"].ToString());
            using (FinanceModuleDataContext db = new FinanceModuleDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {


                    var resourceList = db.AssignedContractorsToProjects.Where(a => a.ProjectReference == int.Parse(Request.QueryString["project"].ToString())).ToList();
                    var contractorList = ud.Contractors.Where(c => c.Status.ToLower() == "active").ToList();

                    List<PMHours> result = (from r in resourceList
                                            join c in contractorList
                                                on r.ContractorID equals c.ID
                                            //  where !userTypeIds.Contains(Convert.ToInt32(c.SID))
                                            select new PMHours()
                                            {
                                                ID = r.ID,
                                                ProjectReference = Convert.ToInt32(r.ProjectReference),
                                                ResourceName = c.ContractorName,
                                                MaxHoursAllocated = decimal.Parse(string.IsNullOrEmpty(r.MaxHoursAllocated.ToString()) ? "0.00" : r.MaxHoursAllocated.ToString()),
                                                TotalHoursBooked = decimal.Parse(string.IsNullOrEmpty(r.TotalHoursBooked.ToString()) ? "0.00" : r.TotalHoursBooked.ToString()),
                                                NotificationRemainingHours = decimal.Parse(string.IsNullOrEmpty(r.NotificationRemainingHours.ToString()) ? "0.00" : r.NotificationRemainingHours.ToString()),
                                                ContractorID = Convert.ToInt32(r.ContractorID)
                                            }).ToList();
                    List<PMHours> resultA = new List<PMHours>();
                    foreach (var item in result)
                    {
                        resultA.Add(new PMHours() { ID = item.ID, ProjectReference = item.ProjectReference, ContractorID = item.ContractorID, ResourceName = item.ResourceName, SectionType = "Forecast", MaxHoursAllocated = item.MaxHoursAllocated, TotalHoursBooked = item.TotalHoursBooked, NotificationRemainingHours = item.NotificationRemainingHours });
                        //resultA.Add(new PMHours() { ID = item.ID, ProjectReference = item.ProjectReference, ContractorID = item.ContractorID, ResourceName = item.ResourceName, SectionType = "Variation", MaxHoursAllocated = item.MaxHoursAllocated, TotalHoursBooked = item.TotalHoursBooked, NotificationRemainingHours = item.NotificationRemainingHours });
                       // resultA.Add(new PMHours() { ID = item.ID, ProjectReference = item.ProjectReference, ContractorID = item.ContractorID, ResourceName = item.ResourceName, SectionType = "Actuals", MaxHoursAllocated = item.MaxHoursAllocated, TotalHoursBooked = item.TotalHoursBooked, NotificationRemainingHours = item.NotificationRemainingHours });
                    }

                    gvPMHours.DataSource = resultA;
                    gvPMHours.DataBind();

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindUserDropDown()
    {
        int[] userTypeIds = new[] { 1, 2, 3 };
        using (FinanceModuleDataContext db = new FinanceModuleDataContext())
        {
            using (UserDataContext ud = new UserDataContext())
            {
                var resourceList = db.AssignedContractorsToProjects.Where(a => a.ProjectReference == int.Parse(Request.QueryString["project"].ToString())).ToList();
                var contractorList = ud.Contractors.Where(c => c.Status.ToLower() == "active").ToList();
                var userList = (from r in resourceList
                                join c in contractorList
                                    on r.ContractorID equals c.ID
                                orderby c.ContractorName
                                where !userTypeIds.Contains(Convert.ToInt32(c.SID))
                                select new { c.ID, c.ContractorName }).ToList();
                ddlUser.DataSource = userList;
                ddlUser.DataValueField = "ID";
                ddlUser.DataTextField = "ContractorName";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, new ListItem("Please select...", "0"));
            }
        }
    }
    private void BindAdditionalHours()
    {
        try
        {
            int[] userTypeIds = new[] { 1, 2, 3 };
            using (UserDataContext db = new UserDataContext())
            {
                using (FinanceModuleDataContext fd = new FinanceModuleDataContext())
                {
                    var userList = db.Contractors.Where(c => c.Status.ToLower() == "active" && !userTypeIds.Contains(Convert.ToInt32(c.SID))).Select(c => c).ToList();
                    var breakdownList = fd.VariationBreakdownHours.Where(f => f.ProjectReference == int.Parse(Request.QueryString["project"].ToString())).Select(f => f).ToList();
                    var DeviationReportList = fd.DeviationReports.Where(a => a.ProjectReference == int.Parse(Request.QueryString["project"].ToString())).Select(a => a).ToList();

                    var gridResult = (from b in breakdownList
                                      join c in userList on b.UserID equals c.ID
                                      join d in DeviationReportList on b.VariationID equals d.ID
                                      select new { b.ID, b.AdditionalHours, c.ContractorName, d.Description }).ToList();

                    gvBreakdownHours.DataSource = gridResult;
                    gvBreakdownHours.DataBind();

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public string GetTatalHoursOfprojectAndResourceId(int ResourceId, string sectionType, int ContractorID)
    {
        string value = string.Empty;
        try
        {
            int ProjectId = QueryStringValues.Project;
            SqlConnection sqlcon = new SqlConnection(Constants.DBString);
            string cmd = "Project_Totalhours";
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ProjectRef", ProjectId);
            sqlcmd.Parameters.AddWithValue("@ResourceId", ResourceId);
            sqlcmd.Parameters.AddWithValue("@sectionType", sectionType);
            sqlcmd.Parameters.AddWithValue("@ContractorID", ContractorID);
            SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                value = (dr["Hours"].ToString());
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return value;
    }
    private void GetDetails(int projectReference)
    {
        try
        {
            using (FinanceModuleDataContext db = new FinanceModuleDataContext())
            {
                var result = db.ProjectPMHours.Where(p => p.ProjectReference == projectReference).ToList();

                for (int r = 0; r < gvPMHours.Rows.Count; r++)
                {
                    ((TextBox)gvPMHours.Rows[r].FindControl("txtPMHours2")).Text = "0:00";
                    ((TextBox)gvPMHours.Rows[r].FindControl("txtPMHours3")).Text = "0:00";
                    ((TextBox)gvPMHours.Rows[r].FindControl("txtPMHours4")).Text = "0:00";
                    ((TextBox)gvPMHours.Rows[r].FindControl("txtPMHours5")).Text = "0:00";
                    ((TextBox)gvPMHours.Rows[r].FindControl("txtPMHours6")).Text = "0:00";

                    string resourceId = ((Label)gvPMHours.Rows[r].FindControl("lblResourceID")).Text;
                    string sectionType = ((Label)gvPMHours.Rows[r].FindControl("lblSectionType")).Text.Trim();
                    ((Label)gvPMHours.Rows[r].FindControl("lblTotal")).Text = GetTatalHoursOfprojectAndResourceId(int.Parse(resourceId), sectionType,0).ToString().Replace('.',':');//total.ToString("F2")

                 
                        decimal lblCostValue = 0;
                        int Contractorid = Convert.ToInt32(db.AssignedContractorsToProjects.Where(a => a.ID == int.Parse(resourceId)).Select(a => a.ContractorID).FirstOrDefault());
                        lblCostValue = InHouseHoursMints(int.Parse(resourceId), Contractorid);
                        ((Label)gvPMHours.Rows[r].FindControl("lblCost")).Text = string.Format("{0:f2}", lblCostValue);
                    
                    foreach (var item in result)
                    {
                        //    decimal total = Convert.ToDecimal(result.Where(p => p.SectionType == sectionType && p.ResourceID == int.Parse(resourceId)).Select(p => p.PMHours).Sum());
                        //  ((Label)gvPMHours.Rows[r].FindControl("lblTotal")).Text = ChangeHours(GetTatalHoursOfprojectAndResourceId(int.Parse(resourceId), sectionType).ToString("F2"));//total.ToString("F2")

                        for (int x = 2; x < 7; x++)
                        {
                            string headerText = gvPMHours.HeaderRow.Cells[x].Text.Trim();
                            if (headerText != "WC5")
                            {
                                DateTime wcDate = DateTime.Parse(headerText);
                                if (item.ResourceID == int.Parse(resourceId) && item.SectionType == sectionType && item.WCDate == wcDate)
                                {
                                    ((TextBox)gvPMHours.Rows[r].FindControl("txtPMHours" + x)).Text = ChangeHours(item.PMHours.ToString());
                                    break;

                                }
                            }
                        }



                    }

                }



                using (TimeSheetDataContext td = new TimeSheetDataContext())
                {
                    var actualHoursList = (from w in td.TimesheetWCDates
                                           join
                                               t in td.TimesheetEntries on w.WCDateID equals t.WCDateID
                                           where t.ProjectReference == projectReference && t.TimeSheetStatusID != 3
                                           group t by new { t.ContractorID, w.WCDate } into grouping
                                           select new
                                           {
                                               grouping.Key.ContractorID,
                                               grouping.Key.WCDate,
                                               Hours = grouping.Sum(g => g.Hours),
                                               SectionType = "Actuals"


                                           }).ToList();


                    for (int r = 0; r < gvPMHours.Rows.Count; r++)
                    {
                        foreach (var item in actualHoursList)
                        {
                            string contractorID = ((Label)gvPMHours.Rows[r].FindControl("lblContractorID")).Text;
                            string resourceId = ((Label)gvPMHours.Rows[r].FindControl("lblResourceID")).Text;
                            string sectionType = ((Label)gvPMHours.Rows[r].FindControl("lblSectionType")).Text.Trim();
                            if (sectionType == "Actuals")
                            {
                                decimal total = Convert.ToDecimal(actualHoursList.Where(p => p.SectionType == sectionType && p.ContractorID == int.Parse(contractorID)).Select(p => p.Hours).Sum());
                                ((Label)gvPMHours.Rows[r].FindControl("lblTotal")).Text = ChangeHours(total.ToString("F2"));
                            }
                            for (int x = 2; x < 7; x++)
                            {
                                string headerText = gvPMHours.HeaderRow.Cells[x].Text.Trim();
                                if (headerText != "WC5")
                                {
                                    DateTime wcDate = DateTime.Parse(headerText);
                                    if (item.ContractorID == int.Parse(contractorID) && item.SectionType == sectionType && item.WCDate == wcDate)
                                    {
                                        ((TextBox)gvPMHours.Rows[r].FindControl("txtPMHours" + x)).Text = ChangeHours(item.Hours.ToString());
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    private List<DateTime> GetDateList()
    {
        List<DateTime> datesList = new List<DateTime>();

        List<DateTime> AllDates = GetDateRange(DateTime.Now, DateTime.Now.AddDays(30));
        foreach (DateTime date in AllDates)
        {
            if (date.DayOfWeek.ToString() == "Monday")
            {
                datesList.Add(date);
            }
            // datesList.Add(date.AddDays(((int)(date.DayOfWeek) * -1) + 1));                  
        }
        datesList = datesList.Distinct().ToList();
        return datesList;
    }
    protected void gvPMHours_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                List<DateTime> datesList = new List<DateTime>();

                List<DateTime> AllDates = GetDateRange(DateTime.Parse(hfStartDate.Value), DateTime.Parse(hfEndDate.Value));
                foreach (DateTime date in AllDates)
                {
                    if (date.DayOfWeek.ToString() == "Monday")
                    {
                        datesList.Add(date);
                    }
                    // datesList.Add(date.AddDays(((int)(date.DayOfWeek) * -1) + 1));                  
                }
                datesList = datesList.Distinct().ToList();
                int i = 1;
                foreach (var item in datesList)
                {

                    if (i == 1)
                    {
                        TableCell cell1 = e.Row.Cells[2];
                        cell1.Text = item.Date.ToString(Deffinity.systemdefaults.GetDateformat());
                    }
                    else if (i == 2)
                    {

                        TableCell cell2 = e.Row.Cells[3];
                        cell2.Text = item.Date.ToString(Deffinity.systemdefaults.GetDateformat());
                    }
                    else if (i == 3)
                    {

                        TableCell cell3 = e.Row.Cells[4];
                        cell3.Text = item.Date.ToString(Deffinity.systemdefaults.GetDateformat());
                    }
                    else if (i == 4)
                    {

                        TableCell cell4 = e.Row.Cells[5];
                        cell4.Text = item.Date.ToString(Deffinity.systemdefaults.GetDateformat());
                    }
                    else if (i == 5)
                    {

                        TableCell cell5 = e.Row.Cells[6];
                        cell5.Text = item.Date.ToString(Deffinity.systemdefaults.GetDateformat());
                    }
                    i++;
                }
                if (datesList.Count < 5)
                {
                    gvPMHours.Columns[6].Visible = false;
                    tdBackgroundWidth.Attributes.Add("style", "width:356px;background:#D7E3DA;text-align:center;font-weight:bold;");
                    tdBackgroundWidth.InnerHtml = hfMonth.Value;

                }
                else
                {
                    gvPMHours.Columns[6].Visible = true;
                    tdBackgroundWidth.Attributes.Add("style", "width:447px;background:#D7E3DA;text-align:center;font-weight:bold;");
                    tdBackgroundWidth.InnerHtml = hfMonth.Value;
                }



            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblSectionType = (Label)e.Row.FindControl("lblSectionType");
                //for (int x = 2; x < 7; x++)
                //{
                //    string headerText = gvPMHours.HeaderRow.Cells[x].Text.Trim();
                //    if (headerText != "WC5")
                //    {
                //        DateTime wcDate = DateTime.Parse(headerText);
                //        if (wcDate > DateTime.Now)
                //        {
                //            ((TextBox)e.Row.FindControl("txtPMHours" + x)).Enabled = false;
                //        }
                //    }
                //}

                if (lblSectionType.Text.ToLower() == "actuals" || lblSectionType.Text.ToLower() == "forecast")
                {

                    if (lblSectionType.Text.ToLower() == "actuals")
                    {
                        ((TextBox)e.Row.FindControl("txtPMHours2")).Enabled = true;
                        ((TextBox)e.Row.FindControl("txtPMHours3")).Enabled = true;
                        ((TextBox)e.Row.FindControl("txtPMHours4")).Enabled = true;
                        ((TextBox)e.Row.FindControl("txtPMHours5")).Enabled = true;
                        ((TextBox)e.Row.FindControl("txtPMHours6")).Enabled = true;
                        if (CheckingLiveOrNot() == true)
                        {
                            ((TextBox)e.Row.FindControl("tMaxHours")).Enabled = false;
                        }
                    }
                    else
                    {
                        ((TextBox)e.Row.FindControl("txtPMHours2")).Enabled = true;
                        ((TextBox)e.Row.FindControl("txtPMHours3")).Enabled = true;
                        ((TextBox)e.Row.FindControl("txtPMHours4")).Enabled = true;
                        ((TextBox)e.Row.FindControl("txtPMHours5")).Enabled = true;
                        ((TextBox)e.Row.FindControl("txtPMHours6")).Enabled = true;

                       // ((HyperLink)e.Row.FindControl("hlUtilization")).Visible = false;
                        ((LinkButton)e.Row.FindControl("LinkButtonEdit")).Visible = false;
                        ((TextBox)e.Row.FindControl("TMaxHours")).Text = "";
                        ((TextBox)e.Row.FindControl("TMaxHours")).Visible = false;
                        ((Label)e.Row.FindControl("lblTotalHoursBooked")).Text = "";
                        ((Label)e.Row.FindControl("lblnotificationreminingHours")).Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }

    private static List<DateTime> GetDateRange(DateTime StartingDate, DateTime EndingDate)
    {
        if (StartingDate > EndingDate)
        {
            return null;
        }
        List<DateTime> rv = new List<DateTime>();
        DateTime tmpDate = StartingDate;
        do
        {
            rv.Add(tmpDate);
            tmpDate = tmpDate.AddDays(1);
        } while (tmpDate <= EndingDate);
        return rv;
    }
    //public void InsertValues()
    //{
    //    using (projectTaskDataContext pdc = new projectTaskDataContext())
    //    {
    //        ProjectBudgetedHoursStaff projectStaff = new ProjectBudgetedHoursStaff();
    //        projectStaff.ProjectReference = int.Parse(Request.QueryString["project"].ToString());
    //        projectStaff.Hours = Convert.ToDecimal(ChangeToDouble(LblHours.Text.Trim()));
    //        projectStaff.cost = decimal.Parse(LblCost.Text);
    //        pdc.ProjectBudgetedHoursStaffs.InsertOnSubmit(projectStaff);
    //        pdc.SubmitChanges();
    //    }
    //}
    //public void UpdateValues()
    //{
    //    int PRefid = int.Parse(Request.QueryString["project"].ToString());
    //    using (projectTaskDataContext pdc = new projectTaskDataContext())
    //    {
    //        ProjectBudgetedHoursStaff projectStaff = new ProjectBudgetedHoursStaff();
    //        projectStaff = (from a in pdc.ProjectBudgetedHoursStaffs where a.ProjectReference == PRefid select a).FirstOrDefault();
    //        projectStaff.Hours = Convert.ToDecimal(ChangeToDouble(LblHours.Text.Trim()));
    //        projectStaff.cost = decimal.Parse(LblCost.Text);
    //        pdc.SubmitChanges();
    //    }
    //}
    public bool CheckingLiveOrNot()
    {
        bool value;
        int PRefid = int.Parse(Request.QueryString["project"].ToString());
        using (projectTaskDataContext pdc = new projectTaskDataContext())
        {
            var x = (from a in pdc.ProjectDetails where a.ProjectReference == PRefid select a).FirstOrDefault();
            if (x.ProjectStatusID != 1)
            {
                value = true;
            }
            else
            {
                value = false;
            }
        }
        return value;
    }
    public void BindingTextboxses()
    {
        try
        {
            LblHours.Text = "0:00";
            LblCost.Text = "";

            string Cname = GetCurrencyName();
            CultureInfo info = new CultureInfo(Cname);

            SqlConnection sqlcon = new SqlConnection(Constants.DBString);
            string cmd = "TotalMaxHoursAllocatedInProject";
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@pid", QueryStringValues.Project);
            SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                LblHours.Text = Convert.ToDecimal(dr["Thours"]).ToString().Replace('.', ':');
                LblCost.Text = Convert.ToDecimal(dr["TCost"].ToString()).ToString("C", info);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    protected void imgUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            //bool Check = CheckingRecord();
            //if (Check == false)
            //{
            //    //insert
            //    InsertValues();
            //}
            //else
            //{
            //    //update
            //    UpdateValues();
            //}
            List<ProjectPMHour> projectPMHoursInsertList = new List<ProjectPMHour>();
            List<ProjectPMHour> projectPMHoursUpdateList = new List<ProjectPMHour>();
            using (FinanceModuleDataContext db = new FinanceModuleDataContext())
            {
                foreach (GridViewRow item in gvPMHours.Rows)
                {
                    string resourceId = ((Label)item.FindControl("lblResourceID")).Text.Trim();
                    string sectionType = ((Label)item.FindControl("lblSectionType")).Text.Trim();

                    string projectReference = Request.QueryString["project"].ToString();

                    if (sectionType.ToLower() == "forecast")
                    {
                        for (int i = 2; i < 7; i++)
                        {
                            string headerText = gvPMHours.HeaderRow.Cells[i].Text.Trim();
                            if (headerText != "WC5")
                            {
                                DateTime wcDate = DateTime.Parse(headerText);

                                string pmHours = string.IsNullOrEmpty(((TextBox)item.FindControl("txtPMHours" + i)).Text.Trim()) ? "0:00" : ((TextBox)item.FindControl("txtPMHours" + i)).Text.Trim();
                                pmHours = ChangeToDouble(pmHours);
                                var checkExists = db.ProjectPMHours.Where(p => p.ResourceID == int.Parse(resourceId) && p.SectionType == sectionType && p.WCDate == wcDate && p.ProjectReference == int.Parse(projectReference)).FirstOrDefault();
                                if (checkExists != null)
                                {
                                    //Update
                                    checkExists.PMHours = decimal.Parse(pmHours);
                                    projectPMHoursUpdateList.Add(checkExists);
                                }
                                else
                                {
                                    //Insert
                                    ProjectPMHour projectPMHour = new ProjectPMHour();
                                    projectPMHour.ProjectReference = int.Parse(projectReference);
                                    projectPMHour.ResourceID = int.Parse(resourceId);
                                    projectPMHour.SectionType = sectionType;
                                    projectPMHour.PMHours = decimal.Parse(pmHours);
                                    projectPMHour.WCDate = wcDate;
                                    projectPMHoursInsertList.Add(projectPMHour);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 2; i < 7; i++)
                        {

                            string Rid = ((Label)item.FindControl("lblResourceID")).Text.Trim();
                            string headerText = gvPMHours.HeaderRow.Cells[i].Text.Trim();
                            if (headerText != "WC5")
                            {
                                DateTime wcDate = DateTime.Parse(headerText);
                                //  string pmHours = string.IsNullOrEmpty(((TextBox)item.FindControl("tMaxHours" + i)).Text.Trim()) ? "0:00" : ((TextBox)item.FindControl("tMaxHours" + i)).Text.Trim();
                                string pmHours = ((TextBox)item.FindControl("TMaxHours")).Text.Trim();
                                pmHours = ChangeToDouble(pmHours);

                                using (FinanceModuleDataContext fdb = new FinanceModuleDataContext())
                                {
                                    Finance.Entity.AssignedContractorsToProject asp = fdb.AssignedContractorsToProjects.Where(a => a.ID == int.Parse(Rid)).FirstOrDefault();
                                    if (asp != null)
                                    {
                                        asp.MaxHoursAllocated = Convert.ToDecimal(pmHours);
                                        fdb.SubmitChanges();
                                    }
                                }
                            }
                        }
                    }
                }
                //update
                db.SubmitChanges();

                    //Insert
                  db.ProjectPMHours.InsertAllOnSubmit(projectPMHoursInsertList);
                  db.SubmitChanges();
              //  BindTotalhours();
                BindData();
                BindTopSection();

                BindingTextboxses();
                //over budget mail sending
                BudgetAlertMail BMail = new BudgetAlertMail();
                BMail.MailSendingList(QueryStringValues.Project);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    public void BindTotalhours()
    {
        try
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "TotalHourinStaff", new SqlParameter("@pid", QueryStringValues.Project));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgPreviousMonth_Click(object sender, EventArgs e)
    {
        DateTime startDate = DateTime.Parse(hfStartDate.Value).AddMonths(-1);
        int numberOfDays = DateTime.DaysInMonth(startDate.Year, startDate.Month);
        hfStartDate.Value = startDate.ToString();
        hfEndDate.Value = new DateTime(startDate.Year, startDate.Month, numberOfDays).ToString();
        hfMonth.Value = startDate.ToString("Y");
        BindData();

    }
    protected void imgNextMonth_Click(object sender, EventArgs e)
    {
        DateTime startDate = DateTime.Parse(hfStartDate.Value).AddMonths(1);
        int numberOfDays = DateTime.DaysInMonth(startDate.Year, startDate.Month);
        hfStartDate.Value = startDate.ToString();
        hfEndDate.Value = new DateTime(startDate.Year, startDate.Month, numberOfDays).ToString();
        hfMonth.Value = startDate.ToString("Y");
        BindData();


    }
    protected void gvPMHours_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvPMHours.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void gvPMHours_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvPMHours.EditIndex = -1;
        BindData();
    }
    public string ChangeHours(string GetHours)
    {
        string GetActivity = "";
        if (GetHours != "")
        {
            char[] comm1 = { '.', ',' };
            string[] displayTime = GetHours.Split(comm1);
            GetActivity = displayTime[0] + ":" + displayTime[1];
            return GetActivity;
        }
        else
        {
            return "0:00";
        }



    }
    public string ChangeToDouble(string GetHours)
    {
        string GetActivity = "";
        if (GetHours != "")
        {
            char[] comm1 = { ':' };
            string[] displayTime = GetHours.Split(comm1);
            GetActivity = displayTime[0] + "." + displayTime[1];
            return GetActivity;
        }
        else
        {
            return GetActivity;
        }
    }
    protected void gvPMHours_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int index = gvPMHours.EditIndex;
            GridViewRow row = gvPMHours.Rows[index];
            TextBox txtMaxHours = (TextBox)row.FindControl("txtMaxHours");
            TextBox txtNotificationinhours = (TextBox)row.FindControl("txtNotificationHours");
            Label totalHoursBooked = (Label)row.FindControl("lblTotalHoursBooked");
            Label lblName = (Label)row.FindControl("lblName");
            Label lblID = (Label)row.FindControl("lblResourceID");

            //local variables
            decimal notificationinHours = 0;
            decimal usedHours = 0;
            decimal maxHours = 0;

            if (!string.IsNullOrEmpty(txtNotificationinhours.Text.Trim()))
            {
                notificationinHours = Convert.ToDecimal(ChangeToDouble(txtNotificationinhours.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(txtMaxHours.Text.Trim()))
            {
                maxHours = Convert.ToDecimal(ChangeToDouble(txtMaxHours.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(totalHoursBooked.Text.Trim()))
            {
                usedHours = Convert.ToDecimal(ChangeToDouble(totalHoursBooked.Text));
            }

            using (FinanceModuleDataContext db = new FinanceModuleDataContext())
            {
                Finance.Entity.AssignedContractorsToProject asp = db.AssignedContractorsToProjects.Where(a => a.ID == int.Parse(lblID.Text)).FirstOrDefault();
                if (asp != null)
                {
                    asp.MaxHoursAllocated = maxHours;
                    asp.NotificationRemainingHours = notificationinHours;
                    db.SubmitChanges();
                }
            }


            if (notificationinHours > 0)
            {
                if (usedHours >= notificationinHours)
                {
                    //SendMail
                    SendMail(Convert.ToInt32(QueryStringValues.Project.ToString()), txtMaxHours.Text, totalHoursBooked.Text, lblName.Text);
                }
            }

            gvPMHours.EditIndex = -1;
            BindData();
            BindTotalhours();
            BindingTextboxses();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void BindTopSection()
    {
        try
        {
            using (FinanceModuleDataContext db = new FinanceModuleDataContext())
            {
                using (TimeSheetDataContext td = new TimeSheetDataContext())
                {
                    using (UserDataContext ud = new UserDataContext())
                    {
                        int projectReference = int.Parse(Request.QueryString["project"].ToString());
                        var resourceList = db.AssignedContractorsToProjects.Where(a => a.ProjectReference == projectReference).ToList();
                        var contractorList = ud.Contractors.Where(c => c.Status.ToLower() == "active").ToList();
                        int[] userTypeIds = new[] { 1, 2, 3 };
                        //take only other than PM's List
                        var assignedContractorsToProjectsListPMList = (from r in resourceList
                                                                       join c in contractorList on r.ContractorID equals c.ID
                                                                       where !userTypeIds.Contains(Convert.ToInt32(c.SID))
                                                                       select new
                                                                       {
                                                                           r.ID,
                                                                           r.ContractorID,
                                                                           r.ProjectReference
                                                                       }).ToList();
                        //  var assignedContractorsToProjectsListPMList = db.AssignedContractorsToProjects.Where(p => p.ProjectReference == projectReference).ToList();
                        decimal actualValue = 0;
                        decimal forcastValue = 0;
                        decimal originalPMHoursQuotedUnit = 0;
                        decimal originalPMHoursQuotedTotal = 0;
                        decimal variatioPMHoursQuotedTotal = 0;
                        decimal variatioPMHoursQuotedUnit = 0;
                        foreach (var item in assignedContractorsToProjectsListPMList)
                        {
                            var actualSum = td.TimesheetEntries.Where(p => p.ProjectReference == item.ProjectReference && p.ContractorID == item.ContractorID && p.TimeSheetStatusID != 3).Select(p => p.Hours).Sum();
                            //var actualSum = db.ProjectPMHours.Where(p => p.ProjectReference == item.ProjectReference && p.SectionType.ToLower() == "actuals" && p.ResourceID == item.ID).Select(p => p.PMHours).Sum();
                            var forcastSum = db.ProjectPMHours.Where(p => p.ProjectReference == item.ProjectReference && p.SectionType.ToLower() == "forecast" && p.ResourceID == item.ID).Select(p => p.PMHours).Sum();
                            var hoursRate = td.TimeSheetRates.Where(t => t.ContractorsId == item.ContractorID && t.Entrytype == 1).FirstOrDefault(); //1 -Normal hours

                            //var variationPMHours = db.VariationBreakdownHours.Where(v => v.ProjectReference == item.ProjectReference && v.UserID == item.ContractorID).Select(v => v.AdditionalHours).Sum();

                            //take only approved status
                            var variationPMHours = (from d in db.DeviationReports
                                                    join v in db.VariationBreakdownHours on d.ID equals v.VariationID
                                                    where d.ProjectReference == projectReference && d.Approved == true && v.UserID == item.ContractorID
                                                    select v.AdditionalHours).Sum();
                            decimal variationPM = Convert.ToDecimal(variationPMHours);
                            decimal actual = Convert.ToDecimal(actualSum);
                            decimal forcast = Convert.ToDecimal(forcastSum);
                            if (hoursRate != null)
                            {

                                decimal hoursbyRate = Convert.ToDecimal(Convert.ToDecimal(hoursRate.Hourlyrate_Buying) / hoursRate.Minimumdailyhours);

                                actualValue = Convert.ToDecimal(actualValue + (actual * hoursbyRate));
                                forcastValue = Convert.ToDecimal(forcastValue + (forcast * hoursbyRate));

                                //
                                originalPMHoursQuotedUnit = Convert.ToDecimal(originalPMHoursQuotedUnit + forcast);
                                originalPMHoursQuotedTotal = Convert.ToDecimal(originalPMHoursQuotedTotal + (forcast * hoursbyRate));
                                variatioPMHoursQuotedUnit = Convert.ToDecimal(variatioPMHoursQuotedUnit + variationPM);
                                variatioPMHoursQuotedTotal = Convert.ToDecimal(variatioPMHoursQuotedTotal + (variationPM * hoursbyRate));
                            }
                        }


                        string Cname = GetCurrencyName();
                        CultureInfo info = new CultureInfo(Cname);
                        //Top section
                      //  lblActual.Text = actualValue.ToString("C", info);
                        //  lblForecast.Text = forcastValue.ToString("C", info);
                       // lblOriginal.Text = (originalPMHoursQuotedTotal + variatioPMHoursQuotedTotal).ToString("C", info);
                       // lblCostRemaining.Text = ((originalPMHoursQuotedTotal + variatioPMHoursQuotedTotal) - actualValue).ToString("C", info);
                        //Unit Section
                        //  lblOriginalPMHoursQuotedUnit.Text = ChangeHours(originalPMHoursQuotedUnit.ToString("F2"));
                        // lblOriginalPMHoursQuotedTotal.Text = originalPMHoursQuotedTotal.ToString("C", info);
                        lblVariationPMHoursQuotedUnit.Text = ChangeHours(variatioPMHoursQuotedUnit.ToString("F2"));
                        lblVariationPMHoursQuotedTotal.Text = variatioPMHoursQuotedTotal.ToString("C", info);


                        //change
                        lblOriginalPMHoursQuotedUnit.Text = InHouseHours().ToString().Replace('.', ':');
                        lblOriginalPMHoursQuotedTotal.Text = InHouseHoursCost().ToString("C", info);



                        //change
                        lblForecast.Text = InHouseHoursCost().ToString("C", info);
                        lblActual.Text = TotalActualhoursCost().ToString("C", info);
                        lblOriginal.Text = (InHouseHoursCost() + TotalVariationInProject()).ToString("C", info);
                        lblCostRemaining.Text = (InHouseHoursCost() + TotalVariationInProject() - TotalActualhoursCost()).ToString("C", info);
                       

                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public decimal TotalActualhoursCost()
    {
        decimal FinalCost = 0;
        try
        {
            using (FinanceModuleDataContext fdc = new FinanceModuleDataContext())
            {
                using (TimeSheetDataContext tdc = new TimeSheetDataContext())
                {
                    var ActualTimesheetEntries = tdc.TimesheetEntries.Where(a => a.ProjectReference == QueryStringValues.Project).ToList();
                    var contraList = fdc.AssignedContractorsToProjects.Where(a => a.ProjectReference == QueryStringValues.Project).ToList();

                    var Slist = (from a in ActualTimesheetEntries
                                 join b in contraList on a.ContractorID equals b.ContractorID
                                 select new
                                 {
                                     a.ContractorID,
                                     a.ProjectReference,
                                     a.EntryType
                                 }).ToList();
                    foreach (var x in Slist)
                    {
                        FinalCost = FinalCost + ActualCostToContractor(x.EntryType.Value, x.ContractorID.Value);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return FinalCost;
    }
    public decimal TotalVariationInProject()
    {
        decimal FinalCost = 0;
        try
        {
            using (FinanceModuleDataContext fdc = new FinanceModuleDataContext())
            {
                var Vlist = (from a in fdc.DeviationReports
                             join b in fdc.VariationBreakdownHours on a.ID equals b.VariationID
                             where a.ProjectReference == QueryStringValues.Project
                             select b).ToList();
                foreach (var x in Vlist)
                {
                    FinalCost = FinalCost + VariationInproject(1,x.UserID.Value);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return FinalCost;
    }

    public decimal VariationInproject(int EntryType, int ContractorID)
    {
        decimal value = 0;
        try
        {
            int ProjectId = QueryStringValues.Project;
            SqlConnection sqlcon = new SqlConnection(Constants.DBString);
            string cmd = "TotalVariationInproject";
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@VariationId", ProjectId);
            sqlcmd.Parameters.AddWithValue("@EntryType", EntryType);
            sqlcmd.Parameters.AddWithValue("@ContractorID", ContractorID);
            SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                value = Convert.ToDecimal(dr["Cost"]);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return (value);
    }
    public decimal ActualCostToContractor(int EntryType, int ContractorID)
    {
        decimal value = 0;
        try
        {
            int ProjectId = QueryStringValues.Project;
            SqlConnection sqlcon = new SqlConnection(Constants.DBString);
            string cmd = "Project_TotalActualCost";
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ProjectRef", ProjectId);
            sqlcmd.Parameters.AddWithValue("@EntryType", EntryType);
            sqlcmd.Parameters.AddWithValue("@ContractorID", ContractorID);
            SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                value = Convert.ToDecimal(dr["Cost"]);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return (value);
    }


    public decimal InHouseHoursMints(int ResourceId, int ContractorID)
    {
        decimal value = 0;
        try
        {
            int ProjectId = QueryStringValues.Project;
            SqlConnection sqlcon = new SqlConnection(Constants.DBString);
            string cmd = "Project_InHouseHoursMints";
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ProjectRef", ProjectId);
            sqlcmd.Parameters.AddWithValue("@ResourceId", ResourceId);
            sqlcmd.Parameters.AddWithValue("@ContractorID", ContractorID);
            SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                value = Convert.ToDecimal(dr["Cost"]);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return value ;
    }
    public decimal InHouseHoursCost()
    {
        decimal FinalCost = 0;
        try
        {
            using (FinanceModuleDataContext fdc = new FinanceModuleDataContext())
            {
                using (TimeSheetDataContext tdc = new TimeSheetDataContext())
                {
                    var PmHoursList = fdc.ProjectPMHours.Where(a => a.ProjectReference == QueryStringValues.Project).ToList();
                    var ResourceIds = PmHoursList.Select(a => a.ResourceID).Distinct().ToList();
                    foreach (int id in ResourceIds)
                    {
                        int Contractorid = Convert.ToInt32(fdc.AssignedContractorsToProjects.Where(a => a.ID == id).Select(a => a.ContractorID).FirstOrDefault());
                        FinalCost = FinalCost + InHouseHoursMints(id, Contractorid);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return FinalCost;
    }
    public string InHouseHours()
    {
        string value = string.Empty;
        try
        {
            int ProjectId = QueryStringValues.Project;
            SqlConnection sqlcon = new SqlConnection(Constants.DBString);
            string cmd = "Project_InHouseHours";
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ProjectRef", ProjectId);
            SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                value = dr["Hours"].ToString();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return value;
    }
    public string GetCurrencyName()
    {
        string Value = string.Empty;
        Value = "en-GB";//British Pound
        //try
        //{
        //    using (projectTaskDataContext db = new projectTaskDataContext())
        //    {
        //        var plist = db.Projects.Where(o => o.ProjectReference == QueryStringValues.Project).FirstOrDefault();
        //        if (plist.BaseCurrency != null)
        //        {
        //            var cName = db.CurrencyLists.Where(o => o.ID == plist.BaseCurrency).FirstOrDefault();
        //            Value = cName.En_Name.ToString();
        //        }
        //        else
        //        {
        //            Value = "en-GB";//British Pound
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    LogExceptions.WriteExceptionLog(ex);
        //}
        return Value;
    }


    #region mailNotification
    public void SendMail(int ProjectReference, string MaxHrs, string TotalBookedHrs, string ResourceName)
    {

        ResourceTimesheetAler1.Visible = true;

        try
        {
            string projecref_withPrefix = string.Empty;
            string ReciverEmail = string.Empty;
            string RecicerName = string.Empty;

            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select ProjectReferenceWithPrefix,OwnerName,OwnerEmail from V_ProjectDetails where ProjectReference = {0}", ProjectReference));
            while (dr.Read())
            {
                projecref_withPrefix = dr["ProjectReferenceWithPrefix"].ToString();
                ReciverEmail = dr["OwnerEmail"].ToString();
                RecicerName = dr["OwnerName"].ToString();
            }
            dr.Close();
            //Bind usercontrol
            ResourceTimesheetAler1.BindControls(RecicerName, MaxHrs, TotalBookedHrs, ResourceName, projecref_withPrefix);

            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            ResourceTimesheetAler1.RenderControl(htmlWrite);
            Email ToEmail = new Email();
            ToEmail.SendingMail(ReciverEmail, string.Format("Time Alert for Project {0}", projecref_withPrefix), htmlWrite.InnerWriter.ToString());

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            ResourceTimesheetAler1.Visible = false;
        }
    }
    #endregion


    protected void gvPMHours_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPMHours.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void gvPMHours_DataBound(object sender, EventArgs e)
    {

    }


    private void RaiseVariation(string type)
    {
        try
        {
            //Insert variation
            string requesterName = "";
            string requesterEmail = "";
            string approverName = "";
            string approverEmail = "";
            decimal variationCost = 0;
            string projectTitle = "";
            int pref = QueryStringValues.Project;
            using (TimeSheetDataContext td = new TimeSheetDataContext())
            {
                var hoursRate = td.TimeSheetRates.Where(t => t.ContractorsId == int.Parse(ddlUser.SelectedValue) && t.Entrytype == 1).FirstOrDefault(); //1 -Normal hours
                if (hoursRate != null)
                {
                    decimal hoursbyRate = Convert.ToDecimal(Convert.ToDecimal(hoursRate.Hourlyrate_Buying) / hoursRate.Minimumdailyhours);
                    decimal addtionalHours = Convert.ToDecimal(ChangeToDouble(txtAdditionalHours.Text));
                    variationCost = hoursbyRate * addtionalHours;
                }
                using (UserDataContext ud = new UserDataContext())
                {
                    using (projectTaskDataContext pd = new projectTaskDataContext())
                    {

                        var projects = pd.Projects.Where(p => p.ProjectReference == pref).ToList();
                        var contactorList = ud.Contractors.Where(c => c.Status.ToLower() == "active").ToList();
                        var projectDetails = (from p in projects
                                              join c in contactorList on p.OwnerID equals c.ID
                                              select new { p.ProjectReference, p.ProjectTitle, c.ContractorName, c.EmailAddress }).FirstOrDefault();
                        var contractors = ud.Contractors.Where(c => c.ID == sessionKeys.UID).FirstOrDefault();
                        if (contractors != null)
                        {
                            requesterName = contractors.ContractorName;
                            requesterEmail = contractors.EmailAddress;
                        }
                        if (projectDetails != null)
                        {
                            approverName = projectDetails.ContractorName;
                            approverEmail = projectDetails.EmailAddress;
                            projectTitle = projectDetails.ProjectTitle;
                        }

                        cmd = db.GetStoredProcCommand("DN_Deviationreportadd");
                        db.AddInParameter(cmd, "@AC2PID", DbType.Int32, 0);
                        db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, pref);
                        db.AddInParameter(cmd, "@ContractorID", DbType.Int32, sessionKeys.UID);
                        db.AddInParameter(cmd, "@RequesterName", DbType.String, requesterName);
                        db.AddInParameter(cmd, "@Telephone", DbType.String, "");
                        db.AddInParameter(cmd, "@MobileNumber", DbType.String, "");
                        db.AddInParameter(cmd, "@Email", DbType.String, requesterEmail);
                        db.AddInParameter(cmd, "@StartDate", DbType.DateTime, DateTime.Now); // start date and end date not required
                        db.AddInParameter(cmd, "@EndDate", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "@CRSProjectManager", DbType.String, "");
                        db.AddInParameter(cmd, "@BusinessHead", DbType.String, "");
                        db.AddInParameter(cmd, "@BusinessGroup", DbType.String, "");
                        db.AddInParameter(cmd, "@ProjectName", DbType.String, projectTitle);
                        db.AddInParameter(cmd, "@ProjectLocation", DbType.String, "");
                        db.AddInParameter(cmd, "@ScopeofProject", DbType.String, "");
                        db.AddInParameter(cmd, "@DetailedExplanation", DbType.String, "");
                        db.AddInParameter(cmd, "@Justification", DbType.String, "");
                        db.AddInParameter(cmd, "@ProposedCompensation", DbType.String, "");
                        db.AddInParameter(cmd, "@ExpectedRemediationDate", DbType.String, "");
                        db.AddInParameter(cmd, "@DeviationValue", DbType.Double, 0);
                        db.AddInParameter(cmd, "@IndirectCost", DbType.Double, variationCost);
                        db.AddInParameter(cmd, "@Approversname", DbType.String, approverName);
                        db.AddInParameter(cmd, "@ApproversEmail", DbType.String, approverEmail);
                        db.AddInParameter(cmd, "@VariationForcast", DbType.Double, variationCost);
                        db.AddInParameter(cmd, "@VariationCostForcast", DbType.Double, variationCost);
                        db.AddInParameter(cmd, "@Description", DbType.String, txtdescription.Text);
                        db.AddInParameter(cmd, "@AdditionalPMHours", DbType.Double, Convert.ToDecimal(ChangeToDouble(txtAdditionalHours.Text.Trim())));
                        db.AddInParameter(cmd, "@CustomerInstructionNumber", DbType.String, "");
                        db.AddInParameter(cmd, "@Status", DbType.String, "");
                        db.AddInParameter(cmd, "@PercentageComplete", DbType.Double, 0);

                        if (type == "customer")
                            db.AddInParameter(cmd, "@CustomerID", DbType.Int32, ddlCustomer.SelectedValue);
                        else
                            db.AddInParameter(cmd, "@CustomerID", DbType.Int32, 0);
                        db.AddOutParameter(cmd, "@Outval", DbType.Int32, 4);

                        db.ExecuteNonQuery(cmd);
                        int getVariationID = (int)db.GetParameterValue(cmd, "Outval");
                        cmd.Dispose();
                        if (type == "manager")
                        {
                            mail.sendMail(pref, getVariationID, 11);
                        }
                        else
                        {
                            mail.sendMail(pref, getVariationID, 3);
                        }


                        // Insert  variation addtional hours
                        decimal additionalHours = 0;
                        if (!string.IsNullOrEmpty(txtAdditionalHours.Text.Trim()))
                        {
                            additionalHours = Convert.ToDecimal(ChangeToDouble(txtAdditionalHours.Text.Trim()));
                        }
                        using (FinanceModuleDataContext fd = new FinanceModuleDataContext())
                        {
                            DateTime dt = DateTime.Now;

                            VariationBreakdownHour variationBreakdownHour = new VariationBreakdownHour();
                            variationBreakdownHour.ProjectReference = int.Parse(Request.QueryString["project"].ToString());
                            variationBreakdownHour.UserID = int.Parse(ddlUser.SelectedValue);
                            variationBreakdownHour.VariationID = getVariationID;
                            variationBreakdownHour.WCDate = dt.AddDays(((int)(dt.DayOfWeek) * -1) + 1);
                            variationBreakdownHour.AdditionalHours = additionalHours;
                            fd.VariationBreakdownHours.InsertOnSubmit(variationBreakdownHour);
                            fd.SubmitChanges();
                        }
                        BindAdditionalHours();
                        BindTopSection();


                    }
                }
            }
        }
        catch (Exception ex)
        {

            LogExceptions.WriteExceptionLog(ex);
        }
    }



    protected void btnApprovalForManager_Click(object sender, EventArgs e)
    {
        RaiseVariation("manager"); // Approval for Manager
    }
    protected void btnApprovalForCustomer_Click(object sender, EventArgs e)
    {
        mdlCustomerMail.Show();
        BindCustomer();



    }
    private void BindCustomer()
    {
        try
        {
            CLPermissionCS CLPClass = new CLPermissionCS();
            DataTable CTable = CLPClass.SelectPRCustomers(Convert.ToInt32(Request.QueryString["Project"]));
            ddlCustomer.DataSource = CTable;
            ddlCustomer.DataValueField = "ContractorID";
            ddlCustomer.DataTextField = "ContractorName";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgSend_Click(object sender, EventArgs e)
    {

        RaiseVariation("customer"); //Approval for Customer
        lblCustomerMsg.Text = "Mail has been sent successfully to the customer.";
        mdlCustomerMail.Show();
    }
    protected void btn_ApplyDate_OnClick(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDetails = sender as LinkButton;
            GridViewRow row = (GridViewRow)btnDetails.NamingContainer;
            TextBox t2 = (TextBox)row.FindControl("txtPMHours2");
            TextBox t3 = (TextBox)row.FindControl("txtPMHours3");
            TextBox t4 = (TextBox)row.FindControl("txtPMHours4");
            TextBox t5 = (TextBox)row.FindControl("txtPMHours5");
            TextBox t6 = (TextBox)row.FindControl("txtPMHours6");
            if (t2 != null)
            {
                if (t3 != null)
                {
                    t3.Text = t2.Text;
                }
                if (t4 != null)
                {
                    t4.Text = t2.Text;
                }
                if (t5 != null)
                {
                    t5.Text = t2.Text;
                }
                if (t6 != null)
                {
                    t6.Text = t2.Text;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvBreakdownHours_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete1")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                using (FinanceModuleDataContext db = new FinanceModuleDataContext())
                {
                    var breakdownHours = db.VariationBreakdownHours.Where(v => v.ID == id).Select(v => v).FirstOrDefault();
                    if (breakdownHours != null)
                    {

                        // delete addition hours
                        db.VariationBreakdownHours.DeleteOnSubmit(breakdownHours);
                        db.SubmitChanges();

                        // delete variation data
                        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, string.Format("delete from  DeviationReport where ID={0}", breakdownHours.VariationID));




                    }
                }

                BindAdditionalHours();
                BindTopSection();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}