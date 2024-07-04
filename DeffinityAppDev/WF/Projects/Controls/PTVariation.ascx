<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_PTVariation" Codebehind="PTVariation.ascx.cs" %>
<style>
    .round
    {
        border: 1px solid Silver;
        padding: 5px 5px;
        background: #d1e7ed;
        width: 40%;
        border-radius: 8px;
    }
</style>
<div class="row">
<div class="col-md-12">
    &nbsp;
</div>
</div>
<asp:Panel Width="100%" runat="server" ID="Panelvariation">
    <div class="well" style="width:35%">
        <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-6 control-label"> Total Variations:</label>
                                      <div class="col-sm-3 pull-right control-label">
                                          <asp:Label ID="lblTotalVariations" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
        <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-6 control-label"> Complete To Date:</label>
                                      <div class="col-sm-3 pull-right control-label"> <asp:Label ID="lblCompleteToDate" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
       
    </div>
    <div>
        <asp:Panel Width="100%" runat="server" ID="Panel1">
            <div style="width: 100%; text-align: right;">
                <asp:LinkButton runat="server" ID="btnApprove" Text="<%$ Resources:DeffinityRes,Approve%>"
                    Font-Bold="True" OnClick="btnApprove_Click"></asp:LinkButton></div>
            <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" DataSourceID="SqlDataSource1"
                Width="100%" EmptyDataText="<%$ Resources:DeffinityRes,Novariationslogged %>"
                OnRowCommand="GridView1_RowCommand" OnRowUpdating="GridView1_RowUpdating" OnRowUpdated="GridView1_RowUpdated"
                OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField ShowHeader="False" Visible="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Update" SkinID="BtnLinkUpdate" Text="Update" />
                            &nbsp;<asp:LinkButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Cancel" SkinID="BtnLinkCancel" Text="Cancel" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Edit" SkinID="BtnLinkEdit" Text="Edit" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                    </asp:TemplateField>
                    <asp:BoundField Visible="False" DataField="ID" />
                    <asp:BoundField Visible="False" DataField="ProjectReference" />
                    <asp:HyperLinkField HeaderStyle-CssClass="header_bg_l"
                        DataNavigateUrlFields="ProjectReference,ID" DataNavigateUrlFormatString="~/WF/Projects/ProjectDeviationReport.aspx?Project={0}&amp;ID={1}"
                        Text="Edit">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:HyperLinkField>
                    <asp:TemplateField HeaderText="Customer Instruction Number">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerInstructionNumber" runat="server" Text='<%# Bind("CustomerInstructionNumber") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="175px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Description%>">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Sales Price">
                        <ItemTemplate>
                            <asp:Label ID="lblvariationFC" runat="server" Text='<%# Bind("DeviationValue", "{0:C}" ) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <EditItemTemplate>
                            <asp:TextBox ID="txtvariationFC" runat="server" Width="80px" Text='<%# Bind("DeviationValue","{0:#.00}" ) %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Variation Cost">
                        <ItemTemplate>
                            <asp:Label ID="lblvariationVC" runat="server" Text='<%# Bind("IndirectCost", "{0:C}" ) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Percentage <br/> Complete (%)">
                        <ItemTemplate>
                            <asp:Label ID="lblPercentageComplete" runat="server" Text='<%# Bind("PercentageComplete") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Sales Complete">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalSalesComplete" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                  <%--  <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="hlBreakdownHours" runat="server" Text="Breakdown of Additional Hours"
                                CommandName="BreakDownHours" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Approve%>" ShowHeader="False">
                        <ItemTemplate>
                            <asp:CheckBox ID="chckApprove" runat="server" Checked='<%# Bind("Approved") %>' />
                            <asp:HiddenField ID="HID" runat="server" Value='<%# Bind("ID") %>' />
                            <asp:HiddenField ID="hfApprove" runat="server" Value='<%# Bind("Approved") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ConnectionString="<%$ ConnectionStrings:DBstring %>" ProviderName="<%$ ConnectionStrings:DBstring.ProviderName %>"
                ID="SqlDataSource1" runat="server" SelectCommand="DN_DeviationSelect" SelectCommandType="StoredProcedure"
                UpdateCommand="DN_Deviationreportupdate1" UpdateCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="ProjectReference" QueryStringField="Project"
                        Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                    <asp:Parameter Name="ProjectReference" Type="Int32" />
                    <asp:Parameter Name="StartDate" Type="String" />
                    <asp:Parameter Name="EndDate" Type="string" />
                    <asp:Parameter Name="VariationForcast" Type="Double" />
                    <asp:Parameter Name="VariationCostForcast" Type="Double" />
                    <asp:Parameter Name="DeviationValue" Type="Double" />
                    <asp:Parameter Name="IndirectCost" Type="Double" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </asp:Panel>
    </div>
    <div class="clr">
    </div>
    <asp:Panel runat="server" ID="BtnPanel1" Width="100%" HorizontalAlign="Right">
        <asp:Button ID="ImageButton7" runat="server" ToolTip="<%$ Resources:DeffinityRes,Raisevariation%>"
            OnClick="ImageButton7_Click" ValidationGroup="ProjectValues1" SkinID="btnDefault" Text="<%$ Resources:DeffinityRes,Raisevariation%>" /></asp:Panel>
