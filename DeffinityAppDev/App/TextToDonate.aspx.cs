using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using IO.ClickSend.ClickSend.Api;
//using IO.ClickSend.Client;
//using IO.ClickSend.ClickSend.Model;
//using RestSharp;
using System.Diagnostics;
using RestSharp;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.Extensions.Primitives;
using PortfolioMgt.Entity;
using com.itextpdf.text.pdf;
using Newtonsoft.Json.Linq;
using Infragistics.WebUI.Util.Serialization;
using ClosedXML.Excel;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Net.Http.Headers;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Vml;

namespace DeffinityAppDev.App
{
    public partial class TextToDonate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {

                    txtcode.Text = Deffinity.systemdefaults.GetCountryCode();

                    ddlType.DataSource = DeffinityManager.TagsBAL.GetSMSTags().OrderBy(o => o.Value).ToList();
                    ddlType.DataTextField = "Value";
                    ddlType.DataValueField = "ID";
                    ddlType.DataBind();
                    ddlType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select...", ""));

                    BindFund();
                    BindListview();
                    BindPurchaseHistory();
                    UpdateData();
              //  IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                    // var tList = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();


                    //BindDropDown();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void UpdateData()
        {
            try
            {

                var noofmessages = 0;
                var totalamount = 0.00;
                var remainingamount = 0.00;

                IPortfolioRepository<PortfolioMgt.Entity.SMSDefaultSetting> pkRep = new PortfolioRepository<PortfolioMgt.Entity.SMSDefaultSetting>();

                var pkList = pkRep.GetAll().FirstOrDefault();



                IPortfolioRepository<PortfolioMgt.Entity.SMSPackageDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageDetail>();

                var sList = pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.IsPaid == true).ToList();


                IPortfolioRepository<PortfolioMgt.Entity.SMSTracker> sRep = new PortfolioRepository<PortfolioMgt.Entity.SMSTracker>();

                var scount = sRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Count();

                lbltotal_package_amount.InnerText = string.Format("{1}{0:N2}", sList.Sum(o => o.Amount.HasValue ? o.Amount.Value : 0.00), Deffinity.Utility.GetCurrencySymbol());
                lblsms_sent_count.InnerText = scount.ToString();
                var r = sList.Sum(o => o.Amount.HasValue ? o.Amount.Value : 0.00) - (scount * pkList.SellingPrice ?? 0.00);
                hval.Value = r.ToString();
                lblRemaining_amount.InnerText = string.Format("{1}{0:N2}", r, Deffinity.Utility.GetCurrencySymbol());

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public static string TruncateString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            if (input.Length <= 100)
                return input;

            return input.Substring(0, 100) + "...";
        }
        private void BindFund()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                //var tithingDetail = pRep.GetAll().Where(o => o.OrganizationID == 0).FirstOrDefault();

                var tithinglist = pRep.GetAll().Where(o => o.Title != "").Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();

                ddlFund.DataSource = from t in tithinglist
                                     select new { Name = TruncateString(t.Title), Value = Deffinity.systemdefaults.GetWebUrl()+ "/fundraiser/"+ t.QRcode };
                ddlFund.DataTextField = "Name";
                ddlFund.DataValueField = "Value";
                ddlFund.DataBind();

                ddlFund.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select...", ""));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        void BindPurchaseHistory()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> pcRep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();

                var pcList = pcRep.GetAll().ToList();
                IPortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting> pkRep = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting>();

                var pkList = pkRep.GetAll().ToList();

                IPortfolioRepository<PortfolioMgt.Entity.SMSPackageDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageDetail>();

                var sList = pRep.GetAll().Where(o=>o.PortfolioID == sessionKeys.PortfolioID).Where(o=>o.IsPaid ==  true).ToList();

                IUserRepository<UserMgt.Entity.v_contractor> uRep = new UserRepository<UserMgt.Entity.v_contractor>();

                var uList = uRep.GetAll().Where(o => o.SID != 2).ToList();


