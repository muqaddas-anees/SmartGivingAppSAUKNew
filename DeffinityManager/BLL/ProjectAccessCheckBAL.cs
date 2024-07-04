using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using ProjectMgt.DAL;
using ProjectMgt.Entity;

namespace ProjectMgt.BAL
{
   
    /// <summary>
    /// Summary description for ProjectAccessCheckBAL
    /// </summary>
    /// 
    public class ProjectAccessCheckBAL :IDisposable
    {
        IProjectRepository<ProjectAccessCheck> pi_repsitory = null;//= new ProjectRepository<CustomField>();
        public ProjectAccessCheckBAL()
        {

            pi_repsitory = new ProjectRepository<ProjectAccessCheck>();
        }

        public void ProjectAccessCheckBAL_Insert(ProjectAccessCheck p)
        {
            pi_repsitory.Add(p);
        }

        public void ProjectAccessCheckBAL_Update(ProjectAccessCheck pc)
        {
            pi_repsitory.Edit(pc);
        }
       

        public IQueryable<ProjectAccessCheck> ProjectAccessCheckBAL_SelectByProject(int ProjectReference)
        {
            return pi_repsitory.GetAll().Where(p => p.ProjectReference == ProjectReference);
        }

        public void Dispose()
        {
            if (pi_repsitory != null)
                pi_repsitory.Dispose();
        }
    }
}