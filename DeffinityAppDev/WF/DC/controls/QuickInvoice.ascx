<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuickInvoice.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.QuickInvoice" %>
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

<asp:UpdatePanel ID="uPanelInvoice" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <link href="../../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../../assets/plugins/global/plugins.bundle.js"></script>
   

<script language="JavaScript">
         function showMe() {
             var mytoken = document.getElementById('mytoken');
             alert("Token=" + mytoken.value);
         }


         </script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {

            var i = 1,
                $example_dropzone_filetable = $("#example-dropzone-filetable_new"),
                example_dropzone = $("#advancedDropzone_new").dropzone({
                    url: 'UploadHandler.ashx?callid=0&qid=' + $('#huid').val(),

                    // Events
                    addedfile: function (file) {
                        if (i == 1) {
                            $example_dropzone_filetable.find('tbody').html('');
                        }

                        var size = parseInt(file.size / 1024, 10);
                        size = size < 1024 ? (size + " KB") : (parseInt(size / 1024, 10) + " MB");

                        var $el = $('<tr>\
													<td class="text-center">'+ (i++) + '</td>\
													<td>'+ file.name + '</td>\
													<td><div class="progress progress-striped"><div class="progress-bar progress-bar-warning"></div></div></td>\
													<td>'+ size + '</td>\
													<td>Uploading...</td>\
												</tr>');

                        $example_dropzone_filetable.find('tbody').append($el);
                        file.fileEntryTd = $el;
                        file.progressBar = $el.find('.progress-bar');
                    },

                    uploadprogress: function (file, progress, bytesSent) {
                        file.progressBar.width(progress + '%');
                    },

                    success: function (file) {
                        file.fileEntryTd.find('td:last').html('<span class="text-success">Uploaded</span>');
                        file.progressBar.removeClass('progress-bar-warning').addClass('progress-bar-success');
                    },

                    error: function (file) {
                        file.fileEntryTd.find('td:last').html('<span class="text-danger">Failed</span>');
                        file.progressBar.removeClass('progress-bar-warning').addClass('progress-bar-red');
                    }
                });

            $("#advancedDropzone_new").css({
                minHeight: 200
            });

        });
    </script>
    <script>
        $(document).ready(function () {
            $('#btnApply').click(function (e) {
                
                //alert('test');
                //var url = "http://123smartpro.com/index.php/cardconnectapplicationform/";
                var url = "https://apply.cardpointe.com/arcnd";
                window.open(url, '_blank');
                return false;
            });
        }
        );
    </script>


<div class="card card-bordered">
	<div class="card-header align-items-center border-0 mt-0">
												<h3 class="card-title">
													<span class="fw-bolder mb-2 text-dark"> <asp:Literal ID="lblheader" runat="server" Text=""></asp:Literal> </span>
													<%--<span class="text-muted fw-bold fs-7">Count <asp:Literal ID="lblCount" runat="server"></asp:Literal>--%>
												</h3>

		</div>
													
													<div class="card-body" style="min-height:680px;overflow-x:scroll;">

                                                        <asp:Panel ID="pnlwelcome" runat="server">
                                                            <div class="card-px text-center py-0 my-10">
											<!--begin::Title-->
											<h2 class="fs-2x fw-bolder mb-10">Take a Payment</h2>
											<!--end::Title-->
											<!--begin::Description-->
											<p class="text-gray-400 fs-4 fw-bold mb-10">Use this section to process a payment. Your client will receive 
an invoice once the payment has been processed 
successfully.</p>
											<!--end::Description-->
											<!--begin::Action-->
                                                                <asp:Button ID="btnBegin" runat="server" Text="Let’s Begin" SkinID="btnDefault" OnClick="btnNext_Click" />
											<%--<a href="#" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#kt_modal_add_customer">Let’s Begin</a>--%>
											<!--end::Action-->
										</div>
                                                            <div class="text-center px-4">
											<img class="mw-100 mh-300px" alt="" src="../../assets/media/illustrations/sketchy-1/4.png">
										</div>

                                                        </asp:Panel>


                                                         <asp:Panel ID="pnlClientDetails" runat="server" Visible="false">
                                                             <%-- <div class="form-group row">
    <div class="col-md-12 text-bold">
        <strong> <%: Resources.DeffinityRes.ClientDetails %> </strong>
        <hr class="no-top-margin" />
    </div>
