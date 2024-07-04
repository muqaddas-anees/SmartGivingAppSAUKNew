<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Permissions.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base href="../../" />
		<title>Metronic - The World's #1 Selling Bootstrap Admin Template by KeenThemes</title>
		<meta charset="utf-8" />
		<meta name="description" content="The most advanced Bootstrap 5 Admin Theme with 40 unique prebuilt layouts on Themeforest trusted by 100,000 beginners and professionals. Multi-demo, Dark Mode, RTL support and complete React, Angular, Vue, Asp.Net Core, Rails, Spring, Blazor, Django, Express.js, Node.js, Flask, Symfony & Laravel versions. Grab your copy now and get life-time updates for free." />
		<meta name="keywords" content="metronic, bootstrap, bootstrap 5, angular, VueJs, React, Asp.Net Core, Rails, Spring, Blazor, Django, Express.js, Node.js, Flask, Symfony & Laravel starter kits, admin themes, web design, figma, web development, free templates, free admin themes, bootstrap theme, bootstrap template, bootstrap dashboard, bootstrap dak mode, bootstrap button, bootstrap datepicker, bootstrap timepicker, fullcalendar, datatables, flaticon" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<meta property="og:locale" content="en_US" />
		<meta property="og:type" content="article" />
		<meta property="og:title" content="Metronic - The World's #1 Selling Bootstrap Admin Template by KeenThemes" />
		<meta property="og:url" content="https://keenthemes.com/metronic" />
		<meta property="og:site_name" content="Metronic by Keenthemes" />
		<link rel="canonical" href="https://preview.keenthemes.com/metronic8" />
		<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />
		<!--begin::Fonts(mandatory for all pages)-->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Inter:300,400,500,600,700" />
		<!--end::Fonts-->
		<!--begin::Vendor Stylesheets(used for this page only)-->
		<link href="assets/plugins/custom/datatables/datatables.bundle.css" rel="stylesheet" type="text/css" />
		<!--end::Vendor Stylesheets-->
		<!--begin::Global Stylesheets Bundle(mandatory for all pages)-->
		<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />
		<!--end::Global Stylesheets Bundle-->
	 <style>
        /* Global styles here */
        body {
            font-family: 'Inter', sans-serif;
            background-color: #f8f9fa;
        }

        .card {
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            background-color: #ffffff;
        }

        .card-header {
            background-color: #f0f2f5;
            padding: 20px;
            border-bottom: 1px solid #dee2e6;
            border-top-left-radius: 10px;
            border-top-right-radius: 10px;
        }

        .card-title {
            font-size: 1.25rem;
            font-weight: bold;
        }

        .card-toolbar {
            text-align: right;
        }

        .btn-light-primary {
            background-color: #6c757d;
            color: #ffffff;
        }
        
        .btn-light-primary:hover {
            background-color: #495057;
            color: #ffffff;
        }
		 /* Darken table rows */
    .table-row-dark {
        color: #555 !important; /* Adjust the color to your preference */
    }
    </style>
		
