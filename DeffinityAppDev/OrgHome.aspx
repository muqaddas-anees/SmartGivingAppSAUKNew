<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgHome.aspx.cs" Inherits="DeffinityAppDev.OrgHomeNew" %>

<%@ Register Src="~/App/controls/ActivitiesCtrl.ascx" TagPrefix="Pref" TagName="ActivitiesCtrl" %>
<%@ Register Src="~/App/controls/TithingNewCtrl.ascx" TagPrefix="Pref" TagName="TithingNewCtrl" %>
<%@ Register Src="~/App/controls/FaithGivingListCtrl.ascx" TagPrefix="Pref" TagName="FaithGivingListCtrl" %>
<%@ Register Src="~/App/controls/FaithEducationCtrl.ascx" TagPrefix="Pref" TagName="FaithEducationCtrl" %>





<!DOCTYPE html>
<!--
Author: Keenthemes
Product Name: Metronic - Bootstrap 5 HTML, VueJS, React, Angular & Laravel Admin Dashboard Theme
Purchase: https://1.envato.market/EA4JP
Website: http://www.keenthemes.com
Contact: support@keenthemes.com
Follow: www.twitter.com/keenthemes
Dribbble: www.dribbble.com/keenthemes
Like: www.facebook.com/keenthemes
License: For each use you must have a valid license purchased only from above link in order to legally use the theme for your project.
-->
<html lang="en">
	<!--begin::Head-->
	<head><base href="../../">
		<title>Organization</title>
		<meta name="description" content="" />
		<meta name="keywords" content="" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<meta charset="utf-8" />
		<meta property="og:locale" content="en_US" />
		<meta property="og:type" content="article" />
		<meta property="og:title" content="" />
		<meta property="og:url" content="" />
		<meta property="og:site_name" content="" />
		<link rel="canonical" href="" />
		<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />
		<!--begin::Fonts-->
		<link rel="stylesheet" href="" />
		<link href='<%:ResolveClientUrl("~/assets/plugins/custom/leaflet/leaflet.bundle.css")%>' rel="stylesheet" type="text/css" />
	
		<!--end::Page Vendor Stylesheets-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css")%>' rel="stylesheet" type="text/css" />
		<link href='<%:ResolveClientUrl("~/assets/css/style.bundle.css")%>' rel="stylesheet" type="text/css" />
			<link href="Content/AjaxControlToolkit/Styles/Calendar.min.css" rel="stylesheet" />
		 <%= System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
		<!--end::Global Stylesheets Bundle-->
		 <style>
     .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
   /* .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: 140px;
    }*/
        </style>
	</head>
	<!--end::Head-->
	<!--begin::Body-->
	<body id="kt_body" class="page-loading-enabled page-loading">
		<div class="page-loader">
			<span class="spinner-border text-primary" role="status">
				<span class="visually-hidden">Loading...</span>
			</span>
		</div>
		 <form id="form1" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
       <Scripts>
          <%-- <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />--%>
		    <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />
       </Scripts>
   </asp:ScriptManager>
		<!--begin::Main-->
		<!--begin::Root-->
		<div class=" d-flex flex-column flex-root ">
			<!--begin::Page-->
			<div class="page d-flex flex-row flex-column-fluid">
				<!--begin::Aside-->
				
				<!--end::Aside-->
				<!--begin::Wrapper-->
				<div class="wrapper d-flex flex-column flex-row-fluid" id="kt_wrapper">
					<!--begin::Header-->
					
					<!--end::Header-->
					<!--begin::Content-->
					<div class="content d-flex flex-column flex-column-fluid" id="kt_content">
						<!--begin::Container-->
						<div class="container-xxl" id="kt_content_container">
							<div class="row mb-6">
							<!--begin::About card-->
							<div class="card">
								<!--begin::Body-->
								<div class="card-body p-lg-17">
									<!--begin::About-->
									<div class="mb-8">
										<!--begin::Wrapper-->
										<div class="mb-10">
											<!--begin::Top-->
											<div class="text-center mb-15">
												<!--begin::Title-->
												<h3 class="fs-2hx text-dark mb-5"><asp:Literal ID="lblOrgnizationname" runat="server"></asp:Literal> </h3>
												<!--end::Title-->
												<!--begin::Text-->
												<%--<div class="fs-5 text-muted fw-bold">Within the last 10 years, we have sold over 100,000 admin theme copies that have been
												<br />successfully deployed by small businesses to global enterprises</div>--%>
												<!--end::Text-->
											</div>
											<!--end::Top-->
											<!--begin::Overlay-->
											<div class="overlay">
												<!--begin::Image-->
												<img class="w-100 card-rounded" src="assets/banners/banner1.jpg" alt="" />
												<!--end::Image-->
												<!--begin::Links-->
												<%--<div class="overlay-layer card-rounded bg-dark bg-opacity-25">
													<a href="../../demo4/dist/pages/company/pricing.html" class="btn btn-primary">Pricing</a>
													<a href="../../demo4/dist/pages/careers/apply.html" class="btn btn-light-primary ms-3">Join Us</a>
												</div>--%>
												<!--end::Links-->
											</div>
											<!--end::Container-->
										</div>
										<!--end::Wrapper-->
										<!--begin::Description-->
										<%--<div class="fs-5 fw-bold text-gray-600">
											<!--begin::Text-->
											<p class="mb-8">First, a disclaimer – the entire process of writing a blog post often takes more than a couple of hours, even if you can type eighty words per minute and your writing skills are sharp. From the seed of the idea to finally hitting “Publish,” you might spend several days or maybe even a week “writing” a blog post, but it’s important to spend those vital hours planning your post and even thinking about
											<a href="../../demo4/dist/pages/blog/post.html" class="link-primary pe-1">Your Post</a>(yes, thinking counts as working if you’re a blogger) before you actually write it.</p>
											<!--end::Text-->
											<!--begin::Text-->
											<p class="mb-8">There’s an old maxim that states,
											<span class="text-gray-800 pe-1">“No fun for the writer, no fun for the reader.”</span>No matter what industry you’re working in, as a blogger, you should live and die by this statement.</p>
											<!--end::Text-->
											<!--begin::Text-->
											<p class="mb-8">Before you do any of the following steps, be sure to pick a topic that actually interests you. Nothing – and
											<a href="../../demo4/dist/pages/blog/home.html" class="link-primary pe-1">I mean NOTHING</a>– will kill a blog post more effectively than a lack of enthusiasm from the writer. You can tell when a writer is bored by their subject, and it’s so cringe-worthy it’s a little embarrassing.</p>
											<!--end::Text-->
											<!--begin::Text-->
											<p class="mb-17">I can hear your objections already. “But Dan, I have to blog for a cardboard box manufacturing company.” I feel your pain, I really do. During the course of my career, I’ve written content for dozens of clients in some less-than-thrilling industries (such as financial regulatory compliance and corporate housing), but the hallmark of a professional blogger is the ability to write well about any topic, no matter how dry it may be. Blogging is a lot easier, however, if you can muster at least a little enthusiasm for the topic at hand.</p>
											<!--end::Text-->
										</div>--%>
										<!--end::Description-->
									</div>
									<!--end::About-->
									
									<!--begin::Section-->

									
									

								
									

									<!--end::Section-->
									
									<!--begin::Card-->
									
									<!--end::Card-->
								</div>
								<!--end::Body-->
							</div>
								</div>
							<div class="row">
								<div class="mb-6">
											<div class="row g-10">
											<!--begin::Col-->
											<div class="col-md-4">
												<div class="card card-xxl-stretch">
											<!--begin::Header-->
											<div class="card-header border-0 bg-danger py-5">
												<h3 class="card-title fw-bolder text-white">Faith </h3>
												<div class="card-toolbar">
													<!--begin::Menu-->
													<button type="button" class="btn btn-sm btn-icon btn-color-white btn-active-white btn-active-color- border-0 me-n3" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end" data-kt-menu-flip="top-end">
														<!--begin::Svg Icon | path: icons/duotune/general/gen024.svg-->
														<span class="svg-icon svg-icon-2">
															<svg xmlns="http://www.w3.org/2000/svg" width="24px" height="24px" viewBox="0 0 24 24">
																<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
																	<rect x="5" y="5" width="5" height="5" rx="1" fill="#000000"></rect>
																	<rect x="14" y="5" width="5" height="5" rx="1" fill="#000000" opacity="0.3"></rect>
																	<rect x="5" y="14" width="5" height="5" rx="1" fill="#000000" opacity="0.3"></rect>
																	<rect x="14" y="14" width="5" height="5" rx="1" fill="#000000" opacity="0.3"></rect>
																</g>
															</svg>
														</span>
														<!--end::Svg Icon-->
													</button>
													<!--begin::Menu 3-->
													<div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-800 menu-state-bg-light-primary fw-bold w-200px py-3" data-kt-menu="false">
														<!--begin::Heading-->
														<div class="menu-item px-3">
															<div class="menu-content text-muted pb-2 px-3 fs-7 text-uppercase" style="display:none;visibility:hidden;">Payments</div>
														</div>
														<!--end::Heading-->
														<!--begin::Menu item-->
														<div class="menu-item px-3">
															<a href="#" class="menu-link px-3">Create Invoice</a>
														</div>
														<!--end::Menu item-->
														<!--begin::Menu item-->
														<div class="menu-item px-3">
															<a href="#" class="menu-link flex-stack px-3">Create Payment
															<i class="fas fa-exclamation-circle ms-2 fs-7" data-bs-toggle="tooltip" title="" data-bs-original-title="Specify a target name for future usage and reference" aria-label="Specify a target name for future usage and reference"></i></a>
														</div>
														<!--end::Menu item-->
														<!--begin::Menu item-->
														<div class="menu-item px-3">
															<a href="#" class="menu-link px-3">Generate Bill</a>
														</div>
														<!--end::Menu item-->
														<!--begin::Menu item-->
														<div class="menu-item px-3" data-kt-menu-trigger="hover" data-kt-menu-placement="right-end" data-kt-menu-flip="bottom, top">
															<a href="#" class="menu-link px-3">
																<span class="menu-title">Subscription</span>
																<span class="menu-arrow"></span>
															</a>
															<!--begin::Menu sub-->
															<div class="menu-sub menu-sub-dropdown w-175px py-4">
																<!--begin::Menu item-->
																<div class="menu-item px-3">
																	<a href="#" class="menu-link px-3">Plans</a>
																</div>
																<!--end::Menu item-->
																<!--begin::Menu item-->
																<div class="menu-item px-3">
																	<a href="#" class="menu-link px-3">Billing</a>
																</div>
																<!--end::Menu item-->
																<!--begin::Menu item-->
																<div class="menu-item px-3">
																	<a href="#" class="menu-link px-3">Statements</a>
																</div>
																<!--end::Menu item-->
																<!--begin::Menu separator-->
																<div class="separator my-2"></div>
																<!--end::Menu separator-->
																<!--begin::Menu item-->
																<div class="menu-item px-3">
																	<div class="menu-content px-3">
																		<!--begin::Switch-->
																		<label class="form-check form-switch form-check-custom form-check-solid">
																			<!--begin::Input-->
																			<input class="form-check-input w-30px h-20px" type="checkbox" value="1" checked="checked" name="notifications">
																			<!--end::Input-->
																			<!--end::Label-->
																			<span class="form-check-label text-muted fs-6">Recuring</span>
																			<!--end::Label-->
																		</label>
																		<!--end::Switch-->
																	</div>
																</div>
																<!--end::Menu item-->
															</div>
															<!--end::Menu sub-->
														</div>
														<!--end::Menu item-->
														<!--begin::Menu item-->
														<div class="menu-item px-3 my-1">
															<a href="#" class="menu-link px-3">Settings</a>
														</div>
														<!--end::Menu item-->
													</div>
													<!--end::Menu 3-->
													<!--end::Menu-->
												</div>
											</div>
											<!--end::Header-->
											<!--begin::Body-->
											<div class="card-body p-0" style="position: relative;min-height:580px;" >
												<!--begin::Chart-->
												<div class="mixed-widget-2-chart card-rounded-bottom bg-danger" data-kt-color="danger" style="height: 200px; min-height: 200px;"><div id="apexcharts8nsiooy9" class="apexcharts-canvas apexcharts8nsiooy9 apexcharts-theme-light" style="width: 403px; height: 200px;"><svg id="SvgjsSvg1935" width="403" height="200" xmlns="http://www.w3.org/2000/svg" version="1.1" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:svgjs="http://svgjs.com/svgjs" class="apexcharts-svg" xmlns:data="ApexChartsNS" transform="translate(0, 0)" style="background: transparent;"><g id="SvgjsG1937" class="apexcharts-inner apexcharts-graphical" transform="translate(0, 0)"><defs id="SvgjsDefs1936"><clipPath id="gridRectMask8nsiooy9"><rect id="SvgjsRect1940" width="410" height="203" x="-3.5" y="-1.5" rx="0" ry="0" opacity="1" stroke-width="0" stroke="none" stroke-dasharray="0" fill="#fff"></rect></clipPath><clipPath id="forecastMask8nsiooy9"></clipPath><clipPath id="nonForecastMask8nsiooy9"></clipPath><clipPath id="gridRectMarkerMask8nsiooy9"><rect id="SvgjsRect1941" width="407" height="204" x="-2" y="-2" rx="0" ry="0" opacity="1" stroke-width="0" stroke="none" stroke-dasharray="0" fill="#fff"></rect></clipPath><filter id="SvgjsFilter1947" filterUnits="userSpaceOnUse" width="200%" height="200%" x="-50%" y="-50%"><feFlood id="SvgjsFeFlood1948" flood-color="#cb1b46" flood-opacity="0.5" result="SvgjsFeFlood1948Out" in="SourceGraphic"></feFlood><feComposite id="SvgjsFeComposite1949" in="SvgjsFeFlood1948Out" in2="SourceAlpha" operator="in" result="SvgjsFeComposite1949Out"></feComposite><feOffset id="SvgjsFeOffset1950" dx="0" dy="5" result="SvgjsFeOffset1950Out" in="SvgjsFeComposite1949Out"></feOffset><feGaussianBlur id="SvgjsFeGaussianBlur1951" stdDeviation="3 " result="SvgjsFeGaussianBlur1951Out" in="SvgjsFeOffset1950Out"></feGaussianBlur><feMerge id="SvgjsFeMerge1952" result="SvgjsFeMerge1952Out" in="SourceGraphic"><feMergeNode id="SvgjsFeMergeNode1953" in="SvgjsFeGaussianBlur1951Out"></feMergeNode><feMergeNode id="SvgjsFeMergeNode1954" in="[object Arguments]"></feMergeNode></feMerge><feBlend id="SvgjsFeBlend1955" in="SourceGraphic" in2="SvgjsFeMerge1952Out" mode="normal" result="SvgjsFeBlend1955Out"></feBlend></filter><filter id="SvgjsFilter1957" filterUnits="userSpaceOnUse" width="200%" height="200%" x="-50%" y="-50%"><feFlood id="SvgjsFeFlood1958" flood-color="#cb1b46" flood-opacity="0.5" result="SvgjsFeFlood1958Out" in="SourceGraphic"></feFlood><feComposite id="SvgjsFeComposite1959" in="SvgjsFeFlood1958Out" in2="SourceAlpha" operator="in" result="SvgjsFeComposite1959Out"></feComposite><feOffset id="SvgjsFeOffset1960" dx="0" dy="5" result="SvgjsFeOffset1960Out" in="SvgjsFeComposite1959Out"></feOffset><feGaussianBlur id="SvgjsFeGaussianBlur1961" stdDeviation="3 " result="SvgjsFeGaussianBlur1961Out" in="SvgjsFeOffset1960Out"></feGaussianBlur><feMerge id="SvgjsFeMerge1962" result="SvgjsFeMerge1962Out" in="SourceGraphic"><feMergeNode id="SvgjsFeMergeNode1963" in="SvgjsFeGaussianBlur1961Out"></feMergeNode><feMergeNode id="SvgjsFeMergeNode1964" in="[object Arguments]"></feMergeNode></feMerge><feBlend id="SvgjsFeBlend1965" in="SourceGraphic" in2="SvgjsFeMerge1962Out" mode="normal" result="SvgjsFeBlend1965Out"></feBlend></filter></defs><g id="SvgjsG1966" class="apexcharts-xaxis" transform="translate(0, 0)"><g id="SvgjsG1967" class="apexcharts-xaxis-texts-g" transform="translate(0, -4)"></g></g><g id="SvgjsG1976" class="apexcharts-grid"><g id="SvgjsG1977" class="apexcharts-gridlines-horizontal" style="display: none;"><line id="SvgjsLine1979" x1="0" y1="0" x2="403" y2="0" stroke="#e0e0e0" stroke-dasharray="0" class="apexcharts-gridline"></line><line id="SvgjsLine1980" x1="0" y1="20" x2="403" y2="20" stroke="#e0e0e0" stroke-dasharray="0" class="apexcharts-gridline"></line><line id="SvgjsLine1981" x1="0" y1="40" x2="403" y2="40" stroke="#e0e0e0" stroke-dasharray="0" class="apexcharts-gridline"></line><line id="SvgjsLine1982" x1="0" y1="60" x2="403" y2="60" stroke="#e0e0e0" stroke-dasharray="0" class="apexcharts-gridline"></line><line id="SvgjsLine1983" x1="0" y1="80" x2="403" y2="80" stroke="#e0e0e0" stroke-dasharray="0" class="apexcharts-gridline"></line><line id="SvgjsLine1984" x1="0" y1="100" x2="403" y2="100" stroke="#e0e0e0" stroke-dasharray="0" class="apexcharts-gridline"></line><line id="SvgjsLine1985" x1="0" y1="120" x2="403" y2="120" stroke="#e0e0e0" stroke-dasharray="0" class="apexcharts-gridline"></line><line id="SvgjsLine1986" x1="0" y1="140" x2="403" y2="140" stroke="#e0e0e0" stroke-dasharray="0" class="apexcharts-gridline"></line><line id="SvgjsLine1987" x1="0" y1="160" x2="403" y2="160" stroke="#e0e0e0" stroke-dasharray="0" class="apexcharts-gridline"></line><line id="SvgjsLine1988" x1="0" y1="180" x2="403" y2="180" stroke="#e0e0e0" stroke-dasharray="0" class="apexcharts-gridline"></line><line id="SvgjsLine1989" x1="0" y1="200" x2="403" y2="200" stroke="#e0e0e0" stroke-dasharray="0" class="apexcharts-gridline"></line></g><g id="SvgjsG1978" class="apexcharts-gridlines-vertical" style="display: none;"></g><line id="SvgjsLine1991" x1="0" y1="200" x2="403" y2="200" stroke="transparent" stroke-dasharray="0"></line><line id="SvgjsLine1990" x1="0" y1="1" x2="0" y2="200" stroke="transparent" stroke-dasharray="0"></line></g><g id="SvgjsG1942" class="apexcharts-area-series apexcharts-plot-series"><g id="SvgjsG1943" class="apexcharts-series" seriesName="NetxProfit" data:longestSeries="true" rel="1" data:realIndex="0"><path id="SvgjsPath1946" d="M 0 200L 0 125C 23.508333333333333 125 43.65833333333334 87.5 67.16666666666667 87.5C 90.67500000000001 87.5 110.82500000000002 120 134.33333333333334 120C 157.84166666666667 120 177.99166666666667 25 201.5 25C 225.00833333333333 25 245.15833333333336 100 268.6666666666667 100C 292.175 100 312.325 100 335.8333333333333 100C 359.34166666666664 100 379.4916666666667 100 403 100C 403 100 403 100 403 200M 403 100z" fill="transparent" fill-opacity="1" stroke-opacity="1" stroke-linecap="butt" stroke-width="0" stroke-dasharray="0" class="apexcharts-area" index="0" clip-path="url(#gridRectMask8nsiooy9)" filter="url(#SvgjsFilter1947)" pathTo="M 0 200L 0 125C 23.508333333333333 125 43.65833333333334 87.5 67.16666666666667 87.5C 90.67500000000001 87.5 110.82500000000002 120 134.33333333333334 120C 157.84166666666667 120 177.99166666666667 25 201.5 25C 225.00833333333333 25 245.15833333333336 100 268.6666666666667 100C 292.175 100 312.325 100 335.8333333333333 100C 359.34166666666664 100 379.4916666666667 100 403 100C 403 100 403 100 403 200M 403 100z" pathFrom="M -1 200L -1 200L 67.16666666666667 200L 134.33333333333334 200L 201.5 200L 268.6666666666667 200L 335.8333333333333 200L 403 200"></path><path id="SvgjsPath1956" d="M 0 125C 23.508333333333333 125 43.65833333333334 87.5 67.16666666666667 87.5C 90.67500000000001 87.5 110.82500000000002 120 134.33333333333334 120C 157.84166666666667 120 177.99166666666667 25 201.5 25C 225.00833333333333 25 245.15833333333336 100 268.6666666666667 100C 292.175 100 312.325 100 335.8333333333333 100C 359.34166666666664 100 379.4916666666667 100 403 100" fill="none" fill-opacity="1" stroke="#cb1b46" stroke-opacity="1" stroke-linecap="butt" stroke-width="3" stroke-dasharray="0" class="apexcharts-area" index="0" clip-path="url(#gridRectMask8nsiooy9)" filter="url(#SvgjsFilter1957)" pathTo="M 0 125C 23.508333333333333 125 43.65833333333334 87.5 67.16666666666667 87.5C 90.67500000000001 87.5 110.82500000000002 120 134.33333333333334 120C 157.84166666666667 120 177.99166666666667 25 201.5 25C 225.00833333333333 25 245.15833333333336 100 268.6666666666667 100C 292.175 100 312.325 100 335.8333333333333 100C 359.34166666666664 100 379.4916666666667 100 403 100" pathFrom="M -1 200L -1 200L 67.16666666666667 200L 134.33333333333334 200L 201.5 200L 268.6666666666667 200L 335.8333333333333 200L 403 200"></path><g id="SvgjsG1944" class="apexcharts-series-markers-wrap" data:realIndex="0"><g class="apexcharts-series-markers"><circle id="SvgjsCircle1997" r="0" cx="0" cy="0" class="apexcharts-marker wwakxab9q no-pointer-events" stroke="#cb1b46" fill="#f1416c" fill-opacity="1" stroke-width="3" stroke-opacity="0.9" default-marker-size="0"></circle></g></g></g><g id="SvgjsG1945" class="apexcharts-datalabels" data:realIndex="0"></g></g><line id="SvgjsLine1992" x1="0" y1="0" x2="403" y2="0" stroke="#b6b6b6" stroke-dasharray="0" stroke-width="1" class="apexcharts-ycrosshairs"></line><line id="SvgjsLine1993" x1="0" y1="0" x2="403" y2="0" stroke-dasharray="0" stroke-width="0" class="apexcharts-ycrosshairs-hidden"></line><g id="SvgjsG1994" class="apexcharts-yaxis-annotations"></g><g id="SvgjsG1995" class="apexcharts-xaxis-annotations"></g><g id="SvgjsG1996" class="apexcharts-point-annotations"></g></g><g id="SvgjsG1975" class="apexcharts-yaxis" rel="0" transform="translate(-18, 0)"></g><g id="SvgjsG1938" class="apexcharts-annotations"></g></svg><div class="apexcharts-legend" style="max-height: 100px;"></div><div class="apexcharts-tooltip apexcharts-theme-light"><div class="apexcharts-tooltip-title" style="font-family: inherit; font-size: 12px;"></div><div class="apexcharts-tooltip-series-group" style="order: 1;"><span class="apexcharts-tooltip-marker" style="background-color: transparent;"></span><div class="apexcharts-tooltip-text" style="font-family: inherit; font-size: 12px;"><div class="apexcharts-tooltip-y-group"><span class="apexcharts-tooltip-text-y-label"></span><span class="apexcharts-tooltip-text-y-value"></span></div><div class="apexcharts-tooltip-goals-group"><span class="apexcharts-tooltip-text-goals-label"></span><span class="apexcharts-tooltip-text-goals-value"></span></div><div class="apexcharts-tooltip-z-group"><span class="apexcharts-tooltip-text-z-label"></span><span class="apexcharts-tooltip-text-z-value"></span></div></div></div></div><div class="apexcharts-yaxistooltip apexcharts-yaxistooltip-0 apexcharts-yaxistooltip-left apexcharts-theme-light"><div class="apexcharts-yaxistooltip-text"></div></div></div></div>
												<!--end::Chart-->
												<!--begin::Stats-->
												<div class="card-p mt-n20 position-relative">
													<!--begin::Row-->
													<div class="row g-0">
														<!--begin::Col-->
														<div class="col bg-light-warning px-6 py-8 rounded-2 me-7 mb-7">
															<!--begin::Svg Icon | path: icons/duotune/general/gen032.svg-->
															<span class="svg-icon svg-icon-3x svg-icon-warning d-block my-2">
																<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																	<rect x="8" y="9" width="3" height="10" rx="1.5" fill="black"></rect>
																	<rect opacity="0.5" x="13" y="5" width="3" height="14" rx="1.5" fill="black"></rect>
																	<rect x="18" y="11" width="3" height="8" rx="1.5" fill="black"></rect>
																	<rect x="3" y="13" width="3" height="6" rx="1.5" fill="black"></rect>
																</svg>
															</span>
															<!--end::Svg Icon-->
															<a href="#" class="text-warning fw-bold fs-6">Faith Tithing</a>
														</div>
														<!--end::Col-->
														<!--begin::Col-->
														<div class="col bg-light-primary px-6 py-8 rounded-2 mb-7">
															<!--begin::Svg Icon | path: icons/duotune/finance/fin006.svg-->
															<span class="svg-icon svg-icon-3x svg-icon-primary d-block my-2">
																<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																	<path opacity="0.3" d="M20 15H4C2.9 15 2 14.1 2 13V7C2 6.4 2.4 6 3 6H21C21.6 6 22 6.4 22 7V13C22 14.1 21.1 15 20 15ZM13 12H11C10.5 12 10 12.4 10 13V16C10 16.5 10.4 17 11 17H13C13.6 17 14 16.6 14 16V13C14 12.4 13.6 12 13 12Z" fill="black"></path>
																	<path d="M14 6V5H10V6H8V5C8 3.9 8.9 3 10 3H14C15.1 3 16 3.9 16 5V6H14ZM20 15H14V16C14 16.6 13.5 17 13 17H11C10.5 17 10 16.6 10 16V15H4C3.6 15 3.3 14.9 3 14.7V18C3 19.1 3.9 20 5 20H19C20.1 20 21 19.1 21 18V14.7C20.7 14.9 20.4 15 20 15Z" fill="black"></path>
																</svg>
															</span>
															<!--end::Svg Icon-->
															<a href="FaithGivingList.aspx" class="text-primary fw-bold fs-6">Faith Giving</a>
														</div>
														<!--end::Col-->
													</div>
													<!--end::Row-->
													<!--begin::Row-->
													<div class="row g-0">
														<!--begin::Col-->
														<div class="col bg-light-danger px-6 py-8 rounded-2 me-7">
															<!--begin::Svg Icon | path: icons/duotune/abstract/abs027.svg-->
															<span class="svg-icon svg-icon-3x svg-icon-danger d-block my-2">
																<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																	<path opacity="0.3" d="M21.25 18.525L13.05 21.825C12.35 22.125 11.65 22.125 10.95 21.825L2.75 18.525C1.75 18.125 1.75 16.725 2.75 16.325L4.04999 15.825L10.25 18.325C10.85 18.525 11.45 18.625 12.05 18.625C12.65 18.625 13.25 18.525 13.85 18.325L20.05 15.825L21.35 16.325C22.35 16.725 22.35 18.125 21.25 18.525ZM13.05 16.425L21.25 13.125C22.25 12.725 22.25 11.325 21.25 10.925L13.05 7.62502C12.35 7.32502 11.65 7.32502 10.95 7.62502L2.75 10.925C1.75 11.325 1.75 12.725 2.75 13.125L10.95 16.425C11.65 16.725 12.45 16.725 13.05 16.425Z" fill="black"></path>
																	<path d="M11.05 11.025L2.84998 7.725C1.84998 7.325 1.84998 5.925 2.84998 5.525L11.05 2.225C11.75 1.925 12.45 1.925 13.15 2.225L21.35 5.525C22.35 5.925 22.35 7.325 21.35 7.725L13.05 11.025C12.45 11.325 11.65 11.325 11.05 11.025Z" fill="black"></path>
																</svg>
															</span>
															<!--end::Svg Icon-->
															<a href="#" class="text-danger fw-bold fs-6 mt-2">Faith Services</a>
														</div>
														<!--end::Col-->
														<!--begin::Col-->
														<div class="col bg-light-success px-6 py-8 rounded-2">
															<!--begin::Svg Icon | path: icons/duotune/communication/com010.svg-->
															<span class="svg-icon svg-icon-3x svg-icon-success d-block my-2">
																<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																	<path d="M6 8.725C6 8.125 6.4 7.725 7 7.725H14L18 11.725V12.925L22 9.725L12.6 2.225C12.2 1.925 11.7 1.925 11.4 2.225L2 9.725L6 12.925V8.725Z" fill="black"></path>
																	<path opacity="0.3" d="M22 9.72498V20.725C22 21.325 21.6 21.725 21 21.725H3C2.4 21.725 2 21.325 2 20.725V9.72498L11.4 17.225C11.8 17.525 12.3 17.525 12.6 17.225L22 9.72498ZM15 11.725H18L14 7.72498V10.725C14 11.325 14.4 11.725 15 11.725Z" fill="black"></path>
																</svg>
															</span>
															<!--end::Svg Icon-->
															<a href="#" class="text-success fw-bold fs-6 mt-2">Faith Membership
