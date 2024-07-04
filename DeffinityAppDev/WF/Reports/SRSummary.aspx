<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_SRSummary" Codebehind="SRSummary.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Request Summary</title>
     <link href="../stylcss/deffinity_frame.css" rel="stylesheet" type="text/css" />
    <link href="../stylcss/deffinity_custom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="bodyframe">
            <div style="clear: both">
            </div>
            <div class="content">
                <div class="content_bodyfull1">
                    <div class="clr">
                    </div>
                    <asp:ValidationSummary ID="ValidationSummary2" runat="Server" />
                    <div class="sec_header">
                        Service Request Summary - Grouped by Category
                    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="Server" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="Server" />
                    <table>
                        <tr>
                            <td>
                                From
                            </td>
                            <td>
                                <asp:TextBox ID="txtFrom" runat="Server" Width="120px" />
                                <asp:RequiredFieldValidator ID="rqdFrom" runat="Server" Display="Dynamic" Text="*"
                                    ErrorMessage="From date is required" ControlToValidate="txtFrom" EnableViewState="False" />
                                <img id="imgFrom" src="../media/icon_calender.png" alt="Open Calendar" enableviewstate="False"
                                    style="border-width: 0px" align="absmiddle" />
                                <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtFrom"
                                    PopupButtonID="imgFrom"  CssClass="MyCalendar" />
                            </td>
                            <td>
                                To
                            </td>
                            <td>
                                <asp:TextBox ID="txtTo" runat="Server" Width="120px" />
                                <img id="imgTo" src="../media/icon_calender.png" alt="Open Calendar" enableviewstate="False"
                                    style="border-width: 0px" align="absmiddle" />
                                <ajaxToolkit:CalendarExtender ID="calTo" CssClass="MyCalendar" runat="Server" 
                                    TargetControlID="txtTo" PopupButtonID="imgTo" />
                                <asp:RequiredFieldValidator ID="rqdToDate" runat="Server" EnableViewState="False"
                                    ControlToValidate="txtTo" Text="*" ErrorMessage="To Date cannot be blank" Display="Dynamic" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnReport" runat="Server" EnableViewState="False"
                                    OnClick="btnReport_Click" SkinID="ImgGetReport" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
