﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="DeffinityAppDev.App.Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
	<%: Resources.DeffinityRes.Settings %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
	<style>
	 .pnlheight{
		 min-height:400px;
	 }
     h2.page-header{
         font-size:21px;
     }
	</style>
    <div class="row g-6 g-xl-9">
       <div class="col-md-6 col-xl-3 card border-0 m-5 m-5">
										<!--begin::Card-->
										<a href="#" class="card border-hover-primary">
											<!--begin::Card header-->
											<div class="card-header border-0 pt-9 d-flex">
												<!--begin::Card Title-->
												<div class="card-title m-auto">
													<!--begin::Avatar-->
													<div class="symbol symbol-50px w-100px bg-light d-flex">
														<i class="fas fa-desktop fa-4x p-3 m-auto"></i>
													</div>
													<!--end::Avatar-->
												</div>
												<!--end::Car Title-->
												<!--begin::Card toolbar-->
											<%--	<div class="card-toolbar">
													<span class="badge badge-light-primary fw-bolder me-auto px-4 py-3">In Progress</span>
												</div>--%>
												<!--end::Card toolbar-->
											</div>
											<!--end:: Card header-->
											<!--begin:: Card body-->
											<div class="card-body p-9" style="height: 180px;">
												<!--begin::Name-->
												<div class="fs-1 fw-bolder text-dark">Portal Branding</div>
												<!--end::Name-->
												<!--begin::Description-->
												<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7" style="height:110px;overflow-y:auto;"> Allows you to add company information such as bank details that appear on an invoice as well as change the logo on the portal.</p>
												
											</div>
											<!--end:: Card body-->
                                            <div class="card-footer">
                                                  <asp:Button ID="BtnPortalBranding" runat="server" CommandName="PortalBranding" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%"  />
                                            </div>
										</a>
										<!--end::Card-->
									</div>
		<div class="col-md-6 col-xl-3 card border-0 m-5" id="Div4" runat="server">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9 d-flex">
		<div class="card-title m-auto">
			<div class="symbol symbol-50px w-100px bg-light d-flex">
			<i class="fas fa-users fa-4x p-3 m-auto"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">  Team Members</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">       Add your team members in this section</p>
			</div>
                                            <div class="card-footer">
                                       <asp:Button ID="Button6" runat="server" CommandName="Members" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>


				<div class="col-md-6 col-xl-3 card border-0 m-5" id="Div5" runat="server">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9 d-flex">
		<div class="card-title m-auto">
			<div class="symbol symbol-50px w-100px bg-light d-flex">
			<i class="fas fa-users fa-4x p-3 m-auto"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">  Icon Configuration</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">       Configure icons for top donations</p>
			</div>
                                            <div class="card-footer">
                                       <asp:Button ID="Button16" runat="server" CommandName="icons" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>



		 <div class="col-md-6 col-xl-3 card border-0 m-5" id="Div3" runat="server">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9 d-flex">
		<div class="card-title m-auto">
			<div class="symbol symbol-50px w-100px bg-light d-flex">
				<svg xmlns="http://www.w3.org/2000/svg"  fill="#99a1b7" style="height:1em" class="bi bi-person-wheelchair fa-4x m-auto" viewBox="0 0 16 16">
  <path d="M12 3a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3m-.663 2.146a1.5 1.5 0 0 0-.47-2.115l-2.5-1.508a1.5 1.5 0 0 0-1.676.086l-2.329 1.75a.866.866 0 0 0 1.051 1.375L7.361 3.37l.922.71-2.038 2.445A4.73 4.73 0 0 0 2.628 7.67l1.064 1.065a3.25 3.25 0 0 1 4.574 4.574l1.064 1.063a4.73 4.73 0 0 0 1.09-3.998l1.043-.292-.187 2.991a.872.872 0 1 0 1.741.098l.206-4.121A1 1 0 0 0 12.224 8h-2.79zM3.023 9.48a3.25 3.25 0 0 0 4.496 4.496l1.077 1.077a4.75 4.75 0 0 1-6.65-6.65z"/>
</svg>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">  Donation Categories </div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;"> Donation categories help to split donations into the various causes you support on a regular basis and appear on your landing page.</p>
			</div>
                                            <div class="card-footer">
                                       <asp:Button ID="Button2" runat="server" CommandName="tithing" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
	
</a>
			 
</div>



				 <div class="col-md-6 col-xl-3 card border-0 m-5" id="Div6" runat="server">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9 d-flex">
		<div class="card-title m-auto">
			<div class="symbol symbol-50px w-100px bg-light d-flex">
				<i class="bi bi-boxes fa-4x m-auto"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">  Event Scrollers </div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">Use this section to configure event scrollers for Wordpress. </p>
			</div>
                                            <div class="card-footer">
                                       <asp:Button ID="Button18" runat="server" CommandName="eventscroller" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
	
</a>
			 
