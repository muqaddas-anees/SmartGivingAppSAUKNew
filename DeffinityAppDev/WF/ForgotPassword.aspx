<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="DeffinityAppDev.WF.ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
        .login-page.login-light{background: #eeeeee url("<%# getbackImageUrl()  %>") top center no-repeat;}
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
				    function disableAutofill()
				    {
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
				            $("#txtusername").attr("autocomplete", "off");
				            $("#txtPwd").attr("autocomplete", "off");
				            //disableAutofill();
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
							<img id="imglogo" src="<%#getImageUrl() %>"  alt="" width="" />
							<span>Forgot password</span>
						</a>
						<p></p>
					</div>
					<%--<div class="form-group ">
                        <label class="control-label" for="username"> <%= Resources.DeffinityRes.Username%>:</label>
<asp:TextBox ClientIDMode="Static" ID="txtusername" CssClass="form-control" runat="server" AutoCompleteType="Disabled" MaxLength="100" > </asp:TextBox>
  <asp:RequiredFieldValidator  style="font-size:small" ID="Rfv1" runat="server" ForeColor="Red" ErrorMessage="Please enter Username" ControlToValidate="txtusername" ValidationGroup="group1" ></asp:RequiredFieldValidator>
					</div>--%>
					<div class="form-group">
                        <label class="control-label" for="passwd"> <%= Resources.DeffinityRes.Username%>:</label>
<asp:TextBox ID="txtEmail" ClientIDMode="Static" CssClass="form-control" runat="server" MaxLength="200" AutoCompleteType="Disabled"></asp:TextBox>
		<asp:RequiredFieldValidator style="font-size:small" ID="Rfv2" runat="server"
			ForeColor="Red" ErrorMessage="Please enter Email" ControlToValidate="txtEmail" ValidationGroup="group1" Display="Dynamic"></asp:RequiredFieldValidator><br />
				<asp:RegularExpressionValidator ID="validmail" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ValidationGroup="group1"
                     ErrorMessage=" Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>
                          </div>
					<div class="form-group">
                <asp:Button ID="btnsubmit" runat="server" SkinID="btnDefault" OnClick="btnsendagain_Click" ValidationGroup="group1" Text="Submit" TabIndex="0" ></asp:Button>
					</div>
                    <div class="form-group">
                        <asp:HyperLink runat="server" NavigateUrl="~/WF/Default.aspx" Text="Back to login"><i class="fa fa-arrow-left"></i> Back to login</asp:HyperLink>
                        </div>
			<!-- Errors container -->
				<div class="errors-container">
				<asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
					<asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
				</div>
					<div class="login-footer">
						<asp:LinkButton ID="btnsendagain" runat="server" Text="click here" 
        CssClass="Linkbutton" OnClick="btnsendagain_Click"></asp:LinkButton>
						<div class="info-links">
						 &copy; <label id="lblyear" runat="server"></label> <label id="lblcopyrighttext" runat="server"></label>
						</div>
					</div>
 <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
        <asp:Button ID="btnDisable" runat="server" Text="Disable" 
        onclick="btnDisable_Click" Visible="false" />
                    <div class="row">
                   
 
       
        <asp:LinkButton ID="btn_popup_area" runat="server" SkinID="BtnLinkAdd" EnableViewState="false" Style="display:none;"  />
				</div>
                     
				</form>
			</div>
		</div>
	</div>
    
<%: System.Web.Optimization.Scripts.Render("~/bundles/xenonjs") %>
    <%: System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
	 <script type="text/javascript">
	     $(window).load(function () {
	         debugger
	         //disableAutofill();
	     });
	     //if (navigator.userAgent.toLowerCase().indexOf("chrome") >= 0 || navigator.userAgent.toLowerCase().indexOf("safari") >= 0) {
	     //}
	 </script>
<%--</form>--%>
</body>
</html>

