<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_CustomerPODetails" Codebehind="CustomerPODetails.ascx.cs" %>
<script language="javascript" type="text/javascript">
    function MutExChkList(chk) {
        var chkList = chk.parentNode.parentNode.parentNode;
        var chks = chkList.getElementsByTagName("input");
        for (var i = 0; i < chks.length; i++) {
            if (chks[i] != chk && chk.checked) {
                chks[i].checked = false;
            }
        }
    }

</script>
<div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group2" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Group3" />
    <asp:Label ID="lblMsg" runat="server" Text="" Visible="false"></asp:Label>
    <asp:HiddenField ID="hdnID" runat="server" Value="0"/>
</div>
 


<div class="form-group">
      <div class="col-md-12">
          <table cellpadding="0" cellspacing="0" border="0">
          <tr>
              <td>
                  Customer</td>
              <td>
                  <asp:DropDownList ID="ddlCustomers" runat="server" Width="150px">
                  </asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                 ErrorMessage="Please select customer" Display="None" ValidationGroup="Group1"
                  ControlToValidate="ddlCustomers" InitialValue="0" ></asp:RequiredFieldValidator>
              </td>
              <td align="left">
                  Select&nbsp;Payment&nbsp;Method</td>
              <td>
                  Date&nbsp;Raised</td>
              <td>
                  <asp:TextBox ID="txtDateRaised" runat="server" SkinID="Date"></asp:TextBox>
                   <asp:Label ID="imgDateRaised" runat="server" SkinID="Calender" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender7"  runat="server"
                                        PopupButtonID="imgDateRaised" TargetControlID="txtDateRaised" CssClass="MyCalendar">
                                    </ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                 ErrorMessage="Please enter date" Display="None" ValidationGroup="Group1"
                  ControlToValidate="txtDateRaised" ></asp:RequiredFieldValidator>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  PO&nbsp;Number</td>
              <td align="left">
                  <asp:TextBox ID="txtPONumber" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                 ErrorMessage="Please Enter PO Number" Display="None" ValidationGroup="Group1"
                  ControlToValidate="txtPONumber" ></asp:RequiredFieldValidator>
              </td>
              <td rowspan="3" valign="top" align="left" valign="top">
                  <asp:RadioButtonList ID="radPaymentType" runat="server">
                  <asp:ListItem Selected="True" Value="1">Payment&nbsp;with&nbsp;reference&nbsp;to&nbsp;an&nbsp;invoice</asp:ListItem>
                  <asp:ListItem  Value="2">Payment&nbsp;without&nbsp;reference&nbsp;to&nbsp;an&nbsp;invoice</asp:ListItem>
                  </asp:RadioButtonList>
              </td>
              <td>
                  Duration&nbsp;In&nbsp;Days</td>
              <td>
                  <asp:TextBox ID="txtDurationDays" runat="server"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                 ErrorMessage="Please enter days" Display="None" ValidationGroup="Group1"
                  ControlToValidate="txtDurationDays" ></asp:RequiredFieldValidator>
              </td>
              <td>
                  </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  Value</td>
              <td align="left">
                  <asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                 ErrorMessage="Please enter value" Display="None" ValidationGroup="Group1"
                  ControlToValidate="txtValue" ></asp:RequiredFieldValidator>
              </td>
              <td>
                  Raised&nbsp;By</td>
              <td>
                  <asp:DropDownList ID="ddlRaisedBy" runat="server" Width="150px">
                  </asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                 ErrorMessage="Please select user" Display="None" ValidationGroup="Group1"
                  ControlToValidate="ddlRaisedBy" InitialValue="0" ></asp:RequiredFieldValidator>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  Project</td>
              <td align="left">
                 <%-- <asp:DropDownList ID="ddlProjects" runat="server" Width="150px">
                  </asp:DropDownList>--%>
                  <asp:DropDownList ID="ddlProjects" runat="server" Width="150px">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlProjects"
                                    Category="Title" PromptText="Please select..." ServicePath="~/ServiceMgr.asmx"
                                    ServiceMethod="GetAllProjectRef" ParentControlID="ddlCustomers" />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                 ErrorMessage="Please select project" Display="None" ValidationGroup="Group1"
                  ControlToValidate="ddlProjects" InitialValue="" ></asp:RequiredFieldValidator>
              </td>
              <td>
                  Related&nbsp;To&nbsp;PO</td>
              <td>
                  <asp:TextBox ID="txtRelatedToPO" runat="server"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                 
              </td>
          </tr>
          <tr>
              <td>
                  Details&nbsp;of&nbsp;the&nbsp;PO</td>
              <td align="left" colspan="2">
                  <asp:TextBox ID="txtDetails" runat="server" TextMode="MultiLine" Width="470px" 
                      Height="53px"></asp:TextBox>
                  &nbsp;&nbsp;&nbsp;
                  </td>
              <td>
                 
                  PO&nbsp;Expiry&nbsp;Date</td>
              <td>
                 <asp:TextBox ID="txtPOExpDate" runat="server" SkinID="Date"></asp:TextBox>
                   <asp:Label ID="imgPOExpDate" runat="server" SkinID="Calender" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                        PopupButtonID="imgPOExpDate" TargetControlID="txtPOExpDate" CssClass="MyCalendar">
                                    </ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                 ErrorMessage="Please enter date" Display="None" ValidationGroup="Group1"
                  ControlToValidate="txtPOExpDate" ></asp:RequiredFieldValidator></td>
              <td>
                 </td>
              <td>
                 </td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td align="left" colspan="2">
                  <asp:Button ID="imgSave" runat="server" SkinID="btnSave" 
                      onclick="imgSave_Click" ValidationGroup="Group1" />  &nbsp; &nbsp;
                  <asp:Button ID="imgUpdate" runat="server" SkinID="btnUpdate" 
                      onclick="imgUpdate_Click" ValidationGroup="Group1" Visible="False" />
              </td>
              <td>
                 
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
      </table>
          
	</div>
	
