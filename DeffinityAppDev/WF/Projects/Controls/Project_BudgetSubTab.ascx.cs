using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;

public partial class controls_Project_BudgetSubTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["project"] != null)
        {
            string pref = Request.QueryString["project"].ToString();
            lbtnGeneral.NavigateUrl = "~/WF/Projects/Budget_General.aspx?project=" + pref;
            lbtnBillofMaterials.NavigateUrl = "~/WF/Projects/ProjectBOM.aspx?project=" + pref;
            lbtnHours.NavigateUrl = "~/WF/Projects/Budget_Hours.aspx?project=" + pref;
            lbtnExpenses.NavigateUrl = "~/WF/Projects/Budget_Expenses.aspx?project=" + pref;
            //lbtnExternalCosts.NavigateUrl = "~/WF/Projects/Budget_ExternalCosts.aspx?project=" + pref;
            lbtnPMBudgetDifference.NavigateUrl = "~/WF/Projects/Budget_PMBudgetDifference.aspx?project=" + pref;
            lbtnBudgetbyTask.NavigateUrl = "~/WF/Projects/ProjectBudgetbyTask.aspx?project=" + pref;
            lbtnBenefitBudget.NavigateUrl = "~/WF/Projects/ProjectBenfitBudget.aspx?project=" + pref;
            SetTabColor();
            ProjectStatusChecking(pref);
        }
    }
    public void ProjectStatusChecking(string Pid)
    {
        try
        {
            using (projectTaskDataContext pdc = new projectTaskDataContext())
            {
                int s =(int) pdc.Projects.Where(a => a.ProjectReference == int.Parse(Pid)).Select(a => a.ProjectStatusID).FirstOrDefault();
                if (s == 1)
                {
                    lbtnPMBudgetDifference.Visible = false;
                    li_lbtnPMBudgetDifference.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void SetTabColor()
    {
        if ((Request.Url.ToString().ToLower()).Contains("budget_general.aspx") == true)
        {
            lbtnGeneral.BackColor = System.Drawing.Color.White;
        }
        if ((Request.Url.ToString().ToLower()).Contains("budget_general.aspx") == true)
        {
            lbtnGeneral.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("projectbom.aspx") == true)
        {
            lbtnBillofMaterials.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("budget_hours.aspx") == true)
        {
            lbtnHours.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("budget_expenses.aspx") == true)
        {
            lbtnExpenses.BackColor = System.Drawing.Color.White;
        }
        //else if ((Request.Url.ToString().ToLower()).Contains("budget_externalcosts.aspx") == true)
        //{
        //    lbtnExternalCosts.BackColor = System.Drawing.Color.White;
        //}
        else if ((Request.Url.ToString().ToLower()).Contains("budget_pmbudgetdifference.aspx") == true)
        {
            lbtnPMBudgetDifference.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("projectbudgetbytask.aspx") == true)
        {
        //    lbtnBudgetbyTask.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("projectbenfitbudget.aspx") == true)
        {
          //  lbtnBenefitBudget.BackColor = System.Drawing.Color.White;
        }
    }
   
}