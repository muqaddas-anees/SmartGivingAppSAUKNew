<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_Budget_Expenses" Codebehind="Budget_Expenses.ascx.cs" %>
<style>
    .round
    {
        border: 1px solid Silver;
        padding: 5px 5px;
        background: #d1e7ed;
        width: 300px;
        border-radius: 8px;
    }
</style>

<div>
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="expenseUpdate" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="expenseInsert" />
    </div>
        <div class="form-group">
        <div class="col-md-4 well" >
            <div class="form-group">
          <div class="col-md-12">
          <label class="col-sm-7 control-label"><%= Resources.DeffinityRes.ForecastExpense%></label>
           <div class="col-sm-3 style=" style="text-align:right;">
               <asp:Label ID="lblForecastExpense" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
</div>
            <div class="form-group">
          <div class="col-md-12">
          <label class="col-sm-7 control-label"><%= Resources.DeffinityRes.Spenttodate%></label>
           <div class="col-sm-3 style=" style="text-align:right;">
                <asp:Label ID="lblSpentToDate" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
</div>
            <div class="form-group">
          <div class="col-md-12">
          <label class="col-sm-7 control-label"><%= Resources.DeffinityRes.Remaining%></label>
           <div class="col-sm-3 style=" style="text-align:right;">
                <asp:Label ID="lblRemaining" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
</div>
        </div>
        </div>
     <div class="form-group">
          <div class="col-md-4">
              <div class="form-group">
          <div class="col-md-12 form-inline">
          <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.EntryType%></label>
           <div class="col-sm-8 form-inline">
                  <asp:DropDownList ID="ddlEntryTypeFilter" runat="server" DataSourceID="SqlDataSourceEntryType" DataTextField="ExpensesentryType" SkinID="ddl_70"
                       DataValueField="EntryTypeID"></asp:DropDownList>
               <asp:Button ID="imgView" runat="server" Text="View" SkinID="btnDefault" OnClick="imgView_Click" />
              
                <asp:SqlDataSource ID="SqlDataSourceEntryType" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="DEFFINITY_ExpensesType" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
            </div>
	</div>
