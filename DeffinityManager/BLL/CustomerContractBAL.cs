using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerContract.Entity;
using CustomerContract.DAL;
using UserMgt.DAL;
using UserMgt.Entity;

/// <summary>
/// Summary description for CustomerContractBAL
/// </summary>
/// 
namespace CustomerContract.BAL
{
    public class CustomerContractBAL
    {
        public CustomerContractBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Contract Journal



        public static bool CustomerContract_Jouranl_IsModified(Customer_Contract CurrentEntry, Customer_ContractsJournal PreviousEntry)
        {
            bool isChanged = false;
            if (PreviousEntry != null)
            {
                if (CurrentEntry.AnniversaryNotes != PreviousEntry.AnniversaryNotes)
                    isChanged = true;
                else if (CurrentEntry.ContractDescription != PreviousEntry.ContractDescription)
                    isChanged = true;
                else if (CurrentEntry.ContractDuration != PreviousEntry.ContractDuration)
                    isChanged = true;
                else if (CurrentEntry.ContractDurationType != PreviousEntry.ContractDurationType)
                    isChanged = true;
                else if (CurrentEntry.ContractTitle != PreviousEntry.ContractTitle)
                    isChanged = true;
                else if (CurrentEntry.DetailsOfSLA != PreviousEntry.DetailsOfSLA)
                    isChanged = true;
                else if (CurrentEntry.DetailsOfTechnologyUsed != PreviousEntry.DetailsOfTechnologyUsed)
                    isChanged = true;
                else if (CurrentEntry.EndDate != PreviousEntry.EndDate)
                    isChanged = true;
                else if (CurrentEntry.GeneralNotes != PreviousEntry.GeneralNotes)
                    isChanged = true;
                else if (CurrentEntry.GovernigLaw != PreviousEntry.GovernigLaw)
                    isChanged = true;
                else if (CurrentEntry.GroupID != PreviousEntry.GroupID)
                    isChanged = true;
                else if (CurrentEntry.InitialValue != PreviousEntry.InitialValue)
                    isChanged = true;
                else if (CurrentEntry.NDADetails != PreviousEntry.NDADetails)
                    isChanged = true;
                else if (CurrentEntry.NonCompleteClauses != PreviousEntry.NonCompleteClauses)
                    isChanged = true;
                else if (CurrentEntry.NotificationPeriod != PreviousEntry.NotificationPeriod)
                    isChanged = true;
                else if (CurrentEntry.NotificationType != PreviousEntry.NotificationType)
                    isChanged = true;
                else if (CurrentEntry.OwnerID != PreviousEntry.OwnerID)
                    isChanged = true;
                else if (CurrentEntry.RenewalDate != PreviousEntry.RenewalDate)
                    isChanged = true;
                else if (CurrentEntry.RollingContract != PreviousEntry.RollingContract)
                    isChanged = true;
                else if (CurrentEntry.StartDate != PreviousEntry.StartDate)
                    isChanged = true;
                else if (CurrentEntry.TerminationDate != PreviousEntry.TerminationDate)
                    isChanged = true;
                else if (CurrentEntry.VendorOrCustomer != PreviousEntry.VendorOrCustomer)
                    isChanged = true;

            }
            else {
                isChanged = true;
            }
            return isChanged;
        }
        public static void CustomerContract_Jouranl_Insert_byJournalEntry(Customer_Contract currentEntry, Customer_ContractsJournal previousEntry, int ModifiedBy)
        {
            if (previousEntry == null)
            {
                using (CustomerContractdbDataContext cb = new CustomerContractdbDataContext())
                {
                    previousEntry = cb.Customer_ContractsJournals.Where(p => p.ContractID == currentEntry.ID).OrderByDescending(p => p.ModifiedDate).FirstOrDefault();
                }
            }
            CustomerContract_Jouranl_Insert(currentEntry,previousEntry,ModifiedBy);
        }
        public static void CustomerContract_Jouranl_Insert(Customer_Contract currentEntry,Customer_ContractsJournal previousEntry,int ModifiedBy)
        {
            
            //Check the data is modified or not
            bool isChanged = CustomerContract_Jouranl_IsModified(currentEntry,previousEntry);
            if (isChanged)
            {
                Customer_ContractsJournal cj = null;
                using (CustomerContractdbDataContext cb = new CustomerContractdbDataContext())
                {
                    //ID filed
                    
                    cj = new Customer_ContractsJournal();
                    cj.ContractID = currentEntry.ID;
                    cj.AnniversaryNotes = currentEntry.AnniversaryNotes;
                    cj.ContractDescription = currentEntry.ContractDescription;
                    cj.ContractDuration = currentEntry.ContractDuration;
                    cj.ContractDurationType = currentEntry.ContractDurationType;
                    cj.ContractTitle = currentEntry.ContractTitle;
                    cj.CustomerID = currentEntry.CustomerID;
                    cj.DetailsOfSLA = currentEntry.DetailsOfSLA;
                    cj.DetailsOfTechnologyUsed = currentEntry.DetailsOfTechnologyUsed;
                    cj.EndDate = currentEntry.EndDate;
                    cj.GeneralNotes = currentEntry.GeneralNotes;
                    cj.GovernigLaw = currentEntry.GovernigLaw;
                    cj.GroupID = currentEntry.GroupID;
                    cj.InitialValue = currentEntry.InitialValue;
                    cj.ModifiedBy = ModifiedBy;
                    cj.ModifiedDate = DateTime.Now;
                    cj.NDADetails = currentEntry.NDADetails;
                    cj.NonCompleteClauses = currentEntry.NonCompleteClauses;
                    cj.NotificationPeriod = currentEntry.NotificationPeriod;
                    cj.NotificationType = currentEntry.NotificationType;
                    cj.OwnerID = currentEntry.OwnerID;
                    cj.RenewalDate = currentEntry.RenewalDate;
                    cj.RollingContract = currentEntry.RollingContract;
                    cj.StartDate = currentEntry.StartDate;
                    cj.TerminationDate = currentEntry.TerminationDate;
                    cj.VendorOrCustomer = currentEntry.VendorOrCustomer;
                    cb.Customer_ContractsJournals.InsertOnSubmit(cj);
                    cb.SubmitChanges();
                }
            }
        }

