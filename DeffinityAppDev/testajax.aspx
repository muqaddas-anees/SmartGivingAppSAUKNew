<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testajax.aspx.cs" Inherits="DeffinityAppDev.testajax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
     <meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<meta name="description" content="" />
	<meta name="author" content="" />
    <title></title>
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Arimo:400,700,400italic"/>
	<%--<link rel="stylesheet" href="../Content/assets/css/fonts/linecons/css/linecons.css"/>
	<link rel="stylesheet" href="../Content/assets/css/fonts/fontawesome/css/font-awesome.min.css" />
	<link rel="stylesheet" href="../Content/assets/css/bootstrap.css"/>
	<link rel="stylesheet" href="../Content/assets/css/xenon-core.css"/>
	<link rel="stylesheet" href="../Content/assets/css/xenon-forms.css" />
	<link rel="stylesheet" href="../Content/assets/css/xenon-components.css" />
	<link rel="stylesheet" href="../Content/assets/css/xenon-skins.css" />
	<link rel="stylesheet" href="../Content/assets/css/custom.css" />--%>
    <%# System.Web.Optimization.Styles.Render("~/bundles/bootstarpcss") %>
<%--<link href="Content/AjaxControlToolkit/Styles/Calendar.min.css" rel="stylesheet" />--%>
	<script src='<%# ResolveClientUrl("~/Content/assets/js/jquery-1.11.1.min.js") %>'></script>
    
     <%--<%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>--%>
	<!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
	<!--[if lt IE 9]>
		<script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
		<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
	<![endif]-->
	<style>
     .navbar.horizontal-menu {
  position: relative;
  height: 85px;
  background: #ffffff;
  margin: 0;
  /* padding: 0; */
  z-index: 97;
  min-height: 0px;
  -webkit-box-shadow: 0 0px 1px rgba(0, 0, 0, 0.15);
  -moz-box-shadow: 0 0px 1px rgba(0, 0, 0, 0.15);
  box-shadow: 0 0px 1px rgba(0, 0, 0, 0.15);
}
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <asp:ScriptManager ID="sc1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true"></asp:ScriptManager>
            <asp:TextBox ID="txtweekcommencedate" runat="server" SkinID="Date"></asp:TextBox>
                                    <asp:Label ID="imgbtnenddate8" runat="server" SkinID="Calender"/>
              <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                        PopupButtonID="txtweekcommencedate" TargetControlID="txtweekcommencedate" >
                                    </ajaxToolkit:CalendarExtender>
        </div>
    </form>
</body>
</html>
