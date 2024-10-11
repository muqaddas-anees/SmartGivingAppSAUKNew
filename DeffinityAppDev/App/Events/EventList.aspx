<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="EventList.aspx.cs" Inherits="DeffinityAppDev.App.EventList" %>


<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
    Events
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
        function copyUrlToClipboard(url) {
            // Create a temporary input element
            var tempInput = document.createElement("input");

            // Set its value to the URL
            tempInput.setAttribute("value", url);

            // Append the input element to the document body
            document.body.appendChild(tempInput);

            // Select the input element's contents
            tempInput.select();

            // Copy the selected text to the clipboard
            document.execCommand("copy");

            // Remove the temporary input element
            document.body.removeChild(tempInput);

            showswal("URL Copied successfully", "Ok");
            // Prevent postback
            return false;
        }
   
    </script>
	<script type="text/javascript">
        function CopyToClipboard(myID) {
            var copyText = document.getElementById(myID);

            /* Select the text field */
            copyText.select();
            copyText.setSelectionRange(0, 99999); /* For mobile devices */

            /* Copy the text inside the text field */
            document.execCommand("copy");
        }
    </script>
	 <div class="row" >
    <!--end::Toolbar-->
                    <!--begin::Post-->
                    <div class="post d-flex flex-column-fluid" id="kt_post1">
                        <!--begin::Container-->
                        <div id="kt_content_container1" class="container-xxl">
		<div class="row" style="float:right;">
			<div class="col-sm-6">
			 </div>
				<div class="col-sm-6">
			<asp:DropDownList ID="ddlSelect" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged" SkinID="ddl_225px">
			<asp:ListItem Text="All events" Value="all"></asp:ListItem> 
			<asp:ListItem Text="Upcoming events" Value="up" Selected="True"></asp:ListItem> 
			<asp:ListItem Text="Past events" Value="past"></asp:ListItem>
			
		                      </asp:DropDownList>
				</div>
			</div>
							</div>
						</div>
			

		</div>
