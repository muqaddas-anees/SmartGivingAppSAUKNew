<%@ Page Language="C#" AutoEventWireup="true" Theme="Theme1"
    Inherits="Default16" Codebehind="CompProj_Delays.aspx.cs" %>

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
<div>
	<div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
        <td colspan="9">
        <asp:ValidationSummary ID="Deffinity"  runat="server" ValidationGroup="Deffibity1" />
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate" Display="None" 
                        ErrorMessage="Please select a date" ToolTip="Please select a date" ValidationGroup="Deffibity1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFromDate"
                      Display="None"  ErrorMessage="Please select a proper date" ToolTip="Please select a proper date" ValidationExpression="\d{2,2}\/\d{2,2}\/\d{4,4}" ValidationGroup="Deffibity1"></asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtToDate"
                       ControlToValidate="txtFromDate" ErrorMessage="From date cannot be greater than to date" Operator="LessThan" ToolTip="From date cannot be greater than to date" ValidationGroup="Deffibity1" Type="Date"></asp:CompareValidator>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                      Display="None"  ErrorMessage="Please select a date" ToolTip="Please select a date" ValidationGroup="Deffibity1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtToDate"
                     Display="None"   ErrorMessage="Please select a proper date" ToolTip="Please select a proper date" ValidationExpression="\d{2,2}\/\d{2,2}\/\d{4,4}" ValidationGroup="Deffibity1"></asp:RegularExpressionValidator>
                  
        <span><font color="#ff6633" size="4"><b></b></font></span>
        </td>
        </tr>
        <tr>
            <td width="19%">
                Select Programme</td>
            <td width="10%">
                <asp:DropDownList ID="ddlOwners" runat="server" Font-Names="Verdana">
                    </asp:DropDownList>
            </td>
            <td width="9%">
                From Date
            </td>
             <td width="17%">
                <asp:TextBox ID="txtFromDate" runat="server" Width="96px" onKeyPress="alert('Please choose a calender control to select the date'); return false;"></asp:TextBox>
                    <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/icon_calender.png" ImageAlign="AbsMiddle" />
                    
            </td>
               <td width="9%">
             To Date
            </td>
             <td width="17%">
                 <asp:TextBox ID="txtToDate" runat="server" Width="96px"  onKeyPress="alert('Please choose a calender control to select the date'); return false;"></asp:TextBox>
                    <asp:Image ID="imgToDate" runat="server" ImageUrl="~/images/icon_calender.png" ImageAlign="AbsMiddle" />
                 
            </td>
            <td width="6%">
            <asp:ImageButton ID="btnReports" runat="server" OnClick="btnReports_Click" AlternateText="Get Report"
                    ImageUrl="~/images/btn_view_report.gif" ToolTip="Click here to view the report"
                    Font-Names="Verdana" ValidationGroup="Deffibity1"/>
             
            </td>
            <td width=13%></td>
        </tr>
        <tr>
            <td colspan="4">
                <font face="Verdana" color="#808080" size="1">
                   <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgFromDate"
                        TargetControlID="txtFromDate"  CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>
            </td>
            <td colspan="5">
                &nbsp;<font face="Verdana" color="#808080" size="1"><ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgToDate"
                        TargetControlID="txtToDate"  CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>
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


<div class="clr"></div>


</div>

</div>
    </form>
</body>
</html>
