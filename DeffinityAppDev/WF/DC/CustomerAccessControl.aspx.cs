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
using System.Text;

public partial class DC_CustomerAccessControl : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //if (sessionKeys.PortalUser || sessionKeys.SID == 7)
        //    this.Page.MasterPageFile = "~/DeffinityCustomerTab.master";
        //else
        //    Response.Redirect("~/Default.aspx");
    }
    //private static int cid;
    //private static int count;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                if (sessionKeys.PortfolioID != 0)
                {
                    //Master.PageHead = "Customer Access Control";
                    if (Request.QueryString["callid"] != null)
                    {

                        int tid = int.Parse(Request.QueryString["callid"]);
                        if (!CheckValid_User(tid))
                        {
                            Response.Redirect("~/WF/Portal/Home.aspx", false);
                            return;
                        }
                        else
                        {
                            GetRecords(tid);
                            lblTitle.InnerText = "Customer Access Control - Ticket Reference " + Request.QueryString["callid"];
                            BindAccessNo(tid);//newly added
                            divhideControls.Visible = false;
                            imgbtncpyvstrs.Visible = false;
                            lblText.Visible = false;
                            pnlNotes.Visible = true;
                            //MakeReadOnlyFields();
                            //ctr_history.DisplayHistory(tid);
                        }

                    }
                    else
                    {
                        Change_SubmitButtonImg(true);
                        divvisitorsdata.Visible = false;
                        ccdCompny.SelectedValue = sessionKeys.PortfolioID.ToString();
                        lblTitle.InnerText = "Customer Access Control ";
                        ccdreqname.SelectedValue = CustomerDetailsBAL.GetCustomerUser_ContactID(sessionKeys.UID).ToString();
                        ccdStatus.SelectedValue = "15";
                        divhideControls.Visible = true;
                        lblText.Visible = false;
                        ctr_history.Visible = false;
                        DefaultData d = DefaultDataBAL.SiteDefaultData_select();
                        if (d != null)
                        {
                            ccdSite.SelectedValue = d.SiteID.ToString();
                        }
                        //cmpRdate.ValueToCompare = DateTime.Now.ToShortDateString();
                        
                    }
                   
                    List<Visitor> visitorscount = new List<Visitor>();
                    visitorscount = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == int.Parse(h_callid.Value)).ToList();
                    lblmailerror.Text = visitorscount.Count().ToString();
                    //count = int.Parse(lblmailerror.Text);
                    //cmpRdate.ValueToCompare = DateTime.Now.ToShortDateString();
                  
                }
            }
            if (Request.QueryString["callid"] != null)
            {
                ctr_history.DisplayHistory(Convert.ToInt32(Request.QueryString["callid"]));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region check valid user
    private bool CheckValid_User(int cid)
    {
        bool retval = true;
        CallDetail cd = CallDetailsBAL.SelectbyId(cid);
        if (cd != null)
        {
            //check valid user or not
            //get the call related user id
            int current_userID = GetUserID_ByRequester(cd.RequesterID.Value);
            if (sessionKeys.UID != current_userID)
            {
                retval = false;
            }
        }
        return retval;
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
                    Day = a.Day
                   
                    //Noshow = string.Empty
                }).ToList();

        repeter.DataSource = acno;
        repeter.DataBind();
    }
    private static int GetUserID_ByRequester(int RequesterID)
    {
        int current_userID = 0;
        using (PortfolioDataContext pt = new PortfolioDataContext())
        {
            var rval = (from p in pt.PortfolioContactAssociates
                        where p.ContactID == RequesterID
                        select p).FirstOrDefault();
            if (rval != null)
            {
                current_userID = rval.CustomerUserID.Value;
            }
        }
        return current_userID;
    }

    #endregion
   
    private void GetRecords(int callid)
    {
        try
        {
                CallDetail cd = CallDetailsBAL.SelectbyId(callid);
                AccessControl ac = AccessControlBAL.AccessControlDetails_selectByCallID(callid);
                if ((cd != null) && (ac != null))
                {
                    if (Request.QueryString["callid"] != null)
                    {
                        h_cid.Value = callid.ToString();
                    }
                    h_callid.Value = callid.ToString();
                    //cid = int.Parse(h_callid.Value);
                    ccdCompny.SelectedValue = Convert.ToString(cd.CompanyID);
                    ccdSite.DataBind();
                    ccdSite.SelectedValue = Convert.ToString(cd.SiteID);
                    //cmpRdate.ValueToCompare = cd.LoggedDate.Value.ToShortDateString();
                    //Area
                    //ccdarea.SelectedValue = Convert.ToString(ac.AreaID);
                    ccdreqname.SelectedValue = Convert.ToString(cd.RequesterID);
                    ccdType.SelectedValue = Convert.ToString(cd.RequestTypeID);
                    ccdStatus.SelectedValue = Convert.ToString(cd.StatusID);
                    //ddlstatus.Enabled = false;
                    ccdvp.SelectedValue = Convert.ToString(ac.PurposeOfVisitID);
                    txtRdate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ac.RequestedDate).Replace("00:00:00", " "));
                    txtNodays.Text = Convert.ToString(ac.NumberOfDays);
                    //txtnotes.Text = ac.Notes;
                  //  BindAccessNumbers(callid);
                    divvisitorsdata.Visible = true;
                    lblText.Visible = false;

                    if (h_callid.Value != "0")
                    {
                        
                        imgbtncpyvstrs.Visible = true;
                        imgSeheduledDate.Visible = false;
                        calSeheduledDate.Enabled = false;
                        txtNodays.Enabled = false;
                        imgbtnCancel.Visible = false;
                    }
                    else
                    {
                        imgbtncpyvstrs.Visible = false;
                    }
                   
                }
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void AddAccessControlDetails()
    {
        try
        {
            DateTime CurrentDate = DateTime.Now;
            CallDetail cd = new CallDetail();
            DC.SRV.WebService ws = new DC.SRV.WebService();
            PortfolioContact pc = CustomerDetailsBAL.GetPortfolioContactDetailsbyID(int.Parse(ddlRname.SelectedValue));
            UserMgt.Entity.Contractor c = CustomerDetailsBAL.GetContractorDetailsbyID(sessionKeys.UID);
            string old_email = pc.Email;
            string old_telNo = pc.Telephone;
           // int rid = ws.GetContactID();

            cd.RequestTypeID = int.Parse(ddlRtype.SelectedValue);
            cd.SiteID = int.Parse(ddlsite.SelectedValue);
            cd.StatusID = int.Parse(ddlstatus.SelectedValue);
            cd.CompanyID = int.Parse(ddlRcmpy.SelectedValue);
            cd.RequesterID = int.Parse(ddlRname.SelectedValue);
            cd.LoggedBy = sessionKeys.UID;
            cd.LoggedDate = DateTime.Now;
            CallDetailsBAL.AddCallDetails(cd);
            AddCallDetailsJournal(cd, CurrentDate);
            AccessControl ac = new AccessControl();
            ac.CallID = cd.ID;
            int id = cd.ID;
            ac.RequestedDate = Convert.ToDateTime(txtRdate.Text);
            ac.NumberOfDays = int.Parse(txtNodays.Text.Trim());
            //Area
            //ac.AreaID = int.Parse(ddlarea.SelectedValue);
            ac.PurposeOfVisitID = int.Parse(ddlvstngpurp.SelectedValue);
            //ac.Notes = txtnotes.Text.Trim();
            AccessControlBAL.AccessControlDetails_Insert(ac);
            AddCustomerAccessControlJournal(id, CurrentDate);
            lblTitle.InnerText = "Customer Access Control - Ticket Reference " + id;
            RandomAccessNumber(id);
            if ((old_email != txtRemail.Text) || (old_telNo != txtRtelno.Text))
            {
                CustomerDetailsBAL.Update_ProfileDetails(int.Parse(ddlRname.SelectedValue), sessionKeys.UID, txtRemail.Text, txtRtelno.Text);
            }
            //disable the no days
            if (cd.ID > 0)
            {
                txtNodays.Enabled = false;
            }
            //add inserted value
            //h_cid.Value = id.ToString();
            GetRecords(id);
            BindAccessNo(id);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> onDDLPopulated();</script>", false);
            Change_SubmitButtonImg(false);
            //Next, please enter details of the visitors using the "+" button in the panel below under each access number
            lblUsermsg.Text = "Next, please enter details of the visitors using the \"+\"  button in the panel below under each access number";
            //add requeste as default in visitors
            InsertDefaultVisitors(id);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #region Update access control
    private void UpdateCustomerAccessControl(int callid)
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
                 // DC.SRV.WebService ws = new DC.SRV.WebService();
                 // int rid = ws.GetContactID();
                 CallDetail cd = CallDetailsBAL.SelectbyId(callid);
                 AccessControl ac = AccessControlBAL.AccessControlDetails_selectByID(callid);

                 //get old status
                 int? old_status = cd.StatusID;
                 // bool IsStatusChanged = CompareStatusChanged(cd, int.Parse(ddlstatus.SelectedValue));
                 bool IsCallDetailsChanged = CompareCallDetails(cd, int.Parse(ddlsite.SelectedValue), int.Parse(ddlstatus.SelectedValue), int.Parse(ddlRname.SelectedValue));
                 cd.SiteID = int.Parse(ddlsite.SelectedValue);
                 cd.RequestTypeID = int.Parse(ddlRtype.SelectedValue);
                 cd.StatusID = int.Parse(ddlstatus.SelectedValue);
                 cd.CompanyID = int.Parse(ddlRcmpy.SelectedValue);
                 cd.RequesterID = int.Parse(ddlRname.SelectedValue);
                 cd.LoggedBy = sessionKeys.UID;
                 //bool IsCustomerAccessControlDetailsChanged = CompareCustomerAccessControlDetails(ac, callid, int.Parse(ddlarea.SelectedValue), int.Parse(txtNodays.Text.Trim()), int.Parse(ddlvstngpurp.SelectedValue), Convert.ToDateTime(txtRdate.Text));
                 bool IsCustomerAccessControlDetailsChanged = CompareCustomerAccessControlDetails(ac, callid, 0, int.Parse(txtNodays.Text.Trim()), int.Parse(ddlvstngpurp.SelectedValue), Convert.ToDateTime(txtRdate.Text), "");
                 if (IsCallDetailsChanged)
                     AddCallDetailsJournal(cd, CurrentDate);

                 CallDetailsBAL.CallDetailsUpdate(cd);

                 ac.RequestedDate = Convert.ToDateTime(txtRdate.Text);
                 ac.NumberOfDays = int.Parse(txtNodays.Text.Trim());
                 //Area
                 //ac.AreaID = int.Parse(ddlarea.SelectedValue);
                 ac.PurposeOfVisitID = int.Parse(ddlvstngpurp.SelectedValue);
                 //ac.Notes = txtnotes.Text.Trim();
                 if (IsCustomerAccessControlDetailsChanged)
                 {
                     AddCustomerAccessControlJournal(callid, CurrentDate);
                 }
                 AccessControlBAL.AccessControlDetails_update(ac);

                 int vistors_count = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == int.Parse(h_callid.Value)).Count();

                 if (vistors_count > 0)
                 {
                     sendmail(true);
                     DistributionMail();
                     Response.Redirect("~/WF/DC/DCCustomerJlist.aspx?type=AccessControl", false);
                     //if (Request.QueryString["callid"] == null)
                     //{
                     //    sendmail(true);
                     //    DistributionMail();
                     //    Response.Redirect("~/DC/DCCustomerJlist.aspx?type=AccessControl", false);
                     //}
                     //else
                     //{
                     //    sendmail(true);
                     //    Response.Redirect("~/DC/DCCustomerJlist.aspx?type=AccessControl", false);

                     //DistributionMail();
                 }
                 ctr_history.DisplayHistory(callid);

             }
             else
             {

                 lblstatustext.Text = "Please add at least one visitor for each access number.";
             }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    private void InsertDefaultVisitors(int Callid)
    {
        using (DCDataContext dc = new DCDataContext())
        {
            using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioDataContext())
            {
                var calldetails = dc.CallDetails.Where(o => o.ID == Callid).FirstOrDefault();
                var companyname = pd.ProjectPortfolios.Where(o => o.ID == calldetails.CompanyID).Select(o => o.PortFolio).FirstOrDefault();
                DC.SRV.WebService ws = new DC.SRV.WebService();
                PortfolioContact pc = ws.GetContactDetails(calldetails.RequesterID.Value);
                List<Visitor> vList = new List<Visitor>();
                List<VisitorsJournal> vjList = new List<VisitorsJournal>();
                var accessControlNumbers = dc.AccessNumbersBasedonDays.Where(o => o.CallID == Callid).ToList();
                foreach (var ac in accessControlNumbers)
                {
                    var v = new Visitor();
                    v.Name = pc.Name;
                    v.EmailAddress = pc.Email;
                    var pn = (!string.IsNullOrEmpty(pc.Telephone) ? pc.Telephone : pc.Mobile);
                    v.PhoneNumber = pn.Length > 20 ? pn.Substring(0, 20) : pn;
                    v.PhotoID = false;
                    v.AccessNo = ac.AccessNumber;
                    v.CallID = Callid;
                    v.Company = companyname;
                    v.ArriveStatus = true;
                    v.DepartStatus = false;
                    v.NoShow = false;
                    dc.Visitors.InsertOnSubmit(v);
                    dc.SubmitChanges();

                    var vj = new VisitorsJournal();

                    vj.AccessNo = v.AccessNo;
                    vj.CallID = v.CallID;
                    vj.Company = v.Company;
                    vj.EmailAddress = v.EmailAddress;
                    vj.Name = v.Name;
                    vj.PhotoID = v.PhotoID;
                    vj.ArriveStatus = v.ArriveStatus;
                    vj.DepartStatus = v.DepartStatus;
                    vj.NoShow = v.NoShow;
                    vj.VisibleToCustomer = true;
                    vj.VisitorID = v.ID;
                    vj.PhoneNumber = v.PhoneNumber;
                    vj.ModifiedDate = DateTime.Now;
                    vj.ModifiedBy = sessionKeys.UID;

                    dc.VisitorsJournals.InsertOnSubmit(vj);
                    dc.SubmitChanges();
                }
            }
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
                UpdateCustomerAccessControl(int.Parse(h_callid.Value));
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #region Access Number
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
            //int mNumber = AccessNumberBAL.AccessNumber_MaxValue();
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
    #endregion

    #region SendMail
    private void sendmail(bool check_insert)
    {
        try
        {

            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();
            int rid = ws.GetContactID();
            List<string> site = new List<string>();
            // site = ws.GetSiteDetailsbyId(int.Parse(ddlsite.SelectedValue));
            var statusname = ddlstatus.SelectedItem.Text;
            EmailFooter ef = new EmailFooter();
            AccessControlEmail ace = DefaultsOfAccessControl.AccessEmail_select();
            ef = FooterEmail.EmailFooter_selectByID(int.Parse(ddlRtype.SelectedValue));
            PortfolioContact pc = ws.GetContactDetails(rid);
           string subject = string.Empty;
           Emailer em = new Emailer();
           string body = em.ReadFile("~/WF/DC/EmailTemplates/AccessControlMail.htm");
           if (check_insert)
           {
               subject = "Ticket Reference: " + int.Parse(h_callid.Value);
               body = body.Replace("[mail_head]", "Access Request");
           }
           else
           {
               subject = "Ticket Reference: " + int.Parse(h_callid.Value);
               body = body.Replace("[mail_head]", "Access Request Update");
           }

           body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
           body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            body = body.Replace("[user]", pc.Name);
            body = body.Replace("[requestercompany]", ddlRcmpy.SelectedItem.Text);
            body = body.Replace("[requestername]", pc.Name);
            body = body.Replace("[sitename]", ddlsite.SelectedItem.Text);
            body = body.Replace("[status]", statusname);
            body = body.Replace("[requesteddate]", txtRdate.Text);
            body = body.Replace("[noofdays]", txtNodays.Text);
            //body = body.Replace("[area]", ddlarea.SelectedItem.Text);
            body = body.Replace("[purposeofvisit]", ddlvstngpurp.SelectedItem.Text);
            body = body.Replace("[requesttype]", "Access Request");
            body = body.Replace("[statustime]", "");
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


            string bodyVisitors = string.Empty;
            List<Visitor> visitors = new List<Visitor>();
            //visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == int.Parse(h_callid.Value)).ToList();
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
            body = body.Replace("[notes]", "");
            body = body.Replace("Notes :", "");
            body = body.Replace("[noteslist]", RequestMailFormat.GetNotesList(int.Parse(h_callid.Value)));
            body = body.Replace("[footer]", HttpUtility.HtmlDecode(ef.EmailFooter1));
            body = body.Replace("[adminemail]", ace.EmailAddress);
            em.SendingMail(fromemailid, subject, body, pc.Email);

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #endregion

    #region Distribution mail
    private void DistributionMail()
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            int rid = ws.GetContactID();
            PortfolioContact pc = ws.GetContactDetails(rid);
            var statusname = ddlstatus.SelectedItem.Text;
            Emailer em = new Emailer();
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            string subject = "Ticket Reference: " + int.Parse(h_callid.Value);
            string body = em.ReadFile("~/WF/DC/EmailTemplates/AccessControlDistributionListMail.htm");
            body = body.Replace("[mail_head]", "Access Request");
            body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            body = body.Replace("[Customer]", ddlRcmpy.SelectedItem.Text);
            body = body.Replace("[Name]", pc.Name);
            body = body.Replace("[noofdays]", txtNodays.Text);
            body = body.Replace("[purposeofvisit]", ddlvstngpurp.SelectedItem.Text);
            //body = body.Replace("[area]", ddlarea.SelectedItem.Text);
            body = body.Replace("[requesteddate]", txtRdate.Text);
            body = body.Replace("[requesttype]", "Access Request");
            body = body.Replace("[sitename]", ddlsite.SelectedItem.Text);
            body = body.Replace("[status]", statusname);
            body = body.Replace("[notes]", "");
            body = body.Replace("Notes:", "");
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

            //visitors = VisitorsBAL.BindVisitorsByCallid().Where(p => p.CallID == int.Parse(h_callid.Value)).ToList();
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
        catch (Exception ex) {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    #endregion
    private string RetRowdata(string str1, string str2, string str3, string str4, string str5, string str6, string str7)
    {
        return string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>", str1, str2, str3, str4, str5, str6, str7);

    }
    private bool CompareCallDetails(CallDetail cd, int sid, int stid, int rid)
    {
        bool IsChanged = false;
        try
        {
            CallDetail pastDetails = cd;
            if (pastDetails.SiteID != sid)
                IsChanged = true;
            else if (pastDetails.StatusID != stid)
                IsChanged = true;
            else if (pastDetails.RequesterID != rid)
                IsChanged = true;
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return IsChanged;
    }

    private bool CompareCustomerAccessControlDetails(AccessControl ac, int cid, int aid, int noOfdays, int pvid, DateTime Rdate,string notes)
    {
        bool IsChanged = false;
        try
        {
            AccessControl PreviousDetails = ac;
            //if (PreviousDetails.CallID != cid)
            //    IsChanged = true;
            //else if (PreviousDetails.AreaID != aid)
            //    IsChanged = true;
            //else if (PreviousDetails.NumberOfDays != noOfdays)
            //    IsChanged = true;
            //else if (PreviousDetails.PurposeOfVisitID != pvid)
            //    IsChanged = true;
            //else if (PreviousDetails.RequestedDate != Rdate)
            //    IsChanged = true;
             if (PreviousDetails.Notes != notes)
                IsChanged = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return IsChanged;
    }

    private void AddCustomerAccessControlJournal(int acid, DateTime ModifiedDate)
    {
        try
        {

            DC.SRV.WebService ws = new DC.SRV.WebService();
            AccessControlJournal acJ = new AccessControlJournal();
            acJ.CallID = acid;
            //acJ.AreaID = int.Parse(ddlarea.SelectedValue);
            acJ.ModifiedBy = sessionKeys.UID;
            acJ.ModifiedDate = ModifiedDate;
           // acJ.Notes = txtnotes.Text.Trim();
            acJ.NumberOfDays = int.Parse(txtNodays.Text.Trim());
            acJ.PurposeOfVisit = int.Parse(ddlvstngpurp.SelectedValue);
            acJ.RequestedDate = Convert.ToDateTime(txtRdate.Text.Trim());
            acJ.VisibleToCustomer = true;
            ws.InsertAccessControlJournal(acJ);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void AddCallDetailsJournal(CallDetail cd, DateTime ModifiedDate)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            CallDetailsJournal cdj = new CallDetailsJournal();

            cdj.CallID = cd.ID;
            cdj.SiteID = int.Parse(ddlsite.SelectedValue);
            cdj.RequestTypeID = int.Parse(ddlRtype.SelectedValue);
            cdj.StatusID = int.Parse(ddlstatus.SelectedValue);
            cdj.CompanyID = int.Parse(ddlRcmpy.SelectedValue);
            cdj.RequesterID = cd.RequesterID;
            cdj.LoggedDate = cd.LoggedDate;
            cdj.LoggedBy = cd.LoggedBy;
            cdj.ModifiedBy = sessionKeys.UID;
            cdj.ModifiedDate = ModifiedDate;
            cdj.VisibleToCustomer = true;
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
            
            txtNodays.Text = string.Empty;
            //txtnotes.Text = string.Empty;
            txtRdate.Text = string.Empty;
            //ccdCompny.SelectedValue = "0";
            //Area
            //ccdarea.SelectedValue = "0";
            //ccdType.SelectedValue = "0";
            //ccdSite.SelectedValue = "0";
            //ccdStatus.SelectedValue = "0";
            ccdvp.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    #region Bind Visitors
    [WebMethod(EnableSession = true)]
    public static object BindVisitors(string an,string callid, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
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
                visitors = visitors.OrderByDescending(o => o.Company).ToList();
            }
            else if (jtSorting.Equals("Company DESC"))
            {
                visitors = visitors.OrderByDescending(o => o.Company).ToList();
            }
            else if (jtSorting.Equals("EmailAddress ASC"))
            {
                visitors = visitors.OrderByDescending(o => o.EmailAddress).ToList();
            }
            else if (jtSorting.Equals("EmailAddress DESC"))
            {
                visitors = visitors.OrderByDescending(o => o.EmailAddress).ToList();
            }
            else if (jtSorting.Equals("PhoneNumber ASC"))
            {
                visitors = visitors.OrderByDescending(o => o.PhoneNumber).ToList();
            }
            else if (jtSorting.Equals("PhoneNumber DESC"))
            {
                visitors = visitors.OrderByDescending(o => o.PhoneNumber).ToList();
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

    }

    #region Bind Access Number
    [WebMethod(EnableSession = true)]
    public static object BindAccessNumber(string callid,int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
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
                        Day = a.Day
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
    #endregion

    #region Update visitors
    [WebMethod(EnableSession = true)]
    public static object Update(Visitor record)
    {

        DCDataContext db = new DCDataContext();
        Visitor v = db.Visitors.Where(p => p.ID == record.ID).SingleOrDefault();
        VisitorsJournal vj = new VisitorsJournal();
        vj.ArriveDate = v.ArrivalDate;
        vj.DepartDate = v.DepatureDate;
        vj.ArriveStatus = v.ArriveStatus;
        vj.DepartStatus = v.DepartStatus;
        vj.PhotoID = v.PhotoID;
        vj.ModifiedDate = DateTime.Now;
        vj.ModifiedBy = sessionKeys.UID;
        vj.AccessNo = v.AccessNo;
        v.Company = record.Company;
        v.EmailAddress = record.EmailAddress;
        v.Name = record.Name;
        v.PhoneNumber = record.PhoneNumber;
        vj.VisitorID = v.ID;
        vj.CallID = v.CallID;
        vj.Name = v.Name;
        vj.Company = v.Company;
        vj.EmailAddress = v.EmailAddress;
        vj.PhoneNumber = v.PhoneNumber;
        vj.VisibleToCustomer = true;
        db.VisitorsJournals.InsertOnSubmit(vj);
        db.SubmitChanges();
        return new { Result = "OK" };
    }
    #endregion

    #region create visitor
    [WebMethod(EnableSession = true)]
    public static object Create(Visitor record, string accno,string callid)
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
        v.AccessNo = accno;
        v.PhotoID = false;
        v.PhoneNumber = record.PhoneNumber;
        vj.ArriveDate = v.ArrivalDate;
        vj.DepartDate = v.DepatureDate;
        vj.AccessNo = accno;
        vj.ArriveStatus = true;
        vj.DepartStatus = false;
        vj.Name = v.Name;
        vj.Company = v.Company;
        vj.EmailAddress = v.EmailAddress;
        vj.PhoneNumber = v.PhoneNumber;
        vj.ModifiedDate = DateTime.Now;
        vj.ModifiedBy = sessionKeys.UID;
        vj.PhotoID = v.PhotoID;
        db.Visitors.InsertOnSubmit(v);
        vj.CallID = Convert.ToInt32(callid);
        vj.VisibleToCustomer = true;
        db.SubmitChanges();
        vj.VisitorID = v.ID;
        db.VisitorsJournals.InsertOnSubmit(vj);
        db.SubmitChanges();
        return new { Result = "OK", Record = v };

    }
    #endregion
 
    protected void imgbtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
   
    private void Change_SubmitButtonImg(bool IsChangeImg)
    {
        if (IsChangeImg)
            imgbtnSubmit.Text = "Add";
        else
            imgbtnSubmit.SkinID = "Submit";
    }

    protected void repeter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void imgbtncpyvstrs_Click(object sender, EventArgs e)
    {
        try
        {
            CopyVisitors();
            //AccessControlHistory1.DisplayHistory(int.Parse(h_callid.Value));
            //iframe_DisplayHistory(int.Parse(h_callid.Value));
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
                                vj.VisibleToCustomer = true;
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


}