using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetMgt.Entity;

namespace TimesheetMgt.BAL
{
    public class TimeExpensesBAL
    {

        public static TimeExpense TimeExpensesBAL_Add(TimeExpense t)
        {
            ITimesheetRepository<TimeExpense> pRep = new TimesheetRepository<TimeExpense>();
            pRep.Add(t);

            return t;
        }
        public static TimeExpense TimeExpensesBAL_update(TimeExpense t)
        {
            ITimesheetRepository<TimeExpense> pRep = new TimesheetRepository<TimeExpense>();
            var u = pRep.GetAll().Where(o => o.ID == t.ID).FirstOrDefault();
            if(u != null)
            {
                u.AccountingCodesID = t.AccountingCodesID;
                u.amount = t.amount;
                u.BuyingPrice = t.BuyingPrice;
                u.ContractorID = t.ContractorID;
                u.Details = t.Details;
                u.EntryType = t.EntryType;
                u.Item = t.Item;
                u.Notes = t.Notes;
                u.ProjectReference = t.ProjectReference;
                u.Qty = t.Qty;
                u.ReimburseToID = t.ReimburseToID;
                u.SellingPrice = t.SellingPrice;
                u.SellingTotal = t.SellingTotal;
                u.Status = t.Status;
                u.TimeExpensesDate = t.TimeExpensesDate;
                u.WCDateID = t.WCDateID;
                u.Image = t.Image;
                pRep.Edit(u);
            }
            

            return u;
        }
        public static List<v_TimeExpense> TimeExpensesBAL_Select(int contractorID)
        {
            ITimesheetRepository<v_TimeExpense> pRep = new TimesheetRepository<v_TimeExpense>();
            return pRep.GetAll().Where(o => o.ContractorID == contractorID ).ToList();

        }

      

        public static IQueryable<v_TimeExpense> TimeExpensesBAL_SelectAll()
        {
            ITimesheetRepository<v_TimeExpense> pRep = new TimesheetRepository<v_TimeExpense>();
            return pRep.GetAll();

        }

        public static bool TimeExpensesBAL_Delete(int id)
        {
            ITimesheetRepository<TimeExpense> pRep = new TimesheetRepository<TimeExpense>();
            var t = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (t != null)
            {
                pRep.Delete(t);
                return true;
            }
            else
                return false;

        }

        public static TimeExpense TimeExpensesBAL_SelectByID(int id)
        {
            ITimesheetRepository<TimeExpense> pRep = new TimesheetRepository<TimeExpense>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }

        public static bool TimeExpense_Delete(int id)
        {
            ITimesheetRepository<TimeExpense> pRep = new TimesheetRepository<TimeExpense>();
            var e = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (e != null)
            {
                pRep.Delete(e);
                return true;
            }
            else return false;
        }
    }
}
