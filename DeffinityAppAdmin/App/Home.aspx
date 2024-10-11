<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DeffinityAppDev.App.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
	Home
	</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
	.bi-envelope::before {
		font-size:39px;
	}
    path{
        fill:#7239EA !important;
    }
    .fill{
                fill:#7239EA !important;

    }
</style>
  <div class="row gy-5 g-xl-8 mb-5">
<div class="col-xl-3">
    <!--begin::Statistics Widget 5-->
    <a href="Organizations.aspx" class="card hoverable card-xl-stretch mb-xl-8" style="border: 2px solid #7239EA; background-color: transparent;">
        <!--begin::Body-->
        <div class="card-body text-center">
            <!--begin::Svg Icon-->
            <span style="color: #7239EA;" class="svg-icon svg-icon-white svg-icon-3x ms-n1">
                <!-- SVG Icon -->
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                        <rect x="0" y="0" width="24" height="24"/>
                        <path d="M13.5,21 L13.5,18 C13.5,17.4477153 13.0522847,17 12.5,17 L11.5,17 C10.9477153,17 10.5,17.4477153 10.5,18 L10.5,21 L5,21 L5,4 C5,2.8954305 5.8954305,2 7,2 L17,2 C18.1045695,2 19,2.8954305 19,4 L19,21 L13.5,21 Z M9,4 C8.44771525,4 8,4.44771525 8,5 L8,6 C8,6.55228475 8.44771525,7 9,7 L10,7 C10.5522847,7 11,6.55228475 11,6 L11,5 C11,4.44771525 10.5522847,4 10,4 L9,4 Z M14,4 C13.4477153,4 13,4.44771525 13,5 L13,6 C13,6.55228475 13.4477153,7 14,7 L15,7 C15.5522847,7 16,6.55228475 16,6 L16,5 C16,4.44771525 15.5522847,4 15,4 L14,4 Z M9,8 C8.44771525,8 8,8.44771525 8,9 L8,10 C8,10.5522847 8.44771525,11 9,11 L10,11 C10.5522847,11 11,10.5522847 11,10 L11,9 C11,8.44771525 10.5522847,8 10,8 L9,8 Z M9,12 C8.44771525,12 8,12.4477153 8,13 L8,14 C8,14.5522847 8.44771525,15 9,15 L10,15 C10.5522847,15 11,14.5522847 11,14 L11,13 C11,12.4477153 10.5522847,12 10,12 L9,12 Z M14,12 C13.4477153,12 13,12.4477153 13,13 L13,14 C13,14.5522847 13.4477153,15 14,15 L15,15 C15.5522847,15 16,14.5522847 16,14 L16,13 C16,12.4477153 15.5522847,12 15,12 L14,12 Z" fill="#7239EA"/> <!-- Changed fill to purple -->
                        <rect fill="#FFFFFF" x="13" y="8" width="3" height="3" rx="1"/>
                        <path d="M4,21 L20,21 C20.5522847,21 21,21.4477153 21,22 L21,22.4 C21,22.7313708 20.7313708,23 20.4,23 L3.6,23 C3.26862915,23 3,22.7313708 3,22.4 L3,22 C3,21.4477153 3.44771525,21 4,21 Z" fill="#000000" opacity="0.3"/>
                    </g>
                </svg>
                <!--end::Svg Icon-->
            </span>
            <div class="fw-bolder" style="color: #7239EA; font-size: 2rem; margin-top: 1rem;">Organizations</div>
            <div class="fw-bold fs-7" style="color: #7239EA !important;"></div>
        </div>
        <!--end::Body-->
    </a>
    <!--end::Statistics Widget 5-->
</div>


<div class="col-xl-3">
    <!--begin::Statistics Widget 5-->
    <a href="Members.aspx" class="card hoverable card-xl-stretch mb-xl-8" style="border: 2px solid #7239EA; background-color: transparent;">
        <!--begin::Body-->
        <div class="card-body text-center"> <!-- Centered the text -->
            <!--begin::Svg Icon | path: icons/duotune/ecommerce/ecm008.svg-->
            <span class="svg-icon svg-icon-3x ms-n1">
                <!-- SVG Icon -->
                <svg xmlns="http://www.w3.org/2000/svg" width="24px" height="24px" viewBox="0 0 24 24">
                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                        <polygon points="0 0 24 0 24 24 0 24" />
                        <path d="M18,8 L16,8 C15.4477153,8 15,7.55228475 15,7 C15,6.44771525 15.4477153,6 16,6 L18,6 L18,4 C18,3.44771525 18.4477153,3 19,3 C19.5522847,3 20,3.44771525 20,4 L20,6 L22,6 C22.5522847,6 23,6.44771525 23,7 C23,7.55228475 22.5522847,8 22,8 L20,8 L20,10 C20,10.5522847 19.5522847,11 19,11 C18.4477153,11 18,10.5522847 18,10 L18,8 z" fill="#7239EA" />
                        <path d="M9,11 C6.790861,11 5,9.209139 5,7 C5,4.790861 6.790861,3 9,3 C11.209139,3 13,4.790861 13,7 C13,9.209139 11.209139,11 9,11 z" fill="#7239EA" />
                        <path d="M0.00065168429,20.1992055 C0.388258525,15.4265159 4.26191235,13 8.98334134,13 C13.7712164,13 17.7048837,15.2931929 17.9979143,20.2 C18.0095879,20.3954741 17.9979143,21 17.2466999,21 C13.541124,21 8.03472472,21 0.727502227,21 C0.476712155,21 -0.0204617505,20.45918 0.00065168429,20.1992055 z" fill="#7239EA" />
                    </g>
                </svg>
                <!--end::Svg Icon-->
            </span>
            <div class="fw-bolder" style="color: #7239EA; font-size: 2rem; margin-top: 1rem;">Members</div>
            <div class="fw-bold fs-7" style="color: #7239EA !important;"></div>
        </div>
        <!--end::Body-->
    </a>
    <!--end::Statistics Widget 5-->
