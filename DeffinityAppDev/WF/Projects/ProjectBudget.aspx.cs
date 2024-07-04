using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.ServiceCatalogManager;
using System.Collections;
using Deffinity.ProgrammeManagers;

public partial class ProjectBudget :BasePage
{
    SqlConnection conn = new SqlConnection(Constants.DBString);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           // Master.PageHead = Resources.DeffinityRes.ProjectManagement;//"Project Management";
            if (!IsPostBack)
            {
               
           
               
               
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    


   
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {
            Response.Redirect(string.Format("~/WF/Projects/ProjectBudget.aspx?project={0}", QueryStringValues.Project));
        }
        else if (RadioButtonList1.SelectedValue == "2")
        {
            Response.Redirect(string.Format("~/WF/Projects/ProjectBOM.aspx?project={0}", QueryStringValues.Project));
        }
        else if (RadioButtonList1.SelectedValue == "3")
        {
            Response.Redirect(string.Format("~/WF/Projects/ProjectBenfitBudget.aspx?project={0}", QueryStringValues.Project));
        }
        else if (RadioButtonList1.SelectedValue == "5")
        {
            Response.Redirect(string.Format("~/WF/Projects/ProjectBudgetbyTask.aspx?project={0}", QueryStringValues.Project));
        }
        else
        {
            Response.Redirect(string.Format("~/WF/Projects/GoodsReceipt.aspx?project={0}", QueryStringValues.Project));
        }
    }
    //task title based on indent value
   



    
}
