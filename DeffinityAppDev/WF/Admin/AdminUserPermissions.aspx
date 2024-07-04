<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="AdminUserPermissions" Title="Untitled Page" Codebehind="AdminUserPermissions.aspx.cs" %>

<%@ Register src="controls/MangeUserTab.ascx" tagname="MangeUserTab" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Manage&nbsp;Users&nbsp; - Permissions
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:LinkButton ID="btngohome" runat="server" SkinID="BtnLinkButton" Text="Return to User Admin"
                                        OnClick="btngohome_Click" CausesValidation="false"><i class="fa fa-arrow-left"></i> Return to User Admin</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="form-group">
             <div class="col-md-12">
                  <%= Resources.DeffinityRes.UserAdminfor%>: <asp:Label ID="lblusername" runat='server' Font-Bold="true"></asp:Label>
                 </div>
         </div>
    <div class="form-group">
             <div class="col-md-12">
    <uc1:MangeUserTab ID="MangeUserTab1" runat="server" />
   </div>
        </div>
   <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.FullName%></label>
                                      <div class="col-sm-6"><asp:DropDownList ID="drContractors" runat="server" 
           SkinID="ddl_90" AutoPostBack="True" 
           onselectedindexchanged="drContractors_SelectedIndexChanged">
                                    </asp:DropDownList>
					</div>
				</div>
                </div>
        <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.PermissionLevel%></label>
                                      <div class="col-sm-6"><asp:DropDownList ID="drpermission" runat="server" SkinID="ddl_90" AutoPostBack="true"></asp:DropDownList>
					</div>
				</div>
                </div>
   
       
   <ajaxToolkit:TabContainer ID="ajaxTbUser" runat="server" ActiveTabIndex="0">
    <ajaxToolkit:TabPanel ID="TPControl" runat="server" HeaderText="Control Panel">
    <ContentTemplate>
    <div class="form-group">
             <div class="col-md-12">
                 <asp:TreeView ID="TreeView2" ExpandDepth="0" runat="server" ShowCheckBoxes="All">
    <HoverNodeStyle Font-Underline="false" />
    <SelectedNodeStyle Font-Underline="false" />
    </asp:TreeView> 
</div>
</div>
        <div class="form-group">
             <div class="col-md-12 form-inline">
                 <asp:Button ID="imgCntrlCancel" SkinID="btnCancel" runat="server" 
           onclick="imgCntrlCancel_Click" /> &nbsp;
     <asp:Button ID="imgCntrlApply"  SkinID="btnApply" runat="server" 
           onclick="imgCntrlApply_Click" /> 
</div>
</div>
    
    </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="TPProjects" runat="server" HeaderText="Projects">
     <ContentTemplate>
         <div class="form-group">
             <div class="col-md-12">
                 <asp:TreeView ID="TreeViewProjects" ExpandDepth="0" runat="server" ShowCheckBoxes="All">
    <HoverNodeStyle Font-Underline="false" />
    <SelectedNodeStyle Font-Underline="false" />
    </asp:TreeView>
</div>
</div>
         <div class="form-group">
             <div class="col-md-12 form-inline">
                 <asp:Button ID="imgbtnPrjCancel" SkinID="btnCancel" runat="server" 
           onclick="imgbtnPrjCancel_Click" /> &nbsp;
     <asp:Button ID="imgbtnPrjApply"  SkinID="btnApply" runat="server" 
           onclick="imgbtnPrjApply_Click" />
</div>
</div>
    
    </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="TPServDsk" runat="server" HeaderText="Service Desk">
     <ContentTemplate>
         <div class="form-group">
             <div class="col-md-12">
                 <asp:TreeView ID="TreeViewSD" ExpandDepth="0" runat="server" ShowCheckBoxes="All">
    <HoverNodeStyle Font-Underline="false" />
    <SelectedNodeStyle Font-Underline="false" />
    </asp:TreeView>
</div>
</div>
         <div class="form-group">
             <div class="col-md-12 form-inline">
                 <asp:Button ID="imgbtnSDCancel" SkinID="btnCancel" runat="server" 
           onclick="imgbtnSDCancel_Click" /> &nbsp;
     <asp:Button ID="imgbtnSDApply"  SkinID="btnApply" runat="server" 
           onclick="imgbtnSDApply_Click" />
</div>
</div>
  
    </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="TPCustPort" runat="server" HeaderText="Customer Portal">
     <ContentTemplate>
         <div class="form-group">
             <div class="col-md-12">
                 <asp:TreeView ID="TreeViewCP" ExpandDepth="0" runat="server" ShowCheckBoxes="All">
    <HoverNodeStyle Font-Underline="false" />
    <SelectedNodeStyle Font-Underline="false" />
    </asp:TreeView>
</div>
</div>
         <div class="form-group">
             <div class="col-md-12 form-inline">
                 <asp:Button ID="imgbtnCPCancel" SkinID="btnCancel" runat="server" 
           onclick="imgbtnCPCancel_Click" /> &nbsp;
     <asp:Button ID="imgbtnCPApply"  SkinID="btnApply" runat="server" 
           onclick="imgbtnCPApply_Click" /> 
