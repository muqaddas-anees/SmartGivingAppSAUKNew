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
using System.Data.SqlClient;
using System.IO;
using System.Text;

public partial class FlowChartUploader : System.Web.UI.Page
{

    #region Form and Control Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            BindData();
        }
    }

    //Uploads the image
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        int documentLength = FlowChartUpload.PostedFile.ContentLength;
        byte[] byteDocTemp = new byte[documentLength];
        Stream objStream;
        objStream = FlowChartUpload.PostedFile.InputStream;
        objStream.Read(byteDocTemp, 0, documentLength);
        try
        {
            using (FlowChartHelper helper = new FlowChartHelper())
            {
                helper.InsertNewRecord(txtReferenceNumber.Text, txtFlowChartName.Text, txtDescription.Text, byteDocTemp, FlowChartUpload.FileName,sessionKeys.PortfolioID.ToString());
            }
            lblMessage.Text = "Record updated/inserted successfully";
            BindData();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Record updation/insertion failed. Please try again.";
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
            //Log the error details into the text file.
            StringBuilder exceptionDetails = new StringBuilder();
            exceptionDetails.AppendFormat("Exception source:{0}", ex.Source);
            exceptionDetails.AppendFormat("Inner Exception:{0}", ex.InnerException);
            exceptionDetails.AppendFormat("Message:{0}", ex.Message);
            exceptionDetails.AppendFormat("Stack Trace:{0}", ex.StackTrace);
            exceptionDetails.AppendFormat("Target Site:{0}", ex.TargetSite);
            exceptionDetails.AppendFormat("Data:{0}", ex.Data);
            LogExceptions.LogException(exceptionDetails.ToString());
        }
    }

    //Cancel automatic deletion by the gridview. Since, the deletion is handled by FlowChartHelper.cs Class.
    protected void gridFlowChart_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        e.Cancel = true;
        BindData();
    }

    #endregion

    #region Form Helper Methods

    private void ClearFormFields()
    {
        lblMessage.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtFlowChartName.Text = string.Empty;
        txtReferenceNumber.Text = string.Empty;
    }

    private void BindData()
    {
        using (FlowChartHelper helper = new FlowChartHelper())
        {
            if (sessionKeys.PortfolioID==0)
                gridFlowChart.DataSource = helper.getFlowChartRecords(0);
            else
                gridFlowChart.DataSource = helper.getFlowChartRecords(sessionKeys.PortfolioID);
            gridFlowChart.DataBind();
        }
        ClearFormFields();
    }

   
    #endregion

    protected void gridFlowChart_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        gridFlowChart.PageIndex = e.NewSelectedIndex;
        BindData();
    }
}