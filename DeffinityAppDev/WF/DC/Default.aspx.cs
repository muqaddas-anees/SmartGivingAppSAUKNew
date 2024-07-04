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

public partial class DC_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["callid"] != null)
            {
                int tid = int.Parse(Request.QueryString["callid"].ToString());
                BindData(tid);
                BindDocuments(tid);
            }
            else
            {
                txtCreatedDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                txtLoggedName.Text = sessionKeys.UName;
                GvDocuments.DataBind();
            }

        }

    }

    private void BindData(int tid)
    {
        try
        {
            CallDetail cd = CallDetailsBAL.SelectbyId(tid);
            FLSDetail fd = FLSDetailsBAL.SelectbyId(tid);

            if (cd != null && fd != null)
            {
                htid.Value = tid.ToString();

                ccdType.SelectedValue = Convert.ToString(cd.RequestTypeID);
                ccdStatus.SelectedValue = Convert.ToString(cd.StatusID);
                ccdCompany.DataBind();
                ccdCompany.SelectedValue = Convert.ToString(cd.CompanyID);
                ccdName.DataBind();
                ccdName.SelectedValue = Convert.ToString(cd.RequesterID);
                ccdSite.DataBind();
                ccdSite.SelectedValue = Convert.ToString(cd.SiteID);
                txtLoggedName.Text = CallDetailsBAL.LoggedByName(int.Parse(cd.LoggedBy.ToString()));
                txtCreatedDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), cd.LoggedDate.ToString().Replace("00:00:00", " "));
                ccdSubject.SelectedValue = Convert.ToString(fd.SubjectID);
                txtDetails.Text = fd.Details;
                ccdAssignedDept.SelectedValue = Convert.ToString(fd.DepartmentID);
                ccdAssignedName.SelectedValue = Convert.ToString(fd.UserID);
                txtTimeAccumulated.Text = fd.TimeAccumulated;
                txtTimeWorked.Text = fd.TimeWorked;
                txtSeheduledDateTime.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), fd.ScheduledDate.ToString().Replace("00:00:00", " "));

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void AddRecord()
    {
        try
        {
            /* Add Call Details */
            CallDetail cd = new CallDetail();

            cd.SiteID = int.Parse(ddlSite.SelectedValue);
            cd.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
            cd.StatusID = int.Parse(ddlStatus.SelectedValue);
            cd.CompanyID = int.Parse(ddlCompany.SelectedValue);
            cd.RequesterID = int.Parse(ddlName.SelectedValue);
            cd.LoggedBy = sessionKeys.UID;
            cd.LoggedDate = Convert.ToDateTime(txtCreatedDate.Text);
            CallDetailsBAL.AddCallDetails(cd);
            int id = cd.ID;

            /*Add FLS Details*/
            FLSDetail fd = new FLSDetail();
            fd.CallID = id;
            fd.SubjectID = int.Parse(ddlSubject.SelectedValue);
            fd.Details = txtDetails.Text.Trim();
            fd.ScheduledDate = Convert.ToDateTime(txtSeheduledDateTime.Text);
            fd.TimeAccumulated = txtTimeAccumulated.Text.Trim();
            fd.TimeWorked = txtTimeWorked.Text.Trim();
            fd.DepartmentID = int.Parse(ddlAssignedtoDept.SelectedValue);
            fd.UserID = int.Parse(ddlAssignedtoName.SelectedValue);
            FLSDetailsBAL.AddFLSDetails(fd);

            BindData(id);


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void UpdateRecord(int tid)  //tid -Ticket Id
    {
        try
        {
            /*Update Call Details*/
            CallDetail cd = CallDetailsBAL.SelectbyId(tid);
            bool isCallDetailsChanged = CompareCallDetails(cd, int.Parse(ddlSite.SelectedValue), int.Parse(ddlTypeofRequest.SelectedValue), int.Parse(ddlStatus.SelectedValue), int.Parse(ddlCompany.SelectedValue), int.Parse(ddlName.SelectedValue), sessionKeys.UID);
            cd.SiteID = int.Parse(ddlSite.SelectedValue);
            cd.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
            cd.StatusID = int.Parse(ddlStatus.SelectedValue);
            cd.CompanyID = int.Parse(ddlCompany.SelectedValue);
            cd.RequesterID = int.Parse(ddlName.SelectedValue);
            //cd.LoggedDate = DateTime.Now;
            cd.LoggedBy = sessionKeys.UID;

            if (isCallDetailsChanged)
                AddCallDetailsJournal(tid);
            CallDetailsBAL.CallDetailsUpdate(cd);
            int id = cd.ID;

            /*Update FLS Details*/

            FLSDetail fd = FLSDetailsBAL.SelectbyId(tid);
            bool isFlsdetailsChanged = CompareFLSDetails(fd, tid, int.Parse(ddlSubject.SelectedValue), txtDetails.Text, txtTimeAccumulated.Text, txtTimeWorked.Text, int.Parse(ddlAssignedtoDept.SelectedValue), int.Parse(ddlAssignedtoName.SelectedValue));
            fd.SubjectID = int.Parse(ddlSubject.SelectedValue);
            fd.Details = txtDetails.Text.Trim();
            fd.ScheduledDate = Convert.ToDateTime(txtSeheduledDateTime.Text);
            fd.TimeAccumulated = txtTimeAccumulated.Text.Trim();
            fd.TimeWorked = txtTimeWorked.Text.Trim();
            fd.DepartmentID = int.Parse(ddlAssignedtoDept.SelectedValue);
            fd.UserID = int.Parse(ddlAssignedtoName.SelectedValue);

            if (isFlsdetailsChanged)
                AddFLSDetails(tid);

            FLSDetailsBAL.FLSDetailsUpdate(fd);
            BindData(id);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }



    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (htid.Value == "0")
            AddRecord();
        else
            UpdateRecord(int.Parse(htid.Value));
    }

    protected void ImgDocumentsUpload_Click(object sender, EventArgs e)
    {
        try
        {

            if (DocumentsUpload.PostedFile != null && DocumentsUpload.PostedFile.FileName != string.Empty)
            {
                lblMsg.Visible = false;
                string filename = DocumentsUpload.FileName;
                string path = Server.MapPath("~/WF/UploadData/DC");
                string contenttype = DocumentsUpload.PostedFile.ContentType;
                string DocumentId = Guid.NewGuid().ToString();
                string fullname = string.Format("{0}/{1}_{2}", path, DocumentId, filename);
                if (htid.Value != "0")
                {
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    DocumentsUpload.SaveAs(fullname);
                    Document d = new Document();
                    d.CallID = int.Parse(htid.Value);
                    d.DocumentID = DocumentId;
                    d.FileName = filename;
                    d.ContentType = contenttype;
                    DocumentsBAL.AddDocuments(d);
                    BindData(int.Parse(htid.Value));
                }
                else
                {
                    AddRecord();
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    DocumentsUpload.SaveAs(fullname);
                    Document d = new Document();
                    d.CallID = int.Parse(htid.Value);
                    d.DocumentID = DocumentId;
                    d.FileName = filename;
                    d.ContentType = contenttype;
                    DocumentsBAL.AddDocuments(d);
                }
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Please select file";
            }
            BindDocuments(int.Parse(htid.Value));
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
        if (e.CommandName.ToString() == "Download")
        {
            Document docs = DocumentsBAL.SelectbyId(int.Parse(e.CommandArgument.ToString()));
            string url = Server.MapPath("..\\Volta_Underwork\\UploadData\\DC\\" + docs.DocumentID + "_" + docs.FileName);
            try
            {
                HttpContext.Current.Response.ContentType = docs.ContentType;
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;FileName=" + docs.FileName);
                HttpContext.Current.Response.WriteFile(url);
                HttpContext.Current.Response.End();
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
        ccdName.DataBind();
        ccdName.SelectedValue = "0";
        ccdSubject.SelectedValue = "0";
        ccdAssignedDept.SelectedValue = "0";
        ccdAssignedName.SelectedValue = "0";
        txtSeheduledDateTime.Text = string.Empty;
        txtDetails.Text = string.Empty;
        txtTimeAccumulated.Text = string.Empty;
        txtTimeWorked.Text = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
    }
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
    private void AddCallDetailsJournal(int cid)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            CallDetailsJournal cdj = new CallDetailsJournal();
            cdj.CallID = cid;
            cdj.SiteID = int.Parse(ddlSite.SelectedValue);
            cdj.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
            cdj.StatusID = int.Parse(ddlStatus.SelectedValue);
            cdj.CompanyID = int.Parse(ddlCompany.SelectedValue);
            cdj.RequesterID = int.Parse(ddlName.SelectedValue);
            cdj.LoggedDate = DateTime.Now;
            cdj.LoggedBy = sessionKeys.UID;
            cdj.ModifiedBy = sessionKeys.UID;
            cdj.ModifiedDate = DateTime.Now;
            ws.AddCallDetailsJournal(cdj);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private bool CompareFLSDetails(FLSDetail fd, int callId, int subId, string details, string timeAcumulated, string timeWorked, int deptId, int userId)
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
            else if (pastDetails.TimeAccumulated != timeAcumulated)
                isChanged = true;
            else if (pastDetails.TimeWorked != timeWorked)
                isChanged = true;
            else if (pastDetails.DepartmentID != deptId)
                isChanged = true;
            else if (pastDetails.UserID != userId)
                isChanged = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return isChanged;

    }
    private void AddFLSDetails(int cid)
    {
        DC.SRV.WebService ws = new DC.SRV.WebService();
        FLSDetailsJournal fj = new FLSDetailsJournal();
        fj.CallID = cid;
        fj.SubjectID = int.Parse(ddlSubject.SelectedValue);
        fj.Details = txtDetails.Text;
        fj.ScheduledDate = Convert.ToDateTime(txtSeheduledDateTime.Text);
        fj.TimeAccumulated = txtTimeAccumulated.Text;
        fj.TimeWorked = txtTimeWorked.Text;
        fj.DepartmentID = int.Parse(ddlAssignedtoDept.SelectedValue);
        fj.UserID = int.Parse(ddlAssignedtoName.SelectedValue);
        fj.ModifiedBy = sessionKeys.UID;
        fj.ModifiedDate = DateTime.Now;
        ws.AddFLSDetailsJournal(fj);
    }
}