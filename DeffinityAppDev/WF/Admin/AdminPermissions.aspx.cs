using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using UserMgt.DAL;
using UserMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using Microsoft.ApplicationBlocks.Data;

public partial class AdminPermissions : System.Web.UI.Page
{
    Location.DAL.LocationDataContext locationCntxt;
    UserDataContext AdminULnqcntxt;
    DisBindings getData = new DisBindings();
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    string userName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {try
        {
            //Master.PageHead = "Admin";
            if (Request.QueryString["uid"] != null)
            {
                BindCustomers();
                SelectUserData(Convert.ToInt32(Request.QueryString["uid"]));
                // UserId=(Convert.ToInt32(Request.QueryString["uid"]));
                BindTeams();
                BindSDTeams();

                DisplaySelectedGroups(Convert.ToInt32(Request.QueryString["uid"]));
                DisplaySDSelectedGroups();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        }
    }


    #region "Bind Data"
    private void BindCustomers()
    {
        PortfolioDataContext timeSheet = new PortfolioDataContext();


        try
        {
            var portfolio = from r in timeSheet.ProjectPortfolios
                            where r.Visible == true
                            orderby r.PortFolio
                            select r;
            ddlCustomers.DataSource = portfolio;
            ddlCustomers.DataTextField = "PortFolio";
            ddlCustomers.DataValueField = "ID";
            ddlCustomers.DataBind();
            ddlCustomers.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindTeams()
    {
        try
        {
        PortfolioDataContext portfolio = new PortfolioDataContext();
        //var getTeams = (from r in portfolio.Teams
        //                select r).ToList();
        var getTeams = (from r in portfolio.PermissionLevels
                        join t in portfolio.Teams on r.TeamID equals t.ID
                        where r.PortfolioID != 0
                        select new { ID = t.ID, TeamName = t.TeamName }).ToList().Distinct();
        CheckBoxList2.DataSource = getTeams;
        CheckBoxList2.DataValueField = "ID";
        CheckBoxList2.DataTextField = "TeamName";
        CheckBoxList2.DataBind();

        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }

    private void BindTeams(int customerID)
    {
        try
        {
            PortfolioDataContext portfolio = new PortfolioDataContext();
            //var getTeams = (from r in portfolio.Teams
            //                where r.PortfolioID == customerID
            //                select r).ToList();
            var getTeams = (from r in portfolio.PermissionLevels
                            join t in portfolio.Teams on r.TeamID equals t.ID
                            where r.PortfolioID == customerID
                            select new { ID = t.ID, TeamName = t.TeamName }).ToList().Distinct();
            CheckBoxList2.DataSource = getTeams;
            CheckBoxList2.DataValueField = "ID";
            CheckBoxList2.DataTextField = "TeamName";
            CheckBoxList2.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    private void BindSDTeams()
    {
        try
        {
            PortfolioDataContext portfolio = new PortfolioDataContext();
            //var getTeams = (from r in portfolio.Teams
            //                select r).ToList();
            var getTeams = (from s in portfolio.SDteams
                            where s.PortfolioID != 0
                            select new { s.ID, s.TeamName, s.PortfolioID }).OrderBy(s => s.TeamName).ToList().Distinct();
            if (Convert.ToInt32(ddlCustomers.SelectedValue) > 0)
            {
                getTeams = getTeams.Where(g => g.PortfolioID == Convert.ToInt32(ddlCustomers.SelectedValue)).ToList();
            }
          
            chkSDTeam.DataSource = getTeams;
            chkSDTeam.DataValueField = "ID";
            chkSDTeam.DataTextField = "TeamName";
            chkSDTeam.DataBind();

        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
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

                    lblusername.Text = dr["ContractorName"].ToString();
                    lblUser.Text = dr["ContractorName"].ToString();

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
    protected void DisplaySelectedGroups(int UserID)
    {

        PortfolioDataContext portfolio = new PortfolioDataContext();
       
        if (Request.QueryString["uid"] != null)
        {


            var getTeams = (from r in portfolio.TeamMembers
                            where r.Name == Convert.ToInt32(Request.QueryString["uid"])
                            select r).ToList();



            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
            {

                if (getTeams.Count > 0)
                {
                    for (int k = 0; k < getTeams.Count; k++)
                    {
                        if (getTeams[k].TeamID ==int.Parse(CheckBoxList2.Items[i].Value))
                        {
                            CheckBoxList2.Items[i].Selected = true;
                        }
                    }
                }

            }
        }
        
    }
    protected void DisplaySDSelectedGroups()
    {

        PortfolioDataContext portfolio = new PortfolioDataContext();

        if (Request.QueryString["uid"] != null)
        {


            var getTeams = (from r in portfolio.SDteamToUsers
                            where r.UserID == Convert.ToInt32(Request.QueryString["uid"])
                            select r).ToList();



            for (int i = 0; i < chkSDTeam.Items.Count; i++)
            {

                if (getTeams.Count > 0)
                {
                    for (int k = 0; k < getTeams.Count; k++)
                    {
                        if (getTeams[k].SDteamID == int.Parse(chkSDTeam.Items[i].Value))
                        {
                            chkSDTeam.Items[i].Selected = true;
                        }
                    }
                }

            }
        }

    }
     #endregion
    protected void imgApply_Click(object sender, EventArgs e)
    {
        PortfolioDataContext portfolio = new PortfolioDataContext();
        UserDataContext users = new UserDataContext();
       
        TeamMember teamMembers = new TeamMember();
        if (Request.QueryString["uid"] != null)                
        {
            var getUser = (from r in users.Contractors
                           where r.ID == int.Parse(Request.QueryString["uid"].ToString())
                           select r).ToList().FirstOrDefault();

            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
            {
                if (CheckBoxList2.Items[i].Selected)
                {
                    if (getUser != null)
                    {
                        var isExist = (from r in portfolio.TeamMembers
                                       where r.Name == int.Parse(Request.QueryString["uid"].ToString())
                                       && r.TeamID == int.Parse(CheckBoxList2.Items[i].Value.ToString())
                                       select r).ToList();
                        if (isExist != null)
                        {
                            if (isExist.Count == 0)
                            {
                                try
                                {
                                    teamMembers.Name = getUser.ID;
                                    teamMembers.TeamID = int.Parse(CheckBoxList2.Items[i].Value.ToString());
                                    teamMembers.Email = getUser.EmailAddress;
                                    teamMembers.ContactNumber = getUser.ContactNumber;
                                    portfolio.TeamMembers.InsertOnSubmit(teamMembers);
                                    portfolio.SubmitChanges();
                                }
                                catch (Exception ex)
                                {
                                    LogExceptions.WriteExceptionLog(ex);
                                }

                            }
                        }
                    }
                }
                else
                {
                    if (getUser != null)
                    {
                        var isExist = (from r in portfolio.TeamMembers
                                       where r.Name == int.Parse(Request.QueryString["uid"].ToString())
                                       && r.TeamID == int.Parse(CheckBoxList2.Items[i].Value.ToString())
                                       select r).ToList();
                        if (isExist != null)
                        {
                            foreach (var detail in isExist)
                            {
                                portfolio.TeamMembers.DeleteOnSubmit(detail);
                            }
                            portfolio.SubmitChanges();
                            //if (isExist.ID != 0)
                            //{
                            //    SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text,
                            //        "delete TeamMember where TeamID=@TeamID and Name=@Name",
                            //        new SqlParameter("@TeamID", isExist.TeamID),
                            //        new SqlParameter("@Name", isExist.Name));

                            //}
                        }
                    }
                }
            }

           
        }
        BindAfterClick();
    }
    protected void ddlCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAfterClick();
        BindAfterSDClick();
    }
    private void BindAfterClick()
    {
        if (int.Parse(ddlCustomers.SelectedValue) != 0)
        {
            BindTeams(int.Parse(ddlCustomers.SelectedValue));
            DisplaySelectedGroups(Convert.ToInt32(Request.QueryString["uid"]));
        }
        else
        {
            BindTeams();
            DisplaySelectedGroups(Convert.ToInt32(Request.QueryString["uid"]));
        }
    }
    private void BindAfterSDClick()
    {
        BindSDTeams();
        DisplaySDSelectedGroups();
    }

    protected void lnkSelectAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < CheckBoxList2.Items.Count; i++)
        {
            if (!CheckBoxList2.Items[i].Selected)
            {
                CheckBoxList2.Items[i].Selected = true;
            }
        }
    }
    protected void lnkSelectSDAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkSDTeam.Items.Count; i++)
        {
            if (!chkSDTeam.Items[i].Selected)
            {
                chkSDTeam.Items[i].Selected = true;
            }
        }
    }
    protected void btngohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Admin/Adminusers.aspx");
    }
    protected void imgSDTeamApply_Click(object sender, EventArgs e)
    {

        PortfolioDataContext portfolio = new PortfolioDataContext();
        UserDataContext users = new UserDataContext();

       
        if (Request.QueryString["uid"] != null)
        {
            var getUser = (from r in users.Contractors
                           where r.ID == int.Parse(Request.QueryString["uid"].ToString())
                           select r).ToList().FirstOrDefault();

            for (int i = 0; i < chkSDTeam.Items.Count; i++)
            {
                if (chkSDTeam.Items[i].Selected)
                {
                    if (getUser != null)
                    {
                        var isExist = (from r in portfolio.SDteamToUsers
                                       where r.UserID == int.Parse(Request.QueryString["uid"].ToString())
                                       && r.SDteamID == int.Parse(chkSDTeam.Items[i].Value.ToString())
                                       select r).ToList();
                        if (isExist != null)
                        {
                            if (isExist.Count == 0)
                            {
                                try
                                {
                                    SDteamToUser teamMembers = new SDteamToUser();
                                    teamMembers.UserID = getUser.ID;
                                    teamMembers.SDteamID = int.Parse(chkSDTeam.Items[i].Value.ToString());
                                    //teamMembers.Email = getUser.EmailAddress;
                                    //teamMembers.ContactNumber = getUser.ContactNumber;
                                    portfolio.SDteamToUsers.InsertOnSubmit(teamMembers);
                                    portfolio.SubmitChanges();
                                }
                                catch (Exception ex)
                                {
                                    LogExceptions.WriteExceptionLog(ex);
                                }

                            }
                        }
                    }
                }
                else
                {
                    if (getUser != null)
                    {
                        var isExist = (from r in portfolio.SDteamToUsers
                                       where r.UserID == int.Parse(Request.QueryString["uid"].ToString())
                                       && r.SDteamID == int.Parse(chkSDTeam.Items[i].Value.ToString())
                                       select r).ToList();
                        if (isExist.Count > 0)
                        {
                            foreach (var detail in isExist)
                            {
                                portfolio.SDteamToUsers.DeleteOnSubmit(detail);
                            }
                            portfolio.SubmitChanges();
                            //if (isExist.ID != 0)
                            //{
                            //    SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text,
                            //        "delete TeamMember where TeamID=@TeamID and Name=@Name",
                            //        new SqlParameter("@TeamID", isExist.TeamID),
                            //        new SqlParameter("@Name", isExist.Name));

                            //}
                        }
                    }
                }
            }


        }
        BindAfterSDClick();
    }
}
