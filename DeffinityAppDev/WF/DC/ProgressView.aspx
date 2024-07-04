<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="ProgressView.aspx.cs" Inherits="DeffinityAppDev.WF.DC.ProgressView" %>


<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <uc2:FlsTab ID="flstab1" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">

        <div class="card-header card-header-stretch">
            <div class="card-title d-flex align-items-center">
                <h3 class="fw-bold m-0 text-gray-800"> Progress </h3>

            </div>


        </div>

        <div class="card-body">
             <div class="d-flex justify-content-end mb-6">

        <asp:Button ID="btnAddProgress" runat="server" Text="Add" OnClick="btnAddProgress_Click" />

    </div>
            <asp:ListView ID="list_progress" runat="server" InsertItemPosition="None"  OnItemCommand="list_progress_ItemCommand" OnItemDataBound="list_progress_ItemDataBound">
           <LayoutTemplate>
               <div class="timeline">
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>
          <ItemTemplate>
			 <div class="timeline-item">
    <!--begin::Timeline line-->
    <div class="timeline-line w-40px"></div>
    <!--end::Timeline line-->

    <!--begin::Timeline icon-->
    <div class="timeline-icon symbol symbol-circle symbol-40px">
        <div class="symbol-label bg-light">
            <!--begin::Svg Icon | path: icons/duotune/communication/com009.svg-->
<span class="svg-icon svg-icon-2 svg-icon-gray-500"><svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
<path opacity="0.3" d="M5.78001 21.115L3.28001 21.949C3.10897 22.0059 2.92548 22.0141 2.75004 21.9727C2.57461 21.9312 2.41416 21.8418 2.28669 21.7144C2.15923 21.5869 2.06975 21.4264 2.0283 21.251C1.98685 21.0755 1.99507 20.892 2.05201 20.7209L2.886 18.2209L7.22801 13.879L10.128 16.774L5.78001 21.115Z" fill="currentColor"></path>
<path d="M21.7 8.08899L15.911 2.30005C15.8161 2.2049 15.7033 2.12939 15.5792 2.07788C15.455 2.02637 15.3219 1.99988 15.1875 1.99988C15.0531 1.99988 14.92 2.02637 14.7958 2.07788C14.6717 2.12939 14.5589 2.2049 14.464 2.30005L13.74 3.02295C13.548 3.21498 13.4402 3.4754 13.4402 3.74695C13.4402 4.01849 13.548 4.27892 13.74 4.47095L14.464 5.19397L11.303 8.35498C10.1615 7.80702 8.87825 7.62639 7.62985 7.83789C6.38145 8.04939 5.2293 8.64265 4.332 9.53601C4.14026 9.72817 4.03256 9.98855 4.03256 10.26C4.03256 10.5315 4.14026 10.7918 4.332 10.984L13.016 19.667C13.208 19.859 13.4684 19.9668 13.74 19.9668C14.0115 19.9668 14.272 19.859 14.464 19.667C15.3575 18.77 15.9509 17.618 16.1624 16.3698C16.374 15.1215 16.1932 13.8383 15.645 12.697L18.806 9.53601L19.529 10.26C19.721 10.452 19.9814 10.5598 20.253 10.5598C20.5245 10.5598 20.785 10.452 20.977 10.26L21.7 9.53601C21.7952 9.44108 21.8706 9.32825 21.9221 9.2041C21.9737 9.07995 22.0002 8.94691 22.0002 8.8125C22.0002 8.67809 21.9737 8.54505 21.9221 8.4209C21.8706 8.29675 21.7952 8.18392 21.7 8.08899Z" fill="currentColor"></path>
</svg>
</span>
<!--end::Svg Icon-->        </div>
    </div>
    <!--end::Timeline icon-->       

    <!--begin::Timeline content-->
    <div class="timeline-content mb-10 mt-n2">
        <!--begin::Timeline heading-->
        <div class="overflow-auto pe-3">
            <!--begin::Title-->
            <div class="fs-5 fw-semibold mb-2"> <asp:Label ID="lblProgressTitlte" runat="server" Text='<%# Bind("Title") %>'></asp:Label> </div>

             <div class="fs-5 fw-semibold mb-2"><asp:Label ID="lblProgressNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label> </div>
            <!--end::Title-->

           
                <asp:ListView ID="listFiles" runat="server" DataSource='<%# Eval("Files") %>' ItemPlaceholderID="filePlaceHolder">
                    <LayoutTemplate>
                         <div class="overflow-auto pb-5">
                        <div class="d-flex align-items-center border border-dashed border-gray-300 rounded min-w-700px p-7">
                         <asp:PlaceHolder runat="server" ID="filePlaceHolder" />
                            </div>
                             </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="overlay me-10"> <%--<asp:Label ID="lblFile" runat="server" Text='<%# Eval("FileName") %>'></asp:Label> --%>

                            <%# showFile(Eval("FileUNID").ToString()) %>

                        </div>
                    </ItemTemplate>
                </asp:ListView>

            

            <!--begin::Description-->
            <div class="d-flex align-items-center mt-1 fs-6">
                <!--begin::Info-->
                <div class="text-muted me-2 fs-7">Sent at <asp:Label ID="lblProgressDate" runat="server" Text='<%# Eval("LoggedOn","{0:dd MMM yyyy hh:mm tt}") %>'></asp:Label>  by <asp:Label ID="lblUser" runat="server" Text='<%# Eval("Username") %>'></asp:Label></div>
                <!--end::Info-->

                <!--begin::User-->
               <%-- <div class="symbol symbol-circle symbol-25px" data-bs-toggle="tooltip" data-bs-boundary="window" data-bs-placement="top" aria-label="Alan Nilson" data-bs-original-title="Alan Nilson" data-kt-initialized="1">
                    <img src="/metronic8/demo1/assets/media/avatars/300-1.jpg" alt="img">
                </div> --%> 
                <!--end::User--> 
            </div>
            <!--end::Description-->
        </div>
        <!--end::Timeline heading-->
    </div>
    <!--end::Timeline content--> 
