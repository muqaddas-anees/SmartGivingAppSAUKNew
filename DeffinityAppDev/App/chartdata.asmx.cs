using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace DeffinityAppDev.App
{
    /// <summary>
    /// Summary description for chartdata
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class chartdata : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        public List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                allDates.Add(date.Date);
            }

            return allDates;
        }
        [WebMethod(enableSession:true)]
        public List<object> GetPieData(string startdate,string enddate)
        {
            var sdate = Deffinity.Utility.StartDateOfMonth(Convert.ToDateTime(startdate));
            var edate = Deffinity.Utility.EndDateOfMonth(Convert.ToDateTime(enddate)).AddDays(1).AddMinutes(-1);
            var getDates = GetDatesBetween(Deffinity.Utility.StartDateOfMonth(Convert.ToDateTime(sdate)), Convert.ToDateTime(edate.AddDays(0).ToShortDateString()));
            var tList = (from s in PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll()
                         .Where(o => o.PaidDate >= sdate && o.PaidDate <= edate).Where(o => o.OrganizationID == sessionKeys.PortfolioID)
                         .Where(o =>( o.IsPaid.HasValue?o.IsPaid.Value:false) ==  true).ToList()
                         select new
                         {
                             s.CreatedDateTime,
                             s.ID,
                             s.IsPaid,
                             s.LoggedByID,
                             s.ModifiedDateTime,
                             s.OrganizationID,
                             s.PaidAmount,
                             PaidDate = Convert.ToDateTime(s.PaidDate.Value.ToShortDateString()),
                             s.PayRef,
                             s.TithingID,
                             s.RecurringType
                              
                            
                         }).ToList();
            //string query = "SELECT ShipCity, COUNT(orderid) TotalOrders";
            //query += " FROM Orders WHERE ShipCountry = 'USA' GROUP BY ShipCity";
            //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
        "Task", "Hours per Day"
            });


            chartData.Add(new object[]
                           {
                        "One-Time", tList.Where(o=>o.RecurringType == null).Count()

                           });
            chartData.Add(new object[]
                          {
                        "Recurring", tList.Where(o=>o.RecurringType != null).Count()

                          });

            return chartData;
          
        }

        [WebMethod(enableSession: true)]
        public  List<object> GetBarData(string startdate, string enddate)
        {
            //string query = "SELECT ShipCity, COUNT(orderid) TotalOrders";
            //query += " FROM Orders WHERE ShipCountry = 'USA' GROUP BY ShipCity";
            //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
             List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
        "Date", "Amount"
            });

            var getDates = GetDatesBetween(Deffinity.Utility.StartDateOfMonth(Convert.ToDateTime(startdate)), Convert.ToDateTime(enddate).AddDays(1).AddMinutes(-1));
            var tList = (from s in PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID)
                         .Where(o => (o.IsPaid.HasValue ? o.IsPaid.Value : false) == true).ToList()
                         select new
                         {
                             s.CreatedDateTime,
                             s.ID,
                             s.IsPaid,
                             s.LoggedByID,
                             s.ModifiedDateTime,
                             s.OrganizationID,
                             s.PaidAmount,
                             PaidDate =  Convert.ToDateTime( s.PaidDate.Value.ToShortDateString()),
                             s.PayRef,
                             s.TithingID,
                             
                         }).ToList();

            foreach (var d in getDates)
            {

                var dtemp = tList.Where(o => o.PaidDate == d).Sum(o => o.PaidAmount.HasValue ? o.PaidAmount.Value : 0);
                chartData.Add(new object[]
{
                        string.Format("{0} {1}", d.ToString("MMM"), d.Day ), tList.Where(o=> o.PaidDate == d ).Sum(o=>o.PaidAmount.HasValue?o.PaidAmount.Value:0)

                         }) ;
            }

            //chartData.Add(new object[]
            //               {
            //            "One-Time", 8

            //               });
            //chartData.Add(new object[]
            //              {
            //            "Recurring", 2

            //              });

            return chartData;

        }
        [WebMethod(enableSession: true)]
        public  List<object> GeLineData(string startdate, string enddate)
        {
            //string query = "SELECT ShipCity, COUNT(orderid) TotalOrders";
            //query += " FROM Orders WHERE ShipCountry = 'USA' GROUP BY ShipCity";
            //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
        "Date", "Amount"
            });

            var getDates = GetDatesBetween(Deffinity.Utility.StartDateOfMonth(Convert.ToDateTime(startdate)), Convert.ToDateTime(enddate).AddDays(1).AddMinutes(-1));
            var tList = (from s in PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID)
                         .Where(o => (o.IsPaid.HasValue ? o.IsPaid.Value : false) == true).ToList()
                         select new
                         {
                             s.CreatedDateTime,
                             s.ID,
                             s.IsPaid,
                             s.LoggedByID,
                             s.ModifiedDateTime,
                             s.OrganizationID,
                             s.PaidAmount,
                             PaidDate = Convert.ToDateTime(s.PaidDate.Value.ToShortDateString()),
                             s.PayRef,
                             s.TithingID,

                         }).ToList();
            double amt = 0.00;
            foreach (var d in getDates)
            {

                 amt = amt + tList.Where(o => o.PaidDate == d).Sum(o => o.PaidAmount.HasValue ? o.PaidAmount.Value : 0.00);
                chartData.Add(new object[]
{
                        string.Format("{0} {1}", d.ToString("MMM"), d.Day ), amt

                         });
            }

            //chartData.Add(new object[]
            //               {
            //            "One-Time", 8

            //               });
            //chartData.Add(new object[]
            //              {
            //            "Recurring", 2

            //              });

            return chartData;

        }
    }
}
