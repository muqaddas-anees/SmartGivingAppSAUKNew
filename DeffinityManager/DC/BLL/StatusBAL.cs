using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for StatusBAL
    /// </summary>
    public static class JobStatus
    {
        public const int Pending = 22;
        public const int New = 22;
        public const int Cancelled = 33;
        public const int Closed = 35;
        public const int Completed = 35;
        public const int Scheduled = 43;
        public const int Active = 43;
        public const int Awaiting_Schedule = 44;
        public const int Arrived = 45;
        public const int Customer_Not_Responding = 46;
        public const int Feedback_Submitted = 47;
        public const int Feedback_Received = 48;
        public const int Quote_Rejected = 49;
        public const int Quote_Accepted = 50;
        public const int Awaiting_Information = 51;
        public const int Waiting_On_Parts = 52;
        public const int Authorised = 53;
        public const int Request_Feeback = 54;
        public const int Quote_Sent = 55;
    }
public class StatusBAL
    {
        

        #region Bind Status
        public static List<Status> BindStatus(int rid)
        {
            List<Status> statusList = new List<Status>();
            using (DCDataContext dd = new DCDataContext())
            {
                statusList = dd.Status.Where(s=>s.RequestTypeID == rid).Select(r => r).ToList();
            }
            return statusList;
        }
        #endregion
        #region Select permit by ID
        public static Status SelectbyId(int id)
        {

            Status s = new Status();
            using (DCDataContext dd = new DCDataContext())
            {
                s = dd.Status.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return s;
        }
        #endregion
        #region Check status Exists when Updating
        public static bool CheckbyIdUpdate(int id,int rid,string name)
        {

            Status s = new Status();
            using (DCDataContext dd = new DCDataContext())
            {
                s = dd.Status.Where(r => r.ID != id && r.RequestTypeID == rid && r.Name.ToLower() == name.ToLower()).Select(r => r).FirstOrDefault();
            }
            if (s != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        # region Update status
        public static void StatusUpdate(Status s)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                Status scurrent = dd.Status.Where(r => r.ID == s.ID).Select(r => r).FirstOrDefault();
                scurrent.Name = s.Name;
                scurrent.RequestTypeID = s.RequestTypeID;
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Check status Exists when Inserting
        public static bool CheckExists(string name,int rid)
        {

            Status s = new Status();
            using (DCDataContext dd = new DCDataContext())
            {
                s = dd.Status.Where(r => r.Name.ToLower() == name.ToLower() && r.RequestTypeID == rid).Select(r => r).FirstOrDefault();
            }
            if (s != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Add status
        public static void AddStatus(Status s)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.Status.InsertOnSubmit(s);
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Delete Status By ID
        public static void StatusDelete(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                Status s = dd.Status.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (s != null)
                {
                    dd.Status.DeleteOnSubmit(s);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
        #region Status Name by stautsId
        public static string StatusNamebyId(int statusId)
        {
            string name = string.Empty;
            using (DCDataContext dd = new DCDataContext())
            {
                name = dd.Status.Where(s => s.ID == statusId).Select(s => s.Name).FirstOrDefault();
            }
            return name;
        }
        #endregion
        #region Select status
        public static List<Status> Status_selectAll()
        {
            List<Status> s = new List<Status>();
            using (DCDataContext dc = new DCDataContext())
            {
                s = (from ac in dc.Status where ac.RequestTypeID == 6 select ac).ToList();
            }
            return s;
        }
        #endregion

        #region Select status by Id
        public static Status StatusbyId(int id)
        {
            Status type = new Status();
            using (DCDataContext dd = new DCDataContext())
            {
                type = dd.Status.Where(s => s.ID == id).Select(s => s).FirstOrDefault();
            }
            return type;
        }
        #endregion

    
    }
}