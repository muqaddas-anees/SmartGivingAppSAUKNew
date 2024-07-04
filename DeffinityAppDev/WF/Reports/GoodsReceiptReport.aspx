<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_GoodsReceiptReport" Codebehind="GoodsReceiptReport.aspx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <link href="http://www.deffinity.com/dlite/media//favicon1.ico" rel="shortcut icon" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_frame.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_color_scheme.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_custom.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/ajaxtabs.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/customer_admin.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <div style="font-family:Verdana, Arial, Helvetica, sans-serif;font-size:13px">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="11" style="height: 16px">
                      <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="one" />
                  
 <%--<asp:CompareValidator ID="c1" runat="server" ControlToCompare="txt_StartDate" ControlToValidate="txt_EndDate"
                        Display="none" Type="Date" Operator="GreaterThanEqual" ErrorMessage="start date can not greater then end date" ValidationGroup="one" ></asp:CompareValidator>      
                        
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txt_StartDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one" >*</asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_EndDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one">*</asp:RegularExpressionValidator>
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>--%></td>
                </tr>
               
                <tr>
                    <td colspan="11" class="sec_header">
                            Project Goods Receipt Report
             
                    </td>
                </tr>
                 <tr>
                <td colspan="11"><div style="width:270px" >
                <span>Select Project:</span><asp:DropDownList ID="ddlProjects" Width="150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged">
                </asp:DropDownList>
                </div></td>
                </tr>
                <tr><td><CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        </CR:CrystalReportSource></td></tr>
                </table>
    
    </div>
    
    </form>
</body>
</html>
