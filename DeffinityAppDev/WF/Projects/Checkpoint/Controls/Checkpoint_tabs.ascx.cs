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


public partial class controls_Checkpoint_tabs : System.Web.UI.UserControl
{
    private string[] Purl = { "Checkpoint_Overview.aspx", "ProjectOverviewV4.aspx", "Checkpoint_Financials.aspx",
                                "Checkpoint_Assets.aspx", 
                                "Checkpoint_PMrisks.aspx", "Checkpoint_PMIssueList.aspx", 
                                "Checkpoint_Feedback.aspx", "QACIPS.aspx", "Checkpoint_Recommendations.aspx", "Checkpoint_TimeSheetApprove.aspx"
                                , "Checkpoint_Assets.aspx", "CheckpointLogicalAsset.aspx", "CheckpointLogicalAssetdependency.aspx","Checkpoint_Docs.aspx", 
                                "CheckpointChecklist.aspx" ,"ProjectCheckpointForm.aspx"};
    protected string getUrl(int i)
    {
        string url = "#";
        if (i < Purl.Length)
        {
            if (QueryStringValues.Project >0)
            {
                if (i == 7)
                {
                    url = Purl[i] + "?checkpoint=0&Project=" + QueryStringValues.Project.ToString() + "&type=pm"; 
                }
                else
                {
                    url = Purl[i] + "?checkpoint=0&Project=" + QueryStringValues.Project.ToString();
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