                var rList = (from s in sList
                             join p in pkList on s.PackageID equals p.ID
                             //where s.IsPaid == true
                             select new
                             {
                                 s.ID,
                                 s.PackageID,
                                 DateTime = s.PaidOn.Value.ToShortDateString() + " " + s.PaidOn.Value.ToShortTimeString(),
                                 Organization = pcList.Where(o => o.ID == s.PortfolioID).FirstOrDefault().PortFolio,
                                 Pacakge = p.PackageName,
                                 Volume = p.SMSCount,
                                 SellPrice = string.Format("{0:F2}", p.SellingPrice),
                                 PurchasedBy = uList.Where(o => o.ID == s.PaidBy).FirstOrDefault().ContractorName

                             }).ToList();

                GridPackageHistory.DataSource = rList.ToList();
                GridPackageHistory.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindListview()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting> rpNew = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting>();
                var pList = rpNew.GetAll().ToList();

                List<packages> tdList = new List<packages>();
                foreach (var p in pList)
                {
                    tdList.Add(new packages() { packageid = p.ID, packagename=p.PackageName, packagesubtext = p.SubText,packagevalue = (p.SellingPrice??0).ToString() });
                    //var amt = p.;
                    //if (amt > 0)
                    //{
                    //    var cnt = rlist.Where(o => o.TicketType == p.TypeOfTicket).Where(o => o.BookingStatus != null).Count();
                    //    tdList.Add(new TicketsDisplay() { Title = p.TypeOfTicket + "<br> No of Bookings", Value = cnt.ToString() });
                    //    tdList.Add(new TicketsDisplay() { Title = p.TypeOfTicket + "<br> Ticket Sales", Value = string.Format("{0:F2}", cnt * amt) });
                    //}
                    //else
                    //{
                    //    tdList.Add(new TicketsDisplay() { Title = p.TypeOfTicket + "<br> No of Bookings", Value = rlist.Where(o => o.TicketType == p.TypeOfTicket).Count().ToString() });
                    //}
                    // tdList.Add(new TicketsDisplay() { Title =  p.TypeOfTicket + "<br> Booking Amount", Value = "0" });
                }
                listdata.DataSource = tdList;
                listdata.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnSendSms_Click(object sender, EventArgs e)
        {
            try
            {
                var pDetails = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(p => p.ID == sessionKeys.PortfolioID).FirstOrDefault();
              
                var client = new RestClient("https://rest.clicksend.com/v3/sms/send");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Basic bmFkZWVtLm1vaGFtbWVkQHNtYXJ0Z2l2aW5nLmFwcDpBeWFhbjIwMDMyIyE=");
                var body = "";
                JArray array = new JArray();
                List<smscalss> slist = new List<smscalss>();
                if (rd.SelectedValue == "By Phone number")
                {
                    var phone = txtTo.Text.Trim().Split(',');
                   
                    foreach (string s in phone)
                    {
                        if(s.Length >0)
                        {
                            slist.Add(new smscalss() { tosms = s, smscontent= txtsms.Text.Trim() ,smstag="New"});
                        }
                    }

                    foreach(var s in slist)
                    {
                        var content = Deffinity.Utility.RemoveHTMLTags(s.smscontent);
                        content = content.Replace("&nbsp;", " ");
                        content = content.Replace("{{instancename}}", sessionKeys.PortfolioName);
                        content = content.Replace("{{username}}", s.username);
                        content = content.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
                        content = content.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                        content = content.Replace("{{todaydate}}", DateTime.Now.ToShortDateString());
                        content = content.Replace("{{currentday}}", DateTime.Now.Day.ToString());
                        content = content.Replace("{{donationurl}}", Deffinity.systemdefaults.GetWebUrl() + "/web/" + pDetails.OrgUniqID);
                        //content = content.Replace(Environment.NewLine, "");
                        //content = content.Replace("\r", "");
                        //content = content.Replace("\n", "");
                        //content = content.Replace("\\r", "");
                        //content = content.Replace("\\n", "");
                        //content = content.Replace("  ", "");
                        if (s.tosms.Length > 0)
                        {
                            var tonum = s.tosms.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace(".", "").Replace("_", "").Trim();

                            if (!tonum.Contains("+"))
                                tonum = txtcode.Text.Trim() + tonum;


                            JObject obj = new JObject { { "source", "php" }, { "body", content }, { "to", "{" + tonum + "}" }, { "custom_string", s.smstag } };
                            array.Add(obj);
                        }
                    }
                    


                }

                else
                {

                    var phone = txtSkills.Value.Trim().Replace("&nbsp;", " ").Replace("[","").Replace("]", "").Replace("{", "").Replace("}", "").Replace("value", "").Replace(":", "").Replace("\"", "").Split(',');
                    foreach (string s in phone)
                    {
                        if (s.Length > 0)
                        {
                            var ulistByTags = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(p => p.CompanyID == sessionKeys.PortfolioID).Where(p => p.Tags.ToLower().Trim().Contains(s.ToLower().Trim()) ).ToList();

                            foreach (var u in ulistByTags)
                            {
                                if (u.ContactNumber.Length > 0)
                                {
                                    slist.Add(new smscalss() { username = u.ContractorName, tosms = u.ContactNumber, smscontent = txtsms.Text.Trim(), smstag = s });
                                }
                            }
                        }
                    }

                    if (slist.Count > 0)
                    {
                        foreach (var s in slist)
                        {

                           var content = Deffinity.Utility.RemoveHTMLTags(s.smscontent);
                            content = content.Replace("&nbsp;", " ");
                             content = content.Replace("{{instancename}}", sessionKeys.PortfolioName);
                            content = content.Replace("{{username}}", s.username);
                            content = content.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
                            content = content.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                            content = content.Replace("{{todaydate}}", DateTime.Now.ToShortDateString());
                            content = content.Replace("{{currentday}}", DateTime.Now.Day.ToString());
                            content = content.Replace("{{donationurl}}", Deffinity.systemdefaults.GetWebUrl() +"/web/"+pDetails.OrgUniqID);
                            //content = content.Replace(Environment.NewLine, "");
                            //content = content.Replace("\r", "");
                            //content = content.Replace("\n", "");
                            //content = content.Replace("\\r", "");
                            //content = content.Replace("\\n", "");
                            //content = content.Replace("  ", "");
                            var tonum = s.tosms.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace(".", "").Replace("_", "").Trim();

                            if (!tonum.Contains("+"))
                                tonum = txtcode.Text.Trim() + tonum;

                            JObject obj = new JObject { { "source", "php" }, { "body", content}, { "to", "{" + tonum + "}" }, { "custom_string", s.smstag } };
                            array.Add(obj);
                        }

                    }


                }
                //body = "";

                // body = @"{" + "\n" +
                //@"  ""messages"" : [" + "\n" +

                //@"      {" + "\n" +
                //@"        ""source"" : ""php""," + "\n" +
                //@"        ""body"" : ""Hello there""," + "\n" +
                //@"        ""to"" : ""{+919986334941}""," + "\n" +
                //@"        ""custom_string"" : ""this is a test""" + "\n" +
                //@"      }" + "\n" +

                //@"  ]" + "\n" +
                //@"}";
                //JArray a = new JArray();
                //a.Add(new JObject("source", "php"));
                //a.Add( new JObject("body", "First jobject msg"));
                //a.Add(new JObject("to","{+919986334941}"));
                //a.Add(new JObject("custom_string", "First msg"));

                //JObject obj = new JObject {  { "source", "php" },{ "body", "First jobject msg" }, { "to", "{+919986334941}" }, { "custom_string", "First msg" } };
                //JArray array = new JArray();

                //array.Add(obj);

                JObject o = new JObject();
                o["messages"] = array;

                body = o.ToString();

                if (body.Length > 0)
                {

                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                    SmsTracker(slist, body, response.Content);
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "SMS sent successfully", "OK");
                }
                //Console.WriteLine(response.Content);
                //Configuration.Default.Username = "nadeem.mohammed@smartgiving.app";
                //Configuration.Default.Password = "Ayaan20032#!";

                //var apiInstance = new AccountApi();

                //try
                //{
                //    // Get account information
                //    string result = apiInstance.AccountGet();
                //    Debug.WriteLine(result);
                //}
                //catch (Exception ex)
                //{
                //    Debug.Print("Exception when calling AccountApi.AccountGet: " + ex.Message);
                //}

                //                var configuration = new Configuration()
                //                {
                //                    Username = "nadeem.mohammed@smartgiving.app",
                //                    Password = "Ayaan20032#!"
                //                };
                //                var smsApi = new SMSApi(configuration);

                //                var listOfSms = new List<SmsMessage>
                //{
                //    new SmsMessage(
                //        to: "+919986334941",
                //        body: "First test sms",
                //        source: "sdk"
                //    )
                //};

                //                var smsCollection = new SmsMessageCollection(listOfSms);
                //                var response = smsApi.SmsSendPost(smsCollection);


            }
            catch(Exception ex)
            {
                DeffinityManager.ShowMessages.ShowSuccessError(this.Page, "SMS faild to send", "OK");
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void SendSms(string apikey,string secretkey)
        {
            var apiKey = apikey;
            var apiSecret = secretkey;

            var apiCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"));
            var pDetails = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(p => p.ID == sessionKeys.PortfolioID).FirstOrDefault();
            JArray array = new JArray();
            List<smscalss> slist = new List<smscalss>();
            if (rd.SelectedValue == "By Phone number")
            {
                var phone = txtTo.Text.Trim().Split(',');

                foreach (string s in phone)
                {
                    if (s.Length > 0)
                    {
                        slist.Add(new smscalss() { tosms = s, smscontent = txtsms.Text.Trim(), smstag = "New" });
                    }
                }

                foreach (var s in slist)
                {
                    var content = Deffinity.Utility.RemoveHTMLTags(s.smscontent);
                    content = content.Replace("&nbsp;", " ");
                    content = content.Replace("{{instancename}}", sessionKeys.PortfolioName);
                    content = content.Replace("{{username}}", s.username);
                    content = content.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
                    content = content.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                    content = content.Replace("{{todaydate}}", DateTime.Now.ToShortDateString());
                    content = content.Replace("{{currentday}}", DateTime.Now.Day.ToString());
                    content = content.Replace("{{donationurl}}", Deffinity.systemdefaults.GetWebUrl() + "/web/" + pDetails.OrgUniqID);
                    //content = content.Replace(Environment.NewLine, "");
                    //content = content.Replace("\r", "");
                    //content = content.Replace("\n", "");
                    //content = content.Replace("\\r", "");
                    //content = content.Replace("\\n", "");
                    content = content.Replace("  ", "");
                    if (s.tosms.Length > 0)
                    {
                        var tonum = s.tosms.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace(".", "").Replace("_", "").Trim();

                        if (!tonum.Contains("+"))
                            tonum = txtcode.Text.Trim() + tonum;


                        JObject obj = new JObject { { "source", "php" }, { "body", content }, { "to", "{" + tonum + "}" }, { "custom_string", s.smstag } };
                        array.Add(obj);

                        var jsonSendRequest =
              "{ \"messages\" : [ { \"content\" : \"-content-\", \"destination\" : \">>-tono-<<\" } ] }";

                        jsonSendRequest = jsonSendRequest.Replace("-content-", content);
                        jsonSendRequest = jsonSendRequest.Replace("-tono-", tonum);
                        var client = new RestClient("https://rest.smsportal.com");
                        var request = new RestRequest("BulkMessages", Method.POST);

                        request.AddHeader("Authorization", "Basic " + apiCredentials);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/json", jsonSendRequest, ParameterType.RequestBody);

                        try
                        {
                            var response = client.Execute(request);

                            if (response.IsSuccessful)
{
                                SmsTracker(slist, jsonSendRequest, response.Content);
                                DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "SMS sent successfully", "OK");
                                UpdateData();
                                Console.WriteLine("Success:");
                               // Console.WriteLine(response.Content);
                            }
                            else
                            {
                                //LogExceptions.WriteExceptionLog(e);
                                Console.WriteLine("Failure:");
                                Console.WriteLine(response.Content);
                            }
                        }
                        catch (Exception e)
                        {
                            LogExceptions.WriteExceptionLog(e);
                            Console.WriteLine("Something went wrong during the network request.");
                            Console.WriteLine(e);
                        }
                    }
                }



            }

            else
            {

                var phone = txtSkills.Value.Trim().Replace("&nbsp;", " ").Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("value", "").Replace(":", "").Replace("\"", "").Split(',');
                foreach (string s in phone)
                {
                    if (s.Length > 0)
                    {
                        var ulistByTags = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(p => p.CompanyID == sessionKeys.PortfolioID).Where(p => p.Tags.ToLower().Trim().Contains(s.ToLower().Trim())).ToList();

                        foreach (var u in ulistByTags)
                        {
                            if (u.ContactNumber.Length > 0)
                            {
                                slist.Add(new smscalss() { username = u.ContractorName, tosms = u.ContactNumber, smscontent = txtsms.Text.Trim(), smstag = s });
                            }
                        }
                    }
                }

                if (slist.Count > 0)
                {
                    foreach (var s in slist)
                    {

                        var content = Deffinity.Utility.RemoveHTMLTags(s.smscontent);
                        content = content.Replace("&nbsp;", " ");
                        content = content.Replace("{{instancename}}", sessionKeys.PortfolioName);
                        content = content.Replace("{{username}}", s.username);
                        content = content.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
                        content = content.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                        content = content.Replace("{{todaydate}}", DateTime.Now.ToShortDateString());
                        content = content.Replace("{{currentday}}", DateTime.Now.Day.ToString());
                        content = content.Replace("{{donationurl}}", Deffinity.systemdefaults.GetWebUrl() + "/web/" + pDetails.OrgUniqID);
                        //content = content.Replace(Environment.NewLine, "");
                        //content = content.Replace("\r", "");
                        //content = content.Replace("\n", "");
                        //content = content.Replace("\\r", "");
                        //content = content.Replace("\\n", "");
                        content = content.Replace("  ", "");
                        var tonum = s.tosms.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace(".", "").Replace("_", "").Trim();

                        if (!tonum.Contains("+"))
                            tonum = txtcode.Text.Trim() + tonum;

                        //JObject obj = new JObject { { "source", "php" }, { "body", content }, { "to", "{" + tonum + "}" }, { "custom_string", s.smstag } };
                        //array.Add(obj);

                        var jsonSendRequest =
                "{ \"messages\" : [ { \"content\" : \"-content-\", \"destination\" : \">>-tono-<<\" } ] }";

                        jsonSendRequest= jsonSendRequest.Replace("-content-", content);
                        jsonSendRequest= jsonSendRequest.Replace("-tono-", tonum);
                        var client = new RestClient("https://rest.smsportal.com");
                        var request = new RestRequest("BulkMessages", Method.POST);

                        request.AddHeader("Authorization", "Basic " + apiCredentials);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/json", jsonSendRequest, ParameterType.RequestBody);

                        try
                        {
                            var response = client.Execute(request);

                            if (response.IsSuccessful)
                            {
                                SmsTracker(slist, jsonSendRequest, response.Content);
                                DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "SMS sent successfully", "OK");
                                Console.WriteLine("Success:");
                                Console.WriteLine(response.Content);
                            }
                            else
                            {
                                Console.WriteLine("Failure:");
                                Console.WriteLine(response.Content);
                            }
                        }
                        catch (Exception e)
                        {
                            LogExceptions.WriteExceptionLog(e);
                            Console.WriteLine("Something went wrong during the network request.");
                            Console.WriteLine(e);
                        }
                    }

                }


            }





            
        }

