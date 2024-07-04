<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" ValidateRequest="false" MaintainScrollPositionOnPostback="true"
     Inherits="AddMeeting" Title="Untitled Page" Codebehind="AddMeeting.aspx.cs" %>
<%--<%@ Register Src="~/MailControls/ProjectUpdateMail.ascx" TagName="ProjectUpdateMail" TagPrefix="PUM">--%>
<%@ Register Src="controls/ProjectRef.ascx" TagName="ProjectRef" TagPrefix="uc2" %>
<%@ Register Src="MailControls/ProjectUpdateMail.ascx" TagName="ProjectUpdate" TagPrefix="uc4" %>
 
<%@ Register src="controls/ProjectTabs.ascx" tagname="ProjectTabs" tagprefix="uc1" %>
<%@ Register assembly="Infragistics2.WebUI.WebHtmlEditor.v7.2" namespace="Infragistics.WebUI.WebHtmlEditor" tagprefix="ighedit" %>
<%@ Register src="controls/HtmlEditor.ascx" tagname="HtmlEditor" tagprefix="uc3" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
   <%-- <PUM:ProjectUpdateMail ID="MailCnt" runat="server" Visible="false"></PUM:ProjectUpdateMail>--%>
   <uc4:ProjectUpdate runat="server" ID="MailCnt" Visible="false" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.ProjectUpdate%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <!--validations-->
<script type="text/javascript">
<!--

    var xPos, yPos;
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);
    function BeginRequestHandler(sender, args) {
        xPos = $get('<%=Panel2.ClientID %>').scrollLeft;
        yPos = $get('<%=Panel2.ClientID %>').scrollTop;
        
    }
    function EndRequestHandler(sender, args) {
        $get('<%=Panel2.ClientID %>').scrollLeft = xPos;
        $get('<%=Panel2.ClientID %>').scrollTop = yPos;
         
    }
 -->
 </script>
 
<div class="form-group"><asp:ValidationSummary ID="summery" runat="server" ValidationGroup="Meeting" />
<asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="GridValid" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" /></div>

    <div class="form-group form-inline">
        <div class="col-md-12 form-inline">
                                  <div class="col-md-4">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Date%></label>
                                      <div class="col-sm-8 form-inline"> <asp:TextBox ID="txtDate" runat="server" SkinID="Date"></asp:TextBox><asp:Label ID="img1" runat="server" SkinID="Calender" />
					</div>
				</div>
 <div class="col-md-4">
             <label class="col-sm-4  control-label"> <%= Resources.DeffinityRes.Time%></label>
             <div class="col-sm-8 form-inline">
                 <asp:TextBox ID="txtTime" runat="server" SkinID="txt_75px"></asp:TextBox>
                 <span style="color: Gray">(HH:MM)</span>
             </div>
     </div>
     <div  class="col-md-4"> <asp:CheckBox id="chkVisibletoCustomer" runat="server" Text="Visible to Customer" /></div>
				
            </div>
</div>
    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8">
                                           <asp:TextBox Visible="false" ID="txtAttendees" runat="server" Width="400px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAttendees"
        ErrorMessage="please enter Attendees" ValidationGroup="Meeting" Display="None"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="cmpTime" runat="server" ControlToValidate="txtTime"
                        Display="None" ErrorMessage="Plese enter valid Time" ValidationExpression="^(\d{2}):(\d{2})"
                        ValidationGroup="Meeting" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTime"
            ErrorMessage="Please enter Time" ValidationGroup="Meeting" Display="None"></asp:RequiredFieldValidator>
    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Location" ControlToValidate="txtLocation" ValidationGroup="Meeting" Display="None"></asp:RequiredFieldValidator>--%>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Date" ControlToValidate="txtDate" ValidationGroup="Meeting" Display="None"></asp:RequiredFieldValidator>
					</div>
				</div>

