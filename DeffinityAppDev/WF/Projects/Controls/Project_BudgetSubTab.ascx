<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_Project_BudgetSubTab" Codebehind="Project_BudgetSubTab.ascx.cs" %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>
<div class="form-wizard" style="padding-bottom:15px">
       <ul class="tabs">
       <li>
            <asp:HyperLink ID="lbtnGeneral" Target="_self" runat="server" Text="General">General </asp:HyperLink></li>
        <li>
            <asp:HyperLink ID="lbtnBillofMaterials" Target="_self" runat="server" Text="Labour Tracker">Bill of Materials </asp:HyperLink></li>
        <li>
            <asp:HyperLink ID="lbtnHours" Target="_self" runat="server" Text="Materials Tracker">Budgeted Hours</asp:HyperLink></li>
        <li>
            <asp:HyperLink ID="lbtnExpenses" Target="_self" runat="server" Text="Misc Tracker">Budgeted Expenses</asp:HyperLink></li>
    <%--    <li>
            <asp:HyperLink ID="lbtnExternalCosts" Visible="false" Target="_self" runat="server" Text="Expense Tracker">External Costs </asp:HyperLink></li>--%>
        <li id="li_lbtnPMBudgetDifference" runat="server">
            <asp:HyperLink ID="lbtnPMBudgetDifference" Target="_self" runat="server" Text="PM Hours">Project Savings </asp:HyperLink>
        </li>
            <li class="dropdown" ><a href="#" data-toggle="dropdown" class="dropdown-toggle" ><%= Resources.DeffinityRes.Advanced%><b class="caret"></b></a>
		<ul class="dropdown-menu dropdown-menu-right">
            <li class="ms-hover" style="background-color:white">
                 <asp:HyperLink ID="lbtnBudgetbyTask" Target="_self" runat="server" Text="Budget by Task">Budget by Task</asp:HyperLink>
            </li>
		 <li class="ms-hover" style="background-color:white">
            <asp:HyperLink ID="lbtnBenefitBudget" Target="_self" runat="server" Text="Project Benefit Budget">Project Benefit Budget</asp:HyperLink>
        </li>
       
		</ul>
	</li>
    </ul>
   
    </div>

<script type="text/javascript">
    var cu = $(location).attr('href').toLowerCase();
    var ck = 'projectbom.aspx';
    //debugger;
    //alert(cu.indexOf($.trim(ck)));
    if (cu.indexOf($.trim(ck)) == -1) {
        activeTab('<%= Resources.DeffinityRes.Budget%>');
    }
    
</script>