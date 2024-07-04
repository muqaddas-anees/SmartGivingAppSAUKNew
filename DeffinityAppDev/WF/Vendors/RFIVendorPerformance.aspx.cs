using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.ApplicationBlocks.Data;

public partial class RFIVendorPerformance : System.Web.UI.Page
{
    DisBindings getdata = new DisBindings();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (!this.IsPostBack)
        {
            //Master.PageHead = "Vendor Performance";
            ddlVendors.DataBind();
            ddlVendors.Items.Insert(0, new ListItem("Please select...", "0"));
            ddlVendors.SelectedValue = QueryStringValues.Vendor.ToString();
            GetFeedback();
            BindgridVP(QueryStringValues.Vendor);
            BindChart(QueryStringValues.Vendor);
        }
        lblmsg.Visible = false;
        //add css dynamically
        //HtmlLink css = new HtmlLink();
        //css.Href = ResolveClientUrl("~/App_Themes/Default/Ratings.css");
        //css.Attributes["rel"] = "stylesheet";
        //css.Attributes["type"] = "text/css";
        ////css.Attributes["media"] = "all";
        //Page.Header.Controls.Add(css);
        
    }

    protected void btnSubmitHours_Click(object sender, ImageClickEventArgs e)
    {
        insertFeedback();

        //DEFFINITY_FEEDBACK
        //int i = RatingTime.CurrentRating;
        //Response.Write(i.ToString());
    }

    protected void ddlVendors_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlVendors.SelectedValue) != 0)
        {
            GetFeedback();
            BindgridVP(Convert.ToInt32(ddlVendors.SelectedValue.ToString()));
            BindChart(Convert.ToInt32(ddlVendors.SelectedValue.ToString()));
        }
        else
        {
            RatingTime.CurrentRating = 0;
            RatingQuality.CurrentRating = 0;
            RatingMoney.CurrentRating = 0;
            RatingCommunication.CurrentRating = 0;
        }
    }

    private void GetFeedback()
    {
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_RFI_VENDORPERFORMANCE_FILLMOSTRECENT");
        db.AddInParameter(cmd, "@VENDORID", DbType.Int32, Convert.ToInt32(ddlVendors.SelectedValue));
        db.AddOutParameter(cmd, "@TIMELINESS", DbType.Int32, RatingTime.CurrentRating);
        db.AddOutParameter(cmd, "@QUALITYOFWORK", DbType.Int32, RatingQuality.CurrentRating);
        db.AddOutParameter(cmd, "@VALUEFORMONEY", DbType.Int32, RatingMoney.CurrentRating);
        db.AddOutParameter(cmd, "@COMMUNICATION", DbType.Int32, RatingCommunication.CurrentRating);
        db.ExecuteNonQuery(cmd);
        RatingTime.CurrentRating = (int)db.GetParameterValue(cmd, "TIMELINESS");
        RatingQuality.CurrentRating = (int)db.GetParameterValue(cmd, "QUALITYOFWORK");
        RatingMoney.CurrentRating = (int)db.GetParameterValue(cmd, "VALUEFORMONEY");
        RatingCommunication.CurrentRating = (int)db.GetParameterValue(cmd, "COMMUNICATION");
        cmd.Dispose();
    }

    private void insertFeedback()
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_RFI_VENDORPERFORMANCE_INSERT");
            //add parameters

            db.AddInParameter(cmd, "@VENDORID", DbType.Int32, Convert.ToInt32(ddlVendors.SelectedValue));
            db.AddInParameter(cmd, "@PMID", DbType.Int32, sessionKeys.UID);
            db.AddInParameter(cmd, "@TIMELINESS", DbType.Int32, RatingTime.CurrentRating);
            db.AddInParameter(cmd, "@QUALITYOFWORK", DbType.String, RatingQuality.CurrentRating);
            db.AddInParameter(cmd, "@VALUEFORMONEY", DbType.Int32, RatingMoney.CurrentRating);
            db.AddInParameter(cmd, "@COMMUNICATION", DbType.Int32, RatingCommunication.CurrentRating);
            db.AddInParameter(cmd, "@SUBMITTEDDATE", DbType.DateTime, DateTime.Now);

            db.ExecuteNonQuery(cmd);
            lblmsg.Text = "Feedback saved";
            lblmsg.ForeColor = System.Drawing.Color.Green;
            lblmsg.Visible = true;
            cmd.Dispose();
            GetFeedback();
            BindgridVP(Convert.ToInt32(ddlVendors.SelectedValue));
            BindChart(Convert.ToInt32(ddlVendors.SelectedValue));

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            //lblmsg.Visible = true;
            //error = ex.Message.ToString();
        }



    }

    private void BindChart(int VendorID)
    {
        DataSet ds = new DataSet();
        ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_RFI_VENDORPERFORMANCE_GRAPH", new SqlParameter("@VENDORID", VendorID));
        UltraChart1.DataSource = ds;
        UltraChart1.DataBind();
    }

    #region gridVendorPerformance Event Handlers

    private void BindgridVP(int VendorID)
    {
        DataSet ds = new DataSet();
        ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_RFI_VENDORPERFORMANCE_FILL", new SqlParameter("@VENDORID", VendorID));
        gridVendorPerformance.DataSource = ds;
        gridVendorPerformance.DataBind();
    }

    protected void gridVendorPerformance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridVendorPerformance.PageIndex = e.NewPageIndex;
       // BindgridVP();
    }

    protected void gridVendorPerformance_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridVendorPerformance.EditIndex = e.NewEditIndex;
        //BindgridVP(); 
    }

    protected void gridVendorPerformance_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridVendorPerformance.EditIndex = -1;
        //BindgridVP(); 
    }
  
    protected void gridVendorPerformance_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
    }

    //protected void gridVendorPerformance_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            if (e.Row.FindControl("ddlgvCategory") != null)
    //            {
    //                string category = DataBinder.Eval(e.Row.DataItem, "Category").ToString();
    //                SqlParameter[] parameters = new SqlParameter[1];
    //                parameters[0] = new SqlParameter("@Category", category);
    //                int ID = (int)SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_RFI_BBBEECategory_GETID", parameters);

    //                string element = DataBinder.Eval(e.Row.DataItem, "Element").ToString();
    //                SqlParameter[] param = new SqlParameter[1];
    //                param[0] = new SqlParameter("@ElementType", element);
    //                int eleID = (int)SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_RFI_BBBEEElement_GETID", param);

    //                DropDownList ddlgvCategory = (DropDownList)e.Row.FindControl("ddlgvCategory");
    //                DataTable dt = new DataTable();
    //                dt = getdata.GetDatatable("DEFFINITY_RFI_BBBEECategory_FILL", true);
    //                ddlgvCategory.DataSource = dt;
    //                ddlgvCategory.DataTextField = "Category";
    //                ddlgvCategory.DataValueField = "ID";
    //                ddlgvCategory.DataBind();
    //                ddlgvCategory.Items.Insert(0, new ListItem("Please select...", "0"));
    //                ddlgvCategory.SelectedValue = ID.ToString();

    //                DropDownList ddlgvElement = (DropDownList)e.Row.FindControl("ddlgvElement");
    //                DataTable dt1 = new DataTable();
    //                dt1 = getdata.GetDatatable("DEFFINITY_RFI_BBBEEElement_FILL", true);
    //                ddlgvElement.DataSource = dt1;
    //                ddlgvElement.DataTextField = "ElementType";
    //                ddlgvElement.DataValueField = "ID";
    //                ddlgvElement.DataBind();
    //                ddlgvElement.Items.Insert(0, new ListItem("Please select...", "0"));
    //                ddlgvElement.SelectedValue = eleID.ToString();

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}

    protected void gridVendorPerformance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "Update")
            {
                int i = gridVendorPerformance.EditIndex;
                GridViewRow Row = gridVendorPerformance.Rows[i];
                TextBox txtgvPoints = (TextBox)Row.FindControl("txtgvPoints");
                TextBox txtgvTarget = (TextBox)Row.FindControl("txtgvTarget");

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Points", Convert.ToSingle(txtgvPoints.Text.ToString()));
                parameters[1] = new SqlParameter("@Target", Convert.ToSingle(txtgvTarget.Text.ToString()));
                parameters[2] = new SqlParameter("@ID", ID);
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_RFI_BBBEE_UPDATE", parameters);
                txtgvPoints.Text = "";
                txtgvTarget.Text = "";
                gridVendorPerformance.EditIndex = -1;
                //BindgridVP(); 
              
            }
            if (e.CommandName == "Dissolve")
            {
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_RFI_VENDORPERFORMANCE_DELETE", new SqlParameter("@ID", ID));
                //BindgridVP();
                GetFeedback();
                BindgridVP(Convert.ToInt32(ddlVendors.SelectedValue));
                BindChart(Convert.ToInt32(ddlVendors.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void gridVendorPerformance_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

     #endregion
}
