using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for FLSDetailsJournalBAL
    /// </summary>
    public class FLSDetailsJournalBAL
    {
        #region Add FLSDetailsJournal
        public static void AddFLSDetailsJournal(FLSDetailsJournal flsdetailsjournal)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.FLSDetailsJournals.InsertOnSubmit(flsdetailsjournal);
                dd.SubmitChanges();
            }
        }
        public static void AddFLSDetailsJournal(FLSDetail f)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                var fj = new FLSDetailsJournal();
                fj.AppliedPreferredDate = f.AppliedPreferredDate;
                fj.CallID = f.CallID;
                fj.CategoryID = f.CategoryID;
                fj.ContactAddressID = f.ContactAddressID;
                fj.CustomerCostCode = f.CustomerCostCode;
                fj.CustomerReference = f.CustomerReference;
                fj.DateTimeClosed = f.DateTimeClosed;
                fj.DateTimeStarted = f.DateTimeStarted;
                fj.DepartmentID = f.DepartmentID;
                fj.Details = f.Details;
                fj.ModifiedBy = f.UserID;
                fj.ModifiedDate = DateTime.Now;
                fj.Notes = f.Notes;
                fj.POnumber = f.POnumber;
                fj.Preferreddate2 = f.Preferreddate2;
                fj.Preferreddate3 = f.Preferreddate3;
                fj.Preferreddatetotime2 = f.Preferreddatetotime2;
                fj.Preferreddatetotime3 = f.Preferreddatetotime3;
                fj.PriorityId = f.PriorityId;
                fj.ProviderPreferredDateTime = f.ProviderPreferredDateTime;
                fj.Qty = f.Qty;
                fj.RAGStatus = f.RAGStatus;
                fj.RequestType = f.RequestType;
                fj.Resolution = f.Resolution;
                fj.ResolutionDateandTime = f.ResolutionDateandTime;
                fj.ScheduledDate = f.ScheduledDate;
                fj.ScheduledDatetotime = f.ScheduledDatetotime;
                fj.ScheduledEndDateTime = f.ScheduledEndDateTime;
                fj.Sitedetails = f.Sitedetails;
                fj.SourceOfRequestID = f.SourceOfRequestID;
                fj.SubCategoryID = f.SubCategoryID;
                fj.SubjectID = f.SubjectID;
                fj.SubmittedBy = f.SubmittedBy;
                fj.TaskCategoryID = f.TaskCategoryID;
                fj.TaskSubcategoryID = f.TaskSubcategoryID;
                fj.TicketManagerID = f.TicketManagerID;
                fj.TimeAccumulated = f.TimeAccumulated;
                fj.TimeTakentoResolve = f.TimeTakentoResolve;
                fj.TimeWorked = f.TimeWorked;
                fj.UserID = f.UserID;
                fj.VisibleToCustomer = false;
                
                dd.FLSDetailsJournals.InsertOnSubmit(fj);
                dd.SubmitChanges();
            }
        }
        #endregion

        #region Select FLSDetailsJournal By CallID
        public static FLSDetailsJournal SelectByCallID(int CallID)
        {
            FLSDetailsJournal fj = new FLSDetailsJournal();
            using (DCDataContext dd = new DCDataContext())
            {
                fj = dd.FLSDetailsJournals.Where(f => f.CallID == CallID).Select(f => f).FirstOrDefault();
            }
            return fj;
        }
        #endregion
        #region Select FLSDetailsJournal by CallID
        public static List<FLSDetailsJournal> SelectFLSDetailsJournalbyCallID(int cid)
        {
            List<FLSDetailsJournal> lstflsj = new List<FLSDetailsJournal>();
            using (DCDataContext dd = new DCDataContext())
            {
                lstflsj = dd.FLSDetailsJournals.Where(c => c.CallID == cid).Select(c => c).ToList();
                return lstflsj;
            }
        }

        public static List<FLSDetailsJournal> SelectFLSDetailsJournal_CustomerVisible_byCallID(int cid)
        {
            return SelectFLSDetailsJournalbyCallID(cid).Where(p => p.VisibleToCustomer == true).ToList();
        }
        #endregion
    }
}