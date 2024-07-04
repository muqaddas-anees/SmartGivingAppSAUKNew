using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;

public partial class controls_CheckpointCustomerTab : System.Web.UI.UserControl
{

    private string[] Purl = { "CustomerCheckpointOverview.aspx", "ProjectOverviewV2.aspx", "Checkpoint_Financials.aspx",
                                "Checkpoint_Assets.aspx", 
                                "Checkpoint_PMrisks.aspx", "Checkpoint_PMIssueList.aspx", 
                                "CheckpointCustomerFeedback.aspx", "Customer_CSI.aspx", "Customer_CheckpointRecommendation.aspx", "Checkpoint_TimeSheetApprove.aspx", 
                                "CheckpointChecklist.aspx", "CheckpointLogicalAsset.aspx", "CheckpointLogicalAssetdependency.aspx","Checkpoint_Docs.aspx" };
    protected string getUrl(int i)
    {
        string url = "#";
        if (i < Purl.Length)
        {
            if (QueryStringValues.Project > 0)
            {
                if (i == 7)
                {
                    url = Purl[i] + "?customer=0&checkpoint=0&Project=" + QueryStringValues.Project.ToString() + "&type=pm";
                }
                else
                {
                    url = Purl[i] + "?customer=0&checkpoint=0&Project=" + QueryStringValues.Project.ToString();
                }
            }
            i++;
        }
        return url;
    }
    protected string GetCssClass(int i)
    {
        string rtValue = string.Empty;
        string strTemp = "";
        string stemp = "";
        //int i = 0;
        if (i < Purl.Length)
        {
            stemp = Purl[i];
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
                rtValue = "current1";

            }
        }
        return rtValue;
    }
}
