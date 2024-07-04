<%@ Page Language="C#" AutoEventWireup="true" Inherits="HC_FormPreview" EnableEventValidation="false" Codebehind="FormPreview.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <%-- <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_frame.css" />
<link rel="stylesheet" type="text/css" href="../stylcss/deffinity_color_scheme.css" />
<link rel="stylesheet" type="text/css" href="../stylcss/deffinity_custom.css" />
<link rel="stylesheet" type="text/css" href="../stylcss/ajaxtabs.css" />
    <link href="../stylcss/deffinity_chat.css" type="text/css" rel="stylesheet" />
     <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
     <script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="jQuery.print.js"></script>
    <link rel="stylesheet" href="../stylcss/ButtonStyle.css"/>
      <link rel="stylesheet" href="../stylcss/HCstyle.css"/>
    <script type="text/javascript" src="../Scripts/HCform.js"></script>--%>
    
  <%--  <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
  <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
  <script src="http://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    
     <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
<%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/jqueryui") %>
<%: System.Web.Optimization.Styles.Render("~/bundles/formscss") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/forms") %>
<%: System.Web.Optimization.Styles.Render("~/bundles/bootstarpcss") %>

  

     
    <script type="text/javascript">
        $(document).ready(function () {
            $("#hrefPrint").click(function () {
                //$("#printdiv").print();
                var divContents = $("#printdiv").html();
                var printWindow = window.open('', '', 'height=800,width=1200');
                printWindow.document.write('<html><head><title>DIV Contents</title>');
                printWindow.document.write('</head><body >');
                printWindow.document.write(divContents);
                printWindow.document.write('</body></html>');
                printWindow.document.close();
                printWindow.print();
                return (false);
            });
        });
</script>
     
  
<head runat="server">
    <title></title>
    
    
    <style>
        .tblcontrol
 {
     
     font-family:Arial;
     font-size:93%;
 }
        .center {
    margin: auto;
    width: 70%;
    /*border:3px solid #8AC007;*/
    padding: 10px;
    float:left;
    

}
@media print {
  h2 { 
    page-break-before: always;
  }
   tr { 
    page-break-before: always;
  }
  h3, h4 {
    page-break-after: avoid;
  }
  pre, blockquote {
    page-break-inside: avoid;
  }
}

span.mobilesubtitle {
    display: table;

}
span.mobilesubtitle > input{
    display: table-cell;

}


span.mobilesubtitle > label {
    display: table-cell;
    vertical-align: top;
}
    </style>
</head>
<body style="background-color:white;">
    <form id="form1" runat="server">
        
    <div class="center">
        <div style="padding-bottom:3px">
            <div class="form-group row">
       <div class="col-md-6 form-inline">
           <a id="hrefPrint" class="btn btn-primary" style="margin-bottom:0px">Print</a> 
           <asp:Button ID="btn" runat="server"  CssClass="btn btn-primary" Text="Return to Form Editor" OnClick="btn_Click" />
            <asp:Button ID="btnPrint" runat="server" Text="WK Print" OnClick="btnPrint_Click" Visible="false"/>
           </div>
           <div class="col-md-6">
               </div>
             </div>
     </div>
        <asp:Panel id="printdiv" runat="server" ClientIDMode="Static" >
    <asp:PlaceHolder ID="ph" runat="server" ClientIDMode="Static"></asp:PlaceHolder></asp:Panel>
    </div>

      

          <script type="text/javascript">
              //apply date 
              applyDatePicker();
          </script>
    </form>
</body>
</html>
