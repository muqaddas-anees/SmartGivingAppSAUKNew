using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCEquipment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hapid.Value = "0";
                if (!IsPostBack)
                {
                    if(!string.IsNullOrEmpty( sessionKeys.Message))
                    {
                        lblMsg.Text = sessionKeys.Message;
                        sessionKeys.Message = string.Empty;
                    }
                    if (Request.QueryString["callid"] != null && Request.QueryString["callid"] != "0")
                    {
                        int tid = int.Parse(Request.QueryString["callid"].ToString());
                        sessionKeys.IncidentID = tid;

                        var d = FLSDetailsBAL.Jqgridlist(tid);
                        haddressid.Value = d.FirstOrDefault().ContactAddressID.ToString();
                        hcid.Value = d.FirstOrDefault().RequesterID.ToString();

                        IDCRespository<CallIdAssociatedProduct> aRepository = new DCRepository<CallIdAssociatedProduct>();
                        var pData = aRepository.GetAll().Where(o => o.Callid == QueryStringValues.CallID).FirstOrDefault();
                        if (pData != null)
                        {
                            hapid.Value = pData.ProductIds;
                        }
                    }
                    else
                    {
                        sessionKeys.IncidentID = 0;
                    }
                    if (QueryStringValues.CCID > 0)
                        lblTitle.InnerText = "Registered Equipment" + " - Job Reference " + QueryStringValues.CCID;
                    else
                        lblTitle.InnerText = "Registered Equipment";
                    if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
                    {
                        link_return.HRef = "FLSResourceList.aspx?type=FLS";
                    }
                    else
                    {
                        link_return.HRef = "FLSJlist.aspx?type=FLS";
                        ////}
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IDCRespository<CallIdAssociatedProduct> aRepository = new DCRepository<CallIdAssociatedProduct>();
                var pData = aRepository.GetAll().Where(o => o.Callid == QueryStringValues.CallID).FirstOrDefault();
                if (pData == null)
                {
                    aRepository.Add(new CallIdAssociatedProduct() { Callid= QueryStringValues.CallID, ProductIds= hpid.Value });
                }
                else
                {
                    if(!string.IsNullOrEmpty( hpid.Value))
                    {
                        pData.ProductIds = hpid.Value;
                        aRepository.Edit(pData);

                    }
                }
                sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                Response.Redirect(Request.RawUrl);
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}