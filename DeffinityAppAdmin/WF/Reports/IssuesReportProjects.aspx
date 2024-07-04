<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_IssuesReportProjects" Codebehind="IssuesReportProjects.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link href="../App_Themes/Theme1/defficss.css" type="text/css" rel="stylesheet" /><link href="../App_Themes/Theme1/ss_portalcss.css" type="text/css" rel="stylesheet" />
    <title>Issues</title>
     <link href="../stylcss/deffinity_frame.css" rel="stylesheet" type="text/css" />
    <link href="../stylcss/deffinity_custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 172px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div> <br /></div>
    <div class="sec_header" style="width:1063px">
 Projects
 </div>
 <div class="clr"></div>
<div class="sec_table1" style="width:507px">
    <table border="0" cellspacing="0" cellpadding="0">   
        <tr>
            <td class="style1">
                <b>Projects</b>
            </td>
            <td style="width: 250PX">
                <asp:DropDownList ID="ddlProjects" runat="server" Font-Names="Verdana" 
                    onselectedindexchanged="ddlProjects_SelectedIndexChanged" 
                    >
                </asp:DropDownList>
            </td>
            <td style="width: 50PX">
                <asp:ImageButton ID="btnReport" runat="server" OnClick="btnReport_Click" SkinID="ImgViewReport"
                    AlternateText="Get Report" ImageAlign="AbsMiddle" ValidationGroup="project" />
           <%--  <asp:ObjectDataSource ID="DS_Projects" runat="server" 
                    TypeName="Deffinity.Bindings.DefaultDatabind" 
                    OldValuesParameterFormatString="original_{0}" 
                    SelectMethod="PrjectTitleWithProjectReference_withSelectAll" >                
                </asp:ObjectDataSource>   --%>    
            </td>
            
        </tr>
        <tr>
            <td class="style1">
            </td>
            <td>
                <asp:RequiredFieldValidator ID="Reqproject" runat="server" InitialValue="0" ControlToValidate="ddlProjects"
                    ErrorMessage="Please select project" ValidationGroup="project"></asp:RequiredFieldValidator>
            </td>
    </tr>
    </table>
 </div>
    </form>
</body>
</html>
