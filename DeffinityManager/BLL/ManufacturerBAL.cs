using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

/// <summary>
/// Summary description for ManufacturerBAL
/// </summary>
///
namespace ProjectMgt.BLL
{
    public class ManufacturerBAL
    {
        public ManufacturerBAL()
        {
           
            //
            // TODO: Add constructor logic here
            //
        }
        #region Get list of Manufacturer
        public static IEnumerable<Manufacturer> GetManufacturerList()
        {
            using ( projectTaskDataContext db = new  projectTaskDataContext())
            {
                return db.Manufacturers.OrderBy(c => c.Name).ToList();
            }
        }
        #endregion

        #region Manufacturer insert
        public static void AddManufacturer(Manufacturer manufacturer)
        {
            using ( projectTaskDataContext db = new  projectTaskDataContext())
            {
                db.Manufacturers.InsertOnSubmit(manufacturer);
                db.SubmitChanges();
            }
        }
        #endregion

        #region Manufacturer update
        public static void UpdateManufacturer(Manufacturer manufacturer)
        {
            using ( projectTaskDataContext db = new  projectTaskDataContext())
            {
                Manufacturer categoryCurrent = db.Manufacturers.Where(r => r.Id == manufacturer.Id).FirstOrDefault();
                categoryCurrent.Name = manufacturer.Name;
                db.SubmitChanges();
            }
        }
        #endregion

        #region Check Manufacturer when insert
        public static bool CheckManufacturer(string name)
        {
            bool exists = false;
            using ( projectTaskDataContext db = new  projectTaskDataContext())
            {
                Manufacturer manufacturer = db.Manufacturers.Where(r => r.Name == name).FirstOrDefault();
                if (manufacturer != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Check Manufacturer when update
        public static bool CheckManufacturer(int id, string name)
        {
            bool exists = false;
            using ( projectTaskDataContext db = new  projectTaskDataContext())
            {
                Manufacturer manufacturer = db.Manufacturers.Where(r => r.Id != id && r.Name == name).FirstOrDefault();
                if (manufacturer != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Select by Manufacturer Id
        public static Manufacturer SelectByID(int id)
        {
            Manufacturer manufacturer = new Manufacturer();
            using ( projectTaskDataContext db = new  projectTaskDataContext())
            {
                manufacturer = db.Manufacturers.Where(r => r.Id == id).FirstOrDefault();
            }
            return manufacturer;
        }

        #endregion

        #region Delete Manufacturer by Id
        public static void DeleteByID(int id)
        {
            using ( projectTaskDataContext db = new  projectTaskDataContext())
            {
                Manufacturer manufacturer = db.Manufacturers.Where(r => r.Id == id).FirstOrDefault();
                if (manufacturer != null)
                {
                    db.Manufacturers.DeleteOnSubmit(manufacturer);
                    db.SubmitChanges();
                }
            }

        }
        #endregion



    }
}