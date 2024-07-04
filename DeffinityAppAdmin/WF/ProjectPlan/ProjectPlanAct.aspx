<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WF/MainTab.master" Inherits="ProjectPlanAct" Codebehind="ProjectPlanAct.aspx.cs" %>
<%@ Register src="controls/ProjectProposalTab.ascx" tagname="ProjectProposalTab" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc2:ProjectProposalTab ID="ProjectProposalTab1" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectProposal%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
   <%= Resources.DeffinityRes.ProjectPlanTasks%> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink runat="Server" Text="Back to Project Proposal" NavigateUrl="~/WF/ProjectPlan/ProjectPipeline.aspx?Status=8"><i class="fa fa-arrow-left"></i> Return to Project Proposal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server"> 
  

<div class="form-group"><asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <asp:Label ID="lblError" runat="server" ForeColor="Red" ></asp:Label>
       
     <asp:Label ID="lblerror1" runat="server" ForeColor="Red"></asp:Label></div>

    <div class="form-group">
             <div class="col-md-12 form-inline">
                 <asp:DropDownList ID="ddlActive" runat="server" SkinID="ddl_40" OnSelectedIndexChanged="ddlActive_SelectedIndexChanged"></asp:DropDownList> <asp:Button ID="btnApply" runat="server" SkinID="btnApply" OnClick="btnApply_Click" />
</div>
</div>
    <div class="form-group">
             <div class="col-md-12">
<asp:ValidationSummary ID="Gridsummary1" runat="server" ValidationGroup="GridValid" />
<asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="GridValid_F" />
                 </div>
        </div>
<asp:Panel ID="Panel1" runat="server"  Width="100%" >
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  
        Width="100%" DataSourceID="SqlDataSource1" 
        OnRowUpdating="GridView1_RowUpdating" DataKeyNames="ID" 
        onrowcommand="GridView1_RowCommand" ShowFooter="True">
        <Columns>
            <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="True" 
                            CommandName="Update" 
                             ValidationGroup="GridValid" SkinID="BtnLinkUpdate">
                            </asp:LinkButton>
                       <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" 
                            CommandName="Cancel" SkinID="BtnLinkCancel" >
                            </asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="False" 
                            CommandName="Edit" SkinID="BtnLinkEdit">
                            </asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                            CommandName="Add" ValidationGroup="GridValid_F" SkinID="BtnLinkAdd">
                            </asp:LinkButton>
                         <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel_Add" SkinID="BtnLinkCancel">
                            </asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
            <asp:BoundField Visible="False" DataField="ID" />
            
            <asp:TemplateField HeaderText="Task" ItemStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
            <ItemTemplate>
            <asp:Label runat="server" ID="lblAct" Text='<%#getItemDes(DataBinder.Eval(Container.DataItem, "IndentLevel").ToString(),DataBinder.Eval(Container.DataItem, "Activity").ToString()) %>'></asp:Label> 
            </ItemTemplate>
            <EditItemTemplate>
            <asp:TextBox runat="server" ID="txtAct" TextMode="MultiLine" Text='<%# Bind("Activity") %>' SkinID="txt_80"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAct"
                    ErrorMessage="please enter data in text field" Display="None" ValidationGroup="GridValid"></asp:RequiredFieldValidator>
                
            </EditItemTemplate>
            <FooterTemplate>
            <asp:TextBox runat="server" ID="txtAct_F" TextMode="MultiLine" Text='<%# Bind("Activity") %>' SkinID="txt_80"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAct_F"
                    ErrorMessage="please enter data in text field" Display="None" ValidationGroup="GridValid_F"></asp:RequiredFieldValidator>
            
            </FooterTemplate>
            </asp:TemplateField>
              <asp:TemplateField  HeaderText="Start Date" ItemStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
                <ItemTemplate>
                        <asp:Label ID="lblSdate" runat="server" Text='<%# Bind("StartDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>                    
                    <EditItemTemplate>
                         <asp:TextBox ID="txtSdate" runat="server" MaxLength="10" SkinID="Date" Text='<%# Bind("StartDate","{0:d}") %>' ></asp:TextBox>
                        <asp:Label ID="Image4" SkinID="Calender" runat="server"  />
                        <ajaxToolkit:calendarextender id="CalendarExtender4" runat="server"  popupbuttonid="Image4"
                           targetcontrolid="txtSdate" CssClass="MyCalendar"></ajaxToolkit:calendarextender>
                        <asp:RequiredFieldValidator ID="sdateval1" runat="server" ErrorMessage="Please enter start date" ControlToValidate="txtSdate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="Sdateval2" runat="server" ControlToValidate="txtSdate" ErrorMessage="Please enter valid start date" Operator="DataTypeCheck" Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                        
                 </EditItemTemplate> 
                 <FooterTemplate>
                     <asp:TextBox ID="txtSdate_F" runat="server" MaxLength="10" SkinID="Date"  ></asp:TextBox>
                     <asp:Label ID="Image_F" SkinID="Calender" runat="server" />
                        <ajaxToolkit:calendarextender id="CalendarExtender_F" runat="server"  popupbuttonid="Image_F"
                           targetcontrolid="txtSdate_F" CssClass="MyCalendar"></ajaxToolkit:calendarextender>
                        <asp:RequiredFieldValidator ID="sdateval_F" runat="server" ErrorMessage="Please enter start date" ControlToValidate="txtSdate_F" ValidationGroup="GridValid_F" Display="None" ></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="Sdateval21" runat="server" ControlToValidate="txtSdate_F" ErrorMessage="Please enter valid start date" Operator="DataTypeCheck" Type="Date" ValidationGroup="GridValid_F" Display="None"></asp:CompareValidator>
                        
                 </FooterTemplate>
                 <ItemStyle  HorizontalAlign="Center" Width="80px"/>               
                </asp:TemplateField>                
                <asp:TemplateField  HeaderText="End Date" ItemStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">                  
                <ItemTemplate>
                        <asp:Label ID="lblEdate" runat="server" Text='<%# Bind("EndDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                         <asp:TextBox ID="txtEdate" runat="server" SkinID="Date" MaxLength="10" Text='<%# Bind("EndDate","{0:d}") %>'></asp:TextBox>
                         <asp:Label ID="Image3" runat="server" SkinID="Calender" />
                        <ajaxToolkit:calendarextender id="CalendarExtender3" runat="server"  popupbuttonid="Image3"
                           targetcontrolid="txtEdate" CssClass="MyCalendar"></ajaxToolkit:calendarextender>
                        <asp:RequiredFieldValidator ID="Edateval1" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtEdate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="Edateval2" runat="server" ControlToValidate="txtEdate" ErrorMessage="Please enter valid end date" Operator="DataTypeCheck" Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtEdate_F" runat="server" SkinID="Date" MaxLength="10" ></asp:TextBox>
                        <asp:Label ID="Image3_F" runat="server" SkinID="Calender"    />
                        <ajaxToolkit:calendarextender id="CalendarExtender3_F" runat="server"  popupbuttonid="Image3_F"
                           targetcontrolid="txtEdate_F" CssClass="MyCalendar"></ajaxToolkit:calendarextender>
                        <asp:RequiredFieldValidator ID="Edateval_F" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtEdate_F" ValidationGroup="GridValid_F" Display="None" ></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="Edateval1_F" runat="server" ControlToValidate="txtEdate_F" ErrorMessage="Please enter valid end date" Operator="DataTypeCheck" Type="Date" ValidationGroup="GridValid_F" Display="None"></asp:CompareValidator>
                    </FooterTemplate>                    
                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                    </asp:TemplateField> 
                     <asp:TemplateField  HeaderText="Indent Level">                  
                <ItemTemplate>
                        <asp:Label ID="lblIndentLevel" runat="server" Text='<%# Bind("IndentLevel") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                      <asp:DropDownList ID="ddlIndentLevel" runat="server" SelectedValue='<%# Bind("IndentLevel") %>' SkinID="ddl_80">
                     <asp:ListItem Text="0" Value="0"></asp:ListItem>
                     <asp:ListItem Text="1" Value="1"></asp:ListItem>
                     <asp:ListItem Text="2" Value="2"></asp:ListItem>
                     <asp:ListItem Text="3" Value="3"></asp:ListItem>
                     </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                     <asp:DropDownList ID="ddlIndentLevel_F" runat="server" SkinID="ddl_80">
                     <asp:ListItem Text="0" Value="0"></asp:ListItem>
                     <asp:ListItem Text="1" Value="1"></asp:ListItem>
                     <asp:ListItem Text="2" Value="2"></asp:ListItem>
                     <asp:ListItem Text="3" Value="3"></asp:ListItem>
                     </asp:DropDownList>
                        
                    </FooterTemplate>                    
                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                    </asp:TemplateField> 
            
            <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" SkinID="BtnLinkDelete" Text="Delete" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
