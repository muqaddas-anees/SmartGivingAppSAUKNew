<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectTaskJournal" Codebehind="ProjectTaskJournal.aspx.cs" %>
<%@ Register src="controls/ProjectTabs.ascx" tagname="ProjectTabs" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      Project Task Journal - <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:HyperLink ID="hlinkGantt" runat="server" Font-Bold="true"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

     <asp:panel ID="panelSearch" runat="server" CssClass="row">
         <div class="form-group">
              <div class="col-md-8">
                  <div class="form-inline">
                      <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Task%></label>
                      <asp:DropDownList ID="ddlTask" runat="server" SkinID="ddl_50"></asp:DropDownList> 
        <asp:Button ID="btnView" runat="server" SkinID="btnDefault" Text="View" onclick="btnView_Click" />
                  </div>
                  </div>
         </div>
           
      </asp:panel>
      <asp:Panel ID="panelGrid" runat="server" Width="100%">
      <asp:GridView ID="GridTaskJournal" runat="server" Width="100%" >
      <Columns>
        <asp:BoundField DataField="Task" HeaderText="Task"  />
        <asp:BoundField DataField="Osdate" HeaderText="Original Start Date" HtmlEncode="false" DataFormatString="{0:d}" />
        <asp:BoundField DataField="sdate" HeaderText="New Start Date" HtmlEncode="false" DataFormatString="{0:d}" />
        <asp:BoundField DataField="Oedate" HeaderText="Original End Date" HtmlEncode="false" DataFormatString="{0:d}" />
        <asp:BoundField DataField="edate" HeaderText="New End Date" HtmlEncode="false" DataFormatString="{0:d}" />
        <asp:BoundField DataField="Oasdate" HeaderText="Original Actual Start Date" HtmlEncode="false" DataFormatString="{0:d}" />
        <asp:BoundField DataField="asdate" HeaderText="New Actual Start Date" HtmlEncode="false" DataFormatString="{0:d}" />
        <asp:BoundField DataField="Oaedate" HeaderText="Original Actual End Date" HtmlEncode="false" DataFormatString="{0:d}" />
        <asp:BoundField DataField="aedate" HeaderText="New Actual End Date" HtmlEncode="false" DataFormatString="{0:d}" />
        <asp:BoundField DataField="Ostatus" HeaderText="Original Status" />
        <asp:BoundField DataField="status" HeaderText="New Status" />
        <asp:BoundField DataField="Opercentcomplete" HeaderText="Original % Completed" Visible="false" />
        <asp:BoundField DataField="percentcomplete" HeaderText="New % Completed" Visible="false" />
        <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By" />
        <asp:BoundField DataField="ModifiedDate" HeaderText="Modified Date"  HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}"  />
        
      </Columns>
      </asp:GridView>
      </asp:Panel>
      
   <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    activeTab('Project Plan');
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
</asp:Content>


