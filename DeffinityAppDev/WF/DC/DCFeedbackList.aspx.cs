using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCFeedbackList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindGridview();
                }
                catch (Exception ex)
                { LogExceptions.WriteExceptionLog(ex); }
            }
        }

        public void BindGridview()
        {
            IDCRespository<CallCustomerSurvey> cusrep = new DCRepository<CallCustomerSurvey>();
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> reqRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();

            var flist = cusrep.GetAll().Where(o => o.CallID >0).ToList();
            var jlist = FLSDetailsBAL.Jqgridlist();
            if (flist != null)
            {
                var rqlist = reqRep.GetAll().Where(o => flist.Select(p => p.CustId).ToArray().Contains(o.ID)).ToList();
                var result = (from p in flist
                              join j in jlist on p.CallID equals j.CallID
                              select new
                              {
                                  CallRef = "" + j.CCID,
                                  p.CustId,
                                  CustomerName = j.RequesterName,
                                  CreatedDate = p.CreatedDate.HasValue ? p.CreatedDate.Value.ToShortDateString() + " " + p.CreatedDate.Value.ToShortTimeString() : string.Empty,
                                  p.Didwell,
                                  p.likelyService,
                                  p.Rating,
                                  p.recommend,
                                  p.servicefeedback,
                                  p.StatusName,
                                  p.Worksatisifaction
                              }).ToList();
                gridFeedback.DataSource = result;
                gridFeedback.DataBind();
            }

        }
    }
}