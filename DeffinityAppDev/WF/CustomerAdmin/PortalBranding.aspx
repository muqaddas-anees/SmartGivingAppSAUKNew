<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="PortalBranding.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.SetLogo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Company Information
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
	 <a class="btn btn-video" id="btn_video" runat="server" style="background-color:#50CD89;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo" href="#MainContent_MainContent_panel_options_vimeo">
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                  <iframe id="vimeo" runat="server" style="display:none" src="https://player.vimeo.com/video/820469971?h=5ccc332cc3" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>

    <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/Onboarding/Default.aspx"><i class="fa fa-arrow-left"></i> Return to Onboarding</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
	  <div class="form-group row mb-6">
        <div class="col-md-12 text-bold">
 <strong>Logo</strong> 
<hr class="no-top-margin">
	</div>
</div>
    <div class="form-group row mb-6">
      <div class="col-md-6">
          
                <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="FileUpload1" Display="None" 
                            ErrorMessage="Browse an image file to upload" 
                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*.*))+\.(jpg|JPG|gif|GIF|png|PNG)$" 
                            ValidationGroup="Group10">File</asp:RegularExpressionValidator>
	</div>
	
</div>
    <div class="form-group row mb-6">
      <div class="col-md-6">
           <asp:Image runat="server" ID="imgLogo" Height="70"  />
          </div>
        </div>
    <div class="form-group row mb-6">
      <div class="col-md-6">
           <asp:Button ID="btnSubmit" runat="server" 
                                OnClick="btnSubmit_Click1" 
                                ValidationGroup="Group10" SkinID="btnSubmit" />
          </div>
        </div>

	 <div class="form-group row mb-6">
        <div class="col-md-12 text-bold">
 <strong>Landing Page Image</strong> 
<hr class="no-top-margin">
	</div>
</div>
    <div class="form-group row mb-6">
      <div class="col-md-6">
          
                <asp:FileUpload ID="FileUpload2" runat="server" />
                       
	</div>
	
</div>
    <div class="form-group row mb-6">
      <div class="col-md-6">
           <asp:Image runat="server" ID="imgBanner" style="min-width:600px;max-width:750px"  />
          </div>
        </div>
    <div class="form-group row mb-6">
      <div class="col-md-6">
           <asp:Button ID="btnUploadBanner" runat="server" 
                                OnClick="btnUploadBanner_Click" 
                                 SkinID="btnSubmit" />
          </div>
        </div>
    <div class="form-group row mb-6">
      <div class="col-md-7">
           <div class="form-group row mb-6">
                                  <div class="col-md-12">
                                      <asp:Label SkinID="GreenBackcolor" ID="lblMsg" runat="server" EnableViewState="false"></asp:Label>
                                       <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="fls"
                DisplayMode="List" ClientIDMode="Static" />
                                      </div>
               </div>
           <div class="form-group row mb-6">
                                 
                                       <label class="col-sm-2 control-label">Company</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtCompany" runat="server" MaxLength="250"></asp:TextBox>      
                     <asp:RequiredFieldValidator ID="rfvNotes" runat="server" ControlToValidate="txtCompany"
                ErrorMessage="Please enter company" SetFocusOnError="true" ValidationGroup="fls" Display="None"></asp:RequiredFieldValidator>
					</div>
				
</div>
          
           <div class="form-group row mb-6">
                                
                                       <label class="col-sm-2 control-label">Registration Number</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtRegistrationNumber" runat="server"  MaxLength="250"></asp:TextBox>      
                    
					</div>
				
</div>
             <div class="form-group row mb-6">
                                 
                                       <label class="col-sm-2 control-label">Tax Reg</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtTaxReg" runat="server"  MaxLength="250"></asp:TextBox>      
                    
					</div>
				
</div>
           <div class="form-group row mb-6">
                                 
                                       <label class="col-sm-2 control-label">Bank Name</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtBank" runat="server"  MaxLength="250"></asp:TextBox>      
                    
					</div>
				
</div>
             <div class="form-group row mb-6">
                                
                                       <label class="col-sm-2 control-label">Account Number</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtAccountNumber" runat="server"  MaxLength="250"></asp:TextBox>      
                    
					</div>
				
</div>
             <div class="form-group row mb-6">
                                  
                                       <label class="col-sm-2 control-label">Sort Code</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtSortCode" runat="server"  MaxLength="250"></asp:TextBox>      
                    
					</div>
				
</div>
             <div class="form-group row mb-6">
                                 
                                       <label class="col-sm-2 control-label">IBAN</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtIBAN" runat="server"  MaxLength="250"></asp:TextBox>      
                    
					</div>
				
</div>
            <div class="form-group row mb-6">
                                 
                                       <label class="col-sm-2 control-label"> Swift Code </label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtSwiftCode" runat="server"  MaxLength="250"></asp:TextBox>      
                    
					</div>
				
</div>
            <div class="form-group row mb-6">
                                 
                                       <label class="col-sm-2 control-label"> Address</label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:TextBox ID="txtAddress" runat="server"  MaxLength="1000" TextMode="MultiLine" SkinID="txtMulti_80"></asp:TextBox>      
                    
					</div>
				
