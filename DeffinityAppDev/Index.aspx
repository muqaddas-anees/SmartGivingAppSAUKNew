<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DeffinityAppDev.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<meta name="description" content="" />
	<meta name="author" content="" />
    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
<title> </title>
<meta name="description" content=""/>
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Arimo:400,700,400italic"/>
	<%: System.Web.Optimization.Styles.Render("~/bundles/bootstarpcss") %>
    <style type="text/css">
        .login-page.login-light{background: #eeeeee url("../Content/images/deffi_coffee.jpg") top center no-repeat;}
        input:-webkit-autofill {
            background-color: white !important;
        }
    </style>
	<%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>
</head>
<body class="page-body login-page login-light">
   <%-- <form id="form1" runat="server">--%>
	<div class="login-container">
		<div class="row">
			<div class="col-sm-6">
				<script type="text/javascript">
				    //function disableAutofill()
				    //{
				    //    $('input[autocomplete="off"]').each(function () {
				    //        debugger
				    //        var input = this;
				    //        var name = $(input).attr('name');
				    //        var id = $(input).attr('id');

				    //        $(input).removeAttr('name');
				    //        $(input).removeAttr('id');

				    //        setTimeout(function () {
				    //            debugger
				    //            $(input).attr('name', name);
				    //            $(input).attr('id', id);
				    //        }, 1);
				    //    });

				    //}
				    //jQuery(document).ready(function ($) {
				    //    $(document).ready(function () {
				    //        debugger
				    //        //$("#uid").attr("autocomplete", "off");
				    //        //$("#pwd").attr("autocomplete", "off");
				    //        disableAutofill();
				    //    });
				    //    // Reveal Login form
				    //    //setTimeout(function () { $(".fade-in-effect").addClass('in'); }, 1);
				    //    // Set Form focus
				    //    //$("form#login .form-group:has(.form-control):first .form-control").focus();
				    //});
				</script>
				<!-- Add class "fade-in-effect" for login form effect -->
				<form clientidmode="Static" runat="server" method="post" role="form" id="login" class="login-form" defaultbutton="btnsubmit">
					<div class="login-header">
						<a href="#" class="logo">
							<img src="../Content/assets/images/logo-white-bg@2x.png" alt="" width="" />
							<span></span>
						</a>
						<p></p>
					</div>
                    <div class="form-group form-inline">
                        <asp:RadioButtonList ID="rbtton" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtton_SelectedIndexChanged">
                            <asp:ListItem Value="1" Selected="True">Access New XACT</asp:ListItem>
                            <asp:ListItem Value="2">Access Old XACT</asp:ListItem>
                        </asp:RadioButtonList>
                        </div>
					<div class="form-group ">
                        <label  > <%= Resources.DeffinityRes.Username%>:</label><br />
<asp:TextBox ClientIDMode="Static" ID="uid" runat="server" > </asp:TextBox>
  <asp:RequiredFieldValidator  style="font-size:small" ID="Rfv1" runat="server" ForeColor="Red" ErrorMessage="Please enter Username" ControlToValidate="uid" ValidationGroup="group1" ></asp:RequiredFieldValidator>
					</div>
					<div class="form-group row">
                        <label> <%= Resources.DeffinityRes.Password%>:</label><br />
<asp:TextBox ID="pwd" ClientIDMode="Static" runat="server" TextMode="Password" Enabled="true"></asp:TextBox>
		<asp:RequiredFieldValidator style="font-size:small" ID="Rfv2" runat="server" ForeColor="Red" ErrorMessage="Please enter Password" ControlToValidate="pwd" ValidationGroup="group1"></asp:RequiredFieldValidator><br />
				
                          </div>
					<div class="form-group row">
                <asp:LinkButton ID="btnsubmit" runat="server" SkinID="BtnLinkLogin" ValidationGroup="group1" Text="Login" TabIndex="0" PostBackUrl="~/WF/Default.aspx" ></asp:LinkButton>
					</div>
			<!-- Errors container -->
				<div class="errors-container">
				<asp:Label ID="lblError" runat="server" ForeColor="red" Visible="false"></asp:Label>
				</div>
					<div class="login-footer">
						<%--<a href="ForgotPassword.aspx"> Forgot your password?</a>--%>
						<div class="info-links">
						 &copy; <label id="lblyear" runat="server"></label> <label id="lblcopyrighttext" runat="server"></label>
						</div>
					</div>
<%--  <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
       <Scripts>
           <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />
       </Scripts>
   </asp:ScriptManager>--%>
     
                   
                   <%--  <%: System.Web.Optimization.Scripts.Render("~/bundles/xenonjs") %>--%>
  
	 <script type="text/javascript">
	     $(window).load(function () {
	         debugger
	         disableAutofill();
	     });
	     //if (navigator.userAgent.toLowerCase().indexOf("chrome") >= 0 || navigator.userAgent.toLowerCase().indexOf("safari") >= 0) {
	     //}
	 </script>
				</form>
			</div>
		</div>
	</div>
    

<%--</form>--%>
</body>
</html>