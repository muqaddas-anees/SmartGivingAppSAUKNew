<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Resource_MyProjects" Title="My Projects" Codebehind="MyProjects.aspx.cs" %>

<%@ Register Src="controls/MyProjectsTab.ascx" TagName="ProjectStatus" TagPrefix="uc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.MyTasks%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.MyProjects%> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <uc1:ProjectStatus id="ProjectStatus1" runat="server"> </uc1:ProjectStatus>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="form-group">
          <div class="col-md-12">
              <label runat="server" id="lblHead"></label>
	</div>
</div>
<asp:Panel ID="panel1" runat="server" Width="100%">
		
		<asp:GridView ID="GridView1" runat="server" EmptyDataText="No Records Found" AutoGenerateColumns="False" Width="100%">
                             
                <Columns>             
                
                <asp:HyperLinkField DataTextField="ProjectReference1" HeaderText="Project Reference" DataNavigateUrlFields="ProjectReference,AC2PID,ContractorID" DataNavigateUrlFormatString="~/UpdateProject.aspx?Project={0}&AC2PID={1}&ContractorID={2}" />                
                                  
                <asp:BoundField HeaderText="Project Title" DataField="ProjectTitle">
                    
                </asp:BoundField>  
                <asp:HyperLinkField DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Resource/MyTasksView.aspx?project={0}&status=live"
                    Text="&lt;img src='media/ico_gantts.png' border='0'/&gt;" Target="_self" HeaderText="Gantt">
            <ItemStyle HorizontalAlign="Center" Width="50px" />
        </asp:HyperLinkField>                       
                                       
                <asp:BoundField HeaderText="Owner" DataField="ContractorName">
                    
                </asp:BoundField>                         
                <asp:BoundField  HeaderText="Site" DataField="Site">                    
                </asp:BoundField> 
                 <asp:BoundField HeaderText="Cost Centre" DataField="CostCentre">
                    
                </asp:BoundField> 
             <%--   <asp:TemplateField>
                <ItemTemplate>
                <asp:HyperLink ID="h1" runat="server" NavigateUrl="TimeSheetResources.aspx">TimeSheetResources</asp:HyperLink>
                </ItemTemplate>
                </asp:TemplateField>   --%>                     
                </Columns>
         </asp:GridView>
            
		
		<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>" SelectCommand="DN_SelectMyProject1" ProviderName="System.Data.SqlClient" SelectCommandType="StoredProcedure">
                <SelectParameters>
                <asp:SessionParameter DefaultValue="0" SessionField="UID" Type="int32" Name="ContractorID" />
                <asp:Parameter Name="StatusID" Type="Int32" DefaultValue="2"/>                              
                </SelectParameters>                
                </asp:SqlDataSource>
		</asp:Panel>

    
<%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
</asp:Content>

