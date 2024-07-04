<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="false" Inherits="ProgrammeDashboard"
    Title="Programme" Codebehind="Programme.aspx.cs" %>

<%@ Register Src="controls/ProgramTitle.ascx" TagName="ProgrammeName" TagPrefix="uc2" %>
<%@ Register Src="controls/DashboardTabs.ascx" TagName="DashboardTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/ProgrammeSubTab.ascx" TagName="ProgrammeSubTab" TagPrefix="uc1" %>
<%@ Register Src="controls/ProgrammeRagDisplayV2.ascx" TagName="ProgrammeRAG" TagPrefix="uc1" %>
<%@ Register Src="controls/ProgrammeWorkstream.ascx" TagName="ProgrammeWorkStream" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Dashboard%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Programme%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:DashboardTabs ID="DashboardTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <script  type="text/javascript">
         //activeTab('Programme');
</script>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <asp:Label runat="server" SkinID="Loading"></asp:Label>
            
        </ProgressTemplate>
    </asp:UpdateProgress>
     
   <%-- <script src="https://www.google.com/jsapi" type="text/javascript"></script>--%>
                <script type="text/javascript" src="https://www.google.com/jsapi?autoload={'modules':[{'name':'visualization','version':'1.1','packages':['timeline']}]}"></script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
   <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.SelectAProgramme%></label>
           <div class="col-sm-9">
                  <asp:DropDownList ID="ddlprojectProgramme" runat="server" SkinID="ddl_70" OnSelectedIndexChanged="ddlprojectPortfolio_SelectedIndexChanged" ClientIDMode="Static"
                                AutoPostBack="True" DataSourceID="SqlDataSource8"  DataTextField="OperationsOwners" DataValueField="ID"></asp:DropDownList>
                            
                             <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_AssignedProgramme" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
	
</div>

    <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.SelectSubProgramme%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlSubProgramme" runat="server" ClientIDMode="Static"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlSubProgramme_SelectedIndexChanged" SkinID="ddl_70"></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlselectview" runat="server" AutoPostBack="True" Visible="false"
                                OnSelectedIndexChanged="ddlselectview_SelectedIndexChanged">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">My Programme</asp:ListItem>
                            </asp:DropDownList>
            </div>
	</div>

