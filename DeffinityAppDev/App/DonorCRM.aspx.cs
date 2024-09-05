using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using DC.Entity;
using DeffinityAppDev;
using DeffinityAppDev.App;
using DeffinityManager;
using DeffinityManager.DAL;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using PayFast.ApiTypes;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using RestSharp;
using RestSharp.Extensions;
using StreamChat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
            BindCountry();

            addDonors();
            BindCountryCodes();
            if (!IsPostBack)
            {
                LoadAllContacts();
                // Get the 'mode' query string parameter
                LoadReceipts();
                // Check if 'mode' is "edit"
                HandleViews();
            }
        }
        private void LoadReceipts()
        {
            try
            {
                // Get the `tid` query parameter from the URL
                string tid = Request.QueryString["tid"];

                if (!string.IsNullOrEmpty(tid))
                {
                    using (var db = new PortfolioDataContext())
                    {
                        // Query to get files whose FileID starts with tid and section is "doc"
                        var files = db.FileDatas
                                      .Where(f => f.FileID.StartsWith(tid) && f.Section == ImageManager.file_section_donor_doc)
                                      .ToList();

                        if (files.Any())
                        {
                            // Generate the table HTML
                            StringBuilder tableHtml = new StringBuilder();
                            tableHtml.Append("<table class='table table-striped'>");
                            tableHtml.Append("<thead><tr>");
                            tableHtml.Append("<th>File Name</th>");
                            tableHtml.Append("<th>Actions</th>");
                            tableHtml.Append("</tr></thead>");
                            tableHtml.Append("<tbody>");

                            foreach (var file in files)
                            {
                                string fileName = file.FileName ?? "Unknown";
                                string fileId = file.FileID ?? "Unknown";

                                tableHtml.Append("<tr>");
                                // Assuming tableHtml is a StringBuilder instance

                                tableHtml.AppendFormat("<td><a href='/imagehandler.ashx?id={0}&s=donordoc'>{1}</a></td>", fileId, fileName);
                                tableHtml.AppendFormat("<td><button type='button' class='btn btn-light' onclick=\"deleteFile('{0}')\"><i class='bi bi-trash'></i></button></td>", fileId);

                                tableHtml.Append("</tr>");
                            }

                            tableHtml.Append("</tbody></table>");

                            // Ensure `receiptsLiteral` is not null
                            if (receiptsLiteral != null)
                            {
                                // Set the generated HTML to the receiptsLiteral
                                receiptsLiteral.Text = tableHtml.ToString();
                            }
                            else
                            {
                                // Handle the case where `receiptsLiteral` is null
                                throw new Exception("ReceiptsLiteral is not initialized.");
                            }
                        }
                        else
                        {
                            // Handle the case where no files are found
                            if (receiptsLiteral != null)
                            {
                                receiptsLiteral.Text = "<p>No receipts found for the specified ID.</p>";
                            }
                            else
                            {
                                throw new Exception("ReceiptsLiteral is not initialized.");
                            }
                        }
                    }
                }
                else
                {
                    // Handle the case where `tid` is not provided
                    if (receiptsLiteral != null)
                    {
                        receiptsLiteral.Text = "<p>No ID provided.</p>";
                    }
                    else
                    {
                        throw new Exception("ReceiptsLiteral is not initialized.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and potential null references
                if (receiptsLiteral != null)
                {
                    receiptsLiteral.Text = $"<p>Error: {ex.Message}</p>";
                }
                else
                {
                    Response.Write($"<p>Error: {ex.Message}</p>");
                }
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


                            var userdetail = tags.UserDetails.Where(o => o.UserId == contact.ID).FirstOrDefault();
                            if (userdetail != null)
                            { //are you d
                                propertynumandstreet.Text = userdetail.Address1 ?? string.Empty;
                                addressine2.Text = userdetail.Address2 ?? string.Empty;
                                town.Text = userdetail.Town ?? string.Empty;
                                city.Text = userdetail.City ?? string.Empty;
                                postalcode.Text=userdetail.PostCode?? string.Empty;
                                Console.WriteLine(userdetail);

                                // Handle ddlCountry.SelectedValue separately
                                string countryValue = userdetail.Country?.ToString() ?? string.Empty;
                               //

                                // Check if the value exists in the dropdown list
                                if (ddlCountry.Items.FindByValue(countryValue) != null)
                                {
                                    ddlCountry.SelectedValue = countryValue;
                                    hiddenFieldCountry.Value = countryValue;
                                 
                                }
                                else
                                {
                                    // Handle cases where the countryValue does not match any item
                                }
                            }

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
                            chkMembers.Checked = roles.Contains("Sponsor");
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
                        
                        txtLastName.Text = contact.LastName;
                        txtCompanyName.Text = contact.CompanyName;
                        txtEmail.Text = contact.EmailAddress;
                        string contactNumber = contact.ContactNumber;

                        if (contactNumber.Contains('-'))
                        {
                            var parts = contactNumber.Split('-');
                            var countryCode = parts[0];

                            // Check if the value exists in the dropdown list
                            var item = ddlPhone.Items.FindByValue(countryCode);

                            if (item != null)
                            {
                                ddlPhone.SelectedValue = countryCode;
                            }
                            // Join the remaining parts (e.g., 301-11123-12) and set them to the textbox
                            txtPhone.Text = string.Join("-", parts.Skip(1));
                        }
                        else
                        {
                            txtPhone.Text = contactNumber;
                            ddlPhone.SelectedValue = "0"; // Assuming "0" is the value for no selection
                        }

                        txtDonationsRaised.Text = "£" + donationsRaised.ToString("N2");
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

                        string tableHtml = "<table id='payment-table' class='table table-striped'>";
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
                                var currentUrl = HttpContext.Current.Request.Url;
                                var queryString = HttpUtility.ParseQueryString(currentUrl.Query);
                                queryString["tid"] = payment.TID.ToString();

                                var redirectUrl = $"{currentUrl.AbsolutePath}?{queryString}";
                                tableHtml += "<tr>";
                                tableHtml += "<td>" + (string.IsNullOrEmpty(payment.FundraiserNames) ? "None" : payment.FundraiserNames) + "</td>";
                                tableHtml += "<td style='padding-left:40px'>" + (payment.Amount.HasValue ? "£" + payment.Amount.Value.ToString("N2") : "None") + "</td>";
                                tableHtml += "<td>" + (payment.Status == "Successful" ? "<span class='badge badge-light-success'>Successful</span>" : (payment.Status == "Failed" ? "<span class='badge badge-light-danger'>Failed</span>" : "None")) + "</td>";
                                tableHtml += "<td>" + (payment.PaidDate.HasValue ? payment.PaidDate.Value.ToString("dd/MM/yyyy") : "None") + "</td>";
                                tableHtml += "<td><a class='btn btn-light' style='border:1px solid #aca5a5' href='" + redirectUrl + "')\">Details</a><button type='button' style='border:1px solid #aca5a5;display:none;' class='btn btn-light' onclick=\"handlePaymentDetails('" + payment.TID + "', '" + payment.Name + "', '" + payment.Email + "', " + payment.Amount + ", '" + payment.FundraiserNames + "', " + payment.PlatformFee + ", '" + payment.PaymentType + "', '" + payment.Status + "')\">Details</button></td>";
                                tableHtml += "</tr>";
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

        private void addDonors()
        {
            using(var dbcontext=new UserDataContext()) { 
                var iList=dbcontext.Contractors.ToList();
            foreach (var contractor in iList)
            {
                using (PortfolioMgt.DAL.PortfolioDataContext db = new PortfolioDataContext())
                {
                    var payments = db.TithingPaymentTrackers
                                 .Where(p => p.DonerEmail == contractor.EmailAddress)
                                 .ToList();
                    if (payments.Any())
                    {
                        bool roleExists = db.tblRoles.Any(r => r.ContractorID == contractor.ID && r.RoleType == "Donor");
                        if (!roleExists)
                        {
                            // Add new role
                            var newRole = new tblRole
                            {
                                ContractorID = contractor.ID,
                                RoleType = "Donor"
                                // Add other properties as needed
                            };
                            db.tblRoles.InsertOnSubmit(newRole);
                            db.SubmitChanges();
                        }

                    }
                }
            }
            }
        }
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
                    postalcode=contact.PostCode,
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

            using(var context=new UserDataContext())
            {


            foreach (var contact in contacts)
            {
                    var tagsarray = context.UserSkills.Where(o => o.UserId == contact.ID).ToList();

                    // Combine all interests and skills into a comma-separated string
                    string interests = string.Join(",", tagsarray.Select(t => t.Notes).Where(note => !string.IsNullOrEmpty(note)));
                    string skills = string.Join(",", tagsarray.Select(t => t.Skills).Where(skill => !string.IsNullOrEmpty(skill)));

                    // Combine both interests and skills into a single string, separated by commas
                    string combinedTags = string.Join(",", new[] { interests, skills }.Where(s => !string.IsNullOrEmpty(s)));

                    // make a string that consists all the skills and interests separated by commas

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
            <span id='tagsforsearch' style='display:none'>{5}</span>
        </div>
    </div></a>
    <div class='separator separator-dashed'></div>
", contactid, ResolveUrl(imgurl), email, name, sidQueryParam,combinedTags));



                // Display the HTML in a literal or another control
                ContactsListLiteral.Text = html.ToString();
            }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string idParam = Request.QueryString["id"];
                if (idParam != null)
                {
                    int ID = int.Parse(idParam);
                    var contact = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany()
                       .Where(o => o.ID == ID)
                       // Adjust based on your needs
                       .FirstOrDefault();

                    if (contact != null)
                    {
                        using (var userContext = new UserDataContext())
                        {
                            var contact1 = userContext.Contractors.FirstOrDefault(o => o.ID == ID);
                            // Update the contractor's properties with the new values from the form
                            contact1.SID = 2;
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
                            contact1.ContactNumber = ddlPhone.SelectedValue+"-"+txtPhone.Text.Trim();


                            var userdetail = userContext.UserDetails.Where(o => o.UserId == ID).FirstOrDefault();
                            if (userdetail != null)
                            {
                                userdetail.Country = Convert.ToInt32(hiddenFieldCountry.Value); 
                                userdetail.Address1 = propertynumandstreet.Text;
                                userdetail.Address2 = addressine2.Text;
                                userdetail.Town = town.Text;
                                userdetail.City = city.Text;
                                userdetail.PostCode = postalcode.Text;

                            }









                            userContext.SubmitChanges();


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
                                        RoleType = "Sponsor"
                                        // Add other properties as needed
                                    };
                                    context.tblRoles.InsertOnSubmit(newRole);

                                }
                            }
                            if (chkDonors.Checked)
                            {
                                bool roleExists = context.tblRoles.Any(r => r.ContractorID == ID && r.RoleType == "Donor");
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
                            using (UserMgt.DAL.UserDataContext tags = new UserMgt.DAL.UserDataContext())
                            {
                                // Check if the UserSkill record exists
                                var Usertag = tags.UserSkills.FirstOrDefault(i => i.UserId == contact.ID);

                                if (Usertag != null)
                                {
                                    // Update existing UserSkill record
                                    Usertag.Skills = txttags.Text;
                                    Usertag.Notes = txtinterest.Text;
                                }
                                else
                                {
                                    // Create a new UserSkill record
                                    Usertag = new UserSkill
                                    {
                                        UserId = contact.ID,
                                        Skills = txttags.Text,
                                        Notes = txtinterest.Text
                                    };

                                    // Insert the new record into the UserSkills table
                                    tags.UserSkills.InsertOnSubmit(Usertag);
                                }

                                // Save the changes to the database
                                tags.SubmitChanges();
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

                        // Get the file extension
                        string fileExtension = System.IO.Path.GetExtension(AvatarUpload.FileName).ToLower();
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
                            SID = 2,
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
                            ContactNumber = ddlPhone.SelectedValue +"-"+ txtPhone.Text.Trim(),


                        };


                        // Check if the email already exists
                        var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany()
           .Where(o => o.CompanyID == sessionKeys.PortfolioID && o.SID == UserType.Donor && o.EmailAddress == newContractor.EmailAddress)

           .FirstOrDefault();

                        if (iList == null)
                        {
                            // Add new contractor to the context
                            userContext.Contractors.InsertOnSubmit(newContractor); // Save changes to the database
                            userContext.SubmitChanges();
                            contractorID = newContractor.ID;


                            int countryValue;
                            
                            bool isCountryValid = int.TryParse(hiddenFieldCountry.Value, out countryValue);
                            UserDetail userDetail;

                            if (countryValue!=0)
                            { 
                             userDetail = new UserDetail
                            {
                                UserId = contractorID,
                                Country = isCountryValid ? countryValue : 0, // Use 0 or another default value if parsing fails
                                Address1 = propertynumandstreet.Text,
                                Address2 = addressine2.Text,
                                Town = town.Text,
                                City = city.Text,
                                PostCode = postalcode.Text
                            };
                            }
                            else
                            {
                                 userDetail = new UserDetail
                                {
                                    UserId = contractorID,
                                     // Use 0 or another default value if parsing fails
                                    Address1 = propertynumandstreet.Text,
                                    Address2 = addressine2.Text,
                                    Town = town.Text,
                                    City = city.Text,
                                    PostCode = postalcode.Text
                                };
                            }

                            userContext.UserDetails.InsertOnSubmit(userDetail);
                            userContext.SubmitChanges();

                            var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                            var urEntity = new UserMgt.Entity.UserToCompany();
                            urEntity.CompanyID = sessionKeys.PortfolioID;
                            urEntity.UserID = contractorID;
                            urRep.Add(urEntity);
                            ddlCountry.SelectedValue = userDetail.Country.ToString();
                            hiddenFieldCountry.Value= userDetail.Country.ToString();

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
                                    RoleType = "Sponsor"
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

                    using (UserMgt.DAL.UserDataContext tags = new UserMgt.DAL.UserDataContext())
                    {
                        // Check if the UserSkill record exists
                        var Usertag = tags.UserSkills.FirstOrDefault(i => i.UserId == contractorID);

                        if (Usertag != null)
                        {
                            // Update existing UserSkill record
                            Usertag.Skills = txttags.Text;
                            Usertag.Notes = txtinterest.Text;
                        }
                        else
                        {
                            // Create a new UserSkill record
                            Usertag = new UserSkill
                            {
                                UserId = contractorID,
                                Skills = txttags.Text,
                                Notes = txtinterest.Text
                            };

                            // Insert the new record into the UserSkills table
                            tags.UserSkills.InsertOnSubmit(Usertag);
                        }

                        // Save the changes to the database
                        tags.SubmitChanges();
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
                        string fileExtension = System.IO.Path.GetExtension(AvatarUpload.FileName).ToLower();
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


                HandleViews();
            }
            catch (Exception ex) {
                LogExceptions.WriteExceptionLog(ex);
                HandleViews();


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

            // Redirect to the same page with tid query parameter
            var currentUrl = HttpContext.Current.Request.Url;
            var queryString = HttpUtility.ParseQueryString(currentUrl.Query);
            

            var redirectUrl = $"{currentUrl.AbsolutePath}?{queryString}";
            Response.Redirect(redirectUrl, false);
        }
        public static void ShowErrorAlertWithHref(System.Web.UI.Page page, string title_msg, string buttonname, string redirectUrl)
        {
            string script = $@"
    window.onload = function() {{
        Swal.fire({{
            title: '{title_msg}',
            icon: 'error',
            showCancelButton: false,
            confirmButtonText: '{buttonname}',
            customClass: {{
                confirmButton: 'swal2-confirm btn btn-primary'
            }}
        }}).then((result) => {{
            if (result.isConfirmed) {{
                window.open('{redirectUrl}', '_blank');
            }}
        }});
    }};
";


            page.ClientScript.RegisterStartupScript(page.GetType(), "sweetalerterrorWithHref", script, true);
        }

        protected void syncwithmailchimp_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new PortfolioDataContext())
                {
                    // Retrieve Mailchimp API Key from the database
                    var settings = db.MailchimpSettings.FirstOrDefault(o => o.UserId == sessionKeys.UID);
                    if (settings == null || string.IsNullOrEmpty(settings.ApiKey))
                    {
                        ShowErrorAlertWithHref(this.Page, "Please Add Mailchimp Settings", "Take me to Settings", "mailchimp.aspx");
                        // Handle case where API Key is not found
                        return;
                    }

                    string apiKey = settings.ApiKey;
                    string serverPrefix = apiKey.Split('-')[1];
                    // Your Mailchimp server prefix
                    string audienceId =settings.TeamId; // Your Audience ID

                    // Fetch users from the system
                    var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany()
                        .Where(o => o.CompanyID == sessionKeys.PortfolioID && o.SID == UserType.Donor)
                        .OrderBy(o => o.ContractorName)
                        .ToList();

                    bool Iserror=false;
                    // Sync each user with Mailchimp
                    foreach (var user in iList)
                    {
                        bool userExists = CheckIfUserExistsInMailchimp(apiKey, serverPrefix, audienceId, user.EmailAddress);

                        if (!userExists)
                        {
                            string message=AddUserToMailchimp(apiKey, serverPrefix, audienceId, user);
                            if (!String.IsNullOrEmpty(message)) {
                                DeffinityManager.ShowMessages.ShowErrorAlert(this.Page,message,"OK");
                                Iserror = true;
                                break;
                            }
                        }
                    }
                    SyncMailchimpToApp(apiKey, serverPrefix, audienceId);
                    LoadAllContacts();
                    if (!Iserror) { 
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Plegit Has Successfully Synchronised Your Contacts With MailChimp", "OK");
                    }
                }
            }
            catch (Exception ex) {
                LogExceptions.WriteExceptionLog(ex);

            }
        }
      

            private void BindCountryCodes()
            {

                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Afghanistan (+93)", "+93"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Albania (+355)", "+355"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Algeria (+213)", "+213"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("American Samoa (+1684)", "+1684"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Andorra (+376)", "+376"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Angola (+244)", "+244"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Anguilla (+1264)", "+1264"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Antarctica (+672)", "+672"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Antigua and Barbuda (+1268)", "+1268"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Argentina (+54)", "+54"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Armenia (+374)", "+374"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Aruba (+297)", "+297"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Australia (+61)", "+61"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Austria (+43)", "+43"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Azerbaijan (+994)", "+994"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bahamas (+1242)", "+1242"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bahrain (+973)", "+973"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bangladesh (+880)", "+880"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Barbados (+1246)", "+1246"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Belarus (+375)", "+375"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Belgium (+32)", "+32"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Belize (+501)", "+501"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Benin (+229)", "+229"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bermuda (+1441)", "+1441"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bhutan (+975)", "+975"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bolivia (+591)", "+591"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bosnia and Herzegovina (+387)", "+387"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Botswana (+267)", "+267"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Brazil (+55)", "+55"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("British Indian Ocean Territory (+246)", "+246"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Brunei (+673)", "+673"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bulgaria (+359)", "+359"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Burkina Faso (+226)", "+226"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Burundi (+257)", "+257"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cambodia (+855)", "+855"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cameroon (+237)", "+237"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Canada (+1)", "+1"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cape Verde (+238)", "+238"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cayman Islands (+1345)", "+1345"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Central African Republic (+236)", "+236"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Chad (+235)", "+235"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Chile (+56)", "+56"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("China (+86)", "+86"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Christmas Island (+61)", "+61"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cocos (Keeling) Islands (+61)", "+61"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Colombia (+57)", "+57"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Comoros (+269)", "+269"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Congo (DRC) (+243)", "+243"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Congo (Republic) (+242)", "+242"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cook Islands (+682)", "+682"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Costa Rica (+506)", "+506"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Côte d’Ivoire (+225)", "+225"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Croatia (+385)", "+385"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cuba (+53)", "+53"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Curaçao (+5999)", "+5999"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cyprus (+357)", "+357"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Czech Republic (+420)", "+420"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Denmark (+45)", "+45"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Djibouti (+253)", "+253"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Dominica (+1767)", "+1767"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Dominican Republic (+1809, +1829, +1849)", "+1809"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ecuador (+593)", "+593"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Egypt (+20)", "+20"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("El Salvador (+503)", "+503"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Equatorial Guinea (+240)", "+240"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Eritrea (+291)", "+291"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Estonia (+372)", "+372"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Eswatini (+268)", "+268"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ethiopia (+251)", "+251"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Falkland Islands (+500)", "+500"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Faroe Islands (+298)", "+298"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Fiji (+679)", "+679"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Finland (+358)", "+358"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("France (+33)", "+33"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("French Guiana (+594)", "+594"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("French Polynesia (+689)", "+689"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Gabon (+241)", "+241"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Gambia (+220)", "+220"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Georgia (+995)", "+995"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Germany (+49)", "+49"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ghana (+233)", "+233"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Gibraltar (+350)", "+350"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Greece (+30)", "+30"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Greenland (+299)", "+299"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Grenada (+1473)", "+1473"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guadeloupe (+590)", "+590"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guam (+1671)", "+1671"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guatemala (+502)", "+502"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guernsey (+44)", "+44"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guinea (+224)", "+224"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guinea-Bissau (+245)", "+245"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guyana (+592)", "+592"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Haiti (+509)", "+509"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Honduras (+504)", "+504"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Hong Kong (+852)", "+852"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Hungary (+36)", "+36"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Iceland (+354)", "+354"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("India (+91)", "+91"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Indonesia (+62)", "+62"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Iran (+98)", "+98"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Iraq (+964)", "+964"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ireland (+353)", "+353"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Isle of Man (+44)", "+44"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Israel (+972)", "+972"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Italy (+39)", "+39"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Jamaica (+1876)", "+1876"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Japan (+81)", "+81"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Jersey (+44)", "+44"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Jordan (+962)", "+962"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kazakhstan (+7)", "+7"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kenya (+254)", "+254"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kiribati (+686)", "+686"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("North Korea (+850)", "+850"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("South Korea (+82)", "+82"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kosovo (+383)", "+383"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kuwait (+965)", "+965"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kyrgyzstan (+996)", "+996"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Laos (+856)", "+856"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Latvia (+371)", "+371"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Lebanon (+961)", "+961"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Lesotho (+266)", "+266"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Liberia (+231)", "+231"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Libya (+218)", "+218"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Liechtenstein (+423)", "+423"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Lithuania (+370)", "+370"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Luxembourg (+352)", "+352"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Macau (+853)", "+853"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Madagascar (+261)", "+261"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Malawi (+265)", "+265"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Malaysia (+60)", "+60"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Maldives (+960)", "+960"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mali (+223)", "+223"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Malta (+356)", "+356"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Marshall Islands (+692)", "+692"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Martinique (+596)", "+596"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mauritania (+222)", "+222"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mauritius (+230)", "+230"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mexico (+52)", "+52"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Micronesia (+691)", "+691"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Moldova (+373)", "+373"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Monaco (+377)", "+377"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mongolia (+976)", "+976"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Montenegro (+382)", "+382"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Montserrat (+1664)", "+1664"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Morocco (+212)", "+212"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mozambique (+258)", "+258"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Myanmar (+95)", "+95"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Namibia (+264)", "+264"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Nauru (+674)", "+674"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Nepal (+977)", "+977"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Netherlands (+31)", "+31"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("New Caledonia (+687)", "+687"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("New Zealand (+64)", "+64"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Nicaragua (+505)", "+505"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Niger (+227)", "+227"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Nigeria (+234)", "+234"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Niue (+683)", "+683"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Norfolk Island (+672)", "+672"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Northern Mariana Islands (+1670)", "+1670"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Norway (+47)", "+47"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Oman (+968)", "+968"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Pakistan (+92)", "+92"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Palau (+680)", "+680"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Palestine (+970)", "+970"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Panama (+507)", "+507"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Papua New Guinea (+675)", "+675"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Paraguay (+595)", "+595"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Peru (+51)", "+51"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Philippines (+63)", "+63"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Pitcairn Islands (+64)", "+64"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Poland (+48)", "+48"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Portugal (+351)", "+351"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Puerto Rico (+1)", "+1"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Qatar (+974)", "+974"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Réunion (+262)", "+262"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Romania (+40)", "+40"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Russia (+7)", "+7"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Rwanda (+250)", "+250"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Barthélemy (+590)", "+590"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Helena (+290)", "+290"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Kitts and Nevis (+1869)", "+1869"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Lucia (+1758)", "+1758"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Martin (+590)", "+590"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Pierre and Miquelon (+508)", "+508"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Vincent and the Grenadines (+1784)", "+1784"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Samoa (+685)", "+685"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("San Marino (+378)", "+378"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("São Tomé and Príncipe (+239)", "+239"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saudi Arabia (+966)", "+966"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Senegal (+221)", "+221"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Serbia (+381)", "+381"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Seychelles (+248)", "+248"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sierra Leone (+232)", "+232"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Singapore (+65)", "+65"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sint Maarten (+1721)", "+1721"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Slovakia (+421)", "+421"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Slovenia (+386)", "+386"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Solomon Islands (+677)", "+677"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Somalia (+252)", "+252"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("South Africa (+27)", "+27"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("South Sudan (+211)", "+211"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Spain (+34)", "+34"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sri Lanka (+94)", "+94"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sudan (+249)", "+249"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Suriname (+597)", "+597"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Svalbard and Jan Mayen (+47)", "+47"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sweden (+46)", "+46"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Switzerland (+41)", "+41"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Syria (+963)", "+963"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Taiwan (+886)", "+886"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tajikistan (+992)", "+992"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tanzania (+255)", "+255"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Thailand (+66)", "+66"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Timor-Leste (+670)", "+670"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Togo (+228)", "+228"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tokelau (+690)", "+690"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tonga (+676)", "+676"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Trinidad and Tobago (+1868)", "+1868"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tunisia (+216)", "+216"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Turkey (+90)", "+90"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Turkmenistan (+993)", "+993"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Turks and Caicos Islands (+1649)", "+1649"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tuvalu (+688)", "+688"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Uganda (+256)", "+256"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ukraine (+380)", "+380"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("United Arab Emirates (+971)", "+971"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("United Kingdom (+44)", "+44"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("United States (+1)", "+1"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Uruguay (+598)", "+598"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Uzbekistan (+998)", "+998"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Vanuatu (+678)", "+678"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Vatican City (+39)", "+39"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Venezuela (+58)", "+58"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Vietnam (+84)", "+84"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Yemen (+967)", "+967"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Zambia (+260)", "+260"));
                ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Zimbabwe (+263)", "+263"));


                // Optionally add a default item
                ddlPhone.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Country Code--", "0"));
        }

        private void BindCountry()
        {
            LocationRepository<Location.Entity.CountryClass> lcRep = new LocationRepository<Location.Entity.CountryClass>();
            var lc = lcRep.GetAll().Where(o => o.Visible == 'Y').OrderBy(o => o.Country1).ToList();
            if (lc.Count > 0)
            {
                ddlCountry.DataSource = lc;
                ddlCountry.DataTextField = "Country1";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();
            }
            ddlCountry.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select...", "0"));

         

            
        }
        private void SyncMailchimpToApp(string apiKey, string serverPrefix, string audienceId)
        {
            try
            {
                using(var pdb=new PortfolioDataContext())
                using (var db = new UserDataContext())
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    // Ensure TLS 1.2 is used
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    var client = new RestClient($"https://{serverPrefix}.api.mailchimp.com/3.0/lists/{audienceId}/members");
                    var request = new RestRequest(Method.GET);

                    // Basic Auth: Mailchimp uses the API key as the password, and the username can be any string
                    var authValue = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"anystring:{apiKey}"));
                    request.AddHeader("Authorization", $"Basic {authValue}");
                    request.AddHeader("Content-Type", "application/json");

                    IRestResponse response = client.Execute(request);

                    if (response.IsSuccessful)
                    {

                        // Parse the Mailchimp members list
                        var mailchimpMembers = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response.Content);
                        foreach (var member in mailchimpMembers.members)
                        {
                            string email = member.email_address.ToString();
                            string firstName = member.merge_fields.FNAME.ToString();
                            string lastName = member.merge_fields.LNAME.ToString();
                            string address = "";
                            /* var addressField = member.merge_fields.ADDRESS as Newtonsoft.Json.Linq.JObject;

                             if (addressField != null && addressField["addr1"] != null)
                             {
                                 address = addressField["addr1"].ToString();
                             }
     */
                            // Check if the user exists in the application database
                            var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany()
       .Where(o => o.CompanyID == sessionKeys.PortfolioID && o.SID == UserType.Donor && o.EmailAddress==email)
       
       .FirstOrDefault();
                            if (iList == null)
                            {
                                // Add the Mailchimp user to the application database
                                var newUser = new UserMgt.Entity.Contractor
                                {
                                    EmailAddress = email,
                                    Status = "Active",
                                    ContractorName = firstName,
                                    LastName = lastName,
                                    SID = 2,
                                    ResetPassword=false,
                                    
                                    CreatedDate=DateTime.Now,
                                    ModifiedDate=DateTime.Now,
                                    Password = Deffinity.Users.Login.GeneratePasswordString("Ems@1234"),
                                    // Set other necessary fields
                                    
                                    LoginName = email,
                                  
                                    // Assuming 2 is for Donor
                                   
                                    isFirstlogin = 0,
                                
                                    Company = "",
                                    ContactNumber = "",
                                };
                                db.Contractors.InsertOnSubmit(newUser);
                                db.SubmitChanges();
                            
                                // After submitting changes, the newUser will have its UserId populated
                                int newUserId = newUser.ID; // Assuming ID is the primary key column in the Contractors table

                                // Now, insert the address into the UserDetails table
                                var userDetails = new UserMgt.Entity.UserDetail
                                {
                                    UserId = newUserId,
                                    Address1 = address,
                                    // Set other necessary fields for UserDetails
                                };
                                var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                                var urEntity = new UserMgt.Entity.UserToCompany();
                                urEntity.CompanyID = sessionKeys.PortfolioID;
                                urEntity.UserID = newUserId;
                                urRep.Add(urEntity);
                                db.UserDetails.InsertOnSubmit(userDetails);
                                db.SubmitChanges(); // Save changes to the UserDetails table

                                var newRole = new tblRole
                                {
                                    ContractorID = newUserId,
                                    RoleType = "Donor"
                                    // Add other properties as needed
                                };
                                pdb.tblRoles.InsertOnSubmit(newRole);
                                pdb.SubmitChanges();
                            }
                        }
                    }

                    else
                    {
                        // Handle error
                        Response.Write($"<br />Failed to retrieve users from Mailchimp. Status Code: {response.StatusCode}");
                        Response.Write($"<br />Response Content: {response.Content}");
                        if (response.ErrorException != null)
                        {
                            Response.Write($"<br />Error Exception: {response.ErrorException.Message}");
                        }
                    }
                }
            }
            catch (Exception e) {
                LogExceptions.WriteExceptionLog(e);

            }
        }

        private string AddUserToMailchimp(string apiKey, string serverPrefix, string audienceId, UserMgt.Entity.v_contractor user)
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                // Ensure TLS 1.2 is used
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                var client = new RestClient($"https://{serverPrefix}.api.mailchimp.com/3.0/lists/{audienceId}/members/");
                var request = new RestRequest(Method.POST);

                // Basic Auth: Mailchimp uses the API key as the password, and the username can be any string
                var authValue = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"anystring:{apiKey}"));
                request.AddHeader("Authorization", $"Basic {authValue}");
                request.AddHeader("Content-Type", "application/json"); // Ensure content type is set to JSON

                var requestBody = new
                {
                    email_address = user.EmailAddress,
                    status = "subscribed", // or "pending" if you want them to confirm
                    merge_fields = new
                    {
                        FNAME = string.IsNullOrWhiteSpace(user.ContractorName) ? "" : user.ContractorName,
                        LNAME = string.IsNullOrWhiteSpace(user.LastName) ? "" : user.LastName,
                        ADDRESS = string.IsNullOrWhiteSpace(user.Address1) ? "" : user.Address1

                    }
                };

                request.AddJsonBody(requestBody);
                IRestResponse response = client.Execute(request);

                // Check the response and log or handle success and errors
                if (response.IsSuccessful)
                {
                    Response.Write($"<br />Successfully added {user.EmailAddress} to Mailchimp.");
                    return "";
                }
                else
                {
                    // Log the response content for debugging
                    Response.Write($"<br />Failed to add {user.EmailAddress} to Mailchimp.");
                    return "Please check the email addresses in your CRM as some may be incorrectly formatted.";
                
                    if (response.ErrorException != null)
                    {
                        Response.Write($"<br />Error Exception: {response.ErrorException.Message}");
                    }
                }
            }
            catch (Exception ex) {
                LogExceptions.WriteExceptionLog(ex);
                return "";

            }
        }

        private bool CheckIfUserExistsInMailchimp(string apiKey, string serverPrefix, string audienceId, string email)
        {
            try { 
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            // Ensure TLS 1.2 is used
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            // Create a REST client with the Mailchimp API endpoint
            var client = new RestClient($"https://{serverPrefix}.api.mailchimp.com/3.0/lists/{audienceId}/members/{GetSubscriberHash(email)}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {apiKey}");

            // Execute the request and get the response
            IRestResponse response = client.Execute(request);

            // Check if the response indicates the user exists
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true; // User exists in Mailchimp
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false; // User does not exist in Mailchimp
            }
            else
            {
                // Handle other possible response statuses (like errors)
                // Log the error or handle it according to your needs
                return false;
            }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return false;

            }
        }

        private string GetSubscriberHash(string email)
        {
            try
            {
                // Mailchimp API requires a hashed version of the email address
                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(email.ToLower());
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    // Convert byte array to a string (hexadecimal format)
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex) {
                LogExceptions.WriteExceptionLog(ex);
                return null;
            }
        }


    }
}

