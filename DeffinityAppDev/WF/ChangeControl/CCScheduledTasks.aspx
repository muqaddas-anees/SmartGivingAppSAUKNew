<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="CCScheduledTasks" Title="Untitled Page" Codebehind="CCScheduledTasks.aspx.cs" %>


<%@ Register Src="controls/ChangeControlTab.ascx" TagName="Tab" TagPrefix="Deffinity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ChangeControl%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
  <label id="lblPageTitle" runat="server"></label> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
    <Deffinity:Tab ID="tabMenu" runat="server" EnableViewState="false" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
   <asp:Panel ID="pnlSheduledTasks" runat="server" ScrollBars="None">
       <div class="form-group">
        <div class="col-md-12">
           <strong> Scheduled Tasks </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
       <div class="form-group">
          <div class="col-md-12">
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Tasks"
                        Width="100%" />
	</div>
</div>
       <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Task%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtTask" runat="server" SkinID="txt_90"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTask"
                        ErrorMessage="Task should not be blank" ValidationGroup="Tasks">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.ScheduledDate%></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtOriginalDate" SkinID="Date" runat="server"></asp:TextBox>
                    <asp:Label ID="imgOriginalDate" runat="server" SkinID="Calender" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtOriginalDate"
                        ErrorMessage="Invalid Date" ValidationExpression="(0[1-9]|[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|[1-9]|1[012])[-/.](19\d\d|20\d\d|0[0-9])"
                        ValidationGroup="Tasks">*</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtOriginalDate"
                        ErrorMessage="Original Date should not be blank" ValidationGroup="Tasks">*</asp:RequiredFieldValidator>
                    <ajaxToolkit:CalendarExtender ID="CalOriginalDate" runat="server" CssClass="MyCalendar"
                         PopupButtonID="imgOriginalDate" TargetControlID="txtOriginalDate">
                    </ajaxToolkit:CalendarExtender>
            </div>
	</div>
</div>
       <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"> Revised Date</label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtNewDate" runat="server" SkinID="Date"></asp:TextBox>
                    <asp:Label ID="imgNewDate" runat="server" SkinID="Calender" ImageAlign="AbsMiddle" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNewDate"
                        ErrorMessage="Invalid Date" ValidationExpression="(0[1-9]|[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|[1-9]|1[012])[-/.](19\d\d|20\d\d|0[0-9])"
                        ValidationGroup="Tasks">*</asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="cmpRevisedAndOriginalDates" runat="server" ControlToValidate="txtNewDate"
                        ControlToCompare="txtOriginalDate" ErrorMessage="Please ensure that the revised date is greater than the original date"
                        Display="Dynamic" Text="*" Operator="GreaterThan" Type="Date" ValidationGroup="Tasks" />
                    <ajaxToolkit:CalendarExtender ID="CalNewDate" runat="server" CssClass="MyCalendar"
                         PopupButtonID="imgNewDate" TargetControlID="txtNewDate">
                    </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
                <asp:Button ID="btnAddTask" runat="server"
                OnClick="btnAddChange_Click" ValidationGroup="Tasks" 
                SkinID="btnAdd" />
            </div>
	</div>
