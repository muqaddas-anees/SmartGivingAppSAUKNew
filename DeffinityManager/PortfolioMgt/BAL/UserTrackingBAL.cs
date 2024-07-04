using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;

namespace PortfolioMgt.BAL
{
    public class UserTrackingBAL
    {

        public static UserTracking UserTrackingBAL_add(int userid, int portfolioid, decimal Latitude, decimal Longitude)
        {
            IPortfolioRepository<UserTracking> urep = new PortfolioRepository<UserTracking>();
            var u = new UserTracking();
            u.Latitude = Latitude;
            u.Longitude = Longitude;
            u.PortfolioID = portfolioid;
            u.UserID = userid;
            u.LoggedDateTime = DateTime.Now;
            urep.Add(u);

            return u;
        }

        public static UserTracking UserTrackingBAL_selectlatest(int userid)
        {
            IPortfolioRepository<UserTracking> urep = new PortfolioRepository<UserTracking>();
            return urep.GetAll().Where(p => p.UserID == userid).OrderByDescending(o=>o.ID).FirstOrDefault();
        }
        public static List<UserTracking> UserTrackingBAL_selectallbyuser(int userid)
        {
            IPortfolioRepository<UserTracking> urep = new PortfolioRepository<UserTracking>();
            return urep.GetAll().Where(p => p.UserID == userid).ToList();
        }
    }
}
