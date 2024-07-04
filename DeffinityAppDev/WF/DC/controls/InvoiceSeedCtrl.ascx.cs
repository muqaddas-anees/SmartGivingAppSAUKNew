using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class InvoiceSeedCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    var l = InvoiceBAL.DefaultData_Select(sessionKeys.PortfolioID);
                    if (l != null)
                        txtInvoiceSeed.Text = (l.InvoiceSeed.HasValue ? l.InvoiceSeed.Value.ToString() : "");
                    else
                    {
                        txtInvoiceSeed.Text = string.Empty;
                    }
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
        }

        protected void btnaddUser_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty( txtInvoiceSeed.Text.Trim()))
            {
                InvoiceBAL.DefaultData_Update(new DefaultData() { CustomerID = sessionKeys.PortfolioID, InvoiceSeed = Convert.ToInt32(txtInvoiceSeed.Text.Trim()) });
                lblEmailDisList.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
        }
    }
}