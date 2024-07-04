<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_PortfolioDdlCtr" Codebehind="PortfolioDdlCtr.ascx.cs" %>
<span class="space_r50 float_l"><%= Resources.DeffinityRes.Customer%>: <b><%=sessionKeys.PortfolioName %></b>&nbsp;
    <asp:LinkButton ID="btnPop_open" runat="server" SkinID="BtnLinkDrag" ToolTip="<%$ Resources:DeffinityRes,ChangeCustomer%>" /> </span>


<ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" 
    TargetControlID="btnPop_open" PopupControlID="Panel_portfolio" 
    CancelControlID="btnClose" 
    BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>

<asp:Panel ID="Panel_portfolio" ClientIDMode="Static" runat="server" Width="70%">
  
    <div class="modal-dialog">
			<div class="modal-content">
    <div class="modal-header">
        <button ID="btnClose" runat="server" type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title"><%= Resources.DeffinityRes.Customer%>:</h4>
				</div>

<asp:UpdatePanel ID="UpdatePanle_PortfolioDDL" runat="server">
<ContentTemplate>
    <div class="modal-body">
<asp:DropDownList ID="ddlPortfolio" runat="server" 
        onselectedindexchanged="ddlPortfolio_SelectedIndexChanged" 
        AutoPostBack="True" SkinID="ddl_90"  >
    </asp:DropDownList>
        </div>
</ContentTemplate>
</asp:UpdatePanel>
                </div>
       
        </div>
       
</asp:Panel>
