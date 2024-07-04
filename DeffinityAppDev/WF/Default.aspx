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
	<%# System.Web.Optimization.Styles.Render("~/bundles/bootstarpcss") %>
    <style type="text/css">
       /* .login-page.login-light{background: #eeeeee url("../Content/images/deffi_coffee.jpg") top center no-repeat;}*/

	    .login-page.login-light{background: #eeeeee url("<%# getbackImageUrl()  %>") top center no-repeat;}
        input:-webkit-autofill {
            background-color: white !important;
        }
		.btn.btn-secondary {
    background-color: #FF6600;
    color: #ffffff;
}
		.btn.btn-secondary a:hover{
    background-color: #FF6600;
    color: #ffffff;
}
		a#btnsubmit{
				
    background-color: #FF6600;
    color: #ffffff;
    border-color: #FF6600;
    border-width: 1px;
		}
		a#btnsubmit :hover{
				
    background-color: #FF6600;
    color: #ffffff;
		}
    </style>
	<%--<%# System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/Utility.js"></script>
</head>
<body class="page-body login-page login-light" runat="server" id="PageBody">
   <%-- <form id="form1" runat="server">--%>
	<div class="login-container">
		<div class="row">
			<div class="col-sm-6">
				<script type="text/javascript">
				    function disableAutofill()
				    {
				        $('input[autocomplete="off"]').each(function () {
				            
				            var input = this;
				            var name = $(input).attr('name');
				            var id = $(input).attr('id');

				            $(input).removeAttr('name');
				            $(input).removeAttr('id');

				            setTimeout(function () {
				                
				                $(input).attr('name', name);
				                $(input).attr('id', id);
				            }, 1);
				        });

				    }
				    jQuery(document).ready(function ($) {
				        $(document).ready(function () {
				            
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
							<img id="imglogo" src="<%#getImageUrl() %>" alt="" width="" />
							<span></span>
						</a>
						<p></p>
					</div>
					<div class="form-group ">
                        <label class="control-label" for="username"> <%= Resources.DeffinityRes.Username%>:</label>
<asp:TextBox ClientIDMode="Static" ID="txtName" CssClass="form-control" runat="server" AutoCompleteType="Disabled" > </asp:TextBox>
                        <asp:RequiredFieldValidator  style="font-size:small" ID="Rfv1" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Username" ControlToValidate="txtName" ValidationGroup="group1" ></asp:RequiredFieldValidator>
					</div>
                   
					<div class="form-group row">
                        <label class="control-label" for="passwd"> <%= Resources.DeffinityRes.Password%>:</label>
<asp:TextBox ID="txtPwd" ClientIDMode="Static" CssClass="form-control" runat="server" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
		 <asp:RequiredFieldValidator style="font-size:small" ID="Rfv2" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Password" ControlToValidate="txtPwd" ValidationGroup="group1"></asp:RequiredFieldValidator>
				
                          </div>
                    <div class="form-group row">
                       
                        </div>
					<div class="form-group row">
                <asp:LinkButton ID="btnsubmit" runat="server" SkinID="BtnLinkLogin" OnClick="btnsubmit_Click" ValidationGroup="group1" Text="Login" TabIndex="0" ></asp:LinkButton>
					</div>
			<!-- Errors container -->
				<div class="errors-container">
				<asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" Visible="false"></asp:Label>
				</div>
					<div class="login-footer">
						<a href="ForgotPassword.aspx"> Forgot your password?</a>
                        <div class="info-links">
						 <label id="Label2" runat="server" visible="false">Powered by First Data Field Services</label>
						</div>
						<div class="info-links">
						 &copy; <label id="lblyear" runat="server" visible="false"></label> <label id="lblcopyrighttext" runat="server"></label>
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
                   
 <asp:Panel ID="Panel1" runat="server" Style="display:none;">
      <div class="col-sm-12">
					
					<div class="card shadow-sm">
						<div class="card-header">
                            <h2>Terms and Conditions of use</h2>
                            </div>
                        <div class="panel-body">
                            <div style="overflow-y:scroll;height:300px;width:500px;border-style:inset;font-size:11px;">
                              <% Response.WriteFile("~/WF/TermsAndConditions.htm"); %>
                                </div>
                            <div style="width:300px;float:left;padding-top:5px;"><asp:CheckBox ID="cbox" runat="server" Text="I accept these terms and conditions" Checked="false"  />  </div>
<div style="width:200px;float:right;padding-top:5px;"><asp:Button ID="btnCtn" runat="server" Text="Continue" onClick="btnCtn_Click" CausesValidation="false"   />
<asp:Button ID="btnCancel" runat="server" Text="Cancel" /> </div>
                        </div>
                        </div>
                        </div>
  <div class="terms_block">

<script language="javascript" type="text/javascript">
    document.getElementById("<%=btnCtn.ClientID %>").style.visibility = 'hidden';
    function fnShow() {
        var cbox = document.getElementById("<%=cbox.ClientID %>");
        document.getElementById("<%=btnCtn.ClientID %>").style.visibility = 'hidden';
        if (cbox.checked) {
            document.getElementById("<%=btnCtn.ClientID %>").style.visibility = 'visible';
        }
        else { document.getElementById("<%=btnCtn.ClientID %>").style.visibility = 'hidden'; }
        return false;
    }
    function fnUncheck() {
        var cbox = document.getElementById("<%=cbox.ClientID %>");
        cbox.checked = false;
    }
</script>
 </div>
        </asp:Panel>
       
<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btn_popup_area" 
PopupControlID="Panel1" BackgroundCssClass="modalBackground" CancelControlID="btnCancel"  />
        <asp:LinkButton ID="btn_popup_area" runat="server" SkinID="BtnLinkAdd" EnableViewState="false" Style="display:none;"  />
				</div>
					<asp:HiddenField ID="hpid" runat="server" ClientIDMode="Static" />
                     <%# System.Web.Optimization.Scripts.Render("~/bundles/xenonjs") %>
    <%# System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
	 <script type="text/javascript">

         $(document).ready(function () {

             var d = new Date();
             var u = window.location.host;
             console.log(u);

			 console.log(u.includes("smarttechapp"));
			 
             if (u.includes("smarttechapp"))
                 $('#imglogo').attr("src", "../WF/UploadData/Customers/partner_" + $("[id*=hpid]").val()+".png?d=" + d.getSeconds());
             else {
				 $('#imglogo').attr("src", "../WF/UploadData/Customers/partner_" + $("[id*=hpid]").val() + ".png?d=" + d.getSeconds());

                // console.log("#eeeeee url('../WF/UploadData/Customers/partnerback_" + $("[id*=hpid]").val() + ".png?d=" + d.getSeconds() + "') top center no-repeat;");
			//	 debugger;
                // $("#bid").css("background", "#eeeeee url('../WF/UploadData/Customers/partnerback_" + $("[id*=hpid]").val() + ".png?d=" + d.getSeconds()+"') top center no-repeat;");
			 }

			 // .login-page.login-light{background: #eeeeee url( "../WF/UploadData/Customers/partner_" + $("[id*=hpid]").val() +".png?d=" + d.getSeconds()) top center no-repeat;}

         });


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
