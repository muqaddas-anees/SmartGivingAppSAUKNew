using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;
using System.IO;
using System.Web;
using System.Net.Mail;
using System.Web.Security;
using PortfolioMgt.BAL;
using UserMgt.DAL;

namespace PortfolioMgt.BLL
{
    public class GeneratePolicy
    {
        #region policy sent track

        public class SentMailDisplay
        {
            public int AddressID { set; get; }
            public string DisplayData { set; get; }
        }
        
        public static void PortfolioContactAddressMailTrack_Add(int addressID)
        {
            try
            {
                var ptRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddressMailTrack>();
                ptRep.Add(new PortfolioContactAddressMailTrack()
                {
                    AddressID = addressID,
                    MailSentDateTime = DateTime.Now,
                    SentBy = sessionKeys.UID
                });
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        public static List<SentMailDisplay> PortfolioContactAddressMailTrack_Select(int addressID)
        {
            var ptRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddressMailTrack>();

            List<SentMailDisplay> dlist = new List<SentMailDisplay>();

            var rlist = ptRep.GetAll().Where(o => o.AddressID == addressID).ToList();

            //if (rlist.Count == 0)
            //{
            //   //if amount payment is done 
            //    //insert first record
            //    var paDetails = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();

            //    var aDetials = paDetails.GetAll().Where(o => o.AddressID == addressID && o.IsPaid == true).FirstOrDefault();

            //    ptRep.Add(new PortfolioMgt.Entity.PortfolioContactAddressMailTrack()
            //    {
            //        AddressID = addressID,
            //        MailSentDateTime = aDetials.PayDate,
            //        SentBy = 0
            //    });
            //    //get the result after adding
            //    rlist = ptRep.GetAll().Where(o => o.AddressID == addressID).ToList();
            //}


            dlist = (from p in rlist
                     select new SentMailDisplay()
                     {
                         AddressID = p.AddressID,
                         DisplayData = "Welcome email sent on " + string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), p.MailSentDateTime)
                     }).ToList();

            return dlist;
        }

