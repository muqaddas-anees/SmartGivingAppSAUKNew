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
    /// Summary description for DeliveryDefaultsBAL
    /// </summary>
    public class DeliveryDefaultsBAL
    {
        #region Bind Defaults
        public static DeliveryDefault BindDefaults()
        {
            DeliveryDefault defaults = new DeliveryDefault();
            using (DCDataContext dd = new DCDataContext())
            {
                defaults = dd.DeliveryDefaults.Select(r => r).FirstOrDefault();
            }
            return defaults;
        }
        #endregion
        #region Check Defaults Exists
        public static int CheckExists()
        {
            int count;
            DeliveryDefault defaults = new DeliveryDefault();
            using (DCDataContext dd = new DCDataContext())
            {
                count = dd.DeliveryDefaults.Count();
            }
            return count;
        }
        #endregion
        #region Add Default
        public static void AddDefault(DeliveryDefault defaults)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.DeliveryDefaults.InsertOnSubmit(defaults);
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Update Defaults
        public static void DefaultsUpdate(DeliveryDefault defaults)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                DeliveryDefault dcurrent = dd.DeliveryDefaults.Select(r => r).FirstOrDefault();
                dcurrent.Weight = defaults.Weight;
                dcurrent.OverdueDays = defaults.OverdueDays;
                dcurrent.StorageCharges = defaults.StorageCharges;
                dcurrent.AutoClosePeriod = defaults.AutoClosePeriod;
                dcurrent.HeavyItemNotice = defaults.HeavyItemNotice;
                dd.SubmitChanges();
            }
        }
        #endregion
       
    }
}