using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;
namespace DC.BAL
{
    /// <summary>
    /// Summary description for FLSSectionConfigBAL
    /// </summary>
    public class FLSSectionConfigBAL
    {
        public FLSSectionConfigBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void InsertSectionConfig(FLSSectionConfig flsSectionConfig)
        {
            using (DCDataContext db = new DCDataContext())
            {
                db.FLSSectionConfigs.InsertOnSubmit(flsSectionConfig);
                db.SubmitChanges();
            }
        }
        public static void UpdateEmailSection(FLSSectionConfig flsSectionConfig, int customerId)
        {
            using (DCDataContext db = new DCDataContext())
            {
                FLSSectionConfig currentFLSSectionConfig = db.FLSSectionConfigs.Where(f => f.CustomerID == customerId).FirstOrDefault();
                if (currentFLSSectionConfig != null)
                {
                    currentFLSSectionConfig.EmailCustomer = flsSectionConfig.EmailCustomer;
                    currentFLSSectionConfig.EmailEngineer = flsSectionConfig.EmailEngineer;
                    db.SubmitChanges();
                }
            }
        }
        public static void UpdateDocumentSection(FLSSectionConfig flsSectionConfig, int customerId)
        {
            using (DCDataContext db = new DCDataContext())
            {
                FLSSectionConfig currentFLSSectionConfig = db.FLSSectionConfigs.Where(f => f.CustomerID == customerId).FirstOrDefault();
                if (currentFLSSectionConfig != null)
                {
                    currentFLSSectionConfig.Document = flsSectionConfig.Document;
                    db.SubmitChanges();
                }
            }
        }
        public static FLSSectionConfig GetFLSSectionConfigData()
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSSectionConfigs.FirstOrDefault();
            }
        }
        public static List<FLSSectionConfig> GetFLSSectionConfigList()
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSSectionConfigs.ToList();
            }
        }
        public static void CheckEmailNotification(out bool emailEngineer, out bool emailCustomer,int customerId)
        {
            emailEngineer = true;
            emailCustomer = true;
            var sectionConfig = FLSSectionConfigBAL.GetFLSSectionConfigList().Where(g=>g.CustomerID==customerId).FirstOrDefault();
            if (sectionConfig != null)
            {
                emailCustomer = Convert.ToBoolean(sectionConfig.EmailCustomer.HasValue ? sectionConfig.EmailCustomer : true);
                emailEngineer = Convert.ToBoolean(sectionConfig.EmailEngineer.HasValue ? sectionConfig.EmailEngineer : true);
            }
        }
        public static bool CheckDocumentVisibility(int customerId)
        {
            bool visible = true;
            var sectionConfig = FLSSectionConfigBAL.GetFLSSectionConfigList().Where(g => g.CustomerID == customerId).FirstOrDefault();
            if (sectionConfig != null)
            {
                visible = Convert.ToBoolean(sectionConfig.Document.HasValue ? sectionConfig.Document : true);
            }
            return visible;
        }


    }
}