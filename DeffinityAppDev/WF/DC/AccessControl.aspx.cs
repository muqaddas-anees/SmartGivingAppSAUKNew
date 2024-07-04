using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;
using DC.BAL;
using DC.DAL;
using System.Web.Services;
using System.Web.Script.Services;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using Location.DAL;
using Location.Entity;
using System.Data;
using System.Collections;
using System.Text;



public partial class DC_AccessControl : System.Web.UI.Page
{

    //private static int cid = 0;
    //private static int count;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (sessionKeys.SID == 9)
        {
            this.Page.MasterPageFile = "~/DeffinityResourceTab.master";
        }
        //else
        //    Response.Redirect("~/Default.aspx");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                //Master.PageHead = "Access Control";
               if (Request.QueryString["callid"] != null)
                {
                    lblTitle.InnerText = "Access Control - Ticket Reference " + Request.QueryString["callid"];
                    int tid = int.Parse(Request.QueryString["callid"]);
                    GetAccessControlDetails(tid);
                    BindAccessNo(tid);//newly added
                    //AccessControlHistory1.DisplayHistory(tid);
                    //AccessControlHistory1.CallID = tid;
                   // AccessControlHistory1.Visible = true;
                   // MakeReadOnlyFields();
                    iframe_DisplayHistory(tid);
                    Disable_Validation();
                    pnlNotes.Visible = true;
                }
                else
                {
                    Change_SubmitButtonImg(true);
                    lblTitle.InnerText = "Access Control ";
                    ccdStatus.SelectedValue = "15";
                    //ccdStatus.Enabled = false;
                    divvisitorsdata.Visible = false;
                    imgbtncpyvstrs.Visible = false;
                    setDefaultSite();
                    txtLoggedDateTime.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                    txtLoggedTime.Text = string.Format("{0:HH:mm}", DateTime.Now);
                }