</asp:Panel>
<%--<ajaxToolkit:ModalPopupExtender ID="mdlBreakdownHours" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="imgPopupBreakdownHours" PopupControlID="pnlBreakdownHours" CancelControlID="imghistoryCancel">
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="imgPopupBreakdownHours" runat="server" />
<asp:Panel ID="pnlBreakdownHours" runat="server" BackColor="White" Style="display: none;"
    Width="600px" Height="400px" BorderStyle="Double" BorderColor="LightSteelBlue"
    ScrollBars="Auto">
    <div style="float: right">
        <asp:ImageButton ID="imghistoryCancel" runat="server" SkinID="ImgSymCancel" ToolTip="Close" /></div>
    <div class="sec_header">
        Breakdown of Additional Hours</div>
    <br />
    <asp:UpdateProgress ID="uprogress1" runat="server">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" ImageUrl="~/media/ico_loading.gif" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ValidationSummary ID="Val1" runat="server" DisplayMode="BulletList" ValidationGroup="b" />
    <asp:HiddenField ID="hfVariationID" runat="server" Value="0" />
    <table>
        <tr>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" Width="180">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlUser"
                    InitialValue="0" ErrorMessage="Please select user" ValidationGroup="b">*</asp:RequiredFieldValidator>
            </td>
            <td>
                Additional Hours
            </td>
            <td>
                <asp:TextBox ID="txtAdditionalHours" runat="server" Width="50px" SkinID="Price" Text="0:00"></asp:TextBox><span
                    style="color: Gray">(HH:MM)</span>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtAdditionalHours"
                    ErrorMessage="Please enter additional hours " ValidationGroup="b">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regex122" runat="server" ControlToValidate="txtAdditionalHours"
                    ValidationExpression="^((\d+):([0-5][0-9]))$" ValidationGroup="b" SetFocusOnError="True"
                    Display="None" Text="*" ErrorMessage="Please enter valid time and miniues "></asp:RegularExpressionValidator>
            </td>
            <td>
                <asp:ImageButton ID="imgAddHours" runat="server" SkinID="imgAdd" ImageAlign="AbsMiddle"
                    OnClick="imgAddHours_Click" ValidationGroup="b" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlGrid" runat="server">
    </asp:Panel>
    <asp:GridView ID="gvBreakdownHours" runat="server" Width="80%" AutoGenerateColumns="False"
        OnRowCommand="gvBreakdownHours_RowCommand">
        <Columns>
            <asp:BoundField DataField="ContractorName" HeaderText="Name" HeaderStyle-CssClass="header_bg_l" />
            <asp:TemplateField HeaderText="Additional Hours">
                <ItemTemplate>
                    <asp:Label ID="lblChange" runat="server" Text='<%# ChangeHours(Eval("AdditionalHours").ToString())%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" Width="60px" />
            </asp:TemplateField>
            <asp:BoundField DataField="WCDate" HeaderText="WC Date" DataFormatString="{0:d}" />
            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
            <ItemStyle Width="20px" />
                <ItemTemplate>
                    <asp:ImageButton ID="LinkButtonDelete1" runat="server" OnClientClick="return confirm('Do you want to delete this record?');"
                        CommandName="Delete1" CommandArgument="<%# Bind('ID')%>" SkinID="ImgSymDel" ToolTip="Delete">
                    </asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Panel>--%>
 