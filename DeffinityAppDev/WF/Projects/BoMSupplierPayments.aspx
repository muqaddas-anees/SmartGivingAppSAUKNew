<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="BoMSupplierPayments" Codebehind="BoMSupplierPayments.aspx.cs" %>

<%@ Register Src="controls/FinanceModuleTab.ascx" TagName="FMTab" TagPrefix="uc2" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.BoMSupplierPayments%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc2:FMTab ID="tab" runat="server" />
    <script type="text/javascript" language="javascript">
        function clearAll() {
           
            document.getElementById("txtApplyInvoiceNumber").value = "";
            document.getElementById("txtApplyInvoiceDate").value = "";
           
        }
        function checkAll(checkbox) {
            var ext = checkbox.id;
            var gridId = ext.substring(0, ext.indexOf("gridGoodReceipt"));
            var gv = document.getElementById(gridId + "gridGoodReceipt");
            for (i = 1; i < gv.rows.length - 1; i++) {
                gv.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = checkbox.checked;
            }
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   
       
            <ajaxToolkit:ModalPopupExtender CancelControlID="ImgCancel" ID="mpopBOM" runat="server"
                PopupControlID="pnlBOM" TargetControlID="imgItemEdit" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:HiddenField ID="hdnID" runat="server" />
            <asp:Panel ID="pnlBOM" runat="server" BackColor="White" ScrollBars="None" Style="display: none"
                Width="650px" Height="300px" BorderStyle="Double" BorderColor="LightSteelBlue">
              
              <div class="form-group">
        <div class="col-md-10">
           <strong><asp:Label ID="lblTaskName" runat="server"></asp:Label> </strong> 
            <hr class="no-top-margin" />
            </div>
                  <div class="col-md-2">
                       <asp:LinkButton ID="imgCancel" runat="server" SkinID="BtnLinkCancel" />
                      </div>
    </div>
                <div>
                    <asp:Label ID="lblErr" ForeColor="Red" runat="server" Visible="false">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="GropPOP" />
                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="GropPOP1" />
                    </asp:Label></div>
               
                <%-- <asp:ImageButton ID="ImgCancel" runat="server" SkinID="ImgCancel" /> --%>
                <asp:Button runat="server" ID="imgItemEdit" Style="display: none" />
                <div style="padding: 20px">
                    <asp:Label ID="lblErrPop" ForeColor="Red" runat="server" Visible="false" EnableViewState="false"></asp:Label>
                    <asp:Panel ID="panel_grid" runat="server" Width="100%" Height="355px" ScrollBars="Auto">
                        <asp:GridView ID="gridPOP" runat="server" Width="100%" AutoGenerateColumns="False"
                            ShowFooter="true" EmptyDataText="<%$ Resources:DeffinityRes,Nodataavailable%>"
                            OnRowCancelingEdit="gridPOP_RowCancelingEdit" OnRowCommand="gridPOP_RowCommand"
                            OnRowDataBound="gridPOP_RowDataBound" OnRowDeleting="gridPOP_RowDeleting" OnRowEditing="gridPOP_RowEditing"
                            OnRowUpdating="gridPOP_RowUpdating">
                            <Columns>
                                <asp:TemplateField Visible="true">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible="false"> </asp:Label>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                    CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate" ToolTip="Update"
                                                    ValidationGroup="GropPOP1"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                    SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton></div>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        
                                                <asp:LinkButton CommandName="Add" ID="ImageButton5a" ToolTip="Add" runat="server"
                                                    SkinID="BtnLinkUpdate" ValidationGroup="GropPOP" />
                                           
                                                <asp:LinkButton CommandName="Cancel" ID="ImageButton22" runat="server" SkinID="BtnLinkCancel" />
                                           
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible='false'>
                                    <ItemTemplate>
                                        <asp:Label ID="lblID1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        <%--<asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNextDate" runat="server" Text='<%# Bind("NextDate","{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNextDate" SkinID="txtCalender" runat="server" Text='<%# Bind("NextDate","{0:d}") %>'></asp:TextBox>
                                        <asp:Label ID="imgbtnenddateerdf" runat="server" SkinID="Calender" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                            PopupButtonID="imgbtnenddateerdf" TargetControlID="txtNextDate" CssClass="MyCalendar">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfv_dateRisedwe1" runat="server" ControlToValidate="txtNextDate"
                                            Display="None" ErrorMessage="Please Enter  Date" ValidationGroup="GropPOP1"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNextDatef" runat="server" SkinID="Date"></asp:TextBox>
                                        <asp:Label ID="imgbtnenddateer" runat="server" SkinID="Calender" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                            PopupButtonID="imgbtnenddateer" TargetControlID="txtNextDatef" CssClass="MyCalendar">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfv_dateRised1" runat="server" ControlToValidate="txtNextDatef"
                                            Display="None" ErrorMessage="Please Enter  Date" ValidationGroup="GropPOP"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Quantity Available" DataField="QTY_aviable" ItemStyle-HorizontalAlign="Right"/>--%>
                                <asp:TemplateField HeaderText="Invoice Number" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="75px" />
                                    <FooterStyle Width="75px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceNumber" Width="75px" runat="server" Text='<%# Bind("InvoiceNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtInvoiceNumber" runat="server" Text='<%# Bind("InvoiceNumber") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_InvoiceNumber" runat="server" ControlToValidate="txtInvoiceNumber"
                                            Display="None" ErrorMessage="Please Enter invoice number" ValidationGroup="GropPOP"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtInvoiceNumberf" runat="server" Width="75px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_InvoiceNumberf" runat="server" ControlToValidate="txtInvoiceNumberf"
                                            Display="None" ErrorMessage="Please Enter invoice number" ValidationGroup="GropPOP"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                    <%-- <ItemStyle Width="50px" />--%>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Amount","{0:f2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAmountr" runat="server" SkinID="Price_75px" Text='<%# Bind("Amount","{0:f2}") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_InvoiceNumber1" runat="server" ControlToValidate="txtAmountr"
                                            Display="None" ErrorMessage="Please Enter amount" ValidationGroup="GropPOP1"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAmountrf" SkinID="Price" runat="server" Width="50px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_InvoiceNumberf1" runat="server" ControlToValidate="txtAmountrf"
                                            Display="None" ErrorMessage="Please Enter amount" ValidationGroup="GropPOP"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <FooterStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                            SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                                    </ItemTemplate>
                                    <FooterStyle Width="30px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </asp:Panel>
            <div>
                <div class="form-group">
      <div class="col-md-4 ">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ProjectReference%>:</label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtPrefix" runat="server" SkinID="txt_75px" Enabled="false"></asp:TextBox>
                <asp:TextBox ID="txtProjectRef" runat="server" SkinID="txt_60" ></asp:TextBox> <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator24"
                            runat="server" ControlToValidate="txtProjectRef" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidprojectreference%>"
                            ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator><asp:CheckBox ID="chkWorksheet" runat="server" Text="Populate&nbsp;Worksheet" Visible="false"
                                OnCheckedChanged="chkWorksheet_CheckedChanged" Checked="false" AutoPostBack="true" />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Project%> </label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlProjects" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged">
                            </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes. Worksheet%></label>
           <div class="col-sm-8">
                 <asp:DropDownList ID="ddlWorkSheet" runat="server" OnSelectedIndexChanged="ddlWorkSheet_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
            </div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Vendor%> </label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlVendor" runat="server" AutoPostBack="true"
                                 OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged"></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-5">
          
	</div>
	<div class="col-md-3 form-inline pull-right">
        <span class="pull-right">
          <asp:Button ID="imgSearch" ValidationGroup="check" runat="server" SkinID="btnSearch" OnClick="imgSearch_Click" />
        <asp:HyperLink ID="lnkRpt" runat="server" Target="_blank" NavigateUrl="#" SkinID="Button" Text="View Report" Style="vertical-align:baseline"></asp:HyperLink>
            </span>
	</div>
</div>
    <div class="form-group">
          <div class="col-md-12">
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
                <asp:ValidationSummary ID="ValInvoiceApply" runat="server" ValidationGroup="InvoiceApply"
                    DisplayMode="BulletList" />
                <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblApplyMsg" runat="server"></asp:Label>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Date%> </label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtApplyInvoiceDate" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
                                <asp:Label ID="imgbtnApplyDate" runat="server" SkinID="Calender" />
                                <ajaxToolkit:CalendarExtender ID="calInvoice"  runat="server"
                                    PopupButtonID="imgbtnApplyDate" TargetControlID="txtApplyInvoiceDate" CssClass="MyCalendar">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfv_applyInvoiceDate" runat="server" ControlToValidate="txtApplyInvoiceDate"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdate%>" ValidationGroup="InvoiceApply"
                                    SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtApplyInvoiceDate"
                                    SetFocusOnError="true" Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervaliddate%>"
                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                    ValidationGroup="InvoiceApply">*</asp:RegularExpressionValidator>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.InvoiceNumber%> </label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtApplyInvoiceNumber" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvInvoiceNumber" runat="server" ControlToValidate="txtApplyInvoiceNumber"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterinvoicenumber%>" ValidationGroup="InvoiceApply"
                                    SetFocusOnError="true" >*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-4 form-inline">
           <asp:Button ID="imgApply" runat="server" SkinID="btnApply" 
                                    ValidationGroup="InvoiceApply" OnClick="imgApply_Click" />
         <asp:Button ID="ImgClear" runat="server" SkinID="btnClear" OnClientClick="clearAll(); return false;"/>
	</div>
</div>
                <div>
                     <asp:Label ID="LblbtnCreditNote" runat="server"></asp:Label>
                </div>
                
                  <div class="clr"></div>
                <div class="form-group">
        <div class="col-md-12">
           <asp:Label ID="lblProject" runat="server"></asp:Label>
            <hr class="no-top-margin" />
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
                        Style="display:none" Width="650px" Height="200px" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="None">

                     <div class="form-group">
        <div class="col-md-10">
           <strong> <asp:Label ID="lblHName" Text="Credit Note" runat="server"></asp:Label></strong> 
            <hr class="no-top-margin" />
            </div>
                         <div class="col-md-2">
                             <asp:LinkButton ID="imgBtnCancel" runat="server" CausesValidation="false" SkinID="BtnLinkCancel"
                                                  ToolTip="<%$ Resources:DeffinityRes,Close%>" OnClick="imgBtnCancel_Click" />
                           <asp:Button ID="btnpopclose" runat="server" CausesValidation="false" Text="Close" Style="display:none;" />
                             </div>
    </div>

                   
                      
                      <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="PnlUpdateCreditNote">
                         <ProgressTemplate>
                               <asp:Label ID="imgloading_4" runat="server" SkinID="Loading" />
                         </ProgressTemplate>
                      </asp:UpdateProgress>
                       <asp:UpdatePanel ID="PnlUpdateCreditNote" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
                           <ContentTemplate>
                               <div class="form-group">
          <div class="col-md-12">
               <asp:Label ID="lblCreditMsg" EnableViewState="false" runat="server"></asp:Label>
                                           <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="CreditNote" />
	</div>
</div>
                               <div class="form-group">
      <div class="col-md-5">
           <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.Vendor%></label>
           <div class="col-sm-7">
               <asp:DropDownList ID="ddlVendorsIncredit" runat="server" SkinID="ddl_90"></asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="req2Credit" runat="server" ControlToValidate="ddlVendorsIncredit" Display="None"
                                                ErrorMessage="Please select vendor." InitialValue="0" ValidationGroup="CreditNote"></asp:RequiredFieldValidator>
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
                                       <asp:Button ID="btnCreditApply" runat="server" Text="Apply" ValidationGroup="CreditNote" OnClick="btnCreditApply_Click" />
                                       </div>
</div>
                               
                           </ContentTemplate>
                       </asp:UpdatePanel>
                 </asp:Panel>
                                

                <asp:GridView ID="gridGoodReceipt" Width="100%" runat="server" AutoGenerateColumns="false"
                    OnRowCommand="gridGoodReceipt_RowCommand" OnRowUpdating="gridGoodReceipt_RowUpdating"
                    OnRowDataBound="gridGoodReceipt_RowDataBound" EmptyDataText="No Records Found">
                    <Columns>
                        <%--  <asp:TemplateField  HeaderStyle-CssClass="header_bg_l" >



                                     <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                       
                                           <div style="width:45px"><div style="width:20px;float:left">
                                            <asp:ImageButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                CommandArgument="<%# Bind('ID')%>"  ImageUrl="~/media/ico_update.png"
                                                ToolTip="Update" ValidationGroup="Group2"></asp:ImageButton></div><div style="width:20px;float:left">
                                            <asp:ImageButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                ImageUrl="~/media/ico_cancel.png" ToolTip="Cancel"></asp:ImageButton></div>
                                                </div>
                                           
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                          
                                        </EditItemTemplate>
                                        
                                       
                                    </asp:TemplateField> --%>
                        <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                            <ItemStyle Width="20px" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkRecords" runat="server" />
                                <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("ID")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Received" Visible="true">
                            <ItemStyle Width="80px" />
                            <ItemTemplate>
                                <%--
                                        <asp:Label ID="lblID" runat="server" Text="<%# Bind('ID')%>" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtDateReceived" runat="server" Text="<%# Bind('ExpectedShipmentDate','{0:d}')%>" Width="65px"></asp:TextBox>
                                          <asp:Image ID="imgbtnenddate6" runat="server" SkinID="Calender" ImageAlign="AbsMiddle" />
    
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                                PopupButtonID="imgbtnenddate6" TargetControlID="txtDateReceived" CssClass="MyCalendar">
                                            </ajaxToolkit:CalendarExtender>
                                           <asp:RequiredFieldValidator ID="rfv_dateRised1" runat="server" ControlToValidate="txtDateReceived"
                                    Display="None" ErrorMessage="Please Enter Received Date"
                                    ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("ExpectedShipmentDate","{0:d}")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:ImageButton ID="imgUpdate" CommandName="Update" SkinID="ImgUpdate" runat="server" />--%>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" Visible="true" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="250px"  ControlStyle-Width="250px" FooterStyle-Width="250px">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description")%>'></asp:Label>
                                <%-- <asp:TextBox ID="txtDescription" runat="server" Text="<%# Bind('Description')%>" ></asp:TextBox>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vendor">
                            <ItemTemplate>
                                <asp:Label ID="lblVendor" runat="server" Text='<%#VendorName(DataBinder.Eval(Container.DataItem,"Supplier").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Part Number" ItemStyle-Width="85px" Visible="true">
                            <ItemTemplate>
                                <%--<asp:TextBox ID="txtPartNumber" runat="server" Text="<%# Bind('PartNumber')%>" ></asp:TextBox>--%>
                                <asp:Label ID="lblPartNumber" runat="server" Text='<%# Bind("PartNumber")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Ordered" ItemStyle-Width="85px" ItemStyle-HorizontalAlign="Right"
                            Visible="true">
                            <ItemTemplate>
                                <%-- <asp:TextBox ID="txtQtyOrdered" runat="server" Text="<%# Bind('QtyOrdered')%>" SkinID="Price" Width="40px"></asp:TextBox>--%>
                                <asp:Label ID="lblQtyOrdered" runat="server" Text='<%# Bind("Qty")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Received" ItemStyle-Width="85px" ItemStyle-HorizontalAlign="Right"
                            Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblQtyReceived" runat="server" Text='<%# Bind("QtyReceived")%>'></asp:Label>
                                <%-- <asp:TextBox ID="txtQtyReceived" runat="server" Text="<%# Bind('QtyReceived')%>" SkinID="Price" Width="40px"></asp:TextBox>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice Number" ItemStyle-Width="85px" Visible="false">
                            <ItemTemplate>
                                <%-- <asp:TextBox ID="txtInvoiceNumber" runat="server" Text="<%# Bind('InvoiceNumber')%>"  Width="75px"></asp:TextBox>--%>
                                <%--  <asp:Label ID="lbl" runat="server"  Text="<%# Bind('InvoiceNumber')%>"></asp:Label>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Next Expected Date" Visible="true">
                            <ItemStyle Width="80px" />
                            <ItemTemplate>
                                <%-- <asp:TextBox ID="txtNextExpectedDate" runat="server" Text="<%# Bind('NextShipmentDate','{0:d}')%>" Width="65px"></asp:TextBox>
                                          <asp:Image ID="imgbtnenddate7" runat="server" SkinID="Calender" ImageAlign="AbsMiddle" />
    
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender61"  runat="server"
                                                PopupButtonID="imgbtnenddate7" TargetControlID="txtNextExpectedDate" CssClass="MyCalendar">
                                            </ajaxToolkit:CalendarExtender>
                                           <asp:RequiredFieldValidator ID="rfv_dateRised" runat="server" ControlToValidate="txtDateReceived"
                                    Display="None" ErrorMessage="Please Enter Next Shippment Date"
                                    ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                                <asp:Label ID="lblNextExpectedDate" runat="server" Text='<%#BlankDate(DataBinder.Eval(Container.DataItem,"ID").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments" ItemStyle-Width="85px" Visible="false">
                            <ItemTemplate>
                                <%-- <asp:TextBox ID="txtComments" runat="server" Text="<%# Bind('Notes')%>" Width="100px"></asp:TextBox>--%>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Notes")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BOM Total" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total","{0:f2}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Invoiced" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalSum" runat="server" Text='<%#TotalInvoiced(DataBinder.Eval(Container.DataItem,"ID").ToString())%>'></asp:Label>
                                <%--<asp:TextBox ID="txtTotalInvoiced" runat="server" Text="<%# Bind('TotalSum')%>" Width="100px"></asp:TextBox>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Outstanding" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%--<asp:TextBox ID="txtTotalOutstanding" runat="server" Text="<%# Bind('outstanding')%>" Width="100px"></asp:TextBox>--%>
                                <asp:Label ID="lblTotaloutstanding" runat="server" Text='<%#TotalOutstanding(DataBinder.Eval(Container.DataItem,"ID").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-CssClass="header_bg_r">
                            <ItemTemplate>
                                <asp:Panel ID="pnlHover" runat="server">
                                    <asp:GridView ID="grdViewBOM" runat="server" Width="30%">
                                        <Columns>
                                            <asp:BoundField DataField="NextDate" DataFormatString="{0:d}" HeaderText="Date"
                                                HeaderStyle-CssClass="header_bg_l" />
                                            <asp:BoundField DataField="InvoiceNumber" HeaderText="Invoice&nbsp;Number" ItemStyle-Width="150px" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:f2}"
                                                HeaderStyle-CssClass="header_bg_r" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                <asp:Label ID="lblIDHover" runat="server" Text='<%# Bind("ID")%>' Visible="false"></asp:Label>
                                <asp:LinkButton ID="imgPOPUp" SkinID="BtnLinkAdd" CommandArgument='<%# Bind("ID")%>'
                                    CommandName="View" runat="server" />
                                <ajaxToolkit:HoverMenuExtender ID="hoverMenu" runat="server"  PopupControlID="pnlHover" PopDelay="25"
                                    TargetControlID="imgPOPUp" CacheDynamicResults="false"  OffsetX="-240" OffsetY="20" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
               
                <div class="form-group">
        <div class="col-md-12">
           <strong>Credit Note</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                
                  <asp:GridView ID="gridCreditRecord" runat="server" AutoGenerateColumns="false" Width="100%" EmptyDataText="No data exists">
                                               <Columns>
                                                   <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="header_bg_l">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblDescription" runat="server" Text='<%#Bind("Description") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="Right" >
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblValue" runat="server" Text='<%#Bind("CreditValue","{0:F2}") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Applied by">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblAppliedby" runat="server" Text='<%#Bind("Appliedby") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="header_bg_r">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblDate" runat="server" Text='<%#Bind("DateandTime","{0:d}") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                               </Columns>
                                           </asp:GridView>
            </div>
           
       
   
<%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
     <script type="text/javascript">
         grid_responsive_parent_display();
         grid_responsive_nested_display();

         $(window).load(function () {
             $(".dropdown-menu li")
           .find("input[type='checkbox']")
           .prop('checked', 'checked').trigger('change');
             $(".btn-toolbar").hide();
         });
    </script>
</asp:Content>

