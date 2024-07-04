<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectRiskItems" Title="Risk Items"
    MaintainScrollPositionOnPostback="true" Codebehind="ProjectRiskItems.aspx.cs" %>

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
       <%= Resources.DeffinityRes.RiskItems%> - <Pref:ProjectRef ID="ProjectRef2" runat="server" /> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="linkToRisk" runat="server" SkinID="BackToRisk"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script type="text/javascript">
        function changeParameter1Working() {
            debugger;
            var a = $('#ddlDegreeofImpact').val();
            var b = $('#ddlProbability').val();
            var c = a * b;
            //$('#txtExposure').val(c);
            $('#txtExposure').val(c);

            //var a = document.getElementById("ctl00$ctl00$MainContent$MainContent$ddlDegreeofImpact").options[document.getElementById("ctl00$ctl00$MainContent$MainContent$ddlDegreeofImpact").selectedIndex].value;
            //var b = document.getElementById("ctl00$ctl00$MainContent$MainContent$ddlProbability").options[document.getElementById("ctl00$ctl00$MainContent$MainContent$ddlProbability").selectedIndex].value;
            //var c = a * b;
            //document.getElementById('ctl00$ctl00$MainContent$MainContent$txtExposure').value = c;
        }
    </script>
    <div class="form-group">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1"
                                    DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Group3"
                                    DisplayMode="List" />
                                <asp:RequiredFieldValidator ID="ref_Projecttitle" runat="server" ControlToValidate="txtRiskTitle"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Plsenterdatainrisktitle%>"
                                    ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfv_dateRised" runat="server" ControlToValidate="txtDateRaised"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Enterdatainstartdate%>"
                                    ValidationGroup="Group1"></asp:RequiredFieldValidator>
                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNextReviewDate"
                                    Display="None" ErrorMessage="Please enter Next Review Date" ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                                <asp:RequiredFieldValidator ID="ref_Riskco_ordinator" runat="server" ControlToValidate="ddlRiskCoordinator"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Plsenterdatainriskcoordinator%>"
                                    ValidationGroup="Group1">
                                </asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="ref_Risktype" runat="server" ControlToValidate="DdlRiskType"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PleaseselectRiskTypeForCat%>"
                                    ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="DdlRptStatus"
                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Plsenterdatainriskstatus%>"
                                    InitialValue="0" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                <asp:Label runat="server" ID="lblError" ForeColor="Red" Font-Size="Small" EnableViewState="false"></asp:Label><asp:ValidationSummary
                                    ID="Group1" runat="server" ValidationGroup="Group2" DisplayMode="List" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Plsselectmigrationaction%>"
                                    InitialValue="0" ValidationGroup="Group2" ControlToValidate="dl_AddRisk" Display="None"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtActionDeadline"
                                        ErrorMessage="<%$ Resources:DeffinityRes,PlsenterdatainActionDeadline%>" ValidationGroup="Group2"
                                        Display="None"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                            runat="server" ControlToValidate="DdlStatus" ErrorMessage="<%$ Resources:DeffinityRes,Plsselectstatus%>"
                                            InitialValue="0" ValidationGroup="Group2" Display="None"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAssignedTo"
                                                ErrorMessage="<%$ Resources:DeffinityRes,Plsselectassignedto%>" InitialValue="0"
                                                ValidationGroup="Group2" Display="None"></asp:RequiredFieldValidator>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgactiondeadline"
                                    TargetControlID="txtActionDeadline"  CssClass="MyCalendar">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                    PopupButtonID="imgDateRaised" TargetControlID="txtDateRaised" CssClass="MyCalendar">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                    PopupButtonID="imgNextReviewDate" TargetControlID="txtNextReviewDate" CssClass="MyCalendar">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DdlRagStatus"
                                    ErrorMessage="<%$ Resources:DeffinityRes,PlsselectRAGStatus%>" ValidationGroup="Group2"
                                    Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblerror1" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
        </div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RiskReference%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtRiskReference" runat="server" Enabled="True" ReadOnly="true" SkinID="txt_80"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.PotentialDelay%></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:TextBox ID="txtdelay" runat="server"  SkinID="Price_75px" CssClass="txt_field"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtdelay"
                                    ErrorMessage="<%$ Resources:DeffinityRes,Plsenternumbersonly%>" SetFocusOnError="True"
                                    ValidationExpression="\d*" ValidationGroup="Group1" Display="None"></asp:RegularExpressionValidator>
					</div>
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.RiskTitle%><span style="color: Red">*</span></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtRiskTitle" runat="server" MaxLength="50" CssClass="txt_field"
                                    Enabled="True" SkinID="txt_80"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.MinimumCost%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtMinimumCost" runat="server" MaxLength="20" CssClass="txt_field"
                                    Enabled="True" SkinID="Price_100px"></asp:TextBox>
					</div>
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label form-inline"><%= Resources.DeffinityRes.DateRaised%>
                                <span style="color: Red">*</span></label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtDateRaised" runat="server" MaxLength="10"
                                    Enabled="True" SkinID="txtCalender"></asp:TextBox>
                                <asp:Label ID="imgDateRaised" runat="server" ToolTip="<%$ Resources:DeffinityRes,Pickadate%>"
                                    SkinID="Calender" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Plsenterdateformat%>"
                                    ControlToValidate="txtDateRaised" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.MaxCost%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtMaximumCost" runat="server" MaxLength="50" 
                                    Enabled="True" SkinID="Price_100px"></asp:TextBox>
					</div>
				</div>
