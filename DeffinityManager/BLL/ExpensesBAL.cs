using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetMgt.Entity;
using TimesheetMgt.DAL;
namespace TimesheetMgt.Entity
{
    public class ExpensesApproverDetails
    {
        public int UserID { set; get; }
        public int ApproverID { set; get; }
        public string ApproverName { set; get; }
        public string UserName { set; get; }
    }

}
namespace TimesheetMgt.BAL
{
    
    public class ExpensesBAL
    {

        #region TimeExpense
        public static bool TimeExpense_Update(TimeExpense et)
        {
            bool retval = false;
            var eInputRepo = new TimesheetRepository<TimeExpense>();

            var cE = eInputRepo.GetAll().Where(o => o.ID == et.ID).FirstOrDefault();
            if (cE == null)
            {
                //eInputRepo.Add(et);
                //if (et.ID > 0)
                //    retval = true;
            }
            else
            {
                cE.amount = et.amount;
                cE.ApprovedOrRejectedDateTime = et.ApprovedOrRejectedDateTime;
                cE.ApproverOrRejecterID = et.ApproverOrRejecterID;
                cE.ApproverOrRejecterComments = et.ApproverOrRejecterComments;
                cE.BuyingPrice = et.BuyingPrice;
                cE.ContractorID = et.ContractorID;
                cE.EntryType = et.EntryType;
                cE.Notes = et.Notes;
                cE.ProjectReference = et.ProjectReference;
                cE.Qty = et.Qty ;
                cE.SellingPrice = et.SellingPrice;
                cE.SellingTotal = et.SellingTotal;
                cE.StatusID = et.StatusID;
                cE.SubCategoryID = et.SubCategoryID;
                cE.SubmittedDateTime = et.SubmittedDateTime;
                cE.TimeExpensesDate = et.TimeExpensesDate;
                cE.TotalIncludeVAT = et.TotalIncludeVAT;
                cE.WCDateID = et.WCDateID;
                
                eInputRepo.Edit(cE);
                retval = true;
            }

            return retval;
        }
        public static TimeExpense TimeExpense_SelectByID(int ExpensesID)
        {
            var etypeRepo = new TimesheetRepository<TimeExpense>();
            return etypeRepo.GetAll().Where(o => o.ID == ExpensesID).FirstOrDefault();
        }

        public static List<TimeExpense> TimeExpense_SelectByWCDate(int WCDateID, int ContractorID)
        {
            var etypeRepo = new TimesheetRepository<TimeExpense>();
            return etypeRepo.GetAll().Where(o => o.WCDateID == WCDateID && o.ContractorID == ContractorID).ToList();
        }
        public static List<TimeExpense> TimeExpense_SelectByWCDate(DateTime WCDate, int ContractorID)
        {
            List<TimeExpense> eList = new List<TimeExpense>();
            var WCRepo = new TimesheetRepository<TimesheetWCDate>();
            var wcEntity = WCRepo.GetAll().Where(o => o.WCDate == WCDate && o.ContractorID == ContractorID).FirstOrDefault();
            if (wcEntity != null)
            {
                var etypeRepo = new TimesheetRepository<TimeExpense>();
                eList = etypeRepo.GetAll().Where(o => o.WCDateID == wcEntity.WCDateID && o.ContractorID == ContractorID).ToList();
            }
            return eList;
        }
        public static List<V_TimeExpense> V_TimeExpense_SelectByWCDate(DateTime WCDate, int ContractorID)
        {
            List<V_TimeExpense> eList = new List<V_TimeExpense>();
            var WCRepo = new TimesheetRepository<TimesheetWCDate>();
            var wcEntity = WCRepo.GetAll().Where(o => o.WCDate == WCDate && o.ContractorID == ContractorID).FirstOrDefault();
            if (wcEntity != null)
            {
                var etypeRepo = new TimesheetRepository<V_TimeExpense>();
                eList = etypeRepo.GetAll().Where(o => o.WCDateID == wcEntity.WCDateID && o.ContractorID == ContractorID).ToList();
            }
            return eList;
        }
        public static List<V_TimeExpense> V_TimeExpense_SelectByIDs(List<int> ExpensesIDs)
        {
            var etypeRepo = new TimesheetRepository<V_TimeExpense>();
            return etypeRepo.GetAll().Where(o => ExpensesIDs.Contains(o.ID)).ToList();
        }

