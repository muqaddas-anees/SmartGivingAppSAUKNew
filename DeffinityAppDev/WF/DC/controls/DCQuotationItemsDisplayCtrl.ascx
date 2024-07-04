<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DCQuotationItemsDisplayCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.DCQuotationItemsDisplayCtrl" %>
<%@ Register Src="~/WF/DC/MailControls/SDQuoteToCustomer.ascx" TagName="SDTeamToCustomer"
    TagPrefix="uc2" %>
    <%@ Register Src="~/WF/DC/controls/CustomerOrder.ascx" TagName="CustomerOrder"
    TagPrefix="uc3" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 

  <script src="../../Content/assets/js/ckeditor/ckeditor.js"></script>
    <script src="../../Content/assets/js/ckeditor/adapters/jquery.js"></script>
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
.galert-success {
    background-color: #8dc63f;
    border-color: #8dc63f;
    color: #ffffff;
}
.galert {
    padding: 15px;
    margin-bottom: 18px;
    border: 1px solid transparent;
    border-radius: 0px;
}
</style>
 
 <style>
        .price_header{
            font-size:18px;font-weight:bold;color: #717272;
        }
        .price_text{
            font-size:18px;font-weight:bold;color: #717272;text-align:right;
        }
         .price_text_white{
            font-size:18px;font-weight:bold;color: white;text-align:right;
        }
        .price_lable{
            font-size:15px;font-weight:bold;color: #717272;text-align:right;
        }
    </style>
