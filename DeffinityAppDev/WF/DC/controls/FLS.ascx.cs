using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using DC.BLL;
using DC.Entity;
using System.IO;
using DC.BAL;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using AjaxControlToolkit;
using System.Text;
using UserMgt.DAL;
using UserMgt.Entity;
using System.Data.SqlClient;
using System.Data;
using HealthCheckMgt.Entity;
using HealthCheckMgt.BAL;
using DC.DAL;
//using Deffinity.PortfolioContact;
using System.Web.Security;
using AssetsMgr.DAL;
using System.Xml;
using System.ComponentModel;
using System.Reflection;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Globalization;

public partial class DC_FLS_ctrl: System.Web.UI.UserControl
{
    HealthCheckBAL hb;
    private static int _callid;
    SqlConnection conn = new SqlConnection(Constants.DBString);
    public int PageType = 1;
    public int VendorID = 0;
    protected void Page_Load(object sender, EventArgs e)
    
    {
        try
        {

           
            //GetWeatherInfo();
            hfforAdminAccess.Value = sessionKeys.SID.ToString();
            if (QueryStringValues.CallID == 0)
            {
                pnlAddress.Visible = false;
                //div_search.Visible = true;
                //ImageButton4.Visible = true;
                //ImgConfig.Visible = true;
                //BtnsubCategoryEdit.Visible = true;
            }
            else
            {
                pnlAddress.Visible = true;
                //div_search.Visible = false;
                //ImageButton4.Visible = false;
                //ImgConfig.Visible = false;
                //BtnsubCategoryEdit.Visible = false;
            }
            if (!IsPostBack)
            {
                hportfolioid.Value = sessionKeys.PortfolioID.ToString();
                BindClient();
                if (QueryStringValues.CCID > 0)
                    lblTitle.InnerText = sessionKeys.JobDisplayName+ " Reference " + QueryStringValues.CCID;
                else
                    lblTitle.InnerText = sessionKeys.JobDisplayName + " Request";
                if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
                {
                    link_return.HRef = "FLSResourceList.aspx?type=FLS";
                }
                else
                {
                    link_return.HRef = "FLSJlist.aspx?type=FLS";
                    ////}
                }

                //hide if login user is service provider

                lblAlert.Visible = false;
                BindTicketManager();
                //BindRequesters();
                //New changes
                //BindAssets();
                FieldsCheck();
                Bindhealthcheck();

                if (Request.QueryString["callid"] != null && Request.QueryString["callid"] != "0")
                {
                    if (Request.QueryString["S"] != null)
                    {
                        string Sname = Request.QueryString["S"].ToString();
                        //   BtnSearch.Visible = false;
                    }
                    int tid = int.Parse(Request.QueryString["callid"].ToString());

                    BindData(tid);
                    SettingTooltip();
                    BindDocuments(tid);
                    bool documentVisibility = FLSSectionConfigBAL.CheckDocumentVisibility(sessionKeys.PortfolioID);
                    pnlDocument.Visible = documentVisibility;
                   // pnlAdditionalInformation.Visible = true;
                    pnlNotes.Visible = true;
                   // iframe_DisplayHistory(tid);
                    //btnCreateNewTicket.Visible = true;
                    //btnRescheduleVisit.Visible = true;
                    NotesCtrl1.Visible = true;
                    //BindAssetsWithCantactId(0, tid);
                    //GridProducts.Columns[0].Visible = false;
                }
                else
                {
                    if (Request.QueryString["ticket"] != null)
                    {
                        lblNewTicketMsg.Text = "New service request created successfully. Please amend the information below as required.";
                        CreateNewTicket(Convert.ToInt32(Request.QueryString["ticket"]));
                        hfCustomerID.Value = ccdCompany.SelectedValue;
                        // BindPriorityDdl();
                        //BindAssetsWithCantactId(0);
                    }
                    else
                    {
                        sessionKeys.IncidentID = 0;
                        ccdCompany.SelectedValue = hportfolioid.Value;
                        hfCustomerID.Value = ccdCompany.SelectedValue;
                        ccdRequestType.SelectedValue = "2";
                        ccdType.SelectedValue = "6"; // 6 for FLS
                        ccdStatus.SelectedValue = "22"; //22 for Status "New"
                        txtCreatedDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                        txtCreatedTime.Text = string.Format(Deffinity.systemdefaults.GetStringTimeformat(), DateTime.Now);
                        txtStartedDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                        //txtStartedTime.Text = string.Format(Deffinity.systemdefaults.GetStringTimeformat(), DateTime.Now);
                        var cuDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        txtSeheduledDateTime.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), cuDate.AddHours(8));
                        txtScheduledTime.Text = string.Format(Deffinity.systemdefaults.GetStringTimeformat12(), cuDate.AddHours(6));
                        var edate = DateTime.Now.AddHours(2);
                        //txtScheduledToTime.Text = string.Format(Deffinity.systemdefaults.GetStringTimeformat(), cuDate.AddHours(6));
                        txtScheduledEndDateTime.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), cuDate.AddHours(18));
                        txtScheduledEndTime.Text = string.Format(Deffinity.systemdefaults.GetStringTimeformat12(), cuDate.AddHours(18));
                        ddlEndtime.SelectedValue = GetAm_PM(cuDate.AddHours(18));
                        BindPriorityDdl();
                        txtLoggedName.Text = sessionKeys.UName;
                        GvDocuments.DataBind();
                        setDefaultSite();
                        ccdAssignedName.SelectedValue = sessionKeys.UID.ToString();
                        //BindAssetsWithCantactId(0);
                    }
                    btnCreateNewTicket.Visible = false;
                    btnRescheduleVisit.Visible = false;
                    pnlDocument.Visible = false;
                    //pnlAdditionalInformation.Visible = true;
                    pnlNotes.Visible = true;
                    NotesCtrl1.Visible = false;
                    //ctrlHistory1.Visible = false;
                    if (sessionKeys.RInsertedId != "" && sessionKeys.RInsertedId != null)
                    {
                        //ccdName.SelectedValue = sessionKeys.RInsertedId.ToString();
                        hcid.Value = sessionKeys.RInsertedId.ToString();
                        sessionKeys.RInsertedId = string.Empty;
                    }
                }
                //CompareValidator3.ValueToCompare = txtCreatedDate.Text;
                CheckFieldVisibility();

                if (sessionKeys.SID == 4)
                {
                    btnCreateNewTicket.Visible = false;
                    btnRescheduleVisit.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (QueryStringValues.CallID > 0)
        {
            pnlSearchAddress.Attributes.Add("style", "display:none;");
            pnlsearchtop.Attributes.Add("style", "display:none;");
        }
        // Custom fields binding
        string[] customer = Regex.Split(ccdCompany.SelectedValue, ":::");
        int customerId = Convert.ToInt32(customer[0] == "" ? "0" : customer[0]);
      

        //BindPlaceholderFields(customerId); //Convert.ToInt32(ccdCompany.SelectedValue == "" ? "0" : ccdCompany.SelectedValue)


        txtSearch.Attributes.Add("placeholder", "Client Name / Email etc");
       }


     private void BindClient()
    {
        try
        {
            var aList = PortfolioMgt.BAL.PortfolioContactAddressBAL.PorfolioContact_Address_ByPortoflioID(int.Parse( hportfolioid.Value)).OrderBy(o => o.Name).ToList();
            ddlContacts.DataSource = aList;
            ddlContacts.DataTextField = "Name";
            ddlContacts.DataValueField = "AddressID";
            ddlContacts.DataBind();
            ddlContacts.Items.Insert(0, new ListItem("Please select...", "0"));

        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindTicketManager()
    {
        int[] sids = { 1, 2 };
        IUserRepository<UserMgt.Entity.Contractor> ucRespository = new UserRepository<UserMgt.Entity.Contractor>();
        var result = (from p in ucRespository.GetAll()
                      orderby p.ContractorName
                      where sids.Contains(p.SID.Value)
                      select new { value = p.ID.ToString(), name = p.ContractorName }).ToList();
        ddlTicketManager.DataSource = result;
        ddlTicketManager.DataTextField = "name";
        ddlTicketManager.DataValueField = "value";
        ddlTicketManager.DataBind();
        ddlTicketManager.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    //private void BindRequesters()
    //{
    //    IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pcRespository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
    //    var nlist = pcRespository.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
    //    var result = (from p in pcRespository.GetAll()
    //                  orderby p.Name
    //                  where p.PortfolioID == sessionKeys.PortfolioID
    //                  select new { value = p.ID.ToString(), name = p.Name }).ToList();
    //    ddlName.DataSource = result;
    //    ddlName.DataTextField = "name";
    //    ddlName.DataValueField = "value";
    //    ddlName.DataBind();
    //    ddlName.Items.Insert(0, new ListItem("Please select...", "0"));
    //}

    private void iframe_DisplayHistory(int tid)
    {
        iframe1.Attributes.Add("src", string.Format("../HistoryDisplay.aspx?type=fls&callid={0}&d={1}", tid.ToString(), string.Format("{0:yyyyMMddHHmmss}", DateTime.Now)));
    }
    public void BindPriorityDdl()
    {
        try {
            using (DCDataContext DCcontext = new DCDataContext())
            {
                var x = DCcontext.PriorityLevels.Where(o=>o.CustomerID== int.Parse(hportfolioid.Value)).ToList();
                ddlPriority.DataSource = x;
                ddlPriority.DataValueField = "Id";
                ddlPriority.DataTextField = "Value";
                ddlPriority.DataBind();
                ddlPriority.Items.Insert(0, new ListItem("Please select", "0"));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void setDefaultSite()
    {
        DefaultData d = DefaultDataBAL.SiteDefaultData_select();
        if (d != null)
        {
            ccdSite.SelectedValue = d.SiteID.ToString();
        }
        //using (UserDataContext db = new UserDataContext())
        //{
        //    ContractorDetail contractorDetail = db.ContractorDetails.Where(c => c.UserID == sessionKeys.UID).FirstOrDefault();
        //    if (contractorDetail != null)
        //    {
        //        if (contractorDetail.DefaultCustomerSite.HasValue)
        //        {
        //            ccdSite.SelectedValue = contractorDetail.DefaultCustomerSite.ToString();
        //        }
        //        else
        //        {
        //            if (d != null)
        //            {
        //                ccdSite.SelectedValue = d.SiteID.ToString();
        //            }
        //        }
        //    }
        //    else
        //    {
               
        //    }
        //}
      
       
       
    }
    public void SaveHealthForm()
    {
        try
        {
            hb = new HealthCheckBAL();
            var retval = hb.HealthCheck_FormAssignToCall_Add(new HealthCheck_FormAssignToCall() { CallID = QueryStringValues.CallID, FormID = Convert.ToInt32(ddlForms.SelectedValue) });
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void SaveHealthForm(int CallID)
    {
        try
        {
            if (Convert.ToInt32(ddlForms.SelectedValue) > 0)
            {
                hb = new HealthCheckBAL();
                var retval = hb.HealthCheck_FormAssignToCall_Add(new HealthCheck_FormAssignToCall() { CallID = CallID, FormID = Convert.ToInt32(ddlForms.SelectedValue) });
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void UpdateHealthForm()
    {
        try
        {
            hb = new HealthCheckBAL();
            HealthCheck_FormAssignToCall hc = hb.HealthCheck_FormAssignToCall_SelectByCallID(QueryStringValues.CallID);
            hc.FormID = Convert.ToInt32(ddlForms.SelectedValue);
            var h = hb.HealthCheck_FormAssignToCall_Update(hc);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void Bindhealthcheck()
    {
        try
        {
            //int customerID = 0;
            //if (QueryStringValues.CallID > 0)
            //{
            //    var callDetails = DC.BLL.CallDetailsBAL.SelectbyId(QueryStringValues.CallID);
            //    customerID = callDetails.CompanyID.Value;
            //}
            //else
            //{
            //    customerID = sessionKeys.PortfolioID;
               

            //}
            //hb = new HealthCheckBAL();
            //var formlist = hb.HealthCheck_Form_SelectByCustomerID(customerID);
            //ddlForms.DataSource = (from h in formlist
            //                       select new { h.FormID, h.FormName }).ToList();
            //ddlForms.DataTextField = "FormName";
            //ddlForms.DataValueField = "FormID";
            //ddlForms.DataBind();
            ddlForms.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
            
    }
    private void BindData(int tid)
    {
        try
        {
            CallDetail cd = CallDetailsBAL.SelectbyId(tid);
            FLSDetail fd = FLSDetailsBAL.SelectbyId(tid);
            var pbal = new PortfolioMgt.BAL.PortfolioContactBAL();
            
            string Pname = string.Empty;
            using (PortfolioDataContext Pdc = new PortfolioDataContext())
            {
                Pname = Pdc.ProjectPortfolios.Where(a => a.ID == cd.CompanyID).FirstOrDefault().PortFolio;
            }
            if (cd != null && fd != null)
            {
                htid.Value = tid.ToString();
                //ddlRAGStatus.SelectedValue = fd.RAGStatus;
                h_status.Value = StatusBAL.StatusNamebyId(Convert.ToInt32(cd.StatusID));
                ccdType.SelectedValue = Convert.ToString(cd.RequestTypeID);
                ccdStatus.SelectedValue = Convert.ToString(cd.StatusID);
                ccdCompany.TargetControlID = "ddlCompany";
                ccdCompany.DataBind();
                ccdCompany.SelectedValue = Convert.ToString(cd.CompanyID);

                sessionKeys.PortfolioID = Convert.ToInt32(cd.CompanyID);
                sessionKeys.PortfolioName = Pname;

                ddlCompany.SelectedValue = Convert.ToString(cd.CompanyID);
                hfCustomerID.Value = Convert.ToString(cd.CompanyID);
                ccdSourceOfRequest.DataBind();
                ccdSourceOfRequest.SelectedValue = Convert.ToString(fd.SourceOfRequestID.HasValue ? fd.SourceOfRequestID.ToString() : "0");
                //ccdName.DataBind();
                hcid.Value = Convert.ToString(cd.RequesterID);
                ccdSite.DataBind();
                ccdSite.SelectedValue = Convert.ToString(cd.SiteID);
                txtLoggedName.Text = CallDetailsBAL.LoggedByName(int.Parse(cd.LoggedBy.ToString()));
                txtCreatedDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), cd.LoggedDate);
                txtCreatedTime.Text = string.Format(Deffinity.systemdefaults.GetStringTimeformat(), cd.LoggedDate);
                ccdSubject.SelectedValue = Convert.ToString(fd.SubjectID);
                txtDetails.Text = fd.Details;
                txtResolution.Text = fd.Resolution;
                ccdAssignedDept.SelectedValue = Convert.ToString(fd.DepartmentID);
               // ccdAssignedName.SelectedValue = Convert.ToString(fd.UserID);
                //txtTimeAccumulated.Text = fd.TimeAccumulated;
                txtTimeWorked.Text = fd.TimeWorked;
                txtSeheduledDateTime.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), fd.ScheduledDate);
                txtScheduledTime.Text = string.Format(Deffinity.systemdefaults.GetStringTimeformat12(), fd.ScheduledDate);
                ddlStartTime.SelectedValue = GetAm_PM(fd.ScheduledDate.Value);
                txtTimeAccumulated.Text = DC.SRV.WebService.CalculateTimeAccumulated(tid);
                txtTimeTakentoResolve.Text = DC.SRV.WebService.CalculateTimeToResolved(tid);
                txtNotes.Text = fd.Notes;
                txtPOnumber.Text = fd.POnumber;
                ccdRequestType.DataBind();
                ccdRequestType.SelectedValue = fd.RequestType.HasValue ? fd.RequestType.ToString() : "0";
                //ddlRequestType.SelectedValue = fd.RequestType.HasValue ? fd.RequestType.ToString() : "0";
                //ccdCategory.TargetControlID ="ddlRequestType";
                ccdCategory.DataBind();
                ccdCategory.SelectedValue = Convert.ToString(fd.CategoryID);
                //ccdSubCategory.TargetControlID = "ddlCategory";
                ccdSubCategory.DataBind();
                ccdSubCategory.SelectedValue = fd.SubCategoryID.HasValue ? fd.SubCategoryID.Value.ToString() : "0";
                //
                hsubid.Value = fd.SubCategoryID.HasValue ? fd.SubCategoryID.Value.ToString() : "0";
                hAssignedTechnicianid.Value = fd.UserID.HasValue ? fd.UserID.Value.ToString() : "0";
                //ddlSubCategory.SelectedIndex = ddlSubCategory.Items.IndexOf(ddlCategory.Items.FindByText(fd.SubCategoryID.HasValue ? fd.SubCategoryID.Value.ToString() : "0"));
                ccdTaskCategory.DataBind();
                ccdTaskCategory.SelectedValue = Convert.ToString(fd.TaskCategoryID.HasValue ? fd.TaskCategoryID : 0);
                
                ccdTaskSubCategory.DataBind();
                ccdTaskSubCategory.SelectedValue = Convert.ToString(fd.TaskSubcategoryID.HasValue ? fd.TaskSubcategoryID : 0);

                txtQty.Text = fd.Qty.ToString();
                txtCustomerCostCode.Text = fd.CustomerCostCode;
                txtCustomerRef.Text = fd.CustomerReference;
                txtStartedDate.Text = fd.DateTimeStarted.HasValue ? fd.DateTimeStarted.Value.ToString(Deffinity.systemdefaults.GetDateformat()) : "";
                txtStartedTime.Text = fd.DateTimeStarted.HasValue ? fd.DateTimeStarted.Value.ToString(Deffinity.systemdefaults.GetTimeformat12()) : "";
               
                txtClosedDate.Text = fd.DateTimeClosed.HasValue ? fd.DateTimeClosed.Value.ToString(Deffinity.systemdefaults.GetDateformat()) : "";
                txtClosedTime.Text = fd.DateTimeClosed.HasValue ? fd.DateTimeClosed.Value.ToString(Deffinity.systemdefaults.GetTimeformat()) : "";

                BindPriorityDdl();
                ddlPriority.SelectedValue = fd.PriorityId.HasValue ? fd.PriorityId.ToString() : "0";
                txtScheduledEndDateTime.Text = fd.ScheduledEndDateTime.HasValue ? fd.ScheduledEndDateTime.Value.ToString(Deffinity.systemdefaults.GetDateformat()) : DateTime.Now.ToString(Deffinity.systemdefaults.GetDateformat());
                txtScheduledEndTime.Text = fd.ScheduledEndDateTime.HasValue ? fd.ScheduledEndDateTime.Value.ToString(Deffinity.systemdefaults.GetTimeformat12()) : DateTime.Now.ToString(Deffinity.systemdefaults.GetTimeformat12());
                ddlEndtime.SelectedValue = GetAm_PM(fd.ScheduledEndDateTime.Value);
                //  txtTimeTakentoResolve.Text = fd.TimeTakentoResolve.HasValue ? fd.TimeTakentoResolve.Value.ToString() : "";
                txtResolutionDateandTime.Text = fd.ResolutionDateandTime.HasValue ? fd.ResolutionDateandTime.Value.ToString(Deffinity.systemdefaults.GetDateformat()) : DateTime.Now.ToString(Deffinity.systemdefaults.GetDateformat());
                txtResolutionTime.Text = fd.ResolutionDateandTime.HasValue ? fd.ResolutionDateandTime.Value.ToString(Deffinity.systemdefaults.GetTimeformat()) : DateTime.Now.ToString(Deffinity.systemdefaults.GetTimeformat());
                if (fd.Preferreddate2.HasValue)
                {
                    DivPreferreddate2.Visible = true;
                    txtPreferreddate2.Text = fd.Preferreddate2.HasValue ? fd.Preferreddate2.Value.ToString(Deffinity.systemdefaults.GetDateformat()) : string.Empty;
                    txtPreferreddatetime2.Text = fd.Preferreddate2.HasValue ? fd.Preferreddate2.Value.ToString(Deffinity.systemdefaults.GetTimeformat()) : string.Empty;
                   
                }
                if (fd.Preferreddate3.HasValue)
                {
                    DivPreferreddate3.Visible = true;
                    txtPreferreddate3.Text = fd.Preferreddate3.HasValue ? fd.Preferreddate3.Value.ToString(Deffinity.systemdefaults.GetDateformat()) : DateTime.Now.ToString(Deffinity.systemdefaults.GetDateformat());
                    txtPreferreddatetime3.Text = fd.Preferreddate3.HasValue ? fd.Preferreddate3.Value.ToString(Deffinity.systemdefaults.GetTimeformat()) : DateTime.Now.ToString(Deffinity.systemdefaults.GetTimeformat());
                }
                txtScheduledToTime.Text = !string.IsNullOrEmpty(fd.ScheduledDatetotime) ? fd.ScheduledDatetotime : string.Empty;
                txtPreferreddatetimeto2.Text = !string.IsNullOrEmpty(fd.Preferreddatetotime2) ? fd.Preferreddatetotime2 : string.Empty;
                txtPreferreddatetimeto3.Text = !string.IsNullOrEmpty(fd.Preferreddatetotime3) ? fd.Preferreddatetotime3 : string.Empty;
                ddlTicketManager.SelectedValue = fd.TicketManagerID.HasValue ? fd.TicketManagerID.ToString() : "0";

                txtSitedetails.Text = fd.Sitedetails;
                txtSubmittedBy.Text = fd.SubmittedBy;
                //set address id to 
                haddressid.Value = fd.ContactAddressID.HasValue ? fd.ContactAddressID.Value.ToString() : string.Empty;
                haid.Value = fd.ContactAddressID.HasValue ? fd.ContactAddressID.Value.ToString() : string.Empty;


                var addressEntity = pbal.v_PortfolioContactAddress_SelectByID(Convert.ToInt32(haddressid.Value)).FirstOrDefault();
                if(addressEntity!= null)
                {
                    hpost.Value = addressEntity.PostCode;
                }
                //IDCRespository<DC.Entity.CallIdAssociatedProduct> aRepository = new DCRepository<DC.Entity.CallIdAssociatedProduct>();
                //var pData = aRepository.GetAll().Where(o=>o.Callid == QueryStringValues.CallID).FirstOrDefault();
                //if(pData != null)
                //{
                //    hfEqID.Value = pData.ProductIds;
                //    hapid.Value = pData.ProductIds;
                //}

                //hb=new HealthCheckBAL();
                //var v = hb.HealthCheck_FormAssignToCall_SelectByCallID(tid);
                //if (v != null)
                //{
                //    ddlForms.SelectedValue = Convert.ToString(v.FormID);
                //}
                //Timeworked validation check
                //DisableTimeworked(cd.StatusID.Value);
                //set key contact
                hfkeyId.Value= FLSDetailsBAL.CallAssoiciateKeyContact_Select(fd.CallID.Value).ToString();
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }


    private int GetValue(string value)
    {
        return int.Parse(string.IsNullOrEmpty(value) ? "0" : value);
    }

    private int AddRecord(int assignedUser = 0, int ProductId =0)
    {
        int id = 0;
        try
        {
            //update the phone no

           // var contactdetails = PortfolioMgt.BAL.PortfolioContactAddressBAL.PorfolioContact_Address_SelectID(Convert.ToInt32(haddressid.Value));
            /* Add Call Details */
            CallDetail cd = new CallDetail();

            cd.SiteID = GetValue(ddlSite.SelectedValue);
            cd.RequestTypeID = GetValue(ddlTypeofRequest.SelectedValue);
            //if (GetValue(ddlStatus.SelectedValue) >0)
            cd.StatusID = 22;// GetValue(ddlStatus.SelectedValue);
            cd.CompanyID = int.Parse(hportfolioid.Value);// GetValue(ddlCompany.SelectedValue);

            cd.RequesterID = Convert.ToInt32(haddressid.Value);// contactdetails.ContactID;// GetValue(hcid.Value);
            cd.LoggedBy = sessionKeys.UID;
            cd.LoggedDate = Convert.ToDateTime(Request.Form[txtCreatedDate.UniqueID] + " " + Request.Form[txtCreatedTime.UniqueID] + ":00");
            CallDetailsBAL.AddCallDetails(cd);
            id = cd.ID;
            // call journal
            DateTime currentDateTime = Convert.ToDateTime(cd.LoggedDate);
            AddCallDetailsJournal(id, cd, currentDateTime);
            //update contact no;
           // UpdateContactnumber(cd.RequesterID.Value);
            /*Add FLS Details*/
            FLSDetail fd = new FLSDetail();
            fd.CallID = id;
            fd.SubjectID = GetValue(ddlSubject.SelectedValue);
            fd.Details = txtDetails.Text.Trim();


            //DateTime dtn = Convert.ToDateTime(dt);
            //DateTime scheduledDateTime = Convert.ToDateTime(txtSeheduledDateTime.Text + " " + txtScheduledTime.Text);
            //if (dtn == scheduledDateTime || scheduledDateTime < dtn)
            //{
            //    fd.ScheduledDate = DateTime.Now;
            //}
            //else
            //{
            fd.ScheduledDate = GetDateTime(txtSeheduledDateTime.Text, (string.IsNullOrEmpty(txtScheduledTime.Text) ? "00:00:00" : txtScheduledTime.Text + ":00"), ddlStartTime.SelectedValue); //Convert.ToDateTime(txtSeheduledDateTime.Text + " " + (string.IsNullOrEmpty(txtScheduledTime.Text) ? "00:00:00" : txtScheduledTime.Text + ":00"));
            //}
            fd.TimeAccumulated = txtTimeAccumulated.Text.Trim();
            fd.TimeTakentoResolve = Convert.ToDouble(!string.IsNullOrEmpty(txtTimeTakentoResolve.Text.Trim()) ? txtTimeTakentoResolve.Text.Trim().Replace(":", ".") : "0");
            fd.TimeWorked = txtTimeWorked.Text.Trim();
            fd.DepartmentID = GetValue(ddlAssignedtoDept.SelectedValue);
            //if(assignedUser == 1)
            //{
            //    if (hrid.Value.ToString().Length > 0)
            //    fd.UserID = Convert.ToInt32(hrid.Value.ToString().Contains(",") ? hrid.Value.ToString().Split(',').ToArray().FirstOrDefault() : hrid.Value.ToString());
            //}
            fd.Notes = txtNotes.Text.Trim();
            //fd.RAGStatus = ddlRAGStatus.SelectedValue;
            fd.POnumber = txtPOnumber.Text.Trim();

            //Newly added
            //FLSDetailsBAL.AddDefaultTypeofRequest(sessionKeys.PortfolioID); //
            fd.RequestType = GetValue(ddlRequestType.SelectedValue);
            fd.SourceOfRequestID = GetValue(ddlSourceOfRequest.SelectedValue);
            fd.CategoryID = GetValue(ddlCategory.SelectedValue);
            fd.SubCategoryID = GetValue(ddlSubCategory.SelectedValue);
            fd.CustomerReference = txtCustomerRef.Text.Trim();
            fd.CustomerCostCode = txtCustomerCostCode.Text.Trim();
            fd.DateTimeStarted = Convert.ToDateTime(Request.Form[txtCreatedDate.UniqueID] + " " + Request.Form[txtCreatedTime.UniqueID] + ":00");
            //fd.DateTimeClosed = Convert.ToDateTime(txtClosedDate.Text + " " + (string.IsNullOrEmpty(txtClosedTime.Text) ? "00:00:00" : txtClosedTime.Text + ":00"));
            fd.SubmittedBy = txtSubmittedBy.Text.Trim();
            //
            fd.TaskCategoryID = GetValue(ddlTaskCategory.SelectedValue);
            fd.TaskSubcategoryID = GetValue(ddlTaskSubCategory.SelectedValue);
            fd.Qty = double.Parse(string.IsNullOrEmpty(txtQty.Text) ? "0" : txtQty.Text);

            var edate = fd.ScheduledDate.Value.AddHours(2);// DateTime.Now.AddHours(2);
            //new fileds on fed 2016
            //fd.ScheduledEndDateTime = edate;
            fd.ScheduledEndDateTime = GetDateTime(txtScheduledEndDateTime.Text, (string.IsNullOrEmpty(txtScheduledEndTime.Text) ? "00:00:00" : txtScheduledEndTime.Text + ":00"), ddlEndtime.SelectedValue); //Convert.ToDateTime(txtScheduledEndDateTime.Text + " " + (string.IsNullOrEmpty(txtScheduledEndTime.Text) ? "00:00:00" : txtScheduledEndTime.Text + ":00"));
            fd.PriorityId = Convert.ToInt32(ddlPriority.SelectedValue);
            fd.TimeTakentoResolve = 0;
            fd.ResolutionDateandTime = Convert.ToDateTime(txtResolutionDateandTime.Text + " " + (string.IsNullOrEmpty(txtResolutionTime.Text) ? "00:00:00" : txtResolutionTime.Text + ":00"));
            fd.Sitedetails = txtSitedetails.Text.Trim();
            fd.ContactAddressID = Convert.ToInt32(haddressid.Value);// contactdetails.ID;// Convert.ToInt32(haddressid.Value);
            if (!string.IsNullOrEmpty(txtPreferreddate2.Text))
                fd.Preferreddate2 = Convert.ToDateTime(txtPreferreddate2.Text + " " + (string.IsNullOrEmpty(txtPreferreddatetime2.Text) ? "00:00:00" : txtPreferreddatetime2.Text + ":00"));
            if (!string.IsNullOrEmpty(txtPreferreddate3.Text))
                fd.Preferreddate3 = Convert.ToDateTime(txtPreferreddate3.Text + " " + (string.IsNullOrEmpty(txtPreferreddatetime3.Text) ? "00:00:00" : txtPreferreddatetime3.Text + ":00"));
            if (!string.IsNullOrEmpty(txtScheduledToTime.Text))
                fd.ScheduledDatetotime = !string.IsNullOrEmpty(txtScheduledToTime.Text) ? txtScheduledToTime.Text : string.Empty;
            if (!string.IsNullOrEmpty(txtPreferreddatetimeto2.Text))
                fd.Preferreddatetotime2 = !string.IsNullOrEmpty(txtPreferreddatetimeto2.Text) ? txtPreferreddatetimeto2.Text : string.Empty;
            if (!string.IsNullOrEmpty(txtPreferreddatetimeto3.Text))
                fd.Preferreddatetotime3 = !string.IsNullOrEmpty(txtPreferreddatetimeto3.Text) ? txtPreferreddatetimeto3.Text : string.Empty;
            fd.TicketManagerID = Convert.ToInt32(!string.IsNullOrEmpty(ddlTicketManager.SelectedValue) ? ddlTicketManager.SelectedValue : "0");
            FLSDetailsBAL.AddFLSDetails(fd);
            // fls details journal
            AddFLSDetailsJournal(id, fd, currentDateTime);
            //Update key contact
            addUpdateKeyContact(id);
            /*Add FLS Time Details ie,Time Accumulated */

            FLSTimeDetail ft = new FLSTimeDetail();
            ft.CallID = id;
            ft.Status = ddlStatus.SelectedItem.Text;
            ft.StatusTime = DateTime.Now;
            FLSTimeDetailsBAL.AddFLSTimeDetail(ft);
            //upload documents
            UploadFiles(id);
            BindData(id);
            SavePlaceholderData(id, Convert.ToInt32(ddlCompany.SelectedValue));


            int i = SendingEmailType();
            //Mail to distribution list
            if (i == 1)
            {
                SendDistributionMail(id, fd);
            }
            //Mail to Priority group
            //else if (i == 2)
            //{
            //    //mail to priority users
            //    BulkEmailSending(id, fd);
            //}
            //  SendMailtoAssignedName(id, fd);
            iframe_DisplayHistory(id);

            if (hcid.Value != "")
            {
                SendMailtoRequester(id, true, fd);
            }
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

    private void UpdateContactnumber(int ContactID)
    {
        try
        {
            var pbc = new PortfolioMgt.BAL.PortfolioContactBAL();
            var cb = pbc.PortfolioContact_SelectByID(ContactID).FirstOrDefault();
            if (cb != null)
            {
                if (cb.Telephone != txtReqTelNo.Text.Trim())
                {
                    cb.Telephone = txtReqTelNo.Text.Trim();
                    pbc.PortfolioContact_update(cb);
                }
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public DateTime GetDateTime(string d_date, string t_time, string t_am_pm)
    {

        string s = string.Format("{0} {1} {2}", d_date, t_time, t_am_pm);
        DateTimeFormatInfo fi = new CultureInfo("en-GB", false).DateTimeFormat;
        DateTime myDate = DateTime.ParseExact(s, "dd/MM/yyyy hh:mm:ss tt", fi);

        return myDate;
    }

    public string GetAm_PM(DateTime d_datetime)
    {
        string s = d_datetime.ToString("tt");
        return s;
    }
    private void addUpdateKeyContact(int id)
    {
        if (hfkeyId.Value != "")
        {
            try
            {
                FLSDetailsBAL.CallAssoiciateKeyContact_Add(id, Convert.ToInt32(hfkeyId.Value));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }

    public int SendingEmailType()
    {
        int i = 1;
        using (DCDataContext dc = new DCDataContext())
        {
            EmailSendingType E_MailType = dc.EmailSendingTypes.Where(a => a.CustomerId == int.Parse(hportfolioid.Value)).FirstOrDefault();
            if (E_MailType != null)
            {
                i = E_MailType.EmailType.Value;
            }
        }
        return i;
    }
  
    private void UpdateRecord(int tid)  //tid -Ticket Id
    {
        try
        {
            int customerId = int.Parse(hportfolioid.Value);
            // check status change email notification
            bool mailToCustomer;
            bool mailToEngineer;
            FLSSectionConfigBAL.CheckEmailNotification(out mailToEngineer, out mailToCustomer, customerId);

            DateTime CurrentDateTime = DateTime.Now;
            int? old_status = null;
            int? assignedtoName;
            /*Update Call Details*/
            CallDetail cd = CallDetailsBAL.SelectbyId(tid);
            int? statusId = 0;
            if (cd != null)
            {
                statusId = cd.StatusID;
                bool isCallDetailsChanged = CompareCallDetails(cd, GetValue(ddlSite.SelectedValue),
                    GetValue(ddlTypeofRequest.SelectedValue), GetValue(ddlStatus.SelectedValue),
                    GetValue(ddlCompany.SelectedValue), GetValue(hcid.Value), sessionKeys.UID);
                //bool isStatusChanged = CompareStatusChanged(cd, int.Parse(ddlStatus.SelectedValue));
                //get old status
                old_status = cd.StatusID;

                cd.SiteID = GetValue(ddlSite.SelectedValue);
                cd.RequestTypeID = GetValue(ddlTypeofRequest.SelectedValue);
                if (GetValue(ddlStatus.SelectedValue) >0)
                cd.StatusID = GetValue(ddlStatus.SelectedValue);
                cd.CompanyID = int.Parse(hportfolioid.Value); //GetValue(ddlCompany.SelectedValue);
               // cd.RequesterID = GetValue(hcid.Value);
                //cd.LoggedDate = DateTime.Now;
                cd.LoggedBy = sessionKeys.UID;
                if (isCallDetailsChanged)
                {
                    AddCallDetailsJournal(tid, cd, CurrentDateTime);
                }
                CallDetailsBAL.CallDetailsUpdate(cd);

                UpdateContactnumber(cd.RequesterID.Value);
                //int id = cd.ID;
            }

            /*Update FLS Details*/

            FLSDetail fd = FLSDetailsBAL.SelectbyId(tid);
            FLSDetail fdj = fd;
            if (fd != null)
            {
                assignedtoName = fd.UserID;
                int deptId = GetValue(ddlAssignedtoDept.SelectedValue);
                int assignedToNameId = GetValue(ddlAssignedtoName.SelectedValue);
                //bool assignedtoNameChanged = AssignedtoNameChanged(fd, Convert.ToInt32(ddlAssignedtoName.SelectedValue));
                
                fd.SubjectID = GetValue(ddlSubject.SelectedValue);
                fd.Details = txtDetails.Text.Trim();
                fd.ScheduledDate = Convert.ToDateTime(txtSeheduledDateTime.Text + " " + (string.IsNullOrEmpty(txtScheduledTime.Text) ? "00:00:00" : txtScheduledTime.Text + ":00"));

                fd.TimeAccumulated = txtTimeAccumulated.Text.Trim();
                fd.TimeWorked = txtTimeWorked.Text.Trim();
                fd.DepartmentID = GetValue(ddlAssignedtoDept.SelectedValue);
                if (GetValue(ddlAssignedtoName.SelectedValue) > 0)
                {
                    fd.UserID = GetValue(ddlAssignedtoName.SelectedValue);
                }
                fd.Notes = txtNotes.Text.Trim();
                //fd.RAGStatus = ddlRAGStatus.SelectedValue;
                fd.POnumber = txtPOnumber.Text.Trim();
                //Newly added
                //FLSDetailsBAL.AddDefaultTypeofRequest(sessionKeys.PortfolioID);//
                fd.RequestType =  GetValue(ddlRequestType.SelectedValue);
                fd.SourceOfRequestID =  GetValue(ddlSourceOfRequest.SelectedValue);
                fd.CategoryID =  GetValue(ddlCategory.SelectedValue);
                //var cs = ccdSubCategory.SelectedValue;
                if (GetValue(ddlSubCategory.SelectedValue) > 0)
                    fd.SubCategoryID = GetValue(ddlSubCategory.SelectedValue);
                fd.CustomerReference = txtCustomerRef.Text.Trim();
                fd.CustomerCostCode = txtCustomerCostCode.Text.Trim();
                // fd.DateTimeStarted = Convert.ToDateTime(txtStartedDate.Text + " " + (string.IsNullOrEmpty(txtStartedTime.Text) ? "00:00:00" : txtStartedTime.Text + ":00"));
                // fd.DateTimeClosed = Convert.ToDateTime(txtClosedDate.Text + " " + (string.IsNullOrEmpty(txtClosedTime.Text) ? "00:00:00" : txtClosedTime.Text + ":00"));
                fd.SubmittedBy = txtSubmittedBy.Text.Trim();
                //
                fd.TaskCategoryID = GetValue(ddlTaskCategory.SelectedValue);
                fd.TaskSubcategoryID = GetValue(ddlTaskSubCategory.SelectedValue);
                fd.Qty = double.Parse(string.IsNullOrEmpty(txtQty.Text) ? "0" : txtQty.Text);


                fd.ScheduledEndDateTime = Convert.ToDateTime(txtScheduledEndDateTime.Text + " " + (string.IsNullOrEmpty(txtScheduledEndTime.Text) ? "00:00:00" : txtScheduledEndTime.Text + ":00"));
                fd.PriorityId = Convert.ToInt32(ddlPriority.SelectedValue);
                //fd.TimeTakentoResolve = txtTimeTakentoResolve.;

                // fd.TimeTakentoResolve = double.Parse(string.IsNullOrEmpty(txtTimeTakentoResolve.Text) ? "0" : txtTimeTakentoResolve.Text.Replace(':', '.'));

                fd.ResolutionDateandTime = Convert.ToDateTime(txtResolutionDateandTime.Text + " " + (string.IsNullOrEmpty(txtResolutionTime.Text) ? "00:00:00" : txtResolutionTime.Text + ":00"));
                fd.Sitedetails = txtSitedetails.Text.Trim();
                if (!string.IsNullOrEmpty(txtPreferreddate2.Text))
                    fd.Preferreddate2 = Convert.ToDateTime(txtPreferreddate2.Text + " " + (string.IsNullOrEmpty(txtPreferreddatetime2.Text) ? "00:00:00" : txtPreferreddatetime2.Text + ":00"));
                if (!string.IsNullOrEmpty(txtPreferreddate3.Text))
                    fd.Preferreddate3 = Convert.ToDateTime(txtPreferreddate3.Text + " " + (string.IsNullOrEmpty(txtPreferreddatetime3.Text) ? "00:00:00" : txtPreferreddatetime3.Text + ":00"));
                if (!string.IsNullOrEmpty(txtScheduledToTime.Text))
                    fd.ScheduledDatetotime = !string.IsNullOrEmpty(txtScheduledToTime.Text) ? txtScheduledToTime.Text : string.Empty;
                if (!string.IsNullOrEmpty(txtPreferreddatetimeto2.Text))
                    fd.Preferreddatetotime2 = !string.IsNullOrEmpty(txtPreferreddatetimeto2.Text) ? txtPreferreddatetimeto2.Text : string.Empty;
                if (!string.IsNullOrEmpty(txtPreferreddatetimeto3.Text))
                    fd.Preferreddatetotime3 = !string.IsNullOrEmpty(txtPreferreddatetimeto3.Text) ? txtPreferreddatetimeto3.Text : string.Empty;
                if (ddlStatus.SelectedItem.Text == "Resolved")
                {
                    fd.Resolution = txtResolution.Text;
                }
                if (ddlStatus.SelectedItem.Text == "Closed" && statusId != 35) // 35 - Closed status for FLS
                {
                    fd.DateTimeClosed = DateTime.Now;
                }
                fd.TicketManagerID = Convert.ToInt32(!string.IsNullOrEmpty(ddlTicketManager.SelectedValue) ? ddlTicketManager.SelectedValue : "0");
                int id = Convert.ToInt32(htid.Value);
                SavePlaceholderData(id, Convert.ToInt32(ddlCompany.SelectedValue));

                bool isFlsdetailsChanged = CompareFLSDetails(fdj, tid, GetValue(ddlSubject.SelectedValue), txtDetails.Text,
                    Convert.ToDateTime(txtSeheduledDateTime.Text + " " + (string.IsNullOrEmpty(txtScheduledTime.Text) ? "00:00:00" : txtScheduledTime.Text + ":00")),
                    txtTimeWorked.Text, deptId, assignedToNameId, txtNotes.Text.Trim(),
                    txtResolution.Text.Trim(), txtPOnumber.Text.Trim(), 0,
                    GetValue(ddlSubCategory.SelectedValue), GetValue(ddlRequestType.SelectedValue),
                    GetValue(ddlSourceOfRequest.SelectedValue), GetValue(ddlPriority.SelectedValue),
                    txtSitedetails.Text.Trim(), txtCustomerRef.Text.Trim(), txtCustomerCostCode.Text.Trim(),
                    fd.Preferreddate2,fd.Preferreddate3,txtScheduledToTime.Text.Trim(), txtPreferreddatetimeto2.Text.Trim(), txtPreferreddatetimeto3.Text.Trim(),
                    Convert.ToInt32(!string.IsNullOrEmpty(ddlTicketManager.SelectedValue) ? ddlTicketManager.SelectedValue : "0"));
                if (isFlsdetailsChanged)
                {
                    AddFLSDetailsJournal(tid, fd, CurrentDateTime);
                }
                FLSDetailsBAL.FLSDetailsUpdate(fd);
                //update key contact
                addUpdateKeyContact(fd.CallID.Value);
                //if assigned user is modified send mail to user
                if (assignedtoName != fd.UserID)
                {
                    if (mailToEngineer)
                    {
                        SendMailtoAssignedName(tid, fd);

                        
                    }
                }

            }
            //if status is modified time details should be updated and send mail to requester
            if (old_status != cd.StatusID)
            {
                FLSTimeDetail ft = new FLSTimeDetail();
                ft.CallID = tid;
                ft.Status = ddlStatus.SelectedItem.Text;
                ft.StatusTime = DateTime.Now;
                FLSTimeDetailsBAL.AddFLSTimeDetail(ft);

            }
            //BindData(id);
            //if (ddlStatus.SelectedItem.Text == "In Hand/WIP")
            //{
            //    FLSDetailsBAL.SetInHandSLAStatus(0, tid);
            //}
            if (ddlStatus.SelectedItem.Text == "Resolved")
            {
                FLSDetailsBAL.SetResolutionSLAStatus(0, tid);
                if (mailToCustomer)
                    ClosedStatusMail(tid, fd);

            }

            if (old_status != cd.StatusID && ddlStatus.SelectedItem.Text != "Resolved")
            {
                // send updated ticket
                //if (mailToCustomer)
                //    SendMailtoRequester(tid, false,fd);
                //int i = SendingEmailType();
                ////Mail to distribution list
                //if (i == 1)
                //{
                //   // SendDistributionMail(tid, fd);
                //}
                //Mail to Priority group
                //else if (i == 2)
                //{
                //    //mail to priority users
                // //   BulkEmailSending(tid, fd);
                //} 

                //if(sessionKeys.SID == 4)
                //{
                //   // TicketManagerMail(tid);
                //}
            }

            //else if (old_status != cd.StatusID)
            //{
            //    // send updated ticket
            //    SendMailtoRequester(id, false);
            //}

            //upload documents
            UploadFiles(tid);
            iframe_DisplayHistory(tid);
            if (ddlStatus.SelectedItem.Text == "Cancelled")
            {
                //remove resouces
                RemoveAssingedReouces();
            }
            if (ddlStatus.SelectedItem.Text == "Resolved")
            {
                //reload the same page
                Response.Redirect(Request.Url.AbsoluteUri, false);
            }
            if (ddlStatus.SelectedItem.Text == "Closed")
            {
                if (sessionKeys.SID == 4)
                {
                    TicketManagerMail(tid);
                }
                ClosedStatusSurveyMail(tid, fd);
                //reload the same page
                Response.Redirect(Request.Url.AbsoluteUri, false);
            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    //if ticket is cancelled remove the assigned resource
    public void RemoveAssingedReouces()
    {
        try
        {
            var cReporsitory = new DCRepository<CallDetail>();
            var cd = cReporsitory.GetAll().Where(o => o.ID == QueryStringValues.CallID).FirstOrDefault();
            var fReporsitory = new DCRepository<FLSDetail>();
            var fd = fReporsitory.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
            //cd.StatusID = 22;
            // cReporsitory.Edit(cd);
            fd.UserID = null;
            fReporsitory.Edit(fd);
            //delete the customer data

            var rReporsitory = new DCRepository<CallResourceSchedule>();
            var rlist = rReporsitory.GetAll().Where(o => o.CallID == QueryStringValues.CallID).ToList();
            if (rlist != null)
            {
                if (rlist.Count > 0)
                {
                    rReporsitory.DeleteAll(rlist);
                }
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public bool AssignedtoNameChanged(FLSDetail fd,int userId)
    {
        bool isChanged = false;
        FLSDetail pastDetails = fd;
        if (fd.UserID != userId)
            isChanged = true;

        return isChanged;
        
    }
    //protected void lnkCancel_Click(object sender, EventArgs e)
    //{
    //    popIssues.Hide();
    //    ModalPopupExtender1.Hide();
    //    ModalPopupExtender2.Hide();
    //}
    //protected void btn_CategoryEdit_Click(object sender, EventArgs e)
    //{
    //        if (ddlCategory.SelectedValue != "0")
    //        {
    //            //HID_Category.Value = ddlCategory.SelectedValue;
    //            txtCategory.Text = ddlCategory.SelectedItem.Text;
    //           // panelVisibleCategory(false, true);
    //            ModalPopupExtender1.Show();
    //            hfId.Value = ddlCategory.SelectedValue;
    //        }
    //}
    //protected void btnSaveCategory_Click1(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //if (Convert.ToInt32(ddlRequestType.SelectedValue) > 0)
    //        //{
    //            Category category = new Category();
    //            category.Name = txtCategory.Text.Trim();
    //        category.TypeOfRequestID = 0;// Convert.ToInt32(ddlRequestType.SelectedValue);
    //                bool exists = CategoryBAL.CheckCategory(txtCategory.Text.Trim());
    //                if (!exists)
    //                {
    //                    CategoryBAL.AddCategory(category);
    //                    lblError.Text = "Category added successfully";
    //                    //lblError.ForeColor = System.Drawing.Color.Green;
    //                    popIssues.Hide();

    //                    upanelMain.Update();
    //                    hfId.Value = "0";
    //                    txtCategory.Text = string.Empty;
    //                    //Reload the
    //                   // ccdCategory.DataBind();
    //                    ccdCategory.SelectedValue = category.ID.ToString();
    //                }
    //                else
    //                {
    //                    lblError.Text = "Category name already exists";
    //                    //lblError.ForeColor = System.Drawing.Color.Red;
    //                }
    //        //}
    //        //else
    //        //{
    //        //    lblError.Text = "Please select type of request.";
    //        //    lblError.ForeColor = System.Drawing.Color.Red;
    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {


            if (htid.Value == "0")
            {
                int id = 0;

                int productid = Convert.ToInt32(!string.IsNullOrEmpty(hfEqID.Value) ? hfEqID.Value : "0");
                //Convert.ToInt32(!string.IsNullOrEmpty(ddlEquipment.SelectedValue) ? ddlEquipment.SelectedValue : "0");
                if (productid > 0)
                    id = AddRecord(1, productid);
                else
                    //if no equipemnt set sero
                    id = AddRecord(1, 0);

               // SaveHealthForm(id);
                //  SavePlaceholderData(id, Convert.ToInt32(ddlCompany.SelectedValue));
                Response.Redirect("~/WF/DC/FLSForm.aspx?CCID=" + FLSDetailsBAL.GetCallIDByCustomer(id) + "&callid=" + id.ToString() + "&SDID=" + id.ToString());
            }

            else
            {
                UpdateRecord(int.Parse(htid.Value));
                //UpdateHealthForm();
                // FLS custom fields data insert
                int id = Convert.ToInt32(htid.Value);
                //  SavePlaceholderData(id, Convert.ToInt32(ddlCompany.SelectedValue));
                Response.Redirect("~/WF/DC/FLSForm.aspx?CCID=" + FLSDetailsBAL.GetCallIDByCustomer(int.Parse(htid.Value)) + "&callid=" + int.Parse(htid.Value) + "&SDID=" + int.Parse(htid.Value));
            }
            //  }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private string checkTicketExist(int product,int RequesterID, int CategoryID)
    {
        string retval = string.Empty;
        IDCRespository<DC.Entity.CallIdAssociatedProduct> pRepostioty = new DCRepository<DC.Entity.CallIdAssociatedProduct>();
        IDCRespository<DC.Entity.CallDetail> cRepostioty = new DCRepository<DC.Entity.CallDetail>();
        IDCRespository<DC.Entity.FLSDetail> fRepostioty = new DCRepository<DC.Entity.FLSDetail>();

        try
        {
            //var plist = pRepostioty.GetAll().Where(o => o.ProductIds == product.ToString()).ToList();
            ////Product exists
            //if (plist.Count > 0)
            //{
            var tdate = DateTime.Now;

                var clist = cRepostioty.GetAll().Where(o => o.RequesterID == RequesterID &&  o.StatusID != 33 && (o.LoggedDate.Value.Year == tdate.Year && o.LoggedDate.Value.Month == tdate.Month && o.LoggedDate.Value.Day == tdate.Day)).ToList();
                if (clist.Count > 0)
                {
                    var fentity = fRepostioty.GetAll().Where(o=>clist.Select(p=>p.ID).ToArray().Contains(o.CallID.HasValue?o.CallID.Value:0) && o.CategoryID == CategoryID).FirstOrDefault();
                    if (fentity != null)
                    {
                        retval = fentity.CallID.Value.ToString();                  

                    }
                    else
                        retval = string.Empty;
                }
                else
                    retval = string.Empty;
            //}
            //else
            //    retval = string.Empty;
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        return retval;

    }
    //private bool CheckDateValidation()
    //{
    //    bool error = false;
    //    DateTime loggedDateTime = Convert.ToDateTime(txtCreatedDate.Text + " " + (string.IsNullOrEmpty(txtCreatedTime.Text) ? "00:00:00" : txtCreatedTime.Text + ":00"));
    //    DateTime startedDateTime = Convert.ToDateTime(txtStartedDate.Text + " " + (string.IsNullOrEmpty(txtStartedTime.Text) ? "00:00:00" : txtStartedTime.Text + ":00"));
    //    DateTime closedDateTime = Convert.ToDateTime(txtClosedDate.Text + " " + (string.IsNullOrEmpty(txtClosedTime.Text) ? "00:00:00" : txtClosedTime.Text + ":00"));
    //    if (startedDateTime < loggedDateTime)
    //    {
    //        lblMsg.Text = "Date and Time Started must be a greater than Logged Date/Time";
    //        error = true;
    //    }
    //    else if (closedDateTime < startedDateTime)
    //    {
    //        lblMsg.Text = "Date and Time Closed must be a greater than Logged Date/Time";
    //        error = true;
    //    }
    //    return error;
    //}


    



    protected void ImgDocumentsUpload_Click(object sender, EventArgs e)
    {
        UploadFiles(int.Parse(htid.Value));
    }
    private void UploadFiles(int callid)
    {
        string filename = string.Empty;
        string path = string.Empty;
        string contenttype = string.Empty;
        string DocumentId = string.Empty;
        string ext = string.Empty;
        string fullname = string.Empty;
        try
        {
            //if (DocumentsUploadnew.HasFiles)
            //{
            //    foreach (HttpPostedFile uploadedFile in DocumentsUploadnew.PostedFiles)
            //    {
            //        uploadedFile.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Images/"), uploadedFile.FileName));
            //        //listofuploadedfiles.Text += String.Format("{0}<br />", uploadedFile.FileName);
            //    }
            //}
            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {

                    filename = hpf.FileName;
                    path = Server.MapPath("~/WF/UploadData/DC");
                    contenttype = hpf.ContentType;
                    DocumentId = Guid.NewGuid().ToString();
                    ext = Path.GetExtension(hpf.FileName);
                    fullname = string.Format("{0}/{1}{2}", path, DocumentId, ext);
                    if (callid > 0)
                    {
                        string[] xArray = { ".jpg", ".png", ".gif" };
                        if (xArray.Contains(ext.ToLower()))
                        {
                            Stream fs = hpf.InputStream;
                            BinaryReader br = new BinaryReader(fs);
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            ImageManager.Save_FlsCustomerFiles(bytes, DocumentId);
                        }
                        else
                        {
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);
                            hpf.SaveAs(fullname);
                        }
                        Document d = new Document();
                        d.CallID = callid;
                        d.DocumentID = DocumentId;
                        d.FileName = filename;
                        d.ContentType = contenttype;
                        DocumentsBAL.AddDocuments(d);
                    }
                }

            }
            Response.Redirect(Request.Url.AbsoluteUri, false);
            //BindDocuments(callid);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public static string GetImageUrl(string a_gId)
    {


        string path = "~/WF/UploadData/DC/" + a_gId.ToString() + "_thumb.png";

        if (!File.Exists(HttpContext.Current.Server.MapPath(path)))
        {
            path = "~/WF/UploadData/DC/asset_0.png?t=" + DateTime.Now.Second.ToString();
        }
        return path;

        // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

    }
    public static string GetImageUrlOriginal(string a_gId)
    {


        string path = "~/WF/UploadData/DC/" + a_gId.ToString() + ".png";

        if (!File.Exists(HttpContext.Current.Server.MapPath(path)))
        {
            path = "~/WF/UploadData/DC/asset_0.png?t=" + DateTime.Now.Second.ToString();
        }
        return path;

        // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

    }
    public bool CheckImageVisibility(string a_guid)
    {
        bool _visible = false;
        string path = "~/WF/UploadData/DC/" + a_guid.ToString() + "_thumb.png";

        if (File.Exists(HttpContext.Current.Server.MapPath(path)))
        {
            _visible = true;
        }
        return _visible;
    }
    private void BindDocuments(int cid)
    {
        try
        {
            List<Document> dList = DocumentsBAL.SelectbyCallId(cid);
            if (dList.Count > 0)
            {
                GvDocuments.DataSource = dList;
                GvDocuments.DataBind();
            }
            else
            {
                GvDocuments.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void GvDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Download")
        {
            try
            {
                GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string contenttype = GvDocuments.DataKeys[gvrow.RowIndex].Values[1].ToString();
                string filename = GvDocuments.DataKeys[gvrow.RowIndex].Values[2].ToString();
                string[] ex = filename.Split('.');
                string ext = ex[ex.Length - 1];
                string filepath = string.Format("~/WF/UploadData/DC/{0}.{1}", e.CommandArgument.ToString(), ext);
                Response.ContentType = contenttype;
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                Context.Response.ContentType = "octet/stream";
                Response.TransmitFile(filepath);
                Response.End();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        else if (e.CommandName == "DeleteFile")
        {
            try
            {
                GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string ID = GvDocuments.DataKeys[gvrow.RowIndex].Values[0].ToString();
                string contenttype = GvDocuments.DataKeys[gvrow.RowIndex].Values[1].ToString();
                string filename = GvDocuments.DataKeys[gvrow.RowIndex].Values[2].ToString();
                string filepath = string.Format("~/WF/UploadData/DC/{0}.{1}", e.CommandArgument.ToString(), filename);
                File.Delete(Server.MapPath(filepath));
                DocumentsBAL.DocumentsDelete(int.Parse(ID));
                BindDocuments(int.Parse(htid.Value));

                iframe_DisplayHistory(int.Parse(htid.Value));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }

    private void ClearFields()
    {
        ccdSite.SelectedValue = "0";
        ccdType.SelectedValue = "0";
        ccdStatus.DataBind();
        ccdStatus.SelectedValue = "0";
        ccdCompany.SelectedValue = "0";
        //ccdName.DataBind();
        //ddlName.SelectedValue = "0";
        ccdSubject.SelectedValue = "0";
        ccdAssignedDept.SelectedValue = "0";
        ccdAssignedName.SelectedValue = "0";
        txtSeheduledDateTime.Text = string.Empty;
        txtDetails.Text = string.Empty;
        txtTimeAccumulated.Text = string.Empty;
        txtTimeWorked.Text = string.Empty;
        txtNotes.Text = string.Empty;
        txtPOnumber.Text = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearFields();
            iframe_DisplayHistory(int.Parse(htid.Value));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
    private bool CompareStatusChanged(CallDetail cd,int statusId)
    {
        bool isChanged=false;
        try
        {
            CallDetail pastDetails = cd;
            if (pastDetails.StatusID != statusId)
                isChanged = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return isChanged;
    }
   

    #region Jounal
    private bool CompareCallDetails(CallDetail cd, int sid, int rtid, int stid, int cid, int rid, int lid)
    {
        bool isChanged = false;
        try
        {
            CallDetail pastDetails = cd;
            if (pastDetails.SiteID != sid)
                isChanged = true;
            else if (pastDetails.RequestTypeID != rtid)
                isChanged = true;
            else if (pastDetails.StatusID != stid)
                isChanged = true;
            else if (pastDetails.CompanyID != cid)
                isChanged = true;
            else if (pastDetails.RequesterID != rid)
                isChanged = true;
            else if (pastDetails.LoggedBy != lid)
                isChanged = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return isChanged;
    }
    private bool CompareFLSDetails(FLSDetail fd, int callId, int subId, string details, DateTime sd,
        string timeWorked, int deptId, int userId, string notes, string resolution, string ponumber,
        int catId, int SubCatId, int TypeOfReq, int SourceofRequest, int Priority, string Sitedetails, string CustomerRef, string CustomerCostCode, DateTime? pd2, DateTime? pd3, string sdt, string pdt2, string pdt3,int ticketmanager)
    {
        bool isChanged = false;
        try
        {
            FLSDetail pastDetails = fd;
            if (pastDetails.CallID != callId)
                isChanged = true;
            else if (pastDetails.SubjectID != subId)
                isChanged = true;
            else if (pastDetails.Details != details)
                isChanged = true;
            else if (pastDetails.ScheduledDate != sd)
                isChanged = true;
            else if (pastDetails.Preferreddate2 != pd2)
                isChanged = true;
            else if (pastDetails.Preferreddate3 != pd3)
                isChanged = true;
            else if (pastDetails.ScheduledDatetotime != sdt)
                isChanged = true;
            else if (pastDetails.Preferreddatetotime2 != pdt2)
                isChanged = true;
            else if (pastDetails.Preferreddatetotime3 != pdt3)
                isChanged = true;
            else if (pastDetails.TimeWorked != timeWorked)
                isChanged = true;
            else if (pastDetails.DepartmentID != deptId)
                isChanged = true;
            else if (pastDetails.UserID != userId && userId != 0)
                isChanged = true;
            else if (pastDetails.Notes != notes)
                isChanged = true;
            else if (pastDetails.Resolution != resolution && pastDetails.Resolution != null)
                isChanged = true;
            else if (pastDetails.POnumber != ponumber)
                isChanged = true;

            else if (pastDetails.CategoryID != catId && catId != 0)
                isChanged = true;
            else if (pastDetails.SubCategoryID != SubCatId && SubCatId != 0)
                isChanged = true;
            else if (pastDetails.RequestType != TypeOfReq && TypeOfReq != 0)
                isChanged = true;
            else if (pastDetails.SourceOfRequestID != SourceofRequest)
                isChanged = true;
            else if (pastDetails.PriorityId != Priority)
                isChanged = true;
            else if (pastDetails.Sitedetails != Sitedetails)
                isChanged = true;
            else if (pastDetails.CustomerReference != CustomerRef)
                isChanged = true;
            else if (pastDetails.CustomerCostCode != CustomerCostCode)
                isChanged = true;
            else if (pastDetails.TicketManagerID != ticketmanager)
                isChanged = true;
            

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return isChanged;
    }
    private void AddFLSDetailsJournal(int cid,FLSDetail fd, DateTime modifiedDate)
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
            fj.Resolution = txtResolution.Text.Trim();
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

    #endregion

    #region Mail

    public void SendMailtoAssignedName(int cid, FLSDetail flsDetail)
    {
        try
        {
            int customerId = int.Parse(hportfolioid.Value);
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();
            EmailFooter ef = new EmailFooter();
            AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select(customerId);
            var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == int.Parse(hportfolioid.Value)).ToList();
            ef = FooterEmail.EmailFooter_selectByID(int.Parse(ddlTypeofRequest.SelectedValue), customerId);
            UserMgt.Entity.Contractor pc = ws.GetContractorDetails(int.Parse(ddlAssignedtoName.SelectedValue));
            string subject = sessionKeys.JobDisplayName +" Reference: " + FLSDetailsBAL.GetCallIDByCustomer(cid).ToString() + " has been assigned to you.";
            Emailer em = new Emailer();
            string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSAssignedToNameMail.htm");
            body = body.Replace("[mail_head]", sessionKeys.JobDisplayName);
            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            body = body.Replace("[user]", ddlAssignedtoName.SelectedItem.Text);
            body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
            body = body.Replace("[Status]", ddlStatus.SelectedItem.Text);

            //body = body.Replace("[Company]", ddlCompany.SelectedItem.Text);
            //body = body.Replace("[sitename]", ddlSite.SelectedItem.Text);
            //body = body.Replace("[reference]", cid.ToString());
            //body = body.Replace("[details]", txtDetails.Text);
            //body = body.Replace("[seduledate]", txtSeheduledDateTime.Text + " " + txtScheduledTime.Text);
            //body = body.Replace("[notes]", txtNotes.Text);

            StringBuilder sb = new StringBuilder();
            //sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Customer:", ddlCompany.SelectedItem.Text));
            //sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Site information:", ddlSite.SelectedItem.Text));
            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", sessionKeys.JobDisplayName+ " Reference:", "" + FLSDetailsBAL.GetCallIDByCustomer(cid).ToString()));
            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Status:", ddlStatus.SelectedItem.Text));
            if (GetFieldVisibility("details", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Details of the request:", txtDetails.Text));
            if (GetFieldVisibility("Priority", fieldList))
            {
                if (ddlPriority.SelectedItem.Text != "Please select")
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Priority:", ddlPriority.SelectedItem.Text));
            }
            if (GetFieldVisibility("Scheduled Date/Time", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Scheduled Date/Time:", flsDetail.ScheduledDate.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat()) ));
            //sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Scheduled Date/Time:", flsDetail.ScheduledDate.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat()) + (!string.IsNullOrEmpty(flsDetail.ScheduledDatetotime) ? " - " + flsDetail.ScheduledDatetotime : string.Empty)));
            if (GetFieldVisibility("Scheduled End Date/Time", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Scheduled End Date/Time:", flsDetail.ScheduledEndDateTime.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat())));
            if (GetFieldVisibility("Time Taken to Resolve", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Time Taken to Resolve:", flsDetail.TimeTakentoResolve.Value.ToString()));
            if (GetFieldVisibility("Notes", fieldList))
            {
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Notes:", txtNotes.Text));
            }

            string[] customer = Regex.Split(ccdCompany.SelectedValue, ":::");
            int customerId1 = Convert.ToInt32(customer[0] == "" ? "0" : customer[0]);
            List<FLSAdditionalInfo> flsAdditionalInfoList = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(cid);
            List<FLSCustomField> clist = CustomFormDesignerBAL.GetFieldList(customerId1,0).ToList();
            foreach (FLSCustomField c in clist)
            {
                string val = flsAdditionalInfoList.Where(p => p.CustomFieldID == c.ID).Select(p => p.CustomFieldValue).FirstOrDefault();
                string Lname = c.LabelName;
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", Lname, val));
            }

            body = body.Replace("[datarow]", sb.ToString());
            em.SendingMail(fromemailid, subject, body, pc.EmailAddress);

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private bool GetFieldVisibility(string fieldName, List<FLSFieldsConfig> fieldList)
    {
        bool isVisible = true;
        var exists = fieldList.Where(f => f.DefaultName.ToLower() == fieldName.ToLower()).FirstOrDefault();
        if (exists != null)
            isVisible = Convert.ToBoolean(exists.IsVisible);
        return isVisible;
    }
    public string TicketManagerMail(int callid)
    {
       
        var jlist = FLSDetailsBAL.Jqgridlist(callid);
        Emailer em = new Emailer();
        string body = em.ReadFile("~/WF/DC/EmailTemplates/TicketManagerMail.html");
        try
        {
            var jEntity = jlist.FirstOrDefault();
            body = body.Replace("[mail_head]", sessionKeys.JobDisplayName);
            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            body = body.Replace("[Status]", jEntity.Status);
            body = body.Replace("[serviceprovider]", jEntity.AssignedTechnician);
            body = body.Replace("[status]", jEntity.Status);
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Ref:", "" + callid.ToString()));
            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Details:", jEntity.Details));
            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Customer:", jEntity.RequesterName));
            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Full Address:", jEntity.RequestersAddress + ", " + jEntity.RequestersCity + ", " + jEntity.RequestersTown + ", " + jEntity.RequestersPostCode));
            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Status:", jEntity.Status));
            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Value:", jEntity.TotalCost));
            body = body.Replace("[datarow]", sb.ToString());

            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();
            // var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).ToList();
            string subject = string.Format( "Ref:{0} Set to Status: {1} by {2}",callid,jEntity.Status,jEntity.AssignedTechnician);
            //send mail to ticket manager
            if(!string.IsNullOrEmpty(jEntity.TicketManager))
            {
                string body2 = body;
                body2 = body2.Replace("[user]", jEntity.TicketManager);
                em.SendingMail(fromemailid, subject, body2, jEntity.TicketManagerEmail);
            }

            var strList = FLSDetailsBAL.GetTicketManagers();
            if (strList.Count > 0)
            {
                foreach (var s in strList)
                {
                   string body1 = body;
                   body1 = body1.Replace("[user]", s.Name);
                    em.SendingMail(fromemailid, subject, body1, s.Email);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return body;
    }
    public string MailBody(FLSDetail flsDetail, int cid)
    {
        var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == int.Parse(hportfolioid.Value)).ToList();
        Emailer em = new Emailer();
        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSDistributionMail.htm");
        try
        {
            body = body.Replace("[mail_head]", sessionKeys.JobDisplayName);
            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            body = body.Replace("[Status]", ddlStatus.SelectedItem.Text);
            StringBuilder sb = new StringBuilder();
            //sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Customer:", ddlCompany.SelectedItem.Text));
            //sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Site information:", ddlSite.SelectedItem.Text));
            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", sessionKeys.JobDisplayName +" Reference:", "" + FLSDetailsBAL.GetCallIDByCustomer(cid).ToString()));
            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Status:", ddlStatus.SelectedItem.Text));
            if (GetFieldVisibility("details", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Details of the request:", txtDetails.Text));
            if (GetFieldVisibility("Priority", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Priority:", ddlPriority.SelectedItem.Text));
            if (GetFieldVisibility("Scheduled Date/Time", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Preferred Date / Time for Service 1:", flsDetail.ScheduledDate.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat())));
            if (flsDetail.Preferreddate2.HasValue)
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Preferred Date / Time for Service 2:", flsDetail.Preferreddate2.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat())));
            if (flsDetail.Preferreddate3.HasValue)
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Preferred Date / Time for Service 3:", flsDetail.Preferreddate3.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat())));
            if (GetFieldVisibility("Scheduled End Date/Time", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Scheduled End Date/Time:", flsDetail.ScheduledEndDateTime.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat())));
            if (GetFieldVisibility("Time Taken to Resolve", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Time Taken to Resolve:", flsDetail.TimeTakentoResolve.Value.ToString()));
            if (GetFieldVisibility("Notes", fieldList))
            {
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Notes:", ""));
            }

            string[] customer = Regex.Split(ccdCompany.SelectedValue, ":::");
            int customerId = Convert.ToInt32(customer[0] == "" ? "0" : customer[0]);
            List<FLSAdditionalInfo> flsAdditionalInfoList = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(cid);
            List<FLSCustomField> clist = CustomFormDesignerBAL.GetFieldList(customerId,0).ToList();
            foreach (FLSCustomField c in clist)
            {
                string val = flsAdditionalInfoList.Where(p => p.CustomFieldID == c.ID).Select(p => p.CustomFieldValue).FirstOrDefault();
                string Lname = c.LabelName;
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", Lname, val));
            }
            //body = body.Replace("[Company]", ddlCompany.SelectedItem.Text);
            //body = body.Replace("[sitename]", ddlSite.SelectedItem.Text);

            //body = body.Replace("[reference]", cid.ToString());
            //var details= fieldsList.Where(f => f.DefaultName.ToLower() == "details").FirstOrDefault();
            //body = body.Replace("[details]", txtDetails.Text);
            //if (!Convert.ToBoolean(details.IsVisible))
            //{
            //    body = body.Replace("Details of the request:", "");
            //    body = body.Replace("[details]", "");
            //}
            //body = body.Replace("[seduledate]", txtSeheduledDateTime.Text + " " + txtScheduledTime.Text);
            //body = body.Replace("[notes]", txtNotes.Text);
            body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
            //body = body.Replace("[Status]", ddlStatus.SelectedItem.Text);
            body = body.Replace("[datarow]", sb.ToString());
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return body;
    }
    private void SendDistributionMail(int cid,FLSDetail flsDetail)
    {
        try
        {
            List<int> idlist = SecurityAccessMail.GetIds(6, int.Parse(hportfolioid.Value)); // 6 for FLS
           
            if (idlist.Count > 0)
            {
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                DC.SRV.WebService ws = new DC.SRV.WebService();
               // var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).ToList();
                string subject = sessionKeys.JobDisplayName +" Reference: " + FLSDetailsBAL.GetCallIDByCustomer(cid) + " Customer has raised a new "+ sessionKeys.JobDisplayName +".";
               
                Emailer em = new Emailer();
                string body = MailBody(flsDetail, cid);
                
                List<string> strList = SecurityAccessMail.GetEmailAddresses(idlist);
                if (strList.Count > 0)
                {
                    foreach (string s in strList)
                    {
                        em.SendingMail(fromemailid, subject, body, s);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //Priority mail
    public void BulkEmailSending(int cid, FLSDetail flsDetail)
    {
        try
        {
            if ((flsDetail.PriorityId.HasValue ? flsDetail.PriorityId.Value : 0) >0)
            {
                Emailer em = new Emailer();
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                string subject = sessionKeys.JobDisplayName+" Reference: " + FLSDetailsBAL.GetCallIDByCustomer(cid) + " Customer has raised a new "+ sessionKeys.JobDisplayName + ".";
                string body = MailBody(flsDetail, cid);
                string EmailsList = GetEmailIdsWithPriority(flsDetail.PriorityId.HasValue ? flsDetail.PriorityId.Value : 0);
                if (EmailsList != string.Empty)
                {
                    em.BulkMailSending(fromemailid, subject, body, EmailsList);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public string GetEmailIdsWithPriority(int priorityId)
    {
        string Emails = string.Empty;
        try
        {
            using (UserDataContext Udc = new UserDataContext())
            {
                var UserList = Udc.Contractors.Where(a => a.Status.ToLower() == "active").ToList();
                using (DCDataContext Ddc = new DCDataContext())
                {
                    var MailsList = Ddc.MailSendingWithPriorities.Where(a => a.PriorityId == priorityId && a.CustomerID == int.Parse(hportfolioid.Value)).FirstOrDefault();
                    if (MailsList != null)
                    {
                        string[] UserIds = MailsList.UserID.Split(',');
                        foreach (string Id in UserIds)
                        {
                            if (Id != string.Empty)
                            {
                                if (Emails == string.Empty)
                                {
                                    Emails = UserList.Where(a => a.ID == int.Parse(Id)).Select(a => a.EmailAddress).FirstOrDefault();
                                }
                                else
                                {
                                    Emails = Emails + "," + UserList.Where(a => a.ID == int.Parse(Id)).Select(a => a.EmailAddress).FirstOrDefault();
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
        return Emails;
    }
    private void ClosedStatusMail(int cid,FLSDetail flsDetail)
    {
        try
        {
            int customerId = sessionKeys.PortfolioID;
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();
            var jqGrid = FLSDetailsBAL.Jqgridlist(cid).FirstOrDefault();
            EmailFooter ef = new EmailFooter();
            AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select(customerId);
            ef = FooterEmail.EmailFooter_selectByID(int.Parse(ddlTypeofRequest.SelectedValue), customerId);
            var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == int.Parse(hportfolioid.Value)).ToList();
            PortfolioContact pc = ws.GetContactDetails(int.Parse(hcid.Value));
            string subject = sessionKeys.JobDisplayName+" Reference: " + FLSDetailsBAL.GetCallIDByCustomer(cid).ToString() + " is been marked as Resolved";
            Emailer em = new Emailer();
            string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSClosedMail.htm");
            body = body.Replace("[mail_head]", sessionKeys.JobDisplayName+" Request");
            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
          
            body = body.Replace("[user]", jqGrid.RequesterName);
            body = body.Replace("[adminemail]", ae == null ? string.Empty : ae.EmailAddress);
            body = body.Replace("[Status]", ddlStatus.SelectedItem.Text);
            // body = body.Replace("[footer]", Server.HtmlDecode(ef == null ? string.Empty : ef.EmailFooter1));
            body = body.Replace("[footer]", Server.HtmlDecode(ef == null ? string.Empty : string.Empty));

            //body = body.Replace("[Company]", ddlCompany.SelectedItem.Text);
            //body = body.Replace("[reference]", cid.ToString());
            //body = body.Replace("[sitename]", ddlSite.SelectedItem.Text);
            //body = body.Replace("[notes]", txtNotes.Text);
            //body = body.Replace("[details]", txtDetails.Text);
            //body = body.Replace("[seduledate]", txtSeheduledDateTime.Text+" "+txtScheduledTime.Text);
            //body = body.Replace("[resolution]", txtResolution.Text);




            StringBuilder sb = new StringBuilder();
            //sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Customer:", ddlCompany.SelectedItem.Text));
            //sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Site information:", ddlSite.SelectedItem.Text));
            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", sessionKeys.JobDisplayName+" Reference:", "" + cid.ToString()));
            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Status:", ddlStatus.SelectedItem.Text));
            if (GetFieldVisibility("details", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Details of the request:", txtDetails.Text));
            if (GetFieldVisibility("Priority", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Priority:", ddlPriority.SelectedItem.Text));
            if (GetFieldVisibility("Scheduled Date/Time", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Preferred Date/Time:", flsDetail.ScheduledDate.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat())));
            if (GetFieldVisibility("Notes", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Notes:", txtNotes.Text));
            if (GetFieldVisibility("Scheduled End Date/Time", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Scheduled End Date/Time:", flsDetail.ScheduledEndDateTime.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat())));
            if (GetFieldVisibility("Time Taken to Resolve", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Time Taken to Resolve:", flsDetail.TimeTakentoResolve.Value.ToString()));
            //sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Resolution:", txtResolution.Text));
            body = body.Replace("[datarow]", sb.ToString());
            em.SendingMail(fromemailid, subject, body, pc.Email);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //private void ClosedStatusMail(int cid, FLSDetail flsDetail)
    //{
    //    try
    //    {
    //        int customerId = sessionKeys.PortfolioID;
    //        string fromemailid = Deffinity.systemdefaults.GetFromEmail();
    //        DC.SRV.WebService ws = new DC.SRV.WebService();

    //        EmailFooter ef = new EmailFooter();
    //        AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select(customerId);
    //        ef = FooterEmail.EmailFooter_selectByID(int.Parse(ddlTypeofRequest.SelectedValue), customerId);
    //        var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).ToList();
    //        PortfolioContact pc = ws.GetContactDetails(int.Parse(ddlName.SelectedValue));
    //        string subject = "Ticket Reference: " + cid + " is been marked as Resolved";
    //        Emailer em = new Emailer();
    //        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSClosedMail.htm");
    //        body = body.Replace("[mail_head]", "Service Desk Request");
    //        body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
    //        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

    //        body = body.Replace("[user]", ddlName.SelectedItem.Text);
    //        body = body.Replace("[adminemail]", ae == null ? string.Empty : ae.EmailAddress);
    //        body = body.Replace("[Status]", ddlStatus.SelectedItem.Text);
    //        body = body.Replace("[footer]", Server.HtmlDecode(ef == null ? string.Empty : ef.EmailFooter1));

    //        StringBuilder sb = new StringBuilder();
    //        sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Customer:", ddlCompany.SelectedItem.Text));
    //        sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Site information:", ddlSite.SelectedItem.Text));
    //        sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Ticket Reference:", "" + cid.ToString()));
    //        sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Status of Service Desk:", ddlStatus.SelectedItem.Text));
    //        if (GetFieldVisibility("details", fieldList))
    //            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Details of the request:", txtDetails.Text));
    //        if (GetFieldVisibility("Priority", fieldList))
    //            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Priority:", ddlPriority.SelectedItem.Text));
    //        if (GetFieldVisibility("Scheduled Date/Time", fieldList))
    //            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Scheduled Date/Time:", flsDetail.ScheduledDate.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat())));
    //        if (GetFieldVisibility("Notes", fieldList))
    //            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Notes:", txtNotes.Text));
    //        if (GetFieldVisibility("Scheduled End Date/Time", fieldList))
    //            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Scheduled End Date/Time:", flsDetail.ScheduledEndDateTime.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat())));
    //        if (GetFieldVisibility("Time Taken to Resolve", fieldList))
    //            sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Time Taken to Resolve:", flsDetail.TimeTakentoResolve.Value.ToString()));
    //        sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Resolution:", txtResolution.Text));
    //        body = body.Replace("[datarow]", sb.ToString());
    //        em.SendingMail(fromemailid, subject, body, pc.Email);
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    private void ClosedStatusSurveyMail(int cid, FLSDetail flsDetail)
    {
        try
        {
            int customerId = int.Parse(hportfolioid.Value);
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();

            EmailFooter ef = new EmailFooter();
            AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select(customerId);
            ef = FooterEmail.EmailFooter_selectByID(int.Parse(ddlTypeofRequest.SelectedValue), customerId);
            var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == int.Parse(hportfolioid.Value)).ToList();
            var pdetails = FLSDetailsBAL.Jqgridlist(cid).FirstOrDefault();
            PortfolioContact pc = ws.GetContactDetails(int.Parse(hcid.Value));
            string subject = sessionKeys.JobDisplayName+ " Reference: " + pdetails.CCID.ToString() + " is been marked as Closed";
            Emailer em = new Emailer();
            string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSClosedSurvey.htm");
            body = body.Replace("[mail_head]", sessionKeys.JobDisplayName);
            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

            body = body.Replace("[user]", pdetails.RequesterName);
            body = body.Replace("[adminemail]", ae == null ? string.Empty : ae.EmailAddress);
            body = body.Replace("[Status]", ddlStatus.SelectedItem.Text);
            //body = body.Replace("[footer]", Server.HtmlDecode(ef == null ? string.Empty : ef.EmailFooter1));
            body = body.Replace("[footer]", Server.HtmlDecode(ef == null ? string.Empty : string.Empty));
            body = body.Replace("[ref]", "" + pdetails.CCID.ToString());
            body = body.Replace("[linksurvey]", string.Format("<a href='{0}'> click here </a> ", Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/Feedback/Feedbackentry.aspx?CID=" + pc.ID.ToString()+"&callid="+QueryStringValues.CallID));
           
            em.SendingMail(fromemailid, subject, body, pc.Email);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void SendMailtoRequester(int cid, bool check_insert, FLSDetail flsDetail)
    {
        try
        {
            int customerId = sessionKeys.PortfolioID;
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();
            var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).ToList();
            EmailFooter ef = new EmailFooter();
            AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select(customerId);
            ef = FooterEmail.EmailFooter_selectByID(int.Parse(ddlTypeofRequest.SelectedValue), customerId);
            var pdetails = FLSDetailsBAL.Jqgridlist(cid).FirstOrDefault();
            //PortfolioContact pc = ws.GetContactDetails(int.Parse(hcid.Value));

            var pc = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.ID == int.Parse(hcid.Value)).FirstOrDefault();

            List<FLSAdditionalInfo> flsAdditionalInfoList = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(cid);
            List<FLSCustomField> clist = CustomFormDesignerBAL.GetFieldList(sessionKeys.PortfolioID,0).ToList();
            string[] emails = pc.EmailAddress.Split(',');
            foreach (string res_email in emails)
            {
                Emailer em = new Emailer();
                string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSMail.htm");
                string subject = string.Empty;
                if (check_insert)
                {
                    subject = sessionKeys.JobDisplayName+" Reference: " + FLSDetailsBAL.GetCallIDByCustomer(cid).ToString() + " Confirmation of "+ sessionKeys.JobDisplayName;
                    body = body.Replace("[mail_head]", "Confirmation/Resolution of " + sessionKeys.JobDisplayName);
                }
                else
                {
                    subject = sessionKeys.JobDisplayName+" Reference: " + FLSDetailsBAL.GetCallIDByCustomer(cid).ToString() + " Status : " + ddlStatus.SelectedItem.Text + " Updated Details of "+ sessionKeys.JobDisplayName;
                    body = body.Replace("[mail_head]", "Updated Details of "+sessionKeys.JobDisplayName);
                }
                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                body = body.Replace("[Status]", ddlStatus.SelectedItem.Text);
                body = body.Replace("[user]", pdetails.RequesterName);
                body = body.Replace("[footer]", Server.HtmlDecode(ef == null ? string.Empty : string.Empty));//Server.HtmlDecode(ef == null ? string.Empty : ef.EmailFooter1));
                body = body.Replace("[adminemail]", ae == null ? string.Empty : ae.EmailAddress);
                body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", sessionKeys.JobDisplayName+" Reference:", "" + pdetails.CCID.ToString()));
                //sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Customer:", ddlCompany.SelectedItem.Text));
                if (GetFieldVisibility("Requesters Email Address", fieldList))
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Contact Email:", string.IsNullOrEmpty(res_email) ? string.Empty : res_email));
                if (GetFieldVisibility("Requesters Telephone No", fieldList))
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Contact Mobile:", string.IsNullOrEmpty(pc.ContactNumber) ? string.Empty : pc.ContactNumber));
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Address:", txtRequestersAddress.Text + ", " + txtRequestersCity.Text + ", " + txtRequestersTown.Text + ", " + txtRequestersPostcode.Text));
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Status:", ddlStatus.SelectedItem.Text));
                if (GetFieldVisibility("Subject", fieldList))
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Subject:", ddlSubject.SelectedItem.Text));
                if (GetFieldVisibility("details", fieldList))
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Details of the request:", txtDetails.Text));
                if (GetFieldVisibility("Priority", fieldList))
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Priority:", ddlPriority.SelectedItem.Text));
                if (GetFieldVisibility("Scheduled Date/Time", fieldList))
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Preferred Date / Time for Service 1:", flsDetail.ScheduledDate.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat()) ));
                if (flsDetail.Preferreddate2.HasValue)
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Preferred Date / Time for Service 2:", flsDetail.Preferreddate2.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat()) ));
                if (flsDetail.Preferreddate3.HasValue)
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Preferred Date / Time for Service 3:", flsDetail.Preferreddate3.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat()) ));

                if (GetFieldVisibility("Scheduled End Date/Time", fieldList))
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Scheduled End Date/Time:", flsDetail.ScheduledEndDateTime.Value.ToString(Deffinity.systemdefaults.GetDateTimeformat())));
               // if (GetFieldVisibility("Time Taken to Resolve", fieldList))
                    //    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Time Taken to Resolve:", flsDetail.TimeTakentoResolve.Value.ToString()));
                    if (GetFieldVisibility("Notes", fieldList))
                {
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Notes:", txtNotes.Text));
                }

               // string[] customer = Regex.Split(ccdCompany.SelectedValue, ":::");
                //int customerId1 = Convert.ToInt32(customer[0] == "" ? "0" : customer[0]);
               
                foreach (FLSCustomField c in clist)
                {
                    string val = flsAdditionalInfoList.Where(p => p.CustomFieldID == c.ID).Select(p => p.CustomFieldValue).FirstOrDefault();
                    string Lname = c.LabelName;
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", Lname, val));
                }
                body = body.Replace("[datarow]", sb.ToString());
                em.SendingMail(fromemailid, subject, body, res_email);
                //send mail to key contact
                if(hfkeyId.Value != null)
                {
                    var kc = PortfolioMgt.BAL.CustomerKeyContactsBAL.CustomerKeyContact_Select(Convert.ToInt32(hfkeyId.Value));
                    if(kc != null)
                    {
                        em.SendingMail(fromemailid, subject, body, kc.EmailAddress); 
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    #endregion


    #region FLS Custom form designer

    TextBox txt = null;
    DropDownList ddl = null;
    Label lbl = null;
    RadioButtonList rbn = null;
    CheckBox chk = null;
   
    int txtid = 1;
    int ddlid = 1;
    int lblid = 1;
    int rbtnid = 1;
    int chkid = 1;

    string[] typeOfFields = new string[] { "text box", "number field", "date", "text area", "url" };

    public void BindPlaceholderFields(int customerId)
    {
         int cid=0;
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
            if(Request.QueryString["callid"]!=null)
            {
                cid = int.Parse(Request.QueryString["callid"]);
            }
           
           
            List<FLSAdditionalInfo> flsAdditionalInfoList = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(cid);

            List<FLSCustomField> clist = CustomFormDesignerBAL.GetFieldList(customerId,0).ToList();
            Table tbl = new Table();
            tbl.EnableViewState = true;
            tbl.Style.Add("width", "100%");
            TableRow tr = new TableRow();
            TableCell td = null;
            int cnt = 0;
            int jcnt = 1;
            int totalCnt = clist.Count();
            if (clist.Count > 0)
            {
                //if (cid > 0)
                //{
                //pnlCustomFields.Visible = false;
                lblCustomFiledCustomer.Text = GetCompanyName(Convert.ToInt32(customerId));
                //}
                //else
                //{
                //    pnlCustomFields.Visible = false;
                //}
            }
            else
                pnlCustomFields.Visible = false;
           
            foreach (FLSCustomField c in clist)
            {
                string val = flsAdditionalInfoList.Where(p => p.CustomFieldID == c.ID).Select(p => p.CustomFieldValue).FirstOrDefault();
                string rval = string.Empty;
                if (val != null)
                    rval = val.ToString();

                if (tr == null)
                    tr = new TableRow();
              
                if (typeOfFields.Contains(c.TypeOfField.ToLower()))
                {
                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.LabelName));
                    td.Style.Add("width", "250px");
                    td.Style.Add("padding", "5px");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateTextbox(c.ID.ToString(), rval, val, Isfirsttime, c.TypeOfField.ToLower(), Convert.ToBoolean(c.Mandatory), c.LabelName, c.MinimumValue, c.MaximumValue,c.DefaultText));
                    if (c.TypeOfField.ToLower() == "date")
                    {
                        td.Controls.Add(GenerateCalendarImageButton(c.ID.ToString()));
                        
                    }
                    td.Style.Add("width", "390px");
                    td.Style.Add("padding", "5px");
                    tr.Cells.Add(td);
                   
                }

                else if (c.TypeOfField.ToLower() == "dropdown list")
                {
                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.LabelName));
                    td.Style.Add("width", "250px");
                    td.Style.Add("padding", "5px");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateDropDown(c.ListValue, c.ID.ToString(), rval, Isfirsttime, Convert.ToBoolean(c.Mandatory), c.LabelName));
                    td.Style.Add("width", "390px");
                    td.Style.Add("padding", "5px");
                    tr.Cells.Add(td);
                }
                else if (c.TypeOfField.ToLower() == "radio button")
                {
                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.LabelName));
                    td.Style.Add("width", "250px");
                    td.Style.Add("padding", "5px");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateRadioButton(c.ListValue, c.ID.ToString(), rval, Isfirsttime));
                    td.Style.Add("width", "390px");
                    td.Style.Add("padding", "5px");
                    tr.Cells.Add(td);
                }
                else if (c.TypeOfField.ToLower() == "checkbox")
                {
                    td = new TableCell();
                    td.Controls.Add(GenerateLable(c.LabelName));
                    td.Style.Add("width", "250px");
                    td.Style.Add("padding", "5px");
                    tr.Cells.Add(td);
                    td = new TableCell();
                    td.Controls.Add(GenerateCheckbox(c.ID.ToString(), rval, Isfirsttime));
                    td.Style.Add("width", "390px");
                    td.Style.Add("padding", "5px");
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
   // string validationGroup = "Custom";
    string validationGroup = "fls";
    public TextBox GenerateTextbox(string id, string setvalue, string val, bool Isfirsttime, string type, bool isMandatory, string labelName, string minValue, string maxValue, string defaultText)
    {
      
        txt = new TextBox();
        txt.ID = id;
        //txt.Width = 200;
        txt.SkinID = "txt_80";
        txt.Text = setvalue;
       
        if (type == "text area")
            txt.TextMode = TextBoxMode.MultiLine;
        txt.EnableViewState = true;

        if (val==null)
        {
            txt.Text = defaultText;
        }
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            txt.Text = setvalue;


        // when we create fls new form set defult text from admin
        if (Request.QueryString["callid"] == null)
        {
           
        }
        //Validator settings
        if (isMandatory)
        {
            RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
           
            requiredFieldValidator.ControlToValidate = txt.ID;
            requiredFieldValidator.ErrorMessage = "Please enter " + labelName.ToLower();
            requiredFieldValidator.SetFocusOnError = true;
            //rfv.Text = "*";
            requiredFieldValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.None;
            requiredFieldValidator.ValidationGroup = validationGroup;
          
            ph.Controls.Add(requiredFieldValidator);
        }
        if (type == "number field")
        {
            RangeValidator rangeValidator = new RangeValidator();
            rangeValidator.MinimumValue = minValue;
            rangeValidator.MaximumValue = maxValue;
            rangeValidator.ControlToValidate = txt.ID;
            rangeValidator.Type = ValidationDataType.Double;
            rangeValidator.SetFocusOnError = true;
            rangeValidator.ValidationGroup = validationGroup;
            rangeValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.None;
            rangeValidator.ErrorMessage = "The " + labelName.ToLower() + " must be " + rangeValidator.MinimumValue + " to " + rangeValidator.MaximumValue;
           
            ph.Controls.Add(rangeValidator);
        }
        if (type == "date")
        {
            txt.Width = 80;

            CalendarExtender calendarExtender = new CalendarExtender();
            calendarExtender.PopupButtonID = "imgDate" + id;
            calendarExtender.Format = Deffinity.systemdefaults.GetDateformat();
            calendarExtender.TargetControlID = txt.ID;
            calendarExtender.CssClass = "MyCalendar";
            ph.Controls.Add(calendarExtender);
             

            RegularExpressionValidator regularExpressionValidator = new RegularExpressionValidator();
            regularExpressionValidator.ControlToValidate = txt.ID;
            regularExpressionValidator.SetFocusOnError = true;
            regularExpressionValidator.Display = ValidatorDisplay.None;
            regularExpressionValidator.ErrorMessage = "Please enter a valid " + labelName.ToLower();
            regularExpressionValidator.ValidationExpression = "(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\\d\\d";
            regularExpressionValidator.ValidationGroup = validationGroup;
            ph.Controls.Add(regularExpressionValidator);
        }

        txtid = txtid + 1;
        return txt;
    }

    public Image GenerateCalendarImageButton(string id)
    {
        Image img = new Image();
        img.ID = "imgDate" + id;
        img.ImageAlign = ImageAlign.AbsMiddle;
        img.ImageUrl = "~/WF/DC/media/icon_calender.png";
        return img;
    }
  
    public DropDownList GenerateDropDown(string Items, string id, string setvalue, bool Isfirsttime, bool isMandatory,string labelName)
    {
        ddl = new DropDownList();
        ddl.ID = id;
        //ddl.Width = 200;
        ddl.SkinID = "ddl_80";
        string[] str = Regex.Split(Items, @"\s*,\s*");
       // The regex \s*,\s* can be read as: "match zero or more white space characters,
        //followed by a comma followed by zero or more white space characters".
       // http://stackoverflow.com/questions/1483645/what-is-the-cleanest-way-to-remove-all-extra-spaces-from-a-user-input-comma-deli
        System.Array.Sort(str);
        foreach (string s in str)
            ddl.Items.Add(s);
        ddl.Items.Insert(0, new ListItem("Please select...", "0"));
        ddl.EnableViewState = true;
        ddl.SelectedIndexChanged += ddl_SelectedIndexChanged;
        ddl.AutoPostBack = true;
        if (Isfirsttime && !string.IsNullOrEmpty(setvalue))
            ddl.SelectedValue = setvalue;

        //RequiredFieldValidator setting
        if (isMandatory)
        {
            RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
            requiredFieldValidator.ControlToValidate = ddl.ID;
            requiredFieldValidator.ErrorMessage = "Please select " + labelName.ToLower();
            requiredFieldValidator.SetFocusOnError = true;
            //rfv.Text = "*";
            requiredFieldValidator.Display = System.Web.UI.WebControls.ValidatorDisplay.None;
            requiredFieldValidator.ValidationGroup = validationGroup;
            requiredFieldValidator.InitialValue = "0";
            ddl.ValidationGroup = validationGroup;
            ph.Controls.Add(requiredFieldValidator);
        }

        ddlid = ddlid + 1;
        return ddl;
    }

    public RadioButtonList GenerateRadioButton(string Items, string id, string setvalue, bool Isfirsttime)
    {
        rbn = new RadioButtonList();
        rbn.ID = id;
        rbn.Width = 200;
        string[] str = Items.Split(',').ToArray();
        foreach (string s in str)
            rbn.Items.Add(s);
        rbn.EnableViewState = true;
        rbn.SelectedIndexChanged += rbn_SelectedIndexChanged;
        rbn.AutoPostBack = true;
        if (Isfirsttime && !string.IsNullOrEmpty(setvalue))
            rbn.SelectedValue = setvalue;
        rbtnid = rbtnid + 1;
        return rbn;

    }

    public CheckBox GenerateCheckbox(string id, string setvalue, bool Isfirsttime)
    {
        bool val;
        if (string.IsNullOrEmpty(setvalue))
        {
            val = false;
        }
        else
        {
            val = bool.Parse(setvalue);
        }
        chk = new CheckBox();
        chk.ID = id;
        //chk.Width = 200;
        chk.Checked = val;

        chk.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            chk.Checked = val;
        chkid = chkid + 1;
        return chk;
    }

    public void rbn_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dval = (RadioButtonList)sender;
        if (dval.SelectedIndex > 0)
        {
            string s = dval.SelectedValue;
        }
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

    FLSAdditionalInfo flsAdditionalInfo = null;

    public void SavePlaceholderData(int callId,int customerId)
    {
        try
        {
            ViewState["state"] = "2";
           
           
            List<FLSCustomField> clist = CustomFormDesignerBAL.GetFieldList(customerId,0).ToList();

            foreach (FLSCustomField c in clist)
            {
                flsAdditionalInfo = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(callId).Where(p => p.CustomFieldID == c.ID).FirstOrDefault();
                if (flsAdditionalInfo == null)
                {
                    flsAdditionalInfo = new FLSAdditionalInfo();
                    flsAdditionalInfo.CallID = callId;
                    if (typeOfFields.Contains(c.TypeOfField.ToLower()))
                    {
                        var txt = ph.FindControl(c.ID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            flsAdditionalInfo.CustomFieldValue = txt.Text;
                        }

                    }
                    else
                        if (c.TypeOfField.ToLower() == "dropdown list")
                        {
                            var ddl = ph.FindControl(c.ID.ToString()) as DropDownList;
                            if (ddl != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = ddl.SelectedValue;
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "checkbox")
                        {
                            var chk = ph.FindControl(c.ID.ToString()) as CheckBox;
                            if (chk != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = chk.Checked.ToString();
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "radio button")
                        {
                            var rdbtn = ph.FindControl(c.ID.ToString()) as RadioButtonList;
                            if (rdbtn != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = rdbtn.SelectedValue;
                            }
                        }
                    flsAdditionalInfo.CustomFieldID = c.ID;
                    FLSAdditionalInfoBAL.InsertFLSAdditionalInfo(flsAdditionalInfo);

                }
                else
                {

                    if (typeOfFields.Contains(c.TypeOfField.ToLower()))
                    {
                        var txt = ph.FindControl(c.ID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            flsAdditionalInfo.CustomFieldValue = txt.Text;
                        }

                    }
                    else
                        if (c.TypeOfField.ToLower() == "dropdown list")
                        {
                            var ddl = ph.FindControl(c.ID.ToString()) as DropDownList;
                            if (ddl != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = ddl.SelectedValue;
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "checkbox")
                        {
                            var chk = ph.FindControl(c.ID.ToString()) as CheckBox;
                            if (chk != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = chk.Checked.ToString();
                            }
                        }
                        else if (c.TypeOfField.ToLower() == "radio button")
                        {
                            var rdbtn = ph.FindControl(c.ID.ToString()) as RadioButtonList;
                            if (rdbtn != null)
                            {
                                flsAdditionalInfo.CustomFieldValue = rdbtn.SelectedValue;
                            }
                        }
                    FLSAdditionalInfoBAL.UpdateFLSAddtionalInfo(flsAdditionalInfo);
                }
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }

    }
   
    #endregion




    protected void imgCustomFieldUpdate_Click(object sender, EventArgs e)
    {
        // FLS custom fields data insert
        int id = Convert.ToInt32(Request.QueryString["callid"]);
        SavePlaceholderData(id, Convert.ToInt32(ddlCompany.SelectedValue));
    }

    private string GetCompanyName(int companyId)
    {
        using (PortfolioDataContext pd = new PortfolioDataContext())
        {
            var result = (from p in pd.ProjectPortfolios
                          where p.ID == companyId
                          select p.PortFolio).FirstOrDefault();
            return result;
        }
    }

    #region Visibility check
    public void FieldsCheck()
    {
        //FieldsVisibilityBAL fb = new FieldsVisibilityBAL();
        //var flist = fb.FieldsVisibility_Select();
        //if (flist != null)
        //{
        //    var bCost = flist.Where(p => p.FieldName == FieldsVisibilityBAL.CustomerCostCentre).Select(p => p.Visible).FirstOrDefault();
        //    var bPo = flist.Where(p => p.FieldName == FieldsVisibilityBAL.PONumber).Select(p => p.Visible).FirstOrDefault();
        //    pnlCostcode.Visible = bCost;
        //    pnlCostcodeLable.Visible = bCost;
        //    pnlPO.Visible = bPo;
        //    pnlPOlable.Visible = bPo;

        //}

    }
    #endregion

    #region Fields visibility check
    private void SetControlProperty(HtmlGenericControl HtmlDiv,HtmlGenericControl li_lbl,string position, HtmlGenericControl li_ddl_or_txt, bool isVisible, bool isMandatory, Label lbl, string instanceLableName, string type,string defaultValue,string Position, RequiredFieldValidator rfv = null,DropDownList ddl=null, CascadingDropDown ccd=null, TextBox txt =null)
    {
        if (!isVisible)
        {
            li_lbl.Style.Add("display", "none");
            li_ddl_or_txt.Style.Add("display", "none");
            if (rfv != null)
                rfv.Enabled = false;
        }
        else
        {
            HtmlDiv.Attributes.Add("class", position);
            HtmlDiv.Style.Add("width", "100%");
            li_ddl_or_txt.Style.Add("padding-top", "8px");
            if (Position == "Right")
            {
                HtmlDiv.Style.Add("float", "right");
                HtmlDiv.Style.Add("clear", "right");
            }
            else
            {
                HtmlDiv.Style.Add("float", "left");
                // li_ddl_or_txt.Style.Add("float", "right");
            }
            li_lbl.Style.Add("display", "block");
            li_ddl_or_txt.Style.Add("display", "block");
            lbl.Text = instanceLableName;
            if (Request.QueryString["callid"] == null && Request.QueryString["ticket"] == null)
            {
                if (txt != null)
                {
                    if (!string.IsNullOrEmpty(defaultValue))
                        txt.Text = defaultValue;
                }
                if (ccd != null)
                {
                    if (!string.IsNullOrEmpty(defaultValue))
                        ccd.SelectedValue = defaultValue;

                    //
                }
                if (ddl != null)
                {
                    if (!string.IsNullOrEmpty(defaultValue))
                        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(defaultValue));
                }
            }
            if (rfv != null)
            {
                rfv.Enabled = isMandatory;
                if (type == "txt")
                {
                    rfv.ErrorMessage = "Please enter " + instanceLableName;
                }
                else if (type == "ddl")
                {
                    rfv.ErrorMessage = "Please select " + instanceLableName;
                }
            }

        }
    }
    public void InitiallyinsertPosition()
    {
        try
        {
            using (DCDataContext db = new DCDataContext())
            {
                int Index = 1;
                var fieldsList = db.FLSFieldsConfigs.Where(a => a.CustomerID == sessionKeys.PortfolioID).ToList();

                if(fieldsList.Count == 0)
                {
                    FLSFieldsConfigBAL.InsertConfigData(sessionKeys.PortfolioID);
                    fieldsList = db.FLSFieldsConfigs.Where(a => a.CustomerID == sessionKeys.PortfolioID).ToList();
                }

                if (fieldsList.FirstOrDefault().Position == null)
                {
                    foreach (FLSFieldsConfig x in fieldsList)
                    {
                        x.Position = Index;
                        db.SubmitChanges();
                        Index = Index + 1;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void CheckFieldVisibility()
    {
        InitiallyinsertPosition();
        var fieldsList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).OrderBy(g => g.Position).ToList();
        lblFieldsCount.Text = fieldsList.Count.ToString();
        if (fieldsList.Count > 0)
        {
            DC.SRV.WebService wb = new DC.SRV.WebService();
            foreach (var item in fieldsList)
            {
                bool isVisible = Convert.ToBoolean(item.IsVisible);
                string instanceName = item.InstanceName;
                bool isMandatory = Convert.ToBoolean(item.IsMandatory);
                string defaultValue = item.DefaultValue;
                string Position = item.Position.HasValue ? item.Position.Value.ToString() : "0";

                if (item.DefaultName.ToLower().Contains("source of request"))
                {  
                    //ccdSourceOfRequest.DataBind();
                   
                    var result = wb.GetSourceOfRequest("", "").Where(s => s.name.ToLower() == defaultValue.ToLower()).FirstOrDefault();
                    defaultValue = result == null ? "" : result.value;
                    SetControlProperty(DivSourceOfRequest,li_lblSourceOfRequest,Position, li_ddlSourceOfRequest, false, isMandatory, lblSourceOfRequest, instanceName, "ddl", defaultValue,item.Alignment, rfvSourceOfRequest, ccd: ccdSourceOfRequest);
                   
                }
                else if (item.DefaultName.ToLower().Contains("company"))
                {
                   // SetControlProperty(li_lblCompany, li_ddlCompany, isVisible, isMandatory, lblCompany, instanceName, "ddl", defaultValue, rfvCompany);
                }
                else if (item.DefaultName.ToLower().Contains("requester name"))
                {
                    SetControlProperty(DivRequesterName, li_lblRequesterName, Position, li_ddlRequesterName, isVisible, isMandatory, lblRequesterName, instanceName, "ddl", defaultValue, item.Alignment, null);
                    
                }
                else if (item.DefaultName.ToLower().Contains("priority"))
                {
                    SetControlProperty(DivPriority, li_lblPriority, Position, li_ddlPriority, false, false, lblPriority, instanceName, "ddl", defaultValue, item.Alignment, rfvDllpriority);
                }
                else if (item.DefaultName.ToLower().Contains("requesters telephone no"))
                {
                    li_lblRequestersTelephoneNo.InnerText = "Co-ordinator’s Mobile Number";
                    SetControlProperty(DivRequesterTelePhoneNo,li_lblRequestersTelephoneNo,Position, li_txtRequestersTelephoneNo, isVisible, isMandatory, lblRequestersTelephoneNo, instanceName, "txt", defaultValue, item.Alignment);
                }
                else if (item.DefaultName.ToLower().Contains("requesters address"))
                {
                    SetControlProperty(DivRequesterAddress, li_lblRequestersAddress, Position, li_txtRequestersAddress, isVisible, isMandatory, lblRequestersAddress, instanceName, "txt", defaultValue, item.Alignment);
                }
                else if (item.DefaultName.ToLower().Contains("requesters city"))
                {
                    SetControlProperty(DivRequestersCity, li_lblRequestersCity, Position, li_txtRequestersCity, isVisible, isMandatory, lblRequestersCity, instanceName, "txt", defaultValue, item.Alignment);
                }
                else if (item.DefaultName.ToLower().Contains("requesters town"))
                {
                    SetControlProperty(DivRequestersTown, li_lblRequestersTown, Position, li_txtRequestersTown, isVisible, isMandatory, lblRequestersTown, instanceName, "txt", defaultValue, item.Alignment);
                }
                else if (item.DefaultName.ToLower().Contains("requesters postcode"))
                {
                    SetControlProperty(DivRequestersPostcode, li_lblRequestersPostcode, Position, li_txtRequestersPostcode, isVisible, isMandatory, lblRequestersPostcode, instanceName, "txt", defaultValue, item.Alignment);
                }
                else if (item.DefaultName.ToLower().Contains("requesters email address"))
                {
                    li_lblRequestersEmailAddress.InnerText = "Co-ordinator’s Email Address";
                    SetControlProperty(DivRequesterEmailAddress,li_lblRequestersEmailAddress,Position, li_txtRequestersEmailAddress, isVisible, isMandatory, lblRequestersEmailAddress, instanceName, "txt", defaultValue, item.Alignment);
                    
                }
                else if (item.DefaultName.ToLower().Contains("requesters department"))
                {
                    SetControlProperty(DivRequesterDepartment,li_lblRequestersDepartment,Position, li_txtRequestersDepartment, isVisible, isMandatory, lblRequestersDepartment, instanceName, "txt", defaultValue, item.Alignment);
                }
                else if (item.DefaultName.ToLower().Contains("requesters job title"))
                {
                    SetControlProperty(DivRequesterJobTitle,li_lblRequestersJobTitle,Position, li_txtRequestersJobTitle, isVisible, isMandatory, lblRequestersJobTitle, instanceName, "txt", defaultValue, item.Alignment);
                }
                else if (item.DefaultName.ToLower().Contains("subject"))
                {
                    var result = wb.GetSubject("", "").Where(s => s.name.ToLower() == defaultValue.ToLower()).FirstOrDefault();
                    defaultValue = result == null ? "" : result.value;
                    SetControlProperty(DivSubject,li_lblSubject,Position, li_ddlSubject, false, isMandatory, lblSubject, instanceName, "ddl", defaultValue, item.Alignment, rfvSubject, ccd: ccdSubject);
                }
                else if (item.DefaultName.ToLower().StartsWith("details"))
                {
                    lblDetails.Text = sessionKeys.JobDisplayName + " Details";
                    SetControlProperty(DivDetails, li_lblDetails, Position, li_txtDetails, isVisible, true, lblDetails, lblDetails.Text, "txt", defaultValue, item.Alignment, rfvDetails, txt: txtDetails);
                }
                else if (item.DefaultName.ToLower().EndsWith("site"))
                {
                    var result = wb.GetOurSite("", "").Where(s => s.name.ToLower() == defaultValue.ToLower()).FirstOrDefault();
                    defaultValue = result == null ? "" : result.value;
                    SetControlProperty(DivSite, li_lblSite, Position, li_ddlSite, false, false, lblSite, instanceName, "ddl", defaultValue, item.Alignment, rfvSite, ccd: ccdSite);
                    
                }
                else if (item.DefaultName.ToLower().Contains("type of request"))
                {
                    
                        lblTypeOfRequest.Text = "Type of "+sessionKeys.JobDisplayName;                  


                    SetControlProperty(DivTypeofRequest, li_lblTypeOfRequest, Position, li_ddlTypeOfRequest, isVisible, isMandatory, lblTypeOfRequest, lblTypeOfRequest.Text, "ddl", defaultValue, item.Alignment, rfvTypeOfRequest, ccd: ccdRequestType);
                   
                }
                else if (item.DefaultName.ToLower().Contains("status"))
                {
                    lblStatus.Text = sessionKeys.JobDisplayName + " Status";
                     var result = wb.GetStatusByTypeId("type:6;", "").Where(s => s.name.ToLower() == defaultValue.ToLower()).FirstOrDefault();
                    defaultValue = result == null ? "" : result.value;
                    SetControlProperty(DivStatus, li_lblStatus, Position, li_ddlStatus, isVisible, isMandatory, lblStatus, lblStatus.Text, "ddl", defaultValue, "Right", rfvStatus, ccd: ccdStatus);
                    // If status = No then set the ticket to default to "Closed" when raised 
                    if (!isVisible)
                    {
                        if ((Request.QueryString["callid"] == null))
                            ccdStatus.SelectedValue = "35"; // Status - closed
                    }
                }
                
                else if (item.DefaultName.ToLower().Contains("sub category"))
                {
                    SetControlProperty(DivSubCategory, li_lblSubCategory, Position, li_ddlSubCategory, isVisible, isMandatory, lblSubCategory, instanceName, "ddl", defaultValue, item.Alignment, rfvSubCategory);
                    //if(hsubid.Value != "0")
                    //{
                    //    ccdSubCategory.DataBind();
                    //    ccdSubCategory.SelectedValue = hsubid.Value;
                    //}
                }
                else if (item.DefaultName.ToLower().Contains("category"))
                {
                    SetControlProperty(DivCategory, li_lblCategory, Position, li_ddlCategory, isVisible, isMandatory, lblCategory, instanceName, "ddl", defaultValue, item.Alignment);

                }
                else if (item.DefaultName.ToLower().Contains("logged by"))
                {
                    SetControlProperty(DivLoggedBy, li_lblLoggedBy, Position, li_txtLoggedBy, isVisible, isMandatory, lblLoggedBy, instanceName, "txt", defaultValue, item.Alignment);
                   
                }
                else if (item.DefaultName.ToLower().Contains("logged date/time"))
                {
                    SetControlProperty(DivLoggedDatetime, li_lblLoggedDateTime, Position, li_txtLoggedDateTime, isVisible, isMandatory, lblLoggedDateTime, instanceName, "txt", defaultValue, item.Alignment);
                   
                }
                else if (item.DefaultName.ToLower().Contains("assigned to department"))
                {
                    var result = wb.GetAssignedtoDepartment("", "").Where(s => s.name.ToLower() == defaultValue.ToLower()).FirstOrDefault();
                    defaultValue = result == null ? "" : result.value;
                    //SetControlProperty(DivAssignedToDepartment, li_lblAssignedtoDepartment, Position, li_ddlAssignedtoDepartment, isVisible, isMandatory, lblAssignedtoDepartment, instanceName, "ddl", defaultValue, item.Alignment, rfvAssignedToDepartment, ccd: ccdAssignedDept);
                    SetControlProperty(DivAssignedToDepartment, li_lblAssignedtoDepartment, Position, li_ddlAssignedtoDepartment, false, false, lblAssignedtoDepartment, instanceName, "ddl", defaultValue, item.Alignment, null, ccd: ccdAssignedDept);
                }
                else if (item.DefaultName.ToLower().Contains("assigned technician"))
                {
                    SetControlProperty(DivAssignedTechnician, li_lblAssignedTechnician, Position, li_ddlAssignedTechnician, false, false, lblAssignedTechnician, instanceName, "ddl", defaultValue, item.Alignment, rfvAssignedToTechnician);
                    
                }
                else if (item.DefaultName.ToLower().Contains("scheduled date/time"))
                {
                    SetControlProperty(DivScheduledDateTime, li_lblScheduledDateTime, Position, li_txtScheduledDateTime, false, false, lblScheduledDateTime, instanceName, "txt", defaultValue, item.Alignment, rfvScheduledDate);
                    lblScheduledDateTime.Text = "Preferred Date / Time";
                }
                else if (item.DefaultName.ToLower().Contains("date and time started"))
                {
                    SetControlProperty(DivDateAndTimeStarted, li_lblDateAndTimeStarted, Position, li_txtDateAndTimeStarted, isVisible, isMandatory, lblDateAndTimeStarted, instanceName, "txt", defaultValue, item.Alignment);

                }
                else if (item.DefaultName.ToLower().Contains("date and time closed"))
                {
                    SetControlProperty(DivDateAndTimeClosed, li_lblDateAndTimeClosed, Position, li_txtDateAndTimeClosed, isVisible, isMandatory, lblDateAndTimeClosed, instanceName, "txt", defaultValue, item.Alignment);
                }

                else if (item.DefaultName.ToLower().Contains("customer ref"))
                {

                    SetControlProperty(DivCustomerRef, li_lblCustomerRef, Position, li_txtCustomerRef, isVisible, isMandatory, lblCustomerRef, instanceName, "txt", defaultValue, item.Alignment, rfvCustomerRef, txt: txtCustomerRef);
                }
                else if (item.DefaultName.ToLower().Contains("po number"))
                {
                    SetControlProperty(DivPONumber, li_lblPONumber, Position, li_txtPONumber, isVisible, isMandatory, lblPONumber, instanceName, "txt", defaultValue, item.Alignment, rfvPONumber, txt: txtPOnumber);
                   
                }
                else if (item.DefaultName.ToLower().Contains("notes"))
                {
                    SetControlProperty(DivNotes, li_lblNotes, Position, li_txtNotes, isVisible, isMandatory, lblNotes, instanceName, "txt", defaultValue, item.Alignment, rfvNotes, txt: txtNotes);
                   
                }
               
                else if (item.DefaultName.ToLower().Contains("time accumulated"))
                {
                    SetControlProperty(DivTimeAccumulated, li_lblTimeAccumulated, Position, li_txtTimeAccumulated, isVisible, isMandatory, lblTimeAccumulated, instanceName, "txt", defaultValue, item.Alignment);

                }
                else if (item.DefaultName.ToLower().Contains("time worked"))
                {
                    SetControlProperty(DivTimeWorked, li_lblTimeWorked, Position, li_txtTimeWorked, isVisible, isMandatory, lblTimeWorked, instanceName, "txt", defaultValue, item.Alignment, rfvTimeWorked);
                    
                }
                else if (item.DefaultName.ToLower().Contains("customer cost code"))
                {
                    isVisible = true;
                    instanceName = "Repair Cost";
                    SetControlProperty(DivCustomerCostCode, li_lblCustomerCostCode, Position, li_txtCustomerCostCode, isVisible, isMandatory, lblCustomerCostCode, instanceName, "txt", defaultValue, item.Alignment, rfvCustomerCostCode, txt: txtCustomerCostCode);
                }
                else if (item.DefaultName.ToLower().Contains("scheduled end date/time"))
                {
                    SetControlProperty(DivScheduledendDateTime, li_lblScheduledendDateTime, Position, li_txtScheduledendDateTime, false, false, lblScheduledEndDateTime, instanceName, "txt", defaultValue, item.Alignment, txt: txtScheduledEndDateTime);
                }
                else if (item.DefaultName.ToLower().Contains("time taken to resolve"))
                {
                    SetControlProperty(DivTimeTakentoResolve, li_lblTimeTakentoResolve, Position, li_txtTimeTakentoResolve, isVisible, isMandatory, lblTimeTakentoResolve, instanceName, "txt", defaultValue, item.Alignment, txt: txtTimeTakentoResolve);
                } 
                else if (item.DefaultName.ToLower().Contains("resolution date/time"))
                {
                    SetControlProperty(DivResolutionDateandTime, li_ResolutionDateandTime, Position, li_txtResolutionDateandTime, isVisible, isMandatory, lblResolutionDateandTime, instanceName, "txt", defaultValue, item.Alignment, txt: txtResolutionDateandTime);
                }
                else if (item.DefaultName.ToLower().Contains("site details"))
                {
                    SetControlProperty(DivSiteDetails, li_lblSitedetails, Position, li_txtSitedetails, false, false, lblSitedetails, instanceName, "txt", defaultValue, item.Alignment, txt: txtSitedetails);
                }
                else if (item.DefaultName.ToLower().Contains("preferred date/time 2"))
                {
                    SetControlProperty(DivPreferreddate2, li_lblPreferreddate2, Position, li_txtPreferreddate2, isVisible, isMandatory, lblPreferreddate2, instanceName, "txt", defaultValue, item.Alignment, txt: txtPreferreddate2);
                }
                else if (item.DefaultName.ToLower().Contains("preferred date/time 3"))
                {
                    SetControlProperty(DivPreferreddate3, li_lblPreferreddate3, Position, li_txtPreferreddate3, isVisible, isMandatory, lblPreferreddate3, instanceName, "txt", defaultValue, item.Alignment, txt: txtPreferreddate3);
                }
                else if (item.DefaultName.ToLower().Contains("ticket manager"))
                {
                    SetControlProperty(DivTicketManager, li_lblTicketManager, Position, li_txtTicketManager, isVisible, isMandatory,lblTicketManager, instanceName, "ddl", defaultValue, item.Alignment, ddl: ddlTicketManager);
                }
                else 
                {

                }
            }
        }
       

    }
    #endregion



    

    private void ClearConfigFields()
    {
        ccdStatus.DataBind();
        ccdStatus.SelectedValue = "22"; //22 for Status "New"
        setDefaultSite();
        ccdRequestType.SelectedValue = "";
        ccdCategory.SelectedValue = "";
        ccdSubject.SelectedValue = "";
        ccdAssignedDept.SelectedValue = "";
        ccdSourceOfRequest.SelectedValue = "";
    }
   


    public void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hfCustomerID.Value) != Convert.ToInt32(ddlCompany.SelectedValue == "" ? "0" : ddlCompany.SelectedValue))
        {
            sessionKeys.PortfolioID = Convert.ToInt32(ddlCompany.SelectedValue == "" ? "0" : ddlCompany.SelectedValue);
            hfCustomerID.Value = ddlCompany.SelectedValue == "" ? "0" : ddlCompany.SelectedValue;
            FLSFieldsConfigBAL.InsertConfigData(sessionKeys.PortfolioID);
            ClearConfigFields();
            CheckFieldVisibility();
            Bindhealthcheck();
        }

    }

    protected void btnCreateNewTicket_Click(object sender, EventArgs e)
    {
        int ticketId = Convert.ToInt32(Request.QueryString["callid"]);
        BindPriorityDdl();
        if (ticketId > 0)
        {
            Response.Redirect("~/WF/DC/FLSForm.aspx?ticket=" + ticketId);
        }
      
    }


    private void CreateNewTicket(int ticketId)
    {

        var cd = CallDetailsBAL.SelectbyId(ticketId);
        var fd = FLSDetailsBAL.SelectbyId(ticketId);
        if (cd != null && fd != null)
        {

            ccdType.SelectedValue = Convert.ToString(cd.RequestTypeID);
            ccdStatus.SelectedValue = Convert.ToString(cd.StatusID);
            ccdCompany.DataBind();
            ccdCompany.SelectedValue = Convert.ToString(cd.CompanyID);
            sessionKeys.PortfolioID = Convert.ToInt32(cd.CompanyID);
            ddlCompany.SelectedValue = Convert.ToString(cd.CompanyID);
            ccdSourceOfRequest.DataBind();
            ccdSourceOfRequest.SelectedValue = Convert.ToString(fd.SourceOfRequestID.HasValue ? fd.SourceOfRequestID.ToString() : "0");
           
            //ccdName.DataBind();
            //BindRequesters();
            hcid.Value = Convert.ToString(cd.RequesterID);
            ccdSite.DataBind();
            ccdSite.SelectedValue = Convert.ToString(cd.SiteID);
            txtLoggedName.Text = sessionKeys.UName;
            txtCreatedDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
            txtCreatedTime.Text = string.Format(Deffinity.systemdefaults.GetStringTimeformat(), DateTime.Now);
            txtStartedDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
            txtStartedTime.Text = string.Format(Deffinity.systemdefaults.GetStringTimeformat(), DateTime.Now);
            txtSeheduledDateTime.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
            txtScheduledTime.Text = string.Format(Deffinity.systemdefaults.GetStringTimeformat(), DateTime.Now);
            ccdSubject.DataBind();
            ccdSubject.SelectedValue = Convert.ToString(fd.SubjectID);
            txtDetails.Text = fd.Details;
            ccdAssignedDept.SelectedValue = Convert.ToString(fd.DepartmentID);
            ccdAssignedName.SelectedValue = Convert.ToString(fd.UserID);
            ccdRequestType.DataBind();
            ccdRequestType.SelectedValue = fd.RequestType.HasValue ? fd.RequestType.ToString() : "0";
            ccdCategory.DataBind();
            ccdCategory.SelectedValue = Convert.ToString(fd.CategoryID);
            ccdSubCategory.DataBind();
            ccdSubCategory.SelectedValue = fd.SubCategoryID.HasValue ? fd.SubCategoryID.ToString() : "0";
            BindPriorityDdl();
            ddlPriority.SelectedValue = fd.PriorityId.HasValue ? fd.PriorityId.ToString() : "0";
        }
       
    }
    //protected void ImgConfig_Click(object sender, EventArgs e)
    //{
    //    hfId.Value = ddlCategory.SelectedValue;
    //    if (ddlTypeofRequest.SelectedValue != "")
    //    {
    //        popIssues.Show();
    //    }
    //}
    //protected void BtnsubCategoryEdit_Click(object sender, EventArgs e)
    //{
    //    if (ddlTypeofRequest.SelectedValue != "")
    //    {
    //        if (ddlCategory.SelectedValue != "")
    //        {
    //            ModalPopupExtender1.Show();
    //        }
    //        else
    //        {
    //            lblMsg.Text = "Please select categoty";
    //        }
    //    }
    //    else
    //    {
    //        lblMsg.Text = "Please select type of request";
    //    }
    //}
    //protected void ImageButton2_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        SubCategory subCategory = new SubCategory();
    //        subCategory.Name = txtsubcategory.Text.Trim();
    //        subCategory.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
    //        bool exists = SubCategoryBAL.CheckSubCategory(txtsubcategory.Text.Trim(), Convert.ToInt32(subCategory.CategoryID));
    //        if (!exists)
    //        {
    //            SubCategoryBAL.AddSubCategory(subCategory);
    //            lblerror2.Text = "Sub Category added successfully";
    //            ModalPopupExtender1.Hide();
    //            //lblerror2.ForeColor = System.Drawing.Color.Green;
    //            txtsubcategory.Text = string.Empty;
    //            upanelMain.Update();
    //            //Update the sub category
    //            ccdSubCategory.SelectedValue = subCategory.ID.ToString();
    //        }
    //        else
    //        {
    //            lblerror2.Text = "Sub Category already exists";
    //            //lblerror2.ForeColor = System.Drawing.Color.Red;
    //            txtsubcategory.Text = string.Empty;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    //protected void ImageButton4_Click(object sender, EventArgs e)
    //{
    //    if (ddlCompany.SelectedValue != "")
    //    {
    //        ModalPopupExtender2.Show();
    //    }
    //    else
    //    {
    //        lblMsg.Text = "Please select customer";
    //    }
    //}
    //protected void btnaddnewsubject_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int customerID = sessionKeys.PortfolioID;
    //        Subject subject = new Subject();
    //        subject.SubjectName = TxtaddSubjectP.Text.Trim();
    //        subject.CustomerID = customerID;
    //        bool exists = FLSSubject.CheckExists(TxtaddSubjectP.Text.Trim(), customerID);
    //        if (!exists)
    //        {
    //            FLSSubject.Add(subject);
    //            Lblerror3.Text = "Subject name added successfully";
    //            ModalPopupExtender2.Hide();
    //            //Lblerror3.ForeColor = System.Drawing.Color.Green;
    //            TxtaddSubjectP.Text = string.Empty;
    //            //subject
    //            ccdSubject.DataBind();
    //            ccdSubject.SelectedValue = subject.ID.ToString();
    //            //update panel
    //            upanelMain.Update();
    //        }
    //        else
    //        {
    //            Lblerror3.Text = "Subject name already exists";
    //            //Lblerror3.ForeColor = System.Drawing.Color.Red;
    //        }
    //    }
    //    catch(Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
       
    }
    public void SettingTooltip()
    {
        try
        {
            if (ddlPriority.SelectedValue != "0")
            {
                using (DCDataContext DCcontext = new DCDataContext())
                {
                    var x = DCcontext.PriorityLevels.Where(a => a.Id == int.Parse(ddlPriority.SelectedValue)).Select(a => a.Description).FirstOrDefault();
                    imgPriority.ToolTip = Convert.ToString(x);
                }
            }
            else
            {
                imgPriority.ToolTip = "";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlPriority_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            SettingTooltip();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //protected void BtnReqSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int PortfolioID = sessionKeys.PortfolioID;
    //        string Name = txtRequestersName.Text.Trim();
    //        string Email = txtEmailAddress.Text.Trim();
    //        string Telephone = TxtTelephone.Text.Trim();
    //        string Mobile = txtMobile.Text.Trim();

    //        using (PortfolioDataContext Pdc = new PortfolioDataContext())
    //        {
    //            var Checking=Pdc.PortfolioContacts.Where(a => a.PortfolioID == PortfolioID && a.Email == Email).ToList();
    //            if (Checking.Count == 0)
    //            {
    //                int ContactID =0;
    //                PortfolioContact fc = new PortfolioContact();
    //                fc.PortfolioID = PortfolioID;
    //                fc.Name = Name;
    //                fc.Email = Email;
    //                fc.Telephone = Telephone;
    //                fc.Mobile = Mobile;
    //                Pdc.PortfolioContacts.InsertOnSubmit(fc);
    //                Pdc.SubmitChanges();

    //                //session
    //               // sessionKeys.RInsertedId = fc.ID.ToString();

    //                if (ChkAccesstoPortal.Checked == true)
    //                {
    //                    ContactID = Convert.ToInt32(fc.ID);
    //                    ContractorsAndAssociateInsert(ContactID);
    //                }
    //                lblReqPanelMsg.ForeColor = System.Drawing.Color.Green;
    //                lblReqPanelMsg.Text = "Requester added successfully";
    //                lblReqPanelMsg.Visible = false;
    //                ClaerFields();
    //                //BindRequesters();
    //                hcid.Value = fc.ID.ToString();
    //                //ccdName.SelectedValue = fc.ID.ToString();
    //                upanelMain.Update();
    //               // Response.Redirect(Request.RawUrl, false);
    //                //ccdName.DataBind();
    //                //ccdName.SelectedValue = ContactID.ToString();
    //            }
    //            else
    //            {
    //                //txtEmailAddress.Text = string.Empty;
    //                lblReqPanelMsg.ForeColor = System.Drawing.Color.Red;
    //                lblReqPanelMsg.Text = "Sorry but the email address you have entered already exists for a contact belonging to this customer. Please check the existing contacts for this customer.";
    //                ModalPopupExtender3.Show();
    //            }
    //        }
           
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    public string GetUserName(List<UserMgt.Entity.Contractor> userlist, string UserName)
    {
        string retVal = string.Empty;
        bool checkUserExist = false;
        int i = 1;
        while (!checkUserExist)
        {
            int cnt = userlist.Where(p => p.LoginName == UserName).Count();
            if (cnt > 0)
            {

                UserName = UserName + i.ToString();
                retVal = UserName;
                checkUserExist = false;
                i++;
            }
            else
            {
                retVal = UserName;
                checkUserExist = true;
            }
        }



        return retVal;
    }
    public void ContractorsAndAssociateInsert(int contactid)
    {
        try
        {
            using (UserDataContext ud = new UserDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    var pcontact = pd.PortfolioContacts.Where(o => o.ID == contactid).FirstOrDefault();
                    string name = pcontact.Name;
                    string email = pcontact.Email;
                    string contactNo = pcontact.Telephone;
                    var contactUsers = pd.PortfolioContactAssociates.Where(o => o.ContactID == contactid).FirstOrDefault();
                    if (contactUsers == null)
                    {
                        UserMgt.Entity.Contractor cont = new UserMgt.Entity.Contractor();
                        string[] loginname = name.Split(' ');
                        string userName = string.Empty;
                        string password = string.Empty;

                        if (loginname.Length > 1)
                        {
                            if (loginname[0].Length > 1)
                                userName = cont.LoginName = loginname[0].Remove(1).ToLower() + loginname[1].ToLower();
                            else
                                userName = cont.LoginName = loginname[0].ToLower() + loginname[1].ToLower();
                        }
                        else
                        {
                            userName = cont.LoginName = loginname[0].Remove(1).ToLower() + loginname[0].ToLower();
                        }
                        //Check the user name is exists 
                        //if exists get new name
                        cont.LoginName = GetUserName(ud.Contractors.Select(p => p).ToList(), userName);
                        password = Membership.GeneratePassword(8, 0);
                        cont.Password = Deffinity.Users.Login.GeneratePasswordString(password);// FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                        cont.ContractorName = name;
                        cont.EmailAddress = email;
                        var cno = contactNo;
                        if (cno.Length > 20)
                            cno = contactNo.Substring(0, 20);
                        cont.ContactNumber = cno;
                        cont.Status = "Active";
                        cont.CreatedDate = DateTime.Now;
                        cont.ModifiedDate = DateTime.Now;
                        //customer user type
                        cont.SID = 7;
                        cont.ResetPassword = false;
                        cont.IsImage = false;

                        ud.Contractors.InsertOnSubmit(cont);
                        ud.SubmitChanges();

                        AssignedCustomerToPortfolio actp = pd.AssignedCustomerToPortfolios.Where(a => a.Portfolio == sessionKeys.PortfolioID && a.CustomerID == cont.ID).Select(a => a).FirstOrDefault();
                        if (actp == null)
                        {
                            AssignedCustomerToPortfolio acp = new AssignedCustomerToPortfolio();
                            acp.Portfolio = sessionKeys.PortfolioID;
                            acp.CustomerID = cont.ID;
                            pd.AssignedCustomerToPortfolios.InsertOnSubmit(acp);
                            pd.SubmitChanges();
                        }
                        contactUsers = new PortfolioContactAssociate();
                        contactUsers.ContactID = contactid;
                        contactUsers.CustomerUserID = cont.ID;
                        pd.PortfolioContactAssociates.InsertOnSubmit(contactUsers);
                        pd.SubmitChanges();
                        //Add customer user to Assoicate Contact table
                        // DC.BLL.CustomerDetailsBAL.PortfolioContactAssociate_Insert(cont.ID, sessionKeys.PortfolioID);


                        //Mail to New Contractors
                        LoginDetailsMail(cont.ContractorName, cont.LoginName, password, cont.EmailAddress);
                        //enable login to portal
                        pcontact.LogintoPortal = true;
                        pd.SubmitChanges();


                    }
                    else
                    {

                        //check portfolio associate is working
                        AssignedCustomerToPortfolio actp = pd.AssignedCustomerToPortfolios.Where(a => a.Portfolio == sessionKeys.PortfolioID && a.CustomerID == contactid).Select(a => a).FirstOrDefault();
                        if (actp == null)
                        {
                            AssignedCustomerToPortfolio acp = new AssignedCustomerToPortfolio();
                            acp.Portfolio = sessionKeys.PortfolioID;
                            acp.CustomerID = contactid;
                            pd.AssignedCustomerToPortfolios.InsertOnSubmit(acp);
                            pd.SubmitChanges();
                        }
                        // IF InActive customer User is ther make active
                        UserMgt.Entity.Contractor Contractor_update = ud.Contractors.Where(c => c.ID == contactid).FirstOrDefault();
                        if (Contractor_update != null)
                        {
                            if (Contractor_update.Status == "InActive")
                            {
                                Contractor_update.Status = "Active";
                                ud.SubmitChanges();
                            }
                        }
                        //Add customer user to Assoicate Contact table
                        //DC.BLL.CustomerDetailsBAL.PortfolioContactAssociate_Insert(contactid, sessionKeys.PortfolioID);
                    }
                }
            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void LoginDetailsMail(string name, string uname, string password, string toEmail)
    {
        try
        {
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            string subject = "Login Details";
            Emailer em = new Emailer();
            string body = em.ReadFile("~/WF/DC/EmailTemplates/ContractorWelcomeMail.htm");
            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            body = body.Replace("[user]", name);
            body = body.Replace("[username]", uname);
            body = body.Replace("[password]", password);
            body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
            em.SendingMail(fromemailid, subject, body, toEmail);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //protected void ImageButton7_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ClaerFields();
    //        ModalPopupExtender3.Hide();
    //        //Response.Redirect(Request.RawUrl);
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    //public void ClaerFields()
    //{
    //    txtRequestersName.Text = string.Empty;
    //    txtEmailAddress.Text = string.Empty;
    //    txtMobile.Text = string.Empty;
    //    TxtTelephone.Text = string.Empty;
    //    ChkAccesstoPortal.Checked = false;
    //}
    public bool ContainsNew(string ProjectId, string[] Values)
    {
        bool ReturnValue = true;
        try
        {
            foreach (string s in Values)
            {
                if (s == ProjectId)
                {
                    ReturnValue = true;
                    break;
                }
                else
                {
                    ReturnValue = false;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ReturnValue;
    }
    //public void BindAssetsWithCantactId(int ReqId,int CallId=0)
    //{
    //    try
    //    {
    //        using (AssetsToSoftwareDataContext asset = new AssetsToSoftwareDataContext())
    //        {
    //            if (QueryStringValues.CallID > 0)
    //            {
    //                using (DCDataContext Dc = new DCDataContext())
    //                {
    //                    var CallIdAssociatedProductsList = Dc.CallIdAssociatedProducts.Where(a => a.Callid == QueryStringValues.CallID).FirstOrDefault();
    //                    if (CallIdAssociatedProductsList != null)
    //                    {
    //                        var Asslist = asset.V_Assets.Where(c => c.ID == int.Parse(CallIdAssociatedProductsList.ProductIds)).ToList();
    //                        GridProducts.DataSource = Asslist;
    //                        GridProducts.DataBind();
    //                    }
                       
    //                }
    //            }
    //            //the requester assets
    //            else if (ReqId > 0)
    //            {
    //                var ContactAssets = asset.AssetAssociatedToContacts.Where(a => a.ContactId == ReqId).ToList();
    //                if (ContactAssets != null)
    //                {
    //                    var Asslist = asset.V_Assets.Where(o => ContactAssets.Select(a => a.AssetId).ToArray().Contains(o.ID)).ToList();
    //                    GridProducts.DataSource = Asslist;
    //                    GridProducts.DataBind();
    //                }
    //            }
               
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
     public static string GetDatesDifferenceInDays(string StartDate,string EndDate)
    {
        double Nodays = 0;
        try
        {
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                DateTime dstart = DateTime.Parse(StartDate);
                DateTime dEnd = DateTime.Parse(EndDate);

                Nodays = (dEnd - dstart).TotalDays;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Nodays.ToString();
    }
    public static string GetImageUrl(int a_gId, ImageManager.ThumbnailSize? a_oThumbSize)
    {
        //return GetImageUrl(a_gId, a_oThumbSize, true);

        ImageManager.ImageType eImageType = ImageManager.ImageType.OriginalData;
        if (a_oThumbSize.HasValue)
        {
            switch (a_oThumbSize.Value)
            {
                case ImageManager.ThumbnailSize.MediumSmaller: eImageType = ImageManager.ImageType.ThumbNails; break;
            }
        }
        else
        {
            eImageType = ImageManager.ImageType.OriginalData;
        }

        string path = "~/WF/UploadData/Assets/" + eImageType.ToString() + "/asset_" + a_gId.ToString() + ".png";
         if (!File.Exists(HttpContext.Current.Server.MapPath(path)))
        {
            path = "~/WF/UploadData/Assets/" + eImageType.ToString() + "/asset_0.png?t=" + DateTime.Now.Second.ToString();
        }
        return path;

        // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

    }
    public bool CheckImageVisibility(int a_guid)
    {
        bool _visible = false;
        if (a_guid.ToString() != "00000000-0000-0000-0000-000000000000")
        {
            _visible = true;
        }
        return _visible;
    }

    public static bool AssetShowVisibility()
    {
        bool ReturnValue = true;
        try
        {
            if (HttpContext.Current.Request.QueryString["callid"] != null)
            {
                int RCallid = int.Parse(HttpContext.Current.Request.QueryString["callid"].ToString());
                using (DCDataContext dc = new DCDataContext())
                {
                    var call_de = dc.CallDetails.Where(a => a.ID == RCallid).FirstOrDefault();
                    if (call_de.StatusID == 22)//checking status new
                    {
                        ReturnValue = true;
                    }
                    else
                    {
                        ReturnValue = false;
                    }
                }
            }
            else
            {
                ReturnValue = false;
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ReturnValue;
    }
    public static bool CheckBoxSelecting(string callid,string ProductId)
    {
        bool ReturnValue = true;
        try
        {
            if (int.Parse(callid) > 0)
            {
                using (DCDataContext dc = new DCDataContext())
                {

                    CallIdAssociatedProduct callidProductIds =
                        dc.CallIdAssociatedProducts.Where(c => c.Callid == int.Parse(callid)).FirstOrDefault();
                    if (callidProductIds != null && !string.IsNullOrEmpty(callidProductIds.ProductIds))
                    {
                        foreach (string s in callidProductIds.ProductIds.Split(','))
                        {
                            if (s == ProductId)
                            {
                                ReturnValue = true;
                                break;
                            }
                            else
                            {
                                ReturnValue = false;
                            }
                        }
                    }
                    else
                    {
                        ReturnValue = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ReturnValue;
    }

    //public void BindAssets(string SearchText = null)
    //{
    //    try
    //    {
    //        using (PortfolioDataContext Pdc = new PortfolioDataContext())
    //        {
    //            var GridAssetsListResult = (from a in Pdc.PortfolioContacts
    //                                        where a.PortfolioID == sessionKeys.PortfolioID
    //                                        select new
    //                                        {
    //                                            Id = a.ID,
    //                                            Name = a.Name,
    //                                            a.Telephone,
    //                                            a.Address1,
    //                                            a.Postcode,
    //                                            a.Email,
    //                                            a.Town,
    //                                            a.City
    //                                        }).ToList();
    //            if (!string.IsNullOrEmpty(SearchText))
    //            {
    //                GridAssetsListResult = (from g in GridAssetsListResult
    //                                        where ((g.Name != null? g.Name.ToLower().Contains(SearchText.ToLower()):false)||
    //                                        (g.Email != null?g.Email.ToLower().Contains(SearchText.ToLower()):false)||
    //                                        (g.Telephone != null?g.Telephone.ToLower().Contains(SearchText.ToLower()):false)||
    //                                        (g.Address1 != null?g.Address1.ToLower().Contains(SearchText.ToLower()):false)||
    //                                        (g.Postcode!= null?g.Postcode.ToLower().Contains(SearchText.ToLower()):false)
    //                                        )
    //                                        select g).ToList();
    //            }
    //            GridRequesterList.DataSource = GridAssetsListResult;
    //            GridRequesterList.DataBind();              
    //            //txtAssetName.Text = string.Empty;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    public bool ContainsTextCheck(string sText,string[] s1)
    {
        bool ReturnValue = true;
        try
        {
            foreach (string s in s1)
            {
                if (s != string.Empty && !string.IsNullOrEmpty(sText))
                {
                    if (sText.ToLower().Contains(s.ToLower()))
                    {
                        ReturnValue = true;
                        break;
                    }
                    else
                    {
                        ReturnValue = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ReturnValue;
    }
    //protected void lnkBtnShowAssets_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ModalPopupExtender4.Show();
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}

    //protected void BtnSearchAsset_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //BindAssets(txtAssetName.Text.Trim().ToLower());
    //        ModalPopupExtender4.Show();
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    public void Insert_CallIdAssociatedProduct(string ProductIds, int R_callid)
    {
        try
        {
            using (DCDataContext dc = new DCDataContext())
            {
                CallIdAssociatedProduct callId_Product = dc.CallIdAssociatedProducts.Where(c => c.Callid == R_callid).FirstOrDefault();
                if (callId_Product == null)
                {
                    CallIdAssociatedProduct callId_ProductNew = new CallIdAssociatedProduct();
                    callId_ProductNew.Callid = R_callid;
                    callId_ProductNew.ProductIds = ProductIds;
                    dc.CallIdAssociatedProducts.InsertOnSubmit(callId_ProductNew);
                    dc.SubmitChanges();
                    //lblInAssetPopUp.ForeColor = System.Drawing.Color.Green;
                    //lblInAssetPopUp.Text = "Inserted successfully.";
                }
                else
                {
                    callId_Product.ProductIds = ProductIds;
                    dc.SubmitChanges();
                    //lblInAssetPopUp.ForeColor = System.Drawing.Color.Green;
                    //lblInAssetPopUp.Text = "Updated successfully.";
                }//}
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

        //protected void LnkBtnCancel_Click(object sender, EventArgs e)
        //{
        //    ModalPopupExtender4.Hide();
        //    Response.Redirect(Request.RawUrl);
        //}
        public string CheckBoxCheckedOrNotInGrid(GridView G)
    {
        string ProductIds = string.Empty;
        try
        {
            foreach (GridViewRow gvr in G.Rows)
            {
                if (((CheckBox)gvr.FindControl("CheckSelection")).Checked == true)
                {
                    Label l11 = (Label)(gvr.FindControl("lblId"));
                    if (ProductIds == string.Empty && l11.Text != string.Empty)
                    {
                        ProductIds = l11.Text;
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ProductIds;
    }
    public string CheckBoxCheckedOrNotInProductGrid()
    {
        string ProductIds = string.Empty;
        try
        {
            ProductIds = hpid.Value;
            //foreach (GridViewRow gvr in GridView1.Rows)
            //{
            //    if (((CheckBox)gvr.FindControl("CheckSelection")).Checked == true)
            //    {
            //        Label l11 = (Label)(gvr.FindControl("lblId"));
            //        if (ProductIds == string.Empty && l11.Text != string.Empty)
            //        {
            //            ProductIds = l11.Text;
            //            //break;
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ProductIds;
    }
    public void SaveProductToCallid(string ProductIds, int R_callid)
    {
        try
        {
            Insert_CallIdAssociatedProduct(ProductIds, R_callid);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    //protected void BtnSaveGridValues_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string ProductIds = CheckBoxCheckedOrNotInGrid(GridRequesterList);
    //        if (!string.IsNullOrEmpty(ProductIds))
    //        {
    //            //ccdName.DataBind();
    //            //ccdName.SelectedValue = ProductIds;
    //            //BindAssetsWithCantactId(int.Parse(ProductIds));
    //            ModalPopupExtender4.Hide();

    //            //foreach (GridViewRow gvr in GridRequesterList.Rows)
    //            //{
    //            //    if (((CheckBox)gvr.FindControl("CheckSelection")).Checked == true)
    //            //    {
    //            //        ((CheckBox)gvr.FindControl("CheckSelection")).Checked = false;
    //            //    }
    //            //}
    //        }
    //        else
    //        {
    //            lblInAssetPopUp.ForeColor = System.Drawing.Color.Red;
    //            lblInAssetPopUp.Text = "Please select one checkbox.";
    //            ModalPopupExtender4.Show();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}

    //protected void GridAssetsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridRequesterList.PageIndex = e.NewPageIndex;
    //    GridRequesterList.DataBind();
    //    UpdatePnlAssetsList.Update();
    //}

    //protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //if (!string.IsNullOrEmpty(ddlName.SelectedValue))
    //        //{
    //        //    BindAssetsWithCantactId(int.Parse(ddlName.SelectedValue));
    //        //}
    //        //else
    //        //{
    //        //    BindAssetsWithCantactId(0);
    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}

    protected void btnRescheduleVisit_Click(object sender, EventArgs e)
    {
        try
        {
            //RFVResolution.Enabled = false;

            IDCRespository<DC.Entity.FLSDetail> frep = new DCRepository<DC.Entity.FLSDetail>();
            //IDCRespository<DC.Entity.FLSDetailsJournal> fhrep = new DCRespository<DC.Entity.FLSDetailsJournal>();

            var fd = frep.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
            if (fd != null)
            {
                fd.UserID = null;
                fd.ScheduledDate = Convert.ToDateTime(txtSeheduledDateTime.Text + " " + (string.IsNullOrEmpty(txtScheduledTime.Text) ? "00:00:00" : txtScheduledTime.Text + ":00"));
                if (!string.IsNullOrEmpty(txtPreferreddate2.Text))
                    fd.Preferreddate2 = Convert.ToDateTime(txtPreferreddate2.Text + " " + (string.IsNullOrEmpty(txtPreferreddatetime2.Text) ? "00:00:00" : txtPreferreddatetime2.Text + ":00"));
                if (!string.IsNullOrEmpty(txtPreferreddate3.Text))
                    fd.Preferreddate3 = Convert.ToDateTime(txtPreferreddate3.Text + " " + (string.IsNullOrEmpty(txtPreferreddatetime3.Text) ? "00:00:00" : txtPreferreddatetime3.Text + ":00"));
                frep.Edit(fd);
            }
            ddlAssignedtoName.SelectedValue = "0";
            lblSuccessmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            //if (htid.Value == "0")
            //{

            //    string ProductId = CheckBoxCheckedOrNotInProductGrid();
            //    if (ProductId != string.Empty)
            //    {
            //        int id = AddRecord(1, Convert.ToInt32(ProductId));
            //        //SaveProductToCallid(ProductId, id);

            //        SaveHealthForm(id);
            //        //  SavePlaceholderData(id, Convert.ToInt32(ddlCompany.SelectedValue));
            //        Response.Redirect("~/WF/DC/FLSForm.aspx?callid=" + id.ToString() + "&SDID=" + id.ToString());
            //    }
            //    else
            //    {
            //        lblMsg.Visible = true;
            //        lblMsg.Text = "Please select one product";
            //    }
            //}
            //else
            //{
            //    UpdateRecord(int.Parse(htid.Value));
            //    UpdateHealthForm();
            //    // FLS custom fields data insert
            //    int id = Convert.ToInt32(htid.Value);
            //    //  SavePlaceholderData(id, Convert.ToInt32(ddlCompany.SelectedValue));
            //    Response.Redirect("~/WF/DC/FLSForm.aspx?callid=" + int.Parse(htid.Value) + "&SDID=" + int.Parse(htid.Value));
            //}
            ////  }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    //protected void btnAssign_Click(object sender, EventArgs e)
    //{
    //    string s = hrid.Value;
    //    //assign value
    //    string ProductId = CheckBoxCheckedOrNotInProductGrid();
    //    if (ProductId != string.Empty)
    //    {
    //        int id = AddRecord(1, Convert.ToInt32(ProductId));
    //        //SaveProductToCallid(ProductId, id);

    //        SaveHealthForm(id);
    //        //  SavePlaceholderData(id, Convert.ToInt32(ddlCompany.SelectedValue));
    //        Response.Redirect("~/WF/DC/FLSForm.aspx?callid=" + id.ToString() + "&SDID=" + id.ToString());
    //    }
    //    else
    //    {
    //        lblMsg.Visible = true;
    //        lblMsg.Text = "Please select one product";
    //    }
    //}

    //protected void btnEmailRequest_Click(object sender, EventArgs e)
    //{
    //    string s = hrid.Value;
    //    //send mail
    //    string ProductId = CheckBoxCheckedOrNotInProductGrid();
    //    if (ProductId != string.Empty)
    //    {
    //        int id = AddRecord(2, Convert.ToInt32(ProductId));
            
    //        //update to Resources 
    //        SaveHealthForm(id);
    //        //  SavePlaceholderData(id, Convert.ToInt32(ddlCompany.SelectedValue));
    //        Response.Redirect("~/WF/DC/FLSForm.aspx?callid=" + id.ToString() + "&SDID=" + id.ToString());
    //    }
    //    else
    //    {
    //        lblMsg.Visible = true;
    //        lblMsg.Text = "Please select one product";
    //    }
    //}

    //private void DocumentSectionVisibility()
    //{
    //    var flsSectionConfig = FLSSectionConfigBAL.GetFLSSectionConfigList().Where(f => f.CustomerID == sessionKeys.PortfolioID).FirstOrDefault();
    //    if(flsSectionConfig != null)
    //    {
    //        //PnlFileUpload.Visible
    //    }
    //}

    public string GetAllPincodesOfRequester()
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<MarkersData> MarkersDataList = new List<MarkersData>();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            IDCRespository<DC.Entity.CallDetail> dReporsitory = new DCRepository<DC.Entity.CallDetail>();
            IDCRespository<DC.Entity.FLSDetail> fReporsitory = new DCRepository<DC.Entity.FLSDetail>();
            //var sids = new int[] { 22, 43, 44 };
            var dList = dReporsitory.GetAll().Where(o => o.ID == QueryStringValues.CallID).ToList();
            var fList = fReporsitory.GetAll().Where(o => dList.Select(p => p.ID).Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> paReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
            var requesterList = pReporsitory.GetAll().Where(o => dList.Select(r => r.RequesterID).ToArray().Contains(o.ID)).ToList();
            var requesterAddressList = paReporsitory.GetAll().Where(o => fList.Select(r => r.ContactAddressID).ToArray().Contains(o.ID)).ToList();
            var getCCIDs = FLSDetailsBAL.GetCCIDS();
            Dictionary<string, object> row;
            //using (AssetsToSoftwareDataContext asset = new AssetsToSoftwareDataContext())
            //{
            List<DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass> LocationPinCodeResult = new List<DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass>();
            //if (sessionKeys.SID == 1 || sessionKeys.SID == 2)
                LocationPinCodeResult = (from a in requesterList
                                         join a1 in requesterAddressList on a.ID equals a1.ContactID
                                         join b in dList on a.ID equals b.RequesterID
                                         where b.ID== QueryStringValues.CallID
                                         orderby b.ID descending
                                         //where a.PortfolioID == sessionKeys.PortfolioID
                                         select new DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass
                                         {
                                             LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : a1.PostCode,
                                             address = "<br>" + "" + getCCIDs.Where(o => o.CallID == b.ID).FirstOrDefault().CompanyCallID + "<br>" + a.Name + "<br>" + a1.Address + "<br>" + a1.City + "<br>" + a1.State + "<br>" + a1.PostCode,
                                             Id = getCCIDs.Where(o => o.CallID == b.ID).FirstOrDefault().CompanyCallID// a1.ID
                                         }).Take(10).ToList();

            //else if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
            //    LocationPinCodeResult = (from a in requesterList
            //                             join a1 in requesterAddressList on a.ID equals a1.ContactID
            //                             join b in dList on a.ID equals b.RequesterID
            //                             join f in fList on b.ID equals f.CallID
            //                             where f.UserID == sessionKeys.UID
            //                             orderby b.ID descending

            //                             //where a.PortfolioID == sessionKeys.PortfolioID
            //                             select new DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass
            //                             {
            //                                 LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : a1.PostCode,
            //                                 address = "<br>" + "" + b.ID + "<br>" + a.Name + "<br>" + a1.Address + "<br>" + a1.City + "<br>" + a1.State + "<br>" + a1.PostCode,
            //                                 Id = a1.ID
            //                             }).Take(10).ToList();

            //var LocationPinCodeResult = (from a in asset.V_Assets
            //                             where a.PortfolioID == sessionKeys.PortfolioID
            //                             select new
            //                             {
            //                                 LocationPinCode = a.FromLocation,
            //                                 Id = a.ID
            //                             }).ToList();
            //var AssetAssociatedToContactsList = asset.AssetAssociatedToContacts.
            //    Where(c => c.ContactId.Value == int.Parse(RequesterId)).Select(c => c.AssectId.Value.ToString()).ToArray();
            //LocationPinCodeResult = LocationPinCodeResult.
            //    Where(a => ContainsNew(a.Id.ToString(), AssetAssociatedToContactsList)).ToList();

            MarkersData MarkersDataSingle;

            foreach (var x in LocationPinCodeResult)
            {
                MarkersDataSingle = new MarkersData();
                MarkersDataSingle.title = x.LocationPinCode;
                string LL = getLatLong(x.LocationPinCode);
                string[] LLArray = LL.Split(',');
                MarkersDataSingle.lat = LLArray[1];
                MarkersDataSingle.lng = LLArray[2];
                MarkersDataSingle.description = x.address; //LLArray[3];
                MarkersDataSingle.Id = x.Id;
                MarkersDataList.Add(MarkersDataSingle);
            }

            DataTable dt = ToDataTable(MarkersDataList);
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            // }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return serializer.Serialize(rows);
    }
    public string getLatLong(string Zip)
    {
        string latlong = "", address = "";

        //IDCRespository<DC.Entity.GeoCode> gRep = new DCRepository<DC.Entity.GeoCode>();
        //var gEntity = gRep.GetAll().Where(o=>o.Zip == Zip.Trim()).FirstOrDefault();
        //if(gEntity!= null)
        //{
        //    latlong = Zip + "," + Convert.ToString(gEntity.Latitude) + "," + Convert.ToString(gEntity.Longitude) + "," + "" + Zip;
        //}

        address = "https://maps.googleapis.com/maps/api/geocode/xml?components=postal_code:" + Zip.Trim() + "&sensor=false&key=" + hkey.Value;
        var result = new System.Net.WebClient().DownloadString(address);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);
        XmlNodeList parentNode = doc.GetElementsByTagName("location");
        var lat = "";
        var lng = "";
        foreach (XmlNode childrenNode in parentNode)
        {
            lat = childrenNode.SelectSingleNode("lat").InnerText;
            lng = childrenNode.SelectSingleNode("lng").InnerText;
        }
        latlong = Zip + "," + Convert.ToString(lat) + "," + Convert.ToString(lng) + "," + "" + Zip;
        return latlong;
    }
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
    public class WeatherInfo
    {
        public City city { get; set; }
        public List<List> list { get; set; }
    }

    public class City
    {
        public string name { get; set; }
        public string country { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public double humidity { get; set; }
    }

    //public class Temp
    //{
    //    public double day { get; set; }
    //    public double min { get; set; }
    //    public double max { get; set; }
    //    public double night { get; set; }
    //}

    public class Weather
    {
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class List
    {
        public Main main { get; set; }
        public int humidity { get; set; }
        public List<Weather> weather { get; set; }
        public string dt_txt { get; set; }
    }
    public class Todaydata
    {
        public string time { get; set; }
        public string timedeg { get; set; }
    }
        public class retval
    {
        public string city { get; set; }
        public string temp { get; set; }
        public string day { get; set; }
        public string date { get; set; }
        public List<Todaydata> todaydata { get; set; }
    }
    protected string GetWeatherInfo()
    {
        try
        {
            if (hpost.Value != "0")
            {
                List<retval> retList = new List<retval>();
                List<Todaydata> tList = new List<Todaydata>();
                string appId = "cea0d6b75754fe3ab77d3983f22573d5";
                string url = string.Format("http://api.openweathermap.org/data/2.5/forecast?zip={0}&units=metric&cnt=40&APPID={1}", hpost.Value, appId);
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);

                    WeatherInfo weatherInfo = (new JavaScriptSerializer()).Deserialize<WeatherInfo>(json);
                    var w = weatherInfo.city.name + "," + weatherInfo.city.country;
                    //imgCountryFlag.ImageUrl = string.Format("http://openweathermap.org/images/flags/{0}.png", weatherInfo.city.country.ToLower());
                    var setTempValues = "< ul class='list-unstyled'>";
                    int i = 0;
                    string oldday = string.Empty;
                    string firstdate = string.Empty;
                    foreach (var l in weatherInfo.list)
                    {
                        var r = new retval();
                        r.city = weatherInfo.city.name + "," + weatherInfo.city.country;
                        var dt = Convert.ToDateTime(l.dt_txt);
                        r.date = dt.ToShortDateString();
                        if (i == 0)
                        {

                            oldday = dt.DayOfWeek.ToString();
                            firstdate = dt.DayOfWeek.ToString();
                            setTempValues = setTempValues + string.Format("<li>< div class='xe-weekday-forecast'><div class='xe-temp'>{0}&#8457;</div><div class='xe-day'>{1}</div><div class='xe-icon'><i class='meteocons-windy-inv'></i></div></div></li>", Math.Round(Fahrenheit(l.main.temp)), dt.DayOfWeek);
                            r.day = oldday;
                            r.temp = Math.Round(Fahrenheit(l.main.temp)).ToString();

                            tList.Add(new Todaydata() { time = string.Format("{0:HH:mm}", dt), timedeg = Math.Round(Fahrenheit(l.main.temp)).ToString() });



                            retList.Add(r);
                        }

                        else if (firstdate == dt.DayOfWeek.ToString())
                        {
                            tList.Add(new Todaydata() { time = string.Format("{0:HH:mm}", dt), timedeg = Math.Round(Fahrenheit(l.main.temp)).ToString() });

                        }

                        if (oldday != dt.DayOfWeek.ToString())
                        {
                            oldday = dt.DayOfWeek.ToString();
                            var d = l.weather[0].description;
                            setTempValues = setTempValues + string.Format("<li>< div class='xe-weekday-forecast'><div class='xe-temp'>{0}&#8457;</div><div class='xe-day'>{1}</div><div class='xe-icon'><i class='meteocons-windy-inv'></i></div></div></li>", Math.Round(Fahrenheit(l.main.temp)), dt.DayOfWeek);
                            r.day = oldday;
                            r.temp = Math.Round(Fahrenheit(l.main.temp)).ToString();
                            retList.Add(r);
                        }

                        i++;

                    }
                    if (retList.FirstOrDefault() != null)
                        retList.FirstOrDefault().todaydata = tList;

                    setTempValues = setTempValues + "</ul>";
                    return JsonConvert.SerializeObject(retList);

                }
            }
            else
            {
                return JsonConvert.SerializeObject(string.Empty);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return JsonConvert.SerializeObject(string.Empty);
        }
    }

    protected double Fahrenheit(double celsius)
    {
        double c = (celsius * 9) / 5 + 32;

        return c;
    }
}
