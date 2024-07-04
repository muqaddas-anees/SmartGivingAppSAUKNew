using System;
using System.Collections.Generic;
using System.Web;
using System.Collections.Generic;
/// <summary>
/// Summary description for VT
/// </summary>
namespace VT.Entity
{
    public class LeaveRequest
    {
        public LeaveRequest()
        {

        }

        public int ID { get; set; }
        public int AbsenseType { get; set; }
        public string AbsenseTypeName { get; set; }
        public DateTime FromDate { get; set; }
        public float FromDatePeriod { get; set; }       
        public DateTime ToDate { get; set; }
        public float ToDatePeriod { get; set; }
        public string ApprovalStatus { get; set; }
        public object Days { get; set; }
        public int RequesterID { get; set; }
        public string RequesterName { get; set; }
        public DateTime RequestedDate { get; set; }
        public string RequestNotes { get; set; }
        public int MemberID { get; set; }
        public int SiteID { get; set; }
        public int TeamType { get; set; }
        public int FromDateMeridian { get; set; }
        public int ToDateMeridian { get; set; }
       
    }

    public class LeaveRequestCollection : List<LeaveRequest>
    { }
}