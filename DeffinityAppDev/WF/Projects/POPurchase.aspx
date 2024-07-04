<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="POPurchase" Codebehind="POPurchase.aspx.cs" %>

<%@ Register src="controls/POTab.ascx" TagName="POTab" TagPrefix="uc1" %>
<%@ Register Src="controls/FinanceModuleTab.ascx" TagName="FMTab" TagPrefix="uc2" %>
<%@ Register Src="controls/InternalPO.ascx" TagName="InternalPO" TagPrefix="uc3" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.POPurchases%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc2:FMTab ID="tab" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<uc1:POTab ID="poSubTAb" runat="server"  />
<uc3:InternalPO ID="InternalPO1" runat="server" />
    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="Server">
   <script type="text/javascript">
       activeTab('PO Database');
   </script>
</asp:Content>




