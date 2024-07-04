<%@ Page Language="C#" AutoEventWireup="true" Inherits="FinancialSummary" Codebehind="FinancialSummary.aspx.cs" %>



<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Deffinity Reports</title>
    <link href="../stylcss/deffinity_custom.css" rel="stylesheet" type="text/css" />
    <link href="../stylcss/deffinity_frame.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
<div class="clr"></div>

<div class="sec_header" style="width:1000px">
 Financial Summary
 </div>
 <div class="clr"></div>
<div class="sec_table1" style="width:450px">
	
        <table border="0" cellspacing="0" cellpadding="0">          
           
            <tr>
                <td style="width: 150px"><b>Projects</b></td>
                <td style="width: 250PX">
                    <asp:DropDownList    ID="ddlProjects" runat="server" Font-Names="Verdana" > </asp:DropDownList></td>
                <td style="width: 50PX">
                    <asp:ImageButton ID="btnReport" runat="server" OnClick="btnReport_Click" SkinID="ImgViewReport" AlternateText="View Report" ImageAlign="AbsMiddle" />
                </td>
            </tr>
            
              </table>
              </div>
            <br />
            <br />
            <br />
            <br />
            <div class="clr"></div>
            <%--   <tr>--%>
                <%--<td colspan="3">--%>
                <CR:CrystalReportViewer    ID="CrystalReportViewer1" runat="server" AutoDataBind="true" BorderColor="LightGray" BorderStyle="Groove" DisplayGroupTree="False" HasToggleGroupTreeButton="False" ReportSourceID="CrystalReportSource1" ToolbarStyle-BackColor="Silver" Width="1013px" />
                <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="Report1.rpt">
            </Report>
        </CR:CrystalReportSource>
       <%-- </td> 
            </tr>--%>
      
      
   
</div>
    </form>
</body>
</html>
