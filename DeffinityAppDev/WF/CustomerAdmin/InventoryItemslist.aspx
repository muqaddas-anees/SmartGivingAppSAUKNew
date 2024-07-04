<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="InventoryItemslist.aspx.cs" EnableEventValidation="false" Inherits="DeffinityAppDev.WF.CustomerAdmin.InventoryItemslist" %>

<%@ Register Src="~/WF/CustomerAdmin/Controls/InventoryItemsCtrl.ascx" TagPrefix="Pref" TagName="InventoryItemsCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Inventory
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:InventoryItemsCtrl runat="server" id="InventoryItemsCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Inventory
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group row">
        
                <asp:Label ID="lblmsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
               
        </div>
      <div class="form-group row">
          <div class="col-md-12 pull-right">
              
              </div>
          </div>
     <div class="form-group row">
     
	<div class="col-md-4">
           <label class="col-sm-3 control-label">Search</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-4">
          <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
	</div>
         <div class="col-md-4">
             <asp:Label ID="lblbtnAddStock" runat="server"></asp:Label>
             <asp:Button ID="btnAddStock" runat="server" SkinID="btnDefault" Text="Add Stock" style="float:right;" OnClick="btnAddStock_OnClick" />
             </div>
</div>
    <asp:GridView ID="GridList" runat="server" OnPageIndexChanging="GridList_PageIndexChanging" OnRowCommand="GridList_RowCommand" PageSize="20" AllowPaging="true" AllowSorting="true" OnSorting="GridList_Sorting" >
        <Columns>
            <asp:TemplateField >
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit1" SkinID="BtnLinkEdit" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Brand" SortExpression="CategoryName">
                <ItemTemplate>
                    <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Type of Equipment" SortExpression="SubCategoryName">
                <ItemTemplate>
                    <asp:Label ID="lblSubCategory" runat="server" Text='<%# Bind("SubCategoryName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Equipment" SortExpression="Equipment">
                <ItemTemplate>
                    <asp:Label ID="lblEquipmentName" runat="server" Text='<%# Bind("Equipment") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Storage Location" SortExpression="StorageLocationName">
                <ItemTemplate>
                    <asp:Label ID="lblStorageLocationName" runat="server" Text='<%# Bind("StorageLocationName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Supplier" SortExpression="SupplierName">
                <ItemTemplate>
                    <asp:Label ID="lblSupplier" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Floor" SortExpression="Floor">
                <ItemTemplate>
                    <asp:Label ID="lblFloor" runat="server" Text='<%# Bind("Floor") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Aisle" SortExpression="Aisle">
                <ItemTemplate>
                    <asp:Label ID="lblAisle" runat="server" Text='<%# Bind("Aisle") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Shelf" SortExpression="Shelf">
                <ItemTemplate>
                    <asp:Label ID="lblShelf" runat="server" Text='<%# Bind("Shelf") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Bin" SortExpression="Bin">
                <ItemTemplate>
                    <asp:Label ID="lblBin" runat="server" Text='<%# Bind("Bin") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Reorder Level" SortExpression="ReorderLevel" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblReorderLevelColor" runat="server" Text='<%# Bind("ReorderLevelColor") %>' CssClass="statuscls" style="visibility:hidden;"></asp:Label>
                    <asp:Label ID="lblReorderLevel" runat="server" Text='<%# Bind("ReorderLevel") %>' Font-Bold="true"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="btnAddStock" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="AddStock" SkinID="btnDefault" Text="Add Stock"  ></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="btnTransfer" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="Transfer1" SkinID="btnDefault" Text="Transfer"  ></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="btnDeployed" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="Deployed" SkinID="btnDefault" Text="Deployed"  ></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                <ItemTemplate>
                    <asp:Button ID="btnSendMail" Visible="false" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="SendMail" SkinID="btnDefault" Text="Allocate to Job"  ></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete1" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkDelete" OnClientClick="if (!confirm('do you want delete item?')) return false;"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
    </asp:GridView>
    <script type="text/javascript">
                                 Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
                                 setStatusBackColor();
                                 function setStatusBackColor() {
                                    
                                         $('.statuscls').each(function () {
                                             
                                             var s = $(this).html();
                                             if (s == 'Red')
                                                 $(this).closest("td").css({ "background-color": "#FF6F64", "text-align": "right", "vertical-align": "middle", "color": "white" });
                                         });
                                    
                                 }
