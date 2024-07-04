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
using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using TimeSheet_Admin;
using CheckTimesheetApprovers;
using System.Linq;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
public partial class Timesheet : System.Web.UI.Page
{
    string javaScript = string.Empty;
    string gvUniqueID = String.Empty;
    int gvNewPageIndex = 0;
    int gvEditIndex = -1;
    private const string ASCENDING1 = " ASC";
    private const string DESCENDING1 = " DESC";
    static private DataView dvProducts;

    private const string ASCENDING2 = " ASC";
    private const string DESCENDING2 = " DESC";
    private const string ASCENDING3 = " ASC";
    private const string DESCENDING3 = " DESC";
    static private DataView dvProducts1;
    static private DataView dvProducts2;
    int GetContractorID = 0;

    int TimesheetPrimaryApprover = 0;
    int TimesheetOptionalApprover = 0;
    int GetTimesheetApproverNow = 0;
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    string submitids = "0";
    int conID;
    ArrayList AppIds = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
       // Master.PageHead = "Timesheet Admin";

        if (!Page.IsPostBack)
        {
            //Bind the wc dates in dropdown
            ////Bind_WCDate();

            getFirstGrid();
            #region Girdview Two
            ////GetSecondGrid();
            #endregion
            delineTimesheetGrid();
            //PendingtoApproveTimeSheet("", "");
            ////Bind_Customers();
           
            ////TimeSheet_TimeSheetJournal(Convert.ToInt32(Dropdownlist4.SelectedValue));
            
        }
        lblError.Text = "";
    }
    

    string SubmitIDs
    {
        get
        {
            return submitids;
        }
    }
    #region Send for Approve TimeSheets
   
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();  
    }



    ////public void GetSecondGrid()
    ////{
    ////    TimeSheetAdminApprove PendingTimesheets = null;
    ////    PendingTimesheets = new TimeSheetAdminApprove();


    ////    try
    ////    {
    ////        ////dvProducts1 = new DataView(PendingTimesheets.displayTimesheet_PendingApproval(DropDownList1.SelectedValue, Convert.ToInt32(sessionKeys.UID)).Tables[0]);
    ////        BindPendingGridView();
    ////        //if ((DropDownList1.SelectedValue == "") || (DropDownList1.SelectedValue == "Please select..."))
    ////        //{
    ////        //    dvProducts1 = new DataView(PendingTimesheets.displayTimesheet_PendingApproval(DropDownList1.SelectedValue, Convert.ToInt32(sessionKeys.UID)).Tables[0]);
    ////        //    BindPendingGridView();
    ////        //}
    ////        //else
    ////        //{
    ////        //    dvProducts1 = new DataView(PendingTimesheets.displayTimesheet_PendingApproval(DropDownList1.SelectedValue, Convert.ToInt32(sessionKeys.UID)).Tables[0]);
    ////        //    BindPendingGridView();
    ////        //}
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        LogExceptions.WriteExceptionLog(ex);
    ////    }
    ////    finally
    ////    {
    ////    }

    ////}
    public void getFirstGrid()
    {
        #region Gridviewone
        TimeSheetAdminApprove DisplayGrid11 = null;
        DisplayGrid11 = new TimeSheetAdminApprove();
        dvProducts = new DataView(DisplayGrid11.displayTimesheet_SendforApproval(Convert.ToInt32(sessionKeys.UID)).Tables[0]);
        BindGridView();
        #endregion
    }

    public void delineTimesheetGrid()
    {
        TimeSheetAdminApprove DisplayGrid11 = null;
        DisplayGrid11 = new TimeSheetAdminApprove();
        dvProducts2 = new DataView(DisplayGrid11.displayTimesheet_Decline(Convert.ToInt32(sessionKeys.UID)).Tables[0]);
        ////BindDecline();
    }



  

    #endregion

    #region Bind Grid based on week id 
    public void BindSignoffGrid(int wcdateid,int contractorid)
    {
        try
        {
            TimeSheetAdminApprove td = new TimeSheetAdminApprove();
            DataSet ds = td.dsTimeSheet_Signoff(wcdateid, contractorid);
            gvSignoff.DataSource = ds.Tables[0];
            gvSignoff.DataBind();
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    #endregion
    public string ChangeHoues(string GetHours)
    {
        string GetActivity = "";
        char[] comm1 = { '.' };
        string[] displayTime = GetHours.Split(comm1);


        GetActivity = displayTime[0] + ":" + displayTime[1];



        return GetActivity;
    }

    public string ChangeToHours(string GetHours)
    {
        string GetActivity = "";
        char[] comm1 = { '.' };
        string[] displayTime = GetHours.Split(comm1);


        GetActivity = displayTime[0] + ":" + displayTime[1];



        return GetActivity;
    }

    public string ApproverStatus(string primeapprover,string secondapprover)
    {
        string img = string.Empty;

        if (primeapprover == "0" && secondapprover == "0")
            img = "<img src='media/ico_no_app.png' alt='Not Approved' title='Not Approved'/>";
        else if (primeapprover == "1" && secondapprover == "0")
            img = "<img src='media/ico_1_app.png' alt='Approved by primary approver' title='Approved by primary approver'/>";
        else if (primeapprover == "0" && secondapprover == "1")
            img = "<img src='media/ico_2_app.png' alt='Approved by secondary approver' title='Approved by secondary approver'/>";
        else 
            img = "";

        //string IMG = "";
        //  string IMG1 = "";
        //if (FlagStatusType == "0")
        //{
         
        //   // IMG1= "pending";

        //    IMG = "ico_no_app.png";
        //    IMG1 = "~/media/" + IMG;
        //}
        //else if (FlagStatusType == "1")
        //{
        //    IMG = "ico_1_app.png";
        //    IMG1= "~/media/" + IMG;
        //}
        //else if (FlagStatusType == "2")
        //{
        //    IMG = "ico_2_app.png";
        //      IMG1= "~/media/" + IMG;
           
        //}
        //else if (FlagStatusType == "3")
        //{
        //    IMG = "This is aleady approved...";
        //}


            return img;
    }

    ////private void Bind_Customers()
    ////{
    ////    //customerDeffinity.PortfolioManager.Portfilio.Portfolio_display();
    ////    TimeSheetAdminApprove GetCustomer = null;
    ////    GetCustomer = new TimeSheetAdminApprove();
    ////    Dropdownlist4.DataSource = GetCustomer.GetContractors(Convert.ToInt32(sessionKeys.UID));
    ////    Dropdownlist4.DataTextField = "ContractorName";
    ////    Dropdownlist4.DataValueField = "ID";
    ////    Dropdownlist4.DataBind();
    ////}

    ////public void TimeSheet_TimeSheetJournal(int ResourceID)
    ////{
    ////    try
    ////    {
    ////        TimeSheetAdminApprove GetTimeSheetJournal = null;
    ////        GetTimeSheetJournal = new TimeSheetAdminApprove();
    ////        int GetResourceID = Convert.ToInt32(Dropdownlist4.SelectedValue);

    ////        GridView3.DataSource = GetTimeSheetJournal.TimeSheetJournals(ResourceID, txt_startDate.Text, txt_EndDate.Text,Convert.ToInt32(sessionKeys.UID));
    ////        GridView3.DataBind();
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        LogExceptions.WriteExceptionLog(ex);
    ////    }
    ////    finally
    ////    {

    ////    }

    ////}

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            try
            {

                int ID = Convert.ToInt32(e.CommandArgument.ToString());

                string GetCID=string.Empty;

                                
                GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                GetCID = ((Label)gvrow.FindControl("lblContractorID1")).Text;

                HiddenField4.Value = GetCID;
                //GetContractorID = 

                GetViewofSendforApproval.Visible = true;
                GetIDAccept.Visible = true;
                TextBox1.Visible = true;
                btn_approve.Visible = true;
                btn_declined.Visible = true;
                ImgClose.Visible = true;
                HiddenField1.Value = ID.ToString();
                HiddenField2.Value = "";

                HiddenField3.Value = "";
                TimeSheetAdminApprove ViewTimeSheet = null;
                ViewTimeSheet = new TimeSheetAdminApprove();
                GridView4.DataSource = ViewTimeSheet.ViewTimeSheet_SubmittApproval(ID, 2, Convert.ToInt32(HiddenField4.Value),Convert.ToInt32(sessionKeys.UID));
                GridView4.Columns[0].Visible = true;
                GridView4.DataBind();                
                ModalControlExtender2.Show();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           
        }
    }


    ////protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    ////{
    ////    GridView3.PageIndex = e.NewPageIndex;
    ////    TimeSheet_TimeSheetJournal(Convert.ToInt32(Dropdownlist4.SelectedValue));
    ////}
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "View_journal")
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                HiddenField2.Value = ID.ToString();
                HiddenField1.Value = "";

                HiddenField3.Value = "";
                GetViewofSendforApproval.Visible = true;
                //GetIDAccept.Visible = false;
                GetIDAccept.Visible = true;
                TextBox1.Visible = false;
                btn_approve.Visible = false;
                btn_declined.Visible = false;
                ImgClose.Visible = true;
                TimeSheetAdminApprove ViewTimeSheet = null;
                ViewTimeSheet = new TimeSheetAdminApprove();

                GridView4.DataSource = ViewTimeSheet.ViewTimeSheet_SubmittApproval(ID, 3, 0, Convert.ToInt32(sessionKeys.UID));
                GridView4.Columns[0].Visible = false;
                GridView4.DataBind();
                ModalControlExtender2.Show();
               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           
        }
    }

    ////private void Bind_WCDate()
    ////{
    ////    //customerDeffinity.PortfolioManager.Portfilio.Portfolio_display();
    ////    TimeSheetAdminApprove GetCustomer = null;
    ////    GetCustomer = new TimeSheetAdminApprove();
    ////    DropDownList1.DataSource = GetCustomer.GetWeekCommanceDate(sessionKeys.UID);
    ////    DropDownList1.DataTextField = "WCDate";
    ////    DropDownList1.DataValueField = "WCDate";
    ////    DropDownList1.DataBind();
        
    ////    DropDownList1.Items.Insert(0, new ListItem("ALL", "0"));

    ////}
   

    protected void btn_approve_Click(object sender, EventArgs e)
    {
        try
        {
            Timesheet_Approve();
            //changeStatus(4);
            DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);

            TextBox1.Text = "";

            getFirstGrid();
            ////GetSecondGrid();
            ////BindPendingGridView();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void btn_innerapprove_Click(object sender, EventArgs e)
    {
        try
        {
            GridView gv = new GridView();
            gv = (GridView)GridView1.FindControl("gvInnerTimeSheet");
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                //GridViewRow row = GridView4.Rows[i];
                //bool isChecked = ((CheckBox)row.FindControl("chkSelect2")).Checked;

                //if (isChecked)
                //{
                //    //get timesheet id
                //    Label lblTimesheetid = ((Label)row.FindControl("lblID"));
                //    //Approver by timesheet ID
                //    ApproveByID(int.Parse(lblTimesheetid.Text), TextBox1.Text.Trim());
                //}
            }
            //Timesheet_Approve();
            ////changeStatus(4);
            //DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);

            //TextBox1.Text = "";

            //getFirstGrid();
            ////GetSecondGrid();
            ////BindPendingGridView();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void btn_declined_Click(object sender, EventArgs e)
    {
        //changeStatusDenyed(3);
        DeclineTime();
        DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);
        TextBox1.Text = "";
        getFirstGrid();
        ////GetSecondGrid();
        //// BindPendingGridView();
    }

    private void DeclineTime()
    {
        try
        {
            string TimeSheetIDs = string.Empty;
            string TIMEID = string.Empty;
            for (int i = 0; i < GridView4.Rows.Count; i++)
            {
                GridViewRow row = GridView4.Rows[i];

                bool isChecked = ((CheckBox)row.FindControl("chkSelect2")).Checked;
                //contractorid = ((Label)row.FindControl("lblContractorID")).Text;
                if (isChecked)
                {
                    Label lbl1 = ((Label)row.FindControl("lblID"));
                    //contractorid1 = ((Label)row.FindControl("lblContractorID")).Text;
                    //ProjectID = Convert.ToInt32(((Label)row.FindControl("lblPref")).Text);
                    //submitids = submitids + "," + contractorid1;
                    TIMEID = ((Label)row.FindControl("lblID")).Text;
                    TimeSheetIDs = TimeSheetIDs + "," + TIMEID;

                }
            }
            if (!string.IsNullOrEmpty(TimeSheetIDs))
            {
                TimeSheetAdminApprove tm = new TimeSheetAdminApprove();
                tm.Timesheet_Decline(TimeSheetIDs, sessionKeys.UID, TextBox1.Text);
                TextBox1.Text = string.Empty;
                //send decline mails to Users 
                SendDdeclineMail(TimeSheetIDs);
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }


    ////protected void btn_View_Click(object sender, ImageClickEventArgs e)
    ////{

    ////    try
    ////    {
    ////        //PendingtoApproveTimeSheet("", "");
            
    ////        GetViewofSendforApproval.Visible = true;
    ////        GetIDAccept.Visible = true;
    ////        TextBox1.Visible = true;
    ////        btn_approve.Visible = true;
    ////        btn_declined.Visible = true;
    ////        ImgClose.Visible = true;
    ////        TimeSheetAdminApprove PendingTimesheet = null;
    ////        PendingTimesheet = new TimeSheetAdminApprove();
    ////        //Bind the exception grid
    ////        GetSecondGrid();

    ////        if ((DropDownList1.SelectedValue == "Please select...") || (DropDownList1.SelectedValue == "") || (DropDownList1.SelectedValue == "0"))
    ////        {
    ////            HiddenField3.Value = "S";
    ////            GridView4.DataSource = PendingTimesheet.ViewTimeSheet_SubmittApproval1(DropDownList1.SelectedValue, 1, Convert.ToInt32(sessionKeys.UID));
    ////            GridView4.Columns[0].Visible = true;
    ////            GridView4.DataBind();
    ////           // ModalControlExtender2.Show();
    ////        }
    ////        else
    ////        {
    ////            HiddenField1.Value = "";
    ////            HiddenField2.Value = "";
    ////            HiddenField3.Value = DropDownList1.SelectedValue;
    ////            GridView4.DataSource = PendingTimesheet.ViewTimeSheet_SubmittApproval1(DropDownList1.SelectedValue, 1, Convert.ToInt32(sessionKeys.UID));
    ////            GridView4.Columns[0].Visible = true;
    ////            GridView4.DataBind();
    ////            //ModalControlExtender2.Show();
    ////        }
            
    ////        BindPendingGridView();

    ////    }

    ////    catch (Exception ex)
    ////    {
    ////        LogExceptions.WriteExceptionLog(ex);
    ////    }
    ////    finally
    ////    {

    ////    }

    ////}
    ////protected void btn_filter_Click(object sender, ImageClickEventArgs e)
    ////{
    ////    TimeSheet_TimeSheetJournal(Convert.ToInt32(Dropdownlist4.SelectedValue));
    ////}
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            
            ((CheckBox)e.Row.FindControl("chkAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("chkAll")).ClientID + "')");
        }
        // Make sure we aren't in header/footer rows
        if (row.DataItem == null)
        {
            return;
        }
  
        TimeSheetAdminApprove ViewTimeSheet = null;
        ViewTimeSheet = new TimeSheetAdminApprove();
        //Find Child GridView control
        GridView gv = new GridView();
        gv = (GridView)row.FindControl("gvInnerTimeSheet");
        if (gv.UniqueID == gvUniqueID)
        {
            gv.PageIndex = gvNewPageIndex;
            gv.EditIndex = gvEditIndex;
            //Check if Sorting used


        }
        DataSet ds = ViewTimeSheet.ViewTimeSheet_SubmittApproval(Convert.ToInt32(((DataRowView)e.Row.DataItem)["WCDateID"]), 2, Convert.ToInt32(((DataRowView)e.Row.DataItem)["ContractorID"]), Convert.ToInt32(sessionKeys.UID));
        gv.DataSource = ds;
        gv.DataBind();
    }

    protected void btn_ApprovalAll_Click(object sender, EventArgs e)
    {
        try
        {
            Timesheet_ApproveAll();
            //ApprovalAll(4);
            getFirstGrid();
            BindGridView();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #region TimeSheet Staus Changes and sending mails

    #region Approval--All
    private void ApprovalAll(int _status)
    {
        //string GetCon
        bool CheckApprovers = false;
        string contractorid1 = "";
        string TimeSheetIDs = "0";
        string TIMEID = "";
        int updateflag = 0;
        int GetOwnerID = 0;

        TimeSheetAdminApprove checkStatus = new TimeSheetAdminApprove();
        TimeSheetAdminApprove GetUpdateTimesheetStatusCheck = new TimeSheetAdminApprove();
     
        try
        {
            submitids = string.Empty;
            bool flage = false;
            ArrayList AppIds = new ArrayList();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow row = GridView1.Rows[i];


                bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                if (isChecked)
                {
                    contractorid1 = ((Label)row.FindControl("lblContractorID1")).Text;
                    Label lbl1 = ((Label)row.FindControl("lblID"));

                    TimeSheetDataContext timeSheet = new TimeSheetDataContext();
                    var myTimesheet = from t in timeSheet.TimesheetEntries
                                      where t.ID == int.Parse(lbl1.Text) //&& t.PrimeApprover == int.Parse(contractorid1)
                                      select t;
                    if (myTimesheet.Count() > 0)
                    {

                    }
                    else
                    {
                        //here need to check howmay TimesheetApprovers for this contractor
                        TimeSheetAdminApprove CheckTimesheetApproverslist = new TimeSheetAdminApprove();
                        TimeSheetAdminApprove UpdateStaus1 = new TimeSheetAdminApprove();
                        //Label lbl1 = ((Label)row.FindControl("lblWCDateID"));
                        submitids = submitids + "," + contractorid1;
                        TIMEID = ((Label)row.FindControl("lblWCDateID")).Text;



                        TimeSheetIDs = TimeSheetIDs + "," + TIMEID;
                        TimeSheetAdminApprove GetTimesheetApproverslist = new TimeSheetAdminApprove();

                        //here first need to Get the projectreference.Then we need to 

                        //

                        int resultingchecing = 0;
                        SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "DN_Checkowner", new SqlParameter("@WCDateID", SqlDbType.Int, Convert.ToInt32(TIMEID)), new SqlParameter("@ContractorID", SqlDbType.Int, Convert.ToInt32(contractorid1)));
                        while (dr.Read())
                        {
                            GetOwnerID = Convert.ToInt32(dr["OwnerID"]);




                            if (GetOwnerID == sessionKeys.UID)
                            {

                                CheckApprovers = CheckTimesheetApproverslist.CheckTimesheetApproversExists(Convert.ToInt32(contractorid1));
                                DataSet listofapprovers = new DataSet();
                                if (CheckApprovers == true)
                                {


                                    listofapprovers = GetTimesheetApproverslist.GetApprovallist(Convert.ToInt32(contractorid1));

                                    if (listofapprovers.Tables[0].Rows.Count > 0)
                                    {
                                        TimesheetPrimaryApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][0].ToString());
                                        TimesheetOptionalApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][1].ToString());

                                    }

                                    if (TimesheetPrimaryApprover == TimesheetOptionalApprover)
                                    {

                                        GetTimesheetApproverNow = sessionKeys.UID;
                                        updateflag = 1;

                                    }
                                    else
                                    {
                                        if (GetOwnerID == sessionKeys.UID)
                                        {
                                            GetTimesheetApproverNow = sessionKeys.UID;
                                            updateflag = 2;
                                        }

                                        else if (TimesheetPrimaryApprover == sessionKeys.UID)
                                        {
                                            GetTimesheetApproverNow = sessionKeys.UID;
                                            updateflag = 2;
                                        }
                                        else
                                        {
                                            GetTimesheetApproverNow = sessionKeys.UID;
                                            updateflag = 3;
                                        }
                                    }

                                    Chekingtimesheetapprovers GetApproveALL = new Chekingtimesheetapprovers();
                                    int Resulttest = GetApproveALL.CheckApproveAll(Convert.ToInt32(lbl1.Text), Convert.ToInt32(contractorid1), updateflag);

                                    if (Resulttest == 3)
                                    {


                                        //CheckFlgStatusofApprover
                                        string Heading = "Timesheet Approved";
                                        string subjectline;
                                        subjectline = "Approved";
                                        TimeMail.Visible = true;

                                        int Checkcon = UpdateStaus1.UpdateTimeApprovalAll(Convert.ToInt32(lbl1.Text), _status, TextBox1.Text, Convert.ToInt32(contractorid1), sessionKeys.UID);

                                        TimesheetMailseing(Convert.ToInt32(TIMEID), subjectline, _status, Convert.ToInt32(contractorid1), "ALLApprove");
                                        string htmlText = string.Empty;
                                        StringWriter sw = new StringWriter();
                                        Html32TextWriter htmlTW = new Html32TextWriter(sw);
                                        TimeMail.RenderControl(htmlTW);
                                        htmlText = htmlTW.InnerWriter.ToString();
                                        if (htmlText != "")
                                        {
                                            string errorString = string.Empty;
                                            Email eMail = new Email();
                                            eMail.TimesheetMailIDs(GetResourceMailID(Convert.ToInt32(contractorid1)), Heading, htmlText);
                                            flage = true;
                                        }
                                        if (Checkcon == 0)
                                        {
                                            flage = false;
                                        }


                                    }

                                }
                                else
                                {

                                    //Here the Customer has only one timesheet approver.then  this condition is true.........


                                    listofapprovers = GetTimesheetApproverslist.GetApprovallist(Convert.ToInt32(contractorid1));

                                    if (listofapprovers.Tables[0].Rows.Count > 0)
                                    {
                                        TimesheetPrimaryApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][0].ToString());
                                        TimesheetOptionalApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][1].ToString());

                                    }


                                    if (TimesheetPrimaryApprover == sessionKeys.UID)
                                    {
                                        GetTimesheetApproverNow = sessionKeys.UID;
                                        updateflag = 2;
                                    }
                                    else
                                    {
                                        GetTimesheetApproverNow = sessionKeys.UID;
                                        updateflag = 3;
                                    }


                                    if ((TimesheetPrimaryApprover != 0) && (TimesheetOptionalApprover == 0))
                                    {



                                        Chekingtimesheetapprovers GetApproveALL = new Chekingtimesheetapprovers();
                                        //int Resulttest = GetApproveALL.CheckAllsingleApprover(Convert.ToInt32(lbl1.Text), Convert.ToInt32(contractorid1), updateflag);

                                        int Resulttest = GetApproveALL.CheckAllsingleApprover(Convert.ToInt32(lbl1.Text), Convert.ToInt32(contractorid1), updateflag);
                                        if (Resulttest == 3)
                                        {

                                            //CheckFlgStatusofApprover
                                            string Heading = "Timesheet Approved";
                                            string subjectline;
                                            subjectline = "Approved";
                                            TimeMail.Visible = true;

                                            int Checkcon = UpdateStaus1.UpdateTimeApprovalAll(Convert.ToInt32(lbl1.Text), _status, TextBox1.Text, Convert.ToInt32(contractorid1), sessionKeys.UID);

                                            TimesheetMailseing(Convert.ToInt32(TIMEID), subjectline, _status, Convert.ToInt32(contractorid1), "ALLApprove");
                                            string htmlText = string.Empty;
                                            StringWriter sw = new StringWriter();
                                            Html32TextWriter htmlTW = new Html32TextWriter(sw);
                                            TimeMail.RenderControl(htmlTW);
                                            htmlText = htmlTW.InnerWriter.ToString();
                                            if (htmlText != "")
                                            {
                                                string errorString = string.Empty;
                                                Email eMail = new Email();
                                                eMail.TimesheetMailIDs(GetResourceMailID(Convert.ToInt32(contractorid1)), Heading, htmlText);
                                                flage = true;
                                            }
                                            if (Checkcon == 0)
                                            {
                                                flage = false;
                                            }



                                        }
                                    }
                                    else
                                    {

                                    }



                                }





                            }

                            else//ProjectOwner not eqlaul to current Login resource then This part will work..................
                            {

                                CheckApprovers = CheckTimesheetApproverslist.CheckTimesheetApproversExists(Convert.ToInt32(contractorid1));
                                DataSet listofapprovers = new DataSet();
                                if (CheckApprovers == true)
                                {


                                    listofapprovers = GetTimesheetApproverslist.GetApprovallist(Convert.ToInt32(contractorid1));

                                    if (listofapprovers.Tables[0].Rows.Count > 0)
                                    {
                                        TimesheetPrimaryApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][0].ToString());
                                        TimesheetOptionalApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][1].ToString());

                                    }

                                    if (TimesheetPrimaryApprover == TimesheetOptionalApprover)
                                    {

                                        GetTimesheetApproverNow = sessionKeys.UID;
                                        updateflag = 1;

                                    }
                                    else
                                    {

                                        if (TimesheetPrimaryApprover == sessionKeys.UID)
                                        {
                                            GetTimesheetApproverNow = sessionKeys.UID;
                                            updateflag = 2;
                                        }
                                        else
                                        {
                                            GetTimesheetApproverNow = sessionKeys.UID;
                                            updateflag = 3;
                                        }
                                    }

                                    Chekingtimesheetapprovers GetApproveALL = new Chekingtimesheetapprovers();
                                    int Resulttest = GetApproveALL.CheckApproveAll(Convert.ToInt32(lbl1.Text), Convert.ToInt32(contractorid1), updateflag);

                                    if (Resulttest == 3)
                                    {


                                        //CheckFlgStatusofApprover
                                        string Heading = "Timesheet Approved";
                                        string subjectline;
                                        subjectline = "Approved";
                                        TimeMail.Visible = true;

                                        int Checkcon = UpdateStaus1.UpdateTimeApprovalAll(Convert.ToInt32(lbl1.Text), _status, TextBox1.Text, Convert.ToInt32(contractorid1), sessionKeys.UID);

                                        TimesheetMailseing(Convert.ToInt32(TIMEID), subjectline, _status, Convert.ToInt32(contractorid1), "ALLApprove");
                                        string htmlText = string.Empty;
                                        StringWriter sw = new StringWriter();
                                        Html32TextWriter htmlTW = new Html32TextWriter(sw);
                                        TimeMail.RenderControl(htmlTW);
                                        htmlText = htmlTW.InnerWriter.ToString();
                                        if (htmlText != "")
                                        {
                                            string errorString = string.Empty;
                                            Email eMail = new Email();
                                            eMail.TimesheetMailIDs(GetResourceMailID(Convert.ToInt32(contractorid1)), Heading, htmlText);
                                            flage = true;
                                        }
                                        if (Checkcon == 0)
                                        {
                                            flage = false;
                                        }


                                    }

                                }
                                else
                                {

                                    //Here the Customer has only one timesheet approver.then  this condition is true.........


                                    listofapprovers = GetTimesheetApproverslist.GetApprovallist(Convert.ToInt32(contractorid1));

                                    if (listofapprovers.Tables[0].Rows.Count > 0)
                                    {
                                        TimesheetPrimaryApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][0].ToString());
                                        TimesheetOptionalApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][1].ToString());

                                    }


                                    if (TimesheetPrimaryApprover == sessionKeys.UID)
                                    {
                                        GetTimesheetApproverNow = sessionKeys.UID;
                                        updateflag = 2;
                                    }
                                    else
                                    {
                                        GetTimesheetApproverNow = sessionKeys.UID;
                                        updateflag = 3;
                                    }


                                    if ((TimesheetPrimaryApprover != 0) && (TimesheetOptionalApprover == 0))
                                    {



                                        Chekingtimesheetapprovers GetApproveALL = new Chekingtimesheetapprovers();
                                        //int Resulttest = GetApproveALL.CheckAllsingleApprover(Convert.ToInt32(lbl1.Text), Convert.ToInt32(contractorid1), updateflag);

                                        int Resulttest = GetApproveALL.CheckAllsingleApprover(Convert.ToInt32(lbl1.Text), Convert.ToInt32(contractorid1), updateflag);
                                        if (Resulttest == 3)
                                        {

                                            //CheckFlgStatusofApprover
                                            string Heading = "Timesheet Approved";
                                            string subjectline;
                                            subjectline = "Approved";
                                            TimeMail.Visible = true;

                                            int Checkcon = UpdateStaus1.UpdateTimeApprovalAll(Convert.ToInt32(lbl1.Text), _status, TextBox1.Text, Convert.ToInt32(contractorid1), sessionKeys.UID);

                                            TimesheetMailseing(Convert.ToInt32(TIMEID), subjectline, _status, Convert.ToInt32(contractorid1), "ALLApprove");
                                            string htmlText = string.Empty;
                                            StringWriter sw = new StringWriter();
                                            Html32TextWriter htmlTW = new Html32TextWriter(sw);
                                            TimeMail.RenderControl(htmlTW);
                                            htmlText = htmlTW.InnerWriter.ToString();
                                            if (htmlText != "")
                                            {
                                                string errorString = string.Empty;
                                                Email eMail = new Email();
                                                eMail.TimesheetMailIDs(GetResourceMailID(Convert.ToInt32(contractorid1)), Heading, htmlText);
                                                flage = true;
                                            }
                                            if (Checkcon == 0)
                                            {
                                                flage = false;
                                            }



                                        }
                                    }
                                    else
                                    {

                                    }


                                }
                            }//PrjectOwer else part end here......................



                        }
                        dr.Close();



                    }


                    }






                  
               
                    
            }

           
          
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            TimeMail.Visible = false;
            getFirstGrid();

        }



    }

    #endregion

    #region Declined

    private void changeStatusDenyed(int _status)
    {
        string contractorid="";
        string contractorid1 = "";
        string TimeSheetIDs = "0";
        int resultTimesheet=0;
        string TIMEID="";
        int ProjectID = 0;
        try
        {
            submitids = "0";
            bool flage = true;

           
            for (int i = 0; i < GridView4.Rows.Count; i++)
            {
                GridViewRow row = GridView4.Rows[i];

                bool isChecked = ((CheckBox)row.FindControl("chkSelect2")).Checked;
                contractorid = ((Label)row.FindControl("lblContractorID")).Text;
                if (isChecked)
                {
                    Label lbl1 = ((Label)row.FindControl("lblID"));
                    contractorid1 = ((Label)row.FindControl("lblContractorID")).Text;
                    ProjectID = Convert.ToInt32(((Label)row.FindControl("lblPref")).Text);
                   submitids = submitids + "," + contractorid1;
                    TIMEID=((Label)row.FindControl("lblID")).Text ;
                    TimeSheetIDs=TimeSheetIDs+","+ TIMEID;
                    
                    TimeSheetAdminApprove UpdateStaus1 = new TimeSheetAdminApprove();
                    int Checkcon = UpdateStaus1.UpdateTimeSheetStaus(Convert.ToInt32(lbl1.Text), _status, TextBox1.Text, Convert.ToInt32(contractorid1), sessionKeys.UID);

                    TimeSheetAdminApprove CheckTimesheetApproverslist = new TimeSheetAdminApprove();


                    // int y1 = Convert.ToInt32(HiddenField1.Value);

                    resultTimesheet = CheckTimesheetresourcesApprovers(Convert.ToInt32(contractorid1), 0, Convert.ToInt32(TIMEID), 1, ProjectID);

                    if (resultTimesheet == 0)
                    {
                        //this Timesheet already submitted.... bu the Timehseet approvers....
                    }
                    else if (resultTimesheet == 1)
                    {
                        //here PrimaryTimesheetapprover approved but optinalTimeheet Approver pending for this resource...
                    }
                    else if (resultTimesheet == 2)
                    {
                        //Optional Timesheet Approver  but primaryTimesheet approver need to approve
                    }
                    else if (resultTimesheet == 3)
                    {

                        //TimeSheetAdminApprove UpdateStaus1 = new TimeSheetAdminApprove();
                        int Checkcon1 = UpdateStaus1.UpdateTimeSheetStaus(Convert.ToInt32(lbl1.Text), _status, TextBox1.Text, Convert.ToInt32(contractorid1), sessionKeys.UID);

                        flage = true;
                    }
                    else if (resultTimesheet == 4)
                    {

                    }
                    else if (resultTimesheet == 5)
                    {

                    }
                    else if (resultTimesheet == 6)
                    {
                        // primary timesheet approved already Submitted the Timesheet
                    }
                    else if (resultTimesheet == 7)
                    {
                        //Optional Timesheet approver already submitted
                    }



                }



            }


            if ((flage == false) && (resultTimesheet == 2) && (_status == 4))
            {
                //lblError.Text = "Sorry, you do not have permission to approve this timesheet";
            }
            else if ((resultTimesheet == 0) && (flage == false) && (_status == 4))
            {
                lblError.Text = "This timesheet is already Submitted";
            }

            else if ((resultTimesheet == 1) && (flage == false) && (_status == 4))
            {
                lblError.Text = "Timesheet has been sent to secondary approver";
            }
            else if ((resultTimesheet == 2) && (flage == false) && (_status == 4))
            {
                lblError.Text = "Secondary Approver  but waiting for  primaryTimesheet approver need to approve";
            }
            else if ((resultTimesheet == 6) && (flage == false) && (_status == 4))
            {
                lblError.Text = "Primary timesheet approver has already approved this timesheet entry";
            }

            else if ((resultTimesheet == 7) && (flage == false) && (_status == 4))
            {
                lblError.Text = "Secondary Timesheet approver has already approved this timesheet entry";
            }


            else if (((_status == 3) && (flage == true) && (resultTimesheet == 3)) || ((_status == 3) && (flage == true) && (resultTimesheet == 1)))
            {
               
                lblError.Text = "Timesheet has been declined";

            
                 TimeMail.Visible = true;
                    string subjectline = string.Empty;
                    subjectline = "Declined";
                    string Heading = string.Empty;
                    Heading = "Your timesheet has been declined";

                    if ((HiddenField1.Value != "0") && (HiddenField1.Value != ""))
                    {
                        string[] strs = submitids.Split(',');
                        ArrayList strsList = new ArrayList();
                        for (int i = 0; i < strs.Length; i++)
                        {
                            if (strsList.Contains(strs[i]))
                                continue;
                            strsList.Add(strs[i]);
                        }
                        foreach (string str in strsList)
                        {
                            if (str != "0")
                            {
                                int ContractorID_TimeSheet = Convert.ToInt32(str);
                                TimesheetMailseing(Convert.ToInt32(HiddenField1.Value), subjectline, _status, ContractorID_TimeSheet, TimeSheetIDs);
                                string htmlText = string.Empty;
                                StringWriter sw = new StringWriter();
                                Html32TextWriter htmlTW = new Html32TextWriter(sw);
                                TimeMail.RenderControl(htmlTW);
                                htmlText = htmlTW.InnerWriter.ToString();
                                if (htmlText != "")
                                {
                                    string errorString = string.Empty;
                                    Email eMail = new Email();
                                    eMail.TimesheetMailIDs(GetResourceMailID(Convert.ToInt32(contractorid)), Heading, htmlText);
                                }
                            }
                        }
                    }
                    }
                     else if ((HiddenField3.Value != "0") && (HiddenField3.Value != ""))
                    {
                        //GetWCDATEID
                        string subjectline = string.Empty;
                        subjectline = "Declined";
                        string Heading = string.Empty;
                        Heading = "Your timesheet has been declined";
                        TimeSheetAdminApprove GetIDs = new TimeSheetAdminApprove();
                        DataTable DT = new DataTable();
                        DT = GetIDs.GetWCDATEID((HiddenField3.Value).ToString());
                        using (DataTableReader reader = DT.CreateDataReader())
                        {
                            while (reader.Read())
                            {
                               
                                int WCDate_Timesheet = Convert.ToInt32(reader["WCDateID"].ToString());

                                TimeSheetAdminApprove ConID = new TimeSheetAdminApprove();
                                DataTable CID = ConID.ContractorID(WCDate_Timesheet);

                                   string[] strs = submitids.Split(',');

                                        ArrayList strsList = new ArrayList();

                                        for (int i = 0; i < strs.Length; i++)
                                        {
                                            if (strsList.Contains(strs[i]))
                                                continue;
                                            strsList.Add(strs[i]);
                                        }

                                 
                                        foreach (string str in strsList)
                                        {
                                            if (str != "0")
                                            {
                                                int ContractorID_TimeSheet = Convert.ToInt32(str);

                                                TimesheetMailseing(WCDate_Timesheet, subjectline, _status, ContractorID_TimeSheet,TimeSheetIDs);
                                                string htmlText = string.Empty;
                                                StringWriter sw = new StringWriter();
                                                Html32TextWriter htmlTW = new Html32TextWriter(sw);
                                                TimeMail.RenderControl(htmlTW);
                                                htmlText = htmlTW.InnerWriter.ToString();
                                                if (htmlText != "")
                                                {
                                                    string errorString = string.Empty;
                                                    Email eMail = new Email();
                                                    eMail.TimesheetMailIDs(GetResourceMailID(ContractorID_TimeSheet), Heading, htmlText);
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
        finally
        {
            TimeMail.Visible = false;
        }

    }
    #endregion

    #region Approve Mail and Staus

    private void changeStatus(int _status)
    {
        string contractorid = "";
        string contractorid1 = "";
        string TimeSheetIDs = "0";
        string TIMEID = "";
        int resultTimesheet = 0;
        int ProjectID = 0;
        

       
        try
        {
            submitids = "0";
            bool flage = false;
            bool CheckFlag = true;
            ArrayList AppIds = new ArrayList();
            for (int i = 0; i < GridView4.Rows.Count; i++)
            {
                GridViewRow row = GridView4.Rows[i];
                TimeSheetAdminApprove CheckTimesheetApproverslist = new TimeSheetAdminApprove();
             
                bool isChecked = ((CheckBox)row.FindControl("chkSelect2")).Checked;
               contractorid = ((Label)row.FindControl("lblContractorID")).Text;
             
                if(CheckFlag)
                {


                    if (isChecked)
                    {
                        Label lbl1 = ((Label)row.FindControl("lblID"));
                        contractorid1 = ((Label)row.FindControl("lblContractorID")).Text;

                        ProjectID = Convert.ToInt32(((Label)row.FindControl("lblPref")).Text);

                        TimeSheetDataContext timeSheet = new TimeSheetDataContext();
                        var myTimesheet = from t in timeSheet.TimesheetEntries
                                          where t.ID == int.Parse(lbl1.Text) && t.ContractorID == int.Parse(contractorid1) //&& t.PrimeApprover == int.Parse(contractorid1)
                                          select t;

                        if (myTimesheet.Count() > 0)
                        {

                        }
                        else
                        {

                            // ProjectID = Convert.ToInt32(ProjectIDGet);

                            submitids = submitids + "," + contractorid1;
                            TIMEID = ((Label)row.FindControl("lblID")).Text;
                            TimeSheetIDs = TimeSheetIDs + "," + TIMEID;

                            //int y1 = Convert.ToInt32(HiddenField1.Value);
                            int y1 = 0;

                            resultTimesheet = CheckTimesheetresourcesApprovers(Convert.ToInt32(contractorid1), y1, Convert.ToInt32(TIMEID), 1, ProjectID);

                            if (resultTimesheet == 0)
                            {
                                //this Timesheet already submitted.... bu the Timehseet approvers....
                            }
                            else if (resultTimesheet == 1)
                            {
                                //here PrimaryTimesheetapprover approved but optinalTimeheet Approver pending for this resource...
                            }
                            else if (resultTimesheet == 2)
                            {
                                //Optional Timesheet Approver  but primaryTimesheet approver need to approve
                            }
                            else if (resultTimesheet == 3)
                            {

                                TimeSheetAdminApprove UpdateStaus1 = new TimeSheetAdminApprove();
                                int Checkcon = UpdateStaus1.UpdateTimeSheetStaus(Convert.ToInt32(lbl1.Text), _status, TextBox1.Text, Convert.ToInt32(contractorid), sessionKeys.UID);

                                flage = true;
                            }
                            else if (resultTimesheet == 4)
                            {

                            }
                            else if (resultTimesheet == 5)
                            {

                            }
                            else if (resultTimesheet == 6)
                            {
                                // primary timesheet approved already Submitted the Timesheet
                            }
                            else if (resultTimesheet == 7)
                            {
                                //Optional Timesheet approver already submitted
                            }

                        }
                    }
                }
                else
                {
                    if (isChecked)
                    {
                        Label lbl1 = ((Label)row.FindControl("lblID"));
                        contractorid1 = ((Label)row.FindControl("lblContractorID")).Text;
                        submitids = submitids + "," + contractorid1;
                        TIMEID = ((Label)row.FindControl("lblID")).Text;
                        TimeSheetIDs = TimeSheetIDs + "," + TIMEID;
                        ProjectID = Convert.ToInt32(((Label)row.FindControl("lblPref")).Text);
                        //int y1 = Convert.ToInt32(HiddenField1.Value);
                        int y1 = 0;

                        resultTimesheet = CheckTimesheetresourcesApprovers(Convert.ToInt32(contractorid1), y1, Convert.ToInt32(TIMEID), 1, ProjectID);

                      //  resultTimesheet = CheckTimesheetresourcesApprovers(Convert.ToInt32(contractorid1), y1, Convert.ToInt32(TIMEID), 1, ProjectID);

                        if (resultTimesheet == 0)
                        {
                            //this Timesheet already submitted.... bu the Timehseet approvers....
                        }
                        else if (resultTimesheet == 1)
                        {
                            //here PrimaryTimesheetapprover approved but optinalTimeheet Approver pending for this resource...
                        }
                        else if (resultTimesheet == 2)
                        {
                            //Optional Timesheet Approver  but primaryTimesheet approver need to approve
                        }
                        else if (resultTimesheet == 3)
                        {

                            TimeSheetAdminApprove UpdateStaus1 = new TimeSheetAdminApprove();
                            int Checkcon = UpdateStaus1.UpdateTimeSheetStaus(Convert.ToInt32(lbl1.Text), _status, TextBox1.Text, Convert.ToInt32(contractorid), sessionKeys.UID);

                            flage = true;
                        }
                        else if (resultTimesheet == 4)
                        {

                        }
                        else if (resultTimesheet == 5)
                        {

                        }
                        else if (resultTimesheet == 6)
                        {
                            // primary timesheet approved already Submitted the Timesheet
                        }
                        else if (resultTimesheet == 7)
                        {
                            //Optional Timesheet approver already submitted
                        }


                      



                    }
                }



            }


            if ((flage == false) && (resultTimesheet == 2) && (_status == 4))
            {
                //lblError.Text = "Sorry, you do not have permission to approve this timesheet";
            }
            else if ((resultTimesheet == 0) && (flage == false) && (_status == 4))
            {
                lblError.Text = "This timesheet is already Submitted";
            }

            else if ((resultTimesheet == 1) && (flage == false) && (_status == 4))
            {
                lblError.Text = "Timesheet has been sent to secondary approver";
            }
            else if ((resultTimesheet == 2) && (flage == false) && (_status == 4))
            {
                lblError.Text = "Optional Timesheet Approver  but primaryTimesheet approver need to approve";
            }
            else if ((resultTimesheet == 6) && (flage == false) && (_status == 4))
            {
                lblError.Text = "Primary timesheet approver has already approved this timesheet entry";
            }

            else if ((resultTimesheet == 7) && (flage == false) && (_status == 4))
            {
                lblError.Text = "Secondary Timesheet approver has already approved this timesheet entry";
            }

            else if ((_status == 4) && (flage == true) && (resultTimesheet == 3))
            {
                lblError.Text = "Timesheet approved";
                TimeMail.Visible = true;
                string subjectline = string.Empty;
                subjectline = "approved";
                string Heading = string.Empty;
                Heading = "Timesheet approved";

                if ((HiddenField1.Value != "0") && (HiddenField1.Value != ""))
                {

                    string[] strs = submitids.Split(',');
                    ArrayList strsList = new ArrayList();
                    for (int i = 0; i < strs.Length; i++)
                    {
                        if (strsList.Contains(strs[i]))
                            continue;
                        strsList.Add(strs[i]);
                    }
                    foreach (string str in strsList)
                    {
                        if (str != "0")
                        {
                            int ContractorID_TimeSheet = Convert.ToInt32(str);
                            TimesheetMailseing(Convert.ToInt32(HiddenField1.Value), subjectline, _status, ContractorID_TimeSheet, TimeSheetIDs);
                            string htmlText = string.Empty;
                            StringWriter sw = new StringWriter();
                            Html32TextWriter htmlTW = new Html32TextWriter(sw);
                            TimeMail.RenderControl(htmlTW);
                            htmlText = htmlTW.InnerWriter.ToString();
                            if (htmlText != "")
                            {
                                string errorString = string.Empty;
                                Email eMail = new Email();
                                eMail.TimesheetMailIDs(GetResourceMailID(Convert.ToInt32(contractorid)), Heading, htmlText);
                            }
                        }
                    }
                }
                else if ((HiddenField3.Value != "0") && (HiddenField3.Value != ""))
                {
                    //GetWCDATEID
                    TimeSheetAdminApprove GetIDs = new TimeSheetAdminApprove();
                    DataTable DT = new DataTable();
                    DT = GetIDs.GetWCDATEID((HiddenField3.Value).ToString());
                    string htmlText = string.Empty;
                     int ContractorID_TimeSheet=0;
                    using (DataTableReader reader = DT.CreateDataReader())
                    {
                        while (reader.Read())
                        {
                            int WCDate_Timesheet = Convert.ToInt32(reader["WCDateID"].ToString());
                            TimeSheetAdminApprove ConID = new TimeSheetAdminApprove();
                            DataTable CID = ConID.ContractorID(WCDate_Timesheet);
                            string[] strs = submitids.Split(',');

                            ArrayList strsList = new ArrayList();

                            for (int i = 0; i < strs.Length; i++)
                            {
                                if (strsList.Contains(strs[i]))
                                    continue;
                                strsList.Add(strs[i]);
                            }
                            foreach (string str in strsList)
                            {
                                if (str != "0")
                                {
                                    ContractorID_TimeSheet = Convert.ToInt32(str);

                                    DataSet ds = new DataSet();
                                    ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimesheetApproveInfo", new SqlParameter("@WCDateID", WCDate_Timesheet), new SqlParameter("@Status", _status), new SqlParameter("@ContractorID", ContractorID_TimeSheet), new SqlParameter("@TimesheetID", TimeSheetIDs));
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {


                                        TimesheetMailseing(WCDate_Timesheet, subjectline, _status, ContractorID_TimeSheet, TimeSheetIDs);

                                        StringWriter sw = new StringWriter();
                                        Html32TextWriter htmlTW = new Html32TextWriter(sw);
                                        TimeMail.RenderControl(htmlTW);
                                        htmlText = htmlTW.InnerWriter.ToString();
                                        if (htmlText != "")
                                        {
                                            string errorString = string.Empty;
                                            Email eMail = new Email();
                                            eMail.TimesheetMailIDs(GetResourceMailID(ContractorID_TimeSheet), Heading, htmlText);
                                        }
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
        finally
        {
            TimeMail.Visible = false;
        }


    }
    #endregion

    #region MailSending
    public void TimesheetMailseing(int WCDATE_ID, string TypeofApprove, int TimesheetStatusID, int ContractorID_TimesheetID, string TimesheetID)
    {
       // TimeMail.Visible = true;
        TimeMail.TimeSheetApproval_BindData(WCDATE_ID, TextBox1.Text, TypeofApprove, TimesheetStatusID, ContractorID_TimesheetID, TimesheetID);
    }

   



    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    #endregion

    #region GetResourceMailID Address
    public string GetResourceMailID(int contractorID)
    {
        string GetResourceMail = string.Empty;

        TimeSheetAdminApprove getResourceMaildi = new TimeSheetAdminApprove(); ;
        GetResourceMail = getResourceMaildi.GetTimesheet_ResourceMailID(contractorID);

        return GetResourceMail;

        // return SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select (select EmailAddress from Contractors as c where c.ID = Contractors.TimeApproveID) as Email  from Contractors where ID = @UID", new SqlParameter("@UID", sessionKeys.UID)).ToString();
    }
    #endregion
    #endregion

    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
          
            GridView4.PageIndex = e.NewPageIndex;
            DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {

        }

   

    }

    ////protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    ////{
    ////    GridView2.PageIndex = e.NewPageIndex;
    ////     BindPendingGridView();
    ////}

    public string ChangePlanner(string Planner)
    {
        string GetActivity = "";

        if (Planner == "Y")
        {
            GetActivity = "Yes";
        }
        else
        {
            GetActivity = "No";
        }

        return GetActivity;
    }


    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("chkAll1")).Attributes.Add("onclick", "javascript:SelectAll1('" +
                    ((CheckBox)e.Row.FindControl("chkAll1")).ClientID + "')");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];
            if (objList != null)
            {
                if (e.Row.FindControl("ddlTitle") != null)
                {
                    DropDownList ddlTitle = e.Row.FindControl("ddlTitle") as DropDownList;
                    Label ContratorID = e.Row.FindControl("lblContractorID") as Label;
                     conID = Convert.ToInt32(ContratorID.Text);

                    ddlTitle.DataSource = GetProjectList();
                    ddlTitle.DataTextField = "ProjectTitle";
                    ddlTitle.DataValueField = "ProjectReference";
                    ddlTitle.DataBind();
                    ddlTitle.SelectedValue = objList[3].ToString();
                    
                    DropDownList ddlProjectTasks = e.Row.FindControl("GetProjectTasks") as DropDownList;
                    //ddlProjectTasks.DataSource = GetProjectTaskList(int.Parse(objList[3].ToString()));
                    ddlProjectTasks.DataSource = GetProjectTaskList(int.Parse(ddlTitle.SelectedValue.ToString()));
                    ddlProjectTasks.DataTextField = "ProjectTask";
                    ddlProjectTasks.DataValueField = "TaskID";
                    ddlProjectTasks.DataBind();
                    try
                    {
                        ddlTitle.SelectedValue = objList[3].ToString();

                        ddlProjectTasks.SelectedValue = objList[4].ToString();
                    }
                    catch (Exception ex)
                    {
                        ddlTitle.SelectedIndex = 0;
                        ddlProjectTasks.SelectedIndex = 0;
                    }

                }
            }
        }
    }

    private DataTable GetProjectList()
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimeSheet_ProjectTile", new SqlParameter("@ContractorID", conID)).Tables[0];
    }
    private DataTable GetProjectTaskList(int project)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_GetProjectTaks", new SqlParameter("@ProjectReference", project), new SqlParameter("@contractorID", conID)).Tables[0];
    }
    #region Sort The gird
    
   
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
    
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            dvProducts.Sort = sortExpression + DESCENDING1;
            BindGridView();
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;
            dvProducts.Sort = sortExpression + ASCENDING1;
            BindGridView();
        }     


    }

    
   
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;

            return (SortDirection)ViewState["sortDirection"];
        }
        set { ViewState["sortDirection"] = value; }
    }
    private void BindGridView()
    {
        try
        {

            GridView1.DataSource = dvProducts;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {

        }
    }
    ////private void BindDecline()
    ////{
    ////    try
    ////    {

    ////        GridView5.DataSource = dvProducts2;
    ////        GridView5.DataBind();
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        LogExceptions.WriteExceptionLog(ex);
    ////    }
    ////    finally
    ////    {

    ////    }
    ////}
    #endregion

    #region Sort Gridview Two

    ////private void BindPendingGridView()
    ////{
    ////    try
    ////    {
    ////        GridView2.DataSource = dvProducts1;
    ////        GridView2.DataBind();
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        LogExceptions.WriteExceptionLog(ex);
    ////    }
    ////    finally
    ////    {

    ////    }
    ////}
    public SortDirection GridViewSortDirection_PendingStatus
    {
        get
        {
            if (ViewState["sortDirection_PendingStatus"] == null)
                ViewState["sortDirection_PendingStatus"] = SortDirection.Ascending;

            return (SortDirection)ViewState["sortDirection_PendingStatus"];
        }
        set { ViewState["sortDirection_PendingStatus"] = value; }
    }
    ////protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    ////{

    ////    string sortExpression = e.SortExpression;

    ////    if (GridViewSortDirection_PendingStatus == SortDirection.Ascending)
    ////    {
    ////        GridViewSortDirection_PendingStatus = SortDirection.Descending;
    ////        dvProducts1.Sort = sortExpression + DESCENDING2;
    ////        BindPendingGridView();
    ////    }
    ////    else
    ////    {
    ////        GridViewSortDirection_PendingStatus = SortDirection.Ascending;
    ////        dvProducts1.Sort = sortExpression + ASCENDING2;
    ////        BindPendingGridView();
    ////    }     


    ////}
    #endregion


    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EmailReminder")
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());

                GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

           
                string contractorID = ((Label)gvrow.FindControl("lblContractorName_ID")).Text;
                    int contractorid1=Convert.ToInt32(contractorID);


                    string Heading = "Reminder to submit your timesheet";

                  Pendingmail.Visible = true;
                TimesheetMailseing_Pendingstate(ID, 1, Convert.ToInt32(contractorid1), Convert.ToInt32(sessionKeys.UID));
                string htmlText = string.Empty;
                StringWriter sw = new StringWriter();
                Html32TextWriter htmlTW = new Html32TextWriter(sw);
                Pendingmail.RenderControl(htmlTW);
                htmlText = htmlTW.InnerWriter.ToString();
                string errorString = string.Empty;
                Email eMail = new Email();
                string EmailAddress = string.Empty;

                EmailAddress = GetResourceMailID(Convert.ToInt32(contractorid1));
                eMail.TimesheetMailIDs(EmailAddress, Heading, htmlText);
           

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            finally
            {

                Pendingmail.Visible = false;
            }
        }
        
    }
    public void TimesheetMailseing_Pendingstate(int WCDATE_ID,  int TimesheetStatusID, int ContractorID_TimesheetID, int TimesheetApproverID)
    {
        // TimeMail.Visible = true;
        Pendingmail.TimeSheetApproval_pending_BindData(WCDATE_ID,TimesheetStatusID, ContractorID_TimesheetID, TimesheetApproverID);
    }

    public int CheckTimesheetresourcesApprovers(int contractorID, int WCDateID,int TimesheetEntryID,int ApproverType,int ProjectReference)
    {
        int Resultflag = 0;
        int ProjectOwnerID = 0;
        int updateflag = 0;
       // int resultTimesheet;
        bool CheckApprovers = false;
       // int y1 = Convert.ToInt32(HiddenField1.Value);

        TimeSheetAdminApprove GetTimesheetApproverslist = new TimeSheetAdminApprove();
        TimeSheetAdminApprove CheckTimesheetApproverslist = new TimeSheetAdminApprove();
        TimeSheetAdminApprove checkStatus = new TimeSheetAdminApprove();
        TimeSheetAdminApprove GetUpdateTimesheetStatusCheck = new TimeSheetAdminApprove();
        TimeSheetAdminApprove CheckFlgStatusandupdate1 = new TimeSheetAdminApprove();

        Chekingtimesheetapprovers CheckProjectOwner = new Chekingtimesheetapprovers();
        //Get the project owner id
       ProjectOwnerID = CheckProjectOwner.GetProjectOwner(ProjectReference);

       if (ProjectOwnerID != 0)
       {


           //check user has exists the approvers or not
           CheckApprovers = CheckTimesheetApproverslist.CheckTimesheetApproversExists(Convert.ToInt32(contractorID));
           Chekingtimesheetapprovers CheckTimesheetApprover = new Chekingtimesheetapprovers();




           if ((CheckApprovers == true))
           {

               DataSet listofapprovers = new DataSet();
               //Get the primary and secondary approver 
               listofapprovers = GetTimesheetApproverslist.GetApprovallist(Convert.ToInt32(contractorID));

               if (listofapprovers.Tables[0].Rows.Count > 0)
               {
                   TimesheetPrimaryApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][0].ToString());
                   TimesheetOptionalApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][1].ToString());

               }

               //check primary and secondary approvers are equeal or not
               if (TimesheetPrimaryApprover == TimesheetOptionalApprover)
               {

                   GetTimesheetApproverNow = sessionKeys.UID;
                   updateflag = 1;
                   Resultflag = updateflag;

               }
               else
               {

                   //check logged in user is project owner or not
                   if (ProjectOwnerID == sessionKeys.UID)
                   {
                       GetTimesheetApproverNow = ProjectOwnerID;
                       updateflag = 4;
                       Resultflag = updateflag;

                   }
                   else
                   {
                       //check logged in user is primary appover or not
                       if (TimesheetPrimaryApprover == sessionKeys.UID)
                       {
                           GetTimesheetApproverNow = sessionKeys.UID;
                           updateflag = 2;
                           Resultflag = updateflag;
                       }
                       else
                       {
                           GetTimesheetApproverNow = sessionKeys.UID;
                           updateflag = 3;
                           Resultflag = updateflag;
                       }
                   }

               }


               //now need to check the timesheet approve list.Chekingtimesheetapprovers.cs class

               //updateflag=2 is promary Timesheet Approver. here i am checking his status.
               if (updateflag == 2)
               {

                   Resultflag = CheckTimesheetApprover.checkApprovertimesheet(TimesheetEntryID, updateflag, 0, 1);

               }
               else if (updateflag == 3)
               {
                   Resultflag = CheckTimesheetApprover.checkApprovertimesheet(TimesheetEntryID, 0, updateflag, 1);

               }
               else if (updateflag == 4)
               {

                   //here it is the projectOwner Approver............
                   Resultflag = CheckTimesheetApprover.checkApprovertimesheet(TimesheetEntryID, updateflag, 0, 1);

               }

           }


           else
           {
               //here only one approver will approve the Timesheet.

               if (TimesheetPrimaryApprover == sessionKeys.UID)
               {
                   GetTimesheetApproverNow = sessionKeys.UID;
                   updateflag = 2;
                   Resultflag = updateflag;
               }
               else
               {
                   GetTimesheetApproverNow = sessionKeys.UID;
                   updateflag = 3;
                   Resultflag = updateflag;
               }
               Chekingtimesheetapprovers checksingleTimesheetapprover = new Chekingtimesheetapprovers();
               int GetResults = checksingleTimesheetapprover.TimesheetsingleApprover(TimesheetEntryID, 2, 3, 3);
               if (GetResults == 0)
               {
                   Resultflag = 0;
               }
               else
               {
                   Resultflag = 3;
               }



               //Resultflag = 4;


           }
       }
       else
       {


           CheckApprovers = CheckTimesheetApproverslist.CheckTimesheetApproversExists(Convert.ToInt32(contractorID));
           Chekingtimesheetapprovers CheckTimesheetApprover = new Chekingtimesheetapprovers();




           if ((CheckApprovers == true))
           {

               DataSet listofapprovers = new DataSet();
               listofapprovers = GetTimesheetApproverslist.GetApprovallist(Convert.ToInt32(contractorID));

               if (listofapprovers.Tables[0].Rows.Count > 0)
               {
                   TimesheetPrimaryApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][0].ToString());
                   TimesheetOptionalApprover = Convert.ToInt32(listofapprovers.Tables[0].Rows[0][1].ToString());

               }


               if (TimesheetPrimaryApprover == TimesheetOptionalApprover)
               {

                   GetTimesheetApproverNow = sessionKeys.UID;
                   updateflag = 1;
                   Resultflag = updateflag;

               }
               else
               {

                   //get the projojectOwner here
                   //GetProjectOwner

                   if (ProjectOwnerID == sessionKeys.UID)
                   {
                       GetTimesheetApproverNow = ProjectOwnerID;
                       updateflag = 4;
                       Resultflag = updateflag;

                   }
                   else
                   {
                       if (TimesheetPrimaryApprover == sessionKeys.UID)
                       {
                           GetTimesheetApproverNow = sessionKeys.UID;
                           updateflag = 2;
                           Resultflag = updateflag;
                       }
                       else
                       {
                           GetTimesheetApproverNow = sessionKeys.UID;
                           updateflag = 3;
                           Resultflag = updateflag;
                       }
                   }
               }

               //now need to check the timesheet approve list.Chekingtimesheetapprovers.cs class

               //updateflag=2 is promary Timesheet Approver. here i am checking his status.
               if ((updateflag == 2)||(updateflag==4))
               {

                   Resultflag = CheckTimesheetApprover.checkApprovertimesheet(TimesheetEntryID, updateflag, 0, 1);

               }
               else if (updateflag == 3)
               {
                   Resultflag = CheckTimesheetApprover.checkApprovertimesheet(TimesheetEntryID, 0, updateflag, 1);

               }

           }
           else
           {
               //here only one approver will approve the Timesheet.

               if (TimesheetPrimaryApprover == sessionKeys.UID)
               {
                   GetTimesheetApproverNow = sessionKeys.UID;
                   updateflag = 2;
                   Resultflag = updateflag;
               }
               else
               {
                   GetTimesheetApproverNow = sessionKeys.UID;
                   updateflag = 3;
                   Resultflag = updateflag;
               }
               Chekingtimesheetapprovers checksingleTimesheetapprover = new Chekingtimesheetapprovers();
               int GetResults = checksingleTimesheetapprover.TimesheetsingleApprover(TimesheetEntryID, 2, 3, 3);
               if (GetResults == 0)
               {
                   Resultflag = 0;
               }
               else
               {
                   Resultflag = 3;
               }
           }

       }

        return Resultflag;
    }
    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = GridView4.EditIndex;
                GridViewRow Row = GridView4.Rows[i];
                int ProjectReference1 = Convert.ToInt32(((DropDownList)Row.FindControl("ddlTitle")).SelectedItem.Value);
               int UpdateProjectTaskID = Convert.ToInt32(((DropDownList)Row.FindControl("GetProjectTasks")).SelectedItem.Value);
                int contractorID=0;
                contractorID = Convert.ToInt32(((Label)Row.FindControl("lblContractorID")).Text);
                int TimesheetentryType=0;
                TimesheetentryType = Convert.ToInt32(((Label)Row.FindControl("lblEntryType")).Text);
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
               Chekingtimesheetapprovers UpdateTimesheetDeatilsentry = new Chekingtimesheetapprovers();
               UpdateTimesheetDeatilsentry.updatedetailsTimesheet(ID, ProjectReference1, UpdateProjectTaskID, Hours1, contractorID, TimesheetentryType);


               DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            finally {
                DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);
            }
            
        }

        ModalControlExtender2.Show();

    }
    protected void GridView4_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView4.EditIndex = e.NewEditIndex;
        DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);
        ModalControlExtender2.Show();
    }
    protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            int index = GridView4.EditIndex;
            GridViewRow row = GridView4.Rows[index];

            DropDownList ddl = (DropDownList)row.FindControl("ddlTitle");
            Label lblContractorID1 = (Label)row.FindControl("lblContractorID");
            string GetValue = ddl.SelectedValue;

            //DropDownList ddlprojects1 = (DropDownList)GridView1.Rows[0].FindControl("ddlTitle");
            DropDownList GetEditProjecttasks = (DropDownList)row.FindControl("GetProjectTasks");

            //String Project = ddlprojects1.SelectedValue.ToString();
            DataTable GetTaskFill = new DataTable();
            SqlCommand cmd1 = new SqlCommand("DN_GetProjectTaks1", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@ProjectReference", SqlDbType.Int, 4).Value = Convert.ToInt32(GetValue);
            cmd1.Parameters.Add("@contractorID", SqlDbType.Int, 4).Value = Convert.ToInt32(lblContractorID1.Text);

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(GetTaskFill);

            GetEditProjecttasks.DataSource = GetTaskFill;
            GetEditProjecttasks.DataTextField = "ProjectTask";
            GetEditProjecttasks.DataValueField = "TaskID";
            GetEditProjecttasks.DataBind();
            ModalControlExtender2.Show();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridView4_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
         
        GridView4.EditIndex = -1;
        DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);
        ModalControlExtender2.Show();
    }


    public void DeatislTimesheetGridDisplay(string firstgridhiddenvalue, string secondgridhiddenvalue, string thirdgridjihhenvalue)
    {
        try
        {
            TimeSheetAdminApprove ViewTimeSheet11 = null;
            ViewTimeSheet11 = new TimeSheetAdminApprove();
          
            if ((firstgridhiddenvalue != "") && (secondgridhiddenvalue == "") && (thirdgridjihhenvalue == ""))
            {
                GridView4.DataSource = ViewTimeSheet11.ViewTimeSheet_SubmittApproval(Convert.ToInt32(firstgridhiddenvalue), 2, Convert.ToInt32(HiddenField4.Value), Convert.ToInt32(sessionKeys.UID));
                GridView4.DataBind();
            }
            else if ((secondgridhiddenvalue != "") && (firstgridhiddenvalue == "") && (thirdgridjihhenvalue == ""))
            {
                GridView4.DataSource = ViewTimeSheet11.ViewTimeSheet_SubmittApproval(Convert.ToInt32(secondgridhiddenvalue), 3, 0, Convert.ToInt32(sessionKeys.UID));
                GridView4.DataBind();
            }

            else if ((thirdgridjihhenvalue != "") && (secondgridhiddenvalue == "") && (firstgridhiddenvalue == ""))
            {
                TimeSheetAdminApprove PendingTimesheet = null;
                PendingTimesheet = new TimeSheetAdminApprove();
                if (thirdgridjihhenvalue == "S")
                {
                    GridView4.DataSource = PendingTimesheet.ViewTimeSheet_SubmittApproval1("", 1, Convert.ToInt32(sessionKeys.UID));
                    GridView4.DataBind();
                }
                else
                {
                    GridView4.DataSource = PendingTimesheet.ViewTimeSheet_SubmittApproval1(thirdgridjihhenvalue, 1, Convert.ToInt32(sessionKeys.UID));
                    GridView4.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }

    protected void GridView4_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView4.EditIndex = -1;
        DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);
        ModalControlExtender2.Show();
    }
    ////protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
    ////{
    ////     GridView5.PageIndex =e.NewPageIndex;
    ////     delineTimesheetGrid();
    ////}
    ////protected void GridView5_Sorting(object sender, GridViewSortEventArgs e)
    ////{
    ////    string sortExpression = e.SortExpression;

    ////    if (GridViewSortDirection == SortDirection.Ascending)
    ////    {
    ////        GridViewSortDirection = SortDirection.Descending;
    ////        dvProducts2.Sort = sortExpression + DESCENDING3;
    ////        delineTimesheetGrid();
    ////    }
    ////    else
    ////    {
    ////        GridViewSortDirection = SortDirection.Ascending;
    ////        dvProducts2.Sort = sortExpression + ASCENDING3;
    ////        delineTimesheetGrid();
    ////    }     

    ////}
    protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeclineTimesheet")
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                HiddenField2.Value = ID.ToString();
                HiddenField1.Value = "";

                HiddenField3.Value = "";
                GetViewofSendforApproval.Visible = true;
                //GetIDAccept.Visible = false;
                GetIDAccept.Visible = true;
                TextBox1.Visible = false;
                btn_approve.Visible = false;
                btn_declined.Visible = false;
                ImgClose.Visible = true;
                TimeSheetAdminApprove ViewTimeSheet = null;
                ViewTimeSheet = new TimeSheetAdminApprove();

                GridView4.DataSource = ViewTimeSheet.ViewTimeSheet_SubmittApproval(ID, 4, 0, Convert.ToInt32(sessionKeys.UID));
                GridView4.Columns[0].Visible = false;
                GridView4.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           
        }
    }

    #region new implementations
    private void Timesheet_ApproveAll()
    {
        bool isSelectd = false;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GridViewRow row = GridView1.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;

            if (isChecked)
            {
                isSelectd = true;
                //get week commencing date id
                Label lblWCdateID = ((Label)row.FindControl("lblWCDateID"));
                Label lblResourceID = (Label)row.FindControl("lblContractorID1");

               

                //Approver by WCdateID
                ApproveByWCdateID(int.Parse(lblWCdateID.Text), int.Parse(lblResourceID.Text));
                isSelectd = true;
                   

                
            }
        }

        if(!isSelectd)
            lblError.Text = "Please select timesheet";
    }

    private void ApproveByWCdateID(int WCdateID, int ResourceID)
    {
        try
        {
            SqlParameter outval = new SqlParameter("@Outval", SqlDbType.Int);
            outval.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_TimesheetEntryByWcdate_Approve", new SqlParameter("@WCdateID", WCdateID), new SqlParameter("@ApproverID", sessionKeys.UID), outval);

            int retval = int.Parse(outval.Value.ToString());
            //pending primary approve 
            if (retval == 1)
                lblError.Text = "";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        

    }

    private void Timesheet_Approve()
    {
        for (int i = 0; i < GridView4.Rows.Count; i++)
        {
            GridViewRow row = GridView4.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkSelect2")).Checked;

            if (isChecked)
            {
                //get timesheet id
                Label lblTimesheetid = ((Label)row.FindControl("lblID"));
                //Approver by timesheet ID
                ApproveByID(int.Parse(lblTimesheetid.Text), TextBox1.Text.Trim());
            }
        }
    }

    private void ApproveByID(int TimesheetID,string Comments)
    {
        try
        {
            for (int i = 0; i < GridView4.Rows.Count; i++)
            {
                GridViewRow row = GridView4.Rows[i];
                Label lbl1 = ((Label)row.FindControl("lblID"));
                string contractorid1 = "";
                int ProjectID;
                contractorid1 = ((Label)row.FindControl("lblContractorID")).Text;

                ProjectID = Convert.ToInt32(((Label)row.FindControl("lblPref")).Text);
                bool isChecked = ((CheckBox)row.FindControl("chkSelect2")).Checked;
                if (isChecked)
                {
                    //TimeSheetDataContext timeSheet = new TimeSheetDataContext();
                   
                    //var myTimesheet = from t in timeSheet.TimesheetEntries
                    //                  where t.ID == int.Parse(lbl1.Text) && t.ContractorID == int.Parse(contractorid1) // || t.PrimeApprover == int.Parse(contractorid1)
                    //                  select new {ID= t.ID, ProjectRef = t.ProjectReference };
                    
                    //if (myTimesheet.Count() > 0)
                    //{
                    //    lblError.Visible = true;
                    //    lblError.Text = "Sorry but you cannot approve timesheets where you are the owner/primary approver of the project";
                    //}
                    //else
                    //{

                        SqlParameter outval = new SqlParameter("@Outval", SqlDbType.Int);
                        outval.Direction = ParameterDirection.Output;
                        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_TimesheetEntry_Approve", new SqlParameter("@timesheetID", TimesheetID), new SqlParameter("@ApproverID", sessionKeys.UID),new SqlParameter("@Comments", Comments), outval);

                        int retval = int.Parse(outval.Value.ToString());
                        //pending primary approve 
                        if (retval == 0)
                        {
                            lblError.Visible = true;
                            lblError.Text = "Sorry but you cannot approve timesheets where you are the owner/primary approver of the project";
                        }
                        //else
                        //{
                         //pending secondary approve
                        //if (retval == 2)
                        //    lblError.Visible = true;
                        //    lblError.Text = "Secondary timesheet approver should approve timesheet(s)";
                        //else
                        //lblError.Text = "";
                    //}
                }
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    #endregion



    #region Decline mail

    //private int GetCustomerUser(int ProjectReference)
    //{
    //    projectTaskDataContext pd = new projectTaskDataContext();

    //    var cu = from p in pd.ProjectDefaults

    //}
    public IEnumerable<DeclineTimesheetMail> GetDeclinemailData(string timesheetids)
    {
        List<DeclineTimesheetMail> declineTimesheetMail = new List<DeclineTimesheetMail>();
        try
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Timesheetentry_declineMail", new SqlParameter("@timesheetids", timesheetids)).Tables[0];
            int R_cont = dt.Rows.Count;
            if (R_cont > 0)
            {
                for (int T1_cnt = 0; T1_cnt <= dt.Rows.Count - 1; T1_cnt++)
                {
                    var emp = new DeclineTimesheetMail
                    {
                        ContractorID = int.Parse(dt.Rows[T1_cnt]["ContractorID"].ToString()),
                        DeclineByName = dt.Rows[T1_cnt]["DeclineByName"].ToString(),
                        EntryType = dt.Rows[T1_cnt]["EntryTypeName"].ToString(),
                        Hours=double.Parse(dt.Rows[T1_cnt]["Hours"].ToString()),
                        ProjectTitle = dt.Rows[T1_cnt]["ProjectTitle"].ToString(),
                        Resource_email = dt.Rows[T1_cnt]["Resource_email"].ToString(),
                        ResourceName = dt.Rows[T1_cnt]["ResourceName"].ToString(),
                        TaskName = dt.Rows[T1_cnt]["TaskName"].ToString(),
                        WCDate = Convert.ToDateTime(dt.Rows[T1_cnt]["WCDate"].ToString()),
                        Approver_Notes = dt.Rows[T1_cnt]["ApproverComments"].ToString(),
                        DateEntered = Convert.ToDateTime(dt.Rows[T1_cnt]["DateEntered"].ToString())

                    };
                    declineTimesheetMail.Add(emp);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return declineTimesheetMail;
    }

    private string SendDdeclineMail(string timesheetids)
    {
        Email e = new Email();
        string retval = string.Empty;
        IEnumerable<DeclineTimesheetMail> declineTimesheetMail;
        declineTimesheetMail = GetDeclinemailData(timesheetids);
        string MailFormat = Deffinity.MailFormat.MailCss("Timesheet");

        string mailContent = string.Empty;

        var Contractor = (from r in declineTimesheetMail select r.ContractorID).Distinct();
        //subprogramme column
        string strTop = string.Empty;
        string toemail = string.Empty;
        string wcdate = string.Empty;
        foreach (int cu in Contractor)
        {


            wcdate =declineTimesheetMail.Where(p => p.ContractorID == cu).Select(p => p.WCDate.ToShortDateString()).FirstOrDefault();
            strTop = MailFormat + string.Format("<table align='center' width='600' style='border:5px solid #b3b3b3; margin-top:10px;' cellspacing='0' cellpadding='0'>  <tr>    <td height='30' valign='top' ><img src='{0}/media/deffinity_logo.gif'  alt=''/></td><td><table width='300' border='0' cellspacing='0' cellpadding='0' align='right' style='float:right'><tr> <td class='hdr1'>Timesheet</td> </tr></table> </td></tr> <tr>  <td height='9' colspan='2' ><img src='{0}/images/border.gif' style='height:5px;width:580px' alt=''> </img></td>  </tr>  <tr>    <td colspan='2'>", System.Configuration.ConfigurationManager.AppSettings["Weburl"].ToString());
            strTop = strTop + "";
            strTop = strTop + "<table style='width:100%'> ";
            strTop = strTop + "<tr> <td>";
            strTop = strTop + string.Format("Dear <b>{0}</b></tr> </td>", declineTimesheetMail.Where(p => p.ContractorID == cu).Select(p => p.ResourceName).FirstOrDefault());
            strTop = strTop + string.Format("<tr> <td>The following entries within your timesheet for WC: <b>{0}</b> have been declined:</td></tr><tr> <td>", wcdate);

            strTop = strTop + "<table style='width:100%' class='Grid_table'> ";
            strTop = strTop + "<tr class='hdrt'> <td> <b>Date</b> </td><td> <b>Project</b> </td><td><b> Task</b> </td><td><b> Entry Type</b> </td><td><b> Hours</b> </td><td><b>Reason</b> </td><td><b>Declined by</b> </td></tr>";

            var CustomerUserDetails = (from r in declineTimesheetMail where r.ContractorID == cu select r);

            bool i = true;
            foreach (DeclineTimesheetMail du in CustomerUserDetails)
            {
                if (i)
                {
                    strTop = strTop + string.Format("<tr class='odd_row'><td>{0}</td><td>{1}</td><td>{4}</td><td>{2}</td><td>{3}</td><td>{5}</td><td>{6}</td> </tr>", du.DateEntered.ToShortDateString(), du.ProjectTitle, du.EntryType, string.Format("{0:F2}", du.Hours), du.TaskName, du.Approver_Notes, du.DeclineByName);
                    i = false;
                }
                else
                {
                    strTop = strTop + string.Format("<tr class='even_row'><td>{0}</td><td>{1}</td><td>{4}</td><td>{2}</td><td>{3}</td><td>{5}</td><td>{6}</td> </tr>", du.DateEntered.ToShortDateString(), du.ProjectTitle, du.EntryType, string.Format("{0:F2}", du.Hours), du.TaskName, du.Approver_Notes, du.DeclineByName);
                    i = true;
                }

            }


            strTop = strTop + "</table> ";


            strTop = strTop + " </td></tr>";
            strTop = strTop + string.Format("<tr> <td> Please <a href='{0}'> click here </a> to access the system. </td></tr>", System.Configuration.ConfigurationManager.AppSettings["Weburl"].ToString());
            strTop = strTop + "<tr> <td>  Thank you. </td></tr>";
            strTop = strTop + string.Format("<tr> <td> <a href='{0}'> {0} </a>  </td></tr>", System.Configuration.ConfigurationManager.AppSettings["Weburl"].ToString());
            strTop = strTop + "</table>";
            strTop = strTop + "</td></tr></table> ";
            strTop = strTop + "</html>";
            toemail = declineTimesheetMail.Where(p => p.ContractorID == cu).Select(p => p.Resource_email).FirstOrDefault();
            e.SendingMail(toemail, string.Format("Timesheet for WC {0} has been declined", wcdate), strTop);
            
        }
        



        return retval;
    }

    #endregion
    public class DeclineTimesheetMail
    {
        public DateTime WCDate { get; set; }
        public DateTime DateEntered { get; set; }
        public string ResourceName { get; set; }
        public string ProjectTitle { get; set; }
        public string TaskName { get; set; }
        public string EntryType { get; set; }
        public string DeclineByName { get; set; }
        public double Hours { get; set; }
        public int ContractorID { get; set; }
        public string Resource_email { get; set; }
        public string Approver_Notes { get; set; }
    }

    protected void gvInnerTimeSheet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        GridViewRow row = e.Row;
        if (e.Row.RowType == DataControlRowType.Header)
        {

            ((CheckBox)e.Row.FindControl("chkAll1")).Attributes.Add("onclick", "javascript:SelectAllTimeSheet('" +
                    ((CheckBox)e.Row.FindControl("chkAll1")).ClientID + "')");
        }
        // Make sure we aren't in header/footer rows
        if (row.DataItem == null)
        {
            return;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

             bool isChecked =  ((CheckBox)e.Row.FindControl("chkSelect2")).Checked;
        }

       
    }
    protected void gvInnerTimeSheet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "viewSignoff")
        {
            try
            {
                string wcdateid_contractorid = e.CommandArgument.ToString();
                string[] darray = wcdateid_contractorid.Split(',');
                BindSignoffGrid(Convert.ToInt32(darray[0]), Convert.ToInt32(darray[1]));
                mdlSignoff.Show();
                getFirstGrid();
                GridView gvTemp = (GridView)sender;
                int GetSubSectionID = Convert.ToInt32(gvTemp.DataKeys[0].Value);
                gvUniqueID = gvTemp.UniqueID;
                getFirstGrid();
                javaScript += "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + GetSubSectionID.ToString() + "','alt');</script>";
                ClientScript.RegisterStartupScript(GetType(), "Expand", javaScript);
            }
            catch(Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        if (e.CommandName == "ApproveClick")
        {
            GridView gvInnerTimeSheet = sender as GridView;
            for (int i = 0; i < gvInnerTimeSheet.Rows.Count; i++)
            {
                bool isChecked = ((CheckBox)gvInnerTimeSheet.Rows[i].FindControl("chkSelect2")).Checked;
                if (isChecked)
                {
                    //txtComments
                    TextBox txtComments = ((TextBox)gvInnerTimeSheet.FooterRow.FindControl("txtComments"));
                    //get timesheet id
                    Label lblTimesheetid = ((Label)gvInnerTimeSheet.Rows[i].FindControl("lblID"));
                    //Approver by timesheet ID

                    SqlParameter outval = new SqlParameter("@Outval", SqlDbType.Int);
                    outval.Direction = ParameterDirection.Output;
                    SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_TimesheetEntry_Approve", new SqlParameter("@timesheetID", int.Parse(lblTimesheetid.Text)), new SqlParameter("@ApproverID", sessionKeys.UID), new SqlParameter("@Comments", txtComments.Text.Trim()), outval);

                    int retval = int.Parse(outval.Value.ToString());
                    //pending primary approve 
                    if (retval == 0)
                    {
                        lblError.Visible = true;
                        lblError.Text = "Sorry but you cannot approve timesheets where you are the owner/primary approver of the project";
                    }
                    
                }
            }

            getFirstGrid();
        }
        if (e.CommandName == "DeclineClick")
        {

            string TimeSheetIDs = string.Empty;
            string TIMEID = string.Empty;
            GridView gvInnerTimeSheet = sender as GridView;
            for (int i = 0; i < gvInnerTimeSheet.Rows.Count; i++)
            {
               

                bool isChecked = ((CheckBox)gvInnerTimeSheet.Rows[i].FindControl("chkSelect2")).Checked;
                //contractorid = ((Label)row.FindControl("lblContractorID")).Text;
                if (isChecked)
                {
                    Label lbl1 = ((Label)gvInnerTimeSheet.Rows[i].FindControl("lblID"));
                    //contractorid1 = ((Label)row.FindControl("lblContractorID")).Text;
                    //ProjectID = Convert.ToInt32(((Label)row.FindControl("lblPref")).Text);
                    //submitids = submitids + "," + contractorid1;
                    TIMEID = ((Label)gvInnerTimeSheet.Rows[i].FindControl("lblID")).Text;
                    TimeSheetIDs = TimeSheetIDs + "," + TIMEID;

                }
            }
            if (!string.IsNullOrEmpty(TimeSheetIDs))
            {
                //txtComments
                TextBox txtComments = ((TextBox)gvInnerTimeSheet.FooterRow.FindControl("txtComments"));
                TimeSheetAdminApprove tm = new TimeSheetAdminApprove();
                tm.Timesheet_Decline(TimeSheetIDs, sessionKeys.UID, txtComments.Text);
                //TextBox1.Text = string.Empty;
                //send decline mails to Users 
                SendDdeclineMail(TimeSheetIDs);
            }
            getFirstGrid();
        }

    }
    protected void gvInnerTimeSheet_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gvTemp = (GridView)sender;
        int GetSubSectionID = Convert.ToInt32(gvTemp.DataKeys[0].Value);
        gvUniqueID = gvTemp.UniqueID;
        gvEditIndex = e.NewEditIndex;
        getFirstGrid();
        javaScript += "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + GetSubSectionID.ToString() + "','alt');</script>";
        ClientScript.RegisterStartupScript(GetType(), "Expand", javaScript);
    }
    protected void gvInnerTimeSheet_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gvTemp = (GridView)sender;
        int GetSubSectionID = Convert.ToInt32(gvTemp.DataKeys[0].Value);
        gvUniqueID = gvTemp.UniqueID;
        gvEditIndex = -1;
        getFirstGrid();
        javaScript += "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + GetSubSectionID.ToString() + "','alt');</script>";
        ClientScript.RegisterStartupScript(GetType(), "Expand", javaScript);

    }
    protected void gvInnerTimeSheet_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gvTemp = (GridView)sender;
        gvUniqueID = gvTemp.UniqueID;

        int id = Convert.ToInt32(((Label)gvTemp.Rows[e.RowIndex].FindControl("lblID")).Text);
        string comments = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtApproveComments")).Text;
        using (TimeSheetDataContext td = new TimeSheetDataContext())
        {
            TimesheetEntry timesheetEntry = td.TimesheetEntries.Where(t => t.ID == id).FirstOrDefault();
            if (timesheetEntry != null)
            {
                timesheetEntry.ApproverComments = comments;
                td.SubmitChanges();
            }
        }
        gvEditIndex = -1;
        int GetSubSectionID = Convert.ToInt32(gvTemp.DataKeys[0].Value);
        getFirstGrid();
        javaScript += "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + GetSubSectionID.ToString() + "','alt');</script>";
        ClientScript.RegisterStartupScript(GetType(), "Expand", javaScript);

    }
}
