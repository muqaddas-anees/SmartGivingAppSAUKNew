<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="OrganizationAdmin.aspx.cs" Inherits="DeffinityAppDev.App.OrganizationAdmin1" %>
<%@ Register Src="~/App/controls/OrgMainTabs.ascx" TagPrefix="Pref" TagName="OrgTabs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="server">
     <Pref:OrgTabs runat="server" ID="OrgTabs" />
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
										<h3 class="fw-bolder m-0">Admin Users</h3>
									</div>
                                     <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Add Admin">
                                     <%--   <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-bg-light" Text="Upload Organizations"  style="margin-right:10px"   />  --%>
                                         
                                        <%-- <asp:Button ID="btnAddOrganization1" runat="server" CssClass="btn btn-primary" Text="Add New Organization"   />--%>

                                         <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#kt_modal_1" title="Add Admin">
    Add Admin
</button>

<div class="modal fade" tabindex="-1" id="kt_modal_1">

    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Add Admin</h5>

                <!--begin::Close-->
                <div class="btn btn-icon btn-sm btn-active-light-primary ms-2" data-bs-dismiss="modal" aria-label="Close">
                    <span class="svg-icon svg-icon-2x"></span>
                </div>
                <!--end::Close-->
            </div>

            <div class="modal-body">
               	<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtUsername1" runat="server" MaxLength="500"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
                 	<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Email</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtEmail1" runat="server" MaxLength="500"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
                	<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Password</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtPassword1" runat="server" MaxLength="500" TextMode="Password"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-light" data-bs-dismiss="modal">Close</button>
             <%--   <button type="button" class="btn btn-primary" id="btnAddOrganization">Save changes</button>--%>

                <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_onclick" />
                
            </div>
        </div>
    </div>
</div>

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
                                              <asp:Label ID="lblMsg" runat="server" EnableViewState="false"></asp:Label>
                                              </div>

                                                    <div class="row">
                                                           <%-- <div class="col-lg-5 fv-row fv-plugins-icon-container form-inline" style="display:inline;">
                                                               <input type="text" name="fname" class="form-control form-control-lg form-control-solid mb-3 mb-lg-0" placeholder="Search" runat="server" id="txtsearch" value="">
                                                                </div>--%>
                                                        <%-- <div class="col-lg-5 fv-row fv-plugins-icon-container form-inline" style="display:inline;">
                                                             

                                                             <asp:Button ID="btnSave" runat="server" CssClass="btn btn-bg-light" Text="Save" Visible="false"   />
                                                                </div>--%>
                                                       <%--  <div class="col-lg-2 fv-row fv-plugins-icon-container form-inline" style="display:inline;">
                                                              
                                                         

                                                        </div>--%>
												
											
												<!--end::Col-->
											</div>

                                                   <div class="row">
                                                           <asp:GridView ID="grid_display" runat="server" OnRowCommand="grid_display_RowCommand">
                                                               <Columns>
                                                                   <asp:TemplateField HeaderText="Name">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblUsername" runat="server" Text='<%# Bind("DisplayName") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Email" >
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Username") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                   <asp:TemplateField ItemStyle-Width="5%">
                                                                       <ItemTemplate>
                                                                          <asp:LinkButton SkinID="BtnLinkDelete" ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete record?');" />
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                               </Columns>
                                                           </asp:GridView>
                                                       </div>

											
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												
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

    <script type="text/javascript">
        $(document).ready(function () {

            //$("#btnAddOrganization").click(function () {
            //   // toastr.success("etetetetet", "testet");
            //});
            //btnAddOrganization
           
        });
    </script>

</asp:Content>
