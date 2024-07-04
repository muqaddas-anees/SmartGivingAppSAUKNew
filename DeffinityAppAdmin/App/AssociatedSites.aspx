<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="AssociatedSites.aspx.cs" Inherits="DeffinityAppDev.App.AssociatedSites" %>

<%@ Register Src="~/App/controls/OrgTabs.ascx" TagPrefix="Pref" TagName="OrgTabs" %>


<asp:Content ID="ContentTabs" runat="server" ContentPlaceHolderID="Tabs">

    <Pref:OrgTabs runat="server" ID="OrgTabs" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

<%--    <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>--%>

    <div class="card mb-5 mb-xl-10" id="pnlGrid" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Associated Sites</h3>
									</div>
									 <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Click to add a contact">
												 <asp:Button ID="btnAddOrganization" runat="server" CssClass="btn btn-primary" Text="Add New Contact" OnClick="btnAddOrganization_Click"  />
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
										
											<!--end::Input group-->
											<!--begin::Input group-->
											  <div class="row mb-6">

                                     
                                                  

                                                   <div class="row">
                                                            <div class="col-lg-5 fv-row fv-plugins-icon-container form-inline" style="display:inline;">

                                                                </div>
                                                       </div>

											
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<asp:GridView ID="GridInstances" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridInstances_RowCommand1" OnRowDataBound="GridInstances_RowDataBound" OnPageIndexChanging="GridInstances_PageIndexChanging">
        <Columns>
              <asp:TemplateField ItemStyle-Width="9px">
                                                    <ItemTemplate>
														
														<asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' Visible="false" />
														<a href='AssociatedSites.aspx?orgid=<%# Eval("OrgID") %>&eid=<%# Eval("ID") %>' class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
																		<!--begin::Svg Icon | path: icons/duotune/art/art005.svg-->
																		<span class="svg-icon svg-icon-3">
																			<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																				<path opacity="0.3" d="M21.4 8.35303L19.241 10.511L13.485 4.755L15.643 2.59595C16.0248 2.21423 16.5426 1.99988 17.0825 1.99988C17.6224 1.99988 18.1402 2.21423 18.522 2.59595L21.4 5.474C21.7817 5.85581 21.9962 6.37355 21.9962 6.91345C21.9962 7.45335 21.7817 7.97122 21.4 8.35303ZM3.68699 21.932L9.88699 19.865L4.13099 14.109L2.06399 20.309C1.98815 20.5354 1.97703 20.7787 2.03189 21.0111C2.08674 21.2436 2.2054 21.4561 2.37449 21.6248C2.54359 21.7934 2.75641 21.9115 2.989 21.9658C3.22158 22.0201 3.4647 22.0084 3.69099 21.932H3.68699Z" fill="black"></path>
																				<path d="M5.574 21.3L3.692 21.928C3.46591 22.0032 3.22334 22.0141 2.99144 21.9594C2.75954 21.9046 2.54744 21.7864 2.3789 21.6179C2.21036 21.4495 2.09202 21.2375 2.03711 21.0056C1.9822 20.7737 1.99289 20.5312 2.06799 20.3051L2.696 18.422L5.574 21.3ZM4.13499 14.105L9.891 19.861L19.245 10.507L13.489 4.75098L4.13499 14.105Z" fill="black"></path>
																			</svg>
																		</span>
																		<!--end::Svg Icon-->
																	</a>
														<asp:LinkButton ID="btnEditContact" CausesValidation="false" runat="server" OnClick="lnkEdit_Click" CommandArgument='<%# Eval("ID") %>' 
															CssClass="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1" Visible="false">
															<span class="svg-icon svg-icon-3">
																			<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																				<path opacity="0.3" d="M21.4 8.35303L19.241 10.511L13.485 4.755L15.643 2.59595C16.0248 2.21423 16.5426 1.99988 17.0825 1.99988C17.6224 1.99988 18.1402 2.21423 18.522 2.59595L21.4 5.474C21.7817 5.85581 21.9962 6.37355 21.9962 6.91345C21.9962 7.45335 21.7817 7.97122 21.4 8.35303ZM3.68699 21.932L9.88699 19.865L4.13099 14.109L2.06399 20.309C1.98815 20.5354 1.97703 20.7787 2.03189 21.0111C2.08674 21.2436 2.2054 21.4561 2.37449 21.6248C2.54359 21.7934 2.75641 21.9115 2.989 21.9658C3.22158 22.0201 3.4647 22.0084 3.69099 21.932H3.68699Z" fill="black"></path>
																				<path d="M5.574 21.3L3.692 21.928C3.46591 22.0032 3.22334 22.0141 2.99144 21.9594C2.75954 21.9046 2.54744 21.7864 2.3789 21.6179C2.21036 21.4495 2.09202 21.2375 2.03711 21.0056C1.9822 20.7737 1.99289 20.5312 2.06799 20.3051L2.696 18.422L5.574 21.3ZM4.13499 14.105L9.891 19.861L19.245 10.507L13.489 4.75098L4.13499 14.105Z" fill="black"></path>
																			</svg>
																		</span>
														</asp:LinkButton>
                                                      <%-- <a href='Organization.aspx?orgid=<%# Eval("ID") %>' class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
																		<!--begin::Svg Icon | path: icons/duotune/art/art005.svg-->
																		<span class="svg-icon svg-icon-3">
																			<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																				<path opacity="0.3" d="M21.4 8.35303L19.241 10.511L13.485 4.755L15.643 2.59595C16.0248 2.21423 16.5426 1.99988 17.0825 1.99988C17.6224 1.99988 18.1402 2.21423 18.522 2.59595L21.4 5.474C21.7817 5.85581 21.9962 6.37355 21.9962 6.91345C21.9962 7.45335 21.7817 7.97122 21.4 8.35303ZM3.68699 21.932L9.88699 19.865L4.13099 14.109L2.06399 20.309C1.98815 20.5354 1.97703 20.7787 2.03189 21.0111C2.08674 21.2436 2.2054 21.4561 2.37449 21.6248C2.54359 21.7934 2.75641 21.9115 2.989 21.9658C3.22158 22.0201 3.4647 22.0084 3.69099 21.932H3.68699Z" fill="black"></path>
																				<path d="M5.574 21.3L3.692 21.928C3.46591 22.0032 3.22334 22.0141 2.99144 21.9594C2.75954 21.9046 2.54744 21.7864 2.3789 21.6179C2.21036 21.4495 2.09202 21.2375 2.03711 21.0056C1.9822 20.7737 1.99289 20.5312 2.06799 20.3051L2.696 18.422L5.574 21.3ZM4.13499 14.105L9.891 19.861L19.245 10.507L13.489 4.75098L4.13499 14.105Z" fill="black"></path>
																			</svg>
																		</span>
																		<!--end::Svg Icon-->
																	</a>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField ItemStyle-Width="9px" >
                                                    <ItemTemplate>
                                                        
                                                         <asp:Label ID="lblPortfolioID" runat="server" Width="40px" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                       <asp:CheckBox ID="chk" runat="server" OnClick="javascript:SelectSingleCheckbox(this.id)" Visible="false"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
           <%--  <asp:TemplateField ItemStyle-Width="50px" >
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# Bind("LogoPath") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
              <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="150px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrganization" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
           
            <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="40%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDenomination" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            
            
          
             <asp:TemplateField HeaderText="City" SortExpression="City">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
			 <asp:TemplateField HeaderText="State" SortExpression="State">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblState" runat="server"  Text='<%# Bind("State") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Zipcode" SortExpression="Postcode">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Postcode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
        </Columns>
    </asp:GridView>
											</div>
										
										
										</div>
										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
										<%--	<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

											
										</div>
										<!--end::Actions-->
									<input type="hidden"><div></div>
                                            
                                            </div>
                                            </form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
             <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

   <%--  <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlAddReligion" CancelControlID="btnClose" >
