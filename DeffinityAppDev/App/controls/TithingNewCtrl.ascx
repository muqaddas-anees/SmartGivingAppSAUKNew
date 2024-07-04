<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TithingNewCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.TithingNewCtrl" %>
	<asp:UpdatePanel ID="upTithing" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
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
<div class="card card-bordered">
	<div class="card-header align-items-center border-0 mt-0">
												<h3 class="card-title">
													<span class="fw-bolder mb-2 text-dark"> <asp:Literal ID="lblheader" runat="server" Text="How much would you like to donate?"></asp:Literal> </span>
												<asp:HiddenField ID="hunid" runat="server" />	<%--<span class="text-muted fw-bold fs-7">Count <asp:Literal ID="lblCount" runat="server"></asp:Literal>--%>
												</h3>

		</div>
													
													<div class="card-body" style="min-height:460px;overflow-x:scroll;">

													
																<asp:Panel ID="pnlAmount" runat="server" Visible="true">

																	
																		<div class="row mb-6">
												<div class="col-lg-2"></div>
												<!--begin::Col-->
												<div class="col-lg-10 fv-row fv-plugins-icon-container" style="height:320px;overflow-x:scroll">

													<asp:ListView ID="listamount" runat="server" OnItemCommand="listamount_ItemCommand">
														<LayoutTemplate>
        <div class="row" >
			<div runat="server"  id="itemPlaceholder"></div>
			</div>
			
															</LayoutTemplate>

														<ItemTemplate>
															<asp:Button ID="btnAmount" runat="server" CssClass="btn btn-icon btn-dark" CommandName="amount" Text="<%# Container.DataItem %>"  CommandArgument="<%# Container.DataItem %>" style="height:70px;width:80px;margin:15px" />
														</ItemTemplate>
													</asp:ListView>

												</div>
												    <div class="col-lg-2"></div>
                                                   
												<!--end::Col-->
											</div>


											<div class="row mb-6">
												<div class="col-lg-2"></div>
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label  fw-bold fs-6">Other Amount</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-5">
													<asp:TextBox ID="txtOtherAmount" runat="server" SkinID="Price" Text="0.00"></asp:TextBox>
													
												</div>
												<!--end::Col-->
											</div>

																	
																</asp:Panel>
																	<asp:Panel ID="pnlIsRecurring" runat="server" Visible="false">

																			

 

																		<div class="row mb-6">
																			<div class="col-lg-12">
																				<asp:Button ID="btnIsRecurring" OnClick="btnIsRecurring_click" runat="server" CssClass="btn btn-dark" Text="Yes I'd like to set up a recurring plan" style="height:70px;width:90%;margin:15px" />
																				</div>
																			</div>

																			<div class="row mb-6">
																			<div class="col-lg-12">
																				<asp:Button ID="btnNoRecurring" OnClick="btnNoRecurring_click" runat="server" CssClass="btn btn-dark" Text="No, This is just a one-off payment" style="height:70px;width:90%;margin:15px" />
																				</div>
																			</div>
	 

																</asp:Panel>

																<asp:Panel ID="pnlRecurring" runat="server" Visible="false">

																	
																	<div class="row mb-6">
																			<div class="col-lg-12"  style="display:none;visibility:hidden;">
																				<asp:Button ID="btnWeekly" OnClick="btnWeely_click" runat="server" CssClass="btn btn-dark" Text="Weekly" style="height:70px;width:90%;margin:15px" />
																				</div>
																			</div>

																			<div class="row mb-6">
																			<div class="col-lg-12">
																				<asp:Button ID="btnMonthly" OnClick="btnMontly_click" runat="server" CssClass="btn btn-dark" Text="Monthly" style="height:70px;width:90%;margin:15px" />
																				</div>
																			</div>

																		<div class="row mb-6">
																			<div class="col-lg-12 mb-5">
																				Date You Would Like Us To Take The Payment
																				</div>
																			<div class="col-lg-12">
																				<asp:TextBox ID="txtRecurringDate" runat="server" SkinID="DateNew"></asp:TextBox>
																				</div>
																			</div>


																</asp:Panel>

																<asp:Panel ID="pnlCardDetails" runat="server" Visible="false">

																	
																	<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-3 col-form-label  fw-bold fs-6"><%: Resources.DeffinityRes.CardType %></label>
												<!--end::Label-->
												<!--begin::Col-->
												
														<!--begin::Col-->
														<div class="col-lg-7 fv-row fv-plugins-icon-container">
															<asp:HiddenField ID="hRecurring" runat="server" Value="" />

															<asp:DropDownList  ID="ddlCurrencyCard"    runat="server">
                                                        <asp:ListItem Value="VISA">VISA</asp:ListItem>
                                                        <asp:ListItem  Value="MASTER CARD">MASTER CARD</asp:ListItem>
                                                        <asp:ListItem  value="DISCOVER" >DISCOVER</asp:ListItem>
                                                    </asp:DropDownList>
														
														
												</div>
											</div>

											

											
										<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-3 col-form-label  fw-bold fs-6"><%: Resources.DeffinityRes.NameonCard %></label>
												<!--end::Label-->
												<!--begin::Col-->
												
														<!--begin::Col-->
														<div class="col-lg-7 fv-row fv-plugins-icon-container">
															
														<asp:TextBox ID="txtCardName" placeholder="Name on Card"   runat="server" ></asp:TextBox>
														  
												</div>
											</div>

											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-3 col-form-label  fw-bold fs-6">Card Number</label>
												<!--end::Label-->
												<!--begin::Col-->
												
														<!--begin::Col-->
														<div class="col-lg-7 fv-row fv-plugins-icon-container">
															
														<asp:TextBox ID="txtCardConnectNumber" placeholder="Card Number" runat="server" style="display:none;visibility:hidden;"></asp:TextBox>
															<iframe id="tokenframe" name="tokenframe"  
                       src="https://boltgw.cardconnect.com/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D" 
                       frameborder="0" scrolling="no" width="300" height="55" runat="server"></iframe>
                <asp:HiddenField ID="mytoken" runat="server" ClientIDMode="Static" />
														
												</div>
											</div>
											<div class="row mb-6">
												<label class="col-lg-3 col-form-label  fw-bold fs-6"></label>
												<%--<label class="col-lg-2 col-form-label required fw-bold fs-6">Campaign Owner</label>--%>
												<!--begin::Col-->
												<div class="col-lg-9">
													<div class="row">
														<!--begin::Col-->
														<%--<label class="col-lg-5 col-form-label  fw-bold fs-6"></label>--%>
														<div class="col-lg-6 fv-row fv-plugins-icon-container">

															<label class=" col-form-label  fw-bold fs-6">Month</label>
															<%--<asp:TextBox ID="txtStartDate" TextMode="Date"  runat="server"></asp:TextBox>--%>
															 <asp:DropDownList ID="ddlMonth" runat="server" CssClass="paymentinfo-text"  >
                        </asp:DropDownList>
													

														</div>
														<!--end::Col-->
														
														<!--begin::Col-->
														<%--<label class="col-lg-1 col-form-label  fw-bold fs-6"></label>--%>
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<label class="col-form-label  fw-bold fs-6">Year</label>
															<%--<asp:TextBox ID="TextExpiryDate"  TextMode="Date" runat="server"></asp:TextBox>--%>
															  <asp:DropDownList ID="ddlYear" runat="server" CssClass="paymentinfo-text"  >
                        </asp:DropDownList>
														
														<!--end::Col-->
													</div>
												</div>
												<!--end::Col-->
											</div>




											<div class="row mb-6 mt-6">
												<!--begin::Label-->
												<label class="col-lg-3 col-form-label  fw-bold fs-6">CVV</label>
												<!--end::Label-->
												<!--begin::Col-->
												
														<!--begin::Col-->
														<div class="col-lg-4 fv-row fv-plugins-icon-container">
															
														<asp:TextBox ID="TxtCSV"  placeholder="CVV"    runat="server" TextMode="Password" MaxLength="6"></asp:TextBox>
														
												</div>
											</div>
																 

																</asp:Panel>


																
																
															
													
														
													</div>

	 <div class="card-footer">
		  <asp:Button ID="btnBack" runat="server" SkinID="btnDefault" Text="Back" OnClick="btnBack_Click" style="float:left;" />
                <asp:Button ID="btnNext" runat="server" SkinID="btnDefault" Text="Next" OnClick="btnNext_Click" style="float:right;" />
            </div>

	</div>

																</ContentTemplate>

														</asp:UpdatePanel>