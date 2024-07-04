<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_TimeSheetSelectResource" Codebehind="TimesheetSummary.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>Time Sheet Reports</title>
</head>
<body>
    <form id="form1" runat="server">
     
    <div class="bodyframe">
<div class="header" >
<div style="clear:both"></div>
<div class="content">
<div class="content_bodyfull1">
<%--<div class="sections">--%>
<div class="clr"></div>
<div  class="sec_body1">
<div class="sec_header">
Time Sheet For Selected Resource</div>
<div class="sec_table1">
    <div>
    <table width="100%">
        <tr>
            <td colspan="5">
              <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Group4" />
               <asp:RequiredFieldValidator ID="RFP" runat="server" ErrorMessage="Please select Resource" ControlToValidate="ddslectresource" ValidationGroup="Group4" Display="None" InitialValue="Please select"
        ></asp:RequiredFieldValidator>
            </td>
        </tr>
    <tr>
    <td width="15%" >
        <b>Select Resource</b></td>
        <td colspan="4">
    <asp:DropDownList ID="ddslectresource" runat="server" Width="200px"></asp:DropDownList>
    <asp:ImageButton ID="btn_view" runat="server" ImageUrl ="~/images/btn_view.gif" ImageAlign="AbsMiddle" OnClick="btn_view_Click" ValidationGroup="Group4" />
        </td>
   
    </tr>
    
    
    <tr>
    <td colspan="5">
     <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
                Height="1039px" ReportSourceID="CrystalReportSource1" Width="901px" BorderColor="LightGray"
                BorderStyle="Groove" DisplayGroupTree="False" HasToggleGroupTreeButton="False"
                ToolbarStyle-BackColor="Silver" ToolbarStyle-BorderColor="White"></CR:CrystalReportViewer>
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                
            </CR:CrystalReportSource>
    </td>
    </tr>
    </table>
    </div>
     </div>
    </div>
	</div>
</div>
</div>

<div class="clr"></div>




</div>
    </form>
</body>
</html>
