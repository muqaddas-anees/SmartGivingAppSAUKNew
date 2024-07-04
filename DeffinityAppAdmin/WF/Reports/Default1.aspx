<%@ Page Language="C#" AutoEventWireup="true" Inherits="Default1" Codebehind="Default1.aspx.cs" %>



<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>i-Dash Board Reports</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <font face="Verdana" color="#808080" size="1">
    <div>
        &nbsp;<br />
        <br />
        <br />
        <br />
        &nbsp; &nbsp;&nbsp; &nbsp;<br />
        &nbsp;&nbsp;
        &nbsp; &nbsp;<asp:Label ID="Label1" runat="server" Text="Projects"></asp:Label>&nbsp;&nbsp;
        &nbsp;<asp:DropDownList
            ID="ddlProjects" runat="server" Font-Names="Verdana" >
        </asp:DropDownList>
        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Get Report" Font-Names="Verdana" />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
        <br />
        <br />
        <br />
        <br />
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
            DisplayGroupTree="False" HasToggleGroupTreeButton="False" ReportSourceID="CrystalReportSource1" BorderColor="LightGray" BorderStyle="Groove" ToolbarStyle-BackColor="Silver" />
        <br />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="Report1.rpt">
            </Report>
        </CR:CrystalReportSource>
    
    </div>
    </font>
    </form>
</body>
</html>