</div>
            <div class="form-group row mb-6">
                                 
                                       <label class="col-sm-2 control-label"> </label>
                                      <div class="col-sm-10 form-inline">
                                   <asp:Button runat="server" ID="btnUpdate" SkinID="btnUpdate" OnClick="btnUpdate_Click" ValidationGroup="fls" />
                    
					
				</div>
</div>

          </div>
        </div>

  


   <div class="form-group row mb-6">
       <div class="col-md-12">

           <%--<table class="table table-hover middle-align">
						<thead>
							<tr>
								<th>Skin Name</th>
								<th width="300">Color Palette</th>
								<th width="300">Skin Activation</th>
							</tr>
						</thead>
						<tbody>
							<tr data-skin="">
								<td>
									<a href="#" class="skin-name-link">Default Skin</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette" data-set-skin="">
										<span style="background-color: #2c2e2f"></span>
										<span style="background-color: #EEE"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #68b828"></span>
										<span style="background-color: #27292a"></span>
										<span style="background-color: #323435"></span>
									</a>
								</td>
								<td>
									<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>
								</td>
							</tr>
							<tr data-skin="aero">
								<td>
									<a href="#" class="skin-name-link">Aero</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #558C89"></span>
										<span style="background-color: #ECECEA"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #5F9A97"></span>
										<span style="background-color: #558C89"></span>
										<span style="background-color: #255E5b"></span>
									</a>
								</td>
								<td>
									<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>
								</td>
							</tr>
							<tr data-skin="navy">
								<td>
									<a href="#" class="skin-name-link">Navy</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #2c3e50"></span>
										<span style="background-color: #a7bfd6"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #34495e"></span>
										<span style="background-color: #2c3e50"></span>
										<span style="background-color: #ff4e50"></span>
									</a>
								</td>
								<td>
									<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>
								</td>
							</tr>
							<tr data-skin="facebook">
								<td>
									<a href="#" class="skin-name-link">Facebook</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #3b5998"></span>
										<span style="background-color: #8b9dc3"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #4160a0"></span>
										<span style="background-color: #3b5998"></span>
										<span style="background-color: #8b9dc3"></span>
									</a>
								</td>
								<td>
									<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>
								</td>
							</tr>
							<tr data-skin="turquoise">
								<td>
									<a href="#" class="skin-name-link">Turquise</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #16a085"></span>
										<span style="background-color: #96ead9"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #1daf92"></span>
										<span style="background-color: #16a085"></span>
										<span style="background-color: #0f7e68"></span>
									</a>
								</td>
								<td>
									<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>
								</td>
							</tr>
							<tr data-skin="lime">
								<td>
									<a href="#" class="skin-name-link">Lime</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #8cc657"></span>
										<span style="background-color: #ffffff"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #95cd62"></span>
										<span style="background-color: #8cc657"></span>
										<span style="background-color: #70a93c"></span>
									</a>
								</td>
								<td>
									<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>
								</td>
							</tr>
							<tr data-skin="green">
								<td>
									<a href="#" class="skin-name-link">Green</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #27ae60"></span>
										<span style="background-color: #a2f9c7"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #2fbd6b"></span>
										<span style="background-color: #27ae60"></span>
										<span style="background-color: #1c954f"></span>
									</a>
								</td>
								<td>
									<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>
								</td>
							</tr>
							<tr data-skin="purple">
								<td>
									<a href="#" class="skin-name-link">Purple</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #795b95"></span>
										<span style="background-color: #c2afd4"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #795b95"></span>
										<span style="background-color: #27ae60"></span>
										<span style="background-color: #5f3d7e"></span>
									</a>
								</td>
								<td>
									<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>
								</td>
							</tr>
							<tr data-skin="white">
								<td>
									<a href="#" class="skin-name-link">White</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #FFF"></span>
										<span style="background-color: #666"></span>
										<span style="background-color: #95cd62"></span>
										<span style="background-color: #EEE"></span>
										<span style="background-color: #95cd62"></span>
										<span style="background-color: #555"></span>
									</a>
								</td>
								<td>
									<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>
								</td>
							</tr>
							<tr data-skin="concrete">
								<td>
									<a href="#" class="skin-name-link">Concrete</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #a8aba2"></span>
										<span style="background-color: #666"></span>
										<span style="background-color: #a40f37"></span>
										<span style="background-color: #b8bbb3"></span>
										<span style="background-color: #a40f37"></span>
										<span style="background-color: #323232"></span>
									</a>
								</td>
								<td>
									<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>
								</td>
							</tr>
							<tr data-skin="watermelon">
								<td>
									<a href="#" class="skin-name-link">Watermelon</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #b63131"></span>
										<span style="background-color: #f7b2b2"></span>
										<span style="background-color: #FFF"></span>
										<span style="background-color: #c03737"></span>
										<span style="background-color: #b63131"></span>
										<span style="background-color: #32932e"></span>
									</a>
								</td>
								<td>
									<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>
								</td>
							</tr>
							<tr data-skin="lemonade">
								<td>
									<a href="#" class="skin-name-link">Lemonade</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #f5c150"></span>
										<span style="background-color: #ffeec9"></span>
										<span style="background-color: #FFF"></span>
										<span style="background-color: #ffcf67"></span>
										<span style="background-color: #f5c150"></span>
										<span style="background-color: #d9a940"></span>
									</a>
								</td>
								<td>
									<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>
								</td>
							</tr>
						</tbody>
					</table>--%>

       </div>
   </div>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
