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
    /// Summary description for PermitToWork
    /// </summary>
    public class PermitToWorkBAL
    {
        #region Bind Permit to Work
        public static List<PermitToWork> BindPermittoWork()
        {
            List<PermitToWork> pwList = new List<PermitToWork>();
            using (DCDataContext dd = new DCDataContext())
            {
                pwList = dd.PermitToWorks.Select(p=>p).ToList();
            }
            return pwList;
        }
        #endregion

        #region Add Permit To Work
        public static void AddPermittoWork(PermitToWork pw)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.PermitToWorks.InsertOnSubmit(pw);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select Permit To Work by ID
        public static PermitToWork SelectbyId(int id)
        {

            PermitToWork pw = new PermitToWork();
            using (DCDataContext dd = new DCDataContext())
            {
                pw = dd.PermitToWorks.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return pw;
        }
        #endregion
        #region Select Permit To Work by CallID
        public static PermitToWork SelectbyCallId(int cid)
        {

            PermitToWork pw = new PermitToWork();
            using (DCDataContext dd = new DCDataContext())
            {
                pw = dd.PermitToWorks.Where(r => r.CallID == cid).Select(r => r).FirstOrDefault();
            }
            return pw;
        }
        #endregion
        # region Update Permit To Work
        public static void PermittoWorkUpdate(PermitToWork pw)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                PermitToWork pwcurrent = dd.PermitToWorks.Where(r => r.ID == pw.ID).Select(r => r).FirstOrDefault();
                pwcurrent.CallID = pw.CallID;
                pwcurrent.FromDateofWork = pw.FromDateofWork;
                pwcurrent.ToDateofWork = pw.ToDateofWork;
                pwcurrent.Area = pw.Area;
                pwcurrent.TypeofPermit = pw.TypeofPermit;
                pwcurrent.DescriptionofWorks = pw.DescriptionofWorks;
                pwcurrent.ArrivalDate = pw.ArrivalDate;
                pwcurrent.Reason = pw.Reason;
                pwcurrent.Notes = pw.Notes;
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Delete Permit To Work By ID
        public static void permittoWorkDelete(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                PermitToWork pw = dd.PermitToWorks.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (pw != null)
                {
                    dd.PermitToWorks.DeleteOnSubmit(pw);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
    }
}