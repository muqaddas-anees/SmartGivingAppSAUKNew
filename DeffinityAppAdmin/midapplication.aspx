<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="midapplication.aspx.cs" Inherits="DeffinityAppDev.App.SigUp_Form" %>

<!DOCTYPE html>





<html lang="en">
	<!--begin::Head-->
	<head><base href="">
			<title>Smart Giving </title>
		
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<meta charset="utf-8" />
		<meta property="og:locale" content="en_US" />
		<meta property="og:type" content="article" />
		<%--<meta property="og:title" content="Metronic - Bootstrap 5 HTML, VueJS, React, Angular &amp; Laravel Admin Dashboard Theme" />--%>
		<%--<meta property="og:url" content="https://keenthemes.com/metronic" />--%>
		<%--<meta property="og:site_name" content="Keenthemes | Metronic" />--%>
		<link rel="canonical" href="Https://preview.keenthemes.com/metronic8" />
		<%--<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />--%>
		<!--begin::Fonts-->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" />
		<!--end::Fonts-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<%--<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />--%>
		<!--end::Global Stylesheets Bundle-->
		<!--begin::Fonts-->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" />
		<!--end::Fonts-->
		<!--begin::Page Vendor Stylesheets(used by this page)-->
		<link href="assets/plugins/custom/fullcalendar/fullcalendar.bundle.css" rel="stylesheet" type="text/css" />
		<!--end::Page Vendor Stylesheets-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />
		<!--end::Global Stylesheets Bundle-->
	</head>
	<!--end::Head-->
	<!--begin::Body-->
	<body id="kt_body" class="header-fixed header-tablet-and-mobile-fixed toolbar-enabled toolbar-fixed aside-enabled aside-fixed" style="--kt-toolbar-height:55px;--kt-toolbar-height-tablet-and-mobile:55px">
		   <div class="page-loader">
			<span class="spinner-border text-primary" role="status">
				<span class="visually-hidden">Loading...</span>
			</span>
		</div>
		<form id="form1" runat="server">
		<!--begin::Main-->
		<!--begin::Root-->
		<div class="d-flex flex-column flex-root">
			<!--begin::Page-->
			<div class="page d-flex flex-row flex-column-fluid">
				<!--begin::Aside-->
				
				<!--end::Aside-->
				<!--begin::Wrapper-->
				<div class=" d-flex flex-column flex-row-fluid" id="kt_wrapper">
					<!--begin::Header-->
				
					<!--end::Header-->
					<!--begin::Content-->
					<div class="content d-flex flex-column flex-column-fluid" id="kt_content">
						<!--begin::Toolbar-->
					
						<!--end::Toolbar-->
						<!--begin::Post-->
						<div class="post d-flex flex-column-fluid" id="kt_post">
							<!--begin::Container-->
							<div id="kt_content_container" class="container-xxl">
								<!--begin::Card-->
								<div class="card shadow-sm">
									<div class="card-header p-10 " style="background-color:#064164; color:#fff;">
									<h1 class="card-img-top" style="color: #fff;">CARD CONNECT</h1>
									<h3 class="card-title" style="color: #fff;">Application Information for E-Signature</h3>
									
									</div>
									<div class="card-body border-top p-9">
										<!--begin::Input group-->
										
										<!--end::Input group-->
										<!--begin::Input group-->
										
										<!--end::Input group-->
										<!--begin::Input group-->
										<div class="row mb-6">
											<!--begin::Label-->
											
											<!--end::Label-->
											<!--begin::Col-->
											<label class="col-lg-2 col-form-label required fw-bold fs-6">Business Legal Name:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" >--%><div class="row mb-4">
                                                    <asp:TextBox ID="txtBusinessLegalName" runat="server" placeholder="Business Legal Name"  MaxLength="250"></asp:TextBox>
													<asp:RequiredFieldValidator ID="rfBname" runat="server" ControlToValidate="txtBusinessLegalName" ErrorMessage="Please enter business legal name" Display="Dynamic" ValidationGroup="g"></asp:RequiredFieldValidator>
                                                </div>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>

											<label class="col-lg-2 col-form-label required fw-bold fs-6">DBA Name</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" >--%>
												 <asp:TextBox ID="txtDbaName" runat="server" placeholder="DBA Name"  MaxLength="250" ></asp:TextBox>
											<div class="fv-plugins-message-container invalid-feedback"></div></div>									
											
											<!--end::Col-->
										</div>
										<div class="row mb-12">
											<!--begin::Label-->
											
											<!--end::Label-->
											<!--begin::Col-->
											<label class="col-lg-2 col-form-label required fw-bold fs-6">Business Address:</label>
											<div class="col-lg-10 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtBusinessAddress" runat="server"    placeholder="Business Address"  SkinID="txtMulti_80" TextMode="MultiLine" ></asp:TextBox>
												<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBusinessAddress" 
													ErrorMessage="Please enter business address" Display="Dynamic" ValidationGroup="g"></asp:RequiredFieldValidator>
