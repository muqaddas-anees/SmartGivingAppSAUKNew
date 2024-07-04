using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgt.BAL
{
    public class UserSkillBAL
    {
        public static UserMgt.Entity.UserSkill UserSkillBAL_Add(UserMgt.Entity.UserSkill cat)
        {
            IPortfolioRepository<UserMgt.Entity.UserSkill> pRep = new PortfolioRepository<UserMgt.Entity.UserSkill>();


            pRep.Add(cat);
            return cat;
        }

        public static UserMgt.Entity.UserSkill UserSkillBAL_Update(UserMgt.Entity.UserSkill mat)
        {
            IPortfolioRepository<UserMgt.Entity.UserSkill> pRep = new PortfolioRepository<UserMgt.Entity.UserSkill>();
            var s = pRep.GetAll().Where(o => o.UserId == mat.UserId).FirstOrDefault();
            if (s != null)
            {
                s.Skills = mat.Skills;
                s.Notes = mat.Notes;
               // s.OrganizationID = mat.OrganizationID;
            }
            pRep.Edit(s);
            return s;
        }

        public static bool UserSkillBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<UserMgt.Entity.UserSkill> pRep = new PortfolioRepository<UserMgt.Entity.UserSkill>();
            var retEntity = pRep.GetAll().Where(o => o.Id == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<UserMgt.Entity.UserSkill> UserSkillBAL_SelectAll()
        {
            IPortfolioRepository<UserMgt.Entity.UserSkill> pRep = new PortfolioRepository<UserMgt.Entity.UserSkill>();
            return pRep.GetAll();

        }

    }
}
