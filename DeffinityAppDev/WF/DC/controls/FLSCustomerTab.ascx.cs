using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DC_controls_FLSCustomerTab : System.Web.UI.UserControl
{
    string[] array_path = { "FLSCustomer.aspx", "WF/DC/DCCustomerChat.aspx" };
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            hidPortfolioID.Value = sessionKeys.PortfolioID.ToString();
            if (!IsPostBack)
            {
                //set the session customer valuew
                sessionKeys.IncidentID = QueryStringValues.CallID;
                hsdid.Value = sessionKeys.IncidentID.ToString();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected string getUrl(int i)
    {
        string[] array_path = { "FLSCustomer.aspx", "DCCustomerChat.aspx" };

        string callId = Request.QueryString["callid"];


        return array_path[i] + "?callid=" + QueryStringValues.CallID;

    }
    protected string GetCssClass(int i)
    {

        string rtValue = string.Empty;
        if (i < array_path.Length)
        {
            if ((Request.Url.ToString().ToLower()).Contains(array_path[i].ToLower()) == true)
            {
                rtValue = "current1";

            }
           
        }
        return rtValue;
    }
}