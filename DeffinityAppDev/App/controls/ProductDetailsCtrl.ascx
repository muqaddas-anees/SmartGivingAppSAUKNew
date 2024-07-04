<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetailsCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.ProductDetailsCtrl" %>
<script type="text/javascript">
    window.addEventListener('message', function (event) {
        var token = JSON.parse(event.data);
        var mytoken = document.getElementById('mytoken');
        mytoken.value = token.message;
        console.log(token.message);
        var txtCardConnectNumber = document.getElementById("<%=txtCardConnectNumber.ClientID%>");
         txtCardConnectNumber.value = token.message;
         console.log(txtCardConnectNumber.value);
     }, false);

</script>
 <div class="row mx-10">
      <div class="col-lg-12">
         
          
          
          
          <div class="card mb-5 mb-xl-10 ">
   
    <!--begin::Content-->
    <div id="kt_account_profile_details" class="collapse show" style="">
        <!--begin::Form-->
        <form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
            <!--begin::Card body-->
            <div class="card-body border-top p-9">
                <!--begin::Input group-->

                <div class="row p-5">

                    <div class="row mb-6">
                       
                    </div>

                    <div class="row p-5">
                         <h3 class="fw-bolder m-0 text-center">

                <asp:Label ID="lblTitle" runat="server" Font-Size="32px"></asp:Label> <asp:HiddenField ID="hmoney" runat="server" />
            </h3>
                    </div>

                   <div class="row">
                         <div class="col-lg-3">
                              <div class="row  mt-5">
                            <asp:Image ID="imgcenterimage" runat="server" CssClass="img-fluid" />
                        </div>
                             </div>
                        <div class="col-lg-9">
                             <br />
                            <div class="row" id="pnlProductDetails" runat="server">  
                               <div class="row">
                    <h4 class="fs-1"> Description </h4>

                </div>
                    <div class="row fs-2 min-h-400px mb-6">
                        
                       
                            <asp:Label ID="lblDescription" Text="" runat="server" /><br />
                            <br />
                        </div>

                               <div class="row fs-2 mb-6">
                         <div class="col-lg-12">
                              <asp:Label ID="lblPriceTitle" Text="Price:" runat="server" Font-Bold="true" /><br />
                            <asp:Label ID="lblPrice" Text="" runat="server" Font-Bold="true" />
                             </div>
                             <br />
                            <br />
                        </div>

                                <asp:Panel ID="pnlQty" runat="server">
                                  <div class="row fs-2 mb-6">
                         <div class="col-lg-2">
                              <asp:Label ID="Label1" Text="Quantity:" runat="server" Font-Bold="true" /><br />
                           <asp:TextBox TextMode="Number" ID="txtQTY" runat="server" Text="1"></asp:TextBox>
                             </div>
                             <br />
                            <br />
                        </div>

                               <div class="row fs-2">
                        
                                    <div class="col-lg-12">
                          <asp:Button ID="btnBuy" runat="server" Text="Buy Now" SkinID="btnDefault" style="height:80px;width:150px;font-size:20px" OnClick="btnBuy_Click" />
                                        
                                        </div><br />
                            <br />
                        </div>

                                    </asp:Panel>

                                 <asp:Panel ID="pnlNoStock" runat="server">
                                      <div class="row fs-2">
                        
                                    <div class="col-lg-12">
                         <asp:Label ID="lblNostock" runat="server" Text="Out of Stock" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        
                                        </div><br />
                            <br />
                        </div>
                                     </asp:Panel>

                                </div>


                             <div class="row" id="pnlUserDetails" runat="server" visible="false">  
                                    <div class="row mb-6">
                                                        <asp:Label ID="Label6" runat="server" Text="Contact Information" Font-Bold="true" Font-Size="22px" ></asp:Label>
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
                                   <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Address</label>
           <div class="col-lg-8">
                <asp:TextBox ID="txtaddress" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_90" MaxLength="250" TextMode="MultiLine"></asp:TextBox>
              
                       
            </div>
	
</div>
                                   <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Town </label>
           <div class="col-lg-8">
                <asp:TextBox ID="txtTown" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_90" MaxLength="250"></asp:TextBox>
              
                       
            </div>
	
</div>
                                   <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">State </label>
           <div class="col-lg-8">
                <asp:TextBox ID="txtState" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_90" MaxLength="250"></asp:TextBox>
              
                       
            </div>
	
</div>
                                   <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Zipcode </label>
           <div class="col-lg-8">
                <asp:TextBox ID="txtZipcode" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_90" MaxLength="250"></asp:TextBox>
             
                       
            </div>
	
