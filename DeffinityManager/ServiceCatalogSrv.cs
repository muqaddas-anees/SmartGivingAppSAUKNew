using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AjaxControlToolkit;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using PortfolioMgt.BAL;
using ProjectMgt.DAL;
using RFI.Entity;
using RFI.DAL;
/// <summary>
/// Summary description for ServiceCatalogSrv
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ServiceCatalogSrv : System.Web.Services.WebService {

    //public ServiceCatalogSrv () {

    //    //Uncomment the following line if using designed components 
    //    //InitializeComponent(); 
    //}

    [WebMethod]
    public CascadingDropDownNameValue[] GetCategoryByAdmin(string knownCategoryValues, string category)
    {

        var x = ServiceCatalog_Admin.ServiceCatalog_category_CategoryByAdmin();
        var result = (from p in x
                      select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.CategoryName }).ToArray();
        return result;

    }
    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetSubCategoryByAdmin(string knownCategoryValues, string category)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        string typeId = (_catgoryValue[1]);

        var x =  ServiceCatalog_Admin.ServiceCatalog_category_SubCategoryByAdmin((int.Parse(typeId)));
        var result = (from p in x
                      select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.CategoryName }).ToArray();
        return result;

    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetRateType(string knownCategoryValues, string category)
    {
        
        List<RateType> r = new List<RateType>();
        using (PortfolioDataContext pd = new PortfolioDataContext())
        {
            r = (from p in pd.RateTypes 
                select p).ToList() ;
            var result = (from p in r
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.RateType1 }).ToArray();
            return result;
        }

    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetVendors(string knownCategoryValues, string category)
    {

        List<VendorDetails> r = new List<VendorDetails>();
        using (RFIDataContext pd = new RFIDataContext())
        {
            r = (from p in pd.VendorDetails
                 select p).ToList();
            var result = (from p in r
                          select new CascadingDropDownNameValue { value = p.VendorID.ToString(), name = p.ContractorName }).ToArray();
            return result;
        }

    }
}
