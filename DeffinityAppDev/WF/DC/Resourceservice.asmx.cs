using DC.BLL;
using DC.DAL;
using DC.Entity;
using DeffinityManager.UserManagement.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using UserMgt.Entity;
using DC.Entity;
using System.Net;
using System.IO;
using System.Data;
using System.Xml;
using DC.BAL;
using PortfolioMgt.DAL;
using AssetsMgr.DAL;

namespace DeffinityAppDev.WF.DC
{

    public class CallResourceDisplay
    {
        public int id { set; get; }
        public int ccid { set; get; }
        public int callid { set; get; }
        public int resourceId { set; get; }
        public string title { set; get; }
        public string start { set; get; }
        public string end { set; get; }
        public string rname { set; get; }
        public string contact { set; get; }
        public string tref { set; get; }
        public string details { set; get; }
        public string status { set; get; }
        public string address { set; get; }
        public string postCode { set; get; }
        public string backcolor { set; get; }
        public string borderColor { set; get; }
        public string town { set; get; }
        public string spid { set; get; }
        public string srid { set; get; }
        public string spname { set; get; }
        public string srname { set; get; }
        public string spcontact { set; get; }
        public string rtype { set; get; }

        public string usertype { set; get; }
    }
    /// <summary>
    /// Summary description for Resourceservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Resourceservice : System.Web.Services.WebService
    {
        //[WebMethod]
        //public string[] GetVendorItems(string prefix)
        //{
        //    List<string> customers = new List<string>();
        //    IPortfolioRepository<PortfolioMgt.Entity.v_ShopItems_vendor> ivendor = new PortfolioRepository<PortfolioMgt.Entity.v_ShopItems_vendor>();
        //    var getlist = ivendor.GetAll().Where(o => o.Details.Contains(prefix)).ToList();

        //    if (getlist != null)
        //    {
        //        foreach (var v in getlist)
        //        {
        //            customers.Add(string.Format("{0}-{1}", v.Details, v.ID));
        //        }
        //    }
        //    return customers.ToArray();
        //}
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        
        #region Resource postcodes
        public class resourcePostcodes
        {
            public int ResourceID { set; get; }
            public string Postcode { set; get; }
        }
        public class RequesterResourceDistance
        {
            public int ResourceID { set; get; }
            public double? Distance { set; get; }
        }
        //Get all users postcodes
        public List<resourcePostcodes> GetAllUserPostcodes()
        {
            List<resourcePostcodes> rpList = new List<resourcePostcodes>();
            IUserRepository<UserMgt.Entity.UserDetail> udetails = new UserRepository<UserMgt.Entity.UserDetail>();
            var GetPostcode = udetails.GetAll().Where(o => o.PostCode != null).ToList();
            if(GetPostcode.Count >0)
            {
                var s = (from p in  GetPostcode
                        where p.PostCode.Length >0
                        select new resourcePostcodes {
                        ResourceID = p.UserId.Value,
                        Postcode = p.PostCode
                        }).ToList();
                rpList.AddRange(s);
            }

            IUserRepository<UserMgt.Entity.UserAssociatedPostcode> updetails = new UserRepository<UserMgt.Entity.UserAssociatedPostcode>();
            var upPostcode = updetails.GetAll().Where(o => o.Postcode != null).ToList();
            if (upPostcode.Count > 0)
            {
                var s = (from p in upPostcode
                         where p.Postcode.Length > 0
                         select new resourcePostcodes
                         {
                             ResourceID = p.UserID,
                             Postcode = p.Postcode
                         }).ToList();
                rpList.AddRange(s);
            }

            return rpList;
        }
        //Get the distance from two postcodes
        public double? GetDistance(string origin, string destination)
        {
            double? retval = null;
            string url = @"http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&sensor=false";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();

            DataSet ds = new DataSet();
            ds.ReadXml(new XmlTextReader(new StringReader(responsereader)));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables["element"].Rows[0]["status"].ToString() == "OK")
                {
                    // lblDuration.Text = ds.Tables["duration"].Rows[0]["text"].ToString();
                    var n = ds.Tables["distance"].Rows[0]["text"].ToString().Replace("km", string.Empty).Replace("m",string.Empty).Trim().ToString();
                    try
                    {
                        retval = Convert.ToDouble(n);
                    }
                    catch(Exception ex)
                    {
                        retval = null;
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
            }
            return retval;
        }
        //Get Requester matching postcodes and user
        
        //Get Requester to resource distance
        public List<RequesterResourceDistance> GetDistanceRequester(string from_postcode)
        {
            List<RequesterResourceDistance> rdList = new List<RequesterResourceDistance>();
            if (from_postcode.Length > 0)
            {
                var rlist = GetAllUserPostcodes();
                foreach (var t in rlist)
                {
                    if (from_postcode != t.Postcode)
                    {
                        var d = GetDistance(from_postcode, t.Postcode);
                        if (d.HasValue)
                            rdList.Add(new RequesterResourceDistance() { ResourceID = t.ResourceID, Distance = d });
                    }
                    else
                        rdList.Add(new RequesterResourceDistance() { ResourceID = t.ResourceID, Distance = 0 });
                }
            }
            return rdList;
        }
        #endregion

        [WebMethod(EnableSession = true)]
        public object GetAllEventsSmartRep(string CallID, string pids)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                
                List<CallResourceDisplay> crDisplay = new List<CallResourceDisplay>();
                List<Jqgrid> flsList = new List<Jqgrid>();
                List<CallResourceSchedule> callResources = new List<CallResourceSchedule>();
                List<CallResourceSchedule> assinedUsers = new List<CallResourceSchedule>();
                List<CallResourceSchedule> acceptedUsers = new List<CallResourceSchedule>();
                //List<PortfolioMgt.Entity.PortfolioContact> ContactList = new List<PortfolioMgt.Entity.PortfolioContact>();

                flsList = FLSDetailsBAL.Jqgridlist().Where(o => o.Status != "Cancelled" && o.Status != "Job Complete").ToList();
                //IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> iContactDetails = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                IDCRespository<FLSDetail> iDetails = new DCRepository<FLSDetail>();
                IDCRespository<CallResourceSchedule> CrRepository = new DCRepository<CallResourceSchedule>();

                // List<FLSDetail> flsDetails = new List<FLSDetail>();
                List<UserMgt.Entity.Contractor> rlist = new List<UserMgt.Entity.Contractor>();
                List<UserMgt.Entity.UserDetail> cdlist = new List<UserMgt.Entity.UserDetail>();
                IUserRepository<UserMgt.Entity.UserDetail> cdRepository = new UserRepository<UserMgt.Entity.UserDetail>();
                //if(CallID.Trim().Length >0)
                //    flsList = flsList.Where(o => o.CallID == Convert.ToInt32(CallID)).ToList();
                ResourceScheduleBAL Res = new ResourceScheduleBAL();
                // var ResourceList = Res.GetAllEvents().ToList();
                if (CallID.Trim().Length > 0)
                {
                    var d = flsList.Where(o => o.CallID == Convert.ToInt32(CallID)).FirstOrDefault();// flsList.FirstOrDefault();
                    callResources = CrRepository.GetAll().Where(o => o.CallID == d.CallID && (o.IsActive.HasValue ? o.IsActive.Value : false) != true && o.UserType == CallResourceScheduleBAL.Usertype_SmartRep).ToList();
                    assinedUsers = CrRepository.GetAll().Where(o => o.CallID == d.CallID && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == false && o.UserType == CallResourceScheduleBAL.Usertype_SmartRep).ToList();
                    acceptedUsers = CrRepository.GetAll().Where(o => o.CallID == d.CallID && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true && o.UserType == CallResourceScheduleBAL.Usertype_SmartRep).ToList();
                    //ContactList = iContactDetails.GetAll().Where(o => o.ID == (d.r != null?d.AssignedTechnicianID : 0))
                    if (d.Status != "Job Complete")
                    {
                        //(d.Status == "Quote Accepted" || d.Status == "Awaiting Schedule" || d.Status == "New" || ) 
                        //44	Awaiting Technician
                        if ( (acceptedUsers.Count == 0 && assinedUsers.Count == 0))
                        {
                            //callResources = CrRepository.GetAll().Where(o => (o.IsActive.HasValue ? o.IsActive.Value : false) != true).ToList();
                            //if (callResources != null)
                            //{
                            if (pids.Length == 0)
                            {
                                rlist = Res.GetAllContractors().Where(o => callResources.Select(p => p.ResourceID.Value).Contains(o.ID)).ToList();
                            }
                            else
                            {
                                List<int> rids = new List<int>();

                                if (rids.Where(p => p > 0).Count() > 0)
                                {
                                    rlist = Res.GetAllContractors().Where(o => rids.Where(p => p > 0).Contains(o.ID)).ToList();
                                }
                                else
                                {
                                    int[] sids = { 1, 4 };
                                    rlist = Res.GetAllContractors().Where(o => sids.Contains(o.SID.Value)).ToList();

                                    using (DCDataContext dc = new DCDataContext())
                                    {
                                        var list = dc.GetServiceProvidersList(Convert.ToInt32(CallID)).Where(o => o.total > 0).ToList();
                                        //if (list.Count() >0)
                                        rlist = rlist.Where(o => list.Select(p => p.UserID).ToArray().Contains(o.ID)).ToList();
                                    }

                                }
                            }
                            // }
                        }
                        else
                        {

                            //after assigned userd
                            //callResources = CrRepository.GetAll().Where(o => o.CallID == d.CallID && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true).ToList();
                            //rlist = Res.GetAllContractors().Where(o => o.ID == (d.AssignedTechnicianID != null ? d.AssignedTechnicianID : 0)).ToList();
                            if (assinedUsers.Count > 0 && acceptedUsers.Count == 0)
                            {
                                rlist = Res.GetAllContractors().Where(o => assinedUsers.Select(c => c.ResourceID).Contains(o.ID)).ToList();
                            }
                            else if (acceptedUsers.Count > 0)
                            {
                                //accepted users
                                rlist = Res.GetAllContractors().Where(o => acceptedUsers.Select(c => c.ResourceID).Contains(o.ID)).ToList();

                            }

                        }
                    }
                }

                //Get list of unasinged resource
                if (assinedUsers.Count() > 0 && acceptedUsers.Count() == 0)
                {
                    var aUsers = assinedUsers.Select(o => o.ResourceID).ToList();
                    acceptedUsers = CrRepository.GetAll().Where(o => aUsers.Contains(o.ResourceID) && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true).ToList();
                    assinedUsers.AddRange(acceptedUsers);// = CrRepository.GetAll().Where(o => o.CallID == d.CallID && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == false).ToList();

                    //if(callResources.Where(o=>(o.IsActive.HasValue ? o.IsActive.Value : false) != false).Count() >0)
                    //{
                    var ResultResource = (from r in flsList
                                          join p in assinedUsers on r.CallID equals p.CallID
                                          join c in rlist on p.ResourceID equals c.ID
                                          //where (p.IsActive.HasValue ? p.IsActive.Value : false) == true
                                          select new
                                          {
                                              Id = p.ID,
                                              CCID = r.CCID,
                                              CallID = r.CallID,
                                              ResourceId = c.ID,
                                              Title = r.RequesterName,
                                              Sdate = p.StartDate,
                                              Edate = p.EndDate,
                                              RequesterName = r.RequesterName,
                                              Contact = r.RequestersTelephoneNo,
                                              TicketRef = "" + r.CCID.ToString(),
                                              Details = r.Details,
                                              Status = r.Status,
                                              Address = r.RequestersAddress,
                                              PostCode = r.RequestersPostCode,
                                              // spid = r.AssignedTechnicianID.ToString(),
                                              //spname = r.AssignedTechnician
                                              spid = c.ID.ToString(),// r.AssignedTechnicianID.ToString(),
                                              spname =  p.UserType + ": "+ ((r.Status == "Awaiting Schedule") ? c.ContractorName : c.ContractorName),
                                              srname = r.TicketManager,
                                                usertype = p.UserType

                                          }).ToList();

                    var snew = (from r in ResultResource
                                select new CallResourceDisplay
                                {
                                    id = r.Id,
                                    callid = r.CallID,
                                    ccid = r.CCID,
                                    resourceId = r.ResourceId,
                                    title = r.Title,
                                    start = string.Format("{0:s}", r.Sdate),
                                    end = string.Format("{0:s}", r.Edate),
                                    rname = r.RequesterName,
                                    contact = r.Contact,
                                    tref = r.TicketRef,
                                    details = r.Details,
                                    status = r.Status,
                                    address = r.Address,
                                    postCode = r.PostCode,
                                    backcolor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep ? GetColor("New") : GetColor(r.Status)),
                                    borderColor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep ? GetColor("New") : GetColor(r.Status)),
                                    spid = r.spid,
                                    spname = r.spname,
                                    srname = r.srname
                                    
                                }).ToList();
                    crDisplay.AddRange(snew);
                    // }

                }
                else if (acceptedUsers.Count > 0)
                {
                    var resourceid = acceptedUsers.FirstOrDefault().ResourceID;
                    acceptedUsers = CrRepository.GetAll().Where(o => (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true).ToList();
                    var Result = from r in flsList

                                 join p in acceptedUsers on r.CallID equals p.CallID
                                 join
                                //c in rlist on r.AssignedTechnicianID equals c.ID
                                c in rlist on p.ResourceID equals c.ID
                                 //where (p.IsActive.HasValue ? p.IsActive.Value : false) == true
                                 //where (p.IsAssigned.HasValue?p.IsAssigned.Value:false) == true
                                 select new
                                 {
                                     Id = p.ID,
                                     CCID = r.CCID,
                                     CallID = r.CallID,
                                     ResourceId = c.ID,
                                     Title = r.RequesterName,
                                     //Sdate = Convert.ToDateTime(r.ScheduledDateTime),
                                     //Edate = Convert.ToDateTime(r.ScheduledEndDateTime),
                                     Sdate = Convert.ToDateTime(p.StartDate),
                                     Edate = Convert.ToDateTime(p.EndDate),
                                     RequesterName = r.RequesterName,
                                     Contact = r.RequestersTelephoneNo,
                                     TicketRef = "" + r.CCID.ToString(),
                                     Details = r.Details,
                                     Status = r.Status,
                                     Address = r.RequestersAddress,
                                     PostCode = r.RequestersPostCode,
                                     spid = c.ID.ToString(),// r.AssignedTechnicianID.ToString(),
                                     spname = p.UserType + ": " + ((r.Status == "Awaiting Schedule") ? c.ContractorName : c.ContractorName),
                                     srname = r.TicketManager,
                                     usertype = p.UserType
                                 };



                    var s = (from r in Result
                             select new CallResourceDisplay
                             {
                                 id = r.Id,
                                 callid = r.CallID,
                                 ccid = r.CCID,
                                 resourceId = r.ResourceId,
                                 title = r.Title,
                                 start = string.Format("{0:s}", r.Sdate),
                                 end = string.Format("{0:s}", r.Edate),
                                 rname = r.RequesterName,
                                 contact = r.Contact,
                                 tref = r.TicketRef,
                                 details = r.Details,
                                 status = r.Status,
                                 address = r.Address,
                                 postCode = r.PostCode,
                                 backcolor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep ? GetColor("New") : GetColor(r.Status)),
                                 borderColor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep ? GetColor("New") : GetColor(r.Status)),
                                 spid = r.spid,
                                 spname = r.spname,
                                 srname = r.srname
                                 
                             }).ToList();
                    crDisplay.AddRange(s);


                }
                else
                {
                    acceptedUsers = CrRepository.GetAll().Where(o => (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true).ToList();

                    var Result = from r in flsList

                                 join p in acceptedUsers on r.CallID equals p.CallID
                                 join
                                //c in rlist on r.AssignedTechnicianID equals c.ID
                                c in rlist on p.ResourceID equals c.ID
                                 //where (p.IsActive.HasValue ? p.IsActive.Value : false) == true
                                 //where (p.IsAssigned.HasValue?p.IsAssigned.Value:false) == true
                                 select new
                                 {
                                     Id = p.ID,
                                     CCID = r.CCID,
                                     CallID = r.CallID,
                                     ResourceId = c.ID,
                                     Title = r.RequesterName,
                                     //Sdate = Convert.ToDateTime(r.ScheduledDateTime),
                                     //Edate = Convert.ToDateTime(r.ScheduledEndDateTime),
                                     Sdate = Convert.ToDateTime(p.StartDate),
                                     Edate = Convert.ToDateTime(p.EndDate),
                                     RequesterName = r.RequesterName,
                                     Contact = r.RequestersTelephoneNo,
                                     TicketRef = "" + r.CCID.ToString(),
                                     Details = r.Details,
                                     Status = r.Status,
                                     Address = r.RequestersAddress,
                                     PostCode = r.RequestersPostCode,
                                     spid = c.ID.ToString(),// r.AssignedTechnicianID.ToString(),
                                     spname = p.UserType + ": " + ((r.Status == "Awaiting Schedule") ? c.ContractorName : c.ContractorName),
                                     srname = r.TicketManager,
                                     usertype = p.UserType
                                 };



                    var s = (from r in Result
                             select new CallResourceDisplay
                             {
                                 id = r.Id,
                                 callid = r.CallID,
                                 ccid = r.CCID,
                                 resourceId = r.ResourceId,
                                 title = r.Title,
                                 start = string.Format("{0:s}", r.Sdate),
                                 end = string.Format("{0:s}", r.Edate),
                                 rname = r.RequesterName,
                                 contact = r.Contact,
                                 tref = r.TicketRef,
                                 details = r.Details,
                                 status = r.Status,
                                 address = r.Address,
                                 postCode = r.PostCode,
                                 backcolor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep ? GetColor("New") : GetColor(r.Status)),
                                 borderColor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep ? GetColor("New") : GetColor(r.Status)),
                                 spid = r.spid,
                                 spname = r.spname,
                                 srname = r.srname
                             }).ToList();
                    crDisplay.AddRange(s);
                }

                var rlistnew = from c in rlist
                               select new
                               {
                                   Id = c.ID,
                                   Title = string.Format("{0}", c.ContractorName)
                               };
                var slist = from r in rlistnew
                            select new
                            {
                                id = r.Id,
                                title = r.Title
                            };

                var result = new { data = crDisplay, data2 = slist };
                return Jsonserializer.Serialize(result).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }


        [WebMethod(EnableSession = true)]
        public object GetAllEvents(string CallID,string pids)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                List<CallResourceDisplay> crDisplay = new List<CallResourceDisplay>();
                List<Jqgrid> flsList = new List<Jqgrid>();
                List<CallResourceSchedule> callResources = new List<CallResourceSchedule>();
                List<CallResourceSchedule> assinedUsers = new List<CallResourceSchedule>();
                List<CallResourceSchedule> acceptedUsers = new List<CallResourceSchedule>();
                //List<PortfolioMgt.Entity.PortfolioContact> ContactList = new List<PortfolioMgt.Entity.PortfolioContact>();

                flsList = FLSDetailsBAL.Jqgridlist().Where(o => o.Status != "Cancelled" && o.Status != "Job Complete").ToList();
                //IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> iContactDetails = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                IDCRespository<FLSDetail> iDetails = new DCRepository<FLSDetail>();
                IDCRespository<CallResourceSchedule> CrRepository = new DCRepository<CallResourceSchedule>();

               // List<FLSDetail> flsDetails = new List<FLSDetail>();
                List<UserMgt.Entity.Contractor> rlist = new List<UserMgt.Entity.Contractor>();
                List<UserMgt.Entity.UserDetail> cdlist = new List<UserMgt.Entity.UserDetail>();
                IUserRepository<UserMgt.Entity.UserDetail> cdRepository = new UserRepository<UserMgt.Entity.UserDetail>();
                //if(CallID.Trim().Length >0)
                //    flsList = flsList.Where(o => o.CallID == Convert.ToInt32(CallID)).ToList();
                ResourceScheduleBAL Res = new ResourceScheduleBAL();
               // var ResourceList = Res.GetAllEvents().ToList();
                if (CallID.Trim().Length > 0)
                {
                    var d = flsList.Where(o => o.CallID == Convert.ToInt32(CallID)).FirstOrDefault();// flsList.FirstOrDefault();
                    callResources = CrRepository.GetAll().Where(o => o.CallID == d.CallID && (o.IsActive.HasValue ? o.IsActive.Value : false) != true).ToList();
                    assinedUsers = CrRepository.GetAll().Where(o => o.CallID == d.CallID && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == false && o.UserType != CallResourceScheduleBAL.Usertype_SmartRep).ToList();
                    acceptedUsers = CrRepository.GetAll().Where(o => o.CallID == d.CallID && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true && o.UserType != CallResourceScheduleBAL.Usertype_SmartRep).ToList();
                    //ContactList = iContactDetails.GetAll().Where(o => o.ID == (d.r != null?d.AssignedTechnicianID : 0))
                    if (d.Status != "Job Complete")
                    {
                       
                        //44	Awaiting Technician
                        if ( (d.Status == "Quote Accepted" || d.Status == "Awaiting Schedule" || d.Status == "New") && (acceptedUsers.Count == 0 && assinedUsers.Count ==0))
                        {

                            //callResources = CrRepository.GetAll().Where(o => (o.IsActive.HasValue ? o.IsActive.Value : false) != true).ToList();
                           
                            //if (callResources != null)
                            //{
                                if (pids.Length == 0)
                                {
                                    rlist = Res.GetAllContractors().Where(o => callResources.Select(p => p.ResourceID.Value).Contains(o.ID)).ToList();
                                }
                                else
                                {
                                    List<int> rids = new List<int>();

                                    if (rids.Where(p => p > 0).Count() > 0)
                                    {
                                        rlist = Res.GetAllContractors().Where(o => rids.Where(p => p > 0).Contains(o.ID)).ToList();
                                    }
                                    else
                                    {
                                    int[] sids = { 1, 4 };
                                        rlist = Res.GetAllContractors().Where(o=> sids.Contains( o.SID.Value)).ToList();

                                        using (DCDataContext dc = new DCDataContext())
                                        {
                                            var list = dc.GetServiceProvidersList(Convert.ToInt32(CallID)).Where(o => o.total > 0).ToList();
                                            //if (list.Count() >0)
                                            rlist = rlist.Where(o => list.Select(p => p.UserID).ToArray().Contains(o.ID)).ToList();
                                        }
                                        
                                    }
                                }
                           // }
                        }
                        else
                        {

                            //after assigned userd
                            //callResources = CrRepository.GetAll().Where(o => o.CallID == d.CallID && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true).ToList();
                            //rlist = Res.GetAllContractors().Where(o => o.ID == (d.AssignedTechnicianID != null ? d.AssignedTechnicianID : 0)).ToList();
                            if(assinedUsers.Count >0 && acceptedUsers.Count == 0)
                            {
                               var n_assinedUsers = CrRepository.GetAll().Where(o => o.CallID == d.CallID && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == false && o.UserType != CallResourceScheduleBAL.Usertype_SmartRep).ToList();
                                rlist = Res.GetAllContractors().Where(o => n_assinedUsers.Select(c=>c.ResourceID).Contains( o.ID )).ToList();
                            }
                            else if(acceptedUsers.Count > 0)
                            {
                                var n_acceptedUsers = CrRepository.GetAll().Where(o => o.CallID == d.CallID && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true).ToList();
                                //accepted users
                                rlist = Res.GetAllContractors().Where(o => n_acceptedUsers.Select(c => c.ResourceID).Contains(o.ID)).ToList();

                            }
                          
                        }
                    }
                }

                //Get list of unasinged resource
                if (assinedUsers.Count() > 0 && acceptedUsers.Count() == 0)
                {
                    var aUsers = assinedUsers.Select(o => o.ResourceID).ToList();
                    acceptedUsers = CrRepository.GetAll().Where(o => aUsers.Contains( o.ResourceID) && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true).ToList();
                    assinedUsers.AddRange(acceptedUsers);// = CrRepository.GetAll().Where(o => o.CallID == d.CallID && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == false).ToList();

                    //if(callResources.Where(o=>(o.IsActive.HasValue ? o.IsActive.Value : false) != false).Count() >0)
                    //{
                    var ResultResource = (from r in flsList
                                         join p in assinedUsers on r.CallID equals p.CallID
                                         join c in rlist on p.ResourceID equals c.ID
                                         //where (p.IsActive.HasValue ? p.IsActive.Value : false) == true
                                         select new
                                         {
                                             Id = p.ID,
                                             CCID = r.CCID,
                                             CallID = r.CallID,
                                             ResourceId = c.ID,
                                             Title = r.RequesterName,
                                             Sdate = p.StartDate,
                                             Edate = p.EndDate,
                                             RequesterName = r.RequesterName,
                                             Contact = r.RequestersTelephoneNo,
                                             TicketRef = "" + r.CCID.ToString(),
                                             Details = r.Details,
                                             Status = r.Status,
                                             Address = r.RequestersAddress,
                                             PostCode = r.RequestersPostCode,
                                             // spid = r.AssignedTechnicianID.ToString(),
                                             //spname = r.AssignedTechnician
                                             spid = c.ID.ToString(),// r.AssignedTechnicianID.ToString(),
                                             spname = p.UserType + ": " + ((r.Status == "Awaiting Schedule") ? c.ContractorName : c.ContractorName),
                                             srname = r.TicketManager,
                                             usertype = p.UserType
                                         }).ToList();

                    var snew = (from r in ResultResource
                                select new CallResourceDisplay
                                {
                                    id = r.Id,
                                    callid = r.CallID,
                                    ccid = r.CCID,
                                    resourceId = r.ResourceId,
                                    title = r.Title,
                                    start = string.Format("{0:s}", r.Sdate),
                                    end = string.Format("{0:s}", r.Edate),
                                    rname = r.RequesterName,
                                    contact = r.Contact,
                                    tref = r.TicketRef,
                                    details = r.Details,
                                    status = r.Status,
                                    address = r.Address,
                                    postCode = r.PostCode,
                                    backcolor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep ? GetColor("New") : GetColor(r.Status)),
                                    borderColor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep ? GetColor("New") : GetColor(r.Status)),
                                    spid = r.spid,
                                    spname = r.spname,
                                    srname = r.srname

                                }).ToList();
                    crDisplay.AddRange(snew);
                    // }

                }
                else if (acceptedUsers.Count > 0)
                {
                    var resourceid = acceptedUsers.FirstOrDefault().ResourceID;
                    acceptedUsers = CrRepository.GetAll().Where(o => (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true).ToList();
                    var Result = from r in flsList

                                 join p in acceptedUsers on r.CallID equals p.CallID
                                 join
                                //c in rlist on r.AssignedTechnicianID equals c.ID
                                c in rlist on p.ResourceID equals c.ID
                                 //where (p.IsActive.HasValue ? p.IsActive.Value : false) == true
                                 //where (p.IsAssigned.HasValue?p.IsAssigned.Value:false) == true
                                 select new
                                 {
                                     Id = p.ID,
                                     CCID = r.CCID,
                                     CallID = r.CallID,
                                     ResourceId = c.ID,
                                     Title = r.RequesterName,
                                     //Sdate = Convert.ToDateTime(r.ScheduledDateTime),
                                     //Edate = Convert.ToDateTime(r.ScheduledEndDateTime),
                                     Sdate = Convert.ToDateTime(p.StartDate),
                                     Edate = Convert.ToDateTime(p.EndDate),
                                     RequesterName = r.RequesterName,
                                     Contact = r.RequestersTelephoneNo,
                                     TicketRef = "" + r.CCID.ToString(),
                                     Details = r.Details,
                                     Status = r.Status,
                                     Address = r.RequestersAddress,
                                     PostCode = r.RequestersPostCode,
                                     spid = c.ID.ToString(),// r.AssignedTechnicianID.ToString(),
                                     spname = p.UserType + ": " + ((r.Status == "Awaiting Schedule") ? c.ContractorName : c.ContractorName),
                                      srname = r.TicketManager,
                                      usertype = p.UserType,
                                 };



                    var s = (from r in Result
                             select new CallResourceDisplay
                             {
                                 id = r.Id,
                                 callid = r.CallID,
                                 ccid = r.CCID,
                                 resourceId = r.ResourceId,
                                 title = r.Title,
                                 start = string.Format("{0:s}", r.Sdate),
                                 end = string.Format("{0:s}", r.Edate),
                                 rname = r.RequesterName,
                                 contact = r.Contact,
                                 tref = r.TicketRef,
                                 details = r.Details,
                                 status = r.Status,
                                 address = r.Address,
                                 postCode = r.PostCode,
                                 backcolor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep ? GetColor("New") : GetColor(r.Status)),
                                 borderColor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep ? GetColor("New") : GetColor(r.Status)),
                                 spid = r.spid,
                                 spname = r.spname,
                                 srname = r.srname
                             }).ToList();
                    crDisplay.AddRange(s);


                }
                else {
                    acceptedUsers = CrRepository.GetAll().Where(o => (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true).ToList();

                    var Result = from r in flsList

                                 join p in acceptedUsers on r.CallID equals p.CallID
                                 join
                                //c in rlist on r.AssignedTechnicianID equals c.ID
                                c in rlist on p.ResourceID equals c.ID
                                 //where (p.IsActive.HasValue ? p.IsActive.Value : false) == true
                                 //where (p.IsAssigned.HasValue?p.IsAssigned.Value:false) == true
                                 select new
                                 {
                                     Id = p.ID,
                                     CCID = r.CCID,
                                     CallID = r.CallID,
                                     ResourceId = c.ID,
                                     Title = r.RequesterName,
                                     //Sdate = Convert.ToDateTime(r.ScheduledDateTime),
                                     //Edate = Convert.ToDateTime(r.ScheduledEndDateTime),
                                     Sdate = Convert.ToDateTime(p.StartDate),
                                     Edate = Convert.ToDateTime(p.EndDate),
                                     RequesterName = r.RequesterName,
                                     Contact = r.RequestersTelephoneNo,
                                     TicketRef = "" + r.CCID.ToString(),
                                     Details = r.Details,
                                     Status = r.Status,
                                     Address = r.RequestersAddress,
                                     PostCode = r.RequestersPostCode,
                                     spid = c.ID.ToString(),// r.AssignedTechnicianID.ToString(),
                                     spname = p.UserType + ": " + ((r.Status == "Awaiting Schedule") ? c.ContractorName : c.ContractorName),
                                     srname =r.TicketManager,
                                     usertype = p.UserType
                                 };



                    var s = (from r in Result
                             select new CallResourceDisplay
                             {
                                 id = r.Id,
                                 callid = r.CallID,
                                 ccid = r.CCID,
                                 resourceId = r.ResourceId,
                                 title = r.Title,
                                 start = string.Format("{0:s}", r.Sdate),
                                 end = string.Format("{0:s}", r.Edate),
                                 rname = r.RequesterName,
                                 contact = r.Contact,
                                 tref = r.TicketRef,
                                 details = r.Details,
                                 status = r.Status,
                                 address = r.Address,
                                 postCode = r.PostCode,
                                 backcolor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep ? GetColor("New") : GetColor(r.Status)),
                                 borderColor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep ? GetColor("New") : GetColor(r.Status)),
                                 spid = r.spid,
                                 spname = r.spname,
                                 srname = r.srname
                             }).ToList();
                    crDisplay.AddRange(s);
                }

                var rlistnew = from c in rlist
                            select new
                            {
                                Id = c.ID,
                                Title = string.Format("{0}", c.ContractorName)
                            };
                var slist = from r in rlistnew
                            select new
                            {
                                id = r.Id,
                                title = r.Title
                            };

                var result = new { data = crDisplay, data2 = slist };
                return Jsonserializer.Serialize(result).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }

        public int GetUserIDs(string ids,int userid, string comparestr)
        {
            int s = 0;
            if (ids != null)
            {
                if (ids.Length > 0)
                {
                    foreach (var p in ids.Split(','))
                    {
                        if (p == comparestr)
                        {
                            s = userid;
                            break;
                        }
                    }
                }
            }
            return s;
        }

        public string GetColor(string status)
        {
            string retval = "#3a87ad";
            if (status == "New")
                retval = "#00B0F0";
            else if (status == "Arrived")
                retval = "#0070C0";
            else if (status == "Customer Not Responding")
                retval = "#FF0000";
            else if (status == "Job Complete")
                retval = "#0070C0";
            else if (status == "Cancelled")
                retval = "#44546a";
            else if (status == "Scheduled")
                retval = "#92D050";
            else if (status == "Awaiting Schedule")
                retval = "#B4c6e7";
            else if (status == "Feedback Submitted")
                retval = "#002060";
            else if (status == "Feedback Received")
                retval = "#ED7D31";
            else if (status == "Resolved")
                retval = "#008000";
            else if (status == "sales")
                retval = "#00B0F0";

            return retval;

        }
        [WebMethod(EnableSession = true)]
        public object GetAllEventsScheduled(string name, string status, string town, string postcode)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                List<CallResourceDisplay> crDisplay = new List<CallResourceDisplay>();
                List<Jqgrid> flsList = new List<Jqgrid>();
                List<CallResourceSchedule> callResources = new List<CallResourceSchedule>();

                flsList = FLSDetailsBAL.Jqgridlist().Where(o => o.Status != "Cancelled" && o.Status != "Job Complete").ToList();
                IDCRespository<CallResourceSchedule> CrRepository = new DCRepository<CallResourceSchedule>();
                //IDCRespository<FLSDetail> iDetails = new DCRepository<FLSDetail>();

                // List<FLSDetail> flsDetails = new List<FLSDetail>();
                List<UserMgt.Entity.Contractor> rlist = new List<UserMgt.Entity.Contractor>();
                //if (CallID.Trim().Length > 0)
                  //  flsList = flsList.ToList();
                IUserRepository<UserMgt.Entity.Contractor> urep = new UserRepository<UserMgt.Entity.Contractor>();
                IUserRepository<UserMgt.Entity.UserDetail> udrep = new UserRepository<UserMgt.Entity.UserDetail>();


                var uidlist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany();
                // Result = Result.Where(o => uidlist.Contains(o.Uid)).OrderBy(a => a.Username).ToList();
                int[] sids = { 1,4 };
                var ulist = urep.GetAll().Where(o => uidlist.Contains(o.ID) && sids.Contains(o.SID.Value)  && o.Status == UserMgt.BAL.ContractorStatus.Active).ToList();
                //ulist = urep.GetAll().Where(o => uidlist.Select(u => u).ToArray().Contains(o.ID)).ToList();
                //var uaddresslist = udrep.GetAll().ToList();

                ResourceScheduleBAL Res = new ResourceScheduleBAL();
                // var ResourceList = Res.GetAllEvents().ToList();
                if (flsList.Count > 0)
                {
                    var dv = flsList.ToList().Select(d => d.AssignedTechnicianID != null ? d.AssignedTechnicianID : 0).ToArray();
                    rlist = ulist;// Res.GetAllContractors().Where(o => dv.Contains(o.ID)).ToList();
                                   //var sids = { 1, 2, 3, 4, 9 };
                                   //rlist = Res.GetAllContractors().Where(o => sids.Contains(o.SID.Value)).ToList();
                                   //--22    New
                                   //--33    Cancelled
                                   //--35    Closed
                                   //--43    Scheduled
                                   //--44    Awaiting Schedule
                                   //--45    Arrived
                                   //--46    Customer Not Responding
                                   //--47    Feedback Submitted
                                   //--48    Feedback Received
                                   //--49    Quote Rejected
                                   //--50    Quote Accepted
                    string[] stids = {"New", "Job Complete", "Scheduled", "Arrived", "Customer Not Responding", "Feedback Submitted", "Feedback Received", "Quote Rejected", "Quote Accepted" };
                    //var callList = flsList.Where(d => d.Status == "Awaiting Schedule" && (d.AssignedTechnicianID != null ? d.AssignedTechnicianID : 0) == 0).ToList();
                    var callList = flsList.Where(d => stids.Contains(d.Status) && ( ((d.AssignedTechnicianID != null ? d.AssignedTechnicianID : 0) > 0) || ((d.TicketManagerID != null ? d.TicketManagerID : 0) > 0))).ToList();
                    if (callList != null)
                    {
                        callResources = CrRepository.GetAll().Where(p => (p.IsAssigned.HasValue ? p.IsAssigned.Value : false) == true).Where(o => callList.Select(p => p.CallID).ToArray().Contains(o.CallID.Value)).ToList();
                    }

                }


                var Result = from r in flsList

                             join p in callResources on r.CallID equals p.CallID
                             join
                            //c in rlist on r.AssignedTechnicianID equals c.ID
                            c in rlist on p.ResourceID equals c.ID
                             where (p.IsAssigned.HasValue ? p.IsAssigned.Value : false) == true
                             select new
                             {
                                 Id = p.ID,
                                 CCID = r.CCID,
                                 CallID = r.CallID,
                                 ResourceId = c.ID,
                                 Title = r.RequesterName,
                                 //Sdate = Convert.ToDateTime(r.ScheduledDateTime),
                                 //Edate = Convert.ToDateTime(r.ScheduledEndDateTime),
                                 Sdate = Convert.ToDateTime(p.StartDate),
                                 Edate = Convert.ToDateTime(p.EndDate),
                                 RequesterName = r.RequesterName,
                                 Contact = r.RequestersTelephoneNo,
                                 TicketRef = "" + r.CCID.ToString(),
                                 Details = r.Details,
                                 Status = r.Status,
                                 Address = r.RequestersAddress,
                                 PostCode = r.RequestersPostCode,
                                 spid = c.ID.ToString(),// r.AssignedTechnicianID.ToString(),
                                 spname =  (r.Status == "Awaiting Schedule") ? c.ContractorName : c.ContractorName,// r.AssignedTechnician,
                                 SPContact = c.ContactNumber,
                                 rtype = r.TypeofRequest,
                                 usertype= p.UserType
                             };



                var s = (from r in Result
                         select new CallResourceDisplay
                         {
                             id = r.Id,
                             callid = r.CallID,
                             ccid = r.CCID,
                             resourceId = r.ResourceId,
                             title = r.Title,
                             start = string.Format("{0:s}", r.Sdate),
                             end = string.Format("{0:s}", r.Edate),
                             rname = r.RequesterName,
                             contact = r.Contact,
                             tref = r.TicketRef,
                             details = r.Details,
                             status = r.Status,
                             address = r.Address,
                             postCode = r.PostCode,
                             backcolor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep?  GetColor("New") : GetColor(r.Status)),
                             borderColor = (r.usertype == CallResourceScheduleBAL.Usertype_SmartRep ? GetColor("New") : GetColor(r.Status)),
                             spid = r.spid,
                             spname = r.spname,
                             spcontact = r.SPContact,
                             rtype = r.rtype,
                             usertype = r.usertype
                         }).ToList();
                crDisplay.AddRange(s);


                //Get list of unasinged resource
                //if (callResources != null)
                //{
                //    if (callResources.Where(o => (o.IsActive.HasValue ? o.IsActive.Value : false) != false).Count() > 0)
                //    {
                //        var ResultResource = from r in flsList
                //                             join p in callResources on r.CallID equals p.CallID
                //                             //join c in rlist on p.ResourceID equals c.ID
                //                             select new
                //                             {
                //                                 Id = p.ID,
                //                                 CCID = r.CCID,
                //                                 CallID = r.CallID,
                //                                 ResourceId = p.ResourceID,
                //                                 Title = r.RequesterName,
                //                                 Sdate = p.StartDate,
                //                                 Edate = p.EndDate,
                //                                 RequesterName = r.RequesterName,
                //                                 Contact = r.RequestersTelephoneNo,
                //                                 TicketRef = "" + r.CCID.ToString(),
                //                                 Details = r.Details,
                //                                 Status = r.Status,
                //                                 Address = r.RequestersAddress,
                //                                 PostCode = r.RequestersPostCode,
                //                                 // spid = r.AssignedTechnicianID.ToString(),
                //                                 //spname = r.AssignedTechnician
                //                                 spid = p.ResourceID.ToString(),// r.AssignedTechnicianID.ToString(),
                //                                 spname = (r.Status == "Awaiting Schedule") ? rlist.Where(o => o.ID == p.ResourceID).FirstOrDefault().ContractorName : rlist.Where(o => o.ID == p.ResourceID).FirstOrDefault().ContractorName,
                //                                 spcontact = (rlist.Where(o=>o.ID == p.ResourceID).FirstOrDefault().ContactNumber) 
                //                             };

                //        var snew = (from r in ResultResource
                //                    select new CallResourceDisplay
                //                    {
                //                        id = r.Id,
                //                        callid = r.CallID,
                //                        ccid = r.CCID,
                //                        resourceId = r.ResourceId.Value,
                //                        title = r.Title,
                //                        start = string.Format("{0:s}", r.Sdate),
                //                        end = string.Format("{0:s}", r.Edate),
                //                        rname = r.RequesterName,
                //                        contact = r.Contact,
                //                        tref = r.TicketRef,
                //                        details = r.Details,
                //                        status = r.Status,
                //                        address = r.Address,
                //                        postCode = r.PostCode,
                //                        backcolor = GetColor(r.Status),
                //                        borderColor = GetColor(r.Status),
                //                        spid = r.spid,
                //                        spname = r.spname,
                //                        spcontact = r.spcontact
                //                    }).ToList();
                        
                //        crDisplay.AddRange(snew);
                //    }

                //}
               

                var rlistnew = from c in rlist
                               select new
                               {
                                   Id = c.ID,
                                   Title = string.Format("{0}", c.ContractorName)
                               };
                var slist = from r in rlistnew
                            select new
                            {
                                id = r.Id,
                                title = r.Title

                            };

                if (name != "Please select..." && !string.IsNullOrEmpty(name))
                    crDisplay = crDisplay.Where(o => o.rname == name).ToList();
                if (status != "Please select..." && !string.IsNullOrEmpty(status))
                    crDisplay = crDisplay.Where(o => o.status == status).ToList();
                if (town != "Please select..." && !string.IsNullOrEmpty(town))
                {
                    if (town.Length > 0)
                    {
                        var towns = town.Split(',').ToList();
                        crDisplay = crDisplay.Where(o => towns.Contains(o.town)).ToList();
                    }
                }
                if (postcode != "Please select..." && !string.IsNullOrEmpty(postcode))
                {
                    if (postcode.Length > 0)
                    {
                        var postcodes = postcode.Split(',').ToList();
                        crDisplay = crDisplay.Where(o => postcodes.Contains(o.postCode)).ToList();
                    }
                }




                var result = new { data = crDisplay.Distinct().ToList(), data2 = slist };
                return Jsonserializer.Serialize(result).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }

       
        [WebMethod(EnableSession = true)]
        public void UpdateResEvent(int ID, string StartDate, string EndtDate, int ResId,string callid)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                int ccid = Convert.ToInt32(callid.Replace("TN:", "")); 
                IDCRespository<FLSDetail> fReposit = new DCRepository<FLSDetail>();
                IDCRespository<CallResourceSchedule> dReposit = new DCRepository<CallResourceSchedule>();

                var rEntity = dReposit.GetAll().Where(o => o.ID == ID).FirstOrDefault();

                var callref =FLSDetailsBAL.GetCallID(Convert.ToInt32(ccid));
                var cDetails = fReposit.GetAll().Where(o => o.CallID == callref).FirstOrDefault();
                var rdetails = dReposit.GetAll().Where(o => o.CallID == callref).ToList();

                DateTime sdt = DateTime.Parse(StartDate);
                DateTime EventStartDate = DateTime.Parse(sdt.ToString("yyyy-MM-dd HH:mm:ss"));

                DateTime edt = DateTime.Parse(EndtDate);
                DateTime EventEndDate = DateTime.Parse(edt.ToString("yyyy-MM-dd HH:mm:ss"));

                if(cDetails != null)
                {
                    cDetails.ScheduledDate = EventStartDate;
                    cDetails.ScheduledEndDateTime = EventEndDate;
                    cDetails.UserID = ResId;
                    //Update start date and end date
                    fReposit.Edit(cDetails);
                }

                if(rdetails != null)
                {
                    //var resDetails = rdetails.Where(o => o.ResourceID == ResId && o.CallID == callref).FirstOrDefault();
                    var resDetails = rdetails.Where(o =>  o.CallID == callref && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true).FirstOrDefault();
                    if (resDetails != null)
                    {
                        resDetails.StartDate = EventStartDate;
                        resDetails.EndDate = EventEndDate;
                        resDetails.ResourceID = ResId;
                        //Update start date and end date
                        dReposit.Edit(resDetails);
                    }
                    //update ticket schedule data with the max date 
                    //var resList = rdetails.Where(o => o.CallID == callref && (o.IsAssigned.HasValue?o.IsAssigned.Value:false)== true).ToList();
                    //if (cDetails != null)
                    //{
                    //    if (resList.Count > 0)
                    //    {
                    //        cDetails.ScheduledDate = resList.Select(o=>o.StartDate).Min();
                    //        cDetails.ScheduledEndDateTime = resList.Select(o => o.EndDate).Max();
                    //        //cDetails.UserID = ResId;
                    //        //Update start date and end date
                    //        fReposit.Edit(cDetails);
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        [WebMethod(EnableSession = true)]
        public object GetAllResources()
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                ResourceScheduleBAL Res = new ResourceScheduleBAL();
                var ResourceList = Res.GetAllEvents().ToList();
                var ContractorList = Res.GetAllContractors().ToList();

                var Result = from c in ContractorList
                             select new
                             {
                                 Id = c.ID,
                                 Title = string.Format("<img style=color:red src='../UploadData/Users/ThumbNailsMedium/user_{1}.png' alt='Image' width='35px'/> {0}", c.ContractorName,c.ID)
                             };
                var s = from r in Result
                        select new
                        {
                            id = r.Id,
                            title = r.Title
                        };
                return Jsonserializer.Serialize(s).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        public void DeleteEvent(int id)
        {
            try
            {
                ResourceScheduleBAL Res = new ResourceScheduleBAL();
                var ResourceList = Res.GetAllEvents().ToList();
                if (id != 0)
                {
                    ResourceSchedule EventData = Res.GetAllEvents().Where(o => o.ID == id).FirstOrDefault();
                    if (EventData != null)
                    {
                        Res.DeleteEvent(EventData);
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //SendCustomerMail
        [WebMethod(EnableSession = true)]
        public void SendCustomerMail(int id)
        {
            try
            {
                //ResourceScheduleBAL Res = new ResourceScheduleBAL();
                //var ResourceList = Res.GetAllEvents().ToList();
                if (id != 0)
                {
                    var cEntity = FLSDetailsBAL.GetTicketByAssingedResourceID(id);
                    if (cEntity != null)
                        sendmailtoCustomer(cEntity.CallID.Value.ToString(), string.Empty);
                    //ResourceSchedule EventData = Res.GetAllEvents().Where(o => o.ID == id).FirstOrDefault();
                    //if (EventData != null)
                    //{
                    //    //Res.DeleteEvent(EventData);
                        
                    //}
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //SendCustomerMail
        [WebMethod(EnableSession = true)]
        public void SendTechMail(int id)
        {
            try
            {
                //ResourceScheduleBAL Res = new ResourceScheduleBAL();
                //var ResourceList = Res.GetAllEvents().Where(o=>o.).ToList();
                if (id != 0)
                {
                    var cEntity = FLSDetailsBAL.GetTicketByAssingedResourceID(id);
                    if(cEntity != null)
                    MailSendingToAssignResource(cEntity.CallID.Value.ToString(),cEntity.ResourceID.Value);
                    //ResourceSchedule EventData = Res.GetAllEvents().Where(o => o.ID == id).FirstOrDefault();
                    //if (EventData != null)
                    //{
                    //    //Res.DeleteEvent(EventData);

                    //}
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void sendmailtoCustomer(string ticketno, string resourceid)
        {

            try
            {
                //var ccid = ticketno;
                var ccid = FLSDetailsBAL.GetCallIDByCustomer(Convert.ToInt32(ticketno)).ToString();
                var flsdata = FLSDetailsBAL.Jqgridlist(Convert.ToInt32(ticketno)).FirstOrDefault();
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                EmailFooter ef = new EmailFooter();
                //6 FLS
                ef = FooterEmail.EmailFooter_selectByID(6, 1);
                Emailer em = new Emailer();
                string body = em.ReadFile("~/WF/DC/EmailTemplates/CalltoResource.htm");
                string subject = string.Empty;
                subject = "Job Reference:" + ccid.ToString();
                body = body.Replace("[mail_head]", "Job");

                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                body = body.Replace("[user]", flsdata.RequesterName);
                body = body.Replace("[date]", string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), flsdata.ScheduledDateTime) +" - "+ string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), flsdata.ScheduledEndDateTime));
                body = body.Replace("[name]", flsdata.AssignedTechnician);
                //LogExceptions.LogException(Deffinity.systemdefaults.GetWebUrl() + string.Format("/WF/UploadData/Users/ThumbNails/user_{0}.png", flsdata.AssignedTechnicianID));
                //body = body.Replace("[src]", Deffinity.systemdefaults.GetWebUrl() + string.Format("/WF/UploadData/Users/ThumbNails/user_{0}.png", flsdata.AssignedTechnicianID));
                body = body.Replace("[img]", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + string.Format("/WF/Admin/ImageHandler.ashx?type=user&id={0}", flsdata.AssignedTechnicianID) + "'/>");
                body = body.Replace("[email]", flsdata.AssignedTechnicianEmail);
                body = body.Replace("[mobile]", flsdata.AssignedTechnicianContact);
                body = body.Replace("[img_arrived]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_serviceproviderhasarrived.png");

                body = body.Replace("[Url_arrived]",
                              Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + flsdata.AssignedTechnicianID.ToString() + "&cid=" + ticketno + "&sta=false&type=arrived");

                body = body.Replace("[instancename]", Deffinity.systemdefaults.GetInstanceTitle());
                body = body.Replace("[footer]", Server.HtmlDecode(ef == null ? string.Empty : ef.EmailFooter1));

                foreach (var s in flsdata.RequestersEmailAddress.Split(','))
                {
                    if (!string.IsNullOrEmpty(s))
                        em.SendingMail(fromemailid, subject, body, s.Trim());
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        public void MailSendingToAssignResource(string ticketno,int ResourceID)
        {
            try
            {
                //var ccid = ticketno;
                var ccid = FLSDetailsBAL.GetCallIDByCustomer(Convert.ToInt32(ticketno)).ToString();
                var flsdata = FLSDetailsBAL.Jqgridlist(Convert.ToInt32(ticketno)).FirstOrDefault();
                string rids = ResourceID.ToString();// flsdata.AssignedTechnicianID.ToString();
                if (rids.Length > 0)
                {
                    //44	Awaiting Technician
                    //cd.StatusID = 44;
                    //CallDetailsBAL.CallDetailsUpdate(cd);
                    string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                    Emailer em = new Emailer();
                    string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSAssignedResourceMail.html");
                    body = body.Replace("[mail_head]", "Job");
                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                    //body = body.Replace("[img]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_yes-accept-and-schedule.png");
                    body = body.Replace("[img_reject]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_reject-this-job.png");
                    body = body.Replace("[img_imhere]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_im-here-let-the-customer-know.png");
                    body = body.Replace("[img_rate]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_submit-an-invoice.png");
                    //body = body.Replace("[img_invoice]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_submit-an-invoice.png");
                    body = body.Replace("[img_close]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_close-this-job.png");
                    body = body.Replace("[img_customernot]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_customer-not-responding.png");
                    body = body.Replace("[img_arrived]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_arrived.png");
                    body = body.Replace("[img_direction]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/GoogleDirections.png");
                    //FlsTicketAcceptMail.html[img]
                    using (PortfolioDataContext pd = new PortfolioDataContext())
                    {
                        if (ResourceID > 0)
                        {
                            var pr = pd.PortfolioContactAddresses.Where(o => o.ID == flsdata.ContactAddressID).FirstOrDefault();
                            body = body.Replace("[address]", pr.Address + ", " + pr.City + ", " + pr.State + ", " + pr.PostCode);
                        }
                        else
                        {
                            var pr = pd.PortfolioContacts.Where(o => o.ID == flsdata.RequesterID).FirstOrDefault();
                            body = body.Replace("[address]", pr.Address1 + ", " + pr.Town + ", " + pr.City + ", " + pr.Postcode);
                        }
                    }

                    string ProductsTable = string.Empty;
                    using (DCDataContext Dc = new DCDataContext())
                    {
                        CallIdAssociatedProduct callidProducts = Dc.CallIdAssociatedProducts.Where(c => c.Callid == Convert.ToInt32(ticketno)).FirstOrDefault();
                        if (callidProducts != null)
                        {
                            string[] ProductIds = callidProducts.ProductIds.Split(',');
                            ProductsTable = " <table style='width:100%'><thead><tr style='background-color:silver;color:white;text-align:center;font-weight:bold;'><th>Product Type</th><th>Make </th><th>Model </th></tr></thead>";
                            using (AssetsToSoftwareDataContext Asset = new AssetsToSoftwareDataContext())
                            {
                                var AssetList = Asset.V_Assets.ToList();
                                foreach (string pId in ProductIds)
                                {
                                    var aSingle = AssetList.Where(v => v.ID == int.Parse(pId)).FirstOrDefault();
                                    if (aSingle != null)
                                    {
                                        ProductsTable = ProductsTable
                                            + "<tr><td>" + aSingle.TypeName + "</td><td>" + aSingle.MakeName
                                            + "</td><td>" + aSingle.ModelName + "</td> </tr>";
                                    }
                                }
                            }
                            ProductsTable = ProductsTable + " </table>";
                            body = body.Replace("[ProductsTable]", ProductsTable);
                        }
                        else
                        {
                            body = body.Replace("[ProductsTable]", string.Empty);
                        }
                    }
                    //ticketref
                    body = body.Replace("[ticketref]", Deffinity.systemdefaults.GetCallPrefix() + ccid);
                    body = body.Replace("[ticketdescription]", flsdata.Details);

                    string cbody = string.Empty;
                    using (UserMgt.DAL.UserDataContext Pdc = new UserMgt.DAL.UserDataContext())
                    {
                        var dfRepository = new DCRepository<ServiceProviderScheduling>();

                        var userlist = Pdc.Contractors.Where(p => rids.Split(',').ToArray().Contains(p.ID.ToString())).ToList();
                        int cnt = 0;
                        foreach (var c in userlist)
                        {
                            cnt = cnt + 1;
                            cbody = body;

                            cbody = cbody.Replace("[resourcename]", c.ContractorName);

                            cbody = cbody.Replace("[ArrivalUrl]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + ticketno + "&sta=true&type=arrival");
                            cbody = cbody.Replace("[RejectUrl]",
                                Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + ticketno + "&sta=false&type=reject");
                            cbody = cbody.Replace("[Url_imhere]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + ticketno + "&sta=false&type=imhere");
                            cbody = cbody.Replace("[Url_rate]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + ticketno + "&sta=false&type=rate");
                            cbody = cbody.Replace("[Url_invoice]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + ticketno + "&sta=false&type=invoice");
                            cbody = cbody.Replace("[Url_close]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + ticketno + "&sta=false&type=close");
                            cbody = cbody.Replace("[Url_arrived]",
                              Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + ticketno + "&sta=false&type=arrived");
                            cbody = cbody.Replace("[Url_customernot]",
                              Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + ticketno + "&sta=false&type=customernot");
                            cbody = cbody.Replace("[Url_direction]",
                           Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/Gmap.aspx?rid=" + c.ID + "&cid=" + ticketno + "&sta=false&type=direction");

                            try
                            {

                                em.SendingMail(Deffinity.systemdefaults.GetFromEmail(), "Resource Email", cbody, c.EmailAddress);
                            }
                            catch (Exception ex)
                            { LogExceptions.WriteExceptionLog(ex); }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}
