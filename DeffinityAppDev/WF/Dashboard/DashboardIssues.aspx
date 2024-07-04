<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DashboardIssues" Title="Untitled Page" Codebehind="DashboardIssues.aspx.cs" %>


<%@ Register src="controls/DashboardTabs.ascx" tagname="DashboardTabs" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Dashboard%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    <%= Resources.DeffinityRes.LstOfPrjIsues%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:DashboardTabs ID="DashboardTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-8">
               <asp:DropDownList id="ddlPortfolio" runat="server" Width="200px" 
            ValidationGroup="Submit"  DataTextField="PortFolio" DataValueField="ID"
             DataSourceID="SqlDataSource3"
            AutoPostBack="true" onselectedindexchanged="ddlPortfolio_SelectedIndexChanged">
                          </asp:DropDownList> 
                           <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_PermissionCustomer" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Programme%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlProjGroups" runat="server"  DataSourceID="SqlDataSource1"
                        Width="200px"  AutoPostBack="true"  DataTextField="OperationsOwners" DataValueField="ID"
                        OnSelectedIndexChanged="ddlProjGroups_SelectedIndexChanged">
                    </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_AssignedProgramme" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="DEFFINITY_DASHDROP1" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="SID" SessionField="SID" Type="Int32" />
                            <asp:SessionParameter DefaultValue="0" Name="UID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.SubProgramme%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlsubprogram" runat="server" Width="225px" DataSourceID="SqlDataSourcesubprogram"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlsubprogram_SelectedIndexChanged"
                        DataValueField="ID" DataTextField="OPERATIONSOWNERS">
                        <%--<asp:ListItem Text=" Please select..." Value="0" Selected="True"></asp:ListItem> --%>
                    </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSourcesubprogram" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_AssignedSubProgramme" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlProjGroups" DefaultValue="0" 
                                        Name="PROGRAMMEID" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                        </asp:SqlDataSource>
            </div>
	</div>
</div>

    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.SelectProject%></label>
           <div class="col-sm-8">
                <asp:DropDownList runat="server" ID="ddlProjects" DataSourceID="SqlDataSource9" DataTextField="ProjectTitle"
                        DataValueField="ProjectReference" AutoPostBack="True" OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged"
                        Width="250px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="DEFFINITY_CONTRACTORPROJECTS11" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="ContractorID" SessionField="UID" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlProjGroups" DefaultValue="0" Name="Programmer"
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:SessionParameter DefaultValue="0" Name="SID" SessionField="SID" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlsubprogram" DefaultValue="0" Name="SubProgID"
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Country%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                        Width="220px">
                    </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.IssueType%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddtype"
                            runat="server" DataSourceID="objDS_IssueType" AutoPostBack="True" Width="190px" DataTextField="IssueTypeName"
                            DataValueField="ID">
                        </asp:DropDownList>
                    <asp:ObjectDataSource ID="objDS_IssueType" runat="server" TypeName="Deffinity.Bindings.DefaultDatabind"
                        SelectMethod="b_IssueTypeWithALL"></asp:ObjectDataSource>
            </div>
	</div>
</div>
      <asp:Panel ID="Panel1" runat="server"  Width="100%" ScrollBars="Horizontal"  >