</div>--%>
      <div class="form-group row" >
      
         <div class="form-group row">
          <div class="col-md-8">
              <asp:Label ID="lblCardERROR" runat="server" EnableViewState="false"></asp:Label>
              </div>
             </div>
         <div class="form-group row mb-6">
      <div class="col-md-12">
           <label class="col-sm-12 control-label"> <%: Resources.DeffinityRes.Client %> </label>
           <div class="col-sm-12 form-inline">
               <asp:TextBox ID="txtContactName" runat="server" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtContactName"  ErrorMessage="Please enter client"  Display="None" ValidationGroup="p"></asp:RequiredFieldValidator>
               <asp:DropDownList ID="ddlContact" runat="server" SkinID="ddl_70" Visible="false"></asp:DropDownList> <asp:Button ID="btnRunserver" runat="server" Text="<%$ Resources:DeffinityRes, AddClient%>" Visible="false" />
              <asp:HiddenField ID="hContact" runat="server" />
             <%--  <asp:RequiredFieldValidator ID="rfContact" runat="server" ControlToValidate="ddlContact" InitialValue="0" ErrorMessage="Please select client"  Display="None" ValidationGroup="p"></asp:RequiredFieldValidator>--%>
            </div>
	</div>
             </div>
                     <div class="form-group row mb-6">
      <div class="col-md-12">
           <label class="col-sm-12 control-label"> <%: Resources.DeffinityRes.Email %> </label>
           <div class="col-sm-12 form-inline">
               <asp:TextBox ID="txtContactEmail" runat="server" MaxLength="300"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtContactEmail"  ErrorMessage="Please enter client email"  Display="None" ValidationGroup="p"></asp:RequiredFieldValidator>
               
            </div>
	</div>
             </div>
         <div class="form-group row" style="display:none;">
      <div class="col-md-12">
           <label class="col-sm-1 control-label"> <%: Resources.DeffinityRes.Address %> </label>
           <div class="col-sm-10">
               <asp:DropDownList ID="ddlAddress" runat="server"></asp:DropDownList>
              <asp:HiddenField ID="hAddress" runat="server" />
               <%--<asp:RequiredFieldValidator ID="rfAddress" runat="server" ControlToValidate="ddlAddress" InitialValue="0" ErrorMessage="Please select address" Display="None" ValidationGroup="p"></asp:RequiredFieldValidator>--%>
            </div>
	</div>
             </div>
                     <div class="form-group row mb-6">
      <div class="col-md-12">
           <label class="col-sm-12 control-label"> <%: Resources.DeffinityRes.Details %> </label>
           <div class="col-sm-12">
               <asp:TextBox ID="txtDetails" runat="server" SkinID="txtMulti_150" TextMode="MultiLine" Text="Carry out work as agreed"></asp:TextBox>
               <asp:RequiredFieldValidator ID="rfDetails" runat="server" ControlToValidate="txtDetails" ErrorMessage="Please enter details" Display="None" ValidationGroup="p"></asp:RequiredFieldValidator>
           
            </div>
	</div>
             </div>
                    <div class="form-group row  mb-6">
          <div class="col-md-12">
 <label class="col-sm-12 control-label"><%: Resources.DeffinityRes.Price %></label>
           <div class="col-sm-12">
                <asp:TextBox ID="txtAmount" runat="server" SkinID="Price_150px" 
                            Width="150px" >0.00</asp:TextBox>
            </div>
	</div>
