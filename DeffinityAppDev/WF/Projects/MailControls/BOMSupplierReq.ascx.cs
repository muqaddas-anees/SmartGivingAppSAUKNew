using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MailControls_BOMSupplierReqNew : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void BindControls1(string RecieverName, string Projectreference, string ManagerName, string Email, string Contactno,string instance
        ,string ownerEmail,string telephone,string ownerName)
    {
        try
        {
            lblrecievername.Text = RecieverName;
            lblprojectref.Text = Projectreference;
            //lblmanager.Text = ManagerName;
            //lblemail.Text = Email;
            lblInstanceName.Text = instance;
            lblOwnerEmail.Text = ownerEmail;
            lblOwnerName.Text = ownerName;
            lblTelephone.Text = telephone;
            //if (Email != "")
            //{
            //    lblmobile.Text = " or call on " + Contactno;
            //}
            //else
            //{
            //    lblmobile.Text = string.Empty;
            //}
            if (Contactno != "")
            {
                //lblmobile.Text = " or call on " + Contactno;
            }
            else
            {
                //lblmobile.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Quote Mail");
        }

        imgLogo.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];


    }
}
