<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="Tasks" Title="Tasks" Codebehind="Tasks.aspx.cs" %>

<%@ Register src="controls/DashboardTabs.ascx" tagname="DashboardTabs" tagprefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
   <%= Resources.DeffinityRes.Tasks%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:DashboardTabs ID="DashboardTabs1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server"> 

    <div class="form-group">
      <div class="col-md-4">
            <div class="control-label col-sm-4 "><%= Resources.DeffinityRes.UPTODATE%> </div>
           <div class="col-sm-7 form-inline">
              <asp:CheckBox runat="server" ID="chkUpto" /> <asp:TextBox ID="txtDate" runat="server" SkinID="txt_90" MaxLength="10"></asp:TextBox>
                 <asp:Image runat="server" ID="img3" SkinID="Calender" />
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
	<div class="col-md-4">
          
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-9">
               <asp:DropDownList id="ddlPortfolio" runat="server" Width="200px" 
            ValidationGroup="Submit"  DataTextField="PortFolio" DataValueField="ID"
             DataSourceID="SqlDataSource3"
            AutoPostBack="false">
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
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Site%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlSite" runat="server" Width="200px">
                                            </asp:DropDownList>
                                            
                                             <ajaxToolkit:CascadingDropDown ID="casCadSite1" runat="server" Category="Task" 
                    ParentControlID="ddlPortfolio" PromptText="Please select..." 
                    ServiceMethod="GetSites" ServicePath="~/WF/DC/webservices/ServiceMgr.asmx" 
                    TargetControlID="ddlSite" />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Resources%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlContractor" 
                                             runat="server" Width="200px">
                                            </asp:DropDownList>
                                            
 <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_AssignedTo" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                           <asp:QueryStringParameter Name="ProjectRefrence" QueryStringField="Project" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
            </div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlStatus" runat="server" Width="200px">
                      <asp:ListItem Value="myStatus">
                                                        Please Select...</asp:ListItem>
                      <asp:ListItem>COMPLETED</asp:ListItem>
                      <asp:ListItem>PENDING</asp:ListItem>
                  </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Programme%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlProjectOwner" runat="server"  Width="200px" DataSourceID="SqlDataSource8"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlProjectOwner_SelectedIndexChanged"
                    DataTextField="OperationsOwners" DataValueField="ID"
              ></asp:DropDownList>
              
               <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_AssignedProgramme" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RAGStatus%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlRAG" runat="Server" Width="200px">
                      <asp:ListItem Text=" <%$ Resources:DeffinityRes,PleaseSelect%>" Value="0"></asp:ListItem>
                      <asp:ListItem Text="GREEN" Value="1"></asp:ListItem>
                      <asp:ListItem Text="AMBER" Value="2"></asp:ListItem>
                      <asp:ListItem Text="RED" Value="3"></asp:ListItem>
                  </asp:DropDownList>
            </div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label">  <%= Resources.DeffinityRes.Country%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlCountry" runat="server" 
                 Width="199px"> </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.SubProgramme%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlProjectOwnerSub" runat="server" Width="199px" 
                             DataSourceID="SqlDataSourcesubprogram" AutoPostBack="true" onselectedindexchanged="ddlProjectOwnerSub_SelectedIndexChanged" 
                                DataValueField="ID" DataTextField="OPERATIONSOWNERS" >  
                            <%--<asp:ListItem Text=" Please select..." Value="0" Selected="True"></asp:ListItem> --%>                             
                            </asp:DropDownList>
                             <asp:SqlDataSource ID="SqlDataSourcesubprogram" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_AssignedSubProgramme" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlProjectOwner" DefaultValue="0" 
                                        Name="PROGRAMMEID" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                        </asp:SqlDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <div class="col-sm-11">
                <asp:RadioButtonList ID="rdbtnMilestone" runat="server" RepeatDirection="Horizontal" >
              <asp:ListItem Text="Show Milestones only" Value="1" ></asp:ListItem>
              <asp:ListItem Text="Show Milestones and Tasks" Value="0" Selected="True"></asp:ListItem>
              </asp:RadioButtonList>
            </div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Project%></label>
           <div class="col-sm-9">
                <asp:DropDownList runat="server" ID="ddlProjects" DataSourceID="SqlDataSource2" DataTextField="ProjectTitle" DataValueField="ProjectReference"
                      Width="200px">
                      <asp:ListItem   Text="Please select..." Value="0"></asp:ListItem>
                  </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Priority%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlPriority" runat="server" Width="200px">
                      <asp:ListItem Text=" <%$ Resources:DeffinityRes,PleaseSelect%>" Value="0"></asp:ListItem>
                      <asp:ListItem Text="High" Value="1"></asp:ListItem>
                      <asp:ListItem Text="Medium" Value="2"></asp:ListItem>
                      <asp:ListItem Text="Low" Value="3"></asp:ListItem>
                  </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                      PopupButtonID="img3" TargetControlID="txtDate" CssClass="MyCalendar">
                  </ajaxToolkit:CalendarExtender>
                  <asp:Button ID="btnSearch" runat="server" SkinID="btnSearch" OnClick="btnSearch_Click">
                  </asp:Button>
                  <asp:ImageButton ID="btnViewreport" runat="server" SkinID="ImgViewReport" OnClick="btnViewreport_Click"
                      Visible="false"></asp:ImageButton>
            </div>
	</div>
