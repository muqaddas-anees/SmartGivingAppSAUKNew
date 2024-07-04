using DC.BLL;
using DC.Entity;
using HealthCheckMgt.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCFormNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    var hb = new HealthCheckBAL();
                    var reval = hb.HealthCheck_FormAssignToCall_SelectByCallID(QueryStringValues.CallID);


                    //update 
                    var resultval = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(Convert.ToInt32(QueryStringValues.CallID));
                    if(resultval.Count ==0)
                    {
                        var h = CustomFormDesignerBAL.findfirstRecord(sessionKeys.PortfolioID);
                        hset.Value = h.PartnerSubcategoryID.Value.ToString();

                        var result = CustomFormDesignerBAL.GetFieldByFormID(Convert.ToInt32(hset.Value));

                        var pnlids = result.Select(o => o.PartnerServiceID).ToList();
                        foreach (var p in pnlids)
                        {
                            //insert all control ids
                            foreach (var r in result.Where(o=>o.PartnerServiceID == p).ToList().OrderBy(o=>o.Position))
                            {
                                //Convert.ToInt32(hset.Value)
                                FLSAdditionalInfoBAL.InsertFLSAdditionalInfo(new FLSAdditionalInfo() { CallID = QueryStringValues.CallID, ServiceID = p, CustomFieldID = r.ID, CustomFieldValue = string.Empty, CustomFieldValueExt = string.Empty, LoggedDatetime = DateTime.Now, Hours = 0, Minutes = 0 });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}