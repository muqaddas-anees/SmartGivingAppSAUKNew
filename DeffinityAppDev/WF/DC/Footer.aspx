<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_Footer" Codebehind="Footer.aspx.cs" %>

<%@ Register src="controls/EmailFooter.ascx" tagname="EmailFooter" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
<br />
<div class="sec_header"  style="width: 650px">Email Footer</div>
    <uc1:EmailFooter ID="EmailFooter1" runat="server" />

</asp:Content>

