<%@ Page Language="C#" AutoEventWireup="true" Inherits="PerformmanceReviewReport" Codebehind="PerformmanceReviewReport.aspx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Performmance Review Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" 
            BorderColor="LightGray" BorderStyle="Groove" Height="900px" ReportSourceID="CrystalReportSource1"
            Width="800px" DisplayGroupTree="False" HasToggleGroupTreeButton="False" ToolbarStyle-BackColor="Silver" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="PerformmanceReviewReportt.rpt">
            </Report>
        </CR:CrystalReportSource>
    </div>
    </form>
</body>
</html>
