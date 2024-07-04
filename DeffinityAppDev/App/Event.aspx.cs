using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{


    public partial class Event : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {




                    //  BindReligion();
                    if (Request.QueryString["mid"] != null)
                    {
                        var id = Request.QueryString["mid"].ToString();

                        var Aid = Convert.ToInt32(Request.QueryString["mid"].ToString());

                        IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();


                        var ActivityDetail = pRep.GetAll().Where(o => o.ID == Aid).FirstOrDefault();

                        BindCategoryEdit();

                        BindSubCategoryEdit(ActivityDetail.ActivityCategoryID);


                        txtTitle.Text = ActivityDetail.Title;
                        CKEditorDetailedDescription.Text = ActivityDetail.Description;


                        // txtareaitemsDescription.Text= ActivityDetail.Description;


                       // ddlOrganisation.SelectedValue = ActivityDetail.OrganizationID.ToString();
                        ddlActiviteCategory.SelectedValue = ActivityDetail.ActivityCategoryID.ToString();
                        ddlSubCategory.SelectedValue = ActivityDetail.ActivitySubCategoryID.ToString();


                        DateTime sd = ActivityDetail.StartDateTime;
                        DateTime Sdtemp = Convert.ToDateTime(sd);
                        var startDate = Sdtemp.ToString("dd-MM-yyyy");



                        //  Apply.Text = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd");


                        string conv = ActivityDetail.StartDateTime.ToString();
                        conv = DateTime.Parse(conv).ToString("yyyy-MM-dd");



                        string convEnd = ActivityDetail.StartDateTime.ToString();
                        convEnd = DateTime.Parse(conv).ToString("yyyy-MM-dd");



                        // Apply.Text = DateTime.Now.ToString("dd-MM-yyyy");


                        //  Apply.Text = EndDate;

                        txtStartDate.Text = conv.ToString();

                        TextEndDate.Text = convEnd.ToString();

                        ckbIsActive.Checked = ActivityDetail.IsActive;





                    }
                    else
                    {

                        BindCategory();

                    }



                }
                else
                {
                    if (ddlSubCategory.SelectedValue != "0")
                    {
                        BindCategory();

                    }
                    else
                    {
                        BindSubCategory();
                    }


                }


                BindSpeakerDetails();



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }





        private void BindSpeakerDetails()
        {

            var cRep = new PortfolioRepository<PortfolioMgt.Entity.Speaker_Detail>();


            var list = cRep.GetAll().Where(o => o.Speaker_ID > 0).ToList();



            BannerList.DataSource = list;
            BannerList.DataBind();




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


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            lblModelHeading.Text = "Add Speaker";
            ImagePageBackground.ImageUrl = "";

            CKEditorSpeakerDetails.Text = "";

            TextBoxLinkedIn.Text = "";

            txtSpeakerName.Text = "";

            lblFilenotSelected.Text = "";


            mdlManageOptions.Show();

            //lblModelHeading.Text = "Edit Banner";
        }



        protected void RemoveImage_Click(object sender, EventArgs e)
        {
            ImagePageBackground.ImageUrl = "";
            mdlManageOptions.Show();
        }

        private void BindCategory()
        {
            ddlActiviteCategory_TextChanged();
            IPortfolioRepository<PortfolioMgt.Entity.ActivityCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityCategory>();
            var lc = pRep.GetAll().Where(o => o.OrganizationID == 124).ToList();



            if (lc.Count > 0)
            {
                ddlActiviteCategory.DataSource = lc;
                ddlActiviteCategory.DataSource = lc;
                ddlActiviteCategory.DataTextField = "Name";
                ddlActiviteCategory.DataValueField = "ID";
                ddlActiviteCategory.DataBind();
            }
            ddlActiviteCategory.Items.Insert(0, new ListItem("Event Category", "0"));
            // ddlSubCategory.Items.Insert(0, new ListItem("Activity Sub  Category: Bible Studies for Beginners", "0"));
        }

        private void BindCategoryEdit()
        {
            IPortfolioRepository<PortfolioMgt.Entity.ActivityCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityCategory>();
            var lc = pRep.GetAll().Where(o => o.OrganizationID == 124).ToList();



            if (lc.Count > 0)
            {
                ddlActiviteCategory.DataSource = lc;
                ddlActiviteCategory.DataSource = lc;
                ddlActiviteCategory.DataTextField = "Name";
                ddlActiviteCategory.DataValueField = "ID";
                ddlActiviteCategory.DataBind();
            }
            ddlActiviteCategory.Items.Insert(0, new ListItem("Event Category", "0"));
            // ddlSubCategory.Items.Insert(0, new ListItem("Activity Sub  Category: Bible Studies for Beginners", "0"));
        }


        private void BindSubCategory()
        {



            string data = ddlActiviteCategory.SelectedValue;

            int ActivityCategoryID = int.Parse(ddlActiviteCategory.SelectedValue);

            IPortfolioRepository<PortfolioMgt.Entity.ActivitySubCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivitySubCategory>();
            var lc = pRep.GetAll().Where(o => o.ActivityCategoryID == ActivityCategoryID).ToList();



            if (lc.Count > 0)
            {
                ddlSubCategory.DataSource = lc;
                ddlSubCategory.DataSource = lc;
                ddlSubCategory.DataTextField = "Name";
                ddlSubCategory.DataValueField = "ID";
                ddlSubCategory.DataBind();
            }

            ddlSubCategory.Items.Insert(0, new ListItem("activity sub  category", "0"));
        }



        private void BindSubCategoryEdit(int id)
        {


            IPortfolioRepository<PortfolioMgt.Entity.ActivitySubCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivitySubCategory>();
            var lc = pRep.GetAll().Where(o => o.ActivityCategoryID == id).ToList();



            if (lc.Count > 0)
            {
                ddlSubCategory.DataSource = lc;
                ddlSubCategory.DataSource = lc;
                ddlSubCategory.DataTextField = "Name";
                ddlSubCategory.DataValueField = "ID";
                ddlSubCategory.DataBind();
            }

            ddlSubCategory.Items.Insert(0, new ListItem("activity sub  category", "0"));
        }



        protected static string SpeakerID(string Sid)
        {
            return "" + Sid;
        }


        protected static string DeleteBanner(string contactsId)
        {
            return "" + contactsId;
        }







        protected void EditSpeakerinList(object sender, EventArgs e)
        {
            mdlManageOptions.Show();

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


        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;



            IPortfolioRepository<PortfolioMgt.Entity.Speaker_Detail> pRep = new PortfolioRepository<PortfolioMgt.Entity.Speaker_Detail>();


            var speackersDetails = pRep.GetAll().Where(o => o.Speaker_ID == Convert.ToInt32(contactsId)).FirstOrDefault();

            string fileName = speackersDetails.Speaker_Photo;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Speakers_Imgages/") + fileName;

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = "~/WF/UploadData/Speakers_Imgages/" + fileName;
                else
                    img = "~/WF/UploadData/Speakers_Imgages/" + fileName;
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "~/WF/UploadData/Speakers_Imgages/dummy-image.jpg";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            return img;
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }



        protected void ddlActiviteCategory_TextChanged()
        {
            BindSubCategory();
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


                        value.OrganizationID = sessionKeys.PortfolioID;// int.Parse(ddlOrganisation.SelectedValue);
                        value.ActivityCategoryID = int.Parse(ddlActiviteCategory.SelectedValue);
                        value.ActivitySubCategoryID = int.Parse(ddlSubCategory.SelectedValue);



                        value.StartDateTime = DateTime.Parse((txtStartDate.Text));
                        value.EndDateTime = DateTime.Parse(TextEndDate.Text);


                        value.Title = txtTitle.Text;
                        value.Description = CKEditorDetailedDescription.Text;




                        value.ModifiedDate = DateTime.Now;



                        value.IsActive = ckbIsActive.Checked;



                        cRep.Edit(value);



                    }
                }
                else
                {
                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                    var value = new PortfolioMgt.Entity.ActivityDetail();

                    value.OrganizationID = sessionKeys.PortfolioID;
                    value.ActivityCategoryID = int.Parse(ddlActiviteCategory.SelectedValue);
                    value.ActivitySubCategoryID = int.Parse(ddlSubCategory.SelectedValue);



                    value.StartDateTime = DateTime.Parse((txtStartDate.Text));
                    value.EndDateTime = DateTime.Parse(TextEndDate.Text);


                    value.Title = txtTitle.Text;
                    value.Description = CKEditorDetailedDescription.Text;




                    value.ModifiedDate = DateTime.Now;



                    value.IsActive = ckbIsActive.Checked;






                    value.CreatedDate = DateTime.Now;
                    value.LoggedBy = id;



                    cRep.Add(value);


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

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
    }




    
}