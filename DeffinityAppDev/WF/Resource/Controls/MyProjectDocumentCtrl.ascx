<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="controls_MyProjectDocumentCtrl" Codebehind="MyProjectDocumentCtrl.ascx.cs" %>
<div class="form-group">
      <div class="col-md-6">
           <div class="col-sm-8">
               <asp:TextBox ID="txtDocName" runat="server" MaxLength="100"></asp:TextBox>
            </div>
           <div class="col-sm-4">
               <asp:Button ID="imgSearchDocument" runat="server" SkinID="btnSearch" onclick="imgSearchDocument_Click" />
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>
    
    
<asp:GridView ID="gvMyDocs" runat="server" AutoGenerateColumns="False" Width="100%"
    AllowPaging="True" onpageindexchanging="gvMyDocs_PageIndexChanging" 
    onrowcommand="gvMyDocs_RowCommand">
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_ID%>"
            InsertVisible="False" ReadOnly="True" SortExpression="ID" Visible="false" />
        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_Document%>"
            HeaderStyle-CssClass="header_bg_l">
            <ItemTemplate>
                <asp:Label ID="lblFileSize" runat="server" Text='<%#Eval("DataSize") %>' Visible="false" />
                <asp:Image ID="imgFileIcon" runat="server" ImageUrl='<%#GetIcon(Eval("DocumentName").ToString())%>'
                    ImageAlign="AbsBottom" Style="min-height: 20px; min-width: 20px" />
                <asp:LinkButton ID="lnkDownLoad" runat="server" Text='<%#Eval("DocumentName")%>'
                    CommandArgument='<%#Eval("ID")%>' CommandName="Download" />
                <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' Visible="false" />
            </ItemTemplate>
           
            <HeaderStyle CssClass="header_bg_l"></HeaderStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-Width="40" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Right"
            HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_SizeinKB%>">
            <ItemTemplate>
                <asp:Label ID="lblDataSize" runat="server" Text='<%#Eval("DataSize") %>' />
            </ItemTemplate>
           
            <ItemStyle HorizontalAlign="Right" Width="40px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-Width="5" ItemStyle-Width="10" HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_Uploaded%>">
            <ItemTemplate>
                <asp:Label ID="lblUploadDateTime" runat="server" Text='<%#Eval("UploadDateTime") %>' />
            </ItemTemplate>
            
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Author%>"  HeaderStyle-CssClass="header_bg_r">
            <ItemTemplate>
                <asp:Label ID="lblUpdatedBy" runat="server" Text='<%#Eval("UpdatedBy") %>' />
            </ItemTemplate>
           
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-Width="10" ItemStyle-Width="10"
            HeaderText="<%$ Resources:DeffinityRes, Author%>" Visible="false">
            <ItemTemplate>
                <%#Eval("UpdatedBy")%>
            </ItemTemplate>
            <HeaderStyle CssClass="header_bg_r" Width="10px" />
            <ItemStyle Width="10px" />
        </asp:TemplateField>
    </Columns>
</asp:GridView>
