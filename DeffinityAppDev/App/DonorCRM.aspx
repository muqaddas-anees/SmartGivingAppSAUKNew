<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DonorCRM.aspx.cs" Inherits="DonorCRM.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base href="../../">
    <title>Donor CRM</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!--begin::Fonts(mandatory for all pages)-->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Inter:300,400,500,600,700">
    <!--end::Fonts-->
    <!--begin::Vendor Stylesheets(used for this page only)-->
    <link href="../assets/plugins/custom/datatables/datatables.bundle.css" rel="stylesheet" type="text/css">
    <!--end::Vendor Stylesheets-->
    <!--begin::Global Stylesheets Bundle(mandatory for all pages)-->
    <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css">
    <link href="../assets/css/style.bundle.css" rel="stylesheet" type="text/css">
    <!--end::Global Stylesheets Bundle-->
    <script>// Frame-busting to prevent site from being loaded within a frame without permission (click-jacking) if (window.top != window.self) { window.top.location.replace(window.self.location.href); }</script>
	<style>
		.contact-item {
    transition: box-shadow 0.3s ease; /* Smooth transition for the shadow */
}

.contact-item:hover {
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); /* Shadow effect on hover */
}
	</style>
</head>
<body>
    <div class="d-flex">
    <div id="kt_aside" class="aside aside-light aside-hoverable" data-kt-drawer="true" data-kt-drawer-name="aside" data-kt-drawer-activate="{default: true, lg: false}" data-kt-drawer-overlay="true" data-kt-drawer-width="{default:'200px', '300px': '250px'}" data-kt-drawer-direction="start" data-kt-drawer-toggle="#kt_aside_mobile_toggle">
				
					<!--begin::Logo-->
					
					<div class="aside-logo flex-column-auto" id="kt_aside_logo">
						<a href="Dashboard.aspx" id="link_home">
							<img src="../ImageHandler.ashx?id=3085&amp;s=portfolio" id="img_logo" alt="Logo" class="h-45px">
						</a>
						<!--begin::Aside toggler-->
						<div id="kt_aside_toggle" class="btn btn-icon w-auto px-0 btn-active-color-primary aside-toggle" data-kt-toggle="true" data-kt-toggle-state="active" data-kt-toggle-target="body" data-kt-toggle-name="aside-minimize">
							<!--begin::Svg Icon | path: icons/duotune/arrows/arr079.svg-->
							<span class="svg-icon svg-icon-1 rotate-180">
								<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
									<path opacity="0.5" d="M14.2657 11.4343L18.45 7.25C18.8642 6.83579 18.8642 6.16421 18.45 5.75C18.0358 5.33579 17.3642 5.33579 16.95 5.75L11.4071 11.2929C11.0166 11.6834 11.0166 12.3166 11.4071 12.7071L16.95 18.25C17.3642 18.6642 18.0358 18.6642 18.45 18.25C18.8642 17.8358 18.8642 17.1642 18.45 16.75L14.2657 12.5657C13.9533 12.2533 13.9533 11.7467 14.2657 11.4343Z" fill="black"></path>
									<path d="M8.2657 11.4343L12.45 7.25C12.8642 6.83579 12.8642 6.16421 12.45 5.75C12.0358 5.33579 11.3642 5.33579 10.95 5.75L5.40712 11.2929C5.01659 11.6834 5.01659 12.3166 5.40712 12.7071L10.95 18.25C11.3642 18.6642 12.0358 18.6642 12.45 18.25C12.8642 17.8358 12.8642 17.1642 12.45 16.75L8.2657 12.5657C7.95328 12.2533 7.95328 11.7467 8.2657 11.4343Z" fill="black"></path>
								</svg>
							</span>
							<!--end::Svg Icon-->
						</div>
						<!--end::Aside toggler-->
					</div>
					<!--end::Logo-->
					<!--begin::Nav-->
					