<div class="content d-flex flex-column flex-column-fluid" id="pnlNodata" runat="server" visible="false">
						<!--begin::Container-->
						<div class="container-xxl" id="kt_content_container">
							<!--begin::Card-->
							<div class="card">
								<!--begin::Card body-->
								<div class="card-body p-0">
									<!--begin::Wrapper-->
									<div class="card-px text-center py-20 my-10">
										<!--begin::Title-->
										<h2 class="fs-2x fw-bolder mb-10">Let’s get you set up for your next event!</h2>
										<!--end::Title-->
										<!--begin::Description-->
										<p class="text-gray-400 fs-4 fw-bold mb-10">Click the button to configure your next event. It’s best to have all the information ready such as the event description, speaker profiles, and types of tickets you wish to offer.</p>
										<!--end::Description-->
										<!--begin::Action-->
										<asp:HyperLink ID="btnBuilder" runat="server" CssClass="btn btn-primary" Text="Let’s Set Up Your Event" NavigateUrl="~/App/Events/BasicInfo.aspx"  ></asp:HyperLink>
										 <a class="btn btn-video" style="background-color:#50CD89;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo" href="#vimeo">
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                  <iframe id="vimeo" style="display:none" src="https://player.vimeo.com/video/782689107?h=12be0c52d8" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>

									</div>
									<!--end::Wrapper-->
									<!--begin::Illustration-->
									<div class="text-center px-4">
										<input type="text" value="" id="myInput" runat="server" style="visibility:hidden;">
										<img class="mw-100 mh-300px" alt="" src="../../assets/media/illustrations/dozzy-1/2.png" />
									</div>
									<!--end::Illustration-->
								</div>
								<!--end::Card body-->
							</div>
							<!--end::Card-->
							<!--begin::Modals-->
							<!--begin::Modal - Customers - Add-->
							<div class="modal fade" id="kt_modal_add_customer" tabindex="-1" aria-hidden="true">
								<!--begin::Modal dialog-->
								<div class="modal-dialog modal-dialog-centered mw-650px">
									<!--begin::Modal content-->
									<div class="modal-content">
										<!--begin::Form-->
										<form class="form" action="#" id="kt_modal_add_customer_form" data-kt-redirect="../../demo4/dist/apps/customers/list.html">
											<!--begin::Modal header-->
											<div class="modal-header" id="kt_modal_add_customer_header">
												<!--begin::Modal title-->
												<h2 class="fw-bolder">Add a Customer</h2>
												<!--end::Modal title-->
												<!--begin::Close-->
												<div id="kt_modal_add_customer_close" class="btn btn-icon btn-sm btn-active-icon-primary">
													<!--begin::Svg Icon | path: icons/duotune/arrows/arr061.svg-->
													<span class="svg-icon svg-icon-1">
														<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
															<rect opacity="0.5" x="6" y="17.3137" width="16" height="2" rx="1" transform="rotate(-45 6 17.3137)" fill="black" />
															<rect x="7.41422" y="6" width="16" height="2" rx="1" transform="rotate(45 7.41422 6)" fill="black" />
														</svg>
													</span>
													<!--end::Svg Icon-->
												</div>
												<!--end::Close-->
											</div>
											<!--end::Modal header-->
											<!--begin::Modal body-->
											<div class="modal-body py-10 px-lg-17">
												<!--begin::Scroll-->
												<div class="scroll-y me-n7 pe-7" id="kt_modal_add_customer_scroll" data-kt-scroll="true" data-kt-scroll-activate="{default: false, lg: true}" data-kt-scroll-max-height="auto" data-kt-scroll-dependencies="#kt_modal_add_customer_header" data-kt-scroll-wrappers="#kt_modal_add_customer_scroll" data-kt-scroll-offset="300px">
													<!--begin::Input group-->
													<div class="fv-row mb-7">
														<!--begin::Label-->
														<label class="required fs-6 fw-bold mb-2">Name</label>
														<!--end::Label-->
														<!--begin::Input-->
														<input type="text" class="form-control form-control-solid" placeholder="" name="name" value="Sean Bean" />
														<!--end::Input-->
													</div>
													<!--end::Input group-->
													<!--begin::Input group-->
													<div class="fv-row mb-7">
														<!--begin::Label-->
														<label class="fs-6 fw-bold mb-2">
															<span class="required">Email</span>
															<i class="fas fa-exclamation-circle ms-1 fs-7" data-bs-toggle="tooltip" title="Email address must be active"></i>
														</label>
														<!--end::Label-->
														<!--begin::Input-->
														<input type="email" class="form-control form-control-solid" placeholder="" name="email" value="sean@dellito.com" />
														<!--end::Input-->
													</div>
													<!--end::Input group-->
													<!--begin::Input group-->
													<div class="fv-row mb-15">
														<!--begin::Label-->
														<label class="fs-6 fw-bold mb-2">Description</label>
														<!--end::Label-->
														<!--begin::Input-->
														<input type="text" class="form-control form-control-solid" placeholder="" name="description" />
														<!--end::Input-->
													</div>
													<!--end::Input group-->
													<!--begin::Billing toggle-->
													<div class="fw-bolder fs-3 rotate collapsible mb-7" data-bs-toggle="collapse" href="#kt_modal_add_customer_billing_info" role="button" aria-expanded="false" aria-controls="kt_customer_view_details">Shipping Information
													<span class="ms-2 rotate-180">
														<!--begin::Svg Icon | path: icons/duotune/arrows/arr072.svg-->
														<span class="svg-icon svg-icon-3">
															<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																<path d="M11.4343 12.7344L7.25 8.55005C6.83579 8.13583 6.16421 8.13584 5.75 8.55005C5.33579 8.96426 5.33579 9.63583 5.75 10.05L11.2929 15.5929C11.6834 15.9835 12.3166 15.9835 12.7071 15.5929L18.25 10.05C18.6642 9.63584 18.6642 8.96426 18.25 8.55005C17.8358 8.13584 17.1642 8.13584 16.75 8.55005L12.5657 12.7344C12.2533 13.0468 11.7467 13.0468 11.4343 12.7344Z" fill="black" />
															</svg>
														</span>
														<!--end::Svg Icon-->
													</span></div>
													<!--end::Billing toggle-->
													<!--begin::Billing form-->
													<div id="kt_modal_add_customer_billing_info" class="collapse show">
														<!--begin::Input group-->
														<div class="d-flex flex-column mb-7 fv-row">
															<!--begin::Label-->
															<label class="required fs-6 fw-bold mb-2">Address Line 1</label>
															<!--end::Label-->
															<!--begin::Input-->
															<input class="form-control form-control-solid" placeholder="" name="address1" value="101, Collins Street" />
															<!--end::Input-->
														</div>
														<!--end::Input group-->
														<!--begin::Input group-->
														<div class="d-flex flex-column mb-7 fv-row">
															<!--begin::Label-->
															<label class="fs-6 fw-bold mb-2">Address Line 2</label>
															<!--end::Label-->
															<!--begin::Input-->
															<input class="form-control form-control-solid" placeholder="" name="address2" value="" />
															<!--end::Input-->
														</div>
														<!--end::Input group-->
														<!--begin::Input group-->
														<div class="d-flex flex-column mb-7 fv-row">
															<!--begin::Label-->
															<label class="required fs-6 fw-bold mb-2">Town</label>
															<!--end::Label-->
															<!--begin::Input-->
															<input class="form-control form-control-solid" placeholder="" name="city" value="Melbourne" />
															<!--end::Input-->
														</div>
														<!--end::Input group-->
														<!--begin::Input group-->
														<div class="row g-9 mb-7">
															<!--begin::Col-->
															<div class="col-md-6 fv-row">
																<!--begin::Label-->
																<label class="required fs-6 fw-bold mb-2">State / Province</label>
																<!--end::Label-->
																<!--begin::Input-->
																<input class="form-control form-control-solid" placeholder="" name="state" value="Victoria" />
																<!--end::Input-->
															</div>
															<!--end::Col-->
															<!--begin::Col-->
															<div class="col-md-6 fv-row">
																<!--begin::Label-->
																<label class="required fs-6 fw-bold mb-2">Post Code</label>
																<!--end::Label-->
																<!--begin::Input-->
																<input class="form-control form-control-solid" placeholder="" name="postcode" value="3000" />
																<!--end::Input-->
															</div>
															<!--end::Col-->
														</div>
														<!--end::Input group-->
														<!--begin::Input group-->
														<div class="d-flex flex-column mb-7 fv-row">
															<!--begin::Label-->
															<label class="fs-6 fw-bold mb-2">
																<span class="required">Country</span>
																<i class="fas fa-exclamation-circle ms-1 fs-7" data-bs-toggle="tooltip" title="Country of origination"></i>
															</label>
															<!--end::Label-->
															<!--begin::Input-->
															<select name="country" aria-label="Select a Country" data-control="select2" data-placeholder="Select a Country..." data-dropdown-parent="#kt_modal_add_customer" class="form-select form-select-solid fw-bolder">
																<option value="">Select a Country...</option>
																<option value="AF">Afghanistan</option>
																<option value="AX">Aland Islands</option>
																<option value="AL">Albania</option>
																<option value="DZ">Algeria</option>
																<option value="AS">American Samoa</option>
																<option value="AD">Andorra</option>
																<option value="AO">Angola</option>
																<option value="AI">Anguilla</option>
																<option value="AG">Antigua and Barbuda</option>
																<option value="AR">Argentina</option>
																<option value="AM">Armenia</option>
																<option value="AW">Aruba</option>
																<option value="AU">Australia</option>
																<option value="AT">Austria</option>
																<option value="AZ">Azerbaijan</option>
																<option value="BS">Bahamas</option>
																<option value="BH">Bahrain</option>
																<option value="BD">Bangladesh</option>
																<option value="BB">Barbados</option>
																<option value="BY">Belarus</option>
																<option value="BE">Belgium</option>
																<option value="BZ">Belize</option>
																<option value="BJ">Benin</option>
																<option value="BM">Bermuda</option>
																<option value="BT">Bhutan</option>
																<option value="BO">Bolivia, Plurinational State of</option>
																<option value="BQ">Bonaire, Sint Eustatius and Saba</option>
																<option value="BA">Bosnia and Herzegovina</option>
																<option value="BW">Botswana</option>
																<option value="BR">Brazil</option>
																<option value="IO">British Indian Ocean Territory</option>
																<option value="BN">Brunei Darussalam</option>
																<option value="BG">Bulgaria</option>
																<option value="BF">Burkina Faso</option>
																<option value="BI">Burundi</option>
																<option value="KH">Cambodia</option>
																<option value="CM">Cameroon</option>
																<option value="CA">Canada</option>
																<option value="CV">Cape Verde</option>
																<option value="KY">Cayman Islands</option>
																<option value="CF">Central African Republic</option>
																<option value="TD">Chad</option>
																<option value="CL">Chile</option>
																<option value="CN">China</option>
																<option value="CX">Christmas Island</option>
																<option value="CC">Cocos (Keeling) Islands</option>
																<option value="CO">Colombia</option>
																<option value="KM">Comoros</option>
																<option value="CK">Cook Islands</option>
																<option value="CR">Costa Rica</option>
																<option value="CI">Côte d'Ivoire</option>
																<option value="HR">Croatia</option>
																<option value="CU">Cuba</option>
																<option value="CW">Curaçao</option>
																<option value="CZ">Czech Republic</option>
																<option value="DK">Denmark</option>
																<option value="DJ">Djibouti</option>
																<option value="DM">Dominica</option>
																<option value="DO">Dominican Republic</option>
																<option value="EC">Ecuador</option>
																<option value="EG">Egypt</option>
																<option value="SV">El Salvador</option>
																<option value="GQ">Equatorial Guinea</option>
																<option value="ER">Eritrea</option>
																<option value="EE">Estonia</option>
																<option value="ET">Ethiopia</option>
																<option value="FK">Falkland Islands (Malvinas)</option>
																<option value="FJ">Fiji</option>
																<option value="FI">Finland</option>
																<option value="FR">France</option>
																<option value="PF">French Polynesia</option>
																<option value="GA">Gabon</option>
																<option value="GM">Gambia</option>
																<option value="GE">Georgia</option>
																<option value="DE">Germany</option>
																<option value="GH">Ghana</option>
																<option value="GI">Gibraltar</option>
																<option value="GR">Greece</option>
																<option value="GL">Greenland</option>
																<option value="GD">Grenada</option>
																<option value="GU">Guam</option>
																<option value="GT">Guatemala</option>
																<option value="GG">Guernsey</option>
																<option value="GN">Guinea</option>
																<option value="GW">Guinea-Bissau</option>
																<option value="HT">Haiti</option>
																<option value="VA">Holy See (Vatican City State)</option>
																<option value="HN">Honduras</option>
																<option value="HK">Hong Kong</option>
																<option value="HU">Hungary</option>
																<option value="IS">Iceland</option>
																<option value="IN">India</option>
																<option value="ID">Indonesia</option>
																<option value="IR">Iran, Islamic Republic of</option>
																<option value="IQ">Iraq</option>
																<option value="IE">Ireland</option>
																<option value="IM">Isle of Man</option>
																<option value="IL">Israel</option>
																<option value="IT">Italy</option>
																<option value="JM">Jamaica</option>
																<option value="JP">Japan</option>
																<option value="JE">Jersey</option>
																<option value="JO">Jordan</option>
																<option value="KZ">Kazakhstan</option>
																<option value="KE">Kenya</option>
																<option value="KI">Kiribati</option>
																<option value="KP">Korea, Democratic People's Republic of</option>
																<option value="KW">Kuwait</option>
																<option value="KG">Kyrgyzstan</option>
																<option value="LA">Lao People's Democratic Republic</option>
																<option value="LV">Latvia</option>
																<option value="LB">Lebanon</option>
																<option value="LS">Lesotho</option>
																<option value="LR">Liberia</option>
																<option value="LY">Libya</option>
																<option value="LI">Liechtenstein</option>
																<option value="LT">Lithuania</option>
																<option value="LU">Luxembourg</option>
																<option value="MO">Macao</option>
																<option value="MG">Madagascar</option>
																<option value="MW">Malawi</option>
																<option value="MY">Malaysia</option>
																<option value="MV">Maldives</option>
																<option value="ML">Mali</option>
																<option value="MT">Malta</option>
																<option value="MH">Marshall Islands</option>
																<option value="MQ">Martinique</option>
																<option value="MR">Mauritania</option>
																<option value="MU">Mauritius</option>
																<option value="MX">Mexico</option>
																<option value="FM">Micronesia, Federated States of</option>
																<option value="MD">Moldova, Republic of</option>
																<option value="MC">Monaco</option>
																<option value="MN">Mongolia</option>
																<option value="ME">Montenegro</option>
																<option value="MS">Montserrat</option>
																<option value="MA">Morocco</option>
																<option value="MZ">Mozambique</option>
																<option value="MM">Myanmar</option>
																<option value="NA">Namibia</option>
																<option value="NR">Nauru</option>
																<option value="NP">Nepal</option>
																<option value="NL">Netherlands</option>
																<option value="NZ">New Zealand</option>
																<option value="NI">Nicaragua</option>
																<option value="NE">Niger</option>
																<option value="NG">Nigeria</option>
																<option value="NU">Niue</option>
																<option value="NF">Norfolk Island</option>
																<option value="MP">Northern Mariana Islands</option>
																<option value="NO">Norway</option>
																<option value="OM">Oman</option>
																<option value="PK">Pakistan</option>
																<option value="PW">Palau</option>
																<option value="PS">Palestinian Territory, Occupied</option>
																<option value="PA">Panama</option>
																<option value="PG">Papua New Guinea</option>
																<option value="PY">Paraguay</option>
																<option value="PE">Peru</option>
																<option value="PH">Philippines</option>
																<option value="PL">Poland</option>
																<option value="PT">Portugal</option>
																<option value="PR">Puerto Rico</option>
																<option value="QA">Qatar</option>
																<option value="RO">Romania</option>
																<option value="RU">Russian Federation</option>
																<option value="RW">Rwanda</option>
																<option value="BL">Saint Barthélemy</option>
																<option value="KN">Saint Kitts and Nevis</option>
																<option value="LC">Saint Lucia</option>
																<option value="MF">Saint Martin (French part)</option>
																<option value="PM">Saint Pierre and Miquelon</option>
																<option value="VC">Saint Vincent and the Grenadines</option>
																<option value="WS">Samoa</option>
																<option value="SM">San Marino</option>
																<option value="ST">Sao Tome and Principe</option>
																<option value="SA">Saudi Arabia</option>
																<option value="SN">Senegal</option>
																<option value="RS">Serbia</option>
																<option value="SC">Seychelles</option>
																<option value="SL">Sierra Leone</option>
																<option value="SG">Singapore</option>
																<option value="SX">Sint Maarten (Dutch part)</option>
																<option value="SK">Slovakia</option>
																<option value="SI">Slovenia</option>
																<option value="SB">Solomon Islands</option>
																<option value="SO">Somalia</option>
																<option value="ZA">South Africa</option>
																<option value="KR">South Korea</option>
																<option value="SS">South Sudan</option>
																<option value="ES">Spain</option>
																<option value="LK">Sri Lanka</option>
																<option value="SD">Sudan</option>
																<option value="SR">Suriname</option>
																<option value="SZ">Swaziland</option>
																<option value="SE">Sweden</option>
																<option value="CH">Switzerland</option>
																<option value="SY">Syrian Arab Republic</option>
																<option value="TW">Taiwan, Province of China</option>
																<option value="TJ">Tajikistan</option>
																<option value="TZ">Tanzania, United Republic of</option>
																<option value="TH">Thailand</option>
																<option value="TG">Togo</option>
																<option value="TK">Tokelau</option>
																<option value="TO">Tonga</option>
																<option value="TT">Trinidad and Tobago</option>
																<option value="TN">Tunisia</option>
																<option value="TR">Turkey</option>
																<option value="TM">Turkmenistan</option>
																<option value="TC">Turks and Caicos Islands</option>
																<option value="TV">Tuvalu</option>
																<option value="UG">Uganda</option>
																<option value="UA">Ukraine</option>
																<option value="AE">United Arab Emirates</option>
																<option value="GB">United Kingdom</option>
																<option value="US" selected="selected">United States</option>
																<option value="UY">Uruguay</option>
																<option value="UZ">Uzbekistan</option>
																<option value="VU">Vanuatu</option>
																<option value="VE">Venezuela, Bolivarian Republic of</option>
																<option value="VN">Vietnam</option>
																<option value="VI">Virgin Islands</option>
																<option value="YE">Yemen</option>
																<option value="ZM">Zambia</option>
																<option value="ZW">Zimbabwe</option>
															</select>
															<!--end::Input-->
														</div>
														<!--end::Input group-->
														<!--begin::Input group-->
														<div class="fv-row mb-7">
															<!--begin::Wrapper-->
															<div class="d-flex flex-stack">
																<!--begin::Label-->
																<div class="me-5">
																	<!--begin::Label-->
																	<label class="fs-6 fw-bold">Use as a billing adderess?</label>
																	<!--end::Label-->
																	<!--begin::Input-->
																	<div class="fs-7 fw-bold text-muted">If you need more info, please check budget planning</div>
																	<!--end::Input-->
																</div>
																<!--end::Label-->
																<!--begin::Switch-->
																<label class="form-check form-switch form-check-custom form-check-solid">
																	<!--begin::Input-->
																	<input class="form-check-input" name="billing" type="checkbox" value="1" id="kt_modal_add_customer_billing" checked="checked" />
																	<!--end::Input-->
																	<!--begin::Label-->
																	<span class="form-check-label fw-bold text-muted" for="kt_modal_add_customer_billing">Yes</span>
																	<!--end::Label-->
																</label>
																<!--end::Switch-->
															</div>
															<!--begin::Wrapper-->
														</div>
														<!--end::Input group-->
													</div>
													<!--end::Billing form-->
												</div>
												<!--end::Scroll-->
											</div>
											<!--end::Modal body-->
											<!--begin::Modal footer-->
											<div class="modal-footer flex-center">
												<!--begin::Button-->
												<button type="reset" id="kt_modal_add_customer_cancel" class="btn btn-light me-3">Discard</button>
												<!--end::Button-->
												<!--begin::Button-->
												<button type="submit" id="kt_modal_add_customer_submit" class="btn btn-primary">
													<span class="indicator-label">Submit</span>
													<span class="indicator-progress">Please wait...
													<span class="spinner-border spinner-border-sm align-middle ms-2"></span></span>
												</button>
												<!--end::Button-->
											</div>
											<!--end::Modal footer-->
										</form>
										<!--end::Form-->
									</div>
								</div>
							</div>
							<!--end::Modal - Customers - Add-->
							<!--end::Modals-->
						</div>
						<!--end::Container-->
					</div>


    <div class="content d-flex flex-column flex-column-fluid" id="kt_content" runat="server" >

         <%-- <h1 class="d-flex text-dark fw-bolder fs-3 flex-column mb-10">View Events </h1>--%>
       
        <asp:ListView ID="lvCustomers" runat="server" GroupPlaceholderID="groupPlaceHolder1"
