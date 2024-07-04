using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.ServiceCatalogManager;
using ProjectMgt.Entity;
//using ProjectMgt.BAL;
using System.Data;
using ProjectMgt.DAL;

public partial class controls_ProjectCost : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindProject();
    }
    protected void imgSave_Click(object sender, EventArgs e)
    {
        try
        {

            ServiceCatalogManager.ProjectBudget_UpdateProject(QueryStringValues.Project, Convert.ToDouble(string.IsNullOrEmpty(txtProjectFee.Text) ? "0" : txtProjectFee.Text),
                Convert.ToDouble(string.IsNullOrEmpty(txtBudgetedCost.Text) ? "0" : txtBudgetedCost.Text));
            ////Project journal insert
            //Project project = ProjectJournalBAL.GetProjectsByReference(QueryStringValues.Project);
            //if (project != null)
            //    ProjectJournalBAL.InsertProjectJournal(project);

            BindProject();

        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    private void BindProject()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ServiceCatalogManager.ProjectBudget_SelectProject(QueryStringValues.Project);
            txtProjectFee.Text = string.Format("{0:F2}", dt.Rows[0]["BudgetaryCost"]);
            txtBudgetedCost.Text = string.Format("{0:F2}", dt.Rows[0]["BuyingPrice"]);
            txtBudGrossProfit.Text = string.Format("{0:F2}", dt.Rows[0]["BudgetGrossProfit"]);
            txtBudgetedGp.Text = string.Format("{0:F2}", dt.Rows[0]["BudgetGP"]);
            int statusId = Convert.ToInt32(dt.Rows[0]["ProjectStatusID"]);
            if (statusId != 1) // Pending
                txtProjectFee.Enabled = false;

            projectTaskDataContext BOM = new projectTaskDataContext();
            var listExist = (from r in BOM.ProjectBOMDetils
                             where r.ProjectReference == QueryStringValues.Project
                             select r).ToList();
            if (listExist != null)
            {
                if (listExist.Count > 0)
                {


                    var MaterialAll = (from r in BOM.ProjectBOMDetils
                                       where (r.ProjectReference == QueryStringValues.Project)
                                       select (r.Material * r.Qty)).Sum();

                    var LabourAll = (from r in BOM.ProjectBOMDetils
                                     where (r.ProjectReference == QueryStringValues.Project)
                                     select (r.Labour * r.Qty)).Sum();


                    var MicsAll = (from r in BOM.ProjectBOMDetils
                                   where (r.ProjectReference == QueryStringValues.Project)

                                   select (r.Mics * r.Qty)).Sum();

                    double total = Convert.ToDouble((MaterialAll + MicsAll + LabourAll));
                    if (total > Convert.ToDouble(txtBudgetedCost.Text))
                    {
                        imgBudget.Visible = true;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
}