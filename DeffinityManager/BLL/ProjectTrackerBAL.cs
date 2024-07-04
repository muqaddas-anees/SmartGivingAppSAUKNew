using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Finance.DAL;
using Finance.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using TimesheetMgt.DAL;
using UserMgt.DAL;

namespace ProjectTracker.BLL
{


/// <summary>
/// Summary description for ProjectTrackerBAL
/// </summary>
    public class ProjectTrackerBAL
    {
        public ProjectTrackerBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Get Variation total by project
        public static double GetVariationCostByProject(int projectReference)
        {
            using (FinanceModuleDataContext fd = new FinanceModuleDataContext())
            {
                return Convert.ToDouble(fd.DeviationReports.Where(p => p.ProjectReference == projectReference).Select(p => p.DeviationValue).Sum());
            }
        }
        #endregion

        #region Labour cost by project
        public static void LabourCost(int projectReference, out double labourCostRemaining, out double labourSpentToDate)
        {

            using (projectTaskDataContext pd = new projectTaskDataContext())
            {
                var labourList = (from r in pd.ProjectBOMDetils
                                  join b in pd.BOM_Types on r.WorkSheetID equals b.ID
                                  where (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false && r.ProjectReference == projectReference && r.Labour != 0 && r.ID != -99
                                  select r).ToList();

                labourCostRemaining = labourList.Select(p => (p.Qty * p.Labour) - (p.Labour * (Convert.ToDouble(p.QtyReceived.HasValue ? p.QtyReceived : 0)))).Sum();

                labourSpentToDate = labourList.Select(p => (p.Labour * (Convert.ToDouble(p.QtyReceived.HasValue ? p.QtyReceived : 0)))).Sum();
            }


        }
        #endregion

        #region Material cost by project
        public static void MaterialCost(int projectReference, out double materialCostRemaining, out double materialSpentToDate)
        {
            using (projectTaskDataContext pd = new projectTaskDataContext())
            {
                var materialList = (from r in pd.ProjectBOMDetils
                                    join b in pd.BOM_Types on r.WorkSheetID equals b.ID
                                    where (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false && r.ProjectReference == projectReference && r.Material != 0 && r.ID != -99
                                    select r).ToList();
                materialCostRemaining = materialList.Select(p => (p.Qty * p.Material) - (p.Material * (Convert.ToDouble(p.QtyReceived.HasValue ? p.QtyReceived : 0)))).Sum();

                materialSpentToDate = materialList.Select(p => (p.Material * (Convert.ToDouble(p.QtyReceived.HasValue ? p.QtyReceived : 0)))).Sum();
            }

        }
        #endregion

        #region Misc cost by project
        public static void MiscCost(int projectReference, out double miscCostRemaining, out double miscSpentToDate)
        {
            using (projectTaskDataContext pd = new projectTaskDataContext())
            {
                var miscList = (from r in pd.ProjectBOMDetils
                                join b in pd.BOM_Types on r.WorkSheetID equals b.ID
                                where (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false && r.ProjectReference == projectReference && r.Mics != 0 && r.ID != -99

                                select r).ToList();
                miscCostRemaining = miscList.Select(p => (p.Qty * p.Mics) - (p.Mics * (Convert.ToDouble(p.QtyReceived.HasValue ? p.QtyReceived : 0)))).Sum();

                miscSpentToDate = miscList.Select(p => (p.Mics * (Convert.ToDouble(p.QtyReceived.HasValue ? p.QtyReceived : 0)))).Sum();
            }

        }
        #endregion

        #region PM and Staff hours cost by project
        public static void PMandStaffHoursCost(int projectReference, out double pmAndStaffHoursCostRemining, out double pmAndStaffHoursSpentToDate)
        {

            using (FinanceModuleDataContext fd = new FinanceModuleDataContext())
            {

                using (TimeSheetDataContext td = new TimeSheetDataContext())
                {
                    using (UserDataContext ud = new UserDataContext())
                    {


                        // var assignedContractorsToProjectsList = fd.AssignedContractorsToProjects.Where(p => p.ProjectReference == pref).ToList();
                        var resourceList = fd.AssignedContractorsToProjects.Where(a => a.ProjectReference == projectReference).ToList();
                        var contractorList = ud.Contractors.Where(u => u.Status.ToLower() == "active").ToList();

                        //take all user type
                        var assignedContractorsToProjectsListPMList = (from r in resourceList
                                                                       join u in contractorList on r.ContractorID equals u.ID
                                                                       // where (u.SID == 1 || u.SID == 2 || u.SID == 3)
                                                                       select new
                                                                       {
                                                                           r.ID,
                                                                           r.ContractorID,
                                                                           r.ProjectReference
                                                                       }).ToList();
                        decimal actualValue = 0;

                        decimal originalPMHoursQuotedTotal = 0;
                        decimal variatioPMHoursQuotedTotal = 0;

                        foreach (var item in assignedContractorsToProjectsListPMList)
                        {
                            //var actualSum = td.TimesheetEntries.Where(p => p.ProjectReference == item.ProjectReference && p.ContractorID == item.ContractorID && p.TimeSheetStatusID != 3).Select(p => p.Hours).Sum();
                            var actualSum = td.TimesheetEntries.Where(p => p.ProjectReference == item.ProjectReference && p.ContractorID == item.ContractorID && p.TimeSheetStatusID != 3).Select(p => p.Hours).Sum();
                            // var actualTotalCost = td.TimesheetEntries.Where(p => p.ProjectReference == item.ProjectReference && p.ContractorID == item.ContractorID && p.TimeSheetStatusID != 3).Select(p => p.TotalCost).Sum();

                            // var actualSum = fd.ProjectPMHours.Where(p => p.ProjectReference == item.ProjectReference && p.SectionType.ToLower() == "actuals" && p.ResourceID == item.ID).Select(p => p.PMHours).Sum();
                            var forcastSum = fd.ProjectPMHours.Where(p => p.ProjectReference == item.ProjectReference && p.SectionType.ToLower() == "forecast" && p.ResourceID == item.ID).Select(p => p.PMHours).Sum();
                            var hoursRate = td.TimeSheetRates.Where(t => t.ContractorsId == item.ContractorID && t.Entrytype == 1).FirstOrDefault(); //1 -Normal hours


                            //  var variationPMHours = fd.VariationBreakdownHours.Where(v => v.ProjectReference == item.ProjectReference && v.UserID == item.ContractorID).Select(v => v.AdditionalHours).Sum();
                            //take only approved status
                            var variationPMHours = (from d in fd.DeviationReports
                                                    join v in fd.VariationBreakdownHours on d.ID equals v.VariationID
                                                    where d.ProjectReference == projectReference && d.Approved == true && v.UserID == item.ContractorID
                                                    select v.AdditionalHours).Sum();
                            decimal variationPM = Convert.ToDecimal(variationPMHours);
                            decimal actual = Convert.ToDecimal(actualSum);
                            decimal forcast = Convert.ToDecimal(forcastSum);
                            if (hoursRate != null)
                            {

                                decimal hoursbyRate = Convert.ToDecimal(Convert.ToDecimal(hoursRate.Hourlyrate_Buying) / hoursRate.Minimumdailyhours);

                                //actualValue = Convert.ToDecimal(actualValue + (actual * hoursbyRate));
                                //actualValue = Convert.ToDecimal(actualValue + Convert.ToDecimal(actualTotalCost));

                                originalPMHoursQuotedTotal = Convert.ToDecimal(originalPMHoursQuotedTotal + (forcast * hoursbyRate));

                                variatioPMHoursQuotedTotal = Convert.ToDecimal(variatioPMHoursQuotedTotal + (variationPM * hoursbyRate));
                            }
                        }

                        var actualTotalCost = td.TimesheetEntries.Where(p => p.ProjectReference == projectReference && p.TimeSheetStatusID != 3).Select(p => p.TotalCost).Sum();
                        actualValue = Convert.ToDecimal(actualTotalCost);
                        pmAndStaffHoursCostRemining = Convert.ToDouble((originalPMHoursQuotedTotal + variatioPMHoursQuotedTotal) - actualValue);
                        pmAndStaffHoursSpentToDate = Convert.ToDouble(actualValue);
                    }
                }

            }



        }
        #endregion

        #region Expense cost by project
        public static void ExpenseCost(int projectReference, out double expenseCostRemaining, out double expenseSpentToDate)
        {

            using (FinanceModuleDataContext fd = new FinanceModuleDataContext())
            {
                var expenseList = fd.ExternalExpenses.Where(e => e.ProjectReference == projectReference).ToList();

                expenseCostRemaining = (from e in expenseList
                                        where e.Expensed != true
                                        select Convert.ToDouble((e.Qty * e.UnitCost))).Sum();
                expenseSpentToDate = (from e in expenseList
                                      where e.Expensed == true
                                      select Convert.ToDouble((e.Qty * e.UnitCost))).Sum();
            }

        }
        #endregion
    }

}