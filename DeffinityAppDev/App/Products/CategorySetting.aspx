<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="CategorySetting.aspx.cs" Inherits="DeffinityAppDev.App.Products.CategorySetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_title" runat="server">
    Product Category
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row mb-10 d-flex d-inline">

										<div class="col-lg-6">
                                            <div class="row "> 
                                                <div class="col-lg-12 d-flex justify-content-end">
                                                <asp:Button ID="btnAddCategory" runat="server" Text="Add Category" OnClick="btnAddCategory_Click" />

                                                    </div>
                                                </div>


												<asp:GridView ID="GridInstances" runat="server" Width="60%" AutoGenerateColumns="false" OnRowCommand="GridInstances_RowCommand">
        <Columns>
             
           
              <asp:TemplateField HeaderText="" >
                                                    <HeaderStyle />
                                                    <ItemStyle Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton SkinID="BtnLinkEdit" runat="server" ID="grid_edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID").ToString() %>'
                            CommandName="edit1" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Name" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                         <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="txtLable" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

             

             <asp:TemplateField HeaderText="" >
                                                    <HeaderStyle />
                                                    <ItemStyle Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton SkinID="BtnLinkDelete" runat="server" ID="grid_delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID").ToString() %>'
                            CommandName="del" OnClientClick="return confirm('Do you want to delete the record?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
               
    
        </Columns>
    </asp:GridView>
											</div>


										</div>
    

     <ajaxToolkit:ModalPopupExtender ID="mdlLanguage" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblLanguage" PopupControlID="pnlLanguage" CancelControlID="lblCLoseLang" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="Label2" runat="server"></asp:Label>
        <asp:Label ID="lblLanguage" runat="server"></asp:Label>
       <asp:Panel ID="pnlLanguage" runat="server" BackColor="White" Style="display:none;"
                       Width="500px" Height="350px" CssClass=" card shadow-sm" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblPopTitleLanguage" runat="server" Text="Add New Category"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lblCLoseLang" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
     

         
       
        <div class="form-group row mb-6">

			<asp:TextBox ID="txtCategory" runat="server" Text=""></asp:TextBox>
</div>
		<div class="form-group row mb-6">

			<div class="col-md-12">
				<asp:HiddenField ID="hid" runat="server" Value="usertype" />
				
				<asp:Button ID="btnSaveData" runat="server" OnClick="btnSaveData_Click" SkinID="btnSubmit"  />
				
			</div>
</div>
</div>


		   </asp:Panel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