</ajaxToolkit:ModalPopupExtender>--%>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
      <asp:HiddenField ID="hid" runat="server" />
    


	 <div class="card mb-5 mb-xl-10" ID="pnlAddSite" style="background-color:white" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"><asp:Literal ID="lblAddSite" runat="server" Text="Add Site"></asp:Literal> </h3>
									</div>
									
											</div>
									<!--end::Card title-->
								
								<!--begin::Card header-->
								<!--begin::Content-->
								<div id="kt_account_profile_details" class="collapse show" style="">
									<!--begin::Form-->
									<form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
										<!--begin::Card body-->
										<div class="card-body border-top p-9">
											<!--begin::Input group-->
											<!--end::Input group-->
											<!--begin::Input group-->
											 <div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6"> Site Name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtName" runat="server" MaxLength="500"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
       
      
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Address</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtAddress" runat="server" MaxLength="4000"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Town</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row">
													<asp:TextBox ID="txtTown" runat="server" MaxLength="100"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>

													<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">State</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row">
													<asp:TextBox ID="txtState" runat="server" MaxLength="100"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<!--begin::Input group-->
										<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Zipcode</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row">
													<asp:TextBox ID="txtZipCode" runat="server" MaxLength="100"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
												<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Country</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row">
													<asp:DropDownList ID="ddlCountry" runat="server"><asp:ListItem Text="Please select..."></asp:ListItem></asp:DropDownList>
												</div>
												<!--end::Col-->
											</div>
										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
										<%--	<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>
											  	<asp:Button ID="btnClose" runat="server" CssClass="btn btn-light" Text="Close" OnClick="btnClose_Click" style="margin-right:10px" />
				<asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Changes" OnClick="btnSave_Click" />
											
										</div>
										<!--end::Actions-->
									<input type="hidden"><div></div>
                                            
                                            </div>
                                            </form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>
   
</asp:Content>