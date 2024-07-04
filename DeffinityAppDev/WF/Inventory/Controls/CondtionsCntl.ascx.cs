using InventoryMgt.DAL;
using InventoryMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_CondtionsCntl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCondtions();
        }
    }
    public void BindCondtions()
    {
        try
        {
            using (InventoryDataContext Idcontext = new InventoryDataContext())
            {
                var tList = Idcontext.Inventory_Condtions.ToList();
                if (tList.Count ==0)
                {
                    tList.Add(new Inventory_Condtion(){ id=-99, Condition=string.Empty});
                }
                gridConditions.DataSource = tList;
                gridConditions.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridConditions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Add")
            {
                string Cname = string.Empty;
                if (((TextBox)gridConditions.FooterRow.FindControl("txtFooterCname")).Text.Trim() != "")
                {
                    Cname = ((TextBox)gridConditions.FooterRow.FindControl("txtFooterCname")).Text.Trim();
                    using (InventoryDataContext Idcontext = new InventoryDataContext())
                    {
                        var checking = Idcontext.Inventory_Condtions.Where(a => a.Condition.ToLower() == Cname.ToLower()).ToList();
                        if (checking.Count == 0)
                        {
                            Inventory_Condtion in_c = new Inventory_Condtion();
                            in_c.Condition = Cname;
                            Idcontext.Inventory_Condtions.InsertOnSubmit(in_c);
                            Idcontext.SubmitChanges();
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            lblmsg.Text = "Inserted successfully";
                            BindCondtions();
                        }
                        else
                        {
                            //already exist
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Already exist with this name";
                        }
                    }
                }
                else
                {
                    //please enter condition
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please enter condition";
                }
            }
            else if (e.CommandName == "Edit")
            { }
            else if (e.CommandName == "Update")
            {
                int i = gridConditions.EditIndex;
                GridViewRow row = gridConditions.Rows[i];
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                TextBox txtcName = (TextBox)row.FindControl("txtCName");
                if (txtcName.Text.Trim() != string.Empty)
                {
                    using (InventoryDataContext Idcontext = new InventoryDataContext())
                    {
                        var checking = Idcontext.Inventory_Condtions.Where(a => a.Condition.ToLower() == txtcName.Text.ToLower()).ToList();
                        if (checking.Count == 0)
                        {
                            Inventory_Condtion In_c = Idcontext.Inventory_Condtions.Where(a => a.id == id).FirstOrDefault();
                            In_c.Condition = txtcName.Text.Trim();
                            Idcontext.SubmitChanges();
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            lblmsg.Text = "Updated successfully";
                            BindCondtions();
                        }
                        else
                        {
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Already exist with this name";
                        }
                    }
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please enter condition";
                }
            }
            else if (e.CommandName == "Delete1")
            {
                int Conditionid = Convert.ToInt32(e.CommandArgument.ToString());
                using (InventoryDataContext Idcontext = new InventoryDataContext())
                {
                    var checkingcount = Idcontext.InventoryManager_PSDProducts.Where(a => a.ConditionId == Conditionid).ToList();
                    if (checkingcount.Count == 0)
                    {
                        Inventory_Condtion In_c = Idcontext.Inventory_Condtions.Where(a => a.id == Conditionid).FirstOrDefault();
                        Idcontext.Inventory_Condtions.DeleteOnSubmit(In_c);
                        Idcontext.SubmitChanges();
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Deleted successfully";
                        BindCondtions();
                    }
                    else
                    {
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Unable to delete this record.";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridConditions_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridConditions.EditIndex = e.NewEditIndex;
        BindCondtions();
    }
    protected void gridConditions_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gridConditions.EditIndex = -1;
        BindCondtions();
    }
    protected void gridConditions_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridConditions.EditIndex = -1;
        BindCondtions();
    }
    protected void gridConditions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblid = e.Row.FindControl("lblid") as Label;
            if (lblid != null)
            {
                if (lblid.Text == "-99")
                    e.Row.Visible = false;
            }
        }
    }
}