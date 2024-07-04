using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Therapy.DAL;
using Therapy.Entity;

namespace TP.BLL
{
    public class DashBoardBAL
    {
        #region Select by trail id
        public static List<KeyMarker> KeyMarker_selectByTrailID(int tid)
        {
            List<KeyMarker> km = new List<KeyMarker>();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                km = (from t in td.KeyMarkers
                      where t.TrialID == tid
                      select t).ToList();
            }

            return km;
        }
        #endregion

        #region Select all
        public static List<TrialConfiguration> Trial_selectAll()
        {
            List<TrialConfiguration> tc = new List<TrialConfiguration>();
            using (TherapyDataContext td = new TherapyDataContext())
            {
                tc = (from t in td.TrialConfigurations where t.Status==true
                          orderby t.Name select t).ToList();
            }
            return tc;
        }
        #endregion
    }
}