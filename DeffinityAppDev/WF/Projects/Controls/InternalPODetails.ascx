<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="controls_InternalPODetails" Codebehind="InternalPODetails.ascx.cs" %>
    <%@ Register Src="~/WF/Projects/MailControls/BOMSupplierReq.ascx" TagName="SupplierMail" TagPrefix="QT1" %>
<QT1:SupplierMail ID="SupplierMail" runat="server" Visible="false" />
<div>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group1" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Group2" />
    <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Group3" />
    <asp:HiddenField ID="hdnID" runat="server" Value="0" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group4" />
    <asp:Label ID="lblMsg" runat="server" Text="" Visible="false"></asp:Label>
</div>


<div class="form-group">
      <div class="col-md-6">
          <div class="form-group">
        <div class="col-md-12">
           <strong>General&nbsp;Information </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
          <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"> PO&nbsp;Number</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtPONumber" runat="server" SkinID="txt_150px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter PO number"
                        Display="None" ValidationGroup="Group4" ControlToValidate="txtPONumber"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
          <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Date</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtPODate" runat="server" SkinID="Date"></asp:TextBox>
                    <asp:Label ID="imgPODate" runat="server" SkinID="Calender" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                        PopupButtonID="imgPODate" TargetControlID="txtPODate" CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter date"
                        Display="None" ValidationGroup="Group4" ControlToValidate="txtPODate"></asp:RequiredFieldValidator>
               <asp:RegularExpressionValidator ID="REV1" runat="server" ErrorMessage="Please enter valid date"
                    ControlToValidate="txtPODate" ValidationGroup="Group4" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                    Display="None">
                </asp:RegularExpressionValidator>
            </div>
	</div>
</div>
          <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"> Requested&nbsp;By</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlRequestedBy" runat="server" SkinID="ddl_80">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select requester"
                        Display="None" ValidationGroup="Group4" ControlToValidate="ddlRequestedBy" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
          <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Approved&nbsp;By</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlApprover" runat="server" SkinID="ddl_80">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please select approver"
                        Display="None" ValidationGroup="Group4" ControlToValidate="ddlApprover" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
          <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Purchased&nbsp;By</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlPurchaser" runat="server" SkinID="ddl_80">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please select approver"
                        Display="None" ValidationGroup="Group4" ControlToValidate="ddlPurchaser" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
          <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Notes</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Height="47px" Width="279px" SkinID="txtMulti_100"></asp:TextBox>
            </div>
	</div>
</div>
          <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
               <asp:Button ID="imgSave" runat="server" SkinID="btnSave" ValidationGroup="Group4"
                        OnClick="imgSave_Click" Visible="false" />
            </div>
	</div>
</div>
         
	</div>
	<div class="col-md-6">
          <div class="form-group">
        <div class="col-md-12">
           <strong> Vendor&nbsp;Information</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

        <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Vendor</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlVendor" runat="server" Width="200px" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Please select vendor"
                        Display="None" ValidationGroup="Group4" ControlToValidate="ddlVendor" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
            <asp:Button ID="imgAdd" runat="server" SkinID="btnAdd" OnClick="imgAdd_Click" />
	</div>
</div>

        <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Address</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label">Contact Name</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtContactName" runat="server"></asp:TextBox>
            </div>
	</div>
</div>

        <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Address</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"> Contact Tel</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtContactTel" runat="server"></asp:TextBox>
            </div>
	</div>
</div>
        <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Postcode</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtPostcode" runat="server"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label">Contact Email</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </div>
	</div>
</div>
         <div>
       
    </div>
        <div class="form-group">
        <div class="col-md-12">
           <strong>Purchase&nbsp;for&nbsp;Project</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
   <div class="form-group">
    <div class="col-md-12">
        <label class="col-sm-3 control-label">Select&nbsp;Project</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlProject" runat="server"
           SkinID="ddl_80">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please select project"
            Display="None" ValidationGroup="Group4" ControlToValidate="ddlProject" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
       
    </div>
       </div>
	</div>
	
