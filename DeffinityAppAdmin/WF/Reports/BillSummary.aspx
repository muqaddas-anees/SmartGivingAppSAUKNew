<%@ Page Language="C#" AutoEventWireup="true" Theme="Theme1" Inherits="BillSummary" Codebehind="BillSummary.aspx.cs" %>


<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Deffinity Reports</title>
     <link href="../stylcss/deffinity_frame.css" rel="stylesheet" type="text/css" />
    <link href="../stylcss/deffinity_custom.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   
	<div >
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
        <td width="20%">Select Projects</td>
        <td width="10%" > <asp:DropDownList ID="ddlProjects" runat="server">
        </asp:DropDownList></td>
        <td width="5%">
         <asp:ImageButton ID="btnReport" runat="server" OnClick="btnReport_Click" 
        AlternateText="Get Report" ImageAlign="AbsMiddle" SkinID="ImgViewReport"/></td>
        <td width="65%" ></td>
        </tr>
           </table>
           <br />
           <br />
           <br /> 
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
            Height="700px" ReportSourceID="CrystalReportSource1" Width="700px" BorderColor="Silver" BorderStyle="Groove" DisplayGroupTree="False" ToolbarStyle-BackColor="Silver" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="Report12.rpt">
            </Report>
        </CR:CrystalReportSource>
    
 
        
    </div>
  
   
    </form>
</body>
</html>
