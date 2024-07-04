<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="sidemenu_fundriser.ascx.cs" Inherits="DeffinityAppDev.App.controls.sidemenu_fundriser" %>

<%--<div class="aside-nav d-flex flex-column align-lg-center flex-column-fluid w-100 pt-5 pt-lg-0" id="kt_aside_nav">
						<!--begin::Primary menu-->
						<div id="kt_aside_menu" class="menu menu-column menu-title-gray-600 menu-state-primary menu-state-icon-primary menu-state-bullet-primary menu-arrow-gray-500 fw-bold fs-6" data-kt-menu="true" >--%>
<div class="aside-menu flex-column-fluid">
						<!--begin::Aside Menu-->
						<div class="hover-scroll-overlay-y my-5 my-lg-5" id="kt_aside_menu_wrapper" data-kt-scroll="true" data-kt-scroll-activate="{default: false, lg: true}" data-kt-scroll-height="auto" data-kt-scroll-dependencies="#kt_aside_logo, #kt_aside_footer" data-kt-scroll-wrappers="#kt_aside_menu" data-kt-scroll-offset="0">
							<!--begin::Menu-->
							<div class="menu menu-column menu-title-gray-800 menu-state-title-primary menu-state-icon-primary menu-state-bullet-primary menu-arrow-gray-500" id="#kt_aside_menu" data-kt-menu="true">
													
								<%--<div class="app-sidebar-footer flex-column-auto pt-2 pb-2 px-6" id="kt_app_sidebar_footer" runat="server" >
								<a href="~/App/OnboardingGuide.aspx" id="a_tithing" runat="server" style="background-color:#50CD89;color:white;" class="btn btn-flex flex-center btn-custom btn-video overflow-hidden text-nowrap px-0 h-40px w-100" title="Getting Started" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
									<span class="menu-icon">
										<i class="bi bi-play-circle-fill fs-2" style="color:white;"></i>
									</span>
									<span class="btn-label">Getting Started</span>
								</a>
								
							</div>--%>
									
							

								
								
								<div class="menu-item py-2" id="link_pagebuilder" runat="server">
								<a href='<%:ResolveClientUrl("~/App/FundraiserListView.aspx")%>' class="menu-link " title="Fundraising Campaigns" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
									<span class="menu-icon">
										<i class="fab fa-sellsy fs-2"></i>
									</span>
									<span class="menu-title">Fundraising Campaigns</span>
								</a>
								
							</div>
							
								<div class="menu-item py-2" >
								<a href='#' id="linkProfile" class="menu-link " runat="server" title="My Profile" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
									<span class="menu-icon">
										<i class="fas fa-cogs fs-2"></i>
									</span>
									<span class="menu-title">My Profile</span>
								</a>
								
							</div>
						</div>
						<!--end::Primary menu-->
					</div>

	</div>