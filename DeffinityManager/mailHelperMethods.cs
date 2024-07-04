using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

/// <summary>
/// Summary description for mailHelperMethods
/// </summary>
public class projectDetails
{
	public projectDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string[] getData(int projectReference,string st_proc)
    {
        Database database;
        DbCommand dbCmd;
        instanceDataBase.createDatabase(out database, out dbCmd, st_proc);
                switch (st_proc)
        {
            case "getProjectDetailsForMail":
                addSelectParametersProjectDetails(database, dbCmd, projectReference);
                break;
            case "getMitigationActionDetailsForMail":
                addSelectParametersMitigationDetails(database, dbCmd, projectReference);
                break;
            case "getVariationRaisedDetailsForMail":
                addSelectParametersVariationRaised(database, dbCmd, projectReference);
                break;
            case "getQAandUATIssueDetailsForMail":
                addSelectParametersQAandUATIssuesRaised(database, dbCmd, projectReference);
                break;
        }
        string[] dataValues;
        dataValues = readDataValues(database, dbCmd);
        return dataValues;
    }

    
    
    int id=0;

    private int ID
    {
        set {id=value; }
        get { return id; }
    }

    public string[] getData(int projectReference, string st_proc, int id)
    {
        ID = 0;
        ID = id;
        return getData(projectReference, st_proc);
    }
    public string[,] getLiveProjectDatabyContracotor(DataSet _ds, int Contractor)
    {
        string[,] dataValues;
        DataRow[] _dr1= _ds.Tables[0].Select("ContractorID=" + Contractor.ToString());
        int kk = _dr1.Length;
        dataValues = new string[kk, 3];
        int _row = 0;
        foreach (DataRow _dr in _dr1)//_ds.Tables[0].Select("ContractorID=" + Contractor.ToString()))
        {
            for (int _col = 0; _col < 3; _col++)
            {
                dataValues[_row, _col] = _dr[_col].ToString();
            }
            _row++;
        }
        return dataValues;
    }
    public string[,] getLiveProjectData(int projectReference, string st_proc)
    {
        Database database;
        DbCommand dbCmd;
        instanceDataBase.createDatabase(out database, out dbCmd, st_proc);
        addSelectParametersLiveProjectDetails(database, dbCmd, projectReference);
        string[,] dataValues;
        dataValues = readLiveProjectDataValues(database, dbCmd,1);
        return dataValues;
    }
    public DataSet getLiveProjectData(int projectReference)
    {
        Database database;
        DbCommand dbCmd;
        instanceDataBase.createDatabase(out database, out dbCmd, "getLiveProjectDetailsForMail");
        addSelectParametersLiveProjectDetails(database, dbCmd, projectReference);
        DataSet _ds = database.ExecuteDataSet(dbCmd);
        return _ds;
    }

    private string[,] readLiveProjectDataValues(Database database, DbCommand dbCmd, int contID)
    {
        string[,] dataValues;
        //int recordCount = 0;
        DataSet _ds = database.ExecuteDataSet(dbCmd);
        int i = _ds.Tables[0].Rows.Count;
        DataRow[] _dr = _ds.Tables[0].Select("ContractorID=1");
        int j = _dr.Length;
        dataValues = new string[0, 0];
        return dataValues;
    }
    private string[,] readLiveProjectDataValues(Database database, DbCommand dbCmd)
    {
        string[,] dataValues;
        int recordCount = 0;
        using (IDataReader datareader = database.ExecuteReader(dbCmd))
        {
            while (datareader.Read())
            {
                recordCount++;
            }
            dataValues = new string[recordCount, datareader.FieldCount];
        }
        int rows = 0;
        using(IDataReader datareader=database.ExecuteReader(dbCmd))
        {
            while (datareader.Read())
            {
                for (int columns = 0; columns < datareader.FieldCount; columns++)
                {
                    dataValues[rows,columns] = datareader[columns].ToString();
                }
                rows++;
            }
        }
        return dataValues;
    }
  
    private string[] readDataValues(Database database, DbCommand dbCmd)
    {
        string[] dataValues;
        using (IDataReader datareader = database.ExecuteReader(dbCmd))
        {
            dataValues = new string[datareader.FieldCount];
            while (datareader.Read())
            {
                for (int index = 0; index < dataValues.Length; index++)
                {
                    dataValues[index] = datareader[index].ToString();
                }
            }
        }
        return dataValues;
    }

    private void addSelectParametersProjectDetails(Database database, DbCommand dbCmd, int parameterValue)
    {
        database.AddInParameter(dbCmd, "ProjectReference", DbType.Int32,parameterValue);
    }

    private void addSelectParametersLiveProjectDetails(Database database, DbCommand dbCmd, int parameterValue)
    {
        database.AddInParameter(dbCmd, "ProjectReference", DbType.Int32, parameterValue);
    }

    private void addSelectParametersMitigationDetails(Database database, DbCommand dbCmd, int parameterValue)
    {
        database.AddInParameter(dbCmd, "ProjectReference", DbType.Int32, parameterValue);
    }

    private void addSelectParametersVariationRaised(Database database,DbCommand dbCmd,int projectReference)
    {
        if (ID == 0)
            throw new moreColumnsException("Please remove the ID column");
        database.AddInParameter(dbCmd, "ProjectReference", DbType.Int32, projectReference);
        database.AddInParameter(dbCmd, "Id", DbType.Int32, ID);
    }

    private void addSelectParametersQAandUATIssuesRaised(Database database, DbCommand dbCmd, int projectReference)
    {
        if (ID == 0)
            throw new moreColumnsException("Please remove the ID column");
        database.AddInParameter(dbCmd, "ProjectReference", DbType.Int32, projectReference);
        database.AddInParameter(dbCmd, "Id", DbType.Int32, ID);
    }
}
