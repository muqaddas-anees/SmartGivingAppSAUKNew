using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Linq;
using AjaxControlToolkit;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

/// <summary>
/// Summary description for TimesheetresourceSection
/// </summary>
public class TimesheetresourceSection
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    DbCommand cmd;
	public TimesheetresourceSection()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string GetProjectOwnermailID(int ProjectOwnerID)
    {
        string ProjectOwnerEMailAddress ="";
        SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select EmailAddress  from Contractors where ID={0}", ProjectOwnerID));
        while (dr.Read())
        {
            ProjectOwnerEMailAddress = dr["EmailAddress"].ToString();

        }
        dr.Close();
        return ProjectOwnerEMailAddress;
    }
    public int GetProjectReference(int WCDATEID,int CONTRACTORID, int ProjectOwnerID)
    {
        int Pref1 = 0;
        SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("SELECT    isnull(TimesheetEntry.ProjectReference,0) as ProjectReference  FROM TimesheetEntry INNER JOIN  Projects ON TimesheetEntry.ProjectReference = Projects.ProjectReference WHERE     (TimesheetEntry.WCDateID = {0}) AND (TimesheetEntry.ContractorID = {1}) AND (TimesheetEntry.ProjectReference <> 0)and Projects.OwnerID={2}",WCDATEID,CONTRACTORID, ProjectOwnerID));
        while (dr.Read())
        {
            Pref1 = Convert.ToInt32(dr["ProjectReference"].ToString());

        }
        dr.Close();
        return Pref1;
    }

    public string GetProjectTitle(int ProjectReference)
    {
        string ProjectTitle = string.Empty;
        ProjectTitle=SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, string.Format("select ProjectTitle from Projects where ProjectReference={0}",ProjectReference)).ToString();
        return ProjectTitle;
    }

    public static DataTable GetProjectListByResource()
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimeSheet_ProjectTile", new SqlParameter("@ContractorID", sessionKeys.UID)).Tables[0];
    }
    public static DataTable GetProjectTaskListByResource(int project)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_GetProjectTaks", new SqlParameter("@ProjectReference", project), new SqlParameter("@contractorID", sessionKeys.UID)).Tables[0];
    }

    public static DataTable GetProjectSitesByProject(int project)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimesheetSite_Customer", new SqlParameter("@ProjectReference", project)).Tables[0];
    }

    #region TimeSheet-Resources Cascading Dropdown

    public static CascadingDropDownNameValue[] Timesheet_GetPortfolios()
    {
        PortfolioDataContext timeSheet = new PortfolioDataContext();
        List<CascadingDropDownNameValue> GetPortfoloio = new List<CascadingDropDownNameValue>();
        DataSet ds = new DataSet();
        try
        {
            GetPortfoloio = (from r in timeSheet.ProjectPortfolios
                             select new CascadingDropDownNameValue { name = r.PortFolio, value = r.ID.ToString() }).ToList();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return GetPortfoloio.ToArray();
    }
    public static CascadingDropDownNameValue[] Timesheet_GetSites(int portfolioID)
    {
        PortfolioDataContext timeSheet = new PortfolioDataContext();
        List<CascadingDropDownNameValue> GetSites = new List<CascadingDropDownNameValue>();
        try
        {
            GetSites = (from r in timeSheet.ProjectPortfolioSite_ByPortfolio(portfolioID)
                        orderby r.site
                        select new CascadingDropDownNameValue { name = r.site, value = r.SiteID.ToString() }).ToList();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return GetSites.ToArray();
    }

    // Get sites by project Reference and Resource  -- Giri

    public static CascadingDropDownNameValue[] Timesheet_GetSitesbyPRef(int ProjectRef)
    {
        PortfolioDataContext timesheet = new PortfolioDataContext();
        List<CascadingDropDownNameValue> GetSites = new List<CascadingDropDownNameValue>();
        try
        {
            GetSites = (from r in timesheet.DN_TimesheetSite_Customer(ProjectRef)
                        orderby r.Site
                        select new CascadingDropDownNameValue { name = r.Site, value = r.SiteID.ToString() }).ToList();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);    
        }
        return GetSites.ToArray();


    }





    public static CascadingDropDownNameValue[] TimeSheet_GetProjectsTitles(int Portfolio)
    {
        projectTaskDataContext timeSheet = new projectTaskDataContext();
        List<CascadingDropDownNameValue> GetProjectsTitles = new List<CascadingDropDownNameValue>();

        DataSet ds = new DataSet();
        try
        {


            GetProjectsTitles = (from r in timeSheet.ProjectDetails
                                 where r.Portfolio == Portfolio
                                 orderby r.ProjectTitle
                                 select new CascadingDropDownNameValue { name = r.ProjectTitle, value = r.ProjectReference.ToString() }).ToList();


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        //CascadingDropDownNameValue[] GetTasks

        return GetProjectsTitles.ToArray();
    }

    //Get the project Title with ProjectReference -- added for TimesheetResourceDaily.aspx --- Giri
    public static CascadingDropDownNameValue[] TimeSheet_GetProjectsTitlesWithReference(int Portfolio)
    {
        //PortfolioDataContext timeSheet = new PortfolioDataContext();
        projectTaskDataContext timeSheet = new projectTaskDataContext();
        List<CascadingDropDownNameValue> GetProjectsTitles = new List<CascadingDropDownNameValue>();
        
        DataSet ds = new DataSet();
        try
        {
            //GetProjectsTitles = (from r in timeSheet.ProjectDetails
            //                     join k in timeSheet.ProjectItems on r.ProjectReference equals k.ProjectReference
            //                     where r.Portfolio == Portfolio && k.ContractorID == sessionKeys.UID
            //                     orderby r.ProjectTitle
            //                     select new CascadingDropDownNameValue { name = r.ProjectReferenceWithPrefix + ' ' + r.ProjectTitle, value = r.ProjectReference.ToString() }).ToList();
           var mylist = (from r in timeSheet.ProjectDetails
                                 join k in timeSheet.ProjectItems on r.ProjectReference equals k.ProjectReference
                                 where  k.ContractorID == sessionKeys.UID
                                 orderby r.ProjectTitle
                                 select new CascadingDropDownNameValue { name = r.ProjectReferenceWithPrefix + ' ' + r.ProjectTitle, value = r.ProjectReference.ToString() }).Distinct();
        
         GetProjectsTitles = mylist.ToList();
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        //CascadingDropDownNameValue[] GetTasks

       return GetProjectsTitles.ToArray();
        //return dbList.ToArray<CascadingDropDownNameValue>();
    }

    public static CascadingDropDownNameValue[] TimeSheet_GetProjectsTasks(int projectRef)
    {
        projectTaskDataContext timeSheet = new projectTaskDataContext();
        List<CascadingDropDownNameValue> GetProjectsTasks = new List<CascadingDropDownNameValue>();

        DataSet ds = new DataSet();
        try
        {


            GetProjectsTasks = (from r in timeSheet.ProjectTaskItems
                                where r.ProjectReference == projectRef 
                                orderby r.ItemDescription
                                select new CascadingDropDownNameValue { name = r.ItemDescription, value = r.ID.ToString() }).ToList();


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        //CascadingDropDownNameValue[] GetTasks

        return GetProjectsTasks.ToArray();
    }
    public static CascadingDropDownNameValue[] TimeSheet_GetProjectsTasksbyPortFolio(int projectRef,int contractorID)
    {
        projectTaskDataContext timeSheet = new projectTaskDataContext();
        List<CascadingDropDownNameValue> GetProjectsTasks = new List<CascadingDropDownNameValue>();

        DataSet ds = new DataSet();
        try
        {


            GetProjectsTasks = (from r in timeSheet.ProjectTaskItems
                                join k in timeSheet.ProjectItems on r.ID equals k.ItemReference
                                where k.ProjectReference == projectRef && k.ContractorID == contractorID
                                 orderby r.ItemDescription
                                 select new CascadingDropDownNameValue { name = r.ItemDescription, value = r.ID.ToString() }).Distinct().ToList();


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        //CascadingDropDownNameValue[] GetTasks
       
        return GetProjectsTasks.ToArray();
    }

    public static CascadingDropDownNameValue[] TimeSheet_GetEntryTypes()
    {
        TimeSheetDataContext timeSheet = new TimeSheetDataContext();
        List<CascadingDropDownNameValue> GetEntryTypes = new List<CascadingDropDownNameValue>();

        DataSet ds = new DataSet();
        try
        {


            GetEntryTypes = (from r in timeSheet.TimesheetEntryTypes
                              
                                select new CascadingDropDownNameValue { name = r.EntryType,value = r.ID.ToString() }).ToList();


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        //CascadingDropDownNameValue[] GetTasks

        return GetEntryTypes.ToArray();
    }


    public static CascadingDropDownNameValue[] TimeSheet_GetSitesByProjectRef(int projectRef)
    {
        TimeSheetDataContext timeSheet = new TimeSheetDataContext();
         PortfolioDataContext portFolio = new PortfolioDataContext();
        List<CascadingDropDownNameValue> GetSitesByProjectRef = new List<CascadingDropDownNameValue>();

        DataSet ds = new DataSet();
        try
        {

            if (projectRef > 0)
            {
                var pd = from r in portFolio.ProjectDetails
                         where r.ProjectReference == projectRef
                         select r;
                int id = 0;
                foreach (ProjectDetail i in pd)
                {
                    id = int.Parse(i.Portfolio.ToString());
                }
                GetSitesByProjectRef = (from r in portFolio.ProjectPortfolioSite_ByPortfolio(id)
                                        orderby r.site
                                        select new CascadingDropDownNameValue { name = r.site, value = r.SiteID.ToString() }).ToList();

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        //CascadingDropDownNameValue[] GetTasks

        return GetSitesByProjectRef.ToArray();
    }

    public static CascadingDropDownNameValue[] TimeSheet_GetAllProjectRef()
    {
       // TimeSheetDataContext timeSheet = new TimeSheetDataContext();
        projectTaskDataContext portFolio = new projectTaskDataContext();
         List<CascadingDropDownNameValue> GetAllProjectRef = new List<CascadingDropDownNameValue>();

        DataSet ds = new DataSet();
        try
        {


            GetAllProjectRef = (from r in portFolio.ProjectDetails
                                 orderby r.ProjectTitle
                                 select new CascadingDropDownNameValue { name = r.ProjectTitle, value = r.ProjectReference.ToString() }).ToList();


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        //CascadingDropDownNameValue[] GetTasks

        return GetAllProjectRef.ToArray();
    }

    public static CascadingDropDownNameValue[] ProjectsByResource_CaseCade()
    {
        DataTable dt = GetProjectListByResource();
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        foreach (DataRow dr in dt.Rows)
        {
            values.Add(new CascadingDropDownNameValue((string)dr["ProjectTitle"], dr["ProjectReference"].ToString()));
        }
        return values.ToArray();
    }
    public static CascadingDropDownNameValue[] ProjectTasksByResource_CaseCade(int ProjectReference)
    {
        DataTable dt = GetProjectTaskListByResource(ProjectReference);
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        foreach (DataRow dr in dt.Rows)
        {
            if (int.Parse(dr["TaskID"].ToString()) > 0)
            {
                values.Add(new CascadingDropDownNameValue((string)dr["ProjectTask"], dr["TaskID"].ToString()));
            }
        }
        return values.ToArray();
    }
    public static CascadingDropDownNameValue[] SiteByProject_CaseCade(int ProjectReference)
    {
        DataTable dt = GetProjectSitesByProject(ProjectReference);
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        foreach (DataRow dr in dt.Rows)
        {
            values.Add(new CascadingDropDownNameValue((string)dr["Site"], dr["SiteID"].ToString()));
        }
        return values.ToArray();
    }
    #endregion
 public static CascadingDropDownNameValue[] TimeSheet_GetAllProjectRef(int Portfolio)
    {
        //TimeSheetDataContext timeSheet = new TimeSheetDataContext();
        projectTaskDataContext portFolio = new projectTaskDataContext();
         List<CascadingDropDownNameValue> GetAllProjectRef = new List<CascadingDropDownNameValue>();

        DataSet ds = new DataSet();
        try
        {


            GetAllProjectRef = (from r in portFolio.ProjectDetails
                                where r.Portfolio==Portfolio
                                 orderby r.ProjectReference
                                select new CascadingDropDownNameValue { name = r.ProjectReferenceWithPrefix + "-" + r.ProjectTitle, value = r.ProjectReference.ToString() }).ToList();


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        //CascadingDropDownNameValue[] GetTasks

        return GetAllProjectRef.ToArray();
    }
 public static CascadingDropDownNameValue[] ProjectsByPortfolio_CasCade(int portfolio)
 {
     DataTable dt = GetProjectByPortfolio(portfolio);
     List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
     foreach (DataRow dr in dt.Rows)
     {
         values.Add(new CascadingDropDownNameValue((string)dr["ProjectTitle"], dr["ProjectReference"].ToString()));
     }
     return values.ToArray();
 }

 public static DataTable GetProjectByPortfolio(int Portfolio)
 {
     return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure,
         "DN_TimeSheet_ProjectTitle", new SqlParameter("@ContractorID", sessionKeys.UID), new SqlParameter("@Portfolio", Portfolio)).Tables[0];
 }

}
