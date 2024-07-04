using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class testmsg1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSUbmit_Click(object sender, EventArgs e)
        {
            //lblresult.Text = Deffinity.Users.Login.GeneratePasswordString(txttest.Text);

            IUserRepository<UserMgt.Entity.Contractor> uRepository = new UserRepository<UserMgt.Entity.Contractor>();
            var glist = uRepository.GetAll().Where(o => o.ID > 0).ToList();
            foreach (var c in glist)
            {
                c.Password = Deffinity.Users.Login.GenerateSHA1to256String(c.pkey);
                uRepository.Edit(c);
            }
            lblresult.Text = "Password Updateded";
        }
    }
}