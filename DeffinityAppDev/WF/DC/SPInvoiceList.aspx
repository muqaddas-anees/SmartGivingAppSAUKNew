<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="SPInvoiceList.aspx.cs" Inherits="DeffinityAppDev.WF.DC.SPInvoiceList" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Supplier Invoices
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Invoices
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group row">
        <asp:Label ID="lblmsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblerror" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
    
        </div>
    <div class="form-group row">
      <div class="col-md-4">
           <label class="col-sm-5 control-label">Service Provider</label>
           <div class="col-sm-7">
               <asp:DropDownList ID="ddlUsers" runat="server"></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Search%></label>
           <div class="col-sm-10">
               <asp:TextBox ID="txtSearch" runat="server" MaxLength="200"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-4">
           <asp:LinkButton ID="btnSearch" runat="server" SkinID="btnSearch" OnClick="btnSearch_Click"></asp:LinkButton>
	</div>
</div>
       <div class="row pull-right">
        <div class="col-md-12 pull-right">
            <asp:Button ID="btnCreditNote" runat="server" Text="Credit Note" OnClick="btnCreditNote_Click" />
            </div>
                     </div>
               
                    <ajaxToolkit:ModalPopupExtender ID="mdlpopUpCreditNote" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnpopclose"
                                      TargetControlID="LblbtnCreditNote" PopupControlID="PnlCreditNote"></ajaxToolkit:ModalPopupExtender>
                 <asp:Panel ID="PnlCreditNote" runat="server" BackColor="White"
                        Style="display:none" Width="700px" Height="300px" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="None">

                     <div class="form-group row">
        <div class="col-md-11">
           <strong> <asp:Label ID="lblHName" Text="Credit Note" runat="server"></asp:Label></strong> 
            <hr class="no-top-margin" />
            </div>
                         <div class="col-md-1">
                             <asp:LinkButton ID="imgBtnCancel" runat="server" CausesValidation="false" SkinID="BtnLinkCancel"
                                                  ToolTip="<%$ Resources:DeffinityRes,Close%>" OnClick="imgBtnCancel_Click" />
                           <asp:Button ID="btnpopclose" runat="server" CausesValidation="false" Text="Close" Style="display:none;" />
                             </div>
    </div>

                   
                      
                      <asp:Label ID="LblbtnCreditNote" runat="server"></asp:Label>
                       <asp:UpdatePanel ID="PnlUpdateCreditNote" runat="server" >
                           <ContentTemplate>
                               <div class="form-group row">
          <div class="col-md-12">
              
               <asp:Label ID="lblCreditMsg" EnableViewState="false" runat="server" SkinID="GreenBackcolor"></asp:Label>
              <asp:Label ID="lblCreditMsgError" EnableViewState="false" runat="server" SkinID="RedBackcolor"></asp:Label>
                                           <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="CreditNote" />
	</div>
</div>
                               <div class="form-group row">
      <div class="col-md-5">
           <label class="col-sm-5 control-label">Service Provider</label>
           <div class="col-sm-7">
               <asp:DropDownList ID="ddlVendorsIncredit" runat="server" SkinID="ddl_90"></asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="req2Credit" runat="server" ControlToValidate="ddlVendorsIncredit" Display="None"
                                                ErrorMessage="Please select Service Provider" InitialValue="0" ValidationGroup="CreditNote"></asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-5">
           <label class="col-sm-5 control-label">Credit Note</label>
           <div class="col-sm-7">
               <asp:TextBox ID="txtCreditValue" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtCreditFilter" runat="server"
                                                                             TargetControlID="txtCreditValue" ValidChars="0123456789."></ajaxToolkit:FilteredTextBoxExtender>
                                           <asp:RequiredFieldValidator ID="req1Credit" runat="server" ControlToValidate="txtCreditValue" Display="None"
                                                                        ErrorMessage="Please enter credit." ValidationGroup="CreditNote"></asp:RequiredFieldValidator>
            </div>
	</div>
                                   <div class="col-md-2">
                                       <asp:Button ID="btnCreditApply" runat="server" Text="Apply" ValidationGroup="CreditNote" OnClick="btnCreditApply_Click"  />
                                       </div>
