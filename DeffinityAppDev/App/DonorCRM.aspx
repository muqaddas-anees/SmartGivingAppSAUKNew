<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="DonorCRM.aspx.cs" Inherits="DonorCRM.WebForm1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <asp:Label ID="lblPageTitle" runat="server" Text="Donors"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
   <style>
        .tag {
            display: inline-block;
            background-color: #f1f1f1;
            color: #333;
            padding: 3px 8px;
            border-radius: 15px;
            margin: 5px;
            font-size: 10px;
        }
        .tags-container {
        width: 100%;
        max-width: 60%;
        overflow-x: auto; /* Allow horizontal scrolling if necessary */
        white-space: nowrap; /* Prevent line breaks within tags */
        display: inline-block; /* Keep tags in a single line */
    }

    .tagify__tag {
        display: inline-block; /* Ensure tags are displayed inline */
        margin: 2px; /* Add some space between tags */
    }
       .form-check-input:disabled~.form-check-label, .form-check-input[disabled]~.form-check-label{
           opacity:1;
           color:#252F4A
       }
       .form-check-input[disabled]{
           opacity:1;
       }
		.contact-item {
    transition: box-shadow 0.3s ease; /* Smooth transition for the shadow */
}

.contact-item:hover {
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); /* Shadow effect on hover */
}
	</style>
    <div id="scripts" runat="server"></div>
        <div id="scripts2" runat="server"></div>

    <div class="d-flex">
   
    <div class="d-flex flex-column flex-column-fluid">
        <!--begin::Toolbar-->
        <div id="kt_app_toolbar" class="app-toolbar pt-6 pb-2">
            <!--begin::Toolbar container-->
            <div id="kt_app_toolbar_container" class="app-container container-fluid d-flex align-items-stretch">
                <!--begin::Toolbar wrapper-->
                <div class="app-toolbar-wrapper d-flex flex-stack flex-wrap gap-4 w-100">
                    <!--begin::Page title-->
                    <div class="page-title d-flex flex-column justify-content-center gap-1 me-3">
                        <!--begin::Title-->
                        <h1 class="page-heading d-flex flex-column justify-content-center text-gray-900 fw-bold fs-3 m-0">Donor CRM</h1>
                        <!--end::Title-->
                        <!--begin::Breadcrumb-->
                        <ul class="breadcrumb breadcrumb-separatorless fw-semibold fs-7 my-0">
                            <!--begin::Item-->
                            <li class="breadcrumb-item text-muted">
                                <a href="/app/dashboard.aspx" class="text-muted text-hover-primary">Home</a>
                            </li>
                            <!--end::Item-->
                            <!--begin::Item-->
                            <li class="breadcrumb-item">
                                <span class="bullet bg-gray-500 w-5px h-2px"></span>
                            </li>
                            <!--end::Item-->
                            <!--begin::Item-->
                            <li class="breadcrumb-item text-muted">Donor CRM</li>
                            <!--end::Item-->
                            <!--begin::Item-->
                            <li class="breadcrumb-item">
                                <span class="bullet bg-gray-500 w-5px h-2px"></span>
                            </li>
                            <!--end::Item-->
                            <!--begin::Item-->
                          
                            <!--end::Item-->
                        </ul>
                        <!--end::Breadcrumb-->
                    </div>
                    <!--end::Page title-->
                    <!--begin::Actions-->
                    
                    <!--end::Actions-->
                </div>
                <!--end::Toolbar wrapper-->
            </div>
            <!--end::Toolbar container-->
        </div>
        <!--end::Toolbar-->
        <!--begin::Content-->
        <div id="kt_app_content" class="app-content flex-column-fluid">
            <!--begin::Content container-->
            <div id="kt_app_content_container" class="app-container container-fluid">
                <!--begin::Contacts App- Add New Contact-->
                <div class="row g-7">
                    <!--begin::Contact groups-->
                    <div class="col-lg-6 col-xl-3">
                        <!--begin::Contact group wrapper-->
                        <div class="card card-flush">
                            <!--begin::Card header-->
                            <div class="card-header pt-7" id="kt_chat_contacts_header">
                                <!--begin::Card title-->
                                <div class="card-title">
                                    <h2>Groups</h2>
                                </div>
                                <!--end::Card title-->
                            </div>
                            <!--end::Card header-->
                            <!--begin::Card body-->
