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
using DC.Entity;

public partial class Delivery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["tid"] != null)
            {
                int tid = int.Parse(Request.QueryString["tid"].ToString());
                BindData(tid);               
                BindDocuments(tid);
            }
            else
            {
                trticket.Visible = false;
                GvDocuments.DataBind();
            }
            
        }
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
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
            CallDetail cd = new CallDetail();
            DeliveryInformation di = new DeliveryInformation();
            RecievedInformation ri = new RecievedInformation();
          
            bool Is1Pallet = false;
            bool IsOverweight = false;
            cd.SiteID = int.Parse(ddlSite.SelectedValue);
            cd.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
            cd.StatusID = int.Parse(ddlStatus.SelectedValue);
            cd.CompanyID = int.Parse(ddlRequestersCompany.SelectedValue);
            cd.RequesterID = int.Parse(ddlRequestersName.SelectedValue);
            cd.LoggedDate = DateTime.Now;
            cd.LoggedBy = sessionKeys.UID;
            CallDetailsBAL.AddCallDetails(cd);
            int id = cd.ID;
            di.CallID = id;
            di.ArrivalDate = Convert.ToDateTime(txtDateofArrival.Text);
            di.Weight = txtWeight.Text;
            di.NumofBoxes = Convert.ToInt32(txtNumberofBoxes.Text);
            di.DeliveryTypeID = int.Parse(ddlDeliveryType.SelectedValue);
            di.Description = txtDescription.Text;
            if (ddlOver1pallet.SelectedValue == "Yes")
                Is1Pallet = true;
            if (ddlOverWeight.SelectedValue == "Yes")
                IsOverweight = true;
            if (ddlOver1pallet.SelectedValue != "0")
                di.Pallet = Is1Pallet;
            if (ddlOverWeight.SelectedValue != "0")
                di.OverWeight = IsOverweight;
            DeliveryInformationBAL.AddDeliveryInformation(di);
            ri.CallID = id;
            ri.ConditionID = int.Parse(ddlCondition.SelectedValue);
            ri.NumofBoxesRec = Convert.ToInt32(txtNumberofBoxesRec.Text);
            ri.StorageLocationID = int.Parse(ddlStorageLocation.SelectedValue);
            ri.Notes = txtNotes.Text;
            ri.DateRecieved = Convert.ToDateTime(txtDateReceived.Text);
            ri.DaysInStore = Convert.ToInt32(txtDaysinStorage.Text);
            ri.ChargeableDate = Convert.ToDateTime(txtChargeableDate.Text);
            ri.TotalCost = Convert.ToDecimal(txtTotalCost.Text);
            ri.StoragePeriodFrom = Convert.ToDateTime(txtFrom.Text);
            ri.StoragePeriodTo = Convert.ToDateTime(txtTo.Text);
            ri.PeriodCost = Convert.ToDecimal(txtPeriodCost.Text);           
            ReceivedInformationBAL.AddReceivedInformation(ri);
            BindData(id);
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
           CallDetail cd = CallDetailsBAL.SelectbyId(cid);
           DeliveryInformation di = DeliveryInformationBAL.SelectbyCallId(cid);
           RecievedInformation ri = ReceivedInformationBAL.SelectbyCallId(cid);
        
            bool Is1Pallet = false;
            bool IsOverweight = false;
            bool IsCallDetailsChanged = CompareCallDetails(cd, int.Parse(ddlSite.SelectedValue), int.Parse(ddlTypeofRequest.SelectedValue), int.Parse(ddlStatus.SelectedValue), int.Parse(ddlRequestersCompany.SelectedValue), int.Parse(ddlRequestersName.SelectedValue), sessionKeys.UID);
            cd.SiteID = int.Parse(ddlSite.SelectedValue);
            cd.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
            cd.StatusID = int.Parse(ddlStatus.SelectedValue);
            cd.CompanyID = int.Parse(ddlRequestersCompany.SelectedValue);
            cd.RequesterID = int.Parse(ddlRequestersName.SelectedValue);
            cd.LoggedDate = DateTime.Now;
            cd.LoggedBy = sessionKeys.UID;           
            if (IsCallDetailsChanged)
                AddCallDetailsJournal(cid);
            CallDetailsBAL.CallDetailsUpdate(cd);
            int id = cd.ID;
            bool IsDeliveryInformationChanged = CompareDeliveryInformation(di, id, Convert.ToDateTime(txtDateofArrival.Text), txtWeight.Text, Convert.ToInt32(txtNumberofBoxes.Text), int.Parse(ddlDeliveryType.SelectedValue), txtDescription.Text, Is1Pallet, IsOverweight);
            di.CallID = id;
            di.ArrivalDate = Convert.ToDateTime(txtDateofArrival.Text);
            di.Weight = txtWeight.Text;
            di.NumofBoxes = Convert.ToInt32(txtNumberofBoxes.Text);
            di.DeliveryTypeID = int.Parse(ddlDeliveryType.SelectedValue);
            di.Description = txtDescription.Text;
            if (ddlOver1pallet.SelectedValue == "Yes")
                Is1Pallet = true;
            if (ddlOverWeight.SelectedValue == "Yes")
                IsOverweight = true;
            if (ddlOver1pallet.SelectedValue != "0")
                di.Pallet = Is1Pallet;
            if (ddlOverWeight.SelectedValue != "0")
                di.OverWeight = IsOverweight;
           
           if(IsDeliveryInformationChanged)
               AddDeliveryInformationJournal(id);
            DeliveryInformationBAL.DeliveryInformationUpdate(di);
            bool IsReceivedInformationChanged = CompareReceivedInformation(ri, id, int.Parse(ddlCondition.SelectedValue), Convert.ToInt32(txtNumberofBoxesRec.Text), int.Parse(ddlStorageLocation.SelectedValue), txtNotes.Text, Convert.ToDateTime(txtDateReceived.Text), Convert.ToInt32(txtDaysinStorage.Text), Convert.ToDateTime(txtChargeableDate.Text), Convert.ToDecimal(txtTotalCost.Text), Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), Convert.ToDecimal(txtPeriodCost.Text));
            ri.CallID = id;
            ri.ConditionID = int.Parse(ddlCondition.SelectedValue);
            ri.NumofBoxesRec = Convert.ToInt32(txtNumberofBoxesRec.Text);
            ri.StorageLocationID = int.Parse(ddlStorageLocation.SelectedValue);
            ri.Notes = txtNotes.Text;
            ri.DateRecieved = Convert.ToDateTime(txtDateReceived.Text);
            ri.DaysInStore = Convert.ToInt32(txtDaysinStorage.Text);
            ri.ChargeableDate = Convert.ToDateTime(txtChargeableDate.Text);
            ri.TotalCost = Convert.ToDecimal(txtTotalCost.Text);
            ri.StoragePeriodFrom = Convert.ToDateTime(txtFrom.Text);
            ri.StoragePeriodTo = Convert.ToDateTime(txtTo.Text);
            ri.PeriodCost = Convert.ToDecimal(txtPeriodCost.Text);
            
            if (IsReceivedInformationChanged)
                AddReceivedInformationJournal(id);
            ReceivedInformationBAL.ReceivedInformationUpdate(ri);
            BindData(id);
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
            RecievedInformation ri = ReceivedInformationBAL.SelectbyCallId(cid);
            
            if (cd != null && di != null && ri != null)
            {
                trticket.Visible = true;
                ltTicket.Text = cid.ToString();
                h_cid.Value = cid.ToString();
               // ccdCompany.DataBind();
                ccdCompany.SelectedValue = Convert.ToString(cd.CompanyID);
                //ccdSite.DataBind();
                ccdSite.SelectedValue = Convert.ToString(cd.SiteID);
                //ccdName.DataBind();
                //sessionKeys.RID = cd.RequesterID.Value;
                ccdName.SelectedValue = Convert.ToString(cd.RequesterID);
               // ccdType.DataBind();
                ccdType.SelectedValue = Convert.ToString(cd.RequestTypeID);
                //ccdStatus.DataBind();
               // sessionKeys.STID = cd.StatusID.Value;
                ccdStatus.SelectedValue = Convert.ToString(cd.StatusID);
                txtDateofArrival.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(di.ArrivalDate).Replace("00:00:00", " ")); 
                txtWeight.Text = Convert.ToString(di.Weight);
                txtNumberofBoxes.Text = Convert.ToString(di.NumofBoxes);
                ccdDeliveryType.SelectedValue = Convert.ToString(di.DeliveryTypeID);
                txtDescription.Text = di.Description;
                if (di.Pallet == true)
                    ddlOver1pallet.SelectedValue = "Yes";
                else if (di.Pallet == false)
                    ddlOver1pallet.SelectedValue = "No";
                else
                    ddlOver1pallet.SelectedValue = "0";
                if (di.OverWeight == true)
                    ddlOverWeight.SelectedValue = "Yes";
                else if (di.OverWeight == false)
                    ddlOverWeight.SelectedValue = "No";
                else
                    ddlOverWeight.SelectedValue = "0";
                ccdCondition.SelectedValue = Convert.ToString(ri.ConditionID);
                txtNumberofBoxesRec.Text = Convert.ToString(ri.NumofBoxesRec);
                ccdLocation.SelectedValue = Convert.ToString(ri.StorageLocationID);
                txtNotes.Text = ri.Notes;
                txtDateReceived.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ri.DateRecieved).Replace("00:00:00", " "));
                txtDaysinStorage.Text = Convert.ToString(ri.DaysInStore);
                txtChargeableDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ri.ChargeableDate).Replace("00:00:00", " "));
                txtTotalCost.Text = Convert.ToString(ri.TotalCost);
                txtFrom.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ri.StoragePeriodFrom).Replace("00:00:00"," "));
                txtTo.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ri.StoragePeriodTo).Replace("00:00:00", " "));
                txtPeriodCost.Text = Convert.ToString(ri.PeriodCost);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void GvDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string contenttype = GvDocuments.DataKeys[gvrow.RowIndex].Values[0].ToString();
            string filename = GvDocuments.DataKeys[gvrow.RowIndex].Values[1].ToString();
            string[] ex = filename.Split('.');
            string ext =ex[ex.Length - 1];
            string filepath =string.Format("~/WF/UploadData/DC/{0}.{1}", e.CommandArgument.ToString(),ext);
            Response.ContentType = contenttype;
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
            Context.Response.ContentType = "octet/stream";
            Response.TransmitFile(filepath);
            Response.End();
        }
    }
    protected void ImgDeskImageUpload_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (DocumentsUpload.HasFile)
            {
                string filename = DocumentsUpload.FileName;
                string path = Server.MapPath("~/WF/UploadData/DC");
                string contenttype = DocumentsUpload.PostedFile.ContentType;
                string DocumentId = Guid.NewGuid().ToString();
                string ext = Path.GetExtension(DocumentsUpload.FileName);
                string fullname = string.Format("{0}/{1}{2}", path, DocumentId,ext);
                if (h_cid.Value != "0")
                {
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    DocumentsUpload.SaveAs(fullname);
                    Document d = new Document();
                    d.CallID = int.Parse(h_cid.Value);
                    d.DocumentID = DocumentId;
                    d.FileName = filename;
                    d.ContentType = contenttype;
                    DocumentsBAL.AddDocuments(d);
                    BindData(int.Parse(h_cid.Value));
                }
                else
                {
                    InsertData();
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    DocumentsUpload.SaveAs(fullname);
                    Document d = new Document();
                    d.CallID = int.Parse(h_cid.Value);
                    d.DocumentID = DocumentId;
                    d.FileName = filename;
                    d.ContentType = contenttype;
                    DocumentsBAL.AddDocuments(d);
                }              
                BindDocuments(int.Parse(h_cid.Value));
            }
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
        ccdType.SelectedValue = "0";
        ccdStatus.DataBind();
        ccdStatus.SelectedValue = "0";
        ccdCompany.SelectedValue = "0";
        ccdName.DataBind();
        ccdName.SelectedValue = "0";
        txtRequestersEmailAddress.Text = string.Empty;
        txtRequestersTelephoneNo.Text = string.Empty;
        txtDateofArrival.Text = string.Empty;
        txtWeight.Text = string.Empty;
        txtNumberofBoxes.Text = string.Empty;
        ccdDeliveryType.SelectedValue = "0";
        txtDescription.Text = string.Empty;
        ddlOver1pallet.SelectedValue = "0";
            ddlOverWeight.SelectedValue = "0";
            ccdCondition.SelectedValue = "0";
            txtNumberofBoxesRec.Text = string.Empty;
            ccdLocation.SelectedValue = "0";
        txtNotes.Text = string.Empty;
        txtDateReceived.Text = string.Empty;
        txtDaysinStorage.Text = string.Empty;
        txtChargeableDate.Text = string.Empty;
        txtTotalCost.Text = "£";
        txtFrom.Text = string.Empty;
        txtTo.Text = string.Empty;
        txtPeriodCost.Text = "£";
    }
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
    }
    private bool CompareCallDetails(CallDetail cd,int sid,int rtid,int stid,int cid,int rid,int lid)
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
            cdj.CompanyID = int.Parse(ddlRequestersCompany.SelectedValue);
            cdj.RequesterID = int.Parse(ddlRequestersName.SelectedValue);
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
    private bool CompareDeliveryInformation(DeliveryInformation di, int cid, DateTime ad, string wt, int nob, int dtid, string desc,bool pallet,bool ovw)
    {
        bool IsChanged = false;
        try
        {
            DeliveryInformation pastInformation = di;
            if (pastInformation.CallID != cid)
                IsChanged = true;
            else if (pastInformation.ArrivalDate != ad)
                IsChanged = true;
            else if (pastInformation.Weight != wt)
                IsChanged = true;
            else if (pastInformation.NumofBoxes != nob)
                IsChanged = true;
            else if (pastInformation.DeliveryTypeID != dtid)
                IsChanged = true;
            else if (pastInformation.Description != desc)
                IsChanged = true;
            else if (pastInformation.Pallet != pallet)
                IsChanged = true;
            else if (pastInformation.OverWeight != ovw)
                IsChanged = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return IsChanged;
    }
    private void AddDeliveryInformationJournal(int cid)
    {
        try
        {
            bool Is1Pallet = false;
            bool IsOverweight = false;
            DC.SRV.WebService ws = new DC.SRV.WebService();
            DeliveryInformationJournal dij = new DeliveryInformationJournal();
            dij.CallID = cid;
            dij.ArrivalDate = Convert.ToDateTime(txtDateofArrival.Text);
            dij.Weight = txtWeight.Text;
            dij.NumofBoxes = Convert.ToInt32(txtNumberofBoxes.Text);
            dij.DeliveryTypeID = int.Parse(ddlDeliveryType.SelectedValue);
            dij.Description = txtDescription.Text;
            if (ddlOver1pallet.SelectedValue == "Yes")
                Is1Pallet = true;
            if (ddlOverWeight.SelectedValue == "Yes")
                IsOverweight = true;
            if (ddlOver1pallet.SelectedValue != "0")
                dij.Pallet = Is1Pallet;
            if (ddlOverWeight.SelectedValue != "0")
                dij.OverWeight = IsOverweight;
            dij.ModifiedBy = sessionKeys.UID;
            dij.ModifiedDate = DateTime.Now;
            ws.AddDeliveryInformationJournal(dij);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private bool CompareReceivedInformation(RecievedInformation ri, int cid, int cnid, int nbrec, int slid, string notes, DateTime rd, int ds, DateTime cd, decimal tc, DateTime from, DateTime to, decimal pc)
    {
        bool IsChanged = false;
        try
        {
            RecievedInformation pastInformation = ri;
            if (pastInformation.CallID != cid)
                IsChanged = true;
            else if (pastInformation.ConditionID != cnid)
                IsChanged = true;
            else if (pastInformation.NumofBoxesRec != nbrec)
                IsChanged = true;
            else if (pastInformation.StorageLocationID != slid)
                IsChanged = true;
            else if (pastInformation.Notes != notes)
                IsChanged = true;
            else if (pastInformation.DateRecieved != rd)
                IsChanged = true;
            else if (pastInformation.DaysInStore != ds)
                IsChanged = true;
            else if (pastInformation.ChargeableDate != cd)
                IsChanged = true;
            else if (pastInformation.TotalCost != tc)
                IsChanged = true;
            else if (pastInformation.StoragePeriodFrom != from)
                IsChanged = true;
            else if (pastInformation.StoragePeriodTo != to)
                IsChanged = true;
            else if (pastInformation.PeriodCost != pc)
                IsChanged = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return IsChanged;
    }
    private void AddReceivedInformationJournal(int cid)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            RecievedInformationJournal rij = new RecievedInformationJournal();
            rij.CallID = cid;
            rij.ConditionID = int.Parse(ddlCondition.SelectedValue);
            rij.NumofBoxesRec = Convert.ToInt32(txtNumberofBoxesRec.Text);
            rij.StorageLocationID = int.Parse(ddlStorageLocation.SelectedValue);
            rij.Notes = txtNotes.Text;
            rij.DateRecieved = Convert.ToDateTime(txtDateReceived.Text);
            rij.DaysInStore = Convert.ToInt32(txtDaysinStorage.Text);
            rij.ChargeableDate = Convert.ToDateTime(txtChargeableDate.Text);
            rij.TotalCost = Convert.ToDecimal(txtTotalCost.Text);
            rij.StoragePeriodFrom = Convert.ToDateTime(txtFrom.Text);
            rij.StoragePeriodTo = Convert.ToDateTime(txtTo.Text);
            rij.PeriodCost = Convert.ToDecimal(txtPeriodCost.Text);
            rij.ModifiedBy = sessionKeys.UID;
            rij.ModifiedDate = DateTime.Now;
            ws.AddReceivedInformationJournal(rij);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}