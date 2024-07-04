using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using DC.DAL;
using DC.Entity;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using UserMgt.BAL;
using UserMgt.Entity;
using UserMgt.DAL;
using System.Data;
using AssetsMgr.DAL;
using System.Xml;
using System.Reflection;
using DC.BAL;
using DC.BLL;

public partial class DC_controls_FLSListCtrl : System.Web.UI.UserControl
{
    DisplayColumnBAL dcBAL = new DisplayColumnBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["uname"] = sessionKeys.UName;
        Session["userid"] = sessionKeys.UID.ToString();
        if (!IsPostBack)
        {
            ManageRecurringEntries();
            AddDefaultFields();
            //check for default data

            InsertDefaultData();
            InsertDefaultColumn();
            BindListBoxes();
            ccdCompany.DataBind();
            ccdCompany.SelectedValue = sessionKeys.PortfolioID.ToString();

            if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
            {
                Img1.Visible = false;
                imgBtnExportToExcel.Visible = false;
                imgBtnExportToPDF.Visible = false;
            }
            else
            {
                Img1.Visible = true;
            }
        }

        if (Request.QueryString["type"] == "FLS")
        {
            //pnlCustomer.Visible=false;
            //pnlMap.Visible = true;
            Img1.NavigateUrl = "~/WF/DC/DCNavigation.ashx?type=FLS";
            pnlExport.Visible = true;
            ccdType.SelectedValue = "6";
            // link_flsreport.Visible = true;
            tdRTLable.Style.Add("display", "none");
            tdRTField.Style.Add("display", "none");
            tdRTpanel.Style.Add("display", "none");
            //tdRequestTypeLable.Style.Add("display", "inline-block");
            //tdRequestTypeField.Style.Add("display", "inline-block");

            // pnlRequestType.Visible = false;
            // link_unitstatus.Visible = true;
            // link_Dashboard.Visible = true;
            //configuration icon can see only admin
            //if (sessionKeys.SID == 1)
            //    ImgConfig.Visible = true;
            //else
                ImgConfig.Visible = false;
            lblFooter.Visible = true;
            //lblFooter.Text = "Please note that changes made to the SLA in this section will affect SLA’s for tickets raised from this point onwards for every customer.";
        }
        else
        {

            lblFooter.Visible = false;
        }
        if (Request.QueryString["type"] == "Delivery")
        {
            //pnlMap.Visible = false;
            //pnlCustomer.Visible = true;
            Img1.NavigateUrl = "~/WF/DC/DCNavigation.ashx?type=Delivery";
            // ccdType.SelectedValue = "1";
            //link_deliveryreport.Visible = true;
            tdRequestTypeLable.Style.Add("display", "none");
            tdRequestTypeField.Style.Add("display", "none");
            ImgConfig.Visible = false;
        }

        if (Request.QueryString["type"] == "AccessControl")
        {
            //pnlCustomer.Visible = true;
            //pnlMap.Visible = false;
            Img1.NavigateUrl = "~/WF/DC/DCNavigation.ashx?type=AccessControl";
            // ccdType.SelectedValue = "3";
            // link_accessreport.Visible = true;
            pnlAccessNo.Visible = true;
            tdRequestTypeLable.Style.Add("display", "none");
            tdRequestTypeField.Style.Add("display", "none");
            ImgConfig.Visible = false;
        }