<div class="card-body pt-5">
    <!--begin::Contact groups-->
    <div class="d-flex flex-column gap-5">
        <!--begin::Contact group-->
           <div class="d-flex flex-stack">
       <a href="#" class="fs-6 fw-bold text-gray-800 ">All Contacts</a>
       <div id="all_contacts_badge" class="badge badge-light-primary"></div>
   </div>
        <div class="d-flex flex-stack">
            <a  class="fs-6 fw-bold text-gray-800">Donors</a>
            <div id="donors_badge" class="badge badge-light-primary"></div>
        </div>
        <!--begin::Contact group-->
        <!--begin::Contact group-->
        <div class="d-flex flex-stack">
            <a  class="fs-6 fw-bold text-gray-800 ">Volunteers</a>
            <div id="volunteers_badge" class="badge badge-light-primary"></div>
        </div>
        <!--begin::Contact group-->
        <!--begin::Contact group-->
        <div class="d-flex flex-stack">
            <a  class="fs-6 fw-bold text-gray-800 ">Leads</a>
            <div id="leads_badge" class="badge badge-light-primary"></div>
        </div>
        <!--begin::Contact group-->
        <!--begin::Contact group-->
        <div class="d-flex flex-stack">
            <a class="fs-6 fw-bold text-gray-800">Sponsors</a>
            <div id="sponsors_badge" class="badge badge-light-primary"></div>
        </div>
        <!--begin::Contact group-->
        <!--begin::Contact group-->

        <!--begin::Contact group-->
    </div>
    <!--end::Contact groups-->
    <!--begin::Separator-->
    <div class="separator my-7"></div>
    <!--begin::Separator-->
    <!--begin::Add contact group-->
   
    <!--end::Add contact group-->
    <!--begin::Separator-->
    <div class="separator my-7"></div>
    <!--begin::Separator-->
    <!--begin::Add new contact-->
 <a onclick="clearForm()" href="/app/member.aspx?type=2&add=donor"  class="btn btn-primary w-100">
													<i class="ki-outline ki-badge fs-2"></i>Add new contact</a>

    <!--end::Add new contact-->
</div>
                            <!--end::Card body-->
                        </div>
                        <!--end::Contact group wrapper-->
                    </div>
                    <!--end::Contact groups-->
                    <!--begin::Search-->
                    <div class="col-lg-6 col-xl-3">
                        <!--begin::Contacts-->
                        <div class="card card-flush" id="kt_contacts_list">
                            <!--begin::Card header-->
                            <div class="card-header pt-7" id="kt_contacts_list_header">
                                <!--begin::Form-->
                                <form  class="d-flex align-items-center position-relative w-100 m-0" autocomplete="off">
                                    <!--begin::Icon-->
                                    <!--end::Icon-->
                                    <!--begin::Input-->
                                    <input type="text" id="searchContacts" class="form-control form-control-solid ps-13" name="search" value="" placeholder="Search contacts">
                                    <!--end::Input-->
                                </form>
                                <!--end::Form-->
                            </div>
                            <!--end::Card header-->
                            <!--begin::Card body-->
                            <div class="card-body pt-5" id="kt_contacts_list_body">
                                <!--begin::List-->
                                <div class="scroll-y me-n5 pe-5 h-300px h-xl-auto" data-kt-scroll="true" data-kt-scroll-activate="{default: false, lg: true}" data-kt-scroll-max-height="auto" data-kt-scroll-dependencies="#kt_header, #kt_toolbar, #kt_footer, #kt_contacts_list_header" data-kt-scroll-wrappers="#kt_content, #kt_contacts_list_body" data-kt-scroll-stretch="#kt_contacts_list, #kt_contacts_main" data-kt-scroll-offset="5px" style="max-height: 1041px;">
                                    <!--begin::User-->
