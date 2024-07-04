<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectPmApproval" Title="Check Points" Codebehind="ProjectPmApproval.aspx.cs" %>
<%@ Register Src="controls/ProjectTabs.ascx" TagName="BuildProjectTabs" TagPrefix="uc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:BuildProjectTabs ID="BuildProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.CheckPoints%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline" ></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

     <div>
    <asp:Label runat="server" ID="lblError" ForeColor="Red" Font-Size="Small"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="Small" />	
    </div>
       <asp:GridView ID="GridView1" runat="server"  DataKeyNames="ID" GridLines="None"  EmptyDataText="No data exist." 
            width="100%" AutoGenerateColumns="False" HorizontalAlign="Left" ShowFooter="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnRowUpdating="GridView1_RowUpdating" OnRowUpdated="GridView1_RowUpdated" OnRowCommand="GridView1_RowCommand" DataSourceID="SqlDataSource1" >
            <Columns>    
                 <asp:TemplateField ShowHeader="False">
                     <EditItemTemplate>
                         <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Update" SkinID="BtnLinkUpdate" Text="Update" />
                         &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ImgCancel" Text="Cancel" />
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Edit" SkinID="ImgEdit" Text="Edit" />
                     </ItemTemplate>
                     <HeaderStyle CssClass="header_bg_l" />
                     <ItemStyle HorizontalAlign="Center" Width="3%" />
                 </asp:TemplateField>
                <asp:TemplateField  HeaderText="Start Date">
                <ItemTemplate>
                        <asp:Label ID="lblSdate" runat="server" Text='<%# Bind("ScheduledDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEdate" runat="server" Width="70px" Text='<%# Bind("ScheduledDate","{0:d}") %>'></asp:TextBox> <asp:Label ID="Image3" runat="server" SkinID="Calender"  />
                       
                        <ajaxToolkit:calendarextender id="CalendarExtender3" runat="server"  popupbuttonid="Image3"
                           targetcontrolid="txtEdate"  CssClass="MyCalendar"></ajaxToolkit:calendarextender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please enter valid end date" ControlToValidate="txtEdate" Text="*" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((1[6-9]|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                                
                </asp:TemplateField>               
                <asp:TemplateField  HeaderText="Tasks">
                <ItemTemplate>
                        <asp:Label ID="lbltask1" runat="server" Text='<%# Bind("Tasks") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lbltask1" runat="server" Text='<%# Bind("Tasks") %>'></asp:Label>
                    </EditItemTemplate>
                      <ItemStyle Width="160px" />          
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assign To">
               
                <ItemTemplate>
                    <asp:Label ID="lblAssignto" runat="server" Text='<%# Bind("assignto") %>'></asp:Label>
                </ItemTemplate>         
               
               <EditItemTemplate>
               <asp:DropDownList ID="ddlResources" runat="server" DataSourceID="sqlResources" DataTextField="ContractorName" DataValueField="ID" SkinID="ddl_125px">                        
                        </asp:DropDownList>
                    <asp:SqlDataSource ID="sqlResources" runat="server" SelectCommand="Select ID,ContractorName from Contractors where SID = 1 and Status = 'Active'" SelectCommandType="text"  ConnectionString="<%$ ConnectionStrings:DBstring %>" ></asp:SqlDataSource>
                    <asp:HiddenField runat="server" ID ="HID" Value='<%# Bind("ID") %>' />
                    
               
               </EditItemTemplate>
                </asp:TemplateField>       
                
                 <asp:TemplateField  HeaderText="Status" >
                <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblStatus1" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </EditItemTemplate>
                      <ItemStyle Width="50px" HorizontalAlign="Center" />          
                     <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
               <%-- <asp:TemplateField HeaderText="Issue type">
                <ItemTemplate>
                    <asp:Label ID="lblIssuseType" runat="server" Text='<%# getIssueName(DataBinder.Eval(Container.DataItem, "IssuseType").ToString()) %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                <asp:DropDownList ID="ddlQAtype1" runat="server" ValidationGroup="valAddNew" SelectedValue='<%# getIssue(DataBinder.Eval(Container.DataItem, "IssuseType").ToString()) %>'>
	<asp:ListItem Text ="QA" Value="1">
</asp:ListItem>
<asp:ListItem Text ="Health and Safety" Value="2">
</asp:ListItem>
    </asp:DropDownList>
                </EditItemTemplate>
                <ItemStyle Width="120px" />
                </asp:TemplateField>--%>
                
                <asp:TemplateField  HeaderText="Notes">
                <ItemTemplate>
                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox TextMode="MultiLine" ID="txtNotes" runat="server" Width="130px" Text='<%# Bind("Notes") %>'></asp:TextBox>
                    </EditItemTemplate>
                      <ItemStyle Width="170px" />          
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False"  HeaderStyle-CssClass="header_bg_r" >
                    <ItemTemplate>
                        <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete" 
                            SkinID="BtnLinkDelete"  CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                    </ItemTemplate>

<HeaderStyle CssClass="header_bg_r"></HeaderStyle>

                    <ItemStyle Width="4%" />
                </asp:TemplateField>
          </Columns>   
   </asp:GridView>
   <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>" SelectCommand="DN_selectQAShedule" SelectCommandType="StoredProcedure" UpdateCommand="DN_UpdateQASchedule" UpdateCommandType="StoredProcedure" DeleteCommandType="Text" DeleteCommand="DELETE FROM ProjectQASchedule WHERE ID = @ID">
        <SelectParameters>
            <asp:QueryStringParameter Type="int32" Name="Project" QueryStringField="Project" DefaultValue="80" />            
        </SelectParameters>   
        <UpdateParameters>
        <asp:Parameter Name="ScheduledDate" Type="datetime"/>
        <asp:Parameter Name="Notes" Type="string" Size="100" />
        <asp:Parameter Name="ID" Type="int32" Size="4" />   
        <asp:Parameter Name="assignto" Type="int32" />
       <%-- <asp:Parameter Name="IssuseType" Type="int32" />    --%>    
        </UpdateParameters>     
        <DeleteParameters>
        <asp:Parameter Name="ID" Type="int32" Size="4" />        
        </DeleteParameters>
        </asp:SqlDataSource>

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

