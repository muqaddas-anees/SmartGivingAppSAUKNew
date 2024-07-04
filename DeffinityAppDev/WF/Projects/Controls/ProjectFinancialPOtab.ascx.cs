using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_ProjectFinancialPOtab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((Request.Url.ToString().ToLower()).Contains("projectfinancial_customerpo.aspx") == true)
        //{
           
        //    lbtnCustomerPO.BackColor = System.Drawing.Color.White;
           
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("project_customerpodetails.aspx") == true)
        //{

        //    lbtnCustomerPO.BackColor = System.Drawing.Color.White;

        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("projectfinancial_internalpo.aspx") == true)
        //{
        //    lbtnInternalPO.BackColor = System.Drawing.Color.White;
           
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("project_internalpodetails.aspx") == true)
        //{
        //    lbtnInternalPO.BackColor = System.Drawing.Color.White;

        //}
        if (Request.QueryString["project"] != null)
        {
            lbtnCustomerPO.NavigateUrl = "~/WF/Projects/ProjectFinancial_CustomerPO.aspx?project=" + Request.QueryString["project"].ToString();
            lbtnInternalPO.NavigateUrl = "~/WF/Projects/ProjectFinancial_InternalPO.aspx?project=" + Request.QueryString["project"].ToString();
        }

        
    }
}