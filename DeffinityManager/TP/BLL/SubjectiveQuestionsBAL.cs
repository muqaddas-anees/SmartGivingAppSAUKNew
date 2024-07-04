using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Therapy.DAL;
using Therapy.Entity;


/// <summary>
/// Summary description for SubjectiveQuestionsBAL
/// </summary>
/// 
namespace TP.BLL
{
    public class SubjectiveQuestionsBAL
    {
        #region Insert
        public static void SubjectiveQuestion_Insert(SubjectiveQuestion sq)
        {
            using (TherapyDataContext db = new TherapyDataContext())
            {
                db.SubjectiveQuestions.InsertOnSubmit(sq);
                db.SubmitChanges();
            }
        }
        #endregion

        #region Check exist
        public static bool SubjectiveQuestion_Exist(string strname, int tid)
        {
            SubjectiveQuestion km = new SubjectiveQuestion();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                km = (from k in td.SubjectiveQuestions
                      where k.Name.ToLower() == strname.ToLower() && k.TrialID == tid
                      select k).FirstOrDefault();
            }
            if (km == null)
                return false;
            else
                return true;

        }
        #endregion
        
        #region Delete
        public static void SubjectiveQuestion_Delete(int id)
        {
            using (TherapyDataContext td = new TherapyDataContext())
            {

                var subQues = (from sq in td.SubjectiveQuestions
                                 where sq.ID == id
                                 select sq).FirstOrDefault();
                td.SubjectiveQuestions.DeleteOnSubmit(subQues);
                td.SubmitChanges();

            }

        }
        #endregion

        #region Select by id
        public static SubjectiveQuestion SubjectiveQuestion_selectByID(int id)
        {
            SubjectiveQuestion sq = new SubjectiveQuestion();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                sq = (from s in td.SubjectiveQuestions
                      where s.ID == id
                      select s).FirstOrDefault();
            }

            return sq;
        }
        #endregion

        #region Select all
        public static List<SubjectiveQuestionList> SubjectiveQuestion_selectAll()
        {
           
            List<SubjectiveQuestionList> subQues = new List<SubjectiveQuestionList>();
            using (TherapyDataContext td = new TherapyDataContext())
            {
                
                    var trial = td.TrialConfigurations.Select(p => p).ToList();
                    var sq = td.SubjectiveQuestions.Select(d => d).ToList();

                    subQues = (from t in trial
                                 join s in sq on t.ID equals s.TrialID

                                 select new SubjectiveQuestionList
                             {
                                ID =s.ID,
                                Name = s.Name,
                                Trial = t.Name,
                                Status = t.Status
                             }).ToList();
                
            }
            return subQues;

        }
        #endregion

    }
}