<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Project_InternalPODetails" Codebehind="Project_InternalPODetails.aspx.cs" %>

<%@ Register Src="~/WF/Projects/controls/ProjectTabs.ascx" TagName="BuildProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="~/WF/Projects/controls/Project_FinancialSubtab.ascx" TagName="Project_FinalcialSubtab"
    TagPrefix="uc2" %>
<%@ Register Src="~/WF/Projects/controls/InternalPODetails.ascx" TagName="InternalPODetails"
    TagPrefix="uc3" %>
    <%@ Register Src="~/WF/Projects/controls/ProjectFinancialPOtab.ascx" TagName="POTab" TagPrefix="uc4" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
     <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    <%= Resources.DeffinityRes.Financials%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:HyperLink runat="Server" NavigateUrl="~/WF/Projects/ProjectPipeline.aspx?Status=2">
<i class="fa fa-arrow-left"></i> Back to Project pipeline</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:BuildProjectTabs ID="BuildProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <uc2:Project_FinalcialSubtab ID="Project_FinalcialSubtab1" runat="server" />
    <uc4:POTab ID="POTab1" runat="server" />
                <uc3:InternalPODetails ID="InternalPODetails1" runat="server" />

    
</asp:Content>



