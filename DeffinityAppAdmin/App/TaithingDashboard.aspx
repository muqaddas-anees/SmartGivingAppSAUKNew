<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="TaithingDashboard.aspx.cs" Inherits="DeffinityAppDev.App.TaithingDashboard" %>

<%@ Register Src="~/App/controls/DonationReportTabs.ascx" TagPrefix="Pref" TagName="DonationReportTabs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:DonationReportTabs runat="server" id="DonationReportTabs" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Tithing Dashboard
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
    <asp:Button ID="btnShowdefault" runat="server" CssClass="btn btn-primary" Text="Tithing Default" OnClick="btnShowdefault_Click"  /> &nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btnSetTithingCategory" runat="server" CssClass="btn btn-primary" Text="Tithing Category Settings" OnClick="btnSetTithingCategory_Click"  />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row gy-5 g-xl-10">
								<!--begin::Col-->
								<div class="col-xl-4">
									<!--begin::Mixed Widget 13-->
									<div class="card card-xl-stretch mb-xl-10" style="background-color: #F7D9E3">
										<!--begin::Body-->
										<div class="card-body d-flex flex-column">
											<!--begin::Wrapper-->
											<div class="d-flex flex-column flex-grow-1">
												<!--begin::Title-->
												<a href="#" class="text-dark text-hover-primary fw-bolder fs-3">Donations</a>
												<!--end::Title-->
												<!--begin::Chart-->
												<div class="mixed-widget-13-chart" style="height: 100px"></div>
												<!--end::Chart-->
											</div>
											<!--end::Wrapper-->
											<!--begin::Stats-->
											<div class="pt-5">
												<!--begin::Symbol-->
												<span class="text-dark fw-bolder fs-2x lh-0">$</span>
												<!--end::Symbol-->
												<!--begin::Number-->
												<span class="text-dark fw-bolder fs-3x me-2 lh-0"><asp:Literal ID="lblthisweek" runat="server" Text="0.00"></asp:Literal></span>
												<!--end::Number-->
												<!--begin::Text-->
												<span class="text-dark fw-bolder fs-6 lh-0"> this week</span>
												<!--end::Text-->
											</div>
											<!--end::Stats-->
										</div>
										<!--end::Body-->
									</div>
									<!--end::Mixed Widget 13-->
								</div>
								<!--end::Col-->
								<!--begin::Col-->
								<div class="col-xl-4">
									<!--begin::Mixed Widget 14-->
									<div class="card card-xxl-stretch mb-xl-10" style="background-color: #CBF0F4">
										<!--begin::Body-->
										<div class="card-body d-flex flex-column">
											<!--begin::Wrapper-->
											<div class="d-flex flex-column flex-grow-1">
												<!--begin::Title-->
												<a href="#" class="text-dark text-hover-primary fw-bolder fs-3">Donations</a>
												<!--end::Title-->
												<!--begin::Chart-->
												<div class="mixed-widget-14-chart" style="height: 100px"></div>
												<!--end::Chart-->
											</div>
											<!--end::Wrapper-->
											<!--begin::Stats-->
											<div class="pt-5">
												<span class="text-dark fw-bolder fs-2x lh-0">$</span>
												<!--begin::Number-->
												<span class="text-dark fw-bolder fs-3x me-2 lh-0"><asp:Literal ID="lblthismonth" runat="server"  Text="0.00"></asp:Literal>  </span>
												<!--end::Number-->
												<!--begin::Text-->
												<span class="text-dark fw-bolder fs-6 lh-0"> this month</span>
												<!--end::Text-->
											</div>
											<!--end::Stats-->
										</div>
									</div>
									<!--end::Mixed Widget 14-->
								</div>
								<!--end::Col-->
								<!--begin::Col-->
								<%--<div class="col-xl-4">
									<!--begin::Mixed Widget 14-->
									<div class="card card-xxl-stretch mb-5 mb-xl-10" style="background-color: #CBD4F4">
										<!--begin::Body-->
										<div class="card-body d-flex flex-column">
											<!--begin::Wrapper-->
											<div class="d-flex flex-column mb-7">
												<!--begin::Title-->
												<a href="#" class="text-dark text-hover-primary fw-bolder fs-3">Summary</a>
												<!--end::Title-->
											</div>
											<!--end::Wrapper-->
											<!--begin::Row-->
											<div class="row g-0">
												<!--begin::Col-->
												<div class="col-6">
													<div class="d-flex align-items-center mb-9 me-2">
														<!--begin::Symbol-->
														<div class="symbol symbol-40px me-3">
															<div class="symbol-label bg-white bg-opacity-50">
																<!--begin::Svg Icon | path: icons/duotune/abstract/abs043.svg-->
																<span class="svg-icon svg-icon-1 svg-icon-dark">
																	<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																		<path opacity="0.3" d="M22 8H8L12 4H19C19.6 4 20.2 4.39999 20.5 4.89999L22 8ZM3.5 19.1C3.8 19.7 4.4 20 5 20H12L16 16H2L3.5 19.1ZM19.1 20.5C19.7 20.2 20 19.6 20 19V12L16 8V22L19.1 20.5ZM4.9 3.5C4.3 3.8 4 4.4 4 5V12L8 16V2L4.9 3.5Z" fill="black" />
																		<path d="M22 8L20 12L16 8H22ZM8 16L4 12L2 16H8ZM16 16L12 20L16 22V16ZM8 8L12 4L8 2V8Z" fill="black" />
																	</svg>
																</span>
																<!--end::Svg Icon-->
															</div>
														</div>
														<!--end::Symbol-->
														<!--begin::Title-->
														<div>
															<div class="fs-5 text-dark fw-bolder lh-1">$50K</div>
															<div class="fs-7 text-gray-600 fw-bold">Sales</div>
														</div>
														<!--end::Title-->
													</div>
												</div>
												<!--end::Col-->
												<!--begin::Col-->
												<div class="col-6">
													<div class="d-flex align-items-center mb-9 ms-2">
														<!--begin::Symbol-->
														<div class="symbol symbol-40px me-3">
															<div class="symbol-label bg-white bg-opacity-50">
																<!--begin::Svg Icon | path: icons/duotune/abstract/abs046.svg-->
																<span class="svg-icon svg-icon-1 svg-icon-dark">
																	<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																		<path d="M8 22C7.4 22 7 21.6 7 21V9C7 8.4 7.4 8 8 8C8.6 8 9 8.4 9 9V21C9 21.6 8.6 22 8 22Z" fill="black" />
																		<path opacity="0.3" d="M4 15C3.4 15 3 14.6 3 14V6C3 5.4 3.4 5 4 5C4.6 5 5 5.4 5 6V14C5 14.6 4.6 15 4 15ZM13 19V3C13 2.4 12.6 2 12 2C11.4 2 11 2.4 11 3V19C11 19.6 11.4 20 12 20C12.6 20 13 19.6 13 19ZM17 16V5C17 4.4 16.6 4 16 4C15.4 4 15 4.4 15 5V16C15 16.6 15.4 17 16 17C16.6 17 17 16.6 17 16ZM21 18V10C21 9.4 20.6 9 20 9C19.4 9 19 9.4 19 10V18C19 18.6 19.4 19 20 19C20.6 19 21 18.6 21 18Z" fill="black" />
																	</svg>
																</span>
																<!--end::Svg Icon-->
															</div>
														</div>
														<!--end::Symbol-->
														<!--begin::Title-->
														<div>
															<div class="fs-5 text-dark fw-bolder lh-1">$4,5K</div>
															<div class="fs-7 text-gray-600 fw-bold">Revenue</div>
														</div>
														<!--end::Title-->
													</div>
												</div>
												<!--end::Col-->
												<!--begin::Col-->
												<div class="col-6">
													<div class="d-flex align-items-center me-2">
														<!--begin::Symbol-->
														<div class="symbol symbol-40px me-3">
															<div class="symbol-label bg-white bg-opacity-50">
																<!--begin::Svg Icon | path: icons/duotune/abstract/abs022.svg-->
																<span class="svg-icon svg-icon-1 svg-icon-dark">
																	<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																		<path opacity="0.3" d="M11.425 7.325C12.925 5.825 15.225 5.825 16.725 7.325C18.225 8.825 18.225 11.125 16.725 12.625C15.225 14.125 12.925 14.125 11.425 12.625C9.92501 11.225 9.92501 8.825 11.425 7.325ZM8.42501 4.325C5.32501 7.425 5.32501 12.525 8.42501 15.625C11.525 18.725 16.625 18.725 19.725 15.625C22.825 12.525 22.825 7.425 19.725 4.325C16.525 1.225 11.525 1.225 8.42501 4.325Z" fill="black" />
																		<path d="M11.325 17.525C10.025 18.025 8.425 17.725 7.325 16.725C5.825 15.225 5.825 12.925 7.325 11.425C8.825 9.92498 11.125 9.92498 12.625 11.425C13.225 12.025 13.625 12.925 13.725 13.725C14.825 13.825 15.925 13.525 16.725 12.625C17.125 12.225 17.425 11.825 17.525 11.325C17.125 10.225 16.525 9.22498 15.625 8.42498C12.525 5.32498 7.425 5.32498 4.325 8.42498C1.225 11.525 1.225 16.625 4.325 19.725C7.425 22.825 12.525 22.825 15.625 19.725C16.325 19.025 16.925 18.225 17.225 17.325C15.425 18.125 13.225 18.225 11.325 17.525Z" fill="black" />
																	</svg>
																</span>
																<!--end::Svg Icon-->
															</div>
														</div>
														<!--end::Symbol-->
														<!--begin::Title-->
														<div>
															<div class="fs-5 text-dark fw-bolder lh-1">40</div>
															<div class="fs-7 text-gray-600 fw-bold">Tasks</div>
														</div>
														<!--end::Title-->
													</div>
												</div>
												<!--end::Col-->
												<!--begin::Col-->
												<div class="col-6">
													<div class="d-flex align-items-center ms-2">
														<!--begin::Symbol-->
														<div class="symbol symbol-40px me-3">
															<div class="symbol-label bg-white bg-opacity-50">
																<!--begin::Svg Icon | path: icons/duotune/abstract/abs045.svg-->
																<span class="svg-icon svg-icon-1 svg-icon-dark">
																	<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																		<path d="M2 11.7127L10 14.1127L22 11.7127L14 9.31274L2 11.7127Z" fill="black" />
																		<path opacity="0.3" d="M20.9 7.91274L2 11.7127V6.81275C2 6.11275 2.50001 5.61274 3.10001 5.51274L20.6 2.01274C21.3 1.91274 22 2.41273 22 3.11273V6.61273C22 7.21273 21.5 7.81274 20.9 7.91274ZM22 16.6127V11.7127L3.10001 15.5127C2.50001 15.6127 2 16.2127 2 16.8127V20.3127C2 21.0127 2.69999 21.6128 3.39999 21.4128L20.9 17.9128C21.5 17.8128 22 17.2127 22 16.6127Z" fill="black" />
																	</svg>
																</span>
																<!--end::Svg Icon-->
															</div>
														</div>
														<!--end::Symbol-->
														<!--begin::Title-->
														<div>
															<div class="fs-5 text-dark fw-bolder lh-1">$5.8M</div>
															<div class="fs-7 text-gray-600 fw-bold">Sales</div>
														</div>
														<!--end::Title-->
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Row-->
										</div>
									</div>
									<!--end::Mixed Widget 14-->
								</div>--%>
								<!--end::Col-->
							</div>
    <div class="row mb-6">
        <asp:GridView ID="GridDashboard" runat="server" Width="100%">
            <Columns>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="Tithing">
                    <ItemTemplate>
                        <asp:Label ID="lblTithigName" runat="server" Text='<%# Bind("TithigName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Paid By">
                    <ItemTemplate>
                        <asp:Label ID="lblPaidBy" runat="server" Text='<%# Bind("PaidBy") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="Paid Date">
                    <ItemTemplate>
                        <asp:Label ID="lblPaidDate" runat="server" Text='<%# Bind("PaidDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Pay Ref">
                    <ItemTemplate>
                        <asp:Label ID="lblPayRef" runat="server" Text='<%# Bind("PayRef") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        </div>
    <script>

        $(window).on('load', function () {
            //    debugger;
			if (getQuerystring('type') == "cash") {
				activeTab('Cash Donations');
			}
            else if (getQuerystring('type') == "inkind") {
                activeTab('In-Kind Donations');
            }

        });

        function getQuerystring(key, default_) {

            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(window.location.href.toLowerCase());
            if (qs == null)
                return default_;
            else
                return qs[1];
        }

		//In-Kind Donations
    </script>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
