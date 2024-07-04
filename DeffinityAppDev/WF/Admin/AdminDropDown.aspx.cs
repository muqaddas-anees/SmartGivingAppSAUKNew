using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using POMgt.DAL;
using POMgt.Entity;
using System.Linq;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using AssetsMgr.DAL;
using AssetsMgr.Entity;
//using TimesheetMgt.DAL;
//using TimesheetMgt.Entity;

public partial class AdminDropDown_page : BasePage
{
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = Resources.DeffinityRes.AdminDropdownLists;//"Admin Dropdown Lists";
        lblmsg.Visible = false;
        if (!IsPostBack)
        {
            if (Request.QueryString["type"] != null)
            {
                btnBack.Visible = true;

            }
            ddlKPIcatgory.DataBind();
            BindKPILabelgrid(int.Parse(ddlPageType.SelectedValue));
            lblError.Visible = false;
            BindTypes();
            BindGrid();
            BindRoundUp();
            //SelectCustomFields();
            int Panel = Convert.ToInt32(Request.QueryString["Panel"]);
            PanelMakeVisible(Panel);
            Bind_gvBBBEERating();
            DisplayProjectclassPanel();
            ProjectClass_Visibility();

            BindStatusDdl();
        }
    }
    private void ProjectClass_Visibility()
    {
        try
        {
            //Check Project class feature is enabled
            string[] str = PermissionManager.GetFeatures();
            if (!Page.IsPostBack)
            {
                pnl_projectclass_content.Visible = Convert.ToBoolean(str[94]);
                pnl_projectclass_head.Visible = Convert.ToBoolean(str[94]);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void Edit(TextBox _txtEdit, DropDownList _ddlItems, Button _imgSubmit, Button _imgAdd, Button _imgEdit)
    {
        _txtEdit.Text = _ddlItems.SelectedItem.Text;
        _txtEdit.Visible = true;
        _ddlItems.Visible = false;
        _imgSubmit.Visible = true;
        _imgAdd.Visible = false;
        _imgEdit.Visible = false;
    }
    private void Edit(TextBox _txtEdit, DropDownList _ddlItems, Button _imgSubmit, Button _imgAdd, Button _imgEdit, Panel _entryType)
    {
        _txtEdit.Text = _ddlItems.SelectedItem.Text;
        char[] comm = { '-' };
        string[] myentry = _txtEdit.Text.Split(comm);
        _txtEdit.Text = myentry[0];
      
        _txtEdit.Visible = true;
        _ddlItems.Visible = false;
        _imgSubmit.Visible = true;
        _imgAdd.Visible = false;
        _imgEdit.Visible = false;
        _entryType.Visible = true;
    }
    private void Add(TextBox _txtEdit, DropDownList _ddlItems, Button _imgSubmit, Button _imgAdd, Button _imgEdit)
    {
        _ddlItems.SelectedIndex = 0;
        _txtEdit.Text = "";
        _txtEdit.Visible = true;
        _ddlItems.Visible = false;
        _imgSubmit.Visible = true;
        _imgAdd.Visible = false;
        _imgEdit.Visible = false;
    }
    private void Add(TextBox _txtEdit, DropDownList _ddlItems, Button _imgSubmit, Button _imgAdd, Button _imgEdit, Panel _entyType)
    {
        _ddlItems.SelectedIndex = 0;
        _txtEdit.Text = "";
        _txtEdit.Visible = true;
        _ddlItems.Visible = false;
        _imgSubmit.Visible = true;
        _imgAdd.Visible = false;
        _imgEdit.Visible = false;
        _entyType.Visible = true;
       
    }
    private void Cancel(TextBox _txtEdit, DropDownList _ddlItems, Button _btnSubmit, Button _btnAdd, Button _btnEdit)
    {

        _ddlItems.Visible = true;
        _ddlItems.SelectedIndex = 0;
        _txtEdit.Visible = false;
        _btnSubmit.Visible = false;
        _btnAdd.Visible = true;
        _btnEdit.Visible = true;
    }
    private void Cancel(TextBox _txtEdit, DropDownList _ddlItems, Button _btnSubmit, Button _btnAdd, Button _btnEdit, Panel _addEntryType)
    {

        _ddlItems.Visible = true;
        _ddlItems.SelectedIndex = 0;
        _txtEdit.Visible = false;
        _btnSubmit.Visible = false;
        _btnAdd.Visible = true;
        _btnEdit.Visible = true;
        _addEntryType.Visible = false;
       


    }
    private int insertUpdate(string _strsql, string _txtEdit, int _ddlItemsID,double StartTime,double EndTime)
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand(_strsql);
            db.AddInParameter(cmd, "@ID", DbType.Int32, _ddlItemsID);
            db.AddInParameter(cmd, "@Item", DbType.String, _txtEdit);
            db.AddInParameter(cmd, "@StartTime", DbType.Double, StartTime);
            db.AddInParameter(cmd, "@EndTime", DbType.Double, EndTime);
            db.AddOutParameter(cmd, "@output", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            int getVal = (int)db.GetParameterValue(cmd, "@output");
            cmd.Dispose();

            return getVal;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {

        }

    }
    private int insertUpdate(string _strsql, string _txtEdit, int _ddlItemsID)
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand(_strsql);           
            db.AddInParameter(cmd, "@ID", DbType.Int32, _ddlItemsID);
            db.AddInParameter(cmd, "@Item", DbType.String, _txtEdit);
            db.AddOutParameter(cmd, "@output", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            int getVal = (int)db.GetParameterValue(cmd, "@output");
            cmd.Dispose();

            return getVal;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {

        }

    }
    private int delete(string _strsql, DropDownList _ddlItems)
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand(_strsql);
            db.AddInParameter(cmd, "@ID", DbType.Int32, Convert.ToInt32(_ddlItems.SelectedValue));
            db.ExecuteNonQuery(cmd);
            cmd.Dispose();
            _ddlItems.DataBind();
            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
        }
    }
    //protected void ImgAddRisk_Click(object sender, EventArgs e)
    //{
    //    Add(txtRiskType, ddlRiskTypes, ImgSubmitRisk, ImgAddRisk, ImgEditRisk);
    //    txtRiskType.Focus();
    //}
    //protected void imgDeleteRisk_Click(object sender, EventArgs e)
    //{
    //    delete("DEFFINITY_DELETERISKTYPE", ddlRiskTypes);
    //}
    //protected void ImgEditRisk_Click(object sender, EventArgs e)
    //{
    //    Edit(txtRiskType, ddlRiskTypes, ImgSubmitRisk, ImgAddRisk, ImgEditRisk);
       
    //}
    //protected void ImgCancelRisk_Click(object sender, EventArgs e)
    //{
    //    Cancel(txtRiskType, ddlRiskTypes, ImgSubmitRisk, ImgAddRisk, ImgEditRisk);
    //}
    //protected void ImgSubmitRisk_Click(object sender, EventArgs e)
    //{
    //    int i = insertUpdate("DEFFINITY_INUP_ADMINRISKTYPES", txtRiskType.Text, Convert.ToInt32(ddlRiskTypes.SelectedValue));
    //    ddlRiskTypes.DataBind();
    //    insertUpdateStatus(i);
    //    txt_UnitPrice.Text ="";

    //    Cancel(txtRiskType, ddlRiskTypes, ImgSubmitRisk, ImgAddRisk, ImgEditRisk);
    //}
    private void insertUpdateStatus(int i)
    {
        lblmsg.Visible = true;
        if (i == 0)
        {
            lblmsg.Text = Resources.DeffinityRes.ErrorOccuredWhileInsertingandUpdating;//"Error Occured While Inserting/Updating";
        }
        else if (i == 1)
        {
            lblmsg.Text = Resources.DeffinityRes.InsertedSuccessfully;//"Inserted Successfully";
            lblmsg.ForeColor = System.Drawing.Color.Green;
        }
        else if (i == 2)
        {
            lblmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;//"Updated Successfully";
            lblmsg.ForeColor = System.Drawing.Color.Green;
        }
        else if (i == 3)
        {
            lblmsg.Text = Resources.DeffinityRes.ItemAlreadyExists;//"Item Already Exists";
        }
        else
        {
            lblmsg.Text = Resources.DeffinityRes.UnknownErrorOccured;//"Unknown Error Occured";
        }
    }
    //protected void ImgAddStatus_Click(object sender, EventArgs e)
    //{
    //    Add(txtStatus, ddlStatus,ImgSubmitStatus, ImgAddStatus, ImgEditStatus);
    //    txtStatus.Focus();
    //}
    //protected void ImgSubmitStatus_Click(object sender, EventArgs e)
    //{
    //    int i = insertUpdate("DEFFINITY_INUP_ADMINAC2PStatus", txtStatus.Text, Convert.ToInt32(ddlStatus.SelectedValue));
    //    ddlStatus.DataBind();
    //    insertUpdateStatus(i);
    //    Cancel(txtStatus, ddlStatus, ImgSubmitStatus, ImgAddStatus, ImgEditStatus);
    //}
    //protected void ImgCancelStatus_Click(object sender, EventArgs e)
    //{
    //    Cancel(txtStatus, ddlStatus, ImgSubmitStatus, ImgAddStatus, ImgEditStatus);
    //}
    //protected void ImgEditStatus_Click(object sender, EventArgs e)
    //{
    //    Edit(txtStatus, ddlStatus, ImgSubmitStatus, ImgAddStatus, ImgEditStatus);
    //}
    //protected void ImgDeleteStatus_Click(object sender, EventArgs e)
    //{
    //    delete("DEFFINITY_DELETEAC2PStatus", ddlStatus);
    //}
    protected void ImgEditAssetType_Click(object sender, EventArgs e)
    {
        Edit(txtAssetType, ddlAssetType, ImgSubmitAssetType, ImdAddAssetType, ImgEditAssetType);
    }
    protected void ImgSubmitAssetType_Click(object sender, EventArgs e)
    {
        int i = insertUpdate("DEFFINITY_INUP_ADMINAssetsType", txtAssetType.Text, Convert.ToInt32(ddlAssetType.SelectedValue));
        ddlAssetType.DataBind();
        insertUpdateStatus(i);
        Cancel(txtAssetType, ddlAssetType, ImgSubmitAssetType, ImdAddAssetType, ImgEditAssetType);
    }
    protected void ImgCancelAssetType_Click(object sender, EventArgs e)
    {
        Cancel(txtAssetType, ddlAssetType, ImgSubmitAssetType, ImdAddAssetType, ImgEditAssetType);
    }
    protected void ImgDeleteAssetType_Click(object sender, EventArgs e)
    {
        delete("DEFFINITY_DELETEAssetsType", ddlAssetType);
    }
    protected void ImdAddAssetType_Click(object sender, EventArgs e)
    {
        Add(txtAssetType, ddlAssetType, ImgSubmitAssetType, ImdAddAssetType, ImgEditAssetType);
        txtAssetType.Focus();
    }
    protected void ImgAddAssetMake_Click(object sender, EventArgs e)
    {
        Add(txtAssetMake, ddlAssetMake, ImgSubmitAssetMake, ImgAddAssetMake, ImgEditAssetMake);
        txtAssetMake.Focus();
    }
    protected void ImgSubmitAssetMake_Click(object sender, EventArgs e)
    {
        int i = insertUpdate("DEFFINITY_INUP_ADMINAssetsMake", txtAssetMake.Text, Convert.ToInt32(ddlAssetMake.SelectedValue));
        ddlAssetMake.DataBind();
        insertUpdateStatus(i);
        Cancel(txtAssetMake, ddlAssetMake, ImgSubmitAssetMake, ImgAddAssetMake, ImgEditAssetMake);
    }

    protected void ImgCancelAssetMake_Click(object sender, EventArgs e)
    {
        Cancel(txtAssetMake, ddlAssetMake, ImgSubmitAssetMake, ImgAddAssetMake, ImgEditAssetMake);

    }
    protected void ImgDeleteAssetMake_Click(object sender, EventArgs e)
    {
        delete("DEFFINITY_DELETEAssetsMake", ddlAssetMake);
    }
    protected void ImgEditAssetMake_Click(object sender, EventArgs e)
    {
        Edit(txtAssetMake, ddlAssetMake, ImgSubmitAssetMake, ImgAddAssetMake, ImgEditAssetMake);

    }
    protected void ImgAddAssetModel_Click(object sender, EventArgs e)
    {
        Add(txtAssetModel, ddlAssetModel, ImgSubmitAssetModel, ImgAddAssetModel, ImgEditAssetModel);
        txtAssetModel.Focus();
    }
    protected void ImgEditAssetModel_Click(object sender, EventArgs e)
    {
        Edit(txtAssetModel, ddlAssetModel, ImgSubmitAssetModel, ImgAddAssetModel, ImgEditAssetModel);
    }
    protected void ImgSubmitAssetModel_Click(object sender, EventArgs e)
    {
        int i = insertUpdate("DEFFINITY_INUP_ADMINAssetsModel", txtAssetModel.Text, Convert.ToInt32(ddlAssetModel.SelectedValue));
        ddlAssetModel.DataBind();
        insertUpdateStatus(i);
        Cancel(txtAssetModel, ddlAssetModel, ImgSubmitAssetModel, ImgAddAssetModel, ImgEditAssetModel);
    }
    protected void ImgCancelAssetModel_Click(object sender, EventArgs e)
    {
        Cancel(txtAssetModel, ddlAssetModel, ImgSubmitAssetModel, ImgAddAssetModel, ImgEditAssetModel);
    }
    protected void ImgDeleteAssetModel_Click(object sender, EventArgs e)
    {
        delete("DEFFINITY_DELETEAssetsModel", ddlAssetModel);
    }
   
  

    //protected void ImgAddLogicalAsset_Click(object sender, EventArgs e)
    //{
    //    Add(txtLogicalAssets, ddlLogicalAssets, ImgSubmitLogicalAsset, ImgAddLogicalAsset, ImgEditLogicalAsset);
    //    txtLogicalAssets.Focus();
    //}
    //protected void ImgEditLogicalAsset_Click(object sender, EventArgs e)
    //{
    //    Edit(txtLogicalAssets, ddlLogicalAssets, ImgSubmitLogicalAsset, ImgAddLogicalAsset, ImgEditLogicalAsset);
    //}
    //protected void ImgSubmitLogicalAsset_Click(object sender, EventArgs e)
    //{
    //    int i = insertUpdate("DEFFINITY_INUP_ADMINLogicalAssets", txtLogicalAssets.Text , Convert.ToInt32(ddlLogicalAssets.SelectedValue ));
    //    ddlLogicalAssets.DataBind();
    //    insertUpdateStatus(i);
    //    Cancel(txtLogicalAssets, ddlLogicalAssets, ImgSubmitLogicalAsset, ImgAddLogicalAsset, ImgEditLogicalAsset);
    //}
    //protected void ImgCancelLogicalAsset_Click(object sender, EventArgs e)
    //{
    //    Cancel(txtLogicalAssets, ddlLogicalAssets, ImgSubmitLogicalAsset, ImgAddLogicalAsset, ImgEditLogicalAsset);
    //}
    //protected void ImgDeleteLogicalAsset_Click(object sender, EventArgs e)
    //{
    //        delete("DEFFINITY_DELETELogicalAssets", ddlLogicalAssets);
    //}
    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        Add(txtCategory, ddlCategory, btnSubmitCategory, btnAddCategory, btnEditCategory);
        txtCategory.Focus();
    }
    protected void btnEditCategory_Click(object sender, EventArgs e)
    {
        Edit(txtCategory, ddlCategory, btnSubmitCategory, btnAddCategory, btnEditCategory);
    }
    protected void btnSubmitCategory_Click(object sender, EventArgs e)
    {
        int i = insertUpdate("DEFFINITY_INUP_ADMINCATEGORY", txtCategory.Text, Convert.ToInt32(ddlCategory.SelectedValue));
        ddlCategory.DataBind();
        insertUpdateStatus(i);
        Cancel(txtCategory, ddlCategory, btnSubmitCategory, btnAddCategory, btnEditCategory);

    }
    protected void btnCancelCategory_Click(object sender, EventArgs e)
    {
        Cancel(txtCategory, ddlCategory, btnSubmitCategory, btnAddCategory, btnEditCategory);
    }
    protected void BtnDelCategory_Click(object sender, EventArgs e)
    {
        delete("DEFFINITY_DELETECATEGORY", ddlCategory);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Count > 1)
            {
                if (Request.QueryString["type"] != null)
                {
                    string type = Request.QueryString["type"].ToString();
                    switch (type)
                    {
                        case "category":
                            Response.Redirect("~/ProjectOverview.aspx?type=category");
                            break;
                        case "logical_p":
                            if (Request.QueryString["id"] != null)
                            { Response.Redirect(string.Format("~/ProjectLogicalAssetDependency.aspx?id={0}&name={1}&project={2}", Request.QueryString["id"].ToString(), Request.QueryString["name"].ToString(), Request.QueryString["project"].ToString())); }
                            else if (Request.QueryString["project"] != null)
                            { Response.Redirect(string.Format("~/ProjectLogicalAssets.aspx?project={0}", Request.QueryString["project"].ToString())); }
                            break;
                        case "logical_r":
                            if (Request.QueryString["id"] != null)
                            { Response.Redirect(string.Format("~/UpdateLogicalAssetdependency.aspx?id={0}&name={1}&project={2}", Request.QueryString["id"].ToString(), Request.QueryString["name"].ToString(), Request.QueryString["project"].ToString())); }
                            else if (Request.QueryString["project"] != null)
                            { Response.Redirect(string.Format("~/UpdateLogicalAsset.aspx?project={0}", Request.QueryString["project"].ToString())); }
                            break;
                        case "logical_pm":
                            if (Request.QueryString["id"] != null)
                            { Response.Redirect(string.Format("~/CheckpointLogicalAssetdependency.aspx?id={0}&name={1}&project={2}", Request.QueryString["id"].ToString(), Request.QueryString["name"].ToString(), Request.QueryString["project"].ToString())); }
                            else if (Request.QueryString["project"] != null)
                            { Response.Redirect(string.Format("~/CheckpointLogicalAsset.aspx?project={0}", Request.QueryString["project"].ToString())); }

                            break;
                        case "logical_a":
                            if (Request.QueryString["id"] != null)
                            { Response.Redirect(string.Format("~/PortfolioLogicalAssetsDep.aspx?id={0}&name={1}type=asset", Request.QueryString["id"].ToString(), Request.QueryString["name"].ToString())); }
                            else
                            { Response.Redirect("~/PortfolioLogicalAssets.aspx"); }
                            break;
                        case "asset":
                            if (Request.QueryString["id"] != null)
                            { Response.Redirect(string.Format("~/LogicalAssetsDependencyNew.aspx?id={0}&name={1}", Request.QueryString["id"].ToString(), Request.QueryString["name"].ToString())); }

                            break;
                        case "assetpf":
                            if (Request.QueryString["id"] != null)
                            {
                               

                                Response.Redirect(string.Format("~/PortfolioLogicalAssetsDep.aspx?id={0}&name={1}", Request.QueryString["id"].ToString(), Request.QueryString["name"].ToString())); }
                            break;
                        case "Finance":
                            if (Request.QueryString["id"] == null)
                            {
                                int Pref = Convert.ToInt32(Request.QueryString["Projet"]);
                                Response.Redirect(string.Format("~/WF/Projects/ProjectFinancials.aspx?Project={0}&type={1}", Pref, "Actual"), false);
                            }
                            break;
                        case "resourcetype":
                            if (QueryStringValues.Project > 0)
                            {
                                Response.Redirect(string.Format("~/WF/Projects/ProjectOverviewV4.aspx?project={0}", QueryStringValues.Project), false);
                            }
                            break;
                    }
                }

            }
            else if (Request.QueryString["type"] != null)
            {
                string type = Request.QueryString["type"].ToString();
                switch (type)
                {
                    case "category":
                        if (Request.QueryString["project"] != null)
                        {
                            Response.Redirect(string.Format("~/WF/Projects/ProjectOverview.aspx?project={0}", Request.QueryString["project"].ToString()));

                        }
                        else
                        {
                            Response.Redirect("~/WF/Projects/ProjectOverview.aspx?type=category");
                        }
                        break;
                    case "asset":
                        if (Request.QueryString["id"] != null)
                        { Response.Redirect(string.Format("~/LogicalAssetsDependencyNew.aspx?id={0}&name={1}", Request.QueryString["id"].ToString(), Request.QueryString["name"].ToString())); }
                        else
                        { Response.Redirect("~/LogicalAssetsAdminNew.aspx"); }
                        break;
                    case "assetpf":
                        if (Request.QueryString["id"] != null)
                        { Response.Redirect(string.Format("~/PortfolioLogicalAssetsDep.aspx?id={0}&name={1}", Request.QueryString["id"].ToString(), Request.QueryString["name"].ToString())); }
                        else
                        { Response.Redirect("~/PortfolioLogicalAssets.aspx"); }
                        break;
                    case "ExpensesType":
                        if (Request.QueryString["id"] == null)
                        { Response.Redirect(string.Format("~/WF/Resources/TimeSheetResources.aspx", false)); }
                        break;
                    case "Adminusers":
                        if (Request.QueryString["id"] == null)
                        { Response.Redirect(string.Format("~/WF/Admin/AdminUsers.aspx", false)); }
                        break;
                }
            }
            else if (Request.QueryString["project"] != null)
            {
                Response.Redirect(string.Format("~/WF/Projects/ProjectOverview.aspx?project={0}", Request.QueryString["project"].ToString()));

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }
    //protected void btnaddpdepartment_Click(object sender, EventArgs e)
    //{
    //    Add(txtPdeparmrnt, ddlpdepartment, btnsubmitpdepartment, btnaddpdepartment,btneditpdepartment);
    //    txtPdeparmrnt.Focus();
    //}
    //protected void btneditpdepartment_Click(object sender, EventArgs e)
    //{
    //    Edit(txtPdeparmrnt, ddlpdepartment, btnsubmitpdepartment, btnaddpdepartment, btneditpdepartment);
    //}
    //protected void btnsubmitpdepartment_Click(object sender, EventArgs e)
    //{
    //    int i = insertUpdate("DEFFINITY_INUP_PORTFOLIODEPARTMENT", txtPdeparmrnt.Text, Convert.ToInt32(ddlpdepartment.SelectedValue));
    //    ddlpdepartment.DataBind();
    //    insertUpdateStatus(i);
    //    Cancel(txtPdeparmrnt, ddlpdepartment, btnsubmitpdepartment, btnaddpdepartment, btneditpdepartment);

    //}
    //protected void btncancelpdepartment_Click(object sender, EventArgs e)
    //{
    //    Cancel(txtPdeparmrnt, ddlpdepartment, btnsubmitpdepartment, btnaddpdepartment, btneditpdepartment);
    //}
    //protected void BtnDelPDepartment_Click(object sender, EventArgs e)
    //{
    //    delete("DEFFINITY_DELETEPDEPARTMENT", ddlpdepartment);
    //}
   
    protected void btncancelCustom_Click(object sender, EventArgs e)
    {
        //SelectCustomFields();
    }
    protected void btnaddIssuetype_Click(object sender, EventArgs e)
    {
        Add(txtIssuetype, ddlIssuestype, btnsubmitIssuetype, btnaddIssuetype, btneditIssuetype);
        txtIssuetype.Focus();
    }
    protected void btneditpIssuetype_Click(object sender, EventArgs e)
    {
        Edit(txtIssuetype, ddlIssuestype, btnsubmitIssuetype, btnaddIssuetype, btneditIssuetype);
    }
    protected void btnsubmitpIssuetype_Click(object sender, EventArgs e)
    {
        int i = insertUpdate("Deffinity_IssuestypeInsertUpdate", txtIssuetype.Text, Convert.ToInt32(ddlIssuestype.SelectedValue));
        ddlIssuestype.DataBind();
        insertUpdateStatus(i);
        Cancel(txtIssuetype , ddlIssuestype, btnsubmitIssuetype, btnaddIssuetype, btneditIssuetype);
    }
    protected void btncancelIssuetype_Click(object sender, EventArgs e)
    {
        Cancel(txtIssuetype, ddlIssuestype, btnsubmitIssuetype, btnaddIssuetype, btneditIssuetype);
    }
    protected void BtnDelIssuetype_Click(object sender, EventArgs e)
    {
        delete("Deffinity_IssuetypeDelete", ddlIssuestype);
        RequiredFieldValidator22.ErrorMessage = string.Empty;
    }
    
    protected void btnaddExperienceClassification_Click(object sender, EventArgs e)
    {
        Add(txtExperienceClassification, ddlExperienceClassification, btnsubmitExperienceClassification, btnaddExperienceClassification, btneditExperienceClassification);
        txtExperienceClassification.Focus();
        
    }
    protected void btneditExperienceClassification_Click(object sender, EventArgs e)
    {
        Edit(txtExperienceClassification, ddlExperienceClassification, btnsubmitExperienceClassification, btnaddExperienceClassification, btneditExperienceClassification);
        
    }
    protected void btnsubmitpExperienceClassification_Click(object sender, EventArgs e)
    {
        int i = insertUpdate("Deffinity_ExperienceClassificationInsertUpdate", txtExperienceClassification.Text, Convert.ToInt32(ddlExperienceClassification.SelectedValue));
        ddlExperienceClassification.DataBind();
        insertUpdateStatus(i);
        Cancel(txtExperienceClassification, ddlExperienceClassification, btnsubmitExperienceClassification, btnaddExperienceClassification, btneditExperienceClassification);
        
    }
    protected void btncancelExperienceClassification_Click(object sender, EventArgs e)
    {
        Cancel(txtExperienceClassification, ddlExperienceClassification, btnsubmitExperienceClassification, btnaddExperienceClassification, btneditExperienceClassification);
        
    }
    protected void BtnDelExperienceClassification_Click(object sender, EventArgs e)
    {
        delete("Deffinity_ExperienceClassificationDelete", ddlExperienceClassification);
        
    }
    protected void btnsubmitpriority_Click(object sender, EventArgs e)
    {
        int i = insertUpdate("Deffinity_PriorityLevelInsertUpdate", txtpriority.Text, Convert.ToInt32(ddlpriority.SelectedValue));
        ddlpriority.DataBind();
        insertUpdateStatus(i);
        Cancel(txtpriority, ddlpriority, btnsubmitpriority, btnaddpriority, btneditpriority);
    }
    protected void tncancelpriority_Click(object sender, EventArgs e)
    {
        Cancel(txtpriority, ddlpriority, btnsubmitpriority, btnaddpriority, btneditpriority);
    }
    protected void btnaddpriority_Click(object sender, EventArgs e)
    {
        Add(txtpriority, ddlpriority, btnsubmitpriority, btnaddpriority, btneditpriority);
        txtpriority.Focus();
    }
    protected void btneditpriority_Click(object sender, EventArgs e)
    {
        Edit(txtpriority, ddlpriority, btnsubmitpriority, btnaddpriority, btneditpriority);
    }

    protected void BtnDelPriority_Click(object sender, EventArgs e)
    {
        delete("Deffinity_priorityLevelDelete", ddlpriority);
    }
    //protected void btnDelPortType_Click(object sender, EventArgs e)
    //{
    //    delete("Deffinity_PortfolioTypeDelete", ddlPortFolioType);

    //}
    //protected void btnaddPortType_Click(object sender, EventArgs e)
    //{
    //    Add(txtPortFolioType, ddlPortFolioType, btnsubmitPortType, btnaddPortType, btneditPortType);
    //    txtPortFolioType.Focus();
        
    //}
    //protected void btneditPortType_Click(object sender, EventArgs e)
    //{
    //    Edit(txtPortFolioType, ddlPortFolioType, btnsubmitPortType, btnaddPortType, btneditPortType);
    //}
    //protected void btnsubmitPortType_Click(object sender, EventArgs e)
    //{
    //    int i = insertUpdate("Deffinity_PortFolioTypeInsertUpdate", txtPortFolioType.Text, Convert.ToInt32(ddlPortFolioType.SelectedValue));
    //    ddlPortFolioType.DataBind();
    //    insertUpdateStatus(i);
    //    Cancel(txtPortFolioType,ddlPortFolioType,btnsubmitPortType,btnaddPortType,btneditPortType);
    //}
    //protected void btncancelPortType_Click(object sender, EventArgs e)
    //{
    //    Cancel(txtPortFolioType, ddlPortFolioType, btnsubmitPortType, btnaddPortType, btneditPortType);
    //}

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        Add(txt_ExpensesEntryTyp, ExpensesEntryType, btnSubmit, btnAdd12, btnedit);
        txt_ExpensesEntryTyp.Focus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Cancel(txt_ExpensesEntryTyp, ExpensesEntryType, btnSubmit, btnAdd12, btnedit);
        txt_UnitPrice.Text = "";
        txt_sellingprice.Text = "";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        decimal getUnitPrice = 0;
        decimal getSellingprice = 0;
        if (txt_UnitPrice.Text == "")
        {
            getUnitPrice = 0;
        }
        else
        { 
            getUnitPrice=Convert.ToDecimal(txt_UnitPrice.Text);

        }
        if (txt_sellingprice.Text == "")
        {
            getSellingprice = 0;
        }
        else
        {
            getSellingprice = Convert.ToDecimal(txt_sellingprice.Text);

        }



        int i = insertUpdate_Expensesentry("DN_InsertExpensesEntryType", txt_ExpensesEntryTyp.Text, Convert.ToInt32(ExpensesEntryType.SelectedValue), getUnitPrice, getSellingprice);
        ExpensesEntryType.DataBind();
        insertUpdateStatus(i);
        Cancel(txt_ExpensesEntryTyp, ExpensesEntryType, btnSubmit, btnAdd12, btnedit);
        txt_UnitPrice.Text = "";
        txt_sellingprice.Text = "";
    }
    private int insertUpdate_Expensesentry(string _strsql, string _txtEdit, int _ddlItemsID,decimal Unitprice,decimal Sellingprice)
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand(_strsql);
            db.AddInParameter(cmd, "@ID", DbType.Int32, _ddlItemsID);
            db.AddInParameter(cmd, "@Item", DbType.String, _txtEdit);
            db.AddInParameter(cmd, "@Unitprice", DbType.Decimal, Unitprice);
            db.AddInParameter(cmd, "@SellingPrice", DbType.Decimal, Sellingprice);
            db.AddOutParameter(cmd, "@output", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            int getVal = (int)db.GetParameterValue(cmd, "@output");
            cmd.Dispose();

            return getVal;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {

        }

    }
        
    protected void btnedit_Click(object sender, EventArgs e)
    {
        Edit(txt_ExpensesEntryTyp, ExpensesEntryType, btnSubmit, btnAdd12, btnedit);

    }
    protected void ImageExpensesEntryType_Click(object sender, EventArgs e)
    {
        delete("Deffinity_ExpensesTypeDelete", ExpensesEntryType);
        
    }
    protected void ExpensesEntryType_SelectedIndexChanged(object sender, EventArgs e)
    {

        SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select BuyingPrice,sellingPrice from ExpensesentryType where ID = {0}", ExpensesEntryType.SelectedValue));
        string GetUnitPrice = "";
        string GetSellingPrice = "";
        while (dr.Read())
        {
            if (dr["BuyingPrice"].ToString() == "")
            {
                GetUnitPrice = "0.00";
            }
            else
            {
                GetUnitPrice = dr["BuyingPrice"].ToString();
            }
            if (dr["sellingPrice"].ToString() == "")
            {
                GetSellingPrice = "0.00";
            }
            else
            {
                GetSellingPrice = dr["sellingPrice"].ToString();
            }
            
            

        }

        txt_UnitPrice.Text = GetUnitPrice;
        txt_sellingprice.Text = GetSellingPrice;
      btnAdd12.Visible = false;
      btnSubmit.Visible = false;
        
    }


    //protected void btnRT_Del_Click(object sender, EventArgs e)
    //{
    //    delete("Deffinity_ResourceTypeDelete", ddlResourceType);

    //}
    //protected void btnRT_Add_Click(object sender, EventArgs e)
    //{
    //    Add(txtResourceType, ddlResourceType, btnRT_Submit, btnRT_Add, btnRT_Edit);
    //    txtResourceType.Focus();

    //}
    //protected void btnRT_Edit_Click(object sender, EventArgs e)
    //{
    //    Edit(txtResourceType, ddlResourceType, btnRT_Submit, btnRT_Add, btnRT_Edit);
    //}
    //protected void btnRT_submit_Click(object sender, EventArgs e)
    //{
    //    int i = insertUpdate("Deffinity_ResourceTypeInsertUpdate", txtResourceType.Text, Convert.ToInt32(ddlResourceType.SelectedValue));
    //    ddlResourceType.DataBind();
    //    insertUpdateStatus(i);
    //    Cancel(txtResourceType, ddlResourceType, btnRT_Submit, btnRT_Add, btnRT_Edit);
    //}
    //protected void btnRT_Cancel_Click(object sender, EventArgs e)
    //{
    //    Cancel(txtResourceType, ddlResourceType, btnRT_Submit, btnRT_Add, btnRT_Edit);
    //}

    protected void btn_addLoabour_Click(object sender, EventArgs e)
    {
        Add(txt_lobourTypename, ddllobourNames, btn_submittLabour, btn_addLoabour, btn_edditLoabour);
        txt_lobourTypename.Focus();
    }
    protected void btn_submittLabour_Click(object sender, EventArgs e)
    {
        int i = insertUpdate("Deffinity_LabourcategoryInsertUpdate", txt_lobourTypename.Text, Convert.ToInt32(ddllobourNames.SelectedValue));
        ddllobourNames.DataBind();
        insertUpdateStatus(i);
        Cancel(txt_lobourTypename, ddllobourNames, btn_submittLabour, btn_addLoabour, btn_edditLoabour);
    }
    protected void btn_CancelLobour_Click(object sender, EventArgs e)
    {
        Cancel(txt_lobourTypename, ddllobourNames, btn_submittLabour, btn_addLoabour, btn_edditLoabour);
    }
    protected void btn_edditLoabour_Click(object sender, EventArgs e)
    {
        Edit(txt_lobourTypename, ddllobourNames, btn_submittLabour, btn_addLoabour, btn_edditLoabour);
    }

    #region Vendors

    protected void btndeleteVendorAttributes_Click(object sender, EventArgs e)
    {
        delete("Deffinity_TBLATTRIBUTES_DELETE", ddlVendorAttributes);
    }
    protected void btnaddVendorAttributes_Click(object sender, EventArgs e)
    {
        Add(txtVendorAttributes, ddlVendorAttributes, btnsubmitVendorAttributes, btnaddVendorAttributes, btneditVendorAttributes);
        txtVendorAttributes.Focus();
    }
    protected void btneditVendorAttributes_Click(object sender, EventArgs e)
    {
        Edit(txtVendorAttributes, ddlVendorAttributes, btnsubmitVendorAttributes, btnaddVendorAttributes, btneditVendorAttributes);
    }
    protected void btnsubmitVendorAttributes_Click(object sender, EventArgs e)
    {
        int i = insertUpdate("Deffinity_TBLATTRIBUTES_InsertUpdate", txtVendorAttributes.Text, Convert.ToInt32(ddlVendorAttributes.SelectedValue));
        ddlVendorAttributes.DataBind();
        insertUpdateStatus(i);
        Cancel(txtVendorAttributes, ddlVendorAttributes, btnsubmitVendorAttributes, btnaddVendorAttributes, btneditVendorAttributes);
    }
    protected void btncancelVendorAttributes_Click(object sender, EventArgs e)
    {
        Cancel(txtVendorAttributes, ddlVendorAttributes, btnsubmitVendorAttributes, btnaddVendorAttributes, btneditVendorAttributes);
    }






    #endregion



    #region panelVisibilty

    private void PanelVisibility(bool p_Projects, bool p_TS, bool p_Resources, bool p_SD, bool p_IssuesRisks, bool p_Assets, bool p_Vendors, bool p_BBBEE, bool p_CRM, bool p_Inventory)
    {
        pnlProject.Visible = p_Projects;
        pnlTS.Visible = p_TS;
        pnlResources.Visible = p_Resources;
        pnlSD.Visible = p_SD;
        pnlIssues.Visible = p_IssuesRisks;
        pnlAssets.Visible = p_Assets;
        pnlVendors.Visible = p_Vendors;
        pnlBBBEE.Visible = p_BBBEE;
        pnlCRM.Visible = p_CRM;
        pnlInventory.Visible = p_Inventory;
    }

    private void PanelMakeVisible(int _type)
    {
        switch (_type)
        {
            case 0:
                //Project panel
                litHeader.Text = "Project Admin";
                PanelVisibility(true, false, false, false, false, false, false, false, false,false);
                break;
            case 1:
                litHeader.Text = "Timesheet Admin";
                //timesheet panel
                PanelVisibility(false, true, false, false, false, false, false, false, false,false);
               
                break;
            case 2:
                litHeader.Text = "Resource Admin";
                PanelVisibility(false, false, true, false, false, false, false, false, false,false);
               
                break;
            case 3:
                litHeader.Text = "Service Desk Defaults";
                PanelVisibility(false, false, false, true, false, false, false, false, false,false);
                
                break;
            case 4:
                litHeader.Text = "Issues & Risks Admin";
                PanelVisibility(false, false, false, false, true, false, false, false, false,false);

                break;
            case 5:
                litHeader.Text = "Assets Admin";
                PanelVisibility(false, false, false, false, false, true, false, false, false,false);
                break;
            case 6:
                litHeader.Text = "Vendor Admin";
                PanelVisibility(false, false, false, false, false, false, true, false, false,false);
                break;
            case 7:
                PanelVisibility(false, false, false, false, false, false, false, true, false,false);
                break;
            case 8:
                PanelVisibility(false, false, false, false, false, false, false, false, true,false);
                break;
            case 9:
                litHeader.Text = "Inventory Admin";
                PanelVisibility(false, false, false, false, false, false, false, false, false, true);
                break;
        }


        
    }
