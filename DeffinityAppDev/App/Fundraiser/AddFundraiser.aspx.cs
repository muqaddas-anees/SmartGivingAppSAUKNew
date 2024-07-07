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

namespace DeffinityAppDev.App.Fundraiser
{
    public partial class AddFunraiser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {
                    // chkActive.Checked = true;

                    if (sessionKeys.Message.Length > 0)
                    {
                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                        sessionKeys.Message = string.Empty;
                    }


                    PortfolioMgt.Entity.TithingDefaultDetail value = new TithingDefaultDetail(); ;
                    if (QueryStringValues.EID > 0)
                        value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();


                    if (QueryStringValues.UNID.Length > 0)
                        value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();

                    if (value != null)
                    {
                        hid.Value = QueryStringValues.EID.ToString();

                        IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                        var tithingDetail = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                        if (tithingDetail != null)
                        {
                            huid.Value = tithingDetail.unid;
                            //int id = sessionKeys.UID;

                            //txtcurrenyValue.ReadOnly = false;
                            if (tithingDetail.MasterUNID == null)
                            {
                                txtTitle.Text = tithingDetail.Title;
                            }
                            else
                            {
                                txtTitle.Text = tithingDetail.Title;
                                txtTitle.ReadOnly = true;
                            }
                            txtDescriptionArea.Text = tithingDetail.Description;
                            txtcurrenyValue.Text = string.Format("{0:F2}", tithingDetail.DefaultTarget);
                            img.ImageUrl = "~/ImageHandler.ashx?id=" + tithingDetail.ID + "&s=" + ImageManager.file_section_fundriser;// GetImageUrl(tithingDetail.ID.ToString());
                            imglogoShow.ImageUrl= "~/ImageHandler.ashx?id=" + tithingDetail.ID + "&s=" + ImageManager.file_section_fundriser_logo;// GetLogoImageUrl(tithingDetail.ID.ToString());
                            chkActive.Checked = tithingDetail.EnableP2P??false;
                            chkWall.Checked = tithingDetail.ShowProgress??false;
                            chkQRCode.Checked = (tithingDetail.ShowQRCode ?? false);
                            UpdateDefaultMoneyGrid(tithingDetail.DefaultValues);
                            //shoe amount
                            UpdateMoneyGrid();


                        }
                    }
                    else
                    {
                        huid.Value = Guid.NewGuid().ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private int SaveData(out string retsult)
        {
            retsult = "";
            int retval = 0;
            try
            {

              
                TithingDefaultDetail value = null;

                if (QueryStringValues.EID > 0)
                {
                    value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();
                }


                if (QueryStringValues.UNID.Length > 0)
                    value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();


                if(hid.Value.Length>0)
                {
                    if(hid.Value != "0")
                    {
                        try
                        {
                            value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                        }
                        catch(Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                    }
                }
                
               // IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                if (hid.Value == "0")
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
                    value.StartDate = DateTime.Now;// Convert.ToDateTime(txtStartDate.Text.Trim());
                    value.EndDate = DateTime.Now.AddDays(30);// Convert.ToDateTime(TextEndDate.Text.Trim());
                    value.DefaultBanner = "";
                    value.ShowQRCode = chkQRCode.Checked;
                    value.ShowProgress = chkWall.Checked;
                    value.EnableP2P = chkActive.Checked;
                    // value.DefaultValues = getMoney();
                    if (huid.Value.Length == 0)
                    {
                        value.unid = Guid.NewGuid().ToString();
                        huid.Value = value.unid;
                    }
                    else
                        value.unid = huid.Value;


                    value.Event_unid = QueryStringValues.EVENTUNID;
                    TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Add(value);
                    retsult = "Inserted";
                    retval = value.ID;

                    if (value.ID > 0)
                    {
                        //Create a project / Request
                        CreateAProject(value.unid, Section_Fundriser, value.Title, value.StartDate.Value, value.EndDate.Value);
                    }


                    UpdateQRCodeImage(value.unid);

                   // sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;
                    BannerUpLoad(value.ID);
                    LogoUpLoad(value.ID);


                    img.ImageUrl = "~/ImageHandler.ashx?id=" + value.ID + "&s=" + ImageManager.file_section_fundriser;// GetImageUrl(value.ID.ToString());
                    imglogoShow.ImageUrl = "~/ImageHandler.ashx?id=" + value.ID + "&s=" + ImageManager.file_section_fundriser_logo;//  GetLogoImageUrl(value.ID.ToString());

                }
                else
                {
                    //update  fundriser
                    value.Title = txtTitle.Text;
                    value.Description = txtDescriptionArea.Text;
                    value.DefaultTarget = float.Parse(txtcurrenyValue.Text);
                    // value.Currency = // ddlCurrency.SelectedValue;
                    value.ModifiedDateTime = DateTime.Now;
                    //value.CreatedDateTime = DateTime.Now;
                    value.LoggedByID = sessionKeys.UID;// Convert.ToInt32(ddlOwner.SelectedValue);
                                                       // value.OrganizationID = 1;
                    value.SendMailAfterDonation = true;// chkSendEmail.Checked;
                    value.ShowProgress = chkWall.Checked;
                    value.EnableP2P = chkActive.Checked;


                    // value.DefaultValues = GetChecklist();
                    // value.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                    //value.EndDate = Convert.ToDateTime(TextEndDate.Text.Trim());
                    //value.DefaultBanner = "";
                    value.ShowQRCode = chkQRCode.Checked;
                    value.ShowChart = true;
                    //value.DefaultValues = "";

                    if (value.unid == null)
                        value.unid = Guid.NewGuid().ToString();

                    if (value.unid.Length == 0)
                        value.unid = Guid.NewGuid().ToString();

                    TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Update(value);
                    retval = value.ID;
                    if (value.ID > 0)
                    {
                        retsult = "Updated";
                        //Create a project / Request
                        CreateAProject(value.unid, Section_Fundriser, value.Title, value.StartDate.Value, value.EndDate.Value);
                    }

                    UpdateQRCodeImage(value.unid);

                  //  sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                   
                    BannerUpLoad(value.ID);
                    LogoUpLoad(value.ID);
                    img.ImageUrl = "~/ImageHandler.ashx?id=" + value.ID + "&s=" + ImageManager.file_section_fundriser;// GetImageUrl(value.ID.ToString());
                    imglogoShow.ImageUrl = "~/ImageHandler.ashx?id=" + value.ID + "&s=" + ImageManager.file_section_fundriser_logo;//  GetLogoImageUrl(value.ID.ToString());


                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }
        private void SaveBanner(int tithingid)
        {


            if (imgLogo.PostedFile.FileName.Length > 0)
            {
                Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgLogo.PostedFile.InputStream);
                ImageManager.SaveTithingImage_setpath(imgLogo.FileBytes, tithingid.ToString(), Deffinity.systemdefaults.GetLogoFolderPath());
                // DisplayLogo();
            }
        }
        public const string Section_Fundriser = "Fundriser";
        public const string Section_Event = "Event";

        private void CreateAProject(string unid, string section, string title, DateTime startdate, DateTime enddate)
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
                // string code = Deffinity.systemdefaults.GetWebUrl() + "/fundraiserdetails.aspx?unid=" + unid; //ab.MoreDetails;
                //FundraiserView
                 string code = Deffinity.systemdefaults.GetWebUrl() + "/FundraiserView.aspx?unid=" + unid; 
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

                        ImageManager.FileDBSave(byteImage, null, unid, ImageManager.file_section_fundriser_qr, ".png", unid);
                        System.Drawing.Image img1 = System.Drawing.Image.FromStream(ms);
                        img1.Save(Server.MapPath("~/WF/UploadData/Events/") + unid + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        //  imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    // plBarCode.Controls.Add(imgBarCode);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
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
        protected static string GetLogoImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing/") + "Tithing_org_" + contactsId.ToString() + "_logo.png";

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("~/WF/UploadData/Tithing/Tithing_org_{0}_logo.png", contactsId.ToString());
                else
                    img = string.Format("~/WF/UploadData/Tithing/Tithing_org_{0}_logo.png", contactsId.ToString());
            }
            else
            {
                img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();

        }
        private void BannerUpLoad(int tithingid)
        {


            if (imgBanner.PostedFile.FileName.Length > 0)
            {
                Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgBanner.PostedFile.InputStream);
                ImageManager.SaveTithingImage_setpath(imgBanner.FileBytes, $"{tithingid}_1", Deffinity.systemdefaults.GetLogoFolderPath());
            }
            if (imgBanner1.PostedFile.FileName.Length > 0)
            {
                Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgBanner1.PostedFile.InputStream);
                ImageManager.SaveTithingImage_setpath(imgBanner1.FileBytes, $"{tithingid}_2", Deffinity.systemdefaults.GetLogoFolderPath());
            }
            if (imgBanner2.PostedFile.FileName.Length > 0)
            {
                Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgBanner2.PostedFile.InputStream);
                ImageManager.SaveTithingImage_setpath(imgBanner2.FileBytes, $"{tithingid}_3", Deffinity.systemdefaults.GetLogoFolderPath());
            }
        }
        private void LogoUpLoad(int tithingid)
        {


            if (imgLogo.PostedFile.FileName.Length > 0)
            {
                Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgLogo.PostedFile.InputStream);
                ImageManager.SaveTithingLogoImage_setpath(imgLogo.FileBytes, tithingid.ToString(), Deffinity.systemdefaults.GetLogoFolderPath());
                // DisplayLogo();
            }

        }
        protected void btnAddAmount_Click(object sender, EventArgs e)
        {

            var id = Convert.ToInt32(hid.Value);
            if (id == 0)
            {
                string outval ="";
                id = SaveData( out outval);
                hid.Value = id.ToString();
            }
            haid.Value = "0";
            txtShortDescription.Text = "";
            txtDescription.Text = "";
            txtAmount.Text = "";

            mdl.Show();
        }
        protected void GridMoney_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit1")
            {
                try
                {
                    var d = PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                    if (d != null)
                    {
                        haid.Value = e.CommandArgument.ToString();
                        txtAmount.Text = (d.Amount ?? 0.00).ToString();
                        txtShortDescription.Text = d.Shortdescription;
                        txtDescription.Text = d.Description;
                        mdl.Show();
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }

            }
            else if (e.CommandName == "del")
            {
                try
                {
                    var d = e.CommandArgument.ToString();

                    PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_delete(Convert.ToInt32(d));



                    UpdateMoneyGrid();
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }

        }

        private void UpdateDefaultMoneyGrid(string defaultmoney)
        {
            try
            {
                if(defaultmoney != null)
                {
                    if (defaultmoney.Length > 0)
                    {
                        var sitems = defaultmoney.Split(',');
                        foreach(var s in sitems )
                        {
                            if(s.Trim().Length >0)
                            {
                                var m = PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_SelectAll().Where(o => o.TithingDefaultUnid == huid.Value).Where(o => o.Amount == Convert.ToDouble(s.Trim())).FirstOrDefault();
                                if(m== null)
                                {
                                    PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_Add(new TithingAmount() { TithingDefaultUnid = huid.Value, Amount = Convert.ToDouble(s.Trim()) });
                                }
                            }
                               
                        }

                        //update detultmoney 
                        var clist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == huid.Value).FirstOrDefault();
                        if(clist != null)
                        {
                            clist.DefaultValues = "";
                            PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Update(clist);

                        }
                    }
                }
               
              //  var mList = PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_SelectAll().Where(o => o.TithingDefaultUnid == huid.Value).ToList();
               

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void UpdateMoneyGrid()
        {
            try
            {
                var mList = PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_SelectAll().Where(o => o.TithingDefaultUnid == huid.Value).ToList();
               // List<moneycls> mcList = new List<moneycls>();
               // var mLIst = hmoney.Value;

                GridMoney.DataSource = mList;
                GridMoney.DataBind();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSaveAmount_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Convert.ToInt32( hid.Value);
                TithingDefaultDetail v;
                if (id > 0)
                {
                     v = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                    if(v != null)
                    huid.Value = v.unid;
                }
                //if(id == 0)
                //{
                //    id = SaveData();
                //}

                var aid = Convert.ToInt32(haid.Value);
                if (haid.Value.Length > 0)
                {
                   var aEntity= PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_SelectAll().Where(o=>o.ID == aid).FirstOrDefault();

                    if(aEntity == null)
                    {
                        aEntity = new PortfolioMgt.Entity.TithingAmount();
                        aEntity.Amount = Convert.ToDouble(txtAmount.Text.Trim());
                        aEntity.Shortdescription = txtShortDescription.Text.Trim();
                        aEntity.Description = txtDescription.Text.Trim();
                        aEntity.TithingDefaultUnid = huid.Value;

                        PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_Add(aEntity);
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Addedsuccessfully, "");
                    }
                    else
                    {
                        aEntity.Amount = Convert.ToDouble(txtAmount.Text.Trim());
                        aEntity.Shortdescription = txtShortDescription.Text.Trim();
                        aEntity.Description = txtDescription.Text.Trim();
                        aEntity.TithingDefaultUnid = huid.Value;
                        PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_Update(aEntity);
                        haid.Value = "0";
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.UpdatedSuccessfully, "");
                    }

                    UpdateMoneyGrid();
                }
                //else
                //{
                //    DeffinityManager.ShowMessages.ShowSuccessError(this.Page, "Please enter valid amount", "");
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            try
            {
                PortfolioMgt.Entity.TithingDefaultDetail value = null;
                if (QueryStringValues.EID > 0)
                    value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();


                if (QueryStringValues.UNID.Length > 0)
                    value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                if (value != null)
                {
                    BannerUpLoad(value.ID);
                    Response.Redirect("~/App/Fundraiser/AddFundraiser.aspx?type=main&eid=" + value.ID, false);
                }
                else
                {
                    string result = "";
                    var id = SaveData(out result);

                    if (id > 0)
                        BannerUpLoad(id);

                    Response.Redirect("~/App/Fundraiser/AddFundraiser.aspx?type=main&eid=" +id, false);

                }
               
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           
        }

        protected void btnSaveLogo_Click(object sender, EventArgs e)
        {
            try
            {
                PortfolioMgt.Entity.TithingDefaultDetail value = null ;
                if (QueryStringValues.EID > 0)
                    value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();


                if (QueryStringValues.UNID.Length > 0)
                    value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                if (value != null)
                {
                    LogoUpLoad(value.ID);
                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                    Response.Redirect("~/App/Fundraiser/AddFundraiser.aspx?type=main&eid=" + value.ID, false);
                }
                else
                {
                    string result = "";
                    var id = SaveData(out result);

                    if (id > 0)
                        LogoUpLoad(id);

                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;


                    Response.Redirect("~/App/Fundraiser/AddFundraiser.aspx?type=main&eid=" + id, false);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnPeertoPeer_Click(object sender, EventArgs e)
        {
            if (hid.Value.Length > 0)
            {
                var value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                if (value != null)
                    Response.Redirect("~/App/Fundraiser/peertopeer/Dashboard.aspx?unid=" + value.unid);
            }

            else if (QueryStringValues.EID > 0)
              
            {
                var value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();
                Response.Redirect("~/App/Fundraiser/peertopeer/Dashboard.aspx?unid=" + value.unid);
            }
            else
            Response.Redirect("~/App/Fundraiser/peertopeer/Dashboard.aspx?unid=" + huid.Value);
        }

        protected void btnSaveFundraiser_Click(object sender, EventArgs e)
        {
            try
            {
                string result = ""; ;
                int id = SaveData(out result);
                if (id > 0)
                {
                    sessionKeys.Message = "Great! Your fundraiser has been saved!";
                    Response.Redirect("~/App/Fundraiser/AddFundraiser.aspx?type=main&eid=" + id, false);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if(hid.Value.Length >0)
                {
                    var value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                    if(value != null)
                    Response.Redirect("~/FundraiserView.aspx?unid=" + value.unid, false);
                }

                else if (QueryStringValues.EID > 0)
                {
                    var value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();
                    Response.Redirect("~/FundraiserView.aspx?unid="+ value.unid, false);
                }
                else
                {
                    Response.Redirect("~/FundraiserView.aspx?unid=" + QueryStringValues.UNID, false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}