        public static V_TimeExpense V_TimeExpense_SelectByID(int ExpensesID)
        {
            var etypeRepo = new TimesheetRepository<V_TimeExpense>();
            return etypeRepo.GetAll().Where(o => o.ID == ExpensesID).FirstOrDefault();
        }

        public static List<V_TimeExpense> V_TimeExpense_SelectByWCDate(int WCDateID, int ContractorID)
        {
            var etypeRepo = new TimesheetRepository<V_TimeExpense>();
            return etypeRepo.GetAll().Where(o => o.WCDateID == WCDateID && o.ContractorID == ContractorID).ToList();
        }
        public static List<V_TimeExpense> V_TimeExpense_Select(int ApproverID)
        {
            var etypeRepo = new TimesheetRepository<V_TimeExpense>();
            return etypeRepo.GetAll().Where(o => o.CurrentApproverID == ApproverID).ToList();
        }
        #endregion


        #region ExpensesApprover
        public static bool ExpensesApprover_AddUpdateAll(int ApproverID)
        {
            bool retval = false;
            var cRepo = new UserRepository<UserMgt.Entity.Contractor>();
            int[] sids = {7,8};
            var ulist = cRepo.GetAll().Where(o => !sids.Contains(o.SID.Value)).ToList();
            foreach(var v in ulist)
            {
                ExpensesApprover_AddUpdate(new ExpensesApprover() { UserID = v.ID, ApproverID = ApproverID });
                retval = true;
            }
            return retval;
        }
        public static bool ExpensesApprover_AddUpdate(ExpensesApprover et)
        {
            bool retval = false;
            var etypeRepo = new TimesheetRepository<ExpensesApprover>();

            var cE = etypeRepo.GetAll().Where(o => o.UserID == et.UserID && o.ApproverID == et.ApproverID).FirstOrDefault();
            if (cE == null)
            {
                etypeRepo.Add(et);
                if (et.ID > 0)
                    retval = true;
            }
            else
            {
                cE.UserID = et.UserID;
                cE.ApproverID = et.ApproverID;
                etypeRepo.Edit(cE);
                retval = true;
            }

            return retval;
        }
        public static List<ExpensesApproverDetails> ExpensesApprover_Select()
        {
            List<ExpensesApproverDetails> eList = new List<ExpensesApproverDetails>();
            var etypeRepo = new TimesheetRepository<ExpensesApprover>();
            var cRepo = new UserRepository<UserMgt.Entity.Contractor>();
            var ulist = (from c in cRepo.GetAll()
                         select new { c.ID, c.ContractorName }).ToList();
            eList = (from p in etypeRepo.GetAll().ToList()
                     join u in ulist on p.UserID equals u.ID
                     join c in ulist on p.ApproverID equals c.ID
                     select new ExpensesApproverDetails { ApproverID = p.ApproverID, ApproverName = c.ContractorName,UserName = u.ContractorName,UserID =p.UserID  }).ToList();
            return eList;
        }
       
        public static bool ExpensesApprover_Delete(int UserID)
        {
            bool retval = false;
            var etypeRepo = new TimesheetRepository<ExpensesApprover>();
            var cE = etypeRepo.GetAll().Where(o => o.UserID == UserID).FirstOrDefault();
            if (cE != null)
            {
                etypeRepo.Delete(cE);
                retval = true;
            }
            return retval;
        }
       
        #endregion

        #region Expensesentrytype

