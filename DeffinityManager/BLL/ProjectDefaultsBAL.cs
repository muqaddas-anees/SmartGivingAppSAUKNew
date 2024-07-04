using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMgt.BLL
{
    public class ProjectDefaultsBAL
    {

        public static ProjectMgt.Entity.ProjectDefault ProjectDefaultsBAL_Add(ProjectMgt.Entity.ProjectDefault cat)
        {
            IProjectRepository<ProjectMgt.Entity.ProjectDefault> pRep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
           

            pRep.Add(cat);
            return cat;
        }

        public static ProjectMgt.Entity.ProjectDefault ProjectDefaultsBAL_Update(ProjectMgt.Entity.ProjectDefault mat)
        {
            IProjectRepository<ProjectMgt.Entity.ProjectDefault> pRep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.Address = mat.Address;
                s.AnnualYearEnd = mat.AnnualYearEnd;
                s.AnnualYearStart = mat.AnnualYearStart;
                s.ApplicationName = mat.ApplicationName;
                s.ApprovalBoardEmail = mat.ApprovalBoardEmail;
                s.Basicholidayallowancefornewjoiners = mat.Basicholidayallowancefornewjoiners;
                s.Case_Custom1 = mat.Case_Custom1;
                s.Case_Custom10 = mat.Case_Custom10;
                s.Case_Custom11 = mat.Case_Custom11;
                s.Case_Custom12 = mat.Case_Custom12;
                s.Case_Custom13 = mat.Case_Custom13;
                s.Case_Custom14 = mat.Case_Custom14;
                s.Case_Custom15 = mat.Case_Custom15;
                s.Case_Custom16 = mat.Case_Custom16;
                s.Case_Custom2 = mat.Case_Custom2;
               // s.

            }
          //  pRep.Edit(s);
            return s;
        }

       
        public static IQueryable<ProjectMgt.Entity.ProjectDefault> ProjectDefaultsBAL_SelectAll()
        {
            IProjectRepository<ProjectMgt.Entity.ProjectDefault> pRep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
            return pRep.GetAll();

        }

    }
}
