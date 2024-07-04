using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlTypes;

/// <summary>
/// Summary description for ReportHelperMethods
/// </summary>
public static class ReportHelperMethods
{
	static ReportHelperMethods()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static DateTime _fromDate;
    private static DateTime _toDate;

    private static DateTime FromDate
    {
        get { return _fromDate; }
        set { _fromDate = value; }
    }

    private static DateTime ToDate
    {
        get { return _toDate; }
        set { _toDate = value; }
    }

    public static ReportDocument BindReport(string reportName,out ReportDocument rpt,DateTime fromDate,DateTime toDate)
    {
        FromDate = fromDate;
        ToDate = toDate;
        return BindReport(reportName,0, out rpt);
    }

    public static ReportDocument BindReport(string reportName,int M_Number,out ReportDocument rpt)
    {

        rpt = new ReportDocument();

        DataTable reportTable = new DataTable();

        switch (reportName)
        { 
            case "fromto_sheet.rpt":
                getData("iMAC_RPTFromToSheet", ref reportTable, M_Number);
                break;
            case "fromto_summary.rpt":
                getData("iMAC_RPTFromToSummary", ref reportTable, M_Number);
                break;
            case "desktop_fromto_summary.rpt":
                getData("iMAC_RPTDesktopFromToSummary",ref reportTable,M_Number);
                break;
            case "voice_fromto_summary.rpt":
                getData("iMAC_RPTVoiceFromToSummary", ref reportTable, M_Number);
                break;
            case "voice_patching_summary.rpt":
                getData("iMAC_RPTVoicePatchingSummary", ref reportTable, M_Number);
                break;
            case "lan_patching_summary.rpt":
                getData("iMAC_RPTLanPatchingSummary", ref reportTable, M_Number);
                break;
            case "voice_and_lan_patching_summary.rpt":
                getData("iMAC_RPTVoiceAndLanPatchingSummary", ref reportTable, M_Number);
                break;
            case "desk_sheet.rpt":
                getData("iMAC_RPTDeskSheet", ref reportTable, M_Number);
                break;
            case "asset_sheet.rpt":
                getData("iMAC_RPTAssetSheet", ref reportTable, M_Number);
                break;
            case "camms_moves_total.rpt":
                getData("iMAC_RPTCammsMovesTotal", ref reportTable,FromDate,ToDate);
                break;
            case "equipment_moves_total.rpt":
                getData("iMAC_RPTEquipmentMovesTotal", ref reportTable,FromDate,ToDate);
                break;
        }

        //Load the selected report file.

        rpt.Load(HttpContext.Current.Server.MapPath(reportName));
        rpt.SetDataSource(reportTable);
        return rpt;
    }

    //Load the data from database to the dataset and return the datatable object.

    private static DataTable getData(string procedureName,ref DataTable reportTable,int M_Number)
    {
        Microsoft.Practices.EnterpriseLibrary.Data.Database database = DatabaseFactory.CreateDatabase("2iMacdbConnection") as Microsoft.Practices.EnterpriseLibrary.Data.Database;
        DbCommand dbcmd = database.GetStoredProcCommand(procedureName);
        addSelectParameters(database, dbcmd, M_Number);
        DataSet dataset = new DataSet();
        dataset = database.ExecuteDataSet(dbcmd);
        reportTable = dataset.Tables[0];
        dataset.Tables.Clear();
        dataset.Dispose();
        return reportTable;
    }

    private static DataTable getData(string procedureName, ref DataTable reportTable,DateTime fromDate,DateTime toDate)
    {
        
        Microsoft.Practices.EnterpriseLibrary.Data.Database database = DatabaseFactory.CreateDatabase("2iMacdbConnection") as Microsoft.Practices.EnterpriseLibrary.Data.Database;
        DbCommand dbcmd = database.GetStoredProcCommand(procedureName);
        addSelectParameters(database, dbcmd, fromDate, toDate);
        DataSet dataset = new DataSet();
        dataset = database.ExecuteDataSet(dbcmd);
        reportTable = dataset.Tables[0];
        dataset.Tables.Clear();
        dataset.Dispose();
        return reportTable;

    }

    private static void addSelectParameters(Microsoft.Practices.EnterpriseLibrary.Data.Database database, DbCommand dbCmd, DateTime fromDate,DateTime toDate)
    {
        database.AddInParameter(dbCmd, "FromDate", DbType.DateTime, fromDate);
        database.AddInParameter(dbCmd, "ToDate", DbType.DateTime, toDate);
    }

    //Over loaded method for the getData. This method is to be called when there are no parameters.

    private static DataTable getData(string procedureName, ref DataTable reportTable)
    {
        Microsoft.Practices.EnterpriseLibrary.Data.Database database = DatabaseFactory.CreateDatabase("2iMacdbConnection") as Microsoft.Practices.EnterpriseLibrary.Data.Database;
        DbCommand dbcmd = database.GetStoredProcCommand(procedureName);
        DataSet dataset = new DataSet();
        dataset = database.ExecuteDataSet(dbcmd);
        reportTable = dataset.Tables[0];
        dataset.Tables.Clear();
        dataset.Dispose();
        return reportTable;
    }



    private static void addSelectParameters(Microsoft.Practices.EnterpriseLibrary.Data.Database database, DbCommand dbCmd,int parameterValue)
    {
        database.AddInParameter(dbCmd, "M_Number", DbType.Int32, parameterValue);
    }
}
