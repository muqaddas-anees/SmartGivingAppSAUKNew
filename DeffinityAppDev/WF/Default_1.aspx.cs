using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class _Default_1: System.Web.UI.Page 
{    
    private string error = "";
    //"~/WF/Projects/ProjectPipeline.aspx?Status=2"
    string[] Purl = { "", "~/WF/Projects/ProjectHome.aspx", "~/WF/Projects/ProjectHome.aspx", "~/WF/Projects/ProjectHome.aspx", "~/WF/Resource/ResourceNewChitChat.aspx",
                        "~/WF/Projects/QA/QASummary.aspx", "", "~/WF/Portal/Home.aspx","~/WF/Vendors/RFIVendorSummary.aspx",
                        "~/WF/Resource/ResourceNewChitChat.aspx",""};
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Page.Title = "Welcome to " + Deffinity.systemdefaults.GetInstanceTitle();
        lblcopyrighttext.InnerText = Deffinity.systemdefaults.GetCopyrightText();
        //CheckLogin("nmohammed","nabeeha");
        if (!IsPostBack)
        {
            //HttpContext.Current.Application["instance"] = "deffinityapp";
            if (Request.Form["uid"] != null)
            {
                CheckLogin(Request.Form["uid"], Request.Form["pwd"]);
            }
            else if (sessionKeys.UID == 0 && Request.Url.AbsoluteUri.ToLower().Contains("xact.excelit.com"))
            {
                Response.Redirect("~/Index.aspx", false);
            }
            else
            {
                Clear_sessions();
            }
            lblyear.InnerText = System.DateTime.Now.Year.ToString();
            txtName.Focus();
            lblError.Visible = false;
        
        }
        txtName.Attributes.Add("placeholder", "Username");
        txtPwd.Attributes.Add("placeholder", "Password");
        cbox.Attributes.Add("onClick", "javascript:fnShow();");
        btnCancel.Attributes.Add("onClick", "javascript:fnUncheck();");
       
       
    }

    private void CheckLogin(string userName, string Password)
    {
        
        lblError.Visible = false;
        bool success = true;
        try
        {

            int retval = Deffinity.Users.Login.LoginUser_withDecript(userName, Password);
            if (retval > 0)
            {
                //get url from array
                int Navigate = 0;
                if (sessionKeys.SID == 99)
                {
                    Navigate = 0;
                }
                else
                {
                    //get the customer portfolio
                    if (sessionKeys.SID == 7)
                    {
                        if (sessionKeys.PortfolioID == 0)
                        {
                            lblError.Visible = true;
                            lblError.Text = "You have not been assigned to a portal. Please contact the system administrator who should be able to set you up.";
                            success = false;
                        }
                        else
                        {
                            Navigate = sessionKeys.SID;
                            //TO check cutomer PortalUser
                            sessionKeys.PortalUser = true;
                        }
                    }
                    else
                    {
                        Navigate = sessionKeys.SID;
                        //TO check cutomer PortalUser
                        sessionKeys.PortalUser = false;
                    }
                }

                if (success)
                {

                   
                        //Add the model pop up control
                        if (!Deffinity.Users.Login.IsFirstLogged(sessionKeys.UID))
                        {
                            showModelPopUp();
                            ViewState["pwd"] = Password;
                        }
                        else
                        {
                            FormsAuthentication.RedirectFromLoginPage(sessionKeys.UName, false);
                            Response.Redirect(Purl[Navigate].ToString(), false);
                        }

                }
            }
            else
            {
                txtPwd.Text = "";
                lblError.Visible = true;
                lblError.Text = "The username or password is incorrect. Please try again.";
            }

        }
        catch (Exception ex)
        {
            //write into log file
            LogExceptions.WriteExceptionLog(ex);
        }           
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
       CheckLogin(txtName.Text.Trim(), txtPwd.Text.Trim());
    }
    private void showModelPopUp()
    {
        ModalPopupExtender1.Show();
    }
   
    protected void btnCtn_Click(object sender, EventArgs e)
    {
        Deffinity.Users.Login.IsFirstLogged_update(sessionKeys.UID, 1);
        if(ViewState["pwd"] != null)
            CheckLogin(txtName.Text.Trim(), ViewState["pwd"].ToString());
    }

    protected void btnDisable_Click(object sender, EventArgs e)
    {
        Deffinity.Users.Login.IsFirstLogged_update(sessionKeys.UID, 0);
    }

    private void Clear_sessions()
    {
        Response.AddHeader("Expires", new DateTime(1940, 1, 1).ToString("R")); 
        Response.AddHeader("Last-Modified", DateTime.Now.ToString("R")); 
        Response.AddHeader("Cache-Control", "no-cache, must-revalidate");
        Response.AddHeader("Pragma", "no-cache");
       
        Session.Abandon();
        Session.Clear();
        Session.RemoveAll();
        Response.CacheControl = "no-cache";
        Response.Expires = -1;
    }
}
