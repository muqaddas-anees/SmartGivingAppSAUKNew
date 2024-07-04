<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_CreditGridCntl" Codebehind="CreditGridCntl.ascx.cs" %>
<div class="form-group">
        <div class="col-md-12">
           <strong> Credit Notes</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
  <div class="row">
          <div class="col-md-12">
              <asp:Label ID="lblmsg" runat="server" EnableViewState="false"></asp:Label>
	</div>
</div>
  
<div>
   <asp:GridView ID="gridCreditRecord" runat="server" AutoGenerateColumns="false" Width="100%"
               OnRowCancelingEdit="gridCreditRecord_RowCancelingEdit" OnRowCommand="gridCreditRecord_RowCommand" 
              OnRowEditing="gridCreditRecord_RowEditing" OnRowUpdating="gridCreditRecord_RowUpdating"
               AllowPaging="true" PageSize="20" OnPageIndexChanging="gridCreditRecord_PageIndexChanging" EmptyDataText="No data exists">
          <Columns>               
              <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header_bg_l">
                      <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CausesValidation="false" 
                        ToolTip="Edit" SkinID="BtnLinkEdit" CommandName="Edit" CommandArgument='<%#Bind("Id") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                      <asp:LinkButton ID="BtnUpdate" runat="server" Text="Update" CausesValidation="false"
                           SkinID="BtnLinkUpdate" ToolTip="Update" CommandName="Update" CommandArgument='<%#Bind("Id") %>' />
                             <asp:LinkButton ID="BtnCancel" runat="server" Text="Cancel" ToolTip="Cancel" SkinID="BtnLinkCancel" CausesValidation="false"
                                 CommandName="Cancel" CommandArgument='<%#Bind("id") %>' />
                </EditItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                       <asp:Label ID="lblDescription" runat="server" Text='<%#Bind("Description") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px">
                   <ItemTemplate>
                     <asp:Label ID="lblValue" runat="server" Text='<%#Bind("CreditValue","{0:F2}") %>'></asp:Label>
                   </ItemTemplate>
                    <EditItemTemplate>
                         <asp:TextBox ID="txtCreditValue" runat="server" Width="80px" Text='<%#Bind("CreditValue","{0:F2}") %>'></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="txtCreditFilter" runat="server"
                                                               TargetControlID="txtCreditValue" ValidChars="0123456789."></ajaxToolkit:FilteredTextBoxExtender>
                    </EditItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Applied by">
                  <ItemTemplate>
                      <asp:Label ID="lblAppliedby" runat="server" Text='<%#Bind("Appliedby") %>'></asp:Label>
                  </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Date">
                   <ItemTemplate>
                         <asp:Label ID="lblDate" runat="server" Text='<%#Bind("DateandTime","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
               </asp:TemplateField>
                 
                 <asp:TemplateField HeaderStyle-CssClass="header_bg_r" HeaderStyle-Width="30px">
                     <ItemTemplate>
                       <asp:LinkButton ID="BtnDelete" runat="server" OnClientClick="return confirm('Do you want to delete this record?');" CausesValidation="false"
                                                                CommandName="Delete1" CommandArgument='<%#Bind("Id") %>' SkinID="BtnLinkDelete"/>
                </ItemTemplate>
                 </asp:TemplateField>
            </Columns>
   </asp:GridView>
    
      

</div>