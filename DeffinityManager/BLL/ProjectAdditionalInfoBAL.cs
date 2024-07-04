using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMgt.Entity;
using ProjectMgt.DAL;
namespace ProjectMgt.BAL
{
    /// <summary>
    /// Summary description for ProjectAdditionalInfoBAL
    /// </summary>
    public class ProjectAdditionalInfoBAL :IDisposable
    {
        IProjectRepository<ProjectAdditionalInfo> pi_repsitory = null;//= new ProjectRepository<CustomField>();
        public ProjectAdditionalInfoBAL()
        {
            //
            // TODO: Add constructor logic here
            //
            pi_repsitory = new ProjectRepository<ProjectAdditionalInfo>();
        }

        public void ProjectAdditionalInfo_Insert(ProjectAdditionalInfo pi)
        {
            pi_repsitory.Add(pi);
        }
        public void ProjectAdditionalInfo_Update(ProjectAdditionalInfo pi)
        {
            pi_repsitory.Edit(pi);
        }

        public ProjectAdditionalInfo ProjectAdditionalInfo_SelectByID(int id)
        {
           return pi_repsitory.GetById(id);
        }

        public IQueryable<ProjectAdditionalInfo> ProjectAdditionalInfo_SelectByProject(int ProjectReference)
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