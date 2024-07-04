using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.BAL;
using UserMgt.DAL;
using UserMgt.Entity;

namespace DeffinityManager.UserManagement.BAL
{
   public class ResourceScheduleBAL
    {
        IUserRepository<ResourceSchedule>Resource_Repository=null;
        IUserRepository<UserMgt.Entity.Contractor> Contractor_Repository = null;
        IUserRepository<UserMgt.Entity.UserToCompany> cCompany_Repository = null;
 
     public ResourceScheduleBAL()
    {
        Resource_Repository = new UserRepository<ResourceSchedule>();
        Contractor_Repository = new UserRepository<UserMgt.Entity.Contractor>();
        cCompany_Repository = new UserRepository<UserMgt.Entity.UserToCompany>();
    }
     public IQueryable<ResourceSchedule> GetAllEvents()
     {
         return Resource_Repository.GetAll();
     }
     public List<UserMgt.Entity.Contractor> GetAllContractors()
     {
         var llist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany();
         return Contractor_Repository.GetAll().Where(o => llist.Contains(o.ID)).ToList();
     }
     public void UpdateEvent(ResourceSchedule Editevent)
     {
         Resource_Repository.Edit(Editevent);
     }
     public void DeleteEvent(ResourceSchedule Delevent)
     {
         Resource_Repository.Delete(Delevent);
     }

 }
}
