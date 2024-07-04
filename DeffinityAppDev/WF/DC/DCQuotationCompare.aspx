<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="DCQuotationCompare.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCQuotationCompare" %>
<%@ Register src="~/WF/DC/controls/DCQuotationCtrl.ascx" tagname="sd_services" tagprefix="uc1" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
 <%@ Register Src="~/WF/DC/controls/CustomerOrder.ascx" TagName="CustomerOrder"
    TagPrefix="uc3" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
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
     <label id="lblTitle" runat="server">
                  </label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
    <style>
    .btn.btn-white.btn-icon-standalone i {
    background-color: #FF6600;
    border-right-color: #e6e6e6;
}
</style>
   <button id="btnVideo" runat="server" class="btn btn-white btn-icon btn-icon-standalone btn-sm" style="display:none;">
									<i class="fa-video-camera" style="color:white;"></i>
									<span>Watch Video</span>
								</button>
    
     <a id ="link_return" visible="false" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to  <%= Resources.DeffinityRes.ServiceDesk%></a>

    <ajaxToolkit:ModalPopupExtender ID="mdlVideo" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnVideo" PopupControlID="pnlVideo" CancelControlID="lblbtnClose" >
</ajaxToolkit:ModalPopupExtender>
    
       <asp:Panel ID="pnlVideo" runat="server" BackColor="White" Style="display:none;"
                       Width="680px" Height="480px" CssClass="panel panel-color panel-info" ScrollBars="None">
         

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="Label7" runat="server" Text="Quotation"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lblbtnClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="panel-body">
        <div class="form-group row">
                   <div class="col-md-12 form-inline">

                       <iframe id="viframe" runat="server" height="340" width="600" style="border:none;" src="https://player.vimeo.com/video/516749822"></iframe>
                       
                       </div>
            </div>
 
      
        
           
        </div>
                  
           </asp:Panel>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <script language="javascript" type="text/javascript">
         function SelectSingleCheckbox(Chkid) {
             var chkbid = document.getElementById(Chkid);
             var chkList = document.getElementsByTagName("input");
             for (i = 0; i < chkList.length; i++) {
                 if (chkList[i].type == "checkbox" && chkList[i].id != chkbid.id) {
                     chkList[i].checked = false;
                 }
             }
         }