ItemPlaceholderID="itemPlaceHolder1" OnPagePropertiesChanging="OnPagePropertiesChanging" OnItemCommand="lvCustomers_ItemCommand">
<LayoutTemplate>
    <table cellpadding="0" cellspacing="0">
      
        <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
        <tr>
            <td >
				<div class="row">
					<div class="col-md-12 text-center">
						  <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvCustomers" PageSize="5">
                    <Fields>
                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                            ShowNextPageButton="false" />
                        <asp:NumericPagerField ButtonType="Link" />
                        <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton = "false" />
                    </Fields>
                </asp:DataPager>
					</div>

				</div>
              
            </td>
        </tr>
    </table>
</LayoutTemplate>
<GroupTemplate>
    <tr>
        <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
    </tr>
</GroupTemplate>
<ItemTemplate>
    <div class="form-group row mb-5">
    <!--end::Toolbar-->
                    <!--begin::Post-->
                    <div class="post d-flex flex-column-fluid" id="kt_post">
                        <!--begin::Container-->
                        <div id="kt_content_container" class="container-xxl">
                            <!--begin::Post card-->
                            <div class="card">
                                <!--begin::Body-->
                                <div class="card-body p-lg-20 pb-lg-0">
    
                                    <!--begin::Layout-->
                                    <div class="d-flex flex-column flex-xl-row">
                                        <!--begin::Content-->
                                        <div class="flex-lg-row-fluid me-xl-10">
                                            <!--begin::Post content-->
                                            <div class="mb-10">
                                                <!--begin::Wrapper-->
                                                <div class="mb-8">
                                                    <!--begin::Info-->

                                                    <!--end::Info-->
                                                    <!--begin::Wrapper-->
                                                    <div class="d-flex flex-column h-100">
                                                        <!--begin::Header-->
                                                        <div class="mb-7">
                                                            <!--begin::Headin-->
                                                            <div class="d-flex flex-stack mb-12">
                                                                <!--begin::Title-->
                                                                <div class="col-md-12 col-xxl-12">
                                                                    <a href="#" class="fs-4 text-gray-800 text-hover-primary fw-bolder mb-0"> <%# Eval("Title") %>        </a>
                                                                </div>
                                                                <!--end::Title-->

                                                            </div>
                                                            <!--end::Heading-->
                                                            <!--begin::Items-->
                                                            <div class="d-flex align-items-center flex-wrap d-grid gap-2">
                                                                <!--begin::Item-->
                                                                <div class="d-flex align-items-center me-5 me-xl-13">
                                                                    <!--begin::Symbol-->
                                                                    <div class="symbol symbol-30px symbol-circle me-3">
                                                                        <asp:Image ID="img_user" runat="server" ImageUrl='<%# GetImageUrl( Eval("LoggedBy").ToString()) %> ' />
                                                                    </div>
                                                                    <!--end::Symbol-->
                                                                    <!--begin::Info-->
                                                                    <div class="m-0">
                                                                        <span class="fw-bold text-gray-400 d-block fs-8">Organiser</span>
                                                                        <span class="fw-bolder text-gray-800 fs-7"> <%# Eval("LoggedByName") %> </span>
                                                                    </div>
                                                                    <!--end::Info-->
                                                                </div>
                                                                <!--end::Item-->
                                                                <!--begin::Item-->
                                                                <div class="d-flex align-items-center">
                                                                    <!--begin::Symbol-->
                                                                    <div class="symbol symbol-30px symbol-circle me-3">
                                                                        <span class="symbol-label bg-success">
                                                                            <!--begin::Svg Icon | path: icons/duotune/abstract/abs042.svg-->
                                                                            <span class="svg-icon svg-icon-5 svg-icon-white">
                                                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
                                                                                    <path d="M18 21.6C16.6 20.4 9.1 20.3 6.3 21.2C5.7 21.4 5.1 21.2 4.7 20.8L2 18C4.2 15.8 10.8 15.1 15.8 15.8C16.2 18.3 17 20.5 18 21.6ZM18.8 2.8C18.4 2.4 17.8 2.20001 17.2 2.40001C14.4 3.30001 6.9 3.2 5.5 2C6.8 3.3 7.4 5.5 7.7 7.7C9 7.9 10.3 8 11.7 8C15.8 8 19.8 7.2 21.5 5.5L18.8 2.8Z" fill="currentColor"></path>
                                                                                    <path opacity="0.3" d="M21.2 17.3C21.4 17.9 21.2 18.5 20.8 18.9L18 21.6C15.8 19.4 15.1 12.8 15.8 7.8C18.3 7.4 20.4 6.70001 21.5 5.60001C20.4 7.00001 20.2 14.5 21.2 17.3ZM8 11.7C8 9 7.7 4.2 5.5 2L2.8 4.8C2.4 5.2 2.2 5.80001 2.4 6.40001C2.7 7.40001 3.00001 9.2 3.10001 11.7C3.10001 15.5 2.40001 17.6 2.10001 18C3.20001 16.9 5.3 16.2 7.8 15.8C8 14.2 8 12.7 8 11.7Z" fill="currentColor"></path>
                                                                                </svg>
                                                                            </span>
                                                                            <!--end::Svg Icon-->
                                                                        </span>
                                                                    </div>
                                                                    <!--end::Symbol-->
                                                                    <!--begin::Info-->
                                                                    <div class="m-0">
                                                                        <span class="fw-bold text-gray-400 d-block fs-8">Price</span>
                                                                        <span class="fw-bolder text-gray-800 fs-7">  <%# Eval("Price") %> </span>
                                                                    </div>
                                                                    <!--end::Info-->
                                                                </div>
                                                                <!--end::Item-->
                                                            </div>
                                                            <!--end::Items-->
                                                        </div>
                                                        <!--end::Header-->
                                                        <!--begin::Body-->
                                                        <div class="mb-6" style="min-height:180px">
                                                            <!--begin::Text-->
                                                            <span class="fw-bold text-gray-600 fs-6 mb-8 d-block" style="max-height:200px">     <%# GetAddress( Eval("Description")) %>     </span>
                                                            <!--end::Text-->
                                                            <!--begin::Stats-->
                                                            <div class=" ">
                                                                <div class="fs-7 fw-bolder text-gray-700"> <i class="fas fa-location-arrow" style="font-size:22px"></i>   <%#  GetAddress( Eval("Address1"), Eval("Address2"),Eval("City"),Eval("state_Province"), Eval("Postalcode"),Eval("Country")) %>   </div>
                                                                <div class="fw-bold text-gray-400 mb-6 mt-5 ">
                                                                <%--    <a href="">
                                                                        <i class="bi bi-pin-map-fill"></i>
                                                                        Google map location</a>--%>
                                                                </div>
                                                            </div>
                                                            <!--end::Stats-->
                                                        </div>
                                                        <!--end::Body-->
                                                        <!--begin::Footer-->
                                                      
                                                        <!--end::Footer-->
                                                    </div>
                                                    <!--end::Wrapper-->
                                                    <!--end::Container-->
                                                </div>
                                                <!--end::Wrapper-->

                                               
                                            </div>
                                            <!--end::Post content-->
                                        </div>
                                        <!--end::Content-->
                                        <!--begin::Sidebar-->
                                        <div class="flex-column flex-lg-row-auto w-100 w-xl-600px mb-5">
                                            <div class="overlay mt-8">
												 <asp:Image ID="img_event" runat="server"  CssClass="img-fluid" ImageUrl='<%# GetImmage(Eval("unid")) %>' />
																<%-- <asp:Image ID="imgView" runat="server" ImageUrl='<%# GetImmage(Eval("ID").ToString()) %>' Height="300" />--%>
                                             <%--   <asp:Label ID="lblImg" runat="server" Text='<%# GetImmageString(Eval("ID").ToString()) %>'></asp:Label>--%>
																<%--<div class="bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-350px" style=background-image:url('<%# GetImmage(Eval("unid")) %>')>
                                                                   
																</div>  --%>
															</div>
                                        </div>
                                        <!--end::Sidebar-->
                                    </div>
                                    <div class="row">
                                                           
															<div class="col-lg-12  my-10">
					
																<asp:Button ID="btnView" runat="server" Text="View Event" CommandName="viewmore" CommandArgument='<%# Eval("unid") %>' style="margin-right:10px"  />
