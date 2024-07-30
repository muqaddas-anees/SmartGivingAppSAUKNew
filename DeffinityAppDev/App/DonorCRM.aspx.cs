﻿using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using DC.Entity;
using DeffinityAppDev;
using DeffinityAppDev.App;
using DeffinityManager;
using DeffinityManager.DAL;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using PayFast.ApiTypes;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using RestSharp.Extensions;
using StreamChat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TuesPechkin;
using UserMgt.BAL;
using UserMgt.DAL;
using UserMgt.Entity;
using Users;
using static QRCoder.PayloadGenerator.SwissQrCode;

namespace DonorCRM
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllContacts();
                // Get the 'mode' query string parameter

                // Check if 'mode' is "edit"
                HandleViews();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {


        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static bool DeleteFile(string fileId)
        {
            try
            {
                using (var db = new PortfolioDataContext())
                {
                    var file = db.FileDatas.SingleOrDefault(f => f.FileID == fileId);
                    if (file != null)
                    {
                        db.FileDatas.DeleteOnSubmit(file);
                        db.SubmitChanges();
                        return true; // Success
                    }
                    return false; // File not found
                }
            }
            catch (Exception ex)
            {
                // Log exception if necessary
                return false; // Error occurred
            }
        }
        private void HandleViews()
        {
            string mode = Request.QueryString["mode"];
            string idParam = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(mode) && mode.Equals("edit", StringComparison.OrdinalIgnoreCase))
            {
                Title_Card.InnerHtml = "View & Edit Contact";
                MultiView1.ActiveViewIndex = 0; // Show the form view

                if (!string.IsNullOrEmpty(idParam) && int.TryParse(idParam, out int id))
                {
                    // Fetch the list of contacts
                    var contact = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany()
                                 .Where(o => o.CompanyID == sessionKeys.PortfolioID && o.ID == id)
                                // Adjust based on your needs
                                 .FirstOrDefault();

                    // Find the contact with the matching ID

                    if (contact != null)
                    {

                        imgAvatar.ImageUrl = "../ImageHandler.ashx?id=" + id + "&s=user";
                        List<object> contactObjects = new List<object>();
                        PortfolioDataContext context = new PortfolioDataContext();

                        // Fetch all TithingDefaultDetails with non-null unid
                        var tithingDefaultDetails = context.TithingDefaultDetails
                            .Where(td => td.unid != null)  // Filter out records with null unid
                            .GroupBy(td => td.unid)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(td => td.Title).ToList()
                            );

                        IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                        List<TithingPaymentTracker> payments;
                        List<UserSkill> tagss;
                        string Tags = String.Empty;
                        string Interest = String.Empty;
                        using (UserMgt.DAL.UserDataContext tags = new UserMgt.DAL.UserDataContext())
                        {
                            tagss = tags.UserSkills
                                        .Where(i => i.UserId == contact.ID)
                                        .ToList();
                            StringBuilder tagsBuilder = new StringBuilder();
                            StringBuilder interestBuilder = new StringBuilder();

                            foreach (var tag in tagss)
                            {
                                if (tag.Skills != null)
                                {
                                    if (tagsBuilder.Length > 0)
                                    {
                                        tagsBuilder.Append(",");
                                    }
                                    tagsBuilder.Append(tag.Skills);
                                }
                                if (tag.Notes != null)
                                {
                                    if (interestBuilder.Length > 0)
                                    {
                                        interestBuilder.Append(",");
                                    }
                                    interestBuilder.Append(tag.Notes);
                                }
                            }

                            Tags = tagsBuilder.ToString();
                            Interest = interestBuilder.ToString();
                        }

                        using (var context1 = new PortfolioDataContext())
                        {
                            // Fetch the roles for the given ContractorID
                            var roles = context1.tblRoles
                                .Where(r => r.ContractorID == id)
                                .Select(r => r.RoleType)
                                .ToList();

                            // Check the checkboxes based on the roles fetched
                            chkVolunteers.Checked = roles.Contains("Volunteer");
                            chkLeads.Checked = roles.Contains("Lead");
                            chkMembers.Checked = roles.Contains("Member");
                            chkDonors.Checked = roles.Contains("Donor");
                        }

                        // Fetch files where FileID starts with the given SID
                        var fList = fRep.GetAll()
                                        .Where(o => o.Section == ImageManager.file_section_user_doc && o.FileID.StartsWith(contact.ID.ToString()))
                                        .Select(o => new
                                        {
                                            o.FileID,
                                            o.FileName,
                                            o.Section,
                                            o.UserID,
                                            o.UploadedDate
                                        })
                                        .ToList();

                        // Fetch payments
                        using (PortfolioMgt.DAL.PortfolioDataContext db = new PortfolioDataContext())
                        {
                            payments = db.TithingPaymentTrackers
                                         .Where(p => p.DonerEmail == contact.EmailAddress)
                                         .ToList();
                        }
                        double donationsRaised = payments
                            .Where(p => p.IsPaid == true)
                            .Sum(p => p.PaidAmount ?? 0);
                        // Populate the TextBoxes with contact data
                        txtFirstName.Text = contact.ContractorName;
                        RadioButtonListRoles.SelectedValue = contact.SID.ToString();
                        txtLastName.Text = contact.LastName;
                        txtCompanyName.Text = contact.CompanyName;
                        txtEmail.Text = contact.EmailAddress;
                        txtPhone.Text = contact.ContactNumber;
                        txtDonationsRaised.Text = donationsRaised.ToString("N2");
                        txttags.Text = Tags;
                        txtinterest.Text = Interest;
                        // Populate CheckBoxes (categories) based on contact's categories
                        /*   chkDonors.Checked = contact.Categories.Contains("Donors");
                           chkVolunteers.Checked = contact.Categories.Contains("Volunteers");
                           chkLeads.Checked = contact.Categories.Contains("Leads");
                           chkMembers.Checked = contact.Categories.Contains("Members");*/

                        // Populate Tags (Skills and Interests)
                        // Assuming contact has Skills and Interests properties or similar
                        /* Skills.Tagify.SetTags(contact.Skills); // Example method for setting tags
                         Interests.Tagify.SetTags(contact.Interests); // Example method for setting tags*/

                        // Generate the payments table HTML with Bootstrap striped table classes
                        var paymentDetails = payments.Select(p => new
                        {
                            TID=p.ID,
                            ID=p.FundriserUNID,
                            Name=contact.ContractorName,
                            Email=contact.EmailAddress,
                            Amount = p.PaidAmount,
                            PaidDate = p.PaidDate,
                            FundraiserNames = p.FundriserUNID != null && tithingDefaultDetails.TryGetValue(p.FundriserUNID, out var titles)
                                ? string.Join(", ", titles)
                                : "Unknown",
                            PaymentType = p.DonationType == "cash" ? "Cash" : (p.DonationType == "inkind" ? "In Kind" : (p.RecurringType == null ? "Normal" : "Recurring")),
                            Status = p.IsPaid.HasValue && p.IsPaid.Value ? "Successful" : "Failed",
                            PayRef = p.PayRef ?? $"REF{new Random().Next(0, 1000000):D6}",
                            PlatformFee = p.PlatformFee ?? 0,
                            TransactionFee = p.TransactionFee ?? 0,
                            MoreDetails = p.MoreDetails
                        }).ToList();
                        paymentDetails.Reverse();

                        string tableHtml = "<table class='table table-striped'>";
                        tableHtml += "<thead>";
                        tableHtml += "<tr class='text-start text-gray-600 fw-bold fs-7 text-uppercase gs-0'>";
                        tableHtml += "<th class=''>Fundraiser Name</th>";
                        tableHtml += "<th class='min-w-100px'>Amount</th>";
                        tableHtml += "<th class='min-w-125px'>Status</th>";
                        tableHtml += "<th class='min-w-125px'>Date</th>";
                        tableHtml += "<th class='min-w-125px'></th>";


                        tableHtml += "</tr>";
                        tableHtml += "</thead>";
                        tableHtml += "<tbody>";

                        // Loop through your data and add rows to the table
                        if (!paymentDetails.Any())
                        {
                            tableHtml += "<tr><td class='text-center' colspan='5'>No donations made</td></tr>";
                        }
                        else
                        {
                            foreach (var payment in paymentDetails)
                            {

                               

                                    tableHtml += "<tr>";
                                    tableHtml += "<td>" + (string.IsNullOrEmpty(payment.FundraiserNames) ? "None" : payment.FundraiserNames) + "</td>";
                                    tableHtml += "<td style='padding-left:40px'>" + (payment.Amount.HasValue ? string.Format("{0:F2}", payment.Amount) : "None") + "</td>";
                                    tableHtml += "<td>" + (payment.Status == "Successful" ? "<span class='badge badge-light-success'>Successful</span>" : (payment.Status == "Failed" ? "<span class='badge badge-light-danger'>Failed</span>" : "None")) + "</td>";
                                    tableHtml += "<td>" + (payment.PaidDate.HasValue ? payment.PaidDate.Value.ToString("dd/MM/yyyy") : "None") + "</td>";
                                    tableHtml += "<td><button type='button' style='border:1px solid #aca5a5' class='btn btn-light' onclick=\"handlePaymentDetails('" + payment.ID + "', '" + payment.Name + "', '" + payment.Email + "', " + payment.Amount + ", '" + payment.FundraiserNames + "', " + payment.PlatformFee + ", '" + payment.PaymentType + "', '" + payment.Status + "')\">Details</button></td>";
                                

                            }
                        }


                        tableHtml += "</tbody>";
                        tableHtml += "</table>";

                        ltrPayments.Text = tableHtml;

                        UserDataContext contractorsContext = new UserDataContext();
                        var contractorList = contractorsContext.Contractors.ToList();

                        var documents = fList.Select(f => new
                        {
                            FileID = f.FileID,
                            FileName = f.FileName,
                            UploadedDate = f.UploadedDate.HasValue ? f.UploadedDate.Value.ToString("dd/MM/yyyy") : "None",
                            Section = f.Section,
                            ContractorName = f.UserID.HasValue ? contractorList.FirstOrDefault(c => c.ID == f.UserID.Value)?.ContractorName : "Unknown"
                        }).ToList();
                        documents.Reverse();

                        // Generate the documents table HTML with Bootstrap striped table classes
                        string tableHtml1 = "<table id=\"documents-table\" class='table table-striped'>";
                        tableHtml1 += "<thead>";
                        tableHtml1 += "<tr class='text-start text-gray-600 fw-bold fs-7 text-uppercase gs-0'>";
                        tableHtml1 += "<th class=''>Document Name</th>";
                        tableHtml1 += "<th class=''>Uploaded By</th>";
                        tableHtml1 += "<th class=''>Uploaded Date</th>";
                        tableHtml1 += "</tr>";
                        tableHtml1 += "</thead>";
                        tableHtml1 += "<tbody>";

                        // Loop through your documents and add rows to the table
                        if (!documents.Any())
                        {
                            tableHtml1 += "<tr><td class='text-center' colspan='3'>No documents uploaded</td></tr>";
                        }
                        else
                        {
                            foreach (var document in documents)
                            {
                                string link = "../ImageHandler.ashx?id=" + document.FileID + "&s=" + document.Section;
                                tableHtml1 += "<tr>";
                                tableHtml1 += "<td><a href='" + link + "'>" + document.FileName + "</a></td>";
                                tableHtml1 += "<td>" + document.ContractorName + "</td>";
                                tableHtml1 += "<td>" + document.UploadedDate + "</td>";
                                tableHtml1 += "<td>" + "<button class='btn btn-light hover-primary' type='button' onclick='deleteFile(\"" + document.FileID + "\")'><i class='bi bi-trash'></i></button>" + "</td>";
                                tableHtml1 += "</tr>";
                            }
                        }

                        tableHtml1 += "</tbody>";
                        tableHtml1 += "</table>";

                        ltrDocuments.Text = tableHtml1;
                    }
                    else
                    {
                        Response.Redirect("dashboard.aspx");     // Handle case where contact is not found
                        // e.g., show a message or redirect
                    }
                }
             
            }
            else
            {
                Title_Card.InnerHtml = "Add New Contact";

            }
        }

        // Helper method to determine SID based on selected categories


        private void LoadAllContacts()
        {
            // Fetch all contacts without a company filter first
            var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany()
         .Where(o => o.CompanyID == sessionKeys.PortfolioID && o.SID == UserType.Donor)
         .OrderBy(o => o.ContractorName)
         .ToList();
            DisplayCategoryCounts(iList);
            // Fetch SID from query parameters
            string sidQueryParam = Request.QueryString["SID"];
            int? specificSID = null;

            if (int.TryParse(sidQueryParam, out int sidValue))
            {
                specificSID = sidValue;
            }

            if (specificSID.HasValue)
            {
                string Role = "";
                if (specificSID == 1) Role = "Donor";
                if (specificSID == 2) Role = "Volunteer";
                if (specificSID == 3) Role = "Lead";
                if (specificSID == 4) Role = "Sponsor";
                using (PortfolioDataContext context = new PortfolioDataContext())
                {

                    // Fetch roles for contractors with specific SID
                    var contractorRoles = from role in context.tblRoles
                                          where role.RoleType == Role
                                          select role.ContractorID;

                    // Filter contacts based on fetched contractor IDs
                    iList = iList.Where(o => contractorRoles.Contains(o.ID)).ToList();
                }
            }

            LoadContacts(iList);
        }


        private int GetSIDValue(string contactType)
        {
            switch (contactType.ToLower())
            {
                case "manager/admin":
                    return 1;
                case "donor":
                    return 2;
                case "fundraiser":
                    return 3;
                case "volunteer":
                    return 4;
                default:
                    throw new ArgumentException("Invalid contact type");
            }
        }

        private void LoadContacts(List<UserMgt.Entity.v_contractor> contacts)
        {
            List<object> contactObjects = new List<object>();
            PortfolioDataContext context = new PortfolioDataContext();

            // Fetch all TithingDefaultDetails with non-null unid
            var tithingDefaultDetails = context.TithingDefaultDetails
                .Where(td => td.unid != null)  // Filter out records with null unid
                .GroupBy(td => td.unid)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(td => td.Title).ToList()
                );

            IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

            // Fetch all contractors from the database
            UserDataContext contractorsContext = new UserDataContext();
            var contractorList = contractorsContext.Contractors.ToList();

            foreach (var contact in contacts)
            {
                // Query roles for the current contractor
                var contractorRoles = from role in context.tblRoles
                                      where role.ContractorID == contact.ID
                                      select role.RoleType;

                List<TithingPaymentTracker> payments;
                List<UserSkill> tagss;

                // Fetch files where FileID starts with the given SID
                var fList = fRep.GetAll()
                                .Where(o => o.Section == ImageManager.file_section_user_doc && o.FileID.StartsWith(contact.ID.ToString()))
                                .Select(o => new
                                {
                                    o.FileID,
                                    o.FileName,
                                    o.Section,
                                    o.UserID,
                                    o.UploadedDate
                                })
                                .ToList();

                // Fetch payments
                using (PortfolioMgt.DAL.PortfolioDataContext db = new PortfolioDataContext())
                {
                    payments = db.TithingPaymentTrackers
                                 .Where(p => p.DonerEmail == contact.EmailAddress)
                                 .ToList();
                }

                // Fetch tags
                using (UserMgt.DAL.UserDataContext tags = new UserMgt.DAL.UserDataContext())
                {
                    tagss = tags.UserSkills
                                .Where(i => i.UserId == contact.ID)
                                .ToList();
                }

                // Calculate total donations raised
                double donationsRaised = payments.Sum(p => p.PaidAmount ?? 0);

                // Prepare payment details including Titles from TithingDefaultDetails
                var paymentDetails = payments.Select(p => new
                {
                    Amount = p.PaidAmount,
                    PaidDate = p.PaidDate,
                    FundraiserNames = p.FundriserUNID != null && tithingDefaultDetails.TryGetValue(p.FundriserUNID, out var titles)
                        ? string.Join(", ", titles)
                        : "Unknown",
                    PaymentType = p.DonationType == "cash" ? "Cash" : (p.DonationType == "inkind" ? "In Kind" : (p.RecurringType == null ? "Normal" : "Recurring")),
                    Status = p.IsPaid.HasValue && p.IsPaid.Value ? "Successful" : "Failed",
                    PayRef = p.PayRef ?? $"REF{new Random().Next(0, 1000000):D6}",
                    PlatformFee = p.PlatformFee ?? 0,
                    TransactionFee = p.TransactionFee ?? 0,
                    MoreDetails = p.MoreDetails
                }).ToList();

                // Construct JSON object including roles and tags
                var contactObject = new
                {
                    ID = contact.ID,
                    SID = contact.SID,
                    FirstName = contact.ContractorName,
                    LastName = contact.LastName,
                    Email = contact.EmailAddress,
                    CompanyName = contact.Company,
                    imgurl = GetImageUrl(contact.ID.ToString()),
                    Phone = contact.ContactNumber,
                    DonationsRaised = donationsRaised,
                    Country = contact.Country,
                    Roles = contractorRoles.ToArray(),
                    Tags = tagss,
                    Documents = fList.Select(f => new
                    {
                        FileID = f.FileID,
                        FileName = f.FileName,
                        UploadedDate = f.UploadedDate?.ToString("dd/MM/yyyy HH:mm:ss") ?? "N/A",
                        Section = f.Section,
                        ContractorName = f.UserID.HasValue ? contractorList.FirstOrDefault(c => c.ID == f.UserID.Value)?.ContractorName : "Unknown"
                    }).ToList(),
                    Payments = paymentDetails // Include detailed payment information with titles
                };

                // Add to contacts list
                contactObjects.Add(contactObject);
            }

            // Serialize contacts list to JSON array
            string jsonContacts = JsonConvert.SerializeObject(contactObjects);
            int cid = sessionKeys.UID;

            // Inject JSON array into client-side script
            var scriptTag = new LiteralControl();
            string scriptText = $@"
