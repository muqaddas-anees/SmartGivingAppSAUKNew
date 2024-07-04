using DC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Health.HC
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
                        hset.Value = Request.QueryString["fid"].ToString(); //CustomFormDesignerBAL.findformtRecord(Convert.ToInt32(Request.QueryString["fid"].ToString())).ToString();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}