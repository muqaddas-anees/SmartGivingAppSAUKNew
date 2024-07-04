using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMgt.Entity;
using ProjectMgt.DAL;

namespace ProjectMgt.BAL
{
    /// <summary>
    /// Summary description for ProjectAdminGroupBAL
    /// </summary>
    public class ProjectAdminGroupBAL :IDisposable
    {
        IProjectRepository<ProjectAdminGroup> ProjectAdminGroup_Repository = null;
        //IUserRepository<UserMgt.Entity.Contractor> Contractor_Repository = null;
        public ProjectAdminGroupBAL()
        {
            //
            // TODO: Add constructor logic here
            //
            ProjectAdminGroup_Repository = new ProjectRepository<ProjectAdminGroup>();
           // Contractor_Repository = new UserRepository<UserMgt.Entity.Contractor>();
        }
        //Insert
        public void ProjectAdminGroup_Insert(ProjectAdminGroup pg)
        {
            ProjectAdminGroup_Repository.Add(pg);
        }
        //Delete
        public void ProjectAdminGroup_Delete(ProjectAdminGroup pg)
        {
            ProjectAdminGroup_Repository.Delete(pg);
        }

        public void ProjectAdminGroup_DeleteByID(int ID)
        {
            ProjectAdminGroup pg = ProjectAdminGroup_Repository.GetAll().Where(p=>p.ID == ID).FirstOrDefault();
            if(pg != null)
            ProjectAdminGroup_Repository.Delete(pg);
        }
        //Select All
        public IEnumerable<ProjectAdminGroup> ProjectAdminGroup_SelectAll()
        {
            return ProjectAdminGroup_Repository.GetAll();
        }

        public void Dispose()
        {
            if (ProjectAdminGroup_Repository != null)
                ProjectAdminGroup_Repository.Dispose();
        }
    }
}