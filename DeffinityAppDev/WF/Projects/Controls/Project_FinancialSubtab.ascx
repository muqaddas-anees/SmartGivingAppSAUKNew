<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="controls_Project_FinancialSubtab" Codebehind="Project_FinancialSubtab.ascx.cs" %>

<div class="form-wizard">
    <ul class="tabs">
        <li class="ms-hover">
            <asp:HyperLink ID="lbtnGeneral" Target="_self" runat="server" Text="General">General</asp:HyperLink></li>
        <li class="ms-hover">
            <asp:HyperLink ID="lbtnLaborTracker" Target="_self" runat="server" Text="Labour Tracker">Labour Tracker</asp:HyperLink></li>
        <li  class="ms-hover">
            <asp:HyperLink ID="lbtnMaterialsTracker" Target="_self" runat="server" Text="Materials Tracker">Materials Tracker</asp:HyperLink></li>
        <li  class="ms-hover">
            <asp:HyperLink ID="lbtnMiscTracker" Target="_self" runat="server" Text="Misc Tracker">Misc Tracker</asp:HyperLink></li>
        <li  class="ms-hover">
            <asp:HyperLink ID="lbtnExpenseTracker" Target="_self" runat="server" Text="Expense Tracker">Expense Tracker</asp:HyperLink></li>
        <li  class="ms-hover">
            <asp:HyperLink ID="lbtnPMHours" Target="_self" runat="server" Text="PM Hours">PM Hours</asp:HyperLink></li>
        <li  class="ms-hover">
            <asp:HyperLink ID="lbtnStaffHours" Target="_self" runat="server" Text="Staff Hours">Staff Hours</asp:HyperLink></li>
         <li class="dropdown" ><a href="#" data-toggle="dropdown" class="dropdown-toggle" ><%= Resources.DeffinityRes.More%><b class="caret"></b></a>
		<ul class="dropdown-menu dropdown-menu-right">
            <li class="ms-hover">
                 <asp:HyperLink ID="lbtnVariation" Target="_self" runat="server" Text="Variations">Variations</asp:HyperLink>
            </li>
		 <li class="ms-hover">
            <asp:HyperLink ID="lbtnActuals" Target="_self" runat="server" Text="Actuals">Actuals</asp:HyperLink>
        </li>
        <li class="ms-hover">
            <asp:HyperLink ID="lbtnInvoicing" Target="_self" runat="server" Text="Invoicing">Invoicing</asp:HyperLink></li>
        <%--<li  class="ms-hover">
            <asp:HyperLink ID="lbtnPO" Target="_self" runat="server" Text="PO">PO</asp:HyperLink>

        </li>--%>
		</ul>
	</li>
    </ul>
</div>
<%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>