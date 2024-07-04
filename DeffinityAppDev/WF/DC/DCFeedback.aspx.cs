using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCFeedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (QueryStringValues.CallID > 0)
                lblTitle.InnerText = "Feedback - Ticket Reference " + QueryStringValues.CallID.ToString();
            else
                lblTitle.InnerText = "Feedback";
            if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
            {
                link_return.HRef = "FLSResourceList.aspx?type=FLS";
            }
            else
            {
                link_return.HRef = "FLSJlist.aspx?type=FLS";
                ////}
            }
            if(!IsPostBack)
            {
                try
                {
                    BindGridview();
                }
                catch(Exception ex)
                { LogExceptions.WriteExceptionLog(ex); }
            }
        }

        public void BindGridview()
        {
            IDCRespository<CallCustomerSurvey> cusrep = new DCRepository<CallCustomerSurvey>();
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> reqRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();

            var flist = cusrep.GetAll().Where(o => o.CallID == QueryStringValues.CallID).ToList();
            if(flist != null)
            {
                var rqlist = reqRep.GetAll().Where(o=>flist.Select(p=>p.CustId).ToArray().Contains(o.ID)).ToList();
                var result = (from p in flist
                              select new
                              {
                                  CallRef = "" + p.CallID,
                                  p.CustId,
                                  CustomerName = rqlist.Where(o=>o.ID == p.CustId).FirstOrDefault().Name,
                                  CreatedDate=p.CreatedDate.HasValue?p.CreatedDate.Value.ToShortDateString() +" " + p.CreatedDate.Value.ToShortTimeString():string.Empty,
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