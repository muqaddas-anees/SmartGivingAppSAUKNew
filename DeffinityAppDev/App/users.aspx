<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="users.aspx.cs" Inherits="Users.users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <asp:Label ID="lblPageTitle" runat="server" Text="Team"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <!--begin::Fonts(mandatory for all pages)-->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Inter:300,400,500,600,700" />
    <!--end::Fonts-->
    <!--begin::Vendor Stylesheets(used for this page only)-->
    <link href="../assets/plugins/custom/fullcalendar/fullcalendar.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../assets/plugins/custom/datatables/datatables.bundle.css" rel="stylesheet" type="text/css" />
    <!--end::Vendor Stylesheets-->
    <!--begin::Global Stylesheets Bundle(mandatory for all pages)-->
    <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/style.bundle.css" rel="stylesheet" type="text/css" />
    <!--end::Global Stylesheets Bundle-->
    <script>// Frame-busting to prevent site from being loaded within a frame without permission (click-jacking) if (window.top != window.self) { window.top.location.replace(window.self.location.href); }</script>


        <div>
            <div class="app-main flex-column flex-row-fluid" id="kt_app_main">
                <!--begin::Content wrapper-->
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
                                    <h1 class="page-heading d-flex flex-column justify-content-center text-gray-900 fw-bold fs-3 m-0">Team</h1>
                                    <!--end::Title-->
                                    <!--begin::Breadcrumb-->
                                    <ul class="breadcrumb breadcrumb-separatorless fw-semibold fs-7 my-0">
                                        <!--begin::Item-->
                                        <li class="breadcrumb-item text-muted">
                                            <a href="/app/home.aspx" class="text-muted text-hover-primary">Home</a>
                                        </li>
                                        <!--end::Item-->
                                        <!--begin::Item-->
                                    
                                        <!--end::Item-->
                                        <!--begin::Item-->
                                        <!--end::Item-->
                                        <!--begin::Item-->
                                        <li class="breadcrumb-item">
                                            <span class="bullet bg-gray-500 w-5px h-2px"></span>
                                        </li>
                                        <!--end::Item-->
                                        <!--begin::Item-->
                                        <li class="breadcrumb-item text-muted">Projects</li>
                                        <!--end::Item-->
                                    </ul>
                                    <!--end::Breadcrumb-->
                                </div>
                                <!--end::Page title-->
                                <!--begin::Actions-->
                                <div class="d-flex align-items-center gap-2 gap-lg-3">
</div>
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
                            <!--begin::Navbar-->
                            
                            <!--end::Navbar-->
                            <!--begin::Toolbar-->
                            <div class="d-flex flex-wrap flex-stack pb-7">
                                <!--begin::Title-->
                                <div class="d-flex flex-wrap align-items-center my-1">
                                    <h3 class="fw-bold me-5 my-1">Members</h3>
                                    <!--begin::Search-->
                                  
                                    <!--end::Search-->
                                </div>
                                <!--end::Title-->
                                <!--begin::Controls-->
                                
                                <!--end::Controls-->
                            </div>
                            <!--end::Toolbar-->
                            <!--begin::Tab Content-->
                          <div class="tab-content">
    <!-- Card View Pane -->
    <div id="kt_project_users_card_pane" class="tab-pane fade show active" role="tabpanel">
        <div class="row g-6 g-xl-9" id="userCards" runat="server">
            <!-- User cards will be populated here -->
        </div>
    </div>
    
    <!-- Table View Pane -->
    <div id="kt_project_users_table_pane" class="tab-pane fade" role="tabpanel">
        <div class="card card-flush">
            <div class="card-body pt-0">
                <div class="table-responsive">
                    <table id="kt_project_users_table" class="table table-row-bordered table-row-dashed gy-4 align-middle fw-bold dataTable no-footer">
                        <thead class="fs-7 text-gray-500 text-uppercase">
                            <tr>
                                <th class="min-w-250px">Name</th>
                                <th class="min-w-150px">Tasks</th>
                                <th class="min-w-90px">Event Ticket Sales</th>
                                <th class="min-w-90px">Donations Raised</th>
                                <th class="text-end">Details</th>
                            </tr>
                        </thead>
                        <tbody class="fs-6" id="userTableBody" runat="server">
                            <!-- User table rows will be populated here -->
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <!-- Pagination Controls Container -->
    <div id="paginationControlsContainer" runat="server">
        <!-- Pagination controls will be populated here -->
    </div>
