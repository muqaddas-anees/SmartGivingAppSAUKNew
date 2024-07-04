using System;
using System.Collections.Generic;
using System.Web;
using System.Collections.Generic;

/// <summary>
/// Summary description for VT
/// </summary>
/// 
namespace VT.Entity
{
    public class LeaveApprover
    {
        public LeaveApprover()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int ID { get; set; }
        public int ApproverID { get; set; }
        public string ApproverName { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
    public class LeaveApproverCollection : List<LeaveApprover>
    { 
        
    }
}