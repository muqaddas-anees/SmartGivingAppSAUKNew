using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;

namespace DeffinityAppDev.WF.Admin.chat
{
    public partial class ChatNew : System.Web.UI.Page
    {
      //static  List<UserDetail> ConnectedUsers = new List<UserDetail>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["uname"] = sessionKeys.UName;
            Session["userid"] = sessionKeys.UID.ToString();
            Bindrepter();
        }
        private void Bindrepter()
        {
            using (UserDataContext Udc = new UserDataContext())
            {


                var Userslist = Udc.Contractors.ToList();
                var Result = (from a in Udc.Contractors
                              where  a.ID != sessionKeys.UID
                              select new
                              {
                                  Uid = 0,
                                  Username = a.ContractorName,
                                  Email = a.EmailAddress,
                                  SID = a.SID,

                                  Status = a.Status
                              }).ToList();


                GridView1.DataSource = Result;
                GridView1.DataBind();
            




            }


        }
    }
}