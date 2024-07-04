<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="controls_PTMaterialTracker" Codebehind="PTMaterialTracker.ascx.cs" %>
<style>
    .round
    {
        border: 1px solid Silver;
        padding: 5px 5px;
        background: #d1e7ed;
        width: 40%;
        border-radius: 8px;
    }
</style>
<script type="text/javascript" >

    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=gvStorage.ClientID %>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
        
       
</script>
<div class="row">
     <div class="col-md-12">
         &nbsp;
         </div>
    </div>
<div class="row">
     <div class="col-md-4 well">
     <div class="form-group ">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label">Materials Cost:</label>
                                      <div class="col-sm-3 pull-right control-label"> <asp:Label ID="lblMaterialsCost" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label">Spent To Date:</label>
                                      <div class="col-sm-3 pull-right control-label"> <asp:Label ID="lblSpentToDate" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> Cost Remaining:</label>
                                      <div class="col-sm-3 pull-right control-label"> <asp:Label ID="lblCostRemaining" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
    
</div>
    </div>
<div class="row">
     <div class="col-md-12">
         &nbsp;
         </div>
    </div>
 <div class="form-group form-inline">
                                  <div class="col-md-12">
                                       <div class="col-sm-4">
                                            <label class="col-sm-5 control-label"> Worksheet</label>
                                      <div class="col-sm-7">
                                           <asp:DropDownList ID="ddlWorksheet" runat="server" SkinID="ddl_90" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlWorksheet_SelectedIndexChanged">
                </asp:DropDownList>
					</div>
                                           </div>
                                       <div class="col-sm-5">
                                            <label class="col-sm-6 control-label"> Search by Description</label>
                                      <div class="col-sm-6">
                                           <asp:TextBox ID="txtDescription" runat="server" SkinID="txt_90" ></asp:TextBox>
					</div>
                                           </div>
                                       <div class="col-sm-3 form-inline">
                                           <asp:Button ID="imgSearch" runat="server" SkinID="btnSearch" OnClick="imgSearch_Click" /> <asp:Button ID="imgViewAll" runat="server" SkinID="btnDefault" Text="View All" 
                    OnClick="imgViewAll_Click" />
                                           </div>
				</div>
