using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;

namespace DeffinityAppDev.WF.test
{
    /// <summary>
    /// Summary description for test
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class test : System.Web.Services.WebService
    {
        [WebMethod]
        public void GetStudents()
        {

            IPortfolioRepository<PortfolioContact> pReporsitory = new PortfolioRepository<PortfolioContact>();

            var contacts = pReporsitory.GetAll();
            //
            
            var js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(contacts));
        }
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
