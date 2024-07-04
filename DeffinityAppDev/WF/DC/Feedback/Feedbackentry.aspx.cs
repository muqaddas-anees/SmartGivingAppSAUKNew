using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.Feedback
{
    public partial class Feedbackentry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
                if(!IsPostBack)
                {
                    try
                    {
                        if (Request.QueryString["cid"] != null && Request.QueryString["callid"] != null)
                        {
                            IDCRespository<FLSDetail> cusrep = new DCRepository<FLSDetail>();
                            IUserRepository<UserMgt.Entity.Contractor> usrep = new UserRepository<UserMgt.Entity.Contractor>();
                            var fentity = cusrep.GetAll().Where(o => o.CallID == Convert.ToInt32(Request.QueryString["callid"])).FirstOrDefault();
                            if (fentity != null)
                            {
                                var uentity = usrep.GetAll().Where(u => u.ID == fentity.UserID).FirstOrDefault();
                                lblServiceProvider.Text = uentity.ContractorName;
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
            
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["cid"] != null && Request.QueryString["callid"] != null)
                {
                    IDCRespository<CallCustomerSurvey> cusrep = new DCRepository<CallCustomerSurvey>();
                    CallCustomerSurvey data = new CallCustomerSurvey();
                    var id = Convert.ToInt32(Request.QueryString["cid"]);
                    data.CustId = id;
                    data.CallID = Convert.ToInt32(Request.QueryString["callid"]);
                    //if (rr1.Checked) { data.likelyService = "Yes"; } else { data.likelyService = "No"; }
                    data.likelyService = hrate1.Value;
                    data.Worksatisifaction = hrate2.Value;
                    data.Didwell = txtImprovements.Text.Trim();
                    data.Rating  = hrate3.Value;
                    data.recommend = hrate4.Value;
                    data.servicefeedback = txtComments.Text;
                    data.CreatedDate = DateTime.Now;
                    data.StatusName = "Submitted";
                    //if (radio1.Checked) { data.Worksatisifaction = "Yes"; } else { data.Worksatisifaction = "No"; }
                    //if (rad1.Checked) { data.recommend = "Yes"; } else { data.recommend = "No"; }
                    cusrep.Add(data);
                    Response.Redirect("FeedbackEnd.aspx");
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}