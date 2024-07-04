using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Deffinity.CustomerConfig;

public partial class PortfolioCustomerConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if (Request.QueryString["type"] != null)
            {
                SetCustomerDefault();

                GridView1.DataBind();
                PortfolioDdlCtr1.Visible = false;

            }
        }
    }
    private void UpdateConfig(bool Checked)
    {
        try
        {
            CustomerConfig cc = new CustomerConfig();

            foreach (GridViewRow row in GridView1.Rows)
            {
                //string s = row.Cells[0].Controls[1].ToString();
                CheckBox chkNew = (CheckBox)row.Cells[0].Controls[1];
                int id = Convert.ToInt32(((HiddenField)row.FindControl("HID")).Value);
                if (chkNew.Checked)
                {

                    cc.Visible = Checked;
                    cc.ID = id;
                    CustomerConfigManager.CustomerConfig_Update(cc);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void Update_Click(object sender, EventArgs e)
    {
        SetCustomerDefault();
       UpdateConfig(true); 
        //bind grigview
       GridView1.DataBind();

    }
    protected void btnDisable_Click(object sender, EventArgs e)
    {
        SetCustomerDefault();
        UpdateConfig(false);
        //bind grigview
        GridView1.DataBind();
    }
    protected void btnApplyAllCustomers_Click(object sender, EventArgs e)
    {
        try
        {
            SetCustomerDefault();
            //CustomerConfigManager.CustomerConfig_ApplyToAll(sessionKeys.PortfolioID);
            //apply Default values
            CustomerConfigManager.CustomerConfig_ApplyToAll(0);
            lblCustomerMsg.Text = "Customer Portal configuration applied successfully.";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "apply")
            {
                SetCustomerDefault();
                int sectionid = Convert.ToInt32(e.CommandArgument.ToString());
                CustomerConfigManager.CustomerConfig_ApplyToAll(sessionKeys.PortfolioID, sectionid);
                lblCustomerMsg.Text = "Applied successfully.";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void SetCustomerDefault()
    {
        if (Request.QueryString["type"] != null)
        {
            sessionKeys.PortfolioID = 0;
            sessionKeys.PortfolioName = string.Empty;
        }
    }
}