</div>

         
          </div>
                   <div class="form-group row">
      <div class="col-md-12">
           <label class="col-sm-12 control-label"> </label>
           <div class="col-sm-12">
               <asp:Button ID="btnSaveQUote" runat="server" SkinID="btnDefault" Text="Send Quick Quote" style="width:100%" OnClick="btnSubmitQuote_Click" Visible="false"  />
               </div>
          </div>
                   </div>

                                                             </asp:Panel>
                                                        <asp:Panel ID="pnlCardDetails" runat="server" Visible="false">
                                                            <div class="form-group row" >
    <div class="form-group row  mb-6" >
  
          <div class="col-md-12">
 <label class="col-sm-12 control-label"><%: Resources.DeffinityRes.CardType %></label>
           <div class="col-sm-12">
               <asp:DropDownList ID="ddlCardType" runat="server" SkinID="ddl_200px">
                            <asp:ListItem></asp:ListItem>
                             <asp:ListItem Value="MASTERCARD" Text="MASTERCARD"></asp:ListItem>
                            <asp:ListItem Selected="True" Value="VISA" Text="VISA"></asp:ListItem>
                   <asp:ListItem Value="DISCOVER" Text="DISCOVER"></asp:ListItem>
                    <asp:ListItem Value="AMEX" Text="AMEX"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="ddlCardType" Display="Dynamic" CssClass="error-text" ErrorMessage="Required" 
                            ></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
<div class="form-group row  mb-6">
          <div class="col-md-12">
 <label class="col-sm-12 control-label"><%: Resources.DeffinityRes.CardNumber %></label>
           <div class="col-sm-12">
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
</div>
    <div class="form-group row  mb-6">
          <div class="col-md-12">
 <label class="col-sm-12 control-label"><%: Resources.DeffinityRes.NameonCard %> </label>
           <div class="col-sm-12">
                <asp:TextBox ID="txtNameOnCard" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_200px" MaxLength="250"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtNameOnCard" Display="None" ErrorMessage="Please enter name on card" ValidationGroup="p"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group row  mb-6">
          <div class="col-md-12">
 <label class="col-sm-12 control-label"><%: Resources.DeffinityRes.Expiration %></label>
           <div class="row">
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
</div>
    <div class="form-group row  mb-6">
          <div class="col-md-12">
 <label class="col-sm-12 control-label"><%: Resources.DeffinityRes.CardSecurityCode %></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtCvv" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_75px" MaxLength="10"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtCvv" Display="None" 
                            ErrorMessage="Please enter CVV" ValidationGroup="p" ></asp:RequiredFieldValidator>
                         <%-- <p>  A code that is printed (not imprinted) on the back of 
                                a credit card. It consist of 3 or 4 digits. </p>--%>
               <p>e.g: 123 </p>
            </div>
	</div>
</div>
       
             </div>
    <div class="form-group row  mb-6">
          <div class="col-md-12">
 <label class="col-sm-12 control-label"></label>
           <div class="col-sm-12form-inline">
             

                <asp:Button ID="btnSubmit" runat="server" Text="<%$ Resources:DeffinityRes, ProcessPayment%>" 
                            onclick="btnSubmit_Click" ValidationGroup="p" style="width:100%" Visible="false" />
                        &nbsp;
                        <asp:Button id="btnCancel" runat="server" SkinID="btnCancel" OnClick="btnCancel_Click" CausesValidation="false" Visible="false" />
            </div>
	</div>
</div>

                                                        </asp:Panel>
                                                         <asp:Panel ID="pnlAttachfiles" runat="server" Visible="false">

                                                             <div class="row mb-6">

                                                                 	<div class="fv-row">
        <!--begin::Dropzone-->
        <div class="dropzone" id="kt_dropzonejs_example_2">
            <!--begin::Message-->
            <div class="dz-message needsclick">
                <!--begin::Icon-->
                <i class="bi bi-file-earmark-arrow-up text-primary fs-3x"></i>
                <!--end::Icon-->

                <!--begin::Info-->
                <div class="ms-4">
                    <h3 class="fs-5 fw-bolder text-gray-900 mb-1">Drop files here or click to upload.</h3>
                    <span class="fs-7 fw-bold text-gray-400">Upload up to 10 files</span>
                </div>
                <!--end::Info-->
            </div>
        </div>
        <!--end::Dropzone-->
    </div>