</asp:Panel>   
   
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Definnity_SelectProjectPlanActivities "
     SelectCommandType="StoredProcedure" ConnectionString="<%$ ConnectionStrings:DBstring %>"
      UpdateCommand="DN_UpdateProjectPlanActivities" UpdateCommandType="StoredProcedure"
        DeleteCommand="DELETE FROM ProjectPlanActivities where ID = @ID"
        InsertCommand="DN_ProjectPlanActInsert" InsertCommandType="StoredProcedure"
         >
    <UpdateParameters>
    <asp:Parameter Name="ID" Type="int32" />
    <asp:Parameter Name="Activity" Type="String" Size="200" />
    <asp:Parameter Name="StartDate" Type="DateTime" />
    <asp:Parameter Name="EndDate" Type="DateTime" />    
    <asp:Parameter Name="IndentLevel" Type="Int32" />    
    </UpdateParameters>
    <InsertParameters>    
    <asp:Parameter Name="Activity" Type="String" Size="200" />
    <asp:Parameter Name="StartDate" Type="DateTime"/>
    <asp:Parameter Name="EndDate" Type="DateTime" />    
    <asp:Parameter Name="IndentLevel" Type="Int32" />   
    <asp:Parameter Name="ProjectPlanID" Type="Int32" />    
    </InsertParameters>
    <SelectParameters>
         <asp:Parameter Name="ProjectPlanID" Type="Int32" />
    </SelectParameters>
     <DeleteParameters>
             <asp:Parameter Name="ID" Type ="Int32" />
     </DeleteParameters>
    </asp:SqlDataSource>
                <asp:HiddenField ID="HiddenField2" runat="server" />
                <asp:HiddenField ID="HiddenField3" runat="server" />
   


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