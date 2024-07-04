<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="True" CodeBehind="ctest.aspx.cs" Inherits="DeffinityAppDev.WF.Projects.ctest" MaintainScrollPositionOnPostback="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    
<%: System.Web.Optimization.Styles.Render("~/bundles/chitchatcss") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/chitchat") %>
    <section class="profile-env">
				
				<div class="row">
                    <div class="col-sm-9">
                        <div id="DivChitchatPost"></div>
                    </div>
                    <div class="col-sm-9">
                        <section class="user-timeline-stories">
                                <div id="DivChitchat"></div>
                         </section>
                        </div>
  </div>
        </section>

</asp:Content>
