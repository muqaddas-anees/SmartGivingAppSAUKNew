using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using System.IO;
using DC.BLL;
using DC.Entity;
using DC.BAL;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System.Data;

public partial class DC_PermitToWork : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //if (sessionKeys.SID == 9)
        //{
        //    this.Page.MasterPageFile = "~/DeffinityResourceTab.master";
        //}
        //else
        //    Response.Redirect("~/Default.aspx");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
               // Master.PageHead = "Permit to Work";

                if (Request.QueryString["callid"] != null)
                {
                    int tid = int.Parse(Request.QueryString["callid"].ToString());
                    lblTitle.InnerText = "Permit to Work - Ticket Reference " + tid;
                    BindData(tid);
                    BindDocuments(tid);
                    //lbtnChecklists.NavigateUrl = string.Format("~/WF/DC/Checklist.aspx?callid={0}", tid);
                    //lbtnPermit.NavigateUrl = string.Format("~/WF/DC/PermitToWork.aspx?callid={0}", tid);
                    //history1.DisplayHistory(tid);
                    //history1.Visible = true;
                    iframe_DisplayHistory(tid);
                }
                else
                {
                    lblTitle.InnerText = "Permit to Work ";
                    GvDocuments.DataBind();
                    setDefaultSite();
                    txtLoggedDateTime.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                    txtLoggedTime.Text = string.Format("{0:HH:mm}", DateTime.Now);
                }
                CompareValidator3.ValueToCompare = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void iframe_DisplayHistory(int tid)
    {
        iframe1.Attributes.Add("src", "/WF/DC/HistoryDisplay.aspx?type=permittowork&callid=" + tid.ToString());
    }
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
        try
        {
            if (h_cid.Value == "0")
            {
                InsertData();
            }
            else
            {
                UpdateData(int.Parse(h_cid.Value));
            }
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void InsertData()
    {
        try
        {
            DateTime CurrentDate = DateTime.Now;
            CallDetail cd = new CallDetail();
            PermitToWork pw = new PermitToWork();
          
            cd.SiteID = int.Parse(ddlSite.SelectedValue);
            cd.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
            cd.StatusID = int.Parse(ddlStatus.SelectedValue);
            cd.CompanyID = int.Parse(ddlRequestersCompany.SelectedValue);
            cd.RequesterID = int.Parse(ddlRequestersName.SelectedValue);
            cd.LoggedDate = DateTime.Now;
            cd.LoggedBy = sessionKeys.UID;
            CallDetailsBAL.AddCallDetails(cd);
            AddCallDetailsJournal(cd.ID, cd,CurrentDate);
            int id = cd.ID;
            pw.CallID = id;
            pw.FromDateofWork = Convert.ToDateTime(txtFrom.Text + " " + (string.IsNullOrEmpty(txtFromTime.Text) ? "00:00:00" : txtFromTime.Text + ":00"));
            pw.ToDateofWork = Convert.ToDateTime(txtTo.Text + " " + (string.IsNullOrEmpty(txtToTime.Text) ? "00:00:00" : txtToTime.Text + ":00"));
            pw.Area = int.Parse(ddlArea.SelectedValue);
            pw.TypeofPermit = int.Parse(ddlPermit.SelectedValue);
            pw.DescriptionofWorks = txtDescription.Text;        
            pw.ArrivalDate = Convert.ToDateTime(txtArrivalDate.Text + " " + (string.IsNullOrEmpty(txtArrivalTime.Text) ? "00:00:00" : txtArrivalTime.Text + ":00"));
            pw.Reason = txtReason.Text;
            pw.Notes = txtNotes.Text;
           
            PermitToWorkBAL.AddPermittoWork(pw);
            AddPermitToWorkJournal(id,CurrentDate);
            AddChecklistItems(id);
            //upload files
            UploadFiles(id);
            lblTitle.InnerText = "Permit to Work - Ticket Reference " + id;
            BindData(id);
            sendmail(true);
            sendDistributionListmail(cd);
            //edited by Dinesh
            //history1.DisplayHistory(id);
            iframe_DisplayHistory(id);
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
            if (ddlStatus.SelectedItem.Text == "Closed")
            {
                bool IsClosed = CheckStatus(cid);
                if (IsClosed)
                {
                    lblerr.Text = "Please complete the checklist.";
                    return;
                }
            }
            DateTime CurrentDate = DateTime.Now;

            CallDetail cd = CallDetailsBAL.SelectbyId(cid);
            PermitToWork pw = PermitToWorkBAL.SelectbyCallId(cid);
            //get old status before update
            int? old_status = cd.StatusID;
            bool IsCallDetailsChanged = CompareCallDetails(cd, int.Parse(ddlSite.SelectedValue), int.Parse(ddlTypeofRequest.SelectedValue), int.Parse(ddlStatus.SelectedValue), int.Parse(ddlRequestersCompany.SelectedValue), int.Parse(ddlRequestersName.SelectedValue));
            cd.SiteID = int.Parse(ddlSite.SelectedValue);
            cd.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
            cd.StatusID = int.Parse(ddlStatus.SelectedValue);
            cd.CompanyID = int.Parse(ddlRequestersCompany.SelectedValue);
            cd.RequesterID = int.Parse(ddlRequestersName.SelectedValue);
            if (IsCallDetailsChanged)
                AddCallDetailsJournal(cid, cd,CurrentDate);
            CallDetailsBAL.CallDetailsUpdate(cd);
            int id = cd.ID;
            bool IsPermitToWorkChanged = ComparePermitToWork(pw, id, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), int.Parse(ddlArea.SelectedValue), int.Parse(ddlPermit.SelectedValue), txtDescription.Text, Convert.ToDateTime(txtArrivalDate.Text), txtReason.Text, txtNotes.Text);
            pw.CallID = id;
            pw.FromDateofWork = Convert.ToDateTime(txtFrom.Text + " " + (string.IsNullOrEmpty(txtFromTime.Text) ? "00:00:00" : txtFromTime.Text + ":00"));
            pw.ToDateofWork = Convert.ToDateTime(txtTo.Text + " " + (string.IsNullOrEmpty(txtToTime.Text) ? "00:00:00" : txtToTime.Text + ":00"));
            pw.Area = int.Parse(ddlArea.SelectedValue);
            pw.TypeofPermit = int.Parse(ddlPermit.SelectedValue);
            pw.DescriptionofWorks = txtDescription.Text;
            pw.ArrivalDate = Convert.ToDateTime(txtArrivalDate.Text + " " + (string.IsNullOrEmpty(txtArrivalTime.Text) ? "00:00:00" : txtArrivalTime.Text + ":00"));
            pw.Reason = txtReason.Text;
            pw.Notes = txtNotes.Text;
          
            PermitToWorkBAL.PermittoWorkUpdate(pw);
            if (IsPermitToWorkChanged)
                AddPermitToWorkJournal(id,CurrentDate);
            //upload files
            UploadFiles(id);
            BindData(id);
            //history1.DisplayHistory(id);
            iframe_DisplayHistory(id);
            if (old_status != cd.StatusID)
            {
                sendmail(false);
            }
           // sendDistributionListmail(cd);
            if (ddlStatus.SelectedItem.Text == "Closed")
            {
                //reload the same page
                Response.Redirect(Request.Url.AbsoluteUri, false);
            }
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
            PermitToWork pw = PermitToWorkBAL.SelectbyCallId(cid);


            if (cd != null && pw != null)
            {
                h_cid.Value = cid.ToString();
                ccdCompany.SelectedValue = Convert.ToString(cd.CompanyID);
                ccdSite.SelectedValue = Convert.ToString(cd.SiteID);
                ccdName.SelectedValue = Convert.ToString(cd.RequesterID);
                ccdType.SelectedValue = Convert.ToString(cd.RequestTypeID);
                ccdStatus.SelectedValue = Convert.ToString(cd.StatusID);
                txtFrom.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(pw.FromDateofWork).Remove(10));
                txtFromTime.Text = string.Format("{0:HH:mm}", pw.FromDateofWork);
                txtTo.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(pw.ToDateofWork).Remove(10));
                txtToTime.Text = string.Format("{0:HH:mm}", pw.ToDateofWork);
                ccdArea.SelectedValue = Convert.ToString(pw.Area);
                txtLoggedDateTime.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), cd.LoggedDate.ToString().Remove(10));
                txtLoggedTime.Text = string.Format("{0:HH:mm}", cd.LoggedDate);
                ccdPermit.SelectedValue = Convert.ToString(pw.TypeofPermit);
                txtDescription.Text = pw.DescriptionofWorks;
                txtArrivalDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(pw.ArrivalDate).Remove(10));
                txtArrivalTime.Text = string.Format("{0:HH:mm}",pw.ArrivalDate);
                txtReason.Text = pw.Reason;
                txtNotes.Text = pw.Notes;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void sendmail(bool  check_insert)
    {
        try
        {
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();
            //List<string> site = new List<string>();
            //site = ws.GetSiteDetailsbyId(int.Parse(ddlSite.SelectedValue));
        //    string area = ws.GetAreaById(int.Parse(ddlArea.SelectedValue));
            PermitType pt = PermitTypeBAL.SelectbyId(int.Parse(ddlPermit.SelectedValue));
            EmailFooter ef = new EmailFooter();
            ef = FooterEmail.EmailFooter_selectByID(int.Parse(ddlTypeofRequest.SelectedValue));
            PortfolioContact pc = ws.GetContactDetails(int.Parse(ddlRequestersName.SelectedValue));
            AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select();
            string subject = string.Empty;
            if (check_insert)
             subject = "Ticket Reference: " + h_cid .Value+ " Confirmation of Permit to Work";
            else
                subject = "Ticket Reference: " + h_cid.Value + " Status : " + ddlStatus.SelectedItem.Text + " Updated Details of Permit to Work";
            Emailer em = new Emailer();
            string body = em.ReadFile("~/WF/DC/EmailTemplates/PermitToWorkMail.htm");
            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            body = body.Replace("[user]", ddlRequestersName.SelectedItem.Text);
            body = body.Replace("[sitename]", ddlSite.SelectedItem.Text);
            //if (!string.IsNullOrEmpty(site[1]))
            //    body = body.Replace("[address1]", "<br />" + site[1]);
            //else
            //    body = body.Replace("[address1]", site[1]);
            //if (!string.IsNullOrEmpty(site[2]))
            //    body = body.Replace("[address2]", "<br />" + site[2]);
            //else
            //    body = body.Replace("[address2]", site[2]);
            //if (!string.IsNullOrEmpty(site[3]))
            //    body = body.Replace("[address3]", "<br />" + site[3]);
            //else
            //    body = body.Replace("[address3]", site[3]);
            //if (!string.IsNullOrEmpty(site[4]))
            //    body = body.Replace("[postcode]", "<br />" + site[4]);
            //else
            //    body = body.Replace("[postcode]", site[4]);
            body = body.Replace("[reference]", h_cid.Value);
            body = body.Replace("[fromdate]", txtFrom.Text + " " + (string.IsNullOrEmpty(txtFromTime.Text) ? "00:00:00" : txtFromTime.Text + ":00"));
            body = body.Replace("[todate]", txtTo.Text + " " + (string.IsNullOrEmpty(txtToTime.Text) ? "00:00:00" : txtToTime.Text + ":00"));
            body = body.Replace("[area]", ddlArea.SelectedItem.Text);
            body = body.Replace("[permit]",pt.Type);
            body = body.Replace("[adate]", txtArrivalDate.Text + " " + (string.IsNullOrEmpty(txtArrivalTime.Text) ? "00:00:00" : txtArrivalTime.Text + ":00"));
            body = body.Replace("[contact]", pc.Email);
            body = body.Replace("[mobile]", pc.Telephone);
            body = body.Replace("[description]", txtDescription.Text);
            body = body.Replace("[reason]", txtReason.Text);
            body = body.Replace("[footer]", HttpUtility.HtmlDecode(ef.EmailFooter1 == null ? string.Empty : ef.EmailFooter1));
            body = body.Replace("[adminemail]", ae.EmailAddress);
            body = body.Replace("[Status]", ddlStatus.SelectedItem.Text);
            body = body.Replace("[Company]", ddlRequestersCompany.SelectedItem.Text);
            em.SendingMail(fromemailid, subject, body, pc.Email);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void sendDistributionListmail(CallDetail cd)
    {
        try
        {
            List<int> idlist = SecurityAccessMail.GetIds(2); //2 = Permit to work
            if (idlist.Count > 0)
            {
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                DC.SRV.WebService ws = new DC.SRV.WebService();

                string subject = string.Format("Ticket Reference: " + h_cid.Value + " {0} {1} has requested permit to work on {2} at {3}", ddlRequestersCompany.SelectedItem.Text, ddlRequestersName.SelectedItem.Text, DateTime.Now.ToString(Deffinity.systemdefaults.GetDateformat()), DateTime.Now.ToString("HH:mm"));
                Emailer em = new Emailer();
                string body = em.ReadFile("~/WF/DC/EmailTemplates/PermitToWorkDistributionListMail.htm");
                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                body = body.Replace("[Customer]", ddlRequestersCompany.SelectedItem.Text);

                body = body.Replace("[Name]", ddlRequestersName.SelectedItem.Text);
                body = body.Replace("[DOD]", cd.LoggedDate.Value.ToString(Deffinity.systemdefaults.GetDateformat()));
                body = body.Replace("[Time]", cd.LoggedDate.Value.ToString("HH:mm"));
                body = body.Replace("[desc]", txtDescription.Text);
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        //history1.DisplayHistory(int.Parse(h_cid.Value));
        iframe_DisplayHistory(int.Parse(h_cid.Value));
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
                    if (callid > 0)
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
    protected void GvDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Download")
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
            else if (e.CommandName == "DeleteFile")
            {
                GridViewRow gvrow = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                string ID = GvDocuments.DataKeys[gvrow.RowIndex].Values[0].ToString();
                string contenttype = GvDocuments.DataKeys[gvrow.RowIndex].Values[1].ToString();
                string filename = GvDocuments.DataKeys[gvrow.RowIndex].Values[2].ToString();
                string[] ex = filename.Split('.');
                string ext = ex[ex.Length - 1];
                string filepath = string.Format("~/WF/UploadData/DC/{0}.{1}", e.CommandArgument.ToString(), ext);
                File.Delete(Server.MapPath(filepath));
                DocumentsBAL.DocumentsDelete(int.Parse(ID));
                BindDocuments(int.Parse(h_cid.Value));
                //history1.DisplayHistory(int.Parse(h_cid.Value));
                iframe_DisplayHistory(int.Parse(h_cid.Value));
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
        ccdStatus.SelectedValue = "0";
        ccdCompany.SelectedValue = "0";
        ccdName.SelectedValue = "0";
        txtRequestersEmailAddress.Text = string.Empty;
        txtRequestersTelephoneNo.Text = string.Empty;
        txtReason.Text = string.Empty;
        txtFrom.Text = string.Empty;
        txtTo.Text = string.Empty;
        txtArrivalDate.Text = string.Empty;
        txtArrivalTime.Text = string.Empty;
        ccdArea.SelectedValue = "0";
        ccdPermit.SelectedValue = "0";
        txtDescription.Text = string.Empty;
        txtNotes.Text = string.Empty;
    }
    private void AddChecklistItems(int cid)
    {
        try
        {
            AssignedChecklist al = AssignedChecklistsBAL.BindAssignedChecklist();
             DC.SRV.WebService ws = new DC.SRV.WebService();
             if (al != null)
             {
                 List<string> strlist = ws.GetItemDescription(al.TemplateID.Value);
                 if (strlist.Count > 0)
                 {
                     foreach (string s in strlist)
                     {
                         ChecklistItem cl = new ChecklistItem();
                         cl.CallID = cid;
                         cl.Status = "In Progress";
                         cl.ItemDescription = s;
                         ChecklistitemsBAL.AddChecklistItems(cl);
                     }
                     //lbtnChecklists.NavigateUrl = string.Format("~/WF/DC/Checklist.aspx?callid={0}", cid);
                    // lbtnPermit.NavigateUrl = string.Format("~/WF/DC/PermitToWork.aspx?callid={0}", cid);
                 }
             }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private bool CheckStatus(int cid)
    {
        bool IsStatusClosed = false;
        try
        {
            List<ChecklistItem> clList = ChecklistitemsBAL.BindChecklistItemsbyCallID(cid);
            if (clList.Count > 0)
            {
                foreach (ChecklistItem cl in clList)
                {
                    if (cl.Status != "Closed")
                    {
                        IsStatusClosed = true;
                        break;
                    }
                }
            }           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return IsStatusClosed;
    }

    private bool CompareCallDetails(CallDetail cd, int sid, int rtid, int stid, int cid, int rid)
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
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return IsChanged;
    }
    private void AddCallDetailsJournal(int cid, CallDetail cd, DateTime ModifiedDate)
    {
        try
        {          
            DC.SRV.WebService ws = new DC.SRV.WebService();
            CallDetailsJournal cdj = new CallDetailsJournal();
            cdj.CallID = cid;
            cdj.SiteID = int.Parse(ddlSite.SelectedValue);
            cdj.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);         
            cdj.StatusID = int.Parse(ddlStatus.SelectedValue);
            cdj.CompanyID = int.Parse(ddlRequestersCompany.SelectedValue);
            cdj.RequesterID = int.Parse(ddlRequestersName.SelectedValue);
            cdj.LoggedDate = cd.LoggedDate;
            cdj.LoggedBy = cd.LoggedBy;
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
    private bool ComparePermitToWork(PermitToWork pw, int cid, DateTime from, DateTime to, int area, int tp, string desc, DateTime ad, string rea, string notes)
    {
        bool IsChanged = false;
        try
        {
            PermitToWork pastInformation = pw;
            if (pastInformation.CallID != cid)
                IsChanged = true;
            else if (pastInformation.FromDateofWork != from)
                IsChanged = true;
            else if (pastInformation.ToDateofWork != to)
                IsChanged = true;
            else if (pastInformation.Area != area)
                IsChanged = true;
            else if (pastInformation.TypeofPermit != tp)
                IsChanged = true;
            else if (pastInformation.DescriptionofWorks != desc)
                IsChanged = true;
            else if (pastInformation.ArrivalDate != ad)
                IsChanged = true;
            else if (pastInformation.Reason != rea)
                IsChanged = true;
            else if (pastInformation.Notes != notes)
                IsChanged = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return IsChanged;
    }
    private void AddPermitToWorkJournal(int cid, DateTime ModifiedDate)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            PermitToWorkJournal pwj = new PermitToWorkJournal();
            pwj.CallID = cid;
            pwj.FromDateofWork = Convert.ToDateTime(txtFrom.Text + " " + (string.IsNullOrEmpty(txtFromTime.Text) ? "00:00:00" : txtFromTime.Text + ":00"));
            pwj.ToDateofWork = Convert.ToDateTime(txtTo.Text + " " + (string.IsNullOrEmpty(txtToTime.Text) ? "00:00:00" : txtToTime.Text + ":00"));
            pwj.Area = int.Parse(ddlArea.SelectedValue);
            pwj.TypeofPermit = int.Parse(ddlPermit.SelectedValue);
            pwj.DescriptionofWorks = txtDescription.Text;
            pwj.ArrivalDate = Convert.ToDateTime(txtArrivalDate.Text + " " + (string.IsNullOrEmpty(txtArrivalTime.Text) ? "00:00:00" : txtArrivalTime.Text + ":00"));
            pwj.Reason = txtReason.Text;
            pwj.ModifiedBy = sessionKeys.UID;
            pwj.ModifiedDate = ModifiedDate;
            pwj.Notes = txtNotes.Text;
            pwj.VisibleToCustomer = false;
            ws.AddPermitToWorkJournal(pwj);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}