</div>




							<div class="col-xl-3">
    <!--begin::Statistics Widget 5-->
    <a href="OrganizationAdmin.aspx" class="card hoverable card-xl-stretch mb-5 mb-xl-8" style="border: 2px solid #7239EA; background-color: transparent;">
        <!--begin::Body-->
        <div class="card-body text-center"> <!-- Centered the text -->
            <!--begin::Svg Icon | path: icons/duotune/graphs/gra005.svg-->
            <span class="svg-icon svg-icon-white svg-icon-3x ms-n1" style="color: #7239EA;"> <!-- Set icon color to purple -->
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="#7239EA" class="bi bi-person-fill-gear" viewBox="0 0 16 16">
                    <path d="M11 5a3 3 0 1 1-6 0 3 3 0 0 1 6 0Zm-9 8c0 1 1 1 1 1h5.256A4.493 4.493 0 0 1 8 12.5a4.49 4.49 0 0 1 1.544-3.393C9.077 9.038 8.564 9 8 9c-5 0-6 3-6 4Zm9.886-3.54c.18-.613 1.048-.613 1.229 0l.043.148a.64.64 0 0 0 .921.382l.136-.074c.561-.306 1.175.308.87.869l-.075.136a.64.64 0 0 0 .382.92l.149.045c.612.18.612 1.048 0 1.229l-.15.043a.64.64 0 0 0-.38.921l.074.136c.305.561-.309 1.175-.87.87l-.136-.075a.64.64 0 0 0-.92.382l-.045.149c-.18.612-1.048.612-1.229 0l-.043-.15a.64.64 0 0 0-.921-.38l-.136.074c-.561.305-1.175-.309-.87-.87l.075-.136a.64.64 0 0 0-.382-.92l-.148-.045c-.613-.18-.613-1.048 0-1.229l.148-.043a.64.64 0 0 0 .382-.921l-.074-.136c-.306-.561.308-1.175.869-.87l.136.075a.64.64 0 0 0 .92-.382l.045-.148ZM14 12.5a1.5 1.5 0 1 0-3 0 1.5 1.5 0 0 0 3 0Z"/>
                </svg>
            </span>
            <!--end::Svg Icon-->
            <div class="fw-bolder" style="color: #7239EA; font-size: 2rem; margin-top: 1rem;">Admin Team</div> <!-- Set text color to purple and increased font size -->
            <div class="fw-bold fs-7" style="color: #7239EA;"></div>
        </div>
        <!--end::Body-->
    </a>
    <!--end::Statistics Widget 5-->
</div>

<div class="col-xl-3">
    <!--begin::Statistics Widget 5-->
    <a href="OrganizationPaymentSettings.aspx" class="card hoverable card-xl-stretch mb-xl-8" style="border: 2px solid #7239EA; background-color: transparent;">
        <!--begin::Body-->
        <div class="card-body text-center">
            <!--begin::Svg Icon | path: icons/duotune/ecommerce/ecm002.svg-->
            <span class="svg-icon svg-icon-white svg-icon-3x ms-n1" style="color: #7239EA;">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                        <rect x="0" y="0" width="24" height="24"/>
                        <path d="M2,6 L21,6 C21.5522847,6 22,6.44771525 22,7 L22,17 C22,17.5522847 21.5522847,18 21,18 L2,18 C1.44771525,18 1,17.5522847 1,17 L1,7 C1,6.44771525 1.44771525,6 2,6 Z M11.5,16 C13.709139,16 15.5,14.209139 15.5,12 C15.5,9.790861 13.709139,8 11.5,8 C9.290861,8 7.5,9.790861 7.5,12 C7.5,14.209139 9.290861,16 11.5,16 Z" fill="#7239EA" opacity="0.3" transform="translate(11.500000, 12.000000) rotate(-345.000000) translate(-11.500000, -12.000000) "/>
                        <path d="M2,6 L21,6 C21.5522847,6 22,6.44771525 22,7 L22,17 C22,17.5522847 21.5522847,18 21,18 L2,18 C1.44771525,18 1,17.5522847 1,17 L1,7 C1,6.44771525 1.44771525,6 2,6 Z M11.5,16 C13.709139,16 15.5,14.209139 15.5,12 C15.5,9.790861 13.709139,8 11.5,8 C9.290861,8 7.5,9.790861 7.5,12 C7.5,14.209139 9.290861,16 11.5,16 Z M11.5,14 C12.6045695,14 13.5,13.1045695 13.5,12 C13.5,10.8954305 12.6045695,10 11.5,10 C10.3954305,10 9.5,10.8954305 9.5,12 C9.5,13.1045695 10.3954305,14 11.5,14 Z" fill="#7239EA"/>
                    </g>
                </svg>
            </span>
            <!--end::Svg Icon-->
            <div class="fw-bolder" style="color: #7239EA; font-size: 2rem; margin-top: 1rem;">Payment Settings</div>
            <div class="fw-bold fs-7" style="color: #7239EA !important;"></div>
        </div>
        <!--end::Body-->
    </a>
    <!--end::Statistics Widget 5-->
</div>