<asp:Button ID="Button1" runat="server" Text="Go Live" CssClass="btn btn-success" CommandName="golive" CommandArgument='<%# Eval("unid") %>' style='<%# "margin-right:10px;background-color:#50CD89; display:" + Eval("display") %>' />

																<asp:Button ID="btnEditEvent" runat="server" Text="Edit Event" CommandName="editevent" CssClass="btn btn-light" CommandArgument='<%# Eval("unid") %>' style="margin-right:10px"    />
																<asp:Button ID="btnGenerate" runat="server" Text="Download QR" CommandName="qr" CssClass="btn btn-light" CommandArgument='<%# Eval("unid") %>' style="margin-right:10px"   />
																<asp:Button ID="btnSocialSettings" runat="server" Text="Social Settings" CommandName="social" CssClass="btn btn-light" CommandArgument='<%# Eval("unid") %>' style="margin-right:10px"   />
															<asp:Button ID="btnCopyURL" runat="server" Text="Copy URL" CommandName="url" CssClass="btn btn-light" OnClientClick='<%# Eval("QRcode", "return copyUrlToClipboard(\"{0}\");") %>' CommandArgument='<%# Eval("unid") %>' style="margin-right:10px"   />
																			<asp:Button ID="btnAtten" runat="server" Text="View Attendees" CommandName="attendees" CssClass="btn btn-light" CommandArgument='<%# Eval("unid") %>' style="margin-right:10px"   />
																	<asp:Button ID="btnEventInteraction" runat="server" Text="Event Interaction" CommandName="interaction" CssClass="btn btn-light" CommandArgument='<%# Eval("unid") %>' style="margin-right:10px"  Visible="false" />
																<asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="del" CssClass="btn btn-light" CommandArgument='<%# Eval("unid") %>' style="margin-right:10px" OnClientClick="return confirm('Do you want to delete this record?');"   />
															</div>
                                                          
                                                        </div>
                                <!--end::Body-->
                            </div>
                            <!--end::Post card-->
                        </div>
                        <!--end::Container-->
                    </div>
                    <!--end::Post-->
                </div>
         </div>
