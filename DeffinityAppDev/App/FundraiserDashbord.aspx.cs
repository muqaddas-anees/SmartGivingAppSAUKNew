using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class FaithGivingDashbord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            try
            {
                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
                var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o=>o.OrganizationID == sessionKeys.PortfolioID).ToList();
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

                if (tclist.Count > 0)
                {
                    GridDashboard.DataSource = rlist;
                    GridDashboard.DataBind();
                    if (rlist.Count > 0)
                    {
                        lblthisweek.Text = string.Format("{0:F2}", rlist.Sum(o => o.Amount.HasValue ? o.Amount.Value : 0));
                        lblthismonth.Text = string.Format("{0:F2}", rlist.Sum(o => o.Amount.HasValue ? o.Amount.Value : 0));
                    }

                    pnlNodata.Visible = false;
                    pnlFunriserlist.Visible = true;
                }
                else
                {
                    pnlNodata.Visible = true;
                    pnlFunriserlist.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }
    }
}