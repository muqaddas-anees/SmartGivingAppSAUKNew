using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;


/// <summary>
/// Summary description for QuoteAdminEntity
/// </summary>
/// 
namespace Quote.BLL
{
    public class QuoteAdminEntity
    {
        public int ID { get; set; }
        public int Portfolio { get; set; }
        public string Prefix { get; set; }
        public float VAT { get; set; }
        public string FolderName { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Contactnumber { get; set; }
        public int QuoteStart { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
    }

    public class QuoteItemsEntity
    {
        public int ID { get; set; }
        public int QuoteID { get; set; }
        public int ItemNo { get; set; }
        public string Worksheet { get; set; }
        public string ItemDescription { get; set; }
        public int Category { get; set; }
        public float UnitPrice { get; set; }
        public float Qty { get; set; }
        public float Total { get; set; }
        public string Unit { get; set; }

    }
    public class QuoteAdminCollection : List<QuoteAdminEntity>
    {

    }
    public class QuoteItemCollection : List<QuoteItemsEntity>
    {
    }

}