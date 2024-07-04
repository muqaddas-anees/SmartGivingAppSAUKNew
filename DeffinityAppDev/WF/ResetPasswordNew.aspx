<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPasswordNew.aspx.cs" Inherits="DeffinityAppDev.WF.ResetPasswordNew" %>
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
				        //$('input[autocomplete="off"]').each(function () {
				        //    debugger
				        //    var input = this;
				        //    var name = $(input).attr('name');
				        //    var id = $(input).attr('id');

				        //    $(input).removeAttr('name');
				        //    $(input).removeAttr('id');

				        //    setTimeout(function () {
				        //        debugger
				        //        $(input).attr('name', name);
				        //        $(input).attr('id', id);
				        //    }, 1);
				        //});

				    }
				    jQuery(document).ready(function ($) {
				        $(document).ready(function () {
				            debugger
				           // $("#txtName").attr("autocomplete", "off");
				            //$("#txtPwd").attr("autocomplete", "off");
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
							<span>Reset password</span>
						</a>
						<p></p>
					</div>
                    <div class="form-group row">
				<asp:Label ID="lblError" runat="server" SkinID="GreenBackcolor"  EnableViewState="false"></asp:Label>
                     <asp:Label ID="lblMsg" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
				</div>
                    <div id="Panel1" runat="server">
					<div class="form-group ">
                        <label class="control-label" for="txtusername"> <%= Resources.DeffinityRes.Username%>:</label>
                         <asp:TextBox ID="txtusername" runat="server" ReadOnly="true" CssClass="form-control" AutoCompleteType="Disabled"  ClientIDMode="Static"></asp:TextBox>
  <asp:RequiredFieldValidator  style="font-size:small" ID="Rfv1" runat="server" ForeColor="Red" ErrorMessage="Please enter Username" ControlToValidate="txtusername" ValidationGroup="group1" ></asp:RequiredFieldValidator>
					</div>
					<div class="form-group row">
                        <label class="control-label" for="txtpwd"> <%= Resources.DeffinityRes.NewPassword%>:</label>
                         <asp:TextBox ID="txtpwd" runat="server" TextMode="Password" ClientIDMode="Static" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="newPasswordReq" runat="server" ControlToValidate="txtpwd"
                                ErrorMessage="Please enter password" SetFocusOnError="True" Display="Dynamic"
                                ValidationGroup="ValInsert" Font-Size="X-Small" ForeColor="Red"/>
                          </div>
                    <div class="form-group row">
                        <label class="control-label" for="TxtCpwd"> <%= Resources.DeffinityRes.ConfirmNewPassword%>:</label>
<asp:TextBox ID="TxtCpwd" runat="server" TextMode="Password" ClientIDMode="Static" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox> <br />
		<asp:RequiredFieldValidator ID="confirmPasswordReq" runat="server" ControlToValidate="TxtCpwd"
                                ErrorMessage="Please enter confirmation password" SetFocusOnError="True" Display="Dynamic"
                                ValidationGroup="ValInsert" Font-Size="X-Small" ForeColor="Red" />
				 <asp:CompareValidator ID="comparePasswords" runat="server" ControlToCompare="txtpwd"
                            ControlToValidate="TxtCpwd" ErrorMessage="Your passwords do not match up!"
                            Display="Dynamic" ValidationGroup="ValInsert" Font-Size="X-Small" ForeColor="Red"/>
                          </div>
					<div class="form-group row">
                         <asp:Button ID="btnsubmit" runat="server" ValidationGroup="ValInsert"
                            Text="Submit" OnClick="btnforgot_Click" />
					</div>
                        </div>
			<!-- Errors container -->
				
					 <div class="form-group row">
                        <asp:HyperLink runat="server" NavigateUrl="~/WF/Default.aspx" Text="Back to login"><i class="fa fa-arrow-left"></i> Back to login</asp:HyperLink>
                       
                        </div>
                    <div class="login-footer">
						  <div class="info-links">
						 &copy; <label id="lblyear" runat="server"></label> <label id="lblcopyrighttext" runat="server"></label>
						</div>
					</div>
 <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
               
                     
				</form>
			</div>
		</div>
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
<%--</form>--%>
</body>
</html>