</ItemTemplate>
</asp:ListView>


                               

    </div>

    <!--end::Main-->
    <!--begin::Javascript-->
    <!--begin::Global Javascript Bundle(used by all pages)-->
 <%--   <script src="assets/plugins/global/plugins.bundle.js"></script>
    <script src="assets/js/scripts.bundle.js"></script>
    <!--end::Global Javascript Bundle-->
    <!--begin::Page Vendors Javascript(used by this page)-->
    <script src="assets/plugins/custom/fullcalendar/fullcalendar.bundle.js"></script>
    <!--end::Page Vendors Javascript-->
    <!--begin::Page Custom Javascript(used by this page)-->
    <script src="/metronic8/demo1/assets/js/custom/apps/inbox/listing.js"></script>
    <script src="assets/js/custom/widgets.js"></script>
    <script src="assets/js/custom/apps/chat/chat.js"></script>
    <script src="assets/js/custom/modals/create-app.js"></script>
    <script src="assets/js/custom/modals/upgrade-plan.js"></script>--%>
    <!--end::Page Custom Javascript-->
    <!--end::Javascript-->





	<ajaxToolkit:ModalPopupExtender ID="mdlSocialSettings" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="btnSpeakerAddOption" PopupControlID="pnlAddSpeaker" CancelControlID="btnSpekerClose">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Label ID="btnSpeakerAddOption" runat="server"></asp:Label>
    <asp:Label ID="btnSpekerClose" runat="server"></asp:Label>

	<asp:Panel ID="pnlAddSpeaker" runat="server" BackColor="White" Style="display: none;"
        Width="1000px"  Height="650px" ScrollBars="Both">                                                                        
                                                                                                   
        <div class="card card-bordered p-10">
            <div class="card-header">
                <h3 class="card-title">
                    <asp:Label ID="lblModelHeading" runat="server" Text="Social sharing setting"></asp:Label>
					<asp:HiddenField ID="heventid" runat="server" />
                </h3>
              
            </div>
           	<div class="row mb-6 ">
				   </div>
											<div class="row mb-6 ">
												<!--begin::Label-->
                                                <label class="col-lg-3 col-form-label fw-bold fs-6">  Image</label>
												
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:Image ID="imgTop" runat="server" Height="200px" Visible="false" />


													<asp:FileUpload runat="server" id="fileUploadimage" />
													
												</div>
												<!--end::Col-->
											</div>
										
											<div class="row mb-6">
												<!--begin::Label-->
												 <label class="col-lg-3 col-form-label required fw-bold fs-6"> Description </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-9 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtDescription"     placeholder="Description" runat="server" TextMode="MultiLine" SkinID="txtMulti_100"></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
										
               <div class="row mb-6">
                <!--begin::Label-->
             <label class="col-lg-3 col-form-label required fw-bold fs-6"> Key words:</label>
                <!--end::Label-->
                <!--begin::Col-->
                <div class="col-lg-8">
                    <div class="row">
                        <!--begin::Col-->
                        <div class="col-lg-9 fv-row fv-plugins-icon-container">
                           <asp:TextBox ID="txtKeywords"     placeholder="Keywords" runat="server"></asp:TextBox>
                           
                        </div>
                        <!--end::Col-->

                    </div>
                </div>
                <!--end::Col-->
            </div>


            <div class="card-footer d-flex justify-content-end py-6 px-9">
                <asp:Button ID="Button2" runat="server" CssClass="btn btn-light" Text="Close" style="margin-right:10px" />

                <asp:Button runat="server" ID="btnUpdate" Text="Submit"  OnClick="btnUpdate_Click"  />

               <%-- <asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Changes" OnClick="btnSaveChangesPop_Click" />--%>
            </div>
        </div>






    </asp:Panel>



</asp:Content>
