<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DCBOMCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC1.controls.DCBOMCtrl" %>
<%@ Register Src="~/WF/DC/MailControls/SDTeamToCustomer.ascx" TagName="SDTeamToCustomer"
    TagPrefix="uc2" %>
    <%@ Register Src="~/WF/DC/controls/CustomerOrder.ascx" TagName="CustomerOrder"
    TagPrefix="uc3" %>
<style>
    .ralert-danger {
    background-color: #cc3f44;
    border-color: #cc3f44;
    color: #ffffff;
}

.ralert {
    padding: 15px;
    margin-bottom: 18px;
    border: 1px solid transparent;
    border-radius: 0px;
}
</style>
<asp:Panel ID="pnlOverprice" runat="server" Visible="false">
    <div class="form-group row">
                                  <div class="col-md-12">
    <asp:Label ID="lblOverPriceMsg" runat="server" Text="This Fixed Rate Price exceeds the threshold. You will need to seek approval." CssClass="ralert ralert-danger">

    </asp:Label>
                                      </div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                      &nbsp;
                                      </div>
          </div>
      <div class="form-group row">
                                  <div class="col-md-12">
                                      <asp:Button SkinID="btnDefault" runat="server" ID="btnSendforapproval" Text="Submit Invoice for Authorization" OnClick="btnSendforapproval_Click" />
                                      </div>
          </div>
    </asp:Panel>
<asp:Label ID="lblservice" runat="server"></asp:Label>
<asp:Panel ID="pnlservice" runat="server">
    <div class="btn_align_right">
        <asp:Image ID="imgBack" runat="server" SkinID="Back" Visible="False" />
    </div>
    <div>
        <asp:Label ID="lblMsg" runat="server" EnableViewState="False" SkinID="GreenBackcolor"></asp:Label>
        <asp:Label ID="lblError" runat="server" EnableViewState="False" SkinID="RedBackcolor"></asp:Label>
        <asp:ValidationSummary Width="100%" ID="ValidationSummary1" runat="server" ValidationGroup="Service" />
    </div>
   
    <asp:Panel ID="pnlOrder" runat="server" Width="100%" Visible="false">
    <uc3:CustomerOrder ID="CustomerOrder1" runat="server" Visible="false" />
    
    </asp:Panel>
   <%-- <br />
    <div class="clr"></div>
    <br />
    <div class="clr"></div>--%>
  
   
    <div>
    <asp:Panel ID="pnl_servicecatalog_add" runat="server">
          <div class="form-group row"  style="display:none;visibility:hidden">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">Vendors</label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlVendors1" runat="server"
                        DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlVendors_SelectedIndexChanged" >
                    </asp:DropDownList>
					</div>
                                      </div>
              </div>
        <div class="form-group row"  style="display:none;visibility:hidden">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Category%></label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlCategory1" runat="server" 
                        DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.SubCategory%></label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlSubCategory1" runat="server" AutoPostBack="True" DataTextField="Name" DataValueField="ID"
                        OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                        
                    </asp:DropDownList>
					</div>
				</div>
</div>
        <div class="form-group row" style="display:none;visibility:hidden">
                                  <div class="col-md-6" >
                                       <label class="col-sm-4 control-label">  Select Catalogue</label>
                                      <div class="col-sm-8">  <asp:DropDownList ID="ddlSelect" runat="server" Width="155px" Style="position: relative"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSelect1_SelectedIndexChanged">
                        <asp:ListItem Value="1">Labour</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">Materials</asp:ListItem>
                        <asp:ListItem Value="3">Service</asp:ListItem>
                    </asp:DropDownList>
                  
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> Item</label>
                                      <div class="col-sm-8">  <asp:DropDownList ID="ddlService" runat="server" DataValueField="ID" DataTextField="Name">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlService"
                        ErrorMessage="Please select service" InitialValue="0" ValidationGroup="Service">*</asp:RequiredFieldValidator>
                 
					</div>
				</div>
</div>
        <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
    rel="Stylesheet" type="text/css" />
     <%--   <script type="text/javascript">
    $(function () {
        $("[id$=txtSearch]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/WF/DC/webservices/DCServices.asmx/GetVendorItems") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        //alert(response.responseText);
                    },
                    failure: function (response) {
                       // alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=hfCustomerId]").val(i.item.val);
            },
            minLength: 1
        });
    });  
