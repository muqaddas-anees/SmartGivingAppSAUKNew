<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="DCChat.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCChat" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     Service Desk
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <uc2:FlsTab ID="flstab1" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     <label id="lblTitle" runat="server">Chat</label>  
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <a id ="link_return" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to Ticket Journal</a>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                   <ProgressTemplate>
                                       <asp:Label ID="lblLoadingImage11" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
                               </asp:UpdateProgress>
                               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                     <ContentTemplate>
                                               <div class="row">
                            <div class="col-md-12">
    <section class="profile-env">
				<div class="form-group row" style="padding-bottom:0px">
                     <div class="card shadow-sm" style="width:100%;padding-top:0px;padding-bottom:0px;margin-bottom:0px">
                         <%-- <div class="card-header">
                                 Chit chat
                          </div>--%>
                          <div class="panel-body" style="padding-bottom:0px">
                                      <div class="col-sm-12" style="padding-bottom:0px;padding-left:0px">
                                                   <div id="DivChitchatPost" style="width:78%;"></div>
                                      </div>
                              </div>
                         </div>

                    <div class="scrollable ps-container ps-active-y" data-max-height="650" style="max-height:650px;">
                            <div class="col-sm-12" style="padding-left:0px;">
                        <section class="user-timeline-stories" style="width:80%;">
                                <div id="DivChitchat"></div>
                         </section>
                        </div>
                    </div>

				</div>
        </section>
           <%--<Pref:ChitChatCtrlHomeNew runat="server" id="ChitChatCtrlHomeNew" />--%>
       </div>
                    </div>
                                     </ContentTemplate>
                                   <Triggers>
                                      
                                   </Triggers>
                               </asp:UpdatePanel>
          <%: System.Web.Optimization.Styles.Render("~/bundles/chitchatcss") %>
           <%: System.Web.Optimization.Scripts.Render("~/bundles/chitchat") %>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
