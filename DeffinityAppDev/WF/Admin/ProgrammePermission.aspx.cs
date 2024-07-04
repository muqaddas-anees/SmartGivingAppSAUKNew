using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using UserMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System.Data;
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
public partial class ProgrammePermission : System.Web.UI.Page
{
    private string cs = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    private string er = "";
    DisBindings _ddlBind = new DisBindings();
    Admin ad = new Admin();
    
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);

    //LinQ Here...
    PortfolioDataContext portfolio = new PortfolioDataContext();
    UserDataContext userDB = new UserDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindCustomers();
                BindTreeview(0, "N");
                BindTeams(0, ddlCLTeam);
                //BindTeamMembers(Convert.ToInt32(ddlCLTeam.SelectedValue), ddlCLUserName);
                BindGridCustomerLevel(grdCustomeLevel, 1);
                BindProgramme();
                BindTeams(0, ddlProgrammeLevelTeam);
               // BindTeamMembers(int.Parse(ddlProgrammeLevelTeam.SelectedValue), ddlProgrammeLevelUsers);
                BindGridProgrammeLevel(grdProgrammeLeve, 2);
                BindSubProgramme();
                BindTeams(0, ddlSPLTeam);
               // BindTeamMembers(int.Parse(ddlSPLTeam.SelectedValue), ddlSPLUserName);
                BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);

                //Master.PageHead = "Permission Manager";// Resources.DeffinityRes.Program; //"Program";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //protected void UltraWebTree1_NodeClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
    //{
    //    try
    //    {
    //        int datakey = Convert.ToInt32(UltraWebTree1.SelectedNode.DataKey);
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    //protected void UltraWebTree1_NodeSelectionChanged(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
    //{
    //    // txtDescription.Text = "UltraWebTree1_NodeSelectionChanged";
    //    try
    //    {
    //        if (UltraWebTree1.SelectedNode.Parent.Text.Trim() == "Programme")
    //        {
    //            HiddenField2.Value = UltraWebTree1.SelectedNode.DataKey.ToString();
    //            HiddenField3.Value = "0";
    //            BindGridCustomerLevel(grdCustomeLevel, 1);
    //            BindGridCustomerLevel(grdProgrammeLeve, 2);
    //            BindGridCustomerLevel(grdSubProgrammeLevel, 3);

    //        }
    //        else
    //        {
    //            HiddenField2.Value = UltraWebTree1.SelectedNode.Parent.DataKey.ToString();
    //            HiddenField3.Value = UltraWebTree1.SelectedNode.DataKey.ToString();
    //            BindGridCustomerLevel(grdCustomeLevel, 1);
    //            BindGridCustomerLevel(grdProgrammeLeve, 2);
    //            BindGridCustomerLevel(grdSubProgrammeLevel, 3);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }

    //}
    private void BindGridCustomerLevel(GridView Grid,int Level)
    {
        try
        {
            int ProgrammeID;
           
            var getCustomerLevel = (from r in portfolio.PermissionLevels
                                    join t in portfolio.Teams on r.TeamID equals t.ID
                                    where r.LevelType == Level && r.PortfolioID == int.Parse(DropDownListCustomer.SelectedValue)
                                

                                    select new { t.TeamName,r.TeamID, t.ID, r.UserID, r.RoleID, r.LevelType, PID = r.ID }).ToList();
            var getContractors = (from r in userDB.Contractors
                                  select r).ToList();
            if (getCustomerLevel != null)
            {
                var getContractors1 = (from r in getCustomerLevel
                                      // join c in getCustomerLevel on r.ID equals c.UserID
                                       select new { r.UserID, r.TeamName, r.RoleID, r.PID, r.ID,r.TeamID }).ToList();

                Grid.DataSource = getContractors1;
                Grid.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    private void BindGridProgrammeLevel(GridView Grid, int Level)
    {
        try
        {
            int ProgrammeID;

            var getCustomerLevel = (from r in portfolio.PermissionLevels
                                    join t in portfolio.Teams on r.TeamID equals t.ID
                                    where r.LevelType == Level && r.ProgrammeID == int.Parse(ddlProgramme.SelectedValue)


                                    select new { t.TeamName,r.TeamID, t.ID, r.UserID,r.ProgrammeID, r.RoleID, r.LevelType, PID = r.ID }).ToList();
            var getContractors = (from r in userDB.Contractors
                                  select r).ToList();
            if (getCustomerLevel != null)
            {
                var getContractors1 = (from r in getCustomerLevel
                                       // join c in getCustomerLevel on r.ID equals c.UserID
                                       select new { r.UserID,r.TeamID, r.TeamName, r.RoleID, r.PID, r.ID,r.ProgrammeID }).ToList();

                Grid.DataSource = getContractors1;
                Grid.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void BindGridSubProgrammeLevel(GridView Grid, int Level)
    {
        try
        {
            int ProgrammeID;

            var getCustomerLevel = (from r in portfolio.PermissionLevels
                                    join t in portfolio.Teams on r.TeamID equals t.ID
                                    where r.LevelType == Level && r.ProgrammeID==int.Parse(ddlSubProgramme.SelectedValue)


                                    select new { t.TeamName, t.ID,r.TeamID, r.UserID, r.RoleID, r.LevelType, PID = r.ID }).ToList();
            var getContractors = (from r in userDB.Contractors
                                  select r).ToList();
            if (getCustomerLevel != null)
            {
                var getContractors1 = (from r in getCustomerLevel
                                       // join c in getCustomerLevel on r.ID equals c.UserID
                                       
                                       select new { r.UserID,r.TeamID, r.TeamName, r.RoleID, r.PID, r.ID }).ToList();

                Grid.DataSource = getContractors1;
                Grid.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void BindTreeview(int intPortfolioID, string strCheck)
    {
        try
        {
            //UltraWebTree1.Nodes.Clear();
            Infragistics.WebUI.UltraWebNavigator.Node rootNode = new Infragistics.WebUI.UltraWebNavigator.Node();
            rootNode.DataKey = 0;
            rootNode.Text = "Programme"; //Resources.DeffinityRes.FilesFolders;// "Files/Folders";
            rootNode.Tag = "Programme";//Resources.DeffinityRes.FilesFolders;// "Files/Folders";
           // UltraWebTree1.Nodes.Add(rootNode);
            if (intPortfolioID > 0)
            {
                NewTreeBinding(intPortfolioID);


            }
           // UltraWebTree1.ExpandAll();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void BindProgramme()
    {
        ProgrammeDataContext programme = new ProgrammeDataContext();
        var programmes = (from r in programme.OperationsOwners
                          where r.Level == 1 orderby r.OperationsOwners
                          select new { r.ID, r.OperationsOwners }).ToList();
        ddlProgramme.DataSource = programmes;
        ddlProgramme.DataTextField = "OperationsOwners";
        ddlProgramme.DataValueField = "ID";
        ddlProgramme.DataBind();
        ddlProgramme.Items.Insert(0, new ListItem("Please select...", "0"));
        if (sessionKeys.ProgrammeID != 0 && ddlProgramme.Items.Count > 0)
        {
            ddlProgramme.SelectedValue = sessionKeys.ProgrammeID.ToString();
        }
        else
        {
            if (ddlProgramme.Items.Count > 1)
            {
                ddlProgramme.SelectedIndex = 1;
            }
        }
       // ddlProgramme.Items.Insert(0, new ListItem("Please select...", "0"));

    }

    private void BindSubProgramme()
    {
        ProgrammeDataContext programme = new ProgrammeDataContext();
        var subProgramme=(from r in programme.OperationsOwners
                          where r.Level==2 && r.MasterProgramme==int.Parse(ddlProgramme.SelectedValue)
                          orderby r.OperationsOwners
                          
                          select new { r.ID, r.OperationsOwners }).ToList();
        ddlSubProgramme.DataSource = subProgramme;
        ddlSubProgramme.DataTextField = "OperationsOwners";
        ddlSubProgramme.DataValueField = "ID";
        ddlSubProgramme.DataBind();
        ddlSubProgramme.Items.Insert(0, new ListItem("Please select...", "0"));
        if (ddlSubProgramme.Items.Count > 1)
        {
            ddlSubProgramme.SelectedIndex = 1;
        }

    }
    private void AddChild(Infragistics.WebUI.UltraWebNavigator.Nodes nodes, Infragistics.WebUI.UltraWebNavigator.Node addNode, int parentID)
    {
        try
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
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void NewTreeBinding(int intPortfolioID)
    {
        try
        {
          
        DataSet ds11 = new DataSet();
        SqlCommand cmdSQL1 = new SqlCommand();
        cmdSQL1.CommandType = CommandType.StoredProcedure;
        cmdSQL1.Parameters.Clear();
        cmdSQL1.CommandText = "DEFFINITY_GETProgrammeLevels_Portfolio";
        cmdSQL1.Parameters.AddWithValue("@PortfolioID", intPortfolioID);
        //}
        cmdSQL1.Connection = cn;
        SqlDataAdapter da11 = new SqlDataAdapter(cmdSQL1);
        da11.Fill(ds11, "OPERATIONSOWNERS");
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
                //if (parentID == 0) UltraWebTree1.Nodes[0].Nodes.Add(node);
                //else AddChild(UltraWebTree1.Nodes[0].Nodes, node, parentID);

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindCustomers()
    {
        PortfolioDataContext timeSheet = new PortfolioDataContext();


        try
        {
            var portfolio = from r in timeSheet.ProjectPortfolios
                            where r.Visible == true
                            orderby r.PortFolio
                            select r;
            DropDownListCustomer.DataSource = portfolio;
            DropDownListCustomer.DataTextField = "PortFolio";
            DropDownListCustomer.DataValueField = "ID";
            DropDownListCustomer.DataBind();
            DropDownListCustomer.Items.Insert(0, new ListItem("Please select...", "0"));
            if (DropDownListCustomer.Items.Count > 1)
            {
                DropDownListCustomer.SelectedIndex = 1;
            }
            if (sessionKeys.PortfolioID != 0)
            {
                DropDownListCustomer.SelectedValue = sessionKeys.PortfolioID.ToString();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //protected void DropDownListCustomer_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        BindTreeview(Convert.ToInt32(DropDownListCustomer.SelectedValue), "N");
    //        BindTeams(Convert.ToInt32(DropDownListCustomer.SelectedValue), ddlCLTeam);
    //        BindTeamMembers(Convert.ToInt32(ddlCLTeam.SelectedValue), ddlCLUserName);
    //        BindGridCustomerLevel(grdCustomeLevel, 1);

    //        BindTeams(Convert.ToInt32(DropDownListCustomer.SelectedValue), ddlProgrammeLevelTeam);
    //        BindTeamMembers(Convert.ToInt32(ddlProgrammeLevelTeam.SelectedValue), ddlProgrammeLevelUsers);
    //        BindGridCustomerLevel(grdProgrammeLeve, 2);



    //        BindTeams(Convert.ToInt32(DropDownListCustomer.SelectedValue), ddlSPLTeam);
    //        BindTeamMembers(Convert.ToInt32(ddlSPLTeam.SelectedValue), ddlSPLUserName);
    //        BindGridCustomerLevel(grdSubProgrammeLevel, 3);
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    private void BindTeams(int customerID,DropDownList dropDownTeam)
    {
        try
        {
           
            var getTeams = (from r in portfolio.Teams
                          orderby r.TeamName
                            select r).ToList();
            dropDownTeam.DataSource = getTeams;
            dropDownTeam.DataValueField = "ID";
            dropDownTeam.DataTextField = "TeamName";
            dropDownTeam.DataBind();
            dropDownTeam.Items.Insert(0, new ListItem("Please select...", "0"));

        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    private void BindTeamMembers(int TeamID,DropDownList dropdownUsers)
    {
        try
        {

            var getTeams = (from r in portfolio.TeamMembers
                            where r.TeamID == TeamID
                            select r).ToList();
            var getContractors = (from r in userDB.Contractors
                                  select r).ToList();
            var getUsers = (from r in getTeams
                            join c in getContractors on r.Name equals c.ID
                            select new { c.ID, c.ContractorName }).ToList();
            dropdownUsers.DataSource = getUsers;
            dropdownUsers.DataValueField = "ID";
            dropdownUsers.DataTextField = "ContractorName";
            dropdownUsers.DataBind();
            dropdownUsers.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    protected string RoleType(string type)
    {
        string val = "";
        try
        {
           
            if (type == "1")
            {
                val = "Disabled";
            }
            if (type == "2")
            {
                val = "Manager";
            }
            if (type == "3")
            {
                val = "Viewer";
            }
            if (type == "4")
            {
                val = "Administrator";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return val;
    }
    protected void ddlCLTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //BindTeamMembers(Convert.ToInt32(ddlCLTeam.SelectedValue), ddlCLUserName);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgApplyCustomerLevel_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsgPL.Visible = false;
            lblSPL.Visible = false;
            PermissionLevel insert = new PermissionLevel();
            int ProgrammeID;
            //if (int.Parse(HiddenField3.Value) != 0)
            //{
            //    ProgrammeID = int.Parse(HiddenField3.Value);
            //}
            //else
            //{
            //    ProgrammeID = int.Parse(HiddenField2.Value);
            //}
            var getExistUser = (from r in portfolio.PermissionLevels
                                where r.LevelType == 1 && r.PortfolioID==int.Parse(DropDownListCustomer.SelectedValue)
                                && r.TeamID == int.Parse(ddlCLTeam.SelectedValue)
                                select r).ToList();
            if (getExistUser != null)
            {
                if (getExistUser.Count == 0)
                {
                    lblCLMsg.Visible = true;
                    insert.LevelType = 1;
                    insert.PortfolioID =int.Parse(DropDownListCustomer.SelectedValue);
                    //insert.ProgrammeID = ProgrammeID;
                    insert.RoleID = int.Parse(ddlCLRole.SelectedValue);
                    insert.TeamID = int.Parse(ddlCLTeam.SelectedValue);
                    insert.UserID = 0;
                    portfolio.PermissionLevels.InsertOnSubmit(insert);
                    portfolio.SubmitChanges();
                    lblCLMsg.Visible = true;
                    lblCLMsg.Text = "Added Successfully";
                    lblCLMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {

                    lblCLMsg.Visible = true;
                    lblCLMsg.Text = "Selected group already exists";
                    lblCLMsg.ForeColor = System.Drawing.Color.Red;

                }
            }

            //ddlCLRole.SelectedIndex = -1;
            //ddlCLTeam.SelectedIndex = -1;
            //ddlCLUserName.SelectedIndex = -1;
            grdCustomeLevel.EditIndex = -1;

            BindGridCustomerLevel(grdCustomeLevel, 1);

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdCustomeLevel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                lblMsgPL.Visible = false;
                lblSPL.Visible = false;
                lblCLMsg.Visible = false;
                PermissionLevel row =
                portfolio.PermissionLevels.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                portfolio.PermissionLevels.DeleteOnSubmit(row);
                portfolio.SubmitChanges();
                BindGridCustomerLevel(grdCustomeLevel, 1);

            }
            if (e.CommandName == "Update")
            {

                int index = grdCustomeLevel.EditIndex;
                GridViewRow row = grdCustomeLevel.Rows[index];

                Label lblContractorID = (Label)row.FindControl("lblContractorID");
                DropDownList ddlCLTeamEdit = (DropDownList)row.FindControl("ddlCLTeamEdit");
                //DropDownList ddlCLUserName = (DropDownList)row.FindControl("ddlCLUserName");
                DropDownList ddlCLPermissions = (DropDownList)row.FindControl("ddlCLPermissions");

                PermissionLevel insert =
              portfolio.PermissionLevels.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                int ProgrammeID;
                //if (int.Parse(HiddenField3.Value) != 0)
                //{
                //    ProgrammeID = int.Parse(HiddenField3.Value);
                //}
                //else
                //{
                //    ProgrammeID = int.Parse(HiddenField2.Value);
                //}
                var getExistUser = (from r in portfolio.PermissionLevels
                                    where r.LevelType == 1 && r.PortfolioID == int.Parse(DropDownListCustomer.SelectedValue)
                                    && r.TeamID == int.Parse(ddlCLTeamEdit.SelectedValue)
                                    select r).ToList();
                if (getExistUser != null)
                {
                    if (getExistUser.Count != 0)
                    {
                        lblCLMsg.Visible = true;
                        // insert.LevelType = 1;
                        //insert.PortfolioID = int.Parse(DropDownListCustomer.SelectedValue);
                        //insert.ProgrammeID = ProgrammeID;
                        insert.RoleID = int.Parse(ddlCLPermissions.SelectedValue);
                        insert.TeamID = int.Parse(ddlCLTeamEdit.SelectedValue);
                        insert.UserID = 0;//int.Parse(ddlCLUserName.SelectedValue);
                        //portfolio.PermissionLevels.InsertOnSubmit(insert);
                        portfolio.SubmitChanges();
                        lblCLMsg.Visible = true;
                        lblCLMsg.Text = "Updated Successfully";
                        lblCLMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {

                        lblCLMsg.Visible = true;
                        lblCLMsg.Text = "Selected group already exists";
                        lblCLMsg.ForeColor = System.Drawing.Color.Red;

                    }
                }
            }
            BindGridCustomerLevel(grdCustomeLevel, 1);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdCustomeLevel_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblCLMsg.Visible = false;
            grdCustomeLevel.EditIndex = -1;
            BindGridCustomerLevel(grdCustomeLevel, 1);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    protected void ddlCLRole_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
   private void BindUserName_Grid(DropDownList ddlUsers,int setValue,int TeamID)
   {
       try
       {
           var getTeams = (from r in portfolio.TeamMembers
                           where r.TeamID == TeamID
                           select r).ToList();
           var getContractors = (from r in userDB.Contractors
                                 select r).ToList();
           var getUsers = (from r in getTeams
                           join c in getContractors on r.Name equals c.ID
                           select new { c.ID, c.ContractorName }).ToList();
           ddlUsers.DataSource = getUsers;
           ddlUsers.DataValueField = "ID";
           ddlUsers.DataTextField = "ContractorName";
           ddlUsers.DataBind();
           ddlUsers.Items.Insert(0, new ListItem("Please select...", "0"));
           ddlUsers.SelectedValue = setValue.ToString();
       }
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
       
    }
   private void BindTeam_Grid(DropDownList ddlTeams, int setValue, int CustomerID)
   {
       try
       {
           //Bind Team
           var getTeams = (from r in portfolio.Teams
                          // where r.PortfolioID == CustomerID
                           select r).ToList();
           ddlTeams.DataSource = getTeams;
           ddlTeams.DataValueField = "ID";
           ddlTeams.DataTextField = "TeamName";
           ddlTeams.DataBind();
           ddlTeams.Items.Insert(0, new ListItem("Please select...", "0"));
           ddlTeams.SelectedValue = setValue.ToString();
       }
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
     
   }
    protected void grdCustomeLevel_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grdCustomeLevel.EditIndex = -1;
            BindGridCustomerLevel(grdCustomeLevel, 1);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }

    protected void grdCustomeLevel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.FindControl("ddlCLTeamEdit") != null)
                {
                    Label lblRoleID = (Label)e.Row.FindControl("lblRoleID");
                    Label lblContractorID = (Label)e.Row.FindControl("lblContractorID");
                    Label lblTeamID = (Label)e.Row.FindControl("lblTeamID");
                    DropDownList ddlCLPermissions = (DropDownList)e.Row.FindControl("ddlCLPermissions");
                    //DropDownList ddlCLUserName = (DropDownList)e.Row.FindControl("ddlCLUserName");
                    DropDownList ddlCLTeam = (DropDownList)e.Row.FindControl("ddlCLTeamEdit");
                    BindTeam_Grid(ddlCLTeam, int.Parse(lblTeamID.Text), 0);
                    //BindUserName_Grid(ddlCLUserName, int.Parse(lblContractorID.Text), int.Parse(ddlCLTeam.SelectedValue));
                    ddlCLPermissions.SelectedValue = lblRoleID.Text;
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlCLTeamEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int index = grdCustomeLevel.EditIndex;
            GridViewRow row = grdCustomeLevel.Rows[index];
            Label lblContractorID = (Label)row.FindControl("lblContractorID");
            DropDownList ddlCLTeamEdit = (DropDownList)row.FindControl("ddlCLTeamEdit");
            //DropDownList ddlCLUserName = (DropDownList)row.FindControl("ddlCLUserName");
           // BindUserName_Grid(ddlCLUserName, 0, int.Parse(ddlCLTeamEdit.SelectedValue));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdCustomeLevel_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdCustomeLevel.EditIndex = e.NewEditIndex;
            BindGridCustomerLevel(grdCustomeLevel, 1);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    protected void grdCustomeLevel_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            grdCustomeLevel.EditIndex = -1;
            BindGridCustomerLevel(grdCustomeLevel, 1);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    //Programme Process goes Here..........................

    protected void ImgApplyPL_Click(object sender, EventArgs e)
    {
        try
        {
            lblCLMsg.Visible = false;
            lblSPL.Visible = false;
            PermissionLevel insert = new PermissionLevel();
            int ProgrammeID;
            
            var getExistUser = (from r in portfolio.PermissionLevels
                                where r.LevelType == 2 && r.ProgrammeID ==int.Parse(ddlProgramme.SelectedValue)
                                && r.TeamID == int.Parse(ddlProgrammeLevelTeam.SelectedValue)
                                select r).ToList();
            if (getExistUser != null)
            {
                if (getExistUser.Count == 0)
                {
                    lblCLMsg.Visible = true;
                    insert.LevelType = 2;
                    insert.PortfolioID = 0;// int.Parse(DropDownListCustomer.SelectedValue);
                    insert.ProgrammeID = int.Parse(ddlProgramme.SelectedValue);
                    insert.RoleID = int.Parse(ddlRolePL.SelectedValue);
                    insert.TeamID = int.Parse(ddlProgrammeLevelTeam.SelectedValue);
                    insert.UserID = 0;// int.Parse(ddlProgrammeLevelUsers.SelectedValue);
                    portfolio.PermissionLevels.InsertOnSubmit(insert);
                    portfolio.SubmitChanges();
                    lblMsgPL.Visible = true;
                    lblMsgPL.Text = "Added Successfully";
                    lblMsgPL.ForeColor = System.Drawing.Color.Green;
                    InsertSubProgrammes(int.Parse(ddlProgrammeLevelTeam.SelectedValue),int.Parse(ddlRolePL.SelectedValue));
                }
                else
                {

                    lblMsgPL.Visible = true;
                    lblMsgPL.Text = "Selected group already exists";
                    lblMsgPL.ForeColor = System.Drawing.Color.Red;

                }
            }

            //ddlCLRole.SelectedIndex = -1;
            //ddlCLTeam.SelectedIndex = -1;
            //ddlCLUserName.SelectedIndex = -1;
            grdProgrammeLeve.EditIndex = -1;

            BindGridProgrammeLevel(grdProgrammeLeve, 2);
            //BindGridCustomerLevel(grdProgrammeLeve, 2);
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void InsertSubProgrammes(int TeamID,int RoleID)
    {
        try
        {
            //FETCH  SUBPROGRAMMES ID
            ProgrammeDataContext programme = new ProgrammeDataContext();
            var getSubProgramme = (from r in programme.OperationsOwners
                                   where r.MasterProgramme == Convert.ToInt32(ddlProgramme.SelectedValue)
                                   && r.Level == 2
                                   select r).ToList();
          
            if (getSubProgramme != null)
            {
                foreach (OperationsOwner ProgrammeID in getSubProgramme)
                {
                    PermissionLevel insert = new PermissionLevel();
                    var getExistUser = (from r in portfolio.PermissionLevels
                                        where r.LevelType == 3 && r.ProgrammeID == ProgrammeID.ID
                                        && r.TeamID == TeamID
                                        select r).ToList();
                    if (getExistUser != null)
                    {
                        if (getExistUser.Count == 0)
                        {
                            lblCLMsg.Visible = true;
                            insert.LevelType = 3;
                            insert.PortfolioID = 0;// int.Parse(DropDownListCustomer.SelectedValue);
                            insert.ProgrammeID = ProgrammeID.ID;// int.Parse(ddlSubProgramme.SelectedValue);
                            insert.RoleID = RoleID;//int.Parse(ddlSPLRole.SelectedValue);
                            insert.TeamID = TeamID;//int.Parse(ddlSPLTeam.SelectedValue);
                            insert.UserID = 0;// int.Parse(ddlSPLUserName.SelectedValue);
                            portfolio.PermissionLevels.InsertOnSubmit(insert);
                            portfolio.SubmitChanges();
                            lblSPL.Visible = true;
                            lblSPL.Text = "Added Successfully";
                            lblSPL.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {

                            lblSPL.Visible = true;
                           // lblSPL.Text = "Selected group already exists";
                            lblSPL.ForeColor = System.Drawing.Color.Red;

                        }
                    }


                }
                BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void DeleteSubProgramme(int TeamID)
    {
        try
        {
            //FETCH  SUBPROGRAMMES ID
            ProgrammeDataContext programme = new ProgrammeDataContext();
            var getSubProgramme = (from r in programme.OperationsOwners
                                   where r.MasterProgramme == Convert.ToInt32(ddlProgramme.SelectedValue)
                                   && r.Level == 2
                                   select r).ToList();

            if (getSubProgramme != null)
            {
                foreach (OperationsOwner ProgrammeID in getSubProgramme)
                {
                    var getExistUser = (from r in portfolio.PermissionLevels
                                        where r.LevelType == 3 && r.ProgrammeID == ProgrammeID.ID
                                        && r.TeamID == TeamID
                                        select r).ToList();


                    if (getExistUser != null)
                    {
                        if (getExistUser.Count != 0)
                        {

                            var getExistID = (from r in portfolio.PermissionLevels
                                              where r.LevelType == 3 && r.ProgrammeID == ProgrammeID.ID
                                              && r.TeamID == TeamID
                                              select r).ToList().FirstOrDefault();
                            PermissionLevel row =
                portfolio.PermissionLevels.Single(P => P.TeamID == TeamID && P.ProgrammeID == ProgrammeID.ID
                && P.LevelType==3);
                            portfolio.PermissionLevels.DeleteOnSubmit(row);
                            portfolio.SubmitChanges();
                            BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);
                            lblSPL.Visible = true;
                            lblSPL.Text = "Deleted Successfully";
                            lblSPL.ForeColor = System.Drawing.Color.Green;
                        }
                        //else
                        //{

                        //    lblSPL.Visible = true;
                        //    lblSPL.Text = "Selected group already exists";
                        //    lblSPL.ForeColor = System.Drawing.Color.Red;

                        //}
                    }


                }
                BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void UpDateSubProgrammes(int TeamID, int RoleID)
    {
        try
        {
            //FETCH  SUBPROGRAMMES ID
            ProgrammeDataContext programme = new ProgrammeDataContext();
            var getSubProgramme = (from r in programme.OperationsOwners
                                   where r.MasterProgramme == Convert.ToInt32(ddlProgramme.SelectedValue)
                                   && r.Level == 2
                                   select r).ToList();

            if (getSubProgramme != null)
            {
                foreach (OperationsOwner ProgrammeID in getSubProgramme)
                {
                    var getExistUser = (from r in portfolio.PermissionLevels
                                        where r.LevelType == 3 && r.ProgrammeID == ProgrammeID.ID
                                        && r.TeamID == TeamID
                                        select r).ToList();


                    if (getExistUser != null)
                    {
                        if (getExistUser.Count != 0)
                        {

                            var getExistID = (from r in portfolio.PermissionLevels
                                              where r.LevelType == 3 && r.ProgrammeID == ProgrammeID.ID
                                              && r.TeamID == TeamID
                                              select r).ToList().FirstOrDefault();
                            PermissionLevel insert =
               portfolio.PermissionLevels.Single(P => P.ID == int.Parse(getExistID.ID.ToString()));
                            lblCLMsg.Visible = true;
                            insert.LevelType = 3;
                            insert.RoleID = RoleID;
                            insert.TeamID = TeamID;
                            insert.UserID = 0;// int.Parse(ddlPLUserName.SelectedValue);
                            //portfolio.PermissionLevels.InsertOnSubmit(insert);
                            portfolio.SubmitChanges();
                            //portfolio.SubmitChanges();
                            lblSPL.Visible = true;
                            lblSPL.Text = "Updated Successfully";
                            lblSPL.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {

                            lblSPL.Visible = true;
                            //lblSPL.Text = "Selected group already exists";
                            lblSPL.ForeColor = System.Drawing.Color.Red;

                        }
                    }


                }
                BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void grdProgrammeLeve_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grdProgrammeLeve.EditIndex = -1;
            BindGridProgrammeLevel(grdProgrammeLeve, 2);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdProgrammeLeve_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {

                ////int index = grdProgrammeLeve.EditIndex;
                //GridViewRow row1 = grdCustomeLevel.Rows[index];

                //Label lblPLTeamID = (Label)row1.FindControl("lblPLTeamID");
                lblMsgPL.Visible = false;
                lblSPL.Visible = false;
                lblCLMsg.Visible = false;
                var getTeamID = (from r in portfolio.PermissionLevels
                                 where r.ID == int.Parse(e.CommandArgument.ToString())
                                 select r).ToList().FirstOrDefault();

                PermissionLevel row =
                portfolio.PermissionLevels.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                portfolio.PermissionLevels.DeleteOnSubmit(row);
                portfolio.SubmitChanges();
                BindGridProgrammeLevel(grdProgrammeLeve, 2);

                DeleteSubProgramme(getTeamID.TeamID.Value);
            }
            if (e.CommandName == "Update")
            {

                int index = grdProgrammeLeve.EditIndex;
                GridViewRow row = grdProgrammeLeve.Rows[index];

                Label lblPLContractorID = (Label)row.FindControl("lblPLContractorID");
                DropDownList ddlPLTeamEdit = (DropDownList)row.FindControl("ddlPLTeamEdit");
               // DropDownList ddlPLUserName = (DropDownList)row.FindControl("ddlPLUserName");
                DropDownList ddlPLPermissions = (DropDownList)row.FindControl("ddlPLPermissions");

                PermissionLevel insert =
              portfolio.PermissionLevels.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                
               
                var getExistUser = (from r in portfolio.PermissionLevels
                                    where r.LevelType == 2 && r.ProgrammeID==int.Parse(ddlProgramme.SelectedValue)
                                    && r.TeamID == int.Parse(ddlPLTeamEdit.SelectedValue)
                                    select r).ToList();
                if (getExistUser != null)
                {
                    if (getExistUser.Count != 0)
                    {
                        lblCLMsg.Visible = true;
                        // insert.LevelType = 1;
                        //insert.PortfolioID = int.Parse(DropDownListCustomer.SelectedValue);
                        //insert.ProgrammeID = ProgrammeID;
                        insert.RoleID = int.Parse(ddlPLPermissions.SelectedValue);
                        insert.TeamID = int.Parse(ddlPLTeamEdit.SelectedValue);
                        insert.UserID = 0;// int.Parse(ddlPLUserName.SelectedValue);
                        //portfolio.PermissionLevels.InsertOnSubmit(insert);
                        portfolio.SubmitChanges();
                        lblMsgPL.Visible = true;
                        lblMsgPL.Text = "Updated Successfully";
                        lblMsgPL.ForeColor = System.Drawing.Color.Green;
                        UpDateSubProgrammes(int.Parse(ddlPLTeamEdit.SelectedValue),int.Parse(ddlPLPermissions.SelectedValue));
                    }
                    else
                    {

                        lblMsgPL.Visible = true;
                        lblMsgPL.Text = "Selected group already exists";
                        lblMsgPL.ForeColor = System.Drawing.Color.Red;

                    }
                }
                grdProgrammeLeve.EditIndex = -1;

                BindGridProgrammeLevel(grdProgrammeLeve, 2);
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdProgrammeLeve_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.FindControl("ddlPLTeamEdit") != null)
                {
                    Label lblPLRoleID = (Label)e.Row.FindControl("lblSPLRoleID");
                    Label lblPLContractorID = (Label)e.Row.FindControl("lblPLContractorID");
                    Label lblPLTeamID = (Label)e.Row.FindControl("lblPLTeamID");
                    DropDownList ddlPLPermissions = (DropDownList)e.Row.FindControl("ddlPLPermissions");
                    //DropDownList ddlPLUserName = (DropDownList)e.Row.FindControl("ddlPLUserName");
                    DropDownList ddlPLTeamEdit = (DropDownList)e.Row.FindControl("ddlPLTeamEdit");
                    BindTeam_Grid(ddlPLTeamEdit, int.Parse(lblPLTeamID.Text), 0);
                    //BindUserName_Grid(ddlPLUserName, int.Parse(lblPLContractorID.Text), int.Parse(ddlPLTeamEdit.SelectedValue));
                    ddlPLPermissions.SelectedValue = lblPLRoleID.Text;
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdProgrammeLeve_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grdProgrammeLeve.EditIndex = -1;
            BindGridProgrammeLevel(grdProgrammeLeve, 2);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdProgrammeLeve_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdProgrammeLeve.EditIndex = e.NewEditIndex;
            BindGridProgrammeLevel(grdProgrammeLeve, 2);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdProgrammeLeve_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            grdProgrammeLeve.EditIndex = -1;
            BindGridProgrammeLevel(grdProgrammeLeve, 2);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlProgrammeLevelTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
       // BindTeamMembers(Convert.ToInt32(ddlProgrammeLevelTeam.SelectedValue), ddlProgrammeLevelUsers);
    }
    
    protected void ddlPLTeamEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int index = grdSubProgrammeLevel.EditIndex;
            GridViewRow row = grdSubProgrammeLevel.Rows[index];
            //Label lblContractorID = (Label)row.FindControl("lblPLContractorID");
            DropDownList ddlPLTeamEdit = (DropDownList)row.FindControl("ddlSPLTeamEdit");
            //DropDownList ddlPLUserName = (DropDownList)row.FindControl("ddlSPLUserName");
            //BindUserName_Grid(ddlPLUserName, 0, int.Parse(ddlPLTeamEdit.SelectedValue));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdSubProgrammeLevelRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grdSubProgrammeLevel.EditIndex = -1;
            BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdSubProgrammeLevel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                lblMsgPL.Visible = false;
                lblSPL.Visible = false;
                lblCLMsg.Visible = false;
                PermissionLevel row =
                portfolio.PermissionLevels.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                portfolio.PermissionLevels.DeleteOnSubmit(row);
                portfolio.SubmitChanges();
                BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);

            }
            if (e.CommandName == "Update")
            {

                int index = grdSubProgrammeLevel.EditIndex;
                GridViewRow row = grdSubProgrammeLevel.Rows[index];

                Label lblPLContractorID = (Label)row.FindControl("lblSPLContractorID");
                DropDownList ddlPLTeamEdit = (DropDownList)row.FindControl("ddlSPLTeamEdit");
               // DropDownList ddlSPLUserName = (DropDownList)row.FindControl("ddlSPLUserName");
                DropDownList ddlPLPermissions = (DropDownList)row.FindControl("ddlSPLPermissions");

                PermissionLevel insert =
              portfolio.PermissionLevels.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                int ProgrammeID;
                
                var getExistUser = (from r in portfolio.PermissionLevels
                                    where r.LevelType == 3 && r.SubProgrammeID==int.Parse(ddlSubProgramme.SelectedValue)
                                    && r.TeamID == int.Parse(ddlPLTeamEdit.SelectedValue)
                                    select r).ToList();
                if (getExistUser != null)
                {
                    if (getExistUser.Count != 0)
                    {
                        lblCLMsg.Visible = true;
                        // insert.LevelType = 1;
                        //insert.PortfolioID = int.Parse(DropDownListCustomer.SelectedValue);
                        //insert.ProgrammeID = ProgrammeID;
                        insert.RoleID = int.Parse(ddlPLPermissions.SelectedValue);
                        insert.TeamID = int.Parse(ddlPLTeamEdit.SelectedValue);
                        insert.UserID = 0;// int.Parse(ddlSPLUserName.SelectedValue);
                        //portfolio.PermissionLevels.InsertOnSubmit(insert);
                        portfolio.SubmitChanges();
                        lblSPL.Visible = true;
                        lblSPL.Text = "Updated Successfully";
                        lblSPL.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {

                        lblSPL.Visible = true;
                        lblSPL.Text = "Selected group already exists";
                        lblSPL.ForeColor = System.Drawing.Color.Red;

                    }
                }
                grdSubProgrammeLevel.EditIndex = -1;

                BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdSubProgrammeLevel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.FindControl("ddlSPLTeamEdit") != null)
                {
                    Label lblPLRoleID = (Label)e.Row.FindControl("lblSPLRoleID");
                    Label lblPLContractorID = (Label)e.Row.FindControl("lblSPLContractorID");
                    Label lblPLTeamID = (Label)e.Row.FindControl("lblSPLTeamID");
                    DropDownList ddlPLPermissions = (DropDownList)e.Row.FindControl("ddlSPLPermissions");
                    //DropDownList ddlPLUserName = (DropDownList)e.Row.FindControl("ddlSPLUserName");
                    DropDownList ddlPLTeamEdit = (DropDownList)e.Row.FindControl("ddlSPLTeamEdit");
                    BindTeam_Grid(ddlPLTeamEdit, int.Parse(lblPLTeamID.Text), 0);
                   // BindUserName_Grid(ddlPLUserName, int.Parse(lblPLContractorID.Text), int.Parse(ddlPLTeamEdit.SelectedValue));
                    ddlPLPermissions.SelectedValue = lblPLRoleID.Text;
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdSubProgrammeLevel_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grdSubProgrammeLevel.EditIndex = -1;
            BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdSubProgrammeLevel_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdSubProgrammeLevel.EditIndex = e.NewEditIndex;
            BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdSubProgrammeLevel_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            grdSubProgrammeLevel.EditIndex = -1;
            BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlSPLTeam_SelectedIndexChanged1(object sender, EventArgs e)
    {
       // BindTeamMembers(Convert.ToInt32(ddlSPLTeam.SelectedValue), ddlSPLUserName);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        try
        {
            lblCLMsg.Visible = false;
            lblMsgPL.Visible = false;
            PermissionLevel insert = new PermissionLevel();
            int ProgrammeID;
           
            var getExistUser = (from r in portfolio.PermissionLevels
                                where r.LevelType == 3 && r.ProgrammeID == int.Parse(ddlSubProgramme.SelectedValue)
                                && r.TeamID == int.Parse(ddlSPLTeam.SelectedValue)
                                select r).ToList();
            if (getExistUser != null)
            {
                if (getExistUser.Count == 0)
                {
                    lblCLMsg.Visible = true;
                    insert.LevelType = 3;
                    insert.PortfolioID = 0;// int.Parse(DropDownListCustomer.SelectedValue);
                    insert.ProgrammeID =int.Parse(ddlSubProgramme.SelectedValue);
                    insert.RoleID = int.Parse(ddlSPLRole.SelectedValue);
                    insert.TeamID = int.Parse(ddlSPLTeam.SelectedValue);
                    insert.UserID = 0;// int.Parse(ddlSPLUserName.SelectedValue);
                    portfolio.PermissionLevels.InsertOnSubmit(insert);
                    portfolio.SubmitChanges();
                    lblSPL.Visible = true;
                    lblSPL.Text = "Added Successfully";
                    lblSPL.ForeColor = System.Drawing.Color.Green;
                }
                else
                {

                    lblSPL.Visible = true;
                    lblSPL.Text = "Selected group already exists";
                    lblSPL.ForeColor = System.Drawing.Color.Red;

                }
            }

            //ddlCLRole.SelectedIndex = -1;
            //ddlCLTeam.SelectedIndex = -1;
            //ddlCLUserName.SelectedIndex = -1;
            grdSubProgrammeLevel.EditIndex = -1;

            BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void DropDownListCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsgPL.Visible = false;
        lblSPL.Visible = false;
        lblCLMsg.Visible = false;
        //BindGridCustomerLevel(
        pnlTeamMembers.Visible = false;
        gridMembers.Visible = false;
        BindGridCustomerLevel(grdCustomeLevel, 1);
    }
    protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsgPL.Visible = false;
        lblSPL.Visible = false;
        lblCLMsg.Visible = false;
        pnlTeamMembers.Visible = false;
        gridMembers.Visible = false;
        BindGridProgrammeLevel(grdProgrammeLeve, 2);
        BindSubProgramme();
        BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);
    }
    protected void ddlSubProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlTeamMembers.Visible = false;
        gridMembers.Visible = false;
        lblMsgPL.Visible = false;
        lblSPL.Visible = false;
        lblCLMsg.Visible = false;
        BindGridSubProgrammeLevel(grdSubProgrammeLevel, 3);
    }

    protected void btnViewMembers_Click(object sender, EventArgs e)
    {
        LinkButton lnkResource = sender as LinkButton;
        GridViewRow row = (GridViewRow)lnkResource.NamingContainer;
        HiddenField hidTeamID = (HiddenField)row.FindControl("hidTeamID");

        Label lblTeam = (Label)row.FindControl("lblTeam");
       // grdTeams.SelectedRowStyle.BackColor = System.Drawing.Color.BlanchedAlmond;
        hidTeamID.Value = hidTeamID.Value;
        // ImgDelete.Visible = true;
        string _sqlTeamByPortfolioSingle = "SELECT ID,TeamName,"
                         + "(SELECT COUNT(*) FROM TEAMMEMBER WHERE TeamID=Team.ID) AS TeamCount FROM TEAM "
                         + " where ID=" + hidTeamID.Value.ToString() + " order by TeamName ";
        pnlTeamMembers.Visible = true;
        gridMembers.Visible = true;
        //LinkButton1.Visible = true;
       // BindSingleRow(_sqlTeamByPortfolioSingle);
        GridMembers(int.Parse(hidTeamID.Value), lblTeam.Text,"Customer Level");

    }
    protected void btnViewMembers1_Click(object sender, EventArgs e)
    {
        LinkButton lnkResource = sender as LinkButton;
        GridViewRow row = (GridViewRow)lnkResource.NamingContainer;
        HiddenField hidTeamID = (HiddenField)row.FindControl("hidTeamID1");

        Label lblTeam = (Label)row.FindControl("lblPLTeam");
        // grdTeams.SelectedRowStyle.BackColor = System.Drawing.Color.BlanchedAlmond;
        hidTeamID.Value = hidTeamID.Value;
        // ImgDelete.Visible = true;
        string _sqlTeamByPortfolioSingle = "SELECT ID,TeamName,"
                         + "(SELECT COUNT(*) FROM TEAMMEMBER WHERE TeamID=Team.ID) AS TeamCount FROM TEAM "
                         + " where ID=" + hidTeamID.Value.ToString() + " order by TeamName ";
        pnlTeamMembers.Visible = true;
        gridMembers.Visible = true;
        //LinkButton1.Visible = true;
        // BindSingleRow(_sqlTeamByPortfolioSingle);
        GridMembers(int.Parse(hidTeamID.Value), lblTeam.Text,"Programme Level");

    }
    protected void btnViewMembers3_Click(object sender, EventArgs e)
    {
        LinkButton lnkResource = sender as LinkButton;
        GridViewRow row = (GridViewRow)lnkResource.NamingContainer;
        HiddenField hidTeamID = (HiddenField)row.FindControl("hidTeamID3");

        Label lblTeam = (Label)row.FindControl("lblSPLTeam");
        // grdTeams.SelectedRowStyle.BackColor = System.Drawing.Color.BlanchedAlmond;
        hidTeamID.Value = hidTeamID.Value;
        // ImgDelete.Visible = true;
        string _sqlTeamByPortfolioSingle = "SELECT ID,TeamName,"
                         + "(SELECT COUNT(*) FROM TEAMMEMBER WHERE TeamID=Team.ID) AS TeamCount FROM TEAM "
                         + " where ID=" + hidTeamID.Value.ToString() + " order by TeamName ";
        pnlTeamMembers.Visible = true;
        gridMembers.Visible = true;
        //LinkButton1.Visible = true;
        // BindSingleRow(_sqlTeamByPortfolioSingle);
        GridMembers(int.Parse(hidTeamID.Value), lblTeam.Text,"Sub Programme Level");

    }
    private void GridMembers(int teamId,string gName,string Level)
    {
        Label2.Text = gName;
        Label3.Text = Level;
        string _sqlGetTeamMembersByID1 = "SELECT C.ContractorName as  memName,T.ID,C.EmailAddress,C.ContactNumber,T.Name FROM TeamMember T,Contractors C  WHERE TeamID=@TeamID and C.ID=T.Name order by C.ContractorName";
        SqlCommand _cmd = new SqlCommand();
        SqlCommand _cmd1 = new SqlCommand();
        using (TeamHelper helper = new TeamHelper())
        {
            _cmd.CommandText = _sqlGetTeamMembersByID1;
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@TeamID", teamId);
            helper.SqlCommand = _cmd;
            DataTable dt = helper.GetData();
            if (dt.Rows.Count != 0)
            {

                gridMembers.DataSource = helper.GetData();
                gridMembers.DataBind();
            }
           
        }
    }
}
