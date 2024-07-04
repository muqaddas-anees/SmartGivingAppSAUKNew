<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" Inherits="DC_DeliveryCustomerList" Codebehind="DeliveryCustomerList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
<h1>List of Delivery Tickets</h1>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" 
        Width="100%" AllowPaging="True" 
        onpageindexchanging="gvList_PageIndexChanging" PageSize="5">
        <Columns>
            <asp:TemplateField HeaderText="Ticket Number">
                <ItemTemplate>
                <b><a href='DeliveryCustomer.aspx?callid=<%# Eval("CallID")%>'>TN:<%# Eval("CallID") %></a></b>         
                   
                </ItemTemplate>
              
            </asp:TemplateField>
            <asp:BoundField DataField="Company" HeaderText="Company" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Site" HeaderText="Site" />
            <asp:BoundField DataField="RequestType" HeaderText="Request Type" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
        </Columns>
    </asp:GridView>
</asp:Content>

