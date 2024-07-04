<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_UserRestrictions" Codebehind="UserRestrictions.ascx.cs" %>

<table width="500px" border="0" cellspacing="0" cellpadding="0" class="sec_table">
<tr>
<td  class="sec_table_p_bg">
The following users are not allowed to access this project<br />
</td>
</tr>
<tr>
<td>Select Resource
<asp:DropDownList ID="ddlResource" runat="server" DataTextField="ContractorName" DataValueField="ID" DataSourceID="SqlDataSource2" ></asp:DropDownList>

<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring%>" SelectCommand="DEFFINITY_PROJ_PERMISSIONS" SelectCommandType="StoredProcedure" >
        <SelectParameters>
            <asp:QueryStringParameter Type="int32" Name="ProjectReference" QueryStringField="Project" DefaultValue="80" />  
        </SelectParameters>   
</asp:SqlDataSource>

<%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
    SelectCommand="DEFFINITY_RESOURCES" SelectCommandType="StoredProcedure"></asp:SqlDataSource> --%>
    <asp:Button ID="btnRestrict" runat="server" OnClick="btnRestrict_Click" Text="Ristrict" SkinID="btnDefault" />
</td>
</tr>
<tr>
<td >
	<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" HorizontalAlign="Left"  GridLines="None" BorderStyle="None" CellPadding="0" CellSpacing="1" 
             Width="100%" AllowPaging="True" BorderWidth="0px" DataSourceID="Sqlds1" EmptyDataText="No data exist." >        
            <Columns>                
                
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" Visible="False">
                    <ItemStyle Wrap="True"/>
                </asp:BoundField>
                <asp:BoundField DataField="ContractorName" HeaderText="User Name" >
                    <ItemStyle Wrap="True" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Delete">
                        <ItemStyle HorizontalAlign="Center" Width="40px" /> 
                        <ItemTemplate >
                            <asp:LinkButton ID="deletebut" runat="server" CommandName="delete" SkinID="BtnLinkDelete"
                            OnClientClick="return confirm('Do you want to delete the record?');" ToolTip="Delete"
                            Visible="True" />
                            </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                         <FooterStyle HorizontalAlign="Right" />
        </asp:GridView>
     <asp:SqlDataSource ID="Sqlds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>" SelectCommand="DEFFINITY_RESTRICT_PROJUSERS" SelectCommandType="StoredProcedure" DeleteCommand="DEFFINITY_DEL_RESTRICT_PROJUSERS" DeleteCommandType="StoredProcedure">
    <SelectParameters>
    <asp:QueryStringParameter QueryStringField="Project" Name="ProjectReference" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
             <asp:Parameter Name="ID" Type ="Int32" />
    </DeleteParameters>
    </asp:SqlDataSource>
</td>
</tr>

   </table>
