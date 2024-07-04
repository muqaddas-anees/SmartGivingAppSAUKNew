using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;

public partial class DC_DeliveryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDeliveryList();
        }
    }
    #region Bind Delivery List
    private void BindDeliveryList()
    {
        try
        {
            gvList.DataSource = DeliveryInformationBAL.BindDeliveryList();
            gvList.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvList.PageIndex = e.NewPageIndex;
            BindDeliveryList();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}