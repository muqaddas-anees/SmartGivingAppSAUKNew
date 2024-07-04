<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default" Codebehind="Default.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <%--<meta charset="utf-8"/>--%>
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
        /*.login-page.login-light{background: #eeeeee url("../Content/images/deffi_coffee.jpg") top center no-repeat;}*/
		.login-page.login-light{background: #eeeeee top center;}
        input:-webkit-autofill {
            background-color: white !important;
        }
    </style>
	<%--<%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js" type="text/javascript"></script>
</head>
<body class="page-body login-page login-light">
   <%-- <form id="form1" runat="server">--%>
	<div class="login-container">
		<div class="row">
			<div class="col-sm-6">
				<script type="text/javascript">
                    function disableAutofill() {
                        $('input[autocomplete="off"]').each(function () {
                            debugger
                            var input = this;
                            var name = $(input).attr('name');
                            var id = $(input).attr('id');

                            $(input).removeAttr('name');
                            $(input).removeAttr('id');

                            setTimeout(function () {
                                debugger
                                $(input).attr('name', name);
                                $(input).attr('id', id);
                            }, 1);
                        });

                    }
                    jQuery(document).ready(function ($) {
                        $(document).ready(function () {
                            debugger
                            $("#txtName").attr("autocomplete", "off");
                            $("#txtPwd").attr("autocomplete", "off");
                            disableAutofill();
                        });
                        // Reveal Login form
                        setTimeout(function () { $(".fade-in-effect").addClass('in'); }, 1);
                        // Set Form focus
                        $("form#login .form-group:has(.form-control):first .form-control").focus();
                    });
				</script>
				<!-- Add class "fade-in-effect" for login form effect -->
				<form clientidmode="Static" runat="server" method="post" role="form" id="login" class="login-form fade-in-effect" defaultbutton="btnsubmit" autocomplete="off">
					<div class="login-header">
						<a href="#" class="logo">
							<img src="../Content/assets/images/logo-white-bg@2x.png" alt="" width="" />
							<span></span>
						</a>
						<p></p>
					</div>
					<div class="form-group ">
                        <label class="control-label" for="username"> <%= Resources.DeffinityRes.Username%>:</label>
<asp:TextBox ClientIDMode="Static" ID="txtName" CssClass="form-control" runat="server" AutoCompleteType="Disabled" > </asp:TextBox>
                        <asp:RequiredFieldValidator  style="font-size:small" ID="Rfv1" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Username" ControlToValidate="txtName" ValidationGroup="group1" ></asp:RequiredFieldValidator>
					</div>
                   
					<div class="form-group">
                        <label class="control-label" for="passwd"> <%= Resources.DeffinityRes.Password%>:</label>
<asp:TextBox ID="txtPwd" ClientIDMode="Static" CssClass="form-control" runat="server" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
		 <asp:RequiredFieldValidator style="font-size:small" ID="Rfv2" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Password" ControlToValidate="txtPwd" ValidationGroup="group1"></asp:RequiredFieldValidator>
				
                          </div>
                    <div class="form-group">
                       
                        </div>
					<div class="form-group">
                <asp:LinkButton ID="btnsubmit" runat="server" SkinID="BtnLinkLogin" OnClick="btnsubmit_Click" ValidationGroup="group1" Text="Login" TabIndex="0" ></asp:LinkButton>
					</div>
			<!-- Errors container -->
				<div class="errors-container">
				<asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" Visible="false"></asp:Label>
				</div>
					<div class="login-footer">
						<a href="ForgotPassword.aspx" style="display:none;visibility:hidden;"> Forgot your password?</a>
                        <div class="info-links">
						 <label id="Label2" runat="server" visible="false">Powered by First Data Field Services</label>
						</div>
						<div class="info-links">
						 &copy; <label id="lblyear" runat="server"></label> <label id="lblcopyrighttext" runat="server"></label>
						</div>
					</div>
  <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
       <Scripts>
           <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />
       </Scripts>
   </asp:ScriptManager>
        <asp:Button ID="btnDisable" runat="server" Text="Disable" 
        onclick="btnDisable_Click" Visible="false" />
                    <div class="row">
                   
 
        <asp:LinkButton ID="btn_popup_area" runat="server" SkinID="BtnLinkAdd" EnableViewState="false" Style="display:none;"  />
				</div>
                     <%: System.Web.Optimization.Scripts.Render("~/bundles/xenonjs") %>
    <%: System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
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
