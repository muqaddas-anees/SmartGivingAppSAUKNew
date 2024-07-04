using System;
using System.Data;
using System.Collections;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;


public partial class controls_InternalPO : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            BindDataPO();
        }

    }

    private void BindDataPO()
    {
        //PurchaseOrderMgtDataContext PODatabases = new PurchaseOrderMgtDataContext();

        //var assets = from r in PODatabases.v_PurchaseDetails

        //             select r;
        DataTable assets = new DataTable();

        assets = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "PO_GenInfo").Tables[0];
        if (Request.QueryString["project"] != null)
        {
            IEnumerable<DataRow> query = from myRow in assets.AsEnumerable()
                                         where myRow.Field<int>("ProjectRef") == Convert.ToInt32(Request.QueryString["project"].ToString())
                                         select myRow;
            if (query.Count() > 0)
            {
                assets = query.CopyToDataTable<DataRow>();
            }
            else
            {
                assets = null;
            }

        }
        grdPODetails.DataSource = assets;
        grdPODetails.DataBind();
    }

    protected void imgSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //PurchaseOrderMgtDataContext PODatabases = new PurchaseOrderMgtDataContext();

            //var assets = from r in PODatabases.v_PurchaseDetails
            //             where r.PONumber.Contains(txtPONumber.Text.Trim())
            //             select r;


            DataTable assets = new DataTable();

            assets = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "PO_GenInfoSearch",
                new SqlParameter("@PONumber", txtPONumber.Text.Trim().ToLower().Contains("po") ? txtPONumber.Text.Trim() : lblPO.Text.Trim() + txtPONumber.Text.Trim())).Tables[0];


            grdPODetails.DataSource = assets;
            grdPODetails.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdPODetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (e.CommandName == "Show")
            {
                if (Request.QueryString["project"] != null)
                    Response.Redirect("Project_InternalPODetails.aspx?POID=" + e.CommandArgument.ToString() + "&project=" + Request.QueryString["project"].ToString());

                else
                    Response.Redirect("POPurchaseDetails.aspx?POID=" + e.CommandArgument.ToString());
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        try
        {
            if (Request.QueryString["project"] != null)
                Response.Redirect("Project_InternalPODetails.aspx?project=" + Request.QueryString["project"].ToString());
            else
                Response.Redirect("POPurchaseDetails.aspx");
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

}
