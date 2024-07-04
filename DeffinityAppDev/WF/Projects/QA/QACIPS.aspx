<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="QACIPS" Title="QACIPS" Codebehind="QACIPS.aspx.cs" %>
<%@ Register Src="controls/QAtabs.ascx" TagName="QATabs" TagPrefix="uc3" %>
<%@ Register Src="~/WF/Projects/Checkpoint/controls/Checkpoint_tabs.ascx" TagName="OpsViewTabs" TagPrefix="uc2" %>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.QA%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.CSI%> -  <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc3:QATabs ID="QATab1" runat="server" Visible="false" />
    <uc2:OpsViewTabs ID="OpsViewTabs1" runat="server" Visible="false" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="form-group">
          <div class="col-md-12">
               <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
                 <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group2" />
                 <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Group3" />
	</div>
</div>
    
<div class="form-group">
          <div class="col-md-12">
              <p>Suggestions for improvements include:</p>
	</div>
</div>



    <asp:GridView ID="GridView1" runat="server" 
                            AutoGenerateSelectButton="false" DataKeyNames="ID" 
                            OnRowCommand="GridView1_RowCommand" OnRowDeleted="GridView1_RowDeleted" OnRowUpdated="GridView1_RowUpdated"
                            OnRowCreated="GridView1_RowCreated" GridLines="None" AutoGenerateColumns="False"
                            HorizontalAlign="Left" 
                            CellPadding="0" CellSpacing="1" ShowFooter="True" Width="100%" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting" EmptyDataText="There were no improvement ideas submitted">
                         
                            <Columns>
                                <asp:TemplateField>  
              <HeaderStyle Width="60px" />
                <ItemStyle  Width="60px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit" CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit" ></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"  CommandArgument='<%# Bind("ID")%>' ValidationGroup="Group1" ToolTip="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel" SkinID="BtnLinkCancel" ToolTip="Cancel" ></asp:LinkButton>
                </EditItemTemplate>
               <FooterTemplate>
                            <asp:LinkButton ID="LinkButtonInsert" runat="server" CommandName="Insert" Text="Insert" ValidationGroup="Group3" SkinID="BtnLinkAdd"></asp:LinkButton>
                           
                            </FooterTemplate>
            </asp:TemplateField>
              <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" /> 
                                
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,DateLogged%>">
                                    <ItemTemplate>
                                        <asp:Literal ID="litLoggedDate" runat="server" Text='<%#Bind("DateLogged","{0:d}") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>
                            
                      <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SuggestionforImprovement%>">
           <HeaderStyle Width="400px" />
            <ItemStyle Width="325px" />
                <EditItemTemplate>
                    <asp:TextBox ID="txtImprovement" runat="server" Width="300px" TextMode="MultiLine"  Text='<%# Bind("Improvement") %>'></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtImprovement"
                                            Display="None" ErrorMessage="Please Enter Suggestion for Improvement" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Improvement") %>' Width="325px" ToolTip='<%#Bind("Improvement") %>'></asp:Label>
                </ItemTemplate>
               <FooterTemplate>
               <asp:TextBox ID="txtImprovement1" runat="server" Width="300px" TextMode="MultiLine"  Text=''></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtImprovement1"
                                            Display="None" ErrorMessage="Please Enter Suggestion for Improvement" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                   
               </FooterTemplate>
            </asp:TemplateField>           
            
              <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Resource%>" SortExpression="ContractorID">
                  <EditItemTemplate>
                      <asp:DropDownList ID="ddresource" runat="server" DataSourceID="QACIPResource"
                          DataTextField="TEXT" DataValueField="VALUE" Width="155px" selectedValue='<% # Bind("ContractorID") %>'>
                      </asp:DropDownList><asp:SqlDataSource ID="QACIPResource" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                          SelectCommand="DN_CIPResource" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddresource"
                    ErrorMessage="Please select Resource" InitialValue="0" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>    
                  </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblContractorName" runat="server" Text='<%# Bind("Name") %>' ToolTip='<%#Bind("Name") %>'></asp:Label>
                </ItemTemplate>
           <FooterTemplate>
           <asp:DropDownList ID="ddresource1" runat="server" DataSourceID="QACIPResource1"
                          DataTextField="TEXT" DataValueField="VALUE" Width="155px" >
                      </asp:DropDownList><asp:SqlDataSource ID="QACIPResource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                          SelectCommand="DN_CIPResource" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddresource1"
                    ErrorMessage="Please select Resource" InitialValue="0" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
           </FooterTemplate>
            <HeaderStyle Width="100px" />
            <ItemStyle Width="120px" />
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ResultExpected%>">
                 <ItemTemplate>
                 <asp:Label ID="lblResultExpected" runat="server" Text='<%# Bind("ResultExpected") %>' ></asp:Label>
                 </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox ID="txtResultExpected" runat="server" Width="150px" TextMode ="MultiLine" Text='<%# Bind("ResultExpected") %>'></asp:TextBox>
                 </EditItemTemplate>
                 <FooterTemplate>
                  <asp:TextBox ID="txtResultExpected1" runat="server" Width="150px" TextMode ="MultiLine" Text=''></asp:TextBox>
                 </FooterTemplate>
                 </asp:TemplateField>          
                 <asp:TemplateField>
            <HeaderStyle Width="70px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete" 
                            SkinID="BtnLinkDelete"  CommandArgument='<%# Bind("ID") %>'  OnClientClick="return confirm('Do you want to delete the record?');" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
            <table>
                <tr  style="font-weight:bold"> <td style="width: 100px; height: 20px" >
                    </td>
                    
                    <td  style="width: 100px; height: 20px">&nbsp;</td>
                   <td  style="width: 300px; height: 20px;">Suggestion for Improvement</td>
                   <td  style="width: 100px; height: 20px;">Resource</td>
                    <td style="width: 200px; height: 20px;">Result Expected</td>
                    </tr>
                <tr>
                    <td class="even_row" style="width: 100px">
                        <asp:LinkButton ID="btSend" runat="server" CommandName="EmptyInsert"   UseSubmitBehavior="False" ValidationGroup="Group2" SkinID="BtnLinkUpdate"></asp:LinkButton></td>
                
                    <td Class="even_row" style="width: 100px"> 
                    
                    </td>
                    <td Class="even_row"  style="width: 300px">       
                   
                    <asp:TextBox ID="txtImprovement2" width="200px" Text =''  runat="server" TextMode="MultiLine" ></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtImprovement2"
                                            Display="None" ErrorMessage="Please enter Suggestion for Improvement" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                      
                    </td>
                    <td Class="even_row" style="width: 100px">
                    
                   <asp:DropDownList ID="ddresource2" runat="server" DataSourceID="QACIPResource2"
                          DataTextField="TEXT" DataValueField="VALUE" Width="155px" >
                      </asp:DropDownList><asp:SqlDataSource ID="QACIPResource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                          SelectCommand="DN_CIPResource" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddresource2"
                    ErrorMessage="Please select Resource" InitialValue="0" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                        </td>
                    <td Class="even_row" style="width: 200px">
                     <asp:TextBox ID="txtResultExpected2" runat="server" Width="180px" TextMode="MultiLine" ></asp:TextBox>
                           </td>
                </tr>
            </table>
        </EmptyDataTemplate>
                        </asp:GridView>

    
<%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script>
        
</asp:Content>

