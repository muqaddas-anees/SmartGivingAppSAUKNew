<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SignupCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.SignupCtrl" %>

    <div class="row gy-5 g-xl-8 mb-6">
        <div class="col-xxl-12 d-flex justify-content-center">

             <div class="card card-xxl-stretch" >
											<!--begin::Header-->
											<div class="card-header border-0 py-5">
												<h3 class="card-title fw-bolder"> 
                                                     </h3>
												<div class="card-toolbar">
                                                   

                                                    </div>

                                                <div class="card-body p-0" >
                                                  
                                                     <div class="row mb-6">
         <div class="col-lg-12 col-sm-12 d-flex justify-content-center"  style="text-align:center;">
             
                    <asp:Label ID="lblHeader" runat="server" Text="Join Our Community and Stay Connected" Font-Bold="true" Font-Size="28px" ></asp:Label> <br />

                  </div>
        
        </div>
<div class="row mb-4">
                                                <hr />
    </div>


                                                  
                         

                                                      <div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">First name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-12 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtFirstName" runat="server" MaxLength="250"></asp:TextBox>
															<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtFirstName" Display="Dynamic" ErrorMessage="Please enter first name" ValidationGroup="signup"></asp:RequiredFieldValidator>
                                                    <asp:HiddenField ID="hunid" runat="server" Value="0" />  
															<asp:HiddenField ID="huid" runat="server" Value="0" />
															<asp:HiddenField ID="hPortfolioid" runat="server" Value="0" />
														<div class="fv-plugins-message-container invalid-feedback"></div>

														</div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Last name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtLastname" runat="server" MaxLength="250"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Email address</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtEmailaddress" runat="server" MaxLength="250"></asp:TextBox>
													<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtEmailaddress" Display="Dynamic" ErrorMessage="Please enter email address" ValidationGroup="signup"></asp:RequiredFieldValidator>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
													<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">State</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlStates" runat="server">
														
														<asp:ListItem Value="0" selected="True" Text="Please select..."></asp:ListItem>
  <asp:ListItem Value="Eastern Cape" Text="Eastern Cape"></asp:ListItem>
  <asp:ListItem Value="Free State" Text="Gauteng"></asp:ListItem>
  <asp:ListItem Value="KwaZulu-Natal" Text="KwaZulu-Natal"></asp:ListItem>
  <asp:ListItem Value="Limpopo" Text="Limpopo"></asp:ListItem>
  <asp:ListItem Value="Mpumalanga" Text="Mpumalanga"></asp:ListItem>
  <asp:ListItem Value="Northern Cape" Text="Northern Cape"></asp:ListItem>
  <asp:ListItem Value="North West" Text="North West"></asp:ListItem>
  <asp:ListItem Value="Western Cape" Text="Western Cape"></asp:ListItem>
													
													</asp:DropDownList>
														<asp:RequiredFieldValidator ID="RequiredFieldValidatorState" runat="server" 
                            ControlToValidate="ddlStates" InitialValue="0" Display="Dynamic" ErrorMessage="Please select state" ValidationGroup="signup"></asp:RequiredFieldValidator>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Phone number</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtContactNumber" runat="server" MaxLength="250"></asp:TextBox>
													<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtContactNumber" Display="Dynamic" ErrorMessage="Please enter phone number" ValidationGroup="signup"></asp:RequiredFieldValidator>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
													<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6"></label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:Button ID="btnJoin" runat="server" SkinID="btnDefault" Text="Join" ValidationGroup="signup" OnClick="btnJoin_Click" style="width:100%;font-size:22px;height:60px" />
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
                            </div>
                                                </div>
                 </div>
            </div>
        </div>