        //Get first Collection
        public static List<JournalDisplay> JournalDisplay_ListByContract(int ContractID)
        {
            List<JournalDisplay> jlist = new List<JournalDisplay>();
            List<UserMgt.Entity.Contractor> ulist = new List<UserMgt.Entity.Contractor>();
            //get the user data
            using (UserDataContext ud = new UserDataContext())
            {
                ulist = ud.Contractors.Select(p=>p).ToList();
            }

            using (CustomerContractdbDataContext cc = new CustomerContractdbDataContext())
            {
                List<ContractorGroup> cGroup = cc.ContractorGroups.Select(p => p).ToList();
                List<Customer_ContractsJournal> cjlist = cc.Customer_ContractsJournals.Where(p => p.ContractID == ContractID).OrderBy(p=>p.ID).ToList();
                //take first record
                if (cjlist.Count > 0)
                {
                    jlist.AddRange(Customer_ContractsJournal_First(cjlist[0], cGroup, ulist, ulist.Where(p => p.ID == cjlist[0].ModifiedBy).Select(p => p.ContractorName).FirstOrDefault()));
                }
                //check for second record on wards
                if (cjlist.Count > 1)
                {
                    for (int j = 1; j <= cjlist.Count()-1; j++)
                    {
                        jlist.AddRange(Customer_ContractsJournal_List(cjlist[j], cjlist[j - 1], cGroup, ulist, ulist.Where(p => p.ID == cjlist[j].ModifiedBy).Select(p => p.ContractorName).FirstOrDefault()));
                    }
                }

            }


            return jlist;
        }

