<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true"
     Inherits="App_AppPermissionManager" Codebehind="AppPermissionManager.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
     
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    Smart Apps
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="panel_title" runat="Server">
    Permissions
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:HyperLink runat="Server" ID="link_back">
<i class="fa fa-arrow-left"></i> Return To App</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
       <asp:UpdatePanel ID="update1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
       <div class="form-group">
              <asp:Label ID="lblMsg" runat="server" EnableViewState="false"></asp:Label>
                            <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="P1" />
       </div>
    <div class="form-group">
        <div class="col-md-4">
           <label class="col-sm-3 control-label">Users</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlusers" runat="server" SkinID="ddl_80" AutoPostBack="true"
                                                                 OnSelectedIndexChanged="ddlusers_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="req1" runat="server" InitialValue="0" ControlToValidate="ddlusers" ValidationGroup="P1"
                                 Display="None" ErrorMessage="Please select user"></asp:RequiredFieldValidator>
            </div>
	</div>
    	<div class="col-md-4">
           <label class="col-sm-4 control-label">Permission type</label>
           <div class="col-sm-8">
                  <asp:DropDownList ID="ddlPermissions" runat="server" SkinID="ddl_90"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" Display="None" ValidationGroup="P1"
                                 ControlToValidate="ddlPermissions" ErrorMessage="Please select permission"></asp:RequiredFieldValidator>
            </div>
	</div>
	    <div class="col-md-4">
           <div class="col-sm-9">
               <asp:Button ID="BtnSave" runat="server" Text="Submit" ValidationGroup="P1" OnClick="BtnSave_Click" />
            </div>
	</div>
    </div>
     <div class="form-group">
          <div class="col-md-6">
         <asp:GridView ID="GridUserPermission" runat="server" AutoGenerateColumns="false" Width="100%" 
                               OnRowCommand="GridUserPermission_RowCommand" OnRowDeleting="GridUserPermission_RowDeleting">
                               <Columns>
                                   <asp:TemplateField HeaderText="Name">
                                      <HeaderStyle CssClass="header_bg_l" />
                                       <ItemTemplate>
                                           <asp:Label ID="lblCname" runat="server" Text='<%#Bind("Cname")%>'></asp:Label> 
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Permission">
                                       <ItemTemplate>
                                           <asp:Label ID="lblPermission" runat="server" Text='<%#Bind("Permission")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                      <asp:TemplateField HeaderStyle-Width="8%">
                                      <ItemStyle HorizontalAlign="Center" />
                                       <ItemTemplate>
                                           <asp:LinkButton ID="imgDelete" runat="server" SkinID="BtnLinkDelete"
                                                OnClientClick="return confirm('Do you want to delete this record?');"
                                                CommandArgument='<%#Bind("Id")%>' CommandName="Delete"></asp:LinkButton>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                               </Columns>
                           </asp:GridView>
              </div>
     </div>
 </ContentTemplate>
 <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlusers" EventName="" />
                        <asp:PostBackTrigger ControlID="BtnSave" />
                        <asp:PostBackTrigger ControlID="GridUserPermission" />
                    </Triggers>
                </asp:UpdatePanel>
</asp:Content>