<div class="aside-menu flex-column-fluid">
						<!--begin::Aside Menu-->
						<div class="hover-scroll-overlay-y my-5 my-lg-5" id="kt_aside_menu_wrapper" data-kt-scroll="true" data-kt-scroll-activate="{default: false, lg: true}" data-kt-scroll-height="auto" data-kt-scroll-dependencies="#kt_aside_logo, #kt_aside_footer" data-kt-scroll-wrappers="#kt_aside_menu" data-kt-scroll-offset="0" style="height: 740px;">
							<!--begin::Menu-->
							<div class="menu menu-column menu-title-gray-800 menu-state-title-primary menu-state-icon-primary menu-state-bullet-primary menu-arrow-gray-500" id="#kt_aside_menu" data-kt-menu="true">
													


						
							
								<div id="sidemenu_kt_app_sidebar_footer" class="app-sidebar-footer flex-column-auto pt-2 pb-2 px-6">
								<a href="OnboardingGuide.aspx" id="sidemenu_a_tithing" style="background-color:#50CD89;color:white;" class="btn btn-flex flex-center btn-custom btn-video overflow-hidden text-nowrap px-0 h-40px w-100" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Getting Started" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="bi bi-play-circle-fill fs-2" style="color:white;"></i>
									</span>
									<span class="btn-label">Getting Started</span>
								</a>
								
							</div>
									<div id="sidemenu_Div3" class="menu-item py-2">
								<a href="Dashboard.aspx" id="sidemenu_a2" class="menu-link " data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Donation Dashboard" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="fas fa-tachometer-alt fs-2"></i>
									</span>
									<span class="menu-title">Donation Dashboard</span>
								</a>
								
							</div>
							<div id="sidemenu_link_members" data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2">
								<span class="menu-link " title="" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="s CRM" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="fas fa-user-cog fs-2"></i>
									</span>
									<span class="menu-title">Donor CRM</span>
									<span class="menu-arrow"></span>
								</span>
								<div class="menu-sub menu-sub-accordion menu-active-bg">
									<div data-kt-menu-trigger="click" class="menu-item menu-accordion">
										
									<div class="menu-item">
										<a class="menu-link" href="Members.aspx?type=2">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">Donor List</span>
										</a>
									</div>
									<div class="menu-item">
										<a class="menu-link" href="Member.aspx?type=2">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">Add New Donor</span>
										</a>
									</div>
										</div>
								</div>
							</div>

								<div id="sidemenu_link_faithgiving" data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2">
								<span class="menu-link " title="" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Fundraisers" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="fas fa-hands fs-2"></i>
									</span>
									<span class="menu-title">Fundraisers</span>
									<span class="menu-arrow"></span>
								</span>
								<div class="menu-sub menu-sub-accordion menu-active-bg">
									<div data-kt-menu-trigger="click" class="menu-item menu-accordion">
										
									<div class="menu-item">
										<a class="menu-link" href="FundraiserListView.aspx">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">View Fundraising Campaigns</span>
										</a>
									</div>
									<div class="menu-item">
										<a class="menu-link" href="Fundraiser/AddFundraiser.aspx?type=main">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">Create New Fundraising Campaign</span>
										</a>
									</div>
										
											
										</div>
								</div>
							</div>
								<div id="sidemenu_pnlFundCamp" class="menu-item py-2">
								<a href="FundraiserListView.aspx?type=camp" class="menu-link " data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Landing Page" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="fas fa-hands fs-2"></i>
									</span>
									<span class="menu-title">P2P Participation </span>
								</a>
								
							</div>
								<div id="sidemenu_link_eventmanagement" data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2">
								<span class="menu-link " title="" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Projects " data-kt-initialized="1">
									<span class="menu-icon">
										<i class="fas fa-project-diagram fs-2"></i>
									</span>
									<span class="menu-title">Projects </span>
									<span class="menu-arrow"></span>
								</span>
							<div class="menu-sub menu-sub-accordion menu-active-bg">
									
									
									<div data-kt-menu-trigger="click" class="menu-item menu-accordion">
											<div class="menu-item">
												<a class="menu-link" href="../WF/DC/Joblist.aspx">
													<span class="menu-bullet">
														<span class="bullet bullet-dot"></span>
													</span>
													<span class="menu-title">View Current Projects</span>
												</a>
											</div>
											<div class="menu-item">
												<a class="menu-link" href="../WF/DC/FLSForm.aspx">
													<span class="menu-bullet">
														<span class="bullet bullet-dot"></span>
													</span>
													<span class="menu-title">Add Project</span>
												</a>
											</div>
											<div class="menu-item" style="display:none;visibility:hidden;">
												<a class="menu-link" href="../WF/DC/FRPApprovals.aspx">
													<span class="menu-bullet">
														<span class="bullet bullet-dot"></span>
													</span>
													<span class="menu-title">Invoice Journal</span>
												</a>
											</div>
										
										
									</div>
									

								
									
									
									
								
								</div>
							</div>
								
							<div id="sidemenu_link_TextToDonate" class="menu-item py-2">
								<a href="TextToDonate.aspx" id="sidemenu_a1" class="menu-link " data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Text To Donate" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="far fa-comment-dots fs-2"></i>
									</span>
									<span class="menu-title">Text To Donate</span>
								</a>
								
							</div>
									<div id="sidemenu_Div5" class="menu-item py-2">
					<a href="chat.aspx" id="sidemenu_a3" class="menu-link " data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Text To Donate" data-kt-initialized="1">
						<span class="menu-icon">
							<i class="far fa-comment-dots fs-2"></i>
						</span>
						<span class="menu-title">Messaging</span>
					</a>
					
				</div>
							<div id="sidemenu_link_activites" data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2">
								<span class="menu-link " title="" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Events Management" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="fas fa-theater-masks  fs-2"></i>
									</span>
									<span class="menu-title">Events Management</span>
									<span class="menu-arrow"></span>
								</span>
								<div class="menu-sub menu-sub-accordion menu-active-bg">
									<div data-kt-menu-trigger="click" class="menu-item menu-accordion">
									
										<div class="menu-item">
										<a class="menu-link" href="Events/EventList.aspx">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">View Events</span>
										</a>
									</div>
											<div class="menu-item">
										<a class="menu-link" href="Events/BasicInfo.aspx">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">Create An Event edit</span>
										</a>
									</div>
								
								
										</div>
								</div>
							</div>
								<div id="sidemenu_Div4" data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2" style="display:none;visibility:hidden;">
								<span class="menu-link " title="" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Online Shop" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="bi bi-basket fs-2"></i>
									</span>
									<span class="menu-title">Online Shop</span>
									<span class="menu-arrow"></span>
								</span>
								<div class="menu-sub menu-sub-accordion menu-active-bg">
									<div data-kt-menu-trigger="click" class="menu-item menu-accordion">
									
										<div class="menu-item">
										<a class="menu-link" href="Products/AddProduct.aspx">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">List a Product</span>
										</a>
									</div>
											<div class="menu-item">
										<a class="menu-link" href="Products/ViewProducts.aspx">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">View Products</span>
										</a>
									</div>
									<div class="menu-item">
										<a class="menu-link" href="Products/ViewOrders.aspx">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">View Orders</span>
										</a>
									</div>
								
										</div>
								</div>
							</div>
								
								
								
								
							
							
								

						

							
							<div id="sidemenu_Div1" class="menu-item py-2" style="display:none;visibility:hidden;">
								<a href="../WF/DC/BusinessServices.aspx" class="menu-link " data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Business Services" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="fab fa-buffer fs-2"></i>
									</span>
									<span class="menu-title">Business Services</span>
								</a>
								
							</div>

							

							
								
							
							
							
							
						
								

							<div id="sidemenu_link_timesheets" data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2" style="display:none;visibility:hidden">
								<span class="menu-link " title="" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Timesheet" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="far fa-clock  fs-2"></i>
									</span>
									<span class="menu-title">Timesheet</span>
								</span>
								<div class="menu-sub menu-sub-dropdown w-225px px-1 py-4">
									<div class="menu-item">
										<div class="menu-content">
											<span class="menu-section fs-5 fw-bolder ps-1 py-1">Timesheets</span>
										</div>
									</div>
									<div class="menu-item">
										<a class="menu-link" href="../WF/DC/Timesheets/AddTimesheets.aspx">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">Add Timesheets</span>
										</a>
									</div>
									<div class="menu-item">
										<a class="menu-link" href="../WF/DC/Timesheets/ApproveTimesheets.aspx">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">Approve Timesheets</span>
										</a>
									</div>
									<div class="menu-item">
										<a class="menu-link" href="../WF/DC/Timesheets/TimesheetReport.aspx">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">Timesheet Report</span>
										</a>
									</div>
									<div class="menu-item">
										<a class="menu-link" href="../WF/DC/Timesheets/Payroll.aspx">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">Payroll</span>
										</a>
									</div>
								</div>
							</div>

							<div id="sidemenu_link_expenses" data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2" style="display:none;visibility:hidden">
								<span class="menu-link " title="" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Expenses" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="far fa-money-bill-alt  fs-2"></i>
									</span>
									<span class="menu-title">Expenses</span>
								</span>
								<div class="menu-sub menu-sub-dropdown w-225px px-1 py-4">
									<div class="menu-item">
										<div class="menu-content">
											<span class="menu-section fs-5 fw-bolder ps-1 py-1">Expenses</span>
										</div>
									</div>
									<div class="menu-item">
										<a class="menu-link" href="../WF/DC/Expenses/AddExpenses.aspx">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">Add Expenses</span>
										</a>
									</div>
									<div class="menu-item">
										<a class="menu-link" href="../WF/DC/Expenses/ExpensesReport.aspx">
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">Expenses Report</span>
										</a>
									</div>
								</div>
							</div>
								<div class="menu-item py-2" style="display:none;visibility:hidden;">
								<a href="../WF/CustomerAdmin/Campaign/CampaignList.aspx" class="menu-link " data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Messaging" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="fas fa-mail-bulk fs-2"></i>
									</span>
									<span class="menu-title">Messaging</span>
								</a>
							</div>
							<div class="menu-item py-2" style="display:none;visibility:hidden;">
								<a href="sessions/LiveSessions.aspx" class="menu-link " data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Live Session" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="fas fa-mail-bulk fs-2"></i>
									</span>
									<span class="menu-title">Live Session</span>
								</a>
							</div>
								<div id="sidemenu_link_settings" class="menu-item py-2">
								<a href="Settings.aspx" class="menu-link " data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Settings" data-kt-initialized="1">
									<span class="menu-icon">
										<i class="fas fa-cogs fs-2"></i>
									</span>
									<span class="menu-title">Settings</span>
								</a>
								
							</div>
						</div>
						<!--end::Primary menu-->
					</div>

	</div>
                
					<!--end::Nav-->
					<!--begin::Footer-->
					<div class="aside-footer d-flex flex-column align-items-center flex-column-auto" id="kt_aside_footer">
						<!--begin::Menu-->
						<div class="mb-7">
							
							<!--begin::Menu 2-->
							
							<!--end::Menu 2-->
						</div>
						<!--end::Menu-->
					</div>
					<!--end::Footer-->
				</div>
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
 <a onclick="clearForm()" href="/app/member.aspx?type=2"  class="btn btn-primary w-100">
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
                                    <i class="ki-outline ki-magnifier fs-3 text-gray-500 position-absolute top-50 ms-5 translate-middle-y"></i>
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
                                    <h2>Add New Contact</h2>
                                </div>
                                <!--end::Card title-->
                            </div>
                            <!--end::Card header-->
                            <!--begin::Card body-->
                            <div id="form" runat="server" class="card-body pt-5">
                                <!--begin::Form-->
                                <form runat="server" id="kt_ecommerce_settings_general_form" class="form fv-plugins-bootstrap5 fv-plugins-framework">
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
																	<label class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="change" data-bs-toggle="tooltip" aria-label="Change avatar" data-bs-original-title="Change avatar" data-kt-initialized="1">
																		<i class="ki-outline ki-pencil fs-7"></i>
																		<!--begin::Inputs-->
																		<input type="file" name="avatar" accept=".png, .jpg, .jpeg">
																		<input type="hidden" name="avatar_remove">
																		<!--end::Inputs-->
																	</label>
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
    <input runat="server" readonly id="txtFirstName" type="text" class="form-control form-control-solid" name="firstName" value="">
    <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