</script>
    <style>
        .price_header{
            font-size:18px;font-weight:bold;color: #717272;
        }
        .price_text{
            font-size:14px;font-weight:bold;color: #717272;text-align:right;
        }
        .price_lable{
            font-size:14px;font-weight:bold;color: #717272;text-align:right;
        }
        .price_item{
            font-size:14px;color: #717272;
        }
    </style>
     <div class="form-group row">
        <div class="col-md-12">
            <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
           
            </div>
         </div>
      <asp:Panel ID="pnlOrder" runat="server" Width="100%" Visible="false">
    <uc3:CustomerOrder ID="CustomerOrder1" runat="server" Visible="false" />
    
    </asp:Panel>
    
      <%--  <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>  Compare Options </strong>
            <hr class="no-top-margin" />
            </div>
    </div>--%>
    <div class="form-wizard">
 <ul id="countrytabs" class="tabs" style="display:none;visibility:hidden;">
 <li class="ms-hover">
       
            <asp:HyperLink ID="lbtnQuote"  Target="_self" runat="server" Text="Customer Quotation">Customer Quotation</asp:HyperLink></li>
         <li class="ms-hover">
              <asp:HyperLink ID="lbtnAttach"  Target="_self" runat="server" Text="Attach Scope of Works">Attach Scope of Works</asp:HyperLink>
             </li>
         <li class="ms-hover" style="display:none;visibility:hidden;">
              <asp:HyperLink ID="lbtnFinance"  Target="_self" runat="server" Text="Finance ">Finance</asp:HyperLink>
             </li>
        </ul>
    </div>
      <div class="form-group ">
        <div class="col-md-12">
            &nbsp;<asp:Label ID="lblservice" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
            
            </div>
                            </div>

      <div class="form-group row mb-6" id="pnlAddOptonsButton" runat="server">
           <div class="col-md-6">
                <asp:Label ID="lblErrorMsg" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
               </div>
        <div class="col-md-6" style="text-align:right;">
       <asp:Button ID="btnAddOptions" runat="server" Text="Additional Quotation Choices" SkinID="btnDefault" style="text-align:right;display:none;visibility:hidden;" />
            <asp:Button ID="btnRaiseInvoice" runat="server" SkinID="btnDefault" Text="Create Invoice" OnClick="btnRaiseInvoice_Click" style="text-align:right;"></asp:Button>
            <asp:Button ID="btnViewCompare" runat="server" Text="Compare Options" SkinID="btnDefault" style="text-align:right;" OnClick="btnViewCompare_Click" Visible="false" />
            <asp:Button ID="btnSend" runat="server" Text="Send to Client" SkinID="btnDefault" OnClick="btnSubmitToCustomer_Click1" />
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
             <asp:Label ID="lblAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnCloseOptions" runat="server"></asp:Label>
       <asp:Panel ID="pnlManageOptions" runat="server" BackColor="White" Style="display:none;"
                       Width="550px" Height="600px" CssClass="card shadow-sm" ScrollBars="None">
           <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text="Quotation Details"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnCloseOptions" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
        <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       <asp:Label ID="lblMsgOptions" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                       <asp:Label ID="lblErrorOptions" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                       </div>
            </div>
        <asp:Panel ID="pnlOptionalDetails"  runat="server">
 <div class="form-group row mb-6">
                   <div class="col-md-12 form-inline">
                         <label class="col-sm-12 col-form-label fw-bold fs-6">Short Quote Title</label>
                         <div class="col-sm-12  form-inline">
                       <asp:DropDownList ID="ddlOptions" runat="server" SkinID="ddl_70" AutoPostBack="true" OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                       <asp:TextBox ID="txtOptions" runat="server" SkinID="txt_70" ></asp:TextBox>
                       <asp:HiddenField ID="hOptionID" runat="server" Value="0" />
                       <asp:LinkButton ID="btnAddOption" runat="server" SkinID="BtnLinkAdd" OnClick="btnAddOption_Click" Visible="false"></asp:LinkButton>
                       <asp:LinkButton ID="btnEditOption" runat="server" SkinID="BtnLinkEdit" OnClick="btnEditOption_Click" Visible="false"></asp:LinkButton>
                       <asp:LinkButton ID="btnDeleteOptions" runat="server" SkinID ="BtnLinkDelete" OnClick="btnDeleteOptions_Click" OnClientClick="return confirm('Do you want to delete the record?');" Visible="false"></asp:LinkButton>
                             </div>
                       </div>
     </div>
          <div class="form-group row mb-6" id="pnlOptionDescription" runat="server">
                   <div class="col-md-12">
                         <label class="col-sm-12 col-form-label fw-bold fs-6">Description</label>
                  <div class="col-sm-12">
                      <asp:TextBox ID="txtOptionDescription" runat="server" SkinID="txtMulti_80" TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
                      </div>
                       </div>
                        </div>

              <div class="form-group row mb-6">
                   <div class="col-md-12 form-inline">
                         <asp:Button ID="btnNext" runat="server" SkinID="btnNext" OnClick="btnNext_Click" style="float:right;" />
                       </div>
                        </div>

            </asp:Panel>

          <asp:Panel ID="pnlAddItem"  runat="server" Visible="false">

                            <ContentTemplate>
                             
 <div  class="form-group row">
     <asp:HiddenField ID="hItemID" runat="server" Value ="0" />
         <asp:CompareValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid Quantity"
                        Operator="DataTypeCheck" Type="Double" ValidationGroup="Service" ControlToValidate="txtQty" Display="Dynamic"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQty"
                        ErrorMessage="Please enter Quantity" ValidationGroup="Service"  Display="Dynamic"></asp:RequiredFieldValidator>
       
    </div>
    <div class="form-group row  mb-6">
            
                   <label class="col-sm-3 control-label"> Item </label>
                  <div class="col-sm-8"> 
                      <asp:DropDownList ID="ddlItems" runat="server"  Visible="false"></asp:DropDownList>
                      <asp:TextBox ID="txtSearch" runat="server" MaxLength="500" ></asp:TextBox>
                      <asp:HiddenField ID="hfCustomerId" runat="server" />
                      <asp:HiddenField ID="hCatelogID" runat="server" />
                      <asp:HiddenField ID="hImageID" runat="server" Value="00000000-0000-0000-0000-000000000000" />
                     
                     
                  </div>
        </div>
    <div class="form-group row  mb-6">
       
              <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Type%></label>
                  <div class="col-sm-8">
                      <asp:DropDownList ID="ddlstype" runat="server"></asp:DropDownList>
                      </div>
            
        </div>
    <div class="form-group row  mb-6">
         
                <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.UnitPrice%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtCost" runat="server" SkinID="Price_100px" MaxLength="10" Text="0.00"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="txtFilterCost" runat="server" ValidChars="0123456789." TargetControlID="txtCost"></ajaxToolkit:FilteredTextBoxExtender>
                                          </div>
              
        </div>
    <div class="form-group row  mb-6">
            
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Quantity%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtQty" runat="server" SkinID="txt_100px" MaxLength="10" Text="1"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredQTY" runat="server" ValidChars="0123456789." TargetControlID="txtQty"></ajaxToolkit:FilteredTextBoxExtender>
                   
					
				</div>
        </div>
                            <div class="form-group row" id="pnlAddItemVat" runat="server" visible="false">
         
                <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.VAT%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtAddItemVat" runat="server" SkinID="Price_100px" MaxLength="10" Text="0.00"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="0123456789." TargetControlID="txtVat"></ajaxToolkit:FilteredTextBoxExtender>
                                          </div>
               
        </div>
    
                             <div class="form-group row mb-6">
             
                   <label class="col-sm-3 control-label"> Notes </label>
                  <div class="col-sm-8"> 
                      
                      <asp:TextBox ID="txtItemNotes" runat="server" MaxLength="500" SkinID="txtMulti" Height="60px" TextMode="MultiLine" ></asp:TextBox>
                     
                     
                     
                  </div>
        </div>
                             <div class="form-group row mb-6">
                        
                                       <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.UploadImage%></label>
                                      <div class="col-sm-8"><asp:FileUpload ID="FileUploadMaterial" runat="server"></asp:FileUpload>
                                
					
				</div>

                        </div>



                 <div class="form-group row">
                   <div class="col-sm-8 m-5">
                       <asp:Button ID="btnSubmitOptions" runat="server" SkinID="btnSubmit" OnClick="btnSubmitOptions_Click" CausesValidation="false"  />
                       <asp:Button ID="btnCancelOptions" runat="server" SkinID="btnCancel" OnClick="btnCancelOptions_Click" CausesValidation="false" />
                       </div>
                          
               </div>
              </asp:Panel>