</div>
     <div class="form-group">
          <div class="col-md-6">
                                       <label class="col-sm-4 control-label form-inline">  <%= Resources.DeffinityRes.RiskCoordinator%>
                                <span style="color: Red">*</span></label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlRiskCoordinator" runat="server" Enabled="True"
                                    SkinID="ddl_80">
                                </asp:DropDownList>
					</div>
				</div>
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label form-inline"> <%= Resources.DeffinityRes.Category%>
                                <span style="color: Red;">*</span></label>
                                      <div class="col-sm-8 form-inline"> 
                                          <asp:DropDownList ID="DdlRiskType" runat="server" Enabled="True"
                                   SkinID="ddl_80" >
                                </asp:DropDownList>
                                <asp:TextBox ID="txtRiskType" runat="server" Visible="false" CssClass="txt_field"
                                    Enabled="True" Width="100px"></asp:TextBox>
                                <asp:LinkButton ID="btnAddIcon" runat="server" Visible="false" OnClick="btnAddIcon_Click" SkinID="BtnLinkAdd" />
                                <asp:LinkButton ID="btnAddRiskType" runat="server" OnClick="btnAddRiskType_Click"
                                    Visible="false" ToolTip="<%$ Resources:DeffinityRes,AddRiskType%>" SkinID="BtnLinkUpdate" />&nbsp;
                                <asp:LinkButton ID="btnCancelRiskType" runat="server" OnClick="btnCancelRiskType_Click"
                                    Visible="false" ToolTip="<%$ Resources:DeffinityRes,Cancel%>" SkinID="BtnLinkCancel" />
					</div>
				</div>

</div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label form-inline">  <%= Resources.DeffinityRes.DegreeofImpact%><span style="color: Red;">*</span></label>
                                      <div class="col-sm-8 form-inline"><asp:DropDownList ID="ddlDegreeofImpact" onchange="changeParameter1Working();" runat="server" ClientIDMode="Static"
                                    SkinID="ddl_80" ValidationGroup="Submit">
                                    <asp:ListItem Text="Please Select..." Value="0"></asp:ListItem>
                                    <asp:ListItem Text="High" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Medium" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Low" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlDegreeofImpact"
                                    Display="None" ErrorMessage="Please select Degree of Impact" InitialValue="0"
                                    ValidationGroup="Group1"></asp:RequiredFieldValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.ReportStatus%></label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="DdlRptStatus" runat="server" SkinID="ddl_80">
                                </asp:DropDownList>
					</div>
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Probability%>
                                <span style="color: Red;">*</span></label>
                                      <div class="col-sm-8 form-inline"><asp:DropDownList ID="ddlProbability" onchange="changeParameter1Working();" runat="server" ClientIDMode="Static"
                                   SkinID="ddl_80" ValidationGroup="Submit">
                                    <asp:ListItem Text="Please Select..." Value="0"></asp:ListItem>
                                    <asp:ListItem Text="High" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Medium" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Low" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlProbability"
                                    Display="None" ErrorMessage="Please select Probability" InitialValue="0" ValidationGroup="Group1"></asp:RequiredFieldValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.NextReviewDate%></label>
                                      <div class="col-sm-8 form-inline"> <asp:TextBox ID="txtNextReviewDate" runat="server" MaxLength="10" 
                                    Enabled="True" SkinID="txtCalender"></asp:TextBox>
                                <asp:Label ID="imgNextReviewDate" runat="server" ToolTip="<%$ Resources:DeffinityRes,Pickadate%>"
                                    SkinID="Calender" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Plsenterdateformat%>"
                                    ControlToValidate="txtNextReviewDate" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
					</div>
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Exposure%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtExposure" runat="server" Enabled="True" ReadOnly="true" ClientIDMode="Static" 
                                    SkinID="Price_50px" CssClass="txt_field"></asp:TextBox>
					</div>
				</div>
 
