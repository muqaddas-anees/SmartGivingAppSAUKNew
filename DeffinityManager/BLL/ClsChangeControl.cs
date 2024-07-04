using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using DeffinityManager.DBChangeControlTableAdapters;
using DeffinityManager;
using Incidents.Entity;
using System.Data;
using DeffinityManager.DAL.DBChangeControlTableAdapters;
using DeffinityManager.DAL;



/// <summary>
/// Summary description for ClsChangeControl
/// </summary>
/// 

public class ClsChangeControl
{
    #region Change_Control
    public ClsChangeControl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private static dtUserEmailAddressTableAdapter _objadaUserEmailAddress;
    public static dtUserEmailAddressTableAdapter objadaUserEmailAddress
    {
        get
        {
            if (_objadaUserEmailAddress == null)
                _objadaUserEmailAddress = new dtUserEmailAddressTableAdapter();
            return _objadaUserEmailAddress;

        }

    }

  
    public DataTable UserEmailID(int UID)
    {
        DBChangeControl.dtUserEmailAddressDataTable dtTab = objadaUserEmailAddress.GetData(UID);
        return dtTab;
    }
    private static dtChangeControlTableAdapter _objadaChangeControl;
    public static dtChangeControlTableAdapter objadaChangeControl
    {
        get
        {
            if (_objadaChangeControl == null)
                _objadaChangeControl = new dtChangeControlTableAdapter();
            return _objadaChangeControl;

        }

    }
    public object InsertchangeControl(Change change)
    {
        object objInsert = null;
        try
        {
            //objInsert = objadaChangeControl.Insert_ChangeControl(change.IncidentID, change.PortfolioID, change.Title, change.ChangeDescription, change.Justification, change.ResourceImpact, change.TargetReleaseDate, change.ResourceImpact, change.RequesterName, change.RequesterEmailID, change.CategoryID, change.Status, change.TargetStartDate, change.RaisedBy, change.RelatesToProjectRef, change.RelatesToservicerequest, change.EstimatedCost, change.EstimatedDaysRequired);
            objInsert = objadaChangeControl.Insert_ChangeControl(change.IncidentID, change.PortfolioID, change.Title, change.ChangeDescription, change.Justification, change.DateRaised, change.TargetReleaseDate, change.ResourceImpact, change.RequesterName, change.RequesterEmailID, change.CategoryID, change.Status, change.TargetStartDate, change.RaisedBy, change.RelatesToProjectRef, change.RelatesToservicerequest, change.EstimatedCost, change.EstimatedDaysRequired,change.RequesterID,change.CoOrdinator,change.SubCategoryID,change.AreaID,change.PriorityID,change.SiteID);

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return objInsert;
    }
    //change.IncidentID,change.PortfolioID, change.Justification, change.ResourceImpact, change.TargetReleaseDate, change.Title, change.ChangeDescription, change.DateRaised, change.RequesterName, change.RequesterEmailID, change.Status, change.CategoryID, change.TargetStartDate, change.RaisedBy, change.RelatesToProjectRef, change.RelatesToservicerequest, change.EstimatedCost, change.EstimatedDaysRequired
    public object UpdateControl(Change change)
    {
        object objRet = null;
        try
        {
            objadaChangeControl.Update_ChangeControl(change.Id, change.IncidentID, change.Title, change.ChangeDescription, change.Justification, change.DateRaised, change.TargetReleaseDate, change.ResourceImpact, change.RequesterName, change.RequesterEmailID, change.PortfolioID, change.CategoryID, change.Status, change.TargetStartDate, change.RaisedBy, change.RelatesToProjectRef, change.RelatesToservicerequest, change.EstimatedCost, change.EstimatedDaysRequired,change.RequesterID,change.CoOrdinator,change.SubCategoryID,change.AreaID,change.PriorityID,change.SiteID);
            objRet = 1;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return objRet;
    }
    public DBChangeControl.dtChangeControlDataTable GetDataById(int ID)
    {
        return objadaChangeControl.SelectChangeControlByID(ID);
    }
    #endregion
    #region CCReport


    private static dtChangeControl_ReportTableAdapter _objadaCCReport;
    public static dtChangeControl_ReportTableAdapter objadaCCReport
    {
        get
        {
            if (_objadaCCReport == null)
                _objadaCCReport = new dtChangeControl_ReportTableAdapter();
            return _objadaCCReport;

        }

    }
    public DataTable GetReports(int ChangeControlID)
    {
        DBChangeControl.dtChangeControl_ReportDataTable dtTab = objadaCCReport.GetData(ChangeControlID);

        return dtTab;
    }
    #endregion
    #region ChangeControlApproval

