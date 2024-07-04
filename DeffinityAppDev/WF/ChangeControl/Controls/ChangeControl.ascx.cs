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

public partial class controls_ChangeControl : System.Web.UI.UserControl
{

    #region Fields

    private string[] Pimg = { 
                                "images/tab_change_control_0.gif", 
                                "images/tab_tasks.gif",
                                "images/tab_risk.gif", 
                                "images/tab_approve.gif"
                            };
    private string[] PimgOn = { 
                                "images/tab_change_control_1.gif", 
                                "images/tab_tasks_01.gif",
                                "images/tab_risk_01.gif", 
                                "images/tab_approve_01.gif" 
                              };
    private string[] Purl = { 
                                "ChangeControl.aspx",
                                "CCScheduledTasks.aspx",
                                "CCRiskAssessment.aspx",
                                "CCAddApproval.aspx"
                            };

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Helper Methods

    protected string GetImage(int i)
    {
        string rtValue = "";
        //i = 0;
        if (i < Purl.Length)
        {
            string stemp = Purl[i];
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
                rtValue = PimgOn[i];
            }
            else
            {
                rtValue = Pimg[i];
            }
        }
        return rtValue;
    }

    #endregion
}
