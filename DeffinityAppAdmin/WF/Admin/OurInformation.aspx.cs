using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.Entity;
using UserMgt.BAL;

namespace DeffinityAppDev.WF.Admin
{
    public partial class OurInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    var u = UserMgt.BAL.CompanyBAL.CompanyBAL_Select();
                    if (u != null)
                    {
                        txtAddress.Text = u.Address;
                        txtCity.Text = u.City;
                        txtTaxReference.Text = u.TaxReference;
                        txtTown.Text = u.Town;
                        txtZipcode.Text = u.Zipcode;
                        txtHost.Text = u.Payment_host;
                        txtVendor.Text = u.Payment_vendor;
                        txtUsername.Text = u.Payment_username;
                        txtPassword.Text = u.Payment_password;
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var u = UserMgt.BAL.CompanyBAL.CompanyBAL_Select();
                if (u != null)
                {
                    u.Zipcode = txtZipcode.Text.Trim();
                    u.Town = txtTown.Text.Trim();
                    u.TaxReference = txtTaxReference.Text.Trim();
                    u.City = txtCity.Text.Trim();
                    u.Address = txtAddress.Text.Trim();
                    u.Payment_password = txtPassword.Text.Trim();
                    u.Payment_username = txtUsername.Text.Trim();
                    u.Payment_vendor = txtVendor.Text.Trim();
                    u.Payment_host = txtHost.Text.Trim();
                    UserMgt.BAL.CompanyBAL.CompanyBAL_Update(u);
                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}