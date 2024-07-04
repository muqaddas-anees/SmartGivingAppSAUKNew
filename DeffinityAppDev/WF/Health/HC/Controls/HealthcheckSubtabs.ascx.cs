using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HC_controls_HealthcheckSubtabs : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((Request.Url.ToString().ToLower()).Contains("healthcheckform.aspx") == true)
        //{
        //    lbtnHealthcheckmain.BackColor = System.Drawing.Color.White;
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("healthcheckformissues.aspx") == true)
        //{
        //    lbtnHealthcheckIssues.BackColor = System.Drawing.Color.White;
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("healthcheckformdeviations.aspx") == true)
        //{
        //    //lbtnHealthcheckDeviations.BackColor = System.Drawing.Color.White;
            
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("healthcheckformdocs.aspx") == true)
        //{
        //    lbtnHealthcheckDocs.BackColor = System.Drawing.Color.White;
        //}
        //R=Y&HealthCheckId=22&PID=6
        lbtnHealthcheckmain.NavigateUrl = string.Format("~/WF/Health/HC/healthcheckform.aspx?R=Y&HealthCheckId={0}&PID={1}",
                                           QueryStringValues.HealthCheckId,Request.QueryString["PID"].ToString() );
        lbtnHealthcheckIssues.NavigateUrl = string.Format("~/WF/Health/healthcheckformissues.aspx?R=Y&HealthCheckId={0}&PID={1}",
                                           QueryStringValues.HealthCheckId, Request.QueryString["PID"].ToString());

       // lbtnHealthcheckDeviations.NavigateUrl = string.Format("~/healthcheckformdeviations.aspx?R=Y&HealthCheckId={0}&PID={1}",
                                          // QueryStringValues.HealthCheckId, Request.QueryString["PID"].ToString());

        lbtnHealthcheckDocs.NavigateUrl = string.Format("~/WF/Health/healthcheckformdocs.aspx?R=Y&HealthCheckId={0}&PID={1}",
                                           QueryStringValues.HealthCheckId, Request.QueryString["PID"].ToString());




    }
}