        private class packages
        {
            public int packageid { set; get; }
            public string packagename { set; get; }
            public string packagesubtext { set; get; }
            public string packagevalue { set; get; }
        }
        private class messages
        {
            public string source { set; get; }
            public string body { set; get; }
            public string to { set; get; }
            public string custom_string { set; get; }
        }
        private class smscalss
        {
            public string username { set; get; }
            public string tosms { set; get; }
            public string smscontent { set; get; }

            public string smstag { set; get; }
        }

        public string JsonWithStringTags()
        {
            List<string> a = new List<string>();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //            JArray a = new JArray();
            ////JObject obj = new JObject { { "php" }};
            //a.Add("php");


            var pList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o=>o.CompanyID ==sessionKeys.PortfolioID).Where(o => o.Tags != "").ToList();

            foreach(var p in pList)
            {
                if(p.Tags.Length >0)
                {
                    try
                    {
                        var jbList = p.Tags.Split(',');

                        foreach (var j in jbList)
                        {
                            if(j.Length >0)
                            a.Add(j.Trim());
                            //var jb = JObject.Parse(j.ToString());
                            //foreach (var b in jb)
                            //{
                            //    var bv = b.Value;

                           // a.Add(bv.ToString().Replace("{", "").Replace("}", ""));
                           // }

                        }


                        //var jbList = JArray.Parse(p.Tags);

                        //foreach (var j in jbList)
                        //{
                        //    var jb = JObject.Parse(j.ToString());
                        //    foreach (var b in jb)
                        //    {
                        //        var bv = b.Value;

                        //        a.Add(bv.ToString().Replace("{", "").Replace("}", ""));
                        //    }

                        //}
                    }
                    catch(Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }

                    //var dval = serializer.Deserialize<List<Dictionary<string, object>>>(p.Tags);
                    //int i = 0;
                    //foreach(var dv in dval.ToList())
                    //{
                    //    // var t = dv[0];
                    //    //KeyValuePair<string, object> s = dv.Values.FirstOrDefault(); 
                    //   // var s1= dv.Value.ToString();

                    //    //var t1 = t[]
                    //}
                }
            }


            
          


           
            return serializer.Serialize(a.Distinct());
        }
            private string JsonWithStringBuilder(List<smscalss> slist)
        {
            List<messages> mlist = new List<messages>();


            JArray a = new JArray();
            a.Add(new JProperty("source", "php"));
            a.Add(new JProperty("body", "test content"));
            a.Add(new JProperty("to", "+919986334941"));
            a.Add(new JProperty("custom_string", "First msg"));

            JObject o = new JObject();
            o["messages"] = a;

            ////Create string builder object
            StringBuilder jsonStringBuilder = new StringBuilder();
            //jsonStringBuilder.Append("{" );
            //jsonStringBuilder.Append("\"messages\" : [" );

            //foreach (var s in slist)
            //{
            //    jsonStringBuilder.Append(" {" );
            //    jsonStringBuilder.Append("\"source\" : \"php\",");
            //    jsonStringBuilder.Append("\"body\" : \"" + s.smscontent +"\",");
            //    jsonStringBuilder.Append("\"to\" : \"{"+ s.tosms +"}\"," );
            //    jsonStringBuilder.Append("\"custom_string\" : \""+s.smstag+"\"");
            //    jsonStringBuilder.Append("}" );
            //}


            //jsonStringBuilder.Append("]" );
            //jsonStringBuilder.Append("}");



            //json base Open braces
           

            //Create javaScript Serialier class object
            //using System.Web.Script.Serialization
            JavaScriptSerializer jsObjectSerializer = new JavaScriptSerializer();
            jsObjectSerializer.MaxJsonLength = Int32.MaxValue;
            //Finally serialize json into a string
            return jsObjectSerializer.Serialize(jsonStringBuilder.ToString());
        }


        private void SmsTracker(List<smscalss> slist, string smsbody,string smsresult)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.SMSTracker> sRep = new PortfolioRepository<PortfolioMgt.Entity.SMSTracker>();
                sRep.Add(new PortfolioMgt.Entity.SMSTracker()
                {
                    PortfolioID = sessionKeys.PortfolioID,
                    SentOn = DateTime.Now,
                    SendBy = sessionKeys.UID,
                    SMSCount = slist.Count(),
                    SMSContent = smsbody,
                    SMSTo = "",
                    SMSResult = smsresult



                })
                ;

               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void rd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void rd_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if(rd.SelectedValue.ToLower().Trim().Contains("tags"))
            {
                pnlTag.Visible = true;
                pnlTextbox.Visible = false;
            }
            else
            {
                pnlTag.Visible = false;
                pnlTextbox.Visible = true;
            }
        }

        protected void listdata_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "buy")
                {
                    // Handle the edit action
                    int itemID = Convert.ToInt32(e.CommandArgument);
                    // Perform the edit action based on the itemID
                    IPortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting> pkRep = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting>();

                    var pkList = pkRep.GetAll().Where(o=>o.ID == itemID).FirstOrDefault();

                    var retval = DC.BLL.QuickPayBAL.SMSPay(sessionKeys.PortfolioID,itemID.ToString(), (pkList.SellingPrice??0.00));
}
}
catch(Exception ex)
{
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                IPortfolioRepository<SMSDefaultSetting> sRep = new PortfolioRepository<SMSDefaultSetting>();
                var p = sRep.GetAll().FirstOrDefault();
                if (p != null)
                {
                    var minVal = p.SellingPrice ?? 0.00;

                    if (Convert.ToDouble(hval.Value) > minVal)
                    {

                        if (p != null)
                            SendSms(p.ClientID, p.APISecret);
                    }
                    else
                    {
                        DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "Please add amount to send message", "Ok");
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}