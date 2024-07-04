<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" Inherits="PortfolioMain" Title="Portfolio" Codebehind="PortfolioMain.aspx.cs" %>


<%@ Register src="controls/DashboardTabs.ascx" tagname="DashboardTabs" tagprefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Dashboard%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.Customer%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:DashboardTabs ID="DashboardTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

<div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.SelectView%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlselectview" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlselectview_SelectedIndexChanged">
                    <asp:ListItem Value="0">ALL</asp:ListItem>
                    <asp:ListItem Value="1">My Customer</asp:ListItem>
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
	
</div>

    <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.SelectCustomer%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlprojectPortfolio" runat="server" AppendDataBoundItems="true" ClientIDMode="Static"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlprojectPortfolio_SelectedIndexChanged">
                    <asp:ListItem Text="Please Select.." Value="0" />
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
	
</div>

    
<div class="form-group">
      <div class="col-md-6">
           <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Summary%> </strong> 
            <hr class="no-top-margin" />
            </div>
      </div>
      <div id="pnlSummary" runat="server" visible="false">
            <div class="form-group">
                     <div class="col-md-12">
                         <label class="col-sm-6 control-label">Customer Owner</label>
                          <div class="col-sm-6 control-label">
                              <asp:Label ID="lblportfolioowner" runat="server"></asp:Label>
                          </div>
                     </div>
             </div>
            <div class="form-group">
                     <div class="col-md-12">
                         <label class="col-sm-6 control-label"> Number of Projects in The Customer</label>
                          <div class="col-sm-6 control-label">
                              <asp:Label ID="lblnpojectP" runat="server"></asp:Label>
                          </div>
                     </div>
             </div>
            <div class="form-group">
                     <div class="col-md-12">
                         <label class="col-sm-6 control-label">Live Project</label>
                          <div class="col-sm-6 control-label">
                              <asp:Label ID="lblliveProject" runat="server"></asp:Label>
                          </div>
                     </div>
             </div>
            <div class="form-group">
                     <div class="col-md-12">
                         <label class="col-sm-6 control-label">Pending Project</label>
                          <div class="col-sm-6 control-label">
                              <asp:Label ID="lblpendingProject" runat="server"></asp:Label>
                          </div>
                     </div>
             </div>
            <div class="form-group">
                     <div class="col-md-12">
                         <label class="col-sm-6 control-label"> Customer Budget</label>
                          <div class="col-sm-6 control-label">
                              <asp:Label ID="lblpBudget" runat="server"></asp:Label>
                          </div>
                     </div>
             </div>
            <div class="form-group">
                     <div class="col-md-12">
                         <label class="col-sm-6 control-label">Total Customer Value</label>
                          <div class="col-sm-6 control-label">
                              <asp:Label ID="lbltotpvalue" runat="server"></asp:Label>
                          </div>
                     </div>
             </div>
         </div>
	</div>
	<div class="col-md-6">
          <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.ProjectSummary%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
          <div id="DivGraph" style="width: 100%;"></div>
	</div>
	
