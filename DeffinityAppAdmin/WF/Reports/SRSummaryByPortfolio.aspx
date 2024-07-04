<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Reports_SRSummaryByPortfolio" Codebehind="SRSummaryByPortfolio.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SR</title>
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
                                Customer
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPortfolio" DataSourceID="objPortfolio" runat="Server" DataTextField="Portfolio"
                                    DataValueField="ID" AppendDataBoundItems="True">
                                    <asp:ListItem Text="Please Select.." Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqdPortfolio" runat="Server" InitialValue="0" Display="Dynamic"
                                    ControlToValidate="ddlPortfolio" Text="*" ErrorMessage="Please select the portfolio" />
                                    <asp:ObjectDataSource ID="objPortfolio" runat="Server" TypeName="DataHelperClass" OldValuesParameterFormatString="original_{0}" SelectMethod="LoadPortfolio" />
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
