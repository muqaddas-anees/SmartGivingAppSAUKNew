<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DCQuotationCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.DCQuotationCtrl" %>
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
    <%--<br />
    <div class="clr"></div>
    <br />
    <div class="clr"></div>--%>
  
   
    <div>
      
    
    </div>
   

  <%-- <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong> Quote </strong>
            <hr class="no-top-margin" />
            </div>
    </div>--%>
   <div class="form-wizard">
 <ul id="countrytabs" class="tabs">
 <li class="ms-hover">
       
            <asp:HyperLink ID="lbtnQuote"  Target="_self" runat="server" Text="Customer Estimates">Customer Quotation</asp:HyperLink></li>
         <li class="ms-hover">
              <asp:HyperLink ID="lbtnAttach"  Target="_self" runat="server" Text="Attach Scope of Works">Attach Scope of Works</asp:HyperLink>
             </li>
         <li class="ms-hover" style="display:none;visibility:hidden;">
              <asp:HyperLink ID="lbtnFinance"  Target="_self" runat="server" Text="Finance ">Finance</asp:HyperLink>
             </li>
        </ul>
    </div>
    <style>
        .price_header{
            font-size:18px;font-weight:bold;color: #717272;
        }
        .price_text{
            font-size:17px;font-weight:bold;color: #717272;text-align:right;
        }
        .price_lable{
            font-size:15px;font-weight:bold;color: #717272;text-align:right;
        }
    </style>
    <asp:Panel ID="pnlQuoteTab" runat="server">
         <div class="form-group ">
        <div class="col-md-12">
            &nbsp;
            </div>
                            </div>
         <div class="form-group row" id="pnlAddOptonsButton" runat="server">
        <div class="col-md-12" style="text-align:right;">
       <asp:Button ID="btnAddOptions" runat="server" Text="Create Quotation" SkinID="btnDefault" style="text-align:right;" />
            <asp:Button ID="btnViewCompare" runat="server" Text="Compare Options" SkinID="btnDefault" style="text-align:right;" OnClick="btnViewCompare_Click" />
            <asp:Button ID="btnSend" runat="server" Text="Send to Client" SkinID="btnDefault" OnClick="btnSubmitToCustomer_Click1" />
            </div>
        </div>
           <asp:ListView Visible="false" ID="list_Customfields" runat="server" InsertItemPosition="LastItem" OnItemCanceling="list_Customfields_ItemCanceling" OnItemCommand="list_Customfields_ItemCommand" OnItemDataBound="list_Customfields_ItemDataBound" OnItemEditing="list_Customfields_ItemEditing">
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
        <div class="col-md-10">
            <p style="font-size:15px;">
            <asp:CheckBox ID="chkRecommand" runat="server" Text="Recommend"  />
                </p>
            </div>
                        </div>
                  <div class="form-group row">
        <div class="col-md-10">
             <asp:HyperLink ID="hlinkItems" runat="server" Text='<%# Eval("OptionName") %>' CssClass="price_header"></asp:HyperLink>
           <%--<asp:Label ID="lblLable" runat="server" Text='<%# Eval("OptionName") %>' CssClass="price_header"></asp:Label> --%>
             <%--<asp:Button ID="btnManageItems" runat="server" Text="Manage Items" SkinID="btnDefault" style="float:right;" />--%>
           <%-- <hr class="no-top-margin" />--%>
            </div>
                        <div class="col-md-2" style="text-align:right;">
                           <asp:Label ID="lblIsApplied" runat="server" Text='<%# Eval("d_IsAplied") %>' CssClass="bg-success" style="height:35px;width:90px;text-align:right;font-weight:bold" ></asp:Label>
                           </div>
                      </div>
                  <div class="form-group row">
                       <div class="col-md-12">
                           <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="price_content"></asp:Label> 
                           </div>
                     
                      </div>
                    <div class="form-group row">
                       <div class="col-md-12">
                            <span><%--<asp:Literal ID="lblcount" runat="server" Text='<%# Eval("ItemsCount") %>' ></asp:Literal> Items

                               <asp:LinkButton ID="btnItems" runat="server" Text='<%# Eval("ItemsCountName") %>' CommandArgument='<%# Eval("ID") %>' CommandName="Item"></asp:LinkButton>--%>

                               <asp:HyperLink ID="hlinkItemsCount" runat="server" Text='<%# Eval("ItemsCountName") %>' ></asp:HyperLink>
                           </span>
                           </div>
                        </div>
                    <div class="form-group row">
                       <div class="col-md-12">

                           <asp:Label ID="lblMdata" runat="server" Text='<%# Eval("mdata") %>'></asp:Label>

                           </div>
                       </div>
                 
    </div>

              </ItemTemplate>
               </asp:ListView>


    <div class="form-group row">
        <div class="col-md-12">
           <div class="form-wizard" style="padding-bottom:15px">
       
           
           <asp:Literal ID="lbltext" runat="server"></asp:Literal>
           
        </div>
            </div>
        </div>
     <div class="form-group row">
        <div class="col-md-12">
       <div class="form-group row">
        <div class="col-md-12">
      <asp:Label ID="lblQuotemsg" runat="server" CssClass="galert galert-success" Text="The Customer Agreed This Quotation" Visible="false"></asp:Label>      
            </div>
           </div>
            <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlManageOptions" CancelControlID="lbl_lbtnCloseOptions" >
