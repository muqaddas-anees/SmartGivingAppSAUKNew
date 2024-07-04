<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Organizations.aspx.cs" Inherits="DeffinityAppDev.App.Organizations" %>

<%@ Register Src="~/App/controls/OrgMainTabs.ascx" TagPrefix="Pref" TagName="OrgMainTabs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:OrgMainTabs runat="server" ID="OrgMainTabs" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
   Admin
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <style>
     .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
     .col-right{
         text-align:right;
        
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

    <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>

    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Organizations</h3>
									</div>
                                     <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Upload Organizations">
                                     <asp:Button ID="btnDownload" runat="server" CssClass="btn btn-bg-light" Text="Download Template" OnClick="btnDownload_Click" style="margin-right:10px"  />    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-bg-light" Text="Upload Organizations" OnClick="btnUpload_Click" style="margin-right:10px"  />  <asp:Button ID="btnAddOrganization" runat="server" CssClass="btn btn-primary" Text="Add New Organization" OnClick="btnAddOrganization_Click"  />
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
                                                            <label class="col-lg-4 col-form-label fw-bold fs-6">Country</label>
                                                            <asp:DropDownList ID="ddlCountry" runat="server" ><asp:ListItem Text="Please select..." ></asp:ListItem> </asp:DropDownList>
                                                            </div>
                                             <div class="col-lg-5 fv-row fv-plugins-icon-container">
                                                   <label class="col-lg-4 col-form-label fw-bold fs-6">State</label>
															<input type="text" name="fname" class="form-control form-control-lg form-control-solid mb-3 mb-lg-0" placeholder="" value="">
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
                                             	<div class="col-lg-2 fv-row fv-plugins-icon-container" style="padding-top:40px">
															   <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" style="width:100%"   />
                                                            <asp:HyperLink ID="btnLinkAddOrganization" runat="server" CssClass="btn btn-primary" Text="Add New Organization" NavigateUrl="~/App/Organization.aspx" Visible="false" ></asp:HyperLink>
														</div>
                                             </div>

                                                    <div class="row">
                                                            <div class="col-lg-5 fv-row fv-plugins-icon-container form-inline" style="display:inline;">
                                                               <input type="text" name="fname" class="form-control form-control-lg form-control-solid mb-3 mb-lg-0" placeholder="Search" runat="server" id="txtsearch" value="">
                                                                </div>
                                                      <%--   <div class="col-lg-5 fv-row fv-plugins-icon-container form-inline" style="display:inline;">
                                                             

                                                             <asp:Button ID="btnSave" runat="server" CssClass="btn btn-bg-light" Text="Save" OnClick="btnSave_Click" Visible="false"   />
                                                                </div>--%>
                                                         <div class="col-lg-2 fv-row fv-plugins-icon-container form-inline" style="display:inline;">
                                                              
                                                         

                                                        </div>
												
											
												<!--end::Col-->
											</div>

                                                   <div class="row">
                                                            <div class="col-lg-5 fv-row fv-plugins-icon-container form-inline" style="display:inline;">
                                                                <br />
                                                                </div>
                                                       </div>

											
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<asp:GridView ID="GridInstances" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridInstances_RowCommand" OnRowDataBound="GridInstances_RowDataBound" OnPageIndexChanging="GridInstances_PageIndexChanging">
        <Columns>
              <asp:TemplateField ItemStyle-Width="9px">
                                                    <ItemTemplate>
                                                       <a href='Organization.aspx?orgid=<%# Eval("ID") %>' class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
																		<!--begin::Svg Icon | path: icons/duotune/art/art005.svg-->
																		<span class="svg-icon svg-icon-3">
																			<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																				<path opacity="0.3" d="M21.4 8.35303L19.241 10.511L13.485 4.755L15.643 2.59595C16.0248 2.21423 16.5426 1.99988 17.0825 1.99988C17.6224 1.99988 18.1402 2.21423 18.522 2.59595L21.4 5.474C21.7817 5.85581 21.9962 6.37355 21.9962 6.91345C21.9962 7.45335 21.7817 7.97122 21.4 8.35303ZM3.68699 21.932L9.88699 19.865L4.13099 14.109L2.06399 20.309C1.98815 20.5354 1.97703 20.7787 2.03189 21.0111C2.08674 21.2436 2.2054 21.4561 2.37449 21.6248C2.54359 21.7934 2.75641 21.9115 2.989 21.9658C3.22158 22.0201 3.4647 22.0084 3.69099 21.932H3.68699Z" fill="black"></path>
																				<path d="M5.574 21.3L3.692 21.928C3.46591 22.0032 3.22334 22.0141 2.99144 21.9594C2.75954 21.9046 2.54744 21.7864 2.3789 21.6179C2.21036 21.4495 2.09202 21.2375 2.03711 21.0056C1.9822 20.7737 1.99289 20.5312 2.06799 20.3051L2.696 18.422L5.574 21.3ZM4.13499 14.105L9.891 19.861L19.245 10.507L13.489 4.75098L4.13499 14.105Z" fill="black"></path>
																			</svg>
																		</span>
																		<!--end::Svg Icon-->
																	</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField ItemStyle-Width="9px" >
                                                    <ItemTemplate>
                                                        
                                                         <asp:Label ID="lblPortfolioID" runat="server" Width="40px" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                       <asp:CheckBox ID="chk" runat="server" OnClick="javascript:SelectSingleCheckbox(this.id)" Visible="false"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField ItemStyle-Width="100px" >
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgLogo" runat="server" Width="100px" ImageUrl='<%# Eval("ID","~/ImageHandler.ashx?s=portfolio&id={0}") %>' />
                                                    </ItemTemplate>
                  <ItemStyle Width="8%" />
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Organization" SortExpression="Organization">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="15%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrganization" runat="server" Text='<%# Bind("InstanceName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="20%" />
                                                    <ItemTemplate>
                                                      <label style="font-weight:bold">Address: </label>   <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label> <br />
                                                       <%-- <label style="font-weight:bold">Religion: </label> <asp:Label ID="lblReligion" runat="server" Text='<%# Bind("Religion") %>'></asp:Label><br />
                                                         <label style="font-weight:bold">Group: </label> <asp:Label ID="lblGroup" runat="server" Text='<%# Bind("Group") %>'></asp:Label><br />
                                                        <label style="font-weight:bold">Denomination: </label>  <asp:Label ID="lblDenomination" runat="server" Text='<%# Bind("Denomination") %>'></asp:Label><br />--%>
                                                        
<br />                                                        
                                                         <label style="font-weight:bold">Contact: </label>  <asp:Label ID="lblContact" runat="server"  Text='<%# Bind("ContactName") %>'></asp:Label> <br />
                                                        <label style="font-weight:bold">Number: </label> <asp:Label ID="lblNumber" runat="server"  Text='<%# Bind("ContactNumber") %>'></asp:Label><br />
                                                         <label style="font-weight:bold">Email: </label> <asp:Label ID="lblemail" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label><br />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:HyperLinkField  DataNavigateUrlFields="OrgarnizationGUID"  DataNavigateUrlFormatString="~/midapplication.aspx?orgref={0}" ControlStyle-CssClass="btn btn-primary" Text="MID Application Form" Visible="false" />
              <asp:HyperLinkField Visible="false"  DataNavigateUrlFields="OrgarnizationGUID"  DataNavigateUrlFormatString="~/midapplication.aspx?orgref={0}" ControlStyle-CssClass="btn btn-alt btn-sm btn-default" DataTextFormatString="<i class='far fa-file-alt fs-2' style='padding-top: 15px;color: white;'></i>" Text="<i class='far fa-file-alt fs-2' style='padding-top: 15px;color: white;'></i>" />
               <asp:TemplateField HeaderText="" Visible="false" >
                                                    <HeaderStyle />
                                                    <ItemStyle Width="3%" />
                                                    <ItemTemplate>
                                                       <asp:Button ID="btnapplication" CommandArgument='<%# Bind("ID") %>' runat="server" SkinID="btnDefault" Text="MID Application Form" CommandName="application" Visible="false"></asp:Button> 
                                                        <asp:HyperLink ID="btnhapplication" runat="server" Target="_blank" Text="MID Application Form" CssClass="btn btn-primary" NavigateUrl="~/midapplication.aspx?orgref=<%# Bind('OrgarnizationGUID') %>"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
        
           <%-- <asp:TemplateField HeaderText="" >
                                                    <HeaderStyle />
                                                    <ItemStyle Width="3%" />
                                                    <ItemTemplate>
                                                       <asp:LinkButton ID="btnsendtheformtothechurch" CommandArgument='<%# Bind("ID") %>' runat="server" class="btn btn-alt btn-sm btn-default" ToolTip="Send the Form to the organization" CommandName="sendtochurh"  BackColor="#3699FF" style="height: 45px;width: 55px;"><i class="far fa-paper-plane fs-2" style="padding-top: 15px;color: white;"></i></asp:LinkButton> 
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="3%" />
                                                    <ItemTemplate>
                                                       <asp:LinkButton ID="btnsendcompletedformtocardconnect" CommandArgument='<%# Bind("ID") %>' runat="server" class="btn btn-alt btn-sm btn-default" ToolTip="Send Completed Form To Card Connect" CommandName="sendtocardconnect" BackColor="#F64E60" style="height: 45px;width: 55px;"><i class="fas fa-envelope-open-text fs-2" style="padding-top: 15px;color: white;"></i></asp:LinkButton> 
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
              <asp:TemplateField HeaderText="" >
                                                    <HeaderStyle />
                                                    <ItemStyle Width="3%" />
                                                    <ItemTemplate>
                                                       <asp:LinkButton ID="btnsendlogintoOrganization" CommandArgument='<%# Bind("ID") %>' runat="server" class="btn btn-alt btn-sm btn-default" ToolTip="Send Login To Organization" CommandName="sendlogin"  BackColor="#8950FC" style="height: 45px;width: 55px;"><i class="fas fa-flag-checkered fs-2" style="padding-top: 15px;color: white;"></i></asp:LinkButton> 
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                 <asp:TemplateField HeaderText="" Visible="false">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="3%" />
                                                    <ItemTemplate>
                                                       <asp:LinkButton ID="btnsendtomembers" CommandArgument='<%# Bind("ID") %>' runat="server" class="btn btn-alt btn-sm btn-default" ToolTip="Send Mail to Members" CommandName="members" BackColor="#1BC5BD" style="height: 45px;width: 55px;"><i class="far fa-file-alt fs-2" style="padding-top: 15px;color: white;"></i></asp:LinkButton> 
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
            
             <asp:TemplateField HeaderText="Contact" SortExpression="Contact" Visible="false">
                                                    <HeaderStyle />
                                                   <ItemStyle Width="12%" />
                                                    <ItemTemplate>
                                                        

                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
           <%--  <asp:TemplateField HeaderText="Email Address" SortExpression="EmailAddress">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
             <asp:TemplateField HeaderText="Platform Support Cost" SortExpression="TransactionFee" ItemStyle-CssClass="col-right">
                 <ItemStyle Width="5%" />
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransactionFee" runat="server" Text='<%# Bind("TransactionFee","{0:F2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Status" SortExpression="Status" Visible="false">
                 <ItemStyle Width="5%" />
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("OrgarnizationStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Approval Status" SortExpression="ApprovalStatus">
                                                    <HeaderStyle />
                  <ItemStyle Width="170px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApprovalStatus" runat="server" Text='<%# Bind("OrgarnizationApproval") %>' Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="ddlApproval" runat="server">
                                                             <asp:ListItem Text="Pending Approval" ></asp:ListItem>
                                                            <asp:ListItem Text="Approved" ></asp:ListItem>
                                                           
                                                            <asp:ListItem Text="Awaiting Information" ></asp:ListItem>
                                                             <asp:ListItem Text="Denied" ></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            
            <asp:TemplateField >
                  <ItemStyle Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSave" runat="server" Text="Save" CommandName="save" CommandArgument='<%# Bind("ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
               <asp:TemplateField >
                  <ItemStyle Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnMID" runat="server" Text="MID" CommandName="mid" CommandArgument='<%# Bind("ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

               <asp:TemplateField >
                  <ItemStyle Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnPID" runat="server" Text="Platform Support Report" CommandName="platform" CommandArgument='<%# Bind("ID") %>' />
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


     <ajaxToolkit:ModalPopupExtender ID="mdlMid" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblShowMid" PopupControlID="pnlManagePassword" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="lblShowMid" runat="server"></asp:Label>
        <asp:Label ID="lblCloseMid" runat="server"></asp:Label>
       <asp:Panel ID="pnlManagePassword" runat="server" BackColor="White" Style="display:none;"
                       Width="700px" Height="400px" CssClass="card shadow-sm" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-title"><asp:Label ID="lblOptions" runat="server" Text="Payment settings"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
        <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       <asp:HiddenField ID="huid" runat="server" />
                       <asp:Label ID="lblMsgPop" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                       <asp:Label ID="lblErrorPop" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                       <asp:ValidationSummary ID="valSumm" runat="server" ValidationGroup="pay" />
                       </div>
            </div>

         
        <div class="form-group row" id="pnlVendor" runat="server">
                                  <div class="col-md-12 row">
                                       <label class="col-sm-2 control-label"><asp:Label ID="lblVendor" runat="server" Text="Stripe Account ID"></asp:Label> </label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtVendor" runat="server" MaxLength="250"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtVendor"
                        Display="None" ErrorMessage="Please enter Merchant ID" ValidationGroup="pay"></asp:RequiredFieldValidator>
                                          </div>
                                      </div>
    </div>
        <div class="form-group row" id="pnlUsername" runat="server" visible="false">
                                  <div class="col-md-12 row">
                                       <label class="col-sm-2 control-label">Secret key</label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtSecretKey" runat="server" MaxLength="250"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSecretKey"
                        Display="None" ErrorMessage="Please enter Merchant Key" ValidationGroup="pay"></asp:RequiredFieldValidator>
                                          </div>
                                      </div>
    </div>
 
        <div class="form-group row" id="pnlPassword" runat="server" visible="false">
                                  <div class="col-md-12 row">
                                       <label class="col-sm-2 control-label">Salt Passphrase</label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtSaltPassphrase" runat="server" MaxLength="250"></asp:TextBox>
                                           <%-- <asp:RequiredFieldValidator ID="rfPassword" runat="server" ControlToValidate="txtUsername"
                        Display="None" ErrorMessage="Please enter password" ValidationGroup="pay"></asp:RequiredFieldValidator>--%>
                                          </div>
                                      </div>
    </div>
         
         
           <div class="form-group row">
                   <div class="col-md-12 row">
                        <label class="col-sm-2 control-label"></label>
                       <div class="col-sm-10 form-inline">
                       <asp:Button ID="btnSubmitPop" runat="server" SkinID="btnDefault" Text="Save" OnClick="btnSubmitSettings_Click" ValidationGroup="pay" />
                       <asp:Button Visible="false" ID="btnCancelPop" runat="server" SkinID="btnCancel"  />
                           </div>
                       </div>
               </div>
        </div>
                   <%--  </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="lbtnClosePop" />
               </Triggers>
           </asp:UpdatePanel>--%>
           </asp:Panel>





     <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnUpload" PopupControlID="pnlAddReligion" CancelControlID="btnClose" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlAddReligion" runat="server" BackColor="White" Style="display:none;"
                       Width="450px" Height="190px"   ScrollBars="None">

		   <div class="card card-bordered">
    <div class="card-header">
        <h3 class="card-title"><asp:Label ID="lblModelHeading" runat="server" Text="Upload Organizations"></asp:Label> </h3>
        <div class="card-toolbar">
           <%-- <button type="button" class="btn btn-sm btn-light">
                Close
            </button>--%>
        </div>
    </div>
    <div class="card-body">
         <div class="row mb-6">
												<!--begin::Label-->
												<%--<label class="col-lg-4 col-form-label required fw-bold fs-6"></label>--%>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-10 fv-row fv-plugins-icon-container">
													 <div class="dropzone dropzone-queue mb-2" id="kt_dropzonejs_example_3">
                <!--begin::Controls-->
                <div class="dropzone-panel mb-lg-0 mb-2">
                    <a class="dropzone-select btn btn-sm btn-bg-light me-2">Drop files here or click to upload</a>
                    <a class="dropzone-remove-all btn btn-sm btn-light-primary">Remove All</a>
                </div>
                <!--end::Controls-->

                <!--begin::Items-->
                <div class="dropzone-items wm-200px">
                    <div class="dropzone-item" style="display:none">
                        <!--begin::File-->
                        <div class="dropzone-file">
                            <div class="dropzone-filename" title="some_image_file_name.jpg">
                                <span data-dz-name>some_image_file_name.jpg</span>
                                <strong>(<span data-dz-size>340kb</span>)</strong>
                            </div>

                            <div class="dropzone-error" data-dz-errormessage></div>
                        </div>
                        <!--end::File-->

                        <!--begin::Progress-->
                        <div class="dropzone-progress">
                            <div class="progress">
                                <div
                                    class="progress-bar bg-primary"
                                    role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0" data-dz-uploadprogress>
                                </div>
                            </div>
                        </div>
                        <!--end::Progress-->

                        <!--begin::Toolbar-->
                        <div class="dropzone-toolbar">
                            <span class="dropzone-delete" data-dz-remove><i class="bi bi-x fs-1"></i></span>
                        </div>
                        <!--end::Toolbar-->
                    </div>
                </div>
                <!--end::Items-->
           <%-- </div>
            <!--end::Dropzone-->

            <!--begin::Hint-->
            <span class="form-text text-muted">Max file size is 1MB and max number of files is 5.</span>
            <!--end::Hint-->
        </div>       --%>
                                                         
                                                         </div>
											

												</div>
												<div class="col-lg-4">
													<%--<asp:Button ID="Button1" runat="server" SkinID="btnDefault" OnClick="btnAddDenimination_Click" />--%>
													</div>
												<!--end::Col-->
											</div>
    </div>
    <div class="card-footer d-flex justify-content-end py-6 px-9">
       	<asp:Button ID="btnClose" runat="server" CssClass="btn btn-light" Text="Close" OnClick="btnClose_Click" />
				<%--<asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Changes" />--%>
    </div>
</div>
    <%-- <div>
         <asp:DropDownList ID="ddlOwner" runat="server"></asp:DropDownList>
     </div>--%>
           </asp:Panel>
    <script>
        // set the dropzone container id
        var id = "#kt_dropzonejs_example_3";

        // set the preview element template
        var previewNode = $(id + " .dropzone-item");

        previewNode.id = "";
        var previewTemplate = previewNode.parent(".dropzone-items").html();
        previewNode.remove();

        var myDropzone = new Dropzone(id, { // Make the whole body a dropzone
            url: "OrganizationHandler.ashx", // Set the url for your upload script location
            parallelUploads: 20,
            maxFilesize: 1, // Max filesize in MB
            previewTemplate: previewTemplate,
            previewsContainer: id + " .dropzone-items", // Define the container to display the previews
            clickable: id + " .dropzone-select" // Define the element that should be used as click trigger to select files.
        });


        myDropzone.on("addedfile", function (file) {
            // Hookup the start button
            $(document).find(id + " .dropzone-item").css("display", "");
        });

        // Update the total progress bar
        myDropzone.on("totaluploadprogress", function (progress) {
            $(id + " .progress-bar").css("width", progress + "%");
        });

        myDropzone.on("sending", function (file) {
            // Show the total progress bar when upload starts
            $(id + " .progress-bar").css("opacity", "1");
        });

        // Hide the total progress bar when nothing"s uploading anymore
        myDropzone.on("complete", function (progress) {
            var thisProgressBar = id + " .dz-complete";

            setTimeout(function () {
                $(thisProgressBar + " .progress-bar, " + thisProgressBar + " .progress").css("opacity", "0");
            }, 300)
        });
    </script>

   
</asp:Content>
