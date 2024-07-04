<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_GanttDefault2"
    Theme="Theme1" Codebehind="GanttDefault2.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deffinity Reports</title>
    <link href="../stylcss/deffinity_frame.css" rel="stylesheet" type="text/css" />
    <link href="../stylcss/deffinity_custom.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style5
        {
            width: 274px;
        }
        .style6
        {
            width: 175px;
        }
    </style>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <asp:Panel ID="panel1" runat="server" Height="200px" Visible="false">
    <table>
        <tr>
            <td class="style6">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label13" runat="server" Text="From Date"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFromDate" runat="server" Width="104px" CssClass="txt_field" onKeyPress="alert('Please choose the calender for date');return false;">
            
                </asp:TextBox>
                <span lang="en-us">&nbsp;&nbsp; </span>
                <asp:Image runat="server" ID="imgFromDate" SkinID="Calender"
                    AlternateText="Please select a date" />
                <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" 
                    CssClass="MyCalendar" TargetControlID="txtFromDate" PopupButtonID="imgFromDate" />
            </td>
            <td>
                <asp:Label ID="Label14" runat="server" Text="To Date"></asp:Label>
            </td>
            <td class="style5">
                <asp:TextBox ID="txtToDate" runat="server" Width="104px" CssClass="txt_field" onKeyPress="alert('Please choose the calender for date');return false;"></asp:TextBox>
                <span lang="en-us">&nbsp;&nbsp; </span>
                <asp:Image runat="server" ID="imgToDate" SkinID="Calender" AlternateText="Please select a date" />
                <ajaxToolkit:CalendarExtender runat="server" ID="calToDate" CssClass="MyCalendar"
                     TargetControlID="txtToDate" PopupButtonID="imgToDate" />
            </td>
        </tr>
        <tr>
            <td class="style6">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Select Resource"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlContractor" runat="server" Width="210px" 
                    Font-Names="Verdana" ToolTip="Select a contractor">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
            <td class="style5">
                <asp:ImageButton ID="btnReport" runat="server" OnClick="btnReport_Click" AlternateText="Get Report"
                    SkinID="ImgViewReport"
                    ToolTip="Click here to view report" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
            </td>
        </tr>
    </table>
    </asp:Panel>
   
    <font face="Verdana" color="#808080" size="1">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
            BorderColor="LightGray" BorderStyle="Groove" Height="1039px" ReportSourceID="CrystalReportSource1"
            Width="901px" DisplayGroupTree="False" HasToggleGroupTreeButton="False" ToolbarStyle-BackColor="Silver" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="GanttReport2.rpt">
            </Report>
        </CR:CrystalReportSource>
         </font>
    </form>
</body>
</html>
