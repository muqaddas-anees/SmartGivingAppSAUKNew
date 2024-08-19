<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="SMSsettings.aspx.cs" Inherits="DeffinityAppDev.App.SMSsettings" %>

<%@ Register Src="~/App/controls/OrgMainTabs.ascx" TagPrefix="Pref" TagName="OrgMainTabs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:OrgMainTabs runat="server" ID="OrgMainTabs" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">SMS Settings</h3>
            </div>
            <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Upload Organizations">
            </div>
            <!--end::Card title-->
        </div>
        <!--begin::Card header-->
        <!--begin::Content-->
        <div id="kt_account_profile_details" class="collapse show" style="">
            <!--begin::Form-->
            <div id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
                <!--begin::Card body-->
                <div class="card-body border-top p-9">
                    <!--begin::Input group-->


                    <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-4 col-form-label required fw-bold fs-6">Client ID</label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtClientID" runat="server" MaxLength="500"></asp:TextBox>
                        </div>
                        <!--end::Col-->
                    </div>

                    <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-4 col-form-label required fw-bold fs-6">API Secret</label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtAPISecret" runat="server" MaxLength="500"></asp:TextBox>
                        </div>
                        <!--end::Col-->
                    </div>
                    <div class="row mb-6" style="display: none; visibility: hidden;">
                        <!--begin::Label-->
                        <label class="col-lg-4 col-form-label required fw-bold fs-6">SMS Buy Cost</label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtSMSBuy" runat="server" MaxLength="20" SkinID="Price_150px" Text="0.00"></asp:TextBox>
                        </div>
                        <!--end::Col-->
                    </div>
                    <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-4 col-form-label required fw-bold fs-6">SMS Sell Cost</label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtSMSSell" runat="server" MaxLength="20" SkinID="Price_150px" Text="0.00"></asp:TextBox>
                        </div>
                        <!--end::Col-->
                    </div>
                    <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-4 col-form-label fw-bold fs-6"></label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <asp:Button ID="btnSaveChanges" runat="server" SkinID="btnDefault" OnClick="btnSaveChanges_Click" Text="Save" />
                        </div>
                        <!--end::Col-->
                    </div>

                </div>

                <div class="card-footer d-flex justify-content-end py-6 px-9">
                </div>
            </div>
        </div>
    </div>

    <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer" role="button" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Packages</h3>
            </div>
            <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Add Bundle">
                <%--   <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-bg-light" Text="Upload Organizations"  style="margin-right:10px"   />  --%>

                <%-- <asp:Button ID="btnAddOrganization1" runat="server" CssClass="btn btn-primary" Text="Add New Organization"   />--%>

                <%--<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#kt_modal_1" title="Add Bundle">
    Add Bundle
