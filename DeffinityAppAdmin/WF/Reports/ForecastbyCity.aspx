<%@ Page Language="C#" AutoEventWireup="true" Theme="Theme1"
    Inherits="GanttDefault1" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" Codebehind="ForecastbyCity.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deffinity Reports</title>
    <link href="../css/defficss.css" rel="stylesheet" type="text/css" />
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
   
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
     
    <div class="bodyframe">
<div class="header" >
<div style="clear:both"></div>
<div class="content">
<div class="content_bodyfull1">
<%--<div class="sections">--%>
<div class="clr"></div>
<div  class="sec_body1">
<div class="sec_header">
Forecast of Live and Pending Projects by City
 </div>
<div>
	<div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
             <tr>
                <td  colspan="8" style="height: 58px" >
                <asp:ValidationSummary ID="P1" ValidationGroup="Group1" runat="server" />
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate" Display="none" 
                            ErrorMessage="Please enter the Date" ToolTip="Please select a date" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFromDate" Display="none" 
                            ErrorMessage="Please enter valid Date" ValidationExpression="\d{2,2}\/\d{2,2}\/\d{4,4}" ValidationGroup="Group1"></asp:RegularExpressionValidator>
                       <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="txtToDate" ControlToValidate="txtFromDate" ErrorMessage="*" 
                        Operator="LessThan" ToolTip="From date cannot be greater than To date"></asp:CompareValidator>
                       --%>   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate" Display="none" 
                            ErrorMessage="Please enter the Date" ToolTip="Please select a date" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtToDate" Display="none" 
                            ErrorMessage="Please enter valid Date" ValidationExpression="\d{2,2}\/\d{2,2}\/\d{4,4}" ValidationGroup="Group1"></asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtFromDate"
                ControlToValidate="txtToDate" Display="None" ErrorMessage="From date can't be greater than To date"
                Operator="GreaterThan" Type="Date" ValidationGroup="Group1"></asp:CompareValidator></td>
               
            </tr>
            <tr> 
                <td width="7%">From Date</td>
                <td colspan="2" width="12%">
                   <asp:TextBox ID="txtFromDate" runat="server" Width="104px" CssClass="txt_field"></asp:TextBox><asp:Image runat="server" ID="imgFromDate" ImageUrl="~/images/icon_calender.png"
                            AlternateText="Pick a date" ImageAlign="AbsMiddle"  Style="cursor:hand;" />
                      </td>
                <td width="6%">To Date</td>
                <td colspan="2" width="12%">
                        <asp:TextBox ID="txtToDate" runat="server" Width="104px"></asp:TextBox><asp:Image ImageUrl="~/images/icon_calender.png" runat="server" ID="imgToDate" AlternateText="Pick a date" ImageAlign="AbsMiddle" Style="cursor:hand;" />
                      </td>
                <td width="8%"> Select City</td>
                <td width="14%">
                   <asp:DropDownList ID="ddlCity" runat="server" Font-Names="Verdana" CssClass="txt_field"
                            meta:resourcekey="ddlCityResource1" ToolTip="Select a city" Width="129px"></asp:DropDownList>
                                  </td>
                <td width="41%">
                <asp:ImageButton ID="btnReport" runat="server" OnClick="btnReport_Click" ImageUrl="~/images/btn_view_report.gif"
                            AlternateText="Get Report" meta:resourcekey="btnReportResource1" ToolTip="Click here to view the report" ImageAlign="AbsMiddle" ValidationGroup="Group1" />
                 
                        </td>
            </tr>
        
     </table>
     </div>
    <div class="sec_table1">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
                BorderColor="LightGray" BorderStyle="Groove" Height="799px" ReportSourceID="CrystalReportSource1"
                Width="1013px" DisplayGroupTree="False" HasToggleGroupTreeButton="False" ToolbarStyle-BackColor="Silver"
                meta:resourcekey="CrystalReportViewer1Resource1" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="GanttReport1.rpt">
                </Report>
            </CR:CrystalReportSource>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                            PopupButtonID="imgFromDate" CssClass="MyCalendar" TargetControlID="txtFromDate">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
                             PopupButtonID="imgToDate" TargetControlID="txtToDate">
                        </ajaxToolkit:CalendarExtender>
      
        
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
