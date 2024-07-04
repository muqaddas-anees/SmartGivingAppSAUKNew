<%@ Page Language="C#" AutoEventWireup="true" Theme="Theme1" Inherits="Reports_Default12" Codebehind="Default12.aspx.cs" %>


<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Deffinity Reports</title>
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

 </div>
<div class="sec_table1">
	<div>
        <table width="80%" border="0" cellspacing="0" cellpadding="0">
        <tr>
        <td><span><font color="#ff6633" size="4"><b>Select Projects </b></font></span></td>
        <td> <asp:DropDownList ID="ddlProjects" runat="server">
        </asp:DropDownList>
         <asp:ImageButton ID="btnReport" runat="server" OnClick="btnReport_Click" 
        AlternateText="Get Report"  ImageUrl="~/images/btn_view_report.gif" ImageAlign="AbsMiddle"/></td>
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
  </div>
    </div>
	</div>
</div>
</div>

<div class="clr"></div>




</div>
   
    </form>
</body>
</html>
