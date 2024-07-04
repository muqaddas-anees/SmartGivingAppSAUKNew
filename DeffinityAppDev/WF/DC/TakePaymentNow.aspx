<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="TakePaymentNow.aspx.cs" Inherits="DeffinityAppDev.WF.DC.TakePaymentNow" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Quick Pay
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
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
    <script>
        $(document).ready(function () {
            $('#btnApply').click(function (e) {
                
                //alert('test');
                //var url = "http://123smartpro.com/index.php/cardconnectapplicationform/";
                var url = "https://apply.cardpointe.com/arcnd";
                window.open(url, '_blank');
                return false;
            });
        }
        );
    </script>

    <div class="form-group row">
                                        <div class="col-md-12">
                                            <div class="col-sm-8 " id="pnltakepayment" runat="server">
                                                
   <div class="card shadow-sm">
							<div class="card-header">
							<h3 class="panel-title form-inline"> Client Details
                               </h3>
							<div class="card-toolbar">

							</div>
						</div>
						<div class="panel-body">
                            <div class="row">
                                    
                                   <div class="form-group row">
      <div class="col-md-12">
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
           <label class="col-sm-3 control-label"> Client </label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlContact" runat="server"></asp:DropDownList>
              <asp:HiddenField ID="hContact" runat="server" />
               <asp:RequiredFieldValidator ID="rfContact" runat="server" ControlToValidate="ddlContact" InitialValue="0" ErrorMessage="Please select client"  Display="None" ValidationGroup="p"></asp:RequiredFieldValidator>
            </div>
	</div>
             </div>
         <div class="form-group row">
      <div class="col-md-8">
           <label class="col-sm-3 control-label"> <%: Resources.DeffinityRes.Address %> </label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlAddress" runat="server"></asp:DropDownList>
              <asp:HiddenField ID="hAddress" runat="server" />
               <asp:RequiredFieldValidator ID="rfAddress" runat="server" ControlToValidate="ddlAddress" InitialValue="0" ErrorMessage="Please select address" Display="None" ValidationGroup="p"></asp:RequiredFieldValidator>
            </div>
	</div>
             </div>
          <div class="form-group row">
      <div class="col-md-8">
           <label class="col-sm-3 control-label"> <%: Resources.DeffinityRes.Details %> </label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtDetails" runat="server" SkinID="txtMulti_80" TextMode="MultiLine"></asp:TextBox>
               <asp:RequiredFieldValidator ID="rfDetails" runat="server" ControlToValidate="txtDetails" ErrorMessage="Please enter details" Display="None" ValidationGroup="p"></asp:RequiredFieldValidator>
           
            </div>
	</div>
             </div>

        <div class="form-group row">
    <div class="col-md-12 text-bold">
        <strong>Card Details </strong>
        <hr class="no-top-margin" />
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
          <div class="col-md-8">
 <label class="col-sm-3 control-label">Card Number</label>
           <div class="col-sm-9">
               <div id="pnlCreditCard" runat="server" visible="false">
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
               <p>e.g: 4111222233334444</p>
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
              <asp:Button ID="btnBack" runat="server" Text="Take another payment" SkinID="btnDefault" OnClick="btnBack_Click"  />
              </div>
              </div>

         </asp:Panel>



     <script type="text/javascript">
         $(function () {

             SetContactContactData();
             SetAddressContactData();

             $("[id*=ddlContact]").change(function () {
                 $("[id*=hContact").val($(this).val());

                 SetAddressContactData();

             });
         });

         function setContactDropdownValue() {
             if ($("[id*=hContact").val() != '') {
                 $("[id*=ddlContact]").val($("[id*=hContact").val());
             }
         }
         function SetContactContactData() {
             //var id = $("[id$='hcid']").val();

             //if (id == "")
             //    id = "0";
             $.ajax({
                 type: "POST",
                 url: "../../WF/DC/webservices/DCServices.asmx/GetContacts",
                 //data: "{id:'" + id + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 async: false,
                 success: function (r) {
                     var ddlCustomers = $("[id*=ddlContact]");
                     debugger;
                     ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                     $.each(r.d, function () {
                         ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                     });
                     $("[id*=hAddress]").val('0');
                     setContactDropdownValue();
                 }
             });
         }



         function SetdropdownsValue() {
             if ($("[id*=hContact]").val().trim() != "") {
                 $("[id*=ddlContact] option").each(function () {
                     if ($(this).val() == $("[id*=hContact]").val()) {
                         $(this).attr('selected', 'selected');
                     }
                 });
             }
         }
