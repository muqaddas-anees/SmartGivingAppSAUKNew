<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeFile="ProjectForumMaster.aspx.cs" Inherits="ProjectForumMaster" Title="Untitled Page" %>
<%@ Register Src="controls/ProjectTabs.ascx" TagName="BuildProjectTabs" TagPrefix="uc3" %>
<%@ Register Src="controls/ForumMaster.ascx" TagName="ForumMaster" TagPrefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc3:BuildProjectTabs ID="BuildProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.Forum%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline" ></asp:HyperLink>
</asp:Content><asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
<script type="text/javascript">
function addFileUploadBox()
{
    if (!document.getElementById || !document.createElement)
        return false;
		
    var uploadArea = document.getElementById ("upload-area");
	
    if (!uploadArea)
        return;

    var newLine = document.createElement ("br");
    uploadArea.appendChild (newLine);
	
    var newUploadBox = document.createElement ("input");
	
    // Set up the new input for file uploads
    newUploadBox.type = "file";
    newUploadBox.size = "60";
	
    // The new box needs a name and an ID
    if (!addFileUploadBox.lastAssignedId)
        addFileUploadBox.lastAssignedId = 100;
	    
    newUploadBox.setAttribute ("id", "dynamic" + addFileUploadBox.lastAssignedId);
    newUploadBox.setAttribute ("name", "dynamic:" + addFileUploadBox.lastAssignedId);
    uploadArea.appendChild (newUploadBox);
    addFileUploadBox.lastAssignedId++;
}
</script>

     <uc2:ForumMaster ID="ForumMaster1" runat="server" />
    <script src="../../Scripts/respond.min.js"></script>
    <script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <script src="../../Scripts/GridDesingFix.js"></script>
    <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();

        $(window).load(function () {
            $(".dropdown-menu li")
          .find("input[type='checkbox']")
          .prop('checked', 'checked').trigger('change');
            $(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});
        });
    </script>
</asp:Content>

