<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="TithingProcess.aspx.cs" Inherits="DeffinityAppDev.App.TithingProcess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Tithing
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
         window.addEventListener('message', function (event) {
             var token = JSON.parse(event.data);
             var mytoken = document.getElementById('mytoken');
             mytoken.value = token.message;
             var txtCardConnectNumber = document.getElementById("<%=txtCardConnectNumber.ClientID%>");
            txtCardConnectNumber.value = token.message;
            // console.log(txtCardConnectNumber.value);
        }, false);

     </script>
     <style>
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
      
    </style>

    <div class="row gy-5 g-xl-8 mb-6" id="pnlResult" runat="server" visible="false">
        <div class="col-xxl-12">

             <div class="card card-xxl-stretch" style="height:340px">
											<!--begin::Header-->
											<div class="card-header border-0 py-5">
												<h3 class="card-title fw-bolder text-white">Taithing </h3>
												<div class="card-toolbar">
                                                   

                                                    </div>

                                                <div class="card-body p-0" >
                                                    <div class="row mb-6">
         <div class="col-lg-8 col-sm-12 d-flex justify-content-center">
             <br />
             </div>
                                                        </div>
                                                     <div class="row mb-6">
         <div class="col-lg-12 col-sm-12 d-flex justify-content-center"  style="text-align:center;">
             <br /> <br /> <br /> <br />
                    <asp:Label ID="lblMsgResult" runat="server" Text="Tithing" Font-Bold="true" Font-Size="28px" ForeColor="Green"></asp:Label> <br />

                  </div>
        
        </div>
                                                     <div class="row mb-6">
                                                         
         <div class="col-lg-12 d-flex justify-content-center" style="text-align:center;">
             <br /><br /><br /><br />
             <asp:Button ID="btnSave" runat="server" Text="Go To Home" OnClick="btnSave_Click" Height="80px" Width="250px" />
             </div>
                                                        
                                                         </div>

                                                    </div>
                                                </div>
                 </div>
            </div>
        </div>

     <div class="row gy-5 g-xl-8 mb-6" id="pnlPrice" runat="server">
        <div class="col-xxl-8">

             <div class="card card-xxl-stretch" style="height:840px">
											<!--begin::Header-->
											<div class="card-header border-0 py-5">
												<h3 class="card-title fw-bolder text-white">Taithing </h3>
												<div class="card-toolbar">
                                                   

                                                    </div>

                                                <div class="card-body p-0" >

                                                       
    <div class="row mb-6">
         <div class="col-lg-8">
                    <asp:Label ID="lblDescription" runat="server" Text="Tithing" Font-Bold="true" Font-Size="28px"></asp:Label> <br />

                  </div>
         <div class="col-lg-4">
              <asp:TextBox ID="txtAmountTotal" runat="server" SkinID="Price" ClientIDMode="Static" Text="0.00" Font-Size="32px" ></asp:TextBox>
             </div>
        </div>
<div class="row mb-4">
                                                <hr />
    </div>

                                                   <asp:ListView ID="listCategory" runat="server" InsertItemPosition="None">
 <LayoutTemplate>
              <div class="form-group row">
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>

<ItemTemplate>
  <div class="row mb-4 bg-light-primary px-3 py-5 rounded-2 ">
              <div class="col-lg-10 pt-6">
                    <asp:Label ID="lblLable" runat="server" Text='<%# Eval("Name") %>' Font-Size="22px"></asp:Label> 
                  </div>
        <div class="col-lg-2 d-flex justify-content-end">
            
            <asp:LinkButton ID="btn" runat="server" ClientIDMode="Static" Text="+" SkinID="BtnLinkButton" OnClientClick="javascript:return  showdetails(this);" Font-Bold="true" Font-Size="32px" value='<%# Eval("ID") %>'></asp:LinkButton>
            <input type="hidden" id='h_<%# Eval("ID").ToString() %>' value="1" />     </div>
      </div>
    <div class="row mb-4" style="display:none;" id='<%# Eval("ID").ToString() %>'>
         <div class="col-lg-8">
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label> 
                  </div>
         <div class="col-lg-4">
             <asp:TextBox ID="txtAmount" runat="server" ClientIDMode="Static" Text="0.00" Font-Size="22px" Visible="false"></asp:TextBox>
             <input type="text" class="form-control form-control-lg form-control-solid txtamt" style="text-align:right;font-size:22px" value="0.00" onkeyup="sum_amt();"   />
             </div>
        </div>