        public static List<JournalDisplay> Customer_ContractsJournal_First(Customer_ContractsJournal cj,List<ContractorGroup> cGroup,List<UserMgt.Entity.Contractor> ulist,string ModifiedByName)
        {
            List<JournalDisplay> lj = new List<JournalDisplay>();
            JournalDisplay j = null;

            if (cj.AnniversaryNotes != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Anniversary Notes:";
                j.FieldValue = cj.AnniversaryNotes;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }

            if (cj.ContractDescription != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Contract Description:";
                j.FieldValue = cj.ContractDescription;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }

            if (cj.ContractDuration != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Contract Duration:";
                j.FieldValue = cj.ContractDuration.ToString();
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.ContractDurationType != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Contract Duration Type:";
                j.FieldValue = cj.ContractDurationType.ToString();
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.ContractTitle != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Contract Title:";
                j.FieldValue = cj.ContractTitle;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.DetailsOfSLA != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Details of SLA:";
                j.FieldValue = cj.DetailsOfSLA;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.DetailsOfTechnologyUsed != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Details of Technology Used:";
                j.FieldValue = cj.DetailsOfTechnologyUsed;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.EndDate != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Contract Description:";
                j.FieldValue = cj.EndDate.HasValue? cj.EndDate.Value.ToShortDateString():string.Empty;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.GeneralNotes != null)
            {
                j = new JournalDisplay();
                j.FieldName = "General Notes:";
                j.FieldValue = cj.GeneralNotes;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.GovernigLaw != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Governig Law:";
                j.FieldValue = cj.GovernigLaw;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.GroupID != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Group:";
                j.FieldValue = cGroup.Where(p=>p.ID == cj.GroupID).Select(p=>p.Name).FirstOrDefault();
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.InitialValue != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Initial Value:";
                j.FieldValue = cj.InitialValue.ToString();
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.NDADetails != null)
            {
                j = new JournalDisplay();
                j.FieldName = "NDA Details:";
                j.FieldValue = cj.NDADetails;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.NonCompleteClauses != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Non Complete Clauses:";
                j.FieldValue = cj.NonCompleteClauses;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.NotificationPeriod != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Notification Period:";
                j.FieldValue = cj.NotificationPeriod.HasValue?cj.NotificationPeriod.Value.ToString():"0";
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.NotificationType != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Notification Type:";
                j.FieldValue = cj.NotificationType.HasValue?cj.NotificationType.Value.ToString(): "0";
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.OwnerID != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Owner:";
                j.FieldValue = ulist.Where(p=>p.ID == cj.OwnerID).Select(p=>p.ContractorName).FirstOrDefault();
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.RenewalDate != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Renewal Date:";
                j.FieldValue = cj.RenewalDate.HasValue?cj.RenewalDate.Value.ToShortDateString():string.Empty;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }

            if (cj.RollingContract != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Rolling Contract:";
                j.FieldValue = cj.RollingContract.HasValue?cj.RollingContract.Value.ToString():"False";
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.StartDate != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Start Date:";
                j.FieldValue = cj.StartDate.HasValue?cj.StartDate.Value.ToShortDateString():string.Empty;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.TerminationDate != null)
            {
                j = new JournalDisplay();
                j.FieldName = "Termination Date:";
                j.FieldValue = cj.TerminationDate.HasValue?cj.TerminationDate.Value.ToShortDateString():string.Empty;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            //if (cj.TerminationDate != null)
            //{
            //    j = new JournalDisplay();
            //    j.FieldName = "Termination Date:";
            //    j.FieldValue = cj.VendorOrCustomer.HasValue ? cj.TerminationDate.Value.ToShortDateString() : string.Empty;
            //    j.ModifiedDate = cj.ModifiedDate;
            //    j.ModifiedBy = ModifiedByName;
            //}
           
            return lj;
        }

        public static List<JournalDisplay> Customer_ContractsJournal_List(Customer_ContractsJournal cj,Customer_ContractsJournal cj_prv, List<ContractorGroup> cGroup, List<UserMgt.Entity.Contractor> ulist, string ModifiedByName)
        {
            List<JournalDisplay> lj = new List<JournalDisplay>();
            JournalDisplay j = null;

            if (cj.AnniversaryNotes != cj_prv.AnniversaryNotes)
            {
                j = new JournalDisplay();
                j.FieldName = "Anniversary Notes:";
                j.FieldValue = cj.AnniversaryNotes;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }

            if (cj.ContractDescription != cj_prv.ContractDescription)
            {
                j = new JournalDisplay();
                j.FieldName = "Contract Description:";
                j.FieldValue = cj.ContractDescription;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }

            if (cj.ContractDuration != cj_prv.ContractDuration)
            {
                j = new JournalDisplay();
                j.FieldName = "Contract Duration:";
                j.FieldValue = cj.ContractDuration.ToString();
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.ContractDurationType != cj_prv.ContractDurationType)
            {
                j = new JournalDisplay();
                j.FieldName = "Contract Duration Type:";
                j.FieldValue = cj.ContractDurationType.ToString();
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.ContractTitle != cj_prv.ContractTitle)
            {
                j = new JournalDisplay();
                j.FieldName = "Contract Title:";
                j.FieldValue = cj.ContractTitle;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.DetailsOfSLA != cj_prv.DetailsOfSLA)
            {
                j = new JournalDisplay();
                j.FieldName = "Details of SLA:";
                j.FieldValue = cj.DetailsOfSLA;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.DetailsOfTechnologyUsed != cj_prv.DetailsOfTechnologyUsed)
            {
                j = new JournalDisplay();
                j.FieldName = "Details of Technology Used:";
                j.FieldValue = cj.DetailsOfTechnologyUsed;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.EndDate != cj_prv.EndDate)
            {
                j = new JournalDisplay();
                j.FieldName = "Contract Description:";
                j.FieldValue = cj.EndDate.HasValue ? cj.EndDate.Value.ToShortDateString() : string.Empty;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.GeneralNotes != cj_prv.GeneralNotes)
            {
                j = new JournalDisplay();
                j.FieldName = "General Notes:";
                j.FieldValue = cj.GeneralNotes;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.GovernigLaw != cj_prv.GovernigLaw)
            {
                j = new JournalDisplay();
                j.FieldName = "Governig Law:";
                j.FieldValue = cj.GovernigLaw;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.GroupID != cj_prv.GroupID)
            {
                j = new JournalDisplay();
                j.FieldName = "Group:";
                j.FieldValue = cGroup.Where(p => p.ID == cj.GroupID).Select(p => p.Name).FirstOrDefault();
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.InitialValue != cj_prv.InitialValue)
            {
                j = new JournalDisplay();
                j.FieldName = "Initial Value:";
                j.FieldValue = cj.InitialValue.ToString();
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.NDADetails != cj_prv.NDADetails)
            {
                j = new JournalDisplay();
                j.FieldName = "NDA Details:";
                j.FieldValue = cj.NDADetails;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.NonCompleteClauses != cj_prv.NonCompleteClauses)
            {
                j = new JournalDisplay();
                j.FieldName = "Non Complete Clauses:";
                j.FieldValue = cj.NonCompleteClauses;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.NotificationPeriod != cj_prv.NotificationPeriod)
            {
                j = new JournalDisplay();
                j.FieldName = "Notification Period:";
                j.FieldValue = cj.NotificationPeriod.HasValue ? cj.NotificationPeriod.Value.ToString() : "0";
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.NotificationType != cj_prv.NotificationType)
            {
                j = new JournalDisplay();
                j.FieldName = "Notification Type:";
                j.FieldValue = cj.NotificationType.HasValue ? cj.NotificationType.Value.ToString() : "0";
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.OwnerID != cj_prv.OwnerID)
            {
                j = new JournalDisplay();
                j.FieldName = "Owner:";
                j.FieldValue = ulist.Where(p => p.ID == cj.OwnerID).Select(p => p.ContractorName).FirstOrDefault();
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.RenewalDate != cj_prv.RenewalDate)
            {
                j = new JournalDisplay();
                j.FieldName = "Renewal Date:";
                j.FieldValue = cj.RenewalDate.HasValue ? cj.RenewalDate.Value.ToShortDateString() : string.Empty;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }

            if (cj.RollingContract != cj_prv.RollingContract)
            {
                j = new JournalDisplay();
                j.FieldName = "Rolling Contract:";
                j.FieldValue = cj.RollingContract.HasValue ? cj.RollingContract.Value.ToString() : "False";
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.StartDate != cj_prv.StartDate)
            {
                j = new JournalDisplay();
                j.FieldName = "Start Date:";
                j.FieldValue = cj.StartDate.HasValue ? cj.StartDate.Value.ToShortDateString() : string.Empty;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            if (cj.TerminationDate != cj_prv.TerminationDate)
            {
                j = new JournalDisplay();
                j.FieldName = "Termination Date:";
                j.FieldValue = cj.TerminationDate.HasValue ? cj.TerminationDate.Value.ToShortDateString() : string.Empty;
                j.ModifiedDate = cj.ModifiedDate;
                j.ModifiedBy = ModifiedByName;
                lj.Add(j);
            }
            //if (cj.TerminationDate != null)
            //{
            //    j = new JournalDisplay();
            //    j.FieldName = "Termination Date:";
            //    j.FieldValue = cj.VendorOrCustomer.HasValue ? cj.TerminationDate.Value.ToShortDateString() : string.Empty;
            //    j.ModifiedDate = cj.ModifiedDate;
            //    j.ModifiedBy = ModifiedByName;
            //}

            return lj;
        }

        public static Customer_ContractsJournal JournalDisplay_SelectByDate(int cid)
        {
            Customer_ContractsJournal cdj = new Customer_ContractsJournal();
            using (CustomerContractdbDataContext dc = new CustomerContractdbDataContext())
            {

                cdj = (from c in dc.Customer_ContractsJournals
                       where c.ContractID == cid
                       orderby c.ModifiedDate ascending
                       select c).FirstOrDefault();
            }

            return cdj;
        }
        #endregion
    }
}