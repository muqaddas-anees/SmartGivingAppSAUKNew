<%@ Page Language="C#"  MasterPageFile="~/WF/MainTab.master"  AutoEventWireup="true" Inherits="VTAdmin" Codebehind="VTAdmin.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.VTAdmin%> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<asp:Panel ID="pnlAnnualleave" runat="server">
     <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" EnableViewState="false" Style="color: red" />
                                        </div>
   <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   <%=Resources.DeffinityRes.AbsenceType %> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
                                   
 <div class="form-group">
     <div class="col-md-12">
      <asp:ValidationSummary ID="valsumadd" runat="server" ForeColor="Red" ValidationGroup="grpadd" />
                                                            <asp:Label ID="lblmsg1" runat='server' ForeColor="Red" EnableViewState="False"></asp:Label>
         </div>
     </div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Absence%></label>
                                      <div class="col-sm-4 form-inline">
                                           <asp:TextBox ID="txtabsensetype" runat="server" ></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rqdType" runat="server" ControlToValidate="txtabsensetype"
                                                                Display="Dynamic" ValidationGroup="grpadd" ErrorMessage="Please enter the Absence type" Text="*" />
                                                           
                                                            <asp:HiddenField ID="HID" runat="server" />
					</div>
                                        <div class="col-sm-4 form-inline">
                                             <asp:DropDownList ID="ddlColors" runat="server" CssClass="txt_field"
                                                                SkinID="ddl_90">
                                                                <asp:ListItem Text="Please Select" Value="0" />
                                                                <asp:ListItem style="background-color: #E55451; color: white" Text="" Value="Red" />
                                                                <asp:ListItem style="background-color: #4CC417; color: white" Text="" Value="Green" />
                                                                <asp:ListItem style="background-color: #FFF380" Text="" Value="Yellow" />
                                                                <asp:ListItem style="background-color: #FE9A2E" Text="" Value="Orange" />
                                                                <asp:ListItem Text="" Value="Aqua" style="background-color: #B6FFFF" />
                                                                <asp:ListItem Text="" Value="Navy" style="background-color: Navy;" />
                                                                <asp:ListItem Text="" Value="Maroon" style="background-color: #E3319D;" />
                                                                <asp:ListItem Text="" Value="Silver" style="background-color: Silver;" />
                                                                <asp:ListItem Text="" Value="Black" style="background-color: Black;" />
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="Server" 
                                                                ControlToValidate="ddlColors" Display="Dynamic" 
                                                                ErrorMessage="Please enter the color for the shift" InitialValue="0" Text="*" 
                                                                ValidationGroup="grpadd">
                                                            </asp:RequiredFieldValidator>
                                            </div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8">
                                           <asp:Button ID="btnsubmit" runat="server" SkinID="btnSubmit" OnClick="btnsubmit_Click"
                                                                ValidationGroup="grpadd" />
                                                            <asp:Button ID="ImageButton2" runat="server" SkinID="btnCancel" 
                                                                onclick="ImageButton2_Click" />
					</div>
				</div>
</div>

                                    <asp:GridView ID="GrdAbsenseTypes" runat="server" AutoGenerateColumns="False" 
                                                                DataKeyNames="ID" DataSourceID="objAbsenseType" 
                                                                EmptyDataText="No records found." onrowdeleted="GrdAbsenseTypes_RowDeleted" 
                                                                onselectedindexchanged="GrdAbsenseTypes_SelectedIndexChanged" 
                                                                onselectedindexchanging="GrdAbsenseTypes_SelectedIndexChanging" 
                                                                Width="100%" onrowdatabound="GrdAbsenseTypes_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ID" Visible="false" />
                                                                    <asp:TemplateField ShowHeader="False">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                                                CommandArgument='<%# Bind("ID")%>' CommandName="Select" 
                                                                                SkinID="BtnLinkEdit" Text="Select" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Absence" SortExpression="Year">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAbsense" runat="server" Text='<%# Bind("TYPE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ControlStyle-Width="100px" ControlStyle-Height="20px" ItemStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblColor" runat="server" Text='<%# Bind("Color")%>' />
                                                                             <asp:Label ID="lblColorEdit" runat="server" Text='<%# Bind("Color")%>' Visible="false" />
                                                                        </ItemTemplate>
                                                                        <ControlStyle Height="20px" Width="100px" />
                                                                        <ItemStyle Width="100px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                    <HeaderStyle/>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" SkinID="BtnLinkDelete" Visible='<%#DeleteButtonVisibility(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/icon_delete.png" 
                                                                        HeaderStyle-CssClass="header_bg_l" ShowDeleteButton="True" Visible="false">
                                                                    </asp:CommandField>
                                                                </Columns>
                                                            </asp:GridView>


                                    </div>
                                  <div class="col-md-6">

                                      
<div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   <%=Resources.DeffinityRes.WorkingHours %> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
                                      <div class="form-group">
                                           <asp:ValidationSummary ID="valsumadd1" runat="server" ForeColor="Red" ValidationGroup="grpadd1" />
                                                            <asp:Label ID="lblMessage" runat="server" EnableViewState="false" Style="color: red" />
                                          </div>

                                      <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label">Working&nbsp;Day&nbsp;Starts&nbsp;from</label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txtdaystart" runat="server" Text="00:00" SkinID="Time" ></asp:TextBox><span style="color:Gray">E.g.08:30</span>
                                                    <asp:RequiredFieldValidator ID="reqfdhrs" runat="server" ControlToValidate="txtdaystart"
                                                    ErrorMessage="Please enter start hours" Text="*" ValidationGroup="grpadd1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="regxfdhrs" runat='server' ControlToValidate="txtdaystart"  ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$"
                                                    ErrorMessage="Please enter valid hours" Text="*" ValidationGroup="grpadd1"></asp:RegularExpressionValidator>
					</div>
				</div>
</div>
                                      <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label">Working&nbsp;Day&nbsp;Ends</label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txtdayend" runat="server" Text="00:00" SkinID="Time"></asp:TextBox><span style="color:Gray">E.g.17:00</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdayend"
                                                    ErrorMessage="Please enter end hours" Text="*" ValidationGroup="grpadd1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat='server' ControlToValidate="txtdayend"  ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$"
                                                    ErrorMessage="Please enter valid hours" Text="*" ValidationGroup="grpadd1"></asp:RegularExpressionValidator>
					</div>
				</div>
</div>
                                      <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> Half&nbsp;Day&nbsp;Point</label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txthdp" runat="server" Text="00:00" SkinID="Time"></asp:TextBox><span style="color:Gray">E.g.13:00</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txthdp"
                                                    ErrorMessage="Please enter Half day point" Text="*" ValidationGroup="grpadd1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat='server' ControlToValidate="txthdp"  ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$"
                                                    ErrorMessage="Please enter valid hours" Text="*" ValidationGroup="grpadd1"></asp:RegularExpressionValidator>
					</div>
				</div>
</div>
                                      <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8">
                                           <asp:Button ID="btnsubmit1" runat="server" OnClick="btnsubmit1_Click" SkinID="btnSubmit"
                                                                ValidationGroup="grpadd1" />
                                                            <asp:Button ID="ImageButton3" runat="server" SkinID="btnCancel" 
                                                                onclick="ImageButton3_Click" />
					</div>
				</div>
</div>

                                    </div>
                                 </div>

                       
                        <asp:ObjectDataSource ID="objAbsenseType" runat="server" SelectMethod="SelectAbsenseTypes"
                            TypeName="VT.AdminEntry" OldValuesParameterFormatString="original_{0}">
                           
                        </asp:ObjectDataSource>
                    </asp:Panel>
    
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