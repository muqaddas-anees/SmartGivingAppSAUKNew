using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;

namespace PortfolioMgt.BAL
{
    public class ProjectPortfolioBAL
    {

        public static void Portfolio_SaveImageData(int portfolioID, byte[] imagedata)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();
            var p = pRep.GetAll().Where(o => o.ID == portfolioID).FirstOrDefault();
            if (p != null)
            {
                p.ImageData = imagedata;
                pRep.Edit(p);
            }
        }
        public static void AddUpdateTrailPerioid(int partnerID, int portfolioID)
        {
            var retval = false;
            var ptdate = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Select(partnerID);
            if (ptdate != null)
            {

                if (ptdate.TrailDays.HasValue)
                {
                    if (ptdate.TrailDays.Value > 0)
                    {
                        var mtdata = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(portfolioID);

                        if (mtdata != null)
                        {
                            if (mtdata.TrailStartDate == null)
                            {
                                mtdata.IsInTrailPeriod = true;
                                mtdata.TrailStartDate = DateTime.Now;
                                mtdata.TrailEndDate = DateTime.Now.AddDays(ptdate.TrailDays.Value);

                                PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(mtdata);
                            }

                        }

                    }

                }
            }

        }

        public static void UpdateThankyouMail(int portfolioid, bool enableThankyouMail)
        {
            var ptdate = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(portfolioid);
            ptdate.EnableThankyouMail = enableThankyouMail;
            PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(ptdate);

        }
            //public static List<ProjectPortfolio> ProjectPortfolioBAL_SelectAll()
            //{
            //    IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
            //    return pRep.GetAll().ToList();
            //}
            public static IQueryable<ProjectPortfolio> ProjectPortfolioBAL_SelectAll()
        {
            IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
            return pRep.GetAll();
        }
        public static IQueryable<ProjectPortfolio> ProjectPortfolioBAL_GetOranizationsAll()
        {
            IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
            return pRep.GetAll().Where(o=>o.PortFolio.Length >0).Where(o=>(o.IsServiceCompany.HasValue?o.IsServiceCompany.Value:false) == false).Where(o => (o.IsGroup.HasValue ? o.IsGroup.Value : false) == false);
        }
        public static IQueryable<ProjectPortfolio> ProjectPortfolioBAL_GetServiceCompaniesAll()
        {
            IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
            return pRep.GetAll().Where(o => o.PortFolio.Length > 0).Where(o => (o.IsServiceCompany.HasValue ? o.IsServiceCompany.Value : false) == true);
        }
        public static IQueryable<ProjectPortfolio> ProjectPortfolioBAL_GetGroupsAll()
        {
            IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
            return pRep.GetAll().Where(o => o.PortFolio.Length > 0).Where(o => (o.IsGroup.HasValue ? o.IsGroup.Value : false) == true);
        }
        public static IQueryable<v_ProjectPortfolio> v_ProjectPortfolioBAL_SelectAll()
        {
            IPortfolioRepository<v_ProjectPortfolio> pRep = new PortfolioRepository<v_ProjectPortfolio>();
            return pRep.GetAll();
        }
        public static ProjectPortfolio ProjectPortfolioBAL_SelectByID(int id)
        {
            IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
            return pRep.GetAll().Where(o=>o.ID == id).FirstOrDefault();
        }
        public static bool ProjectPortfolioBAL_IsActive(int PortfolioID)
        {
            bool retval = false;
            IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
            var p = pRep.GetAll().Where(o => o.ID == PortfolioID).FirstOrDefault();
            if(p != null)
            {
                retval = p.Visible.HasValue ? p.Visible.Value : false;
            }
            return false;
        }
        public static ProjectPortfolio ProjectPortfolioBAL_Add(ProjectPortfolio p)
        {
            IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
             pRep.Add(p);
            return p;
        }
        public static ProjectPortfolio ProjectPortfolioBAL_UpdateVisibility(int PortfolioID, bool visible)
        {
            IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
            var pe = pRep.GetAll().Where(o => o.ID == PortfolioID).FirstOrDefault();
            if (pe != null)
            {
                pe.Visible = visible;
                pRep.Edit(pe);
            }
            return pe;
        }