</div>

    <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.TasksList%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
    
			
		
		
		<asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="True" 
                AutoGenerateColumns="False"  OnRowUpdating="MyRowUpdate" 
                OnRowEditing="GridView1_RowEditing" OnRowDataBound="GridView1_RowDataBound" 
                AllowPaging="True" ondatabound="GridView1_DataBound" 
                onrowcreated="GridView1_RowCreated" EmptyDataText="No data exist" >
                                       <Columns>
                           
                            <asp:TemplateField Visible="False"> 
                                <ItemTemplate>
                                   <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>' /> 
                                </ItemTemplate> 
                            </asp:TemplateField>                       

                             <asp:HyperLinkField DataTextField="ProjectReference"  HeaderStyle-Width="100"   DataNavigateUrlFields="ProjectReference1" DataNavigateUrlFormatString="~/WF/Projects/projectoverview4.aspx?project={0}" HeaderText="
                        <%$ Resources:DeffinityRes,ProjectRef%>" />
                         <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Task%>"  SortExpression="ID1">
                            <ItemTemplate>
                              <asp:Label ID="Label3a" runat="server" Text='<%# Bind("ID1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                                    
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,RAG%>">
                                <ItemTemplate>
                                    <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("RAGStatus") %>' />                                    
                                    <asp:Image ID="Image1" runat="server" />
                                </ItemTemplate>
                             </asp:TemplateField>                             
                          <asp:BoundField DataField="OperationsOwners" HeaderText="<%$ Resources:DeffinityRes,Programme%>" SortExpression="OperationsOwners" ReadOnly="True" >
                                <ControlStyle Width="50px" />
                                <HeaderStyle Width="75px" />
                            </asp:BoundField>                           
                            <asp:BoundField DataField="ContractorsName" HeaderText="<%$ Resources:DeffinityRes,Resource%>" SortExpression="ContractorsName"  ReadOnly="True" >
                                <ControlStyle Width="75px" />
                                <HeaderStyle Width="75px" />
                            </asp:BoundField>                            
                          <asp:BoundField DataField="Site" HeaderText="<%$ Resources:DeffinityRes,Site%>" SortExpression="Site" ReadOnly="True"/>                               
                           <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ExpectedStartdate%>" SortExpression="StartDate"> 
                                <ItemTemplate>
                                  <%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:d}")%>
                                </ItemTemplate> 
                                  <HeaderStyle Width="75px"/>
                            </asp:TemplateField> 
                                              
                           <asp:TemplateField HeaderText="Expected End Date" SortExpression="EndDate" > 
                                <ItemTemplate>
                                  <%# DataBinder.Eval(Container.DataItem, "EndDate", "{0:d}")%>
                                </ItemTemplate> 
                                   <HeaderStyle Width="75px" />
                            </asp:TemplateField>
                             
                                           <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Priority%>" ItemStyle-HorizontalAlign="Center">
                     <ItemTemplate>
                         <asp:Image ID="Image2" runat="server" ImageUrl='<%#LoadPriority(Eval("Priority").ToString())%>' ToolTip='<%# Bind("Priority")%>' />
                     </ItemTemplate>                                          
                 </asp:TemplateField>
                                           <asp:BoundField DataField="PercentComplete" HeaderText="<%$ Resources:DeffinityRes,Percentcomplete%>" 
                                               ReadOnly="True" SortExpression="PercentComplete">
                                               <ControlStyle Width="75px" />
                                               <HeaderStyle Width="75px" />
                                           </asp:BoundField>
                                           <asp:BoundField DataField="Comments" HeaderText="<%$ Resources:DeffinityRes,Notes%>" ReadOnly="True" 
                                               SortExpression="Comments">
                                               <ControlStyle Width="125px" />
                                               <HeaderStyle Width="75px" />
                                           </asp:BoundField>
                                           <asp:TemplateField Visible="false">
                                               <ItemTemplate>
                                                   <asp:CheckBox ID="chkStatus" runat="server" />
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:BoundField HeaderStyle-CssClass="header_bg_r" DataField="ItemStatus" HeaderText="<%$ Resources:DeffinityRes,Status%>" ReadOnly="True" SortExpression="ItemStatus">                                               <HeaderStyle Width="75px" />
                                           </asp:BoundField>
                                                       
                        </Columns>
                    </asp:GridView>
                   
            
            <asp:Panel Width="100%" runat="server" ID="Panel2" HorizontalAlign="Right" >
		        <asp:ImageButton id="btnUpdateStatus1" onclick="btnUpdateStatus1_Click" runat="server" SkinID="ImgUpdateStatus" Visible="False"></asp:ImageButton>
		       
		    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>" 
                        SelectCommand="AMPS_Projects" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                         <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                         </SelectParameters>
                     </asp:SqlDataSource>
		    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>" 
                        SelectCommand="DN_TasksMy" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                       <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                         </SelectParameters>
                     </asp:SqlDataSource>
             <asp:SqlDataSource ID="Task" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        ProviderName="<%$ ConnectionStrings:DBstring.ProviderName %>" SelectCommand="DN_Tasks"
                        SelectCommandType="StoredProcedure" OnLoad="Task_Load">
                        <SelectParameters>        
                            <asp:Parameter Name="SiteID" Type="int32" DefaultValue="0" />
                            <asp:Parameter Name="subprogramme" Type="int32" DefaultValue="0" />
                           <asp:Parameter Name="ContractorID" Type="int32" DefaultValue="0" />
                            <asp:Parameter Name="ProjectOwnerID" Type="int32" DefaultValue="0" />
                            <asp:Parameter Name="StatusID" Type="int32" DefaultValue="0" />
                            <asp:Parameter Name="RAGStatus" Type="int32" DefaultValue="0" />                            
                            <asp:Parameter Name="Date" Type="string" DefaultValue="mydate" />
                            <asp:Parameter Name="Upto" Type="int32" DefaultValue="0" />
                            <asp:Parameter Name="ProjectReference" Type="int32" DefaultValue="0" />    
                            <asp:Parameter Name="Priority" Type="int32" DefaultValue="0" />   
                            <asp:Parameter Name="isMileStone" Type="int32" DefaultValue="0" /> 
                            <asp:Parameter Name="Portfolio" Type="Int32" DefaultValue="0" />                     
                        </SelectParameters>
                    </asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSourceCountry" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                            SelectCommand="GetCountryByProgrameOrSubProgramme" SelectCommandType="StoredProcedure">
                            <SelectParameters>                            
                           <asp:Parameter Name="Program" Type="int32" DefaultValue="0" />   
                            <asp:Parameter Name="SubProgram" Type="int32" DefaultValue="0" />   
                        </SelectParameters>                    
		   </asp:SqlDataSource>
		</asp:Panel>

		
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
GridResponsiveCss();
 </script> 
</asp:Content>

