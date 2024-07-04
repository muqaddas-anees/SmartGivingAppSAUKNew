using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using AjaxControlToolkit;
using System.Collections.Specialized;
using Location.DAL;
using Location.Entity;
using ProjectMgt.Entity;
using ProjectMgt.DAL;
using ProgrammeMgt.DAL;
using ProgrammeMgt.Entity;
using IncidentMgt.DAL;
using IncidentMgt.Entity;
using InventoryMgt.DAL;
using System.Web.Script.Services;
using System.Web.Script.Serialization;


/// <summary>
/// Summary description for Timesheet
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class ServiceMgr : System.Web.Services.WebService
{

    [WebMethod]
    public string SaveList(string data,string ucdata)
    {
        JavaScriptSerializer json = new JavaScriptSerializer();
        try
        {
            List<string> strValues = json.Deserialize<List<string>>(data);
            using (UserMgt.DAL.UserDataContext td = new UserMgt.DAL.UserDataContext())
            {
                List<int> getUnCheckedIDS = new List<int>();
                List<UserMgt.Entity.UserSkill> SkillsNew = new List<UserMgt.Entity.UserSkill>();
                //Get existing data
                var DList = td.UserSkills.Select(p => p).ToList();
                foreach (string sval in strValues.Distinct().ToArray())
                {
                    var sids = sval.Split('_');
                    //0 - CategoryID
                    //1- CourseID
                    //2-UserID
                    if (sids.Count() > 2)
                    {
                        var dval = DList.Where(o => o.UserId == int.Parse(sids[2]) && o.CourseID == int.Parse(sids[1])).FirstOrDefault();
                        if (dval == null)
                        {
                            //add record to collection
                            SkillsNew.Add(new UserMgt.Entity.UserSkill()
                            {
                                CategoryID = Convert.ToInt32(sids[0]),
                                CourseID = Convert.ToInt32(sids[1]),
                                UserId = Convert.ToInt32(sids[2]),
                                DateBooked = DateTime.Now
                            });
                        }
                    }
                    //else
                    //{
                    //    getexistingIDS.Add(dval.Id);

                    //}
                   
                }
                List<string> strUnValues = json.Deserialize<List<string>>(ucdata);
                
                foreach (string sval in strUnValues.Distinct().ToArray().Except(strValues).ToArray())
                {
                    var sids = sval.Split('_');
                    if (sids.Count() > 2)
                    {
                        var dval = DList.Where(o => o.UserId == int.Parse(sids[2]) && o.CourseID == int.Parse(sids[1])).FirstOrDefault();
                        if (dval == null)
                        {

                        }
                        else
                        {
                            getUnCheckedIDS.Add(dval.Id);

                        }
                    }
                }
                if (SkillsNew.Count > 0)
                {
                    //delete if anything is uncheked
                    if (getUnCheckedIDS.Count > 0)
                    {
                        var dItems = DList.Where(o => getUnCheckedIDS.ToArray().Contains(o.Id)).ToList();
                        td.UserSkills.DeleteAllOnSubmit(dItems);
                    }
                    td.UserSkills.InsertAllOnSubmit(SkillsNew);
                    td.SubmitChanges();
                    SkillsNew.Clear();
                }
                else
                {
                    if (getUnCheckedIDS.Count > 0)
                    {
                        var dItems = DList.Where(o => getUnCheckedIDS.ToArray().Contains(o.Id)).ToList();
                        td.UserSkills.DeleteAllOnSubmit(dItems);
                    }
                   
                    td.SubmitChanges();
                    SkillsNew.Clear();
                }
            }
           
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            data = string.Empty;
        }
        return "saved";
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetPortfolios(string knownCategoryValues, string category)
    {
       

        return TimesheetresourceSection.Timesheet_GetPortfolios();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetProjectsTitles1(string knownCategoryValues, string category)
    {
        string[] _categoryValues = knownCategoryValues.Split(':', ';');
        int portfolioID = int.Parse(_categoryValues[1]);
        return TimesheetresourceSection.TimeSheet_GetProjectsTitles(portfolioID);
       
    }

    //webmethod for ProjectTitles with Project Prefix and reference to display in TimesheetResourceDaily.aspx -- Giri
    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetProjectsTitle2(string knownCategoryValues, string category)
    {
        string[] _categoryValues = knownCategoryValues.Split(':', ';');
        int portfolioID = int.Parse(_categoryValues[1]);
        return TimesheetresourceSection.TimeSheet_GetProjectsTitlesWithReference(portfolioID);
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetCity(string knownCategoryValues, string category)
    {
        string[] _cityValues = knownCategoryValues.Split(':', ';');
        int countyId = int.Parse(_cityValues[1]);
        return LocationResource.City_CaseCade(countyId);

    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetProjectsTasks(string knownCategoryValues, string category)
    {
        string[] _categoryValues = knownCategoryValues.Split(':', ';');
        int projectRef = int.Parse(_categoryValues[3]);
        return TimesheetresourceSection.TimeSheet_GetProjectsTasks(projectRef);
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetProjectsTasks1(string knownCategoryValues, string category)
    {
        string[] _categoryValues = knownCategoryValues.Split(':', ';');
        int projectRef = int.Parse(_categoryValues[1]);
        return TimesheetresourceSection.TimeSheet_GetProjectsTasks(projectRef);
    }
    
    /// <summary>
    /// add for getting project Tasks for selected Contractor ID
    /// </summary>TimeSheet_GetProjectsTasksbyPortFolio
    /// <param name="knownCategoryValues"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    /// 

    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetProjectsTasksbyPortFolio(string knownCategoryValues, string category)
    {
        string[] _categoryValues = knownCategoryValues.Split(':', ';');
        int projectRef = int.Parse(_categoryValues[1]);
        //int portfolio = int.Parse(_categoryValues[1]);
        return TimesheetresourceSection.TimeSheet_GetProjectsTasksbyPortFolio(projectRef, sessionKeys.UID);
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetProjectsTasks2(string knownCategoryValues, string category)
    {
        string[] _categoryValues = knownCategoryValues.Split(':', ';');
        int projectRef = int.Parse(_categoryValues[1]);
       
        return TimesheetresourceSection.TimeSheet_GetProjectsTasks(projectRef);
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetSites(string knownCategoryValues, string category)
    {
        
        string[] _categoryValues = knownCategoryValues.Split(':', ';');
         int portfolioID = int.Parse(_categoryValues[1]);
         return TimesheetresourceSection.Timesheet_GetSites(portfolioID);
    }

    /// <summary>
    ///  created for getting sites accroding to the Project Referecne
    /// </summary>
    /// <param name="knownCategoryValues"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    [WebMethod]
    public CascadingDropDownNameValue[] GetSitesByProjRef(string knownCategoryValues, string category)
    {
        string[] _categoryValues = knownCategoryValues.Split(':', ';');
        int ProjectRef = int.Parse(_categoryValues[1]);
        return TimesheetresourceSection.Timesheet_GetSitesbyPRef(ProjectRef);
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetEntryTypes(string knownCategoryValues, string category)
    {
      
        
        return TimesheetresourceSection.TimeSheet_GetEntryTypes();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetSitesByProjectRef(string knownCategoryValues, string category)
    {

        string[] _categoryValues = knownCategoryValues.Split(':', ';');
        int projectRef = int.Parse(_categoryValues[1]);
        return TimesheetresourceSection.TimeSheet_GetSitesByProjectRef(projectRef);
    }
   [WebMethod]
    public CascadingDropDownNameValue[] GetAllProjectRef(string knownCategoryValues, string category)
    {

        string[] _categoryValues = knownCategoryValues.Split(':', ';');
        int projectRef = int.Parse(_categoryValues[1]);
        return TimesheetresourceSection.TimeSheet_GetAllProjectRef(projectRef);
    }
   [WebMethod(EnableSession = true)]
   public CascadingDropDownNameValue[] GetSite1(string knownCategoryValues, string category)
   {
       //string[] _catgoryValue = knownCategoryValues.Split(':', ';');
       //string CusomerId = (_catgoryValue[1]);
       int projectRef = sessionKeys.Project;
       using (InventoryDataContext db = new InventoryDataContext())
       {
           using (projectTaskDataContext pd = new projectTaskDataContext())
           {
               int portfolioId = pd.Projects.Where(p => p.ProjectReference == projectRef).Select(p => Convert.ToInt32(p.Portfolio)).FirstOrDefault();
               var result = (from s in db.Sites
                             join a in db.AssignedSitesToPortfolios
                                 on s.ID equals a.SiteID
                             where a.Portfolio == portfolioId
                             orderby s.Site1
                             select new CascadingDropDownNameValue { value = s.ID.ToString(), name = s.Site1 }).ToArray();

               return result;
           }
       }






   }
   [WebMethod(EnableSession = true)]
   public CascadingDropDownNameValue[] GetCategory1(string knownCategoryValues, string category)
   {
       //string[] _catgoryValue = knownCategoryValues.Split(':', ';');
       //string CusomerId = (_catgoryValue[1]);

       int projectRef = sessionKeys.Project;

       using (InventoryDataContext db = new InventoryDataContext())
       {
           using (projectTaskDataContext pd = new projectTaskDataContext())
           {

               int portfolioId = pd.Projects.Where(p => p.ProjectReference == projectRef).Select(p => Convert.ToInt32(p.Portfolio)).FirstOrDefault();

               var result = (from s in db.ServiceCatalog_categories
                             where s.PortfolioID == portfolioId && s.Type == 0 && s.PageType == 1
                             orderby s.CategoryName
                             select new CascadingDropDownNameValue { value = s.ID.ToString(), name = s.CategoryName }).ToArray();

               return result;

           }
       }

   }
   [System.Web.Services.WebMethod(EnableSession = true)]
   [System.Web.Script.Services.ScriptMethod]
   public CascadingDropDownNameValue[] GetSubCategory1(string knownCategoryValues, string category)
   {
       string[] _catgoryValue = knownCategoryValues.Split(':', ';');
       string categoryId = (_catgoryValue[1]);

       using (InventoryDataContext db = new InventoryDataContext())
       {
           using (projectTaskDataContext pd = new projectTaskDataContext())
           {
               int projectRef = sessionKeys.Project;
               int portfolioId = pd.Projects.Where(p => p.ProjectReference == projectRef).Select(p => Convert.ToInt32(p.Portfolio)).FirstOrDefault();
               var result = (from s in db.ServiceCatalog_categories
                             where s.MasterID == int.Parse(categoryId) && s.PortfolioID == portfolioId && s.Type == 1
                             orderby s.CategoryName
                             select new CascadingDropDownNameValue { value = s.ID.ToString(), name = s.CategoryName }).ToArray();

               return result;
           }
       }

   }
    //[WebMethod]
    //public CascadingDropDownNameValue[] GetAllProjectRef(string knownCategoryValues, string category)
    //{
    //    return TimesheetresourceSection.TimeSheet_GetAllProjectRef();
    //}

    [WebMethod (EnableSession = true)]
    public CascadingDropDownNameValue[] GetProjectTaskByResource(string knownCategoryValues, string category)
    {
        string[] _categoryValues = knownCategoryValues.Split(':', ';');
        int projectRef = int.Parse(_categoryValues[1]);
        return TimesheetresourceSection.ProjectTasksByResource_CaseCade(projectRef);
        
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetSubProgramme(string knownCategoryValues, string category)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        int programmeID = int.Parse(_catgoryValue[1]);

        ProgrammeDataContext programme = new ProgrammeDataContext();
        var subprogramme = (from r in programme.OperationsOwners
                            where r.MasterProgramme == programmeID && r.Level == 2
                            orderby r.OperationsOwners
                            select new CascadingDropDownNameValue{ value=r.ID.ToString(), name=r.OperationsOwners }
                          ).ToArray();
        return subprogramme;
    }
     [WebMethod]
    public CascadingDropDownNameValue[] GetSitesByPortfolio(string knownCategoryValues, string category)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        int PortfolioID = int.Parse(_catgoryValue[1]);

        using (LocationDataContext loc = new LocationDataContext())
        {
            var sites = (from r in loc.Sites
                         join p in loc.AssignedSitesToPortfolios on r.ID equals p.SiteID
                         where p.Portfolio == PortfolioID
                         orderby r.Site1
                         select new CascadingDropDownNameValue { value = r.ID.ToString(), name = r.Site1 }
                              ).ToArray();
            return sites;
        }
    }
    #region vendor Category

    [WebMethod]
    public CascadingDropDownNameValue[] GetVendorCategory(string knownCategoryValues, string category)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        int vendorID = int.Parse(_catgoryValue[1]);

        projectTaskDataContext pt = new projectTaskDataContext();
        var vendorCategories = (from r in pt.ServiceCatalog_categories
                                where r.VendorID >0 && r.VendorID == vendorID && r.MasterID == 0
                            orderby r.CategoryName
                            select new CascadingDropDownNameValue { value = r.ID.ToString(), name = r.CategoryName }
                          ).ToArray();
        return vendorCategories;
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetVendorSubCategory(string knownCategoryValues, string category)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        int masterID = int.Parse(_catgoryValue[3]);

        projectTaskDataContext pt = new projectTaskDataContext();
        var vendorCategories = (from r in pt.ServiceCatalog_categories
                                where r.VendorID > 0 && r.MasterID == masterID 
                                orderby r.CategoryName
                                select new CascadingDropDownNameValue { value = r.ID.ToString(), name = r.CategoryName }
                          ).ToArray();
        return vendorCategories;
    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetProjectSubCategory(string knownCategoryValues)
    {
        
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        int masterID=int.Parse(_catgoryValue[1]);



        using (PortfolioDataContext obj = new PortfolioDataContext())
        {
            var SubCategory = (from p in obj.ProjectCategories
                               where p.MasterID == masterID
                               orderby p.CategoryName
                               select new CascadingDropDownNameValue { value = p.CategoryID.ToString(), name = p.CategoryName }
                             ).ToArray();

            return SubCategory;
        }

    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetProjectMasterCategory(string knownCategoryValues)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        using (PortfolioDataContext obj = new PortfolioDataContext())
        {
            var Category = (from p in obj.Projectcategory_Masers
                               //where p.PortfolioID == masterID
                               orderby p.CategoryName
                               select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.CategoryName }
                             ).ToArray();

            return Category;
        }

    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetSubCategory(string knownCategoryValues)
    {

        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        int masterID = int.Parse(_catgoryValue[1]);



        PortfolioDataContext obj = new PortfolioDataContext();
        //var SubCategory = (from p in obj.ProjectCategories
        //                   where p.MasterID == masterID
        //                   orderby p.CategoryName
        //                   select new CascadingDropDownNameValue { value = p.CategoryID.ToString(), name = p.CategoryName }
        //                 ).ToArray();
        var SubCategory = (from p in obj.ServiceCatalog_categories
                           where p.MasterID == masterID
                           orderby p.CategoryName
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.CategoryName }
                        ).ToArray();
        return SubCategory;

    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetTypeOfHours(string knownCategoryValues, string contextKey)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');


        string PortfolioId = contextKey.ToString();


        string srtype = (_catgoryValue[1]);

        IncidentDataContext obj = new IncidentDataContext();
        
        var ddlhoursresult = (from p in obj.Incident_UnitConsumptionConfigurations
                              where  p.SRType == srtype &&p.PortfolioID==int.Parse(PortfolioId)
                              orderby p.TypeOfHours
                              select new CascadingDropDownNameValue { value = p.TypeOfHours, name = p.TypeOfHours }
                             ).ToArray();


        return ddlhoursresult;
    }

    #endregion 

 #region SD team
    [WebMethod]
    public CascadingDropDownNameValue[] GetAreaByPortfolio(string knownCategoryValues, string category)
    {
        string[] _areaid = knownCategoryValues.Split(':', ';');
        int areaid = int.Parse(_areaid[1]);

        PortfolioDataContext pdt = new PortfolioDataContext();
        var subprogramme = (from r in pdt.SDteams
                            where r.AreaID == areaid
                            orderby r.TeamName
                            select new CascadingDropDownNameValue { value = r.ID.ToString(), name = r.TeamName }
                          ).ToArray();
        pdt.Dispose();
        return subprogramme;
    }

#endregion


    [WebMethod]
    public CascadingDropDownNameValue[] GetPortfolio_Active(string knownCategoryValues, string category)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        // int PortfolioID = int.Parse(_catgoryValue[1]);

        using (PortfolioDataContext loc = new PortfolioDataContext())
        {
            var portfolios = (from r in loc.ProjectPortfolios
                              where r.Visible == true
                              orderby r.PortFolio
                              select new CascadingDropDownNameValue { value = r.ID.ToString(), name = r.PortFolio }
                              ).ToArray();
            return portfolios;
        }
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetAssetType(string knownCategoryValues, string category)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        

        using (AssetsMgr.DAL.AssetsToSoftwareDataContext loc = new AssetsMgr.DAL.AssetsToSoftwareDataContext())
        {
            var atype = (from r in loc.Assets_Type()
                              orderby r.Type
                              select new CascadingDropDownNameValue { value = r.TypeID.ToString(), name = r.Type }
                              ).ToArray();
            return atype;
        }
    }
}

