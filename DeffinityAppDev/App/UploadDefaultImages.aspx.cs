using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static QRCoder.SvgQRCode;

namespace DeffinityAppDev.App
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (filelogo.PostedFile.FileName.Length > 0)
                {
                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(filelogo.PostedFile.InputStream);
                    ImageManager.SaveProtfolioImage_setpath(filelogo.FileBytes, "0");
                    // DisplayLogo();
                }
                if (fileUser.PostedFile.FileName.Length > 0)
                {
                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(fileUser.PostedFile.InputStream);
                    ImageManager.SaveUserImage_setpath(fileUser.FileBytes, "0", Deffinity.systemdefaults.GetUsersFolderPath());
                    // DisplayLogo();
                }

                if (fileimage.PostedFile.FileName.Length > 0)
                {
                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(fileimage.PostedFile.InputStream);
                    ImageManager.SaveEventImage_setpath(fileimage.FileBytes, "0", "", "0");
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