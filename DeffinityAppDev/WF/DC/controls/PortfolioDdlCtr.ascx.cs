using DC.BLL;
using DC.Entity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_DC_PortfolioDdlCtr : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (QueryStringValues.CallID > 0)
            {
                SetCustomerByCallID(QueryStringValues.CallID);
            }
            //bind data
            BindData();
            if (sessionKeys.PortfolioID == 0)
            {
                mdlPopup.Show();
            }
            else
            {
                checkFiledsConfig(sessionKeys.PortfolioID);
                mdlPopup.Hide(); }
        }
    }

    private void BindData()
    {
        //dbind.DdlBindSelect(ddlPortfolio, "SELECT ID,PortFolio FROM ProjectPortfolio where visible=1 order by PortFolio", "ID", "PortFolio",false,true,false);

        ddlPortfolio.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
        ddlPortfolio.DataTextField = "PortFolio";
        ddlPortfolio.DataValueField = "ID";
        ddlPortfolio.DataBind();
        ddlPortfolio.Items.RemoveAt(0);
        ddlPortfolio.Items.Insert(0, new ListItem("Please select...", "0"));
        if (ddlPortfolio.Items.Count > 0)
        {
            if (sessionKeys.PortfolioID != 0)
            {
                ddlPortfolio.SelectedValue = sessionKeys.PortfolioID.ToString();
            }
            else
            {

                sessionKeys.PortfolioID = Convert.ToInt32(ddlPortfolio.SelectedValue);
                sessionKeys.PortfolioName = ddlPortfolio.SelectedItem.Text;

            }
        }
    }
    protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            sessionKeys.PortfolioID = Convert.ToInt32(ddlPortfolio.SelectedValue);
            sessionKeys.PortfolioName = ddlPortfolio.SelectedItem.Text;
            if (sessionKeys.PortfolioID > 0)
            {
                checkFiledsConfig(sessionKeys.PortfolioID);
                mdlPopup.Hide();
                Response.Redirect(Request.RawUrl, false);
            }
            else
                mdlPopup.Show();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }

    protected void btnCloseNew_Click(object sender, EventArgs e)
    {
        //sessionKeys.PortfolioID = Convert.ToInt32(ddlPortfolio.SelectedValue);
        //sessionKeys.PortfolioName = ddlPortfolio.SelectedItem.Text;
        if (sessionKeys.PortfolioID > 0)
        {
            mdlPopup.Hide();
            Response.Redirect(Request.RawUrl, false);
        }
        else
            mdlPopup.Show();
    }

    #region check Service desk fields

    public void checkFiledsConfig(int customerID)
    {
        var cnt = DC.BAL.FLSFieldsConfigBAL.GetFLSFieldsConfigCount(customerID);
        if(cnt == 0)
        {
            DC.BAL.FLSFieldsConfigBAL.InsertConfigData(customerID,0);
        }
    }

    public void SetCustomerByCallID(int callid)
    {

        CallDetail cd = CallDetailsBAL.SelectbyId(callid);

        if (cd != null)
        {
            sessionKeys.PortfolioID = cd.CompanyID.Value;
            
        }
    }

    #endregion
}