</script>--%>
        
        <div class="form-group row">
                     <div class="col-md-6">
                         </div>            
 <div class="col-md-6" style="float:right;text-align:right;">
                                      
				</div>
</div>
       
        </asp:Panel>
    </div>
    <%-- <div class="form-group row">
        <div class="col-md-12 text-bold">
        &nbsp;<br />
            </div>
    </div>
   <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.BOM%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>--%>
    <div  class="form-group row">
         <asp:CompareValidator ID="CValidatior1" runat="server" ErrorMessage="Please enter a valid Quantity"
                         Operator="DataTypeCheck" ValidationGroup="Service" ControlToValidate="txtQty" Display="Dynamic" Type="Double"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQty"
                        ErrorMessage="Please enter Quantity" ValidationGroup="Service"  Display="Dynamic"></asp:RequiredFieldValidator>
         <asp:LinkButton ID="btnAddItem" runat="server" SkinID="BtnLinkAdd" OnClick="btnAddItem_Click" Visible="false"></asp:LinkButton>
                      <asp:LinkButton ID="btnAdditemCancel" runat="server" SkinID="BtnLinkCancel" OnClick="btnAdditemCancel_Click" Visible="false"></asp:LinkButton>
    </div>
    <div class="form-group row">
              <div class="col-md-2">
                   <label class="col-sm-2 control-label"> Item</label>
                  <div class="col-sm-9 form-inline"> 
                      <asp:DropDownList ID="ddlItems" runat="server"  Visible="false"></asp:DropDownList>
                      <asp:TextBox ID="txtSearch" runat="server" MaxLength="250" style="width:150%" ></asp:TextBox>
                      <asp:HiddenField ID="hfCustomerId" runat="server" />
                       
                      <asp:HiddenField ID="hCatelogID" runat="server" />
                       <asp:HiddenField ID="hImage" runat="server" Value="00000000-0000-0000-0000-000000000000"/>
                      
                      </div>
                  </div>
         <div class="col-md-1" >
            <asp:Button ID="btnPopShow" runat="server" SkinID="btnDefault" Text="Look Up" OnClick="btnPopShow_Click" ></asp:Button>
            </div>
         <div class="col-md-2">
              <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Type%></label>
                  <div class="col-sm-8">
                      <asp:DropDownList ID="ddlstype" runat="server"></asp:DropDownList>
                      </div>
             </div>
              <div class="col-md-2">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.Quantity%></label>
                                      <div class="col-sm-5"> <asp:TextBox ID="txtQty" runat="server" SkinID="txt_100px" MaxLength="10" Text="1"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredQTY" runat="server" ValidChars="0123456789." TargetControlID="txtQty"></ajaxToolkit:FilteredTextBoxExtender>
                   
					</div>
				</div>
        
              <div class="col-md-2">
                <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Price%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtCost" runat="server" SkinID="Price_100px" MaxLength="10" Text="0.00"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="txtFilterCost" runat="server" ValidChars="0123456789." TargetControlID="txtCost"></ajaxToolkit:FilteredTextBoxExtender>
                                          </div>
               </div>
         <div class="col-md-3 form-inline">
             <asp:LinkButton ID="btnAdd" runat="server" SkinID="btnAdd" ValidationGroup="Service"
                        OnClick="btnAdd_Click" />
             <asp:Button ID="btnSendToQuotation" runat="server" SkinID="btnDefault" Text="Copy to Estimates" ValidationGroup="Service"
                        OnClick="btnSendToQuotations_Click" />
              <asp:Button ID="btnSend" runat="server" Text="Send to Client" SkinID="btnDefault" OnClick="btnSendToQuotations_Click" Visible="false" />
            
             </div>
             </div>
  
      <div class="form-group row">
          <div class="col-md-5  form-inline pull-right">
               <asp:LinkButton SkinID="btnCancel" runat="server" ID="back" Text="Cancel" Visible="false"></asp:LinkButton>
              <asp:Button ID="btnPay" runat="server" Text="Process Payment" OnClick="btnPay_Click" Visible="false" />
             
              </div>
         
          </div>
    <asp:Panel ID="PanelServices" runat="server">
        <div class="row pull-right" style="background-color:whitesmoke;padding-right:15px;">
         <asp:Button ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click1" />
            </div>
        <asp:ValidationSummary ID="GridValsum" runat="server" ValidationGroup="GridVal" />
        <asp:GridView ID="Grid_services" runat="server" AutoGenerateColumns="False" OnRowCommand="Grid_services_RowCommand"
            Width="100%" OnRowUpdated="Grid_services_RowUpdated" OnRowUpdating="Grid_services_RowUpdating"
            OnRowCancelingEdit="Grid_services_RowCancelingEdit" OnRowEditing="Grid_services_RowEditing" OnRowDataBound="Grid_services_RowDataBound" >
            <Columns>
                <asp:TemplateField Visible="false" ItemStyle-CssClass="col-nowrap form-inline" FooterStyle-CssClass="form-inline"  ControlStyle-CssClass="form-inline" ItemStyle-Width="125px">
                   
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                            CommandArgument='<%# Bind("ID")%>' ValidationGroup="GridVal" SkinID="BtnLinkUpdate"
                            ToolTip="Update"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                 <asp:TemplateField>
                                    <ItemTemplate>
                                      
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                            Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                      
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item">
                    <ItemTemplate>
                         <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        
                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                         <asp:TextBox ID="txtDesc" runat="server" Text='<%# Bind("Description") %>' Visible="false" ></asp:TextBox>
                        <asp:Label ID="lblServiceID" runat="server" Text='<%# Bind("ServiceID") %>' Visible="false"></asp:Label>
                    </EditItemTemplate>
                    <ItemStyle Width="37%" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <asp:Label ID="blstype1" runat="server" Text='<%# Bind("FixedRateTypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblstype2" runat="server" Text='<%# Bind("FixedRateTypeName") %>'></asp:Label>
                        <asp:DropDownList ID="ddlSType" runat="server" Visible="false" ></asp:DropDownList>
                        <asp:Label ID="lblFixedRateTypeID" runat="server" Text='<%# Bind("FixedRateTypeID") %>' Visible="false"></asp:Label>
                    </EditItemTemplate>
                    <ItemStyle Width="15%" />
                </asp:TemplateField>
              
                <asp:TemplateField HeaderText="QTY" FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("QTY") %>' Visible="false"></asp:Label>
                         <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("QTY") %>' SkinID="Price_75px" MaxLength="10"></asp:TextBox>
                        <asp:CompareValidator ID="CV_QTY" runat="server" ControlToValidate="txtQty" Display="None"
                            ErrorMessage="Please enter valid qty" Operator="DataTypeCheck" Type="Double"
                            ValidationGroup="GridVal"></asp:CompareValidator>
                    </ItemTemplate>
                    <EditItemTemplate>
                       
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblfooter_qty" runat="server" Text="0" Font-Bold="true" Visible="false"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Price"  FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("SellingPrice","{0:F2}" ) %>' Visible="false"></asp:Label>
                         <asp:TextBox ID="txtSellingPrice" runat="server" Text='<%# Bind("SellingPrice","{0:F2}") %>'
                            SkinID="Price_75px" MaxLength="10"></asp:TextBox>
                        <asp:CompareValidator ID="CV_SP" runat="server" ControlToValidate="txtSellingPrice"
                            Display="None" ErrorMessage="Please enter valid Unit price" Operator="DataTypeCheck"
                            Type="Double" ValidationGroup="GridVal"></asp:CompareValidator>
                    </ItemTemplate>
                    <EditItemTemplate>
                       
                    </EditItemTemplate>
                     <FooterTemplate>
                        <asp:Label ID="lblfooter_unitprice" runat="server" Text="0" Font-Bold="true"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Width="10%"  HorizontalAlign="Right" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="TAX Rate (%)" FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblVATRate" runat="server" Text='<%# Bind("VATRate","{0:F2}" ) %>' Visible="false"></asp:Label>
                        <asp:TextBox ID="txtVATRate" runat="server" Text='<%# Bind("VATRate","{0:F2}") %>'
                            SkinID="Price_75px" MaxLength="10"></asp:TextBox>
                        <asp:CompareValidator ID="CV_vatRate" runat="server" ControlToValidate="txtVATRate"
                            Display="None" ErrorMessage="Please enter valid TAX Rate" Operator="DataTypeCheck"
                            Type="Double" ValidationGroup="GridVal"></asp:CompareValidator>
                    </ItemTemplate>
                    <EditItemTemplate>
                        
                    </EditItemTemplate>
                     <FooterTemplate>
                        <asp:Label ID="lblfooter_vatrate" runat="server" Text="0" Font-Bold="true"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Width="10%"  HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TAX" FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblVAT" runat="server" Text='<%# Bind("VAT","{0:F2}" ) %>' Visible="false"></asp:Label>
                        <asp:TextBox ID="txtVAT" runat="server" Text='<%# Bind("VAT","{0:F2}") %>'
                            SkinID="Price_75px" MaxLength="10"></asp:TextBox>
                        <asp:CompareValidator ID="CV_vat" runat="server" ControlToValidate="txtVAT"
                            Display="None" ErrorMessage="Please enter valid TAX" Operator="DataTypeCheck"
                            Type="Double" ValidationGroup="GridVal"></asp:CompareValidator>
                    </ItemTemplate>
                    <EditItemTemplate>
                        
                    </EditItemTemplate>
                     <FooterTemplate>
                        <asp:Label ID="lblfooter_vat" runat="server" Text="0" Font-Bold="true"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Width="10%"  HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total" FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Total","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <%--<asp:Label ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total", "{0:f2}")%>' ></asp:Label>--%>
                        <%# DataBinder.Eval(Container.DataItem, "Total", "{0:f2}")%>
                    </EditItemTemplate>
                     <FooterTemplate>
                        <asp:Label ID="lblfooter_total" runat="server" Text="0" Font-Bold="true"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Width="15%" HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Units" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbluc" runat="server" Text='<%# Bind("UnitConsumption","{0:F2}" ) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtuc" runat="server" Text='<%# Bind("UnitConsumption","{0:F2}") %>'
                            SkinID="Price_75px" MaxLength="10"></asp:TextBox>
                       
                    </EditItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Units" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbltu" runat="server" Text='<%# Bind("TotalUnits","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <%--<asp:Label ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total", "{0:f2}")%>' ></asp:Label>--%>
                        <%# DataBinder.Eval(Container.DataItem, "TotalUnits", "{0:f2}")%>
                    </EditItemTemplate>
                    <ItemStyle Width="20%" HorizontalAlign="Right" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Notes">
                    <ItemTemplate>
                        <asp:Label ID="lblgridnotes" runat="server" Text='<%# Bind("Notes") %>' Visible="false"></asp:Label>
                        <asp:TextBox ID="txtEnotes" runat="server" Text='<%# Bind("Notes") %>' Width="175px"></asp:TextBox>
                    </ItemTemplate>
                    <EditItemTemplate>
                        
                    </EditItemTemplate>
                     <ItemStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton SkinID="BtnLinkDelete" runat="server" ID="grid_delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID").ToString() %>'
                            OnClick="grid_delete_Click" OnClientClick="return confirm('Do you want to delete the record?');" />
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
       
       <%-- <asp:ObjectDataSource ID="obj_services" runat="server" SelectMethod="Services_SelectByIncidentID"
            TypeName="Deffinity.IncidentService.ServiceManager" OnUpdated="obj_services_Updated"
            UpdateMethod="Services_Update" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="0" Name="IncidentID" SessionField="IncidentID"
                    Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="ID" />
                <asp:Parameter Name="Description" />
                <asp:Parameter Name="QTY" />
                <asp:Parameter Name="SellingPrice" />
            </UpdateParameters>
        </asp:ObjectDataSource>--%>
        <uc2:SDTeamToCustomer ID="SDTeamToCustomer1" runat="server" Visible="false"  />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
            SelectCommand="DCBOM_SelectByIncidentID" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="0" Name="IncidentID" SessionField="IncidentID"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </asp:Panel>
      

    <asp:Panel id="pnlSummary" runat="server" style="display:none;visibility:hidden;">

    <asp:Panel ID="pnlStatus" runat="server" Visible="false" CssClass="form-group row"> 
    <div class="col-md-8">
                                       <label class="col-sm-4 control-label">   Customer Approval Status:</label>
                                      <div class="col-sm-8"><label id="lblStatus" runat="server" style="font-weight:bold;"></label>
					</div>
        </div>
    </asp:Panel>

      <div class="form-group row">
                   <div class="col-md-8">
                        <label class="col-sm-2 control-label"> Total Price:</label>
                     <div class="col-sm-8 ">  <label id="lblTotalPrice" runat="server" style="font-weight: bold;margin-bottom:0px" class="control-label">0.00
                    </label>
					</div>
				</div>
            </div>
      <div class="form-group row" style="display:none;visibility:hidden">
                   <div class="col-md-8">
                        <label class="col-sm-4 control-label">  Discount %:</label>
                     <div class="col-sm-8"> <asp:TextBox ID="txtDiscountPercent" runat="server" Width="75px"></asp:TextBox>
					</div>
				</div>
            </div>
      <div class="form-group row" style="display:none;visibility:hidden">
                   <div class="col-md-8">
                        <label class="col-sm-4 control-label"> Discount Value:</label>
                     <div class="col-sm-8"> <label id="lblDiscountValue" runat="server" style="font-weight: bold;">
                    </label>
					</div>
				</div>
            </div>
      <div class="form-group row">
                   <div class="col-md-8" style="display:none;visibility:hidden">
                        <label class="col-sm-4 control-label"> Revised Price:</label>
                     <div class="col-sm-8"><label id="lblRevisedPrice" runat="server" style="font-weight: bold;">
                    </label>
					</div>
				</div>
            </div>
      <div class="form-group row">
                   <div class="col-md-8" style="display:none;visibility:hidden">
                        <label class="col-sm-4 control-label">  <label id="lblUnit_title" runat="server"> Unit Consumption:</label></label>
                     <div class="col-sm-8">  <label id="lbluc" runat="server" style="font-weight: bold;">
                    </label>
					</div>
				</div>
            </div>
      <div class="form-group row">
                   <div class="col-md-8">
                        <label class="col-sm-2 control-label">Notes:</label>
                     <div class="col-sm-8"> <asp:TextBox ID="txtNotes" SkinID="txtMulti" runat="server"></asp:TextBox>
					</div>
				</div>
            </div>
      <div class="form-group row">
                   <div class="col-md-8">
                       <br />
                        <label class="col-sm-2 control-label"> </label>
                       <div class="col-sm-8"> <asp:Button ID="btnUpdateTotals" runat="server" SkinID="btnUpdate" OnClick="btnUpdateTotals_Click" />
                    <asp:Button ID="btnSubmitToCustomer" runat="server" Text="Submit Revised Price to Customer"
                        OnClick="btnSubmitToCustomer_Click1" Visible="false" /></div>
                       </div>
          </div>
        </asp:Panel>
    <div>
       
    </div>