</div>
<asp:Label ID="lblMessage" runat="server" ForeColor="Green" EnableViewState="false"></asp:Label>
<asp:ValidationSummary ID="Val1" runat="server" DisplayMode="BulletList" ValidationGroup="mat" />
<div style="width:100%">
<asp:GridView ID="gvMaterial" runat="server" OnRowDataBound="gvMaterial_RowDataBound"
    AllowPaging="true" Width="100%"  OnRowCommand="gvMaterial_RowCommand"
    EmptyDataText="No records found" OnPageIndexChanging="gvMaterial_PageIndexChanging">
    <Columns>
        <asp:TemplateField HeaderText="ID" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="WorksheetID" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblWorksheetID" runat="server" Text='<%# Bind("WorkSheetID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,DateReceived%>" ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
            <ItemStyle Width="120px" />
            <ItemTemplate>
              
                        <asp:TextBox ID="txtDateReceived" runat="server" Text='<%# Bind("ExpectedShipmentDate","{0:d}")%>'
                            SkinID="Date"></asp:TextBox>
                    
                        <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender"  /></div>
                    <asp:CompareValidator ID="CompareValidatorDateReceived" runat="server" ControlToValidate="txtDateReceived"
                        ErrorMessage="Please enter valid date" Operator="DataTypeCheck" Type="Date" ValidationGroup="mat">*</asp:CompareValidator>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                        PopupButtonID="imgbtnenddate6" TargetControlID="txtDateReceived" CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>
                    <%-- <asp:RequiredFieldValidator ID="rfv_dateRised1" runat="server" ControlToValidate="txtDateReceived"
                                        Display="None" ErrorMessage="Please Enter Received Date" ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                    
                        <asp:LinkButton ID="imgApplyDate" SkinID="BtnLinkDownload" OnClick="btn_ApplyDate_OnClick"
                            runat="server" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Worksheet Name">
            <ItemTemplate>
                <asp:Label ID="lblWorksheet" runat="server" Text='<%# Bind("Worksheet") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblfWorksheet" runat="server" Font-Bold="true" Text="Total"></asp:Label>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Material" HeaderText="Unit Cost" DataFormatString="{0:C}"
            ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Right" />
        <asp:TemplateField HeaderText="BOM Quantities">
            <ItemStyle HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblBOMQuantities" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total">
            <ItemStyle HorizontalAlign="Right" />
            <FooterStyle HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total","{0:C}") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblfTotal" runat="server" Font-Bold="true"></asp:Label>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Quantity Delivered" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:TextBox ID="txtQtyReceived" runat="server" Width="50px" SkinID="Price" Text='<%# Bind("QtyReceived") %>'>
                </asp:TextBox>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter a valid quantity delivered"
                    ControlToValidate="txtQtyReceived" Type="Double" Operator="DataTypeCheck" ValidationGroup="mat">*</asp:CompareValidator>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle Width="5px" />
            <ItemTemplate>
                <asp:LinkButton ID="imgStorageDetails" runat="server" SkinID="BtnLinkBarcode"
                    ToolTip="View Storage Details" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                    CommandName="Storage" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="imgHis" runat="server" CausesValidation="false" CommandName="History"
                    SkinID="BtnLinkHistory" CommandArgument='<%# Bind("ID") %>' ToolTip="View History" />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="20px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Quantity Left to <br/>Order">
            <ItemStyle HorizontalAlign="Right" />
            <FooterStyle HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblQtyLeftToOrder" runat="server" Text='<%# Bind("OrderLeft") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblfQtyLeftToOrder" runat="server" Font-Bold="true"></asp:Label>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Order to Date">
            <ItemStyle HorizontalAlign="Right" />
            <FooterStyle HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblOrderToDate" runat="server" Text='<%# Bind("OrderToDate","{0:C}") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblfOrderToDate" runat="server" Font-Bold="true"></asp:Label>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,NextExpectedDate%>" ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
            <ItemStyle Width="140px" />
            <ItemTemplate>
                <asp:TextBox ID="txtNextExpectedDate" runat="server" Text="<%# Bind('NextShipmentDate','{0:d}')%>"
                   SkinID="Date"></asp:TextBox>
                <asp:Label ID="imgbtnenddate7" runat="server" SkinID="Calender" />
                <asp:CompareValidator ID="CompareValidatorDate" runat="server" ControlToValidate="txtNextExpectedDate"
                    ErrorMessage="Please enter valid date" Operator="DataTypeCheck" Type="Date" ValidationGroup="mat">*</asp:CompareValidator>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender61"  runat="server"
                    PopupButtonID="imgbtnenddate7" TargetControlID="txtNextExpectedDate" CssClass="MyCalendar">
                </ajaxToolkit:CalendarExtender>
                <%-- <asp:RequiredFieldValidator ID="rfv_dateRised" runat="server" ControlToValidate="txtDateReceived"
                                    Display="None" ErrorMessage="Please Enter Next Shipment Date" ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Comments%>" ItemStyle-Width="85px"
            >
            <ItemTemplate>
                <asp:TextBox ID="txtComments" runat="server" Text='<%# Bind("Notes")%>' Width="100px"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</div>
<div class="clr">
</div>
<br />
<div>
    <asp:Label ID="lblRes" runat="server" Text="" ForeColor="Green" Width="100%"></asp:Label></div>
<asp:Button ID="imgUpdate" SkinID="btnUpdate" runat="server" OnClick="imgUpdate_Click"
    ValidationGroup="mat" />