</div>






		 <div class="col-md-6 col-xl-3 card border-0 m-5" id="DivTimesheetApprover" runat="server" visible="false">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="far fa-file-alt fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">  Timesheet Approver</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;"> Timesheet Approver</p>
			</div>
                                            <div class="card-footer">
                                       <asp:Button ID="Button4" runat="server" CommandName="Timesheet" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>

		 <div class="col-md-6 col-xl-3 card border-0 m-5" id="Div1" runat="server" visible="false">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="far fa-file-alt fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">  Expenses</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">        Create accounting codes for your expenses</p>
			</div>
                                            <div class="card-footer">
                                       <asp:Button ID="Button5" runat="server" CommandName="Expenses" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>

		 <div class="col-md-6 col-xl-3 card border-0 m-5" id="Div2" runat="server">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9 d-flex">
		<div class="card-title m-auto">
			<div class="symbol symbol-50px w-100px bg-light d-flex">
			<i class="bi bi-boxes fa-4x m-auto"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">  Page Builder</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">        Use this section to set up your web page. Donors to make donations, access fundraisers, and book tickets to events using your web page. This page can be linked to your website using the short URL.</p>
			</div>
                                            <div class="card-footer">
                                       <asp:Button ID="Button1" runat="server" CommandName="PageBuilder" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>

           <div class="col-md-6 col-xl-3 card border-0 m-5" style="display:none;visibility:hidden;">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="fas fa-calculator fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">VAT</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;"> Enter the VAT rate. This defaults to 20%</p>
			</div>
                                            <div class="card-footer">
                                                 <asp:Button ID="btnTax" runat="server" CommandName="Tax" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>

    

         <div class="col-md-6 col-xl-3 card border-0 m-5"  style="display:none;visibility:hidden;">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="far fa-money-bill-alt fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">Default % Mark Up</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">  This sets the default % mark up of a job when you create an estimate. By default we set
