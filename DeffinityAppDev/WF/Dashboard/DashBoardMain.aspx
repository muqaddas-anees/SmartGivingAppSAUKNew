<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DashBoard3" Title="Project DashBoard" Codebehind="DashBoardMain.aspx.cs" %>


<%@ Register Src="controls/DashboardTabs.ascx" TagName="DashboardTabs" TagPrefix="uc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Dashboard%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
   <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Programme%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlProjGroups" runat="server" DataSourceID="SqlDataSource8"
                                    Width="200px" DataTextField="OperationsOwners" DataValueField="ID" AutoPostBack="true" ClientIDMode="Static"
                                    OnSelectedIndexChanged="ddlProjGroups_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                    SelectCommand="Project_AssignedProgramme" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.SubProgramme%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlsubprogram" runat="server" Width="150px" DataSourceID="SqlDataSourcesubprogram"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlsubprogram_SelectedIndexChanged"
                                    DataValueField="ID" DataTextField="OPERATIONSOWNERS" ClientIDMode="Static">
                                    <%--<asp:ListItem Text=" Please select..." Value="0" Selected="True"></asp:ListItem> --%>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourcesubprogram" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                    SelectCommand="Project_AssignedSubProgramme" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                                        <asp:ControlParameter ControlID="ddlProjGroups" DefaultValue="0" Name="PROGRAMMEID"
                                            PropertyName="SelectedValue" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.SelectProject%></label>
           <div class="col-sm-9">
               <asp:DropDownList runat="server" ID="ddlProjects" ClientIDMode="Static" DataSourceID="SqlDataSource9"
                                    DataTextField="ProjectTitle" DataValueField="ProjectReference" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged" Width="300px">
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
</div>

                  
<div class="form-group">
      <div class="col-md-4">
           <div id="chart1">
                                </div>
	</div>
	<div class="col-md-4">
           <div id="chart2">
                                </div>
	</div>
	<div class="col-md-4">
            <div id="chart3">
                                </div>
	</div>
</div>
                    
                   <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.ProjectSummary%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="DropDownListStatus" runat="server" Width="200px" DataTextField="Status"
                                    DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="DropDownListStatus_SelectedIndexChanged">
                                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
	<div class="col-md-4">
          
	</div>
