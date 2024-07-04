<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_ResourceReport" Codebehind="PortfolioResourceRpt.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Resource Report</title>
    <link href="../stylcss/deffinity_frame.css" rel="stylesheet" type="text/css" />
    <link href="../stylcss/deffinity_custom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        
        <div class="sec_header">
            Resource Report</div>
        <div>
            <table style="width: 70%">
                <tr>
                    <td colspan="2" align="right">
                        Customer
                    </td>
                    <td colspan="2" align="center">
                        <asp:DropDownList ID="ddlportfolio" runat="server" DataSourceID="SqlDataSource1"
                            DataTextField="PortFolio" DataValueField="ID">
                        </asp:DropDownList>
                    </td>
                    <td colspan="1">
                        <asp:ImageButton ID="btnViewrpt" runat="server"
                            OnClick="btnViewrpt_Click" SkinID="ImgViewReport" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <asp:Panel ID="pnlrpt" runat="server">
        </asp:Panel>
        <table style="width: 80%">
            <tr>
                <td align="center">
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                        BorderColor="LightGray" BorderStyle="Groove" DisplayGroupTree="False" HasToggleGroupTreeButton="False"
                        ReportSourceID="CrystalReportSource1" ToolbarStyle-BackColor="Silver" 
                        Width="1013px" onunload="CrystalReportViewer1_Unload" />
                </td>
            </tr>
        </table>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="">
            </Report>
        </CR:CrystalReportSource>
    </div>
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
            SelectCommand="SELECT ID, PortFolio FROM ProjectPortfolio"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