<asp:Label ID="lblservice" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
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
   
  
   
    <div>
      
    
    </div>
   

  <%-- <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>  Quote </strong>
            <hr class="no-top-margin" />
            </div>
    </div>--%>
   
    <div class="form-group row" id="pnlTopAddPanel" runat="server" style="margin-bottom:0px;">
        <div class="col-md-6">
       <asp:Label ID="lblQuotemsg" runat="server" CssClass="galert galert-success" Text="The Customer Agreed This Quotation" Visible="false"></asp:Label> 
            </div>
        <div class="col-md-6" style="text-align:right">
           
            <asp:Button ID="btnAddMemberShip" runat="server" SkinID="btnDefault" Text="Apply for Membership" OnClick="btnAddMemberShip_Click" Visible="false"></asp:Button>
            <asp:Button ID="btnAddNew" runat="server" SkinID="btnDefault" Text="Add Item to Quotation" OnClick="btnAddNew_Click"></asp:Button>
             <asp:Button ID="btnPopShow" runat="server" SkinID="btnDefault" Text="Look Up" OnClick="btnPopShow_Click"></asp:Button>
            <asp:Button ID="btnRaiseInvoice" runat="server" SkinID="btnDefault" Text="Raise Invoice" OnClick="btnRaiseInvoice_Click"></asp:Button>
            

            
            </div>
    </div>
    <style>
        .price_header{
            font-size:18px;font-weight:bold;color: #717272;
        }
        .price_text{
            font-size:18px;font-weight:bold;color: #717272;/*float:right;text-align:right;*/
        }
        .price_lable{
            font-size:15px;font-weight:bold;color: #717272;/*float:right;text-align:right;*/
        }
    </style>

     <asp:ListView Visible="false" ID="list_Customfields" runat="server" InsertItemPosition="None" OnItemCanceling="list_Customfields_ItemCanceling" OnItemCommand="list_Customfields_ItemCommand" OnItemDataBound="list_Customfields_ItemDataBound" OnItemEditing="list_Customfields_ItemEditing">
           <LayoutTemplate>
              <div class="form-group ">
        <div class="col-md-12">
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  </div>
              </LayoutTemplate>
          <ItemTemplate>
              <div class="well">
                   <div class="form-group row">
        <div class="col-md-3" style="vertical-align:central;text-align:center;">
            <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>' />

            </div>
                        <div class="col-md-9">
                            <div class="form-group row">
        <div class="col-md-10">

              <div class="form-group row">
        <div class="col-md-10">
           <asp:Label ID="lblLable" runat="server" Text='<%# Eval("Description") %>' CssClass="price_header"></asp:Label> 
             <%--<asp:Button ID="btnManageItems" runat="server" Text="Manage Items" SkinID="btnDefault" style="float:right;" />--%>
           <%-- <hr class="no-top-margin" />--%>
            </div>
                       <div class="col-md-2 form-inline">
                           <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="cEdit"
                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                        </asp:LinkButton>
                           <asp:LinkButton ID="btnDeleteOptions" runat="server" SkinID ="BtnLinkDelete" CommandArgument='<%# Bind("ID")%>'
                               OnClientClick="return confirm('Do you want to delete the record?');"  CommandName="cDelete"></asp:LinkButton>
                           </div>
                      </div>
                  
                  <div class="form-group row">
                       <div class="col-md-6">
                           <span><asp:Literal ID="lblNotes" runat="server" Text='<%# Eval("Notes") %>' ></asp:Literal> 

                             
                           </span>
                           </div>
                     
                      </div>
                   <div class="form-group row">
                       <div class="col-md-10">

                           <asp:Label ID="lblMdata" runat="server" Text='<%# Eval("mdata") %>'></asp:Label>

                           </div>
                       </div>
            </div>
                      </div>

            </div>
                       </div>

                
    </div>

              </ItemTemplate>
               </asp:ListView>
     
    <asp:Panel ID="pnlQuoteTab" runat="server" >
                 <div class="form-group row" id="pnlAddOptonsButton" runat="server" style="margin-bottom:0px;">
       <%-- <div class="col-md-12">
       
            </div>--%>
        </div>
           
    <div class="form-group row" style="margin-bottom:0px;">
        <div class="col-md-12">
           <div class="form-wizard">
           <asp:Literal ID="lbltext" runat="server" Visible="false" ></asp:Literal>
           
        </div>
            </div>
        </div>
     <div class="form-group row" style="margin-bottom:0px;">
        <div class="col-md-12">
           
            </div>
    </div>

    <asp:Panel ID="pnlOptions" runat="server">
        <asp:Panel ID="pnlAdd" runat="server">
    
      <div class="form-group row">
           
         
          <div class="col-md-5  form-inline pull-right">
               
              <asp:Button ID="btnPay" runat="server" Text="Process Payment" OnClick="btnPay_Click" style="display:none;visibility:hidden;" />
              <asp:Button ID="btnSend" runat="server" Text="Send to Client" SkinID="btnDefault" OnClick="btnSend_Click" style="display:none;visibility:hidden;"/>
             
          </div>
         
          </div>
            </asp:Panel>

    <asp:Panel ID="PanelServices" runat="server">
      <div class="row pull-right" style="background-color:whitesmoke;padding-right:15px">
         <asp:Button ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click1" Visible="false" />
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
                <asp:TemplateField HeaderText="Item" ItemStyle-CssClass="col-nowrap">
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
                    <ItemStyle Width="10%" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Cost Price" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblSellingPrice" runat="server" Text='<%# Bind("SellingPrice","{0:F2}" ) %>'></asp:Label>
                       
                    </ItemTemplate>
                    <EditItemTemplate>
                         <asp:TextBox ID="txtSellingPrice" runat="server" Text='<%# Bind("SellingPrice","{0:F2}") %>'
                            SkinID="Price_75px" MaxLength="10"></asp:TextBox>
                        <asp:CompareValidator ID="CV_SP" runat="server" ControlToValidate="txtSellingPrice"
                            Display="None" ErrorMessage="Please enter valid Cost Price" Operator="DataTypeCheck"
                            Type="Double" ValidationGroup="GridVal"></asp:CompareValidator>
                    </EditItemTemplate>
                    <ItemStyle Width="10%"  HorizontalAlign="Right" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="QTY">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
                       
                    </ItemTemplate>
                    <EditItemTemplate>
                         <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("QTY") %>' SkinID="Price_75px" MaxLength="10"></asp:TextBox>
                        <asp:CompareValidator ID="CV_QTY" runat="server" ControlToValidate="txtQty" Display="None"
                            ErrorMessage="Please enter valid qty" Operator="DataTypeCheck" Type="Double"
                            ValidationGroup="GridVal"></asp:CompareValidator>
                    </EditItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                </asp:TemplateField>
                 
                 <asp:TemplateField HeaderText="Total" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Total","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <%--<asp:Label ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total", "{0:f2}")%>' ></asp:Label>--%>
                        <%# DataBinder.Eval(Container.DataItem, "Total", "{0:f2}")%>
                    </EditItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Right" />
                </asp:TemplateField>
                
                  <asp:TemplateField HeaderText="Sales Price">
                    <ItemTemplate>
                        <asp:Label ID="lblSalesPrice" runat="server" Text='<%# Bind("SalesPrice","{0:F2}" ) %>'></asp:Label>
                        
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSalesPrice" runat="server" Text='<%# Bind("SalesPrice","{0:F2}") %>'
                            SkinID="Price_75px" MaxLength="10"></asp:TextBox>
                        <asp:CompareValidator ID="CV_txtSalesPrice" runat="server" ControlToValidate="txtSalesPrice"
                            Display="None" ErrorMessage="Please enter valid sales price" Operator="DataTypeCheck"
                            Type="Double" ValidationGroup="GridVal"></asp:CompareValidator>
                    </EditItemTemplate>
                    <ItemStyle Width="10%"  HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TAX">
                    <ItemTemplate>
                        <asp:Label ID="lblVAT" runat="server" Text='<%# Bind("VAT", "{0:F2}") %>'></asp:Label>
                       
                    </ItemTemplate>
                    <EditItemTemplate>
                         <asp:TextBox ID="txtVAT" runat="server" Text='<%# Bind("VAT", "{0:F2}") %>' SkinID="Price_75px" MaxLength="10"></asp:TextBox>
                        <asp:CompareValidator ID="CV_vat" runat="server" ControlToValidate="txtVAT" Display="None"
                            ErrorMessage="Please enter valid TAX" Operator="DataTypeCheck" Type="Double"
                            ValidationGroup="GridVal"></asp:CompareValidator>
                    </EditItemTemplate>
                    <ItemStyle Width="8%" HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Notes">
                    <ItemTemplate>
                        <asp:Label ID="lblgridnotes" runat="server" Text='<%# Bind("Notes") %>' ></asp:Label>
                        
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEnotes" runat="server" Text='<%# Bind("Notes") %>' SkinID="txt_90"></asp:TextBox>
                    </EditItemTemplate>
                     <ItemStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton SkinID="BtnLinkDelete" runat="server" ID="grid_delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID").ToString() %>'
                            OnClick="grid_delete_Click" OnClientClick="return confirm('Do you want to delete the record?');" />
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
       
      
        <uc2:SDTeamToCustomer ID="SDTeamToCustomer1" runat="server" Visible="false"  />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
            SelectCommand="QuotationItems_SelectByCallID" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="0" Name="IncidentID" SessionField="IncidentID"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </asp:Panel>
      

    <asp:Panel id="pnlSummary" runat="server">

    <asp:Panel ID="pnlStatus" runat="server" Visible="false" CssClass="form-group row"> 
    <div class="col-md-8">
                                       <label class="col-sm-4 control-label">   Customer Approval Status:</label>
                                      <div class="col-sm-8"><label id="lblStatus" runat="server" style="font-weight:bold;"></label>
					</div>
        </div>
    </asp:Panel>

      <div class="form-group row">
                   <div class="col-md-8" style="padding-left:0px">
                        <label class="col-sm-2 control-label"> Total Price:</label>
                     <div class="col-sm-8 ">  <label id="lblTotalPrice" runat="server" style="font-weight: bold;margin-bottom:0px" class="control-label">0.00
                    </label>
					</div>
				</div>
            </div>
          <div class="form-group row" id="pnlDiscountpanle" runat="server">
                   <div class="col-md-8" style="padding-left:0px">
                        <label class="col-sm-2 control-label" style="font-weight:bold">  Your Price:</label>
                     <div class="col-sm-8 ">  <label id="lblFinalprice" runat="server" style="font-weight: bold;margin-bottom:0px" class="control-label">0.00
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
      <div class="form-group row" style="display:none;visibility:hidden;">
                   <div class="col-md-8" style="display:none;visibility:hidden">
                        <label class="col-sm-4 control-label">  <label id="lblUnit_title" runat="server"> Unit Consumption:</label></label>
                     <div class="col-sm-8">  <label id="lbluc" runat="server" style="font-weight: bold;">
                    </label>
					</div>
				</div>
            </div>
      <div class="form-group row" style="display:none;visibility:hidden;">
                   <div class="col-md-8" style="padding-left:0px;padding-top:20px">
                        <label class="col-sm-2 control-label">Notes:</label>
                     <div class="col-sm-8"> <asp:TextBox ID="txtNotes" SkinID="txtMulti" runat="server" TextMode="MultiLine"></asp:TextBox>
					</div>
				</div>
            </div>
        
      <div class="form-group row" style="display:none;visibility:hidden;">
                   <div class="col-md-8">
                       <br />
                        <label class="col-sm-2 control-label"> </label>
                       <div class="col-sm-8"> <asp:Button ID="btnUpdateTotals" runat="server" SkinID="btnUpdate" OnClick="btnUpdateTotals_Click" />
                   </div>
                       </div>
          </div>

          <asp:Panel ID="pnlOffer" runat="server" Visible="false" style="display:none;visibility:hidden;">
             <div class="form-group row" >
        <div class="col-md-7 text-bold">
        <strong>  Take advantage of one of the following Maintenance Plans and save even more:  </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
            <div class="form-group row">
        <div class="col-md-6">
            <asp:GridView ID="GridOffers" runat="server" OnRowCommand="GridOffers_RowCommand"
            Width="100%" OnRowUpdated="GridOffers_RowUpdated" OnRowUpdating="GridOffers_RowUpdating"
            OnRowCancelingEdit="GridOffers_RowCancelingEdit" OnRowEditing="GridOffers_RowEditing">
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
                      <asp:TemplateField HeaderText="Plan ">
                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("Title") %>' Width="175px"></asp:TextBox>
                    </EditItemTemplate>
                     <ItemStyle Width="20%" />
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Annual Price">
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("AnnualPrice","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblPrice1" runat="server" Text='<%# Bind("AnnualPrice","{0:F2}") %>'></asp:Label>
                    </EditItemTemplate>
                     <ItemStyle Width="20%" HorizontalAlign="Right" />
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Save">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscountPrice" runat="server" Text='<%# Bind("DiscountPrice","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDiscountPrice" runat="server" Text='<%# Bind("DiscountPrice") %>' Width="175px" SkinID="Price_150px"></asp:TextBox>
                    </EditItemTemplate>
                     <ItemStyle Width="20%" HorizontalAlign="Right" />
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                        <asp:Button ID="btnSendMail" runat="server" Text="Please contact me" CommandName="contact" CommandArgument='<%# Bind("ID")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                       
                    </EditItemTemplate>
                     <ItemStyle Width="20%" />
                </asp:TemplateField>
                      <asp:TemplateField HeaderText="" >
                    <ItemTemplate>
                        <asp:Button ID="btnApplyQuotation" runat="server" Text="Apply to Quotation" CommandName="apply" CommandArgument='<%# Bind("ID")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                       
                    </EditItemTemplate>
                     <ItemStyle Width="20%" />
                </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
                </div>
        </asp:Panel>


        <div class="form-group row" style="visibility:hidden;display:none;">
        <div class="col-md-12">
     <div class="form-group row">
        <div class="col-md-12" style="padding-left:30px;padding-right:30px">

      <div class="form-group well">
        <div class="col-md-12">
            <div class="form-group form-inline">
                
                <p class="bg-muted" style="display:inline-block;margin:15px">
                    <label class="price_text" style="font-size:22px;" id="blbl_StandardPrice" runat="server">0.00 </label><br />
                     <label class="price_text">Standard Price</label>
                    
                    </p>
                     <p class="bg-success" style="display:inline-block;margin:15px;">
                    <label class="price_text_white" style="font-size:22px;" id="blblMemberPrice" runat="server">0.00 </label><br />
                     <label class="price_text_white">Member Price</label>
                    
                    </p>
                 <p class="bg-success" style="display:inline-block;margin:15px">
                    <label class="price_text_white" style="font-size:22px"  id="blblYourSavings" runat="server">0.00</label><br />
                     <label class="price_text_white">Your Savings </label> 
                    
                    </p>
                 <p class="bg-muted" style="display:inline-block;margin:15px">
                    <label class="price_text" style="font-size:22px"  id="blblSubTotal" runat="server">0.00</label><br />
                     <label class="price_text">Sub Total </label> 
                    
                    </p>
                 <p class="bg-muted" style="display:inline-block;margin:15px">
                    <label class="price_text" style="font-size:22px"  id="blblTax" runat="server">0.00</label><br />
                     <label class="price_text">Tax </label> 
                    
                    </p>
                 <p class="bg-muted" style="display:inline-block;margin:15px">
                    <label class="price_text" style="font-size:22px"  id="blblTotal" runat="server">0.00</label><br />
                     <label class="price_text">Total </label> 
                    
                    </p>
                 
                </div>
                </div>
                  </div>
            </div>
         </div>
            </div>
         </div>
        </asp:Panel>


     
    </asp:Panel>


    

    
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
 


                  
         
                        
  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-group row">
     <div class="form-group row">
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
     <div class="form-group row">
     
     <div class="col-xs-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Type%></label>
           <div class="col-sm-9">
                  <asp:DropDownList ID="ddlSelect" runat="server">
                                            <asp:ListItem Value="0">Please select...</asp:ListItem>
                                            <asp:ListItem Value="1">Labour</asp:ListItem>
                                            <asp:ListItem Value="2">Material</asp:ListItem>
                                            <asp:ListItem Value="3">Service</asp:ListItem>
                                        </asp:DropDownList>
            </div>
	</div>
     <div class="col-xs-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Item%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtItemDescription" runat="server" MaxLength="100"></asp:TextBox> 
            </div>
	</div>
	<div class="col-xs-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-6">
                 <asp:Button ID="imgVendorSearch" runat="server"
                                              SkinID="btnSearch" onclick="imgVendorSearch_Click" CausesValidation="false" style="float:right"/>
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




    <asp:Label ID="lbladdNewItem1" runat="server"></asp:Label>
  <ajaxToolkit:ModalPopupExtender ID="mdlAddItem" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lbladdNewItem1" PopupControlID="pnlAddNewItems" CancelControlID="lbtnCloseItem" >
</ajaxToolkit:ModalPopupExtender>
        
       <asp:Panel ID="pnlAddNewItems" runat="server" BackColor="White" Style="display:none;"
                       Width="650px" Height="490px" CssClass="panel panel-color panel-info" ScrollBars="None">
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblItemAddHeader" runat="server" Text="Add Item"></asp:Label> 
                               

							</h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnCloseItem" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
								
							</div>
						</div>
    <div class="panel-body">
 


                  
         
                        
  <asp:UpdatePanel ID="updatePanleAddItem" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                             
 <div  class="form-group row">
     <asp:HiddenField ID="hItemID" runat="server" Value ="0" />
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid Quantity"
                        ValidationExpression="(^-?\d\d*$)" ValidationGroup="Service" ControlToValidate="txtQty" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQty"
                        ErrorMessage="Please enter Quantity" ValidationGroup="Service"  Display="Dynamic"></asp:RequiredFieldValidator>
       
    </div>
    <div class="form-group row">
              <div class="col-md-10">
                   <label class="col-sm-3 control-label"> Item </label>
                  <div class="col-sm-8"> 
                      <asp:DropDownList ID="ddlItems" runat="server"  Visible="false"></asp:DropDownList>
                      <asp:TextBox ID="txtSearch" runat="server" MaxLength="500" ></asp:TextBox>
                      <asp:HiddenField ID="hfCustomerId" runat="server" />
                      <asp:HiddenField ID="hCatelogID" runat="server" />
                      <asp:HiddenField ID="hImageID" runat="server" Value="00000000-0000-0000-0000-000000000000" />
                     
                      </div>
                  </div>
        </div>
    <div class="form-group row">
         <div class="col-md-10">
              <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Type%></label>
                  <div class="col-sm-8">
                      <asp:DropDownList ID="ddlstype" runat="server"></asp:DropDownList>
                      </div>
             </div>
        </div>
    <div class="form-group row">
          <div class="col-md-10">
                <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.UnitPrice%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtCost" runat="server" SkinID="Price_100px" MaxLength="10" Text="0.00"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="txtFilterCost" runat="server" ValidChars="0123456789." TargetControlID="txtCost"></ajaxToolkit:FilteredTextBoxExtender>
                                          </div>
               </div>
        </div>
    <div class="form-group row">
              <div class="col-md-10">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Quantity%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtQty" runat="server" SkinID="txt_100px" MaxLength="10" Text="1"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredQTY" runat="server" ValidChars="0123456789" TargetControlID="txtQty"></ajaxToolkit:FilteredTextBoxExtender>
                   
					</div>
				</div>
        </div>
                            <div class="form-group row" id="pnlAddItemVat" runat="server" visible="false">
          <div class="col-md-10">
                <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.VAT%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtAddItemVat" runat="server" SkinID="Price_100px" MaxLength="10" Text="0.00"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="0123456789." TargetControlID="txtVat"></ajaxToolkit:FilteredTextBoxExtender>
                                          </div>
               </div>
        </div>
    
                             <div class="form-group row">
              <div class="col-md-10">
                   <label class="col-sm-3 control-label"> Notes </label>
                  <div class="col-sm-8"> 
                      
                      <asp:TextBox ID="txtItemNotes" runat="server" MaxLength="500" SkinID="txtMulti" Height="60px" TextMode="MultiLine" ></asp:TextBox>
                     
                     
                      </div>
                  </div>
        </div>
                             <div class="form-group row">
                        <div class="col-md-10">
                                       <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.UploadImage%></label>
                                      <div class="col-sm-8"><asp:FileUpload ID="FileUploadMaterial" runat="server"></asp:FileUpload>
                                  <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                        ControlToValidate="FileUploadMaterial" Display="None" ErrorMessage="Please select JPG,GIF or PNG."
                                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*.*))+\.(jpg|JPG|gif|GIF|png|PNG)$"
                                        ValidationGroup="Service"></asp:RegularExpressionValidator>--%>
					</div>
				</div>

                        </div>
    <div class="form-group row">
        <div class="col-md-10">
            <label class="col-sm-3 control-label"> </label>
             <div class="col-sm-4">
                  <asp:LinkButton ID="btnAdd" runat="server" SkinID="btnSubmit" ValidationGroup="Service"
                        OnClick="btnAdd_Click" />
               <asp:LinkButton SkinID="btnCancel" runat="server" ID="back" Text="Cancel" Visible="false" ></asp:LinkButton>
             <asp:Button ID="btnSubmitToCustomer" runat="server" Text="Submit to Customer"
                        OnClick="btnSubmitToCustomer_Click1" Visible="false" />
                 </div>
            </div>
      
             </div>
                     <div class="form-group row">
             <div class="col-md-2" style="display:none;visibility:hidden;">
                                       <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.VAT%></label>
                                      <div class="col-sm-6"> <asp:TextBox ID="txtVAT" runat="server" SkinID="txt_100px" MaxLength="10" Text="1"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789." TargetControlID="txtVAT"></ajaxToolkit:FilteredTextBoxExtender>
                   
					</div>
				</div>
        </div>       
    <div class="form-group row">
    <div class="form-group row">
         <div class="col-md-1" style="display:none;visibility:hidden">
             <asp:LinkButton ID="btnAddItem" runat="server" SkinID="BtnLinkAdd" OnClick="btnAddItem_Click" Visible="false"></asp:LinkButton>
                      <asp:LinkButton ID="btnAdditemCancel" runat="server" SkinID="BtnLinkCancel" OnClick="btnAdditemCancel_Click" Visible="false"></asp:LinkButton>
            </div>
        </div>
       
    </div>
        
                          
   
                            </ContentTemplate>
                         <Triggers>
                        <asp:PostBackTrigger ControlID="btnAdd" />
                        </Triggers>
                    </asp:UpdatePanel>
                       
        </div>
            
                    </asp:Panel>






    </asp:Panel>

 <asp:Panel ID="pnlAccept" runat="server" Visible="false">
    <div class="form-group row">
              <div class="col-md-12" style="padding-top:20px;">
                  <asp:Button ID="btnAcceptOption" runat="server" Text="Accept Quotation" OnClick="btnAcceptOption_Click" />
                  </div>
        </div>
</asp:Panel>

<%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>
