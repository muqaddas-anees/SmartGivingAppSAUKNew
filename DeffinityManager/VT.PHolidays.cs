using System;
using System.Collections.Generic;
using System.Web;
/// <summary>
/// Summary description for VT
/// </summary>
namespace VT.PHEntity
{
    public class VTPHolidays
    {
        public int ID
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public bool AnnualReoccurence
        {
            get;
            set;
        }

        //public VTPHolidays()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}
    }

    public class VTPHolidaysCollection : List<VTPHolidays>
    {

    }
}