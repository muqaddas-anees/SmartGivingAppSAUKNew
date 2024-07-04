using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class TaithingDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
              //  Guid.NewGuid().
                BindGrid();
            }
        }
        protected void btnShowdefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App/TaithingDefault.aspx", false);

        }
        private void BindGrid()
        {
            try
            {
                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().ToList();
                var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o=>o.OrganizationID ==0).ToList();
                var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();

                Random generator = new Random();
                var rlist = (from t in tList
                             join c in ulist on t.LoggedByID equals c.ID
                             join tc in tclist on t.TithingID equals tc.ID
                             select new
                             {
                                 ID = t.ID,
                                 TithigName = tc.Title,
                                 PaidBy = c.ContractorName,
                                 Amount = t.PaidAmount,
                                 PaidDate = t.PaidDate,
                                 PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef

                             }).ToList();
                GridDashboard.DataSource = rlist;
                GridDashboard.DataBind();
                if (rlist.Count > 0)
                {
                    lblthisweek.Text = string.Format("{0:F2}", rlist.Sum(o => o.Amount.HasValue ? o.Amount.Value : 0));
                    lblthismonth.Text = string.Format("{0:F2}", rlist.Sum(o => o.Amount.HasValue ? o.Amount.Value : 0));
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }

        protected void btnSetTithingCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App/TithingCategorySettings.aspx?type=active", false);
        }
    }
}