</div>

<div class="fv-row mb-7 fv-plugins-icon-container">
    <label class="fs-6 fw-semibold form-label mt-3">
        <span class="">Last Name</span>
        <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's last name." data-bs-original-title="Enter the contact's last name." data-kt-initialized="1">
            <i class="ki-outline ki-information fs-7"></i>
        </span>
    </label>
    <input runat="server" readonly id="txtLastName" type="text" class="form-control form-control-solid" name="lastName" value="">
    <div class="fv-plugins-message-container fv-plugins-message-container--enabled invalid-feedback"></div>
</div>


<div class="fv-row mb-7">
    <label class="fs-6 fw-semibold form-label mt-3">
        <span>Company Name</span>
        <span class="ms-1" data-bs-toggle="tooltip" aria-label="Enter the contact's company name (optional)." data-bs-original-title="Enter the contact's company name (optional)." data-kt-initialized="1">
            <i class="ki-outline ki-information fs-7"></i>
        </span>
    </label>
    <input runat="server" readonly id="txtCompanyName" type="text" class="form-control form-control-solid" name="company_name" value="">
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
            <input runat="server" readonly id="txtEmail" type="email" class="form-control form-control-solid" name="email" value="">
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
            <input runat="server" readonly id="DonationsRaised" type="text" class="form-control form-control-solid" name="DonationsRaised" value="">
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
            <input runat="server" readonly id="txtPhone" type="text" class="form-control form-control-solid" name="phone" value="">
        </div>
    </div>