this to 25%.</p>
			</div>
                                            <div class="card-footer">
                                                   <asp:Button ID="btnJobconfigConfig" runat="server" CommandName="Markup" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>
        
         <div class="col-md-6 col-xl-3 card border-0 m-5" style="display:none;visibility:hidden;">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="fas fa-bolt fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">Source of Request</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">   Where the job originated from</p>
			</div>
                                            <div class="card-footer">
                                                     <asp:Button ID="btnSourceofRequest" runat="server" CommandName="SourceofRequest" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>

         <div class="col-md-6 col-xl-3 card border-0 m-5" style="display:none;visibility:hidden;">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="fab fa-whmcs fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">Service Charge</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">    The category of the job </p>
			</div>
                                            <div class="card-footer">
                                                     <asp:Button ID="btnServiceCharge" runat="server" CommandName="ServiceCharge" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>











		         <div class="col-md-6 col-xl-3 card border-0 m-5">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="fab fa-whmcs fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">Event Display Settings</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">   Use this section to customise your event display. </p>
			</div>
                                            <div class="card-footer">
                                                     <asp:Button ID="Button17" runat="server" CommandName="EventDisplay" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>









         <div class="col-md-6 col-xl-3 card border-0 m-5" style="display:none;visibility:hidden;">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="fas fa-envelope fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark"> From Email</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">    This sets the email that the system sends emails to your customer from. You can set it to any valid email address.</p>
			</div>
                                            <div class="card-footer">
                                                    <asp:Button ID="btnEmailFromEmail" runat="server" CommandName="FromMail" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>
         <div class="col-md-6 col-xl-3 card border-0 m-5">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9 d-flex">
		<div class="card-title m-auto">
			<div class="symbol symbol-50px w-100px bg-light d-flex">
			<i class="bi bi-file-earmark-text fa-4x m-auto"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">Email Footer</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">     The bottom section of an <b>e-mail</b> message that contains information that does not change from one <b>e-mail</b> marketing campaign to another</p>
			</div>
                                            <div class="card-footer">
                                                    <asp:Button ID="btnEmailFooter" runat="server" CommandName="EmailFooter" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>
		   <div class="col-md-6 col-xl-3 card border-0 m-5">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9 fa-4x d-flex">
		<div class="card-title m-auto">
			<div class="symbol symbol-50px w-100px bg-light d-flex">
			<i class="bi bi-envelope-open-heart fa-4x m-auto"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark"> Donation Thank  You Email</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">     Use this section to configure email templates which are sent to donors once they make a donation.</p>
			</div>
                                            <div class="card-footer">
                                                    <asp:Button ID="Button8" runat="server" CommandName="thankyoumail" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>
        <div class="col-md-6 col-xl-3 card border-0 m-5">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9 fa-4x d-flex">
		<div class="card-title m-auto">
			<div class="symbol symbol-50px w-100px bg-light d-flex">
			<i class="bi bi-menu-button fa-4x m-auto"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark"> Custom Fields</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">    Use this section to create custom fields to store specific data about your members</p>
			</div>
                                            <div class="card-footer">
                                                    <asp:Button ID="Button9" runat="server" CommandName="custom" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>

		  <div class="col-md-6 col-xl-3 card border-0 m-5" style="display:none;visibility:hidden;">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="fas fa-envelope fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark"> Company</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">    Use this panel to add information about companies that support your cause</p>
			</div>
                                            <div class="card-footer">
                                                    <asp:Button ID="Button10" runat="server" CommandName="company" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>
		 <div class="col-md-6 col-xl-3 card border-0 m-5" style="display:none;visibility:hidden;">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="fas fa-envelope fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark"> Platform Support Report</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">    Use this section to produce a report of the platform fees gathered by the platform. You will be invoiced at the end of the month for these. </p>
			</div>
                                            <div class="card-footer">
                                                    <asp:Button ID="Button11" runat="server" CommandName="platform" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>
		<div class="col-md-6 col-xl-3 card border-0 m-5" style="display:none;visibility:hidden;">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="bi bi-bar-chart fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark"> Product Category</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">     Add categories for your online shop using this section. </p>
			</div>
                                            <div class="card-footer">
                                                    <asp:Button ID="Button12" runat="server" CommandName="product" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>

		
        <div class="col-md-6 col-xl-3 card border-0 m-5" id="DivDefaultMarkup" runat="server" visible="false"  style="display:none;visibility:hidden;">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="fas fa-arrow-up fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">Set Default Mark-up Percentage</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">         Set the default sales markup price within estimates. The portal will apply a 25% mark-up by default.</p>
			</div>
                                            <div class="card-footer">
                                               <asp:Button ID="btnMarkup" runat="server" CommandName="Markup" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>

         <div class="col-md-6 col-xl-3 card border-0 m-5" id="DivInternalCost" runat="server" visible="false" style="display:none;visibility:hidden;">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="fas fa-arrow-up fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">Internal Cost</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">        Enable Internal Cost.</p>
			</div>
                                            <div class="card-footer">
                                             <asp:Button ID="Button7" runat="server" CommandName="Internal" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>
         <div class="col-md-6 col-xl-3 card border-0 m-5" id="DivInventoryCategory"  runat="server" visible="false">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="fas fa-bars fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">Inventory Category</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">        The category of equipment that you will store in your warehouse or vans.</p>
			</div>
                                            <div class="card-footer">
                                             <asp:Button ID="btnWarehourse" runat="server" CommandName="InventoryCategory" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>

         <div class="col-md-6 col-xl-3 card border-0 m-5" id="DivStorageLocations" runat="server" visible="false">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="fas fa-bars fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">Storage Locations</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">       Vans, warehouses, offices where you plan to store your equipment.</p>
			</div>
                                            <div class="card-footer">
                                              <asp:Button ID="btnStorageLocations" runat="server" CommandName="StorageLocations" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>


         <div class="col-md-6 col-xl-3 card border-0 m-5" id="DivCatalogues" runat="server" visible="false">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9">
		<div class="card-title m-0">
			<div class="symbol symbol-50px w-100px bg-light">
			<i class="fas fa-bars fa-4x p-3"></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">Catalogues</div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">     If you have a list of products and services that you offer your clients on a regular basis then you can configure them here. This will make creating estimates simpler. </p>
			</div>
                                            <div class="card-footer">
                                         <asp:Button ID="Button3" runat="server" CommandName="SuppliersandCatalogues" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>

				   <div class="col-md-6 col-xl-3 card border-0 m-5">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9" style="display:flex">
		<div class="card-title " style="margin:auto">
			<div class="symbol symbol-50px w-100px bg-light d-flex">
			<i class="bi bi-arrow-left-right fa-4x m-auto" ></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">MailChimp </div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">     Configure your Mailchimp API Key to allow you to synchronise your contracts with the Donor CRM.</p>
			</div>
                                            <div class="card-footer">
                                                    <asp:Button ID="Button13" runat="server" CommandName="mailchimp" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>


						   <div class="col-md-6 col-xl-3 card border-0 m-5">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9" style="display:flex">
		<div class="card-title " style="margin:auto">
			<div class="symbol symbol-50px w-100px bg-light d-flex">
			<i class="bi bi-wordpress fa-4x m-auto" ></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">WordPress </div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;">     Use this section to customise how the Fundraiser appears on your WordPress site.</p>
			</div>
                                            <div class="card-footer">
                                                    <asp:Button ID="Button14" runat="server" CommandName="wordpress" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>


								   <div class="col-md-6 col-xl-3 card border-0 m-5">
<a href="#" class="card border-hover-primary">
	<div class="card-header border-0 pt-9" style="display:flex">
		<div class="card-title " style="margin:auto">
			<div class="symbol symbol-50px w-100px bg-light d-flex">
			<i class="bi bi-gift fa-4x m-auto" ></i>
			</div>
			</div>
			</div>
			<div class="card-body p-9" style="height: 180px;">
			<div class="fs-1 fw-bolder text-dark">Gift Aid </div>
			<p class="text-gray-400 fw-bold fs-5 mt-1 mb-7"  style="height:110px;overflow-y:auto;"> Configure Gift Aid Settings.</p>
			</div>
                                            <div class="card-footer">
                                                    <asp:Button ID="Button15" runat="server" CommandName="giftaid" OnCommand="BtnOpen_Command" 
                                                                                                   Text="Open" SkinID="btnDefault" Font-Size="Large" style="width:100%" />
                  </div>
</a>
</div>


         
    
        </div>
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
