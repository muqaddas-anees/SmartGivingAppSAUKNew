<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="MyTasksView" Codebehind="MyTasksView.aspx.cs" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.MyTasks%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    My Tasks View
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:HyperLink runat="Server" NavigateUrl="~/WF/Resource/MyTasks.aspx">
<i class="fa fa-arrow-left"></i> Back to My Tasks</asp:HyperLink>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <asp:Panel ID="PanelCsv1" runat="server" Width="100%" Style="overflow: hidden">
                                <div style="width: 100%; margin-left: 5px">
                                    <iframe id="iframeMpp" height="800px" width="100%" src='<%=RetUrl1() %>' scrolling="auto"
                                        frameborder="0"></iframe>
                                </div>
                            </asp:Panel>
</asp:Content>


