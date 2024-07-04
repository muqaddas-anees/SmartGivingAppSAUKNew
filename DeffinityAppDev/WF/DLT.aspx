<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Dlt" Title="Deffinity Lockdown Tool" Codebehind="DLT.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" Runat="Server">
    Deffinity Lockdown Tool
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="panel_title" Runat="Server">
     Deffinity Lockdown Tool
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
<table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
 
  <tr>    
    <td class="p_section1 data_carrier_block">
    
<script type="text/javascript"> 
<!--
   
      function selectAll(chkVal, idVal)
            {
                var frm = document.forms[0];
                if (idVal.indexOf('chkItem') != -1 && chkVal == true)
                {
                    
                    var AllAreSelected = true;
                    for (i=0; i<frm.length; i++) 
                    {
                        if (frm.elements[i].id.indexOf('chkItem') != -1 && frm.elements[i].checked == false)
                        { 
                            AllAreSelected = false;
                            break;
                         } 
                    } 
                    if(AllAreSelected == true)
                    {
                        for (j=0; j<frm.length; j++) 
                        {
                            if (frm.elements[j].id.indexOf ('chkHeading') != -1)
                            {
                                frm.elements[j].checked = true;
                                break;
                            }
                        }
                    }
                } else 
                {
                    for (i=0; i<frm.length; i++) 
                    {
                        
                        if (idVal.indexOf ('chkHeading') != -1) 
                        {   
                            if(chkVal == true)
                            {
                                frm.elements[i].checked = true; 
                            } else 
                            {
                                frm.elements[i].checked = false; 
                            }
                            }else if (idVal.indexOf('chkItem') != -1 && frm.elements[i].checked == false) {
                            
                            for (j=0; j<frm.length; j++) 
                            {
                                if (frm.elements[j].id.indexOf ('chkHeading') != -1) 
                                { 
                                    frm.elements[j].checked = false;
                                    break; 
                                }
                            }
                        }
                    }
                }
            }
-->
</script>

<table width="100%" border="0" >
<tr>
<td>

   <asp:GridView ID="GridView1" ShowFooter="True" runat="server"  DataKeyNames="ID"
               AutoGenerateColumns="False"  Width="100%"  EmptyDataText="No Locked Features." 
               OnRowCancelingEdit="GridView1_RowCancelingEdit" 
               OnRowEditing="GridView1_RowEditing" OnRowCommand="GridView1_RowCommand" 
               OnRowUpdating="GridView1_RowUpdating" DataSourceID="sqlds" OnRowUpdated="GridView1_RowUpdated"  >
             <Columns>      
                   <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkHeading" onclick="selectAll(this.checked,this.id);" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkItem" runat="server" />
                        <asp:HiddenField runat="server" ID ="HID" Value='<%# Bind("ID") %>' /> 
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle  HorizontalAlign="Center" Width="20px"/>
                </asp:TemplateField>
                
             <%--<asp:TemplateField HeaderText="Edit" >  
              <HeaderStyle Width="80px" />
                <ItemStyle  Width="80px" />
                <ItemTemplate>
                    <asp:ImageButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit" CommandArgument="<%# Bind('ID')%>" ImageUrl="~/images/icon_edit.png" ToolTip="Edit" ></asp:ImageButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"  CommandArgument="<%# Bind('ID')%>" ValidationGroup="Group2" ImageUrl="~/images/icon_update.png" ToolTip="Update"></asp:ImageButton>
                    <asp:ImageButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel" ImageUrl="~/images/icon_cancel.png" ToolTip="Cancel" ></asp:ImageButton>
                </EditItemTemplate>
              
            </asp:TemplateField>--%>
             
            <asp:TemplateField HeaderText="Feature">
               <ItemTemplate>
                    <asp:Label ID="lblFeature" runat="server" Text='<%# Bind("Feature") %>'></asp:Label>
                </ItemTemplate>
             </asp:TemplateField>
             
             
            <asp:TemplateField HeaderText="Status">
            <HeaderStyle Width="100px" />
             <ItemStyle Width="100px" />
             <ItemTemplate>
                                        
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Enable") %>'></asp:Label>
                                    </ItemTemplate>
                <EditItemTemplate>
                   <asp:DropDownList ID="ddlStatus" Width="125px"  runat="server" SelectedValue='<%# Bind("Enable") %>' >
                   <asp:ListItem Text="Enabled" Value="True">
                   </asp:ListItem>
                   <asp:ListItem Text="Disabled" Value="False">
                   </asp:ListItem>
                    </asp:DropDownList>
               </EditItemTemplate>
               </asp:TemplateField>
               
        </Columns>
        
    </asp:GridView>
    <asp:SqlDataSource ID="sqlds" runat="server"  
     SelectCommand="select  * from lockdownfeatures" SelectCommandType="text"
     UpdateCommand="UpdateFeatureStatus" UpdateCommandType="StoredProcedure" OnUpdating="sqlds_Updating" DataSourceMode="DataReader">
     <UpdateParameters>
     <asp:Parameter Name="ID" Type="Int32" DefaultValue="0" />
     <asp:Parameter Name="Status" Type="boolean" DefaultValue="True" />
     </UpdateParameters>
     </asp:SqlDataSource>
    
</td>
</tr>
<tr>
<td colspan="2">
    <asp:LinkButton ID="btnEnable" runat="server" OnClick="btnEnable_Click" Text="Enable" Font-Bold="true" OnClientClick="return confirm('Do you want to Enable these Feature(s)?');"></asp:LinkButton>    
    <asp:LinkButton ID="btnDisable" runat="server" OnClick="btnDisable_Click" Text="Disable" Font-Bold="true" OnClientClick="return confirm('Do you want to Disable these Feature(s)?');"></asp:LinkButton>    
</td>
</tr>
</table>
<br />
<table width="100%" border="0" >
<tr>
<td>Max PMs allowed:<asp:TextBox ID="txtMaxPMs" runat="server" ValidationGroup="LimitGroup"></asp:TextBox>
<asp:RequiredFieldValidator ValidationGroup="LimitGroup" runat="Server" id="ReqMaxPM" Text="*" Display="Dynamic" ControlToValidate="txtMaxPMs"></asp:RequiredFieldValidator>
</td>
<td>Max Resources allowed:<asp:TextBox ID="txtMaxResources" runat="server" ValidationGroup="LimitGroup"></asp:TextBox>
<asp:RequiredFieldValidator ValidationGroup="LimitGroup" runat="Server" id="ReqMaxResources" Text="*" Display="Dynamic" ControlToValidate="txtMaxResources"></asp:RequiredFieldValidator>
</td>
<td>
<asp:Button SkinID="btnUpdate" runat="server" ID="imgupdateLimit" ValidationGroup="LimitGroup" OnClick="imgupdateLimit_Click"/>
</td>
</tr>
</table>
    
    </td>
    </tr>
    </table>
     <script src="../../Scripts/respond.min.js"></script>
    <script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <script src="../../Scripts/GridDesingFix.js"></script>
    <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();

        $(window).load(function () {
            $(".dropdown-menu li")
          .find("input[type='checkbox']")
          .prop('checked', 'checked').trigger('change');
            $(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});
        });
    </script>
</asp:Content>