</div>
          </div>
     </div>
     <asp:Label ID="l11" runat="server"></asp:Label>
    <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="False" Width="100%"
                        EmptyDataText="No external expenses found" OnRowDataBound="gvExpense_RowDataBound" AllowPaging="true" PageSize="20"
                        OnRowCommand="gvExpense_RowCommand" OnRowCancelingEdit="gvExpense_RowCancelingEdit"
                        OnRowEditing="gvExpense_RowEditing" OnRowDeleting="gvExpense_RowDeleting" 
                        OnRowUpdating="gvExpense_RowUpdating" 
                        onpageindexchanging="gvExpense_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkeditexternalExpenses" runat="server" CausesValidation="false"
                                        Enabled="<%#CommandField()%>" CommandName="Edit" CommandArgument='<%# Bind("ID")%>'
                                         SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkUpdateernalExpenses" runat="server" CommandName="Update"
                                        Text="<%$ Resources:DeffinityRes,Update%>" CommandArgument='<%# Bind("ID")%>'
                                         SkinID="BtnLinkUpdate" ToolTip="<%$ Resources:DeffinityRes,Update%>"
                                        ValidationGroup="expenseUpdate"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkCancelernalExpenses" runat="server" CausesValidation="false"
                                        CommandName="Cancel" SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>">
                                    </asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="LinkUpdateernalExpenses" runat="server" CommandName="Insert_ExternalFooter"
                                        Enabled="<%#CommandField()%>" Text="<%$ Resources:DeffinityRes,Update%>" SkinID="BtnLinkUpdate"
                                        ToolTip="Insert" ValidationGroup="expenseInsert"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkCancelernalExpenses" runat="server" CommandName="Cancel"
                                         SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>">
                                    </asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgSave" runat="server" SkinID="BtnLinkSave" CommandName="Saving"
                                                                                         CommandArgument='<%# Bind("ID")%>' ToolTip="Saving" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Date%>" ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
                                <HeaderStyle HorizontalAlign="Center" />
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server" Text='<%# Bind("ExternalExpensesDate","{0:d}") %>' SkinID="Date" ></asp:TextBox>
                                    <asp:Label ID="imgDate" runat="server" SkinID="Calender"></asp:Label>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                        PopupButtonID="imgDate" TargetControlID="txtDate" CssClass="MyCalendar">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDate"
                                        SetFocusOnError="true" Display="None" ErrorMessage="Please enter date" ValidationGroup="expenseUpdate"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDate"
                                        SetFocusOnError="true" Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Plsentervaliddateindatefield%>"
                                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                        ValidationGroup="expenseUpdate">*</asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("ExternalExpensesDate","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    
                                    <asp:TextBox ID="txtFooterDate" runat="server" Text='<%# Bind("ExternalExpensesDate","{0:d}") %>' SkinID="Date"></asp:TextBox>
                                    <asp:Label ID="imgbtnenddateExpenses_Footer" runat="server" SkinID="Calender"></asp:Label>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                        PopupButtonID="imgbtnenddateExpenses_Footer" TargetControlID="txtFooterDate"
                                        CssClass="MyCalendar">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiDT6" runat="server" ControlToValidate="txtFooterDate"
                                        Display="None" ErrorMessage="Please enter date" ValidationGroup="expenseInsert"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RSTdR" runat="server" ControlToValidate="txtFooterDate"
                                        SetFocusOnError="true" Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Plsentervaliddateindatefield%>"
                                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                        ValidationGroup="expenseInsert">*</asp:RegularExpressionValidator>
                                       
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="<%$ Resources:DeffinityRes,Description%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description")%>'></asp:TextBox>
                                    <%-- <asp:ImageButton ID="imgDescription" runat="server" SkinID="ImgSymAdd" ImageAlign="Right" />--%>
                                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                                        ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdescription%>" Display="None"
                                        SetFocusOnError="true" ValidationGroup="expenseUpdate"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFooterDescription" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFooterDescription" runat="server" ControlToValidate="txtFooterDescription"
                                        ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdescription%>" Display="None"
                                        SetFocusOnError="true" ValidationGroup="expenseInsert"></asp:RequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EntryType%>" ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
                                <EditItemTemplate>
                                     
                                    <asp:DropDownList ID="ddlEntryExternalExpenses" runat="server" DataSourceID="SqlDataSourceEntry2TandE23"
                                        DataTextField="ExpensesentryType" DataValueField="EntryTypeID" SelectedValue='<%# Bind("EntryTypeID") %>'>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSourceEntry2TandE23" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                        SelectCommand="SELECT ID as EntryTypeID,ExpensesentryType FROM [ExpensesentryType]">
                                    </asp:SqlDataSource>
                                         
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEntryExternalExpenses" runat="server" Text='<%# Eval("EntryType") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlEntry_footerExternalExpenses" runat="server" SkinID="ddl_80"
                                        DataSourceID="SqlDataSourceEntry2_Footer" DataTextField="ExpensesentryType" DataValueField="EntryTypeID">
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="btn_Footer_ExternalExpenses" runat="server" SkinID="BtnLinkAdd"
                                        Enabled="<%#CommandField()%>" CommandName="Admin" />
                                    <asp:SqlDataSource ID="SqlDataSourceEntry2_Footer" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                        SelectCommand="DEFFINITY_ExpensesType" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    <asp:RequiredFieldValidator ID="RequiAS5" runat="server" ControlToValidate="ddlEntry_footerExternalExpenses"
                                        SetFocusOnError="true" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectentrytype%>"
                                        InitialValue="0" Display="None" ValidationGroup="expenseInsert"></asp:RequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Qty%>">
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right"/>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("Qty") %>' SkinID="Price"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQty"
                                        ErrorMessage="Please enter qty" Display="None" ValidationGroup="expenseUpdate"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidatorQty" runat="server" ControlToValidate="txtQty"
                                        Display="None" ErrorMessage="Please enter valid qty" Operator="DataTypeCheck"
                                        Type="Double" ValidationGroup="expenseUpdate" SetFocusOnError="true"></asp:CompareValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty","{0:N2}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFooterQty" runat="server" Text='<%# Bind("Qty") %>'
                                        SkinID="Price"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtFooterQty"
                                        ErrorMessage="Please enter qty" Display="None" ValidationGroup="expenseInsert"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompSD" runat="server" ControlToValidate="txtFooterQty"
                                        Display="None" ErrorMessage="Please enter valid qty" Operator="DataTypeCheck"
                                        Type="Double" ValidationGroup="expenseInsert" SetFocusOnError="true"></asp:CompareValidator>
                                </FooterTemplate>
                                <FooterStyle Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,UnitCost%>">
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUnitCost" runat="server" Text='<%# Bind("ForecastValue","{0:N2}") %>'
                                        SkinID="Price"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUnitCost" runat="server" ControlToValidate="txtUnitCost"
                                        ErrorMessage="Please enter unit cost" Display="None" SetFocusOnError="true" ValidationGroup="expenseInsert"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtUnitCost"
                                        SetFocusOnError="true" Display="None" ErrorMessage="Please enter valid unit cost"
                                        Operator="DataTypeCheck" Type="Double" ValidationGroup="expenseUpdate"></asp:CompareValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitCost" runat="server" Text='<%# Bind("ForecastValue","{0:N2}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFooterUnitCost" runat="server" Text='<%# Bind("ForecastValue") %>'
                                       SkinID="Price"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUnitcostFooter" runat="server"
                                        ControlToValidate="txtFooterUnitCost" ErrorMessage="Please enter unit cost" Display="None"
                                        SetFocusOnError="true" ValidationGroup="expenseInsert"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompSD1" runat="server" ControlToValidate="txtFooterUnitCost"
                                        Display="None" ErrorMessage="Please enter valid unit cost" Operator="DataTypeCheck"
                                        SetFocusOnError="true" Type="Double" ValidationGroup="expenseInsert"></asp:CompareValidator>
                                </FooterTemplate>
                              
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Total%>">
                              
                                <ItemStyle HorizontalAlign="Right"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total","{0:N2}") %>'></asp:Label>
                                </ItemTemplate>
                              
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,AssignedTo%>">
                              
                              
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedTo" runat="server" Text='<%# Eval("AssignedToName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlAssignTo" runat="server"></asp:DropDownList>
                                    <asp:HiddenField ID="hfAssignedTo" runat="server" Value='<%# Eval("AssignedTo") %>' />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlFooterAssignedTo" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvAssigendTo" runat="server" ControlToValidate="ddlFooterAssignedTo"
                                        ErrorMessage="Please select assigned to" InitialValue="0" Display="None" ValidationGroup="expenseInsert"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </FooterTemplate>
                            
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Expensed%>" Visible="false">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkExpensed" runat="server" Checked='<%# Eval("Expensed").ToString() == "False" ? false:true %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblExpensed" runat="server" Text='<%# Eval("Expensed").ToString() == "False" ? "No":"Yes" %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBox ID="chkFooterExpensed" runat="server" />
                                </FooterTemplate>
                              
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="imgDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                        Enabled="<%#CommandField()%>" SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>'
                                        OnClientClick="return confirm('Do you want to delete the record?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
