<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="CardConnectInegration.aspx.cs" Inherits="DeffinityAppDev.App.CardConnectInegration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
	Admin
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Card Connect Integration</h3>
									</div>
									<!--end::Card title-->
								</div>
								<!--begin::Card header-->
								<!--begin::Content-->
								<div id="kt_account_profile_details" class="collapse show" style="">
									<!--begin::Form-->
									<form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
										<!--begin::Card body-->
										<div class="card-body border-top p-9">
											
											<!--begin::Input group-->
											<%--<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Organization Name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtOrganizationName" runat="server" MaxLength="500"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>--%>
											<!--end::Input group-->
										 <div class="row mb-6" id="pnlVendor" runat="server">
                                 
                                       <label class="col-lg-2 col-form-label required fw-bold fs-6"><asp:Label ID="lblVendor" runat="server" Text="MID"></asp:Label> </label>
                                      <div class="col-lg-4 form-inline">
                                          <asp:TextBox ID="txtVendor" runat="server" MaxLength="250" SkinID="txt_80"></asp:TextBox>
                                         
                                          </div>
                                    
    </div>
    <div class="row mb-6" id="pnlUsername" runat="server">
                                 
                                       <label class="col-lg-2 col-form-label required fw-bold fs-6">Username</label>
                                      <div class="col-lg-4 form-inline">
                                          <asp:TextBox ID="txtUsername" runat="server" MaxLength="250" SkinID="txt_80"></asp:TextBox>
                                          
                                          </div>
                                      
    </div>
<div class="row mb-6" id="pnlPassword" runat="server">
                                
                                       <label class="col-lg-2 col-form-label required fw-bold fs-6">Password</label>
                                      <div class="col-lg-4 form-inline">
                                          <asp:TextBox ID="txtPassword" runat="server" MaxLength="250" SkinID="txt_80"></asp:TextBox>
                                           
                                         
                                      </div>
    </div>
	
    <div class="row mb-6"  id="pnlHost" runat="server">
                                 
                                       <label class="col-lg-2 col-form-label required fw-bold fs-6">Host</label>
                                      <div class="col-lg-4 form-inline">
                                          <asp:TextBox ID="txtHost" runat="server" MaxLength="500" Text="pilot-payflowpro.paypal.com" SkinID="txt_80"></asp:TextBox>
                                         
                                          </div>
                                      
    </div>
     <div class="row mb-6">
      <div class="col-md-8">
          <label class="col-sm-2 control-label"> </label>
          <div class="col-sm-8 form-inline">
              
              </div>
          </div>
         </div>
											<!--begin::Input group-->
											
											<!--end::Input group-->
											
											
											<!--begin::Input group-->
										
										</div>
										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<%--<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

											<asp:Button ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click" />
										</div>
										<!--end::Actions-->
									<input type="hidden"><div></div></form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
</asp:Content>
