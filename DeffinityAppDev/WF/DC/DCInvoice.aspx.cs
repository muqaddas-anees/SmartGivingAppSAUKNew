using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (QueryStringValues.CallID > 0)
                    lblTitle.InnerText = "Invoice - Ticket Reference " + QueryStringValues.CallID.ToString();
                else
                    lblTitle.InnerText = "Invoice";
                if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
                {
                    link_return.HRef = "FLSResourceList.aspx?type=FLS";
                }
                else
                {
                    link_return.HRef = "FLSJlist.aspx?type=FLS";
                    ////}
                }
                IDCRespository<CallInvoice> inRepository = new DCRepository<CallInvoice>();
                var inDetails = inRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
                var aRepository = new DCRepository<FixedPriceApproval>();
                var fentity = aRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
                InvoiceCtrl.Visible = false;
                if (fentity !=null)
                {
                    if(fentity.ApprovedBy.HasValue)
                        InvoiceCtrl.Visible = true;
                    else
                        InvoiceCtrl.Visible = false;

                }
                else
                {
                    InvoiceCtrl.Visible = true;
                }
                //if(Session["invoicedisable"] != null)
                //{
                //    if(Convert.ToBoolean(Session["invoicedisable"]) == true )
                //    {
                //        InvoiceCtrl.Visible = false;
                //    }
                //    else
                //    {
                //        InvoiceCtrl.Visible = true;
                //    }
                //}
                //else
                //{

                //    InvoiceCtrl.Visible = true;
                //}
            }
        }
    }
}