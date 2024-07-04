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
    /// Summary description for DeliveryTypeBAL
    /// </summary>
    public class DeliveryTypeBAL
    {
        #region Bind DeliveryType
        public static List<DeliveryType> BindTypes()
        {
            List<DeliveryType> typeList = new List<DeliveryType>();
            using (DCDataContext dd = new DCDataContext())
            {
                typeList = dd.DeliveryTypes.Select(r => r).OrderBy(r=>r.Name).ToList();
            }
            return typeList;           
        }
        #endregion
        #region Check Type Exists when Inserting
        public static bool CheckExists(string name)
        {

            DeliveryType type = new DeliveryType();
            using (DCDataContext dd = new DCDataContext())
            {
                type = dd.DeliveryTypes.Where(r => r.Name.ToLower() == name.ToLower()).Select(r => r).FirstOrDefault();
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
        #region Add Type
        public static void AddTypes(DeliveryType dtype)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.DeliveryTypes.InsertOnSubmit(dtype);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select type by ID
        public static DeliveryType SelectbyId(int id)
        {

            DeliveryType type = new DeliveryType();
            using (DCDataContext dd = new DCDataContext())
            {
                type = dd.DeliveryTypes.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return type;
        }
        #endregion
        #region Check type Exists when Updating
        public static bool CheckbyIdUpdate(int id, string name)
        {

            DeliveryType type = new DeliveryType();
            using (DCDataContext dd = new DCDataContext())
            {
                type = dd.DeliveryTypes.Where(r => r.ID != id && r.Name.ToLower() == name.ToLower()).Select(r => r).FirstOrDefault();
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
        # region Update Type
        public static void TypeUpdate(DeliveryType type)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                DeliveryType dcurrent = dd.DeliveryTypes.Where(r => r.ID == type.ID).Select(r => r).FirstOrDefault();
                dcurrent.Name = type.Name;
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Delete Type By ID
        public static void TypeDelete(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                DeliveryType type = dd.DeliveryTypes.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (type != null)
                {
                    dd.DeliveryTypes.DeleteOnSubmit(type);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
    }
}