<div class="col-xl-3">
    <!--begin::Statistics Widget 5-->
    <a href="SMSsettings.aspx" class="card hoverable card-xl-stretch mb-5 mb-xl-8" style="border: 2px solid #7239EA; background-color: transparent;">
        <!--begin::Body-->
        <div class="card-body text-center">
            <!--begin::Svg Icon | path: icons/duotune/graphs/gra005.svg-->
            <span class="svg-icon svg-icon-white svg-icon-3x ms-n1" style="color: #7239EA;">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="#7239EA" class="bi bi-chat-right-dots" viewBox="0 0 16 16">
                    <path d="M2 1a1 1 0 0 0-1 1v8a1 1 0 0 0 1 1h9.586a2 2 0 0 1 1.414.586l2 2V2a1 1 0 0 0-1-1H2zm12-1a2 2 0 0 1 2 2v12.793a.5.5 0 0 1-.854.353l-2.853-2.853a1 1 0 0 0-.707-.293H2a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12z"/>
                    <path d="M5 6a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm4 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm4 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0z"/>
                </svg>
            </span>
            <!--end::Svg Icon-->
            <div class="fw-bolder" style="color: #7239EA; font-size: 2rem; margin-top: 1rem;">SMS Settings</div>
            <div class="fw-bold fs-7" style="color: #7239EA !important;"></div>
        </div>
        <!--end::Body-->
    </a>
    <!--end::Statistics Widget 5-->
</div>

	<div class="col-xl-3">
    <!--begin::Statistics Widget 5-->
    <a href="PaymentTypeSettings.aspx" class="card hoverable card-xl-stretch mb-xl-8" style="border: 2px solid #7239EA; background-color: transparent;">
        <!--begin::Body-->
        <div class="card-body text-center">
            <!--begin::Svg Icon | path: icons/duotune/ecommerce/ecm002.svg-->
            <span class="svg-icon svg-icon-white svg-icon-3x ms-n1">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                        <rect x="0" y="0" width="24" height="24"/>
                        <path d="M2,6 L21,6 C21.5522847,6 22,6.44771525 22,7 L22,17 C22,17.5522847 21.5522847,18 21,18 L2,18 C1.44771525,18 1,17.5522847 1,17 L1,7 C1,6.44771525 1.44771525,6 2,6 Z M11.5,16 C13.709139,16 15.5,14.209139 15.5,12 C15.5,9.790861 13.709139,8 11.5,8 C9.290861,8 7.5,9.790861 7.5,12 C7.5,14.209139 9.290861,16 11.5,16 Z" fill="rgba(114, 57, 234, 0.3)" transform="translate(11.500000, 12.000000) rotate(-345.000000) translate(-11.500000, -12.000000) "/>
                        <path d="M2,6 L21,6 C21.5522847,6 22,6.44771525 22,7 L22,17 C22,17.5522847 21.5522847,18 21,18 L2,18 C1.44771525,18 1,17.5522847 1,17 L1,7 C1,6.44771525 1.44771525,6 2,6 Z M11.5,16 C13.709139,16 15.5,14.209139 15.5,12 C15.5,9.790861 13.709139,8 11.5,8 C9.290861,8 7.5,9.790861 7.5,12 C7.5,14.209139 9.290861,16 11.5,16 Z M11.5,14 C12.6045695,14 13.5,13.1045695 13.5,12 C13.5,10.8954305 12.6045695,10 11.5,10 C10.3954305,10 9.5,10.8954305 9.5,12 C9.5,13.1045695 10.3954305,14 11.5,14 Z" fill="#7239EA"/>
                    </g>
                </svg>
            </span>
            <!--end::Svg Icon-->
            <div class="fw-bolder" style="color: #7239EA; font-size: 2rem; margin-top: 1rem;">Payment Type</div>
            <div class="fw-bold fs-7" style="color: #7239EA !important;"></div>
        </div>
        <!--end::Body-->
    </a>
    <!--end::Statistics Widget 5-->
</div>

	  				
								
	<div class="col-xl-3">
    <!--begin::Statistics Widget 5-->
    <a href="TransactionReport.aspx" class="card card-xl-stretch mb-xl-8 hoverable" style="border: 2px solid #7239EA; background-color: transparent;">
        <!--begin::Body-->
        <div class="card-body text-center">
            <!--begin::Svg Icon | path: icons/duotune/ecommerce/ecm008.svg-->
            <span class="svg-icon svg-icon-3x" style="color: #7239EA;">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                        <rect x="0" y="0" width="24" height="24"/>
                        <path d="M15.9497475,3.80761184 L13.0246125,6.73274681 C12.2435639,7.51379539 12.2435639,8.78012535 13.0246125,9.56117394 L14.4388261,10.9753875 C15.2198746,11.7564361 16.4862046,11.7564361 17.2672532,10.9753875 L20.1923882,8.05025253 C20.7341101,10.0447871 20.2295941,12.2556873 18.674559,13.8107223 C16.8453326,15.6399488 14.1085592,16.0155296 11.8839934,14.9444337 L6.75735931,20.0710678 C5.97631073,20.8521164 4.70998077,20.8521164 3.92893219,20.0710678 C3.1478836,19.2900192 3.1478836,18.0236893 3.92893219,17.2426407 L9.05556629,12.1160066 C7.98447038,9.89144078 8.36005124,7.15466739 10.1892777,5.32544095 C11.7443127,3.77040588 13.9552129,3.26588995 15.9497475,3.80761184 Z" fill="#7239EA"/>
                        <path d="M16.6568542,5.92893219 L18.0710678,7.34314575 C18.4615921,7.73367004 18.4615921,8.36683502 18.0710678,8.75735931 L16.6913928,10.1370344 C16.3008685,10.5275587 15.6677035,10.5275587 15.2771792,10.1370344 L13.8629656,8.7228208 C13.4724413,8.33229651 13.4724413,7.69913153 13.8629656,7.30860724 L15.2426407,5.92893219 C15.633165,5.5384079 16.26633,5.5384079 16.6568542,5.92893219 Z" fill="#7239EA"/>
                    </g>
                </svg>
            </span>
            <!--end::Svg Icon-->
            <div class="fw-bolder fs-2 mb-2 mt-5" style="color: #7239EA;">Transaction Report</div>
            <div class="fw-bold fs-7" style="color: #7239EA;"></div>
        </div>
        <!--end::Body-->
    </a>
    <!--end::Statistics Widget 5-->
