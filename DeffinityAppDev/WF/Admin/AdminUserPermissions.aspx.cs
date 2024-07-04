using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Deffinity.Bindings;
using Deffinity.BE;
using Deffinity.BLL;
using Certifications;
using VT.Entity;
using VT.DAL;
using System.Text;
//using Deffinity.TrainingEntity;
//using Deffinity.TrainingManager;
using System.Collections.Generic;
using Deffinity.AddUserPermissionData;

public partial class AdminUserPermissions : System.Web.UI.Page
{
    DisBindings getData = new DisBindings();
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    string userName;
    int GUserId;
    protected void Page_Load(object sender, EventArgs e)
    {
       // Master.PageHead = "Admin";
        //int uid = Convert.ToInt32(Request.QueryString["uid"]);

        //getUserId.Value = Request.QueryString["uid"];
        try
        {
            if (!this.IsPostBack)
            {

               
                SelectUserData(Convert.ToInt32(Request.QueryString["uid"]));
                GUserId = Convert.ToInt32(Request.QueryString["uid"]);
                DefaultBindings();

            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void SelectUserData(int cid)
    {

        try
        {
            //edit name panel
            DbCommand cmd = db.GetStoredProcCommand("DN_SelectResource");
            db.AddInParameter(cmd, "@ID", DbType.Int32, cid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    drContractors.SelectedValue = cid.ToString();
                    
                    lblusername.Text = dr["ContractorName"].ToString();
                    drpermission.SelectedValue = dr["SID"].ToString();
                    
                    
                }
                dr.Close();
            }
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    protected void btngohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Admin/Adminusers.aspx");
    }

    private void DefaultBindings()
    {
        getData.DdlBindSelect(drContractors, "DN_ResourcesList", "ID", "ContractorName", true, true);
        getData.DdlBindSelect(drpermission, "DN_ResourceType", "ID", "Type", true, true);
        BindTreeViewParentNodes();
        BindProjectTreeView();
        BindSDTreeView();
        BindCPTreeView();
        BindDBVTreeView();
        BindOMTreeView();
        
    }

    private void BindTreeViewParentNodes()
    {
        try
        {
            AddUserPermissionData AUPermission = new AddUserPermissionData ();
            DataTable _dt = AUPermission.SelectAllModule();
            TreeView2.Nodes.Clear();
            TreeNodeCollection nodes = TreeView2.Nodes;
            
            foreach (DataRow _dr in _dt.Rows)
            {
                TreeNode tn = new TreeNode();
                tn.Text = _dr["ModuleName"].ToString();
                tn.Value = _dr["ModuleId"].ToString();
                if (Convert.ToInt32(tn.Value) < 4)
                {
                    nodes.Add(tn);
                    BindTreeViewChildNodes(Convert.ToInt32(_dr["ModuleId"].ToString()), tn);
                }
            }
            TreeView2.ExpandAll();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindTVOtherModules()
    {
        try
        {
            AddUserPermissionData AUPermission = new AddUserPermissionData();
            DataTable _dt = AUPermission.SelectAllModule();
            //TreeView2.Nodes.Clear();
            TreeNodeCollection nodes = TreeViewOM.Nodes;

            foreach (DataRow _dr in _dt.Rows)
            {
                TreeNode tn = new TreeNode();
                tn.Text = _dr["ModuleName"].ToString();
                tn.Value = _dr["ModuleId"].ToString();
                if ((Convert.ToInt32(tn.Value) > 7) && (Convert.ToInt32(tn.Value) < 10))
                {
                    nodes.Add(tn);
                    BindTreeViewChildNodes(Convert.ToInt32(_dr["ModuleId"].ToString()), tn);
                }
            }
            TreeViewOM.ExpandAll();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindTreeViewChildNodes(int ParentId, TreeNode ParentNode)
    {
        try
        {
            AddUserPermissionData AUPermission = new AddUserPermissionData();
            DataTable _dt = AUPermission.SelectSection(ParentId);
             TreeNodeCollection nodes = ParentNode.ChildNodes;
            foreach (DataRow _dr in _dt.Rows)
            {
                TreeNode tn = new TreeNode();
                tn.Text = _dr["SectionName"].ToString();
                tn.Value = _dr["SectionId"].ToString();
                tn.Checked = true;
                DataTable _dtC = AUPermission.CheckPermissionExist(GUserId, Convert.ToInt32(tn.Value.ToString()));
                if (_dtC.Rows.Count > 0)
                {
                    DataRow myrow = _dtC.Rows[0];
                    tn.Checked = Convert.ToBoolean(myrow["Enable"]);
                }
                nodes.Add(tn);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void BindTreeViewParentNodesByModuleId(int ModuleId, TreeNodeCollection Nodes)
    {
        try
        {
            AddUserPermissionData AUPermission = new AddUserPermissionData();
            DataTable _dt = AUPermission.SelecModuleById(ModuleId);
            //TreeView2.Nodes.Clear();
            TreeNodeCollection nodes = Nodes;

            foreach (DataRow _dr in _dt.Rows)
            {
                TreeNode tn = new TreeNode();
                tn.Text = _dr["ModuleName"].ToString();
                tn.Value = _dr["ModuleId"].ToString();
                nodes.Add(tn);
                BindTreeViewChildNodes(Convert.ToInt32(_dr["ModuleId"].ToString()), tn);
            }
            //TreeView2.ExpandAll();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindProjectTreeView()
    {
        BindTreeViewParentNodesByModuleId(4, TreeViewProjects.Nodes);
        TreeViewProjects.ExpandAll();
    }

    private void BindSDTreeView()
    {
        BindTreeViewParentNodesByModuleId(5, TreeViewSD.Nodes);
        TreeViewSD.ExpandAll();
    }
    //Binding Control Panel Treeview
    private void BindCPTreeView()
    {
        BindTreeViewParentNodesByModuleId(6, TreeViewCP.Nodes);
        TreeViewCP.ExpandAll();
    }
    //Binding Dashboard Viewer TreeView
    private void BindDBVTreeView()
    {
        BindTreeViewParentNodesByModuleId(7, TreeViewDBV.Nodes);
        TreeViewDBV.ExpandAll();
        
    }
    //Binding Other Modules Treeview
    private void BindOMTreeView()
    {
        BindTVOtherModules();
    }

    protected void imgbtnPrjApply_Click(object sender, EventArgs e)
    {
        //need to add code for applying user project permissions
        try
        {

            int UserId = Convert.ToInt32(drContractors.SelectedValue.ToString());

            if ((drContractors.SelectedItem.Text != "Please select...") || (UserId != 0))
                foreach (TreeNode node1 in TreeViewProjects.Nodes)
                {
                    int SectionId = Convert.ToInt32(node1.Value.ToString());
                    AddUserPermissionData AUPermission = new AddUserPermissionData();
                    DataTable _dt1 = AUPermission.CheckPermissionExist(UserId, SectionId);
                    int retVal = -1;
                    //retVal = InsertAndUpdatePermission(node1, UserId);
                    foreach (TreeNode Cnode in node1.ChildNodes)
                    {
                        retVal = InsertAndUpdatePermission(Cnode, UserId);
                    }


                }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //imgbtnDBVApply_Click
    protected void imgbtnDBVApply_Click(object sender, EventArgs e)
    {
        try
        {

            int UserId = Convert.ToInt32(drContractors.SelectedValue.ToString());

            if ((drContractors.SelectedItem.Text != "Please select...") || (UserId != 0))
                foreach (TreeNode node1 in TreeViewDBV.Nodes)
                {
                    int SectionId = Convert.ToInt32(node1.Value.ToString());
                    AddUserPermissionData AUPermission = new AddUserPermissionData();
                    DataTable _dt1 = AUPermission.CheckPermissionExist(UserId, SectionId);
                    int retVal = -1;
                    //retVal = InsertAndUpdatePermission(node1, UserId);
                    foreach (TreeNode Cnode in node1.ChildNodes)
                    {
                        retVal = InsertAndUpdatePermission(Cnode, UserId);
                    }


                }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
    //imgbtnCPApply_Click
    protected void imgbtnCPApply_Click(object sender, EventArgs e)
    {
        try
        {

            int UserId = Convert.ToInt32(drContractors.SelectedValue.ToString());

            if ((drContractors.SelectedItem.Text != "Please select...") || (UserId != 0))
                foreach (TreeNode node1 in TreeViewCP.Nodes)
                {
                    int SectionId = Convert.ToInt32(node1.Value.ToString());
                    AddUserPermissionData AUPermission = new AddUserPermissionData();
                    DataTable _dt1 = AUPermission.CheckPermissionExist(UserId, SectionId);
                    int retVal = -1;
                    //retVal = InsertAndUpdatePermission(node1, UserId);
                    foreach (TreeNode Cnode in node1.ChildNodes)
                    {
                        retVal = InsertAndUpdatePermission(Cnode, UserId);
                    }


                }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    //imgbtnSDApply_Click
    protected void imgbtnSDApply_Click(object sender, EventArgs e)
    {
        try
        {

            int UserId = Convert.ToInt32(drContractors.SelectedValue.ToString());

            if ((drContractors.SelectedItem.Text != "Please select...") || (UserId != 0))
                foreach (TreeNode node1 in TreeViewSD.Nodes)
                {
                    int SectionId = Convert.ToInt32(node1.Value.ToString());
                    AddUserPermissionData AUPermission = new AddUserPermissionData();
                    DataTable _dt1 = AUPermission.CheckPermissionExist(UserId, SectionId);
                    int retVal = -1;
                    //retVal = InsertAndUpdatePermission(node1, UserId);
                    foreach (TreeNode Cnode in node1.ChildNodes)
                    {
                        retVal = InsertAndUpdatePermission(Cnode, UserId);
                    }


                }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //imgbtnOMApply_Click
    protected void imgbtnOMApply_Click(object sender, EventArgs e)
    {
        try
        {

            int UserId = Convert.ToInt32(drContractors.SelectedValue.ToString());

            if ((drContractors.SelectedItem.Text != "Please select...") || (UserId != 0))
                foreach (TreeNode node1 in TreeViewOM.Nodes)
                {
                    int SectionId = Convert.ToInt32(node1.Value.ToString());
                    AddUserPermissionData AUPermission = new AddUserPermissionData();
                    DataTable _dt1 = AUPermission.CheckPermissionExist(UserId, SectionId);
                    int retVal = -1;
                    //retVal = InsertAndUpdatePermission(node1, UserId);
                    foreach (TreeNode Cnode in node1.ChildNodes)
                    {
                        retVal = InsertAndUpdatePermission(Cnode, UserId);
                    }


                }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //imgbtnOMCancel_Click
    protected void imgbtnOMCancel_Click(object sender, EventArgs e)
    {
        //need to re-bind TreeViewOM
        Response.Redirect("~/WF/Admin/AdminUserPermissions.aspx?uid=" + Convert.ToInt32(drContractors.SelectedValue));
    }
    //imgbtnBBVCancel_Click
    protected void imgbtnDBVCancel_Click(object sender, EventArgs e)
    {
        //need to bind TreeViewDBV
        Response.Redirect("~/WF/Admin/AdminUserPermissions.aspx?uid=" + Convert.ToInt32(drContractors.SelectedValue));
    }
    //imgbtnCPCancel_Click
    protected void imgbtnCPCancel_Click(object sender, EventArgs e)
    {
        // need to bind TreeViewCP
        Response.Redirect("~/WF/Admin/AdminUserPermissions.aspx?uid=" + Convert.ToInt32(drContractors.SelectedValue));
    }
    //imgbtnSDCancel_Click
    protected void imgbtnSDCancel_Click(object sender, EventArgs e)
    {
        //need to bind TreeviewSD
        Response.Redirect("~/WF/Admin/AdminUserPermissions.aspx?uid=" + Convert.ToInt32(drContractors.SelectedValue));
    }

    protected void imgbtnPrjCancel_Click(object sender, EventArgs e)
    {
        //need to write code for cancel of project tab tree
        //TreeViewProjects.Nodes.Clear();
        //BindProjectTreeView();
        Response.Redirect("~/WF/Admin/AdminUserPermissions.aspx?uid=" + Convert.ToInt32(drContractors.SelectedValue));
       
    }
    protected void imgCntrlApply_Click(object sender, EventArgs e)
    {
        try
        {
           
            int UserId = Convert.ToInt32(drContractors.SelectedValue.ToString());
           
            if ((drContractors.SelectedItem.Text != "Please select...") || (UserId != 0))
            foreach (TreeNode node1 in TreeView2.Nodes)
            {
                int SectionId = Convert.ToInt32(node1.Value.ToString());
                AddUserPermissionData AUPermission = new AddUserPermissionData();
                DataTable _dt1 = AUPermission.CheckPermissionExist(UserId, SectionId);
                int retVal = -1;
                //retVal = InsertAndUpdatePermission(node1, UserId);
                foreach (TreeNode Cnode in node1.ChildNodes)
                {
                    retVal = InsertAndUpdatePermission(Cnode, UserId);
                }
               

            }
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private int InsertAndUpdatePermission(TreeNode node,int UserId)
    {
        int retVal =-1;
        int SectionId = Convert.ToInt32(node.Value.ToString());
        AddUserPermissionData AUPermission = new AddUserPermissionData();
        DataTable _dt = AUPermission.CheckPermissionExist(UserId, SectionId);
        bool CheckState = false;
                if (node.Checked == true)
                {
                    CheckState = true;
                }
                
                if (_dt.Rows.Count > 0)
                {
                    DataRow _dr = _dt.Rows[0];
                    retVal = AUPermission.UpdateUserPermission(Convert.ToInt32(_dr["Id"].ToString()), UserId, SectionId, CheckState);
                }
                else
                {
                    retVal = AUPermission.InsertUserPermission(UserId, SectionId, CheckState);
                }

        return retVal;
    }
   
    protected void imgCntrlCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Admin/AdminUserPermissions.aspx?uid=" + Convert.ToInt32(drContractors.SelectedValue));
    }
    protected void drContractors_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Admin/AdminUserPermissions.aspx?uid=" + Convert.ToInt32(drContractors.SelectedValue));
    }
}
