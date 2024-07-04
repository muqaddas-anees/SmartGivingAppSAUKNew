using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;


public partial class RootVariationApprove : System.Web.UI.Page
{
    RiseValuation RiasVal = new RiseValuation();
    Email mail = new Email();
    BuildProject bp = new BuildProject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
                
            {
                int pref = Convert.ToInt32(Request.QueryString["Project"].ToString());
                int id = Convert.ToInt32(Request.QueryString["ID"].ToString());
                bool approve = Convert.ToBoolean(Request.QueryString["Approve"].ToString());
                  
                //db connection
                bp.VarianceApprove(pref,id,approve);
                if (approve)
                {
                    mail.sendMail(pref, id, 4);
                }
                else
                {
                    mail.sendMail(pref, id, 5);
                }
               // RiasVal.TaskItemToInvoice(id,"new1", "0", "0", "0", false, pref);
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message, "variation approve in mail");
            }
        }
    }
}
