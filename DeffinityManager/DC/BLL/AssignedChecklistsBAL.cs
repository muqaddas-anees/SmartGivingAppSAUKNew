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
    /// Summary description for AssignedChecklistsBAL
    /// </summary>
    public class AssignedChecklistsBAL
    {
        #region Add Assigned Checklists
        public static void AddAssignedChecklists(AssignedChecklist al)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.AssignedChecklists.InsertOnSubmit(al);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Bind Assigned Checklists
        public static AssignedChecklist BindAssignedChecklist()
        {
            AssignedChecklist al = new AssignedChecklist();
            using (DCDataContext dd = new DCDataContext())
            {
                al = dd.AssignedChecklists.Select(p => p).FirstOrDefault();
            }
            return al;
        }
        #endregion
        # region Update Assigned Checklists
        public static void AssignedChecklistsUpdate(AssignedChecklist al)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                AssignedChecklist alcurrent = dd.AssignedChecklists.Where(r => r.ID == al.ID).Select(r => r).FirstOrDefault();
                alcurrent.TemplateID = al.TemplateID;
                dd.SubmitChanges();
            }
        }
        #endregion
    }
}