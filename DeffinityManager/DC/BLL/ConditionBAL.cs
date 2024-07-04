using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for ConditionBAL
    /// </summary>
    public class ConditionBAL
    {
        #region Bind Condition
        public static List<Condition> BindConditions()
        {
            List<Condition> conditionList = new List<Condition>();
            using (DCDataContext dd = new DCDataContext())
            {
                conditionList = dd.Conditions.Select(r => r).OrderBy(r=>r.Name).ToList();
            }
            return conditionList;
        }
        #endregion
        #region Check Condition Exists when Inserting
        public static bool CheckExists(string name)
        {

            Condition c = new Condition();
            using (DCDataContext dd = new DCDataContext())
            {
                c = dd.Conditions.Where(r => r.Name.ToLower() == name.ToLower()).Select(r => r).FirstOrDefault();
            }
            if (c != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Add Condition
        public static void AddConditions(Condition c)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.Conditions.InsertOnSubmit(c);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Select Condition by ID
        public static Condition SelectbyId(int id)
        {

            Condition c = new Condition();
            using (DCDataContext dd = new DCDataContext())
            {
                c = dd.Conditions.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
            }
            return c;
        }
        #endregion
        #region Check Condition Exists when Updating
        public static bool CheckbyIdUpdate(int id, string name)
        {

            Condition c = new Condition();
            using (DCDataContext dd = new DCDataContext())
            {
                c = dd.Conditions.Where(r => r.ID != id && r.Name.ToLower() == name.ToLower()).Select(r => r).FirstOrDefault();
            }
            if (c != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        # region Update Condition
        public static void ConditionUpdate(Condition c)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                Condition tcurrent = dd.Conditions.Where(r => r.ID == c.ID).Select(r => r).FirstOrDefault();
                tcurrent.Name = c.Name;
                dd.SubmitChanges();
            }
        }
        #endregion
        # region Delete Condition By ID
        public static void ConditionDelete(int id)
        {
            using (DCDataContext dd = new DCDataContext())
            {

                Condition c = dd.Conditions.Where(r => r.ID == id).Select(r => r).FirstOrDefault();
                if (c != null)
                {
                    dd.Conditions.DeleteOnSubmit(c);
                    dd.SubmitChanges();
                }
            }
        }
        #endregion
    }
}