</head>
<body>
   <div id="kt_app_content_container" class="app-container container-fluid">
									<!--begin::Card-->
									<div class="card card-flush">
										<!--begin::Card header-->
										<div class="card-header mt-6">
											<!--begin::Card title-->
											<div class="card-title">
												<!--begin::Search-->
												<div class="d-flex align-items-center position-relative my-1 me-5">
													<i class="ki-outline ki-magnifier fs-3 position-absolute ms-5"></i>
													<input type="text" data-kt-permissions-table-filter="search" class="form-control form-control-solid w-250px ps-13" placeholder="Search Permissions">
												</div>
												<!--end::Search-->
											</div>
											<!--end::Card title-->
											<!--begin::Card toolbar-->
											<div class="card-toolbar">
												<!--begin::Button-->
												<button type="button" class="btn btn-light-primary" data-bs-toggle="modal" data-bs-target="#kt_modal_add_permission">
												<i class="ki-outline ki-plus-square fs-3"></i>Add Permission</button>
												<!--end::Button-->
											</div>
											<!--end::Card toolbar-->
										</div>
										<!--end::Card header-->
										<!--begin::Card body-->
										<div class="card-body pt-0">
											<!--begin::Table-->
											<div id="kt_permissions_table_wrapper" class="dataTables_wrapper dt-bootstrap4 no-footer"><div class="table-responsive"><table class="table align-middle table-row-dashed fs-6 gy-5 mb-0 dataTable no-footer" id="kt_permissions_table">
												<thead>
													<tr class="text-start text-gray-800 fw-bold fs-7 text-uppercase gs-0"><th class="min-w-125px sorting" tabindex="0" aria-controls="kt_permissions_table" rowspan="1" colspan="1" aria-label="Name: activate to sort column ascending" style="width: 125px;">Name</th><th class="min-w-250px sorting_disabled" rowspan="1" colspan="1" aria-label="Assigned to" style="width: 250px;">Assigned to</th><th class="min-w-125px sorting" tabindex="0" aria-controls="kt_permissions_table" rowspan="1" colspan="1" aria-label="Created Date: activate to sort column ascending" style="width: 125px;">Created Date</th><th class="text-end min-w-100px sorting_disabled" rowspan="1" colspan="1" aria-label="Actions" style="width: 100px;">Actions</th></tr>
												</thead>
												<tbody runat="server" id="tablepermission" class="fw-semibold text-gray-600">
													
													
													
													
													
													
													
													
													
												</tbody>
											</table></div>
											
									</div>
									<!--end::Card-->
									<!--begin::Modals-->
									<!--begin::Modal - Add permissions-->
									<div class="modal fade" id="kt_modal_add_permission" tabindex="-1" aria-hidden="true">
										<!--begin::Modal dialog-->
										<div class="modal-dialog modal-dialog-centered mw-650px">
											<!--begin::Modal content-->
											<div class="modal-content">
												<!--begin::Modal header-->
												<div class="modal-header">
													<!--begin::Modal title-->
													<h2 class="fw-bold">Add a Permission</h2>
													<!--end::Modal title-->
													<!--begin::Close-->
													<div class="btn btn-icon btn-sm btn-active-icon-primary" data-kt-permissions-modal-action="close">
														<i class="ki-outline ki-cross fs-1"></i>
													</div>
													<!--end::Close-->
												</div>
												<!--end::Modal header-->
												<!--begin::Modal body-->
												<div class="modal-body scroll-y mx-5 mx-xl-15 my-7">
													<!--begin::Form-->
													<form id="kt_modal_add_permission_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" action="#">
														<!--begin::Input group-->
														<div class="fv-row mb-7 fv-plugins-icon-container">
															<!--begin::Label-->
															<label class="fs-6 fw-semibold form-label mb-2">
																<span class="required">Permission Name</span>
																<span class="ms-2" data-bs-toggle="popover" data-bs-trigger="hover" data-bs-html="true" data-bs-content="Permission names is required to be unique." data-kt-initialized="1">
																	<i class="ki-outline ki-information fs-7"></i>
																</span>
															</label>
															<!--end::Label-->
															<!--begin::Input-->
															<input class="form-control form-control-solid" placeholder="Enter a permission name" name="permission_name">
															<!--end::Input-->
														<div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div></div>
														<!--end::Input group-->
														<!--begin::Input group-->
														<div class="fv-row mb-7">
															<!--begin::Checkbox-->
															<label class="form-check form-check-custom form-check-solid me-9">
																<input class="form-check-input" type="checkbox" value="" name="permissions_core" id="kt_permissions_core">
																<span class="form-check-label" for="kt_permissions_core">Set as core permission</span>
															</label>
															<!--end::Checkbox-->
														</div>
														<!--end::Input group-->
														<!--begin::Disclaimer-->
														<div class="text-gray-600">Permission set as a 
														<strong class="me-1">Core Permission</strong>will be locked and 
														<strong class="me-1">not editable</strong>in future</div>
														<!--end::Disclaimer-->
														<!--begin::Actions-->
														<div class="text-center pt-15">
															<button type="reset" class="btn btn-light me-3" data-kt-permissions-modal-action="cancel">Discard</button>
															<button type="submit" class="btn btn-primary" data-kt-permissions-modal-action="submit">
																<span class="indicator-label">Submit</span>
																<span class="indicator-progress">Please wait... 
																<span class="spinner-border spinner-border-sm align-middle ms-2"></span></span>
															</button>
														</div>
														<!--end::Actions-->
													</form>
													<!--end::Form-->
												</div>
												<!--end::Modal body-->
											</div>
											<!--end::Modal content-->
										</div>
										<!--end::Modal dialog-->
									</div>
									<!--end::Modal - Add permissions-->
									<!--begin::Modal - Update permissions-->
									<div class="modal fade" id="kt_modal_update_permission" tabindex="-1" aria-hidden="true">
										<!--begin::Modal dialog-->
										<div class="modal-dialog modal-dialog-centered mw-650px">
											<!--begin::Modal content-->
											<div class="modal-content">
												<!--begin::Modal header-->
												<div class="modal-header">
													<!--begin::Modal title-->
													<h2 class="fw-bold">Update Permission</h2>
													<!--end::Modal title-->
													<!--begin::Close-->
													<div class="btn btn-icon btn-sm btn-active-icon-primary" data-kt-permissions-modal-action="close">
														<i class="ki-outline ki-cross fs-1"></i>
													</div>
													<!--end::Close-->
												</div>
												<!--end::Modal header-->
												<!--begin::Modal body-->
												<div class="modal-body scroll-y mx-5 mx-xl-15 my-7">
													<!--begin::Notice-->
													<!--begin::Notice-->
													<div class="notice d-flex bg-light-warning rounded border-warning border border-dashed mb-9 p-6">
														<!--begin::Icon-->
														<i class="ki-outline ki-information fs-2tx text-warning me-4"></i>
														<!--end::Icon-->
														<!--begin::Wrapper-->
														<div class="d-flex flex-stack flex-grow-1">
															<!--begin::Content-->
															<div class="fw-semibold">
																<div class="fs-6 text-gray-700">
																<strong class="me-1">Warning!</strong>By editing the permission name, you might break the system permissions functionality. Please ensure you're absolutely certain before proceeding.</div>
															</div>
															<!--end::Content-->
														</div>
														<!--end::Wrapper-->
													</div>
													<!--end::Notice-->
													<!--end::Notice-->
													<!--begin::Form-->
													<form id="kt_modal_update_permission_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" action="#">
														<!--begin::Input group-->
														<div class="fv-row mb-7 fv-plugins-icon-container">
															<!--begin::Label-->
															<label class="fs-6 fw-semibold form-label mb-2">
																<span class="required">Permission Name</span>
																<span class="ms-2" data-bs-toggle="popover" data-bs-trigger="hover" data-bs-html="true" data-bs-content="Permission names is required to be unique." data-kt-initialized="1">
																	<i class="ki-outline ki-information fs-7"></i>
																</span>
															</label>
															<!--end::Label-->
															<!--begin::Input-->
															<input class="form-control form-control-solid" placeholder="Enter a permission name" name="permission_name">
															<!--end::Input-->
														<div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div></div>
														<!--end::Input group-->
														<!--begin::Actions-->
														<div class="text-center pt-15">
															<button type="reset" class="btn btn-light me-3" data-kt-permissions-modal-action="cancel">Discard</button>
															<button type="submit" class="btn btn-primary" data-kt-permissions-modal-action="submit">
																<span class="indicator-label">Submit</span>
																<span class="indicator-progress">Please wait... 
																<span class="spinner-border spinner-border-sm align-middle ms-2"></span></span>
															</button>
														</div>
														<!--end::Actions-->
													</form>
													<!--end::Form-->
												</div>
												<!--end::Modal body-->
											</div>
											<!--end::Modal content-->
										</div>
										<!--end::Modal dialog-->
									</div>
									<!--end::Modal - Update permissions-->
									<!--end::Modals-->
								</div>

    <!--begin::Javascript-->
		<script>var hostUrl = "assets/";</script>
		<!--begin::Global Javascript Bundle(mandatory for all pages)-->
		<script src="assets/plugins/global/plugins.bundle.js"></script>
		<script src="assets/js/scripts.bundle.js"></script>
		<!--end::Global Javascript Bundle-->
		<!--begin::Vendors Javascript(used for this page only)-->
		<script src="assets/plugins/custom/datatables/datatables.bundle.js"></script>
		<!--end::Vendors Javascript-->
		<!--begin::Custom Javascript(used for this page only)-->
		<script src="assets/js/custom/apps/user-management/permissions/list.js"></script>
		<script src="assets/js/custom/apps/user-management/permissions/add-permission.js"></script>
		<script src="assets/js/custom/apps/user-management/permissions/update-permission.js"></script>
		<script src="assets/js/widgets.bundle.js"></script>
		<script src="assets/js/custom/widgets.js"></script>
		<script src="assets/js/custom/apps/chat/chat.js"></script>
		<script src="assets/js/custom/utilities/modals/upgrade-plan.js"></script>
		<script src="assets/js/custom/utilities/modals/create-campaign.js"></script>
		<script src="assets/js/custom/utilities/modals/users-search.js"></script>
		<!--end::Custom Javascript-->
		<!--end::Javascript-->
</body>
</html>
