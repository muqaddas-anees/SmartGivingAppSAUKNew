<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" 
    AutoEventWireup="true" Inherits="budget_expenses" EnableEventValidation="false" Codebehind="budget_expenses.aspx.cs" %>

<%@ Register Src="controls/ProjectTabs.ascx" TagName="ProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/Project_BudgetSubTab.ascx" TagName="BudgetTab" TagPrefix="uc4"%>
<%@ Register Src="controls/ProjectCost.ascx" TagName="ProjectCost" TagPrefix="uc2" %>
<%@ Register Src="controls/Budget_Expenses.ascx" TagName="Budget_Expenses" TagPrefix="uc3" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
       <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_title" runat="Server">
    <%= Resources.DeffinityRes.Expenses%>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc4:BudgetTab ID="BudgetTab1" runat="server" />
        <div>
                      <uc3:Budget_Expenses ID="Budget_Expenses" runat="server" />
          </div>
    
 
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();
        $(window).load(function () {
                     $("button:contains('Display all')").click(function (e) {
                e.preventDefault();
                $(".dropdown-menu li")
          .find("input[type='checkbox']")
          .prop('checked', 'checked').trigger('change');
            });
                 });
    </script> 
</asp:Content>