</ajaxToolkit:ModalPopupExtender>
        <asp:Label ID="lbl_lbtnCloseOptions" runat="server"></asp:Label>
       <asp:Panel ID="pnlManageOptions" runat="server" BackColor="White" Style="display:none;"
                       Width="500px" Height="370px" CssClass="panel panel-color panel-info" ScrollBars="None">
           <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text="Options"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnCloseOptions" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" OnClick="lbtnCloseOptions_Click"/>
								
							</div>
						</div>
    <div class="panel-body">
        <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       <asp:Label ID="lblMsgOptions" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                       <asp:Label ID="lblErrorOptions" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                       </div>
            </div>
 <div class="form-group row">
                   <div class="col-md-12 form-inline">
                         <div class="col-sm-12  form-inline">
                       <asp:DropDownList ID="ddlOptions" runat="server" SkinID="ddl_70" AutoPostBack="true" OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged"></asp:DropDownList>
                       <asp:TextBox ID="txtOptions" runat="server" SkinID="txt_70" Visible="false"></asp:TextBox>
                       <asp:HiddenField ID="hOptionID" runat="server" Value="0" />
                       <asp:LinkButton ID="btnAddOption" runat="server" SkinID="BtnLinkAdd" OnClick="btnAddOption_Click"></asp:LinkButton>
                       <asp:LinkButton ID="btnEditOption" runat="server" SkinID="BtnLinkEdit" OnClick="btnEditOption_Click"></asp:LinkButton>
                       <asp:LinkButton ID="btnDeleteOptions" runat="server" SkinID ="BtnLinkDelete" OnClick="btnDeleteOptions_Click" OnClientClick="return confirm('Do you want to delete the record?');"></asp:LinkButton>
                             </div>
                       </div>
     </div>
          <div class="form-group row" id="pnlOptionDescription" runat="server">
                   <div class="col-md-12">
                         <label class="col-sm-12 control-label">Description</label>
                  <div class="col-sm-12">
                      <asp:TextBox ID="txtOptionDescription" runat="server" SkinID="txtMulti_80" TextMode="MultiLine"></asp:TextBox>
                      </div>
                       </div>
                        </div>