</div>
     <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Auto">
                                <asp:GridView ID="gviewclientprojectstatus" runat="server" AutoGenerateColumns="False"
                                    Width="100%" GridLines="None" EmptyDataText="<%$ Resources:DeffinityRes,NoRecordsExists%>"
                                    AllowPaging="True" PageSize="20" AllowSorting="True" OnPageIndexChanging="gviewclientprojectstatus_PageIndexChanging"
                                    OnRowDataBound="gviewclientprojectstatus_RowDataBound" OnSorting="gviewclientprojectstatus_Sorting">
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="rdbGVRow1" runat="server" />
                                                <asp:HiddenField ID="HID" runat="server" Value='<%# Bind("ProjectReference") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="<%$ Resources:DeffinityRes,ID%>" Visible="False" />
                                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectReference%>" SortExpression="ProjectReference">
                                            <ItemStyle Width="50px" />
                                            <HeaderStyle CssClass="header_bg_l" />
                                            <ItemTemplate>
                                                <%--<asp:HyperLink ID="hpref" runat="server" Text="<%# Bind("ProjectReference") %>" NavigateUrl="~/BuildProject.aspx?ProjectReference="+<%# Bind("ProjectReference") %>+"--%>
                                                <a href="../../WF/Projects/ProjectOverviewV4.aspx?project=<%# DataBinder.Eval(Container.DataItem, "ProjectReference")%>">
                                                    <%# DataBinder.Eval(Container.DataItem, "Project")%></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <a href="#" onclick="window.open('LoginJournal.aspx?project=<%#DataBinder.Eval(Container.DataItem,"ProjectReference")%>',
        null,'height=450 width=500 scrollbars=yes')">
                                                    <i class="fa fa-history"></i></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ProjectTitle" HeaderText="<%$ Resources:DeffinityRes,ProjectTitle%>"
                                            SortExpression="ProjectTitle" />
                                        <asp:BoundField DataField="PortfolioName" HeaderText="<%$ Resources:DeffinityRes,Customer%>"
                                            SortExpression="PortfolioName" />
                                        <asp:BoundField DataField="ProjectStatusName" HeaderText="<%$ Resources:DeffinityRes,Status%>"
                                            SortExpression="ProjectStatusName" />
                                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TargetDate%>" SortExpression="ProjectEndDate">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ProjectEndDate", "{0:d}")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SiteName" HeaderText="<%$ Resources:DeffinityRes,SiteName%>"
                                            SortExpression="SiteName" Visible="false" />
                                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Variation%>" ItemStyle-HorizontalAlign="Right"
                                            Visible="false">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "variation")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href="#" onclick="window.open('WF/Projects/TaskItemsPopup.aspx?project=<%#DataBinder.Eval(Container.DataItem,"ProjectReference")%>&ragstatus=red',null,'height=450 width=750 scrollbars=yes')">
                                                    <img src="media/indcate_red.png" border="0" alt="<%$ Resources:DeffinityRes,Red%>"
                                                        runat="server" id="imgRed" visible='<%#getImage(DataBinder.Eval(Container.DataItem, "StatusRed").ToString())%>' />
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href="#" onclick="window.open('WF/Projects/TaskItemsPopup.aspx?project=<%#DataBinder.Eval(Container.DataItem,"ProjectReference")%>&ragstatus=amber',null,'height=450 width=750 scrollbars=yes')">
                                                    <img src="images/indcate_yellow.png" border="0" alt="<%$ Resources:DeffinityRes,Amber%>"
                                                        runat="server" id="imgAmber" visible='<%#getImage(DataBinder.Eval(Container.DataItem, "StatusAmber").ToString())%>' /></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Priority%>">
                                            <ItemTemplate>
                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#LoadPriority(Eval("Priority").ToString())%>'
                                                    ToolTip='<%# Bind("Priority")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Over Budget">
                                            <ItemTemplate>
                                                <asp:Image ID="imgOverBudget" runat="server" ImageUrl='<%#LoadOverBudget((double)Eval("OverBudget"))%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="5px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Overdue Tasks">
                                            <ItemTemplate>
                                                <a href="#" onclick="window.open('../../WF/Projects/OverdueTaskItems.aspx?project=<%#DataBinder.Eval(Container.DataItem,"ProjectReference")%>',null,'height=450 width=750 scrollbars=yes')">
                                                    <%#DataBinder.Eval(Container.DataItem,"OverdueTasks")%>
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle Width="5px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Late">
                                            <ItemTemplate>
                                                <asp:Image ID="imgLate" runat="server" ImageUrl='<%#LoadLate((int)Eval("Late"))%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="5px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:HyperLinkField DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Projects/ProjectOverviewV4.aspx?project={0}"
                                            Text="&lt;img src='media/ico_gantts.png' border='0'/&gt;" Target="_blank" HeaderText="<%$ Resources:DeffinityRes,Gantt%>"
                                            Visible="false">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:HyperLinkField>
                                        <asp:HyperLinkField DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Reports/Tasks.aspx?Project={0}"
                                            Text="&lt;img src='media/ico_pro_task.png' border='0'/&gt;" Target="_blank" HeaderText="<%$ Resources:DeffinityRes,ProjectTasks%>"
                                            Visible="true">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:HyperLinkField>
                                        <asp:HyperLinkField DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Reports/InvoiceSummary.aspx?Project={0}"
                                            Text="&lt;img src='media/ico_invoice_sumamry.png' border='0'/&gt;" Target="_blank"
                                            HeaderText="<%$ Resources:DeffinityRes,InvoiceSummary%>" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:HyperLinkField>
                                        <asp:HyperLinkField DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Reports/RiskReport.aspx?Project={0}"
                                            Text="&lt;img src='images/icon_risk_report.png' border='0'/&gt;" Target="_blank"
                                            HeaderText="<%$ Resources:DeffinityRes,RiskIssuesReport%>" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                                        </asp:HyperLinkField>
                                        <asp:HyperLinkField DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Reports/ProjectReport.aspx?Project={0}"
                                            Text="&lt;img src='images/icon_pro_report.png' border='0'/&gt;" Target="_blank"
                                            HeaderText="<%$ Resources:DeffinityRes,ProjectReport%>" Visible="False">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:HyperLinkField>
                                        <asp:HyperLinkField Text="&lt;img src='media/ico_timesheet.png' border='0'/&gt;"
                                            DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Reports/Timeandexpenses.aspx?Project={0}"
                                            Target="_blank" HeaderText="<%$ Resources:DeffinityRes,TimeandExpenses%>" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:HyperLinkField>
                                        <asp:HyperLinkField Text="<%$ Resources:DeffinityRes,VarianceReport%>" DataNavigateUrlFields="ProjectReference"
                                            Visible="False" />
                                        <asp:HyperLinkField HeaderStyle-CssClass="header_bg_r" Visible="false" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="50px" HeaderText="<%$ Resources:DeffinityRes,ManageInvoices%>"
                                            Text="&lt;img src='media/ico_invoicing.png' border='0'/&gt;" DataNavigateUrlFields="ProjectReference"
                                            DataNavigateUrlFormatString="~/WF/Projects/ProjectFinancials.aspx?Project={0}&Invoice=Invoice">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:HyperLinkField>



                                       <%-- <asp:BoundField HeaderText="Project Value" DataField="<%#GetProjectValue(Eval("ProjectReference").ToString())%>" DataFormatString="{0:F2}" />--%>


                                        <asp:BoundField DataField="ProjectFee" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Project Fee">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ActualCost" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Actual Costs to Date">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalVariations" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Total Variations">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                     <asp:TemplateField HeaderText="Total Project Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalProjectValue" runat="server" Text='<%# totalprojectvalue((double)Eval("ProjectFee"),(double)Eval("TotalVariations"))%>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SavingstoDate" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Savings to Date">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                          <asp:TemplateField HeaderText="Forecast cost to completion" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblForeCosttoCompletion" runat="server" Text='<%# Bind("Priority")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField HeaderText="Savings to Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSavingstoDate" runat="server" Text='<%#GetSavingstoDate(Eval("ProjectReference").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="ProgrammeName" HeaderText="Programme" Visible="false" />
                                        <asp:BoundField DataField="SubProgrammeName" HeaderText="Sub Programme" Visible="false" />
                                        <asp:BoundField DataField="CustomerReference" HeaderText="Customer PO Number" SortExpression="CustomerReference" />
                                        <asp:TemplateField HeaderText="PO Days Remaining" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="Label3" runat="server" Text='<%#ReturnDays(DataBinder.Eval(Container.DataItem,"Days").ToString())%>'></asp:Label>--%>
                                                <asp:Label ID="Label3" runat="server" Visible='<%#POVisible(DataBinder.Eval(Container.DataItem,"POCheck").ToString())%>'
                                                    ForeColor='<%#foreColor(DataBinder.Eval(Container.DataItem,"Days").ToString())%>'
                                                    Text='<%#ReturnDays(DataBinder.Eval(Container.DataItem,"Days").ToString(),DataBinder.Eval(Container.DataItem,"CustomerReference").ToString(),DataBinder.Eval(Container.DataItem,"DDays").ToString(),DataBinder.Eval(Container.DataItem,"TotalHrs").ToString())%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="% PO Remaining" ItemStyle-HorizontalAlign="Right">
                                            <HeaderStyle CssClass="header_bg_r" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCR" runat="server" Text='<%#Bind("CustomerReference")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblDDays" runat="server" Text='<%#Bind("DDays")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblTotalDays" runat="server" Text='<%#Bind("TotalHrs")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblDyas" runat="server" Text='<%#Bind("Days")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblProgress" runat="server" Text="" Visible='<%#POVisible(DataBinder.Eval(Container.DataItem,"POCheck").ToString())%>'></asp:Label>
                                                <asp:Label ID="lblPer" runat="server" Text='<%# Bind("per")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Comments%>" Visible="false">
                                            <HeaderStyle CssClass="header_bg_r" />
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Project")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                               
                            </asp:Panel>
                    
    
    <%-- <script src="Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
    <script src="https://www.google.com/jsapi" type="text/javascript"></script>
    <script type="text/javascript">
        // Global variable to hold data
        //  google.load("visualization", "1.1", { packages: ["bar"] });
        google.load('visualization', '1.1', { packages: ['corechart'] });

        //Budget vs Actual - chart
        google.setOnLoadCallback(drawchart1);
        //Variations for Live Projects - chart
        google.setOnLoadCallback(drawchart2);
        //Project Performance  - chart
        google.setOnLoadCallback(drawchart3);

        function drawchart1() {
            var projectGroup = $('#ddlProjGroups').val();
            var projectId = $('#ddlProjects').val();
            var subProgrammeId = $('#ddlsubprogram').val();

            $(function () {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    url: 'DashBoardMain.aspx/GetChartData1',
                    data: "{projectGroup:'" + projectGroup + "',projectId:'" + projectId + "',subProgrammeId:'" + subProgrammeId + "'}",
                    success:
function (response) {
    //drawchart1(response.d);
    var dataValues = response.d;
    var data1 = new google.visualization.DataTable();
    data1.addColumn('string', 'Column Name');

    data1.addColumn('number', 'Budget');
    data1.addColumn('number', 'Actual Cost to Date');
    for (var i = 0; i < dataValues.length; i++) {
        data1.addRow([dataValues[i].ProjectReference, dataValues[i].Budget, dataValues[i].ActualCost]);
    }
    var options1 = {
        width: 350,
        height: 320,
        title: "Budget vs Actual", titleTextStyle: { color: 'gray', fontSize: '13' },
        legend: { position: 'bottom', maxLines: 3 }


    }

    new google.visualization.ColumnChart(document.getElementById('chart1')).
        draw(data1, options1);


},

                    error: function () {
                        alert("Error loading data.Please try again.");
                    }
                });
            });

        }




        function drawchart2() {
            var projectGroup = $('#ddlProjGroups').val();
            var projectId = $('#ddlProjects').val();
            var subProgrammeId = $('#ddlsubprogram').val();

            $(function () {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    url: 'DashBoardMain.aspx/GetChartData2',
                    data: "{projectGroup:'" + projectGroup + "',projectId:'" + projectId + "',subProgrammeId:'" + subProgrammeId + "'}",
                    success:
function (response) {
    var dataValues = response.d;
    var data2 = new google.visualization.DataTable();
    data2.addColumn('string', 'Column Name');
    data2.addColumn('number', 'Budget Cost');
    data2.addColumn('number', 'Variances');
    for (var i = 0; i < dataValues.length; i++) {
        data2.addRow([dataValues[i].ProjectReference, dataValues[i].Budget, dataValues[i].Variences]);
    }
    var options2 = {
        width: 350,
        height: 320,
        title: "Variations for Live Projects", titleTextStyle: { color: 'gray', fontSize: '13' },
        legend: { position: 'bottom', maxLines: 3 },
        bar: { groupWidth: '75%' },
        isStacked: true
    }

    new google.visualization.ColumnChart(document.getElementById('chart2')).
draw(data2, options2);
},

                    error: function () {
                        alert("Error loading data.Please try again.");
                    }
                });
            })



        }

        function drawchart3() {
            var projectGroup = $('#ddlProjGroups').val();
            var projectId = $('#ddlProjects').val();
            var subProgrammeId = $('#ddlsubprogram').val();

            $(function () {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    url: 'DashBoardMain.aspx/GetChartData3',
                    data: "{projectGroup:'" + projectGroup + "',projectId:'" + projectId + "',subProgrammeId:'" + subProgrammeId + "'}",
                    success:
function (response) {
    var dataValues = response.d;
    var data3 = new google.visualization.DataTable();
    data3.addColumn('string', 'Column Name');
    data3.addColumn('number', 'GREEN');
    data3.addColumn('number', 'AMBER');
    data3.addColumn('number', 'RED');
    for (var i = 0; i < dataValues.length; i++) {
        data3.addRow([dataValues[i].ProjectReference, dataValues[i].Green, dataValues[i].Amber, dataValues[i].Red]);
    }
    var options3 = {
        width: 350,
        height: 320,
        title: "Project Performance", titleTextStyle: { color: 'gray', fontSize: '13' },
        legend: { position: 'bottom', maxLines: 3 },
        colors: ['green', 'yellow', 'red'],
        bar: { groupWidth: '75%' }

    }

    new google.visualization.ColumnChart(document.getElementById('chart3')).
draw(data3, options3);
},

                    error: function () {
                        alert("Error loading data.Please try again.");
                    }
                });
            })



        }


    </script>


               
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
   
    <uc1:DashboardTabs ID="DashboardTabs1" runat="server" />
</asp:Content>
