<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="DCInventory.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="DeffinityAppDev.WF.DC.DCInventory" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<%@ Register Src="~/WF/DC/controls/CustomerOrder.ascx" TagName="CustomerOrder"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <uc2:FlsTab ID="flstab1" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <label id="lblTitle" runat="server"></label>  
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
    <a id ="link_return" visible="false" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to  <%= Resources.DeffinityRes.ServiceDesk%></a>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="pnlOrder" runat="server" Width="100%" Visible="false">
    <uc3:CustomerOrder ID="CustomerOrder1" runat="server" Visible="false" />
    
    </asp:Panel>
    <div class="form-group row">
        <div class="col-md-12">
           <strong>Assigned Stock </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
    <div class="form-group row">
        <div class="col-md-5">
    <asp:GridView  ID="GridAS" runat="server" OnPageIndexChanging="GridAS_PageIndexChanging" OnRowCommand="GridAS_RowCommand" PageSize="10" 
        AllowPaging="true"  HeaderStyle-BackColor="#40bbea" HeaderStyle-ForeColor="White" >
        <Columns>
          
             <asp:TemplateField HeaderText="Equipment"  ItemStyle-Width="60%" HeaderStyle-BackColor="#40bbea">
                <ItemTemplate>
                    <asp:Label ID="lblEquipmentName" runat="server" Text='<%# Bind("Equipment") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Storage Location"  HeaderStyle-BackColor="#40bbea">
                <ItemTemplate>
                    <asp:Label ID="lblStorageLocationName" runat="server" Text='<%# Bind("StorageLocationName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Qty Used"  ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#40bbea">
                <ItemTemplate>
                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("QtyUsed") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
           
            <%-- <asp:TemplateField HeaderText="Supplier" SortExpression="SupplierName">
                <ItemTemplate>
                    <asp:Label ID="lblSupplier" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="btnSendMail" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="Edit1" SkinID="btnDefault" Text="Allocate to Job"  ></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>--%>
             <asp:TemplateField ItemStyle-Width="10%" HeaderStyle-BackColor="#40bbea">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete1" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkDelete" OnClientClick="if (!confirm('do you want delete item?')) return false;"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
    </asp:GridView>
            </div>
        </div>
    <div class="form-group row">
        <div class="col-md-12">
           <strong>Search Inventory </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

      <div class="form-group row">
        
                <asp:Label ID="lblmsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
               
        </div>
    <div class="form-group row">
      <div class="col-md-4">
           <label class="col-sm-4 control-label">Storage Location</label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlStorageLocation" runat="server" SkinID="ddl_90"></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label">Search</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-4">
          <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
	</div>
</div>
      <div class="form-group row">
          <div class="col-md-12 pull-right">
              <asp:Button ID="btnAddStock" runat="server" SkinID="btnDefault" Text="Add New Item" style="float:right;visibility:hidden;display:none;" />
              </div>
          </div>
    <asp:GridView ID="GridList" runat="server" OnPageIndexChanging="GridList_PageIndexChanging" OnRowCommand="GridList_RowCommand" PageSize="20" AllowPaging="true" AllowSorting="true" OnSorting="GridList_Sorting" >
        <Columns>
            <asp:TemplateField Visible="false" >
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit1" SkinID="BtnLinkEdit" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Category" SortExpression="CategoryName">
                <ItemTemplate>
                    <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Sub Category" SortExpression="SubCategoryName">
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
          
             <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="btnSendMail" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="Edit1" SkinID="btnDefault" Text="Allocate to Job"  ></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete1" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkDelete" OnClientClick="if (!confirm('do you want delete item?')) return false;"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
    </asp:GridView>
      <ajaxToolkit:ModalPopupExtender ID="mdlExnter" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddStock" PopupControlID="pnlStorageNew" CancelControlID="btnPopClose" >
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="lblStorageNew" runat="server"></asp:Label>
<asp:Panel ID="pnlStorageNew" runat="server" BackColor="White" Style="display:none;"
                       Width="680px" Height="320px" CssClass="panel panel-color panel-info" ScrollBars="None">
    <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblPnltitle" runat="server" Text="Allocate to Job"></asp:Label>  </h3>
							
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
								 <asp:LinkButton ID="btnPopClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
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
              <asp:ValidationSummary ID="vdSummary" runat="server" ValidationGroup="vd" />
              </div>
                   </div>
             <%-- <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Category</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlCategory" runat="server" SkinID="ddl_90"></asp:DropDownList>
               <ajaxToolkit:CascadingDropDown ID="ccdCategory" runat="server" TargetControlID="ddlCategory"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetInventoryCategory" LoadingText="[Loading ...]" />
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCategory" InitialValue="0"
                                        Display="None" ErrorMessage="Please select category" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
              <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Sub Category</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlSubCategory" runat="server" SkinID="ddl_90"></asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdSubCategory" runat="server" TargetControlID="ddlSubCategory"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetInventorySubCategory" LoadingText="[Loading...]" ParentControlID="ddlCategory" />
               <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSubCategory" InitialValue="0"
                                        Display="None" ErrorMessage="Please select sub category" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>--%>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Equipment</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtEquipment" runat="server" SkinID="txt_90" Width="10"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEquipment"
                                        Display="None" ErrorMessage="Please enter equipment" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
     <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Quantity</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtQuantity" runat="server" SkinID="Price_150px" Text="0"></asp:TextBox>
                
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtQuantity"
                                        Display="None" ErrorMessage="Please enter quantity" ValidationGroup="vd"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQuantity"
                        ErrorMessage="Please enter valid quantity" Operator="DataTypeCheck" Type="Integer" ValidationGroup="vd" Display="None"></asp:CompareValidator>
                   
            </div>
              </div>
	</div>
   
              
     <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
               <asp:HiddenField ID="hbomid" runat="server" Value="0" />
               <asp:Button ID="btnSelect" runat="server" SkinID="btnSubmit" OnClick="btnSelect_OnClick" ValidationGroup="vd" />
              
               </div>
              </div>
         </div>

              </div>
         </div>
        </div>
</asp:Panel>
    <asp:HiddenField ID="hid" runat="server" />
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