</div>
                               
                           </ContentTemplate>
                       </asp:UpdatePanel>
                 </asp:Panel>
    <asp:HiddenField ID="hcallid" runat="server" />
    <asp:GridView ID="GridInvoice" runat="server" OnRowCommand="GridInvoice_RowCommand" OnRowDataBound="GridInvoice_RowDataBound" OnRowCancelingEdit="GridInvoice_RowCancelingEdit" OnRowEditing="GridInvoice_RowEditing">
        <Columns>
             <asp:TemplateField HeaderText=""  ItemStyle-Width="3%">
                <ItemTemplate>
                    <asp:CheckBox ID="chk" runat="server" CssClass="check"></asp:CheckBox>
                    
                </ItemTemplate>
                 <EditItemTemplate>

                 </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField ItemStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline"  ControlStyle-CssClass="form-inline" ItemStyle-Width="125px">
                   
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                            CommandArgument='<%# Bind("InvoiceNo")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update1" Text="Update"
                            CommandArgument='<%# Bind("InvoiceNo")%>' SkinID="BtnLinkUpdate"
                            ToolTip="Update"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Raised"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="11%">
                <ItemTemplate>
                    <asp:Label ID="lblDateRised" runat="server" Text='<%#Bind("DateRaised") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                     <asp:Label ID="lblDateRised" runat="server" Text='<%#Bind("DateRaised") %>'></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Invoice No"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="10%">
                <ItemTemplate>
                     <asp:Label ID="lblCallid" runat="server" Text='<%#Bind("CallID") %>' Visible="false" ></asp:Label>
                    <%--<asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Bind("InvoiceNo") %>'></asp:Label>--%>
                    <asp:LinkButton ID="lblInvoiceNo" runat="server" Text='<%#Bind("InvoiceNo") %>' CommandName="invoice" CommandArgument='<%#Bind("CallID") %>'></asp:LinkButton>
                </ItemTemplate>
                 <EditItemTemplate>
                      <asp:Label ID="lblCallid" runat="server" Text='<%#Bind("CallID") %>' Visible="false" ></asp:Label>
                    <%--<asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Bind("InvoiceNo") %>'></asp:Label>--%>
                    <asp:LinkButton ID="lblInvoiceNo" runat="server" Text='<%#Bind("InvoiceNo") %>' CommandName="invoice" CommandArgument='<%#Bind("CallID") %>'></asp:LinkButton>
                 </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText ="Ticket Ref"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="8%">
                <ItemTemplate>
                    <asp:LinkButton ID="lblTicketRef" runat="server" Text='<%#Bind("TicketRef") %>' CommandName="details" CommandArgument='<%#Bind("CallID") %>'></asp:LinkButton>
                </ItemTemplate>
                 <EditItemTemplate>
                     <asp:LinkButton ID="lblTicketRef" runat="server" Text='<%#Bind("TicketRef") %>' CommandName="details" CommandArgument='<%#Bind("CallID") %>'></asp:LinkButton>
                 </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Details" >
                <ItemTemplate>
                    <asp:Label ID="lblDetails" runat="server" Text='<%#Bind("Details") %>'></asp:Label>
                </ItemTemplate>
                 <EditItemTemplate>
                      <asp:Label ID="lblDetails" runat="server" Text='<%#Bind("Details") %>'></asp:Label>
                 </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Service Provider"  ItemStyle-Width="15%">
                <ItemTemplate>
                    <asp:Label ID="lblServiceProvider" runat="server" Text='<%#Bind("ServiceProvider") %>'></asp:Label>
                </ItemTemplate>
                 <EditItemTemplate>
                      <asp:Label ID="lblServiceProvider" runat="server" Text='<%#Bind("ServiceProvider") %>'></asp:Label>
                 </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Cost"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                <ItemTemplate>
                    <asp:Label ID="lblCost" runat="server" Text='<%#Bind("Cost") %>'></asp:Label>
                </ItemTemplate>
                 <EditItemTemplate>
                      <asp:Label ID="lblCost" runat="server" Text='<%#Bind("Cost") %>'></asp:Label>
                 </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="VAT" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                <ItemTemplate>
                    <asp:Label ID="lblVAT" runat="server" Text='<%#Bind("VAT") %>'></asp:Label>
                </ItemTemplate>
                 <EditItemTemplate>
                     <asp:Label ID="lblVAT" runat="server" Text='<%#Bind("VAT") %>'></asp:Label>
                 </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Invoice Total"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="9%">
                <ItemTemplate>
                    <asp:Label ID="lblTotalCost" runat="server" Text='<%#Bind("TotalCost") %>'></asp:Label>
                </ItemTemplate>
                 <EditItemTemplate>
                      <asp:Label ID="lblTotalCost" runat="server" Text='<%#Bind("TotalCost") %>'></asp:Label>
                 </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                </ItemTemplate>
                 <EditItemTemplate>
                     <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                      <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>' Visible="false"></asp:Label>
                     <asp:Label ID="lblTicketStatus" runat="server" Text='<%#Bind("TicketStatus") %>' Visible="false"></asp:Label>
                     
                 </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
      <div class="form-group row">
        <div class="col-md-12">
           <strong>Credit Note</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                
                  <asp:GridView ID="gridCreditRecord" runat="server" AutoGenerateColumns="false" Width="100%" EmptyDataText="No data exists" OnRowCommand="gridCreditRecord_RowCommand">
                                               <Columns>
                                                   <asp:TemplateField HeaderText="Description" >
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblDescription" runat="server" Text='<%#Bind("Description") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="10%">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblValue" runat="server" Text='<%#Bind("CreditValue","{0:F2}") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Applied by"  ItemStyle-Width="15%">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblAppliedby" runat="server" Text='<%#Bind("Appliedby") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Date" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblDate" runat="server" Text='<%#Bind("DateandTime","{0:d}") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right" >
                                                       <ItemTemplate>
                                                           <asp:LinkButton SkinID="BtnLinkDelete" ID="btndelete" CommandName="del" runat="server" CommandArgument='<%#Bind("Id")%>' OnClientClick="return confirm('Do you want to delete the record?');" ></asp:LinkButton>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                               </Columns>
                                           </asp:GridView>

    
   
