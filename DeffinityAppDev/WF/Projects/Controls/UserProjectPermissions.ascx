<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_UserProjectPermissions" Codebehind="UserProjectPermissions.ascx.cs" %>

    <asp:GridView ID="GridView1" runat="server"  DataKeyNames="ID" GridLines="None"  EmptyDataText="No data exist." 
            width="100%" AutoGenerateColumns="False"   HorizontalAlign="Left" BorderColor="White" BorderStyle="None" CellPadding="0" CellSpacing="1" ShowFooter="True" DataSourceID="SqlDataSource1" AllowPaging="true" PageSize="10" >
            <Columns>
            
                <asp:TemplateField  HeaderText="Resource" HeaderStyle-CssClass="header_bg_l">
                <ItemTemplate>
                        <asp:Label ID="lblContractor" runat="server" Text='<%# Bind("CONTRACTORNAME") %>'></asp:Label>
                        <asp:HiddenField runat="server" ID ="HID" Value='<%# Bind("ID") %>' />     
                        <asp:HiddenField runat="server" ID ="HContractorID" Value='<%# Bind("ContractorID") %>' />     
                    </ItemTemplate>
                   <ItemStyle Width="120px" HorizontalAlign="Center" />                   
                </asp:TemplateField> 
                <asp:TemplateField  HeaderText="Admin">
                <ItemTemplate>
                        <asp:CheckBox ID="chkAdmin" runat="Server" Checked='<%# Bind("AdminRights") %>' />
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="80px" />          
                </asp:TemplateField>              
                <asp:TemplateField  HeaderText="Modify Project">
                <ItemTemplate>
                        <asp:CheckBox ID="chkModifyProject" runat="Server" Checked='<%# Bind("ModifyProject") %>' />
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="80px" />          
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="Allocate Task">
                <ItemTemplate>
                        <asp:CheckBox ID="chkAllocateTask" runat="Server" Checked='<%# Bind("AllocateTask") %>' />
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="80px" />          
                </asp:TemplateField>
<asp:TemplateField  HeaderText="Manage Issues">
                <ItemTemplate>
                        <asp:CheckBox ID="chkManageIssues" runat="Server" Checked='<%# Bind("ManageIssues") %>' />
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="80px" />          
                </asp:TemplateField>
<asp:TemplateField  HeaderText="Manage Risks">
                <ItemTemplate>
                        <asp:CheckBox ID="chkManageRisks" runat="Server" Checked='<%# Bind("ManageRisk") %>' />
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="80px" />          
                </asp:TemplateField>
<asp:TemplateField  HeaderText="Add Assets">
                <ItemTemplate>
                        <asp:CheckBox ID="chkAddAssets" runat="Server" Checked='<%# Bind("AddAssets") %>' />
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="80px" />          
                </asp:TemplateField>
<asp:TemplateField  HeaderText="Add Documents">
                <ItemTemplate>
                        <asp:CheckBox ID="chkAddDocuments" runat="Server" Checked='<%# Bind("AddDocuments") %>' />
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="80px" />          
                </asp:TemplateField>
<asp:TemplateField  HeaderText="Delete Document">
                <ItemTemplate>
                        <asp:CheckBox ID="chkDeleteDocument" runat="Server" Checked='<%# Bind("DeleteDocument") %>' />
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="80px" />          
                </asp:TemplateField>

<asp:TemplateField  HeaderText="Manage Project Financials">
                <ItemTemplate>
                        <asp:CheckBox ID="chkManageProjFinancials" runat="Server" Checked='<%# Bind("ManageProjectFinancials") %>' />
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="80px" />          
                </asp:TemplateField>
<asp:TemplateField  HeaderText="Approve Variations" HeaderStyle-CssClass="header_bg_r">
                <ItemTemplate>
                        <asp:CheckBox ID="chkApproveVariations" runat="Server" Checked='<%# Bind("ApproveVariations") %>' />
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="80px" />          
                </asp:TemplateField>
               </Columns>   
   </asp:GridView>
   <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring%>" SelectCommand="DEFFINITY_PROJ_PERMISSIONS" SelectCommandType="StoredProcedure" >
        <SelectParameters>
            <asp:QueryStringParameter Type="int32" Name="ProjectReference" QueryStringField="Project" DefaultValue="80" />  
        </SelectParameters>   
        </asp:SqlDataSource>
        
   <div class="form-group">
       <div class="col-md-12">
   <asp:Button ID="imgbtnUpdatePermissions" runat="server" 
           OnClick="btnUpdatePermissions_Click" SkinID="btnUpdate" />
   </div>
       </div>