</div>
                  </ItemTemplate>

        </asp:ListView>
        </div>

    </div>


   
    <ajaxToolkit:ModalPopupExtender ID="mdl" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblpnl" PopupControlID="pnl" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="Label1" runat="server"></asp:Label>
        <asp:Label ID="lblpnl" runat="server"></asp:Label>
       <asp:Panel ID="pnl" runat="server" BackColor="White" Style="display:none;"
                       Width="700px" Height="600px" CssClass=" card shadow-sm" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblPopUpHeader" runat="server" Text="Add Fund Progress"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
        <div class="form-group row mb-6">
                   <div class="col-md-12 form-inline">
                       <asp:HiddenField ID="huid" runat="server" Value="0" />
                       </div>
            </div>

         
       
        <div class="form-group row mb-6">

		
			<div class="row mb-6">
				<label class="form-label">Title</label>
				<asp:TextBox ID="txtTitle" runat="server" MaxLength="500" ></asp:TextBox>

			</div>
				<div class="row mb-6">
				<label class="form-label"> Short Description</label>
				<asp:TextBox ID="txtDescription" runat="server" MaxLength="1000" SkinID="txtMulti_80" TextMode="MultiLine"></asp:TextBox>

			</div>
				<div class="row mb-6">
				<label class="form-label">Upload Files</label>

				 <asp:FileUpload ID="Pfiles" runat="server" AllowMultiple="true" CssClass="form-control" />
                    <%--<input type="file" class="form-control" name="files[]" id="f_UploadImageAC" multiple/>--%>
			</div>
			
</div>
		<div class="form-group row mb-6">

			<div class="col-md-12">
				
				
				<asp:HiddenField ID="haid" runat="server" Value="0" />
				<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="btnSubmit"/>
				
			</div>