<asp:GridView ID="Grid_Issues" runat="server"  Width="100%" DataKeyNames="ID" 
        AutoGenerateColumns="False" DataSourceID="objDS_SelectIssues"  EmptyDataText="No Records Found"
        onrowupdated="Grid_Issues_RowUpdated" onrowupdating="Grid_Issues_RowUpdating" EnableViewState="false" >
      <Columns>                 
                <asp:BoundField Visible="False" DataField="ID" />   
               <asp:TemplateField >  
                <ItemStyle  Width="55px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" Enabled='<%# CommandField() %>' CausesValidation="false" CommandName="Edit" CommandArgument="<%# Bind('ID')%>" SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes, Edit%>" ></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="<%$ Resources:DeffinityRes, Update%>"  CommandArgument="<%# Bind('ID')%>" ValidationGroup="Group2" SkinID="BtnLinkUpdate" ToolTip="<%$ Resources:DeffinityRes, Update%>"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel" SkinID="BtnLinkCancel"  ToolTip="<%$ Resources:DeffinityRes, Cancel%>" ></asp:LinkButton>
                    
                </EditItemTemplate>              
            </asp:TemplateField>
            
            <asp:HyperLinkField DataTextField="ProjectReferenceWithPrefix" HeaderStyle-Width="90" ItemStyle-Width="30"  DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Projects/ProjectIssues.aspx?project={0}" HeaderText="<%$ Resources:DeffinityRes,ProjectRef%>" />            
             
               
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, ProjectTitle%>">
                 <ItemStyle  Width="125px" />
                <ItemTemplate>
                        <asp:Label ID="lblProjectTitle" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Issue%>">
                <ItemStyle  Width="175px" />
                <ItemTemplate>
                        <asp:Label ID="lblIssue" runat="server" Text='<%# Bind("Issue") %>'></asp:Label>
                    </ItemTemplate>
                
                   
                    <HeaderStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, DateRaised%>"> 
                                <ItemTemplate>
                                  <%# DataBinder.Eval(Container.DataItem, "ScheduledDate", "{0:d}")%>
                                </ItemTemplate> 
                                
                         <HeaderStyle HorizontalAlign="Center"  />
                               </asp:TemplateField> 
                
                <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes, AssignedTo%>" SortExpression="AssignedTo">
                    <ItemTemplate>
                   <asp:Label ID="lblAssignedTo" runat="server" Text='<%# Bind("AssignToName") %>'></asp:Label>                    
                    </ItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, IssueType%>"  SortExpression="IssuesList">
                <ItemStyle Width="8%" />
                <ItemTemplate>
                    <asp:Label ID="lblIssue1" runat="server" Text='<%# Bind("IssuseTypeName") %>'></asp:Label>
                </ItemTemplate>         
                </asp:TemplateField>    
                <asp:TemplateField  Visible="false" HeaderText="<%$ Resources:DeffinityRes, Status%>" SortExpression="StatusID">
                <ItemTemplate>
                        <asp:Label ID="lblstat" runat="server" Text='<%# Bind("ProjectStatus") %>' ></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlGdStatus"  Width="90px"    runat="server" DataSourceID="objDS_IssueStatus" DataTextField="Status" DataValueField="ID" SelectedValue='<%# Bind("Status") %>' ToolTip='<%# Bind("Status") %>' >
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="objDS_IssueStatus" runat="server" TypeName="Deffinity.Bindings.DefaultDatabind" SelectMethod="b_Issue_ItemStatus_withSelect">
</asp:ObjectDataSource> 
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                  </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Notes%>">
                  
                  <ItemTemplate>
                   <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>' ></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                 <asp:TextBox ID="txtNotes"  runat="server" width="150px" Text='<%# Bind("Notes") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemStyle Width="150px" />
                  </asp:TemplateField>
            </Columns>
                  
          </asp:GridView>

</asp:Panel>
    <asp:ObjectDataSource ID="objDS_SelectIssues" runat="server" TypeName="Deffinity.IssuesManager.IssuesManager" SelectMethod="Dashboard_DisplayIssues"
    UpdateMethod="Dashboard_UpdateIssues">
    <SelectParameters>
    <asp:ControlParameter ControlID="ddtype" DefaultValue="0" PropertyName="SelectedValue" Name="IssueType"/>
    <asp:SessionParameter SessionField="ProgrammeID" DefaultValue="0" Type="int32" Name="Program" />    
    <asp:ControlParameter ControlID="ddlProjects" DefaultValue="0" PropertyName="SelectedValue" Name="ProjectRef"/>    
    <asp:ControlParameter ControlID="ddlCountry" DefaultValue="0" PropertyName="SelectedValue" Name="CountryID"/>        
    <asp:ControlParameter ControlID="ddlsubprogram" DefaultValue="0" PropertyName="SelectedValue" Name="SubProgram"/> 
    <asp:ControlParameter ControlID="ddlPortfolio"  DefaultValue="0" PropertyName="SelectedValue" Name="portfolio" />      
    <asp:SessionParameter SessionField="UID" Type="Int32" Name="UserID" DefaultValue="0" />
    </SelectParameters>
    <UpdateParameters>
    <asp:Parameter Name="ID" Type="Int32" />
    <asp:Parameter Name="Status" Type="Int32" />
    <asp:Parameter Name="Notes" Type="String" ConvertEmptyStringToNull="true" />
    </UpdateParameters>
    </asp:ObjectDataSource>   
  <asp:SqlDataSource ID="SqlDataSourceCountry" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                            SelectCommand="GetCountryByProgrameOrSubProgramme" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                            <asp:ControlParameter ControlID="ddlProjGroups" DefaultValue="0" Name="Program"
                                PropertyName="SelectedValue" Type="Int32" />                        
                            <asp:ControlParameter ControlID="ddlsubprogram" DefaultValue="0" Name="SubProgram"
                                PropertyName="SelectedValue" Type="Int32" />                              
                        </SelectParameters>                    
		   </asp:SqlDataSource>   
   
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
GridResponsiveCss();
 </script> 
</asp:Content>

