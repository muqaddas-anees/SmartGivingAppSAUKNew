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
using System.Text;
using System.Text.RegularExpressions;
using AjaxControlToolkit;
using DC.DAL;


public partial class FLSCustomer1 : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (sessionKeys.PortalUser || sessionKeys.SID == 7)
        { var s = "1"; }
        else
            Response.Redirect("~/WF/Default.aspx");
    }
     protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["callid"] != null)
                {
                    int tid = int.Parse(Request.QueryString["callid"].ToString());
                    if (!CheckValid_User(tid))
                    {
                        Response.Redirect("~/WF/Portal/Home.aspx", false);
                        return;
                    }
                    else
                    {
                        lblTitle.InnerText = "Job Reference " + QueryStringValues.CCID;
                        BindData(tid);
                        BindDocuments(tid);
                        BindGrid();
                        if (hstatusid.Value == "22")
                        {
                            btnSave.Visible = true;
                            btnCancel.Visible = true;
                        }
                        else
                        {
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                        }
                        ImgDocumentsUpload.Visible = false;
                        //pnlAdditionalInformation.Visible = true;
                        //ctr_history.DisplayHistory(tid);
                    }
                }
                else
                {
                    lblTitle.InnerText = Resources.DeffinityRes.ServiceDesk;
                    ccdRequestType.SelectedValue = "2";
                    ccdType.SelectedValue = "6"; // 6 for FLS
                    ccdStatus.SelectedValue = "22"; //22 for Status "New"
                    ccdCompany.SelectedValue = sessionKeys.PortfolioID.ToString();
                    var contactid = CustomerDetailsBAL.GetCustomerUser_ContactID(sessionKeys.UID).ToString();
                    ccdName.SelectedValue = contactid;
                    hcid.Value = contactid;
                    txtCreatedDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now); 
                    txtCreatedTime.Text = string.Format("{0:HH:mm}", DateTime.Now);
                    txtLoggedName.Text = sessionKeys.UName;
                    txtSeheduledDateTime.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                    txtScheduledTime.Text = string.Format("{0:HH:mm}", DateTime.Now);
                    txtScheduledToTime.Text = string.Format("{0:HH:mm}", DateTime.Now.AddHours(2));
                    GvDocuments.DataBind();
                    setDefaultSite();
                   // ctr_history.Visible = false;
                    //pnlAdditionalInformation.Visible = false;
                }
                //CompareValidator3.ValueToCompare = txtCreatedDate.Text;
                
            }
            if (Request.QueryString["callid"] != null)
            {
               // ctr_history.DisplayHistory(Convert.ToInt32(Request.QueryString["callid"]));
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        string[] customer = Regex.Split(ccdCompany.SelectedValue, ":::");
        int customerId = Convert.ToInt32(customer[0] == "" ? "0" : customer[0]);


        //BindPlaceholderFields(customerId); //Convert.ToInt32(ccdCompany.SelectedValue == "" ? "0" : ccdCompany.SelectedValue)
    }
    
    private void BindGrid()
    {
        try
        {
            var jlist = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID);
            var slist = (from j in jlist
                         select new
                         {
                             ImageUrl = "~/WF/Admin/ImageHandler.ashx?type=user&id="+ j.AssignedTechnicianID,
                             Date = j.ScheduledDateTime,
                             UserID = j.AssignedTechnicianID,
                             Name = j.AssignedTechnician
                         }).ToList();
            gridAssigned.DataSource = slist;
            gridAssigned.DataBind();

            if(jlist.FirstOrDefault().AssignedTechnicianID >0)
            {
                pnlSmarttech.Visible = true;
            }

        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
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

    private void BindData(int tid)
    {
        try
        {
            CallDetail cd = CallDetailsBAL.SelectbyId(tid);
            FLSDetail fd = FLSDetailsBAL.SelectbyId(tid);
           
            if (cd != null && fd != null)
            {
                htid.Value = tid.ToString();
                hstatusid.Value = cd.StatusID.Value.ToString();
                h_status.Value = StatusBAL.StatusNamebyId(Convert.ToInt32(cd.StatusID));
                ccdType.SelectedValue = Convert.ToString(cd.RequestTypeID);
                ccdStatus.SelectedValue = Convert.ToString(cd.StatusID);
                ccdCompany.DataBind();
                ccdCompany.SelectedValue = Convert.ToString(cd.CompanyID);
                //ccdName.DataBind();
                ccdName.SelectedValue = Convert.ToString(cd.RequesterID);
                //ccdSite.DataBind();
                //ccdSite.SelectedValue = Convert.ToString(cd.SiteID);
               
                txtLoggedName.Text = CallDetailsBAL.LoggedByName(int.Parse(cd.LoggedBy.ToString()));
                txtCreatedDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), cd.LoggedDate);
                txtCreatedTime.Text = string.Format("{0:HH:mm}", cd.LoggedDate);
               // ccdSubject.SelectedValue = Convert.ToString(fd.SubjectID);
                txtDetails.Text = fd.Details;
                //ccdRequestType.DataBind();
                //ccdRequestType.SelectedValue = Convert.ToString(fd.RequestType);
                //ddlRequestType.SelectedValue = fd.RequestType.ToString();
                //ccdCategory.DataBind();
                //ccdCategory.SelectedValue = (fd.CategoryID.HasValue ? fd.CategoryID.Value : 0).ToString();
                //txtNotes.Text = fd.Notes;
                //ccdAssignedDept.SelectedValue = Convert.ToString(fd.DepartmentID);
                //ccdAssignedName.SelectedValue = Convert.ToString(fd.UserID);
                
                //txtTimeWorked.Text = fd.TimeWorked;
                txtSeheduledDateTime.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), fd.ScheduledDate);
                txtScheduledTime.Text = string.Format("{0:HH:mm}", fd.ScheduledDate);
               // txtTimeAccumulated.Text = DC.SRV.WebService.CalculateTimeAccumulated(tid);

                //Additinal Information
                //txtCustomerCostCode.Text = fd.CustomerCostCode;
                //txtCustomerRef.Text = fd.CustomerReference;
                //txtPOnumber.Text = fd.POnumber;
                //txtStartedDate.Text = fd.DateTimeStarted.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateformat(), fd.DateTimeStarted.Value) : "";
                //txtStartedTime.Text = fd.DateTimeStarted.HasValue ? fd.DateTimeStarted.Value.ToString("HH:mm") : "";
                //txtClosedDate.Text = fd.DateTimeClosed.HasValue ? fd.DateTimeClosed.Value.ToString(Deffinity.systemdefaults.GetDateformat()) : "";
                //txtClosedTime.Text = fd.DateTimeClosed.HasValue ? fd.DateTimeClosed.Value.ToString("HH:mm") : "";
                //txtNotes.Text = fd.Notes;
                //txtTimeWorked.Text = fd.TimeWorked;
                if (fd.Preferreddate2.HasValue)
                {
                    DivPreferreddate2.Visible = true;
                    txtPreferreddate2.Text = fd.Preferreddate2.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateformat(),fd.Preferreddate2.Value) : string.Empty;
                    txtPreferreddatetime2.Text = fd.Preferreddate2.HasValue ? fd.Preferreddate2.Value.ToString(Deffinity.systemdefaults.GetTimeformat()) : string.Empty;

                }
                if (fd.Preferreddate3.HasValue)
                {
                    DivPreferreddate3.Visible = true;
                    txtPreferreddate3.Text = fd.Preferreddate3.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateformat(),fd.Preferreddate3.Value) : DateTime.Now.ToString(Deffinity.systemdefaults.GetDateformat());
                    txtPreferreddatetime3.Text = fd.Preferreddate3.HasValue ? string.Format(Deffinity.systemdefaults.GetStringTimeformat(), fd.Preferreddate3.Value) : DateTime.Now.ToString(Deffinity.systemdefaults.GetTimeformat());
                }
                haddressid.Value = fd.ContactAddressID.HasValue ? fd.ContactAddressID.Value.ToString() : string.Empty;

                txtScheduledToTime.Text = !string.IsNullOrEmpty(fd.ScheduledDatetotime) ? fd.ScheduledDatetotime : string.Empty;
                txtPreferreddatetimeto2.Text = !string.IsNullOrEmpty(fd.Preferreddatetotime2) ? fd.Preferreddatetotime2 : string.Empty;
                txtPreferreddatetimeto3.Text = !string.IsNullOrEmpty(fd.Preferreddatetotime3) ? fd.Preferreddatetotime3 : string.Empty;

                IDCRespository<DC.Entity.CallIdAssociatedProduct> aRepository = new DCRepository<DC.Entity.CallIdAssociatedProduct>();
                var pData = aRepository.GetAll().Where(o => o.Callid == tid).FirstOrDefault();
                if (pData != null)
                {
                    hapid.Value = pData.ProductIds;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private int AddRecord(int ProductId)
    {
        int id = 0;
        try
        {
            DateTime CurrentDateTime = DateTime.Now;
           // DC.SRV.WebService ws = new DC.SRV.WebService();
            /* Add Call Details */
            CallDetail cd = new CallDetail();
            //int rid = ws.GetContactID();
            PortfolioContact pc = CustomerDetailsBAL.GetPortfolioContactDetailsbyID(int.Parse(ddlName.SelectedValue));
            UserMgt.Entity.Contractor c = CustomerDetailsBAL.GetContractorDetailsbyID(sessionKeys.UID);
            string old_email = pc.Email;
            string old_telNo = pc.Telephone;
            cd.SiteID = 0;// int.Parse(ddlSite.SelectedValue);
            cd.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
            //if (int.Parse(ddlStatus.SelectedValue) >0)
            cd.StatusID = JobStatus.New;  //int.Parse(ddlStatus.SelectedValue);
            cd.CompanyID = sessionKeys.PortfolioID;
            int rid = int.Parse(ddlName.SelectedValue);
            cd.RequesterID = int.Parse(ddlName.SelectedValue);
            cd.LoggedBy = sessionKeys.UID;
           
            cd.LoggedDate = Convert.ToDateTime(Request.Form[txtCreatedDate.UniqueID] + " " + Request.Form[txtCreatedTime.UniqueID] + ":00");
            CallDetailsBAL.AddCallDetails(cd);
            id = cd.ID;
            AddCallDetailsJournal(cd, CurrentDateTime);

            /*Add FLS Details*/
            FLSDetail fd = new FLSDetail();
            fd.CallID = id;
            //string subject_id =  string.IsNullOrEmpty(ddlSubject.SelectedValue) || ddlSubject.SelectedValue == "[Loading subject...]" ? "0": ddlSubject.SelectedValue;
            
            fd.SubjectID = 0;
            fd.Details = txtDetails.Text.Trim();
            fd.ScheduledDate = Convert.ToDateTime(txtSeheduledDateTime.Text + " " + (string.IsNullOrEmpty(txtScheduledTime.Text) ? "00:00:00" : txtScheduledTime.Text + ":00"));
            fd.ScheduledEndDateTime = Convert.ToDateTime(txtSeheduledDateTime.Text + " " + (string.IsNullOrEmpty(txtScheduledTime.Text) ? "00:00:00" : txtScheduledTime.Text + ":00")).AddHours(2);
            fd.TimeAccumulated = ""; //txtTimeAccumulated.Text.Trim();
            fd.TimeWorked = string.Empty;
            //fd.Notes = txtNotes.Text.Trim();
            fd.RequestType = FLSDetailsBAL.AddDefaultTypeofRequest(sessionKeys.PortfolioID);
            //fd.RequestType = int.Parse(ddlRequestType.SelectedValue);
            fd.CategoryID = 0;// Convert.ToInt32(ddlCategory.SelectedValue);
            fd.DateTimeStarted = cd.LoggedDate;
            //fd.DepartmentID = int.Parse(ddlAssignedtoDept.SelectedValue);
            //fd.UserID = int.Parse(ddlAssignedtoName.SelectedValue);
            //fd.Notes = txtNotes.Text.Trim();
            ////fd.RAGStatus = ddlRAGStatus.SelectedValue;
            //fd.POnumber = txtPOnumber.Text.Trim();
            ////Newly added
            //fd.CustomerReference = txtCustomerRef.Text.Trim();
            //fd.CustomerCostCode = txtCustomerCostCode.Text.Trim();
            //18 - Customer Portal
            fd.SourceOfRequestID = 0;
            fd.ContactAddressID = Convert.ToInt32(haddressid.Value);
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
            FLSDetailsBAL.AddFLSDetails(fd);

            AddFLSDetailsJournal(id, CurrentDateTime,fd);
            /*Add FLS Time Details ie,Time Accumulated */

            FLSTimeDetail ft = new FLSTimeDetail();
            ft.CallID = id;
            ft.Status = ddlStatus.SelectedItem.Text;
            ft.StatusTime = DateTime.Now;
            FLSTimeDetailsBAL.AddFLSTimeDetail(ft);
            if (ProductId > 0)
                SaveProductToCallid(ProductId.ToString(), id);

            //upload files
            UploadFiles(id);

            BindData(id);

            //if ((old_email != txtReqEmailAddress.Text) || (old_telNo != txtReqTelNo.Text))
            //{
            //    CustomerDetailsBAL.Update_ProfileDetails(int.Parse(ddlName.SelectedValue), sessionKeys.UID, txtReqEmailAddress.Text, txtReqTelNo.Text);
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return id;
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
                   // lblInAssetPopUp.Text = "Inserted successfully.";
                }
                else
                {
                    callId_Product.ProductIds = ProductIds;
                    dc.SubmitChanges();
                    //lblInAssetPopUp.ForeColor = System.Drawing.Color.Green;
                   // lblInAssetPopUp.Text = "Updated successfully.";
                }
            }
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
            DateTime CurrentDateTime = DateTime.Now;
           //// DC.SRV.WebService ws = new DC.SRV.WebService();
           // /*Update Call Details*/
           // CallDetail cd = CallDetailsBAL.SelectbyId(tid);
            
           //// int rid = ws.GetContactID();
           // bool isCallDetailsChanged = CompareCallDetails(cd, int.Parse(ddlSite.SelectedValue), int.Parse(ddlStatus.SelectedValue),int.Parse(ddlName.SelectedValue));
           // bool isStatusChanged = CompareStatusChanged(cd, int.Parse(ddlStatus.SelectedValue));
           // cd.SiteID = int.Parse(ddlSite.SelectedValue);
           // cd.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
           // cd.StatusID = int.Parse(ddlStatus.SelectedValue);
           // cd.CompanyID = int.Parse(ddlCompany.SelectedValue);
           // cd.RequesterID = int.Parse(ddlName.SelectedValue);
           // //cd.LoggedDate = DateTime.Now;
           // //cd.LoggedBy = sessionKeys.UID;
           // if (isCallDetailsChanged)
           //     AddCallDetailsJournal(cd,CurrentDateTime);
           // CallDetailsBAL.CallDetailsUpdate(cd);
           // int id = cd.ID;

            /*Update FLS Details*/

            FLSDetail fd = FLSDetailsBAL.SelectbyId(tid);
            FLSDetail fdj = fd;
            //fd.SubjectID = int.Parse(ddlSubject.SelectedValue);
            //fd.Details = txtDetails.Text.Trim();
            //fd.Notes = txtNotes.Text.Trim();
            
            fd.ScheduledDate = Convert.ToDateTime(txtSeheduledDateTime.Text + " " + (string.IsNullOrEmpty(txtScheduledTime.Text) ? "00:00:00" : txtScheduledTime.Text + ":00"));
            if(!string.IsNullOrEmpty(txtPreferreddate2.Text.Trim()))
                fd.Preferreddate2 = Convert.ToDateTime(txtPreferreddate2.Text + " " + (string.IsNullOrEmpty(txtPreferreddatetime2.Text) ? "00:00:00" : txtPreferreddatetime2.Text + ":00"));
            if (!string.IsNullOrEmpty(txtPreferreddate3.Text.Trim()))
                fd.Preferreddate3 = Convert.ToDateTime(txtPreferreddate3.Text + " " + (string.IsNullOrEmpty(txtPreferreddatetime3.Text) ? "00:00:00" : txtPreferreddatetime3.Text + ":00"));
            if (!string.IsNullOrEmpty(txtScheduledToTime.Text))
                fd.ScheduledDatetotime = !string.IsNullOrEmpty(txtScheduledToTime.Text) ? txtScheduledToTime.Text : string.Empty;
            if (!string.IsNullOrEmpty(txtPreferreddatetimeto2.Text))
                fd.Preferreddatetotime2 = !string.IsNullOrEmpty(txtPreferreddatetimeto2.Text) ? txtPreferreddatetimeto2.Text : string.Empty;
            if (!string.IsNullOrEmpty(txtPreferreddatetimeto3.Text))
                fd.Preferreddatetotime3 = !string.IsNullOrEmpty(txtPreferreddatetimeto3.Text) ? txtPreferreddatetimeto3.Text : string.Empty;
            //fd.TimeAccumulated = txtTimeAccumulated.Text.Trim();
            //fd.TimeWorked = txtTimeWorked.Text.Trim();
           // fd.DepartmentID = int.Parse(ddlAssignedtoDept.SelectedValue);
           // fd.UserID = int.Parse(ddlAssignedtoName.SelectedValue);
            FLSDetailsBAL.FLSDetailsUpdate(fd);

            bool isFlsdetailsChanged = CompareFLSDetails(fdj, string.Empty, fd.ScheduledDate, fd.Preferreddate2, fd.Preferreddate3,txtScheduledToTime.Text.Trim(), txtPreferreddatetimeto2.Text.Trim(), txtPreferreddatetimeto3.Text.Trim());
            if (isFlsdetailsChanged)
                AddFLSDetailsJournal(tid, CurrentDateTime, fd);
            //if (isStatusChanged)
            //{
            //    FLSTimeDetail ft = new FLSTimeDetail();
            //    ft.CallID = tid;
            //    ft.Status = ddlStatus.SelectedItem.Text;
            //    ft.StatusTime = DateTime.Now;
            //    FLSTimeDetailsBAL.AddFLSTimeDetail(ft);
            //}
            //upload files
            UploadFiles(tid);
            BindData(tid);
          
            //ctr_history.DisplayHistory(tid);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (htid.Value == "0")
            {
                int id = AddRecord(0);
                // SavePlaceholderData(id, Convert.ToInt32(ddlCompany.SelectedValue));
                SendMailtoRequester(int.Parse(ddlName.SelectedValue), id);
                SendDistributionMail(id);
                Response.Redirect("~/WF/Portal/Home.aspx", false);
            }
            else
            {
                UpdateRecord(int.Parse(htid.Value));
                //Response.Redirect("~/DC/FLSCustomer.aspx?callid=" + int.Parse(htid.Value));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region Documents
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
                            ImageManager.Save_FlsCustomerFiles(bytes, DocumentId );
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
            BindDocuments(callid);
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
        if (e.CommandName.ToString() == "del")
        {
            var docID = Convert.ToInt32(e.CommandArgument.ToString());
            DocumentsBAL.DocumentsDelete(docID);
            lblSuccessMsg.Visible = true;
            lblSuccessMsg.Text = "Documnet deleted successfully";
            BindDocuments(QueryStringValues.CallID);
            
        }

        else if (e.CommandName.ToString() == "Download")
        {
            try
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();

                GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string contenttype = GvDocuments.DataKeys[gvrow.RowIndex].Values[1].ToString();
                string filename = GvDocuments.DataKeys[gvrow.RowIndex].Values[2].ToString();
                string[] ex = filename.Split('.');
                string ext = ex[ex.Length - 1];
                string filepath = string.Format("~/WF/UploadData/DC/{0}.{1}", e.CommandArgument.ToString(),ext);
                Response.ContentType = contenttype;
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                Context.Response.ContentType = "octet/stream";
                Response.TransmitFile(filepath);
                Response.Flush();
                Response.End();

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        
    }
    #endregion
    private void ClearFields()
    {
        //ccdSite.SelectedValue = "0";
        ccdType.SelectedValue = "0";
        ccdStatus.DataBind();
        ccdStatus.SelectedValue = "0";
        //ccdCompany.SelectedValue = "0";
        //ccdName.DataBind();
        //ccdName.SelectedValue = "0";
        //ccdSubject.SelectedValue = "0";
       
        txtSeheduledDateTime.Text = string.Empty;
        txtDetails.Text = string.Empty;
        //txtTimeAccumulated.Text = string.Empty;
       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
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
    private void AddCallDetailsJournal(CallDetail cd,DateTime ModifiedDate)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            CallDetailsJournal cdj = new CallDetailsJournal();
            cdj.CallID = cd.ID;
            cdj.SiteID = 0;
            cdj.RequestTypeID = cd.RequestTypeID;
            cdj.StatusID = cd.StatusID;
            cdj.CompanyID = cd.CompanyID;
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
    private bool CompareFLSDetails(FLSDetail fd, string notes, DateTime? pd1, DateTime? pd2, DateTime? pd3, string sdt, string pdt2, string pdt3)
    {
        bool isChanged = false;
        try
        {
            FLSDetail pastDetails = fd;
            //if (pastDetails.CallID != callId)
            //    isChanged = true;
            //else if (pastDetails.SubjectID != subId)
            //    isChanged = true;
            //else if (pastDetails.Details != details)
            //    isChanged = true;
            if (pastDetails.ScheduledDate != pd1)
                isChanged = true;
            else if (pastDetails.Preferreddate2 != pd2)
                isChanged = true;
            else if (pastDetails.Preferreddate3 != pd3)
                isChanged = true;
             else if (pastDetails.Notes != notes)
                isChanged = true;
            else if (pastDetails.ScheduledDatetotime != sdt)
                isChanged = true;
            else if (pastDetails.Preferreddatetotime2 != pdt2)
                isChanged = true;
            else if (pastDetails.Preferreddatetotime3 != pdt3)
                isChanged = true;
          
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return isChanged;

    }
    private void AddFLSDetailsJournal(int cid,DateTime ModifiedDate,FLSDetail fd)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            FLSDetailsJournal fj = new FLSDetailsJournal();
            fj.CallID = cid;
            fj.SubjectID = 0;
            fj.Details = fd.Details;
            fj.ScheduledDate = fd.ScheduledDate;
            fj.Preferreddate2 = fd.Preferreddate2;
            fj.Preferreddate3 = fd.Preferreddate3;
            fj.TimeAccumulated = fd.TimeAccumulated;
            //fj.TimeWorked = string.Empty;
            fj.ModifiedBy = sessionKeys.UID;
            //fj.Notes = txtNotes.Text;
            fj.ModifiedDate = ModifiedDate;
            fj.VisibleToCustomer = true;
            fj.ScheduledDatetotime = fd.ScheduledDatetotime;
            fj.Preferreddatetotime2 = fd.Preferreddatetotime2;
            fj.Preferreddatetotime3 = fd.Preferreddatetotime3;
            fd.RequestType = fd.RequestType;
            fd.CategoryID = 0;
            ws.AddFLSDetailsJournal(fj);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region Mail
    private void SendDistributionMail(int Callid)
    {
        try
        {
            var ccid = FLSDetailsBAL.GetCallIDByCustomer(Callid);
            List<int> idlist = SecurityAccessMail.GetIds(6, sessionKeys.PortfolioID); // 6 for FLS
            if (idlist.Count > 0)
            {
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                DC.SRV.WebService ws = new DC.SRV.WebService();

                string subject = "Job Reference: " + ccid + " Customer has raised a new ticket.";
                Emailer em = new Emailer();
                string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSDistributionMail.htm");
                body = body.Replace("[mail_head]", "Service Desk Request");
                //body = body.Replace("[Company]", ddlCompany.SelectedItem.Text);
                body = body.Replace("[Status]", ddlStatus.SelectedItem.Text);
                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                //body = body.Replace("[sitename]", ddlSite.SelectedItem.Text);
                //body = body.Replace("[reference]", htid.Value);
                //body = body.Replace("[details]", txtDetails.Text);
                //body = body.Replace("[seduledate]", txtSeheduledDateTime.Text + " " + txtScheduledTime.Text);
                //body = body.Replace("[notes]", "");
                StringBuilder sb = new StringBuilder();
                //sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Customer:", ddlCompany.SelectedItem.Text));
                //sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Site information:", ddlSite.SelectedItem.Text));
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Job Reference:", "" + ccid));
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Status:", ddlStatus.SelectedItem.Text));
                // if (GetFieldVisibility("details", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Details of the request:", txtDetails.Text));
                // if (GetFieldVisibility("Scheduled Date/Time", fieldList))
                sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Preferred Date/Time for Service 1:", txtSeheduledDateTime.Text + " " + txtScheduledTime.Text + (!string.IsNullOrEmpty(txtScheduledToTime.Text) ? txtScheduledToTime.Text : string.Empty)));
                if (!string.IsNullOrEmpty(txtPreferreddate2.Text))
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Preferred Date/Time for Service 2:", txtPreferreddate2.Text + " " + txtPreferreddatetime2.Text + (!string.IsNullOrEmpty(txtPreferreddatetimeto2.Text) ? txtPreferreddatetimeto2.Text : string.Empty)));
                if (!string.IsNullOrEmpty(txtPreferreddate3.Text))
                    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Preferred Date/Time for Service 3:", txtPreferreddate3.Text + " " + txtPreferreddatetime3.Text + (!string.IsNullOrEmpty(txtPreferreddatetimeto3.Text) ? txtPreferreddatetimeto3.Text : string.Empty)));
                //custom fields
               
                    string[] customer = Regex.Split(ccdCompany.SelectedValue, ":::");
                    int customerId = Convert.ToInt32(customer[0] == "" ? "0" : customer[0]);
                    List<FLSAdditionalInfo> flsAdditionalInfoList = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(Callid);
                    List<FLSCustomField> clist = CustomFormDesignerBAL.GetFieldList(customerId,0).ToList();
                    foreach (FLSCustomField c in clist)
                    {
                        string val = flsAdditionalInfoList.Where(p => p.CustomFieldID == c.ID).Select(p => p.CustomFieldValue).FirstOrDefault();
                        string Lname = c.LabelName;
                        sb.Append(string.Format("<tr><td>{0}:</td><td><b>{1}</b></td><tr>", Lname, val));
                    }

                //if (GetFieldVisibility("Notes", fieldList))
                //    sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Notes:", ""));
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
                //body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                body = body.Replace("[Status]", ddlStatus.SelectedItem.Text);
                body = body.Replace("[datarow]", sb.ToString());
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
    private void SendMailtoRequester(int rid,int Callid)
    {
        try
        {
            var ccid = FLSDetailsBAL.GetCallIDByCustomer(Callid);
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();
            EmailFooter ef = new EmailFooter();
            AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select(sessionKeys.PortfolioID);
            ef = FooterEmail.EmailFooter_selectByID(int.Parse(ddlTypeofRequest.SelectedValue),sessionKeys.PortfolioID);
            PortfolioContact pc = ws.GetContactDetails(rid);

             string[] emails = pc.Email.Split(',');
             foreach (string res_email in emails)
             {
                 string subject = "Job Reference: " + ccid + " Confirmation of Job Request";
                 Emailer em = new Emailer();
                 string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSMail.htm");
                 body = body.Replace("[mail_head]", "Confirmation/Resolution of Job Request");
                 body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                 body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                 body = body.Replace("[user]", pc.Name);
                 //body = body.Replace("[sitename]", ddlSite.SelectedItem.Text);
                 //body = body.Replace("[Company]", ddlCompany.SelectedItem.Text);
                 //body = body.Replace("[subject]", ddlSubject.SelectedItem.Text);
                 body = body.Replace("[Status]", ddlStatus.SelectedItem.Text);
                 //body = body.Replace("[contact]", string.IsNullOrEmpty(pc.Email) ? string.Empty : pc.Email);
                 //body = body.Replace("[mobile]", string.IsNullOrEmpty(pc.Telephone) ? string.Empty : pc.Telephone);
                 //body = body.Replace("[reference]", htid.Value);
                 //body = body.Replace("[details]", txtDetails.Text);
                 //body = body.Replace("[seduledate]", txtSeheduledDateTime.Text + " " + txtScheduledTime.Text);
                 body = body.Replace("[footer]", Server.HtmlDecode(ef == null ? string.Empty : ef.EmailFooter1));
                 //body = body.Replace("[notes]","");s
                 body = body.Replace("[adminemail]", ae == null ? string.Empty : ae.EmailAddress);
                 StringBuilder sb = new StringBuilder();
                 sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Job Reference:", "" + ccid));
                 //sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Customer:", ddlCompany.SelectedItem.Text));
                 // if (GetFieldVisibility("Requesters Email Address", fieldList))
                 sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Contact Email:", string.IsNullOrEmpty(pc.Email) ? string.Empty : pc.Email));
                 //if (GetFieldVisibility("Requesters Telephone No", fieldList))
                 sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Contact Mobile:", string.IsNullOrEmpty(pc.Telephone) ? string.Empty : pc.Telephone));
                 //sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Site information:", ddlSite.SelectedItem.Text));
                 sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Status:", ddlStatus.SelectedItem.Text));
                 // if (GetFieldVisibility("Subject", fieldList))
                 sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Subject:", ddlSubject.SelectedItem.Text));
                 //if (GetFieldVisibility("details", fieldList))
                 sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Details of the request:", txtDetails.Text));
                 // if (GetFieldVisibility("Scheduled Date/Time", fieldList))
                 sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Preferred Date/Time for Service 1:", txtSeheduledDateTime.Text + " " + txtScheduledTime.Text + (!string.IsNullOrEmpty(txtScheduledToTime.Text) ? txtScheduledToTime.Text : string.Empty)));
                 if (!string.IsNullOrEmpty(txtPreferreddate2.Text))
                     sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Preferred Date/Time for Service 2:", txtPreferreddate2.Text + " " + txtPreferreddatetime2.Text + (!string.IsNullOrEmpty(txtPreferreddatetimeto2.Text) ? txtPreferreddatetimeto2.Text : string.Empty)));
                 if (!string.IsNullOrEmpty(txtPreferreddate3.Text))
                     sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Preferred Date/Time for Service 3:", txtPreferreddate3.Text + " " + txtPreferreddatetime3.Text + (!string.IsNullOrEmpty(txtPreferreddatetimeto3.Text) ? txtPreferreddatetimeto3.Text : string.Empty)));
                 //if (GetFieldVisibility("Notes", fieldList))
                 sb.Append(string.Format("<tr><td>{0}</td><td><b>{1}</b></td><tr>", "Notes:", string.Empty));
                 //custom fields

                 string[] customer = Regex.Split(ccdCompany.SelectedValue, ":::");
                 int customerId = Convert.ToInt32(customer[0] == "" ? "0" : customer[0]);
                 List<FLSAdditionalInfo> flsAdditionalInfoList = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(Callid);
                 List<FLSCustomField> clist = CustomFormDesignerBAL.GetFieldList(customerId,0).ToList();
                 foreach (FLSCustomField c in clist)
                 {
                     string val = flsAdditionalInfoList.Where(p => p.CustomFieldID == c.ID).Select(p => p.CustomFieldValue).FirstOrDefault();
                     string Lname = c.LabelName;
                     sb.Append(string.Format("<tr><td>{0}:</td><td><b>{1}</b></td><tr>", "" + Lname, val));
                 }

                 body = body.Replace("[datarow]", sb.ToString());
                 body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                 em.SendingMail(fromemailid, subject, body, res_email);
             }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

   

    protected void btnAddNotes_Click(object sender, EventArgs e)
    {
        
    }

    protected void gridAssigned_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Track")
            {
                Response.Redirect(String.Format( "~/WF/DC/ShowMap.aspx?Callid={0}&UserID={1}&CCID={2}",QueryStringValues.CallID,e.CommandArgument.ToString(),QueryStringValues.CCID), false);
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}