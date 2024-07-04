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
    /// Summary description for StorageLocationBAL
    /// </summary>
    public class StorageLocationBAL
    {
        #region Bind Location
        public static List<StorageLocation> BindLocation()
        {
            List<StorageLocation> locationList = new List<StorageLocation>();
            using (DCDataContext dd = new DCDataContext())
            {
                locationList = dd.StorageLocations.Select(r => r).OrderBy(r=>r.Name).ToList();
            }
            return locationList;
        }
        #endregion
        #region Check Location Exists when Inserting
        public static bool CheckExists(string name)
        {

            StorageLocation location = new StorageLocation();
            using (DCDataContext dd = new DCDataContext())
            {
                location = dd.StorageLocations.Where(r => r.Name.ToLower() == name.ToLower()).Select(r => r).FirstOrDefault();
            }
            if (location != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Add Locations
        public static void AddLocations(StorageLocation location)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.StorageLocations.InsertOnSubmit(location);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select Location by ID
        public static StorageLocation SelectbyId(int id)
        {

            StorageLocation location = new StorageLocation();
            using (DCDataContext dd = new DCDataContext())
            {
                location = dd.StorageLocations.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return location;
        }
        #endregion
        #region Check Location Exists when Updating
        public static bool CheckbyIdUpdate(int id, string name)
        {

            StorageLocation location = new StorageLocation();
            using (DCDataContext dd = new DCDataContext())
            {
                location = dd.StorageLocations.Where(r => r.ID != id && r.Name.ToLower() == name.ToLower()).Select(r => r).FirstOrDefault();
            }
            if (location != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        # region Update Location
        public static void LocationUpdate(StorageLocation location)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                StorageLocation tcurrent = dd.StorageLocations.Where(r => r.ID == location.ID).Select(r => r).FirstOrDefault();
                tcurrent.Name = location.Name;
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Delete Location By ID
        public static void LocationDelete(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                StorageLocation location = dd.StorageLocations.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (location != null)
                {
                    dd.StorageLocations.DeleteOnSubmit(location);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
    }
}