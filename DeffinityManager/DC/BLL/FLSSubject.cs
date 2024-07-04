using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
/// <summary>
/// Summary description for FLSSubject
/// </summary>
    public class FLSSubject
    {
        public FLSSubject()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region BindSubject
        public static List<Subject> Bind()
        {
            List<Subject> subjectList = new List<Subject>();
            using (DCDataContext dd = new DCDataContext())
            {
                subjectList = dd.Subjects.OrderBy(r => r.SubjectName).Select(r=>r).ToList();
            }
            return subjectList;
        }
        #endregion
        #region Check Exists when Inserting
        public static bool CheckExists(string name, int customerId)
        {

            Subject subject = new Subject();
            using (DCDataContext dd = new DCDataContext())
            {
                subject = dd.Subjects.Where(r => r.SubjectName.ToLower() == name.ToLower() && r.CustomerID==customerId).Select(r => r).FirstOrDefault();
            }
            if (subject != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Add Subject
        public static void Add(Subject sub)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.Subjects.InsertOnSubmit(sub);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select Subject by ID
        public static Subject SelectById(int id)
        {

            Subject subject = new Subject();
            using (DCDataContext dd = new DCDataContext())
            {
                subject = dd.Subjects.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return subject;
        }
        #endregion
        #region Check Exists when Updating
        public static bool CheckByIdUpdate(int id, string name, int customerId)
        {

            Subject subject = new Subject();
            using (DCDataContext dd = new DCDataContext())
            {
                subject = dd.Subjects.Where(r => r.ID != id && r.SubjectName.ToLower() == name.ToLower()&& r.CustomerID == customerId).Select(r => r).FirstOrDefault();
            }
            if (subject != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        # region Update Subject
        public static void Update(Subject subject)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                Subject subCurrent = dd.Subjects.Where(r => r.ID == subject.ID).Select(r => r).FirstOrDefault();
                subCurrent.SubjectName = subject.SubjectName;
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Delete Subject By ID
        public static void DeleteById(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                Subject sub = dd.Subjects.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (sub != null)
                {
                    dd.Subjects.DeleteOnSubmit(sub);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
    }
}