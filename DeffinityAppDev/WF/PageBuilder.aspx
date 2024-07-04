<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PageBuilder.aspx.cs" Inherits="DeffinityAppDev.WF.PageBuilder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Page Builder
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <iframe src="~/examples/WebForm1.aspx?dt=<%=DateTime.Now.Millisecond.ToString() %>" width="100%" height="1200px"  id="myIframe" runat="server"></iframe>


    <script>
    // Selecting the iframe element
    var iframe = document.getElementById("myIframe");
    
    // Adjusting the iframe height onload event
    iframe.onload = function(){
        iframe.style.height = (iframe.contentWindow.document.body.scrollHeight -200) + 'px';
    }
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