</div>

<div class="col-xl-3">
    <!--begin::Statistics Widget 5-->
    <a href="CharityChampions.aspx" class="card card-xl-stretch mb-xl-8 hoverable" style="border: 2px solid #7239EA; background-color: transparent;">
        <!--begin::Body-->
        <div class="card-body text-center">
            <!--begin::Svg Icon | path: icons/duotune/ecommerce/ecm002.svg-->
            <span class="svg-icon svg-icon-3x" style="color: #7239EA;">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                        <rect x="0" y="0" width="24" height="24"/>
                        <rect fill="#7239EA" opacity="0.3" x="2" y="2" width="10" height="12" rx="2"/>
                        <path d="M4,6 L20,6 C21.1045695,6 22,6.8954305 22,8 L22,20 C22,21.1045695 21.1045695,22 20,22 L4,22 C2.8954305,22 2,21.1045695 2,20 L2,8 C2,6.8954305 2.8954305,6 4,6 Z M18,16 C19.1045695,16 20,15.1045695 20,14 C20,12.8954305 19.1045695,12 18,12 C16.8954305,12 16,12.8954305 16,14 C16,15.1045695 16.8954305,16 18,16 Z" fill="#7239EA"/>
                    </g>
                </svg>
            </span>
            <!--end::Svg Icon-->
            <div class="fw-bolder fs-2 mb-2 mt-5" style="color: #7239EA;">Charity Champions</div>
            <div class="fw-bold fs-7" style="color: #7239EA;"></div>
        </div>
        <!--end::Body-->
    </a>
    <!--end::Statistics Widget 5-->
</div>

	<div class="row gy-5 g-xl-8">
								
				
		</div>
	<div class="row gy-5 g-xl-8">
								
<div class="col-xl-3">
    <!--begin::Statistics Widget 5-->
    <a href="ProfitReport.aspx" class="card card-xl-stretch mb-xl-8 hoverable" style="border: 2px solid #7239EA; background-color: transparent;">
        <!--begin::Body-->
        <div class="card-body text-center">
            <!--begin::Svg Icon | path: icons/duotune/ecommerce/ecm008.svg-->
            <span class="svg-icon svg-icon-3x" style="color: #7239EA;">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                        <rect x="0" y="0" width="24" height="24"/>
                        <rect fill="#7239EA" class="fill" opacity="0.3" x="2" y="5" width="20" height="14" rx="2"/>
                        <rect fill="#7239EA" class="fill" x="2" y="8" width="20" height="3"/>
                        <rect fill="#7239EA" class="fill" opacity="0.3" x="16" y="14" width="4" height="2" rx="1"/>
                    </g>
                </svg>
            </span>
            <!--end::Svg Icon-->
            <div class="fw-bolder fs-2 mb-2 mt-5" style="color: #7239EA;">Profit Report</div>
            <div class="fw-bold fs-7" style="color: #7239EA;"></div>
        </div>
        <!--end::Body-->
    </a>
    <!--end::Statistics Widget 5-->
</div>

<div class="col-xl-3">
    <!--begin::Statistics Widget 5-->
    <a href="VideosConfig.aspx" class="card card-xl-stretch mb-xl-8 hoverable" style="border: 2px solid #7239EA; background-color: transparent;">
        <!--begin::Body-->
        <div class="card-body text-center">
            <!--begin::Svg Icon | path: icons/duotune/graphs/gra005.svg-->
            <span class="svg-icon svg-icon-3x" style="color: #7239EA;">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                        <rect x="0" y="0" width="24" height="24"/>
                        <path d="M2.56066017,10.6819805 L4.68198052,8.56066017 C5.26776695,7.97487373 6.21751442,7.97487373 6.80330086,8.56066017 L8.9246212,10.6819805 C9.51040764,11.267767 9.51040764,12.2175144 8.9246212,12.8033009 L6.80330086,14.9246212 C6.21751442,15.5104076 5.26776695,15.5104076 4.68198052,14.9246212 L2.56066017,12.8033009 C1.97487373,12.2175144 1.97487373,11.267767 2.56066017,10.6819805 Z M14.5606602,10.6819805 L16.6819805,8.56066017 C17.267767,7.97487373 18.2175144,7.97487373 18.8033009,8.56066017 L20.9246212,10.6819805 C21.5104076,11.267767 21.5104076,12.2175144 20.9246212,12.8033009 L18.8033009,14.9246212 C18.2175144,15.5104076 17.267767,15.5104076 16.6819805,14.9246212 L14.5606602,12.8033009 C13.9748737,12.2175144 13.9748737,11.267767 14.5606602,10.6819805 Z" fill="#7239EA" opacity="0.3"/>
                        <path d="M8.56066017,16.6819805 L10.6819805,14.5606602 C11.267767,13.9748737 12.2175144,13.9748737 12.8033009,14.5606602 L14.9246212,16.6819805 C15.5104076,17.267767 15.5104076,18.2175144 14.9246212,18.8033009 L12.8033009,20.9246212 C12.2175144,21.5104076 11.267767,21.5104076 10.6819805,20.9246212 L8.56066017,18.8033009 C7.97487373,18.2175144 7.97487373,17.267767 8.56066017,16.6819805 Z M8.56066017,4.68198052 L10.6819805,2.56066017 C11.267767,1.97487373 12.2175144,1.97487373 12.8033009,2.56066017 L14.9246212,4.68198052 C15.5104076,5.26776695 15.5104076,6.21751442 14.9246212,6.80330086 L12.8033009,8.9246212 C12.2175144,9.51040764 11.267767,9.51040764 10.6819805,8.9246212 L8.56066017,6.80330086 C7.97487373,6.21751442 7.97487373,5.26776695 8.56066017,4.68198052 Z" fill="#7239EA"/>
                    </g>
                </svg>
            </span>
            <!--end::Svg Icon-->
            <div class="fw-bolder fs-2 mb-2 mt-5" style="color: #7239EA;">Videos</div>
            <div class="fw-bold fs-7" style="color: #7239EA;"></div>
        </div>
        <!--end::Body-->
    </a>
    <!--end::Statistics Widget 5-->