<script type="text/javascript">
    

      //  $(document).ready(function () {

            pageLoad();




            function pageLoad() {

                //  jQuery(document).ready(function () {


                var myDropzoneNew = new Dropzone("#kt_dropzonejs_example_2", {
                    url: 'UploadHandler.ashx?callid=0&qid=' + $('#huid').val(),
                    paramName: "file", // The name that will be used to transfer the file
                    maxFiles: 10,
                    maxFilesize: 10, // MB
                    addRemoveLinks: true,
                    accept: function (file, done) {
                        if (file.name == "wow.jpg") {
                            done("Naha, you don't.");
                        } else {
                            done();
                        }
                    }
                });
              //   });


                //jquery code and events, event binding.. etc..

                //var id = "#kt_dropzonejs_example_4";

                //// set the preview element template
                //var previewNode = $(id + " .dropzone-item");

                //previewNode.id = "";
                //var previewTemplate = previewNode.parent(".dropzone-items").html();
                //previewNode.remove();

                //var myDropzone = new Dropzone(id, { // Make the whole body a dropzone
                //    url: 'UploadHandler.ashx?callid=0&qid=' + $('#huid').val(), // Set the url for your upload script location
                //    parallelUploads: 20,
                //    maxFilesize: 1, // Max filesize in MB
                //    previewTemplate: previewTemplate,
                //    previewsContainer: id + " .dropzone-items", // Define the container to display the previews
                //    clickable: id + " .dropzone-select" // Define the element that should be used as click trigger to select files.
                //});


                //myDropzone.on("addedfile", function (file) {
                //    // Hookup the start button
                //    $(document).find(id + " .dropzone-item").css("display", "");
                //});

                //// Update the total progress bar
                //myDropzone.on("totaluploadprogress", function (progress) {
                //    $(id + " .progress-bar").css("width", progress + "%");
                //});

                //myDropzone.on("sending", function (file) {
                //    // Show the total progress bar when upload starts
                //    $(id + " .progress-bar").css("opacity", "1");
                //});

                //// Hide the total progress bar when nothing"s uploading anymore
                //myDropzone.on("complete", function (progress) {
                //    var thisProgressBar = id + " .dz-complete";

                //    setTimeout(function () {
                //        $(thisProgressBar + " .progress-bar, " + thisProgressBar + " .progress").css("opacity", "0");
                //    }, 300)
                //});

         //   });
    }
    function getQuerystring(key, default_) {

        if (default_ == null) default_ = "";
        key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
        var qs = regex.exec(window.location.href.toLowerCase());
        if (qs == null)
            return default_;
        else
            return qs[1];
    }
        // set the dropzone container id

</script>
												<!--begin::Label-->
												<%--<label class="col-lg-4 col-form-label required fw-bold fs-6"></label>--%>
												<!--end::Label-->
												<!--begin::Col-->
												<%--<div class="col-lg-12 fv-row fv-plugins-icon-container">
													 <div class="dropzone dropzone-queue mb-2" id="kt_dropzonejs_example_4">
                <!--begin::Controls-->
                <div class="dropzone-panel mb-lg-0 mb-2">
                    <a class="dropzone-select btn btn-sm btn-bg-light me-2" style="width: 100%;height: 100px;vertical-align: middle;font-size: medium;margin-top: 50px;text-align: center;padding-top: 40px;">Drop files here or click to upload</a>
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
        </div>       
                                                         
                                                         </div>
											

												</div>--%>
												<div class="col-lg-4">
													<%--<asp:Button ID="Button1" runat="server" SkinID="btnDefault" OnClick="btnAddDenimination_Click" />--%>
													</div>
												<!--end::Col-->
											</div>
                                                              <%-- <div class="form-group row">
    <div class="col-md-12 text-bold">
        <strong> Attach Documents </strong>
        <hr class="no-top-margin" />
    </div>
