using AjaxControlToolkit;
using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using UserMgt.DAL;
using System.Collections.Specialized;

namespace ChangeControlMgt.Srv
{
    /// <summary>
    /// Summary description for CCService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CCService1 : System.Web.Services.WebService
    {

        //public CCService () {

        //    //Uncomment the following line if using designed components 
        //    //InitializeComponent(); 
        //}
      
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] GetNameByCompanyId(string knownCategoryValues, string category)
        {
            string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            string companyId = (_catgoryValue[1]);

            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                //using (UserDataContext ud = new UserDataContext())
                //{
                //    //get the active customer users
                //    var users_Active_Customers = ud.Contractors.Where(p => p.SID == 7 && p.Status.ToLower() == "active").ToList();
                //    var getAsssociatedContacts = (from p in pd.PortfolioContacts
                //                                 join q in pd.PortfolioContactAssociates
                //                                 on p.ID equals q.ContactID
                //                                 where p.PortfolioID == int.Parse(companyId)
                //                                 select new {p.ID,p.Name,q.CustomerUserID}).ToList();

                var result = (from p in pd.PortfolioContacts
                              orderby p.Name
                              where p.PortfolioID == int.Parse(companyId)
                              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
                //var result = (from p in getAsssociatedContacts
                //             join u in users_Active_Customers
                //             on p.CustomerUserID equals u.ID
                //             select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
                return result;
                //}
            }
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] GetAllNames(string knownCategoryValues, string category)
        {
            //string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            //string companyId = (_catgoryValue[1]);

            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                var result = (from p in pd.PortfolioContacts
                              orderby p.Name
                              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

                return result;

            }
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] GetAdminUsers(string knownCategoryValues, string category)
        {
            using (UserDataContext ud = new UserDataContext())
            {
                var result = (from p in ud.Contractors
                              where p.Status.ToLower() == "active" && (p.SID == 1 || p.SID == 2 || p.SID == 3)
                              orderby p.ContractorName
                              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.ContractorName }).ToArray();

                return result;
            }
        }


    }
}