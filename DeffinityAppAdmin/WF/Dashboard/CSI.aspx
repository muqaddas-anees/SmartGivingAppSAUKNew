<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="CIP1" Title="CSI" Codebehind="CSI.aspx.cs" %>

<%@ Register src="controls/DashboardTabs.ascx" tagname="DashboardTabs" tagprefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Dashboard%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Continuous Improvement Program
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">

    <uc1:DashboardTabs ID="DashboardTabs1" runat="server" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-8">
                 <asp:DropDownList id="ddlPortfolio" runat="server" Width="200px" 
            ValidationGroup="Submit"  DataTextField="PortFolio" DataValueField="ID"
             DataSourceID="SqlDataSource3"
            AutoPostBack="True" onselectedindexchanged="ddlPortfolio_SelectedIndexChanged">
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
                <asp:DropDownList ID="ddlProjGroups" runat="server" DataSourceID="SqlDataSource1"
                        Width="200px" DataTextField="OperationsOwners" DataValueField="ID" AutoPostBack="true"
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
               <asp:DropDownList ID="ddlsubprogram" runat="server" Width="220px" DataSourceID="SqlDataSourcesubprogram"
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
                        Width="280px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="DEFFINITY_CONTRACTORPROJECTS11" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter  Name="ContractorID" SessionField="UID" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlProjGroups" DefaultValue="0" Name="Programmer"
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:SessionParameter Name="SID" SessionField="SID" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlsubprogram" DefaultValue="0" Name="SubProgID"
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
            </div>
	</div>
	<div class="col-md-4">
         
	</div>
	<div class="col-md-4">
          
	</div>
</div>

     
    <asp:Panel ID="ps" Width="100%" runat="server" >
