using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetMgt.Entity;

namespace TimesheetMgt.BAL
{
    public class ExpensesAccountingCodesBAL
    {
        public static ExpensesAccountingCode ExpensesAccountingCodesBAL_Update(int portfolioID,string accountingCode,string description, int id = 0)
        {
            ITimesheetRepository<ExpensesAccountingCode> pRep = new TimesheetRepository<ExpensesAccountingCode>();
            var s = new ExpensesAccountingCode();
            if (id == 0)
            {
                s = pRep.GetAll().Where(o => o.PortfolioID == portfolioID && o.AccountingCode.ToLower() == accountingCode.ToLower()).FirstOrDefault();
                if (s == null)
                {
                    s = new ExpensesAccountingCode();

                    s.PortfolioID = portfolioID;
                    s.AccountingCode = accountingCode;
                    s.Description = description;

                    pRep.Add(s);
                }
                else
                {
                    s.AccountingCode = accountingCode;
                    s.Description = description;
                    pRep.Edit(s);
                }
            }
            else
            {
                s = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
                if (s != null)
                {
                    s.AccountingCode = accountingCode;
                    s.Description = description;
                    pRep.Edit(s);
                }
            }
            return s;
        }

        public static List<ExpensesAccountingCode> ExpensesAccountingCodeBAL_Select(int portfolioid)
        {
            ITimesheetRepository<ExpensesAccountingCode> pRep = new TimesheetRepository<ExpensesAccountingCode>();
            return pRep.GetAll().Where(o => o.PortfolioID == portfolioid).ToList();

        }

        public static ExpensesAccountingCode ExpensesAccountingCodeBAL_SelectByID(int id)
        {
            ITimesheetRepository<ExpensesAccountingCode> pRep = new TimesheetRepository<ExpensesAccountingCode>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }

        public static bool ExpensesAccountingCodeBAL_Delete(int id)
        {
            ITimesheetRepository<ExpensesAccountingCode> pRep = new TimesheetRepository<ExpensesAccountingCode>();
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
