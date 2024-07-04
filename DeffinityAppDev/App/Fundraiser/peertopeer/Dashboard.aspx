<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="DeffinityAppDev.App.Fundraiser.peertopeer.Dashboard" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .txt_right{
            
            text-align:right;
        }
        .txt_center{
            text-align:center;
        }
    </style>
     <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer gap-3" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0"><asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label> </h3>
            </div>
            <div class="card-toolbar gap-3">
                <a class="btn btn-video" style="background-color:#50CD89;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo" href="#vimeo">
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                  <iframe id="vimeo" style="display:none" src="https://player.vimeo.com/video/829145143?h=f1caf25e86" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>
                <asp:Button ID="btnInvite" runat="server" Text="Invite Fundraiser" SkinID="btnDefault" OnClick="btnInvite_Click" />
                 <asp:Button ID="btnPreview" runat="server" Text="Preview Campaign" SkinID="btnDefault" OnClick="btnPreview_Click" />
                 <asp:Button ID="btnCashDonation" runat="server" Text="Add Cash Donation" SkinID="btnDefault" OnClick="btnCashDonation_Click" />
                 <asp:Button ID="btnShare" runat="server" Text="Share Fundraiser" SkinID="btnDefault" OnClick="btnShare_Click" />
                </div>
            </div>
         </div>
     <div class="row mb-5">
        <div class="col-lg-4">
            <div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block">Total Donations</span>

																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true">
                                                                    
                                                                    <asp:Label id="lbltotal_donations" runat="server" Text="0.00"></asp:Label> 
                                                                  <%--  <asp:Label ID="icon_totalicon" runat="server" style="width:80px;padding-top:15px;padding-left:10px"></asp:Label>--%>

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>
            

        </div>
          <div class="col-lg-4">
            <div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block">Total Donors</span>

																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true">
                                                                    
                                                                   <asp:Label id="lbltotal_donors" runat="server" Text="0.00"></asp:Label> 
                                                                   <%-- <asp:Label ID="Label2" runat="server" style="width:80px;padding-top:15px;padding-left:10px"></asp:Label>--%>

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>
            

        </div>
          <div class="col-lg-4">
            <div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block">Average Donation</span>

																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true">
                                                                    
                                                                    <asp:Label id="lblaverage_donation" runat="server" Text="0.00"></asp:Label> 
                                                                  <%--  <asp:Label ID="Label4" runat="server" style="width:80px;padding-top:15px;padding-left:10px"></asp:Label>--%>

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>
            

        </div>

         </div>


    <div class="card mb-5 mb-xl-10">
       
        <div class="card-header border-0 cursor-pointer gap-3" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Fundraisers</h3>
            </div>
            <div class="card-toolbar gap-3">
               
                </div>
            </div>
        <div class="card-body">
             <div class="row mb-10">
            <div class="col-lg-4">
                <asp:TextBox ID="txtSearchFunraiser" runat="server" Text=""></asp:TextBox>
            </div>
             <div class="col-lg-1">
                 <asp:Button ID="btnSearchFundraiser" runat="server" ClientIDMode="Static" Text="Search" OnClick="btnSearchFundraiser_Click" />
                 </div>
        </div>
          <div class="row mb-10">
              
              
                <asp:GridView ID="GridFundrisers" runat="server" OnRowCommand="GridFundrisers_RowCommand">
														<Columns>
															<asp:TemplateField ItemStyle-Width="30px" Visible="false">
																<ItemTemplate>
																<asp:LinkButton ID="btnEdit" runat="server" CommandName="edit1" CommandArgument='<%#Eval("ID") %>' SkinID="BtnLinkEdit"></asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateField>
														
																<asp:TemplateField HeaderText="Name">
																<ItemTemplate>
																<asp:Label ID="lblShortDesc" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
																</ItemTemplate>
															</asp:TemplateField>
																<asp:TemplateField HeaderText="Email">
																<ItemTemplate>
																<asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' ></asp:Label>
																</ItemTemplate>
															</asp:TemplateField>
																<asp:TemplateField HeaderText="Total Raised" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="txt_right" ItemStyle-CssClass="txt_right" ItemStyle-Width="125px">
																<ItemTemplate>
																<asp:Label ID="lblTotalRaised" runat="server" Text='<%#Eval("TotalRaisedDisplay") %>'></asp:Label>
																</ItemTemplate>
															</asp:TemplateField>
                                                            	<asp:TemplateField HeaderText="Number of Donors" HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="txt_right" ItemStyle-Width="135px">
																<ItemTemplate>
																<asp:Label ID="lbNumberOfDonors" runat="server" Text='<%#Eval("NumberofDonors") %>'></asp:Label>
																</ItemTemplate>
															</asp:TemplateField>
                                                            	<asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center" ItemStyle-Width="125px">
																<ItemTemplate>
																<asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
																</ItemTemplate>
															</asp:TemplateField>
                                                            <asp:TemplateField  ItemStyle-Width ="75px">
																<ItemTemplate>
																<asp:Button ID="btnResendInvitation" runat="server" CommandName="resend" CommandArgument='<%#Eval("ID") %>' SkinID="btnDefault" Text="Resend Invitation" />
																</ItemTemplate>
															</asp:TemplateField>
                                                              <asp:TemplateField  ItemStyle-Width ="75px">
																<ItemTemplate>
																<asp:Button ID="btnPreview" runat="server" CommandName="preview" CommandArgument='<%#Eval("ID") %>' SkinID="btnDefault" Text="Preview Campaign" />
																</ItemTemplate>
															</asp:TemplateField>
                                                              <asp:TemplateField ItemStyle-Width ="75px">
																<ItemTemplate>
																<asp:Button ID="btnRemove" runat="server" CommandName="del" CommandArgument='<%#Eval("ID") %>' SkinID="btnDefault" Text="Remove Fundraiser" OnClientClick="return confirm('Do you want to remove the this record?');" />
																</ItemTemplate>
															</asp:TemplateField>
															<%--<asp:TemplateField ItemStyle-Width="30px">
																<ItemTemplate>
																<asp:LinkButton ID="btnDel" runat="server" CommandName="del" CommandArgument='<%#Eval("ID") %>' SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete the this amount?');"></asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateField>--%>
														</Columns>
													</asp:GridView>

              </div>

        </div>
         </div>
     <div class="card mb-5 mb-xl-10">
       
        <div class="card-header border-0 cursor-pointer gap-3" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Donations</h3>
            </div>
            <div class="card-toolbar gap-3">
               
                </div>
            </div>
        <div class="card-body">

            <asp:GridView ID="GridDashboard" runat="server" Width="100%" OnRowCommand="GridDashboard_RowCommand">
            <Columns>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField  HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblPaidDate" runat="server" Text='<%# Bind("PaidDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField  HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>' Visible="false"></asp:Label>
                         <asp:LinkButton ID="btnNavigate" runat="server"  Text='<%# Bind("Name") %>' CommandArgument='<%# Bind("ID") %>' CommandName="member"  />
                    </ItemTemplate>
                </asp:TemplateField>
               <%--  <asp:TemplateField  HeaderText="Donation">
                    <ItemTemplate>
                        <asp:Label ID="lblTithigName" runat="server" Text='<%# Bind("CategoryList") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                 <asp:TemplateField HeaderText="Logged By">
                    <ItemTemplate>
                        <asp:Label ID="lblPaidBy" runat="server" Text='<%# Bind("PaidBy") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="grid_header_right" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px" >
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="Pay Ref">
                    <ItemTemplate>
                        <asp:Label ID="lblPayRef" runat="server" Text='<%# Bind("PayRef") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField HeaderText="Payment Type">
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentType" runat="server" Text='<%# Bind("PaymentType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

				 <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:Label ID="lbStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

				<%-- <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                       <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-light" CommandArgument='<%# Bind("ID") %>' CommandName="view"  />
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                       <asp:Button ID="btnSendRecipt" runat="server" Text="Send Thank You" CssClass="btn btn-light" CommandArgument='<%# Bind("ID") %>' CommandName="SendReceipt"  />
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
            </div>
         </div>

    <%--<div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <asp:HiddenField ID="htab" runat="server" Value="1" />
                                    <script>
                                        $(document).ready(function () {

                                            $("[id*='tab1']").click(function () {

                                                $("[id *='kt_tab_pane_1']").show();
                                                $("[id *='kt_tab_pane_2']").hide();

                                            })

                                            $("[id*='tab2']").click(function () {

                                                $("[id *='kt_tab_pane_1']").hide();
                                                $("[id *='kt_tab_pane_2']").show();

                                            })

                                        });
                                    </script>


    <div class="row mb-6">
        <ul class="nav nav-tabs nav-line-tabs mb-5 fs-6">
    <li class="nav-item">
       <%-- <a class="nav-link active" data-bs-toggle="tab"  id="tab1">Donations</a>
        <asp:HyperLink ID="link1" runat="server" CssClass="nav-link active">Donations</asp:HyperLink>
    </li>
    <li class="nav-item">
       <%-- <a class="nav-link" data-bs-toggle="tab"  id="tab2" >Fundraisers</a>
        <asp:HyperLink ID="link2" runat="server" CssClass="nav-link">Fundraisers</asp:HyperLink>
    </li>
   
   
