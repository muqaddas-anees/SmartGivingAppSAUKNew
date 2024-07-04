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
    /// Summary description for RequestTypeBAL
    /// </summary>
    public class RequestTypeBAL
    {
        #region Bind Permits
        public static List<RequestType> BindPermit()
        {
            List<RequestType> typeList = new List<RequestType>();
            using (DCDataContext dd = new DCDataContext())
            {
                typeList = dd.RequestTypes.Select(r => r).ToList();
            }
            return typeList;
        }
        #endregion
        #region Check permit Exists when Inserting
        public static bool CheckExists(string name)
        {

            RequestType type = new RequestType();
            using (DCDataContext dd = new DCDataContext())
            {
                type = dd.RequestTypes.Where(r => r.Name.ToLower() == name.ToLower()).Select(r => r).FirstOrDefault();
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
        #region Add permits
        public static void AddPermits(RequestType rtype)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.RequestTypes.InsertOnSubmit(rtype);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select permit by ID
        public static RequestType SelectbyId(int id)
        {

            RequestType type = new RequestType();
            using (DCDataContext dd = new DCDataContext())
            {
                type = dd.RequestTypes.Where(r=>r.ID == id).Select(r => r).FirstOrDefault();
            }
            return type;
        }
        #endregion
        #region Check permit Exists when Updating
        public static bool CheckbyIdUpdate(int id,string name)
        {

            RequestType type = new RequestType();
            using (DCDataContext dd = new DCDataContext())
            {
                type = dd.RequestTypes.Where(r => r.ID != id && r.Name.ToLower() == name.ToLower()).Select(r => r).FirstOrDefault();
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
        # region Update Permit
        public static void PermitUpdate(RequestType type)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                RequestType tcurrent = dd.RequestTypes.Where(r=>r.ID == type.ID).Select(r => r).FirstOrDefault();      
                tcurrent.Name = type.Name;
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Delete Permit By ID
        public static void PermitDelete(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                RequestType type = dd.RequestTypes.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (type != null)
                {
                    dd.RequestTypes.DeleteOnSubmit(type);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion

      
    }
}