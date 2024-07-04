using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.Entity;

namespace DeffinityAppDev.WF.DC
{
    public partial class QuoteApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                IDCRespository<CallDetail> crep = new DCRepository<CallDetail>();
                var cDetails = crep.GetAll().Where(o => o.ID == QueryStringValues.CallID).FirstOrDefault();
                lblJob.Text = QueryStringValues.CallID.ToString();
                if (QueryStringValues.Type.ToLower() == "reject")
                {
                    pnlReject.Visible = true;
                    pnlmsg.Visible = false;
                    if (cDetails != null)
                    {
                        cDetails.StatusID = 49;
                        crep.Edit(cDetails);
                    }
                }
                else if (QueryStringValues.Type.ToLower() == "accept")
                {
                    pnlmsg.Visible = true;
                    pnlReject.Visible = false;
                    if (cDetails != null)
                    {
                        cDetails.StatusID = 50;
                        crep.Edit(cDetails);
                        try
                        {
                           // Deffinity.IncidentService.ServiceManager.Quotation_CopyToInvoice(QueryStringValues.CallID,);
                        }
                        catch(Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}