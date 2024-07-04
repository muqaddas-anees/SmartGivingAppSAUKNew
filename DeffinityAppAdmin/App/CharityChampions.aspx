<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="CharityChampions.aspx.cs" Inherits="DeffinityAppDev.App.CharityChampions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
	
    
    <script>
function copyUrlToClipboard(url) {
    // Create a temporary input element
    var tempInput = document.createElement("input");
    
    // Set its value to the URL
    tempInput.setAttribute("value", url);
    
    // Append the input element to the document body
    document.body.appendChild(tempInput);
    
    // Select the input element's contents
    tempInput.select();
    
    // Copy the selected text to the clipboard
    document.execCommand("copy");
    
    // Remove the temporary input element
    document.body.removeChild(tempInput);

    showswal("URL Copied successfully", "Ok");
    // Prevent postback
    return false;
}
    </script>
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
										<h3 class="fw-bolder m-0">Charity Champions</h3>
									</div>
                                     <div class="card-toolbar gap-3" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Add New Charity Champion">
                                     <%--   <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-bg-light" Text="Upload Organizations"  style="margin-right:10px"   />  --%>
                                         
                                        <%-- <asp:Button ID="btnAddOrganization1" runat="server" CssClass="btn btn-primary" Text="Add New Organization"   />--%>

                                         <%--<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#kt_modal_1" title="Add Bundle">
    Add Bundle
</button>--%>

										 <asp:Button ID="btnAddBundle" runat="server" SkinID="btnDefault" Text="Add New Charity Champions" OnClick="btnAddBundle_Click" />

                                          <asp:Button ID="btnExport" runat="server" SkinID="btnDefault" Text="Export Transactions" OnClick="btnExport_Click" />


										  <ajaxToolkit:ModalPopupExtender ID="mdl" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblpnl" PopupControlID="pnl" CancelControlID="btlmdlClose" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="Label2" runat="server"></asp:Label>
        <asp:Label ID="lblpnl" runat="server"></asp:Label>
       <asp:Panel ID="pnl" runat="server" BackColor="White" Style="display:none;"
                       Width="600px" Height="650px" CssClass="card shadow-sm" ScrollBars="None">
       
            <div class="card-header">
                <h5 class="card-title">Add Charity Champion</h5>

                <!--begin::Close-->
                <div class="btn btn-icon btn-sm btn-active-light-primary ms-2" data-bs-dismiss="modal" aria-label="Close">
                  <asp:LinkButton ID="btlmdlClose" runat="server" SkinID="BtnLinkCloseNoCss"></asp:LinkButton>
                </div>
                <!--end::Close-->
            </div>

            <div class="card-body">
              
                 	<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">First Name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtFirstName" runat="server" MaxLength="500"></asp:TextBox>
													  <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator5" Display="Dynamic" runat="server" 
														  ForeColor="Red" ErrorMessage="Please enter first name" 
														  ControlToValidate="txtFirstName" ValidationGroup="group1" ></asp:RequiredFieldValidator>
												</div>
												<!--end::Col-->
											</div>
                	<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Last Name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtLastName" runat="server" MaxLength="500"></asp:TextBox>
													 <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator1" Display="Dynamic" runat="server" 
														  ForeColor="Red" ErrorMessage="Please enter last name" 
														  ControlToValidate="txtLastName" ValidationGroup="group1" ></asp:RequiredFieldValidator>
												</div>
												<!--end::Col-->
											</div>
					<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Referral Code</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtReferralCode" runat="server" MaxLength="6"  ReadOnly="true"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
					<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">%Commission</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtCommission" runat="server" MaxLength="5" SkinID="Price_150px"></asp:TextBox>
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
                                                     <div class="row mb-6">
                                                         <div class="col-lg-12 d-flex d-inline gap-3">

                                                             <label class="form-label  mt-3"> Start Date</label>
                                                             <asp:TextBox ID="txtStartDate" runat="server" SkinID="DateNew"></asp:TextBox>

                                                               <label class="form-label mt-3"> End Date</label>
                                                             <asp:TextBox ID="txtEndDate" runat="server" SkinID="DateNew"></asp:TextBox>

                                                             <asp:Button ID="btnSerach" runat="server" SkinID="btnSearch" OnClick="btnSerach_Click" />
                                                         </div>

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
																   																 
																   																  
                                                                   <asp:TemplateField HeaderText="First Name">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Last Name" >
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
																   	      
																    <asp:TemplateField HeaderText="RefCode"  >
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblRefCode" runat="server" Text='<%# Bind("RefCode") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
															   <asp:TemplateField HeaderText="Registration URL" >
                                                    
                                                    <ItemTemplate>
                                                        <asp:Button ID="CopyButton" runat="server" Text="Copy URL" OnClientClick='<%# Eval("Url", "return copyUrlToClipboard(\"{0}\");") %>' />
            
                                                       <%-- <asp:Button ID="btnCopyUrl" runat="server" Text="Copy URL" CommandName="copy"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID").ToString() %>' />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
																    <asp:TemplateField HeaderText="%Commission"  ItemStyle-CssClass="text-right" HeaderStyle-CssClass="text-right">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblcommission" runat="server" Text='<%# Bind("commission") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
																     <asp:TemplateField HeaderText="No of Instances"  ItemStyle-CssClass="text-right" HeaderStyle-CssClass="text-right">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblNoofInstances" runat="server" Text='<%# Bind("NoofInstances") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Donations This Month"  ItemStyle-CssClass="text-right" HeaderStyle-CssClass="text-right">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblDonationsThisMonth" runat="server" Text='<%# Bind("DonationsThisMonth") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Donations Last Month"  ItemStyle-CssClass="text-right" HeaderStyle-CssClass="text-right">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblDonationsLastMonth" runat="server" Text='<%# Bind("DonationsLastMonth") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Donations This Year"  ItemStyle-CssClass="text-right" HeaderStyle-CssClass="text-right">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblDonationsThisYear" runat="server" Text='<%# Bind("DonationsThisYear") %>'></asp:Label>
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