<div class="fv-plugins-message-container invalid-feedback"></div></div>													
											
											<!--end::Col-->
										</div>

										<div class="row mb-6">
											<!--begin::Label-->
											
											<!--end::Label-->
											<!--begin::Col-->
											<label class="col-lg-2 col-form-label required fw-bold fs-6"  >City:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtCity" runat="server"      placeholder="City" MaxLength="250" ></asp:TextBox>
												<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCity" 
													ErrorMessage="Please enter city" Display="Dynamic" ValidationGroup="g"></asp:RequiredFieldValidator>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>

											<label class="col-lg-2 col-form-label required fw-bold fs-6">State:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtState" runat="server"    placeholder="State"  MaxLength="250"  ></asp:TextBox>
												<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtState" 
													ErrorMessage="Please enter state" Display="Dynamic" ValidationGroup="g"></asp:RequiredFieldValidator>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>									

											</div>
											<div class="row mb-6">
											<label class="col-lg-2 col-form-label required fw-bold fs-6">Zip:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtZip" runat="server"    placeholder="Zip" MaxLength="50"  ></asp:TextBox>
												<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtZip" 
													ErrorMessage="Please enter zip" Display="Dynamic" ValidationGroup="g"></asp:RequiredFieldValidator>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>

											<label class="col-lg-2 col-form-label required fw-bold fs-6">Business Phone:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtBusinessPhone" runat="server"     placeholder="Business Phone" MaxLength="20"   ></asp:TextBox>
													<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBusinessPhone" 
													ErrorMessage="Please enter business phone" Display="Dynamic" ValidationGroup="g"></asp:RequiredFieldValidator>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>	
										</div>
										<div class="row mb-6">
											<label class="col-lg-2 col-form-label required fw-bold fs-6">Business Fax:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtBusinessFax" runat="server"    placeholder="Business Fax"  MaxLength="20" ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>

											<label class="col-lg-2 col-form-label required fw-bold fs-6">Email:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtEmail" runat="server"   placeholder="Email"  MaxLength="250" ></asp:TextBox>
												<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtState" 
													ErrorMessage="Please enter email" Display="Dynamic" ValidationGroup="g"></asp:RequiredFieldValidator>
												<asp:RegularExpressionValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtState" 
													ErrorMessage="Please enter email" Display="Dynamic" ValidationGroup="g"></asp:RegularExpressionValidator>--%>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>	
											<!--end::Col-->
										</div>


											<!--begin::Label-->
											
											<!--end::Label-->
											<!--begin::Col-->
										

											
											<div class="row mb-12">
												<label class="col-lg-2 col-form-label required fw-bold fs-6">Requested Start Date: </label>
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                    <asp:TextBox ID="txtRequestedStartDate" runat="server"     placeholder="Requested Start Date" SkinID="DateNew" style="width:100%" MaxLength="20"  ></asp:TextBox>
													<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtRequestedStartDate" 
													ErrorMessage="Please enter requested start date" Display="Dynamic" ValidationGroup="g"></asp:RequiredFieldValidator>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>
												<!--begin::Label-->
												<label class="col-lg-2 col-form-label required fw-bold fs-6">Is this date flexible?:</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-1 fv-row fv-plugins-icon-container mt-2">
													<div class="form-check form-check-custom form-check-solid">
														<input name="flexRadioDefault" class="form-check-input" type="radio" value="Yes" id="chk_date_flexible_yes_12" runat="server">
														<label class="form-check-label" for="12">
															Yes
														</label>
													</div>
												<div class="fv-plugins-message-container invalid-feedback"></div></div>	
	
												<div class="col-lg-1 fv-row fv-plugins-icon-container mt-2">
													<div class="form-check form-check-custom form-check-solid">
														<input name="flexRadioDefault" class="form-check-input" type="radio" value="No" id="chk_date_flexible_no_12" runat="server">
														<label class="form-check-label" for="13">
															No
														</label>
													</div>
												<div class="fv-plugins-message-container invalid-feedback"></div></div>													
												<!--end::Col-->
											</div>	
										
										<div class="row mb-12">
												<label class="col-lg-2 col-form-label required fw-bold fs-6">Enable ACH Payments: </label>
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:CheckBox ID="chkACHPayment" runat="server" style="padding-top:30px; margin-top:15px;font-size:25px" />
													</div>
											</div>
										
										<hr>	
										
										<div class="row mb-6">
											<!--begin::Label-->
											
											<!--end::Label-->
											<!--begin::Col-->
											<label class="col-lg-2 col-form-label required fw-bold fs-6">Services Sold:</label>
											<div class="col-lg-10 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" placeholder="Products/Services Sold"><br />--%>
                                                <asp:TextBox ID="txtServicesSold" runat="server"    placeholder="Products/Services Sold" MaxLength="250"   ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>													
											
											<!--end::Col-->
										</div>

										<div class="row mb-6">
											<!--begin::Label-->
											
											<!--end::Label-->
											<!--begin::Col-->
											<label class="col-lg-2 col-form-label required fw-bold fs-6"> Fed Tax ID:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" placeholder="Fed Tax ID"><br />--%>
                                                <asp:TextBox ID="txtFedTaxID" runat="server"    placeholder="Fed Tax ID"    MaxLength="20"  ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>

											<label class="col-lg-2 col-form-label required fw-bold fs-6">Fed ID Started:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" placeholder="Fed ID Month / Year Started"><br />--%>
                                                <asp:TextBox ID="txtFedIDStarted" runat="server"    placeholder="Fed ID Month / Year Started" MaxLength="20"    ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>									
											
											<!--end::Col-->
										</div>
										

										<div class="row mb-12">
											<!--begin::Label-->
											
											<!--end::Label-->
											<!--begin::Col-->
											<label class="col-lg-2 col-form-label required fw-bold fs-6">Tax Filing Type?:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" placeholder="Products/Services Sold"><br />--%>
                                                <asp:TextBox ID="txtTaxFilingType" runat="server"        placeholder="Tax Filing Type" MaxLength="50"  ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>													
											<div class="col-lg-6 fv-row fv-plugins-icon-container mt-2">
												(Sole Proprietor, Partnership, Corporation – Public or Private)
											<div class="fv-plugins-message-container invalid-feedback"></div></div>
											
											<!--end::Col-->
										</div>

										<div class="row mb-12">
											
											<!--begin::Label-->
											<label class="col-lg-4 col-form-label required fw-bold fs-6">Tax Exempt Organization?:</label>
											<!--end::Label-->
											<!--begin::Col-->
											<div class="col-lg-1 fv-row fv-plugins-icon-container mt-2">
												<div class="form-check form-check-custom form-check-solid">

													<input name="123" class="form-check-input" type="radio" value="Yes" id="chk_tax_yes_12" runat="server">
													<label class="form-check-label" for="112">
														Yes
													</label>
												</div>
											<div class="fv-plugins-message-container invalid-feedback"></div></div>	

											

											<div class="col-lg-1 fv-row fv-plugins-icon-container mt-2">
												<div class="form-check form-check-custom form-check-solid">
													<input name="123" class="form-check-input" type="radio" value="No" id="chk_tax_no_12" runat="server">
													<label class="form-check-label" for="113">
														No
													</label>
												</div>
											<div class="fv-plugins-message-container invalid-feedback"></div></div>	
											
											
											<!--end::Col-->
											<label class="col-lg-2 col-form-label fw-bold fs-6">Number of Employees?: </label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtNumberofEmployees" runat="server"    placeholder="Number of Employees" MaxLength="10"  ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>
										</div>	

										<div class="row">
											<!--begin::Label-->
											
											<!--end::Label-->
											<!--begin::Col-->
											<label class="col-lg-5 col-form-label fw-bold fs-6">Estimated Average $$ Credit Card Individual Sale Amount: </label>
											<div class="col-lg-6 fv-row fv-plugins-icon-container mb-6">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtEstimatedAverage" runat="server"   TextMode="Number" SkinID="Price"    placeholder="Estimated Average  Credit Card Individual Sale" MaxLength="20"   ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>
										
											<label class="col-lg-5 col-form-label fw-bold fs-6">Estimated Highest $$ Credit Card Individual Sale Amount:</label>
											<div class="col-lg-6 fv-row fv-plugins-icon-container mb-6">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtEstimatedHighest" runat="server"    TextMode="Number" SkinID="Price"  placeholder="Estimated Highest Credit Card Individual Sale" MaxLength="20" ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>
																			
											
											<!--end::Col-->
										</div>
										<hr class="mb-12">
										<h6 class="mb-12">Signor (Must be officer or controller of private Corp., member LLC, or individual only of sole proprietorship)</h6>

										<div class="row mb-6">
											<!--begin::Label-->
											
											<!--end::Label-->
											<!--begin::Col-->
											<label class="col-lg-2 col-form-label fw-bold fs-6">Signor/Owner Name: </label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtSignorOwnerName" runat="server"     placeholder="Signor/Owner Name"    MaxLength="250"   ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>

											<label class="col-lg-2 col-form-label fw-bold fs-6">Signor Title: </label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtSignorTitle" runat="server"    placeholder="Signor Title"  MaxLength="250"  ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>		
											
											
											
											<!--end::Col-->
										</div>

										<div class="row mb-6">
											<!--begin::Label-->
											
											<!--end::Label-->
											<!--begin::Col-->
											<label class="col-lg-2 col-form-label fw-bold fs-6">Signor Date of Birth: </label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtSignorDateofBirth" runat="server"     placeholder="Signor Date of Birth" SkinID="DateNew" style="width:100%" MaxLength="20" ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>

											<label class="col-lg-2 col-form-label fw-bold fs-6">Signor Home Phone: </label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtSignorHomePhone" runat="server" placeholder="Signor Home Phone" MaxLength="20"   ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>		
											<!--end::Col-->
										</div>

										<div class="row mb-12">
											<!--begin::Label-->
											
											<!--end::Label-->
											<!--begin::Col-->
											<label class="col-lg-2 col-form-label fw-bold fs-6">Signor Home Address: </label>
											<div class="col-lg-10 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtSignorHomeAddress" runat="server"       placeholder="Signor Home Address" MaxLength="500"  ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>

												
											<!--end::Col-->
										</div>

										<div class="row mb-6">
											<!--begin::Label-->
											
											<!--end::Label-->
											<!--begin::Col-->
											<label class="col-lg-1 col-form-label fw-bold fs-6">City: </label>
											<div class="col-lg-3 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtSignorCity" runat="server"    placeholder="City"  MaxLength="250" ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>

											<label class="col-lg-1 col-form-label fw-bold fs-6">State: </label>
											<div class="col-lg-3 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtSignorState" runat="server"     placeholder="State"  MaxLength="250" ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>		
											
											<label class="col-lg-1 col-form-label fw-bold fs-6">Zip: </label>
											<div class="col-lg-3 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtSignorZip" runat="server"     placeholder="Zip" MaxLength="20"  ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>		
											
											<!--end::Col-->
										</div>
										
										
										<hr class="mb-12">
										<h6 class="mb-12">Deposit Bank Name (checking only)</h6>
										<div class="row mb-6">
											<!--begin::Label-->
											<!--end::Label-->
											<!--begin::Col-->
											<label class="col-lg-2 col-form-label fw-bold fs-6">Bank Name:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtBankName" runat="server"    placeholder="Bank Name" MaxLength="250"   ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>		

											<label class="col-lg-2 col-form-label fw-bold fs-6">Bank Account:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtBankAccount" runat="server"     placeholder="Bank Account" MaxLength="50"  ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>	
											
											<!--end::Col-->
										</div>

										<div class="row mb-6">
											<!--begin::Label-->
											<!--end::Label-->
											<!--begin::Col-->
											<label class="col-lg-2 col-form-label fw-bold fs-6">Bank Routing #:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtBankRouting" runat="server"    placeholder="Bank Routing #" MaxLength="50" ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>		

											<label class="col-lg-2 col-form-label fw-bold fs-6">Bank Phone:</label>
											<div class="col-lg-4 fv-row fv-plugins-icon-container">
												<%--<input type="text" name="Property" class="form-control form-control-lg form-control-solid" ><br />--%>
                                                <asp:TextBox ID="txtBankPhone" runat="server"    placeholder="Bank Phone" MaxLength="50"   ></asp:TextBox>
