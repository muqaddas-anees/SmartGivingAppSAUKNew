//using DeffinityManager.Marketplace.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_ResourcePlannerTabs : System.Web.UI.UserControl
{
    private string[] Purl = { "ResourcePlanner.aspx", "Timesheet.aspx", "Vt.RequestVacation.aspx" };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // VisibilityChecking();
        }
    }

    //public void VisibilityChecking()
    //{
    //    try
    //    {
    //        Market_BLL M_BLL = new Market_BLL();
    //        LinkVacationTracker.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.VacationTracker);
    //        LinkResourcePlanner.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.ResourcePlanner);
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}


    protected string GetCssClass(int i)
    {
        string rtValue = string.Empty;
        //i = 0;
        if (i < Purl.Length)
        {
            string stemp = Purl[i];
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
                rtValue = "current1";
               
               
            }
            else
            {
                rtValue = string.Empty;
            }
        }
        return rtValue;
    }
    protected string GetTabLayOut()
    {
        string _GetTabLayOut = string.Empty;
        for (int i = 0; i < Purl.Length; i++)
        {
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
               
                    _GetTabLayOut = "tabs_list1";
               
              
            }

        }
        return _GetTabLayOut;
    }
   
}
