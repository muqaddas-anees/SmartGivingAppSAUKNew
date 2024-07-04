using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;

public static class PortfolioModuleNames
{
    public const string Marketing = "Marketing";
    public const string SchedulinginJobs = "Scheduling in Jobs";
    public const string DispatchBoard = "Dispatch Board";
    public const string Inventory = "Inventory";
    public const string ServicePlans = "Service Plans";
    public const string Equipment = "Equipment";
    public const string Reminders = "Reminders";
    public const string Forms = "Forms";
    public const string App = "App";
    public const string TimesheetsAndExpenseses = "Timesheets & Expenses";
}


namespace PortfolioMgt.BAL
{
    public class PortfolioModuleItem
    {
        public int ModuleID { set; get; }
        public string ModuleName { set; get; }
        
    }

    public class PortfolioModulesBAL
    {
        public static bool PortfolioModulesBAL_ModuleAccess(string moduleName)
        {
            var retval = false;
            if (sessionKeys.Modules == null)
                PortfolioModulesBAL_ModuleSelect();

            var plist = sessionKeys.Modules;
            var module = plist.Where(o => o.ModuleName == moduleName).FirstOrDefault();
            IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
            var pe = pRep.GetAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();
            if (pe != null)
            {
                retval = pe.AllowModules.HasValue ? pe.AllowModules.Value : false;
                if (!retval)
                    retval = !module.IsPaid;
            }

            //check company has plan

            if (sessionKeys.CompanyAccess == null)
                sessionKeys.CompanyAccess = PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_SelectAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && o.IsActive == true && o.IsPaid == true).FirstOrDefault();

            var plan = sessionKeys.CompanyAccess;
            if (plan != null)
            {
                if (sessionKeys.CompanyModules == null)
                    sessionKeys.CompanyModules = PortfolioMgt.BAL.PortfolioBillingModulesBAL.PortfolioBillingModulesBAL_SelectAll().Where(o => o.PortfolioBillingTypeID == plan.PlanID).ToList();

                var pmodulelist = sessionKeys.CompanyModules;

                if(pmodulelist.Count >0)
                {
                    if (pmodulelist.Where(o => o.ModuleID == module.ID).FirstOrDefault() != null)
                        retval = true;
                    else
                        retval = false;
                }
            }
            return  retval;
        }

        public static bool PortfolioModulesBAL_ModuleAccess(string moduleName,int portfolioid)
        {
            if (sessionKeys.Modules == null)
                PortfolioModulesBAL_ModuleSelect();

            var plist = sessionKeys.Modules;
            var p = plist.Where(o => o.ModuleName == moduleName).FirstOrDefault();
            IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
            var pe = pRep.GetAll().Where(o => o.ID == portfolioid).FirstOrDefault();

            return pe.AllowModules.HasValue ? pe.AllowModules.Value : false;
            // return  !p.IsPaid;

        }

        //IPortfolioRepository<ProjectPortfolio> pRep = new PortfolioRepository<ProjectPortfolio>();
        //var pe = pRep.GetAll().Where(o => o.ID == PortfolioID).FirstOrDefault();

       

        public static List<PortfolioModule> PortfolioModulesBAL_ModuleSelect()
        {
            List<PortfolioModule> pList = new List<PortfolioModule>();
            IPortfolioRepository<PortfolioModule> pmRes = new PortfolioRepository<PortfolioModule>();
            pList = pmRes.GetAll().ToList();
            //insert default data
            if (pList.Count == 0)
            {
                //Insert Default data
                PortfolioModulesBAL_ModuleAdd();
                pList = pmRes.GetAll().ToList();
            }
            //new module added
            if (pList.Count >= 8)
            {
                PortfolioModulesBAL_ModuleAdd();
                pList = pmRes.GetAll().ToList();
            }
                sessionKeys.Modules = pList;
            return sessionKeys.Modules;
        }
        public static void PortfolioModulesBAL_ModuleAdd()
        {
            IPortfolioRepository<PortfolioModule> pmRes = new PortfolioRepository<PortfolioModule>();
            var plist = pmRes.GetAll().ToList();
            if (plist.Count == 0)
            {
                pmRes.Add(new PortfolioModule() { ModuleID = 1, ModuleName = PortfolioModuleNames.Marketing, IsPaid = true });
                pmRes.Add(new PortfolioModule() { ModuleID = 2, ModuleName = PortfolioModuleNames.SchedulinginJobs, IsPaid = false });
                pmRes.Add(new PortfolioModule() { ModuleID = 3, ModuleName = PortfolioModuleNames.DispatchBoard, IsPaid = false });
                pmRes.Add(new PortfolioModule() { ModuleID = 4, ModuleName = PortfolioModuleNames.Inventory, IsPaid = true });
                pmRes.Add(new PortfolioModule() { ModuleID = 5, ModuleName = PortfolioModuleNames.ServicePlans, IsPaid = true });
                pmRes.Add(new PortfolioModule() { ModuleID = 6, ModuleName = PortfolioModuleNames.Equipment, IsPaid = true });
                pmRes.Add(new PortfolioModule() { ModuleID = 7, ModuleName = PortfolioModuleNames.Reminders, IsPaid = true });
                pmRes.Add(new PortfolioModule() { ModuleID = 8, ModuleName = PortfolioModuleNames.Forms, IsPaid = true });
                pmRes.Add(new PortfolioModule() { ModuleID = 9, ModuleName = PortfolioModuleNames.App, IsPaid = true });
            }
            //App new module
            if(pmRes.GetAll().Where(o=>o.ModuleName == PortfolioModuleNames.App).Count() ==0)
            {
                pmRes.Add(new PortfolioModule() { ModuleID = 9, ModuleName = PortfolioModuleNames.App, IsPaid = true });
            }
            //Timesheet module
            if (pmRes.GetAll().Where(o => o.ModuleName == PortfolioModuleNames.TimesheetsAndExpenseses).Count() == 0)
            {
                pmRes.Add(new PortfolioModule() { ModuleID = 10, ModuleName = PortfolioModuleNames.TimesheetsAndExpenseses, IsPaid = true });
            }


        }
        public static PortfolioModule PortfolioModulesBAL_ModuleUpdate(int ModuleID, bool IsPaid)
        {
            IPortfolioRepository<PortfolioModule> pmRes = new PortfolioRepository<PortfolioModule>();
            var p = pmRes.GetAll().Where(o => o.ModuleID == ModuleID).FirstOrDefault();
            if (p != null)
            {
                p.IsPaid = IsPaid;
                pmRes.Edit(p);
            }
            return p;
        }
        public static PortfolioModule PortfolioModulesBAL_ModuleUpdate(int ModuleID,string moduleDescription,string imagepath, bool IsPaid)
        {
            IPortfolioRepository<PortfolioModule> pmRes = new PortfolioRepository<PortfolioModule>();
            var p = pmRes.GetAll().Where(o => o.ModuleID == ModuleID).FirstOrDefault();
            if (p != null)
            {
                p.ModuleDescription = moduleDescription;
                p.ModuleImage = imagepath;
                p.IsPaid = IsPaid;
                pmRes.Edit(p);
            }
            return p;
        }

    }
}
