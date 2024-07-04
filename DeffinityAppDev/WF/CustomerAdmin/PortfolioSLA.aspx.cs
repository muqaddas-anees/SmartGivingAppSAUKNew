using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Deffinity.Bindings;
using Deffinity.PortfolioSLAmanager;

public partial class PortfolioSLA : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    DisBindings getdata = new DisBindings();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        if (!IsPostBack)
        {
            //Master.PageHead = "Customer Admin";
            //FillFields();
            //bindloads(0);
            fillGrid();
        }
        txtNewCategory.Visible = false;
        bntCancelCategory.Visible = false;
        
    }

    #region Helper Methods


    public DataTable BindCategoryData()
    {
        return Deffinity.PortfolioSLAmanager.PortfolioSLA.CategoryAssociatedToPortfolio(sessionKeys.PortfolioID, 0); //DefaultDatabind.CategoryAssociatedToPortfolio(sessionKeys.PortfolioID);
    }

    private void fillGrid()
    {
        try
        {
           // int MasterID = Convert.ToInt32(ddlmastercategory.SelectedValue);
            GridView1.DataSource = Deffinity.PortfolioSLAmanager.PortfolioSLA.PortfolioSLA_Select(sessionKeys.PortfolioID, 0);
            GridView1.DataBind();
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

   
    #endregion

    #region Control Events
    private string GetCategory()
    {
        string category = string.Empty;
        if (ddlCategory.Visible == true)
        {
            if (ddlCategory.SelectedItem.Text != "Please Select...")
            {
                category = ddlCategory.SelectedItem.Text;
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(txtNewCategory.Text.Trim()))
            {
                category = txtNewCategory.Text.Trim();
            }
            ddlCategory.SelectedIndex = 0;
        }
        return category;
    }
    protected void btnADDSLA_Click(object sender, EventArgs e)
    {
        try
        {
            //int MasterID = Convert.ToInt32(ddlmastercategory.SelectedValue);
            Deffinity.PortfolioSLAmanager.PortfolioSLA.PortfolioSLA_Insert(Convert.ToInt32(ddlRequestType.SelectedValue), Convert.ToInt32(ddlmastercategory.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), sessionKeys.PortfolioID, txtTimetoresolve.Text.Substring(0, txtTimetoresolve.Text.Length - 1).Trim(),
                txtDescription.Text, txtTimetoresolve.Text.Substring(txtTimetoresolve.Text.Length - 1).Trim(), int.Parse(txtTakeInHand.Text.Substring(0, txtTakeInHand.Text.Length - 1).Trim()),
                txtTakeInHand.Text.Substring(txtTakeInHand.Text.Length - 1).Trim());
            int masterid = Convert.ToInt32(ddlmastercategory.SelectedValue);
            //bindloads(masterid);
            fillGrid();
            ddlCategory.Visible = true;
            btnAddCategory.Visible = true;
            txtNewCategory.Text = string.Empty;
            txtTimetoresolve.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtTakeInHand.Text = string.Empty;
            //drop down validations disable.
            RequiredFieldValidator2.ErrorMessage = string.Empty;
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }

    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        txtNewCategory.Visible = true;
        bntCancelCategory.Visible = true;
        ddlCategory.Visible = false;
        btnAddCategory.Visible = false;
    }

    protected void bntCancelCategory_Click(object sender, EventArgs e)
    {
        txtNewCategory.Visible = false;
        bntCancelCategory.Visible = false;
        ddlCategory.Visible = true;
        btnAddCategory.Visible = true;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillGrid();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = GridView1.EditIndex;
                GridViewRow Row = GridView1.Rows[i];
                //string Portfolio1 = ((DropDownList)Row.FindControl("ddlPortfolio1")).SelectedItem.Value;
                //string Category1 = ((DropDownList)Row.FindControl("ddlCategory1")).SelectedItem.Value;
                string TimetoResolve1 = ((TextBox)Row.FindControl("txtTimetoResolve1")).Text;
                string TakeInHand = ((TextBox)Row.FindControl("txtTakeInHand")).Text;
                string Description1 = ((TextBox)Row.FindControl("txtDescription1")).Text;


                int verification = Deffinity.PortfolioSLAmanager.PortfolioSLA.PortfolioSLA_Update(ID, 
                    TimetoResolve1.Substring(0, TimetoResolve1.Length - 1),
                    Description1, TimetoResolve1.Substring(TimetoResolve1.Length - 1), int.Parse(TakeInHand.Substring(0, TakeInHand.Length - 1)),
                    TakeInHand.Substring(TakeInHand.Length - 1)); 
                if (verification == 1)
                {
                    //lblError.Text = "This category is already associated with the same portfolio";
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            
        }
        if (e.CommandName == "Delete")
        {
            try
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                SqlCommand comm_del = new SqlCommand("delete from PortfolioSLA WHERE ID=" + ID, conn);
                conn.Open();
                int c = comm_del.ExecuteNonQuery();
                if (c > 0)
                {
                    fillGrid();
                }
             }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        fillGrid();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillGrid();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillGrid();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillGrid();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillGrid();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        fillGrid();
    }

    #endregion

    #region incident management
    public void FillFields()
    {
       // ddlmastercategory.DataSource = Deffinity.PortfolioSLAmanager.PortfolioSLA.GetMasterCategory(sessionKeys.PortfolioID);
       // ddlmastercategory.DataBind();
    }
    //protected void btnSlaSubmit_Click(object sender, EventArgs e)
    //{
    //    InsserUpdateTargetSLA();        
    //}
    //private void InsserUpdateTargetSLA()
    //{
    //    try
    //    {

    //        SqlCommand comm_udpate = new SqlCommand("DN_InsertUpdateTargetSLA", conn);
    //        comm_udpate.CommandType = CommandType.StoredProcedure;
    //        comm_udpate.Parameters.Add("@PortfolioID", SqlDbType.Int).Value = sessionKeys.PortfolioID;
    //        comm_udpate.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;
    //        comm_udpate.Parameters.Add("@OvrallTargetSLA", SqlDbType.Float).Value = string.IsNullOrEmpty(txtTargetSLA.Text) ? 0 : Convert.ToDouble(txtTargetSLA.Text);
    //        comm_udpate.Parameters.Add("@BaselineVolume", SqlDbType.Int).Value = string.IsNullOrEmpty(txtBaselineVolume.Text) ? 0 : Convert.ToInt32(txtBaselineVolume.Text);
    //        comm_udpate.Parameters.Add("@BaselineCost", SqlDbType.Float).Value = string.IsNullOrEmpty(txtBaselineCost.Text) ? 0 : Convert.ToDouble(txtBaselineCost.Text);
    //        comm_udpate.Parameters.Add("@FlagGreen", SqlDbType.Float).Value = string.IsNullOrEmpty(txtRagGreen.Text) ? 0 : Convert.ToDouble(txtRagGreen.Text);
    //        comm_udpate.Parameters.Add("@FlagAmber", SqlDbType.Float).Value = string.IsNullOrEmpty(txtRagAmber.Text) ? 0 : Convert.ToDouble(txtRagAmber.Text);
    //        comm_udpate.Parameters.Add("@FlagRed", SqlDbType.Float).Value = string.IsNullOrEmpty(txtRagRed.Text) ? 0 : Convert.ToDouble(txtRagRed.Text);
    //        comm_udpate.Parameters.Add("@IncidentType", SqlDbType.NVarChar).Value = ddlcasetype.SelectedItem.ToString();
    //        comm_udpate.Parameters.Add("@CategoryID", SqlDbType.Int).Value = (ddlCategory.SelectedValue == "Please select...") ? 0 : Convert.ToInt32(ddlCategory.SelectedValue);
    //        try
    //        {
    //            conn.Open();
    //            comm_udpate.ExecuteNonQuery();
    //        }
    //        catch (Exception ex)
    //        {
    //            LogExceptions.WriteExceptionLog(ex);

    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.LogException(ex.Message);
    //    }
    
    //}
    //private void selectTargerSLA()
    //{
    //    try
    //    {
    //        using (SqlCommand cmd = new SqlCommand("DN_SelectTargetSLA", conn))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.Add("@PortfolioID", SqlDbType.Int).Value = sessionKeys.PortfolioID;
    //            cmd.Parameters.Add("@IncidentType", SqlDbType.VarChar).Value = ddlcasetype.SelectedValue;
    //            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = ddlCategory.SelectedValue == "Please select..." ? 0 : Convert.ToInt32(ddlCategory.SelectedValue);

    //            conn.Open();
    //            using (SqlDataReader reader = cmd.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    txtBaselineCost.Text = reader["BaselineCost"].ToString();
    //                    txtBaselineVolume.Text = reader["BaselineVolume"].ToString();
    //                    txtRagAmber.Text = reader["FlagAmber"].ToString();
    //                    txtRagGreen.Text = reader["FlagGreen"].ToString();
    //                    txtRagRed.Text = reader["FlagRed"].ToString();
    //                    txtTargetSLA.Text = reader["OvrallTargetSLA"].ToString();
    //                    ddlcasetype.SelectedValue = reader["IncidentType"].ToString();
    //                }
    //            }
    //        }
    //        conn.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.LogException(ex.Message);
    //    }
    //}
    protected void btnSlaCancel_Click(object sender, EventArgs e)
    {
       // clearFields();
    }
    //private void clearFields()
    //{
    //    txtBaselineCost.Text = string.Empty;
    //    txtBaselineVolume.Text = string.Empty;
    //    txtRagAmber.Text = string.Empty;
    //    txtRagGreen.Text = string.Empty;
    //    txtRagRed.Text = string.Empty;
    //    txtTargetSLA.Text = string.Empty;        
    //}
    #endregion

    //protected void ddlcasetype_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //clearFields();
    //        //selectTargerSLA();
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}

    protected void btnDeleteCateogry_Click(object sender, EventArgs e)
    {
        UpdateCategoryStatus(Convert.ToInt32(ddlCategory.SelectedValue), true);
        FillFields();
    }
    private void UpdateCategoryStatus(int CategoryID, bool HiddenStatus)
    {
        SqlParameter[] sqlParams;
        sqlParams = new SqlParameter[]{ new SqlParameter("@CategoryID",CategoryID),
            new SqlParameter("@HiddenStatus",HiddenStatus)
        };
        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_HideProjectCategory", sqlParams);
    }
    protected void ddlmastercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //int masterid = Convert.ToInt32(ddlmastercategory.SelectedValue);
            //bindloads(masterid);
            fillGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void bindloads(int masterid)
    {
        BindCategory(masterid);
        //selectTargerSLA();
    }
    private void BindCategory(int masterid)
    {
        //ddlCategory.DataSource = Deffinity.PortfolioSLAmanager.PortfolioSLA.CategoryAssociatedToPortfolio(sessionKeys.PortfolioID, masterid);
        //ddlCategory.DataValueField = "CategoryID";
        //ddlCategory.DataTextField = "CategoryName";
        //ddlCategory.DataBind();
        //ddlCategory.Items.Insert(0, Constants.ddlDefaultBind(true));
    }
    protected void lnkOk_Click(object sender, EventArgs e)
    {
        try
        {
            int Portfolio = sessionKeys.PortfolioID;
            string Category = txtmastercategory.Text.Trim();
            Deffinity.PortfolioSLAmanager.PortfolioSLA.InsertMasterCategory(Category, Portfolio);
            FillFields();
            txtmastercategory.Text = "";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
     }
}


