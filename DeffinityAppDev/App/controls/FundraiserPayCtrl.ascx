<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FundraiserPayCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.FundraiserPayCtrl" %>
 <script type="text/javascript">
         window.addEventListener('message', function (event) {
             var token = JSON.parse(event.data);
             var mytoken = document.getElementById('mytoken');
             mytoken.value = token.message;
             var txtCardConnectNumber = document.getElementById("<%=txtCardConnectNumber.ClientID%>");
             txtCardConnectNumber.value = token.message;
        }, false);

 </script>
<script type="text/javascript">
    $(document).ready(function () {

        updateDropdown();


        $('#<%= txtAmountTotal.ClientID %>').on('input', function () {
            updateDropdown();
        });

        $('#<%= lblplatfee.ClientID %>').on('change', function () {
            setFee();
        });
    });

    function updateDropdown() {
      
        debugger;
        var amount = parseFloat($('#<%= txtAmountTotal.ClientID %>').val());
            var dropdown = $('#<%= lblplatfee.ClientID %>');
        debugger;
        if (isNaN(amount) || amount <= 0) {
            dropdown.empty();
            return;
        }
        debugger;
        var options = [
            { text: '5% (£' + (amount * 0.05).toFixed(2) + ')', value: (amount * 0.05).toFixed(2) },
            { text: '10% (£' + (amount * 0.10).toFixed(2) + ')', value: (amount * 0.10).toFixed(2) },
            { text: '15% (£' + (amount * 0.15).toFixed(2) + ')', value: (amount * 0.15).toFixed(2) },
            { text: '20% (£' + (amount * 0.20).toFixed(2) + ')', value: (amount * 0.20).toFixed(2) }
        ];
        debugger;
        dropdown.empty();
        $.each(options, function (index, option) {
            dropdown.append($('<option>', {
                value: option.value,
                text: option.text
            }));
        });
    }
    </script>
<style>
           .mycheckBig input {width:18px; height:18px;}
           .mycheckBig label {padding-left:8px}
       </style>
     <style>
          
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
       .money_cls{
           width:95%;
           font-size:xx-large;

       }
    </style>
<script>

   

  
    function setmoney(val) {
       
        $('#txtAmountTotal').val(val);
        $('#txtTotal').val(val);
       
        updateDropdown();
        setFee();
        debugger;
        return false;
    }
</script>

 <asp:HiddenField ID="hcode" runat="server" Value="" ClientIDMode="Static" />
 <asp:HiddenField ID="hfixedamount" runat="server" Value="0" ClientIDMode="Static" />
<asp:HiddenField ID="hrec" runat="server" Value="1" ClientIDMode="Static" />
 <asp:HiddenField ID="hweek" runat="server" Value="1" ClientIDMode="Static" />
 <asp:HiddenField ID="hamount" runat="server" Value="1" ClientIDMode="Static" />
  <asp:HiddenField ID="hplatformfee" runat="server" ClientIDMode="Static" Value="0" /> 
<asp:HiddenField ID="hplatformfeepercent" runat="server" ClientIDMode="Static" Value="0" /> 
                                                                    <asp:HiddenField ID="hfeepercent" runat="server" ClientIDMode="Static" Value="0" />   
                                                                     <asp:HiddenField ID="hfee" runat="server" ClientIDMode="Static" Value="0" />   


    <div class="row gy-5 g-xl-2 mb-6" id="pnlResult" runat="server" visible="false">
        <div class="col-xxl-12">

             <div class="card card-xxl-stretch" >
											<!--begin::Header-->
											<div class="card-header border-0 py-5">
												<h3 class="card-title fw-bolder text-white"><asp:Label ID="lblTitle" runat="server"></asp:Label> 
                                                   </h3>
												<div class="card-toolbar">
                                                   

                                                    </div>

                                                <div class="card-body p-0" >
                                                    <div class="row mb-6">
         <div class="col-lg-8 col-sm-12  justify-content-center">
             <br />
             </div>
                                                        </div>
                                                     <div class="row mb-6">
         <div class="col-lg-12 col-sm-12 d-flex justify-content-center"  style="text-align:center;">
             <br /> <br /> <br /> <br />
                    <asp:Label ID="lblMsgResult" runat="server" Text="Donation" Font-Bold="true" Font-Size="28px" ForeColor="#7239EA"></asp:Label> <br />
               <asp:HiddenField ID="hPortfolioid" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hunid" runat="server" Value="0" />  <asp:HiddenField ID="huid" runat="server" Value="0" /> 
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

     <div class="row gy-5 g-xl-2 mb-6" id="pnlPrice" runat="server">

      

        <div class="col-xxl-12">

             <div class="card card-xxl-stretch" >
											<!--begin::Header-->
											<div class="card-header border-0 py-5">
												<h3 class="card-title fw-bolder text-white"> </h3>
												<div class="card-toolbar">
                                                   

                                                    </div>

                                                <div class="card-body p-0" >

                                                       
    <div class="row mb-6">
         <div class="col-lg-8">
                    <asp:Label ID="lblDescription" runat="server" Text="Donation" Font-Bold="true" Font-Size="24px"></asp:Label> <br />

                  </div>
         <div class="col-lg-4">
             
             </div>
        </div>
