<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="AdminUsersRates" Title="Untitled Page" Codebehind="AdminUsersRates.aspx.cs" %>

<%@ Register src="controls/MangeUserTab.ascx" tagname="MangeUserTab" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">


</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Manage&nbsp;Users&nbsp; - Rates
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:LinkButton ID="btngohome" runat="server" SkinID="BtnLinkButton" Text="Return to User Admin"
                                        OnClick="btngohome_Click" CausesValidation="false"><i class="fa fa-arrow-left"></i> Return to User Admin</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
             <div class="col-md-12 form-inline">
                 User Admin for : <asp:Label ID="lblusername" runat='server' Font-Bold="true"></asp:Label>
</div>
</div>
    <uc1:MangeUserTab ID="MangeUserTab1" runat="server" />
    <div class="form-group">
             <div class="col-md-12">
                 <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group2" />
                                    <asp:Label ID="lbluserrate" runat="server" SkinID="RedBackcolor" EnableViewState="false" Text="Label" Visible="False"></asp:Label>
                 <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
</div>
</div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowCommand="GridView1_RowCommand"
                                        OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                                        EnableViewState="False">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="10px" />
                                                <ItemStyle Width="6%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                        CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                        CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate" ValidationGroup="Group2"
                                                        ToolTip="Update"></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                        SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry Type">
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="15%" />
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlEntry" Width="125px" runat="server" DataSourceID="SqlDataSourceEntry2"
                                                        DataTextField="EntryType" DataValueField="EntryTypeID" SelectedValue='<%# Bind("EntryTypeID") %>'>
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSourceEntry2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                        SelectCommand="SELECT ID as EntryTypeID,EntryType FROM [TimesheetEntryType]"></asp:SqlDataSource>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlEntry"
                                                        ErrorMessage="Please select Entry Type" InitialValue="Please select..." Display="None"
                                                        ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEntry" runat="server" Text='<%# Bind("EntryType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Buying Daily Rate">
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtHoursBuying" runat="server" Text='<%# Bind("Hourlyrate_Buying") %>'
                                                        Width="70px" SkinID="Price"></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareHoursBuying" runat="server" ControlToValidate="txtHoursBuying"
                                                        Display="None" ErrorMessage="Please enter valid data in hourly buying rate" Operator="DataTypeCheck"
                                                        Type="Double" ValidationGroup="Group2"></asp:CompareValidator>
                                                    &nbsp;
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHourlyBuying" runat="server" Text='<%# Bind("Hourlyrate_Buying","{0:N2}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Daily Selling Rate" Visible="false">
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtHoursselling" runat="server" Text='<%# Bind("Hourlyrate_Selling") %>'
                                                        Width="70px" SkinID="Price"></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareHoursselling" runat="server" ControlToValidate="txtHoursselling"
                                                        Display="None" ErrorMessage="Please enter valid data in  houly selling rate"
                                                        Operator="DataTypeCheck" Type="Double"  ValidationGroup="Group2"></asp:CompareValidator>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHourlyselling" runat="server" SkinID="Price" Text='<%# Bind("Hourlyrate_Selling","{0:N2}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Minimum daily&nbsp;hours" Visible="false">
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMinimumdailyhours" runat="server" Text='<%# ChangeHoues(Eval("Minimumdailyhours").ToString())%>'
                                                        Width="75px" SkinID="Price" MaxLength="5"></asp:TextBox>
                                                   
                                                    <asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txtMinimumdailyhours"
                                                        ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group2"
                                                        Display="None" Text="*" ErrorMessage="Please enter valid time and miniues "></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="GETR111" runat="server" ControlToValidate="txtMinimumdailyhours"
                                                        ErrorMessage="Please enter minimum hours that is grate than 0" Display="None"
                                                        ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMinimumdailyhours" runat="server" Text='<%# ChangeHoues(Eval("Minimumdailyhours").ToString())%>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                                        SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
    <div class="form-group">
             <div class="col-md-12">
                  <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="valSubmit2" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txthourly_Buying"
                                        Display="None" ErrorMessage="Please enter valid data in hourly buying rate" Operator="DataTypeCheck"
                                        Type="Double" ValidationGroup="valSubmit2"></asp:CompareValidator>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txthourly_Selling"
                                        Display="None" ErrorMessage="Please enter valid data in  houly selling rate"
                                        Operator="DataTypeCheck" Type="Double" ValidationGroup="valSubmit2"></asp:CompareValidator>
                                    
                                    <asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txt_minimundailyhours"
                                        ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="valSubmit2"
                                        Text="*" ErrorMessage="Please enter valid time and miniues "></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlentry"
                                        ErrorMessage="Please select Entry Type" InitialValue="Please select..." Display="None"
                                        ValidationGroup="valSubmit2"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="GETR11" runat="server" ControlToValidate="txt_minimundailyhours"
                                        ErrorMessage="Please enter minimum hours" Display="None" ValidationGroup="valSubmit2"></asp:RequiredFieldValidator>
                                    
                                    <asp:ValidationSummary ID="ValidationSummary7" runat="server" ValidationGroup="TEST3" />
