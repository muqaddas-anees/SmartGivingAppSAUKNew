using System;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using System.Web;



/// <summary>
/// Summary description for ResourcePlanner
/// </summary>
public class ResourcePlanner
{
    
    public static DataSet SelectTeamData(int PortfolioID)
    {
        
       string key = string.Format("{0}_{1}", CacheNames.DefaultNames.ResourcePlanner, sessionKeys.PortfolioID);
        
        try
        {

            if (BaseCache.Cache_Select(key) == null)
            {
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_TeamAndTeamMembers_Level", new SqlParameter("@PortfolioID", PortfolioID));
                BaseCache.Cache_Insert(key, ds);
            }
        
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return BaseCache.Cache_Select(key) as DataSet;
    }

    public static DataSet SelectTeamData(int PortfolioID,string startdate,string enddate,int displaytype)
    {
        DataSet ds = new DataSet();
        //string key = string.Format("{0}_{1}", CacheNames.DefaultNames.ResourcePlanner, sessionKeys.PortfolioID);

        try
        {

            //if (BaseCache.Cache_Select(key) == null)
            //{
            //    DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_TeamAndTeamMembers_Level", new SqlParameter("@PortfolioID", PortfolioID), new SqlParameter("@startdate", Convert.ToDateTime(string.IsNullOrEmpty(startdate) ? "01/01/1900" : startdate)), new SqlParameter("@enddate", Convert.ToDateTime(string.IsNullOrEmpty(enddate) ? "01/01/1900" : enddate)), new SqlParameter("@DisplayType", displaytype));
            //    BaseCache.Cache_Insert(key, ds);
            //}

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ds;
    }

    public static SqlDataReader SelectShift(int PortfolioID)
    {
        SqlDataReader dr;
        dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, "", new SqlParameter("@PortfolioID", PortfolioID));
        return dr;
    }

    //inset or update member_shift table
    public void Membershift_InsertOrUpdate(int MemberID,int ShiftID,DateTime FromDate,int SiteID,int TeamType,string Notes,DateTime Todate,int ID)
    {
        string key = string.Format("{0}_{1}", CacheNames.DefaultNames.ResourcePlanner, sessionKeys.PortfolioID);
        try
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_MemberShift_InsertUpdate",
                new SqlParameter("@MemberID", MemberID), new SqlParameter("@ShiftID", ShiftID), new SqlParameter("@FromDate", FromDate)
                , new SqlParameter("@SiteID", SiteID),
                new SqlParameter("@TeamType", TeamType), new SqlParameter("@Notes", Notes), new SqlParameter("@Todate", Todate), new SqlParameter("@ID", ID));

            BaseCache.Cache_Remove(key);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, FromDate.ToString()); 
        }
        
    }
    public static void Membershift_Delete( int MemeberShiftID)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "Delete from Member_shift where ID = @ID",
                new SqlParameter("@ID", MemeberShiftID));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    public static DataTable ShiftByPortfolio(int portfolio)
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ShiftColorDisplay", new SqlParameter("@portfolioid", portfolio)).Tables[0];
        return dt;
    }

}