</div>
           <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       <div class="col-sm-12 form-inline">
                       <asp:Button ID="btnSubmitOptions" runat="server" SkinID="btnSubmit" OnClick="btnSubmitOptions_Click" />
                       <asp:Button ID="btnCancelOptions" runat="server" SkinID="btnCancel" OnClick="btnCancelOptions_Click" />
                           </div>
                       </div>
               </div>
                     </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="lbtnCloseOptions" />
               </Triggers>
           </asp:UpdatePanel>
           </asp:Panel>

            </div>
    </div>

    <asp:Panel ID="pnlOptions" runat="server">
        <asp:Panel ID="pnlAdd" runat="server" >
    <div  class="form-group row">
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid Quantity"
                        ValidationExpression="(^-?\d\d*$)" ValidationGroup="Service" ControlToValidate="txtQty" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQty"
                        ErrorMessage="Please enter Quantity" ValidationGroup="Service"  Display="Dynamic"></asp:RequiredFieldValidator>
       
    </div>
    <div class="form-group row">
              <div class="col-md-2">
                   <label class="col-sm-2 control-label"> Item </label>
                  <div class="col-sm-9"> 
                      <asp:DropDownList ID="ddlItems" runat="server"  Visible="false"></asp:DropDownList>
                      <asp:TextBox ID="txtSearch" runat="server" MaxLength="500" style="width:140%"></asp:TextBox>
                      <asp:HiddenField ID="hfCustomerId" runat="server" />
                      <asp:HiddenField ID="hCatelogID" runat="server" />
                     
                      </div>
                  </div>
        <div class="col-md-1" >
            <asp:Button ID="btnPopShow" runat="server" SkinID="btnDefault" Text="Look Up" OnClick="btnPopShow_Click"></asp:Button>
            </div>
         <div class="col-md-2">
              <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Type%></label>
                  <div class="col-sm-8">
                      <asp:DropDownList ID="ddlstype" runat="server"></asp:DropDownList>
                      </div>
             </div>
          <div class="col-md-2">
                <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.UnitPrice%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtCost" runat="server" SkinID="Price_100px" MaxLength="10" Text="0.00"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="txtFilterCost" runat="server" ValidChars="0123456789." TargetControlID="txtCost"></ajaxToolkit:FilteredTextBoxExtender>
                                          </div>
               </div>
              <div class="col-md-2">
                                       <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.Quantity%></label>
                                      <div class="col-sm-6"> <asp:TextBox ID="txtQty" runat="server" SkinID="txt_100px" MaxLength="10" Text="1"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredQTY" runat="server" ValidChars="0123456789" TargetControlID="txtQty"></ajaxToolkit:FilteredTextBoxExtender>
                   
					</div>
				</div>
        
             <div class="col-md-2" style="display:none;visibility:hidden;">
                                       <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.VAT%></label>
                                      <div class="col-sm-6"> <asp:TextBox ID="txtVAT" runat="server" SkinID="txt_100px" MaxLength="10" Text="1"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789." TargetControlID="txtVAT"></ajaxToolkit:FilteredTextBoxExtender>
                   
					</div>
				</div>
        <div class="col-md-3 form-inline">
            <asp:LinkButton ID="btnAdd" runat="server" SkinID="btnAdd" ValidationGroup="Service"
                        OnClick="btnAdd_Click" />
               <asp:LinkButton SkinID="btnCancel" runat="server" ID="back" Text="Cancel" Visible="false" ></asp:LinkButton>
             <asp:Button ID="btnSubmitToCustomer" runat="server" Text="Submit to Customer"
                        OnClick="btnSubmitToCustomer_Click1" />
            </div>
             </div>
    <div class="form-group row">
         <div class="col-md-1" style="display:none;visibility:hidden">
             <asp:LinkButton ID="btnAddItem" runat="server" SkinID="BtnLinkAdd" OnClick="btnAddItem_Click" Visible="false"></asp:LinkButton>
                      <asp:LinkButton ID="btnAdditemCancel" runat="server" SkinID="BtnLinkCancel" OnClick="btnAdditemCancel_Click" Visible="false"></asp:LinkButton>
            </div>
        <div class="col-md-12 text-bold">
        <strong> &nbsp;</strong>
            
            </div>
    </div>
      <div class="form-group row">
           
         
          <div class="col-md-5  form-inline pull-right">
               
              <asp:Button ID="btnPay" runat="server" Text="Process Payment" OnClick="btnPay_Click" style="display:none;visibility:hidden;" />
              
              </div>
         
          </div>
            </asp:Panel>

    <asp:Panel ID="PanelServices" runat="server">
       
        <div class="form-group row" style="float:right;">
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
                    <ItemStyle Width="20%" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Cost Price">
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
                    <ItemStyle Width="10%"  HorizontalAlign="Right" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="QTY">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("QTY") %>' Visible="false"></asp:Label>
                         <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("QTY") %>' SkinID="Price_75px" MaxLength="10"></asp:TextBox>
                        <asp:CompareValidator ID="CV_QTY" runat="server" ControlToValidate="txtQty" Display="None"
                            ErrorMessage="Please enter valid qty" Operator="DataTypeCheck" Type="Integer"
                            ValidationGroup="GridVal"></asp:CompareValidator>
                    </ItemTemplate>
                    <EditItemTemplate>
                       
                    </EditItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="TAX">
                    <ItemTemplate>
                        <asp:Label ID="lblVAT" runat="server" Text='<%# Bind("VAT", "{0:f2}") %>' Visible="false"></asp:Label>
                         <asp:TextBox ID="txtVAT" runat="server" Text='<%# Bind("VAT") %>' SkinID="Price_75px" MaxLength="10"></asp:TextBox>
                        <asp:CompareValidator ID="CV_vat" runat="server" ControlToValidate="txtVAT" Display="None"
                            ErrorMessage="Please enter valid TAX" Operator="DataTypeCheck" Type="Double"
                            ValidationGroup="GridVal"></asp:CompareValidator>
                    </ItemTemplate>
                    <EditItemTemplate>
                       
                    </EditItemTemplate>
                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Total">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Total","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <%--<asp:Label ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total", "{0:f2}")%>' ></asp:Label>--%>
                        <%# DataBinder.Eval(Container.DataItem, "Total", "{0:f2}")%>
                    </EditItemTemplate>
                    <ItemStyle Width="15%" HorizontalAlign="Right" />
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
            SelectCommand="QuotationItems_SelectByCallID" SelectCommandType="StoredProcedure">
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
                     <div class="col-sm-8"> <asp:TextBox ID="txtNotes" SkinID="txtMulti" runat="server" TextMode="MultiLine"></asp:TextBox>
					</div>
				</div>
            </div>
      <div class="form-group row">
                   <div class="col-md-8">
                       <br />
                        <label class="col-sm-2 control-label"> </label>
                       <div class="col-sm-8"> <asp:Button ID="btnUpdateTotals" runat="server" SkinID="btnUpdate" OnClick="btnUpdateTotals_Click" />
                   </div>
                       </div>
          </div>
        </asp:Panel>

        <asp:Panel ID="pnlOffer" runat="server">
             <div class="form-group row" style="display:none;visibility:hidden;">
        <div class="col-md-12 text-bold">
        <strong>  Special Offer </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
            <div class="form-group row">
        <div class="col-md-8">

            <asp:GridView ID="GridOffers" runat="server" OnRowCommand="GridOffers_RowCommand"
            Width="100%" OnRowUpdated="GridOffers_RowUpdated" OnRowUpdating="GridOffers_RowUpdating"
            OnRowCancelingEdit="GridOffers_RowCancelingEdit" OnRowEditing="GridOffers_RowEditing">
                <Columns>
                      <asp:TemplateField ItemStyle-CssClass="col-nowrap form-inline" FooterStyle-CssClass="form-inline"  ControlStyle-CssClass="form-inline" ItemStyle-Width="125px">
                   
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
                      <asp:TemplateField HeaderText="Maintenance Plan ">
                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("Title") %>' Width="175px"></asp:TextBox>
                    </EditItemTemplate>
                     <ItemStyle Width="20%" />
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblPrice1" runat="server" Text='<%# Bind("Price","{0:F2}") %>'></asp:Label>
                    </EditItemTemplate>
                     <ItemStyle Width="20%" HorizontalAlign="Right" />
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount Price">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscountPrice" runat="server" Text='<%# Bind("DiscountPrice","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDiscountPrice" runat="server" Text='<%# Bind("DiscountPrice") %>' Width="175px" SkinID="Price_150px"></asp:TextBox>
                    </EditItemTemplate>
                     <ItemStyle Width="20%" HorizontalAlign="Right" />
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:Button ID="btnSendMail" runat="server" Text="Please contact me" CommandName="contact" CommandArgument='<%# Bind("ID")%>' />
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

    </asp:Panel>

        <asp:Panel ID="pnlAccept" runat="server" Visible="false">
    <div class="form-group row">
              <div class="col-md-12">
                  <asp:Button ID="btnAcceptOption" runat="server" Text="Accept Quote" OnClick="btnAcceptOption_Click" />
                  </div>
        </div>
