using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetMgt.Entity;



namespace TimesheetMgt.BAL
{
    public class TimesheetApproverBAL
    {
        public static TimesheetApprover TimesheetApproverBAL_Update(int approverID,int portfolioID)
        {
            ITimesheetRepository<TimesheetApprover> pRep = new TimesheetRepository<TimesheetApprover>();
            var s = pRep.GetAll().Where(o => o.PortfolioID == portfolioID ).FirstOrDefault();
            if (s == null)
            {
                s = new TimesheetApprover();
                s.ApproverID = approverID;
                s.PortfolioID = portfolioID;
                pRep.Add(s);
            }
            else
            {
                s.ApproverID = approverID;
                pRep.Edit(s);
            }
            return s;
        }
       
        public static TimesheetApprover TimesheetApproverBAL_Select(int portfolioID)
        {
            ITimesheetRepository<TimesheetApprover> pRep = new TimesheetRepository<TimesheetApprover>();
            return pRep.GetAll().Where(o => o.PortfolioID == portfolioID).FirstOrDefault();

        }
       
      
    }
}
