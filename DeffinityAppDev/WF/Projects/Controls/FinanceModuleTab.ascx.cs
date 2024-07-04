using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_FinanceModuleTab : System.Web.UI.UserControl
{
    private string[] Purl = { "FMResources.aspx", "FMProjects.aspx", "CustomerContracts.aspx", "FMInvoicing.aspx", "POJournal.aspx", "ExportofProjectOverviewdata.aspx", "BoMSupplierPayments.aspx", "FMWorkInProgress.aspx", "FMSalesForeCast.aspx", "KPIFinancial.aspx" };
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected string GetCssClass(int i)
    {
        string rtValue = string.Empty;
        if (i < Purl.Length)
        {
            string stemp = Purl[i];
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
                rtValue = "current2";
            }
            else
            {
                rtValue = string.Empty;
            }
        }
        return rtValue;
    }

}