</div>


                            <!--end::Tab pane-->
                        </div>
                        <!--end::Tab Content-->
                        <!--begin::Modals-->
                        <!--begin::Modal - View Users-->
                        <div class="modal fade" id="kt_modal_view_users" tabindex="-1" aria-hidden="true">
                            <!--begin::Modal dialog-->
                            <div class="modal-dialog mw-650px">
                                <!--begin::Modal content-->
                                <div class="modal-content">
                                    <!--begin::Modal header-->
                                    <div class="modal-header pb-0 border-0 justify-content-end">
                                        <!--begin::Close-->
                                        <div class="btn btn-sm btn-icon btn-active-color-primary" data-bs-dismiss="modal">
                                            <i class="ki-outline ki-cross fs-1"></i>
                                        </div>
                                        <!--end::Close-->
                                    </div>
                                    <!--begin::Modal header-->
                                    <!--begin::Modal body-->
                                    <div class="modal-body scroll-y mx-5 mx-xl-18 pt-0 pb-15">
                                        <!--begin::Heading-->
                                        <div class="text-center mb-13">
                                            <!--begin::Title-->
                                            <h1 class="mb-3">Browse Users</h1>
                                            <!--end::Title-->
                                            <!--begin::Description-->
                                            <div class="text-muted fw-semibold fs-5">
                                                If you need more info, please check out our 
														<a href="#" class="link-primary fw-bold">Users Directory</a>.
                                            </div>
                                            <!--end::Description-->
                                        </div>
                                        <!--end::Heading-->
                                        <!--begin::Users-->
                                        <div class="mb-15">
                                            <!--begin::List-->
                                            <div class="mh-375px scroll-y me-n7 pe-7">
                                                <!--begin::User-->
                                                <div class="d-flex flex-stack py-5 border-bottom border-gray-300 border-bottom-dashed">
                                                    <!--begin::Details-->
                                                    <div class="d-flex align-items-center">
                                                        <!--begin::Avatar-->
                                                        <div class="symbol symbol-35px symbol-circle">
                                                            <img alt="Pic" src="../assets/media/avatars/300-6.jpg">
                                                        </div>
                                                        <!--end::Avatar-->
                                                        <!--begin::Details-->
                                                        <div class="ms-6">
                                                            <!--begin::Name-->
                                                            <a href="#" class="d-flex align-items-center fs-5 fw-bold text-gray-900 text-hover-primary">Emma Smith 
																		<span class="badge badge-light fs-8 fw-semibold ms-2">Art Director</span></a>
                                                            <!--end::Name-->
                                                            <!--begin::Email-->
                                                            <div class="fw-semibold text-muted">smith@kpmg.com</div>
                                                            <!--end::Email-->
                                                        </div>
                                                        <!--end::Details-->
                                                    </div>
                                                    <!--end::Details-->
                                                    <!--begin::Stats-->
                                                    <div class="d-flex">
                                                        <!--begin::Sales-->
                                                        <div class="text-end">
                                                            <div class="fs-5 fw-bold text-gray-900">$23,000</div>
                                                            <div class="fs-7 text-muted">Sales</div>
                                                        </div>
                                                        <!--end::Sales-->
                                                    </div>
                                                    <!--end::Stats-->
                                                </div>
                                                <!--end::User-->
                                                <!--begin::User-->
                                                <div class="d-flex flex-stack py-5 border-bottom border-gray-300 border-bottom-dashed">
                                                    <!--begin::Details-->
                                                    <div class="d-flex align-items-center">
                                                        <!--begin::Avatar-->
                                                        <div class="symbol symbol-35px symbol-circle">
                                                            <span class="symbol-label bg-light-danger text-danger fw-semibold">M</span>
                                                        </div>
                                                        <!--end::Avatar-->
                                                        <!--begin::Details-->
                                                        <div class="ms-6">
                                                            <!--begin::Name-->
                                                            <a href="#" class="d-flex align-items-center fs-5 fw-bold text-gray-900 text-hover-primary">Melody Macy 
																		<span class="badge badge-light fs-8 fw-semibold ms-2">Marketing Analytic</span></a>
                                                            <!--end::Name-->
                                                            <!--begin::Email-->
                                                            <div class="fw-semibold text-muted">melody@altbox.com</div>
                                                            <!--end::Email-->
                                                        </div>
                                                        <!--end::Details-->
                                                    </div>
                                                    <!--end::Details-->
                                                    <!--begin::Stats-->
                                                    <div class="d-flex">
                                                        <!--begin::Sales-->
                                                        <div class="text-end">
                                                            <div class="fs-5 fw-bold text-gray-900">$50,500</div>
                                                            <div class="fs-7 text-muted">Sales</div>
                                                        </div>
                                                        <!--end::Sales-->
                                                    </div>
                                                    <!--end::Stats-->
                                                </div>
                                                <!--end::User-->
                                                <!--begin::User-->
                                                <div class="d-flex flex-stack py-5 border-bottom border-gray-300 border-bottom-dashed">
                                                    <!--begin::Details-->
                                                    <div class="d-flex align-items-center">
                                                        <!--begin::Avatar-->
                                                        <div class="symbol symbol-35px symbol-circle">
                                                            <img alt="Pic" src="../assets/media/avatars/300-1.jpg">
                                                        </div>
                                                        <!--end::Avatar-->
                                                        <!--begin::Details-->
                                                        <div class="ms-6">
                                                            <!--begin::Name-->
                                                            <a href="#" class="d-flex align-items-center fs-5 fw-bold text-gray-900 text-hover-primary">Max Smith 
																		<span class="badge badge-light fs-8 fw-semibold ms-2">Software Enginer</span></a>
                                                            <!--end::Name-->
                                                            <!--begin::Email-->
                                                            <div class="fw-semibold text-muted">max@kt.com</div>
                                                            <!--end::Email-->
                                                        </div>
                                                        <!--end::Details-->
                                                    </div>
                                                    <!--end::Details-->
                                                    <!--begin::Stats-->
                                                    <div class="d-flex">
                                                        <!--begin::Sales-->
                                                        <div class="text-end">
                                                            <div class="fs-5 fw-bold text-gray-900">$75,900</div>
                                                            <div class="fs-7 text-muted">Sales</div>
                                                        </div>
                                                        <!--end::Sales-->
                                                    </div>
                                                    <!--end::Stats-->
                                                </div>
                                                <!--end::User-->
                                                <!--begin::User-->
                                                <div class="d-flex flex-stack py-5 border-bottom border-gray-300 border-bottom-dashed">
                                                    <!--begin::Details-->
                                                    <div class="d-flex align-items-center">
                                                        <!--begin::Avatar-->
                                                        <div class="symbol symbol-35px symbol-circle">
                                                            <img alt="Pic" src="../assets/media/avatars/300-5.jpg">
                                                        </div>
                                                        <!--end::Avatar-->
                                                        <!--begin::Details-->
                                                        <div class="ms-6">
                                                            <!--begin::Name-->
                                                            <a href="#" class="d-flex align-items-center fs-5 fw-bold text-gray-900 text-hover-primary">Sean Bean 
																		<span class="badge badge-light fs-8 fw-semibold ms-2">Web Developer</span></a>
                                                            <!--end::Name-->
                                                            <!--begin::Email-->
                                                            <div class="fw-semibold text-muted">sean@dellito.com</div>
                                                            <!--end::Email-->
                                                        </div>
                                                        <!--end::Details-->
                                                    </div>
                                                    <!--end::Details-->
                                                    <!--begin::Stats-->
                                                    <div class="d-flex">
                                                        <!--begin::Sales-->
                                                        <div class="text-end">
                                                            <div class="fs-5 fw-bold text-gray-900">$10,500</div>
                                                            <div class="fs-7 text-muted">Sales</div>
                                                        </div>
                                                        <!--end::Sales-->
                                                    </div>
                                                    <!--end::Stats-->
                                                </div>
                                                <!--end::User-->
                                                <!--begin::User-->
                                                <div class="d-flex flex-stack py-5 border-bottom border-gray-300 border-bottom-dashed">
                                                    <!--begin::Details-->
                                                    <div class="d-flex align-items-center">
                                                        <!--begin::Avatar-->
                                                        <div class="symbol symbol-35px symbol-circle">
                                                            <img alt="Pic" src="../assets/media/avatars/300-25.jpg">
                                                        </div>
                                                        <!--end::Avatar-->
                                                        <!--begin::Details-->
                                                        <div class="ms-6">
                                                            <!--begin::Name-->
                                                            <a href="#" class="d-flex align-items-center fs-5 fw-bold text-gray-900 text-hover-primary">Brian Cox 
																		<span class="badge badge-light fs-8 fw-semibold ms-2">UI/UX Designer</span></a>
                                                            <!--end::Name-->
                                                            <!--begin::Email-->
                                                            <div class="fw-semibold text-muted">brian@exchange.com</div>
                                                            <!--end::Email-->
                                                        </div>
                                                        <!--end::Details-->
                                                    </div>
                                                    <!--end::Details-->
                                                    <!--begin::Stats-->
                                                    <div class="d-flex">
                                                        <!--begin::Sales-->
                                                        <div class="text-end">
                                                            <div class="fs-5 fw-bold text-gray-900">$20,000</div>
                                                            <div class="fs-7 text-muted">Sales</div>
                                                        </div>
                                                        <!--end::Sales-->
                                                    </div>
                                                    <!--end::Stats-->
                                                </div>
                                                <!--end::User-->
                                                <!--begin::User-->
                                                <div class="d-flex flex-stack py-5 border-bottom border-gray-300 border-bottom-dashed">
                                                    <!--begin::Details-->
                                                    <div class="d-flex align-items-center">
                                                        <!--begin::Avatar-->
                                                        <div class="symbol symbol-35px symbol-circle">
                                                            <span class="symbol-label bg-light-warning text-warning fw-semibold">C</span>
                                                        </div>
                                                        <!--end::Avatar-->
                                                        <!--begin::Details-->
                                                        <div class="ms-6">
                                                            <!--begin::Name-->
                                                            <a href="#" class="d-flex align-items-center fs-5 fw-bold text-gray-900 text-hover-primary">Mikaela Collins 
																		<span class="badge badge-light fs-8 fw-semibold ms-2">Head Of Marketing</span></a>
                                                            <!--end::Name-->
                                                            <!--begin::Email-->
                                                            <div class="fw-semibold text-muted">mik@pex.com</div>
                                                            <!--end::Email-->
                                                        </div>
                                                        <!--end::Details-->
                                                    </div>
                                                    <!--end::Details-->
                                                    <!--begin::Stats-->
                                                    <div class="d-flex">
                                                        <!--begin::Sales-->
                                                        <div class="text-end">
                                                            <div class="fs-5 fw-bold text-gray-900">$9,300</div>
                                                            <div class="fs-7 text-muted">Sales</div>
                                                        </div>
                                                        <!--end::Sales-->
                                                    </div>
                                                    <!--end::Stats-->
                                                </div>
                                                <!--end::User-->
                                                <!--begin::User-->
                                                <div class="d-flex flex-stack py-5 border-bottom border-gray-300 border-bottom-dashed">
                                                    <!--begin::Details-->
                                                    <div class="d-flex align-items-center">
                                                        <!--begin::Avatar-->
                                                        <div class="symbol symbol-35px symbol-circle">
                                                            <img alt="Pic" src="../assets/media/avatars/300-9.jpg">
                                                        </div>
                                                        <!--end::Avatar-->
                                                        <!--begin::Details-->
                                                        <div class="ms-6">
                                                            <!--begin::Name-->
                                                            <a href="#" class="d-flex align-items-center fs-5 fw-bold text-gray-900 text-hover-primary">Francis Mitcham 
																		<span class="badge badge-light fs-8 fw-semibold ms-2">Software Arcitect</span></a>
                                                            <!--end::Name-->
                                                            <!--begin::Email-->
                                                            <div class="fw-semibold text-muted">f.mit@kpmg.com</div>
                                                            <!--end::Email-->
                                                        </div>
                                                        <!--end::Details-->
                                                    </div>
                                                    <!--end::Details-->
                                                    <!--begin::Stats-->
                                                    <div class="d-flex">
                                                        <!--begin::Sales-->
                                                        <div class="text-end">
                                                            <div class="fs-5 fw-bold text-gray-900">$15,000</div>
                                                            <div class="fs-7 text-muted">Sales</div>
                                                        </div>
                                                        <!--end::Sales-->
                                                    </div>
                                                    <!--end::Stats-->
                                                </div>
                                                <!--end::User-->
                                                <!--begin::User-->
                                                <div class="d-flex flex-stack py-5 border-bottom border-gray-300 border-bottom-dashed">
                                                    <!--begin::Details-->
                                                    <div class="d-flex align-items-center">
                                                        <!--begin::Avatar-->
                                                        <div class="symbol symbol-35px symbol-circle">
                                                            <span class="symbol-label bg-light-danger text-danger fw-semibold">O</span>
                                                        </div>
                                                        <!--end::Avatar-->
                                                        <!--begin::Details-->
                                                        <div class="ms-6">
                                                            <!--begin::Name-->
                                                            <a href="#" class="d-flex align-items-center fs-5 fw-bold text-gray-900 text-hover-primary">Olivia Wild 
																		<span class="badge badge-light fs-8 fw-semibold ms-2">System Admin</span></a>
                                                            <!--end::Name-->
                                                            <!--begin::Email-->
                                                            <div class="fw-semibold text-muted">olivia@corpmail.com</div>
                                                            <!--end::Email-->
                                                        </div>
                                                        <!--end::Details-->
                                                    </div>
                                                    <!--end::Details-->
                                                    <!--begin::Stats-->
                                                    <div class="d-flex">
                                                        <!--begin::Sales-->
                                                        <div class="text-end">
                                                            <div class="fs-5 fw-bold text-gray-900">$23,000</div>
                                                            <div class="fs-7 text-muted">Sales</div>
                                                        </div>
                                                        <!--end::Sales-->
                                                    </div>
                                                    <!--end::Stats-->
                                                </div>
                                                <!--end::User-->
                                                <!--begin::User-->
                                                <div class="d-flex flex-stack py-5 border-bottom border-gray-300 border-bottom-dashed">
                                                    <!--begin::Details-->
                                                    <div class="d-flex align-items-center">
                                                        <!--begin::Avatar-->
                                                        <div class="symbol symbol-35px symbol-circle">
                                                            <span class="symbol-label bg-light-primary text-primary fw-semibold">N</span>
                                                        </div>
                                                        <!--end::Avatar-->
                                                        <!--begin::Details-->
                                                        <div class="ms-6">
                                                            <!--begin::Name-->
                                                            <a href="#" class="d-flex align-items-center fs-5 fw-bold text-gray-900 text-hover-primary">Neil Owen 
																		<span class="badge badge-light fs-8 fw-semibold ms-2">Account Manager</span></a>
                                                            <!--end::Name-->
                                                            <!--begin::Email-->
                                                            <div class="fw-semibold text-muted">owen.neil@gmail.com</div>
                                                            <!--end::Email-->
                                                        </div>
                                                        <!--end::Details-->
                                                    </div>
                                                    <!--end::Details-->
                                                    <!--begin::Stats-->
                                                    <div class="d-flex">
                                                        <!--begin::Sales-->
                                                        <div class="text-end">
                                                            <div class="fs-5 fw-bold text-gray-900">$45,800</div>
                                                            <div class="fs-7 text-muted">Sales</div>
                                                        </div>
                                                        <!--end::Sales-->
                                                    </div>
                                                    <!--end::Stats-->
                                                </div>
                                                <!--end::User-->
                                                <!--begin::User-->
                                                <div class="d-flex flex-stack py-5 border-bottom border-gray-300 border-bottom-dashed">
                                                    <!--begin::Details-->
                                                    <div class="d-flex align-items-center">
                                                        <!--begin::Avatar-->
                                                        <div class="symbol symbol-35px symbol-circle">
                                                            <img alt="Pic" src="../assets/media/avatars/300-23.jpg">
                                                        </div>
                                                        <!--end::Avatar-->
                                                        <!--begin::Details-->
                                                        <div class="ms-6">
                                                            <!--begin::Name-->
                                                            <a href="#" class="d-flex align-items-center fs-5 fw-bold text-gray-900 text-hover-primary">Dan Wilson 
																		<span class="badge badge-light fs-8 fw-semibold ms-2">Web Desinger</span></a>
                                                            <!--end::Name-->
                                                            <!--begin::Email-->
                                                            <div class="fw-semibold text-muted">dam@consilting.com</div>
                                                            <!--end::Email-->
                                                        </div>
                                                        <!--end::Details-->
                                                    </div>
                                                    <!--end::Details-->
                                                    <!--begin::Stats-->
                                                    <div class="d-flex">
                                                        <!--begin::Sales-->
                                                        <div class="text-end">
                                                            <div class="fs-5 fw-bold text-gray-900">$90,500</div>
                                                            <div class="fs-7 text-muted">Sales</div>
                                                        </div>
                                                        <!--end::Sales-->
                                                    </div>
                                                    <!--end::Stats-->
                                                </div>
                                                <!--end::User-->
                                                <!--begin::User-->
                                                <!--end::User-->
                                                <!--begin::User-->
                                                <!--end::User-->
                                                <!--begin::User-->
                                                <!--end::User-->
                                            </div>
                                            <!--end::List-->
                                        </div>
                                        <!--end::Users-->
                                        <!--begin::Notice-->
                                        <div class="d-flex justify-content-between">
                                            <!--begin::Label-->
                                            <div class="fw-semibold">
                                                <label class="fs-6">Adding Users by Team Members</label>
                                                <div class="fs-7 text-muted">If you need more info, please check budget planning</div>
                                            </div>
                                            <!--end::Label-->
                                            <!--begin::Switch-->
                                            <label class="form-check form-switch form-check-custom form-check-solid">
                                                <input class="form-check-input" type="checkbox" value="" checked="checked">
                                                <span class="form-check-label fw-semibold text-muted">Allowed</span>
                                            </label>
                                            <!--end::Switch-->
                                        </div>
                                        <!--end::Notice-->
                                    </div>
                                    <!--end::Modal body-->
                                </div>
                                <!--end::Modal content-->
                            </div>
                            <!--end::Modal dialog-->
                        </div>
                        <!--end::Modal - View Users-->
                        <!--begin::Modal - Users Search-->
                        <div class="modal fade" id="kt_modal_users_search" tabindex="-1" aria-hidden="true">
                            <!--begin::Modal dialog-->
                            <div class="modal-dialog modal-dialog-centered mw-650px">
                                <!--begin::Modal content-->
                                <div class="modal-content">
                                    <!--begin::Modal header-->
                                    <div class="modal-header pb-0 border-0 justify-content-end">
                                        <!--begin::Close-->
                                        <div class="btn btn-sm btn-icon btn-active-color-primary" data-bs-dismiss="modal">
                                            <i class="ki-outline ki-cross fs-1"></i>
                                        </div>
                                        <!--end::Close-->
                                    </div>
                                    <!--begin::Modal header-->
                                    <!--begin::Modal body-->
                                    <div class="modal-body scroll-y mx-5 mx-xl-18 pt-0 pb-15">
                                        <!--begin::Content-->
                                        <div class="text-center mb-13">
                                            <h1 class="mb-3">Search Users</h1>
                                            <div class="text-muted fw-semibold fs-5">Invite Collaborators To Your Project</div>
                                        </div>
                                        <!--end::Content-->
                                        <!--begin::Search-->
                                        <div id="kt_modal_users_search_handler" data-kt-search-keypress="true" data-kt-search-min-length="2" data-kt-search-enter="enter" data-kt-search-layout="inline" data-kt-search="true">
                                            <!--begin::Form-->
                                            <form data-kt-search-element="form" class="w-100 position-relative mb-5" autocomplete="off">
                                                <!--begin::Hidden input(Added to disable form autocomplete)-->
                                                <input type="hidden">
                                                <!--end::Hidden input-->
                                                <!--begin::Icon-->
                                                <i class="ki-outline ki-magnifier fs-2 fs-lg-1 text-gray-500 position-absolute top-50 ms-5 translate-middle-y"></i>
                                                <!--end::Icon-->
                                                <!--begin::Input-->
                                                <input type="text" class="form-control form-control-lg form-control-solid px-15" name="search" value="" placeholder="Search by username, full name or email..." data-kt-search-element="input">
                                                <!--end::Input-->
                                                <!--begin::Spinner-->
                                                <span class="position-absolute top-50 end-0 translate-middle-y lh-0 d-none me-5" data-kt-search-element="spinner">
                                                    <span class="spinner-border h-15px w-15px align-middle text-muted"></span>
                                                </span>
                                                <!--end::Spinner-->
                                                <!--begin::Reset-->
                                                <span class="btn btn-flush btn-active-color-primary position-absolute top-50 end-0 translate-middle-y lh-0 me-5 d-none" data-kt-search-element="clear">
                                                    <i class="ki-outline ki-cross fs-2 fs-lg-1 me-0"></i>
                                                </span>
                                                <!--end::Reset-->
                                            <!--end::Form-->
                                            <!--begin::Wrapper-->
                                            <div class="py-5">
                                                <!--begin::Suggestions-->
                                                <div data-kt-search-element="suggestions">
                                                    <!--begin::Heading-->
                                                    <h3 class="fw-semibold mb-5">Recently searched:</h3>
                                                    <!--end::Heading-->
                                                    <!--begin::Users-->
                                                    <div class="mh-375px scroll-y me-n7 pe-7">
                                                        <!--begin::User-->
                                                        <a href="#" class="d-flex align-items-center p-3 rounded bg-state-light bg-state-opacity-50 mb-1">
                                                            <!--begin::Avatar-->
                                                            <div class="symbol symbol-35px symbol-circle me-5">
                                                                <img alt="Pic" src="../assets/media/avatars/300-6.jpg">
                                                            </div>
                                                            <!--end::Avatar-->
                                                            <!--begin::Info-->
                                                            <div class="fw-semibold">
                                                                <span class="fs-6 text-gray-800 me-2">Emma Smith</span>
                                                                <span class="badge badge-light">Art Director</span>
                                                            </div>
                                                            <!--end::Info-->
                                                        </a>
                                                        <!--end::User-->
                                                        <!--begin::User-->
                                                        <a href="#" class="d-flex align-items-center p-3 rounded bg-state-light bg-state-opacity-50 mb-1">
                                                            <!--begin::Avatar-->
                                                            <div class="symbol symbol-35px symbol-circle me-5">
                                                                <span class="symbol-label bg-light-danger text-danger fw-semibold">M</span>
                                                            </div>
                                                            <!--end::Avatar-->
                                                            <!--begin::Info-->
                                                            <div class="fw-semibold">
                                                                <span class="fs-6 text-gray-800 me-2">Melody Macy</span>
                                                                <span class="badge badge-light">Marketing Analytic</span>
                                                            </div>
                                                            <!--end::Info-->
                                                        </a>
                                                        <!--end::User-->
                                                        <!--begin::User-->
                                                        <a href="#" class="d-flex align-items-center p-3 rounded bg-state-light bg-state-opacity-50 mb-1">
                                                            <!--begin::Avatar-->
                                                            <div class="symbol symbol-35px symbol-circle me-5">
                                                                <img alt="Pic" src="../assets/media/avatars/300-1.jpg">
                                                            </div>
                                                            <!--end::Avatar-->
                                                            <!--begin::Info-->
                                                            <div class="fw-semibold">
                                                                <span class="fs-6 text-gray-800 me-2">Max Smith</span>
                                                                <span class="badge badge-light">Software Enginer</span>
                                                            </div>
                                                            <!--end::Info-->
                                                        </a>
                                                        <!--end::User-->
                                                        <!--begin::User-->
                                                        <a href="#" class="d-flex align-items-center p-3 rounded bg-state-light bg-state-opacity-50 mb-1">
                                                            <!--begin::Avatar-->
                                                            <div class="symbol symbol-35px symbol-circle me-5">
                                                                <img alt="Pic" src="../assets/media/avatars/300-5.jpg">
                                                            </div>
                                                            <!--end::Avatar-->
                                                            <!--begin::Info-->
                                                            <div class="fw-semibold">
                                                                <span class="fs-6 text-gray-800 me-2">Sean Bean</span>
                                                                <span class="badge badge-light">Web Developer</span>
                                                            </div>
                                                            <!--end::Info-->
                                                        </a>
                                                        <!--end::User-->
                                                        <!--begin::User-->
                                                        <a href="#" class="d-flex align-items-center p-3 rounded bg-state-light bg-state-opacity-50 mb-1">
                                                            <!--begin::Avatar-->
                                                            <div class="symbol symbol-35px symbol-circle me-5">
                                                                <img alt="Pic" src="../assets/media/avatars/300-25.jpg">
                                                            </div>
                                                            <!--end::Avatar-->
                                                            <!--begin::Info-->
                                                            <div class="fw-semibold">
                                                                <span class="fs-6 text-gray-800 me-2">Brian Cox</span>
                                                                <span class="badge badge-light">UI/UX Designer</span>
                                                            </div>
                                                            <!--end::Info-->
                                                        </a>
                                                        <!--end::User-->
                                                    </div>
                                                    <!--end::Users-->
                                                </div>
                                                <!--end::Suggestions-->
                                                <!--begin::Results(add d-none to below element to hide the users list by default)-->
                                                <div data-kt-search-element="results" class="d-none">
                                                    <!--begin::Users-->
                                                    <div class="mh-375px scroll-y me-n7 pe-7">
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="0">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='0']" value="0">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <img alt="Pic" src="../assets/media/avatars/300-6.jpg">
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Emma Smith</a>
                                                                    <div class="fw-semibold text-muted">smith@kpmg.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-13-aotr" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1">Guest</option>
                                                                    <option value="2" selected="selected" data-select2-id="select2-data-15-oi4g">Owner</option>
                                                                    <option value="3">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-14-s9x6" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-3251-container" aria-controls="select2-3251-container"><span class="select2-selection__rendered" id="select2-3251-container" role="textbox" aria-readonly="true" title="Owner">Owner</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="1">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='1']" value="1">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <span class="symbol-label bg-light-danger text-danger fw-semibold">M</span>
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Melody Macy</a>
                                                                    <div class="fw-semibold text-muted">melody@altbox.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-16-joao" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1" selected="selected" data-select2-id="select2-data-18-kmfs">Guest</option>
                                                                    <option value="2">Owner</option>
                                                                    <option value="3">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-17-b64k" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-72pm-container" aria-controls="select2-72pm-container"><span class="select2-selection__rendered" id="select2-72pm-container" role="textbox" aria-readonly="true" title="Guest">Guest</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="2">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='2']" value="2">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <img alt="Pic" src="../assets/media/avatars/300-1.jpg">
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Max Smith</a>
                                                                    <div class="fw-semibold text-muted">max@kt.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-19-erli" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1">Guest</option>
                                                                    <option value="2">Owner</option>
                                                                    <option value="3" selected="selected" data-select2-id="select2-data-21-6j5s">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-20-5l4y" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-4q7f-container" aria-controls="select2-4q7f-container"><span class="select2-selection__rendered" id="select2-4q7f-container" role="textbox" aria-readonly="true" title="Can Edit">Can Edit</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="3">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='3']" value="3">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <img alt="Pic" src="../assets/media/avatars/300-5.jpg">
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Sean Bean</a>
                                                                    <div class="fw-semibold text-muted">sean@dellito.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-22-vyea" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1">Guest</option>
                                                                    <option value="2" selected="selected" data-select2-id="select2-data-24-h95r">Owner</option>
                                                                    <option value="3">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-23-jlg9" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-r58r-container" aria-controls="select2-r58r-container"><span class="select2-selection__rendered" id="select2-r58r-container" role="textbox" aria-readonly="true" title="Owner">Owner</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="4">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='4']" value="4">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <img alt="Pic" src="../assets/media/avatars/300-25.jpg">
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Brian Cox</a>
                                                                    <div class="fw-semibold text-muted">brian@exchange.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-25-ixz4" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1">Guest</option>
                                                                    <option value="2">Owner</option>
                                                                    <option value="3" selected="selected" data-select2-id="select2-data-27-j7gj">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-26-nd9e" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-a7u7-container" aria-controls="select2-a7u7-container"><span class="select2-selection__rendered" id="select2-a7u7-container" role="textbox" aria-readonly="true" title="Can Edit">Can Edit</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="5">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='5']" value="5">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <span class="symbol-label bg-light-warning text-warning fw-semibold">C</span>
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Mikaela Collins</a>
                                                                    <div class="fw-semibold text-muted">mik@pex.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-28-j23s" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1">Guest</option>
                                                                    <option value="2" selected="selected" data-select2-id="select2-data-30-8hyq">Owner</option>
                                                                    <option value="3">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-29-jrfj" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-n4xt-container" aria-controls="select2-n4xt-container"><span class="select2-selection__rendered" id="select2-n4xt-container" role="textbox" aria-readonly="true" title="Owner">Owner</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="6">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='6']" value="6">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <img alt="Pic" src="../assets/media/avatars/300-9.jpg">
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Francis Mitcham</a>
                                                                    <div class="fw-semibold text-muted">f.mit@kpmg.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-31-6ybi" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1">Guest</option>
                                                                    <option value="2">Owner</option>
                                                                    <option value="3" selected="selected" data-select2-id="select2-data-33-y1o0">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-32-fjmw" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-pm8s-container" aria-controls="select2-pm8s-container"><span class="select2-selection__rendered" id="select2-pm8s-container" role="textbox" aria-readonly="true" title="Can Edit">Can Edit</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="7">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='7']" value="7">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <span class="symbol-label bg-light-danger text-danger fw-semibold">O</span>
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Olivia Wild</a>
                                                                    <div class="fw-semibold text-muted">olivia@corpmail.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-34-u7as" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1">Guest</option>
                                                                    <option value="2" selected="selected" data-select2-id="select2-data-36-92ue">Owner</option>
                                                                    <option value="3">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-35-rkzn" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-1eg4-container" aria-controls="select2-1eg4-container"><span class="select2-selection__rendered" id="select2-1eg4-container" role="textbox" aria-readonly="true" title="Owner">Owner</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="8">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='8']" value="8">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <span class="symbol-label bg-light-primary text-primary fw-semibold">N</span>
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Neil Owen</a>
                                                                    <div class="fw-semibold text-muted">owen.neil@gmail.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-37-vrec" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1" selected="selected" data-select2-id="select2-data-39-h2bu">Guest</option>
                                                                    <option value="2">Owner</option>
                                                                    <option value="3">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-38-kc8b" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-386w-container" aria-controls="select2-386w-container"><span class="select2-selection__rendered" id="select2-386w-container" role="textbox" aria-readonly="true" title="Guest">Guest</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="9">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='9']" value="9">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <img alt="Pic" src="../assets/media/avatars/300-23.jpg">
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Dan Wilson</a>
                                                                    <div class="fw-semibold text-muted">dam@consilting.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-40-uajb" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1">Guest</option>
                                                                    <option value="2">Owner</option>
                                                                    <option value="3" selected="selected" data-select2-id="select2-data-42-4scl">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-41-usum" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-mj6j-container" aria-controls="select2-mj6j-container"><span class="select2-selection__rendered" id="select2-mj6j-container" role="textbox" aria-readonly="true" title="Can Edit">Can Edit</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="10">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='10']" value="10">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <span class="symbol-label bg-light-danger text-danger fw-semibold">E</span>
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Emma Bold</a>
                                                                    <div class="fw-semibold text-muted">emma@intenso.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-43-3qsc" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1">Guest</option>
                                                                    <option value="2" selected="selected" data-select2-id="select2-data-45-jm22">Owner</option>
                                                                    <option value="3">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-44-gpdv" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-wfm6-container" aria-controls="select2-wfm6-container"><span class="select2-selection__rendered" id="select2-wfm6-container" role="textbox" aria-readonly="true" title="Owner">Owner</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="11">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='11']" value="11">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <img alt="Pic" src="../assets/media/avatars/300-12.jpg">
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Ana Crown</a>
                                                                    <div class="fw-semibold text-muted">ana.cf@limtel.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-46-5ab5" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1" selected="selected" data-select2-id="select2-data-48-j45e">Guest</option>
                                                                    <option value="2">Owner</option>
                                                                    <option value="3">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-47-50qs" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-6221-container" aria-controls="select2-6221-container"><span class="select2-selection__rendered" id="select2-6221-container" role="textbox" aria-readonly="true" title="Guest">Guest</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="12">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='12']" value="12">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <span class="symbol-label bg-light-info text-info fw-semibold">A</span>
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Robert Doe</a>
                                                                    <div class="fw-semibold text-muted">robert@benko.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-49-j9cx" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1">Guest</option>
                                                                    <option value="2">Owner</option>
                                                                    <option value="3" selected="selected" data-select2-id="select2-data-51-jpcd">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-50-h79q" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-b7eu-container" aria-controls="select2-b7eu-container"><span class="select2-selection__rendered" id="select2-b7eu-container" role="textbox" aria-readonly="true" title="Can Edit">Can Edit</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="13">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='13']" value="13">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <img alt="Pic" src="../assets/media/avatars/300-13.jpg">
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">John Miller</a>
                                                                    <div class="fw-semibold text-muted">miller@mapple.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-52-vwfq" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1">Guest</option>
                                                                    <option value="2">Owner</option>
                                                                    <option value="3" selected="selected" data-select2-id="select2-data-54-1uo1">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-53-2uyj" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-t56y-container" aria-controls="select2-t56y-container"><span class="select2-selection__rendered" id="select2-t56y-container" role="textbox" aria-readonly="true" title="Can Edit">Can Edit</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="14">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='14']" value="14">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <span class="symbol-label bg-light-success text-success fw-semibold">L</span>
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Lucy Kunic</a>
                                                                    <div class="fw-semibold text-muted">lucy.m@fentech.com</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-55-aijq" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1">Guest</option>
                                                                    <option value="2" selected="selected" data-select2-id="select2-data-57-ha31">Owner</option>
                                                                    <option value="3">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-56-z296" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-3n2y-container" aria-controls="select2-3n2y-container"><span class="select2-selection__rendered" id="select2-3n2y-container" role="textbox" aria-readonly="true" title="Owner">Owner</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="15">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='15']" value="15">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <img alt="Pic" src="../assets/media/avatars/300-21.jpg">
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Ethan Wilder</a>
                                                                    <div class="fw-semibold text-muted">ethan@loop.com.au</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-58-4wvy" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1" selected="selected" data-select2-id="select2-data-60-42w4">Guest</option>
                                                                    <option value="2">Owner</option>
                                                                    <option value="3">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-59-s4r7" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-29zg-container" aria-controls="select2-29zg-container"><span class="select2-selection__rendered" id="select2-29zg-container" role="textbox" aria-readonly="true" title="Guest">Guest</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                        <!--begin::Separator-->
                                                        <div class="border-bottom border-gray-300 border-bottom-dashed"></div>
                                                        <!--end::Separator-->
                                                        <!--begin::User-->
                                                        <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="16">
                                                            <!--begin::Details-->
                                                            <div class="d-flex align-items-center">
                                                                <!--begin::Checkbox-->
                                                                <label class="form-check form-check-custom form-check-solid me-5">
                                                                    <input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='16']" value="16">
                                                                </label>
                                                                <!--end::Checkbox-->
                                                                <!--begin::Avatar-->
                                                                <div class="symbol symbol-35px symbol-circle">
                                                                    <img alt="Pic" src="../assets/media/avatars/300-21.jpg">
                                                                </div>
                                                                <!--end::Avatar-->
                                                                <!--begin::Details-->
                                                                <div class="ms-5">
                                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">Ethan Wilder</a>
                                                                    <div class="fw-semibold text-muted">ethan@loop.com.au</div>
                                                                </div>
                                                                <!--end::Details-->
                                                            </div>
                                                            <!--end::Details-->
                                                            <!--begin::Access menu-->
                                                            <div class="ms-2 w-100px">
                                                                <select class="form-select form-select-solid form-select-sm select2-hidden-accessible" data-control="select2" data-hide-search="true" data-select2-id="select2-data-61-g184" tabindex="-1" aria-hidden="true" data-kt-initialized="1">
                                                                    <option value="1">Guest</option>
                                                                    <option value="2">Owner</option>
                                                                    <option value="3" selected="selected" data-select2-id="select2-data-63-htap">Can Edit</option>
                                                                </select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-62-lij4" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid form-select-sm" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-zcli-container" aria-controls="select2-zcli-container"><span class="select2-selection__rendered" id="select2-zcli-container" role="textbox" aria-readonly="true" title="Can Edit">Can Edit</span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                                            </div>
                                                            <!--end::Access menu-->
                                                        </div>
                                                        <!--end::User-->
                                                    </div>
                                                    <!--end::Users-->
                                                    <!--begin::Actions-->
                                                    <div class="d-flex flex-center mt-15">
                                                        <button type="reset" id="kt_modal_users_search_reset" data-bs-dismiss="modal" class="btn btn-active-light me-3">Cancel</button>
                                                        <button type="submit" id="kt_modal_users_search_submit" class="btn btn-primary">Add Selected Users</button>
                                                    </div>
                                                    <!--end::Actions-->
                                                </div>
                                                <!--end::Results-->
                                                <!--begin::Empty-->
                                                <div data-kt-search-element="empty" class="text-center d-none">
                                                    <!--begin::Message-->
                                                    <div class="fw-semibold py-10">
                                                        <div class="text-gray-600 fs-3 mb-2">No users found</div>
                                                        <div class="text-muted fs-6">Try to search by username, full name or email...</div>
                                                    </div>
                                                    <!--end::Message-->
                                                    <!--begin::Illustration-->
                                                    <div class="text-center px-5">
                                                        <img src="../assets/media/illustrations/sketchy-1/1.png" alt="" class="w-100 h-200px h-sm-325px">
                                                    </div>
                                                    <!--end::Illustration-->
                                                </div>
                                                <!--end::Empty-->
                                            </div>
                                            <!--end::Wrapper-->
                                        </div>
                                        <!--end::Search-->
                                    </div>
                                    <!--end::Modal body-->
                                </div>
                                <!--end::Modal content-->
                            </div>
                            <!--end::Modal dialog-->
                        </div>
                        <!--end::Modal - Users Search-->
                        <!--end::Modals-->
                    </div>

                    <!--end::Content-->
                </div>
                <!--end::Content wrapper-->
                <!--begin::Footer-->
                <div id="kt_app_footer" class="app-footer">
                    <!--begin::Footer container-->


                </div>
                <!--end::Footer-->
            </div>
        </div>
    <!--end::Modal - Invite Friend-->
    <!--end::Modals-->
    <!--begin::Javascript-->
    <script>var hostUrl = "../assets/";</script>
    <!--begin::Global Javascript Bundle(mandatory for all pages)-->
    <script src="../assets/plugins/global/plugins.bundle.js"></script>
    <script src="../assets/js/scripts.bundle.js"></script>
    <!--end::Global Javascript Bundle-->
    <!--begin::Vendors Javascript(used for this page only)-->
    <script src="../assets/plugins/custom/fullcalendar/fullcalendar.bundle.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/index.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/xy.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/percent.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/radar.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/themes/Animated.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/map.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/geodata/worldLow.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/geodata/continentsLow.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/geodata/usaLow.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/geodata/worldTimeZonesLow.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/geodata/worldTimeZoneAreasLow.js"></script>
    <script src="../assets/plugins/custom/datatables/datatables.bundle.js"></script>
    <!--end::Vendors Javascript-->
    <!--begin::Custom Javascript(used for this page only)-->
    <script src="../assets/js/widgets.bundle.js"></script>
    <script src="../assets/js/custom/widgets.js"></script>
    <script src="../assets/js/custom/apps/chat/chat.js"></script>
    <script src="../assets/js/custom/utilities/modals/upgrade-plan.js"></script>
    <script src="../assets/js/custom/utilities/modals/users-search.js"></script>
    <!--end::Custom Javascript-->
    <!--end::Javascript-->
    </asp:Content>