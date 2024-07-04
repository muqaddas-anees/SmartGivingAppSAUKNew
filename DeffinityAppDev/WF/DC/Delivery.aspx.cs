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
using DC.BAL;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System.Data;
using System.Text;
public partial class DC_Delivery : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (sessionKeys.SID == 9)
        {
            //this.Page.MasterPageFile = "~/DeffinityResourceTab.master";
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
                CompareVal_Date.ValueToCompare = DateTime.Now.ToShortDateString();
                cv_datecheck.ValueToCompare = DateTime.Now.ToShortDateString();
               // Master.PageHead = "Delivery";

               
                if (Request.QueryString["callid"] != null)
                {
                    //show Courier update buttom
                    btnUpdateCourier.Visible = true;
                    Anticipated_HideDateValidation();
                    int tid = int.Parse(Request.QueryString["callid"].ToString());
                    lblTitle.InnerText = "Delivery - Ticket Reference " + tid;
                    BindData(tid);
                    BindDocuments(tid);
                    RecivedItems_display(ccdStatus.SelectedValue);
                    //tablereceived.Visible = true;
                    //history1.Visible = true;
                    //HistoryBind(tid);
                    iframe_DisplayHistory(tid);
                    pnlNotes.Visible = true;
                }
                else
                {
                    lblTitle.InnerText = "Delivery ";
                    //tablereceived.Visible = false;
                    txtLoggedDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                    txtLoggedTime.Text = string.Format("{0:HH:mm}", DateTime.Now);
                    GvDocuments.DataBind();
                    setDefaultSite();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    private void Anticipated_HideDateValidation()
    {
        CompareVal_Date.Visible = false;
    }
    private void iframe_DisplayHistory(int tid)
    {
        iframe1.Attributes.Add("src", string.Format("/WF/DC/HistoryDisplay.aspx?type=delivery&callid={0}&d={1}", tid.ToString(), string.Format("{0:yyyyMMddHHmmss}", DateTime.Now)));
    }
    //private void HistoryBind(int tid)
    //{
    //    history1.CallID = tid;
    //    //history1.DataBind();
    //    history1.DisplayHistory(tid);
    //}
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
            //hide validation message
            Over_validation();

            DateTime CurrentDate = DateTime.Now;
            CallDetail cd = new CallDetail();
            DeliveryInformation di = new DeliveryInformation();

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
            AddCallDetailsJournal(cd.ID, cd, CurrentDate);
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
            di.Notes = txtNotes.Text;
            di.ItemWeight = Convert.ToInt32(ddlItemWeight.SelectedValue);
            DeliveryInformationBAL.AddDeliveryInformation(di);
            AddDeliveryInformationJournal(id, CurrentDate);
            //upload files
            UploadFiles(id);
            lblTitle.InnerText = "Delivery - Ticket Reference " + id;
            //disable Anticipated date validation
            //Anticipated_HideDateValidation();
            BindData(id);
           
           

            if (cd.ID > 0)
            {
                //mail to Customer 
                // first time status will be - Awaiting Approval
                sendmail(true, cd, "Awaiting Approval");
                //Mail to Team
                if (Is1Pallet == true && Convert.ToInt32(ddlItemWeight.SelectedValue) >1)
                    sendDistributionListmail(cd, true, "Overweight delivery");
                //else
                //    sendDistributionListmail(cd, true, "Delivery Request");
            }

            //edited by Dinesh on 16/05/2013
            //history1.DisplayHistory(id);
           //HistoryBind(id);
            //iframe_DisplayHistory(id);
            Response.Redirect("~/WF/DC/Delivery.aspx?callid=" + id,false);
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
            //hide validation message
            Over_validation();
            DateTime CurrentDate = DateTime.Now;
            CallDetail cd = CallDetailsBAL.SelectbyId(cid);
            DeliveryInformation di = DeliveryInformationBAL.SelectbyCallId(cid);
            RecievedInformation ri = ReceivedInformationBAL.SelectbyCallId(cid);
            ////if the recived information is not exists then user should not allow status to approve
            //if ((ddlStatus.SelectedItem.Text.ToLower() == "approved") && ri == null)
            //{
            //    lblerr.ForeColor = System.Drawing.Color.Red;
            //    lblerr.Text = "Can't change status to approved with out ";
            //}
            //if Number of boxes is less than number of boxes recived, user can not set Status as Recived
            if (Check_Status_ByItems())
            {
                lblerr.ForeColor = System.Drawing.Color.Red;
                //lblerr.Text = "Please select status - Received (part).";
                lblerr.Text = "Please select Received (Part) in the status field";
            }
            else
            {
                //get old status before update
                int? old_status = cd.StatusID;
                bool Is1Pallet = false;
                bool IsOverweight = false;
                bool IsReceivedInformationChanged = false;
                bool isfirstRevicedEnity = false;
                //int statusid = 1;// 1=Awaiting Approval

                int statusid = int.Parse(ddlStatus.SelectedValue);

                if (ddlOver1pallet.SelectedValue == "Yes")
                    Is1Pallet = true;
                //if (ddlOverWeight.SelectedValue == "Yes")
                //    IsOverweight = true;
                //if (Is1Pallet == false || IsOverweight == false)
                //  statusid =  int.Parse(ddlStatus.SelectedValue);

                bool IsCallDetailsChanged = CompareCallDetails(cd, int.Parse(ddlSite.SelectedValue), int.Parse(ddlTypeofRequest.SelectedValue), statusid, int.Parse(ddlRequestersCompany.SelectedValue), int.Parse(ddlRequestersName.SelectedValue));
                cd.SiteID = int.Parse(ddlSite.SelectedValue);
                cd.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
                //if (Is1Pallet == true && IsOverweight == true)
                //    cd.StatusID = statusid; // 1=Awaiting Approval
                //else
                //Recived boxes or less then we should change the staus to Received(Part)
                int n_boxes = Convert.ToInt32(string.IsNullOrEmpty(txtNumberofBoxes.Text.Trim()) ? "0" : txtNumberofBoxes.Text.Trim());
                int n_reviced_boxes = Convert.ToInt32(string.IsNullOrEmpty(txtNumberofBoxesRec.Text.Trim()) ? "0" : txtNumberofBoxesRec.Text.Trim());
                //if (n_boxes > 0 && n_reviced_boxes > 0 && n_boxes != n_reviced_boxes)
                //{
                //    //ddlStatus.SelectedValue = "4";
                //    ccdStatus.SelectedValue = "4";
                //    cd.StatusID = 4;
                //}
                //else
                //{
                //    cd.StatusID = int.Parse(ddlStatus.SelectedValue);
                //}
                cd.StatusID = int.Parse(ddlStatus.SelectedValue);
                cd.CompanyID = int.Parse(ddlRequestersCompany.SelectedValue);
                cd.RequesterID = int.Parse(ddlRequestersName.SelectedValue);
                if (IsCallDetailsChanged)
                    AddCallDetailsJournal(cid, cd, CurrentDate);
                CallDetailsBAL.CallDetailsUpdate(cd);
                int id = cd.ID;

                bool IsDeliveryInformationChanged = CompareDeliveryInformation(di, id, Convert.ToDateTime(txtDateofArrival.Text), string.Empty, Convert.ToInt32(txtNumberofBoxes.Text), int.Parse(ddlDeliveryType.SelectedValue), txtDescription.Text, Is1Pallet, IsOverweight, txtTrackingNumber.Text, txtCourierCompany.Text, txtNotes.Text,Convert.ToInt32(ddlItemWeight.SelectedValue));
                di.CallID = id;
                di.ArrivalDate = Convert.ToDateTime(txtDateofArrival.Text);
               // di.Weight = txtWeight.Text;
                di.NumofBoxes = Convert.ToInt32(txtNumberofBoxes.Text);
                di.DeliveryTypeID = int.Parse(ddlDeliveryType.SelectedValue);
                di.Description = txtDescription.Text;

                if (ddlOver1pallet.SelectedValue != "0")
                    di.Pallet = Is1Pallet;
                //if (ddlOverWeight.SelectedValue != "0")
                //    di.OverWeight = IsOverweight;
                di.CourierNumber = txtTrackingNumber.Text;
                di.CourierCompany = txtCourierCompany.Text;
                di.Notes = txtNotes.Text;
                di.ItemWeight = Convert.ToInt32(ddlItemWeight.SelectedValue);
                DeliveryInformationBAL.DeliveryInformationUpdate(di);
                if (IsDeliveryInformationChanged)
                    AddDeliveryInformationJournal(id, CurrentDate);

                //check the recived items panle to display
                RecivedItems_display(ddlStatus.SelectedValue);
                //"received (part)"
                //check for recived item panel is visible or not
                try
                {
                    if (int.Parse(!string.IsNullOrEmpty(ddlCondition.SelectedValue)?ddlCondition.SelectedValue:"0") > 0)
                    {
                        try
                        {
                            if (ri != null)
                            {
                                IsReceivedInformationChanged = CompareReceivedInformation(ri, id, int.Parse(ddlCondition.SelectedValue), Convert.ToInt32(txtNumberofBoxesRec.Text), int.Parse(txtCollectedBoxes.Text.Trim()), int.Parse(ddlStorageLocation.SelectedValue), int.Parse(ddlOurSite.SelectedValue), Convert.ToDateTime(txtDateReceived.Text), Convert.ToInt32(txtDaysinStorage.Text), Convert.ToDateTime(txtChargeableDate.Text), Convert.ToDecimal(txtTotalCost.Text), string.IsNullOrEmpty(txtFrom.Text) ? (DateTime?)null : Convert.ToDateTime(txtFrom.Text), string.IsNullOrEmpty(txtTo.Text) ? (DateTime?)null : Convert.ToDateTime(txtTo.Text), string.IsNullOrEmpty(txtPeriodCost.Text) ? (Decimal?)null : Convert.ToDecimal(txtPeriodCost.Text));
                                ri.CallID = id;
                                ri.ConditionID = int.Parse(ddlCondition.SelectedValue);
                                ri.NumofBoxesRec = Convert.ToInt32(txtNumberofBoxesRec.Text);
                                ri.StorageLocationID = int.Parse(ddlStorageLocation.SelectedValue);
                                ri.DateRecieved = Convert.ToDateTime(txtDateReceived.Text);
                                ri.DaysInStore = Convert.ToInt32(txtDaysinStorage.Text);
                                ri.ChargeableDate = Convert.ToDateTime(txtChargeableDate.Text);
                                ri.TotalCost = Convert.ToDecimal(txtTotalCost.Text);

                                ri.StoragePeriodFrom = string.IsNullOrEmpty(txtFrom.Text) ? (DateTime?)null : Convert.ToDateTime(txtFrom.Text);
                                ri.StoragePeriodTo = string.IsNullOrEmpty(txtTo.Text) ? (DateTime?)null : Convert.ToDateTime(txtTo.Text);
                                ri.PeriodCost = string.IsNullOrEmpty(txtPeriodCost.Text) ? (Decimal?)null : Convert.ToDecimal(txtPeriodCost.Text);
                                ri.OurSiteID = int.Parse(ddlOurSite.SelectedValue);
                                ri.NumofBoxesCollected = int.Parse(txtCollectedBoxes.Text.Trim());
                                if (IsReceivedInformationChanged)
                                    AddReceivedInformationJournal(id, CurrentDate);
                                ReceivedInformationBAL.ReceivedInformationUpdate(ri);
                            }
                            else
                            {

                                RecievedInformation rin = new RecievedInformation();
                                rin.CallID = id;
                                rin.ConditionID = int.Parse(ddlCondition.SelectedValue);
                                rin.NumofBoxesRec = Convert.ToInt32(txtNumberofBoxesRec.Text);
                                rin.StorageLocationID = int.Parse(ddlStorageLocation.SelectedValue);
                                rin.DateRecieved = Convert.ToDateTime(txtDateReceived.Text);
                                rin.DaysInStore = Convert.ToInt32(txtDaysinStorage.Text);
                                rin.ChargeableDate = Convert.ToDateTime(txtChargeableDate.Text);
                                rin.TotalCost = Convert.ToDecimal(txtTotalCost.Text);
                                rin.StoragePeriodFrom = string.IsNullOrEmpty(txtFrom.Text) ? (DateTime?)null : Convert.ToDateTime(txtFrom.Text);
                                rin.StoragePeriodTo = string.IsNullOrEmpty(txtTo.Text) ? (DateTime?)null : Convert.ToDateTime(txtTo.Text);
                                rin.PeriodCost = string.IsNullOrEmpty(txtPeriodCost.Text) ? (Decimal?)null : Convert.ToDecimal(txtPeriodCost.Text);
                                rin.OurSiteID = int.Parse(ddlOurSite.SelectedValue);
                                rin.NumofBoxesCollected = int.Parse(txtCollectedBoxes.Text.Trim());
                                ReceivedInformationBAL.AddReceivedInformation(rin);
                                AddReceivedInformationJournal(id, CurrentDate);
                                //set true insert first time
                                isfirstRevicedEnity = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.LogException(ex.Message, "Recived item insert update");
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.LogException("Recived control faild.");
                }
                //upload files
                UploadFiles(cid);

                BindData(id);
                //Bind History
                iframe_DisplayHistory(id);

                //Send mail to requester when the Status is changed
                //if (isfirstRevicedEnity)
                //{
                //    sendmail(false, cd, ddlStatus.SelectedItem.Text);
                //}
                if (old_status != cd.StatusID)
                {
                    //Skip mail sending while the status is in Collected & Collected (Part)
                   // if (cd.StatusID != 5 && cd.StatusID != 6)
                  //  {
                    sendmail(false, cd, ddlStatus.SelectedItem.Text);
                 //   }
                }
                if (Is1Pallet == true && Convert.ToInt32(ddlItemWeight.SelectedValue) > 1)
                {
                    //first time
                    if (isfirstRevicedEnity)
                    {
                        sendDistributionListmail(cd, false, "Overweight delivery");
                    }
                    else if (ddlStatus.SelectedItem.Text.ToLower() != "received (part)")
                    {
                        sendDistributionListmail(cd, false, "Overweight delivery");
                    }
                }
                //history1.DisplayHistory(id);
                //HistoryBind(id);
                if (ddlStatus.SelectedItem.Text == "Closed")
                {
                    //reload the same page
                    Response.Redirect(Request.Url.AbsoluteUri, false);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void Over_validation()
    {
        rfv_overpallet.Visible = false;
        rfv_overpallet.ErrorMessage = string.Empty;
        rfv_weight.Visible = false;
        rfv_weight.ErrorMessage = string.Empty;
        //rfv_overweight.Visible = false;
        //rfv_overweight.ErrorMessage = string.Empty;
    }
    private void BindData(int cid)
    {
        try
        {
            CallDetail cd = CallDetailsBAL.SelectbyId(cid);
            DeliveryInformation di = DeliveryInformationBAL.SelectbyCallId(cid);
            RecievedInformation ri = ReceivedInformationBAL.SelectbyCallId(cid);

            if (cd != null && di != null)
            {
                hstatus.Value = cd.StatusID.Value.ToString();
                h_cid.Value = cid.ToString();
                ccdCompany.SelectedValue = Convert.ToString(cd.CompanyID);
                ccdSite.SelectedValue = Convert.ToString(cd.SiteID);
                ccdName.SelectedValue = Convert.ToString(cd.RequesterID);
                ccdType.SelectedValue = Convert.ToString(cd.RequestTypeID);
                ccdStatus.SelectedValue = Convert.ToString(cd.StatusID);
                txtDateofArrival.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(di.ArrivalDate).Replace("00:00:00", " "));
                //txtWeight.Text = di.Weight;
                txtNumberofBoxes.Text = Convert.ToString(di.NumofBoxes);
                ccdDeliveryType.SelectedValue = Convert.ToString(di.DeliveryTypeID);
                txtLoggedDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), cd.LoggedDate.ToString().Remove(10));
                txtLoggedTime.Text = string.Format("{0:HH:mm}", cd.LoggedDate);
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
                txtCourierCompany.Text = di.CourierCompany;
                txtNotes.Text = di.Notes;
                ccditemweight.SelectedValue = di.ItemWeight.HasValue ? di.ItemWeight.Value.ToString() : "0";
                
                if (ri != null)
                {
                    ccdCondition.SelectedValue = Convert.ToString(ri.ConditionID);
                    txtNumberofBoxesRec.Text = Convert.ToString(ri.NumofBoxesRec);
                    txtCollectedBoxes.Text = Convert.ToString(ri.NumofBoxesCollected);
                    ccdLocation.SelectedValue = Convert.ToString(ri.StorageLocationID);
                    
                    txtDateReceived.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ri.DateRecieved).Replace("00:00:00", " "));
                    txtDaysinStorage.Text = Convert.ToString(ri.DaysInStore);
                    txtChargeableDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ri.ChargeableDate).Replace("00:00:00", " "));
                    txtTotalCost.Text = Convert.ToString(ri.TotalCost);
                    txtFrom.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ri.StoragePeriodFrom).Replace("00:00:00", " "));
                    txtTo.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ri.StoragePeriodTo).Replace("00:00:00", " "));
                    txtPeriodCost.Text = Convert.ToString(ri.PeriodCost);
                    ccdOurSite.SelectedValue = Convert.ToString(ri.OurSiteID);
                }
                //Disable columns

                ddlOver1pallet.Enabled = false;
                //imgToDate.Visible = false;
                //ddlOverWeight.Enabled = false;
                //ddlItemWeight.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void sendmail(bool check_insert, CallDetail cd,string StatusName)
    {
        try
        {
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();
            // List<string> site = new List<string>();
            //  site = ws.GetSiteDetailsbyId(int.Parse(ddlSite.SelectedValue));
            EmailFooter ef = new EmailFooter();
            ef = FooterEmail.EmailFooter_selectByID(int.Parse(ddlTypeofRequest.SelectedValue));
            PortfolioContact pc = ws.GetContactDetails(int.Parse(ddlRequestersName.SelectedValue));
            AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select();
            Emailer em = new Emailer();
            string subject = string.Empty;
            string body = em.ReadFile("~/WF/DC/EmailTemplates/DeliveryMail.htm");
            string status = DC.BLL.StatusBAL.SelectbyId(string.IsNullOrEmpty(ccdStatus.SelectedValue) ? 0 : Convert.ToInt32(ccdStatus.SelectedValue)).Name;
           
            if (check_insert)
            {
                subject = "Ticket Reference: " + cd.ID.ToString();
                body = body.Replace("[mail_head]", "Delivery Request");
               //First time should display from Delivery information
                //body = body.Replace("[items]", txtNumberofBoxes.Text);
                //body = body.Replace("[Date]", txtDateofArrival.Text);
            }
            else
            {
                subject = "Ticket Reference: " + cd.ID.ToString();
                if (status.ToLower() == "closed")
                    body = body.Replace("[mail_head]", "Delivery Request Closed");
                else
                body = body.Replace("[mail_head]", "Delivery Request Update");

                //after that it should display from Recived information
                //body = body.Replace("[items]", txtNumberofBoxesRec.Text);
                //body = body.Replace("[Date]", txtDateReceived.Text);
                //body = body.Replace("Number of Boxes", "Number of Items Received");
                //body = body.Replace("Anticipated Date of Delivery", "Date Received");
            }

            //Thank you for raising a Delivery request, details of your request can be found below:
            if (StatusName == "Awaiting Approval" && check_insert)
            { }
            else if (StatusName == "Awaiting Delivery" && check_insert)
            { }
            else if (StatusName == "Closed")
            {
                body = body.Replace("Thank you for raising a Delivery Request, details of your request can be found below:", "The following Delivery Request has now been closed.");
            }
            else
            {
                body = body.Replace("Thank you for raising a Delivery Request, details of your request can be found below:", "Please see below an update on your Delivery Request:");
            }
            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            body = body.Replace("[user]", ddlRequestersName.SelectedItem.Text);
            body = body.Replace("[Status]", status);
            body = body.Replace("[adminemail]", ae.EmailAddress);
            body = body.Replace("[footer]", ef == null ? string.Empty : HttpUtility.HtmlDecode(ef.EmailFooter1));
            body = body.Replace("[mailcontent]", RequestMailFormat.DeliveryHtmlMailDetails(cd.ID));
            //body = body.Replace("[sitename]", ddlSite.SelectedItem.Text);
            
            //body = body.Replace("[Status]", status);
            //body = body.Replace("[reference]", cd.ID.ToString());
            
            //body = body.Replace("[cnumber]", txtTrackingNumber.Text);
            //body = body.Replace("[ccompany]", txtCourierCompany.Text);
            //body = body.Replace("[contact]", pc.Email);
            //body = body.Replace("[mobile]", pc.Telephone);
            //body = body.Replace("[notes]", txtNotes.Text);
            //
            //body = body.Replace("[adminemail]", ae.EmailAddress);
            //body = body.Replace("[Company]", ddlRequestersCompany.SelectedItem.Text);
            ////body = body.Replace("[Weight]", txtWeight.Text);
            //body = body.Replace("[Weight]", ddlItemWeight.SelectedItem.Text);
            //body = body.Replace("[DType]", ddlDeliveryType.SelectedItem.Text);
            //body = body.Replace("[pallet]", ddlOver1pallet.SelectedItem.Text);
            ////body = body.Replace("[OWeight]", ddlOverWeight.SelectedItem.Text);
            ////body = body.Replace("[kgs]", lblweight.Text);
            //body = body.Replace("[Description]", txtDescription.Text);

            em.SendingMail(fromemailid, subject, body, pc.Email);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void sendDistributionListmail(CallDetail cd,bool isInserted,string MailHeader)
    {
        try
        {
            List<int> idlist = SecurityAccessMail.GetIds(1); // 1=delivery
            if (idlist.Count > 0)
            {
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                DC.SRV.WebService ws = new DC.SRV.WebService();
                string status = DC.BLL.StatusBAL.SelectbyId(string.IsNullOrEmpty(ccdStatus.SelectedValue) ? 0 : Convert.ToInt32(ccdStatus.SelectedValue)).Name;

                string subject = string.Format("Ticket Reference: " + cd.ID.ToString());
                Emailer em = new Emailer();
                string body = em.ReadFile("~/WF/DC/EmailTemplates/DeliveryDistributionListMail.htm");
                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                if (isInserted)
                {
                    body = body.Replace("[mail_head]", "Delivery Request");
                    body = body.Replace("[content_header]", "The following customer would like to schedule a delivery");
                }
                else
                {
                    body = body.Replace("[mail_head]", "Delivery Request Update");
                    body = body.Replace("[content_header]", "Updated Delivery Request Details ");
                }
                body = body.Replace("[Status]", status);
                body = body.Replace("[mailcontent]", RequestMailFormat.DeliveryHtmlMailDetails(cd.ID));
                //body = body.Replace("[Customer]", ddlRequestersCompany.SelectedItem.Text);
                //body = body.Replace("[reference]", cd.ID.ToString());
                //body = body.Replace("[Status]", status);
                //body = body.Replace("[Name]", ddlRequestersName.SelectedItem.Text);
                //body = body.Replace("[DOD]", txtDateofArrival.Text);
                //body = body.Replace("[Time]", string.Empty);
                //body = body.Replace("Time:", "");
                //body = body.Replace("[Notes]", txtNotes.Text);
                //body = body.Replace("[grossweight]", ddlItemWeight.SelectedItem.Text);
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
                GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
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
                //HistoryBind(int.Parse(h_cid.Value));
                iframe_DisplayHistory(int.Parse(h_cid.Value));
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
    private void Clear()
    {
        ccdSite.SelectedValue = "0";
        ccdStatus.SelectedValue = "0";
        ccdCompany.SelectedValue = "0";
        ccdName.SelectedValue = "0";
        txtRequestersEmailAddress.Text = string.Empty;
        txtRequestersTelephoneNo.Text = string.Empty;
        txtDateofArrival.Text = string.Empty;
        //txtWeight.Text = string.Empty;
        txtNumberofBoxes.Text = string.Empty;
        ccdDeliveryType.SelectedValue = "0";
        txtDescription.Text = string.Empty;
        ddlOver1pallet.SelectedValue = "0";
        //ddlOverWeight.SelectedValue = "0";
        ddlItemWeight.SelectedValue = "0";
        txtCourierCompany.Text = string.Empty;
        txtTrackingNumber.Text = string.Empty;
        ccdCondition.SelectedValue = "0";
        txtNumberofBoxesRec.Text = string.Empty;
        ccdLocation.SelectedValue = "0";
        txtNotes.Text = string.Empty;
        txtDateReceived.Text = string.Empty;
        txtDaysinStorage.Text = string.Empty;
        txtChargeableDate.Text = string.Empty;
        txtTotalCost.Text = string.Empty;
        txtFrom.Text = string.Empty;
        txtTo.Text = string.Empty;
        txtPeriodCost.Text = string.Empty;
        ccdOurSite.SelectedValue = "0";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        //history1.DisplayHistory(int.Parse(h_cid.Value));
        //HistoryBind(int.Parse(h_cid.Value));
        iframe_DisplayHistory(int.Parse(h_cid.Value));
    }
    #region Journal
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
    private void AddCallDetailsJournal(int cid,CallDetail cd,DateTime ModifiedDate)
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
            cdj.CallID = cid;
            cdj.SiteID = int.Parse(ddlSite.SelectedValue);
            cdj.RequestTypeID = int.Parse(ddlTypeofRequest.SelectedValue);
            if (Is1Pallet == true && IsOverweight == true)
                cdj.StatusID = 1; // 1=Awaiting Approval
            else
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
    private bool CompareDeliveryInformation(DeliveryInformation di, int cid, DateTime ad, string wt, int nob, int dtid, string desc, bool pallet, bool ovw,string cn,string cc,string notes,int ItemWeight)
    {
        bool IsChanged = false;
        try
        {
            DeliveryInformation pastInformation = di;
            if (pastInformation.CallID != cid)
                IsChanged = true;
            else if (pastInformation.ArrivalDate != ad)
                IsChanged = true;
            //else if (pastInformation.Weight != wt)
            //    IsChanged = true;
            else if (pastInformation.NumofBoxes != nob)
                IsChanged = true;
            else if (pastInformation.DeliveryTypeID != dtid)
                IsChanged = true;
            else if (pastInformation.Description != desc)
                IsChanged = true;
            else if (pastInformation.Pallet != pallet)
                IsChanged = true;
            //else if (pastInformation.OverWeight != ovw)
            //    IsChanged = true;
            else if (pastInformation.CourierNumber != cn)
                IsChanged = true;
            else if (pastInformation.CourierCompany != cc)
                IsChanged = true;
            else if (pastInformation.Notes != notes)
                IsChanged = true;
            else if (pastInformation.ItemWeight != ItemWeight)
                IsChanged = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return IsChanged;
    }
    private void AddDeliveryInformationJournal(int cid, DateTime ModifiedDate)
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
            dij.Notes = txtNotes.Text;
            dij.ModifiedBy = sessionKeys.UID;
            dij.ModifiedDate = ModifiedDate;
            dij.VisibleToCustomer = false;
            dij.ItemWeight = Convert.ToInt32(string.IsNullOrEmpty(ddlItemWeight.SelectedValue) ? "0" : ddlItemWeight.SelectedValue);
            ws.AddDeliveryInformationJournal(dij);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private bool CompareReceivedInformation(RecievedInformation ri, int cid, int cnid, int nbrec, int nbcol, int slid, int osid, DateTime rd, int ds, DateTime cd, decimal tc, DateTime? from, DateTime? to, decimal? pc)
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
            else if (pastInformation.NumofBoxesCollected != nbcol)
                IsChanged = true;
            else if (pastInformation.StorageLocationID != slid)
                IsChanged = true;
            else if (pastInformation.OurSiteID != osid)
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
    private void AddReceivedInformationJournal(int cid, DateTime ModifiedDate)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            RecievedInformationJournal rij = new RecievedInformationJournal();
            rij.CallID = cid;
            rij.ConditionID = int.Parse(ddlCondition.SelectedValue);
            rij.NumofBoxesRec = Convert.ToInt32(txtNumberofBoxesRec.Text);
            rij.NumofBoxesCollected = Convert.ToInt32(txtCollectedBoxes.Text);
            rij.StorageLocationID = int.Parse(ddlStorageLocation.SelectedValue);
            rij.OurSiteID = int.Parse(ddlOurSite.SelectedValue);
            rij.DateRecieved = Convert.ToDateTime(txtDateReceived.Text);
            rij.DaysInStore = Convert.ToInt32(txtDaysinStorage.Text);
            rij.ChargeableDate = Convert.ToDateTime(txtChargeableDate.Text);
            rij.TotalCost = Convert.ToDecimal(txtTotalCost.Text);
            rij.StoragePeriodFrom = string.IsNullOrEmpty(txtFrom.Text) ? (DateTime?)null : Convert.ToDateTime(txtFrom.Text);
            rij.StoragePeriodTo = string.IsNullOrEmpty(txtTo.Text) ? (DateTime?)null : Convert.ToDateTime(txtTo.Text);
            rij.PeriodCost = string.IsNullOrEmpty(txtPeriodCost.Text) ? (Decimal?)null : Convert.ToDecimal(txtPeriodCost.Text);
            rij.ModifiedBy = sessionKeys.UID;
            rij.ModifiedDate = ModifiedDate;
            rij.VisibleToCustomer = false;
            ws.AddReceivedInformationJournal(rij);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #endregion


    private bool Check_Status_ByItems()
    {
        bool retval = false;
        var sArray = new string[] { "received (part)", "collected (part)", "collected", "closed", "rejected", "awaiting approval", "awaiting delivery", "approved" };
        if (! sArray.Contains(ddlStatus.SelectedItem.Text.ToLower()))
        {
            int n_boxes = Convert.ToInt32(string.IsNullOrEmpty(txtNumberofBoxes.Text.Trim()) ? "0" : txtNumberofBoxes.Text.Trim());
            int n_reviced_boxes = Convert.ToInt32(string.IsNullOrEmpty(txtNumberofBoxesRec.Text.Trim()) ? "0" : txtNumberofBoxesRec.Text.Trim());

            if ((n_boxes > 0 && n_reviced_boxes >= 0 && n_boxes != n_reviced_boxes) && int.Parse(string.IsNullOrEmpty(ddlCondition.SelectedValue) ? "0" : ddlCondition.SelectedValue) > 0)
            {
                retval = true;
            }
        }

        return retval;
    }

    private void RecivedItems_display(string StatusID)
    {
        //Approved and Awaiting Delivery
        //if ((StatusID == "1") || (StatusID == "36"))
        //    tablereceived.Visible = false;
        //else
        //    tablereceived.Visible = true;
    }
    protected void btnUpdateCourier_Click(object sender, EventArgs e)
    {
        Update_CourierTrackNumber();
    }

    private void Update_CourierTrackNumber()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtTrackingNumber.Text.Trim()))
            {
                int cid = QueryStringValues.CallID;
                CallDetail cd = CallDetailsBAL.SelectbyId(cid);
                DeliveryInformation di = DeliveryInformationBAL.SelectbyCallId(cid);
                bool IsDeliveryInformationChanged = false;
                //check the Courier number is modified
                if (!string.Equals(di.CourierNumber.ToLower(), txtCourierCompany.Text.ToLower().Trim()))
                {
                    IsDeliveryInformationChanged = true;
                }
                di.CallID = cid;
                di.CourierNumber = txtTrackingNumber.Text;

                DeliveryInformationBAL.DeliveryInformationUpdate(di);
                if (IsDeliveryInformationChanged)
                    AddDeliveryInformationJournal(cid, DateTime.Now);
                //bind history
                iframe_DisplayHistory(cid);
            }
            else
            {
                lblerr.Text = "Please enter courier tracking number";
                lblerr.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}