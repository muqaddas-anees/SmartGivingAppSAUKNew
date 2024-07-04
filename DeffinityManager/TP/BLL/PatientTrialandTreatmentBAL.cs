using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Therapy.DAL;
using Therapy.Entity;

/// <summary>
/// Summary description for PatientTrialandTreatmentBAL
/// </summary>
/// 
namespace TP.BLL
{
    public class PatientTrialandTreatmentBAL
    {
        #region Insert
        public static void PatientTrialandTreatment_Insert(PatientTrialsAndTreatment tp)
        {
            using (TherapyDataContext db = new TherapyDataContext())
            {
                db.PatientTrialsAndTreatments.InsertOnSubmit(tp);
                db.SubmitChanges();
            }
        }
        #endregion

        #region Select by trail id
        public static List<PatientTrialsAndTreatment> PatientTrialandTreatment_selectByTrailID(int tid)
        {
            List<PatientTrialsAndTreatment> tm = new List<PatientTrialsAndTreatment>();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                tm = (from t in td.PatientTrialsAndTreatments
                      where t.TrialID == tid
                      select t).ToList();
            }

            return tm;
        }
        #endregion

        #region Select all primary treatments
        public static List<PatientTrailsandTreatmentList> PatientTrailsandPrimaryTreatmentList_selectAll(int uid)
        {

            List<PatientTrailsandTreatmentList> pt = new List<PatientTrailsandTreatmentList>();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                var trial = td.TrialConfigurations.Select(p => p).ToList();
                var patientTherapy = td.PatientTrialsAndTreatments.Select(d => d).ToList();
                var primarytreatment = td.Treatments.Select(d => d).ToList();

                pt = (from trl in primarytreatment
                      join p in patientTherapy on trl.ID equals p.TreatmentID
                      //join t in patientTherapy on trl.ID equals t.TrialID
                      join t in trial on p.TrialID equals t.ID

                      where p.UserID == uid && p.TypeofTreatment == "PrimaryTreatment"
                      select new PatientTrailsandTreatmentList
                      {
                          ID = p.ID,
                          Treatment = trl.Name,
                          Trial = t.Name


                      }).ToList();

            }
            return pt;
        }
        #endregion

        #region check exist
        public static bool PatientTrailsandPrimaryTreatment_CheckExists(int tid, int treatmentID, int uid)
        {

            List<PatientTrialsAndTreatment> pt = new List<PatientTrialsAndTreatment>();
            using (TherapyDataContext dd = new TherapyDataContext())
            {
                pt = dd.PatientTrialsAndTreatments.Where(r => r.TrialID == tid && r.TreatmentID == treatmentID && r.UserID == uid && r.TypeofTreatment == "PrimaryTreatment").Select(r => r).ToList();
            }
            if (pt.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region check exist
        public static bool PatientTrailsandSecondaryTreatment_CheckExists(int tid, int treatmentID, int uid)
        {

            List<PatientTrialsAndTreatment> pt = new List<PatientTrialsAndTreatment>();
            using (TherapyDataContext dd = new TherapyDataContext())
            {
                pt = dd.PatientTrialsAndTreatments.Where(r => r.TrialID == tid && r.TreatmentID == treatmentID && r.UserID == uid && r.TypeofTreatment == "SecondaryTreatment").Select(r => r).ToList();
            }
            if (pt.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region Select all secondary treatments
        public static List<PatientTrailsandTreatmentList> PatientTrailsandSecondaryTreatmentList_selectAll(int uid)
        {

            List<PatientTrailsandTreatmentList> pt = new List<PatientTrailsandTreatmentList>();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                var trial = td.TrialConfigurations.Select(p => p).ToList();
                var patientTherapy = td.PatientTrialsAndTreatments.Select(d => d).ToList();
                var secondarytreatment = td.SecondaryTreatments.Select(d => d).ToList();

                pt = (from trl in secondarytreatment
                      join p in patientTherapy on trl.ID equals p.TreatmentID
                      //join t in patientTherapy on trl.ID equals t.TrialID
                      join t in trial on p.TrialID equals t.ID

                      where p.UserID == uid && p.TypeofTreatment == "SecondaryTreatment"
                      select new PatientTrailsandTreatmentList
                      {
                          ID = p.ID,
                          Treatment = trl.Name,
                          Trial = t.Name


                      }).ToList();

            }
            return pt;
        }
        #endregion
        #region Delete
        public static void TrialandTreatment_Delete(int id)
        {
            using (TherapyDataContext td = new TherapyDataContext())
            {

                var tt = (from pt in td.PatientTrialsAndTreatments
                          where pt.ID == id
                          select pt).FirstOrDefault();
                td.PatientTrialsAndTreatments.DeleteOnSubmit(tt);
                td.SubmitChanges();

            }
        }
        #endregion

        #region Get Primary Treatment ID by UserID and TrailID
        public static int GetPrimaryTreatmentID(int tid, int uid)
        {
            int PTID;
            using (TherapyDataContext dd = new TherapyDataContext())
            {
                PTID = dd.PatientTrialsAndTreatments.Where(r => r.TrialID == tid && r.UserID == uid && r.TypeofTreatment == "PrimaryTreatment").Select(r => r.TreatmentID.Value).FirstOrDefault();
            }
            return PTID;
        }
        #endregion

        #region Get Secondary Treatment ID by UserID and TrailID
        public static int GetSecondaryTreatmentID(int tid, int uid)
        {
            int STID;
            using (TherapyDataContext dd = new TherapyDataContext())
            {
                STID = dd.PatientTrialsAndTreatments.Where(r => r.TrialID == tid && r.UserID == uid && r.TypeofTreatment == "SecondaryTreatment").Select(r => r.TreatmentID.Value).FirstOrDefault();
            }
            return STID;
        }
        #endregion
        #region Get Primary Details
        public static PatientTrialsAndTreatment GetPrimaryTreatmentDetails(int tid, int uid)
        {

            PatientTrialsAndTreatment pt = new PatientTrialsAndTreatment();
            using (TherapyDataContext dd = new TherapyDataContext())
            {
                pt = dd.PatientTrialsAndTreatments.Where(r => r.TrialID == tid && r.UserID == uid && r.TypeofTreatment == "PrimaryTreatment").Select(r => r).FirstOrDefault();
            }
            return pt;
        }
        #endregion
        #region Get Secondary Details
        public static PatientTrialsAndTreatment GetSecondaryTreatmentDetails(int tid, int uid)
        {
            PatientTrialsAndTreatment pt = new PatientTrialsAndTreatment();
            using (TherapyDataContext dd = new TherapyDataContext())
            {
                pt = dd.PatientTrialsAndTreatments.Where(r => r.TrialID == tid && r.UserID == uid && r.TypeofTreatment == "SecondaryTreatment").Select(r => r).FirstOrDefault();
            }
            return pt;
        }
        #endregion
        #region Update
        public static void PatientTrialsAndTreatment_Update(PatientTrialsAndTreatment t)
        {
            using (TherapyDataContext td = new TherapyDataContext())
            {
                PatientTrialsAndTreatment pt = (from tr in td.PatientTrialsAndTreatments
                                         where tr.ID == t.ID
                                         select tr).FirstOrDefault();
                pt.TreatmentID = t.TreatmentID;
                pt.ModifiedDate = DateTime.Now;
                td.SubmitChanges();
            }

        }
        #endregion
    }
}