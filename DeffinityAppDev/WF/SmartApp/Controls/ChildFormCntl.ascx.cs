using HealthCheckMgt.DAL;
using HealthCheckMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;

public partial class App_Controls_ChildFormCntl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["AppFormID"] != null)
            {
                BindGrid(int.Parse(Request.QueryString["AppFormID"]));
            }
            PermissionsChecking();
        }
    }
    public int PermissionsChecking()
    {
        int i = 0;
        using (HealthCheckDataContext Ddc = new HealthCheckDataContext())
        {
            App_Permission x = Ddc.App_Permissions.Where(a => a.UserId == sessionKeys.UID).FirstOrDefault();
            if (x != null)
            {
                if (x.PermissionId == 1)//Administrator
                {
                    i = 1;
                }
                else if (x.PermissionId == 2)//Read Only
                {
                    btnCreateNewEntry.Enabled = false;
                    i = 2;
                }
                else if (x.PermissionId == 3)//Hide
                {
                    i = 3;
                }
                else if (x.PermissionId == 4)//Manage
                {
                    i = 4;
                }
            }
        }
        return i;
    }
    public void BindGrid(int Formid)
    {
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                using (UserDataContext Udc = new UserDataContext())
                {
                    var AssingedFormAppId = Hdc.AppManagerAssignedForms.Where(a => a.ID == Formid).Select(a => a.AppID).FirstOrDefault();

                    var app = Hdc.AppManagers.Where(o => o.ID == AssingedFormAppId).FirstOrDefault();
                    lblName.Text = Hdc.AppManagerAssignedForms.Where(a => a.ID == Formid).Select(a => a.FormName).FirstOrDefault();
                    var AppList = Hdc.AppManagerAssignedForms.Where(a => a.AppID == AssingedFormAppId && a.Form_Type == "Child" && a.ParentFormId == int.Parse(Request.QueryString["AppFormID"])).ToList();
                    var Clist = Udc.Contractors.Where(a => a.Status == "Active" && AppList.Select(o => o.CreatedBy.HasValue ? o.CreatedBy.Value : 0).ToArray().Contains(a.ID)).ToList();
                    var xy = (from a in AppList
                              join b in Clist on a.CreatedBy.Value equals b.ID
                              select new
                              {
                                  ID = a.ID,
                                  FormName = a.FormName,
                                  CreatedBy = b.ContractorName,
                                  CreatedDate = a.CreatedDate.Value.ToShortDateString(),
                                  Notes = a.Notes
                              }).ToList();

                    GridCreatedEntries.DataSource = xy;
                    GridCreatedEntries.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridCreatedEntries_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int FormId = 0;
            if (Request.QueryString["AppFormID"] != null)
            {
                FormId = int.Parse(Request.QueryString["AppFormID"]);
            }
            if (e.CommandName == "Edit")
            {
                Response.Redirect("~/App/AppChildForm.aspx?AppFormID=" + e.CommandArgument.ToString(), false);
            }
            if (e.CommandName == "Delete")
            {
                using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
                {
                    AppManagerAssignedForm App_m = Hdc.AppManagerAssignedForms.Where(a => a.ID == int.Parse(e.CommandArgument.ToString())).FirstOrDefault();
                    Hdc.AppManagerAssignedForms.DeleteOnSubmit(App_m);
                    Hdc.SubmitChanges();
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Deleted successfully";
                    BindGrid(FormId);//  BindGrid(int.Parse(Request.QueryString["AppFormID"]));
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridCreatedEntries_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridCreatedEntries_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void btnCreateNewEntry_Click(object sender, EventArgs e)
    {
        try
        {
            int AppId = 0;
           
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                if (Request.QueryString["AppFormID"] != null)
                {
                    AppId = Hdc.AppManagerAssignedForms.Where(a => a.ID == int.Parse(Request.QueryString["AppFormID"])).Select(a => a.AppID.Value).FirstOrDefault();
                }

                AppManagerAssignedForm App_m = new AppManagerAssignedForm();
                var isexists = Hdc.AppManagerAssignedForms.Where(o => o.FormName.ToLower().Trim() == txtChildName.Text.ToLower().Trim()
                    && o.AppID == AppId && o.Form_Type.ToLower() == "child" && o.ParentFormId == int.Parse(Request.QueryString["AppFormID"])).FirstOrDefault();
                if (isexists == null)
                {
                    App_m.AppID = AppId;
                    App_m.FormName = txtChildName.Text.Trim();
                    App_m.Notes = TxtNotes.Text.Trim();
                    App_m.CreatedDate = DateTime.Now;
                    App_m.CreatedBy = sessionKeys.UID;
                    App_m.ModifiedBy = sessionKeys.UID;
                    App_m.ModifiedDate = DateTime.Now;
                    App_m.Form_Type = "Child";
                    App_m.ParentFormId = int.Parse(Request.QueryString["AppFormID"]);
                    Hdc.AppManagerAssignedForms.InsertOnSubmit(App_m);
                    Hdc.SubmitChanges();
                    Response.Redirect(string.Format("~/App/AppChildForm.aspx?appformid={0}", App_m.ID), false);
                    //BindGrid(AppId);
                    //txtFormName.Text = string.Empty;
                    //txtNotes.Text = string.Empty;
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Form name already exists";
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridCreatedEntries_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgDelete = ((ImageButton)e.Row.FindControl("btnDelete"));
               // ImageButton btnEdit = ((ImageButton)e.Row.FindControl("btnEdit"));

                int i = PermissionsChecking();
                if (i == 2)
                {
                    imgDelete.Enabled = false;
                 //   btnEdit.Enabled = false;
                }
                else if (i == 3)
                {

                }
                else if (i == 4)
                {
                    imgDelete.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}