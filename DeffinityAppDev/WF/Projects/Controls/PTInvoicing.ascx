<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_PTInvoicing" Codebehind="PTInvoicing.ascx.cs" %>
 <asp:Panel Width="100%" runat="server" ID="PanelInvoice" >
     <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>   Invoice Summary</strong>
            <hr class="no-top-margin" />
            </div>
    </div>
                      
                        <asp:Panel runat="server" Width="100%" HorizontalAlign="Right" ID="btnPanel2">
                            <asp:Button ID="ImageButton6" runat="server" ToolTip="<%$ Resources:DeffinityRes,Raisevaluation%>"
                                OnClick="ImageButton6_Click" ValidationGroup="ProjectValues1" Text="Raise Invoice" SkinID="btnDefault" />
                        </asp:Panel>
                        <div>
 <asp:Panel Width="100%" runat="server" ID="Panel3" >
                                 <asp:GridView ID="GridView3" runat="server" Width="100%" EmptyDataText="<%$ Resources:DeffinityRes,Nodataexist%>"
                                                OnRowDataBound="GridView3_RowDataBound" OnRowEditing="GridView3_RowEditing" OnRowUpdating="GridView3_RowUpdating"
                                                OnRowCommand="GridView3_RowCommand" OnRowCancelingEdit="GridView3_RowCancelingEdit"
                                                OnRowDeleting="GridView3_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                                        <ItemStyle Width="30px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text="<%# Bind('ValuationID')%>" Visible="false"> </asp:Label>
                                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                                Enabled="<%#CommandField()%>" CommandArgument="<%# Bind('ValuationID')%>" SkinID="BtnLinkEdit"
                                                                ToolTip="Edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                                CommandArgument="<%# Bind('ValuationID')%>" SkinID="BtnLinkUpdate"
                                                                ToolTip="Update"></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                                SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="ProjectReference,ValuationID" DataNavigateUrlFormatString="~/WF/Projects/Newinvoice.aspx?Project={0}&type=2&ValuationID={1}"
                                                        DataTextField="InvoiceReference" HeaderStyle-Width="75px" HeaderText="<%$ Resources:DeffinityRes,InvoiceReference%>" />
                                                    <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,InvoiceReference%>" DataField="InvoiceReference"
                                                        Visible="False" />
                                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,DateRaised%>">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("DateRaised", "{0:d}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Value%>" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID1" runat="server" Text="<%# Bind('VATPercentage')%>" Visible="false"> </asp:Label>
                                                            <asp:Label ID="lblInvoice" runat="server" Text='<%# Bind("Value","{0:F2}")%>' Visible="true"></asp:Label>
                                                            <asp:Label ID="lblInvoice1" runat="server" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField  HeaderText="<%$ Resources:DeffinityRes,Status%>" DataField="Status">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Status%>">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text="<%# Bind('Status')%>"></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblStatusID" runat="server" Text="<%# Bind('InvoiceStatus')%>" Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="ddlInvoiceStatus" runat="server">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Notes%>" HeaderStyle-Width="220px">
                                                        <ItemStyle Width="220px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNotes" runat="server" Text="<%# Bind('Notes')%>"></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtNotes" runat="server" Text="<%# Bind('Notes')%>" Width="220px"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField ItemStyle-Width="100px"  HeaderText="<%$ Resources:DeffinityRes,Notes%>" DataField="Notes" ReadOnly="true"  />--%>
                                                    <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,RaisedBy%>" DataField="RaisedBy1"
                                                        ReadOnly="true" />
                                                    <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="ImageButton1" Enabled="<%#CommandField()%>" runat="server" CausesValidation="false"
                                                                CommandName="Delete" SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ValuationID") %>'
                                                                OnClientClick="return confirm('Do you want to delete the record?');" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                ProviderName="<%$ ConnectionStrings:DBstring.ProviderName %>" SelectCommand="select ProjectReference,ValuationID,InvoiceReference,(select ContractorName from contractors where ID = RaisedBy) as RaisedBy1,RaisedBy,DateRaised,Value,InvoiceStatus,(case isnull(InvoiceStatus,0) when 1 then 'Paid' when 2 then 'Pending' when 3 then 'Submitted' end)as Status  from ProjectValuations where ProjectReference =@ProjectReference"
                                                UpdateCommand="">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter Name="ProjectReference" QueryStringField="Project" Type="int32" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
     <div class="form-group">
          <div class="col-md-5">
                                <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.Summary%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>

    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> Revised project value:</label>
                                      <div class="col-sm-8"><asp:Label ID="lblRevisedProjectValue" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
</div>
<div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> Total amount of invoices raised:</label>
                                      <div class="col-sm-8"><asp:Label ID="lblTotalInvoice" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
</div>
  <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> Total&nbsp;amount&nbsp;of&nbsp;invoices&nbsp;paid:</label>
                                      <div class="col-sm-8"><asp:Label ID="lblPaid" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
</div>
 <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> Unpaid invoices:</label>
                                      <div class="col-sm-8"><asp:Label ID="lblUnPaid" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
</div>
<div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> Outstanding invoice amount:</label>
                                      <div class="col-sm-8"><asp:Label ID="lblOutstandingVal" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
</div>
              </div>
         </div>
                            </asp:Panel>
                        </div>
                       
                    </asp:Panel>