<div class="row mb-4">
                                                <hr />
    </div>

                                                    <asp:Panel ID="pnlCategory" runat="server">
                                                         <div class="row">
                                                          <label style="font-size:20px">   Please select the amount:</label>
                                                             </div>
                                                        <div class="row mb-3">

                                                            <%= GetAmounts() %>
                                                          <%--  <div class="col-lg-3 mb-3"><a href="#" onclick="return setmoney(10);"> <span class="badge badge-light-primary money_cls" >$10.00</span> </a></div>
                                                            <div class="col-lg-3  mb-3"><a href="#" onclick="return setmoney(20);"> <span class="badge badge-light-primary  money_cls">$20.00</span></a></div>
<div class="col-lg-3  mb-3"><a href="#" onclick="return setmoney(50);"> <span class="badge badge-light-primary  money_cls">$50.00</span></a> </div>
<div class="col-lg-3  mb-3"><a href="#" onclick="return setmoney(100);"> <span class="badge badge-light-primary  money_cls">$100.00</span></a></div>--%>

                                                        </div>
                                                         <div class="row">
                                                          <label style="font-size:20px">Or enter your own amount:</label>
                                                             </div>
                                                          <div class="row mb-6">
                                                               <div class="col-lg-12 mr-3">
                                                               <asp:TextBox ID="txtAmountTotal" runat="server" SkinID="Price" MaxLength="10" ClientIDMode="Static" Text="0.00" Font-Size="32px" ></asp:TextBox>
                                                             </div>
                                                                   </div>
                                                        <div class="row">
                                                             <div class="col-lg-12">
                                                   <asp:ListView ID="listCategory" runat="server" InsertItemPosition="None">
 <LayoutTemplate>
              <div class="form-group row mb-6 mx-3">
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>

<ItemTemplate>
  <div class="row mb-4 bg-light-primary px-2 py-2 rounded-2 ">
              <div class="col-lg-9 pt-6">
                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Name") %>' Font-Size="22px"></asp:Label> 
                  </div>
        <div class="col-lg-3">
            
            <asp:LinkButton style="float:right;" ID="btn" runat="server" ClientIDMode="Static" Text="+" SkinID="BtnLinkButton" OnClientClick="javascript:return  showdetails(this);" Font-Bold="true" Font-Size="32px" value='<%# Eval("ID") %>'></asp:LinkButton>
            <input type="hidden" id='h_<%# Eval("ID").ToString() %>' value="1" />     </div>
      </div>
    <div class="row mb-4" style="display:none;" id='<%# Eval("ID").ToString() %>'>
         <div class="col-lg-8">
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label> 
                  </div>
         <div class="col-lg-4">
             <asp:TextBox ID="txtAmount" runat="server" ClientIDMode="Static" Text="0" Font-Size="22px" Visible="false"></asp:TextBox>
             <input type="text" runat="server" id="txtamount_list" class="form-control form-control-lg form-control-solid txtamt" style="text-align:right;font-size:22px" value="0" onkeyup="sum_amt();"   />
             </div>
        </div>

