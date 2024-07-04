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
    /// Summary description for PermitTypeBAL
    /// </summary>
    public class PermitTypeBAL
    {
        #region Bind Permit Type
        public static List<PermitType> BindPermitTypes()
        {
            List<PermitType> ptList = new List<PermitType>();
            using (DCDataContext dd = new DCDataContext())
            {
                ptList = dd.PermitTypes.Select(p => p).OrderBy(p=>p.Type).ToList();
            }
            return ptList;
        }
        #endregion

        #region Add Permit Type
        public static void AddPermitType(PermitType pt)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.PermitTypes.InsertOnSubmit(pt);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select Permit Type
        public static PermitType SelectbyId(int id)
        {

            PermitType pt = new PermitType();
            using (DCDataContext dd = new DCDataContext())
            {
                pt = dd.PermitTypes.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return pt;
        }
        #endregion
   
        # region Update Permit TYpe
        public static void PermitTypeUpdate(PermitType pt)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                PermitType ptcurrent = dd.PermitTypes.Where(r => r.ID == pt.ID).Select(r => r).FirstOrDefault();
                ptcurrent.Type = pt.Type;
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Delete Permit Type
        public static void PermitTypeDelete(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                PermitType pt = dd.PermitTypes.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (pt != null)
                {
                    dd.PermitTypes.DeleteOnSubmit(pt);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
        #region Check type Exists when Updating
        public static bool CheckbyIdUpdate(int id, string name)
        {
            PermitType type = new PermitType();
            using (DCDataContext dd = new DCDataContext())
            {
                type = dd.PermitTypes.Where(r => r.ID != id && r.Type.ToLower() == name.ToLower()).Select(r => r).FirstOrDefault();
            }
            if (type != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Check Type Exists when Inserting
        public static bool CheckExists(string name)
        {

            PermitType type = new PermitType();
            using (DCDataContext dd = new DCDataContext())
            {
                type = dd.PermitTypes.Where(r => r.Type.ToLower() == name.ToLower()).Select(r => r).FirstOrDefault();
            }
            if (type != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}