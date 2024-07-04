using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerContract.DAL;
using CustomerContract.Entity;

/// <summary>
/// Summary description for ContractsGroupBAL
/// </summary>
/// 

namespace CustomerContract.BAL
{
    public class ContractsGroupBAL
    {

        public static IEnumerable<ContractorGroup> GetGroupList()
        {
            List<ContractorGroup> contractorGroupList = new List<ContractorGroup>();
            using (CustomerContractdbDataContext db = new CustomerContractdbDataContext())
            {
                contractorGroupList = db.ContractorGroups.Select(c => c).OrderBy(c=>c.Name).ToList();
            }
            return contractorGroupList;
        }

        public static void AddGroup(ContractorGroup contractorGroup)
        {
            using (CustomerContractdbDataContext db = new CustomerContractdbDataContext())
            {
                db.ContractorGroups.InsertOnSubmit(contractorGroup);
                db.SubmitChanges();
            }
        }

        public static void UpdateGroup(ContractorGroup contractorGroup)
        {
            using (CustomerContractdbDataContext db = new CustomerContractdbDataContext())
            {
                ContractorGroup group = db.ContractorGroups.Where(g => g.ID == contractorGroup.ID).Select(g => g).FirstOrDefault();
                if (group != null)
                {
                    group.Name = contractorGroup.Name;
                    db.SubmitChanges();
                }
            }
        }

        public static ContractorGroup GetGroupByID(int id)
        {
            ContractorGroup contractorGroup = new ContractorGroup();
            using (CustomerContractdbDataContext db = new CustomerContractdbDataContext())
            {
                contractorGroup = db.ContractorGroups.Where(g => g.ID == id).Select(g => g).FirstOrDefault();
            }
            return contractorGroup;
        }

        public static void DeleteGroupByID(int id)
        {
            ContractorGroup contractorGroup = new ContractorGroup();
            using (CustomerContractdbDataContext db = new CustomerContractdbDataContext())
            {
                contractorGroup = db.ContractorGroups.Where(g => g.ID == id).Select(g => g).FirstOrDefault();
                if (contractorGroup != null)
                {
                    db.ContractorGroups.DeleteOnSubmit(contractorGroup);
                    db.SubmitChanges();
                }
            }
           
 
        }

        #region Check the group name already exist or not when an inserting the group
        public static bool CheckNameWhileInserting(string name)
        {
            using (CustomerContractdbDataContext db = new CustomerContractdbDataContext())
            {
                ContractorGroup contractorGroup = db.ContractorGroups.Where(g => g.Name == name).Select(g => g).FirstOrDefault();
                if (contractorGroup != null)
                    return true;
                else
                    return false;
            }

        }
        #endregion

        #region Check the group name already exist or not when an updating the group
        public static bool CheckNameWhileUpdating(int id, string name)
        {
            using (CustomerContractdbDataContext db = new CustomerContractdbDataContext())
            {
                ContractorGroup contractorGroup = db.ContractorGroups.Where(g => g.ID != id && g.Name == name).Select(g => g).FirstOrDefault();
                if (contractorGroup != null)
                    return true;
                else
                    return false;
            }
        }
        #endregion
    }
}