</div>

 <ajaxToolkit:ModalPopupExtender ID="mdlpopupinGridToSave" runat="server" BackgroundCssClass="modalBackground"
                                                 TargetControlID="l11" PopupControlID="PnlPopUpInGrid" CancelControlID="ImageButton5"></ajaxToolkit:ModalPopupExtender>
   <asp:Panel ID="PnlPopUpInGrid" runat="server" BackColor="White" Style="display:none" Width="450px" Height="270px"
                  BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="Auto">
             <div style="float: right">
               <asp:LinkButton ID="ImageButton5" runat="server" SkinID="BtnLinkCancel" ToolTip="Close" />
             </div>
                <div class="sec_header"><asp:Label ID="Label6" Font-Bold="true" Font-Size="Large" runat="server" Text="Saving"></asp:Label></div>
             <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePnlPopUpInGrid">
                <ProgressTemplate>
              <%--      <asp:Image ID="imgloading_5" runat="server" ImageUrl="~/media/ico_loading.gif" />--%>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePnlPopUpInGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="form-group">
                                  <div class="col-md-11">
                                        <asp:Label ID="lblProjectExpansesId" runat="server"></asp:Label>
                                        <asp:ValidationSummary ID="ValidationSummaryinPopUP" runat="server" ValidationGroup="Goods" ForeColor="Red" />
                                  </div>
                    </div>
                    <div class="form-group">
                                  <div class="col-md-11">
                                       <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.QTY%></label>
                                   <div class="col-sm-6">
                                       <asp:Label ID="lblQtyReceivedtoDate" Font-Bold="true" runat="server"></asp:Label>
                                   </div>
                                  </div>
                    </div>
                    <div class="form-group">
                                  <div class="col-md-11">
                                       <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.UnitCost%></label>
                                   <div class="col-sm-6">
                                         <asp:TextBox ID="txtbudgetQty" runat="server" SkinID="Price_75px" Enabled="false"></asp:TextBox>
                                   </div>
                                  </div>
                    </div>
                    <div class="form-group">
                                  <div class="col-md-11">
                                       <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.UpdatedUnitCost%></label>
                                   <div class="col-sm-6">
                                        <asp:TextBox ID="txtActualReq" runat="server" SkinID="Price_75px"></asp:TextBox>
                                         <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789."
                                                                 TargetControlID="txtActualReq"></ajaxToolkit:FilteredTextBoxExtender>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter Required Qty"
                                               ControlToValidate="txtActualReq" Display="None" ValidationGroup="Goods" Width="225px">*</asp:RequiredFieldValidator>
                                   </div>
                                  </div>
                    </div>
                    <div class="form-group">
                                  <div class="col-md-11">
                                      <asp:Button ID="BtnSave" runat="server" SkinID="btnSave" Text="Commit Saving to Project" ValidationGroup="Goods" OnClick="BtnSave_Click" />
                                  </div>
                    </div>
                </ContentTemplate>
                </asp:UpdatePanel>
                  </asp:Panel>




   