using System;
using System.Collections.Generic;
using System.Web;
using Therapy.DAL;
using Therapy.Entity;
using System.Linq;
namespace TP.BLL
{
    /// <summary>
    /// Summary description for PatientTrialbyDateBAL
    /// </summary>
    public class PatientTrialbyDateBAL
    {
        #region insert
        public static void Insert(PatientTrailbyDate PatientTrailbyDate)
        {
            using (TherapyDataContext db = new TherapyDataContext())
            {
                db.PatientTrailbyDates.InsertOnSubmit(PatientTrailbyDate);
                db.SubmitChanges();
            }
        }
        #endregion
        #region Select by UserID and TrialID
        public static PatientTrailbyDate SelectByUserID(int UserID, int TrialID)
        {
            PatientTrailbyDate tc = new PatientTrailbyDate();
            using (TherapyDataContext td = new TherapyDataContext())
            {
                tc = (from t in td.PatientTrailbyDates where t.UserID == UserID && t.TrialID == TrialID select t).FirstOrDefault();
            }
            return tc;
        }
        #endregion
        #region Select by ID
        public static PatientTrailbyDate SelectByID(int id)
        {
            PatientTrailbyDate tc = new PatientTrailbyDate();
            using (TherapyDataContext td = new TherapyDataContext())
            {
                tc = (from t in td.PatientTrailbyDates where t.ID == id select t).FirstOrDefault();
            }
            return tc;
        }
        #endregion
        #region Delete
        public static void Delete(int id)
        {
            using (TherapyDataContext td = new TherapyDataContext())
            {

                var trial = (from t in td.PatientTrailbyDates
                             where t.ID == id
                             select t).FirstOrDefault();
                td.PatientTrailbyDates.DeleteOnSubmit(trial);
                td.SubmitChanges();

            }
        }
        #endregion
        #region Update
        public static void Update(PatientTrailbyDate t)
        {
            using (TherapyDataContext td = new TherapyDataContext())
            {
                PatientTrailbyDate tc = (from tr in td.PatientTrailbyDates
                                         where tr.ID == t.ID
                                         select tr).FirstOrDefault();
             //   tc.FlagStartedFromBeginning = t.FlagStartedFromBeginning;
                tc.UserID = t.UserID;
                tc.TrialID = t.TrialID;
                td.SubmitChanges();
            }

        }
        #endregion

    }
}