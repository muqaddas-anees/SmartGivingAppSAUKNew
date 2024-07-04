<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Risks" Title="Risks" Codebehind="Risks.aspx.cs" %>

<%@ Register src="controls/DashboardTabs.ascx" tagname="DashboardTabs" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">

    <uc1:DashboardTabs ID="DashboardTabs1" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Dashboard%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    <%= Resources.DeffinityRes.Risks%>
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
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Country%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                        Width="220px">
                    </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSourceCountry" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                            SelectCommand="GetCountryByProgrameOrSubProgramme" SelectCommandType="StoredProcedure">
                            <SelectParameters>                            
                           <asp:Parameter Name="Program" Type="int32" DefaultValue="0" />   
                            <asp:Parameter Name="SubProgram" Type="int32" DefaultValue="0" />   
                        </SelectParameters>                    
		   </asp:SqlDataSource>
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
</div>

    <asp:Panel ID="Panle2" runat="server" Width="100%">
    
       <asp:GridView ID="GridView9" runat="server"  DataKeyNames="ID"
            width="100%" AutoGenerateColumns="False"             
            ShowFooter="True" DataSourceID="SqlDataSource7" AllowPaging="True" 
        onpageindexchanging="GridView9_PageIndexChanging" PageSize="15" EmptyDataText="No Records Found"  >
           <RowStyle />
            
            <Columns>  
           <asp:TemplateField HeaderStyle-CssClass="header_bg_l" Visible="false">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkHeading" onclick="selectAll(this.checked,this.id);" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkItem" runat="server" />
                        <asp:HiddenField runat="server" ID ="HID" Value='<%# Bind("ID") %>' /> 
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle  HorizontalAlign="Center" Width="20px"/>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" Visible="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" SkinID="BtnLinkUpdate"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" SkinID="BtnLinkCancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" SkinID="BtnLinkEdit"></asp:LinkButton>
                    </ItemTemplate>
                  <ItemStyle Width="40px" />
                </asp:TemplateField>                               
                <asp:HyperLinkField DataNavigateUrlFields="project" DataNavigateUrlFormatString="~/WF/Projects/ProjectRisks.aspx?project={0}" DataTextField="ProjectReference" HeaderStyle-Width="20" HeaderText="<%$ Resources:DeffinityRes,ProjectRef%>" ItemStyle-Width="100">
                <HeaderStyle Width="20px" />
                <ItemStyle Width="100px" />
                </asp:HyperLinkField>
                <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, ProjectTitle%>">
                    <ItemTemplate>
                        <asp:Label ID="lblProjectTitle1" runat="server" Text='<%# Bind("ProjectTitle") %>' Width="135px"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                    <ItemStyle HorizontalAlign="Left" Width="230px" />
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes, RiskDescription%>">
                    <ItemTemplate>                     
                    <asp:Label ID="lblRiskDescription" Width="115px" runat="server" Text='<%# Bind("RiskDescription") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:TemplateField>               
                <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes, RiskCoordinator%>">
                    <ItemTemplate>
                    <asp:Label ID="lblCo_ordinatorName" Width="115px" runat="server" Text='<%# Bind("Co_ordinatorName") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:TemplateField>
                                
                <asp:TemplateField  HeaderStyle-Width="10%"  HeaderText="<%$ Resources:DeffinityRes, DateRaised%>">
                <ItemTemplate>
                        <asp:Label ID="lblSdate" runat="server" Text='<%# Bind("DateRaised","{0:d}") %>' ></asp:Label>                        
                </ItemTemplate>
                    <HeaderStyle Width="10%" />
                <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes, ReportStatus%>">
                    <ItemTemplate>
                     
                    <asp:Label ID="lblReportStatus" Width="40px" runat="server" Text='<%# Bind("ReportStatus") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:TemplateField>                
                
                <asp:TemplateField   HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, Probability%>">
                    <ItemTemplate>                     
                    <asp:Label ID="lblProbability" Width="30px" runat="server" Text='<%# Bind("Probability") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                  <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:TemplateField>                   
                                                          
                <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes, DegreeofImpact%>">
                    <ItemTemplate>                     
                    <asp:Label ID="lblDegreeofImpact" Width="30px" runat="server" Text='<%# Bind("DegreeofImpact") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes, Exposure%>" SortExpression="Exposure">
                    <ItemTemplate>                     
                    <asp:Label ID="lblExposure" Width="30px" runat="server" Text='<%# Bind("Exposure") %>'   ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:TemplateField>                
                
                <asp:TemplateField  HeaderStyle-Width="5%" HeaderText="<%$ Resources:DeffinityRes, RAG %>">                  
                <ItemTemplate>
                       <asp:Image ID="Image1" runat="server" ImageUrl='<%#LoadRagSTatus(Eval("NextReviewDate").ToString())%>'/>  
                    </ItemTemplate>                   
                    <HeaderStyle Width="5%" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:TemplateField>                                                          
                <asp:TemplateField  HeaderStyle-Width="10%"   HeaderText="<%$ Resources:DeffinityRes, NextReviewDate %>">                  
                <ItemTemplate>
                        <asp:Label ID="lblNextReviewDate" runat="server" Text='<%# Bind("NextReviewDate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>                   
                    <HeaderStyle Width="10%" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>                          
                 
            </Columns>   
            
   </asp:GridView>   
   <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DBstring %>" 
        SelectCommand="DEFFINITY_IPD_PROJRISKS_DashBoard" 
        SelectCommandType="StoredProcedure" >

        <SelectParameters>                
       <%-- <asp:SessionParameter SessionField="ProgrammeID" DefaultValue="0" Type="int32" Name="Program" /> --%> 
       <asp:ControlParameter PropertyName="SelectedValue" Name="Portfolio" DefaultValue="0" ControlID="ddlPortfolio" /> 
       <asp:SessionParameter SessionField="UID" Type="Int32" Name="UID" />
        <asp:ControlParameter ControlID="ddlProjGroups" DefaultValue="0" PropertyName="SelectedValue" Name="Program"/>     
        <asp:ControlParameter ControlID="ddlProjects" DefaultValue="0" PropertyName="SelectedValue" Name="ProjectRef"/>    
        <asp:ControlParameter ControlID="ddlCountry" DefaultValue="0" PropertyName="SelectedValue" Name="CountryID"/>                                
        <asp:ControlParameter ControlID="ddlsubprogram" DefaultValue="0" PropertyName="SelectedValue" Name="SubProgram"/>    
        </SelectParameters>          
        </asp:SqlDataSource>
</asp:Panel>

</asp:Content>

