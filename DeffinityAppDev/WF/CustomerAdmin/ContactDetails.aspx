<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="ContactDetails.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.ContactDetailsPage" %>

<%@ Register Src="~/WF/CustomerAdmin/Controls/ContactTabCtrl.ascx" TagPrefix="Pref" TagName="ContactTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     Contact Details
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
       <link href="../../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
   
     <link rel="stylesheet" href="../../Content/assets/js/select2/select2.css"/>
	<link rel="stylesheet" href="../../Content/assets/js/select2/select2-bootstrap.css"/>
	<link rel="stylesheet" href="../../Content/assets/js/multiselect/css/multi-select.css"/>
    <script src="../../Content/assets/js/select2/select2.min.js"></script>
	<%--<script src="assets/js/jquery-ui/jquery-ui.min.js"></script>--%>
	<%--<script src="../../Content/assets/js/selectboxit/jquery.selectBoxIt.min.js"></script>--%>
	<script src="../../Content/assets/js/tagsinput/bootstrap-tagsinput.min.js"></script>
	<%--<script src="assets/js/typeahead.bundle.js"></script>
	<script src="assets/js/handlebars.min.js"></script>--%>
	<script src="../../Content/assets/js/multiselect/js/jquery.multi-select.js"></script>
     	
    <%-- <nav class="navbar navbar-default" role="navigation" id="navTab">
         <Pref:ContactTabCtrl runat="server" ID="ContactTabCtrl" />
        </nav>--%>
     <script type="text/javascript">
         $(document).ready(function () {
             $('#MainContent_MainContent_lbtnUpload').click(
                 function () {
                     $('#contactupload').toggle();
                     $('#MainContent_MainContent_lbtnUpload').toggle();
                 });
         });
     </script>	
    <style>
        .clsred {
          color:  red;
        }
    </style>
    <asp:HiddenField ID="hsid" runat="server" ClientIDMode="Static" />
    <asp:Panel id="pnl_profile" runat="server">
    	
				<div class="card shadow-sm mb-6" >
					<div class="card-body">
			
						<div class="member-form-add-header">
                            <div class="row mb-6">
                                 <div class="col-sm-10" style="margin-bottom:5px">
                                      <asp:Label ID="lblMsg1" runat="server" Text="" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
                <asp:Literal ID="lblmsg" runat="server" Text="" EnableViewState="false"></asp:Literal>
                <asp:Literal ID="lblerrmsg" runat="server" Text="" EnableViewState="false"></asp:Literal>
                <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="add"  />
                                     </div>
            </div>
                            <div class="row mb-6" id="divPercent" runat="server" visible="false">
                                <div class="col-sm-10">
                                    <p class="bg-info" id="lblinfo" runat="server">
                                        </p>
                                   
                                    <br />
                                    </div>

                            </div>
							<div class="row mb-6">
								
								<div class="col-md-10 col-sm-8">
			                       <div class="user-info">
									<div class="user-img">
                                       <asp:Image ID="imguser" runat="server" CssClass="img-responsive img-circle" ImageUrl="~/WF/Admin/ImageHandler.ashx?type=contact&id=0"  ClientIDMode="Static" AlternateText="user-pic"/>
                                      </div>
                                          <div class="user-name">
                                            <asp:Label ID="lblFullName" runat="server" ClientIDMode="Static" ForeColor="Black" Font-Size="X-Large" Text="[Add New Contact]"></asp:Label> <span id="lblUsertype" runat="server"></span>  <br />
                                               <a id="lbtnUpload" style="font-size:small;cursor:pointer;" class="label label-default" title="Upload Image" runat="server">Upload</a>
                                                    <div id="contactupload" style="display:none">
                                        <asp:FileUpload ID="Fileupload_contact" runat="server" /><br /><br />
                                    <asp:Button ID="btnuser" runat="server" SkinID="btnUpload" OnClick="btnuser_Click" ValidationGroup="add" />
                                              <asp:Button ID="btncancel" runat="server" SkinID="btnCancel"/>
                                            </div>
                                          	</div>	
                                      					
									<br />
                                       <br />
									
			                       </div>
								</div>
                                <div class="col-md-2 col-sm-4 pull-right-sm">
									<div class="action-buttons">
                                        <asp:Button ID="btnupdate"  runat="server" Text="Save Changes" SkinID="btnDefault" ValidationGroup="add" OnClick="btnupdate_Click"   />
                                        <asp:Button ID="btnreset" runat="server" CssClass="btn btn-block btn-gray" Text="Back to Contact list" OnClick="btnreset_Click" CausesValidation="false" Visible="false"  />
									</div>
								</div>
							</div>
						</div>
                        
                        	<div class="row mb-6" style="display:none;visibility:hidden;">
			
				<div class="col-sm-3">
					
					<div class="xe-widget xe-counter" data-count=".num" data-from="0" data-to="99.9" data-suffix="%" data-duration="2">
						<div class="xe-icon">
							<i class="fa-credit-card"></i>
						</div>
						<div class="xe-label">
							<strong class="num"><asp:Literal ID="lblTotalRevenueThisYear" runat="server" Text="0.00"></asp:Literal> </strong>
							<span>Total Revenue This Year</span>
						</div>
					</div>
					
				</div>
                                <div class="col-sm-3">
					
					<div class="xe-widget xe-counter" data-count=".num" data-from="0" data-to="99.9" data-suffix="%" data-duration="2">
						<div class="xe-icon">
							<i class="fa-wrench"></i>
						</div>
						<div class="xe-label">
							<strong class="num"><asp:Literal ID="lblOutstandingInvoices" runat="server" Text="0.00"></asp:Literal> </strong>
							<span>Outstanding Invoices</span>
						</div>
					</div>
					
				</div>
                                <div class="col-sm-3">
					
					<div class="xe-widget xe-counter" data-count=".num" data-from="0" data-to="99.9" data-suffix="%" data-duration="2">
						<div class="xe-icon">
							<i class="fa-dashboard"></i>
						</div>
						<div class="xe-label">
							<strong class="num"><asp:Literal ID="lblDebtorDays" runat="server" Text="0.00"></asp:Literal></strong>
							<span>Debtor Days</span>
						</div>
					</div>
					
				</div>
                                </div>
                        <div class="row mb-6" style="display:none;visibility:hidden;">
                           <div class="col-md-3" id="pnlCreatedBy" runat="server" visible="false">
                               <div class="well well-sm">
                                  <p> Created By:<label id="lblCreatedBy" runat="server"></label></p>
							</div>
                              
                               </div>
                            <div class="col-md-3" id="pnlTotals" runat="server">
                               <div class="well well-sm">
                                  <p> Open Jobs:<label id="lblOpen" runat="server" style="float:right;"></label></p>