</div>
    <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.ProjectPlan%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
 <div class="form-group form-group-no-margin">
                                  <div class="col-md-4 pull-right no-top-margin">
                                       <div class="col-sm-4 control-label form-inline"> <label id="lblRagStatus" runat="server"></label>&nbsp;</div>
                                      <div class="col-sm-8 form-inline pull-right"> RAG&nbsp;&nbsp; <asp:DropDownList ID="ddlRagStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRagStatus_SelectedIndexChanged" SkinID="ddl_50">
    
    </asp:DropDownList>
					</div>
				</div>
     </div>
     <div class="form-group">
                                  <div class="col-md-12">
    <asp:Panel id="Panel2" runat="server" Width="100%" Height="380px" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="Vertical">
    <asp:GridView ID="GridTasks" runat="server" Width="98%" DataKeyNames="ID" OnRowDataBound="GridTasks_RowDataBound" OnRowEditing="GridTasks_RowEditing" OnRowUpdating="GridTasks_RowUpdating" OnRowCancelingEdit="GridTasks_RowCancelingEdit">
    <Columns>
    <asp:TemplateField HeaderStyle-CssClass="header_bg_l">  
              <HeaderStyle Width="60px" />
                <ItemStyle  Width="60px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" Enabled="<%#CommandField()%>" CommandName="Edit" SkinID="BtnLinkEdit" ToolTip="Edit" ></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update" ValidationGroup="GridValid" SkinID="BtnLinkUpdate" ToolTip="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel" SkinID="BtnLinkCancel" ToolTip="Cancel" ></asp:LinkButton>
                </EditItemTemplate>
              
            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="ID"
                                 InsertVisible="False" ReadOnly="True" SortExpression="ID" Visible="false"/>
                            <asp:TemplateField HeaderText="Task">
                            <ItemTemplate>
                             <asp:Label ID="lblTask" runat="server" 
                                  Text='<%#getItemDes(DataBinder.Eval(Container.DataItem, "IndentLevel").ToString(),DataBinder.Eval(Container.DataItem, "ItemDescription").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:TextBox ID="txtTask" runat="server" TextMode="MultiLine" Height="50px" Text='<%# Bind("ItemDescription") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="Please enter Task" ControlToValidate="txtTask" Display="None" ValidationGroup="GridValid"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText="Assign to">
                            <ItemTemplate>
                            <asp:Label ID="lblAssignTo" runat="server"  Text='<%# lblResource(DataBinder.Eval(Container.DataItem, "Resources").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>  
                             <asp:TemplateField  HeaderText="Start Date">
                <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("ProjectStartDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>                    
                    <EditItemTemplate>
                        <asp:TextBox ID="txtStartDate" runat="server" MaxLength="10" SkinID="Date" Text='<%# Bind("ProjectStartDate","{0:d}") %>' ></asp:TextBox>
                         <asp:Label ID="Image4" SkinID="Calender" runat="server"></asp:Label>
                        <ajaxToolkit:calendarextender id="CalendarExtender4" runat="server"  popupbuttonid="Image4"
                           targetcontrolid="txtStartDate" CssClass="MyCalendar"></ajaxToolkit:calendarextender>
                        <asp:RequiredFieldValidator ID="sdateval1" runat="server" ErrorMessage="Please enter Start date" ControlToValidate="txtStartDate"
                             ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="Sdateval2" runat="server" ControlToValidate="txtStartDate" ErrorMessage="Please enter valid Start date"
                             Operator="DataTypeCheck" Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                       
                 </EditItemTemplate> 
                 <ItemStyle  HorizontalAlign="Center" Width="80px"/>               
                </asp:TemplateField>                
                <asp:TemplateField  HeaderText="End Date">                  
                <ItemTemplate>
                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("ProjectEndDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                   
                        <asp:TextBox ID="txtEndDate" runat="server" SkinID="Date" MaxLength="10" Text='<%# Bind("ProjectEndDate","{0:d}") %>'  ></asp:TextBox>
                       
                        <asp:Label ID="Image3" runat="server" SkinID="Calender"  />
                        <ajaxToolkit:calendarextender id="CalendarExtender3" runat="server"  popupbuttonid="Image3"
                           targetcontrolid="txtEndDate" CssClass="MyCalendar"></ajaxToolkit:calendarextender>
                        <asp:RequiredFieldValidator ID="Edateval1" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtEndDate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="Edateval2" runat="server" ControlToValidate="txtEndDate" ErrorMessage="Please enter valid end date" Operator="DataTypeCheck" Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                       
                    </EditItemTemplate>
                               
                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                    </asp:TemplateField>                                                      
                             <asp:TemplateField  HeaderText="Notes">
                            <ItemTemplate>
                            <asp:Label ID="lblNotes" runat="server"  Text='<%# Bind("Notes") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="header_bg_r">
                            <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"  Text='<%# Bind("ItemStatus1") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                            </EditItemTemplate>
                            </asp:TemplateField>                           
                            </Columns>   
    
    <EmptyDataTemplate>
    No Task items exists.
    </EmptyDataTemplate>
    </asp:GridView>
    </asp:Panel>

   
</div>
         </div>


<!--TaskGrid-->

<!--Raise Issue-->

<div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.ProjectUpdate%></label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtGeneralNotes" runat="server" TextMode="MultiLine" Height="70px"></asp:TextBox>
					</div>
				</div>
 
</div>
<div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.LessonsLearnt%></label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtLessonLearnt" runat="server" TextMode="MultiLine" Height="70px"></asp:TextBox>
					</div>
				</div>
 
</div>
        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Keyachievementslastweek%></label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtKeyAchievements" runat="server" Height="70px" TextMode="MultiLine" ></asp:TextBox>
					</div>
				</div>
 
</div>
        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> Key Tasks this week</label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtKeyTasks" runat="server" Height="70px" TextMode="MultiLine"></asp:TextBox>
					</div>
				</div>
 
</div>
        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.RAGStatus%></label>
                                      <div class="col-sm-9"><asp:DropDownList id="ddlRagAlert" runat="server" SkinID="ddl_50" ValidationGroup="Submit">
                           <asp:ListItem Text="AMBER" Value="3"></asp:ListItem>
                          <asp:ListItem Text="GREEN" Value="1"></asp:ListItem>
                          <asp:ListItem Text="RED" Value="2"></asp:ListItem>
                                          </asp:DropDownList>
					</div>
				</div>
 
</div>
        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:Button ID="btnAdd" runat="server" SkinID="btnDefault"
                                               Text="Add Update" ToolTip="Add Meeting" OnClick="btnAdd_Click"
                                               ValidationGroup="Meeting" />

<asp:Button ID="btnCancel" runat="server" SkinID="btnCancel" ToolTip="Cancel" OnClick="btnCancel_Click" />
					</div>
				</div>
 
</div>

<div>

<ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"  PopupButtonID="img1" TargetControlID="txtDate" CssClass="MyCalendar">
                                                    </ajaxToolkit:CalendarExtender>
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

