using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_PortfolioDdlCtr : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            if (sessionKeys.PortfolioID == 0)
            {
                mdlPopup.Show();
            }
            else
            {  mdlPopup.Hide(); }
        }
    }

    private void BindData()
    {
        //dbind.DdlBindSelect(ddlPortfolio, "SELECT ID,PortFolio FROM ProjectPortfolio where visible=1 order by PortFolio", "ID", "PortFolio",false,true,false);

        ddlPortfolio.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
        ddlPortfolio.DataTextField = "PortFolio";
        ddlPortfolio.DataValueField = "ID";
        ddlPortfolio.DataBind();
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
            mdlPopup.Hide();
            Response.Redirect(Request.RawUrl,false);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
}