</div>


    <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.LiveProjects%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

    <asp:Panel ID="PANEL1" runat="server" Width="100%">
        <asp:GridView ID="girdLiveProjects" runat="server" Width="100%" AutoGenerateColumns="False"
            GridLines="None" AllowPaging="True" EmptyDataText="No projects available.">
            <Columns>
                <%--<asp:BoundField Visible="False" DataField="ID" />
                <asp:BoundField Visible="False" DataField="Project" />--%>
                <asp:BoundField DataField="ProjectReference" HeaderText="Project" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" CssClass="header_bg_l" />
                    <ItemStyle Wrap="true" />
                </asp:BoundField>
                <asp:BoundField DataField="ProjectTitle" HeaderText="Title" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
               
                <asp:TemplateField HeaderText="Start Date" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:d}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ProjectEndDate", "{0:d}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField Text="QA" DataNavigateUrlFields="Project" DataNavigateUrlFormatString="./TEST.aspx?Project={0}#"
                    HeaderStyle-Font-Bold="true" HeaderText="QA" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false">
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                <asp:HyperLinkField Text="Variance" DataNavigateUrlFields="ProjectReference" HeaderStyle-Font-Bold="true"
                    HeaderText="Variance" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    Visible="false">
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                 <asp:TemplateField HeaderText="Project Value" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "BudgetaryCost", "{0:c}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Cost" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "ActualCost", "{0:c}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Variations<br/>Approved" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Literal ID="litVariationsApproved" runat="server" Text='<%#Eval("VariationsApproved","{0:C}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Variations<br/>Not Approved" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Literal ID="litVariationsNotApproved" runat="server" Text='<%#Eval("VariationsNotApproved","{0:C}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoiced Amount" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Literal ID="litInvoicedAmount" runat="server" Text='<%#Eval("AmountInvoiced","{0:C}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoice Remaining" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Literal ID="litInoviceRemaining" runat="server" Text='<%#Eval("RemainingInvoice","{0:c}")%>' />
                    </ItemTemplate>
                    <HeaderStyle CssClass="header_bg_r" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.PendingProjects%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

     <asp:Panel ID="PANEL2" runat="server" Width="100%">
        <asp:GridView ID="gridPendingProjects" runat="server" AutoGenerateColumns="False"
            GridLines="None" Width="100%" CellPadding="0" CellSpacing="1" AllowPaging="True" EmptyDataText="No projects available.">
            <Columns>
                <asp:BoundField Visible="False" DataField="ID" />
                <asp:BoundField Visible="False" DataField="Project" />
                <asp:BoundField DataField="ProjectReference" HeaderText="Project" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="12%" ItemStyle-Width="12%">
                    <HeaderStyle HorizontalAlign="Center" Width="12%" CssClass="header_bg_l" />
                    <ItemStyle Width="12%" />
                </asp:BoundField>
                <asp:BoundField DataField="ProjectTitle" HeaderText="Title" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="14%" ItemStyle-Width="14%">
                    <HeaderStyle HorizontalAlign="Center" Width="14%" />
                    <ItemStyle Width="14%" />
                </asp:BoundField>
             
                <asp:TemplateField HeaderText="Start Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="13%">
                    <HeaderStyle HorizontalAlign="Center" Width="13%" />
                    <ItemStyle HorizontalAlign="Center" Width="13%" />
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:d}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="13%">
                    <HeaderStyle HorizontalAlign="Center" Width="13%" />
                    <ItemStyle HorizontalAlign="Center" Width="13%" />
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ProjectEndDate", "{0:d}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="Project Value" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="14%">
                    <HeaderStyle HorizontalAlign="Center" Width="14%" />
                    <ItemStyle HorizontalAlign="Right" Width="14%" />
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "BudgetaryCost", "{0:c}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Cost" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="14%">
                    <HeaderStyle HorizontalAlign="Center" Width="14%" CssClass="header_bg_r"/>
                    <ItemStyle HorizontalAlign="Right" Width="14%" />
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "ActualCost", "{0:c}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField Text="QA" ControlStyle-Width="10%" DataNavigateUrlFields="Project"
                    DataNavigateUrlFormatString="./TEST.aspx?Project={0}" HeaderStyle-Font-Bold="true"
                    HeaderText="QA" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    Visible="false">
                    <ControlStyle Width="10%" />
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                <asp:HyperLinkField Text="Variance " ControlStyle-Width="10%" DataNavigateUrlFields="ProjectReference"
                    HeaderStyle-Font-Bold="true" HeaderText="Variance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ControlStyle Width="10%" />
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="header_bg_r" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Completedprojects%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
     <asp:Panel ID="PANEL3" runat="server" Width="100%">
        <asp:GridView ID="gridCompletedProjects" runat="server" AutoGenerateColumns="False"
            GridLines="None" Width="100%" AllowPaging="True" EmptyDataText="No projects available.">
            <Columns>
                <asp:BoundField Visible="False" DataField="ID" />
                <asp:BoundField Visible="False" DataField="Project" />
                <asp:BoundField DataField="ProjectReference" HeaderText="Project" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="60px" ItemStyle-Width="12%">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" CssClass="header_bg_l" />
                    <ItemStyle Width="12%" />
                </asp:BoundField>
                <asp:BoundField DataField="ProjectTitle" HeaderText="Title" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="14%" ItemStyle-Width="14%">
                    <HeaderStyle HorizontalAlign="Center" Width="14%" />
                    <ItemStyle Width="14%" />
                </asp:BoundField>
               
                <asp:TemplateField HeaderText="Start Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="13%">
                    <HeaderStyle HorizontalAlign="Center" Width="13%" />
                    <ItemStyle HorizontalAlign="Center" Width="13%" />
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:d}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="13%">
                    <HeaderStyle HorizontalAlign="Center" Width="13%" />
                    <ItemStyle HorizontalAlign="Center" Width="13%" />
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ProjectEndDate", "{0:d}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Project Value" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="14%">
                    <HeaderStyle HorizontalAlign="Center" Width="14%" />
                    <ItemStyle HorizontalAlign="Right" Width="14%" />
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "BudgetaryCost", "{0:c}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Cost" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="14%">
                    <HeaderStyle HorizontalAlign="Center" Width="14%" CssClass="header_bg_r" />
                    <ItemStyle HorizontalAlign="Right" Width="14%" />
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "ActualCost", "{0:c}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField Text="QA" ControlStyle-Width="10%" DataNavigateUrlFields="Project"
                    DataNavigateUrlFormatString="./TEST.aspx?Project={0}" HeaderStyle-Font-Bold="true"
                    HeaderText="QA" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    Visible="false">
                    <ControlStyle Width="10%" />
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                <asp:HyperLinkField Text="Variance " ControlStyle-Width="10%" DataNavigateUrlFields="ProjectReference"
                    HeaderStyle-Font-Bold="true" HeaderText="Variance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ControlStyle Width="10%" />
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="header_bg_r" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
            </Columns>
        </asp:GridView>
    </asp:Panel>



    <asp:Panel ID="Panel4" runat="server">

        
