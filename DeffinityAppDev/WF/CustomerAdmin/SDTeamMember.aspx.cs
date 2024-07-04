using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class SDTeamMember : System.Web.UI.Page
{
    SqlCommand _cmd = new SqlCommand();
    SqlCommand _cmd1 = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        _sqlTeamDDL += sessionKeys.PortfolioID.ToString();
        if (!IsPostBack)
        {
            // Page.MaintainScrollPositionOnPostBack = true;
            //Master.PageHead = "Customer Admin";
            BindLocalData();
            pnlTeamMembers.Visible = false;
            // BindContrator();
            GridBind();
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


    }
    #endregion
    #region List Of Sql Statements

    #region Select Statements

    string _sqlTeamDDL = "Select ID,TeamName From Team where PortfolioID=";
    string _sqlTeamByPortfolio = "SELECT ID,TeamName,"
                            + "(SELECT COUNT(*) FROM SDTeamToUsers WHERE SDTeamID=SDTeam.ID) AS TeamCount FROM SDTeam "
                            + " WHERE PortfolioID=@PortfolioID";

    string _sqlTeamByPortfolioZero = "SELECT ID,TeamName,"
                           + "(SELECT COUNT(*) FROM TEAMMEMBER WHERE TeamID=Team.ID) AS TeamCount FROM TEAM "
                           + " WHERE PortfolioID!=0";
    string _sqlGetTeamMembersByID = "SELECT (Select ContractorName From Contractors where ID=Name) AS Name,ID,Email,Replace(Skills,char(13)+char(10),'<br/>') AS Skills FROM TeamMember WHERE TeamID=@TeamID";
    string _sqlGetTeamMemberDefaultDetails = "SELECT ID,ContractorName,EmailAddress,Details,ContactNumber FROM Contractors WHERE ID=@ContractorID";
    string _sqlGetTeamMembers = "SELECT ID,ContractorName,EmailAddress,Details,ContactNumber FROM Contractors where id not in (select Name from teammember where teamid =@TeamID ) and SID not in(7,8) and Status='active' order by ContractorName";
    // "SELECT ID,ContractorName,EmailAddress,Details,ContactNumber FROM Contractors where id not in (select Name from teammember where teamid =@TeamID )";
    string _sqlGetTeamMembersByID1 = "SELECT C.ContractorName as  memName,T.ID,C.EmailAddress,C.ContactNumber,T.UserID FROM SDTeamToUsers T,Contractors C  WHERE SDTeamID=@TeamID and C.ID=T.UserID";
    #endregion

    #region DML Statements

    string _sqlInsertTeamMember = "INSERT INTO SDTeamToUsers(SDTeamID,UserID) "
        + "Values(@TeamID,@Name)";
    string _insertTeamRecord = "INSERT INTO SDTEAM(TeamName,PortfolioID) Values(@TeamName,@PortfolioID)";
    string _deleteTeam = "Delete From SDTeam Where ID=@ID";
    string _copyTeamRecord = "Deffinity_SDTeam_CopyOtherPortfolioTeam";
    string _deleteTeamMembers = "Delete From SDTeamToUsers Where UserID in (select ID from fnSplitter(@IDs)) and SDTeamID=@TeamID";

    #endregion

    #endregion
    protected void btnAddTeamMember_Click(object sender, EventArgs e)
    {
        try
        {
            int items = 0;

            // string ResourceIDs = string.Empty;

            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
            {
                if (CheckBoxList2.Items[i].Selected)
                {
                    items++;
                    //ResourceIDs = ResourceIDs + CheckBoxList2.Items[i].Value.ToString() + ",";

                    using (TeamHelper helper = new TeamHelper())
                    {

                        _cmd.CommandText = _sqlInsertTeamMember;
                        addInsertParametersForTeamMembers(int.Parse(hdTeamID.Value), int.Parse(CheckBoxList2.Items[i].Value));
                        helper.SqlCommand = _cmd;
                        if (helper.UpdateOrInsertOrDeleteData())
                        {
                            lblMessage.Text = "Record inserted successfully";
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
            if (items == 0)
            {
                lblErrMsg.Text = "Please select atleast one member";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                mdlMembers.Show();
            }


        }

        catch (SqlException ex)
        {
            if (ex.Message.Contains("Violation of UNIQUE KEY constraint "))
            {
                lblMessage.Text = "Member already exists in the team.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
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
                            lblMessage.Text = "Record inserted successfully";
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
                lblMessage.ForeColor = System.Drawing.Color.Red;
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
                    lblMessage.Text = "Record inserted successfully";
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
            {
                lblMessage.Text = "Team name already exists. Please try with a different name.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void btnAddResource_Click(object sender, EventArgs e)
    {
        pnlTeamMembers.Visible = false;
        mdlMembers.Show();//OnClick="btnViewMembers_Click"
        LinkButton lnkResource = sender as LinkButton;
        GridViewRow row = (GridViewRow)lnkResource.NamingContainer;
        Label litTeamName = (Label)row.FindControl("litTeamName");
        Label lblID = (Label)row.FindControl("lblID");
        HiddenField hidTeamID = (HiddenField)row.FindControl("hidTeamID");
        lblTeamsName.Text = litTeamName.Text;
        hdTeamID.Value = hidTeamID.Value;
        CheckBoxList2.Items.Clear();
        BindContrator(int.Parse(hdTeamID.Value));

        mdlMembers.Show();

    }
    protected void btnViewMembers_Click(object sender, EventArgs e)
    {
        LinkButton lnkResource = sender as LinkButton;
        GridViewRow row = (GridViewRow)lnkResource.NamingContainer;
        HiddenField hidTeamID = (HiddenField)row.FindControl("hidTeamID");
        hdTeamID.Value = hidTeamID.Value;
        // ImgDelete.Visible = true;
        pnlTeamMembers.Visible = true;
        gridMembers.Visible = true;
        GridMembers(int.Parse(hidTeamID.Value));

    }


    #region Helper Methods

    #region Methods for adding Parameters to Command Object

    private void GridBind()
    {
        using (TeamHelper helper = new TeamHelper())
        {
            //if (sessionKeys.PortfolioID == 0)
            //{
            //    _cmd.CommandText = _sqlTeamByPortfolioZero;
            //}
            //else
            //{
            //    _cmd.Parameters.Clear();
            //    _cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
            //    _cmd.CommandText = _sqlTeamByPortfolio;
            //}
            _cmd.CommandText = _sqlTeamByPortfolio;
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
            //_cmd.CommandText = _sqlTeamByPortfolio;
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

    private void addInsertParametersForTeamMembers(int TeamID, int NameID)
    {
        _cmd.Parameters.Clear();
        _cmd.Parameters.AddWithValue("@TeamID", TeamID);
        _cmd.Parameters.AddWithValue("@Name", NameID);
       
    }

    private void addInsertParametersForTeam()
    {
        _cmd.Parameters.Clear();
        _cmd.Parameters.AddWithValue("@TeamName", txtTeamName.Text);
        _cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
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

    //protected void ImgDelete_Click(object sender, EventArgs e)
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
}