using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using GoodsReceiptClass;
using Microsoft.ApplicationBlocks.Data;
using POMgt.DAL;
using POMgt.Entity;
using System.Linq;

public partial class Reports_BOMExportDataReport1 : System.Web.UI.Page
{
    //ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Export of BOM data";
        if (!IsPostBack)
        {
            setProjectPrefix();
            BindProjects();
            BindVendors();

        }
    }

    private void BindProjects()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "GoodsReceipt_ProjectList").Tables[0];
        ddlProjects.Items.Clear();
        ddlProjects.DataSource = dt;
        ddlProjects.DataTextField = "ProjectReferenceWithPrefix";
        ddlProjects.DataValueField = "ProjectReference";
        ddlProjects.DataBind();
        ddlProjects.Items.Insert(0, new ListItem("Please select...", "0"));

    }


    private void BindVendors()
    {


        try
        {
            PurchaseOrderMgtDataContext Vendors = new PurchaseOrderMgtDataContext();

            var vendorsList = from r in Vendors.v_Vendors

                              orderby r.ContractorName
                              select r;
            ddlVendor.DataSource = vendorsList;
            ddlVendor.DataValueField = "VendorID";
            ddlVendor.DataTextField = "ContractorName";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("Please select...", "0"));
            //ddlVendor.SelectedValue = setvalue.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void setProjectPrefix()
    {
        lblProjectPrefix.Value = sessionKeys.Prefix;
    }
    protected void btn_Submitt_Click(object sender, EventArgs e)
    {
        try
        {

            int projRef=0;
            lblErrorMsg.Visible = false;
            if ((ddlProjects.SelectedValue.ToString() == "0") && (txtProRef.Text.ToString() != ""))
            {
                
                projRef = int.Parse(txtProRef.Text.ToString());
            }
            //else if ((ddlProjects.SelectedValue.ToString() == "0") && (txtProRef.Text.ToString() == ""))
            //{
            //    lblErrorMsg.Visible = true;
            //    lblErrorMsg.Text = "Please select or enter project reference";
            //    lblErrorMsg.ForeColor = System.Drawing.Color.Red;
            //}
            else

            {
                
                projRef = int.Parse(ddlProjects.SelectedValue.ToString());
            }
            if ((ddlProjects.SelectedValue.ToString() == "0") && (txtProRef.Text.ToString() == "")&&projRef==0)
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = "Please select or enter project reference";
                lblErrorMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {

                TimesheetSummary.Attributes.Add("src", string.Format("Reports/BOMExportDataReportfrm.aspx?Prjref={0}&sdate={1}&edate={2}&vend={3}&type=pdf", projRef,
             Convert.ToDateTime(string.IsNullOrEmpty(txt_FromDate.Text) ? "01/01/1900" : txt_FromDate.Text).ToShortDateString()
             , Convert.ToDateTime(string.IsNullOrEmpty(txt_ToDate.Text) ? "01/01/1900" : txt_ToDate.Text).ToShortDateString(), ddlVendor.SelectedValue.ToString()));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }


    protected void lnkButtonExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int projRef;
            if ((ddlProjects.SelectedValue.ToString() == "0") && (txtProRef.Text.ToString() != ""))
            {
                projRef = int.Parse(txtProRef.Text.ToString());
            }
            else
            {
                projRef = int.Parse(ddlProjects.SelectedValue.ToString());
            }
            TimesheetSummary.Attributes.Add("src", string.Format("Reports/BOMExportDataReportfrm.aspx?Prjref={0}&sdate={1}&edate={2}&vend={3}&type=xsl1", projRef,
         Convert.ToDateTime(string.IsNullOrEmpty(txt_FromDate.Text) ? "01/01/1900" : txt_FromDate.Text).ToShortDateString()
         , Convert.ToDateTime(string.IsNullOrEmpty(txt_ToDate.Text) ? "01/01/1900" : txt_ToDate.Text).ToShortDateString(), ddlVendor.SelectedValue.ToString()));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void lnkButtonExcel1_Click(object sender, EventArgs e)
    {
        try
        {
            int projRef;
            if ((ddlProjects.SelectedValue.ToString() == "0") && (txtProRef.Text.ToString() != ""))
            {
                projRef = int.Parse(txtProRef.Text.ToString());
            }
            else
            {
                projRef = int.Parse(ddlProjects.SelectedValue.ToString());
            }
            TimesheetSummary.Attributes.Add("src", string.Format("Reports/BOMExportDataReportfrm.aspx?Prjref={0}&sdate={1}&edate={2}&vend={3}&type=xsl", projRef,
         Convert.ToDateTime(string.IsNullOrEmpty(txt_FromDate.Text) ? "01/01/1900" : txt_FromDate.Text).ToShortDateString()
         , Convert.ToDateTime(string.IsNullOrEmpty(txt_ToDate.Text) ? "01/01/1900" : txt_ToDate.Text).ToShortDateString(), ddlVendor.SelectedValue.ToString()));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