</ItemTemplate>
</asp:ListView>
                                                                 </div>
                                                            </div>
                                                          <div class="row  mb-6 d-flex d-inline">
                                                                <div class="col-lg-12 mb-6 d-flex d-inline justify-content-between">
                                                                    <asp:Label ID="lblplatform" runat="server" style="font-size:20px;margin-left:5px;margin-right:5px" Text="Please help the <charity name> by contributing towards the platform fee:"></asp:Label>
                                                                    <asp:DropDownList id="lblplatfee" runat="server" ClientIDMode="Static" CssClass="form-select form-select-lg fw-bold" style="width:200px;width:200px;font-size: 20px;"></asp:DropDownList>
                                                                    </div>
                                                              </div>
                                                           <div class="row  mb-6 d-flex d-inline">
                                                                <div class="col-lg-12">
                                                       <asp:CheckBox ID="chkAnonymously" runat="server" Text=" " Font-Size="20px" CssClass="mycheckBig" ClientIDMode="Static" />
                                                               <label style="font-size:20px;margin-left:0px;">I want to give anonymously</label>
                                                                    </div>
                                                       </div>
                                                          <div class="row  mb-6 " >
                                                                <div class="col-lg-12 mb-6 d-flex d-inline justify-content-between">
                                                                    <label style="font-size:20px;margin-left:5px;margin-right:5px" class="control-label"> Total Amount</label>
                                                                      <asp:TextBox ID="txtTotalAmt" runat="server" SkinID="Price_175px" Visible="false"></asp:TextBox>
                                                                           <asp:Label ID="lblptotal" runat="server" ClientIDMode="Static" Font-Bold="true" Font-Size="X-Large" CssClass="form-control form-control-lg" style="text-align:right;width:25%"></asp:Label>
                                                                    </div>
                                                                      
                                                                       </div>
                                                          <div class="row  mb-6">
                                                              <div class="col-lg-4 mb-6">
                                                                   <div class="row  mb-6">
                                                                       <label class="form-label" style="font-size:22px;margin-left:5px; font-weight:bold"> Turn <label id="lblgtotal"></label> into <label id="lblgptotal"></label> with Gift Aid  </label>
                                                                       </div>
                                                                   <div class="row  mb-6">
                                                                        <div class="col-lg-12 d-flex d-inline">
                                                       <asp:CheckBox ID="chkgift" runat="server" Text="" Font-Size="20px" CssClass="mycheckBig pt-2" ClientIDMode="Static"  />
                                                            <label style="font-size:20px;margin-left:5px;"> Please Gift Aid this donation  </label> 
                                                                
                                                                </div>
                                                                       </div>
                                                                  </div>


                                                              <div class="col-lg-4 mb-6" >
                                                                  <asp:Image ID="imggift" runat="server" ImageUrl="~/assets/GiftAidLogo.png" CssClass="img-fluid" Height="80" />
                                                                  </div>
                                                             </div>
                                                         <div class="row  mb-12 " style="display:none;visibility:hidden;">
                                                                <div class="col-lg-12 d-flex d-inline">
                                                       <asp:CheckBox ID="chkfee" runat="server" Text="" Font-Size="20px" CssClass="mycheckBig pt-2" ClientIDMode="Static" Checked="true" />
                                                            <label style="font-size:20px;margin-left:5px;"> Yes! I would like to cover the transaction fee and platform support cost of <lable id="lblfee" style="font-weight:bold;">0.00</lable> so that <asp:Label ID="lblOrg" runat="server"></asp:Label> can benefit from the full donation  </label> 
                                                                
                                                                </div>
                                                       </div>
                                                          <%-- <div class="row  mb-6 d-flex d-inline">
                                                                <div class="col-lg-12">
                                                       <asp:CheckBox ID="chkfee" runat="server" Text="" Font-Size="20px" CssClass="mycheckBig" ClientIDMode="Static" Checked="true" />
                                                           <label style="font-size:20px;margin-left:5px;"> Yes! I would like to cover the transaction fee and platform support cost of <lable id="lblfee" style="font-weight:bold;">0.00</lable> so that <asp:Label ID="lblOrg" runat="server"></asp:Label> can benefit from the full donation  </label> 
                                                             
                                                                </div>
                                                       </div>--%>
                                                       <%-- <div class="row mb-6">
                                                                <div class="col-lg-12 d-flex justify-content-center">
                                                                <label style="font-size:20px;margin-left:5px;margin-right:5px" class="control-label">   Total amount: </label> <asp:Label ID="lblptotal" runat="server" ClientIDMode="Static" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                                                                    </div>
                                                              </div>--%>
                                                          <div class="card-footer py-3 px-5 mb-10">
                                                              <div class="row d-flex justify-content-around"> 
                                                                  <div class="col-lg-3 text-center mt-5">
                                                                      <asp:Button ID="btnBack" runat="server" SkinID="btnDefault" Text="Back" Height="60px" Width="70%" Font-Size="22px" OnClick="btnBack_Click"/>
                                                                      </div>
                                                                  <div class="col-lg-3 text-center mt-5">
 
                    <asp:Button ID="btnNextCategory" runat="server" SkinID="btnNext" Height="60px" Width="70%" Font-Size="22px" OnClick="btnNextCategory_Click"/>
                                                                   <asp:Button ID="btnNextPaynow" runat="server" Font-Size="22px" SkinID="btnDefault" Text="Pay Now" Height="60px" Width="70%" OnClick="btnProceed_Click" style="display:none;" />
                                                                  
                                                                  </div> 

                                                              </div>
                </div>                             
    

