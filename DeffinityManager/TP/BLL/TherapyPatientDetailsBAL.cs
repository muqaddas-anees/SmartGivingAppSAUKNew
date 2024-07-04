using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Therapy.DAL;
using Therapy.Entity;
namespace TP.BLL
{
    /// <summary>
    /// Summary description for TherapyPatientDetailsBAL
    /// </summary>
    public class TherapyPatientDetailsBAL
    {
        public TherapyPatientDetailsBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region GetQuestions
        public static IEnumerable<CommonQuestion> GetCommonQuestion()
        {
            List<CommonQuestion> commonQuestion = new List<CommonQuestion>();
            using (TherapyDataContext db = new TherapyDataContext())
            {
                commonQuestion = db.CommonQuestions.Select(c => c).ToList();
            }
            return commonQuestion;
        }

        public static IEnumerable<KeyMarker> GetKeyMarkersByTrialId(int trialId)
        {
            List<KeyMarker> keyMarker = new List<KeyMarker>();
            using (TherapyDataContext db = new TherapyDataContext())
            {
                keyMarker = db.KeyMarkers.Where(k => k.TrialID == trialId).ToList();
            }
            return keyMarker;
        }

        public static IEnumerable<SubjectiveQuestion> GetSubjectiveQuetionByTrialId(int trialId)
        {
            List<SubjectiveQuestion> subjectiveQuestion = new List<SubjectiveQuestion>();
            using (TherapyDataContext db = new TherapyDataContext())
            {
                subjectiveQuestion = db.SubjectiveQuestions.Where(s => s.TrialID == trialId).ToList();
            }
            return subjectiveQuestion;
        }
        #endregion

        #region Therapy Patient Details Insertion
        public static void InsertTherapyPatientDetails(TherapyPatientDetail therapyPatientDetail)
        {
            using (TherapyDataContext db = new TherapyDataContext())
            {
                db.TherapyPatientDetails.InsertOnSubmit(therapyPatientDetail);
                db.SubmitChanges();
            }
        }

        public static void InsertAssociatedTherapyPatientDetails(AssociatedTherapyDetail associatedTherapyDetail)
        {
            using (TherapyDataContext db = new TherapyDataContext())
            {
                db.AssociatedTherapyDetails.InsertOnSubmit(associatedTherapyDetail);
                db.SubmitChanges();
            }
        }
        #endregion

        #region Get Therapy Patient Details by ID
        public static TherapyPatientDetail GetTherapyDetailsByID(int id)
        {
            TherapyPatientDetail therapyPatientDetail = new TherapyPatientDetail();
            using (TherapyDataContext db = new TherapyDataContext())
            {
                therapyPatientDetail = db.TherapyPatientDetails.Where(t => t.ID == id).FirstOrDefault();
            }
            return therapyPatientDetail;
        }
        #endregion
        #region Therapy Patient Details Update

        public static void UpdateTherapyDetails(TherapyPatientDetail therapyPatientDetail)
        {
            using (TherapyDataContext db = new TherapyDataContext())
            {
                TherapyPatientDetail currentTherapyDetail = db.TherapyPatientDetails.Where(t => t.ID == therapyPatientDetail.ID).FirstOrDefault();
                if (currentTherapyDetail != null)
                {
                    currentTherapyDetail.DateLogged = therapyPatientDetail.DateLogged;
                    currentTherapyDetail.WeekCommencingDate = therapyPatientDetail.WeekCommencingDate;
                    currentTherapyDetail.TreatmentID = therapyPatientDetail.TreatmentID;
                    currentTherapyDetail.FlagStartedFromBeginning = therapyPatientDetail.FlagStartedFromBeginning;
                    db.SubmitChanges();
                }
            }
        }
        public static void UpdateAssociatedTherapyPatientDetails(AssociatedTherapyDetail associatedTherapyDetail)
        {
            using (TherapyDataContext db = new TherapyDataContext())
            {
                AssociatedTherapyDetail therapyDetail = db.AssociatedTherapyDetails.Where(t => t.TherapyID == associatedTherapyDetail.TherapyID && t.QuestionID == associatedTherapyDetail.QuestionID && t.SectionType == associatedTherapyDetail.SectionType).Select(t => t).FirstOrDefault();
                if (therapyDetail != null)
                {
                    therapyDetail.Answer = associatedTherapyDetail.Answer;
                    db.SubmitChanges();
                }
                else
                {
                    db.AssociatedTherapyDetails.InsertOnSubmit(associatedTherapyDetail);
                    db.SubmitChanges();
                }
            }
        }
        #endregion
        #region Get All Section Answers  By TherapyId
        public static IEnumerable<AssociatedTherapyDetail> GetAnswerByTherapyId(int therapyId)
        {
            List<AssociatedTherapyDetail> associatedTherapyDetail = new List<AssociatedTherapyDetail>();
            using (TherapyDataContext db = new TherapyDataContext())
            {
                associatedTherapyDetail = db.AssociatedTherapyDetails.Where(a => a.TherapyID == therapyId).Select(a=>a).ToList();
            }
            return associatedTherapyDetail;
            
        }
        #endregion

        #region Update Details By UserID and TrialID
        public static void UpdateDetails(int UserID, int TrialID, string name)
        {
            List<TherapyPatientDetail> List = new List<TherapyPatientDetail>();
            using (TherapyDataContext db = new TherapyDataContext())
            {
                List = db.TherapyPatientDetails.Where(t => t.TrialID == TrialID && t.UserID == UserID).ToList();
                if (name == "Delete")
                {
                    List.ForEach(a => a.FlagStartedFromBeginning = false);
                }
                else if(name == "Add")
                {
                    List.ForEach(a => a.FlagStartedFromBeginning = true);
                }
                db.SubmitChanges();
            }        
        }
        #endregion
    }
}