#endregion

    #region "Added on 2nd March 2011 PO DRAIN"

    private void BindTypes()
    {
    //    TimeSheetDataContext timesheet = new TimeSheetDataContext();
    //    var types = (from r in timesheet.TimesheetEntryTypes
    //                 orderby r.EntryType
    //                 select r).ToList();
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ID,EntryType from TimesheetEntryType  order by EntryType").Tables[0];

        ddlHoursType.DataSource = dt;
        ddlHoursType.DataTextField = "EntryType";
        ddlHoursType.DataValueField = "ID";
        ddlHoursType.DataBind();
        ddlHoursType.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindGrid()
    {
        PurchaseOrderMgtDataContext po = new PurchaseOrderMgtDataContext();
        var types = (from r in po.PODrains
                     orderby r.HourName
                     select r).ToList();
        gridHoursType.DataSource = types;
        gridHoursType.DataBind();
    }

    protected void imgApply_Click(object sender, EventArgs e)
    {
        try
        {
            PurchaseOrderMgtDataContext PO = new PurchaseOrderMgtDataContext();
            PODrain insert = new PODrain();
            lblError.Visible = false;
            var isExist = (from r in PO.PODrains
                           where r.HourNameType == int.Parse(ddlHoursType.SelectedValue)
                           select r).ToList();
            if (isExist != null)
            {
                if (isExist.Count == 0)
                {
                    insert.HourName = ddlHoursType.SelectedItem.Text;
                    insert.HourNameType = int.Parse(ddlHoursType.SelectedValue);
                    PO.PODrains.InsertOnSubmit(insert);
                    PO.SubmitChanges();
                    lblError.Visible = true;
                    lblError.Text = "Successfully Added";
                    lblError.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Selected type already exist";
                    


                }
            }

            ddlHoursType.SelectedValue = "0";
            BindGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
   
    protected void gridHoursType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "delete")
            {
                PurchaseOrderMgtDataContext PO = new PurchaseOrderMgtDataContext();
                PODrain delete = PO.PODrains.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                PO.PODrains.DeleteOnSubmit(delete);
                PO.SubmitChanges();
                lblError.Visible = false;
                gridHoursType.EditIndex = -1;
                BindGrid();
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    protected void gridHoursType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        gridHoursType.EditIndex = -1;
        BindGrid();
    }
    protected void imgApply1_Click(object sender, EventArgs e)
    {
        //if (chkRoundUp.Checked == true)
        //{
          projectTaskDataContext projectDef = new projectTaskDataContext();
           
            var getProjectdef = (from r in projectDef.ProjectDefaults
                                 select r).ToList().FirstOrDefault();
            if (getProjectdef != null)
            {
                ProjectDefault update =
                    projectDef.ProjectDefaults.Single(P => P.ID == int.Parse(getProjectdef.ID.ToString()));
                if (chkRoundUp.Checked == true)
                {
                    update.RoundUp = 1;
                }
                else
                {
                    update.RoundUp = 0;
                }
                update.HalfDay = Hours(string.IsNullOrEmpty(txtHalfDay.Text) ? "0:0" : txtHalfDay.Text);
                update.FullDay = Hours(string.IsNullOrEmpty(txtFullDay.Text) ? "0:0" : txtFullDay.Text);// Convert.ToDouble(string.IsNullOrEmpty(txtFullDay.Text) ? "0" : txtFullDay.Text);
                projectDef.SubmitChanges();
                lblMsgRoundUp.Visible = true;
                lblMsgRoundUp.ForeColor = System.Drawing.Color.Green;
                lblMsgRoundUp.Text = "Successfully Added";
            }
        //}
        //else
        //{
        //    lblMsgRoundUp.Visible = true;
        //    lblMsgRoundUp.ForeColor = System.Drawing.Color.Red;
        //    lblMsgRoundUp.Text = "Please select round up check box";
        //}
    }

    private void BindRoundUp()
    {
        projectTaskDataContext projectDef = new projectTaskDataContext();

        var getData = (from r in projectDef.ProjectDefaults
                       select r).ToList().FirstOrDefault();

        if (getData != null)
        {
            if (getData.RoundUp == 1)
            {
             
                chkRoundUp.Checked = true;
                txtHalfDay.Text =ToConvertHours(getData.HalfDay.ToString());
                txtFullDay.Text = ToConvertHours(getData.FullDay.ToString());

            }

        }
    }

    private decimal Hours(string val)
    {
        decimal hours = 0;
        //string val = "";
        char[] comm = { ':' };
        string[] getva = val.Split(comm);

        string newval = "";

        newval = getva[0] + "." + getva[1];


        hours = Convert.ToDecimal(newval);
        return hours;

    }
    private string ToConvertHours(string val)
    {
        int GettingTotalhours = 0;
       
        if (!val.Contains("."))
        {
            val = val + ".00";

        }
        char[] getvalue1 = { '.' };
       
        string[] hours = val.Split(getvalue1);
       
        GettingTotalhours = (Convert.ToInt32(hours[0]) * 60) + Convert.ToInt32(hours[1]);


        int hours1 = GettingTotalhours / 60;
        int minuts = GettingTotalhours % 60;
        string s = hours1.ToString("00") + ":" + minuts.ToString("00");
        return s;
    }


    #region BBBEE

    private void Bind_gvBBBEERating()
    {
        DataSet ds = new DataSet();
        ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_RFI_BBBEE_MAIN_FILL");
        gvBBBEERating.DataSource = ds;
        gvBBBEERating.DataBind();
    }



    protected void gvBBBEERating_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBBBEERating.PageIndex = e.NewPageIndex;
        Bind_gvBBBEERating();
    }

    protected void gvBBBEERating_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvBBBEERating.EditIndex = e.NewEditIndex;
        Bind_gvBBBEERating();
    }

    protected void gvBBBEERating_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvBBBEERating.EditIndex = -1;
        Bind_gvBBBEERating();
    }


    protected void gvBBBEERating_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvBBBEERating_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void gvBBBEERating_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Update")
        {
            int ID = Convert.ToInt32(e.CommandArgument.ToString());
            int i = gvBBBEERating.EditIndex;
            GridViewRow Row = gvBBBEERating.Rows[i];
            TextBox txtMinPoints = (TextBox)Row.FindControl("txtMinPoints");
            TextBox txtMaxPoints = (TextBox)Row.FindControl("txtMaxPoints");
            TextBox txtBEEStatus = (TextBox)Row.FindControl("txtBEEStatus");
            TextBox txtRecogLevel = (TextBox)Row.FindControl("txtRecogLevel");
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@MinimumPoints", Convert.ToInt32(txtMinPoints.Text.ToString()));
            parameters[1] = new SqlParameter("@MaximumPoints", Convert.ToInt32(txtMaxPoints.Text.ToString()));
            parameters[2] = new SqlParameter("@BEEStatus", txtBEEStatus.Text.ToString());
            parameters[3] = new SqlParameter("@RecognitionLevel", Convert.ToSingle(txtRecogLevel.Text.ToString()));
            parameters[4] = new SqlParameter("@ID", ID);
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_RFI_BBBEE_MAIN_UPDATE", parameters);
            gvBBBEERating.EditIndex = -1;
            Bind_gvBBBEERating();
        }
        else if (e.CommandName == "Delete")
        {
            int ID = Convert.ToInt32(e.CommandArgument.ToString());
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", ID);
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_RFI_BBBEE_MAIN_DELETE", parameters);
            Bind_gvBBBEERating();
        }
        else if (e.CommandName == "Add")
        {
            TextBox txtMinPoints1 = (TextBox)gvBBBEERating.FooterRow.FindControl("txtMinPoints1");
            TextBox txtMaxPoints1 = (TextBox)gvBBBEERating.FooterRow.FindControl("txtMaxPoints1");
            TextBox txtBEEStatus1 = (TextBox)gvBBBEERating.FooterRow.FindControl("txtBEEStatus1");
            TextBox txtRecogLevel1 = (TextBox)gvBBBEERating.FooterRow.FindControl("txtRecogLevel1");
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@MinimumPoints", Convert.ToInt32(txtMinPoints1.Text.ToString()));
            parameters[1] = new SqlParameter("@MaximumPoints", Convert.ToInt32(txtMaxPoints1.Text.ToString()));
            parameters[2] = new SqlParameter("@BEEStatus", txtBEEStatus1.Text.ToString());
            parameters[3] = new SqlParameter("@RecognitionLevel", Convert.ToSingle(txtRecogLevel1.Text.ToString()));
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_RFI_BBBEE_MAIN_INSERT", parameters);
            Bind_gvBBBEERating();
        }
        else if (e.CommandName == "CancleEntry")
        {
            TextBox txtMinPoints1 = (TextBox)gvBBBEERating.FooterRow.FindControl("txtMinPoints1");
            TextBox txtMaxPoints1 = (TextBox)gvBBBEERating.FooterRow.FindControl("txtMaxPoints1");
            TextBox txtBEEStatus1 = (TextBox)gvBBBEERating.FooterRow.FindControl("txtBEEStatus1");
            TextBox txtRecogLevel1 = (TextBox)gvBBBEERating.FooterRow.FindControl("txtRecogLevel1");
            txtMinPoints1.Text = "";
            txtMaxPoints1.Text = "";
            txtBEEStatus1.Text = "";
            txtRecogLevel1.Text = "";
        }

    }

    protected void gvBBBEERating_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.FindControl("lblId").ToString() != null)
                {
                    string id = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
                    if (id == "-99")
                    {
                        e.Row.Visible = false ;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }



    #endregion

    protected void btnAddProjectClass_Click(object sender, EventArgs e)
    {
        ProjectClassButtonVisibility(false, true, true, false);
        ProjectClassButtonVisibility(false);
    }
    protected void btnInsertProjectClass_Click(object sender, EventArgs e)
    {
        projectTaskDataContext pd = new projectTaskDataContext();

        if (!string.IsNullOrEmpty(txtProjectClass.Text.Trim()))
        {
            if (pd.ProjectClasses.Where(p => p.ClassName.ToLower() == txtProjectClass.Text.Trim().ToLower()).Count() == 0)
            {
                ProjectClass pcentity = new ProjectClass();
                pcentity.ClassName = txtProjectClass.Text.Trim();
                pd.GetTable<ProjectClass>().InsertOnSubmit(pcentity);
                pd.SubmitChanges();
                ProjectClassButtonVisibility(true, false, false, true);
                ProjectClassButtonVisibility(true);
                lblmsgProjectclass.ForeColor = System.Drawing.Color.Green;
                lblmsgProjectclass.Text = "Project Class added successfully.";
                txtProjectClass.Text = string.Empty;
                BindProjectClass();
            }
            else
            {
                lblmsgProjectclass.ForeColor = System.Drawing.Color.Red;
                lblmsgProjectclass.Text = "Project Class already exists.";
            }
        }
        else
        {
            lblmsgProjectclass.ForeColor = System.Drawing.Color.Red;
            lblmsgProjectclass.Text = "Please enter Project Class";

        }

    }
    protected void btnCancelProjectClass_Click(object sender, EventArgs e)
    {
        txtProjectClass.Text = string.Empty;
        ProjectClassButtonVisibility(true, false, false, true);
        ProjectClassButtonVisibility(true);
    }
    protected void btnDeleteProjectClass_Click(object sender, EventArgs e)
    {
        if (int.Parse(ddlProjectClass.SelectedValue) > 0)
        {
            projectTaskDataContext pd = new projectTaskDataContext();
            ProjectClass pcentity = pd.ProjectClasses.Where(p => p.ID == int.Parse(ddlProjectClass.SelectedValue)).FirstOrDefault();
            pd.GetTable<ProjectClass>().DeleteOnSubmit(pcentity);
            pd.SubmitChanges();
            lblmsgProjectclass.ForeColor = System.Drawing.Color.Green;
            lblmsgProjectclass.Text = "Project Class deleted successfully.";
            BindProjectClass();
        }
        else
        {
            lblmsgProjectclass.ForeColor = System.Drawing.Color.Red;
            lblmsgProjectclass.Text = "Please select Project Class";

        }

    }
    private void ProjectClassButtonVisibility(bool v_btnAddProjectClass, bool v_btnInsertProjectClass, bool v_btnCancelProjectClass, bool v_btnDeleteProjectClass)
    {
        btnAddProjectClass.Visible = v_btnAddProjectClass;
        btnInsertProjectClass.Visible = v_btnInsertProjectClass;
        btnCancelProjectClass.Visible = v_btnCancelProjectClass;
        btnDeleteProjectClass.Visible = v_btnDeleteProjectClass;


    }
    private void ProjectClassButtonVisibility(bool v_ddlprojectclass)
    {
        ddlProjectClass.Visible = v_ddlprojectclass;
        txtProjectClass.Visible = !v_ddlprojectclass;
    }
    private void BindProjectClass()
    {
        projectTaskDataContext pd = new projectTaskDataContext();

        ddlProjectClass.DataSource = from pc in pd.ProjectClasses
                                     select new { pc.ID, pc.ClassName };
        ddlProjectClass.DataTextField = "ClassName";
        ddlProjectClass.DataValueField = "ID";
        ddlProjectClass.DataBind();
        ddlProjectClass.Items.Insert(0, new ListItem("Please select...", "0"));

        ProjectDefault pcentity = (from pc in pd.ProjectDefaults
                                   select pc).FirstOrDefault();
        ChkEnableClass.Checked = pcentity.ProjectClass.Value;

    }
    private void BindChecklist()
    {
        projectTaskDataContext pd = new projectTaskDataContext();

        ddlChecklist.DataSource = from pc in pd.MasterTemplates
                                  where (pc.ChecklistType == 6)
                                  select new { pc.ID, pc.Description };
        ddlChecklist.DataTextField = "Description";
        ddlChecklist.DataValueField = "ID";
        ddlChecklist.DataBind();
        ddlChecklist.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void DisplayProjectclassPanel()
    {
        BindProjectClass();
        BindChecklist();
        ProjectClassButtonVisibility(true, false, false, true);
        ProjectClassButtonVisibility(true);
    }
    protected void btnProjectClassApply_Click(object sender, EventArgs e)
    {
        try
        {
            projectTaskDataContext pd = new projectTaskDataContext();

            ProjectClassToChecklist pcentity;
            if (pd.ProjectClassToChecklists.Where(p => p.ClassID == int.Parse(ddlProjectClass.SelectedValue) && p.MasterChecklistID == int.Parse(ddlChecklist.SelectedValue) && p.assignedStatus == int.Parse(radioClassSelect.SelectedValue)).Count() == 0)
            {
                pcentity = new ProjectClassToChecklist();
                pcentity.MasterChecklistID = int.Parse(ddlChecklist.SelectedValue);
                pcentity.ClassID = int.Parse(ddlProjectClass.SelectedValue);
                pcentity.assignedStatus = int.Parse(radioClassSelect.SelectedValue);
                pd.GetTable<ProjectClassToChecklist>().InsertOnSubmit(pcentity);
                pd.SubmitChanges();
            }
            else
            {
                pcentity = pd.ProjectClassToChecklists.Where(p => p.ClassID == int.Parse(ddlProjectClass.SelectedValue)).FirstOrDefault();
                pcentity.MasterChecklistID = int.Parse(ddlChecklist.SelectedValue);
                pcentity.assignedStatus = int.Parse(radioClassSelect.SelectedValue);
                pd.SubmitChanges();
            }

            lblmsgProjectclass.ForeColor = System.Drawing.Color.Green;
            lblmsgProjectclass.Text = "Checklist assigned to Project class successfully.";
            BindClassGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlProjectClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(ddlProjectClass.SelectedValue) > 0)
        {
            BindClassGrid();
            //projectTaskDataContext pd = new projectTaskDataContext();

            //ProjectClassToChecklist pcentity = pd.ProjectClassToChecklists.Where(p => p.ClassID == int.Parse(ddlProjectClass.SelectedValue)).FirstOrDefault();
            //if (pcentity != null)
            //    ddlChecklist.SelectedValue = pcentity.MasterChecklistID.ToString();
            //else
            //    ddlChecklist.SelectedIndex = 0;
        }

    }
    protected void ChkEnableClass_CheckedChanged1(object sender, EventArgs e)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "update ProjectDefaults set ProjectClass = @enableProjectClass", new SqlParameter("@enableProjectClass", ChkEnableClass.Checked));
            lblmsgProjectclass.ForeColor = System.Drawing.Color.Green;
            if (ChkEnableClass.Checked)
            {
                lblmsgProjectclass.Text = "Project class enabled successfully.";
            }
            else
            {
                lblmsgProjectclass.Text = "Project class disabled successfully.";
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    private void BindClassGrid()
    {
        projectTaskDataContext pd = new projectTaskDataContext();
        List<ProjectClassToChecklist_Details> pcentity1 = pd.ProjectClassToChecklist_Details.Where(p => p.ClassID == int.Parse(ddlProjectClass.SelectedValue)).ToList();

        gridClass.DataSource = pcentity1;
        gridClass.DataBind();

    }
    protected void gridClass_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "delete_item")
            {
                projectTaskDataContext pd = new projectTaskDataContext();

                ProjectClassToChecklist pcentity = pd.ProjectClassToChecklists.Where(p => p.ID == int.Parse(e.CommandArgument.ToString())).FirstOrDefault();

                pd.GetTable<ProjectClassToChecklist>().DeleteOnSubmit(pcentity);
                pd.SubmitChanges();
            }

            BindClassGrid();

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }

    //Added on sept 09 2011 ----sani
    #region "KPI Category"
    protected void imgAddKpi_Click(object sender, EventArgs e)
    {
        Add(txtKpiCategory, ddlKPIcatgory, imgKPISubmit, imgAddKpi, imgEditKpi);
        txtKpiCategory.Focus();
    }
    protected void imgEditKpi_Click(object sender, EventArgs e)
    {
        Edit(txtKpiCategory, ddlKPIcatgory, imgKPISubmit, imgAddKpi, imgEditKpi);
    }
    protected void imgKPISubmit_Click(object sender, EventArgs e)
    {
        int i = insertUpdate("DEFFINITY_ADMIN_KPI_CATEGORY", txtKpiCategory.Text, Convert.ToInt32(ddlKPIcatgory.SelectedValue));
        ddlKPIcatgory.DataBind();
        ddlCategory_KPI.DataBind();
        insertUpdateStatus(i);
        Cancel(txtKpiCategory, ddlKPIcatgory, imgKPISubmit, imgAddKpi, imgEditKpi);
    }
    protected void imgKPICancel_Click(object sender, EventArgs e)
    {
        Cancel(txtKpiCategory, ddlKPIcatgory, imgKPISubmit, imgAddKpi, imgEditKpi);
    }

    protected void imgKPIdelete_Click(object sender, EventArgs e)
    {
        delete("DEFFINITY_DELETE_KPI_CATEGORY", ddlKPIcatgory);
    }
    #endregion
    #region "KPI Lables"

    private void BindKPILabelgrid(int PageID)
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text,
            "select ID,LabelsName,KPICategoryID,(select kpiCatogeryName from KPI_category where ID=KPICategoryID) as Name  from KPI_LablesName  where pagetype=@PageID",
            new SqlParameter("@PageID",PageID)).Tables[0];
        gridKPILables.DataSource = dt;
        gridKPILables.DataBind();
    }


    protected void gridKPILables_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.FindControl("ddlCategoryKpi") != null)
            {
                Label lblLabelName = (Label)e.Row.FindControl("lblLabelName");
                Label lblCategoryID = (Label)e.Row.FindControl("lblCategoryID");
                DropDownList ddlCategoryKpi = (DropDownList)e.Row.FindControl("ddlCategoryKpi");

                ddlCategoryKpi.DataBind();
                ddlCategoryKpi.SelectedValue = lblCategoryID.Text;

            }
        }
    }
    protected void gridKPILables_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        gridKPILables.EditIndex = -1;
        BindKPILabelgrid(int.Parse(ddlPageType.SelectedValue));
    }
    protected void gridKPILables_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            int index = gridKPILables.EditIndex;
            GridViewRow row = gridKPILables.Rows[index];
            TextBox txtKpiLabelName = (TextBox)row.FindControl("txtKpiLabelName");
            DropDownList ddlCategoryKpi = (DropDownList)row.FindControl("ddlCategoryKpi");

            //int i=SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure,
            //    "DEFFINITY_KPI_InsertUpdateLables", new SqlParameter("@kpiLabelName", txtAddLable.Text),
            //    new SqlParameter("@kpiCatID", int.Parse(ddlCategory_KPI.SelectedValue)),
            //    new SqlParameter("@ID", int.Parse(e.CommandArgument.ToString())));
            AddEditKPILabels(int.Parse(e.CommandArgument.ToString()), txtKpiLabelName.Text, int.Parse(ddlCategoryKpi.SelectedValue), "DEFFINITY_KPI_InsertUpdateLables", 0);
            BindKPILabelgrid(int.Parse(ddlPageType.SelectedValue));

        }
        if (e.CommandName == "delete")
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text,
               "delete KPI_LablesName where ID=@ID", new SqlParameter("@ID", int.Parse(e.CommandArgument.ToString())));
            BindKPILabelgrid(int.Parse(ddlPageType.SelectedValue));
        }

    }
    protected void gridKPILables_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridKPILables.EditIndex = -1;
        BindKPILabelgrid(int.Parse(ddlPageType.SelectedValue));
    }
    protected void gridKPILables_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridKPILables.EditIndex = e.NewEditIndex;
        BindKPILabelgrid(int.Parse(ddlPageType.SelectedValue));
    }


    protected void imgAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure,
            //    "DEFFINITY_KPI_InsertUpdateLables", new SqlParameter("@kpiLabelName", txtAddLable.Text),
            //    new SqlParameter("@kpiCatID", int.Parse(ddlCategory_KPI.SelectedValue)),
            //    new SqlParameter("@ID",0));
            AddEditKPILabels(0, txtAddLable.Text, int.Parse(ddlCategory_KPI.SelectedValue), "DEFFINITY_KPI_InsertUpdateLables", int.Parse(ddlPageNo.SelectedValue));
            BindKPILabelgrid(int.Parse(ddlPageType.SelectedValue));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void AddEditKPILabels(int ID, string Name, int Category, string _strsql, int pageNo)
    {
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd = db.GetStoredProcCommand(_strsql);
        db.AddInParameter(cmd, "@kpiLabelName", DbType.String, Name);
        db.AddInParameter(cmd, "@kpiCatID", DbType.Int32, Category);
        db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
        db.AddInParameter(cmd, "@kpipageNo", DbType.Int32, pageNo);
        db.ExecuteNonQuery(cmd);
        cmd.Dispose();

    }


    protected void gridKPILables_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gridKPILables.EditIndex = -1;
        BindKPILabelgrid(int.Parse(ddlPageType.SelectedValue));
    }
     protected void ddlPageType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindKPILabelgrid(int.Parse(ddlPageType.SelectedValue));
    }
    #endregion

    #region Timesheet custom type

     protected void ImgAddTimeType_Click(object sender, EventArgs e)
     {
         Add(txtTimeType, ddlTimeType, ImgSubmitTimeType, ImgAddTimeType, ImgEditTimeType);
         txtTimeType.Focus();
     }
     protected void ImgEditTimeType_Click(object sender, EventArgs e)
     {
         Edit(txtTimeType, ddlTimeType, ImgSubmitTimeType, ImgAddTimeType, ImgEditTimeType);
     }
     protected void ImgSubmitTimeType_Click(object sender, EventArgs e)
     {
         int i = insertUpdate("DEFFINITY_INUP_ADMINTimeSheetType", txtTimeType.Text, Convert.ToInt32(ddlTimeType.SelectedValue));
         ddlTimeType.DataBind();
         insertUpdateStatus(i);
         Cancel(txtTimeType, ddlTimeType, ImgSubmitTimeType, ImgAddTimeType, ImgEditTimeType);

        

     }
     protected void ImgCancelTimeType_Click(object sender, EventArgs e)
     {
         Cancel(txtTimeType, ddlTimeType, ImgSubmitTimeType, ImgAddTimeType, ImgEditTimeType);
     }
     protected void imgDeleteTimeType_Click(object sender, EventArgs e)
     {
         delete("DEFFINITY_DELETETimeSheetType", ddlTimeType);

     }

     
    #endregion




     protected void ImgAddEquipment_Click(object sender, EventArgs e)
     {
         Add(txtEquipment, ddlEquipment, ImgSubmitEquipment, ImgAddEquipment, ImgEditEquipment);
         txtEquipment.Focus();
     }
     protected void ImgEditEquipment_Click(object sender, EventArgs e)
     {
         Edit(txtEquipment, ddlEquipment, ImgSubmitEquipment, ImgAddEquipment, ImgEditEquipment);
     }
     protected void ImgSubmitEquipment_Click(object sender, EventArgs e)
     {
         int i = insertUpdate("DEFFINITY_INUP_ADMINAssetEquipmentType", txtEquipment.Text, Convert.ToInt32(ddlEquipment.SelectedValue));
         ddlEquipment.DataBind();
         insertUpdateStatus(i);
         Cancel(txtEquipment, ddlEquipment, ImgSubmitEquipment, ImgAddEquipment, ImgEditEquipment);
     }
     protected void ImgCancelEquipment_Click(object sender, EventArgs e)
     {
         Cancel(txtEquipment, ddlEquipment, ImgSubmitEquipment, ImgAddEquipment, ImgEditEquipment);
     }
     protected void ImgEquipment_delete_Click(object sender, EventArgs e)
     {
         delete("DEFFINITY_DELETEEquipmetType", ddlEquipment);
     }
     protected void ImgSpkrAdd_Click(object sender, EventArgs e)
     {
         Add(txtSpkrBusType, ddlSpkrBusType, ImgSpkrSubmit, ImgSpkrAdd, ImgSpkrEdit);
         txtSpkrBusType.Focus();
     }
     protected void ImgSpkrEdit_Click(object sender, EventArgs e)
     {
         Edit(txtSpkrBusType, ddlSpkrBusType, ImgSpkrSubmit, ImgSpkrAdd, ImgSpkrEdit);
     }
     protected void ImgSpkrSubmit_Click(object sender, EventArgs e)
     {
         int i = insertUpdate("DEFFINITY_INUP_ADMIN_DV_SpeakerBusType", txtSpkrBusType.Text, Convert.ToInt32(ddlSpkrBusType.SelectedValue));
         ddlSpkrBusType.DataBind();
         insertUpdateStatus(i);
         Cancel(txtSpkrBusType, ddlSpkrBusType, ImgSpkrSubmit, ImgSpkrAdd, ImgSpkrEdit);
     }
     protected void ImgSpkrCancel_Click(object sender, EventArgs e)
     {
         Cancel(txtSpkrBusType, ddlSpkrBusType, ImgSpkrSubmit, ImgSpkrAdd, ImgSpkrEdit);
     }
     protected void ImgSpkrDelete_Click(object sender, EventArgs e)
     {
         delete("DEFFINITY_DELETESpeakerBusType", ddlSpkrBusType);
     }
     protected void btnCatAdd_Click(object sender, EventArgs e)
     {
         Add(txtCategoryName, ddlCategoryName, btnCatSubmit, btnCatAdd,btnCatEdit);
         txtCategoryName.Focus();
     }
     protected void btnCatEdit_Click(object sender, EventArgs e)
     {
         Edit(txtCategoryName, ddlCategoryName, btnCatSubmit, btnCatAdd, btnCatEdit);
     }
     protected void btnCatSubmit_Click(object sender, EventArgs e)
     {
         int i = insertUpdate("DEFFINITY_INUP_ADMINTimeSheetCategory", txtCategoryName.Text, Convert.ToInt32(ddlCategoryName.SelectedValue));
         ddlCategoryName.DataBind();
         insertUpdateStatus(i);
         Cancel(txtCategoryName, ddlCategoryName, btnCatSubmit, btnCatAdd, btnCatEdit);

        

     }
     protected void btnCatCancel_Click(object sender, EventArgs e)
     {
         Cancel(txtCategoryName, ddlCategoryName, btnCatSubmit, btnCatAdd, btnCatEdit);
     }
     protected void btnCatDelete_Click(object sender, EventArgs e)
     {
         delete("DEFFINITY_DELETETimeSheetCategory", ddlCategoryName);
       
     }

     #region Sectors

     protected void btndeleteSectors_Click(object sender, EventArgs e)
     {
         delete("Sectors_DELETE", ddlSectors);
     }
     protected void btnaddSectors_Click(object sender, EventArgs e)
     {
         Add(txtSectors, ddlSectors, btnsubmitSectors, btnaddSectors, btneditSectors);
         txtSectors.Focus();
     }
     protected void btneditSectors_Click(object sender, EventArgs e)
     {
         Edit(txtSectors, ddlSectors, btnsubmitSectors, btnaddSectors, btneditSectors);
     }
     protected void btnsubmitSectors_Click(object sender, EventArgs e)
     {
         int i = insertUpdate("Sectors_INSERTUPDATE", txtSectors.Text, Convert.ToInt32(ddlSectors.SelectedValue));
         ddlSectors.DataBind();
         insertUpdateStatus(i);
         Cancel(txtSectors, ddlSectors, btnsubmitSectors, btnaddSectors, btneditSectors);
     }
     protected void btncancelSectors_Click(object sender, EventArgs e)
     {
         Cancel(txtSectors, ddlSectors, btnsubmitSectors, btnaddSectors, btneditSectors);
     }
     #endregion

     #region RevenueType

     protected void btndeleteRevenueType_Click(object sender, EventArgs e)
     {
         delete("RevenueType_DELETE", ddlRevenueType);
     }
     protected void btnaddRevenueType_Click(object sender, EventArgs e)
     {
         Add(txtRevenueType, ddlRevenueType, btnsubmitRevenueType, btnaddRevenueType, btneditRevenueType);
         txtRevenueType.Focus();
     }
     protected void btneditRevenueType_Click(object sender, EventArgs e)
     {
         Edit(txtRevenueType, ddlRevenueType, btnsubmitRevenueType, btnaddRevenueType, btneditRevenueType);
     }
     protected void btnsubmitRevenueType_Click(object sender, EventArgs e)
     {
         int i = insertUpdate("RevenueType_INSERTUPDATE", txtRevenueType.Text, Convert.ToInt32(ddlRevenueType.SelectedValue));
         ddlRevenueType.DataBind();
         insertUpdateStatus(i);
         Cancel(txtRevenueType, ddlRevenueType, btnsubmitRevenueType, btnaddRevenueType, btneditRevenueType);
     }
     protected void btncancelRevenueType_Click(object sender, EventArgs e)
     {
         Cancel(txtRevenueType, ddlRevenueType, btnsubmitRevenueType, btnaddRevenueType, btneditRevenueType);
     }
     #endregion

     protected void BtnaddStatus_Click(object sender, EventArgs e)
     {
         try
         {
             Add(txtStatusName, DdlStatusName, BtnSubmitStatus, BtnaddStatus, BtnEditStatus);
             txtStatusName.Focus();
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
     }

     protected void BtnEditStatus_Click(object sender, EventArgs e)
     {
         try
         {
             Edit(txtStatusName, DdlStatusName, BtnSubmitStatus, BtnaddStatus, BtnEditStatus);
             txtStatusName.Focus();
             lblType.Text = DdlStatusName.SelectedValue;
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
     }
     public void BindStatusDdl()
     {
         try
         {
             using (AssetsToSoftwareDataContext ass = new AssetsToSoftwareDataContext())
             {
                 var slist = ass.Asset_Status.ToList();
                 DdlStatusName.DataSource = slist;
                 DdlStatusName.DataTextField = "StatusName";
                 DdlStatusName.DataValueField = "Id";
                 DdlStatusName.DataBind();
                 DdlStatusName.Items.Insert(0, new ListItem("PLEASE SELECT", "0"));
             }
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
     }
     public void UpdateStatus(int id ,string Name)
     {
         try
         {
             using (AssetsToSoftwareDataContext ass = new AssetsToSoftwareDataContext())
             {
                 if (ass.Asset_Status.Where(p => p.StatusName.ToLower() == Name.ToLower() && p.Id != id).Count() == 0)
                 {
                     Asset_Status a_s = ass.Asset_Status.Where(a => a.Id == id).FirstOrDefault();
                     a_s.StatusName = Name;
                     ass.SubmitChanges();
                     lblMsgInStatus.ForeColor = System.Drawing.Color.Green;
                     lblMsgInStatus.Text = "Updated successfully";
                     Cancel(txtStatusName, DdlStatusName, BtnSubmitStatus, BtnaddStatus, BtnEditStatus);
                 }
                 else
                 {
                     lblMsgInStatus.ForeColor = System.Drawing.Color.Red;
                     lblMsgInStatus.Text = "Status already exist with this name";
                 }
             }
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
     }
     public void DeleteStatus(int id)
     {
         try 
         {
             using (AssetsToSoftwareDataContext ass = new AssetsToSoftwareDataContext())
             {
                 Asset_Status a_s = ass.Asset_Status.Where(a => a.Id == id).FirstOrDefault();
                 ass.Asset_Status.DeleteOnSubmit(a_s);
                 ass.SubmitChanges();
                 BindStatusDdl();
             }
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
     }
     public void InsertStatus(Asset_Status a_s)
     {

         try
         {
             using (AssetsToSoftwareDataContext ass = new AssetsToSoftwareDataContext())
             {
                 if (ass.Asset_Status.Where(p =>p.StatusName.ToLower() == a_s.StatusName.ToLower()).Count() == 0)
                 {
                     ass.Asset_Status.InsertOnSubmit(a_s);
                     ass.SubmitChanges();
                     lblMsgInStatus.ForeColor = System.Drawing.Color.Green;
                     lblMsgInStatus.Text = "Added successfully";
                     Cancel(txtStatusName, DdlStatusName, BtnSubmitStatus, BtnaddStatus, BtnEditStatus);
                 }
                 else
                 {
                     lblMsgInStatus.ForeColor = System.Drawing.Color.Red;
                     lblMsgInStatus.Text = "Status already exist with this name";
                 }
             }
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
     }
     protected void BtnSubmitStatus_Click(object sender, EventArgs e)
     {
         try
         {
             Asset_Status a_s = new Asset_Status();
             if (lblType.Text == string.Empty)
             {
                 a_s.StatusName = txtStatusName.Text;
                 InsertStatus(a_s);
             }
             else if (lblType.Text != string.Empty)
             {
                 UpdateStatus(int.Parse(lblType.Text), txtStatusName.Text.Trim().ToLower());
             }
             lblType.Text = string.Empty;
             BindStatusDdl();
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
     }

     protected void BtnStatusCancel_Click(object sender, EventArgs e)
     {
         try
         {
             Cancel(txtStatusName, DdlStatusName, BtnSubmitStatus, BtnaddStatus, BtnEditStatus);
             lblType.Text = string.Empty;
             BindStatusDdl();
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
     }

     protected void LnlStatusDelete_Click(object sender, EventArgs e)
     {
         try
         {
             DeleteStatus(int.Parse(DdlStatusName.SelectedValue));
             lblMsgInStatus.ForeColor = System.Drawing.Color.Green;
             lblMsgInStatus.Text = "Deleted successfully.";
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
     }
}