<asp:Literal ID="ContactsListLiteral" runat="server"></asp:Literal>                                    <!--end::User-->
                                    <!--begin::Separator-->
                                    <div class="separator separator-dashed d-none"></div>
                                    <!--end::Separator-->
                                    <!--begin::User-->

                                </div>
                                <!--end::List-->
                            </div>
                            <!--end::Card body-->
                        </div>
                        <!--end::Contacts-->
                    </div>
                    <!--end::Search-->
                    <!--begin::Content-->
                    <div class="col-xl-6">
                        <!--begin::Contacts-->
                        <div class="card card-flush h-lg-100" id="kt_contacts_main">
                            <!--begin::Card header-->
                            <div class="card-header pt-7" id="kt_chat_contacts_header">
                                <!--begin::Card title-->
                                <div class="card-title">
                                    <i class="ki-outline ki-badge fs-1 me-2"></i>
                                    <h2>View Contact</h2>
                                   <a id="edit"> <i style="margin-left:10px" class="ki-outline ki-pencil fs-2"></i></a>
                                </div>
                                <!--end::Card title-->
                            </div>
                            <!--end::Card header-->
                            <!--begin::Card body-->
                            <div id="form" runat="server" class="card-body pt-5">
                                <!--begin::Form-->
                                <form id="kt_ecommerce_settings_general_form" class="form fv-plugins-bootstrap5 fv-plugins-framework">
                                    <!--begin::Input group-->
                                    <div class="mb-7">
                                        <!--begin::Label-->
                                    
                                        <!--end::Label-->
                                        <!--begin::Image input wrapper-->
                                        <div class="mt-1">
                                            <!--begin::Image placeholder-->
                                            <style>
                                                .image-input-placeholder {
                                                    background-image: url('../assets/media/svg/files/blank-image.svg');
                                                }

                                                [data-bs-theme="dark"] .image-input-placeholder {
                                                    background-image: url('../assets/media/svg/files/blank-image-dark.svg');
                                                }
                                            </style>
											<div class="image-input image-input-outline image-input-placeholder image-input-empty image-input-empty" id="bgimg" data-kt-image-input="true">
																	<!--begin::Preview existing avatar-->
																	<div class="image-input-wrapper w-100px h-100px" style="background-image: url('')"></div>
																	<!--end::Preview existing avatar-->
																	<!--begin::Edit-->
																
																	<!--end::Edit-->
																	<!--begin::Cancel-->
																	<span class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="cancel" data-bs-toggle="tooltip" aria-label="Cancel avatar" data-bs-original-title="Cancel avatar" data-kt-initialized="1">
																		<i class="ki-outline ki-cross fs-2"></i>
																	</span>
																	<!--end::Cancel-->
																	<!--begin::Remove-->
																	<span class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="remove" data-bs-toggle="tooltip" aria-label="Remove avatar" data-bs-original-title="Remove avatar" data-kt-initialized="1">
																		<i class="ki-outline ki-cross fs-2"></i>
																	</span>
																	<!--end::Remove-->
																</div>
                                            <!--end::Image placeholder-->
                                            <!--begin::Image input-->
                                            <!--end::Image input-->
                                        </div>
                                        <!--end::Image input wrapper-->
                                    </div>
                                    <!--end::Input group-->
                                    <!--begin::Input group-->
                                 
									<div class="fv-row mb-7 fv-plugins-icon-container">
    <label class="fs-6 fw-semibold form-label mt-3">
        <span class="">First Name</span>
        <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's first name." data-bs-original-title="Enter the contact's first name." data-kt-initialized="1">
            <i class="ki-outline ki-information fs-7"></i>
        </span>
    </label>
    <input readonly id="txtFirstName" type="text" class="form-control form-control-solid" name="firstName" value="">
    <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
</div>

<div class="fv-row mb-7 fv-plugins-icon-container">
    <label class="fs-6 fw-semibold form-label mt-3">
        <span class="">Last Name</span>
        <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's last name." data-bs-original-title="Enter the contact's last name." data-kt-initialized="1">
            <i class="ki-outline ki-information fs-7"></i>
        </span>
    </label>
    <input readonly id="txtLastName" type="text" class="form-control form-control-solid" name="lastName" value="">
    <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
