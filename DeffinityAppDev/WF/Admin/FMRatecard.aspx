<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="FMRatecard" Title="Untitled Page" Codebehind="FMRatecard.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
   <%= Resources.DeffinityRes.Admin%> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     Rate Card 
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="form-group">
        <asp:ValidationSummary ID="valSummary" runat="server" ValidationGroup="vldgrpClass"
                    EnableViewState="false" />
                <asp:Label ID="lblErr" runat="server" Visible="false" EnableViewState="false"></asp:Label>
        </div>
    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong> By Classification</strong>
            <hr class="no-top-margin" />
            </div>
</div>
    <div class="form-group">
             <div class="col-md-6">
                                       <label class="col-sm-3 control-label"> Classification</label>
                                      <div class="col-sm-9 form-inline"> <asp:Panel ID="pnlClassfi" runat="server">
                            <asp:DropDownList ID="ddlClass" runat="server" SkinID="ddl_90">
                            </asp:DropDownList>
                            <asp:LinkButton ID="btnaddclassifi" runat="server" SkinID="BtnLinkAdd" CausesValidation="False"
                                OnClick="btnaddclassifi_Click"></asp:LinkButton>
                        </asp:Panel>
                        <asp:Panel ID="pnladdclassfi" runat="server" Visible="false">
                            <asp:TextBox ID="txtAddClassi" runat="server" SkinID="Price_150px" ValidationGroup="cat1"></asp:TextBox>
                            <asp:LinkButton ID="btnSaveClassi" runat="server" ToolTip="Add Category" ValidationGroup="cat1"
                                 SkinID="BtnLinkUpdate" onclick="btnSaveClassi_Click"></asp:LinkButton>
                            <asp:LinkButton ID="btnCancelClassi" runat="server" ToolTip="Cancel" 
                                CausesValidation="False" SkinID="BtnLinkCancel" 
                                onclick="btnCancelClassi_Click"></asp:LinkButton>
                            <div>
                                <asp:RequiredFieldValidator runat="server" ID="ReqCatname" ControlToValidate="txtAddClassi"
                                    SetFocusOnError="true" ErrorMessage="Please enter Classification" ForeColor="Red" ValidationGroup="cat1"></asp:RequiredFieldValidator></div>
                        </asp:Panel>
					</div>
				</div>
        <div class="col-md-6">
                                       <label class="col-sm-3 control-label">Rate Type</label>
                                      <div class="col-sm-9"> <asp:DropDownList ID="ddlRateType" runat="server" SkinID="ddl_90">
                        </asp:DropDownList>
					</div>
				</div>
                </div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-3 control-label">Daily Rate</label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtDailyR" runat="server" SkinID="Price_125px"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtDailyR" Operator="DataTypeCheck"
                            Display="None" Type="Double" ErrorMessage="Please enter valid Daily Rate" ValidationGroup="vldgrpClass"></asp:CompareValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-3 control-label">Hourly Rate</label>
                                      <div class="col-sm-9"> <asp:TextBox ID="txtHourlyR" runat="server" SkinID="Price_125px"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtHourlyR" Operator="DataTypeCheck"
                            Display="None" Type="Double" ErrorMessage="Please enter valid Hourly Rate" ValidationGroup="vldgrpClass"></asp:CompareValidator>
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8">
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8 pull-right"> <asp:Button ID="imgbtnSave" runat="server" SkinID="btnSave" OnClick="imgbtnSave_Click"
                            ValidationGroup="vldgrpClass" ToolTip="Save Rate Card" />
					</div>
				</div>
</div>
   
    <div class="form-group">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vldgrpGrdClass"
                    EnableViewState="false" />
        </div>
    <asp:GridView ID="grdClassfication" runat="server" AutoGenerateColumns="False" EmptyDataText="No Rates cards available"
                    OnRowCancelingEdit="grdClassfication_RowCancelingEdit" OnRowCommand="grdClassfication_RowCommand"
                    OnRowDataBound="grdClassfication_RowDataBound" OnRowEditing="grdClassfication_RowEditing"
                    OnPageIndexChanging="grdClassfication_PageIndexChanging" OnRowUpdating="grdClassfication_RowUpdating"
                    OnRowDeleting="grdClassfication_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                            <HeaderStyle Width="40px" />
                            <ItemStyle Width="40px" />
                            <ItemTemplate>
                                <div style="width: 45px">
                                    <div style="width: 20px; float: left">
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible="false"> </asp:Label>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit"  ToolTip="Edit">
                                        </asp:LinkButton></div>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div style="width: 45px">
                                    <div style="width: 20px; float: left">
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                            CommandArgument='<%# Bind("ID")%>' ValidationGroup="vldgrpGrdClass" SkinID="BtnLinkUpdate"
                                            ToolTip="Update"></asp:LinkButton>
                                    </div>
                                    <div style="width: 20px; float: left">
                                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton></div>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Classfication">
                            <ItemTemplate>
                                <asp:Label ID="lblClassification" runat="server" Text='<%# Bind("ExpClassification") %>'
                                    Width="140px"></asp:Label>
                            </ItemTemplate>
                            <%-- <EditItemTemplate>
                                <asp:DropDownList ID="ddlEClassification" runat="server" AppendDataBoundItems="true"
                                     Width="140px">
                                    
                                </asp:DropDownList>
                               
                            </EditItemTemplate>--%>
                            <ItemStyle Width="140px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate Type">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="140px" />
                            <ItemTemplate>
                                <asp:Label ID="lblRateType" runat="server" Text='<%# Bind("EntryType") %>'></asp:Label>
                            </ItemTemplate>
                            <%-- <EditItemTemplate>
                                <asp:DropDownList ID="ddlERateType" runat="server" AppendDataBoundItems="true"
                                     Width="140px">
                                    
                                </asp:DropDownList>
                               
                            </EditItemTemplate>--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Daily Rate">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDailyRate" runat="server" Text='<%# Eval("DailyRate","{0:N2}") %>' Style="text-align: right"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="75px" HorizontalAlign="Right" />
                            <EditItemTemplate>
                                <asp:TextBox ID="txtECDRate" runat="server" Style="text-align: right" Width="75px"
                                    Text='<%# Eval("DailyRate") %>'></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtECDRate" Operator="DataTypeCheck"
                            Display="None" Type="Double" ErrorMessage="Please enter valid Daily Rate" ValidationGroup="vldgrpGrdClass"></asp:CompareValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hourly Rate">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="75px" HorizontalAlign="Right" />
                            <EditItemTemplate>
                                <asp:TextBox ID="txtECHRate" runat="server" Width="75px" Style="text-align: right"
                                    Text='<%# Eval("HourlyRate")%>'></asp:TextBox>
                            
                              <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtECHRate" Operator="DataTypeCheck"
                            Display="None" Type="Double" ErrorMessage="Please enter valid Hourly Rate" ValidationGroup="vldgrpGrdClass"></asp:CompareValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblECHRate" runat="server" Style="text-align: right" Text='<%# Eval("HourlyRate","{0:N2}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                    SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
   
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

