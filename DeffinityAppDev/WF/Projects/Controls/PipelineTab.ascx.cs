using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Deffinity.Bindings;


public partial class controls_PipelineTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //set error message
        if (Session["msg"] != null)
        {
            // if (Session["msg"] == "msg")
            //lblmsg.Text = "Sorry, only Administrators are allowed to archive projects";
        }



        if (!IsPostBack)
        {
            if (sessionKeys.SID == 2 || sessionKeys.SID == 3)
            {
                linkpm.Visible = false;
                linkpm_retention.Visible = false;
            }
            else
            {
                linkpm.Visible = true;
                linkpm_retention.Visible = true;
            }

            if (Request.QueryString["status"] != null)
            {
                //avoid if the status is archive or Retention
                if (Convert.ToInt32(Request.QueryString["status"]) == 5 || Convert.ToInt32(Request.QueryString["status"]) == 11)
                {
                    if (sessionKeys.SID != 1)
                    {
                        Session["msg"] = "msg";
                        Response.Redirect("ProjectPipeline.aspx?Status=0");
                    }

                }
                else
                {
                    Session["msg"] = string.Empty;
                }

            }
        }
        
    }
   
    protected string GetCssClass(int t)
    {
        string CssClass = string.Empty;

        if (t == int.Parse(Request.QueryString["status"].ToString()))
        {
            CssClass = "current1";
        }
        return CssClass;
    }
}
