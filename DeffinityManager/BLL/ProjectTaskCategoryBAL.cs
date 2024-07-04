using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

namespace ProjectMgt.BLL
{
    /// <summary>
    /// Summary description for ProjectTaskCategoryBAL
    /// </summary>
    public class ProjectTaskCategoryBAL
    {
        public ProjectTaskCategoryBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Get list of ProjectTaskCategory
        public static IEnumerable<ProjectTaskCategory> GetCategoryList()
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                return db.ProjectTaskCategories.OrderBy(c => c.Name).ToList();
            }
        }
        #endregion

        #region ProjectTaskCategory insert
        public static void AddCategory(ProjectTaskCategory category)
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                db.ProjectTaskCategories.InsertOnSubmit(category);
                db.SubmitChanges();
            }
        }
        #endregion

        #region ProjectTaskCategory update
        public static void UpdateCategory(ProjectTaskCategory category)
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                ProjectTaskCategory categoryCurrent = db.ProjectTaskCategories.Where(r => r.ID == category.ID).FirstOrDefault();
                categoryCurrent.Name = category.Name;
                db.SubmitChanges();
            }
        }
        #endregion

        #region Check ProjectTaskCategory when an insert
        public static bool CheckCategory(string name)
        {
            bool exists = false;
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                ProjectTaskCategory category = db.ProjectTaskCategories.Where(r => r.Name == name).FirstOrDefault();
                if (category != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Check ProjectTaskCategory when an update
        public static bool CheckCategory(int id, string name)
        {
            bool exists = false;
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                ProjectTaskCategory category = db.ProjectTaskCategories.Where(r => r.ID != id && r.Name == name).FirstOrDefault();
                if (category != null)
                    exists = true;
            }

            return exists;
        }
        #endregion

        #region Select by ProjectTaskCategory Id
        public static ProjectTaskCategory SelectByID(int id)
        {
            ProjectTaskCategory category = new ProjectTaskCategory();
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                category = db.ProjectTaskCategories.Where(r => r.ID == id).FirstOrDefault();
            }
            return category;
        }

        #endregion

        #region Delete ProjectTaskCategory by Id
        public static void DeleteByID(int id)
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                ProjectTaskCategory category = db.ProjectTaskCategories.Where(r => r.ID == id).FirstOrDefault();
                if (category != null)
                {
                    db.ProjectTaskCategories.DeleteOnSubmit(category);
                    db.SubmitChanges();
                }
            }

        }
        #endregion
    }
}