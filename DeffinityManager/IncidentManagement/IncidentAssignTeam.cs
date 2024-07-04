using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Collections;


/// <summary>
/// Summary description for IncidentAssignTeam
/// </summary>

public class IncidentAssignTeam
{
	public IncidentAssignTeam()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void IncidentAssignTeam_Insert(int IncidentID, int TeamID, int AssignTo,string type)
    {
        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_AssignedTeam_Insert",
        new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@TeamID", TeamID), new SqlParameter("@AssignTo", AssignTo), new SqlParameter("@Type",type));
    
    }
    public void IncidentAssignTeam_Update(int ID, int IncidentID, int TeamID, int AssigneTo, string RequiredDate, string ScheduledDate, string Notes, bool Required)
    {
        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_AssignedTeam_Update", new SqlParameter("@ID", ID)
        , new SqlParameter("@IncidentID", IncidentID)
        , new SqlParameter("@TeamID", TeamID)
        , new SqlParameter("@AssigneTo", AssigneTo)
        , new SqlParameter("@RequiredDate", Convert.ToDateTime(string.IsNullOrEmpty(RequiredDate) ? "01/01/1900" : RequiredDate))
        , new SqlParameter("@ScheduledDate", Convert.ToDateTime(string.IsNullOrEmpty(ScheduledDate) ? "01/01/1900" : ScheduledDate))
        , new SqlParameter("@Notes", Notes)
        , new SqlParameter("@Required", Required)
       
        );

    }    

    public void IncidentAssignTeam_Delete(int ID)
    {
        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_AssignedTeam_Delete", new SqlParameter("@ID", ID));
    }
    public DataTable IncidentAssignTeam_SelectAll(int IncidentID,string type)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Incident_AssignedTeam_SelectAll", new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@Type", type)).Tables[0];
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
            return dt;
    }

    public DataTable SDTeams_SelectAll(int PortfolioID)
    {
        DataTable dt = new DataTable();
         try
        {
            dt = Deffinity.SDTeam_Manager.SDTeam.Method_SelectTeam(PortfolioID,string.Empty);
            //SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("select ID,TeamName from SDTeam where PortfolioID="+sessionKeys.PortfolioID+" order by TeamName").Tables[0];
        }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
             return dt;
    }
    public DataTable SDTeams_SelectAll(int PortfolioID,int AreaID)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = Deffinity.SDTeam_Manager.SDTeam.Method_SelectTeam(PortfolioID, AreaID.ToString());
            //SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("select ID,TeamName from SDTeam where PortfolioID="+sessionKeys.PortfolioID+" order by TeamName").Tables[0];
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return dt;
    }
    /// <summary>
    /// Assign team member email address checked with distinct property
    /// </summary>
    /// <param name="IncidentID"></param>
    /// <returns>Array list</returns>
    public ArrayList GetMailIDs(int IncidentID)
    {
        ArrayList ar = new ArrayList();
        SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, "select distinct (select EmailAddress from Contractors where ID = Incident_AssignedTeam.AssigneTo) as EmailID  from Incident_AssignedTeam where IncidentID = @IncidentID", new SqlParameter("@IncidentID", IncidentID));
        while (dr.Read())
        {
            ar.Add(dr["EmailID"].ToString());
        }
        dr.Close();
        return ar;
    }
    public ArrayList GetTeamMailIDs(int TeamID,int AreaID)
    {
        ArrayList ar = new ArrayList();
        try
        {

            if (TeamID > 0)
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, "select (select EmailAddress from Contractors where ID = UserID) as EmailID  from SDteamToUsers where SDTeamID = @TeamID", new SqlParameter("@TeamID", TeamID));
                try
                {
                    while (dr.Read())
                    {
                        ar.Add(dr["EmailID"].ToString());
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dr.Close();
                }
            }
            else
            {
                SqlDataReader dr1 = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, "select (select EmailAddress from Contractors where ID = UserID) as EmailID  from SDteamToUsers where SDTeamID in (select ID from SDTeam where AreaID = @AreaID)", new SqlParameter("@AreaID", AreaID));
                try
                {
                    while (dr1.Read())
                    {
                        ar.Add(dr1["EmailID"].ToString());
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dr1.Close();
                }
            }
                //select (select EmailAddress from Contractors where ID = UserID) as EmailID  from SDteamToUsers where SDTeamID in (select ID from SDTeam where AreaID = @AreaID)
           
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
        return ar;
    }
    //check this the

    public bool GetRequired_Confirmation(int IncidentID)
    {
        bool RetVal = false;
        int _temp = 0;
        _temp = int.Parse(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select count(Required) from Incident_AssignedTeam where Required = 1 and IncidentId =@IncidentID", new SqlParameter("@IncidentID", IncidentID)).ToString());

        RetVal = (_temp == 0 ? false : true);
        
        return RetVal;
    }

    public static DataTable GetTeamMembers(int TeamID)
    {
        return Deffinity.Bindings.DefaultDatabind.AddSelectRow("ID", "ContractorName", SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT c.ID, c.ContractorName FROM SDTeamToUsers t INNER JOIN Contractors c ON t.UserID = c.ID where t.SDTeamID = @TeamID order by c.ContractorName", new SqlParameter("@TeamID", TeamID)).Tables[0], 2);         
        
    }
    //public void IncidentAssignTeam_Insert(int IncidentID, int TeamID, int AssigneTo, string RequiredDate, string ScheduledDate, string Notes, bool Required)
    //{
    //    SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_AssignedTeam_Insert", new SqlParameter("@IncidentID", IncidentID)
    //    , new SqlParameter("@TeamID", TeamID)
    //    , new SqlParameter("@AssigneTo", AssigneTo)
    //    , new SqlParameter("@RequiredDate", Convert.ToDateTime(string.IsNullOrEmpty(RequiredDate) ? "01/01/1900" : RequiredDate))
    //    , new SqlParameter("@ScheduledDate", Convert.ToDateTime(string.IsNullOrEmpty(ScheduledDate) ? "01/01/1900" : ScheduledDate))
    //    , new SqlParameter("@Notes", Notes)
    //    , new SqlParameter("@Required", Required)
    //    );

    //}
}