</div>

     <div>
               
            </div>
            <uc1:ProgrammeSubTab ID="ProgrammeSubTab1" runat="server" />
               <br />
            <div>
                <%--  <asp:DropDownList ID="ddlHidePanel" runat="server" 
        onselectedindexchanged="ddlHidePanel_SelectedIndexChanged" 
        AutoPostBack="True">
    <asp:ListItem Selected="True" Value="1">Financial Summary</asp:ListItem>
    <asp:ListItem  Value="2">Programme Summary</asp:ListItem>
    <asp:ListItem  Value="3">Benefits Tracking Report</asp:ListItem>
    </asp:DropDownList>--%>
            </div>
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
            </div>
            <asp:Panel ID="PnlReports" Visible="false" runat="server"></asp:Panel>
   
           <div class="form-group">
                         <div class="col-md-6">
                              <asp:Label runat="server" ID="lblSelectReport" Text="<%$ Resources:DeffinityRes, SelectReport%>" CssClass="col-sm-3 control-label"></asp:Label>
                              <div class="col-sm-9">
                                   <asp:DropDownList Visible="false" SkinID="ddl_70" ID="ddlHidePanel" runat="server" ClientIDMode="Static"
                                                                       OnSelectedIndexChanged="ddlHidePanel_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="1">Financial Summary</asp:ListItem>
                                        <asp:ListItem Value="2">Programme Summary</asp:ListItem>
                                        <asp:ListItem Value="3">Benefits Tracking Report</asp:ListItem>
                                   </asp:DropDownList>
                              </div>
                         </div>
           </div>

    <asp:Panel ID="PnlSummaryGraph" Visible="false" runat="server" Width="100%">
                     <div class="form-group">
                           <div class="col-md-6">
                                <div class="form-group">
                                           <div class="col-md-12">
                                                      <strong><%= Resources.DeffinityRes.Summary%> </strong> 
                                                      <hr class="no-top-margin" />
                                           </div>
                               </div>
                                <div class="form-group">
                                   <div class="col-md-12">
                                      <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.ProgrammeOwner%></label>
                                    <div class="col-sm-6 control-label">
                                         <asp:Label ID="lblportfolioowner" runat="server" Font-Bold="true" />
                                    </div>
                                        </div>
                               </div>
                                <div class="form-group">
                                   <div class="col-md-12">
                                      <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.LiveProject%></label>
                               
                                    <div class="col-sm-6 control-label">
                                           <asp:Label ID="lblliveProject" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                           </div>
                               </div>
                                <div class="form-group">
                                   <div class="col-md-12">
                                      <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.TotalValofLiveProjects%></label>
                                  
                                    <div class="col-sm-6 control-label">
                                          <asp:Label runat="server" ID="lblTotalLiveVal" Font-Bold="true"></asp:Label>
                                    </div>
                                        </div>
                               </div>
                                <div class="form-group">
                                   <div class="col-md-12">
                                      <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.PendingProject%></label>
                                  
                                    <div class="col-sm-6 control-label">
                                             <asp:Label ID="lblpendingProject" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                        </div>
                               </div>
                                <div class="form-group">
                                   <div class="col-md-12">
                                      <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.TotalValofPendingProjects%></label>
                              
                                    <div class="col-sm-6 control-label">
                                           <asp:Label runat="server" ID="lblTotalPendingVal" Font-Bold="true"></asp:Label>
                                    </div>
                                            </div>
                               </div>
                                <div class="form-group">
                                   <div class="col-md-12">
                                      <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.TotalProjectValue%></label>
                             
                                    <div class="col-sm-6 control-label">
                                            <asp:Label ID="lbltotpvalue" runat="server" Font-Bold="true"></asp:Label>
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

                             <div id="DivProgramChart"></div>


                              <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                    SelectCommand="DEFFINITY_PROJECTS_PROGRAMME_SUMMURY" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter SessionField="ProgrammeID" DefaultValue="0" Type="int32" Name="PROGRAMME" />
                                    </SelectParameters>
                                </asp:SqlDataSource>

                         </div>
                     </div>
            </asp:Panel>

     <asp:Panel ID="PnlBenefitsTracking" runat="server" Visible="false" Width="100%" >
                <%--<uc1:ProjectChart ID="BenefitsTracking" runat="server" ProgrammeID="0" />--%>

         <div class="form-group">
                   <div class="col-md-4">
           <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.BenefitType%></label>
           <div class="col-sm-6">
                <asp:DropDownList ID="ddlType" SkinID="ddl_90" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" ClientIDMode="Static"
                                                        AutoPostBack="True"></asp:DropDownList>
            </div>
	</div>
	               <div class="col-md-4">
           <label class="col-sm-6 control-label">Report Type</label>
           <div class="col-sm-6">
               <asp:DropDownList ID="ddlReportType" SkinID="ddl_90" runat="server" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                                    <asp:ListItem Selected="True" Value="0">Please select...</asp:ListItem>
                                    <asp:ListItem Value="1">By Country Report</asp:ListItem>
                                    <asp:ListItem Value="2">Project Summary</asp:ListItem>
               </asp:DropDownList>
            </div>
	</div>
                   <div class="col-md-4">
                          <asp:Button ID="btn_ViewReport" runat="server" Text="View Report"
                                                                       OnClick="btn_ViewReport_Click" Visible="false"  />   
                          <label class="col-sm-6 control-label">  <asp:Label ID="lblCoutry" runat="server" Visible="false"><%= Resources.DeffinityRes.Country%></asp:Label></label>
                           <div class="col-sm-6 form-inline">
                                <asp:DropDownList ID="ddlCountry" SkinID="ddl_70" runat="server" Visible="false" ClientIDMode="Static"></asp:DropDownList>
                                <asp:Button ID="ingSearch" runat="server" Text="View" OnClick="ingSearch_Click" Visible="false"  />
                             
                           </div>
                    </div>
         </div>
         <div class="form-group">
        <div class="col-md-12">
           <strong> Project Benefit </strong> 
            <hr class="no-top-margin" />
            </div>
         </div>
               <div class="form-group">
                   <div id="DivBenfitTrackingReport"></div>
                </div>
                <br />
                <br />
                <div>
                    <asp:GridView ID="grdProjectBenefit" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                        ShowHeader="true" OnRowCommand="grdProjectBenefit_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Project" HeaderStyle-CssClass="header_bg_l">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk" CommandName="View" CommandArgument='<%# Bind("ProjectRef")%>'
                                                                       runat="server" Text='<%# Bind("ProjectName")%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProjectTitle" HeaderText="Project Title" HeaderStyle-Width="200px" />
                            <%-- <asp:BoundField DataField="LE"  HeaderText="Latest Reported Entry" DataFormatString="{0:N2}"/>   --%>
                            <asp:BoundField DataField="Adate" DataFormatString="{0:MM/yyyy}" HeaderText="Report Date" />
                            <asp:BoundField DataField="LE" HeaderText="Actual to Date" DataFormatString="{0:N2}"
                                ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Nextpp" HeaderText="Planned to Date" DataFormatString="{0:N2}"
                                ItemStyle-HorizontalAlign="Right" />
                            <%-- <asp:BoundField DataField="pdate" DataFormatString="{0:MM/yyyy}" HeaderText="Planned" />--%>
                            <asp:BoundField DataField="Target" DataFormatString="{0:N2}" HeaderText="Target to End"
                                ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="tdate" DataFormatString="{0:MM/yyyy}" HeaderText="End Date" />
                            <asp:TemplateField HeaderText="Benefits Schedule " HeaderStyle-CssClass="header_bg_r">
                                <ItemStyle Width="50px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk1" CommandName="Link" CommandArgument='<%# Bind("ProjectRef")%>'
                                                                                runat="server" Text="View"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

    <asp:Panel ID="PnlProgrammeSummary" runat="server" Width="100%" >
                <%--<uc2:ProjectChart2 runat="server" ID="PnlProgrammeSummary1" ProgrammeID="0" />--%>
                <div>

                  <div class="form-group">
                              <div class="col-md-12">
                                   <strong>  Programme Status Summary Report </strong> 
                                    <hr class="no-top-margin" />
                              </div>
                  </div> 
                    <div id="DivFinancialSummary"></div>
                </div>
            </asp:Panel>

    <asp:Panel ID="PnlLiveProjects" runat="server" Visible="false" Width="100%">
                
                <script type="text/javascript">
                    google.charts.load('43', { packages: ['timeline'] });
                    //google.charts.setOnLoadCallback(drawChart);

                    Date.prototype.toDDMMYYYYString = function () { return isNaN(this) ? 'NaN' : [this.getDate() > 9 ? this.getDate() : '0' + this.getDate(), this.getMonth() > 8 ? this.getMonth() + 1 : '0' + (this.getMonth() + 1), this.getFullYear()].join('/') }
                    Date.prototype.toMMDDYYYYString = function () { return isNaN(this) ? 'NaN' : [this.getMonth() > 8 ? this.getMonth() + 1 : '0' + (this.getMonth() + 1), this.getDate() > 9 ? this.getDate() : '0' + this.getDate(), this.getFullYear()].join('/') }
                    Date.isLeapYear = function (year) {
                        return (((year % 4 === 0) && (year % 100 !== 0)) || (year % 400 === 0));
                    };

                    Date.getDaysInMonth = function (year, month) {
                        return [31, (Date.isLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month];
                    };

                    Date.prototype.isLeapYear = function () {
                        return Date.isLeapYear(this.getFullYear());
                    };

                    Date.prototype.getDaysInMonth = function () {
                        return Date.getDaysInMonth(this.getFullYear(), this.getMonth());
                    };

                    Date.prototype.addMonths = function (value) {
                        var n = this.getDate();
                        this.setDate(1);
                        this.setMonth(this.getMonth() + value);
                        this.setDate(Math.min(n, this.getDaysInMonth()));
                        return this;
                    };
                    // Global variable to hold data
                    //google.load("visualization", "1.1", { packages: ["timeline"] });

                    google.setOnLoadCallback(drawchart1);
                    google.setOnLoadCallback(drawchart2);
                    function finder(cmp, arr, attr) {
                        var val = arr[0][attr];
                        for (var i = 1; i < arr.length; i++) {
                            val = cmp(val, arr[i][attr])
                        }
                        return val;
                    }
                    function getHighestVal(data, index) {
                        $.each(data, function (i, v) {
                            thisVal = v[index];
                            max = (max < thisVal) ? thisVal : max;
                        });
                        return max;
                    }
                    function getLowestVal(data, index) {
                        $.each(data, function (i, v) {
                            thisVal = v[index];
                            min = (min > thisVal) ? thisVal : min;
                        });
                        return min;
                    }

                    function drawchart1() {
                        //var container = document.getElementById('chart1');
                        //var chart = new google.visualization.Timeline(container);
                        var projectGroup = 0;// $('#ddlProjGroups').val();
                        var projectId = 0;// $('#ddlProjects').val();
                        var subProgrammeId = 0;// $('#ddlSubProgramme').val();

                        $(function () {
                            $.ajax({
                                type: 'POST',
                                dataType: 'json',
                                contentType: 'application/json',
                                url: 'Programme.aspx/GetChartData1',
                                data: "{projectGroup:'" + projectGroup + "',projectId:'" + projectId + "',subProgrammeId:'" + subProgrammeId + "'}",
                                success:
            function (response) {
                var dataValues = response.d;
                var data1 = new google.visualization.DataTable();
                data1.addColumn({ type: 'string', id: 'Project' });
                data1.addColumn({ type: 'date', id: 'start' });
                data1.addColumn({ type: 'date', id: 'end' });

                var minyear = 0;
                var minmonth = 0;
                var maxyear = 0;
                var maxmonth = 0;
                for (var i = 0; i < dataValues.length; i++) {
                    if (i == 0) {
                        minyear = dataValues[i].syear;
                        minmonth = dataValues[i].smonth;
                    }
                    minyear = (minyear > dataValues[i].syear) ? dataValues[i].syear : minyear;
                    minmonth = (minmonth > dataValues[i].smonth) ? dataValues[i].smonth : minmonth;
                    maxyear = (maxyear < dataValues[i].eyear) ? dataValues[i].eyear : maxyear;
                    maxmonth = (maxmonth < dataValues[i].emonth) ? dataValues[i].emonth : maxmonth;

                    data1.addRow([dataValues[i].Project, new Date(dataValues[i].syear, dataValues[i].smonth, dataValues[i].sday), new Date(dataValues[i].eyear, dataValues[i].emonth, dataValues[i].eday)]);
                }
                //if (minyear == maxyear)
                //{
                //    var mdef = maxmonth - minmonth
                //    if(mdef < 12)
                //    {
                //        minmonth = 1
                //        maxmonth = 12
                //    }

                //}
                var mindate = new Date(minyear, minmonth, 0)
                mindate = mindate.addMonths(6);
                var maxdate = new Date(maxyear, maxmonth, 0)
                maxdate = maxdate.addMonths(6);

                debugger;
                var options = {
                    timeline: { showRowLabels: true, groupByRowLabel: true },
                    avoidOverlappingGridLines: false,
                    hAxis: {
                        format: "MMM y",
                        minValue: mindate,
                        maxValue: maxdate,
                        gridlines: {
                            count: 12,
                        }
                    }

                };
                if (dataValues.length != 0) {
                    new google.visualization.Timeline(document.getElementById('chart1')).
                        draw(data1, options);
                    //chart.draw(data1, options);
                }
                else {
                    $('#chart1').hide();
                }

                //var cnt = dataValues.length;

                //$('#chart1').height('')


            },
                                error: function () {
                                    alert("Error loading data.Please try again.");
                                }
                            });
                        });

                    }
                    function drawchart2() {
                        var projectGroup = 0;// $('#ddlProjGroups').val();
                        var projectId = 0;// $('#ddlProjects').val();
                        var subProgrammeId = 0;// $('#ddlSubProgramme').val();

                        $(function () {
                            $.ajax({
                                type: 'POST',
                                dataType: 'json',
                                contentType: 'application/json',
                                url: 'Programme.aspx/GetChartData2',
                                data: "{projectGroup:'" + projectGroup + "',projectId:'" + projectId + "',subProgrammeId:'" + subProgrammeId + "'}",
                                success:
            function (response) {
                var dataValues = response.d;
                var data1 = new google.visualization.DataTable();
                data1.addColumn({ type: 'string', id: 'Project' });
                data1.addColumn({ type: 'date', id: 'start' });
                data1.addColumn({ type: 'date', id: 'end' });
                var minyear = 0;
                var minmonth = 0;
                var maxyear = 0;
                var maxmonth = 0;
                for (var i = 0; i < dataValues.length; i++) {
                    if (i == 0) {
                        minyear = dataValues[i].syear;
                        minmonth = dataValues[i].smonth;
                    }
                    minyear = (minyear > dataValues[i].syear) ? dataValues[i].syear : minyear;
                    minmonth = (minmonth > dataValues[i].smonth) ? dataValues[i].smonth : minmonth;
                    maxyear = (maxyear < dataValues[i].eyear) ? dataValues[i].eyear : maxyear;
                    maxmonth = (maxmonth < dataValues[i].emonth) ? dataValues[i].emonth : maxmonth;

                    data1.addRow([dataValues[i].Project, new Date(dataValues[i].syear, dataValues[i].smonth, dataValues[i].sday), new Date(dataValues[i].eyear, dataValues[i].emonth, dataValues[i].eday)]);
                }
                //if (minyear == maxyear) {
                //    var mdef = maxmonth - minmonth
                //    if (mdef < 12) {
                //        minmonth = 1
                //        maxmonth = 12
                //    }

                //}

                var mindate = new Date(minyear, minmonth, 0)
                mindate = mindate.addMonths(6);
                var maxdate = new Date(maxyear, maxmonth, 0)
                maxdate = maxdate.addMonths(6);
                var options = {
                    timeline: { showRowLabels: true },
                    avoidOverlappingGridLines: false,
                    hAxis: {
                        format: "MMM y",
                        minValue: mindate,
                        maxValue: maxdate,
                        gridlines: {
                            count: 12,
                        }
                    }
                };
                if (dataValues.length != 0) {
                    new google.visualization.Timeline(document.getElementById('chart2')).
                        draw(data1, options);
                }
                else {
                    $('#chart2').hide();
                }

                //var cnt = dataValues.length;

                //$('#chart1').height('')


            },
                                error: function () {
                                    alert("Error loading data.Please try again.");
                                }
                            });
                        });

                    }
                    </script>
                 <table width="100%" style="overflow: auto">
                        <tr>
                            <td>
                                <div id="chart1" style="height:250px;">
                                </div>
                            </td>
                            </tr>
                     </table>
        <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.LiveProjects%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
               
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                    GridLines="None" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound1"
                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                    OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting" AllowPaging="True"
                    AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="15">
                    <Columns>
                        <asp:BoundField Visible="False" DataField="ID" />
                        <asp:BoundField Visible="False" DataField="Project" />
                        <asp:TemplateField>
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPriority1" runat="server" Text='<%# Bind("Priority")%>' Visible="false"> </asp:Label>
                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("project")%>' Visible="false"> </asp:Label>
                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                    CommandArgument='<%# Bind("project")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                    CommandArgument='<%# Bind("project")%>' SkinID="BtnLinkUpdate" ToolTip="Update">
                                </asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                    SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField DataTextField="ProjectReference" HeaderStyle-Width="100" DataNavigateUrlFields="project"
                            DataNavigateUrlFormatString="~/WF/Projects/projectoverview.aspx?project={0}" HeaderText="<%$ Resources:DeffinityRes,ProjectRef%>" >
                        <HeaderStyle Width="100px" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="ProjectTitle" HeaderText="<%$ Resources:DeffinityRes,Title%>"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="150px" ReadOnly="true">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Priority" SortExpression="Priority">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority")%>' Visible="true"> </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblStatusID" runat="server" Text='<%# Bind("Priority") %>' Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlPriority" runat="server">
                                    <asp:ListItem Value="High" Text="High"></asp:ListItem>
                                    <asp:ListItem Value="Low" Text="Low"></asp:ListItem>
                                    <asp:ListItem Value="Medium" Text="Medium"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Projectvalue%>" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" Width="75px" />
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "BudgetaryCost", "{0:c}"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Actualcoststodate%>" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="10%">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Right" Width="14%" />
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "ActualCost", "{0:c}"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,StartDate%>" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="13%">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:d}")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EndDate%>" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="13%">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ProjectEndDate", "{0:d}")%></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,ApprovedVariances%>" DataField="ApprovedVariances"
                            DataFormatString="{0:c}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right"
                            ReadOnly="true">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,PendingVariances%>" DataField="PendingVariances"
                            DataFormatString="{0:c}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right"
                            ReadOnly="true">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <a href="#" onclick="window.open('WF/Projects/TaskItemsPopup.aspx?project=<%#DataBinder.Eval(Container.DataItem,"project")%>&ragstatus=green',null,'height=450 width=750 scrollbars=yes')">
                                    <%#DataBinder.Eval(Container.DataItem, "GREEN")%>
                                </a>
                            </ItemTemplate>
                            <HeaderStyle Width="40px" />
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <a href="#" onclick="window.open('WF/Projects/TaskItemsPopup.aspx?project=<%#DataBinder.Eval(Container.DataItem,"project")%>&ragstatus=amber',null,'height=450 width=750 scrollbars=yes')">
                                    <%#DataBinder.Eval(Container.DataItem, "AMBER")%></a>
                            </ItemTemplate>
                            <HeaderStyle Width="40px" />
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <a href="#" onclick="window.open('WF/Projects/TaskItemsPopup.aspx?project=<%#DataBinder.Eval(Container.DataItem,"project")%>&ragstatus=red',null,'height=450 width=750 scrollbars=yes')">
                                    <%#DataBinder.Eval(Container.DataItem, "RED")%></a>
                            </ItemTemplate>
                            <HeaderStyle Width="40px" />
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("project", "~/WF/Reports/ReportViewer.aspx?Projects={0}") %>' Target="_blank" SkinID="LinkPrint"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>

   <asp:Panel ID="PnlPendingProjects" runat="server" Visible="false" Width="100%">
                 <table width="100%" style="overflow: auto">
                        <tr>
                            <td>
                                <div id="chart2" style="height:250px;">
                                </div>
                            </td>
                            </tr>
                     </table>
       <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.PendingProjects%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="15">
                    <Columns>
                        <asp:BoundField Visible="False" DataField="ID" />
                        <asp:BoundField Visible="False" DataField="Project" />
                        <asp:HyperLinkField DataTextField="ProjectReference" HeaderStyle-Width="90" DataNavigateUrlFields="project"
                            DataNavigateUrlFormatString="~/WF/Projects/projectoverview.aspx?project={0}" HeaderText="<%$ Resources:DeffinityRes,ProjectRef%>" />
                        <asp:BoundField DataField="ProjectTitle" HeaderText="<%$ Resources:DeffinityRes,Title%>"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="14%" ItemStyle-Width="14%">
                            <HeaderStyle HorizontalAlign="Center" Width="14%" />
                            <ItemStyle Width="14%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Projectvalue%>" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="14%">
                            <HeaderStyle HorizontalAlign="Center" Width="14%" />
                            <ItemStyle HorizontalAlign="Right" Width="14%" />
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "BudgetaryCost", "{0:c}"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Actualcoststodate%>" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="14%">
                            <HeaderStyle HorizontalAlign="Center" Width="14%" />
                            <ItemStyle HorizontalAlign="Right" Width="14%" />
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "ActualCost", "{0:c}"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,StartDate%>" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="13%">
                            <HeaderStyle HorizontalAlign="Center" Width="13%" />
                            <ItemStyle HorizontalAlign="Center" Width="13%" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:d}")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EndDate%>" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="13%">
                            <HeaderStyle HorizontalAlign="Center" Width="13%" />
                            <ItemStyle HorizontalAlign="Center" Width="13%" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ProjectEndDate", "{0:d}")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,ApprovedVariances%>" DataField="ApprovedVariances"
                            DataFormatString="{0:c}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,PendingVariances%>" DataField="PendingVariances"
                            DataFormatString="{0:c}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>

      <asp:Panel ID="PnlCompletedprojects" Visible="false" runat="server" Width="100%">
          <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Completedprojects%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" GridLines="None"
                    Width="100%" AllowPaging="True" OnPageIndexChanging="GridView3_PageIndexChanging"
                    PageSize="15">
                    <Columns>
                        <asp:BoundField Visible="False" DataField="ID" />
                        <asp:BoundField Visible="False" DataField="Project" />
                        <asp:HyperLinkField DataTextField="ProjectReference" HeaderStyle-Width="90" DataNavigateUrlFields="project"
                            DataNavigateUrlFormatString="~/WF/Projects/projectoverview.aspx?project={0}" HeaderText="<%$ Resources:DeffinityRes,ProjectRef%>" />
                        <asp:BoundField DataField="ProjectTitle" HeaderText="<%$ Resources:DeffinityRes,Title%>"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="14%" ItemStyle-Width="14%">
                            <HeaderStyle HorizontalAlign="Center" Width="14%" />
                            <ItemStyle Width="14%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Projectvalue%>" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="14%">
                            <HeaderStyle HorizontalAlign="Center" Width="14%" />
                            <ItemStyle HorizontalAlign="Right" Width="14%" />
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "BudgetaryCost", "{0:c}"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Actualcoststodate%>" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="14%">
                            <HeaderStyle HorizontalAlign="Center" Width="14%" />
                            <ItemStyle HorizontalAlign="Right" Width="14%" />
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "ActualCost", "{0:c}"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,StartDate%>" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="13%">
                            <HeaderStyle HorizontalAlign="Center" Width="13%" />
                            <ItemStyle HorizontalAlign="Center" Width="13%" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:d}")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EndDate%>" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="13%">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ProjectEndDate", "{0:d}")%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="13%" />
                            <ItemStyle HorizontalAlign="Center" Width="13%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,ApprovedVariances%>" DataField="ApprovedVariances"
                            DataFormatString="{0:c}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,PendingVariances%>" DataField="PendingVariances"
                            DataFormatString="{0:c}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>

    <asp:Panel ID="PnlCategoryView" Visible="false" runat="server">
                <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Category%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="DdlRiskType"
                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlRiskType_SelectedIndexChanged"
                    CssClass="txt_field" Enabled="True" Width="225px">
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
</div>

                <div class="form-group">
        <div class="col-md-12">
           <strong> <%= Resources.DeffinityRes.Tasks%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
               
                <asp:GridView ID="GridView6" runat="server" DataKeyNames="TaskID" Width="100%" AutoGenerateColumns="False"
                    ShowFooter="True" DataSourceID="SqlDataSource2" AllowPaging="True" OnPageIndexChanging="GridView6_PageIndexChanging"
                    PageSize="15">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="header_bg_l" Visible="false">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeading" onclick="selectAll(this.checked,this.id);" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" />
                                <asp:HiddenField runat="server" ID="HID" Value='<%# Bind("TaskID") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        <asp:CommandField ItemStyle-Width="40px" Visible="false" ShowEditButton="True" EditText="&lt;img src='media/ico_edit.png' border=0 title='Edit'&gt;"
                            UpdateText="&lt;img src='media/ico_update.png' border=0 title='Update'&gt;" CancelText="&lt;img src='media/ico_cancel.png' border=0 title='Cancel'&gt;"
                            ValidationGroup="GridValid">
                            <ItemStyle Width="40px" />
                        </asp:CommandField>
                        <asp:HyperLinkField HeaderStyle-Width="90" ItemStyle-Width="90" DataTextField="ProjectReference"
                            DataNavigateUrlFields="project" DataNavigateUrlFormatString="~/WF/Projects/projectoverview.aspx?project={0}"
                            HeaderText="<%$ Resources:DeffinityRes,ProjectRef%>" />
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, ProjectTitle%>">
                            <ItemTemplate>
                                <asp:Label ID="lblProjectTitleTask" Width="150px" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Task%>">
                            <ItemTemplate>
                                <asp:Label ID="lblTask1" Width="150px" runat="server" Text='<%# Bind("Task") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, StartDate%>">
                            <ItemTemplate>
                                <asp:Label ID="lblSdate" runat="server" Text='<%# Bind("ProjectStartDate","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtStartdate" runat="server" MaxLength="10" Width="67px" Text='<%# Bind("ProjectStartDate","{0:d}") %>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="imgStartDate" SkinID="Calender" runat="server" Visible="True" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" 
                                                PopupButtonID="imgStartDate" TargetControlID="txtStartdate" CssClass="MyCalendar">
                                            </ajaxToolkit:CalendarExtender>
                                            <%--<asp:RequiredFieldValidator ID="sdateval1" runat="server" ErrorMessage="Please enter start date" ControlToValidate="txtStartdate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                                            <asp:CompareValidator ID="Sdateval2" runat="server" ControlToValidate="txtStartdate"
                                                ErrorMessage="<%$ Resources:DeffinityRes, PleaseentervalidStartdate%>" Operator="DataTypeCheck"
                                                Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, EndDate%>">
                            <ItemTemplate>
                                <asp:Label ID="lblEnddate" runat="server" Text='<%# Bind("ProjectEndDate","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtEnddate" runat="server" Width="67px" MaxLength="10" Text='<%# Bind("ProjectEndDate","{0:d}") %>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="imgEndDate" runat="server" SkinID="Calender" Visible="True" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderEndDate" runat="server" 
                                                PopupButtonID="imgEndDate" TargetControlID="txtEnddate" CssClass="MyCalendar">
                                            </ajaxToolkit:CalendarExtender>
                                            <%--<asp:RequiredFieldValidator ID="Enddateval1" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtEnddate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                                            <asp:CompareValidator ID="Enddateval2" runat="server" ControlToValidate="txtEnddate"
                                                ErrorMessage="<%$ Resources:DeffinityRes, Plsentervalidenddate%>" Operator="DataTypeCheck"
                                                Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, PercentageCompleted%>">
                            <ItemTemplate>
                                <asp:Label ID="lblPercentComplete" Width="50px" runat="server" Text='<%# Bind("PercentComplete") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField FooterStyle-Width="5px" HeaderText="<%$ Resources:DeffinityRes,RAGStatus%>">
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#LoadRagSTatus(Eval("RAGStatus").ToString())%>' />
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, AssignedTo%>">
                            <ItemTemplate>
                                <asp:Label ID="lblAssignTO" Width="145px" runat="server" Text='<%# Bind("AssignTO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="145px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="DEFFINITY_IPD_PROJTASKS_Category" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="Program" Type="Int32" SessionField="ProgrammeID" />
                        <asp:SessionParameter DefaultValue="0" Name="UserID" Type="Int32" SessionField="UID" />
                    </SelectParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DdlRiskType" DefaultValue="0" Name="CategoryID"
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </asp:Panel>
            

    <asp:Panel ID="PnlRisk" Visible="false" runat="server">
                <div class="form-group">
        <div class="col-md-12">
           <strong> <%= Resources.DeffinityRes.Risks%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                
                <asp:GridView ID="GridView7" runat="server" DataKeyNames="ID" Width="100%" AutoGenerateColumns="False"
                    ShowFooter="True" DataSourceID="SqlDataSource4" AllowPaging="True" OnPageIndexChanging="GridView7_PageIndexChanging"
                    PageSize="15">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="header_bg_l" Visible="false">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeading" onclick="selectAll(this.checked,this.id);" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" />
                                <asp:HiddenField runat="server" ID="HID" Value='<%# Bind("ID") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        <asp:CommandField ItemStyle-Width="40px" Visible="false" ShowEditButton="True" EditText="&lt;img src='media/ico_edit.png' border=0 title='Edit'&gt;"
                            UpdateText="&lt;img src='media/ico_update.png' border=0 title='Update'&gt;" CancelText="&lt;img src='media/ico_cancel.png' border=0 title='Cancel'&gt;"
                            ValidationGroup="GridValid">
                            <ItemStyle Width="40px" />
                        </asp:CommandField>
                        <asp:HyperLinkField HeaderStyle-Width="90" ItemStyle-Width="90" DataTextField="ProjectReference"
                            DataNavigateUrlFields="project" DataNavigateUrlFormatString="~/WF/Projects/ProjectRisks.aspx?project={0}"
                            HeaderText="<%$ Resources:DeffinityRes,ProjectRef%>" />
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, ProjectTitle%>">
                            <ItemTemplate>
                                <asp:Label ID="lblProjectTitle" Width="175px" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="175px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, RiskDescription%>">
                            <ItemTemplate>
                                <asp:Label ID="lblRiskDescription" Width="115px" runat="server" Text='<%# Bind("RiskDescription") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="115px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, RiskCoordinator%>">
                            <ItemTemplate>
                                <asp:Label ID="lblCo_ordinatorName" Width="115px" runat="server" Text='<%# Bind("Co_ordinatorName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, DateRaised%>">
                            <ItemTemplate>
                                <asp:Label ID="lblSdate" runat="server" Text='<%# Bind("DateRaised","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, ReportStatus%>">
                            <ItemTemplate>
                                <asp:Label ID="lblReportStatus" Width="30px" runat="server" Text='<%# Bind("ReportStatus") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Probability%>">
                            <ItemTemplate>
                                <asp:Label ID="lblProbability" Width="50px" runat="server" Text='<%# Bind("Probability") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, DegreeofImpact%>">
                            <ItemTemplate>
                                <asp:Label ID="lblDegreeofImpact" Width="30px" runat="server" Text='<%# Bind("DegreeofImpact") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Exposure%>" SortExpression="Exposure">
                            <ItemTemplate>
                                <asp:Label ID="lblExposure" Width="30px" runat="server" Text='<%# Bind("Exposure") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="5%" HeaderText="<%$ Resources:DeffinityRes, RAG %>">
                            <ItemTemplate>
                                <asp:Image ID="Image1" ImageAlign="Middle" runat="server" ImageUrl='<%#LoadRagSTatusDate(Eval("NextReviewDate").ToString())%>' />
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, NextReviewDate %>">
                            <ItemTemplate>
                                <asp:Label ID="lblNextReviewDate" runat="server" Text='<%# Bind("NextReviewDate","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="DEFFINITY_IPD_PROJRISKS_Category" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="Program" Type="Int32" SessionField="ProgrammeID" />
                        <asp:SessionParameter DefaultValue="0" Name="UserID" Type="Int32" SessionField="UID" />
                    </SelectParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DdlRiskType" DefaultValue="0" Name="CategoryID"
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </asp:Panel>

    <asp:Panel ID="PnlIssue" Visible="false" runat="server">
                <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Issue%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
              
                <asp:GridView ID="GridView8" runat="server" DataKeyNames="ID" Width="100%" AutoGenerateColumns="False"
                    ShowFooter="True" DataSourceID="SqlDataSource5" AllowPaging="True" OnPageIndexChanging="GridView8_PageIndexChanging"
                    PageSize="15">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="header_bg_l" Visible="false">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeading" onclick="selectAll(this.checked,this.id);" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" />
                                <asp:HiddenField runat="server" ID="HID" Value='<%# Bind("ID") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        <asp:CommandField ItemStyle-Width="40px" Visible="false" ShowEditButton="True" EditText="&lt;img src='media/ico_edit.png' border=0 title='Edit'&gt;"
                            UpdateText="&lt;img src='media/ico_update.png' border=0 title='Update'&gt;" CancelText="&lt;img src='media/ico_cancel.png' border=0 title='Cancel'&gt;"
                            ValidationGroup="GridValid">
                            <ItemStyle Width="40px" />
                        </asp:CommandField>
                        <asp:HyperLinkField HeaderStyle-Width="90" ItemStyle-Width="90" DataTextField="ProjectReference"
                            DataNavigateUrlFields="project" DataNavigateUrlFormatString="~/WF/Projects/ProjectIssues.aspx?project={0}"
                            HeaderText="<%$ Resources:DeffinityRes,ProjectRef%>" />
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, ProjectTitle%>">
                            <ItemTemplate>
                                <asp:Label ID="lblProjectTitleIssue" Width="115px" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Issue%>">
                            <ItemTemplate>
                                <asp:Label ID="lblIssue" Width="115px" runat="server" Text='<%# Bind("Issue") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Assignto%>">
                            <ItemTemplate>
                                <asp:Label ID="lblAssignto" Width="115px" runat="server" Text='<%# Bind("AssignToName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, DateLogged%>">
                            <ItemTemplate>
                                <asp:Label ID="lblDateLogged" runat="server" Text='<%# Bind("DateLogged","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Location%>">
                            <ItemTemplate>
                                <asp:Label ID="lblLocation" Width="115px" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="5%" HeaderText="<%$ Resources:DeffinityRes, RAG %>">
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#LoadRagSTatusDate(Eval("NextReviewDate").ToString())%>' />
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, NextReviewDate %>">
                            <ItemTemplate>
                                <asp:Label ID="lblNextReviewDate" runat="server" Text='<%# Bind("NextReviewDate","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="DEFFINITY_IPD_PROJISSUES_Category" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="Program" Type="Int32" SessionField="ProgrammeID" />
                        <asp:SessionParameter DefaultValue="0" Name="UserID" Type="Int32" SessionField="UID" />
                    </SelectParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DdlRiskType" DefaultValue="0" Name="CategoryID"
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </asp:Panel>

    <asp:Panel ID="PnlRiskMatrix" Visible="false" runat="server">
                <div class="form-group">
        <div class="col-md-12">
           <strong>  <%= Resources.DeffinityRes.RiskMatrix%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
              
                <asp:GridView ID="GridView9" runat="server" DataKeyNames="ID" Width="100%" AutoGenerateColumns="False"
                    ShowFooter="True" DataSourceID="SqlDataSource7" AllowPaging="True" OnPageIndexChanging="GridView9_PageIndexChanging"
                    PageSize="15">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="header_bg_l" Visible="false">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeading" onclick="selectAll(this.checked,this.id);" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" />
                                <asp:HiddenField runat="server" ID="HID" Value='<%# Bind("ID") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="center" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        <asp:CommandField ItemStyle-Width="40px" Visible="false" ShowEditButton="True" EditText="&lt;img src='media/ico_edit.png' border=0 title='Edit'&gt;"
                            UpdateText="&lt;img src='media/ico_update.png' border=0 title='Update'&gt;" CancelText="&lt;img src='media/ico_cancel.png' border=0 title='Cancel'&gt;"
                            ValidationGroup="GridValid">
                            <ItemStyle Width="40px" />
                        </asp:CommandField>
                        <asp:HyperLinkField DataTextField="ProjectReference" HeaderStyle-Width="90" ItemStyle-Width="90"
                            DataNavigateUrlFields="project" DataNavigateUrlFormatString="~/WF/Projects/projectoverview.aspx?project={0}"
                            HeaderText="<%$ Resources:DeffinityRes,ProjectRef%>" />
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, RiskDescription%>">
                            <ItemTemplate>
                                <asp:Label ID="lblRiskDescription" Width="115px" runat="server" Text='<%# Bind("RiskDescription") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, RiskCoordinator%>">
                            <ItemTemplate>
                                <asp:Label ID="lblCo_ordinatorName" Width="115px" runat="server" Text='<%# Bind("Co_ordinatorName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, DateRaised%>">
                            <ItemTemplate>
                                <asp:Label ID="lblSdate" runat="server" Text='<%# Bind("DateRaised","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, ReportStatus%>">
                            <ItemTemplate>
                                <asp:Label ID="lblReportStatus" Width="30px" runat="server" Text='<%# Bind("ReportStatus") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, Probability%>">
                            <ItemTemplate>
                                <asp:Label ID="lblProbability" Width="30px" runat="server" Text='<%# Bind("Probability") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, DegreeofImpact%>">
                            <ItemTemplate>
                                <asp:Label ID="lblDegreeofImpact" Width="30px" runat="server" Text='<%# Bind("DegreeofImpact") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Exposure%>" SortExpression="Exposure">
                            <ItemTemplate>
                                <asp:Label ID="lblExposure" Width="30px" runat="server" Text='<%# Bind("Exposure") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="5%" HeaderText="<%$ Resources:DeffinityRes, RAG %>">
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#LoadRagSTatusDate(Eval("NextReviewDate").ToString())%>' />
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, NextReviewDate %>">
                            <ItemTemplate>
                                <asp:Label ID="lblNextReviewDate" runat="server" Text='<%# Bind("NextReviewDate","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="DEFFINITY_IPD_PROJRISKS_Matrix" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlprojectProgramme" DefaultValue="0" Name="Program"
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlSubProgramme" DefaultValue="0" Name="SubProgram"
                            PropertyName="SelectedValue" Type="Int32" />
                            <asp:SessionParameter DefaultValue="0" Name="UserID" SessionField="UID" DbType="Int32" />
                    </SelectParameters>
                    
                </asp:SqlDataSource>
            </asp:Panel>
    <asp:Panel ID="PnlAssessment" Visible="false" runat="server">
                <div class="form-group">
        <div class="col-md-12">
           <strong> <%= Resources.DeffinityRes.Assessment%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
         <div class="pro_madatory" style="padding-top: 10px">
                                            <span></span><a href="#" onclick="window.close()"></a>
                                        </div>
        <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.AddAssessment%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
               <div class="form-group">
      <div class="col-md-8">
           <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.DateLogged%></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtDateLogged" Width="95px" runat="server" SkinID="txtCalender"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Plsenterdateformat%>"
                                            ControlToValidate="txtDateLogged" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                                        <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender" ImageAlign="AbsMiddle" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                            PopupButtonID="imgbtnenddate6" TargetControlID="txtDateLogged" CssClass="MyCalendar">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfv_dateRised" runat="server" ControlToValidate="txtDateLogged"
                                            Display="None" ErrorMessage="Please enter Logged Date" ValidationGroup="Group1"></asp:RequiredFieldValidator>
            </div>
	</div>
	
