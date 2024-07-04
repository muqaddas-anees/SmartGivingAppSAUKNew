<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" CodeFile="ProjectResourceView.aspx.cs" Inherits="ProjectResourceView" %>

<%@ Register src="controls/ResourceView.ascx" tagname="ResourceView" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:ResourceView ID="ResourceView1" runat="server" />

    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 

</asp:Content>

