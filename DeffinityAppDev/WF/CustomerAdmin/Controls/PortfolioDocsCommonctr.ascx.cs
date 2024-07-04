using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using Deffinity.BLL;
//using Deffinity.DocumentSearch;
using System.Text;
using System.Collections.Generic;
using Deffinity.PortfolioManager;
using System.IO;
using Ionic.Zip;
public partial class controls_PortfolioDocsCommonctr : System.Web.UI.UserControl
{
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    private string cs = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    ArrayList hif = new ArrayList();
    int folderID = 0;
    int projectID = 0;

    protected double DSize = 0;
    delegate void displayDelegate(SearchResultList searchResults);


    protected void Page_Load(object sender, EventArgs e)
    {
        
        //txtContractorID.Value = sessionKeys.UID.ToString();
        //<asp:ControlParameter DefaultValue="-1" ControlID="txtContractorID" Name="ContractorID" PropertyName="Value" />
        Response.Cache.SetNoStore();
        lblMsg.Text = string.Empty;
        if (!IsPostBack)
        {
            HiddenCustomerdocs.Value = selectPortFolio();
            if (HiddenCustomerdocs.Value.Trim() != "")
            {
                chkAllowCustomers.Checked = Convert.ToBoolean(HiddenCustomerdocs.Value);
            }
        }
        
        
       
        Session["CustomerPortalID"] = "0";            
        if ((Page.Request.Path.ToLower().Contains("portfoliodocscommon.aspx")) || (Page.Request.Path.ToLower().Contains("portfoliodocs.aspx")))
        {
            CustomerAndPortalAdmin_Load();
            if (sessionKeys.PortalUser)
            {
                CustomerPortal_CommonDocs_visible(false);
            }
            else
            {
                CustomerPortal_CommonDocs_visible(true);
            }

        }

        else if (Page.Request.Path.ToLower().Contains("healthcheckformdocs.aspx"))
        {
            
            Healthdocuments_Load();


        }
        else if (Page.Request.Path.ToLower().Contains("sddocuments_frm.aspx"))
        {

            SDdocuments_Load();
        }

        else if (Page.Request.Path.ToLower().Contains("mytasksdocuments.aspx"))
        {

            MyTaskdocuments_Load();
        }
        else if (Page.Request.Path.ToLower().Contains("rfidocuments.aspx"))
        {

            //MyTaskdocuments_Load();
            RFIdocuments_Load();
        }
        else if (Page.Request.Path.ToLower().Contains("customerprojectdocuments.aspx"))
        {
            Session["CustomerPortalID"] = "1";
            CustomerProjectdocuments_Load();
            CustomerProjectsControlsInVisible();

            
            

        }
        else
        {
            Projectdocuments_Load();
        }
    }


    protected void CustomerProjectsControlsInVisible()
    {
        Literal1.Visible = false;
        txtCreateFolder.Visible = false;
        lnkFolder.Visible = false;
        lnkRenameFolder.Visible = false;
        lnkDeleteFolder.Visible = false;
        btnResetCheckout.Visible = false;
        //IMAGE1.Visible = false;
        //LABEL2.Visible = false;
        btnCopyFile.Visible = false;
        //IMAGE4.Visible = false;
        //lblCopyFile.Visible = false;
        btnPasteFile.Visible = false;
        //IMAGE5.Visible = false;
        //lblPasteFile.Visible = false;
        btnMoveFile.Visible = false;
        //IMAGE6.Visible = false;
       // Label1.Visible = false;
        btnDeleteFile.Visible = false;
       // IMAGE7.Visible = false;
        //Label3.Visible = false;
       // Literal4.Visible = false;
        txtSearchBox1.Visible = false;
        ImgBtnsearch1.Visible = false;
        ddlProject.Visible = false;
        PnlFileUpload.Visible = false;
        Literal2.Visible = false;
        FileUpload1.Visible = false;
        btnUpload.Visible = false;

       // gridFiles.Columns[0].Visible = false;

        gridFiles.Columns[4].Visible = false;
        gridFiles.Columns[5].Visible = false;
        gridFiles.Columns[6].Visible = false;
        gridFiles.Columns[7].Visible = false;
        gridFiles.Columns[8].Visible = false;
        gridFiles.Columns[9].Visible = false;
        gridFiles.Columns[10].Visible = false;
        gridFiles.Columns[11].Visible = false;
        gridFiles.Columns[12].Visible = false;
        gridFiles.Columns[13].Visible = true;

        
    }
    protected void CustomerPortal_CommonDocs_visible(bool visibility)
    {
        gridFiles.Columns[6].Visible = visibility;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        ViewState["update"] = Session["update"];
        if ((ViewState["Links"] == null))
        {

            if ((Page.Request.Path.ToLower().Contains("portfoliodocscommon.aspx")) || (Page.Request.Path.ToLower().Contains("portfoliodocs.aspx")))
            {

                CustomerAndPortalAdmin_PreRender();
            }
            else if (Page.Request.Path.ToLower().Contains("healthcheckformdocs.aspx"))
            {
                healthdocuments_PreRender();
            }
            else if (Page.Request.Path.ToLower().Contains("sddocuments_frm.aspx"))
            {

                SDdocuments_PreRender();
            }
            else if (Page.Request.Path.ToLower().Contains("mytasksdocuments.aspx"))
            {

                MyTaskdocuments_PreRender();
            }
            else if (Page.Request.Path.ToLower().Contains("rfidocuments.aspx"))
            {

                //MyTaskdocuments_Load();
                RFIdocuments_PreRender();
            }
            else if (Page.Request.Path.ToLower().Contains("customerprojectdocuments.aspx"))
            {

                //MyTaskdocuments_Load();
                //RFIdocuments_PreRender();
                CustomerProjectdocuments_PreRender();
                CustomerProjectsControlsInVisible();
            }
            else
            {
                Projectdocuments_PreRender();
            }

            //gridFiles.Rows.Count.ToString()

        }
        //lblCounter.Text = Resources.DeffinityRes.Contains /*"Contains: "*/ + gridFiles.Rows.Count.ToString() + Resources.DeffinityRes.files /*" file(s)"*/;
        //lblSpaceUsed.Text = Resources.DeffinityRes.TotalFilesSize /*"Total Files Size: " */ + GetTotalFilesSize() + Resources.DeffinityRes.KB /*" KB"*/;

        lblCounter.Text = Resources.DeffinityRes.Contains +" :"+ gridFiles.Rows.Count.ToString() + Resources.DeffinityRes.files ;
        lblSpaceUsed.Text = Resources.DeffinityRes.TotalFilesSize  + GetTotalFilesSize() + Resources.DeffinityRes.KB ;

        ViewState["Links"] = null;
    }
    
    //Recursively loop through all the child nodes.
    private void LoopThroughNodes(Infragistics.WebUI.UltraWebNavigator.Nodes nodes)
    {
        foreach (Infragistics.WebUI.UltraWebNavigator.Node node in nodes)
        {
            if (Convert.ToInt32(folderID) == Convert.ToInt32(node.DataKey))
            {
                int selectedNode = Convert.ToInt32(folderID);
                string nodeText = node.Text;
                node.Selected = true;
                break;
            }
            else
                if (node.Nodes.Count > 0)
                    LoopThroughNodes(node.Nodes);
        }
    }

    #region TreeView Binding

