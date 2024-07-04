using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgt.BAL
{
     public class CompanyBAL
    {

        public static void CompanyBAL_Update(UserMgt.Entity.Company c)
        {
            IUserRepository<UserMgt.Entity.Company> uRep = new UserRepository<UserMgt.Entity.Company>();
            var u = uRep.GetAll().Where(o => o.ID == c.ID).FirstOrDefault();
            if(u != null)
            {
                u.Address = c.Address;
                u.City = c.City;
                u.Name = c.Name;
                u.Payment_host = c.Payment_host;
                u.Payment_password = c.Payment_password;
                u.Payment_username = c.Payment_username;
                u.Payment_vendor = c.Payment_vendor;
                u.TaxReference = c.TaxReference;
                u.Town = c.Town;
                u.Zipcode = c.Zipcode;
                u.UpgradDescription = c.UpgradDescription;
                u.TrainingDescription = c.TrainingDescription;
                uRep.Edit(u);
            }
        }

        public static UserMgt.Entity.Company CompanyBAL_Select()
        {
            IUserRepository<UserMgt.Entity.Company> uRep = new UserRepository<UserMgt.Entity.Company>();
            return uRep.GetAll().FirstOrDefault();
        }
    }
}
