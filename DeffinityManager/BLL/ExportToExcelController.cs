using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeffinityManager.DAL.ExportToExcelTableAdapters;
using DeffinityManager.DAL;

/// <summary>
/// Summary description for ExportToExcelController
/// </summary>
///
/// 
/// 
namespace Deffinity.BLL
{
    public class ExportToExcelController
    {

        #region ExportofProjectdata
        private static ProjectdetailsByCustomerAndOwnerTableAdapter _ProjectdetailsByCustomerAndOwnerTableAdapter;
        public static ProjectdetailsByCustomerAndOwnerTableAdapter ProjectdetailsByCustomerAndOwnerAdapter
        {
            get
            {
                if (_ProjectdetailsByCustomerAndOwnerTableAdapter == null)
                    _ProjectdetailsByCustomerAndOwnerTableAdapter = new ProjectdetailsByCustomerAndOwnerTableAdapter();
                return _ProjectdetailsByCustomerAndOwnerTableAdapter;

            }
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ExportToExcel.ProjectdetailsByCustomerAndOwnerDataTable GetProjectdetailsByCustomerAndOwner(DateTime FromDate, DateTime ToDate, int Owner, int Customer)
        {

            return ProjectdetailsByCustomerAndOwnerAdapter.GetProjectdetailsByCustomerAndOwner(FromDate, ToDate, Owner, Customer);
        }
        #endregion ExportofProjectdata

        #region TimeSheet
        private static TimesheerByResourceAndProjectTableAdapter _TimesheerByResourceAndProjectTableAdapter;
        public static TimesheerByResourceAndProjectTableAdapter TimesheerByResourceAndProjectAdapter
        {
            get
            {
                if (_TimesheerByResourceAndProjectTableAdapter == null)
                    _TimesheerByResourceAndProjectTableAdapter = new TimesheerByResourceAndProjectTableAdapter();
                return _TimesheerByResourceAndProjectTableAdapter;

            }
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ExportToExcel.TimesheerByResourceAndProjectDataTable GetTimesheerByResourceAndProject(DateTime FromDate, DateTime ToDate, int ResourceID, int ProjectRef)
        {
            
            
            return TimesheerByResourceAndProjectAdapter.GetTimesheerByResourceAndProject(FromDate, ToDate, ResourceID, ProjectRef);
        }

        #endregion TimeSheet

        #region Annual Leave and Absence

        private static RequestsByResourceTableAdapter _RequestsByResourceTableAdapter;
        public static RequestsByResourceTableAdapter RequestsByResourceAdapter
        {
            get
            {
                if (_RequestsByResourceTableAdapter == null)
                    _RequestsByResourceTableAdapter = new RequestsByResourceTableAdapter();
                return _RequestsByResourceTableAdapter;

            }
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ExportToExcel.RequestsByResourceDataTable GetRequestsByResource(DateTime FromDate, DateTime ToDate, int ResourceID, int AbsenceType)
        {
            return RequestsByResourceAdapter.GetRequestsByResource(FromDate, ToDate, ResourceID, AbsenceType);
        }
        #endregion Annual Leave and Absence

        #region Issues & Risks
        private static IssuelistByProjectTableAdapter _IssuelistByProjectTableAdapter;
        public static IssuelistByProjectTableAdapter IssuelistByProjectAdapter
        {
            get
            {
                if (_IssuelistByProjectTableAdapter == null)
                    _IssuelistByProjectTableAdapter = new IssuelistByProjectTableAdapter();
                return _IssuelistByProjectTableAdapter;

            }
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ExportToExcel.IssuelistByProjectDataTable GetIssuelistByProject(int CustomerID, int StatusID, int ProjectRef)
        {
            return IssuelistByProjectAdapter.GetIssuelistByProject(CustomerID, StatusID, ProjectRef);
        }
        #endregion Issues & Risks
    }
}