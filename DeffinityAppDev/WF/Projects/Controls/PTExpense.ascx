<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_PTExpense" Codebehind="PTExpense.ascx.cs" %>
<style>
    /*.round
    {
        border: 1px solid Silver;
        padding: 5px 5px;
        background: #d1e7ed;
        width: 40%;
        border-radius: 8px;
    }*/
</style>
<div class="row">
     <div class="col-md-12">
         &nbsp;
         </div>
    </div>

<div class="row">
     <div class="col-md-4 well">
   <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label">Forecast Expense:</label>
                                      <div class="col-sm-3 pull-right control-label"><asp:Label ID="lblForecastExpense" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> Spent To Date:</label>
                                      <div class="col-sm-3 pull-right control-label"> <asp:Label ID="lblSpentToDate" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> Remaining:</label>
                                      <div class="col-sm-3 pull-right control-label"><asp:Label ID="lblRemaining" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
       </div>
</div>
 
<div class="form-group">
             <div class="col-md-6">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.EntryType%></label>
                                      <div class="col-sm-9 form-inline">
                                           <asp:DropDownList ID="ddlEntryTypeFilter" SkinID="ddl_70" runat="server" DataSourceID="SqlDataSourceEntryType"
                    DataTextField="ExpensesentryType" DataValueField="EntryTypeID">
                </asp:DropDownList>
                                          <asp:Button ID="imgView" runat="server" SkinID="btnDefault" Text="View" OnClick="imgView_Click" />
                <asp:SqlDataSource ID="SqlDataSourceEntryType" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="DEFFINITY_ExpensesType" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
					</div>
				</div>
                </div>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="expenseUpdate" />
<asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="expenseInsert" />

       
                    <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="False" Width="100%"
                        EmptyDataText="No external expenses found" OnRowDataBound="gvExpense_RowDataBound" AllowPaging="true" PageSize="20"
                        OnRowCommand="gvExpense_RowCommand" OnRowCancelingEdit="gvExpense_RowCancelingEdit"
                        OnRowEditing="gvExpense_RowEditing" OnRowDeleting="gvExpense_RowDeleting" 
                        OnRowUpdating="gvExpense_RowUpdating" 
                        onpageindexchanging="gvExpense_PageIndexChanging" SkinID="gv_responsive" >
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
                                        Enabled="<%#CommandField()%>" Text="<%$ Resources:DeffinityRes,Update%>" SkinID="BtnLinkAdd"
                                        ToolTip="Insert" ValidationGroup="expenseInsert"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkCancelernalExpenses" runat="server" CommandName="Cancel"
                                        SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>">
                                    </asp:LinkButton>
                                </FooterTemplate>
                                
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Date%>">
                               
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server" Text='<%# Bind("ExternalExpensesDate","{0:d}") %>'
                                        SkinID="Date"></asp:TextBox><asp:Label ID="imgDate" runat="server" SkinID="Calender" />
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
                                    <asp:TextBox ID="txtFooterDate" runat="server" Text='<%# Bind("ExternalExpensesDate","{0:d}") %>'
                                        SkinID="Date"></asp:TextBox>
                                    <asp:Label ID="imgbtnenddateExpenses_Footer" runat="server" SkinID="Calender" />
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
                                    <asp:TextBox ID="txtDescription" Width="290px" runat="server" Text='<%# Bind("Description")%>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                                        ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdescription%>" Display="None"
                                        SetFocusOnError="true" ValidationGroup="expenseUpdate"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFooterDescription" runat="server" Width="290px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFooterDescription" runat="server" ControlToValidate="txtFooterDescription"
                                        ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdescription%>" Display="None"
                                        SetFocusOnError="true" ValidationGroup="expenseInsert"></asp:RequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EntryType%>">
                              
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEntryExternalExpenses" Width="160px" runat="server" DataSourceID="SqlDataSourceEntry2TandE23"
                                        DataTextField="ExpensesentryType" DataValueField="EntryTypeID" SelectedValue='<%# Bind("EntryTypeID") %>' SkinID="ddl_125px">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSourceEntry2TandE23" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                        SelectCommand="SELECT ID as EntryTypeID,ExpensesentryType FROM [ExpensesentryType]">
                                    </asp:SqlDataSource>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEntryExternalExpenses" runat="server" Text='<%# Eval("EntryType") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlEntry_footerExternalExpenses"  SkinID="ddl_125px" runat="server"
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
                              <FooterStyle CssClass="form-inline" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                               
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("Qty") %>'  SkinID="Price_75px"></asp:TextBox>
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
                                        SkinID="Price_75px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtFooterQty"
                                        ErrorMessage="Please enter qty" Display="None" ValidationGroup="expenseInsert"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompSD" runat="server" ControlToValidate="txtFooterQty"
                                        Display="None" ErrorMessage="Please enter valid qty" Operator="DataTypeCheck"
                                        Type="Double" ValidationGroup="expenseInsert" SetFocusOnError="true"></asp:CompareValidator>
                                </FooterTemplate>
                               
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Cost">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUnitCost" runat="server" Text='<%# Bind("UnitCost","{0:N2}") %>'
                                        SkinID="Price_75px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUnitCost" runat="server" ControlToValidate="txtUnitCost"
                                        ErrorMessage="Please enter unit cost" Display="None" SetFocusOnError="true" ValidationGroup="expenseInsert"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtUnitCost"
                                        SetFocusOnError="true" Display="None" ErrorMessage="Please enter valid unit cost"
                                        Operator="DataTypeCheck" Type="Double" ValidationGroup="expenseUpdate"></asp:CompareValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitCost" runat="server" Text='<%# Bind("UnitCost","{0:N2}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFooterUnitCost" runat="server" Text='<%# Bind("UnitCost") %>' SkinID="Price_75px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUnitcostFooter" runat="server"
                                        ControlToValidate="txtFooterUnitCost" ErrorMessage="Please enter unit cost" Display="None"
                                        SetFocusOnError="true" ValidationGroup="expenseInsert"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompSD1" runat="server" ControlToValidate="txtFooterUnitCost"
                                        Display="None" ErrorMessage="Please enter valid unit cost" Operator="DataTypeCheck"
                                        SetFocusOnError="true" Type="Double" ValidationGroup="expenseInsert"></asp:CompareValidator>
                                </FooterTemplate>
                               
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total","{0:N2}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned To">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedTo" runat="server" Text='<%# Eval("AssignedToName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlAssignTo" SkinID="ddl_150px" runat="server">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hfAssignedTo" runat="server" Value='<%# Eval("AssignedTo") %>' />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlFooterAssignedTo" SkinID="ddl_150px" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvAssigendTo" runat="server" ControlToValidate="ddlFooterAssignedTo"
                                        ErrorMessage="Please select assigned to" InitialValue="0" Display="None" ValidationGroup="expenseInsert"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expensed">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkExpensed" runat="server" Checked='<%# Eval("Expensed").ToString() == "False" ? false:true %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblExpensed" runat="server" Text='<%# Eval("Expensed").ToString() == "False" ? "No":"Yes" %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBox ID="chkFooterExpensed" runat="server" Checked="true" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="imgDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                        Enabled="<%#CommandField()%>" SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>'
                                        OnClientClick="return confirm('Do you want to delete the record?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
              


    