<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="DonorCRM.aspx.cs" Inherits="DonorCRM.WebForm1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Donor CRM
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        if (performance.navigation.type === 2) {
            // Page was accessed using the back/forward buttons
            window.location.reload();
        }

        document.addEventListener("DOMContentLoaded", function () {
            // Select the trigger and menu divs
            var triggerDiv = document.querySelector('[data-kt-menu-trigger="click"][data-kt-menu-attach="parent"][data-kt-menu-placement="bottom-end"][data-kt-menu-flip="bottom"]');
            var menuDiv = document.querySelector('.menu.menu-sub.menu-sub-dropdown.menu-column.menu-rounded.menu-gray-800.menu-state-bg.menu-state-primary.fw-bold.py-4.fs-6.w-275px');
            console.log(triggerDiv);
            if (triggerDiv && menuDiv) {
                // Remove `data-kt-menu` attributes from the trigger

                triggerDiv.removeAttribute('data-kt-menu-trigger');
                // Initially set the menu to closed state
                menuDiv.classList.add('menu-closed');
                menuDiv.style.display = 'none'; // Set initial display state to 'none'

                // Add click event listener to the trigger element
                triggerDiv.addEventListener('click', function () {
                    if (menuDiv.classList.contains('menu-closed')) {
                        // Open the menu
                        menuDiv.classList.remove('menu-closed');
                        menuDiv.classList.add('show');
                        menuDiv.style.zIndex = '105';
                        menuDiv.style.position = 'fixed';
                        menuDiv.style.inset = '0px 0px auto auto';
                        menuDiv.style.display = 'block';
                        menuDiv.style.transform = 'translate(-30px, 65px)';
                    } else {
                        // Close the menu
                        console.log("close");
                        menuDiv.classList.remove('show');
                        menuDiv.classList.add('menu-closed');
                        menuDiv.style.zIndex = '';
                        menuDiv.style.position = '';
                        menuDiv.style.inset = '';
                        menuDiv.style.display = 'none';
                        menuDiv.style.transform = '';
                    }
                });
            } else {
                console.error('Trigger or menu div not found.');
            }
        });
      


    </script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/tagify/4.31.2/tagify.min.js" integrity="sha512-A2qHx32K51q0EvjW10OTcPvF0UzSq3DleVukdbqL8VX8/Nl58vtR3WO2xn7j/Ft6T49i7tyevkoffSOARKyV8w==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/tagify/4.31.2/tagify.css" integrity="sha512-fg4mbaXioGkhZsVQlBUD7MmEA5zQY4I3aiawILa2nHXUk0e5gBZjlwGoJCeRIAVHqYOdaddDQA7HUXwqx3vVAA==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
