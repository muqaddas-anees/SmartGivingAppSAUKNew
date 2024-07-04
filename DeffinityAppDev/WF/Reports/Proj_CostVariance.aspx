<%@ Page Language="C#" AutoEventWireup="true" Theme="Theme1" Inherits="Reports_Default17" Codebehind="Proj_CostVariance.aspx.cs" %>



<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Deffinity Reports</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
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
   
    <div class="bodyframe">
<div class="header" >
<div style="clear:both"></div>
<div class="content">
<div class="content_bodyfull1">
<%--<div class="sections">--%>
<div class="clr"></div>
<div  class="sec_body1">
<div class="sec_header">
 Financial Summary
 </div>
<div >
	<div>
        <table width="80%" border="0" cellspacing="0" cellpadding="0">
        <tr>
        <td width="14%">Select Programme</td>
        <td width="14%"><asp:DropDownList ID="ddlOwners" runat="server" Width="200px" Font-Names="Verdana">
        </asp:DropDownList></td>
        <td width="5%">
        Days
        </td>
        <td width="14%">
        <asp:DropDownList ID="ddlDays" runat="server" Width="70px" Font-Names="Verdana">
            <asp:ListItem Selected="True" Value="-30">30</asp:ListItem>
            <asp:ListItem Value="-60">60</asp:ListItem>
            <asp:ListItem Value="-90">90</asp:ListItem>
            <asp:ListItem Value="-365">Annual</asp:ListItem>
        </asp:DropDownList>
        </td>
        <td width="5%">
         <asp:ImageButton ID="btnReport" runat="server" OnClick="btnReport_Click"
         AlternateText="Get Report" ImageUrl="~/images/btn_view_report.gif" ToolTip="Click here to view the report" />
        
        </td>
        <td width="28%"></td>
        </tr>
      </table>
      <br />
      <br />
      <br />  
        <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="True"
            height="1039px" reportsourceid="CrystalReportSource1" width="901px" BorderColor="LightGray" BorderStyle="Groove" DisplayGroupTree="False" HasToggleGroupTreeButton="False" ToolbarStyle-BackColor="Silver"></cr:crystalreportviewer>
        <cr:crystalreportsource id="CrystalReportSource1" runat="server">
<Report FileName="Report17.rpt"><Parameters>
<CR:ControlParameter PropertyName="Text" ReportName="" Name="@OwnerID" DefaultValue="1" ConvertEmptyStringToNull="False" ControlID="TextBox1"></CR:ControlParameter>
<CR:ControlParameter PropertyName="Text" ReportName="" Name="@days" DefaultValue="-30" ConvertEmptyStringToNull="False" ControlID="TextBox2"></CR:ControlParameter>
</Parameters>
</Report>
</cr:crystalreportsource>

        
    </div>
  </div>
    </div>
	</div>
</div>


<div class="clr"></div>



</div>
</div>
   
    </form>
</body>
</html>