        public static ProjectPortfolio ProjectPortfolioBAL_UpdateAllowModules(int PortfolioID, bool allowmodules)
        {
            IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
            var pe = pRep.GetAll().Where(o => o.ID == PortfolioID).FirstOrDefault();
            if (pe != null)
            {
                pe.AllowModules = allowmodules;
                pRep.Edit(pe);
            }
            return pe;
        }
        public static ProjectPortfolio ProjectPortfolioBAL_Update(ProjectPortfolio p)
        {
            IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
            var pe = pRep.GetAll().Where(o => o.ID == p.ID).FirstOrDefault();
            if (pe != null)
            {
                pe.AccountNumber = p.AccountNumber;
                pe.Address = p.Address;
                pe.BankName = p.BankName;
                pe.Description = p.Description;
                pe.docenable = p.docenable;
                pe.EmailAddress = p.EmailAddress;
                pe.EndDate = p.EndDate;
                pe.FromEmail = p.FromEmail;
                pe.MaxBudget = p.MaxBudget;
                pe.OtherNumber = p.OtherNumber;
                pe.Owner = p.Owner;
                pe.KeyContactName = p.KeyContactName;
                pe.PortFolio = p.PortFolio;
                pe.PortfolioTypeID = p.PortfolioTypeID;
                pe.RegistrationNumber = p.RegistrationNumber;
                pe.StartDate = pe.StartDate;
                pe.Status = pe.Status;
                pe.TaxReg = pe.TaxReg;
                pe.TelephoneNumber = p.TelephoneNumber;
                pe.Visible = p.Visible;
                pe.Accepted = p.Accepted;
                pe.AcceptedDate = p.AcceptedDate;
                pe.ApplicationReceived = p.ApplicationReceived;
                pe.ApplicationReceivedDate = p.ApplicationReceivedDate;
                pe.ApplicationSenttoClient = p.ApplicationSenttoClient;
                pe.ApplicationSenttoClientDate = p.ApplicationSenttoClientDate;
                pe.BaseUrl = p.BaseUrl;
                pe.CardConnectApproved = p.CardConnectApproved;
                pe.SalesNotes = p.SalesNotes;
                pe.InterestedInCC = p.InterestedInCC;
                pe.ResendLoginDateTime = p.ResendLoginDateTime;
                pe.PartnerID = p.PartnerID;
                pe.ShowDiagnostic = p.ShowDiagnostic;
                pe.InstanceDuration = p.InstanceDuration;
                pe.EnableInternalCost = p.EnableInternalCost;
                pe.IsInTrailPeriod = p.IsInTrailPeriod;
                pe.IsPaid = p.IsPaid;
                pe.TrailEndDate = p.TrailEndDate;
                pe.TrailStartDate = p.TrailStartDate;
                pe.NoofUsers = p.NoofUsers;
                pe.PlanID = p.PlanID;
                pe.LogoPath = p.LogoPath;
                pe.OrgarnizationApproval = p.OrgarnizationApproval;
                pe.OrgarnizationGUID = p.OrgarnizationGUID;
                pe.OrgarnizationStatus = p.OrgarnizationStatus;
                pe.State = p.State;
                pe.Town = p.Town;
                pe.Postcode = p.Postcode;
                pe.CountryID = p.CountryID;
                pe.DenominationDetailsID = p.DenominationDetailsID;
                pe.SubDenominationDetailsID = p.SubDenominationDetailsID;
                pe.GroupDetailsID = p.GroupDetailsID;
                pe.Contribution = p.Contribution;
                pe.DateStamp = p.DateStamp;
                pe.SignatureSentToCardConnect = p.SignatureSentToCardConnect;
                pe.SignatureSentToCardConnectOn = p.SignatureSentToCardConnectOn;
                pe.SignatureSentToOrg = p.SignatureSentToOrg;
                pe.SignatureSentToOrgOn = p.SignatureSentToOrgOn;
                pe.SignatureSetLoginToOrg = p.SignatureSetLoginToOrg;
                pe.SignatureSetLoginToOrgOn = p.SignatureSetLoginToOrgOn;
                pe.SignatureUpdatedOrgOn = p.SignatureUpdatedOrgOn;
                pe.OrgUniqID = p.OrgUniqID;
                pe.IsServiceCompany = p.IsServiceCompany;
                pe.EnableThankyouMail = p.EnableThankyouMail;
                pe.CostCentre = p.CostCentre;
                pRep.Edit(pe);
            }
            return p;
        }

        public static List<PortfolioMgt.Entity.ProjectPortfolio_AdminDisplayResult> ProjectPortfolioBAL_AdminDisplay(int partnerID)
        {
            List<PortfolioMgt.Entity.ProjectPortfolio_AdminDisplayResult> pList = new List<ProjectPortfolio_AdminDisplayResult>();
            using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioDataContext())
            {
                pList = pd.ProjectPortfolio_AdminDisplay(partnerID).ToList();
            }
            return pList;
        }

        public static string ProjectPortfolioBAL_UpdateOrgID(string orgguid)
        {
            string OrgID = string.Empty;
            IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();

            var org=  pRep.GetAll().Where(o => o.OrgarnizationGUID == orgguid).FirstOrDefault();

            if(org.OrgUniqID != null)
            {
                OrgID = org.OrgUniqID;
            }
            else
            {
                //update the orgid
                var orgList = pRep.GetAll().Where(o=>o.OrgUniqID != null).ToList();

                var tempid = org.PortFolio.Length > 0 ? (org.PortFolio.Replace(" ", "").Trim()) : "";

                if(orgList.Where(o=>o.OrgUniqID == tempid).FirstOrDefault() ==  null)
                {
                    org.OrgUniqID = tempid;
                    pRep.Edit(org);
                    OrgID = org.OrgUniqID;
                }
                else
                {
                    org.OrgUniqID = tempid+"1";
                    pRep.Edit(org);
                    OrgID = org.OrgUniqID;
                }

            }


            return OrgID;
        }

    }
}
