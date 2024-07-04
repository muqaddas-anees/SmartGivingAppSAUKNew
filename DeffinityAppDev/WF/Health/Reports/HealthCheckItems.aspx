<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_HealthCheckItems" Codebehind="HealthCheckItems.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Health Check List Items</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" BorderColor="LightGray" BorderStyle="Groove"
           HasDrillUpButton="False" ToolPanelView="None" ToolbarStyle-BackColor="Silver" />
    </div>
    </form>
</body>
</html>