<p> Completed Jobs:<label id="lblCompletedJobs" runat="server" style="float:right;"></label></p>
<p> Invoice Overdue:<label id="lblInvoiceOverdue" runat="server" style="float:right;"></label></p>
                                   <p> Invoiced To Date:<label id="lblInvoiceToDate" runat="server" style="float:right;"></label></p>
                                   <p> Maintenance Agreement:<label id="lblMaintenaceAgreement" runat="server" style="float:right;"></label></p>
                                    <p> Equipment:<label id="lblEquipment" runat="server" style="float:right;"></label></p>
							</div>
                              
                               </div>
                        </div>
						<div class="member-form-inputs pt-5">		
                           
							<div class="row mb-6">
								<div class="col-md-2">
									<label class="control-label" for="name"><%= Resources.DeffinityRes.Name%></label> <span class="clsred">*</span>
								</div>
								<div class="col-md-4">
                                    <asp:TextBox ID="txtname" runat="server" SkinID="txt_90" MaxLength="250"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="Rf_name" runat="server" ControlToValidate="txtname"
                                                            ErrorMessage="Please enter name" ForeColor="Red" ValidationGroup="add" Display="None"></asp:RequiredFieldValidator>
								</div>
							</div>
			
							<div class="row mb-6">
								<div class="col-md-2">
									<label class="control-label" for="birthdate"><%= Resources.DeffinityRes.EmailAddress%></label> <span class="clsred">*</span>
								</div>
								<div class="col-md-4">
		                            	<asp:TextBox ID="txtEmail" runat="server" SkinID="txt_90" MaxLength="500"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfemail" runat="server" ControlToValidate="txtEmail"
                                                            ErrorMessage="Please enter email address" ForeColor="Red" ValidationGroup="add" Display="None"></asp:RequiredFieldValidator>
                                      <asp:RegularExpressionValidator ID="validmail" runat="server" ControlToValidate="txtEmail"
                                        Display="None" ErrorMessage=" Please enter valid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="add"></asp:RegularExpressionValidator>
								</div>
							</div>
                                  
                            <div class="row mb-6">
								<div class="col-md-2">
									<label class="control-label" for="name">Primary Phone</label>
								</div>
								<div class="col-md-4">
                                    <asp:TextBox ID="txtTelephone" SkinID="txt_90" ValidationGroup="update" runat="server" MaxLength="20"></asp:TextBox>
                                    <%--<asp:RangeValidator ID="RangeValidator1" ValidationGroup="update" runat="server" ControlToValidate="txtzipcode"
                                         ErrorMessage="Please enter valid zip code" MaximumValue="999999" MinimumValue="100000"></asp:RangeValidator--%>
								</div>
							</div>
                              <div class="row mb-6">
								<div class="col-md-2">
									<label class="control-label" for="name">Cell number</label>
								</div>
								<div class="col-md-4">
                                    <asp:TextBox ID="txtmobileno" runat="server" SkinID="txt_90" MaxLength="20"></asp:TextBox>
								</div>
							</div>

                            
          <div class="row mb-6">
              <div class="col-md-2">
 <label class="control-label">Address 1</label> <span class="clsred">*</span>
                  </div>
           <div class="col-sm-4">
               <asp:TextBox ID="txtAddress1" runat="server" ClientIDMode="Static" MaxLength="500" SkinID="txt_90"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress1"
                                        Display="None" ErrorMessage="Please enter address 1"
                                        ValidationGroup="add" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
	</div>

          
          <div class="row mb-6">
 <label class="col-sm-2 control-label">Address 2</label>
           <div class="col-sm-4">
               <asp:TextBox ID="txtAddress2" runat="server" ClientIDMode="Static"  MaxLength="500" SkinID="txt_90"></asp:TextBox>
            </div>
	</div>

          
          <div class="row mb-6">
 <label class="col-sm-2 control-label">City</label>
           <div class="col-sm-4">
               <asp:TextBox ID="txtCity" runat="server" ClientIDMode="Static" MaxLength="200" SkinID="txt_90"></asp:TextBox>
            </div>
	</div>

          
          <div class="row mb-6">
 <label class="col-sm-2 control-label">State</label>
           <div class="col-sm-4">
               <asp:TextBox ID="txtState" runat="server" ClientIDMode="Static" MaxLength="200" SkinID="txt_90"></asp:TextBox>
            </div>
	</div>

         
          <div class="row mb-6">
              <div class="col-sm-2">
 <label class="control-label">Zipcode</label> <span class="clsred">*</span>
                  </div>
           <div class="col-sm-4">
               <asp:TextBox ID="txtZipcode" runat="server" ClientIDMode="Static" MaxLength="50" SkinID="txt_90"></asp:TextBox>
               <asp:RequiredFieldValidator ID="rf_zipcode" runat="server" ControlToValidate="txtZipcode"
                                        Display="None" ErrorMessage="Please enter zipcode"
                                        ValidationGroup="add" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
	</div>


                             <div class="row mb-6">
								<div class="col-md-2">
									<label class="control-label" for="name">Source of Lead</label>
								</div>
								<div class="col-md-4">
                                    <asp:DropDownList ID="ddlSourceofLead" runat="server" SkinID="ddl_90"></asp:DropDownList>
								</div>
							</div>

                            <div class="row mb-6">
									<label class="control-label col-sm-2" for="name">Tags</label>
								<div class="col-md-4 form-inline">
                                    	
									<script type="text/javascript">
                                        jQuery(document).ready(function ($) {
                                            $("#listTags").select2({
                                                placeholder: 'Choose Tags',
                                                allowClear: true
                                            }).on('select2-open', function () {
                                                // Adding Custom Scrollbar
                                                $(this).data('select2').results.addClass('overflow-hidden').perfectScrollbar();
                                            });

                                        });
									</script>
									
									<asp:ListBox CssClass="form-control" runat="server" id="listTags" ClientIDMode="Static" SelectionMode="Multiple" Width="250px">
                                       <%-- <asp:ListItem></asp:ListItem>
											<asp:ListItem>Development</asp:ListItem>
											<asp:ListItem>Designing</asp:ListItem>
											<asp:ListItem>Engineering</asp:ListItem>
											<asp:ListItem>Analysis</asp:ListItem>--%>
											
									</asp:ListBox>
                                    <asp:LinkButton ID="btnAddTagPop" runat="server" SkinID="BtnLinkAdd" style="margin-top:10px;" ToolTip="Create a Tag" />
								</div>
							</div>
                              <div class="" style="display:none;">
          
              <div class="col-md-2">
              <label class="control-label" for="name">Warranty documents</label>
                  </div>
               <div class="col-sm-4">
                    <asp:FileUpload ID="fileupload" runat="server" ClientIDMode="Static" AllowMultiple="true" />
               </div>
            
         
    </div>
    <div class="" style="display:none">
          <div class="col-md-12">
               <div class="col-sm-6">
                  
               </div>
          </div>
    </div>
                             <ajaxtoolkit:modalpopupextender id="mdlWorksheet" cancelcontrolid="imgBtnWorksheetCancel"
                        runat="server" backgroundcssclass="modalBackground" targetcontrolid="btnAddTagPop"
                        popupcontrolid="pnlWorksheet">
                    </ajaxtoolkit:modalpopupextender>
        <asp:Panel ID="pnlWorksheet" runat="server" BackColor="White" Width="230px" ScrollBars="None" BorderStyle="Double" BorderColor="LightSteelBlue">

                        <div class="form-group row">
                                    <div class="col-md-12">
                                             <strong> Add Tag </strong>
                                             <hr class="no-top-margin" />
                                    </div>
                        </div>

                        <div class="form-group row">
                                      <div class="col-md-10">
                                          <asp:TextBox ID="txtTag" runat="server"></asp:TextBox>
                                          <asp:HiddenField ID="H_tag" runat="server" Value="0" />
                                          <asp:Label ID="lblTag" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTag"
                                                                                      ErrorMessage="Please enter tag" ForeColor="Red"
                                                                                      ValidationGroup="Group_Tag"></asp:RequiredFieldValidator>
                                      </div>
                         </div>
                         <div class="form-group row">
                                      <div class="col-md-10">
                                          <asp:Button runat="server" ID="imgaddcourse" Style="display: none" />
                                          <asp:Button ID="BtnAddTag" runat="server" Text="OK" SkinID="btnSubmit"
                                                                                       ValidationGroup="Group_Tag" OnClick="BtnAddTag_Click"  />
                                          <asp:Button ID="imgBtnWorksheetCancel" runat="server" Text="Close" SkinID="btnCancel" />
                                      </div>
                         </div>
                    </asp:Panel>


                             <asp:HiddenField ID="pstatus" runat="server" Value="" ClientIDMode="Static" />
             <asp:HiddenField ID="sid" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="selectedids" runat="server" Value="" ClientIDMode="Static" />
                            <asp:HiddenField ID="haid" runat="server" Value="0" ClientIDMode="Static" />
    <%--<button id="btn">btn</button>--%>
           
         <asp:HiddenField ID="huid" runat="server" ClientIDMode="Static" />
 <script type="text/javascript" language="javascript" class="init">
     function getUser() {
         return $("[id$='huid']").val();
     };
     function getUrlParameter(name) {
         name = name.toLowerCase().replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
         var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
         var results = regex.exec(location.search.toLowerCase());

         return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
     };

     var editor; // use a global for the submit and return data rendering in the examples
     var selected = [];
     var table;
     

     function setDeleteButton()
     {
         $(window).on('load', function () {
             $(".editor_remove").each(function (index) {
                 if ($('#hsid').val() != "1") {
                     $(this).hide();
                 }
             });
         });
        
       
       
     }
    

     


     function GetTrackUrl(l, m, mi, p, imurl, t) {

         if (l != 0)
             return '<a href="' + 'ProjectLabourTracker.aspx?project=' + p + '" target="_self" title="' + t + '"><img src="' + imurl + '"  border="0"></a>';
         else if (m != 0)
             return '<a href="' + 'ProjectMaterialTracker.aspx?project=' + p + '" target="_self" title="' + t + '"><img src="' + imurl + '" border="0"></a>';
         else if (mi != 0)
             return '<a href="' + 'ProjectMiscTracker.aspx?project=' + p + '" target="_self" title="' + t + '"><img src="' + imurl + '" border="0"></a>';

     }

   
 </script>
             <div class="row mb-6" id="pnladdress" runat="server" visible="false">
                 

                 <asp:HyperLink ID="btnAddNewAddress" runat="server" Text="Add New Address" SkinID="Button" NavigateUrl="~/WF/CustomerAdmin/ContactAddressDetailsBasic.aspx" style="float:right"  />
            <table id="example" class="col-md-12 display nowrap"  cellspacing="0" width="100%">
        <thead>
            <tr>
                <th></th>
                <th>Address1</th>
                 <th>Address2</th>
                <th>City</th>
                <th>State</th>
                <th>Zipcode</th>
                <th>Policy Type</th>
                <th>Policy Number</th>
                <th>Start Date</th>
                
                <th>Expiry Date</th>
                <th>Days Remaining </th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
               
            </tr>
        </thead>
       
    </table>
                     
            </div>
                         
                             <%-- <div class="row pnl">
                                  <table id="plist" class="col-md-12 display nowrap"  cellspacing="0" width="100%">
        <thead>
            <tr>
                <th></th>
                <th>Type</th>
               <th>Make</th>
                <th>Model</th>
                <th>Serial Number</th>
                <th>Purchase Date</th>
                <th>Warranty Term</th>
                <th>Expiry Date</th>
                <th>Notes</th>
                <%--<th>&nbsp;</th>
               
            </tr>
        </thead>
                                      </table>
                                  </div>--%>

						</div>
			
					</div>
				</div>
        
    	<div class="card mb-5 mb-xl-10" id="pnlDocuments" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_deactivate" aria-expanded="true" aria-controls="kt_account_deactivate">
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Documents</h3>
									</div>
								</div>
								<!--end::Card header-->
								<!--begin::Content-->
								<div id="11kt_account_deactivate" class="collapse show">
									<!--begin::Form-->
								<%--	<form id="kt_account_deactivate_form" class="form">--%>
										<!--begin::Card body-->
										<div class="card-body border-top p-9">

											<div class="row mb-6">
												<!--begin::Label-->
												<%--<label class="col-lg-4 col-form-label required fw-bold fs-6"></label>--%>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-10 fv-row fv-plugins-icon-container">
													 <div class="dropzone dropzone-queue mb-2" id="kt_dropzonejs_example_3">
                <!--begin::Controls-->
                <div class="dropzone-panel mb-lg-0 mb-2">
                    <a class="dropzone-select btn btn-sm btn-bg-light me-2">Drop files here or click to upload</a>
                    <a class="dropzone-remove-all btn btn-sm btn-light-primary">Remove All</a>
                </div>
                <!--end::Controls-->

                <!--begin::Items-->
                <div class="dropzone-items wm-200px">
                    <div class="dropzone-item" style="display:none">
                        <!--begin::File-->
                        <div class="dropzone-file">
                            <div class="dropzone-filename" title="some_image_file_name.jpg">
                                <span data-dz-name>some_image_file_name.jpg</span>
                                <strong>(<span data-dz-size>340kb</span>)</strong>
                            </div>

                            <div class="dropzone-error" data-dz-errormessage></div>
                        </div>
                        <!--end::File-->

                        <!--begin::Progress-->
                        <div class="dropzone-progress">
                            <div class="progress">
                                <div
                                    class="progress-bar bg-primary"
                                    role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0" data-dz-uploadprogress>
                                </div>
                            </div>
                        </div>
                        <!--end::Progress-->

                        <!--begin::Toolbar-->
                        <div class="dropzone-toolbar">
                            <span class="dropzone-delete" data-dz-remove><i class="bi bi-x fs-1"></i></span>
                        </div>
                        <!--end::Toolbar-->
                    </div>
                </div>
                <!--end::Items-->
           <%-- </div>
            <!--end::Dropzone-->

            <!--begin::Hint-->
            <span class="form-text text-muted">Max file size is 1MB and max number of files is 5.</span>
            <!--end::Hint-->
        </div>       --%>
                                                         
                                                         </div>
											

												</div>
												<div class="col-lg-4">
													<%--<asp:Button ID="Button1" runat="server" SkinID="btnDefault" OnClick="btnAddDenimination_Click" />--%>
													</div>
												<!--end::Col-->
											</div>


											<div class="row mb-6">
												  <asp:GridView ID="gridfiles" runat="server" AutoGenerateColumns="false" OnRowCommand="gridfiles_RowCommand" >
                       <Columns>
                       <asp:BoundField DataField="Text" HeaderText="File Name" Visible="false"  />
                           <asp:TemplateField HeaderText="File Name">
                               <ItemTemplate>
                                   <asp:LinkButton ID="btnDownload" runat="server" CommandName="Download" CommandArgument='<%# Eval("Text") %>' Text='<%# Eval("Text") %>'></asp:LinkButton>
                               </ItemTemplate>
                           </asp:TemplateField>
                      <asp:TemplateField ItemStyle-Width="30px">
                           <ItemTemplate>
                            <%-- <asp:LinkButton ID = "lnkDelete" OnClick = "DeleteFile" CausesValidation="false" 
                                 Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server"></asp:LinkButton>--%>
                     <asp:LinkButton runat="server" ID="lnkDelete" CausesValidation="false" SkinID="BtnLinkDelete"
                               CommandArgument = '<%# Eval("Value") %>'
                          OnClientClick="return confirm('Do you want to delete the record?');" OnClick="DeleteFile"></asp:LinkButton>
                           </ItemTemplate>
                      </asp:TemplateField>
                 </Columns>
                </asp:GridView>
												</div>

										
													
