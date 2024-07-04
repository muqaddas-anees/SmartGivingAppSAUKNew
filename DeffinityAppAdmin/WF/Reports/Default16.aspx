<%@ Page Language="C#" AutoEventWireup="true" Theme="Theme1"
    Inherits="Reports_Default16" Codebehind="Default16.aspx.cs" %>

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
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 234px;
        }
    </style>
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
 Financial Summary
 </div>
<div class="sec_table1">
	<div>
        <table width="80%" border="0" cellspacing="0" cellpadding="0">
        <tr>
        <td colspan="4">
        <span><font color="#ff6633" size="4"><b></b></font></span>
        </td>
        </tr>
        <tr>
            <td >
                
                    Select Owner
            </td>
            <td>
                
                    <asp:DropDownList ID="ddlOwners" runat="server" Font-Names="Verdana">
                    </asp:DropDownList>
            </td>
            <td>
              
            </td>
            <td>
             
            </td>
        </tr>
        <tr>
            <td>
              
                   From Date
            </td>
            <td class="style2">
                <font face="Verdana" color="#808080" size="1">
                    <asp:TextBox ID="txtFromDate" runat="server" Width="96px" onKeyPress="alert('Please choose a calender control to select the date'); return false;"></asp:TextBox>
                    <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/icon_calender.png" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                        ErrorMessage="*" ToolTip="Please select a date"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFromDate"
                        ErrorMessage="*" ToolTip="Please select a proper date" ValidationExpression="\d{2,2}\/\d{2,2}\/\d{4,4}"></asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtToDate"
                        ControlToValidate="txtFromDate" ErrorMessage="*" Operator="LessThan" ToolTip="From date cannot be greater than to date"></asp:CompareValidator>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgFromDate"
                        TargetControlID="txtFromDate"  CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>
            </td>
            <td>
               To Date
            </td>
            <td>
                <font face="Verdana" color="#808080" size="1">
                    <asp:TextBox ID="txtToDate" runat="server" Width="96px"  onKeyPress="alert('Please choose a calender control to select the date'); return false;"></asp:TextBox>
                    <asp:Image ID="imgToDate" runat="server" ImageUrl="~/images/icon_calender.png" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                        ErrorMessage="*" ToolTip="Please select a date"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtToDate"
                        ErrorMessage="*" ToolTip="Please select a proper date" ValidationExpression="\d{2,2}\/\d{2,2}\/\d{4,4}"></asp:RegularExpressionValidator>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgToDate"
                        TargetControlID="txtToDate"  CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                </td>
            <td class="style2">
                </td>
            <td>
                &nbsp;</td>
            <td>
                <font face="Verdana" color="#808080" size="1" />
                <asp:ImageButton ID="btnReports" runat="server" OnClick="btnReports_Click" AlternateText="Get Report"
                    ImageUrl="~/images/btn_view_report.gif" ToolTip="Click here to view the report"
                    Font-Names="Verdana" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
                Height="1039px" ReportSourceID="CrystalReportSource1" Width="901px" BorderColor="LightGray"
                BorderStyle="Groove" DisplayGroupTree="False" HasToggleGroupTreeButton="False"
                ToolbarStyle-BackColor="Silver" ToolbarStyle-BorderColor="White"></CR:CrystalReportViewer>
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="Report16.rpt">
                    <Parameters>
                        <CR:ControlParameter PropertyName="SelectedValue" ReportName="" Name="@OwnerID" DefaultValue=""
                            ConvertEmptyStringToNull="False" ControlID="ddlOwners"></CR:ControlParameter>
                    </Parameters>
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