</asp:Panel>
                                                    
  <asp:Panel ID="pnlPaymentOptioin" runat="server">
        <div class="row mb-6">
                                                        <asp:Label ID="lblTitleSub" runat="server" Text="How often would you like to give?" Font-Bold="true" Font-Size="22px" ></asp:Label>
                                                    </div>
      <div class="row" style="min-height:300px">
                                                    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-1 row-cols-xl-2 g-9 mb-6" >
																<!--begin::Col-->
																<div class="col">
																	<!--begin::Option-->
																	<label class="btn btn-outline btn-outline-dashed btn-outline-default d-flex text-start p-6 active" data-kt-button="true">
																		<!--begin::Radio-->
																		<span class="form-check form-check-custom form-check-solid form-check-sm align-items-start mt-1">
																			<input class="form-check-input" type="radio" name="discount_option" value="1"  runat="server" id="chkonetime2" >
                                                                           
																		</span>
																		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<!--end::Radio--><!--begin::Info--><span class="ms-5"><span class="fs-4 fw-bolder text-gray-800 d-block">One Time</span>
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
																			<input class="form-check-input" type="radio" name="discount_option" value="2" runat="server" id="chkRecurring">
																		</span>
																		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<!--end::Radio--><!--begin::Info--><span class="ms-5"><span class="fs-4 fw-bolder text-gray-800 d-block">Recurring</span>
																		</span>
																		<!--end::Info-->
																	</label>
																	<!--end::Option-->
																</div>
																<!--end::Col-->
																
															</div>
          </div>
        <div class="card-footer mb-10">
            <div class="row d-flex justify-content-around">
                <div class="col-sm-3 mt-5"> <asp:Button ID="btnBackToCategory" runat="server" SkinID="btnDefault" Text="Back" Height="60px" Width="70%" OnClick="btnBackToCategory_Click" style="margin-right:50px" /></div>
                <div class="col-sm-3  mt-5"> <asp:Button ID="btnNextToPayAdvanced" runat="server" SkinID="btnNext" Height="60px" Width="70%" OnClick="btnNextToPayAdvanced_Click" style="float:right;" /></div>
              
              

            </div>
                    
                   
                </div>                             
   

      </asp:Panel>

      <asp:Panel ID="pnlRecurringOption" runat="server" ClientIDMode="Static">
          <div class="row mb-6">
                                                        <asp:Label ID="Label5" runat="server" Text="How frequently would you like to donate?" Font-Bold="true" Font-Size="22px" ></asp:Label>
                                                    </div>
        

                                                              <div class="row row-cols-1 row-cols-md-2 row-cols-lg-1 row-cols-xl-2 g-9 mb-6" data-kt-buttons="true" data-kt-buttons-target="[data-kt-button='true']">
																<!--begin::Col-->
																<div class="col" style="display:none;visibility:hidden;">
																	<!--begin::Option-->
																	<label class="btn btn-outline btn-outline-dashed btn-outline-default d-flex text-start p-6 active" data-kt-button="true">
																		<!--begin::Radio-->
																		<span class="form-check form-check-custom form-check-solid form-check-sm align-items-start mt-1">
																			<input class="form-check-input" type="radio" name="discount_option1" value="4" runat="server" id="chkWeekly">
                                                                           
																		</span>
																		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<!--end::Radio--><!--begin::Info--><span class="ms-5"><span class="fs-4 fw-bolder text-gray-800 d-block">Weekly</span>
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
																			
                                                                            <input class="form-check-input" type="radio" name="discount_option1" value="3"  runat="server" id="chkMontly" >
																		</span>
																		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<!--end::Radio--><!--begin::Info--><span class="ms-5"><span class="fs-4 fw-bolder text-gray-800 d-block">Monthly</span>
																		</span>
																		<!--end::Info-->
																	</label>
																	<!--end::Option-->
																</div>
																<!--end::Col-->
																
															</div>

                                                            
                                                              <div class="row row-cols-1 row-cols-md-2 row-cols-lg-1 row-cols-xl-2 g-9 mb-6" data-kt-buttons="true" data-kt-buttons-target="[data-kt-button='true']"  id="spnl" runat="server" >
																<!--begin::Col-->
																<div class="col">
																	<!--begin::Option-->
																	<label class="col-lg-12 control-label">Start Date</label>
                                                                  <div class="col-lg-12 d-flex d-inline">
                                                                      <asp:TextBox ID="txtStartDate" runat="server" SkinID="DateNew"></asp:TextBox>
                                                                        <asp:CompareValidator 
    ID="cpStartDate" 
    runat="server" 
    ControlToValidate="txtStartDate" 
    Operator="GreaterThanEqual" 
    Type="Date" 
    ErrorMessage="Please enter the current date or a future date" Display="Dynamic" style="font-size:small" ValidationGroup="date1" />
                                                                      </div>
																	<!--end::Option-->
																</div>
																<!--end::Col-->
																<!--begin::Col-->
																<div class="col" style="display:none;visibility:hidden;">
																	<!--begin::Option-->
																	<label class="col-lg-12 control-label">End Date</label>
                                                                    <div class="col-lg-12 d-flex d-inline">
                                                                      <asp:TextBox ID="txtEndDate" runat="server" SkinID="DateNew"></asp:TextBox>
                                                                      </div>
																	<!--end::Option-->
																</div>
																<!--end::Col-->
																
															</div>
                                                             <div class="row row-cols-1 row-cols-md-2 row-cols-lg-1 row-cols-xl-2 g-9 mb-6" data-kt-buttons="true" data-kt-buttons-target="[data-kt-button='true']"  id="dpnl" runat="server" visible="false">
																<!--begin::Col-->
																<div class="col">
																	<!--begin::Option-->
																	<label class="col-lg-12 control-label">Start Day</label>
                                                                    <div class="col-lg-12 d-flex d-inline"><%--<asp:TextBox ID="txtStartDay" runat="server" ></asp:TextBox> --%>

                                                                        <asp:DropDownList ID="txtStartDay" runat="server" SkinID="ddl_125px">
                                                                           
                                                                        </asp:DropDownList>

                                                                    </div>
																	<!--end::Option-->
																</div>
																<!--end::Col-->
																<!--begin::Col-->
																<div class="col">
																	<!--begin::Option-->
																	
																	<!--end::Option-->
																</div>
																<!--end::Col-->
																
															</div>
             <div class="card-footer  py-3 px-5 mb-10">
                 <div class="row d-flex justify-content-around">
               <div class="col-sm-5 mt-5"> <asp:Button ID="btnRecurringBack" runat="server" SkinID="btnDefault" Text="Back" Height="100%" Width="70%" OnClick="btnRecurringBack_Click"  style="margin-right:50px" /></div>
                <div class="col-sm-5 mt-5">  <asp:Button ID="btnNextUserInfo" runat="server" SkinID="btnNext" Height="60px" Width="70%"  OnClick="btnNextUserInfo_Click" ValidationGroup="date1" style="float:right" /></div>
                    
                  
                     
                  
                           
                     </div>
                                     
    </div>
                                                        </asp:Panel>
                                                     <asp:Panel ID="pnlUserInfo" runat="server"  ClientIDMode="Static">

                                                            <div class="row mb-6">
                                                        <asp:Label ID="Label6" runat="server" Text="Donor Information" Font-Bold="true" Font-Size="22px" ></asp:Label>
                                                    </div>
                                                         
                                                          <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">First Name </label>
           <div class="col-lg-8">
                <asp:TextBox ID="txtNameOnCard" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_90" MaxLength="250"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtNameOnCard" Display="Dynamic" ErrorMessage="Please enter first name" ValidationGroup="user"></asp:RequiredFieldValidator>
            </div>
	
