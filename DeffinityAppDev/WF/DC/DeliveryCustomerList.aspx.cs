using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;

public partial class DC_DeliveryCustomerList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(sessionKeys.PortfolioID != 0)
                BindDeliveryList(sessionKeys.PortfolioID);
        }
    }
    #region Bind Delivery List by Company
    private void BindDeliveryList(int cid)
    {
        try
        {
            gvList.DataSource = DeliveryInformationBAL.BindDeliveryList(cid);
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
            BindDeliveryList(sessionKeys.PortfolioID);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}