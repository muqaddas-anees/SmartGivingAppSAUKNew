using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //BindActiviteis();

                if (sessionKeys.Message.Length > 0)
                {
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                    sessionKeys.Message = string.Empty;
                }
            }
        }


        //private void BindActiviteis()
        //{
        //    try
        //    {
        //        var alist = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectAll().Where(o=>o.OrganizationID== sessionKeys.PortfolioID).ToList();

        //        ListActivites.DataSource = alist.OrderByDescending(o => o.StartDateTime).ToList();
        //        ListActivites.DataBind();
        //        lblCount.Text = alist.Count().ToString();
        //    }
        //    catch(Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //}


    }
}