</asp:Panel>

<asp:Label ID="lblStorageNew" runat="server"></asp:Label>
  <ajaxToolkit:ModalPopupExtender ID="mdlExnter" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnPopShow" PopupControlID="pnlStorageNew" CancelControlID="btnPopClose" >
</ajaxToolkit:ModalPopupExtender>
        
       <asp:Panel ID="pnlStorageNew" runat="server" BackColor="White" Style="display:none;"
                       Width="850px" Height="480px" CssClass="panel panel-color panel-info" ScrollBars="None">
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblPnltitle" runat="server" Text="Supplier Catalog"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="btnPopClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
								
							</div>
						</div>
    <div class="panel-body">
 


                  
         
                        
  <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-group row">
	<div class="col-xs-6 form-inline">
           <asp:TextBox ID="txtItemDescription" runat="server" MaxLength="100" SkinID="txt_80"></asp:TextBox><asp:Button ID="imgVendorSearch" runat="server"
                                              SkinID="btnSearch" onclick="imgVendorSearch_Click" CausesValidation="false" style="float:right"/>
	</div>



     <div class="form-group row" style="display:none;visibility:hidden;">
      <div class="col-xs-4">
           <label class="col-sm-3 control-label">Supplier</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlVendors" runat="server"></asp:DropDownList>
              
            </div>
	  </div>
     <div class="col-xs-4">
           <label class="col-sm-3 control-label"><%= Deffinity.systemdefaults.GetCategoryName() %></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
                 <ajaxToolkit:CascadingDropDown ID="ccdCategory" runat="server" TargetControlID="ddlCategory"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetCategoryByTypeOfRequest" LoadingText="[Loading ...]" />
            </div>
	</div>
         <div class="col-xs-4">
           <label class="col-sm-6 control-label"><%= Deffinity.systemdefaults.GetSubCategoryName() %></label>
           <div class="col-sm-6">
                     <asp:DropDownList ID="ddlSubCategory" runat="server"></asp:DropDownList>
                     <ajaxToolkit:CascadingDropDown ID="ccdSubCategory" runat="server" TargetControlID="ddlSubCategory"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetSubCategory" LoadingText="[Loading...]" ParentControlID="ddlCategory" />
            </div>
	</div>
         </div>
     <div class="form-group row" style="display:none;visibility:hidden;">
     
     <div class="col-xs-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Type%></label>
           <div class="col-sm-9">
                  <asp:DropDownList ID="DropDownList4" runat="server">
                                            <asp:ListItem Value="0">Please select...</asp:ListItem>
                                            <asp:ListItem Value="1">Labour</asp:ListItem>
                                            <asp:ListItem Value="2">Material</asp:ListItem>
                                            <asp:ListItem Value="3">Service</asp:ListItem>
                                        </asp:DropDownList>
            </div>
	</div>
    
     </div>
       </div>
 
        
                            <div>
                                <asp:Button ID="imgUpdate" runat="server" SkinID="btnApply" OnClick="imgUpdate_Click" />
                            </div>
   <asp:Label ID="lblErr" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
   <asp:Panel ID="panel_grid" runat="server" Width="100%" Height="230px" ScrollBars="Auto">
                            <asp:GridView ID="GridView2" runat="server"  AutoGenerateColumns="False"
                                 OnRowCommand="GridView2_RowCommand"
                                DataKeyNames="ID" EmptyDataText="<%$ Resources:DeffinityRes,Nodataavailable %>" width="100%">
                                <Columns>
                                    <asp:TemplateField Visible='false'>
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                             <asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                             <asp:Label ID="lblVendorID" runat="server" Text='<%# Bind("VID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        
                                        <ItemStyle  />
                                        <ItemTemplate>
                                           
                                            <asp:CheckBox ID="chkbox" runat="server" />
                                      </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField>
                                    <ItemTemplate>
                                      
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                            Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                      
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Description%>" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="250px"  ControlStyle-Width="250px">
                                       
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>' ></asp:Label>
                                      </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="False">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VendorName" HeaderText="Supplier" ItemStyle-HorizontalAlign="left">
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Type%>" Visible="false"  >
                                        <ItemTemplate>
                                            <asp:Label ID="lblAvil12" runat="server" Text='<%#GetItemsType(DataBinder.Eval(Container.DataItem,"Type").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes,BuyingPrice%>" ItemStyle-HorizontalAlign="Right" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAvil"  runat="server" Text='<%#Bind("BP","{0:F2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Unit Price"  
                                        ItemStyle-HorizontalAlign="Right">
                                        
                                        <ItemTemplate>
                                        <asp:Label ID="lblUnitPrice" runat="server"  Text='<%#Bind("UnitPrice","{0:F2}")%>'></asp:Label>
                                            
                                        </ItemTemplate>
                                       
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SellingPrice%>"  
                                        ItemStyle-HorizontalAlign="Right">
                                        
                                        <ItemTemplate>
                                        <asp:Label ID="lblSP" runat="server"  Text='<%#Bind("SP","{0:F2}")%>'></asp:Label>
                                            <asp:TextBox ID="txtQtyReq" Width="50px" Visible="false" runat="server" Text='<%#Bind("SP","{0:F2}")%>'></asp:TextBox>
                                        </ItemTemplate>
                                       
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                             </asp:Panel>
                            </ContentTemplate>
                         <Triggers>
                        <asp:PostBackTrigger ControlID="imgUpdate" />
                        </Triggers>
                    </asp:UpdatePanel>
                        <div style="text-align: left">
                            <asp:Button runat="server" ID="imgItemEdit" Style="display: none" />
                        </div>
        </div>
            
                    </asp:Panel>
 <div class="form-group row">
          <div class="col-md-12">
              <label class="col-sm-1 control-label">File(s)</label>
               <div class="col-sm-10">
                   <asp:Button ID="btnUploadfile" runat="server" SkinID="btnDefault" Text="Upload file(s)" />
               </div>
          </div>
    </div>
    <div class="form-group row">
          <div class="col-md-12">
              <label class="col-sm-1 control-label"></label>
               <div class="col-sm-6">
                   <asp:GridView ID="gridfiles" runat="server" AutoGenerateColumns="false" OnRowCommand="gridfiles_RowCommand" >
                       <Columns>
                       <asp:BoundField DataField="Text" HeaderText="File Name" Visible="false"  />
                           <asp:TemplateField HeaderText="File Name">
                               <ItemTemplate>
                                   <asp:LinkButton ID="btnDownload" runat="server" CommandName="Download" CommandArgument='<%# Eval("Text") %>' Text='<%# Eval("Text") %>'></asp:LinkButton>
                               </ItemTemplate>
                           </asp:TemplateField>
                      <asp:TemplateField ItemStyle-Width="30px">
                           <ItemTemplate>
                            <%-- <asp:LinkButton ID = "lnkDelete" OnClick = "DeleteFile" CausesValidation="false" 
                                 Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server"></asp:LinkButton>--%>
                     <asp:LinkButton runat="server" ID="lnkDelete" CausesValidation="false" SkinID="BtnLinkDelete"
                               CommandArgument = '<%# Eval("Value") %>'
                          OnClientClick="return confirm('Do you want to delete the record?');" OnClick="DeleteFile"></asp:LinkButton>
                           </ItemTemplate>
                      </asp:TemplateField>
                 </Columns>
                </asp:GridView>
               </div>
          </div>
    </div>