</div>
        <div class="form-group">
      <div class="col-md-8">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.ProgresstoDate%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtProgress" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
                                        <asp:HiddenField ID="id" runat="server" />
            </div>
	</div>
	
</div>
        <div class="form-group">
      <div class="col-md-8">
           <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Benefits%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtBenefits" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
            </div>
	</div>
	
</div>
        <div class="form-group">
      <div class="col-md-8">
           <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Eval_emer_Opportunities%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtOpportunities" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
            </div>
	</div>
	
</div>
        <div class="form-group">
      <div class="col-md-8">
           <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.PaceofProgress%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtPaceOfProgress" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
            </div>
	</div>
	
</div>
        <div class="form-group">
      <div class="col-md-8">
           <label class="col-sm-3 control-label"> </label>
           <div class="col-sm-9">
               <asp:Button ID="btnSubmit" runat="server" ValidationGroup="Group1" OnClick="btnSubmit_Click"
                                            SkinID="btnSubmit" />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" SkinID="btnCancel"
                                            />
               </div>
          </div>
            </div>
               <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Assessments%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                           
                            <div>
                                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                    DataSourceID="SqlDataSource1" Width="100%" OnSelectedIndexChanging="GridView5_SelectedIndexChanging"
                                    OnRowDataBound="GridView5_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" SkinID="BtnLinkEdit"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Select" SkinID="BtnLinkEdit"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="<%$ Resources:DeffinityRes,ID%>" InsertVisible="False"
                                            ReadOnly="True" SortExpression="ID" Visible="false" />
                                        <asp:TemplateField HeaderStyle-Width="25%" HeaderText="<%$ Resources:DeffinityRes,ProgresstoDate%>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProgressToDate" runat="server" Text='<%#TrimSmallDescStr(Eval("ProgressToDate").ToString())%>'></asp:Label>
                                                <asp:Label ID="lblProgressToDate1" runat="server" Text='<%#Bind("ProgressToDate")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="25%" />
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ProgrammeID" HeaderText="<%$ Resources:DeffinityRes,ProgrammeID%>"
                                            SortExpression="ProgrammeID" Visible="false" />
                                        <asp:TemplateField HeaderStyle-Width="25%" HeaderText="<%$ Resources:DeffinityRes,Benefits%>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBenefits" runat="server" Text='<%#TrimSmallDescStr(Eval("Benefits").ToString())%>'></asp:Label>
                                                <asp:Label ID="lblBenefits1" runat="server" Text='<%#Bind("Benefits")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EmergentOpportunities%>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmergentOpportunities" runat="server" Text='<%#TrimSmallDescStr(Eval("EmergentOpportunities").ToString())%>'></asp:Label>
                                                <asp:Label ID="lblEmergentOpportunities1" runat="server" Text='<%#Bind("EmergentOpportunities")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="25%" />
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="25%" HeaderText="<%$ Resources:DeffinityRes,PaceofProgress%>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaceOfProgress" runat="server" Text='<%#TrimSmallDescStr(Eval("PaceOfProgress").ToString())%>'></asp:Label>
                                                <asp:Label ID="lblPaceOfProgress1" runat="server" Text='<%#Bind("PaceOfProgress")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="25%" />
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="<%$ Resources:DeffinityRes,DateLogged%>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDateLogged" runat="server" Text='<%# Bind("datelogged","{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="8%" HeaderText="<%$ Resources:DeffinityRes,RaisedDate%>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRaisedDate" runat="server" Text='<%# Bind("RaisedDate","{0:d}") %>'></asp:Label>
                                                <asp:HiddenField ID="HID" runat="server" Value='<%# Bind("ID") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="deletebut" runat="server" CommandName="delete" SkinID="BtnLinkDelete"
                                                    OnClientClick="return confirm('Do you want to delete the Assessment?');" ToolTip="<%$ Resources:DeffinityRes,Delete%>"
                                                    Visible="True" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                    DeleteCommand="delete from ProgrammeAssessment where ID=@ID" SelectCommand="SELECT ProgrammeAssessment.* FROM ProgrammeAssessment where ProgrammeID=@ProgrammeID order by datelogged desc">
                                    <SelectParameters>
                                        <asp:SessionParameter DefaultValue="0" Name="ProgrammeID" Type="Int32" SessionField="ProgrammeID" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="ID" />
                                    </DeleteParameters>
                                </asp:SqlDataSource>
                            </div>
            </asp:Panel>

    <asp:Panel ID="PnlDependencyMap" Visible="false" runat="server">

        <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.DependencyMap%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                    <asp:GridView ID="GridView4" runat="server" DataKeyNames="TaskID" Width="100%" AutoGenerateColumns="False"
                        ShowFooter="True" DataSourceID="SqlDataSource3" AllowPaging="True" OnPageIndexChanging="GridView4_PageIndexChanging"
                        PageSize="15">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="header_bg_l" Visible="false">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkHeading" onclick="selectAll(this.checked,this.id);" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkItem" runat="server" />
                                    <asp:HiddenField runat="server" ID="HID" Value='<%# Bind("ID") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>
                            <asp:CommandField ItemStyle-Width="40px" Visible="false" ShowEditButton="True" EditText="&lt;img src='media/ico_edit.png' border=0 title='Edit'&gt;"
                                UpdateText="&lt;img src='media/ico_update.png' border=0 title='Update'&gt;" CancelText="&lt;img src='media/ico_cancel.png' border=0 title='Cancel'&gt;"
                                ValidationGroup="GridValid">
                                <ItemStyle Width="40px" />
                            </asp:CommandField>
                            <asp:HyperLinkField DataTextField="ProjectReference" HeaderStyle-Width="90" ItemStyle-Width="90"
                                DataNavigateUrlFields="project" DataNavigateUrlFormatString="~/WF/Projects/projectoverview.aspx?project={0}"
                                HeaderText="<%$ Resources:DeffinityRes,ProjectRef%>" />
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, ProjectTitle%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectTitle" Width="115px" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Task%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblTask11" Width="115px" runat="server" Text='<%# Bind("Task") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, StartDate%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblSdate" runat="server" Text='<%# Bind("TaskStartDate","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtStartdate" runat="server" MaxLength="10" Width="67px" Text='<%# Bind("TaskStartDate","{0:d}") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="imgStartDate" SkinID="Calender" runat="server" Visible="True" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" 
                                                    PopupButtonID="imgStartDate" TargetControlID="txtStartdate" CssClass="MyCalendar">
                                                </ajaxToolkit:CalendarExtender>
                                                <%--<asp:RequiredFieldValidator ID="sdateval1" runat="server" ErrorMessage="Please enter start date" ControlToValidate="txtStartdate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                                                <asp:CompareValidator ID="Sdateval2" runat="server" ControlToValidate="txtStartdate"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, PleaseentervalidStartdate%>" Operator="DataTypeCheck"
                                                    Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, EndDate%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblEnddate" runat="server" Text='<%# Bind("TaskCompletiondate","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtEnddate" runat="server" Width="67px" MaxLength="10" Text='<%# Bind("TaskCompletiondate","{0:d}") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="imgEndDate" runat="server" SkinID="Calender" Visible="True" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderEndDate" runat="server" 
                                                    PopupButtonID="imgEndDate" TargetControlID="txtEnddate" CssClass="MyCalendar">
                                                </ajaxToolkit:CalendarExtender>
                                                <%--<asp:RequiredFieldValidator ID="Enddateval1" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtEnddate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                                                <asp:CompareValidator ID="Enddateval2" runat="server" ControlToValidate="txtEnddate"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, Plsentervalidenddate%>" Operator="DataTypeCheck"
                                                    Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageAlign="AbsMiddle" ImageUrl="~\images\ico_arrow.gif" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, DependsUpon%>">
                                <ItemTemplate>
                                    <asp:Label ID="DepOnProject" Width="60px" runat="server" Text='<%# Bind("DependingOnProject") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, ProjectTitle%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectTitle1" Width="115px" runat="server" Text='<%# Bind("ProjectTitleDep") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Task%>">
                                <ItemTemplate>
                                    <asp:Label ID="DepOnTask" Width="90px" runat="server" Text='<%# Bind("DependingOnTask") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, StartDate%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblOnStartdate" runat="server" Text='<%# Bind("OnStartDate","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtOnStartdate" runat="server" Width="67px" MaxLength="10" Text='<%# Bind("OnStartDate","{0:d}") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="imgOnStartdate" runat="server" SkinID="Calender" Visible="True" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderOnStartDate" runat="server" 
                                                    PopupButtonID="imgOnStartdate" TargetControlID="txtOnStartdate" CssClass="MyCalendar">
                                                </ajaxToolkit:CalendarExtender>
                                                <%--<asp:RequiredFieldValidator ID="OnStartdateval1" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtOnStartdate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                                                <asp:CompareValidator ID="OnStartdateval2" runat="server" ControlToValidate="txtOnStartdate"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, EntervalidEnddate%>" Operator="DataTypeCheck"
                                                    Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="10%" HeaderText="<%$ Resources:DeffinityRes, EndDate%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblOnEnddate" runat="server" Text='<%# Bind("OnCompletionDate","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtonEnddate" runat="server" Width="67px" MaxLength="10" Text='<%# Bind("OnCompletionDate","{0:d}") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="ImgOnEndDate" runat="server" SkinID="Calender" Visible="True" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderOnEndDate" runat="server" 
                                                    PopupButtonID="ImgOnEndDate" TargetControlID="txtOnEnddate" CssClass="MyCalendar">
                                                </ajaxToolkit:CalendarExtender>
                                                <%--<asp:RequiredFieldValidator ID="OnEnddateval1" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtonEnddate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                                                <asp:CompareValidator ID="OnEnddateval2" runat="server" ControlToValidate="txtonEnddate"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, EntervalidEnddate%>" Operator="DataTypeCheck"
                                                    Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="<%$ Resources:DeffinityRes, DependentProject%>">
                                <ItemTemplate>
                                    <asp:Label ID="DepProject" Width="60px" runat="server" Text='<%# Bind("DependentProject") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="<%$ Resources:DeffinityRes, Task%>">
                                <ItemTemplate>
                                    <asp:Label ID="DepTask" Width="90px" runat="server" Text='<%# Bind("DependentTask") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="<%$ Resources:DeffinityRes, StartDate%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepStartdate" runat="server" Text='<%# Bind("DepStartDate","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDepStartdate" runat="server" Width="67px" MaxLength="10" Text='<%# Bind("DepStartDate","{0:d}") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="imgDepStartdate" runat="server" SkinID="Calender" Visible="True" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderdepStartDate" runat="server" 
                                                    PopupButtonID="imgDepStartdate" TargetControlID="txtDepStartdate" CssClass="MyCalendar">
                                                </ajaxToolkit:CalendarExtender>
                                                <%--<asp:RequiredFieldValidator ID="DepStartdateval1" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtDepStartdate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                                                <asp:CompareValidator ID="DepStartdateval2" runat="server" ControlToValidate="txtDepStartdate"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, EntervalidEnddate%>" Operator="DataTypeCheck"
                                                    Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="<%$ Resources:DeffinityRes, EndDate%>"
                                HeaderStyle-CssClass="header_bg_r">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepEnddate" runat="server" Text='<%# Bind("DepCompletionDate","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtdepEnddate" runat="server" Width="67px" MaxLength="10" Text='<%# Bind("DepCompletionDate","{0:d}") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="ImgDepEndDate" runat="server" SkinID="Calender" Visible="True" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderDepEndDate" runat="server" 
                                                    PopupButtonID="ImgDepEndDate" TargetControlID="txtdepEnddate" CssClass="MyCalendar">
                                                </ajaxToolkit:CalendarExtender>
                                                <%--<asp:RequiredFieldValidator ID="DepEnddateval1" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtdepEnddate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                                                <asp:CompareValidator ID="DepEnddateval2" runat="server" ControlToValidate="txtdepEnddate"
                                                    ErrorMessage="<%$ Resources:DeffinityRes,EntervalidEnddate%>" Operator="DataTypeCheck"
                                                    Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="DEFFINITY_IPD_PROJTASKS_Program" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlprojectProgramme" DefaultValue="0" PropertyName="SelectedValue"
                                Name="Program" />
                            <asp:ControlParameter ControlID="ddlSubProgramme" DefaultValue="0" PropertyName="SelectedValue"
                                Name="SubProgram" />
                                <asp:SessionParameter DbType="Int32" DefaultValue="0" Name="UserID" SessionField="UID" />
                        </SelectParameters>
                    </asp:SqlDataSource>
            </asp:Panel>


     <asp:Panel ID="PnlRAGSummary" Visible="false" runat="server">
                <uc1:ProgrammeRAG ID="rag" runat="server"></uc1:ProgrammeRAG>
            </asp:Panel>
           
             <asp:Panel ID="pnlWorkstream" Visible="false" runat="server">
             <uc1:ProgrammeWorkStream ID="ProgrammeWorkStream1" runat="server" />
             </asp:Panel>



    <script language="javascript" type="text/javascript" src="js/overlib.js"></script>
    
     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
      <%: System.Web.Optimization.Scripts.Render("~/bundles/charts") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 
    <script type="text/javascript">
        $(document).ready(function () {
            var PID = $('#ddlprojectProgramme').val();
            var Type = $('#ddlType').val();
            var SPID = $('#ddlSubProgramme').val();
            var Cid = $('#ddlCountry').val();


            DashBoardFinancialGrpah(PID);
            DashBoardGrpah2(PID);
            debugger;
            if (Cid != undefined) {
                DashBoardBenfitGrpah2(PID, Type, SPID, Cid);
            }
        });
        function GraphBind(dataSource) {
            debugger;
            $("#DivProgramChart").dxChart({
                equalBarWidth: false,
                dataSource: dataSource,
                commonSeriesSettings: {
                    argumentField: "state",
                    type: "bar"
                },
                series: [
                    { valueField: "ProjectValue", name: "Project value" },
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
        function DashBoardFinancialGrpah(PID) {

            $.ajax({
                url: '../Dashboard/Programme.aspx/BindFinancialGrpah',
                type: "POST",
                data: "{'PID': '" + PID + "'}",
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
                                ProjectValue: Newdt[i].ProjectValue,
                                ActualcosttoDate: Newdt[i].ActualcosttoDate,
                                Variances: Newdt[i].Variances,
                                Invoiced: Newdt[i].Invoiced
                            });
                    }
                    GraphBind(datatable1);
                }
            });
        };



        function GraphBind1(dataSource) {
            debugger;
            $("#DivFinancialSummary").dxChart({
                equalBarWidth: false,
                dataSource: dataSource,
                commonSeriesSettings: {
                    argumentField: "state",
                    type: "bar"
                },
                series: [
                    { valueField: "Late", name: "Late" },
                    { valueField: "OnTime", name: "OnTime" },
                    { valueField: "Complete", name: "Complete" }
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
        function DashBoardGrpah2(PID) {

            $.ajax({
                url: '../Dashboard/Programme.aspx/BindProgrammeSummaryGraph',
                type: "POST",
                data: "{'PID': '" + PID + "'}",
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
                                Late: Newdt[i].Late,
                                OnTime: Newdt[i].OnTime,
                                Complete: Newdt[i].Complete
                            });
                    }
                    GraphBind1(datatable1);
                }
            });
        };


        function GraphBind2(dataSource) {
            debugger;
            $("#DivBenfitTrackingReport").dxChart({
                equalBarWidth: false,
                dataSource: dataSource,
                commonSeriesSettings: {
                    argumentField: "state",
                    type: "bar"
                },
                series: [
                    { valueField: "ActualtoDate", name: "Actual to Date" },
                    { valueField: "PlannedtoDate", name: "Planned to Date" },
                    { valueField: "TargettoEnd", name: "Target to End" }
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
        function DashBoardBenfitGrpah2(PID, Type, SPID, Cid) {

            $.ajax({
                url: '../Dashboard/Programme.aspx/BindBenFitGrpah',
                type: "POST",
                data: "{'PID': '" + PID + "','Type': '" + Type + "','SPID': '" + SPID + "','Cid': '" + Cid + "'}",
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
                                ActualtoDate: Newdt[i].ActualtoDate,
                                PlannedtoDate: Newdt[i].PlannedtoDate,
                                TargettoEnd: Newdt[i].TargettoEnd
                            });
                    }
                    GraphBind2(datatable1);
                }
            });
        };


    </script>

   
</asp:Content>
