using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for TaskEntity
/// </summary>
namespace Deffinity.Project
{
    public class Task
    {
       
        int _ID, _ProjectReference, _ListPosition, _AmberDays, _RedDays, _ItemStatus, _IndentLevel, _QTY, _CompletedBy, _WorstCaseTime, _BestCaseTime, _MostLikelyCaseTime, _Defects;
        string _RAGStatus,_ItemDescription, _PercentComplete, _RAGRequired, _Comments, _IncludeInValuation, _Notes, _QA, _WCExtension, _BCExtension, _MCExtension, _CheckPoint_Notes;
        string _StartDate, _CompletionDate, _ProjectStartDate, _ProjectEndDate;
        double _Price, _SellingPrice, _GrossProfit, _CostTotal, _SellingTotal;
        bool _Approved;
        #region int datatype
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int ProjectReference
        {
            get { return _ProjectReference; }
            set { _ProjectReference = value; }
        }
        public int ListPosition
        {
            get { return _ListPosition; }
            set { _ListPosition = value; }
        }       
        public int AmberDays
        {
            get { return _AmberDays; }
            set { _AmberDays = value; }
        }
        public int RedDays
        {
            get { return _RedDays; }
            set { _RedDays = value; }
        }
        public int ItemStatus
        {
            get { return _ItemStatus; }
            set { _ItemStatus = value; }
        }
        public int IndentLevel
        {
            get { return _IndentLevel; }
            set { _IndentLevel = value; }
        }
        public int QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }
        public int CompletedBy
        {
            get { return _CompletedBy; }
            set { _CompletedBy = value; }
        }
        public int WorstCaseTime
        {
            get { return _WorstCaseTime; }
            set { _WorstCaseTime = value; }
        }
        public int BestCaseTime
        {
            get { return _BestCaseTime; }
            set { _BestCaseTime = value; }
        }
        public int MostLikelyCaseTime
        {
            get { return _MostLikelyCaseTime; }
            set { _MostLikelyCaseTime = value; }
        }
        public int Defects
        {
            get { return _Defects; }
            set { _Defects = value; }
        }
        #endregion
        #region string datatype
        public string ItemDescription
        {
            get { return _ItemDescription; }
            set { _ItemDescription = value; }
        }
        public string PercentComplete
        {
            get { return _PercentComplete; }
            set { _PercentComplete = value; }
        }
        public string RAGRequired
        {
            get { return _RAGRequired; }
            set { _RAGRequired = value; }
        }
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }
        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }
        public string IncludeInValuation
        {
            get { return _IncludeInValuation; }
            set { _IncludeInValuation = value; }
        }
        public string QA
        {
            get { return _QA; }
            set { _QA = value; }
        }
        public string WCExtension
        {
            get { return _WCExtension; }
            set { _WCExtension = value; }
        }
        public string BCExtension
        {
            get { return _BCExtension; }
            set { _BCExtension = value; }
        }
        public string MCExtension
        {
            get { return _MCExtension; }
            set { _MCExtension = value; }
        }
        public string CheckPoint_Notes
        {
            get { return _CheckPoint_Notes; }
            set { _CheckPoint_Notes = value; }
        }
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public string CompletionDate
        {
            get { return _CompletionDate; }
            set { _CompletionDate = value; }
        }
        public string ProjectStartDate
        {
            get { return _ProjectStartDate; }
            set { _ProjectStartDate = value; }
        }
        public string ProjectEndDate
        {
            get { return _ProjectEndDate; }
            set { _ProjectEndDate = value; }
        }
        public string RAGStatus
        {
            get { return _RAGStatus; }
            set { _RAGStatus = value; }
        }
#endregion
        #region double datatype
        public double Price
        {
            get { return _Price; }
            set { _Price=value;  }
        }
        public double SellingPrice
        {
            get { return _SellingPrice; }
            set { _SellingPrice = value; }
        }
        public double GrossProfit
        {
            get { return _GrossProfit; }
            set { _GrossProfit = value; }
        }
        public double CostTotal
        {
            get { return _CostTotal; }
            set { _CostTotal = value; }
        }
        public double SellingTotal
        {
            get { return _SellingTotal; }
            set { _SellingTotal = value; }
        }
        #endregion
        #region bool datatype
        public bool Approved
        {
            get { return _Approved; }
            set { _Approved = value; }
        }
        #endregion

    }
}