<%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
     <script type="text/javascript">
         function SelectAll(id) {
             //get reference of GridView control
             var grid = document.getElementById("<%= GridInvoice.ClientID %>");
             //variable to contain the cell of the grid
             var cell;


             if (grid.rows.length > 0) {
                 //loop starts from 1. rows[0] points to the header.
                 for (i = 1; i < grid.rows.length; i++) {
                     //get the reference of first column
                     cell = grid.rows[i].cells[0];

                     //loop according to the number of childNodes in the cell
                     for (j = 0; j < cell.childNodes.length; j++) {
                         //if childNode type is CheckBox                 
                         if (cell.childNodes[j].type == "checkbox") {
                             //assign the status of the Select All checkbox to the cell checkbox within the grid
                             cell.childNodes[j].checked = document.getElementById(id).checked;


                         }
                     }
                 }
             }

         }
         $(document).ready(function () {
             $('.check').click(function () {
                 SelectAll(1);
                 $(this).attr('checked', false);
                 debugger;
                 //$('.check').not(this).prop('checked', false);
                 //$('.check').each(function () {
                     
                 //    $(this).attr('checked', false);
                 //    $('#MainContent_MainContent_GridInvoice_chk_3').checked = false;
                 //});
             });
         });

         //grid_responsive_parent_display();
         //grid_responsive_nested_display();

         //$(window).load(function () {
         //    $(".dropdown-menu li")
         //  .find("input[type='checkbox']")
         //  .prop('checked', 'checked').trigger('change');
         //    $(".btn-toolbar").hide();
         //});
    </script>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script type="text/javascript">
        hidetabs();
    </script>
</asp:Content>
