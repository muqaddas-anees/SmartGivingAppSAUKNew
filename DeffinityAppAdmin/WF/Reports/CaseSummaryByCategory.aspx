<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Reports_CaseSummaryByCategory" Codebehind="CaseSummaryByCategory.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SR Summary By Category</title>
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
                    <asp:ValidationSummary ID="ValidationSummary1" runat="Server" />
                    <div class="sec_header">
                        Service Request By Category
                    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="Server" />
                    <table>
                        <tr>
                            <td>
                                Type
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSRType" runat="Server" Width="120px">
                                    <asp:ListItem Text="Please Select.." Value="0" />
                                    <asp:ListItem Text="Service Request" Value="Service Request" />
                                    <asp:ListItem Text="Fault" Value="Fault" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqdSRType" runat="Server" ControlToValidate="ddlSRType"
                                    Text="*" ErrorMessage="Please select the Service Request Type" InitialValue="0"
                                    EnableViewState="False" Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Logged Between
                            </td>
                            <td>
                                <asp:TextBox ID="txtFrom" runat="server" Width="120px" />
                                <img id="imgFrom" src="../media/icon_calender.png" alt="Open Calendar" enableviewstate="False"
                                    style="border-width: 0px" align="absmiddle" />
                                <ajaxToolkit:CalendarExtender ID="calFrom" CssClass="MyCalendar" runat="Server" 
                                    TargetControlID="txtFrom" PopupButtonID="imgFrom" />
                                <asp:RequiredFieldValidator ID="rqdFromDate" runat="Server" EnableViewState="False"
                                    ControlToValidate="txtFrom" Text="*" ErrorMessage="From Date cannot be blank"
                                    Display="Dynamic" />
                                &nbsp;&nbsp;&nbsp; and &nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtTo" runat="Server" Width="120px" />
                                <img id="imgTo" src="../media/icon_calender.png" alt="Open Calendar" enableviewstate="False"
                                    style="border-width: 0px" align="absmiddle" />
                                <ajaxToolkit:CalendarExtender ID="calTo" CssClass="MyCalendar" runat="Server" 
                                    TargetControlID="txtTo" PopupButtonID="imgTo" />
                                <asp:RequiredFieldValidator ID="rqdToDate" runat="Server" EnableViewState="False"
                                    ControlToValidate="txtTo" Text="*" ErrorMessage="To Date cannot be blank" Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:ImageButton ID="btnReport" runat="Server" EnableViewState="False" SkinID="ImgGetReport" OnClick="btnReport_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div>
                <%--<CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true" DisplayGroupTree="False" HasCrystalLogo="False" />--%>
            </div>
        </div>
    </form>
</body>
</html>
