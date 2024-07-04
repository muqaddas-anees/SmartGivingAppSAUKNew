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


public partial class controls_MyProjectsTab : System.Web.UI.UserControl
{
    private string[] Purl = { "MyTasks.aspx", "FLSResourceList.aspx?type=FLS", "HealthCheckSchedule.aspx?R=Y", "MyQAIssues.aspx", "MyRisks.aspx", "MyProjects.aspx?Status=2", "MyProjects.aspx?Status=3", "CCApproval.aspx", "TimeSheetResourcesDaily.aspx", "VT.ResourceVacationRequest.aspx", "MyTasksDocuments.aspx?mode=central", "ResourceNewChitChat.aspx", "ResourceInventory.aspx", "MyProjectDocs.aspx" };
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] str = PermissionManager.GetFeatures();
        if (!Page.IsPostBack)
        {
            img29.Visible = true;// Convert.ToBoolean(str[29]);
            //img30.Visible = Convert.ToBoolean(str[30]);
            //img31.Visible = Convert.ToBoolean(str[31]);
            img32.Visible = Convert.ToBoolean(str[32]);
            img33.Visible = Convert.ToBoolean(str[33]);
            img34.Visible = Convert.ToBoolean(str[34]);
            img35.Visible = Convert.ToBoolean(str[35]);
            //img36.Visible = Convert.ToBoolean(str[36]);
            img37.Visible = Convert.ToBoolean(str[37]);
            img38.Visible = true;
            img39.Visible = true;
            //img29.Attributes["Class"] = GetCssClass(0);
            //img30.Attributes["Class"] = GetCssClass(1);
            //img31.Attributes["Class"] = GetCssClass(2);
            //img32.Attributes["Class"] = GetCssClass(3);
            //img33.Attributes["Class"] = GetCssClass(4);
            //img34.Attributes["Class"] = GetCssClass(5);
            //img35.Attributes["Class"] = GetCssClass(6);
            //img36.Attributes["Class"] = GetCssClass(7);
            //img37.Attributes["Class"] = GetCssClass(8);
            //img38.Attributes["Class"] = GetCssClass(9);
            //img39.Attributes["Class"] = GetCssClass(10);
            //img40.Attributes["Class"] = GetCssClass(11);
            //img42.Attributes["Class"] = GetCssClass(12);
            //img43.Attributes["Class"] = GetCssClass(14);
          
            //if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
            //{
            //    img39.Visible = true;
            //}
            //else
            //{
            //    img39.Visible = false;
            //}
        }
    }
    protected string GetCssClass(int i)
    {
        string rtValue = string.Empty;
        //i = 0;

        if (i < Purl.Length)
        {
            string stemp = Purl[i];
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
                if (i == 1 || i == 7 || i==8)
                {
                    rtValue = "current2"; 
                }
                else if(i==2)
                {
                    rtValue = "current4"; 
                }
                //else if (i == 10)
                //{
                //    rtValue = "current4";
                //}
                    
                else
                {
                    rtValue = "current1";
                }
                
            }

        }
        
        return rtValue;
    }

    protected string GetTabLayOut()
    {
        string rtValue =  "tabs_list1";
        
        for (int i = 0; i < Purl.Length; i++)
        {
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
                if (i == 1 || i == 7 || i==8)
                {
                    rtValue = "tabs_list2";
                }
                else if (i == 2)
                {
                    rtValue = "tabs_list4";
                }
                else
                {
                    rtValue = "tabs_list1";
                }
               
            }

        }
        return rtValue;
    }
}
