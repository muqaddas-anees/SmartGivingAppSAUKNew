using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BAL;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class VATCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtVatPercent.Text = VATByCustomerBAL.VATByCustomer_select().ToString();
            }
        }

        protected void imgbtnupdateemail_Click(object sender, EventArgs e)
        {
            try
            {
                VATByCustomerBAL.VATByCustomer_Update(Convert.ToInt32(!string.IsNullOrEmpty(txtVatPercent.Text.Trim()) ? txtVatPercent.Text.Trim() : "0"));
                lblsuccessemail.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}