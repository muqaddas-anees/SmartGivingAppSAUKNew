<%@ Page Language="C#" AutoEventWireup="true" Theme="Theme1"
    Inherits="ReportView" Codebehind="ReportView.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resource View</title>
     <link href="../stylcss/deffinity_frame.css" rel="stylesheet" type="text/css" />
    <link href="../stylcss/deffinity_custom.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     
  
<div  class="sec_body1">
<div class="sec_header">
 Resource View
 </div>
<div >
	<div>
        <table width="80%" border="0" cellspacing="0" cellpadding="0">
        <tr>
                    <td width="30%" align="right" valign="middle">
                       <b>Select Resources</b>
                    </td>
                    <td width="50%" align="left" colspan="2">
                        <asp:ListBox ID="ListBox1" CssClass="txt_field"  runat="server" SelectionMode="Multiple"
                            DataTextField="ContractorName" DataValueField="ID"></asp:ListBox>
                                          <asp:ImageButton ID="BtnView" runat="server" OnClick="BtnView_Click" AlternateText="View Report"
                            ImageUrl="~/images/btn_view_report.gif" />
                            
                             <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_AssignedResource" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                          
                        </SelectParameters>
                    </asp:SqlDataSource>
                    </td>
                   
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="red" Font-Size="Small"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
          </table>
          <br />
          <br />
          <br />
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" BorderColor="LightGray" BorderStyle="Groove"
        DisplayGroupTree="False"   HasToggleGroupTreeButton="False"
                ToolbarStyle-BackColor="Silver"/>
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    </CR:CrystalReportSource>
      

        
    </div>
  </div>
    </div>
	
    </form>
</body>
</html>
