using Newtonsoft.Json.Linq;
//using SpaWizard.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Http;
using System.Web.Security;
using UserMgt.Entity;
using ProjectMgt.Entity;
//using System.Web.Http.Cors;
using UserMgt.BAL;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.BAL;
using DeffinityManager.DC.BLL;
using DC.BAL;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using DC.BLL;
using DC.Entity;
using DC.DAL;
using Deffinity.PortfolioManager;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;

namespace DeffinityAppDev.Controllers
{
    public class listitem
    {
        public string Text { set; get; }
        public string Value { set; get; }
    }
    [AllowAnonymous]
    public class InstanceController : ApiController
    {
        public static string webName = "Plegit";
        // static readonly IInsatanceRepository repository = new InstanceRepository();

        //IUserRepository<P> userReporsitory = null;
        IUserRepository<Contractor> cRep = null;
        IUserRepository<v_contractor> cvRep = null;
        IUserRepository<UserMgt.Entity.Company> cmRep = null;
        IUserRepository<UserMgt.Entity.UserToCompany> cuRep = null;
        IUserRepository<UserMgt.Entity.UserDetail> usRep = null;
        IPortfolioRepository<ProjectPortfolio> poRep = null;
        IPortfolioRepository<PortfolioContact> pcRep = null;
        IPortfolioRepository<PortfolioContactAddress> pcaRep = null;

        public InstanceController()
        {
            cRep = new UserRepository<Contractor>();
            cvRep = new UserRepository<v_contractor>();
            cmRep = new UserRepository<UserMgt.Entity.Company>();
            cuRep = new UserRepository<UserMgt.Entity.UserToCompany>();
            poRep = new PortfolioRepository<ProjectPortfolio>();
            pcRep = new PortfolioRepository<PortfolioContact>();
            pcaRep = new PortfolioRepository<PortfolioContactAddress>();
            usRep = new UserRepository<UserMgt.Entity.UserDetail>();

        }

        public class signupdetails
        {
            public string Firstname { get; set; }
            public string CompanyName { get; set; }
            public string EmailAddress { get; set; }
            public string MobileNumber { get; set; }
            public string Password { get; set; }
            public string Industry { get; set; }
            public string url { get; set; }
            public string partner { get; set; }
            public string EcoSystem { get; set; }
            public string organization { get; set; }
            public string faith { get; set; }

            public string group { get; set; }

            public string denomination { get; set; }
            public int sid { get; set; }
        }


        public class accountdetails
        {
            public string fullname { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string field_8 { get; set; }
            public string field_6 { get; set; }
           
        }


        
        [AllowAnonymous]
        [HttpGet]
        // [EnableCors(origins: "*", headers: "*", methods: "*")]
        public object getdata()
        {


            return "test";

        }

        [Route("api/instance/memberserviceaccount")]
        [AllowAnonymous]
        [HttpPost]
       // [EnableCors(origins: "*", headers: "*", methods: "*")]
        public object member_serviceaccount_instance_account(JObject data)
        {


