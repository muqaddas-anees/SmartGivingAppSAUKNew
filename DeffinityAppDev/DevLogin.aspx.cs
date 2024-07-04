using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class DevLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SetDefaultCustomer();
                CheckLogin("nmohammed", "nabeeha", "~/WF/CustomerAdmin/ContactDetails.aspx?ContactID=11");
               
            }
        }

        private void CheckLogin(string userName, string Password,string redirect)
        {

            //lblError.Visible = false;
            bool success = true;
            try
            {

                int retval = Deffinity.Users.Login.LoginUser(userName, Password);
                if (retval > 0)
                {
                  
                    if (success)
                    {
                        Response.Redirect(redirect, false);
                    }
                }
               

            }
            catch (Exception ex)
            {
                //write into log file
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void SetDefaultCustomer()
        {
            var pRepository = new PortfolioRepository<ProjectPortfolio>();
            var pdetails = pRepository.GetAll().FirstOrDefault();
            if (pdetails != null)
            {
                sessionKeys.PortfolioID = pdetails.ID;
                sessionKeys.PortfolioName = pdetails.PortFolio;
            }
        }
    }
}