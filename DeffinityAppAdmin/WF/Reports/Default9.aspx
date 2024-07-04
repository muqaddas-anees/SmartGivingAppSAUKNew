<%@ Page Language="C#" AutoEventWireup="true" Theme="Theme1" Inherits="Default9" Codebehind="Default9.aspx.cs" %>



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
   
  
    <div class="bodyframe">
<div class="header" >
<div style="clear:both"></div>
<div class="content">
<div class="content_bodyfull1">
<%--<div class="sections">--%>
<div class="clr"></div>
<div  class="sec_body1">
<div class="sec_header">
RAG Performence Report
 </div>
<div class="sec_table1">
	<div>
        <table width="80%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="14%">
                  Select Resourecs</td>
         
 
                <td>
    
        <asp:DropDownList ID="ddlContractors" runat="server" Font-Names="Verdana" CssClass="txt_field">
        </asp:DropDownList>
 
                </td>
                <td >
                    Days</td>
                <td colspan="2">
   
        <asp:DropDownList ID="ddlDays" runat="server" Width="90px" Font-Names="Verdana" CssClass="txt_field">
            <asp:ListItem Selected="True" Value="-30">30</asp:ListItem>
            <asp:ListItem Value="-60">60</asp:ListItem>
            <asp:ListItem Value="-90">90</asp:ListItem>
            <asp:ListItem Value="-365">Annual</asp:ListItem>
        </asp:DropDownList>
                    <asp:ImageButton ID="btnReport" runat="server" AlternateText="Get Report" ImageUrl="~/images/btn_view_report.gif"
                        OnClick="btnReport_Click" />
         </td>
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
</div>
</div>

<div class="clr"></div>



</div>
    </form>
</body>
</html>
