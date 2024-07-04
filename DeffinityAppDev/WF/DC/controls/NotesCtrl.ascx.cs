using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.Entity;
using DC.DAL;
using DC.BAL;
using UserMgt.DAL;
using PortfolioMgt.Entity;
using DC.BLL;
using System.Text;
public partial class DC_controls_NotesCtrl : System.Web.UI.UserControl
{
   
           
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindGrid();
        }

    }


    private void BindGrid()
    {
        try
        {
            using (DCDataContext db = new DCDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    int callId = Convert.ToInt32(Request.QueryString["callid"]);


                    var notesList = db.Notes.Where(n => n.CallID == callId).OrderByDescending(o => o.ID).ToList();
                    var contractorList = ud.Contractors.Select(s => s).ToList();
                    h_statusid.Value = db.CallDetails.Where(p => p.ID == callId).Select(p => p.StatusID.HasValue ? p.StatusID.Value : 0).FirstOrDefault().ToString();

                    var result = new List<NotesList>();

                    if (callId > 0)
                    {
                        result = (from f in notesList
                                  join c in contractorList
                                      on f.LoggedBy equals c.ID
                                  orderby f.ID descending
                                  select new NotesList { ID = f.ID, UserName = c.ContractorName, DateTime = f.LoggedDate, Notes = f.Notes }).ToList();
                    }
                    //add empty row
                    if (!StatusCheck())
                        result.Add(new NotesList { ID = -99, UserName = "" });


                    gvNotes.DataSource = result;
                    gvNotes.DataBind();
                    //if(gvNotes.Rows.Count ==1)
                    //{
                    //    if (StatusCheck())
                    //        gvNotes.Visible = false;
                    //}

                }
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
      
    }
    protected void gvNotes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int callId = Convert.ToInt32(Request.QueryString["callid"]);
            if (e.CommandName == "InsertNotes")
            {
                string fnotes = ((TextBox)gvNotes.FooterRow.FindControl("txtfNotes")).Text;
                using (DCDataContext db = new DCDataContext())
                {
                    var callDetails = db.CallDetails.Where(c => c.ID == callId).FirstOrDefault();
                    Note notes = new Note();
                    notes.CallID = callId;
                    notes.Notes = fnotes;
                    notes.LoggedBy = sessionKeys.UID;
                    notes.LoggedDate = DateTime.Now;
                    db.Notes.InsertOnSubmit(notes);
                    db.SubmitChanges();
                    //6- FLS
                    if (callDetails.RequestTypeID == 6)
                    {
                        //34	Resolved
                         //if the status is resolved then customer adds notes then change the status to Awaiting information
                        //if (callDetails.StatusID == 34)
                        //{
                            //update the status
                            //31	Awaiting Information
                            //callDetails.StatusID = 31;
                            //db.SubmitChanges();
                       // }
                        //After adding notes maill should go to customer user & assigned user
                        //FLS_SendMailtoRequester();
                    }
                    else if (callDetails.RequestTypeID == 1)
                    {
                        //mail to customer
                        //if (sessionKeys.SID == 7)
                        //   DeliveryDistributionListmail();
                        //else
                        //    DeliveryRequestermail();
                    }
                    //Access request
                    else if (callDetails.RequestTypeID == 3)
                    {
                        //if (sessionKeys.SID == 7)
                        //    AccessDistributionMail();
                        //else
                        //    AccessRequesterMail();
                    }
                    
                }

            }
            else if (e.CommandName == "Update")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                int i = gvNotes.EditIndex;
                GridViewRow Row = gvNotes.Rows[i];
                string notes = ((TextBox)Row.FindControl("txtNotes")).Text.Trim();
                using (DCDataContext db = new DCDataContext())
                {
                    var callDetails = db.CallDetails.Where(c => c.ID == callId).FirstOrDefault();
                    Note notesPresent = db.Notes.Where(n => n.ID == id).FirstOrDefault();
                    if (notesPresent != null)
                    {
                        notesPresent.Notes = notes;
                        db.SubmitChanges();
                          //6- FLS
                        
                        if (callDetails.RequestTypeID == 6)
                        {
                            //34	Resolved
                            //if the status is resolved then customer adds notes then change the status to Awaiting information
                            if (callDetails.StatusID == 34)
                            {
                                //update the status
                                //31	Awaiting Information
                                //callDetails.StatusID = 31;
                                //db.SubmitChanges();
                            }
                            //After changing the notes maill should go to customer user & assigned user
                            //FLS_SendMailtoRequester();
                        }
                        //6- Delivery
                        else if (callDetails.RequestTypeID == 1)
                        {
                            //mail to customer
                            //if (sessionKeys.SID == 7)
                            //    DeliveryDistributionListmail();
                            //else 
                            //    DeliveryRequestermail();
                        }
                        //Access request
                        else if (callDetails.RequestTypeID  == 3)
                        {
                            //if (sessionKeys.SID == 7)
                            //    AccessDistributionMail();
                            //else
                            //    AccessRequesterMail();
                        }
                    }
                }
            }
            else if (e.CommandName == "delete")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                using (DCDataContext db = new DCDataContext())
                {
                    Note notesPresent = db.Notes.Where(n => n.ID == id).FirstOrDefault();
                    if (notesPresent != null)
                    {
                        db.Notes.DeleteOnSubmit(notesPresent);
                        db.SubmitChanges();
                       
                    }
                }
            }
            // Bind the grid
            BindGrid();
        }
        catch (Exception ex)
        {

            LogExceptions.WriteExceptionLog(ex);
        }
       
    }
   
    protected void gvNotes_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvNotes.EditIndex = -1;
        BindGrid();
    }
    protected void gvNotes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        gvNotes.EditIndex = -1;
        BindGrid();
    }
    protected void gvNotes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvNotes.EditIndex = -1;
        BindGrid();
    }
    protected void gvNotes_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvNotes.EditIndex = e.NewEditIndex;
        BindGrid();
    }

   
    protected void gvNotes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string id = ((Label)e.Row.FindControl("lblID")).Text.Trim();
            LinkButton imgEdit = ((LinkButton)e.Row.FindControl("lnkEditNotes"));
            LinkButton imgDelete = ((LinkButton)e.Row.FindControl("imgDelete"));
            
            
            if (id == "-99")
            {
                e.Row.Visible = false;
            }
            //if status is closed edit image should hide
            if (imgEdit != null)
            {
                if (StatusCheck())
                {
                    imgEdit.Visible = false;
                    imgDelete.Visible = false;
                }
                   
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //if status is closed footer should hide
            if (StatusCheck())
            e.Row.Visible = false;
        }
    }
    //Closed Status ids
    //--1	Delivery - 7 
    //--2	Permit to Work - 13
    //--3	Access Control - 21
    //--6	FLS - 35
    private bool StatusCheck()
    { 
        string[] ids = {"7","13","21","35"};
        return ids.Contains(h_statusid.Value);
    }

    #region Mails
    private bool GetFieldVisibility(string fieldName, List<FLSFieldsConfig> fieldList)
    {
        bool isVisible = true;
        var exists = fieldList.Where(f => f.DefaultName.ToLower() == fieldName.ToLower()).FirstOrDefault();
        if (exists != null)
            isVisible = Convert.ToBoolean(exists.IsVisible);
        return isVisible;
    }
    private void FLS_SendMailtoRequester()
    {
        try
        {
            int callid = QueryStringValues.CallID;
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();
            EmailFooter ef = new EmailFooter();
            AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select();
            
            ef = FooterEmail.EmailFooter_selectByID(6,sessionKeys.PortfolioID);
            using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
            {
                using (DC.DAL.DCDataContext dc = new DCDataContext())
                {
                    var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).ToList();
                    var cdetails = dc.CallDetails.Where(c => c.ID == callid).FirstOrDefault();
                    var site = dc.OurSites.Where(c => c.ID == cdetails.SiteID).FirstOrDefault();
                    var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                    var portfolio = pd.ProjectPortfolios.Where(c => c.ID == cdetails.CompanyID).FirstOrDefault();
                    var status = dc.Status.Where(c => c.ID == cdetails.StatusID).FirstOrDefault();
                    var flsdetail = dc.FLSDetails.Where(p=>p.CallID == cdetails.ID).FirstOrDefault();
                    var subjectEntity = dc.Subjects.Where(c => c.ID == flsdetail.SubjectID).FirstOrDefault();
                    var noteslist = dc.Notes.Where(c => c.CallID == callid).ToList();
                    //var subject = dc
                    //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                    string subject = "Ticket Reference: " + callid.ToString();
                    Emailer em = new Emailer();
                    string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSNotesMail.htm");
                    
                    body = body.Replace("[mail_head]", "Service Request Update");
                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                    //body = body.Replace("[sitename]", site.Name);
                    //body = body.Replace("[Company]", portfolio.PortFolio);
                    //body = body.Replace("[subject]", subjectEntity.SubjectName);
                    //body = body.Replace("[Status]", status.Name);
                    //body = body.Replace("[contact]", string.IsNullOrEmpty(pcontact.Email) ? string.Empty : pcontact.Email);
                    //body = body.Replace("[mobile]", string.IsNullOrEmpty(pcontact.Telephone) ? string.Empty : pcontact.Telephone);
                    //body = body.Replace("[reference]", cdetails.ID.ToString());
                    //body = body.Replace("[details]", flsdetail.Details);
                    //body = body.Replace("[seduledate]", flsdetail.ScheduledDate.Value.ToShortDateString());
                    body = body.Replace("[footer]", Server.HtmlDecode(ef == null ? string.Empty : ef.EmailFooter1));
                    //body = body.Replace("[notes]", "");
                    //body = body.Replace("[adminemail]", ae == null ? string.Empty : ae.EmailAddress);
                    body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                    body = body.Replace("[noteslist]", GetNotesList(noteslist));
                    body = body.Replace("[subhead]", "Details of your Service Request can be found below:");
                    if (!string.IsNullOrEmpty(flsdetail.Resolution) && sessionKeys.SID == 7)
                        body = body.Replace("[resolvenotes]", string.Format("<tr><td style='padding-top: 10px;'>Resolution:</td><td style='padding-top: 10px;'><b>{0}</b></td></tr>", flsdetail.Resolution));
                    else
                        body = body.Replace("[resolvenotes]", string.Empty);

                    StringBuilder sb = new StringBuilder();
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Customer:", portfolio.PortFolio));
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Site information:", site.Name));
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Job Reference:", "" + callid));
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Status of Service Desk:", status.Name));
                    if (GetFieldVisibility("details", fieldList))
                        sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Details of the request:", flsdetail.Details));
                    if (GetFieldVisibility("Scheduled Date/Time", fieldList))
                        sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Scheduled Date/Time:", flsdetail.ScheduledDate.Value.ToString(Deffinity.systemdefaults.GetFullDateTimeformat())));
                    if (GetFieldVisibility("Notes", fieldList))
                    {
                        sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Notes:", ""));
                    }

                    //string[] customer = Regex.Split(ccdCompany.SelectedValue, ":::");
                    //int customerId = Convert.ToInt32(customer[0] == "" ? "0" : customer[0]);
                    List<FLSAdditionalInfo> flsAdditionalInfoList = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(callid);
                    List<FLSCustomField> clist = CustomFormDesignerBAL.GetFieldList(sessionKeys.PortfolioID,0).ToList();
                    foreach (FLSCustomField c in clist)
                    {
                        string val = flsAdditionalInfoList.Where(p => p.CustomFieldID == c.ID).Select(p => p.CustomFieldValue).FirstOrDefault();
                        string Lname = c.LabelName;
                        sb.Append(string.Format("<tr><td>{0}:</td><td><b>{1}</b></td><tr>", Lname, val));
                    }
                   
                    body = body.Replace("[datarow]", sb.ToString());

                    string Dis_body = body;
                    bool ismailsent = false;
                    // mail to requester
                    //if help desk or assign users are changed then mail should go to requester
                    if (!string.IsNullOrEmpty(pcontact.Email) && sessionKeys.SID != 7)
                    {
                        body = body.Replace("[user]", pcontact.Name);
                       
                        em.SendingMail(fromemailid, subject, body, pcontact.Email);
                        ismailsent = true;
                    }
                    if (sessionKeys.SID == 7)
                    {
                        UserMgt.BAL.ContractorsBAL cCollection = new UserMgt.BAL.ContractorsBAL();
                        if (flsdetail.UserID.HasValue)
                        {
                            var assignUser = cCollection.Contractor_SelectAll().Where(p => p.ID == flsdetail.UserID).FirstOrDefault();
                            body = body.Replace("[user]", assignUser.ContractorName);
                            //body = body.Replace("[subhead]", "Details of Service Request can be found below:");
                            em.SendingMail(fromemailid, subject, body, assignUser.EmailAddress);
                        }
                        List<int> idlist = SecurityAccessMail.GetIds(6);
                        if(idlist.Count >0)
                        {
                            Dis_body = Dis_body.Replace("[user]", "All");
                            List<string> strList = SecurityAccessMail.GetEmailAddresses(idlist);
                            if (strList.Count > 0)
                            {
                               
                                foreach (string s in strList)
                                {
                                    em.SendingMail(fromemailid, subject, Dis_body, s);
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
    private string GetNotesList(List<Note> noteslist)
    {
        StringBuilder sbuild = new StringBuilder();
        if (noteslist.Count > 0)
        {
            UserMgt.BAL.ContractorsBAL cCollection = new UserMgt.BAL.ContractorsBAL();
            var uids = noteslist.Select(p=>p.LoggedBy).ToArray();
            var usercollection = cCollection.Contractor_SelectAll().Where(p => uids.Contains(p.ID)).ToList();
            sbuild.Append("<table style='width:100%'>");
            sbuild.Append("<tr style='background-color:silver;color:white;text-align:center;font-weight:bold;'>");
            sbuild.Append("<td style='width:50%'>Notes</td><td>Logged by</td><td> Date & Time</td>");
            sbuild.Append("</tr>");
            foreach (Note n in noteslist)
            {
                sbuild.Append("<tr>");
                sbuild.Append(string.Format("<td>{0}</td><td>{1}</td><td>{2}</td>",n.Notes,usercollection.Where(p=>p.ID == n.LoggedBy).Select(p=>p.ContractorName).FirstOrDefault(), n.LoggedDate.Value));

                sbuild.Append("</tr>");
            }
            sbuild.Append("</table>");
        }

        return sbuild.ToString();
    }

    private void DeliveryRequestermail()
    {
        try
        {
            using (DCDataContext dc = new DCDataContext())
            {
                int callid = QueryStringValues.CallID;
                CallDetail cd = dc.CallDetails.Where(o => o.ID == callid).FirstOrDefault();
                var stlist = dc.Status.ToList();
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                DC.SRV.WebService ws = new DC.SRV.WebService();

                EmailFooter ef = new EmailFooter();
                ef = FooterEmail.EmailFooter_selectByID(1);
                PortfolioContact pc = ws.GetContactDetails(cd.RequesterID.Value);
                AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select();
                Emailer em = new Emailer();
                string subject = string.Empty;
                string body = em.ReadFile("~/WF/DC/EmailTemplates/DeliveryMail.htm");
                string status = stlist.Where(o => o.ID == cd.StatusID).Select(o => o.Name).FirstOrDefault();

              
                    subject = "Delivery Request: " + cd.ID.ToString();
                    if (status.ToLower() == "closed")
                        body = body.Replace("[mail_head]", "Delivery Request Closed");
                    else
                        body = body.Replace("[mail_head]", "Delivery Request Update");
                //Thank you for raising a Delivery request, details of your request can be found below:
                    if (status == "Awaiting Approval" )
                { }
                    else if (status == "Awaiting Delivery")
                { }
                    else if (status == "Closed")
                {
                    body = body.Replace("Thank you for raising a Delivery Request, details of your request can be found below:", "The following Delivery Request has now been closed.");
                }
                else
                {
                    body = body.Replace("Thank you for raising a Delivery Request, details of your request can be found below:", "Please see below an update on your Delivery Request:");
                }
                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                body = body.Replace("[user]", pc.Name);
                body = body.Replace("[Status]", status);
                body = body.Replace("[adminemail]", ae.EmailAddress);
                body = body.Replace("[footer]", ef == null ? string.Empty : HttpUtility.HtmlDecode(ef.EmailFooter1));
                body = body.Replace("[mailcontent]", RequestMailFormat.DeliveryHtmlMailDetails(cd.ID));

                em.SendingMail(fromemailid, subject, body, pc.Email);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void DeliveryDistributionListmail()
    {
        try
        {

            int callid = QueryStringValues.CallID;
            List<int> idlist = SecurityAccessMail.GetIds(1); // 1=delivery
            if (idlist.Count > 0)
            {
                using (DCDataContext dc = new DCDataContext())
                {
                    CallDetail cd = dc.CallDetails.Where(o => o.ID == callid).FirstOrDefault();
                    var stlist = dc.Status.ToList();
                    string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                    DC.SRV.WebService ws = new DC.SRV.WebService();
                    string status = stlist.Where(o => o.ID == cd.StatusID).Select(o => o.Name).FirstOrDefault();

                    string subject = string.Format(" Delivery Request: " + cd.ID.ToString());
                    Emailer em = new Emailer();
                    string body = em.ReadFile("~/WF/DC/EmailTemplates/DeliveryDistributionListMail.htm");
                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                    body = body.Replace("[mail_head]", "Delivery Request Update");
                   
                    body = body.Replace("[content_header]", "Delivery Request Update");
                    body = body.Replace("[Status]", status);
                    body = body.Replace("[mailcontent]", RequestMailFormat.DeliveryHtmlMailDetails(cd.ID));

                    body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());

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
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void AccessRequesterMail()
    {
        try
        {
            using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
            {
                using (DCDataContext dc = new DCDataContext())
                {
                    int callid = QueryStringValues.CallID;
                    CallDetail cd = dc.CallDetails.Where(o => o.ID == callid).FirstOrDefault();
                    var statusname = dc.Status.Where(o => o.ID == cd.StatusID).Select(o => o.Name).FirstOrDefault();
                    AccessControl accessEntity = dc.AccessControls.Where(o => o.CallID == callid).FirstOrDefault();
                    string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                    var check_insert = false;
                    DC.SRV.WebService ws = new DC.SRV.WebService();
                    List<string> site = new List<string>();
                    //ProjectPortfolio cmpy = ProjectPortFolioBAL.SelectbyId(int.Parse(ddlRcmpy.SelectedValue));
                    // site = ws.GetSiteDetailsbyId(int.Parse(ddlsite.SelectedValue));
                    EmailFooter ef = new EmailFooter();
                    AccessControlEmail ace = DefaultsOfAccessControl.AccessEmail_select();
                    ef = FooterEmail.EmailFooter_selectByID(3);
                    PortfolioContact pc = ws.GetContactDetails(cd.RequesterID.Value);
                    string subject = string.Empty;

                    subject = "Ticket Reference: " + callid;
                    Emailer em = new Emailer();
                    string body = em.ReadFile("~/WF/DC/EmailTemplates/AccessControlMail.htm");

                    if (statusname == "Expected" && check_insert)
                    { body = body.Replace("[mail_head]", "Access Request"); }
                    else if (statusname == "Closed")
                    {
                        body = body.Replace("[mail_head]", "Access Request Closed");
                        body = body.Replace("Thank you for raising a Access request, details of your request can be found below:", "The following Access Request has now been closed.");
                    }
                    else
                    {
                        body = body.Replace("[mail_head]", "Access Request Update");
                        body = body.Replace("Thank you for raising a Access request, details of your request can be found below:", "Please see below an update on your Access Request:");
                    }

                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                    body = body.Replace("[user]", pc.Name);
                    body = body.Replace("[requestercompany]", pd.ProjectPortfolios.Where(o => o.ID == cd.CompanyID).Select(o => o.PortFolio).FirstOrDefault());
                    body = body.Replace("[requestername]", pc.Name);
                    body = body.Replace("[sitename]", dc.OurSites.Where(o => o.ID == cd.SiteID).Select(o => o.Name).FirstOrDefault());
                    body = body.Replace("[status]", statusname);
                    body = body.Replace("[requesteddate]", string.Format(Deffinity.systemdefaults.GetStringDateformat(), accessEntity.RequestedDate.Value));
                    body = body.Replace("[noofdays]", (accessEntity.NumberOfDays.HasValue ? accessEntity.NumberOfDays.Value : 0).ToString());
                    //body = body.Replace("[area]", ddlarea.SelectedItem.Text); 
                    body = body.Replace("[purposeofvisit]", dc.PurposeToVisits.Where(o => o.ID == accessEntity.PurposeOfVisitID).Select(o => o.Name).FirstOrDefault());
                    body = body.Replace("[requesttype]", "Access Request");
                    //19	Arrived
                    //42	Departed
                    //if (StatusName.ToLower() == "arrived")
                    //    body = body.Replace("[statustime]", string.Format("<tr><td style='padding-top: 10px;' width='20%' valign='top' >Arrived <b>:</b></td><td style='padding-top: 10px;'><b>{0}</b></td></tr>", GetTimeByStatus(callid, 19)));
                    //else if (StatusName.ToLower() == "departed")
                    //    body = body.Replace("[statustime]", string.Format("<tr><td style='padding-top: 10px;' width='20%' valign='top' >Departed <b>:</b></td><td style='padding-top: 10px;'><b>{0}</b></td></tr>", GetTimeByStatus(callid, 42)));
                    //else
                        body = body.Replace("[statustime]", "");
                    body = body.Replace("[notes]", accessEntity.Notes);


                    body = body.Replace("[noteslist]", RequestMailFormat.GetNotesList(callid));

                    List<AccessNumbersBasedonDay> acno = AccessNumberBAL.AccessNumber_selectByID(callid);
                    StringBuilder stracday = new StringBuilder();
                    StringBuilder stracnum = new StringBuilder();
                    foreach (AccessNumbersBasedonDay ac in acno)
                    {

                        string str = string.Format("Access No. (Day" + " " + ac.Day + ")" + "<br />");
                        stracday.Append(str);
                        string strNum = string.Format(ac.AccessNumber + "<br />");
                        stracnum.Append(strNum);
                    }

                    body = body.Replace("AccessNo.", HttpUtility.HtmlDecode(stracday.ToString()));
                    body = body.Replace("[accessno]", HttpUtility.HtmlDecode(stracnum.ToString()));
                    List<Visitor> visitors = new List<Visitor>();

                    //visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == callid).ToList();
                    if (statusname == "Arrived")
                    {
                        visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == callid && p.ArrivalDate.HasValue ).ToList();
                    }
                    else if (statusname == "Departed")
                    {
                        visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == callid && p.DepatureDate.HasValue).ToList();
                    }
                    else if (statusname == "Expected")
                    {
                        visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == callid && !p.ArrivalDate.HasValue && !p.DepatureDate.HasValue).ToList();
                    }
                    string bodyVisitors = string.Empty;
                    StringBuilder visitorTable = new StringBuilder();
                    if (visitors.Count > 0)
                    {
                        bodyVisitors = em.ReadFile("~/WF/DC/EmailTemplates/AccesControlMailVisitors.htm");

                        string strValue = string.Empty;
                        int intI = 1;

                        foreach (Visitor objVisitor in visitors)
                        {
                            visitorTable.Append(RetRowdata("Visitor " + HttpUtility.HtmlDecode(intI.ToString()),
                                   objVisitor.Name, objVisitor.Company, objVisitor.EmailAddress, objVisitor.AccessNo.ToString(),
                                   string.Format(objVisitor.ArrivalDate.HasValue ? string.Format("{0:dd/MM/yyyy HH:mm }", objVisitor.ArrivalDate.Value) : string.Empty),
                                   string.Format(string.Format(objVisitor.DepatureDate.HasValue ? string.Format("{0:dd/MM/yyyy HH:mm }", objVisitor.DepatureDate.Value) : string.Empty))));
                            intI++;
                        }

                    }
                    if (visitorTable.Length > 0)
                    {
                        bodyVisitors = bodyVisitors.Replace("[rowdata]", visitorTable.ToString());
                        visitorTable.Clear();
                    }
                    else
                        bodyVisitors = bodyVisitors.Replace("[rowdata]", string.Empty);

                    bodyVisitors = bodyVisitors.Replace("[status]", statusname);
                    if (bodyVisitors == string.Empty)
                    {
                        body = body.Replace("[visitorsdata]", "");
                    }
                    else
                    {
                        body = body.Replace("[visitorsdata]", bodyVisitors);
                    }

                    body = body.Replace("[footer]", HttpUtility.HtmlDecode(ef.EmailFooter1));
                    body = body.Replace("[adminemail]", ace.EmailAddress);
                    em.SendingMail(fromemailid, subject, body, pc.Email);

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void AccessDistributionMail()
    {
        using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
        {
            using (DCDataContext dc = new DCDataContext())
            {
                int callid = QueryStringValues.CallID;
                CallDetail cd = dc.CallDetails.Where(o => o.ID == callid).FirstOrDefault();
                AccessControl accessEntity = dc.AccessControls.Where(o => o.CallID == callid).FirstOrDefault();
                var statusname = dc.Status.Where(o => o.ID == cd.StatusID).Select(o => o.Name).FirstOrDefault();
                DC.SRV.WebService ws = new DC.SRV.WebService();
                PortfolioContact pc = ws.GetContactDetails(cd.RequesterID.Value);
                Emailer em = new Emailer();
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                string subject = "Ticket Reference: " + callid;
                //string subject = string.Format("Ticket Reference: {4} {0} {1} has requested access control on {2} at {3}", ddlRcmpy.SelectedItem.Text, ddlRname.SelectedItem.Text, DateTime.Now.ToString(Deffinity.systemdefaults.GetDateformat()), DateTime.Now.ToString("HH:mm"), h_callid.Value);
                string body = em.ReadFile("~/WF/DC/EmailTemplates/AccessControlDistributionListMail.htm");
                body = body.Replace("[mail_head]", "Access Request");
                body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                body = body.Replace("[Customer]", pd.ProjectPortfolios.Where(o=>o.ID == cd.CompanyID).Select(o=>o.PortFolio).FirstOrDefault());
                body = body.Replace("[Name]", pc.Name);
                body = body.Replace("[noofdays]", (accessEntity.NumberOfDays.HasValue?accessEntity.NumberOfDays.Value:0).ToString());
                body = body.Replace("[purposeofvisit]", dc.PurposeToVisits.Where(o=>o.ID == accessEntity.PurposeOfVisitID).Select(o=>o.Name).FirstOrDefault());
                //body = body.Replace("[area]", ddlarea.SelectedItem.Text);
                body = body.Replace("[requesteddate]", string.Format(Deffinity.systemdefaults.GetStringDateformat(), accessEntity.RequestedDate.Value));
                body = body.Replace("[requesttype]", "Access Request");
                body = body.Replace("[sitename]", dc.OurSites.Where(o => o.ID == cd.SiteID).Select(o => o.Name).FirstOrDefault());
                body = body.Replace("[status]", statusname);
                body = body.Replace("[notes]", accessEntity.Notes);
                body = body.Replace("[noteslist]", RequestMailFormat.GetNotesList(callid));
                List<AccessNumbersBasedonDay> acno = AccessNumberBAL.AccessNumber_selectByID(callid);
                StringBuilder stracday = new StringBuilder();
                StringBuilder stracnum = new StringBuilder();
                foreach (AccessNumbersBasedonDay ac in acno)
                {

                    string str = string.Format("Access No. (Day" + " " + ac.Day + ")" + "<br />");
                    stracday.Append(str);
                    string strNum = string.Format(ac.AccessNumber + "<br />");
                    stracnum.Append(strNum);
                }

                body = body.Replace("AccessNo.", HttpUtility.HtmlDecode(stracday.ToString()));
                body = body.Replace("[accessno]", HttpUtility.HtmlDecode(stracnum.ToString()));
                List<Visitor> visitors = new List<Visitor>();

               // visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == callid).ToList();
                if (statusname == "Arrived")
                {
                    visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == callid && p.ArrivalDate.HasValue ).ToList();
                }
                else if (statusname == "Departed")
                {
                    visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == callid && p.DepatureDate.HasValue).ToList();
                }
                else if (statusname == "Expected")
                {
                    visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == callid && !p.ArrivalDate.HasValue && !p.DepatureDate.HasValue).ToList();
                }
                string bodyVisitors = string.Empty;
                StringBuilder visitorTable = new StringBuilder();
                if (visitors.Count > 0)
                {
                    bodyVisitors = em.ReadFile("~/WF/DC/EmailTemplates/AccesControlMailVisitors.htm");

                    string strValue = string.Empty;
                    int intI = 1;

                    foreach (Visitor objVisitor in visitors)
                    {
                        visitorTable.Append(RetRowdata("Visitor " + HttpUtility.HtmlDecode(intI.ToString()),
                               objVisitor.Name, objVisitor.Company, objVisitor.EmailAddress, objVisitor.AccessNo.ToString(),
                               string.Format(objVisitor.ArrivalDate.HasValue ? string.Format("{0:dd/MM/yyyy HH:mm }", objVisitor.ArrivalDate.Value) : string.Empty),
                               string.Format(string.Format(objVisitor.DepatureDate.HasValue ? string.Format("{0:dd/MM/yyyy HH:mm }", objVisitor.DepatureDate.Value) : string.Empty))));
                        intI++;
                    }

                }
                if (visitorTable.Length > 0)
                {
                    bodyVisitors = bodyVisitors.Replace("[rowdata]", visitorTable.ToString());
                    visitorTable.Clear();
                }
                else
                    bodyVisitors = bodyVisitors.Replace("[rowdata]", string.Empty);

                bodyVisitors = bodyVisitors.Replace("[status]", statusname);

                if (bodyVisitors == string.Empty)
                {
                    body = body.Replace("[visitorsdata]", "");
                }
                else
                {
                    body = body.Replace("[visitorsdata]", bodyVisitors);
                }



                List<int> idlist = SecurityAccessMail.GetIds(3); // 3=Acccess control
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
    }
    private string RetRowdata(string str1, string str2, string str3, string str4, string str5, string str6, string str7)
    {
        return string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>", str1, str2, str3, str4, str5, str6, str7);

    }
    public string GetTimeByStatus(int callid, int statusid)
    {
        string retval = "00:00";
        using (DC.DAL.DCDataContext dc = new DCDataContext())
        {
            if (statusid == 19)
            {
                var cjournal = (from p in dc.VisitorsJournals
                                where p.CallID == callid && p.ArriveDate.HasValue
                                orderby p.ArriveDate  descending
                                select p).FirstOrDefault();
                if (cjournal != null)
                {
                    retval = string.Format("{0:hh:mm tt}", cjournal.ArriveDate.Value);
                }
            }
            else if (statusid == 42)
            {

                var cjournal = (from p in dc.VisitorsJournals
                                where p.CallID == callid && p.DepartDate.HasValue
                                orderby p.DepartDate descending
                                select p).FirstOrDefault();
                if (cjournal != null)
                {
                    retval = string.Format("{0:hh:mm tt}", cjournal.DepartDate.Value);
                }
            }
        }
        return retval;
    }
    #endregion
}
 public class NotesList
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public DateTime? DateTime { get; set; }
        public string Notes { get; set; }
    }