<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="OtherDonationList.aspx.cs" Inherits="DeffinityAppDev.App.OtherDonationList" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<%--<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>--%>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

       <%-- <link href="../assets/plugins/global/plugins.bundle.css?V1" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>--%>
	<style>
		.col-lg-8> tags{
			  padding:20px;
			  height:70px;
		}
		/*.tagify__input {
    flex-grow: 1;
    display: inline-block;
    min-width: 110px;
    margin: 5px;
    padding: 0.3em 0.5em;
    padding: var(--tag-pad,.3em .5em);
    line-height: inherit;
    position: relative;
    white-space: pre-wrap;
    color: inherit;
    color: var(--input-color,inherit);
    box-sizing: inherit;
  
}*/
		.tags-look .tagify__dropdown__item{
  display: inline-block;
  border-radius: 3px;
  padding: .3em .5em;
  border: 1px solid #CCC;
  background: #F3F3F3;
  margin: .2em;
  font-size: .85em;
  color: black;
  transition: 0s;
 /* height:75px;*/
}

.tags-look .tagify__dropdown__item--active{
  color: black;
}

.tags-look .tagify__dropdown__item:hover{
  background: lightyellow;
  border-color: gold;
}
	</style>


	

	<div class="card mb-5 mb-xl-10" id="pnlSearch" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" data-bs-target="#kt_account_deactivate" >
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Search</h3>
									</div>
                                      <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="">
                 

                <a class="btn btn-video" style="background-color:#50CD89;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo" href="#MainContent_MainContent_vimeo">
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                  <iframe id="vimeo" runat="server" style="display:none" src="https://player.vimeo.com/video/773366112?h=4d14ac395f" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>

                
            </div>
								</div>
								
								<div id="kt_account_tags1" class="collapse show">
									
										<div class="card-body border-top p-9">

												<div class="row mb-6">
												<!--begin::Label-->
												<div class="col-lg-6 fv-row fv-plugins-icon-container">
													<div class="col-sm-12 d-flex justify-content-between gap-3">
                                                         <asp:HiddenField ID="hunid" runat="server" ClientIDMode="Static" />
													<asp:TextBox ID="txtMemberSearch" runat="server" placeholder="Search" SkinID="txt_90"></asp:TextBox>
													<asp:Button ID="btnSearch" runat="server" SkinID="btnDefault" OnClick="btnSearch_Click" Text="Search" />
													<asp:HiddenField ID="hid" runat="server" Value="0" />
													</div>
													</div>
													<div class="col-lg-6 fv-row fv-plugins-icon-container">
														<div class="col-sm-12 d-flex justify-content-end">
														
															</div>
													</div>


											</div>
									</div>
		</div>
		</div>

	<div class="card mb-5 mb-xl-10" id="Div1" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" data-bs-target="#kt_account_deactivate" >
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"><asp:Label ID="lblPanelTitle" runat="server"></asp:Label> </h3>
									</div>
                                      <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="">
                 

                <asp:Button ID="btnTithing" runat="server" CssClass="btn btn-primary" Text="Donate" OnClick="btnTithing_Click" style="margin-right:10px"     />
                
            </div>
								</div>
								
								<div class="collapse show">
									
										<div class="card-body border-top p-9">

											<div class="row mb-6">
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
                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="Details of Donation" ItemStyle-Width="40%">
                    <ItemTemplate>
                        <asp:Label ID="lblTithigName" runat="server" Text='<%# Bind("TithigName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderText="Logged By">
                    <ItemTemplate>
                        <asp:Label ID="lblPaidBy" runat="server" Text='<%# Bind("PaidBy") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                 <asp:TemplateField HeaderText="Value of Goods" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" >
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("ValueOfGoods") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField  HeaderText="Company">
                    <ItemTemplate>
                        <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
<%--                 <asp:TemplateField HeaderText="Pay Ref">
                    <ItemTemplate>
                        <asp:Label ID="lblPayRef" runat="server" Text='<%# Bind("PayRef") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField HeaderText="Payment Type">
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentType" runat="server" Text='<%# Bind("PaymentType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

				 <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lbStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

				 <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                       <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-light" CommandArgument='<%# Bind("ID") %>' CommandName="view"  />
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                       <asp:Button ID="btnSendRecipt" runat="server" Text="Send Receipt" CssClass="btn btn-light" CommandArgument='<%# Bind("ID") %>' CommandName="SendReceipt"  />
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                      <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="del" SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete this record?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        </div>
											



											</div>

									</div>
		</div>

    <ajaxToolkit:ModalPopupExtender ID="mdlShowMail" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblSendMail" PopupControlID="pnlManagePassword" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="lblSendMail" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlManagePassword" runat="server" BackColor="White" Style="display:none;"
                       Width="950px" Height="750px" CssClass="card card-custom" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
                 <div class="card-title">
												
													<h3 class="card-label"><asp:Label ID="Label2" runat="server" Text="Send Receipt"></asp:Label> </h3>
												</div>
							
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
          <div class="row mb-6">
        <div class="col-lg-6 d-flex d-inline">
            <asp:Label ID="Label4" runat="server" Text="Template" style="margin-top:10px;padding-right:10px" ></asp:Label>
       
            <asp:DropDownList ID="ddlTemplate" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged" AutoPostBack="true" SkinID="ddl_60" style="padding-right:10px">
               
            </asp:DropDownList>
           
            </div>
       
        </div>
 
        <div class="form-group row mb-6" style="height:480px;overflow-y:auto;overflow-x:hidden;">
           <div class="form-group row mb-6">
               <div class="col-md-12 form-inline">
                  
									 <CKEditor:CKEditorControl ID="CKEditor1" BasePath="~/Scripts/ckeditor_4.20.1/" runat="server"
                         Height="370px" ClientIDMode="Static" BasicEntities="true" FullPage="true"  ></CKEditor:CKEditorControl>
                   </div>
								</div>
    </div>
       
           <div class="form-group row mb-6">
                   <div class="col-md-12 form-inline">
                       
                      <asp:HiddenField ID="HiddenField1" runat="server" />
                       
                       
                          <asp:HiddenField ID="htomail" runat="server" />  <asp:HiddenField ID="hsubject" runat="server" />
                                        <asp:Button ID="Button1" runat="server" SkinID="btnDefault" Text="Send" OnClick="btnSend_Click" />
                       <asp:Button ID="btnSubmitPop" runat="server" SkinID="btnDefault" Text="Save"  Visible="false" />
                       </div>
               </div>
        </div>
                   <%--  </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="lbtnClosePop" />
               </Triggers>
           </asp:UpdatePanel>--%>
           </asp:Panel>



</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