</div>

		<div class="col-xl-3">
    <!--begin::Statistics Widget 5-->
    <a href="Wordpress.aspx" class="card card-xl-stretch mb-5 mb-xl-8 hoverable" style="border: 2px solid #7239EA; background-color: transparent;">
        <!--begin::Body-->
        <div class="card-body text-center">
            <!--begin::Svg Icon-->
            <span class="svg-icon svg-icon-3x">
                <i class="bi bi-envelope" style="color: #7239EA; height:39px;width:39px;"></i>
            </span>
            <!--end::Svg Icon-->
            <div class="fw-bolder fs-2 mb-2 mt-5" style="color: #7239EA;">WordPress</div>
            <div class="fw-bold fs-7" style="color: #7239EA;"></div>
        </div>
        <!--end::Body-->
    </a>
    <!--end::Statistics Widget 5-->
</div>





















		<div class="col-xl-3">
    <!--begin::Statistics Widget 5-->
    <a href="Marketplace.aspx" class="card card-xl-stretch mb-5 mb-xl-8 hoverable" style="border: 2px solid #7239EA; background-color: transparent;">
        <!--begin::Body-->
        <div class="card-body text-center">
            <!--begin::Svg Icon-->
            <span class="svg-icon svg-icon-3x">
                <i class="bi bi-envelope" style="color: #7239EA; height:39px;width:39px;"></i>
            </span>
            <!--end::Svg Icon-->
            <div class="fw-bolder fs-2 mb-2 mt-5" style="color: #7239EA;">Marketplace</div>
            <div class="fw-bold fs-7" style="color: #7239EA;"></div>
        </div>
        <!--end::Body-->
    </a>
    <!--end::Statistics Widget 5-->
</div>


        		<div class="col-xl-3">
    <!--begin::Statistics Widget 5-->
    <a href="Iconconfiguration.aspx" class="card card-xl-stretch mb-5 mb-xl-8 hoverable" style="border: 2px solid #7239EA; background-color: transparent;">
        <!--begin::Body-->
        <div class="card-body text-center">
            <!--begin::Svg Icon-->
            <span class="svg-icon svg-icon-3x">
                <i class="bi bi-envelope" style="color: #7239EA; height:39px;width:39px;"></i>
            </span>
            <!--end::Svg Icon-->
            <div class="fw-bolder fs-2 mb-2 mt-5" style="color: #7239EA;">Icon Configuration</div>
            <div class="fw-bold fs-7" style="color: #7239EA;"></div>
        </div>
        <!--end::Body-->
    </a>
    <!--end::Statistics Widget 5-->
</div>
























		<div class="col-xl-3" style="display:none;visibility:hidden;">
									<!--begin::Statistics Widget 5-->
									<a href="InternationalSettings.aspx" class="card bg-info hoverable card-xl-stretch mb-xl-8">
										<!--begin::Body-->
										<div class="card-body">
											<!--begin::Svg Icon | path: icons/duotune/ecommerce/ecm002.svg-->
											<span class="svg-icon svg-icon-white svg-icon-3x ms-n1">
												<!--begin::Svg Icon | path:/var/www/preview.keenthemes.com/metronic/releases/2021-05-14-112058/theme/html/demo1/dist/../src/media/svg/icons/Home/Earth.svg--><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
        <rect x="0" y="0" width="24" height="24"/>
        <circle fill="#000000" opacity="0.3" cx="12" cy="12" r="9"/>
        <path d="M11.7357634,20.9961946 C6.88740052,20.8563914 3,16.8821712 3,12 C3,11.9168367 3.00112797,11.8339369 3.00336944,11.751315 C3.66233009,11.8143341 4.85636818,11.9573854 4.91262842,12.4204038 C4.9904938,13.0609191 4.91262842,13.8615942 5.45804656,14.101772 C6.00346469,14.3419498 6.15931561,13.1409372 6.6267482,13.4612567 C7.09418079,13.7815761 8.34086797,14.0899175 8.34086797,14.6562185 C8.34086797,15.222396 8.10715168,16.1034596 8.34086797,16.2636193 C8.57458427,16.423779 9.5089688,17.54465 9.50920913,17.7048097 C9.50956962,17.8649694 9.83857487,18.6793513 9.74040201,18.9906563 C9.65905192,19.2487394 9.24857641,20.0501554 8.85059781,20.4145589 C9.75315358,20.7620621 10.7235846,20.9657742 11.7357634,20.9960544 L11.7357634,20.9961946 Z M8.28272988,3.80112099 C9.4158415,3.28656421 10.6744554,3 12,3 C15.5114513,3 18.5532143,5.01097452 20.0364482,7.94408274 C20.069657,8.72412177 20.0638332,9.39135321 20.2361262,9.6327358 C21.1131932,10.8600506 18.0995147,11.7043158 18.5573343,13.5605384 C18.7589671,14.3794892 16.5527814,14.1196773 16.0139722,14.886394 C15.4748026,15.6527403 14.1574598,15.137809 13.8520064,14.9904917 C13.546553,14.8431744 12.3766497,15.3341497 12.4789081,14.4995164 C12.5805657,13.664636 13.2922889,13.6156126 14.0555619,13.2719546 C14.8184743,12.928667 15.9189236,11.7871741 15.3781918,11.6380045 C12.8323064,10.9362407 11.963771,8.47852395 11.963771,8.47852395 C11.8110443,8.44901109 11.8493762,6.74109366 11.1883616,6.69207022 C10.5267462,6.64279981 10.170464,6.88841096 9.20435656,6.69207022 C8.23764828,6.49572949 8.44144409,5.85743687 8.2887174,4.48255778 C8.25453994,4.17415686 8.25619136,3.95717082 8.28272988,3.80112099 Z M20.9991771,11.8770357 C20.9997251,11.9179585 21,11.9589471 21,12 C21,16.9406923 17.0188468,20.9515364 12.0895088,20.9995641 C16.970233,20.9503326 20.9337111,16.888438 20.9991771,11.8770357 Z" fill="#000000" opacity="0.3"/>
    </g>
