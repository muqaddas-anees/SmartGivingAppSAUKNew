using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using System.Globalization;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using UserMgt.DAL;
using UserMgt.Entity;
using Microsoft.ApplicationBlocks.Data;
using Finance.DAL;
using TimesheetMgt.BAL;

public partial class controls_PTActuals : System.Web.UI.UserControl
{
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    string grdTotal = "";
    int GrandTotalHous = 0;
    decimal grandBuyingTotal = 0;
    decimal grandSellingTotal = 0;
    int ConID = 0;
    decimal Expenses_grandBuyingTotal = 0;
    decimal Expenses_grandSellingTotal = 0;
    decimal ExpensesExternal_grandBuyingTotal = 0;
     protected int getProject = 0;
     
     protected int getUID = 0;
     protected void Page_Load(object sender, EventArgs e)
     {
         getProject = QueryStringValues.Project;
         btndownload.NavigateUrl = "~/WF/Projects/DownloadHandler.ashx?Project=" + getProject + "&UID=" + sessionKeys.UID + "";
         frm_setpage.Attributes.Add("src", GetUrl());
         frm_setpage.Attributes.Add("onLoad", "iFrameHeight()");
         if (!IsPostBack)
         {
             SelectValues(QueryStringValues.Project);
             DisplayTimeSheetGrid(QueryStringValues.Project);
             viewButtonCode_Timeandexpenses();
             SetExternalExpenseCost();
             //viewButtonCode_ExternalExpenses();
         }

         MaterialSummary();
         ActualSectionTotals();

     }

