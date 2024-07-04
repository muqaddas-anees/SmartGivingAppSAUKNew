using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthCheckMgt.Entity;
using HealthCheckMgt.DAL;
using UserMgt.DAL;
using System.IO;

public partial class App_App_Manager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            Binddl();
            BindGrid();
            BinChildddl();
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
                    BtnCreateApp.Enabled = false;
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
    protected string GetCssClass(int t)
    {
        string CssClass = string.Empty;

        if (t == 0)
        {
            CssClass = "current1";
        }
        return CssClass;
    }
    public void BindGrid()
    {
        try 
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                using (UserDataContext Udc = new UserDataContext())
                {
                    var Clist = Udc.Contractors.Where(a => a.Status == "Active").ToList();
                    var AppList = Hdc.AppManagers.ToList();
                    var FormsList = Hdc.HealthCheck_Forms.Where(a => AppList.Select(o => o.FormID).Contains(a.FormID)).ToList();
                    var FormsListForChild = Hdc.HealthCheck_Forms.ToList();
                    var Gridlist = (from a in AppList
                                    orderby a.ID descending
                                    select new
                                    {
                                        ID = a.ID,
                                        AppName = a.Name,
                                        Notes = a.Description,
                                        Type = a.Type != null? a.Type.ToString():string.Empty,// == "ParentandChild" ? "Parent and Child" : "Flat file",
                                        ChildFormName = FormsListForChild.Where(o => o.FormID == (a.ChildFormId.HasValue ? a.ChildFormId.Value : 0)).Select(o => o.FormName).FirstOrDefault(),
                                        FormName = FormsList.Where(o => o.FormID == a.FormID).Select(o => o.FormName).FirstOrDefault()
                                    }).ToList();
                    //if (lbltype.Text != string.Empty)
                    //{
                    //    Gridlist = Gridlist.Where(a => a.Type.ToLower() == lbltype.Text.ToLower()).ToList();
                    //}
                    GridApps.DataSource = Gridlist;
                    GridApps.DataBind();
                    if (lbltype.Text != string.Empty)
                    {
                        if (lbltype.Text != "ParentandChild")
                        {
                            GridApps.Columns[5].Visible = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BinChildddl()
    {
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                var x = Hdc.HealthCheck_Forms.OrderBy(a => a.FormName).ToList();
                ddlChilfForm.DataSource = x;
                ddlChilfForm.DataValueField = "FormID";
                ddlChilfForm.DataTextField = "FormName";
                ddlChilfForm.DataBind();
                ddlChilfForm.Items.Insert(0, new ListItem("Please select..", "0"));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void Binddl()
    {
        try 
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                var x = Hdc.HealthCheck_Forms.OrderBy(a => a.FormName).ToList();
                ddlForms.DataSource = x;
                ddlForms.DataValueField = "FormID";
                ddlForms.DataTextField = "FormName";
                ddlForms.DataBind();
                ddlForms.Items.Insert(0, new ListItem("Please select..", "0"));
            }
        }  
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void FileUpload(string Id)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                string filePath = "~/UploadData/App/";
                if (!Directory.Exists(Server.MapPath(filePath)))
                {
                    Directory.CreateDirectory(Server.MapPath(filePath));
                }
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/UploadData/App/"), "App_" + Id + ".*");
                var filename = filePaths.FirstOrDefault();
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                FileUpload1.PostedFile.SaveAs(Server.MapPath(filePath) + "App_" + Id.ToString() + System.IO.Path.GetExtension(FileUpload1.FileName));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BtnCreateApp_Click(object sender, EventArgs e)
    {
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                if (lblId.Text == string.Empty)
                {
                    AppManager AppM = new AppManager();
                    AppM.Name = txtAppName.Text.Trim();
                    AppM.Description = txtapplicationdo.Text.Trim();
                    AppM.FormID = int.Parse(ddlForms.SelectedValue);
                    AppM.Icon = GetSelectedControlID().ToString().Replace('_','-');

                    if (lbltype.Text == "ParentandChild")
                    {
                        AppM.ChildFormId = int.Parse(ddlChilfForm.SelectedValue);
                        AppM.Type = lbltype.Text;
                    }
                    else
                    {
                        AppM.Type = lbltype.Text;
                    }
                    AppM.EngineerView = chkEngineer.Checked;
                    AppM.CustomerView = chkCustomer.Checked;
                    Hdc.AppManagers.InsertOnSubmit(AppM);
                    Hdc.SubmitChanges();
                    FileUpload(AppM.ID.ToString());
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Added successfully";
                }
                else
                {
                    AppManager App_m = Hdc.AppManagers.Where(a => a.ID == int.Parse(lblId.Text)).FirstOrDefault();
                    App_m.Name = txtAppName.Text.Trim();
                    App_m.Description = txtapplicationdo.Text.Trim();
                    App_m.FormID = int.Parse(ddlForms.SelectedValue);
                    App_m.Icon = GetSelectedControlID().ToString().Replace('_', '-');
                    if (App_m.Type == "ParentandChild")
                    {
                        App_m.ChildFormId = int.Parse(ddlChilfForm.SelectedValue);
                    }
                    App_m.EngineerView = chkEngineer.Checked;
                    App_m.CustomerView = chkCustomer.Checked;
                    Hdc.SubmitChanges();
                    FileUpload(App_m.ID.ToString());
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Updated successfully";
                    
                }
                //change panel visibility
                PanelVisibility(true, false, false);
                BindGrid();
                Binddl();
                ClearValues();
                ClearOptions();
                //   Response.Redirect("App_ManagerAssignedForm.aspx?App=" + AppM.ID.ToString());
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void ClearValues()
    {
        txtAppName.Text = string.Empty;
        txtapplicationdo.Text = string.Empty;
        ddlForms.SelectedValue = "0";
        ddlChilfForm.SelectedValue = "0";
        lblId.Text = string.Empty;
        chkCustomer.Checked = false;
        chkEngineer.Checked = false;
        SetSelectedControlID(string.Empty);
    }
    private void ClearOptions()
    {
        CheckboxFlatFileStructure.Checked = false;
        CheckBoxParentAndChildRelationShip.Checked = false;
    }
    public void DeleteFile(string Id)
    {
        try
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/UploadData/App/"), "App_" + Id + ".*");
            var filename = filePaths.FirstOrDefault();
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void DownloadFile(string Id)
    {
        string[] filePaths = Directory.GetFiles(Server.MapPath("~/UploadData/App/"), "App_" + Id + ".*");

        Response.ContentType = ContentType;
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + "File_" + Path.GetFileName(filePaths.FirstOrDefault()));
        Response.ContentType = "application/octet-stream";
        Response.WriteFile(filePaths.FirstOrDefault());
        Response.End();
    }
    protected void GridApps_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try 
        {
            if (e.CommandName == "Delete")
            {
                using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
                {
                    var x = Hdc.AppManagerAssignedForms.Where(a => a.AppID == int.Parse(e.CommandArgument.ToString())).ToList();
                    if (x.Count == 0)
                    {
                        AppManager App_m = Hdc.AppManagers.Where(a => a.ID == int.Parse(e.CommandArgument.ToString())).FirstOrDefault();
                        Hdc.AppManagers.DeleteOnSubmit(App_m);
                        Hdc.SubmitChanges();
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = "Deleted successfully";
                        BindGrid();
                    }
                    else
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = "Unable to delete this application until all records within it have been deleted";
                        BindGrid();
                    }
                }
            }
            if (e.CommandName == "DeleteFile")
            {
                DeleteFile(e.CommandArgument.ToString());
                BindGrid();
            }
            if (e.CommandName == "Download")
            {
                DownloadFile(e.CommandArgument.ToString());
                BindGrid();
            }
            if (e.CommandName == "Edit")
            {
                //update panle
                PanelVisibility(false, false, true);
                BtnCreateApp.Text = "Update";
                BtnBack.Visible = false;
                using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
                {
                    AppManager App_m = Hdc.AppManagers.Where(a => a.ID == int.Parse(e.CommandArgument.ToString())).FirstOrDefault();
                    txtapplicationdo.Text = App_m.Description;
                    txtAppName.Text = App_m.Name;
                    ddlForms.SelectedValue = App_m.FormID.HasValue ? App_m.FormID.Value.ToString() : "0";
                    if (App_m.Type == "ParentandChild")
                    {
                        ddlChilfForm.SelectedValue = App_m.ChildFormId.HasValue ? App_m.ChildFormId.Value.ToString() : "0";
                    }
                    ChildFormddl.Visible = true;
                    lblId.Text = App_m.ID.ToString();
                    SetSelectedControlID(App_m.Icon.Replace('-','_'));
                    lbltype.Text = App_m.Type;
                    chkEngineer.Checked = App_m.EngineerView.HasValue ? App_m.EngineerView.Value : false;
                    chkCustomer.Checked = App_m.CustomerView.HasValue ? App_m.CustomerView.Value : false;
                    //DivAppOptions.Visible = false;
                    
                    //DivAppCreating.Visible = true;
                    //if (App_m.Type != "Flatfile")
                    //{
                    //    ChildFormddl.Visible = false;
                    //    lbltype.Text = "Flatfile";
                    //}
                    //BindGrid();
                    lblStep2.Text = "Update app details:";
                }
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridApps_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridApps_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    public string GetSelectedControlID()
    {
        string retval = string.Empty;
        foreach (Control ctrl in PnlIcons.Controls)
        {
            var chk = ctrl as CheckBox;
            if (chk != null)
            {
                if (chk.Checked)
                    retval = chk.ID.ToString();
            }
        }
        return retval;
    }

    public void SetSelectedControlID(string controlid)
    {
        foreach (Control ctrl in PnlIcons.Controls)
        {
            var chk = ctrl as CheckBox;
            if (chk != null)
            {
                if (string.IsNullOrEmpty(controlid))
                {
                    chk.Checked = false;
                }
                else if (chk.ID.ToString().ToLower() == controlid.ToLower())
                {
                    chk.Checked = true;
                }
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //BtnCreateApp.Text = "Create App";
        ClearValues();
        ClearOptions();
        //Panel visibility
        PanelVisibility(true, false, false);
    }
    protected void BtnNext_Click(object sender, EventArgs e)
    {
        try 
        {
           
            if (CheckboxFlatFileStructure.Checked == true || CheckBoxParentAndChildRelationShip.Checked == true)
            {
                ChildFormddl.Visible = true;
                lbltype.Text = "ParentandChild";
                //DivAppOptions.Visible = false;
                BtnBack.Visible = true;
                //DivAppCreating.Visible = true;
                if (CheckboxFlatFileStructure.Checked == true)
                {
                    ChildFormddl.Visible = false;
                    lbltype.Text = "Flatfile";
                }
                //BindGrid();
                lblStep2.Text = "App details:";
                BtnCreateApp.Text = "Create App";
                //panel visibility
                PanelVisibility(false, false, true);
            }
            else
            {
                lblOptionsMsg.ForeColor = System.Drawing.Color.Red;
                lblOptionsMsg.Text = "Please select one option";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        try
        {
            //DivAppOptions.Visible = true;
            //DivAppCreating.Visible = false;
            //BtnBack.Visible = false;
            lbltype.Text = string.Empty;
            //BindGrid();
            //clera the details values
            ClearValues();
            ClearOptions();
            //change panel visibility
            PanelVisibility(false, true, false);
            //lblStep1.Text = "Is this app:";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridApps_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton LnlDownloadFile = (LinkButton)e.Row.FindControl("LnlDownloadFile");
                LinkButton LnlDeletefile = (LinkButton)e.Row.FindControl("LnlDeletefile");
                Label lblCidForFile = (Label)e.Row.FindControl("lblAppId");
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/WF/UploadData/App/"), "App_" + lblCidForFile.Text + ".*");
                var filename = filePaths.FirstOrDefault();
                if (File.Exists(filename))
                {
                    LnlDeletefile.Visible = true;
                    LnlDownloadFile.Visible = true;
                }
                else
                {
                    LnlDeletefile.Visible = false;
                    LnlDownloadFile.Visible = false;
                }
                LinkButton imgDelete = ((LinkButton)e.Row.FindControl("btnDelete"));
                //ImageButton btnEdit = ((ImageButton)e.Row.FindControl("btnEdit"));
                int i = PermissionsChecking();
                if (i == 2)
                {
                    imgDelete.Enabled = false;
                  //  btnEdit.Enabled = false;
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
    protected void btnCreateappMain_Click(object sender, EventArgs e)
    {
        PanelVisibility(false, true, false);
    }

    public void PanelVisibility(bool appList,bool appOptions,bool appDetails)
    {
        pnlApplist.Visible = appList;
        DivAppOptions.Visible = appOptions;
        DivAppDetails.Visible = appDetails;
    }
    protected void btnCancelStep1_Click(object sender, EventArgs e)
    {
        //clear options
        ClearOptions();
        //Panel options
        PanelVisibility(true, false, false);
    }
}