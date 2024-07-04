<%@ Page Language="C#" AutoEventWireup="true" Inherits="HC_FormDataPreview" Codebehind="FormDataPreview.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <%-- <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_frame.css" />
<link rel="stylesheet" type="text/css" href="../stylcss/deffinity_color_scheme.css" />
<link rel="stylesheet" type="text/css" href="../stylcss/deffinity_custom.css" />--%>
<%--<link rel="stylesheet" type="text/css" href="../stylcss/ajaxtabs.css" />--%>
   <%-- <link href="../stylcss/deffinity_chat.css" type="text/css" rel="stylesheet" />--%>
     <%-- <script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
<%: System.Web.Optimization.Scripts.Render("~/bundles/jqueryui") %>
<%: System.Web.Optimization.Styles.Render("~/bundles/formscss") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/forms") %>
<%: System.Web.Optimization.Styles.Render("~/bundles/bootstarpcss") %>
        <script type="text/javascript" src="jQuery.print.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>

    <style type="text/css">
       .tblcontrol
 {
     
     font-family:Arial;
     font-size:93%;
 }
.center {
    /*margin: auto;*/
    width: 100%;
    /*border:3px solid #8AC007;*/
    /*padding: 10px;*/
}

    </style>
    
</head>
    
<body>
    <form id="form1" runat="server">
    <div class="center">
      <%--  <div style="padding-bottom:3px;">
        <a id="hrefPrint" class="button deffinity medium" >Print</a>
            </div>--%>
         <asp:Panel id="printdiv" runat="server" ClientIDMode="Static" >
     <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
             &nbsp;</asp:Panel>
    </div>
         <script type="text/javascript">
             applyDatePicker();
     </script>
    </form>
</body>
</html>
