using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using UserMgt.Entity;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.PageHead = "Admin";
            using (UserDataContext ud = new UserDataContext())
            {
                PasswordExpiryConfig pc = ud.PasswordExpiryConfigs.Select(r => r).FirstOrDefault();
                if (pc != null)
                {
                    txtExpiryDate.Text = pc.ExpiryDate.ToString().Remove(10);
                    ddlExpiryType.SelectedValue = pc.Expirytype;
                }

            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        try
        {
            using (UserDataContext ud = new UserDataContext())
            {
                PasswordExpiryConfig pc = ud.PasswordExpiryConfigs.Select(r => r).FirstOrDefault();
                if (Convert.ToDateTime(txtExpiryDate.Text).Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString())).Days > 0)
                {
                    if (pc != null)
                    {
                        // Update take place
                        pc.Expirytype = ddlExpiryType.SelectedItem.Text;
                        pc.ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text);
                        ud.SubmitChanges();
                        lblMsg.Text = "Updated Successfully.";
                    }
                    else
                    {
                        //insert take place

                        PasswordExpiryConfig pec = new PasswordExpiryConfig();
                        pec.Expirytype = ddlExpiryType.SelectedItem.Text;
                        pec.ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text);
                        ud.PasswordExpiryConfigs.InsertOnSubmit(pec);
                        ud.SubmitChanges();
                        lblMsg.Text = "Updated Successfully.";

                    }
                }
                else {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Expiry date should be greater than current date.";
                    
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
}