</div>
    <div class="form-group">
        <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.ClosureCriteria%></label>
                                      <div class="col-sm-10"><asp:TextBox ID="txtClosureCriteria" runat="server" SkinID="txtMulti" Enabled="True"></asp:TextBox>
					</div>
				</div>
    </div>

    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8 form-inline"> <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" ValidationGroup="Group1"
                        SkinID="btnSubmit" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" SkinID="btnCancel" />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8">
					</div>
				</div>
</div>

     <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.MitigatingActions%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
     <asp:Panel Width="100%" ID="PnlGrid" runat="server">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Records Found"
                        AllowPaging="True" DataKeyNames="ID" HorizontalAlign="Center" AllowSorting="True"
                        DataSourceID="Mysource" OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand1"
                        OnRowUpdated="GridView1_RowUpdated" OnRowEditing="GridView1_RowEditing" CaptionAlign="Top"
                        OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ImageButton2" runat="server" Enabled="<%#CommandField()%>" CausesValidation="False" CommandName="Select"
                                        SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>" />
                                    <asp:HiddenField runat="server" ID="HID" Value='<%# Bind("ID") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="header_bg_l" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="<%$ Resources:DeffinityRes,ID%>" ReadOnly="True"
                                SortExpression="ID" Visible="False">
                                <ItemStyle Wrap="True" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Risk%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblmake" runat="server" Text='<%# Bind("MigatingActions") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,AssignedTo%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedTo" Text='<% # Bind("AssignedTo") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ActionDeadline%>">
                                <ItemStyle Width="125px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblActionDeadline" Text='<%# Bind("ActionDeadline","{0:d}") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,RagStatus%>">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# getImage(DataBinder.Eval(Container.DataItem,"RagStaus").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Status%>">
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus1" Text='<% # Bind("Status1") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" SkinID="BtnLinkDelete" Text="Delete" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="header_bg_r" />
                                <ItemStyle HorizontalAlign="Center" Width="25px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="Mysource" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="SELECT AC2P_RiskItems.ID, AC2P_RiskItems.RiskReference, AC2P_RiskItems.AssignedTo, AC2P_RiskItems.ActionDeadline, AC2P_RiskItems.RagStaus, AC2P_RiskItems.Status, AC2PStatus.Status AS Status1, MasterMigatingActions.MigatingActions FROM AC2P_RiskItems INNER JOIN AC2PStatus ON AC2P_RiskItems.Status = AC2PStatus.ID INNER JOIN MasterMigatingActions ON AC2P_RiskItems.MitigationAction = MasterMigatingActions.ID where AC2P_RiskItems.RiskReference = @RiskReference"
                        UpdateCommand="UPDATE AC2P_RiskItems SET MitigationAction = @MitigationAction, AssignedTo = @AssignedTo, ActionDeadline=@ActionDeadline,Status = @Status where (ID = @ID)"
                        DeleteCommand="DELETE FROM AC2P_RiskItems where ID = @ID">
                        <SelectParameters>
                            <asp:QueryStringParameter QueryStringField="RiskRef" Name="RiskReference" Type="Int32"
                                DefaultValue="0" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="ID" Type="Int32" />
                            <asp:Parameter Name="MitigationAction" Type="String" />
                            <asp:Parameter Name="AssignedTo" Type="String" />
                            <asp:Parameter Name="ActionDeadline" Type="DateTime" />
                            <asp:Parameter Name="RagStaus" Type="String" />
                            <asp:Parameter Name="Status" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="ID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                </asp:Panel>
     <div class="sec_table" id="pnlrisk" runat="server" visible="false">
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.Risk%><span style="color: Red">*</span></label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:DropDownList ID="dl_AddRisk" runat="server" ValidationGroup="Group2" SkinID="ddl_80" />
                                <asp:TextBox ID="txtMigration" runat="server" CssClass="txt_field" MaxLength="50"
                                    Visible="false" Wrap="False" SkinID="txt_80"></asp:TextBox>&nbsp;
                                <asp:LinkButton ID="btnAddMigration" runat="server" OnClick="btnAddMigration_Click"
                                    ToolTip="<%$ Resources:DeffinityRes,AddMitigatingAction%>" SkinID="BtnLinkAdd" />
                                <asp:LinkButton ID="btn_MigrationPlus" runat="server" OnClick="btn_MigrationPlus_Click"
                                    Visible="false" ToolTip="<%$ Resources:DeffinityRes,AddMitigatingAction%>" SkinID="BtnLinkUpdate" CausesValidation="false" />
                                <asp:LinkButton ID="btn_MigrationCancel" runat="server" OnClick="btn_MigrationCancel_Click"
                                    ToolTip="<%$ Resources:DeffinityRes,Cancel%>" Visible="false" SkinID="BtnLinkCancel" />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.AssignedTo%><span style="color: Red">*</span></label>
                                      <div class="col-sm-8 form-inline"><asp:DropDownList ID="ddlAssignedTo" runat="server" CssClass="txt_field" ValidationGroup="Group2"
                                    SkinID="ddl_80">
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="txtAssignedTo" MaxLength="50" Visible="false" Wrap="False"
                                    CssClass="txt_field" Width="200px"></asp:TextBox>&nbsp;
                                <asp:LinkButton ID="btnAddAssignto" runat="server" OnClick="btnAddAssignto_Click"
                                    ToolTip="<%$ Resources:DeffinityRes,AddAssignTo%>" SkinID="BtnLinkAdd" />
                                <asp:LinkButton ID="btnAssignto" runat="server" OnClick="btnAssignto_Click" Visible="false"
                                    ToolTip="<%$ Resources:DeffinityRes,AddAssignTo%>" SkinID="BtnLinkUpdate" /><asp:LinkButton
                                        ID="btnCancelAssignto" runat="server" OnClick="btnCancelAssignto_Click" Visible="false"
                                        ToolTip="<%$ Resources:DeffinityRes,Cancel%>" SkinID="BtnLinkCancel" />
					</div>
				</div>
