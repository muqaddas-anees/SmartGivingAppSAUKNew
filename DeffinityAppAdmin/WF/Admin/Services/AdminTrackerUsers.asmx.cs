using DeffinityManager.PortfolioMgt.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace DeffinityAppDev.WF.Admin.Services
{
    /// <summary>
    /// Summary description for AdminTrackerUsers
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AdminTrackerUsers : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        public object BindTrackerLogin()
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                var userlist = PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_SelectAll();
                var partnerlist = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll();

                var rlist = (from u in userlist
                             join p in partnerlist on u.PartnerID equals p.ID
                             orderby u.DisplayName ascending
                             select new
                             {
                                 ID = u.ID,
                                 Name =u.DisplayName,
                                 UserName = u.Username,
                                 Password = u.Password,
                                 PartnerID = u.PartnerID,
                                 PartnerName = p.PartnerName
                             }).ToList();

                return Jsonserializer.Serialize(rlist).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }
    }
}