</div>--%>
                   <%-- <div class="form-group row">
                         <div class="col-md-12">
                             
					
					<br />
					<div class="row">
						<div class="col-sm-12 text-center">
						
							<div id="advancedDropzone_new" class="droppable-area">
								Drop Files Here
							</div>
							
						</div>
						<div class="col-sm-12">
							
							<table class="table table-bordered table-striped" id="example-dropzone-filetable_new">
								<thead>
									<tr>
										<th width="1%" class="text-center">#</th>
										<th width="50%">Name</th>
										<th width="20%">Upload Progress</th>
										<th>Size</th>
										<th>Status</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td colspan="5">Files list will appear here</td>
									</tr>
								</tbody>
							</table>
							
						</div>
					</div>

                             </div>
                        </div>--%>

                                                             </asp:Panel>

                                                         <asp:Panel ID="pnlResult" runat="server" Visible="false">
           <div class="card-px text-center py-20 my-10">
											<!--begin::Title-->
											<h2 class="fs-2x fw-bolder mb-10">All Done!</h2>
											<!--end::Title-->
											<!--begin::Description-->
											<p class="text-gray-400 fs-4 fw-bold mb-10"><asp:Label ID="lblResultSucess" runat="server"  EnableViewState="false" CssClass="green-alert green-alert-success" Visible="false" style="width:100%"></asp:Label>
               <asp:Label ID="lblResultFail" runat="server" EnableViewState="false" CssClass="red-alert red-alert-danger" Visible="false" style="width:100%"></asp:Label></p>
											<!--end::Description-->
											<!--begin::Action-->
                                                                 <asp:Button ID="btnBackHome" runat="server" Text="Back To Home" SkinID="btnDefault" OnClick="btnBackToHOme_Click"  />
											<%--<a href="#" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#kt_modal_add_customer">Let’s Begin</a>--%>
											<!--end::Action-->
										</div>
                                                            <div class="text-center px-4">
											<img class="mw-100 mh-300px" alt="" src="../../assets/media/illustrations/sketchy-1/4.png">
										</div>


         </asp:Panel>
                                                        </div>

     <div class="card-footer" style="height:90px">
		  <asp:Button ID="btnBack" runat="server" SkinID="btnDefault" Text="Back" OnClick="btnBack_Click" style="float:left;" Visible="false" />
     <asp:Button ID="Button2" runat="server" SkinID="btnDefault" Text="Send Quick Quote" style="width:100%" OnClick="btnSubmitQuote_Click" Visible="false"  />
                <asp:Button ID="btnNext" runat="server" SkinID="btnDefault" Text="Next" OnClick="btnNext_Click" style="float:right;" Visible="false" />
            </div>
    </div>



<asp:HiddenField ID="huid" runat="server" ClientIDMode="Static" />
    <div class="form-group row">
                                        <div class="col-md-12">
                                            <div class="col-sm-12 " id="pnltakepayment" runat="server">
                                                
   
                            <div class="row">
                                     
                                   <div class="form-group row">
      <div class="col-md-12">
           <asp:Panel ID="pnlPaymentDetails" runat="server">
               <div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
               <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
              <asp:ValidationSummary ID="pSUm" runat="server" ValidationGroup="p" />
              </div>
         </div>
                <div class="form-group row">
          <div class="row">

               <div class="col-md-4">
                   
                   </div>
               <div class="col-md-4">
    
<%--
        <div class="form-group row" >
    <div class="col-md-12 text-bold">
        <strong> <%: Resources.DeffinityRes.CardDetails %> </strong>
        <hr class="no-top-margin" />
    </div>
</div>--%>
         

                   </div>


                <div class="col-md-4">
                       
                    </div>

              </div>
                    </div>
        </asp:Panel>
       
     



     <script type="text/javascript">
         $(function () {

             SetContactContactData();
             SetAddressContactData();

             $("[id*=ddlContact]").change(function () {
                 $("[id*=hContact").val($(this).val());

                 SetAddressContactData();

             });
         });

         function setContactDropdownValue() {
             if ($("[id*=hContact").val() != '') {
                 $("[id*=ddlContact]").val($("[id*=hContact").val());
             }
         }
         function SetContactContactData() {
             //var id = $("[id$='hcid']").val();

             //if (id == "")
             //    id = "0";
             $.ajax({
                 type: "POST",
                 url: "../../WF/DC/webservices/DCServices.asmx/GetContacts",
                 //data: "{id:'" + id + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 async: false,
                 success: function (r) {
                     var ddlCustomers = $("[id*=ddlContact]");
                     debugger;
                     ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                     $.each(r.d, function () {
                         ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                     });
                     $("[id*=hAddress]").val('0');
                     setContactDropdownValue();
                 }
             });
         }



         function SetdropdownsValue() {
             if ($("[id*=hContact]").val().trim() != "") {
                 $("[id*=ddlContact] option").each(function () {
                     if ($(this).val() == $("[id*=hContact]").val()) {
                         $(this).attr('selected', 'selected');
                     }
                 });
             }
         }
