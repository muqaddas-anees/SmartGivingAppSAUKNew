<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="AddSponsor.aspx.cs" Inherits="DeffinityAppDev.App.Sponsor.AddSponsor" %>


<%@ Register Src="~/App/Events/controls/EventTabs.ascx" TagPrefix="Pref" TagName="EventTabs" %>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:EventTabs runat="server" id="EventTabs" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button"  data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"> Information About The Sponsor </h3>
									</div>

									  <div class="card-toolbar"  data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Click to add a user">
                                          
                                         
										 </div>

									 <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Click to add a user">

										<%-- <asp:Button ID="BackToGrid" runat="server" CssClass="btn btn-primary" Text="Add Breakout Sessions" OnClick="BackToGrid_Click"   />--%>
										 <asp:Button ID="idCancel" runat="server" CssClass="btn btn-light"  Text=" Cancel " OnClick="BackToGrid_Click"  Visible="false"  />
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
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Company Name </label>
												<!--end::Label-->
												<!--begin::Col-->

												<div class="col-lg-5 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtCompanyName" runat="server" placeholder=""   ></asp:TextBox>
													<asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator3" Display="Dynamic" runat="server" 
							ForeColor="Red" ErrorMessage="Please enter Company Name" ControlToValidate="txtCompanyName" ValidationGroup="group1" ></asp:RequiredFieldValidator>
														</div>

											



										





											</div>
											<div class="row mb-6">
												<label class="col-lg-4 col-form-label  fw-bold fs-6"></label>
													<div class="col-lg-3 fv-row fv-plugins-icon-container">
														<label class="col-lg-5 col-form-label required fw-bold fs-6">	Browse Logo </label>
														<br />
																<div class="image-input image-input-outline" data-kt-image-input="true" style="background-image: url(assets/media/avatars/blank.png)">
														<!--begin::Preview existing avatar-->
														<div class="image-input-wrapper w-125px h-125px" style="background-image: url(assets/media/avatars/150-26.jpg)"><asp:Image ID="img" runat="server" Height="150px" /></div>
														<!--end::Preview existing avatar-->
														<!--begin::Label-->
														<label class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="change" data-bs-toggle="tooltip" title="" data-bs-original-title="Change avatar">
															<i class="bi bi-pencil-fill fs-7"></i>
															<!--begin::Inputs-->
															<%--<input type="file" name="avatar" accept=".png, .jpg, .jpeg" runat="server" id="imgLogo">--%>
															<asp:FileUpload runat="server" id="imgLogo" />
															<input type="hidden" name="avatar_remove">
															<!--end::Inputs-->
														</label>
														<!--end::Label-->
														<!--begin::Cancel-->
														<span class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="cancel" data-bs-toggle="tooltip" title="" data-bs-original-title="Cancel avatar">
															<i class="bi bi-x fs-2"></i>
														</span>
														<!--end::Cancel-->
														<!--begin::Remove-->
														<span class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="remove" data-bs-toggle="tooltip" title="" data-bs-original-title="Remove avatar">
															<i class="bi bi-x fs-2"></i>
														</span>
														<!--end::Remove-->
													</div>


														<div class="fv-plugins-message-container invalid-feedback">
															<br />
															
														
														</div>



												</div>

												</div>
											<!--end::Input group-->


														<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6"> About the Company </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-9 fv-row fv-plugins-icon-container">
													
															<%--<textarea id="txtDescriptionArea" rows="4"  style="background-color:#F5F8FA;border:none;  " cols="100"></textarea>--%>

															<asp:TextBox ID="txtDescriptionAboutCompany" runat="server" TextMode="MultiLine" Height="200" Columns="100" ></asp:TextBox>

															
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->







											
													<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Contact Name </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtContactName" runat="server" placeholder=""  ></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->




											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Contact Email  </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtContactEmail" runat="server" placeholder=""  ></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->



											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Contact Phone Number  </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtContactPhoneNumber" runat="server"  placeholder="" ></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->




											<div class="row mb-6">
												
												<!--begin::Col-->


												<div class="col-lg-12">

													

													<div class="row">


														<!--begin::Col-->
														<label class="col-lg-4 col-form-label  fw-bold fs-6">  Amount Sponsored  </label>
														<div class="col-lg-4 fv-row fv-plugins-icon-container">

														
															
                                                           
															<asp:TextBox ID="txtAmountSponsored" runat="server"  placeholder="0.00" TextMode="Number" ></asp:TextBox>

															
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
														
														<div class="col-lg-3 fv-row fv-plugins-icon-container">
															
															
															

														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
													</div>
												</div>
												<!--end::Col-->
											</div>

											
											<!--begin::Input group-->


											<br />

										
											<div class="row mb-6">
												
												<!--begin::Col-->
												<label class="col-lg-4 col-form-label fw-bold fs-6"> Date Pledged :</label>
												<div class="col-lg-6">
													

													<asp:TextBox  ID="txtDatePledged"    Text="" SkinID="DateNew" runat="server"></asp:TextBox>
													</div>
												</div>






											<div class="row mb-6">
												
												<!--begin::Col-->


												<div class="col-lg-12">

													

													<div class="row">


														<!--begin::Col-->
														<label class="col-lg-4 col-form-label  fw-bold fs-6"> Status  </label>
														<div class="col-lg-4 fv-row fv-plugins-icon-container">

<%--															<label class=" col-form-label required fw-bold fs-6">Status</label>--%>
															
                                                            <asp:DropDownList ID="DropDownListStatus" runat="server">

                                                                <asp:ListItem Value="" > Select...  </asp:ListItem>
																 <asp:ListItem Value="Pending Payment" > Pending Payment  </asp:ListItem>
																 <asp:ListItem Value="Paid" >  Paid </asp:ListItem>
																 <asp:ListItem Value="Cancelled" >  Cancelled </asp:ListItem>
																 <asp:ListItem Value="Dispute" >  Dispute </asp:ListItem>
        
                                                               
                                                            </asp:DropDownList>


															
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
														
														<div class="col-lg-3 fv-row fv-plugins-icon-container">
														
															
															

														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
													</div>
												</div>
												<!--end::Col-->
											</div>


											
													<div class="row">


														<!--begin::Col-->
														<label class="col-lg-4 col-form-label  fw-bold fs-6">  Date Paid : </label>
														<div class="col-lg-4 fv-row fv-plugins-icon-container">
															<asp:TextBox  ID="txtDatePaid"    Text="" SkinID="DateNew" runat="server"></asp:TextBox>
															</div>
														</div>

										
										</div>





										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<%--<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

											   
											<asp:Button ID="btnSaveAndEdit" runat="server" SkinID="btnDefault"  Text="Save" OnClick="btnSaveAndEdit_Click" ValidationGroup="group1"  />   
											
											

										</div>
										<!--end::Actions-->
									<input type="hidden"><div></div></form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>

	<style type="text/css">
  .BigCheckBox input { width:25px; height:25px;   border-radius: 50%; background-color:#F5F8FA; padding:40px; padding-bottom:4px;  }

  .checkbox-round {
    width: 1.3em;
    height: 1.3em;
    background-color: white;
    border-radius: 50%;
    vertical-align: middle;
    border: 1px solid #ddd;
   
    -webkit-appearance: none;
    outline: none;
    cursor: pointer;
}

  .checkbox-round:checked {
    background-color: gray;
}

</style>
</asp:Content>