</asp:Panel>
    </asp:Panel>


     <asp:Panel ID="pnlTemplateTab" runat="server">
     <asp:Panel ID="pnlTemplateSection" runat="server">

     <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>  Apply Template </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
     <div class="form-group row">
              <div class="col-md-4">
                   <asp:DropDownList ID="ddlTitle" runat="server"></asp:DropDownList>
                  </div>
          <div class="col-md-4 form-inline">
                  <asp:Button ID="btnApply" runat="server" SkinID="btnApply" OnClick="btnApply_Click" />
              <asp:Button ID="btnEditTemplate" runat="server" SkinID="btnEdit" OnClick="btnEditTemplate_Click"  />
                  </div>
         </div>

    
<asp:Label ID="lblTemp" runat="server"></asp:Label>
  <ajaxToolkit:ModalPopupExtender ID="mdl_Template" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblTemp" PopupControlID="pnlTemplate" CancelControlID="tclose" >
</ajaxToolkit:ModalPopupExtender>

<asp:Panel ID="pnlTemplate" runat="server" BackColor="White" Style="display:none;"
                       Width="850px" Height="480px" CssClass="panel panel-color panel-info" ScrollBars="None">
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblTemplate" runat="server" Text="Template"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="tclose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
								
							</div>
						</div>
    <div class="panel-body">
        <div class="form-group row">
      <div class="col-md-12">
           <label class="col-sm-2 control-label">Template Name</label>
           <div class="col-sm-6">
               <asp:TextBox ID="txtTemplateName" runat="server" MaxLength="255"></asp:TextBox>
              
            </div>
           <div class="col-sm-4">
                <asp:Button ID="btnSaveTemplate" runat="server" OnClick="btnSaveTemplate_Click" SkinID="btnSave" />
               </div>
	  </div>
            </div>
         <CKEditor:CKEditorControl ID="txtTemplate" BasePath="~/Scripts/ckeditor/" runat="server"
                          Width="780px" Height="220px" ClientIDMode="Static"  ></CKEditor:CKEditorControl>
         <div class="form-group row" style="padding-top:5px">
      <div class="col-md-4">
         
          </div>
             </div>
        </div>
    </asp:Panel>


    </asp:Panel>
     <asp:Panel id="pnlTemplateDisplay" runat="server">
     <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>  <asp:Literal ID="lblTemplateName" runat="server"></asp:Literal> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
<div class="form-group row" >
                   <div class="col-md-12">
                       <asp:Panel ID="pnlViewTemplate" runat="server" Height="350px" Width="100%" ScrollBars="Auto" BorderStyle="Solid" BorderWidth="1" BorderColor="Silver">
                           <asp:Literal ID="lblTemplateData" runat="server"></asp:Literal>
                       </asp:Panel>
                       </div>

    </div>
         </asp:Panel>

         </asp:Panel>

     <asp:Panel ID="pnlFinance" runat="server">
         <div class="form-group row">
        <div class="col-md-12">
            &nbsp;
            </div>
             </div>
         <div class="form-group row">
        <div class="col-md-12">
       <asp:Button ID="btnApplyforConsumerCredit" runat="server" SkinID="btnDefault" Text="Apply for Consumer Credit" />
            </div>
    </div>
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



<%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>
<script type="text/javascript">
    //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    //GridResponsiveCss();
</script> 
