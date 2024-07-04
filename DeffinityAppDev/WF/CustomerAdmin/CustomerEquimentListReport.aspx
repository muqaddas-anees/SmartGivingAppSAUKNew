<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="CustomerEquimentListReport.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.CustomerEquimentListReport" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Equipment Report
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <asp:Literal ID="lblContact" runat="server"></asp:Literal>  <asp:Literal ID="lblAddress" runat="server" Text="Equipment List"></asp:Literal>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <style>
       #plist_filter {
float: left;
}

    </style>
    <div class="row" style="display:none;visibility:hidden;">
								<div class="col-md-10">
									
								</div>

        <div class="col-md-2">
									<asp:Button ID="btnExport" runat="server" SkinID="btnDefault" Text="Export Data" OnClick="btnExport_Click" />
								</div>

        </div>
    <div class="row" style="display:none;visibility:hidden;">
								<div class="col-md-10">
									<input type="text" placeholder="Search" id="txtSearch" />
                                    <input type="button" id="btnsearch" title="Search" value="Search" />
								</div>

        <div class="col-md-2">
									
								</div>

        </div>
     <div class="row" style="display:none;visibility:hidden;">
								<div class="col-md-1">
									<label class="control-label" for="name">Address List</label>
								</div>
								<div class="col-md-8">
                                    <asp:DropDownList ID="ddlAddress" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAddress_SelectedIndexChanged"></asp:DropDownList>
								</div>
							</div>
     <asp:HiddenField ID="hsid" runat="server" ClientIDMode="Static" />
      <asp:HiddenField ID="pstatus" runat="server" Value="" ClientIDMode="Static" />
             <asp:HiddenField ID="sid" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="selectedids" runat="server" Value="" ClientIDMode="Static" />
                            <asp:HiddenField ID="haid" runat="server" Value="0" ClientIDMode="Static" />
    <%--<button id="btn">btn</button>--%>
          
         <asp:HiddenField ID="huid" runat="server" ClientIDMode="Static" />
 
             <div class="row" id="pnladdress" runat="server">
                 

                 <asp:HyperLink style="display:none;visibility:hidden;" ID="btnAddNewAddress" runat="server" Text="Add New Address" SkinID="Button" NavigateUrl="~/WF/CustomerAdmin/ContactAddressDetailsBasic.aspx"   />
            </div>
     <div class="form-group row">
         <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         </div>
     <div class="form-group form-inline">
         <div class="col-md-10 form-inline">
         <asp:TextBox ID="txtSearchNew" runat="server" SkinID="txt_50"></asp:TextBox>
         <asp:Button ID="btnSearch" runat="server" SkinID="btnSearch" OnClick="btnSearch_Click" />
             </div>
          <div class="col-md-2 form-inline pull-right" style="text-align:right;">
               <asp:Button ID="btnDownload" runat="server" SkinID="btnDefault" Text="Download XLS" OnClick="btnDownload_Click" />
              </div>
         </div>
    <asp:GridView ID="GridEqipment" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnRowCommand="GridEqipment_RowCommand">
        <Columns>
             <asp:TemplateField ItemStyle-Width="5%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Image ID="img" runat="server" ImageUrl='<%# GetImage( Eval("files_id") as int?)%>' Height="75px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Name" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPortfolioContact_Name" runat="server" Text='<%# Bind("PortfolioContact_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Address" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPortfolioContactAddress_Address" runat="server" Text='<%# Bind("PortfolioContactAddress_Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Equipment Brand" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCategory_Name" runat="server" Text='<%# Bind("Category_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Equipment Type" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubCategory_Name" runat="server" Text='<%# Bind("SubCategory_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField  HeaderText="Model" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductModel_ModelName" runat="server" Text='<%# Bind("ProductModel_ModelName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Serial Number">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAssets_SerialNo" runat="server" Text='<%# Bind("Assets_SerialNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Purchase Date" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAssets_PurchasedDate" runat="server" Text='<%# DateDisplay( Eval("Assets_PurchasedDate") as DateTime?)%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Warranty Term" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWarrantyTerm_Name" runat="server" Text='<%# Bind("WarrantyTerm_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Expiry Date" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAssets_ExpDate" runat="server" CssClass="statuscls" Text='<%# DateExpiredDisplay( Eval("Assets_ExpDate") as DateTime? )%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Notes" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAssets_FromNotes" runat="server" Text='<%# Bind("Assets_FromNotes") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                       <asp:Button ID="btnEmailClient" runat="server" SkinID="btnDefault" Text="Email Client" CommandArgument='<%# Bind("Assets_ID") %>' CommandName="emailclient" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    

     <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlManagePassword" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlManagePassword" runat="server" BackColor="White" Style="display:none;"
                       Width="950px" Height="630px" CssClass="panel panel-color panel-info" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text="Email Client"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="panel-body">
        <div class="form-group row">
                   <div class="col-md-12 form-inline">
                      
                       </div>
            </div>
      
 
        <div class="form-group row" style="height:480px;overflow-y:auto;overflow-x:hidden;">
             <section class="mailbox-env">
				
				<div class="row">
					
					<!-- Compose Email Form -->
					<div class="col-sm-12 mailbox-right">
						
						<div class="mail-compose">
							
							<%--	<!-- Header Title and Button Options -->
								<div class="mail-header">
									<div class="row">
										<div class="col-sm-6">							
											<h3>
												<i class="linecons-pencil"></i>
												Compose Mail
											</h3>
										</div>
										
										
									</div>
								</div>--%>
								
								<div class="form-group row">
									<label for="to">From:</label>
									 <asp:DropDownList ID="ddlAdminUsers" runat="server" CssClass="form-control"  style="height: 48px;padding-left: 80px;">
                                          </asp:DropDownList>
								</div>
								
								<div class="form-group row">
									<label for="txtcc">To:</label>
									<input type="text" class="form-control" id="txtto" runat="server" />
								</div>
								<div class="form-group row">
									<label for="txtsubject">Subject:</label>
									<input type="text" class="form-control" id="txtsubject" runat="server" />
								</div>
								
								<div class="form-group row">
									 <CKEditor:CKEditorControl ID="CKEditor1" BasePath="~/Scripts/ckeditor/" runat="server"
                         Height="150px" ClientIDMode="Static" BasicEntities="true" ></CKEditor:CKEditorControl>
								</div>
								
						</div>
						
					</div>
					
				</div>
				
			</section>                    
    </div>
       
           <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       
                       <div class="col-sm-10 form-inline">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                       
                       <asp:ValidationSummary ID="valSumm" runat="server" ValidationGroup="pay" />
                            <asp:HiddenField ID="hAssetID" runat="server" />
                           <asp:HiddenField ID="hFromEmail" runat="server" />
                            <asp:HiddenField ID="hClientID" runat="server" />
                                        <asp:Button ID="btnSend" runat="server" SkinID="btnDefault" Text="Send" OnClick="btnSend_Click" />
                       <asp:Button ID="btnSubmitPop" runat="server" SkinID="btnDefault" Text="Save"  Visible="false" />
                       <asp:Button Visible="false" ID="btnCancelPop" runat="server" SkinID="btnCancel"  />
                           </div>
                       </div>
               </div>
        </div>
                   <%--  </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="lbtnClosePop" />
               </Triggers>
           </asp:UpdatePanel>--%>
           </asp:Panel>
     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
   



</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
     
   


  
    
    <style type="text/css" class="init">
        div.dataTables_wrapper {
        /*width: 800px;*/
        margin: 0 auto;
        
    }
        div.dt-buttons{
            float:right;
        }
    </style>
  

   
     <script type="text/javascript">
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
        
         setStatusBackColor();
         
         //DTE_Field DTE_Field_Type_text DTE_Field_Name_PortfolioContactAddress.Amount
         //DTE_Field DTE_Field_Type_text DTE_Field_Name_PortfolioContactAddress.BillingName
         $(window).load(function () {
             $('.statuscls1').each(function () {

                 var s = $(this).html();
                 if (s == 'Expired')
                     
                     $(this).closest("td").css({ "background-color": "#FF3333", "text-align": "center", "vertical-align": "middle", "color": "white", "font-weight": "bold" });

             });

         });

         function setStatusBackColor() {

             $('.statuscls').each(function () {

                 var s = $(this).html();
                 if (s == 'Expired') {
                     
                     $(this).closest("td").css({ "background-color": "#FF3333", "text-align": "center", "vertical-align": "middle", "color": "white", "font-weight": "bold" });
                 }
             });

         }

       

        
         hidetabs();
</script>

    <!-- Imported styles on this page -->
	
</asp:Content>
