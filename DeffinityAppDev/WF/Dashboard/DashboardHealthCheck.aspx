<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WF/MainTab.master" Inherits="DashboardHealthCheck1" Codebehind="DashboardHealthCheck.aspx.cs" %>

<%@ Register src="controls/DashboardTabs.ascx" tagname="DashboardTabs" tagprefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Dashboard%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.HealthCheck%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
    
    <uc1:DashboardTabs ID="DashboardTabs1" runat="server" />
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
      <div class="col-md-5">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlPortfolio" runat="server" Width="200px" AutoPostBack="true" 
        DataValueField="ID" DataTextField="Portfolio" DataSourceID="SqlDataSourceTitle2"
        onselectedindexchanged="ddlPortfolio_SelectedIndexChanged">
        </asp:DropDownList>
       <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_PermissionCustomer" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
            </div>
	</div>
	<div class="col-md-3">
          
	</div>
	<div class="col-md-4">
          
	</div>
</div>

<div>

</div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
           <asp:Label runat="server" SkinID="Loading"></asp:Label>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableViewState="false" />
   <%-- <div >
        Health Check Summary for <%=DateTime.Now.Date.ToString("dd/MM/yyyy") %>
    </div>--%>
    <asp:UpdatePanel ID="uPnlCheckListSummary" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                AllowPaging="True" DataSourceID="sqlGridfiller">
                <Columns>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Overall<br />RAG" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                        <ItemTemplate>
                            <asp:Label ID="imgRAG" runat="server" Text='<%#getRagUrl(Eval("OverAllStatus").ToString())%>'
                                ToolTip='<%#Eval("OverAllStatus").ToString() %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Health Check Title">
                        <ItemTemplate>
                            <asp:Label ID="lblHealthCheckTitle" runat="server" Text='<%#Eval("HealthTitle")%>' />
                        </ItemTemplate>
                        <ItemStyle Wrap="True" Width="250px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Health Check Item">
                        <ItemTemplate>
                            <asp:Label ID="lblHealthCheck" runat="server" Text='<%#Eval("HealthCheck")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Notes">
                        <ItemTemplate>
                            <asp:Literal ID="litNotes" runat="server" Text='<%#Eval("Notes")%>' />
                        </ItemTemplate>
                        <ControlStyle Width="230px"></ControlStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RAG" ItemStyle-Width="20px">
                        <ItemTemplate>
                            <asp:Label ID="imgRAG" runat="server" Text='<%#getRagUrl(Eval("Status").ToString())%>'
                                ToolTip='<%#Eval("Status").ToString() %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="clr">
    </div>
    <div class="form-group">
        <div class="col-md-12">
           <strong>Health Check Issue Summary</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
    <div class="form-group">
      <div class="col-md-5">
           <label class="col-sm-4 control-label">Health Check Title</label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlHealthCheckTitles" runat="server" AppendDataBoundItems="true"
                    DataSourceID="objHealthCheckDDLFiller" DataTextField="Title" DataValueField="ID"
                    ValidationGroup="Issues">
                    <asp:ListItem Text="Please Select.." Value="0" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rqdHealthCheckTitles" runat="server" ControlToValidate="ddlHealthCheckTitles"
                    ErrorMessage="Please select Health Check Title" Text="*" Display="Dynamic" ValidationGroup="Issues" />
                <asp:ObjectDataSource ID="objHealthCheckDDLFiller" runat="server" TypeName="DataHelperClass"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="LoadPortfolioHealthCheckTitle">
                      </asp:ObjectDataSource>
                  
            </div>
	</div>
	<div class="col-md-3">
            <asp:Button ID="btnView" runat="server" SkinID="btnView" OnClick="btnView_Click"
                    ValidationGroup="Issues" />
	</div>
	<div class="col-md-4">
          
	</div>
</div>

    <div class="tab_header_Bold">
        
    </div>
   
    <asp:UpdatePanel ID="updatePanelIssueSummary" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gridHealthCheckIssues" runat="server" AutoGenerateColumns="false"
                Width="100%" EmptyDataText="No issues logged" EnableViewState="false" AllowPaging="true"
                OnPageIndexChanging="gridHealthCheckIssues_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Literal ID="litDate" runat="server" Text='<%#getDate(Eval("IssueDate")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Issues">
                        <ItemTemplate>
                            <asp:Literal ID="litIssues" runat="server" Text='<%#Eval("Issues")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="clr">
    </div>

    <div class="form-group">
        <div class="col-md-12">
           <strong>Health Check History</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gridHealthChecks" runat="server" AutoGenerateColumns="False" Width="100%"
                EmptyDataText="No Items Found" EnableViewState="false" AllowPaging="true" AllowSorting="true"
                OnSorting="gridHealthChecks_Sorting" OnPageIndexChanging="gridHealthChecks_PageIndexChanging">
                <Columns>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Add Issue" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIssue" runat="server" EnableViewState="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PDF">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" SkinID="LinkPrint"  NavigateUrl='<%# "~/WF/Reports/HealthCheckItems.aspx?pdf=true&healthCheckID=" + Eval("ID").ToString()%>'
                                ID="linkPDF" Text="PDF" Target="_blank" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" SortExpression="DateRaised">
                        <ItemTemplate>
                            <asp:Literal ID="litDateRaised" Text='<%#Eval("DateRaised","{0:d}")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LocationName" HeaderText="Location" SortExpression="LocationName" />
                    <asp:BoundField DataField="HealthCheckTitle" HeaderText="Health Check" SortExpression="HealthCheckTitle" />
                    <asp:BoundField DataField="AssignedTeamName" HeaderText="Assigned Team" SortExpression="AssignedTeamName" />
                    <asp:TemplateField HeaderText="Overall<br/> RAG" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                        <ItemTemplate>
                            <asp:Label ID="imgRAG" runat="server" Text='<%#getRagUrl(Eval("Status").ToString())%>'
                                ToolTip='<%#Eval("Status").ToString() %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="clr">
    </div>


    <div class="form-group">
      <div class="col-md-5">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Issue%></label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtIssue" runat="server" Width="400px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="reqIssue" runat="server" Text="*" ErrorMessage="Issue cannot be blank"
        ControlToValidate="txtIssue" EnableViewState="false" />
   
            </div>
	</div>
	<div class="col-md-3">
           <asp:Button ID="btnUpdateIssue" runat="server" SkinID="btnUpdate"
        OnClick="btnUpdateIssue_OnClick" />
	</div>
	<div class="col-md-4">
         
	</div>
</div>
   
    

<div class="form-group">
      <div class="col-md-12">
           <asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="false" />
	</div>

</div>

    <asp:SqlDataSource ID="sqlGridfiller" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
        SelectCommand="HealthCheckListItemsSelectAllWithOutID_Status" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="PortfolioID" SessionField="PortfolioID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
GridResponsiveCss();
 </script> 

</asp:Content>