</div>
</div>


		   </asp:Panel>


 <%--   <div class="timeline">
        <div class="timeline-item">
    <!--begin::Timeline line-->
    <div class="timeline-line w-40px"></div>
    <!--end::Timeline line-->

    <!--begin::Timeline icon-->
    <div class="timeline-icon symbol symbol-circle symbol-40px">
        <div class="symbol-label bg-light">
            <!--begin::Svg Icon | path: icons/duotune/communication/com009.svg-->
<span class="svg-icon svg-icon-2 svg-icon-gray-500"><svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
<path opacity="0.3" d="M5.78001 21.115L3.28001 21.949C3.10897 22.0059 2.92548 22.0141 2.75004 21.9727C2.57461 21.9312 2.41416 21.8418 2.28669 21.7144C2.15923 21.5869 2.06975 21.4264 2.0283 21.251C1.98685 21.0755 1.99507 20.892 2.05201 20.7209L2.886 18.2209L7.22801 13.879L10.128 16.774L5.78001 21.115Z" fill="currentColor"></path>
<path d="M21.7 8.08899L15.911 2.30005C15.8161 2.2049 15.7033 2.12939 15.5792 2.07788C15.455 2.02637 15.3219 1.99988 15.1875 1.99988C15.0531 1.99988 14.92 2.02637 14.7958 2.07788C14.6717 2.12939 14.5589 2.2049 14.464 2.30005L13.74 3.02295C13.548 3.21498 13.4402 3.4754 13.4402 3.74695C13.4402 4.01849 13.548 4.27892 13.74 4.47095L14.464 5.19397L11.303 8.35498C10.1615 7.80702 8.87825 7.62639 7.62985 7.83789C6.38145 8.04939 5.2293 8.64265 4.332 9.53601C4.14026 9.72817 4.03256 9.98855 4.03256 10.26C4.03256 10.5315 4.14026 10.7918 4.332 10.984L13.016 19.667C13.208 19.859 13.4684 19.9668 13.74 19.9668C14.0115 19.9668 14.272 19.859 14.464 19.667C15.3575 18.77 15.9509 17.618 16.1624 16.3698C16.374 15.1215 16.1932 13.8383 15.645 12.697L18.806 9.53601L19.529 10.26C19.721 10.452 19.9814 10.5598 20.253 10.5598C20.5245 10.5598 20.785 10.452 20.977 10.26L21.7 9.53601C21.7952 9.44108 21.8706 9.32825 21.9221 9.2041C21.9737 9.07995 22.0002 8.94691 22.0002 8.8125C22.0002 8.67809 21.9737 8.54505 21.9221 8.4209C21.8706 8.29675 21.7952 8.18392 21.7 8.08899Z" fill="currentColor"></path>
</svg>
</span>
<!--end::Svg Icon-->        </div>
    </div>
    <!--end::Timeline icon-->       

    <!--begin::Timeline content-->
    <div class="timeline-content mb-10 mt-n2">
        <!--begin::Timeline heading-->
        <div class="overflow-auto pe-3">
            <!--begin::Title-->
            <div class="fs-5 fw-semibold mb-2">Invitation for crafting engaging designs that speak human workshop</div>
            <!--end::Title-->

            <!--begin::Description-->
            <div class="d-flex align-items-center mt-1 fs-6">
                <!--begin::Info-->
                <div class="text-muted me-2 fs-7">Sent at 4:23 PM by</div>
                <!--end::Info-->

                <!--begin::User-->
                <div class="symbol symbol-circle symbol-25px" data-bs-toggle="tooltip" data-bs-boundary="window" data-bs-placement="top" aria-label="Alan Nilson" data-bs-original-title="Alan Nilson" data-kt-initialized="1">
                    <img src="/metronic8/demo1/assets/media/avatars/300-1.jpg" alt="img">
                </div>  
                <!--end::User--> 
            </div>
            <!--end::Description-->
        </div>
        <!--end::Timeline heading-->
    </div>
    <!--end::Timeline content--> 
</div>

        </div>--%>


    

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
