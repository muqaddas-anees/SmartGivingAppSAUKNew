using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.DAL;
using PortfolioMgt.BAL;

namespace PortfolioMgt.BAL
{
    public class ESignatureDetailBAL
    {
        public static PortfolioMgt.Entity.ESignatureDetail ESignatureDetailBAL_Add(PortfolioMgt.Entity.ESignatureDetail cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ESignatureDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ESignatureDetail>();


            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.ESignatureDetail ESignatureDetailBAL_Update(PortfolioMgt.Entity.ESignatureDetail m)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ESignatureDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ESignatureDetail>();
            var s = pRep.GetAll().Where(o => o.OrganizationGuid == m.OrganizationGuid).FirstOrDefault();
            if (s != null)
            {
                s.BankAccount = m.BankAccount;
                s.BankName = m.BankName;
                s.BankPhone = m.BankPhone;
                s.BankRouting = m.BankRouting;
                s.BusinessAddress = m.BusinessAddress;
                s.BusinessFax = m.BusinessFax;
                s.BusinessLegalName = m.BusinessLegalName;
                s.BusinessPhone = m.BusinessPhone;
                s.BusinessZip = m.BusinessZip;
                s.DBAName = m.DBAName;
                s.EmailEmail = m.EmailEmail;
                s.Est_Avg_Cred_card_Indi_Sale_Amount = m.Est_Avg_Cred_card_Indi_Sale_Amount;
                s.Est_Hig_Cred_card_Indi_Sale_Amount = m.Est_Hig_Cred_card_Indi_Sale_Amount;
                s.FedIDStarted = m.FedIDStarted;
                s.FedTaxID = m.FedTaxID;
                s.IsthisDateFlexible = m.IsthisDateFlexible;
                s.NumberofEmployees = m.NumberofEmployees;
                s.RequestedStartDate = m.RequestedStartDate;
              
                s.SignorDateofBirth = m.SignorDateofBirth;
                s.SignorHomeAddress = m.SignorHomeAddress;
                s.SignorHomePhone = m.SignorHomePhone;
                s.SignorTitle = m.SignorTitle;
                s.SignorZip = m.SignorZip;
                s.Signor_OwnerName = m.Signor_OwnerName;
                s.TaxExemptOrganization = m.TaxExemptOrganization;
                s.TaxFilingType = m.TaxFilingType;
                s.BusinessCity = m.BusinessCity;
                s.BusinessState = m.BusinessState;
                s.SignorCity = m.SignorCity;
                s.ACHPayment = m.ACHPayment;
                // s.up
                // s.UpdatedOrgOn = m.UpdatedOrgOn;
            }
            pRep.Edit(s);
            return s;
        }

        public static bool ESignatureDetailBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.ESignatureDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ESignatureDetail>();
            var retEntity = pRep.GetAll().Where(o => o.ESignatureID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static IQueryable<PortfolioMgt.Entity.ESignatureDetail> ESignatureDetailBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.ESignatureDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ESignatureDetail>();
            return pRep.GetAll();

        }

    }
}
