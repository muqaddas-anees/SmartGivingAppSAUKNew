using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Deffinity.ProgrammeEntitys;
using Deffinity.ProgrammeManagers;
using Deffinity.Bindings;
using Microsoft.ApplicationBlocks.Data;
using ProgrammeMgt.DAL;
using ProgrammeMgt.Entity;
using System.Linq;

 

public partial class ProgrammeManagement : BasePage
{
    private string cs = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    private string er = "";
    DisBindings _ddlBind = new DisBindings();
    Admin ad = new Admin();
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);

    ArrayList hif = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Page Header
        lblError.Text = "";
        lblMsg.Text = "";
       // Master.PageHead = Resources.DeffinityRes.Program; //"Program";
        if (!IsPostBack)
        {
            ddlprogrammowner.SelectedValue ="0";
            BindProgramme();
            BindProgrammeSub();
            BindDefaults();
           
            if (DropDownListCustomer.Items.Count > 1)
            {
                btnAddNew.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnAddNew.Enabled = false;
                btnDelete.Enabled = false;
            }
            //if (sessionKeys.ProgrammeID != 0)
            //{
            //    //ProgramName.Visible = true;
            //    getProgrammeDisplay(sessionKeys.ProgrammeID);
            //}

        }
        if (Request.QueryString["type"] != null)
        {
            ImageButton1.Visible = true;
        }
        if (Request.QueryString["Project"] != null)
        {
            ImageButton1.Visible = true;
        }
	    Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";

    }
    //protected void Page_UnLoad()
    //{
    //    sessionKeys.ProgrammeID = 0;
    //    clearFields();
    //}
    protected void BindTreeview(int intPortfolioID,string strCheck)
    {
        
        UltraWebTree1.Nodes.Clear();
        Infragistics.WebUI.UltraWebNavigator.Node rootNode = new Infragistics.WebUI.UltraWebNavigator.Node();
        rootNode.DataKey = 0;
        rootNode.Text = "Programme"; //Resources.DeffinityRes.FilesFolders;// "Files/Folders";
        rootNode.Tag = "Programme";//Resources.DeffinityRes.FilesFolders;// "Files/Folders";
        UltraWebTree1.Nodes.Add(rootNode);
        //if (intPortfolioID>0)
        //{
            NewTreeBinding();
            //NewTreeBinding(intPortfolioID);
            //if (strCheck == "Y")
            //{

            //    if (!ddlGroups.SelectedItem.Text.Contains("Please select..."))
            //    {
            //        //UltraWebTree1.Find(ddlGroups.SelectedItem.Text).Selected = true;

            //        //if (HiddenField3.Value != "")
            //        //{

            //        //    FindAndSentTheDataKey(Convert.ToInt16(HiddenField3.Value));

            //        //    //foreach (Infragistics.WebUI.UltraWebNavigator.Node n in UltraWebTree1.Nodes)
            //        //    //{                         
            //        //    //    FindSelection(n, Convert.ToInt16(HiddenField3.Value));

            //        //    //}
                        
            //        //}


                    

                    
            //    }
            //}
            
        //}
       // UltraWebTree1.ExpandAll();
            //if (rootNode.DataKey.ToString() == "0")
            //{
            //    UltraWebTree1.ExpandAll();
            //}
            //else
            //{
            //    UltraWebTree1.ExpandOnClick = false;
            //}

           
           // UltraWebTree1.ExpandOnClick = false;
    }


    //protected void NewTreeBinding(int intPortfolioID)
    protected void NewTreeBinding()
    {
        DataSet ds11 = new DataSet();
        SqlCommand cmdSQL1 = new SqlCommand();
        cmdSQL1.CommandType = CommandType.StoredProcedure;
        cmdSQL1.Parameters.Clear();
        cmdSQL1.CommandText = "DEFFINITY_GETProgrammeLevels_Portfolio1";
        //cmdSQL1.Parameters.AddWithValue("@PortfolioID", intPortfolioID);
        //}
        cmdSQL1.Connection = cn;
        SqlDataAdapter da11 = new SqlDataAdapter(cmdSQL1);
        da11.Fill(ds11, "OPERATIONSOWNERS");
        try
        {
            foreach (DataRow row in ds11.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                string folderName = row["OPERATIONSOWNERS"].ToString();
                string FolderNameReal = row["OPERATIONSOWNERS"].ToString();

                string imageUrl = row["image"].ToString();
                int parentID = Convert.ToInt32(row["MasterProgramme"]);
                Infragistics.WebUI.UltraWebNavigator.Node node = new Infragistics.WebUI.UltraWebNavigator.Node();
                node.Text = folderName;
                node.Tag = FolderNameReal;
                //node.Tag = "";
                node.DataKey = id;
                       
                
                node.Styles.Padding.Bottom = 0;
                if (parentID == 0) UltraWebTree1.Nodes[0].Nodes.Add(node);
                else AddChild(UltraWebTree1.Nodes[0].Nodes, node, parentID);
                
            }
            UltraWebTree1.InitialExpandDepth = 1;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    private void FindAndSentTheDataKey(int intDataKey)
    {
        foreach (Infragistics.WebUI.UltraWebNavigator.Node n in UltraWebTree1.Nodes)
        {
            //if (blnFromDropdown)
            //{
                FindSelection(n, intDataKey);
            //}
            //else
            //{
            //    FindSelection(n, Convert.ToInt16(HiddenField3.Value));
            //}

        }
    }
    private void FindSelection(Infragistics.WebUI.UltraWebNavigator.Node node, int intDataKey)
    {
       
        
        
        foreach (Infragistics.WebUI.UltraWebNavigator.Node n in node.Nodes)
        {
            if (Convert.ToInt32(n.DataKey) == intDataKey)
            {
               
                n.Selected = true;                  
                
                break;

            }
            else
            {
                //n.Selected = true;
                if (n.Nodes.Count > 0)
                    //FindNodeByTag(NodeTag, n);
                    FindSelection(n, intDataKey);
            }        

        }
    
        
    }



    private void AddChild(Infragistics.WebUI.UltraWebNavigator.Nodes nodes, Infragistics.WebUI.UltraWebNavigator.Node addNode, int parentID)
    {
        foreach (Infragistics.WebUI.UltraWebNavigator.Node node in nodes)
        {
            if (Convert.ToInt32(node.DataKey) == parentID)
            {
                node.Nodes.Add(addNode);   
                //node.Nodes
                break;
            }
            else
                AddChild(node.Nodes, addNode, parentID );
        }
    }
    protected void DropDownListCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        rdbLevel1.Enabled = false;
        rdbLevel2.Enabled = false;
        HiddenField2.Value = "";
        HiddenField3.Value = "";
        BindTreeview(Convert.ToInt32(DropDownListCustomer.SelectedValue), "N");
        BindProgramme();
        if (DropDownListCustomer.SelectedValue == "0")
        {
            DDLGroupDefault();
        }
        else
        {
            clearFields();
        }

        if (DropDownListCustomer.Items.Count > 1)
        {
            btnAddNew.Enabled = true;
            btnDelete.Enabled = true;
        }
        else
        {
            btnAddNew.Enabled = false;
            btnDelete.Enabled = false;
        }
        if (!btnCancel.Visible)
        {
            divLevel.Visible = false;
        }

        //txtEmail.Text = _ddlBind.exeScalar("Select EmailAddress from Contractors where ID=" + ddlprogrammowner.SelectedValue, false);
    }
    protected void UltraWebTree1_NodeClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
    {
       // txtCostcenter.Text = "UltraWebTree1_NodeClicked";


        int datakey = Convert.ToInt32(UltraWebTree1.SelectedNode.DataKey);
        //Session["SelectedNode"] = e.Node.DataKey;

        sessionKeys.ProgrammeID = Convert.ToInt32(UltraWebTree1.SelectedNode.DataKey);
        try
        {
            
            //ddlGroups.SelectedValue = e.Node.DataKey.ToString();
            if (datakey != 0)
            {
                int intProgramID = 0;
                if (HiddenField2.Value != "")
                {
                    ddlGroups.SelectedValue = HiddenField2.Value;
                    ddlPgmlevel2.SelectedValue = HiddenField2.Value;
                    intProgramID = Convert.ToInt16(HiddenField2.Value);
                    BindProgrammeSub();

                    if (HiddenField3.Value != "")
                    {
                        if (HiddenField3.Value != "0")
                        {

                            dropdownSubProgramme.SelectedValue = HiddenField3.Value;
                            intProgramID = Convert.ToInt16(HiddenField3.Value);
                            ddlGroups.SelectedValue = HiddenField2.Value;
                            ddlPgmlevel2.SelectedValue = HiddenField2.Value;
                        }
                    }

                }
           
            //BindProgrammeTreeNodeClick(int.Parse(e.Node.DataKey.ToString()));
            //if (ddlGroups.SelectedValue != "0")
            //{


            
            //if (DropDownListCustomer.SelectedValue != "0")
            //{
            //    intProgramID = Convert.ToInt32(DropDownListCustomer.SelectedValue);

            //}
            //else
            //{
            //    if (ddlGroups.SelectedValue != "0")
            //    {
            //        intProgramID = Convert.ToInt32(ddlGroups.SelectedValue);
            //    }
            //}

            if (intProgramID > 0)
            {
                if (btnAddNew.Visible)
                {
                    panelEditProgramName.Visible = true;
                }
                getProgrammeDisplay(intProgramID);
                
                //rdbLevel1.Checked = false;
                //rdbLevel2.Checked = true;
                //BindProgrammeLevel(intProgramID);
            }
            }
            else
            {
                clearFields();
            }

            //if (DropDownListCustomer.SelectedValue!= "0")
            //{
            //    if (ddlGroups.SelectedValue != "0")
            //    {
            //        sessionKeys.ProgrammeID = Convert.ToInt32(ddlGroups.SelectedValue);
            //        sessionKeys.ProgrammeName = ddlGroups.SelectedItem.Text;
            //        getProgrammeDisplay(sessionKeys.ProgrammeID);
            //        panelEditProgramName.Visible = true;
            //        //rdbLevel1.Checked = false;
            //        //rdbLevel2.Checked = true;
            //        BindProgrammeLevel(sessionKeys.ProgrammeID);
            //    }
            //}
            //else
            //{
            //    DropDownListCustomer.SelectedValue = DropDownListCustomer.SelectedValue;
            //    sessionKeys.ProgrammeID = 0;
                //divLevel.Visible = false;
            //    clearFields();
            //    BindProgrammeLevel(-1);

            //}
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

        //TxtFolderID.Value = UltraWebTree1.SelectedNode.DataKey.ToString();
        //folderID = Convert.ToInt32(TxtFolderID.Value);
        //txtCreateFolder.Text = UltraWebTree1.SelectedNode.Tag.ToString();
        //string projectID = "0";
        //PnlFileUpload.Visible = true;
        //if (Request.QueryString["project"] != null)
        //{
        //    projectID = Request.QueryString["project"].ToString();
        //}
        ////if (Page.Request.Path.ToLower().Contains("customerdocs.aspx"))
        //if ((Page.Request.Path.ToLower().Contains("customerdocs.aspx")) || (Page.Request.Path.ToLower().Contains("portfoliodocs.aspx")))
        //{
        //    Response.Redirect(string.Format("{0}?folderID={1}", Request.Url.AbsolutePath, folderID));
        //}
        //else if (Page.Request.Path.ToLower().Contains("healthcheckformdocs.aspx"))
        //{
        //    Response.Redirect(string.Format("{0}?HealthCheckId={1}&folderID={2}&PID={3}", Request.Url.AbsolutePath, QueryStringValues.HealthCheckId, folderID, Request.QueryString["PID"]));
        //}
        //else if (Page.Request.Path.ToLower().Contains("sddocuments_frm.aspx"))
        //{
        //    Response.Redirect(string.Format("{0}?SDID={1}&folderID={2}", Request.Url.AbsolutePath, QueryStringValues.SDID, folderID));
        //}
        //else if (Page.Request.Path.ToLower().Contains("mytasksdocuments.aspx"))
        //{
        //    if (Request.QueryString["mode"] == null)
        //        Response.Redirect(string.Format("{0}?project={1}&folderID={2}", Request.Url.AbsolutePath, ddlProject.SelectedValue, folderID));
        //    else
        //        Response.Redirect(string.Format("{0}?mode=central&folderID={2}", Request.Url.AbsolutePath, ddlProject.SelectedValue, folderID));

        //}
        //else if (Page.Request.Path.ToLower().Contains("rfidocuments.aspx"))
        //{
        //    //RFIDocuments

        //    Response.Redirect(string.Format("{0}?project={1}&folderID={2}&VendorID={3}", Request.Url.AbsolutePath, projectID, folderID, QueryStringValues.Vendor));
        //}
        ////RFIDocuments
        //else
        //{
        //    if (Request.QueryString["mode"] == null)
        //        Response.Redirect(string.Format("{0}?project={1}&folderID={2}", Request.Url.AbsolutePath, ddlProject.SelectedValue, folderID));
        //    else
        //        Response.Redirect(string.Format("{0}?mode=central&folderID={2}", Request.Url.AbsolutePath, ddlProject.SelectedValue, folderID));

        //}

    }
    #region default bindings
    private void BindDefaults()
    {
        divLevel.Visible = false;
        BindOwner();
        //BindPortfolio();
        BindPortfolioTreeDropdown();
       BindTreeview(0,"N");
    }
    //private void BindPortfolio()
    //{
    //    ddlPortfolio.DataSource = DefaultDatabind.b_Portfolio();
    //    ddlPortfolio.DataValueField = "ID";
    //    ddlPortfolio.DataTextField = "PortFolio";
    //    ddlPortfolio.DataBind();
    //    ddlPortfolio.Items.Insert(0, Constants.ddlDefaultBind(true));
    //}
    private void BindPortfolioTreeDropdown()
    {

        DropDownListCustomer.DataSource = DefaultDatabind.b_Portfolio();
        DropDownListCustomer.DataValueField = "ID";
        DropDownListCustomer.DataTextField = "PortFolio";
        DropDownListCustomer.DataBind();
        DropDownListCustomer.Items.Insert(0, Constants.ddlDefaultBind(true));
    }

    //private void BindProgrammeLevelOne()
    //{

    //    DataTable dt = new DataTable();
    //    dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners where PortfolioID=@portfolio  and OperationsOwners.Level=1 order by  OperationsOwners.OperationsOwners",
    //    new SqlParameter("@portfolio", int.Parse(string.IsNullOrEmpty(DropDownListCustomer.SelectedValue) ? "0" : DropDownListCustomer.SelectedValue))).Tables[0];
        
    //    ddlPgmlevel2.DataSource = dt;
    //    ddlPgmlevel2.DataTextField = "OPERATIONSOWNERS";
    //    ddlPgmlevel2.DataValueField = "ID";
    //    ddlPgmlevel2.DataBind();
    //    //Here New
    //    if (HiddenField2.Value != "")
    //    {
    //        ddlPgmlevel2.SelectedValue = HiddenField2.Value;
    //    }
    //    ////rdbLevel2
    //    //if (ddlPgmlevel2.Visible == true)
    //    //{
    //    //    rdbLevel2.Checked = true;
    //    //    rdbLevel1.Checked = false;
    //    //}
    //    //else
    //    //{
    //    //    rdbLevel2.Checked = false;
    //    //    rdbLevel1.Checked = true;
    //    //}
    //    //try
    //    //{
    //    //    ddlPgmlevel2.DataSource = DefaultDatabind.GetProgrammes(ProgrammeID);
    //    //    ddlPgmlevel2.DataValueField = "ID";
    //    //    ddlPgmlevel2.DataTextField = "OPERATIONSOWNERS";
    //    //    ddlPgmlevel2.DataBind();
    //    //    ddlPgmlevel2.SelectedValue = HiddenField2.Value;
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    LogExceptions.WriteExceptionLog(ex);
    //    //}
    //    //ddlPgmlevel2.Items.Insert(0, Constants.ddlDefaultBind(true));
    //}

    //private void BindProgrammeLevel(int ProgrammeID)
    //{

    //    DataTable dt = new DataTable();
    //    dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners where PortfolioID=@portfolio  and OperationsOwners.Level=1 order by  OperationsOwners.OperationsOwners",
    //    new SqlParameter("@portfolio", int.Parse(string.IsNullOrEmpty(DropDownListCustomer.SelectedValue) ? "0" : DropDownListCustomer.SelectedValue))).Tables[0];
    //    ddlPgmlevel2.DataSource = dt;
    //    ddlPgmlevel2.DataTextField = "OPERATIONSOWNERS";
    //    ddlPgmlevel2.DataValueField = "ID";
    //    ddlPgmlevel2.DataBind();
    //    if (HiddenField2.Value != "")
    //    {
    //        ddlPgmlevel2.SelectedValue = HiddenField2.Value;
    //    }

    //    //try
    //    //{
    //    //    ddlPgmlevel2.DataSource = DefaultDatabind.GetProgrammes(ProgrammeID);
    //    //    ddlPgmlevel2.DataValueField = "ID";
    //    //    ddlPgmlevel2.DataTextField = "OPERATIONSOWNERS";
    //    //    ddlPgmlevel2.DataBind();
    //    //    ddlPgmlevel2.SelectedValue = HiddenField2.Value;
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    LogExceptions.WriteExceptionLog(ex);
    //    //}
    //    //ddlPgmlevel2.Items.Insert(0, Constants.ddlDefaultBind(true));
    //}
    //private void BindProgrammeTreeNodeClick(int MastProgramme)
    //{
    //    try
    //    {
    //        // _ddlBind.DdlBindSelect(ddlGroups, "SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners order by OperationsOwners ", "ID", "OperationsOwners", false, false, true);
    //        DataTable dt = new DataTable();
    //        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners where PortfolioID=@portfolio and MasterProgramme=@MasterProgramme  order by  OperationsOwners.OperationsOwners",
    //            new SqlParameter("@portfolio", int.Parse(string.IsNullOrEmpty(DropDownListCustomer.SelectedValue) ? "0" : DropDownListCustomer.SelectedValue)),new SqlParameter("@MasterProgramme",MastProgramme)).Tables[0];
    //        ddlGroups.DataSource = dt;
    //        ddlGroups.DataTextField = "OperationsOwners";
    //        ddlGroups.DataValueField = "ID";
    //        ddlGroups.DataBind();
    //        ddlGroups.Items.Insert(0, new ListItem("Please select...", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    private void BindProgramme()
    {
        try
        {
            // _ddlBind.DdlBindSelect(ddlGroups, "SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners order by OperationsOwners ", "ID", "OperationsOwners", false, false, true);
            DataTable dt = new DataTable();
            //dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners where PortfolioID=@portfolio  order by  OperationsOwners.OperationsOwners", //Modified by Dine
            //dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners where PortfolioID=@portfolio  and OperationsOwners.Level=1 order by  OperationsOwners.OperationsOwners",
            //new SqlParameter("@portfolio", int.Parse(string.IsNullOrEmpty(DropDownListCustomer.SelectedValue)?"0":DropDownListCustomer.SelectedValue))).Tables[0];
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners where  OperationsOwners.Level=1 order by  OperationsOwners.OperationsOwners").Tables[0];
            ProgrammeDataContext programme = new ProgrammeDataContext();

            var list = (from r in programme.OperationsOwners
                        where r.MasterProgramme ==0 && r.Level==1
                        select r).ToList();
            if (list != null)
            {
                ddlGroups.DataSource = list;
                ddlGroups.DataTextField = "OperationsOwners";
                ddlGroups.DataValueField = "ID";
                ddlGroups.DataBind();
                ddlGroups.Items.Insert(0, new ListItem("Please select...", "0"));
                ddlGroups.SelectedIndex = 0;

                ddlPgmlevel2.DataSource = list;
                ddlPgmlevel2.DataTextField = "OperationsOwners";
                ddlPgmlevel2.DataValueField = "ID";
                ddlPgmlevel2.DataBind();
                ddlPgmlevel2.Items.Insert(0, new ListItem("Please select...", "0"));
                ddlPgmlevel2.SelectedIndex = 0;
            }
           

            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindProgrammeSub()
    {
        try
        {
            // _ddlBind.DdlBindSelect(ddlGroups, "SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners order by OperationsOwners ", "ID", "OperationsOwners", false, false, true);
            DataTable dt = new DataTable();
            //dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners where PortfolioID=@portfolio  order by  OperationsOwners.OperationsOwners", //Modified by Dine
            //dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners where PortfolioID=@portfolio  and OperationsOwners.Level=2 order by  OperationsOwners.OperationsOwners",
            //new SqlParameter("@portfolio", int.Parse(string.IsNullOrEmpty(ddlGroups.SelectedValue) ? "0" : ddlGroups.SelectedValue))).Tables[0];

             ProgrammeDataContext programme = new ProgrammeDataContext();

            var list = (from r in programme.OperationsOwners
                        where r.MasterProgramme >0 && r.Level==2
                        && r.MasterProgramme==int.Parse(string.IsNullOrEmpty(ddlGroups.SelectedValue) ? "0" : ddlGroups.SelectedValue)
                        select r).ToList();
            if (list != null)
            {
                if (ddlGroups.SelectedValue != "0")
                {
                    dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners where  MasterProgramme=@MasterProgramme  order by  OperationsOwners.OperationsOwners",
                    new SqlParameter("@MasterProgramme", int.Parse(string.IsNullOrEmpty(ddlGroups.SelectedValue) ? "0" : ddlGroups.SelectedValue))).Tables[0];
                    if (list.Count != 0)
                    {
                        dropdownSubProgramme.Items.Clear();
                        dropdownSubProgramme.DataSource = list;
                        dropdownSubProgramme.DataTextField = "OperationsOwners";
                        dropdownSubProgramme.DataValueField = "ID";
                        try
                        {
                            dropdownSubProgramme.DataBind();
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                        dropdownSubProgramme.Items.Insert(0, new ListItem("Please select...", "0"));
                    }
                    else
                    {
                        dropdownSubProgramme.Items.Clear();
                        dropdownSubProgramme.Items.Insert(0, new ListItem("Please select...", "0"));
                        dropdownSubProgramme.SelectedIndex = 0;
                    }
                    //dropdownSubProgramme.SelectedIndex = -1;
                }
                else
                {

                    dropdownSubProgramme.Items.Clear();
                    dropdownSubProgramme.Items.Insert(0, new ListItem("Please select...", "0"));
                    dropdownSubProgramme.SelectedIndex = 0;
                }
            }

           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindOwner()
    {
        _ddlBind.DdlBindSelect(ddlprogrammowner, "select ID,ContractorName from Contractors where (SID = 1 or SID = 2 or SID = 3) and Status = 'ACTIVE' order by ContractorName", "ID", "ContractorName", false, true);
    }
    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (!ddlGroups.Visible)
        {
            if (!string.IsNullOrEmpty(txtGroups.Text))
            {
           bool blnRetval =InsertProgramme();

               if (!blnRetval)
               {
                   HiddenField2.Value = "";
                   HiddenField3.Value = "";

                   if (!btnAddNew.Visible)
                   {
                       clearFields_CancelSubmit();
                   }
                   else
                   {
                       clearFields();
                   }
                   //BindTreeview(Convert.ToInt32(DropDownListCustomer.SelectedValue), "Y");
                   BindTreeview(0, "Y");
               }
            }
            else
            {
                lblError.Text = Resources.DeffinityRes.PlsenterProgramme; //"Please enter Programme";
            }


        }
        else
        {

            int intID = 0;
            string strCheckProg = "";

            if (dropdownSubProgramme.SelectedValue != "0")//old if (dropdownSubProgramme.SelectedValue != "Please select...")
            {
                intID = Convert.ToInt32(dropdownSubProgramme.SelectedValue);
                strCheckProg = "sub";
            }
            else
            {
                if (ddlGroups.SelectedValue != "0")//if (ddlGroups.SelectedValue != "Please select...")
                {
                    intID = Convert.ToInt32(ddlGroups.SelectedValue);
                    strCheckProg = "main";
                }
            }

            if (intID != 0)
            {

                UpdateProgramme(intID, strCheckProg);
            }
            else
            {
                lblError.Text = Resources.DeffinityRes.PlsselectProgramme; //"Please select Programme";
            }




            clearFields();
           // BindTreeview(Convert.ToInt32(DropDownListCustomer.SelectedValue), "Y");
            BindTreeview(0, "Y");
            
        }


        //lblMsg.Text = "";
        //try
        //{


        //    if (panleDdl.Visible)
        //    {
        //        //update email id
        //        if (ddlGroups.SelectedValue != "Please select...")
        //            UpdateProgramme(Convert.ToInt32(ddlGroups.SelectedValue));
        //        else
        //            lblError.Text = Resources.DeffinityRes.PlsselectProgramme; //"Please select Programme";
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(txtGroups.Text))
        //        {
        //            InsertProgramme();
        //            clearFields();
        //        }
        //        else
        //            lblError.Text = Resources.DeffinityRes.PlsenterProgramme; //"Please enter Programme";
        //    }
        //    //getProgrammeDisplay(int _programme)
        //    //getProgrammeDisplay(int.Parse(sessionKeys.ProgrammeID.ToString()));
        //    //DropDownListCustomer.SelectedValue = sessionKeys.ProgrammeID.ToString();
        //    BindTreeview(Convert.ToInt32(DropDownListCustomer.SelectedValue), "Y");
        //}
        //catch (Exception ex)
        //{
        //    LogExceptions.LogException(ex.Message);
        //}
        Response.Redirect("ProgrammeManagement.aspx");
    }

    protected void DeleteProgramme()
    {

        
        int intID = 0;
        string strCheckProg = "";

        if (dropdownSubProgramme.SelectedValue != "Please select...")
        {

            if (dropdownSubProgramme.SelectedValue != "0")
            {


                intID = Convert.ToInt32(dropdownSubProgramme.SelectedValue);
                strCheckProg = "sub";
            }
            else
            {
                if (ddlGroups.SelectedValue != "Please select...")
                {

                    intID = Convert.ToInt32(ddlGroups.SelectedValue);
                    strCheckProg = "main";
                }
            }

        }
        else
        {
            if (ddlGroups.SelectedValue != "Please select...")
            {
                if (ddlGroups.SelectedValue != "0")
                {
                    intID = Convert.ToInt32(ddlGroups.SelectedValue);
                    strCheckProg = "main";
                }
            }
        }
        if (intID != 0)
        {

            DeleteProgramme(intID, strCheckProg);
        }
        else
        {
            lblError.Text = Resources.DeffinityRes.PlsselectProgramme; //"Please select Programme";
        }


    }
    //protected void btnSubmit_Click_OLD(object sender, ImageClickEventArgs e)
    //{
    //    lblMsg.Text = "";
    //    try
    //    {
    //        if (panleDdl.Visible)
    //        {
    //            //update email id
    //            if (ddlGroups.SelectedValue != "Please select...")
    //                UpdateProgramme(Convert.ToInt32(ddlGroups.SelectedValue));
    //            else
    //                lblError.Text = Resources.DeffinityRes.PlsselectProgramme; //"Please select Programme";
    //        }
    //        else
    //        {
    //            if (!string.IsNullOrEmpty(txtGroups.Text))
    //            {
    //                InsertProgramme();
    //                clearFields();
    //            }
    //            else
    //                lblError.Text = Resources.DeffinityRes.PlsenterProgramme; //"Please enter Programme";
    //        }
    //      //getProgrammeDisplay(int _programme)
    //        //getProgrammeDisplay(int.Parse(sessionKeys.ProgrammeID.ToString()));
    //        //DropDownListCustomer.SelectedValue = sessionKeys.ProgrammeID.ToString();
    //        BindTreeview(Convert.ToInt32(DropDownListCustomer.SelectedValue), "Y");
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.LogException(ex.Message);
    //    }
    //}

    
    private void DeleteProgramme(int id, string strCheckProg)
    {
        int outValue;
        Admin.DeleteProgramme(GetProgramme(id), out outValue);

        if (outValue == 1)
        {
            lblMsg.Text = "Programme deleted successfully"; //"Program updated successfully";
            lblMsg.ForeColor = System.Drawing.Color.Green;
        }

        clearFields();
        BindProgramme();
        BindProgrammeSub();
        BindTreeview(0, "Y");
       // UltraWebTree1.Nodes.Clear();
       
       // BindTreeview(0, "Y");
    }
    private void UpdateProgramme(int id, string strCheckProg)
    {
        int outValue;

        Admin.InsertUpdateProgramme(false, GetProgramme(id),out outValue);

        if (outValue == 1)
        {
            //BindProgramme();
            //BindProgrammeSub();
            //if (strCheckProg == "main")
            //{
            //    ddlGroups.SelectedValue = id.ToString();
            //    ddlPgmlevel2.SelectedValue = id.ToString();
            //}
            //else
            //{
            //    dropdownSubProgramme.SelectedValue = id.ToString();
            //}
            //sessionKeys.ProgrammeName = txtEditProgramName.Text.Trim();
            lblMsg.Text = Resources.DeffinityRes.ProgramUpdated; //"Program updated successfully";
            lblMsg.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblMsg.Text = Resources.DeffinityRes.ProgNamealreadyexists; //"Please check Program name already exists.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
        
    }
    private bool InsertProgramme()
    {

        bool blnRetValue = false;
        int outValue;

        if (rdbLevel2.Checked )
        {
            if (ddlPgmlevel2.SelectedValue == "0")
            {
                lblMsg.Text = "please select programme"; //Resources.DeffinityRes.ProgramCreated; //"Program created successfully";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                Admin.InsertUpdateProgramme(true, GetProgramme(0), out outValue);
                if (outValue == 1)
                {
                    lblMsg.Text = "Programme created successfully"; //Resources.DeffinityRes.ProgramCreated; //"Program created successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    //bind dropdown
                    BindProgramme();
                    BindProgrammeSub();
                    ddlVisibility();
                    blnRetValue = false;
                }
                else
                {
                    lblMsg.Text = Resources.DeffinityRes.ProgNamealreadyexists; //"Please check Program name already exists.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    blnRetValue = true;
                }
            }
        }
        if (rdbLevel1.Checked)
        {
            Admin.InsertUpdateProgramme(true, GetProgramme(0), out outValue);
            if (outValue == 1)
            {
                lblMsg.Text = "Programme created successfully"; //Resources.DeffinityRes.ProgramCreated; //"Program created successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                //bind dropdown
                BindProgramme();
                BindProgrammeSub();
                ddlVisibility();
                blnRetValue = false;
            }
            else
            {
                lblMsg.Text = Resources.DeffinityRes.ProgNamealreadyexists; //"Please check Program name already exists.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                blnRetValue = true;
            }
        }

       
        return blnRetValue;
        
    }
    private Programme GetProgramme(int id)
    {        
        Programme programme = new Programme();
        programme.ProgrammeID = id;
        if (id == 0)
            programme.OperationsOwners = txtGroups.Text.Trim();
        else
            programme.OperationsOwners = txtEditProgramName.Text.Trim();//ddlGroups.SelectedItem.Text;
        programme.EmailAddress = txtEmail.Text.Trim();
        programme.Visible = chkVisible.Checked ? "Y" : "N";
        programme.ProgrammOwnerID = Convert.ToInt32(ddlprogrammowner.SelectedValue);
        programme.DateofReview = DateTime.Now.Date;
        programme.TargetSLAPercentCompleted = 0;
        programme.ExpectedStartDate = DateTime.Now.Date;// Convert.ToDateTime(txtstartdate.Text.Trim());
        programme.ExpectedEndDate = DateTime.Now.Date;// Convert.ToDateTime(txtenddate.Text.Trim());
        programme.CostCenter = txtCostcenter.Text.Trim();
        programme.MaximumBudget = string.IsNullOrEmpty(txtMaxBudget.Text.Trim())? 0:Convert.ToDouble(txtMaxBudget.Text.Trim());
        programme.Justification = txtJustification.Text.Trim();
        programme.Description = txtDescription.Text.Trim();
        programme.BenefitsToOrganisation = txtBenefits.Text.Trim();
        programme.StrategicFitAlignment = txtStratergic.Text.Trim();
        programme.VisionStatement = txtvision.Text.Trim();
        programme.RisksAndIssues = txtRisk.Text.Trim();
        programme.ResourcesRequired = txtResources.Text.Trim();
        programme.Approve = false;
        programme.PortfolioID = 0; //int.Parse(DropDownListCustomer.SelectedValue); //int.Parse(ddlPortfolio.SelectedValue);
        if (rdbLevel1.Checked)
        {
            programme.MasterProgrammeID = 0;
            programme.LevelID = 1;
        }
        else
        {
            programme.MasterProgrammeID = Convert.ToInt32(ddlPgmlevel2.SelectedValue);
            programme.LevelID = 2;
        }
        return programme;
    
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
      

    }
    #region functions
    private void GetEMail(string ddlVal)
    {
        char v = 'Y';
        SqlConnection con = new SqlConnection(cs);

        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select EmailAddress,Visible,ProgrammOwnerID from OperationsOwners where ID = " + ddlVal, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                txtEmail.Text = dr["EmailAddress"].ToString();
                v = Convert.ToChar(dr["Visible"].ToString());
                ddlprogrammowner.SelectedValue = dr["ProgrammOwnerID"].ToString();
            }
            cmd.Dispose();
            dr.Close();
            if (v == 'Y')
            {
                chkVisible.Checked = true;
            }
            else
            {
                chkVisible.Checked = false;
            }
        }

        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);

        }
        finally
        {
            con.Close();
        }
    }
    #endregion
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtEditProgramName.Text = "";
        rdbLevel1.Enabled = false;
        rdbLevel2.Enabled = false;
        //display dropdown
        ddlVisibility();
        btnAddNew.Visible = true;
        btnDelete.Visible = true;
        btnCancel.Visible = false;

        ddlGroups.Visible = true;
        ddlPgmlevel2.Visible = false;
        panelEditProgramName.Visible = true;

        if (HiddenField2.Value != "")
        {
            ddlGroupFunction();
        }

        //clear fields
       // clearFields();
     
    }
    
   
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (QueryStringValues.Type == "projectplan")
        {
            Response.Redirect(string.Format("~/WF/ProjectPlan/ProjectPlan.aspx?projectplanid={0}", QueryStringValues.Project.ToString()));
        }
        else if (QueryStringValues.Type == "project")
        {
            if (QueryStringValues.Project > 0)
            {
                Response.Redirect(string.Format("~/WF/Projects/ProjectOverview.aspx?project={0}", QueryStringValues.Project));
            }
            else
            {
                Response.Redirect("~/WF/Projects/ProjectOverview.aspx?type=project");
            }
        }

        //project reference
        if (QueryStringValues.Project > 0)
        {
            Response.Redirect(string.Format("~/WF/Projects/ProjectOverview.aspx?project={0}", QueryStringValues.Project));
        }

    }

    protected void DDLGroupDefault()
    {
        try
        {
            //if (ddlGroups.SelectedValue != "0")
            //{
            //    sessionKeys.ProgrammeID = Convert.ToInt32(ddlGroups.SelectedValue);
            //    sessionKeys.ProgrammeName = ddlGroups.SelectedItem.Text;
            //    getProgrammeDisplay(sessionKeys.ProgrammeID);
            //    panelEditProgramName.Visible = true;
            //}
            //else
            //{
               // sessionKeys.ProgrammeID = 0;
                clearFields();
                //BindProgrammeLevel(-1);
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }



    protected void ddlPgmlevel2_SelectedIndexChanged(object sender, EventArgs e)
    {
        panelEditProgramName.Visible = false;
        ddlGroups.SelectedValue = ddlPgmlevel2.SelectedValue;
        rdbLevel1.Enabled = true;
        rdbLevel2.Enabled = true;
        int intProgrammeID = 0;
        try
        {
            if (ddlGroups.SelectedValue != "0")
            {
                intProgrammeID = Convert.ToInt32(ddlGroups.SelectedValue);
                //sessionKeys.ProgrammeName = ddlGroups.SelectedItem.Text;
                 getProgrammeDisplay(intProgrammeID);
                //panelEditProgramName.Visible = true;
                // BindTreeview(Convert.ToInt32(DropDownListCustomer.SelectedValue), "Y");
                FindAndSentTheDataKey(Convert.ToInt32(ddlGroups.SelectedValue));
            }
            else
            {
                // sessionKeys.ProgrammeID = 0;
                clearFields();
                //BindProgrammeLevel(-1);
                //DropDownListCustomer.SelectedValue = sessionKeys.ProgrammeID.ToString();
                //BindTreeview(Convert.ToInt32(DropDownListCustomer.SelectedValue), "Y");
                FindAndSentTheDataKey(Convert.ToInt32(dropdownSubProgramme.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        BindProgrammeSub();
    }
    protected void ddlGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPgmlevel2.SelectedValue = ddlGroups.SelectedValue;
        ddlGroupFunction();
    }

    protected void ddlGroupFunction()
    {
        int intProgrammeID = 0;
        try
        {
            if (ddlGroups.SelectedValue != "0")
            {
                intProgrammeID = Convert.ToInt32(ddlGroups.SelectedValue);
                //sessionKeys.ProgrammeName = ddlGroups.SelectedItem.Text;
                getProgrammeDisplay(intProgrammeID);
                panelEditProgramName.Visible = true;
                // BindTreeview(Convert.ToInt32(DropDownListCustomer.SelectedValue), "Y");
                FindAndSentTheDataKey(Convert.ToInt32(ddlGroups.SelectedValue));
            }
            else
            {
                // sessionKeys.ProgrammeID = 0;
                clearFields();
                //BindProgrammeLevel(-1);
                //DropDownListCustomer.SelectedValue = sessionKeys.ProgrammeID.ToString();
                //BindTreeview(Convert.ToInt32(DropDownListCustomer.SelectedValue), "Y");
                FindAndSentTheDataKey(Convert.ToInt32(dropdownSubProgramme.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        BindProgrammeSub();

        if (!btnCancel.Visible)
        {
            divLevel.Visible = false;
        }
    }
    protected void dropdownSubProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intProgrammeID = 0;
        try
        {
            if (dropdownSubProgramme.SelectedValue != "0")
            {
                intProgrammeID = Convert.ToInt32(dropdownSubProgramme.SelectedValue);
                //sessionKeys.ProgrammeName = dropdownSubProgramme.SelectedItem.Text;
                getProgrammeDisplay(intProgrammeID);
                panelEditProgramName.Visible = true;
                //BindTreeview(Convert.ToInt32(DropDownListCustomer.SelectedValue), "Y");
                FindAndSentTheDataKey(Convert.ToInt32(dropdownSubProgramme.SelectedValue));

            }
            else
            {
                intProgrammeID = Convert.ToInt32(ddlGroups.SelectedValue); ;
                clearFields();
                //BindProgrammeLevel(-1);
                getProgrammeDisplay(intProgrammeID);
                //DropDownListCustomer.SelectedValue = sessionKeys.ProgrammeID.ToString();
                // BindTreeview(Convert.ToInt32(DropDownListCustomer.SelectedValue), "Y");
                FindAndSentTheDataKey(Convert.ToInt32(dropdownSubProgramme.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        if (!btnCancel.Visible)
        {
            divLevel.Visible = false;
        }
    }
    private void getProgrammeDisplay(int _programme)
    {
        Programme programme = ad.SelectProgramme(_programme);
        getProgramme(programme);       
        
        //SqlDataSource1.DataBind();
        if (checkDatasource(SqlDataSource1))
        {
            Repeater1.Visible = false;
        }
        else
        {
            Repeater1.Visible = true;
        }

       // divLevel.Visible = true;
    }
    //getdata
    private void getProgramme(Programme programme)
    {
        txtEditProgramName.Text = programme.OperationsOwners;
        txtBenefits.Text = programme.BenefitsToOrganisation;
        txtCostcenter.Text = programme.CostCenter;
        txtDescription.Text = programme.Description;
        txtEmail.Text = programme.EmailAddress;
        //txtenddate.Text =  programme.ExpectedEndDate==DateTime.MinValue?"":programme.ExpectedEndDate.ToShortDateString();
        txtJustification.Text = programme.Justification;
        txtMaxBudget.Text = programme.MaximumBudget.ToString();
        txtResources.Text = programme.ResourcesRequired;
        txtRisk.Text = programme.RisksAndIssues;
        //txtstartdate.Text = programme.ExpectedStartDate ==DateTime.MinValue? "": programme.ExpectedStartDate.ToShortDateString();
        txtStratergic.Text = programme.StrategicFitAlignment;
        txtvision.Text = programme.VisionStatement;
        //ddlGroups.SelectedValue = programme.ProgrammeID.ToString();
        ddlprogrammowner.SelectedValue = programme.ProgrammOwnerID.ToString();        
        chkVisible.Checked= programme.Visible=="Y"? true:false;
        //ddlPortfolio.SelectedValue = programme.PortfolioID.ToString();
        DropDownListCustomer.SelectedValue = programme.PortfolioID.ToString();  //Setting PortfolioID for both Dropdowns(Treeview and main)
       // BindTreeview(Convert.ToInt32(DropDownListCustomer.SelectedValue),"Y");
        //programme.LevelID == 1 ? rdbLevel1.Checked =true:rdbLevel2.Checked= false;
        //BindProgrammeLevel(programme.ProgrammeID);

        if (btnAddNew.Visible)
        {
            if (programme.LevelID == 1)
            {
                rdbLevel2.Enabled = true;
                rdbLevel1.Enabled = true;
                rdbLevel2.Checked = false;
                rdbLevel1.Checked = true;
                divLevel.Visible = false;

                rdbLevel2.Enabled = false;
                rdbLevel1.Enabled = false;
                //changeLevels(1);
            }
            else
            {
                rdbLevel2.Checked = true;
                rdbLevel1.Checked = false;
                divLevel.Visible = false;
                //ddlPgmlevel2.SelectedValue = programme.MasterProgrammeID.ToString();
                //changeLevels(2);
            }

        }
        else
        {
           
                panelEditProgramName.Visible = false;
                divLevel.Visible = true;
           
            if (programme.LevelID == 1)
            {
                rdbLevel2.Enabled = true;
                rdbLevel1.Enabled = true;
                rdbLevel2.Checked = true;
                rdbLevel1.Checked = false;
                //divLevel.Visible = false;

                //rdbLevel2.Enabled = false;
                //rdbLevel1.Enabled = false;
                //changeLevels(1);
            }
            else
            {
                rdbLevel2.Checked = true;
                rdbLevel1.Checked = false;
                //divLevel.Visible = true;
                //ddlPgmlevel2.SelectedValue = programme.MasterProgrammeID.ToString();
                //changeLevels(2);
            }
        }

    }
    private void GetEMail1(string ddlVal)
    {
        char v = 'Y';
        SqlConnection con = new SqlConnection(cs);

        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select ID,OperationsOwners,EmailAddress,Visible from OperationsOwners where ID = " + Convert.ToInt32(ddlVal), con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ddlGroups.Visible = false;
            txtGroups.Visible = true;
            while (dr.Read())
            {
                HiddenField1.Value = dr["ID"].ToString();
                txtGroups.Text = dr["OperationsOwners"].ToString();
                txtEmail.Text = dr["EmailAddress"].ToString();
                v = Convert.ToChar(dr["Visible"].ToString());
            }
            cmd.Dispose();
            dr.Close();
            if (v == 'Y')
            {
                chkVisible.Checked = true;
            }
            else
            {
                chkVisible.Checked = false;
            }
        }

        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);

        }
        finally
        {
            con.Close();
        }
    }

    protected void btnAddNew_Click1(object sender, EventArgs e)
    {
        //clear fields
         clearFields();
        //dispaly text box
        txtVisibility();
        //hide the edit panel programe name 
        panelEditProgramName.Visible = false;
        rdbLevel1.Enabled = true;
        rdbLevel2.Enabled = true;


        rdbLevel2.Checked = true;
        rdbLevel1.Checked = false;
        panelEditProgramName.Visible = false;
        ddlGroups.Visible = false;
        ddlPgmlevel2.Visible = true;
        ddlprogrammowner.SelectedIndex = 0;
        txtEmail.Text = "";
        txtJustification.Text = "";
        txtResources.Text = "";
        txtRisk.Text = "";
        txtStratergic.Text = "";
        txtMaxBudget.Text = "";
        txtDescription.Text = "";
        txtGroups.Text = "";
        

        //if (rdbLevel1.Checked)
        //{
        //    rdbLevel2.Checked = true;
        //    rdbLevel1.Checked = false;
        //    panelEditProgramName.Visible = false;
        //    ddlGroups.Visible = false;
        //    ddlPgmlevel2.Visible = true;
        //}
        //else
        //{
        //    panelEditProgramName.Visible = false;
        //    ddlGroups.Visible = false;
        //    ddlPgmlevel2.Visible = true;
        //    rdbLevel2.Checked = true;
        //    rdbLevel1.Checked = false;
        //}


    }

    protected void btnDelete_Click1(object sender, EventArgs e)
    {

        DeleteProgramme();
        HiddenField2.Value = "";
        HiddenField3.Value = "";
        Response.Redirect("~/WF/Admin/ProgrammeManagement.aspx");
        //clear fields
       // clearFields();
        //dispaly text box
        //txtVisibility();
        ////hide the edit panel programe name 
        //panelEditProgramName.Visible = false;
        //rdbLevel1.Enabled = true;
        //rdbLevel2.Enabled = true;
        
    }    
    protected void btnClear_Click(object sender, EventArgs e)
    {       
        clearFields();
    }
    #region clear fileds
    //clear Cancel text from fields
    private void clearFields_CancelSubmit()
    {
        try
        {
            txtBenefits.Text = string.Empty;
            txtCostcenter.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtEmail.Text = string.Empty;
            //txtenddate.Text = string.Empty;
            txtGroups.Text = string.Empty;
            txtJustification.Text = string.Empty;
            txtMaxBudget.Text = string.Empty;
            txtResources.Text = string.Empty;
            txtRisk.Text = string.Empty;
            //txtstartdate.Text = string.Empty;
            txtStratergic.Text = string.Empty;
            txtvision.Text = string.Empty;
            ddlGroups.SelectedIndex = 0;
            ddlPgmlevel2.SelectedIndex = 0;
            ddlprogrammowner.SelectedIndex = 0;
            dropdownSubProgramme.SelectedIndex = 0;
            //ddlPortfolio.SelectedIndex = 0;
            //DropDownListCustomer.SelectedIndex = 0;

            //if (HiddenField2.Value != "")
            //{
            //    if (ddlPgmlevel2.Items.Count>0)
            //    {
            //ddlPgmlevel2.SelectedValue =HiddenField2.Value;
            //}
            //}
            //else
            //{
            //    ddlPgmlevel2.SelectedIndex = 0;
            //}
            //ddlPgmlevel2.SelectedIndex = 0; //Dine

            lblError.Text = string.Empty;
            //if (sessionKeys.ProgrammeID == 0)
            //{
            //    rdbLevel1.Checked = false;
            //    rdbLevel2.Checked = true;
            //}
            //else
            //{
            //    rdbLevel1.Checked = true;
            //    rdbLevel2.Checked = false;
            //}
            txtEditProgramName.Text = string.Empty;
            btnAddNew.Visible = true;
            btnDelete.Visible = true;
            btnCancel.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    //clear text from fields
    private void clearFields()
    {
        try
        {
            txtBenefits.Text = string.Empty;
            txtCostcenter.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtEmail.Text = string.Empty;
            //txtenddate.Text = string.Empty;
            txtGroups.Text = string.Empty;
            txtJustification.Text = string.Empty;
            txtMaxBudget.Text = string.Empty;
            txtResources.Text = string.Empty;
            txtRisk.Text = string.Empty;
            //txtstartdate.Text = string.Empty;
            txtStratergic.Text = string.Empty;
            txtvision.Text = string.Empty;
            ddlGroups.SelectedIndex = 0;
            ddlPgmlevel2.SelectedIndex = 0;
            ddlprogrammowner.SelectedIndex = 0;
            dropdownSubProgramme.SelectedIndex = 0;
            //ddlPortfolio.SelectedIndex = 0;
            //DropDownListCustomer.SelectedIndex = 0;

            //if (HiddenField2.Value != "")
            //{
            //    if (ddlPgmlevel2.Items.Count>0)
            //    {
            //ddlPgmlevel2.SelectedValue =HiddenField2.Value;
            //}
            //}
            //else
            //{
            //    ddlPgmlevel2.SelectedIndex = 0;
            //}
            //ddlPgmlevel2.SelectedIndex = 0; //Dine

            lblError.Text = string.Empty;
            //if (sessionKeys.ProgrammeID == 0)
            //{
            //    rdbLevel1.Checked = false;
            //    rdbLevel2.Checked = true;
            //}
            //else
            //{
            //    rdbLevel1.Checked = true;
            //    rdbLevel2.Checked = false;
            //}
            txtEditProgramName.Text = string.Empty;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //ddl visibility
    private void ddlVisibility()
    {
        panleDdl.Visible = true;
        trProgrammesub.Visible = true;
        //panelText.Visible = false;
        txtGroups.Visible = false;
        ddlGroups.Visible = true;
        rdbLevel1.Enabled = false;
        rdbLevel2.Enabled = false;
        divLevel.Visible = false;
        panelEditProgramName.Visible = true;
        divLevel.Visible = false;
    }
    private void txtVisibility()
    {
        btnAddNew.Visible = false;
        btnDelete.Visible = false;
        btnCancel.Visible = true;
        //panelText.Visible = true;
        txtGroups.Visible = true;
        ddlGroups.Visible = false;
        //panleDdl.Visible = false;
        trProgrammesub.Visible = false;
        rdbLevel1.Enabled = true;
        //rdbLevel2.Checked = true;
        rdbLevel2.Enabled = true;
        divLevel.Visible = true;
        //BindProgrammeLevelOne();
    
    }
    #endregion

    protected void rdbLevel2_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbLevel2.Checked)
        {
            //BindProgrammeLevelOne();
            divLevel.Visible = true;
            //tdProgramme.Visible = true;
            //panelProgramme.Visible = false;
            //panelProgrammeName.Visible = true;
            //ddlGroups.Visible = true;
            ddlprogrammowner.SelectedIndex = 0;
            txtEmail.Text = "";
        }
    }
    protected void rdbLevel1_CheckedChanged(object sender, EventArgs e)
    {

        if (rdbLevel1.Checked)
        {
            //BindProgrammeLevel(Convert.ToInt32(sessionKeys.ProgrammeID));
            //BindProgrammeLevelOne();
            divLevel.Visible = false;

            //panelProgramme.Visible = true;
            //panelProgrammeName.Visible = false;
            ddlGroups.Visible = false;
            ddlprogrammowner.SelectedIndex = 0;
            txtEmail.Text = "";
            
        }
        
    }

    protected void ddlprogrammowner_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtEmail.Text = _ddlBind.exeScalar("Select EmailAddress from Contractors where ID=" + ddlprogrammowner.SelectedValue, false);
    }
    private bool checkDatasource(SqlDataSource _sqlDatasource)
    {
        DataView dv;

        dv = (DataView)_sqlDatasource.Select(DataSourceSelectArguments.Empty);
        if (dv.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void UltraWebTree1_NodeSelectionChanged(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
    {
        // txtDescription.Text = "UltraWebTree1_NodeSelectionChanged";
        int datakey = Convert.ToInt32(UltraWebTree1.SelectedNode.DataKey);
        try
        {

            if (UltraWebTree1.SelectedNode.Parent.Text.Trim() == "Programme")
            {
                HiddenField2.Value = UltraWebTree1.SelectedNode.DataKey.ToString();
                HiddenField3.Value = "0";
            }
            else
            {
                HiddenField2.Value = UltraWebTree1.SelectedNode.Parent.DataKey.ToString();
                HiddenField3.Value = UltraWebTree1.SelectedNode.DataKey.ToString();

            }
           
            
            ddlGroups.SelectedValue = HiddenField2.Value;
            BindProgrammeSub();
            ddlPgmlevel2.SelectedValue = HiddenField2.Value;
            dropdownSubProgramme.SelectedValue = HiddenField3.Value;
            getProgrammeDisplay(datakey);
        }
        catch (Exception ex)
        {

        }
        //HiddenField3.Value = UltraWebTree1.SelectedNode.DataKey.ToString();

    }
}
