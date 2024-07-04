using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using DeffinityManager.Marketplace.BLL;

public partial class controls_ProjectTabs : System.Web.UI.UserControl
{
    protected string[] Purl = { "ProjectOverview.aspx", "ProjectTasks.aspx", "ProjectResources.aspx", "ProjectBOM.aspx", 
                                  "ProjectTracker_General.aspx", "ProjectRisks.aspx", "ProjectPmApproval.aspx", "ProjectAsset.aspx", 
                                  "ProjectDocuments.aspx", "ProjectForummaster.aspx", "ProjectIssues.aspx", "IPD.aspx", 
                                  "UserPermissions.aspx", "ProjectMeetings.aspx", "ProjectLogicalAssets.aspx", "ProjectOverviewV4.aspx", 
                                  "adminmasterlists.aspx?type=prj", "ProjectDeviations.aspx","ProjectPlanReocurrence.aspx","ProjectCustomEmail.aspx","ProjectInventoryManager.aspx" };
                                                                    
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] str = PermissionManager.GetFeatures();
        if (!Page.IsPostBack)
        {
            //VisibilityChecking();
            //img0.Visible = Convert.ToBoolean(str[1]);
            //img1.Visible = Convert.ToBoolean(str[2]);
            //img2.Visible = Convert.ToBoolean(str[3]);
            //img3.Visible = Convert.ToBoolean(str[4]);
            //img4.Visible = Convert.ToBoolean(str[5]);
            //img5.Visible = Convert.ToBoolean(str[6]);
            //img6.Visible = Convert.ToBoolean(str[7]);
            ////img7.Visible = Convert.ToBoolean(str[8]);
            //if (Convert.ToBoolean(str[8]))
            //{
            //    img7.Visible = true;
            //}
            //else
            //{
            //    img7.Attributes["style"] = "display:none";
            //}
           // img8.Visible = Convert.ToBoolean(str[9]);
           // img9.Visible = Convert.ToBoolean(str[10]);
           //// img17.Visible = Convert.ToBoolean(str[17]);
           // img10.Visible = Convert.ToBoolean(str[11]);
           // img11.Visible = Convert.ToBoolean(str[12]);
           // img12.Visible = Convert.ToBoolean(str[13]);
           // img13.Visible = Convert.ToBoolean(str[14]);
           // img14.Visible = Convert.ToBoolean(str[15]);
        }
    }

    //public void VisibilityChecking()
    //{
    //    try
    //    {

    //        Market_BLL M_BLL = new Market_BLL();
    //        LinkRisks.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.ProjectRiskRegister);
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}

    //protected string GetCssClass(int i)
    //{
    //    string rtValue = string.Empty;
    //    string strTemp = "";
    //    string stemp = "";
    //    //int i = 0;
    //    if (i <= 20)
    //    {
    //        stemp = Purl[i];
    //        if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
    //        {
    //            rtValue = "current1";

    //        }
    //        if (i == 2)
    //        {
    //            strTemp = "Scopeofworks.aspx";
    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }
    //        }
    //        else if (i == 4)
    //        {
    //            strTemp = "ProjectDeviationReport.aspx";
    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }
    //        }
    //        else if (i == 3)
    //        {
    //            strTemp = "ProjectBOM.aspx";
    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }
    //            strTemp = "GoodsReceipt.aspx";
    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }
    //            strTemp = "ProjectBudgetbyTask.aspx";
    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }
    //            strTemp = "ProjectBenfitBudget.aspx";
    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }
    //            strTemp = "ProjectBOMSupplierRequisitions.aspx";
    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }
    //        }
    //        //else if (i == 3)
    //        //{
                
    //        //}
    //        //else if (i == 3)
    //        //{
               
    //        //}
    //        else if (i == 5)
    //        {
    //            strTemp = "ProjectRiskItems.aspx";

    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }
    //        }
    //        else if (i == 9)
    //        {
    //            strTemp = "ProjectForumMsg.aspx";

    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }

    //        }
    //        else if (i == 10)
    //        {
    //            strTemp = "ProjectIssues.aspx";

    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }

    //        }
    //        else if (i == 17)
    //        {
    //            strTemp = "ProjectDeviations.aspx";

    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }

    //        }
    //        else if (i == 13)
    //        {
    //            strTemp = "AddMeeting.aspx";

    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }
    //            strTemp = "ProjectMeetings.aspx";

    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }
    //        }
    //        else if (i == 18)
    //        {
    //            strTemp = "ProjectPlanReocurrence.aspx";

    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }
    //        }
    //        //
    //        else if (i == 19)
    //        {
    //            strTemp = "ProjectCustomEmail.aspx";

    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }
    //        }
    //        else if (i == 20)
    //        {
    //            strTemp = "ProjectInventoryManager.aspx";

    //            if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
    //            {
    //                rtValue = "current1";
    //            }
    //        }

    //        i++;
    //    }
    //    return rtValue;

    //}
    


    protected string getUrlQuery(string strUrl)
    {
        string url = "#";
        if (Request.QueryString["project"] != null)
        {
            url = "?project=" + Request.QueryString["project"].ToString();
        }
        else
        {
            url = "?project=" + "5";
        }
        url=strUrl + url;
        return url;
    }

    protected string getUrl(int i)
    {
        string url = "#";
        if (i <= 20)
        {
            if (Request.QueryString["project"] != null)
            {
                if(i == 16)
                  url = Purl[i] + "&project=" + Request.QueryString["project"].ToString();
                else
                 url = Purl[i] + "?project=" + Request.QueryString["project"].ToString();
            }
            if (Request.QueryString["project"] != null)
            {
                if (i == 17)
                    url = Purl[i] + "?project=" + Request.QueryString["project"].ToString();
                else
                    url = Purl[i] + "?project=" + Request.QueryString["project"].ToString();
            }
            if (Request.QueryString["project"] != null)
            {
                if (i == 19)
                    url = Purl[i] + "?project=" + Request.QueryString["project"].ToString() + "&Type=Project";
            }
            i++;
        }
       
        return url;
    }
}
