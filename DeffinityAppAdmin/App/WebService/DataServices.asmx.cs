using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace DeffinityAppDev.App.WebService
{




    public class Continent
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }



    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ContinentId { get; set; }
    }




    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
    }


    public class Relegion
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RelegionId { get; set; }
    }

   



    public class Denomination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
    }





    /// <summary>
    /// Summary description for DataServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DataServices : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }









        [WebMethod]
        public void GetContinents()
        {
            

            IPortfolioRepository<PortfolioMgt.Entity.DenominationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationDetail>();


            var ListDenomination = pRep.GetAll();

           

            List<Continent> continents = new List<Continent>();

            foreach (var value in ListDenomination)
            {
                Continent continent = new Continent();
                continent.Id = value.ID; ;
                continent.Name = value.Name;
                continents.Add(continent);

            }


            List<Relegion> relegions = new List<Relegion>();

            foreach (var value in ListDenomination)
            {
                Relegion relegion = new Relegion();
                relegion.Id = value.ID; ;
                relegion.Name = value.Name;
                relegions.Add(relegion);

            }





            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(relegions));
        }




        public static IQueryable<PortfolioMgt.Entity.DenominationGroupDetail> DenominationGroupDetailsBAL_Select()
        {
            IPortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail>();
            return pRep.GetAll();
        }


        [WebMethod]
        public void GetCountriesByContinentId(int ContinentId)
        {
            var rlist = DenominationGroupDetailsBAL_Select().Where(o => o.DenominationDetailsID == ContinentId).OrderBy(o => o.Name).ToList();

            List<Country> countries = new List<Country>();


            foreach (var value in rlist)
            {
                Country country = new Country();
                country.Id = value.ID; ;
                country.Name = value.Name;
                countries.Add(country);

            }

            


             List<Group> Groups = new List<Group>();


            foreach (var value in rlist)
            {
                Group Group = new Group();
                Group.Id = value.ID; ;
                Group.Name = value.Name;
                Groups.Add(Group);

            }




          

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Groups));
        }






        [WebMethod]
        public void GetCitiesByCountryId(int GroupId)
        {



            var rlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.DenominationGroupDetailsID == GroupId).OrderBy(o => o.Name).ToList();


            List<Denomination> Denominations = new List<Denomination>();


            foreach (var value in rlist)
            {
                Denomination Denomination = new Denomination();
                Denomination.Id = value.ID; ;
                Denomination.Name = value.Name;
                Denominations.Add(Denomination);

            }



         //   List<City> cities = new List<City>

         //   {
         //       new City { Name = "Banglore", Id = 1, CountryId  = 1 },
         //       new City { Name = "L A", Id = 2, CountryId = 2 },
         //       new City { Name = "Melborn", Id = 3, CountryId = 3 }
         //   };



         //var   Lcities = cities.Where(o => o.CountryId == CountryId).FirstOrDefault();

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Denominations));
        }









        [WebMethod]
        public void GetRelegions()
        {


            IPortfolioRepository<PortfolioMgt.Entity.DenominationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationDetail>();


            var ListDenomination = pRep.GetAll();



            List<Continent> continents = new List<Continent>();

            foreach (var value in ListDenomination)
            {
                Continent continent = new Continent();
                continent.Id = value.ID; ;
                continent.Name = value.Name;
                continents.Add(continent);

            }


            List<Relegion> relegions = new List<Relegion>();

            foreach (var value in ListDenomination)
            {
                Relegion relegion = new Relegion();
                relegion.Id = value.ID; ;
                relegion.Name = value.Name;
                relegions.Add(relegion);

            }





            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(relegions));
        }



        [WebMethod]
        public void GetGroupsByRelegionId(int ContinentId)
        {
            var rlist = DenominationGroupDetailsBAL_Select().Where(o => o.DenominationDetailsID == ContinentId).OrderBy(o => o.Name).ToList();

            List<Country> countries = new List<Country>();


            foreach (var value in rlist)
            {
                Country country = new Country();
                country.Id = value.ID; ;
                country.Name = value.Name;
                countries.Add(country);

            }




            List<Group> Groups = new List<Group>();


            foreach (var value in rlist)
            {
                Group Group = new Group();
                Group.Id = value.ID; ;
                Group.Name = value.Name;
                Groups.Add(Group);

            }






            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Groups));
        }






        [WebMethod]
        public void GetDenominationsByGroupId(int GroupId)
        {



            var rlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.DenominationGroupDetailsID == GroupId).OrderBy(o => o.Name).ToList();


            List<Denomination> Denominations = new List<Denomination>();


            foreach (var value in rlist)
            {
                Denomination Denomination = new Denomination();
                Denomination.Id = value.ID; ;
                Denomination.Name = value.Name;
                Denominations.Add(Denomination);

            }



          

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Denominations));
        }





    }
}