</div>
                                                           <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Last Name </label>
           <div class="col-lg-8">
                <asp:TextBox ID="txtLastname" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_90" MaxLength="250"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtLastname" Display="Dynamic" ErrorMessage="Please enter last name" ValidationGroup="user"></asp:RequiredFieldValidator>
                       
            </div>
	
</div>
                                                                  <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Email </label>
           <div class="col-lg-8">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_90" MaxLength="250"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                            ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Please enter email" ValidationGroup="user"></asp:RequiredFieldValidator>
                       
            </div>
	
</div>
                                                                  <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Phone number </label>
           <div class="col-lg-8">
                <asp:TextBox ID="txtPhone" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_90" MaxLength="250"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                            ControlToValidate="txtPhone" Display="Dynamic" ErrorMessage="Please enter phone number" ValidationGroup="user"></asp:RequiredFieldValidator>
                       
            </div>
	
</div>
                                                            <div class="card-footer  py-3 px-5 mb-10">
                                                                <div class="row d-flex justify-content-around">
               <div class="col-sm-5 mt-5"> 
               
                      <asp:Button ID="btnBackToOptions" runat="server" SkinID="btnDefault" Text="Back" Height="60px" Width="70%" OnClick="btnBackToOptions_Click"  style="margin-right:50px"/>
                  </div>
                                                                     <div class="col-sm-5 mt-5">
                  <asp:Button ID="btnNextToCardDetails" runat="server" SkinID="btnDefault" Text="Pay Now" Height="60px" Width="70%" OnClick="btnProceed_Click" style="float:right" ValidationGroup="user" />
                           </div>
                     </div>
                   
                   
                </div>                             
    

                                                         </asp:Panel>

                                                     <asp:Panel ID="pnlnewCard" runat="server" ClientIDMode="Static">

                                                            <div class="row mb-6">
                                                        <asp:Label ID="Label4" runat="server" Text="Card Details" Font-Bold="true" Font-Size="22px" ></asp:Label>
                                                    </div>

   
                                                                
                     <div class="form-group row  mb-6" >
 <label class="col-lg-4 control-label">Card Type</label>
           <div class="col-lg-8">
               <asp:DropDownList ID="ddlCardType" runat="server" SkinID="ddl_90">
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
               <div id="pnlCardConnect" runat="server" style="height:50px">
                   <asp:TextBox ID="txtCardConnectNumber" placeholder="Card Number" runat="server" ></asp:TextBox>
                 		<iframe id="tokenframe" name="tokenframe"  
                       src="https://boltgw.cardconnect.com/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D" 
                       frameborder="0" scrolling="no" width="300" height="55" runat="server" visible="false"></iframe>
                <asp:HiddenField ID="mytoken" runat="server" ClientIDMode="Static" />
                   </div>
               <p>e.g: 4111222233334444</p>
            </div>
	
</div>
                    
   
                    <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Expiration</label>
           <div class="col-lg-8 d-flex d-inline">
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="paymentinfo-text" SkinID="ddl_125px">
                        </asp:DropDownList> &nbsp; &nbsp; &nbsp;
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
                            SkinID="txt_100px" MaxLength="5"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtCvv" Display="None" 
                            ErrorMessage="Please enter CVV" ValidationGroup="p" ></asp:RequiredFieldValidator>
                        
               <p>e.g: 123 </p>
            </div>
	
</div>
                      <div class="card-footer  py-3 px-5 mb-10">
                          <div class="row d-flex justify-content-around">
               
  <div class="col-sm-5 mt-5">                      <asp:Button  ID="btnClose"  runat="server" SkinID="btnDefault" Text="Back" OnClientClick="" Height="60px" Width="70%" style="margin-right:50px" /> 
                  </div>
                          <div class="col-sm-5 mt-5">   
                   <asp:Button ID="btnProceed" runat="server" SkinID="btnDefault" Text="Pay Now" OnClick="btnProceed_Click" Height="60px" Width="70%"  style="float:right;" />

                           </div>
                              
                          
                     </div>
               

              
               

                

                         
            </div>

