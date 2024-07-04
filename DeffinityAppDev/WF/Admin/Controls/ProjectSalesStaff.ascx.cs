using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using UserMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

public partial class ProjectSalesStaff : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropdown();
            BindGrid();
        }
    }
    private void BindDropdown()
    {
        try
        {
            using (UserDataContext db = new UserDataContext())
            {
                var activeUsersList = db.Contractors.Where(c => c.Status.ToLower() == "active" && (c.SID != 8 && c.SID !=10)).OrderBy(c => c.ContractorName).Select(c => c).ToList();
                ddlSalesStaff.DataSource = activeUsersList;
                ddlSalesStaff.DataValueField = "ID";
                ddlSalesStaff.DataTextField = "ContractorName";
                ddlSalesStaff.DataBind();

                ddlSalesStaff.Items.Insert(0, new ListItem("Please Select...", "0"));
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindGrid()
    {
        try
        {
            using (UserDataContext ud = new UserDataContext())
            {
                using (projectTaskDataContext pt = new projectTaskDataContext())
                {
                    var contractorsList = ud.Contractors.Where(c => c.Status.ToLower() == "active").ToList();
                    var salesStaffList = pt.ProjectsSalesStaffs.Select(p => p).ToList();
                    var result = (from p in contractorsList
                                  join s in salesStaffList on p.ID equals s.UserID orderby s.ID
                                  select new { ID = s.ID, SalesStaff = p.ContractorName }).ToList();

                    gvSalesStaff.DataSource = result;
                    gvSalesStaff.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_AddSite_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlSalesStaff.SelectedValue) > 0)
            {
                using (projectTaskDataContext db = new projectTaskDataContext())
                {
                    ProjectsSalesStaff projectsSalesStaff = db.ProjectsSalesStaffs.Where(p => p.UserID == int.Parse(ddlSalesStaff.SelectedValue)).Select(p => p).FirstOrDefault();
                    if (projectsSalesStaff == null)
                    {
                        ProjectsSalesStaff salesStaff = new ProjectsSalesStaff();
                        salesStaff.UserID = int.Parse(ddlSalesStaff.SelectedValue);
                        db.ProjectsSalesStaffs.InsertOnSubmit(salesStaff);
                        db.SubmitChanges();
                        lblmsg.Text = "Added successfully.";
                        lblmsg.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        lblmsg.Text = "User already exists. Please check and try again.";
                        lblmsg.ForeColor = System.Drawing.Color.Red;

                    }
                    BindGrid();
                }
            }
            else {
                lblmsg.Text = "Please select User.";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvSalesStaff_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSalesStaff.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gvSalesStaff_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Deleterow")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                using (projectTaskDataContext db = new projectTaskDataContext())
                {
                    ProjectsSalesStaff projectsSalesStaff = db.ProjectsSalesStaffs.Where(p => p.ID == id).Select(p => p).FirstOrDefault();
                    if (projectsSalesStaff != null)
                    {
                        db.ProjectsSalesStaffs.DeleteOnSubmit(projectsSalesStaff);
                        db.SubmitChanges();
                        BindGrid();
                        lblmsg.Text = "Deleted successfully.";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvSalesStaff_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}