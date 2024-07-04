<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_ResourcePlanner" Codebehind="ResourcePlanner.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deffinity - Resource Planner</title>
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
                        Resource Planner
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
                                <asp:DropDownList ID="ddlPortfolio" runat="Server" DataTextField="PortFolio" DataValueField="ID" 
                       DataSourceID="SqlDataSourceTitle2">
                                   
                                </asp:DropDownList>
                                   <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_PermissionCustomer" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                            </td>
                            <td>&nbsp;</td>
                           
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
                            <td><asp:LinkButton ID="btnPdf" runat="server" Text="Get Report in pdf" 
                                    Font-Bold="true" Font-Underline="true" ForeColor="Navy" onclick="btnPdf_Click" Font-Size="Small" Visible="false"></asp:LinkButton> </td>
                        </tr>
                        </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
