using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for SourceOfRequestBAL
    /// </summary>
    public class SourceOfRequestBAL
    {
        public SourceOfRequestBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Get list of FLS Source Of Request
        public static IEnumerable<FLSSourceOfRequest> GetSourceOfRequestList()
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSSourceOfRequests.OrderBy(c => c.Name).ToList();
            }
        }
        #endregion

        #region FLS Source Of Request insert
        public static void AddSourceOfRequest(FLSSourceOfRequest flsSourceOfRequest)
        {
            using (DCDataContext db = new DCDataContext())
            {
                db.FLSSourceOfRequests.InsertOnSubmit(flsSourceOfRequest);
                db.SubmitChanges();
            }
        }
        #endregion

        #region FLS Source Of Request update
        public static void UpdateSourceOfRequest(FLSSourceOfRequest flsSourceOfRequest)
        {
            using (DCDataContext db = new DCDataContext())
            {
                FLSSourceOfRequest currentRequest = db.FLSSourceOfRequests.Where(r => r.ID == flsSourceOfRequest.ID).FirstOrDefault();
                currentRequest.Name = flsSourceOfRequest.Name;
                db.SubmitChanges();
            }
        }
        #endregion

        #region Check FLS Source Of Request when an insert
        public static bool CheckSourceOfRequest(string name, int customerId)
        {
            bool exists = false;
            using (DCDataContext db = new DCDataContext())
            {
                FLSSourceOfRequest flsSourceOfRequest = db.FLSSourceOfRequests.Where(r => r.Name == name && r.CustomerID == customerId).FirstOrDefault();
                if (flsSourceOfRequest != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Check FLSSource Of Request when an update
        public static bool CheckSourceOfRequest(int id, string name, int customerId)
        {
            bool exists = false;
            using (DCDataContext db = new DCDataContext())
            {
                FLSSourceOfRequest flsSourceOfRequest = db.FLSSourceOfRequests.Where(r => r.ID != id && r.Name == name && r.CustomerID == customerId).FirstOrDefault();
                if (flsSourceOfRequest != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Select by FLS Source Of Request Id
        public static FLSSourceOfRequest SelectByID(int id)
        {
            FLSSourceOfRequest flsSourceOfRequest = new FLSSourceOfRequest();
            using (DCDataContext db = new DCDataContext())
            {
                flsSourceOfRequest = db.FLSSourceOfRequests.Where(r => r.ID == id).FirstOrDefault();
            }
            return flsSourceOfRequest;
        }

        #endregion

        #region Delete FLS Source Of Request by Id
        public static void DeleteByID(int id)
        {
            using (DCDataContext db = new DCDataContext())
            {
                FLSSourceOfRequest flsSourceOfRequest = db.FLSSourceOfRequests.Where(r => r.ID == id).FirstOrDefault();
                if (flsSourceOfRequest != null)
                {
                    db.FLSSourceOfRequests.DeleteOnSubmit(flsSourceOfRequest);
                    db.SubmitChanges();
                }
            }

        }
        #endregion
        #region Delete all FLS Source Of Request by name
        public static void DeteleSRtoAllCustomers(string Sr)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                List<FLSSourceOfRequest> l = new List<FLSSourceOfRequest>();
                l = (from a in dc.FLSSourceOfRequests where a.Name == Sr select a).ToList();
                dc.FLSSourceOfRequests.DeleteAllOnSubmit(l);
                dc.SubmitChanges();
            }
        }
        #endregion
    }
}