<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_DCMoveTab" Codebehind="DCMoveTab.ascx.cs" %>
<div>
    <ul id="countrytabs" class="shadetabs">
        <li>
            <asp:HyperLink ID="lbtnSDAssetsMove" NavigateUrl="~/DC/DCMoveInformation.aspx"
                Target="_self" runat="server" Text="Move Information"></asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="lbtnSDMoveAsset" NavigateUrl="~/DC/DCAssets.aspx"
                Target="_self" runat="server" Text="Assets"></asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="lbtnSDMoveDealerVoice" NavigateUrl="~/DC/DCDealerVoice.aspx"
                Target="_self" runat="server" Text="Dealer Voice"></asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="lbtnSDMoveDashboard" NavigateUrl="~/DC/DCDashboard.aspx"
                Target="_self" runat="server" Text="Dashboard"></asp:HyperLink>
        </li>
    </ul>
</div>
<script language="javascript" type="text/javascript" src="/js/Dynamic_styles.js"></script>
