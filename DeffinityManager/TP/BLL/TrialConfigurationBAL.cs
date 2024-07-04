using System;
using System.Collections.Generic;
using System.Web;
using Therapy.DAL;
using Therapy.Entity;
using System.Linq;
namespace TP.BLL
{
    /// <summary>
    /// Summary description for TrialBAL
    /// </summary>
    public class TrialConfigurationBAL
    {

        #region Trial List
        public static IEnumerable<TrialConfiguration> TrialList()
        {
            List<TrialConfiguration> trialConfiguration = new List<TrialConfiguration>();

            using (TherapyDataContext db = new TherapyDataContext())
            {
                //trialConfiguration = db.TrialConfigurations.Select(t=>t).ToList();
            }

            return trialConfiguration;
        }
        #endregion

        #region insert
        public static void TrialInsert(TrialConfiguration trialConfiguration)
        {
            using (TherapyDataContext db = new TherapyDataContext())
            {
                db.TrialConfigurations.InsertOnSubmit(trialConfiguration);
                db.SubmitChanges();
            }
        }
        #endregion

        #region Select by id
        public static TrialConfiguration Trial_SelectByID(int id)
        {
            TrialConfiguration tc = new TrialConfiguration();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                tc = (from t in td.TrialConfigurations
                        where t.ID == id
                      select t).FirstOrDefault();
            }

            return tc;
        }
        #endregion

        #region Select by Trial
        public static TrialConfiguration Trial_SelectByTrial(string trial)
        {
            TrialConfiguration tc = new TrialConfiguration();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                tc = (from t in td.TrialConfigurations
                      where t.Name == trial
                      select t).FirstOrDefault();
            }

            return tc;
        }
        #endregion

        #region Check exist to update
        public static bool Trial_ExistUpdate(string strname, int id)
        {
            TrialConfiguration tc = new TrialConfiguration();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                tc = (from t in td.TrialConfigurations
                           where t.Name.ToLower() == strname.ToLower() && t.ID != id
                           select t).FirstOrDefault();
            }
            if (tc == null)
                return false;
            else
                return true;

        }
        #endregion

        #region Update
        public static void Trial_Update(TrialConfiguration t)
        {
            using (TherapyDataContext td = new TherapyDataContext())
            {
                TrialConfiguration tc = (from tr in td.TrialConfigurations
                                         where tr.ID == t.ID
                                         select tr).FirstOrDefault();
                tc.Name = t.Name;
                tc.Status = t.Status;
                tc.TrailStartDate = t.TrailStartDate;
                td.SubmitChanges();
            }

        }
        #endregion

        #region Check exist
        public static bool Trial_Exist(string strname)
        {
            TrialConfiguration tc = new TrialConfiguration();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                tc = (from t in td.TrialConfigurations
                           where t.Name.ToLower() == strname.ToLower()
                           select t).FirstOrDefault();
            }
            if (tc == null)
                return false;
            else
                return true;

        }
        #endregion

        #region Delete
        public static void TrialConfiguration_Delete(int id)
        {
            using (TherapyDataContext td = new TherapyDataContext())
            {

                var trial = (from t in td.TrialConfigurations
                                 where t.ID == id
                                 select t).FirstOrDefault();
                td.TrialConfigurations.DeleteOnSubmit(trial);
                td.SubmitChanges();

            }
        }
        #endregion

        #region Select all
        public static List<TrialConfiguration> Trial_selectAll()
        {
            List<TrialConfiguration> tc = new List<TrialConfiguration>();
            using (TherapyDataContext td = new TherapyDataContext())
            {
                tc = (from t in td.TrialConfigurations select t).ToList();
            }
            return tc;
        }
        #endregion

    }


    
}