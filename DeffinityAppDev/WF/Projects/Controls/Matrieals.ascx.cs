using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

public partial class controls_Matrieals : System.Web.UI.UserControl
{
    SqlConnection conn = new SqlConnection(Constants.DBString);
   public decimal totalcp = 0;
   public decimal totalsp = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMaterialsGrid();
        }
    }
    private void BindMaterialsGrid()
    {
        try
        {
            GetMaterils();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    private DataTable GetMaterilasList(int projectReffernce)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure,
            "DN_ProjectBudget_Materials", new SqlParameter("@ProjectReference", projectReffernce)).Tables[0];
    }

    private  void GetMaterils()
    {
       
        projectTaskDataContext projects = new projectTaskDataContext();
        var materials = (from r in projects.ProjectBOMDetils
                         where r.ProjectReference == QueryStringValues.Project
                         && r.QtyReceived != 0
                         select new { r.ID, r.Description, r.PartNumber, r.Company, r.QtyReceived,price=(r.Material+r.Mics+r.Labour), Toatls= ((r.Material+r.Mics+r.Labour)*r.QtyReceived) }).ToList();
          grdMaterials.DataSource =materials;
            grdMaterials.DataBind();
    }
    public DataTable GetVendorsList()
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure,
            "DN_ProjectBudget_Vendors").Tables[0];
    }
    //DN_ProjectBudget_Vendors
    protected void grdMaterials_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {  
               
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.FindControl("ddlVendor") != null)
                {
                    DropDownList ddlVendor = (DropDownList)e.Row.FindControl("ddlVendor");

                    ddlVendor.DataSource = GetVendorsList();
                    ddlVendor.DataValueField = "VendorID";
                    ddlVendor.DataTextField = "ContractorName";
                    ddlVendor.DataBind();
                    ddlVendor.Items.Insert(0, new ListItem("Please select...", "0"));
                    ddlVendor.SelectedValue = DataBinder.Eval(e.Row.DataItem, "VendorID").ToString();
                }
               
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                projectTaskDataContext projects = new projectTaskDataContext();
                var materials = (from r in projects.ProjectBOMDetils
                                 where r.ProjectReference == QueryStringValues.Project
                                 && r.QtyReceived != 0
                                 select (r.QtyReceived * (r.Material + r.Mics + r.Labour))).Sum();
                Label lblCp = (Label)e.Row.FindControl("lblTotalPrice1");

                lblCp.Text = string.Format("{0:f2}", materials.Value);
                //Label lblSp = (Label)e.Row.FindControl("totalSp");
                //lblCp.Text = string.Format("{0:F2}", totalcp);
                //lblSp.Text = string.Format("{0:F2}", totalsp);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdMaterials_RowEditing(object sender, GridViewEditEventArgs e)
    {
    
             grdMaterials.EditIndex = e.NewEditIndex;
         BindMaterialsGrid();
    }
    protected void grdMaterials_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdMaterials.EditIndex = -1;
        BindMaterialsGrid();
    }
    protected void grdMaterials_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        grdMaterials.EditIndex = -1;
        BindMaterialsGrid();
    }
    protected void grdMaterials_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        grdMaterials.EditIndex = -1;
        BindMaterialsGrid();
    }
    protected void grdMaterials_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Update")
        {

            try
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = grdMaterials.EditIndex;
                GridViewRow Row = grdMaterials.Rows[i];
                TextBox txtItemDes = (TextBox)Row.FindControl("txtItemDescription");
                TextBox txtPartNo = (TextBox)Row.FindControl("txtPartNumber");
                DropDownList ddlVendor = (DropDownList)Row.FindControl("ddlVendor");
                TextBox txtQty = (TextBox)Row.FindControl("txtQty");
                TextBox txtCostPrice = (TextBox)Row.FindControl("txtCostPrice");
                TextBox txtSellingPrice = (TextBox)Row.FindControl("txtSellingPrice");
                TextBox txtNotes = (TextBox)Row.FindControl("txtNotes");

                SqlCommand comm_update = new SqlCommand("DN_ProjectBudget_UpdateMaterial", conn);
                conn.Open();
                comm_update.CommandType = CommandType.StoredProcedure;
                comm_update.Parameters.Add("@ItemDescription", SqlDbType.NVarChar, 500).Value = txtItemDes.Text;
                comm_update.Parameters.Add("@BuyingPrice", SqlDbType.Float).Value =
                    Convert.ToDouble(string.IsNullOrEmpty(txtCostPrice.Text) ? "0" : txtCostPrice.Text);
                comm_update.Parameters.Add("@SellingPrice", SqlDbType.Float).Value =
                    Convert.ToDouble(string.IsNullOrEmpty(txtSellingPrice.Text) ? "0" : txtSellingPrice.Text);

                comm_update.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = txtNotes.Text;
                comm_update.Parameters.Add("@VendorID", SqlDbType.Int).Value = ddlVendor.SelectedValue;
                comm_update.Parameters.Add("@QTY", SqlDbType.Int, 500).Value =
                    int.Parse(string.IsNullOrEmpty(txtQty.Text) ? "0" : txtQty.Text);
                comm_update.Parameters.Add("@PartNumber", SqlDbType.NVarChar, 500).Value = txtPartNo.Text;
                comm_update.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                comm_update.ExecuteNonQuery();
                conn.Close();

            }


            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            finally
            {
                conn.Close();
            }
        }

        if (e.CommandName == "Delete")
        {
             try
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = grdMaterials.EditIndex;
                SqlCommand comm_del = new SqlCommand("delete from ContractorMaterialCatalogue where ID=" + ID, conn);
                conn.Open();
                int c1 = comm_del.ExecuteNonQuery();
                conn.Close();
                if (c1 > 0)
                {
                    BindMaterialsGrid();
                }

            }


             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
             }
             finally
             {
                 conn.Close();
             }
        }
    }
}