        #endregion
        public static void DownloadPolicyMail(int addressid)
        {
            try
            {
                string fname = HttpContext.Current.Server.MapPath("~/WF/UploadData/Policy/" + addressid.ToString() + "/Policy.pdf");
                //if (!File.Exists(fname))
                //{
                    GeneratePolicyNew(addressid);
               // }
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=Policy.pdf");
                HttpContext.Current.Response.TransmitFile(fname);
                HttpContext.Current.Response.End();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public static void SendPolicyMail(int addressid)
        {
            try
            {
                GeneratePolicyNew(addressid);

                var paRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                var pEntity = paRep.GetAll().Where(o => o.ID == addressid).FirstOrDefault();
                var pcRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                var pcEntity = pcRep.GetAll().Where(o => o.ID == pEntity.ContactID).FirstOrDefault();
                LoginDetailsMail(pcEntity.ID, addressid, pcEntity, "~/WF/UploadData/Policy/" + addressid.ToString() + "/Policy.pdf");
            }
            catch(Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        public static void GeneratePolicyNew(int addressid)
        {
            //Send policy email
            string content = GeneratePolicyHTML(addressid);
            string dname = HttpContext.Current.Server.MapPath("~/WF/UploadData/Policy/" + addressid);
            if (!Directory.Exists(dname))
            {
                Directory.CreateDirectory(dname);
            }
            System.IO.File.WriteAllText(Path.Combine(dname, "Policy.html"), content);

            //Generate PDF
            PdfGenerator.PdfGenerator.HtmlToPdf("~/WF/UploadData/Policy/" + addressid, "Policy", new string[] { string.Format(@"{1}/WF/UploadData/Policy/{0}/Policy.html", addressid, Deffinity.systemdefaults.GetWebUrl()) }, new string[] { "--print-media-type" });
        }

        public static string GeneratePolicyHTML(int AddressID)
        {
            string retval = string.Empty;
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.v_PortfolioContactAddress> pRep = new PortfolioRepository<PortfolioMgt.Entity.v_PortfolioContactAddress>();
                var d = pRep.GetAll().Where(o => o.AddressID == AddressID).OrderByDescending(o => o.PayID).FirstOrDefault();

                Emailer em = new Emailer();
                string body = em.ReadFile("~/WF/DC/EmailTemplates/Policy.html");
                if (d != null)
                {
                    body = body.Replace("[Policynumber]", d.PolicyNumber);
                    body = body.Replace("[Policystarts]", d.PolicyStartsValue.ToString());
                    body = body.Replace("[Startdate]", string.Format(Deffinity.systemdefaults.GetStringDateformat(), d.PolicyStartdate));
                    body = body.Replace("[Expirydate]", string.Format(Deffinity.systemdefaults.GetStringDateformat(), d.PolicyEnddate));
                    body = body.Replace("[Address]", d.Address + " " + d.Address2 + "<br>" + d.City + ", " + d.State + " " + d.PostCode);
                    body = body.Replace("[Propertytype]", d.PropertyType);
                    body = body.Replace("[Other]", d.IsLessThan5KSqft.HasValue ? (d.IsLessThan5KSqft.Value==false ? "Under" : "Over") : "Under");
                    body = body.Replace("[Paymenttype]", d.ContractTermName);
                    body = body.Replace("[Price]", string.Format("{0:F2}", d.PaidAmount));
                    body = body.Replace("[Purchasedate]", string.Format(Deffinity.systemdefaults.GetStringDateformat(), d.PayDate));
                    body = body.Replace("[Deductible]", d.Deductible.ToString());
                    body = body.Replace("[Contactname]", d.Name);
                    body = body.Replace("[Contactno]", d.ContactNo);
                    body = body.Replace("[Contactemail]", d.Email);
                    body = body.Replace("[Billingaddress]", d.BillingAddress1 + " " + d.BillingAddress2 + "<br> " + d.BillingCity + ", " + d.BillingState + " " + d.BillingZipcode);
                    body = body.Replace("[Policytype]", d.PolicyTypeName);
                    body = body.Replace("[policydetails]", GetPolicyOptions(d.PolicyTypeName, d.AddressID));
                    retval = body;
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;

        }

        public static string GetPolicyOptions(string policytype, int AddressID)
        {
            string retval = string.Empty;

            if (policytype == "Total Home Guard")
            {
                retval = @"<table width='100%' cellpadding='0' cellspacing='0' style='table-layout: fixed;'>
        <thead>
            <tr>
                <th style='padding:5px 10px;background:#e94753;font-size: 16px;color: #fff '>SYSTEMS</th>
                <th style='padding:5px 10px;background:#e94753;font-size: 16px;color: #fff '>APPLIANCES</th>
                <th style='padding:5px 10px;background:#e94753;font-size: 16px;color: #fff '>OPTIONAL ADD-ON’S</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td valign='top'>
                    <ul>
                        <li>Air Conditioning</li>
                        <li>Heating</li>
                        <li>Ductwork</li>
                        <li>Plumbing</li>
                        <li>Electrical</li>
                        <li>Water Heater</li>
                    </ul>
                </td>
                <td valign='top' style='border-left: 2px solid #ccc; border-right: 2px solid #ccc'>
                    <ul>
                        <li>Clothes Washer</li>
                        <li>Clothes Dryer</li>
                        <li>
                            Kitchen Refrigerator w/Ice
                            <br> Maker and Dispenser
                        </li>
                        <li>Built-in Microwave Oven</li>
                        <li>Dishwasher</li>
                        <li>Garbage Disposal</li>
                        <li>Range/Oven/Cooktop</li>
                        <li>Ceiling and Exhaust Fan</li>
                        <li>Garage Door Opener</li>
                    </ul>
                </td>
                <td valign='top'>
                    <ul>
                        " + GetAddonNames(AddressID) + @"

                    </ul>
                </td>
            </tr>
        </tbody>
    </table>";
            }
            else if (policytype == "System Guard")
            {
                retval = @"<table width='100%' cellpadding='0' cellspacing='0' style='table-layout: fixed;'>
        <thead>
            <tr>
                <th style='padding:5px 10px;background:#e94753;font-size: 16px;color: #fff '>SYSTEMS</th>
                
                <th style='padding:5px 10px;background:#e94753;font-size: 16px;color: #fff '>OPTIONAL ADD-ON’S</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td valign='top'>
                    <ul>
                        <li>Air Conditioning</li>
                        <li>Heating</li>
                        <li>Ductwork</li>
                        <li>Plumbing</li>
                        <li>Electrical</li>
                        <li>Water Heater</li>
                    </ul>
                </td>
                
                 <td valign='top'>
                    <ul>
                        " + GetAddonNames(AddressID) + @"
                    </ul>
                </td>
            </tr>
        </tbody>
    </table>";
            }
            else if (policytype == "Appliance Guard")
            {
                retval = @"<table width='100%' cellpadding='0' cellspacing='0' style='table-layout: fixed;'>
        <thead>
            <tr>
               
                <th style='padding:5px 10px;background:#e94753;font-size: 16px;color: #fff '>APPLIANCES</th>
                <th style='padding:5px 10px;background:#e94753;font-size: 16px;color: #fff '>OPTIONAL ADD-ON’S</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td valign='top' style='border-left: 2px solid #ccc; border-right: 2px solid #ccc'>
                    <ul>
                        <li>Clothes Washer</li>
                        <li>Clothes Dryer</li>
                        <li>
                            Kitchen Refrigerator w/Ice
                            <br> Maker and Dispenser
                        </li>
                        <li>Built-in Microwave Oven</li>
                        <li>Dishwasher</li>
                        <li>Garbage Disposal</li>
                        <li>Range/Oven/Cooktop</li>
                        <li>Ceiling and Exhaust Fan</li>
                        <li>Garage Door Opener</li>
                    </ul>
                </td>
                 <td valign='top'>
                    <ul>
                        " + GetAddonNames(AddressID) + @"
                    </ul>
                </td>
            </tr>
        </tbody>
    </table>";
            }
            return retval;
        }

        public static string GetAddonNames(int Addressid)
        {
            string retval = string.Empty;
            IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> aRep = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();
             IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice> apRep = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice>();
            var aList = aRep.GetAll().Where(o => o.AddressID == Addressid).ToList();
            var apList = apRep.GetAll().ToList();
            if (aList.Count > 0)
            {
                foreach (var s in aList)
                {
                    retval = retval + string.Format("<li>{0}</li>", apList.Where(p=>p.PAPID == s.AddonID).FirstOrDefault().AddOnDetails);
                }
            }
            return retval;
        }
        public static string GetUserName(List<UserMgt.Entity.Contractor> userlist, string UserName)
        {
            string retVal = string.Empty;
            bool checkUserExist = false;
            int i = 1;
            while (!checkUserExist)
            {
                int cnt = userlist.Where(p => p.LoginName == UserName).Count();
                if (cnt > 0)
                {

                    UserName = UserName + i.ToString();
                    retVal = UserName;
                    checkUserExist = false;
                    i++;
                }
                else
                {
                    retVal = UserName;
                    checkUserExist = true;
                }
            }



            return retVal;
        }
        public static void ContractorsAndAssociateInsert(int contactid)
        {
            try
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    using (PortfolioDataContext pd = new PortfolioDataContext())
                    {
                        var pcontact = pd.PortfolioContacts.Where(o => o.ID == contactid).FirstOrDefault();
                        string name = pcontact.Name;
                        string email = pcontact.Email;
                        string contactNo = pcontact.Telephone;
                        var contactUsers = pd.PortfolioContactAssociates.Where(o => o.ContactID == contactid).FirstOrDefault();
                        if (contactUsers == null)
                        {
                            //
                            UserMgt.Entity.Contractor cont = new UserMgt.Entity.Contractor();
                            string[] loginname = name.Split(' ');
                            string userName = string.Empty;
                            string password = string.Empty;

                            if (loginname.Length > 1)
                            {
                                if (loginname[0].Length > 1)
                                    userName = cont.LoginName = loginname[0].Remove(1).ToLower() + loginname[1].ToLower();
                                else
                                    userName = cont.LoginName = loginname[0].ToLower() + loginname[1].ToLower();
                            }
                            else
                            {
                                userName = cont.LoginName = loginname[0].Remove(1).ToLower() + loginname[0].ToLower();
                            }
                            //Check the user name is exists 
                            //if exists get new name
                            cont.LoginName = GetUserName(ud.Contractors.Select(p => p).ToList(), userName);
                            password = Membership.GeneratePassword(8, 0);
                            cont.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                            cont.ContractorName = name;
                            cont.EmailAddress = email;
                            var cno = contactNo;
                            if (cno.Length > 20)
                                cno = contactNo.Substring(0, 20);
                            cont.ContactNumber = cno;
                            cont.Status = "Active";
                            cont.CreatedDate = DateTime.Now;
                            cont.ModifiedDate = DateTime.Now;
                            //customer user type
                            cont.SID = 7;
                            cont.ResetPassword = false;
                            cont.IsImage = false;

                            ud.Contractors.InsertOnSubmit(cont);
                            ud.SubmitChanges();

                            //update details password
                            PortfolioContactLoginDeatilsBAL.PortfolioContactLoginDeatils_AddUpdate(contactid, cont.ID, cont.LoginName, password);
                            AssignedCustomerToPortfolio actp = pd.AssignedCustomerToPortfolios.Where(a => a.Portfolio == sessionKeys.PortfolioID && a.CustomerID == cont.ID).Select(a => a).FirstOrDefault();
                            if (actp == null)
                            {
                                AssignedCustomerToPortfolio acp = new AssignedCustomerToPortfolio();
                                acp.Portfolio = sessionKeys.PortfolioID;
                                acp.CustomerID = cont.ID;
                                pd.AssignedCustomerToPortfolios.InsertOnSubmit(acp);
                                pd.SubmitChanges();
                            }
                            contactUsers = new PortfolioContactAssociate();
                            contactUsers.ContactID = contactid;
                            contactUsers.CustomerUserID = cont.ID;
                            pd.PortfolioContactAssociates.InsertOnSubmit(contactUsers);
                            pd.SubmitChanges();
                            //Add customer user to Assoicate Contact table
                            // DC.BLL.CustomerDetailsBAL.PortfolioContactAssociate_Insert(cont.ID, sessionKeys.PortfolioID);


                            //Mail to New Contractors
                            // LoginDetailsMail(cont.ContractorName, cont.LoginName, password, cont.EmailAddress);
                            //enable login to portal
                            pcontact.LogintoPortal = true;
                            pd.SubmitChanges();


                        }
                        else
                        {

                            //check portfolio associate is working
                            AssignedCustomerToPortfolio actp = pd.AssignedCustomerToPortfolios.Where(a => a.Portfolio == sessionKeys.PortfolioID && a.CustomerID == contactid).Select(a => a).FirstOrDefault();
                            if (actp == null)
                            {
                                AssignedCustomerToPortfolio acp = new AssignedCustomerToPortfolio();
                                acp.Portfolio = sessionKeys.PortfolioID;
                                acp.CustomerID = contactid;
                                pd.AssignedCustomerToPortfolios.InsertOnSubmit(acp);
                                pd.SubmitChanges();
                            }
                            // IF InActive customer User is ther make active
                            UserMgt.Entity.Contractor Contractor_update = ud.Contractors.Where(c => c.ID == contactid).FirstOrDefault();
                            if (Contractor_update != null)
                            {
                                if (Contractor_update.Status == "InActive")
                                {
                                    Contractor_update.Status = "Active";
                                    ud.SubmitChanges();
                                }
                            }
                            //Add customer user to Assoicate Contact table
                            //DC.BLL.CustomerDetailsBAL.PortfolioContactAssociate_Insert(contactid, sessionKeys.PortfolioID);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        public static void LoginDetailsMail(int ContactID, int AddressID, PortfolioMgt.Entity.PortfolioContact pc, string pdfurl)
        {
            try
            {
                string loginName = string.Empty;
                string password = string.Empty;
                var pEntity = PortfolioMgt.BAL.PortfolioContactLoginDeatilsBAL.PortfolioContactLoginDeatils_SelectByContactID(ContactID);
                //Generate new password and send mail
                if (pEntity == null)
                {
                    IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAssociate> aRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAssociate>();
                    var aEntity = aRep.GetAll().Where(o => o.ContactID == ContactID).FirstOrDefault();
                    if (aEntity == null)
                    {
                        ContractorsAndAssociateInsert(ContactID);
                        //after insering 
                        pEntity = PortfolioMgt.BAL.PortfolioContactLoginDeatilsBAL.PortfolioContactLoginDeatils_SelectByContactID(ContactID);
                        if (pEntity != null)
                        {
                            loginName = pEntity.UserName;
                            password = pEntity.Password;
                        }
                    }
                    else
                    {
                        IUserRepository<UserMgt.Entity.Contractor> uRep = new UserRepository<UserMgt.Entity.Contractor>();
                        var uEntity = uRep.GetAll().Where(o => o.ID == aEntity.CustomerUserID).FirstOrDefault();
                        if (uEntity != null)
                        {
                            loginName = uEntity.LoginName;
                            password = Membership.GeneratePassword(8, 0);
                            uEntity.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                            uEntity.ModifiedDate = DateTime.Now;
                            uRep.Edit(uEntity);


                           //update login
                            PortfolioMgt.BAL.PortfolioContactLoginDeatilsBAL.PortfolioContactLoginDeatils_AddUpdate(ContactID,uEntity.ID,loginName,password);
                        }
                    }
                }
                else
                {
                    loginName = pEntity.UserName;
                    password = pEntity.Password;
                }
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                string subject = "Welcome to your Liberty Home Guard Portal";
                Emailer em = new Emailer();
                string body = em.ReadFile("~/WF/DC/EmailTemplates/ContractorWelcomeMail.htm");
                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                body = body.Replace("[user]", pc.Name);
                body = body.Replace("[username]", loginName);
                body = body.Replace("[password]", password);
                body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                body = body.Replace("[year]", DateTime.Now.Year.ToString());
                // em.SendingMail(fromemailid, subject, body, pc.Email);
                Attachment attachment1 = new Attachment(HttpContext.Current.Server.MapPath(pdfurl));
                Email eMail = new Email();
                eMail.SendingMail(pc.Email, subject, body, fromemailid, attachment1,Deffinity.systemdefaults.GetCCEmailAddress());
                //Log sent mail date time
                PortfolioContactAddressMailTrack_Add(AddressID);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
       


    }
}