</asp:Panel>

               <div class="card card-xxl-stretch" style="display:none;visibility:hidden;" >
											<!--begin::Header-->
											<div class="card-header border-0 py-5">
												<h3 class="card-title fw-bolder text-white"> </h3>
												<div class="card-toolbar">

                                                     <asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Card & Pay Now" OnClick="btnSaveRegion_Click" Height="60px" style="display:none;visibility:hidden;" />
                                                    </div>
                                            
                                                <div class="card-body p-0" id="pnl_paytype">

                                                  

                                                     <div class="row mb-6">

                                                         <asp:TextBox ID="txtNotes" runat="server" SkinID="txtMulti_80" TextMode="MultiLine" placeholder="Add a message"></asp:TextBox>

                                                         </div>
                                                   

                                                 <div class="row mb-6">
                                                    
                                                     <div class="row row-cols-1 row-cols-md-2 row-cols-lg-1 row-cols-xl-2 g-9 mb-6" data-kt-buttons="true" >
                                                         <div class="col">
                                                             <asp:Label ID="lblTotal" runat="server" Text="Total" Font-Size="22px"></asp:Label>
                                                         </div>
                                           
                                                               <asp:TextBox ID="txtTotal" runat="server" SkinID="Price" ClientIDMode="Static" Text="0.00" Font-Size="32px" ></asp:TextBox>
                                                           </div>
                                                         </div>
                                                     </div>
                                                   <div class="row mb-6">
                                                     
                                                       </div>
                                                <div class="row mb-6 d-flex justify-content-center">

                                                    <asp:Button ID="btnPayDetails"  runat="server" Text="Next: Payment Details" Font-Size="20px" Width="100%" Height="80px" OnClientClick="fnpaydetails();" />
                                                    </div>
                                                </div>

                                                    <div class="card-body p-0" id="pnl_paydetails" style="display:none;" >
                                                         <div class="row mb-6">
                                                        <asp:Label ID="Label1" runat="server" Text="Payment Details" Font-Bold="true" Font-Size="22px" style="padding-bottom:10px"></asp:Label>
                                                             
                                                               <hr  />
                                                    </div>

                                                        
                                                        <asp:HiddenField ID="hcount" runat="server" ClientIDMode="Static" Value="0" />
                                                        <asp:Panel ID="pnlListCards" runat="server" ClientIDMode="Static">

                                                              <asp:ListView ID="listCards" runat="server" OnItemCommand="listCards_ItemCommand">
                         <LayoutTemplate>
              <div class="form-group row  mb-6" >
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>