    protected void CustomerPortalProjectDocuments_NewTreeBinding()
   {
   
        DataSet ds11 = new DataSet();
        SqlCommand cmdSQL1 = new SqlCommand();
        cmdSQL1.CommandType = CommandType.StoredProcedure;
        cmdSQL1.Parameters.Clear();
        cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS";
        cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        //}
        cmdSQL1.Connection = cn;
        SqlDataAdapter da11 = new SqlDataAdapter(cmdSQL1);
        da11.Fill(ds11, "MasterDoc");
        try
        {
            foreach (DataRow row in ds11.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                string folderName = row["FolderName"].ToString();
                string FolderNameReal = row["FolderNameReal"].ToString();

                string imageUrl = row["image"].ToString();
                int parentID = Convert.ToInt32(row["ParentID"]);
                Infragistics.WebUI.UltraWebNavigator.Node node = new Infragistics.WebUI.UltraWebNavigator.Node();
                node.Text = "<i class='fa fa-folder'></i> " + folderName;
                node.Tag = FolderNameReal;
                //node.Tag = "";
                node.DataKey = id;
               // node.ImageUrl = imageUrl;
                node.Styles.Padding.Bottom = 0;
                if (parentID == 0) UltraWebTree1.Nodes[0].Nodes.Add(node);
                else AddChild(UltraWebTree1.Nodes[0].Nodes, node, parentID);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
  
        //DataSet ds11 = new DataSet();
        //SqlCommand cmdSQL1 = new SqlCommand();
        //cmdSQL1.CommandType = CommandType.StoredProcedure;
        //cmdSQL1.Parameters.Clear();

        //if (sessionKeys.SID == 8)
        //{
        //    cmdSQL1.CommandText = "DEFFINITY_RFI_VENDORDOCUMENTS_SELECT";
        //    //cmdSQL1.Parameters.AddWithValue("@contractorid", sessionKeys.UID);
        //    cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        //}
        //else if (QueryStringValues.Vendor != 0)
        //{
        //    int CID = 0;
        //    cmdSQL1.CommandText = "DEFFINITY_RFI_VENDORDOCUMENTS_SELECT";
        //    //cmdSQL1.Parameters.AddWithValue("@contractorid", CID);
        //    cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        //}
        //else
        //{
        //    if (QueryStringValues.Project > 0)
        //    {
        //        cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS";
        //        cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        //        //make incident session ID
        //        sessionKeys.IncidentID = 0;
        //    }
        //    else if (Request.QueryString["mode"] != null && Request.QueryString["mode"] == "central")
        //    {
        //        cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS_Centralized";
        //        cmdSQL1.Parameters.AddWithValue("@Projectreference", 0);
        //    }
        //    else
        //    {
        //        //cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS_BYINCIDENT";
        //        //cmdSQL1.Parameters.AddWithValue("@SDID", sessionKeys.IncidentID);
        //    }
        //}
        //cmdSQL1.Connection = cn;

        //SqlDataAdapter da11 = new SqlDataAdapter(cmdSQL1);
        //da11.Fill(ds11, "MasterDoc");

        //try
        //{
        //    foreach (DataRow row in ds11.Tables[0].Rows)
        //    {
        //        int id = Convert.ToInt32(row["ID"]);
        //        string folderName = row["FolderName"].ToString();
        //        string FolderNameReal = row["FolderNameReal"].ToString();

        //        string imageUrl = row["image"].ToString();
        //        int parentID = Convert.ToInt32(row["ParentID"]);
        //        Infragistics.WebUI.UltraWebNavigator.Node node = new Infragistics.WebUI.UltraWebNavigator.Node();
        //        node.Text = folderName;
        //        node.Tag = FolderNameReal;
        //        //node.Tag = "";
        //        node.DataKey = id;
        //        node.ImageUrl = imageUrl;
        //        node.Styles.Padding.Bottom = 0;
        //        if (parentID == 0) UltraWebTree1.Nodes[0].Nodes.Add(node);
        //        else AddChild(UltraWebTree1.Nodes[0].Nodes, node, parentID);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    LogExceptions.WriteExceptionLog(ex);
        //}
    }

    protected void CustomerAndPortalAdmin_NewTreeBinding()
    {
        DataSet ds11 = new DataSet();
        SqlCommand cmdSQL1 = new SqlCommand();
        cmdSQL1.CommandType = CommandType.StoredProcedure;
        cmdSQL1.Parameters.Clear();
        cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS_Portfolio";
        cmdSQL1.Parameters.AddWithValue("@PortfolioID", -99);
        //}
        cmdSQL1.Connection = cn;
        SqlDataAdapter da11 = new SqlDataAdapter(cmdSQL1);
        da11.Fill(ds11, "MasterDoc");
        try
        {
            foreach (DataRow row in ds11.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                string folderName = row["FolderName"].ToString();
                string FolderNameReal = row["FolderNameReal"].ToString();

                string imageUrl = row["image"].ToString();
                int parentID = Convert.ToInt32(row["ParentID"]);
                Infragistics.WebUI.UltraWebNavigator.Node node = new Infragistics.WebUI.UltraWebNavigator.Node();
                node.Text = "<i class='fa fa-folder'></i> " + folderName;
                node.Tag = FolderNameReal;
                //node.Tag = "";
                node.DataKey = id;
                //node.ImageUrl = imageUrl;
                node.Styles.Padding.Bottom = 0;
                if (parentID == 0) UltraWebTree1.Nodes[0].Nodes.Add(node);
                else AddChild(UltraWebTree1.Nodes[0].Nodes, node, parentID);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void MyTasksdocuments_NewTreeBinding()
    {
        DataSet ds11 = new DataSet();
        SqlCommand cmdSQL1 = new SqlCommand();
        cmdSQL1.CommandType = CommandType.StoredProcedure;
        cmdSQL1.Parameters.Clear();

        if (sessionKeys.SID == 8)
        {
            cmdSQL1.CommandText = "DEFFINITY_RFI_VENDORDOCUMENTS_SELECT";
           // cmdSQL1.Parameters.AddWithValue("@contractorid", sessionKeys.UID);
            cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        }
        else if (QueryStringValues.Vendor != 0)
        {
            int CID = 0;
            cmdSQL1.CommandText = "DEFFINITY_RFI_VENDORDOCUMENTS_SELECT";
            //cmdSQL1.Parameters.AddWithValue("@contractorid", CID);
            cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        }
        else
        {
            if (QueryStringValues.Project > 0)
            {
                cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS";
                cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
                //make incident session ID
                sessionKeys.IncidentID = 0;
            }
            else if (Request.QueryString["mode"] != null && Request.QueryString["mode"] == "central")
            {
                cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS_Centralized";
                cmdSQL1.Parameters.AddWithValue("@Projectreference", 0);
            }
            else
            {
                //cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS_BYINCIDENT";
                //cmdSQL1.Parameters.AddWithValue("@SDID", sessionKeys.IncidentID);
            }
        }
        cmdSQL1.Connection = cn;

        SqlDataAdapter da11 = new SqlDataAdapter(cmdSQL1);
        da11.Fill(ds11, "MasterDoc");

        try
        {
            foreach (DataRow row in ds11.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                string folderName = row["FolderName"].ToString();
                string FolderNameReal = row["FolderNameReal"].ToString();

                string imageUrl = row["image"].ToString();
                int parentID = Convert.ToInt32(row["ParentID"]);
                Infragistics.WebUI.UltraWebNavigator.Node node = new Infragistics.WebUI.UltraWebNavigator.Node();
                node.Text = "<i class='fa fa-folder'></i> " + folderName;
                node.Tag = FolderNameReal;
                //node.Tag = "";
                node.DataKey = id;
                //node.ImageUrl = imageUrl;
                node.Styles.Padding.Bottom = 0;
                if (parentID == 0) UltraWebTree1.Nodes[0].Nodes.Add(node);
                else AddChild(UltraWebTree1.Nodes[0].Nodes, node, parentID);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }



    protected void RFIdocuments_NewTreeBinding()
    {
        DataSet ds11 = new DataSet();
        SqlCommand cmdSQL1 = new SqlCommand();
        cmdSQL1.CommandType = CommandType.StoredProcedure;
        cmdSQL1.Parameters.Clear();

        if (sessionKeys.SID == 8)
        {
            cmdSQL1.CommandText = "DEFFINITY_RFI_VENDORDOCUMENTS_SELECT";
            //cmdSQL1.Parameters.AddWithValue("@contractorid", sessionKeys.UID);
            cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        }
        else if (QueryStringValues.Vendor != 0)
        {
            int CID = 0;
            cmdSQL1.CommandText = "DEFFINITY_RFI_VENDORDOCUMENTS_SELECT";
            //cmdSQL1.Parameters.AddWithValue("@contractorid", CID);
            cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        }
        else
        {
            
            if (QueryStringValues.Project > 0)
            {
                cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS";
                cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
                //make incident session ID
                sessionKeys.IncidentID = 0;
            }
            else if (Request.QueryString["project"] != null)
            {
                cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS";
                cmdSQL1.Parameters.AddWithValue("@Projectreference", Convert.ToInt32(Request.QueryString["project"].ToString()));
                //make incident session ID
                sessionKeys.IncidentID = 0;

            }

            else if (Request.QueryString["mode"] != null && Request.QueryString["mode"] == "central")
            {
                cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS_Centralized";
                cmdSQL1.Parameters.AddWithValue("@Projectreference", 0);
            }
            else
            {
                //cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS_BYINCIDENT";
                //cmdSQL1.Parameters.AddWithValue("@SDID", sessionKeys.IncidentID);
            }
        }
        cmdSQL1.Connection = cn;

        SqlDataAdapter da11 = new SqlDataAdapter(cmdSQL1);
        da11.Fill(ds11, "MasterDoc");

        try
        {
            foreach (DataRow row in ds11.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                string folderName = row["FolderName"].ToString();
                string FolderNameReal = row["FolderNameReal"].ToString();

                string imageUrl = row["image"].ToString();
                int parentID = Convert.ToInt32(row["ParentID"]);
                Infragistics.WebUI.UltraWebNavigator.Node node = new Infragistics.WebUI.UltraWebNavigator.Node();
                node.Text = "<i class='fa fa-folder'></i> " + folderName;
                node.Tag = FolderNameReal;
                //node.Tag = "";
                node.DataKey = id;
                //node.ImageUrl = imageUrl;
                node.Styles.Padding.Bottom = 0;
                if (parentID == 0) UltraWebTree1.Nodes[0].Nodes.Add(node);
                else AddChild(UltraWebTree1.Nodes[0].Nodes, node, parentID);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    protected void CustomerProjectdocuments_NewTreeBinding()
    {
        DataSet ds11 = new DataSet();
        SqlCommand cmdSQL1 = new SqlCommand();
        cmdSQL1.CommandType = CommandType.StoredProcedure;
        cmdSQL1.Parameters.Clear();

        if (sessionKeys.SID == 8)
        {
            cmdSQL1.CommandText = "DEFFINITY_RFI_VENDORDOCUMENTS_SELECT";
            //cmdSQL1.Parameters.AddWithValue("@contractorid", sessionKeys.UID);
            cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        }
        else if (QueryStringValues.Vendor != 0)
        {
            int CID = 0;
            cmdSQL1.CommandText = "DEFFINITY_RFI_VENDORDOCUMENTS_SELECT";
            //cmdSQL1.Parameters.AddWithValue("@contractorid", CID);
            cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        }
        else
        {
            if (QueryStringValues.Project > 0)
            {
                cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS_CustomerPortal";
                cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
                //make incident session ID
                sessionKeys.IncidentID = 0;
            }
            else if (Request.QueryString["mode"] != null && Request.QueryString["mode"] == "central")
            {
                cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS_Centralized";
                cmdSQL1.Parameters.AddWithValue("@Projectreference", 0);
            }
            else
            {
                //cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS_BYINCIDENT";
                //cmdSQL1.Parameters.AddWithValue("@SDID", sessionKeys.IncidentID);
            }
        }
        cmdSQL1.Connection = cn;

        SqlDataAdapter da11 = new SqlDataAdapter(cmdSQL1);
        da11.Fill(ds11, "MasterDoc");

        try
        {
            foreach (DataRow row in ds11.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                string folderName = row["FolderName"].ToString();
                string FolderNameReal = row["FolderNameReal"].ToString();

                string imageUrl = row["image"].ToString();
                int parentID = Convert.ToInt32(row["ParentID"]);
                Infragistics.WebUI.UltraWebNavigator.Node node = new Infragistics.WebUI.UltraWebNavigator.Node();
                node.Text = "<i class='fa fa-folder'></i> " + folderName;
                node.Tag = FolderNameReal;
                //node.Tag = "";
                node.DataKey = id;
                //node.ImageUrl = imageUrl;
                node.Styles.Padding.Bottom = 0;
                if (parentID == 0) UltraWebTree1.Nodes[0].Nodes.Add(node);
                else AddChild(UltraWebTree1.Nodes[0].Nodes, node, parentID);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void Projectdocuments_NewTreeBinding()
    {
        DataSet ds11 = new DataSet();
        SqlCommand cmdSQL1 = new SqlCommand();
        cmdSQL1.CommandType = CommandType.StoredProcedure;
        cmdSQL1.Parameters.Clear();

        if (sessionKeys.SID == 8)
        {
            cmdSQL1.CommandText = "DEFFINITY_RFI_VENDORDOCUMENTS_SELECT";
            //cmdSQL1.Parameters.AddWithValue("@contractorid", sessionKeys.UID);
            cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        }
        else if (QueryStringValues.Vendor != 0)
        {
            int CID = 0;
            cmdSQL1.CommandText = "DEFFINITY_RFI_VENDORDOCUMENTS_SELECT";
            //cmdSQL1.Parameters.AddWithValue("@contractorid", CID);
            cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        }
        else
        {
            if (QueryStringValues.Project > 0)
            {
                cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS";
                cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
                //make incident session ID
                sessionKeys.IncidentID = 0;
            }
            else if (Request.QueryString["mode"] != null && Request.QueryString["mode"] == "central")
            {
                cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS_Centralized";
                cmdSQL1.Parameters.AddWithValue("@Projectreference", 0);
            }
            else
            {
                //cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS_BYINCIDENT";
                //cmdSQL1.Parameters.AddWithValue("@SDID", sessionKeys.IncidentID);
            }
        }
        cmdSQL1.Connection = cn;

        SqlDataAdapter da11 = new SqlDataAdapter(cmdSQL1);
        da11.Fill(ds11, "MasterDoc");

        try
        {
            foreach (DataRow row in ds11.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                string folderName = row["FolderName"].ToString();
                string FolderNameReal = row["FolderNameReal"].ToString();

                string imageUrl = row["image"].ToString();
                int parentID = Convert.ToInt32(row["ParentID"]);
                Infragistics.WebUI.UltraWebNavigator.Node node = new Infragistics.WebUI.UltraWebNavigator.Node();
                node.Text = "<i class='fa fa-folder'></i> " + folderName;
                node.Tag = FolderNameReal;
                //node.Tag = "";
                node.DataKey = id;
               // node.ImageUrl = imageUrl;
                node.Styles.Padding.Bottom = 0;
                if (parentID == 0) UltraWebTree1.Nodes[0].Nodes.Add(node);
                else AddChild(UltraWebTree1.Nodes[0].Nodes, node, parentID);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void Healthdocuments_NewTreeBinding()
    {
        DataSet ds11 = new DataSet();
        SqlCommand cmdSQL1 = new SqlCommand();
        cmdSQL1.CommandType = CommandType.StoredProcedure;
        cmdSQL1.Parameters.Clear();

        //if (sessionKeys.SID == 8)
        //{
        //    cmdSQL1.CommandText = "DEFFINITY_RFI_VENDORDOCUMENTS_SELECT";
        //    cmdSQL1.Parameters.AddWithValue("@contractorid", sessionKeys.UID);
        //    cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        //}
        //else if (QueryStringValues.Vendor != 0)
        //{
        //    int CID = 0;
        //    cmdSQL1.CommandText = "DEFFINITY_RFI_VENDORDOCUMENTS_SELECT";
        //    cmdSQL1.Parameters.AddWithValue("@contractorid", CID);
        //    cmdSQL1.Parameters.AddWithValue("@Projectreference", QueryStringValues.Project);
        //}
        //else
        //{
            if (QueryStringValues.HealthCheckId > 0)
            {
                cmdSQL1.CommandText = "DEFFINITY_Health_GETDOCUMENTS";
                cmdSQL1.Parameters.AddWithValue("@HealthCheckId", QueryStringValues.HealthCheckId);
                //make incident session ID
                sessionKeys.IncidentID = 0;
            }
            else if (Request.QueryString["mode"] != null && Request.QueryString["mode"] == "central")
            {
                cmdSQL1.CommandText = "DEFFINITY_Health_GETDOCUMENTS_Centralized";
                cmdSQL1.Parameters.AddWithValue("@HealthCheckId", 0);
            }            
        //}
        cmdSQL1.Connection = cn;

        SqlDataAdapter da11 = new SqlDataAdapter(cmdSQL1);
        da11.Fill(ds11, "MasterDoc");

        try
        {
            foreach (DataRow row in ds11.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                string folderName = row["FolderName"].ToString();
                string FolderNameReal = row["FolderNameReal"].ToString();

                string imageUrl = row["image"].ToString();
                int parentID = Convert.ToInt32(row["ParentID"]);
                Infragistics.WebUI.UltraWebNavigator.Node node = new Infragistics.WebUI.UltraWebNavigator.Node();
                node.Text = "<i class='fa fa-folder'></i> " + folderName;
                node.Tag = FolderNameReal;
                //node.Tag = "";
                node.DataKey = id;
                //node.ImageUrl = imageUrl;
                node.Styles.Padding.Bottom = 0;
                if (parentID == 0) UltraWebTree1.Nodes[0].Nodes.Add(node);
                else AddChild(UltraWebTree1.Nodes[0].Nodes, node, parentID);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void SDdocuments_NewTreeBinding()
    {
        DataSet ds11 = new DataSet();
        SqlCommand cmdSQL1 = new SqlCommand();
        cmdSQL1.CommandType = CommandType.StoredProcedure;
        cmdSQL1.Parameters.Clear();

        if (Request.QueryString["SDID"] != null) 
        {
            cmdSQL1.CommandText = "DEFFINITY_GETDOCUMENTS_BYINCIDENT";
            cmdSQL1.Parameters.AddWithValue("@SDID", Convert.ToInt32(Request.QueryString["SDID"].ToString()));
        }


        //}
        cmdSQL1.Connection = cn;

        SqlDataAdapter da11 = new SqlDataAdapter(cmdSQL1);
        da11.Fill(ds11, "MasterDoc");

        try
        {
            foreach (DataRow row in ds11.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                string folderName = row["FolderName"].ToString();
                string FolderNameReal = row["FolderNameReal"].ToString();

                string imageUrl = row["image"].ToString();
                int parentID = Convert.ToInt32(row["ParentID"]);
                Infragistics.WebUI.UltraWebNavigator.Node node = new Infragistics.WebUI.UltraWebNavigator.Node();
                node.Text = "<i class='fa fa-folder'></i> " + folderName;
                node.Tag = FolderNameReal;
                //node.Tag = "";
                node.DataKey = id;
                //node.ImageUrl = imageUrl;
                node.Styles.Padding.Bottom = 0;
                if (parentID == 0) UltraWebTree1.Nodes[0].Nodes.Add(node);
                else AddChild(UltraWebTree1.Nodes[0].Nodes, node, parentID);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void NewTreeBinding()
    {
        UltraWebTree1.Nodes.Clear();
        Infragistics.WebUI.UltraWebNavigator.Node rootNode = new Infragistics.WebUI.UltraWebNavigator.Node();
        rootNode.DataKey = 0;
        rootNode.Text = Resources.DeffinityRes.FilesFolders;// "Files/Folders";
        rootNode.Tag = Resources.DeffinityRes.FilesFolders;// "Files/Folders";
        UltraWebTree1.Nodes.Add(rootNode);


        if ((Page.Request.Path.ToLower().Contains("portfoliodocscommon.aspx")) || (Page.Request.Path.ToLower().Contains("portfoliodocs.aspx")))
        {
            CustomerAndPortalAdmin_NewTreeBinding();
        }

        else if (Page.Request.Path.ToLower().Contains("healthcheckformdocs.aspx"))
        {
            Healthdocuments_NewTreeBinding();
        }
        else if (Page.Request.Path.ToLower().Contains("sddocuments_frm.aspx"))
        {
            SDdocuments_NewTreeBinding();
        }

        else if (Page.Request.Path.ToLower().Contains("mytasksdocuments.aspx"))
        {
            MyTasksdocuments_NewTreeBinding();
        }
        else if (Page.Request.Path.ToLower().Contains("rfidocuments.aspx"))
        {
            RFIdocuments_NewTreeBinding();
        }
        else if (Page.Request.Path.ToLower().Contains("customerprojectdocuments.aspx"))
        {
            CustomerProjectdocuments_NewTreeBinding();
            CustomerProjectsControlsInVisible();
        }
            
            //RFIDocuments.aspx
        else
        {
            Projectdocuments_NewTreeBinding();

        }
        UltraWebTree1.ExpandAll();
    }
    
    private void AddChild(Infragistics.WebUI.UltraWebNavigator.Nodes nodes, Infragistics.WebUI.UltraWebNavigator.Node addNode, int parentID)
    {
        foreach (Infragistics.WebUI.UltraWebNavigator.Node node in nodes)
        {
            if (Convert.ToInt32(node.DataKey) == parentID)
            {
                node.Nodes.Add(addNode);
                break;
            }
            else
                AddChild(node.Nodes, addNode, parentID);
        }
    }

    private string DeletedNodesString(Infragistics.WebUI.UltraWebNavigator.Nodes nodes, string strDeletedIDs)
    {
        foreach (Infragistics.WebUI.UltraWebNavigator.Node node in nodes)
        {
            if (node.Nodes.Count > 0)
                strDeletedIDs = DeletedNodesString(node.Nodes, strDeletedIDs);
            strDeletedIDs += node.DataKey.ToString() + ",";
        }
        return strDeletedIDs;
    }
   
 #endregion TreeView Binding

    #region Control Events

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("{0}?project={1}", Request.Url.AbsolutePath, ddlProject.SelectedValue));
    }

    protected void btnUpdateDoc_Click(object sender, EventArgs e)
    {
        //chkAllowCustomers.Checked
        updatePortfolio_Doc();
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {

        if (Session["update"].ToString() == ViewState["update"].ToString())
        {
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            UploadDocuments();

        }
    }
    private void updatePortfolio_Doc()
    {
        HiddenCustomerdocs.Value = chkAllowCustomers.Checked.ToString();
        try
        {
            SqlConnection cn = new SqlConnection(cs);

            //if (ddlPortfolio.SelectedValue != "Please select...")
            //{

            using (SqlCommand cmd = new SqlCommand("DN_PortfolioUpdate_Docenable", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("ID", sessionKeys.PortfolioID);
                cmd.Parameters.AddWithValue("docenable", chkAllowCustomers.Checked);
                cn.Open();
                try
                {

                    cmd.ExecuteNonQuery();
                    cn.Close();


                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void CloseModel1_Click(object sender, EventArgs e)
    {
        ModalControlExtender1.Hide();
    }
    protected void linkFolder_Click(object sender, EventArgs e)
    {

        if (Session["update"].ToString() == ViewState["update"].ToString())
        {
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            ImageButton linkFolder = (ImageButton)sender;
            if (linkFolder.CommandName == "CreateFolder")
            {
                if (Page.Request.Path.ToLower().Contains("healthcheckformdocs.aspx"))
                {
                    Health_CreateFolder();
                }
                else
                {
                    CreateFolder();
                }
            }
            else if (linkFolder.CommandName == "RenameFolder")
            {
                RenameFolder();
            }
            else if (linkFolder.CommandName == "DeleteFolder")
            {
                DeleteFolder();
            }
        }

    }
   
    //protected void ImageFile_Click(object sender, EventArgs e)
    //{
    //    if (Session["update"].ToString() == ViewState["update"].ToString())
    //    {
    //        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
    //        ImageButton linkFile = (ImageButton)sender;
    //        //txtSearchBox2
    //        string strSearch = "";

    //        if (linkFile.CommandName == "SearchFile1")
    //        {
    //            strSearch = txtSearchBox1.Text.Trim();
    //            txtSearchBox2.Text = strSearch;
    //        }
    //        if (linkFile.CommandName == "SearchFile2")
    //        {
    //            strSearch = txtSearchBox2.Text.Trim();
    //        }
    //        if (!string.IsNullOrEmpty(strSearch))
    //            ModalControlExtender2.Show();
    //        searchTextFiles(new displayDelegate(DisplayResults), strSearch);

    //        //SearchDocuments(strSearch);                

    //    }
    //}
    protected void ImageFile_Click(object sender, EventArgs e)
    {
        if (Session["update"].ToString() == ViewState["update"].ToString())
        {
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            ImageButton linkFile = (ImageButton)sender;
            //txtSearchBox2
            string strSearch = "";

            if (linkFile.ToolTip.ToLower().Contains("check out"))
            {
                CheckInOutUpdate(true, linkFile.CommandArgument);
            }
            else if (linkFile.ToolTip.ToLower().Contains("check in"))
            {
                CheckInOutUpdate(false, linkFile.CommandArgument);
            }
            if (linkFile.CommandName.Trim()=="ResetCheckOut")
            {
                CheckInOutUpdate(false, linkFile.CommandArgument);
            }
            
            if (linkFile.CommandName == "SearchFile1")
            {
                strSearch = txtSearchBox1.Text.Trim();
                txtSearchBox2.Text = strSearch;
            }
            if (linkFile.CommandName == "SearchFile2")
            {
                strSearch = txtSearchBox2.Text.Trim();
            }
            //Dinesh
            //if (!string.IsNullOrEmpty(strSearch))
            //    ModalControlExtender2.Show();
            //searchTextFiles(new displayDelegate(DisplayResults), strSearch);

            ////SearchDocuments(strSearch);                

        }
    }
    private void CheckInOutUpdate(bool check, string CommandArgumentID)
    {
        lblMsg.Text = string.Empty;
        string strDeleteIDS = string.Empty;
        int intChecked = 0;
        //foreach (GridViewRow dgi in gridFiles.Rows)
        //{
        //    CheckBox chkChecked = (CheckBox)dgi.FindControl("chkChecked");
        //    if (chkChecked.Checked)
        //    {
        //        intChecked++;
        //        Label lblID = (Label)dgi.FindControl("lblID");
        //        if (strDeleteIDS == string.Empty)
        //        {
        //            strDeleteIDS = lblID.Text.Trim() + ",";
        //        }
        //        else
        //        {
        //            strDeleteIDS = strDeleteIDS + lblID.Text.Trim() + ",";
        //        }
        //    }
        //}

        strDeleteIDS = CommandArgumentID + ",";
        if (strDeleteIDS != string.Empty)
        {
            AC2P_DocumentsController ObjAC2P_DocumentsController = new AC2P_DocumentsController();

            int intRet = ObjAC2P_DocumentsController.AC2PDocumentsCheckOut(strDeleteIDS, check, sessionKeys.UID);
            
            if (check == true)
            {
                lblMsg.Text = intChecked.ToString() + Resources.DeffinityRes.CheckOutFiles; //" file(s) are Checked Out.";
            }
            else
            {
                lblMsg.Text = intChecked.ToString() + Resources.DeffinityRes.CheckInFiles; //" file(s) are Checked In.";
            }
        }
        else
        {
            if (check == true)
            {
                lblMsg.Text = Resources.DeffinityRes.CheckOutSelPlsMsg; //"Plese select file(s) to Check Out.";
            }
            else
            {
                lblMsg.Text = Resources.DeffinityRes.CheckInSelPlsMsg;  //"Plese select file(s) to Check In.";
            }
        }
    }


    //protected void CheckBoxInvisible()
    //{
    //    //lblMsg.Text = string.Empty;
    //    string strDeleteIDS = string.Empty;

    //    foreach (GridViewRow dgi in gridFiles.Rows)
    //    {
    //        CheckBox chkChecked = (CheckBox)dgi.FindControl("chkChecked");
    //        chkChecked.Visible = false;
    //    }
    //}
    //protected void CheckBoxInvisible()
    //{
    //    //lblMsg.Text = string.Empty;
    //    string strDeleteIDS = string.Empty;

    //    foreach (GridViewRow dgi in gridFiles.Rows)
    //    {
    //        CheckBox chkChecked = (CheckBox)dgi.FindControl("chkChecked");
    //        chkChecked.Visible = false;
    //    }
    //}
    //'<%#GetCheckInOutEnable(Eval("CheckOut").ToString())%>'
    protected bool CheckBoxInvisible()
    {
        bool blnEnable = true;
        if (Page.Request.Path.ToLower().Contains("customerprojectdocuments.aspx"))
        {      
            blnEnable = false;
        }


        if (Page.Request.Path.ToLower().Contains("portfoliodocscommon.aspx"))
        {
            if (sessionKeys.SID <= 2)
            {
                blnEnable = true;
            }
            else
            {

                if (HiddenCustomerdocs.Value == "False")
                {
                    blnEnable = false;
                }
            }
        }

        return blnEnable;
    }
    protected void linkFile_Click(object sender, EventArgs e)
    {


        if (Session["update"].ToString() == ViewState["update"].ToString())
        {
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            LinkButton linkFile = (LinkButton)sender;
            if (linkFile.CommandName == "RefreshFiles")
            {
                RefreshFiles();
            }
            else if (linkFile.CommandName == "CopyFile")
            {
                CopyFiles(false);
            }
            else if (linkFile.CommandName == "PasteFile")
            {
                PasteFiles();
                NewTreeBinding();
            }
            else if (linkFile.CommandName == "MoveFile")
            {
                CopyFiles(true);
                NewTreeBinding();
            }
            else if (linkFile.CommandName == "DeleteFile")
            {
                DeleteFiles();
                NewTreeBinding();
            }
            else if (linkFile.CommandName == "SearchFile")
            {
                //SearchDocuments();
            }
            else if (linkFile.CommandName == "ResetCheckOut")
            {
                ResetCheckOut(false);
                NewTreeBinding();
            }


        }

    }
    private void ResetCheckOut(bool check)
    {
        //lblMsg.Text = string.Empty;
        string strDeleteIDS = string.Empty;
        int intChecked = 0;
        foreach (GridViewRow dgi in gridFiles.Rows)
        {
            CheckBox chkChecked = (CheckBox)dgi.FindControl("chkChecked");
            if (chkChecked.Checked)
            {
                intChecked++;
                Label lblID = (Label)dgi.FindControl("lblID");
                if (strDeleteIDS == string.Empty)
                {
                    strDeleteIDS = lblID.Text.Trim() + ",";
                }
                else
                {
                    strDeleteIDS = strDeleteIDS + lblID.Text.Trim() + ",";
                }
            }
        }

        //strDeleteIDS = CommandArgumentID + ",";
        if (strDeleteIDS != string.Empty)
        {
            AC2P_DocumentsController ObjAC2P_DocumentsController = new AC2P_DocumentsController();

            int intRet = ObjAC2P_DocumentsController.AC2PDocumentsCheckOut(strDeleteIDS, check, sessionKeys.UID);

            if (check == true)
            {
                //lblMsg.Text = intChecked.ToString() + " file(s) are Checked Out.";
            }
            else
            {
                lblMsg.Text = intChecked.ToString() + Resources.DeffinityRes.CheckInFiles;//" file(s) are Checked In.";
            }
        }
        else
        {
            if (check == true)
            {
                //lblMsg.Text = "Plese select file(s) to Check Out.";
            }
            else
            {
                lblMsg.Text = Resources.DeffinityRes.CheckInSelPlsMsg; //"Plese select file(s) to Check In.";
            }
        }
    }
    protected void UltraWebTree1_NodeClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
    {

        int datakey = Convert.ToInt32(UltraWebTree1.SelectedNode.DataKey);
        Session["SelectedNode"] = e.Node.DataKey;
        TxtFolderID.Value = UltraWebTree1.SelectedNode.DataKey.ToString();
        folderID = Convert.ToInt32(TxtFolderID.Value);
        txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
        string projectID = "0";
        PnlFileUpload.Visible = true;
         if (Request.QueryString["project"] != null)
         {
             projectID = Request.QueryString["project"].ToString();
        }
        //if (Page.Request.Path.ToLower().Contains("portfoliodocscommon.aspx"))
        if ((Page.Request.Path.ToLower().Contains("portfoliodocscommon.aspx")) || (Page.Request.Path.ToLower().Contains("portfoliodocs.aspx")))
        {
            Response.Redirect(string.Format("{0}?folderID={1}", Request.Url.AbsolutePath, folderID));
        }
        else if (Page.Request.Path.ToLower().Contains("healthcheckformdocs.aspx"))
        {
            Response.Redirect(string.Format("{0}?HealthCheckId={1}&folderID={2}&PID={3}", Request.Url.AbsolutePath, QueryStringValues.HealthCheckId, folderID,Request.QueryString["PID"]));
        }
        else  if (Page.Request.Path.ToLower().Contains("sddocuments_frm.aspx"))
        {
            Response.Redirect(string.Format("{0}?SDID={1}&folderID={2}", Request.Url.AbsolutePath, QueryStringValues.SDID, folderID));
        }
        else if (Page.Request.Path.ToLower().Contains("mytasksdocuments.aspx"))
        {
            if (Request.QueryString["mode"] == null)
                Response.Redirect(string.Format("{0}?project={1}&folderID={2}", Request.Url.AbsolutePath, ddlProject.SelectedValue, folderID));
            else
                Response.Redirect(string.Format("{0}?mode=central&folderID={2}", Request.Url.AbsolutePath, ddlProject.SelectedValue, folderID));

        }
        else if (Page.Request.Path.ToLower().Contains("rfidocuments.aspx")) 
        {
           //RFIDocuments

            Response.Redirect(string.Format("{0}?project={1}&folderID={2}&VendorID={3}", Request.Url.AbsolutePath, projectID, folderID, QueryStringValues.Vendor));
        }
         //RFIDocuments
        else
        {
            if (Request.QueryString["mode"] == null)
                Response.Redirect(string.Format("{0}?project={1}&folderID={2}", Request.Url.AbsolutePath, ddlProject.SelectedValue, folderID));
            else
                Response.Redirect(string.Format("{0}?mode=central&folderID={2}", Request.Url.AbsolutePath, ddlProject.SelectedValue, folderID));

        }

    }
  
    protected void gridFiles_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        lblMsg.Text = string.Empty;

        if (e.CommandName.Equals("PortalView"))
        {

            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

            int intToggleProjectDoc=1;


            if (commandArgs[1]=="1")
            {
                intToggleProjectDoc = 0;
            }

            


            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("DEFFINITY_Document_PortalView", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", commandArgs[0]);
                    cmd.Parameters.AddWithValue("@projectDoc", intToggleProjectDoc);
                    conn.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {

                        //lblMsg.Text = Resources.DeffinityRes.DoucmentDeleted;  //"Document deleted successfully.";
                        gridFiles.DataBind();
                        NewTreeBinding();
                    }
                    else
                    {

                        lblMsg.Text = Resources.DeffinityRes.DocumentDeleteFailed;  //"Document deletion failed. Possible reasons are, you may not have delete permission for the document or the file is already deleted.";
                    }
                }
            }
        }

        if (e.CommandName.Equals("Download"))
        {
            AC2P_DocumentsController _AC2P_DocumentsController = new AC2P_DocumentsController();
            _AC2P_DocumentsController.DocumentJournalInsert(Convert.ToInt32(e.CommandArgument.ToString()), -99, sessionKeys.UID);


            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                string sqlCommand = string.Format("SELECT ContentType,Document,DocumentName FROM AC2P_Documents WHERE ID={0}", e.CommandArgument);
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            HttpContext.Current.Response.ContentType = reader.GetString(0);
                            byte[] getContent = (byte[])reader[1];
                            HttpContext.Current.Response.BinaryWrite(getContent);
                            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;FileName=" + reader.GetString(2).Replace(" ", string.Empty));
                            HttpContext.Current.Response.End();
                        }
                        else
                        {

                            lblMsg.Text = Resources.DeffinityRes.DocumentNotFound;//"The document is not found.  Possible reasons may be the other users have deleted the document you are looking for.";
                        }
                    }
                }
            }
        }
        else if (e.CommandName.Equals("DeleteFile"))
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("Deffinity_DeleteDocument", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@ContractorID", sessionKeys.UID);
                    conn.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {

                        lblMsg.Text = Resources.DeffinityRes.DoucmentDeleted;  //"Document deleted successfully.";
                        gridFiles.DataBind();
                        NewTreeBinding();
                    }
                    else
                    {

                        lblMsg.Text = Resources.DeffinityRes.DocumentDeleteFailed;  //"Document deletion failed. Possible reasons are, you may not have delete permission for the document or the file is already deleted.";
                    }
                }
            }
        }
        else if (e.CommandName.Equals("Select"))
        {

            DocumentPermissionsList(e.CommandArgument.ToString());
            ModalControlExtender1.Show();
        }
    }
   
    protected void gridResources_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("SetPermissions"))
        {
            string strValues = e.CommandArgument.ToString();
            string[] args = strValues.Split('|');
            if (args != null)
            {
                if (args.Length > 0)
                {
                    SetPermissions(args[0].ToString(), args[1].ToString());
                    DocumentPermissionsList(args[0].ToString());
                }
            }
        }
        this.updatePanel.Update();
        ModalControlExtender1.Show();
    }
    
    protected void gridFiles_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow gvrEditRow = gridFiles.Rows[e.RowIndex];
        TextBox txtEditname = (TextBox)gridFiles.Rows[e.RowIndex].FindControl("txtEditName");
        sqlFileList.UpdateParameters["DocumentName"].DefaultValue = txtEditname.Text.Trim();
        sqlFileList.Update();
    }

    #endregion Control Events
    
    #region Delete icon show or hide
    protected bool ShowHide()
    {
        bool del = true;
        //in customer portal we need to hide the icon 
        //page name = CustomerDocs.aspx
        if ((Request.Url.ToString().ToLower()).Contains("portfoliodocscommon.aspx") == true)
        {
            del = false;
        }
        return del;
    }

    #endregion Delete icon show or hide


    #region Control Methods



    protected void SDdocuments_Load()
    {
        if (sessionKeys.SID <= 2)
        {
            btnResetCheckout.Visible = true;
            //IMAGE1.Visible = true;
            //LABEL2.Visible = true;
            gridFiles.Columns[9].Visible = false;
        }
        else
        {
            //IMAGE1.Visible = false;
            //LABEL2.Visible = false;
            btnResetCheckout.Visible = false;
            gridFiles.Columns[9].Visible = true;
        }
        if (Request.QueryString["folderID"] == null)
            folderID = 0;
        else
            folderID = Convert.ToInt32(Request.QueryString["folderID"]);
        if (projectID > 0 || QueryStringValues.SDID > 0)
        {
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                NewTreeBinding();
                Session["Array"] = hif;

            }
            hif = (ArrayList)Session["Array"];
            if (Request.QueryString["FolderID"] != null)
                folderID = Convert.ToInt32(Request.QueryString["FolderID"]);
            //lblMsg.Text = Session["folderID"].ToString().Equals("-1") ? "Select a folder on the left tree to upload files." : string.Empty;
            //lblMsg.Text = folderID.ToString().Equals("-1") ? "Select a folder on the left tree to upload files." : string.Empty;
            lblMsg.Text = folderID.ToString().Equals("-1") ? Resources.DeffinityRes.SelectFolder : string.Empty;
        }
        else
        {
            if (!IsPostBack)
            {

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            NewTreeBinding();
        }
    }
    protected void Healthdocuments_Load()

    {
        if (sessionKeys.SID <= 2)
        {
            btnResetCheckout.Visible = true;
            //IMAGE1.Visible = true;
            //LABEL2.Visible = true;
            gridFiles.Columns[9].Visible = false;
        }
        else
        {
            //IMAGE1.Visible = false;
            //LABEL2.Visible = false;
            btnResetCheckout.Visible = false;
            gridFiles.Columns[9].Visible = true;
        }
        if (Page.Request.Path.ToLower().Contains("healthcheckformdocs.aspx"))
        {
            //lnkDeleteFolder.Visible = false;
            //btnRefresh.Visible = false;
            //btnMoveFile.Visible = false;
            //btnDeleteFile.Visible = false;
        }
        if (Request.QueryString["folderID"] == null)
            folderID = 0;
        else
            folderID = Convert.ToInt32(Request.QueryString["folderID"]);
        if (projectID > 0 || sessionKeys.IncidentID > 0)
        {
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                NewTreeBinding();
                Session["Array"] = hif;

            }
            hif = (ArrayList)Session["Array"];
            if (Request.QueryString["FolderID"] != null)
                folderID = Convert.ToInt32(Request.QueryString["FolderID"]);
            lblMsg.Text = Session["folderID"].ToString().Equals("-1") ? Resources.DeffinityRes.SelectFolder : string.Empty;
            
            
        }
        else
        {
            if (!IsPostBack)
            {

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            NewTreeBinding();
        }
    }

    protected void MyTaskdocuments_Load()
    {

        if (sessionKeys.SID <= 2)
        {
            btnResetCheckout.Visible = true;
            //IMAGE1.Visible = true;
            //LABEL2.Visible = true;
            gridFiles.Columns[9].Visible = false;
        }
        else
        {
            //IMAGE1.Visible = false;
            //LABEL2.Visible = false;
            btnResetCheckout.Visible = false;
            gridFiles.Columns[9].Visible = true;
        }
        if (Request.QueryString["folderID"] == null)
            folderID = 0;
        else
            folderID = Convert.ToInt32(Request.QueryString["folderID"]);
        if (!IsPostBack)
        {

            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

            DataTable table = new DataTable();
            ddlProject.Items.Clear();
            ListItem item = new ListItem("Select Project..", "0");
            ddlProject.Items.Add(item);

            //check the query sting is in mode=central
            if (Request.QueryString["mode"] != null)
            {
                if (Request.QueryString["mode"] == "central")
                    sessionKeys.PortfolioID = 0;
            }

            if (sessionKeys.PortfolioID <= 0)
                DataHelperClass.DDLHelper(table, "SELECT ProjectReference, projectTitle FROM Projects WHERE ProjectTitle IS NOT NULL");
            else
                DataHelperClass.DDLHelper(table, "SELECT ProjectReference, projectTitle FROM Projects WHERE ProjectTitle IS NOT NULL AND Portfolio=" + sessionKeys.PortfolioID);
            ddlProject.DataSource = table;
            ddlProject.DataBind();
        }
        if (Request.QueryString["project"] != null)
        {
            projectID = Convert.ToInt32(Request.QueryString["project"]);
        }
        if (projectID > 0 || sessionKeys.IncidentID > 0)
        {
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                NewTreeBinding();
                Session["Array"] = hif;

            }
            hif = (ArrayList)Session["Array"];
            if (Request.QueryString["FolderID"] != null)
                folderID = Convert.ToInt32(Request.QueryString["FolderID"]);
            lblMsg.Text = folderID == -1 ? Resources.DeffinityRes.SelectFolder : string.Empty;
            //lblMsg.Text = folderID == -1 ? "Select a folder on the left tree to upload files." : string.Empty;
        }
        else
        {
            NewTreeBinding();
        }
    }
    protected void CustomerProjectdocuments_Load()
    {


        if (sessionKeys.SID <= 2)
        {
            btnResetCheckout.Visible = true;
            //IMAGE1.Visible = true;
            //LABEL2.Visible = true;
            gridFiles.Columns[9].Visible = false;
        }
        else
        {
            //IMAGE1.Visible = false;
            //LABEL2.Visible = false;
            btnResetCheckout.Visible = false;
            gridFiles.Columns[9].Visible = true;
        }

        if (Request.QueryString["folderID"] == null)
            folderID = 0;
        else
            folderID = Convert.ToInt32(Request.QueryString["folderID"]);
        if (!IsPostBack)
        {

            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

            DataTable table = new DataTable();
            ddlProject.Items.Clear();
            ListItem item = new ListItem("Select Project..", "0");
            ddlProject.Items.Add(item);

            //check the query sting is in mode=central
            if (Request.QueryString["mode"] != null)
            {
                if (Request.QueryString["mode"] == "central")
                    sessionKeys.PortfolioID = 0;
            }

            if (sessionKeys.PortfolioID <= 0)
                DataHelperClass.DDLHelper(table, "SELECT ProjectReference, projectTitle FROM Projects WHERE ProjectTitle IS NOT NULL");
            else
                DataHelperClass.DDLHelper(table, "SELECT ProjectReference, projectTitle FROM Projects WHERE ProjectTitle IS NOT NULL AND Portfolio=" + sessionKeys.PortfolioID);
            ddlProject.DataSource = table;
            ddlProject.DataBind();
        }
        if (Request.QueryString["project"] != null)
        {
            projectID = Convert.ToInt32(Request.QueryString["project"]);
        }
        if (projectID > 0 || sessionKeys.IncidentID > 0)
        {
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                NewTreeBinding();
                Session["Array"] = hif;

            }
            hif = (ArrayList)Session["Array"];
            if (Request.QueryString["FolderID"] != null)
                folderID = Convert.ToInt32(Request.QueryString["FolderID"]);
            lblMsg.Text = folderID == -1 ? Resources.DeffinityRes.SelectFolder : string.Empty;
            //lblMsg.Text = folderID == -1 ? "Select a folder on the left tree to upload files." : string.Empty;
        }
        else
        {
            NewTreeBinding();
        }
    }
    protected void Projectdocuments_Load()
    {


        if (sessionKeys.SID <= 2)
        {
            btnResetCheckout.Visible = true;
            //IMAGE1.Visible = true;
            //LABEL2.Visible = true;
            gridFiles.Columns[9].Visible = false;
        }
        else
        {
            //IMAGE1.Visible = false;
            //LABEL2.Visible = false;
            btnResetCheckout.Visible = false;
            gridFiles.Columns[9].Visible = true;
        }

        if (Request.QueryString["folderID"] == null)
            folderID = 0;
        else
            folderID = Convert.ToInt32(Request.QueryString["folderID"]);
        if (!IsPostBack)
        {

            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

            DataTable table = new DataTable();
            ddlProject.Items.Clear();
            ListItem item = new ListItem("Select Project..", "0");
            ddlProject.Items.Add(item);

            //check the query sting is in mode=central
            if (Request.QueryString["mode"] != null)
            {
                if (Request.QueryString["mode"] == "central")
                    sessionKeys.PortfolioID = 0;
            }

            if (sessionKeys.PortfolioID <= 0)
                DataHelperClass.DDLHelper(table, "SELECT ProjectReference, projectTitle FROM Projects WHERE ProjectTitle IS NOT NULL");
            else
                DataHelperClass.DDLHelper(table, "SELECT ProjectReference, projectTitle FROM Projects WHERE ProjectTitle IS NOT NULL AND Portfolio=" + sessionKeys.PortfolioID);
            ddlProject.DataSource = table;
            ddlProject.DataBind();
        }
        if (Request.QueryString["project"] != null)
        {
            projectID = Convert.ToInt32(Request.QueryString["project"]);
        }
        if (projectID > 0 || sessionKeys.IncidentID > 0)
        {
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                NewTreeBinding();
                Session["Array"] = hif;

            }
            hif = (ArrayList)Session["Array"];
            if (Request.QueryString["FolderID"] != null)
                folderID = Convert.ToInt32(Request.QueryString["FolderID"]);
            lblMsg.Text = folderID == -1 ? Resources.DeffinityRes.SelectFolder : string.Empty;
            //lblMsg.Text = folderID == -1 ? "Select a folder on the left tree to upload files." : string.Empty;
        }
        else
        {
            NewTreeBinding();
        }
    }


    protected void RFIdocuments_Load()
    {


        if (sessionKeys.SID <= 2)
        {
            btnResetCheckout.Visible = true;
            //IMAGE1.Visible = true;
            //LABEL2.Visible = true;
            gridFiles.Columns[9].Visible = false;
        }
        else
        {
            //IMAGE1.Visible = false;
            //LABEL2.Visible = false;
            btnResetCheckout.Visible = false;
            gridFiles.Columns[9].Visible = true;
        }

        if (Request.QueryString["folderID"] == null)
            folderID = 0;
        else
            folderID = Convert.ToInt32(Request.QueryString["folderID"]);
        if (!IsPostBack)
        {

            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

            DataTable table = new DataTable();
            ddlProject.Items.Clear();
            ListItem item = new ListItem("Select Project..", "0");
            ddlProject.Items.Add(item);

            //check the query sting is in mode=central
            if (Request.QueryString["mode"] != null)
            {
                if (Request.QueryString["mode"] == "central")
                    sessionKeys.PortfolioID = 0;
            }

            if (sessionKeys.PortfolioID <= 0)
                DataHelperClass.DDLHelper(table, "SELECT ProjectReference, projectTitle FROM Projects WHERE ProjectTitle IS NOT NULL");
            else
                DataHelperClass.DDLHelper(table, "SELECT ProjectReference, projectTitle FROM Projects WHERE ProjectTitle IS NOT NULL AND Portfolio=" + sessionKeys.PortfolioID);
            ddlProject.DataSource = table;
            ddlProject.DataBind();
        }
        if (Request.QueryString["project"] != null)
        {
            projectID = Convert.ToInt32(Request.QueryString["project"]);
        }
        if (projectID > 0 || sessionKeys.IncidentID > 0)
        {
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                NewTreeBinding();
                Session["Array"] = hif;

            }
            hif = (ArrayList)Session["Array"];
            if (Request.QueryString["FolderID"] != null)
                folderID = Convert.ToInt32(Request.QueryString["FolderID"]);
            lblMsg.Text = folderID == -1 ? Resources.DeffinityRes.SelectFolder : string.Empty;

            //lblMsg.Text = folderID == -1 ? "Select a folder on the left tree to upload files." : string.Empty;
        }
        else
        {
            NewTreeBinding();
        }
    }

    //
    private string selectPortFolio()
    {
        string strEnable = string.Empty; ;
        try
        {
            SqlDataReader dr = Portfilio.SelectPortfolio(sessionKeys.PortfolioID);

            while (dr.Read())
            {
                strEnable = dr["docenable"].ToString();
            }
            dr.Close();
            dr.Dispose();

        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        return strEnable;
    }

    private void InvisibleCRUDOperstions_Customer()
    {
        gridFiles.Columns[0].Visible = false;        
        gridFiles.Columns[6].Visible = false;
        gridFiles.Columns[8].Visible = false;
        gridFiles.Columns[9].Visible = false;
        gridFiles.Columns[10].Visible = false;
        gridFiles.Columns[11].Visible = false;

        PnlFileUpload.Visible = false;
        Literal1.Visible = false;
        txtCreateFolder.Visible = false;
        lnkFolder.Visible = false;
        lnkRenameFolder.Visible = false;
        lnkDeleteFolder.Visible = false;
        btnResetCheckout.Visible = false;
        //IMAGE1.Visible = false;
        //LABEL2.Visible = false;
        btnCopyFile.Visible = false;
        //IMAGE4.Visible = false;
        //lblCopyFile.Visible = false;
        btnPasteFile.Visible = false;
        //IMAGE5.Visible = false;
        //lblPasteFile.Visible = false;
        btnMoveFile.Visible = false;
        //IMAGE6.Visible = false;
        //Label1.Visible = false;
        btnDeleteFile.Visible = false;
        //IMAGE7.Visible = false;
        //Label3.Visible = false;
        //hide delete column
        gridFiles.Columns[12].Visible = false;
    }
    protected void CustomerAndPortalAdmin_Load()
    {
        if (sessionKeys.SID <= 2)
        {
            btnResetCheckout.Visible = true;
            //IMAGE1.Visible = true;
            //LABEL2.Visible = true;
            gridFiles.Columns[9].Visible = false;
            gridFiles.Columns[12].Visible = true;
            PnlAllowCustomers.Visible = false;
        }
        else
        {
            //IMAGE1.Visible = false;
            //LABEL2.Visible = false;
            btnResetCheckout.Visible = false;            
            btnMoveFile.Visible = false;
            btnDeleteFile.Visible = false;
            gridFiles.Columns[10].Visible = false;
            gridFiles.Columns[9].Visible = false;
            gridFiles.Columns[8].Visible = false;
            gridFiles.Columns[6].Visible = false;
            gridFiles.Columns[11].Visible = false;
            btnResetCheckout.Visible = false;
            lnkDeleteFolder.Visible = false;
            btnResetCheckout.Visible = false;


            if (HiddenCustomerdocs.Value == "False")
            {
                InvisibleCRUDOperstions_Customer();
            }

        }

        //if (Page.Request.Path.ToLower().Contains("documentsadmincustomerforall.aspx"))
        //{
        //    lnkDeleteFolder.Visible = false;
        //    btnResetCheckout.Visible = false;
        //    //btnRefresh.Visible = false;
        //    btnMoveFile.Visible = false;
        //    btnDeleteFile.Visible = false;
        //    gridFiles.Columns[10].Visible = false;
        //    gridFiles.Columns[9].Visible = false;
        //    gridFiles.Columns[8].Visible = false;
        //    btnResetCheckout.Visible = false;
        //    //HeaderStyle-CssClass="header_bg_r"
        //    //gridFiles.Columns[11].HeaderStyle.RegisteredCssClass
        //    //btnResetCheckout.Visible = false;
        //    IMAGE1.Visible=false;
        //    LABEL2.Visible = false;
            
        //}
        if (Request.QueryString["folderID"] == null)
            folderID = 0;
        else
            folderID = Convert.ToInt32(Request.QueryString["folderID"]);
        if (projectID > 0 || sessionKeys.IncidentID > 0)
        {
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                NewTreeBinding();
                Session["Array"] = hif;

            }
            hif = (ArrayList)Session["Array"];
            if (Request.QueryString["FolderID"] != null)
                folderID = Convert.ToInt32(Request.QueryString["FolderID"]);
            lblMsg.Text = folderID.ToString().Equals("-1") ? Resources.DeffinityRes.SelectFolder : string.Empty;

             //   lblMsg.Text = folderID.ToString().Equals("-1") ? "Select a folder on the left tree to upload files." : string.Empty;
             
        }
        else
        {
            if (!IsPostBack)
            {

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            NewTreeBinding();
        }
    }
    
    protected void CustomerAndPortalAdmin_PreRender()
    {
        string strEnable=string.Empty ;
        if (sessionKeys.SID <= 2)
        {
            btnResetCheckout.Visible = true;
            //IMAGE1.Visible = true;
            //LABEL2.Visible = true;
            gridFiles.Columns[9].Visible = false;
            PnlAllowCustomers.Visible = false;
            lnkDeleteFolder.Visible = true;
        }
        else
        {
            //IMAGE1.Visible = false;
            //LABEL2.Visible = false;
            btnResetCheckout.Visible = false;
            btnMoveFile.Visible = false;
            btnDeleteFile.Visible = false;
            gridFiles.Columns[10].Visible = false;
            gridFiles.Columns[9].Visible = false;
            gridFiles.Columns[8].Visible = false;
            btnResetCheckout.Visible = false;
            lnkDeleteFolder.Visible = false;
            btnResetCheckout.Visible = false;
            gridFiles.Columns[6].Visible = false;
            gridFiles.Columns[11].Visible = false;

            if (HiddenCustomerdocs.Value == "False")
            {
                InvisibleCRUDOperstions_Customer();
            }

        }
        if (projectID == 0)

            if (Page.Request.Path.ToLower().Contains("portfoliodocscommon.aspx"))
            {
               // lnkDeleteFolder.Visible = false;
                gridFiles.Columns[gridFiles.Columns.Count - 2].Visible = false;
                //delete button
                if (sessionKeys.SID != 7)
                    gridFiles.Columns[12].Visible = true;
                else
                    gridFiles.Columns[12].Visible = false;

            }
        //Pass the query string parameters to the flash file upload control.
        //rqdFolder.Visible = true;
        //flashUpload.QueryParameters = string.Format("project={0}&folderID={1}&contractorID={2}&IncidentID={3}", sessionKeys.Project, folderID, sessionKeys.UID, sessionKeys.IncidentID);
        if (folderID <= 0)
        {
            //flashUpload.Visible = false;
            //gridFiles.Visible = false;
            //lnkRenameFolder.Visible = false;
            //lnkDeleteFolder.Visible = false;

            lblCounter.Visible = false;
            lblSpaceUsed.Visible = false;
            PnlFileUpload.Visible = false;
            gridFiles.Visible = false;
            lnkRenameFolder.Visible = false;
            lnkDeleteFolder.Visible = false;
            btnResetCheckout.Visible = false;
            lblCounter.Visible = false;
            lblSpaceUsed.Visible = false;
        }
        else
        {
            //flashUpload.Visible = true;
            //gridFiles.Visible = true;
            //lblMsg.Text = string.Empty;
            //lnkRenameFolder.Visible = true;
            //lnkDeleteFolder.Visible = true;
            lblCounter.Visible = true;
            lblSpaceUsed.Visible = true;
            //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
            if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)

            {
                if (sessionKeys.SID <=2)
                {
                    PnlFileUpload.Visible = true;
                }
                else
                {
                    if (HiddenCustomerdocs.Value == "False")
                    {
                        PnlFileUpload.Visible = false;
                    }
                    else
                    {
                        PnlFileUpload.Visible = true;
                    }
                }
            }
            else
            {
                
                PnlFileUpload.Visible = false;
                //Response.Redirect("/Deffinity_team/PortfolioDocs.aspx", false);
                //return;                

            }

            gridFiles.Visible = true;
            lblMsg.Text = string.Empty;


            //lnkRenameFolder.Visible = true;
            
            btnResetCheckout.Visible = true;
            if (!Page.Request.Path.ToLower().Contains("portfoliodocscommon.aspx"))
            {
                //lnkDeleteFolder.Visible = true;
                btnResetCheckout.Visible=false;
            }

        }
        if (Session["SelectedNode"] != null)
        {
            gridFiles.Visible = true;
            LoopThroughNodes(UltraWebTree1.Nodes);
            TxtFolderID.Value = Session["SelectedNode"].ToString();
            txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();

        }
        gridFiles.Visible = true;
        gridFiles.DataBind();
    }

    protected void SDdocuments_PreRender()
    {
        if (sessionKeys.SID <= 2)
        {
            btnResetCheckout.Visible = true;
            //IMAGE1.Visible = true;
            //LABEL2.Visible = true;
            gridFiles.Columns[9].Visible = false;
        }
        else
        {
            //IMAGE1.Visible = false;
            //LABEL2.Visible = false;
            btnResetCheckout.Visible = false;
            gridFiles.Columns[9].Visible = true;
        }

        string strUrl = "";
        string strSDID = "0";

        if (Request.QueryString["SDID"] != null)
        {
            strSDID = Request.QueryString["SDID"].ToString();
        }
        strUrl = "/Servicedesk/SDdocuments_frm.aspx?sdid=" + strSDID;
        //strUrl = "/Deffinity_team/Servicedesk/SDdocuments_frm.aspx?sdid=" + strSDID;

        if (folderID == -1)
        {
            //flashUpload.Visible = false;
            lblCounter.Visible = false;
            lblSpaceUsed.Visible = false;

            PnlFileUpload.Visible = false;
            gridFiles.Visible = false;
            lnkRenameFolder.Visible = false;
            lnkDeleteFolder.Visible = false;
            btnResetCheckout.Visible = false;
            lblCounter.Visible = false;
            lblSpaceUsed.Visible = false;
        }
        else
        {
            //flashUpload.Visible = true;
            lblCounter.Visible = true;
            lblSpaceUsed.Visible = true;
            //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
            if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)
            
            {
                PnlFileUpload.Visible = true;

            }
            else
            {

                PnlFileUpload.Visible = false;
                 Response.Redirect(strUrl, false);
                 return;
                
            }
            gridFiles.Visible = true;
            lblMsg.Text = string.Empty;
            lnkRenameFolder.Visible = true;
            lnkDeleteFolder.Visible = true;
            btnResetCheckout.Visible = true;
        }
        gridFiles.Visible = true;
        //}
        //else
        if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString().ToLower() == "central")
        {

            // flashUpload.QueryParameters = string.Format("project=0&folderID={0}&contractorID={1}&IncidentID=0", folderID, sessionKeys.UID);

        }
        else
        {
            //   flashUpload.Visible = false;
            PnlFileUpload.Visible = false;
            lblCounter.Visible = false;
            lblSpaceUsed.Visible = false;
        }

        //made project drop down not to visible as per the client requirements.
        ddlProject.Visible = false;
        if (Session["SelectedNode"] != null)
        {
            gridFiles.Visible = true;
            LoopThroughNodes(UltraWebTree1.Nodes);
            TxtFolderID.Value = Session["SelectedNode"].ToString();
            txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
        }
        sqlFileList.DataBind();
        gridFiles.DataBind();

        
        if (folderID > 0)
        {
            gridFiles.Visible = true;
            //flashUpload.Visible = true;
            //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
            if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)
            {
                PnlFileUpload.Visible = true;
            }
            else
            {
                PnlFileUpload.Visible = false;
                Response.Redirect(strUrl, false);                
                return;
            }
            lblCounter.Visible = true;
            lblSpaceUsed.Visible = true;
        }
        else
        {
            gridFiles.Visible = false;
            //flashUpload.Visible = false;
            PnlFileUpload.Visible = false;
            lblCounter.Visible = false;
            lblSpaceUsed.Visible = false;
        }
        try
        {

            //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
            if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)
            {
                txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
            }
            else
            {
                PnlFileUpload.Visible = false;
                Response.Redirect(strUrl, false);
                return;
               // Response.Redirect("/ProjectDocuments.aspx?mode=central", false);
            }

        }
        catch (Exception ex)
        {
            string strEx = ex.ToString();
            //Ignore
        }
    }

    protected void healthdocuments_PreRender()
    {
        //if (projectID > 0 || sessionKeys.IncidentID > 0)
        //{
        //    try
        //    {
        //        ddlProject.SelectedValue = projectID.ToString();
        //    }
        //    catch { ddlProject.SelectedValue = "0"; }
        //    //Pass the query string parameters to the flash file upload control.
        //    if (sessionKeys.IncidentID > 0)
        //    {
        //        projectID = 0;
        //        ddlProject.Visible = false;
        //        //rqdFolder.Visible = false;
        //    }
        //    else
        //    {
        //        ddlProject.Visible = true;
        //        //rqdFolder.Visible =true;

        //    }
        //    if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString().ToLower() == "central")
        //    {
        //        // flashUpload.QueryParameters = string.Format("project=0&folderID={0}&contractorID={1}&IncidentID=0", folderID, sessionKeys.UID);

        //    }
        //    else
        //    {

        //        // flashUpload.QueryParameters = string.Format("project={0}&folderID={1}&contractorID={2}&IncidentID={3}", sessionKeys.Project, folderID, sessionKeys.UID, sessionKeys.IncidentID);
        //    }
        gridFiles.Columns[9].Visible = false;
        if (sessionKeys.SID <=2)
        {
            btnResetCheckout.Visible = true;
            //IMAGE1.Visible = true;
            //LABEL2.Visible = true;
            gridFiles.Columns[9].Visible = false;
        }
        else
        {
            //IMAGE1.Visible = false;
            //LABEL2.Visible = false;
            btnResetCheckout.Visible = false;
            gridFiles.Columns[9].Visible = true;
        }
        string strUrl = "";
        string strHealthCheckId = "0";
        string strPID = "";

        if (Request.QueryString["HealthCheckId"] != null)
        {
            strHealthCheckId = Request.QueryString["HealthCheckId"].ToString();

            if (Request.QueryString["PID"] != null)
            {
                strPID = Request.QueryString["PID"].ToString();
            }
            //strUrl = "/Deffinity_team/healthcheckformdocs.aspx?HealthCheckId=" + strHealthCheckId + "&PID=" + strPID;
            strUrl = "/healthcheckformdocs.aspx?HealthCheckId=" + strHealthCheckId + "&PID=" + strPID;
        }
        
        


            if (folderID == -1)
            {
                //flashUpload.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;

                PnlFileUpload.Visible = false;
                gridFiles.Visible = false;
                lnkRenameFolder.Visible = false;
                lnkDeleteFolder.Visible = false;
                btnResetCheckout.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;
            }
            else
            {
                //flashUpload.Visible = true;
                lblCounter.Visible = true;
                lblSpaceUsed.Visible = true;
                //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
                if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)
                    
                {
                    PnlFileUpload.Visible = true;
                }
                else
                {
                    //Response.Redirect("/ProjectDocuments.aspx?mode=central", false);
                    Response.Redirect(strUrl, false);
                    PnlFileUpload.Visible = false;
                    return;
                }
                gridFiles.Visible = true;
                lblMsg.Text = string.Empty;
                lnkRenameFolder.Visible = true;
                lnkDeleteFolder.Visible = true;
                btnResetCheckout.Visible = true;
            }
            gridFiles.Visible = true;
        //}
        //else
            if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString().ToLower() == "central")
            {

                // flashUpload.QueryParameters = string.Format("project=0&folderID={0}&contractorID={1}&IncidentID=0", folderID, sessionKeys.UID);

            }
            else
            {
                //   flashUpload.Visible = false;
                PnlFileUpload.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;
            }

        //made project drop down not to visible as per the client requirements.
        ddlProject.Visible = false;
        if (Session["SelectedNode"] != null)
        {
            gridFiles.Visible = true;
            LoopThroughNodes(UltraWebTree1.Nodes);
            TxtFolderID.Value = Session["SelectedNode"].ToString();
            txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
        }
        sqlFileList.DataBind();
        gridFiles.DataBind();

        //Hide the admin links based on the users access permission rights on the document.
        ////if (sessionKeys.SID > 3 && Request.QueryString["project"] != null)
        ////{
        ////    foreach (GridViewRow row in gridFiles.Rows)
        ////    {
        ////        int documentID = Convert.ToInt32(((Label)row.FindControl("lblID")).Text);
        ////        bool isRestricted = false;
        ////        using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        ////        {
        ////            using (SqlCommand cmd = new SqlCommand("IsRestrictedDocument", conn))
        ////            {
        ////                cmd.CommandType = CommandType.StoredProcedure;
        ////                cmd.Parameters.AddWithValue("DocumentID", documentID);
        ////                cmd.Parameters.AddWithValue("UserID", sessionKeys.UID);
        ////                conn.Open();
        ////                isRestricted = Convert.ToBoolean(cmd.ExecuteScalar());
        ////            }
        ////        }
        ////        if (isRestricted)
        ////        {
        ////            row.Visible = false;
        ////            ((LinkButton)row.Cells[0].FindControl("lnkDownLoad")).Enabled = false;
        ////            ((LinkButton)row.Cells[0].FindControl("lnkDownLoad")).ForeColor = System.Drawing.Color.Gray;
        ////            ((ImageButton)row.Cells[row.Cells.Count - 1].FindControl("btnDelete")).Enabled = false;
        ////        }
        ////    }
        ////}
        //if (sessionKeys.SID <= 3)
        ////if (Request.QueryString["project"] != null)
        ////{
        ////    int ProjID;
        ////    ProjID = Convert.ToInt32(Request.QueryString["project"]);
        ////    if (ProjID > 0)
        ////    {
        ////        gridFiles.Columns[6].Visible = true;
        ////    }
        ////    else
        ////    {
        ////        gridFiles.Columns[6].Visible = false;
        ////    }
        ////}
        ////else
        ////{
        ////    gridFiles.Columns[6].Visible = false;
        ////}
        if (folderID > 0)
        {
            gridFiles.Visible = true;
            //flashUpload.Visible = true;
            //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
                if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)
                
            {
                PnlFileUpload.Visible = true;
            }
            else
            {
                PnlFileUpload.Visible = false;
                Response.Redirect(strUrl , false);                
                return;
            }
            lblCounter.Visible = true;
            lblSpaceUsed.Visible = true;
        }
        else
        {
            gridFiles.Visible = false;
            //flashUpload.Visible = false;
            PnlFileUpload.Visible = false;
            lblCounter.Visible = false;
            lblSpaceUsed.Visible = false;
        }
        try
        {
            //if (lblMsg.Text.Trim() != "Folder deleted successfully.")

            if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess) 
            {
                txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
            }
            else
            {
                PnlFileUpload.Visible = false;
                Response.Redirect(strUrl, false);
                return;
                //Response.Redirect("/ProjectDocuments.aspx?mode=central", false);
            }

        }
        catch (Exception ex)
        {
            string strEx = ex.ToString();
            //Ignore
        }
    }


    protected void RFIdocuments_PreRender()
    {
        string strUrl = "";
        string strPojectID = "0";

        if (Request.QueryString["project"] != null)
        {
            strPojectID = Request.QueryString["project"].ToString();
            //strUrl = "/Deffinity_team/ProjectDocuments.aspx?project=" + strPojectID;
            strUrl = "/RFIDocuments.aspx.aspx?project=" + strPojectID;
        }
        else
        {
            //strUrl = "/Deffinity_team/ProjectDocuments.aspx?mode=central";
            strUrl = "/RFIDocuments.aspx?mode=central";
        }
        

        if (projectID > 0 || sessionKeys.IncidentID > 0)
        {
            try
            {
                ddlProject.SelectedValue = projectID.ToString();
            }
            catch { ddlProject.SelectedValue = "0"; }
            //Pass the query string parameters to the flash file upload control.
            if (sessionKeys.IncidentID > 0)
            {
                projectID = 0;
                ddlProject.Visible = false;
                //rqdFolder.Visible = false;
            }
            else
            {
                ddlProject.Visible = true;
                //rqdFolder.Visible =true;

            }
            if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString().ToLower() == "central")
            {
                // flashUpload.QueryParameters = string.Format("project=0&folderID={0}&contractorID={1}&IncidentID=0", folderID, sessionKeys.UID);

            }
            else
            {

                // flashUpload.QueryParameters = string.Format("project={0}&folderID={1}&contractorID={2}&IncidentID={3}", sessionKeys.Project, folderID, sessionKeys.UID, sessionKeys.IncidentID);
            }


            if (folderID == -1)
            {
                //flashUpload.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;

                PnlFileUpload.Visible = false;
                gridFiles.Visible = false;
                lnkRenameFolder.Visible = false;
                lnkDeleteFolder.Visible = false;
                btnResetCheckout.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;
            }
            else
            {
                //flashUpload.Visible = true;
                lblCounter.Visible = true;
                lblSpaceUsed.Visible = true;
                //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
                if (lblMsg.Text.Trim() !=Resources.DeffinityRes.FolderDeletedSuccess)
                {
                    PnlFileUpload.Visible = true;
                }
                else
                {


                    PnlFileUpload.Visible = false;
                    Response.Redirect(strUrl, false);
                    return;
                    
                }
                gridFiles.Visible = true;
                lblMsg.Text = string.Empty;
                lnkRenameFolder.Visible = true;
                lnkDeleteFolder.Visible = true;
                btnResetCheckout.Visible = true;
            }
            gridFiles.Visible = true;
        }
        else
            if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString().ToLower() == "central")
            {

                // flashUpload.QueryParameters = string.Format("project=0&folderID={0}&contractorID={1}&IncidentID=0", folderID, sessionKeys.UID);

            }
            else
            {
                //   flashUpload.Visible = false;
                PnlFileUpload.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;
            }

        //made project drop down not to visible as per the client requirements.
        ddlProject.Visible = false;
        if (Session["SelectedNode"] != null)
        {
            gridFiles.Visible = true;
            LoopThroughNodes(UltraWebTree1.Nodes);
            TxtFolderID.Value = Session["SelectedNode"].ToString();
            txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
        }
        sqlFileList.DataBind();
        gridFiles.DataBind();

        //Hide the admin links based on the users access permission rights on the document.
        if (sessionKeys.SID > 3 && Request.QueryString["project"] != null)
        {
            foreach (GridViewRow row in gridFiles.Rows)
            {
                int documentID = Convert.ToInt32(((Label)row.FindControl("lblID")).Text);
                bool isRestricted = false;
                using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
                {
                    using (SqlCommand cmd = new SqlCommand("IsRestrictedDocument", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("DocumentID", documentID);
                        cmd.Parameters.AddWithValue("UserID", sessionKeys.UID);
                        conn.Open();
                        isRestricted = Convert.ToBoolean(cmd.ExecuteScalar());
                    }
                }
                if (isRestricted)
                {
                    row.Visible = false;
                    ((LinkButton)row.Cells[0].FindControl("lnkDownLoad")).Enabled = false;
                    ((LinkButton)row.Cells[0].FindControl("lnkDownLoad")).ForeColor = System.Drawing.Color.Gray;
                    ((ImageButton)row.Cells[row.Cells.Count - 1].FindControl("btnDelete")).Enabled = false;
                }
            }
        }
        //if (sessionKeys.SID <= 3)
        if (Request.QueryString["project"] != null)
        {
            int ProjID;
            ProjID = Convert.ToInt32(Request.QueryString["project"]);
            if (ProjID > 0)
            {
                gridFiles.Columns[7].Visible = true;
            }
            else
            {
                gridFiles.Columns[7].Visible = false;
            }
        }
        else
        {
            gridFiles.Columns[7].Visible = false;
        }
        if (folderID > 0)
        {
            gridFiles.Visible = true;
            //flashUpload.Visible = true;
//            if (lblMsg.Text.Trim() != "Folder deleted successfully.")
                if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)

            {
                PnlFileUpload.Visible = true;
            }
            else
            {

                PnlFileUpload.Visible = false;
                Response.Redirect(strUrl, false);
                return;
            }
            lblCounter.Visible = true;
            lblSpaceUsed.Visible = true;
        }
        else
        {
            gridFiles.Visible = false;
            //flashUpload.Visible = false;
            PnlFileUpload.Visible = false;
            lblCounter.Visible = false;
            lblSpaceUsed.Visible = false;
        }
        try
        {

  //          if (lblMsg.Text.Trim() != "Folder deleted successfully.")
            if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)

            {
                txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
            }
            else
            {

                PnlFileUpload.Visible = false;
                Response.Redirect(strUrl, false);
                return;
            }

        }
        catch (Exception ex)
        {
            string strEx = ex.ToString();
            //Ignore
        }
    }

