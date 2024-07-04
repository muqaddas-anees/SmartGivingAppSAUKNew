<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="MyTasksDocuments" Title="Untitled Page" Codebehind="MyTasksDocuments.aspx.cs" %>
<%@ Register Src="controls/MyProjectsTab.ascx" TagName="ProjectStatus" TagPrefix="uc1" %>
<%@ Register Src="MailControls/TaskNotes.ascx" TagName="TaskNotes" TagPrefix="uc2" %>
<%@ Register Src="~/WF/Projects/Controls/Documents.ascx" TagPrefix="uc2" TagName="Documents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.MyTasks%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Documents%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:ProjectStatus ID="ProjectStatus1" runat="server" />    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
   
    <uc2:Documents ID="Documents1" runat="server"></uc2:Documents>
    <script>
        $(document).ready(function() {
            $("#headerCheck").change(function() {
                if ($("#headerCheck:checked").val() == null) {
                    //$("#checkBoxes:checkbox").checked = false;
                    $(":input").checked = true;
                }
            });
        });
    </script>

</asp:Content>