</script>

      <ajaxToolkit:ModalPopupExtender ID="mdlDeploy" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblbtnShowDeploy" PopupControlID="pnlDeploy" CancelControlID="linkdepoyclose" >
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="lblbtnShowDeploy" runat="server"></asp:Label>


    <asp:Panel ID="pnlDeploy" runat="server" BackColor="White" Style="display:none;"
                       Width="730px" Height="470px" CssClass="panel panel-color panel-info" ScrollBars="None">
    <div class="card-header">
							<h3 class="card-body"><asp:Label ID="Label1" runat="server" Text="Deploy"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="linkdepoyclose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
								
							</div>
						</div>
    <div class="panel-body">
        <div style="overflow-y:scroll">
             <asp:GridView ID="GridDeploy" runat="server" Width="97%">
            <Columns>
                <asp:TemplateField HeaderText="Date Deployed" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDateDeployed" runat="server" Text='<%#Bind("DateDeployed") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Job Ref">
                    <ItemTemplate>
                        <asp:Label ID="lblJobRef" runat="server" Text='<%#Bind("JobRef") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Customer">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomer" runat="server" Text='<%#Bind("Customer") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Address">
                    <ItemTemplate>
                        <asp:Label ID="lblAddress" runat="server" Text='<%#Bind("Address") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%#Bind("Qty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
       


        </div>
        </asp:Panel>


      <ajaxToolkit:ModalPopupExtender ID="mdlExnter" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblbtnAddStock" PopupControlID="pnlStorageNew" CancelControlID="btnPopClose" >
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="lblStorageNew" runat="server"></asp:Label>
<asp:Panel ID="pnlStorageNew" runat="server" BackColor="White" Style="display:none;"
                       Width="730px" Height="470px" CssClass="panel panel-color panel-info" ScrollBars="None">
    <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblPnltitle" runat="server" Text="Add Stock"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="btnPopClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
								
							</div>
						</div>
    <div class="panel-body">
        <asp:UpdatePanel ID="pnlUpdateInventorypnlUpdateInventory" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

           
     <div class="form-group row">

          <div class="col-md-12" >
     
               <div class="form-group row">
          <div class="col-md-12">
              <asp:ValidationSummary ID="vdSummary" runat="server" ValidationGroup="vd" />
              </div>
                   </div>
                <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Supplier</label>
           <div class="col-sm-10"  style="padding-left:10px">
                <asp:DropDownList ID="ddlSupplier" runat="server" SkinID="ddl_90" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged" ></asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdVendors" runat="server" TargetControlID="ddlSupplier"
                        Category="Supplier" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetVendorsList" LoadingText="[Loading...]" />
               <asp:RequiredFieldValidator ID="rfvSupplier" runat="server" ControlToValidate="ddlSupplier" InitialValue="0"
                                        Display="None" ErrorMessage="Please select supplier" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
              <div class="form-group row">
          <div class="col-md-6">
 <label class="col-sm-4 control-label"><%= Deffinity.systemdefaults.GetCategoryName() %></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
               <ajaxToolkit:CascadingDropDown ID="ccdCategory" runat="server" TargetControlID="ddlCategory"
                        Category="Category" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetCategoryByTypeOfRequest" LoadingText="[Loading ...]" />
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCategory" InitialValue="0"
                                        Display="None" ErrorMessage="Please select brand" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
                   <div class="col-md-6">
 <label class="col-sm-4 control-label"><%= Deffinity.systemdefaults.GetSubCategoryName() %></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged"></asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdSubCategory" runat="server" TargetControlID="ddlSubCategory"
                        Category="SubCategory" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetSubCategory" LoadingText="[Loading...]" ParentControlID="ddlCategory" />
               <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSubCategory" InitialValue="0"
                                        Display="None" ErrorMessage="Please select type of equipment" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
             
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Equipment</label>
           <div class="col-sm-10 form-inline"  style="padding-left:10px">
               <asp:DropDownList ID="ddlSupplierEquipment" runat="server" SkinID="ddl_80"></asp:DropDownList> <asp:LinkButton ID="btnShowText" runat="server" SkinID="BtnLinkAdd" OnClick="btnShowText_OnClick"    />
                <%--<ajaxToolkit:CascadingDropDown ID="casecadeMaterail" runat="server" TargetControlID="ddlSupplierEquipment"
                        Category="Equipment" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetMaterialItemsByVendor" LoadingText="[Loading...]" ParentControlID="ddlSubCategory" />--%>
               <asp:TextBox ID="txtEquipment" runat="server" Visible="false" SkinID="txt_80"></asp:TextBox>  <asp:LinkButton ID="btnCancel" runat="server" SkinID="BtnLinkCancel" OnClick="btnCancel_OnClick"    />
               <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEquipment"
                                        Display="None" ErrorMessage="Please enter equipment" ValidationGroup="vd"></asp:RequiredFieldValidator>--%>
            </div>
	</div>
