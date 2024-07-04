<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectExpenseTracker" Codebehind="ProjectExpenseTracker.aspx.cs" %>
<%@ Register Src="controls/ProjectTabs.ascx" TagName="BuildProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/Project_FinancialSubtab.ascx" TagName="Project_FinalcialSubtab"
    TagPrefix="uc2" %>
<%@ Register Src="controls/PTExpense.ascx" TagName="Expense" TagPrefix="uc3" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc1:BuildProjectTabs ID="BuildProjectTabs1" runat="server" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_title" runat="Server">
      Expense Tracker - <Pref:ProjectRef ID="ProjectRef1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      
    <uc2:Project_FinalcialSubtab ID="Project_FinalcialSubtab1" runat="server" />
    <uc3:Expense ID="Expense1" runat="server" />
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 
</asp:Content>

