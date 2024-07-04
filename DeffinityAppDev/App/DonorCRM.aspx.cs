using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using UserMgt.BAL;

namespace DonorCRM
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string contactType = Request.QueryString["type"];

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(contactType))
                {
                    LoadContactsByType(contactType);
                }
                else
                {
                    LoadAllContacts();
                }
                DisplayCategoryCounts();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Value;
            string companyName = txtCompanyName.Value;
            string email = txtEmail.Value;
            string phone = txtPhone.Value;
            string city = txtCity.Value;
            string notes = txtNotes.Value;
            Response.Clear();
            Response.Redirect(name);
            /*
                        // Build categories list
                        List<string> selectedCategories = new List<string>();
                        if (chkDonors.Checked) selectedCategories.Add("Donors");
                        if (chkVolunteers.Checked) selectedCategories.Add("Volunteers");
                        if (chkLeads.Checked) selectedCategories.Add("Leads");
                        if (chkSponsors.Checked) selectedCategories.Add("Sponsors");

                        // Create a new contractor object
                        var newContractor = new UserMgt.Entity.Contractor
                        {
                            ContractorName = name,
                            Company = companyName,
                            EmailAddress = email,
                            ContactNumber = phone,
                            SID = GetSIDFromCategories(selectedCategories), // Assume SID is set based on selected categories
                            Status = UserStatus.Active, // Assuming default status is Active
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            isFirstlogin = 0,
                            ResetPassword = false
                        };

                        // Save the new contractor
                        int newContractorId = UserMgt.BAL.ContractorsBAL.InsertContractor(newContractor);

                        if (newContractorId > 0)
                        {
                            // Create user detail for the new contractor
                            var userDetail = new UserMgt.Entity.UserDetail
                            {
                                UserId = newContractorId,
                                City = city,
                                Notes = notes
                            };

                            // Save user detail
                            UserMgt.BAL.ContractorsBAL.InsertUserDetail(userDetail);

                            // Redirect to Index page with default behavior (show all contacts)
                            Response.Redirect("Index.aspx");
                        }
                        else
                        {
                            // Handle error if contractor insertion fails
                            // You can add error handling logic here
                            // For example, show an error message to the user
                        }*/
        }

        // Helper method to determine SID based on selected categories
        private int GetSIDFromCategories(List<string> selectedCategories)
        {
            // Implement your logic here to determine SID based on selected categories
            // For example, return UserType.Donor if "Donors" category is selected
            // You should customize this based on your application's business logic
            // Defaulting to 2 for demonstration purposes, assuming it corresponds to a type like "Donor"
            return 2;
        }

        private void LoadAllContacts()
        {
            var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.SID == UserType.Donor).ToList();
            ;


            LoadContacts(iList);
        }

        private void LoadContactsByType(string contactType)
        {
            int sidValue = GetSIDValue(contactType);

            var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany()
              /*  .Where(o => o.CompanyID == sessionKeys.PortfolioID)*/
                .Where(o => o.SID == sidValue)
                .ToList();

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

            foreach (var contact in contacts)
            {
                // Construct JSON object including categories
                var contactObject = new
                {
                    Name = contact.ContractorName,
                    Email = contact.EmailAddress,
                    AvatarUrl = "assets/media/avatars/300-6.jpg",
                    CompanyName = contact.Company,
                    Phone = contact.ContactNumber,
                  //  City = contact.City,
                    Country = contact.Country,
                   // Notes = contact.Notes,
                    Categories = contact.SID // Assuming there's a Categories property
                };

                // Add to contacts list
                contactObjects.Add(contactObject);
            }

            // Serialize contacts list to JSON array
            string jsonContacts = JsonConvert.SerializeObject(contactObjects);

            // Inject JSON array into client-side script
            var scriptTag = new LiteralControl();
            scriptTag.Text = $@"
                <script type='text/javascript'>
                    var contacts = {jsonContacts};
                    console.log('Loaded {contactObjects.Count} contacts from server.');
                    // You can now use 'contacts' array in your JavaScript code
                </script>
            ";

            // Register script to be executed when page renders
            Page.Header.Controls.Add(scriptTag);

            // Display contacts in HTML (optional, if needed)
            DisplayContactsInHTML(contacts);
        }

        private void DisplayCategoryCounts()
        {
            var allContacts = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany()
                .Where(o => o.CompanyID == sessionKeys.PortfolioID)
                .ToList();

            int allCount = allContacts.Count;
            int donorsCount = allContacts.Count(o => o.SID == 2); // Donors
            int volunteersCount = allContacts.Count(o => o.SID == 4); // Volunteers
            int leadsCount = allContacts.Count(o => o.SID == 3); // Leads
            int sponsorsCount = allContacts.Count(o => o.SID == 1); // Sponsors (assuming based on SID)

            // Inject counts into client-side script or display in HTML
            var scriptTag = new LiteralControl();
            scriptTag.Text = $@"
                <script type='text/javascript'>
                    var allCount = {allCount};
                    var donorsCount = {donorsCount};
                    var volunteersCount = {volunteersCount};
                    var leadsCount = {leadsCount};
                    var sponsorsCount = {sponsorsCount};
                    console.log('Counts: Donors=' + donorsCount + ', Volunteers=' + volunteersCount + ', Leads=' + leadsCount + ', Sponsors=' + sponsorsCount);
                </script>";

            // Register script to be executed when page renders
            Page.Header.Controls.Add(scriptTag);
        }

        private void DisplayContactsInHTML(List<UserMgt.Entity.v_contractor> contacts)
        {
            StringBuilder html = new StringBuilder();

            foreach (var contact in contacts)
            {
                string name = contact.ContractorName;
                string email = contact.EmailAddress;
                string avatarUrl = "assets/media/avatars/300-6.jpg";
                string companyName = contact.Company;
                string phone = contact.ContactNumber;
                string city = "Islamabad";
                string country = "Pakistan";
                string notes = " ";

                html.Append(string.Format(@"
                    <div class='d-flex flex-stack py-4' onclick='displayContactDetails(""{0}"")'>
                        <div class='d-flex align-items-center'>
                            <div class='symbol symbol-40px symbol-circle'>
                                <img alt='Pic' src='{1}'>
                                <div class='symbol-badge bg-success start-100 top-100 border-4 h-15px w-15px ms-n2 mt-n2'></div>
                            </div>
                            <div class='ms-4'>
                                <a href='apps/contacts/view-contact.html' class='fs-6 fw-bold text-gray-900 text-hover-primary mb-2'>{2}</a>
                                <div class='fw-semibold fs-7 text-muted'>{0}</div>
                            </div>
                        </div>
                    </div>
                    <div class='separator separator-dashed d-none'></div>
                ", email, avatarUrl, name));
            }

            // Display the HTML in a literal or another control
            ContactsListLiteral.Text = html.ToString();
        }
    }
}
