using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class controls_ChangePassword : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Deffinity.Users.Login objLogin = new Deffinity.Users.Login();
        int uid = sessionKeys.UID;
        string oldpwd = txtOldPwd.Text.Trim();
        string newpwd = txtNewPwd.Text.Trim();
        object objOldPassword = objLogin.Old_Password(uid, oldpwd);

        if (objOldPassword != null)
        {

            object objRetVal = objLogin.LoginUser_ChangePassword(newpwd, uid);
            UpdateCustomerPasswordRecord(uid, newpwd);
            //lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "Password has been changed.";
        }
        else
        {
            //lblMsg.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Please enter valid old password";
        }
    }

    private static void UpdateCustomerPasswordRecord(int uid, string newpwd)
    {
        UserMgt.BAL.ContractorsBAL cb = new UserMgt.BAL.ContractorsBAL();
        var cEntity = cb.Contractor_Select_ByID(uid);
        if (cEntity != null)
        {
           //if user id customer then only update password
            if(cEntity.SID == 7)
            PortfolioMgt.BAL.PortfolioContactLoginDeatilsBAL.PortfolioContactLoginDeatils_AddUpdate(0, cEntity.ID, cEntity.LoginName, newpwd);
        }
    }
}
