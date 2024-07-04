using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;

namespace DeffinityManager.PortfolioMgt.BAL
{
    public class PortfolioTrackerLoginBAL
    {

        public static bool PortfolioTrackerLoginBAL_CheckUserExists(string Username, string Password)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioTrackerLogin> pRep = new PortfolioRepository<PortfolioTrackerLogin>();
            //check the user
            var plist = pRep.GetAll().ToList();
            //if (plist.Count == 0)
            //{
            //    //add defualt users
            //    PortfolioTrackerLoginBAL_Add("Nadeem", "nadeem.mohammed@123smartpro.com", "nabeeha");
            //    PortfolioTrackerLoginBAL_Add("Mike", "mike.boreham@123smartpro.com", "mike123");
            //}
            //if (plist.Count == 3)
            //{
                
            //    //add defualt users
            //    //PortfolioTrackerLoginBAL_Add("Cardconnect", "123@cardconnect.com", "Cardconnect123!");
            //    PortfolioTrackerLoginBAL_Add("Mike boreham", "Mike.boreham@123smartpro.com", "123CardConnect");
            //    PortfolioTrackerLoginBAL_Add("Sneville", "Sneville@cardconnect.com", "123CardConnect");
            //    PortfolioTrackerLoginBAL_Add("Mike dodier", "Mike.dodier@123smartpro.com", "123CardConnect");
            //}
            //if (plist.Count == 5)
            //{

            //    //add defualt users
            //    //PortfolioTrackerLoginBAL_Add("Cardconnect", "123@cardconnect.com", "Cardconnect123!");
            //    PortfolioTrackerLoginBAL_Add("Karlaperez", "karlaperez-t@porch.com", "guardian",sessionKeys.PartnerID);
            //    PortfolioTrackerLoginBAL_Add("Carlosurquizo", "carlosurquizo-t@porch.com", "guardian", sessionKeys.PartnerID);
            //    PortfolioTrackerLoginBAL_Add("Toms", "toms@porch.com", "guardian", sessionKeys.PartnerID);
            //}

            //if (plist.Count == 8)
            //{
            //    PortfolioTrackerLoginBAL_Add("Greg", "greggperry19@gmail.com", "Smarthvacalliance123#", sessionKeys.PartnerID);
            //    PortfolioTrackerLoginBAL_Add("Steve", "steve@lesslergroup.com", "Smarthvacalliance123#", sessionKeys.PartnerID);
            //    PortfolioTrackerLoginBAL_Add("Tom", "tomw@serviceenergy.com", "Smarthvacalliance123#", sessionKeys.PartnerID);
            //}
            //if (plist.Count == 11)
            //{
            //    //add defualt users
            //    PortfolioTrackerLoginBAL_Add("Aaron", "aaron@smallbizguardian.com", "guardian", sessionKeys.PartnerID);
            //    PortfolioTrackerLoginBAL_Add("Ben", "ben@smallbizguardian.com", "guardian", sessionKeys.PartnerID);
            //}

            //if (plist.Count == 13)
            //{
            //    //1 partner is porch
            //    //add defualt users
            //    PortfolioTrackerLoginBAL_Add("Carlosurquizo", "carlosurquizo-t@porch.com", "porch123#!", 1);
            //    PortfolioTrackerLoginBAL_Add("Alejandraarias", "alejandraarias-t@porch.com", "porch123#!", 1);
            //    PortfolioTrackerLoginBAL_Add("Karlaperez", "karlaperez-t@porch.com", "porch123#!", 1);
            //}
            //

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
        public static bool PortfolioTrackerLoginBAL_Add(string DisplayName, string Username, string Password,int partnerID)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioTrackerLogin> pRep = new PortfolioRepository<PortfolioTrackerLogin>();
            var p = pRep.GetAll().Where(o => o.Username == Username.Trim() && o.PartnerID == partnerID).FirstOrDefault();
            if (p == null)
            {
                p = new PortfolioTrackerLogin();
                p.IsActive = true;
                p.Password = Deffinity.Users.Login.GeneratePasswordString(Password);
                p.Username = Username;
                p.DisplayName = DisplayName;
                p.BaseURL = "site.faithunionhub.com";
                p.PartnerID = partnerID;
                pRep.Add(p);
            }

            return retval;
        }

        public static PortfolioTrackerLogin PortfolioTrackerLoginBAL_AddNew(PortfolioTrackerLogin c)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioTrackerLogin> pRep = new PortfolioRepository<PortfolioTrackerLogin>();
            var p = pRep.GetAll().Where(o => o.Username == c.Username.Trim() && o.PartnerID == c.PartnerID).FirstOrDefault();
            if (p == null)
            {
                p = new PortfolioTrackerLogin();
                p.IsActive = true;
                p.Password = Deffinity.Users.Login.GeneratePasswordString(c.Password);
                p.Username = c.Username;
                p.DisplayName = c.DisplayName;
                p.BaseURL = "site.faithunionhub.com";
                p.PartnerID = c.PartnerID;
                pRep.Add(p);
            }

            return p;
        }

        public static bool PortfolioTrackerLoginBAL_Update(int userid, string DisplayName, string Password)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioTrackerLogin> pRep = new PortfolioRepository<PortfolioTrackerLogin>();
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

        public static PortfolioTrackerLogin PortfolioTrackerLoginBAL_UpdateNew(PortfolioTrackerLogin c)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioTrackerLogin> pRep = new PortfolioRepository<PortfolioTrackerLogin>();
            var p = pRep.GetAll().Where(o => o.ID == c.ID).FirstOrDefault();
            if (p != null)
            {
                p.IsActive = true;
                if(!string.IsNullOrEmpty( c.Password))
                p.Password = Deffinity.Users.Login.GeneratePasswordString(c.Password);
                p.DisplayName = c.DisplayName;
                p.Username = c.Username;
                p.PartnerID = c.PartnerID;
                
                pRep.Edit(p);
            }

            return p;
        }

        public static List<PortfolioTrackerLogin> PortfolioTrackerLoginBAL_SelectAll()
        {
          
            IPortfolioRepository<PortfolioTrackerLogin> pRep = new PortfolioRepository<PortfolioTrackerLogin>();
            return pRep.GetAll().ToList();
          
        }
        public static bool PortfolioTrackerLoginBAL_Delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioTrackerLogin> pRep = new PortfolioRepository<PortfolioTrackerLogin>();
            var c= pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

            if (c != null)
            {
                pRep.Delete(c);
                retval = true;
            }

            return retval;

        }
    }
}
