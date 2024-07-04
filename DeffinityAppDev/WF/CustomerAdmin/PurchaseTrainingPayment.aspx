<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="PurchaseTrainingPayment.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.PurchaseTrainingPayment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Training Payment
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     Card Details
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
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
           <div class="col-md-1">
               </div>
          <div class="col-md-10">
    <img src="../../../Content/images/icon_cardconnect.png">
              </div>
           </div>
    <asp:Panel ID="pnlPaymentDetails" runat="server">
     <div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
               <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
              <asp:ValidationSummary ID="pSUm" runat="server" ValidationGroup="p" />
              </div>
         </div>
         <div class="form-group row">
          <div class="col-md-8">
              <asp:Label ID="lblCardERROR" runat="server" EnableViewState="false"></asp:Label>
              </div>
             </div>
        

      
        
    <div class="form-group row">
          <div class="col-md-8">
 <label class="col-sm-3 control-label">Amount to Charge</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtAmount" runat="server" SkinID="Price_150px" 
                            Width="150px" >0.00</asp:TextBox>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-8">
 <label class="col-sm-3 control-label">Card Type</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlCardType" runat="server" SkinID="ddl_200px">
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
          <div class="col-md-8">
 <label class="col-sm-3 control-label">Card Number</label>
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
          <div class="col-md-8">
 <label class="col-sm-3 control-label"> Name on Card</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtNameOnCard" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_200px" MaxLength="250"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtNameOnCard" Display="None" ErrorMessage="Please enter name on card" ValidationGroup="p"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-8">
 <label class="col-sm-3 control-label">Expiration</label>
           <div class="col-sm-9 form-inline">
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="paymentinfo-text" SkinID="ddl_125px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="paymentinfo-text"  SkinID="ddl_125px">
                        </asp:DropDownList>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="ddlMonth" Display="None"  
                            ErrorMessage="Please select month" ValidationGroup="p"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="ddlYear" Display="None" 
                            ErrorMessage="Please select year" ValidationGroup="p"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-8">
 <label class="col-sm-3 control-label">Card Security Code</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtCvv" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_75px" MaxLength="10"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtCvv" Display="None" 
                            ErrorMessage="Please enter CVV" ValidationGroup="p" ></asp:RequiredFieldValidator>
                         <%-- <p>  A code that is printed (not imprinted) on the back of 
                                a credit card. It consist of 3 or 4 digits. </p>--%>
               <p>e.g: 123 </p>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-8">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
               <asp:HiddenField ID="hterm" runat="server" />
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                            onclick="btnSubmit_Click" ValidationGroup="p" />
                        &nbsp;
                        <asp:Button id="btnCancel" runat="server" SkinID="btnCancel" OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
	</div>
</div>



        </asp:Panel>

     <asp:Panel ID="pnlResult" runat="server" Visible="false">
          <div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lblResultSucess" runat="server"  EnableViewState="false" CssClass="green-alert green-alert-success" Visible="false" style="width:100%"></asp:Label>
               <asp:Label ID="lblResultFail" runat="server" EnableViewState="false" CssClass="red-alert red-alert-danger" Visible="false" style="width:100%"></asp:Label>
              </div>
         </div>
          <div class="form-group row">
          <div class="col-md-12">
              </div>
              </div>
          <div class="form-group row">
          <div class="col-md-12">
              <asp:Button ID="btnBack" runat="server" Text="Back to Home" SkinID="btnDefault" OnClick="btnBack_Click" Visible="false"  />
              </div>
              </div>
         </asp:Panel>



</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>

