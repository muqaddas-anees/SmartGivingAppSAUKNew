<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="ContactCommunication.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.ContactCommunication" %>

<%@ Register Src="~/WF/CustomerAdmin/Controls/ContactTabCtrl.ascx" TagPrefix="Pref" TagName="ContactTabCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Contact Details
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:ContactTabCtrl runat="server" ID="ContactTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     <asp:Literal ID="lblContact" runat="server"></asp:Literal> - <asp:Literal runat="server" Text="Communication"></asp:Literal>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-group row">
         <div class="col-md-12">
             <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
             </div>
         </div>
     <div class="form-group row">
         <div class="col-md-12">

             <asp:GridView ID="GridMail" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnRowCommand="GridMail_RowCommand" >
        <Columns>
             
                                                    
              <asp:TemplateField HeaderText="Date and Time Email Sent" ItemStyle-Width="15%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# DateDisplay( Eval("DateandTimeEmailSent") as DateTime?)%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Subject" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("MailSubject") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Equipment" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEquipment" runat="server" Text='<%# Bind("Equipment") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField ItemStyle-Width="10%"  >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                       <asp:Button ID="btnEmailClient" runat="server" SkinID="btnDefault" Text="Resend Message" CommandArgument='<%# Bind("ID") %>' CommandName="emailclient" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
           
        </Columns>
    </asp:GridView>


             </div>
         </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
