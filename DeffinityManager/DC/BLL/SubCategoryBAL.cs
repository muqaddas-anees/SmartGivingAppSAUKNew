using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;
/// <summary>
/// Summary description for SubCategoryBAL
/// </summary>
/// 

namespace DC.BLL
{
    public class SubCategoryBAL
    {
        public SubCategoryBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Get list of SubCategory
        public static IEnumerable<SubCategory> GetSubCategoryList()
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.SubCategories.OrderBy(c => c.Name).ToList();
            }
        }

        public static List<SubCategory> GetSubCategoryList(int categoryid)
        {
            IDCRespository<SubCategory> sRep = new DCRepository<SubCategory>();
            return sRep.GetAll().Where(o => o.CategoryID == categoryid).OrderBy(c => c.Name).ToList();
            //using (DCDataContext db = new DCDataContext())
            //{
            //    return db.SubCategories.Where(o => o.CategoryID == categoryid).OrderBy(c => c.Name).ToList();
            //}
        }

        #endregion

        #region SubCategory insert
        public static SubCategory AddSubCategory(int categoryid, string name)
        {
            var subCategory = new SubCategory();
            using (DCDataContext db = new DCDataContext())
            {
                if (db.SubCategories.Where(o => o.CategoryID == Convert.ToInt32(categoryid) && o.Name.ToLower() == name.ToLower()).Count() == 0)
                {
                    subCategory.CategoryID = Convert.ToInt32(categoryid);
                    subCategory.Name = name;

                    db.SubCategories.InsertOnSubmit(subCategory);
                    db.SubmitChanges();
                }
            }
            return subCategory;
        }
        public static void AddSubCategory(SubCategory subCategory)
        {
            using (DCDataContext db = new DCDataContext())
            {
                db.SubCategories.InsertOnSubmit(subCategory);
                db.SubmitChanges();
            }
        }
        #endregion

        #region SubCategory update

        public static SubCategory UpdateSubCategory(int id, string name)
        {
            var subCategory = new SubCategory();
            using (DCDataContext db = new DCDataContext())
            {
                subCategory = db.SubCategories.Where(r => r.ID == id).FirstOrDefault();
                if (db.SubCategories.Where(o => o.CategoryID == subCategory.CategoryID && o.Name.ToLower() == name.ToLower() && o.ID != id).Count() == 0)
                {
                    subCategory.Name = name;
                    db.SubmitChanges();
                }
            }
            return subCategory;
        }
        public static void UpdateSubCategory(SubCategory subCategory)
        {
            using (DCDataContext db = new DCDataContext())
            {
                SubCategory categoryCurrent = db.SubCategories.Where(r => r.ID == subCategory.ID).FirstOrDefault();
                categoryCurrent.Name = subCategory.Name;
                db.SubmitChanges();
            }
        }
        #endregion

        #region Check SubCategory when an insert
        public static bool CheckSubCategory(string name, int categoryId)
        {
            bool exists = false;
            using (DCDataContext db = new DCDataContext())
            {
                SubCategory subCategory = db.SubCategories.Where(r => r.Name == name && r.CategoryID == categoryId).FirstOrDefault();
                if (subCategory != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Check SubCategory when an update
        public static bool CheckSubCategory(int id, int categoryId, string name)
        {
            bool exists = false;
            using (DCDataContext db = new DCDataContext())
            {
                SubCategory subCategory = db.SubCategories.Where(r => r.ID != id && r.CategoryID == categoryId && r.Name == name).FirstOrDefault();
                if (subCategory != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Select by SubCategory Id
        public static SubCategory SelectByID(int id)
        {
            SubCategory subCategory = new SubCategory();
            using (DCDataContext db = new DCDataContext())
            {
                subCategory = db.SubCategories.Where(r => r.ID == id).FirstOrDefault();
            }
            return subCategory;
        }

        #endregion

        #region Delete SubCategory by Id
        public static void DeleteByID(int id)
        {
            using (DCDataContext db = new DCDataContext())
            {
                SubCategory subCategory = db.SubCategories.Where(r => r.ID == id).FirstOrDefault();
                if (subCategory != null)
                {
                    db.SubCategories.DeleteOnSubmit(subCategory);
                    db.SubmitChanges();
                }
            }

        }
        #endregion
    }

}