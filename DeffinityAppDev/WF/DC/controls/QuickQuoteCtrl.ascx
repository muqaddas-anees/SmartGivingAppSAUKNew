<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuickQuoteCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.QuickQuoteCtrl" %>
<%--<asp:UpdatePanel ID="updatepanelQuote" runat="server" UpdateMode="Conditional">
    <ContentTemplate>--%>
          <%--<link href="../../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../../assets/plugins/global/plugins.bundle.js"></script>--%>

 <link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css")%>' rel="stylesheet" type="text/css" />
        
<script src='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.js")%>'></script>


<asp:HiddenField ID="hquotestep" runat="server" Value="0" ClientIDMode="Static" />
<div class="card card-bordered">
	<div class="card-header align-items-center border-0 mt-0">
												<h3 class="card-title">
													<span class="fw-bolder mb-2 text-dark"> <asp:Label ID="lblquoteheader" runat="server" Text=""></asp:Label> </span>
													<%--<span class="text-muted fw-bold fs-7">Count <asp:Literal ID="lblCount" runat="server"></asp:Literal>--%>
												</h3>

		</div>
													
													<div class="card-body" style="min-height:680px;overflow-x:scroll;">

                                                        <asp:Panel ID="pnlQuotewelcomeQuote" runat="server">
                                                            <div class="card-px text-center py-0 my-10">
											<!--begin::Title-->
											<h2 class="fs-2x fw-bolder mb-10">Send a Quotation</h2>
											<!--end::Title-->
											<!--begin::Description-->
											<p class="text-gray-400 fs-4 fw-bold mb-10">Use this section to send your customer a quick quotation. You 
can even attach a document. You’ll be notified when your 
customer approves the quote so you can plan your schedule.</p>
											<!--end::Description-->
											<!--begin::Action-->
                                                                <asp:Button ID="btnQuoteBegin" runat="server" Text="Let’s Begin" SkinID="btnDefault" />
											<%--<a href="#" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#kt_modal_add_customer">Let’s Begin</a>--%>
											<!--end::Action-->
										</div>
                                                            <div class="text-center px-4">
											<img class="mw-100 mh-300px" alt="" src="../../assets/media/illustrations/sketchy-1/2.png">
										</div>

                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlQuoteClientDetails" runat="server" >
                                                           
                    <%--<div class="form-group row">
    <div class="col-md-12 text-bold">
        <strong> <%: Resources.DeffinityRes.ClientDetails %> </strong>
        <hr class="no-top-margin" />
    </div>
</div>--%>
      <div class="form-group row" >
      
         <div class="form-group row mb-6">
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
                    <div class="form-group row mb-6">
          <div class="col-md-12">
 <label class="col-sm-12 control-label"><%: Resources.DeffinityRes.Price %></label>
           <div class="col-sm-12">
                <asp:TextBox ID="txtAmount" runat="server" SkinID="Price_150px" 
                            Width="150px" >0.00</asp:TextBox>
            </div>
	</div>
</div>

         
          </div>
                  
                  


                                                            </asp:Panel>

                                                         <asp:Panel ID="pnlQuoteAttachfiles" runat="server">
                                                              <%--<div class="form-group row">
    <div class="col-md-12 text-bold">
        <strong> Attach Documents </strong>
        <hr class="no-top-margin" />
    </div>
