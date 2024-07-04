using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.Feedback
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               

                if (Request.QueryString["cid"] != null && Request.QueryString["callid"] != null)
                    btnNext.HRef = "Feedbackentry.aspx?cid=" + Request.QueryString["cid"].ToString() + "&callid=" + Request.QueryString["callid"].ToString();
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
           
        }
    }
}