</div>



<asp:GridView ID="grdPODetails" runat="server"  AutoGenerateColumns="False" 
                               
                          
        EmptyDataText="No Records Found" 
            onrowcommand="grdPODetails_RowCommand" 
            onrowdatabound="grdPODetails_RowDataBound" 
            onrowediting="grdPODetails_RowEditing" onrowupdating="grdPODetails_RowUpdating" onrowcancelingedit="grdPODetails_RowCancelingEdit" 
       
                                >
                                <Columns>
                                   
                                     
                                     <asp:TemplateField  HeaderStyle-CssClass="header_bg_l" >
                                     <ItemStyle Width="30px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text="<%# Bind('ID1')%>" Visible="false"> </asp:Label>
                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                CommandArgument="<%# Bind('ID1')%>" SkinID="BtnLinkEdit" ToolTip="Edit" >
                                            </asp:LinkButton>
                                           
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                CommandArgument="<%# Bind('ID1')%>" ValidationGroup="Group2" SkinID="BtnLinkUpdate"
                                                ToolTip="Update"></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                                    <asp:LinkButton CommandName="Add" ID="ImageButton5a" runat="server"
                                                        SkinID="BtnLinkUpdate" ValidationGroup="Group3" />&nbsp;<asp:LinkButton
                                                            CommandName="Cancel" ID="ImageButton22" runat="server" SkinID="BtnLinkCancel" />
                                                </FooterTemplate>
                                                <FooterStyle Width="30px" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Invoice&nbsp;Number" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="200px"  ControlStyle-Width="200px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="75px" HorizontalAlign="Center" />
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice" runat="server"  Text='<%# Bind("InvoiceNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                      <EditItemTemplate>
                                          <asp:TextBox ID="txtInvoiceNum" runat="server" SkinID="txt_150px"  Text='<%# Bind("InvoiceNumber") %>'></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="ReqInvoiceNum" runat="server" ErrorMessage="Please enter invoice number"
                                           ControlToValidate="txtInvoiceNum" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                        </EditItemTemplate> 
                                        <FooterTemplate>
                                        <asp:TextBox ID="txtInvoiceNumf" runat="server" SkinID="txt_150px"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="ReqInvoiceNumf" runat="server" ErrorMessage="Please enter invoice number"
                                           ControlToValidate="txtInvoiceNumf" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        </FooterTemplate> 
                                    </asp:TemplateField>
                                    
                                    
                                     <asp:TemplateField HeaderText="Project&nbsp;Reference" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="200px"  ControlStyle-Width="200px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                      <ItemStyle Width="200px" HorizontalAlign="Center" />
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblProject" runat="server"  Text='<%# Bind("Title") %>'></asp:Label>
                                        </ItemTemplate>
                                      <EditItemTemplate>
                                            <asp:DropDownList ID="ddlProjectTitle"  runat="server">
                                            </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator126" runat="server" ControlToValidate="ddlProjectTitle"
                                                Display="None" ErrorMessage="Please select project" ValidationGroup="Group2" InitialValue="0"></asp:RequiredFieldValidator>
                                        
                                        </EditItemTemplate> 
                                        <FooterTemplate>
                                        <asp:DropDownList ID="ddlProjectTitlef"  runat="server"    Width="150px">
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="ddlProjectTitlef"
                                                Display="None" ErrorMessage="Please select project" ValidationGroup="Group3" InitialValue="0"></asp:RequiredFieldValidator>
                                        
                                        </FooterTemplate> 
                                    </asp:TemplateField>
                                    
                                    
                                     <asp:TemplateField HeaderText="Date&nbsp;Raised" FooterStyle-CssClass="form-inline" ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline">
                                        <HeaderStyle HorizontalAlign="Center"/>
                                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDateRaisedG" runat="server" Text='<%# Bind("DateRaised","{0:d}") %>'
                                                SkinID="Date"></asp:TextBox>
                                            <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender"  />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                                PopupButtonID="imgbtnenddate6" TargetControlID="txtDateRaisedG" CssClass="MyCalendar">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDateRaisedG"
                                                Display="None" ErrorMessage="Date can not be blank" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDateRaisedG"
                                                Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                ValidationGroup="Group2">*</asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("DateRaised","{0:d}") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        
                                        <FooterTemplate>
                                        
                                        <asp:TextBox ID="txtDateRaisedF" runat="server" 
                                                SkinID="Date"></asp:TextBox>
                                            <asp:Label ID="imgDateRaisedf" runat="server" SkinID="Calender"  />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                                PopupButtonID="imgDateRaisedf" TargetControlID="txtDateRaisedF" CssClass="MyCalendar">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequireDateRaisedF" runat="server" ControlToValidate="txtDateRaisedF"
                                                Display="None" ErrorMessage="Date can not be blank" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegDateRaisedF" runat="server" ControlToValidate="txtDateRaisedF"
                                                Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                ValidationGroup="Group3">*</asp:RegularExpressionValidator>
                                        </FooterTemplate>
                                       <FooterStyle  HorizontalAlign="Center" Width="150px"/>
                                    </asp:TemplateField>
                                   
                                                                     
                                    <asp:TemplateField HeaderText="Total&nbsp;Amount" >
                                    <HeaderStyle HorizontalAlign="Center"/>
                                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                                     <ItemTemplate>
                                     <asp:Label ID="lblTotalAmt" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                     </ItemTemplate>
                                       
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtTotalAmt" runat="server" SkinID="Price_125px" Text='<%# Eval("TotalAmount") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="ReqTotalAmt" runat="server" ErrorMessage="Please enter total amount"
                                           ControlToValidate="txtTotalAmt" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                         <asp:TextBox ID="txtTotalAmtf" runat="server" SkinID="Price_125px"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="ReqTotalAmtf" runat="server" ErrorMessage="Please enter total amount"
                                           ControlToValidate="txtTotalAmtf" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField >
                                  <asp:TemplateField HeaderText="Currency">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="75px"  />
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCurrencey" Visible="false" runat="server" Text='<%# Eval("Currencey") %>'
                                               SkinID="Price_125px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrencey" runat="server" Text='<%# Eval("Currencey") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCurrenceyf" Visible="false" runat="server" 
                                                Width="75px"></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle Width="75px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total&nbsp;Paid">
                                        <HeaderStyle  HorizontalAlign="Center"/>
                                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalPaid" runat="server"  Text='<%# Bind("TotalPaid") %>'></asp:Label>
                                        </ItemTemplate>
                                      <EditItemTemplate>
                                          <asp:TextBox ID="txtTotalPaid" runat="server" SkinID="Price_125px"  Text='<%# Bind("TotalPaid") %>'></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="ReqTotalPaid" runat="server" ErrorMessage="Please enter total paid"
                                           ControlToValidate="txtTotalPaid" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                        </EditItemTemplate> 
                                        <FooterTemplate>
                                        <asp:TextBox ID="txtTotalPaidf" runat="server" SkinID="Price_125px"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="ReqTotalPaidf" runat="server" ErrorMessage="Please enter total paid"
                                           ControlToValidate="txtTotalPaidf" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        </FooterTemplate> 
                                         <FooterStyle Width="75px" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                  <asp:TemplateField HeaderText="Balance">
                                        <HeaderStyle HorizontalAlign="Center"/>
                                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalance" runat="server"  Text='<%# Bind("Balance") %>'></asp:Label>
                                        </ItemTemplate>
                                     <%-- <EditItemTemplate>
                                          <asp:TextBox ID="txtBalance" runat="server" Width="75px"  Text='<%# Bind("TotalPaid") %>'></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="ReqBalance" runat="server" ErrorMessage="Please enter b"
                                           ControlToValidate="txtBalance" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                        </EditItemTemplate> 
                                        <FooterTemplate>
                                        <asp:TextBox ID="txtTotalPaidf" runat="server" Width="75px"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="ReqTotalPaidf" runat="server" ErrorMessage="Please enter total paid"
                                           ControlToValidate="txtTotalPaidf" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        </FooterTemplate>--%> 
                                        
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Notes" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="200px"  ControlStyle-Width="200px" FooterStyle-Width="200px">
                                       <HeaderStyle  HorizontalAlign="Center"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNotes" runat="server"  Text='<%# Bind("Notes") %>'></asp:Label>
                                        </ItemTemplate>
                                      <EditItemTemplate>
                                          <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" SkinID="txtMulti_100"  Text='<%# Bind("Notes") %>'></asp:TextBox>
                                    
                                        </EditItemTemplate> 
                                        <FooterTemplate>
                                        <asp:TextBox ID="txtNotesf" runat="server" TextMode="MultiLine" SkinID="txtMulti_100"></asp:TextBox>
                                         
                                        </FooterTemplate> 
                                        
                                    </asp:TemplateField>
                                    
                                    
                                   
                                    
                                    
                                    <asp:TemplateField HeaderText="Retention&nbsp;Value" >
                                        <ItemStyle Width="75px"  />
                                        <HeaderStyle HorizontalAlign="Center" />
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblRetentionValue" runat="server" Text='<%# Bind("RetentionValue") %>' ></asp:Label>
                                        </ItemTemplate>
                                         <EditItemTemplate>
                                             <asp:TextBox ID="txtRetentionValue" runat="server"  Text='<%# Bind("RetentionValue") %>'></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="ReqRetentionValue" runat="server" ErrorMessage="Please enter retention value"
                                           ControlToValidate="txtRetentionValue" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                         <FooterTemplate>
                                        <asp:TextBox ID="txtRetentionValuef" runat="server"  Width="75px"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="ReqRetentionValuef" runat="server" ErrorMessage="Please enter retention value"
                                           ControlToValidate="txtRetentionValuef" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        </FooterTemplate> 
                                        
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="header_bg_r" HeaderText="Retention&nbsp;Reminder">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="15px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%--<asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                                SkinID="ImgSymDel" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />--%>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                                
                             </Columns>
                            </asp:GridView>

<div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Payment Mode:</label>
           <div class="col-sm-5">
               <asp:CheckBoxList ID="chkPaymentMode" runat="server">
    <asp:ListItem Selected="True" Value="1">Cheque</asp:ListItem>
    <asp:ListItem Value="2">Bank Transfer</asp:ListItem>
        </asp:CheckBoxList>
            </div>
	</div>
</div>
<%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 