</div>--%>
                                                           
                                                             <div class="row mb-6">
												<div class="fv-row">
        <!--begin::Dropzone-->
        <div class="dropzone" id="kt_dropzonejs_example_1">
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
											</div>
               
                                                              
                                                             </asp:Panel>

                                                        <asp:Panel ID="pnlQuoteResult" runat="server">

                                                            <div class="card-px text-center py-20 my-10">
											<!--begin::Title-->
											<h2 class="fs-2x fw-bolder mb-10">All Done!</h2>
											<!--end::Title-->
											<!--begin::Description-->
											<p class="text-gray-400 fs-4 fw-bold mb-10"><asp:Label ID="lblResultSucess" runat="server"  EnableViewState="false" CssClass="green-alert green-alert-success" Visible="false" style="width:100%"></asp:Label>
               <asp:Label ID="lblResultFail" runat="server" EnableViewState="false" CssClass="red-alert red-alert-danger" Visible="false" style="width:100%"></asp:Label></p>
											<!--end::Description-->
											<!--begin::Action-->
                                                                 <asp:Button ID="btnQuoteBackHome" runat="server" Text="Back To Home" SkinID="btnDefault"   />
											<%--<a href="#" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#kt_modal_add_customer">Let’s Begin</a>--%>
											<!--end::Action-->
										</div>
                                                            <div class="text-center px-4">
											<img class="mw-100 mh-300px" alt="" src="../../assets/media/illustrations/sketchy-1/2.png">
										</div>



         
         </asp:Panel>
                                                        </div>


 <div class="card-footer" style="height:90px">
		  <asp:Button ID="btnQuoteBack" runat="server" SkinID="btnDefault" Text="Back" OnClick="btnBack_Click" style="float:left;" ClientIDMode="Static"/>
     <asp:Button ID="btnSaveQUote" runat="server" SkinID="btnDefault" Text="Send Quick Quote" style="width:100%" OnClick="btnSubmitQuote_Click" Visible="false"  />
                <asp:Button ID="btnQuoteNext" runat="server" SkinID="btnDefault" Text="Next" OnClick="btnQuoteNext_Click" style="float:right;"  ClientIDMode="Static" />
      <asp:Button ID="btnFinal" runat="server" SkinID="btnDefault" Text="Next" OnClick="btnQuoteNext_Click" style="float:right;"  ClientIDMode="Static" CausesValidation="false" />
            </div>

</div>



<asp:HiddenField ID="huid" runat="server" ClientIDMode="Static" />
    
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>

<script>
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
    var id = "#kt_dropzonejs_example_3";

    // set the preview element template
    var previewNode = $(id + " .dropzone-item");

    previewNode.id = "";
    var previewTemplate = previewNode.parent(".dropzone-items").html();
    previewNode.remove();

    var myDropzone = new Dropzone(id, { // Make the whole body a dropzone
        url: "UploadHandler.ashx?mid=" + getQuerystring('mid'), // Set the url for your upload script location
        parallelUploads: 20,
        maxFilesize: 1, // Max filesize in MB
        previewTemplate: previewTemplate,
        previewsContainer: id + " .dropzone-items", // Define the container to display the previews
        clickable: id + " .dropzone-select" // Define the element that should be used as click trigger to select files.
    });


    myDropzone.on("addedfile", function (file) {
        // Hookup the start button
        $(document).find(id + " .dropzone-item").css("display", "");
    });

    // Update the total progress bar
    myDropzone.on("totaluploadprogress", function (progress) {
        $(id + " .progress-bar").css("width", progress + "%");
    });

    myDropzone.on("sending", function (file) {
        // Show the total progress bar when upload starts
        $(id + " .progress-bar").css("opacity", "1");
    });

    // Hide the total progress bar when nothing"s uploading anymore
    myDropzone.on("complete", function (progress) {
        var thisProgressBar = id + " .dz-complete";

        setTimeout(function () {
            $(thisProgressBar + " .progress-bar, " + thisProgressBar + " .progress").css("opacity", "0");
        }, 300)
    });
