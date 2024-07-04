<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ShowComments.aspx.cs" Inherits="DeffinityAppDev.App.ShowComments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">

        <div class="card w-100 rounded-0 border-0" id="kt_drawer_chat_messenger">
				<!--begin::Card header-->
				<div class="card-header pe-5" id="kt_drawer_chat_messenger_header">
					<!--begin::Title-->
					<div class="card-title">
						<!--begin::User-->
						<div class="d-flex justify-content-center flex-column me-3">
							<a href="#" class="fs-4 fw-bold text-gray-900 text-hover-primary me-1 mb-2 lh-1">Comments</a>
							<!--begin::Info-->
							<div class="mb-0 lh-1">
								<span class="badge badge-success badge-circle w-10px h-10px me-1"></span>
								<span class="fs-7 fw-semibold text-muted">Active</span>
							</div>
							<!--end::Info-->
						</div>
						<!--end::User-->
					</div>
					<!--end::Title-->
				
				</div>
				<!--end::Card header-->
				<!--begin::Card body-->
				<div class="card-body" id="kt_drawer_chat_messenger_body">
					<!--begin::Messages-->
					<div class="scroll-y me-n5 pe-5" data-kt-element="messages" data-kt-scroll="true" data-kt-scroll-activate="true" data-kt-scroll-height="auto" data-kt-scroll-dependencies="#kt_drawer_chat_messenger_header, #kt_drawer_chat_messenger_footer" data-kt-scroll-wrappers="#kt_drawer_chat_messenger_body" data-kt-scroll-offset="0px" style="height: 278px;">
						<!--begin::Message(in)-->
						<div class="d-flex justify-content-start mb-10">
							<!--begin::Wrapper-->
							<div class="d-flex flex-column align-items-start">
								<!--begin::User-->
								<div class="d-flex align-items-center mb-2">
									<!--begin::Avatar-->
									<div class="symbol symbol-35px symbol-circle">
										<img alt="Pic" src="assets/media/avatars/300-25.jpg">
									</div>
									<!--end::Avatar-->
									<!--begin::Details-->
									<div class="ms-3">
										<a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary me-1">Brian Cox</a>
										<span class="text-muted fs-7 mb-1">2 mins</span>
									</div>
									<!--end::Details-->
								</div>
								<!--end::User-->
								<!--begin::Text-->
								<div class="p-5 rounded bg-light-info text-dark fw-semibold mw-lg-400px text-start" data-kt-element="message-text">How likely are you to recommend our company to your friends and family ?</div>
								<!--end::Text-->
							</div>
							<!--end::Wrapper-->
						</div>
						<!--end::Message(in)-->
					
					</div>
					<!--end::Messages-->
				</div>
				<!--end::Card body-->
				<!--begin::Card footer-->
				<div class="card-footer pt-4" id="kt_drawer_chat_messenger_footer">
					<!--begin::Input-->
					<textarea class="form-control form-control-flush mb-3" rows="1" data-kt-element="input" placeholder="Type a message"></textarea>
					<!--end::Input-->
					<!--begin:Toolbar-->
					<div class="d-flex flex-stack">
						<!--begin::Actions-->
						<div class="d-flex align-items-center me-2">
							<button class="btn btn-sm btn-icon btn-active-light-primary me-1" type="button" data-bs-toggle="tooltip" aria-label="Coming soon" data-kt-initialized="1">
								<i class="bi bi-paperclip fs-3"></i>
							</button>
							<button class="btn btn-sm btn-icon btn-active-light-primary me-1" type="button" data-bs-toggle="tooltip" aria-label="Coming soon" data-kt-initialized="1">
								<i class="bi bi-upload fs-3"></i>
							</button>
						</div>
						<!--end::Actions-->
						<!--begin::Send-->
						<button class="btn btn-primary" type="button" data-kt-element="send">Send</button>
						<!--end::Send-->
					</div>
					<!--end::Toolbar-->
				</div>
				<!--end::Card footer-->
			</div>

    </div>

      <div class="modal fade" id="modal-10" aria-hidden="true" data-backdrop="false" style="display: none;">
		<div class="modal-dialog">
			<div class="modal-content" style="width:800px">
				
				 <div class="modal-header">
        <h5 class="modal-title">Add Comment</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
				
				<div class="modal-body">
				 
					
                    <div class="row mb-6">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Name</label>
          <div class="col-sm-12">            
			<asp:TextBox ID="txtname" runat="server" ClientIDMode="Static"></asp:TextBox>             
              </div>
          </div>
				</div>
					  <div class="row mb-6">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Email</label>
          <div class="col-sm-12">            
			<asp:TextBox ID="txtemail" runat="server" ClientIDMode="Static"></asp:TextBox>             
              </div>
          </div>
				</div>
                    
					  <div class="row mb-6">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Contact</label>
          <div class="col-sm-12">            
			<asp:TextBox ID="txtcontact" runat="server" ClientIDMode="Static" ></asp:TextBox>             
              </div>
          </div>
				</div>
                      <div class="row mb-6">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Comment</label>
          <div class="col-sm-12">            
			<asp:TextBox ID="txtcomment" runat="server" ClientIDMode="Static" SkinID="txtMulti_80" TextMode="MultiLine"></asp:TextBox>             
              </div>
          </div>
				</div>
                    
                    
				
				<div class="modal-footer">
					
                   
					<button type="button" class="btn btn-info" id="btnSaveReason">Submit</button>
				</div>
			</div>
		</div>
	</div>
  </div>
    <script type="text/javascript">
        function showAjaxModal() {
            $("[id*=txtcomment]").val('');
            $("[id*=txtcontact]").val('');
            $("[id*=txtemail]").val('');
            $("[id*=txtname]").val('');
            $('#modal-10').modal('show', { backdrop: 'fade' });
        }
        function hideAjaxModal {
            $('#modal-10').modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
