using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Therapy.DAL;
using Therapy.Entity;

/// <summary>
/// Summary description for SecondaryTreatmentBAL
/// </summary>
/// 
namespace TP.BLL
{
    public class SecondaryTreatmentBAL
    {
        #region Insert
        public static void SecondaryTreatment_Insert(SecondaryTreatment t)
        {
            using (TherapyDataContext db = new TherapyDataContext())
            {
                db.SecondaryTreatments.InsertOnSubmit(t);
                db.SubmitChanges();
            }
        }
        #endregion

        #region Delete
        public static void SecondaryTreatment_Delete(int id)
        {
            using (TherapyDataContext td = new TherapyDataContext())
            {

                var Streatment = (from t in td.SecondaryTreatments
                                 where t.ID == id
                                 select t).FirstOrDefault();
                td.SecondaryTreatments.DeleteOnSubmit(Streatment);
                td.SubmitChanges();

            }
        }
        #endregion

        #region Select by Trialid
        public static List<SecondaryTreatment> SecondaryTreatment_selectByTrialID(int tid)
        {
            List<SecondaryTreatment> tm = new List<SecondaryTreatment>();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                tm = (from t in td.SecondaryTreatments
                      where t.TrialID == tid 
                      select t).ToList();
            }

            return tm;
        }
        #endregion

        #region Select all
        public static List<PatientTrailsandTreatmentList> PatientTrailsandPrimaryTreatmentList_selectAll(int uid)
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
                      
                      where p.UserID == uid
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

        #region Select by id
        public static SecondaryTreatment SecondaryTreatment_selectByID(int id)
        {
            SecondaryTreatment tm = new SecondaryTreatment();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                tm = (from t in td.SecondaryTreatments
                      where t.ID == id
                      select t).FirstOrDefault();
            }

            return tm;
        }
        #endregion
        #region select all
        public static List<SecondaryTreatmentList> SecondaryTreatment_selectAll()
        {
            List<SecondaryTreatmentList> Streatment = new List<SecondaryTreatmentList>();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                var trial = td.TrialConfigurations.Select(p => p).ToList();
                var tr = td.SecondaryTreatments.Select(d => d).ToList();

                Streatment = (from t in trial
                             join tm in tr on t.ID equals tm.TrialID

                             select new SecondaryTreatmentList
                             {
                                 ID = tm.ID,
                                 Name = tm.Name,
                                 Trial = t.Name,
                                 Status = t.Status
                             }).ToList();

            }
            return Streatment;

        }
        #endregion
    }
}