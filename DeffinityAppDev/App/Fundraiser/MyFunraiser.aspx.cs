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
using TuesPechkin;

namespace DeffinityAppDev.App.Fundraiser
{
    public partial class MyFunraiser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // chkActive.Checked = true;
                    string muind = "";
                    string mid = "";
                    PortfolioMgt.Entity.TithingDefaultDetail value = new TithingDefaultDetail(); ;
                    if (QueryStringValues.EID > 0)
                        value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();


                    if (QueryStringValues.UNID.Length > 0)
                    {
                        value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                        if (value != null)
                        {
                            hid.Value = value.ID.ToString();
                            mid = value.ID.ToString();
                            muind = value.unid;
                        }
                    }

                    if(QueryStringValues.MUNID.Length >0)
                    {
                        value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.MUNID).FirstOrDefault();
                        if (value != null)
                        {
                            hid.Value = value.ID.ToString();
                            mid = value.ID.ToString();
                            muind = value.unid;
                        }
                    }

                    var f = PortfolioMgt.BAL.FundraisersInfoBAL.FundraisersInfoBAL_SelectAll().Where(o => o.FundUNID == QueryStringValues.UNID && o.Email.ToLower().Trim() == sessionKeys.UEmail.ToLower()).FirstOrDefault();

                    if(f != null)
                    {
                        muind = f.MainFundUNID;
                        var m = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == muind).FirstOrDefault();
                        if(m != null)
                        mid = m.ID.ToString();
                        //huid.Value = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        huid.Value = Guid.NewGuid().ToString();
                    }
                   


                    if (value != null)
                    {
                        if(QueryStringValues.EID >0)
                        hid.Value = QueryStringValues.EID.ToString();
                        
                        IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                        var tithingDetail = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                        if (tithingDetail != null)
                        {
                            if(f == null)
                            huid.Value = tithingDetail.unid;
                            //int id = sessionKeys.UID;

                            //txtcurrenyValue.ReadOnly = false;

                            txtTitle.Text = tithingDetail.Title;
                            txtDescriptionArea.Text = tithingDetail.Description;
                            txtyourstory.Text = tithingDetail.FundraiserDetails;
                            txtcurrenyValue.Text = string.Format("{0:F2}", tithingDetail.DefaultTarget ?? 0.00);

                            //img.ImageUrl = GetImageUrl(mid);
                            //imglogoShow.ImageUrl = GetLogoImageUrl(mid);
                            img.ImageUrl = "~/ImageHandler.ashx?id=" + mid + "&s=" + ImageManager.file_section_fundriser;// GetImageUrl(tithingDetail.ID.ToString());
                            imglogoShow.ImageUrl = "~/ImageHandler.ashx?id=" + mid + "&s=" + ImageManager.file_section_fundriser_logo;// GetLogoImageUrl(tithingDetail.ID.ToString());

                            if (ImageManager.FileIsExists(tithingDetail.ID.ToString(), ImageManager.file_section_fundriser_my_logo))
                            {
                                img_mylogo.Visible = true;
                                img_mylogo.ImageUrl = "~/ImageHandler.ashx?id=" + tithingDetail.ID.ToString() + "&s=" + ImageManager.file_section_fundriser_my_logo;
                            }
                            else
                                img_mylogo.Visible = false;

                            // img_mylogo.ImageUrl = GetMyLogoImageUrl(hid.Value);
                            chkActive.Checked = tithingDetail.EnableP2P ?? false;
                            chkWall.Checked = tithingDetail.ShowProgress ?? false;

                            txtStartDate.Text = tithingDetail.StartDate.Value.ToShortDateString();
                            txtEndDate.Text = tithingDetail.EndDate.Value.ToShortDateString();
                            txtAddress.Text = (tithingDetail.Address??"");
                            txtState.Text = (tithingDetail.State??"");
                            txtCity.Text = (tithingDetail.City??"");
                            txtPostcode.Text = (tithingDetail.Postcode??"");
                            if ((tithingDetail.Country ?? "").Length > 0)
                                ddlcountry.SelectedValue = tithingDetail.Country;

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
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void SaveImageFileData(string section,int fundid,int mfunid )
        {
            try
            {
               var frep = new PortfolioRepository<FileData>();
               var fList = frep.GetAll().Where(o => o.Section == section).Where(o => o.FileID == fundid.ToString()).FirstOrDefault();

                if (fList == null)
                {
                    //copy main image
                    var fEntiry = frep.GetAll().Where(o => o.Section == section).Where(o => o.FileID == mfunid.ToString()).FirstOrDefault();
                    if (fEntiry != null)
                    {
                        var fdata = new FileData();
                        fdata.FileID = fundid.ToString();// mfunid.ToString();
                        fdata.ContentLength = fEntiry.ContentLength;
                        fdata.FileData1 = fEntiry.FileData1;
                        fdata.FileData2 = fEntiry.FileData2;
                        fdata.FileName = fEntiry.FileName;
                        fdata.FileType = fEntiry.FileType;
                        fdata.FolderID = fEntiry.FolderID;
                        fdata.Section = fEntiry.Section;
                        frep.Add(fdata);
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
                string munid = "";
                var f = PortfolioMgt.BAL.FundraisersInfoBAL.FundraisersInfoBAL_SelectAll().Where(o => o.FundUNID == QueryStringValues.UNID && o.Email.ToLower() == sessionKeys.UEmail.ToLower()).FirstOrDefault();

                if (f != null)
                {
                    munid = f.MainFundUNID;
                }
                else
                {
                    huid.Value = Guid.NewGuid().ToString();
                }
                //else
                //{
                //    huid.Value = Guid.NewGuid().ToString();
                //}


                TithingDefaultDetail value = null;

                if (QueryStringValues.EID > 0)
                    value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();


                if (QueryStringValues.UNID.Length > 0)
                    value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();

                if(QueryStringValues.MUNID.Length >0)
                {
                    hid.Value = "0";
                }
                // IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                if (hid.Value == "0")
                {
                    value = new PortfolioMgt.Entity.TithingDefaultDetail();

                    value.Title = txtTitle.Text;
                    value.Description = txtDescriptionArea.Text;
                    value.FundraiserDetails = txtyourstory.Text;
                    value.DefaultTarget = Convert.ToDouble(txtcurrenyValue.Text);
                    // value.Currency = ddlCurrency.SelectedValue;
                    value.ModifiedDateTime = DateTime.Now;
                    value.CreatedDateTime = DateTime.Now;
                    value.LoggedByID = sessionKeys.UID;// Convert.ToInt32(ddlOwner.SelectedValue);
                    value.OrganizationID = sessionKeys.PortfolioID;
                    value.SendMailAfterDonation = true;// chkSendEmail.Checked;
                    if (txtStartDate.Text.Trim().Length > 0)
                        value.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                    else
                        value.StartDate = DateTime.Now;// Convert.ToDateTime(txtStartDate.Text.Trim());
                    if (txtEndDate.Text.Trim().Length > 0)
                        value.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());
                    else
                        value.EndDate = DateTime.Now.AddDays(30);// Convert.ToDateTime(TextEndDate.Text.Trim());
                    value.DefaultBanner = "";
                    value.ShowQRCode = false;
                    value.ShowProgress = chkWall.Checked;
                    value.EnableP2P = chkActive.Checked;
                    value.MasterUNID = QueryStringValues.MUNID;
                    // value.DefaultValues = getMoney();
                    if (f == null)
                        value.unid = Guid.NewGuid().ToString();
                    else
                        value.unid = huid.Value;
                    value.Event_unid = QueryStringValues.EVENTUNID;
                    value.Address = txtAddress.Text.Trim();
                    value.City = txtCity.Text.Trim();
                    value.State = txtState.Text.Trim();
                    value.Postcode = txtPostcode.Text.Trim();
                    value.Country = ddlcountry.SelectedValue;
                    TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Add(value);

                    var mfunid = 0;
                    if (QueryStringValues.MUNID.Length > 0)
                    {
                        hid.Value = "0";
                        var mData = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.MUNID).FirstOrDefault();
                        mfunid = mData.ID;
                    }
                    else
                    {
                        var mData = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == value.MasterUNID).FirstOrDefault();
                        if(mData != null)
                        mfunid = mData.ID;
                    }

                    SaveImageFileData(ImageManager.file_section_fundriser, value.ID, mfunid);
                    SaveImageFileData(ImageManager.file_section_fundriser_logo, value.ID, mfunid);
                    SaveImageFileData(ImageManager.file_section_fundriser_my_logo, value.ID, mfunid);
                    SaveImageFileData(ImageManager.file_section_fundriser_top, value.ID, mfunid);
                   // SaveImageFileData(ImageManager.file_section_fundriser_top, value.ID, mfunid);

                    retsult = "Inserted";
                    retval = value.ID;

                    if (value.ID > 0)
                    {
                        //Create a project / Request
                       // CreateAProject(value.unid, Section_Fundriser, value.Title, value.StartDate.Value, value.EndDate.Value);
                    }


                    UpdateQRCodeImage(value.unid);

                    sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;
                    BannerUpLoad(value.ID);
                    LogoUpLoad(value.ID);
                    img.ImageUrl = "~/ImageHandler.ashx?id=" + value.ID + "&s=" + ImageManager.file_section_fundriser;// GetImageUrl(value.ID.ToString());
                    imglogoShow.ImageUrl = "~/ImageHandler.ashx?id=" + value.ID + "&s=" + ImageManager.file_section_fundriser_logo;//  GetLogoImageUrl(value.ID.ToString());

                    YourLogoUpLoad(value.ID);

                    CopyMasterAMount(QueryStringValues.MUNID, value.unid);

                    //update fundriser infor

                    var fNew = PortfolioMgt.BAL.FundraisersInfoBAL.FundraisersInfoBAL_SelectAll().Where(o => o.MainFundUNID == QueryStringValues.MUNID && o.Email.ToLower() == sessionKeys.UEmail.ToLower()).FirstOrDefault();
                    if(fNew != null)
                    {
                        fNew.FundUNID = value.unid;
                        fNew.Status = PortfolioMgt.BAL.FundriaserUserStatus.Invitation_Accepted;
                        
                        PortfolioMgt.BAL.FundraisersInfoBAL.FundraisersInfoBAL_Update(fNew);
                    }

                }
                else
                {
                    //update  fundriser
                    value.Title = txtTitle.Text;
                    value.Description = txtDescriptionArea.Text;
                    value.FundraiserDetails = txtyourstory.Text;
                    value.DefaultTarget = float.Parse(txtcurrenyValue.Text);
                    // value.Currency = // ddlCurrency.SelectedValue;
                    value.ModifiedDateTime = DateTime.Now;
                    //value.CreatedDateTime = DateTime.Now;
                    value.LoggedByID = sessionKeys.UID;// Convert.ToInt32(ddlOwner.SelectedValue);
                                                       // value.OrganizationID = 1;
                    value.SendMailAfterDonation = true;// chkSendEmail.Checked;
                    value.ShowProgress = chkWall.Checked;
                    value.EnableP2P = chkActive.Checked;

                    if (txtStartDate.Text.Trim().Length > 0)
                        value.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                    else
                        value.StartDate = DateTime.Now;// Convert.ToDateTime(txtStartDate.Text.Trim());
                    if (txtEndDate.Text.Trim().Length > 0)
                        value.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());
                    else
                        value.EndDate = DateTime.Now.AddDays(30);// Convert.ToDateTime(TextEndDate.Text.Trim());

                    // value.DefaultValues = GetChecklist();
                    // value.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                    //value.EndDate = Convert.ToDateTime(TextEndDate.Text.Trim());
                    //value.DefaultBanner = "";
                    value.ShowQRCode = true;
                    value.ShowChart = true;
                    value.Address = txtAddress.Text.Trim();
                    value.City = txtCity.Text.Trim();
                    value.State = txtState.Text.Trim();
                    value.Postcode = txtPostcode.Text.Trim();
                    value.Country = ddlcountry.SelectedValue;
                    //value.DefaultValues = "";

                    if (value.unid == null)
                        value.unid = Guid.NewGuid().ToString();

                    TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Update(value);

                    var mfunid = 0;
                    if (QueryStringValues.MUNID.Length > 0)
                    {
                        hid.Value = "0";
                        var mData = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.MUNID).FirstOrDefault();
                        mfunid = mData.ID;
                    }
                    else
                    {
                        var mData = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == value.MasterUNID).FirstOrDefault();
                        if (mData != null)
                            mfunid = mData.ID;
                    }

                    SaveImageFileData(ImageManager.file_section_fundriser, value.ID, mfunid);
                    SaveImageFileData(ImageManager.file_section_fundriser_logo, value.ID, mfunid);
                    SaveImageFileData(ImageManager.file_section_fundriser_my_logo, value.ID, mfunid);
                    SaveImageFileData(ImageManager.file_section_fundriser_top, value.ID, mfunid);

                    if (value.ID > 0)
                    {
                        retsult = "Updated";
                        //Create a project / Request
                       // CreateAProject(value.unid, Section_Fundriser, value.Title, value.StartDate.Value, value.EndDate.Value);
                    }

                    UpdateQRCodeImage(value.unid);

                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                    BannerUpLoad(value.ID);
                    LogoUpLoad(value.ID);

                    img.ImageUrl = "~/ImageHandler.ashx?id=" + value.ID + "&s=" + ImageManager.file_section_fundriser;// GetImageUrl(value.ID.ToString());
                    imglogoShow.ImageUrl = "~/ImageHandler.ashx?id=" + value.ID + "&s=" + ImageManager.file_section_fundriser_logo;//  GetLogoImageUrl(value.ID.ToString());
                    YourLogoUpLoad(value.ID);
                    retval = value.ID;

                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }

        private void CopyFileData()
        {

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
                string code = Deffinity.systemdefaults.GetWebUrl() + "/FundraiserView.aspx?unid=" + unid; //ab.MoreDetails;

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
        protected static string GetMyLogoImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing/") + "Tithing_org_" + contactsId.ToString() + "_mylogo.png";

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("~/WF/UploadData/Tithing/Tithing_org_{0}_mylogo.png", contactsId.ToString());
                else
                    img = string.Format("~/WF/UploadData/Tithing/Tithing_org_{0}_mylogo.png", contactsId.ToString());
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
                ImageManager.SaveTithingImage_setpath(imgBanner.FileBytes, tithingid.ToString(), Deffinity.systemdefaults.GetLogoFolderPath());
                // DisplayLogo();
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
        private void YourLogoUpLoad(int tithingid)
        {


            if (file_yourlogo.PostedFile.FileName.Length > 0)
            {
                Bitmap upBmp = (Bitmap)Bitmap.FromStream(file_yourlogo.PostedFile.InputStream);
                ImageManager.SaveTithingMyLogoImage_setpath(file_yourlogo.FileBytes, tithingid.ToString(), Deffinity.systemdefaults.GetLogoFolderPath());
                // DisplayLogo();
            }

        }
        protected void btnAddAmount_Click(object sender, EventArgs e)
        {
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
                if (defaultmoney != null)
                {
                    if (defaultmoney.Length > 0)
                    {
                        var sitems = defaultmoney.Split(',');
                        foreach (var s in sitems)
                        {
                            if (s.Trim().Length > 0)
                            {
                                var m = PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_SelectAll().Where(o => o.TithingDefaultUnid == huid.Value).Where(o => o.Amount == Convert.ToDouble(s.Trim())).FirstOrDefault();
                                if (m == null)
                                {
                                    PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_Add(new TithingAmount() { TithingDefaultUnid = huid.Value, Amount = Convert.ToDouble(s.Trim()) });
                                }
                            }

                        }

                        //update detultmoney 
                        var clist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == huid.Value).FirstOrDefault();
                        if (clist != null)
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
                string unid = QueryStringValues.UNID.Length > 0 ? QueryStringValues.UNID : huid.Value;

                var mList = PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_SelectAll().Where(o => o.TithingDefaultUnid == unid).ToList();
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

        private void CopyMasterAMount(string munid,string unid)
        {
            try
            {
                var mlist = PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_SelectAll().Where(o => o.TithingDefaultUnid == munid).ToList();
                var clist = PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_SelectAll().Where(o => o.TithingDefaultUnid == unid).ToList();
                if (clist.Count() == 0)
                {
                    foreach(var m in mlist.ToList())
                    {
                        PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_Add(new TithingAmount() { Amount = m.Amount, Description = m.Description, Shortdescription = m.Shortdescription, TithingDefaultUnid = unid });

                    }

                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void btnSaveAmount_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(hid.Value);

                //if(id == 0)
                //{
                //    id = SaveData();
                //}

                var aid = Convert.ToInt32(haid.Value);
                if (haid.Value.Length > 0)
                {
                    var aEntity = PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_SelectAll().Where(o => o.ID == aid).FirstOrDefault();

                    if (aEntity == null)
                    {
                        aEntity = new PortfolioMgt.Entity.TithingAmount();
                        aEntity.Amount = Convert.ToDouble(txtAmount.Text.Trim());
                        aEntity.Shortdescription = txtShortDescription.Text.Trim();
                        aEntity.Description = txtDescription.Text.Trim();
                        aEntity.TithingDefaultUnid = QueryStringValues.UNID.Length>0? QueryStringValues.UNID:huid.Value;

                        PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_Add(aEntity);
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Addedsuccessfully, "");
                    }
                    else
                    {
                        aEntity.Amount = Convert.ToDouble(txtAmount.Text.Trim());
                        aEntity.Shortdescription = txtShortDescription.Text.Trim();
                        aEntity.Description = txtDescription.Text.Trim();
                        //aEntity.TithingDefaultUnid = huid.Value;
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
                PortfolioMgt.Entity.TithingDefaultDetail value = new TithingDefaultDetail(); ;
                if (QueryStringValues.EID > 0)
                    value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();


                if (QueryStringValues.UNID.Length > 0)
                    value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                if (value != null)
                {
                    BannerUpLoad(value.ID);
                    Response.Redirect("~/App/Fundraiser/MyFunraiser.aspx?type=main&unid=" + value.unid, false);
                }
                else
                {
                    string result = "";
                    var id = SaveData(out result);



                    if (id > 0)
                    {
                        value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == id).FirstOrDefault();
                        BannerUpLoad(id);
                    }

                    Response.Redirect("~/App/Fundraiser/MyFunraiser.aspx?type=main&unid=" + value.unid, false);

                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnSaveLogo_Click(object sender, EventArgs e)
        {
            try
            {
                PortfolioMgt.Entity.TithingDefaultDetail value = new TithingDefaultDetail(); ;
                if (QueryStringValues.EID > 0)
                    value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == QueryStringValues.EID).FirstOrDefault();


                if (QueryStringValues.UNID.Length > 0)
                    value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                if (value != null)
                {
                    LogoUpLoad(value.ID);
                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                    Response.Redirect("~/App/Fundraiser/MyFunraiser.aspx?type=main&unid=" + value.ID, false);
                }
                else
                {
                    string result = "";
                    var id = SaveData(out result);

                    if (id > 0)
                    {
                        LogoUpLoad(id);
                        value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == id).FirstOrDefault();
                    }

                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;


                    Response.Redirect("~/App/Fundraiser/MyFunraiser.aspx?type=main&unid=" + value.unid, false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnPeertoPeer_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App/Fundraiser/peertopeer/Dashboard.aspx?unid=" + huid.Value);
        }

        protected void btnSaveFundraiser_Click(object sender, EventArgs e)
        {
            try
            {
                string result = ""; ;
                int id = SaveData(out result);

                var value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == id).FirstOrDefault();

                sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;


                Response.Redirect("~/App/Fundraiser/MyFunraiser.aspx?type=main&unid=" + value.unid, false);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSaveYourlogo_Click(object sender, EventArgs e)
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
                    YourLogoUpLoad(value.ID);
                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                    Response.Redirect("~/App/Fundraiser/MyFunraiser.aspx?type=main&unid=" + value.unid, false);
                }
                else
                {
                    string result = "";
                    var id = SaveData(out result);

                    if (id > 0)
                    {
                        value = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.ID == id).FirstOrDefault();
                        YourLogoUpLoad(value.ID);
                    }

                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;


                    Response.Redirect("~/App/Fundraiser/MyFunraiser.aspx?type=main&unid=" + value.unid, false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnPreView_Click(object sender, EventArgs e)
        {
            if(QueryStringValues.UNID.Length>0)
            Response.Redirect("~/FundraiserView.aspx?type=main&unid=" + QueryStringValues.UNID, false);
        }
    }
}