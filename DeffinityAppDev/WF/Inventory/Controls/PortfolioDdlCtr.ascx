<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_PortfolioDdlCtr_Inventory" Codebehind="PortfolioDdlCtr.ascx.cs" %>
<span class="space_r50 float_l">
       <%= Resources.DeffinityRes.Customer%>: <b><%=sessionKeys.PortfolioName %></b>&nbsp;
    <asp:LinkButton ID="btnPop_open" runat="server" SkinID="BtnLinkChange"  ToolTip="<%$ Resources:DeffinityRes,ChangeCustomer%>"></asp:LinkButton>
</span>
<ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" 
    TargetControlID="btnPop_open" PopupControlID="Panel_portfolio" 
    CancelControlID="btnClose" 
    BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="Panel_portfolio" runat="server"  class="selct_customer" style="display:none;">
<div class="panel panel-default">
    <div class="panel-heading">
     <%= Resources.DeffinityRes.Customer%>: 
    <div style="width:20px;float:right;">
        <asp:LinkButton ID="btnClose" runat="server" SkinID="BtnLinkCancel"></asp:LinkButton>
    </div>
    </div>
    <div class="panel-body">
        <asp:UpdatePanel ID="UpdatePanle_PortfolioDDL" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="ddlPortfolio" runat="server" onselectedindexchanged="ddlPortfolio_SelectedIndexChanged" SkinID="ddl_90"
                                AutoPostBack="True"></asp:DropDownList>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
</asp:Panel>
