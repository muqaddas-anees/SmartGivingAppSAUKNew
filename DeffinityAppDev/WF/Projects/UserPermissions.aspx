<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="UserPermissions" Codebehind="UserPermissions.aspx.cs" %>

<%@ Register Src="controls/UserRestrictions.ascx" TagName="UserRestrictions" TagPrefix="uc2" %>

<%@ Register src="controls/ProjectTabs.ascx" tagname="ProjectTabs" tagprefix="uc1" %>
<%@ Register Src="controls/UserProjectPermissions.ascx" TagName="UserProjectPermissions"
    TagPrefix="uc1" %>
    <%@ Register Src="controls/UserPermissionPro.ascx" TagName="UserProjectPermissions"
    TagPrefix="ucPP" %>

<%@ Register src="controls/UserProjectRestrictions.ascx" tagname="UserProjectRestrictions" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.CustomerPermissions%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
        <uc3:UserProjectRestrictions ID="UserProjectRestrictions1" runat="server" />
</div>
  <%--  <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong> <%= Resources.DeffinityRes.IncludeUsersandGroups%>  </strong>
              <hr class="no-top-margin" />
            </div>
            </div>
    
     <div class="form-group">
        <div class="col-md-12">
        <ucPP:UserProjectPermissions ID="UserPermissionsLevel" runat="server" />
            </div>
             </div>

     <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>   <%= Resources.DeffinityRes.ProjectProfiler%>  </strong>
             <hr class="no-top-margin" />
         </div> 
    </div>
      <div class="form-group">
           <div class="col-md-12">
              <uc1:UserProjectPermissions id="UserProjectPermissions1" runat="server">
        </uc1:UserProjectPermissions>
              </div>
          </div>--%>

   <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script>
   </asp:Content>