    #region Material summary
    private string GetUrl()
    {
        getProject = QueryStringValues.Project;
        getUID = sessionKeys.UID;

        return ResolveClientUrl(string.Format("/WF/Projects/FileuploadPage.aspx?project={0}&UID={1}", getProject, getUID));
    }
    protected void btnClosePop_Click(object sender, EventArgs e)
    {
        mdlUpload.Hide();
        DisplayTimeSheetGrid(QueryStringValues.Project);
    }
    private void MaterialSummary()
    {
        try
        {

            using (projectTaskDataContext projects = new projectTaskDataContext())
            {
                var materials = (from r in projects.ProjectBOMDetils
                                 where r.ProjectReference == QueryStringValues.Project
                                 && r.QtyReceived != 0
                                 select (r.QtyReceived * (r.Material + r.Mics + r.Labour))).Sum();

                lblTotalMaterialCost.Text = string.Format("{0:N2}", 0);
                lblTotalMaterialSell.Text = string.Format("{0:N2}", 0);
              
                if (materials != null)
                    lblTotalMaterialCost.Text = string.Format("{0:N2}", materials.Value);
             
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    public void CreditlblsBinding()
    {
        try
        {
            using (projectTaskDataContext Pdc = new projectTaskDataContext())
            {
                var x = Pdc.Project_CreditNotes.Where(a => a.ProjectRef == QueryStringValues.Project).ToList();
                if (x != null)
                {
                    lblCreditnote.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", x.Select(a => a.CreditValue.HasValue ? a.CreditValue : 0).Sum());
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void ActualSectionTotals()
    {
        try
        {
            //update credit note
            CreditlblsBinding();
            double t1 = Convert.ToDouble(string.IsNullOrEmpty(lblTotalMaterialCost.Text) ? "0" : lblTotalMaterialCost.Text) + Convert.ToDouble(string.IsNullOrEmpty(lblTotalTimeBuying.Text) ? "0" : lblTotalTimeBuying.Text) + Convert.ToDouble(string.IsNullOrEmpty(lblExpenses_BuyingCost.Text) ? "0" : lblExpenses_BuyingCost.Text) + Convert.ToDouble(string.IsNullOrEmpty(lblExternalExpenses.Text) ? "0" : lblExternalExpenses.Text);
            double t2 = Convert.ToDouble(string.IsNullOrEmpty(lblTotalMaterialSell.Text) ? "0" : lblTotalMaterialSell.Text) + Convert.ToDouble(string.IsNullOrEmpty(tbltotaltimeselling.Text) ? "0" : tbltotaltimeselling.Text) + Convert.ToDouble(string.IsNullOrEmpty(lblExpenses_SellingCost.Text) ? "0" : lblExpenses_SellingCost.Text);
            lblTotalprojectBuyingCost.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", t1);
            lbltotalsellingpriceofproject.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", t2);
            lblprojectactualtotal.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", t1);

            DbCommand cmd = db.GetStoredProcCommand("ProjectFinanacialActualSummary");
            db.AddInParameter(cmd, "@ProjectRef", DbType.Int32, QueryStringValues.Project);
            db.AddInParameter(cmd, "@ContractorID", DbType.Int32, sessionKeys.UID);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {

                while (dr.Read())
                {

                    lblTotalTimeBuying.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["TotalCost"]);
                    tbltotaltimeselling.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["TotalSellingCost"]);
                  
                    //lblTotalTimeBuying
                    //lblprojectactualtotal.Text = string.Format("{0:F2}", dr["TotalCost"]);
                    //lblactualProjectCosttodate.Text = string.Format("{0:F2}", dr["TotalCost"]);
                    lblTotalHours.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["Hours"]);
                }
            }


            try
            {
                var paval = Convert.ToDouble(lblProjectbudget.Text);
                var tpval = Convert.ToDouble(lblprojectactualtotal.Text);
                var caval = Convert.ToDouble(lblCreditnote.Text);
                var tActauls = (tpval - caval);
                var lpValue = (paval - tActauls);
                lblLiveprofitability.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", lpValue);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    #endregion 
  
    protected void btn_ViewTimesheet_Click(object sender, EventArgs e)
    {
        DisplayTimeSheetGrid(QueryStringValues.Project);
    }
    private void DisplayTimeSheetGrid(int projectReference)
    {


        try
        {


            //DbDataAdapter da=new dbd
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            SqlCommand myCommand = new SqlCommand("DN_DisplayTimeSheet", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@ProjectReference", SqlDbType.Int, 32).Value = projectReference;
            if (ddlcustomer_Timesheet.SelectedValue == "")
            {
                myCommand.Parameters.Add("@ResourceID", SqlDbType.Int, 32).Value = 0;
            }
            else
            {
                myCommand.Parameters.Add("@ResourceID", SqlDbType.Int, 32).Value = ddlcustomer_Timesheet.SelectedValue;
            }

            myCommand.Parameters.Add("@StartDate", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_StartDate.Text);
            myCommand.Parameters.Add("@Enddate", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_EndDate.Text) ? "01/01/1900" : txt_EndDate.Text);

            //  myCommand.Parameters.Add("@ContractorID", SqlDbType.Int, 32).Value = Convert.ToInt32(Session["UID"].ToString());

            //Check show costs financials in user profile
            using (UserDataContext db = new UserDataContext())
            {
                ContractorDetail contractorDetail = db.ContractorDetails.Where(c => c.UserID == sessionKeys.UID).Select(c => c).FirstOrDefault();
                if (contractorDetail != null)
                {
                    //Total Buying Cost column
                    GridView4.Columns[12].Visible = (contractorDetail.ShowFinancialCosts == null ? true : Convert.ToBoolean(contractorDetail.ShowFinancialCosts));
                    //Total Selling Price column
                    GridView4.Columns[13].Visible = contractorDetail.ShowFinancialCosts == null ? true : Convert.ToBoolean(contractorDetail.ShowFinancialCosts);
                }

            }



            SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            DataSet ds = new DataSet();
            myadapter.Fill(ds);
            //string _output = myCommand.Parameters["@out"].Value.ToString();
            GridView4.DataSource = ds;
            GridView4.DataBind();


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            // con.Close();

        }


    }
    protected void lnkReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Reports/Timeandexpenses.aspx?Project=" + QueryStringValues.Project);
    }
    protected void GridView4_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView4.EditIndex = -1;

        DisplayTimeSheetGrid(QueryStringValues.Project);
    }
    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {

            if (e.CommandName == "Update")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = GridView4.EditIndex;
                GridViewRow Row = GridView4.Rows[i];
                int EntryType1 = Convert.ToInt32(((DropDownList)Row.FindControl("ddlEntry")).SelectedItem.Value);
                DateTime eDate1 = Convert.ToDateTime(((TextBox)Row.FindControl("txtEndDate")).Text);

                int GetContractorID = Convert.ToInt32(((Label)Row.FindControl("lblcontratorID")).Text);
                //   int GetCustomers = 0;
                int GetCustomers = Convert.ToInt32(((DropDownList)Row.FindControl("ddlsite1")).SelectedItem.Value);
                string PONumber = ((DropDownList)Row.FindControl("ddlPONumber")).SelectedItem.Text.ToString();
                int pref = 0;
                pref = Convert.ToInt32(((DropDownList)Row.FindControl("ddlProjects")).SelectedItem.Value);

                int GetProjectTasks = 0;
                GetProjectTasks = Convert.ToInt32(((DropDownList)Row.FindControl("GetProjectTasks")).SelectedItem.Value);

                double Hours1 = 0;
                if (((TextBox)Row.FindControl("txtHours")).Text != "")
                {
                    string val = ((TextBox)Row.FindControl("txtHours")).Text;
                    char[] comm = { ':' };
                    string[] getva = val.Split(comm);

                    string newval = "";

                    newval = getva[0] + "." + getva[1];


                    Hours1 = Convert.ToDouble(newval);

                }

                string Notes1 = ((TextBox)Row.FindControl("txtNotes")).Text;
                double TotalBuyingCost = 0;
                if (((TextBox)Row.FindControl("txttotalcost")).Text != "")
                {
                    TotalBuyingCost = Convert.ToDouble(((TextBox)Row.FindControl("txttotalcost")).Text);
                }
                double TotalSellingCost = 0;
                if (((TextBox)Row.FindControl("txttotalSellingCost")).Text != "")
                {
                    TotalSellingCost = Convert.ToDouble(((TextBox)Row.FindControl("txttotalSellingCost")).Text);
                }

                try
                {
                    //if the project reference is 0 then update with query string value
                    int te = updatetimesheet(ID, (pref == 0 ? QueryStringValues.Project : pref), GetContractorID, EntryType1, eDate1, Hours1, TotalBuyingCost, TotalSellingCost, Notes1, GetCustomers, GetProjectTasks, PONumber);
                    if (te == 1)
                    {
                        lblacual.Text = Resources.DeffinityRes.Timesheet_Updated;//"Timesheet updated successfully";
                    }
                    DisplayTimeSheetGrid(QueryStringValues.Project);
                    SelectValues(QueryStringValues.Project);
                    UpdatePONumber(QueryStringValues.Project, PONumber);
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                    //Stemp = ex.ToString();
                }
                finally
                {

                }


            }
            else if (e.CommandName == "Insert")
            {
                int entitytype, contractorid, taskid, siteid;
                DateTime date_entered;
                string contractorName;
                double hours = 0, totalsellingprict = 0, totalbuyingprice = 0;
                string notes, PONumber;

                contractorid = Convert.ToInt32(((DropDownList)GridView4.FooterRow.FindControl("ddlContractor_footer")).SelectedValue);
                contractorName = ((DropDownList)GridView4.FooterRow.FindControl("ddlContractor_footer")).SelectedItem.Text;
                date_entered = DateTime.Parse(((TextBox)GridView4.FooterRow.FindControl("txtDate_footer")).Text);
                if (((TextBox)GridView4.FooterRow.FindControl("txtHours_footer")).Text != "")
                {
                    string val = ((TextBox)GridView4.FooterRow.FindControl("txtHours_footer")).Text;
                    char[] comm = { ':' };
                    string[] getva = val.Split(comm);
                    string newval = "";
                    newval = getva[0] + "." + getva[1];
                    hours = Convert.ToDouble(newval);
                }
                entitytype = Convert.ToInt32(((DropDownList)GridView4.FooterRow.FindControl("ddlEntry_footer")).SelectedValue);
                siteid = Convert.ToInt32(((DropDownList)GridView4.FooterRow.FindControl("ddlsite_footer")).SelectedValue);
                taskid = Convert.ToInt32(((DropDownList)GridView4.FooterRow.FindControl("DdlProjectTasks_footer")).SelectedValue);
                notes = ((TextBox)GridView4.FooterRow.FindControl("txtNotes_footer")).Text;
                //totalbuyingprice = double.Parse( ((TextBox)GridView4.FooterRow.FindControl("txttotalcost_footer")).Text);
                //totalsellingprict =  double.Parse(((TextBox)GridView4.FooterRow.FindControl("txttotalSellingCost_footer")).Text);
                PONumber = "";// ((DropDownList)GridView4.FooterRow.FindControl("ddlPONumber_footer")).SelectedItem.Text;

                projectTaskDataContext project = new projectTaskDataContext();
                var PO1 = (from r in project.Projects
                           where r.ProjectReference == Convert.ToInt32(Request.QueryString["Project"].ToString())
                           select r).ToList().FirstOrDefault();
                if (PO1 != null)
                {
                    if (PO1.CustomerReference != string.Empty || PO1.CustomerReference != null)
                    {
                        PONumber = PO1.CustomerReference;
                    }
                }
                InsertTimesheet(QueryStringValues.Project, contractorid,
                    date_entered, entitytype, string.Empty, hours, siteid,
                    taskid, notes, totalbuyingprice, totalsellingprict, PONumber);
                // Send Timesheet alert
                TimesheetAlertBAL.SendTimesheetAlert(QueryStringValues.Project, contractorid, contractorName);
               
                DisplayTimeSheetGrid(QueryStringValues.Project);
                SelectValues(QueryStringValues.Project);
                UpdatePONumber(QueryStringValues.Project, PONumber);

            }
            else if (e.CommandName == "del")
            {
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "delete from Timesheetentry where ID = @ID", new SqlParameter("@ID", int.Parse(e.CommandArgument.ToString())));

                DisplayTimeSheetGrid(QueryStringValues.Project);
                SelectValues(QueryStringValues.Project);
            }

           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    #region Timesheet Insert

    public void InsertTimesheet(int ProjectReference, int ResourceID, DateTime Dateenter, int entity, string Activity, double Hours, int Siteid, int ProjectTaskID, string Notes, double TotalbuyingCost, double TotalSelling, string PONumber)
    {
        try
        {
            //@PONumber varchar(100)=null        
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Timesheetentry_CasualLabourInsert",
                new SqlParameter("@ProjectReference", ProjectReference),
                new SqlParameter("@ResourceID", ResourceID),
                new SqlParameter("@entryid", entity),
                new SqlParameter("@Timesheetdate", Dateenter),
                new SqlParameter("@hours", Hours),
                new SqlParameter("@TotalbuyingCost", TotalbuyingCost),
                new SqlParameter("@TotalSelling", TotalSelling),
                new SqlParameter("@notes", Notes),
                new SqlParameter("@SiteID", Siteid),
                new SqlParameter("@ProjectTaskID", ProjectTaskID),
                new SqlParameter("@ApproverID", sessionKeys.UID)

                );

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion
    private void UpdatePONumber(int projectRef, string PONumber)
    {

        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "PONumberUpdate_Timesheet",
            new SqlParameter("@ProjectReference", projectRef), new SqlParameter("@PONumber", PONumber));



    }
    protected void GridView4_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView4.EditIndex = e.NewEditIndex;
        DisplayTimeSheetGrid(QueryStringValues.Project);
    }
    protected void GridView4_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView4.EditIndex = -1;

        DisplayTimeSheetGrid(QueryStringValues.Project);
    }
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
    public string ChangeHoues(string GetHours)
    {
        string GetActivity = "";
        char[] comm1 = { '.', ',' };
        string[] displayTime = GetHours.Split(comm1);


        GetActivity = displayTime[0] + ":" + displayTime[1];



        return GetActivity;
    }
    protected void ddlEntryTandE_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int i = GridView5.EditIndex;
            GridViewRow Row = GridView5.Rows[i];
            int test = Convert.ToInt32(((DropDownList)Row.FindControl("ddlEntryTandE")).SelectedValue);

            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select BuyingPrice,SellingPrice from ExpensesentryType where ID = {0}", test));
            string GetUnitPrice = "";
            string SellingPrice = "";
            while (dr.Read())
            {
                if ((dr["BuyingPrice"].ToString() == "") || (dr["BuyingPrice"].ToString() == "0"))
                {
                    GetUnitPrice = "0.00";
                }
                else
                {
                    GetUnitPrice = dr["BuyingPrice"].ToString();
                }
                if ((dr["SellingPrice"].ToString() == "") || (dr["SellingPrice"].ToString() == "0"))
                {
                    SellingPrice = "0.00";
                }
                else
                {
                    SellingPrice = dr["SellingPrice"].ToString();
                }
            }
            dr.Close();
            ((TextBox)Row.FindControl("txtUnitPriceTandE")).Text = GetUnitPrice;
            ((TextBox)Row.FindControl("txtSellingTandE")).Text = SellingPrice;
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }


    }
    protected void ddlEntryTandE_footer_SelectedIndexChanged(object sender, EventArgs e)
    {

        //DropDownList ddlEntry = ((DropDownList)(GridView2.FooterRow.FindControl(GridView2.FindControl("ddlEntry"))));
        string test = ((DropDownList)GridView5.FooterRow.FindControl("ddlEntryTandE_footer")).Text;
        SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select BuyingPrice,SellingPrice from ExpensesentryType where ID = {0}", test));
        string GetUnitPrice = "";
        string SellingPrice = "";
        while (dr.Read())
        {
            if ((dr["BuyingPrice"].ToString() == "") || dr["BuyingPrice"].ToString() == "0")
            {
                GetUnitPrice = "0.00";
            }
            else
            {
                GetUnitPrice = dr["BuyingPrice"].ToString();
            }

            if ((dr["SellingPrice"].ToString() == "") || dr["SellingPrice"].ToString() == "0")
            {
                SellingPrice = "0.00";
            }
            else
            {
                SellingPrice = dr["SellingPrice"].ToString();
            }

        }
        dr.Close();
        ((TextBox)GridView5.FooterRow.FindControl("txtUnitPrice_footerexpenses")).Text = GetUnitPrice;
        ((TextBox)GridView5.FooterRow.FindControl("txtSelling_footerexpenses")).Text = SellingPrice;


    }
    private DataTable GetProjectList(int resourceid)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimeSheet_ProjectTile", new SqlParameter("@ContractorID", resourceid)).Tables[0];
    }
    private DataTable GetProjectTaskList(int project, int resourceid)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_GetProjectTaks", new SqlParameter("@ProjectReference", project), new SqlParameter("@contractorID", resourceid)).Tables[0];
    }
    private DataTable GetPONumbers(int ProjectRef)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select distinct PONumber,ID from customer_podatabase where projectref=@ProjectReference order by PONumber", new SqlParameter("@ProjectReference", ProjectRef)).Tables[0];
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];

                int GettingTotalhours = 0;
                decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Hours"));
                string getTotsl = DataBinder.Eval(e.Row.DataItem, "Hours").ToString();
                char[] getvalue1 = { '.' };

                // char[] comm1 = { '.' };
                string[] hours = getTotsl.Split(getvalue1);

                GettingTotalhours = (Convert.ToInt32(hours[0]) * 60) + Convert.ToInt32(hours[1]);
                GrandTotalHous = GrandTotalHous + GettingTotalhours;

                int hours1 = GrandTotalHous / 60;
                int minuts = GrandTotalHous % 60;
                string s = hours1.ToString("00") + ":" + minuts.ToString("00");
                grdTotal = s;
                //lblTotalHours.Text = grdTotal.ToString();
                decimal rowBuyingTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalCost"));
                grandBuyingTotal = grandBuyingTotal + rowBuyingTotal;
                //lblTotalTimeBuying.Text = string.Format("{0:#.00}", grandBuyingTotal);
                decimal rowSellingTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_SellingCost"));
                grandSellingTotal = grandSellingTotal + rowSellingTotal;
                //tbltotaltimeselling.Text = string.Format("{0:#.00}", grandSellingTotal);


                if (objList[0].ToString() == "-99")
                {
                    e.Row.Visible = false;
                }
                else
                {

                    if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
                    {
                        DropDownList ddlProjectTasks = e.Row.FindControl("GetProjectTasks") as DropDownList;
                        DropDownList ddlProject = e.Row.FindControl("ddlProjects") as DropDownList;
                        Label lblRID = e.Row.FindControl("lblRid") as Label;
                        DropDownList ddlsite1 = e.Row.FindControl("ddlsite1") as DropDownList;
                        DropDownList ddlPONumber = e.Row.FindControl("ddlPONumber") as DropDownList;

                        if (ddlProjectTasks != null)
                        {
                            //lblRid
                            ddlProject.DataSource = GetProjectList(int.Parse(lblRID.Text));
                            ddlProject.DataTextField = "ProjectTitle";
                            ddlProject.DataValueField = "ProjectReference";
                            ddlProject.DataBind();

                            Label contractorID = e.Row.FindControl("lblcontratorID") as Label;
                            ConID = Convert.ToInt32(contractorID.Text);
                            int i = QueryStringValues.Project;

                            ddlProjectTasks.DataSource = GetProjectTaskList(QueryStringValues.Project, int.Parse(lblRID.Text));
                            ddlProjectTasks.DataTextField = "ProjectTask";
                            ddlProjectTasks.DataValueField = "TaskID";
                            ddlProjectTasks.DataBind();
                            Label lblPONumber1 = e.Row.FindControl("lblPONumber1") as Label;

                            ddlPONumber.DataSource = GetPONumbers(QueryStringValues.Project);
                            ddlPONumber.DataTextField = "PONumber";
                            ddlPONumber.DataValueField = "PONumber";
                            ddlPONumber.DataBind();
                            ddlPONumber.Items.Insert(0, new ListItem(" ", "0"));



                            try
                            {
                                ddlProject.SelectedValue = objList[4].ToString();
                                ddlsite1.SelectedValue = objList[9].ToString();
                                ddlProjectTasks.SelectedValue = objList[11].ToString();
                                ddlPONumber.SelectedValue = objList[13].ToString();
                            }
                            catch (Exception ex)
                            {
                                ddlProjectTasks.SelectedIndex = 0;
                                // ddlsiteProject.SelectedIndex = 0;
                                LogExceptions.WriteExceptionLog(ex);
                            }
                        }
                    }
                }




            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {

                DropDownList ddlProjectTasks_footer = e.Row.FindControl("DdlProjectTasks_footer") as DropDownList;

                ddlProjectTasks_footer.Items.Insert(0, new ListItem("Please select", "0"));

                DropDownList ddlPONumber_footer = e.Row.FindControl("ddlPONumber_footer") as DropDownList;


                ddlPONumber_footer.DataSource = GetPONumbers(QueryStringValues.Project);
                ddlPONumber_footer.DataTextField = "PONumber";
                ddlPONumber_footer.DataValueField = "PONumber";
                ddlPONumber_footer.DataBind();
                ddlPONumber_footer.Items.Insert(0, new ListItem("Please select", "0"));
                //ddlPONumber.Items.Insert(0, new ListItem("Please select...", "0"));

                //Display project reference
                Label lblProjectTitle = (Label)e.Row.FindControl("lblProjectTitle_footer");
                lblProjectTitle.Text = Deffinity.Bindings.DefaultDatabind.GetProjectTitle(QueryStringValues.Project);

                Label lbl = (Label)e.Row.FindControl("AmountTotal");
                TextBox txtDate_footer = (TextBox)e.Row.FindControl("txtDate_footer");
                txtDate_footer.Text = DateTime.Now.ToShortDateString();
                Label lbltotpagehours = (Label)e.Row.FindControl("lblTothrspage");
                lbltotpagehours.Text = grdTotal.ToString();
                //lbl.Text = grdTotal.ToString();
                //lblactualhours.Text = string.Format("{0:#.00}", grdTotal.ToString());
                DbCommand cmd = db.GetStoredProcCommand("ProjectFinanacialActualSummary");
                db.AddInParameter(cmd, "@ProjectRef", DbType.Int32, QueryStringValues.Project);
                db.AddInParameter(cmd, "@ContractorID", DbType.Int32, sessionKeys.UID);
                //lbl.Text = grdTotal.ToString();
               // lblactualhours.Text = grdTotal;

                Label lb2 = (Label)e.Row.FindControl("lbltotalcost_BuyingCost");

                //lb2.Text = string.Format("{0:#.00}", grandBuyingTotal);
                Label lb3 = (Label)e.Row.FindControl("lbltotalcost_SellingCost");

                //lb3.Text = string.Format("{0:#.00}", grandSellingTotal);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {

                    while (dr.Read())
                    {

                        lblTotalTimeBuying.Text = string.Format("{0:F2}", dr["TotalCost"]);
                        tbltotaltimeselling.Text = string.Format("{0:F2}", dr["TotalSellingCost"]);
                       // lblactualhours.Text = string.Format("{0:F2}", dr["Hours"]);
                        lbl.Text = string.Format("{0:F2}", dr["Hours"]);
                        lb2.Text = string.Format("{0:N2}", dr["TotalCost"]);
                        lb3.Text = string.Format("{0:N2}", dr["TotalSellingCost"]);
                        lblTotalHours.Text = string.Format("{0:F2}", dr["Hours"]);
                    }
                }

                //double t1 = Convert.ToDouble(string.IsNullOrEmpty(lblTotalMaterialCost.Text) ? "0" : lblTotalMaterialCost.Text) + Convert.ToDouble(string.IsNullOrEmpty(lblTotalTimeBuying.Text) ? "0" : lblTotalTimeBuying.Text) + Convert.ToDouble(string.IsNullOrEmpty(lblExpenses_BuyingCost.Text) ? "0" : lblExpenses_BuyingCost.Text) + Convert.ToDouble(string.IsNullOrEmpty(lblExternalExpenses.Text) ? "0" : lblExternalExpenses.Text);
                //lblactualProjectCosttodate.Text = string.Format("{0:#.00}", lb3.Text);

                //added on  18th march 2011
                GrandTotalHous = 0;
                grandSellingTotal = 0;
                grandBuyingTotal = 0;
                grdTotal = "";
                //lblTotalHours.Text = grdTotal.ToString();



            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView4.PageIndex = e.NewPageIndex;
        DisplayTimeSheetGrid(QueryStringValues.Project);
    }
    public int updatetimesheet(int ID, int projectReference, int ResourceID, int EntryType, DateTime Date, double Hours, double TotalBuyingCost, double TotalSellingCost, string Notes, int GetCustomers, int GetProjectTasks, string PONumber)
    {


        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd = db.GetStoredProcCommand("DN_TimeSheetUpdateByprojectreference");
        db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
        db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, projectReference);
        db.AddInParameter(cmd, "@ResourceID", DbType.Int32, ResourceID);
        db.AddInParameter(cmd, "@Timesheetdate", DbType.DateTime, Date);
        db.AddInParameter(cmd, "@entryid", DbType.Int32, EntryType);
        db.AddInParameter(cmd, "@hours", DbType.Double, Hours);
        db.AddInParameter(cmd, "@TotalbuyingCost", DbType.Double, TotalBuyingCost);
        db.AddInParameter(cmd, "@TotalSelling", DbType.Double, TotalSellingCost);
        db.AddInParameter(cmd, "@notes", DbType.String, Notes);
        db.AddInParameter(cmd, "@SiteID", DbType.String, GetCustomers);
        db.AddInParameter(cmd, "@ProjectTaskID", DbType.String, GetProjectTasks);
        db.AddInParameter(cmd, "@PONumber", DbType.String, PONumber);
        //Convert.ToDouble(txthours.Text)

        int getVal = Convert.ToInt32(db.ExecuteNonQuery(cmd));
        cmd.Dispose();
        return getVal;

    }
    private void SelectValues(int pref)
    {
        try
        {
            // lblactualProjectvaluetodate

            DbCommand cmd = db.GetStoredProcCommand("DN_SelectProject");
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, pref);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
               
                while (dr.Read())
                {
                   
                    if (dr["BudgetaryCost"].ToString() == "0")
                    {
                        lblProjectbudget.Text = "0.00";

                    }
                    else
                    {
                        lblProjectbudget.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["BudgetaryCost"]);
                    }

                    if (dr["ActualCost"].ToString() == "0")
                    {
                        lblprojectactualtotal.Text = "0.00";
                    }
                    else
                    {
                        lblprojectactualtotal.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["ActualCost"]);
                    }

                   
                
                }
            }

            cmd.Dispose();
           
            //update the Pricing values before dispaly
           // UpdateProfit(QueryStringValues.Project, getData.getDouble(txtProjectvalue.Text.Trim()), getData.getDouble(txtBuyingPrice.Text.Trim()), getData.getDouble(txtProjectForcast.Text.Trim()), getData.getDouble(lblprojectactualtotal.Text.Trim()), getData.getDouble(txtAccrualsPri.Text.Trim()), getData.getDouble(txtAccrualsCurt.Text.Trim()), getData.getDouble(txtAccuralsMonth.Text.Trim()), getData.getDouble(txtProjectCostForecast.Text.Trim()));


        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    protected void ddlContractor_footer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlContractor_footer = (DropDownList)GridView4.FooterRow.FindControl("ddlContractor_footer");
            DropDownList GetEditProjecttasks = (DropDownList)GridView4.FooterRow.FindControl("DdlProjectTasks_footer");

            GetEditProjecttasks.DataSource = GetProjectTaskList(QueryStringValues.Project, int.Parse(ddlContractor_footer.SelectedValue));
            GetEditProjecttasks.DataTextField = "ProjectTask";
            GetEditProjecttasks.DataValueField = "TaskID";
            GetEditProjecttasks.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            int index = GridView4.EditIndex;
            GridViewRow row = GridView4.Rows[index];

            DropDownList ddl = (DropDownList)row.FindControl("ddlProjects");
            string GetValue = ddl.SelectedValue;

            //DropDownList ddlprojects1 = (DropDownList)GridView1.Rows[0].FindControl("ddlTitle");
            DropDownList GetEditProjecttasks = (DropDownList)row.FindControl("GetProjectTasks");
            Label lblRID = row.FindControl("lblRid") as Label;
            //DropDownList GetEditSite = (DropDownList)row.FindControl("ddlsite1");
            //select ID,ItemDescription from ProjectTaskItems where ProjectReference = 2

            GetEditProjecttasks.DataSource = GetProjectTaskList(int.Parse(GetValue), int.Parse(lblRID.Text));
            GetEditProjecttasks.DataTextField = "ProjectTask";
            GetEditProjecttasks.DataValueField = "TaskID";
            GetEditProjecttasks.DataBind();

            //DataTable DT_dropdown = new DataTable();
            //SqlCommand cmd = new SqlCommand("DN_TimesheetSite_Customer1", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@ProjectReference", SqlDbType.Int, 4).Value = Convert.ToInt32(GetValue);

            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(DT_dropdown);

            //GetEditSite.DataSource = DT_dropdown;
            //GetEditSite.DataTextField = "Site";
            //GetEditSite.DataValueField = "SiteID";
            //GetEditSite.DataBind();



        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridView5_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView5.EditIndex = -1;
        viewButtonCode_Timeandexpenses();
        SelectValues(QueryStringValues.Project);
    }
    protected void GridView5_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView5.EditIndex = e.NewEditIndex;
        viewButtonCode_Timeandexpenses();
    }
    protected void GridView5_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView5.EditIndex = -1;

        viewButtonCode_Timeandexpenses();
        SelectValues(QueryStringValues.Project);
    }
    protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ExpensesType")
        {
            Response.Redirect("~/WF/Admin/AdminDropDown.aspx?type=Finance&Projet=" + QueryStringValues.Project, false);
        }

        if (e.CommandName == "Cancel_ExterNalFooter")
        {
            ((TextBox)GridView5.FooterRow.FindControl("txt_expenseDate")).Text = "";

            ((TextBox)GridView5.FooterRow.FindControl("txt_footerNotes")).Text = "";
            ((DropDownList)GridView5.FooterRow.FindControl("ddlEntryTandE_footer")).SelectedIndex = 0;
            //((DropDownList)GridView5.FooterRow.FindControl("ddlTitle_FooterExpenses")).SelectedIndex = 0;
            ((TextBox)GridView5.FooterRow.FindControl("txtQty_footerexpenses")).Text = "";

        }


        if (e.CommandName == "Insert_Footer")
        {

            double Qty = 0;
            double UnitPrice = 0;
            string notes = "";
            int ProjectTitel = 0;
            int EntryType = 0;

            double sellingPrice = 0;


            DateTime TESTdate = Convert.ToDateTime(((TextBox)GridView5.FooterRow.FindControl("txt_expenseDate")).Text);

            if (((TextBox)GridView5.FooterRow.FindControl("txtQty_footerexpenses")).Text != "")
            {
                Qty = Convert.ToDouble(((TextBox)GridView5.FooterRow.FindControl("txtQty_footerexpenses")).Text);
            }

            if (((TextBox)GridView5.FooterRow.FindControl("txtUnitPrice_footerexpenses")).Text != "")
            {
                UnitPrice = Convert.ToDouble(((TextBox)GridView5.FooterRow.FindControl("txtUnitPrice_footerexpenses")).Text);
            }

            if (((TextBox)GridView5.FooterRow.FindControl("txtSelling_footerexpenses")).Text != "")
            {
                sellingPrice = Convert.ToDouble(((TextBox)GridView5.FooterRow.FindControl("txtSelling_footerexpenses")).Text);
            }


            if (((TextBox)GridView5.FooterRow.FindControl("txt_footerNotes")).Text != "")
            {
                notes = ((TextBox)GridView5.FooterRow.FindControl("txt_footerNotes")).Text;
            }
            if (((DropDownList)GridView5.FooterRow.FindControl("ddlEntryTandE_footer")).Text != "")
            {
                EntryType = Convert.ToInt32(((DropDownList)GridView5.FooterRow.FindControl("ddlEntryTandE_footer")).SelectedValue);
            }
            //if (((DropDownList)GridView5.FooterRow.FindControl("ddlTitle_FooterExpenses")).Text != "")
            //{
            //    ProjectTitel = Convert.ToInt32(((DropDownList)GridView5.FooterRow.FindControl("ddlTitle_FooterExpenses")).SelectedValue);
            //}
            ProjectTitel = Convert.ToInt32(QueryStringValues.Project);

            //
            int timeandexpenses;
            timeandexpenses = insertExpenses(TESTdate, ProjectTitel, EntryType, Qty, UnitPrice, sellingPrice, notes, Convert.ToInt32(Session["UID"].ToString()));
            if (timeandexpenses == 1)
            {
                lblacual.Visible = true;
                lblacual.Text = Resources.DeffinityRes.Expenses_Added;//"Expenses added successfully";

            }
            viewButtonCode_Timeandexpenses();
            SelectValues(QueryStringValues.Project);
        }

        if (e.CommandName == "Update")
        {
            int ID = Convert.ToInt32(e.CommandArgument.ToString());
            int i = GridView5.EditIndex;
            GridViewRow Row = GridView5.Rows[i];
            int EntryType1 = Convert.ToInt32(((DropDownList)Row.FindControl("ddlEntryTandE")).SelectedItem.Value);
            DateTime eDate1 = Convert.ToDateTime(((TextBox)Row.FindControl("txtEndDateTandE")).Text);
            double Qty = 0;
            double UnitPrice = 0;
            double sellingPrice = 0;

            if (((TextBox)Row.FindControl("txtQtyTandE")).Text != "")
            {
                Qty = Convert.ToDouble(((TextBox)Row.FindControl("txtQtyTandE")).Text);
            }
            if (((TextBox)Row.FindControl("txtUnitPriceTandE")).Text != "")
            {
                UnitPrice = Convert.ToDouble(((TextBox)Row.FindControl("txtUnitPriceTandE")).Text);
            }
            if (((TextBox)Row.FindControl("txtSellingTandE")).Text != "")
            {
                sellingPrice = Convert.ToDouble(((TextBox)Row.FindControl("txtSellingTandE")).Text);
            }
            int GetContractorIDEXP = Convert.ToInt32(((Label)Row.FindControl("lblcontratorIDexp")).Text);

            string Notes1 = ((TextBox)Row.FindControl("txtNotesTandE")).Text;

            //   int ProjectReference_External = Convert.ToInt32(((DropDownList)Row.FindControl("ddlTitleTandE")).SelectedItem.Value);

            int ProjectReference_External = Convert.ToInt32(QueryStringValues.Project);

            try
            {
                int te = UpdateExpenses(ID, ProjectReference_External, GetContractorIDEXP, EntryType1, eDate1, Qty, UnitPrice, sellingPrice, Notes1);
                if (te == 1)
                {
                    lblacual.Visible = true;
                    lblacual.Text = Resources.DeffinityRes.Expenses_Updated;// "Expenses updated successfully";
                }
                viewButtonCode_Timeandexpenses();
                SelectValues(QueryStringValues.Project);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                //Stemp = ex.ToString();
            }

        }
        if (e.CommandName == "Delete")
        {

            string id = e.CommandArgument.ToString();

            string delete = "delete from TimeExpenses where ID='" + id + "'";

            SqlCommand cmd = new SqlCommand(delete, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                GridView5.EditIndex = -1;
                viewButtonCode_Timeandexpenses();
                SelectValues(QueryStringValues.Project);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            finally
            {
                con.Close();

            }

        }

       
    }
    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal rowExpenses_BuyingTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
                Expenses_grandBuyingTotal = Expenses_grandBuyingTotal + rowExpenses_BuyingTotal;
                lblExpenses_BuyingCost.Text = string.Format("{0:N2}", Expenses_grandBuyingTotal);
                lblTotalCostofExpenses.Text = string.Format("{0:N2}", Expenses_grandBuyingTotal);
                // lblTotalprojectBuyingCost.Text = (Expenses_grandBuyingTotal + grandBuyingTotal).ToString();

                decimal Expenses_rowSellingTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SellingTotal"));
                Expenses_grandSellingTotal = Expenses_grandSellingTotal + Expenses_rowSellingTotal;

                lblExpenses_SellingCost.Text = string.Format("{0:N2}", Expenses_grandSellingTotal);


                lbltoalCost_Expensesselling.Text = string.Format("{0:N2}", Expenses_grandSellingTotal);

                object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];
                if (objList != null)
                {
                    if (objList[0].ToString() == "-99")
                    {
                        e.Row.Visible = false;
                    }
                    else
                    {
                        if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
                        {
                            DropDownList ddlEntryTandE = e.Row.FindControl("ddlEntryTandE") as DropDownList;

                            try { ddlEntryTandE.SelectedValue = objList[2].ToString(); }
                            catch (Exception ex)
                            {
                                ddlEntryTandE.SelectedIndex = 0;
                                LogExceptions.WriteExceptionLog(ex);
                            }

                        }
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Expenses_grandBuyingTotal = 0;
                Expenses_grandSellingTotal = 0;
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    protected void GridView5_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    private void viewButtonCode_Timeandexpenses()
    {

        try
        {


            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            SqlCommand myCommand = new SqlCommand("DN_ExpensesDisplay", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@ProjectReference", SqlDbType.Int, 32).Value = QueryStringValues.Project;
            //  myCommand.Parameters.Add("@ContractorID", SqlDbType.Int, 32).Value = Convert.ToInt32(Session["UID"].ToString());


            SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            DataSet ds = new DataSet();
            myadapter.Fill(ds);
            //string _output = myCommand.Parameters["@out"].Value.ToString();
            GridView5.DataSource = ds.Tables[0];
            GridView5.DataBind();


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    private int insertExpenses(DateTime Expensesfooter_Date, int Expensesfooter_projectReference, int Expensesfooter_EntryType, double Expensesfooter_Amount, double UnitPrice, double sellingPrice, string Expensesfooter_Notes, int Expensesfooter_ResourceID)
    {
        int Getvaltype = 0;
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand("DN_InserTimeandexpenses");
            db.AddInParameter(cmd, "@TimeExpensesDate", DbType.DateTime, Expensesfooter_Date);
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Expensesfooter_projectReference);
            db.AddInParameter(cmd, "@EntryType", DbType.Int32, Expensesfooter_EntryType);
            db.AddInParameter(cmd, "@Qty", DbType.Double, Expensesfooter_Amount);
            db.AddInParameter(cmd, "@BuyingPrice", DbType.Double, UnitPrice);
            db.AddInParameter(cmd, "@sellingPrice", DbType.Double, sellingPrice);
            db.AddInParameter(cmd, "@Notes", DbType.String, Expensesfooter_Notes);
            db.AddInParameter(cmd, "@ContractorID", DbType.Int32, Expensesfooter_ResourceID);
            Getvaltype = Convert.ToInt32(db.ExecuteNonQuery(cmd));
            cmd.Dispose();


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Getvaltype;
    }
    public int UpdateExpenses(int ID, int projectReference, int ResourceID, int EntryType, DateTime Date, double Hours, double UnitPrice, double sellingPrice, string Notes)
    {
        int getVal = 0;
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand("DN_Timeandexpensesupdate");
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, projectReference);
            db.AddInParameter(cmd, "@ResourceID", DbType.Int32, ResourceID);
            db.AddInParameter(cmd, "@entryid", DbType.Int32, EntryType);
            db.AddInParameter(cmd, "@Timesheetdate", DbType.DateTime, Date);
            db.AddInParameter(cmd, "@Qty", DbType.Double, Hours);
            db.AddInParameter(cmd, "@BuyingPrice", DbType.Double, UnitPrice);
            db.AddInParameter(cmd, "@SellingPrice", DbType.Double, sellingPrice);
            db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
            db.AddInParameter(cmd, "@notes", DbType.String, Notes);
            db.AddOutParameter(cmd, "@output", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            getVal = (int)db.GetParameterValue(cmd, "@output");
            cmd.Dispose();


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return getVal;
    }

    private void SetExternalExpenseCost()
    {
        using (FinanceModuleDataContext db = new FinanceModuleDataContext())
        {
            int projectReference = QueryStringValues.Project;

            double? expenseCost = (from e in db.ExternalExpenses 
                                         where e.Expensed == true && e.ProjectReference==projectReference
                                         select (e.Qty * e.UnitCost)).Sum();
            lblExternalExpenses.Text = expenseCost.HasValue ? expenseCost.Value.ToString("f2") : "0.00";
        }
    }
  
    //protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
    //{


    //    if (e.CommandName == "Insert_ExternalFooter")
    //    {

    //        double amount = 0;
    //        string notes = "";
    //        // int ProjectTitel = 0;
    //        int EntryType = 0;


    //        DateTime TESTdate = Convert.ToDateTime(((TextBox)GridView6.FooterRow.FindControl("txt_DATEfooterexternalExpenses")).Text);

    //        if (((TextBox)GridView6.FooterRow.FindControl("txtAmount_FooterExternalExpenses")).Text != "")
    //        {
    //            amount = Convert.ToDouble(((TextBox)GridView6.FooterRow.FindControl("txtAmount_FooterExternalExpenses")).Text);
    //        }

    //        notes = ((TextBox)GridView6.FooterRow.FindControl("txtNotes_FooterExternalExpenses")).Text;

    //        if (((DropDownList)GridView6.FooterRow.FindControl("ddlEntry_footerExternalExpenses")).Text != "")
    //        {
    //            EntryType = Convert.ToInt32(((DropDownList)GridView6.FooterRow.FindControl("ddlEntry_footerExternalExpenses")).SelectedValue);
    //        }
    //        //if (((DropDownList)GridView6.FooterRow.FindControl("ddlTitle_FooterExternalExpenses")).Text != "")
    //        //{
    //        //    ProjectTitel = Convert.ToInt32(((DropDownList)GridView6.FooterRow.FindControl("ddlTitle_FooterExternalExpenses")).SelectedValue);
    //        //}


    //        int ExternalExpenses;
    //        ExternalExpenses = InsertExternalExpenses(Convert.ToInt32(QueryStringValues.Project), Convert.ToInt32(Session["UID"].ToString()), EntryType, TESTdate, amount, notes);
    //        if (ExternalExpenses == 0)
    //            lblError1.Text = Resources.DeffinityRes.ErrorWhileinserting;//;"Error While inserting";
    //        else if (ExternalExpenses >= 1)
    //        {
    //            lblError1.Visible = true;
    //            lblError1.Text = Resources.DeffinityRes.ExternalExpensesenteredsuccessfully;// "External Expenses entered successfully";
    //            viewButtonCode_ExternalExpenses();
    //            SelectValues(QueryStringValues.Project);

    //        }




    //    }


    //    if (e.CommandName == "Update")
    //    {
    //        int ID = Convert.ToInt32(e.CommandArgument.ToString());
    //        int i = GridView6.EditIndex;
    //        GridViewRow Row = GridView6.Rows[i];
    //        //int ProjectReferenceTandE = Convert.ToInt32(((Label)Row.FindControl("lblProjectExternalExpenses")).Text );
    //        int ProjectReferenceTandE = QueryStringValues.Project;
    //        //

    //        //if (((DropDownList)Row.FindControl("ddlTitleExternalExpenses")).Text != "")
    //        //{
    //        //    ProjectReferenceTandE = Convert.ToInt32(((DropDownList)Row.FindControl("ddlTitleExternalExpenses")).SelectedItem.Value);
    //        //}


    //        int ContractorID = Convert.ToInt32(((Label)Row.FindControl("lblProjectReferenceexp")).Text);
    //        int EntryTypeTandE = Convert.ToInt32(((DropDownList)Row.FindControl("ddlEntryExternalExpenses")).SelectedItem.Value);
    //        DateTime eDateTandE = Convert.ToDateTime(((TextBox)Row.FindControl("txtEndDateernalExpenses")).Text);
    //        double AmountTandE = 0;
    //        if (((TextBox)Row.FindControl("txtAmountExternalExpenses")).Text != "")
    //        {
    //            AmountTandE = Convert.ToDouble(((TextBox)Row.FindControl("txtAmountExternalExpenses")).Text);
    //        }
    //        string NotesTandE = ((TextBox)Row.FindControl("txtNotesExternalExpenses")).Text;

    //        UpdateExternalExpenses(ID, ProjectReferenceTandE, EntryTypeTandE, eDateTandE, AmountTandE, NotesTandE, ContractorID);

    //    }
    //    if (e.CommandName == "Delete")
    //    {

    //        string id = e.CommandArgument.ToString();

    //        string delete = "delete from ExternalExpenses where ID='" + id + "'";

    //        SqlCommand cmd = new SqlCommand(delete, con);
    //        try
    //        {
    //            con.Open();
    //            cmd.ExecuteNonQuery();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogExceptions.WriteExceptionLog(ex);
    //        }
    //        finally
    //        {
    //            con.Close();

    //        }
    //        GridView6.EditIndex = -1;
    //        // viewButtonCode();
    //        viewButtonCode_ExternalExpenses();
    //        SelectValues(QueryStringValues.Project);
          
    //    }
    //    if (e.CommandName == "ExternalExpensesType")
    //    {
    //        Response.Redirect("AdminDropDown.aspx?type=Finance&Projet=" + QueryStringValues.Project, false);
    //    }


    //}
    //protected void GridView6_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    GridView6.EditIndex = -1;
    //    viewButtonCode_ExternalExpenses();
        

    //}
    //protected void GridView6_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    GridView6.EditIndex = e.NewEditIndex;
    //    viewButtonCode_ExternalExpenses();
       
    //}
    //protected void GridView6_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    GridView6.EditIndex = -1;
    //    viewButtonCode_ExternalExpenses();
    //}
    //protected void GridView6_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{

    //}
    //protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        decimal rowExpensesExternal_BuyingTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
    //        ExpensesExternal_grandBuyingTotal = ExpensesExternal_grandBuyingTotal + rowExpensesExternal_BuyingTotal;
    //        lblExternalExpenses.Text = string.Format("{0:N2}", ExpensesExternal_grandBuyingTotal);
    //        lbltotalexternal_ExpensesCost.Text = string.Format("{0:N2}", ExpensesExternal_grandBuyingTotal);

    //        //lblTotalprojectBuyingCost.Text = string.Format("{0:#.00}",Expenses_grandBuyingTotal + grandBuyingTotal + ExpensesExternal_grandBuyingTotal);
    //        //lbltotalsellingpriceofproject.Text = string.Format("{0:#.00}",Expenses_grandSellingTotal + grandSellingTotal );
    //        object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];
    //        if (objList != null)
    //        {
    //            if (objList[0].ToString() == "-99")
    //            {
    //                e.Row.Visible = false;
    //            }
    //        }
    //    }
    //}
    //public int InsertExternalExpenses(int ProjectReference_External, int ResourceID_External, int EntryType_External, DateTime Date_External, double Amount_External, String Notes)
    //{
    //    try
    //    {
    //        Database db = DatabaseFactory.CreateDatabase("DBstring");
    //        DbCommand cmd = db.GetStoredProcCommand("DN_ExternalExpensesInsert");
    //        db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, ProjectReference_External);
    //        db.AddInParameter(cmd, "@ContractorID", DbType.Int32, ResourceID_External);
    //        db.AddInParameter(cmd, "@ExpensesentrytypeID", DbType.Int32, EntryType_External);
    //        db.AddInParameter(cmd, "@ExternalExpensesDate", DbType.DateTime, Date_External);
    //        db.AddInParameter(cmd, "@amount", DbType.Double, Amount_External);

    //        db.AddInParameter(cmd, "@Notes", DbType.String, Notes);

    //        int getVal = Convert.ToInt32(db.ExecuteNonQuery(cmd));
    //        cmd.Dispose();
    //        return getVal;



    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //        return 0;
    //    }
    //    finally
    //    {

    //    }
    //}
    //private void viewButtonCode_ExternalExpenses()
    //{

    //    try
    //    {


    //        SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    //        SqlCommand myCommand = new SqlCommand("DN_ExternalExpensesDisplaybyprojectRef", myConnection);
    //        myCommand.CommandType = CommandType.StoredProcedure;
    //        myCommand.Parameters.Add("@ProjectReference", SqlDbType.Int, 32).Value = QueryStringValues.Project;
    //        //  myCommand.Parameters.Add("@ContractorID", SqlDbType.Int, 32).Value = Convert.ToInt32(Session["UID"].ToString());


    //        SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
    //        DataSet ds = new DataSet();
    //        myadapter.Fill(ds);
    //        //string _output = myCommand.Parameters["@out"].Value.ToString();
    //        GridView6.DataSource = ds.Tables[0];
    //        GridView6.DataBind();


    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }


    //}
    //public void UpdateExternalExpenses(int ID, int ProjectReferenceexternalexpenses, int ExpensesEntryType, DateTime ExternalExpenseDate, double ExternalExpenseAoumnt, String ExternalexpensesNotes, int ContractorID)
    //{



    //    try
    //    {
    //        Database db = DatabaseFactory.CreateDatabase("DBstring");
    //        DbCommand cmd = db.GetStoredProcCommand("DN_ExternalExpensesupdate");
    //        db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, ProjectReferenceexternalexpenses);
    //        db.AddInParameter(cmd, "@ResourceID", DbType.Int32, ContractorID);
    //        db.AddInParameter(cmd, "@entryid", DbType.Int32, ExpensesEntryType);
    //        db.AddInParameter(cmd, "@Timesheetdate", DbType.DateTime, ExternalExpenseDate);
    //        db.AddInParameter(cmd, "@amount", DbType.Double, ExternalExpenseAoumnt);
    //        db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
    //        db.AddInParameter(cmd, "@notes", DbType.String, ExternalexpensesNotes);
    //        db.AddOutParameter(cmd, "@output", DbType.Int32, 4);
    //        db.ExecuteNonQuery(cmd);
    //        int GetVal = (int)db.GetParameterValue(cmd, "@output");

    //        if (GetVal == 0)
    //        {
    //            lblError1.Visible = true;
    //            lblError1.Text = Resources.DeffinityRes.ErrWhileExtrnalExpsesUpdation;//"Error While External Expenses Updation";
    //        }
    //        else if (GetVal == 1)
    //        {
    //            lblError1.Visible = true;
    //            lblError1.Text = Resources.DeffinityRes.ExternalExpensesUpdated;//"External Expenses Updated  Successfully ";
    //        }


    //        viewButtonCode_ExternalExpenses();
    //        SelectValues(QueryStringValues.Project);

    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //        //return 0;
    //    }
    //    finally
    //    {

    //    }

    //}
}