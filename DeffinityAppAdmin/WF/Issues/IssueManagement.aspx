<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeFile="IssueManagement.aspx.cs" Inherits="IssueManagement" %>
<%@ Register Src="~/WF/Projects/controls/ProjectPmNewIssues.ascx" TagName="ProjectPmIssues" TagPrefix="uc2" %>
<%@ Register src="~/WF/Projects/MailControls/ProjectIssue.ascx" tagname="ProjectIssue" tagprefix="PD1" %>
<%@ Register Src="~/WF/Issues/Controls/IssuesGraphCntl.ascx" TagName="IssueGraphs" TagPrefix="Issue_G" %>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.IssuesManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Issues%> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
     <script  type="text/javascript">
       $(document).ready(function () {
           $('#navTab').hide();
       });
       </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
       <div class="form-group">
           <Issue_G:IssueGraphs ID="i1" runat="server" />
       </div>
    <br />
 <asp:Panel ID="PanelControl" runat="server">
        <uc2:ProjectPmIssues id="ProjectPmIssues1" runat="server"></uc2:ProjectPmIssues>
</asp:Panel>
 
     


 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <%: System.Web.Optimization.Scripts.Render("~/bundles/charts") %>
 <script type="text/javascript">

     //grid_responsive();
     grid_responsive_display();
     sideMenuActive('<%= Resources.DeffinityRes.Issues%>');
     $(window).load(function () {


         $("button:contains('Display all')").click(function (e) {
             e.preventDefault();
             $(".dropdown-menu li")
       .find("input[type='checkbox']")
       .prop('checked', 'checked').trigger('change');
         });

     });
    </script>
    
<script>
    $(document).ready(function () {
        GetIssuesDataWithCategory();
        GetIssuesDataWithA_User();

        function GetIssuesDataWithCategory() {
            $.ajax({
                url: 'IssueManagement.aspx/GetIssuesDataWithCategory',
                type: "POST",
                data: "{}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        var newData = jQuery.parseJSON(data.d);
                        var ds = [];
                        for (var i = 0; i < newData.length; i++) {
                            ds.push({
                                Name: newData[i].Name,
                                Value: newData[i].Value
                            });
                        }
                        $("#IssuesGraphWithCategory").dxChart({
                            dataSource: ds,
                            series: {
                                argumentField: "Name",
                                valueField: "Value",
                                name: "Category",
                                type: "bar",
                                color: '#68b828'
                            },
                            title:
                                {
                                    text: 'Issues by category',
                                    font: { size: 22 }
                                },
                            font: { size: 22 },
                            tooltip: {
                                enabled: true
                            },
                            legend: {
                                visible: false
                            }
                        });
                    }
                }
            });
        }
        function GetIssuesDataWithA_User() {
            $.ajax({
                url: 'IssueManagement.aspx/GetIssuesDataWithUser',
                type: "POST",
                data: "{}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        var newData = jQuery.parseJSON(data.d);
                        var ds = [];
                        for (var i = 0; i < newData.length; i++) {
                            ds.push({
                                Name: newData[i].Name,
                                Value: newData[i].Value
                            });
                        }
                        $("#IssuesGraphWithUser").dxChart({
                            dataSource: ds,
                            series: {
                                argumentField: "Name",
                                valueField: "Value",
                                name: "Category",
                                type: "bar",
                                color: '#68b828'
                            },
                            title: {
                                text: "Issues by user",
                                font: { size: 22 }
                            },
                            tooltip: {
                                enabled: true
                            },
                            legend: {
                                visible: false
                            }
                        });
                    }
                }
            });
        }

    });
</script>



</asp:Content>

