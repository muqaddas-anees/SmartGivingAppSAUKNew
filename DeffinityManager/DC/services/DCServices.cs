using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DC.BLL;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using PortfolioMgt.BLL;
using UserMgt.DAL;
using UserMgt.Entity;
using Location.DAL;
using Location.Entity;
using AjaxControlToolkit;
using DC.DAL;
using DC.Entity;
using DC.BAL;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Drawing;
using System.Globalization;
using System.Data;
using InventoryMgt.Entity;
using System.Web.UI.WebControls;


namespace DC.SRV
{
/// <summary>
/// Summary description for WebService
/// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {


        [WebMethod(EnableSession = true)]
        public string addNewCustomer(string name, string email, string cell, string address, string city, string state, string zipcode)
        {
            string jsonString = string.Empty;
            int addressid =0;
            try
            {


                try
                {
                    
                    //var cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                    //var uRepository = new UserRepository<UserMgt.Entity.Contractor>();
                    if (!string.IsNullOrEmpty(email.Trim()) && !string.IsNullOrEmpty(name.Trim()))
                    {

                      var result = UserMgt.BAL.UserMgtBAL.AddOrUpdateMembers(email, name, "", cell, address, city, state, zipcode);
                        //if ((cRepository.GetAll().Where(o => o.Email.ToLower() == emails[0] && o.PortfolioID == sessionKeys.PortfolioID ).Count() == 0) &&
                        //     (uRepository.GetAll().Where(o => o.SID != 7 && o.Status=="Active" && o.EmailAddress.ToLower() == emails[0] ).Count() == 0))
                        //if ((cRepository.GetAll().Where(o => o.Email.ToLower() == email && (o.isDisabled.HasValue ? o.isDisabled.Value : false) == false && o.PortfolioID == sessionKeys.PortfolioID).Count() == 0))
                        //{
                        //    var pContact = new PortfolioContact();
                        //    pContact.Name = name;
                        //    pContact.PortfolioID = sessionKeys.PortfolioID;
                        //    pContact.Email = email;
                        //    pContact.Telephone = cell;
                        //    pContact.DateOfBirth = Convert.ToDateTime("01/01/1900");
                        //    pContact.Mobile = cell;
                        //    pContact.Address1 = string.Empty;
                        //    pContact.Town = string.Empty;
                        //    pContact.City = string.Empty;
                        //    pContact.Postcode = string.Empty;
                        //    pContact.DateLogged = DateTime.Now;
                        //    pContact.SourceofLead = "";
                        //    //pContact.Tags = ListBox_getValues();
                        //    //add to contact 
                        //    cRepository.Add(pContact);
                        //    if (pContact != null)
                        //    {


                        //        var contactid = pContact.ID;

                        //        try
                        //        {
                        //            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> pRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                        //            IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> paRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();
                        //            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();

                        //            PortfolioMgt.Entity.PortfolioContactAddress sResult = pRepository.GetAll().Where(o => o.ContactID == contactid).OrderBy(o => o.ID).FirstOrDefault();
                        //            //
                        //            if (sResult == null)
                        //            {
                        //                sResult = new PortfolioMgt.Entity.PortfolioContactAddress();
                        //                sResult.ContactID = contactid;
                        //                sResult.Address = address;
                        //                sResult.Address2 = string.Empty;
                        //                sResult.Amount = 0;
                        //                sResult.City = city;
                        //                sResult.State = state;
                        //                sResult.PostCode = zipcode;
                        //                sResult.LoggedBy = sessionKeys.UID;
                        //                sResult.LoggedDatetime = DateTime.Now;
                        //                pRepository.Add(sResult);
                        //                addressid = sResult.ID;
                        //                //Session["msg"] = "Address details added successfully";
                        //                jsonString = "Added successfully";
                        //            }
                                   
                        //        }
                        //        catch (Exception e)
                        //        {
                        //            LogExceptions.WriteExceptionLog(e);
                        //        }


                        //        // lblmsg.Text = "<p class='bg-success'>Added successfully</p>";
                        //        //Response.Redirect("~/WF/CustomerAdmin/ContactDetails.aspx?ContactID=" + pContact.ID, false);
                        //    }
                        //    else
                        //    {
                        //        //lblmsg.Text = "Cannot insert contact";
                        //    }
                        //}
                        if(!result)
                        {
                            // lblmsg.Text = "<p class='bg-danger'> Email address already exists. please try again </p>";
                            jsonString = "";
                        }
                        else
                        {
                            jsonString = "";
                        }
                    }
                   
                }
                catch (Exception ex)
                { LogExceptions.WriteExceptionLog(ex); }



                //DataTable table = Bind_UsagesInMainGrid(productid, Cid);
                jsonString = JsonConvert.SerializeObject(new { result = jsonString, rid = addressid.ToString() });
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return jsonString;
        }

        [WebMethod]
        public string DataTableToJsonWithJsonNetForUsageGrid(int productid, int Cid)
        {
            string jsonString = string.Empty;
            try
            {
                DataTable table = Bind_UsagesInMainGrid(productid, Cid);
                jsonString = JsonConvert.SerializeObject(table);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return jsonString;
        }

        [WebMethod]
        public DataTable Bind_UsagesInMainGrid(int productid, int Cid)
        {
            DataTable dt = new DataTable();
            try
            {
                IAssetRespository<AssetsMgr.Entity.V_Asset> aRepository = new AssetRespository<AssetsMgr.Entity.V_Asset>();

                var alist = aRepository.GetAll().Where(o => o.ContactAddressID == productid).ToList();


                //InventoryRepository<GridFieldConfigurator> IGFC = new InventoryRepository<GridFieldConfigurator>();
                //InventoryRepository<InventoryManager_PSDProduct> IMPSD = new InventoryRepository<InventoryManager_PSDProduct>();
                //InventoryRepository<InventoryManager_UsageCustomData> IUCD = new InventoryRepository<InventoryManager_UsageCustomData>();

                //var plist = IGFC.GetAll().Where(o => o.CustomerId == Cid).OrderBy(o => o.Position).ToList();

                //var InList = IMPSD.GetAll().Where(a => a.SectionType == "PROJECT" && a.ProductId == productid).ToList();
                //var projectids = InList.Select(o => o.projectid).ToArray();
                //var pRepository = new ProjectRepository<ProjectMgt.Entity.ProjectDetails>();

                //var projectlist = pRepository.GetAll().Where(o => projectids.Contains(o.ProjectReference)).ToList();

                var pCols = new string[] {"_id", "Image", "Product_Type", "Make", "Model", "Serial_Number","Notes"};
                foreach (var p in pCols)
                {
                    dt.Columns.Add(p.ToString(), typeof(string));
                }
               
                DataRow datarw;
                foreach (var d in alist)
                {
                    datarw = dt.NewRow();
                    int i = 0;
                    foreach (var p in pCols)
                    {
                        // datarw[i] = d.Id.ToString();
                        if (p == "_id")
                        {
                            datarw[i] = d.ID.ToString();
                        }
                        else if (p == "Image")
                        {
                            string path = "/WF/UploadData/Assets/ThumbNails/asset_" + d.ID.ToString() + ".png";
                            if (! System.IO.File.Exists(HttpContext.Current.Server.MapPath(path)))
                            {
                                path = "/WF/UploadData/Assets/ThumbNails/asset_0.png?t=" + DateTime.Now.Second.ToString();
                            }

                            datarw[i] = path;
                        }
                        else if (p == "Product_Type")
                        {
                            datarw[i] = d.TypeName != null?d.TypeName.ToString():string.Empty;
                        }
                        else if (p == "Make")
                        {
                            datarw[i] = d.MakeName != null ? d.MakeName.ToString() : string.Empty;
                        }
                        else if (p == "Model")
                        {
                            datarw[i] = d.ModelName != null? d.ModelName.ToString():string.Empty;
                        }
                        else if (p == "Serial_Number")
                        {
                            datarw[i] = d.SerialNo != null ? d.SerialNo.ToString() : string.Empty;
                        }
                        else if (p == "Notes")
                        {
                            datarw[i] = d.FromNotes != null?d.FromNotes.ToString():string.Empty;
                        }
                        
                        i++;
                    }
                    dt.Rows.Add(datarw);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return dt;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object OpenCallforToday()
        {
            DateTime? TodayDate = null;
            DateTime? LastWeekdate = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                using (DCDataContext Dc = new DCDataContext())
                {
                    TodayDate = DateTime.Now;
                    LastWeekdate = TodayDate.Value.AddDays(-6);

                    var List = (from a in Dc.CallDetails
                                join b in Dc.FLSDetails on a.ID equals b.CallID
                                where b.UserID == sessionKeys.UID
                                select a).ToList();
                    var retcls = (from a in List
                                  select new PerDayGraphCls
                                  {
                                      PerdayCount = List.Where(b => b.LoggedDate.Value.Date <= TodayDate.Value.Date && b.LoggedDate.Value.Date >= LastWeekdate.Value.Date).Count(),
                                      TotalCount = List.Count
                                  });

                    if (sessionKeys.PortfolioID > 0)
                    {
                        List = List.Where(a => a.CompanyID == sessionKeys.PortfolioID).ToList();
                        if (List.Count != 0)
                        {
                            retcls = (from a in List
                                      select new PerDayGraphCls
                                      {
                                          PerdayCount = List.Where(b => b.LoggedDate.Value.Date <= TodayDate.Value.Date && b.LoggedDate.Value.Date >= LastWeekdate.Value.Date).Count(),
                                          TotalCount = List.Count
                                      });
                        }
                    }
                    return jsonSerializer.Serialize(retcls).ToString();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object TodayOpenCallWithTime()
        {
            DateTime? FDate = null;
            DateTime? Tdate = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            List<CallForDayWithTime> T_List = new List<CallForDayWithTime>();
            try
            {
                using (DCDataContext Dc = new DCDataContext())
                {
                    FDate = DateTime.Now;
                    Tdate = FDate.Value.AddDays(-6);
                    var List = (from a in Dc.CallDetails
                                join b in Dc.FLSDetails on a.ID equals b.CallID
                                where b.UserID == sessionKeys.UID
                                select a).ToList();
                    List = List.Where(b => b.LoggedDate <= FDate && b.LoggedDate >= Tdate).ToList();

                    if (sessionKeys.PortfolioID > 0)
                    {
                        List = List.Where(a => a.CompanyID == sessionKeys.PortfolioID).ToList();
                    }

                    CallForDayWithTime T_single = null;

                    for (int i = 1; i <= 24; i++)
                    {
                        T_single = new CallForDayWithTime();
                        T_single.Calls_Count = List.Where(a => a.LoggedDate.Value.Hour == i).Count();
                        T_single.d_Time = DateTime.Now.ToString("MMMM dd, yyyy ") + i + ":00:00";
                        T_List.Add(T_single);
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return jsonSerializer.Serialize(T_List).ToString();
        }
        [WebMethod]
        public static string CalculateTimeToResolved(int callid)
        {
            string returnValue = string.Empty;
            using (DC.DAL.DCDataContext dc = new DCDataContext())
            {
                DateTime? newdate = new DateTime();
                //22 - New
                if (dc.CallDetailsJournals.Where(o => o.CallID == callid && o.StatusID == 22).Count() > 0)
                {
                    newdate = dc.CallDetailsJournals.Where(o => o.CallID == callid && o.StatusID == 22).OrderBy(o => o.ModifiedDate).FirstOrDefault().ModifiedDate;
                }
                else
                {
                    newdate = null;
                }
                DateTime? resolveddate = new DateTime();
                //34	Resolved
                if (dc.CallDetailsJournals.Where(o => o.CallID == callid && o.StatusID == 34).Count() > 0)
                {
                    resolveddate = dc.CallDetailsJournals.Where(o => o.CallID == callid && o.StatusID == 34).OrderBy(o => o.ModifiedDate).FirstOrDefault().ModifiedDate;
                }
                else { resolveddate = null; }

                TimeSpan timeAccumulated;
                int day, hour, min;

                if (newdate != null && resolveddate != null)
                {
                    timeAccumulated = (Convert.ToDateTime(resolveddate) - Convert.ToDateTime(newdate));
                    day = timeAccumulated.Days;
                    if (day != 0)
                    {
                        day = day * 24;
                        hour = day + timeAccumulated.Hours;
                    }
                    else
                    {
                        hour = timeAccumulated.Hours;
                    }
                    min = timeAccumulated.Minutes;
                    returnValue = hour.ToString("00") + ":" + min.ToString("00");
                }
                else if (newdate != null)
                {
                    //timeAccumulated = (DateTime.Now - Convert.ToDateTime(newdate));
                    //day = timeAccumulated.Days;
                    //if (day != 0)
                    //{
                    //    day = day * 24;
                    //    hour = day + timeAccumulated.Hours;
                    //}
                    //else
                    //{
                    //    hour = timeAccumulated.Hours;
                    //}
                    //min = timeAccumulated.Minutes;

                    returnValue = "00:00";
                }
            }

            return returnValue;
        }
        [WebMethod]
        public CascadingDropDownNameValue[] GetTypeofRequest(string knownCategoryValues, string category)
        {

            var x = RequestTypeBAL.BindPermit();
            var result = (from p in x
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
            return result;

        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] GetStatusByTypeId(string knownCategoryValues, string category)
        {
            string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            string typeId = (_catgoryValue[1]);

            var x = StatusBAL.BindStatus(int.Parse(typeId));
            x.Add(new Status() { Name = "All Tickets", ID = -99 });
            var result = (from p in x.OrderBy(o => o.Name).ToList()
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
            return result;

        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] GetStatusByFLS(string knownCategoryValues, string category)
        {
            //string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            //string typeId = (_catgoryValue[1]);
            //get fls
            var x = StatusBAL.BindStatus(6);
            var result = (from p in x
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
            return result;

        }
        [WebMethod(EnableSession=true)]
        public CascadingDropDownNameValue[] GetSubject(string knownCategoryValues, string category)
        {
            var x = FLSSubject.Bind().Where(s => s.CustomerID == sessionKeys.PortfolioID).ToList();;
            var result = (from p in x
                          orderby p.SubjectName
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.SubjectName }).ToArray();
            return result;

        }
        [WebMethod(EnableSession=true)]
        public object QuickSearchResult(string stringValue)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string strSearch = stringValue.Trim();
            string[] MemCols = null;
          //  MemCols = strSearch.Split(' ');
            MemCols = strSearch.Split(' ');
            using (UserDataContext udc = new UserDataContext())
            {
                using (PortfolioDataContext pdc = new PortfolioDataContext())
                {
                    using (DCDataContext Dc = new DCDataContext())
                    {
                        var Userslist = udc.Contractors.Where(ac => ac.Status == "Active").ToList();
                        var FLSlist = Dc.FLSDetails.ToList();
                        var clist = Dc.CallDetails.ToList();
                        var CompList = pdc.ProjectPortfolios.ToList();
                        var ReqList = pdc.PortfolioContacts.ToList();
                        var SiteList = Dc.OurSites.Where(s => s.CustomerID == sessionKeys.PortfolioID).ToList();
                        var Glist = (from a in FLSlist
                                     join b in clist on a.CallID equals b.ID
                                     //  join c in CompList on b.CompanyID equals c.ID
                                     //  join d in SiteList on b.CompanyID equals d.CustomerID
                                     where (MemCols.Any(s => 
                                          (a.Details != null && a.Details.Contains(s))//description
                                            ||  (b.LoggedBy != null && Userslist.Where(u=>u.ID==b.LoggedBy).Select(u=>u.ContractorName).Contains(s))//logged by
                                            || (b.RequesterID != null && ReqList.Where(p=>p.ID==b.RequesterID).Select(r=>r.Name).Contains(s))//requesterid
                                            || (b.CompanyID != null && CompList.Where(q => q.ID == b.CompanyID).Select(q => q.PortFolio).Contains(s))//customer
                                            || (a.UserID != null && Userslist.Where(u => u.ID ==a.UserID).Select(u => u.ContractorName).Contains(s))//Technician
                                            || (b.SiteID != null && SiteList.Where(p => p.ID == b.SiteID).Select(p => p.Name).Contains(s))//site
                                            || (a.UserID != null && ReqList.Where(p => p.ID == b.RequesterID).Select(r => r.Name).Contains(s))//email
                                            || (a.CallID!=null && a.CallID.Value.ToString().Contains(s))//callid
                                     ))
                                     select new
                                     {
                                         CallID = a.CallID,
                                         Details = a.Details,
                                         Company = CompList.Where(q => q.ID == b.CompanyID).Select(q => q.PortFolio).FirstOrDefault()
                                     }).ToList();
                        return jsonSerializer.Serialize(Glist).ToString();
                    }
                }
            }
        }
        [WebMethod]
        public CascadingDropDownNameValue[] GetAssignedtoDepartment(string knownCategoryValues, string category)
        {
            var x = FLSDepartment.Bind();
            var result = (from p in x
                          orderby p.DepartmentName
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.DepartmentName }).ToArray();
            return result;

        }

        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetAssignedtoDepartmentByCutomer(string knownCategoryValues, string category)
        {
            var x = FLSDepartment.Bind().Where(o=>o.CustomerID ==  sessionKeys.PortfolioID).ToList();
            var result = (from p in x
                          orderby p.DepartmentName
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.DepartmentName }).ToArray();
            return result;

        }


        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetCompany(string knownCategoryValues, string category)
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                var result = (from p in pd.ProjectPortfolios
                              where (p.Visible.HasValue ? p.Visible.Value : false) == true
                              orderby p.PortFolio
                              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.PortFolio }).ToArray();
                
                //if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
                //{
                //    var userResult = (from s in pd.SDteams
                //                      join t in pd.SDteamToUsers
                //                          on s.ID equals t.SDteamID
                //                      where t.UserID == sessionKeys.UID
                //                      select s.PortfolioID).Distinct().ToList();
                //    result = (from p in result
                //              where userResult.Contains(Convert.ToInt32(p.value))
                //              orderby p.name
                //              select new CascadingDropDownNameValue { value = p.value.ToString(), name = p.name }).ToArray();
                   
                //}
                return result;
            }

        }
        [WebMethod(EnableSession = true)]
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
                              where p.PortfolioID == Convert.ToInt32(companyId)
                              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
                    //var result = (from p in getAsssociatedContacts
                    //             join u in users_Active_Customers
                    //             on p.CustomerUserID equals u.ID
                    //             select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
                    return result;
                //}
            }
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] GetNameByCompanySession(string knownCategoryValues, string category)
        {

            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                var result = (from p in pd.PortfolioContacts
                              orderby p.Name
                              where p.PortfolioID == sessionKeys.PortfolioID
                              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
                return result;
            }
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] GetKeyContactName(string knownCategoryValues, string category, string contextKey)
        {

            string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            var contactID = 0;
            var rlist = PortfolioMgt.BAL.CustomerKeyContactsBAL.CustomerKeyContact_SelectAll(contactID);
                var result = (from p in rlist
                              orderby p.Name
                              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
            return result;

        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> GetContacts()
        {

            var rlist = PortfolioMgt.BAL.PortfolioContactsBAL.PorfolioContact_SelectAll();
            var result = (from p in rlist
                          orderby p.Name
                          select new ListItem { Value = p.ID.ToString(), Text = p.Name }).ToList();
            return result;

        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> GetContactsAddress(string id)
        {


            var contactID = Convert.ToInt32(id);
            var rlist = PortfolioMgt.BAL.PortfolioContactAddressBAL.PorfolioContact_Address_SelectAll(Convert.ToInt32(id));
            var result = (from p in rlist
                          orderby p.BillingName
                          select new ListItem { Value = p.ID.ToString(), Text = string.Format("{0} - {1} - {2} - {3}",p.Address +" " +p.Address ,p.City,p.State,p.PostCode) }).ToList();
            return result;

        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> GetKeyContactName1(string id)
        {

            
            var contactID = Convert.ToInt32(id);
            var rlist = PortfolioMgt.BAL.CustomerKeyContactsBAL.CustomerKeyContact_SelectAll(contactID);
            var result = (from p in rlist
                          orderby p.Name
                          select new ListItem { Value = p.ID.ToString(), Text = p.Name }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> GetEquipments(string id)
        {
            var result = new List<ListItem>();
            try
            {
                var _addressID = Convert.ToInt32(!string.IsNullOrEmpty(id) ? id : "0");
                //var contactID = Convert.ToInt32(id);
                using (AssetsMgr.DAL.AssetsToSoftwareDataContext rlist = new AssetsMgr.DAL.AssetsToSoftwareDataContext())
                {
                    result = (from p in rlist.V_Assets
                              where p.ContactAddressID == _addressID
                              orderby p.ID
                              select new ListItem { Value = p.ID.ToString(), Text = string.Format("{0} - {1} - {2}", p.TypeName, p.MakeName, p.ModelName) }).ToList();

                    if (result.Count == 0)
                        result.Add(new ListItem("No equipment assigned", "0"));
                    else
                    {
                        result.Add(new ListItem(" Please select...", "0"));
                    }

                }

                //IAssetRespository<AssetsMgr.Entity.V_Asset> aRes = new AssetRespository<AssetsMgr.Entity.V_Asset>();
                //var rlist = aRes.GetAll().Where(o => o.ContactAddressID == _addressID).ToList();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return result.OrderBy(o => o.Text).ToList();

        }

        //
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public string GetEquipmentsByID(string id)
        {
            var result = string.Empty;
            try
            {
                var _ID = Convert.ToInt32(!string.IsNullOrEmpty(id) ? id : "0");
                //var contactID = Convert.ToInt32(id);
                using (AssetsMgr.DAL.AssetsToSoftwareDataContext rlist = new AssetsMgr.DAL.AssetsToSoftwareDataContext())
                {
                    var e = rlist.V_Assets.Where(o => o.ID == _ID).FirstOrDefault();
                    if (e != null)
                        result = e.ExpDate.HasValue ? e.ExpDate.Value.ToShortDateString() : string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return result;

        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] GetTownByCompanySession(string knownCategoryValues, string category)
        {

            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                var result = (from p in pd.PortfolioContacts
                              where p.PortfolioID == sessionKeys.PortfolioID && p.Town != null
                              orderby p.Town 
                              select new CascadingDropDownNameValue { value = p.Town, name = p.Town }).ToArray();
                return result;
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] GetPostcodeByCompanySession(string knownCategoryValues, string category)
        {

            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                var result = (from p in pd.PortfolioContacts
                              orderby p.Name
                              where p.PortfolioID == sessionKeys.PortfolioID && p.Postcode != null
                              orderby p.Postcode 
                              select new CascadingDropDownNameValue { value = p.Postcode, name = p.Postcode }).ToArray();
                return result;
            }
        }
        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetAssignedtoName(string knownCategoryValues, string category)
        {
            using (UserDataContext ud = new UserDataContext())
            {
                using (DCDataContext Ddc = new DCDataContext())
                {
                    string[] _catgoryValue = knownCategoryValues.Split(':', ';');
                    string DeptId = (_catgoryValue[1]);
                    CascadingDropDownNameValue CDdlRecord = new CascadingDropDownNameValue();
                    List<CascadingDropDownNameValue> CDdlRecordList = new List<CascadingDropDownNameValue>();
                    if (int.Parse(DeptId) > 0)
                    {
                        var DepartmentUsersList = Ddc.DepartmentUsers.Where(a => a.DeptId == int.Parse(DeptId) && a.CustomerID == sessionKeys.PortfolioID).ToList();
                        foreach (var Record in DepartmentUsersList)
                        {
                            var Userslist = ud.Contractors.ToList();
                            if (Record.UserIds != string.Empty)
                            {
                                string[] userIds = Record.UserIds.Split(',');
                                for (int i = 0; i <= userIds.Length - 1; i++)
                                {
                                    CDdlRecord = new CascadingDropDownNameValue();
                                    CDdlRecord.value = userIds[i].ToString();
                                    CDdlRecord.name = Userslist.Where(a => a.ID == int.Parse(userIds[i].ToString())).FirstOrDefault().ContractorName;
                                    CDdlRecordList.Add(CDdlRecord);
                                }
                            }
                        }
                    }
                    //int[] ids = { 1, 2, 3, 4, 9 };
                    //var result = (from p in ud.Contractors
                    //              where p.Status.ToLower() == "active" && ids.Contains(Convert.ToInt32(p.SID))
                    //              orderby p.ContractorName
                    //              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.ContractorName }).ToArray();
                    return CDdlRecordList.ToArray();
                }
            }
        }


        [WebMethod]
        public UserMgt.Entity.Contractor GetContractorDetails(int id)
        {
            using (UserDataContext ud = new UserDataContext())
            {
                var result = (from p in ud.Contractors
                              where p.ID == id
                              select p).FirstOrDefault();

                return result;
            }
        }
   
        [WebMethod(EnableSession=true)]
        public CascadingDropDownNameValue[] GetOurSite(string knownCategoryValues, string category)
        {

            var x = OurSiteBAL.BindOurSite().Where(s=>(s.CustomerID.HasValue?s.CustomerID.Value:0)==sessionKeys.PortfolioID).ToList();
            var result = (from p in x
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
            return result;

        }
        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetOurSiteByCustomerID(string knownCategoryValues, string category)
        {
            string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            string CustomerID = (_catgoryValue[1]);
            var x = OurSiteBAL.BindOurSite().Where(s => s.CustomerID == Convert.ToInt32(CustomerID)).ToList();
            var result = (from p in x
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
            return result;

        }
        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetOurSite_withoutCustomer(string knownCategoryValues, string category)
        {

            var x = OurSiteBAL.BindOurSite().Where(s => (s.CustomerID.HasValue?s.CustomerID.Value:0)==0).ToList();
            var result = (from p in x
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
            return result;

        }
        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetSite(string knownCategoryValues, string category)
        {
            using (LocationDataContext ld = new LocationDataContext())
            {
                var result = (from p in ld.Sites
                              join o in ld.AssignedSitesToPortfolios
                              on p.ID equals o.SiteID
                              where o.Portfolio == sessionKeys.PortfolioID && p.Visible.ToString().ToLower() == "y"
                              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Site1 }).ToArray();


                return result;
            }
        }
        [WebMethod]
        public CascadingDropDownNameValue[] GetSiteByCusomerId(string knownCategoryValues, string category)
        {
            string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            string CusomerId = (_catgoryValue[1]);
            using (LocationDataContext ld = new LocationDataContext())
            {
                var result = (from p in ld.Sites
                              join o in ld.AssignedSitesToPortfolios
                              on p.ID equals o.SiteID
                              where o.Portfolio == int.Parse(CusomerId) && p.Visible.ToString().ToLower() == "y"
                              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Site1 }).ToArray();


                return result;
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]  
        public string GetReqTelNo(int ID)
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                var result = (from p in pd.PortfolioContacts
                              where p.ID == ID
                              select p.Telephone).FirstOrDefault();
                return result;
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]  
        public string GetReqEmail(int ID)
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                var result = (from p in pd.PortfolioContacts
                              where p.ID == ID
                              select p.Email).FirstOrDefault();
                return result;
            }

        }
        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetArea(string knownCategoryValues, string category)
        {

            using (PortfolioDataContext pdt = new PortfolioDataContext())
            {
                var result = (from p in pdt.incident_Areas
                              where p.Portfolio == sessionKeys.PortfolioID
                              orderby p.Name ascending
                              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
                return result;
            }
        }
        [WebMethod]
        public CascadingDropDownNameValue[] GetAreaByCustomerId(string knownCategoryValues, string category)
        {
            string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            string CusomerId = (_catgoryValue[1]);

            using (PortfolioDataContext pdt = new PortfolioDataContext())
            {
                var result = (from p in pdt.incident_Areas
                              where p.Portfolio == int.Parse(CusomerId)
                              orderby p.Name ascending
                              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
                return result;
            }
        }

        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetDeliveryType(string knownCategoryValues, string category)
        {
            var x = DeliveryTypeBAL.BindTypes();
            var result = (from p in x
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
            return result;
        }
        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetCondition(string knownCategoryValues, string category)
        {
            var x = ConditionBAL.BindConditions();
            var result = (from p in x
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
            return result;
        }
        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetStorageLocation(string knownCategoryValues, string category)
        {
            var x = StorageLocationBAL.BindLocation();
            var result = (from p in x
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
            return result;
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public DeliveryDefault GetDeliveryDefaults()
        {
            using (DCDataContext dd = new DCDataContext())
            {
                DeliveryDefault result = dd.DeliveryDefaults.Select(d => d).FirstOrDefault();
                return result;
            }
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string GetDeliveryDefaults_Notice()
        {
            string retval = string.Empty;
            using (DCDataContext dd = new DCDataContext())
            {
                DeliveryDefault result = dd.DeliveryDefaults.Select(d => d).FirstOrDefault();
                if(result != null)
                retval= System.Web.HttpUtility.HtmlDecode(result.HeavyItemNotice);
            }

            return retval;
        }
        [WebMethod]
        public List<string> GetData(string date, int days,int nob, string charges)
        {
          List<string> valuelist = new List<string>();     
            if (Convert.ToDateTime(date) < DateTime.Now)
            {
                decimal totalcost = 0.00M;
               valuelist.Add(Convert.ToDateTime(date).AddDays(days).ToString(Deffinity.systemdefaults.GetDateformat()));
               TimeSpan t = DateTime.Now.Date - Convert.ToDateTime(date);
               valuelist.Add(t.Days.ToString());
                if(days < t.Days)               
                   totalcost  = (t.Days - days) * nob * Convert.ToDecimal(charges);

                valuelist.Add(string.Format("{0:F2}", totalcost));
            }
            return valuelist;
        }
        [WebMethod]
        public string CalculateStorageCharges(string from, string to, string charges)
        {
             decimal totalCharges = 0.00M;
            TimeSpan t = Convert.ToDateTime(to) - Convert.ToDateTime(from);
            int days = t.Days;
            decimal charge =Convert.ToDecimal(charges);
            if(days > 0)
                totalCharges = days * charge;            
            return string.Format("{0:F2}", totalCharges);

        }
        [WebMethod]
        public static string CalculateTimeAccumulated(int callid)
        {
            FLSTimeDetail newDate = FLSTimeDetailsBAL.SelectFLSTimeDetailsByID(callid, "New");
            FLSTimeDetail closedDate = FLSTimeDetailsBAL.SelectFLSTimeDetailsByID(callid, "Resolved");
            TimeSpan timeAccumulated;
            int day, hour, min;
            string returnValue = string.Empty;

            if (newDate != null && closedDate != null)
            {
                timeAccumulated = (Convert.ToDateTime(closedDate.StatusTime) - Convert.ToDateTime(newDate.StatusTime));
                day = timeAccumulated.Days;
                if (day != 0)
                {
                    day = day * 24;
                    hour = day + timeAccumulated.Hours;
                }
                else
                {
                    hour = timeAccumulated.Hours;
                }
                min = timeAccumulated.Minutes;
                returnValue = hour.ToString("00") + ":" + min.ToString("00");
            }
            else if (newDate != null)
            {
                timeAccumulated = (DateTime.Now - Convert.ToDateTime(newDate.StatusTime));
                day = timeAccumulated.Days;
                if (day != 0)
                {
                    day = day * 24;
                    hour = day + timeAccumulated.Hours;
                }
                else
                {
                    hour = timeAccumulated.Hours;
                }
                min = timeAccumulated.Minutes;

                returnValue = hour.ToString("00") + ":" + min.ToString("00");
            }
            return returnValue;
        }
        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetAreaByCompanyId(string knownCategoryValues, string category)
        {
            string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            string companyId = (_catgoryValue[1]);

            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                var result = (from p in pd.incident_Areas
                              orderby p.Name
                              where p.Portfolio == int.Parse(companyId)                            
                              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

                return result;
            }
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetVisitinPurpose(string knownCategoryValues, string category)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                var result = (from p in dc.PurposeToVisits

                              select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

                return result;
            }
        }
        [WebMethod]
        public CascadingDropDownNameValue[] GetLocation(string knownCategoryValues, string category)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                var result = (from s in dc.StorageLocations

                              select new CascadingDropDownNameValue { value = s.ID.ToString(), name = s.Name }).ToArray();

                return result;
            }
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public void AddCallDetailsJournal(CallDetailsJournal cdJournal)
        {
            CallDetailsJournalBAL.AddCallDetailsJournal(cdJournal);
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public void AddDeliveryInformationJournal(DeliveryInformationJournal diJournal)
        {
            DeliveryInformationJournalBAL.AddDeliveryInformationJournal(diJournal);
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public void AddReceivedInformationJournal(RecievedInformationJournal riJournal)
        {
            ReceivedInformationJournalBAL.AddReceivedInformation(riJournal);
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public void AddFLSDetailsJournal(FLSDetailsJournal fdJournal)
        {
            FLSDetailsJournalBAL.AddFLSDetailsJournal(fdJournal);
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public void AddServicePriceJournal(ServicePriceJournal sdJournal)
        {
            ServicePriceJournalBAL.AddServicePriceJournal(sdJournal);
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public void InsertAccessControlJournal(AccessControlJournal acJournal)
        {
            AccessControlJournalBAL.InsertAccessControlJournal(acJournal);
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public void AddPermitToWorkJournal(PermitToWorkJournal pwJournal)
        {
            PermitToWorkJournalBAL.AddPermitToWorkJournalJournal(pwJournal);
        }
         [WebMethod]
        public PortfolioContact GetContactDetails(int rid)
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                var result =pd.PortfolioContacts.Where(p=>p.ID == rid).Select(d => d).FirstOrDefault();
                return result;
            }
        }
         
       
         [WebMethod]
         public string GetCompanyDetails(int cmid)
         {
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 var result = pd.ProjectPortfolios.Where(p => p.ID == cmid).Select(d => d.PortFolio).FirstOrDefault();
                 return result;
             }
         }
         [WebMethod]
         public List<string> GetSiteDetailsbyId(int sid)
         {             
             using (LocationDataContext ld = new LocationDataContext())
             {
                 List<string> reslist = new List<string>();
                 var result = ld.Sites.Where(s => s.ID == sid).Select(s => s).FirstOrDefault();
                 reslist.Add(result.Site1);
                 reslist.Add(result.Address1);
                  reslist.Add(result.Address2);
                  reslist.Add(result.Address3);
                  reslist.Add(result.Postcode);
                 return reslist;
             }
         }
         [WebMethod(EnableSession = true)]
         [System.Web.Script.Services.ScriptMethod]
         public CascadingDropDownNameValue[] GetStatusByRequestTypeId(string knownCategoryValues, string category)
         {
             if (string.IsNullOrEmpty(category))
                 category = "1";
             var x = StatusBAL.BindStatus(Convert.ToInt32(category));   //  1 = Delivery 
             var result = (from p in x
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
             return result;

         }
         [WebMethod(EnableSession = true)]
         [System.Web.Script.Services.ScriptMethod]
         public CascadingDropDownNameValue[] GetStatus(string knownCategoryValues, string category)
         {
             var x = StatusBAL.BindStatus(3);   //  3 = Access control 
             var result = (from p in x
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
             return result;

         }
         [WebMethod(EnableSession = true)]
         public CascadingDropDownNameValue[] GetPermitType(string knownCategoryValues, string category)
         {
             var x = PermitTypeBAL.BindPermitTypes();
             var result = (from p in x
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Type }).ToArray();
             return result;
         }
         [WebMethod(EnableSession = true)]
         public CascadingDropDownNameValue[] GetChecklists(string knownCategoryValues, string category)
         {
             using (projectTaskDataContext pd = new projectTaskDataContext())
             {
                 var result = (from pc in pd.MasterTemplates
                                  where (pc.ChecklistType == 7)                                 
                               select new CascadingDropDownNameValue { value = pc.ID.ToString(), name =pc.Description }).ToArray();
                 return result;
             }           
         }
         [WebMethod]
         public List<string> GetItemDescription(int mtid)
         {
             using (projectTaskDataContext pd = new projectTaskDataContext())
             {
                 List<string> strlist = pd.MasterTemplateItems.Where(m => m.MasterTemplateID == mtid).Select(m => m.ItemDescription).ToList();
                 return strlist;
             }
         }
         [WebMethod]
         public string GetAreaById(int id)
         {            
             using (PortfolioDataContext pdt = new PortfolioDataContext())
             {
                 string area = (from p in pdt.incident_Areas
                               where p.ID == id                              
                               select p.Name).FirstOrDefault();
                 return area;
             }
         }
         #region FLS History
         [WebMethod(EnableSession=true)]
         public List<History> BindFLSHistory(int cid)
         {
             using (UserDataContext ud = new UserDataContext())
             {
                 using (DCDataContext DDc = new DCDataContext())
                 {
                     List<Category> CalList = DDc.Categories.ToList();
                     List<SubCategory> subCatList = DDc.SubCategories.ToList();
                     List<TypeOfRequest> TypeofReq = DDc.TypeOfRequests.ToList();
                     List<FLSSourceOfRequest> FlsSourceReq = DDc.FLSSourceOfRequests.ToList();
                     List<PriorityLevel> PriorityList = DDc.PriorityLevels.ToList();


                     History h;
                     List<Subject> subList = FLSSubject.Bind();
                     List<AssignedDepartment> deptList = FLSDepartment.Bind();
                     deptList.Add(new AssignedDepartment { ID = 0, DepartmentName = "" });
                     List<UserMgt.Entity.Contractor> cList = (from p in ud.Contractors select p).ToList();
                     cList.Add(new UserMgt.Entity.Contractor { ID = 0, ContractorName = "" });
                     List<History> hList = new List<History>();
                     hList = GetCallDetailsHistory(cid, 6);
                     List<FLSDetailsJournal> lstflsj = new List<FLSDetailsJournal>();
                     List<ServicePriceJournal> lstService_price = new List<ServicePriceJournal>();
                     //if(sessionKeys.SID == 7)
                     //    lstflsj = FLSDetailsJournalBAL.SelectFLSDetailsJournal_CustomerVisible_byCallID(cid);
                     //else
                     lstflsj = FLSDetailsJournalBAL.SelectFLSDetailsJournalbyCallID(cid);
                     lstService_price = ServicePriceJournalBAL.SelectServicePriceJournalbyCallID(cid);
                     //First time FLS History Binding
                     if (lstflsj.Count > 0)
                     {
                         FLSDetailsJournal flsj = lstflsj[0];
                         if (sessionKeys.SID == 7 && flsj.VisibleToCustomer == true)
                             hList.AddRange(FLS_history_firstrecord(subList, deptList, cList, flsj));
                         else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                             hList.AddRange(FLS_history_firstrecord(subList, deptList, cList, flsj));
                     }

                     if (lstflsj.Count > 1)
                     {

                         for (int i = 0; i < (lstflsj.Count) - 1; i++)
                         {

                             FLSDetailsJournal flsj1 = lstflsj[i + 1];
                             FLSDetailsJournal flsj2 = lstflsj[i];
                             if (sessionKeys.SID == 7 && flsj1.VisibleToCustomer == true)
                                 hList.AddRange(FLS_history_list(subList, deptList, cList, flsj1, flsj2, CalList, subCatList, FlsSourceReq, TypeofReq, PriorityList));
                             else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                                 hList.AddRange(FLS_history_list(subList, deptList, cList, flsj1, flsj2, CalList, subCatList, FlsSourceReq, TypeofReq, PriorityList));

                         }



                     }
                     //Add first record
                     if (lstService_price.Count > 0)
                     {
                         ServicePriceJournal spj = lstService_price[0];
                         if (sessionKeys.SID == 7 && spj.VisibleToCustomer == true)
                             hList.AddRange(SPJ_history_firstrecord(subList, deptList, cList, spj));
                         else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                             hList.AddRange(SPJ_history_firstrecord(subList, deptList, cList, spj));
                     }
                     //Add service price journal
                     if (lstService_price.Count > 1)
                     {
                         for (int i = 0; i < (lstService_price.Count) - 1; i++)
                         {

                             ServicePriceJournal spj1 = lstService_price[i + 1];
                             ServicePriceJournal spj2 = lstService_price[i];
                             if (sessionKeys.SID == 7 && spj1.VisibleToCustomer == true)
                                 hList.AddRange(SPJ_history_list(subList, deptList, cList, spj1, spj2));
                             else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                                 hList.AddRange(SPJ_history_list(subList, deptList, cList, spj1, spj2));

                         }
                     }
                     hList = hList.OrderByDescending(hi => Convert.ToDateTime(hi.ModifiedDate)).ToList();
                     return hList;
                 }
             }
         }
         private static IEnumerable<History> SPJ_history_firstrecord(List<Subject> subList, List<AssignedDepartment> deptList, List<UserMgt.Entity.Contractor> cList, ServicePriceJournal spj)
         {
             List<History> hList = new List<History>();
             History h = null;
             string fls_user = cList.Where(c => c.ID == spj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
             if (spj.DiscountPercent != null)
             {
                 h = new History();
                 h.FieldName = "Notes:";
                 h.FieldValue = spj.Notes;
                 h.ModifiedDate = spj.ModifiedDate;
                 h.ModifiedBy = fls_user;
                 h.VisibleToCustomer = spj.VisibleToCustomer;
                 hList.Add(h);
             }

             return hList;
         }
         private static IEnumerable<History> SPJ_history_list(List<Subject> subList, List<AssignedDepartment> deptList, List<UserMgt.Entity.Contractor> cList, ServicePriceJournal flsj1, ServicePriceJournal flsj2)
         {
             List<History> hList = new List<History>();
             History h = null;
             string fu_user = cList.Where(c => c.ID == flsj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
             if (flsj1.Notes != flsj2.Notes)
             {
                 h = new History();
                 h.FieldName = "Notes:";
                 h.FieldValue = flsj1.Notes;
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList;
         }
         private static IEnumerable<History> FLS_history_list(List<Subject> subList, List<AssignedDepartment> deptList,
             List<UserMgt.Entity.Contractor> cList, FLSDetailsJournal flsj1, FLSDetailsJournal flsj2,List<Category> CatList,
             List<SubCategory> SubCatList,List<FLSSourceOfRequest> SourceOfReqList,List<TypeOfRequest> TypeOfreq,List<PriorityLevel> Prioritylist)
         {
             List<History> hList = new List<History>();
             History h=null;
             string fu_user = cList.Where(c => c.ID == flsj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
             if (flsj1.SubjectID != flsj2.SubjectID)
             {
                 h = new History();
                 h.FieldName = "Subject:";
                 h.FieldValue = subList.Where(d => d.ID == flsj1.SubjectID).Select(d => d.SubjectName).FirstOrDefault();
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }


             if (flsj1.SubCategoryID != flsj2.SubCategoryID)
             {
                 h = new History();
                 h.FieldName = "SubCategory";
                 h.FieldValue = SubCatList.Where(a => a.ID == flsj1.SubCategoryID.Value).Select(a => a.Name).FirstOrDefault();
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.CategoryID != flsj2.CategoryID)
             {
                 h = new History();
                 h.FieldName = "Category:";
                 h.FieldValue = CatList.Where(a => a.ID == flsj1.CategoryID.Value).Select(a => a.Name).FirstOrDefault();
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.SourceOfRequestID != flsj2.SourceOfRequestID)
             {
                 h = new History();
                 h.FieldName = "Source Of Request:";
                 h.FieldValue = SourceOfReqList.Where(a => a.ID == flsj1.SourceOfRequestID.Value).Select(a => a.Name).FirstOrDefault();
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.RequestType != flsj2.RequestType)
             {
                 h = new History();
                 h.FieldName = "Request Type";
                 h.FieldValue = TypeOfreq.Where(a => a.ID == flsj1.RequestType.Value).Select(a => a.Name).FirstOrDefault();
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.PriorityId != flsj2.PriorityId)
             {
                 h = new History();
                 h.FieldName = "Priority:";
                 h.FieldValue = Prioritylist.Where(a => a.Id == flsj1.PriorityId.Value).Select(a => a.Value).FirstOrDefault();
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.Sitedetails != flsj2.Sitedetails)
             {
                 h = new History();
                 h.FieldName = "Site details:";
                 h.FieldValue = flsj1.Sitedetails;
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.CustomerReference != flsj2.CustomerReference)
             {
                 h = new History();
                 h.FieldName = "Customer Reference:";
                 h.FieldValue = flsj1.CustomerReference;
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.CustomerCostCode != flsj2.CustomerCostCode)
             {
                 h = new History();
                 h.FieldName = "Customer Cost Code:";
                 h.FieldValue = flsj1.CustomerCostCode;
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }



             if (flsj1.Details != flsj2.Details)
             {
                 h = new History();
                 h.FieldName = "Details:";
                 h.FieldValue = flsj1.Details;
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.ScheduledDate != flsj2.ScheduledDate)
             {
                 h = new History();
                 h.FieldName = "Preferred Date/Time:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), flsj1.ScheduledDate);
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.Preferreddate2 != flsj2.Preferreddate2)
             {
                 h = new History();
                 h.FieldName = "Preferred Date/Time2:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), flsj1.Preferreddate2);
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.Preferreddate3 != flsj2.Preferreddate3)
             {
                 h = new History();
                 h.FieldName = "Preferred Date/Time3:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), flsj1.Preferreddate3);
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.DepartmentID != flsj2.DepartmentID)
             {
                 h = new History();
                 h.FieldName = "Assigned to Department:";
                 h.FieldValue = deptList.Where(d => d.ID == flsj1.DepartmentID).Select(d => d.DepartmentName).FirstOrDefault();
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.UserID != flsj2.UserID)
             {
                 h = new History();
                 h.FieldName = "Assigned Technician:";
                 h.FieldValue = cList.Where(d => d.ID == flsj1.UserID).Select(d => d.ContractorName).FirstOrDefault();
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.TimeWorked != flsj2.TimeWorked)
             {
                 h = new History();
                 h.FieldName = "Time Worked:";
                 h.FieldValue = flsj1.TimeWorked;
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.Notes != flsj2.Notes)
             {
                 h = new History();
                 h.FieldName = "Notes:";
                 h.FieldValue = flsj1.Notes;
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.Resolution != flsj2.Resolution)
             {
                 h = new History();
                 h.FieldName = "Resolution:";
                 h.FieldValue = flsj1.Resolution;
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.RAGStatus != flsj2.RAGStatus)
             {
                 h = new History();
                 h.FieldName = "RAG Status:";
                 h.FieldValue = flsj1.RAGStatus;
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj1.POnumber != flsj2.POnumber)
             {
                 h = new History();
                 h.FieldName = "PO Number:";
                 h.FieldValue = flsj1.POnumber;
                 h.ModifiedDate = flsj1.ModifiedDate;
                 h.ModifiedBy = fu_user;
                 h.VisibleToCustomer = flsj1.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList;
         }

         private static IEnumerable<History> FLS_history_firstrecord(List<Subject> subList, List<AssignedDepartment> deptList, List<UserMgt.Entity.Contractor> cList, FLSDetailsJournal flsj)
         {
             List<History> hList = new List<History>();
             History h= null;
             string fls_user = cList.Where(c => c.ID == flsj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
             if (flsj.SubjectID != null && flsj.SubjectID != 0)
             {
                 h = new History();
                 h.FieldName = "Subject:";
                 h.FieldValue = subList.Where(d => d.ID == flsj.SubjectID).Select(d => d.SubjectName).FirstOrDefault();
                 h.ModifiedDate = flsj.ModifiedDate;
                 h.ModifiedBy = fls_user;
                 h.VisibleToCustomer = flsj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj.Details != null )
             {
                 h = new History();
                 h.FieldName = "Details:";
                 h.FieldValue = flsj.Details;
                 h.ModifiedDate = flsj.ModifiedDate;
                 h.ModifiedBy = fls_user;
                 h.VisibleToCustomer = flsj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj.ScheduledDate != null)
             {
                 h = new History();
                 h.FieldName = "Preferred Date/Time:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), flsj.ScheduledDate);
                 h.ModifiedDate = flsj.ModifiedDate;
                 h.ModifiedBy = fls_user;
                 h.VisibleToCustomer = flsj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj.Preferreddate2 != null)
             {
                 h = new History();
                 h.FieldName = "Preferred Date/Time2:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), flsj.Preferreddate2);
                 h.ModifiedDate = flsj.ModifiedDate;
                 h.ModifiedBy = fls_user;
                 h.VisibleToCustomer = flsj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj.Preferreddate3 != null)
             {
                 h = new History();
                 h.FieldName = "Preferred Date/Time3:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), flsj.Preferreddate3);
                 h.ModifiedDate = flsj.ModifiedDate;
                 h.ModifiedBy = fls_user;
                 h.VisibleToCustomer = flsj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj.DepartmentID != null && flsj.DepartmentID != 0)
             {
                 h = new History();
                 h.FieldName = "Assigned to Department:";
                 h.FieldValue = deptList.Where(d => d.ID == flsj.DepartmentID).Select(d => d.DepartmentName).FirstOrDefault();
                 h.ModifiedDate = flsj.ModifiedDate;
                 h.ModifiedBy = fls_user;
                 h.VisibleToCustomer = flsj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj.UserID != null &&flsj.UserID !=0 )
             {
                 h = new History();
                 h.FieldName = "Assigned Technician:";
                 h.FieldValue = cList.Where(d => d.ID == flsj.UserID).Select(d => d.ContractorName).FirstOrDefault();
                 h.ModifiedDate = flsj.ModifiedDate;
                 h.ModifiedBy = fls_user;
                 h.VisibleToCustomer = flsj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj.TimeWorked != null)
             {
                 h = new History();
                 h.FieldName = "Time Worked:";
                 h.FieldValue = flsj.TimeWorked;
                 h.ModifiedDate = flsj.ModifiedDate;
                 h.ModifiedBy = fls_user;
                 h.VisibleToCustomer = flsj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj.Notes != null)
             {
                 h = new History();
                 h.FieldName = "Notes:";
                 h.FieldValue = flsj.Notes;
                 h.ModifiedDate = flsj.ModifiedDate;
                 h.ModifiedBy = fls_user;
                 h.VisibleToCustomer = flsj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj.Resolution != null)
             {
                 h = new History();
                 h.FieldName = "Resolution:";
                 h.FieldValue = flsj.Resolution;
                 h.ModifiedDate = flsj.ModifiedDate;
                 h.ModifiedBy = fls_user;
                 h.VisibleToCustomer = flsj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj.RAGStatus != null)
             {
                 h = new History();
                 h.FieldName = "RAG Status:";
                 h.FieldValue = flsj.RAGStatus;
                 h.ModifiedDate = flsj.ModifiedDate;
                 h.ModifiedBy = fls_user;
                 h.VisibleToCustomer = flsj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (flsj.POnumber != null)
             {
                 h = new History();
                 h.FieldName = "PO Number:";
                 h.FieldValue = flsj.POnumber;
                 h.ModifiedDate = flsj.ModifiedDate;
                 h.ModifiedBy = fls_user;
                 h.VisibleToCustomer = flsj.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList;
         }
        #endregion

         #region Delivery History
         [WebMethod(EnableSession=true)]
         public List<History> BindDeliveryHistory(int cid)
         {
             using (UserDataContext ud = new UserDataContext())
             {
                 History h;
                 List<DeliveryType> dtList = DeliveryTypeBAL.BindTypes();
                 List<Condition> cnList = ConditionBAL.BindConditions();
                 List<UserMgt.Entity.Contractor> cList = (from p in ud.Contractors select p).ToList();
                 List<StorageLocation> slList = StorageLocationBAL.BindLocation();
                 List<History> hList = new List<History>();
                 hList = GetCallDetailsHistory(cid, 1);
                 List<OurSite> ostList = OurSiteBAL.BindOurSite();
                 List<DeliveryInformationJournal> lstdij = new List<DeliveryInformationJournal>();
                 //check user type is customer 
                 //if(sessionKeys.SID==7)
                 //  lstdij=  DeliveryInformationJournalBAL.SelectDeliveryInformationJournal_CustomerVisible_byCallID(cid);
                 //else
                 lstdij = DeliveryInformationJournalBAL.SelectDeliveryInformationJournalbyCallID(cid);
                 List<DeliveryItemWeight> item_weight = DeliveryItemWeightBAL.DeliveryItemWeightBAL_Select();
                 if (lstdij.Count > 0)
                 {
                     // First time history binding

                     DeliveryInformationJournal dij = lstdij[0];
                     if (sessionKeys.SID == 7 && dij.VisibleToCustomer == true)
                         hList.AddRange(Delivery_history_firstrecord(dtList, cList, dij, item_weight));
                     else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                         hList.AddRange(Delivery_history_firstrecord(dtList, cList, dij, item_weight));

                 }
                 if (lstdij.Count > 1)
                 {
                     for (int i = 0; i < (lstdij.Count) - 1; i++)
                     {

                         DeliveryInformationJournal dij1 = lstdij[i + 1];
                         DeliveryInformationJournal dij2 = lstdij[i];
                         if (sessionKeys.SID == 7 && dij1.VisibleToCustomer == true)
                             hList.AddRange(Delivery_history_list(dtList, cList, dij1, dij2, item_weight));
                         else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                             hList.AddRange(Delivery_history_list(dtList, cList, dij1, dij2, item_weight));

                     }
                 }

                 List<RecievedInformationJournal> lstrij = ReceivedInformationJournalBAL.SelectReceivedInformationJournalbyCallID(cid);
                 //add first recored
                 if (lstrij.Count > 0)
                 {

                     RecievedInformationJournal rij1 = lstrij[0];
                     if (sessionKeys.SID == 7 && rij1.VisibleToCustomer == true)
                         hList.AddRange(Received_history_first_record(cnList, cList, slList, ostList, rij1));
                     if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                         hList.AddRange(Received_history_first_record(cnList, cList, slList, ostList, rij1));

                 }
                 if (lstrij.Count > 1)
                 {
                     for (int i = 0; i < (lstrij.Count) - 1; i++)
                     {
                         RecievedInformationJournal rij1 = lstrij[i + 1];
                         RecievedInformationJournal rij2 = lstrij[i];
                         if (sessionKeys.SID == 7 && rij1.VisibleToCustomer == true)
                             hList.AddRange(Received_history_list(cnList, cList, slList, ostList, rij1, rij2));
                         if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                             hList.AddRange(Received_history_list(cnList, cList, slList, ostList, rij1, rij2));
                     }
                 }
                 hList = hList.OrderByDescending(hi => Convert.ToDateTime(hi.ModifiedDate)).ToList();
                 return hList;
             }
         }
         private static IEnumerable<History> Received_history_first_record(List<Condition> cnList, List<UserMgt.Entity.Contractor> cList, List<StorageLocation> slList, List<OurSite> ostList, RecievedInformationJournal rij1)
         {
             List<History> hList = new List<History>();
             History h;
             string mr_username = cList.Where(c => c.ID == rij1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
             if (rij1.ConditionID != null)
             {
                 h = new History();
                 h.FieldName = "Condition:";
                 h.FieldValue = cnList.Where(p => p.ID == rij1.ConditionID).Select(p => p.Name).FirstOrDefault();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (rij1.NumofBoxesRec != null)
             {
                 h = new History();
                 h.FieldName = "Number of Boxes Received:";
                 h.FieldValue = rij1.NumofBoxesRec.ToString();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (rij1.StorageLocationID != null)
             {
                 h = new History();
                 h.FieldName = "Storage Location:";
                 h.FieldValue = slList.Where(s => s.ID == rij1.StorageLocationID).Select(s => s.Name).FirstOrDefault();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }

             if (rij1.DateRecieved != null)
             {
                 h = new History();
                 h.FieldName = "Date Recieved:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(rij1.DateRecieved).Replace("00:00:00", " "));
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);

                 h = new History();
                 h.FieldName = "Days in Storage:";
                 h.FieldValue = rij1.DaysInStore.ToString();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);

                 h = new History();
                 h.FieldName = "Chargeable Date:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(rij1.ChargeableDate).Replace("00:00:00", " "));
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);

                 h = new History();
                 h.FieldName = "Total Cost:";
                 h.FieldValue = rij1.TotalCost.ToString();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (rij1.StoragePeriodFrom != null)
             {
                 h = new History();
                 h.FieldName = "Storage Period From:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(rij1.StoragePeriodFrom).Replace("00:00:00", " "));
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (rij1.StoragePeriodTo != null)
             {
                 h = new History();
                 h.FieldName = "Storage Period To:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(rij1.StoragePeriodTo).Replace("00:00:00", " "));
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (rij1.PeriodCost != null)
             {
                 h = new History();
                 h.FieldName = "Period Cost:";
                 h.FieldValue = rij1.PeriodCost.ToString();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (rij1.OurSiteID != null)
             {
                 h = new History();
                 h.FieldName = "Site:";
                 h.FieldValue = ostList.Where(o => o.ID == rij1.OurSiteID).Select(o => o.Name).FirstOrDefault();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList;
         }
         private static IEnumerable<History> Received_history_list(List<Condition> cnList, List<UserMgt.Entity.Contractor> cList, List<StorageLocation> slList, List<OurSite> ostList, RecievedInformationJournal rij1, RecievedInformationJournal rij2)
         {
             List<History> hList = new List<History>();
             History h;
             string mr_username = cList.Where(c => c.ID == rij1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
             if (rij1.ConditionID != rij2.ConditionID)
             {
                 h = new History();
                 h.FieldName = "Condition:";
                 h.FieldValue = cnList.Where(p => p.ID == rij1.ConditionID).Select(p => p.Name).FirstOrDefault();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (rij1.NumofBoxesRec != rij2.NumofBoxesRec)
             {
                 h = new History();
                 h.FieldName = "Number of Boxes Received:";
                 h.FieldValue = rij1.NumofBoxesRec.ToString();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (rij1.StorageLocationID != rij2.StorageLocationID)
             {
                 h = new History();
                 h.FieldName = "Storage Location:";
                 h.FieldValue = slList.Where(s => s.ID == rij1.StorageLocationID).Select(s => s.Name).FirstOrDefault();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }

             if (rij1.DateRecieved != rij2.DateRecieved)
             {
                 h = new History();
                 h.FieldName = "Date Recieved:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(rij1.DateRecieved).Replace("00:00:00", " "));
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);

                 h = new History();
                 h.FieldName = "Days in Storage:";
                 h.FieldValue = rij1.DaysInStore.ToString();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);

                 h = new History();
                 h.FieldName = "Chargeable Date:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(rij1.ChargeableDate).Replace("00:00:00", " "));
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);

                 h = new History();
                 h.FieldName = "Total Cost:";
                 h.FieldValue = rij1.TotalCost.ToString();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (rij1.StoragePeriodFrom != rij2.StoragePeriodFrom)
             {
                 h = new History();
                 h.FieldName = "Storage Period From:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(rij1.StoragePeriodFrom).Replace("00:00:00", " "));
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (rij1.StoragePeriodTo != rij2.StoragePeriodTo)
             {
                 h = new History();
                 h.FieldName = "Storage Period To:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(rij1.StoragePeriodTo).Replace("00:00:00", " "));
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (rij1.PeriodCost != rij2.PeriodCost)
             {
                 h = new History();
                 h.FieldName = "Period Cost:";
                 h.FieldValue = rij1.PeriodCost.ToString();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (rij1.OurSiteID != rij2.OurSiteID)
             {
                 h = new History();
                 h.FieldName = "Site:";
                 h.FieldValue = ostList.Where(o => o.ID == rij1.OurSiteID).Select(o => o.Name).FirstOrDefault();
                 h.ModifiedDate = rij1.ModifiedDate;
                 h.ModifiedBy = mr_username;
                 h.VisibleToCustomer = rij1.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList;
         }

         private static IEnumerable<History> Delivery_history_list(List<DeliveryType> dtList, List<UserMgt.Entity.Contractor> cList,  DeliveryInformationJournal dij1, DeliveryInformationJournal dij2,List<DeliveryItemWeight> item_weight)
         {
             List<History> hList = new List<History>();
             History h= null;
             string md_user = cList.Where(c => c.ID == dij1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
             if (dij1.ArrivalDate != dij2.ArrivalDate)
             {
                 h = new History();
                 h.FieldName = "Arrival Date:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(dij1.ArrivalDate).Replace("00:00:00", " "));
                 h.ModifiedDate = dij1.ModifiedDate;
                 h.ModifiedBy = md_user;
                 h.VisibleToCustomer = dij1.VisibleToCustomer;
                 hList.Add(h);
             }
             //if (dij1.Weight != dij2.Weight)
             //{
             //    h = new History();
             //    h.FieldName = "Weight:";
             //    h.FieldValue = dij1.Weight;
             //    h.ModifiedDate = dij1.ModifiedDate;
             //    h.ModifiedBy = md_user;
             //    h.VisibleToCustomer = dij1.VisibleToCustomer;
             //    hList.Add(h);
             //}
             if (dij1.NumofBoxes != dij2.NumofBoxes)
             {
                 h = new History();
                 h.FieldName = "Number of Boxes:";
                 h.FieldValue = dij1.NumofBoxes.ToString();
                 h.ModifiedDate = dij1.ModifiedDate;
                 h.ModifiedBy = md_user;
                 h.VisibleToCustomer = dij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (dij1.DeliveryTypeID != dij2.DeliveryTypeID)
             {
                 h = new History();
                 h.FieldName = "Delivery Type:";
                 h.FieldValue = dtList.Where(d => d.ID == dij1.DeliveryTypeID).Select(d => d.Name).FirstOrDefault();
                 h.ModifiedDate = dij1.ModifiedDate;
                 h.ModifiedBy = md_user;
                 h.VisibleToCustomer = dij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (dij1.Description != dij2.Description)
             {
                 h = new History();
                 h.FieldName = "Description:";
                 h.FieldValue = dij1.Description;
                 h.ModifiedDate = dij1.ModifiedDate;
                 h.ModifiedBy = md_user;
                 h.VisibleToCustomer = dij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (dij1.Pallet != dij2.Pallet)
             {
                 string pallet = "No";
                 h = new History();
                 h.FieldName = "Over 1 Pallet:";
                 if (dij1.Pallet == true)
                     pallet = "yes";
                 h.FieldValue = pallet;
                 h.ModifiedDate = dij1.ModifiedDate;
                 h.ModifiedBy = md_user;
                 h.VisibleToCustomer = dij1.VisibleToCustomer;
                 hList.Add(h);
             }
             //if (dij1.OverWeight != dij2.OverWeight)
             //{
             //    string OverWeight = "No";
             //    h = new History();
             //    h.FieldName = "Over Weight:";
             //    if (dij1.OverWeight == true)
             //        OverWeight = "yes";
             //    h.FieldValue = OverWeight;
             //    h.ModifiedDate = dij1.ModifiedDate;
             //    h.ModifiedBy = md_user;
             //    h.VisibleToCustomer = dij1.VisibleToCustomer;
             //    hList.Add(h);
             //}
             if (dij1.ItemWeight != dij2.ItemWeight)
             {
                 h = new History();
                 h.FieldName = "Weight:";
                 h.FieldValue = item_weight.Where(p => p.ID == dij1.ItemWeight).Select(p => p.Weight_Value).FirstOrDefault();
                 h.ModifiedDate = dij1.ModifiedDate;
                 h.ModifiedBy = md_user;
                 h.VisibleToCustomer = dij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (dij1.CourierNumber != dij2.CourierNumber)
             {
                 h = new History();
                 h.FieldName = "Courier Number:";
                 h.FieldValue = dij1.CourierNumber;
                 h.ModifiedDate = dij1.ModifiedDate;
                 h.ModifiedBy = md_user;
                 h.VisibleToCustomer = dij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (dij1.CourierCompany != dij2.CourierCompany)
             {
                 h = new History();
                 h.FieldName = "Courier Company:";
                 h.FieldValue = dij1.CourierCompany;
                 h.ModifiedDate = dij1.ModifiedDate;
                 h.ModifiedBy = md_user;
                 h.VisibleToCustomer = dij1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (dij1.Notes != dij2.Notes)
             {
                 h = new History();
                 h.FieldName = "Notes:";
                 h.FieldValue = dij1.Notes;
                 h.ModifiedDate = dij1.ModifiedDate;
                 h.ModifiedBy = md_user;
                 h.VisibleToCustomer = dij1.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList;
         }

         private static IEnumerable<History> Delivery_history_firstrecord(List<DeliveryType> dtList, List<UserMgt.Entity.Contractor> cList, DeliveryInformationJournal dij, List<DeliveryItemWeight> item_weight)
         {
             List<History> hList = new List<History>();
             History h= null;
             string md_user1 = cList.Where(c => c.ID == dij.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
             if (dij.ArrivalDate != null)
             {
                 h = new History();
                 h.FieldName = "Arrival Date:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(dij.ArrivalDate).Replace("00:00:00", " "));
                 h.ModifiedDate = dij.ModifiedDate;
                 h.ModifiedBy = md_user1;
                 h.VisibleToCustomer = dij.VisibleToCustomer;
                 hList.Add(h);
             }
             //if (dij.Weight != null)
             //{
             //    h = new History();
             //    h.FieldName = "Weight:";
             //    h.FieldValue = dij.Weight;
             //    h.ModifiedDate = dij.ModifiedDate;
             //    h.ModifiedBy = md_user1;
             //    h.VisibleToCustomer = dij.VisibleToCustomer;
             //    hList.Add(h);
             //}
             if (dij.NumofBoxes != null)
             {
                 h = new History();
                 h.FieldName = "Number of Boxes:";
                 h.FieldValue = dij.NumofBoxes.ToString();
                 h.ModifiedDate = dij.ModifiedDate;
                 h.ModifiedBy = md_user1;
                 h.VisibleToCustomer = dij.VisibleToCustomer;
                 hList.Add(h);
             }
             if (dij.DeliveryTypeID != null)
             {
                 h = new History();
                 h.FieldName = "Delivery Type:";
                 h.FieldValue = dtList.Where(d => d.ID == dij.DeliveryTypeID).Select(d => d.Name).FirstOrDefault();
                 h.ModifiedDate = dij.ModifiedDate;
                 h.ModifiedBy = md_user1;
                 h.VisibleToCustomer = dij.VisibleToCustomer;
                 hList.Add(h);
             }
             if (dij.Description != null)
             {
                 h = new History();
                 h.FieldName = "Description:";
                 h.FieldValue = dij.Description;
                 h.ModifiedDate = dij.ModifiedDate;
                 h.ModifiedBy = md_user1;
                 h.VisibleToCustomer = dij.VisibleToCustomer;
                 hList.Add(h);
             }

             if (dij.Pallet != null)
             {
                 string pallet = "No";
                 h = new History();
                 h.FieldName = "Over 1 Pallet:";
                 if (dij.Pallet == true)
                     pallet = "yes";
                 h.FieldValue = pallet;
                 h.ModifiedDate = dij.ModifiedDate;
                 h.ModifiedBy = md_user1;
                 h.VisibleToCustomer = dij.VisibleToCustomer;
                 hList.Add(h);
             }
             //if (dij.OverWeight != null)
             //{
             //    string OverWeight = "No";
             //    h = new History();
             //    h.FieldName = "Over Weight:";
             //    if (dij.OverWeight == true)
             //        OverWeight = "yes";
             //    h.FieldValue = OverWeight;
             //    h.ModifiedDate = dij.ModifiedDate;
             //    h.ModifiedBy = md_user1;
             //    h.VisibleToCustomer = dij.VisibleToCustomer;
             //    hList.Add(h);
             //}
             if (dij.ItemWeight != null)
             {
                 h = new History();
                 h.FieldName = "Weight:";
                 h.FieldValue = item_weight.Where(p=>p.ID == dij.ItemWeight).Select(p=>p.Weight_Value).FirstOrDefault();
                 h.ModifiedDate = dij.ModifiedDate;
                 h.ModifiedBy = md_user1;
                 h.VisibleToCustomer = dij.VisibleToCustomer;
                 hList.Add(h);
             }
             if (dij.CourierNumber != null)
             {
                 h = new History();
                 h.FieldName = "Courier Number:";
                 h.FieldValue = dij.CourierNumber;
                 h.ModifiedDate = dij.ModifiedDate;
                 h.ModifiedBy = md_user1;
                 h.VisibleToCustomer = dij.VisibleToCustomer;
                 hList.Add(h);
             }
             if (dij.CourierCompany != null)
             {
                 h = new History();
                 h.FieldName = "Courier Company:";
                 h.FieldValue = dij.CourierCompany;
                 h.ModifiedDate = dij.ModifiedDate;
                 h.ModifiedBy = md_user1;
                 h.VisibleToCustomer = dij.VisibleToCustomer;
                 hList.Add(h);
             }
             if (dij.Notes != null)
             {
                 h = new History();
                 h.FieldName = "Notes:";
                 h.FieldValue = dij.Notes;
                 h.ModifiedDate = dij.ModifiedDate;
                 h.ModifiedBy = md_user1;
                 h.VisibleToCustomer = dij.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList;
         }
        #endregion

         #region Access Control History
         [WebMethod(EnableSession=true)]
         public List<History> BindAccessControlHistory(int cid)
         {
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 using (UserDataContext ud = new UserDataContext())
                 {
                     History h= null;
                     List<PurposeToVisit> vpList = VisitingPurpose.BindVisitingPurpose();
                     // List<Condition> cnList = ConditionBAL.BindConditions();
                     List<UserMgt.Entity.Contractor> cList = (from p in ud.Contractors select p).ToList();
                     List<StorageLocation> slList = StorageLocationBAL.BindLocation();
                     List<OurSite> ostList = OurSiteBAL.BindOurSite();
                     List<ProjectPortfolio> ppList = (from p in pd.ProjectPortfolios select p).ToList();
                     List<PortfolioContact> pcLsit = (from p in pd.PortfolioContacts select p).ToList();
                     List<Status> stList = StatusBAL.BindStatus(3);
                     List<History> hList = new List<History>();
                     hList = GetCallDetailsHistoryforAccessControl(cid, 3);// 3 for access control
                     List<AccessControlJournal> acjlist = new List<AccessControlJournal>();
                     //check user type is customer 
                     //if (sessionKeys.SID == 7)
                     //    acjlist = AccessControlJournalBAL.SelectAccessControlJournal_CustomerVisible_byCallID(cid);
                     //else
                     acjlist = AccessControlJournalBAL.SelectAccessControlJournalbyCallID(cid);
                    
                     if (acjlist.Count > 1)
                     {
                         for (int i = 0; i < (acjlist.Count) - 1; i++)
                         {
                             AccessControlJournal acj1 = acjlist[i + 1];
                             AccessControlJournal acj2 = acjlist[i];
                             if (sessionKeys.SID == 7 && acj1.VisibleToCustomer == true)
                             hList.AddRange(AccessControl_history_list(vpList, cList, slList, acj1, acj2));
                             else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                                 hList.AddRange(AccessControl_history_list(vpList, cList, slList, acj1, acj2));


                         }
                     }

                     //List<VisitorsJournal> Visitorlist = VisitorsBAL.SelectVisitorsJournalbyCallID(cid);
                     int[] visitors = VisitorsBAL.SelectVisitorsbyCallID(cid);
                     if (visitors != null)
                     {
                         foreach (int v in visitors)
                         {
                             List<VisitorsJournal> Visitorlist = new List<VisitorsJournal>();

                             //if (sessionKeys.SID == 7)
                             //    Visitorlist = VisitorsBAL.VisitorsJournal_CustomerVisible_selectByIDs(v);
                             //else
                             Visitorlist = VisitorsBAL.VisitorsJournal_selectByIDs(v);

                             if (Visitorlist.Count > 0)
                             {
                                 VisitorsJournal vj1 = Visitorlist[0];
                                 if (sessionKeys.SID == 7 && vj1.VisibleToCustomer == true)
                                     hList.AddRange(Visitor_history_firstrecord(cList, vj1));
                                 else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                                     hList.AddRange(Visitor_history_firstrecord(cList, vj1));
                             }
                             if (Visitorlist.Count > 1)
                             {
                                 for (int i = 0; i < (Visitorlist.Count) - 1; i++)
                                 {
                                     VisitorsJournal vj1 = Visitorlist[i + 1];
                                     VisitorsJournal vj2 = Visitorlist[i];
                                     if (sessionKeys.SID == 7 && vj1.VisibleToCustomer == true)
                                          hList.AddRange(Visitor_history_list(cList, vj1, vj2));
                                     else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                                         hList.AddRange(Visitor_history_list(cList, vj1, vj2));


                                 }
                             }
                         }
                     }

                     AccessControlJournal ac = AccessControlJournalBAL.SelectFirstRecordbyCallID(cid);
                     CallDetailsJournal cd = CallDetailsJournalBAL.SelectByDate(cid);
                     
                     if (ac != null && cd != null)
                     {
                         if (sessionKeys.SID == 7 && cd.VisibleToCustomer == true )
                             hList.AddRange(AccessControl_history_firstrecord(vpList, cList, slList, ostList, ppList, pcLsit, stList, ac, cd));
                         else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                             hList.AddRange(AccessControl_history_firstrecord(vpList, cList, slList, ostList, ppList, pcLsit, stList, ac, cd));


                     }


                     hList = hList.OrderByDescending(hi => Convert.ToDateTime(hi.ModifiedDate)).ToList();
                     return hList;
                 }
             }
         }

         private static List<History> Visitor_history_firstrecord(List<UserMgt.Entity.Contractor> cList, VisitorsJournal vj1)
         {
             List<History> hList = new List<History>();
             History h;
             string vi_user = cList.Where(c => c.ID == vj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
             if (vj1.Name != null)
             {
                 h = new History();
                 h.FieldName = "Name:";
                 h.FieldValue = vj1.Name;
                 h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                 h.ModifiedDate = vj1.ModifiedDate;
                 h.ModifiedBy = vi_user;
                 h.VisibleToCustomer = vj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (vj1.Company != null)
             {
                 h = new History();
                 h.FieldName = "Company:";
                 h.FieldValue = vj1.Company;
                 h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                 h.ModifiedDate = vj1.ModifiedDate;
                 h.ModifiedBy = vi_user;
                 h.VisibleToCustomer = vj1.VisibleToCustomer;
                 hList.Add(h);
             }

             if (vj1.EmailAddress != null)
             {
                 h = new History();
                 h.FieldName = "Email Address:";
                 h.FieldValue = vj1.EmailAddress;
                 h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                 h.ModifiedDate = vj1.ModifiedDate;
                 h.ModifiedBy = vi_user;
                 h.VisibleToCustomer = vj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (vj1.PhoneNumber != null)
             {
                 h = new History();
                 h.FieldName = "Phone Number:";
                 h.FieldValue = vj1.PhoneNumber;
                 h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                 h.ModifiedDate = vj1.ModifiedDate;
                 h.ModifiedBy = vi_user;
                 h.VisibleToCustomer = vj1.VisibleToCustomer;
                 hList.Add(h);
             }

             if (vj1.NoShow == true)
             {
                 h = new History();
                 h.FieldName = "No Show:";
                 h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(vj1.ModifiedDate).Replace("00:00:00", " "));
                 h.ModifiedDate = vj1.ModifiedDate;
                 h.ModifiedBy = vi_user;
                 h.VisibleToCustomer = vj1.VisibleToCustomer;
                 hList.Add(h);
             }

             else
             {

                 if (vj1.ArriveDate != null)
                 {
                     h = new History();
                     h.FieldName = "Arrival Date:";
                     h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                     h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(vj1.ArriveDate).Replace("00:00:00", " "));
                     h.ModifiedDate = vj1.ModifiedDate;
                     h.ModifiedBy = vi_user;
                     h.VisibleToCustomer = vj1.VisibleToCustomer;
                     hList.Add(h);
                 }
                 if (vj1.DepartDate != null)
                 {
                     h = new History();
                     h.FieldName = "Depature Date:";
                     h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                     h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(vj1.DepartDate).Replace("00:00:00", " "));
                     h.ModifiedDate = vj1.ModifiedDate;
                     h.ModifiedBy = vi_user;
                     h.VisibleToCustomer = vj1.VisibleToCustomer;
                     hList.Add(h);
                 }
             }
             if (vj1.PhotoID != null)
             {
                 string photo = "No";
                 h = new History();
                 h.FieldName = "Photo ID:";
                 if (vj1.PhotoID == true)
                     photo = "yes";
                 h.FieldValue = photo;
                 h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                 h.ModifiedDate = vj1.ModifiedDate;
                 h.ModifiedBy = vi_user;
                 h.VisibleToCustomer = vj1.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList;
         }

         private static List<History> Visitor_history_list(List<UserMgt.Entity.Contractor> cList,  VisitorsJournal vj1, VisitorsJournal vj2)
         {
             List<History> hList = new List<History>();
             History h;
             string vi_user = cList.Where(c => c.ID == vj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
             if (vj1.Name != vj2.Name)
             {
                 h = new History();
                 h.FieldName = "Name:";
                 h.FieldValue = vj1.Name;
                 h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                 h.ModifiedDate = vj1.ModifiedDate;
                 h.ModifiedBy = vi_user;
                 h.VisibleToCustomer = vj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (vj1.Company != vj2.Company)
             {
                 h = new History();
                 h.FieldName = "Company:";
                 h.FieldValue = vj1.Company;
                 h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                 h.ModifiedDate = vj1.ModifiedDate;
                 h.ModifiedBy = vi_user;
                 h.VisibleToCustomer = vj1.VisibleToCustomer;
                 hList.Add(h);
             }

             if (vj1.EmailAddress != vj2.EmailAddress)
             {
                 h = new History();
                 h.FieldName = "Email Address:";
                 h.FieldValue = vj1.EmailAddress;
                 h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                 h.ModifiedDate = vj1.ModifiedDate;
                 h.ModifiedBy = vi_user;
                 h.VisibleToCustomer = vj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (vj1.PhoneNumber != vj2.PhoneNumber)
             {
                 h = new History();
                 h.FieldName = "Phone Number:";
                 h.FieldValue = vj1.PhoneNumber;
                 h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                 h.ModifiedDate = vj1.ModifiedDate;
                 h.ModifiedBy = vi_user;
                 h.VisibleToCustomer = vj1.VisibleToCustomer;
                 hList.Add(h);
             }

             if (vj1.NoShow == true)
             {
                 h = new History();
                 h.FieldName = "No Show:";
                 h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(vj1.ModifiedDate).Replace("00:00:00", " "));
                 h.ModifiedDate = vj1.ModifiedDate;
                 h.ModifiedBy = vi_user;
                 h.VisibleToCustomer = vj1.VisibleToCustomer;
                 hList.Add(h);
             }

             else
             {

                 if (vj1.ArriveDate != vj2.ArriveDate)
                 {
                     h = new History();
                     h.FieldName = "Arrival Date:";
                     h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                     h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(vj1.ArriveDate).Replace("00:00:00", " "));
                     h.ModifiedDate = vj1.ModifiedDate;
                     h.ModifiedBy = vi_user;
                     h.VisibleToCustomer = vj1.VisibleToCustomer;
                     hList.Add(h);
                 }
                 if (vj1.DepartDate != vj2.DepartDate)
                 {
                     h = new History();
                     h.FieldName = "Depature Date:";
                     h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                     h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(vj1.DepartDate).Replace("00:00:00", " "));
                     h.ModifiedDate = vj1.ModifiedDate;
                     h.ModifiedBy = vi_user;
                     h.VisibleToCustomer = vj1.VisibleToCustomer;
                     hList.Add(h);
                 }
             }
             if (vj1.PhotoID != vj2.PhotoID)
             {
                 string photo = "No";
                 h = new History();
                 h.FieldName = "Photo ID:";
                 if (vj1.PhotoID == true)
                     photo = "yes";
                 h.FieldValue = photo;
                 h.VisitorName = HttpUtility.HtmlDecode("<b>" + vj1.Name + " under access number " + vj1.AccessNo + "</b>");
                 h.ModifiedDate = vj1.ModifiedDate;
                 h.ModifiedBy = vi_user;
                 h.VisibleToCustomer = vj1.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList;
         }

         private static List<History> AccessControl_history_firstrecord(List<PurposeToVisit> vpList, List<UserMgt.Entity.Contractor> cList, List<StorageLocation> slList, List<OurSite> ostList, List<ProjectPortfolio> ppList, List<PortfolioContact> pcLsit, List<Status> stList,  AccessControlJournal ac, CallDetailsJournal cd)
         {
             List<History> hList = new List<History>();
             History h=null;
             string c_user = cList.Where(c => c.ID == cd.LoggedBy).Select(c => c.ContractorName).FirstOrDefault();
             //if (cd.CompanyID != null)
             //{
             //    h = new History();
             //    h.FieldName = "Requester Company:";
             //    h.FieldValue = ppList.Where(p => p.ID == cd.CompanyID).Select(p => p.PortFolio).FirstOrDefault();
             //    h.ModifiedBy = c_user;
             //    h.ModifiedDate = cd.ModifiedDate;
             //    h.VisibleToCustomer = cd.VisibleToCustomer;
             //    hList.Add(h);
             //}
             if (cd.RequesterID != null)
             {
                 h = new History();
                 h.FieldName = "Requester Name:";
                 h.FieldValue = pcLsit.Where(p => p.ID == cd.RequesterID).Select(p => p.Name).FirstOrDefault();
                 h.ModifiedBy = c_user;
                 h.ModifiedDate = cd.ModifiedDate;
                 h.VisibleToCustomer = cd.VisibleToCustomer;
                 hList.Add(h);
             }

             if (cd.SiteID != null)
             {
                 h = new History();
                 h.FieldName = "Site:";
                 h.FieldValue = ostList.Where(o => o.ID == cd.SiteID).Select(o => o.Name).FirstOrDefault();
                 h.ModifiedBy = c_user;
                 h.ModifiedDate = cd.ModifiedDate;
                 h.VisibleToCustomer = cd.VisibleToCustomer;
                 hList.Add(h);
             }
             if (cd.StatusID != null)
             {
                 h = new History();
                 h.FieldName = "Status:";
                 h.FieldValue = stList.Where(s => s.ID == cd.StatusID).Select(s => s.Name).FirstOrDefault();
                 h.ModifiedBy = c_user;
                 h.ModifiedDate = cd.ModifiedDate;
                 h.VisibleToCustomer = cd.VisibleToCustomer;
                 hList.Add(h);
             }

             if (ac.RequestedDate != null)
             {
                 h = new History();
                 h.FieldName = "Requested Date:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ac.RequestedDate).Replace("00:00:00", " "));
                 h.ModifiedDate = cd.ModifiedDate;
                 h.ModifiedBy = c_user;
                 h.VisibleToCustomer = cd.VisibleToCustomer;
                 hList.Add(h);
             }

             if (ac.NumberOfDays != null)
             {
                 h = new History();
                 h.FieldName = "Number of days:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ac.NumberOfDays).Replace("00:00:00", " "));
                 h.ModifiedBy = c_user;
                 h.ModifiedDate = cd.ModifiedDate;
                 h.VisibleToCustomer = cd.VisibleToCustomer;
                 hList.Add(h);
             }
             if (ac.AreaID != null)
             {
                 h = new History();
                 h.FieldName = "Area:";
                 h.FieldValue = h.FieldValue = slList.Where(d => d.ID == ac.AreaID).Select(d => d.Name).FirstOrDefault();
                 h.ModifiedBy = c_user;
                 h.ModifiedDate = cd.ModifiedDate;
                 h.VisibleToCustomer = cd.VisibleToCustomer;
                 hList.Add(h);
             }
             if (ac.PurposeOfVisit != null)
             {
                 h = new History();
                 h.FieldName = "Purpose of visit:";
                 h.FieldValue = vpList.Where(d => d.ID == ac.PurposeOfVisit).Select(d => d.Name).FirstOrDefault();
                 h.ModifiedBy = c_user;
                 h.ModifiedDate = cd.ModifiedDate;
                 h.VisibleToCustomer = cd.VisibleToCustomer;
                 hList.Add(h);
             }


             if (ac.Notes != null)
             {
                 h = new History();
                 h.FieldName = "Notes:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ac.Notes).Replace("00:00:00", " "));
                 h.ModifiedBy = c_user;
                 h.ModifiedDate = cd.ModifiedDate;
                 h.VisibleToCustomer = cd.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList;
         }

         private static List<History> AccessControl_history_list(List<PurposeToVisit> vpList, List<UserMgt.Entity.Contractor> cList, List<StorageLocation> slList, AccessControlJournal acj1, AccessControlJournal acj2)
         {
             List<History> hList = new List<History>();
             History h= null;
             string ac_user = cList.Where(c => c.ID == acj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
             if (acj1.RequestedDate != acj2.RequestedDate)
             {
                 h = new History();
                 h.FieldName = "Requested Date:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(acj1.RequestedDate).Replace("00:00:00", " "));
                 h.ModifiedDate = acj1.ModifiedDate;
                 h.ModifiedBy = ac_user;
                 h.VisibleToCustomer = acj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (acj1.NumberOfDays != acj2.NumberOfDays)
             {
                 h = new History();
                 h.FieldName = "Number of Days:";
                 h.FieldValue = acj1.NumberOfDays.ToString();
                 h.ModifiedDate = acj1.ModifiedDate;
                 h.ModifiedBy = ac_user;
                 h.VisibleToCustomer = acj1.VisibleToCustomer;
                 hList.Add(h);
             }

             if (acj1.AreaID != acj2.AreaID)
             {
                 h = new History();
                 h.FieldName = "Area:";
                 h.FieldValue = slList.Where(d => d.ID == acj1.AreaID).Select(d => d.Name).FirstOrDefault();
                 h.ModifiedDate = acj1.ModifiedDate;
                 h.ModifiedBy = ac_user;
                 h.VisibleToCustomer = acj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (acj1.PurposeOfVisit != acj2.PurposeOfVisit)
             {
                 h = new History();
                 h.FieldName = "Purpose of Visit:";
                 h.FieldValue = vpList.Where(d => d.ID == acj1.PurposeOfVisit).Select(d => d.Name).FirstOrDefault();
                 h.ModifiedDate = acj1.ModifiedDate;
                 h.ModifiedBy = ac_user;
                 h.VisibleToCustomer = acj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (acj1.Notes != acj2.Notes)
             {
                 h = new History();
                 h.FieldName = "Notes:";
                 h.FieldValue = acj1.Notes;
                 h.ModifiedDate = acj1.ModifiedDate;
                 h.ModifiedBy = ac_user;
                 h.VisibleToCustomer = acj1.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList;
         }
         
        [WebMethod(EnableSession = true)]
         public List<History> GetCallDetailsHistoryforAccessControl(int cid, int rid)
         {
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 using (UserDataContext ud = new UserDataContext())
                 {
                     using (LocationDataContext ld = new LocationDataContext())
                     {
                         History h;
                         List<History> hList = new List<History>();
                         List<ProjectPortfolio> ppList = (from p in pd.ProjectPortfolios select p).ToList();
                         List<UserMgt.Entity.Contractor> cList = (from p in ud.Contractors select p).ToList();
                         List<Status> stList = StatusBAL.BindStatus(rid);
                         //List<Site> sList = (from p in ld.Sites select p).ToList();
                         List<OurSite> ostList = OurSiteBAL.BindOurSite();
                         List<PortfolioContact> pcLsit = (from p in pd.PortfolioContacts select p).ToList();
                         List<CallDetailsJournal> lstcdj = new List<CallDetailsJournal>();
                         //if (sessionKeys.SID == 7)
                         //    lstcdj = CallDetailsJournalBAL.SelectCallDetailsJournal_CustomerVisible_byCallID(cid);
                         //else
                             lstcdj = CallDetailsJournalBAL.SelectCallDetailsJournalbyCallID(cid);

                         if (lstcdj.Count > 1)
                         {
                             for (int i = 0; i < (lstcdj.Count) - 1; i++)
                             {
                                 CallDetailsJournal cdj1 = lstcdj[i + 1];
                                 CallDetailsJournal cdj2 = lstcdj[i];
                                 //if (cdj1.CompanyID != cdj2.CompanyID)
                                 //{
                                 //    h = new History();
                                 //    h.FieldName = "Requesters Company:";
                                 //    h.FieldValue = ppList.Where(p => p.ID == cdj1.CompanyID).Select(p => p.PortFolio).FirstOrDefault();
                                 //    h.ModifiedDate = cdj1.ModifiedDate ;
                                 //    h.ModifiedBy = cList.Where(c => c.ID == cdj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                                 //    h.VisibleToCustomer = cdj1.VisibleToCustomer;
                                 //    hList.Add(h);
                                 //}
                                 if (cdj1.RequesterID != cdj2.RequesterID)
                                 {
                                     h = new History();
                                     h.FieldName = "Requesters Name:";
                                     h.FieldValue = pcLsit.Where(p => p.ID == cdj1.RequesterID).Select(p => p.Name).FirstOrDefault();
                                     h.ModifiedDate = cdj1.ModifiedDate ;
                                     h.ModifiedBy = cList.Where(c => c.ID == cdj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                                     h.VisibleToCustomer = cdj1.VisibleToCustomer;
                                     hList.Add(h);
                                 }
                                 if (cdj1.SiteID != cdj2.SiteID)
                                 {
                                     h = new History();
                                     h.FieldName = "Site:";
                                     h.FieldValue = ostList.Where(o => o.ID == cdj1.SiteID).Select(o => o.Name).FirstOrDefault();
                                     h.ModifiedDate = cdj1.ModifiedDate ;
                                     h.ModifiedBy = cList.Where(c => c.ID == cdj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                                     h.VisibleToCustomer = cdj1.VisibleToCustomer;
                                     hList.Add(h);
                                 }
                                 if (cdj1.StatusID != cdj2.StatusID)
                                 {
                                     h = new History();
                                     h.FieldName = "Status:";
                                     h.FieldValue = stList.Where(s => s.ID == cdj1.StatusID).Select(s => s.Name).FirstOrDefault();
                                     h.ModifiedDate = cdj1.ModifiedDate ;
                                     //  h.ModifiedDate = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), Convert.ToString(cdj1.ModifiedDate.Value));
                                     h.ModifiedBy = cList.Where(c => c.ID == cdj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                                     h.VisibleToCustomer = cdj1.VisibleToCustomer;
                                     hList.Add(h);
                                 }
                             }
                         }
                         return hList;
                     }
                 }
             }
         }

        #endregion

       
         #region Permit To work History
        [WebMethod(EnableSession=true)]
         public List<History> BindPermitToWorkHistory(int cid)
         {
             using (UserDataContext ud = new UserDataContext())
             {
                 using (PortfolioDataContext pdt = new PortfolioDataContext())
                 {

                     History h=null;
                     List<StorageLocation> slList = StorageLocationBAL.BindLocation();
                     List<PermitType> lstPtype = PermitTypeBAL.BindPermitTypes();
                     List<UserMgt.Entity.Contractor> cList = (from p in ud.Contractors select p).ToList();
                     List<History> hList = new List<History>();
                     hList = GetCallDetailsHistory(cid, 2);
                     List<PermitToWorkJournal> lstpwj = new List<PermitToWorkJournal>();
                     //if (sessionKeys.SID == 7)
                     //    lstpwj = PermitToWorkJournalBAL.SelectPermitToWorkJournal_CustomerVisible_byCallID(cid);
                     //else
                         lstpwj = PermitToWorkJournalBAL.SelectPermitToWorkJournalbyCallID(cid);
                     if (lstpwj.Count > 0)
                     {
                         //First time Permit To Work Journal binding

                          if (sessionKeys.SID == 7 && lstpwj[0].VisibleToCustomer == true)
                                     hList.AddRange(PermitToWork_History_firstrecord(slList, lstPtype, cList, lstpwj));
                                 else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                              hList.AddRange(PermitToWork_History_firstrecord(slList, lstPtype, cList, lstpwj));
                        
                     }
                     if (lstpwj.Count > 1)
                     {
                         for (int i = 0; i < (lstpwj.Count) - 1; i++)
                         {

                             PermitToWorkJournal pwj1 = lstpwj[i + 1];
                             PermitToWorkJournal pwj2 = lstpwj[i];

                             if (sessionKeys.SID == 7 && pwj1.VisibleToCustomer == true )
                                 hList.AddRange(PermitTowork_History_List(slList, lstPtype, cList, pwj1, pwj2));
                             else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                                 hList.AddRange(PermitTowork_History_List(slList, lstPtype, cList, pwj1, pwj2));
                         }
                     }
                     hList = hList.OrderByDescending(hi => Convert.ToDateTime(hi.ModifiedDate)).ToList();
                     return hList;
                 }
             }
         }

        private static IEnumerable<History> PermitTowork_History_List(List<StorageLocation> slList, List<PermitType> lstPtype, List<UserMgt.Entity.Contractor> cList, PermitToWorkJournal pwj1, PermitToWorkJournal pwj2)
        {
            List<History> hList = new List<History>();
            History h = null;
            if (pwj1.FromDateofWork != pwj2.FromDateofWork)
            {
                h = new History();

                h.FieldName = "From Date of Work:";
                h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(pwj1.FromDateofWork).Replace("00:00:00", " "));
                h.ModifiedDate = pwj1.ModifiedDate;
                h.ModifiedBy = cList.Where(c => c.ID == pwj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                h.VisibleToCustomer = pwj1.VisibleToCustomer;
                hList.Add(h);
            }
            if (pwj1.ToDateofWork != pwj2.ToDateofWork)
            {
                h = new History();
                h.FieldName = "To Date of Work:";
                h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(pwj1.ToDateofWork).Replace("00:00:00", " "));
                h.ModifiedDate = pwj1.ModifiedDate;
                h.ModifiedBy = cList.Where(c => c.ID == pwj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                h.VisibleToCustomer = pwj1.VisibleToCustomer;
                hList.Add(h);
            }
            if (pwj1.Area != pwj2.Area)
            {
                h = new History();
                h.FieldName = "Area:";
                h.FieldValue = slList.Where(d => d.ID == pwj1.Area).Select(d => d.Name).FirstOrDefault();
                h.ModifiedDate = pwj1.ModifiedDate;
                h.ModifiedBy = cList.Where(c => c.ID == pwj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                h.VisibleToCustomer = pwj1.VisibleToCustomer;
                hList.Add(h);
            }
            if (pwj1.TypeofPermit != pwj2.TypeofPermit)
            {
                h = new History();
                h.FieldName = "Type of Permit:";
                h.FieldValue = lstPtype.Where(d => d.ID == pwj1.TypeofPermit).Select(d => d.Type).FirstOrDefault();
                h.ModifiedDate = pwj1.ModifiedDate;
                h.ModifiedBy = cList.Where(c => c.ID == pwj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                h.VisibleToCustomer = pwj1.VisibleToCustomer;
                hList.Add(h);
            }
            if (pwj1.DescriptionofWorks != pwj2.DescriptionofWorks)
            {
                h = new History();
                h.FieldName = "Description of Works:";
                h.FieldValue = pwj1.DescriptionofWorks;
                h.ModifiedDate = pwj1.ModifiedDate;
                h.ModifiedBy = cList.Where(c => c.ID == pwj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                h.VisibleToCustomer = pwj1.VisibleToCustomer;
                hList.Add(h);
            }
            if (pwj1.ArrivalDate != pwj2.ArrivalDate)
            {
                h = new History();
                h.FieldName = "Arrival Date:";
                h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(pwj1.ArrivalDate).Replace("00:00:00", " "));
                h.ModifiedDate = pwj1.ModifiedDate;
                h.ModifiedBy = cList.Where(c => c.ID == pwj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                h.VisibleToCustomer = pwj1.VisibleToCustomer;
                hList.Add(h);
            }
            if (pwj1.Reason != pwj2.Reason)
            {
                h = new History();
                h.FieldName = "Reason:";
                h.FieldValue = pwj1.Reason;
                h.ModifiedDate = pwj1.ModifiedDate;
                h.ModifiedBy = cList.Where(c => c.ID == pwj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                h.VisibleToCustomer = pwj1.VisibleToCustomer;
                hList.Add(h);
            }
            if (pwj1.Notes != pwj2.Notes)
            {
                h = new History();
                h.FieldName = "Notes:";
                h.FieldValue = pwj1.Notes;
                h.ModifiedDate = pwj1.ModifiedDate;
                h.ModifiedBy = cList.Where(c => c.ID == pwj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                h.VisibleToCustomer = pwj1.VisibleToCustomer;
                hList.Add(h);
            }
            return hList;
        }
        

        private static IEnumerable<History> PermitToWork_History_firstrecord(List<StorageLocation> slList, List<PermitType> lstPtype, List<UserMgt.Entity.Contractor> cList,  List<PermitToWorkJournal> lstpwj)
         {
            List<History> hList =new List<History>();
             History h=null;
             PermitToWorkJournal pwj = lstpwj[0];
             h = new History();
             if (pwj.FromDateofWork != null)
             {
                 h.FieldName = "From Date of Work:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(pwj.FromDateofWork).Replace("00:00:00", " "));
                 h.ModifiedDate = pwj.ModifiedDate;
                 h.ModifiedBy = cList.Where(c => c.ID == pwj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = pwj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (pwj.ToDateofWork != null)
             {
                 h = new History();
                 h.FieldName = "To Date of Work:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(pwj.ToDateofWork).Replace("00:00:00", " "));
                 h.ModifiedDate = pwj.ModifiedDate;
                 h.ModifiedBy = cList.Where(c => c.ID == pwj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = pwj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (pwj.Area != null)
             {
                 h = new History();
                 h.FieldName = "Area:";
                 h.FieldValue = slList.Where(d => d.ID == pwj.Area).Select(d => d.Name).FirstOrDefault();
                 h.ModifiedDate = pwj.ModifiedDate;
                 h.ModifiedBy = cList.Where(c => c.ID == pwj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = pwj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (pwj.TypeofPermit != null)
             {
                 h = new History();
                 h.FieldName = "Type of Permit:";
                 h.FieldValue = lstPtype.Where(d => d.ID == pwj.TypeofPermit).Select(d => d.Type).FirstOrDefault();
                 h.ModifiedDate = pwj.ModifiedDate;
                 h.ModifiedBy = cList.Where(c => c.ID == pwj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = pwj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (pwj.DescriptionofWorks != null)
             {
                 h = new History();
                 h.FieldName = "Description of Works:";
                 h.FieldValue = pwj.DescriptionofWorks;
                 h.ModifiedDate = pwj.ModifiedDate;
                 h.ModifiedBy = cList.Where(c => c.ID == pwj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = pwj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (pwj.ArrivalDate != null)
             {
                 h = new History();
                 h.FieldName = "Arrival Date:";
                 h.FieldValue = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(pwj.ArrivalDate).Replace("00:00:00", " "));
                 h.ModifiedDate = pwj.ModifiedDate;
                 h.ModifiedBy = cList.Where(c => c.ID == pwj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = pwj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (pwj.Reason != null)
             {
                 h = new History();
                 h.FieldName = "Reason:";
                 h.FieldValue = pwj.Reason;
                 h.ModifiedDate = pwj.ModifiedDate;
                 h.ModifiedBy = cList.Where(c => c.ID == pwj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = pwj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (pwj.Notes != null)
             {
                 h = new History();
                 h.FieldName = "Notes:";
                 h.FieldValue = pwj.Notes;
                 h.ModifiedDate = pwj.ModifiedDate;
                 h.ModifiedBy = cList.Where(c => c.ID == pwj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = pwj.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList;
         }
        
        #endregion

         #region Call Details
         [WebMethod(EnableSession = true)]
         public List<History> GetCallDetailsHistory(int cid,int rid)
         {
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 using (UserDataContext ud = new UserDataContext())
                 {
                     using (LocationDataContext ld = new LocationDataContext())
                     {
                         History h;
                         List<History> hList = new List<History>();
                         List<ProjectPortfolio> ppList = (from p in pd.ProjectPortfolios select p).ToList();
                         List<UserMgt.Entity.Contractor> cList = (from p in ud.Contractors select p).ToList();
                         List<Status> stList = StatusBAL.BindStatus(rid);
                         //List<Site> sList = (from p in ld.Sites select p).ToList();
                         List<OurSite> ostList = OurSiteBAL.BindOurSite();
                         List<PortfolioContact> pcLsit = (from p in pd.PortfolioContacts select p).ToList();
                         pcLsit.Add(new PortfolioContact { ID = 0, Name = "" });
                         List<CallDetailsJournal> lstcdj = new List<CallDetailsJournal>();
                         //if (sessionKeys.SID == 7)
                         //    lstcdj = CallDetailsJournalBAL.SelectCallDetailsJournal_CustomerVisible_byCallID(cid);
                         //else
                             lstcdj = CallDetailsJournalBAL.SelectCallDetailsJournalbyCallID(cid);

                             if (lstcdj.Count > 0)
                             {
                                 if (sessionKeys.SID == 7 && lstcdj[0].VisibleToCustomer == true)
                                     hList.AddRange(CallDetails_History_FirstRecord(ppList, cList, stList, ostList, pcLsit, lstcdj));
                                 else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                                     hList.AddRange(CallDetails_History_FirstRecord(ppList, cList, stList, ostList, pcLsit, lstcdj));
                             }
                         if (lstcdj.Count > 1)
                         {
                             for (int i = 0; i < (lstcdj.Count) - 1; i++)
                             {
                                 CallDetailsJournal cdj1 = lstcdj[i + 1];
                                 CallDetailsJournal cdj2 = lstcdj[i];

                                 if (sessionKeys.SID == 7 && cdj1.VisibleToCustomer == true )
                                     hList.AddRange(CallDetails_History_list(ppList, cList, stList, ostList, pcLsit, cdj1, cdj2));
                                 else if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
                                     hList.AddRange(CallDetails_History_list(ppList, cList, stList, ostList, pcLsit, cdj1, cdj2));
                             }
                         }
                         return hList;
                     }
                 }
             }
         }

         private static IEnumerable<History> CallDetails_History_list( List<ProjectPortfolio> ppList, List<UserMgt.Entity.Contractor> cList, List<Status> stList, List<OurSite> ostList, List<PortfolioContact> pcLsit, CallDetailsJournal cdj1, CallDetailsJournal cdj2)
         {
             List<History> hList= new List<History>();
             History h= null;
             //if (cdj1.CompanyID != cdj2.CompanyID)
             //{
             //    h = new History();
             //    h.FieldName = "Requesters Company:";
             //    h.FieldValue = ppList.Where(p => p.ID == cdj1.CompanyID).Select(p => p.PortFolio).FirstOrDefault();
             //    h.ModifiedDate = cdj1.ModifiedDate;
             //    h.ModifiedBy = cList.Where(c => c.ID == cdj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
             //    h.VisibleToCustomer = cdj1.VisibleToCustomer;
             //    hList.Add(h);
             //}
             if (cdj1.RequesterID != cdj2.RequesterID)
             {
                 h = new History();
                 h.FieldName = "Requesters Name:";
                 h.FieldValue = pcLsit.Where(p => p.ID == cdj1.RequesterID).Select(p => p.Name).FirstOrDefault();
                 h.ModifiedDate = cdj1.ModifiedDate;
                 h.ModifiedBy = cList.Where(c => c.ID == cdj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = cdj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (cdj1.SiteID != cdj2.SiteID)
             {
                 h = new History();
                 h.FieldName = "Site:";
                 h.FieldValue = ostList.Where(o => o.ID == cdj1.SiteID).Select(o => o.Name).FirstOrDefault();
                 h.ModifiedDate = cdj1.ModifiedDate;
                 h.ModifiedBy = cList.Where(c => c.ID == cdj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = cdj1.VisibleToCustomer;
                 hList.Add(h);
             }
             if (cdj1.StatusID != cdj2.StatusID)
             {
                 h = new History();
                 h.FieldName = "Status:";
                 h.FieldValue = stList.Where(s => s.ID == cdj1.StatusID).Select(s => s.Name).FirstOrDefault();
                 h.ModifiedDate = cdj1.ModifiedDate;
                 //  h.ModifiedDate = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), Convert.ToString(cdj1.ModifiedDate.Value));
                 h.ModifiedBy = cList.Where(c => c.ID == cdj1.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = cdj1.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList;
         }

         private static IEnumerable<History> CallDetails_History_FirstRecord( List<ProjectPortfolio> ppList, List<UserMgt.Entity.Contractor> cList, List<Status> stList, List<OurSite> ostList, List<PortfolioContact> pcLsit, List<CallDetailsJournal> lstcdj)
         {
             List<History> hList= new List<History>();
             History h=null;
             //First time Call history binding
             CallDetailsJournal cdj = lstcdj[0];
             //if (cdj.CompanyID != null)
             //{
             //    h = new History();
             //    h.FieldName = "Requesters Company:";
             //    h.FieldValue = ppList.Where(p => p.ID == cdj.CompanyID).Select(p => p.PortFolio).FirstOrDefault();
             //    h.ModifiedDate = cdj.ModifiedDate;
             //    h.ModifiedBy = cList.Where(c => c.ID == cdj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
             //    h.VisibleToCustomer = cdj.VisibleToCustomer;
             //    hList.Add(h);
             //}
             if (cdj.RequesterID != null && cdj.RequesterID != 0)
             {
                 h = new History();
                 h.FieldName = "Requesters Name:";
                 h.FieldValue = pcLsit.Where(p => p.ID == cdj.RequesterID).Select(p => p.Name).FirstOrDefault();
                 h.ModifiedDate = cdj.ModifiedDate;
                 h.ModifiedBy = cList.Where(c => c.ID == cdj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = cdj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (cdj.SiteID != null && cdj.SiteID !=0)
             {
                 h = new History();
                 h.FieldName = "Site:";
                 h.FieldValue = ostList.Where(s => s.ID == cdj.SiteID).Select(s => s.Name).FirstOrDefault();
                 h.ModifiedDate = cdj.ModifiedDate;
                 h.ModifiedBy = cList.Where(c => c.ID == cdj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = cdj.VisibleToCustomer;
                 hList.Add(h);
             }
             if (cdj.StatusID != null)
             {
                 h = new History();
                 h.FieldName = "Status:";
                 h.FieldValue = stList.Where(s => s.ID == cdj.StatusID).Select(s => s.Name).FirstOrDefault();
                 h.ModifiedDate = cdj.ModifiedDate;
                 //  h.ModifiedDate = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), Convert.ToString(cdj1.ModifiedDate.Value));
                 h.ModifiedBy = cList.Where(c => c.ID == cdj.ModifiedBy).Select(c => c.ContractorName).FirstOrDefault();
                 h.VisibleToCustomer = cdj.VisibleToCustomer;
                 hList.Add(h);
             }
             return hList ;
         }
        #endregion

        [WebMethod (EnableSession=true)]
         public int GetContactID()
         {
             int cid = 0;
             UserMgt.Entity.Contractor c = CustomerDetailsBAL.GetContractorDetailsbyID(sessionKeys.UID);
             if (c != null)
             {
                 cid = CustomerDetailsBAL.CheckExists(c.ContractorName, c.EmailAddress,sessionKeys.PortfolioID);
                 //if (cid == 0)
                 //{
                 //    //cid = CustomerDetailsBAL.InsertPortfolioContact(c.ContractorName, c.EmailAddress,c.ContactNumber);
                 //}
             }
             return cid;
         }

        [WebMethod(EnableSession = true)]
         public void Delivery_CustomerVisiblityUpdate(int CallID, DateTime ModifiedDate, bool chk)
         {
             using(DCDataContext dc = new DCDataContext())
             {
                 bool isChanged = false;
                 
                //check in call details
                 CallDetailsJournal ch = dc.CallDetailsJournals.Where(p => DateTime.Equals(p.ModifiedDate.Value, ModifiedDate) && p.CallID == CallID).FirstOrDefault();
                 if (ch != null)
                 { 
                     ch.VisibleToCustomer = chk;
                     isChanged = true;
                 }

                 DeliveryInformationJournal dj = dc.DeliveryInformationJournals.Where(p => DateTime.Equals( p.ModifiedDate.Value,ModifiedDate) && p.CallID == CallID).FirstOrDefault();
                 if (dj != null)
                 {
                     dj.VisibleToCustomer = chk;
                     isChanged = true;
                 }

                 //check in call details
                 RecievedInformationJournal rj = dc.RecievedInformationJournals.Where(p => DateTime.Equals(p.ModifiedDate.Value, ModifiedDate) && p.CallID == CallID).FirstOrDefault();
                 if (rj != null)
                 {
                     rj.VisibleToCustomer = chk;
                     isChanged = true;
                 }

                 if (isChanged)
                     dc.SubmitChanges();
             }
         }
         public void Fls_CustomerVisiblityUpdate(int CallID, DateTime ModifiedDate, bool chk)
         {
             using (DCDataContext dc = new DCDataContext())
             {
                 //check in call details
                 CallDetailsJournal ch = dc.CallDetailsJournals.Where(p => p.ModifiedDate == ModifiedDate && p.CallID == CallID).FirstOrDefault();
                 if (ch != null)
                 {
                     ch.VisibleToCustomer = chk;
                     dc.SubmitChanges();
                 }

                 //check in call details
                 FLSDetailsJournal dj = dc.FLSDetailsJournals.Where(p => p.ModifiedDate == ModifiedDate && p.CallID == CallID).FirstOrDefault();
                 if (dj != null)
                 {
                     dj.VisibleToCustomer = chk;
                     dc.SubmitChanges();
                 }

               
             }
         }

         public void AccessControl_CustomerVisiblityUpdate(int CallID, DateTime ModifiedDate, bool chk)
         {
             using (DCDataContext dc = new DCDataContext())
             {
                 //check in call details
                 CallDetailsJournal ch = dc.CallDetailsJournals.Where(p => p.ModifiedDate == ModifiedDate && p.CallID == CallID).FirstOrDefault();
                 if (ch != null)
                 {
                     ch.VisibleToCustomer = chk;
                     dc.SubmitChanges();
                 }

                 //check in call details
                 AccessControlJournal dj = dc.AccessControlJournals.Where(p => p.ModifiedDate == ModifiedDate && p.CallID == CallID).FirstOrDefault();
                 if (dj != null)
                 {
                     dj.VisibleToCustomer = chk;
                     dc.SubmitChanges();
                 }

                 //check in call details
                List<VisitorsJournal> vj = dc.VisitorsJournals.Where(p => p.ModifiedDate == ModifiedDate && p.CallID == CallID).ToList();
                
                 if (vj != null)
                 {
                     foreach (VisitorsJournal v in vj)
                     {
                         v.VisibleToCustomer = chk;
                         dc.SubmitChanges();
                     }
                 }

             }
         }
         public void PermitToWork_CustomerVisiblityUpdate(int CallID, DateTime ModifiedDate, bool chk)
         {
             using (DCDataContext dc = new DCDataContext())
             {
                 //check in call details
                 CallDetailsJournal ch = dc.CallDetailsJournals.Where(p => p.ModifiedDate == ModifiedDate && p.CallID == CallID).FirstOrDefault();
                 if (ch != null)
                 {
                     ch.VisibleToCustomer = chk;
                     dc.SubmitChanges();
                 }

                 //check in call details
                 PermitToWorkJournal dj = dc.PermitToWorkJournals.Where(p => p.ModifiedDate == ModifiedDate && p.CallID == CallID).FirstOrDefault();
                 if (dj != null)
                 {
                     dj.VisibleToCustomer = chk;
                     dc.SubmitChanges();
                 }

               

             }
         }
        

         
         [WebMethod(EnableSession = true)]
         [System.Web.Script.Services.ScriptMethod]
         public CascadingDropDownNameValue[] GetDeliveryItemWeight(string knownCategoryValues, string category)
         {
             var x = DeliveryItemWeightBAL.DeliveryItemWeightBAL_Select();  
             var result = (from p in x
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Weight_Value }).ToArray();
             return result;

         }
        [WebMethod(EnableSession = true)]
        public PortfolioContactAddressDetails GetPortfolioContactDetailsSearch(string id)
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                //var portfolioContactsDepartmentList = pd.PortfolioContactsDepartments.Select(p => p).ToList();
                //portfolioContactsDepartmentList.Add(new PortfolioContactsDepartment { ID = 0, Name = "" });
                string stext = id.ToLower().Trim();
                //var portfolioContactsList = pd.PortfolioContactAddresses.ToList();
                IUserRepository<UserMgt.Entity.v_contractor> pRepository = new UserRepository<UserMgt.Entity.v_contractor>();
                var clist = pRepository.GetAll().Where(o => o.ID == Convert.ToInt32(stext)).ToList();

                //var r1 = (from p in pd.PortfolioContacts
                //          join d in pd.PortfolioContactAddresses on p.ID equals d.ContactID
                //          //join pl in pd.ProductPolicyTypes on d.PolicyTypeID equals pl.ID
                //          where ( p.Name.ToLower().Contains(stext)) && p.PortfolioID == sessionKeys.PortfolioID select d).ToList();
                var result = (from p in clist
                                  //join d in pd.PortfolioContactAddresses on p.ID equals d.ContactID
                                  //join pl in pd.ProductPolicyTypes on d.PolicyTypeID equals pl.ID
                                  //where ( p.Name.ToLower().Contains(stext) || p.Email.ToLower().Contains(stext)
                                  //|| p.PostCode.ToLower().Contains(stext) || p.State.ToLower().Contains(stext) || p.Address.ToLower().Contains(stext)
                                  //|| p.City.ToLower().Contains(stext)) && p.PortfolioID == sessionKeys.PortfolioID
                              select new PortfolioContactAddressDetails
                              {
                                  ID = p.ID,
                                  AddressID = p.ID,
                                  RequesterName = p.ContractorName != null ? p.ContractorName : string.Empty,
                                  RequesterEmail = p.EmailAddress != null ? p.EmailAddress : string.Empty,
                                  Location = p.Address1 != null ? p.Address1 : string.Empty,
                                  Mobile = p.ContactNumber != null ? p.ContactNumber : string.Empty,
                                  Telephone = p.ContactNumber != null ? p.ContactNumber : string.Empty,
                                  Title = p.ContractorName != null ? p.ContractorName : string.Empty,
                                  Department = "0",
                                  Address = p.Address1 == null ? string.Empty : p.Address1,
                                  
                                  PostCode = p.PostCode == null ? string.Empty : p.PostCode,
                                  Town = p.State == null ? string.Empty : p.State,
                                  City = p.State == null ? string.Empty : p.State,
                                  DaysRemaining = string.Empty,
                                  ExpiryDate = string.Empty,
                                  StartDate = string.Empty,
                                  PolicyNumber = string.Empty,
                                  PolicyTypeID = 0,
                                  //PolicyNotes = pl.Description != null ? pl.Description : string.Empty,
                                  //PolicyType = pl.Title != null ? pl.Title : string.Empty
                                  PolicyNotes = string.Empty,
                                  PolicyType = string.Empty
                              }).FirstOrDefault();

                return result;
            }
            //using (PortfolioDataContext pd = new PortfolioDataContext())
            //{
            //    var portfolioContactsDepartmentList = pd.PortfolioContactsDepartments.Select(p=>p).ToList();
            //    portfolioContactsDepartmentList.Add(new PortfolioContactsDepartment { ID = 0, Name = "" });

            //    var portfolioContactsList = pd.PortfolioContacts.ToList();
            //    var result = (from p in portfolioContactsList
            //                  join d in portfolioContactsDepartmentList on p.DepartmentID.HasValue ? p.DepartmentID : 0 equals d.ID
            //                  where p.ID == id
            //                  select new PortfolioContactDetails
            //                  {
            //                      ID = p.ID,
            //                      RequesterName = p.Name,
            //                      RequesterEmail = p.Email,
            //                      Location = p.Location,
            //                      Mobile = p.Mobile,
            //                      Telephone = p.Telephone,
            //                      Title = p.Title,
            //                      Department = d.Name,
            //                      Address= p.Address1,
            //                      PostCode = p.Postcode,
            //                      Town = p.Town,
            //                      City=p.City
            //                  }).FirstOrDefault();

            //    return result;
            //}
        }

        [WebMethod(EnableSession = true)]
         public PortfolioContactAddressDetails GetPortfolioContactDetailsByAddressID(string id)
         {
             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 //var portfolioContactsDepartmentList = pd.PortfolioContactsDepartments.Select(p => p).ToList();
                 //portfolioContactsDepartmentList.Add(new PortfolioContactsDepartment { ID = 0, Name = "" });
                 string stext = id.ToLower().Trim();
                //var portfolioContactsList = pd.PortfolioContactAddresses.Where(o=>o.ID == Convert.ToInt32(id)).ToList();
                //var dval = portfolioContactsList.FirstOrDefault();
                //var contid = dval.ContactID;
                //var portfolioContacts = pd.PortfolioContacts.Where(o=>o.ID == contid).ToList();
                // var portfolioContactsList = pd.PortfolioContactAddresses.Where(o=> portfolioContacts.Select(p=>p.ID).Contains(o.ContactID)).ToList();
                IUserRepository<UserMgt.Entity.v_contractor> pRepository = new UserRepository<UserMgt.Entity.v_contractor>();
                var clist = pRepository.GetAll().Where(o => o.ID == Convert.ToInt32(stext)).ToList();

               // var ptypes = pd.ProductPolicyTypes.Where(o=>o.CustomerID == sessionKeys.PortfolioID).ToList();
                 var result = (from p in clist
                               where (p.ID == Convert.ToInt32(stext)) && p.CompanyID == sessionKeys.PortfolioID
                               select new PortfolioContactAddressDetails
                               {
                                   ID = p.ID,
                                   AddressID = p.ID,
                                   RequesterName = p.ContractorName != null ? p.ContractorName : string.Empty,
                                   RequesterEmail = p.EmailAddress != null ? p.EmailAddress : string.Empty,
                                   Location = p.Address1 != null ? p.Address1 : string.Empty,
                                   Mobile = p.ContactNumber != null ? p.ContactNumber : string.Empty,
                                   Telephone = p.ContactNumber != null ? p.ContactNumber : string.Empty,
                                   Title = p.ContractorName != null ? p.ContractorName : string.Empty,
                                   Department = "0",
                                   Address = p.Address1 == null ? string.Empty : p.Address1,
                                   PostCode = p.PostCode == null ? string.Empty : p.PostCode,
                                   Town = p.State == null ? string.Empty : p.State,
                                   City = p.State == null ? string.Empty : p.State,
                                   DaysRemaining =  string.Empty,
                                   ExpiryDate = string.Empty,
                                   StartDate =  string.Empty,
                                   PolicyNumber = string.Empty,
                                   PolicyTypeID  = 0,
                                   //PolicyNotes = pl.Description != null ? pl.Description : string.Empty,
                                   //PolicyType = pl.Title != null ? pl.Title : string.Empty
                                   PolicyNotes =  string.Empty,
                                   PolicyType =  string.Empty
                               }).ToList();
                var result1= (from r in result
                             select new PortfolioContactAddressDetails
                             {
                                 ID = r.ID,
                                 AddressID = r.AddressID,
                                 RequesterName = r.RequesterName,
                                 RequesterEmail = r.RequesterEmail,
                                 Location = r.Location ,
                                 Mobile = r.Mobile,
                                 Telephone = r.Telephone ,
                                 Title = r.Title,
                                 Department = "0",
                                 Address = r.Address,
                                 PostCode = r.PostCode,
                                 Town = r.Town,
                                 City = r.City,
                                 DaysRemaining = r.DaysRemaining,
                                 ExpiryDate = r.ExpiryDate,
                                 StartDate = r.StartDate,
                                 PolicyNumber = r.PolicyNumber,
                                
                                 PolicyNotes = string.Empty,
                                 PolicyType = string.Empty,
                             }).FirstOrDefault();
                return result1;
             }
             //using (PortfolioDataContext pd = new PortfolioDataContext())
             //{
             //    var portfolioContactsDepartmentList = pd.PortfolioContactsDepartments.Select(p=>p).ToList();
             //    portfolioContactsDepartmentList.Add(new PortfolioContactsDepartment { ID = 0, Name = "" });

             //    var portfolioContactsList = pd.PortfolioContacts.ToList();
             //    var result = (from p in portfolioContactsList
             //                  join d in portfolioContactsDepartmentList on p.DepartmentID.HasValue ? p.DepartmentID : 0 equals d.ID
             //                  where p.ID == id
             //                  select new PortfolioContactDetails
             //                  {
             //                      ID = p.ID,
             //                      RequesterName = p.Name,
             //                      RequesterEmail = p.Email,
             //                      Location = p.Location,
             //                      Mobile = p.Mobile,
             //                      Telephone = p.Telephone,
             //                      Title = p.Title,
             //                      Department = d.Name,
             //                      Address= p.Address1,
             //                      PostCode = p.Postcode,
             //                      Town = p.Town,
             //                      City=p.City
             //                  }).FirstOrDefault();

             //    return result;
             //}
         }



         [WebMethod(EnableSession = true)]
         public PortfolioContactDetails GetPortfolioContactDetails(int id)
         {
             //using (PortfolioDataContext pd = new PortfolioDataContext())
             //{
                 //var portfolioContactsDepartmentList = pd.PortfolioContactsDepartments.Select(p => p).ToList();
                 //portfolioContactsDepartmentList.Add(new PortfolioContactsDepartment { ID = 0, Name = "" });

                 //var portfolioContactsList = pd.PortfolioContactAddresses.Where(o=>o.ID == id).ToList();
                 //var result = (from p in pd.PortfolioContacts
                 //              join d in pd.PortfolioContactAddresses on p.ID equals d.ContactID
                 //              where d.ID == id
                 //              select new PortfolioContactDetails
                 //              {
                 //                  ID = p.ID,
                 //                  RequesterName = p.Name == null ? string.Empty : p.Name,
                 //                  RequesterEmail = p.Email == null? string.Empty:p.Email,
                 //                  Location = p.Location == null? string.Empty:p.Location,
                 //                  Mobile = p.Mobile == null? string.Empty:p.Mobile,
                 //                  Telephone = p.Telephone == null? string.Empty:p.Telephone,
                 //                  Title = p.Title == null ? string.Empty : p.Title,
                 //                  Department = "0",
                 //                  Address = d.Address == null ? string.Empty : d.Address,
                 //                  PostCode = d.PostCode == null ? string.Empty : d.PostCode,
                 //                  Town = d.State == null ? string.Empty : d.State,
                 //                  City = d.City == null ? string.Empty : d.City
                 //              }).FirstOrDefault();

                 //return result;


                IUserRepository<UserMgt.Entity.v_contractor> pRepository = new UserRepository<UserMgt.Entity.v_contractor>();
                var clist = pRepository.GetAll().Where(o => o.ID == Convert.ToInt32(id)).ToList();

                var result = (from p in clist
                              
                              select new PortfolioContactDetails
                              {
                                  ID = p.ID,
                                  RequesterName = p.ContractorName == null ? string.Empty : p.ContractorName,
                                  RequesterEmail = p.EmailAddress == null ? string.Empty : p.EmailAddress,
                                  Location = p.Address1 == null ? string.Empty : p.Address1,
                                  Mobile = p.ContactNumber == null ? string.Empty : p.ContactNumber,
                                  Telephone = p.ContactNumber == null ? string.Empty : p.ContactNumber,
                                  Title = p.ContractorName == null ? string.Empty : p.ContractorName,
                                  Department = "0",
                                  Address = p.Address1 == null ? string.Empty : p.Address1,
                                  PostCode = p.PostCode == null ? string.Empty : p.PostCode,
                                  Town = p.State == null ? string.Empty : p.State,
                                  City = p.State == null ? string.Empty : p.State
                              }).FirstOrDefault();

                return result;


          //  }
             //using (PortfolioDataContext pd = new PortfolioDataContext())
             //{
             //    var portfolioContactsDepartmentList = pd.PortfolioContactsDepartments.Select(p=>p).ToList();
             //    portfolioContactsDepartmentList.Add(new PortfolioContactsDepartment { ID = 0, Name = "" });

             //    var portfolioContactsList = pd.PortfolioContacts.ToList();
             //    var result = (from p in portfolioContactsList
             //                  join d in portfolioContactsDepartmentList on p.DepartmentID.HasValue ? p.DepartmentID : 0 equals d.ID
             //                  where p.ID == id
             //                  select new PortfolioContactDetails
             //                  {
             //                      ID = p.ID,
             //                      RequesterName = p.Name,
             //                      RequesterEmail = p.Email,
             //                      Location = p.Location,
             //                      Mobile = p.Mobile,
             //                      Telephone = p.Telephone,
             //                      Title = p.Title,
             //                      Department = d.Name,
             //                      Address= p.Address1,
             //                      PostCode = p.Postcode,
             //                      Town = p.Town,
             //                      City=p.City
             //                  }).FirstOrDefault();
                 
             //    return result;
             //}
         }



        [WebMethod(EnableSession = true)]
        public object BindCallAssets(string id, string status="")
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                List<Jqgrid> vlist = new List<Jqgrid>();
                if (id == "0")
                {
                    var d_array = new string[] { "[Loading status...]", "All", "" };
                    if (!d_array.Contains(status))
                    {
                        vlist = FLSDetailsBAL.Jqgridlist().Where(o => o.Status == status).OrderByDescending(o => o.CCID).ToList();
                    }
                    else
                    {
                        string[] s = new[] { "Cancelled", "Job Complete" };
                        vlist = FLSDetailsBAL.Jqgridlist().Where(o => !s.Contains(o.Status)).OrderByDescending(o => o.CCID).ToList();
                    }
                }
                else if (id != "0")
                {
                    //id is requester ID/ Contact ID
                    vlist = FLSDetailsBAL.JqgridlistByRequester(0, Convert.ToInt32(id));//.Where(o => o.RequesterID == Convert.ToInt32(id)).ToList();
                }
                var rlist = (from r in vlist
                             orderby r.CCID descending
                             select new
                             {
                                 ID = r.CallID,
                                 LoggedDate = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToDateTime(r.ScheduledDateTime)),
                                 MakeName = string.Empty,
                                 ModelName = string.Empty,
                                 PolicyType = string.Empty,
                                 RequesterID = r.RequesterID,
                                 SerialNo = string.Empty,
                                 StatusName = r.Status,
                                 TypeName = string.Empty,
                                 Details = r.Details,
                                 AssignedTechnician = r.AssignedTechnician,
                                 CCID = r.CCID,
                                 ClientName = r.RequesterName
                             }).ToList();

                return Jsonserializer.Serialize(rlist).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }


        [WebMethod(EnableSession = true)]
        public object BindPayDetails(string id)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                List<Jqgrid> vlist = new List<Jqgrid>();
                List<DC.Entity.Incident_ServicePrice> iplist = new List<Incident_ServicePrice>();
                if (id == "0")
                {
                    //string[] s = new[] { "Cancelled", "Closed" };
                    string[] s = new[] {  "Closed" };
                    vlist = FLSDetailsBAL.Jqgridlist().Where(o => !s.Contains(o.Status)).OrderByDescending(o => o.CCID).ToList();
                }
                else if (id != "0")
                {
                    //id is requester ID/ Contact ID
                    vlist = FLSDetailsBAL.JqgridlistByRequester(0, Convert.ToInt32(id));//.Where(o => o.RequesterID == Convert.ToInt32(id)).ToList();
                }

                var invList = DC.BLL.InvoiceBAL.SelectInvoiceList().Where(o=> vlist.Select(s=>s.CallID).Contains(o.IncidentID.Value)).ToList();
                
                var rlist = (from r in vlist
                             orderby r.CCID descending
                             where( r.LoggedDate> Deffinity.Utility.StartDateOfMonth(DateTime.Now) && r.LoggedDate < Deffinity.Utility.EndDateOfMonth(DateTime.Now))
                             select new
                             {
                                 ID = r.CallID,
                                 LoggedDate = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToDateTime(r.ScheduledDateTime)),
                                 MakeName = string.Empty,
                                 ModelName = string.Empty,
                                 PolicyType = string.Empty,
                                 RequesterID = r.RequesterID,
                                 SerialNo = string.Empty,
                                 StatusName = r.Status,
                                 TypeName = string.Empty,
                                 Details = r.Details,
                                 AssignedTechnician = r.AssignedTechnician,
                                 CCID = r.CCID,
                                 PaidAmount = string.Format("{0:N2}", invList.Where(o=>o.IncidentID == r.CallID && o.Status == "Paid").Select(o=>o.OriginalPrice).Sum())
                             }).ToList();

                return Jsonserializer.Serialize(rlist.Where(o=>Convert.ToDouble( o.PaidAmount) >0).ToList()).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }
        [WebMethod(EnableSession = true)]
        public object BindAddressDataSearch(string search)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {

                //IPortfolioRepository<PortfolioMgt.Entity.v_PortfolioContactAddress> pRepository = new PortfolioRepository<PortfolioMgt.Entity.v_PortfolioContactAddress>();

                //var clist = pRepository.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.Name.Contains(search) || o.Address.Contains(search) || o.Email.Contains(search) || o.PostCode.Contains(search) || o.City.Contains(search) || o.State.Contains(search)).ToList();

                //var rlist = (from r in clist
                //             select new
                //             {
                //                 AddressID = r.AddressID,
                //                 Name = r.Name,
                //                 Email = r.Email,
                //                 Address = r.Address + " " + r.Address2,
                //                 City = r.City,
                //                 State = r.State,
                //                 Postcode = r.PostCode
                //             }).Take(10).ToList();

                //return Jsonserializer.Serialize(rlist).ToString();


                IUserRepository<UserMgt.Entity.v_contractor> pRepository = new UserRepository<UserMgt.Entity.v_contractor>();

                var clist = pRepository.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID).Where(o => o.ContractorName.Contains(search) || o.EmailAddress.Contains(search) || o.Address1.Contains(search) || o.PostCode.Contains(search) || o.ContactNumber.Contains(search) || o.State.Contains(search)).ToList();

                var rlist = (from r in clist
                             select new
                             {
                                 AddressID = r.ID,
                                 Name = r.ContractorName,
                                 Email = r.EmailAddress,
                                 Address = r.Address1 + " " + r.Address2,
                                 City = r.State,
                                 State = r.State,
                                 Postcode = r.PostCode
                             }).Take(10).ToList();

                return Jsonserializer.Serialize(rlist).ToString();
                //grdstudetails.DataSource = StudentRecords;
                //grdstudetails.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }
        [WebMethod(EnableSession = true)]
        public object BindAddressData(string id)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {

                //IPortfolioRepository<PortfolioMgt.Entity.v_PortfolioContactAddress> pRepository = new PortfolioRepository<PortfolioMgt.Entity.v_PortfolioContactAddress>();

                //var clist = pRepository.GetAll().Where(o => o.AddressID == Convert.ToInt32(id)).ToList();

                //var rlist = (from r in clist
                //             select new
                //             {
                //                 AddressID = r.AddressID,
                //                 Name = r.Name,
                //                 Email = r.Email,
                //                 Address = r.Address + " " + r.Address2,
                //                 City = r.City,
                //                 State = r.State,
                //                 Postcode = r.PostCode
                //             }).ToList();

                //return Jsonserializer.Serialize(rlist).ToString();
                IUserRepository<UserMgt.Entity.v_contractor> pRepository = new UserRepository<UserMgt.Entity.v_contractor>();
                var clist = pRepository.GetAll().Where(o => o.ID == Convert.ToInt32(id)).ToList();
                var rlist = (from r in clist
                             select new
                             {
                                 AddressID = r.ID,
                                 Name = r.ContractorName,
                                 Email = r.EmailAddress,
                                 Address = r.Address1 + " " + r.Address2,
                                 City = r.State,
                                 State = r.State,
                                 Postcode = r.PostCode
                             }).Take(10).ToList();
                return Jsonserializer.Serialize(rlist).ToString();
                //grdstudetails.DataSource = StudentRecords;
                //grdstudetails.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }
[WebMethod(EnableSession = true)]
public object BindOpenClaims(string fromdate,string todate,string search, string period)
{
JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
try
{
var result = GetOpenClaimesList(fromdate, todate, search, period);
return Jsonserializer.Serialize(result).ToString();
//grdstudetails.DataSource = StudentRecords;
//grdstudetails.DataBind();
}
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return Jsonserializer.Serialize(string.Empty).ToString();
             }
         }
        [WebMethod(EnableSession = true)]
        public object BindSaleData(string fromdate, string todate, string search, string period)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                var result = GetSalesDataList(fromdate, todate, search, period);

                return Jsonserializer.Serialize(result).ToString();
                //grdstudetails.DataSource = StudentRecords;
                //grdstudetails.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }
        private static List<OpenClaimsCls> GetSalesDataList(string fromdate, string todate, string search, string period)
        {
            var flslit = FLSDetailsBAL.JqgridlistByStatus(JobStatus.Closed).ToList();
            var jRep = new DCRepository<DC.Entity.CallDetailsJournal>();
            var ctRep = new PortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm>();
            var aRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
            var uRep = new UserRepository<UserMgt.Entity.Contractor>();

            var ptRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
            var alist = aRep.GetAll();
            var jlist = jRep.GetAll();
            var ctlist = ctRep.GetAll();
            var ulist = uRep.GetAll();
            var ptdata = ptRep.GetAll();


            var result = (from f in flslit
                          join a in alist on f.ContactAddressID equals a.ID
                          select new OpenClaimsCls
                          {
                              CCID= f.CCID,
                              Callid = f.CallID,
                              DateClosed = Convert.ToDateTime( f.LoggedDateTime),// jlist.Where(o => o.CallID == f.CallID && o.StatusID == 35).FirstOrDefault().ModifiedDate.Value,
                              DateClosedDis = string.Format(Deffinity.systemdefaults.GetStringDateformat(), f.LoggedDateTime),
                              SalesPerson = a.LoggedBy.HasValue ? ulist.Where(u => u.ID == a.LoggedBy.Value).FirstOrDefault().ContractorName : string.Empty,
                              PolicyType = a.ProductPolicyType.Title,
                              ContractTerm = a.ContractTermID.HasValue ? ctlist.Where(o => o.PCTID == a.ContractTermID).FirstOrDefault().Name : string.Empty,
                              PolicyNumber = a.PolicyNumber,
                              Customer = f.RequesterName,
                              Address = a.Address,
                              Address1 = a.Address2 == null ? string.Empty : a.Address2,
                              City = a.City == null ? string.Empty : a.City,
                              ZipCode = a.PostCode,
                              InvoiceValue = f.TotalCost,
                              Category = f.Category,
                              AssignTechnicianID= f.AssignedTechnicianID,
                              AssignTechnician   = f.AssignedTechnician,
                              TypeOfRequest = f.TypeofRequest,
                              paypalref = ptdata.Where(o=>o.AddressID == f.ContactAddressID && o.IsPaid == true).FirstOrDefault() != null? ptdata.Where(o => o.AddressID == f.ContactAddressID && o.IsPaid == true).FirstOrDefault().PayPalRef:string.Empty,
                              transval = string.Format("{0:F2}", Convert.ToDouble( ptdata.Where(o => o.AddressID == f.ContactAddressID && o.IsPaid == true).FirstOrDefault() != null ? ptdata.Where(o => o.AddressID == f.ContactAddressID && o.IsPaid == true).FirstOrDefault().PaidAmount : 0.00)),
                              details = f.Details,
                              TicketManager = f.TicketManager == null ? string.Empty : f.TicketManager,
                          }).ToList();

            if (!string.IsNullOrEmpty(fromdate))
            {
                var sdate = Convert.ToDateTime(fromdate);
                result = result.Where(o => o.DateClosed >= sdate).ToList();
            }
            if (!string.IsNullOrEmpty(todate))
            {
                var tdate = Convert.ToDateTime(todate);
                result = result.Where(o => o.DateClosed <= tdate).ToList();
            }
            if (!string.IsNullOrEmpty(search))
            {
                //result = result.Where(o => o.DateClosed <= tdate).ToList();
                var txtsearch = search.ToLower();
                result = (from x in result
                          where (
                          (x.Callid != null && x.Callid.ToString().ToLower().Contains(txtsearch)) ||
                          (x.Address != null && x.Address.ToLower().ToString().Contains(txtsearch)) ||
                          (x.Address1 != null && x.Address1.ToString().Contains(txtsearch)) ||
                           (x.City != null && x.City.ToLower().ToString().Contains(txtsearch)) ||
                             (x.ContractTerm != null && x.ContractTerm.ToLower().ToString().Contains(txtsearch)) ||
                             (x.Customer != null && x.Customer.ToString().ToLower().Contains(txtsearch)) ||
                             (x.PolicyNumber != null && x.PolicyNumber.ToString().ToLower().Contains(txtsearch)) ||
                             (x.PolicyType != null && x.PolicyType.ToLower().ToString().Contains(txtsearch)) ||
                             (x.SalesPerson != null && x.SalesPerson.ToString().ToLower().Contains(txtsearch)) ||
                             (x.ZipCode != null && x.ZipCode.ToString().ToLower().Contains(txtsearch))
                             )

                          select x).ToList();
            }
            if (!string.IsNullOrEmpty(period))
            {
                //if (period != "0")
                //{
                //    DateTime p1 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                //    DateTime p2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                //    var currentdate = DateTime.Now;
                //    //Deffinity.Utility.StartDateOfMonth

                //    if (period == "7 days")
                //    {
                //        p2 = DateTime.Now.AddDays(7);
                //    }
                //    else if (period == "this month")
                //    {
                //        p1 = Deffinity.Utility.StartDateOfMonth(currentdate);
                //        p2 = Deffinity.Utility.EndDateOfMonth(currentdate);
                //    }
                //    else if (period == "last month")
                //    {
                //        currentdate = DateTime.Now.AddMonths(-1);
                //        p1 = Deffinity.Utility.StartDateOfMonth(currentdate);
                //        p2 = Deffinity.Utility.EndDateOfMonth(currentdate);
                //    }

                //    result = result.Where(p => p.DateClosed >= p1 && p.DateClosed <= p2).ToList();
                //}

                if (period == "Paid")
                {
                    result = result.Where(p => p.paypalref != string.Empty).ToList();
                }
                else if (period == "Not Processed")
                {
                    result = result.Where(p => p.paypalref == string.Empty).ToList();
                }

            }
            //return result.Where(o => o.paypalref != "").ToList();
            return result.ToList();
        }

        private static List<OpenClaimsCls> GetOpenClaimesList(string fromdate, string todate, string search, string period)
         {
             var flslit = FLSDetailsBAL.Jqgridlist().Where(o => o.Status == "Closed" && o.ContactAddressID > 0).ToList();
             var jRep = new DCRepository<DC.Entity.CallDetailsJournal>();
             var ctRep = new PortfolioRepository<PortfolioMgt.Entity.PolicyContractTerm>();
             var aRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
             var uRep = new UserRepository<UserMgt.Entity.Contractor>();
             var alist = aRep.GetAll();
             var jlist = jRep.GetAll();
             var ctlist = ctRep.GetAll();
             var ulist = uRep.GetAll();


             var result = (from f in flslit
                           join a in alist on f.ContactAddressID equals a.ID
                           select new OpenClaimsCls
                           {
                               Callid = f.CallID,
                              
                               DateClosed = jlist.Where(o => o.CallID == f.CallID && o.StatusID == 35).FirstOrDefault().ModifiedDate.Value,
                               DateClosedDis = string.Format(Deffinity.systemdefaults.GetStringDateformat(), jlist.Where(o => o.CallID == f.CallID && o.StatusID == 35).FirstOrDefault().ModifiedDate),
                               SalesPerson = a.LoggedBy.HasValue ? ulist.Where(u => u.ID == a.LoggedBy.Value).FirstOrDefault().ContractorName : string.Empty,
                               PolicyType = a.ProductPolicyType.Title,
                               ContractTerm = a.ContractTermID.HasValue ? ctlist.Where(o => o.PCTID == a.ContractTermID).FirstOrDefault().Name : string.Empty,
                               PolicyNumber = a.PolicyNumber,
                               Customer = f.RequesterName,
                               Address = a.Address,
                               Address1 = a.Address2 == null ? string.Empty : a.Address2,
                               City = a.City == null ? string.Empty : a.City,
                               ZipCode = a.PostCode,
                               InvoiceValue = f.TotalCost
                           }).ToList();

             if (!string.IsNullOrEmpty(fromdate))
             {
                 var sdate = Convert.ToDateTime(fromdate);
                 result = result.Where(o => o.DateClosed >= sdate).ToList();
             }
             if (!string.IsNullOrEmpty(todate))
             {
                 var tdate = Convert.ToDateTime(todate);
                 result = result.Where(o => o.DateClosed <= tdate).ToList();
             }
             if (!string.IsNullOrEmpty(search))
             {
                 //result = result.Where(o => o.DateClosed <= tdate).ToList();
                 var txtsearch = search.ToLower();
                 result = (from x in result
                           where (
                           (x.Callid != null && x.Callid.ToString().ToLower().Contains(txtsearch)) ||
                           (x.Address != null && x.Address.ToLower().ToString().Contains(txtsearch)) ||
                           (x.Address1 != null && x.Address1.ToString().Contains(txtsearch)) ||
                            (x.City != null && x.City.ToLower().ToString().Contains(txtsearch)) ||
                              (x.ContractTerm != null && x.ContractTerm.ToLower().ToString().Contains(txtsearch)) ||
                              (x.Customer != null && x.Customer.ToString().ToLower().Contains(txtsearch)) ||
                              (x.PolicyNumber != null && x.PolicyNumber.ToString().ToLower().Contains(txtsearch)) ||
                              (x.PolicyType != null && x.PolicyType.ToLower().ToString().Contains(txtsearch)) ||
                              (x.SalesPerson != null && x.SalesPerson.ToString().ToLower().Contains(txtsearch)) ||
                              (x.ZipCode != null && x.ZipCode.ToString().ToLower().Contains(txtsearch))
                              )

                           select x).ToList();
             }
             if (!string.IsNullOrEmpty(period))
             {
                 if (period != "0")
                 {
                     DateTime p1 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                     DateTime p2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                     var currentdate = DateTime.Now;
                     //Deffinity.Utility.StartDateOfMonth

                     if (period == "7 days")
                     {
                         p2 = DateTime.Now.AddDays(7);
                     }
                     else if (period == "this month")
                     {
                         p1 = Deffinity.Utility.StartDateOfMonth(currentdate);
                         p2 = Deffinity.Utility.EndDateOfMonth(currentdate);
                     }
                     else if (period == "last month")
                     {
                         currentdate = DateTime.Now.AddMonths(-1);
                         p1 = Deffinity.Utility.StartDateOfMonth(currentdate);
                         p2 = Deffinity.Utility.EndDateOfMonth(currentdate);
                     }

                     result = result.Where(p => p.DateClosed >= p1 && p.DateClosed <= p2).ToList();
                 }
             }
             return result;
         }

         [WebMethod(EnableSession = true)]
         [System.Web.Script.Services.ScriptMethod]
         public object GetOpenClaimsChat(string fromdate, string todate, string search, string period)
         {
             List<DxchartFNew> DxchartFList = new List<DxchartFNew>();
             DxchartFNew Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 var newList = GetOpenClaimesList(fromdate, todate, search, period);
                 var grouped = from p in newList
                               orderby  p.DateClosed ascending
                               group p by new { month = p.DateClosed.Month, year = p.DateClosed.Year } into d
                               select new { dt = string.Format("{0}/{1}", d.Key.month, d.Key.year), count = d.Sum(s=>Convert.ToDouble(string.IsNullOrEmpty(s.InvoiceValue)?"0.00":s.InvoiceValue)) };
                 foreach (var L in grouped)
                 {
                     Dlist = new DxchartFNew();
                     Dlist.Value = L.count;
                     Dlist.Name = L.dt.ToString();
                     DxchartFList.Add(Dlist);
                 }
                 return jsonSerializer.Serialize(DxchartFList).ToString();
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object GetSalesChat(string fromdate, string todate, string search, string period)
        {
            List<DxchartFNew> DxchartFList = new List<DxchartFNew>();
            DxchartFNew Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                var newList = GetSalesDataList(fromdate, todate, search, period);
                var grouped = from p in newList
                              orderby p.DateClosed ascending
                              group p by new { month = p.DateClosed.Month, year = p.DateClosed.Year } into d
                              select new { dt = string.Format("{0}/{1}", d.Key.month, d.Key.year), count = d.Sum(s => Convert.ToDouble(string.IsNullOrEmpty(s.transval) ? "0.00" : s.transval)) };
                foreach (var L in grouped)
                {
                    Dlist = new DxchartFNew();
                    Dlist.Value = L.count;
                    Dlist.Name = L.dt.ToString();
                    DxchartFList.Add(Dlist);
                }
                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
         public CascadingDropDownNameValue[] GetCategory(string knownCategoryValues, string category)
         {

             var categoyList = CategoryBAL.GetCategoryList();

             var result = (from p in categoyList
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

             return result;



         }
         [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetCategoryByTypeOfRequest(string knownCategoryValues, string category)
         {
             //string[] _catgoryValue = knownCategoryValues.Split(':', ';');
             //string typeOfRequestId = (_catgoryValue[1]);
            //var categoyList = CategoryBAL.GetCategoryList().Where(c => c.TypeOfRequestID == Convert.ToInt32(typeOfRequestId)).ToList();
            var categoyList = CategoryBAL.GetCategoryList().ToList();

            var result = (from p in categoyList
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

             return result;



         }
         [WebMethod(EnableSession = true)]
         public CascadingDropDownNameValue[] GetCategoryByTypeOfRequestFault(string knownCategoryValues, string category)
         {
             string[] _catgoryValue = knownCategoryValues.Split(':', ';');
             //string typeOfRequestId = (_catgoryValue[1]);
             //2-Fault
             var categoyList = CategoryBAL.GetCategoryList().ToList();

             var result = (from p in categoyList
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

             return result;



         }
         [WebMethod(EnableSession=true)]
         public CascadingDropDownNameValue[] GetDepartment(string knownCategoryValues, string category)
         {

             var departmentList = PortfolioContactsDepartmentBAL.GetPortfolioContactsDepartmentList().Where(d => d.CustomerID == sessionKeys.PortfolioID).ToList();

             var result = (from p in departmentList
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

             return result;
         }
         [WebMethod(EnableSession = true)]
         public CascadingDropDownNameValue[] GetSourceOfRequest(string knownCategoryValues, string category)
         {

             var sourceOfRequestList = SourceOfRequestBAL.GetSourceOfRequestList().Where(s => s.CustomerID == sessionKeys.PortfolioID).ToList();

             var result = (from p in sourceOfRequestList
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

             return result;



         }

         [WebMethod]
         public CascadingDropDownNameValue[] GetTaskCategory(string knownCategoryValues, string category)
         {
             using (DCDataContext db = new DCDataContext())
             {
                 var taskCategoryList = db.TaskCategories.ToList().OrderBy(t => t.Name);


                 var result = (from p in taskCategoryList
                               select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

                 return result;
             }


         }
         [WebMethod]
         public CascadingDropDownNameValue[] GetTaskSubCategory(string knownCategoryValues, string category)
         {
             using (DCDataContext db = new DCDataContext())
             {
                 var taskSubCategoryList = db.TaskSubCategories.ToList().OrderBy(t => t.Name);


                 var result = (from p in taskSubCategoryList
                               select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

                 return result;
             }


         }
         [WebMethod(EnableSession=true)]
         public CascadingDropDownNameValue[] GetRequestType(string knownCategoryValues, string category)
         {

             var typeOfRequest = TypeOfRequestBAL.GetTypeOfRequestList().Where(t => t.CustomerID == sessionKeys.PortfolioID);

             var result = (from p in typeOfRequest
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

             return result;



         }
         [WebMethod(EnableSession = true)]
         public CascadingDropDownNameValue[] GetRequestTypeByCustomer(string knownCategoryValues, string category)
         {

             string[] _catgoryValue = knownCategoryValues.Split(':', ';');
             string customerId = (_catgoryValue[1]);
             var typeOfRequest = TypeOfRequestBAL.GetTypeOfRequestList().Where(t => t.CustomerID == Convert.ToInt32(customerId));

             var result = (from p in typeOfRequest
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

             return result;



         }
         [WebMethod(EnableSession = true)]
         public CascadingDropDownNameValue[] GetRequestTypeByCustomerSession(string knownCategoryValues, string category)
         {

             var typeOfRequest = TypeOfRequestBAL.GetTypeOfRequestList().Where(t => t.CustomerID == sessionKeys.PortfolioID);

             var result = (from p in typeOfRequest
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

             return result;



         }
        [WebMethod]
        public CascadingDropDownNameValue[] GetSubCategoryAdmin(string knownCategoryValues, string category)
        {
            string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            string categoryId = "0";
            if (_catgoryValue.Length > 3)
            {
                categoryId = (_catgoryValue[3]);
            }
            var subCategoyList = SubCategoryBAL.GetSubCategoryList().Where(c => c.CategoryID == Convert.ToInt32(categoryId)).ToList();

            var result = (from p in subCategoyList
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

            return result;



        }
        [WebMethod]
         public CascadingDropDownNameValue[] GetSubCategory(string knownCategoryValues, string category)
         {
             string[] _catgoryValue = knownCategoryValues.Split(':', ';');
             string  categoryId = "0";
             if (_catgoryValue.Length > 0)
             {
                 categoryId = (_catgoryValue[1]);
             }
             var subCategoyList = SubCategoryBAL.GetSubCategoryList().Where(c => c.CategoryID == Convert.ToInt32(categoryId)).ToList();

             var result = (from p in subCategoyList
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

             return result;



         }
        [WebMethod(EnableSession = true)]
        //[System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] GetMaterialItemsByVendor(string knownCategoryValues, string category)
        {
            string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            var vendor = "0";
            string categoryId = "0";
            if (_catgoryValue.Length > 3)
            {
                categoryId = (_catgoryValue[3]);
            }
            IPortfolioRepository<PortfolioMgt.Entity.ServiceCatelog_Material> mRep = new PortfolioRepository<PortfolioMgt.Entity.ServiceCatelog_Material>();

            var subCategoyList = mRep.GetAll().Where(o => o.SubCategory == Convert.ToInt32(categoryId) && o.Supplier == sessionKeys.VendorID).ToList();

            var result = (from p in subCategoyList
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.ItemDescription }).ToArray();

            return result;



        }
        [WebMethod(EnableSession = true)]
        //[System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] GetMaterialItems(string knownCategoryValues, string category)
        {
            string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            string categoryId = "0";
            if (_catgoryValue.Length > 3)
            {
                categoryId = (_catgoryValue[3]);
            }
            IPortfolioRepository<PortfolioMgt.Entity.ServiceCatelog_Material> mRep = new PortfolioRepository<PortfolioMgt.Entity.ServiceCatelog_Material>();

            var subCategoyList = mRep.GetAll().Where(o => o.SubCategory == Convert.ToInt32(categoryId) && o.Supplier == sessionKeys.VendorID).ToList();

            var result = (from p in subCategoyList
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.ItemDescription }).ToArray();

            return result;



        }
        [WebMethod]
         public CascadingDropDownNameValue[] GetSubCategoryTypeofRequestFault(string knownCategoryValues, string category)
         {
             string[] _catgoryValue = knownCategoryValues.Split(':', ';');
             string categoryId = "0";
             if (_catgoryValue.Length > 1)
             {
                 categoryId = (_catgoryValue[1]);
             }
             var subCategoyList = SubCategoryBAL.GetSubCategoryList().Where(c => c.CategoryID == Convert.ToInt32(categoryId)).ToList();

             var result = (from p in subCategoyList
                           select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

             return result;



         }
         [WebMethod]
         public CascadingDropDownNameValue[] GetModel(string knownCategoryValues, string category)
         {
             string[] _catgoryValue = knownCategoryValues.Split(':', ';');
             string subcategoryId = "0";
             if (_catgoryValue.Length > 5)
             {
                 subcategoryId = (_catgoryValue[5]);
             }
             IDCRespository<DC.Entity.ProductModel> mRepository = new DCRepository<DC.Entity.ProductModel>();
             var mData = mRepository.GetAll().Where(o => o.SubCategoryID == int.Parse(subcategoryId)).ToList();
             //var subCategoyList = SubCategoryBAL.GetSubCategoryList().Where(c => c.CategoryID == Convert.ToInt32(categoryId)).ToList();

             var result = (from p in mData
                           select new CascadingDropDownNameValue { value = p.ModelID.ToString(), name = p.ModelName }).ToArray();

             return result;



         }
         [WebMethod]
         public void InsertSDColumnPosition(string value)
         {
             int ab = value.Length - 1;
             Array a = value.Split(',');

             int Index = 0;
             foreach (var x in a)
             {
                 //x !=""
                 if (x != string.Empty)
                 {
                     int i = int.Parse(x.ToString());

                     using (DCDataContext dc = new DCDataContext())
                     {

                         DisplayColumnsByUser y = (from c in dc.DisplayColumnsByUsers where c.ID == i select c).FirstOrDefault();
                         y.Position = Index;
                         Index = Index + 1;
                         dc.SubmitChanges();
                     }
                 }
             }
         }

         #region Field position

         //[WebMethod]
         //public void InsertSDColumnPosition(string value)
         //{
         //    int ab = value.Length - 1;
         //    Array a = value.Split(',');

         //    int Index = 0;
         //    foreach (var x in a)
         //    {
         //        //x !=""
         //        if (x != string.Empty)
         //        {
         //            int i = int.Parse(x.ToString());

         //            using (DCDataContext dc = new DCDataContext())
         //            {

         //                DisplayColumnsByUser y = (from c in dc.DisplayColumnsByUsers where c.ID == i select c).FirstOrDefault();
         //                y.Position = Index;
         //                Index = Index + 1;
         //                dc.SubmitChanges();
         //            }
         //        }
         //    }
         //}

         [WebMethod]
         public void InsertFieldsPosition(string value)
         {
             try
             {
                 int ab = value.Length - 1;
                 Array a = value.Replace("\"","").Split(',');

                 int Index = 1;
                 foreach (var x in a)
                 {
                     //x !=""
                     if (x != string.Empty)
                     {
                         int i = int.Parse(x.ToString());

                         using (DCDataContext dc = new DCDataContext())
                         {

                             FLSFieldsConfig y = (from c in dc.FLSFieldsConfigs where c.ID == i select c).FirstOrDefault();
                             y.Position = Index;
                             Index = Index + 1;
                             dc.SubmitChanges();
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
             }
         }

         #endregion

         #region Dx Chart methods
         [WebMethod]
         public object GetChartData1()
         {
             int sts1 = 22;
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             DateTime? FDate = null;
             DateTime? Tdate = null;
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {

                         var clist = Udc.Contractors.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();

                         DateTime date = DateTime.Now;
                         DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                         int offset = fdow - date.DayOfWeek;
                         FDate = date.AddDays(offset);

                         Tdate = FDate.Value.AddDays(6);

                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1 &&
                                      (b.LoggedDate.Value.Date >= FDate.Value.Date && b.LoggedDate.Value.Date <= Tdate.Value.Date)//new status
                                      select new
                                      {
                                          a.UserID,
                                          b.StatusID
                                      }).ToList();
                         var listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();
                         var newList = new object();
                         foreach (var L in listOfTechnicians)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.UserID == L.value).Count();
                             Dlist.Name = L.name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }

             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetChartData2()
         {
             int sts = 35;
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             DateTime? FDate = null;
             DateTime? Tdate = null;
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {

                         var clist = Udc.Contractors.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();

                         DateTime date = DateTime.Now;
                         DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                         int offset = fdow - date.DayOfWeek;
                         FDate = date.AddDays(offset);
                         Tdate = FDate.Value.AddDays(6);


                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts &&
                                      (b.LoggedDate.Value.Date >= FDate.Value.Date && b.LoggedDate.Value.Date <= Tdate.Value.Date)//new status
                                      select new
                                      {
                                          a.UserID,
                                          b.StatusID
                                      }).ToList();
                         var listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();
                         var newList = new object();
                         foreach (var L in listOfTechnicians)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts && x.UserID == L.value).Count();
                             Dlist.Name = L.name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetChartData3()
         {
             int pending = 0;//pending
             int InHand = 27;
             int Completed = 35;
             int Resolved = 34;
             int sts1 = 22;//new status
             List<StatusList> DxchartFList = new List<StatusList>();
             StatusList Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

             DateTime? FDate = null;
             DateTime? Tdate = null;
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var clist = Udc.Contractors.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();

                         DateTime date = DateTime.Now;
                         DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                         int offset = fdow - date.DayOfWeek;
                         FDate = date.AddDays(offset);
                         Tdate = FDate.Value.AddDays(6);

                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value.Date >= FDate.Value.Date && b.LoggedDate.Value.Date <= Tdate.Value.Date)
                                      select new
                                      {
                                          a.UserID,
                                          b.StatusID
                                      }).ToList();
                         var listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();
                         var newList = new object();
                         foreach (var L in listOfTechnicians)
                         {
                             Dlist = new StatusList();
                             Dlist.Pending = rlist.Where(x => x.StatusID == pending && x.UserID == L.value).Count();
                             Dlist.InHand = rlist.Where(x => x.StatusID == InHand && x.UserID == L.value).Count();
                             Dlist.Completed = rlist.Where(x => x.StatusID == Completed && x.UserID == L.value).Count();
                             Dlist.Resolved = rlist.Where(x => x.StatusID == Resolved && x.UserID == L.value).Count();
                             Dlist.Name = L.name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }

             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetChartData4(string Cname)
         {
             int Completed = 35;

             DateTime? FDate = null;
             DateTime? Tdate = null;
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var clist = Udc.Contractors.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();

                         DateTime date = DateTime.Now;
                         DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                         int offset = fdow - date.DayOfWeek;
                         FDate = date.AddDays(offset);
                         Tdate = FDate.Value.AddDays(6);

                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      //where b.StatusID == 22//new status
                                      where (b.LoggedDate.Value.Date >= FDate.Value.Date && b.LoggedDate.Value.Date <= Tdate.Value.Date)
                                      select new
                                      {
                                          a.UserID,
                                          b.StatusID,
                                          b.SiteID
                                      }).ToList();
                         var listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();
                         var newList = new object();
                         var allcallList = (from a in cdlist
                                            join b in FlsList on a.ID equals b.CallID
                                            where a.StatusID == 35
                                            select a).ToList();
                         var sitelist = (from a in dc.OurSites where a.CustomerID == int.Parse(Cname) select a).ToList();//where a.CustomerID == sessionKeys.PortfolioID 

                         List<dynamic> li_data = new List<dynamic>();
                         dynamic dl_data = null;
                         foreach (var s in sitelist)
                         {
                             foreach (var L in listOfTechnicians)
                             {
                                 dl_data = new JObject();
                                 dl_data["state"] = s.Name;
                                 dl_data[L.name] = rlist.Where(x => x.StatusID == Completed && x.UserID == L.value
                                     && x.SiteID == s.ID).Count();
                                 li_data.Add(dl_data);
                             }
                         }
                         List<dynamic> li_series = new List<dynamic>();
                         dynamic dl_Series = null;
                         foreach (var L_tech in listOfTechnicians)
                         {
                             dl_Series = new JObject();
                             dl_Series["valueField"] = L_tech.name;
                             dl_Series["name"] = L_tech.name;
                             //string cname=Deff_color();
                             dl_Series["color"] = "#68b828";

                             li_series.Add(dl_Series);
                         }
                         var rtlist = new { a = li_data, b = li_series };
                         return JsonConvert.SerializeObject(rtlist);
                     }
                 }
             }
             catch (Exception ex)
             {
                 //return hc;
                 LogExceptions.WriteExceptionLog(ex);
                 return JsonConvert.SerializeObject(string.Empty).ToString();
             }
         }
         public string Deff_color()
         {
             var values = Guid.NewGuid().ToByteArray().Select(b => (int)b);
             int red = values.Take(5).Sum() % 255;
             int green = values.Skip(5).Take(5).Sum() % 255;
             int blue = values.Skip(10).Take(5).Sum() % 255;
             return Color.FromArgb(255, red, green, blue).Name;
         }
         [WebMethod]
         public object GetChartData1InBtnClick(string TechIds, string FromDate, string ToDate)
         {
             int sts1 = 22;//new status
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             DateTime? FDate = null;
             DateTime? Tdate = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var clist = Udc.Contractors.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();


                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                      select new { a.UserID, b.StatusID }).ToList();

                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                      && (b.LoggedDate >= FDate && b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate >= FDate)
                                      select new { a.UserID, b.StatusID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID }).ToList();
                         }

                         if (FromDate == string.Empty || ToDate == string.Empty)
                         {

                         }
                         var listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();
                         if (TechIds != string.Empty)
                         {
                             string[] NewTechIds = TechIds.Split(',').Reverse().Skip(1).ToArray();
                             listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  where NewTechIds.Contains(a.UserID.ToString())
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();
                         }
                         var newList = new object();
                         foreach (var L in listOfTechnicians)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.UserID == L.value).Count();
                             Dlist.Name = L.name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetChartData2InBtnClick(string TechIds, string FromDate, string ToDate)
         {
             int sts1 = 35;//new status
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             DateTime? FDate = null;
             DateTime? Tdate = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var clist = Udc.Contractors.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                      select new { a.UserID, b.StatusID }).ToList();
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate >= FDate && b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate >= FDate)
                                      select new { a.UserID, b.StatusID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID }).ToList();
                         }
                         var listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();
                         if (TechIds != string.Empty)
                         {
                             string[] NewTechIds = TechIds.Split(',').Reverse().Skip(1).ToArray();
                             listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  where NewTechIds.Contains(a.UserID.ToString())
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();
                         }
                         var newList = new object();
                         foreach (var L in listOfTechnicians)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.UserID == L.value).Count();
                             Dlist.Name = L.name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetChartData3InBtnClick(string TechIds, string FromDate, string ToDate)
         {
             int pending = 0;//pending
             int InHand = 27;
             int Completed = 35;
             int Resolved = 34;
             DateTime? FDate = null;
             DateTime? Tdate = null;
             List<StatusList> DxchartFList = new List<StatusList>();
             StatusList Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var clist = Udc.Contractors.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      select new { a.UserID, b.StatusID }).ToList();
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate >= FDate && b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.LoggedDate >= FDate
                                      select new { a.UserID, b.StatusID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.LoggedDate <= Tdate
                                      select new { a.UserID, b.StatusID }).ToList();
                         }



                         if (FromDate == string.Empty || ToDate == string.Empty)
                         {

                         }
                         var listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();
                         if (TechIds != string.Empty)
                         {
                             string[] NewTechIds = TechIds.Split(',').Reverse().Skip(1).ToArray();
                             listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  where NewTechIds.Contains(a.UserID.ToString())
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();
                         }
                         var newList = new object();
                         foreach (var L in listOfTechnicians)
                         {
                             Dlist = new StatusList();
                             Dlist.Pending = rlist.Where(x => x.StatusID == pending && x.UserID == L.value).Count();
                             Dlist.InHand = rlist.Where(x => x.StatusID == InHand && x.UserID == L.value).Count();
                             Dlist.Completed = rlist.Where(x => x.StatusID == Completed && x.UserID == L.value).Count();
                             Dlist.Resolved = rlist.Where(x => x.StatusID == Resolved && x.UserID == L.value).Count();
                             Dlist.Name = L.name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }
             }
             catch (Exception ex)
             {
                 //return hc;
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetChartData4InBtnClick(string TechIds, string FromDate, string ToDate, string Cname)
         {
             int Completed = 35;
             DateTime? FDate = null;
             DateTime? Tdate = null;
             List<StatusList> DxchartFList = new List<StatusList>();
             StatusList Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var clist = Udc.Contractors.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      //where b.StatusID == 22//new status
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();
                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate >= FDate && b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.LoggedDate >= FDate
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.LoggedDate <= Tdate
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();
                         }

                         var listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();

                         if (TechIds != string.Empty)
                         {
                             string[] NewTechIds = TechIds.Split(',').Reverse().Skip(1).ToArray();
                             listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  where NewTechIds.Contains(a.UserID.ToString())
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();
                         }

                         var allcallList = (from a in cdlist
                                            join b in FlsList on a.ID equals b.CallID
                                            where a.StatusID == Completed
                                            select a).ToList();
                         var sitelist = (from a in dc.OurSites where a.CustomerID == int.Parse(Cname) select a).ToList();//where condtion needed
                         List<dynamic> li_data = new List<dynamic>();
                         dynamic dl_data = null;
                         foreach (var s in sitelist)
                         {
                             foreach (var L in listOfTechnicians)
                             {
                                 dl_data = new JObject();
                                 dl_data["state"] = s.Name;
                                 dl_data[L.name] = rlist.Where(x => x.StatusID == Completed && x.UserID == L.value
                                     && x.SiteID == s.ID).Count();
                                 li_data.Add(dl_data);
                             }
                         }
                         List<dynamic> li_series = new List<dynamic>();
                         dynamic dl_Series = null;
                         foreach (var L_tech in listOfTechnicians)
                         {
                             dl_Series = new JObject();
                             dl_Series["valueField"] = L_tech.name;
                             dl_Series["name"] = L_tech.name;
                             //string cname=Deff_color();
                             dl_Series["color"] = "#68b828";

                             li_series.Add(dl_Series);
                         }
                         var rtlist = new { a = li_data, b = li_series };
                         return JsonConvert.SerializeObject(rtlist);
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetBarchartDataofSite1(string Cname)
         {
             int sts1 = 22;
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var slist = dc.OurSites.Where(a => a.CustomerID == int.Parse(Cname)).ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1//new status
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();

                         foreach (var L in slist)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.SiteID == L.ID).Count();
                             Dlist.Name = L.Name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }

             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetBarchartDataofSite2(string Cname)
         {
             int sts1 = 35;
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var slist = dc.OurSites.Where(a => a.CustomerID == int.Parse(Cname)).ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1//new status
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();

                         foreach (var L in slist)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.SiteID == L.ID).Count();
                             Dlist.Name = L.Name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }

             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetBarchartDataofSite3(string Cname)
         {
             int pending = 0;//pending
             int InHand = 27;
             int Completed = 35;
             int Resolved = 34;
             int sts1 = 22;//new status
             List<StatusList> DxchartFList = new List<StatusList>();
             StatusList Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var slist = dc.OurSites.Where(a => a.CustomerID == int.Parse(Cname)).ToList();
                         var clist = Udc.Contractors.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      select new { b.SiteID, b.StatusID }).ToList();
                         foreach (var L in slist)
                         {
                             Dlist = new StatusList();
                             Dlist.Pending = rlist.Where(x => x.StatusID == pending && x.SiteID == L.ID).Count();
                             Dlist.InHand = rlist.Where(x => x.StatusID == InHand && x.SiteID == L.ID).Count();
                             Dlist.Completed = rlist.Where(x => x.StatusID == Completed && x.SiteID == L.ID).Count();
                             Dlist.Resolved = rlist.Where(x => x.StatusID == Resolved && x.SiteID == L.ID).Count();
                             Dlist.Name = L.Name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }

             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }


         [WebMethod]
         public object GetDataInBtnClickSite1(string Cname, string FromDate, string ToDate)
         {
             int sts1 = 22;
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             DateTime? FDate = null;
             DateTime? Tdate = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var slist = dc.OurSites.Where(a => a.CustomerID == int.Parse(Cname)).ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1//new status
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();

                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                      && (b.LoggedDate >= FDate && b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate >= FDate)
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();
                         }

                         foreach (var L in slist)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.SiteID == L.ID).Count();
                             Dlist.Name = L.Name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }

             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetDataInBtnClickSite2(string Cname, string FromDate, string ToDate)
         {
             int sts1 = 35;
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             DateTime? FDate = null;
             DateTime? Tdate = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var slist = dc.OurSites.Where(a => a.CustomerID == int.Parse(Cname)).ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1//new status
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();
                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                      && (b.LoggedDate >= FDate && b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate >= FDate)
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID, b.SiteID }).ToList();
                         }
                         foreach (var L in slist)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.SiteID == L.ID).Count();
                             Dlist.Name = L.Name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }

             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetDataInBtnClickSite3(string Cname, string FromDate, string ToDate)
         {
             int pending = 0;//pending
             int InHand = 27;
             int Completed = 35;
             int Resolved = 34;
             int sts1 = 22;//new status
             DateTime? FDate = null;
             DateTime? Tdate = null;
             List<StatusList> DxchartFList = new List<StatusList>();
             StatusList Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var slist = dc.OurSites.Where(a => a.CustomerID == int.Parse(Cname)).ToList();
                         var clist = Udc.Contractors.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      select new { b.SiteID, b.StatusID }).ToList();

                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate >= FDate && b.LoggedDate <= Tdate)
                                      select new { b.SiteID, b.StatusID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate >= FDate)
                                      select new { b.SiteID, b.StatusID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate <= Tdate)
                                      select new { b.SiteID, b.StatusID }).ToList();
                         }
                         foreach (var L in slist)
                         {
                             Dlist = new StatusList();
                             Dlist.Pending = rlist.Where(x => x.StatusID == pending && x.SiteID == L.ID).Count();
                             Dlist.InHand = rlist.Where(x => x.StatusID == InHand && x.SiteID == L.ID).Count();
                             Dlist.Completed = rlist.Where(x => x.StatusID == Completed && x.SiteID == L.ID).Count();
                             Dlist.Resolved = rlist.Where(x => x.StatusID == Resolved && x.SiteID == L.ID).Count();
                             Dlist.Name = L.Name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }

             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }

         [WebMethod]
         public object GetBarchartDataofCategory1()
         {
             int sts1 = 22;
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             DateTime? FDate = null;
             DateTime? Tdate = null;
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var Catlist = dc.Categories.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         DateTime date = DateTime.Now;
                         DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                         int offset = fdow - date.DayOfWeek;
                         FDate = date.AddDays(offset);
                         Tdate = FDate.Value.AddDays(6);
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1 && (b.LoggedDate.Value.Date >= FDate.Value.Date && b.LoggedDate.Value.Date <= Tdate.Value.Date)
                                      //new status
                                      select new
                                      {
                                          a.UserID,
                                          b.StatusID,
                                          a.CategoryID
                                      }).ToList();

                         foreach (var L in Catlist)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.CategoryID == L.ID).Count();
                             Dlist.Name = L.Name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }

             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetBarchartDataofCategory2()
         {
             int sts1 = 35;
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             DateTime? FDate = null;
             DateTime? Tdate = null;
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var Catlist = dc.Categories.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         DateTime date = DateTime.Now;
                         DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                         int offset = fdow - date.DayOfWeek;
                         FDate = date.AddDays(offset);
                         Tdate = FDate.Value.AddDays(6);
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1 && (b.LoggedDate.Value.Date >= FDate.Value.Date && b.LoggedDate.Value.Date <= Tdate.Value.Date)//new status
                                      select new
                                      {
                                          a.UserID,
                                          b.StatusID,
                                          a.CategoryID
                                      }).ToList();

                         foreach (var L in Catlist)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.CategoryID == L.ID).Count();
                             Dlist.Name = L.Name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }

             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetBarchartDataofCategory3()
         {
             int pending = 0;//pending
             int InHand = 27;
             int Completed = 35;
             int Resolved = 34;
             int sts1 = 22;//new status
             List<StatusList> DxchartFList = new List<StatusList>();
             StatusList Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             DateTime? FDate = null;
             DateTime? Tdate = null;
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var Catlist = dc.Categories.ToList();
                         var clist = Udc.Contractors.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         DateTime date = DateTime.Now;
                         DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                         int offset = fdow - date.DayOfWeek;
                         FDate = date.AddDays(offset);
                         Tdate = FDate.Value.AddDays(6);
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value.Date >= FDate.Value.Date && b.LoggedDate.Value.Date <= Tdate.Value.Date)
                                      select new
                                      {
                                          b.SiteID,
                                          b.StatusID,
                                          a.CategoryID
                                      }).ToList();
                         foreach (var L in Catlist)
                         {
                             Dlist = new StatusList();
                             Dlist.Pending = rlist.Where(x => x.StatusID == pending && x.CategoryID == L.ID).Count();
                             Dlist.InHand = rlist.Where(x => x.StatusID == InHand && x.CategoryID == L.ID).Count();
                             Dlist.Completed = rlist.Where(x => x.StatusID == Completed && x.CategoryID == L.ID).Count();
                             Dlist.Resolved = rlist.Where(x => x.StatusID == Resolved && x.CategoryID == L.ID).Count();
                             Dlist.Name = L.Name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }

             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetChartData1InBtnClickCategory(string TechIds, string FromDate, string ToDate, string CustomerInCat, string RequestTypeInCat)
         {
           //  int TypeofRequest = 6;
             int sts1 = 22;//new status
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             DateTime? FDate = null;
             DateTime? Tdate = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var TypeOFReqs = dc.TypeOfRequests.ToList();
                         var Clist = dc.Categories.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                      select new
                                      {
                                          a.UserID,
                                          b.StatusID,
                                          a.CategoryID
                                      }).ToList();

                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                      && (b.LoggedDate >= FDate && b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID, a.CategoryID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate >= FDate)
                                      select new { a.UserID, b.StatusID, a.CategoryID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID, a.CategoryID }).ToList();
                         }

                         var Catlist = (from a in Clist
                                        select new
                                        {
                                            value = a.ID,
                                            name = a.Name,
                                            TypeReqId=a.TypeOfRequestID
                                        }).Distinct().ToList();
                         if (TechIds != string.Empty)
                         {
                             string[] NewTechIds = TechIds.Split(',').Reverse().Skip(1).ToArray();
                             Catlist = Catlist = (from a in Catlist
                                                  where NewTechIds.Contains(a.value.ToString())
                                                  select new
                                                  {
                                                      value = a.value,
                                                      name = a.name,
                                                      TypeReqId=a.TypeReqId
                                                  }).Distinct().ToList();
                         }
                         else if (CustomerInCat != "0" && RequestTypeInCat != "0")
                         {
                             Catlist = Catlist.Where(a => a.TypeReqId == int.Parse(RequestTypeInCat)).ToList();
                         }
                         else if (CustomerInCat != "0")
                         {
                             var TypeOFReqsArray = TypeOFReqs.Where(a => a.CustomerID == int.Parse(CustomerInCat)).Select(a => a.ID).ToArray();
                             Catlist = Catlist.Where(a => TypeOFReqsArray.Contains(a.TypeReqId.Value)).ToList();
                         }
                         var newList = new object();
                         foreach (var L in Catlist)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.CategoryID == L.value).Count();
                             Dlist.Name = L.name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetChartData2InBtnClickCategory(string TechIds, string FromDate, string ToDate, string CustomerInCat, string RequestTypeInCat)
         {
            // int TypeofRequest = 6;
             int sts1 = 35;//new status
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             DateTime? FDate = null;
             DateTime? Tdate = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var TypeOFReqs = dc.TypeOfRequests.ToList();
                         var Clist = dc.Categories.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                      select new { a.UserID, b.StatusID, a.CategoryID }).ToList();
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate >= FDate && b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID, a.CategoryID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate >= FDate)
                                      select new { a.UserID, b.StatusID, a.CategoryID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where b.StatusID == sts1
                                       && (b.LoggedDate.Value.Date <= FDate.Value.Date)
                                      select new { a.UserID, b.StatusID, a.CategoryID }).ToList();
                         }
                         var Catlist = (from a in Clist
                                        //&& a.TypeOfRequestID == TypeofRequest
                                        select new
                                        {
                                            value = a.ID,
                                            name = a.Name,
                                            TypeReqId = a.TypeOfRequestID
                                        }).Distinct().ToList();
                         if (TechIds != string.Empty)
                         {
                             string[] NewTechIds = TechIds.Split(',').Reverse().Skip(1).ToArray();
                             Catlist = Catlist = (from a in Catlist
                                                  where NewTechIds.Contains(a.value.ToString())
                                                  select new
                                                  {
                                                      value = a.value,
                                                      name = a.name,
                                                      TypeReqId = a.TypeReqId
                                                  }).Distinct().ToList();
                         }
                         else if (CustomerInCat != "0" && RequestTypeInCat != "0")
                         {
                             Catlist = Catlist.Where(a => a.TypeReqId == int.Parse(RequestTypeInCat)).ToList();
                         }
                         else if (CustomerInCat != "0")
                         {
                             var TypeOFReqsArray = TypeOFReqs.Where(a => a.CustomerID == int.Parse(CustomerInCat)).Select(a => a.ID).ToArray();
                             Catlist = Catlist.Where(a => TypeOFReqsArray.Contains(a.TypeReqId.Value)).ToList();
                         }

                         var newList = new object();
                         foreach (var L in Catlist)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.CategoryID == L.value).Count();
                             Dlist.Name = L.name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetChartData3InBtnClickCategory(string TechIds, string FromDate, string ToDate, string CustomerInCat, string RequestTypeInCat)
         {
             //int TypeofRequest = 6;
             int pending = 0;//pending
             int InHand = 27;
             int Completed = 35;
             int Resolved = 34;
             DateTime? FDate = null;
             DateTime? Tdate = null;
             List<StatusList> DxchartFList = new List<StatusList>();
             StatusList Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var TypeOFReqs = dc.TypeOfRequests.ToList();
                         var Clist = dc.Categories.ToList();
                         var FlsList = dc.FLSDetails.ToList();
                         var cdlist = dc.CallDetails.ToList();
                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      select new { a.UserID, b.StatusID, a.CategoryID }).ToList();
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate >= FDate && b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID, a.CategoryID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate >= FDate)
                                      select new { a.UserID, b.StatusID, a.CategoryID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate <= Tdate)
                                      select new { a.UserID, b.StatusID, a.CategoryID }).ToList();
                         }



                         if (FromDate == string.Empty || ToDate == string.Empty)
                         {

                         }
                         var Catlist = (from a in Clist
                                        select new
                                        {
                                            value = a.ID,
                                            name = a.Name,
                                            TypeReqId = a.TypeOfRequestID
                                        }).Distinct().ToList();
                         if (TechIds != string.Empty)
                         {
                             string[] NewTechIds = TechIds.Split(',').Reverse().Skip(1).ToArray();
                             Catlist = (from a in Catlist
                                        where NewTechIds.Contains(a.value.ToString())
                                        select new
                                        {
                                            value = a.value,
                                            name = a.name,
                                            TypeReqId = a.TypeReqId
                                        }).Distinct().ToList();
                         }
                         else if (CustomerInCat != "0" && RequestTypeInCat != "0")
                         {
                             Catlist = Catlist.Where(a => a.TypeReqId == int.Parse(RequestTypeInCat)).ToList();
                         }
                         else if (CustomerInCat != "0")
                         {
                             var TypeOFReqsArray = TypeOFReqs.Where(a => a.CustomerID == int.Parse(CustomerInCat)).Select(a => a.ID).ToArray();
                             Catlist = Catlist.Where(a => TypeOFReqsArray.Contains(a.TypeReqId.Value)).ToList();
                         }

                         var newList = new object();
                         foreach (var L in Catlist)
                         {
                             Dlist = new StatusList();
                             Dlist.Pending = rlist.Where(x => x.StatusID == pending && x.CategoryID == L.value).Count();
                             Dlist.InHand = rlist.Where(x => x.StatusID == InHand && x.CategoryID == L.value).Count();
                             Dlist.Completed = rlist.Where(x => x.StatusID == Completed && x.CategoryID == L.value).Count();
                             Dlist.Resolved = rlist.Where(x => x.StatusID == Resolved && x.CategoryID == L.value).Count();
                             Dlist.Name = L.name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetDataOfRevenueByCategory()
         {
             int StsCompleted = 35;
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (DCDataContext dc = new DCDataContext())
                 {
                     var Catlist = dc.Categories.ToList();
                     var ViewList = dc.V_ServiceDeskPrices.ToList();
                     foreach (var L in Catlist)
                     {
                         Dlist = new DxchartF();
                         Dlist.Value = int.Parse(ViewList.Where(x => x.StatusID == StsCompleted && x.CategoryID == L.ID).Sum(x => x.Price).ToString());
                         Dlist.Name = L.Name;
                         DxchartFList.Add(Dlist);
                     }
                     return jsonSerializer.Serialize(DxchartFList).ToString();
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetDataOfRevenueByCategoryInBtnClick(string Cname, string FromDate, string ToDate)
         {
             int StsCompleted = 35;
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             DateTime? FDate = null;
             DateTime? Tdate = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (DCDataContext dc = new DCDataContext())
                 {
                     var TypeOfRequests = dc.TypeOfRequests.Where(a => a.CustomerID == int.Parse(Cname)).ToList();
                     var Catlist = dc.Categories.Where(a => TypeOfRequests.Select(b => b.ID).Contains(a.TypeOfRequestID.Value)).ToList();
                     var ViewList = dc.V_ServiceDeskPrices.ToList();
                     if (FromDate != string.Empty)
                     {
                         FDate = DateTime.Parse(FromDate);
                     }
                     if (ToDate != string.Empty)
                     {
                         Tdate = DateTime.Parse(ToDate);
                     }
                     if (FromDate != string.Empty && ToDate != string.Empty)
                     {
                         ViewList = (from a in ViewList
                                     where a.LoggedDate >= FDate && a.LoggedDate <= Tdate
                                     select a).ToList();
                     }
                     else if (FromDate != string.Empty)
                     {
                         ViewList = (from a in ViewList
                                     where a.LoggedDate >= FDate
                                     select a).ToList();
                     }
                     else if (ToDate != string.Empty)
                     {

                         ViewList = (from a in ViewList
                                     where a.LoggedDate <= Tdate
                                     select a).ToList();
                     }
                     foreach (var L in Catlist)
                     {
                         Dlist = new DxchartF();
                         Dlist.Value = int.Parse(ViewList.Where(x => x.StatusID == StsCompleted && x.CategoryID == L.ID).Sum(x => x.Price).ToString());
                         Dlist.Name = L.Name;
                         DxchartFList.Add(Dlist);
                     }
                     return jsonSerializer.Serialize(DxchartFList).ToString();
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetDataOfRevenueBySite(string Cname)
         {
             int StsCompleted = 35;
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (DCDataContext dc = new DCDataContext())
                 {
                     var slist = dc.OurSites.Where(a => a.CustomerID == int.Parse(Cname)).ToList();
                     var ViewList = dc.V_ServiceDeskPrices.ToList();
                     foreach (var L in slist)
                     {
                         Dlist = new DxchartF();
                         Dlist.Value = int.Parse(ViewList.Where(x => x.StatusID == StsCompleted && x.SiteID == L.ID).Sum(x => x.Price).ToString());
                         Dlist.Name = L.Name;
                         DxchartFList.Add(Dlist);
                     }
                     return jsonSerializer.Serialize(DxchartFList).ToString();
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetDataOfRevenueBySiteInBtnClick(string Cname, string FromDate, string ToDate)
         {
             int StsCompleted = 35;
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             DateTime? FDate = null;
             DateTime? Tdate = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (DCDataContext dc = new DCDataContext())
                 {
                     var slist = dc.OurSites.Where(a => a.CustomerID == int.Parse(Cname)).ToList();
                     var ViewList = dc.V_ServiceDeskPrices.ToList();
                     if (FromDate != string.Empty)
                     {
                         FDate = DateTime.Parse(FromDate);
                     }
                     if (ToDate != string.Empty)
                     {
                         Tdate = DateTime.Parse(ToDate);
                     }
                     if (FromDate != string.Empty && ToDate != string.Empty)
                     {
                         ViewList = (from a in ViewList
                                     where a.LoggedDate >= FDate && a.LoggedDate <= Tdate
                                     select a).ToList();
                     }
                     else if (FromDate != string.Empty)
                     {
                         ViewList = (from a in ViewList
                                     where a.LoggedDate >= FDate
                                     select a).ToList();
                     }
                     else if (ToDate != string.Empty)
                     {
                         ViewList = (from a in ViewList
                                     where a.LoggedDate <= Tdate
                                     select a).ToList();
                     }


                     foreach (var L in slist)
                     {
                         Dlist = new DxchartF();
                         Dlist.Value = int.Parse(ViewList.Where(x => x.StatusID == StsCompleted && x.SiteID == L.ID).Sum(x => x.Price).ToString());
                         Dlist.Name = L.Name;
                         DxchartFList.Add(Dlist);
                     }
                     return jsonSerializer.Serialize(DxchartFList).ToString();
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }


         [WebMethod]
         public object GetdataOfCustomerPriorityBtnClick(string Cname, string FromDate, string ToDate)
         {
             int StsCompleted = 35;
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             DateTime? FDate = null;
             DateTime? Tdate = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (DCDataContext dc = new DCDataContext())
                 {
                     var slist = dc.OurSites.Where(a => a.CustomerID == int.Parse(Cname)).ToList();
                     var ViewList = dc.V_ServiceDeskPrices.ToList();
                     if (FromDate != string.Empty)
                     {
                         FDate = DateTime.Parse(FromDate);
                     }
                     if (ToDate != string.Empty)
                     {
                         Tdate = DateTime.Parse(ToDate);
                     }
                     if (FromDate != string.Empty && ToDate != string.Empty)
                     {
                         ViewList = (from a in ViewList
                                     where a.LoggedDate.Value.Date >= FDate && a.LoggedDate.Value.Date <= Tdate
                                     select a).ToList();
                     }
                     else if (FromDate != string.Empty)
                     {
                         ViewList = (from a in ViewList
                                     where a.LoggedDate.Value.Date >= FDate
                                     select a).ToList();
                     }
                     else if (ToDate != "")
                     {
                         ViewList = (from a in ViewList
                                     where a.LoggedDate.Value.Date <= Tdate
                                     select a).ToList();
                     }


                     foreach (var L in slist)
                     {
                         Dlist = new DxchartF();
                         Dlist.Value = int.Parse(ViewList.Where(x => x.StatusID == StsCompleted && x.SiteID == L.ID).Sum(x => x.Price).ToString());
                         Dlist.Name = L.Name;
                         DxchartFList.Add(Dlist);
                     }
                     return jsonSerializer.Serialize(DxchartFList).ToString();
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetChartCustomerCategoryBtnClick(string Cname, string Rtype, string FromDate, string ToDate)
         {
             int sts1 = 22;//new status
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             DateTime? FDate = null;
             DateTime? Tdate = null;

             //string[] TechIdsArray = TechIds.Split(',');

             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var cdlist = dc.CallDetails.Where(p => p.CompanyID == Convert.ToInt32(Cname) && p.RequestTypeID == 6).ToList();
                         //var clist = Udc.Contractors.ToList();
                         var FlsList = dc.FLSDetails.Where(o => cdlist.Select(p => p.ID).ToArray().Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
                         var CategoryList = dc.Categories.Where(o => o.TypeOfRequestID == Convert.ToInt32(Rtype)).ToList();


                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      // where b.StatusID == sts1
                                      select new { a.CategoryID, b.StatusID }).ToList();

                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value.Date >= FDate && b.LoggedDate.Value.Date <= Tdate)
                                      select new { a.CategoryID, b.StatusID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value.Date >= FDate)
                                      select new { a.CategoryID, b.StatusID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value <= Tdate)
                                      select new { a.CategoryID, b.StatusID }).ToList();
                         }

                         if (FromDate == string.Empty || ToDate == string.Empty)
                         {

                         }

                         //var listOfTechnicians = (from a in FlsList
                         //                         join b in clist on a.UserID equals b.ID
                         //                         select new
                         //                         {
                         //                             value = b.ID,
                         //                             name = b.ContractorName
                         //                         }).Distinct().ToList();
                         //if (TechIds == string.Empty)
                         //{
                         //    listOfTechnicians = (from a in FlsList
                         //                         join b in clist on a.UserID equals b.ID
                         //                         select new
                         //                         {
                         //                             value = b.ID,
                         //                             name = b.ContractorName
                         //                         }).Distinct().ToList();
                         //}
                         var newList = new object();
                         foreach (var L in CategoryList)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.CategoryID == L.ID).Count();
                             Dlist.Name = L.Name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetChartCustomerEngineerBtnClick(string Cname, string Rtype, string FromDate, string ToDate)
         {
             int sts1 = 22;//new status
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             DateTime? FDate = null;
             DateTime? Tdate = null;

             //string[] TechIdsArray = TechIds.Split(',');

             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var cdlist = dc.CallDetails.Where(p => p.CompanyID == Convert.ToInt32(Cname) && p.RequestTypeID == 6).ToList();
                         var FlsList = dc.FLSDetails.Where(o => (o.RequestType.HasValue ? o.RequestType.Value : 0) == Convert.ToInt32(Rtype) && cdlist.Select(p => p.ID).ToArray().Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
                         var clist = Udc.Contractors.Where(o => FlsList.Select(p => p.UserID).ToArray().Contains(o.ID)).ToList();

                         //var CategoryList = dc.Categories.Where(o => o.TypeOfRequestID == Convert.ToInt32(Rtype)).ToList();


                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      // where b.StatusID == sts1
                                      select new { a.UserID, b.StatusID }).ToList();

                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value.Date >= FDate && b.LoggedDate.Value.Date <= Tdate)
                                      select new { a.UserID, b.StatusID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value.Date >= FDate)
                                      select new { a.UserID, b.StatusID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value.Date <= Tdate)
                                      select new { a.UserID, b.StatusID }).ToList();
                         }

                         if (FromDate == string.Empty || ToDate == string.Empty)
                         {

                         }

                         var listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();
                         if (Cname == string.Empty)
                         {
                             listOfTechnicians = (from a in FlsList
                                                  join b in clist on a.UserID equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.ContractorName
                                                  }).Distinct().ToList();
                         }
                         var newList = new object();
                         foreach (var L in listOfTechnicians)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == sts1 && x.UserID == L.value).Count();
                             Dlist.Name = L.name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
         [WebMethod]
         public object GetDataCustomerPiechartBtnClick(string Cname, string Rtype, string FromDate, string ToDate)
         {
             int sts1 = 22;//new status
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             DateTime? FDate = null;
             DateTime? Tdate = null;

             //string[] TechIdsArray = TechIds.Split(',');

             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var cdlist = dc.CallDetails.Where(p => p.CompanyID == Convert.ToInt32(Cname) && p.RequestTypeID == 6).ToList();
                         var FlsList = dc.FLSDetails.Where(o => (o.RequestType.HasValue ? o.RequestType.Value : 0) == Convert.ToInt32(Rtype) && cdlist.Select(p => p.ID).ToArray().Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
                         //var clist = Udc.Contractors.Where(o => FlsList.Select(p => p.UserID).ToArray().Contains(o.ID)).ToList();

                         var StatusList = dc.Status.Where(o => o.RequestTypeID == 6).ToList();


                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      // where b.StatusID == sts1
                                      select new { a.RequestType, b.StatusID }).ToList();

                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value.Date >= FDate && b.LoggedDate.Value.Date <= Tdate)
                                      select new { a.RequestType, b.StatusID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value.Date >= FDate)
                                      select new { a.RequestType, b.StatusID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value.Date <= Tdate)
                                      select new { a.RequestType, b.StatusID }).ToList();
                         }

                         if (FromDate == string.Empty || ToDate == string.Empty)
                         {

                         }

                         var listOfTechnicians = (from a in cdlist
                                                  join b in StatusList on a.StatusID equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.Name
                                                  }).Distinct().ToList();
                         if (Cname == string.Empty)
                         {
                             listOfTechnicians = (from a in cdlist
                                                  join b in StatusList on a.StatusID equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.Name
                                                  }).Distinct().ToList();
                         }
                         var newList = new object();
                         foreach (var L in listOfTechnicians)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.StatusID == L.value).Count();
                             Dlist.Name = L.name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }

         [WebMethod]
         public object GetDataCustomerTypeBtnClick(string Cname, string Rtype, string FromDate, string ToDate)
         {
             int sts1 = 22;//new status
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             DateTime? FDate = null;
             DateTime? Tdate = null;

             //string[] TechIdsArray = TechIds.Split(',');

             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (UserDataContext Udc = new UserDataContext())
                 {
                     using (DCDataContext dc = new DCDataContext())
                     {
                         var cdlist = dc.CallDetails.Where(p => p.CompanyID == Convert.ToInt32(Cname) && p.RequestTypeID == 6).ToList();
                         var FlsList = dc.FLSDetails.Where(o => cdlist.Select(p => p.ID).ToArray().Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
                         //var clist = Udc.Contractors.Where(o => FlsList.Select(p => p.UserID).ToArray().Contains(o.ID)).ToList();

                         var typeList = dc.TypeOfRequests.Where(o => o.CustomerID == Convert.ToInt32(Cname)).ToList();


                         var rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      // where b.StatusID == sts1
                                      select new { a.RequestType, b.StatusID }).ToList();

                         if (FromDate != string.Empty)
                         {
                             FDate = DateTime.Parse(FromDate);
                         }
                         if (ToDate != string.Empty)
                         {
                             Tdate = DateTime.Parse(ToDate);
                         }
                         if (FromDate != string.Empty && ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value.Date >= FDate && b.LoggedDate.Value.Date <= Tdate)
                                      select new { a.RequestType, b.StatusID }).ToList();
                         }
                         else if (FromDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value.Date >= FDate)
                                      select new { a.RequestType, b.StatusID }).ToList();
                         }
                         else if (ToDate != string.Empty)
                         {
                             rlist = (from a in FlsList
                                      join b in cdlist on a.CallID equals b.ID
                                      where (b.LoggedDate.Value.Date <= Tdate)
                                      select new { a.RequestType, b.StatusID }).ToList();
                         }

                         if (FromDate == string.Empty || ToDate == string.Empty)
                         {

                         }

                         var listOfTechnicians = (from a in FlsList
                                                  join b in typeList on a.RequestType equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.Name
                                                  }).Distinct().ToList();
                         if (Cname == string.Empty)
                         {
                             listOfTechnicians = (from a in FlsList
                                                  join b in typeList on a.RequestType equals b.ID
                                                  select new
                                                  {
                                                      value = b.ID,
                                                      name = b.Name
                                                  }).Distinct().ToList();
                         }
                         var newList = new object();
                         foreach (var L in listOfTechnicians)
                         {
                             Dlist = new DxchartF();
                             Dlist.Value = rlist.Where(x => x.RequestType == L.value).Count();
                             Dlist.Name = L.name;
                             DxchartFList.Add(Dlist);
                         }
                         return jsonSerializer.Serialize(DxchartFList).ToString();
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }


         #endregion


         #region DC dashboard
         [WebMethod(EnableSession = true)]
         [System.Web.Script.Services.ScriptMethod]
         public Array GetCallStatusCount(string towns, string postcodes)
         {
             string Counts = string.Empty;

             //int WithInonTime = 0;
             //int DelayedTasks = 0;
             //int TaskDueWithIn3Days = 0;

             //try
             //{
                 //IDCRespository<CallDetail> cRepository = new DCRepository<CallDetail>();
                 //IDCRespository<Status> sRepository = new DCRepository<Status>();

                 //var clist = cRepository.GetAll().Where(o => o.RequestTypeID == 6);
                 //var slist = sRepository.GetAll().Where(o => o.RequestTypeID == 6);
                 List<Jqgrid> flsList = new List<Jqgrid>();
                 flsList = DC.BLL.FLSDetailsBAL.Jqgridlist();
                 if (towns.Length > 0)
                 {

                     var t = towns.Split(',').ToList().Where(o => o != string.Empty).ToArray();
                     if (t.Count() > 0)
                         flsList = flsList.Where(o => t.Contains(o.RequestersTown)).ToList();

                 }
                 if (postcodes.Length > 0)
                 {

                     var t = postcodes.Split(',').ToList().Where(o => o != string.Empty).ToArray();
                     if (t.Count() > 0)
                         flsList = flsList.Where(o => t.Contains(o.RequestersPostCode)).ToList();

                 }
                 string[] sArray = new string[] { "Cancelled" };
                 var result = (from c in flsList
                               //join s in slist on c.StatusID equals s.ID
                               where c.Status != null && !sArray.Contains(c.Status)
                               group c by new { c.Status} into g
                               select new
                               {
                                   status = g.Key.Status,
                                   count = g.Count()
                               }).ToList();



                 return result.Select(o => o.count).ToArray();// Counts.Split(',').ToArray();
             //}
             //catch(Exception ex)
             //{ LogExceptions.WriteExceptionLog(ex); }
         }

       

         [WebMethod(EnableSession = true)]
         [System.Web.Script.Services.ScriptMethod]
         public object GetUnassignedCalls(string towns, string postcodes)
         {
             //List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

             try
             {
                 List<Jqgrid> flsList = new List<Jqgrid>();
                 flsList = DC.BLL.FLSDetailsBAL.Jqgridlist();
                 if (towns.Length > 0)
                 {

                     var t = towns.Split(',').ToList().Where(o => o != string.Empty).ToArray();
                     if (t.Count() > 0)
                         flsList = flsList.Where(o => t.Contains(o.RequestersTown)).ToList();

                 }
                 if (postcodes.Length > 0)
                 {

                     var t = postcodes.Split(',').ToList().Where(o => o != string.Empty).ToArray();
                     if (t.Count() > 0)
                         flsList = flsList.Where(o => t.Contains(o.RequestersPostCode)).ToList();

                 }
                 string[] sArray = new string[] { "Cancelled" };

                 var result = (from c in flsList
                               where c.RequestersTown != null && c.RequestersTown.Length > 0 && !sArray.Contains(c.Status)
                               group c by new { c.RequestersTown } into g
                               select new DxchartF
                               {
                                   Name = g.Key.RequestersTown,
                                   Value = g.Count()
                               }).ToList();

                 return jsonSerializer.Serialize(result).ToString();
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
        #endregion

         #region Project Home
         [WebMethod(EnableSession = true)]
         [System.Web.Script.Services.ScriptMethod]
         public Array GetTasksData()
         {
             string Counts = string.Empty;

             int WithInonTime = 0;
             int DelayedTasks = 0;
             int TaskDueWithIn3Days = 0;

             try
             {
                 using (projectTaskDataContext Pdc = new projectTaskDataContext())
                 {
                     var ProjectsList = Pdc.Projects.Where(a => a.ProjectStatusID == 2).ToList();
                     var ProjectItemsLsit = Pdc.ProjectItems.Where(a => a.ContractorID.Value == sessionKeys.UID).ToList();

                     if (sessionKeys.PortfolioID > 0)
                     {
                         ProjectsList = ProjectsList.Where(a => a.Portfolio == sessionKeys.PortfolioID).ToList();
                     }
                     var ProjectsList1 = ProjectsList.Select(a => a.ProjectReference).ToArray();

                     foreach (var x in ProjectItemsLsit)
                     {
                         var ProjectTaskItemsRecord = Pdc.ProjectTaskItems.Where(a => a.ID == x.ItemReference).FirstOrDefault();
                         if (ProjectTaskItemsRecord != null)
                         {
                             if (ProjectsList1.Contains(ProjectTaskItemsRecord.ProjectReference.Value))
                             {
                                 //if (ProjectTaskItemsRecord.ProjectEndDate.Value.Date >= DateTime.Now.Date)
                                 if (ProjectTaskItemsRecord.CompletionDate.Value.Date >= DateTime.Now.Date)
                                 {
                                     WithInonTime = WithInonTime + 1;
                                 }
                                 //else if (ProjectTaskItemsRecord.ProjectEndDate.Value.Date.AddDays(3) >= DateTime.Now.Date)
                                 else if (ProjectTaskItemsRecord.CompletionDate.Value.Date.AddDays(3) >= DateTime.Now.Date)
                                 {
                                     TaskDueWithIn3Days = TaskDueWithIn3Days + 1;
                                 }
                                 else
                                 {
                                     DelayedTasks = DelayedTasks + 1;
                                 }
                             }
                         }
                     }
                 }
                 if (DelayedTasks != 0 && TaskDueWithIn3Days != 0 && WithInonTime != 0)
                 {
                     Counts = "-" + DelayedTasks.ToString() + ",-" + TaskDueWithIn3Days.ToString() + "," + WithInonTime.ToString();
                 }
                 else
                 {
                     if (DelayedTasks != 0)
                     {
                         Counts = "-" + DelayedTasks.ToString();
                     }
                     else
                     {
                         Counts = DelayedTasks.ToString();
                     }
                     if (TaskDueWithIn3Days != 0)
                     {
                         Counts = Counts + ",-" + TaskDueWithIn3Days.ToString();
                     }
                     else
                     {
                         Counts = Counts + "," + TaskDueWithIn3Days.ToString();
                     }
                     if (WithInonTime != 0)
                     {
                         Counts = Counts + "," + WithInonTime.ToString();
                     }
                     else
                     {
                         Counts = Counts + "," + WithInonTime.ToString();
                     }
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
             }
             return Counts.Split(',').ToArray();
         }


         [WebMethod(EnableSession = true)]
         [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
         public object AddSessionValues(string Cid, string text)
         {
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 sessionKeys.PortfolioID = int.Parse(Cid);
                 sessionKeys.PortfolioName = text;
                 return jsonSerializer.Serialize("Session updated succussfully").ToString();
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }
        

         [WebMethod(EnableSession = true)]
         [System.Web.Script.Services.ScriptMethod]
         public object GetIssuesData()
         {
             List<DxchartF> DxchartFList = new List<DxchartF>();
             DxchartF Dlist = null;
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             try
             {
                 using (projectTaskDataContext Pdc = new projectTaskDataContext())
                 {
                     var listOfCategoryTypes = Pdc.IssueTypes.ToList();
                     var ProjectIssuesList = Pdc.ProjectIssues.Where(a => a.AssignTo == sessionKeys.UID).ToList();

                     if (sessionKeys.PortfolioID > 0)
                     {
                         var ProjectIds = Pdc.Projects.Where(a => a.Portfolio == sessionKeys.PortfolioID).Select(a => a.ProjectReference).ToList();
                         ProjectIssuesList = ProjectIssuesList.Where(a => ProjectIds.Contains(a.Projectreference.HasValue ? a.Projectreference.Value : 0)).ToList();
                     }

                     var newList = new object();

                     foreach (var L in listOfCategoryTypes)
                     {
                         Dlist = new DxchartF();
                         Dlist.Value = ProjectIssuesList.Where(x => x.IssuseType == L.ID).Count();
                         Dlist.Name = L.IssueTypeName;
                         DxchartFList.Add(Dlist);
                     }
                     return jsonSerializer.Serialize(DxchartFList).ToString();
                 }
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return jsonSerializer.Serialize(string.Empty).ToString();
             }
         }


        #endregion 

        #region Get vendor catalog items
         [WebMethod]
         [System.Web.Script.Services.ScriptMethod]
         public string[] GetVendorItems(string prefix)
         {
             List<string> customers = new List<string>();
             IPortfolioRepository<PortfolioMgt.Entity.v_ShopItems_vendor> ivendor = new PortfolioRepository<PortfolioMgt.Entity.v_ShopItems_vendor>();
             var getlist = ivendor.GetAll().Where(o => o.Description.Contains(prefix)).ToList();

             if (getlist != null)
             {
                 foreach(var v in getlist)
                 {
                     customers.Add(string.Format("{0}-{1}", v.Description, v.ID));
                 }
             }
             return customers.ToArray();
         }
         [WebMethod]
         [System.Web.Script.Services.ScriptMethod]
         public List<PortfolioMgt.Entity.v_ShopItems_vendor> GetVendorItems()
         {
             List<string> customers = new List<string>();
             IPortfolioRepository<PortfolioMgt.Entity.v_ShopItems_vendor> ivendor = new PortfolioRepository<PortfolioMgt.Entity.v_ShopItems_vendor>();
             var getlist = ivendor.GetAll().OrderBy(o=>o.Description).ToList();

             //if (getlist != null)
             //{
             //    foreach (var v in getlist)
             //    {
             //        customers.Add(string.Format("{0}-{1}", v.Description, v.ID));
             //    }
             //}
             return getlist;
         }
        #endregion



        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetInventoryCategory(string knownCategoryValues, string category)
        {
            string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            string typeOfRequestId = (_catgoryValue[0]);
            var categoyList = InventoryMgt.BAL.InventoryCategoryBAL.InventoryCategoryBAL_CategorySelect().Where(c => c.MasterID == 0 && c.PortfolioID == sessionKeys.PortfolioID).OrderBy(o=>o.Name).ToList();

            var result = (from p in categoyList
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

            return result;



        }
        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetInventorySubCategory(string knownCategoryValues, string category)
        {
            string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            string categoryId = "0";
            //if (_catgoryValue.Length > 3)
            //{
                categoryId = (_catgoryValue[1]);
            //}
            var subCategoyList = InventoryMgt.BAL.InventoryCategoryBAL.InventoryCategoryBAL_SubCategorySelect(Convert.ToInt32(categoryId)).Where(o=>o.MasterID >0).OrderBy(o=>o.Name).ToList();

            var result = (from p in subCategoyList
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();

            return result;
        }
        //GetVendorsList
        //  RFIRepository<RFI.Entity.VendorDetails> RRep = new RFIRepository<RFI.Entity.VendorDetails>();
        // GridView1.DataSource = RRep.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetVendorsList(string knownCategoryValues, string category)
        {
            string[] _catgoryValue = knownCategoryValues.Split(':', ';');
            //string categoryId = "0";
            ////if (_catgoryValue.Length > 3)
            ////{
            //categoryId = (_catgoryValue[1]);
            ////}
            RFIRepository<RFI.Entity.VendorDetails> RRep = new RFIRepository<RFI.Entity.VendorDetails>();
            var v = RRep.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
            var result = (from p in v
                          select new CascadingDropDownNameValue { value = p.VendorID.ToString(), name = p.ContractorName }).ToArray();

            return result;
        }



        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object GetDataByStatus()
        {
            List<DxChartStatus> DxchartFList = new List<DxChartStatus>();
            DxChartStatus Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                //get data object
                List<DxDateDisplay> dlist = new List<DxDateDisplay>();
                int[] ar = { 0,-1, -2 };
                foreach(var v in ar)
                {
                    dlist.Add(new DxDateDisplay { 
                        month = DateTime.Now.AddMonths(v).Month, 
                        year = DateTime.Now.AddMonths(v).Year, 
                        displayname = DateTime.Now.AddMonths(v).ToString("MMMM") + "-" + DateTime.Now.AddMonths(v).Year.ToString() ,
                        startdate = Deffinity.Utility.StartDateOfMonth(DateTime.Now.AddMonths(v)),
                        enddate = Deffinity.Utility.EndDateOfMonth(DateTime.Now.AddMonths(v))

                    });
                }
                

                //get service request

                //get list of tickets
                //22 new
                //35	Closed
                var jobs = FLSDetailsBAL.JqgridlistByStatus(35);
                var list = DC.BLL.TypeOfRequestBAL.GetTypeOfRequestList().ToList();
                
                foreach (var l in dlist)
                {
                    Dlist = new DxChartStatus();
                    Dlist.dateitem = l.displayname;
                    Dlist.Fault = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Fault").Count();
                    Dlist.Inspection = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Inspection").Count();
                    Dlist.Maintenance = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Maintenance").Count();
                    Dlist.Installation = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Installation").Count();
                    Dlist.Repair = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Repair").Count();
                    Dlist.Service = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Service").Count();
                    Dlist.Upgrade = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Upgrade").Count();
                    DxchartFList.Add(Dlist);
                }
                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object GetDataByStatusAll(string fromdate, string todate, string search)
        {
            List<DxChartStatus> DxchartFList = new List<DxChartStatus>();
            DxChartStatus Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                //get data object
                List<DxDateDisplay> dlist = new List<DxDateDisplay>();
                DateTime sdate = Convert.ToDateTime(fromdate);
                DateTime edate = Convert.ToDateTime(todate);
                int difMonth = GetMonths(sdate, edate);

                List<int> ar = new List<int>();
                for (int i = 0; i < difMonth; i++)
                {
                    ar.Add(i * -1);
                }
                //int[] ar = { 0, -1, -2,-3,-4,-5 };
                foreach (var v in ar)
                {
                    dlist.Add(new DxDateDisplay
                    {
                        month = DateTime.Now.AddMonths(v).Month,
                        year = DateTime.Now.AddMonths(v).Year,
                        displayname = DateTime.Now.AddMonths(v).ToString("MMMM") + "-" + DateTime.Now.AddMonths(v).Year.ToString(),
                        startdate = Deffinity.Utility.StartDateOfMonth(DateTime.Now.AddMonths(v)),
                        enddate = Deffinity.Utility.EndDateOfMonth(DateTime.Now.AddMonths(v))

                    });
                }


                //get service request

                //get list of tickets
                //22 new
                //35	Closed
                var jobs = FLSDetailsBAL.JqgridlistByStatus(JobStatus.Closed);
                var list = DC.BLL.TypeOfRequestBAL.GetTypeOfRequestList().ToList();

                foreach (var l in dlist)
                {
                    Dlist = new DxChartStatus();
                    Dlist.dateitem = l.displayname;
                    Dlist.Fault = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Fault").Count();
                    Dlist.Inspection = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Inspection").Count();
                    Dlist.Maintenance = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Maintenance").Count();
                    Dlist.Installation = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Installation").Count();
                    Dlist.Repair = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Repair").Count();
                    Dlist.Service = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Service").Count();
                    Dlist.Upgrade = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.TypeofRequest == "Upgrade").Count();
                    DxchartFList.Add(Dlist);
                }
                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object GetDataByPriceAll(string fromdate, string todate,string search)
        {
            List<DxChartPrice> DxchartFList = new List<DxChartPrice>();
            DxChartPrice Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                //get data object
                List<DxDateDisplay> dlist = new List<DxDateDisplay>();
                DateTime sdate = Convert.ToDateTime(fromdate);
                DateTime edate = Convert.ToDateTime(todate);
                int difMonth = GetMonths(sdate, edate);

                List<int> ar = new List<int>();
                for(int i = 0; i< difMonth;i++)
                {
                    ar.Add(i *-1);
                }

                
                //int[] ar = { 0, -1, -2, -3, -4, -5 };
                foreach (var v in ar)
                {
                    dlist.Add(new DxDateDisplay
                    {
                        month = DateTime.Now.AddMonths(v).Month,
                        year = DateTime.Now.AddMonths(v).Year,
                        displayname = DateTime.Now.AddMonths(v).ToString("MMMM") + "-" + DateTime.Now.AddMonths(v).Year.ToString(),
                        startdate = Deffinity.Utility.StartDateOfMonth(DateTime.Now.AddMonths(v)),
                        enddate = Deffinity.Utility.EndDateOfMonth(DateTime.Now.AddMonths(v))

                    });
                }


                //get service request

                //get list of tickets
                //22 new
                //35	Closed
                var jobs = FLSDetailsBAL.JqgridlistByStatus(JobStatus.Closed);
                //var list = DC.BLL.TypeOfRequestBAL.GetTypeOfRequestList().ToList();

                foreach (var l in dlist)
                {
                    Dlist = new DxChartPrice();
                    Dlist.dateitem = l.displayname;
                    Dlist.SalesPrice = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Sum(o=>Convert.ToDouble( o.TotalCost));
                    Dlist.TotalCosts = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Sum(o => Convert.ToDouble(o.Cost));
                    Dlist.Profit = Dlist.SalesPrice - Dlist.TotalCosts;
                    
                    DxchartFList.Add(Dlist);
                }
                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object GetDataByCardPayment()
        {
            List<DxChartPrice> DxchartFList = new List<DxChartPrice>();
            DxChartPrice Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                using (DCDataContext dc = new DCDataContext())
                {
                    DateTime reference = DateTime.Now;
                    var calendar = CultureInfo.CurrentCulture.Calendar;

                    IEnumerable<int> daysInMonth = Enumerable.Range(1, calendar.GetDaysInMonth(reference.Year, reference.Month));

                    List<Tuple<DateTime, DateTime>> weeks = daysInMonth.Select(day => new DateTime(reference.Year, reference.Month, day))
                        .GroupBy(d => calendar.GetWeekOfYear(d, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday))
                        .Select(g => new Tuple<DateTime, DateTime>(g.First(), g.Last()))
                        .ToList();

                   // weeks.ForEach(x => Response.Write(string.Format("{0:dd/MM/yyyy} - {1:dd/MM/yyyy}", x.Item1, x.Item2)));



                    //get data object
                    List<DxDateDisplay> dlist = new List<DxDateDisplay>();
                    DateTime sdate = Convert.ToDateTime(DateTime.Now);
                    DateTime edate = Convert.ToDateTime(DateTime.Now);
                    int difMonth = GetMonths(sdate, edate);

                    //List<int> ar = new List<int>();
                    //for (int i = 0; i < difMonth; i++)
                    //{
                    //    ar.Add(i * -1);
                    //}


                    int i = 1;
                    //int[] ar = { 0, -1, -2, -3, -4, -5 };
                    foreach (var v in weeks)
                    {
                        dlist.Add(new DxDateDisplay
                        {
                            //month = DateTime.Now.AddMonths(Convert.ToDateTime( v.Item1.ToShortDateString())).Month,
                            //year = DateTime.Now.AddMonths(v.Item1).Year,
                            displayname = "Week "+ i.ToString(), //+  DateTime.Now.AddMonths(v.Item1).ToString("MMMM") + "-" + DateTime.Now.AddMonths(v).Year.ToString(),
                            startdate = v.Item1,//  Deffinity.Utility.StartDateOfMonth(DateTime.Now.AddMonths(v)),
                            enddate = v.Item2,// Deffinity.Utility.EndDateOfMonth(DateTime.Now.AddMonths(v))

                        });
                        i++;
                    }


                    //get service request

                    //get list of tickets
                    //22 new
                    //35	Closed
                    var jobs = dc.ServiceDesk_InvoiceDisplayByMonth(sessionKeys.PortfolioID).ToList(); //FLSDetailsBAL.JqgridlistByStatus(JobStatus.Closed);
                    //var list = DC.BLL.TypeOfRequestBAL.GetTypeOfRequestList().ToList();

                    foreach (var l in dlist)
                    {
                        Dlist = new DxChartPrice();
                        Dlist.dateitem = l.displayname;
                        Dlist.SalesPrice = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o=>o.Status == "card").Sum(o => Convert.ToDouble(o.Price));
                        Dlist.TotalCosts = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.Status != "card").Sum(o => Convert.ToDouble(o.Price));
                        Dlist.Profit = Dlist.SalesPrice - Dlist.TotalCosts;

                        DxchartFList.Add(Dlist);
                    }
                    return jsonSerializer.Serialize(DxchartFList).ToString();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }


        public int GetMonths(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new Exception("Start Date is greater than the End Date");
            }

            int months = ((endDate.Year * 12) + endDate.Month) - ((startDate.Year * 12) + startDate.Month);

            if (endDate.Day >= startDate.Day)
            {
                months++;
            }

            return months;
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object GetDataBySales(string fromdate, string todate, string search, string period)
        {
            List<DxChartSales> DxchartFList = new List<DxChartSales>();
            DxChartSales Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                //get data object
                List<DxDateDisplay> dlist = new List<DxDateDisplay>();
                int[] ar = { 0, -1, -2, -3, -4, -5 };
                foreach (var v in ar)
                {
                    dlist.Add(new DxDateDisplay
                    {
                        month = DateTime.Now.AddMonths(v).Month,
                        year = DateTime.Now.AddMonths(v).Year,
                        displayname = DateTime.Now.AddMonths(v).ToString("MMMM") + "-" + DateTime.Now.AddMonths(v).Year.ToString(),
                        startdate = Deffinity.Utility.StartDateOfMonth(DateTime.Now.AddMonths(v)),
                        enddate = Deffinity.Utility.EndDateOfMonth(DateTime.Now.AddMonths(v))

                    });
                }


                //get service request

                //get list of tickets
                //22 new
                //35	Closed
                var jobs = FLSDetailsBAL.JqgridlistByStatus(JobStatus.Closed);
                //var list = DC.BLL.TypeOfRequestBAL.GetTypeOfRequestList().ToList();
                UserMgt.BAL.ContractorsBAL cb = new UserMgt.BAL.ContractorsBAL();
                int[] sids = { 1, 4 };
                var userlist = cb.Contractor_Select_Active().Where(o => sids.Contains(o.SID.Value)).ToList();
                foreach (var l in dlist)
                {
                    foreach (var u in userlist)
                    {
                        Dlist = new DxChartSales();
                        
                        Dlist.dateitem = l.displayname;
                        Dlist.count = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.AssignedTechnicianID == u.ID ).Count();
                        Dlist.rank = 1;
                        Dlist.name = u.ContractorName;
                       
                        DxchartFList.Add(Dlist);
                    }
                }
                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object GetDataBySalesGrid(string fromdate, string todate, string search, string period)
        {
            List<DxChartSales> DxchartFList = new List<DxChartSales>();
            DxChartSales Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                //get data object
                List<DxDateDisplay> dlist = new List<DxDateDisplay>();
                int[] ar = { 0, -1, -2, -3, -4, -5 };
                foreach (var v in ar)
                {
                    dlist.Add(new DxDateDisplay
                    {
                        month = DateTime.Now.AddMonths(v).Month,
                        year = DateTime.Now.AddMonths(v).Year,
                        displayname = DateTime.Now.AddMonths(v).ToString("MMMM") + "-" + DateTime.Now.AddMonths(v).Year.ToString(),
                        startdate = Deffinity.Utility.StartDateOfMonth(DateTime.Now.AddMonths(v)),
                        enddate = Deffinity.Utility.EndDateOfMonth(DateTime.Now.AddMonths(v))

                    });
                }


                //get service request

                //get list of tickets
                //22 new
                //35	Closed
                var jobs = FLSDetailsBAL.JqgridlistByStatus(JobStatus.Closed);
                //var list = DC.BLL.TypeOfRequestBAL.GetTypeOfRequestList().ToList();
                UserMgt.BAL.ContractorsBAL cb = new UserMgt.BAL.ContractorsBAL();
                int[] sids = { 1, 4 };
                var userlist = cb.Contractor_Select_Active().Where(o => sids.Contains(o.SID.Value)).ToList();
                foreach (var l in dlist)
                {
                    foreach (var u in userlist)
                    {
                        Dlist = new DxChartSales();
                        Dlist.dateitem = l.displayname;
                        Dlist.count = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.AssignedTechnicianID == u.ID).Count();
                        Dlist.rank = 1;
                        Dlist.name = u.ContractorName;
                        
                        DxchartFList.Add(Dlist);
                    }
                }


                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public static List<DxChartSales> GetDataBySalesGridView(string fromdate, string todate, string search, string period)
        {
            List<DxChartSales> DxchartFList = new List<DxChartSales>();
            DxChartSales Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                //get data object
                List<DxDateDisplay> dlist = new List<DxDateDisplay>();
                int[] ar = { 0, -1, -2, -3, -4, -5 };
                foreach (var v in ar)
                {
                    dlist.Add(new DxDateDisplay
                    {
                        month = DateTime.Now.AddMonths(v).Month,
                        year = DateTime.Now.AddMonths(v).Year,
                        displayname = DateTime.Now.AddMonths(v).ToString("MMMM") + "-" + DateTime.Now.AddMonths(v).Year.ToString(),
                        startdate = Deffinity.Utility.StartDateOfMonth(DateTime.Now.AddMonths(v)),
                        enddate = Deffinity.Utility.EndDateOfMonth(DateTime.Now.AddMonths(v))
                        
                    });
                }
                var jobs = FLSDetailsBAL.JqgridlistByStatus(JobStatus.Closed);
                //var list = DC.BLL.TypeOfRequestBAL.GetTypeOfRequestList().ToList();
                UserMgt.BAL.ContractorsBAL cb = new UserMgt.BAL.ContractorsBAL();
                int[] sids = { 1, 4 };
                var userlist = cb.Contractor_Select_Active().Where(o => sids.Contains(o.SID.Value)).ToList();
                foreach (var l in dlist)
                {
                    foreach (var u in userlist)
                    {
                        Dlist = new DxChartSales();
                        Dlist.dateitem = l.displayname;
                        Dlist.count = jobs.Where(o => o.LoggedDate >= l.startdate && o.LoggedDate <= l.enddate).Where(o => o.AssignedTechnicianID == u.ID).Count();
                        Dlist.rank = 1;
                        Dlist.name = u.ContractorName;
                        Dlist.userid = u.ID;
                        DxchartFList.Add(Dlist);
                    }
                }



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return DxchartFList;
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object GetDataByStatusthisMonth()
        {
            List<DxchartF> DxchartFList = new List<DxchartF>();
            DxchartF Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                //get data object
                int[] st = { 22, 43, 35, 50, 44 };
                var dlist = DC.BLL.StatusBAL.Status_selectAll().Where(o=>st.Contains( o.ID));
                //int[] ar = { -1, -2, -3 };
                //foreach (var v in ar)
                //{
                //    dlist.Add(new DxDateDisplay
                //    {
                //        month = DateTime.Now.AddMonths(v).Month,
                //        year = DateTime.Now.AddMonths(v).Year,
                //        displayname = DateTime.Now.AddMonths(v).ToString("MMMM") + "-" + DateTime.Now.AddMonths(v).Year.ToString(),
                //        startdate = Deffinity.Utility.StartDateOfMonth(DateTime.Now.AddMonths(v)),
                //        enddate = Deffinity.Utility.EndDateOfMonth(DateTime.Now.AddMonths(v))

                //    });
                //}

                var startdate = Deffinity.Utility.StartDateOfMonth(DateTime.Now);
                var enddate = Deffinity.Utility.EndDateOfMonth(DateTime.Now);
                //get service request

                //get list of tickets
                //22 new
                //35	Closed
                var jobs = FLSDetailsBAL.Jqgridlist().ToList();
                var list = DC.BLL.TypeOfRequestBAL.GetTypeOfRequestList().ToList();

                foreach (var l in dlist)
                {
                    Dlist = new DxchartF();
                    Dlist.Name = l.Name;
                    Dlist.Value = jobs.Where(o => o.LoggedDate >= startdate && o.LoggedDate <= enddate).Where(o => o.Status == l.Name).Count();
                    DxchartFList.Add(Dlist);
                }
                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object GetDataByTeamthisMonth()
        {
            List<DxchartFWith3Fields> DxchartFList = new List<DxchartFWith3Fields>();
            DxchartFWith3Fields Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                
                var startdate = Deffinity.Utility.StartDateOfMonth(DateTime.Now);
                var enddate = Deffinity.Utility.EndDateOfMonth(DateTime.Now);
                //get service request

                //get list of tickets
                //22 new
                //35	Closed
                var jobs = FLSDetailsBAL.Jqgridlist().Where(o => o.LoggedDate >= startdate && o.LoggedDate <= enddate).ToList();
                var aList = jobs.GroupBy(o => o.AssignedTechnicianID);
                var uBal = new UserMgt.BAL.ContractorsBAL();
                var ulist = uBal.Contractor_SelectAll();
                //var list = DC.BLL.TypeOfRequestBAL.GetTypeOfRequestList().ToList();

                foreach (var l in aList.Where(o=>o.Key >0).ToList())
                {

                    Dlist = new DxchartFWith3Fields();
                    Dlist.Name = ulist.Where(o=>o.ID == l.Key).FirstOrDefault().ContractorName;
                    Dlist.Value1 = jobs.Where(o => o.AssignedTechnicianID == l.Key).Count().ToString();
                    Dlist.Value2 = l.Key.ToString();
                    DxchartFList.Add(Dlist);
                }
                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }



        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object GetInvoiceDataThisYear()
        {
            List<DxInvoices> DxchartFList = new List<DxInvoices>();
            DxInvoices Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                //get data object
                //int[] st = { 22, 43, 35, 50, 44 };
                //var dlist = DC.BLL.StatusBAL.Status_selectAll().Where(o => st.Contains(o.ID));
                

                var startdate = Deffinity.Utility.StartDateOfMonth(DateTime.Now.AddMonths(-12));
                var enddate = Deffinity.Utility.EndDateOfMonth(DateTime.Now);
                //get service request

                //get list of tickets
                //22 new
                //35	Closed
                //var invoices = InvoiceBAL.GetInvoiceList().Where(o => (o.LoggedDate >= startdate && o.LoggedDate <= enddate) && o.Status == InvoiceStatus.Paid).ToList();

                var invoices = InvoiceBAL.GetInvoiceList().Where(o => (o.LoggedDate >= startdate && o.LoggedDate <= enddate) ).ToList();
                for (int i = 0; i < 12; i++)
                {
                    var sdate = startdate;
                    var edate = startdate.AddMonths(i);
                    var sTotal = invoices.Where(o => (o.LoggedDate >= startdate && o.LoggedDate <= enddate)).Select(l=> (l.RevicedPrice.HasValue ? l.RevicedPrice.Value : 0.00)).Sum();
                    foreach (var l in invoices)
                    {
                        Dlist = new DxInvoices();
                        Dlist.date = l.LoggedDate.Value.ToString("MMMM dd, yyyy HH:mm:mm");
                        Dlist.value = (l.RevicedPrice.HasValue ? l.RevicedPrice.Value : 0.00);
                        DxchartFList.Add(Dlist);
                    }
                }
                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object GetInvoiceSumByStatusThisYear()
        {
            List<DxInvoicesSum> DxchartFList = new List<DxInvoicesSum>();
            DxInvoicesSum Dlist = null;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {

                using (DCDataContext DC = new DCDataContext())
                {
                    //New Status
                    //22	New
                    //35	Closed
                    var l = DC.ServiceDesk_DashboardDisplay(sessionKeys.PortfolioID).FirstOrDefault();
                    //var startdate = Deffinity.Utility.StartDateOfMonth(DateTime.Now.AddMonths(-12));
                    //var enddate = Deffinity.Utility.EndDateOfMonth(DateTime.Now);
                    //get service request

                    //get list of tickets
                    //22 new
                    //35	Closed
                    //var invoices = InvoiceBAL.GetInvoiceList().Where(o => (o.LoggedDate >= startdate && o.LoggedDate <= enddate)).ToList();
                    //paid
                    Dlist = new DxInvoicesSum();
                    Dlist.name = InvoiceStatus.Paid;
                    Dlist.value = l.InvoicedPaid.HasValue?l.InvoicedPaid.Value:0.00; //invoices.Where(o => o.Status == InvoiceStatus.Paid).Sum(l => (l.RevicedPrice.HasValue ? l.RevicedPrice.Value : 0.00));
                    DxchartFList.Add(Dlist);

                    Dlist = new DxInvoicesSum();
                    Dlist.name = "Outstanding";
                    Dlist.value = (l.InvoicedAwaiting.HasValue ? l.InvoicedAwaiting.Value : 0.00); //invoices.Where(o => o.Status != InvoiceStatus.Paid).Sum(l => (l.RevicedPrice.HasValue ? l.RevicedPrice.Value : 0.00));
                    DxchartFList.Add(Dlist);

                }
                    //get data object
                    //int[] st = { 22, 43, 35, 50, 44 };
                    //var dlist = DC.BLL.StatusBAL.Status_selectAll().Where(o => st.Contains(o.ID));


                  

                //non paid

                //foreach (var l in invoices)
                //{
                //    Dlist = new DxInvoices();
                //    Dlist.date = l.LoggedDate.Value.ToString("MMMM dd, yyyy HH:mm:mm");
                //    Dlist.value = (l.RevicedPrice.HasValue ? l.RevicedPrice.Value : 0.00);
                //    DxchartFList.Add(Dlist);
                //}
                return jsonSerializer.Serialize(DxchartFList).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }

        //[WebMethod(EnableSession = true)]
        //[System.Web.Script.Services.ScriptMethod]
        //public object GetInvoiceSumByStatusByCardThisYear()
        //{
        //    List<DxInvoicesSum> DxchartFList = new List<DxInvoicesSum>();
        //    DxInvoicesSum Dlist = null;
        //    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        //    try
        //    {

        //        using (DCDataContext DC = new DCDataContext())
        //        {
        //            //New Status
        //            //22	New
        //            //35	Closed
        //            var l = DC.ServiceDesk_InvoiceDisplayByMonth(sessionKeys.PortfolioID).FirstOrDefault();


        //            //var startdate = Deffinity.Utility.StartDateOfMonth(DateTime.Now.AddMonths(-12));
        //            //var enddate = Deffinity.Utility.EndDateOfMonth(DateTime.Now);
        //            //get service request

        //            //get list of tickets
        //            //22 new
        //            //35	Closed
        //            //var invoices = InvoiceBAL.GetInvoiceList().Where(o => (o.LoggedDate >= startdate && o.LoggedDate <= enddate)).ToList();
        //            //paid
                    
        //            Dlist = new DxInvoicesSum();
        //            Dlist.name = InvoiceStatus.Paid;
        //            Dlist.value = l.InvoicedPaid.HasValue ? l.InvoicedPaid.Value : 0.00; //invoices.Where(o => o.Status == InvoiceStatus.Paid).Sum(l => (l.RevicedPrice.HasValue ? l.RevicedPrice.Value : 0.00));
        //            DxchartFList.Add(Dlist);

        //            Dlist = new DxInvoicesSum();
        //            Dlist.name = "Outstanding";
        //            Dlist.value = (l.InvoicedAwaiting.HasValue ? l.InvoicedAwaiting.Value : 0.00); //invoices.Where(o => o.Status != InvoiceStatus.Paid).Sum(l => (l.RevicedPrice.HasValue ? l.RevicedPrice.Value : 0.00));
        //            DxchartFList.Add(Dlist);

        //        }
        //        //get data object
        //        //int[] st = { 22, 43, 35, 50, 44 };
        //        //var dlist = DC.BLL.StatusBAL.Status_selectAll().Where(o => st.Contains(o.ID));




        //        //non paid

        //        //foreach (var l in invoices)
        //        //{
        //        //    Dlist = new DxInvoices();
        //        //    Dlist.date = l.LoggedDate.Value.ToString("MMMM dd, yyyy HH:mm:mm");
        //        //    Dlist.value = (l.RevicedPrice.HasValue ? l.RevicedPrice.Value : 0.00);
        //        //    DxchartFList.Add(Dlist);
        //        //}
        //        return jsonSerializer.Serialize(DxchartFList).ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //        return jsonSerializer.Serialize(string.Empty).ToString();
        //    }
        //}


    }
    #region "Custom Class"
    public class PortfolioContactDetails
    {
        public int ID { get; set; }
        public string RequesterName { get; set; }
        public string RequesterEmail { get; set; }
        public string Location { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
    }

    public class PortfolioContactAddressDetails
    {
        public int ID { get; set; }
        public int AddressID { get; set; }
        public string RequesterName { get; set; }
        public string RequesterEmail { get; set; }
        public string Location { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string PolicyNumber { get; set; }
        public string StartDate { get; set; }
        public string ExpiryDate { get; set; }
        public string DaysRemaining { get; set; }
        public string PolicyNotes { get; set; }
        public string PolicyType { get; set; }
        public int PolicyTypeID { get; set; }

    }
    #endregion

    public class DxDateDisplay
    {
        public int month { set; get; }
        public int year { set; get; }

        public DateTime startdate { set; get; }
        public DateTime enddate { set; get; }
        public string displayname { set; get; }

    }
        public class DxChartStatus
    {
        public string dateitem { set; get; }
        public int Fault { set; get; }

        public int Inspection { set; get; }
        public int Maintenance { set; get; }
        public int Installation { set; get; }
        public int Repair { set; get; }
        public int Service { set; get; }
        public int Upgrade { set; get; }
       
    }

    public class DxChartPrice
    {
        public string dateitem { set; get; }
        public double SalesPrice { set; get; }

        public double TotalCosts { set; get; }
        public double Profit { set; get; }
      

    }

    public class DxChartSales
    {
        public int userid { set; get; }
        public string dateitem { set; get; }
        public int rank { set; get; }
        public int count { set; get; }

        public string name { set; get; }
        
       


    }

    public class GvFields
   {
       public int callid { get; set; }
       public string Des { get; set; }
   }
   public class DxchartFNew
   {
       public double Value { get; set; }
       public string Name { get; set; }
   }
   public class DxchartF
   {
       public int Value { get; set; }
       public string Name { get; set; }
   }
    public class DxInvoices
    {
        public double value { get; set; }
        public string date { get; set; }
    }
    public class DxInvoicesSum
    {
        public double value { get; set; }
        public string name { get; set; }
    }

    public class DxchartFWith3Fields
   {
       public string Value1 { get; set; }
       public string Value2 { get; set; }
       public string Value3 { get; set; }
       public string Name { get; set; }
   }
   public class StatusList
   {
       public int Pending { get; set; }
       public int InHand { get; set; }
       public int Completed { get; set; }
       public int Resolved { get; set; }
       public string Name { get; set; }
   }

   public class CallForDayWithTime
   {
       public string d_Time { get; set; }
       public int Calls_Count { get; set; }
   }
   public class PerDayGraphCls
   {
       public int PerdayCount { get; set; }
       public int TotalCount { get; set; }
   }


   public class OpenClaimsCls
   {

        public int CCID { set; get; }
        public int Callid { set; get; }
       public DateTime DateClosed { set; get; }
       public string DateClosedDis { set; get; }
       public string SalesPerson { set; get; }
       public string PolicyType { set; get; }
       public string ContractTerm { set; get; }
       public string PolicyNumber { set; get; }
       public string Customer { set; get; }
       public string Address { set; get; }
       public string Address1 { set; get; }
       public string City { set; get; }
       public string ZipCode { set; get; }
       public string InvoiceValue { set; get; }
       public string TypeOfRequest { set; get; }
        public int TypeOfRequestID { set; get; }
        public string AssignTechnician { set; get; }
        public int AssignTechnicianID { set; get; }
        public string Category { set; get; }
        public int CategoryID { set; get; }
        public string paypalref { set; get; }
        public string transval { set; get; }
        public string details { set; get; }

        public string TicketManager { set; get; }
        
    }
}

