using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using DC.BLL;
using DC.Entity;
using PortfolioMgt.BAL;
using PortfolioMgt.Entity;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class AddFaithGiving : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    hmoney.Value = "10.00,20.00,50.00,100.00,1000.00";

                    if (QueryStringValues.Type.Length > 0)
                        pnlDates.Visible = true;
                    else
                        pnlDates.Visible = false;

                    if (sessionKeys.Message.Length > 0)
                    {
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, sessionKeys.Message, "");
                        sessionKeys.Message = string.Empty;
                    }
                    Bind_Campaigning_Owner();

                    //  BindReligion();
                    if (QueryStringValues.EID > 0)
                    {
                        var uid = QueryStringValues.EID;

                        IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                        var tithingDetail = pRep.GetAll().Where(o => o.ID == uid).FirstOrDefault();
                        if (tithingDetail != null)
                        {
                            //int id = sessionKeys.UID;

                            //txtcurrenyValue.ReadOnly = false;

                            txtTitle.Text = tithingDetail.Title;
                            txtDescriptionArea.Text = tithingDetail.Description;
                           // ddlCurrency.Text = tithingDetail.Currency;

                            txtcurrenyValue.Text = string.Format("{0:F2}", tithingDetail.DefaultTarget);
                           // txtStartDate.Text = tithingDetail.StartDate.HasValue ? tithingDetail.StartDate.Value.ToShortDateString() : DateTime.Now.ToShortDateString();
                           // TextEndDate.Text = tithingDetail.EndDate.HasValue ? tithingDetail.EndDate.Value.ToShortDateString() : DateTime.Now.ToShortDateString();
                            img.ImageUrl = GetImageUrl(uid.ToString());
                            // SetAmountValues(tithingDetail.DefaultValues);
                            hmoney.Value = tithingDetail.DefaultValues;
                            chkShowChart.Checked = tithingDetail.ShowChart.HasValue?tithingDetail.ShowChart.Value:false;
                            chkShowQR.Checked = tithingDetail.ShowQRCode.HasValue ? tithingDetail.ShowQRCode.Value : false;

                            txtStartDate.Text = tithingDetail.StartDate.HasValue ? tithingDetail.StartDate.Value.ToShortDateString() : "";
                            txtEndDate.Text = tithingDetail.EndDate.HasValue ? tithingDetail.EndDate.Value.ToShortDateString() : "";


                            

                            // chkSendEmail.Checked = tithingDetail.SendMailAfterDonation.HasValue ? tithingDetail.SendMailAfterDonation.Value : false;
                            // ddlOwner.SelectedValue = tithingDetail.LoggedByID.ToString();
                        }
                    }

                    if (QueryStringValues.UNID.Length > 0)
                    {
                        var uid = QueryStringValues.UNID;

                        IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                        var tithingDetail = pRep.GetAll().Where(o => o.unid == uid).FirstOrDefault();
                        if (tithingDetail != null)
                        {
                            //int id = sessionKeys.UID;

                            //txtcurrenyValue.ReadOnly = false;

                            txtTitle.Text = tithingDetail.Title;
                            txtDescriptionArea.Text = tithingDetail.Description;
                            // ddlCurrency.Text = tithingDetail.Currency;

                            txtcurrenyValue.Text = string.Format("{0:F2}", tithingDetail.DefaultTarget);
                            // txtStartDate.Text = tithingDetail.StartDate.HasValue ? tithingDetail.StartDate.Value.ToShortDateString() : DateTime.Now.ToShortDateString();
                            // TextEndDate.Text = tithingDetail.EndDate.HasValue ? tithingDetail.EndDate.Value.ToShortDateString() : DateTime.Now.ToShortDateString();
                            img.ImageUrl = GetImageUrl(uid.ToString());
                            // SetAmountValues(tithingDetail.DefaultValues);
                            hmoney.Value = tithingDetail.DefaultValues;
                            chkShowChart.Checked = tithingDetail.ShowChart.HasValue ? tithingDetail.ShowChart.Value : false;
                            chkShowQR.Checked = tithingDetail.ShowQRCode.HasValue ? tithingDetail.ShowQRCode.Value : false;
                            txtStartDate.Text = tithingDetail.StartDate.HasValue ? tithingDetail.StartDate.Value.ToShortDateString() : "";
                            txtEndDate.Text = tithingDetail.EndDate.HasValue ? tithingDetail.EndDate.Value.ToShortDateString() : "";

                            // chkSendEmail.Checked = tithingDetail.SendMailAfterDonation.HasValue ? tithingDetail.SendMailAfterDonation.Value : false;
                            // ddlOwner.SelectedValue = tithingDetail.LoggedByID.ToString();
                        }
                    }

                    UpdateMoneyGrid();


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        class moneycls
        {
            public double id { set; get; }
            public double value { set; get; }
        }

        private void UpdateMoneyGrid()
        {
            try
            {
                List<moneycls> mcList = new List<moneycls>();
                var mLIst = hmoney.Value;
                int index = 1;
                foreach(var m in mLIst.Split(','))
                {
                    if (m.Trim().Length > 0)
                        mcList.Add(new moneycls() { id = index, value =Convert.ToDouble( m)});

                            index++;
                }

                GridMoney.DataSource = mcList;
                GridMoney.DataBind();

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void SetAmountValues(string setvalues)
        {
            //if (setvalues.Length > 1)
            //{
            //    var sList = setvalues.Split(',');
            //    foreach (var s in sList)
            //    {
            //        for (int i = 0; i < ckbCurrencyList.Items.Count; i++)
            //        {
            //            if (s.Length > 0)
            //            {
            //                if (Convert.ToDouble(ckbCurrencyList.Items[i].Value.Trim()) == Convert.ToDouble(s.Trim()))
            //                {
            //                    ckbCurrencyList.Items[i].Selected = true;
            //                }
            //            }
            //        }
            //    }
            //}
        }


        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing/") + "Tithing_org_" + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("~/WF/UploadData/Tithing/Tithing_org_{0}.png", contactsId.ToString());
                else
                    img = string.Format("~/WF/UploadData/Tithing/Tithing_org_{0}.png", contactsId.ToString());
            }
            else
            {
                img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();

        }


        private string getMoney()
        {
            var retval = "";
            foreach (GridViewRow gvrow in GridMoney.Rows)
            {
                TextBox lblValue = (TextBox)gvrow.FindControl("lblValue");
if (lblValue.Text.Trim().Length >0)
{
                    retval = retval + lblValue.Text.Trim()+",";
                }
            }
            return retval;
        }
        protected void btnSaveAndEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //var cRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                TithingDefaultDetail value= null;

                if(QueryStringValues.EID >0)
                 value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();
                 
                
                if (QueryStringValues.UNID.Length>0)
                    value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                // elese 

                if (value == null)
                {
                    value = new PortfolioMgt.Entity.TithingDefaultDetail();

                    value.Title = txtTitle.Text;
                    value.Description = txtDescriptionArea.Text;
                    value.DefaultTarget = Convert.ToDouble(txtcurrenyValue.Text);
                   // value.Currency = ddlCurrency.SelectedValue;
                    value.ModifiedDateTime = DateTime.Now;
                    value.CreatedDateTime = DateTime.Now;
                    value.LoggedByID = sessionKeys.UID;// Convert.ToInt32(ddlOwner.SelectedValue);
                    value.OrganizationID = sessionKeys.PortfolioID;
                    value.SendMailAfterDonation = true;// chkSendEmail.Checked;
                                                       // value.DefaultValues = GetChecklist();

                    if (QueryStringValues.Type.Length > 0)
                    {
                        if(txtStartDate.Text.Length >0)
                        value.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                        if(txtEndDate.Text.Length >0)
                        value.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());
                    }
                    else
                    {
                        value.StartDate = DateTime.Now;// Convert.ToDateTime(txtStartDate.Text.Trim());
                        value.EndDate = DateTime.Now.AddDays(30);// Convert.ToDateTime(TextEndDate.Text.Trim());
                    }
                    value.DefaultBanner = "";
                    value.ShowQRCode = chkShowQR.Checked;
                    value.ShowChart = chkShowChart.Checked;
                    value.DefaultValues =getMoney();
                    value.unid = Guid.NewGuid().ToString();
                    value.Event_unid = QueryStringValues.EVENTUNID;
                    TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Add(value);

                    if(value.ID >0)
                    {
                        //Create a project / Request
                        CreateAProject(value.unid, Section_Fundriser, value.Title,value.StartDate.Value,value.EndDate.Value);
                    }


                    UpdateQRCodeImage(value.unid);

                    sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;
                    StartUpLoad(value.ID);
                    //Response.Redirect(Request.RawUrl+"?eid="+value.ID.ToString(), false);
                    if(QueryStringValues.Type.Length >0)
                    {
                        Response.Redirect("~/App/FundraiserListView.aspx", false);
                    }
                    else
                    Response.Redirect("~/App/FundraiserList.aspx?eventunid=" + QueryStringValues.EVENTUNID+"&unid="+value.unid, false);
                }
                else
                {
                    value.Title = txtTitle.Text;
                    value.Description = txtDescriptionArea.Text;
                    value.DefaultTarget = float.Parse(txtcurrenyValue.Text);
                   // value.Currency = // ddlCurrency.SelectedValue;
                    value.ModifiedDateTime = DateTime.Now;
                    value.CreatedDateTime = DateTime.Now;
                    value.LoggedByID = sessionKeys.UID;// Convert.ToInt32(ddlOwner.SelectedValue);
                                                       // value.OrganizationID = 1;
                    value.SendMailAfterDonation = true;// chkSendEmail.Checked;

                    if (QueryStringValues.Type.Length > 0)
                    {
                        if (txtStartDate.Text.Length > 0)
                            value.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                        if (txtEndDate.Text.Length > 0)
                            value.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());
                    }
                  
                    // value.DefaultValues = GetChecklist();
                    // value.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                    //value.EndDate = Convert.ToDateTime(TextEndDate.Text.Trim());
                    value.DefaultBanner = "";
                    value.ShowQRCode = chkShowQR.Checked;
                    value.ShowChart = chkShowChart.Checked;
                    value.DefaultValues = hmoney.Value;
                    
                    if(value.unid == null)
                        value.unid = Guid.NewGuid().ToString();

                    TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Update(value);

                    if (value.ID > 0)
                    {
                        //Create a project / Request
                        CreateAProject(value.unid, Section_Fundriser, value.Title, value.StartDate.Value, value.EndDate.Value);
                    }

                    UpdateQRCodeImage(value.unid);

                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                    StartUpLoad(value.ID);
                    if (QueryStringValues.Type.Length > 0)
                    {
                        Response.Redirect("~/App/FundraiserListView.aspx", false);
                    }
                    else
                        Response.Redirect("~/App/FundraiserList.aspx?eventunid=" + value.Event_unid + "&unid=" + value.unid, false);
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        public const string  Section_Fundriser = "Fundriser";
        public const string Section_Event = "Event";

        private void CreateAProject(string unid, string section, string title, DateTime startdate,DateTime enddate)
        {
            try
            {


                var fE = FLSDetailsBAL.FLSDetailsBAL_SelectAll().Where(o => o.UNID == unid).FirstOrDefault();

                if (fE == null)
                {

                    var cudate = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddHours(9);

                    int incBy = 0;

                    var c = new CallDetail();
                    c.CompanyID = sessionKeys.PortfolioID;
                    c.LoggedBy = sessionKeys.UID;
                    c.LoggedDate = cudate;
                    c.RequesterID = sessionKeys.UID;
                    //6 default
                    c.RequestTypeID = 6;
                    c.SiteID = 0;
                    c.StatusID = JobStatus.Active;
                    CallDetailsBAL.AddCallDetails(c);
                    var CallID = c.ID;
                    //Journal entiry
                    CallDetailsJournalBAL.AddCallDetailsJournal(c);


                    var f = new FLSDetail();
                    f.CallID = c.ID;
                    f.CategoryID = 0;
                    f.ContactAddressID = sessionKeys.UID;
                    f.DateTimeStarted = cudate.AddHours(incBy);
                    f.DepartmentID = 0;
                    f.Details = title;
                    f.PriorityId = 0;
                    f.ScheduledDate = startdate;
                    //increment by hours
                    incBy = incBy + 2;
                    f.ScheduledEndDateTime = enddate;
                    f.DateTimeClosed = enddate;

                    f.SourceOfRequestID = 0;
                    f.SubCategoryID = 0;
                    f.SubjectID = 0;
                    f.UNID = unid;
                    f.Section = section;
                    var tReq = TypeOfRequestBAL.GetTypeOfRequestList().Where(o => o.Name == "Charity").FirstOrDefault();
                    if (tReq != null)
                    {

                        f.RequestType = tReq.ID;
                    }


                    //f.UserID = value.ID;
                    FLSDetailsBAL.AddFLSDetails(f);


                    //add to journal
                    FLSDetailsJournalBAL.AddFLSDetailsJournal(f);

                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void UpdateQRCodeImage(string unid)
        {
            try
            {
                string code = Deffinity.systemdefaults.GetWebUrl() + "/fundraiserdetails.aspx?unid=" + unid; //ab.MoreDetails;

                var filepath = "~/WF/UploadData/Events/" + unid + ".png";
               // var filepathpdf = "~/WF/UploadData/Events/" + unid + ".pdf";

                // var filepath = Server.MapPath("~/WF/UploadData/Events/") + id + ".png";
                //if (File.Exists(Server.MapPath(filepath)))
                //{
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                imgBarCode.Height = 150;
                imgBarCode.Width = 150;
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        System.Drawing.Image img1 = System.Drawing.Image.FromStream(ms);
                        img1.Save(Server.MapPath("~/WF/UploadData/Events/") + unid + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        //  imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    // plBarCode.Controls.Add(imgBarCode);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private string GetChecklist()
        {
            string retval = "";
            //for (int i = 0; i < ckbCurrencyList.Items.Count; i++)
            //{
            //    if (ckbCurrencyList.Items[i].Selected == true)// getting selected value from CheckBox List  
            //    {
            //        retval += ckbCurrencyList.Items[i].Text + " ,"; // add selected Item text to the String .  
            //    }
            //}
            return retval;
        }
        protected void btnPublish_Click(object sender, EventArgs e)
        {

        }


        private void Bind_Campaigning_Owner()
        {
            //var ownerlist = (from c in UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany()
            //                 where c.SID == UserMgt.BAL.UserType.SmartPros
            //                 && c.CompanyID == sessionKeys.PortfolioID
            //                 orderby c.ContractorName
            //                 select new
            //                 {
            //                     ID = c.ID,
            //                     Name = c.ContractorName
            //                 }).ToList();
            //ddlOwner.DataSource = ownerlist;
            //ddlOwner.DataTextField = "Name";
            //ddlOwner.DataValueField = "ID";
            //ddlOwner.DataBind();
            //ddlOwner.Items.Insert(0, new ListItem("Please select...", "0"));
        }






        protected void Check_Clicked(object sender, EventArgs e)
        {

            int id = sessionKeys.UID;

            float num = 0;

            //foreach (ListItem item in ckbCurrencyList.Items)
            //{
            //    if (item.Selected)
            //    {

            //        if (item.Value == "Other Amount")
            //        {
            //            txtcurrenyValue.ReadOnly = false;
            //            txtcurrenyValue.Text = "";
            //            // ddlCurrency.SelectedValue = "US Doller";

            //            foreach (ListItem li in ckbCurrencyList.Items)
            //            {
            //                li.Selected = false;
            //            }
            //            item.Selected = true;

            //        }
            //        else
            //        {
            //            float num1 = float.Parse(item.Value);
            //            num = num + num1;
            //            txtcurrenyValue.Text = num.ToString();
            //        }


            //    }
            //}



        }








        private void StartUpLoad(int tithingid)
        {


            if (imgLogo.PostedFile.FileName.Length > 0)
            {
                Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgLogo.PostedFile.InputStream);
                ImageManager.SaveTithingImage_setpath(imgLogo.FileBytes, tithingid.ToString(), Deffinity.systemdefaults.GetLogoFolderPath());
                // DisplayLogo();
            }
            //int id = tithingid;
            ////get the file name of the posted image  
            //string imgName = imgLogo.FileName;
            ////sets the image path  
            //string imgPath = "~/WF/UploadData/Tithing/" + "Tithing_" + id + ".png";
            ////get the size in bytes that  

            //int imgSize = imgLogo.PostedFile.ContentLength;




            ////validates the posted file before saving  
            //if (imgLogo.PostedFile != null && imgLogo.PostedFile.FileName != "")
            //{
            //    //then save it to the Folder  
            //    imgLogo.SaveAs(Server.MapPath(imgPath));
            //    img.ImageUrl = "~/" + imgPath;


            //    //// 10240 KB means 10MB, You can change the value based on your requirement  
            //    //if (imgLogo.PostedFile.ContentLength > 10240)
            //    //{
            //    //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('File is too big.')", true);
            //    //}
            //    //else
            //    //{
            //    //    //then save it to the Folder  
            //    //    imgLogo.SaveAs(Server.MapPath(imgPath));
            //    //    img.ImageUrl = "~/" + imgPath;

            //    //}

            //}
        }

        protected void GridMoney_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "edit1")
            {

            }
            else if(e.CommandName == "del")
            {
                try
                {
                    var d = e.CommandArgument.ToString();


                    hmoney.Value = hmoney.Value.Replace(d.ToString() + ",", "");

                    hmoney.Value = hmoney.Value.Replace(d.ToString(), "");

                    UpdateMoneyGrid();
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
            
        }

        protected void btnAddMoney_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMoney.Text.Trim().Length > 0)
                {
                    var m = hmoney.Value;
                    hmoney.Value = m + "," + txtMoney.Text.Trim();
                    txtMoney.Text = string.Empty;
                    UpdateMoneyGrid();
                }
                else
                {
                    DeffinityManager.ShowMessages.ShowSuccessError(this.Page, "Please enter valid amount", "");
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }


}