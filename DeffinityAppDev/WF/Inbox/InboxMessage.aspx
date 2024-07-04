<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="InboxMessage" EnableEventValidation="false" Codebehind="InboxMessage.aspx.cs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
   Inbox 
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="panel_title" runat="Server">
   Inbox Messages
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="uProgress" runat="server">
        <ProgressTemplate>
            <asp:Label ID="imgloading" runat="server" SkinID="Loading" />
        </ProgressTemplate>
    </asp:UpdateProgress>
   <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                                                
<div class="form-group row">
      <div class="col-xs-4">
          <asp:Panel ID="pnlInbox" runat="server" Height="700px" BorderWidth="1pt"
                                    BorderStyle="Solid" BorderColor="LightGray" ScrollBars="Vertical">
                                    <div style="color: White; background: #b3b3b3; padding-left: 5px;">
                                        <asp:Label ID="lblInboxCount" runat="server"></asp:Label></div>
                                    <asp:GridView ID="gvInbox" runat="server" ShowHeader="false" Width="100%" ShowFooter="false"
                                        OnRowCommand="gvInbox_RowCommand" EmptyDataText="Inbox empty"
                                        OnRowDataBound="gvInbox_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("ID") %>' />
                                                    <asp:HiddenField ID="hfFrom" runat="server" Value='<%# Bind("FromAddress") %>' />
                                                    <asp:HiddenField ID="hfTo" runat="server" Value='<%# Bind("ToAddress") %>' />
                                                    <asp:HiddenField ID="hfIsViewed" runat="server" Value='<%# Bind("IsViewed") %>' />
                                                    <asp:HiddenField ID="hfPath" runat="server" Value='<%#Bind("Gid") %>' />
                                                    <asp:LinkButton ID="lnkbtnInbox" runat="server" CommandName="Inbox" CommandArgument='<%# Container.DataItemIndex %>'
                                                        ToolTip='<%# "Received: " +Eval("ReceivedDate") %>' Text='<%#Eval("Subject") %>'
                                                        Font-Size="7pt"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="imgDelete" ToolTip="<%$ Resources:DeffinityRes, Delete%>"
                                                        SkinID="BtnLinkDelete" runat="server" CommandName="Deleterow" CommandArgument='<%# Bind("Gid")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
	</div>
	<div class="col-xs-8">
         <asp:Panel ID="pnlHeader" runat="server" Visible="false">
                                    From:&nbsp
                                    <asp:Label ID="lblFrom" runat="server"></asp:Label><br />
                                    To:&nbsp;<asp:Label ID="lblTo" runat="server"></asp:Label><br />
                                    Subject:&nbsp<asp:Label ID="lblSubject" runat="server" Font-Bold="true"></asp:Label><br />
                                    Received:&nbsp<asp:Label ID="lblReceived" runat="server"></asp:Label>
                                    <div class="tab_subheader" style="border-bottom: dotted 1px Silver; width: 96%;">
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlMailMessage" runat="server" Height="700px" ScrollBars="Auto" BorderWidth="0pt"
                                    BorderStyle="Solid" BorderColor="LightGray" Visible="false">
                                    <asp:Label ID="lblMailMsg" runat="server" Width="700px" Height="700px"></asp:Label>
                                </asp:Panel>
          
	</div>
	
</div>
                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvInbox" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>


   
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
     //grid_responsive();
     grid_responsive_display();
     $(window).load(function () {
         $("button:contains('Display all')").click(function (e) {
             e.preventDefault();
             $(".dropdown-menu li")
       .find("input[type='checkbox']")
       .prop('checked', 'checked').trigger('change');
         });
     });
    </script> 
</asp:Content>

