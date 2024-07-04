<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_POTab" Codebehind="POTab.ascx.cs" %>
  <%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>
<div class="form-wizard">
    <ul class="tabs">
        <li class="ms-hover">
                        <asp:HyperLink ID="lbtnPoJournal" NavigateUrl="~/WF/Projects/POJournal.aspx" Target="_self" runat="server" Text="Customer PO">Customer PO</asp:HyperLink>
                            </li><li class="ms-hover">
                        <asp:HyperLink ID="lbtnPoPurchase" NavigateUrl="~/WF/Projects/POPurchase.aspx" Target="_self" runat="server" Text="Internal PO">Internal PO</asp:HyperLink>
                        </li></ul>
                </div>