</div>
</div>

    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.User%></label>
                                      <div class="col-sm-7"><asp:Label ID="lblUserratename" runat="server" Text="" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.RateType%></label>
                                      <div class="col-sm-7 form-inline">
                                           <asp:DropDownList ID="ddlentry" runat="server" SkinID="ddl_70">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtExpensetype" runat="server" Visible="False" SkinID="txt_70"></asp:TextBox>
                                    <asp:LinkButton ID="btnadd_entry" runat="server" CausesValidation="False" OnClick="btnadd_entry_Click"
                                        SkinID="BtnLinkAdd" />
                                    <asp:Button ID="btnadd1" runat="server" CausesValidation="False" OnClick="btnadd1_Click"
                                        SkinID="btnAdd" ValidationGroup="TEST3" Visible="false" />
                                    <asp:LinkButton ID="btncancel1" runat="server" CausesValidation="False" OnClick="btncancel1_Click"
                                        SkinID="BtnLinkCancel" Visible="false" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtExpensetype"
                                        Display="None" ErrorMessage="Please enter rate type" ValidationGroup="TEST3"></asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.DailyBuyingRate%></label>
                                      <div class="col-sm-7">
                                          <asp:TextBox ID="txthourly_Buying" runat="server" SkinID="Price_100px"></asp:TextBox>
					</div>
				</div>
                </div>
    <div class="form-group"  style="display:none;visibility:hidden;">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.DailySellingRate%></label>
                                      <div class="col-sm-7">
                                           <asp:TextBox ID="txthourly_Selling" runat="server" SkinID="Price_100px"></asp:TextBox>
					</div>
				</div>
                </div>
    <div class="form-group" style="display:none;visibility:hidden;">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Minimumdailyhours%></label>
                                      <div class="col-sm-7 form-inline">
                                           <asp:TextBox ID="txt_minimundailyhours" runat="server" MaxLength="5"
                                        SkinID="Time" Text="10:00"></asp:TextBox><span style="color: gray">HH:MM</span>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> </label>
                                      <div class="col-sm-7"><asp:Button ID="img_RateSubmit" runat="server" SkinID="btnSubmit" 
                                        OnClick="img_RateSubmit_Click" ValidationGroup="valSubmit2" />
					</div>
				</div>
                </div>
    
    <asp:HiddenField ID="getUserId" runat="server" />
     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();
        $(window).load(function () {
                     $("button:contains('Display all')").click(function (e) {
                e.preventDefault();
                $(".dropdown-menu li")
          .find("input[type='checkbox']")
          .prop('checked', 'checked').trigger('change');
            });
                 });
    </script> 
</asp:Content>

