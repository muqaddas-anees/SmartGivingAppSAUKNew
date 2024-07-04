using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF
{
    public partial class testcal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //var sdate = Deffinity.Utility.StartDateOfMonth(DateTime.Now);
                //var edate = Deffinity.Utility.EndDateOfMonth(DateTime.Now);
                var str = GetAm_PM(DateTime.Now);
                DateTime reference = DateTime.Now;
                var calendar = CultureInfo.CurrentCulture.Calendar;

                string s = "11/18/2005 11:23:03 PM";
                s = string.Format("{0} {1}:00 {2}", "02/03/2021", "11:23", "PM");
                DateTimeFormatInfo fi = new CultureInfo("en-US", false).DateTimeFormat;
                DateTime myDate = DateTime.ParseExact(s, "MM/dd/yyyy hh:mm:ss tt", fi);

                var r = myDate.ToShortDateString();
                //IEnumerable<int> daysInMonth = Enumerable.Range(1, calendar.GetDaysInMonth(reference.Year, reference.Month));

                //List<Tuple<DateTime, DateTime>> weeks = daysInMonth.Select(day => new DateTime(reference.Year, reference.Month, day))
                //    .GroupBy(d => calendar.GetWeekOfYear(d, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday))
                //    .Select(g => new Tuple<DateTime, DateTime>(g.First(), g.Last()))
                //    .ToList();

                //weeks.ForEach(x => Response.Write(string.Format("{0:MM/dd/yyyy} - {1:MM/dd/yyyy}", x.Item1, x.Item2)));


                // var list = WeekDatesInAMonth(10, 2020);
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        public DateTime GetDateTime(string d_date,string t_time, string t_am_pm)
        {
            
            string s = string.Format("{0} {1}:00 {2}", d_date, t_time, t_time);
            DateTimeFormatInfo fi = new CultureInfo("en-US", false).DateTimeFormat;
            DateTime myDate = DateTime.ParseExact(s, "MM/dd/yyyy hh:mm:ss tt", fi);

            return myDate;
        }
        public string GetAm_PM(DateTime d_datetime)
        {
            string s = d_datetime.ToString("tt");
            return s;
        }
        public List<DateTime> WeekDatesInAMonth(int month, int year)
        {
            var firstOftargetMonth = new DateTime(year, month, 1);
            var firstOfNextMonth = firstOftargetMonth.AddMonths(1);

            var allDates = new List<DateTime>();

            for (DateTime date = firstOftargetMonth; date < firstOfNextMonth; date = date.AddDays(7))
            {
                allDates.Add(date);
            }

            return allDates;
        }
    }
}