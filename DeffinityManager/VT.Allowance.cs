using System;
using System.Collections.Generic;
using System.Web;


/// <summary>
/// Summary description for VT
/// </summary>
namespace VT.Entity
{
    public class Allowance
    {
        public int ID
        {
            get;
            set;
        }
        public int UserID
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public float LeaveAllowance
        {
            get;
            set;
        }

        public float CarriedOver
        {
            get;
            set;
        }
        public int Year
        {
            get;
            set;
        }
        public float Available
        {
            get;
            set;
        }
    }
    public class LeaveDaysEntity
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public double Days { get; set; }
        public string Comments { get; set; }
        public int Year { get; set; }
    }

    public class AllowanceCollection : List<Allowance>
    { 
        
    }
    public class LeaveDaysCollection : List<LeaveDaysEntity>
    {

    }
}