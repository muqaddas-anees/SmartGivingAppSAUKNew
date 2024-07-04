<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="DCInvoiceList.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCInvoiceList" EnableEventValidation="false" %>
 <%@ Register Src="~/WF/DC/controls/CustomerOrder.ascx" TagName="CustomerOrder"
    TagPrefix="uc3" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
   <%:sessionKeys.JobsDisplayName %>
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

    .grid-col-right{
        text-align:right;
    }
</style>
    <%-- <button id="btnVideo" runat="server" class="btn btn-white btn-icon btn-icon-standalone btn-sm">
									<i class="fa-video-camera" style="color:white;"></i>
									<span>Watch Video</span>
								</button>--%>
    <%--  <ajaxToolkit:ModalPopupExtender ID="mdlVideo" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnVideo" PopupControlID="pnlVideo" CancelControlID="lblbtnClose" >
</ajaxToolkit:ModalPopupExtender>
    
       <asp:Panel ID="pnlVideo" runat="server" BackColor="White" Style="display:none;"
                       Width="680px" Height="480px" CssClass="panel panel-color panel-info" ScrollBars="None">
             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="Label7" runat="server" Text="Invoice"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lblbtnClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="panel-body">
        <div class="form-group row">
                   <div class="col-md-12 form-inline">

                       <iframe id="viframe" runat="server" height="340" width="600" style="border:none;" src="https://player.vimeo.com/video/516722643"></iframe>
                       
                       </div>
            </div>
 
      
        
           
        </div>
                  
           </asp:Panel>--%>
     <asp:Button ID="btnRaiseInvoice" runat="server" Text="Create Invoice" OnClick="btnRaiseInvoice_Click" ToolTip="Create Invoice" />
     <a id ="link_return" visible="false" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self" ><i class="fa fa-arrow-left"></i> Return to <%= Resources.DeffinityRes.ServiceDesk%></a>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="pnlOrder" runat="server" Width="100%" Visible="false">
    <uc3:CustomerOrder ID="CustomerOrder1" runat="server" Visible="false" />
    
    </asp:Panel>
    <%-- <div class="form-group row">
          <div class="col-md-12  form-inline pull-right">
   
</div>
         </div>--%>
    <div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lblmsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
              </div>
        </div>
    <div class="form-group row">
          <div class="col-md-12">
              <asp:HiddenField ID="hpriceid" runat="server" Value="0" />
              <asp:GridView ID="Grid_services" runat="server" AutoGenerateColumns="False"
            Width="100%" OnRowCommand="Grid_services_RowCommand"  >
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="col-nowrap form-inline" FooterStyle-CssClass="form-inline"  ControlStyle-CssClass="form-inline" ItemStyle-Width="125px">
                   
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="inv"
                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                        </asp:LinkButton>
                    </ItemTemplate>
                   
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoice Ref">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoiceRef" runat="server" Text='<%# Bind("InvoiceRef") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Invoice Details">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoiceDesc" runat="server" Text='<%# Bind("InvoiceDescription") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="15%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sub Total"  HeaderStyle-CssClass="grid-col-right" ItemStyle-CssClass="grid-col-right">
                    <ItemTemplate>
                        <asp:Label ID="lblSubTotal" runat="server" Text='<%# Bind("SubTotal","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                   
                    <ItemStyle Width="10%" HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT" HeaderStyle-CssClass="grid-col-right" ItemStyle-CssClass="grid-col-right">
                    <ItemTemplate>
                        <asp:Label ID="lblVAT" runat="server" Text='<%# Bind("VAT","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                   
                    <ItemStyle Width="10%" HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total"  HeaderStyle-CssClass="grid-col-right" ItemStyle-CssClass="grid-col-right">
                    <ItemTemplate>
                        <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("RevicedPrice","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                   
                    <ItemStyle Width="10%" HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" >
                    <ItemTemplate>
                        <asp:Label ID="lblgridnotes" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                     <ItemStyle Width="15%" />
                </asp:TemplateField>
                 <asp:TemplateField >
                    <ItemTemplate>
                        <asp:Button ID="btnPay" runat="server" CommandArgument='<%# Bind("ID") %>' SkinID="btnDefault" Text="Pay Now" CommandName="pay" Visible='<%# SetButtonVisible((string)Eval("Status") )%>'></asp:Button>
                    </ItemTemplate>
                     <ItemStyle Width="8%" />
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:Button ID="btnSendToCustomer" runat="server" CommandArgument='<%# Bind("ID") %>' SkinID="btnDefault" Text="Send To Client" CommandName="send"></asp:Button>
                    </ItemTemplate>
                     <ItemStyle Width="8%" />
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
              </div>
        </div>

    
     <ajaxToolkit:ModalPopupExtender ID="mdlContacts" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblshowcontacts" PopupControlID="pnlContacts" CancelControlID="btnCloseContacts" >
</ajaxToolkit:ModalPopupExtender>
   
     <asp:Label ID="lblshowcontacts" runat="server"></asp:Label>
        <asp:Label ID="Label4" runat="server"></asp:Label>
       <asp:Panel ID="pnlContacts" runat="server" BackColor="White" Style="display:none;"
                       Width="950px" Height="630px" CssClass="card shadow-sm" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="Label5" runat="server" Text="Select contacts to send mail"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="btnCloseContacts" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
       
 
        <div class="form-group row" style="height:480px;overflow-y:auto;overflow-x:hidden;">
           <div class="form-group row mb-6">
              
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
       
           <div class="form-group row">
                   <div class="col-lg-12 form-inline">
                       
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

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">





</asp:Content>
