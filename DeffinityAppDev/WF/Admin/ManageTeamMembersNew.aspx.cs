using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;
public partial class ManageTeamMembersNew : System.Web.UI.Page
{
    SqlCommand _cmd = new SqlCommand();
    SqlCommand _cmd1 = new SqlCommand();
    public int teamID;
    protected void Page_Load(object sender, EventArgs e)
    {
        _sqlTeamDDL += sessionKeys.PortfolioID.ToString();
        if(!IsPostBack)
            {
               // Page.MaintainScrollPositionOnPostBack = true;
                //Master.PageHead = "Permission Manager";
                BindLocalData();
              pnlTeamMembers.Visible = false;
               // BindContrator();
                GridBind();
                //DisplaySelectedGroups();
            }

    }

    //Added newly
    private void BindSingleRow(string _sqlTeamByPortfolio)
    {
        using (TeamHelper helper = new TeamHelper())
        {
            _cmd.CommandText = _sqlTeamByPortfolio;
            _cmd.Parameters.Clear();
            //_cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
            helper.SqlCommand = _cmd;
            gridTeams.DataSource = helper.GetData();
            gridTeams.DataBind();
        }
    }

    private void BindSingleRow_Customersgrid(string _sqlTeamByPortfolio)
    {
        using (TeamHelper helper = new TeamHelper())
        {
            _cmd.CommandText = _sqlTeamByPortfolio;
            _cmd.Parameters.Clear();
            //_cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
            helper.SqlCommand = _cmd;
            gridMembers.DataSource = helper.GetData();
            gridMembers.DataBind();
        }
    }
    #region BindData
    private void BindContrator(int teamID)
    {

        _cmd.Parameters.Clear();
        //_cmd.Parameters.AddWithValue("@ContractorID", ddlContractorName.SelectedValue);
        _cmd.CommandText = _sqlGetTeamMembers;
        // using (TeamHelper helper = new TeamHelper())
        //{
        //helper.SqlCommand = _cmd;
        // }

        using (TeamHelper helper = new TeamHelper())
        {
            _cmd.CommandText = _sqlGetTeamMembers;
           // _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@TeamID", teamID);
            helper.SqlCommand = _cmd;
            CheckBoxList2.DataSource = helper.GetData();
            CheckBoxList2.DataValueField = "ID";
            CheckBoxList2.DataTextField = "ContractorName";
            CheckBoxList2.DataBind();
        }
        DisplaySelectedGroups();
      
       
    }
    #endregion 
    #region List Of Sql Statements

    #region Select Statements

    string _sqlTeamDDL = "Select ID,TeamName From Team where PortfolioID=";
    string _sqlTeamByPortfolio = "SELECT ID,TeamName,"
                           + "(SELECT COUNT(*) FROM TEAMMEMBER WHERE TeamID=Team.ID) AS TeamCount FROM TEAM "
                           + "  order by TeamName ";
  

    //string _sqlTeamByPortfolio = "SELECT ID,TeamName,"
    //                        + "(SELECT COUNT(*) FROM TEAMMEMBER WHERE TeamID=Team.ID) AS TeamCount FROM TEAM "
    //                        + " WHERE PortfolioID=@PortfolioID";
    string _sqlGetTeamMembersByID = "SELECT (Select ContractorName From Contractors where ID=Name) AS Name,ID,Email,Replace(Skills,char(13)+char(10),'<br/>') AS Skills FROM TeamMember WHERE TeamID=@TeamID";
    string _sqlGetTeamMemberDefaultDetails = "SELECT ID,ContractorName,EmailAddress,Details,ContactNumber FROM Contractors WHERE ID=@ContractorID";
   string _sqlGetTeamMembers = "SELECT ID,ContractorName,EmailAddress,Details,ContactNumber FROM Contractors where  Status='active'  order by ContractorName";
   //string _sqlGetTeamMembers = "SELECT ID,ContractorName,EmailAddress,Details,ContactNumber FROM Contractors where  Status='active' and SID not in (7,8)  order by ContractorName";//modified on 22/08/2011
    //string _sqlGetTeamMembers = "SELECT ID,ContractorName,EmailAddress,Details,ContactNumber FROM Contractors where id not in (select Name from teammember where teamid =@TeamID ) and Status='active' order by ContractorName";
        // "SELECT ID,ContractorName,EmailAddress,Details,ContactNumber FROM Contractors where id not in (select Name from teammember where teamid =@TeamID )";
   string _sqlGetTeamMembersByID1 = "SELECT C.ContractorName as  memName,T.TeamID,C.EmailAddress,C.ID as CID,C.ContactNumber,T.Name FROM TeamMember T,Contractors C  WHERE TeamID=@TeamID and C.ID=T.Name order by C.ContractorName";
    #endregion