</div>


<div class="fv-row mb-7">
    <label class="fs-6 fw-semibold form-label mt-3">
        <span>Company Name</span>
        <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's company name (optional)." data-bs-original-title="Enter the contact's company name (optional)." data-kt-initialized="1">
            <i class="ki-outline ki-information fs-7"></i>
        </span>
    </label>
    <input readonly id="txtCompanyName" type="text" class="form-control form-control-solid" name="company_name" value="">
</div>

<div class="row row-cols-1 row-cols-sm-2 rol-cols-md-1 row-cols-lg-2">
    <div class="col">
        <div class="fv-row mb-7 fv-plugins-icon-container">
            <label class="fs-6 fw-semibold form-label mt-3">
                <span class="">Email</span>
                <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's email." data-bs-original-title="Enter the contact's email." data-kt-initialized="1">
                    <i class="ki-outline ki-information fs-7"></i>
                </span>
            </label>
            <input readonly id="txtEmail" type="email" class="form-control form-control-solid" name="email" value="">
            <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
        </div>
    </div>
	    <div class="col">
        <div class="fv-row mb-7 fv-plugins-icon-container">
            <label class="fs-6 fw-semibold form-label mt-3">
                <span class="">Donations Raised</span>
                <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's email." data-bs-original-title="Enter the contact's email." data-kt-initialized="1">
                    <i class="ki-outline ki-information fs-7"></i>
                </span>
            </label>
            <input readonly id="DonationsRaised" type="text" class="form-control form-control-solid" name="DonationsRaised" value="">
            <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
        </div>
    </div>
    <div class="col">
        <div class="fv-row mb-7">
            <label class="fs-6 fw-semibold form-label mt-3">
                <span>Phone</span>
                <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's phone number (optional)." data-bs-original-title="Enter the contact's phone number (optional)." data-kt-initialized="1">
                    <i class="ki-outline ki-information fs-7"></i>
                </span>
            </label>
            <input readonly id="txtPhone" type="text" class="form-control form-control-solid" name="phone" value="" />
        </div>
    </div>
</div>
<div class="">
    <div class="" style="width:90%" id="tags-container">
        <label class="fs-6 fw-semibold form-label mt-3">Tags</label>
        <tags id="Skills" class="tagify form-control tags-container">
        </tags>
    </div>

    <div class="" style="width:90%">
        <label class="fs-6 fw-semibold form-label mt-3">Interests</label>
        <tags id="Interests" class="tagify form-control tags-container">
        </tags>
    </div>
</div>
<div class="row row-cols-1 row-cols-sm-2 rol-cols-md-1 row-cols-lg-2">
    </div>
    



<!-- Checkbox Group -->
<div class="fv-row mb-7">
    <div>
    <label class="fs-6 fw-semibold form-label mt-3">Categories</label>
    <div class="form-check">
        <input id="chkDonors" type="checkbox" class="form-check-input" name="categories" value="Donors">
        <label for="chkDonors" class="form-check-label">Donors</label>
    </div>
    <div class="form-check mt-5">
        <input id="chkVolunteers" type="checkbox" class="form-check-input" name="categories" value="Volunteers">
        <label for="chkVolunteers" class="form-check-label">Volunteers</label>
    </div>
    <div class="form-check mt-5">
        <input id="chkLeads" type="checkbox" class="form-check-input" name="categories" value="Leads">
        <label for="chkLeads" class="form-check-label">Leads</label>
    </div>
    <div class="form-check mt-5">
        <input id="chkSponsors" type="checkbox" class="form-check-input" name="categories" value="Sponsors">
        <label for="chkSponsors" class="form-check-label">Sponsors</label>
    </div>
        </div>
       
</div>


                                        <label class="fs-6 fw-semibold form-label mt-3">Donation History</label>

                                    <table class="table table-bordered">
  <thead style="font-weight:700">
    <tr>
      <th scope="col">Fundraiser</th>
      <th scope="col">Date</th>
      <th scope="col">Amount</th>
            <th scope="col">Status</th>
    </tr>
  </thead>
  <tbody id="payments">
      </tbody>
                                        </table>
                                                                            <label class="fs-6 fw-semibold form-label mt-3">Documents</label>

                                                                      <table class="table table-bordered">
