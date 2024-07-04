using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Deffinity.VT.DAL;
using Deffinity.VT.Entity;
using UserMgt.DAL;
using UserMgt.Entity;

namespace Deffinity.VT.BAL
{
    /// <summary>
    /// Summary description for VTLeaveRequestBAL
    /// </summary>
    public class VTLeaveRequestBAL
    {
        public static void InsertJournal(LeaveRequestJournal leaveRequestJournal)
        {
            using (VTDataContext db = new VTDataContext())
            {
                db.LeaveRequestJournals.InsertOnSubmit(leaveRequestJournal);
                db.SubmitChanges();
            }
        }

        public static LeaveRequest GetLeaveRequestByID(int id)
        {
            LeaveRequest leaveRequest = new LeaveRequest();
            using (VTDataContext db = new VTDataContext())
            {
                leaveRequest = db.LeaveRequests.Where(l => l.ID == id).Select(l => l).FirstOrDefault();
            }

            return leaveRequest;
        }
        public static LeaveRequestJournal GetLeaveRequestJounalByID(int id)
        {
            LeaveRequestJournal journal = new LeaveRequestJournal();
            using (VTDataContext db = new VTDataContext())
            {
                journal = db.LeaveRequestJournals.Where(l => l.ID == id).Select(l => l).FirstOrDefault();
            }

            return journal;
        }
        public static List<LeaveRequestJournal> GetLeaveRequestJounalListByID(int id)
        {
            List<LeaveRequestJournal> journalList = new List<LeaveRequestJournal>();
            using (VTDataContext db = new VTDataContext())
            {
                journalList = db.LeaveRequestJournals.Where(l => l.UserID == id).Select(l => l).ToList();
            }

            return journalList;
        }
        public static List<LeaveRequestJournal> GetLeaveRequestJounalList()
        {
            List<LeaveRequestJournal> journalList = new List<LeaveRequestJournal>();
            using (VTDataContext db = new VTDataContext())
            {
                journalList = db.LeaveRequestJournals.Select(l => l).ToList();
            }

            return journalList;
        }

       
    }
}