</script>
    <script type="text/javascript">
        $(function () {

            //SetAddressContactData();
            $("[id*=ddlAddress]").change(function () {
                $("[id*=hAddress").val($(this).val());
               // SetSubAddressContactData();
            });
        });

        function setAddressDropdownValue() {
            if ($("[id*=hAddress").val() != '') {
                $("[id*=ddlAddress]").val($("[id*=hAddress").val());
            }
        }
        function SetAddressContactData() {
            var id = $("[id*=ddlContact]").val();
            if (id == null)
                id = "0";
            if (id == "")
                id = "0";
            debugger;
            if (id != "0") {
                $.ajax({
                    type: "POST",
                    url: "../../WF/DC/webservices/DCServices.asmx/GetContactsAddress",
                    data: "{id:'" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (r) {
                        var ddlCustomers = $("[id*=ddlAddress]");
                        debugger;
                        ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(r.d, function () {
                            ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                        $("[id*=hAddress]").val('0');
                        setAddressDropdownValue();
                    }
                });
            }
            else {
                var ddlCustomers = $("[id*=ddlAddress]");
                debugger;
                ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        }



        function SetdropdownsValue() {
            if ($("[id*=hAddress]").val().trim() != "") {
                $("[id*=ddlAddress] option").each(function () {
                    if ($(this).val() == $("[id*=hAddress]").val()) {
                        $(this).attr('selected', 'selected');
                    }
                });
            }
        }
</script>
        
         
          </div>
                                        
              </div>
                                </div>
                </div>
            </div>
                                            </div>
                                            <div class="col-sm-4 form-inline" id="pnlccd" runat="server">


                                                <div class="panel panel-color panel-info">
						<div class="card-header">
							<h3 class="panel-title form-inline">Don't have a Card Connect Account?
                               </h3>
							<div class="card-toolbar">

                              


							</div>
						</div>
						<div class="panel-body">
                            <div class="row">
                                    
                                  
                               
                               
 <div class="form-group row" style="padding-bottom:25px;">
      <div class="col-md-12">
          <label class="col-sm-12 control-label"> <lable style="font-size:16px">Apply now for the lowest payment processing fees Nationwide!</lable> </label>
          </div>
     </div>


                                <div class="form-group row" style="padding-bottom:25px;font-size:16px">
      <div class="col-md-12">
          <ul>
              <li>Lowest cost payment processing</li>
              <li>Beats Stripe, Cube, Intuit, and many other providers</li>
              <li>Turns your mobile into a touchless terminal</li>
          </ul>
          </div>
     </div>
                                <div class="form-group row" style="width:100%">
      <div class="col-md-12" style="text-align:center;">
    
          <asp:Button ID="btnWatch" runat="server" Text="Watch Video" CssClass="btn btn-blue"  /> <asp:Button ID="btnApply" runat="server" Text="Apply Now" ClientIDMode="Static"  />
          </div>
     </div>

                                </div>
                </div>
            </div>


                                                </div>
                                            </div>
             </div>
      <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnWatch" PopupControlID="pnlManagePassword" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlManagePassword" runat="server" BackColor="White" Style="display:none;"
                       Width="680px" Height="480px" CssClass="panel panel-color panel-info" ScrollBars="None">
         

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text=""></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="panel-body">
        <div class="form-group row">
                   <div class="col-md-12 form-inline">

                       <iframe id="viframe" runat="server" height="340" width="600" style="border:none;" src="https://player.vimeo.com/video/447483488"></iframe>
                       
                       </div>
            </div>
 
      
        
           
        </div>
                  
           </asp:Panel>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
