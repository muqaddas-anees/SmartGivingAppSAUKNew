<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_GoodsReceiptCostJournal" Codebehind="GoodsReceiptCostJournal.aspx.cs" %>
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
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Verdana, Arial, Helvetica, sans-serif;font-size:13px">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 162%">
                <tr>
                    <td colspan="14" style="height: 16px; width: 1146px;">
                      <asp:ValidationSummary ID="V1" runat="server" ValidationGroup="one" />
                  
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
                            Project Cost Journal By Period
             
                    </td>
                </tr>
                </table>
                <table border="0" width="100%" cellpadding="0" cellspacing="0" >
                <tr><td>From&nbsp;Date:</td><td align="left"> 
                    <asp:TextBox ID="txtFromDate" runat="server" Width="100px"></asp:TextBox> <asp:Image ID="imgExpDate" runat="server" CssClass="MyCalendar" SkinID="Calender" />
                     <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"  PopupButtonID="imgExpDate"
                    TargetControlID="txtFromDate" ></ajaxToolkit:CalendarExtender>
                    <asp:RegularExpressionValidator
                            ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid expected shipment date" Display="None" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                             ControlToValidate="txtFromDate"  ValidationGroup="one"></asp:RegularExpressionValidator></td>
                             <td>To&nbsp;Date:</td>
                   
                    <td align="left"> 
                    <asp:TextBox ID="txtToDate" runat="server" Width="100px"></asp:TextBox><asp:Image ID="imgToDate" runat="server" CssClass="MyCalendar" SkinID="Calender" />
                     <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"  PopupButtonID="imgToDate"
                    TargetControlID="txtToDate" ></ajaxToolkit:CalendarExtender>
                    <asp:RegularExpressionValidator
                            ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter valid expected shipment date" Display="None" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                             ControlToValidate="txtToDate"  ValidationGroup="one"></asp:RegularExpressionValidator></td>
                             <td>Select&nbsp;Customer:</td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCustomer" runat="server" Width="150px" 
                            AutoPostBack="True" onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    </tr>
                    <tr>
                    <td>Select&nbsp;Project:</td>
                    <td align="left">
                        <asp:DropDownList ID="ddlProject" runat="server" Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td colspan="2">
                        <asp:CheckBox ID="chkLive" Text="Live" runat="server" /><asp:CheckBox ID="chkCompleted" Text="Completed" runat="server" /></td>
                        <td>
                        Order&nbsp;By:</td>
                        
                        <td>
                            <asp:DropDownList ID="ddlOrderBy" runat="server" Width="150px">
                            <asp:ListItem Value="0">Select Order By</asp:ListItem>
                                <asp:ListItem Value="P.ProjectReference">Project</asp:ListItem>
                                <asp:ListItem Value="VendorName">Supplier</asp:ListItem>
                                <asp:ListItem Value="G.AuthorizedPay">Pay Authorised</asp:ListItem>
                            </asp:DropDownList></td><td align="left">
                            </td>
                            <td align="left">
                                <asp:ImageButton ID="imgView" runat="server" SkinID="imgViewReport" OnClick="imgView_Click" /></td>
                </tr>
               <tr><td colspan="14"><CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        </CR:CrystalReportSource></td></tr>
        <tr>   
            <td colspan="9">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
        </td>
        </tr>
                </table>
               
    
    </div>
    </form>
</body>
</html>
