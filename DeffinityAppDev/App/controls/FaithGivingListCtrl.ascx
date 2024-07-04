<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FaithGivingListCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.FaithGivingListCtrl" %>

<%-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>--%>
<asp:UpdatePanel ID="upTithing" runat="server" UpdateMode="Conditional" class="row d-flex d-inline">
															<ContentTemplate>
<asp:ListView ID="ListFaithGiving" runat="server" OnItemCommand="ListFaithGiving_ItemCommand1">
														<LayoutTemplate>
        
			<div runat="server"  id="itemPlaceholder" class="row d-flex d-inline"></div>
			
			
															</LayoutTemplate>

														<ItemTemplate>

                                                            <div class="col-md-4 mb-6">
												<!--begin::Publications post-->
												<div class="card-xl-stretch me-md-6">
													<!--begin::Overlay-->
													<asp:LinkButton ID="link_img" runat="server"  CommandName="amount" CommandArgument='<%# Eval("ID") %>' CssClass="d-block overlay mb-4" data-fslightbox="lightbox-hot-sales"  >
														<!--begin::Image-->
														<div class="overlay-wrapper bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-175px" style="background-image:url('<%# GetImageUrl(Eval("ID").ToString()) %>')"></div>
														<!--end::Image-->
														<!--begin::Action-->
														<div class="overlay-layer bg-dark card-rounded bg-opacity-25">
															<i class="bi bi-eye-fill fs-2x text-white"></i>
														</div>
														<!--end::Action-->
													</asp:LinkButton>
													
													<div class="m-0 p-2">
														<!--begin::Title-->
														<asp:LinkButton ID="link_des" runat="server"  CommandName="amount" CommandArgument='<%# Eval("ID") %>'  CssClass="fs-4 text-dark fw-bolder text-hover-primary text-dark lh-base"> <%# Eval("Title") %> </asp:LinkButton>
													
														<div class="fw-bold fs-5 text-gray-600 text-dark mt-3 mb-5" style="min-height:125px;max-height:125px"><%# Eval("Description") %></div>
													
													</div>
													<!--end::Body-->
												</div>
																 <div class="card-footer">
        <asp:Button style="float:right;" ID="Button1" runat="server" CssClass="btn btn-dark" CommandName="amount" Text="Find out more"  CommandArgument='<%# Eval("ID") %>'  />
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


																</ContentTemplate>
																</asp:UpdatePanel>