<asp:Label ID="lblclosefile" runat="server"></asp:Label>
  <ajaxToolkit:ModalPopupExtender ID="mdlFileupload" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnUploadfile" PopupControlID="pnlFileupload" CancelControlID="lblclosefile" >
</ajaxToolkit:ModalPopupExtender>
        
       <asp:Panel ID="pnlFileupload" runat="server" BackColor="White" Style="display:none;"
                       Width="850px" Height="480px" CssClass="panel panel-color panel-info" ScrollBars="None">
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="Label6" runat="server" Text="Upload files"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lblClosefileupload" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" OnClick="ClosePopup"/>
								
							</div>
						</div>
    <div class="panel-body">
        	<script type="text/javascript">
                jQuery(document).ready(function ($) {
                   
                    var i = 1,
                        $example_dropzone_filetable = $("#example-dropzone-filetable"),
                        example_dropzone = $("#advancedDropzone").dropzone({
                            url: 'UploadHandler.ashx?callid=' + getQuerystring('callid'),

                            // Events
                            addedfile: function (file) {
                                if (i == 1) {
                                    $example_dropzone_filetable.find('tbody').html('');
                                }

                                var size = parseInt(file.size / 1024, 10);
                                size = size < 1024 ? (size + " KB") : (parseInt(size / 1024, 10) + " MB");

                                var $el = $('<tr>\
													<td class="text-center">'+ (i++) + '</td>\
													<td>'+ file.name + '</td>\
													<td><div class="progress progress-striped"><div class="progress-bar progress-bar-warning"></div></div></td>\
													<td>'+ size + '</td>\
													<td>Uploading...</td>\
												</tr>');

                                $example_dropzone_filetable.find('tbody').append($el);
                                file.fileEntryTd = $el;
                                file.progressBar = $el.find('.progress-bar');
                            },

                            uploadprogress: function (file, progress, bytesSent) {
                                file.progressBar.width(progress + '%');
                            },

                            success: function (file) {
                                file.fileEntryTd.find('td:last').html('<span class="text-success">Uploaded</span>');
                                file.progressBar.removeClass('progress-bar-warning').addClass('progress-bar-success');
                            },

                            error: function (file) {
                                file.fileEntryTd.find('td:last').html('<span class="text-danger">Failed</span>');
                                file.progressBar.removeClass('progress-bar-warning').addClass('progress-bar-red');
                            }
                        });

                    $("#advancedDropzone").css({
                        minHeight: 200
                    });

                });
					</script>
					
					<br />
					<div class="row">
						<div class="col-sm-3 text-center">
						
							<div id="advancedDropzone" class="droppable-area">
								Drop Files Here
							</div>
							
						</div>
						<div class="col-sm-9">
							
							<table class="table table-bordered table-striped" id="example-dropzone-filetable">
								<thead>
									<tr>
										<th width="1%" class="text-center">#</th>
										<th width="50%">Name</th>
										<th width="20%">Upload Progress</th>
										<th>Size</th>
										<th>Status</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td colspan="5">Files list will appear here</td>
									</tr>
								</tbody>
							</table>
							
						</div>
					</div>
        </div>
           </asp:Panel>

	

	<!-- Imported styles on this page -->
	<link rel="stylesheet" href="../../../Content/assets/js/dropzone/css/dropzone.css">

	<!-- Imported scripts on this page -->
	<script src="../../../Content/assets/js/dropzone/dropzone.min.js"></script>
