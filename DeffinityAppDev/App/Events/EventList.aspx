<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="EventList.aspx.cs" Inherits="DeffinityAppDev.App.EventList" %>


<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
    Events
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<style>
		 .icon-20 {
        font-size: 20px !important;
    }
	</style>
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
				<button type="button" class="btn btn-primary" onclick="displayAllEmbedCode()">
  Show Embed Code
</button>
			 </div>
			<!-- Button to Trigger Modal -->


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
												<asp:HiddenField runat="server" ID="hdWordpressCode" Value='<%# HttpUtility.JavaScriptStringEncode(Eval("WordpressCode").ToString()) %>' ClientIDMode="Static" />
														<asp:LinkButton ID="btnView" runat="server" CommandName="viewmore" CommandArgument='<%# Eval("unid") %>' CssClass="btn btn-primary text-white" ToolTip="Preview the event as a visit to your site." style="margin-right:10px">
    <i class="bi bi-binoculars icon-20"></i>
</asp:LinkButton>

<asp:LinkButton ID="Button1" runat="server" CommandName="golive" CommandArgument='<%# Eval("unid") %>' CssClass="btn btn-success" ToolTip="Go to the Livestream page" style='<%# "margin-right:10px;background-color:#50CD89; display:" + Eval("display") %>'>
    Go Live
</asp:LinkButton>

<asp:LinkButton ID="btnEditEvent" runat="server" CommandName="editevent" CommandArgument='<%# Eval("unid") %>' CssClass="btn btn-light" ToolTip="Edit the event" style="margin-right:10px">
    <i class="bi bi-pencil icon-20"></i>
</asp:LinkButton>

<asp:LinkButton ID="Button3" runat="server" CommandName="reminder" CommandArgument='<%# Eval("unid") %>' CssClass="btn btn-light" ToolTip="Schedule event reminders." style="margin-right:10px">
    <i class="bi bi-calendar2-check icon-20"></i>
</asp:LinkButton>

<asp:LinkButton ID="btnGenerate" runat="server" CommandName="qr" CommandArgument='<%# Eval("unid") %>' CssClass="btn btn-light" ToolTip="Download the QR code for this event" style="margin-right:10px">
    <i class="bi bi-qr-code-scan icon-20"></i>
</asp:LinkButton>

<asp:LinkButton ID="btnSocialSettings" runat="server" CommandName="social" CommandArgument='<%# Eval("unid") %>' CssClass="btn btn-light" ToolTip="Set up the social sharing options for this event to ensure it appears as you want when shared on social media platforms." style="margin-right:10px">
    <i class="bi bi-share icon-20"></i>
</asp:LinkButton>

<asp:LinkButton ID="btnCopyURL" runat="server" CommandName="url" CommandArgument='<%# Eval("unid") %>' CssClass="btn btn-light" ToolTip="Copy the event's short URL to share it on social media, send via SMS, email, or any other platform." OnClientClick='<%# Eval("QRcode", "return copyUrlToClipboard(\"{0}\");") %>' style="margin-right:10px">
    <i class="bi bi-copy icon-20"></i>
</asp:LinkButton>

<asp:LinkButton ID="btnAtten" runat="server" CommandName="attendees" CommandArgument='<%# Eval("unid") %>' CssClass="btn btn-light" ToolTip="Access the list of potential attendees, review ticket sales statistics, and resend tickets to them before the event." style="margin-right:10px">
    <i class="bi bi-people icon-20"></i>
</asp:LinkButton>

<asp:LinkButton ID="btnEventInteraction" runat="server" CommandName="interaction" CommandArgument='<%# Eval("unid") %>' CssClass="btn btn-light" ToolTip="Event Interaction" Visible="false" style="margin-right:10px">
    Event Interaction
</asp:LinkButton>

<asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("unid") %>' CssClass="btn btn-light" ToolTip="Delete this event" OnClientClick="return confirmDelete(this);" style="margin-right:10px">
    <i class="bi bi-trash icon-20"></i>
</asp:LinkButton>


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


	<!-- Bootstrap Modal -->
<div class="modal fade" id="embedCodeModal" tabindex="-1" aria-labelledby="embedCodeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="embedCodeModalLabel">WordPress Embed Code</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <div class="mb-3">
          <!-- Embed Code Display -->
          <label for="embedCodeText" class="form-label">Copy the code below:</label>
          <textarea id="embedCodeText" class="form-control" rows="4">
<!-- Sample WordPress Embed Code -->

          </textarea>
        </div>
      </div>
      <div class="modal-footer">
        <!-- Copy Button -->
        <button type="button" class="btn btn-primary" id="copyEmbedCode">Copy Code</button>
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>

<div class="modal fade" id="embedAllCodeModal" tabindex="-1" aria-labelledby="embedCodeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="embedCodeModalLabel">WordPress Embed Code</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <div class="mb-3">
          <!-- Embed Code Display -->
          <label for="embedAllCodeText" class="form-label">Copy the code below:</label>
          <textarea id="embedAllCodeText" class="form-control" rows="4">