</ul>
        <div class="tab-content" id="myTabContent">
    <div class="row" id="kt_tab_pane_1" role="tabpanel" runat="server">
      
          <div class="row mb-6">
        

        </div>

    </div>
    <div class="row" id="kt_tab_pane_2" role="tabpanel" runat="server">
       

       

    </div>
            </div>
    </div>

                                    </div>
        </div>--%>


        <ajaxToolkit:ModalPopupExtender ID="mdl" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblpnl" PopupControlID="pnl" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="Label2" runat="server"></asp:Label>
        <asp:Label ID="lblpnl" runat="server"></asp:Label>
       <asp:Panel ID="pnl" runat="server" BackColor="White" Style="display:none;"
                       Width="500px" Height="500px" CssClass=" card shadow-sm" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblPopUpHeader" runat="server" Text="Invite Fundraiser"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
        <div class="form-group row mb-6">
                   <div class="col-md-12 form-inline">
                       <asp:HiddenField ID="huid" runat="server" />
                       </div>
            </div>

         
       
        <div class="form-group row mb-6">

		
			<div class="row mb-6">
				<label class="form-label">First Name</label>
				<asp:TextBox ID="txtFirstName" runat="server" MaxLength="200" ></asp:TextBox>

			</div>
				<div class="row mb-6">
				<label class="form-label"> Last Name</label>
				<asp:TextBox ID="txtLastName" runat="server" MaxLength="200" ></asp:TextBox>

			</div>
				<div class="row mb-6">
				<label class="form-label">Email</label>
				<asp:TextBox ID="txtEmail" runat="server" MaxLength="250"></asp:TextBox>

			</div>
			
</div>
		<div class="form-group row mb-6">

			<div class="col-md-12">
				
				
				<asp:HiddenField ID="haid" runat="server" Value="0" />
				<asp:Button ID="btnSaveInvite" runat="server" OnClick="btnSaveInvite_Click" SkinID="btnDefault" Text="Invite"/>
				
			</div>
</div>
</div>


		   </asp:Panel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
