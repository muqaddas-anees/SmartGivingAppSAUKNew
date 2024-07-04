using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for VT
/// </summary>
/// 
namespace VT
{
    public enum ApprovalStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3
    }

    public static class AllowanceProcedure
    {
        public const string SelectAll = "VT.AllowanceSelectAll";
        public const string SelectByUser = "VT.AllowanceSelectByuser";
        public const string Update = "VT.AllowanceUpdate";
        public const string Insert = "VT.AllowanceInsert";
        public const string Delete = "VT.AllowanceDelete";
    }

    public static class LeaveRequestProcedure
    {
        public const string SelectAll = "VT.LeaveRequestSelectAll";
        public const string SelectPending = "VT.LeaveRequestPending";
        public const string Update = "VT.LeaveRequestUpdate";
        //public const string Insert = "VT.LeaveRequestInsert";
        public const string Insert = "VT.InsertLeaveRequest_Period";
        public const string Pre_Insert = "VT.InsertLeaveRequest_PreRequest";
        public const string Delete = "VT.LeaveRequestDelete";
    }

    public static class LeaveApproverProcedure
    {
        public const string SelectApproverByUser="VT.LeaveApproverByUser";
        public const string SelectApproverByApprover = "VT.LeaveRequestByApprover";
        public const string SelectArchivedByApprover = "VT.LeaveRequestArchiveByApprover";
        public const string SetStatus = "VT.SetApprovalStatus";
        public const string SelectAll="VT.LeaveApproverSelectAll";
        public const string Update = "VT.LeaveApproverUpdate";
        public const string Insert = "VT.LeaveApproverInsert";
        public const string Delete = "VT.LeaveApproverDelete";
    }
    public static class LeaveDays
    {
        public const string Insert = "VT.LeaveDaysInsert";
        public const string Delete = "VT.LeaveDaysDelete";
        public const string SelectByUser = "VT.LeaveDaysSelectByuser"; 
    }
    public static class VTPHolidaysProcedure
    {
        public const string Select = "VT.PublicholidaysSelect";
        public const string Update = "VT.PublicholidaysUpdate";
        public const string Insert = "VT.PublicholidaysInsert";
        public const string Delete = "VT.PublicholidaysDelete";
        public const string LastYearInsert = "VT.PHcopyleaveInsert";
    }
}