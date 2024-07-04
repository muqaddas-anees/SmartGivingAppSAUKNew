<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="FaithGivingDetails.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="DeffinityAppDev.App.FaithGivingDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
   Fundraisers
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
	
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

    <div class="card mb-5 mb-xl-10">
    <div class="card-header">
        <h3 class="card-body"> <asp:Literal ID="lblTittle" runat="server"></asp:Literal>  </h3>
        <div class="card-toolbar">
            <%--<button type="button" class="btn btn-sm btn-light">
                Action
            </button>--%>
        </div>
    </div>
    <div class="card-body">
		<div class="d-flex flex-row-auto flex-center mb-6">
       <asp:Image ID="imgBanner" CssClass="img-fluid mx-auto d-block" runat="server" style="min-height:250px"   />
			</div>
		<P> <asp:Literal ID="lblDescription" runat="server"></asp:Literal> </P>
    </div>
   <%-- <div class="card-footer">
        <asp:Button style="float:right;" ID="btnAmount" runat="server" CssClass="btn btn-dark" CommandName="amount" Text="Find out more"  CommandArgument="<%# Container.DataItem %>"  />
    </div>--%>
</div>

	 <div class="card mb-5 mb-xl-10">


	<div style="width: 100%; height:50%; ">
        <div style="width: 50%; float: left;">
            	 <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">  How Would You Like To Help Today? </h3>
									</div>
									 <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Click to add a user">


										 </div>
									<!--end::Card title-->
								</div>
								<!--begin::Card header-->
								<!--begin::Content-->
								<div  class="collapse show" style="">
									<!--begin::Form-->
									<form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
										<!--begin::Card body-->
										<div class="card-body border-top p-9">

												<div class="row mb-6">
												<div class="col-lg-2"></div>
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">

													<asp:ListView ID="listamount" runat="server" OnItemCommand="listamount_ItemCommand">
														<LayoutTemplate>
        <div class="row" >
			<div runat="server"  id="itemPlaceholder"></div>
			</div>
			
															</LayoutTemplate>

														<ItemTemplate>
															<asp:Button ID="btnAmount" runat="server" CssClass="btn btn-icon btn-dark" CommandName="amount" Text="<%# Container.DataItem %>"  CommandArgument="<%# Container.DataItem %>" style="height:70px;width:100px;margin:15px" />
														</ItemTemplate>
													</asp:ListView>

													
                                                   <%-- <asp:CheckBoxList ID="ckbCurrencyList" CssClass="BigCheckBox" runat="server" CellPadding="10" CellSpacing="4" Height="2px" Width="200px" BorderColor="#F5F8FA" BorderStyle="None" ForeColor="#9933FF" RepeatLayout="Table" RepeatDirection="Vertical" RepeatColumns="3"
                                                         AutoPostBack="True" Enabled="false" Visible="false">
                                                        <asp:ListItem Value="10" Text="10"><h3>10.00</h3>   </asp:ListItem>
                                                        <asp:ListItem Value="250" Text="250"> <h3>  250.00</h3> </asp:ListItem>
                                                        <asp:ListItem Value="20" Text="20"><h3>   20.00</h3> </asp:ListItem>
                                                        <asp:ListItem Value="300" Text="300"> <h3>  300.00</h3> </asp:ListItem>
                                                        <asp:ListItem Value="50" Text="50"><h3>  50.00</h3> </asp:ListItem>
                                                        <asp:ListItem Value="500" Text="500"><h3>500.00</h3> </asp:ListItem>
                                                        <asp:ListItem Value="100" Text="100"> <h3>  100.00</h3> </asp:ListItem>
                                                        <asp:ListItem Value="1000" Text="1000"> <h3> 1000.00 </h3></asp:ListItem>
                                                        <asp:ListItem Value="150" Text="150"> <h3> 150.00 </h3></asp:ListItem>
                                                        <asp:ListItem Value="1500" Text="1500"> <h3>  1500.00 </h3></asp:ListItem>
                                                        <asp:ListItem Value="200" Text="200"> <h3> 200.00 </h3></asp:ListItem>
                                                        <asp:ListItem Value="2000" Text="2000"> <h3> 2000.00 </h3></asp:ListItem>
                                                       <asp:ListItem  Value="Other Amount" >Other Amount</asp:ListItem>
                                                    </asp:CheckBoxList>--%>

												    <br />


												

												</div>
												    <div class="col-lg-2"></div>
                                                    <br />
												    <br />
												<!--end::Col-->
											</div>


											<div class="row mb-6">
												<div class="col-lg-2"></div>
												<!--begin::Label-->
												<label class="col-lg-2 col-form-label  fw-bold fs-6">Other Amount</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">

															<asp:TextBox ID="txtOtherAmount" runat="server" SkinID="Price" Text="0.00"></asp:TextBox>
															<%--<asp:CheckBoxList ID="CheckBoxOtherAmount" CssClass="BigCheckBox" runat="server"  BorderColor="#F5F8FA" BorderStyle="None" ForeColor="#9933FF" RepeatLayout="Table" 
                                                         AutoPostBack="True" Enabled="false">


																<asp:ListItem  Value="Other Amount" ><h3>Other Amount</h3></asp:ListItem>

																</asp:CheckBoxList>--%>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
														<%--	<asp:TextBox ID="TextBoxOtherAmount" runat="server"  ToolTip="Enter the Amount" ReadOnly="true" TextMode="Number"></asp:TextBox>--%>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
													</div>
												</div>
												<!--end::Col-->
											</div>

											<br />
											<br />
										
										
										</div>
										<!--end::Card body-->
										<!--begin::Actions-->


										<%--<div class="card-footer d-flex justify-content-end py-6 px-9">
											
											<asp:Button ID="Button1" runat="server" SkinID="btnDefault"  Text="Save and Edit Later" OnClick="btnSaveAndEdit_Click"  />   <div class ="col-lg-1"></div>
											<asp:Button ID="Button2" runat="server" SkinID="btnDefault"  Text="Publish" OnClick="btnPublish_Click"  />  <div class ="col-lg-1"></div>

											

										</div>--%>
									
								</div>
								<!--end::Content-->
							</div>
        </div>

        <div style="width: 50%;  float: right;">
            	<!--begin::Input group-->
											  <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"> </h3>


									</div>
									 <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Click to add a user">


										 </div>
									<!--end::Card title-->
								</div>
								<!--begin::Card header-->
								<!--begin::Content-->
								<div  class="collapse show" style="">
									<!--begin::Form-->
									<form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
										<!--begin::Card body-->
										<div class="card-body border-top p-9">
											



											<!--begin::Input group-->
											<div class="d-flex flex-row-auto flex-center ">
												 
												 
											 	<i class="fa fa-cc-visa p-5" style="font-size:48px;color:blue" ></i> <i class="fa fa-cc-mastercard p-5" style="font-size:48px;color:blue"></i>  <i class="fa fa-cc-discover p-5" style="font-size:48px;color:blue"></i> <i class="fa fa-cc-diners-club p-5" style="font-size:48px;color:blue"></i>
											 	

												
											</div>
											<!--end::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-3 col-form-label  fw-bold fs-6"><%: Resources.DeffinityRes.CardType %></label>
												<!--end::Label-->
												<!--begin::Col-->
												
														<!--begin::Col-->
														<div class="col-lg-7 fv-row fv-plugins-icon-container">
															<asp:DropDownList  ID="ddlCurrencyCard"    runat="server">
                                                        <asp:ListItem Value="VISA">VISA</asp:ListItem>
                                                        <asp:ListItem  Value="MASTER CARD">MASTER CARD</asp:ListItem>
                                                        <asp:ListItem  value="DISCOVER" >DISCOVER</asp:ListItem>
                                                    </asp:DropDownList>
														
														
												</div>
											</div>

											<br />

											
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


											<br />

										

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
												<div class="col-lg-7">
													<div class="row">
														<!--begin::Col-->
														<%--<label class="col-lg-5 col-form-label  fw-bold fs-6"></label>--%>
														<div class="col-lg-5 fv-row fv-plugins-icon-container">

															<label class=" col-form-label  fw-bold fs-5">Month</label>
															<%--<asp:TextBox ID="txtStartDate" TextMode="Date"  runat="server"></asp:TextBox>--%>
															 <asp:DropDownList ID="ddlMonth" runat="server" CssClass="paymentinfo-text" >
                        </asp:DropDownList>
														<div class="fv-plugins-message-container invalid-feedback"></div>

														</div>
														<!--end::Col-->
														
														<!--begin::Col-->
														<%--<label class="col-lg-1 col-form-label  fw-bold fs-6"></label>--%>
														<div class="col-lg-5 fv-row fv-plugins-icon-container">
															<label class="col-form-label  fw-bold fs-6">Year</label>
															<%--<asp:TextBox ID="TextExpiryDate"  TextMode="Date" runat="server"></asp:TextBox>--%>
															  <asp:DropDownList ID="ddlYear" runat="server" CssClass="paymentinfo-text"  >
                        </asp:DropDownList>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
													</div>
												</div>
												<!--end::Col-->
											</div>



											
											

											<!--begin::C S V-->
											<br />


											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-3 col-form-label  fw-bold fs-6">CVV</label>
												<!--end::Label-->
												<!--begin::Col-->
												
														<!--begin::Col-->
														<div class="col-lg-4 fv-row fv-plugins-icon-container">
															
														<asp:TextBox ID="TxtCSV"  placeholder="CVV"    runat="server" TextMode="Password"></asp:TextBox>
														
												</div>
											</div>
										
										</div>

										</div>



										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<%--<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

											<asp:Button ID="btnSaveAndEdit" runat="server" SkinID="btnDefault"  Text="Process Payment" OnClick="btnSaveAndEdit_Click"  />   <%--<div class ="col-lg-3"></div>--%>
											


											


										</div>
										<!--end::Actions-->
									
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>




        </div>

	   </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
