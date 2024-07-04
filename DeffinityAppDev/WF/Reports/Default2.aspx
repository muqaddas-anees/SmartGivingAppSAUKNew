<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_Default2"
    Theme="Theme1" Codebehind="Default2.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deffinity Reports</title>
    <link href="../css/defficss.css" rel="stylesheet" type="text/css" />
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
Cost Variance Report 
 </div>
<div class="sec_table1">
	<div>
        <table width="80%" border="0" cellspacing="0" cellpadding="0">
 
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label><br />
      
                <tr>
                    <td width="10%" >From Date</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtFromDate" runat="server" Width="104px" onKeyPress="alert('Please choose the Calender to select the date');return false;"   MaxLength="10"></asp:TextBox>
                        <%--<asp:Image runat="server" ImageUrl="~/images/icon_calender.png" AlternateText="Pick a date"    ID="imgFrom" ImageAlign="AbsMiddle" />--%>
                        <A HREF="#"><img src="images/icon_calender.png" alt="Pick a date" id="imgFrom" style="border:0px" /></A>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                            ErrorMessage="*" ToolTip="Please select the date"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFromDate"
                            ErrorMessage="*" ToolTip="Please enter the proper date" ValidationExpression="\d{2,2}\/\d{2,2}\/\d{4,4}"></asp:RegularExpressionValidator></td>
                            <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td> To Date</td>
                    <td colspan="3" style="height: 14px">
                        <asp:TextBox ID="txtToDate" runat="server" onKeyPress="alert('Please choose the Calender to select the date');return false;"                    Width="104px"></asp:TextBox><asp:Image ID="imgTo" runat="server" ImageUrl="~/images/icon_calender.png" AlternateText="Pick a date" ImageAlign="AbsMiddle" />&nbsp;
                        <asp:ImageButton ID="btnReport" runat="server" ImageUrl="~/images/btn_view_report.gif"
                            AlternateText="Get Report" OnClick="btnReport_Click" />
                            <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ToolTip="Please enter the to date" ErrorMessage="*" ControlToValidate="txtToDate"></asp:RequiredFieldValidator><asp:RegularExpressionValidator id="valToDate" runat="server" ToolTip="Please enter the proper date" ErrorMessage="*" ControlToValidate="txtToDate" ValidationExpression="\d{2,2}\/\d{2,2}\/\d{4,4}"></asp:RegularExpressionValidator>
                            </td>
                </tr>
                <tr>
                    <td colspan="6">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
                Height="1039px" ReportSourceID="CrystalReportSource1" Width="901px" BorderColor="LightGray"
                BorderStyle="Groove" ToolbarStyle-BackColor="Silver" ToolbarStyle-BorderColor="White"
                DisplayGroupTree="False" />
                    </td>
                    
                </tr>
                  <ajaxToolkit:CalendarExtender ID="calFrom" runat="server"  PopupButtonID="imgFrom"
                    TargetControlID="txtFromDate" CssClass="MyCalendar">
                </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:CalendarExtender ID="calTo" runat="server"  PopupButtonID="imgTo"
                TargetControlID="txtToDate" CssClass="MyCalendar">
            </ajaxToolkit:CalendarExtender>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
          </table>
          <br />
          <br />
          <br />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="Report2.rpt">
                    <Parameters>
                        <CR:ControlParameter ControlID="txtFromDate" ConvertEmptyStringToNull="False" DefaultValue=""
                            Name="@date1" PropertyName="Text" ReportName="" />
                        <CR:ControlParameter ControlID="txtToDate" ConvertEmptyStringToNull="False" DefaultValue=""
                            Name="@date2" PropertyName="Text" ReportName="" />
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
