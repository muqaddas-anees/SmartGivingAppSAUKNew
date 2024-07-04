<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Target.aspx.cs" Inherits="DeffinityAppDev.App.Targets.Target" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex flex-wrap flex-stack pt-10 pb-8" data-select2-id="select2-data-148-dkpk">
									<!--begin::Heading-->
									<h3 class="fw-bolder my-2">Project Targets
									<span class="fs-6 text-gray-400 fw-bold ms-1">by Recent Updates ↓</span></h3>
									<!--end::Heading-->
									<!--begin::Controls-->
									<div class="d-flex flex-wrap my-1" data-select2-id="select2-data-147-3cc4">
										<!--begin::Tab nav-->
										<ul class="nav nav-pills me-5">
											<li class="nav-item m-0">
												<a class="btn btn-sm btn-icon btn-light btn-color-muted btn-active-primary active me-3" data-bs-toggle="tab" href="#kt_project_targets_card_pane">
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
												</a>
											</li>
											<li class="nav-item m-0">
												<a class="btn btn-sm btn-icon btn-light btn-color-muted btn-active-primary" data-bs-toggle="tab" href="#kt_project_targets_table_pane">
													<!--begin::Svg Icon | path: icons/duotune/abstract/abs015.svg-->
													<span class="svg-icon svg-icon-2">
														<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
															<path d="M21 7H3C2.4 7 2 6.6 2 6V4C2 3.4 2.4 3 3 3H21C21.6 3 22 3.4 22 4V6C22 6.6 21.6 7 21 7Z" fill="black"></path>
															<path opacity="0.3" d="M21 14H3C2.4 14 2 13.6 2 13V11C2 10.4 2.4 10 3 10H21C21.6 10 22 10.4 22 11V13C22 13.6 21.6 14 21 14ZM22 20V18C22 17.4 21.6 17 21 17H3C2.4 17 2 17.4 2 18V20C2 20.6 2.4 21 3 21H21C21.6 21 22 20.6 22 20Z" fill="black"></path>
														</svg>
													</span>
													<!--end::Svg Icon-->
												</a>
											</li>
										</ul>
										<!--end::Tab nav-->
										<!--begin::Wrapper-->
										<div class="my-0">
											<!--begin::Select-->
											<select name="status" data-control="select2" data-hide-search="true" class="form-select form-select-white form-select-sm w-150px select2-hidden-accessible" data-select2-id="select2-data-64-h81u" tabindex="-1" aria-hidden="true">
												<option value="1" selected="selected" data-select2-id="select2-data-66-wkdw">Recently Updated</option>
												<option value="2" data-select2-id="select2-data-149-3wkt">Last Month</option>
												<option value="3" data-select2-id="select2-data-150-7fcq">Last Quarter</option>
												<option value="4" data-select2-id="select2-data-151-rg11">Last Year</option>
											</select>
											<%--<span class="select2 select2-container select2-container--bootstrap5 select2-container--below" dir="ltr" data-select2-id="select2-data-65-80a9" style="width: 100%;">
												<span class="selection">
													<span class="select2-selection select2-selection--single form-select form-select-white form-select-sm w-150px" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-status-x8-container" aria-controls="select2-status-x8-container">
												<span class="select2-selection__rendered" id="select2-status-x8-container" role="textbox" aria-readonly="true" title="Recently Updated">Recently Updated</span>
												<span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span>

												                        </span>

												</span>
												<span class="dropdown-wrapper" aria-hidden="true"></span>

											</span>--%>
											<!--end::Select-->
										</div>
										<!--end::Wrapper-->
									</div>
									<!--end::Controls-->
								</div>

	<div id="kt_project_targets_card_pane" class="tab-pane fade show active" data-select2-id="select2-data-kt_project_targets_card_pane">
										<!--begin::Row-->
										<div class="row g-9" data-select2-id="select2-data-137-qw49">
											<!--begin::Col-->
											<div class="col-md-4 col-lg-12 col-xl-4" data-select2-id="select2-data-136-9n3x">
												<!--begin::Col header-->
												<div class="mb-9">
													<div class="d-flex flex-stack">
														<div class="fw-bolder fs-4">Yet to start
														<span class="fs-6 text-gray-400 ms-2">2</span></div>
														<!--begin::Menu-->
														<div>
															<button type="button" class="btn btn-sm btn-icon btn-color-light-dark btn-active-light-primary" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end" data-kt-menu-flip="top-end">
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
															<!--begin::Menu 1-->
															<div class="menu menu-sub menu-sub-dropdown w-250px w-md-300px" data-kt-menu="true" id="kt_menu_61233aa3e378a" style="">
																<!--begin::Header-->
																<div class="px-7 py-5">
																	<div class="fs-5 text-dark fw-bolder">Filter Options</div>
																</div>
																<!--end::Header-->
																<!--begin::Menu separator-->
																<div class="separator border-gray-200"></div>
																<!--end::Menu separator-->
																<!--begin::Form-->
																<div class="px-7 py-5">
																	<!--begin::Input group-->
																	<div class="mb-10" data-select2-id="select2-data-135-xdjj">
																		<!--begin::Label-->
																		<label class="form-label fw-bold">Status:</label>
																		<!--end::Label-->
																		<!--begin::Input-->
																		<div data-select2-id="select2-data-134-3ws3">
																			<select class="form-select form-select-solid select2-hidden-accessible" data-kt-select2="true" data-placeholder="Select option" data-dropdown-parent="#kt_menu_61233aa3e378a" data-allow-clear="true" data-select2-id="select2-data-67-dpg1" tabindex="-1" aria-hidden="true">
																				<option data-select2-id="select2-data-69-yhsk"></option>
																				<option value="1" data-select2-id="select2-data-142-40w7">Approved</option>
																				<option value="2" data-select2-id="select2-data-143-ixcd">Pending</option>
																				<option value="2" data-select2-id="select2-data-144-ewxi">In Process</option>
																				<option value="2" data-select2-id="select2-data-145-stt8">Rejected</option>
																			</select><span class="select2 select2-container select2-container--bootstrap5 select2-container--below" dir="ltr" data-select2-id="select2-data-68-ev5l" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-by7s-container" aria-controls="select2-by7s-container"><span class="select2-selection__rendered" id="select2-by7s-container" role="textbox" aria-readonly="true" title="Select option"><span class="select2-selection__placeholder">Select option</span></span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
																		</div>
																		<!--end::Input-->
																	</div>
																	<!--end::Input group-->
																	<!--begin::Input group-->
																	<div class="mb-10">
																		<!--begin::Label-->
																		<label class="form-label fw-bold">Member Type:</label>
																		<!--end::Label-->
																		<!--begin::Options-->
																		<div class="d-flex">
																			<!--begin::Options-->
																			<label class="form-check form-check-sm form-check-custom form-check-solid me-5">
																				<input class="form-check-input" type="checkbox" value="1">
																				<span class="form-check-label">Author</span>
																			</label>
																			<!--end::Options-->
																			<!--begin::Options-->
																			<label class="form-check form-check-sm form-check-custom form-check-solid">
																				<input class="form-check-input" type="checkbox" value="2" checked="checked">
																				<span class="form-check-label">Customer</span>
																			</label>
																			<!--end::Options-->
																		</div>
																		<!--end::Options-->
																	</div>
																	<!--end::Input group-->
																	<!--begin::Input group-->
																	<div class="mb-10">
																		<!--begin::Label-->
																		<label class="form-label fw-bold">Notifications:</label>
																		<!--end::Label-->
																		<!--begin::Switch-->
																		<div class="form-check form-switch form-switch-sm form-check-custom form-check-solid">
																			<input class="form-check-input" type="checkbox" value="" name="notifications" checked="checked">
																			<label class="form-check-label">Enabled</label>
																		</div>
																		<!--end::Switch-->
																	</div>
																	<!--end::Input group-->
																	<!--begin::Actions-->
																	<div class="d-flex justify-content-end">
																		<button type="reset" class="btn btn-sm btn-light btn-active-light-primary me-2" data-kt-menu-dismiss="true">Reset</button>
																		<button type="submit" class="btn btn-sm btn-primary" data-kt-menu-dismiss="true">Apply</button>
																	</div>
																	<!--end::Actions-->
																</div>
																<!--end::Form-->
															</div>
															<!--end::Menu 1-->
														</div>
														<!--end::Menu-->
													</div>
													<div class="h-3px w-100 bg-warning"></div>
												</div>
												<!--end::Col header-->
												<!--begin::Card-->
												<div class="card mb-6 mb-xl-9">
													<!--begin::Card body-->
													<div class="card-body">
														<!--begin::Header-->
														<div class="d-flex flex-stack mb-3">
															<!--begin::Badge-->
															<div class="badge badge-light">Phase 2.6 QA</div>
															<!--end::Badge-->
															<!--begin::Menu-->
															<div>
																<button type="button" class="btn btn-sm btn-icon btn-color-light-dark btn-active-light-primary" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end" data-kt-menu-flip="top-end">
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
																<div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-800 menu-state-bg-light-primary fw-bold w-200px py-3" data-kt-menu="true">
																	<!--begin::Heading-->
																	<div class="menu-item px-3">
																		<div class="menu-content text-muted pb-2 px-3 fs-7 text-uppercase">Payments</div>
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
															</div>
															<!--end::Menu-->
														</div>
														<!--end::Header-->
														<!--begin::Title-->
														<div class="mb-2">
															<a href="#" class="fs-4 fw-bolder mb-1 text-gray-900 text-hover-primary">User Module Testing</a>
														</div>
														<!--end::Title-->
														<!--begin::Content-->
														<div class="fs-6 fw-bold text-gray-600 mb-5">First, a disclaimer – the entire process writing a blog post often.</div>
														<!--end::Content-->
														<!--begin::Footer-->
														<div class="d-flex flex-stack flex-wrapr">
															<!--begin::Users-->
															<div class="symbol-group symbol-hover my-1">
																<div class="symbol symbol-35px symbol-circle" data-bs-toggle="tooltip" title="" data-bs-original-title="Alan Warden">
																	<span class="symbol-label bg-warning text-inverse-warning fw-bolder">A</span>
																</div>
																<div class="symbol symbol-35px symbol-circle" data-bs-toggle="tooltip" title="" data-bs-original-title="Perry Matthew">
																	<span class="symbol-label bg-success text-inverse-success fw-bolder">R</span>
																</div>
															</div>
															<!--end::Users-->
															<!--begin::Stats-->
															<div class="d-flex my-1">
																<!--begin::Stat-->
																<div class="border border-dashed border-gray-300 rounded py-2 px-3">
																	<!--begin::Svg Icon | path: icons/duotune/communication/com008.svg-->
																	<span class="svg-icon svg-icon-3">
																		<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																			<path opacity="0.3" d="M4.425 20.525C2.525 18.625 2.525 15.525 4.425 13.525L14.825 3.125C16.325 1.625 18.825 1.625 20.425 3.125C20.825 3.525 20.825 4.12502 20.425 4.52502C20.025 4.92502 19.425 4.92502 19.025 4.52502C18.225 3.72502 17.025 3.72502 16.225 4.52502L5.82499 14.925C4.62499 16.125 4.62499 17.925 5.82499 19.125C7.02499 20.325 8.82501 20.325 10.025 19.125L18.425 10.725C18.825 10.325 19.425 10.325 19.825 10.725C20.225 11.125 20.225 11.725 19.825 12.125L11.425 20.525C9.525 22.425 6.425 22.425 4.425 20.525Z" fill="black"></path>
																			<path d="M9.32499 15.625C8.12499 14.425 8.12499 12.625 9.32499 11.425L14.225 6.52498C14.625 6.12498 15.225 6.12498 15.625 6.52498C16.025 6.92498 16.025 7.525 15.625 7.925L10.725 12.8249C10.325 13.2249 10.325 13.8249 10.725 14.2249C11.125 14.6249 11.725 14.6249 12.125 14.2249L19.125 7.22493C19.525 6.82493 19.725 6.425 19.725 5.925C19.725 5.325 19.525 4.825 19.125 4.425C18.725 4.025 18.725 3.42498 19.125 3.02498C19.525 2.62498 20.125 2.62498 20.525 3.02498C21.325 3.82498 21.725 4.825 21.725 5.925C21.725 6.925 21.325 7.82498 20.525 8.52498L13.525 15.525C12.325 16.725 10.525 16.725 9.32499 15.625Z" fill="black"></path>
																		</svg>
																	</span>
																	<!--end::Svg Icon-->
																	<span class="ms-1 fs-7 fw-bolder text-gray-600">6</span>
																</div>
																<!--end::Stat-->
																<!--begin::Stat-->
																<div class="border border-dashed border-gray-300 rounded py-2 px-3 ms-3">
																	<!--begin::Svg Icon | path: icons/duotune/communication/com012.svg-->
																	<span class="svg-icon svg-icon-3">
																		<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																			<path opacity="0.3" d="M20 3H4C2.89543 3 2 3.89543 2 5V16C2 17.1046 2.89543 18 4 18H4.5C5.05228 18 5.5 18.4477 5.5 19V21.5052C5.5 22.1441 6.21212 22.5253 6.74376 22.1708L11.4885 19.0077C12.4741 18.3506 13.6321 18 14.8167 18H20C21.1046 18 22 17.1046 22 16V5C22 3.89543 21.1046 3 20 3Z" fill="black"></path>
																			<rect x="6" y="12" width="7" height="2" rx="1" fill="black"></rect>
																			<rect x="6" y="7" width="12" height="2" rx="1" fill="black"></rect>
																		</svg>
																	</span>
																	<!--end::Svg Icon-->
																	<span class="ms-1 fs-7 fw-bolder text-gray-600">5</span>
																</div>
																<!--end::Stat-->
															</div>
															<!--end::Stats-->
														</div>
														<!--end::Footer-->
													</div>
													<!--end::Card body-->
												</div>
												<!--end::Card-->
												<a href="#" class="btn btn-primary er w-100 fs-6 px-8 py-4" data-bs-toggle="modal" data-bs-target="#kt_modal_new_target">Create New Target</a>
											</div>
											<!--end::Col-->
											<!--begin::Col-->
											<div class="col-md-4 col-lg-12 col-xl-4">
												<!--begin::Col header-->
												<div class="mb-9">
													<div class="d-flex flex-stack">
														<div class="fw-bolder fs-4">In Progress
														<span class="fs-6 text-gray-400 ms-2">4</span></div>
														<!--begin::Menu-->
														<div>
															<button type="button" class="btn btn-sm btn-icon btn-color-light-dark btn-active-light-primary" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end" data-kt-menu-flip="top-end">
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
															<!--begin::Menu 1-->
															<div class="menu menu-sub menu-sub-dropdown w-250px w-md-300px" data-kt-menu="true" id="kt_menu_61233aa3e62d0">
																<!--begin::Header-->
																<div class="px-7 py-5">
																	<div class="fs-5 text-dark fw-bolder">Filter Options</div>
																</div>
																<!--end::Header-->
																<!--begin::Menu separator-->
																<div class="separator border-gray-200"></div>
																<!--end::Menu separator-->
																<!--begin::Form-->
																<div class="px-7 py-5">
																	<!--begin::Input group-->
																	<div class="mb-10">
																		<!--begin::Label-->
																		<label class="form-label fw-bold">Status:</label>
																		<!--end::Label-->
																		<!--begin::Input-->
																		<div>
																			<select class="form-select form-select-solid select2-hidden-accessible" data-kt-select2="true" data-placeholder="Select option" data-dropdown-parent="#kt_menu_61233aa3e62d0" data-allow-clear="true" data-select2-id="select2-data-70-uya9" tabindex="-1" aria-hidden="true">
																				<option data-select2-id="select2-data-72-dsgl"></option>
																				<option value="1">Approved</option>
																				<option value="2">Pending</option>
																				<option value="2">In Process</option>
																				<option value="2">Rejected</option>
																			</select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-71-o4gm" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-lqa2-container" aria-controls="select2-lqa2-container"><span class="select2-selection__rendered" id="select2-lqa2-container" role="textbox" aria-readonly="true" title="Select option"><span class="select2-selection__placeholder">Select option</span></span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
																		</div>
																		<!--end::Input-->
																	</div>
																	<!--end::Input group-->
																	<!--begin::Input group-->
																	<div class="mb-10">
																		<!--begin::Label-->
																		<label class="form-label fw-bold">Member Type:</label>
																		<!--end::Label-->
																		<!--begin::Options-->
																		<div class="d-flex">
																			<!--begin::Options-->
																			<label class="form-check form-check-sm form-check-custom form-check-solid me-5">
																				<input class="form-check-input" type="checkbox" value="1">
																				<span class="form-check-label">Author</span>
																			</label>
																			<!--end::Options-->
																			<!--begin::Options-->
																			<label class="form-check form-check-sm form-check-custom form-check-solid">
																				<input class="form-check-input" type="checkbox" value="2" checked="checked">
																				<span class="form-check-label">Customer</span>
																			</label>
																			<!--end::Options-->
																		</div>
																		<!--end::Options-->
																	</div>
																	<!--end::Input group-->
																	<!--begin::Input group-->
																	<div class="mb-10">
																		<!--begin::Label-->
																		<label class="form-label fw-bold">Notifications:</label>
																		<!--end::Label-->
																		<!--begin::Switch-->
																		<div class="form-check form-switch form-switch-sm form-check-custom form-check-solid">
																			<input class="form-check-input" type="checkbox" value="" name="notifications" checked="checked">
																			<label class="form-check-label">Enabled</label>
																		</div>
																		<!--end::Switch-->
																	</div>
																	<!--end::Input group-->
																	<!--begin::Actions-->
																	<div class="d-flex justify-content-end">
																		<button type="reset" class="btn btn-sm btn-light btn-active-light-primary me-2" data-kt-menu-dismiss="true">Reset</button>
																		<button type="submit" class="btn btn-sm btn-primary" data-kt-menu-dismiss="true">Apply</button>
																	</div>
																	<!--end::Actions-->
																</div>
																<!--end::Form-->
															</div>
															<!--end::Menu 1-->
														</div>
														<!--end::Menu-->
													</div>
													<div class="h-3px w-100 bg-primary"></div>
												</div>
												<!--end::Col header-->
												<div class="card mb-6 mb-xl-9">
													<!--begin::Card body-->
													<div class="card-body">
														<!--begin::Header-->
														<div class="d-flex flex-stack mb-3">
															<!--begin::Badge-->
															<div class="badge badge-light">Phase 2.6 QA</div>
															<!--end::Badge-->
															<!--begin::Menu-->
															<div>
																<button type="button" class="btn btn-sm btn-icon btn-color-light-dark btn-active-light-primary" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end" data-kt-menu-flip="top-end">
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
																<div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-800 menu-state-bg-light-primary fw-bold w-200px py-3" data-kt-menu="true">
																	<!--begin::Heading-->
																	<div class="menu-item px-3">
																		<div class="menu-content text-muted pb-2 px-3 fs-7 text-uppercase">Payments</div>
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
															</div>
															<!--end::Menu-->
														</div>
														<!--end::Header-->
														<!--begin::Title-->
														<div class="mb-2">
															<a href="#" class="fs-4 fw-bolder mb-1 text-gray-900 text-hover-primary">User Module Testing</a>
														</div>
														<!--end::Title-->
														<!--begin::Content-->
														<div class="fs-6 fw-bold text-gray-600 mb-5">First, a disclaimer – the entire process writing a blog post often.</div>
														<!--end::Content-->
														<!--begin::Footer-->
														<div class="d-flex flex-stack flex-wrapr">
															<!--begin::Users-->
															<div class="symbol-group symbol-hover my-1">
																<div class="symbol symbol-35px symbol-circle" data-bs-toggle="tooltip" title="" data-bs-original-title="Alan Warden">
																	<span class="symbol-label bg-warning text-inverse-warning fw-bolder">A</span>
																</div>
																<div class="symbol symbol-35px symbol-circle" data-bs-toggle="tooltip" title="" data-bs-original-title="Perry Matthew">
																	<span class="symbol-label bg-success text-inverse-success fw-bolder">R</span>
																</div>
															</div>
															<!--end::Users-->
															<!--begin::Stats-->
															<div class="d-flex my-1">
																<!--begin::Stat-->
																<div class="border border-dashed border-gray-300 rounded py-2 px-3">
																	<!--begin::Svg Icon | path: icons/duotune/communication/com008.svg-->
																	<span class="svg-icon svg-icon-3">
																		<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																			<path opacity="0.3" d="M4.425 20.525C2.525 18.625 2.525 15.525 4.425 13.525L14.825 3.125C16.325 1.625 18.825 1.625 20.425 3.125C20.825 3.525 20.825 4.12502 20.425 4.52502C20.025 4.92502 19.425 4.92502 19.025 4.52502C18.225 3.72502 17.025 3.72502 16.225 4.52502L5.82499 14.925C4.62499 16.125 4.62499 17.925 5.82499 19.125C7.02499 20.325 8.82501 20.325 10.025 19.125L18.425 10.725C18.825 10.325 19.425 10.325 19.825 10.725C20.225 11.125 20.225 11.725 19.825 12.125L11.425 20.525C9.525 22.425 6.425 22.425 4.425 20.525Z" fill="black"></path>
																			<path d="M9.32499 15.625C8.12499 14.425 8.12499 12.625 9.32499 11.425L14.225 6.52498C14.625 6.12498 15.225 6.12498 15.625 6.52498C16.025 6.92498 16.025 7.525 15.625 7.925L10.725 12.8249C10.325 13.2249 10.325 13.8249 10.725 14.2249C11.125 14.6249 11.725 14.6249 12.125 14.2249L19.125 7.22493C19.525 6.82493 19.725 6.425 19.725 5.925C19.725 5.325 19.525 4.825 19.125 4.425C18.725 4.025 18.725 3.42498 19.125 3.02498C19.525 2.62498 20.125 2.62498 20.525 3.02498C21.325 3.82498 21.725 4.825 21.725 5.925C21.725 6.925 21.325 7.82498 20.525 8.52498L13.525 15.525C12.325 16.725 10.525 16.725 9.32499 15.625Z" fill="black"></path>
																		</svg>
																	</span>
																	<!--end::Svg Icon-->
																	<span class="ms-1 fs-7 fw-bolder text-gray-600">6</span>
																</div>
																<!--end::Stat-->
																<!--begin::Stat-->
																<div class="border border-dashed border-gray-300 rounded py-2 px-3 ms-3">
																	<!--begin::Svg Icon | path: icons/duotune/communication/com012.svg-->
																	<span class="svg-icon svg-icon-3">
																		<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																			<path opacity="0.3" d="M20 3H4C2.89543 3 2 3.89543 2 5V16C2 17.1046 2.89543 18 4 18H4.5C5.05228 18 5.5 18.4477 5.5 19V21.5052C5.5 22.1441 6.21212 22.5253 6.74376 22.1708L11.4885 19.0077C12.4741 18.3506 13.6321 18 14.8167 18H20C21.1046 18 22 17.1046 22 16V5C22 3.89543 21.1046 3 20 3Z" fill="black"></path>
																			<rect x="6" y="12" width="7" height="2" rx="1" fill="black"></rect>
																			<rect x="6" y="7" width="12" height="2" rx="1" fill="black"></rect>
																		</svg>
																	</span>
																	<!--end::Svg Icon-->
																	<span class="ms-1 fs-7 fw-bolder text-gray-600">5</span>
																</div>
																<!--end::Stat-->
															</div>
															<!--end::Stats-->
														</div>
														<!--end::Footer-->
													</div>
													<!--end::Card body-->
												</div>
											</div>
											<!--end::Col-->
											<!--begin::Col-->
											<div class="col-md-4 col-lg-12 col-xl-4">
												<!--begin::Col header-->
												<div class="mb-9">
													<div class="d-flex flex-stack">
														<div class="fw-bolder fs-4">Completed
														<span class="fs-6 text-gray-400 ms-2">3</span></div>
														<!--begin::Menu-->
														<div>
															<button type="button" class="btn btn-sm btn-icon btn-color-light-dark btn-active-light-primary" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end" data-kt-menu-flip="top-end">
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
															<!--begin::Menu 1-->
															<div class="menu menu-sub menu-sub-dropdown w-250px w-md-300px" data-kt-menu="true" id="kt_menu_61233aa3eb6fe">
																<!--begin::Header-->
																<div class="px-7 py-5">
																	<div class="fs-5 text-dark fw-bolder">Filter Options</div>
																</div>
																<!--end::Header-->
																<!--begin::Menu separator-->
																<div class="separator border-gray-200"></div>
																<!--end::Menu separator-->
																<!--begin::Form-->
																<div class="px-7 py-5">
																	<!--begin::Input group-->
																	<div class="mb-10">
																		<!--begin::Label-->
																		<label class="form-label fw-bold">Status:</label>
																		<!--end::Label-->
																		<!--begin::Input-->
																		<div>
																			<select class="form-select form-select-solid select2-hidden-accessible" data-kt-select2="true" data-placeholder="Select option" data-dropdown-parent="#kt_menu_61233aa3eb6fe" data-allow-clear="true" data-select2-id="select2-data-73-kasx" tabindex="-1" aria-hidden="true">
																				<option data-select2-id="select2-data-75-6s79"></option>
																				<option value="1">Approved</option>
																				<option value="2">Pending</option>
																				<option value="2">In Process</option>
																				<option value="2">Rejected</option>
																			</select><span class="select2 select2-container select2-container--bootstrap5" dir="ltr" data-select2-id="select2-data-74-jc1s" style="width: 100%;"><span class="selection"><span class="select2-selection select2-selection--single form-select form-select-solid" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-g5fj-container" aria-controls="select2-g5fj-container"><span class="select2-selection__rendered" id="select2-g5fj-container" role="textbox" aria-readonly="true" title="Select option"><span class="select2-selection__placeholder">Select option</span></span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span></span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
																		</div>
																		<!--end::Input-->
																	</div>
																	<!--end::Input group-->
																	<!--begin::Input group-->
																	<div class="mb-10">
																		<!--begin::Label-->
																		<label class="form-label fw-bold">Member Type:</label>
																		<!--end::Label-->
																		<!--begin::Options-->
																		<div class="d-flex">
																			<!--begin::Options-->
																			<label class="form-check form-check-sm form-check-custom form-check-solid me-5">
																				<input class="form-check-input" type="checkbox" value="1">
																				<span class="form-check-label">Author</span>
																			</label>
																			<!--end::Options-->
																			<!--begin::Options-->
																			<label class="form-check form-check-sm form-check-custom form-check-solid">
																				<input class="form-check-input" type="checkbox" value="2" checked="checked">
																				<span class="form-check-label">Customer</span>
																			</label>
																			<!--end::Options-->
																		</div>
																		<!--end::Options-->
																	</div>
																	<!--end::Input group-->
																	<!--begin::Input group-->
																	<div class="mb-10">
																		<!--begin::Label-->
																		<label class="form-label fw-bold">Notifications:</label>
																		<!--end::Label-->
																		<!--begin::Switch-->
																		<div class="form-check form-switch form-switch-sm form-check-custom form-check-solid">
																			<input class="form-check-input" type="checkbox" value="" name="notifications" checked="checked">
																			<label class="form-check-label">Enabled</label>
																		</div>
																		<!--end::Switch-->
																	</div>
																	<!--end::Input group-->
																	<!--begin::Actions-->
																	<div class="d-flex justify-content-end">
																		<button type="reset" class="btn btn-sm btn-light btn-active-light-primary me-2" data-kt-menu-dismiss="true">Reset</button>
																		<button type="submit" class="btn btn-sm btn-primary" data-kt-menu-dismiss="true">Apply</button>
																	</div>
																	<!--end::Actions-->
																</div>
																<!--end::Form-->
															</div>
															<!--end::Menu 1-->
														</div>
														<!--end::Menu-->
													</div>
													<div class="h-3px w-100 bg-success"></div>
												</div>
												<!--end::Col header-->
												<!--begin::Card-->
												<div class="card mb-6 mb-xl-9">
													<!--begin::Card body-->
													<div class="card-body">
														<!--begin::Header-->
														<div class="d-flex flex-stack mb-3">
															<!--begin::Badge-->
															<div class="badge badge-light">UI Design</div>
															<!--end::Badge-->
															<!--begin::Menu-->
															<div>
																<button type="button" class="btn btn-sm btn-icon btn-color-light-dark btn-active-light-primary" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end" data-kt-menu-flip="top-end">
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
																<div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-800 menu-state-bg-light-primary fw-bold w-200px py-3" data-kt-menu="true">
																	<!--begin::Heading-->
																	<div class="menu-item px-3">
																		<div class="menu-content text-muted pb-2 px-3 fs-7 text-uppercase">Payments</div>
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
															</div>
															<!--end::Menu-->
														</div>
														<!--end::Header-->
														<!--begin::Title-->
														<div class="mb-2">
															<a href="#" class="fs-4 fw-bolder mb-1 text-gray-900 text-hover-primary">Branding Logo</a>
														</div>
														<!--end::Title-->
														<!--begin::Content-->
														<div class="fs-6 fw-bold text-gray-600 mb-5">First, a disclaimer – the entire process writing a blog post often takes a couple of hours if you can type</div>
														<!--end::Content-->
														<!--begin::Footer-->
														<div class="d-flex flex-stack flex-wrapr">
															<!--begin::Users-->
															<div class="symbol-group symbol-hover my-1">
																<div class="symbol symbol-35px symbol-circle" data-bs-toggle="tooltip" title="" data-bs-original-title="Perry Matthew">
																	<span class="symbol-label bg-success text-inverse-success fw-bolder">R</span>
																</div>
																<div class="symbol symbol-35px symbol-circle" data-bs-toggle="tooltip" title="" data-bs-original-title="Barry Walter">
																	<img alt="Pic" src="assets/media/avatars/150-7.jpg">
																</div>
																<div class="symbol symbol-35px symbol-circle" data-bs-toggle="tooltip" title="" data-bs-original-title="Susan Redwood">
																	<span class="symbol-label bg-primary text-inverse-primary fw-bolder">S</span>
																</div>
															</div>
															<!--end::Users-->
															<!--begin::Stats-->
															<div class="d-flex my-1">
																<!--begin::Stat-->
																<div class="border border-dashed border-gray-300 rounded py-2 px-3">
																	<!--begin::Svg Icon | path: icons/duotune/communication/com008.svg-->
																	<span class="svg-icon svg-icon-3">
																		<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																			<path opacity="0.3" d="M4.425 20.525C2.525 18.625 2.525 15.525 4.425 13.525L14.825 3.125C16.325 1.625 18.825 1.625 20.425 3.125C20.825 3.525 20.825 4.12502 20.425 4.52502C20.025 4.92502 19.425 4.92502 19.025 4.52502C18.225 3.72502 17.025 3.72502 16.225 4.52502L5.82499 14.925C4.62499 16.125 4.62499 17.925 5.82499 19.125C7.02499 20.325 8.82501 20.325 10.025 19.125L18.425 10.725C18.825 10.325 19.425 10.325 19.825 10.725C20.225 11.125 20.225 11.725 19.825 12.125L11.425 20.525C9.525 22.425 6.425 22.425 4.425 20.525Z" fill="black"></path>
																			<path d="M9.32499 15.625C8.12499 14.425 8.12499 12.625 9.32499 11.425L14.225 6.52498C14.625 6.12498 15.225 6.12498 15.625 6.52498C16.025 6.92498 16.025 7.525 15.625 7.925L10.725 12.8249C10.325 13.2249 10.325 13.8249 10.725 14.2249C11.125 14.6249 11.725 14.6249 12.125 14.2249L19.125 7.22493C19.525 6.82493 19.725 6.425 19.725 5.925C19.725 5.325 19.525 4.825 19.125 4.425C18.725 4.025 18.725 3.42498 19.125 3.02498C19.525 2.62498 20.125 2.62498 20.525 3.02498C21.325 3.82498 21.725 4.825 21.725 5.925C21.725 6.925 21.325 7.82498 20.525 8.52498L13.525 15.525C12.325 16.725 10.525 16.725 9.32499 15.625Z" fill="black"></path>
																		</svg>
																	</span>
																	<!--end::Svg Icon-->
																	<span class="ms-1 fs-7 fw-bolder text-gray-600">10</span>
																</div>
																<!--end::Stat-->
																<!--begin::Stat-->
																<div class="border border-dashed border-gray-300 rounded py-2 px-3 ms-3">
																	<!--begin::Svg Icon | path: icons/duotune/communication/com012.svg-->
																	<span class="svg-icon svg-icon-3">
																		<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																			<path opacity="0.3" d="M20 3H4C2.89543 3 2 3.89543 2 5V16C2 17.1046 2.89543 18 4 18H4.5C5.05228 18 5.5 18.4477 5.5 19V21.5052C5.5 22.1441 6.21212 22.5253 6.74376 22.1708L11.4885 19.0077C12.4741 18.3506 13.6321 18 14.8167 18H20C21.1046 18 22 17.1046 22 16V5C22 3.89543 21.1046 3 20 3Z" fill="black"></path>
																			<rect x="6" y="12" width="7" height="2" rx="1" fill="black"></rect>
																			<rect x="6" y="7" width="12" height="2" rx="1" fill="black"></rect>
																		</svg>
																	</span>
																	<!--end::Svg Icon-->
																	<span class="ms-1 fs-7 fw-bolder text-gray-600">1</span>
																</div>
																<!--end::Stat-->
															</div>
															<!--end::Stats-->
														</div>
														<!--end::Footer-->
													</div>
													<!--end::Card body-->
												</div>
												<!--end::Card-->
												
											</div>
											<!--end::Col-->
										</div>
										<!--end::Row-->
									</div>


		<!--begin::Modal - New Target-->
								<div class="modal fade" id="kt_modal_new_target" tabindex="-1" aria-hidden="true">
									<!--begin::Modal dialog-->
									<div class="modal-dialog modal-dialog-centered mw-650px">
										<!--begin::Modal content-->
										<div class="modal-content rounded">
											<!--begin::Modal header-->
											<div class="modal-header pb-0 border-0 justify-content-end">
												<!--begin::Close-->
												<div class="btn btn-sm btn-icon btn-active-color-primary" data-bs-dismiss="modal">
													<!--begin::Svg Icon | path: icons/duotune/arrows/arr061.svg-->
													<span class="svg-icon svg-icon-1">
														<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
															<rect opacity="0.5" x="6" y="17.3137" width="16" height="2" rx="1" transform="rotate(-45 6 17.3137)" fill="black" />
															<rect x="7.41422" y="6" width="16" height="2" rx="1" transform="rotate(45 7.41422 6)" fill="black" />
														</svg>
													</span>
													<!--end::Svg Icon-->
												</div>
												<!--end::Close-->
											</div>
											<!--begin::Modal header-->
											<!--begin::Modal body-->
											<div class="modal-body scroll-y px-10 px-lg-15 pt-0 pb-15">
												<!--begin:Form-->
												<form id="kt_modal_new_target_form" class="form" action="#">
													<!--begin::Heading-->
													<div class="mb-13 text-center">
														<!--begin::Title-->
														<h1 class="mb-3">Set First Target</h1>
														<!--end::Title-->
													<%--	<!--begin::Description-->
														<div class="text-muted fw-bold fs-5">If you need more info, please check
														<a href="#" class="fw-bolder link-primary">Project Guidelines</a>.</div>
														<!--end::Description-->--%>
													</div>
													<!--end::Heading-->
													<!--begin::Input group-->
													<div class="d-flex flex-column mb-8 fv-row">
														<!--begin::Label-->
														<label class="d-flex align-items-center fs-6 fw-bold mb-2">
															<span class="required">Target Title</span>
															<i class="fas fa-exclamation-circle ms-2 fs-7" data-bs-toggle="tooltip" title="Specify a target name for future usage and reference"></i>
														</label>
														<!--end::Label-->
														<input type="text" class="form-control form-control-solid" placeholder="Enter Target Title" name="target_title" />
													</div>
													<!--end::Input group-->
													<!--begin::Input group-->
													<div class="row g-9 mb-8">
														<!--begin::Col-->
														<div class="col-md-6 fv-row">
															<label class="required fs-6 fw-bold mb-2">Assign</label>
															<select class="form-select form-select-solid" data-control="select2" data-hide-search="true" data-placeholder="Select a Team Member" name="target_assign">
																<option value="">Select user...</option>
																<option value="1">Karina Clark</option>
																<option value="2">Robert Doe</option>
																<option value="3">Niel Owen</option>
																<option value="4">Olivia Wild</option>
																<option value="5">Sean Bean</option>
															</select>
														</div>
														<!--end::Col-->
														<!--begin::Col-->
														<div class="col-md-6 fv-row">
															<label class="required fs-6 fw-bold mb-2">Due Date</label>
															<!--begin::Input-->
															<div class="position-relative d-flex align-items-center">
																<!--begin::Icon-->
																<!--begin::Svg Icon | path: icons/duotune/general/gen014.svg-->
																<span class="svg-icon svg-icon-2 position-absolute mx-4">
																	<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																		<path opacity="0.3" d="M21 22H3C2.4 22 2 21.6 2 21V5C2 4.4 2.4 4 3 4H21C21.6 4 22 4.4 22 5V21C22 21.6 21.6 22 21 22Z" fill="black" />
																		<path d="M6 6C5.4 6 5 5.6 5 5V3C5 2.4 5.4 2 6 2C6.6 2 7 2.4 7 3V5C7 5.6 6.6 6 6 6ZM11 5V3C11 2.4 10.6 2 10 2C9.4 2 9 2.4 9 3V5C9 5.6 9.4 6 10 6C10.6 6 11 5.6 11 5ZM15 5V3C15 2.4 14.6 2 14 2C13.4 2 13 2.4 13 3V5C13 5.6 13.4 6 14 6C14.6 6 15 5.6 15 5ZM19 5V3C19 2.4 18.6 2 18 2C17.4 2 17 2.4 17 3V5C17 5.6 17.4 6 18 6C18.6 6 19 5.6 19 5Z" fill="black" />
																		<path d="M8.8 13.1C9.2 13.1 9.5 13 9.7 12.8C9.9 12.6 10.1 12.3 10.1 11.9C10.1 11.6 10 11.3 9.8 11.1C9.6 10.9 9.3 10.8 9 10.8C8.8 10.8 8.59999 10.8 8.39999 10.9C8.19999 11 8.1 11.1 8 11.2C7.9 11.3 7.8 11.4 7.7 11.6C7.6 11.8 7.5 11.9 7.5 12.1C7.5 12.2 7.4 12.2 7.3 12.3C7.2 12.4 7.09999 12.4 6.89999 12.4C6.69999 12.4 6.6 12.3 6.5 12.2C6.4 12.1 6.3 11.9 6.3 11.7C6.3 11.5 6.4 11.3 6.5 11.1C6.6 10.9 6.8 10.7 7 10.5C7.2 10.3 7.49999 10.1 7.89999 10C8.29999 9.90003 8.60001 9.80003 9.10001 9.80003C9.50001 9.80003 9.80001 9.90003 10.1 10C10.4 10.1 10.7 10.3 10.9 10.4C11.1 10.5 11.3 10.8 11.4 11.1C11.5 11.4 11.6 11.6 11.6 11.9C11.6 12.3 11.5 12.6 11.3 12.9C11.1 13.2 10.9 13.5 10.6 13.7C10.9 13.9 11.2 14.1 11.4 14.3C11.6 14.5 11.8 14.7 11.9 15C12 15.3 12.1 15.5 12.1 15.8C12.1 16.2 12 16.5 11.9 16.8C11.8 17.1 11.5 17.4 11.3 17.7C11.1 18 10.7 18.2 10.3 18.3C9.9 18.4 9.5 18.5 9 18.5C8.5 18.5 8.1 18.4 7.7 18.2C7.3 18 7 17.8 6.8 17.6C6.6 17.4 6.4 17.1 6.3 16.8C6.2 16.5 6.10001 16.3 6.10001 16.1C6.10001 15.9 6.2 15.7 6.3 15.6C6.4 15.5 6.6 15.4 6.8 15.4C6.9 15.4 7.00001 15.4 7.10001 15.5C7.20001 15.6 7.3 15.6 7.3 15.7C7.5 16.2 7.7 16.6 8 16.9C8.3 17.2 8.6 17.3 9 17.3C9.2 17.3 9.5 17.2 9.7 17.1C9.9 17 10.1 16.8 10.3 16.6C10.5 16.4 10.5 16.1 10.5 15.8C10.5 15.3 10.4 15 10.1 14.7C9.80001 14.4 9.50001 14.3 9.10001 14.3C9.00001 14.3 8.9 14.3 8.7 14.3C8.5 14.3 8.39999 14.3 8.39999 14.3C8.19999 14.3 7.99999 14.2 7.89999 14.1C7.79999 14 7.7 13.8 7.7 13.7C7.7 13.5 7.79999 13.4 7.89999 13.2C7.99999 13 8.2 13 8.5 13H8.8V13.1ZM15.3 17.5V12.2C14.3 13 13.6 13.3 13.3 13.3C13.1 13.3 13 13.2 12.9 13.1C12.8 13 12.7 12.8 12.7 12.6C12.7 12.4 12.8 12.3 12.9 12.2C13 12.1 13.2 12 13.6 11.8C14.1 11.6 14.5 11.3 14.7 11.1C14.9 10.9 15.2 10.6 15.5 10.3C15.8 10 15.9 9.80003 15.9 9.70003C15.9 9.60003 16.1 9.60004 16.3 9.60004C16.5 9.60004 16.7 9.70003 16.8 9.80003C16.9 9.90003 17 10.2 17 10.5V17.2C17 18 16.7 18.4 16.2 18.4C16 18.4 15.8 18.3 15.6 18.2C15.4 18.1 15.3 17.8 15.3 17.5Z" fill="black" />
																	</svg>
																</span>
																<!--end::Svg Icon-->
																<!--end::Icon-->
																<!--begin::Datepicker-->
																<input class="form-control form-control-solid ps-12" placeholder="Select a date" name="due_date" />
																<!--end::Datepicker-->
															</div>
															<!--end::Input-->
														</div>
														<!--end::Col-->
													</div>
													<!--end::Input group-->
													<!--begin::Input group-->
													<div class="d-flex flex-column mb-8">
														<label class="fs-6 fw-bold mb-2">Target Details</label>
														<textarea class="form-control form-control-solid" rows="3" name="target_details" placeholder="Type Target Details"></textarea>
													</div>
													<!--end::Input group-->
													
													
													<!--begin::Actions-->
													<div class="text-center">
														<button type="reset" id="kt_modal_new_target_cancel" class="btn btn-light me-3">Cancel</button>
														<button type="submit" id="kt_modal_new_target_submit" class="btn btn-primary">
															<span class="indicator-label">Submit</span>
															<span class="indicator-progress">Please wait...
															<span class="spinner-border spinner-border-sm align-middle ms-2"></span></span>
														</button>
													</div>
													<!--end::Actions-->
												</form>
												<!--end:Form-->
											</div>
											<!--end::Modal body-->
										</div>
										<!--end::Modal content-->
									</div>
									<!--end::Modal dialog-->
								</div>
								<!--end::Modal - New Target-->

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
