<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentProcess.aspx.cs" Inherits="DeffinityAppDev.WF.payinvoice.PaymentProcess" %>

<%@ Register Src="~/WF/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<meta name="description" content="" />
	<meta name="author" content="" />
    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
<title> Invoice Payment </title>
<meta name="description" content=""/>
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Arimo:400,700,400italic"/>
	<%: System.Web.Optimization.Styles.Render("~/bundles/bootstarpcss") %>
    <style type="text/css">
        .login-page.login-light{background: #eeeeee url("../Content/images/deffi_coffee.jpg") top center no-repeat;}
        input:-webkit-autofill {
            background-color: white !important;
        }
    </style>
	<%--<%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>--%>
</head>
<body class="page-body login-page login-light">
   <%-- <form id="form1" runat="server">--%>
	<div class="login-container">
<form id="form2" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"> </asp:ScriptManager>
    <asp:HiddenField ID="hinvid" runat="server" Value="" />
    <asp:Panel ID="pnlMain" runat="server" Visible="false">
    <div class="card shadow-sm">
						<div class="card-header">
							 <asp:Label ID="lblHeader" runat="server" CssClass="header" 
                Text="PayPal Payflow Pro Online Credit Card Transaction"></asp:Label>
						</div>
        <div class="panel-body">


            <script language="JavaScript">
                function showMe() {
                    var mytoken = document.getElementById('mytoken');
                    alert("Token=" + mytoken.value);
                }


         </script>
    <script type="text/javascript">
        window.addEventListener('message', function (event) {
            var token = JSON.parse(event.data);
            var mytoken = document.getElementById('mytoken');
            mytoken.value = token.message;
        }, false);

    </script>
    

             <div class="form-group row">
          <%-- <div class="col-md-1">
               </div>--%>
          <div class="col-md-10">
    <img src="../../../Content/images/icon_cardconnect.png"> 
              </div>
           </div>
              <div class="form-group row">
          <div class="col-md-12">
              <br />
               <br />
              </div>
              </div>
    <asp:Panel ID="pnlCCD" runat="server">
        
         <div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lblCardERROR" runat="server" EnableViewState="false"></asp:Label>
              </div>
             </div>

         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Job Ref</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtJobRef" runat="server" SkinID="Price_100px" 
                            Width="150px" ReadOnly="true"></asp:TextBox>
                <br />
            </div>
	</div>
</div>
         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Job Details</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtJobDetails" runat="server" SkinID="txtMulti_80" 
                            TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
               <br />
            </div>
	</div>
</div>

         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Client Details</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtClientDetails" runat="server" SkinID="txtMulti_80" 
                            TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
               <br />
            </div>
	</div>
</div>
         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Invoice Ref</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtInvoiceRef" runat="server"  SkinID="Price_100px"
                            Width="150px" ReadOnly="true"></asp:TextBox>
                <br />
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Amount to Charge</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtAmount" runat="server" SkinID="Price_150px" 
                            Width="150px" ReadOnly="true">00.00</asp:TextBox>
                <br />
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Card Type</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlCardType" runat="server" SkinID="ddl_200px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="MASTERCARD" Text="MASTERCARD"></asp:ListItem>
                            <asp:ListItem Selected="True" Value="VISA" Text="VISA"></asp:ListItem>
                   <asp:ListItem Value="DISCOVER" Text="DISCOVER"></asp:ListItem>
                    <asp:ListItem Value="AMEX" Text="AMEX"></asp:ListItem>
                          
                        </asp:DropDownList>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="ddlCardType" Display="Dynamic" CssClass="error-text" ErrorMessage="Required" 
                            ></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Card Number</label>
           <div class="col-sm-9">
               <div id="pnlCreditCard" runat="server">
                   <asp:TextBox ID="txtCardConnectNumber" runat="server" CssClass="paymentinfo-text" SkinID="txt_50" Visible="false"></asp:TextBox>
                   <asp:TextBox ID="txtCardNumber" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_200px" MaxLength="20"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="rfCardnumber" runat="server" 
                            ControlToValidate="txtCardNumber" Display="Dynamic"  ErrorMessage="Please enter Card Number"></asp:RequiredFieldValidator>
                   </div>
               <div id="pnlCardConnect" runat="server">
                   <iframe id="tokenframe" name="tokenframe"  
                       src="https://boltgw.cardconnect.com/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D" 
                       frameborder="0" scrolling="no" width="200" height="35" runat="server"></iframe>
                <asp:HiddenField ID="mytoken" runat="server" ClientIDMode="Static" />
                   </div>
               <p>e.g: 4111111111111111</p>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"> Name on Card</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtNameOnCard" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_200px" MaxLength="250"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtNameOnCard" Display="Dynamic" ErrorMessage="Please enter Name on Card"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Expiration</label>
           <div class="col-sm-9 form-inline">
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="paymentinfo-text" SkinID="ddl_125px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="paymentinfo-text"  SkinID="ddl_125px">
                        </asp:DropDownList>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="ddlMonth" Display="Dynamic"  
                            ErrorMessage="Please select Month"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="ddlYear" Display="Dynamic" 
                            ErrorMessage="Please select Year"></asp:RequiredFieldValidator>
                
            </div>
	</div>
</div>
        <div class="form-group row">
          <div class="col-md-12">
              <br />
              </div>
            </div>
        
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Card Security Code</label>
           <div class="col-sm-9">

                <asp:TextBox ID="txtCvv" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_75px" MaxLength="10"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtCvv" Display="Dynamic" 
                            ErrorMessage="Please enter CVV"></asp:RequiredFieldValidator>
                         <%-- <p>  A code that is printed (not imprinted) on the back of 
                                a credit card. It consist of 3 or 4 digits. </p>--%>
               <p>e.g: 123 </p>
            </div>
	</div>
</div>
          <div class="form-group row">
          <div class="col-md-12">
              <br />
              </div>
            </div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"></label>
           <div class="col-sm-9 form-inline">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                            onclick="btnSubmit_Click" />
                        &nbsp;
                        <asp:Button id="btnCancel" runat="server" SkinID="btnCancel" OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
	</div>
</div>
        </asp:Panel>
    <asp:Panel ID="pnlResult" runat="server" Visible="false">
        <div class="form-group row">
          <div class="col-md-12">
              <div class="label label-success" style="font-size:18px"><asp:Literal ID="lblResult" runat="server"></asp:Literal> </div>
      
              </div>
            </div>
        <div class="form-group row" style="display:none;visibility:hidden">
          <div class="col-md-12">
                <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor"></asp:Label>
    <%--<asp:Label ID="" runat="server" SkinID="GreenBackcolor"></asp:Label>--%>
        <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor"></asp:Label>
              </div>
            </div>
        <div class="form-group row">
          <div class="col-md-12 ">
              <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" Visible="false" />
	</div>
</div>
        </asp:Panel>
     <asp:HiddenField ID="hadid" runat="server" Value="0" />

            </div>
        </div>
    </asp:Panel>
     <asp:Panel ID="pnlInvalidUrl" runat="server" Visible="false">
           <div class="form-group row">
          <div class="col-md-12">
             <div id="lblInvalidUrl" class="alert alert-danger" style="color:White;width: 100%;">Invalid Link</div>
       <%-- <asp:Label ID="lblInvalidUrl" runat="server" SkinID="RedBackcolor" Text="Invalid Link"></asp:Label>--%>
              </div>
            </div>

           <div class="form-group row">
          <div class="col-md-12">


              </div>
               </div>
         </asp:Panel>

      <asp:Panel ID="pnlInvalidLink" runat="server" Visible="false">
            <div class="form-group row">
          <div class="col-md-12">
              <div id="lblInvalidlink" class="alert alert-danger" style="color:White;width: 100%;">Link Inactive</div>
       <%-- <asp:Label ID="lblInvalidlink" runat="server" SkinID="RedBackcolor" Text="Link Inactive"></asp:Label>--%>
              </div>
            </div>
            <div class="form-group row">
          <div class="col-md-12">


              </div>
               </div>
         </asp:Panel>

<div class="data_carrier" style="color:gray;">


<uc1:Footer ID="ctrl_footer" runat="server" />
</div>

</form>
        </div>
      
                     <%: System.Web.Optimization.Scripts.Render("~/bundles/xenonjs") %>
   <script src='<%=ResolveClientUrl("~/Scripts/Utility.js")%>'></script>
	 <script type="text/javascript">
	     $(window).load(function () {
	         debugger
	         disableAutofill();
	     });
	     //if (navigator.userAgent.toLowerCase().indexOf("chrome") >= 0 || navigator.userAgent.toLowerCase().indexOf("safari") >= 0) {
	     //}
	 </script>
</body>
</html>