</ItemTemplate>
</asp:ListView>
                                                    </div>
                                                    </div>
                                                </div>
                 </div>
              <div class="col-xxl-4">
               <div class="card card-xxl-stretch" style="height:840px">
											<!--begin::Header-->
											<div class="card-header border-0 py-5">
												<h3 class="card-title fw-bolder text-white"> </h3>
												<div class="card-toolbar">


                                                    </div>

                                                <div class="card-body p-0" >

                                                    <div class="row mb-6">
                                                        <asp:Label ID="lblTitleSub" runat="server" Text="How often would you like to Give?" Font-Bold="true" Font-Size="22px" ></asp:Label>
                                                    </div>

                                                    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-1 row-cols-xl-2 g-9 mb-6" data-kt-buttons="true" data-kt-buttons-target="[data-kt-button='true']">
																<!--begin::Col-->
																<div class="col">
																	<!--begin::Option-->
																	<label class="btn btn-outline btn-outline-dashed btn-outline-default d-flex text-start p-6 active" data-kt-button="true">
																		<!--begin::Radio-->
																		<span class="form-check form-check-custom form-check-solid form-check-sm align-items-start mt-1">
																			<input class="form-check-input" type="radio" name="discount_option" value="1" checked="checked" >
																		</span>
																		<!--end::Radio-->
																		<!--begin::Info-->
																		<span class="ms-5">
																			<span class="fs-4 fw-bolder text-gray-800 d-block">One Time</span>
																		</span>
																		<!--end::Info-->
																	</label>
																	<!--end::Option-->
																</div>
																<!--end::Col-->
																<!--begin::Col-->
																<div class="col">
																	<!--begin::Option-->
																	<label class="btn btn-outline btn-outline-dashed btn-outline-default d-flex text-start p-6" data-kt-button="true">
																		<!--begin::Radio-->
																		<span class="form-check form-check-custom form-check-solid form-check-sm align-items-start mt-1">
																			<input class="form-check-input" type="radio" name="discount_option" value="2" runat="server">
																		</span>
																		<!--end::Radio-->
																		<!--begin::Info-->
																		<span class="ms-5">
																			<span class="fs-4 fw-bolder text-gray-800 d-block">Recurring</span>
																		</span>
																		<!--end::Info-->
																	</label>
																	<!--end::Option-->
																</div>
																<!--end::Col-->
																
															</div>

                                                     <div class="row mb-6">

                                                         <asp:TextBox ID="txtNotes" runat="server" SkinID="txtMulti_80" TextMode="MultiLine" placeholder="Add a message"></asp:TextBox>

                                                         </div>
                                                    </div>

                                                 <div class="row mb-6">
                                                    
                                                     <div class="row row-cols-1 row-cols-md-2 row-cols-lg-1 row-cols-xl-2 g-9 mb-6" data-kt-buttons="true" >
                                                         <div class="col">
                                                             <asp:Label ID="lblTotal" runat="server" Text="Total" Font-Size="22px"></asp:Label>
                                                         </div>
                                                           <div class="col">
                                                               <label id="lbltotalval" style="font-size:22px;font-weight:bold;float:right;visibility:hidden;display:none;">$ 0.00</label>
                                                               <asp:TextBox ID="txtTotal" runat="server" SkinID="Price" ClientIDMode="Static" Text="0.00" Font-Size="32px" ></asp:TextBox>
                                                           </div>
                                                         </div>
                                                     </div>
                                                   <div class="row mb-6">
                                                       <asp:CheckBox ID="chkSelect" runat="server" Text=" I want to Give anonymously" Font-Size="20px" />
                                                       </div>
                                                <div class="row mb-6 d-flex justify-content-center">

                                                    <asp:Button ID="btnPayDetails" runat="server" Text="Next: Payment Details" Font-Size="20px" Width="100%" Height="80px" />
                                                    </div>
                                                </div>
                   </div>
                 
              </div>
             </div>
        <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="btnPayDetails" PopupControlID="pnlAddReligion" >
    </ajaxToolkit:ModalPopupExtender>
    <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
    <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
    <asp:Panel ID="pnlAddReligion" runat="server" BackColor="White" Style="display: none;"
        Width="550px" Height="190px" ScrollBars="None">

        <div class="card card-bordered">


             <div class="card-header">
                <h3 class="card-title">
                    <asp:Label ID="lblModelHeading" runat="server" Text="Cards"></asp:Label>
                </h3>
                <div class="card-toolbar">
                  
                </div>
            </div>
            <div class="card-body">
                 <asp:Panel ID="pnlCardsList" runat="server">
                     <asp:ListView ID="listCards" runat="server">
                         <LayoutTemplate>
              <div class="form-group row">
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>

<ItemTemplate>
      <div class="row row-cols-1 row-cols-md-3 row-cols-lg-1 row-cols-xl-3 g-9 mb-6" data-kt-buttons="true" data-kt-buttons-target="[data-kt-button='true']">
																<!--begin::Col-->
																<div class="col">
                                                                    Card type
                                                                    </div>
          <div class="col">
              Card Number
          </div>
          <div class="col">
              Apply
          </div>
          </div>
    </ItemTemplate>
                         

                     </asp:ListView>

                     </asp:Panel>
                <asp:Panel ID="pnlNewCard" runat="server">
                     <div class="form-group row  mb-6" >
 <label class="col-lg-4 control-label">Card Type</label>
           <div class="col-lg-8">
               <asp:DropDownList ID="ddlCardType" runat="server" SkinID="ddl_200px">
                            <asp:ListItem></asp:ListItem>
                             <asp:ListItem Value="MASTERCARD" Text="MASTERCARD"></asp:ListItem>
                            <asp:ListItem Selected="True" Value="VISA" Text="VISA"></asp:ListItem>
                   <asp:ListItem Value="DISCOVER" Text="DISCOVER"></asp:ListItem>
                    <asp:ListItem Value="AMEX" Text="AMEX"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="ddlCardType" Display="Dynamic" CssClass="error-text" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </div>
	
