<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="ContactPaymentInfo.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.ContactPaymentInfo" %>

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
    <asp:Literal ID="lblContact" runat="server"></asp:Literal> - <asp:Literal ID="lblAddress" runat="server" Text="Payments"></asp:Literal>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="gridPaymentDetails" runat="server">
        <Columns>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                   <asp:Label ID="lblRequesterName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                   <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Address">
                <ItemTemplate>
                   <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Payment Date"  ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                   <asp:Label ID="lblRequesterName" runat="server" Text='<%# Bind("PayDate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                   <asp:Label ID="lblRequesterName" runat="server" Text='<%# Bind("PaidAmount","{0:F2}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Ref. No">
                <ItemTemplate>
                   <asp:Label ID="lblRequesterName" runat="server" Text='<%# Bind("PayPalRef") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
