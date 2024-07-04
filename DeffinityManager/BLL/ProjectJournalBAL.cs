using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMgt.Entity;
using ProjectMgt.DAL;

namespace ProjectMgt.BAL
{
    /// <summary>
    /// Summary description for ProjectJournalBAL
    /// </summary>
    public class ProjectJournalBAL
    {
        public static Project GetProjectsByReference(int projectRef)
        {
            Project project = new Project();
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                project = db.Projects.Where(p => p.ProjectReference == projectRef).Select(p => p).FirstOrDefault();
            }
            return project;
        }

        public static void InsertProjectJournal(Project project)
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                ProjectJournal projectjournal = new ProjectJournal();
                projectjournal.ProjectReference = project.ProjectReference;
                projectjournal.ProjectTitle = project.ProjectTitle;
                projectjournal.ProjectDescription = project.ProjectDescription;
                projectjournal.CostCentre = project.CostCentre;
                projectjournal.OwnerID = project.OwnerID;
                projectjournal.OwnerEmail = project.OwnerEmail;
                projectjournal.OwnerGroupID= project.OwnerGroupID;
                projectjournal.CountryID = project.CountryID;
                projectjournal.BudgetaryCost = project.BudgetaryCost;
                projectjournal.BudgetaryCostLevel3 = project.BudgetaryCostLevel3;
                projectjournal.CityID =project.CityID;
                projectjournal.SiteID =project.SiteID;
                projectjournal.StartDate =project.StartDate;
                projectjournal.ProjectStatusID = project.ProjectStatusID;
                projectjournal.ProjectEndDate = project.ProjectEndDate;
                projectjournal.QADesignate = project.QADesignate;
                projectjournal.DatePending = project.DatePending;
                projectjournal.DateLive = project.DateLive;
                projectjournal.ScheduledQADate = project.ScheduledQADate;
                projectjournal.BaseCurrency = project.BaseCurrency;
                projectjournal.CompletedDate = project.CompletedDate;
                projectjournal.ActualCost = project.ActualCost;
                projectjournal.Custom1 = project.Custom1;
                projectjournal.Custom2 = project.Custom2;
                projectjournal.Portfolio = project.Portfolio;
                projectjournal.Priority = project.Priority;
                projectjournal.RAGstatus = project.RAGstatus;
                projectjournal.CategoryID = project.CategoryID;
                projectjournal.ViewCustomer = project.ViewCustomer;
                projectjournal.RequestorName = project.RequestorName;
                projectjournal.RequestorEmail = project.RequestorEmail;
                projectjournal.SubProgramme = project.SubProgramme;
                projectjournal.POCheck = project.POCheck;
                projectjournal.CustomerReference = project.CustomerReference;
                projectjournal.CustomerUserID = project.CustomerUserID;
                projectjournal.ProjectForecast = project.ProjectForecast;
                projectjournal.BuyingPrice = project.BuyingPrice;
                projectjournal.AccrualsPriorFinancial = project.AccrualsPriorFinancial;
                projectjournal.AccrualsCurrentFinancial = project.AccrualsCurrentFinancial;
                projectjournal.CurrentMonthAccrual = project.CurrentMonthAccrual;
                projectjournal.GrossProfit = project.GrossProfit;
                projectjournal.SalesStaffID = project.SalesStaffID;
                projectjournal.ModifiedBy = sessionKeys.UID;
                projectjournal.ModifiedDate = DateTime.Now;
                db.ProjectJournals.InsertOnSubmit(projectjournal);
                db.SubmitChanges();
            }
        }

    }
}