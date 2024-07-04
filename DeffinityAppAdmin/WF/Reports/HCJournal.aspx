<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_HCJournal" Codebehind="HCJournal.aspx.cs" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../stylcss/deffinity_custom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmanager1" runat="server" />
    <div>
        <table>
            <tr>
                <td>
                    Health Check
                </td>
                <td>
                    <asp:DropDownList ID="ddlHealthCheck" runat="server" DataSourceID="objHealthCheck"
                        DataTextField="Title" DataValueField="ID" AppendDataBoundItems="true" >
                        <asp:ListItem Text="All" Value="0" />
                        </asp:DropDownList>
                    <asp:ObjectDataSource ID="objHealthCheck" runat="server" SelectMethod="LoadPortfolioHealthCheckTitle"
                        TypeName="DataHelperClass" />
                </td>
                <td>
                    From Date
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" />
                    <asp:Image ID="Image4" SkinID="Calender" runat="server" Style="padding-left: 0px;
                        padding-right: 0px" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" 
                        PopupButtonID="Image4" TargetControlID="txtFromDate" CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                </td>
                <td>
                    To Date
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" />
                    <asp:Image ID="Image1" SkinID="Calender" runat="server" Style="padding-left: 0px;
                        padding-right: 0px" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                        PopupButtonID="Image1" TargetControlID="txtToDate" CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    <asp:ImageButton ID="btnGetReport" ImageUrl="~/media/btn_get_report.gif" runat="server" Text="Get Report" OnClick="btnGetReport_Click" />
                </td>
            </tr>
        </table>
        <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />--%>
         <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
                        BorderColor="LightGray" BorderStyle="Groove" Height="1039px" ReportSourceID="CrystalReportSource1"
                        Width="901px" DisplayGroupTree="False" HasToggleGroupTreeButton="False" ToolbarStyle-BackColor="Silver" ShowAllPageIds="True" />--%>
    </div>
    </form>
</body>
</html>
