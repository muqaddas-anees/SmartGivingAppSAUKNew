using AngleSharp.Dom;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
   


    public partial class AddLiveSession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FirstFunction()", true);

                    //BindReligion();

                    SetData();

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void SetData()
        {
            if (QueryStringValues.EID > 0)
            {
                var cRep = new PortfolioRepository<PortfolioMgt.Entity.LiveSeesionDetail>();
                var value = cRep.GetAll().Where(o => o.SessionId == QueryStringValues.EID).FirstOrDefault();
                if (value != null)
                {
                    txtLiveSessionTitle.Text = value.SessionTitle;
                    TextAreaDescription.Text = value.Description;
                    txtSpeakers.Text = value.Speakers;
                    txtDateScheduled.Text = value.DateScheduled.Value.ToShortDateString();
                    CheckBoxEnableTithing.Checked = value.EnableTithing == "True" ? true : false;
                    txtZoomLink.Text = value.ZoomLink;
                    txtRecordedLink.Text = value.RecordedLink;
                    txtZoomUserID.Text = value.metting_userid;
                    txtZoomAPIKey.Text = value.metting_apikey;
                    txtZoomSecretCode.Text = value.metting_secrete;
                    ImagePageBackground.ImageUrl = GetImageUrl(value.unid);
                }
            }
            else
            {
                txtDateScheduled.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {

            var cRep = new PortfolioRepository<PortfolioMgt.Entity.LiveSeesionDetail>();

          

          //  var text = CKEditorTextArea.Text;

            try
            {
                if (QueryStringValues.EID == 0)
                {
                    var value = new PortfolioMgt.Entity.LiveSeesionDetail();
                    value.SessionTitle = txtLiveSessionTitle.Text.Trim();
                    value.Description = TextAreaDescription.Text.Trim();
                    value.Speakers = txtSpeakers.Text.Trim();
                    value.DateScheduled = Convert.ToDateTime(txtDateScheduled.Text);
                    value.EnableTithing = CheckBoxEnableTithing.Checked ? "True" : "False";// (CheckBoxEnableTithing.Checked).ToString().Trim();
                    value.ZoomLink = txtZoomLink.Text.Trim();
                    value.RecordedLink = txtRecordedLink.Text.Trim();
                    value.metting_userid = txtZoomUserID.Text.Trim();
                    value.metting_apikey = txtZoomAPIKey.Text.Trim();
                    value.metting_secrete = txtZoomSecretCode.Text.Trim();
                    value.unid = Guid.NewGuid().ToString();
                    cRep.Add(value);

                    if (FileUploadPageBackground.HasFile)
                    {
                        string FileType = Path.GetExtension(FileUploadPageBackground.PostedFile.FileName).ToLower().Trim();
                        // Checking the format of the uploaded file.  

                        string FileName = value.unid + ".png";

                        string folderPath = Server.MapPath("~/WF/UploadData/Sessions/");


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
                        ImagePageBackground.ImageUrl = "~/WF/UploadData/Sessions/" + Path.GetFileName(FileName);

                    }
                  


                }
                else
                {
                    var value = cRep.GetAll().Where(o => o.SessionId == QueryStringValues.EID).FirstOrDefault();
                    if(value != null)
                    {
                        value.SessionTitle = txtLiveSessionTitle.Text.Trim();
                        value.Description = TextAreaDescription.Text.Trim();
                        value.Speakers = txtSpeakers.Text.Trim();
                        value.DateScheduled = Convert.ToDateTime(txtDateScheduled.Text);
                        value.EnableTithing = CheckBoxEnableTithing.Checked ? "True" : "False";//(CheckBoxEnableTithing.Checked).ToString().Trim();
                        value.ZoomLink = txtZoomLink.Text.Trim();
                        value.RecordedLink = txtRecordedLink.Text.Trim();
                        value.metting_userid = txtZoomUserID.Text.Trim();
                        value.metting_apikey = txtZoomAPIKey.Text.Trim();
                        value.metting_secrete = txtZoomSecretCode.Text.Trim();

                        cRep.Edit(value);

                        if (FileUploadPageBackground.HasFile)
                        {
                            string FileType = Path.GetExtension(FileUploadPageBackground.PostedFile.FileName).ToLower().Trim();
                            // Checking the format of the uploaded file.  

                            string FileName = value.unid + ".png";

                            string folderPath = Server.MapPath("~/WF/UploadData/Sessions/");


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
                            ImagePageBackground.ImageUrl = "~/WF/UploadData/Sessions/" + Path.GetFileName(FileName);

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

      





        protected void UploadFile(object sender, EventArgs e)
        {




            if (FileUploadPageBackground.HasFile)
            {
                string FileType = Path.GetExtension(FileUploadPageBackground.PostedFile.FileName).ToLower().Trim();
                // Checking the format of the uploaded file.  
                if (FileType != ".jpg" && FileType != ".png" && FileType != ".jpeg")
                {
                  //  lblFilenotSelected.Text = "File Format Not Supported. Only png, jpg and jpeg. file formats are allowed";

                }
                else
                {
                    string OrganizationImagesId = "OrganizationImages_";

                    if (Request.QueryString["orgid"] != null)
                    {
                        var id = Request.QueryString["orgid"];
                        OrganizationImagesId = OrganizationImagesId + id + "/";
                    }



                    string folderPath = Server.MapPath("~/WF/UploadData/sessions/" + OrganizationImagesId);

                    var unicId = Guid.NewGuid();





                    if (!(Directory.Exists(folderPath)))
                    {
                        Directory.CreateDirectory(folderPath);



                        FileUploadPageBackground.SaveAs(folderPath + Path.GetFileName(FileUploadPageBackground.FileName));
                    }
                    else
                    {


                        FileUploadPageBackground.SaveAs(folderPath + Path.GetFileName(FileUploadPageBackground.FileName));
                    }






                    //Display the Picture in Image control.
                    ImagePageBackground.ImageUrl = "~/WF/UploadData/sessions/" + OrganizationImagesId + Path.GetFileName(FileUploadPageBackground.FileName);

                }
            }
            else
            {
              //  lblFilenotSelected.Text = "No  file selected";
            }




        }

        protected string GetImageUrl(object activityid)
        {
            string retval = string.Empty;
            if (activityid != null)
            {
                if (File.Exists(Server.MapPath("~/WF/UploadData/sessions/" + activityid.ToString() + ".png")))
                {
                    retval = "~/WF/UploadData/sessions/" + activityid + ".png";
                }

                else
                {

                    retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
                }
            }
            else
                retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            return retval;

        }







    }



}