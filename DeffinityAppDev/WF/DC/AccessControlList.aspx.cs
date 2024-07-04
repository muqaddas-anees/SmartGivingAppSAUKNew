using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;
using DC.BAL;
using DC.DAL;
using System.Web.UI.HtmlControls;


public partial class DC_AccessControlList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAccessControlList();
        }
    }


    private void GetAccessControlList()
    {
        try
        {

            grdaccessctrl.DataSource = DeliveryInformationBAL.BindAccessControlList();
            grdaccessctrl.DataBind();
        }

        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void grdaccessctrl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdaccessctrl.PageIndex = e.NewPageIndex;
            grdaccessctrl.DataBind();
            GetAccessControlList();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdaccessctrl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    HiddenField h_callid = (HiddenField)e.Row.FindControl("h_callid");
        //    HtmlAnchor a_callid = (HtmlAnchor)e.Row.FindControl("a_callid");
        //    if (sessionKeys.IsCustomer != null)
        //    {
        //        a_callid.HRef = string.Format("CustomerAccessControl.aspx?callid={0}", h_callid.Value);
        //    }
        //    else
        //    {
        //        a_callid.HRef = string.Format("AccessControl.aspx?callid={0}", h_callid.Value);
        //    }
        //}
    }
}