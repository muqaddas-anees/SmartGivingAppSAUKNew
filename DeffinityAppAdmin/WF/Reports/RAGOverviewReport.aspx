<%@ Page Language="C#" AutoEventWireup="true" Inherits="RAGOverviewReport" Codebehind="RAGOverviewReport.aspx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../stylcss/deffinity_custom.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Programme</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="sec_header">
           Project
           RAGs Overview Report</div>
        <div>
        <table style="width: 70%">
                <tr>
                    <td colspan="2" align="right">
                       Programme
                    </td>
                    <td colspan="2" align="center">
                        <asp:DropDownList ID="ddlprogram" runat="server" DataSourceID="SqlDataSource1"
                            DataTextField="OperationsOwners" DataValueField="ID">
                        </asp:DropDownList>
                    </td>
                    <td colspan="1">
                        <asp:ImageButton ID="btnViewrpt" runat="server"
                            OnClick="btnViewrpt_Click" SkinID="ImgViewReport" />
                    </td>
                </tr>
            </table>
    </div>
     <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
            SelectCommand="SELECT ID,OperationsOwners FROM OperationsOwners"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
