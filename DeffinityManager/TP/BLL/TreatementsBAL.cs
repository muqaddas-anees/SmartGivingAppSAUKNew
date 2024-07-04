using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Therapy.DAL;
using Therapy.Entity;

/// <summary>
/// Summary description for TreatementsBAL
/// </summary>
/// 
namespace TP.BLL
{
    public class TreatementsBAL
    {
        #region Insert
        public static void Treatment_Insert(Treatment t)
        {
            using (TherapyDataContext db = new TherapyDataContext())
            {
                db.Treatments.InsertOnSubmit(t);
                db.SubmitChanges();
            }
        }
        #endregion

        #region Check exist
        public static bool Treatement_Exist(string strname, int tid)
        {
            Treatment tm = new Treatment();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                tm = (from t in td.Treatments
                      where t.Name.ToLower() == strname.ToLower() && t.TrialID == tid
                      select t).FirstOrDefault();
            }
            if (tm == null)
                return false;
            else
                return true;

        }
        #endregion

        #region Delete
        public static void Treatment_Delete(int id)
        {
            using (TherapyDataContext td = new TherapyDataContext())
            {

                var treatment = (from t in td.Treatments
                                 where t.ID == id
                                 select t).FirstOrDefault();
                td.Treatments.DeleteOnSubmit(treatment);
                td.SubmitChanges();

            }
        }
        #endregion

        #region Select by id
        public static Treatment Treatment_selectByID(int id)
        {
            Treatment tm = new Treatment();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                tm = (from t in td.Treatments
                      where t.ID == id
                      select t).FirstOrDefault();
            }

            return tm;
        }
        #endregion

        #region Select by Treatment
        public static Treatment Treatment_selectByName(string name)
        {
            Treatment tm = new Treatment();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                tm = (from t in td.Treatments
                      where t.Name == name
                      select t).FirstOrDefault();
            }

            return tm;
        }
        #endregion

        #region Select by trail id
        public static List<Treatment> Treatment_selectByTrailID(int tid)
        {
            List<Treatment> tm = new List<Treatment>();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                tm = (from t in td.Treatments
                      where t.TrialID == tid 
                      select t).ToList();
            }

            return tm;
        }
        #endregion

        #region Select all Treatments
        public static List<Treatment> Treatment_select()
        {
            List<Treatment> tm = new List<Treatment>();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                tm = (from t in td.Treatments
                      select t).ToList();
            }

            return tm;
        }
        #endregion

        #region select all
        public static List<TreatmentList> Treatment_selectAll()
        {
            List<TreatmentList> treatment = new List<TreatmentList>();
            using (TherapyDataContext td = new TherapyDataContext())
            {
                
                    var trial = td.TrialConfigurations.Select(p => p).ToList();
                    var tr = td.Treatments.Select(d => d).ToList();

                    treatment = (from t in trial
                                 join tm in tr on t.ID equals tm.TrialID

                                 select new TreatmentList
                             {
                                ID = tm.ID,
                                Name = tm.Name,
                                Trial = t.Name,
                                Status = t.Status
                             }).ToList();
                
            }
            return treatment;

        }
        #endregion

    
    }
}