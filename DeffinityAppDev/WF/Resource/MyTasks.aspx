<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Resource_MyTasks" Title="My Tasks" Codebehind="MyTasks.aspx.cs" %>

<%@ Register Src="~/WF/Resource/Controls/MyProjectsTab.ascx" TagName="ProjectStatus" TagPrefix="uc1" %>
<%@ Register Src="MailControls/TaskNotes.ascx" TagName="TaskNotes" TagPrefix="uc2" %>
<%@ Register src="~/WF/Vendors/Controls/RFIVendorMainTabNew.ascx" tagname="RFIVendorTabsNew" tagprefix="ucNew1" %>
<%@ Register Src="~/WF/Portal/Controls/CustomerHomeTabs.ascx" TagName="CustomerTabs" TagPrefix="ucCustomer1" %>
<%@ Register src="~/WF/Projects/MailControls/ProjectIssue.ascx" tagname="ProjectIssue" tagprefix="uc4" %>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.MyTasks%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.MyTasks%> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ProjectStatus id="ProjectStatus1" runat="server">
    </uc1:ProjectStatus>
    <ucNew1:RFIVendorTabsNew ID="RFIVendorTabs1" runat="server" visible="false" />  
 <ucCustomer1:CustomerTabs ID="CustomerTabs1" runat="server"  visible="false" />
 <uc2:TaskNotes id="TaskNotes1" runat="server" Visible="False" >
    </uc2:TaskNotes>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
<div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Projects%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlProjects" runat="server" 
        DataSourceID="objDS_Projects" DataTextField="ProjectTitle" 
        DataValueField="ProjectReference" Width="175px">
    </asp:DropDownList>
    <asp:ObjectDataSource ID="objDS_Projects" runat="server" 
        SelectMethod="PrjectTitleWithProjectReference_withSelect" 
        TypeName="Deffinity.Bindings.DefaultDatabind">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="ResourceID" SessionField="UID" 
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customers%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlPortfolio" runat="server" DataTextField="PortFolio" 
        DataValueField="ID" DataSourceID="objDS_Customer" Width="175px">
    </asp:DropDownList>
    <asp:ObjectDataSource ID="objDS_Customer" runat="server" 
        SelectMethod="Portfolio_Withselect" 
        TypeName="Deffinity.Bindings.DefaultDatabind"></asp:ObjectDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RAG%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddrag1" runat="server" Width="175px"  >
<asp:ListItem Value="&quot;&quot;">Please select...</asp:ListItem>
<asp:ListItem Value="RED">RED</asp:ListItem>
<asp:ListItem>AMBER</asp:ListItem>
<asp:ListItem>GREEN</asp:ListItem>

</asp:DropDownList>
            </div>
	</div>
</div>
    
<div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddstatus" runat="server" Width="175px"  >

</asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Task%> <%= Resources.DeffinityRes.ToDate%></label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtdudate" runat="server" SkinID="Date"></asp:TextBox>
<asp:Label ID ="imgdatecomm2" runat="server" SkinID="Calender" />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ProjectPriority%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlPriority" runat="server" Width="175px">
            <asp:ListItem Text="Please select..." Value="Please select..."></asp:ListItem>
             <asp:ListItem Text="High" Value="High"></asp:ListItem>
            <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
            <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
            </asp:DropDownList>
            </div>
	</div>
</div>
    <div class="form-group">
          <div class="col-md-12 form-inline">
               <asp:Button ID="btn_search" runat="server" SkinID="btnSearch" OnClick="btn_search_Click" /><ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                PopupButtonID="imgdatecomm2" TargetControlID="txtdudate" CssClass="MyCalendar">
            </ajaxToolkit:CalendarExtender>
	</div>
