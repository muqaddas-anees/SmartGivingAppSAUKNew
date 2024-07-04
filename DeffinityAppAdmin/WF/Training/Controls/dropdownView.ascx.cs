using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingManager;
using Deffinity.TrainingEntity;

public partial class Training_dropdownView : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindStatus();
            if (Request.QueryString["selectview"] != null)
            {
                ddlView.SelectedValue = Request.QueryString["selectview"].ToString();
            }
        }
    }
    private void BindStatus()
    {
        ddlView.DataSource = Views.SelectAll(true);
        ddlView.DataValueField = "value";
        ddlView.DataTextField = "text";
        ddlView.DataBind();


    }
    protected void ddlView_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlView.SelectedValue == "1")
        {
            Response.Redirect("../Training/trDueSoon.aspx?selectview=1");
        }
        else if (ddlView.SelectedValue == "2")
        {
            Response.Redirect("../Training/trGapAnalysis.aspx?selectview=2");
        }
        else if (ddlView.SelectedValue == "3")
        {
            Response.Redirect("../Training/trCRDashboard.aspx?selectview=3"); //Cost analysis page
        }
        else if (ddlView.SelectedValue == "4")
        {
            Response.Redirect("../Training/trReportByIndividual.aspx?selectview=4");
        }
     
        else if (ddlView.SelectedValue == "6")
        {
            Response.Redirect("../Training/trCostForecast.aspx?selectview=6");
        }
        else if (ddlView.SelectedValue == "7")
        {
            Response.Redirect("../Training/trchartPenalties.aspx?selectview=7");
        }
        else if (ddlView.SelectedValue == "8")
        {
            Response.Redirect("../Training/trBudgetVsActual.aspx?selectview=8");
        }
        else
        {
            //Department/Target area 
            Response.Redirect("../Training/trDepartmentTarget.aspx?selectview=5");
        }
        
    }
}
