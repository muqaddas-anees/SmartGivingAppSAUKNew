<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="DocVersions" Codebehind="DocVersions.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="tab_subheader" style="border-bottom:solid 1px Silver;width:99%;"><asp:Label ID="lblHeader"  Text="List of documents" runat="server"></asp:Label> </div>

    <asp:GridView ID="gridFiles" EnableViewState="true" runat="server" AutoGenerateColumns="False"
        DataSourceID="sqlFileList" Width="100%" DataKeyNames="ID" OnRowCommand="gridFiles_RowCommand"
        AllowPaging="True" OnRowUpdating="gridFiles_RowUpdating">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_ID%>"
                InsertVisible="False" ReadOnly="True" SortExpression="ID" Visible="false" />
            <asp:TemplateField HeaderStyle-Width="50%" ItemStyle-Width="50" HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_Document%>" HeaderStyle-CssClass="header_bg_l">
                        <ItemTemplate>
                            <asp:Label ID="lblFileSize" runat="server" Text='<%#Eval("DataSize") %>' Visible="false" />
                            <asp:CheckBox ID="chkChecked" Visible='false'  runat="server"></asp:CheckBox>
                            <asp:Image ID="imgFileIcon" runat="server" ImageUrl='<%#GetIcon(Eval("DocumentName").ToString())%>'
                                ImageAlign="AbsBottom" Style="min-height: 20px; min-width: 20px" />
                            <asp:LinkButton ID="lnkDownLoad" runat="server" Text='<%#Eval("DocumentName")%>'
                                CommandArgument='<%#Eval("ID")%>' Enabled='<%#GetCheckInOutEnable(Eval("CheckOut").ToString())%>' CommandName="Download" />
                            
                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' Visible="false" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditName" runat="server" Text='<% # DataBinder.Eval(Container.DataItem,"DocumentName")%>'>
                            </asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle CssClass="header_bg_l"></HeaderStyle>
                    </asp:TemplateField>
             
             <asp:TemplateField HeaderStyle-Width="5%" ItemStyle-Width="2" HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_Version%>">
                    <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>                            
                            <%#Eval("Version")%>
                        </ItemTemplate>
                    </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-Width="25" ItemStyle-HorizontalAlign="Center"
                HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_SizeinKB%>">
                <ItemTemplate>
                    <asp:Label ID="lblDataSize" runat="server" Text='<%#Eval("DataSize") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="16%" ItemStyle-Width="20"  ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_Uploaded%>">
                <ItemTemplate>
                    <asp:Label ID="lblUploadDateTime" runat="server" Text='<%#Eval("UploadDateTime") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="20%"  HeaderText="<%$ Resources:DeffinityRes, Author%>">
                <ItemTemplate>
                    <asp:Label ID="lblUpdatedBy" runat="server" Text='<%#Eval("UpdatedBy") %>' />
                </ItemTemplate>
                <ItemStyle Width="75" />
                <HeaderStyle CssClass="header_bg_r"></HeaderStyle>
            </asp:TemplateField>            
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sqlFileList" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
        SelectCommand="DEFFINITY_FileListVersion" SelectCommandType="StoredProcedure" UpdateCommandType="StoredProcedure"
        UpdateCommand="AC2P_Documents_Update">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="ParentID" QueryStringField="folderID"
                Type="Int32" />
                <asp:QueryStringParameter DefaultValue="-1" Name="DocumentID" QueryStringField="DocumentID"
                Type="Int32" />                
            
        </SelectParameters>
        <UpdateParameters>
            <asp:ControlParameter ControlID="gridFiles" Name="ID" PropertyName="SelectedValue" />
            <asp:Parameter Name="DocumentName" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

<div class="clr"></div>
<div style="float:right"><asp:LinkButton ID="btnlinkClose" runat="server" Text="Close" OnClientClick="javascript:window.close()" Font-Bold="true"></asp:LinkButton></div>

</asp:Content>

