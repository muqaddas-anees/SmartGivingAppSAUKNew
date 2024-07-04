<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PaymentTypeSettings.aspx.cs" Inherits="DeffinityAppDev.App.PaymentTypeSettings" %>
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
										<h3 class="fw-bolder m-0"> Payment Type</h3>
									</div>
                                     <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Add Payment Type">
                                    

										 <asp:Button ID="btnAddBundle" runat="server" SkinID="btnDefault" Text="Add Payment Type" OnClick="btnAddBundle_Click" />


										  <ajaxToolkit:ModalPopupExtender ID="mdl" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblpnl" PopupControlID="pnl" CancelControlID="btlmdlClose" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="Label2" runat="server"></asp:Label>
        <asp:Label ID="lblpnl" runat="server"></asp:Label>
       <asp:Panel ID="pnl" runat="server" BackColor="White" Style="display:none;"
                       Width="600px" Height="650px" CssClass="card shadow-sm" ScrollBars="None">
       
            <div class="card-header">
                <h5 class="card-title">Add Payment Type</h5>

                <!--begin::Close-->
                <div class="btn btn-icon btn-sm btn-active-light-primary ms-2" data-bs-dismiss="modal" aria-label="Close">
                  <asp:LinkButton ID="btlmdlClose" runat="server" SkinID="BtnLinkCloseNoCss"></asp:LinkButton>
                </div>
                <!--end::Close-->
            </div>

            <div class="card-body">
              
                 	<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Payment type Code</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtPaytypeCode" runat="server" MaxLength="500"></asp:TextBox>
													  <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator5" Display="Dynamic" runat="server" 
														  ForeColor="Red" ErrorMessage="Please enter Payment type Code" 
														  ControlToValidate="txtPaytypeCode" ValidationGroup="group1" ></asp:RequiredFieldValidator>
												</div>
												<!--end::Col-->
											</div>
                	<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Payment Method</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtPaymentMethod" runat="server" MaxLength="500"></asp:TextBox>
													 <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator1" Display="Dynamic" runat="server" 
														  ForeColor="Red" ErrorMessage="Please enter Package Title" 
														  ControlToValidate="txtPaymentMethod" ValidationGroup="group1" ></asp:RequiredFieldValidator>
												</div>
												<!--end::Col-->
											</div>
					<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Transaction (%)</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtTransactionFee" runat="server" MaxLength="5" SkinID="Price_150px"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
					<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Fixed Fee</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtFixedFee" runat="server" MaxLength="20" SkinID="Price_150px"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
					<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Active</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:CheckBox ID="chk_Active" runat="server" Checked="true" />
												</div>
												<!--end::Col-->
											</div>
					<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Upload Image</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													 <asp:FileUpload runat="server" id="imgFile" CssClass="form-control" Text="Upload" />
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
																   																   <asp:TemplateField ItemStyle-Width="3%">
                                                                       <ItemTemplate>
                                                                          <asp:LinkButton SkinID="BtnLinkUp" ID="btnUp" runat="server" CommandName="up" CommandArgument='<%# Bind("ID") %>' ToolTip="Move Up" />
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
																   																   <asp:TemplateField ItemStyle-Width="3%">
                                                                       <ItemTemplate>
                                                                          <asp:LinkButton SkinID="BtnLinkDown" ID="btndown" runat="server" CommandName="down" CommandArgument='<%# Bind("ID") %>'  ToolTip="Move Down" />
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Code">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblCode" runat="server" Text='<%# Bind("ShortCode") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Payment Method" >
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblPaymentMethod" runat="server" Text='<%# Bind("PaymentMethod") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
																   	       <asp:TemplateField ItemStyle-Width="100px" >
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgLogo" runat="server" Width="100px" ImageUrl='<%# Eval("ID","~/ImageHandler.ashx?s=paymenttype&id={0}") %>' />
                                                    </ItemTemplate>
                  <ItemStyle Width="8%" />
                                                </asp:TemplateField>
																    <asp:TemplateField HeaderText="Transaction (%)" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right" >
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblTransactionPercent" runat="server" Text='<%# Bind("TransactionPercent","{0:N2}") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
															
																    <asp:TemplateField HeaderText="Fixed Fee"  ItemStyle-CssClass="text-right" HeaderStyle-CssClass="text-right">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblFixedFee" runat="server" Text='<%# Bind("FixedFee","{0:N2}") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
																    <asp:TemplateField HeaderText="Active" >
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblActive" runat="server" Text='<%# Bind("IsActive") %>'></asp:Label>
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


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
