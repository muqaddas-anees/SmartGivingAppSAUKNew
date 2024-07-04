using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryMgt.Entity;
using InventoryMgt.DAL;
using System.Web.UI.HtmlControls;
using System.Data;
//using Infragistics.WebUI.UltraWebGrid;
using ProjectMgt.DAL;


public partial class controls_GridFieldConfigurator : System.Web.UI.UserControl
{
    PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> IC = null;
    InventoryRepository<GridFieldConfigurator> IGFC = null;
    GridFieldConfigurator GFC = null;
    List<GridFieldConfigurator> GFCList = null;
    List<InventoryManager_UsageCustomData> In_ucd = null;
    InventoryRepository<InventoryMgt.Entity.InventoryManager_UsageCustomData> IMUCD = null;
    Inventory_BatchCustomData BCD = null;
    InventoryRepository<Inventory_BatchCustomData> IBCD = null;
    List<Inventory_BatchCustomData> BCDList = null;

    Table tbl = null;
    TableRow tr = null;
    TableCell td = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCustomer();
            BindFields();
            //SinglerecordInsert();
        }
    }
    public void BindCustomer()
    {
        try
        {
            IC = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();
            var iclist = IC.GetAll().OrderBy(a => a.PortFolio).ToList();
            ddlcustomer.DataSource = iclist;
            ddlcustomer.DataTextField = "PortFolio";
            ddlcustomer.DataValueField = "ID";
            ddlcustomer.DataBind();
            ddlcustomer.Items.Insert(0, new ListItem("Please select", "0"));

            if(sessionKeys.PortfolioID >0)
            {
                ddlcustomer.SelectedValue = sessionKeys.PortfolioID.ToString();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindGrid()
    {
        try
        {
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            var List = IGFC.GetAll().Where(o => o.CustomerId == int.Parse(ddlcustomer.SelectedValue)).OrderBy(o => o.Position).ToList();
            grid.DataSource = List;
            grid.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlcustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFields();
    }
    public void SinglerecordInsert()
    {
        if (ddlcustomer.SelectedValue != "0")
        {
            var lcount = IGFC.GetAll().Where(o => o.CustomerId == int.Parse(ddlcustomer.SelectedValue) && o.DeafaultName == "Record Number").FirstOrDefault();
            int i = IGFC.GetAll().Where(o => o.CustomerId == int.Parse(ddlcustomer.SelectedValue)).OrderByDescending(o => o.Position.Value).FirstOrDefault().Position.Value;
            if (lcount== null)
            {
                GFC = new GridFieldConfigurator();
                GFC.CustomerId = int.Parse(ddlcustomer.SelectedValue);
                GFC.DeafaultName = "Record Number";
                GFC.DisplayName = "Record Number";
                GFC.Position = i + 1;
                GFC.Visibility = true;
                IGFC.Add(GFC);
            }
        }
        BindGrid();
    }
    private void BindFields()
    {
        try
        {
            if (ddlcustomer.SelectedValue != "0")
            {
                string[] stringvalue = { "Qty Used", "Requester", "Project", "Status", "Notes" };//, "Record Number"
                IGFC = new InventoryRepository<GridFieldConfigurator>();
                var lcount = IGFC.GetAll().Where(o => o.CustomerId == int.Parse(ddlcustomer.SelectedValue)).ToList();
                if (lcount.Count == 0)
                {
                    for (int i = 0; i < stringvalue.Length; i++)
                    {
                        GFC = new GridFieldConfigurator();
                        GFC.CustomerId = int.Parse(ddlcustomer.SelectedValue);
                        GFC.DeafaultName = stringvalue[i];
                        GFC.DisplayName = stringvalue[i];
                        GFC.Position = i + 1;
                        IGFC.Add(GFC);
                    }
                }
                grid.Visible = true;
                BindGrid();
            }
            else
            {
                grid.Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
  
    protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grid.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grid.EditIndex = -1;
        BindGrid();
    }
    protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindGrid();
    }
    protected void grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grid.EditIndex = -1;
        BindGrid();
    }
    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            
            GFC = new GridFieldConfigurator();
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            IMUCD = new InventoryRepository<InventoryManager_UsageCustomData>();
            In_ucd=new List<InventoryManager_UsageCustomData>();
            BCD = new Inventory_BatchCustomData();
            IBCD = new InventoryRepository<Inventory_BatchCustomData>();
            BCDList = new List<Inventory_BatchCustomData>();

            int Cid = Convert.ToInt32(ddlcustomer.SelectedValue);
        
            if (e.CommandName == "Delete1")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                GFC = IGFC.GetAll().Where(o => o.id == id && o.CustomerId == sessionKeys.PortfolioID).FirstOrDefault();

                In_ucd = IMUCD.GetAll().Where(o => o.CustomFieldID == GFC.id).ToList();
                BCDList = IBCD.GetAll().Where(a => a.CustomFieldID == id).ToList();

                if (BCDList.Count != 0)
                {
                    IBCD.DeleteAll(BCDList);
                }
                if (In_ucd.Count != 0)
                {
                    IMUCD.DeleteAll(In_ucd);
                }
                IGFC.Delete(GFC);

                int DeletedPosition = (int)GFC.Position;
                IGFC.Delete(GFC);

                var Rlist = IGFC.GetAll().Where(o => o.Position > DeletedPosition && o.CustomerId == sessionKeys.PortfolioID).OrderBy(o => o.Position).ToList();
                foreach (var r in Rlist)
                {
                    GFC = IGFC.GetAll().Where(o => o.id == r.id && o.CustomerId == sessionKeys.PortfolioID).FirstOrDefault();
                    GFC.Position = DeletedPosition;
                    DeletedPosition = DeletedPosition + 1;
                    IGFC.Edit(GFC);
                }
                lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Deleted successfully";
                BindGrid();
            }
            if(e.CommandName == "Add")
            {
                string dname = string.Empty;
                string disname = string.Empty;
                if (((TextBox)grid.FooterRow.FindControl("txtDnamefooter")).Text.Trim() != "")
                {
                    dname = ((TextBox)grid.FooterRow.FindControl("txtDnamefooter")).Text.Trim();
                }
                if (((TextBox)grid.FooterRow.FindControl("txtDisnamefooter")).Text.Trim() != "")
                {
                    disname = ((TextBox)grid.FooterRow.FindControl("txtDisnamefooter")).Text.Trim();
                }

                if (!string.IsNullOrEmpty(dname))
                {
                    IGFC = new InventoryRepository<GridFieldConfigurator>();
                    int Cnt = IGFC.GetAll().Where(o => o.DisplayName == disname && o.CustomerId == Cid).Count();
                    int Cnt1 = IGFC.GetAll().Where(o => o.DeafaultName == dname && o.CustomerId == Cid).Count();
                    if (Cnt == 0 && Cnt1 == 0)
                    {
                        var gc = new GridFieldConfigurator();
                        gc.CustomerId = sessionKeys.PortfolioID;
                        gc.DeafaultName = dname;
                        gc.DisplayName = disname;
                        gc.Visibility = true;
                        gc.Position = IGFC.GetAll().Where(p => p.CustomerId == sessionKeys.PortfolioID).Select(p => p.Position).Max() + 1;

                        IGFC.Add(gc);
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        lblmsg.Text = "Added successfully";
                        BindGrid();
                    }
                    else
                    {
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Already record exist with this names";
                    }
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please enter default name";
                }

            }
            if (e.CommandName == "Update")
            {
                int i = grid.EditIndex;
                GridViewRow Row = grid.Rows[i];
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                TextBox DisName = (TextBox)Row.FindControl("txtName");
                GFC = IGFC.GetAll().Where(o => o.id == id && o.CustomerId == Cid).FirstOrDefault();
                if (GFC != null)
                {
                    GFC.DisplayName = DisName.Text;
                    IGFC.Edit(GFC);
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Display name updated successfully";
                    BindGrid();
                }
            }
            if (e.CommandName == "Up")
            {
                GFC = IGFC.GetAll().Where(o => o.CustomerId == Cid && o.id == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                if (GFC.Position != 1)
                {
                    int index = (int)GFC.Position;
                    GFC.Position = index - 1;
                    IGFC.Edit(GFC);
                    GFCList = IGFC.GetAll().Where(o => o.CustomerId == int.Parse(ddlcustomer.SelectedValue) && o.Position >= GFC.Position && o.id != Convert.ToInt32(e.CommandArgument.ToString())).OrderBy(o => o.Position).ToList();
                    int index1 = (int)GFC.Position;
                    foreach (GridFieldConfigurator item in GFCList)
                    {
                        index1 = index1 + 1;
                        item.Position = index1;
                        IGFC.Edit(item);
                    }
                    BindGrid();
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Order changed successfully.";
                }
            }
            if (e.CommandName == "Down")
            {
                GFC = IGFC.GetAll().Where(o => o.CustomerId == Cid && o.id == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                if (GFC.Position != 5)
                {
                    int index = (int)GFC.Position;
                    GFC.Position = index + 1;
                    IGFC.Edit(GFC);
                    GFCList = IGFC.GetAll().Where(o => o.CustomerId == int.Parse(ddlcustomer.SelectedValue) && o.Position <= GFC.Position && o.id != Convert.ToInt32(e.CommandArgument.ToString())).OrderByDescending(o => o.Position).ToList();
                    int index1 = (int)GFC.Position;
                    foreach (GridFieldConfigurator item in GFCList)
                    {
                        index1 = index1 - 1;
                        item.Position = index1;
                        IGFC.Edit(item);
                    }
                    BindGrid();
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Order changed successfully.";
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
    protected void grid_DataBound(object sender, EventArgs e)
    {
      
    }
    protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var cnt = e.Row.Cells.Count;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox ch = e.Row.FindControl("chkBox") as CheckBox;
            Label l1 = e.Row.FindControl("lblcheckBoxStatus") as Label;
            Label lblDname = (Label)e.Row.FindControl("lblDeafultName") as Label;
            LinkButton ib = (LinkButton)e.Row.FindControl("BtnDelete") as LinkButton;

            var strs = new string[] { "Qty Used", "Requester", "Project", "Status", "Notes" };
            if (lblDname != null)
            {
                if (strs.Contains(lblDname.Text))
                {
                    ib.Visible = false;
                }
            }
            if (l1 != null)
            {
                if (l1.Text == "False")
                {
                    ch.Checked = false;
                }
                else if (l1.Text == "True")
                {
                    ch.Checked = true;
                }
            }
        }
    }
    protected void btnUpdateVisibility_Click(object sender, EventArgs e)
    {
        try
        {
            IGFC = new InventoryRepository<GridFieldConfigurator>();
            GFC = new GridFieldConfigurator();
            foreach (GridViewRow row in grid.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox c1 = (row.Cells[3].FindControl("chkBox") as CheckBox);
                    int id = int.Parse((row.Cells[0].FindControl("lblid") as Label).Text);
                    GFC = IGFC.GetAll().Where(o => o.id == id).FirstOrDefault();
                    GFC.Visibility = c1.Checked;
                    IGFC.Edit(GFC);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
}