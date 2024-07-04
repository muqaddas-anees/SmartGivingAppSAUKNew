using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Health.Entity;
using Health.DAL;
using System.Collections;
using Health.StateManager;

public partial class HealthCheckFormIssues : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Health Checks";
        ProjectPmNewIssues1.IssueSection = Deffinity.GlobalIssues.IssueSection.Healthcheck.ToString();
        
        if (!IsPostBack)
        {
            setDefaultValues();
        }
    }
    private void setDefaultValues()
    {
        if(QueryStringValues.HealthCheckId >0)
        LoadPageControls(GetHealthCheckSchedule(QueryStringValues.HealthCheckId));
    }
     private static HealthCheckList GetHealthCheckSchedule(int healthCheckID)
    {
        foreach (HealthCheckList healthCheckList in HealthCheckListHelper.LoadAllHealthCheckLists())
        {
            if (healthCheckID == healthCheckList.Id)
                return healthCheckList;
        }
        return null;
    }

    void LoadPageControls(HealthCheckList healthCheckList)
    {
       // Master.PageHead = healthCheckList.HealthCheckTitle;
        ProjectPmNewIssues1.Healthcheckname = healthCheckList.HealthCheckTitle +" - "+ DateTime.Now.ToShortDateString() +" - "+ DateTime.Now.ToShortTimeString();
    }
}