</div>

        <asp:Label ID="lblTaskMessage" runat="server" EnableViewState="false" ForeColor="Red" />
        <asp:Panel ID="pnlTaskGrid" runat="server">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" DataSourceID="odsTask"
                DataKeyNames="ID" OnRowUpdated="GridView1_RowUpdated" OnRowUpdating="GridView1_RowUpdating">
                <Columns>
                     <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnedit" runat="server" CausesValidation="false" CommandName="Edit"
                                SkinID="BtnLinkEdit" ToolTip="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="btnupdate" runat="server" CommandName="Update" SkinID="BtnLinkUpdate"
                                CommandArgument='<%# Bind("ID") %> ' ToolTip="Update" ValidationGroup="Grid" />
                            <asp:LinkButton ID="btncancel" runat="server" CausesValidation="false" CommandName="cancel"
                                SkinID="BtnLinkCancel" ToolTip="Cancel" />
                        </EditItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <EditItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Task" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="300px"  ControlStyle-Width="300px" FooterStyle-Width="300px">
                        <ItemTemplate>
                            <asp:Label ID="lblTaskDescription" runat="server" Text='<%#Eval("TaskDescription")%>'
                                Width="250px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTaskDescription" runat="server" Text='<%#Bind("TaskDescription")%>'
                                Width="250px" />
                            <asp:RequiredFieldValidator ID="rqdTask" runat="server" Text="*" ControlToValidate="txtTaskDescription"
                                Display="Dynamic" ValidationGroup="Grid" ToolTip="Task description is mandatory" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Original Date" ItemStyle-Width="180px" ControlStyle-CssClass="form-inline" ItemStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
                        <ItemTemplate>
                            <asp:Label ID="OriginalDate" runat="server" Text='<%#validateDate(Eval("OriginalDate"))%>'
                                Width="100px"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtOriginalDate" SkinID="Date" runat="server" Text='<%#validateDate(Eval("OriginalDate"))%>'
                                Width="100px" />
                            <asp:Label ID="imgCal" runat="server" SkinID="Calender" 
                                AlternateText="Calendar" />
                            <asp:RequiredFieldValidator ID="rqdOriginalDate" runat="server" ControlToValidate="txtOriginalDate"
                                Text="*" Display="Dynamic" ToolTip="Original Date is mandatory" ValidationGroup="Grid" />
                            <asp:RegularExpressionValidator ID="regOriginalDate" runat="server" ControlToValidate="txtOriginalDate"
                                ToolTip="Invalid Date" ValidationExpression="(0[1-9]|[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|[1-9]|1[012])[-/.](19\d\d|20\d\d|0[0-9])"
                                ValidationGroup="Grid">*</asp:RegularExpressionValidator>
                            <ajaxToolkit:CalendarExtender ID="calOriginalDate" runat="server" 
                                TargetControlID="txtOriginalDate" PopupButtonID="imgCal" CssClass="MyCalendar" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Revised Date" ItemStyle-Width="180px" ControlStyle-CssClass="form-inline" ItemStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
                        <ItemTemplate>
                            <asp:Label ID="NewDate" runat="server" Text='<%#validateDate(Eval("NewDate"))%>'
                                Width="100px"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNewDate" SkinID="Date" runat="server" Text='<%#validateDateForMakingEmpty(Eval("NewDate","{0:d}"))%>' Width="100px" />
                            <asp:Label ID="imgRevisedCal" runat="server" AlternateText="Calendar" SkinID="Calender"
                                />
                            <asp:RegularExpressionValidator ID="regRevisedDate" runat="server" ControlToValidate="txtNewDate"
                                ToolTip="Invalid Date" ValidationExpression="(0[1-9]|[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|[1-9]|1[012])[-/.](19\d\d|20\d\d|0[0-9])"
                                ValidationGroup="Grid">*</asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="cmpRevisedAndOriginalDates" runat="server" ControlToValidate="txtNewDate"
                                ControlToCompare="txtOriginalDate" ToolTip="Please ensure that the revised date is greater than the original date"
                                Display="Dynamic" Text="*" Operator="GreaterThan" Type="Date" ValidationGroup="Grid" />
                            <ajaxToolkit:CalendarExtender ID="calRevisedDate" runat="server" CssClass="MyCalendar"
                                 PopupButtonID="imgRevisedCal" TargetControlID="txtNewDate" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason for Change" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="250px"  ControlStyle-Width="250px" FooterStyle-Width="250px">
                        <ItemTemplate>
                            <asp:Label ID="lblChange" runat="server" Text='<%#Eval("Change")%>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtChange" runat="server" Text='<%#Bind("Change")%>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" SkinID="BtnLinkDelete"
                                 OnClientClick="return confirm('Do you really want to delete.')"
                                CommandName="Delete" />
                        </ItemTemplate>
                       
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
       
        <asp:ObjectDataSource ID="odsTask" runat="server" TypeName="Incidents.DAL.TaskHelper"
            DataObjectTypeName="Incidents.Entity.Task" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}"
            SelectMethod="LoadTasksById" UpdateMethod="Update">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="0" Name="id" SessionField="changeControlID" Type="Int32"
                    ConvertEmptyStringToNull="False" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:Panel>

    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 
</asp:Content>
