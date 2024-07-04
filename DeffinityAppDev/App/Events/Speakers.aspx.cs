using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static QRCoder.SvgQRCode;

namespace DeffinityAppDev.App.Events
{
    public partial class ManageSpeakers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["unid"] != null)
                    {
                        var id = Request.QueryString["unid"].ToString();



                        //  IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();


                        // var ActivityDetail = pRep.GetAll().Where(o => o.ID == Aid).FirstOrDefault();


                        BindSpeakerDetails(id);




                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindSpeakerDetails(string unid)
        {


            var uEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(unid);
            
            var cRep = new PortfolioRepository<PortfolioMgt.Entity.Speaker_Detail>();


            var list = cRep.GetAll().Where(o => o.Event_ID == uEntity.ID).ToList();
            hEventid.Value = uEntity.ID.ToString();


            BannerList.DataSource = list;
            BannerList.DataBind();



        }

        protected static string GetImageUrl(object contactsId)
        {
           

            return "~/ImageHandler.ashx?id=" + contactsId + "&s=" + ImageManager.file_section_speaker; 

        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App/Events/EventList.aspx",false);
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
        protected void EditSpeakerinList(object sender, EventArgs e)
        {
            mdlAddSpeaker.Show();

            try
            {
                if (HiddenSpeakerID.Value != null && HiddenSpeakerID.Value != "")
                {



                    var sid = HiddenSpeakerID.Value;

                    int id = Convert.ToInt32(sid);

                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.Speaker_Detail>();

                    var list = cRep.GetAll().Where(o => o.Speaker_ID == id).FirstOrDefault();


                    lblModelHeading.Text = "Edit Speaker";
                    ImagePageBackground.ImageUrl = "~/ImageHandler.ashx?id=" + list.Speaker_ID.ToString() + "&s=" + ImageManager.file_section_speaker; ;

                    CKEditorSpeakerDetails.Text = list.Speaker_Bio;

                    TextBoxLinkedIn.Text = list.LinkedIn;

                    txtSpeakerName.Text = list.Speaker_Name;

                    lblFilenotSelected.Text = "";


                }


            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }




        }
        protected void UploadBanner_Click(object sender, EventArgs e)
        {
            var cRep = new PortfolioRepository<PortfolioMgt.Entity.Speaker_Detail>();



            var pEntity = new PortfolioMgt.Entity.Speaker_Detail();


            var unicId = Guid.NewGuid();

            // Add New Banner

            if (lblModelHeading.Text == "Add Speaker")
            {

                pEntity.Speaker_Photo = "Speaker";
                pEntity.Event_ID = Convert.ToInt32( hEventid.Value);

                pEntity.Speaker_Name = txtSpeakerName.Text;
                pEntity.Speaker_Bio = CKEditorSpeakerDetails.Text.Trim();
                pEntity.LinkedIn = TextBoxLinkedIn.Text;
                cRep.Add(pEntity);

                if (FileUploadPageBackground.PostedFile.FileName.Length > 0)
                {
                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(FileUploadPageBackground.PostedFile.InputStream);
                    ImageManager.SaveSpeakerImage_setpath(FileUploadPageBackground.FileBytes, pEntity.Speaker_ID.ToString(), Deffinity.systemdefaults.GetUsersFolderPath());
                    // DisplayLogo();
                }

            }
            else
            {

                try
                {

                    int id = Convert.ToInt32(HiddenSpeakerID.Value);

                    var list = cRep.GetAll().Where(o => o.Speaker_ID == id).FirstOrDefault();
                  

                    list.Speaker_Name = txtSpeakerName.Text;
                  //  list.Speaker_Bio = CKEditorSpeakerDetails.Text;
                    list.LinkedIn = TextBoxLinkedIn.Text;

                    list.Speaker_Bio =CKEditorSpeakerDetails.Text.Trim();
                    cRep.Edit(list);

                    lblModelHeading.Text = "Add Speaker";

                    if (FileUploadPageBackground.PostedFile.FileName.Length > 0)
                    {
                        Bitmap upBmp = (Bitmap)Bitmap.FromStream(FileUploadPageBackground.PostedFile.InputStream);
                        ImageManager.SaveSpeakerImage_setpath(FileUploadPageBackground.FileBytes, list.Speaker_ID.ToString(), Deffinity.systemdefaults.GetUsersFolderPath());
                        // DisplayLogo();
                    }

                }

                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }





            }
            if (Request.QueryString["unid"] != null)
            {
                var id = Request.QueryString["unid"].ToString();
                BindSpeakerDetails(id);
            }


        }

        protected void BannerList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edit1")
                {
                    mdlAddSpeaker.Show();

                    var id = Convert.ToInt32(e.CommandArgument.ToString());

                    if (id > 0)
                    {

                       

                        // var sid = HiddenSpeakerID.Value;

                        //int id = Convert.ToInt32(sid);

                        var cRep = new PortfolioRepository<PortfolioMgt.Entity.Speaker_Detail>();

                        var list = cRep.GetAll().Where(o => o.Speaker_ID == id).FirstOrDefault();

                        if (list != null)
                        {

                            HiddenSpeakerID.Value = list.Speaker_ID.ToString();
                            // "~/WF/UploadData/OrganizationImages/OrganizationImages_100/" + list.BannerID + ".png";

                         

                            lblModelHeading.Text = "Edit Speaker";
                            ImagePageBackground.ImageUrl = "~/ImageHandler.ashx?id=" + list.Speaker_ID.ToString() + "&s=" + ImageManager.file_section_speaker; // imageUrl;

                            CKEditorSpeakerDetails.Text = list.Speaker_Bio;

                            TextBoxLinkedIn.Text = list.LinkedIn;

                            txtSpeakerName.Text = list.Speaker_Name;

                            lblFilenotSelected.Text = "";
                        }


                    }


                }

                else if (e.CommandName == "del")
                {
                    var id = Convert.ToInt32(e.CommandArgument.ToString());
                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.Speaker_Detail>();

                    var list = cRep.GetAll().Where(o => o.Speaker_ID == id).FirstOrDefault();

                    if(list != null)
                    {
                        cRep.Delete(list);
                    }

                }

               

                BindSpeakerDetails(QueryStringValues.UNID);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}