<ajaxToolkit:ModalPopupExtender ID="mdlpopupHisstory" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="ImgHistory" PopupControlID="pnlHistory" CancelControlID="imgClose">
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="ImgHistory" runat="server" />
<asp:Panel ID="pnlHistory" runat="server" BackColor="White" Style="display: none;"
    Width="700px" Height="400px" BorderStyle="Double" BorderColor="LightSteelBlue"
    ScrollBars="Auto">
    <div class="form-group">
        <div class="col-md-10">
           <strong>History</strong> 
            <hr class="no-top-margin" />
            </div>

<div class="col-md-2">
     <asp:LinkButton ID="imgClose" runat="server" SkinID="BtnLinkClose" ToolTip="Close" />
</div>
    </div>
    
  
    <asp:GridView ID="GvHistory" runat="server" AutoGenerateColumns="False" GridLines="None"
                    ShowHeaderWhenEmpty="true" HorizontalAlign="Left" BorderStyle="None" CellPadding="2"
                    CellSpacing="2" EmptyDataText="No histoy found!" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ModifiedDate" HeaderText="Date Time" />
                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                        <asp:BoundField DataField="Worksheet" HeaderText="Worksheet Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="PreviousValue" HeaderText="Previous Value" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="ValueNow" HeaderText="Value Now" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mdlPopupStorage" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="imgPopupStorage" PopupControlID="pnlStorage">
</ajaxToolkit:ModalPopupExtender>
<asp:Button ID="imgPopupStorage" runat="server" Style="display: none" />

