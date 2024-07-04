using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;
namespace DC.BAL
{
    /// <summary>
    /// Summary description for FLSFieldsConfigBAL
    /// </summary>
    public class FLSFieldsConfigBAL
    {
        private FLSFieldsConfigBAL()
        {

        }
        public static void InsertConfigData(int customerId,int copyFromCustomerID)
        {
            using (DCDataContext db = new DCDataContext())
            {
                var staticFieldList = db.FLSFieldsConfigs.Where(f => f.CustomerID == 1).ToList();
                List<FLSFieldsConfig> flsConfigList = new List<FLSFieldsConfig>();
                var configList = db.FLSFieldsConfigs.Where(f => f.CustomerID == customerId).ToList();
                foreach (var item in staticFieldList)
                {
                    var fieldName = configList.Where(f => f.DefaultName.ToLower() == item.DefaultName).FirstOrDefault();
                    if (fieldName == null)
                    {
                        
                       
                        flsConfigList.Add(new FLSFieldsConfig
                        {
                            CustomerID = customerId,
                            DefaultName = item.DefaultName,
                            InstanceName = item.InstanceName,
                            IsMandatory = item.IsMandatory,
                            IsVisible = item.IsVisible,
                            DefaultValue = item.DefaultValue,
                            Alignment = item.Alignment,
                            Position = item.Position
                        });
                    }
                }
                if (flsConfigList.Count > 0)
                {
                    db.FLSFieldsConfigs.InsertAllOnSubmit(flsConfigList);
                    db.SubmitChanges();
                }
            }
        }
        public static void InsertConfigData(int customerId)
        {
            using (DCDataContext db = new DCDataContext())
            {
                string[] fields = new string[] { "Source of Request" ,"Requester Name","Requesters Telephone No","Requesters Email Address","Requesters Department","Requesters Job Title","Subject","Details",
    "Site","Type of Request","Status","Category","Sub Category","Logged By","Logged Date/Time","Assigned to Department","Assigned Technician","Scheduled Date/Time",
    "Date and Time Started","Date and Time Closed","Customer Ref","PO Number","Notes","Time Accumulated","Time Worked","Customer Cost Code","Priority","Scheduled End Date/Time","Time Taken to Resolve","Resolution Date/Time","Site details",
    "Requesters Address","Requesters City","Requesters Town","Requesters Postcode","Preferred Date/Time 2","Preferred Date/Time 3"};

                string[] requiredFields = new string[] { "Source of Request" ,"Requester Name","Subject",
    "Site","Type of Request","Status","Category","Logged By","Logged Date/Time","Assigned to Department","Assigned Technician","Scheduled Date/Time",
    "Date and Time Started","Date and Time Closed"};

                string[] RightFields = new string[] { "Logged Date/Time",  "Logged By",
                    "Type of Request","Category","Sub Category","Site","Category","Sub Category","Assigned to Department","Assigned Technician","Scheduled Date/Time", "Scheduled End Date/Time",
               "PO Number", "Time Accumulated","Time Worked","Time Taken to Resolve","Preferred Date/Time 2","Preferred Date/Time 3"};
                var staticFieldList = fields.ToList(); //db.FLSStaticFields.ToList();
                List<FLSFieldsConfig> flsConfigList = new List<FLSFieldsConfig>();
                var configList = db.FLSFieldsConfigs.Where(f => f.CustomerID == customerId).ToList();
                foreach (var item in staticFieldList)
                {
                    var fieldName = configList.Where(f => f.DefaultName.ToLower() == item.ToLower()).FirstOrDefault();
                    bool isMandatory = false;
                    if (fieldName == null)
                    {
                        string P = "Left";
                        if (RightFields.Contains(item))
                        {
                            P = "Right";
                        }

                        if (requiredFields.Contains(item))
                            isMandatory = true;
                        flsConfigList.Add(new FLSFieldsConfig { CustomerID = customerId, DefaultName = item, InstanceName = item,
                                                                IsMandatory = isMandatory,
                                                                IsVisible = true,
                                                                DefaultValue = "",
                                                                Alignment = P,
                                                               
                        });
                    }
                }
                if (flsConfigList.Count > 0)
                {
                    db.FLSFieldsConfigs.InsertAllOnSubmit(flsConfigList);
                    db.SubmitChanges();
                }
            }

            //update field name
            UpdateFiledNamesWithFirstCustomer(customerId);
        }
        public static List<FLSFieldsConfig> GetListOfFields()
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSFieldsConfigs.ToList();
            }
        }

        public static FLSFieldsConfig GetFLSFieldsConfigByID(int id)
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSFieldsConfigs.Where(f => f.ID == id).FirstOrDefault();
            }
        }
        public static FLSFieldsConfig GetFLSFieldsConfigByDefaultName(string defaultName, int customerId)
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSFieldsConfigs.Where(f => f.DefaultName == defaultName && f.CustomerID == customerId).FirstOrDefault();
            }
        }

        public static int GetFLSFieldsConfigCount(int customerID)
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.FLSFieldsConfigs.Where(f => f.CustomerID == customerID).Count();
            }
        }

        public static void UpdateFiledNamesWithFirstCustomer(int CurrentCustomerID)
        {
            using (DCDataContext db = new DCDataContext())
            {
                //get firstcustomer data
                var fList = db.FLSFieldsConfigs.Where(f => f.CustomerID == 1).ToList();
                var cList = db.FLSFieldsConfigs.Where(f => f.CustomerID == CurrentCustomerID).ToList();
                foreach(var f in fList)
                {
                    var c = cList.Where(o => o.DefaultName == f.DefaultName).FirstOrDefault();
                    if (c != null)
                    {
                        c.InstanceName = f.InstanceName;
                        c.IsVisible = f.IsVisible;
                        c.IsMandatory = f.IsMandatory;
                        db.SubmitChanges();
                    }
                    
                }

                
            }
        }
    }
}