using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for OurSiteBAL
    /// </summary>
    public class OurSiteBAL
    {
        public OurSiteBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Get OurSite List
        public static List<OurSite> BindOurSite()
        {
            List<OurSite> ourSite = new List<OurSite>();
            using (DCDataContext dd = new DCDataContext())
            {
                ourSite = dd.OurSites.Select(o => o).OrderBy(o=>o.Name).ToList();
            }
            return ourSite;
        }
        #endregion

        #region Add OurSite
        public static void AddOurSite(OurSite os)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.OurSites.InsertOnSubmit(os);
                dd.SubmitChanges();
            }

        }
        #endregion

        #region Update OurSite
        public static void UpdateOurSite(OurSite os)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                OurSite osCurrent = dd.OurSites.Where(o => o.ID == os.ID).Select(o => o).FirstOrDefault();
                osCurrent.Name = os.Name;
                dd.SubmitChanges();
            }
        }
        #endregion

        #region Delete OurSite By ID
        public static void DeleteOurSite(int id)
        {
           
            using (DCDataContext dd = new DCDataContext())
            {
                OurSite os = dd.OurSites.Where(o => o.ID == id).Select(o => o).FirstOrDefault();
                if (os != null)
                {
                    dd.OurSites.DeleteOnSubmit(os);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
        #region Select OurSite by ID
        public static OurSite SelectbyId(int id)
        {
            OurSite os = new OurSite();
            using (DCDataContext dd = new DCDataContext())
            {
                os = dd.OurSites.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return os;
        }
        #endregion
        #region Check OurSite Exists when Updating
        public static bool CheckbyIdUpdate(int id, string name, int customerId)
        {
            OurSite os = new OurSite();
            using (DCDataContext dd = new DCDataContext())
            {
                os = dd.OurSites.Where(r => r.ID != id && r.Name.ToLower() == name.ToLower() && r.CustomerID == customerId).Select(r => r).FirstOrDefault();
            }
            if (os != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Check OurSite Exists when Inserting
        public static bool CheckExists(string name,int customerId)
        {

            OurSite os = new OurSite();
            using (DCDataContext dd = new DCDataContext())
            {
                os = dd.OurSites.Where(r => r.Name.ToLower() == name.ToLower() && r.CustomerID == customerId).Select(r => r).FirstOrDefault();
            }
            if (os != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Check OurSite Exists when Inserting
        public void DeteleSitetoAllCustomers(string Site)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                List<OurSite> l = new List<OurSite>();
                l = (from a in dc.OurSites where a.Name == Site select a).ToList();
                dc.OurSites.DeleteAllOnSubmit(l);
                dc.SubmitChanges();
            }
        }
        #endregion
    }
}