<script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>

    <style>
        a:hover {
            color: deepskyblue !important;
        }
        tags{
            padding:10px !important;
        }
        .tag {
            display: inline-block;
            background-color: #f1f1f1;
            color: #333;
            padding: 3px 8px;
            border-radius: 15px;
            margin: 5px;
            font-size: 10px;
        }

        #RadioButtonListRoles tbody tr td label {
            margin-left: 10px;
        }

        select.fw-bold {
            font-weight: normal !important;
        }
        span.fw-bold{
            font-weight:normal !important;
        }

        /*.tags-container {
            width: 100%;
            max-width: 60%;
            overflow-x: auto;*/ /* Allow horizontal scrolling if necessary */
            /*white-space: nowrap;*/ /* Prevent line breaks within tags */
            /*display: inline-block;*/ /* Keep tags in a single line */
        /*}*/

        /*.tagify__tag {
            display: inline-block;*/ /* Ensure tags are displayed inline */
            /*margin: 2px;*/ /* Add some space between tags */
        /*}*/

        .form-check-input:disabled ~ .form-check-label, .form-check-input[disabled] ~ .form-check-label {
            opacity: 1;
            color: #252F4A
        }

        .form-check-input[disabled] {
            opacity: 1;
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
        <div id="alert" style="position: fixed; top: 20px; width: 100%; z-index: 1000;"></div>

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
                      
                        <!--begin::Content-->
                        <div class="col-xl-7">
                            <div class="card card-flush h-lg-100" id="kt_contacts_main">

                                <!--begin::Contacts-->
                                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                    <!-- View 1 -->
                                    <asp:View ID="View1" runat="server">
                                        <!--begin::Card header-->
                                        <div class="card-header pt-7" id="kt_chat_contacts_header">
                                            <!--begin::Card title-->
                                            <div class="card-title">
                                                <i class="ki-outline ki-badge fs-1 me-2"></i>
                                                <h2 runat="server" id="Title_Card">View Contact</h2>
                                                <a id="edit"></a>
                                            </div>
                                            <!--end::Card title-->
                                        </div>
                                        <!--end::Card header-->
                                        <!--begin::Card body-->
                                        <div id="form" style="background-color: #f9f9f9;"
                                            runat="server" class="card-body pt-5">
                                            <!--begin::Form-->
                                            <div id="kt_ecommerce_settings_general_form" class="form fv-plugins-bootstrap5 fv-plugins-framework">
                                                <!--begin::Input group-->
                                                <div style="background-color: white; padding: 40px; border-radius: 10px; margin-top: 43px;">
                                                    <div class="mb-7">
                                                        <!--begin::Image input wrapper-->
                                                        <div class="mt-1">
                                                            <style>
                                                                .image-input-placeholder {
                                                                    background-image: url('../assets/media/svg/files/blank-image.svg');
                                                                }

                                                                [data-bs-theme="dark"] .image-input-placeholder {
                                                                    background-image: url('../assets/media/svg/files/blank-image-dark.svg');
                                                                }

                                                                .avatarImg {
                                                                    width: 100%;
                                                                    height: 100%;
                                                                }
                                                            </style>

                                                            <div class="image-input image-input-outline image-input-placeholder image-input-empty image-input-empty" id="bgimg" data-kt-image-input="true">
                                                                <!--begin::Preview existing avatar-->
                                                                <div class="image-input-wrapper w-100px h-100px" style="background-image: url('')">
                                                                    <asp:Image ID="imgAvatar" CssClass="avatarImg" runat="server" />
                                                                </div>
                                                                <!--end::Preview existing avatar-->
                                                                <label id="avatarLabel" class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="change" data-bs-toggle="tooltip" aria-label="Change avatar" data-bs-original-title="Change avatar" data-kt-initialized="1">
                                                                    <!--begin::Inputs-->
                                                                    <i class="fas fa-pencil-alt" style="padding-left: 25px"></i>
                                                                    <asp:FileUpload ID="AvatarUpload" onchange="previewImage(event)" runat="server" Style="display: none" />
<asp:RegularExpressionValidator 
    ID="ImageFileValidator" 
    runat="server" 
    ControlToValidate="AvatarUpload" 
    ErrorMessage="Only image files are allowed." 
    ValidationExpression="^.*\.(jpg|jpeg|png|gif)$" 
    ForeColor="Red" />

                                                                    <!--end::Inputs-->
                                                                </label>
                                                                <!--begin::Cancel-->
                                                                <span  onclick="removeImage()" class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="cancel" data-bs-toggle="tooltip" aria-label="Cancel avatar" data-bs-original-title="Cancel avatar" data-kt-initialized="1">
                                                                    <i class="ki-outline ki-cross fs-2"></i>
                                                                </span>
                                                                <!--end::Cancel-->
                                                                <!--begin::Remove-->
                                                             
                                                                <!--end::Remove-->
                                                            </div>
                                                        </div>
                                                        <!--end::Image input wrapper-->
                                                    </div>
                                                    <!--end::Input group-->
                                                    <!--begin::Input group-->
                                                    <div class="fv-row mb-7 fv-plugins-icon-container">
                                                        <label class="fs-6 fw-semibold form-label mt-3">
                                                            <span>First Name</span>
                                                            <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's first name." data-bs-original-title="Enter the contact's first name." data-kt-initialized="1">
                                                                <i class="ki-outline ki-information fs-7"></i>
                                                            </span>
                                                        </label>
                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control form-control-solid"></asp:TextBox>
                                                        <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
                                                    </div>

                                                    <div class="fv-row mb-7 fv-plugins-icon-container">
                                                        <label class="fs-6 fw-semibold form-label mt-3">
                                                            <span>Last Name</span>
                                                            <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's last name." data-bs-original-title="Enter the contact's last name." data-kt-initialized="1">
                                                                <i class="ki-outline ki-information fs-7"></i>
                                                            </span>
                                                        </label>
                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control form-control-solid"></asp:TextBox>
                                                        <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
                                                    </div>

                                                    <div class="fv-row mb-7">
                                                        <label class="fs-6 fw-semibold form-label mt-3">
                                                            <span>Company Name</span>
                                                            <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's company name (optional)." data-bs-original-title="Enter the contact's company name (optional)." data-kt-initialized="1">
                                                                <i class="ki-outline ki-information fs-7"></i>
                                                            </span>
                                                        </label>
                                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control form-control-solid"></asp:TextBox>
                                                    </div>

                                                    <div class="row row-cols-1 row-cols-sm-2 rol-cols-md-1 row-cols-lg-2">



                                                        <div class="" style="width:100%">
                                                        <div class="col" style="width: 100%">
                                                            <div class="fv-row mb-7 fv-plugins-icon-container">
                                                                <label class="fs-6 fw-semibold form-label mt-3">
                                                                    <span>Email</span>
                                                                    <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's email." data-bs-original-title="Enter the contact's email." data-kt-initialized="1">
                                                                        <i class="ki-outline ki-information fs-7"></i>
                                                                    </span>
                                                                </label>
                                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-solid"></asp:TextBox>
                                                                <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
                                                            </div>
                                                        </div>
                                                               <div class="col">
                                                                          </div>

                                                        <div class="row row-cols-1 row-cols-sm-2 rol-cols-md-1 mb-7 row-cols-lg-1">                                                               <div class="col">

           <label class="fs-6 fw-semibold form-label mt-3">
               <span>Phone</span>
               <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's phone number (optional)." data-bs-original-title="Enter the contact's phone number (optional)." data-kt-initialized="1">
                   <i class="ki-outline ki-information fs-7"></i>
               </span>
           </label>
           <div class="row">
               <div class="col-lg-5" style="font-weight: normal!important">
                   <asp:DropDownList ID="ddlPhone" ClientIDMode="Static" runat="server"></asp:DropDownList>
               </div>
               <div class="col-lg-7">

                   <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
               </div>
           </div>
</div>
   </div>


                                                            </div>



                                                        <div class="row row-cols-1 row-cols-sm-2 rol-cols-md-1 row-cols-lg-2">
                                                            <div class="col" style="width: 100%; display: none">
                                                                <div class="fv-row mb-7 fv-plugins-icon-container">
                                                                    <label class="fs-6 fw-semibold form-label mt-3">
                                                                        <span>Password</span>
                                                                        <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's email." data-bs-original-title="Enter the contact's email." data-kt-initialized="1">
                                                                            <i class="ki-outline ki-information fs-7"></i>
                                                                        </span>
                                                                    </label>
                                                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control form-control-solid" Style="width: 100% !important; max-width: 900px !important;"></asp:TextBox>
                                                                    <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
                                                                </div>
                                                            </div>
                                                        </div>


                                                    </div>
                                                    <div class="fv-row">
                                                        <div class="col-lg-6">
                                                            <div class="fv-row m fv-plugins-icon-container">
                                                                <label class="fs-6 fw-semibold form-label mt-3">
                                                                    <span>Property Number and Street</span>
                                                                    <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's email." data-bs-original-title="Enter the contact's email." data-kt-initialized="1">
                                                                        <i class="ki-outline ki-information fs-7"></i>
                                                                    </span>
                                                                </label>
                                                                <asp:TextBox ID="propertynumandstreet" runat="server" CssClass="form-control form-control-solid"></asp:TextBox>
                                                                <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="fv-row mb-7">
                                                        <br />

                                                        <div class="col-lg-6">
                                                            <div class="fv-row m fv-plugins-icon-container">
                                                                <label class="fs-6 fw-semibold form-label mt-3">
                                                                    <span>Address Line 2</span>
                                                                    <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's email." data-bs-original-title="Enter the contact's email." data-kt-initialized="1">
                                                                        <i class="ki-outline ki-information fs-7"></i>
                                                                    </span>
                                                                </label>
                                                                <asp:TextBox ID="addressine2" runat="server" CssClass="form-control form-control-solid"></asp:TextBox>
                                                                <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
                                                            </div>
                                                        </div>

                                                        <div class="col-lg-6">
                                                            <div class="fv-row m fv-plugins-icon-container">
                                                                <label class="fs-6 fw-semibold form-label mt-3">
                                                                    <span>Town</span>
                                                                    <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's email." data-bs-original-title="Enter the contact's email." data-kt-initialized="1">
                                                                        <i class="ki-outline ki-information fs-7"></i>
                                                                    </span>
                                                                </label>
                                                                <asp:TextBox ID="town" runat="server" CssClass="form-control form-control-solid"></asp:TextBox>
                                                                <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <div class="fv-row m fv-plugins-icon-container">
                                                                <label class="fs-6 col-lg-6 fw-semibold form-label mt-3">
                                                                    <span>City</span>
                                                                    <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's email." data-bs-original-title="Enter the contact's email." data-kt-initialized="1">
                                                                        <i class="ki-outline ki-information fs-7"></i>
                                                                    </span>
                                                                </label>
                                                                <asp:TextBox ID="city" runat="server" CssClass="form-control form-control-solid"></asp:TextBox>
                                                                <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-6">
                                                        <div class="fv-row m fv-plugins-icon-container">
                                                            <label class="fs-6 col-lg-6 fw-semibold form-label mt-3">
<span style="white-space: nowrap;">Postalcode/Zipcode</span>
                                                                <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's email." data-bs-original-title="Enter the contact's email." data-kt-initialized="1">
                                                                    <i class="ki-outline ki-information fs-7"></i>
                                                                </span>
                                                            </label>
                                                            <asp:TextBox ID="postalcode" runat="server" CssClass="form-control form-control-solid"></asp:TextBox>
                                                            <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="fv-row m fv-plugins-icon-container">
                                                            <label class="fs-6 col-lg-6 fw-semibold form-label mt-3">
                                                                <span>Country</span>
                                                                <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's email." data-bs-original-title="Enter the contact's email." data-kt-initialized="1">
                                                                    <i class="ki-outline ki-information fs-7"></i>
                                                                </span>
                                                            </label>
<asp:DropDownList ID="ddlCountry" ClientIDMode="Static" CssClass="" runat="server" onchange="setHiddenFieldValue()"></asp:DropDownList>
<asp:HiddenField ID="hiddenFieldCountry" Value="0" runat="server" ClientIDMode="Static" />
                                                            <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
                                                        </div>
                                                    </div>



                                                    <div class="col-lg-6">
                                                        <div class="fv-row mb-7 fv-plugins-icon-container">
                                                            <label class="fs-6 fw-semibold form-label mt-3">
                                                                <span>Donations Raised</span>
                                                                <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's donations raised." data-bs-original-title="Enter the contact's donations raised." data-kt-initialized="1">
                                                                    <i class="ki-outline ki-information fs-7"></i>
                                                                </span>
                                                            </label>
                                                            <asp:TextBox ID="txtDonationsRaised" ReadOnly="true" runat="server" CssClass="form-control form-control-solid"></asp:TextBox>
                                                            <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
                                                        </div>
                                                    </div>
                                               
                                                    <div class="">
                                              <!-- Tags Container -->
<!-- Tags Container -->
<!-- Tags Container -->
<div class="tag-container" style="width: 100%" id="tags-container">
    <label class="fs-6 fw-semibold form-label mt-3">Tags</label>
    <input name="tags" class="form-control form-control-solid" id="tags" value="">
    <asp:TextBox ID="txttags" runat="server" Style="display: none;"></asp:TextBox>
</div>

<!-- Interests Container -->
<div class="interest-container" style="width: 100%">
    <label class="fs-6 fw-semibold form-label mt-3">Interests</label>
    <input name="interests" class="form-control form-control-solid" id="interests" value="">
    <asp:TextBox ID="txtinterest" runat="server" Style="display: none;"></asp:TextBox>
</div>
<script>
    document.addEventListener('DOMContentLoaded', function () {
        // Initialize Tagify for Tags
        var inputTags = document.querySelector('#tags');
        var tagifyTags = new Tagify(inputTags, {
            whitelist: [], // Add any predefined tags if needed
            enforceWhitelist: false,
            dropdown: {
                maxItems: 20, // Maximum items to show in the dropdown
                enabled: 0 // Always show the dropdown (0 disables it)
            }
        });

        // Sync Tags with ASP.NET Hidden Field
        tagifyTags.on('add', function () { updateHiddenField(tagifyTags, '<%= txttags.ClientID %>'); });
        tagifyTags.on('remove', function () { updateHiddenField(tagifyTags, '<%= txttags.ClientID %>'); });
        tagifyTags.on('edit', function () { updateHiddenField(tagifyTags, '<%= txttags.ClientID %>'); });

        // Prepopulate Tags
        var tagsString = '<%= txttags.Text %>';
    if (tagsString) {
        tagifyTags.addTags(tagsString.split(','));
    }

    // Initialize Tagify for Interests
    var inputInterests = document.querySelector('#interests');
    var tagifyInterests = new Tagify(inputInterests, {
        whitelist: [],
        enforceWhitelist: false,
        dropdown: {
            maxItems: 20,
            enabled: 0
        }
    });

    // Sync Interests with ASP.NET Hidden Field
    tagifyInterests.on('add', function() { updateHiddenField(tagifyInterests, '<%= txtinterest.ClientID %>'); });
    tagifyInterests.on('remove', function() { updateHiddenField(tagifyInterests, '<%= txtinterest.ClientID %>'); });
    tagifyInterests.on('edit', function() { updateHiddenField(tagifyInterests, '<%= txtinterest.ClientID %>'); });

    // Prepopulate Interests
        var interestsString = '<%= txtinterest.Text %>';
        if (interestsString) {
            tagifyInterests.addTags(interestsString.split(','));
        }
    });

    function updateHiddenField(tagifyInstance, hiddenFieldId) {
        var tagsArray = tagifyInstance.value.map(tag => tag.value);
        document.getElementById(hiddenFieldId).value = tagsArray.join(',');
    }

</script>
                                       

                                                    </div>
                                                    <div class="row row-cols-1 row-cols-sm-2 rol-cols-md-1 row-cols-lg-2"></div>













                                                    <!-- Checkbox Group -->
                                                    <label class="fs-6 fw-semibold form-label mt-3">Categories</label>
                                                    <div class="form-check mt-2">
                                                        <asp:CheckBox ID="chkDonors" runat="server" CssClass="widget-13-check" />
                                                        <label style="margin-left: 8px" class="" for="chkDonors">Donors</label>
                                                    </div>
                                                    <div class="form-check mt-2">
                                                        <asp:CheckBox ID="chkVolunteers" runat="server" CssClass="\widget-13-check" />
                                                        <label style="margin-left: 8px" class="" for="chkVolunteers">Volunteers</label>
                                                    </div>
                                                    <div class="form-check mt-2">
                                                        <asp:CheckBox ID="chkLeads" runat="server" CssClass="widget-13-check" />
                                                        <label style="margin-left: 8px" class="" for="chkLeads">Leads</label>
                                                    </div>
                                                    <div class="form-check mt-2">
                                                        <asp:CheckBox ID="chkMembers" runat="server" CssClass="widget-13-check" />
                                                        <label style="margin-left: 8px" class="" for="chkMembers">Sponsors</label>
                                                    </div>
                                                </div>

                                                <div id="educationSkillsDiv" style="background-color: white; padding: 40px; border-radius: 10px; margin-top: 43px; display: block;">
    <label class="fs-6 fw-semibold form-label mt-3">Education and Skills</label>

    <div class="form-group" style="margin-top: 20px;">
        <asp:Label ID="lblDate" runat="server" Text="Enrolment Date" CssClass="form-label"></asp:Label>
        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group" style="margin-top: 20px;">
        <asp:Label ID="lblLevel" runat="server" Text="Level" CssClass="form-label"></asp:Label>
       <asp:DropDownList ID="ddlLevel" runat="server" CssClass="form-control">
    <asp:ListItem>Apprenticeship</asp:ListItem>
    <asp:ListItem>Associate Degree</asp:ListItem>
    <asp:ListItem>Bachelor’s Degree</asp:ListItem>
    <asp:ListItem>Certificate</asp:ListItem>
    <asp:ListItem>Chartered Status</asp:ListItem>
    <asp:ListItem>Continuing Education Credits (CEUs)</asp:ListItem>
    <asp:ListItem>Diploma</asp:ListItem>
    <asp:ListItem>Diploma of Higher Education (DipHE)</asp:ListItem>
    <asp:ListItem>Doctorate (PhD/DPhil)</asp:ListItem>
    <asp:ListItem>Foundation Degree</asp:ListItem>
    <asp:ListItem>General Certificate of Secondary Education (GCSE)</asp:ListItem>
    <asp:ListItem>Graduate Certificate/Diploma</asp:ListItem>
    <asp:ListItem>Honours Degree</asp:ListItem>
    <asp:ListItem>International General Certificate of Secondary Education (IGCSE)</asp:ListItem>
    <asp:ListItem>Language Proficiency Certificates</asp:ListItem>
    <asp:ListItem>Lower Secondary Education Certificate</asp:ListItem>
    <asp:ListItem>Master’s Degree</asp:ListItem>
    <asp:ListItem>Professional Certification</asp:ListItem>
    <asp:ListItem>Professional Doctorate</asp:ListItem>
    <asp:ListItem>Secondary School Certificate/Diploma</asp:ListItem>
    <asp:ListItem>Technical Certificates/Diplomas</asp:ListItem>
    <asp:ListItem>Vocational Certificates/Diplomas</asp:ListItem>
</asp:DropDownList>

    </div>

    <div class="form-group" style="margin-top: 20px;">
        <asp:Label ID="lblQualification" runat="server" Text="Qualification" CssClass="form-label"></asp:Label>
        <asp:TextBox ID="txtQualification" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group" style="margin-top: 20px;">
        <asp:Label ID="lblGrade" runat="server" Text="Grade" CssClass="form-label"></asp:Label>
        <asp:TextBox ID="txtGrade" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group" style="margin-top: 20px;">
        <asp:Label ID="lblInstitution" runat="server" Text="Institution" CssClass="form-label"></asp:Label>
        <asp:TextBox ID="txtInstitution" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group" style="margin-top: 20px;">
        <asp:Label ID="lblDateQualified" runat="server" Text="Date Qualified" CssClass="form-label"></asp:Label>
<asp:TextBox ID="txtDateQualified" runat="server" CssClass="form-control" ></asp:TextBox>
    </div>
</div>



                                                <div id="paymentsdiv" style="background-color: white; padding: 40px; border-radius: 10px; margin-top: 43px;">
                                                    <label class="fs-6 fw-semibold form-label mt-3">Donations</label>

                                                    <div class="table-responsive" style="margin-left: 40px;">
                                                        <asp:Literal ID="ltrPayments" runat="server"></asp:Literal>
                                                    </div>
                                                    <div class="d-flex justify-content-center" style="margin: auto">
                                                        <div id="pagination-controlsp" style="margin-left: 40px;">
                                                            <button style="border: 1px solid #dcdcdc;" id="prev-pagep" class="btn btn-light" type="button" onclick="changePagep(-1)">Previous</button>
                                                            <span style="margin: 3px 10px" id="page-infop"></span>
                                                            <button style="border: 1px solid #dcdcdc;" id="next-pagep" class="btn btn-light" type="button" onclick="changePagep(1)">Next</button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="documentsdiv" style="background-color: white; padding: 40px; border-radius: 10px; margin-top: 43px;">
                                                    <label class="fs-6 fw-semibold form-label mt-3">Documents</label>

                                                    <!-- View 3 -->
                                                    <div class="table-responsive" style="margin-left: 40px;">
                                                        <asp:Literal ID="ltrDocuments" runat="server"></asp:Literal>


                                                    </div>
                                                    <div class="d-flex justify-content-center" style="margin: auto">
                                                        <div id="pagination-controls" style="margin-left: 40px;">
                                                            <button style="border: 1px solid #dcdcdc;" id="prev-page" class="btn btn-light" type="button" onclick="changePage(-1)">Previous</button>
                                                            <span style="margin: 3px 10px" id="page-info"></span>
                                                            <button style="border: 1px solid #dcdcdc;" id="next-page" class="btn btn-light" type="button" onclick="changePage(1)">Next</button>
                                                        </div>
                                                    </div>
                                                    <label class="fs-6 fw-semibold form-label mt-3">Upload a Document</label>

                                                    <asp:FileUpload AllowMultiple="true" Style="margin-left: 40px; margin-right: 40px; width: 90%" CssClass="form-control" ID="DocumentFile" runat="server" />

                                                    <asp:HiddenField ID="txtImagePath" runat="server" />
                                                    <!-- Submit Button -->

                                                    <input type="hidden" />
                                                </div>
                                                <!--end::Form-->
                                            </div>
                                            <!--end::Card body-->
                                            <!-- View 2 -->
                                        </div>


                                        <div class="card-footer d-flex justify-content-end py-6 px-9">

                                            <br />
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click" Text="Save Changes" />

                                        </div>
                                    </asp:View>
                                    <asp:View ID="view2" runat="server">
                                        <h1>Hello</h1>
                                    </asp:View>
                                </asp:MultiView>
                            </div>

                        </div>
                          <div class="col-lg-7 col-xl-5">
  <!--begin::Contact groups-->
  <div class="">
      <!--begin::Contact group wrapper-->
      <div class="card card-flush">
          <!--begin::Card header-->
          <div class="card-header pt-7">
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
                      <a href="DonorCrm.aspx" class="fs-6 hover-primary fw-bold text-gray-800 ">All Contacts</a>
                      <div id="all_contacts_badge" class="badge badge-light-primary"></div>
                  </div>
                  <div class="d-flex flex-stack">
                      <a href="?SID=1" class="fs-6 fw-bold hover-primary text-gray-800">Donors</a>
                      <div id="donors_badge" class="badge badge-light-primary"></div>
                  </div>
                  <!--begin::Contact group-->
                  <!--begin::Contact group-->
                  <div class="d-flex flex-stack">
                      <a href="?SID=2" class="fs-6 fw-bold text-gray-800 hover-primary ">Volunteers</a>
                      <div id="volunteers_badge" class="badge badge-light-primary"></div>
                  </div>
                  <!--begin::Contact group-->
                  <!--begin::Contact group-->
                  <div class="d-flex flex-stack">
                      <a href="?SID=3" class="fs-6 fw-bold text-gray-800 hover-primary">Leads</a>
                      <div id="leads_badge" class="badge badge-light-primary"></div>
                  </div>
                  <!--begin::Contact group-->
                  <!--begin::Contact group-->
                  <div class="d-flex flex-stack">
                      <a href="?SID=4" class="fs-6 fw-bold text-gray-800 hover-primary">Sponsors</a>
                      <div id="sponsors_badge" class="badge badge-light-primary"></div>
                  </div>
                  <!--begin::Contact group-->
                  <!--begin::Contact group-->

                  <!--begin::Contact group-->
              </div>
              <!--end::Contact groups-->
              <!--begin::Separator-->
              <!--begin::Separator-->
              <!--begin::Add contact group-->

              <!--end::Add contact group-->
              <!--begin::Separator-->
              <div class="separator my-7"></div>
              <!--begin::Separator-->
              <!--begin::Add new contact-->
              <div class="d-flex">
                  <a onclick="clearForm()" href="DONORCRM.aspx" class="btn btn-primary w-50 me-4">Add new contact
                  </a>

                  <asp:Button runat="server" ID="syncwithmailchimp" CssClass="btn btn-primary w-50" Text="Sync with Mailchimp" OnClick="syncwithmailchimp_Click" />
              </div>


              <!--end::Add new contact-->
          </div>
          <!--end::Card body-->
      </div>
      <!--end::Contact group wrapper-->
  </div>
  <!--end::Contact groups-->
  <!--begin::Search-->
  <div class="mt-3">
      <!--begin::Contacts-->
      <div class="card card-flush" id="kt_contacts_list">
          <!--begin::Card header-->
          <div class="card-header pt-7" id="kt_contacts_list_header">
              <!--begin::Form-->
              <div class="d-flex align-items-center position-relative w-100 m-0" autocomplete="off">
                  <!--begin::Icon-->
                  <!--end::Icon-->
                  <!--begin::Input-->
                  <input type="text" id="searchContacts" class="form-control form-control-solid ps-13" name="search" value="" placeholder="Search contacts" />
                  <!--end::Input-->
              </div>
              <!--end::Form-->
          </div>
          <!--end::Card header-->
          <!--begin::Card body-->
          <div class="card-body pt-5" id="kt_contacts_list_body">
              <!--begin::List-->
              <div class="scroll-y me-n5 pe-5 h-300px h-xl-auto" data-kt-scroll="true" data-kt-scroll-activate="{default: false, lg: true}" data-kt-scroll-max-height="auto" data-kt-scroll-dependencies="#kt_header, #kt_toolbar, #kt_footer, #kt_contacts_list_header" data-kt-scroll-wrappers="#kt_content, #kt_contacts_list_body" data-kt-scroll-stretch="#kt_contacts_list, #kt_contacts_main" data-kt-scroll-offset="5px" style="max-height: 1041px;">
                  <!--begin::User-->
                  <asp:Literal ID="ContactsListLiteral" runat="server"></asp:Literal>
                  <!--end::User-->
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
      </div>
                        <!--end::Content-->
                    </div>
                    <!--end::Contacts App- Add New Contact-->
                </div>
                <!--end::Content container-->
            </div>
            <!--end::Content-->
        </div>
    </div>
    <!-- Modal -->
    <!-- Modal -->
    <div class="modal fade" id="paymentDetailsModal" tabindex="-1" aria-labelledby="paymentDetailsModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="paymentDetailsModalLabel">Payment Details</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="UNID" ClientIDMode="Static" CssClass="d-none" runat="server" />
                    <p class="display-6"><strong>Amount:</strong> &#163;<span id="modalAmount"></span></p>
                    <p><strong>Status:</strong> <span id="modalStatus" class="badge"></span></p>
                    <div style="margin-bottom: 10px" class="separator"></div>
                    <p><strong>Name:</strong> <span id="modalName"></span></p>
                    <div style="margin-bottom: 10px" class="separator"></div>
                    <p><strong>Email:</strong> <span id="modalEmail"></span></p>
                    <div style="margin-bottom: 10px" class="separator"></div>
                    <p><strong>Fundraiser Names:</strong> <span id="modalFundraiserNames"></span></p>
                    <div style="margin-bottom: 10px" class="separator"></div>
                    <p><strong>Platform Fee:</strong> &#163;<span id="modalPlatformFee"></span></p>
                    <div style="margin-bottom: 10px" class="separator"></div>
                    <p><strong>Payment Type:</strong> <span id="modalPaymentType"></span></p>
                    <div style="margin-bottom: 10px" class="separator"></div>
                    <div id="receipts1" runat="server"></div>
                    <asp:Literal runat="server" ID="receiptsLiteral" />
                    <div class="d-flex mt-5" style="margin-right: 40px">
                        <asp:FileUpload Style="margin-left: 40px; margin-right: 40px; width: 90%" CssClass="form-control" AllowMultiple="true" ID="ReceiptsUpload" runat="server" />
                        <asp:Button runat="server" OnClick="Unnamed_Click" Text="Save File" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>



        <script type="text/javascript">
            function setHiddenFieldValue() {
        var ddl = document.getElementById('<%= ddlCountry.ClientID %>');
        var hiddenField = document.getElementById('<%= hiddenFieldCountry.ClientID %>');
            hiddenField.value = ddl.value;
    }
    </script>

    <script>
        function previewImage(event) {
            var input = event.target;
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    // Set the src attribute of the asp:Image control
                    var imgControl = document.getElementById('<%= imgAvatar.ClientID %>');
                    imgControl.src = e.target.result;
                };

                reader.readAsDataURL(input.files[0]);
            }
        }
        function removeImage() {
            // Clear the src of the Image control
            var imgControl = document.getElementById('<%= imgAvatar.ClientID %>');
    imgControl.src = '';

    // Reset the FileUpload control
            var fileUploadControl = document.getElementById('<%= AvatarUpload.ClientID %>');
            fileUploadControl.value = ''; // Clear the selected file
        }


    </script>
    <script>

        let currentPage = 1;
        const rowsPerPage = 10;
        function paymentTable() {
            const table = document.getElementById('payment-table');
            const rows = Array.from(table.querySelectorAll('tbody tr'));
            const totalPages = Math.ceil(rows.length / rowsPerPage);

            // Hide all rows
            rows.forEach(row => row.style.display = 'none');

            // Show only the rows for the current page
            const start = (currentPage - 1) * rowsPerPage;
            const end = start + rowsPerPage;
            rows.slice(start, end).forEach(row => row.style.display = '');

            // Update page info
            document.getElementById('page-infop').textContent = `Page ${currentPage} of ${totalPages}`;

            // Disable prev/next buttons if on first/last page
            document.getElementById('prev-pagep').disabled = currentPage === 1;
            document.getElementById('next-pagep').disabled = currentPage === totalPages;
        }
        function paginateTable() {
            const table = document.getElementById('documents-table');
            const rows = Array.from(table.querySelectorAll('tbody tr'));
            const totalPages = Math.ceil(rows.length / rowsPerPage);

            // Hide all rows
            rows.forEach(row => row.style.display = 'none');

            // Show only the rows for the current page
            const start = (currentPage - 1) * rowsPerPage;
            const end = start + rowsPerPage;
            rows.slice(start, end).forEach(row => row.style.display = '');

            // Update page info
            document.getElementById('page-info').textContent = `Page ${currentPage} of ${totalPages}`;

            // Disable prev/next buttons if on first/last page
            document.getElementById('prev-page').disabled = currentPage === 1;
            document.getElementById('next-page').disabled = currentPage === totalPages;
        }

        function changePage(direction) {
            const table = document.getElementById('documents-table');
            const totalRows = table.querySelectorAll('tbody tr').length;
            const totalPages = Math.ceil(totalRows / rowsPerPage);

            currentPage += direction;

            // Ensure currentPage stays within bounds
            currentPage = Math.max(1, Math.min(currentPage, totalPages));

            paginateTable();
        }
        function changePagep(direction) {
            const table = document.getElementById('payment-table');
            const totalRows = table.querySelectorAll('tbody tr').length;
            const totalPages = Math.ceil(totalRows / rowsPerPage);

            currentPage += direction;

            // Ensure currentPage stays within bounds
            currentPage = Math.max(1, Math.min(currentPage, totalPages));

            paymentTable();
        }

        // Initialize pagination on page load
        document.addEventListener('DOMContentLoaded', () => {
            // Function to get query parameter value by name
            function getQueryParam(name) {
                const urlParams = new URLSearchParams(window.location.search);
                return urlParams.get(name);
            }

            // Get the `id` query parameter
            const id = getQueryParam('id');

            // Select the div elements
            const documentsDiv = document.getElementById('documentsdiv');
            const paymentsDiv = document.getElementById('paymentsdiv');

            // Show or hide the divs based on the presence of `id` in the query string
            if (id) {
                // If `id` is present, show the divs
                if (documentsDiv) documentsDiv.style.display = 'block';
                console.log("block")
                if (paymentsDiv) paymentsDiv.style.display = 'block';
            } else {
                // If `id` is not present, hide the divs
                if (documentsDiv) documentsDiv.style.display = 'none';
                if (paymentsDiv) paymentsDiv.style.display = 'none';
                console.log("none")

            }
            paginateTable();
            paymentTable();
            const tid = getQueryParam('tid');

            // If id is present, find the corresponding row and call handlePaymentDetails
            if (tid) {
                // Find the table row with the matching id
                const table = document.getElementById('payment-table');
                const rows = table.querySelectorAll('tbody tr');

                rows.forEach(row => {
                    const button = row.querySelector('button');
                    const onclickContent = button.getAttribute('onclick');
                    const buttonId = onclickContent.match(/handlePaymentDetails\('([^']*)'/)[1];

                    if (buttonId === tid) {
                        // Extract values from the onclick attribute
                        const match = onclickContent.match(/handlePaymentDetails\('([^']*)', '([^']*)', '([^']*)', ([^,]*), '([^']*)', ([^,]*), '([^']*)', '([^']*)'\)/);
                        if (match) {
                            const [, id, name, email, amount, fundraiserNames, platformFee, paymentType, status] = match;
                            // Call the handlePaymentDetails function with extracted values
                            handlePaymentDetails(id, name, email, parseFloat(amount), fundraiserNames, parseFloat(platformFee), paymentType, status);
                        }
                    }
                });
            }
        });
    </script>




    <script>var hostUrl = "../assets/";


        document.getElementById('UNID').style.display = 'none';
        function populateDonorDocs(fileDataJson) {
            var fileData = fileDataJson; // Ensure JSON is parsed into an object
            var tbody = document.getElementById('donordocs');
            tbody.innerHTML = ""; // Clear any existing content

            fileData.forEach(function (file) {
                var tr = document.createElement('tr');

                // Create the file link
                var tdLink = document.createElement('td');
                var a = document.createElement('a');
                a.href = '/imagehandler.ashx?id=' + file.FileID + '&s=donordoc';
                a.textContent = file.FileName;
                tdLink.appendChild(a);

                // Create the delete button
                var tdDelete = document.createElement('td');
                var deleteButton = document.createElement('button');
                deleteButton.type = 'button';
                deleteButton.style.border = 'none';
                deleteButton.style.marginLeft = "70%";

                deleteButton.className = 'btn btn-light';
                deleteButton.innerHTML = '<i style="font-size:16px;" class="bi bi-trash"></i>'; // Use the Bootstrap Icons class
                deleteButton.onclick = function () { deleteFile(file.FileID); };

                tdDelete.appendChild(deleteButton);

                // Append both cells to the row
                tr.appendChild(tdLink);
                tr.appendChild(tdDelete);
                tbody.appendChild(tr);
            });
        }

        // Function to call the web method to delete the file
        function deleteFile(fileId) {
            fetch('DonorCRM.aspx/DeleteFile', { // Make sure the URL matches your actual WebMethod URL
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({ fileId: fileId })
            })
                .then(response => response.json())
                .then(result => {
                    if (result.d) { // Access the .d property to get the result from WebMethod
                        showAlert('File deleted successfully', 'success');
                        // Optionally, remove the file row from the table
                        var row = document.querySelector(`tr:has(button[onclick*="${fileId}"])`);
                        if (row) row.remove();
                    } else {
                        showAlert('Error deleting file', 'danger');
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    showAlert('An error occurred while deleting the file', 'danger');
                });
        }

        function showAlert(message, type) {
            const alertDiv = document.getElementById('alert');
            alertDiv.innerHTML = `
        <div class="alert alert-${type} alert-dismissible fade show" role="alert">
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;

            // Automatically dismiss the alert after 2 seconds
            setTimeout(() => {
                const alert = alertDiv.querySelector('.alert');
                if (alert) {
                    alert.classList.remove('show');
                    alert.addEventListener('transitionend', () => alert.remove());
                }
            }, 2000);
        }


        function formatNumber(amount) {
            // Convert to a float with two decimal places
            var formattedAmount = parseFloat(amount).toFixed(2);

            // Split the amount into integer and decimal parts
            var parts = formattedAmount.split('.');
            var integerPart = parts[0];
            var decimalPart = parts[1];

            // Add comma delimiters to the integer part
            var commaDelimitedInteger = integerPart.replace(/\B(?=(\d{3})+(?!\d))/g, ',');

            // Combine integer and decimal parts
            var result = commaDelimitedInteger + '.' + decimalPart;

            return result;
        }

        function handlePaymentDetails(tid, name, email, amount, fundraiserNames, platformFee, paymentType, status) {
            document.getElementById('UNID').value = tid;
            var formattedAmount = formatNumber(amount);
            var formattedPlatformFee = formatNumber(platformFee);
            // populateDonorDocs(fileDataJson)
            // Populate modal fields
            document.getElementById('modalAmount').textContent = formattedAmount;
            document.getElementById('modalName').textContent = name;
            document.getElementById('modalEmail').textContent = email;
            document.getElementById('modalFundraiserNames').textContent = fundraiserNames;
            document.getElementById('modalPlatformFee').textContent = formattedPlatformFee;
            document.getElementById('modalPaymentType').textContent = paymentType;
            var button = event.target;

            /*// Get the data-file-data attribute
            var fileDataJson = button.getAttribute('data-file-data');

            // Parse the JSON string
            var fileDataArray = JSON.parse(fileDataJson);
            console.log(fileDataArray)*/
            //populateDonorDocs(fileDataArray)


            // Set status with appropriate badge
            var statusBadge = document.getElementById('modalStatus');
            statusBadge.textContent = status;
            if (status === "Successful") {
                statusBadge.className = "badge bg-success";
            } else {
                statusBadge.className = "badge bg-danger";
            }

            // Show the modal
            var paymentDetailsModal = new bootstrap.Modal(document.getElementById('paymentDetailsModal'));
            paymentDetailsModal.show();
            document.getElementById('paymentDetailsModal').addEventListener('hidden.bs.modal', function (e) {
                document.body.classList.remove('modal-open');
                const modals = document.querySelectorAll('.modal-backdrop');
                modals.forEach(modal => modal.remove());
                // Reset the body overflow
                document.body.style.overflow = '';
            });

        }


        document.getElementById('searchContacts').addEventListener('input', function (event) {
            // Prevent any default behavior (e.g., form submission) if necessary
            console.log("asd");
            event.preventDefault();

            var filter = this.value.toLowerCase(); // Get the search input value in lowercase
            var contacts = document.getElementsByClassName('contact-item'); // Get all elements with class 'contact-item'

            // Loop through all contact items
            for (var i = 0; i < contacts.length; i++) {
                // Extract the name and email from each contact item and convert them to lowercase
                var contactName = contacts[i].querySelector('p.fs-6').textContent.toLowerCase();
                var contactEmail = contacts[i].querySelector('div.fw-semibold').textContent.toLowerCase();
                var tags = contacts[i].querySelector('span#tagsforsearch').textContent.toLowerCase();
                // Check if the input filter matches either the name or the email
                if (contactName.includes(filter) || contactEmail.includes(filter) || tags.includes(filter)) {
                    contacts[i].style.display = ''; // Show the contact item
                } else {
                    contacts[i].style.display = 'none'; // Hide the contact item
                }
            }
        });

        document.addEventListener('keydown', function (event) {
            // Check if the Enter key is pressed
            if (event.key === 'Enter' || event.keyCode === 13) {
                // Prevent the default action (e.g., form submission)
                event.preventDefault();
            }
        });

        document.getElementById('all_contacts_badge').innerHTML = allCount;
        document.getElementById('volunteers_badge').innerText = volunteersCount;
        document.getElementById('donors_badge').innerText = donorsCount;
        document.getElementById('leads_badge').innerText = leadsCount;
        document.getElementById('sponsors_badge').innerText = sponsorsCount;

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
    <script>
        $(document).ready(function () {
            $("#ddlPhone").select2();
            $("#ddlCountry").select2();
        });

    </script>
     <script>
         document.addEventListener('DOMContentLoaded', function () {
             flatpickr("#<%= txtDate.ClientID %>", {
            dateFormat: "d/m/Y", // UK date format
            locale: {
                firstDayOfWeek: 1 // Start week on Monday
            }
        });

        flatpickr("#<%= txtDateQualified.ClientID %>", {
            dateFormat: "d/m/Y", // UK date format
            locale: {
                firstDayOfWeek: 1 // Start week on Monday
            }
        });
    });
     </script>

    <!--begin::Global Javascript Bundle(mandatory for all pages)-->

    <!--end::Custom Javascript-->
              

</asp:Content>
