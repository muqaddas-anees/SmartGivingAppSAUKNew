using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMgt.Entity;
using ProjectMgt.DAL;


namespace ProjectMgt.BAL
{
    /// <summary>
    /// Summary description for CustomFieldsBAL
    /// </summary>
    public class CustomFieldsBAL : IDisposable
    {
        IProjectRepository<CustomField> cf_repsitory = null;//= new ProjectRepository<CustomField>();
        
        public CustomFieldsBAL()
        {
            //
            // TODO: Add constructor logic here
            //
            cf_repsitory = new ProjectRepository<CustomField>();
        }

        public void CustomFields_Insert(CustomField cf)
        {
            cf_repsitory.Add(cf);
        }
        public void CustomFields_update(CustomField cf)
        {
            cf_repsitory.Edit(cf);
        }
        public CustomField CustomFields_SelectByID(int id)
        {
            return cf_repsitory.GetAll().Where(p=>p.ID == id).FirstOrDefault();
        }

        public IQueryable<CustomField> CustomFields_SelectAll()
        {
            return cf_repsitory.GetAll();
        }

        public void CustomFields_DeleteByID(int id)
        {
            CustomField cf = CustomFields_SelectByID(id);
            cf_repsitory.Delete(cf);
        }

        public void Dispose()
        {
            if (cf_repsitory != null)
                cf_repsitory.Dispose();
        }
    }
}