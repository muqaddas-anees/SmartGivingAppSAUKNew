<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Checkpoint_Feedback"
         Title="PM Feedback" Codebehind="Checkpoint_Feedback.aspx.cs" %>

<%@ Register Src="controls/Checkpoint_tabs.ascx" TagName="OpsViewTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/CheckpointFeedback.ascx" TagName="CheckpointFeedback" TagPrefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Checkpoint%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Feedback - <Pref:ProjectRef ID="ProjectRef1" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:OpsViewTabs ID="OpsViewTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <%--<div id="DivRatingsCharts"></div>--%>
 <uc2:CheckpointFeedback ID="Checkpointfeedback1" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {

            var Cid = $('#ddlResource').val();
            if (Cid > 0) {
                GraphInFeedBack(Cid);
            }

        });
    function GraphBind(dataSource)
    {
        debugger;
        $("#DivRatingsCharts").dxChart({
            dataSource: dataSource,
            commonSeriesSettings: {
                argumentField: "state",
                type: "bar"
            },
            series: [
                { valueField: "Timeliness", name: "Timeliness" },
                { valueField: "QualityofWork", name: "QualityofWork" },
                { valueField: "ValueforMoney", name: "ValueforMoney" },
                { valueField: "Communication", name: "Communication" }
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
    function GraphInFeedBack(Cid) {
      
        $.ajax({
            url: '../Checkpoint/Checkpoint_Feedback.aspx/DataOfFeedBackGraph',
            type: "POST",
            data: "{'Cid': '" + Cid + "'}",
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
                            Timeliness: Newdt[i].Timeliness,
                            QualityofWork: Newdt[i].QualityofWork,
                            ValueforMoney: Newdt[i].ValueforMoney,
                            Communication: Newdt[i].Communication
                        });
                }
                GraphBind(datatable1);
            }
        });
    };
</script>
                       
</asp:Content>

