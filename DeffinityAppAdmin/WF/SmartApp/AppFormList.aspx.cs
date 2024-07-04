using HealthCheckMgt.DAL;
using HealthCheckMgt.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;

public partial class App_App_ManagerAssignedForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["appid"] != null)
            {
                int AppId = int.Parse(Request.QueryString["appid"]);
                IntialInsert(AppId);
                SetDefaultFormname(AppId);
                //  BindGrid(AppId);
                BindCustomGrid(AppId);
                //Master.PageHead = form_lable.Text;
                PermissionsChecking();
                ColumnsReOrder();
            }
            else
            {
                Response.Redirect("");
            }
        }
        else
        {
            BindCustomGrid(int.Parse(Request.QueryString["appid"]));
        }
    }
    public void ColumnsReOrder()
    {

    }
    public void IntialInsert(int AppId)
    {
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                if (Hdc.gridColumnsVisibilities.Where(a => a.Appid == AppId).ToList().Count == 0)
                {
                    List<gridColumnsVisibility> Tlist = new List<gridColumnsVisibility>();
                    gridColumnsVisibility gcv = null;
                    int ParentFormID = Hdc.AppManagers.Where(a => a.ID == AppId).Select(a => a.FormID.Value).First();
                    var PanelsList = Hdc.HealthCheck_FormPanels.Where(a => a.FormID == ParentFormID && a.PanelName != "Header" && a.PanelName != "Signature Panel").ToList();
                    foreach (var Pnl in PanelsList)
                    {
                        var FormControlsList = Hdc.HealthCheck_FormControls.Where(a => a.PanelID == Pnl.PanelID).ToList();
                        foreach (var Formcntl in FormControlsList)
                        {
                            if (Formcntl.TypeOfField != "Image")
                            {
                                gcv = new gridColumnsVisibility();
                                gcv.Appid = AppId;
                                gcv.ColumnId = Formcntl.ControlID;
                                gcv.Visibility = true;
                                Tlist.Add(gcv);
                            }
                        }
                    }
                    Hdc.gridColumnsVisibilities.InsertAllOnSubmit(Tlist);
                    Hdc.SubmitChanges();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void HideColumnIngrid()
    {
      
    }
    private void SetDefaultFormname(int AppId)
    {
        try
        {
            using (HealthCheckDataContext Ddc = new HealthCheckDataContext())
            {
                var formCount = Ddc.AppManagerAssignedForms.Where(o => o.AppID == AppId).Count();
                if (formCount != null)
                {
                    txtFormName.Text = (formCount + 1).ToString();
                }
                else
                    txtFormName.Text = (1).ToString();
            }
        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    public int PermissionsChecking()
    {
        int i = 0;
        using (HealthCheckDataContext Ddc = new HealthCheckDataContext())
        {
            int appid = 0;
            if (Request.QueryString["appid"] != null)
            {
                appid = Convert.ToInt32(Request.QueryString["appid"]);
            }
            App_Permission x = Ddc.App_Permissions.Where(a => a.UserId == sessionKeys.UID && a.AppId == appid).FirstOrDefault();
            if (x == null)
            {
                x = Ddc.App_Permissions.Where(a => a.UserId == sessionKeys.UID).FirstOrDefault();
            }
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
    public void BindCustomGrid(int appid)
    {
        form_subname.Text = "Record";
        
        List<UserMgt.Entity.Contractor> Clist = new List<UserMgt.Entity.Contractor>();
        try
        {
            using (UserDataContext Udc = new UserDataContext())
            {
                Clist = Udc.Contractors.ToList();
            }
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                int ParentFormID = Hdc.AppManagers.Where(a => a.ID == appid).Select(a => a.FormID.Value).First();

                var app = Hdc.AppManagers.Where(o => o.ID == appid).FirstOrDefault();
                form_lable.Text = app.Name;

                var ParentRecordsList = Hdc.AppManagerAssignedForms.Where(a => a.AppID == appid && a.Form_Type == "Parent").ToList();
                if (ParentRecordsList.Count != 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(string));
                    dt.Columns.Add("Record", typeof(string));
                    dt.Columns.Add("Created Date", typeof(string));
                    dt.Columns.Add("Created By", typeof(string));
                    dt.Columns.Add("Notes", typeof(string));

                    //  int AppID = childRecordsList.FirstOrDefault().AppID.Value;
                    //  int childFormId = Hdc.AppManagers.Where(a => a.ID == AppID).FirstOrDefault().ChildFormId.Value;

                    var VisibleColumns = Hdc.gridColumnsVisibilities.Where(a => a.Appid == appid && a.Visibility == true).Select(a=>a.ColumnId.Value).ToArray();
                    var PanelsList = Hdc.HealthCheck_FormPanels.Where(a => a.FormID == ParentFormID && a.PanelName != "Header" && a.PanelName != "Signature Panel").ToList();
                    foreach (var Pnl in PanelsList)
                    {
                        var FormControlsList = Hdc.HealthCheck_FormControls.Where(a => a.PanelID == Pnl.PanelID).ToList();
                        foreach (var Formcntl in FormControlsList)
                        {
                            if (VisibleColumns.Contains(Formcntl.ControlID))
                            {
                                if (Formcntl.TypeOfField != "Image")
                                {
                                    dt.Columns.Add(Formcntl.ControlLabelName, typeof(string));
                                }
                            }
                        }
                    }
                    dt.Columns.Add(" ", typeof(string));
                    DataRow datarw;
                    foreach (var d in ParentRecordsList)
                    {
                        datarw = dt.NewRow();
                        int i = 0;
                        if (PanelsList.Count == 0)
                        {
                            datarw[0] = d.ID;
                            datarw[1] = d.FormName;
                            datarw[2] = string.Format("{0:d}", d.CreatedDate.HasValue ? d.CreatedDate.Value : Convert.ToDateTime("01/01/1900"));
                            datarw[3] = Clist.Where(a => a.ID == d.CreatedBy).FirstOrDefault().ContractorName;
                            datarw[4] = Clist.Where(a => a.ID == d.ModifiedBy).FirstOrDefault().ContractorName;
                        }
                        else
                        {
                            if (i == 0)
                            {
                                datarw[0] = d.ID;
                                datarw[1] = d.FormName;
                                datarw[2] = string.Format("{0:d}", d.CreatedDate.HasValue ? d.CreatedDate.Value : Convert.ToDateTime("01/01/1900"));
                                datarw[3] = Clist.Where(a => a.ID == d.CreatedBy).FirstOrDefault().ContractorName;
                                datarw[4] = Clist.Where(a => a.ID == d.ModifiedBy).FirstOrDefault().ContractorName;
                                i = 5;
                            }
                            if (i > 0)
                            {
                                if (PanelsList != null)
                                {
                                    foreach (var l in PanelsList)
                                    {
                                        var Cntllist = Hdc.HealthCheck_FormControls.Where(a => a.PanelID == l.PanelID).ToList();

                                        foreach (var c in Cntllist)
                                        {
                                            if (VisibleColumns.Contains(c.ControlID))
                                            {
                                                if (c.TypeOfField != "Image")
                                                {
                                                    var cuslist = Hdc.HealthCheck_FormDatas.Where(a => a.HealthCheckID == d.ID && a.ControlID == c.ControlID && a.Section == "app").FirstOrDefault();
                                                    if (cuslist != null)
                                                    {
                                                        if (!string.IsNullOrEmpty(cuslist.ControlValue))
                                                        {
                                                            if (cuslist.ControlValue.Contains('$'))
                                                            {
                                                                int ij = cuslist.ControlValue.LastIndexOf('$');
                                                                datarw[i] = cuslist.ControlValue.Substring(0, ij);
                                                            }
                                                            else
                                                            {
                                                                datarw[i] = cuslist.ControlValue;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            datarw[i] = string.Empty;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        datarw[i] = string.Empty;
                                                    }
                                                    i++;
                                                }
                                            }
                                        }
                                     //   datarw[i] = "Delete";
                                        if (PanelsList.Count == 1)
                                        {
                                            dt.Rows.Add(datarw);
                                        }
                                    }
                                }
                                else
                                {
                                    datarw[i] = string.Empty;
                                }
                            }
                            //  i++;
                        }
                        if (PanelsList.Count != 1)
                        {
                            dt.Rows.Add(datarw);
                        }
                    }
                    GridCreatedEntries.DataSource = dt;
                    GridCreatedEntries.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
    public void BindGrid(int appid)
    {
        try 
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                using (UserDataContext Udc = new UserDataContext())
                {
                    var app = Hdc.AppManagers.Where(o => o.ID == appid).FirstOrDefault();
                    form_lable.Text = app.Name;
                    //form_subname.Text = app.Name;
                    form_subname.Text = "Record";
                    var AppList = Hdc.AppManagerAssignedForms.Where(a => a.AppID == appid && a.Form_Type == "Parent").ToList();
                    var Clist = Udc.Contractors.Where(a => a.Status == "Active" && AppList.Select(o=>o.CreatedBy.HasValue?o.CreatedBy.Value:0).ToArray().Contains(a.ID)).ToList();
                    var xy = (from a in AppList
                              join b in Clist on a.CreatedBy.Value equals b.ID
                              orderby a.CreatedDate descending
                              select new
                              {
                                  ID = a.ID,
                                  FormName = a.FormName,
                                  CreatedBy = b.ContractorName,
                                  CreatedDate = a.CreatedDate.Value.ToShortDateString(),
                                  Notes = a.Notes
                              }).ToList();

                    GridCreatedEntries.DataSource = xy.ToList();
                    GridCreatedEntries.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnCreateNewEntry_Click(object sender, EventArgs e)
    {
        try
        {
            int AppId = 0;
            if (Request.QueryString["appid"] != null)
            {
                AppId = int.Parse(Request.QueryString["appid"]);
            }
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                AppManagerAssignedForm App_m = new AppManagerAssignedForm();
                var isexists = Hdc.AppManagerAssignedForms.Where(o => o.FormName.ToLower().Trim() == txtFormName.Text.ToLower().Trim() && o.AppID == AppId && o.Form_Type.ToLower() != "child").FirstOrDefault();
                if (isexists == null)
                {
                    App_m.AppID = AppId;
                    App_m.FormName = txtFormName.Text.Trim();
                    App_m.Notes = txtNotes.Text.Trim();
                    App_m.CreatedDate = DateTime.Now;
                    App_m.CreatedBy = sessionKeys.UID;
                    App_m.ModifiedBy = sessionKeys.UID;
                    App_m.ModifiedDate = DateTime.Now;
                    App_m.Form_Type = "Parent";
                    Hdc.AppManagerAssignedForms.InsertOnSubmit(App_m);
                    Hdc.SubmitChanges();
                    Response.Redirect(string.Format("~/WF/SmartApp/AppForm.aspx?appformid={0}", App_m.ID), false);
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
    protected void GridCreatedEntries_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try 
        {
            int AppId = 0;
            if (Request.QueryString["appid"] != null)
            {
                AppId = int.Parse(Request.QueryString["appid"]);
            }
           // sesion["AppId"] = AppId 

            if (e.CommandName == "Edit")
            {
                Response.Redirect("~/WF/SmartApp/AppForm.aspx?AppFormID=" + e.CommandArgument.ToString());
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
                    BindCustomGrid(AppId);
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
    protected void GridCreatedEntries_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string DeletedId = ((Label)e.Row.FindControl("lblDeleteId")).Text;

                LinkButton Im = new LinkButton();
                Im.ID = "DeleteImg";
                //Im.ClientIDMode = "static";
                Im.OnClientClick = "return confirm('Do you want to delete this record?');";
                Im.ToolTip = "Delete";
                Im.CommandName = "Delete";
                Im.CommandArgument = DeletedId.ToString();
                Im.SkinID = "BtnLinkDelete";
                e.Row.Cells[e.Row.Cells.Count - 1].Controls.Add(Im);



                e.Row.Cells[2].Visible = false;
                LinkButton imgDelete = ((LinkButton)e.Row.FindControl("btnDelete"));
                // ImageButton btnEdit = ((ImageButton)e.Row.FindControl("btnEdit"));

                int i = PermissionsChecking();
                if (i == 2)
                {
                    imgDelete.Visible = false;
                    //   btnEdit.Enabled = false;
                }
                else if (i == 3)
                {

                }
                else if (i == 4)
                {
                    imgDelete.Visible = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}