<ItemTemplate>

    <div class="card card-dashed h-xl-100 flex-row flex-stack flex-wrap p-6">
													<!--begin::Info-->
													<div class="d-flex flex-column py-2">
														<!--begin::Owner-->
														<div class="d-flex align-items-center fs-4 fw-bolder mb-5"><asp:Label ID="Label2" runat="server" Text='<%# Eval("name") %>' ></asp:Label> 
														<%--<span class="badge badge-light-success fs-7 ms-2">Primary</span>--%></div>
														<!--end::Owner-->
														<!--begin::Wrapper-->
														<div class="d-flex align-items-center">
															<!--begin::Icon-->
															<img src='<%# Eval("imageurl") %>' alt="" class="me-4" runat="server">
															<!--end::Icon-->
															<!--begin::Details-->
															<div>
																<div class="fs-4 fw-bolder"><asp:Label ID="lblcardtype" runat="server" Text='<%# Eval("cardtype") %>'></asp:Label> **** <asp:Label ID="lblnumber" runat="server" Text='<%# Eval("CardNumbder") %>'></asp:Label> </div>
																<div class="fs-6 fw-bold text-gray-400">Card expires at <asp:Label ID="lblmonth" runat="server" Text='<%# Eval("month") %>'></asp:Label>/<asp:Label ID="lblyear" runat="server" Text='<%# Eval("year") %>'></asp:Label></div>
															</div>
															<!--end::Details-->
														</div>
														<!--end::Wrapper-->
													</div>
													<!--end::Info-->
													<!--begin::Actions-->
													<div class="d-flex align-items-center py-2">

                                                        <asp:Button ID="btnPayNowListView" runat="server" CausesValidation="false" CommandName="paynow" CssClass="btn btn-sm btn-light btn-active-light-primary me-3" Text="Pay Now" CommandArgument='<%# Eval("ID") %>' />
														<%--<button type="reset" class="btn btn-sm btn-light btn-active-light-primary me-3">Pay Now</button>
														<button class="btn btn-sm btn-light btn-active-light-primary" data-bs-toggle="modal" data-bs-target="#kt_modal_new_card" style="display:none;visibility:hidden;">Edit</button>--%>
													</div>
													<!--end::Actions-->
												</div>
    <%--  <div class="row row-cols-1 row-cols-md-3 row-cols-lg-1 row-cols-xl-3 g-9 mb-6" data-kt-buttons="true" data-kt-buttons-target="[data-kt-button='true']">
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
          </div>--%>
    </ItemTemplate>
                         

                     </asp:ListView>

  <div class="row mb-6">
    <asp:Button ID="btnAddNewCard" runat="server" Text="Add New Card" OnClientClick="shownewcard();" />
    </div>
    </asp:Panel>

                                                      

                                                           

                                                            
                                                    </div>
                                                </div>
                   </div>


                                                    </div>
                                                    </div>
                                                </div>
                 </div>
             <%-- <div class="col-xxl-4">
                 
              </div>--%>
            
        <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="lbl_lbtnClosePassword" PopupControlID="pnlAddReligion" >
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
                   
                     </asp:Panel>
                
                </div>
            </div>
        </asp:Panel>
    

     <script type="text/javascript">

         $(document).ready(function () {
             setmoney(10);

            
             $("#input[id$='btnAddNewCard']").click(function () {


                 $("#pnlnewCard").show();
                 $("#pnlListCards").hide();

                 return false
             });

             $("input[id$='btnPayDetails']").click(function () {

                 //txtNameOnCard
                 // $(window).scrollTop($("[id$='txtNameOnCard']").position().top);
                 $("[id$='txtNameOnCard']").focus();
                 //   alert($("#MainContent_MainContent_chkonetime2").is(": checked"));
                 // alert($("#MainContent_MainContent_chkonetime2").is(":checked"));

                 // if ($("#MainContent_MainContent_taithingNewCtrl_chkonetime2").is(":checked")) {
                 // $("[id$='chkonetime2']").
                 if ($("[id$='chkonetime2']").is(":checked")) {
                     $("#pnlRecurringOption").hide();
                 }
                 else {
                     $("#pnlRecurringOption").show();
                 }


                 var cnt = $("input[id$='hcount']").val();
                 
                 if (cnt == "0") {
                     $('#pnl_paytype').hide();

                     $('#pnl_paydetails').show();
                     $("#pnlnewCard").show();
                     $("#pnlListCards").hide();
                 }
                 else {
                     $('#pnl_paytype').hide();

                     $('#pnl_paydetails').show();
                 }

                 return false
             });



         });




         $('#txtAmountTotal').on('keyup', function (e) {
             var mn = $('#txtAmountTotal').val();
             if (mn.length > 0) {


                 $('#txtTotal').val($('#txtAmountTotal').val());
                 setmoney(parseFloat($('#txtAmountTotal').val()));
             }
             else {
                 $('#txtTotal').val(0);
                 setmoney(parseFloat(0));
             }
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
             $("#hamount").val(add.toFixed(2));
             // console.log(add);
         }

         function shownewcard() {
             $("#pnlnewCard").show();
             $("#pnlListCards").hide();
             return false;
         }

         function fnpaydetails() {
             $('#pnl_paytype').hide();

             $('#pnl_paydetails').show();
             return false;
         }

     </script>
    <script type="text/javascript">
        var ischeck = 1;
        function showdetails(id) {

            var divid = $(id).attr('value');
            ischeck = parseInt($('#h_' + divid).val());
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
        //btnSaveRegion
    </script>
<%--<script>
    function setmoney(val) {

        // debugger;
        //var add = 0;
        //$(".txtamt").each(function () {
        //    add += Number($(this).val());
        //});
        // alert(add);

        $('#txtAmountTotal').val(val);
        $('#txtTotal').val(val);
        // $('#lblptotal').html(val.toFixed(2));

        setFee();
        debugger;
        return false;
    }
</script>--%>

 <script type="text/javascript">

     $(document).ready(function () {

         function showpanels(category, payoption, recurring, userinfo, carddetails) {

             if (category == true) {
                 $("[id$='pnlCategory']").show();
             }
             else {
                 $("[id$='pnlCategory']").hide();
             }

             if (payoption == true) {
                 $("[id$='pnlPaymentOptioin']").show();
             }
             else {
                 $("[id$='pnlPaymentOptioin']").hide();
             }

             if (recurring == true) {
                 $("[id$='pnlRecurringOption']").show();
             }
             else {
                 $("[id$='pnlRecurringOption']").hide();
             }

             if (userinfo == true) {
                 $("[id$='pnlUserInfo']").show();
             }
             else {
                 $("[id$='pnlUserInfo']").hide();
             }

             if (carddetails == true) {
                 $("[id$='pnlnewCard']").show();
             }
             else {
                 $("[id$='pnlnewCard']").hide();
             }

             //if (paytype == true) {
             //    $("[id$='pnlPaymentType']").show();
             //}
             //else {
             //    $("[id$='pnlPaymentType']").hide();
             //}
         }


         //category, payoption, recurring, userinfo, carddetails
         showpanels(true, false, false, false, false);



         $("[id$='btnNextCategory']").click(function () {

             //category, payoption, recurring, userinfo, carddetails
             showpanels(false, true, false, false, false);


             //if ($('#chkAnonymously').is(":checked")) {
             //    //$("[id$='btnNextCategory']").hide();
             //    $("[id$='btnNextPaytype_paynow']").show();
             //    $("[id$='btnNextPaytype']").show();
             //  //  showpanels(false, false, false, false, false, true);

             //   // return false;
             //}
             //else {
             //    $("[id$='btnNextPaytype_paynow']").hide();
             //    $("[id$='btnNextPaytype']").hide();
             //}

             return false;
         });
         $("[id$='btnNextPaytype']").click(function () {

             //category, payoption, recurring, userinfo, carddetails
             showpanels(false, true, false, false, false);

             return false;
         });

         $("[id$='btnBackPaytype']").click(function () {

             //category, payoption, recurring, userinfo, carddetails
             showpanels(true, false, false, false, false);

             return false;
         });
         //btnBackToCategory
         $("[id$='btnBackToCategory']").click(function () {

             //category, payoption, recurring, userinfo, carddetails
             showpanels(true, false, false, false, false);


             return false;
         });
         //chkRecurring
         $("[id$='chkRecurring']").click(function () {
             var ck = $("[id$='chkRecurring']").is(":checked");

             if (ck) {
                 $("#hrec").val("2");
             }
             else {
                 $("#hrec").val("1");
             }
             //category, payoption, recurring, userinfo, carddetails
             showpanels(false, false, true, false, false);
             // alert($("#hrec").val());
             return false;
         });

         //btnNextToPayAdvanced
         $("[id$='btnNextToPayAdvanced']").click(function () {

             //alert(String($("[id$='chkRecurring']").is(":checked")));
             var ck = $("[id$='chkRecurring']").is(":checked");
             //category, payoption, recurring, userinfo, carddetails

             if (ck) {
                 showpanels(false, false, true, false, false);
             }
             else {
                 showpanels(false, false, false, true, false);
             }



             return false;
         });

         //btnNextToPayAdvanced
         $("[id$='btnRecurringBack']").click(function () {
             //category, payoption, recurring, userinfo, carddetails
             showpanels(false, true, false, false, false);

             return false;
         });

         //btnNextToPayAdvanced
         $("[id$='btnBackToOptions']").click(function () {
             //category, payoption, recurring, userinfo, carddetails
             showpanels(false, true, false, false, false);

             return false;
         });

         //btnNextToPayAdvanced
         // btnNextUserInfo
         $("[id$='btnNextUserInfo']").click(function () {
             //alert("tst");
             //category, payoption, recurring, userinfo, carddetails
             showpanels(false, false, false, true, false, false);

             return false;
         });

         //$("[id$='btnNextToCardDetails']").click(function () {

         //    if (Page_ClientValidate('user')) {
         //        showpanels(false, false, false, false, true);
         //    }
         //    //alert("tst");
         //    //category, payoption, recurring, userinfo, carddetails


         //    return false;
         //});

         //$btnClose


         $("[id$='btnClose']").click(function () {
             //category, payoption, recurring, userinfo, carddetails
             showpanels(false, false, false, true, false, false);

             return false;
         });
         //$("[id$='btnProceed']").click(function () {
         //    //category, payoption, recurring, userinfo, carddetails
         //    //showpanels(false, false, false, false, false);



         //    return true;
         //});

     });

     function myFunction(fixedfee, tranfee, code) {
         // alert("Parameter 1: " + fixedfee + ", Parameter 2: " + tranfee + ", Parameter 3: " + code);
         $("#hfeepercent").val(tranfee);
         $("#hfixedamount").val(fixedfee);
         $("#hcode").val(code);
         //hcode
         setFee();
     }

     function add15Percent(amount) {
         var percentage = (15 / 100) * amount;
         var totalAmount = amount + percentage;
         return totalAmount;
     }

     function setFee() {
         var t = Number($('#txtAmountTotal').val());

         var pval = parseFloat($('#<%= lblplatfee.ClientID %>').val());

         $("#hplatformfeepercent").val(pval);


         var pr = 0;
         var t_total = (t + pval).toFixed(2);
         $('#lblfee').html(pval);
         $('#hamount').val(t_total);
         $('#lblptotal').html('£' + t_total);


     }
     //  setFee();

     //function for is checked
     //funtion Check_Update()
     //{
     //    var t = Number($('#txtAmountTotal').val());
     //    var p = Number($("#hfeepercent").val());
     //    var f = Number($("#hplatformfee").val());
     //    if (p > 0) {
     //        var r = t * (p / 100).toFixed(2) + f;
     //        $('#txtAmountTotal').val((t + r).toFixed(2));
     //        // $('#txtTotal').val((t + r).toFixed(2));

     //        $('#lblptotal').html((t + r).toFixed(2));
     //        $('#hfee').val(r);


     //    }
     //}

     $('#chkfee').change(function () {

         setFee();
         //if (this.checked) {

         //    chkfee_checked();

         //}
         //else {


         //    var t = Number($('#txtAmountTotal').val());
         //    var p = Number($('#hfee').val());
         //    var f = Number($("#hplatformfee").val());

         //    if (p > 0) {
         //       // var r = t * (p / 100).toFixed(2);
         //       // $('#txtAmountTotal').val((t - p).toFixed(2));
         //        $('#txtAmountTotal').val($('#txtTotal').val());
         //        //$('#txtTotal').val((t + r).toFixed(2));
         //        $('#lblptotal').html($('#txtTotal').val());
         //        $('#hamount').val($('#txtTotal').val());


         //    }
         //}

     });
     $('#chkAnonymously').change(function () {

         if ($('#chkAnonymously').is(":checked")) {
             $("[id$='btnNextPaytype']").hide();
             $("[id$='btnNextPaytype_paynow']").show();
             // showpanels(false, false, false, false, false, true);

             return false;
         }
         else {
             $("[id$='btnNextPaytype']").show();
             $("[id$='btnNextPaynow']").hide();
             $("[id$='btnNextPaytype_paynow']").hide();
         }

         setFee();


     });

     $(document).ready(function () {
         $("[id$='btnNextPaynow']").hide();
     });

 </script>