</div>
</div>
   
    </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="TPDashboard" runat="server" HeaderText="Dashboard Viewer">
     <ContentTemplate>
         <div class="form-group">
             <div class="col-md-12">
                 <asp:TreeView ID="TreeViewDBV" ExpandDepth="0" runat="server" ShowCheckBoxes="All">
    <HoverNodeStyle Font-Underline="false" />
    <SelectedNodeStyle Font-Underline="false" />
    </asp:TreeView>
</div>
</div>
         <div class="form-group">
             <div class="col-md-12 form-inline">
                 <asp:Button ID="imgbtnDBVCancel" SkinID="btnCancel" runat="server" 
           onclick="imgbtnDBVCancel_Click" /> &nbsp;
     <asp:Button ID="imgbtnDBVApply"  SkinID="btnApply" runat="server" 
           onclick="imgbtnDBVApply_Click" />
</div>
</div>
   
    </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="TPOther" runat="server" HeaderText="Other Modules">
     <ContentTemplate>
         <div class="form-group">
             <div class="col-md-12">
                  <asp:TreeView ID="TreeViewOM" ExpandDepth="0" runat="server" ShowCheckBoxes="All">
    <HoverNodeStyle Font-Underline="false" />
    <SelectedNodeStyle Font-Underline="false" />
    </asp:TreeView>
</div>
</div>
         <div class="form-group">
             <div class="col-md-12 form-inline">
                 <asp:Button ID="imgbtnOMCancel" SkinID="btnCancel" runat="server" 
           onclick="imgbtnOMCancel_Click" /> &nbsp;
     <asp:Button ID="imgbtnOMApply"  SkinID="btnApply" runat="server" 
           onclick="imgbtnOMApply_Click" /> 
</div>
</div>
    
    </ContentTemplate>
    </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
  
  <script type="text/javascript">
      var TREEVIEW_ID = "TreeControl"; //the ID of the TreeView control
      //the constants used by GetNodeIndex()
      var LINK = 0;
      var CHECKBOX = 1;

      //this function is executed whenever user clicks on the node text
      function ToggleCheckBox(senderId) {
          var nodeIndex = GetNodeIndex(senderId, LINK);
          var checkBoxId = TREEVIEW_ID + "n" + nodeIndex + "CheckBox";
          var checkBox = document.getElementById(checkBoxId);
          checkBox.checked = !checkBox.checked;

          ToggleChildCheckBoxes(checkBox);
          ToggleParentCheckBox(checkBox);
      }

      //checkbox click event handler
      function checkBox_Click(eventElement) {
          ToggleChildCheckBoxes(eventElement.target);
          ToggleParentCheckBox(eventElement.target);
      }

      //returns the index of the clicked link or the checkbox
      function GetNodeIndex(elementId, elementType) {
          var nodeIndex;
          if (elementType == LINK) {
              nodeIndex = elementId.substring((TREEVIEW_ID + "t").length);
          }
          else if (elementType == CHECKBOX) {
              nodeIndex = elementId.substring((TREEVIEW_ID + "n").length, elementId.indexOf("CheckBox"));
          }
          return nodeIndex;
      }

      //checks or unchecks the nested checkboxes
      function ToggleChildCheckBoxes(checkBox) {
          var postfix = "n";
          var childContainerId = TREEVIEW_ID + postfix + GetNodeIndex(checkBox.id, CHECKBOX) + "Nodes";
          var childContainer = document.getElementById(childContainerId);
          if (childContainer) {
              var childCheckBoxes = childContainer.getElementsByTagName("input");
              for (var i = 0; i < childCheckBoxes.length; i++) {
                  childCheckBoxes[i].checked = checkBox.checked;
              }
          }
      }

      //unchecks the parent checkboxes if the current one is unchecked
      function ToggleParentCheckBox(checkBox) {
          if (checkBox.checked == false) {
              var parentContainer = GetParentNodeById(checkBox, TREEVIEW_ID);
              if (parentContainer) {
                  var parentCheckBoxId = parentContainer.id.substring(0, parentContainer.id.search("Nodes")) + "CheckBox";
                  if ($get(parentCheckBoxId) && $get(parentCheckBoxId).type == "checkbox") {
                      $get(parentCheckBoxId).checked = false;
                      ToggleParentCheckBox($get(parentCheckBoxId));
                  }
              }
          }
      }

      //returns the ID of the parent container if the current checkbox is unchecked
      function GetParentNodeById(element, id) {
          var parent = element.parentNode;
          if (parent == null) {
              return false;
          }
          if (parent.id.search(id) == -1) {
              return GetParentNodeById(parent, id);
          }
          else {
              return parent;
          }
      }
    </script>
    <script type="text/javascript">
        var links = document.getElementsByTagName("a");
        for (var i = 0; i < links.length; i++) {
            if (links[i].className == TREEVIEW_ID + "_0") {
                links[i].href = "javascript:ToggleCheckBox(\"" + links[i].id + "\");";
            }
        }

        var checkBoxes = document.getElementsByTagName("input");
        for (var i = 0; i < checkBoxes.length; i++) {
            if (checkBoxes[i].type == "checkbox") {
                $addHandler(checkBoxes[i], "click", checkBox_Click);
            }
        }
    </script>
</asp:Content>