</div>
													
												</div>
												<!--end::Wrapper-->
											
											<!--end::Notice-->
											<!--begin::Form input row-->
										<%--	<div class="form-check form-check-solid fv-row">
												<input name="deactivate" class="form-check-input" type="checkbox" value="" id="deactivate" />
												<label class="form-check-label fw-bold ps-2 fs-6" for="deactivate">I confirm my account deactivation</label>
											</div>--%>
											<!--end::Form input row-->
										
										<!--end::Card body-->
										<!--begin::Card footer-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
										<%--	<asp:Button ID="Button2" runat="server" CausesValidation="false" OnClick="btnUpdateSkill_Click" Text="Update" />--%>
											<%--<button id="kt_account_deactivate_account_submit" type="submit" class="btn btn-danger fw-bold" runat="server" onclick="">Deactivate Account</button>--%>
										</div>
										<!--end::Card footer-->
									<%--</form>--%>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							

							<div class="card" id="pnlDeactive" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_deactivate" aria-expanded="true" aria-controls="kt_account_deactivate">
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Deactivate Account</h3>
									</div>
								</div>
								<!--end::Card header-->
								<!--begin::Content-->
								<div id="1kt_account_deactivate" class="collapse show">
									<!--begin::Form-->
								<%--	<form id="kt_account_deactivate_form" class="form">--%>
										<!--begin::Card body-->
										<div class="card-body border-top p-9">
											<!--begin::Notice-->
											<div class="notice d-flex bg-light-warning rounded border-warning border border-dashed mb-9 p-6">
												<!--begin::Icon-->
												<!--begin::Svg Icon | path: icons/duotune/general/gen044.svg-->
												<span class="svg-icon svg-icon-2tx svg-icon-warning me-4">
													<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
														<rect opacity="0.3" x="2" y="2" width="20" height="20" rx="10" fill="black" />
														<rect x="11" y="14" width="7" height="2" rx="1" transform="rotate(-90 11 14)" fill="black" />
														<rect x="11" y="17" width="2" height="2" rx="1" transform="rotate(-90 11 17)" fill="black" />
													</svg>
												</span>
												<!--end::Svg Icon-->
												<!--end::Icon-->
												<!--begin::Wrapper-->
												<div class="d-flex flex-stack flex-grow-1">
													<!--begin::Content-->
													<div class="fw-bold">
														<h4 class="text-gray-900 fw-bolder">You Are Deactivating Your Account</h4>
														<div class="fs-6 text-gray-700">For extra security, this requires you to confirm your email or phone number when you reset yousignr password.
														<br />
														<a class="fw-bolder" href="#">Learn more</a></div>
													</div>
													<!--end::Content-->
												</div>
												<!--end::Wrapper-->
											</div>
											<!--end::Notice-->
											<!--begin::Form input row-->
											<div class="form-check form-check-solid fv-row">
												<input name="deactivate" class="form-check-input" type="checkbox" value="" id="deactivate" />
												<label class="form-check-label fw-bold ps-2 fs-6" for="deactivate">I confirm my account deactivation</label>
											</div>
											<!--end::Form input row-->
										</div>
										<!--end::Card body-->
										<!--begin::Card footer-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<asp:Button ID="btnDeativate" runat="server" CssClass="btn btn-danger fw-bold" CausesValidation="false" OnClick="btnDeactivate_Click" Text="Deactivate Account" />
											<%--<button id="kt_account_deactivate_account_submit" type="submit" class="btn btn-danger fw-bold" runat="server" onclick="">Deactivate Account</button>--%>
										</div>
										<!--end::Card footer-->
									<%--</form>--%>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>
        </asp:Panel>
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
   
    <%--<link rel="stylesheet" type="text/css" href="/Web/examples/resources/syntax/shCore.css">--%>
   <%-- <link rel="stylesheet" type="text/css" href="/Web/examples/resources/demo.css">--%>
     
   <%--<%-- <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
    </script>
   
     
    <script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">

    </script>
     <script type="text/javascript"  src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js">
    </script>
    <script src="https://cdn.datatables.net/plug-ins/1.10.15/sorting/datetime-moment.js"></script>
    
     <script type="text/javascript" src="https://cdn.datatables.net/plug-ins/1.10.16/dataRender/datetime.js">
    </script>
   
    <script type="text/javascript"  src="https://cdn.datatables.net/buttons/1.4.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="https://cdn.datatables.net/select/1.2.2/js/dataTables.select.min.js">
    </script>
    <script type="text/javascript" src="/web/js/dataTables.editor.min.js">
    </script>--%>
 
    
    <style type="text/css" class="init">
        div.dataTables_wrapper {
        /*width: 800px;*/
        margin: 0 auto;
        
    }
        div.dt-buttons{
            float:right;
        }
    </style>
  <%--  <script type="text/javascript" language="javascript" src="/web/examples/resources/syntax/shCore.js">
    </script>--%>
    <%--<script type="text/javascript" language="javascript" src="/Web/examples/resources/demo.js">
    </script>--%>
    <%--<script type="text/javascript" language="javascript" src="/Web/examples/resources/editor-demo.js">
    </script>--%>

    <script>
        //$(window).on('load', function () {
        //    $("#MainContent_MainContent_pnlUploadtemplate").each(function (index) {

        //        $(this).parent('div').css("overflow-x", "hidden");
        //    });
        //});
        //$(window).load(function () {
        //    $('#MainContent_MainContent_pnlUploadtemplate').closest('div').css("overflow-y", "visible");
        //});
        //$('#MainContent_MainContent_pnlUploadtemplate').closest('div').css("overflow-y", "visible");
        //MainContent_MainContent_pnlUploadtemplate
    </script>
     <script type="text/javascript">
         //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
         //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setLable);
         //setStatusBackColor();
         //setLable();
         //DTE_Field DTE_Field_Type_text DTE_Field_Name_PortfolioContactAddress.Amount
         //DTE_Field DTE_Field_Type_text DTE_Field_Name_PortfolioContactAddress.BillingName
         //$(window).load(function () {
         //    $('.statuscls1').each(function () {
                 
         //        var s = $(this).html();
         //        if (s == 'Expired')
                     
         //        $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle", "color": "white", "font-weight": "bold" });

         //    });
             
         //    //$('.DTE_Field').each(function () {
             
         //    //    $(this).appendTo('<div>Billing Information</div>');
         //    //});
             
         //});

         function setStatusBackColor() {

             //$('.statuscls').each(function () {
                 
             //    var s = $(this).html();
             //    if (s == 'Expired')
             //        $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle", "color": "white", "font-weight": "bold" });

             //});

         }

         function setLable()
         {
             var i =0;
             $('.DTE_Field').each(function () {
                 i = i + 1;
                 
                 if (i == 15) {
                     if ($("p").hasClass("bclass").toString() == "false")
                         $(this).prepend('<p> <input type="checkbox" name="cinfo" value="1" class="cinfo" onclick="cinfoclick();"> Copy Home Information </p><p class="bclass" style="font-size: medium;">Billing Information</p><hr>');
                 }
             });
         }

         function cinfoclick()
         {

             $('#DTE_Field_PortfolioContactAddress-BillingAddress1').val($('#DTE_Field_PortfolioContactAddress-Address').val());
             $('#DTE_Field_PortfolioContactAddress-BillingAddress2').val($('#DTE_Field_PortfolioContactAddress-Address2').val());
             $('#DTE_Field_PortfolioContactAddress-BillingCity').val($('#DTE_Field_PortfolioContactAddress-City').val());
             $('#DTE_Field_PortfolioContactAddress-BillingState').val($('#DTE_Field_PortfolioContactAddress-State').val());
             $('#DTE_Field_PortfolioContactAddress-BillingZipcode').val($('#DTE_Field_PortfolioContactAddress-PostCode').val());
             //alert('checked');
             return false;
         }
        

</script>



    
</asp:Content>
