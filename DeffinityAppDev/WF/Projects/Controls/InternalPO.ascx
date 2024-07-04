<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_InternalPO" Codebehind="InternalPO.ascx.cs" %>
<div class="form-group">
          <div class="col-md-12">
              <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group1" />
	</div>
</div>
<div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.PONumber%></label>
           <div class="col-sm-8 form-inline">
               <asp:Label ID="lblPO" runat="server" Text="PO"
                Font-Bold="true"></asp:Label><asp:TextBox ID="txtPONumber" runat="server" SkinID="txt_150px"
                    MaxLength="8"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter PO Number to search"
                ValidationGroup="Group1" ControlToValidate="txtPONumber" Display="None"></asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-4 form-inline">
          <asp:Button ID="imgSearch" runat="server" SkinID="btnSearch" ValidationGroup="Group1"
                OnClick="imgSearch_Click" />
            <asp:Button ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" SkinID="btnDefault" Text="Raise a New PO"></asp:Button>
	</div>
	<div class="col-md-4">
           
	</div>
</div>
<asp:GridView ID="grdPODetails" runat="server" AutoGenerateColumns="False" EmptyDataText="No Records Found"
                OnRowCommand="grdPODetails_RowCommand" Width="85%">
                <Columns>
                    <asp:TemplateField HeaderText="PO&nbsp;Number" HeaderStyle-CssClass="header_bg_l">
                        <HeaderStyle Width="40px" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="40px" />
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" CommandName="Show" runat="server" Text='<%# Bind("PONumber") %>'
                                CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="300px"  ControlStyle-Width="300px" FooterStyle-Width="300px">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vendor">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="175px" />
                        <ItemTemplate>
                            <asp:Label ID="lblVendor" runat="server" Text='<%# Bind("ContractorName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Raised By">
                        <HeaderStyle Width="125px" />
                        <ItemStyle Width="125px" />
                        <ItemTemplate>
                            <asp:Label ID="lblRName" runat="server" Text='<%# Bind("RName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approved By">
                        <HeaderStyle Width="125px" />
                        <ItemStyle Width="125px" />
                        <ItemTemplate>
                            <asp:Label ID="lblAName" runat="server" Text='<%# Bind("AName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase By" HeaderStyle-CssClass="header_bg_r">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPName" runat="server" Text='<%# Eval("PName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="125px" />
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
