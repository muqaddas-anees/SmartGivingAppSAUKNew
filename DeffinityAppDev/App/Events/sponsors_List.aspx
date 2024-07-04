<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="sponsors_List.aspx.cs" Inherits="DeffinityAppDev.App.Sponsor.sponsors_List" %>


<%@ Register Src="~/App/Events/controls/EventTabs.ascx" TagPrefix="Pref" TagName="EventTabs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:EventTabs runat="server" id="EventTabs" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <style>
          .txtright
    {
        float:right;
        text-align:right;
    }
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
      <asp:HiddenField ID="huid" runat="server" ClientIDMode="Static" />
    <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>

    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button"  data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Details of Event Sponsors</h3>
									</div>
                                     <div class="card-toolbar"  data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Click to add a user">
                                          <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-bg-light" Text="Upload Sponser" OnClick="btnUpload_Click" style="margin-right:20px; display:none"   /> 
                                         <asp:Button ID="btnAddOrganization" runat="server" CssClass="btn btn-primary" Text="Add a Sponsor" OnClick="btnAddOrganization_Click"  />

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


											
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
                                                <asp:GridView ID="GridInstances" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridInstances_RowCommand"  OnPageIndexChanging="GridInstances_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="9px">
                                                            <ItemTemplate>
                                                                <a href='AddSponsor.aspx?unid=<%# Eval("EventUNID") %>&mid=<%# Eval("SponsorId") %>' class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
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

                                                        <asp:TemplateField ItemStyle-Width="9px">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblPortfolioID" runat="server" Width="40px" Text='<%# Bind("SponsorId") %>' Visible="false"></asp:Label>
                                                                <asp:CheckBox ID="chk" runat="server" OnClick="javascript:SelectSingleCheckbox(this.id)" Visible="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="10%">
														<ItemTemplate>
															 <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetImageUrl(Eval("SponsorId")) %>' Width="100px" Height="100px" />
														</ItemTemplate>
													</asp:TemplateField>
                                                        <%--<asp:TemplateField ItemStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetImageUrl(Eval("SponsorId").ToString()) %>' Width="50px" Height="50px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="Company  Name" SortExpression="Company  Name">
                                                            <HeaderStyle />
                                                            <ItemStyle Width="150px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="About Company" SortExpression="About Company">
                                                            <HeaderStyle />
                                                            <ItemStyle Width="150px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAboutCompany" runat="server" Text='<%# Bind("AboutCompany") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sponsorship Amount" SortExpression="Sponsorship Amount">
                                                            <HeaderStyle />
                                                            <ItemStyle Width="150px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmountSponsored" runat="server" Text='<%# Bind("AmountSponsored","{0:F2}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                                            <HeaderStyle />
                                                            <ItemStyle Width="200px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" SortExpression="">
                                                            <HeaderStyle />
                                                            <ItemStyle Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDel" runat="server" CommandName="del" SkinID="BtnLinkDelete" CommandArgument='<%# Bind("SponsorId") %>' OnClientClick="return confirm('Do you want to delete this record?');"></asp:LinkButton>
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
									<input type="hidden"><div></div></form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
             <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

  
</asp:Content>
