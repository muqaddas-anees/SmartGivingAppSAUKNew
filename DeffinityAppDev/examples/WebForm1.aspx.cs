using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace test4._5.examples
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hid.Value = sessionKeys.PortfolioID.ToString();

            if(!IsPostBack)
            {
                
                if (sessionKeys.PortfolioID>0)
                {
                  
                    var orgEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();
                    if (orgEntity != null)
                    {
                        bntPreview.NavigateUrl = "~/OrgHomeNewV2.aspx?orgguid=" + orgEntity.OrgarnizationGUID+"&dt="+DateTime.Now.Ticks;

                        //if( System.IO.File.Exists(Server.MapPath("~/WF/UploadData/OrgTemplate/org_" + sessionKeys.PortfolioID + ".html")))
                        //// hpath.Value = string.Format("../../WF/UploadData/OrgTemplate/org_{0}.html?dt={1}", sessionKeys.PortfolioID, DateTime.Now.Ticks);
                        //else
                        //     hpath.Value = string.Format("../../WF/UploadData/OrgTemplate/org_{0}.html?dt={1}", 0, DateTime.Now.Ticks);

                        hpath.Value = "ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s=" + ImageManager.file_section_landing;



                    }
                }
            }

        }

        protected void ChangeImageButton_Click(object sender, EventArgs e)
        {
            // This is intentionally left empty or you can trigger the file upload here if not using the JavaScript approach
        }

        ////protected void ImageUploader_Changed(object sender, EventArgs e)
        ////{
        ////    if (ImageUploader.HasFile)
        ////    {
        ////        // Save the uploaded file to a server directory
        ////        string filePath = Server.MapPath("~/UploadedImages/") + ImageUploader.FileName;
        ////        ImageUploader.SaveAs(filePath);

        ////        // Update the ImageUrl of the LogoImage control to display the uploaded image
        ////        LogoImage.ImageUrl = "~/UploadedImages/" + ImageUploader.FileName;
        ////    }
        ////}
    }
}