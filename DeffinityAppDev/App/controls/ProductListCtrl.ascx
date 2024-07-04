<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductListCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.ProductListCtrl" %>
<asp:ListView ID="ListFaithGiving" runat="server" OnItemCommand="ListFaithGiving_ItemCommand1">
														<LayoutTemplate>
        
			<div runat="server"  id="itemPlaceholder" class="row d-flex d-inline"></div>
			
			
															</LayoutTemplate>

														<ItemTemplate>

                                                            <div class="col-md-4 mb-6">
												<!--begin::Publications post-->
												<div class="card-xl-stretch me-md-6">
													<!--begin::Overlay-->
													<a class="d-block overlay mb-4" data-fslightbox="lightbox-hot-sales" href="#" >
														<!--begin::Image-->
														<%--<div class="overlay-wrapper bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-250px" style="background-image:url('<%# GetImageUrl(Eval("unid").ToString()) %>')"></div>--%>
														<div class="row">
															<img src='<%# GetImageUrl(Eval("unid").ToString()) %>' alt="Img" class="img-fluid" />

														</div>
														<!--end::Image-->
														<!--begin::Action-->
														<div class="overlay-layer bg-dark card-rounded bg-opacity-25">
															<i class="bi bi-eye-fill fs-2x text-white"></i>
														</div>
														<!--end::Action-->
													</a>
													
													<div class="m-0 p-2">
														<!--begin::Title-->
														<a href="#" class="fs-4 text-dark fw-bolder text-hover-primary text-dark lh-base"> <%# Eval("Title") %> </a>
													
														<div class="fw-bold fs-5 text-gray-600 text-dark mt-3 mb-5 overflow-scroll" style="min-height:125px;max-height:125px"><%# Eval("Description") %></div>
													
													</div>
													<!--end::Body-->
												</div>
																 <div class="card-footer d-flex justify-content-between">
																	 <a href="#" class="btn btn-flex btn-primary px-6">
    
    <span class="d-flex flex-column align-items-start">
        <span class="fs-7 fw-bold">Price</span>
        <span class="fs-3"><%# Eval("ProductPriceDisplay") %></span>
    </span>
</a>
																	 <asp:LinkButton ID="linkPrice" runat="server" Text='<%# Eval("ProductPrice","{0:C2}") %>' Visible="false" ></asp:LinkButton>
        <asp:Button style="float:right;" ID="Button1" runat="server" CssClass="btn btn-dark" CommandName="amount" Text="View Details"  CommandArgument='<%# Eval("ID") %>' CausesValidation="false"  />
    </div>
											
											</div>






														</ItemTemplate>
													</asp:ListView>

																
	<%--<script type="text/javascript">
        window.addEventListener('message', function (event) {
            var token = JSON.parse(event.data);
            var mytoken = document.getElementById('mytoken');
            mytoken.value = token.message;
            var txtCardConnectNumber = document.getElementById("<%=txtCardConnectNumber.ClientID%>");
            txtCardConnectNumber.value = token.message;
           // console.log(txtCardConnectNumber.value);
        }, false);

    </script>--%>

																<style>
																	.mdlshow{
																		background-color: white; height: 450px; width: 1180px; position: fixed; z-index: 10002; left: 361.5px; top: 15.5px;
																	}
																</style>
																<script type="text/javascript">
															

                                                                    //$(document).ready(function () {
                                                                    //    $('#FaithGivingListCtrl_pnlAddReligion').css('style');
                                                                    //});
                                                                </script>


																<%--</ContentTemplate>
																</asp:UpdatePanel>--%>