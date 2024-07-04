using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;
//using IncidentMgt.DAL;
//using IncidentMgt.Entity;


namespace DC.BAL
{
    /// <summary>
    /// Summary description for UnitsExtesionBAL
    /// </summary>
    public class UnitsExtesionBAL :IDisposable
    {
        DCDataContext dContext = null;
        //IncidentDataContext iContext = null;
        public UnitsExtesionBAL()
        {
            dContext = new DCDataContext();
           // iContext = new IncidentDataContext();
        }

        public void UpdateUnits(int Callid, int LoggedUserID)
        {
            CallDetail cd = dContext.CallDetails.Where(o => o.ID == Callid).Select(o=>o).FirstOrDefault();
            //get list of services 
            List<DC.Entity.Incident_Service> slist = dContext.Incident_Services.Where(o => o.IncidentID == Callid).ToList();
            List<Incident_ServiceUnit> ulist = new List<Incident_ServiceUnit>();
           
            int UID = LoggedUserID;
            foreach (DC.Entity.Incident_Service sEnity in slist)
            {
                if (dContext.Incident_ServiceUnits.Where(o => o.CallServiceID == sEnity.ID).Count() == 0)
                {
                    ulist.Add(new Incident_ServiceUnit
                    {
                        SdID = Callid,
                        DateApplied = DateTime.Now,
                        CategoryID = 0,
                        SubCategoryID = 0,
                        QtyUsed = (sEnity.UnitConsumption.HasValue?sEnity.UnitConsumption.Value:0) * (sEnity.QTY.HasValue?sEnity.QTY.Value:0),
                        Notes = string.Empty,
                        AppliedBy = LoggedUserID,
                        SRType = "Service Request",
                        TypeOfHours = string.Empty,
                        SectionType = "Service Request",
                        CallServiceID = sEnity.ID

                    });
                }
            }
            dContext.Incident_ServiceUnits.InsertAllOnSubmit(ulist);
            dContext.SubmitChanges();
            //update unit history
            UpdateUnitsHistory(cd.CompanyID.Value);
        }
        public void UpdateUnitsHistory(int portfolioid)
        {
            // Get all closed Calls
            //6- fls
            //35 - closed status
            var callIds = dContext.CallDetails.Where(o => o.CompanyID == portfolioid && o.RequestTypeID == 6 && o.StatusID == 35 ).Select(o => o.ID).ToArray();
            //Select total Units
            List<DC.Entity.Incident_Service> sList = dContext.Incident_Services.Where(o => callIds.Contains(o.IncidentID.Value)).Select(o => o).ToList();
            //Get total unit consuption
            var total_Units = sList.Select(o => o.UnitConsumption).Sum();

            List<Incident_UnitsPurchaseHistory> uList = dContext.Incident_UnitsPurchaseHistories.Where(o => o.PortfolioID == portfolioid).Select(p => p).Take(1).ToList();
            //update first entry
            foreach (Incident_UnitsPurchaseHistory u in uList)
            { 
                u.QtyUsed = Convert.ToDecimal(total_Units);
                dContext.SubmitChanges();

            }
            //iContext.Incident_UnitsPurchaseHistories
            //iContext.SubmitChanges();
        }
        public void Dispose()
        {
            if (dContext != null)
                dContext = null;
            //if (iContext != null)
            //    iContext = null;
        }
    }
}