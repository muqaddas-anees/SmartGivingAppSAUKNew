﻿using DeffinityAppDev;
using Newtonsoft.Json;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using RestSharp.Extensions;
using StreamChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using UserMgt.BAL;
using UserMgt.DAL;
using UserMgt.Entity;
using static QRCoder.PayloadGenerator.SwissQrCode;

namespace DonorCRM
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string contactType = Request.QueryString["type"];

            if (!IsPostBack)
            {

                LoadAllContacts();

             }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
          

        }

        // Helper method to determine SID based on selected categories


        private void LoadAllContacts()
        {
            var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();// //PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().ToList();
            ;

            iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).Where(o => o.SID == UserType.Donor).ToList();

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
                        ContractorName = f.UserID.HasValue ? contractorList.FirstOrDefault(c => c.ID == f.UserID.Value)?.ContractorName : "Unknown"
                    }).ToList(),
                    Payments = paymentDetails // Include detailed payment information with titles
                };

                // Add to contacts list
                contactObjects.Add(contactObject);
            }

            // Serialize contacts list to JSON array
            string jsonContacts = JsonConvert.SerializeObject(contactObjects);

            // Inject JSON array into client-side script
            var scriptTag = new LiteralControl();
            string scriptText = $@"
<script type='text/javascript'>
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
            DisplayCategoryCounts(contacts);
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
                if (isDonor || contact.SID==2) donorsCount++;
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

            foreach (var contact in contacts)
            {
                string name = contact.ContractorName;
                string email = contact.EmailAddress;
                string companyName = contact.Company;
                string phone = contact.ContactNumber;
                string contactid = contact.ID.ToString();
                string imgurl = GetImageUrl(contactid);
                html.Append(string.Format(@"
    <div style='display: flex; align-items: center;cursor:pointer;' class='text-hover-primary contact-item py-4' onclick='displayContactDetails(&quot;{2}&quot;)'>
        <div class='symbol symbol-40px symbol-circle'>
            <img alt='Pic' src='{1}'>
            <div class='symbol-badge bg-success start-100 top-100 border-4 h-15px w-15px ms-n2 mt-n2'></div>
        </div>
        
        <div style='margin-left: 30px;' class='ms-4'>
            <a class='fs-6 fw-bold text-gray-900 text-hover-primary mb-2'>{2}</a>
            <div class='fw-semibold fs-7 text-muted'>{3}</div>
        </div>
    </div>
    <div class='separator separator-dashed'></div>
", contactid, ResolveUrl(imgurl), email, name));



                // Display the HTML in a literal or another control
                ContactsListLiteral.Text = html.ToString();
            }
        }
    }
}
