using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.Linq;
using System.IO;
using DC.BLL;
using DC.BAL;
using DC.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;

public partial class DC_DeliveryCustomer : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //if (sessionKeys.PortalUser || sessionKeys.SID == 7)
        //    this.Page.MasterPageFile = "~/DeffinityCustomerTab.master";
        //else
        //    Response.Redirect("~/WF/Default.aspx");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CompareVal_Date.ValueToCompare = DateTime.Now.ToShortDateString();
                if (sessionKeys.PortfolioID != 0)
                {
                    if (Request.QueryString["callid"] != null)
                    {
                        int tid = int.Parse(Request.QueryString["callid"].ToString());
                       // checked valid user if not return to home page 
                        if (!CheckValid_User(tid))
                        {
                            Response.Redirect("~/WF/Portal/Home.aspx", false);
                            return;
                        }
                        else
                        {
                            Anticipated_HideDateValidation();
                            lblTitle.InnerText = "Delivery - Ticket Reference " + tid;
                            BindData(tid);
                            BindDocuments(tid);
                            //bind history
                            // ctr_history.DisplayHistory(tid);
                            //ctr_history.DataBind();
                            pnlNotes.Visible = true;
                        }
                       
                        
                    }
                    else
                    {
                        lblTitle.InnerText = "Delivery ";
                        ccdCompany.SelectedValue = sessionKeys.PortfolioID.ToString();
                        ccdName.SelectedValue = CustomerDetailsBAL.GetCustomerUser_ContactID(sessionKeys.UID).ToString();
                        GvDocuments.DataBind();
                        setDefaultSite();
                       
                    }
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
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
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

    private void setDefaultSite()
    {
        DefaultData d = DefaultDataBAL.SiteDefaultData_select();
        if (d != null)
        {
            ccdSite.SelectedValue = d.SiteID.ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (h_cid.Value == "0")
            InsertData();
        else
            UpdateData(int.Parse(h_cid.Value));
    }
  
    private void InsertData()
    {
        try
        {
            DateTime CurrentDate = DateTime.Now;
           // DC.SRV.WebService ws = new DC.SRV.WebService();
            CallDetail cd = new CallDetail();
            DeliveryInformation di = new DeliveryInformation();
            PortfolioContact pc = CustomerDetailsBAL.GetPortfolioContactDetailsbyID(int.Parse(ddlRequestersName.SelectedValue));
            UserMgt.Entity.Contractor c = CustomerDetailsBAL.GetContractorDetailsbyID(sessionKeys.UID);
            string old_email = pc.Email;
            string old_telNo = pc.Telephone;
            //int rid = ws.GetContactID();
            bool Is1Pallet = false;
            bool IsOverweight = false;
            cd.SiteID = int.Parse(ddlSite.SelectedValue);
            cd.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
            cd.StatusID = int.Parse(ddlStatus.SelectedValue);
            cd.CompanyID = int.Parse(ddlRequestersCompany.SelectedValue);
            cd.RequesterID = int.Parse(ddlRequestersName.SelectedValue);
            int rid = int.Parse(ddlRequestersName.SelectedValue);
            cd.LoggedDate = DateTime.Now;
            cd.LoggedBy = sessionKeys.UID;
            CallDetailsBAL.AddCallDetails(cd);
            AddCallDetailsJournal(cd, CurrentDate);
            int id = cd.ID;
            di.CallID = id;
            di.ArrivalDate = Convert.ToDateTime(txtDateofArrival.Text);
            //di.Weight = txtWeight.Text;
            di.NumofBoxes = Convert.ToInt32(txtNumberofBoxes.Text);
            di.DeliveryTypeID = int.Parse(ddlDeliveryType.SelectedValue);
            di.Description = txtDescription.Text;
            if (ddlOver1pallet.SelectedValue == "Yes")
                Is1Pallet = true;
            //if (ddlOverWeight.SelectedValue == "Yes")
            //    IsOverweight = true;
            if (ddlOver1pallet.SelectedValue != "0")
                di.Pallet = Is1Pallet;
            //if (ddlOverWeight.SelectedValue != "0")
            //    di.OverWeight = IsOverweight;
            di.CourierNumber = txtTrackingNumber.Text;
            di.CourierCompany = txtCourierCompany.Text;
           // di.Notes = txtNotes.Text;
            di.ItemWeight = Convert.ToInt32(ddlItemWeight.SelectedValue);
            DeliveryInformationBAL.AddDeliveryInformation(di);
            AddDeliveryInformationJournal(id, CurrentDate);
            //upload files
            UploadFiles(id);
            lblTitle.InnerText = "Delivery - Ticket Reference " + id;
            BindData(id);
            if ((old_email != txtRequestersEmailAddress.Text) || (old_telNo != txtRequestersTelephoneNo.Text))
            {
                CustomerDetailsBAL.Update_ProfileDetails(int.Parse(ddlRequestersName.SelectedValue), sessionKeys.UID, txtRequestersEmailAddress.Text, txtRequestersTelephoneNo.Text);
            }

            if (cd.ID > 0)
            {
                sendmail(rid,cd);
                //1	Awaiting Approval
                if (cd.StatusID == 1)
                    sendDistributionListmail(cd, true, "Delivery Request");
                //if (Is1Pallet == true && Convert.ToInt32(ddlItemWeight.SelectedValue) >1)
                //    sendDistributionListmail(cd, true, "Overweight delivery");
                //else
                //    sendDistributionListmail(cd, true, "Delivery Request");

                Response.Redirect("~/WF/DC/DCCustomerJlist.aspx?type=Delivery");
                
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void UpdateData(int cid)
    {
        try
        {
            DateTime CurrentDate = DateTime.Now;
           // DC.SRV.WebService ws = new DC.SRV.WebService();
            CallDetail cd = CallDetailsBAL.SelectbyId(cid);
            DeliveryInformation di = DeliveryInformationBAL.SelectbyCallId(cid);
            
           //int rid = ws.GetContactID();
            bool Is1Pallet = false;
            bool IsOverweight = false;
            //int statusid = 1;    // 1=Awaiting Approval
            if (ddlOver1pallet.SelectedValue == "Yes")
                Is1Pallet = true;
            //if (ddlOverWeight.SelectedValue == "Yes")
            //    IsOverweight = true;
            //if (Is1Pallet == false || IsOverweight == false)
            //    statusid = int.Parse(ddlStatus.SelectedValue);
            //bool IsCallDetailsChanged = CompareCallDetails(cd, int.Parse(ddlSite.SelectedValue), statusid, int.Parse(ddlRequestersName.SelectedValue));
            //cd.SiteID = int.Parse(ddlSite.SelectedValue);
            //cd.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
            //cd.RequesterID = int.Parse(ddlRequestersName.SelectedValue);
            //if (Is1Pallet == true && IsOverweight == true)
            //    cd.StatusID = statusid; // 1=Awaiting Approval
            //else
            //    cd.StatusID = int.Parse(ddlStatus.SelectedValue);
            //if (IsCallDetailsChanged)
            //    AddCallDetailsJournal(cd, CurrentDate);
            //CallDetailsBAL.CallDetailsUpdate(cd);
            int id = cd.ID;

            bool IsDeliveryInformationChanged = CompareDeliveryInformation(di, id, Convert.ToDateTime(txtDateofArrival.Text), string.Empty, Convert.ToInt32(txtNumberofBoxes.Text), int.Parse(ddlDeliveryType.SelectedValue), txtDescription.Text, Is1Pallet, IsOverweight, txtTrackingNumber.Text, txtCourierCompany.Text, Convert.ToInt32(ddlItemWeight.SelectedValue), "");
            di.CallID = id;
            //di.ArrivalDate = Convert.ToDateTime(txtDateofArrival.Text);
            //di.Weight = txtWeight.Text;
            //di.NumofBoxes = Convert.ToInt32(txtNumberofBoxes.Text);
            //di.DeliveryTypeID = int.Parse(ddlDeliveryType.SelectedValue);
            //di.Description = txtDescription.Text;           
            //if (ddlOver1pallet.SelectedValue != "0")
            //    di.Pallet = Is1Pallet;
            //if (ddlOverWeight.SelectedValue != "0")
            //    di.OverWeight = IsOverweight;
            di.CourierNumber = txtTrackingNumber.Text;
            //di.CourierCompany = txtCourierCompany.Text;
           // di.Notes = txtNotes.Text;
            if (IsDeliveryInformationChanged)
                AddDeliveryInformationJournal(id, CurrentDate);

            
            DeliveryInformationBAL.DeliveryInformationUpdate(di);
            //upload files
           // UploadFiles(cd.ID);
            BindData(cd.ID);
            //if (cd.ID > 0)
            //{
            //    if (Is1Pallet == true && IsOverweight == true)
            //        sendDistributionListmail(cd, true, "Overweight delivery");
            //}
            //bind history
            ctr_history.DisplayHistory(cd.ID);
            Response.Redirect("~/WF/DC/DCCustomerJlist.aspx?type=Delivery");
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    private void BindData(int cid)
    {
        try
        {
            CallDetail cd = CallDetailsBAL.SelectbyId(cid);
            DeliveryInformation di = DeliveryInformationBAL.SelectbyCallId(cid);         

            if (cd != null && di != null)
            { 
               
                h_cid.Value = cid.ToString();
                 if (Request.QueryString["callid"] != null)
                     h_callid.Value = cid.ToString();
                ccdCompany.SelectedValue = Convert.ToString(cd.CompanyID);
                ccdSite.SelectedValue = Convert.ToString(cd.SiteID);               
                ccdType.SelectedValue = Convert.ToString(cd.RequestTypeID);
                ccdStatus.SelectedValue = Convert.ToString(cd.StatusID);
                ccdName.SelectedValue = Convert.ToString(cd.RequesterID);
                txtDateofArrival.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(di.ArrivalDate).Replace("00:00:00", " "));
                //txtWeight.Text = di.Weight;
                txtNumberofBoxes.Text = Convert.ToString(di.NumofBoxes);
                ccdDeliveryType.SelectedValue = Convert.ToString(di.DeliveryTypeID);
                txtDescription.Text = di.Description;
                if (di.Pallet == true)
                    ddlOver1pallet.SelectedValue = "Yes";
                else if (di.Pallet == false)
                    ddlOver1pallet.SelectedValue = "No";
                else
                    ddlOver1pallet.SelectedValue = "0";
                //if (di.OverWeight == true)
                //    ddlOverWeight.SelectedValue = "Yes";
                //else if (di.OverWeight == false)
                //    ddlOverWeight.SelectedValue = "No";
                //else
                //    ddlOverWeight.SelectedValue = "0";
                txtTrackingNumber.Text = di.CourierNumber;
                txtCourierCompany.Text =  di.CourierCompany;
                ccditemweight.SelectedValue = di.ItemWeight.HasValue ? di.ItemWeight.Value.ToString() : "0";
               // txtNotes.Text = di.Notes;
                if (h_cid.Value != "0")
                {
                    //tdbuttons.Visible = true;
                    trupload.Visible = false;
                    ddlOver1pallet.Enabled = false;
                    //imgToDate.Visible = false;
                    //ddlOverWeight.Enabled = false;
                    //ddlItemWeight.Enabled = false;
                }
                else
                {
                    //tdbuttons.Visible = true;
                    trupload.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    
    private void sendmail(int rid, CallDetail cd)
    {
        try
        {
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();
            //List<string> site = new List<string>();
            //site = ws.GetSiteDetailsbyId(int.Parse(ddlSite.SelectedValue));
            EmailFooter ef = new EmailFooter();
            ef = FooterEmail.EmailFooter_selectByID(int.Parse(ddlTypeofRequest.SelectedValue));
            PortfolioContact pc = ws.GetContactDetails(rid);
            AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select();
            string status = DC.BLL.StatusBAL.SelectbyId(string.IsNullOrEmpty(ccdStatus.SelectedValue) ? 0 : Convert.ToInt32(ccdStatus.SelectedValue)).Name;
            string subject = "Ticket Reference: " + cd.ID;
            
            Emailer em = new Emailer();
            string body = em.ReadFile("~/WF/DC/EmailTemplates/DeliveryMail.htm");

            body = body.Replace("[mail_head]", "Delivery Request");
            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            body = body.Replace("[user]", pc.Name);
            body = body.Replace("[Status]", status);
            body = body.Replace("[mailcontent]", RequestMailFormat.DeliveryHtmlMailDetails(cd.ID));
            body = body.Replace("[adminemail]", ae.EmailAddress);
            //body = body.Replace("[sitename]", ddlSite.SelectedItem.Text);
            //body = body.Replace("[reference]", h_cid.Value);
            //body = body.Replace("[items]", txtNumberofBoxes.Text);
            //body = body.Replace("[cnumber]", txtTrackingNumber.Text);
            //body = body.Replace("[ccompany]", txtCourierCompany.Text);
            //body = body.Replace("[contact]", pc.Email);
            //body = body.Replace("[mobile]", pc.Telephone);
            //body = body.Replace("[notes]", "");
           
            //body = body.Replace("[adminemail]", ae.EmailAddress);
            //body = body.Replace("[Company]", ddlRequestersCompany.SelectedItem.Text);
            //body = body.Replace("[Date]", txtDateofArrival.Text);
            ////body = body.Replace("[Weight]", txtWeight.Text);
            //body = body.Replace("[Weight]", ddlItemWeight.SelectedItem.Text);
            //body = body.Replace("[DType]", ddlDeliveryType.SelectedItem.Text);
            //body = body.Replace("[pallet]", ddlOver1pallet.SelectedItem.Text);
            ////body = body.Replace("[OWeight]", ddlOverWeight.SelectedItem.Text);
            ////body = body.Replace("[kgs]", lblweight.Text);
            //body = body.Replace("[Description]", txtDescription.Text);
            //body = body.Replace("[Status]", status);


            if (ef != null)
            {
                body = body.Replace("[footer]", HttpUtility.HtmlDecode(ef.EmailFooter1));
            }
            else
            {
                body = body.Replace("[footer]", "");
            }
            em.SendingMail(fromemailid, subject, body, pc.Email);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void sendDistributionListmail(CallDetail cd, bool isInserted,string MailHeader)
    {
        try
        {
            List<int> idlist = SecurityAccessMail.GetIds(1); //1=Delivery
            if (idlist.Count > 0)
            {
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                DC.SRV.WebService ws = new DC.SRV.WebService();
                PortfolioContact pc = ws.GetContactDetails(cd.RequesterID.Value);
                string status = DC.BLL.StatusBAL.SelectbyId(string.IsNullOrEmpty(ccdStatus.SelectedValue) ? 0 : Convert.ToInt32(ccdStatus.SelectedValue)).Name;

                //string subject = string.Format("Delivery Request: " + cd.ID + " {0} {1} has requested a {4} on {2} at {3}", ddlRequestersCompany.SelectedItem.Text, pc.Name, DateTime.Now.ToString(Deffinity.systemdefaults.GetDateformat()), DateTime.Now.ToString("HH:mm"), MailHeader.ToLower());
                string subject = string.Format("Ticket Reference: " + cd.ID);
                Emailer em = new Emailer();
                string body = em.ReadFile("~/WF/DC/EmailTemplates/DeliveryDistributionListMail.htm");
                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                body = body.Replace("[mail_head]", MailHeader);
                if (isInserted)
                {
                    //[mail_head]
                    body = body.Replace("[content_header]", "The following customer would like to schedule a delivery");
                }
                else
                {
                    body = body.Replace("[content_header]", "Updated Delivery Request Details ");
                }
               body = body.Replace("[mailcontent]", RequestMailFormat.DeliveryHtmlMailDetails(cd.ID));
               body = body.Replace("[Status]", status);
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
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GvDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Download")
            {
                GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string contenttype = GvDocuments.DataKeys[gvrow.RowIndex].Values[0].ToString();
                string filename = GvDocuments.DataKeys[gvrow.RowIndex].Values[1].ToString();
                string[] ex = filename.Split('.');
                string ext = ex[ex.Length - 1];
                string filepath = string.Format("~/WF/UploadData/DC/{0}.{1}", e.CommandArgument.ToString(), ext);
                Response.ContentType = contenttype;
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                Context.Response.ContentType = "octet/stream";
                Response.TransmitFile(filepath);
                Response.End();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ImgDeskImageUpload_Click(object sender, EventArgs e)
    {
        UploadFiles(int.Parse(h_cid.Value));
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
                    if (callid >0)
                    {
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        hpf.SaveAs(fullname);
                        Document d = new Document();
                        d.CallID = callid;
                        d.DocumentID = DocumentId;
                        d.FileName = filename;
                        d.ContentType = contenttype;
                        DocumentsBAL.AddDocuments(d);
                    }
                }
                
            }
            BindDocuments(callid);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
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
    private void Clear()
    {
        ccdSite.SelectedValue = "0";
        ccdStatus.DataBind();
        ccdStatus.SelectedValue = "0";
        txtDateofArrival.Text = string.Empty;
        //txtWeight.Text = string.Empty;
        txtNumberofBoxes.Text = string.Empty;
        ccdDeliveryType.SelectedValue = "0";
        txtDescription.Text = string.Empty;
        ddlOver1pallet.SelectedValue = "0";
        //ddlOverWeight.SelectedValue = "0";
        ddlItemWeight.SelectedValue = "0";
        //txtNotes.Text = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
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
    private void AddCallDetailsJournal(CallDetail cd, DateTime ModifiedDate)
    {
        try
        {
            bool Is1Pallet = false;
            bool IsOverweight = false;
            if (ddlOver1pallet.SelectedValue == "Yes")
                Is1Pallet = true;
            //if (ddlOverWeight.SelectedValue == "Yes")
            //    IsOverweight = true;
            DC.SRV.WebService ws = new DC.SRV.WebService();
            CallDetailsJournal cdj = new CallDetailsJournal();
            cdj.CallID = cd.ID;
            cdj.SiteID = int.Parse(ddlSite.SelectedValue);
            cdj.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
            if (Is1Pallet == true && IsOverweight == true)
                cdj.StatusID = 1; // 1=Awaiting Approval
            else
                cdj.StatusID = int.Parse(ddlStatus.SelectedValue);
            cdj.CompanyID = int.Parse(ddlRequestersCompany.SelectedValue);
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
    private bool CompareDeliveryInformation(DeliveryInformation di, int cid, DateTime ad, string wt, int nob, int dtid, string desc, bool pallet, bool ovw, string cn, string cc,int itemweight,string notes)
    {
        bool IsChanged = false;
        try
        {
            DeliveryInformation pastInformation = di;
            //if (pastInformation.CallID != cid)
            //    IsChanged = true;
            //else if (pastInformation.ArrivalDate != ad)
            //    IsChanged = true;
            //else if (pastInformation.Weight != wt)
            //    IsChanged = true;
            //else if (pastInformation.NumofBoxes != nob)
            //    IsChanged = true;
            //else if (pastInformation.DeliveryTypeID != dtid)
            //    IsChanged = true;
            //else if (pastInformation.Description != desc)
            //    IsChanged = true;
            //else if (pastInformation.Pallet != pallet)
            //    IsChanged = true;
            //else if (pastInformation.OverWeight != ovw)
            //    IsChanged = true;
            //else 
            if (pastInformation.CourierNumber != cn)
                IsChanged = true;
           else if (pastInformation.Notes != notes)
                IsChanged = true;
            //else if (pastInformation.CourierCompany != cc)
            //    IsChanged = true;
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return IsChanged;
    }
    private void AddDeliveryInformationJournal(int cid,DateTime ModifiedDate)
    {
        try
        {
            bool Is1Pallet = false;
            bool IsOverweight = false;
            DC.SRV.WebService ws = new DC.SRV.WebService();
            DeliveryInformationJournal dij = new DeliveryInformationJournal();
            dij.CallID = cid;
            dij.ArrivalDate = Convert.ToDateTime(txtDateofArrival.Text);
            //dij.Weight = txtWeight.Text;
            dij.NumofBoxes = Convert.ToInt32(txtNumberofBoxes.Text);
            dij.DeliveryTypeID = int.Parse(ddlDeliveryType.SelectedValue);
            dij.Description = txtDescription.Text;
            if (ddlOver1pallet.SelectedValue == "Yes")
                Is1Pallet = true;
            //if (ddlOverWeight.SelectedValue == "Yes")
            //    IsOverweight = true;
            if (ddlOver1pallet.SelectedValue != "0")
                dij.Pallet = Is1Pallet;
            //if (ddlOverWeight.SelectedValue != "0")
            //    dij.OverWeight = IsOverweight;
            dij.CourierNumber = txtTrackingNumber.Text;
            dij.CourierCompany = txtCourierCompany.Text;
            //dij.Notes = txtNotes.Text;
            dij.ModifiedBy = sessionKeys.UID;
            dij.ModifiedDate = ModifiedDate;
            dij.VisibleToCustomer = true;
            dij.ItemWeight = Convert.ToInt32(ddlItemWeight.SelectedValue);
            ws.AddDeliveryInformationJournal(dij);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void Anticipated_HideDateValidation()
    {
        CompareVal_Date.Visible = false;
    }
}