using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace DeffinityAppDev.App.WebService
{
    /// <summary>
    /// Summary description for EventDetailsData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EventDetailsData : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }



        public class Continent
        {

        }

        [WebMethod]
        public void GetRelegions()
        {


            IPortfolioRepository<PortfolioMgt.Entity.DenominationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationDetail>();


            var ListDenomination = pRep.GetAll();



            //List<Continent> continents = new List<Continent>();

            //foreach (var value in ListDenomination)
            //{
            //    Continent continent = new Continent();
            //    continent.Id = value.ID; ;
            //    continent.Name = value.Name;
            //    continents.Add(continent);

            //}


            //List<Relegion> relegions = new List<Relegion>();

            //foreach (var value in ListDenomination)
            //{
            //    Relegion relegion = new Relegion();
            //    relegion.Id = value.ID; ;
            //    relegion.Name = value.Name;
            //    relegions.Add(relegion);

            //}





            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(""));
        }









        [WebMethod]
        public void GetEventDetails()
        {


            IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();


            var ListDenomination = pRep.GetAll();




            List<EventDetails> eventDetails = new List<EventDetails>();

            foreach (var value in ListDenomination)
            {
                EventDetails eventDetail = new EventDetails();
                eventDetail.title = value.Title;



                DateTime dtstart = value.StartDateTime;
                DateTime dtend = value.EndDateTime;





              







                string strartdate = ConvertToDateTime(dtstart);

                string Enddate = ConvertToDateTime(dtend);






               



                eventDetail.start = "2022-04-09T16:00:00";
                eventDetail.end = "2022-04-19T06:00:00";



                eventDetail.start = strartdate;
                eventDetail.end = Enddate;


                eventDetails.Add(eventDetail);

            }




            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(eventDetails));




            //JavaScriptSerializer js = new JavaScriptSerializer();
            //Context.Response.Write(js.Serialize(eventDetails));
        }









        public string ConvertToDateTime(   DateTime dateTime   )
        {

            DateTime dt = Convert.ToDateTime(dateTime);

            //Know the month

            int month = dt.Month;


            int day = dt.Day;

            int year = dt.Year;

            string time = dt.TimeOfDay.ToString();

            string stMonth;

            string stday;

            stMonth = month + "";

            stday = day + "";

            if (month < 10)
            {
                 stMonth = "0" + month;
            }



            if (day < 10)
            {
                 stday = "0" + day;
            }


            


            string FullDateTime = year + "-" + stMonth + "-" + stday + "T" + time;



            //"2022-04-09T16:00:00";


            return FullDateTime;

        }



    }












    public class EventDetails
    {
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }





}
