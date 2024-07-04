using Newtonsoft.Json;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DeffinityAppDev.WF.Admin.Services
{
    /// <summary>
    /// Summary description for ManageContactServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ManageContactServices : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public string addNewCustomer(string name, string email, string cell, string address, string city, string state, string zipcode)
        {
            string jsonString = string.Empty;
            try
            {


                try
                {

                    var cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                    var uRepository = new UserRepository<UserMgt.Entity.Contractor>();
                    if (!string.IsNullOrEmpty(email.Trim()) && !string.IsNullOrEmpty(name.Trim()))
                    {
                        //if ((cRepository.GetAll().Where(o => o.Email.ToLower() == emails[0] && o.PortfolioID == sessionKeys.PortfolioID ).Count() == 0) &&
                        //     (uRepository.GetAll().Where(o => o.SID != 7 && o.Status=="Active" && o.EmailAddress.ToLower() == emails[0] ).Count() == 0))
                        if ((cRepository.GetAll().Where(o => o.Email.ToLower() == email && (o.isDisabled.HasValue ? o.isDisabled.Value : false) == false && o.PortfolioID == sessionKeys.PortfolioID).Count() == 0))
                        {
                            var pContact = new PortfolioContact();
                            pContact.Name = name;
                            pContact.PortfolioID = sessionKeys.PortfolioID;
                            pContact.Email = email;
                            pContact.Telephone = cell;
                            pContact.DateOfBirth = Convert.ToDateTime("01/01/1900");
                            pContact.Mobile = cell;
                            pContact.Address1 = string.Empty;
                            pContact.Town = string.Empty;
                            pContact.City = string.Empty;
                            pContact.Postcode = string.Empty;
                            pContact.DateLogged = DateTime.Now;
                            pContact.SourceofLead = "";
                            //pContact.Tags = ListBox_getValues();
                            //add to contact 
                            cRepository.Add(pContact);
                            if (pContact != null)
                            {


                                var contactid = pContact.ID;

                                try
                                {
                                    IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> pRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                                    IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> paRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();
                                    IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();

                                    PortfolioMgt.Entity.PortfolioContactAddress sResult = pRepository.GetAll().Where(o => o.ContactID == contactid).OrderBy(o => o.ID).FirstOrDefault();
                                    //
                                    if (sResult == null)
                                    {
                                        sResult = new PortfolioMgt.Entity.PortfolioContactAddress();
                                        sResult.ContactID = contactid;
                                        sResult.Address = address;
                                        sResult.Address2 = string.Empty;
                                        sResult.Amount = 0;
                                        sResult.City = city;
                                        sResult.State = state;
                                        sResult.PostCode = zipcode;
                                        sResult.LoggedBy = sessionKeys.UID;
                                        sResult.LoggedDatetime = DateTime.Now;
                                        pRepository.Add(sResult);
                                        //Session["msg"] = "Address details added successfully";

                                    }

                                }
                                catch (Exception e)
                                {
                                    LogExceptions.WriteExceptionLog(e);
                                }


                                // lblmsg.Text = "<p class='bg-success'>Added successfully</p>";
                                //Response.Redirect("~/WF/CustomerAdmin/ContactDetails.aspx?ContactID=" + pContact.ID, false);
                            }
                            else
                            {
                                //lblmsg.Text = "Cannot insert contact";
                            }
                        }
                        else
                        {
                            // lblmsg.Text = "<p class='bg-danger'> Email address already exists. please try again </p>";
                        }
                    }

                }
                catch (Exception ex)
                { LogExceptions.WriteExceptionLog(ex); }



                //DataTable table = Bind_UsagesInMainGrid(productid, Cid);
                jsonString = JsonConvert.SerializeObject("");
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return jsonString;
        }

    }
}
