using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.controls
{
    public partial class FundProgressCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindData();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindData()
        {
            IPortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile>();

            IPortfolioRepository<PortfolioMgt.Entity.FundraisersProgress> pRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersProgress>();
            var pList = pRep.GetAll().Where(o => o.FundUNID == QueryStringValues.UNID).OrderBy(o => o.LoggedOn).ToList();
            var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
            if (pList.Count() > 0)
            {
                var rList = (from p in pList
                             orderby p.ID descending
                             select new
                             {
                                 p.Title,
                                 p.Notes,
                                 p.LoggedOn,
                                 p.UserID,
                                 UserName = (ulist.Where(o => o.ID == p.UserID).FirstOrDefault()?.CompanyName ?? ""),
                                 p.CallID,
                                 p.FundUNID,
                                 Files = fRep.GetAll().Where(o => o.ProgressId == p.ID).ToList()

                             }).ToList();

                list_progress.DataSource = rList;
                list_progress.DataBind();

                if(rList.Count >0)
                {
                    pnl_progress.Visible = true;
                }
                else
                {
                    pnl_progress.Visible = false;
                }
            }
        }

        public string showFile(string fileunid)
        {
            string retval = "";

            IPortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile>();

            var f = fRep.GetAll().Where(o => o.FileUNID == fileunid).FirstOrDefault();

            if (f != null)
            {
                var ext = Path.GetExtension(f.FileName);
                string[] exts = { ".jpg", ".jpeg", ".png", ".gif" };
                if (exts.Contains(ext.ToLower()))
                {
                    retval = string.Format("<div class='overlay-wrapper'><img src='../../wf/uploaddata/progress/{0}' class='rounded w-150px'/></div>", f.FileUNID + ext);

                }
                else
                {
                    retval = string.Format("<a href='filedownloader.ashx?unid={0}' class='fs-6 text-hover-primary fw-bold'> {1} </a>", f.FileUNID, f.FileName);
                }

            }



            return retval;
        }
        protected void list_progress_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }

        protected void list_progress_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }
    }
}