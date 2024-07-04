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
    /// Summary description for ChecklistitemsBAL
    /// </summary>
    public class ChecklistitemsBAL
    {
        #region Add ChecklistItems
        public static void AddChecklistItems(ChecklistItem cl)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                dd.ChecklistItems.InsertOnSubmit(cl);
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Bind ChecklistItems by callid
        public static List<ChecklistItem> BindChecklistItemsbyCallID(int cid)
        {
            List<ChecklistItem> clList = new List<ChecklistItem>();
            using (DCDataContext dd = new DCDataContext())
            {
                clList = dd.ChecklistItems.Where(c => c.CallID == cid).Select(p => p).ToList();
            }
            return clList;
        }
        #endregion
        # region Update ChecklistItems
        public static void ChecklistItemsUpdate(ChecklistItem cl)
        {
            using (DCDataContext dd = new DCDataContext())
            {
                ChecklistItem clcurrent = dd.ChecklistItems.Where(r => r.ID == cl.ID).Select(r => r).FirstOrDefault();
                clcurrent.ItemDescription = cl.ItemDescription;
                clcurrent.Status = cl.Status;
                clcurrent.ClosedDate = cl.ClosedDate;
                dd.SubmitChanges();
            }
        }
        #endregion
        #region Get ChecklistItem by id
        public static ChecklistItem GetChecklistItembyID(int id)
        {
            ChecklistItem cl = new ChecklistItem();
            using (DCDataContext dd = new DCDataContext())
            {
                cl = dd.ChecklistItems.Where(c => c.ID == id).Select(p => p).FirstOrDefault();
            }
            return cl;
        }
        #endregion
    }
}