</div>
    

    <asp:Panel ID="Panel2" runat="server" Width="100%">
           <asp:GridView ID="GridView1" ShowFooter="True" runat="server" 
               AutoGenerateColumns="False"  Width="100%"  EmptyDataText="No tasks assigned." 
               OnRowCancelingEdit="GridView1_RowCancelingEdit" 
               OnRowEditing="GridView1_RowEditing" OnRowCommand="GridView1_RowCommand" 
               OnRowUpdating="GridView1_RowUpdating"  >
             <Columns>
              <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" /> 
             <asp:TemplateField >  
              <HeaderStyle Width="70px" />
                <ItemStyle  Width="70px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit" CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit" ></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"  CommandArgument='<%# Bind("ID")%>' ValidationGroup="Group2" SkinID="BtnLinkUpdate" ToolTip="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel" SkinID="BtnLinkCancel" ToolTip="Cancel" ></asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Project Reference">
            <ItemTemplate>
            <asp:Label ID="lblprojectRef" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:HyperLinkField DataTextField="Project" HeaderText="Project Reference" DataNavigateUrlFields="ProjectReference,AC2PID,ContractorID" DataNavigateUrlFormatString="~/WF/Resource/UpdateProject.aspx?Project={0}&AC2PID={1}&ContractorID={2}" />                --%>
                 <asp:HyperLinkField DataTextField="Project" HeaderText="Project Reference" DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Resource/MyTasksView.aspx?project={0}" />                
             <asp:BoundField DataField="ProjectReference" HeaderText="ProjectReference" Visible="False" />
              <asp:TemplateField HeaderText="Project Title" Visible="false">
               <ItemTemplate>
               <asp:Label ID="lblprojectReference" runat="server" Text='<%# Bind("ProjectReference") %>'></asp:Label>
                    <asp:Label ID="lblprojectTitle" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                </ItemTemplate>
                 <ItemStyle HorizontalAlign="Left" Width="190px" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Task">
            <ItemStyle HorizontalAlign="Left" Width="190px" />
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ItemDescription") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Site">
               <ItemStyle HorizontalAlign="Left" Width="130px" />
              <ItemTemplate>
                    <asp:Label ID="lblSite" runat="server" Text='<%# Bind("Site") %>'></asp:Label>
                </ItemTemplate>
             </asp:TemplateField>
             <asp:HyperLinkField DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Resource/MyTasksView.aspx?project={0}"
                    Text="&lt;i class='fa fa-tasks' &gt;&lt;/i &gt;" Target="_self" HeaderText="Gantt">
            <ItemStyle HorizontalAlign="Center" Width="50px" />
        </asp:HyperLinkField>
            <asp:TemplateField HeaderText="%  Complete" SortExpression="PercentComplete">
                                    <ItemTemplate>
                                        <asp:Literal ID="litPercentComplete" runat="server" Text='<%#Eval("PercentComplete")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    <asp:TextBox ID ="txtEditPercentComplete" runat="server" Text='<%#Eval("PercentComplete")%>' Width="38px" MaxLength="3"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="True" />
                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Start Date">
            <ItemStyle Width="65px" />
            <HeaderStyle Width="65px" />
                <ItemTemplate>
                    <asp:Label ID="lblStart" runat="server" Text='<%# Bind("ProjectStartDate","{0:d}") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
             
             <asp:TemplateField HeaderText="End Date">
            <ItemStyle Width="70px" />
            <HeaderStyle Width="70px" />
                <ItemTemplate>
                    <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("ProjectEndDate","{0:d}") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="150px"  ControlStyle-Width="150px" FooterStyle-Width="150px">
                <EditItemTemplate>
                   <asp:DropDownList ID="ddlRAGStatus" runat="server" DataSourceID="SqlDataSourceStatus2"  DataTextField="Status" DataValueField="ID" SelectedValue='<%# Bind("itemStatus") %>' >
                    </asp:DropDownList><asp:SqlDataSource ID="SqlDataSourceStatus2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="SELECT ID ,Status FROM [ItemStatus]"></asp:SqlDataSource>
               </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblItemStatus1" runat="server" Text='<%# Bind("ItemStatus1") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="RAG">
                <ItemTemplate>
                <asp:Literal ID="lit_Rag" runat="server" Text='<%#loadImage(Eval("RAGStatus").ToString())%>'></asp:Literal>                                        
                </ItemTemplate>
                <EditItemTemplate>
                <asp:Literal ID="lit_Rag_edit" runat="server" Text='<%#loadImage(Eval("RAGStatus").ToString())%>'></asp:Literal>                                        
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Priority" ItemStyle-HorizontalAlign="Center">
                     <ItemTemplate>
                         <asp:Image ID="imgpriority" runat="server" ImageUrl='<%#LoadPriority(Eval("Priority").ToString())%>' ToolTip='<%# Bind("Priority")%>' />
                     </ItemTemplate>                                          
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Issues" ItemStyle-CssClass="col-nowrap" FooterStyle-Width="150px">
                <ItemTemplate>
                    <asp:Image ID="imgissue" runat="server" ImageUrl='<%#loadCommentsPicture(Eval("Comments").ToString())%>'
                                ToolTip='<%#checkNoComments(Eval("Comments").ToString())%>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditComments" runat="server" Text='<%#Bind("Comments") %>' TextMode="MultiLine" SkinID="txtMulti_80" style="width:150px;height:50px"/>
                </EditItemTemplate>
                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="150px"  ControlStyle-Width="150px" FooterStyle-Width="150px">
            <EditItemTemplate>
             <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine"  Text='<%# Bind("Notes") %>' Width="150px"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>' Width="150px" ToolTip='<%#Bind("Notes") %>'></asp:Label>
            </ItemTemplate>
                     
            </asp:TemplateField>             
        </Columns>
    </asp:GridView>
        
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
GridResponsiveCss();
 </script> 

  </asp:Panel>
     <uc4:ProjectIssue ID="ProjectIssue1" runat="server" Visible="false" />
</asp:Content>

