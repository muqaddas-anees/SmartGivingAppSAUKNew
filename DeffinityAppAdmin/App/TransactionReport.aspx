<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="TransactionReport.aspx.cs" Inherits="DeffinityAppDev.App.TransactionReport" Async="true"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

      


	<style>
		.text-right{
			text-align:right;
		}
	</style>

    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"> Transaction Report</h3>
									</div>
                                     <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Add Payment Type">
                                    

										 <asp:Button ID="btnAddBundle" runat="server" SkinID="btnDefault" Text="Export Report"  />


										  
     <asp:Label ID="Label2" runat="server"></asp:Label>
        <asp:Label ID="lblpnl" runat="server"></asp:Label>
       



                                         </div>
									<!--end::Card title-->
								</div>
								
								<div id="kt_account_profile_details" class="collapse show" style="">
									<!--begin::Form-->
									<form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
										<!--begin::Card body-->
										<div class="card-body border-top p-9">
										
											  <div class="row mb-6">
												    <div class="row mb-6">
														<div>
            <h2>Stripe Balance</h2>
            <asp:Label ID="lblAvailableBalance" runat="server" Text="Available Balance: "></asp:Label>
            <br />
            <asp:Label ID="lblPendingBalance" runat="server" Text="Pending Balance: "></asp:Label>
        </div>

														</div>
                                          <div class="row">
                                              <asp:Label ID="lblMsg" runat="server" EnableViewState="false"></asp:Label>
                                              </div>
                                                   <div class="row mb-6">
                                                         <div class="col-lg-12 d-flex d-inline gap-3">

                                                             <label class="form-label mt-3"> Start Date</label>
                                                             <asp:TextBox ID="txtStartDate" runat="server" SkinID="DateNew"></asp:TextBox>

                                                               <label class="form-label mt-3"> End Date</label>
                                                             <asp:TextBox ID="txtEndDate" runat="server" SkinID="DateNew"></asp:TextBox>
                                                            <label class="form-label mt-3"> Stripe Status</label>
                                                           <asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_150px">
                                                               <asp:ListItem Text="Please select..." Value="" ></asp:ListItem>
                                                                <asp:ListItem Text="Pending" Value="Pending" ></asp:ListItem>
                                                               <asp:ListItem Text="Processed" Value="Processed" ></asp:ListItem>
                                                           </asp:DropDownList>
                                                             <asp:Button ID="btnSerach" runat="server" SkinID="btnSearch" OnClick="btnSerach_Click" />
                                                         </div>

                                                         </div>
                                                   
                                                  
                                                  <div class="row mb-6">
                                                   <asp:Label ID="lblTransferDetails" runat="server" EnableViewState="false"></asp:Label>
                                                  </div>
                                                   <div class="row">
													   <asp:HiddenField runat="server" ID="hid" Value="0" />
                                                           <asp:GridView ID="grid_display" runat="server" OnRowCommand="grid_display_RowCommand">
                                                               <Columns>
																   <asp:TemplateField ItemStyle-Width="5%" Visible="false">
                                                                       <ItemTemplate>

                                                                          <asp:CheckBox ID="chk" runat="server"  />
                                                                          <asp:LinkButton SkinID="BtnLinkEdit" ID="btnEdit" runat="server" CommandName="edit1" CommandArgument='<%# Bind("ID") %>' />
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
																   																  
                                                                   <asp:TemplateField HeaderText="Date & Time">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblPaidDate" runat="server" Text='<%# Bind("PaidDate") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Charity Name">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblCharityName" runat="server" Text='<%# Bind("CharityName") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Paid Amount" ItemStyle-CssClass="text-right">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblPaidAmount" runat="server" Text='<%# Bind("PaidAmount","{0:N2}") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Company AccountID" >
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblCompanyAccountID" runat="server" Text='<%# Bind("CompanyAccountID") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
																   	      <asp:TemplateField HeaderText="To Transfer" ItemStyle-CssClass="text-right">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblComapanyAmount" runat="server" Text='<%# Bind("ComapanyAmount","{0:N2}") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                  
																    <asp:TemplateField HeaderText="Stripe Status">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblStripeStatus" runat="server" Text='<%# Bind("StripeStatus") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Transfered On">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblStripeDate" runat="server" Text='<%# Bind("StripeDate") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                       <ItemTemplate>
                                                                           <asp:Button ID="btnSave" runat="server" Text="Transfer Funds" CommandName="CheckAndTransfer" Visible='<%# GetVisible( Eval("StripeStatus").ToString()) %>' />
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                   <asp:TemplateField ItemStyle-Width="5%" Visible="false">
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
										

											
										</div>
										<!--end::Actions-->
									<div></div>
                                            
                                            </div>
                                            </form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>



</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