    #region DML Statements

    string _sqlInsertTeamMember = "INSERT INTO TeamMember(TeamID,Name,ContactNumber,Email,Location,Skills) "
        + "Values(@TeamID,@Name,@ContactNumber,@Email,@Location,@Skills)";

    string _insertTeamRecord = "INSERT INTO TEAM(TeamName,PortfolioID) Values(@TeamName,@PortfolioID)";
    //string _insertTeamRecord = "INSERT INTO TEAM(TeamName) Values(@TeamName)";
    string _deleteTeam = "Delete From Team Where ID=@ID";
    string _deleteSelectedTeam = "Delete From Team Where ID in (select ID from fnSplitter(@IDs))";
    string _copyTeamRecord = "Deffinity_Team_CopyOtherPortfolioTeam";
    string _deleteTeamMembers = "Delete From TeamMember Where Name in (select ID from fnSplitter(@IDs)) and TeamID=@TeamID";

    #endregion

    #endregion
    protected void btnAddTeamMember_Click(object sender, EventArgs e)
    {
        try
        {
            int items = 0;
           
                  // string ResourceIDs = string.Empty;
            PortfolioDataContext portfolio = new PortfolioDataContext();




            var getTeams = (from r in portfolio.TeamMembers
                            where r.TeamID == int.Parse(hdTeamID.Value)
                            orderby r.Name
                            select r).ToList();

                    for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                    {
                        //if (getTeams.Count > 0)
                        //{
                        //    for (int k = 0; k < getTeams.Count; k++)
                        //    {
                                //if (getTeams[k].Name != int.Parse(CheckBoxList2.Items[i].Value))
                                //{
                                    if (CheckBoxList2.Items[i].Selected)
                                    {
                                        var isExist = (from r in portfolio.TeamMembers
                                                        where r.TeamID == int.Parse(hdTeamID.Value)
                                                        && r.Name == int.Parse(CheckBoxList2.Items[i].Value)
                                                        orderby r.Name
                                                        select r).ToList();
                                        items++;
                                        //ResourceIDs = ResourceIDs + CheckBoxList2.Items[i].Value.ToString() + ",";
                                        if(isExist.Count==0)
                                        {
                                            using (TeamHelper helper = new TeamHelper())
                                            {

                                                _cmd.CommandText = _sqlInsertTeamMember;
                                                addInsertParametersForTeamMembers(int.Parse(hdTeamID.Value), int.Parse(CheckBoxList2.Items[i].Value));
                                                helper.SqlCommand = _cmd;
                                                if (helper.UpdateOrInsertOrDeleteData())
                                                {
                                                    lblMessage.Text = "Resource inserted successfully";
                                                    lblMessage.ForeColor = System.Drawing.Color.Green;
                                                    clearTeamMemberControls();
                                                    GridBind();
                                                }
                                                else
                                                {
                                                    lblMessage.Text = "Insertion failed. Please try again.";
                                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                                }
                                            }
                                        }
                                    }
                                //}
                            //}
                        //}
                       
                       
                    }
                    if (items == 0)
                    {
                        lblErrMsg.Text = "Please select atleast one member";
                        lblErrMsg.ForeColor = System.Drawing.Color.Red;
                        mdlMembers.Show();
                    }
               
               
            }
       
        catch (SqlException ex)
        {
            if (ex.Message.Contains("Violation of UNIQUE KEY constraint "))
            {
               // lblMessage.Text = "Member already exists in the team.";
                //lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
                throw;
        }
        catch (genericException ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    protected void lnkOk_Click(object sender, EventArgs e)
    {
        int items = 0;
        try
        {
            for (int i = 0; i < grdTeams.Rows.Count; i++)
            {
                GridViewRow row = grdTeams.Rows[i];
                CheckBox chkbox1 = (CheckBox)row.FindControl("chkItem");
                Label lblID = (Label)row.FindControl("lblID");
                Label lblTeamName = (Label)row.FindControl("lblTeamName");


                if (chkbox1.Checked)
                {
                    items++;
                    using (TeamHelper helper = new TeamHelper())
                    {
                        int TeamID = Convert.ToInt32(lblID.Text);
                        _cmd1.CommandText = _copyTeamRecord;
                        _cmd1.CommandType = CommandType.StoredProcedure;
                        addCopyParametersForTeam(TeamID, sessionKeys.PortfolioID, lblTeamName.Text);

                        helper.SqlCommand = _cmd1;
                        if (helper.CopyData() == 2)
                        {
                            chkbox1.Checked = false;
                            lblMessage.Text = "Group created successfully";
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                            BindLocalData();
                            clearTeamControls();
                            GridBind();
                        }
                        else if (helper.CopyData() == 1)
                        {
                            chkbox1.Checked = false;
                            lblMessage.Text = "Team Already Exists";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            lblMessage.Text = "Insertion failed. Please try again.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }

            if (items == 0)
            {
                lblmsg.Text = "Please select atleast one Team";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void btnAddTeam_Click(object sender, EventArgs e)
    {
        try
        {
            gridMembers.Visible = false;
            using (TeamHelper helper = new TeamHelper())
            {
                _cmd.CommandText = _insertTeamRecord;
                addInsertParametersForTeam();
                helper.SqlCommand = _cmd;
                if (helper.UpdateOrInsertOrDeleteData())
                {
                    lblMessage.Text = "Group created successfully";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    BindLocalData();
                    clearTeamControls();
                    GridBind();
                }
                else
                {
                    lblMessage.Text = "Insertion failed. Please try again.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Violation of UNIQUE KEY constraint 'UniqueTeam'."))
                lblMessage.Text = "Team name already exists. Please try with a different name.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnAddResource_Click(object sender, EventArgs e)
    {
        try
        {
            Panel2.Visible = false;
            LinkButton1.Visible = true;
            pnlTeamMembers.Visible = false;
            //imgShowAll.Visible = true;
            mdlMembers.Show();//OnClick="btnViewMembers_Click"
            LinkButton lnkResource = sender as LinkButton;
            GridViewRow row = (GridViewRow)lnkResource.NamingContainer;

            Label litTeamName = (Label)row.FindControl("litTeamName");
            Label lblID = (Label)row.FindControl("lblID");
            HiddenField hidTeamID = (HiddenField)row.FindControl("hidTeamID");
            lblTeamsName.Text = litTeamName.Text;
            hdTeamID.Value = hidTeamID.Value;
            string _sqlTeamByPortfolioSingle = "SELECT ID,TeamName,"
                           + "(SELECT COUNT(*) FROM TEAMMEMBER WHERE TeamID=Team.ID) AS TeamCount FROM TEAM "
                           + " where ID=" + hdTeamID.Value.ToString() + " order by TeamName ";
            CheckBoxList2.Items.Clear();
            BindContrator(int.Parse(hdTeamID.Value));
            DisplaySelectedGroups();
            txtSearch.Text = "";
            //  GridMembers(int.Parse(hidTeamID.Value));
            BindSingleRow(_sqlTeamByPortfolioSingle);
            mdlMembers.Show();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    protected void btnViewMembers_Click(object sender, EventArgs e)
    {
        try
        {
        LinkButton2.Visible = false;
        Panel2.Visible = false;
        LinkButton lnkResource = sender as LinkButton;
        GridViewRow row = (GridViewRow)lnkResource.NamingContainer;
        HiddenField hidTeamID = (HiddenField)row.FindControl("hidTeamID");
        grdTeams.SelectedRowStyle.BackColor = System.Drawing.Color.BlanchedAlmond;
        hdTeamID.Value = hidTeamID.Value;
       // ImgDelete.Visible = true;
        string _sqlTeamByPortfolioSingle = "SELECT ID,TeamName,"
                         + "(SELECT COUNT(*) FROM TEAMMEMBER WHERE TeamID=Team.ID) AS TeamCount FROM TEAM "
                         + " where ID=" + hdTeamID.Value.ToString() + " order by TeamName ";
        pnlTeamMembers.Visible = true;
        gridMembers.Visible = true;
        LinkButton1.Visible = true;
        //imgShowAll.Visible = true;
        BindSingleRow(_sqlTeamByPortfolioSingle);
       
        GridMembers(int.Parse(hidTeamID.Value));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
     }
    protected void btnViewMembers11_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton2.Visible = true;
            LinkButton lnkResource = sender as LinkButton;
            GridViewRow row = (GridViewRow)lnkResource.NamingContainer;
            HiddenField hidUID = (HiddenField)row.FindControl("hidTeamID11");
            Label lblID = (Label)row.FindControl("lblID");
            Literal litTeamSize = (Literal)row.FindControl("litTeamSize");
            // grdTeams.SelectedRowStyle.BackColor = System.Drawing.Color.BlanchedAlmond;
            //hdTeamID.Value = hidTeamID.Value;
            // ImgDelete.Visible = true;
            string _sqlGetTeamMembersByID11 = "SELECT C.ContractorName as  memName,C.EmailAddress,C.ID as CID," +
            "C.ContactNumber FROM Contractors C  WHERE ID=" + hidUID.Value + " order by C.ContractorName";

            string _sqlGetTeamMembersByID12 = "SELECT C.ContractorName as  memName,T.TeamID,C.EmailAddress,C.ID as CID," +
             " C.ContactNumber,T.Name FROM TeamMember T,Contractors C  WHERE TeamID=" + hdTeamID.Value.ToString() + " and C.ID=T.Name " +
            " and c.ID = " + hidUID.Value + " order by C.ContractorName";
            pnlTeamMembers.Visible = true;
            gridMembers.Visible = true;
            LinkButton1.Visible = true;
            Panel2.Visible = true;
            lblUserName.Text = litTeamSize.Text;
            BindSingleRow_Customersgrid(_sqlGetTeamMembersByID12);
            LinkButton2.Visible = true;
            HiddenField1.Value = lblID.Text;
            showTeams(int.Parse(hidUID.Value));
            showCustomers(int.Parse(hidUID.Value));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
   
    #region Helper Methods

    #region Methods for adding Parameters to Command Object

    private void GridBind()
    {
        using (TeamHelper helper = new TeamHelper())
        {
            _cmd.CommandText = _sqlTeamByPortfolio;
            _cmd.Parameters.Clear();
            //_cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
            helper.SqlCommand = _cmd;
            gridTeams.DataSource = helper.GetData();
            gridTeams.DataBind();
        }
    }

    private void GridMembers(int teamId)
    {
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
            else
            {
                pnlTeamMembers.Visible = false;
                gridMembers.Visible = false;
            }
        }
    }
    private void showTeams(int userID)
    {
        PortfolioDataContext teams = new PortfolioDataContext();
        var teamsID = from r in teams.TeamMembers
                      where r.Name == userID
                      select r.TeamID ;
        var teamsNames = (from t in teams.Teams
                         where teamsID.Contains(t.ID)
                          orderby t.TeamName
                          select new { t.TeamName, t.ID }).ToList();
        if (teamsNames != null)
        {
            if (teamsNames.Count() != 0)
            {
                Panel2.Visible = true;
                grdGroups.DataSource = teamsNames;
                grdGroups.DataBind();
            }
            else
            {
                Panel2.Visible = false;
            }
        }

    }

    private void showCustomers(int userID)
    {
        DataTable dt = new DataTable();
        string query = "select distinct p.ID,p.PortFolio from PermissionLevels pr,ProjectPortfolio p where TeamID in( "
                         + " select ID from Team where ID in (select TeamID from TeamMember where Name=@UserID)) and PortfolioID!=0 " +
                   " and p.ID=pr.PortfolioID order by p.PortFolio";
       
      
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text,query, new SqlParameter("@UserID", userID)).Tables[0];
        if (dt.Rows.Count != 0)
        {
            Panel2.Visible = true;
            grdCustomers.DataSource = dt;
            grdCustomers.DataBind();
        }
        else
        {
            Panel2.Visible = false;
        }

    }

    private void GridSingleMember(int teamId)
    {

    }
    protected void gridTeams_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gridTeams.PageIndex = e.NewPageIndex;
        GridBind();
    }
    protected void gridTeams_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(((Label)(gridTeams.Rows[e.RowIndex].FindControl("lblID"))).Text);
        _cmd.CommandText = _deleteTeam;
        _cmd.Parameters.Clear();
        _cmd.Parameters.AddWithValue("@ID", id);
        using (TeamHelper helper = new TeamHelper())
        {
            helper.SqlCommand = _cmd;
            helper.UpdateOrInsertOrDeleteData();
        }
        GridBind();
        BindLocalData();
    }

    private void addInsertParametersForTeamMembers(int TeamID,int NameID)
    {
        _cmd.Parameters.Clear();
        _cmd.Parameters.AddWithValue("@TeamID", TeamID);
        _cmd.Parameters.AddWithValue("@Name", NameID);
        _cmd.Parameters.AddWithValue("@ContactNumber", "");
        _cmd.Parameters.AddWithValue("@Email", "");
        _cmd.Parameters.AddWithValue("@Location", "");
        _cmd.Parameters.AddWithValue("@Skills", "");
    }

    private void addInsertParametersForTeam()
    {
        _cmd.Parameters.Clear();
        _cmd.Parameters.AddWithValue("@TeamName", txtTeamName.Text);
     //   _cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
        _cmd.Parameters.AddWithValue("@PortfolioID",0);
    }

    private void addCopyParametersForTeam(int TeamID, int PortfolioID, string TeamName)
    {
        _cmd1.Parameters.Clear();
        _cmd1.Parameters.AddWithValue("@TeamID", TeamID);
        _cmd1.Parameters.AddWithValue("@TeamName", TeamName);
        _cmd1.Parameters.AddWithValue("@PortfolioID", PortfolioID);
        _cmd1.Parameters.AddWithValue("@Out", 0);
    }




    #endregion

    private void BindLocalData()
    {
        //using (TeamHelper helper = new TeamHelper())
        //{
        //    helper.SqlCommand = _cmd;
        //    helper.BindDDLData(_sqlTeamDDL, ddlTeam, "ID", "TeamName");
        //}
    }

    private void clearTeamControls()
    {
        txtTeamName.Text = string.Empty;
    }

    private void clearTeamMemberControls()
    {
        //ddlTeam.SelectedIndex = 0;
        //txtContactNumber.Text = string.Empty;
        //txtEmailAddress.Text = string.Empty;
        //txtLocation.Text = string.Empty;
        //ddlContractorName.SelectedIndex = 0;
        //txtSkills.Text = string.Empty;
        //gridTeams.DataBind();
    }

    #endregion

    //protected void ImgDelete_Click(object sender, ImageClickEventArgs e)
    //{
    //}
    protected void ImgDelete_Click(object sender, EventArgs e)
    {

        try
        {
            int items = 0;
            string ids = "";
            foreach (GridViewRow row in gridMembers.Rows)
            {
                CheckBox chkRow = (CheckBox)row.FindControl("cbSelectAll");
                // HiddenField hidTeamID = (HiddenField)row.FindControl("hidTeamID");
                if (chkRow.Checked)
                {
                    items++;
                    Label lblID = (Label)row.FindControl("lblID");
                    ids += lblID.Text + ",";

                }
            }

            _cmd.CommandText = _deleteTeamMembers;
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IDs", ids);
            _cmd.Parameters.AddWithValue("@TeamID", int.Parse(hdTeamID.Value));
            using (TeamHelper helper = new TeamHelper())
            {
                helper.SqlCommand = _cmd;
                helper.UpdateOrInsertOrDeleteData();
            }
            GridMembers(int.Parse(hdTeamID.Value));
            GridBind();
          
            if (items != 0)
            {
                lblMessage1.Text = "Deleted successfully";
                lblMessage1.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage1.Text = "Please select atleast one member";
                lblMessage1.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridMembers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridMembers.PageIndex = e.NewPageIndex;
        GridMembers(int.Parse(hdTeamID.Value));
    }
    protected void gridMembers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            //onclick="javascript:SelectAllCheckboxesSpecific(this);"
            //((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "');");
            //((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAllCheckboxesSpecific('" + ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "');");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        pnlTeamMembers.Visible = false;
        LinkButton1.Visible = false;
        gridMembers.Visible = false;
        //imgShowAll.Visible = false;
        Panel2.Visible = false;
        GridBind();
    }
    protected void lnkDeleteTeam_Click(object sender, EventArgs e)
    {
        try
        {
            int items = 0;
            string ids = "";
            foreach (GridViewRow row in gridTeams.Rows)
            {
                CheckBox chkRow = (CheckBox)row.FindControl("cbSelectAll1");
                // HiddenField hidTeamID = (HiddenField)row.FindControl("hidTeamID");
                if (chkRow.Checked)
                {
                    items++;
                    Label lblID = (Label)row.FindControl("lblID1");
                    ids += lblID.Text + ",";

                }
            }

            _cmd.CommandText = _deleteSelectedTeam;
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IDs", ids);
            //_cmd.Parameters.AddWithValue("@TeamID", int.Parse(hdTeamID.Value));
            using (TeamHelper helper = new TeamHelper())
            {
                helper.SqlCommand = _cmd;
                helper.UpdateOrInsertOrDeleteData();
            }
            //GridMembers(int.Parse(hdTeamID.Value));
           
            if (items != 0)
            {
                Label1.Text = "Deleted successfully";
                Label1.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                Label1.Text = "Please select atleast one group";
                Label1.ForeColor = System.Drawing.Color.Red;
            }
            GridBind();

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        
        SearchTeamMembers(int.Parse(hdTeamID.Value));
        DisplaySelectedGroups();
        mdlMembers.Show();
    }
    private void SearchTeamMembers(int teamID)
    {
        string _sqlGetTeamMembers = "SELECT ID,ContractorName,EmailAddress,Details,ContactNumber FROM Contractors where  Status='active' and SID not in (7,8) and ContractorName like '%'+'" + txtSearch.Text.Trim() + "'+'%'  order by ContractorName";
        _cmd.Parameters.Clear();
        //_cmd.Parameters.AddWithValue("@ContractorID", ddlContractorName.SelectedValue);
        _cmd.CommandText = _sqlGetTeamMembers;
        // using (TeamHelper helper = new TeamHelper())
        //{
        //helper.SqlCommand = _cmd;
        // }

        using (TeamHelper helper = new TeamHelper())
        {
            _cmd.CommandText = _sqlGetTeamMembers;
            // _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@TeamID", teamID);
            helper.SqlCommand = _cmd;
            CheckBoxList2.DataSource = helper.GetData();
            CheckBoxList2.DataValueField = "ID";
            CheckBoxList2.DataTextField = "ContractorName";
            CheckBoxList2.DataBind();
        }


    }
    protected void DisplaySelectedGroups()
    {

        PortfolioDataContext portfolio = new PortfolioDataContext();

       


            var getTeams = (from r in portfolio.TeamMembers
                            where r.TeamID == int.Parse(hdTeamID.Value)
                            select r).ToList();



            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
            {

                if (getTeams.Count > 0)
                {
                    for (int k = 0; k < getTeams.Count; k++)
                    {
                        if (getTeams[k].Name == int.Parse(CheckBoxList2.Items[i].Value))
                        {
                            CheckBoxList2.Items[i].Selected = true;
                        }
                    }
                }

            }
       

    }
    protected void lnkShowAll_Click(object sender, EventArgs e)
    {
        BindContrator(int.Parse(hdTeamID.Value));
        DisplaySelectedGroups();
        mdlMembers.Show();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton2.Visible = false;
        Panel2.Visible = false;
        GridMembers(int.Parse(hdTeamID.Value));
    }
}