</div>

<div class="row row-cols-1 row-cols-sm-2 rol-cols-md-1 row-cols-lg-2">

    



<!-- Checkbox Group -->
<div class="fv-row mb-7">
    <label class="fs-6 fw-semibold form-label mt-3">Categories</label>
    <div class="form-check">
        <input runat="server" id="chkDonors" type="checkbox" class="form-check-input" name="categories" value="Donors">
        <label for="chkDonors" class="form-check-label">Donors</label>
    </div>
    <div class="form-check mt-5">
        <input runat="server" id="chkVolunteers" type="checkbox" class="form-check-input" name="categories" value="Volunteers">
        <label for="chkVolunteers" class="form-check-label">Volunteers</label>
    </div>
    <div class="form-check mt-5">
        <input runat="server" id="chkLeads" type="checkbox" class="form-check-input" name="categories" value="Leads">
        <label for="chkLeads" class="form-check-label">Leads</label>
    </div>
    <div class="form-check mt-5">
        <input runat="server" id="chkSponsors" type="checkbox" class="form-check-input" name="categories" value="Sponsors">
        <label for="chkSponsors" class="form-check-label">Sponsors</label>
    </div>
</div>

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






			var checkbox1 = document.getElementById('chkDonors');
			var checkbox2 = document.getElementById('chkVolunteers');
			var checkbox3 = document.getElementById('chkLeads');
			var checkbox4 = document.getElementById('chkSponsors');
			checkbox1.checked = false;
			checkbox2.checked = false;
			checkbox3.checked = false;
            checkbox4.checked = false;
			



            if (contact) {
				document.getElementById('txtFirstName').value = contact.FirstName || "";
                document.getElementById('txtLastName').value = contact.LastName || "";
                document.getElementById('txtCompanyName').value = contact.CompanyName || "";
                document.getElementById('txtEmail').value = contact.Email || "";
				document.getElementById('txtPhone').value = contact.Phone || "";
				document.getElementById('DonationsRaised').value = contact.DonationsRaised || "";
                document.getElementById('bgimg').style.backgroundImage = `url('${contact.imgurl}')`;
                console.log(`url('${contact.imgurl}')`)
                console.log(document.getElementById('bgimg'));

             
                if (contact.Categories == 2) {
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
                    console.log(category);
					var checkbox = document.getElementById('chk' + category + 's');
                   

                    if (checkbox) {
                        checkbox.checked = true;
                    }
                });

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
</body>
</html>