</div>


    <asp:GridView ID="grdPODetails" runat="server" AutoGenerateColumns="False" EmptyDataText="No Records Found"
        OnRowCommand="grdPODetails_RowCommand" OnRowDataBound="grdPODetails_RowDataBound"
        OnRowEditing="grdPODetails_RowEditing" OnRowUpdating="grdPODetails_RowUpdating"
        OnRowCancelingEdit="grdPODetails_RowCancelingEdit">
        <Columns>
            <asp:TemplateField HeaderStyle-CssClass="header_bg_l" Visible="false">
                <ItemStyle Width="30px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text="<%# Bind('ID')%>" Visible="false"> </asp:Label>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                        CommandArgument="<%# Bind('ID')%>" SkinID="BtnLinkEdit" ToolTip="Edit">
                    </asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                        CommandArgument="<%# Bind('ID')%>" ValidationGroup="Group2" SkinID="BtnLinkUpdate"
                        ToolTip="Update"></asp:LinkButton>
                    <asp:LinkButton  ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                        SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton CommandName="Add" ID="ImageButton5a" runat="server" SkinID="BtnLinkAdd"
                        ValidationGroup="Group3" />&nbsp;<asp:LinkButton CommandName="Cancel" ID="ImageButton22"
                            runat="server" SkinID="BtnLinkCancel" />
                </FooterTemplate>
                <FooterStyle Width="30px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item&nbsp;Number" Visible="false" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="250px"  ControlStyle-Width="250px">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="75px" />
                <ItemTemplate>
                    <asp:Label ID="lblItemNumber" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtItemNumber" runat="server" Width="75px" Text='<%# Bind("Description") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqInvoiceNum1" runat="server" ErrorMessage="Please enter item number"
                        ControlToValidate="txtItemNumber" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <%-- <FooterTemplate>
                                        <asp:TextBox ID="txtItemNumberf" runat="server" Width="75px"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="ReqInvoiceNumf" runat="server" ErrorMessage="Please enter item number"
                                           ControlToValidate="txtItemNumberf" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        </FooterTemplate> --%>
                <FooterStyle Width="50px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="250px"  ControlStyle-Width="250px">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDescription" runat="server" SkinID="txt_150px" Text='<%# Bind("Description") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqInvoiceNum12" runat="server" ErrorMessage="Please enter Description"
                        ControlToValidate="txtDescription" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <%-- <FooterTemplate>
                                        <asp:TextBox ID="txtDescriptionf" runat="server" Width="75px"  Text='<%# Bind("Description") %>'></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="ReqInvoiceNum121" runat="server" ErrorMessage="Please enter Description"
                                           ControlToValidate="txtDescriptionf" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        </FooterTemplate> --%>
                <FooterStyle Width="50px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Part&nbsp;Number" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="250px"  ControlStyle-Width="250px">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" />
                <EditItemTemplate>
                    <asp:TextBox ID="txtPartNumber" runat="server" Text='<%# Bind("PartNumber") %>' SkinID="txt_75px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPartNumber"
                        Display="None" ErrorMessage="Please enter part number" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPartNumber" runat="server" Text='<%# Bind("PartNumber") %>' Width="50px"></asp:Label>
                </ItemTemplate>
                <%--  <FooterTemplate>
                                        
                                        <asp:TextBox ID="txtPartNumberF" runat="server" 
                                                Width="75px"></asp:TextBox>
                                           
                                            <asp:RequiredFieldValidator ID="RequireDateRaisedF" runat="server" ControlToValidate="txtPartNumberF"
                                                Display="None" ErrorMessage="Please enter part number" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                           
                                        </FooterTemplate>--%>
                <FooterStyle Width="50px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtUnit" runat="server" SkinID="txt_75px" Text='<%# Eval("Unit") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqTotalAmt1" runat="server" ErrorMessage="Please enter unit"
                        ControlToValidate="txtUnit" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <%-- <FooterTemplate>
                                         <asp:TextBox ID="txtUnitf" runat="server" Width="75px"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="ReqTotalAmtf" runat="server" ErrorMessage="Please enter unit"
                                           ControlToValidate="txtUnitf" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        </FooterTemplate>--%>
                <FooterStyle Width="50px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
                <EditItemTemplate>
                    <asp:TextBox ID="txtQty" runat="server" Text='<%# Eval("QtyOrderd") %>' SkinID="txt_75px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqTotalAmt2" runat="server" ErrorMessage="Please enter Quantity"
                        ControlToValidate="txtQty" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblQty" runat="server" Text='<%# Eval("QtyOrderd") %>'></asp:Label>
                </ItemTemplate>
                <%-- <FooterTemplate >
                                            <asp:TextBox ID="txtQtyf" runat="server" 
                                                Width="50px"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="ReqTotalAmt3" runat="server" 
                                            ErrorMessage="Please enter Quantity"
                                           ControlToValidate="txtQtyf" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        </FooterTemplate>--%>
                <FooterStyle Width="50px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Total","{0:F2}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtPrice" runat="server" SkinID="Price_100px" Text='<%# Bind("Total","{0:F2}") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqTotalPaid" runat="server" ErrorMessage="Please enter price"
                        ControlToValidate="txtPrice" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <%-- <FooterTemplate>
                                        <asp:TextBox ID="txtPricef" runat="server" Width="75px"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="ReqTotalPaidf1" runat="server"
                                          ErrorMessage="Please enter price"
                                           ControlToValidate="txtPricef" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        </FooterTemplate> --%>
                <FooterStyle Width="50px" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty Received">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblQtyRec" runat="server" Text='<%# Bind("QtyRec") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtQtyRec" runat="server" SkinID="txt_75px" Text='<%# Bind("QtyRec") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqBalance1" runat="server" ErrorMessage="Please enter received quantiy"
                        ControlToValidate="txtQtyRec" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <%--  <FooterTemplate>
                                        <asp:TextBox ID="txtQtyRecf" runat="server" Width="75px"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="ReqTotalPaidf2" runat="server"
                                          ErrorMessage="Please enter received quantiy"
                                           ControlToValidate="txtQtyRecf" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        </FooterTemplate>--%>
                <FooterStyle Width="50px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty Outstanding">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblQtyOut" runat="server" Text='<%# Bind("QtyOut") %>'></asp:Label>
                </ItemTemplate>
                <%--<EditItemTemplate>
                                          <asp:TextBox ID="txtQtyOut" runat="server" Width="75px"  Text='<%# Bind("QtyOut") %>'></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="ReqBalance2" runat="server"
                                      ErrorMessage="Please enter outstanding quantiy"
                                           ControlToValidate="txtQtyOut" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                        </EditItemTemplate> 
                                        <FooterTemplate>
                                        <asp:TextBox ID="txtQtyOutf" runat="server" Width="75px"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="ReqTotalPaidf3" runat="server"
                                          ErrorMessage="Please enter outstanding quantiy"
                                           ControlToValidate="txtQtyOutf" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                      <FooterStyle Width="50px"  HorizontalAlign="Center"/>--%>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="200px"  ControlStyle-Width="200px">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="75px" />
                <ItemTemplate>
                    <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" SkinID="txt_150px" Text='<%# Bind("Notes") %>'></asp:TextBox>
                </EditItemTemplate>
                <%-- <FooterTemplate>
                                        <asp:TextBox ID="txtNotesf" runat="server" TextMode="MultiLine" Width="100px"></asp:TextBox>
                                         
                                        </FooterTemplate> --%>
                <FooterStyle Width="50px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>



<div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Email to:</label>
           <div class="col-sm-5">
                <asp:TextBox ID="txtSupplierMail" runat="server" Width="283px"></asp:TextBox>
            </div>
               <div class="col-sm-4">
                   <asp:Button ID="imgSaveAndSubmit" runat="server" ValidationGroup="Group4" SkinID="btnDefault" Text="Save and submit to supplier"
                    OnClick="imgSaveAndSubmit_Click" />
            </div>
	</div>
</div>



<%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 