        if (Request.QueryString["type"].ToLower() == "permittowork")
        {
            //pnlCustomer.Visible = true;
            //pnlMap.Visible = false;
            Img1.NavigateUrl = "~/WF/DC/DCNavigation.ashx?type=permittowork";
            // ccdType.SelectedValue = "1";
            //link_deliveryreport.Visible = true;
            tdRequestTypeLable.Style.Add("display", "none");
            tdRequestTypeField.Style.Add("display", "none");
            ImgConfig.Visible = false;
        }

      
            //lit_pagetitle.Text = Resources.DeffinityRes.ServiceDesk;
            lit_paneltitle.Text = Resources.DeffinityRes.ServiceDesk;
        

       
    }
    public static string GetTimeZoneID(string val)
    {
        string retval = "Pacific Standard Time";
        if (val.ToUpper().Contains("GMT-08:00"))
            retval = "Pacific Standard Time";
        else if (val.ToUpper().Contains("GMT+00:00"))
            retval = "GMT Standard Time";
        else if (val.ToUpper().Contains("GMT+05:30"))
            retval = "India Standard Time";
        return retval;
    }
    public  void ManageRecurringEntries()
    {
        try
        {
            UserMgt.BAL.ContractorsBAL cbal = new ContractorsBAL();
            using (DC.DAL.DCDataContext dc = new DC.DAL.DCDataContext())
            {
                using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    using (HealthCheckMgt.DAL.HealthCheckDataContext hd = new HealthCheckMgt.DAL.HealthCheckDataContext())
                    {
                        var contactList = pd.PortfolioContacts.Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
                        foreach (var c in contactList)
                        {

                            var mlist = pd.MaintenanceSchedules.Where(o => o.RequesterID == c.ID).ToList();
                            foreach (var m in mlist)
                            {
                                var r = hd.HealthCheck_Recurs.Where(o => o.HealthCheckId == m.ID).FirstOrDefault();
                                if (r != null)
                                {
                                    var c_addresslist = pd.PortfolioContactAddresses.Where(o => o.ContactID == c.ID).FirstOrDefault();

                                    DateTime cdate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById(GetTimeZoneID("GMT-08:00")));
                                    //DateTime cdate = DateTime.Now;
                                    var sdate = m.DateOfReminder.Value; //r.StartDate.Value; 
                                    var stime = r.StartTime.Value;
                                    var rdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, stime.Hour, stime.Minute, 0);
                                    LogExceptions.LogException("dateofremainder:" + sdate.ToShortDateString() + " " + stime.ToShortTimeString() + "- Current USA time:" + cdate.ToShortDateString() + " " + cdate.ToShortTimeString());
                                    var AssignedUserid = cbal.GetUserByName();
                                    //check the entity is there with data and time
                                    var checkDate = dc.CallDetails.Where(o => o.LoggedDate == rdate && o.RequesterID == c.ID).FirstOrDefault();
                                    if (checkDate == null)
                                    {
                                        if ((cdate.Year == rdate.Year && cdate.Month == rdate.Month && cdate.Day == rdate.Day) && cdate.Hour >= rdate.Hour )

                                        {
                                            LogExceptions.LogException("executed");

                                            var typeofrequser = dc.TypeOfRequests.Where(o => o.CustomerID == sessionKeys.PortfolioID && o.Name == "Maintenance").FirstOrDefault();
                                            int typeofrequserid = (typeofrequser == null)? 0 : typeofrequser.ID;
                                            int callid = AddRecord(c.ID, rdate, sessionKeys.PortfolioID, c_addresslist.ID, typeofrequserid);
                                            if(callid >0)
                                            UpdateResource(AssignedUserid, callid);
                                        }

                                    }
                            }
                            }
                        }
                    }

                }
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public static void UpdateResource(int assignedUserID,int callid)
    {
        var cReporsitory = new DCRepository<CallDetail>();
        var cd = cReporsitory.GetAll().Where(o => o.ID == callid).FirstOrDefault();
        var fReporsitory = new DCRepository<FLSDetail>();
        var fd = fReporsitory.GetAll().Where(o => o.CallID == callid).FirstOrDefault();

        if (assignedUserID.ToString().Length > 0)
        {
            FLSDetailsBAL.UpdateTicketStatus(callid, assignedUserID, 43);
            //43	Assigned Technician
            //cd.StatusID = 43;
            //cReporsitory.Edit(cd);
            fd.UserID = assignedUserID;

            fReporsitory.Edit(fd);


        }
    }
    public int AddRecord(int ContactID,DateTime loggedDate,int PortfolioID ,int addressid,int? typeofrequestid,int assignedUser = 0, int ProductId = 0)
    {
        int id = 0;
        try
        {

            /* Add Call Details */
            CallDetail cd = new CallDetail();

            cd.SiteID = 0;
            cd.RequestTypeID = typeofrequestid;
            cd.StatusID = 22;
            cd.CompanyID = PortfolioID;
            cd.RequesterID = ContactID;
            cd.LoggedBy = 0;
            cd.LoggedDate = loggedDate;
            CallDetailsBAL.AddCallDetails(cd);
            id = cd.ID;
            // call journal
            DateTime currentDateTime = Convert.ToDateTime(cd.LoggedDate);
            //AddCallDetailsJournal(id, cd, currentDateTime);

            /*Add FLS Details*/
            FLSDetail fd = new FLSDetail();
            fd.CallID = id;
            fd.SubjectID = 0;
            fd.Details = string.Empty;


            //DateTime dtn = Convert.ToDateTime(dt);
            //DateTime scheduledDateTime = Convert.ToDateTime(txtSeheduledDateTime.Text + " " + txtScheduledTime.Text);
            //if (dtn == scheduledDateTime || scheduledDateTime < dtn)
            //{
            //    fd.ScheduledDate = DateTime.Now;
            //}
            //else
            //{
            fd.ScheduledDate = loggedDate;
            //}
            fd.TimeAccumulated = "";
            fd.TimeTakentoResolve = 0;
            fd.TimeWorked = "0";
            fd.DepartmentID = 0;
            //if(assignedUser == 1)
            //{
            //    if (hrid.Value.ToString().Length > 0)
            //    fd.UserID = Convert.ToInt32(hrid.Value.ToString().Contains(",") ? hrid.Value.ToString().Split(',').ToArray().FirstOrDefault() : hrid.Value.ToString());
            //}
            fd.Notes = string.Empty;
            //fd.RAGStatus = ddlRAGStatus.SelectedValue;
            fd.POnumber = string.Empty;

            //Newly added
            //FLSDetailsBAL.AddDefaultTypeofRequest(sessionKeys.PortfolioID); //
           // fd.RequestType = GetValue(ddlRequestType.SelectedValue);
            fd.SourceOfRequestID = 0;
            fd.CategoryID = 0;
            fd.SubCategoryID = 0;
            fd.CustomerReference = string.Empty;
            fd.CustomerCostCode = string.Empty;
            fd.DateTimeStarted = loggedDate;
            //fd.DateTimeClosed = Convert.ToDateTime(txtClosedDate.Text + " " + (string.IsNullOrEmpty(txtClosedTime.Text) ? "00:00:00" : txtClosedTime.Text + ":00"));
            fd.SubmittedBy = "";
            //
            fd.TaskCategoryID = 0;
            fd.TaskSubcategoryID = 0;
            fd.Qty = 0;

            var edate = fd.ScheduledDate.Value.AddHours(2);// DateTime.Now.AddHours(2);
            //new fileds on fed 2016
            fd.ScheduledEndDateTime = edate;// Convert.ToDateTime(txtScheduledEndDateTime.Text + " " + (string.IsNullOrEmpty(txtScheduledEndTime.Text) ? "00:00:00" : txtScheduledEndTime.Text + ":00"));
            fd.PriorityId = 0;
            fd.TimeTakentoResolve = 0;
            fd.ResolutionDateandTime = null; //Convert.ToDateTime(txtResolutionDateandTime.Text + " " + (string.IsNullOrEmpty(txtResolutionTime.Text) ? "00:00:00" : txtResolutionTime.Text + ":00"));
            fd.Sitedetails = string.Empty;
            fd.ContactAddressID = addressid; //Convert.ToInt32(haddressid.Value);
            //if (!string.IsNullOrEmpty(txtPreferreddate2.Text))
            //    fd.Preferreddate2 = Convert.ToDateTime(txtPreferreddate2.Text + " " + (string.IsNullOrEmpty(txtPreferreddatetime2.Text) ? "00:00:00" : txtPreferreddatetime2.Text + ":00"));
            //if (!string.IsNullOrEmpty(txtPreferreddate3.Text))
            //    fd.Preferreddate3 = Convert.ToDateTime(txtPreferreddate3.Text + " " + (string.IsNullOrEmpty(txtPreferreddatetime3.Text) ? "00:00:00" : txtPreferreddatetime3.Text + ":00"));
            //if (!string.IsNullOrEmpty(txtScheduledToTime.Text))
            //    fd.ScheduledDatetotime = !string.IsNullOrEmpty(txtScheduledToTime.Text) ? txtScheduledToTime.Text : string.Empty;
            //if (!string.IsNullOrEmpty(txtPreferreddatetimeto2.Text))
            //    fd.Preferreddatetotime2 = !string.IsNullOrEmpty(txtPreferreddatetimeto2.Text) ? txtPreferreddatetimeto2.Text : string.Empty;
            //if (!string.IsNullOrEmpty(txtPreferreddatetimeto3.Text))
            //    fd.Preferreddatetotime3 = !string.IsNullOrEmpty(txtPreferreddatetimeto3.Text) ? txtPreferreddatetimeto3.Text : string.Empty;
            fd.TicketManagerID = 0;
            FLSDetailsBAL.AddFLSDetails(fd);
            // fls details journal
            AddFLSDetailsJournal(id, fd, currentDateTime);
            /*Add FLS Time Details ie,Time Accumulated */

            FLSTimeDetail ft = new FLSTimeDetail();
            ft.CallID = id;
            ft.Status = ddlStatus.SelectedItem.Text;
            ft.StatusTime = DateTime.Now;
            FLSTimeDetailsBAL.AddFLSTimeDetail(ft);
            //upload documents
            //UploadFiles(id);
            //BindData(id);
            //SavePlaceholderData(id, Convert.ToInt32(ddlCompany.SelectedValue));


            //int i = SendingEmailType();
            ////Mail to distribution list
            //if (i == 1)
            //{
            //    SendDistributionMail(id, fd);
            //}
            //Mail to Priority group
            //else if (i == 2)
            //{
            //    //mail to priority users
            //    BulkEmailSending(id, fd);
            //}
            //SendMailtoAssignedName(id, fd);
            //iframe_DisplayHistory(id);

            //if (hcid.Value != "")
            //{
            //    SendMailtoRequester(id, true, fd);
            //}
            //send mails to resources
            //if (ProductId > 0)
            //    SaveProductToCallid(ProductId.ToString(), id);
            //update the status
            //43	Assigned Technician
            //if (assignedUser == 1)
            //{
            //    //43	Assigned Technician
            //    cd.StatusID = 43;
            //    CallDetailsBAL.CallDetailsUpdate(cd);
            //}
            //update the status
            //44	Awaiting Technician
            //if (assignedUser == 2)
            //{
            //    MailSendingToAllRequestersWithPincode(fd, cd);
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return id;
    }
    public static void AddFLSDetailsJournal(int cid, FLSDetail fd, DateTime modifiedDate)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            FLSDetailsJournal fj = new FLSDetailsJournal();
            fj.CallID = cid;
            fj.SubjectID = fd.SubjectID;//int.Parse(ddlSubject.SelectedValue);
            fj.Details = fd.Details;//txtDetails.Text;
            fj.ScheduledDate = fd.ScheduledDate;
            //fj.ScheduledDate = Convert.ToDateTime(txtSeheduledDateTime.Text + " " + (string.IsNullOrEmpty(txtScheduledTime.Text) ? "00:00:00" : txtScheduledTime.Text + ":00"));
            fj.TimeAccumulated = fd.TimeAccumulated; //txtTimeAccumulated.Text;

            fj.TimeWorked = fd.TimeWorked;//txtTimeWorked.Text;
            fj.DepartmentID = fd.DepartmentID;//int.Parse(string.IsNullOrEmpty(ddlAssignedtoDept.SelectedValue) ? "0" : ddlAssignedtoDept.SelectedValue);
            fj.UserID = fd.UserID;// int.Parse(string.IsNullOrEmpty(ddlAssignedtoName.SelectedValue) ? "0" : ddlAssignedtoName.SelectedValue);
            fj.Notes = fd.Notes; //txtNotes.Text;
            fj.ModifiedBy = sessionKeys.UID;
            fj.ModifiedDate = modifiedDate;
            fj.VisibleToCustomer = false;
            fj.Resolution = string.Empty;
            //fj.RAGStatus = ddlRAGStatus.SelectedValue;
            fj.POnumber = fd.POnumber;//txtPOnumber.Text.Trim();
            fj.RequestType = fd.RequestType;
            fj.SourceOfRequestID = fd.SourceOfRequestID;
            fj.CategoryID = fd.CategoryID;
            fj.SubCategoryID = fd.SubCategoryID;
            fj.CustomerReference = fd.CustomerReference;
            fj.CustomerCostCode = fd.CustomerCostCode;
            fj.DateTimeStarted = fd.DateTimeStarted;
            fj.DateTimeClosed = fd.DateTimeClosed;
            fj.SubmittedBy = fd.SubmittedBy;

            //new fileds
            fj.PriorityId = fd.PriorityId;
            fj.ScheduledEndDateTime = fd.ScheduledEndDateTime;
            fj.TimeTakentoResolve = fd.TimeTakentoResolve;
            fj.ResolutionDateandTime = fd.ResolutionDateandTime;
            fj.Sitedetails = fd.Sitedetails;
            fj.ContactAddressID = fd.ContactAddressID;
            fj.Preferreddate2 = fd.Preferreddate2;
            fj.Preferreddate3 = fd.Preferreddate3;
            fj.ScheduledDatetotime = fd.ScheduledDatetotime;
            fj.Preferreddatetotime2 = fd.Preferreddatetotime2;
            fj.Preferreddatetotime3 = fd.Preferreddatetotime3;
            fj.TicketManagerID = fd.TicketManagerID;
            //fj. = fd.Sitedetails;
            ws.AddFLSDetailsJournal(fj);

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void AddCallDetailsJournal(int cid, CallDetail cd, DateTime ModifiedDate)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            CallDetailsJournal cdj = new CallDetailsJournal();
            cdj.CallID = cid;
            cdj.SiteID = cd.SiteID;//int.Parse(ddlSite.SelectedValue);
            cdj.RequestTypeID = cd.RequestTypeID; //int.Parse(ddlTypeofRequest.SelectedValue);
            cdj.StatusID = cd.StatusID; //int.Parse(ddlStatus.SelectedValue);
            cdj.CompanyID = cd.CompanyID; //int.Parse(ddlCompany.SelectedValue);
            cdj.RequesterID = cd.RequesterID; //int.Parse(string.IsNullOrEmpty(ddlName.SelectedValue) ? "0" : ddlName.SelectedValue);
            cdj.LoggedDate = cd.LoggedDate;
            cdj.LoggedBy = sessionKeys.UID;
            cdj.ModifiedBy = sessionKeys.UID;
            cdj.ModifiedDate = ModifiedDate;
            cdj.VisibleToCustomer = false;
            ws.AddCallDetailsJournal(cdj);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public static void AddDefaultFields()
    {
        try
        {
            if (FLSFieldsConfigBAL.GetFLSFieldsConfigCount(sessionKeys.PortfolioID) == 0)
            {
                FLSFieldsConfigBAL.InsertConfigData(sessionKeys.PortfolioID);
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public static void InsertDefaultColumn()
    {
        using (DCDataContext dc = new DCDataContext())
        {
            int count = (from a in dc.DisplayColumnsByUsers where a.UserID == sessionKeys.UID select a).Count();
            if (count == 0)
            {
                var dcBAL = new DisplayColumnBAL();
                dcBAL.Insertfornewuser();
            }
        }

    }
    public class list_class
    {
        public int ID { set; get; }
        public string Name { set; get; }

    }
    public static void InsertDefaultData()
    {
        try
        {
            //update the call ID by cusotmers
            DC.BLL.FLSDetailsBAL.UpdateCallIDByCustomer();

            //update the tax
            DC.BAL.VATByCustomerBAL.VATByCustomer_SetDefault();
            //update the markup
            PortfolioMgt.BAL.PortolioMarginBAL.PortolioMarginBAL_SetDefault();

            using (DCDataContext dc = new DCDataContext())
            {
                //int count = (from a in dc.TypeOfRequests where a.CustomerID == sessionKeys.PortfolioID select a).Count();
                //if (count == 0)
                //{
                //    dc.TypeOfRequests.InsertOnSubmit(new TypeOfRequest() { CustomerID = sessionKeys.PortfolioID, Name = "Fault" });
                //    dc.SubmitChanges();
                //}
                var h = (from a in dc.TypeOfRequests where a.CustomerID == sessionKeys.PortfolioID && a.Name == "HVAC" select a).FirstOrDefault();
                if (h!= null)
                {
                    dc.TypeOfRequests.DeleteOnSubmit(h);
                    dc.SubmitChanges();
                }

                if ( dc.TypeOfRequests.Where(o=>o.CustomerID == sessionKeys.PortfolioID).Count() == 0)
                {
                    List<list_class> tList = new List<list_class>();
                    tList.Add(new list_class() { Name = "Event" });
                    tList.Add(new list_class() { Name = "Demonstration" });
                    tList.Add(new list_class() { Name = "Charity" });
                    tList.Add(new list_class() { Name = "R&D" });
                    tList.Add(new list_class() { Name = "Administrative" });
                    tList.Add(new list_class() { Name = "Installation" });
                    tList.Add(new list_class() { Name = "Maintenance" });
                    tList.Add(new list_class() { Name = "Design" });
                    tList.Add(new list_class() { Name = "New Product Development" });
                    tList.Add(new list_class() { Name = "Research" });
                    tList.Add(new list_class() { Name = "Software Development" });
                    tList.Add(new list_class() { Name = "Other" });

                    foreach (var t in tList)
                    {
                        var tEntity = (from a in dc.TypeOfRequests where a.CustomerID == sessionKeys.PortfolioID && a.Name.ToLower() == t.Name.ToLower() select a).FirstOrDefault();
                        if (tEntity == null)
                        {
                            tEntity = new TypeOfRequest() { CustomerID = sessionKeys.PortfolioID, Name = t.Name };
                            dc.TypeOfRequests.InsertOnSubmit(tEntity);
                            dc.SubmitChanges();
                        }
                    }
                }

                if (dc.Categories.Where(o => o.CustomerID == sessionKeys.PortfolioID).Count() == 0)
                {
                    //sub category default data
                    List<list_class> cList = new List<list_class>();
                    cList.Add(new list_class() { Name = "Carrier" });
                    cList.Add(new list_class() { Name = "Bryant" });
                    cList.Add(new list_class() { Name = "Trane" });
                    cList.Add(new list_class() { Name = "Rheem" });
                    cList.Add(new list_class() { Name = "Lennox" });
                    cList.Add(new list_class() { Name = "York" });
                    cList.Add(new list_class() { Name = "Ruud" });
                    cList.Add(new list_class() { Name = "Amana" });
                    cList.Add(new list_class() { Name = "Goodman" });
                    cList.Add(new list_class() { Name = "American Standard" });
                    cList.Add(new list_class() { Name = "LG" });
                    cList.Add(new list_class() { Name = "Miele" });
                    cList.Add(new list_class() { Name = "Haler" });
                    cList.Add(new list_class() { Name = "Rinnai" });
                    cList.Add(new list_class() { Name = "Fijitsu" });
                    cList.Add(new list_class() { Name = "Coleman" });
                    cList.Add(new list_class() { Name = "Sanyo" });
                    cList.Add(new list_class() { Name = "Aspen" });
                    cList.Add(new list_class() { Name = "Honeywell" });
                    cList.Add(new list_class() { Name = "ClimateMaster" });
                    cList.Add(new list_class() { Name = "NuTone" });
                    cList.Add(new list_class() { Name = "Laars" });
                    cList.Add(new list_class() { Name = "Burnham" });
                    cList.Add(new list_class() { Name = "Gree" });
                    cList.Add(new list_class() { Name = "Toshiba" });
                    cList.Add(new list_class() { Name = "Westinghouse" });
                    cList.Add(new list_class() { Name = "Sears" });
                    cList.Add(new list_class() { Name = "Reznor" });
                    cList.Add(new list_class() { Name = "Buderus" });
                    cList.Add(new list_class() { Name = "GE" });
                    cList.Add(new list_class() { Name = "AdobeAir" });
                    cList.Add(new list_class() { Name = "AC Smith" });
                    cList.Add(new list_class() { Name = "Aprilaire" });


                    //sub category default data
                    List<list_class> sList = new List<list_class>();
                    sList.Add(new list_class() { Name = "Furnaces" });
                    sList.Add(new list_class() { Name = "Heat Pumps" });
                    sList.Add(new list_class() { Name = "Packaged Units" });
                    sList.Add(new list_class() { Name = "Boilers" });
                    sList.Add(new list_class() { Name = "Mini-Split Systems" });
                    sList.Add(new list_class() { Name = "Air Conditioners" });
                    sList.Add(new list_class() { Name = "Solar Ready" });
                    sList.Add(new list_class() { Name = "Air Handlers" });
                    sList.Add(new list_class() { Name = "Garage Heaters" });
                    sList.Add(new list_class() { Name = "Thermostats" });
                    sList.Add(new list_class() { Name = "Zoning" });
                    sList.Add(new list_class() { Name = "Air Purification" });
                    sList.Add(new list_class() { Name = "Humidity Controls" });
                    sList.Add(new list_class() { Name = "Ventilation" });
                    sList.Add(new list_class() { Name = "Filters" });

                    foreach (var c in cList)
                    {
                        int categoryID = 0;
                        var cEntity = (from a in dc.Categories where a.CustomerID == sessionKeys.PortfolioID && a.Name.ToLower() == c.Name.ToLower() select a).FirstOrDefault();
                        if (cEntity == null)
                        {
                            var cEData = new Category() { CustomerID = sessionKeys.PortfolioID, Name = c.Name };
                            dc.Categories.InsertOnSubmit(cEData);
                            dc.SubmitChanges();
                            categoryID = cEData.ID;
                        }
                        else
                        {
                            categoryID = cEntity.ID;
                        }

                        if (categoryID > 0)
                        {
                            foreach (var s in sList)
                            {
                                var sEntity = (from a in dc.SubCategories where a.CategoryID == categoryID && a.Name.ToLower() == s.Name.ToLower() select a).FirstOrDefault();
                                if (sEntity == null)
                                {
                                    sEntity = new SubCategory() { CategoryID = categoryID, Name = s.Name };
                                    dc.SubCategories.InsertOnSubmit(sEntity);
                                    dc.SubmitChanges();
                                }
                            }
                        }

                    }
                }
               
            }

            //using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
            //{
            //    int count = (from a in pd.ProductPolicyTypes where a.CustomerID == sessionKeys.PortfolioID select a).Count();
            //    if (count == 0)
            //    {
            //        for (int i = 1; i <= 3; i++)
            //        {
            //            var p1 = new PortfolioMgt.Entity.ProductPolicyType() { CustomerID = sessionKeys.PortfolioID, Description = "Warranty "+i, Title = "Warranty "+i, Monthly = 1, Yearly = 1 };
            //            pd.ProductPolicyTypes.InsertOnSubmit(p1);
            //            pd.SubmitChanges();
            //            pd.ProductAddonPrices.InsertOnSubmit(new PortfolioMgt.Entity.ProductAddonPrice() { CustomerID = sessionKeys.PortfolioID, ProductPolicyTypeID = p1.ID, AddOnDetails = "Add On 1", MontlyCost = 1, YearlyCost = 1 });
            //            pd.ProductAddonPrices.InsertOnSubmit(new PortfolioMgt.Entity.ProductAddonPrice() { CustomerID = sessionKeys.PortfolioID, ProductPolicyTypeID = p1.ID, AddOnDetails = "Add On 2", MontlyCost = 1, YearlyCost = 1 });
            //            pd.ProductAddonPrices.InsertOnSubmit(new PortfolioMgt.Entity.ProductAddonPrice() { CustomerID = sessionKeys.PortfolioID, ProductPolicyTypeID = p1.ID, AddOnDetails = "Add On 3", MontlyCost = 1, YearlyCost = 1 });
            //            pd.SubmitChanges();
            //        }
                    
            //    }
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    
    public void BindListBoxes()
    {

        list.DataSource = dcBAL.selectinggridcolumns();
        list.DataValueField = "ID";
        list.DataTextField = "ColumnName";
        list.DataBind();
        using (DCDataContext dc = new DCDataContext())
        {
            var y = (from a in dc.DisplayColumnsByUsers
                     join b in dc.DisplayColumns on a.DisplayColumnID equals b.ID
                     where a.UserID == sessionKeys.UID
                     orderby (a.Position) ascending
                     select new { a.ID, b.ColumnName }).ToList();
            //Repllace 100 by sessionKeys.UID
            gridlist.DataSource = y;
            gridlist.DataValueField = "ID";
            gridlist.DataTextField = "ColumnName";
            gridlist.DataBind();
        }
    }
    protected void add_Click(object sender, EventArgs e)
    {

        if (list.SelectedItem != null)
        {
            int ID = int.Parse(list.SelectedValue);
            dcBAL.Insertrecord(ID);

            BindListBoxes();
            lblscreen.Text = "";
        }
        //else
        //{
        //    lblscreen.Text = "Select One Field From Additional Fields";
        //}
    }
    protected void remove_Click(object sender, EventArgs e)
    {
        if (gridlist.SelectedItem != null)
        {
            int ID = int.Parse(gridlist.SelectedValue);
            dcBAL.deleterecord(ID);

            BindListBoxes();
            lblscreen.Text = "";
        }
        //else 
        //{
        //    lblscreen.Text = "Select One Field From Current Columns Grid List";
        //}
    }
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        popIssues.Hide();
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void imgBtnExportToExcel_Click(object sender, EventArgs e)
    {
        ExportData("Excel");
    }

    protected void imgBtnExportToPDF_Click(object sender, EventArgs e)
    {
        ExportData("PDF");
    }
    public void ExportData(string type)
    {
        try
        {


            using (DCDataContext db = new DCDataContext())
            {
                string[] ulist = { "Im On My Way", "Chat", "Contact", "Email Icon", "Resource Image", "Company" };
                var name = (from d in db.DisplayColumns
                            join u in db.DisplayColumnsByUsers
                                 on d.ID equals u.DisplayColumnID

                            where u.UserID == sessionKeys.UID && !ulist.Contains( d.ColumnName)
                            orderby u.Position
                            select new { FieldName = d.Value, ColumnName = d.ColumnName }).ToList();
                string company = ddlCompany.SelectedItem.Text;
                string ticketno = String.Format("{0}", Request.Form["ticketno"]);
                string status = ddlStatus.SelectedItem.Text;
                string requestType = ddlRequestType.SelectedItem.Text;
                string url = HttpContext.Current.Request.Url.AbsolutePath;
                List<Jqgrid> flsList = new List<Jqgrid>();
                flsList = DC.BLL.FLSDetailsBAL.Jqgridlist().OrderByDescending(j => j.CallID).ToList();
                if (url.ToLower().Contains("flsresourcelist.aspx"))
                {
                    flsList = flsList.Where(f => f.AssignedTechnicianID == sessionKeys.UID).ToList();
                }
                if (!string.IsNullOrEmpty(company))
                {
                    var d_array = new string[] { "[Loading customer...]", "Please select..." };
                    if (!d_array.Contains(company))
                    {
                        flsList = flsList.Where(o => o.Company.ToLower() == company.ToLower()).ToList();
                    }
                }

                if (status == "" && ticketno == string.Empty)
                {
                    flsList = flsList.Where(f => f.Status != "Closed" && f.Status != "Resolved").ToList();
                }
                if (ticketno != string.Empty)
                {
                    flsList = flsList.Where(f => f.CallID.ToString() == ticketno).ToList();
                }


                if (status != "")
                {
                    flsList = flsList.Where(f => f.Status == status).ToList();
                }


                if (!string.IsNullOrEmpty(requestType))
                {
                    var array = new string[] { "[Loading...]", "Please select..." };
                    if (!array.Contains(requestType))
                        flsList = flsList.Where(f => f.TypeofRequest == requestType).ToList();
                }

                if (type == "Excel")
                {
                    var wb = new XLWorkbook();
                    var ws = wb.Worksheets.Add("List of Tickets");
                    char alpahbets = 'A';
                    int count = 1;

                    foreach (var p in name)
                    {
                        int i = 2;
                        ws.Cell(alpahbets.ToString() + "1").Value = p.ColumnName;

                        foreach (var item in flsList)
                        {
                            if (p.ColumnName == "Ticket Ref")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = "" + item.CallID;
                            }
                            else if (p.ColumnName == "Source of Request")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.SourceofRequest;
                            }
                            else if (p.ColumnName == "Requester Name")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.RequesterName;
                            }
                            else if (p.ColumnName == "Requesters Telephone No")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.RequestersTelephoneNo;
                            }
                            else if (p.ColumnName == "Requesters Email Address")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.RequestersEmailAddress;
                            }
                            else if (p.ColumnName == "Requesters Department")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.RequestersDepartment;
                            }
                            else if (p.ColumnName == "Requesters Job Title")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.RequestersJobTitle;
                            }
                            else if (p.ColumnName == "Subject")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Subject;
                            }
                            else if (p.ColumnName == "Details")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Details;
                            }
                            else if (p.ColumnName == "Site")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Site;
                            }
                            else if (p.ColumnName == "Type of Request")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.TypeofRequest;
                            }
                            else if (p.ColumnName == "Status")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Status;
                            }
                            else if (p.ColumnName == "Category")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Category;
                            }
                            else if (p.ColumnName == "Logged By")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.LoggedBy;
                            }
                            else if (p.ColumnName == "Logged Date/Time")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.LoggedDateTime;
                            }
                            else if (p.ColumnName == "Assigned to Department")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.AssignedtoDepartment;
                            }
                            else if (p.ColumnName == "Assigned Technician")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.AssignedTechnician;
                            }
                            else if (p.ColumnName == "Scheduled Date/Time")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.ScheduledDateTime;
                            }
                            else if (p.ColumnName == "Date and Time Started")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.DateandTimeStarted;
                            }
                            else if (p.ColumnName == "Date and Time Closed")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.DateandTimeClosed;
                            }
                            else if (p.ColumnName == "Customer Ref")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.CustomerRef;
                            }
                            else if (p.ColumnName == "PO Number")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.PONumber;
                            }
                            else if (p.ColumnName == "Notes")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Notes;
                            }
                            else if (p.ColumnName == "Time Accumulated")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.TimeAccumulated;
                            }
                            else if (p.ColumnName == "Time Worked")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.TimeWorked;
                            }
                            else if (p.ColumnName == "Customer Cost Code")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.CustomerCostCode;
                            }
                            else if (p.ColumnName == "In Hand SLA")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.InHandSLA;
                            }
                            else if (p.ColumnName == "Resolution SLA")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.ResolutionSLA;
                            }
                            else if (p.ColumnName == "Company")
                            {
                                ws.Cell(alpahbets.ToString() + i.ToString()).Value = item.Company;
                            }
                            i++;

                        }
                        alpahbets++;
                        count++;
                    }
                    var rngTable = ws.Range("A1:" + alpahbets-- + "1");

                    var rngHeaders = rngTable.Range("A1:" + alpahbets-- + "1");
                    rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    rngHeaders.Style.Font.Bold = true;
                    rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;
                    ws.Columns(1, name.Count).AdjustToContents();

                    string path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tickets");

                    if (Directory.Exists(path) == false)
                    {
                        Directory.CreateDirectory(path);
                    }

                    wb.SaveAs(path + "\\" + "ListOfTickets.xlsx");
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + "ListOfTickets.xlsx");
                    if (fileInfo.Exists)
                    {
                        System.Web.HttpContext.Current.Response.Clear();
                        System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                        System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                        System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                        System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=ListOfTickets.xlsx");
                        System.Web.HttpContext.Current.Response.Flush();
                        System.Web.HttpContext.Current.Response.End();

                    }
                }
                else
                {
                    PdfPTable table = new PdfPTable(name.Count) { WidthPercentage = 100 };// table for Usage

                    Font headerFont = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.BOLD, new BaseColor(System.Drawing.Color.White))));
                    BaseColor headerColour = new BaseColor(System.Drawing.Color.Gray);
                    //Header

                    foreach (var p in name)
                    {

                        PdfPCell cell1 = new PdfPCell(new Phrase(p.ColumnName, headerFont));
                        cell1.BackgroundColor = headerColour;
                        table.AddCell(cell1);

                    }

                    PdfPCell cell = new PdfPCell(new Phrase("", headerFont));
                    cell.BackgroundColor = headerColour;
                    //table.AddCell(cell);
                    foreach (var item in flsList)
                    {
                        foreach (var p in name)
                        {
                            if (p.ColumnName == "Ticket Ref")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString("" + item.CallID), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Source of Request")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.SourceofRequest), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Requester Name")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.RequesterName), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Requesters Telephone No")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.RequestersTelephoneNo), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Requesters Email Address")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.RequestersEmailAddress), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Requesters Department")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.RequestersDepartment), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Requesters Job Title")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.RequestersJobTitle), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Subject")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.Subject), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Details")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.Details), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Site")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.Site), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Type of Request")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.TypeofRequest), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Status")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.Status), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Category")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.Category), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Logged By")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.LoggedBy), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Logged Date/Time")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.LoggedDateTime), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Assigned to Department")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.AssignedtoDepartment), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Assigned Technician")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.AssignedTechnician), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Scheduled Date/Time")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.ScheduledDateTime), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Date and Time Started")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.DateandTimeStarted), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Date and Time Closed")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.DateandTimeClosed), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Customer Ref")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.CustomerRef), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "PO Number")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.PONumber), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Notes")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.Notes), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Time Accumulated")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.TimeAccumulated), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Time Worked")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.TimeWorked), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Customer Cost Code")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.CustomerCostCode), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "In Hand SLA")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.InHandSLA), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Resolution SLA")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.ResolutionSLA), font));
                                table.AddCell(cell);
                            }
                            else if (p.ColumnName == "Company")
                            {
                                Font font = new Font(new Font(FontFactory.GetFont("Tahoma", 8f, Font.NORMAL)));
                                cell = new PdfPCell(new Phrase(Convert.ToString(item.Company), font));
                                table.AddCell(cell);
                            }
                        }
                    }

                    string path = Server.MapPath("~/WF/UploadData/Temp");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 2f, 2f, 2f, 1f);
                    var writer = PdfWriter.GetInstance(document, Response.OutputStream);

                    Paragraph paragraph = new Paragraph("List of Tickets", new Font(FontFactory.GetFont("Tahoma", 10f, Font.BOLD)));
                    paragraph.SpacingAfter = 0f;
                   
                    Chunk linebreak = new Chunk(new LineSeparator(1f, 100f, new BaseColor(System.Drawing.Color.Gray), Element.ALIGN_CENTER, -1));

                    document.Open();
                    document.Add(paragraph);
                    document.Add(linebreak);
                    document.Add(table);
                    document.Close();
                    //System.Diagnostics.Process.Start(FullPath); //automatically opens
                    //Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=ListOfTicket.pdf");
                    Response.Write(document);
                    // Response.TransmitFile(FullPath);
                    Response.End();


                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridServices_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridRecords.PageIndex = e.NewPageIndex;
        selectHistoryRecords();
        UpdatePanelHistoryRecords.Update();
    }
    public void selectHistoryRecords()
    {
        try
        {
            //using (UserDataContext Udc = new UserDataContext())
            //{
            //    using (DCDataContext Dc = new DCDataContext())
            //    {
            //        var FlsList = Dc.FLSDetails.ToList();
            //        var CallsList = Dc.CallDetails.ToList();
            //        var rtList = Dc.TypeOfRequests.Select(rt => rt).ToList();
            //        var sList = Dc.Status.Select(s => s).ToList();
            //        var Clist = Udc.Contractors.ToList();

            //        var Result = (from a in FlsList
            //                      join b in CallsList on a.CallID equals b.ID
            //                      select new
            //                      {
            //                          CompanyID = b.CompanyID,
            //                          TicketRef1 = a.CallID.Value,
            //                          TicketRef = "TN:" + a.CallID.Value.ToString(),
            //                          RequesterName = Clist.Where(v => v.ID == b.RequesterID.Value).Select(v => v.ContractorName).FirstOrDefault(),
            //                          Details = a.Details,
            //                          TypeofRequest = rtList.Where(t => t.ID == a.RequestType.Value).Select(t => t.Name).FirstOrDefault(),
            //                          Status = sList.Where(t => t.ID == b.StatusID).Select(t => t.Name).FirstOrDefault(),
            //                          LoggedDateTime = b.LoggedDate
            //                      }).OrderBy(a => a.TicketRef1).ToList();
            //        if (ddlCompany.SelectedValue != "" && int.Parse(ddlCompany.SelectedValue) > 0)
            //        {
            //            Result = Result.Where(a => a.CompanyID == int.Parse(ddlCompany.SelectedValue)).ToList();
            //        }
            //        GridRecords.DataSource = Result;
            //        GridRecords.DataBind();
            //    }
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BtnHistory_Click(object sender, EventArgs e)
    {
        try 
        {
            selectHistoryRecords();
            ModelPopUpForHistoryRecords.Show();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.RawUrl);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //public string getLatLong(string Zip)
    //{
    //    string latlong = "", address = "";

    //    //IDCRespository<DC.Entity.GeoCode> gRep = new DCRepository<DC.Entity.GeoCode>();
    //    //var gEntity = gRep.GetAll().Where(o=>o.Zip == Zip.Trim()).FirstOrDefault();
    //    //if(gEntity!= null)
    //    //{
    //    //    latlong = Zip + "," + Convert.ToString(gEntity.Latitude) + "," + Convert.ToString(gEntity.Longitude) + "," + "" + Zip;
    //    //}

    //    address = "https://maps.googleapis.com/maps/api/geocode/xml?components=postal_code:" + Zip.Trim() + "&sensor=false&key=" + hkey.Value;
    //    var result = new System.Net.WebClient().DownloadString(address);
    //    XmlDocument doc = new XmlDocument();
    //    doc.LoadXml(result);
    //    XmlNodeList parentNode = doc.GetElementsByTagName("location");
    //    var lat = "";
    //    var lng = "";
    //    foreach (XmlNode childrenNode in parentNode)
    //    {
    //        lat = childrenNode.SelectSingleNode("lat").InnerText;
    //        lng = childrenNode.SelectSingleNode("lng").InnerText;
    //    }
    //    latlong = Zip + "," + Convert.ToString(lat) + "," + Convert.ToString(lng) + "," + "" + Zip;
    //    return latlong;
    //}
    public DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        //put a breakpoint here and check datatable
        return dataTable;
    }

public class LocationDisplayClass
{
    public string LocationPinCode { set; get; }
    public string address { set; get; }
    public int Id { set; get; }
    public int cid { set; get; }
}

    //public string GetAllPincodesOfRequester()
    //{
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    List<MarkersData> MarkersDataList = new List<MarkersData>();
    //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //    try
    //    {
    //        IDCRespository<DC.Entity.CallDetail> dReporsitory = new DCRepository<DC.Entity.CallDetail>();
    //         IDCRespository<DC.Entity.FLSDetail> fReporsitory = new DCRepository<DC.Entity.FLSDetail>();
    //        var sids = new int[]{22,43,44};
    //        var dList = dReporsitory.GetAll().Where(o=>o.CompanyID == sessionKeys.PortfolioID).Where(o => sids.Contains(o.StatusID.Value)).ToList();
    //         var fList = fReporsitory.GetAll().Where(o=> dList.Select(p=>p.ID).Contains(o.CallID.HasValue?o.CallID.Value:0)).ToList();
    //        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
    //        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> paReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
    //        var requesterList = pReporsitory.GetAll().Where(o => dList.Select(r=>r.RequesterID).ToArray().Contains(o.ID)).ToList();
    //        var requesterAddressList = paReporsitory.GetAll().Where(o => dList.Select(r => r.RequesterID).ToArray().Contains(o.ContactID)).ToList();
    //        Dictionary<string, object> row;
    //        //using (AssetsToSoftwareDataContext asset = new AssetsToSoftwareDataContext())
    //        //{
    //        List<LocationDisplayClass> LocationPinCodeResult = new List<LocationDisplayClass>();
    //        if (sessionKeys.SID == 1 || sessionKeys.SID == 2)
    //         LocationPinCodeResult = (from a in requesterList
    //                                     join a1 in requesterAddressList on a.ID equals a1.ContactID
    //                                     join b in dList on a.ID equals b.RequesterID
                                         
    //                                     orderby b.ID descending
    //                                         //where a.PortfolioID == sessionKeys.PortfolioID
    //                                                            select new LocationDisplayClass
    //                                     {
    //                                         LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : a1.PostCode,
    //                                         address = "<br>"+""+b.ID+"<br>"+a.Name + "<br>" + a1.Address + "<br>" + a1.City+"<br>" + a1.State +"<br>" + a1.PostCode,
    //                                         Id = a1.ID, cid= a.ID
    //                                     }).Take(10).ToList();

    //        else if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
    //            LocationPinCodeResult = (from a in requesterList
    //                                     join a1 in requesterAddressList on a.ID equals a1.ContactID
    //                                     join b in dList on a.ID equals b.RequesterID
    //                                     join f in fList on b.ID equals f.CallID
    //                                     where f.UserID == sessionKeys.UID
    //                                     orderby b.ID descending
                                          
    //                                     //where a.PortfolioID == sessionKeys.PortfolioID
    //                                     select new LocationDisplayClass
    //                                     {
    //                                         LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : a1.PostCode,
    //                                         address = "<br>" + "" + b.ID + "<br>" + a.Name + "<br>" + a1.Address + "<br>" + a1.City + "<br>" + a1.State + "<br>" + a1.PostCode,
    //                                         Id = a1.ID, cid=a.ID
    //                                     }).Take(10).ToList();

    //        //var LocationPinCodeResult = (from a in asset.V_Assets
    //        //                             where a.PortfolioID == sessionKeys.PortfolioID
    //        //                             select new
    //        //                             {
    //        //                                 LocationPinCode = a.FromLocation,
    //        //                                 Id = a.ID
    //        //                             }).ToList();
    //        //var AssetAssociatedToContactsList = asset.AssetAssociatedToContacts.
    //        //    Where(c => c.ContactId.Value == int.Parse(RequesterId)).Select(c => c.AssectId.Value.ToString()).ToArray();
    //        //LocationPinCodeResult = LocationPinCodeResult.
    //        //    Where(a => ContainsNew(a.Id.ToString(), AssetAssociatedToContactsList)).ToList();

    //        MarkersData MarkersDataSingle;

    //            foreach (var x in LocationPinCodeResult)
    //            {
    //                MarkersDataSingle = new MarkersData();
    //                MarkersDataSingle.title = x.LocationPinCode;
    //                string LL = getLatLong(x.LocationPinCode);
    //                string[] LLArray = LL.Split(',');
    //                MarkersDataSingle.lat = LLArray[1];
    //                MarkersDataSingle.lng = LLArray[2];
    //                MarkersDataSingle.description = x.address; //LLArray[3];
    //                MarkersDataSingle.Id = x.Id;
    //                MarkersDataSingle.cid = x.cid;
    //                MarkersDataList.Add(MarkersDataSingle);
    //            }
                
    //            DataTable dt = ToDataTable(MarkersDataList);
    //            foreach (DataRow dr in dt.Rows)
    //            {
    //                row = new Dictionary<string, object>();
    //                foreach (DataColumn col in dt.Columns)
    //                {
    //                    row.Add(col.ColumnName, dr[col]);
    //                }
    //                rows.Add(row);
    //            }

    //       // }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //    return serializer.Serialize(rows);
    //}
}
public class MarkersData
{
    public string title { get; set; }
    public string name { get; set; }
    public string jobref { get; set; }
    public string lat { get; set; }
    public string lng { get; set; }
    public int Id { get; set; }
    public string description { get; set; }
    public string color { get; set; }
    public int cid { get; set; }
    public string imgtype { get; set; }
}
public class Column
{
    public string FieldName { get; set; }
    public string ColumnName { get; set; }
}