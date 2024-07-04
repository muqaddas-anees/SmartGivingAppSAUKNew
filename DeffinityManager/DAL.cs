using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Web;
using System;


/// <summary>
/// Summary description for DAL
/// </summary>
/// 
public class connectionString
{
    public connectionString()
    {

    }

    public static string retrieveConnString()
    {
        return ConfigurationManager.ConnectionStrings["DBstring"].ConnectionString;
    }
}

public class instanceDataBase
{

    public static void createDatabase(out Database database, out DbCommand dbcmd, string procedureName)
    {
        try
        {
            database = DatabaseFactory.CreateDatabase("DBstring") as Database;
            dbcmd = database.GetStoredProcCommand(procedureName);
        }
        catch
        {
            databaseConnectionException dbConnException = new databaseConnectionException("Connection to the database failed");
            throw dbConnException;
        }
    }
}

public partial class DataHelperClass
{

    public static void DDLHelper(DataTable table, string sqlQuery)
    {
        using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        {
            using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    table.Load(reader);
                    conn.Close();
                }
            }
        }
    }

    /// <summary>
    /// Gets the data for filling the drop down list items.
    /// This method was developed to provide the ObjectDataSource control
    /// </summary>
    /// <returns>
    /// Data table with ID and Portfolio fields from the ProjectPortfolio table.
    /// </returns>
    public static DataTable LoadPortfolio()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "Select ID,Portfolio FROM ProjectPortfolio");
        return table;
    }
    public static DataTable LoadProjectsByPortfolio()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "Select ProjectReference as Pref,projectTitle as Title from Projects where portfolio=" + sessionKeys.PortfolioID);
        return table;
    }

    public static DataTable LoadDistributionMailIDs()
    {
        if (HttpContext.Current.Cache["MailList"] == null)
        {
            int PID = 0;
            if (HttpContext.Current.Request.QueryString["PID"] != null)
                PID = Convert.ToInt32(HttpContext.Current.Request.QueryString["PID"]);
            HttpContext.Current.Cache["MailList"] = DataHelperClass.GenericSelectMethodHelp("SELECT Distinct Mail.ID AS ID,Mail.Name AS Name,Mail.EmailID AS EmailID," +
                "ISNULL(HCL_Mail.MailID,0) AS Assigned " +
                "FROM healthchecknamemailid Mail Left Outer Join healthchecklist_healthchecknamemailid HCL_Mail " +
                "ON Mail.ID=HCL_Mail.MailID WHERE ([PortfolioHealthCheckID] = " + PID + ")");
        }
        return (DataTable) HttpContext.Current.Cache["MailList"];
    }

    public static DataTable GetTeamDetailsForHealthCheckAssignedTeam(int healthCheckID)
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT * FROM TeamMember where TEAMID=(SELECT AssignedTeam FROM HealthCheckList WHERE ID=" + healthCheckID.ToString() + ");");
        return table;
    }

    public static DataTable GenericSelectMethodHelp(string sqlStatement)
    {
        DataTable table = new DataTable();
        DDLHelper(table,sqlStatement);
        return table;
    }

    /// <summary>
    /// Gets the data for filling the drop down list items.
    /// This method was developed to provide the ObjectDataSource control
    /// </summary>
    /// <returns>
    /// Data table with ID and Department fields from the PortfolioDepartment table.
    /// </returns>
    public static DataTable LoadPortfolioDepartment()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "Select ID,DeptName FROM PortfolioDepartment");
        return table;
    }

   /// <summary>
   /// Gets the Sites for ddl
   /// </summary>
   /// <returns></returns>

    public static DataTable LoadSite()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,Site FROM Site WHERE Visible='Y' Order BY Site");
        return table;
    }

    public static DataTable LoadPortfolioHealthCheckTitle()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,Title FROM PortfolioHealthCheck Where PortfolioID=" + sessionKeys.PortfolioID);
        return table;
    }
    public static DataTable LoadPortfolioHealthCheckTitle(int PortfolioID)
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,Title FROM PortfolioHealthCheck Where PortfolioID=" + PortfolioID);
        return table;
    }

    /// <summary>
    /// Gets the Project Category for DDL
    /// </summary>
    /// <returns></returns>
    public static DataTable LoadProjectCategory()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT CategoryID,CategoryName from ProjectCategory");
        return table;
    }

    /// <summary>
    /// Gets the all the contractors with id and email
    /// </summary>
    /// <returns></returns>
    public static DataTable LoadContractorMailID()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,EmailAddress From Contractors");
        return table;
    }

    /// <summary>
    /// Gets the Resource(Contractors where SID=4) for DDL
    /// </summary>
    /// <returns></returns>
    public static DataTable LoadResource()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,ContractorName AS Name,EmailAddress AS Email from Contractors where SID=4 Order By ContractorName");
        return table;
    }
 public static DataTable LoadApprover()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,ContractorName AS Name,EmailAddress AS Email from Contractors where SID IN (1,2,3) Order By ContractorName");
        return table;
    }

    public static DataTable LoadAllContractors()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,ContractorName AS Name,EmailAddress AS Email from Contractors Order By ContractorName");
        return table;
    }
    /// <summary>
    /// Gets the Resource(Team Members) by the team for DDL
    /// </summary>
    /// <returns></returns>
    public static DataTable LoadResourceByTeam(int teamID)
    {
        DataTable table = new DataTable();
        DDLHelper(table, "select id, contractorname AS Name,EmailAddress AS Email from contractors where id in(select name from teammember where teamid= " + teamID.ToString() + ") order by contractorname");
        return table;
    }
    /// <summary>
    /// Gets the Resource(Team Members) by the team for DDL
    /// </summary>
    /// <returns></returns>
    public static DataTable LoadSDResourceByTeam(int teamID)
    {
        DataTable table = new DataTable();
        DDLHelper(table, "select id, contractorname AS Name,EmailAddress AS Email from contractors where id in(select userID from SDteamToUsers where SDteamid= " + teamID.ToString() + ") order by contractorname");
        return table;
    }
    public static DataTable LoadResourceByPortfolio()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "Select ID,ContractorName AS Name,EmailAddress AS Email from Contractors where ID in (SELECT distinct TeamMember.Name as ID from TeamMember INNER JOIN Team ON TeamMember.TeamID = Team.ID where Team.PortfolioID =" + sessionKeys.PortfolioID.ToString() + ") order by contractorname");
        return table;
    }

    /// <summary>
    /// Gets the Team Names for DDL
    /// </summary>
    /// <returns></returns>
    public static DataTable LoadTeams()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,TeamName FROM TEAM");
        return table;
    }

    //Load teams related to portfolio
    public static DataTable LoadTeamsByPortfolio()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,TeamName FROM TEAM WHERE PortfolioID=" + sessionKeys.PortfolioID.ToString() );
        return table;
    }
    public static DataTable LoadTeamsByPortfolio(int portfolioID)
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,TeamName FROM TEAM WHERE PortfolioID=" + portfolioID.ToString());
        return table;
    }
    //Load teams related to portfolio
    public static DataTable LoadSDTeamsByPortfolio()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,TeamName FROM SDteam WHERE PortfolioID=" + sessionKeys.PortfolioID.ToString());
        return table;
    }

    
    //Load teams related to portfolio
    public static DataTable LoadAllTeamsByPortfolio()
    {
        DataSet dsTeams =Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString.retrieveConnString(),CommandType.StoredProcedure,"DEFFINITY_GETALLTEAMS",new SqlParameter[]{ new SqlParameter("@PORTFOLIOID", sessionKeys.PortfolioID) });
        return dsTeams.Tables[0];
    }


    /// <summary>
    /// Gets all the portfolioSLA Services
    /// </summary>
    /// <returns></returns>
    public static DataTable LoadServices()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,ServiceDescription FROM Service");
        return table;
    }

    public static DataTable LoadTeamByUser()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT TEAMID FROM TeamMember Where Name=" + sessionKeys.UID.ToString());
        return table;
    }

    /// <summary>
    /// Gets all the portfolioSLA Services for Incident Services
    /// </summary>
    /// <returns></returns>
    public static DataTable LoadServicesForIncidentServices()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,ServiceDescription FROM Service Where PortfolioID=" + sessionKeys.PortfolioID.ToString());
        return table;
    }


    public static DataTable LoadSiteByPortfolio()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT SiteID AS ID,(SELECT SITE FROM SITE WHERE ID=AssignedSitesToPortfolio.SiteID) AS Site "+
                        "FROM AssignedSitesToPortfolio WHERE Portfolio=" +sessionKeys.PortfolioID.ToString());
        return table;
    }

    public static DataTable LoadSiteByPortfolio(int portfolioID)
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT SiteID AS ID,(SELECT SITE FROM SITE WHERE ID=AssignedSitesToPortfolio.SiteID) AS Site " +
                        "FROM AssignedSitesToPortfolio WHERE Portfolio=" + portfolioID);
        return table;
    }

    public static DataTable LoadDepartmentByPortfolio()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID, DeptName FROM PortfolioDepartments WHERE PortfolioID=" + sessionKeys.PortfolioID.ToString());
        return table;
    }

    public static DataTable LoadCustomFieldsFromDefaults()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "select top 1 * from projectdefaults");
        return table;
    }

    public static DataTable LoadCheckLists()
    {
        DataTable table = new DataTable();
        DDLHelper(table, "SELECT ID,Description FROM MASTERTEMPLATE where CHecklistType =2 ORDER BY Description");
        return table;
    }

    /// <summary>
    /// Gets the Priority for DDL
    /// </summary>
    /// <returns></returns>
    public static List<string> LoadPriority()
    {
        List<string> listStatus = new List<string>();
        listStatus.Add("Please select...");
        listStatus.Add("High");
        listStatus.Add("Medium");
        listStatus.Add("Low");
        return listStatus;
    }

    /// <summary>
    /// Gets the Incident Status for DDL
    /// </summary>
    /// <returns></returns>
    public static List<string> LoadIncidentStatus()
    {
        List<string> listStatus = new List<string>();
        listStatus.Add("Please select...");
        listStatus.Add("New Incident");
        listStatus.Add("On-Hold");
        listStatus.Add("Open");
        listStatus.Add("Closed");
        return listStatus;
    }
    public static DataTable Category_SelectAll(string incidentType,int MasterCategoryID)
    {
        DataTable table = new DataTable();
        DDLHelper(table, "select  count(*) as no,(select CategoryName from Projectcategory_Maser where " +
            "  ID =c.CategoryID) as Category from ChangeControl c where  status = 'Closed' and CategoryID= " + MasterCategoryID.ToString() +
       " and c.CategoryID>0 group by CategoryID  order by CategoryID ");
        return table;
    }
}