        public static bool Expensesentrytype_AddUpdate(Expensesentrytype et)
        {
            bool retval = false;
            var etypeRepo = new TimesheetRepository<Expensesentrytype>();

            var cE = etypeRepo.GetAll().Where(o => o.ID == et.ID).FirstOrDefault();
            if(cE == null)
            {
                etypeRepo.Add(et);
                if (et.ID > 0)
                    retval = true;
            }
            else
            {
                cE.IsDeleted = et.IsDeleted;
                cE.MasterID = et.MasterID;
                cE.sellingPrice = et.sellingPrice;
                cE.BuyingPrice = et.BuyingPrice;
                cE.ExpensesentryType1 = et.ExpensesentryType1;
                cE.ExpensesVATIncluded = et.ExpensesVATIncluded;
                etypeRepo.Edit(cE);
                retval = true;
            }

            return retval;
        }
        public static List<Expensesentrytype> Expensesentrytype_SelectCategory()
        {
            
            var etypeRepo = new TimesheetRepository<Expensesentrytype>();

            return etypeRepo.GetAll().Where(o => o.MasterID == 0 && o.IsDeleted == false).ToList();
        }
        public static List<Expensesentrytype> Expensesentrytype_SelectSubCategory(int CategoryID)
        {

            var etypeRepo = new TimesheetRepository<Expensesentrytype>();

            return etypeRepo.GetAll().Where(o => o.MasterID == CategoryID && o.MasterID >0 && o.IsDeleted == false).ToList();
        }
        public static Expensesentrytype Expensesentrytype_SelectSubCategoryByID(int SubCategoryID)
        {

            var etypeRepo = new TimesheetRepository<Expensesentrytype>();

            return etypeRepo.GetAll().Where(o => o.ID == SubCategoryID).FirstOrDefault();
        }
        public static bool Expensesentrytype_CategoryExists(string categoryName)
        {
            bool retval = false;
            var etypeRepo = new TimesheetRepository<Expensesentrytype>();

            var cE = etypeRepo.GetAll().Where(o => o.ExpensesentryType1.ToLower() == categoryName.ToLower() && o.MasterID == 0 && o.IsDeleted == false).FirstOrDefault();
            if (cE == null)
                retval = false;
            else
                retval = true;

            return retval;
        }
        public static bool Expensesentrytype_SubCategoryExists(string categoryName, int CategoryID)
        {
            bool retval = false;
            var etypeRepo = new TimesheetRepository<Expensesentrytype>();

            var cE = etypeRepo.GetAll().Where(o => o.ExpensesentryType1.ToLower() == categoryName.ToLower() && o.MasterID == CategoryID && o.IsDeleted == false).FirstOrDefault();
            if (cE == null)
                retval = false;
            else
                retval = true;

            return retval;
        }
        public static bool Expensesentrytype_Delete(int ID)
        {
            bool retval = false;
            var etypeRepo = new TimesheetRepository<Expensesentrytype>();
            var cE = etypeRepo.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            if (cE != null)
            {
                cE.IsDeleted = true;
                etypeRepo.Edit(cE);
                retval = true;
            }
            return retval;
        }
        public static Expensesentrytype Expensesentrytype_SelectByID(int ID)
        {
            var etypeRepo = new TimesheetRepository<Expensesentrytype>();
            return etypeRepo.GetAll().Where(o => o.ID == ID).FirstOrDefault();
        }
        #endregion

        #region ExpensesInput

        public static bool ExpensesInput_AddUpdate(ExpensesInput et)
        {
            bool retval = false;
            var eInputRepo = new TimesheetRepository<ExpensesInput>();

            var cE = eInputRepo.GetAll().Where(o => o.ID == et.ID).FirstOrDefault();
            if (cE == null)
            {
                eInputRepo.Add(et);
                if (et.ID > 0)
                    retval = true;
            }
            else
            {
                cE.IsDeleted = et.IsDeleted;
                cE.InputType = et.InputType;
                cE.Name = et.Name;
                cE.CategoryID = et.CategoryID;
                eInputRepo.Edit(cE);
                retval = true;
            }

            return retval;
        }
        public static bool ExpensesInput_Exists(string Name, int CategoryID)
        {
            bool retval = false;
            var etypeRepo = new TimesheetRepository<ExpensesInput>();

            var cE = etypeRepo.GetAll().Where(o => o.Name.ToLower() == Name.ToLower() && o.CategoryID == CategoryID && o.IsDeleted == false ).FirstOrDefault();
            if (cE == null)
                retval = false;
            else
                retval = true;

            return retval;
        }

