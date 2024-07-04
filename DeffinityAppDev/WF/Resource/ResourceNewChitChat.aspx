<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ResourceNewChitChat" Codebehind="ResourceNewChitChat.aspx.cs" %>

<%@ Register src="controls/ChitChatCtrlNewResource.ascx" tagname="ChitChatCtrlResource" tagprefix="uc2" %>
<%@ Register Src="controls/MyProjectsTab.ascx" TagName="ProjectStatus" TagPrefix="uc1" %>


<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.MyTasks%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   <asp:UpdateProgress ID="uProgress" runat="server">
<ProgressTemplate>
    <asp:Label runat="server" SkinID="Loading"></asp:Label>
</ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.ChitChat%> 
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc1:ProjectStatus ID="ProjectStatus1" runat="server"></uc1:ProjectStatus>
     <link href="../../Content/assets/css/fonts/elusive/css/elusive.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            
<div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-1 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-5">
               <asp:DropDownList ID="ddlCustomer" runat="server" ClientIDMode="Static" SkinID="ddl_70"></asp:DropDownList>
            </div>
               <div class="col-sm-6">
                   </div>
	</div>
</div>
                             <section class="profile-env">
				<div class="form-group" style="padding-bottom:0px">
                     <div class="panel panel-default" style="width:74%;display:none;">
                          <div class="panel-heading">
                                 Chit chat
                          </div>
                          <div class="panel-body" style="padding-bottom:0px">
                                      <div class="col-sm-12" style="padding-bottom:0px;padding-left:0px">
                                                   <div id="DivChitchatPost"></div>
                                      </div>
                              </div>
                         </div>

                    <div class="scrollable ps-container ps-active-y" data-max-height="650" style="max-height:850px;">
                            <div class="col-sm-9" style="padding-left:0px;">
                        <section class="user-timeline-stories">
                                <div id="DivChitchat"></div>
                         </section>
                        </div>
                    </div>

				</div>
        </section>
       
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts_Section" runat="server">
          <%: System.Web.Optimization.Styles.Render("~/bundles/chitchatcss") %>
           <%: System.Web.Optimization.Scripts.Render("~/bundles/chitchat") %>
</asp:Content>