<script type='text/javascript'>
var id={cid}
console.log('iddddd'+id)
    var contacts = {jsonContacts};
    console.log(contacts);
    console.log('Loaded {contactObjects.Count} contacts from server.');
    // You can now use 'contacts' array in your JavaScript code
</script>
";

            // Register script to be executed when page renders
            ((HtmlGenericControl)scripts2).InnerHtml = scriptText;

            // Display contacts in HTML (optional, if needed)
            DisplayContactsInHTML(contacts);

        }

        public int GetUniqueValue(string idPrefix)
        {
            IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

            int fileCount = 0;
            bool isUnique = false;

            while (!isUnique)
            {
                fileCount++;
                string potentialFileId = $"{idPrefix}_{fileCount}";

                // Check if a file with the same FileID already exists
                isUnique = !fRep.GetAll().Any(o => o.Section == ImageManager.file_section_user_doc && o.FileID == potentialFileId);
            }

            return fileCount;
        }

        private void DisplayCategoryCounts(List<UserMgt.Entity.v_contractor> contacts)
        {
            // Initialize counts
            int allCount = 0;
            int donorsCount = 0;
            int volunteersCount = 0;
            int leadsCount = 0;
            int sponsorsCount = 0;
            PortfolioDataContext context = new PortfolioDataContext();

            foreach (var contact in contacts)
            {
                // Fetch roles for the current contractor from tblRoles

                var contractorRoles = context.tblRoles
                                               .Where(r => r.ContractorID == contact.ID)  // Filter roles for current contractor
                                               .ToList();

                // Check each role type for the current contractor
                bool isDonor = contractorRoles.Any(r => r.RoleType == "Donor");

                bool isVolunteer = contractorRoles.Any(r => r.RoleType == "Volunteer");
                bool isLead = contractorRoles.Any(r => r.RoleType == "Lead");
                bool isSponsor = contractorRoles.Any(r => r.RoleType == "Sponsor");

                // Increment counts based on role presence
                if (isDonor) donorsCount++;
                if (contact.SID == 2) allCount++;
                if (isVolunteer) volunteersCount++;
                if (isLead) leadsCount++;
                if (isSponsor) sponsorsCount++;

                // Increment total count of roles

            }

            // Inject counts into client-side script or display in HTML
            var scriptTag = new LiteralControl();
            string scriptText = $@"
        <script type='text/javascript'>
            var allCount = {allCount};
            var donorsCount = {donorsCount};
            var volunteersCount = {volunteersCount};
            var leadsCount = {leadsCount};
            var sponsorsCount = {sponsorsCount};
            console.log('Counts: Donors=' + donorsCount + ', Volunteers=' + volunteersCount + ', Leads=' + leadsCount + ', Sponsors=' + sponsorsCount);
        </script>";

            // Register script to be executed when page renders
            ((HtmlGenericControl)scripts).InnerHtml = scriptText;
        }

        protected static string GetImageUrl(string contactsId)
        {

            return "/ImageHandler.ashx?id=" + contactsId + "&s=" + ImageManager.file_section_user; //"img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
        }

        private void DisplayContactsInHTML(List<UserMgt.Entity.v_contractor> contacts)
        {
            StringBuilder html = new StringBuilder();
            string sidQueryParam = Request.QueryString["SID"];




            foreach (var contact in contacts)
            {
                string name = contact.ContractorName;
                string email = contact.EmailAddress;
                string companyName = contact.Company;
                string phone = contact.ContactNumber;
                string contactid = contact.ID.ToString();
                string imgurl = GetImageUrl(contactid);
                html.Append(string.Format(@"
<a href='?mode=edit&id={0}&SID={4}'>
    <div style='display: flex; align-items: center;cursor:pointer;' class='text-hover-primary contact-item py-4' >
        <div class='symbol symbol-40px symbol-circle'>
            <img alt='Pic' src='{1}'>
            <div class='symbol-badge bg-success start-100 top-100 border-4 h-15px w-15px ms-n2 mt-n2'></div>
        </div>
        
        <div style='margin-left: 30px;' class='ms-4'>
            <p class='fs-6 fw-bold text-gray-900 text-hover-primary mb-2'>{3}</p>
            <div class='fw-semibold fs-7 text-muted'>{2}</div>
        </div>
    </div></a>
    <div class='separator separator-dashed'></div>
", contactid, ResolveUrl(imgurl), email, name, sidQueryParam));



                // Display the HTML in a literal or another control
                ContactsListLiteral.Text = html.ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string idParam = Request.QueryString["id"];
            if (idParam != null)
            {
                int ID = int.Parse(idParam);
                var contact = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany()
                   .Where(o =>  o.ID == ID)
                  // Adjust based on your needs
                   .FirstOrDefault();

                if (contact != null)
                {
                    using (var userContext = new UserDataContext())
                    {
                        var contact1 = userContext.Contractors.FirstOrDefault(o => o.ID == ID);
                        // Update the contractor's properties with the new values from the form
                        contact1.SID = int.Parse(RadioButtonListRoles.SelectedValue);
                        contact1.ContractorName = txtFirstName.Text.Trim();
                        contact1.LastName = txtLastName.Text.Trim();
                        contact1.EmailAddress = txtEmail.Text;
                        contact1.LoginName = txtEmail.Text;
                        // Check if txtPassword.Text is not empty or whitespace
                        if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                        {
                            // Generate a new password and update contact1.Password
                            contact1.Password = Deffinity.Users.Login.GeneratePasswordString(txtPassword.Text.Trim());
                        }

                        contact1.ModifiedDate = DateTime.Now;
                        contact1.Company = txtCompanyName.Text;
                        contact1.ContactNumber = txtPhone.Text.Trim();

                        // Save the changes to the database
                        userContext.SubmitChanges();
                    }
                using (var context = new PortfolioDataContext())
                    {
                        // Check if the role already exists
                        if (chkVolunteers.Checked)
                        {

                            bool roleExists = context.tblRoles.Any(r => r.ContractorID == ID && r.RoleType == "Volunteer");
                            if (!roleExists)
                            {
                                // Add new role
                                var newRole = new tblRole
                                {
                                    ContractorID = ID,
                                    RoleType = "Volunteer"
                                    // Add other properties as needed
                                };
                                context.tblRoles.InsertOnSubmit(newRole);

                            }
                        }

                        if (chkLeads.Checked)
                        {
                            bool roleExists = context.tblRoles.Any(r => r.ContractorID == ID && r.RoleType == "Lead");
                            if (!roleExists)
                            {
                                // Add new role
                                var newRole = new tblRole
                                {
                                    ContractorID = ID,
                                    RoleType = "Lead"
                                    // Add other properties as needed
                                };
                                context.tblRoles.InsertOnSubmit(newRole);

                            }
                        }

                        if (chkMembers.Checked)
                        {
                            bool roleExists = context.tblRoles.Any(r => r.ContractorID == ID && r.RoleType == "Member");
                            if (!roleExists)
                            {
                                // Add new role
                                var newRole = new tblRole
                                {
                                    ContractorID = ID,
                                    RoleType = "Member"
                                    // Add other properties as needed
                                };
                                context.tblRoles.InsertOnSubmit(newRole);

                            }
                        }
                        if (chkDonors.Checked)
                        {
                            bool roleExists = context.tblRoles.Any(r => r.ContractorID == ID && r.RoleType == "Member");
                            if (!roleExists)
                            {
                                // Add new role
                                var newRole = new tblRole
                                {
                                    ContractorID = ID,
                                    RoleType = "Donor"
                                    // Add other properties as needed
                                };
                                context.tblRoles.InsertOnSubmit(newRole);

                            }
                        }
                        UserSkill Usertag = new UserSkill();
                        using (UserMgt.DAL.UserDataContext tags = new UserMgt.DAL.UserDataContext())
                        {
                            Usertag = tags.UserSkills
                                        .Where(i => i.UserId == contact.ID)
                                        .FirstOrDefault();
                            if (Usertag != null)
                            {
                                Usertag.Skills = txttags.Text;
                                Usertag.Notes = txtinterest.Text;
                            }
                            else
                            {

                            }
                        }

                        // Save changes to the database
                        context.SubmitChanges();
                    }



















                }

                int UserID = sessionKeys.UID;
                DateTime Date = DateTime.Now;
                if (DocumentFile.HasFiles) // Check if there are multiple files
                {
                    // Initialize the repository
                    IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                    // Define the ID prefix to search for
                    string idPrefix = ID.ToString(); // Convert the ID to a string

                    // Get the count of files where FileID starts with the given ID prefix
                    int fileCount = fRep.GetAll()
                        .Where(o => o.FileID.StartsWith(idPrefix))
                        .Count();

                    foreach (HttpPostedFile uploadedFile in DocumentFile.PostedFiles)
                    {
                        // Define fileid using ID and a unique value
                        string fileid = $"{ID}_{GetUniqueValue(ID.ToString())}";

                        // Read the file into a byte array
                        byte[] fileBytes;
                        using (var binaryReader = new BinaryReader(uploadedFile.InputStream))
                        {
                            fileBytes = binaryReader.ReadBytes(uploadedFile.ContentLength);
                        }

                        // Save the file using ImageManager.FileDBSave
                        ImageManager.FileDBSave(
                            UserID,
                            Date,
                            fileBytes, // filebytearray
                            null, // Smallfilebytearray is null
                            fileid, // fileid
                            ImageManager.file_section_user_doc,
                            System.IO.Path.GetExtension(uploadedFile.FileName).ToLower(),
                            uploadedFile.FileName,
                            uploadedFile.ContentType,
                            null, // Assuming foldername is defined elsewhere
                            true // allowMutifile
                        );
                    }
                }



                if (AvatarUpload.HasFile)
                {
                    Response.Write("File reached");

                    // Get the file extension
                    string fileExtension = Path.GetExtension(AvatarUpload.FileName).ToLower();
                    byte[] bytes = AvatarUpload.FileBytes;

                    // Initialize the repository
                    IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                    // Get the existing file record based on section and FileID
                    var existingFileRecord = fRep.GetAll().Where(o => o.Section == ImageManager.file_section_user && o.FileID == idParam).FirstOrDefault();

                    if (existingFileRecord != null)
                    {
                        // Update the existing record with the new file data
                        existingFileRecord.FileData1 = bytes;
                        existingFileRecord.FileName = AvatarUpload.FileName;
                        existingFileRecord.UploadedDate = DateTime.Now;

                        // Save the changes to the database
                        fRep.Edit(existingFileRecord);
                        fRep.Save(); // Assuming Save() commits the changes
                    }
                    else
                    {
                        // Save the file to the database using ImageManager if no existing record is found
                        ImageManager.FileDBSave(
                            bytes,
                            null,
                            idParam,
                            ImageManager.file_section_user,
                            fileExtension,
                            AvatarUpload.FileName,
                            AvatarUpload.PostedFile.ContentType,
                            "",
                            true
                        );
                    }
                }

                LoadAllContacts();
                // Get the 'mode' query string parameter

                // Check if 'mode' is "edit"
                HandleViews();
            }
            else
            {
                Button1.Text = "Add New Contact";
                int contractorID = -1;
                using (var userContext = new UserDataContext())
                {
                    string pw = "Smart@2022";


                    // Create a new Contractor instance
                    var newContractor = new UserMgt.Entity.Contractor
                    {
                        SID = int.Parse(RadioButtonListRoles.SelectedValue),
                        ContractorName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        EmailAddress = txtEmail.Text,
                        LoginName = txtEmail.Text,
                        Password = !string.IsNullOrWhiteSpace(txtPassword.Text) ?
                            Deffinity.Users.Login.GeneratePasswordString(txtPassword.Text.Trim()) :
                            Deffinity.Users.Login.GeneratePasswordString(pw),
                        // Assuming 2 is for Donor
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Status = "Active", // Default or provided status
                        isFirstlogin = 0,
                        ResetPassword = false,
                        Company = txtCompanyName.Text,
                        ContactNumber = txtPhone.Text.Trim(),


                    };


                    // Check if the email already exists
                    bool emailExists = userContext.Contractors
                        .Any(c => c.LoginName.ToLower().Trim() == newContractor.LoginName.ToLower().Trim() ); // Check only Donor type

                    if (!emailExists)
                    {
                        // Add new contractor to the context
                        userContext.Contractors.InsertOnSubmit(newContractor); // Save changes to the database
                        userContext.SubmitChanges();
                        contractorID = newContractor.ID;
                   
                        var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                        var urEntity = new UserMgt.Entity.UserToCompany();
                        urEntity.CompanyID = sessionKeys.PortfolioID;
                        urEntity.UserID = contractorID;
                        urRep.Add(urEntity);
                    }
                    else
                    {
                        Response.Write("<div class=\"alert alert-danger\" role=\"alert\">\r\n  Email Already Exists! \r\n</div>");
                    }
                    LoadAllContacts();
                    // Get the 'mode' query string parameter

                    // Check if 'mode' is "edit"
                    HandleViews();
                    // Optionally, add related records (roles, tags, etc.)

                }



                using (var context = new PortfolioDataContext())
                {
                    if (contractorID != -1)
                    {
                        if (chkVolunteers.Checked)
                        {
                            var newRole = new tblRole
                            {
                                ContractorID = contractorID,
                                RoleType = "Volunteer"
                                // Add other properties as needed
                            };
                            context.tblRoles.InsertOnSubmit(newRole);
                        }

                        if (chkLeads.Checked)
                        {
                            var newRole = new tblRole
                            {
                                ContractorID = contractorID,
                                RoleType = "Lead"
                                // Add other properties as needed
                            };
                            context.tblRoles.InsertOnSubmit(newRole);
                        }

                        if (chkMembers.Checked)
                        {
                            var newRole = new tblRole
                            {
                                ContractorID = contractorID,
                                RoleType = "Member"
                                // Add other properties as needed
                            };
                            context.tblRoles.InsertOnSubmit(newRole);
                        }
                        if (chkDonors.Checked)
                        {
                            var newRole = new tblRole
                            {
                                ContractorID = contractorID,
                                RoleType = "Donor"
                                // Add other properties as needed
                            };
                            context.tblRoles.InsertOnSubmit(newRole);
                        }
                        context.SubmitChanges();
                    }
                }

                UserSkill Usertag = new UserSkill();
                using (UserMgt.DAL.UserDataContext tags = new UserMgt.DAL.UserDataContext())
                {
                    Usertag = tags.UserSkills
                                .Where(i => i.UserId == contractorID)
                                .FirstOrDefault();
                    if (Usertag != null)
                    {
                        Usertag.Skills = txttags.Text;
                        Usertag.Notes = txtinterest.Text;
                    }
                }

                // Save changes to the database



                int UserID = sessionKeys.UID;
                DateTime Date = DateTime.Now;
                if (DocumentFile.HasFiles) // Check if there are multiple files
                {
                    // Initialize the repository
                    IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                    // Define the ID prefix to search for
                    string idPrefix = ID.ToString(); // Convert the ID to a string

                    // Get the count of files where FileID starts with the given ID prefix
                    int fileCount = fRep.GetAll()
                        .Where(o => o.FileID.StartsWith(idPrefix))
                        .Count();

                    foreach (HttpPostedFile uploadedFile in DocumentFile.PostedFiles)
                    {
                        // Define fileid using ID and a unique value
                        string fileid = $"{ID}_{GetUniqueValue(ID.ToString())}";

                        // Read the file into a byte array
                        byte[] fileBytes;
                        using (var binaryReader = new BinaryReader(uploadedFile.InputStream))
                        {
                            fileBytes = binaryReader.ReadBytes(uploadedFile.ContentLength);
                        }

                        // Save the file using ImageManager.FileDBSave
                        ImageManager.FileDBSave(
                            UserID,
                            Date,
                            fileBytes, // filebytearray
                            null, // Smallfilebytearray is null
                            fileid, // fileid
                            ImageManager.file_section_user_doc,
                            System.IO.Path.GetExtension(uploadedFile.FileName).ToLower(),
                            uploadedFile.FileName,
                            uploadedFile.ContentType,
                            null, // Assuming foldername is defined elsewhere
                            true // allowMutifile
                        );
                    }
                }


                if (AvatarUpload.HasFile)
                {

                    // Get the file extension
                    string fileExtension = Path.GetExtension(AvatarUpload.FileName).ToLower();
                    byte[] bytes = AvatarUpload.FileBytes;

                    // Initialize the repository
                    IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                    // Get the existing file record based on section and FileID
                    var existingFileRecord = fRep.GetAll().Where(o => o.Section == ImageManager.file_section_user && o.FileID == idParam).FirstOrDefault();

                    if (existingFileRecord != null)
                    {
                        // Update the existing record with the new file data
                        existingFileRecord.FileData1 = bytes;
                        existingFileRecord.FileName = AvatarUpload.FileName;
                        existingFileRecord.UploadedDate = DateTime.Now;

                        // Save the changes to the database
                        fRep.Edit(existingFileRecord);
                        fRep.Save(); // Assuming Save() commits the changes
                    }
                    else
                    {
                        // Save the file to the database using ImageManager if no existing record is found
                        ImageManager.FileDBSave(
                            bytes,
                            null,
                            contractorID.ToString(),
                            ImageManager.file_section_user,
                            fileExtension,
                            AvatarUpload.FileName,
                            AvatarUpload.PostedFile.ContentType,
                            "",
                            true
                        );
                    }
                }
            }


        }
      /*  [WebMethod]
        protected void SendReceipt(string id)
        {
           
               
                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
                var tclist = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).ToList();
                var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                Random generator = new Random();
                var dItem = (from t in tList
                                 // join c in ulist on t.LoggedByID equals c.ID
                                 //join tc in tclist on t.TithingID equals tc.ID
                             where t.ID == Convert.ToInt32(id)
                             select new
                             {
                                 ID = t.ID,
                                 Name = t.DonerName,// getUserData(ulist, t.LoggedByID, t.DonerName, "name"),//t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                 Email = t.DonerEmail,// getUserData(ulist, t.LoggedByID, t.DonerName, "email"),// t.LoggedByID == null ? t.DonerEmail : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().EmailAddress),
                                 TithigName = getTithing(tclist, t.TithingID.HasValue ? t.TithingID.Value : 0),// t.TithingID == null ? string.Empty : tclist.Where(o => o.ID == t.TithingID).FirstOrDefault().Title,
                                 PaidBy = getUserData(ulist, t.LoggedByID, t.DonerName, "name"),// t.LoggedByID == null ? t.DonerName : (t.LoggedByID == 0 ? t.DonerName : ulist.Where(o => o.ID == t.LoggedByID).FirstOrDefault().ContractorName),
                                 Amount = t.PaidAmount,
                                 PaidDate = t.PaidDate,
                                 PayRef = t.PayRef == null ? "REF" + generator.Next(0, 1000000).ToString("D6") : t.PayRef,
                                 PaymentType = t.RecurringType == null ? "Normal" : "Recurring",
                                 REcurring = t.RecurringType,
                                 Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-danger'>Failed</span>",
                                 //Status = (t.IsPaid.HasValue ? t.IsPaid.Value : false) ? "<span class='badge badge-success'>Successful</span>" : "<span class='badge badge-success'>Successful</span>",
                                 CategoryListWithAmount = GetDonationCategoriesWithAmount(t.MoreDetails == null ? "" : t.MoreDetails),
                                 CategoryList = GetDonationCategories(t.MoreDetails == null ? "" : t.MoreDetails),
                                 t.MoreDetails,
                                 t.unid,
                                 IsPaid = (t.IsPaid.HasValue ? t.IsPaid.Value : false),

                                 GiftAid = (t.GiftAid.HasValue ? t.GiftAid.Value : false)

                             }).FirstOrDefault();

                // var dItem = rlist.Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();

                if (dItem.unid == null)
                {
                    var dEntity = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                    if (dEntity != null)
                    {
                        dEntity.unid = Guid.NewGuid().ToString();

                        PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_Update(dEntity);
                        
                    }
                }
                   


                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> rp = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                var tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && (o.SetAsDefault.HasValue ? o.SetAsDefault.Value : false) == true).FirstOrDefault();
                if (tn == null)
                    tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && (o.SetAsDefault.HasValue ? o.SetAsDefault.Value : false) == true).FirstOrDefault();


                // ddlTemplate.SelectedValue = tn.ID.ToString();

                String body = "";
                if (tn != null)
                {
                    body = tn.EmailContent;
                    //{{currentyear}}
                    body = body.Replace("{{instancename}}", sessionKeys.PortfolioName);
                    body = body.Replace("{{fundraiserdate}}", dItem.PaidDate.Value.ToShortDateString());
                    body = body.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
                    body = body.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                    body = body.Replace("{{amount}}", string.Format("{1}{0:N2}", dItem.Amount.HasValue ? dItem.Amount.Value : 0, Deffinity.Utility.GetCurrencySymbol()));
                    body = body.Replace("{{name}}", dItem.Name);
                    body = body.Replace("{{category}}", dItem.CategoryList);
                    body = body.Replace("{{signature}}", sessionKeys.PortfolioName);
                    body = body.Replace("{{date}}", dItem.PaidDate.Value.ToShortDateString());


                    body = body.Replace("{{amount}}", string.Format("{1}{0:N2}", dItem.Amount.HasValue ? dItem.Amount.Value : 0, Deffinity.Utility.GetCurrencySymbol()));

                    body = body.Replace("{{donorfirstname}}", dItem.Name);
                    body = body.Replace("{{donorsurname}}", dItem.Name);
                    //donorcompany
                    body = body.Replace("{{category}}", dItem.CategoryList);

                    body = body.Replace("{{donorcompany}}", sessionKeys.PortfolioName);


                    body = body.Replace("{{categorydonationamount}}", string.Format("{0:F2}", dItem.Amount));

                    body = body.Replace("{{categorydonationdate}}", dItem.PaidDate.Value.ToShortDateString());
                    body = body.Replace("{{todaysdate}}", DateTime.Now.ToShortDateString());
                    //logo

                    // body = body.Replace("{{logo}}", "<img src='"+ Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID,Deffinity.systemdefaults.GetLocalPath())+"' />");

                    // body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + "/ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s=portfolio" + "' />");
                    body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + "/ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s=portfolio" + "' />");
                }



                if (!body.Contains("!DOCTYPE HTML PUBLIC"))
                {
                    Emailer em = new Emailer();
                    string html_body = em.ReadFile("~/WF/DC/EmailTemplates/mastertemplate.html");

                    html_body = html_body.Replace("[table]", body);
                    body = html_body;

                    string fromid = Deffinity.systemdefaults.GetFromEmail();

                    string toid = dItem.Email;
                    string subject = "Donation";
                   

                    if (dItem.Status.Contains("Successful"))
               
                    //Email ToEmail = new Email();


                    //ToEmail.SendingMail(fromid, subject,body,toid,"");

                    //sessionKeys.Message = "Your message is on it's way!";

                    //Response.Redirect(Request.RawUrl, false);
                }




            
        }*/
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            int UserID = sessionKeys.UID;
            DateTime Date = DateTime.Now;
            string unid = UNID.Text;

            using (var filedatacontext = new PortfolioDataContext())
            {
                // Get the number of existing files with the same unid prefix
                int fileCount = filedatacontext.FileDatas.Count(file => file.FileID.StartsWith(unid));

                // Create a unique fileid based on unid and file count
                string fileid = $"{unid}_{fileCount + 1}";

                if (ReceiptsUpload.HasFiles) // Check if there are multiple files
                {
                    foreach (HttpPostedFile uploadedFile in ReceiptsUpload.PostedFiles)
                    {
                        // Read the file into a byte array
                        byte[] fileBytes;
                        using (var binaryReader = new BinaryReader(uploadedFile.InputStream))
                        {
                            fileBytes = binaryReader.ReadBytes(uploadedFile.ContentLength);
                        }

                        // Save the file
                        ImageManager.FileDBSave(
                            UserID,
                            Date,
                            fileBytes, // filebytearray
                            null, // Smallfilebytearray is null
                            fileid, // fileid
                            ImageManager.file_section_donor_doc,
                            System.IO.Path.GetExtension(uploadedFile.FileName).ToLower(),
                            uploadedFile.FileName,
                            uploadedFile.ContentType,
                            null, // Assuming foldername is defined elsewhere
                            true // allowMutifile
                        );
                    }
                }
            }
        }

    }
}