</div>

                  

                   
        
                     </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="lbtnCloseOptions" />
                   <asp:PostBackTrigger ControlID="btnSubmitOptions" />
               </Triggers>
           </asp:UpdatePanel>
           </asp:Panel>



             <ajaxToolkit:ModalPopupExtender ID="mdlRaiseInvoice" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblRiaseInvoice" PopupControlID="pnlRaiseInvoice" CancelControlID="lbl_lbtnCloseRaiseinvoice" >
</ajaxToolkit:ModalPopupExtender>
              <asp:Label ID="lblRiaseInvoice" runat="server"></asp:Label>
            <asp:Label ID="lbl_lbtnCloseRaiseinvoice" runat="server"></asp:Label>
            <asp:Panel ID="pnlRaiseInvoice" runat="server" BackColor="White" Style="display:none;"
                       Width="500px" Height="300px" CssClass="panel panel-color panel-info" ScrollBars="None">
           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
               <ContentTemplate>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="Label1" runat="server" Text="Invoice"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="LinkButton1" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" OnClick="lbtnCloseOptions_Click"/>
								
							</div>
						</div>
    <div class="panel-body">
        <asp:ValidationSummary ID="vtSum" runat="server" ValidationGroup="radd" />
 
          <div class="form-group row">
                   <div class="col-md-12">
                         <label class="col-sm-12 control-label">Invoice Description</label>
                  <div class="col-sm-12">
                      <asp:TextBox ID="txtInoicedescription" runat="server" SkinID="txtMulti_80" TextMode="MultiLine" ValidationGroup="radd"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="rvDetails" runat="server" ControlToValidate="txtInoicedescription" ErrorMessage="Please enter invoice description" ValidationGroup="radd" Display="None"></asp:RequiredFieldValidator>
                      </div>
                       </div>
                        </div>
