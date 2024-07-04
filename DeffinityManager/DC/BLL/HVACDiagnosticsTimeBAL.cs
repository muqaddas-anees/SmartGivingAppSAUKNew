using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.BLL
{
    public class HVACDiagnosticsTimeBAL
    {
      

        public static HVACDiagnosticsTime HVACDiagnosticsTimeBAL_Update(HVACDiagnosticsTime cat)
        {
            IDCRespository<HVACDiagnosticsTime> pRep = new DCRepository<HVACDiagnosticsTime>();
            var s = pRep.GetAll().Where(o => o.CallID == cat.CallID).FirstOrDefault();
            if (s != null)
            {
                s.StartTime = cat.StartTime;
                s.StopTime = cat.StopTime;
                s.Status = cat.Status;
                pRep.Edit(s);
            }
            else
            {
                pRep.Add(cat);
            }

            
            return s;
        }
       
        public static List<HVACDiagnosticsTime> HVACDiagnosticsTimeBAL_SelectJobID(int callID)
        {
            IDCRespository<HVACDiagnosticsTime> pRep = new DCRepository<HVACDiagnosticsTime>();
            return pRep.GetAll().Where(o => o.CallID == callID).ToList();

        }
        public static HVACDiagnosticsTime HVACDiagnosticsTimeBAL_Select(int id)
        {
            IDCRespository<HVACDiagnosticsTime> pRep = new DCRepository<HVACDiagnosticsTime>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
    }
}
