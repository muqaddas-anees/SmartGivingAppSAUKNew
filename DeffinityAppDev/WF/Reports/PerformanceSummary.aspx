<%@ Page Language="C#" AutoEventWireup="true" Theme="Theme1" Inherits="Reports_perform_summ_page" Codebehind="PerformanceSummary.aspx.cs" %>



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
    <style type="text/css">
        .style1
        {
            width: 94%;
        }
        .style5
        {
            width: 43px;
        }
        .style6
        {
            width: 59px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
<div>
<div class="clr"></div>
<div  >
<div class="sec_header">
RAG Performence Report
 </div>
<div >
	<div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="12%">
                  &nbsp;&nbsp;Resource</td>
 <td width="20%">
    <asp:DropDownList ID="ddlContractors" runat="server">
        </asp:DropDownList>
 </td>
                <td width="4%">
                    &nbsp;&nbsp;Days</td>
                <td colspan="2"width="5%">
   
        <asp:DropDownList ID="ddlDays" runat="server" Width="90px" Font-Names="Verdana" CssClass="txt_field">
            <asp:ListItem Selected="True" Value="-30">30</asp:ListItem>
            <asp:ListItem Value="-60">60</asp:ListItem>
            <asp:ListItem Value="-90">90</asp:ListItem>
            <asp:ListItem Value="-365">Annual</asp:ListItem>
        </asp:DropDownList>
            </td>
            <td width="7%">
             <asp:ImageButton ID="btnReport" runat="server" AlternateText="Get Report" SkinID="ImgViewReport"
                        OnClick="btnReport_Click" />
            </td>
         <td width="52%"></td>
            </tr>
                     
               
        </table>
        <br />
        <br />
        <br />
     
        <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="True"
           Height="400px" reportsourceid="CrystalReportSource1" width="901px"       BorderColor="LightGray" BorderStyle="Groove" DisplayGroupTree="False" HasToggleGroupTreeButton="False" ToolbarStyle-BackColor="Silver"></cr:crystalreportviewer>
        <cr:crystalreportsource id="CrystalReportSource1" runat="server">
<Report FileName="Report9.rpt"><Parameters>
<CR:ControlParameter PropertyName="SelectedValue" ReportName="" Name="@ContractorID" DefaultValue="" ConvertEmptyStringToNull="False" ControlID="ddlContractors"></CR:ControlParameter>
<CR:ControlParameter PropertyName="SelectedValue" ReportName="" Name="@days" DefaultValue="" ConvertEmptyStringToNull="False" ControlID="ddlDays"></CR:ControlParameter>
</Parameters>
</Report>
</cr:crystalreportsource>
   
        
    </div>
  </div>
    </div>
	</div>
    </form>
</body>
</html>
