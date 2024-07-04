using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;

namespace PortfolioMgt.BAL
{
    public class MaintenanceBAL
    {
        public static List<MaintenanceType> MaintenanceType_Select(int CustomerID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.MaintenanceType> mRep = new PortfolioRepository<PortfolioMgt.Entity.MaintenanceType>();
            var mlist = mRep.GetAll().Where(o => o.PortfolioID == CustomerID).ToList();
            if(mlist.Count == 0)
            {
                string[] dData = { "Maintenance", "Repair", "Service", "Replace", "Decommission", "Upgrade" };
                //add default data
                foreach(var d in dData)
                {
                    MaintenanceType_Add(d, CustomerID);
                }
            }
            return mRep.GetAll().Where(o => o.PortfolioID == CustomerID).ToList();
        }
        public static MaintenanceType MaintenanceType_SelectByID(int ID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.MaintenanceType> mRep = new PortfolioRepository<PortfolioMgt.Entity.MaintenanceType>();
            return mRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
        }
        public static bool MaintenanceType_DeleteByID(int ID)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.MaintenanceType> mRep = new PortfolioRepository<PortfolioMgt.Entity.MaintenanceType>();
            var dItem = mRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            if(dItem != null)
            {
                mRep.Delete(dItem);
                retval = true;
            }
            return retval;

        }
        public static MaintenanceType MaintenanceType_Add(string Name, int CustomerID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.MaintenanceType> mRep = new PortfolioRepository<PortfolioMgt.Entity.MaintenanceType>();
            var mdata = mRep.GetAll().Where(o => o.PortfolioID == CustomerID && o.Name.ToLower() == Name.ToLower()).FirstOrDefault();
            if (mdata == null)
            {
                mdata = new MaintenanceType();
                mdata.Name = Name;
                mdata.PortfolioID = CustomerID;
                mRep.Add(mdata);
            }
            return mdata;
        }
        public static MaintenanceType MaintenanceType_Update(string Name, int CustomerID, int ID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.MaintenanceType> mRep = new PortfolioRepository<PortfolioMgt.Entity.MaintenanceType>();
            var mdata = mRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            if (mdata != null)
            {
                mdata.Name = Name;
                mRep.Edit(mdata);
            }
            return mdata;
        }
        public static bool MaintenanceType_IsExists(string Name, int CustomerID, int ID = 0)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.MaintenanceType> mRep = new PortfolioRepository<PortfolioMgt.Entity.MaintenanceType>();
            if (ID == 0)
            {
                var mdata = mRep.GetAll().Where(o => o.PortfolioID == CustomerID && o.Name.ToLower() == Name.ToLower()).FirstOrDefault();
                if(mdata != null)
                    retval = true;
            }
            else
            {
                var mdata = mRep.GetAll().Where(o => o.PortfolioID == CustomerID && o.Name.ToLower() == Name.ToLower() && o.ID != ID).FirstOrDefault();
                if (mdata != null)
                    retval = true;
            }
            return retval;
        }




        public static List<V_MaintenanceSchedule> MaintenanceSchedule_Select(int CustomerID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.V_MaintenanceSchedule> mRep = new PortfolioRepository<PortfolioMgt.Entity.V_MaintenanceSchedule>();
            return mRep.GetAll().Where(o => o.PortfolioID == CustomerID).ToList();
        }
        public static MaintenanceSchedule MaintenanceSchedule_SelectByID(int ID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.MaintenanceSchedule> mRep = new PortfolioRepository<PortfolioMgt.Entity.MaintenanceSchedule>();
            return mRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
        }
        public static bool MaintenanceSchedule_DeleteByID(int ID)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.MaintenanceSchedule> mRep = new PortfolioRepository<PortfolioMgt.Entity.MaintenanceSchedule>();
            var dItem = mRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            if(dItem != null)
            {
                mRep.Delete(dItem);
                retval = true;
            }

            return retval;
        }
        public static MaintenanceSchedule MaintenanceSchedule_Add(MaintenanceSchedule mEntity)
        {
            IPortfolioRepository<PortfolioMgt.Entity.MaintenanceSchedule> mRep = new PortfolioRepository<PortfolioMgt.Entity.MaintenanceSchedule>();
            mEntity.DateCreated = DateTime.Now;
            mEntity.LoggedBy = sessionKeys.UID;
            mEntity.PortfolioID = sessionKeys.PortfolioID;
            mRep.Add(mEntity);
            
            return mEntity;
        }
        public static MaintenanceSchedule MaintenanceSchedule_Update(MaintenanceSchedule mEntity)
        {
            IPortfolioRepository<PortfolioMgt.Entity.MaintenanceSchedule> mRep = new PortfolioRepository<PortfolioMgt.Entity.MaintenanceSchedule>();
            var mdata = mRep.GetAll().Where(o => o.ID == mEntity.ID).FirstOrDefault();
            if (mdata != null)
            {
                mdata.DateOfReminder = mEntity.DateOfReminder;
                mdata.EquipmentName = mEntity.EquipmentName;
                mdata.MaintenanceTypeID = mEntity.MaintenanceTypeID;
                mdata.ReminderDescription = mEntity.ReminderDescription;
                mdata.RenewalAmount = mEntity.RenewalAmount;
                mdata.RequesterID = mEntity.RequesterID;
                mdata.AssignTo = mEntity.AssignTo;
                mRep.Edit(mdata);
            }
            return mdata;
        }
       
    }
}