</div>

         <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.ActionDeadline%><span style="color: Red">*</span></label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txtActionDeadline" runat="server" MaxLength="10" CssClass="txt_field"
                                    Enabled="True" ValidationGroup="Group2" SkinID="txtCalender"></asp:TextBox>
                                <asp:Label ID="imgactiondeadline" runat="server" BORDER="0" ToolTip="<%$ Resources:DeffinityRes,Pickadate%>  "
                                    SkinID="Calender" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Plsenterdateformat%>"
                                    ControlToValidate="txtActionDeadline" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.RAGStatus%><span style="color: Red">*</span></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="DdlRagStatus" runat="server" SkinID="ddl_80"
                                    ValidationGroup="Group2">
                                </asp:DropDownList>
					</div>
				</div>
</div>

         <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label form-inline"> <%= Resources.DeffinityRes.Status%><span style="color: Red">*</span></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="DdlStatus" runat="server" SkinID="ddl_80" CssClass="txt_field"
                                    ValidationGroup="Group2">
                                </asp:DropDownList>
					</div>
				</div>
 
</div>

         <div class="form-group">
                                 <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.MitigationAction%></label>
                                      <div class="col-sm-10"> <asp:TextBox ID="txtResolution" runat="server" Enabled="True"
                                    SkinID="txtMulti"></asp:TextBox>
					</div>
				</div>
</div>
         <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8 form-inline"> <asp:Button ID="btnAddItems" runat="server" OnClick="btnAddItems_Click" ValidationGroup="Group2"
                            SkinID="btnSubmit" />
                        <asp:Button ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" Visible="False"
                            ValidationGroup="Group2" SkinID="btnUpdate" />&nbsp;
                        <asp:Button ID="btnCancelItems" runat="server" OnClick="btnCancelItems_Click"
                            SkinID="btnCancel" />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8"> <asp:HiddenField ID="HidAssignTo" runat="server" Visible="False" /> <asp:HiddenField ID="HdRiskItem" runat="server" />
					</div>
				</div>
</div>
         </div>
    
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8">
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8"> <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" CausesValidation="false"
                        SkinID="btnDefault" Text="Back" Visible="false" />
					</div>
				</div>
</div>
  
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
