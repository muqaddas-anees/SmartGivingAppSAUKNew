using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class Activity : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindCategory();
                    BindSubCategory();
                    //  BindReligion();
                    if (Request.QueryString["mid"] != null)
                    {
                        var id = Request.QueryString["mid"].ToString();

                        var Aid = Convert.ToInt32(Request.QueryString["mid"].ToString());

                        // IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();
                        var ActivityDetail = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectAll().Where(o => o.ID == Aid).FirstOrDefault();
                        if (ActivityDetail != null)
                        {
                            txtTitle.Text = ActivityDetail.Title;
                            txtDescriptionArea.Text = ActivityDetail.Description;
                            txtNotes.Text = ActivityDetail.Notes;
                            ddlActiviteCategory.SelectedValue = ActivityDetail.ActivityCategoryID.ToString();
                            BindSubCategory();
                            ddlSubCategory.SelectedValue = ActivityDetail.ActivitySubCategoryID.ToString();
                            txtStartDate.Text = ActivityDetail.StartDateTime.ToShortDateString();
                            TextEndDate.Text = ActivityDetail.EndDateTime.ToShortDateString();
                            txtStartTime.Text = ActivityDetail.StartDateTime.ToShortTimeString().Substring(0, 5);
                            txtEndTime.Text = ActivityDetail.EndDateTime.ToShortTimeString().Substring(0, 5);
                            ckbIsActive.Checked = ActivityDetail.IsActive;
                            txtSlot.Text = ActivityDetail.Slots.ToString();
                            txtPrice.Text = string.Format("{0:F2}", ActivityDetail.Price);

                            if (File.Exists(Server.MapPath("~/WF/UploadData/Event_" + ActivityDetail.ID + ".png")))
                            {
                                img.Visible = true;
                                img.ImageUrl = "~/WF/UploadData/Event_" + ActivityDetail.ID + ".png";
                            }
                            else
                            {
                                img.Visible = false;
                            }
                        }

                        BindSpeakerDetails();
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            lblModelHeading.Text = "Add Speaker";
            ImagePageBackground.ImageUrl = "";

            CKEditorSpeakerDetails.Text = "";

            TextBoxLinkedIn.Text = "";

            txtSpeakerName.Text = "";

            lblFilenotSelected.Text = "";


            mdlAddSpeaker.Show();

            //lblModelHeading.Text = "Edit Banner";
        }
        protected void UploadBanner_Click(object sender, EventArgs e)
        {
            var cRep = new PortfolioRepository<PortfolioMgt.Entity.Speaker_Detail>();



            var pEntity = new PortfolioMgt.Entity.Speaker_Detail();


            var unicId = Guid.NewGuid();

            // Add New Banner

            if (lblModelHeading.Text == "Add Speaker")
            {








                if (FileUploadPageBackground.HasFile)
                {
                    string FileType = Path.GetExtension(FileUploadPageBackground.PostedFile.FileName).ToLower().Trim();
                    // Checking the format of the uploaded file.  

                    string FileName = unicId + FileType;

                    if (FileType != ".jpg" && FileType != ".png" && FileType != ".jpeg")
                    {
                        lblFilenotSelected.Text = "File Format Not Supported. Only png, jpg and jpeg. file formats are allowed";

                    }
                    else
                    {


                        string folderPath = Server.MapPath("~/WF/UploadData/Speakers_Imgages/");


                        if (!(Directory.Exists(folderPath)))
                        {
                            Directory.CreateDirectory(folderPath);



                            FileUploadPageBackground.SaveAs(folderPath + Path.GetFileName(FileName));
                        }
                        else
                        {


                            FileUploadPageBackground.SaveAs(folderPath + Path.GetFileName(FileName));
                        }






                        //Display the Picture in Image control.
                        ImagePageBackground.ImageUrl = "~/WF/UploadData/Speakers_Imgages/" + Path.GetFileName(FileName);

                    }



                    pEntity.Speaker_Photo = FileName;



                }
                else
                {
                    lblFilenotSelected.Text = "No  file selected";
                }


                if (Request.QueryString["mid"] != null)
                {
                    var id = Request.QueryString["mid"].ToString();

                    int EventID = Convert.ToInt32(Request.QueryString["mid"].ToString());

                    pEntity.Event_ID = EventID;

                }

                pEntity.Speaker_Name = txtSpeakerName.Text;
                pEntity.Speaker_Bio = CKEditorSpeakerDetails.Text;
                pEntity.LinkedIn = TextBoxLinkedIn.Text;
                cRep.Add(pEntity);

            }
            else
            {

                try
                {

                    int id = Convert.ToInt32(HiddenSpeakerID.Value);

                    var list = cRep.GetAll().Where(o => o.Speaker_ID == id).FirstOrDefault();
                    //   FileUploadPageBackground.PostedFile.FileName;

                    string filenamePresent = ImagePageBackground.ImageUrl.Split('/')[ImagePageBackground.ImageUrl.Split('/').Length - 1];


                    //FileInfo info = new FileInfo(myCompleteFilePath);
                    //string fileNameWithoutPath = info.Name;




                    if (FileUploadPageBackground.HasFile)
                    {

                        string fileNameWithoutPath = FileUploadPageBackground.FileName;

                        string FileType = Path.GetExtension(FileUploadPageBackground.PostedFile.FileName).ToLower().Trim();

                        // Checking the format of the uploaded file.  

                        string FileName = unicId + FileType;

                        if (filenamePresent != fileNameWithoutPath)
                        {




                            if (FileType != ".jpg" && FileType != ".png" && FileType != ".jpeg")
                            {
                                lblFilenotSelected.Text = "File Format Not Supported. Only png, jpg and jpeg. file formats are allowed";

                            }
                            else
                            {

                                string folderPath = Server.MapPath("~/WF/UploadData/Speakers_Imgages/");


                                if (!(Directory.Exists(folderPath)))
                                {
                                    Directory.CreateDirectory(folderPath);

                                    FileUploadPageBackground.SaveAs(folderPath + Path.GetFileName(FileName));
                                }
                                else
                                {
                                    FileUploadPageBackground.SaveAs(folderPath + Path.GetFileName(FileName));
                                }


                                //Display the Picture in Image control.
                                ImagePageBackground.ImageUrl = "~/WF/UploadData/Speakers_Imgages/" + Path.GetFileName(FileName);

                            }

                            list.Speaker_Photo = FileName;

                        }



                    }


                    //string filename2 = list.BannerID;

                    if (Request.QueryString["mid"] != null)
                    {
                        var sid = Request.QueryString["mid"].ToString();

                        int EventID = Convert.ToInt32(Request.QueryString["mid"].ToString());

                        list.Event_ID = EventID;

                    }

                    list.Speaker_Name = txtSpeakerName.Text;
                    list.Speaker_Bio = CKEditorSpeakerDetails.Text;
                    list.LinkedIn = TextBoxLinkedIn.Text;


                    cRep.Edit(list);





                }

                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }





            }

            BindSpeakerDetails();


        }

        private void BindSpeakerDetails()
        {
            if (Request.QueryString["mid"] != null)
            {
                var id = Request.QueryString["mid"].ToString();

                int EventID = Convert.ToInt32(Request.QueryString["mid"].ToString());

                var cRep = new PortfolioRepository<PortfolioMgt.Entity.Speaker_Detail>();


                var list = cRep.GetAll().Where(o => o.Event_ID == EventID).ToList();



                BannerList.DataSource = list;
                BannerList.DataBind();


            }




        }

        public DateTime GetDateTime(string d_date, string t_time, string t_am_pm)
        {

            string s = string.Format("{0} {1} {2}", d_date, t_time, t_am_pm);
            DateTimeFormatInfo fi = new CultureInfo("en-US", false).DateTimeFormat;
            DateTime myDate = DateTime.ParseExact(s, "MM/dd/yyyy hh:mm:ss tt", fi);

            return myDate;
        }
        private void BindCategory()
        {
            var lc = PortfolioMgt.BAL.ActivityCategoryBAL.ActivityCategoryBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
            if (lc.Count > 0)
            {
                ddlActiviteCategory.DataSource = lc;
                ddlActiviteCategory.DataTextField = "Name";
                ddlActiviteCategory.DataValueField = "ID";
                ddlActiviteCategory.DataBind();
            }
            ddlActiviteCategory.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        private void BindSubCategory()
        {
            try
            {
                int ActivityCategoryID = int.Parse(ddlActiviteCategory.SelectedValue);
                var lc = PortfolioMgt.BAL.ActivitySubCategoryBAL.ActivitySubCategoryBAL_SelectAll().Where(o => o.ActivityCategoryID == ActivityCategoryID).ToList();
                if (lc.Count > 0)
                {
                    ddlSubCategory.DataSource = lc;
                    ddlSubCategory.DataTextField = "Name";
                    ddlSubCategory.DataValueField = "ID";
                    ddlSubCategory.DataBind();
                }

                ddlSubCategory.Items.Insert(0, new ListItem("Please select...", "0"));
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

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Speakers_Imgages/") + contactsId;

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("~/WF/UploadData/Speakers_Imgages/{0}", contactsId);
                else
                    img = string.Format("~/WF/UploadData/Speakers_Imgages/{0}", contactsId);
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }


        protected void btnSaveAndEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = sessionKeys.UID;

                IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                if (Request.QueryString["mid"] != null)
                {
                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();
                    var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                    var cDetails = cRep.GetAll().Where(o => o.ID == uid).FirstOrDefault();
                    if (cDetails != null)
                    {
                        var value = cRep.GetAll().Where(o => o.ID == uid).FirstOrDefault();

                       //value.OrganizationID = sessionKeys.PortfolioID;
                        value.ActivityCategoryID = int.Parse(ddlActiviteCategory.SelectedValue);
                        value.ActivitySubCategoryID = int.Parse(ddlSubCategory.SelectedValue);
                        value.StartDateTime = Convert.ToDateTime(txtStartDate.Text + " " + (string.IsNullOrEmpty(txtStartTime.Text) ? "00:00:00" : txtStartTime.Text + ":00"));// Convert.ToDateTime(!string.IsNullOrEmpty( txtStartDate.Text.Trim())? txtStartDate.Text.Trim():DateTime.Now.ToShortDateString());
                        value.EndDateTime = Convert.ToDateTime(TextEndDate.Text + " " + (string.IsNullOrEmpty(txtEndTime.Text) ? "00:00:00" : txtEndTime.Text + ":00"));//Convert.ToDateTime(!string.IsNullOrEmpty( TextEndDate.Text.Trim())? TextEndDate.Text.Trim():DateTime.Now.ToShortDateString());
                        value.Title = txtTitle.Text;
                        value.Description = txtDescriptionArea.Text;
                        value.Notes = txtNotes.Text;
                        value.ModifiedDate = DateTime.Now;
                        value.IsActive = ckbIsActive.Checked;
                        value.Slots = Convert.ToInt32(!string.IsNullOrEmpty(txtSlot.Text.Trim()) ? txtSlot.Text.Trim() : "0");
                        value.Price = Convert.ToDouble(!string.IsNullOrEmpty(txtPrice.Text.Trim()) ? txtPrice.Text.Trim() : "0.00");
                        value.LoggedBy = sessionKeys.UID;
                        cRep.Edit(value);

                        uploadLogo("Event_"+value.ID.ToString());
                        sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                        Response.Redirect("~/App/Activities.aspx",false);
                    }
                }
                else
                {
                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();
                    var value = new PortfolioMgt.Entity.ActivityDetail();
                    value.OrganizationID = sessionKeys.PortfolioID;
                    value.ActivityCategoryID = int.Parse(ddlActiviteCategory.SelectedValue);
                    value.ActivitySubCategoryID = int.Parse(ddlSubCategory.SelectedValue);
                    value.StartDateTime = Convert.ToDateTime(txtStartDate.Text + " " + (string.IsNullOrEmpty(txtStartTime.Text) ? "00:00:00" : txtStartTime.Text + ":00"));// Convert.ToDateTime(!string.IsNullOrEmpty( txtStartDate.Text.Trim())? txtStartDate.Text.Trim():DateTime.Now.ToShortDateString());
                    value.EndDateTime = Convert.ToDateTime(TextEndDate.Text + " " + (string.IsNullOrEmpty(txtEndTime.Text) ? "00:00:00" : txtEndTime.Text + ":00"));//Convert.ToDateTime(!string.IsNullOrEmpty( TextEndDate.Text.Trim())? TextEndDate.Text.Trim():DateTime.Now.ToShortDateString());
                    value.Title = txtTitle.Text;
                    value.Description = txtDescriptionArea.Text;
                    value.Notes = txtNotes.Text;
                    value.ModifiedDate = DateTime.Now;
                    value.IsActive = ckbIsActive.Checked;
                    value.CreatedDate = DateTime.Now;
                    value.LoggedBy = sessionKeys.UID;
                    value.Slots = Convert.ToInt32(!string.IsNullOrEmpty(txtSlot.Text.Trim()) ? txtSlot.Text.Trim() : "0");
                    value.Price = Convert.ToDouble(!string.IsNullOrEmpty(txtPrice.Text.Trim()) ? txtPrice.Text.Trim() : "0.00");
                    cRep.Add(value);
                    uploadLogo("Event_" + value.ID.ToString());
                    sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;
                    Response.Redirect("~/App/Activities.aspx",false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlActiviteCategory_TextChanged1(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                lblModelHeading.Text = "Add Category";
                ddlActiviteCategory.SelectedValue = "0";
                mdlManageOptions.Show();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnAddDenimination_Click(object sender, EventArgs e)
        {
            try
            {
                lblModelHeading.Text = "Add Subcategory";
                ddlSubCategory.SelectedValue = "0";
                mdlManageOptions.Show();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSaveChangesPop_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblModelHeading.Text.Contains("Add Category"))
                {

                    if (txtAddReligion.Text.Trim().Length > 0)
                    {
                        PortfolioMgt.BAL.ActivityCategoryBAL.ActivityCategoryBAL_Add(new PortfolioMgt.Entity.ActivityCategory() { Name = txtAddReligion.Text.Trim(), OrganizationID = sessionKeys.PortfolioID});
                        txtAddReligion.Text = string.Empty;

                        BindCategory();
                        mdlManageOptions.Hide();
                    }
                }
                else
                {
                    var rid = Convert.ToInt32(ddlActiviteCategory.SelectedValue);
                    if (rid > 0)
                    {
                        if (txtAddReligion.Text.Trim().Length > 0)
                        {
                            PortfolioMgt.BAL.ActivitySubCategoryBAL.ActivitySubCategoryBAL_Add(new PortfolioMgt.Entity.ActivitySubCategory()
                            {
                                Name = txtAddReligion.Text.Trim(),
                                ActivityCategoryID = rid
                            });
                            txtAddReligion.Text = string.Empty;

                            BindSubCategory();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlActiviteCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCategory();
        }

        protected void btnCategoryDelete_click(object sender, EventArgs e)
        {
            try
            {
                var cid = Convert.ToInt32(ddlActiviteCategory.SelectedValue);
                PortfolioMgt.BAL.ActivityCategoryBAL.ActivityCategoryBAL_delete(cid);
                sessionKeys.Message = Resources.DeffinityRes.Deletedsuccessfully;
                Response.Redirect(Request.RawUrl, false);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnSubCategoryDelete_click(object sender, EventArgs e)
        {
            try
            {
                var cid = Convert.ToInt32(ddlSubCategory.SelectedValue);
                PortfolioMgt.BAL.ActivitySubCategoryBAL.ActivitySubCategoryBAL_delete(cid);
                sessionKeys.Message = Resources.DeffinityRes.Deletedsuccessfully;
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        protected void EditSpeakerinList(object sender, EventArgs e)
        {
            mdlAddSpeaker.Show();

            if (HiddenSpeakerID.Value != null && HiddenSpeakerID.Value != "")
            {



                var sid = HiddenSpeakerID.Value;

                int id = Convert.ToInt32(sid);

                var cRep = new PortfolioRepository<PortfolioMgt.Entity.Speaker_Detail>();

                var list = cRep.GetAll().Where(o => o.Speaker_ID == id).FirstOrDefault();



                // "~/WF/UploadData/OrganizationImages/OrganizationImages_100/" + list.BannerID + ".png";

                string imageUrl = "~/WF/UploadData/Speakers_Imgages/" + list.Speaker_Photo;



                lblModelHeading.Text = "Edit Speaker";
                ImagePageBackground.ImageUrl = imageUrl;

                CKEditorSpeakerDetails.Text = list.Speaker_Bio;

                TextBoxLinkedIn.Text = list.LinkedIn;

                txtSpeakerName.Text = list.Speaker_Name;

                lblFilenotSelected.Text = "";


            }







        }

        private void uploadLogo(string blogref)
        {
            try
            {
                if (fileupload.PostedFile.FileName.Length > 0)
                {
                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(fileupload.PostedFile.InputStream);
                    ImageManager.SaveImage_setfullpath( blogref, fileupload.FileBytes, Deffinity.systemdefaults.GetLocalPath());
                    // DisplayLogo();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}