<div class="form-group">
      <div class="col-md-4">
          <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.LiveIssues%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
        <asp:Panel ID="Panel21" runat="server" Height="200px" Width="310px" BorderStyle="Solid" BorderWidth="1px" BorderColor="LightSteelBlue" ScrollBars="Vertical">
         <asp:DataList ID="ListLiveIssues" runat="server" width="293px" SkinID="ProgrammeList" >
          <ItemTemplate>
                <table width="100%" border="0" cellpadding="0" cellspacing="1"><tr>
                <td width="70%">
                <%# DataBinder.Eval(Container.DataItem, "Issue")%>
                    </td>
                    <td align="right"  width="20%">
                    <%# DataBinder.Eval(Container.DataItem, "ScheduledDate","{0:d}")%>
                    </td>
                    <td align="right" width="10%">
                    <a href="Mailto:<%#  DataBinder.Eval(Container.DataItem,"ContractorEmail").ToString() %>" > <asp:Label SkinID="Mail" ID="Image1" runat="server" /></a> 
                    </td>                    
                    </tr>
                    </table>
            </ItemTemplate>
          
        </asp:DataList>
        </asp:Panel>   
	</div>
	<div class="col-md-4">
        <div class="form-group">
        <div class="col-md-12">
           <strong>Resources Allocated to Projects</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

        <asp:Panel ID="Panel5" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="LightSteelBlue"  Height="200px" Width="310px" ScrollBars="Vertical">
        <asp:DataList ID="ListResources" runat="server"  width="293px" SkinID="ProgrammeList" >
            <ItemTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="1"><tr><td>
            <%# DataBinder.Eval(Container.DataItem, "Contractor")%>
            </td>
            <td  align="right">            
            <a href="Mailto:<%#  DataBinder.Eval(Container.DataItem,"ContractorEmail").ToString() %>"> <asp:Label ID="Image1" SkinID="Mail" runat="server" /></a> 
                    <asp:Label ID="imgTelephone" ToolTip='<%# DataBinder.Eval(Container.DataItem, "Tele") %>' SkinID="Phone" runat="server" />
            </td>
            </tr></table>
            </ItemTemplate>
        </asp:DataList>
        </asp:Panel>
	</div>
	<div class="col-md-4">
        <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.MitigationAction%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
         
        <asp:Panel ID="Panel6" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="LightSteelBlue" Height="200px" Width="310px" ScrollBars="Vertical">
        <asp:DataList ID="ListMitigation" runat="server"  width="293px" SkinID="ProgrammeList" >
            <ItemTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="1"><tr>
            <td>
                <%# DataBinder.Eval(Container.DataItem, "MigatingActions")%>            
               </td>
               <td align="right">
               <%# DataBinder.Eval(Container.DataItem, "ActionDeadline","{0:d}")%>
               </td>
            </tr></table>
            </ItemTemplate>
        </asp:DataList>
        </asp:Panel>
	</div>
</div>



        </asp:Panel>