            try
            {
                if (data != null)
                {
                    Email em = new Email();
                    // em.SendingMail("indra@deffinity.com", "web method from start active account", "web method from start active account");
                    LogExceptions.LogException("web method from start active account");
                    string firstname = string.Empty;
                    string lastname = string.Empty;
                    string email = string.Empty;
                    string phone = string.Empty;
                    string company = string.Empty;
                    string industry = string.Empty;
                    string organization = string.Empty;
                    string faith = string.Empty;
                    string group = string.Empty;
                    string denomination = string.Empty;

                    try
                    {
                        LogExceptions.LogException("serial");
                        LogExceptions.LogException("ser:", JsonConvert.SerializeObject(data));
                    }
                    catch (Exception ex)
                    {
                        var s = "errr";
                    }

                    // LogExceptions.LogException("json serial" + data.ToString());

                    if (data["contact"]["first_name"] != null)
                    {
                        LogExceptions.LogException(" first name:" + data["contact"]["first_name"]);
                        firstname = data["contact"]["first_name"].ToString();
                    }

                    if (data["contact"]["last_name"] != null)
                    {
                        LogExceptions.LogException(" last name:" + data["contact"]["last_name"]);
                        lastname = data["contact"]["last_name"].ToString();
                    }
                    if (data["contact"]["email"] != null)
                    {
                        LogExceptions.LogException(" email:" + data["contact"]["email"]);
                        email = data["contact"]["email"].ToString();
                    }

                    if (data["contact"]["phone"] != null)
                    {
                        LogExceptions.LogException(" phone:" + data["contact"]["phone"]);
                        phone = data["contact"]["phone"].ToString();
                    }


                    if (data["contact"]["fields"] != null)
                    {
                        if (data["contact"]["fields"]["company_name"] != null)
                        {
                            LogExceptions.LogException(" company:" + data["contact"]["fields"]["company_name"]);
                            company = data["contact"]["fields"]["company_name"].ToString();
                        }
                        if (data["contact"]["fields"]["industry"] != null)
                        {
                            LogExceptions.LogException(" industry:" + data["contact"]["fields"]["industry"]);
                            industry = data["contact"]["fields"]["industry"].ToString();
                        }

                        if (data["contact"]["fields"]["organization_name"] != null)
                        {
                            LogExceptions.LogException(" organization_name:" + data["contact"]["fields"]["organization_name"]);
                            organization = data["contact"]["fields"]["organization_name"].ToString();
                        }

                        if (data["contact"]["fields"]["faith"] != null)
                        {
                            LogExceptions.LogException(" faith:" + data["contact"]["fields"]["faith"]);
                            faith = data["contact"]["fields"]["faith"].ToString();
                        }
                    }

                    var acc = cRep.GetAll().Where(o => o.LoginName.ToLower() == email.ToLower() && o.SID != UserType.Donor ).FirstOrDefault();
                    //if (cRep.GetAll().Where(o => o.LoginName.ToLower() == email.ToLower() && o.Status == UserStatus.Active && o.SID == UserType.SmartPros).Count() == 0)
                    if (acc == null)
                    {
                        em.SendingMail("indra@deffinity.com", "web method from start set user account", "web method from start set user account");
                        LogExceptions.LogException("web method from start set user account");
                        signupdetails cs = new signupdetails();
                        cs.CompanyName = company;
                        cs.EmailAddress = email;
                        cs.Firstname = firstname + " " + lastname;
                        cs.Industry = industry;
                        cs.MobileNumber = phone;
                        cs.url = "https://portal.plegitafrica.com";
                        cs.partner = "partner1";
                        cs.EcoSystem = "Member Service";
                        cs.organization = organization;
                        cs.faith =  faith;
                        cs.group = group;
                        cs.denomination = denomination;
                        cs.sid = 3;
                        InsertContractor1(cs,false,false,false,true);


                    }
                    return data;

                    LogExceptions.LogException("web method from end active account");

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return data;

        }

        [Route("api/instance/memberaccount")]
        [AllowAnonymous]
        [HttpPost]
       // [EnableCors(origins: "*", headers: "*", methods: "*")]
        public object member_instance_account(JObject data)
        {


            try
            {
                if (data != null)
                {
                    Email em = new Email();
                    // em.SendingMail("indra@deffinity.com", "web method from start active account", "web method from start active account");
                    LogExceptions.LogException("web method from start active account");
                    string firstname = string.Empty;
                    string lastname = string.Empty;
                    string email = string.Empty;
                    string phone = string.Empty;
                    string company = string.Empty;
                    string industry = string.Empty;
                    string organization = string.Empty;
                    string faith = string.Empty;
                    string group = string.Empty;
                    string denomination = string.Empty;

                    try
                    {
                        LogExceptions.LogException("serial");
                        LogExceptions.LogException("ser:", JsonConvert.SerializeObject(data));
                    }
                    catch (Exception ex)
                    {
                        var s = "errr";
                    }

                    // LogExceptions.LogException("json serial" + data.ToString());

                    if (data["contact"]["first_name"] != null)
                    {
                        LogExceptions.LogException(" first name:" + data["contact"]["first_name"]);
                        firstname = data["contact"]["first_name"].ToString();
                    }

                    if (data["contact"]["last_name"] != null)
                    {
                        LogExceptions.LogException(" last name:" + data["contact"]["last_name"]);
                        lastname = data["contact"]["last_name"].ToString();
                    }
                    if (data["contact"]["email"] != null)
                    {
                        LogExceptions.LogException(" email:" + data["contact"]["email"]);
                        email = data["contact"]["email"].ToString();
                    }

                    if (data["contact"]["phone"] != null)
                    {
                        LogExceptions.LogException(" phone:" + data["contact"]["phone"]);
                        phone = data["contact"]["phone"].ToString();
                    }


                    if (data["contact"]["fields"] != null)
                    {
                        if (data["contact"]["fields"]["company_name"] != null)
                        {
                            LogExceptions.LogException(" company:" + data["contact"]["fields"]["company_name"]);
                            company = data["contact"]["fields"]["company_name"].ToString();
                        }
                        if (data["contact"]["fields"]["industry"] != null)
                        {
                            LogExceptions.LogException(" industry:" + data["contact"]["fields"]["industry"]);
                            industry = data["contact"]["fields"]["industry"].ToString();
                        }
                        if (data["contact"]["fields"]["organization_name"] != null)
                        {
                            LogExceptions.LogException(" organization_name:" + data["contact"]["fields"]["organization_name"]);
                            organization = data["contact"]["fields"]["organization_name"].ToString();
                        }

                        if (data["contact"]["fields"]["faith"] != null)
                        {
                            LogExceptions.LogException(" faith:" + data["contact"]["fields"]["faith"]);
                            faith = data["contact"]["fields"]["faith"].ToString();
                        }
                        if (data["contact"]["fields"]["group"] != null)
                        {
                            LogExceptions.LogException(" group:" + data["contact"]["fields"]["group"]);
                            group = data["contact"]["fields"]["group"].ToString();
                        }
                        if (data["contact"]["fields"]["group"] != null)
                        {
                            LogExceptions.LogException(" denomination:" + data["contact"]["fields"]["denomination"]);
                            denomination = data["contact"]["fields"]["denomination"].ToString();
                        }
                    }

                    var acc = cRep.GetAll().Where(o => o.LoginName.ToLower() == email.ToLower() && o.SID != UserType.Donor ).FirstOrDefault();
                    //if (cRep.GetAll().Where(o => o.LoginName.ToLower() == email.ToLower() && o.Status == UserStatus.Active && o.SID == UserType.SmartPros).Count() == 0)
                    if (acc == null)
                    {
                        em.SendingMail("indra@deffinity.com", "web method from start set user account", "web method from start set user account");
                        LogExceptions.LogException("web method from start set user account");
                        signupdetails cs = new signupdetails();
                        cs.CompanyName = company;
                        cs.EmailAddress = email;
                        cs.Firstname = firstname + " " + lastname;
                        cs.Industry = industry;
                        cs.MobileNumber = phone;
                        cs.url = "https://site.plegit.co.uk";
                        cs.partner = "partner1";
                        cs.EcoSystem = "Member";
                        cs.organization = organization;
                        cs.faith = "Christianity";// faith;
                        cs.group = group;
                        cs.denomination = denomination;
                        cs.sid = 4;
                        InsertContractor1(cs,false,false,true);


                    }
                    return data;

                    LogExceptions.LogException("web method from end active account");

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return data;

        }

        [Route("api/instance/groupaccount")]
        [AllowAnonymous]
        [HttpPost]
      //  [EnableCors(origins: "*", headers: "*", methods: "*")]
        public object Group_instance_account(JObject data)
        {


            try
            {
                if (data != null)
                {
                    Email em = new Email();
                    // em.SendingMail("indra@deffinity.com", "web method from start active account", "web method from start active account");
                    LogExceptions.LogException("web method from start active account");
                    string firstname = string.Empty;
                    string lastname = string.Empty;
                    string email = string.Empty;
                    string phone = string.Empty;
                    string company = string.Empty;
                    string industry = string.Empty;
                    string organization = string.Empty;
                    string faith = string.Empty;
                    string group = string.Empty;
                    string denomination = string.Empty;


                    try
                    {
                        LogExceptions.LogException("serial");
                        LogExceptions.LogException("ser:", JsonConvert.SerializeObject(data));
                    }
                    catch (Exception ex)
                    {
                        var s = "errr";
                    }

                    // LogExceptions.LogException("json serial" + data.ToString());

                    if (data["contact"]["first_name"] != null)
                    {
                        LogExceptions.LogException(" first name:" + data["contact"]["first_name"]);
                        firstname = data["contact"]["first_name"].ToString();
                    }

                    if (data["contact"]["last_name"] != null)
                    {
                        LogExceptions.LogException(" last name:" + data["contact"]["last_name"]);
                        lastname = data["contact"]["last_name"].ToString();
                    }
                    if (data["contact"]["email"] != null)
                    {
                        LogExceptions.LogException(" email:" + data["contact"]["email"]);
                        email = data["contact"]["email"].ToString();
                    }

                    if (data["contact"]["phone"] != null)
                    {
                        LogExceptions.LogException(" phone:" + data["contact"]["phone"]);
                        phone = data["contact"]["phone"].ToString();
                    }


                    if (data["contact"]["fields"] != null)
                    {
                        if (data["contact"]["fields"]["company_name"] != null)
                        {
                            LogExceptions.LogException(" company:" + data["contact"]["fields"]["company_name"]);
                            company = data["contact"]["fields"]["company_name"].ToString();
                        }
                        if (data["contact"]["fields"]["industry"] != null)
                        {
                            LogExceptions.LogException(" industry:" + data["contact"]["fields"]["industry"]);
                            industry = data["contact"]["fields"]["industry"].ToString();
                        }
                        if (data["contact"]["fields"]["organization_name"] != null)
                        {
                            LogExceptions.LogException(" organization_name:" + data["contact"]["fields"]["organization_name"]);
                            organization = data["contact"]["fields"]["organization_name"].ToString();
                        }

                        if (data["contact"]["fields"]["faith"] != null)
                        {
                            LogExceptions.LogException(" faith:" + data["contact"]["fields"]["faith"]);
                            faith = data["contact"]["fields"]["faith"].ToString();
                        }
                        if (data["contact"]["fields"]["group"] != null)
                        {
                            LogExceptions.LogException(" group:" + data["contact"]["fields"]["group"]);
                            group = data["contact"]["fields"]["group"].ToString();
                        }
                        if (data["contact"]["fields"]["group"] != null)
                        {
                            LogExceptions.LogException(" denomination:" + data["contact"]["fields"]["denomination"]);
                            denomination = data["contact"]["fields"]["denomination"].ToString();
                        }
                    }

                    var acc = cRep.GetAll().Where(o => o.LoginName.ToLower() == email.ToLower() && o.SID != UserType.Donor ).FirstOrDefault();
                    //if (cRep.GetAll().Where(o => o.LoginName.ToLower() == email.ToLower() && o.Status == UserStatus.Active && o.SID == UserType.SmartPros).Count() == 0)
                    if (acc == null)
                    {
                        em.SendingMail("indra@deffinity.com", "web method from start set user account", "web method from start set user account");
                        LogExceptions.LogException("web method from start set user account");
                        signupdetails cs = new signupdetails();
                        cs.CompanyName = organization;// company;
                        cs.EmailAddress = email;
                        cs.Firstname = firstname + " " + lastname;
                        cs.Industry = industry;
                        cs.MobileNumber = phone;
                        cs.url = "https://site.plegit.co.uk";
                        cs.partner = "partner1";
                        cs.EcoSystem = "Group";
                        cs.organization = organization;
                        cs.faith = "Christianity";// faith;
                        cs.group = group;
                        cs.denomination = denomination;
                        cs.sid = 2;
                        InsertContractor1(cs, false,true,false,true);


                    }
                    return data;

                    LogExceptions.LogException("web method from end active account");

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return data;

        }

        [Route("api/instance/organizationaccount")]
        [AllowAnonymous]
        [HttpPost]
       // [EnableCors(origins: "*", headers: "*", methods: "*")]
        public object instance_account(JObject data, string type="")
        {
            
            
            try
            {
                if (data != null)
                {
                    Email em = new Email();
                   // em.SendingMail("indra@deffinity.com", "web method from start active account", "web method from start active account");
                    LogExceptions.LogException("web method from start active account");
                    string firstname=string.Empty;
                    string lastname = string.Empty;
                    string email= string.Empty;
                    string phone=string.Empty;
                    string company = string.Empty;
                    string industry =string.Empty;
                    string organization = string.Empty;
                    string faith = string.Empty;
                    string group = type;
                    string denomination = "Charity";

                    try
                    {
                        LogExceptions.LogException("serial");
                        LogExceptions.LogException("ser:", JsonConvert.SerializeObject(data));
                    }
                    catch (Exception ex)
                    {
                        var s = "errr";
                    }

                    // LogExceptions.LogException("json serial" + data.ToString());

                    if (data["contact"]["first_name"] != null)
                    {
                        LogExceptions.LogException(" first name:" + data["contact"]["first_name"]);
                        firstname = data["contact"]["first_name"].ToString();
                    }

                    if (data["contact"]["last_name"] != null)
                    {
                        LogExceptions.LogException(" last name:" + data["contact"]["last_name"]);
                        lastname = data["contact"]["last_name"].ToString();
                    }
                    if (data["contact"]["email"] != null)
                    {
                        LogExceptions.LogException(" email:" + data["contact"]["email"]);
                        email = data["contact"]["email"].ToString();
                    }

                    if (data["contact"]["phone"] != null)
                    {
                        LogExceptions.LogException(" phone:" + data["contact"]["phone"]);
                        phone = data["contact"]["phone"].ToString();
                    }

                  
                    if (data["contact"]["fields"] != null)
                    {
                        if (data["contact"]["fields"]["company_name"] != null)
                        {
                            LogExceptions.LogException(" company:" + data["contact"]["fields"]["company_name"]);
                            company = data["contact"]["fields"]["company_name"].ToString();
                        }
                        if (data["contact"]["fields"]["industry"] != null)
                        {
                            LogExceptions.LogException(" industry:" + data["contact"]["fields"]["industry"]);
                            industry = data["contact"]["fields"]["industry"].ToString();
                        }
                        if (data["contact"]["fields"]["organization_name"] != null)
                        {
                            LogExceptions.LogException(" organization_name:" + data["contact"]["fields"]["organization_name"]);
                            organization = data["contact"]["fields"]["organization_name"].ToString();
                        }

                        if (data["contact"]["fields"]["average_donations_per_month"] != null)
                        {
                            LogExceptions.LogException(" faith:" + data["contact"]["fields"]["average_donations_per_month"]);
                            faith = data["contact"]["fields"]["average_donations_per_month"].ToString();
                        }
                        //if (data["contact"]["fields"]["group"] != null)
                        //{
                        //    LogExceptions.LogException(" group:" + data["contact"]["fields"]["group"]);
                        //    group = data["contact"]["fields"]["group"].ToString();
                        //}
                        //if (data["contact"]["fields"]["group"] != null)
                        //{
                        //    LogExceptions.LogException(" denomination:" + data["contact"]["fields"]["denomination"]);
                        //    denomination = data["contact"]["fields"]["denomination"].ToString();
                        //}
                    }
                   
                    var acc = cRep.GetAll().Where(o => o.LoginName.ToLower().Trim() == email.ToLower().Trim() && o.SID != UserType.Donor ).FirstOrDefault();
                    //if (cRep.GetAll().Where(o => o.LoginName.ToLower() == email.ToLower() && o.Status == UserStatus.Active && o.SID == UserType.SmartPros).Count() == 0)
                    if (acc == null)
                        {
                        em.SendingMail("indra@deffinity.com", "web method from start set user account", "web method from start set user account");
                        LogExceptions.LogException("web method from start set user account");
                        signupdetails cs = new signupdetails();
                        cs.CompanyName = organization;// company;
                        cs.EmailAddress = email;
                        cs.Firstname = firstname + " " + lastname;
                        cs.Industry = industry;
                        cs.MobileNumber = phone;
                        cs.url = "https://portal.plegitafrica.com";
                        cs.partner = "partner1";
                        cs.EcoSystem = "Organization";
                        cs.organization = organization;
                        cs.faith =  faith;
                        cs.group = group;
                        cs.denomination = denomination;
                        cs.sid = 1;
                        InsertContractor1(cs,true,false,false,true);


                        }
                       return data;

                    LogExceptions.LogException("web method from end active account");

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           
            return data;

        }

        [Route("api/Instance/staticdata")]        
        [AllowAnonymous]
        [HttpPost]
       // [EnableCors(origins: "*", headers: "*", methods: "*")]
        public object staticdata(signupdetails data)
        {
            LogExceptions.LogException("web method started static data");
            Random rd = new Random();
            
           
            var ret = postitem(data);
            if (ret == "success")
            {
                var acc = cRep.GetAll().Where(o => o.LoginName.ToLower() == data.EmailAddress.ToLower() && o.SID != UserType.Donor).FirstOrDefault(); // cRep.GetAll().Where(i => i.EmailAddress.ToLower() == data.EmailAddress.ToLower() && i.Status.ToLower() == "active").FirstOrDefault();
                //if (acc.AccessName == data.AccessName)
                if (acc==  null)
                {

                    InsertContractor1(data);
                    

                }
                return data;
            }
            else
            {
                return data;
            }
            //return data;

        }
        [Route("api/Instance/EmailExists")]
        [AllowAnonymous]
        [HttpPost]
      //  [EnableCors(origins: "*", headers: "*", methods: "*")]
        public object emailexists(signupdetails data)
        {
            string retval = "false";
            var cRep = new UserRepository<v_contractor>();
            var acc = cRep.GetAll().Where(o => o.LoginName.ToLower() == data.EmailAddress.ToLower() && o.SID != UserType.Donor).FirstOrDefault();  // cRep.GetAll().Where(i => i.EmailAddress.ToLower() == data.EmailAddress.ToLower() && i.Status.ToLower() == "active").FirstOrDefault();
                                                                                                                                                                                       //if (acc.AccessName == data.AccessName)
            if (acc != null)
             {
                 retval = "true";
             }
            return retval;

        }


        [Route("api/Instance/SectionsGet")]
        [AllowAnonymous]
        [HttpPost]
       // [EnableCors(origins: "*", headers: "*", methods: "*")]
        public List<listitem> SectionsGet()
        {
           var list = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Select();


            var rlist = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Select();
            var result = (from p in rlist
                          orderby p.Portfoliotype1
                          select new listitem { Value = p.ID.ToString(), Text = p.Portfoliotype1 }).ToList();
            return result;


        }





        public string postitem(signupdetails item)
        {
            try
            {
                LogExceptions.LogException("postitem event"); 
                string companyname = item.CompanyName;
                string result = companyname.Replace(" ", "");

                var p = new ProjectPortfolio();
                p.PortFolio = item.CompanyName;
               // p.Address = item.ad

            }
            catch(Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
            return "success";
        }


        public class plist
        {
            public string Email { set; get; }
            public string Password { set; get; }
        }


        public string InsertContractor(object[] item1)
        {


            if (HttpContext.Current.Session["InstId"] != null)
            {
                dynamic ss = item1[0];
                //for (int i = 0; i < ss.Length; i++)
                foreach (dynamic st in ss)
                {

                    dynamic dt = st;
                    string name = dt["name"];
                    string Mailid = dt["email"];

                    //check user mail all ready exists
                    //InsertContractor1(name, Mailid);

                }
               // sendmail(); //send mail after instance created
                return "success";
            }
            else { return "Fail"; }


        }

        public string sendmailtoDistributionlist(signupdetails data, Contractor c)
        {
            try
            {
                LogExceptions.LogException("send distribution list mail method");

                string turl = HttpContext.Current.Request.Url.AbsolutePath;

                string subject = "Plegit Africa Lead";
                string fromemail = "services@plegitafrica.com";

                string contents = string.Empty;
                string FILENAME = string.Empty;

                Email em = new Email();
                //sessionKeys.URL = indetails.AccessUrl;

                string displayName = webName;
                string siteurl = data.url;
                //siteurl = "http://www.wisal.cloud";
                //displayName = "Wisal";
                FILENAME = System.Web.HttpContext.Current.Server.MapPath("~/Content/emailtemplate/NewInstanceDistributionMail.html");

                using (StreamReader objstreamreader = File.OpenText(FILENAME))
                {
                    contents = objstreamreader.ReadToEnd();
                }
                //mail to owner
                if (c != null)
                {
                    string s = string.Empty;
                    if (c.EmailAddress != null)
                    {
                        var tempContent = contents;
                        tempContent = tempContent.Replace("[name]", c.ContractorName);
                        tempContent = tempContent.Replace("[company]", data.CompanyName);
                        tempContent = tempContent.Replace("[email]", c.EmailAddress);
                        tempContent = tempContent.Replace("[mobile]", data.MobileNumber);

                        var t = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Select().Where(o=>o.ID == Convert.ToInt32(!string.IsNullOrEmpty( data.Industry)? data.Industry:"0")).FirstOrDefault();
                        if(t != null)
                        tempContent = tempContent.Replace("[industry]", t.Portfoliotype1);
                        else
                            tempContent = tempContent.Replace("[industry]", string.Empty);


                        var newBody = tempContent;
                        var dlist = DeffinityManager.PortfolioMgt.BAL.PortfolioDistributionListBAL.PortfolioDistributionListBAL_SelectAll();
                        foreach (var p in dlist)
                        {
                            newBody = newBody.Replace("[username]", p.Name);

                            if (!c.EmailAddress.ToLower().Contains("indra"))
                            {
                                em.SendingMail("nadeem.mohammed@123servicepro.com", subject, newBody);
                                if (!c.EmailAddress.ToLower().Contains("porch1@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch2@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch3@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch4@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch5@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch6@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch7@deffinity.com") || !c.EmailAddress.ToLower().Contains("porch8@deffinity.com"))
                                {
                                    em.SendingMail(p.EmailAddress, subject, newBody);
                                }

                            }

                           

                            em.SendingMail("indra@deffinity.com", subject, newBody);

                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return "";

        }
        public string sendmail(signupdetails data, Contractor c,PartnerDetail partnerentity,PortfolioMgt.Entity.ProjectPortfolio portfolio,bool IsAorg)
        {
            try
            {
                //data.partner
                LogExceptions.LogException("send mail method"); 
               
                string turl = HttpContext.Current.Request.Url.AbsolutePath;

                string subject = string.Format( "GREAT NEWS! Your {0} Portal Is Ready.", webName);
                string fromemail = "services@plegitafrica.com";

                string contents = string.Empty;
                string FILENAME = string.Empty;

                Email em = new Email();
                //sessionKeys.URL = indetails.AccessUrl;

                string displayName = webName;
                string siteurl = data.url;
                //siteurl = "http://www.wisal.cloud";
                //displayName = "Wisal";
                FILENAME = System.Web.HttpContext.Current.Server.MapPath("~/Content/emailtemplate/newinstancewisal.htm");

                using (StreamReader objstreamreader = File.OpenText(FILENAME))
                {
                    contents = objstreamreader.ReadToEnd();
                }
                //mail to owner
                if (c != null)
                {
                    string s = string.Empty;
                    if (c.ContactNumber != null)
                    {
                        var tempContent = contents;


                       
                        if (partnerentity != null)
                        {
                            tempContent = tempContent.Replace("[logo]", string.Format("https://site.plegit.co.uk/assets/media/logos/logo-1.png?d=57", partnerentity.ID));
                            tempContent = tempContent.Replace("[portalname]", Deffinity.systemdefaults.GetInstanceTitle());
                            tempContent = tempContent.Replace("[supportmail]", Deffinity.systemdefaults.GetFromEmail());
                            //fromemail = partnerentity.FromEmail;
                            webName = Deffinity.systemdefaults.GetInstanceTitle();
                            fromemail = Deffinity.systemdefaults.GetFromEmail();// partnerentity.FromEmail;
                            subject = string.Format("GREAT NEWS! Your {0} Portal Is Ready.", webName);
                            
                           // else
                            //    tempContent = tempContent.Replace("[url]", partnerentity.ParnerPortal.Contains("https") ? partnerentity.ParnerPortal : "https://" + partnerentity.ParnerPortal);
                        }

                        tempContent = tempContent.Replace("[loginurl]", data.url);

                        //if (IsAorg)
                        //{
                        //    tempContent = tempContent.Replace("[loginurl]", data.url + "/OrgHome.aspx?orgguid=" + portfolio.OrgarnizationGUID);
                        //    //tempContent = tempContent.Replace("[url]", (partnerentity.ParnerPortal.Contains("https") ? partnerentity.ParnerPortal : "https://" + partnerentity.ParnerPortal) + "/OrgHome.aspx?orgguid=" + portfolio.OrgarnizationGUID);
                        //}
                        //else
                        //{
                        //    tempContent = tempContent.Replace("[loginurl]", data.url);
                        //}

                        tempContent = tempContent.Replace("[url]", data.url);

                        tempContent = tempContent.Replace("[user]", c.ContractorName);
                        tempContent = tempContent.Replace("[site]", data.url);
                        tempContent = tempContent.Replace("[urlroot]", data.url);
                        tempContent = tempContent.Replace("[displayname]", data.CompanyName);
                       
                        //[name]
                        //foreach (var c in udetails)
                        //{
                            tempContent = tempContent.Replace("[name]", c.ContractorName);
                            s = s + string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", c.ContractorName, c.LoginName, pwd.Where(o => o.Email.ToLower() == c.EmailAddress.ToLower()).FirstOrDefault().Password);
                        //}
                        tempContent = tempContent.Replace("[datarow]", s);
                        //if mail gives exception
                        try
                        {
                            em.SendingMail(fromemail, subject, tempContent, data.EmailAddress,"");
                            LogExceptions.LogException("send mail method - to :", data.EmailAddress+ "-from email: " + fromemail);
                        }
                       catch(Exception ex)
                        {
                            LogExceptions.LogException(data.EmailAddress +" failed to send mail "+ ex);                        
                        }

                        try
                        {
                            em.SendingMail(fromemail,  subject, tempContent, "indra@deffinity.com","");
                            if (!data.EmailAddress.ToLower().Contains("indra"))
                            {
                               // em.SendingMail(fromemail, subject, tempContent, "Instance@123smartpro.com","");
                                em.SendingMail(fromemail, subject, tempContent, "nadeem.mohammed@deffinity.com", "");
                               // em.SendingMail(fromemail, subject, tempContent,"123SignupEmailDistribution@deffinity.com","");
                            }
                        }
                        catch(Exception ex)
                        {
                            LogExceptions.LogException("mail to faild send" + ex);
                        }
                       
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return "success";
        }
        private void AddDefaultData(int portfolioID)
        {

            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                var tList = tk.GetAll().Where(o => o.PortfolioID == portfolioID).ToList();
                var tpLfist = tList.ToList();
                if (tpLfist.Count() == 0)
                {
                    tList = tk.GetAll().ToList();
                    var pE = tList.Where(o => o.Type == "Default Thank You Email").FirstOrDefault();
                    if (pE != null)
                    {
                        var dEntity = tk.GetAll().Where(o => o.PortfolioID == portfolioID).Where(o => o.Type == "Default Thank You Email").FirstOrDefault();
                        if (dEntity == null)
                        {
                            dEntity = new PortfolioMgt.Entity.TithingThankyouSetting();
                            dEntity.Type = pE.Type;
                            dEntity.AmountGrater = pE.AmountGrater;
                            dEntity.EmailContent = pE.EmailContent;
                            dEntity.EnableAutoMative = pE.EnableAutoMative;
                            dEntity.IsAmountGraterThan = pE.IsAmountGraterThan;
                            dEntity.IsRecurring = pE.IsRecurring;
                            dEntity.Notes = pE.Notes;
                            dEntity.PortfolioID = portfolioID;
                            dEntity.SetAsDefault = pE.SetAsDefault;

                            tk.Add(dEntity);

                        }

                        pE = tList.Where(o => o.Type == "Recurring Email").FirstOrDefault();
                        dEntity = tk.GetAll().Where(o => o.PortfolioID == portfolioID).Where(o => o.Type == "Recurring Email").FirstOrDefault();
                        if (dEntity == null)
                        {
                            dEntity = new PortfolioMgt.Entity.TithingThankyouSetting();
                            dEntity.Type = pE.Type;
                            dEntity.AmountGrater = pE.AmountGrater;
                            dEntity.EmailContent = pE.EmailContent;
                            dEntity.EnableAutoMative = pE.EnableAutoMative;
                            dEntity.IsAmountGraterThan = pE.IsAmountGraterThan;
                            dEntity.IsRecurring = pE.IsRecurring;
                            dEntity.Notes = pE.Notes;
                            dEntity.PortfolioID = portfolioID;
                            dEntity.SetAsDefault = pE.SetAsDefault;

                            tk.Add(dEntity);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        List<plist> pwd = new List<plist>();
        public string InsertContractor1(signupdetails data,bool IsAorg = false,bool IsGroup=false,bool IsMember=false,bool IsMemberWithService= false)
        {
            string retval = "fail";
            //if (HttpContext.Current.Session["InstId"] != null)
            //{

            try
            {
                int[] adminaids = { 1, 2 };
                var partnerentity = new PartnerDetail();
                var portfolio = new PortfolioMgt.Entity.ProjectPortfolio();
                var value = new Contractor();
                var udEntity = new UserDetail();
                value.ContractorName = data.Firstname;
                value.EmailAddress = data.EmailAddress;
                value.LoginName = data.EmailAddress; //value.ContractorName.Replace(" ", "");
                if (HttpContext.Current.Session["password"] == null)
                {
                    var pw = DeffinityManager.RandomPassword.GetPassword();
                    pwd.Add(new plist() { Email = value.EmailAddress, Password = pw });
                    value.Password = Deffinity.Users.Login.GeneratePasswordString(pw);// FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                }
                else
                {
                    var pw1 = HttpContext.Current.Session["password"].ToString();
                    pwd.Add(new plist() { Email = value.EmailAddress, Password = pw1 });
                    value.Password = Deffinity.Users.Login.GeneratePasswordString(pw1); //FormsAuthentication.HashPasswordForStoringInConfigFile(pw1, "SHA1");
                }
                //1 - Admin


                value.SID = data.sid;
                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;
                value.Status = UserStatus.Active;
                value.isFirstlogin = 0;
                value.ResetPassword = false;
                value.Company = data.CompanyName;
                value.ContactNumber = data.MobileNumber;
                value.Details = data.organization;
               
                //value. = Convert.ToInt32(HttpContext.Current.Session["InstId"]);
                //check email is already exists 
                //user should not be customer user
                /// if (cRep.GetAll().Where(o => o.EmailAddress.ToLower() == value.EmailAddress.ToLower() && o.Status.ToLower() == "active").Count() == 0)
                /// 
                int partnerid = 0;
                if (!string.IsNullOrEmpty(data.partner))
                {
                    //get partner id

                    //get partner detailstry{
                    try
                    {
                        partnerentity = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().Where(o => o.PartnerName.ToLower() == data.partner.ToLower()).FirstOrDefault();
                        if (partnerentity != null)
                            partnerid = partnerentity.ID;

                        if (data.EcoSystem.Trim().Length > 0)
                        {
                            partnerentity.EcoSystem = data.EcoSystem;
                            PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Update(partnerentity);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }

                if (cvRep.GetAll().Where(o => o.LoginName.ToLower().Trim() == value.LoginName.ToLower().Trim()  && o.SID != UserType.Donor).Count() == 0)
                {
                    cRep.Add(value);
                }

                if (value.ID > 0)
                {
                    

                    if (IsMember)
                    {
                        portfolio = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.PortFolio.ToLower() == "Faith church".ToLower()).FirstOrDefault();
                    }
                    else if (IsMemberWithService)
                    {

                        var cmpname =string.Empty;
                        cmpname = data.organization;
                        //if (data.sid == 3)
                            
                        //else
                        //    cmpname = data.CompanyName.ToLower().Trim().Length == 0 ? data.organization.Trim() : data.CompanyName.Trim();


                        portfolio = poRep.GetAll().Where(o => o.PortFolio.ToLower().Trim() == cmpname.ToLower().Trim()).FirstOrDefault();

                        if (portfolio == null)
                        {
                            portfolio = new ProjectPortfolio();
                            var cDate = DateTime.Now;
                            if (IsGroup)
                                portfolio.IsGroup = IsGroup;
                            //set as service company
                            if(data.sid == 3)
                                portfolio.IsServiceCompany = true;

                            portfolio.BenefitstoOrganisation = data.EcoSystem;
                            portfolio.OrgarnizationGUID = Guid.NewGuid().ToString();
                            portfolio.PortFolio = cmpname;
                            portfolio.EmailAddress = data.EmailAddress;
                            portfolio.Visible = true;
                            portfolio.TelephoneNumber = data.MobileNumber;
                            portfolio.Description = data.Industry;
                            portfolio.StartDate = DateTime.Now;
                            portfolio.EndDate = DateTime.Now;
                            portfolio.Owner = value.ID;
                            portfolio.AdminID = value.ID;
                            portfolio.ContactName = value.ContractorName;
                            portfolio.BankName = value.ContractorName;
                            portfolio.OrgarnizationStatus = "";
                            portfolio.CostCentre = data.faith;
                            portfolio.SalesNotes = data.group;
                            portfolio.RisksandIssues = data.denomination;
                            portfolio.EnableThankyouMail = true;
                            
                            portfolio.PortfolioTypeID = GetTypeID(data.Industry);
                            if (pwd.Where(o => o.Email.ToLower() == data.EmailAddress).FirstOrDefault() != null)
                                portfolio.KeyContactName = pwd.Where(o => o.Email.ToLower() == data.EmailAddress).FirstOrDefault().Password;
                           
                            try
                            {
                                if (!string.IsNullOrEmpty(data.partner))
                                {
                                    //get partner id
                                    portfolio.Justification = data.partner;
                                   
                                }
                                else
                                {
                                    partnerentity = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().FirstOrDefault();
                                }
                            }
                            catch (Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }
                            //if user is Organization admin or Group admin
                            poRep.Add(portfolio);

                            try
                            {
                                PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateOrgID(portfolio.OrgarnizationGUID);
                            }
                            catch(Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }

                            try
                            {
                                AddDefaultData(portfolio.ID);
                            }
                            catch(Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }

                            try
                            {
                                //update plat form fee
                                var payDetails = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(portfolio.ID);
                                if (payDetails == null)
                                {
                                    payDetails = new PortfolioMgt.Entity.PortfolioPaymentSetting();
                                    payDetails.PortfolioID = portfolio.ID;
                                    payDetails.PayType = "cardconnect";
                                    payDetails.IsActive = true;

                                }
                                payDetails.TransactionFee = 2;
                                payDetails.CardFee = 3;
                                
                                PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_AddUpdate(payDetails);
                            }
                            catch(Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }

                        }
                        //if (adminaids.Contains(value.SID.Value))
                        //{


                        //}
                        //else
                        //{
                        //    //if user is a member or member 

                        //}
                    }

                    else
                
                    {
                        var cDate = DateTime.Now;
                        if (IsGroup)
                            portfolio.IsGroup = IsGroup;

                        if (data.sid == 3)
                            portfolio.IsServiceCompany = true;
                        portfolio.BenefitstoOrganisation = data.EcoSystem;
                        portfolio.OrgarnizationGUID = Guid.NewGuid().ToString();
                        portfolio.PortFolio = data.CompanyName;
                        portfolio.EmailAddress = data.EmailAddress;
                        portfolio.Visible = true;
                        portfolio.TelephoneNumber = data.MobileNumber;
                        portfolio.Description = data.Industry;
                        portfolio.StartDate = DateTime.Now;
                        portfolio.EndDate = DateTime.Now;
                        portfolio.Owner = value.ID;
                        portfolio.AdminID = value.ID;
                        portfolio.ContactName = value.ContractorName;
                        portfolio.BankName = value.ContractorName;
                        portfolio.OrgarnizationStatus = "";
                        portfolio.CostCentre = data.faith;
                        portfolio.SalesNotes = data.group;
                        portfolio.RisksandIssues = data.denomination;
                        portfolio.PortfolioTypeID = GetTypeID(data.Industry);
                        if (pwd.Where(o => o.Email.ToLower() == data.EmailAddress).FirstOrDefault() != null)
                            portfolio.KeyContactName = pwd.Where(o => o.Email.ToLower() == data.EmailAddress).FirstOrDefault().Password;

                        try
                        {
                            if (!string.IsNullOrEmpty(data.partner))
                            {
                                //get partner id
                                portfolio.Justification = data.partner;
                                //get partner details
                                partnerentity = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().Where(o => o.PartnerName.ToLower() == data.partner.ToLower()).FirstOrDefault();
                                if (partnerentity != null)
                                {
                                    portfolio.PartnerID = partnerentity.ID;
                                    //if the partner is HVAC
                                    if (data.partner.ToLower().Contains("1hvac1"))
                                    {
                                        portfolio.Description = "HVAC Contractors";
                                        portfolio.PortfolioTypeID = GetTypeID("HVAC Contractors");
                                    }
                                }
                                else
                                {
                                    partnerentity = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().FirstOrDefault();
                                }
                            }
                            else
                            {
                                partnerentity = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().FirstOrDefault();
                            }
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                        //if user is Organization admin or Group admin

                        try
                        {
                            AddDefaultData(portfolio.ID);
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }


                        if (adminaids.Contains(value.SID.Value))
                        {
                            poRep.Add(portfolio);
                            try
                            {
                                PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateOrgID(portfolio.OrgarnizationGUID);
                            }
                            catch (Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }
                            try
                            {
                                //update plat form fee
                                var payDetails = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(portfolio.ID);
                                if (payDetails == null)
                                {
                                    payDetails = new PortfolioMgt.Entity.PortfolioPaymentSetting();
                                    payDetails.PortfolioID = portfolio.ID;
                                    payDetails.PayType = "cardconnect";


                                }
                                payDetails.TransactionFee = 2;
                                payDetails.CardFee = 3;
                                payDetails.IsActive = true;
                                PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_AddUpdate(payDetails);
                            }
                            catch (Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }

                        }
                        else
                        {
                            //if user is a member or member 
                           
                        }
                    }

                  
                    //if user a memeber
                 

                    if (portfolio.ID > 0)
                    {
                        try
                        {
                            try
                            {
                                //insert into group table
                                if (portfolio.IsGroup.HasValue ? portfolio.IsGroup.Value : false)
                                {
                                    //insert in to group table

                                    var gEntity = PortfolioMgt.BAL.DenominationGroupDetailsBAL.DenominationGroupDetailsBAL_Select().Where(o => o.Name.ToLower() == portfolio.PortFolio.ToLower()).FirstOrDefault();
                                    if (gEntity != null)
                                    {
                                        PortfolioMgt.BAL.DenominationGroupDetailsBAL.DenominationGroupDetailsBAL_Add(new DenominationGroupDetail() { Name = portfolio.PortFolio, DenominationDetailsID = 1 });
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                LogExceptions.LogException("Group:" + ex.Message);
                            }
                            //sene
                            //update trail period
                            PortfolioMgt.BAL.ProjectPortfolioBAL.AddUpdateTrailPerioid(portfolio.PartnerID.HasValue ? portfolio.PartnerID.Value : 0, portfolio.ID);
                            //insert default data
                            FLSFieldsConfigBAL.InsertConfigData(portfolio.ID, 1);
                            DefaultConfigurationToAllCustomer D_Configuration = new DefaultConfigurationToAllCustomer();
                            D_Configuration.DataBindToTables(portfolio.ID.ToString());
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }

                        var usertoCompany = new UserMgt.Entity.UserToCompany();
                        usertoCompany.CompanyID = portfolio.ID;
                        usertoCompany.UserID = value.ID;
                        cuRep.Add(usertoCompany);

                        //add default contact
                        var pc = new PortfolioContact();
                        pc.Name = "Sample client";
                        pc.Email = "Sample.client@plegit.co.uk";
                        pc.Mobile = "";
                        pc.PortfolioID = portfolio.ID;
                        pc.DateLogged = DateTime.Now;
                        pcRep.Add(pc);

                        var pca = new PortfolioContactAddress();
                        pca.BillingName = "Sample client";
                        pca.ContactID = pc.ID;
                        pca.Address = "125 Vinton Avenue ";
                        pca.Address2 = "";
                        pca.City = "Cranston";
                        pca.State = "Rhode Island";
                        pca.PostCode = "02920";
                        pca.LoggedDatetime = DateTime.Now;

                        //pca.
                        pcaRep.Add(pca);

                        //add new defautl jobs by sector
                        //Get default jobs
                        if (portfolio.PortfolioTypeID.HasValue)
                        {
                            try
                            {
                                int startby = 9;
                                var cudate = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddHours(9);

                                int incBy = 0;
                                var dJobs = DefaultJobsBAL.DefaultJobsBAL_select(portfolio.PortfolioTypeID.Value);
                                foreach (var j in dJobs)
                                {
                                    var c = new CallDetail();
                                    c.CompanyID = portfolio.ID;
                                    c.LoggedBy = value.ID;
                                    c.LoggedDate = cudate;
                                    c.RequesterID = pc.ID;
                                    //6 default
                                    c.RequestTypeID = 6;
                                    c.SiteID = 0;
                                    c.StatusID = JobStatus.New;
                                    CallDetailsBAL.AddCallDetails(c);
                                    var CallID = c.ID;
                                    //Journal entiry
                                    CallDetailsJournalBAL.AddCallDetailsJournal(c);


                                    var f = new FLSDetail();
                                    f.CallID = c.ID;
                                    f.CategoryID = 0;
                                    f.ContactAddressID = pca.ID;
                                    f.DateTimeStarted = cudate.AddHours(incBy);
                                    f.DepartmentID = 0;
                                    f.Details = j.JobDescription;
                                    f.PriorityId = 0;
                                    f.ScheduledDate = cudate.AddHours(incBy);
                                    //increment by hours
                                    incBy = incBy + 2;
                                    f.ScheduledEndDateTime = cudate.AddHours(incBy);
                                    f.DateTimeClosed = cudate.AddHours(incBy);
                                    f.SourceOfRequestID = 0;
                                    f.SubCategoryID = 0;
                                    f.SubjectID = 0;
                                    //f.UserID = value.ID;
                                    FLSDetailsBAL.AddFLSDetails(f);


                                    //add to journal
                                    FLSDetailsJournalBAL.AddFLSDetailsJournal(f);


                                    var cReporsitory = new DCRepository<CallDetail>();
                                    var cd = cReporsitory.GetAll().Where(o => o.ID == CallID).FirstOrDefault();
                                    var fReporsitory = new DCRepository<FLSDetail>();
                                    var fd = fReporsitory.GetAll().Where(o => o.CallID == CallID).FirstOrDefault();

                                    //assign ticket to user

                                    if (value.ID > 0)
                                    {
                                        FLSDetailsBAL.UpdateTicketStatus(CallID, value.ID, 43);
                                        //43	Assigned Technician
                                        //cd.StatusID = 43;
                                        //cReporsitory.Edit(cd);
                                        fd.UserID = value.ID;

                                        fReporsitory.Edit(fd);
                                        using (UserMgt.DAL.UserDataContext Pdc = new UserMgt.DAL.UserDataContext())
                                        {
                                            var userlist = Pdc.Contractors.Where(p => p.ID == value.ID).ToList();

                                            foreach (var cdeatils in userlist)
                                            {
                                                //update to associated callids
                                                UpdateAssignResources(fd, cd, null, false, false, 0, cdeatils, true);
                                            }
                                            //MailSendingToAssignResource(fd, cd);
                                            //update 
                                            //sendmailtoCustomer(QueryStringValues.CallID.ToString(), fd.UserID.Value.ToString());
                                        }
                                    }


                                }


                            }
                            catch (Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }

                        }

                        //var 


                        sendmail(data, value, partnerentity, portfolio, IsAorg);
                        //send mail to orgnizations
                        try
                        {
                            //if (adminaids.Contains(value.SID.Value))
                            //{
                            //    .SendMailToOrganization(portfolio.OrgarnizationGUID);
                            //}
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.LogException("Mail to Organization:" + ex.Message);
                        }

                        sendmailtoDistributionlist(data, value);
                        retval = "success";
                    }


                }
            }
            catch (Exception ex)
            { return "fail"; LogExceptions.WriteExceptionLog(ex); }
            return retval;
            //}
            //else { return "Fail"; }
        }
        private bool UpdateAssignResources(FLSDetail fd, CallDetail cd, ServiceProviderScheduling blastValues, bool tocheck, bool smail, int cnt, UserMgt.Entity.Contractor c, bool IsAssigned)
        {
            using (DCDataContext Dc = new DCDataContext())
            {
                var cdResource = Dc.CallResourceSchedules.Where(o => o.CallID == cd.ID && o.ResourceID == c.ID).FirstOrDefault();
                if (cdResource == null)
                {
                    cdResource = new CallResourceSchedule();
                    cdResource.IsActive = false;
                    cdResource.ResourceID = c.ID;
                    cdResource.CallID = cd.ID;
                    cdResource.StartDate = fd.ScheduledDate;
                    cdResource.EndDate = fd.ScheduledEndDateTime;
                    cdResource.IsAssigned = IsAssigned;
                    cdResource.UserType = "Smart Tech";
                    if (tocheck)
                    {
                        if (cnt > blastValues.InitialBatchQty)
                        {
                            smail = false;
                            cdResource.MailSent = false;
                            cdResource.MailSentDateTime = DateTime.Now.AddMinutes(blastValues.MinBeforeNextBlast.HasValue ? blastValues.MinBeforeNextBlast.Value : 0);
                        }
                        else
                        {
                            cdResource.MailSent = true;
                            cdResource.MailSentDateTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        cdResource.MailSent = true;
                        cdResource.MailSentDateTime = DateTime.Now;
                    }

                    Dc.CallResourceSchedules.InsertOnSubmit(cdResource);
                    Dc.SubmitChanges();
                }
                else
                {
                    cdResource.StartDate = fd.ScheduledDate;
                    cdResource.EndDate = fd.ScheduledEndDateTime;
                    cdResource.IsAssigned = IsAssigned;
                    cdResource.UserType = CallResourceScheduleBAL.Usertype_SmartTech;
                    if (tocheck)
                    {
                        if (cnt > blastValues.InitialBatchQty)
                        {
                            smail = false;
                            cdResource.MailSent = false;
                            cdResource.MailSentDateTime = DateTime.Now.AddMinutes(blastValues.MinBeforeNextBlast.HasValue ? blastValues.MinBeforeNextBlast.Value : 0);
                        }
                        else
                        {
                            cdResource.MailSent = true;
                            cdResource.MailSentDateTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        cdResource.MailSent = true;
                        cdResource.MailSentDateTime = DateTime.Now;
                    }


                    Dc.SubmitChanges();
                }
            }

            return smail;
        }
public int GetTypeID(string industry)
        {
            int retval = 0;
            try
            {
                var sEntity = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Select().Where(o => o.Portfoliotype1.ToLower() == industry.Trim().ToLower()).FirstOrDefault();
                if (sEntity != null)
                {
                    retval = sEntity.ID;
                    LogExceptions.LogException("End industry method: industry id" + sEntity.ID);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;

        }

       

    }

   
}