        public static bool ExpensesInput_Delete(int ID)
        {
            bool retval = false;
            var etypeRepo = new TimesheetRepository<ExpensesInput>();
            var cE = etypeRepo.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            if (cE != null)
            {
                cE.IsDeleted = true;
                etypeRepo.Edit(cE);
                retval = true;
            }
            return retval;
        }
        
        public static List<ExpensesInput> ExpensesInput_Select(int CategoryID)
        {
            var etypeRepo = new TimesheetRepository<ExpensesInput>();
            return etypeRepo.GetAll().Where(o => o.CategoryID == CategoryID && o.IsDeleted == false).ToList();
        }
        public static ExpensesInput ExpensesInput_SelectByID(int ID)
        {
            var etypeRepo = new TimesheetRepository<ExpensesInput>();
            return etypeRepo.GetAll().Where(o => o.ID == ID).FirstOrDefault();
        }
        #endregion

        #region ExpensesInputValue
        public static ExpensesInputValue ExpensesInputValue_SelectByID(int ID)
        {
            var etypeRepo = new TimesheetRepository<ExpensesInputValue>();
            return etypeRepo.GetAll().Where(o => o.ID == ID).FirstOrDefault();
        }
        public static List<ExpensesInputValue> ExpensesInputValue_Select(int ExpensesID)
        {
            var etypeRepo = new TimesheetRepository<ExpensesInputValue>();
            return etypeRepo.GetAll().Where(o => o.ExpensesID == ExpensesID).ToList();
        }
        public static bool ExpensesInputValue_Delete(int ExpensesID, int rowid)
        {
            bool retval = false;
            var etypeRepo = new TimesheetRepository<ExpensesInputValue>();
            var dlist = etypeRepo.GetAll().Where(o => o.ExpensesID == ExpensesID && o.RowID == rowid).ToList();
            if(dlist.Count >0)
            {
                etypeRepo.DeleteAll(dlist);
                retval = true;
            }
            return retval;
        }
        public static bool ExpensesInputValue_Delete(int ExpensesID)
        {
            bool retval = false;
            var etypeRepo = new TimesheetRepository<ExpensesInputValue>();
            var dlist = etypeRepo.GetAll().Where(o => o.ExpensesID == ExpensesID ).ToList();
            if (dlist.Count > 0)
            {
                etypeRepo.DeleteAll(dlist);
                retval = true;
            }
            return retval;
        }
        public static IQueryable<ExpensesInputValue> ExpensesInputValue_Select()
        {
            var etypeRepo = new TimesheetRepository<ExpensesInputValue>();
            return etypeRepo.GetAll();
        }
        public static bool ExpensesInputValue_AddUpdate(ExpensesInputValue et)
        {
            bool retval = false;
            var eInputRepo = new TimesheetRepository<ExpensesInputValue>();

            var cE = eInputRepo.GetAll().Where(o => o.RowID == et.RowID && o.ExpensesInputID == et.ExpensesInputID && o.ExpensesID == et.ExpensesID).FirstOrDefault();
            if (cE == null)
            {
                eInputRepo.Add(et);
                if (et.ID > 0)
                    retval = true;
            }
            else
            {
                cE.ExpensesID = et.ExpensesID;
                cE.ExpensesInputID = et.ExpensesInputID;
                cE.Modified = et.Modified;
                cE.UpdatedBy = et.UpdatedBy;
                cE.ExpensesInputValue1 = et.ExpensesInputValue1;
                cE.RowID = et.RowID;
                eInputRepo.Edit(cE);
                retval = true;
            }

            return retval;
        }
       

        #endregion
    }   
}