<asp:Panel ID="pa1" runat="server" Width="100%" Visible="false" >
     <asp:ValidationSummary id="ValidationSummary1" runat="server" ValidationGroup="Group1"></asp:ValidationSummary>
   <asp:RequiredFieldValidator ID="RC" runat="server" ErrorMessage="Please Enter Improvement" Display="None" ControlToValidate="txtImprovementpage" ValidationGroup="Group1" ></asp:RequiredFieldValidator>
        <asp:Label ID="lblConfirmation" runat="server" Width="274px"></asp:Label>
  
    </asp:Panel>
   <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">DataSourceID="sourceGrid"OnClick="btnSubmit_Click" 
        <ContentTemplate>--%>

        <asp:GridView ID="GridView1" runat="server" 
                            AutoGenerateSelectButton="false" DataKeyNames="ID" 
                            OnRowCommand="GridView1_RowCommand" OnRowDeleted="GridView1_RowDeleted" OnRowUpdated="GridView1_RowUpdated"
                            OnRowCreated="GridView1_RowCreated" GridLines="None" AutoGenerateColumns="False"
                            HorizontalAlign="Left" 
                            CellPadding="0" CellSpacing="1" ShowFooter="True" Width="100%" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting" style="margin-bottom: 1px">
                         
                            <Columns>
               <asp:TemplateField HeaderStyle-CssClass="header_bg_l">  
              <HeaderStyle Width="60px" />
                <ItemStyle  Width="60px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" Enabled="<%#CommandField()%>" CausesValidation="false" CommandName="Edit" CommandArgument="<%# Bind('ID')%>" SkinID="BtnLinkEdit" ToolTip="Edit" ></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"  CommandArgument="<%# Bind('ID')%>" ValidationGroup="Group2" SkinID="BtnLinkUpdate" ToolTip="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel" SkinID="BtnLinkCancel" ToolTip="Cancel" ></asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
              <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" /> 
              <asp:TemplateField HeaderText="Project Ref.">
              <ItemStyle Width="100px" />
              <ItemTemplate>
              <asp:Literal ID="litPref" runat="server" Text='<%#Bind("ProjectReferenceWithExt") %>' />
              </ItemTemplate>
              </asp:TemplateField>
              
              
                                
                                <asp:TemplateField HeaderText="&#160;&#160;Date Logged&#160;&#160;">
                                    <ItemTemplate>
                                        <asp:Literal ID="litLoggedDate" runat="server" Text='<%#Bind("DateLogged","{0:d}") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Project Title">
           
            <HeaderStyle Width="100px" />
            <ItemStyle Width="120px" />
                <%--<EditItemTemplate>
                    <asp:DropDownList ID="ddresource3" runat="server" Width="150px" DataSourceID="SqlDataSource3" DataTextField="ProjectTitle" DataValueField="ProjectReference" SelectedValue='<%# Bind("ProjectReference") %>'   ></asp:DropDownList><asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="SELECT ProjectReference, ProjectTitle FROM Projects">
                    </asp:SqlDataSource>
                </EditItemTemplate>--%>
                <ItemTemplate>
                    <asp:Label ID="lblProjectTitle" runat="server" Text='<%# Bind("ProjectTitle") %>' ToolTip='<%#Bind("ProjectTitle") %>'></asp:Label>
                </ItemTemplate>
               
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Improvement">
           <HeaderStyle Width="175px" />
            <ItemStyle Width="175px" />
                <EditItemTemplate>
                    <asp:TextBox ID="txtImprovement" runat="server" Width="175px" TextMode="MultiLine"  Text='<%# Bind("Improvement") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Improvement") %>' Width="175px" ToolTip='<%#Bind("Improvement") %>'></asp:Label>
                </ItemTemplate>
               
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Expected Result">
           <HeaderStyle Width="175px" />
            <ItemStyle Width="175px" />
                <EditItemTemplate>
                    <asp:TextBox ID="txtResultExpected" runat="server" Width="175px" TextMode="MultiLine"  Text='<%# Bind("ResultExpected") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblResultExpected" runat="server" Text='<%# Bind("ResultExpected") %>' Width="175px" ToolTip='<%#Bind("ResultExpected") %>'></asp:Label>
                </ItemTemplate>
               
            </asp:TemplateField>
            
              <asp:TemplateField HeaderText="Resource">
           
            <HeaderStyle Width="100px" />
            <ItemStyle Width="120px" />
               <EditItemTemplate>
                    <asp:DropDownList ID="ddresource1" runat="server" Width="150px" DataSourceID="SqlDataSource1" DataTextField="ContractorName" DataValueField="ID" SelectedValue='<%# Bind("ContractorID") %>'   ></asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="SELECT ID, ContractorName FROM Contractors union select 0 as ID,' Select Resource' as ContractorName ORDER BY ContractorName">
                    </asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblContractorName" runat="server" Text='<%# Bind("ContractorName") %>' ToolTip='<%#Bind("ContractorName") %>'></asp:Label>
                </ItemTemplate>
               
            </asp:TemplateField>
                           
                      
                 <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
            <HeaderStyle Width="70px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete" Enabled="<%#CommandField()%>"
                            SkinID="BtnLinkDelete"  CommandArgument='<%# Bind("ID") %>'  OnClientClick="return confirm('Do you want to delete the record?');" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate >
                                <asp:Label ID="lblNoData" runat="server" Text="Currently no ideas is submitted" />
                            </EmptyDataTemplate>
                        </asp:GridView>
        <div class="form-group">
        <div class="col-md-12">
           <strong>Lessons Learnt </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
        <asp:GridView ID="GridLessonsLearnt" AutoGenerateColumns="False" runat="server" DataKeyNames="ID,ProjectReference" 
                  EmptyDataText="No Records Found"
                  Width="100%" OnRowCommand="GridLessonsLearnt_RowCommand" OnRowEditing="GridLessonsLearnt_RowEditing" 
                      OnRowCancelingEdit="GridLessonsLearnt_RowCancelingEdit" OnRowDataBound="GridLessonsLearnt_RowDataBound1"
                       OnRowUpdating="GridLessonsLearnt_RowUpdating" AllowPaging="true" PageSize="15" OnPageIndexChanging="GridLessonsLearnt_PageIndexChanging">
                            <Columns>
                             <asp:CommandField ShowEditButton="True" Visible="false" />
                             <asp:TemplateField HeaderStyle-CssClass="header_bg_l">  
              <HeaderStyle Width="60px" />
                <ItemStyle  Width="60px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" Enabled="<%#CommandField()%>" CausesValidation="false" CommandName="Edit" SkinID="BtnLinkEdit" ToolTip="Edit" ></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update" ValidationGroup="Group2" SkinID="BtnLinkUpdate" ToolTip="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel" SkinID="BtnLinkCancel" ToolTip="Cancel" ></asp:LinkButton>
                </EditItemTemplate>
              
            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                    SortExpression="ID" Visible="false"/>
                            <asp:TemplateField HeaderText="Project Reference">
                            <ItemTemplate>
                            <asp:Label ID="lblProjectReference" runat="server"></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText="Description">
                            <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>                           
                            <asp:TemplateField HeaderText="Business Impact">
                            <ItemTemplate>
                            <asp:Label ID="lblBusiness" runat="server"></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Identified by">
                            <ItemTemplate>
                            <asp:Label ID="lblIdentifiedBy" runat="server"></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Remediation Actions">
                            <ItemTemplate>
                            <asp:Label ID="lblRemediation" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:TextBox ID="txtRemediation" runat="server" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned to">
                            <ItemTemplate>
                            <asp:Label ID="lblAssignedTo" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:DropDownList ID="ddlAssignedTo" runat="server"></asp:DropDownList>
                            </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="header_bg_r">
                            <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                            </EditItemTemplate>
                            </asp:TemplateField>
                           
                            </Columns>
                            </asp:GridView>
         <div class="form-group">
        <div class="col-md-12">
           <strong>Customer Satisfaction Feedback </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                        <asp:GridView ID="gridCustViews" runat="server" AutoGenerateColumns="False"
                                   
                                    GridLines="None" HorizontalAlign="Left" 
                                    CellPadding="0" CellSpacing="1" Width="100%" OnRowCommand="gridCustViews_RowCommand" >
                            <Columns>
