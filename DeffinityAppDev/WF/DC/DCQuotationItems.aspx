<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="DCQuotationItems.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCQuotationItems" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
<%@ Register src="~/WF/DC/controls/DCQuotationItemsCtrl.ascx" tagname="sd_services" tagprefix="uc1" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <uc2:FlsTab ID="flstab1" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     <label id="lblTitle" runat="server">
                  </label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <style>
    .btn.btn-white.btn-icon-standalone i {
    background-color: #FF6600;
    border-right-color: #e6e6e6;
}
</style>
   <button id="btnVideo" runat="server" class="btn btn-white btn-icon btn-icon-standalone btn-sm" style="display:none;visibility:hidden;">
									<i class="fa-video-camera" style="color:white;"></i>
									<span>Watch Video</span>
								</button>
    <a id ="link_return" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self" style="padding-bottom:15px" title="Return to Quotation"><i class="fa fa-arrow-left"></i> Return to Quotation</a>
    <ajaxToolkit:ModalPopupExtender ID="mdlVideo" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnVideo" PopupControlID="pnlVideo" CancelControlID="lblbtnClose" >
</ajaxToolkit:ModalPopupExtender>
    
       <asp:Panel ID="pnlVideo" runat="server" BackColor="White" Style="display:none;"
                       Width="680px" Height="480px" CssClass="panel panel-color panel-info" ScrollBars="None">
         

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="Label7" runat="server" Text="Quotation"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lblbtnClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
        <div class="form-group row mb-5">
                  

                       <iframe id="viframe" runat="server" height="340" width="600" style="border:none;" src="https://player.vimeo.com/video/515804411"></iframe>
                       
                       
            </div>
 
      
        
           
        </div>
                  
           </asp:Panel>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <uc1:sd_services ID="DcServices" runat="server" Type="FLS"/>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
   <%--  <script type="text/javascript">
         activeTab('Customer Estimates');
    </script>--%>
     <%-- <script>
          activeTab('Quotation');
    </script>--%>
</asp:Content>