</svg><!--end::Svg Icon-->
											</span>
											<!--end::Svg Icon-->
											<div class="text-inverse-danger fw-bolder fs-2 mb-2 mt-5">International Settings</div>
											<div class="fw-bold text-inverse-danger fs-7"></div>
										</div>
										<!--end::Body-->
									</a>
									<!--end::Statistics Widget 5-->
								</div>
		<div class="col-xl-3" style="display:none;visibility:hidden;">
									<!--begin::Statistics Widget 5-->
									<a href="../WF/Admin/Premium.aspx" class="card bg-success hoverable card-xl-stretch mb-xl-8">
										<!--begin::Body-->
										<div class="card-body">
											<!--begin::Svg Icon | path: icons/duotune/ecommerce/ecm002.svg-->
											<span class="svg-icon svg-icon-white svg-icon-3x ms-n1">
												<!--begin::Svg Icon | path:/var/www/preview.keenthemes.com/metronic/releases/2021-05-14-112058/theme/html/demo1/dist/../src/media/svg/icons/Home/Earth.svg--><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
        <rect x="0" y="0" width="24" height="24"/>
        <circle fill="#000000" opacity="0.3" cx="12" cy="12" r="9"/>
        <path d="M11.7357634,20.9961946 C6.88740052,20.8563914 3,16.8821712 3,12 C3,11.9168367 3.00112797,11.8339369 3.00336944,11.751315 C3.66233009,11.8143341 4.85636818,11.9573854 4.91262842,12.4204038 C4.9904938,13.0609191 4.91262842,13.8615942 5.45804656,14.101772 C6.00346469,14.3419498 6.15931561,13.1409372 6.6267482,13.4612567 C7.09418079,13.7815761 8.34086797,14.0899175 8.34086797,14.6562185 C8.34086797,15.222396 8.10715168,16.1034596 8.34086797,16.2636193 C8.57458427,16.423779 9.5089688,17.54465 9.50920913,17.7048097 C9.50956962,17.8649694 9.83857487,18.6793513 9.74040201,18.9906563 C9.65905192,19.2487394 9.24857641,20.0501554 8.85059781,20.4145589 C9.75315358,20.7620621 10.7235846,20.9657742 11.7357634,20.9960544 L11.7357634,20.9961946 Z M8.28272988,3.80112099 C9.4158415,3.28656421 10.6744554,3 12,3 C15.5114513,3 18.5532143,5.01097452 20.0364482,7.94408274 C20.069657,8.72412177 20.0638332,9.39135321 20.2361262,9.6327358 C21.1131932,10.8600506 18.0995147,11.7043158 18.5573343,13.5605384 C18.7589671,14.3794892 16.5527814,14.1196773 16.0139722,14.886394 C15.4748026,15.6527403 14.1574598,15.137809 13.8520064,14.9904917 C13.546553,14.8431744 12.3766497,15.3341497 12.4789081,14.4995164 C12.5805657,13.664636 13.2922889,13.6156126 14.0555619,13.2719546 C14.8184743,12.928667 15.9189236,11.7871741 15.3781918,11.6380045 C12.8323064,10.9362407 11.963771,8.47852395 11.963771,8.47852395 C11.8110443,8.44901109 11.8493762,6.74109366 11.1883616,6.69207022 C10.5267462,6.64279981 10.170464,6.88841096 9.20435656,6.69207022 C8.23764828,6.49572949 8.44144409,5.85743687 8.2887174,4.48255778 C8.25453994,4.17415686 8.25619136,3.95717082 8.28272988,3.80112099 Z M20.9991771,11.8770357 C20.9997251,11.9179585 21,11.9589471 21,12 C21,16.9406923 17.0188468,20.9515364 12.0895088,20.9995641 C16.970233,20.9503326 20.9337111,16.888438 20.9991771,11.8770357 Z" fill="#000000" opacity="0.3"/>
    </g>
