<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_ProgramReport_Programme" Codebehind="ProgramReport_Programme.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Programme Overview Report</title>
  <link rel="stylesheet" type="text/css" href="~/stylcss/deffinity_frame.css" />
<link rel="stylesheet" type="text/css" href="~/stylcss/deffinity_color_scheme.css" />
<link rel="stylesheet" type="text/css" href="~/stylcss/deffinity_custom.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        
        <div class="sec_header">
            Programme Report</div>
        <div>
            <table style="width: 60%">
                <tr>
                    <td style="width:15%">
                        Programme
                    </td>
                    <td style="vertical-align:middle">
                        <asp:DropDownList ID="ddlprogramme" runat="server" DataSourceID="SqlDataSource1"
                            DataTextField="OperationsOwners" DataValueField="ID">
                        </asp:DropDownList>
                        <asp:ImageButton ID="btnViewrpt" runat="server" ImageUrl="~/images/btn_view_report.gif"
                            OnClick="btnViewrpt_Click" ImageAlign="AbsMiddle" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <asp:Panel ID="pnlrpt" runat="server">
        </asp:Panel>
       
        
    </div>
    <div>
        <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
            SelectCommand="SELECT ID,OperationsOwners FROM OperationsOwners"></asp:SqlDataSource>--%>
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_AssignedProgramme" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
