using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;

public partial class controls_SADropDown : System.Web.DynamicData.FieldTemplateUserControl {

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["tab"] != null)
                {
                    string ddlname = Request.QueryString["tab"].ToString();
                    ddlAdmin.SelectedValue = ddlname.ToLower();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlAdmin_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmin.SelectedValue == "fls")
            {
                Response.Redirect("~/DC/FLSDefault.aspx?tab=fls");
            }
            else if (ddlAdmin.SelectedValue == "delivery")
            {
                Response.Redirect("~/DC/DeliveryDefaults.aspx?tab=delivery");
            }
            else if (ddlAdmin.SelectedValue == "accesscontrol")
            {
                Response.Redirect("~/DC/AccessControlDefaults.aspx?tab=accesscontrol");
            }
            else if (ddlAdmin.SelectedValue == "permittowork")
            {
                Response.Redirect("~/DC/PermitToWorkDefaults.aspx?tab=permittoWork");
            }
            else
            {
                Response.Redirect("~/DC/SecurityAccess.aspx?tab=securityaccess");
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