</svg><!--end::Svg Icon-->
											</span>
											<!--end::Svg Icon-->
											<div class="text-inverse-danger fw-bolder fs-2 mb-2 mt-5">Branding</div>
											<div class="fw-bold text-inverse-danger fs-7"></div>
										</div>
										<!--end::Body-->
									</a>
									<!--end::Statistics Widget 5-->
								</div>
		<div class="col-xl-3" style="display:none;visibility:hidden;" >
									<!--begin::Statistics Widget 5-->
									<a href="../WF/Admin/BlogList.aspx" class="card bg-dark hoverable card-xl-stretch mb-xl-8">
										<!--begin::Body-->
										<div class="card-body">
											<!--begin::Svg Icon | path: icons/duotune/ecommerce/ecm002.svg-->
											<span class="svg-icon svg-icon-white svg-icon-3x ms-n1">
												<!--begin::Svg Icon | path:/var/www/preview.keenthemes.com/metronic/releases/2021-05-14-112058/theme/html/demo1/dist/../src/media/svg/icons/Home/Earth.svg--><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
        <rect x="0" y="0" width="24" height="24"/>
        <circle fill="#000000" opacity="0.3" cx="12" cy="12" r="9"/>
        <path d="M11.7357634,20.9961946 C6.88740052,20.8563914 3,16.8821712 3,12 C3,11.9168367 3.00112797,11.8339369 3.00336944,11.751315 C3.66233009,11.8143341 4.85636818,11.9573854 4.91262842,12.4204038 C4.9904938,13.0609191 4.91262842,13.8615942 5.45804656,14.101772 C6.00346469,14.3419498 6.15931561,13.1409372 6.6267482,13.4612567 C7.09418079,13.7815761 8.34086797,14.0899175 8.34086797,14.6562185 C8.34086797,15.222396 8.10715168,16.1034596 8.34086797,16.2636193 C8.57458427,16.423779 9.5089688,17.54465 9.50920913,17.7048097 C9.50956962,17.8649694 9.83857487,18.6793513 9.74040201,18.9906563 C9.65905192,19.2487394 9.24857641,20.0501554 8.85059781,20.4145589 C9.75315358,20.7620621 10.7235846,20.9657742 11.7357634,20.9960544 L11.7357634,20.9961946 Z M8.28272988,3.80112099 C9.4158415,3.28656421 10.6744554,3 12,3 C15.5114513,3 18.5532143,5.01097452 20.0364482,7.94408274 C20.069657,8.72412177 20.0638332,9.39135321 20.2361262,9.6327358 C21.1131932,10.8600506 18.0995147,11.7043158 18.5573343,13.5605384 C18.7589671,14.3794892 16.5527814,14.1196773 16.0139722,14.886394 C15.4748026,15.6527403 14.1574598,15.137809 13.8520064,14.9904917 C13.546553,14.8431744 12.3766497,15.3341497 12.4789081,14.4995164 C12.5805657,13.664636 13.2922889,13.6156126 14.0555619,13.2719546 C14.8184743,12.928667 15.9189236,11.7871741 15.3781918,11.6380045 C12.8323064,10.9362407 11.963771,8.47852395 11.963771,8.47852395 C11.8110443,8.44901109 11.8493762,6.74109366 11.1883616,6.69207022 C10.5267462,6.64279981 10.170464,6.88841096 9.20435656,6.69207022 C8.23764828,6.49572949 8.44144409,5.85743687 8.2887174,4.48255778 C8.25453994,4.17415686 8.25619136,3.95717082 8.28272988,3.80112099 Z M20.9991771,11.8770357 C20.9997251,11.9179585 21,11.9589471 21,12 C21,16.9406923 17.0188468,20.9515364 12.0895088,20.9995641 C16.970233,20.9503326 20.9337111,16.888438 20.9991771,11.8770357 Z" fill="#000000" opacity="0.3"/>
    </g>
</svg><!--end::Svg Icon-->
											</span>
											<!--end::Svg Icon-->
											<div class="text-inverse-danger fw-bolder fs-2 mb-2 mt-5">Business Services</div>
											<div class="fw-bold text-inverse-danger fs-7"></div>
										</div>
										<!--end::Body-->
									</a>
									<!--end::Statistics Widget 5-->
								</div>
							</div>

	<div class="row gy-5 g-xl-8">
									
							<%--	<div class="col-xl-3" style="display:none;visibility:hidden;">
									<!--begin::Statistics Widget 5-->
									<a href="AppAdvertisingBanner.aspx" class="card bg-dark hoverable card-xl-stretch mb-xl-8">
										<!--begin::Body-->
										<div class="card-body">
											<!--begin::Svg Icon | path: icons/duotune/ecommerce/ecm002.svg-->
											<span class="svg-icon svg-icon-white svg-icon-3x ms-n1">
												<!--begin::Svg Icon | path:/var/www/preview.keenthemes.com/metronic/releases/2021-05-14-112058/theme/html/demo1/dist/../src/media/svg/icons/Home/Earth.svg--><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
        <rect x="0" y="0" width="24" height="24"/>
        <circle fill="#000000" opacity="0.3" cx="12" cy="12" r="9"/>
        <path d="M11.7357634,20.9961946 C6.88740052,20.8563914 3,16.8821712 3,12 C3,11.9168367 3.00112797,11.8339369 3.00336944,11.751315 C3.66233009,11.8143341 4.85636818,11.9573854 4.91262842,12.4204038 C4.9904938,13.0609191 4.91262842,13.8615942 5.45804656,14.101772 C6.00346469,14.3419498 6.15931561,13.1409372 6.6267482,13.4612567 C7.09418079,13.7815761 8.34086797,14.0899175 8.34086797,14.6562185 C8.34086797,15.222396 8.10715168,16.1034596 8.34086797,16.2636193 C8.57458427,16.423779 9.5089688,17.54465 9.50920913,17.7048097 C9.50956962,17.8649694 9.83857487,18.6793513 9.74040201,18.9906563 C9.65905192,19.2487394 9.24857641,20.0501554 8.85059781,20.4145589 C9.75315358,20.7620621 10.7235846,20.9657742 11.7357634,20.9960544 L11.7357634,20.9961946 Z M8.28272988,3.80112099 C9.4158415,3.28656421 10.6744554,3 12,3 C15.5114513,3 18.5532143,5.01097452 20.0364482,7.94408274 C20.069657,8.72412177 20.0638332,9.39135321 20.2361262,9.6327358 C21.1131932,10.8600506 18.0995147,11.7043158 18.5573343,13.5605384 C18.7589671,14.3794892 16.5527814,14.1196773 16.0139722,14.886394 C15.4748026,15.6527403 14.1574598,15.137809 13.8520064,14.9904917 C13.546553,14.8431744 12.3766497,15.3341497 12.4789081,14.4995164 C12.5805657,13.664636 13.2922889,13.6156126 14.0555619,13.2719546 C14.8184743,12.928667 15.9189236,11.7871741 15.3781918,11.6380045 C12.8323064,10.9362407 11.963771,8.47852395 11.963771,8.47852395 C11.8110443,8.44901109 11.8493762,6.74109366 11.1883616,6.69207022 C10.5267462,6.64279981 10.170464,6.88841096 9.20435656,6.69207022 C8.23764828,6.49572949 8.44144409,5.85743687 8.2887174,4.48255778 C8.25453994,4.17415686 8.25619136,3.95717082 8.28272988,3.80112099 Z M20.9991771,11.8770357 C20.9997251,11.9179585 21,11.9589471 21,12 C21,16.9406923 17.0188468,20.9515364 12.0895088,20.9995641 C16.970233,20.9503326 20.9337111,16.888438 20.9991771,11.8770357 Z" fill="#000000" opacity="0.3"/>
    </g>
