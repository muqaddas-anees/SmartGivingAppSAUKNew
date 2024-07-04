<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_ServiceRequestmianpivote" Codebehind="ServiceRequestmianpivote.aspx.cs" %>
<%--<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <title></title>
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
            <table width="100%">
               
                <tr>
                    <td colspan="7">
                        <asp:ValidationSummary ID="V1" runat="server" ValidationGroup="one" />
                  
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txt_StartDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one" >*</asp:RegularExpressionValidator>
               </td>
                    <td >
                    </td>
                     <td >
                    </td>
                </tr>
                <tr>
                    <td colspan="9" class="sec_header">
                        Service Area Request</td>
                </tr>
                <tr>
                <td style="width: 10px">
                </td>
                <td width="160px">
                <asp:DropDownList ID="ddlmainCategory" runat="server" Width="160px"  DataSourceID="DDLCustomer"
                                                DataTextField="ContractorName" DataValueField="IncidentID" Visible="False" >
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="DDLCustomer" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                        SelectCommand="DN_SRRResourceName" SelectCommandType="StoredProcedure">
                                      
                                    </asp:SqlDataSource>
                </td>
                
                    <td width="10px">
                        Date&nbsp;
                    </td>
                    <td width="110px">
                        <asp:TextBox ID="txt_StartDate" runat="server" Width="65px" MaxLength="10">
                        </asp:TextBox>
                        <asp:Image ID="imgbtnenddate6" runat="server" SkinID="Calender" />
                    </td>
                    <td width="10px">
        <asp:ImageButton ID="btn_Submitt" runat="server" SkinID="ImgView" OnClick="btn_Submitt_Click"
                            ValidationGroup="one"  /></td>
                    <td width="110px">
                        &nbsp;
                    </td>
                    <td width="260px">
                        &nbsp;</td>
        <td width="10px" style="text-align:left;">
            &nbsp;</td>
          <td ></td>
                </tr>
                
               
            </table>
        </div>
       <div>
            <table width="100%">
                 <tr>
                    <td colspan="4">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
                 <tr>
                    <td colspan="4">
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                            PopupButtonID="imgbtnenddate6" TargetControlID="txt_StartDate" CssClass="MyCalendar">
                        </ajaxToolkit:CalendarExtender>
                      
                    </td>
                    <td>
                    </td>
                </tr>
                </table>
                </div>
        <div>
            <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>--%>
        </div>
    </form>
</body>
</html>
