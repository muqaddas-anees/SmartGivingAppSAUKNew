using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;


namespace DeffinityManager.Portfolio.BAL
{
    public class PortfolioAdminLoginBAL
    {
        //check user is exists or not
        public static bool PortfolioAdminLoginBAL_CheckUserExists(string Username, string Password)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioAdminLogin> pRep = new PortfolioRepository<PortfolioAdminLogin>();
            //check the user
            var plist = pRep.GetAll().ToList();
            if(plist.Count ==0)
            {
                //add defualt users
                PortfolioAdminLoginBAL_Add("Nadeem", "nadeem.mohammed@123smartpro.com", "nabeeha");
                PortfolioAdminLoginBAL_Add("Mike", "mike.boreham@123smartpro.com", "mike123");
                
            }

            if (plist.Count == 2)
            {
                //add defualt users
                PortfolioAdminLoginBAL_Add("Sharon", "sharondbutler@gmail.com ", "Sharon123#!");

            }

            var p = pRep.GetAll().Where(o => o.Username.ToLower() == Username.ToLower().Trim() && o.Password == Deffinity.Users.Login.GeneratePasswordString(Password)).FirstOrDefault();
            if (p != null)
            {
                sessionKeys.UID = p.ID;
                sessionKeys.SID = 1;
                sessionKeys.UName = p.DisplayName;
               
                retval = true;
            }

            return retval;
        }

        //add user
        public static bool PortfolioAdminLoginBAL_Add(string DisplayName,string Username, string Password)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioAdminLogin> pRep = new PortfolioRepository<PortfolioAdminLogin>();
            var p = pRep.GetAll().Where(o => o.Username == Username.Trim()).FirstOrDefault();
            if (p == null)
            {
                p = new PortfolioAdminLogin();
                p.IsActive = true;
                p.Password = Deffinity.Users.Login.GeneratePasswordString(Password);
                p.Username = Username;
                p.DisplayName = DisplayName;
                pRep.Add(p);
            }

            return retval;
        }

        public static bool PortfolioAdminLoginBAL_Update(int userid,string DisplayName,string Password)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioAdminLogin> pRep = new PortfolioRepository<PortfolioAdminLogin>();
            var p = pRep.GetAll().Where(o => o.ID == userid).FirstOrDefault();
            if (p != null)
            {
                p.IsActive = true;
                p.Password = Deffinity.Users.Login.GeneratePasswordString(Password);
                p.DisplayName = DisplayName;
                pRep.Edit(p);
            }

            return retval;
        }
    }
    //
}