    private static dtChangeControlApprovalReportTableAdapter _objadaApprovalReport;
    public static dtChangeControlApprovalReportTableAdapter objadaApprovalReport
    {
        get
        {
            if (_objadaApprovalReport == null)
                _objadaApprovalReport = new dtChangeControlApprovalReportTableAdapter();
            return _objadaApprovalReport;

        }

    }
    public DataTable GetApprovalReport(int ChangeControlID)
    {
        DBChangeControl.dtChangeControlApprovalReportDataTable dtTab = objadaApprovalReport.GetData(ChangeControlID);

        return dtTab;
    }
    public object ApprovalUpdate(int changeControlID, bool ApprovalVal)
    {
        object rowsAffected = null;
        try
        {
            objadaApprovalReport.Update_ChangeControlApproval(ApprovalVal, changeControlID);
            rowsAffected = 1;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
        }
        return rowsAffected;
    }

    public object ApprovalOrDenyByID(int ApprovalID, bool ApprovalVal)
    {
        object rowsAffected = null;
        try
        {
            objadaApprovalReport.ChangeControl_ApproveOrDeny_ByApprovalID(ApprovalVal, ApprovalID);
            rowsAffected = 1;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
        }
        return rowsAffected;
    }

    #endregion
    #region ChangeControlTask


    private static dtChangeControlTaskReportTableAdapter _objadaTaskReport;
    public static dtChangeControlTaskReportTableAdapter objadaTaskReport
    {
        get
        {
            if (_objadaTaskReport == null)
                _objadaTaskReport = new dtChangeControlTaskReportTableAdapter();
            return _objadaTaskReport;

        }

    }
    public DataTable GetTaskReport(int ChangeControlID)
    {
        DBChangeControl.dtChangeControlTaskReportDataTable dtTab = objadaTaskReport.GetData(ChangeControlID);

        return dtTab;
    }
    #endregion

    #region ChangeControlRisk


    private static dtChangeControlRiskReportTableAdapter _objadaRiskReport;
    public static dtChangeControlRiskReportTableAdapter objadaRiskReport
    {
        get
        {
            if (_objadaRiskReport == null)
                _objadaRiskReport = new dtChangeControlRiskReportTableAdapter();
            return _objadaRiskReport;

        }

    }
    public DataTable GetRiskReport(int ChangeControlID)
    {
        DBChangeControl.dtChangeControlRiskReportDataTable dtTab = objadaRiskReport.GetData(ChangeControlID);

        return dtTab;
    }
    #endregion

    #region Pricing_Schedule

    private static dtPricingScheduleTableAdapter _objadaadaPricingSchedule;
    public static dtPricingScheduleTableAdapter objadaadaPricingSchedule
    {
        get
        {
            if (_objadaadaPricingSchedule == null)
                _objadaadaPricingSchedule = new dtPricingScheduleTableAdapter();
            return _objadaadaPricingSchedule;

        }

    }
    public DataTable SelectPricingSchedule(int ChangeControlID)
    {
        DBChangeControl.dtPricingScheduleDataTable dtTab = objadaadaPricingSchedule.GetData(ChangeControlID);
        return dtTab;
    }


   

    public DataTable SelectPricingSchedule_ByID(int pricingScheduleID)
    {
        DBChangeControl.dtPricingScheduleDataTable dtTab = objadaadaPricingSchedule.GetDataByID(pricingScheduleID);

        return dtTab;
    }
    public object PricingScheduleInsert(string type, string description, string unit, double estimatedCost, int quantity, double total, int changeControlID)
    {
        object rowsAffected = null;
        try
        {
            rowsAffected = objadaadaPricingSchedule.Insert_PricingSchedule(type, description, unit, estimatedCost, quantity, total, changeControlID);

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
        }
        return rowsAffected;
    }

    public object PricingScheduleUpdate(int PricingSceduleID, string type, string description, string unit, double estimatedCost, int quantity, double total, int changeControlID)
    {
        object rowsAffected = null;
        try
        {
            objadaadaPricingSchedule.Update_PricingSchedule(PricingSceduleID, type, description, unit, estimatedCost, quantity, total, changeControlID);
            rowsAffected = 1;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
        }
        return rowsAffected;
    }






    #endregion


}

