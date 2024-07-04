using ProjectMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class controls_BudgetSavingGrid : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    public void BindGrid()
    {
        try {
            using (projectTaskDataContext pdc = new projectTaskDataContext())
            {
                var Blist = pdc.ProjectBOMs.ToList();
                var Slist = pdc.GoodsReceiptSavings.Where(a => a.S_type == "Qty").ToList();
                
                if (QueryStringValues.Project != 0)
                {
                    Slist = Slist.Where(o => o.projectRef == QueryStringValues.Project).ToList();
                }
                var x = (from a in Slist
                         join b in Blist on a.BOMId equals b.ID
                         select new
                         {
                             BOMId = a.BOMId,
                             BOMIdName = b.Description,
                             Budget = a.BudgetQty,
                             Actual = a.ActualQty,
                             Saving = a.BudgetQty - a.ActualQty,
                             CostSaving = (a.BudgetQty - a.ActualQty) * (b.Labour.Value + b.Mics.Value + b.Material.Value)
                         }).ToList();
                GridSavingRecord.DataSource = x;
                GridSavingRecord.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}