</svg><!--end::Svg Icon-->
											</span>
											<!--end::Svg Icon-->
											<div class="text-inverse-danger fw-bolder fs-2 mb-2 mt-5">App Advertising Banner </div>
											<div class="fw-bold text-inverse-danger fs-7"></div>
										</div>
										<!--end::Body-->
									</a>
									<!--end::Statistics Widget 5-->
								</div>--%>
		<div class="col-xl-3" style="display:none;visibility:hidden;">
									<!--begin::Statistics Widget 5-->
									<a href="AppHomeScreen.aspx" class="card bg-warning hoverable card-xl-stretch mb-xl-8">
										<!--begin::Body-->
										<div class="card-body">
											<!--begin::Svg Icon | path: icons/duotune/ecommerce/ecm002.svg-->
											<span class="svg-icon svg-icon-white svg-icon-3x ms-n1">
												<!--begin::Svg Icon | path:/var/www/preview.keenthemes.com/metronic/releases/2021-05-14-112058/theme/html/demo1/dist/../src/media/svg/icons/Home/Earth.svg--><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
        <rect x="0" y="0" width="24" height="24"/>
        <circle fill="#000000" opacity="0.3" cx="12" cy="12" r="9"/>
        <path d="M11.7357634,20.9961946 C6.88740052,20.8563914 3,16.8821712 3,12 C3,11.9168367 3.00112797,11.8339369 3.00336944,11.751315 C3.66233009,11.8143341 4.85636818,11.9573854 4.91262842,12.4204038 C4.9904938,13.0609191 4.91262842,13.8615942 5.45804656,14.101772 C6.00346469,14.3419498 6.15931561,13.1409372 6.6267482,13.4612567 C7.09418079,13.7815761 8.34086797,14.0899175 8.34086797,14.6562185 C8.34086797,15.222396 8.10715168,16.1034596 8.34086797,16.2636193 C8.57458427,16.423779 9.5089688,17.54465 9.50920913,17.7048097 C9.50956962,17.8649694 9.83857487,18.6793513 9.74040201,18.9906563 C9.65905192,19.2487394 9.24857641,20.0501554 8.85059781,20.4145589 C9.75315358,20.7620621 10.7235846,20.9657742 11.7357634,20.9960544 L11.7357634,20.9961946 Z M8.28272988,3.80112099 C9.4158415,3.28656421 10.6744554,3 12,3 C15.5114513,3 18.5532143,5.01097452 20.0364482,7.94408274 C20.069657,8.72412177 20.0638332,9.39135321 20.2361262,9.6327358 C21.1131932,10.8600506 18.0995147,11.7043158 18.5573343,13.5605384 C18.7589671,14.3794892 16.5527814,14.1196773 16.0139722,14.886394 C15.4748026,15.6527403 14.1574598,15.137809 13.8520064,14.9904917 C13.546553,14.8431744 12.3766497,15.3341497 12.4789081,14.4995164 C12.5805657,13.664636 13.2922889,13.6156126 14.0555619,13.2719546 C14.8184743,12.928667 15.9189236,11.7871741 15.3781918,11.6380045 C12.8323064,10.9362407 11.963771,8.47852395 11.963771,8.47852395 C11.8110443,8.44901109 11.8493762,6.74109366 11.1883616,6.69207022 C10.5267462,6.64279981 10.170464,6.88841096 9.20435656,6.69207022 C8.23764828,6.49572949 8.44144409,5.85743687 8.2887174,4.48255778 C8.25453994,4.17415686 8.25619136,3.95717082 8.28272988,3.80112099 Z M20.9991771,11.8770357 C20.9997251,11.9179585 21,11.9589471 21,12 C21,16.9406923 17.0188468,20.9515364 12.0895088,20.9995641 C16.970233,20.9503326 20.9337111,16.888438 20.9991771,11.8770357 Z" fill="#000000" opacity="0.3"/>
    </g>
</svg><!--end::Svg Icon-->
											</span>
											<!--end::Svg Icon-->
											<div class="text-inverse-danger fw-bolder fs-2 mb-2 mt-5">App Home Screen</div>
											<div class="fw-bold text-inverse-danger fs-7"></div>
										</div>
										<!--end::Body-->
									</a>
									<!--end::Statistics Widget 5-->
								</div>
							</div>

</asp:Content>