    protected void CustomerProjectdocuments_PreRender()
    {
        string strUrl = "";
        string strPojectID = "0";        

        if (Request.QueryString["project"] != null)
        {
            strPojectID = Request.QueryString["project"].ToString();
            //strUrl = "/Deffinity_team/ProjectDocuments.aspx?project=" + strPojectID;
            strUrl = "/CustomerProjectDocuments.aspx?project=" + strPojectID;
        }
        else
        {
            //strUrl = "/Deffinity_team/ProjectDocuments.aspx?mode=central";
            strUrl = "/CustomerProjectDocuments.aspx?mode=central";
        }


        if (projectID > 0 || sessionKeys.IncidentID > 0)
        {
            try
            {
                ddlProject.SelectedValue = projectID.ToString();
            }
            catch { ddlProject.SelectedValue = "0"; }
            //Pass the query string parameters to the flash file upload control.
            if (sessionKeys.IncidentID > 0)
            {
                projectID = 0;
                ddlProject.Visible = false;
                //rqdFolder.Visible = false;
            }
            else
            {
                ddlProject.Visible = true;
                //rqdFolder.Visible =true;

            }
            if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString().ToLower() == "central")
            {
                // flashUpload.QueryParameters = string.Format("project=0&folderID={0}&contractorID={1}&IncidentID=0", folderID, sessionKeys.UID);

            }
            else
            {

                // flashUpload.QueryParameters = string.Format("project={0}&folderID={1}&contractorID={2}&IncidentID={3}", sessionKeys.Project, folderID, sessionKeys.UID, sessionKeys.IncidentID);
            }


            if (folderID == -1)
            {
                //flashUpload.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;

                PnlFileUpload.Visible = false;
                gridFiles.Visible = false;
                lnkRenameFolder.Visible = false;
                lnkDeleteFolder.Visible = false;
                btnResetCheckout.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;
            }
            else
            {
                //flashUpload.Visible = true;
                lblCounter.Visible = true;
                lblSpaceUsed.Visible = true;
                //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
                if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)
                {
                    PnlFileUpload.Visible = true;
                }
                else
                {


                    PnlFileUpload.Visible = false;
                    Response.Redirect(strUrl, false);
                    return;

                }
                gridFiles.Visible = true;
                lblMsg.Text = string.Empty;
                lnkRenameFolder.Visible = true;
                lnkDeleteFolder.Visible = true;
                btnResetCheckout.Visible = true;
            }
            gridFiles.Visible = true;
        }
        else
            if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString().ToLower() == "central")
            {

                // flashUpload.QueryParameters = string.Format("project=0&folderID={0}&contractorID={1}&IncidentID=0", folderID, sessionKeys.UID);

            }
            else
            {
                //   flashUpload.Visible = false;
                PnlFileUpload.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;
            }

        //made project drop down not to visible as per the client requirements.
        ddlProject.Visible = false;
        if (Session["SelectedNode"] != null)
        {
            gridFiles.Visible = true;
            LoopThroughNodes(UltraWebTree1.Nodes);
            TxtFolderID.Value = Session["SelectedNode"].ToString();
            txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
        }
        sqlFileList.DataBind();
        gridFiles.DataBind();

        //Hide the admin links based on the users access permission rights on the document.
        if (sessionKeys.SID > 3 && Request.QueryString["project"] != null)
        {
            foreach (GridViewRow row in gridFiles.Rows)
            {
                int documentID = Convert.ToInt32(((Label)row.FindControl("lblID")).Text);
                bool isRestricted = false;
                using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
                {
                    using (SqlCommand cmd = new SqlCommand("IsRestrictedDocument", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("DocumentID", documentID);
                        cmd.Parameters.AddWithValue("UserID", sessionKeys.UID);
                        conn.Open();
                        isRestricted = Convert.ToBoolean(cmd.ExecuteScalar());
                    }
                }
                if (isRestricted)
                {
                    row.Visible = false;
                    ((LinkButton)row.Cells[0].FindControl("lnkDownLoad")).Enabled = false;
                    ((LinkButton)row.Cells[0].FindControl("lnkDownLoad")).ForeColor = System.Drawing.Color.Gray;
                    ((ImageButton)row.Cells[row.Cells.Count - 1].FindControl("btnDelete")).Enabled = false;
                }
            }
        }
        //if (sessionKeys.SID <= 3)
        if (Request.QueryString["project"] != null)
        {
            int ProjID;
            ProjID = Convert.ToInt32(Request.QueryString["project"]);
            if (ProjID > 0)
            {
                gridFiles.Columns[7].Visible = true;
            }
            else
            {
                gridFiles.Columns[7].Visible = false;
            }
        }
        else
        {
            gridFiles.Columns[7].Visible = false;
        }
        if (folderID > 0)
        {
            gridFiles.Visible = true;
            //flashUpload.Visible = true;
            //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
            if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)
            {
                PnlFileUpload.Visible = true;
            }
            else
            {

                PnlFileUpload.Visible = false;
                Response.Redirect(strUrl, false);
                return;
            }
            lblCounter.Visible = true;
            lblSpaceUsed.Visible = true;
        }
        else
        {
            gridFiles.Visible = false;
            //flashUpload.Visible = false;
            PnlFileUpload.Visible = false;
            lblCounter.Visible = false;
            lblSpaceUsed.Visible = false;
        }
        try
        {

            //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
            if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)
            {
                txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
            }
            else
            {

                PnlFileUpload.Visible = false;
                Response.Redirect(strUrl, false);
                return;
            }

        }
        catch (Exception ex)
        {
            string strEx = ex.ToString();
            //Ignore
        }
        gridFiles.Columns[10].Visible = false;
    }
    protected void Projectdocuments_PreRender()
    {
        string strUrl = "";
        string strPojectID = "0";

        if (Request.QueryString["project"] != null)
        {
            strPojectID = Request.QueryString["project"].ToString();
            //strUrl = "/Deffinity_team/ProjectDocuments.aspx?project=" + strPojectID;
            strUrl = "/ProjectDocuments.aspx?project=" + strPojectID;
        }
        else
        {
            //strUrl = "/Deffinity_team/ProjectDocuments.aspx?mode=central";
            strUrl = "/ProjectDocuments.aspx?mode=central";
        }
        

        if (projectID > 0 || sessionKeys.IncidentID > 0)
        {
            try
            {
                ddlProject.SelectedValue = projectID.ToString();
            }
            catch { ddlProject.SelectedValue = "0"; }
            //Pass the query string parameters to the flash file upload control.
            if (sessionKeys.IncidentID > 0)
            {
                projectID = 0;
                ddlProject.Visible = false;
                //rqdFolder.Visible = false;
            }
            else
            {
                ddlProject.Visible = true;
                //rqdFolder.Visible =true;

            }
            if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString().ToLower() == "central")
            {
                // flashUpload.QueryParameters = string.Format("project=0&folderID={0}&contractorID={1}&IncidentID=0", folderID, sessionKeys.UID);

            }
            else
            {

                // flashUpload.QueryParameters = string.Format("project={0}&folderID={1}&contractorID={2}&IncidentID={3}", sessionKeys.Project, folderID, sessionKeys.UID, sessionKeys.IncidentID);
            }


            if (folderID == -1)
            {
                //flashUpload.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;

                PnlFileUpload.Visible = false;
                gridFiles.Visible = false;
                lnkRenameFolder.Visible = false;
                lnkDeleteFolder.Visible = false;
                btnResetCheckout.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;
            }
            else
            {
                //flashUpload.Visible = true;
                lblCounter.Visible = true;
                lblSpaceUsed.Visible = true;
                //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
                if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)

                {
                    PnlFileUpload.Visible = true;
                }
                else
                {


                    PnlFileUpload.Visible = false;
                    Response.Redirect(strUrl, false);
                    return;
                    
                }
                gridFiles.Visible = true;
                lblMsg.Text = string.Empty;
                lnkRenameFolder.Visible = true;
                lnkDeleteFolder.Visible = true;
                btnResetCheckout.Visible = true;
            }
            gridFiles.Visible = true;
        }
        else
            if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString().ToLower() == "central")
            {

                // flashUpload.QueryParameters = string.Format("project=0&folderID={0}&contractorID={1}&IncidentID=0", folderID, sessionKeys.UID);

            }
            else
            {
                //   flashUpload.Visible = false;
                PnlFileUpload.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;
            }

        //made project drop down not to visible as per the client requirements.
        ddlProject.Visible = false;
        if (Session["SelectedNode"] != null)
        {
            gridFiles.Visible = true;
            LoopThroughNodes(UltraWebTree1.Nodes);
            TxtFolderID.Value = Session["SelectedNode"].ToString();
            txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
        }
        sqlFileList.DataBind();
        gridFiles.DataBind();

        //Hide the admin links based on the users access permission rights on the document.
        if (sessionKeys.SID > 3 && Request.QueryString["project"] != null)
        {
            foreach (GridViewRow row in gridFiles.Rows)
            {
                int documentID = Convert.ToInt32(((Label)row.FindControl("lblID")).Text);
                bool isRestricted = false;
                using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
                {
                    using (SqlCommand cmd = new SqlCommand("IsRestrictedDocument", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("DocumentID", documentID);
                        cmd.Parameters.AddWithValue("UserID", sessionKeys.UID);
                        conn.Open();
                        isRestricted = Convert.ToBoolean(cmd.ExecuteScalar());
                    }
                }
                if (isRestricted)
                {
                    row.Visible = false;
                    ((LinkButton)row.Cells[0].FindControl("lnkDownLoad")).Enabled = false;
                    ((LinkButton)row.Cells[0].FindControl("lnkDownLoad")).ForeColor = System.Drawing.Color.Gray;
                    ((ImageButton)row.Cells[row.Cells.Count - 1].FindControl("btnDelete")).Enabled = false;
                }
            }
        }
        //if (sessionKeys.SID <= 3)
        if (Request.QueryString["project"] != null)
        {
            int ProjID;
            ProjID = Convert.ToInt32(Request.QueryString["project"]);
            if (ProjID > 0)
            {
                gridFiles.Columns[7].Visible = true;
            }
            else
            {
                gridFiles.Columns[7].Visible = false;
            }
        }
        else
        {
            gridFiles.Columns[7].Visible = false;
        }
        if (folderID > 0)
        {
            gridFiles.Visible = true;
            //flashUpload.Visible = true;
            //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
            if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)

            {
                PnlFileUpload.Visible = true;
            }
            else
            {

                PnlFileUpload.Visible = false;
                Response.Redirect(strUrl, false);
                return;
            }
            lblCounter.Visible = true;
            lblSpaceUsed.Visible = true;
        }
        else
        {
            gridFiles.Visible = false;
            //flashUpload.Visible = false;
            PnlFileUpload.Visible = false;
            lblCounter.Visible = false;
            lblSpaceUsed.Visible = false;
        }
        try
        {

            //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
            if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)

            {
                txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
            }
            else
            {

                PnlFileUpload.Visible = false;
                Response.Redirect(strUrl, false);
                return;
            }

        }
        catch (Exception ex)
        {
            string strEx = ex.ToString();
            //Ignore
        }
        gridFiles.Columns[10].Visible = false;
    }
    protected void MyTaskdocuments_PreRender()
    {
        lnkDeleteFolder.Visible = false;
        btnResetCheckout.Visible = false;
        //btnRefresh.Visible = false;
        //btnMoveFile.Visible = false;
        btnDeleteFile.Visible = false;

        string strUrl = "";
        string strPojectID = "0";

        if (Request.QueryString["project"] != null)
        {
            strPojectID = Request.QueryString["project"].ToString();
            //strUrl = "/Deffinity_team/MyTasksDocuments.aspx?project=" + strPojectID;
            strUrl = "/MyTasksDocuments.aspx?project=" + strPojectID;
        }
        else
        {
            //strUrl = "/Deffinity_team/MyTasksDocuments.aspx?mode=central";
            strUrl = "/MyTasksDocuments.aspx?mode=central";
        }


        if (projectID > 0 || sessionKeys.IncidentID > 0)
        {
            try
            {
                ddlProject.SelectedValue = projectID.ToString();
            }
            catch { ddlProject.SelectedValue = "0"; }
            //Pass the query string parameters to the flash file upload control.
            if (sessionKeys.IncidentID > 0)
            {
                projectID = 0;
                ddlProject.Visible = false;
                //rqdFolder.Visible = false;
            }
            else
            {
                ddlProject.Visible = true;
                //rqdFolder.Visible =true;

            }
            


            if (folderID == -1)
            {
                //flashUpload.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;

                PnlFileUpload.Visible = false;
                gridFiles.Visible = false;
                lnkRenameFolder.Visible = false;
                lnkDeleteFolder.Visible = false;
                btnResetCheckout.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;
            }
            else
            {
                //flashUpload.Visible = true;
                lblCounter.Visible = true;
                lblSpaceUsed.Visible = true;
                //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
                if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)

                {
                    PnlFileUpload.Visible = true;
                }
                else
                {


                    PnlFileUpload.Visible = false;
                    Response.Redirect(strUrl, false);
                    return;

                }
                gridFiles.Visible = true;
                lblMsg.Text = string.Empty;
                lnkRenameFolder.Visible = true;
                lnkDeleteFolder.Visible = true;
                btnResetCheckout.Visible = true;
            }
            gridFiles.Visible = true;
        }
        else
            if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString().ToLower() == "central")
            {

                // flashUpload.QueryParameters = string.Format("project=0&folderID={0}&contractorID={1}&IncidentID=0", folderID, sessionKeys.UID);

            }
            else
            {
                //   flashUpload.Visible = false;
                PnlFileUpload.Visible = false;
                lblCounter.Visible = false;
                lblSpaceUsed.Visible = false;
            }

        //made project drop down not to visible as per the client requirements.
        ddlProject.Visible = false;
        if (Session["SelectedNode"] != null)
        {
            gridFiles.Visible = true;
            LoopThroughNodes(UltraWebTree1.Nodes);
            TxtFolderID.Value = Session["SelectedNode"].ToString();
            txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
        }
        sqlFileList.DataBind();
        gridFiles.DataBind();

        //Hide the admin links based on the users access permission rights on the document.
        if (sessionKeys.SID > 3 && Request.QueryString["project"] != null)
        {
            foreach (GridViewRow row in gridFiles.Rows)
            {
                int documentID = Convert.ToInt32(((Label)row.FindControl("lblID")).Text);
                bool isRestricted = false;
                using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
                {
                    using (SqlCommand cmd = new SqlCommand("IsRestrictedDocument", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("DocumentID", documentID);
                        cmd.Parameters.AddWithValue("UserID", sessionKeys.UID);
                        conn.Open();
                        isRestricted = Convert.ToBoolean(cmd.ExecuteScalar());
                    }
                }
                if (isRestricted)
                {
                    row.Visible = false;
                    ((LinkButton)row.Cells[0].FindControl("lnkDownLoad")).Enabled = false;
                    ((LinkButton)row.Cells[0].FindControl("lnkDownLoad")).ForeColor = System.Drawing.Color.Gray;
                    ((ImageButton)row.Cells[row.Cells.Count - 1].FindControl("btnDelete")).Enabled = false;
                }
            }
        }
        //if (sessionKeys.SID <= 3)
        if (Request.QueryString["project"] != null)
        {
            int ProjID;
            ProjID = Convert.ToInt32(Request.QueryString["project"]);
            if (ProjID > 0)
            {
                gridFiles.Columns[7].Visible = true;
            }
            else
            {
                
                gridFiles.Columns[7].Visible = false;
                gridFiles.Columns[8].Visible = false;
                gridFiles.Columns[9].Visible = false;

                gridFiles.Columns[10].Visible = false; // Journals

                gridFiles.Columns[11].Visible = false;
                gridFiles.Columns[12].Visible = false;
                btnResetCheckout.Visible = false;
            }
        }
        else
        {
            
            gridFiles.Columns[7].Visible = false;
            gridFiles.Columns[8].Visible = false;
            gridFiles.Columns[9].Visible = false;
            gridFiles.Columns[10].Visible = false;// Journals
            gridFiles.Columns[11].Visible = false;
            gridFiles.Columns[12].Visible = false;
            btnResetCheckout.Visible = false;
        }
        if (folderID > 0)
        {
            gridFiles.Visible = true;
            //flashUpload.Visible = true;
            //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
            if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)

            {
                PnlFileUpload.Visible = true;
            }
            else
            {

                PnlFileUpload.Visible = false;
                Response.Redirect(strUrl, false);
                return;
            }
            lblCounter.Visible = true;
            lblSpaceUsed.Visible = true;
        }
        else
        {
            gridFiles.Visible = false;
            //flashUpload.Visible = false;
            PnlFileUpload.Visible = false;
            lblCounter.Visible = false;
            lblSpaceUsed.Visible = false;
        }
        try
        {

            //if (lblMsg.Text.Trim() != "Folder deleted successfully.")
            if (lblMsg.Text.Trim() != Resources.DeffinityRes.FolderDeletedSuccess)

            {
                txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
            }
            else
            {

                PnlFileUpload.Visible = false;
                Response.Redirect(strUrl, false);
                return;
            }

        }
        catch (Exception ex)
        {
            string strEx = ex.ToString();
            //Ignore
        } 
    }
    
    protected string PermissionURL(int Id)
    {
        if (Request.QueryString["project"] != null)
        {
            string url = string.Format("DocumentPermission.aspx?DocumentID={0}&project={1}", Id, Request.QueryString["project"].ToString());
            return url;
        }
        return string.Empty;
    }
    
    protected double GetTotalFilesSize()
    {

        double DSizeTotal = 0;
        try
        {
            foreach (GridViewRow dgi in gridFiles.Rows)
            {
                {
                    Label lblFileSize = (Label)dgi.FindControl("lblFileSize");
                    double dblblFileSize = Convert.ToDouble(lblFileSize.Text.Trim());
                    double fileSize = Convert.ToDouble(String.Format("{0:0.00}", dblblFileSize));

                    DSizeTotal = DSizeTotal + fileSize;


                }
            }
        }
        catch (Exception ex)
        {
            return DSizeTotal;
        }
        return DSizeTotal;
    }
    
    protected string getProjectReference()
    {
        if (Request.QueryString["project"] != null)
            return Request.QueryString["project"].ToString();
        else
            return "0";
    }

    protected string ResetEnableIcon(string ContractorID, string CheckOut)
    {
        string imageURL = string.Empty;


       
        if (ContractorID.Trim() == sessionKeys.UID.ToString())
        {
            if (CheckOut.Trim() == "True")
            {
                //imageURL = "/Deffinity_team/images/ico_reset.gif";
                imageURL = "/images/ico_reset.gif";
            }
            else
            {
                //imageURL = "/Deffinity_team/images/ico_reset_grey.gif";
                imageURL = "/images/ico_reset_grey.gif";                
            }            
        }
        else
        {

            //imageURL = "/Deffinity_team/images/ico_reset_grey.gif";
            imageURL = "/images/ico_reset_grey.gif";


        }
        return imageURL;
    }




    protected bool ResetEnable(string ContractorID, string CheckOut)
    {
        bool blnEnable = true;

        if (ContractorID.Trim() == sessionKeys.UID.ToString())
        {

            if (CheckOut.Trim() == "True")
            {
                blnEnable = true;
            }
            else
            {
                blnEnable = false;
            }           

        }
        else
        {
                blnEnable = false;
           
        }
        return blnEnable;
    }

    protected bool GetCheckInOutEnable(string CheckOut)
    {
        bool blnEnable = true;

        if (CheckOut.Trim() == "True")
        {
            blnEnable = false;

        }
        else
        {
            blnEnable = true;
        }
        return blnEnable;
    }

    protected string GetCheckInOut(string CheckOut, string CheckOutBy, string CheckOutDate)
    {
        string imageToolTip = string.Empty;

        if (CheckOut.Trim() == "False")
        {
            //imageToolTip = "Click here to Check Out";
            imageToolTip = Resources.DeffinityRes.ClickheretoCheckOut;
            

        }
        else
        {
            //string strMsg = "Checked out by "+CheckOutBy +" on "+CheckOutDate;//Nadeem Mohammed on 16/09/2010 at 09:15";
            //string strMsg = Resources.DeffinityRes.Checkedoutby + CheckOutBy + " " + Resources.DeffinityRes.CHeckedon + " " + CheckOutDate;//Nadeem Mohammed on 16/09/2010 at 09:15";
            //imageToolTip = "Click here to Check In";
           // imageToolTip = strMsg;

        }
        return imageToolTip;
    }

    protected bool GetCheckInOutEnableByID(string ContractorID, string CheckOut)
    {
        bool blnEnable = true;


        if (ContractorID.Trim() != sessionKeys.UID.ToString())
        {
            if (CheckOut.Trim() == "True")
            {
                blnEnable = false;
            }
            
        }
        return blnEnable;
    }
   

    protected string GetCheckInOutUrl(string id, string CheckOut)
    {

       //string str= "<script language=Javascript>  window.open('POPCheckin.aspx?ID="+ CheckOut +"')</script>";
        string str = "";
        if (CheckOut.ToLower() == "false")
        {

            str = "javascript:OpenChild(" + id + ","+ folderID +",'" + CheckOut.ToLower() + "');return true;";
        }
        else
        {
            str = "javascript:OpenChild(" + id + ","+ folderID +",'" + CheckOut.ToLower() + "');return false;";
        }

       
       return str;
    }


    protected string GetPortalViewImage(string CheckOut)
    {
        string imageURL = string.Empty;
        imageURL = "el-check";
        if (CheckOut.Trim() == "1")
        {

            imageURL = "el-check-empty";
            
        }
        return imageURL;
    }
    protected string GetCheckInOutIcon(string CheckOut)
    {
        string imageURL = string.Empty;

        if (CheckOut.Trim() == "False")
        {

            //imageURL = "/Deffinity_team/media/ico_checkout.gif";
            imageURL = "fa fa-lock";
        }
        else
        {
             
             //imageURL = "/Deffinity_team/media/ico_checkin.gif";
            imageURL = "fa fa-unlock-alt";
            
            
        }
        return imageURL;
    }
    protected string GetIcon(string fileName)
    {
        string imageURL = string.Empty;
        string fileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1);
        switch (fileExtension.ToLower())
        {
            case "xls":
            case "xlsx":
                imageURL = "fa fa-file-excel-o";
                break;
            case "doc":
            case "docx":
                imageURL = "fa fa-file-word-o";
                break;
            case "jpeg":
            case "jpg":
            case "png":
            case "gif":
            case "bmp":
            case "ico":
            case "psd":
            case "tif":
            case "psp":
            case "dwg":
            case "dxf":
            case "3dm":
                imageURL = "fa fa-file-image-o";
                break;
            case "aac":
            case "aif":
            case "iff":
            case "m3u":
            case "midi":
            case "mp3":
            case "mpa":
            case "wma":
            case "mov":
            case "flv":
            case "avi":
            case "swf":
            case "vob":
            case "wmv":
                imageURL = "fa fa-file-movie-o";
                break;
            case "7z":
            case "deb":
            case "gz":
            case "pkg":
            case "rar":
            case "sit":
            case "sitx":
            case "zip":
            case "zipx":
                imageURL = "fa fa-file-zip-o";
                break;
            case "txt":
                imageURL = "fa fa-file-text";
                break;
            case "pdf":
                imageURL = "fa fa-file-pdf-o";
                break;
            case "ppt":
            case "pptx":
                imageURL = "fa fa-file-powerpoint-o";
                break;
            case "vsd":
            case "vsdx":
                imageURL = "fa fa-file-code-o";
                break;
            default:
                imageURL = "fa fa-file-o";
                break;
        }
        return imageURL;
    }
    
    private void ShowBufferStatus()
    {

        lblMsg.Text = string.Empty;
        string count = "0";
        string action = "";
        if (Session["copiedFiles"] != null && Session["copied"] != null)
        {
            count = ((Hashtable)Session["copiedFiles"]).Count.ToString();
            action = (((bool)Session["copied"]) ? " - Copied" : " - Cut");
        }
        if (count.Trim().Equals("0"))
        {
           // lblMsg.Text = "Error, No File or Folders selected";
            lblMsg.Text = Resources.DeffinityRes.NoFileoFoldersSel;
        }
        else
        {
            lblMsg.Text = count + " " + Resources.DeffinityRes.Fileinamemory + " " + action;
        }
    }
    
    private void CopyFiles(bool cut)
    {
        Session["copiedFiles"] = null;
        Session["copied"] = null;
        ViewState["Links"] = "True";
        lblMsg.Text = string.Empty;
        try
        {
            Hashtable ht = new Hashtable();
            foreach (GridViewRow dgi in gridFiles.Rows)
            {
                CheckBox chkChecked = (CheckBox)dgi.FindControl("chkChecked");
                if (chkChecked.Checked)
                {
                    LinkButton lnkName = (LinkButton)dgi.FindControl("lnkDownLoad");
                    Label lblID = (Label)dgi.FindControl("lblID");
                    ht.Add(lblID.Text.Trim(), lnkName.Text);


                    Session["copiedFiles"] = ht;
                    Session["copied"] = !cut;
                }
            }

            ShowBufferStatus();
        }
        catch (Exception ex)
        {
            string strEx = ex.ToString();
            lblMsg.Text = ex.Message;
        }
    }
    
    private void PasteFiles()
    {


        lblMsg.Text = string.Empty;
        try
        {
            if (Session["copiedFiles"] != null && Session["copied"] != null)
            {
                Hashtable ht = (Hashtable)Session["copiedFiles"];
                bool copied = (bool)Session["copied"];
                string strCopy = copied ? "COPYANDPASTE" : "CUTANDPASTE";
                AC2P_DocumentsController ObjAC2P_DocumentsController = new AC2P_DocumentsController();

                foreach (DictionaryEntry de in ht)
                {

                    //if (int.Parse(de.Value.ToString()) == 0)
                    if (de.Key.ToString().Trim() != "")
                    {
                        int intRetval = ObjAC2P_DocumentsController.AC2PDocumentsInsert(Convert.ToInt32(de.Key.ToString().Trim()), folderID, strCopy);
                    }

                }
                if (copied)
                {
                    Session["copiedFiles"] = null;
                    Session["copied"] = null;
                }
                //lblMsg.Text = ht.Count + " file(s) are copied.";
                lblMsg.Text = ht.Count + " " + Resources.DeffinityRes.FilesCopied + " ";
                
            }
            else
            {
                //lblMsg.Text = "No File are copied to paste.";
                lblMsg.Text = Resources.DeffinityRes.Nofilescopied;
                
            }
            gridFiles.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = string.Empty;
            string strEx = ex.ToString();
            lblMsg.Text = ex.Message;
        }


    }
    
    private void RefreshFiles()
    {
        if (gridFiles.Rows.Count != 0)
        {
            gridFiles.EditIndex = -1;
            gridFiles.DataBind();
        }
    }
    
    private void DeleteFiles()
    {
        lblMsg.Text = string.Empty;
        string strDeleteIDS = string.Empty;
        foreach (GridViewRow dgi in gridFiles.Rows)
        {
            CheckBox chkChecked = (CheckBox)dgi.FindControl("chkChecked");
            if (chkChecked.Checked)
            {

                Label lblID = (Label)dgi.FindControl("lblID");
                if (strDeleteIDS == string.Empty)
                {
                    strDeleteIDS = lblID.Text.Trim() + ",";
                }
                else
                {
                    strDeleteIDS = strDeleteIDS + lblID.Text.Trim() + ",";
                }
            }
        }
        if (strDeleteIDS != string.Empty)
        {
            AC2P_DocumentsController ObjAC2P_DocumentsController = new AC2P_DocumentsController();
            int intRet = ObjAC2P_DocumentsController.AC2PDocumentsDelete(strDeleteIDS);
            NewTreeBinding();
            //lblMsg.Text = intRet.ToString() + " file(s) are deleted.";
            lblMsg.Text = intRet.ToString() + " " + Resources.DeffinityRes.FilesDeleted;
        }
        else
        {


            //lblMsg.Text = "Plese select file(s) to delete.";
            lblMsg.Text =Resources.DeffinityRes.SelFilesDeleted;
        }
    }
    
    private void UploadDocuments()
    {
        try
        {
            // Get the HttpFileCollection
            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {
                    //hpf.SaveAs(Server.MapPath("MyFiles") + "\\" +
                    //  System.IO.Path.GetFileName(hpf.FileName));
                    //Response.Write("<b>File: </b>" + hpf.FileName + "  <b>Size:</b> " +
                    //     hpf.ContentLength + "  <b>Type:</b> " + hpf.ContentType + " Uploaded Successfully <br/>");



                    byte[] myFileData = new byte[hpf.ContentLength];
                    hpf.InputStream.Read(myFileData, 0, hpf.ContentLength);
                    AC2P_DocumentsController AC2PDocumentsController = new AC2P_DocumentsController();
                    //string.Format("project={0}&folderID={1}&contractorID={2}&IncidentID={3}", , folderID, sessionKeys.UID, sessionKeys.IncidentID);
                    //AC2PDocumentsController.DN_CustomerCommonUploadInsertNew (-99, System.IO.Path.GetFileName(hpf.FileName), myFileData, System.IO.Path.GetFileName(hpf.FileName), hpf.ContentType, "P", hpf.ContentLength, folderID, sessionKeys.UID, sessionKeys.IncidentID);

                }
            }
            NewTreeBinding();
            //Infragistics.WebUI.UltraWebNavigator.Node SelectedFolder = null;
            //SelectedFolder = UltraWebTree1.SelectedNode;
            //SelectedFolder.Text = hfc.Count.ToString();
        }
        catch (Exception ex)
        {

        }


    }
    
    private void Health_CreateFolder()
    {

        lblMsg.Text = string.Empty;
        using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        {
            using (SqlCommand cmd = new SqlCommand("DEFFINITY_Health_CreateFolder", conn))
            {
                cmd.Parameters.AddWithValue("FolderName", txtCreateFolder.Text);
                if (Convert.ToInt32(folderID) == -1)
                    TxtFolderID.Value = "-1";
                cmd.Parameters.AddWithValue("ParentID", folderID);
                cmd.Parameters.AddWithValue("ContractorID", sessionKeys.UID);
                cmd.Parameters.AddWithValue("HealthCheckId", QueryStringValues.HealthCheckId);
                
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    //lblMsg.Text = "Folder Added successfully.";
                    lblMsg.Text = Resources.DeffinityRes.FolderAddedSuccess;
                    
                    NewTreeBinding();

                }
                else

                    //lblMsg.Text = "Folder creation failed.  The folder may already exists.";
                    lblMsg.Text = Resources.DeffinityRes.FolderExist;
                
            }
        }
    }
    
    private void CreateFolder()
    {
        lblMsg.Text = string.Empty;
        using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        {
            using (SqlCommand cmd = new SqlCommand("DEFFINITY_CreateFolder", conn))
            {
                cmd.Parameters.AddWithValue("FolderName", txtCreateFolder.Text);
                if (Convert.ToInt32(folderID) == -1)
                    TxtFolderID.Value = "-1";
                cmd.Parameters.AddWithValue("ParentID", folderID);
                cmd.Parameters.AddWithValue("ProjectReference", QueryStringValues.Project);
                if (Request.QueryString["mode"] != null)
                    cmd.Parameters.AddWithValue("PortfolioID", 0);
                else
                cmd.Parameters.AddWithValue("PortfolioID", -99);
                cmd.Parameters.AddWithValue("SDID", QueryStringValues.SDID);
                cmd.Parameters.AddWithValue("ContractorID", sessionKeys.UID);
                cmd.Parameters.AddWithValue("HealthCheckId", 0);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    //lblMsg.Text = "Folder Added successfully.";
                    lblMsg.Text = Resources.DeffinityRes.FolderAddedSuccess;
                    NewTreeBinding();

                }
                else

                    //lblMsg.Text = "Folder creation failed.  The folder may already exists.";
                lblMsg.Text = Resources.DeffinityRes.FolderExist;
            }
        }
    }
    
    private void RenameFolder()
    {
        lblMsg.Text = string.Empty;
        if (Convert.ToInt32(folderID) > 0)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("DEFFINITY_RenameFolder", conn))
                {
                    cmd.Parameters.AddWithValue("FolderID", folderID);
                    cmd.Parameters.AddWithValue("FolderName", txtCreateFolder.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {

                        //lblMsg.Text = "Folder renamed successfully.";

                        lblMsg.Text = Resources.DeffinityRes.Folderrenamedsuccess;
                        NewTreeBinding();
                    }
                    else
                    {

                        //lblMsg.Text = "Updation failed.  Please try again.";
                        lblMsg.Text = Resources.DeffinityRes.Updationfailed;
                    }
                }
            }
        }
        else
        {
            //lblMsg.Text = "Please select a folder to rename.";
            lblMsg.Text = Resources.DeffinityRes.SelFolderToRename;
        }
    }
    
    private void DeleteFolder()
    {
        Infragistics.WebUI.UltraWebNavigator.Node deleteNode = null;
        deleteNode = UltraWebTree1.SelectedNode;
        
        if (deleteNode != null)
        {

            string strDeletedIDs = string.Empty;


            if (deleteNode.Nodes.Count > 0)
            {
                strDeletedIDs = DeletedNodesString(deleteNode.Nodes, strDeletedIDs);
            }

            if ((int)deleteNode.DataKey != 0)
            {
                strDeletedIDs += deleteNode.DataKey;
            }





            int datakey = Convert.ToInt32(UltraWebTree1.SelectedNode.DataKey);
            lblMsg.Text = string.Empty;
            //if (Convert.ToInt32(folderID) > 0)
            //{
            if (strDeletedIDs != string.Empty)
            {
                using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
                {
                    using (SqlCommand cmd = new SqlCommand("DEFFINITY_DeleteFolder", conn))
                    {
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("FolderIDS", strDeletedIDs);
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();
                        txtCreateFolder.Text = "";
                        if (cmd.ExecuteNonQuery() > 0)
                        {

                            //lblMsg.Text = "Folder deleted successfully.";
                            lblMsg.Text = Resources.DeffinityRes.FolderDeletedSuccess;

                            txtCreateFolder.Text = "";
                            TxtFolderID.Value = "-1";
                            gridFiles.DataBind();
                            NewTreeBinding();

                            PnlFileUpload.Visible = false;
                        }
                        else
                        {

                            //lblMsg.Text = "Delete failed.  Please try again.";
                            lblMsg.Text = Resources.DeffinityRes.DeleteFailed;
                        }
                    }
                }
            }
            else
            {

                //lblMsg.Text = "Please select a folder to rename.";
                lblMsg.Text = Resources.DeffinityRes.SelFolderToRename;
            }


        }
        else
        {
            //lblMsg.Text = "Please select a folder to rename.";
            lblMsg.Text = Resources.DeffinityRes.SelFolderToRename;
        }
    }
    
    private void DisplayResults(SearchResultList searchResults)
    {
        StringBuilder sb = new StringBuilder();
        if (searchResults.Count <= 0)
            sb.Append("<div><h3 class='none'>No results found...</h3></div>");
        else
            foreach (SearchResult result in searchResults)
            {
                if (string.IsNullOrEmpty(result.version.Trim()))
                    result.version = "1";
                sb.Append(string.Format("<div style='font-size:12px;'><span style='font-size:14px;'><img src='{0}' alt='File Icon' style='vertical-align:bottom'/>", GetIcon(result.fileName)));
                sb.Append(string.Format("<strong><a style='color:blue;padding-left:5px;text-decoration:underline;' target='_blank' href='Download.aspx?FileID={1}'>{0}</a></strong></span><br/>", result.fileName, result.fileID));
                sb.Append(string.Format("<span style='padding-left:20px'>{0} {1}</span>", "Uploaded By:", Server.HtmlEncode(result.uploadedBy)));
                sb.Append(string.Format("<span style='padding-left:20px'>{0} {1}</span>", "Uploaded On:", result.uploadedTime));
                sb.Append(string.Format("<span style='padding-left:20px'>{0} {1}</span><br/>", "Version:", result.version));
                sb.Append("</div><br/>");
            }

        Control divSearchResults = pnlSearchResults.FindControl("divSearchResults");
        System.Web.UI.HtmlControls.HtmlContainerControl HtmlDivControl = (System.Web.UI.HtmlControls.HtmlContainerControl)divSearchResults;



        HtmlDivControl.InnerHtml = sb.ToString();


    }
    
    private void searchTextFiles(Delegate dispResults, string txtSearchBox)
    {
        Dictionary<int, string> searchedFiles = new Dictionary<int, string>();
        string searchString = txtSearchBox;
        if (!string.IsNullOrEmpty(searchString))
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("DEFFINITY_SearchInFileAll", conn))
                {
                    cmd.Parameters.AddWithValue("SearchKey", searchString);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        using (DataTableReader tableReader = table.CreateDataReader())
                        {
                            while (tableReader.Read())
                            {
                                searchedFiles.Add(Convert.ToInt32(tableReader["ID"]), tableReader["DocumentName"].ToString());
                            }
                        }
                    }
                }
            }
            //Dinesh

            //SearchInFiles fileSearch = new SearchInFiles();
            //SearchResultList searchResults = fileSearch.getAc2pDocument(searchedFiles, searchString);
            //if (dispResults != null)
            //    dispResults.DynamicInvoke(searchResults);
        }
    }
    
    protected void DocumentPermissionsList(string DocumentID)
    {
        int projectReference = 0;
        if (Request.QueryString["project"] != null)
            projectReference = Convert.ToInt32(Request.QueryString["project"]);

        using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        {
            using (SqlCommand cmd = new SqlCommand("DocumentPermissions", conn))
            {
                cmd.Parameters.AddWithValue("ProjectReference", projectReference);
                cmd.Parameters.AddWithValue("DocumentID", DocumentID);
                //cmd.Parameters.AddWithValue("ProjectReference", 53);
                //cmd.Parameters.AddWithValue("DocumentID", DocumentID);
                //cmd.Parameters.AddWithValue("DocumentID", 16);

                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    gridResources.DataSource = table;
                    gridResources.DataBind();

                }
            }
        }
    }
    
    protected void SetPermissions(string DocumentID, string ContractorID)
    {
        int projectReference = 0;
        if (Request.QueryString["project"] != null)
            projectReference = Convert.ToInt32(Request.QueryString["project"]);
        string mystring = string.Empty;
        //if (Request.QueryString["DocumentID"] != null && Request.QueryString["ContractorID"] != null)
        //{
        using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        {
            using (SqlCommand cmd = new SqlCommand("SetDocumentPermission", conn))
            {
                cmd.Parameters.AddWithValue("DocumentID", Convert.ToInt32(DocumentID));
                cmd.Parameters.AddWithValue("ContractorID", Convert.ToInt32(ContractorID));
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        //}
    }

    #endregion Control Methods


    protected void btnMultiDownload_Click(object sender, EventArgs e)
    {
        try
        {
            string strDeleteIDS = string.Empty;
            int intChecked = 0;
            foreach (GridViewRow dgi in gridFiles.Rows)
            {
                CheckBox chkChecked = (CheckBox)dgi.FindControl("chkChecked");
                if (chkChecked.Checked)
                {
                    intChecked++;
                    Label lblID = (Label)dgi.FindControl("lblID");
                    if (strDeleteIDS == string.Empty)
                    {
                        strDeleteIDS = lblID.Text.Trim() + ",";
                    }
                    else
                    {
                        strDeleteIDS = strDeleteIDS + lblID.Text.Trim() + ",";
                    }

                }
            }
            //gridFiles.DataBind();
            //foreach (GridViewRow dgi in gridFiles.Rows)
            //{
            //    CheckBox chkChecked = (CheckBox)dgi.FindControl("chkChecked");
            //    if (chkChecked.Checked)
            //    {
            //        chkChecked.Checked = false;
            //    }
            //}
            if (!string.IsNullOrEmpty(strDeleteIDS))
            {
                string directoryPath = Server.MapPath(string.Format("~/WF/UploadData/{0}", "DocTemp"));
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                else
                {
                    foreach (string filename in Directory.GetFiles(directoryPath))
                    {
                        File.Delete(filename);
                    }
                }
                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    zip.AddDirectoryByName("Files");
                    string DocIDs = strDeleteIDS.Substring(0, strDeleteIDS.Length - 1);//"10103,10104,10105,10106";
                    int index = 1;
                    using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
                    {
                        string sqlCommand = string.Format("SELECT ContentType,Document,DocumentName FROM AC2P_Documents WHERE ID in ({0})", DocIDs);
                        using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                        {
                            conn.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        HttpContext.Current.Response.ContentType = reader.GetString(0);
                                        byte[] getContent = (byte[])reader[1];
                                        var filepath = directoryPath + "\\" + index.ToString() + "_" + reader.GetString(2).Trim().Replace(" ", string.Empty);
                                        File.WriteAllBytes(filepath, getContent);

                                        zip.AddFile(filepath, "Files");
                                        index++;
                                    }
                                    reader.Close();
                                }
                            }
                        }
                        Response.Clear();
                        Response.BufferOutput = false;
                        string zipName = String.Format("Files_{0}.zip", DateTime.Now.ToString("yyyy-MM-dd-HHmmss"));
                        Response.ContentType = "application/zip";
                        Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                        zip.Save(Response.OutputStream);
                        Response.End();
                    }
                }
            }
            else
            {
                //lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Please select File(s)";
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
}