<!-- Sample WordPress Embed Code -->
          </textarea>
        </div>
        
        <!-- Configuration Options -->
      <!-- Configuration Options -->
		      <div class="row mb-3 align-items-center">
      <div class="col-sm-4">
        <label for="showPaging" class="form-label">Show Paging</label>
      </div>
      <div class="col-sm-4">
        <input type="checkbox" id="showPaging" onchange="updateEmbedCode()" class="form-check-input" />
      </div>
    </div>


        <div class="row mb-3 align-items-center">
          <div class="col-lg-12" style="display:flex;align-items:center">
            <label for="eventsPerPage" class="">Show</label>
         
            <input type="number" value="10" oninput="updateEmbedCode()" id="eventsPerPage" class="form-control" style="width: 80px;margin:0px 10px" />
      
         
            Per Page
          </div>
        </div>

    
 
		  <div class="row mb-3">
  <div class="col-sm-4">
    <label for="viewType" class="form-label">Type of View</label>
  </div>
  <div class="col-sm-8">
    <select id="viewType" class="form-control" style="max-width: 150px;" onchange="updateEmbedCode()">
      <option value="panel">Panel View</option>
      <option value="list">List View</option>
    </select>
  </div>
</div>
		         <div class="row mb-3">
         <div class="col-sm-4">
           <label for="panelHeight" class="form-label">Panel Height</label>
         </div>
         <div class="col-sm-8">
           <input type="number" oninput="updateEmbedCode()" id="panelHeight" class="form-control" placeholder="Enter Panel Height" style="max-width: 150px;" />
         </div>
       </div>
        <div class="row mb-3">
          <div class="col-sm-4">
            <label for="panelWidth" class="form-label">Panel Width</label>
          </div>
          <div class="col-sm-8">
            <input type="number" id="panelWidth" oninput="updateEmbedCode()" class="form-control" placeholder="Enter Panel Width" style="max-width: 150px;" />
          </div>
        </div>
      </div>
      <div class="modal-footer">
        <!-- Copy Button -->
        <button type="button" class="btn btn-primary" id="copyAllEmbedCode">Copy Code</button>
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>


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
	<asp:HiddenField runat="server" ID="hfAllEmbedCode" ClientIDMode="Static" />
		<asp:HiddenField runat="server" ID="hfAllEmbedListCode" ClientIDMode="Static" />

	<asp:HiddenField runat="server" ID="BasehfAllEmbedCode" ClientIDMode="Static" />
	<asp:HiddenField runat="server" ID="BasehfAllEmbedListCode" ClientIDMode="Static" />
	<script>
		function updateEmbedCode() {
            const viewType = document.getElementById("viewType").value;
			var display = "";
			var bgcolor = "#990000";
            if (viewType === "panel") {
				display = "";
				bgcolor = "#50CD89";
            } else if (viewType === "list") {
                 display = "flex";
            }
            const JS = `
<script>
// Wait for the DOM to fully load

document.addEventListener("DOMContentLoaded", () => {
    const container = document.getElementById("PlegitWordpressEmbed");

    // Check if pagination is enabled
    if (container && container.dataset.showPaging === "true") {
        const itemsPerPage = parseInt(container.dataset.pagingValue, 10) || 10;
        const items = Array.from(container.children);

        // Filter out the pagination container if already exists
        const mainItems = items.filter(item => !item.classList.contains('pagination-container'));

        // Calculate total pages
        const totalPages = Math.ceil(mainItems.length / itemsPerPage);

        // Create pagination controls
        const paginationContainer = document.createElement("div");
        paginationContainer.className = "pagination-container";

        for (let i = 1; i <= totalPages; i++) {
            const pageButton = document.createElement("button");
            pageButton.className = "pagination-button";
            pageButton.textContent = i;
            pageButton.dataset.page = i;
            paginationContainer.appendChild(pageButton);
        }

        // Add pagination container to the DOM
        container.appendChild(paginationContainer);

        // Function to display items for the current page
        const showPage = (pageNumber) => {
            const start = (pageNumber - 1) * itemsPerPage;
            const end = start + itemsPerPage;

            mainItems.forEach((item, index) => {
                item.style.display = index >= start && index < end ? "${display}" : "none";
            });

            // Highlight the active page button
            document.querySelectorAll(".pagination-button").forEach(button => {
                button.classList.toggle("active", button.dataset.page == pageNumber);
            });
        };

        // Initial display of the first page
        showPage(1);

        // Add event listeners to pagination buttons
        document.querySelectorAll(".pagination-button").forEach(button => {
            button.addEventListener("click", () => {
                const page = parseInt(button.dataset.page, 10);
                showPage(page);
            });
        });
    }

    // Styling for pagination controls
    const style = document.createElement("style");
    style.textContent = \`
        #PlegitWordpressEmbed {
            position: relative; /* Ensure positioning context for child elements */
        }
        .pagination-container {
            position: absolute; /* Place at the bottom of the parent */
            bottom: 0;
            left: 50%; /* Center horizontally */
            transform: translateX(-50%); /* Correct centering alignment */
            text-align: center;
            margin-top: 20px; /* Space between content and pagination */
        }
        .pagination-button {
            margin: 0 5px;
            padding: 10px 15px;
            border: none;
            background-color: #ddd;
            color: #333;
            cursor: pointer;
            border-radius: 5px;
        }
        .pagination-button.active {
            background-color: ${bgcolor};
            color: white;
        }
        .pagination-button:hover {
            background-color: ${bgcolor} ;
            color: white;
        }
    \`;
    document.head.appendChild(style);
});
// Closing script tag split to avoid parsing issues
</scr` + `ipt>
`;
       // Get the selected value from the dropdown
		
            const heightInput = document.getElementById("panelHeight").value;
			const widthInput = document.getElementById("panelWidth").value;

            const height = heightInput+"px" || "400px"; // Use defaultHeight if input is empty
			const width = widthInput + "px" || "600px";   // Use defaultWidth if input is empty

			const showPaging = document.getElementById('showPaging').checked;
			const pagingValue = document.getElementById('eventsPerPage').value;

            // Get the hidden field values
            var panelEmbedCode = document.getElementById("BasehfAllEmbedCode").value;
            var listEmbedCode = document.getElementById("BasehfAllEmbedListCode").value;

            panelEmbedCode = panelEmbedCode
								.replace(/{{height}}/g, height)
								.replace(/{{width}}/g, width)
								.replace(/{{showPaging}}/g, showPaging)
								.replace(/{{pageValue}}/g, pagingValue);
			listEmbedCode = listEmbedCode
								.replace(/{{height}}/g, height)
								.replace(/{{width}}/g, width)
								.replace(/{{showPaging}}/g, showPaging)
								.replace(/{{pageValue}}/g, pagingValue);

			document.getElementById("hfAllEmbedCode").value = panelEmbedCode;
			document.getElementById("hfAllEmbedListCode").value = listEmbedCode;
            // Get the textarea
            const embedCodeTextArea = document.getElementById("embedAllCodeText");
			
            // Update the textarea based on the selection
            if (viewType === "panel") {
                embedCodeTextArea.value = JSON.parse('"' + panelEmbedCode + '"')+JS; 
            } else if (viewType === "list") {
                embedCodeTextArea.value = JSON.parse('"' + listEmbedCode + '"')+JS; 
            }
        }

    </script>
	<script>
        document.getElementById("copyAllEmbedCode").addEventListener("click", function () {
            var embedCodeTextarea = document.getElementById("embedAllCodeText");
            embedCodeTextarea.select();
            document.execCommand("copy");

            // Optional: Show a temporary alert to confirm
			showswal("Copied Successfully!", "OK");
        });
    </script>
	<script>
        function showEmbedCodeModal(embedCode) {
            // Set the value of the textarea in the modal
            document.getElementById('embedAllCodeText').value = embedCode;

            // Show the modal
            var modal = new bootstrap.Modal(document.getElementById('embedAllCodeText'));
            modal.show();
        }
    </script>
	<script>
        function displayAllEmbedCode() {
            var encodedHtml = document.getElementById('hfAllEmbedCode').value;

            // Decode the encoded HTML using JSON.parse
            var decodedHtml = JSON.parse('"' + encodedHtml + '"');

            // Set the decoded HTML into the textarea
            document.getElementById('embedAllCodeText').value = decodedHtml;

            // Show the modal
            $('#embedAllCodeModal').modal('show');
        }


    </script>
<script>
    function confirmDelete(button) {
        // Prevent the default action (the postback via the link)
        event.preventDefault();

        Swal.fire({
            title: "Are you sure?",
            text: "To delete this event type the word 'EVENT' to confirm you wish to delete it. Note: You cannot undo this action.",
            input: "text",
            inputPlaceholder: "Type 'EVENT'",
            showCancelButton: true,
            confirmButtonText: "Delete",
            cancelButtonText: "Cancel",
            confirmButtonColor: "#d33",
            customClass: {
                cancelButton: "btn btn-light",
                confirmButton: "btn btn-danger"
            },
            preConfirm: (inputValue) => {
                if (inputValue !== "EVENT") {
                    Swal.showValidationMessage("You must type 'EVENT' to confirm.");
                }
                return inputValue === "EVENT";
            },
        }).then((result) => {
            if (result.isConfirmed) {
                // Extract the href from the button (the postback info)
                var href = button.getAttribute("href");

                // Manually trigger the postback using the href (this calls __doPostBack)
                eval(href); // This will run the __doPostBack with the correct arguments
            }
        });

        // Return false to prevent immediate postback
        return false;
    }
</script>


</asp:Content>