</div>
<div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Card Number</label>
           <div class="col-lg-8">
               <div id="pnlCreditCard" runat="server" visible="false">
                  
                   <asp:TextBox ID="txtCardNumber" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_200px" MaxLength="20" ClientIDMode="Static"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="rfCardnumber" runat="server" 
                            ControlToValidate="txtCardNumber" Display="Dynamic"  ErrorMessage="Please enter Card Number"></asp:RequiredFieldValidator>
                   </div>
               <div id="pnlCardConnect" runat="server">
                    <asp:TextBox ID="txtCardConnectNumber" ClientIDMode="Static" runat="server" CssClass="paymentinfo-text" SkinID="txt_50" style="display:none;visibility:hidden;"></asp:TextBox>
                   <iframe id="tokenframe" name="tokenframe"  
                       src="https://boltgw.cardconnect.com/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D" 
                       frameborder="0" scrolling="no" width="200" height="35" runat="server"></iframe>
                <asp:HiddenField ID="mytoken" runat="server" ClientIDMode="Static" />
                   </div>
               <p>e.g: 4111222233334444</p>
            </div>
	
</div>
                    
    <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Name </label>
           <div class="col-lg-8">
                <asp:TextBox ID="txtNameOnCard" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_200px" MaxLength="250"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtNameOnCard" Display="None" ErrorMessage="Please enter name on card" ValidationGroup="p"></asp:RequiredFieldValidator>
            </div>
	
</div>
                    <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Expiration</label>
           <div class="col-lg-8 d-flex d-inline">
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
                    <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Card Security Code</label>
           <div class="col-lg-8">
                <asp:TextBox ID="txtCvv" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_75px" MaxLength="10"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtCvv" Display="None" 
                            ErrorMessage="Please enter CVV" ValidationGroup="p" ></asp:RequiredFieldValidator>
                        
               <p>e.g: 123 </p>
            </div>
	
</div>
                      <div class="card-footer d-flex justify-content-end py-6 px-9">
                <asp:Button  ID="btnClose"  runat="server" CssClass="btn btn-light" Text="Close" OnClick="btnClose_Click" /> &nbsp;&nbsp;&nbsp;

              
               

                 <asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Card Details & Pay Now" OnClick="btnSaveRegion_Click" />&nbsp;&nbsp;&nbsp;

                           <asp:Button ID="btnProceed" runat="server" SkinID="btnDefault" Text="Pay Now" OnClick="btnProceed_Click" />
            </div>


    </asp:Panel>
                </div>
            </div>
        </asp:Panel>


     <script type="text/javascript">
         $('#txtAmountTotal').on('keyup', function (e) {
             $('#txtTotal').val($('#txtAmountTotal').val());
         });
         $('#txtAmount').on('keyup', function (e) {
             sum_amt();
         });
         function sum_amt() {
             var add = 0;
             $(".txtamt").each(function () {
                 add += Number($(this).val());
             });
            // alert(add);

             $('#txtAmountTotal').val(add.toFixed(2));
             $('#txtTotal').val(add.toFixed(2));

            // console.log(add);
         }
         //$(function () {
         //    $("#addAll").click(function () {
         //        var add = 0;
         //        $(".amt").each(function () {
         //            add += Number($(this).val());
         //        });
         //        $("#para").text("Sum of all textboxes is : " + add);
         //    });
         //});
     </script>
    <script type="text/javascript">
        var ischeck = 1;
        function showdetails(id) {
           // alert($(id).attr('value'));
           // id.preventDefault();
            var divid = $(id).attr('value');
            ischeck = parseInt( $('#h_' + divid).val());
            if (ischeck == 1) {
                $(id).html("-");
                ischeck = 0;
                $('#h_' + divid).val(0);
                $('#' + divid).show();
            }
            else {
                $(id).html("+");
                ischeck = 1;
                $('#h_' + divid).val(1);
                $('#' + divid).hide();
            }
                 

            return false;
        }


        //$(document).ready(function () {

        //    $("#MainContent_MainContent_listCategory").on('input', '.txtCal', function () {
        //        var calculated_total_sum = 0;

        //        $("#MainContent_MainContent_listCategory .txtCal").each(function () {
        //            var get_textbox_value = $(this).val();
        //            if ($.isNumeric(get_textbox_value)) {
        //                calculated_total_sum += parseFloat(get_textbox_value);
        //            }
        //        });
        //        $("#total_sum_value").html(calculated_total_sum);
        //    });
        //});
    </script>
        
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
