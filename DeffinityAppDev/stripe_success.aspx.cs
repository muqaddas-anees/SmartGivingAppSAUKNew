using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class stripe_success : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Request.QueryString["session_id"] !=null)
                {
                    LogExceptions.LogException("Success - session id" + Request.QueryString["session_id"]);
                }

                SuccessMessage.Text = "Thank you for your payment. Your transaction was successful!";
            }
        }
    }
}