using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthCheckMgt.Entity;
using HealthCheckMgt.BAL;
using HealthCheckMgt.DAL;
using DC.DAL;
using DC.Entity;
using DC.BAL;
using DC.BLL;

namespace DeffinityAppDev.WF.Health.HC
{
    public partial class FormDesignNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["fid"] != null)
                    {
                        var c = CustomFormDesignerBAL.GetFieldByFormID(Convert.ToInt32(Request.QueryString["fid"].ToString())).FirstOrDefault();
                        if (c != null)
                        {
                            hform.Value = Request.QueryString["fid"].ToString();
                            hset.Value = c.PartnerServiceID.Value.ToString();
                        }
                    }
                    else
                    {
                        hform.Value = "0";
                        hset.Value = "0";
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