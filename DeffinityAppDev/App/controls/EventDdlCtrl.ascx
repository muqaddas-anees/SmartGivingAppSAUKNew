<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventDdlCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.EventDdlCtrl" %>
<span class="space_r50 float_l">Event: &nbsp; <asp:Literal ID="lblEventname" runat="server"></asp:Literal>
    <asp:LinkButton ID="btnPop_open" runat="server" SkinID="BtnLinkDrag" ToolTip="Change The Event" /> </span>


<ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" 
    TargetControlID="btnPop_open" PopupControlID="Panel_portfolio" 
    CancelControlID="lbtnCloseOptions" 
    BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>

<asp:Panel ID="Panel_portfolio" ClientIDMode="Static" runat="server" Width="70%" CssClass="card shadow-sm">
   <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text="Event"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnCloseOptions" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">


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
</asp:Panel>