<asp:Panel ID="pnlStorage" runat="server" BackColor="White" Style="display: none"
                       Width="850px" Height="500px" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="Auto">

    <div class="form-group">
        <div class="col-md-10">
           <strong> Storage Details </strong> 
            <hr class="no-top-margin" />
            </div>
         <div class="col-md-2" style="float:right;text-align:right;">
        <asp:LinkButton ID="imghistoryCancel" runat="server" SkinID="BtnLinkClose" OnClick="imghistoryCancel_Click" />
    </div>
    </div>
      <div class="form-group">
            <div class="col-md-12">
                  Using this facility you can state where stock is being held by simply selecting
        the location and quantity held in each location.
            </div>
      </div>
  
       <div class="form-group">
            <div class="col-md-12">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
                             ValidationGroup="Storage" Font-Size="8pt" />
            </div>
       </div>
    <asp:UpdateProgress ID="uprogress1" runat="server">
        <ProgressTemplate>
            <asp:Label ID="imgLoad" SkinID="Loading" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanelStoregeDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <ajaxToolkit:ModalPopupExtender ID="mdlPopupConfirmation" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="btnAddToInventory" PopupControlID="pnlConformation" CancelControlID="btnNo">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlConformation" runat="server" BackColor="White" Style="display: none"
                Width="430px" Height="120px" BorderStyle="Solid" BorderColor="#787878" ScrollBars="Auto">
                <div style="background: #999999; padding: 7px; color: White; font-size: 10pt; font-weight: bold">
                    Confirmation</div>
                <p style="font-family: Tahoma; font-size: 10pt; font-weight: bold;">
                    Are you sure you want to add these items to the inventory?
                </p>
                <div style="float: right; padding-right: 5px;">
                    <asp:Button ID="btnYes" runat="server" Width="60px" Text="Yes" ForeColor="White"
                        Font-Bold="true" BackColor="#999999" OnClick="btnYes_Click" />
                    <asp:Button ID="btnNo" runat="server" Width="60px" ForeColor="White" Text="No" Font-Bold="true"
                        BackColor="#999999" />
                </div>
            </asp:Panel>

            <div class="form-group">
                            <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Category%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
                        <ajaxToolkit:CascadingDropDown ID="ccdCategory1" runat="server" TargetControlID="ddlCategory"
                            Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/Inventory/Webservices/InventoryMgr.asmx"
                            ServiceMethod="GetCategory" LoadingText="[Loading...]" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCategory"
                            InitialValue="0" ErrorMessage="Please select category" ValidationGroup="Storage">*</asp:RequiredFieldValidator>
            </div>
	</div>
 	                        <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.SubCategory%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlSubCategory" runat="server"></asp:DropDownList>
                        <ajaxToolkit:CascadingDropDown ID="ccdSubCategory" runat="server" TargetControlID="ddlSubCategory"
                            Category="SubCategory" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/Inventory/Webservices/InventoryMgr.asmx"
                            ServiceMethod="GetSubCategory" ParentControlID="ddlCategory" UseContextKey="true" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSubCategory"
                            InitialValue="0" ErrorMessage="Please select sub category" ValidationGroup="Storage">*</asp:RequiredFieldValidator>
            </div>
	</div>
	                        <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Product%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlProduct" runat="server"></asp:DropDownList>
                        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlProduct"
                            Category="Product" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/Inventory/Webservices/InventoryMgr.asmx"
                            ServiceMethod="GetProductBySubcategory" ParentControlID="ddlSubCategory" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlProduct"
                            InitialValue="0" ErrorMessage="Please select product" ValidationGroup="Storage">*</asp:RequiredFieldValidator>
            </div>
	</div>
            </div>
            <div class="form-group">
                            <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.QTY%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtQty" runat="server" Width="60px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter qty"
                            ControlToValidate="txtQty" ValidationGroup="Storage">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtQty"
                            ErrorMessage="Please enter integer value." ForeColor="Red" ValidationExpression="^[0-9]*$"
                            ValidationGroup="Storage">*
                        </asp:RegularExpressionValidator>
            </div>
	</div>
	                        <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Site%></label>
           <div class="col-sm-9">
                 <asp:DropDownList ID="ddlSite" runat="server" Width="120px">
                        </asp:DropDownList>
                        <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlSite"
                            Category="Site" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/Inventory/Webservices/InventoryMgr.asmx"
                            ServiceMethod="GetSite" LoadingText="[Loading...]" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSite"
                            InitialValue="0" ErrorMessage="Please select site" ValidationGroup="Storage">*</asp:RequiredFieldValidator>
            </div>
	</div>
	                        <div class="col-md-4">
           <asp:HiddenField ID="hfBOMID" runat="server" Value="0" />
            <asp:HiddenField ID="hfCategoryID" runat="server" Value="0" />
            <asp:HiddenField ID="hfSubcategoryID" runat="server" Value="0" />
            <asp:HiddenField ID="hfPartNumber" runat="server" Value="" />
            <asp:HiddenField ID="hfDescription" runat="server" Value="" />
            <asp:HiddenField ID="hfBarcode" runat="server" Value="" />
           <asp:Button ID="Save" runat="server" SkinID="btnApply"
                            OnClick="Save_Click" ValidationGroup="Storage" />
	</div>
            </div>
            <div class="form-group">
                 <div style="float: right; padding-right: 300px;">
                            <asp:Button ID="btnAddToInventory" runat="server" Text="Add to Inventory" /></div>
            </div>
              <div class="form-group">
                      <div class="col-md-12">
                          <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Width="100%"></asp:Label>
                          <asp:Label ID="lblError" runat="server" ForeColor="red" Width="100%"></asp:Label>
                          <asp:Label ID="lblStockError" runat="server" ForeColor="red" Width="100%"></asp:Label>
                      </div>
              </div>
            <div class="form-group">
                  <div class="col-md-12">
                        <asp:GridView ID="gvStorage" runat="server" AutoGenerateColumns="False" GridLines="None"
                            HorizontalAlign="Left" BorderStyle="None" CellPadding="2" CellSpacing="2" EmptyDataText="No Records Found!"
                            Width="60%">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                        <asp:CheckBox ID="chkInventory" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Site">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryId" runat="server" Text='<%# Bind("CategoryId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblSubCategoryId" runat="server" Text='<%# Bind("SubCategoryId") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblProduct" runat="server" Text='<%# Bind("Product") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblSiteId" runat="server" Text='<%# Bind("SiteId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblSite" runat="server" Text='<%# Bind("Site") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInventoryId" runat="server" Text='<%# Bind("InventoryID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                  </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Save" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Panel>
