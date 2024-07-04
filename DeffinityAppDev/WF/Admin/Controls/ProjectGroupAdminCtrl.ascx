<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ProjectGroupAdminCtrl" Codebehind="ProjectGroupAdminCtrl.ascx.cs" %>

    <div class="form-group">
          <div class="col-sm-6">
              <asp:DropDownList ID="ddlProjectAdmins" runat="server"></asp:DropDownList> 
            </div>
           <div class="col-sm-4">
               <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_Click" />
            </div>
        <div class="col-sm-2">

            </div>
	</div>

   
    
   <div class="form-group">
          <div class="col-md-12">
               <asp:Label ID="lblMsg" runat="server" EnableViewState="false"></asp:Label>
	</div>
</div>
   
    
     <script type="text/javascript">
         //GridCornerStyle('<%=gridAdmins.ClientID %>');
     </script>
    <asp:GridView ID="gridAdmins" runat="server" Width="100%" OnRowCommand="gridAdmins_RowCommand">
        <Columns>
            <asp:TemplateField ItemStyle-Width="90%">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton SkinID="BtnLinkDelete" ID="btnDel" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>'  OnClientClick="return confirm('Do you want to delete the record?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
