<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_ProjectVariancePage"
    Theme="Theme1" Codebehind="ProjectVariance.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deffinity Reports</title>
  
    <link href="../stylcss/deffinity_frame.css" rel="stylesheet" type="text/css" />
    <link href="../stylcss/deffinity_custom.css" rel="stylesheet" type="text/css" />
   <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
  
    <div class="frm_space">
    <div class="home_canvase"> 
<div class="sec_header">
Cost Variance Report 
 </div>

        <table width="70%" border="0" cellspacing="0" cellpadding="0">
        <tr>
        <td colspan="7">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                            ErrorMessage="Enter From Date" ToolTip="Please select the date" Display="None" ValidationGroup="G1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFromDate"
                            ErrorMessage="Enter Valid From Date" ToolTip="Please enter the valid date" ValidationExpression="\d{2,2}\/\d{2,2}\/\d{4,4}" Display="None" ValidationGroup="G1"></asp:RegularExpressionValidator>
                           <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ToolTip="Please enter the to date" ErrorMessage="Enter To Date" ControlToValidate="txtToDate" Display="None" ValidationGroup="G1"></asp:RequiredFieldValidator><asp:RegularExpressionValidator id="valToDate" runat="server" ToolTip="Please enter the proper date" ErrorMessage="Enter Valid To Date" ControlToValidate="txtToDate" ValidationExpression="\d{2,2}\/\d{2,2}\/\d{4,4}" Display="None" ValidationGroup="G1"></asp:RegularExpressionValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtFromDate"
                ControlToValidate="txtToDate" Display="None" ErrorMessage="From date can't be greater than To date"
                Operator="GreaterThan" Type="Date" ValidationGroup="G1"></asp:CompareValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="X-Small"
                ValidationGroup="G1" />
        </td>
        </tr>
   <tr>
                    <td >From&nbsp;Date</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtFromDate" runat="server" Width="104px"    MaxLength="10" ValidationGroup="G1"></asp:TextBox>
                        <asp:Image ID="Image4" runat="server" SkinID="Calender" />
                          </td>
                          <td>
                    &nbsp;&nbsp;&nbsp;To&nbsp;Date</td>
                           <td >
                              <asp:TextBox ID="txtToDate" runat="server" Width="104px"    MaxLength="10" ValidationGroup="G1"  />
                            <asp:Image runat="server" ID="imgTo" SkinID="Calender" />
                              </td>
                           <td >
                            <asp:ImageButton ID="btnReport" runat="server"
                            AlternateText="Get Report" OnClick="btnReport_Click" ValidationGroup="G1" 
                                   SkinID="ImgViewReport" />
                    </td>
                           <td>
                    </td>
                </tr>
          
                <tr>
                    <td colspan="6">
           
                    </td>
                    
                </tr>
                  <ajaxToolkit:CalendarExtender ID="calFrom" runat="server"  PopupButtonID="Image4"
                    TargetControlID="txtFromDate" CssClass="MyCalendar">
                </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:CalendarExtender ID="calTo" runat="server"  PopupButtonID="imgTo"
                TargetControlID="txtToDate" CssClass="MyCalendar">
            </ajaxToolkit:CalendarExtender>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
          </table>
<div class="clr"></div>
</div>
</div>
    </form>
</body>
</html>
