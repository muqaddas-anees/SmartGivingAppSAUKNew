using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF
{
    public partial class DefaultNew : System.Web.UI.Page
    {
        public const string users = "users";
        IPortfolioRepository<ProjectPortfolio> pRepository = null;
        private string error = "";
        //"~/WF/Projects/ProjectPipeline.aspx?Status=2"
        //~/WF/DC/FLSJlist.aspx?type=FLS
        string[] Purl = { "~/App/Home.aspx", "~/App/Home.aspx" };
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                Page.Title = "Welcome to " + Deffinity.systemdefaults.GetInstanceTitle();
                lblcopyrighttext.InnerText = Deffinity.systemdefaults.GetCopyrightText();


                if (!IsPostBack)
                {
                    Session[users] = null;
                    //
                    //get first company details
                    //HttpContext.Current.Application["instance"] = "HWA";
                    //clear application data
                    //HttpContext.Current.Application["FromEmail"] = null;
                    //Redirect to page


                    lblyear.InnerText = System.DateTime.Now.Year.ToString();
                    txtName.Focus();
                    lblError.Visible = false;
                }
                txtName.Attributes.Add("placeholder", "Username");
                txtPwd.Attributes.Add("placeholder", "Password");
                //cbox.Attributes.Add("onClick", "javascript:fnShow();");
                //btnCancel.Attributes.Add("onClick", "javascript:fnUncheck();");
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void CheckLogin(string userName, string Password, string setUrl = "")
        {

            lblError.Visible = false;
            bool success = true;
            try
            {

                bool retval = DeffinityManager.Portfolio.BAL.PortfolioAdminLoginBAL.PortfolioAdminLoginBAL_CheckUserExists(userName, Password); //Deffinity.Users.Login.LoginUser(userName, Password);
                if (retval)
                {
                    FormsAuthentication.RedirectFromLoginPage(sessionKeys.UName, false);
                    Response.Redirect(Purl[0].ToString(), false);
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

        //private void SetDefaultCustomer()
        //{
        //    pRepository = new PortfolioRepository<ProjectPortfolio>();
        //    if (sessionKeys.PortfolioID == null)
        //    {
        //        var pdetails = pRepository.GetAll().FirstOrDefault();
        //        if (pdetails != null)
        //        {

        //            sessionKeys.PortfolioID = pdetails.ID;
        //            sessionKeys.PortfolioName = pdetails.PortFolio;
        //        }
        //    }
        //}
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            //set customer default data

            CheckLogin(txtName.Text.Trim(), txtPwd.Text.Trim());
        }
        private void showModelPopUp()
        {
            //ModalPopupExtender1.Show();
        }

        protected void btnCtn_Click(object sender, EventArgs e)
        {
            Deffinity.Users.Login.IsFirstLogged_update(sessionKeys.UID, 1);
            if (ViewState["pwd"] != null)
                CheckLogin(txtName.Text.Trim(), ViewState["pwd"].ToString());
        }

        protected void btnDisable_Click(object sender, EventArgs e)
        {
            //Deffinity.Users.Login.IsFirstLogged_update(sessionKeys.UID, 0);
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
}