</div>
                                   <div class="row fs-2">
                        
                                  <label class="col-lg-4 control-label"> </label>
           <div class="col-lg-8">
                          <asp:Button ID="btnSaveContact" runat="server" Text="Save & Add Card Details" SkinID="btnDefault" style="height:80px;width:250px;font-size:20px" OnClick="btnSaveContact_Click" />
                                        
                                        </div><br />
                            <br />
                        </div>

                                 </div>

                              <div class="row" id="pnlPaymentDetails" runat="server" visible="false">  
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
                   <asp:TextBox ID="txtCardConnectNumber" placeholder="Card Number" runat="server" style="display:none;visibility:hidden;"></asp:TextBox>
                 		<iframe id="tokenframe" name="tokenframe"  
                       src="https://boltgw.cardconnect.com/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D" 
                       frameborder="0" scrolling="no" width="300" height="55" runat="server" ></iframe>
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

                                    <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label"></label>
           <div class="col-lg-8">
                                   <asp:Button ID="btnProceed" runat="server" SkinID="btnDefault" Text="Pay Now" OnClick="btnProceed_Click" Height="80px" Width="120px"   />

               </div>
                                        </div>

                                 </div>


                              <div class="row" id="pnlResult" runat="server" visible="false">  
                                  <div class="row mb-10  text-center">


                                      </div>
                                  <div class="row mb-10 text-center">
														<p>
														<asp:Label ID="lblMsg" runat="server" Text="Thank you" Font-Size="22px" Font-Bold="true"></asp:Label></p>

														<p>
														<%--<asp:Label ID="Label3" runat="server" Text="Do want to be a member?" Font-Size="22px"></asp:Label></p>--%>

													</div>
													<div class="row mb-6  text-center">
														<div class ="col-lg-12  text-center">
															

															<asp:Button ID="btnBacktologin" runat="server" OnClick="btnBacktologin_Click" Text ="Back to Home" SkinID="btnDefault" />
														</div>
														</div>

                                  </div>
                             </div>

                   </div>
                   
                  
                </div>
                

            </div>
            <!--end::Card body-->
            <!--begin::Actions-->
          
            <!--end::Actions-->
            <input type="hidden"><div></div>
        </form>
        <!--end::Form-->
    </div>
    <!--end::Content-->
</div>

          
          

          

          </div>


                    </div>





<style>
    .header_right {
        text-align: right;
    }
</style>


<asp:HiddenField ID="hraised" runat="server" Value="30.00" ClientIDMode="Static" />

<asp:HiddenField ID="hremaing" runat="server" Value="40.00" ClientIDMode="Static" />
<script>
   /* dchart();*/

    //function dchart() {
    //    // init chart
    //    var element = document.getElementById("project_overview_chart");

    //    if (!element) {
    //        return;
    //    }
    //    const darray = [];
    //    darray[0] = $("#hraised").val();
    //    darray[1] = $("#hremaing").val();
    //    var config = {
    //        type: 'doughnut',
    //        data: {
    //            datasets: [{
    //                data: darray,
    //                backgroundColor: ['#00A3FF', '#E4E6EF']
    //            }],
    //            labels: ['Raised amount', 'Remaining amount']
    //        },
    //        options: {
    //            chart: {
    //                fontFamily: 'inherit'
    //            },
    //            rotation: -90,
    //            circumference: 180,
    //            cutoutPercentage: 75,
    //            responsive: true,
    //            maintainAspectRatio: true,
    //            cutout: '75%',
    //            title: {
    //                display: false
    //            },
    //            animation: {
    //                animateScale: true,
    //                animateRotate: true
    //            },
    //            tooltips: {
    //                enabled: true,
    //                intersect: false,
    //                mode: 'nearest',
    //                bodySpacing: 5,
    //                yPadding: 10,
    //                xPadding: 10,
    //                caretPadding: 0,
    //                displayColors: false,
    //                backgroundColor: '#20D489',
    //                titleFontColor: '#ffffff',
    //                cornerRadius: 4,
    //                footerSpacing: 0,
    //                titleSpacing: 0
    //            },
    //            plugins: {
    //                legend: {
    //                    display: true,
    //                    position: 'bottom',
    //                    textStyle: { fontSize: 34 },
    //                    labels: {
    //                        generateLabels: (chart) => {
    //                            const datasets = chart.data.datasets;
    //                            return datasets[0].data.map((data, i) => ({
    //                                text: `${chart.data.labels[i]} ${data}`,
    //                                fillStyle: datasets[0].backgroundColor[i],
    //                            }))
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    };

    //    var ctx = element.getContext('2d');
    //    var myDoughnut = new Chart(ctx, config);
    //}
</script>
