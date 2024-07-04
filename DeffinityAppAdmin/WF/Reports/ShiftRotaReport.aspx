<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_ShiftRotaReport" Codebehind="ShiftRotaReport.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ShiftRota  Report</title>
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
                   
                    <div class="sec_header">
                        ShiftRota  Report
                    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="Server" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="Server" Font-Size="Small" />
                    <asp:Label ID="lblmsg" runat='server' ForeColor="Red" Font-Size='Small'></asp:Label><br />
                    <span style="color:red; font-size:small">Note: Please select a period of less than or equal to 3 months</span>
                    <table>
                        <tr>
                       
                            <td>
                                Customer
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPortfolio" DataSourceID="objPortfolio" runat="Server" DataTextField="Portfolio"
                                    DataValueField="ID" AppendDataBoundItems="True" AutoPostBack="True" 
                                    onselectedindexchanged="ddlPortfolio_SelectedIndexChanged">
                                    <asp:ListItem Text="Please Select.." Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqdPortfolio" runat="Server" InitialValue="0" Display="Dynamic"
                                    ControlToValidate="ddlPortfolio" Text="*" ErrorMessage="Please select a customer" />
                                    <asp:ObjectDataSource ID="objPortfolio" runat="Server" TypeName="DataHelperClass" OldValuesParameterFormatString="original_{0}" SelectMethod="LoadPortfolio" />
                            </td>
                            <td>Team</td>
                            <td>
                                <asp:Panel ID="Panel1" runat="server" Height="70px" 
                                    ScrollBars="Auto" Width="300px" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" >
                                    <asp:CheckBoxList ID="chkTeam" runat="server" RepeatColumns="1" 
                                        RepeatLayout="Flow" Font-Size="Small" />
                                </asp:Panel>
                            </td>
                            <td>From</td>
                            <td>
                                <asp:TextBox ID="txtFrom" runat="Server" Width="80px" />
                                <asp:RequiredFieldValidator ID="rqdFrom" runat="Server" Display="Dynamic" Text="*"
                                    ErrorMessage="Please select Fromdate" ControlToValidate="txtFrom" EnableViewState="False" />
                                <img id="imgFrom" src="../media/icon_calender.png" alt="Open Calendar" enableviewstate="False"
                                    style="border-width: 0px" align="absmiddle" />
                                <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtFrom"
                                    PopupButtonID="imgFrom"  CssClass="MyCalendar" />
                            </td>
                            <td>
                                To
                            </td>
                            <td>
                                <asp:TextBox ID="txtTo" runat="Server" Width="80px" />
                                <img id="imgTo" src="../media/icon_calender.png" alt="Open Calendar" enableviewstate="False"
                                    style="border-width: 0px" align="absmiddle" />
                                <ajaxToolkit:CalendarExtender ID="calTo" CssClass="MyCalendar" runat="Server" 
                                    TargetControlID="txtTo" PopupButtonID="imgTo" />
                                <asp:RequiredFieldValidator ID="rqdToDate" runat="Server" EnableViewState="False"
                                    ControlToValidate="txtTo" Text="*" ErrorMessage="Please select Todate" Display="Dynamic" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnReport" runat="Server" EnableViewState="False"
                                    OnClick="btnReport_Click" SkinID="ImgGetReport" />
                            </td>
                        </tr>
                        <tr>
                        <td colspan="8">
                        <asp:CheckBoxList ID="chkboxlistteams" runat='server'></asp:CheckBoxList>
                        </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
