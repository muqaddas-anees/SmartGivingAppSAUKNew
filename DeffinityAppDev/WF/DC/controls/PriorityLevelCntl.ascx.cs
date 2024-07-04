using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.DAL;
using DC.Entity;

public partial class DC_controls_PriorityLevelCntl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPriorityLevelGrid();
        }
    }
    public void BindPriorityLevelGrid()
    {
        try
        {
            using (DCDataContext Dcd = new DCDataContext())
            {
                var tList = Dcd.PriorityLevels.Where(o => o.CustomerID == sessionKeys.PortfolioID).ToList();
                if (tList.Count == 0)
                {
                    tList.Add(new PriorityLevel() { Id = -99, Value = string.Empty, Description = string.Empty });
                }
                gridPriorityLevel.DataSource = tList.ToList();
                gridPriorityLevel.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridPriorityLevel_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridPriorityLevel.EditIndex = -1;
        BindPriorityLevelGrid();
    }
    protected void gridPriorityLevel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Add")
            {
                string Cname = string.Empty;
                string Des = ((TextBox)gridPriorityLevel.FooterRow.FindControl("txtFooterDescription")).Text.Trim();
                if (((TextBox)gridPriorityLevel.FooterRow.FindControl("txtFooterPname")).Text.Trim() != "" && Des != "")
                {
                    Cname = ((TextBox)gridPriorityLevel.FooterRow.FindControl("txtFooterPname")).Text.Trim();



                    using (DCDataContext Dccontext = new DCDataContext())
                    {
                        var checking = Dccontext.PriorityLevels.Where(a => a.Value.ToLower() == Cname.ToLower() && a.CustomerID == sessionKeys.PortfolioID).ToList();
                        if (checking.Count == 0)
                        {
                            PriorityLevel in_c = new PriorityLevel();
                            in_c.Value = Cname;
                            in_c.Description = Des;
                            in_c.CustomerID = sessionKeys.PortfolioID;
                            Dccontext.PriorityLevels.InsertOnSubmit(in_c);
                            Dccontext.SubmitChanges();
                            //lblmsg.ForeColor = System.Drawing.Color.Green;
                            lblmsg.Text = "Added successfully";
                            BindPriorityLevelGrid();
                        }
                        else
                        {
                            //already exist
                            //lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblEror.Text = "Priority name already exists";
                        }
                    }
                }
                else
                {
                    //please enter condition
                    //lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblEror.Text = "Please enter name and description";
                }
            }
            else if (e.CommandName == "Update")
            {
                int i = gridPriorityLevel.EditIndex;
                GridViewRow row = gridPriorityLevel.Rows[i];
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                TextBox txtPName = (TextBox)row.FindControl("txtPName");
                TextBox txtDescription = (TextBox)row.FindControl("txtDescription");

                if (txtPName.Text.Trim() != string.Empty && txtDescription.Text.Trim() != string.Empty)
                {
                    using (DCDataContext Dccontext = new DCDataContext())
                    {
                        var checking = Dccontext.PriorityLevels.Where(a => a.Value.ToLower() == txtPName.Text.ToLower()).ToList();
                        if (checking.Count == 1)
                        {
                            PriorityLevel In_c = Dccontext.PriorityLevels.Where(a => a.Id == id).FirstOrDefault();
                            In_c.Value = txtPName.Text.Trim();
                            In_c.Description = txtDescription.Text.Trim();
                            Dccontext.SubmitChanges();
                            //lblmsg.ForeColor = System.Drawing.Color.Green;
                            lblmsg.Text = "Updated successfully";
                            BindPriorityLevelGrid();
                        }
                        else
                        {
                            //lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblEror.Text = "Priority name already exists";
                        }
                    }
                }
                else
                {
                    //lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblEror.Text = "Please enter name and description";
                }
            }
            else if (e.CommandName == "Delete1")
            {
                int Priorityid = Convert.ToInt32(e.CommandArgument.ToString());
                using (DCDataContext Dcd = new DCDataContext())
                {
                    var checkingcount = Dcd.FLSDetails.Where(a => a.ID == Priorityid).ToList();
                    if (checkingcount.Count == 0)
                    {
                        PriorityLevel In_c = Dcd.PriorityLevels.Where(a => a.Id == Priorityid).FirstOrDefault();
                        Dcd.PriorityLevels.DeleteOnSubmit(In_c);
                        Dcd.SubmitChanges();
                        //lblmsg.ForeColor = System.Drawing.Color.Green;
                        lblmsg.Text = "Deleted successfully";
                        BindPriorityLevelGrid();
                    }
                    else
                    {
                        //lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblEror.Text = "Unable to delete this record";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridPriorityLevel_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void gridPriorityLevel_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridPriorityLevel.EditIndex = e.NewEditIndex;
        BindPriorityLevelGrid();
    }
    protected void gridPriorityLevel_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gridPriorityLevel.EditIndex = -1;
        BindPriorityLevelGrid();
    }
}