</div>
     <div class="form-group row">
          <div class="col-md-6">
 <label class="col-sm-4 control-label">Quantity</label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtQuantity" runat="server" SkinID="Price_150px" Text="0"></asp:TextBox>
                
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtQuantity"
                                        Display="None" ErrorMessage="Please enter quantity" ValidationGroup="vd"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQuantity"
                        ErrorMessage="Please enter valid quantity" Operator="DataTypeCheck" Type="Integer" ValidationGroup="vd" Display="None"></asp:CompareValidator>
                   
            </div>
              </div>
         <div class="col-md-6">
 <label class="col-sm-4 control-label">Reorder&nbsp;Level</label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtReorderLevel" runat="server" SkinID="Price_150px" MaxLength="10" Text="0"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtReorderLevel"
                        ErrorMessage="Please enter valid reorder level" Operator="DataTypeCheck" Type="Integer" ValidationGroup="vd" Display="None"></asp:CompareValidator>
            </div>
	</div>
	</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Storage&nbsp;Location</label>
           <div class="col-sm-10" style="padding-left:10px">
                <asp:DropDownList ID="ddlStorageLocation" runat="server" SkinID="ddl_90"></asp:DropDownList>
               <asp:RequiredFieldValidator ID="rfvStorageLocation" runat="server" ControlToValidate="ddlStorageLocation" InitialValue="0"
                                        Display="None" ErrorMessage="Please select storage location" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
              
<div class="form-group row">
      <div class="col-md-3">
          <label class="col-sm-3 control-label">Floor</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtFloor" runat="server" MaxLength="250"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-3">
          <label class="col-sm-3 control-label">Aisle</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtAisle" runat="server" MaxLength="250"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-3">
          <label class="col-sm-3 control-label">Shelf</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtShelf" runat="server" MaxLength="250"></asp:TextBox>
            </div>
	</div>
<div class="col-md-3">
          <label class="col-sm-3 control-label">Bin</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtBin" runat="server" MaxLength="250"></asp:TextBox>
            </div>
	</div>
</div>
               
            
              
     <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"></label>
           <div class="col-sm-10 form-inline">
               <asp:HiddenField ID="hbomid" runat="server" Value="0" />
               <asp:Button ID="btnSelect" runat="server" SkinID="btnSubmit" OnClick="btnSelect_OnClick" ValidationGroup="vd" />
              
               </div>
              </div>
         </div>

              </div>
         </div>
                 </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSelect" />
            </Triggers>
        </asp:UpdatePanel>
        </div>
</asp:Panel>
    <asp:HiddenField ID="hid" runat="server" />
    <asp:HiddenField ID="hIsAddstock" runat="server" Value="0" />
     <ajaxToolkit:ModalPopupExtender ID="MdlTransfer" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblTransfer" PopupControlID="pnlTransfer" CancelControlID="lbtTransferClose" >
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="lblTransfer" runat="server"></asp:Label>
    <asp:Panel ID="pnlTransfer" runat="server" BackColor="White" Style="display:none;"
                       Width="630px" Height="330px" CssClass="panel panel-color panel-info" ScrollBars="None">
    <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblTransferTitle" runat="server" Text="Transfer Stock"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								<%--<a href="#">
									<i class="linecons-cog"></i>
								</a>
								
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">–</span>
									<span class="expand-icon">+</span>
								</a>
								
								<a href="#" data-toggle="reload">
									<i class="fa-rotate-right"></i>
								</a>--%>
								 <asp:LinkButton ID="lbtTransferClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
								<%--<a href="#" data-toggle="remove">
									×
								</a>--%>
							</div>
						</div>
    <div class="panel-body">
     <div class="form-group row">
          <div class="col-md-12" >
     
               <div class="form-group row">
          <div class="col-md-12">
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vt" />
              </div>
                   </div>
              
              <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Equipment</label>
           <div class="col-sm-10"  style="padding-left:10px">
               <asp:TextBox ID="txtEquipmentTransfer" runat="server" Width="10" Enabled="false"></asp:TextBox>
            </div>
	</div>
</div>
    
     <div class="form-group row">
          <div class="col-md-6">
 <label class="col-sm-4 control-label">Quantity</label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtTransferQty" runat="server" SkinID="Price_150px" Text="1"></asp:TextBox>
                
              <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTransferQty"
                                        Display="None" ErrorMessage="Please enter quantity" ValidationGroup="vt"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtTransferQty"
                        ErrorMessage="Please enter valid quantity" Operator="DataTypeCheck" Type="Integer" ValidationGroup="vt" Display="None"></asp:CompareValidator>
                   
            </div>
              </div>
       
	</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Storage Location</label>
           <div class="col-sm-10" style="padding-left:10px">
                <asp:DropDownList ID="ddlTranStorageLocation" runat="server" SkinID="ddl_90"></asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlStorageLocation" InitialValue="0"
                                        Display="None" ErrorMessage="Please select storage location" ValidationGroup="vt"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
              

               
              
              
     <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"></label>
           <div class="col-sm-10 form-inline">
               <asp:HiddenField ID="htid" runat="server" Value="0" />
               <asp:Button ID="btnTransfer" runat="server" Text="Submit" SkinID="btnDefault" OnClick="btnTransfer_OnClick" ValidationGroup="vt" />
              
               </div>
              </div>
         </div>

              </div>
         </div>
        </div>
</asp:Panel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
