<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_RPT_ShiftRota" Codebehind="RPT_ShiftRota.aspx.cs" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shift Rota</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

    <div>
    <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
        AutoDataBind="True" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" Height="50px" 
        Width="350px" DisplayGroupTree="False" style="position: relative" />--%>
    
    <CR:CrystalReportViewer  ID="CrystalReportViewer1" 
        runat="server" AutoDataBind="true" 
        BorderColor="LightGray" BorderStyle="Groove" 
        DisplayGroupTree="False" HasToggleGroupTreeButton="False" 
        ToolbarStyle-BackColor="Silver" 
        Width="1013px" />
    </div>
    
        &nbsp;
    </form>
</body>
</html>
