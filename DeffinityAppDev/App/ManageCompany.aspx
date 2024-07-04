<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="ManageCompany.aspx.cs" Inherits="DeffinityAppDev.App.ManageCompany" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
	Settings
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
	Manage Company
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
	<asp:Button ID="btnAddCompnay" runat="server"  Text="Add" OnClick="btnAddCompnay_Click" ToolTip="Add Company" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

	<div class="row">
		<asp:GridView ID="GridInstances" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="false" OnRowCommand="GridInstances_RowCommand">
        <Columns>
              <asp:TemplateField ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                       <asp:LinkButton ID="btnEdit1" runat="server" SkinID="BtnLinkEdit" CommandName="edit1" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
           
           
              <asp:TemplateField HeaderText="Name" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
               <asp:TemplateField HeaderText="Email" SortExpression="Email">
                                                    <HeaderStyle />
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Phone" SortExpression="Contactno">
                                                    <HeaderStyle />
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContactno" runat="server" Text='<%# Bind("Contactno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                                    <HeaderStyle />
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes" SortExpression="Notes">
                                                    <HeaderStyle />
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
           
               <asp:TemplateField HeaderText="" SortExpression=""  ItemStyle-Width="40px">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                       <asp:LinkButton ID="btnDel" runat="server" SkinID="BtnLinkDelete" CommandName="del" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete this record?');"></asp:LinkButton>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
    
        </Columns>
    </asp:GridView>

	</div>
    
<ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" 
    TargetControlID="btnPop_open" PopupControlID="Panel_portfolio" 
    CancelControlID="lbtnCloseOptions" 
    BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
<asp:Label ID="btnPop_open" runat="server"></asp:Label>
<asp:Panel ID="Panel_portfolio" ClientIDMode="Static" runat="server" Width="50%" CssClass="card shadow-sm">
   <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text="Add Company"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnCloseOptions" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">


  <%-- <asp:UpdatePanel ID="UpdatePanle_PortfolioDDL" runat="server">
<ContentTemplate>--%>
    <div class="modal-body">
<div class="row mb-6">
	<label class="col-lg-3 col-form-label required fw-bold fs-6">Company </label>
	<div class="col-lg-9"><asp:TextBox ID="txtAddCompany" runat="server" SkinID="txt_90"></asp:TextBox>
        <asp:HiddenField ID="hid" runat="server" Value="0" />
	</div>
	
</div>
		
		<div class="row mb-6">
	<label class="col-lg-3 col-form-label fw-bold fs-6">Email </label>
	<div class="col-lg-9"><asp:TextBox ID="txtCompanyEmail" runat="server" SkinID="txt_90"></asp:TextBox></div>
	
</div>
		<div class="row mb-6">
	<label class="col-lg-3 col-form-label fw-bold fs-6">Phone number </label>
	<div class="col-lg-9"><asp:TextBox ID="txtCompanyPhone" runat="server" SkinID="txt_90" ></asp:TextBox></div>
	
</div>
		<div class="row mb-6">
	<label class="col-lg-3 col-form-label fw-bold fs-6">Address </label>
	<div class="col-lg-9"><asp:TextBox ID="txtCompanyAddress" runat="server" SkinID="txtMulti_80" TextMode="MultiLine"></asp:TextBox></div>
	
</div>
		<div class="row mb-6">
	<label class="col-lg-3 col-form-label fw-bold fs-6">Notes </label>
	<div class="col-lg-9"><asp:TextBox ID="txtCompanyNotes" runat="server" SkinID="txtMulti_80" TextMode="MultiLine"></asp:TextBox></div>
	
</div>
		<div class="row mb-6">
		<label class="col-lg-3 col-form-label fw-bold fs-6"> </label>
	<div class="col-lg-9">
			<asp:Button ID="btnSubmitCompany" runat="server" SkinID="btnSubmit" OnClick="btnSubmitCompany_Click"  />
		</div>
			</div>
        </div>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>
       </div>
</asp:Panel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
