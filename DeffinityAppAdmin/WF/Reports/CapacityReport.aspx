<%@ Page Language="C#" AutoEventWireup="true" Inherits="GanttDefault2"
    Theme="Theme1" Codebehind="CapacityReport.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deffinity Reports</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style5
        {
            width: 274px;
        }
        .style6
        {
            width: 175px;
        }
    </style>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
<div class="sec_header">
Forecast of Live and Pending Projects
</div>
     <asp:Panel ID="panel1" runat="server"  Width="100%">
    <table>
        <tr>
            <td colspan="8">
            <asp:ValidationSummary id="ValidationSummary1" runat="server" ValidationGroup="Group1"></asp:ValidationSummary>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtFromDate"
                                Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                ValidationGroup="Group1"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RC" runat="server" ErrorMessage="Please Enter Date" Display="None" ControlToValidate="txtToDate" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFromDate"
                                Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                ValidationGroup="Group1"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RC1" runat="server" ErrorMessage="Please Enter Date" Display="None" ControlToValidate="txtToDate" ValidationGroup="Group1"></asp:RequiredFieldValidator>
 
   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlContractor"
                    ErrorMessage="Please select Resource Name" InitialValue="Please select..." Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
    
 
            </td>
        </tr>
        <tr>
            <td class="style6" width="8%">
                &nbsp;&nbsp;&nbsp;&nbsp;From Date</td>
            <td width="20%">
                <asp:TextBox ID="txtFromDate" runat="server" Width="104px" CssClass="txt_field" > </asp:TextBox>
                 &nbsp;<asp:Image runat="server" ID="imgFromDate" ImageUrl="~/images/icon_calender.png"
                    AlternateText="Please select a date" ImageAlign="AbsMiddle" Style="cursor:hand;" />
                </td>
                     <td width="7%">&nbsp;&nbsp;&nbsp;&nbsp;
               
                      To&nbsp;Date
          
            </td>
            <%--onKeyPress="alert('Please choose the calender for date');return false;"onKeyPress="alert('Please choose the calender for date');return false;"--%>
            <td width="19%">
               <asp:TextBox ID="txtToDate" runat="server" Width="104px" CssClass="txt_field" ></asp:TextBox>  
               &nbsp;<asp:Image runat="server" ID="imgToDate" ImageUrl="~/images/icon_calender.png" AlternateText="Please select a date" ImageAlign="AbsMiddle"  Style="cursor:hand;" />
            </td>
            <td width="12%">
              Resource
            </td>
             <td width="15%">
              <asp:DropDownList ID="ddlContractor" runat="server" Width="210px" 
                    Font-Names="Verdana" ToolTip="Select a Resource">
                </asp:DropDownList>
            </td>
            <td colspan="2">
                <asp:ImageButton ID="btnReport" runat="server" OnClick="btnReport_Click" AlternateText="Get Report"
                    ImageUrl="~/images/btn_view_report.gif" 
                    ToolTip="Click here to view report" ValidationGroup="Group1" />
            </td>
        </tr>
        <tr>
            <td class="style6">
              </td>
            <td>
              </td>
            <td colspan="2">
   
       </td>
            <td class="style5">
             
        </td>
        <td>
          <ajaxToolkit:CalendarExtender runat="server" ID="calToDate" CssClass="MyCalendar"
                     TargetControlID="txtToDate" PopupButtonID="imgToDate" />
        </td>
        <td colspan="2">
                               
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   </td>
        </tr>
        <tr>
            <td class="style6">
            </td>
            <td>
            </td>
            <td colspan="2">
            </td>
            <td class="style5">
            </td>
            <td>
               <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" 
                    CssClass="MyCalendar" TargetControlID="txtFromDate" PopupButtonID="imgFromDate" /></td>
            <td colspan="2">
            </td>
        </tr>
    </table>
    </asp:Panel>
   
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" 
            BorderColor="LightGray" BorderStyle="Groove" Height="900px" ReportSourceID="CrystalReportSource1"
            Width="800px" DisplayGroupTree="False" HasToggleGroupTreeButton="False" ToolbarStyle-BackColor="Silver" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="GanttReport2.rpt">
            </Report>
        </CR:CrystalReportSource>
        
    </form>
</body>
</html>