</script>
<%--
<script src='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.js")%>'></script>--%>
                                                             <script type="text/javascript">

                                                                 var stepquote = parseInt($("[id*=hquotestep]").val());
                                                                 $(document).ready(function () {
                                                                     //$("[id*=pnlQuotewelcome]").show();
                                                                     pnlshow(stepquote);
                                                                     $("[id*=btnQuoteBegin]").click(function (e) {
                                                                         //alert('button');
                                                                         // e.preventDefault();
                                                                         stepquote = stepquote + 1;
                                                                         console.log('Next stepquote:' + stepquote);
                                                                         pnlshow(stepquote);

                                                                         return false;
                                                                     });
                                                                     $("[id*=btnQuoteBack]").click(function (e) {
                                                                         //alert('button');
                                                                         // e.preventDefault();
                                                                         stepquote = stepquote - 1;
                                                                         console.log('Back stepquote:' + stepquote);
                                                                         pnlshow(stepquote);

                                                                         return false;
                                                                     });
                                                                     $("[id*=btnQuoteNext]").click(function (e) {
                                                                         //alert('button');
                                                                          e.preventDefault();
                                                                         stepquote = stepquote + 1;
                                                                         console.log('Next stepquote:' + stepquote);
                                                                        
                                                                         //debugger;
                                                                         debugger;
                                                                         if (stepquote == 3) {
                                                                             debugger;
                                                                             return true;
                                                                         }
                                                                         else {
                                                                             pnlshow(stepquote);
                                                                             return false;

                                                                         }
                                                                     });

                                                                     $("[id*=btnFinal]").click(function (e) {
                                                                         //alert('button');
                                                                         return true;
                                                                     });

                                                                     //btnQuoteBackHome
                                                                     $("[id*=btnQuoteBackHome]").click(function (e) {
                                                                         stepquote = 0;
                                                                         pnlshow(stepquote);
                                                                         return false;
                                                                     });

                                                                     function pnlshow(sval) {
                                                                         $("[id*=pnlQuotewelcome]").hide();
                                                                         $("[id*=lblquoteheader]").html('');
                                                                         $("[id*=hquotestep]").val(sval);
                                                                         $("[id*=pnlQuoteClientDetails]").hide();
                                                                         $("[id*=pnlQuoteAttachfiles]").hide();
                                                                         $("[id*=pnlQuoteResult]").hide();
                                                                       
                                                                         $("[id*=btnQuoteBack]").hide();
                                                                         $("[id*=btnQuoteNext]").hide();
                                                                         $("[id*=btnFinal]").hide();
                                                                         //btnFinal

                                                                         $("[id*=btnQuoteNext]").html('Next');
                                                                         if (sval == 0) {
                                                                             // $("[id*=lblquoteheader]").html('Client Details');
                                                                             $("[id*=pnlQuotewelcome]").show();
                                                                             // $("[id*=pnlQuoteClientDetails]").show();
                                                                             $("[id*=btnQuoteBack]").hide();
                                                                             $("[id*=btnQuoteNext]").hide();
                                                                         }
                                                                         if (sval == 1) {
                                                                             $("[id*=lblquoteheader]").html('Client Details');
                                                                             $("[id*=pnlQuotewelcome]").hide();
                                                                             $("[id*=pnlQuoteClientDetails]").show();
                                                                             $("[id*=btnQuoteBack]").show();
                                                                             $("[id*=btnQuoteNext]").show();
                                                                         }
                                                                        
                                                                         if (sval == 2) {
                                                                             $("[id*=lblquoteheader]").html('Attach Files');
                                                                             $("[id*=pnlQuoteClientDetails]").hide();
                                                                             $("[id*=pnlQuoteAttachfiles]").show();
                                                                             $("[id*=btnQuoteBack]").show();
                                                                             $("[id*=btnQuoteNext]").hide();
                                                                             $("[id*=btnFinal]").val('Send Quick Quote');
                                                                             $("[id*=btnFinal]").show();
                                                                         }

                                                                         if (sval == 3) {
                                                                          //   $("[id*=lblquoteheader]").html('Attach Files');
                                                                             $("[id*=pnlQuoteAttachfiles]").hide();
                                                                             $("[id*=pnlQuoteResult]").show();
                                                                             $("[id*=btnQuoteBackHome]").show();
                                                                            // $("[id*=pnlQuoteAttachfiles]").show();
                                                                            // $("[id*=btnQuoteBack]").show();
                                                                            // $("[id*=btnQuoteNext]").show();
                                                                            // $("[id*=btnQuoteNext]").val('Send Quick Quote');
                                                                         }
                                                                     }
                                                                 });


                                                                 pageLoad();

                                                                 function pageLoad() {

                                                                     //  jQuery(document).ready(function () {


                                                                     var myDropzone1 = new Dropzone("#kt_dropzonejs_example_1", {
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
                                                                     // });


                                                                    
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