<thead style="font-weight:700">
  <tr>
    <th scope="col">FileName</th>
          <th scope="col">Uploaded By</th>
    <th scope="col">Date</th>


  </tr>
</thead>
<tbody id="Documents">
    </tbody>
                                      </table>

<!-- Action Buttons -->
<div class="d-flex justify-content-end">
 
                                        <!--end::Button-->
                                    </div>
                                    <!--end::Action buttons-->
                                </form>
                                <!--end::Form-->
                            </div>
                            <!--end::Card body-->
                        </div>
                        <!--end::Contacts-->
                    </div>
                    <!--end::Content-->
                </div>
                <!--end::Contacts App- Add New Contact-->
            </div>
            <!--end::Content container-->
        </div>
        <!--end::Content-->
    </div></div>

    <script>var hostUrl = "../assets/";
    
        document.getElementById('searchContacts').addEventListener('input', function () {
            var filter = this.value.toLowerCase();
            var contacts = document.getElementsByClassName('contact-item');

            for (var i = 0; i < contacts.length; i++) {
                var contactName = contacts[i].querySelector('a').textContent.toLowerCase();
                var contactEmail = contacts[i].querySelector('div.fw-semibold').textContent.toLowerCase();

                if (contactName.includes(filter) || contactEmail.includes(filter)) {
                    contacts[i].style.display = '';
                } else {
                    contacts[i].style.display = 'none';
                    console.log(contacts[i].style);
                }
            }
        });



        document.getElementById('all_contacts_badge').innerHTML = allCount;
        document.getElementById('volunteers_badge').innerText = volunteersCount;
        document.getElementById('donors_badge').innerText = donorsCount;
        document.getElementById('leads_badge').innerText = leadsCount;
        document.getElementById('sponsors_badge').innerText = sponsorsCount;
        function displayContactDetails(email) {
            var contact = contacts.find(c => c.Email === email); // Assuming contacts array is accessible

            if (contact) {
                document.getElementById('edit').href = "/App/Member.aspx?mid=" + contact.ID+"&add=donor";

                var checkbox1 = document.getElementById('chkDonors');
                var checkbox2 = document.getElementById('chkVolunteers');
                var checkbox3 = document.getElementById('chkLeads');
                var checkbox4 = document.getElementById('chkSponsors');

                checkbox1.checked = false;
                checkbox2.checked = false;
                checkbox3.checked = false;
                checkbox4.checked = false;

                document.getElementById('txtFirstName').value = contact.FirstName || "";
                document.getElementById('txtLastName').value = contact.LastName || "";
                document.getElementById('txtCompanyName').value = contact.CompanyName || "";
                document.getElementById('txtEmail').value = contact.Email || "";
                document.getElementById('txtPhone').value = contact.Phone || "";
                document.getElementById('DonationsRaised').value = contact.DonationsRaised || 0;
                document.getElementById('bgimg').style.backgroundImage = `url('${contact.imgurl}')`;

                if (contact.Categories == 2 || contact.SID == 2) {
                    var checkbox = document.getElementById('chkDonors');
                    if (checkbox) {
                        checkbox.checked = true;
                    }
                }
                if (contact.Categories == 4) {
                    var checkbox = document.getElementById('chkVolunteers');
                    if (checkbox) {
                        checkbox.checked = true;
                    }
                }

                contact.Roles.forEach(category => {
                    var checkbox = document.getElementById('chk' + category + 's');
                    if (checkbox) {
                        checkbox.checked = true;
                    }
                });

                // Display tags
                var tagsContainer = document.querySelector('#Skills'); // Select the container for the skills tags
                tagsContainer.innerHTML = ''; // Clear existing tags
                var interestContainer = document.querySelector('#Interests'); // Select the container for the interest tags
                interestContainer.innerHTML = ''; // Clear existing interest tags

                if (contact.Tags && Array.isArray(contact.Tags)) {
                    contact.Tags.forEach(tag => {
                        try {
                            // Handle Skills
                            var SkillArrayChecked = tag.Skills || "[]"; // Provide a default empty JSON array if tag.Skills is null or undefined
                            var skillArray = JSON.parse(SkillArrayChecked); // Assuming tag.Skills is a JSON string

                            if (Array.isArray(skillArray)) {
                                skillArray.forEach(skillValue => {
                                    // Ensure skillValue is not null and has a value property
                                    var skillValueText = (skillValue && skillValue.value) ? skillValue.value : "Unknown"; // Provide a default value if null or undefined

                                    // Create tag element for skills
                                    var tagElement = document.createElement('tag');
                                    tagElement.className = 'tagify__tag';
                                    tagElement.setAttribute('title', skillValueText);
                                    tagElement.setAttribute('contenteditable', 'false');
                                    tagElement.setAttribute('spellcheck', 'false');
                                    tagElement.setAttribute('tabindex', '-1');
                                    tagElement.setAttribute('value', skillValueText);

                                    // Create remove button
                                    var removeBtn = document.createElement('x');
                                    removeBtn.className = 'tagify__tag__removeBtn';
                                    removeBtn.setAttribute('role', 'button');
                                    removeBtn.setAttribute('aria-label', 'remove tag');
                                    tagElement.appendChild(removeBtn);

                                    // Create tag text span
                                    var tagText = document.createElement('div');
                                    var tagTextSpan = document.createElement('span');
                                    tagTextSpan.className = 'tagify__tag-text';
                                    tagTextSpan.textContent = skillValueText;
                                    tagText.appendChild(tagTextSpan);
                                    tagElement.appendChild(tagText);

                                    // Append tag element to container
                                    tagsContainer.appendChild(tagElement);
                                });
                            } else {
                                console.error('Parsed skills data is not an array!');
                            }

                            // Handle Notes
                            if (tag.Notes) {
                                var notesArray = tag.Notes.split(',').map(note => note.trim()); // Split notes by comma and trim whitespace

                                notesArray.forEach(noteValue => {
                                    // Create tag element for notes
                                    var noteElement = document.createElement('tag');
                                    noteElement.className = 'tagify__tag';
                                    noteElement.setAttribute('title', noteValue);
                                    noteElement.setAttribute('contenteditable', 'false');
                                    noteElement.setAttribute('spellcheck', 'false');
                                    noteElement.setAttribute('tabindex', '-1');
                                    noteElement.setAttribute('value', noteValue);

                                    // Create remove button
                                    var removeBtn = document.createElement('x');
                                    removeBtn.className = 'tagify__tag__removeBtn';
                                    removeBtn.setAttribute('role', 'button');
                                    removeBtn.setAttribute('aria-label', 'remove tag');
                                    noteElement.appendChild(removeBtn);

                                    // Create tag text span
                                    var noteText = document.createElement('div');
                                    var noteTextSpan = document.createElement('span');
                                    noteTextSpan.className = 'tagify__tag-text';
                                    noteTextSpan.textContent = noteValue;
                                    noteText.appendChild(noteTextSpan);
                                    noteElement.appendChild(noteText);

                                    // Append tag element to interest container
                                    interestContainer.appendChild(noteElement);
                                });
                            }
                        } catch (error) {
                            console.error('Error processing tags:', error);
                        }
                    });
                } else {
                    console.error('Tags data is not an array or is missing!');
                }

                // Make input fields readonly
                var inputs = document.querySelectorAll('.form-control');
                inputs.forEach(input => {
                    input.setAttribute('readonly', 'readonly');
                });

                // Make checkboxes readonly
                var checkboxes = document.querySelectorAll('.form-check-input');
                checkboxes.forEach(checkbox => {
                    checkbox.setAttribute('disabled', 'disabled');
                });

                // Display payment details
                var paymentsTableBody = document.getElementById('payments');
                paymentsTableBody.innerHTML = ''; // Clear existing rows

                if (contact.Payments && Array.isArray(contact.Payments)) {
                    contact.Payments.forEach(payment => {
                        var row = document.createElement('tr');

                        var fundraiserCell = document.createElement('td');
                        fundraiserCell.textContent = payment.FundraiserNames || "Unknown";
                        row.appendChild(fundraiserCell);

                        var dateCell = document.createElement('td');
                        dateCell.textContent = new Date(payment.PaidDate).toLocaleDateString(); // Format date
                        row.appendChild(dateCell);

                        var amountCell = document.createElement('td');
                        amountCell.textContent = payment.Amount.toFixed(2); // Format amount
                        row.appendChild(amountCell);
                        var StatusCell = document.createElement('td');
                        var statusSpan = document.createElement('span');

                        if (payment.Status.toLowerCase() === "failed") {
                            statusSpan.className = "badge badge-light-danger";
                            statusSpan.textContent = payment.Status;
                        } else {
                            statusSpan.className = "badge badge-light-success";
                            statusSpan.textContent = payment.Status;
                        }

                        StatusCell.appendChild(statusSpan);
                        row.appendChild(StatusCell);

                        paymentsTableBody.appendChild(row);
                    });
                } else {
                    console.error('Payments data is not an array or is missing!');
                }






                var DocumentsTableBody = document.getElementById('Documents');
                DocumentsTableBody.innerHTML = ''; // Clear existing rows

                if (Array.isArray(contact.Documents)) {
                    contact.Documents.forEach(documentt => {
                        var row = document.createElement('tr');

                        var link = "/ImageHandler.ashx?id=" + documentt.FileID + "&s=" + documentt.Section;

                        var linkCell = document.createElement('td');
                        var anchor = document.createElement('a');
                        anchor.href = link;
                        anchor.textContent = documentt.FileName;
                        linkCell.appendChild(anchor);
                        row.appendChild(linkCell);

                        var NameCell = document.createElement('td');
                        // Check if ContractorName is null or empty, provide a default value if so
                        NameCell.innerText = documentt.ContractorName ? documentt.ContractorName : "Unknown";
                        row.appendChild(NameCell);

                        var Datecell = document.createElement('td');
                        // Check if UploadedBy is null or empty, provide a default value if so
                        Datecell.innerText = documentt.UploadedDate ? documentt.UploadedDate : "N/A";
                        row.appendChild(Datecell);

                        DocumentsTableBody.appendChild(row);
                    });
                } else {
                    console.error('Documents data is not an array or is missing!');
                }






            } else {
                console.error('Contact not found!');
            }
        }

        function clearForm() {
            // Clear all input fields
            var inputs = document.querySelectorAll('.form-control');
            inputs.forEach(input => {
                input.value = '';
                input.removeAttribute('readonly');
            });

            // Uncheck all checkboxes and make them editable
            var checkboxes = document.querySelectorAll('.form-check-input');
            checkboxes.forEach(checkbox => {
                checkbox.checked = false;
                checkbox.removeAttribute('disabled');
            });
        }
        console.log(contacts);
    </script>
    <!--begin::Global Javascript Bundle(mandatory for all pages)-->
    <script src="../assets/plugins/global/plugins.bundle.js"></script>
    <script src="../assets/js/scripts.bundle.js"></script>
    <!--end::Global Javascript Bundle-->
    <!--begin::Vendors Javascript(used for this page only)-->
    <script src="../assets/plugins/custom/datatables/datatables.bundle.js"></script>
    <!--end::Vendors Javascript-->
    <!--begin::Custom Javascript(used for this page only)-->
    <script src="../assets/js/custom/apps/contacts/edit-contact.js"></script>
    <script src="../assets/js/widgets.bundle.js"></script>
    <script src="../assets/js/custom/widgets.js"></script>
    <script src="../assets/js/custom/apps/chat/chat.js"></script>
    <script src="../assets/js/custom/utilities/modals/upgrade-plan.js"></script>
    <script src="../assets/js/custom/utilities/modals/create-campaign.js"></script>
    <script src="../assets/js/custom/utilities/modals/users-search.js"></script>
    <!--end::Custom Javascript-->
    <!--end::Javascript-->

</asp:Content>