</a>
														</div>
														<!--end::Col-->
													</div>
													<!--end::Row-->
												</div>
												<!--end::Stats-->
											<div class="resize-triggers"><div class="expand-trigger"><div style="width: 404px; height: 460px;"></div></div><div class="contract-trigger"></div></div></div>
											<!--end::Body-->
										</div>
												</div>
												<div class="col-md-4">
                                                    <Pref:TithingNewCtrl runat="server" id="TithingNewCtrl" />
													<!--end::Body-->
												
												</div>
												<div class="col-md-4">
                                                    <Pref:ActivitiesCtrl runat="server" ID="ActivitiesCtrl" />
												</div>

												</div>

										</div>
					</div>

							<div class="row mb-6">
								<div class="card">
									<div class="card-body">
											<div class="mb-16">
										<!--begin::Top-->
										<div class="text-center mb-12">
											<!--begin::Title-->
											<h3 class="fs-2hx text-dark mb-5">Faith Giving</h3>
											<!--end::Title-->
											<!--begin::Text-->
										<%--	<div class="fs-5 text-muted fw-bold">Our goal is to provide a complete and robust theme solution
											<br />to boost all of our customer’s project deployments</div>--%>
											<!--end::Text-->
										</div>
										<!--end::Top-->
										<!--begin::Row-->
											<div class="scroll-y mh-540px">
										<div class="row g-5 mb-6" style="overflow-y:scroll;max-height:540px">
                                           
                                            <Pref:FaithGivingListCtrl runat="server" ID="FaithGivingListCtrl" />
											</div>
												</div>
										
										<!--end::Row-->
									</div>
									</div>

								</div>
							</div>


							<div class="row">
								<div class="card">
									<div class="card-body">
										
									<div class="mb-16">
										<!--begin::Top-->
										<div class="text-center mb-12">
											<!--begin::Title-->
											<h3 class="fs-2hx text-dark mb-5">Faith Education</h3>
											<!--end::Title-->
											<!--begin::Text-->
										<%--	<div class="fs-5 text-muted fw-bold">Our goal is to provide a complete and robust theme solution
											<br />to boost all of our customer’s project deployments</div>--%>
											<!--end::Text-->
										</div>
										<!--end::Top-->
										<!--begin::Row-->
										<div class="scroll-y mh-540px">
										<div class="row g-10 mb-6" style="overflow-y:scroll;max-height:540px">
											
                                            <Pref:FaithEducationCtrl runat="server" id="FaithEducationCtrl" />
											</div>
											</div>

										
										<!--end::Row-->
									</div>

										</div>
									</div>
								</div>
							<!--end::About card-->
						</div>
						<!--end::Container-->
					</div>
					
					<!--end::Content-->
					<!--begin::Footer-->
					<div class="footer py-4 d-flex flex-lg-column" id="kt_footer">
						<!--begin::Container-->
						<div class="container-xxl d-flex flex-column flex-md-row flex-stack">
							<!--begin::Copyright-->
							<div class="text-dark order-2 order-md-1">
								<%--<span class="text-gray-400 fw-bold me-1">Created by</span>
								<a href="https://keenthemes.com" target="_blank" class="text-muted text-hover-primary fw-bold me-2 fs-6">Keenthemes</a>--%>
							</div>
							<!--end::Copyright-->
							<!--begin::Menu-->
							<ul class="menu menu-gray-600 menu-hover-primary fw-bold order-1">
								<%--<li class="menu-item">
									<a href="https://keenthemes.com" target="_blank" class="menu-link px-2">About</a>
								</li>
								<li class="menu-item">
									<a href="https://keenthemes.com/support" target="_blank" class="menu-link px-2">Support</a>
								</li>
								<li class="menu-item">
									<a href="https://1.envato.market/EA4JP" target="_blank" class="menu-link px-2">Purchase</a>
								</li>--%>
							</ul>
							<!--end::Menu-->
						</div>
						<!--end::Container-->
					</div>
					<!--end::Footer-->
				</div>
				<!--end::Wrapper-->
			</div>
			<!--end::Page-->
		</div>
		<!--end::Root-->
		<!--begin::Drawers-->
		
		<!--end::Modals-->
		<!--begin::Scrolltop-->
		<div id="kt_scrolltop" class="scrolltop" data-kt-scrolltop="true">
			<!--begin::Svg Icon | path: icons/duotune/arrows/arr066.svg-->
			<span class="svg-icon">
				<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
					<rect opacity="0.5" x="13" y="6" width="13" height="2" rx="1" transform="rotate(90 13 6)" fill="black" />
					<path d="M12.5657 8.56569L16.75 12.75C17.1642 13.1642 17.8358 13.1642 18.25 12.75C18.6642 12.3358 18.6642 11.6642 18.25 11.25L12.7071 5.70711C12.3166 5.31658 11.6834 5.31658 11.2929 5.70711L5.75 11.25C5.33579 11.6642 5.33579 12.3358 5.75 12.75C6.16421 13.1642 6.83579 13.1642 7.25 12.75L11.4343 8.56569C11.7467 8.25327 12.2533 8.25327 12.5657 8.56569Z" fill="black" />
				</svg>
			</span>
			<!--end::Svg Icon-->
		</div>
		<!--end::Scrolltop-->
		<!--end::Main-->
		<!--begin::Javascript-->
		<!--begin::Global Javascript Bundle(used by all pages)-->
		<script src="assets/plugins/global/plugins.bundle.js"></script>
		<script src="assets/js/scripts.bundle.js"></script>
		<!--end::Global Javascript Bundle-->
		<!--begin::Page Vendors Javascript(used by this page)-->
		<script src="assets/plugins/custom/fslightbox/fslightbox.bundle.js"></script>
		<!--end::Page Vendors Javascript-->
		<!--begin::Page Custom Javascript(used by this page)-->
		<script src="assets/js/custom/widgets.js"></script>
		<script src="assets/js/custom/apps/chat/chat.js"></script>
		<script src="assets/js/custom/modals/create-app.js"></script>
		<script src="assets/js/custom/modals/upgrade-plan.js"></script>
		<!--end::Page Custom Javascript-->
		<!--end::Javascript-->
			 </form>
	</body>
	<!--end::Body-->


	<script type="text/javascript">
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-center",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        //toastr.success("etetetetet", "testet");
        function showswal(msg, buttontitle) {
            Swal.fire({
                text: msg,
                icon: 'success',
                buttonsStyling: false,
                confirmButtonText: buttontitle,
                customClass: {
                    confirmButton: 'btn btn-primary'
                }
            });
        }
    </script>
	
	
<%--	<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css"/>--%>
<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
<script type="text/javascript">




    $(".input_date_new").flatpickr({
        //enableTime: true,
        dateFormat: "m/d/Y"

    });

    $(".input_time_new").flatpickr({
        enableTime: true,
        dateFormat: "H:i",
        noCalendar: true,

    });
</script>


</html>