<asp:TemplateField HeaderText="Project Title">
           
            <HeaderStyle Width="150px" />
            <ItemStyle Width="150px" />
                <ItemTemplate>
                    <asp:Label ID="lblProjectTitle1" runat="server" Text='<%# Bind("ProjectTitle") %>' ToolTip='<%#Bind("ProjectTitle") %>'></asp:Label>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Satisfaction">
            <HeaderStyle Width="80px" />
            <ItemStyle Width="80px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblSatisfaction" runat="server" Text='<%# Bind("Satisfied") %>' ToolTip='<%#Bind("Satisfied") %>'></asp:Label>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ways to Improve">
           
            <HeaderStyle Width="200px" />
            <ItemStyle Width="200px" />
                <ItemTemplate>
                    <asp:Label ID="lblways2Improve" runat="server" Text='<%# Bind("WaystoImprove") %>' ToolTip='Ways to Improve'></asp:Label>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Non Performing Individuals">
           
            <HeaderStyle Width="200px" />
            <ItemStyle Width="200px" />
                <ItemTemplate>
                    <asp:Label ID="lblnpindividuals" runat="server" Text='<%# Bind("NonPerformingIndividuals") %>' ToolTip='Non Performing Individuals'></asp:Label>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Discuss">
           
            <HeaderStyle Width="80px" />
            <ItemStyle Width="80px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lbldiscuss" runat="server" Text='<%# Bind("Discuss") %>' ToolTip='Discuss'></asp:Label>
                </ItemTemplate>
               
            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="&#160;&#160;Date Logged&#160;&#160;">
                                    <ItemTemplate>
                                        <asp:Literal ID="litLoggedDate1" runat="server" Text='<%#Bind("DateLogged","{0:d}") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Bind("ID") %>' CommandName="ConvertCSI"  runat="server" Enabled="<%#CommandField()%>">Turn to CSI</asp:LinkButton>
                                </ItemTemplate>
                                    
                                </asp:TemplateField>
                               <%-- <asp:ButtonField Text=""  CommandName="ConvertCSI" 
                                            HeaderText="Status">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="True" />
                                </asp:ButtonField>--%>
                            </Columns>
                            <EmptyDataTemplate>
                                <div style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px; color: #754c23;
                                            padding-top: 10px; background-color: #f2f2f2; text-align: left">
                                    <asp:Label ID="lblGridViewNoData" runat="Server" Text="No data available.." />
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    
                    <asp:SqlDataSource ID="sourceGrid" runat="server"
        ConnectionString="<%$ ConnectionStrings:DBstring %>" SelectCommand="DEFFINITY_CUSTSAT_REPORT"
        SelectCommandType="StoredProcedure" >
        
    </asp:SqlDataSource>


    </asp:Panel>
   
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
GridResponsiveCss();
 </script> 
</asp:Content>

