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


public partial class controls_ProjectProposalTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected string[] Purl = { "ProjectPlan.aspx", "ProjectPlanningScope.aspx", "ProjectPlanningFunding.aspx", "ProjectPlanningBizReq.aspx", "ProjectPlanAct.aspx", "ProjectPlanApprove.aspx" };
    protected string getUrl(int i)
    {
        string url = "#";
        if (QueryStringValues.ProjectPlanID > 0)
        {
            url = Purl[i] + string.Format("?ProjectPlanID={0}", QueryStringValues.ProjectPlanID);
        }

        return url;
    }
    protected string GetCssClass(int i)
    {
        string CssClass = string.Empty;
        if (i < 6)
        {
            string stemp = Purl[i];
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
                CssClass = "current1";
            }
            else
            {
                CssClass = string.Empty;
            }
            i++;
        }
        return CssClass;
    }
}