<div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.DependencyMap%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

      <asp:GridView ID="GridView1" runat="server"  DataKeyNames="TaskID"
            width="100%" AutoGenerateColumns="False"             
            ShowFooter="True" DataSourceID="SqlDataSource3" OnRowDataBound="GridView1_RowDataBound"  EmptyDataText="No dependency tasks available.">
           <RowStyle />
            <Columns>  
                <asp:TemplateField  HeaderText="Task">
                    <ItemTemplate>
                     
                    <asp:Label ID="Task" Width="115px" runat="server" Text='<%# Bind("Task") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="120px" HorizontalAlign="Left" />
                  <HeaderStyle CssClass="header_bg_l" />
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="Start Date">
                <ItemTemplate>
                        <asp:Label ID="lblSdate" runat="server" Text='<%# Bind("TaskStartDate","{0:d}") %>' ></asp:Label>
                </ItemTemplate>                    
                 <ItemStyle  HorizontalAlign="Center" Width="80px"/>               
                </asp:TemplateField>                
                <asp:TemplateField  HeaderText="End Date">                  
                <ItemTemplate>
                        <asp:Label ID="lblEnddate" runat="server" Text='<%# Bind("TaskCompletiondate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
                    
                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                    </asp:TemplateField> 
                    
                <asp:TemplateField  HeaderText="Depending On Project">
                    <ItemTemplate>
                    <asp:Label ID="DepOnProject" Width="60px" runat="server" Text='<%# Bind("DependingOnProject") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:TemplateField>    
                    <asp:TemplateField  HeaderText="Task">
                    <ItemTemplate>
                    <asp:Label ID="DepOnTask" Width="90px" runat="server" Text='<%# Bind("DependingOnTask") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:TemplateField>
                
                <asp:TemplateField  HeaderText="Start Date">                  
                <ItemTemplate>
                        <asp:Label ID="lblOnStartdate" runat="server" Text='<%# Bind("OnStartDate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
                    
                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                    </asp:TemplateField> 
                    
                    <asp:TemplateField  HeaderText="End Date">                  
                    <ItemTemplate>
                        <asp:Label ID="lblOnEnddate" runat="server" Text='<%# Bind("OnCompletionDate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
                    
                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                    </asp:TemplateField> 
                     
                <asp:TemplateField  HeaderText="Dependent Project">
                    <ItemTemplate>
                    <asp:Label ID="DepProject" Width="60px" runat="server" Text='<%# Bind("DependentProject") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:TemplateField>    
                    <asp:TemplateField  HeaderText="Task">
                    <ItemTemplate>
                    <asp:Label ID="DepTask" Width="90px" runat="server" Text='<%# Bind("DependentTask") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:TemplateField>
                
                <asp:TemplateField  HeaderText="Start Date">                  
                <ItemTemplate>
                        <asp:Label ID="lblDepStartdate" runat="server" Text='<%# Bind("DepStartDate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
                    
                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                    </asp:TemplateField> 
                    
                    <asp:TemplateField HeaderText="End Date">                  
                <ItemTemplate>
                        <asp:Label ID="lblDepEnddate" runat="server" Text='<%# Bind("DepCompletionDate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
                    
                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                    <HeaderStyle CssClass="header_bg_r" />
                    </asp:TemplateField> 
               
            </Columns>   
   </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DBstring %>" 
        SelectCommand="DEFFINITY_IPD_PROJTASKS_Portfolio" 
        SelectCommandType="StoredProcedure" >
        <SelectParameters>
        <asp:ControlParameter ControlID="ddlprojectPortfolio" DefaultValue="0" 
                Name="PortfolioID" PropertyName="SelectedValue" Type="Int32" />
                <asp:SessionParameter SessionField="UID" Name="UID" Type="Int32" />
        </SelectParameters>   
       
        </asp:SqlDataSource>
  <%: System.Web.Optimization.Scripts.Render("~/bundles/charts") %>
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
GridResponsiveCss();
 </script> 
<script type="text/javascript">
    $(document).ready(function () {
        var Pid = $('#ddlprojectPortfolio').val();
        //$("#ddlprojectPortfolio").change(function () {
        //    var Pid = $('#ddlprojectPortfolio').val();
        //    if (Pid > 0) {
        //        GetGraphInDashBoard(Pid);
        //    }
        //});
        if (Pid > 0) {
            GetGraphInDashBoard(Pid);
        }
    });
    function GraphBind(dataSource)
    {
        debugger;
        $("#DivGraph").dxChart({
            //equalBarWidth: false,
            dataSource: dataSource,
            commonSeriesSettings: {
                argumentField: "state",
                type: "bar"
            },
            series: [
                { valueField: "Budgetcost", name: "Budget cost" },
                { valueField: "ActualcosttoDate", name: "Actual cost to Date" },
                { valueField: "Variances", name: "Variances" },
                { valueField: "Invoiced", name: "Invoiced" }
            ],
            tooltip: {
                enabled: true,
                customizeTooltip: function (arg) {
                    return {
                        text: this.argumentText + "<br/>" + this.valueText
                    };
                }
            },
            size: {
                height: 300
            },
            legend: {
                verticalAlignment: "bottom",
                horizontalAlignment: "center"
            },
            title: ""
        });
    }
    function GetGraphInDashBoard(Pid) {
      
        $.ajax({
            url: '../Dashboard/PortfolioMain.aspx/GraphInDashBoard',
            type: "POST",
            data: "{'Pid': '" + Pid + "'}",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                var datatable1 = [];
                var Newdt = jQuery.parseJSON(data.d);
                for (var i = 0; i < Newdt.length; i++) {
                    datatable1.push(
                        {
                            state: Newdt[i].state,
                            Budgetcost: Newdt[i].Budgetcost,
                            ActualcosttoDate: Newdt[i].ActualcosttoDate,
                            Variances: Newdt[i].Variances,
                            Invoiced: Newdt[i].Invoiced
                        });
                }
                GraphBind(datatable1);
            }
        });
    };
</script>

</asp:Content>
