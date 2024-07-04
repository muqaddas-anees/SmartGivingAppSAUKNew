<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="MyFunraiser.aspx.cs" Inherits="DeffinityAppDev.App.Fundraiser.MyFunraiser" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
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
	</style>
     <div class="card mb-5 mb-xl-10">
		 <div class="card-body">
			 <div class="row gap-3">
				 <div class="col-lg-6">
			<h1 class="text-dark fw-bolder my-1 fs-1 lh-1">	 <asp:Label ID="lblTopTitle" runat="server" Text="Let's Setup Your Fundraising Page"></asp:Label> </h1>
					 </div>
				 <div class="col-lg-5 d-flex d-inline justify-content-end gap-3">
					 <div class="form-check form-switch form-check-custom form-check-solid me-10" style="display:none;visibility:hidden;">
    <input class="form-check-input h-30px w-50px" type="checkbox" value="" id="chkActive" runat="server"/>
    <label class="form-check-label" for="flexSwitch30x50">
       Active
    </label>
</div>
					 <asp:Button ID="btnPeertoPeer" runat="server" Text="Peer-to-Peer Fundraising" OnClick="btnPeertoPeer_Click" Visible="false" />

					 <asp:Button ID="btnPreView" runat="server" Text="Preview" OnClick="btnPreView_Click" />
					 <asp:Button ID="btnSaveFundraiser" runat="server" Text="Save Fundraiser" OnClick="btnSaveFundraiser_Click" />
					 </div>
				
			 </div>
		 </div>
		 </div>

	<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Campaign Title</h3>
									</div>

				</div>
		 <div class="card-body">

			 <div class="row">
				 <div class="col-lg-8">
					<asp:TextBox ID="txtTitle" runat="server" ></asp:TextBox>
				 </div>
			 </div>

			 </div>
		</div>

	<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Your Profile Picture</h3>
									</div>

				</div>
		 <div class="card-body">

			 <div class="row mb-6"><label> Your pic should be around 150 x 150 pixels in size. </label> </div>
			  <div class="row">
				 <div class="col-lg-6">
					 <div class="row"></div>
					  <asp:FileUpload runat="server" id="file_yourlogo" CssClass="form-control" />
					 <br />
					 <asp:Button ID="btnSaveYourlogo" runat="server"  OnClick="btnSaveYourlogo_Click" Text="Upload" />

				 </div>
				  <div class="col-lg-6">
					   <asp:Image ID="img_mylogo" runat="server" CssClass="img-responsive" style="max-height:150px;max-width:150px" />
				 </div>

			 </div>
			 </div>
		</div>

	<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Your Story</h3>
									</div>

				</div>
		 <div class="card-body">

		<CKEditor:CKEditorControl ID="txtyourstory" BasePath="~/Scripts/ckeditor_4.20.1/" Skin="moono-lisa" runat="server"
                         Height="500px" ClientIDMode="Static" ></CKEditor:CKEditorControl>

			 </div>
		</div>

	
	<div class="card mb-5 mb-xl-10" id="pnlHideDetails" runat="server" visible="false">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Additional Details</h3>
									</div>

				</div>
		 <div class="card-body">
			 	<div class="row mb-6" id="pnlDates" runat="server">
												
					 <div class="col-lg-6 d-flex d-inline">
						 <label class="col-sm-2 col-form-label fw-bold fs-6">Campaign Start Date </label>
						 <div class="col-sm-4">
															<asp:TextBox ID="txtStartDate" Text="" runat="server" SkinID="DateNew"></asp:TextBox>
																	</div>
						 <label class="col-sm-2 col-form-label fw-bold fs-6">Campaign End Date </label>
						   <div class="col-sm-4">
															<asp:TextBox  ID="txtEndDate" Text="" SkinID="DateNew" runat="server"></asp:TextBox>
																	</div>
					 </div>
					  <div class="col-lg-6 d-flex d-inline">
						   
						
					 </div>

												
												
											</div>
			 	<div class="row mb-6">
												
					 <div class="col-lg-6 d-flex d-inline">
						 <label class="col-sm-2 col-form-label fw-bold fs-6">Address </label>
						 <div class="col-sm-10">
															<asp:TextBox ID="txtAddress" Text="" runat="server"></asp:TextBox>
																	</div>
					 </div>

					 </div>
			 <div class="row mb-6">
												
					 <div class="col-lg-6 d-flex d-inline">
						 <label class="col-sm-2 col-form-label fw-bold fs-6">State </label>
						 <div class="col-sm-10">
															<asp:TextBox ID="txtState" Text="" runat="server"></asp:TextBox>
																	</div>
					 </div>

					 </div>
			  <div class="row mb-6">
												
					 <div class="col-lg-6 d-flex d-inline">
						 <label class="col-sm-2 col-form-label fw-bold fs-6">City </label>
						 <div class="col-sm-10">
															<asp:TextBox ID="txtCity" Text="" runat="server"></asp:TextBox>
																	</div>
					 </div>

					 </div>
			   <div class="row mb-6">
												
					 <div class="col-lg-6 d-flex d-inline">
						 <label class="col-sm-2 col-form-label fw-bold fs-6">Postcode </label>
						 <div class="col-sm-10">
															<asp:TextBox ID="txtPostcode" Text="" runat="server"></asp:TextBox>
																	</div>
					 </div>

					 </div>
			   <div class="row mb-6">
												
					 <div class="col-lg-6 d-flex d-inline">
						 <label class="col-sm-2 col-form-label fw-bold fs-6">Country </label>
						 <div class="col-sm-10">
															<asp:DropDownList ID="ddlcountry" runat="server">
																<asp:ListItem Text="UK" Value="UK"></asp:ListItem>
																<asp:ListItem Text="USA" Value="USA"></asp:ListItem>
															</asp:DropDownList>
																	</div>
					 </div>

					 </div>
											
			 <div class="row">
				  <div class="col-lg-6">

					  </div>
			 </div>



			 </div>
		</div>

	<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Campaign Details</h3>
									</div>

				</div>
		 <div class="card-body">

		<CKEditor:CKEditorControl ID="txtDescriptionArea" BasePath="~/Scripts/ckeditor_4.20.1/" Skin="moono-lisa" runat="server"
                         Height="500px" ClientIDMode="Static" ReadOnly="true" ></CKEditor:CKEditorControl>

			 </div>
		</div>

	<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Organisation Logo</h3>
									</div>

				</div>
		 <div class="card-body">

			 <div class="row mb-6"><%--<label> The logo should be around 150 pixels in height. </label> --%></div>
			  <div class="row">
				 <div class="col-lg-6" style="display:none;visibility:hidden;">
					 <div class="row"></div>
					  <asp:FileUpload runat="server" id="imgLogo" CssClass="form-control" />
					 <br />
					 <asp:Button ID="btnSaveLogo" runat="server"  OnClick="btnSaveLogo_Click" Text="Upload" />

				 </div>
				  <div class="col-lg-6">
					   <asp:Image ID="imglogoShow" runat="server" CssClass="img-responsive" style="max-height:150px;max-width:150px;" />
				 </div>

			 </div>
			 </div>
		</div>
	<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Banner Image</h3>
									</div>

				</div>
		 <div class="card-body">
	 <div class="row mb-6"><%--<label> The banner should be 1920 X 1080 pixels for the best quality. </label> --%></div>

			 <div class="row">
				 <div class="col-lg-6" style="display:none;visibility:hidden;">
					  <asp:FileUpload runat="server" id="imgBanner" CssClass="form-control" Text="Upload" />
					 <br />
					 <asp:Button ID="btnSaveBanner" runat="server" OnClick="btnSaveBanner_Click" Text="Upload" />

				 </div>
				  <div class="col-lg-6">
					   <asp:Image ID="img" runat="server" CssClass="img-responsive" style="max-height:350px" />
				 </div>

			 </div>
			

			 </div>
		</div>
	<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Campaign Target</h3>
									</div>

				</div>
		 <div class="card-body">
			 
			   <div class="row mb-6"><label> Setting the goal will display a donation metre. </label> </div>

			 <div class="row mb-6">
				 <div class="col-lg-6">
				 <asp:TextBox id="txtcurrenyValue" runat="server" Text="0.00" SkinID="Price_200px" MaxLength="15"></asp:TextBox>
					   <ajaxToolkit:FilteredTextBoxExtender ID="txtfilter" runat="server" TargetControlID="txtcurrenyValue" ValidChars="0123456789."></ajaxToolkit:FilteredTextBoxExtender>
					 </div>
			 </div>

			 </div>
		</div>
		<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Quick Donation Buttons</h3>
									</div>

				</div>
		 <div class="card-body">

			  <div class="row mb-6"><label> Donation buttons make it easier for your supporters to donate for specific categories, such as a food parcel. </label> </div>

			  <div class="row mb-6">

				  <div class="col-lg-12 d-flex "> <asp:Button ID="btnAddAmount" runat="server" OnClick="btnAddAmount_Click" Text="Add a Donation Button" /> </div>

				  </div>

			  <div class="row mb-6">
				  <div class="col-lg-6">
				  <asp:GridView ID="GridMoney" runat="server" OnRowCommand="GridMoney_RowCommand">
														<Columns>
															<asp:TemplateField ItemStyle-Width="30px">
																<ItemTemplate>
																<asp:LinkButton ID="btnEdit" runat="server" CommandName="edit1" CommandArgument='<%#Eval("ID") %>' SkinID="BtnLinkEdit"></asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateField>
														
																<asp:TemplateField HeaderText="Title">
																<ItemTemplate>
																<asp:Label ID="lblShortDesc" runat="server" Text='<%#Eval("Shortdescription") %>'></asp:Label>
																</ItemTemplate>
															</asp:TemplateField>
																<asp:TemplateField HeaderText="Short Description">
																<ItemTemplate>
																<asp:Label ID="lblDesc" runat="server" Text='<%#Eval("Description") %>' ></asp:Label>
																</ItemTemplate>
															</asp:TemplateField>
																<asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="txt_right" ItemStyle-Width="125px">
																<ItemTemplate>
																<asp:Label ID="lblValue" runat="server" Text='<%#Eval("Amount","{0:F2}") %>'></asp:Label>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField ItemStyle-Width="30px">
																<ItemTemplate>
																<asp:LinkButton ID="btnDel" runat="server" CommandName="del" CommandArgument='<%#Eval("ID") %>' SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete the this amount?');"></asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateField>
														</Columns>
													</asp:GridView>
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
							<h3 class="card-body"><asp:Label ID="lblPopUpHeader" runat="server" Text="Add Amount"></asp:Label>  </h3>
							
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
				<label class="form-label">Title</label>
				<asp:TextBox ID="txtShortDescription" runat="server" MaxLength="500" ></asp:TextBox>

			</div>
				<div class="row mb-6">
				<label class="form-label"> Short Description</label>
				<asp:TextBox ID="txtDescription" runat="server" MaxLength="1000" SkinID="txtMulti_80" TextMode="MultiLine"></asp:TextBox>

			</div>
				<div class="row mb-6">
				<label class="form-label">Amount</label>
				<asp:TextBox ID="txtAmount" runat="server" MaxLength="20" SkinID="Price_200px"></asp:TextBox>

			</div>
			
</div>
		<div class="form-group row mb-6">

			<div class="col-md-12">
				
				
				<asp:HiddenField ID="haid" runat="server" Value="0" />
				<asp:Button ID="btnSaveAmount" runat="server" OnClick="btnSaveAmount_Click" SkinID="btnSubmit"/>
				
			</div>
</div>
</div>


		   </asp:Panel>


				  </div>

			 </div>
		</div>
	<div class="card mb-5 mb-xl-10" style="display:none;visibility:hidden;">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Show Campaign Update Wall</h3>
									</div>

				</div>
		 <div class="card-body">

			  <div class="row mb-6">
				  <div class="col-lg-4">This will show the progress of the fundraiser.Only Admins can update the wall.</div>
				   <div class="col-lg-6">
					    <div class="form-check form-switch form-check-custom form-check-solid me-10">
    <input class="form-check-input h-30px w-50px" type="checkbox" value="" id="chkWall" runat="server"/>
    <label class="form-check-label" for="flexSwitch30x50">
       On
    </label>
</div>
				   </div>
			  </div>


			 </div>
		</div>

	<asp:HiddenField ID="hid" runat="server" Value="0" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>

