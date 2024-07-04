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


public partial class controls_ChangeControlTab : System.Web.UI.UserControl
{
    private string[] Purl = { 
                                "ChangeControl.aspx",
                                "CCScheduledTasks.aspx",
                                "CCRiskAssessment.aspx",
                                "CCAddApproval.aspx",
                                "CCServiceUnits.aspx"
                            };
    protected string GetCssClass(int i)
    {
        string rtValue = string.Empty;
        //i = 0;
        if (i < Purl.Length)
        {
            string stemp = Purl[i];
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
                rtValue = "current2";
            }
            else
            {
                rtValue = string.Empty;
            }
        }
        return rtValue;
    }
  
}