                List<Visitor> visitorscount = new List<Visitor>();
                visitorscount = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == int.Parse(h_callid.Value)).ToList();
                lblmailerror.Text = visitorscount.Count().ToString();
                //count = int.Parse(lblmailerror.Text);
                cmpRdate.ValueToCompare = DateTime.Now.ToShortDateString();
            }

           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
      
       
    }
    private void iframe_DisplayHistory(int tid)
    {
        iframe1.Attributes.Add("src", string.Format("/WF/DC/HistoryDisplay.aspx?type=accesscontrol&callid={0}&d={1}",tid.ToString(),string.Format("{0:yyyyMMddHHmmss}",DateTime.Now)));
    }
    private void setDefaultSite()
    {
        DefaultData d = DefaultDataBAL.SiteDefaultData_select();
        if (d != null)
        {
            ccdSite.SelectedValue = d.SiteID.ToString();
        }
    }

    

    private void AddAccessControlDetails()
    {
        try
        {
            
            DateTime CurrentDate = DateTime.Now;
            CallDetail cd = new CallDetail();
            cd.RequestTypeID = int.Parse(ddlRtype.SelectedValue);
            cd.SiteID = int.Parse(ddlsite.SelectedValue);
            cd.StatusID = int.Parse(ddlstatus.SelectedValue);
            int sid = int.Parse(ddlstatus.SelectedValue);
            cd.CompanyID = int.Parse(ddlRcmpy.SelectedValue);
            cd.RequesterID = int.Parse(ddlRname.SelectedValue);
            cd.LoggedBy = sessionKeys.UID;
            cd.LoggedDate = DateTime.Now;
            CallDetailsBAL.AddCallDetails(cd);
            AccessControl ac = new AccessControl();
            ac.CallID = cd.ID;
           
            int id = cd.ID;
            ac.RequestedDate = Convert.ToDateTime(txtRdate.Text);
            ac.NumberOfDays = int.Parse(txtNodays.Text.Trim());
            //ac.AreaID = int.Parse(ddlarea.SelectedValue);
            ac.PurposeOfVisitID = int.Parse(ddlvstngpurp.SelectedValue);
            ac.Notes = txtnotes.Text.Trim();
            AccessControlBAL.AccessControlDetails_Insert(ac);
            lblTitle.InnerText = "Access Control - Ticket Reference " + id;
            AddCallDetailsJournal(id, CurrentDate, sid);
            AddAccessControlJournal(id, CurrentDate);
            RandomAccessNumber(id);
            GetAccessControlDetails(id);
            
            PortfolioContact pc = CustomerDetailsBAL.GetPortfolioContactDetailsbyID(int.Parse(ddlRname.SelectedValue));
            string old_email = pc.Email;
            string old_telNo = pc.Telephone;
            if ((old_email != txtRemail.Text) || (old_telNo != txtRtelno.Text))
            {
                CustomerDetailsBAL.Update_ProfileDetails(int.Parse(ddlRname.SelectedValue), sessionKeys.UID, txtRemail.Text, txtRtelno.Text);
            }
            //disable the no days
            //if (cd.ID > 0)
            //{
            //    txtNodays.Enabled = false;
            //}
            
            iframe_DisplayHistory(cd.ID);

            BindAccessNo(id);
           

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void UpdateAccessControlDetails(int callid)
     {
         try
         {
             bool check_visitors = true;
             //check atleast one visitor should be exists in each day
             if (Request.QueryString["callid"] == null)
             {
                 check_visitors = AccessControlBAL.CheckVisitorExist(callid);
             }

             if (check_visitors)
             {
                 DateTime CurrentDate = DateTime.Now;
                 CallDetail cd = CallDetailsBAL.SelectbyId(callid);
                 AccessControl ac = AccessControlBAL.AccessControlDetails_selectByID(callid);
                 bool visitors = VisitorsBAL.CheckVisitorsDepartStatusByCallid(callid);
                 bool nullExists = false;// VisitorsBAL.CheckVisitorsArriveStatusByCallid(callid);

                 if (ddlstatus.SelectedItem.Text == "Arrived" && nullExists == true)
                 {
                     lblstatustext.Text = "Please select the person and mark their corresponding status as arrived.";
                 }
                 else
                 {
                     //get old status
                     int? old_status = cd.StatusID;
                     if (ddlstatus.SelectedValue == "21")
                     {
                         if (visitors)
                         {
                             ccdStatus.SelectedValue = old_status.ToString();
                             cd.StatusID = old_status;
                             int sid = int.Parse(ccdStatus.SelectedValue);
                             lblstatustext.Text = "You cannot select status as closed, as visitors Depart Status is pending.";
                             bool IsCallDetailsChanged = CompareCallDetails(cd, int.Parse(ddlsite.SelectedValue), int.Parse(ddlRtype.SelectedValue), int.Parse(ccdStatus.SelectedValue), int.Parse(ddlRcmpy.SelectedValue), int.Parse(ddlRname.SelectedValue), sessionKeys.UID);
                             if (IsCallDetailsChanged)
                                 AddCallDetailsJournal(callid, CurrentDate, sid);
                         }
                     }

                     if (!visitors || ddlstatus.SelectedValue != "21")
                     {
                         int stid = int.Parse(ddlstatus.SelectedValue);
                         bool IsCallDetailChanged = CompareCallDetails(cd, int.Parse(ddlsite.SelectedValue), int.Parse(ddlRtype.SelectedValue), int.Parse(ddlstatus.SelectedValue), int.Parse(ddlRcmpy.SelectedValue), int.Parse(ddlRname.SelectedValue), sessionKeys.UID);
                         if (IsCallDetailChanged)
                             AddCallDetailsJournal(callid, CurrentDate, stid);
                         cd.StatusID = int.Parse(ddlstatus.SelectedValue);
                         lblstatustext.Text = string.Empty;
                     }

                     cd.SiteID = int.Parse(ddlsite.SelectedValue);
                     cd.RequestTypeID = int.Parse(ddlRtype.SelectedValue);

                     cd.CompanyID = int.Parse(ddlRcmpy.SelectedValue);
                     cd.RequesterID = int.Parse(ddlRname.SelectedValue);
                     //cd.LoggedBy = sessionKeys.UID;
                     //cd.LoggedDate = Convert.ToDateTime(txtLoggedDateTime.Text + " " + (string.IsNullOrEmpty(txtLoggedTime.Text) ? "00:00:00" : txtLoggedTime.Text + ":00"));
                     //bool IsAccessControlDetailsChanged = CompareAccessControlDetails(ac, callid, int.Parse(ddlarea.SelectedValue), txtnotes.Text.Trim(), int.Parse(txtNodays.Text.Trim()), int.Parse(ddlvstngpurp.SelectedValue), Convert.ToDateTime(txtRdate.Text));
                     bool IsAccessControlDetailsChanged = CompareAccessControlDetails(ac, callid, 0, txtnotes.Text.Trim(), int.Parse(txtNodays.Text.Trim()), int.Parse(ddlvstngpurp.SelectedValue), Convert.ToDateTime(txtRdate.Text));

                     CallDetailsBAL.CallDetailsUpdate(cd);

                     ac.RequestedDate = Convert.ToDateTime(txtRdate.Text);
                     ac.NumberOfDays = int.Parse(txtNodays.Text.Trim());
                     //ac.AreaID = int.Parse(ddlarea.SelectedValue);
                     ac.PurposeOfVisitID = int.Parse(ddlvstngpurp.SelectedValue);
                     ac.Notes = txtnotes.Text.Trim();
                     if (IsAccessControlDetailsChanged)
                         AddAccessControlJournal(callid, CurrentDate);
                     AccessControlBAL.AccessControlDetails_update(ac);


                     int vistors_count = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == int.Parse(h_callid.Value)).Count();

                     if (vistors_count > 0)
                     {
                         h_hidevisitors.Value = callid.ToString();
                         if (Request.QueryString["callid"] == null)
                         {
                             sendmail(true, "Expected", CurrentDate);
                             DistributionMail();
                             Response.Redirect(string.Format("~/WF/DC/AccessControl.aspx?callid={0}", int.Parse(h_callid.Value)), false);

                         }
                         else if (old_status != cd.StatusID)
                         {
                             // send updated ticket
                             sendmail(false, ddlstatus.SelectedItem.Text, CurrentDate);
                         }
                         //DistributionMail();
                         if (string.IsNullOrEmpty(lblstatustext.Text) && Request.QueryString["callid"] != null)
                         {
                             Response.Redirect(string.Format("~/WF/DC/AccessControl.aspx?callid={0}", Request.QueryString["callid"].ToString()), false);
                         }
                     }
                 }

                 //AccessControlHistory1.DisplayHistory(callid);
                 iframe_DisplayHistory(callid);

             }
             else {

                 lblstatustext.Text = "Please add atleast one visitor for each access number.";
             }
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
    


    }
    

   

    private bool GetNumberOfDaysChanged(AccessControl ac,int nod)
    { 
       bool IsChanged = false;
       try
       {
           AccessControl pastDetails = ac;
           if (pastDetails.NumberOfDays != nod)
               IsChanged = true;

           
       }
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
       return IsChanged;
    }

    private void GetAccessControlDetails(int callid)
    {
        try
        {

            CallDetail cd = CallDetailsBAL.SelectbyId(callid);
            AccessControl ac = AccessControlBAL.AccessControlDetails_selectByCallID(callid);
            if ((cd != null) && (ac != null))
            {
                Change_SubmitButtonImg(false);
                h_callid.Value = callid.ToString();
                if (Request.QueryString["callid"] != null)
                {
                    h_cid.Value = callid.ToString();
                    h_hidevisitors.Value = callid.ToString();
                    
                }
               
                //cid = int.Parse(h_callid.Value);
                ccdCompny.SelectedValue = Convert.ToString(cd.CompanyID);
                ccdreqname.DataBind();
                ccdreqname.SelectedValue = Convert.ToString(cd.RequesterID);
                ccdSite.DataBind();
                ccdSite.SelectedValue = Convert.ToString(cd.SiteID);
                //Area
                //ccdarea.SelectedValue = Convert.ToString(ac.AreaID);
                txtLoggedDateTime.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), cd.LoggedDate.ToString().Remove(10));
                txtLoggedTime.Text = string.Format("{0:HH:mm}", cd.LoggedDate);
                ccdType.SelectedValue = Convert.ToString(cd.RequestTypeID);
                ccdStatus.SelectedValue = Convert.ToString(cd.StatusID);
                ccdvp.SelectedValue = Convert.ToString(ac.PurposeOfVisitID);
                txtRdate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ac.RequestedDate).Replace("00:00:00", " "));
                txtNodays.Text = Convert.ToString(ac.NumberOfDays);
                txtnotes.Text = ac.Notes;
                divvisitorsdata.Visible = true;
              
                if (h_callid.Value != "0")
                {
                    if (QueryStringValues.CallID == 0)
                        imgbtncpyvstrs.Visible = true;
                    
                    imgDateRequested.Visible = false;
                    calSeheduledDate.Enabled = false;
                    txtNodays.Enabled = false;
                    imgbtnCancel.Visible = false;
                }
                else
                {
                    imgbtncpyvstrs.Visible = false;
                }
               // AccessControlHistory1.DisplayHistory(callid);
               
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

   
    protected void imgbtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (h_callid.Value == "0")
            {
                AddAccessControlDetails();
            }
            else
            {
                
                UpdateAccessControlDetails(int.Parse(h_callid.Value));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void RandomAccessNumber(int callid)
    {
        try
        {
            int StartingNumber = 1;
            int LastNumber = int.Parse(txtNodays.Text);
            int amountOfRandomNumbers = LastNumber;

            List<int> possibleNumbers = new List<int>();
            for (int i = StartingNumber; i <= LastNumber; i++)
                possibleNumbers.Add(i);
            AccessNumber acno = AccessNumberBAL.AccessNumber_select();
            AccessNumbersBasedonDay maxacno = AccessNumberBAL.SelectMaximumAccessNumber();
            int accessno;
            if (maxacno == null)
            {
                accessno = int.Parse(acno.AccessNo);
            }
            else
            {
                accessno = int.Parse(maxacno.AccessNumber);
            }
            
            int[] po = possibleNumbers.ToArray<int>();

            foreach (int va in po)
            {

                int ac = accessno + va;
                AccessNumbersBasedonDay acday = new AccessNumbersBasedonDay();
                acday.AccessNumber = ac.ToString();
                //acday.AccessNo = ac.ToString();
                acday.CallID = callid;
                acday.Day = va;
                AccessNumberBAL.AccessNumber_Insert(acday);

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    #region Send mail
    //19	Arrived
    //42	Departed
    public string GetTimeByStatus(int callid, int statusid)
    {
        string retval = "00:00";
        using (DC.DAL.DCDataContext dc = new DCDataContext())
        {
            if (statusid == 19)
            {
                var cjournal = (from p in dc.VisitorsJournals
                                where p.CallID == callid && p.ArriveDate.HasValue
                                orderby p.ArriveDate descending
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

    private void sendmail(bool check_insert, string StatusName, DateTime CurrentDate)
     {
        try
        {
            
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();
            List<string> site = new List<string>();
            //ProjectPortfolio cmpy = ProjectPortFolioBAL.SelectbyId(int.Parse(ddlRcmpy.SelectedValue));
           // site = ws.GetSiteDetailsbyId(int.Parse(ddlsite.SelectedValue));
            EmailFooter ef = new EmailFooter();
            AccessControlEmail ace = DefaultsOfAccessControl.AccessEmail_select();
            ef = FooterEmail.EmailFooter_selectByID(int.Parse(ddlRtype.SelectedValue));
            PortfolioContact pc = ws.GetContactDetails(int.Parse(ddlRname.SelectedValue));
            string subject = string.Empty;
            var statusname = ddlstatus.SelectedItem.Text;
            subject = "Ticket Reference: " + int.Parse(h_callid.Value);
            Emailer em = new Emailer();
            string body = em.ReadFile("~/WF/DC/EmailTemplates/AccessControlMail.htm");

            if (StatusName == "Expected" && check_insert)
            { body = body.Replace("[mail_head]", "Access Request"); }
            else if (StatusName == "Closed")
            {
                body = body.Replace("[mail_head]", "Access Request Closed");
                body = body.Replace("Thank you for raising a Access Request, details of your request can be found below:", "The following Access Request has now been closed.");
            }
            else
            {
                body = body.Replace("[mail_head]", "Access Request Update");
                body = body.Replace("Thank you for raising a Access Request, details of your request can be found below:", "Please see below an update on your Access Request:");
            }

            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            body = body.Replace("[user]", ddlRname.SelectedItem.Text);
            body = body.Replace("[requestercompany]", ddlRcmpy.SelectedItem.Text);
            body = body.Replace("[requestername]", ddlRname.SelectedItem.Text);
            body = body.Replace("[sitename]", ddlsite.SelectedItem.Text);
            body = body.Replace("[status]", ddlstatus.SelectedItem.Text);
            body = body.Replace("[requesteddate]", txtRdate.Text);
            body = body.Replace("[noofdays]", txtNodays.Text);
            //body = body.Replace("[area]", ddlarea.SelectedItem.Text); 
            body = body.Replace("[purposeofvisit]", ddlvstngpurp.SelectedItem.Text);
            body = body.Replace("[requesttype]", "Access Request");
            //19	Arrived
            //42	Departed
            //if(ddlstatus.SelectedItem.Text.ToLower() == "arrived")
            //    body = body.Replace("[statustime]", string.Format("<tr><td style='padding-top: 10px;' width='20%' valign='top' >Arrived <b>:</b></td><td style='padding-top: 10px;'><b>{0}</b></td></tr>", GetTimeByStatus(int.Parse(h_callid.Value), 19)));
            //else if(ddlstatus.SelectedItem.Text.ToLower() == "departed")
            //    body = body.Replace("[statustime]", string.Format("<tr><td style='padding-top: 10px;' width='20%' valign='top' >Departed <b>:</b></td><td style='padding-top: 10px;'><b>{0}</b></td></tr>", GetTimeByStatus(int.Parse(h_callid.Value), 42)));
            //else
             body = body.Replace("[statustime]","");
            body = body.Replace("[notes]", txtnotes.Text);
            body = body.Replace("[noteslist]", RequestMailFormat.GetNotesList(int.Parse(h_callid.Value)));
            List<AccessNumbersBasedonDay> acno = AccessNumberBAL.AccessNumber_selectByID(int.Parse(h_callid.Value));
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
            //statusname is arrived
            //if the visitors are 
            if (statusname == "Arrived")
            {
                visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == int.Parse(h_callid.Value) && p.ArrivalDate.HasValue ).ToList();
            }
            else if (statusname == "Departed")
            {
                visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == int.Parse(h_callid.Value) && p.DepatureDate.HasValue).ToList();
            }
            else if (statusname == "Expected")
            {
                visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == int.Parse(h_callid.Value) && !p.ArrivalDate.HasValue && !p.DepatureDate.HasValue).ToList();
            }
            string bodyVisitors = string.Empty;
            StringBuilder visitorTable = new StringBuilder();
            if (visitors.Count > 0)
            {
                bodyVisitors = em.ReadFile("EmailTemplates/AccesControlMailVisitors.htm");
                string strValue = string.Empty;
                int intI = 1;
                foreach (Visitor objVisitor in visitors)
                {
                   visitorTable.Append( RetRowdata("Visitor " + HttpUtility.HtmlDecode(intI.ToString()),
                        objVisitor.Name,objVisitor.Company,objVisitor.EmailAddress,objVisitor.AccessNo.ToString(),
                        string.Format(objVisitor.ArrivalDate.HasValue ? string.Format("{0:dd/MM/yyyy HH:mm }", objVisitor.ArrivalDate.Value): string.Empty),
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
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private string RetRowdata(string str1,string str2,string str3,string str4,string str5,string str6,string str7)
    {
        return string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>", str1, str2, str3, str4, str5, str6, str7);

    }
    #endregion

    #region Distribution Mail
    private void DistributionMail()
    {
        Emailer em = new Emailer();
        string fromemailid = Deffinity.systemdefaults.GetFromEmail();
        string subject = "Ticket Reference: " + int.Parse(h_callid.Value);
        var statusname = ddlstatus.SelectedItem.Text;
        //string subject = string.Format("Ticket Reference: {4} {0} {1} has requested access control on {2} at {3}", ddlRcmpy.SelectedItem.Text, ddlRname.SelectedItem.Text, DateTime.Now.ToString(Deffinity.systemdefaults.GetDateformat()), DateTime.Now.ToString("HH:mm"), h_callid.Value);
        string body = em.ReadFile("~/WF/DC/EmailTemplates/AccessControlDistributionListMail.htm");
        body = body.Replace("[mail_head]", "Access Request");
        body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
        body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
        body = body.Replace("[Customer]", ddlRcmpy.SelectedItem.Text);
        body = body.Replace("[Name]", ddlRname.SelectedItem.Text);
        body = body.Replace("[noofdays]", txtNodays.Text);
        body = body.Replace("[purposeofvisit]", ddlvstngpurp.SelectedItem.Text); 
        //body = body.Replace("[area]", ddlarea.SelectedItem.Text);
        body = body.Replace("[requesteddate]", txtRdate.Text);
        body = body.Replace("[requesttype]", "Access Request");
        body = body.Replace("[sitename]", ddlsite.SelectedItem.Text);
        body = body.Replace("[status]", statusname );
        body = body.Replace("[notes]", txtnotes.Text);
        body = body.Replace("[noteslist]", RequestMailFormat.GetNotesList(int.Parse(h_callid.Value)));
        List<AccessNumbersBasedonDay> acno = AccessNumberBAL.AccessNumber_selectByID(int.Parse(h_callid.Value));
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
        if(statusname == "Arrived")
        {
            visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == int.Parse(h_callid.Value) && p.ArrivalDate.HasValue ).ToList();
        }
        else if(statusname == "Departed")
        {
             visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == int.Parse(h_callid.Value) && p.DepatureDate.HasValue).ToList();
        }
        else if (statusname == "Expected")
        {
            visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == int.Parse(h_callid.Value) && !p.ArrivalDate.HasValue && !p.DepatureDate.HasValue).ToList();
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
        //[status]
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
    #endregion

    private bool CompareCallDetails(CallDetail cd, int sid, int rtid, int stid, int cid, int rid, int lid)
     {
        bool IsChanged = false;
        try
        {
            CallDetail pastDetails = cd;
            if (pastDetails.SiteID != sid)
                IsChanged = true;
            else if (pastDetails.RequestTypeID != rtid)
                IsChanged = true;
            else if (pastDetails.StatusID != stid)
                IsChanged = true;
            else if (pastDetails.CompanyID != cid)
                IsChanged = true;
            else if (pastDetails.RequesterID != rid)
                IsChanged = true;
            else if (pastDetails.LoggedBy != lid)
                IsChanged = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return IsChanged;
    }

    private bool CompareAccessControlDetails(AccessControl ac, int cid, int aid, string notes, int noOfdays, int pvid, DateTime Rdate)
    {
        bool IsChanged = false;
        try
        {
            AccessControl PreviousDetails = ac;
            if (PreviousDetails.CallID != cid)
                IsChanged = true;
            //else if (PreviousDetails.DeliveryNumber != Dno)
            //    IsChanged = true;
            else if (PreviousDetails.AreaID!= aid)
                IsChanged = true;
            else if (PreviousDetails.Notes != notes)
                IsChanged = true;
            else if (PreviousDetails.NumberOfDays != noOfdays)
                IsChanged = true;
            else if (PreviousDetails.PurposeOfVisitID != pvid)
                IsChanged = true;
            else if (PreviousDetails.RequestedDate!= Rdate)
                IsChanged = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return IsChanged;
    }

    private void AddAccessControlJournal(int acid, DateTime ModifiedDate)
    {
        try
        {
           
            DC.SRV.WebService ws = new DC.SRV.WebService();
            AccessControlJournal acJ = new AccessControlJournal();
            acJ.CallID = acid;
            //acJ.AreaID = int.Parse(ddlarea.SelectedValue);
            acJ.ModifiedBy = sessionKeys.UID;
            acJ.ModifiedDate = ModifiedDate;
            acJ.Notes = txtnotes.Text.Trim();
            acJ.NumberOfDays =int.Parse(txtNodays.Text.Trim());
            acJ.PurposeOfVisit = int.Parse(ddlvstngpurp.SelectedValue);
            acJ.RequestedDate = Convert.ToDateTime(txtRdate.Text.Trim());
            acJ.VisibleToCustomer = false;
            ws.InsertAccessControlJournal(acJ);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void AddCallDetailsJournal(int callid, DateTime ModifiedDate, int oldstatus)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            CallDetailsJournal cdj = new CallDetailsJournal();
            cdj.CallID = callid;
            cdj.SiteID = int.Parse(ddlsite.SelectedValue);
            cdj.RequestTypeID = int.Parse(ddlRtype.SelectedValue);
            cdj.StatusID = oldstatus;
            cdj.CompanyID = int.Parse(ddlRcmpy.SelectedValue);
            cdj.RequesterID = int.Parse(ddlRname.SelectedValue);
            cdj.LoggedDate = Convert.ToDateTime(txtLoggedDateTime.Text + " " + (string.IsNullOrEmpty(txtLoggedTime.Text) ? "00:00:00" : txtLoggedTime.Text + ":00"));
            
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

    #region Clear
    private void Clear()
    {
        try
        {
            ccdCompny.SelectedValue = "0";
            ccdreqname.SelectedValue = "0";
            //Area
            //ccdarea.SelectedValue = "0";
            ccdType.SelectedValue = "0";
            ccdSite.SelectedValue = "0";
            ccdStatus.SelectedValue = "0";
            ccdvp.SelectedValue = "0";
            // txtdelno.Text = string.Empty;
            txtNodays.Text = string.Empty;
            txtnotes.Text = string.Empty;
            txtRdate.Text = string.Empty;
            txtRemail.Text = string.Empty;
            txtRtelno.Text = string.Empty;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

   
    #region Bind Visitors

    [WebMethod(EnableSession = true)]
    public static object BindVisitors(string an, string callid, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    { 
        try
        {
            if (HttpContext.Current.Request.QueryString["callid"] != null)
                callid = HttpContext.Current.Request.QueryString["callid"].ToString();

            DCDataContext db = new DCDataContext();
            List<VisitorDetails> visitors = new List<VisitorDetails>();

            visitors = VisitorsBAL.BindVisitors(an).Where(p => p.CallID == Convert.ToInt32(callid)).ToList();
            
            if (jtSorting.Equals("Name ASC"))
            {
                visitors = visitors.OrderBy(o => o.Name).ToList();
            }
            else if (jtSorting.Equals("Name DESC"))
            {
                visitors = visitors.OrderByDescending(o => o.Name).ToList();
            }
            else if (jtSorting.Equals("CallID ASC"))
            {
                visitors = visitors.OrderBy(o => o.CallID).ToList();
            }
            else if (jtSorting.Equals("CallID DESC"))
            {
                visitors = visitors.OrderByDescending(o => o.CallID).ToList();
            }
            else if (jtSorting.Equals("ArriveStatus ASC"))
            {
                visitors = visitors.OrderBy(o => o.ArriveStatus).ToList();
            }
            else if (jtSorting.Equals("ArriveStatus DESC"))
            {
                visitors = visitors.OrderByDescending(o => o.ArriveStatus).ToList();
            }
            else if (jtSorting.Equals("Company ASC"))
            {
                visitors = visitors.OrderBy(o => o.Company).ToList();
            }
            else if (jtSorting.Equals("Company DESC"))
            {
                visitors = visitors.OrderByDescending(o => o.Company).ToList();
            }
            else if (jtSorting.Equals("EmailAddress ASC"))
            {
                visitors = visitors.OrderBy(o => o.EmailAddress).ToList();
            }
            else if (jtSorting.Equals("EmailAddress DESC"))
            {
                visitors = visitors.OrderByDescending(o => o.EmailAddress).ToList();
            }
            else if (jtSorting.Equals("PhoneNumber ASC"))
            {
                visitors = visitors.OrderBy(o => o.PhoneNumber).ToList();
            }
            else if (jtSorting.Equals("PhoneNumber DESC"))
            {
                visitors = visitors.OrderByDescending(o => o.PhoneNumber).ToList();
            }
            else if (jtSorting.Equals("PhotoID ASC"))
            {
                visitors = visitors.OrderBy(o => o.PhotoID).ToList();
            }
            else if (jtSorting.Equals("PhotoID DESC"))
            {
                visitors = visitors.OrderByDescending(o => o.PhotoID).ToList();
            }
            else if (jtSorting.Equals("ArrivalDate ASC"))
            {
                visitors = visitors.OrderBy(o => o.ArrivalDate).ToList();
            }
            else if (jtSorting.Equals("ArrivalDate DESC"))
            {
                visitors = visitors.OrderByDescending(o => o.ArrivalDate).ToList();
            }
            else if (jtSorting.Equals("DepatureDate ASC"))
            {
                visitors = visitors.OrderBy(o => o.DepatureDate).ToList();
            }
            else if (jtSorting.Equals("DepatureDate DESC"))
            {
                visitors = visitors.OrderByDescending(o => o.DepatureDate).ToList();
            }
            else
            {
                visitors = visitors.OrderBy(o => o.CallID).ToList();
            }

            var result = visitors.Skip(jtStartIndex).Take(jtPageSize).ToList();
            return new { Result = "OK", Records = result, TotalRecordCount = visitors.Count() };
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
    #endregion
    public class AccessControlNumber
    {

        public int ID { get; set; }
        public string AccessNumber { get; set; }
        public string AccessNo { get; set; }
        public int? CallID { get; set; }
        public int? Day { get; set; }
        public string Number { get; set; }
        public string Noshow { get; set; }
    }

    #region Bind Access Numbers
    [WebMethod(EnableSession = true)]
    public static object BindAccessNumber(string callid, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {
            if (HttpContext.Current.Request.QueryString["callid"] != null)
                callid = HttpContext.Current.Request.QueryString["callid"].ToString();
           
             DCDataContext dc = new DCDataContext();
             List<AccessControlNumber> acno = new List<AccessControlNumber>();
             acno = (from a in dc.AccessNumbersBasedonDays
                       where a.CallID == Convert.ToInt32(callid)
                       orderby a.AccessNumber ascending

                       select new AccessControlNumber
                       {
                           ID = a.ID,
                           CallID = a.CallID,
                           AccessNumber = a.AccessNumber,
                           AccessNo = a.AccessNumber,
                           Day = a.Day,
                           Number = string.Empty,
                           //Noshow = string.Empty
                       }).ToList();


            if (jtSorting.Equals("AccessNumber ASC"))
            {
                acno = acno.OrderBy(a => a.AccessNumber).ToList();
            }
            else if (jtSorting.Equals("AccessNumber DESC"))
            {
                acno = acno.OrderByDescending(a => a.AccessNumber).ToList();
            }
            if (jtSorting.Equals("AccessNo ASC"))
            {
                acno = acno.OrderBy(a => a.AccessNo).ToList();
            }
            else if (jtSorting.Equals("AccessNo DESC"))
            {
                acno = acno.OrderByDescending(a => a.AccessNo).ToList();
            }
            if (jtSorting.Equals("Day ASC"))
            {
                acno = acno.OrderBy(a => a.Day).ToList();
            }
            else if (jtSorting.Equals("Day DESC"))
            {
                acno = acno.OrderByDescending(a => a.Day).ToList();
            }
            else
            {
                acno = acno.OrderBy(a => a.CallID).ToList();
            }

            
            return new { Result = "OK", Records = acno, TotalRecordCount = acno.Count() };
           
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
    public void BindAccessNo(int id)
    {
        

        DCDataContext dc = new DCDataContext();
        List<AccessControlNumber> acno = new List<AccessControlNumber>();
        acno = (from a in dc.AccessNumbersBasedonDays
                where a.CallID == id
                orderby a.AccessNumber ascending

                select new AccessControlNumber
                {
                    ID = a.ID,
                    CallID = a.CallID,
                    AccessNumber = a.AccessNumber,
                    AccessNo = a.AccessNumber,
                    Day = a.Day,
                    Number = string.Empty,
                    //Noshow = string.Empty
                }).ToList();

        repeter.DataSource = acno;
        repeter.DataBind();
    }

    #endregion

    #region create visitor
    [WebMethod(EnableSession = true)]
    public static object Create(Visitor record, string accno, string callid)
    {
        try
        {

            DCDataContext db = new DCDataContext();
            Visitor v = new Visitor();
            VisitorsJournal vj = new VisitorsJournal();
            v.CallID = Convert.ToInt32(callid);
            v.Name = record.Name;
            v.EmailAddress = record.EmailAddress;
            v.Company = record.Company;
            v.ArriveStatus = true;
            v.DepartStatus = false;
            v.PhotoID = false;
            v.AccessNo = accno;
            v.NoShow = false;
            //v.Day = day;
            v.PhoneNumber = record.PhoneNumber;
            vj.ArriveStatus = true;
            vj.DepartStatus = false;
            db.Visitors.InsertOnSubmit(v);
            vj.CallID = Convert.ToInt32(callid);
            db.SubmitChanges();
            vj.Name = v.Name;
            vj.Company = v.Company;
            vj.EmailAddress = v.EmailAddress;
            vj.VisibleToCustomer = false;
            vj.PhoneNumber = v.PhoneNumber;
            if (v.ArrivalDate != null)
                vj.ArriveDate = v.ArrivalDate;
            if (v.DepatureDate != null)
                vj.DepartDate = v.DepatureDate;
            vj.VisitorID = v.ID;
            vj.PhotoID = v.PhotoID;
            vj.AccessNo = accno;
            vj.ModifiedBy = sessionKeys.UID;
            vj.ModifiedDate = DateTime.Now;
            vj.NoShow = false;
            db.VisitorsJournals.InsertOnSubmit(vj);
            db.SubmitChanges();
            return new { Result = "OK", Record = v };
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }

    }
    #endregion

    #region Update visitor
    [WebMethod(EnableSession = true)]
    public static object Update(Visitor record)
    {
        try
        {

            DCDataContext db = new DCDataContext();
            Visitor v = db.Visitors.Where(p => p.ID == record.ID).SingleOrDefault();
            VisitorsJournal vj = new VisitorsJournal();
            if (v.ArrivalDate != null)
                vj.ArriveDate = v.ArrivalDate;
            if (v.DepatureDate != null)
                vj.DepartDate = v.DepatureDate;
            vj.ArriveStatus = v.ArriveStatus;
            vj.DepartStatus = v.DepartStatus;
            vj.PhotoID = v.PhotoID;
            vj.AccessNo = v.AccessNo;
            vj.ModifiedBy = sessionKeys.UID;
            vj.ModifiedDate = DateTime.Now;
            v.Company = record.Company;
            v.EmailAddress = record.EmailAddress;
            v.Name = record.Name;
            v.PhoneNumber = record.PhoneNumber;
            vj.VisitorID = v.ID;
            vj.CallID = v.CallID;
            vj.VisibleToCustomer = false;
            vj.Name = v.Name;
            vj.Company = v.Company;
            vj.EmailAddress = v.EmailAddress;
            vj.PhoneNumber = v.PhoneNumber;
            vj.NoShow = v.NoShow;
            db.VisitorsJournals.InsertOnSubmit(vj);
            db.SubmitChanges();
            return new { Result = "OK" };
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
      
    }

    #endregion
    
    #region Delete visitor
    [WebMethod(EnableSession = true)]
    public static object Delete(int ID)
    {
        try
        {
            DCDataContext db = new DCDataContext();
            var result = (from p in db.Visitors
                          where p.ID == ID
                          select p).FirstOrDefault();

            db.Visitors.DeleteOnSubmit(result);
            db.SubmitChanges();
            return new { Result = "OK" };
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }

    }
    #endregion

    #region Check PhotoID
    [WebMethod(EnableSession = true)]
    public static object PhotoID(int ID)
    {
        try
        {

            DCDataContext db = new DCDataContext();
            Visitor visitor = db.Visitors.Where(p => p.ID == ID).FirstOrDefault();
            VisitorsJournal vj = new VisitorsJournal();
            string check = string.IsNullOrEmpty(visitor.PhotoID.ToString()) ? "false" : visitor.PhotoID.ToString();
            bool photo = bool.Parse(check);

            if (photo == true)
            {
                photo = false;
            }
            else
            {
                photo = true;
            }
            vj.CallID = visitor.CallID;
            vj.VisitorID = visitor.ID;
            vj.PhotoID = photo;
            vj.Name = visitor.Name;
            vj.AccessNo = visitor.AccessNo;
            vj.Company = visitor.Company;
            vj.EmailAddress = visitor.EmailAddress;
            vj.PhoneNumber = visitor.PhoneNumber;
            if(visitor.ArrivalDate != null)
                vj.ArriveDate = visitor.ArrivalDate; 
            if(visitor.DepatureDate != null)
                vj.DepartDate = visitor.DepatureDate; 
            vj.ArriveStatus = visitor.ArriveStatus;
            vj.DepartStatus = visitor.DepartStatus;
            vj.VisibleToCustomer = false;
            vj.NoShow = visitor.NoShow;
            vj.ModifiedBy = sessionKeys.UID;
            vj.ModifiedDate = DateTime.Now;
            visitor.PhotoID = photo;
            db.VisitorsJournals.InsertOnSubmit(vj);
            db.SubmitChanges();
            return new { Result = "OK", Options = visitor };
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
    #endregion

    #region Arrive Status

    [WebMethod(EnableSession = true)]
    public static object ArriveStatus(int ID)
    {
        try
        {
            var cdatetime = DateTime.Now;
            DCDataContext db = new DCDataContext();
            Visitor visitor = db.Visitors.Where(p => p.ID == ID).FirstOrDefault();
            VisitorsJournal vjournal = new VisitorsJournal();
            vjournal.ArriveDate = cdatetime;
            vjournal.ArriveStatus = false;
            vjournal.DepartStatus = true;
            vjournal.VisitorID = visitor.ID;
            vjournal.CallID = visitor.CallID;
            vjournal.Name = visitor.Name;
            vjournal.AccessNo = visitor.AccessNo;
            vjournal.VisibleToCustomer = false;
            vjournal.NoShow = false;
            vjournal.Company = visitor.Company;
            vjournal.EmailAddress = visitor.EmailAddress;
            vjournal.PhoneNumber = visitor.PhoneNumber;
            if(visitor.DepatureDate != null)
                vjournal.DepartDate = visitor.DepatureDate;
            vjournal.PhotoID = visitor.PhotoID;
            vjournal.ModifiedBy = sessionKeys.UID;
            vjournal.ModifiedDate = cdatetime;
            visitor.ArriveStatus = false;
            visitor.DepartStatus = true;
            visitor.NoShow = false;
            visitor.ArrivalDate = cdatetime;
            db.VisitorsJournals.InsertOnSubmit(vjournal);
            db.SubmitChanges();

            return "";
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
    #endregion
    [WebMethod(EnableSession = true)]
    public static string PhotoStatus(int ID)
    {
        try
        {
            DCDataContext db = new DCDataContext();
            Visitor visitor = db.Visitors.Where(p => p.ID == ID).FirstOrDefault();
            return (visitor.PhotoID.HasValue?visitor.PhotoID.Value:false).ToString();
        }
        catch (Exception ex)
        {
            return (false).ToString();
        }

    }

    #region Depart Status
    [WebMethod(EnableSession = true)]
    public static object DepartStatus(int ID)
    {
        try
        {
            using (DCDataContext db = new DCDataContext())
            {
                bool isChanged = false;
                int callid = 0;
                string currentAccessno = string.Empty;
                Visitor visitor = db.Visitors.Where(p => p.ID == ID).FirstOrDefault();
                if (visitor != null)
                {
                    currentAccessno = visitor.AccessNo;
                    callid = visitor.CallID.Value;
                    VisitorsJournal Vjournal = new VisitorsJournal();
                    Vjournal.DepartDate = DateTime.Now;
                    Vjournal.ArriveStatus = true;
                    Vjournal.DepartStatus = false;
                    Vjournal.VisitorID = visitor.ID;
                    Vjournal.CallID = visitor.CallID;
                    Vjournal.VisibleToCustomer = false;
                    Vjournal.Name = visitor.Name;
                    Vjournal.AccessNo = visitor.AccessNo;
                    Vjournal.Company = visitor.Company;
                    Vjournal.EmailAddress = visitor.EmailAddress;
                    Vjournal.PhoneNumber = visitor.PhoneNumber;
                    Vjournal.NoShow = visitor.NoShow;
                    if (visitor.ArrivalDate != null)
                        Vjournal.ArriveDate = visitor.ArrivalDate;
                    Vjournal.PhotoID = visitor.PhotoID;
                    Vjournal.ModifiedBy = sessionKeys.UID;
                    Vjournal.ModifiedDate = DateTime.Now;
                    visitor.ArriveStatus = true;
                    visitor.DepartStatus = false;
                    visitor.DepatureDate = DateTime.Now;
                    db.VisitorsJournals.InsertOnSubmit(Vjournal);
                    db.SubmitChanges();
                    isChanged = true;
                }

                if(isChanged)
                {
                    //get total number of access numbers
                    var Visotorlist = db.Visitors.Where(o => o.CallID == callid && !o.DepatureDate.HasValue && o.NoShow == false).ToList();
                    //if more days are exists with out filling the depart status then set status of request as 'Expected'
                    var cvcount = Visotorlist.Where(o => o.AccessNo == currentAccessno).Count();
                    if (Visotorlist.Count() > 0 && cvcount == 0)
                    {
                        //set the request status to Expected
                        var cd = db.CallDetails.Where(o => o.ID == callid).FirstOrDefault();
                        //15	Expected
                        var statusID = 15;
                        cd.StatusID = statusID;
                        var cj = new CallDetailsJournal();
                        cj.CallID = cd.ID;
                        cj.CompanyID = cd.CompanyID;
                        cj.LoggedBy = sessionKeys.UID;
                        cj.ModifiedBy = sessionKeys.UID;
                        cj.ModifiedDate = DateTime.Now;
                        cj.RequesterID = cd.RequesterID;
                        cj.RequestTypeID = cd.RequestTypeID;
                        cj.SiteID = cd.SiteID;
                        cj.StatusID = statusID;
                        cj.VisibleToCustomer = false;
                        db.CallDetailsJournals.InsertOnSubmit(cj);
                        db.SubmitChanges();
                    }
                   

                }
                //check current day all departed status or active

            }
            return "";
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
    #endregion

    //#region Disable Arrive and Depart Status
    //[WebMethod(EnableSession = true)]
    //public static object DisableArriveAndDepartStatus(string acno)
    //{
    //    try
    //    {
    //        DCDataContext db = new DCDataContext();
    //        List<Visitor> visitors = db.Visitors.Where(p => p.AccessNo == acno).ToList();
    //        if (visitors.Count > 0)
    //        {
    //            foreach (Visitor v in visitors)
    //            {
    //                if (v.ArriveStatus != false || v.DepartStatus != false)
    //                {
    //                    v.ArriveStatus = false;
    //                    v.DepartStatus = false;

    //                    int visitorid = v.ID;
    //                    VisitorsJournal vj = VisitorsBAL.VisitorsJournal_selectByVisitorID(visitorid);
    //                    vj.AccessNo = v.AccessNo;
    //                    
    //                    vj.ArriveStatus = false;
    //                    vj.DepartStatus = false;
    //                    vj.CallID = v.CallID;
    //                    vj.Company = v.Company;
    //                    vj.EmailAddress = v.EmailAddress;
    //                    vj.ModifiedDate = DateTime.Now;
    //                    vj.Name = v.Name;
    //                    vj.PhoneNumber = v.PhoneNumber;
    //                    vj.VisitorID = v.ID;
    //                    vj.PhotoID = v.PhotoID;
    //                    vj.ModifiedBy = sessionKeys.UID;

    //                    db.VisitorsJournals.InsertOnSubmit(vj);
    //                    db.SubmitChanges();

    //                }
    //            }
    //        }
    //        return "";
           
    //    }
    //    catch (Exception ex)
    //    {
    //        return new { Result = "ERROR", Message = ex.Message };
    //    }
    //}
    //#endregion


    #region Disable Arrive and Depart Status
    [WebMethod(EnableSession = true)]
    public static object DisableArriveAndDepartStatus(int ID)
    {
        try
        {
            DCDataContext db = new DCDataContext();
            Visitor v = db.Visitors.Where(p => p.ID == ID).FirstOrDefault();
            if (v != null)
            {
                   if (v.ArriveStatus != false || v.DepartStatus != false)
                    {
                        v.ArriveStatus = false;
                        v.DepartStatus = false;

                      //  int visitorid = v.ID;
                        VisitorsJournal vj = VisitorsBAL.VisitorsJournal_selectByVisitorID(ID);
                        vj.AccessNo = v.AccessNo;
                        if (v.ArrivalDate == null)
                            v.ArrivalDate = DateTime.Now;
                        if (v.DepatureDate == null)
                            v.DepatureDate = DateTime.Now;
                        v.NoShow = true;
                        vj.DepartDate = DateTime.Now;
                        vj.ArriveDate = DateTime.Now;
                        vj.ArriveStatus = false;
                        vj.DepartStatus = false;
                        vj.CallID = v.CallID;
                        vj.Company = v.Company;
                        vj.EmailAddress = v.EmailAddress;
                        vj.ModifiedDate = DateTime.Now;
                        vj.Name = v.Name;
                        vj.PhoneNumber = v.PhoneNumber;
                        vj.VisitorID = v.ID;
                        vj.PhotoID = v.PhotoID;
                        vj.ModifiedBy = sessionKeys.UID;
                        vj.NoShow = true;
                        db.VisitorsJournals.InsertOnSubmit(vj);
                        db.SubmitChanges();

                    }
                
            }
            return "";

        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
    #endregion

    protected void imgbtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
            //AccessControlHistory1.DisplayHistory(int.Parse(h_callid.Value));
            iframe_DisplayHistory(int.Parse(h_callid.Value));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imgbtncpyvstrs_Click(object sender, EventArgs e)
    {
        try
        {
        CopyVisitors();
        //AccessControlHistory1.DisplayHistory(int.Parse(h_callid.Value));
        iframe_DisplayHistory(int.Parse(h_callid.Value));
        }
         catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    #region Copy Visitors
    private void CopyVisitors()
    {
        try
        {
            AccessNumbersBasedonDay accnum = AccessNumberBAL.SelectMinimumAccessNumberByCallID(int.Parse(h_callid.Value));
            string minacno = accnum.AccessNumber;
            List<AccessNumbersBasedonDay> acno = AccessNumberBAL.SelectByCallid(int.Parse(h_callid.Value));
            foreach (AccessNumbersBasedonDay a in acno)
            {
                if (a.AccessNumber != minacno)
                {

                    List<Visitor> visitors = VisitorsBAL.SelectVisitorsByCallIDandAccessNo(int.Parse(h_callid.Value), minacno);
                    if (visitors.Count > 0)
                    {
                        Visitor copyvisitor = new Visitor();
                        VisitorsJournal vj = new VisitorsJournal();
                        foreach (Visitor v in visitors)
                        {
                            bool CheckVisitorsExist = VisitorsBAL.CheckVisitorsExist(v.Name, int.Parse(h_callid.Value), a.AccessNumber);
                            if (!CheckVisitorsExist)
                            {
                                copyvisitor.Name = v.Name;
                                copyvisitor.Company = v.Company;
                                copyvisitor.EmailAddress = v.EmailAddress;
                                copyvisitor.PhoneNumber = v.PhoneNumber;
                                copyvisitor.ArriveStatus = true;
                                copyvisitor.DepartStatus = false;
                                copyvisitor.PhotoID = false;
                                copyvisitor.AccessNo = a.AccessNumber;
                                copyvisitor.CallID = a.CallID;

                                copyvisitor.NoShow = false;
                                
                                VisitorsBAL.Visitors_Insert(copyvisitor);
                                vj.ArriveStatus = true;
                                vj.DepartStatus = false;
                                vj.CallID = int.Parse(h_callid.Value);
                                vj.Name = v.Name;
                                vj.Company = v.Company;
                                vj.EmailAddress = v.EmailAddress;
                                vj.PhoneNumber = v.PhoneNumber;
                                vj.VisitorID = copyvisitor.ID;
                                vj.PhotoID = v.PhotoID;
                                vj.NoShow = false;
                                vj.AccessNo = a.AccessNumber;
                                vj.VisibleToCustomer = false;
                                vj.ModifiedBy = sessionKeys.UID;
                                vj.ModifiedDate = DateTime.Now;
                                VisitorsBAL.VisitorsJournal_Insert(vj);
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
    #endregion

    private void Disable_Validation()
    {
        cmpRdate.EnableClientScript = false;
        cmpRdate.ErrorMessage = string.Empty;
        cmpRdate.Visible = false;
    }


    private void Change_SubmitButtonImg(bool IsChangeImg)
    {
       
        if (IsChangeImg)
        {
            imgbtnSubmit.Text = "Add";
            imgbtnSubmit.ToolTip = "Add Visitors";
        }
        else
            imgbtnSubmit.Text = "Submit";
    }
    protected void repeter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            AccessControlNumber a = e.Item.DataItem as AccessControlNumber;
            //string s= AddScriptBlock(a.AccessNumber, (a.Day.Value - 1).ToString());
           // ((HiddenField)e.Item.FindControl("scriptBind")).Text = s
            //((Literal)e.Item.FindControl("scriptBind")).Text = s;
            //((Button)e.Item.FindControl("btn_subgrid")). = "showAccessnumber(" + a.AccessNumber + "," + (a.Day.Value - 1).ToString() + ");";
        }
    }

    
}