</div>
           <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       <div class="col-sm-12 form-inline">
                       <asp:Button ID="btnSubmitRaiseinvoice" runat="server" SkinID="btnSubmit" OnClick="btnSubmitRaiseinvoice_Click" ValidationGroup="radd" />
                       <asp:Button ID="btnCancelRaiseinvoice" runat="server" SkinID="btnCancel" OnClick="btnCancelRaiseinvoice_Click" />
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
      <div class="row">

    <asp:ListView ID="list_Customfields" runat="server" InsertItemPosition="None" OnItemCanceling="list_Customfields_ItemCanceling"
        OnItemCommand="list_Customfields_ItemCommand" OnItemDataBound="list_Customfields_ItemDataBound" OnItemEditing="list_Customfields_ItemEditing">
           <LayoutTemplate>
              <div class="form-group row">
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>
          <ItemTemplate>
              <div class="col-md-4">
              <div class="card card-stretch card-bordered mb-5" style="min-height:400px;">
                  <div class="card-body">

                      <div class="row">
                          <asp:Image ID="img1" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' />

                      </div>

                   <div class="form-group row" style="text-align:right;margin-bottom:-5px;">
       
            <p style="font-size:15px;text-align:right;visibility:hidden;" >
            <asp:CheckBox ID="chkRecommand" runat="server" Text="Recommend"  />
                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>'></asp:Label>
                </p>
           
                        </div>
                      
                  
            <div class="form-group row mb-15" style="height:55px;font-weight:bold;text-align:center;font-size:20px">
       
           <asp:Label ID="lblLable" runat="server" Text='<%# Eval("OptionName") %>' style="font-size:30px" ></asp:Label> 
            
          
                      </div>
                  <div class="form-group row" style="display:none;visibility:hidden;">
                       <div class="col-md-6">
                           <span>
                               <asp:HyperLink ID="hlinkItems" runat="server" Text='<%# Eval("ItemsCountName") %>' Visible="false" ></asp:HyperLink>
                           </span>
                           </div>
                       <div class="col-md-6" style="text-align:right;">
                           <asp:Label Visible="false" ID="lblIsApplied" runat="server" Text='<%# Eval("IsAplied") %>' CssClass="bg-success" style="height:35px;width:90px;text-align:right;font-weight:bold" ></asp:Label>
                           </div>
                      </div>
                    <div class="form-group row">
                       <div class="col-md-12">

                           <asp:Label ID="lblMdata" runat="server" Text='<%# Eval("mdata") %>'></asp:Label>

                           </div>
                       </div>
                         <div class="form-group row">
        <div class="col-md-12" style="padding-left:0px;padding-right:0px;padding-top:5px">
                   <asp:Button ID="btnItemView" runat="server" Text="Edit Quotation" SkinID="btnDefault" CommandName="Item" CommandArgument='<%# Eval("ID") %>' CausesValidation="false" style="float:left;width:100%" ></asp:Button>
                 
            </div>
                         </div>
                    <div class="form-group row" style="margin-bottom:0px;display:none;visibility:hidden;">
        <div class="col-md-12 text-center alert alert-info" style="padding:5px;margin-bottom:10px">
        <strong> <asp:HyperLink ID="hLinkFinnace" runat="server"  Text="Quotation Summary" ForeColor="White"></asp:HyperLink> </strong>
            
            </div>
    </div>
                  <%-- <div class="form-group row" runat="server" visible='<%# planvisibile() %>'>
        <div class="col-md-12 text-center alert alert-success" style="padding:5px;">
        <strong> <asp:HyperLink ID="hLinkPlan" runat="server"  Text="Maintenance Plan Admin" ForeColor="White"></asp:HyperLink> </strong>
            
            </div>
    </div>--%>
                 
                 <%-- <div class="form-group row" style="height:150px;overflow-y:auto;overflow-x:hidden;">
                       <%--<div class="col-md-12">
                           <ul>
                           <asp:Label ID="lblItemsList" runat="server" Text='<%# Eval("ItemsList") %>'></asp:Label>
                               </ul>
                           <%--</div>
                       </div>--%>
                   <div class="form-group row">
                       <div class="col-md-12 form-inline" style="text-align:center; visibility:hidden;display:none;">
                           
                           <asp:LinkButton ID="btnDeleteQuote" runat="server" SkinID="BtnLinkDelete" CommandName="Del" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" style="margin-bottom:0px"></asp:LinkButton>
                       </div>
                       </div>
                
    </div>
                  </div>
              </div>
              </ItemTemplate>
               </asp:ListView>


          </div>
    
     <ajaxToolkit:ModalPopupExtender ID="mdlShowMail" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblSendMail" PopupControlID="pnlManagePassword" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="lblSendMail" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlManagePassword" runat="server" BackColor="White" Style="display:none;"
                       Width="950px" Height="630px" CssClass="card shadow-sm" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="Label3" runat="server" Text="Send to Client"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
       
 
        <div class="form-group row" style="height:480px;overflow-y:auto;overflow-x:hidden;">
           <div class="form-group row">
               <div class="col-md-12 form-inline">
									 <CKEditor:CKEditorControl ID="CKEditor1" BasePath="~/Scripts/ckeditor/" runat="server"
                         Height="370px" ClientIDMode="Static" BasicEntities="true" FullPage="true" ></CKEditor:CKEditorControl>
                   </div>
								</div>
    </div>
       
           <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       
                      <asp:HiddenField ID="HiddenField1" runat="server" />
                       
                       
                          
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


     <ajaxToolkit:ModalPopupExtender ID="mdlContacts" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblshowcontacts" PopupControlID="pnlContacts" CancelControlID="btnCloseContacts" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="lblshowcontacts" runat="server"></asp:Label>
        <asp:Label ID="Label4" runat="server"></asp:Label>
       <asp:Panel ID="pnlContacts" runat="server" BackColor="White" Style="display:none;"
                       Width="950px" Height="630px" CssClass="panel panel-color panel-info" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="Label5" runat="server" Text="Select contacts to send mail"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="btnCloseContacts" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="panel-body">
       
 
        <div class="form-group row" style="height:480px;overflow-y:auto;overflow-x:hidden;">
           <div class="form-group row">
               <div class="col-md-12 form-inline">
									<asp:GridView ID="gridContacts" runat="server" Width="100%">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkContact" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30%" HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContact" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField ItemStyle-Width="50%" HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContactEmail" runat="server" Text='<%# Eval("EmailAddress") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
									</asp:GridView>
                   </div>
								</div>
    </div>
       
           <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       
                      <asp:HiddenField ID="HiddenField2" runat="server" />
                       
                       
                          
                                        <asp:Button ID="Button2" runat="server" SkinID="btnDefault" Text="Send" OnClick="btnSendMailContacts_Click" />
                       <asp:Button ID="Button3" runat="server" SkinID="btnDefault" Text="Save"  Visible="false" />
                       </div>
               </div>
        </div>
                   <%--  </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="lbtnClosePop" />
               </Triggers>
           </asp:UpdatePanel>--%>
           </asp:Panel>

   
     


       <%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>
    
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
     <script type="text/javascript">
         //activeTab('Quotation');
    </script>
      <script>
        //  activeTab('Quote &amp; Assign Sales');
    </script>
</asp:Content>
