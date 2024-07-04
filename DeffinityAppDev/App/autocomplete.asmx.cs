using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DeffinityAppDev.App
{
    /// <summary>
    /// Summary description for autocomplete
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class autocomplete : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        private static List<Customer> GetStudent()
        {
            List<Customer> Customers = new List<Customer>()
   {
    new Customer  {Name = "Roman"},
    new Customer  {Name = "Iqbal"},
    new Customer  { Name = "Amin"},
    new Customer  {  Name = "Asad"},
    new Customer  {  Name = "Abul"},
    new Customer  {  Name = "Abaden"},
    new Customer  { Name = "M Ali"},
    new Customer  {  Name = "Ashikur Rhaman"},
    new Customer  {  Name = "Abdul"},
    new Customer  {  Name = "Asif"},
    new Customer  {  Name = "Aminur"},
    new Customer  {  Name = "Arifur Rahman"},
    new Customer  {  Name = "Asgor"},
    new Customer  {  Name = "Abul Momen"}

  };
            return Customers;
        }
        public class Customer
        {

            public string Name { get; set; }

        }

        [WebMethod]
        public  List<string> SearchCustomers(string prefixText, int count)
        {
            List<Customer> CustomeList = GetStudent();
            var query = from m in CustomeList

                        select m.Name.ToString();
            try
            {
                return (from customer in query
                        where customer.ToLower().StartsWith(prefixText.ToLower())
                        select customer).Take(count).ToList<string>();

            }
            catch (Exception ex)
            {

                throw new Exception("Problem Loading in finding customer" + ex.Message);
            }

        }

    }
}
