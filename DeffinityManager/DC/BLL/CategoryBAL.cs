using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
/// <summary>
/// Summary description for CategoryBAL
/// </summary>

    public class CategoryBAL
    {
        public CategoryBAL()
        {

            //
            // TODO: Add constructor logic here
            //
        }

        #region Get list of Category
        public static IEnumerable<Category> GetCategoryList()
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.Categories.OrderBy(c=>c.Name).Where(o=>o.CustomerID== sessionKeys.PortfolioID).ToList();
            }
        }

        public static IEnumerable<Category> GetCategoryList(int typeID)
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.Categories.OrderBy(c => c.Name).Where(o => o.TypeOfRequestID == typeID).ToList();
            }
        }
        #endregion

        #region Category insert
        public static Category AddCategory(int typeID, string name)
        {
            var category = new Category();
            using (DCDataContext db = new DCDataContext())
            {
                if (db.Categories.Where(o => o.Name.ToLower() == name.ToLower() && o.TypeOfRequestID == typeID).Count() == 0)
                {
                    
                    category.TypeOfRequestID = typeID;
                    category.Name = name;
                    category.CustomerID = sessionKeys.PortfolioID;
                    db.Categories.InsertOnSubmit(category);
                    db.SubmitChanges();
                }
            }

            return category;
        }

        public static Category UpdateCategory(int id, string name)
        {
            var category = new Category();
            using (DCDataContext db = new DCDataContext())
            {
                category = db.Categories.Where(o => o.ID == id).FirstOrDefault();
                if (db.Categories.Where(o => o.Name.ToLower() == name.ToLower() && o.TypeOfRequestID == category.TypeOfRequestID && o.ID != id).Count() == 0)
                {
                    
                    category.Name = name;
                    //db.Categories.InsertOnSubmit(category);
                    db.SubmitChanges();
                }
            }
            return category;
        }
        public static void AddCategory(Category category)
        {
            using (DCDataContext db = new DCDataContext())
            {
                category.CustomerID = sessionKeys.PortfolioID;
                db.Categories.InsertOnSubmit(category);
                db.SubmitChanges();
            }
        }
        #endregion

        #region Category update
        public static void UpdateCategory(Category category)
        {
            using (DCDataContext db = new DCDataContext())
            {
                Category categoryCurrent = db.Categories.Where(r => r.ID == category.ID).FirstOrDefault();
                categoryCurrent.Name = category.Name;
                db.SubmitChanges();
            }
        }
        #endregion

        #region Check Category when an insert
        public static bool CheckCategory(string name)
        {
            bool exists = false;
            using (DCDataContext db = new DCDataContext())
            {
                Category category = db.Categories.Where(r => r.Name == name && r.CustomerID == sessionKeys.PortfolioID).FirstOrDefault();
                if (category != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Check Category when an update
        public static bool CheckCategory(int id, string name)
        {
            bool exists = false;
            using (DCDataContext db = new DCDataContext())
            {
                Category category = db.Categories.Where(r => r.ID != id && r.CustomerID == sessionKeys.PortfolioID && r.Name == name).FirstOrDefault();
                if (category != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Select by Category Id
        public static Category SelectByID(int id)
        {
            Category category = new Category();
            using (DCDataContext db = new DCDataContext())
            {
                category = db.Categories.Where(r => r.ID == id).FirstOrDefault();
            }
            return category;
        }

        #endregion

        #region Delete Category by Id
        public static void DeleteByID(int id)
        {
            using (DCDataContext db = new DCDataContext())
            {
                Category category = db.Categories.Where(r => r.ID == id).FirstOrDefault();
                if (category != null)
                {
                    db.Categories.DeleteOnSubmit(category);
                    db.SubmitChanges();
                }
            }

        }
        #endregion
    }
}