&nbsp;<div class="fv-plugins-message-container invalid-feedback"></div></div>	
											
											<!--end::Col-->
										</div>

									

										


										

										
										<!--end::Input group-->
										<!--begin::Input group-->
										
										<!--end::Input group-->
										<!--begin::Input group-->
										
									
									</div>
									<div class="card-footer" style="background-color:#064164; color: #fff;">
									
										An electronic application will be generated and emailed to you for e-signature.<br>
										We will need a copy of a voided business check to complete setup of your merchant processing account.
									<div class=" d-flex justify-content-end py-6 px-9">										
											<%--<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Submit</button>--%>	
										

                                        <asp:Button ID="btnSubmitDetails" runat="server" Text="Submit" OnClick="btnSubmitDetails_Click" Width="200px" ValidationGroup="g"     />



									</div></div>
									
								</div>
								<!--end::Card-->
							</div>
							<!--end::Container-->
						</div>
						<!--end::Post-->
					</div>
					<!--end::Content-->
					<!--begin::Footer-->
					<div class="footer py-4 d-flex flex-lg-column" id="kt_footer">
						<!--begin::Container-->
						<div class="container-fluid d-flex flex-column flex-md-row align-items-center justify-content-between">
							<!--begin::Copyright-->
							<%--<div class="text-dark order-2 order-md-1">
								<span class="text-muted fw-bold me-1">2021©</span>
								<a href="#" target="_blank" class="text-gray-800 text-hover-primary">123 Services</a>
							</div>--%>
							<!--end::Copyright-->
							<!--begin::Menu-->
							<%--<ul class="menu menu-gray-600 menu-hover-primary fw-bold order-1">
								<li class="menu-item">
									<a href="#" target="_blank" class="menu-link px-2">About</a>
								</li>
								<li class="menu-item">
									<a href="#" target="_blank" class="menu-link px-2">Support</a>
								</li>
								<li class="menu-item">
									<a href=#" target="_blank" class="menu-link px-2">Contact</a>
								</li>
							</ul>--%>
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
			<script src='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/scripts.bundle.js")%>'></script>

			<!--begin::Page Vendors Javascript(used by this page)-->
		<script src='<%:ResolveClientUrl("~/assets/plugins/custom/leaflet/leaflet.bundle.js")%>'></script>
		<!--end::Page Vendors Javascript-->
		<!--begin::Page Custom Javascript(used by this page)-->
		<script src='<%:ResolveClientUrl("~/assets/js/custom/modals/select-location.js")%>'></script>
	<%--	<script src="assets/plugins/global/plugins.bundle.js"></script>
		<script src="assets/js/scripts.bundle.js"></script>--%>
		<!--end::Global Javascript Bundle-->
		<!--begin::Page Vendors Javascript(used by this page)-->
	<%--	<script src="assets/plugins/custom/fullcalendar/fullcalendar.bundle.js"></script>--%>
		<!--end::Page Vendors Javascript-->
		<!--begin::Page Custom Javascript(used by this page)-->
		<script src="assets/js/custom/widgets.js"></script>
		<%--<script src="assets/js/custom/apps/chat/chat.js"></script>--%>
		<%--<script src="assets/js/custom/modals/create-app.js"></script>--%>
		<%--<script src="assets/js/custom/modals/upgrade-plan.js"></script>--%>
			  <script src='<%:ResolveClientUrl("~/Scripts/Utility.js")%>'></script>
		<!--end::Page Custom Javascript-->
		<!--end::Javascript-->
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
		<script>
		// Date
