using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;
namespace DC.BLL
{
    /// <summary>
    /// Summary description for TypeOfRequestBAL
    /// </summary>
    public class TypeOfRequestBAL
    {

        public TypeOfRequestBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Get list of Type of Request
        public static IEnumerable<TypeOfRequest> GetTypeOfRequestList()
        {
            AddDefaultData();
            using (DCDataContext db = new DCDataContext())
            {
                return db.TypeOfRequests.Where(o => o.CustomerID == sessionKeys.PortfolioID).OrderBy(c => c.Name).ToList();
            }
        }

        public static IEnumerable<TypeOfRequest> GetTypeOfRequestList(int PortfolioID)
        {
            AddDefaultData();
            using (DCDataContext db = new DCDataContext())
            {
                return db.TypeOfRequests.Where(o=>o.CustomerID == PortfolioID).OrderBy(c => c.Name).ToList();
            }
        }

        public static void AddDefaultData()
        {
            string[] slist = { "Event", "Demonstration", "Charity", "R&D", "Administrative", "Installation", "Maintenance", "Design", "Hire", "Other" };
            using (DCDataContext db = new DCDataContext())
            {
                foreach(var s in slist)
                {
                    if(db.TypeOfRequests.Where(o=>o.Name.ToLower() == s.ToLower() && o.CustomerID == sessionKeys.PortfolioID).Count() == 0)
                    {
                        var t = new TypeOfRequest();
                        t.CustomerID = sessionKeys.PortfolioID;
                        t.Name = s;
                        db.TypeOfRequests.InsertOnSubmit(t);
                        db.SubmitChanges();
                    }
                }
            }
        }
        #endregion

        #region Type of Request insert
        public static void AddTypeOfRequest(TypeOfRequest typeOfRequest)
        {
            using (DCDataContext db = new DCDataContext())
            {
                db.TypeOfRequests.InsertOnSubmit(typeOfRequest);
                db.SubmitChanges();
            }
        }
        #endregion

        #region Type of Request update
        public static void UpdateTypeOfRequest(TypeOfRequest typeOfRequest)
        {
            using (DCDataContext db = new DCDataContext())
            {
                TypeOfRequest categoryCurrent = db.TypeOfRequests.Where(r => r.ID == typeOfRequest.ID).FirstOrDefault();
                categoryCurrent.Name = typeOfRequest.Name;
                db.SubmitChanges();
            }
        }
        #endregion

        #region Check Type of Request when an insert
        public static bool CheckTypeOfRequest(string name, int customerId)
        {
            bool exists = false;
            using (DCDataContext db = new DCDataContext())
            {
                TypeOfRequest typeOfRequest = db.TypeOfRequests.Where(r => r.Name == name && r.CustomerID == customerId).FirstOrDefault();
                if (typeOfRequest != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Check Type of Request when an update
        public static bool CheckTypeOfRequest(int id, string name, int customerId)
        {
            bool exists = false;
            using (DCDataContext db = new DCDataContext())
            {
                TypeOfRequest typeOfRequest = db.TypeOfRequests.Where(r => r.ID != id && r.Name == name && r.CustomerID == customerId).FirstOrDefault();
                if (typeOfRequest != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Select by Type of Request Id
        public static TypeOfRequest SelectByID(int id)
        {
            TypeOfRequest typeOfRequest = new TypeOfRequest();
            using (DCDataContext db = new DCDataContext())
            {
                typeOfRequest = db.TypeOfRequests.Where(r => r.ID == id).FirstOrDefault();
            }
            return typeOfRequest;
        }

        #endregion

        #region Delete Type of Request by Id
        public static void DeleteByID(int id)
        {
            using (DCDataContext db = new DCDataContext())
            {
                TypeOfRequest typeOfRequest = db.TypeOfRequests.Where(r => r.ID == id).FirstOrDefault();
                if (typeOfRequest != null)
                {
                    db.TypeOfRequests.DeleteOnSubmit(typeOfRequest);
                    db.SubmitChanges();
                }
            }

        }
        #endregion
        #region Delete all Type of Request by Id
        public static void DeteleSitetoAllCustomers(string TypeOfRequest)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                  List<TypeOfRequest> l = new List<TypeOfRequest>();
                  List<SubCategory> Lss = new List<SubCategory>();
                  List<Category> Ls=new List<Category>();
                l = (from a in dc.TypeOfRequests where a.Name == TypeOfRequest select a).ToList();
                foreach(var item in l)
                {
                    Ls = (from a in dc.Categories where a.TypeOfRequestID==item.ID select a).ToList();
                    foreach (var x in Ls)
                    {
                        Lss = (from a in dc.SubCategories where a.CategoryID==x.ID select a).ToList();
                        dc.SubCategories.DeleteAllOnSubmit(Lss);
                        dc.SubmitChanges();
                    }
                    dc.Categories.DeleteAllOnSubmit(Ls);
                    dc.SubmitChanges();
                }
                dc.TypeOfRequests.DeleteAllOnSubmit(l);
                dc.SubmitChanges();
            }
        }
        #endregion
    }
}