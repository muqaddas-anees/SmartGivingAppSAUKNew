using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class Images : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["orgid"] != null)
            {
                var id = Request.QueryString["orgid"];
                var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(Request.QueryString["orgid"].ToString())).FirstOrDefault();

                if (pEntity != null)
                {
                    lblGallery.Text = pEntity.PortFolio;
                    lblGalleryName.Text = pEntity.PortFolio;
                }


                }


            LoadImages();
        }

       


        protected void UploadFile(object sender, EventArgs e)
        {




            if (FileUpload1.HasFile)
            {
                string FileType = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower().Trim();
                // Checking the format of the uploaded file.  
                if (FileType != ".jpg" && FileType != ".png" && FileType != ".jpeg" )
                {
                    lblFilenotSelected.Text = "File Format Not Supported. Only png, jpg and jpeg. file formats are allowed";
                    
                }
                else
                {
                    string OrganizationImagesId = "OrganizationImages_";

                    if (Request.QueryString["orgid"] != null)
                    {
                        var id = Request.QueryString["orgid"];
                        OrganizationImagesId = OrganizationImagesId + id + "/";
                    }



                    string folderPath = Server.MapPath("~/WF/UploadData/OrganizationImages/" + OrganizationImagesId);

                    var unicId = Guid.NewGuid();





                    if (!(Directory.Exists(folderPath)))
                    {
                        Directory.CreateDirectory(folderPath);



                        FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));
                    }
                    else
                    {


                        FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));
                    }






                    //Display the Picture in Image control.
                    img.ImageUrl = "~/WF/UploadData/OrganizationImages/" + OrganizationImagesId + Path.GetFileName(FileUpload1.FileName);

                }
            }
            else
            {
                lblFilenotSelected.Text = "No  file selected";
            }


            LoadImages();


        }




        private void LoadImages()
        {
            string OrganizationImagesId = "OrganizationImages_";

            if (Request.QueryString["orgid"] != null)
            {
                var id = Request.QueryString["orgid"];
                OrganizationImagesId = OrganizationImagesId + id+"/";
            }

          

            string folderPath = Server.MapPath("~/WF/UploadData/OrganizationImages/" + OrganizationImagesId);


            if ((Directory.Exists(folderPath)))
            {
                ImageList.Controls.Clear();

                foreach (string strfile in Directory.GetFiles(Server.MapPath("~/WF/UploadData/OrganizationImages/" + OrganizationImagesId)))
                {
                    ImageButton imageButton = new ImageButton();
                    FileInfo fi = new FileInfo(strfile);
                    imageButton.ImageUrl = "~/WF/UploadData/OrganizationImages/" + OrganizationImagesId + fi.Name;
                    imageButton.Height = Unit.Pixel(200);
                    imageButton.Style.Add("padding", "20px");
                    imageButton.Width = Unit.Pixel(200);
                    //   imageButton.Click += new ImageClickEventHandler(imageButton_Click);
                    //  Panel1.Controls.Add(imageButton);

                    ImageList.Controls.Add(imageButton);
                }
            }
            



               
        }



        protected void imageButton_Click(object sender, ImageClickEventArgs e)
        {

            //var imageDetail = ((ImageButton)sender).ImageUrl;

            //Response.Redirect("WebForm2.aspx?ImageURL=" +
            //    ((ImageButton)sender).ImageUrl);
        }


    }





  


}