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

public partial class FlowChartDownLoad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // Master.PageHead = "Processes";
            BindData();
        }
    }

    protected void gridFlowChart_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            using (FlowChartHelper helper = new FlowChartHelper())
            {
                //Does the required operations
                DataTable downLoadContent = (DataTable)helper.DataGridOperations(e);
                if (downLoadContent.ToString() != "NoDownLoad")
                    helper.downLoadFile(downLoadContent);
            }
        }
        catch (Exception ex)
        {
            //Ignore any errors
        }
    }

    //Cancel automatic deletion by the gridview. Since, the deletion is handled by FlowChartHelper.cs Class.
    protected void gridFlowChart_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        e.Cancel = true;
        BindData();
    }

    private void BindData()
    {
        using (FlowChartHelper helper = new FlowChartHelper())
        {
            gridFlowChart.DataSource = helper.getFlowChartRecords(sessionKeys.PortfolioID);
            gridFlowChart.DataBind();
        }
    }
}