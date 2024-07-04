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


public partial class controls_ResourceTabs : System.Web.UI.UserControl
{

    private string[] redirectUrl =
    {
        @"UpdateProject.aspx",
        @"UpdateProjectList.aspx",
        @"UpdateProjectScope.aspx",
        @"updateProjectVariations.aspx",
        @"UpdateProjectCIP.aspx",
        @"UpdateProjectAssets.aspx",
        @"UpdateProjectDocs.aspx",
        @"UpdateForumMaster.aspx"
       
        
    };
    protected string GetCssClass(int i)
    {
        string rtValue = string.Empty;
        string stemp = "";
        
        if (i < redirectUrl.Length)
        {
            stemp = redirectUrl[i];
            if ((Request.Url.ToString().ToLower()).Contains(redirectUrl[i].ToLower()) == true)
            {
                rtValue = "current1";
            }
            i++;
        }
        return rtValue;

    }
    protected string getUrl(int i)
    {
        string url = "#";
        if (i < redirectUrl.Length)
        {
            if (Request.QueryString["Project"] != null)
            {
                try
                {
                    url = string.Format(redirectUrl[i] + "?Project={0}&AC2PID={1}&ContractorID={2}", QueryStringValues.Project, QueryStringValues.AC2PID, QueryStringValues.ContractorID);
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
            i++;
        }
        return url;
    }
   
}
