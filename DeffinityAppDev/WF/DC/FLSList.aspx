<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" Inherits="FLSList1" Codebehind="FLSList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div style="float:right"><a href="FLSForm.aspx">Add New FLS Ticket</a></div>
<h3>FLS Requests</h3>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" 
        Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Ticket Number">
                <ItemTemplate>
                <a href='FLSForm.aspx?tid=<%# Eval("CallID")%>'><%# Eval("CallID") %></a>              
                   
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
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" Runat="Server">
</asp:Content>

