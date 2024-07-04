using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;
using HealthCheckMgt.BAL;

namespace DeffinityAppDev.WF.DC
{
    public partial class FormPreviewNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (!IsPostBack)
                {

                    if (Request.QueryString["fid"] != null)
                    {
                        hSubCategory.Value = Request.QueryString["fid"].ToString();
                        hform.Value = Request.QueryString["fid"].ToString();
                    }

                    //        if (Request.QueryString["callid"] != null)
                    //    {
                    //        sessionKeys.IncidentID = Convert.ToInt32(Request.QueryString["callid"]);
                    //    }

                    //    var hb = new HealthCheckBAL();
                    //    var reval = hb.HealthCheck_FormAssignToCall_SelectByCallID(QueryStringValues.CallID);
                    //    if (reval != null)
                    //    {
                    //        hSubCategory.Value = (reval.FormID.HasValue ? reval.FormID.Value : 0).ToString();
                    //        hform.Value = (reval.FormID.HasValue ? reval.FormID.Value : 0).ToString();
                    //    }

                    //    //update 
                    //    var resultval = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(Convert.ToInt32(QueryStringValues.CallID));
                    //    if (resultval.Count == 0)
                    //    {
                    //        var h = CustomFormDesignerBAL.findfirstRecord(sessionKeys.PortfolioID);
                    //        hset.Value = h.PartnerSubcategoryID.Value.ToString();
                    //        hSubCategory.Value = h.PartnerSubcategoryID.Value.ToString();

                    //        var result = CustomFormDesignerBAL.GetFieldByFormID(Convert.ToInt32(hset.Value));

                    //        var pnlids = result.Select(o => o.PartnerServiceID).ToList();
                    //        foreach (var p in pnlids)
                    //        {
                    //            //insert all control ids
                    //            foreach (var r in result.Where(o => o.PartnerServiceID == p).ToList().OrderBy(o => o.Position))
                    //            {
                    //                //Convert.ToInt32(hset.Value)
                    //                FLSAdditionalInfoBAL.InsertFLSAdditionalInfo(new FLSAdditionalInfo() { CallID = QueryStringValues.CallID, ServiceID = p, CustomFieldID = r.ID, CustomFieldValue = string.Empty, CustomFieldValueExt = string.Empty, LoggedDatetime = DateTime.Now, Hours = 0, Minutes = 0 });
                    //            }
                    //        }
                    //    }

                    //}
                    //if (Request.QueryString["fid"] != null)
                    //{
                    //    var c = CustomFormDesignerBAL.GetFieldByFormID(Convert.ToInt32(Request.QueryString["fid"].ToString())).FirstOrDefault();
                    //    if (c != null)
                    //    {
                    //        hform.Value = Request.QueryString["fid"].ToString();
                    //        hset.Value = c.PartnerServiceID.Value.ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    hform.Value = "0";
                    //    hset.Value = "0";
                    //}
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        
        }
    }
}