</script>
    <script type="text/javascript">
        $(function () {

            //SetAddressContactData();
            $("[id*=ddlAddress]").change(function () {
                $("[id*=hAddress").val($(this).val());
               // SetSubAddressContactData();
            });
        });

        function setAddressDropdownValue() {
            if ($("[id*=hAddress").val() != '') {
                $("[id*=ddlAddress]").val($("[id*=hAddress").val());
            }
        }
        function SetAddressContactData() {
            var id = $("[id*=ddlContact]").val();
            if (id == null)
                id = "0";
            if (id == "")
                id = "0";
            debugger;
            if (id != "0") {
                $.ajax({
                    type: "POST",
                    url: "../../WF/DC/webservices/DCServices.asmx/GetContactsAddress",
                    data: "{id:'" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (r) {
                        var ddlCustomers = $("[id*=ddlAddress]");
                        debugger;
                        ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(r.d, function () {
                            ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                        $("[id*=hAddress]").val('0');
                        setAddressDropdownValue();
                    }
                });
            }
            else {
                var ddlCustomers = $("[id*=ddlAddress]");
                debugger;
                ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        }



        function SetdropdownsValue() {
            if ($("[id*=hAddress]").val().trim() != "") {
                $("[id*=ddlAddress] option").each(function () {
                    if ($(this).val() == $("[id*=hAddress]").val()) {
                        $(this).attr('selected', 'selected');
                    }
                });
            }
        }
    </script>
        
         
          </div>
                                        
              </div>
                                </div>
                
                                            </div>
                                            
                                            </div>
             </div>
<div class="form-group row">
                                        <div class="col-md-12">
<div class="col-sm-4 form-inline" id="pnlccd" runat="server" visible="false">


                                                <div class="panel panel-color panel-info">
						<div class="card-header">
							<h3 class="panel-title form-inline">Don't have a Card Connect Account?
                               </h3>
							<div class="card-toolbar">

                              


							</div>
						</div>
						<div class="panel-body">
                            <div class="row">
                                    
                                  
                               
                               
 <div class="form-group row" style="padding-bottom:25px;">
      <div class="col-md-12">
          <label class="col-sm-12 control-label"> <lable style="font-size:16px">Apply now for the lowest payment processing fees Nationwide!</lable> </label>
          </div>
     </div>


                                <div class="form-group row" style="padding-bottom:25px;font-size:16px">
      <div class="col-md-12">
          <ul>
              <li>Lowest cost payment processing</li>
              <li>Beats Stripe, Cube, Intuit, and many other providers</li>
              <li>Turns your mobile into a touchless terminal</li>
          </ul>
          </div>
     </div>
                                <div class="form-group row" style="width:100%">
      <div class="col-md-12" style="text-align:center;">
    
          <asp:Button ID="btnWatch" runat="server" Text="Watch Video" CssClass="btn btn-blue"  /> <asp:Button ID="btnApply" runat="server" Text="Apply Now" ClientIDMode="Static"  />
          </div>
     </div>

                                </div>
                </div>
            </div>


                                                </div>
                                            </div>
    </div>
      <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnWatch" PopupControlID="pnlManagePassword" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlManagePassword" runat="server" BackColor="White" Style="display:none;"
                       Width="680px" Height="480px" CssClass="panel panel-color panel-info" ScrollBars="None">
         

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text=""></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="panel-body">
        <div class="form-group row">
                   <div class="col-md-12 form-inline">

                       <iframe id="viframe" runat="server" height="340" width="600" style="border:none;" src="https://player.vimeo.com/video/447483488"></iframe>
                       
                       </div>
            </div>
 
      
        
           
        </div>
                  
           </asp:Panel>

         </ContentTemplate>

</asp:UpdatePanel>


   <!-- Imported styles on this page -->
	<link rel="stylesheet" href="../../../Content/assets/js/dropzone/css/dropzone.css">

	<!-- Imported scripts on this page -->
	<script src="../../../Content/assets/js/dropzone/dropzone.min.js"></script>