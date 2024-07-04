using System;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Class1
{
    //public static SqlDataReader SelectTeamData(int PortfolioID)
    //{
    //    SqlDataReader dr;
    //    dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Deffinity_TeamAndTeamMembers_Level", new SqlParameter("@PortfolioID", PortfolioID));
    //    return dr;
    //}

    public static DataSet SelectTeamData(int PortfolioID)
    {
        DataSet ds;
        ds = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_TeamAndTeamMembers_Level", new SqlParameter("@PortfolioID", PortfolioID));
        return ds;
    }

    public static SqlDataReader SelectShift(int PortfolioID)
    {
        SqlDataReader dr;

        dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, "", new SqlParameter("@PortfolioID", PortfolioID));
        return dr;
    }
}
