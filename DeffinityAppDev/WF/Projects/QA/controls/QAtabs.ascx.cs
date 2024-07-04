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


public partial class controls_QAtabs : System.Web.UI.UserControl
{
    protected string[] Purl = { "QAProjectSummary.aspx", "QACheckList.aspx", "QAIssuesList.aspx", "QACIPS.aspx", "QADocs.aspx", "QALessonsLearnt.aspx", "QAApprove.aspx" };
   
    protected string getUrl(int i)
    {
        string url = "#";
        if (i < Purl.Length)
        {
            if (QueryStringValues.Project > 0)
            {
                if (i == 3)
                {
                    url = Purl[i] + "?Project=" + QueryStringValues.Project.ToString()+"&type=qa";
                }
                else
                {
                    url = Purl[i] + "?Project=" + QueryStringValues.Project.ToString();
                }
                
            }
            i++;
        }
        return url;
    }
}
