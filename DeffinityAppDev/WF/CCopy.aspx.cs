using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF
{
    public partial class CCopy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> pdrep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();
                var plist = pdrep.GetAll().OrderBy(o => o.PortFolio).ToList();
                ddlFromCustomer.DataSource = plist;
                ddlFromCustomer.DataValueField = "ID";
                ddlFromCustomer.DataTextField = "PortFolio";
                ddlFromCustomer.DataBind();
                ddlFromCustomer.Items.Insert(0, new ListItem("Please select...", "0"));

                ddlToCustomer.DataSource = plist;
                ddlToCustomer.DataValueField = "ID";
                ddlToCustomer.DataTextField = "PortFolio";
                ddlToCustomer.DataBind();
                ddlToCustomer.Items.Insert(0, new ListItem("Please select...", "0"));
            }
        }

        protected void btnCopyUsers_Click(object sender, EventArgs e)
        {
            using (UserMgt.DAL.UserDataContext ud = new UserMgt.DAL.UserDataContext())
            {
                var ulist = ud.UserToCompanies.Where(o => o.CompanyID == Convert.ToInt32(ddlFromCustomer.SelectedValue)).ToList();
                if (ulist.Count > 0)
                {
                    var clist = ud.Contractors.Where(o => ulist.Select(u => u.UserID).ToArray().Contains(o.ID)).ToList();
                    foreach(var c in clist)
                    {
                        var c1 = new UserMgt.Entity.Contractor();
                        //copy to new customer
                        c1.AreaID = c.AreaID ;
                        c1.CasualLabourType = c.CasualLabourType;
                        c1.Company = c.Company;
                        c1.ContactNumber = c.ContactNumber;
                        c1.ContractorName = c.ContractorName;
                        c1.CreatedDate = c.CreatedDate;
                        c1.DepartmentID = c.DepartmentID;
                        c1.Details = c.Details;
                        c1.EmailAddress = c.EmailAddress;
                        c1.EmploymentStartDate = c.EmploymentStartDate;
                        c1.ExpClassification = c.ExpClassification;
                        c1.ForcePeriodicPwd = c.ForcePeriodicPwd;
                        c1.GroupOwnerID = c.GroupOwnerID;
                        c1.isFirstlogin = c.isFirstlogin;
                        c1.IsImage = c.IsImage;
                        c1.LoginName = c.LoginName;
                        c1.ModifiedDate = c.ModifiedDate;
                        c1.NormalBuyingRate = c.NormalBuyingRate;
                        c1.NormalSellingRate = c.NormalSellingRate;
                        c1.OvertimeBuyingRate = c.OvertimeBuyingRate;
                        c1.OvertimeSellingRate = c.OvertimeSellingRate;
                        c1.Password = c.Password;
                        c1.ReleaseDate = c.ReleaseDate;
                        c1.ResetPassword = c.ResetPassword;
                        c1.SecondTSApprover = c.SecondTSApprover;
                        c1.SID = c.SID;
                        c1.Status = c.Status;
                        c1.TimeApproveID = c.TimeApproveID;
                        c1.Type = c.Type;
                        
                        

                        ud.Contractors.InsertOnSubmit(c1);
                        ud.SubmitChanges();
                        //add to user to company table
                        var uc = new UserMgt.Entity.UserToCompany();
                        uc.CompanyID = Convert.ToInt32(ddlToCustomer.SelectedValue);
                        uc.UserID = c1.ID;
                        ud.UserToCompanies.InsertOnSubmit(uc);
                        ud.SubmitChanges();
                    }
                }
            }
        }

        protected void btnCopyCRM_Click(object sender, EventArgs e)
        {
            using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
            {
                var ulist = pd.PortfolioContacts.Where(o => o.PortfolioID == Convert.ToInt32(ddlFromCustomer.SelectedValue)).ToList();
                
                foreach(var contact in ulist)
                {
                    var adList = pd.PortfolioContactAddresses.Where(o => o.ContactID ==contact.ID).ToList();
                    var c1 = new PortfolioMgt.Entity.PortfolioContact();

                    c1.Address1 = contact.Address1;
                    c1.Address2 = contact.Address2;
                    c1.BuildingName = contact.BuildingName;
                    c1.City = contact.City;
                    c1.Country = contact.Country;
                    c1.County = contact.County;
                    c1.DateLogged = contact.DateLogged;
                    c1.DateOfBirth = contact.DateOfBirth;
                    c1.DepartmentID = contact.DepartmentID;
                    c1.Email = contact.Email;
                    c1.Fax = contact.Fax;
                    c1.isDisabled = contact.isDisabled;
                    c1.Key_Contact = contact.Key_Contact;
                    c1.Likes_Dislikes = contact.Likes_Dislikes;
                    c1.Location = contact.Location;
                    c1.LoggedBy = contact.LoggedBy;
                    c1.LogintoPortal = contact.LogintoPortal;
                    c1.Mobile = contact.Mobile;
                    c1.Name = contact.Name;
                    c1.Notes = contact.Notes;
                    c1.PortfolioID = Convert.ToInt32(ddlToCustomer.SelectedValue);
                    c1.Postcode = contact.Postcode;
                    c1.Telephone = contact.Telephone;
                    c1.Title = contact.Title;
                    c1.Town = contact.Town;
                    
                    
                    c1.PortfolioID = Convert.ToInt32(ddlToCustomer.SelectedValue);
                    pd.PortfolioContacts.InsertOnSubmit(c1);
                    pd.SubmitChanges();
                    //add address
                                                           
                    foreach (var c in adList)
                    {
                        var cc1 = new PortfolioMgt.Entity.PortfolioContactAddress();
                        cc1.Address = c.Address;
                        cc1.Address2 = c.Address2;
                        cc1.Amount = c.Amount;
                        cc1.BillingAddress1 = c.BillingAddress1;
                        cc1.BillingAddress2 = c.BillingAddress2;
                        cc1.BillingCity = c.BillingCity;
                        cc1.BillingName = c.BillingName;
                        cc1.BillingState = c.BillingState;
                        cc1.BillingZipcode = c.BillingZipcode;
                        cc1.City = c.City;
                        cc1.ContactID = c1.ID;
                        cc1.IsLessThan5KSqft = c.IsLessThan5KSqft;
                        cc1.LoggedBy = c.LoggedBy;
                        cc1.LoggedDatetime = c.LoggedDatetime;
                        cc1.Notes = c.Notes;
                        cc1.Other = c.Other;
                        cc1.PolicyNumber = c.PolicyNumber;
                        cc1.PolicyStartsID = c.PolicyStartsID;
                        cc1.PolicyTypeID = c.PolicyTypeID;
                        cc1.PostCode = c.PostCode;
                        cc1.StartDate = c.StartDate;
                        cc1.State = c.State;
                        
                       
                        pd.PortfolioContactAddresses.InsertOnSubmit(cc1);
                        pd.SubmitChanges();
                        //address id



                    }
                }
            }
        }
    }
}