</button>--%>

                <asp:Button ID="btnAddBundle" runat="server" SkinID="btnDefault" Text="Add Bundle" OnClick="btnAddBundle_Click" />


                <ajaxToolkit:ModalPopupExtender ID="mdl" runat="server" BackgroundCssClass="modalBackground"
                    TargetControlID="lblpnl" PopupControlID="pnl" CancelControlID="btlmdlClose">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Label ID="Label2" runat="server"></asp:Label>
                <asp:Label ID="lblpnl" runat="server"></asp:Label>
                <asp:Panel ID="pnl" runat="server" BackColor="White" Style="display: none;"
                    Width="500px" Height="550px" CssClass="card shadow-sm" ScrollBars="None">

                    <div class="card-header">
                        <h5 class="card-title">Add Bundle</h5>

                        <!--begin::Close-->
                        <div class="btn btn-icon btn-sm btn-active-light-primary ms-2" data-bs-dismiss="modal" aria-label="Close">
                            <asp:LinkButton ID="btlmdlClose" runat="server" SkinID="BtnLinkCloseNoCss"></asp:LinkButton>
                        </div>
                        <!--end::Close-->
                    </div>

                    <div class="card-body">
                        <div class="row mb-6">
                            <!--begin::Label-->
                            <label class="col-lg-4 col-form-label required fw-bold fs-6">Package Title</label>
                            <!--end::Label-->
                            <!--begin::Col-->
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtPackageTitle" runat="server" MaxLength="500"></asp:TextBox>
                                <asp:RequiredFieldValidator Style="font-size: small" ID="RequiredFieldValidator5" Display="Dynamic" runat="server"
                                    ForeColor="Red" ErrorMessage="Please enter Package Title"
                                    ControlToValidate="txtPackageTitle" ValidationGroup="group1"></asp:RequiredFieldValidator>
                            </div>
                            <!--end::Col-->
                        </div>
                        <div class="row mb-6">
                            <!--begin::Label-->
                            <label class="col-lg-4 col-form-label required fw-bold fs-6">Sub Text</label>
                            <!--end::Label-->
                            <!--begin::Col-->
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtSubText" runat="server" MaxLength="500"></asp:TextBox>

                            </div>
                            <!--end::Col-->
                        </div>
                        <div class="row mb-6">
                            <!--begin::Label-->
                            <label class="col-lg-4 col-form-label required fw-bold fs-6">Price</label>
                            <!--end::Label-->
                            <!--begin::Col-->
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtPrice" runat="server" MaxLength="20"></asp:TextBox>
                            </div>
                            <!--end::Col-->
                        </div>
                        <div class="row mb-6">
                            <!--begin::Label-->
                            <label class="col-lg-4 col-form-label required fw-bold fs-6">SMS Volume</label>
                            <!--end::Label-->
                            <!--begin::Col-->
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtSMSVolume" runat="server" MaxLength="20"></asp:TextBox>
                            </div>
                            <!--end::Col-->
                        </div>
                    </div>

                    <div class="card-footer">
                        <%-- <button type="button" class="btn btn-light" data-bs-dismiss="modal">Close</button>--%>
                        <%--   <button type="button" class="btn btn-primary" id="btnAddOrganization">Save changes</button>--%>

                        <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_onclick" ValidationGroup="group1" />

                    </div>

                </asp:Panel>



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
                        </div>

                        <div class="row">
                            <asp:HiddenField runat="server" ID="hid" Value="0" />
                            <asp:GridView ID="grid_display" runat="server" OnRowCommand="grid_display_RowCommand">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:LinkButton SkinID="BtnLinkEdit" ID="btnEdit" runat="server" CommandName="edit1" CommandArgument='<%# Bind("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Package Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPackageName" runat="server" Text='<%# Bind("PackageName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Text">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubText" runat="server" Text='<%# Bind("SubText") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Selling Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSellingPrice" runat="server" Text='<%# Bind("SellingPrice") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SMS Volume">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUsername" runat="server" Text='<%# Bind("SMSCount") %>'></asp:Label>
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


    <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer" role="button" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Purchase History</h3>
            </div>
            <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Add Bundle">
                <%--   <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-bg-light" Text="Upload Organizations"  style="margin-right:10px"   />  --%>

                <%-- <asp:Button ID="btnAddOrganization1" runat="server" CssClass="btn btn-primary" Text="Add New Organization"   />--%>

                <%--  <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#kt_modal_1" title="Add Bundle">
    Add Bundle
</button>--%>
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
                            <asp:Label ID="Label1" runat="server" EnableViewState="false"></asp:Label>
                        </div>

                        <div class="row">
                        </div>

                        <div class="row">
                            <asp:GridView ID="GridPackageHistory" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date & Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateTime" runat="server" Text='<%# Bind("DateTime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Organization">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrganization" runat="server" Text='<%# Bind("Organization") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bundle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPacakge" runat="server" Text='<%# Bind("Pacakge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SMS Volume">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVolume" runat="server" Text='<%# Bind("Volume") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sell Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSellPrice" runat="server" Text='<%# Bind("SellPrice") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PurchasedBy">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchasedBy" runat="server" Text='<%# Bind("PurchasedBy") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField ItemStyle-Width="5%">
                                                                       <ItemTemplate>
                                                                          <asp:LinkButton SkinID="BtnLinkDelete" ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete record?');" />
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>--%>
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
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