Inputmask({
    "mask" : "99/99/9999"
}).mask("#kt_inputmask_1");

// Phone
Inputmask({
    "mask" : "(999) 999-9999"
}).mask("#kt_inputmask_2");

// Placeholder
Inputmask({
    "mask" : "(999) 999-9999",
    "placeholder": "(999) 999-9999",
}).mask("#kt_inputmask_3");

// Repeating
Inputmask({
    "mask": "9",
    "repeat": 10,
    "greedy": false
}).mask("#kt_inputmask_4");

// Right aligned
Inputmask("decimal", {
    "rightAlignNumerics": false
}).mask("#kt_inputmask_5");

// Currency
Inputmask("€ 999.999.999,99", {
    "numericInput": true
}).mask("#kt_inputmask_6");

// Ip address
Inputmask({
    "mask": "999.999.999.999"
}).mask("#kt_inputmask_7");

// Email address
Inputmask({
    mask: "*{1,20}[.*{1,20}][.*{1,20}][.*{1,20}]@*{1,20}[.*{2,6}][.*{1,2}]",
    greedy: false,
    onBeforePaste: function (pastedValue, opts) {
        pastedValue = pastedValue.toLowerCase();
        return pastedValue.replace("mailto:", "");
    },
    definitions: {
        "*": {
            validator: '[0-9A-Za-z!#$%&"*+/=?^_`{|}~\-]',
            cardinality: 1,
            casing: "lower"
        }
    }
}).mask("#kt_inputmask_8");	
		</script>


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
	    </form>
	</body>
	<!--end::Body-->
</html>





<%--
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>--%>
