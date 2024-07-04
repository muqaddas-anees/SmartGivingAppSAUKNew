<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrameNoPanel.Master" AutoEventWireup="true" 
        CodeBehind="ProjectChitchat.aspx.cs" Inherits="DeffinityAppDev.WF.Projects.ProjectChitchat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
      <%: System.Web.Optimization.Styles.Render("~/bundles/chitchatcss") %>
      <%: System.Web.Optimization.Scripts.Render("~/bundles/chitchat") %>
    <section class="profile-env">
				<div class="form-group" style="padding-bottom:0px">
                     <div class="panel panel-default" style="width:100%">
                          <div class="panel-heading">
                                 Chit chat
                          </div>
                          <div class="panel-body" style="padding-bottom:0px">
                                      <div class="col-sm-12" style="padding-bottom:0px;">
                                                   <div id="DivChitchatPost" style="width:74%;"></div>
                                      </div>
                              </div>
                         </div>

                    <div class="scrollable ps-container ps-active-y" data-max-height="650" style="max-height:650px;">
                            <div class="col-sm-12">
                        <section class="user-timeline-stories">
                                <div id="DivChitchat"></div>
                         </section>
                        </div>
                    </div>

				</div>
        </section>
</asp:Content>
