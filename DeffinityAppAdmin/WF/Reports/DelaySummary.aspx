<%@ Page Language="C#" AutoEventWireup="true" Theme="Theme1" Inherits="Delay_SummaryPage" Codebehind="DelaySummary.aspx.cs" %>



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
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   

<div>
	<div>
        <table width="80%" border="0" cellspacing="0" cellpadding="0">
   
        <tr>
    <td width="15%"><b>Select Resource</b></td>
            <td colspan="2" width="14%">
     <asp:DropDownList ID="ddlContractors" runat="server"  Font-Names="Verdana" >
        </asp:DropDownList></td>
        <td width="11%">
         <asp:ImageButton ID="btnReport" runat="server" AlternateText="Get Report" 
                OnClick="btnReport_Click" Font-Names="Verdana" ImageAlign="AbsMiddle" 
                SkinID="ImgViewReport" />
     
        </td>
      <td width="40%"></td>
      </tr>  
       
        </table>
        <br />
        <br />
        <br /> 
        <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="True"
            height="1039px" reportsourceid="CrystalReportSource1" width="901px" BorderColor="LightGray" BorderStyle="Groove" DisplayGroupTree="False" HasToggleGroupTreeButton="False" ToolbarStyle-BackColor="Silver"></cr:crystalreportviewer>
        <cr:crystalreportsource id="CrystalReportSource1" runat="server">
<Report FileName="Report10.rpt"></Report>
</cr:crystalreportsource>
    
   
        
    </div>
  </div>
 
    </form>
</body>
</html>
