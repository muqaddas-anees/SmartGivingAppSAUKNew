<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Training_controls_ManageUserSkillsCtrl" Codebehind="ManageUserSkillsCtrl.ascx.cs" %>
    <asp:Panel ID="pnlUser" runat="server">
         <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Users%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlUser" runat="server" Width="200px" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged"
        AutoPostBack="true">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvUser1" runat="server" ControlToValidate="ddlUser"
        InitialValue="0" ErrorMessage="Please select user" ValidationGroup="Group2">*</asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="rfvUser2" runat="server" ErrorMessage="Please select user"
        ControlToValidate="ddlUser" InitialValue="0" ValidationGroup="gu">*</asp:RequiredFieldValidator>
            </div>
	</div>
    
    </asp:Panel>
   <div class="form-group">
        <div class="col-md-12">
           <strong>Training Booking Record</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
     <div class="form-group">
        <div class="col-md-12">
    <asp:ValidationSummary ID="validSummryEdit" runat="server" ValidationGroup="Group1"
        ShowSummary="true" />
    <asp:ValidationSummary ID="validSummryAddNew" runat="server" ValidationGroup="Group2"
        ShowSummary="true" />
    <asp:ValidationSummary ID="validSummryUpdate" runat="server" ValidationGroup="Group3"
        ShowSummary="true" />
            </div>
         </div>
        <div style="width: 100%">
    <asp:GridView ID="gvTrainingBooking" runat="server" Width="100%" AllowPaging="true"
        PageSize="20" OnRowDataBound="gvTrainingBooking_RowDataBound" OnRowCommand="gvTrainingBooking_RowCommand"
        OnRowEditing="gvTrainingBooking_RowEditing" OnRowUpdating="gvTrainingBooking_RowUpdating"
        OnRowCancelingEdit="gvTrainingBooking_RowCancelingEdit" OnRowDeleting="gvTrainingBooking_RowDeleting"
        OnPageIndexChanging="gvTrainingBooking_PageIndexChanging">
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Width="45px" />
                <ItemStyle Width="45px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                        CommandArgument='<%#Bind("ID")%>' ToolTip="Edit" SkinID="BtnLinkEdit" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                        CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate" CausesValidation="true"
                        ValidationGroup="Group1" ToolTip="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                        SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="LinkButtonAddNew" runat="server" CommandName="AddNew" CommandArgument="<%# Bind('ID')%>"
                        SkinID="BtnLinkUpdate" CausesValidation="true" ValidationGroup="Group2"
                        ToolTip="AddNew"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonCancel1" runat="server" CausesValidation="false" CommandName="Clear"
                        SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Booked">
                 <ItemStyle Width="120px" CssClass="form-inline" />
                <FooterStyle Width="120px" CssClass="form-inline" />
                <ControlStyle CssClass="form-inline" />
                <EditItemTemplate>
                    <asp:TextBox Text='<%#Bind("BookedDate","{0:d}") %>' ID="txtDate" runat="server"
                        SkinID="Date">
                    </asp:TextBox>
                    <asp:Label ID="imgDate" runat="server" SkinID="Calender" />
                    <ajaxToolkit:CalendarExtender ID="cldExtender1"  CssClass="MyCalendar"
                        TargetControlID="txtDate" PopupButtonID="imgDate" runat="server">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RV1" runat="server" ErrorMessage="Please enter date"
                        ControlToValidate="txtDate" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="REV1" runat="server" ErrorMessage="Please enter valid date"
                        ControlToValidate="txtDate" ValidationGroup="Group1" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        Display="None">
                    </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblDateOfCourse" runat="server" Text='<%#Bind("BookedDate","{0:d}") %>'
                        Width="75px"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtDateFooter" runat="server" SkinID="Date"></asp:TextBox>
                    <asp:Label ID="imgDateFooter" runat="server" SkinID="Calender" />
                    <ajaxToolkit:CalendarExtender ID="cldExtenderFooter" PopupButtonID="imgDateFooter"
                        TargetControlID="txtDateFooter"  CssClass="MyCalendar" runat="server">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RVFooter" runat="server" ErrorMessage="Please enter date"
                        ControlToValidate="txtDateFooter" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RExpFooter" runat="server" ErrorMessage="Please Enter valid date"
                        ControlToValidate="txtDateFooter" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="Group2" Display="None"></asp:RegularExpressionValidator>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Completed">
                <ItemStyle Width="120px" CssClass="form-inline" />
                <FooterStyle Width="120px" CssClass="form-inline" />
                <ControlStyle CssClass="form-inline" />
                <EditItemTemplate>
                    <asp:TextBox Text='<%#Bind("CompletedDate") %>' ID="txtCompletedDate" runat="server"
                        SkinID="Date">
                    </asp:TextBox>
                    <asp:Label ID="imgDateCompleted" runat="server" SkinID="Calender" />
                    <ajaxToolkit:CalendarExtender ID="cldCompltedDate"  CssClass="MyCalendar"
                        TargetControlID="txtCompletedDate" PopupButtonID="imgDateCompleted" runat="server">
                    </ajaxToolkit:CalendarExtender>
                 <%--   <asp:RequiredFieldValidator ID="RVCom1" runat="server" ErrorMessage="Please enter completed date"
                        ControlToValidate="txtCompletedDate" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator ID="REVCom1" runat="server" ErrorMessage="Please enter valid date"
                        ControlToValidate="txtCompletedDate" ValidationGroup="Group1" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        Display="None">
                    </asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblCompleted" runat="server" Text='<%#Bind("CompletedDate") %>'
                        Width="75px"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtfCompletedDate" runat="server" SkinID="Date"></asp:TextBox>
                    <asp:Label ID="imgfCompletedDate" runat="server" SkinID="Calender" />
                    <ajaxToolkit:CalendarExtender ID="cldCompletedFooter" PopupButtonID="imgfCompletedDate"
                        TargetControlID="txtfCompletedDate"  CssClass="MyCalendar"
                        runat="server">
                    </ajaxToolkit:CalendarExtender>
                   <%-- <asp:RequiredFieldValidator ID="RVComFooter" runat="server" ErrorMessage="Please enter completed date"
                        ControlToValidate="txtfCompletedDate" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator ID="RExpCompletedDate" runat="server" ErrorMessage="Please enter valid date"
                        ControlToValidate="txtfCompletedDate" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="Group2" Display="None"></asp:RegularExpressionValidator>
                </FooterTemplate>
                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Course Category">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="140px" />
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlCategory" runat="server" Width="150px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please select category"
                        InitialValue="0" ControlToValidate="ddlCategory" Display="None" ValidationGroup="Group1">
                    </asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblCategory" runat="server" Width="140px" Text='<%# Bind("Category") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlCategory_footer" ValidationGroup="Group2" runat="server"
                        Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_footer_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please select category"
                        ControlToValidate="ddlCategory_footer" InitialValue="0" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                </FooterTemplate>
                <FooterStyle Width="140px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Course">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="450px" />
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlCourse" runat="server" Width="450px">
                    </asp:DropDownList>
                  
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="Group1"
                        ErrorMessage="Please select course" Display="None" InitialValue="0" ControlToValidate="ddlCourse"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblCourse" runat="server" Width="250px" Text='<%# Bind("Course") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlCourse_footer" runat="server" Width="450px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please select course"
                        Display="None" ControlToValidate="ddlCourse_footer" ValidationGroup="Group2"
                        InitialValue="0"></asp:RequiredFieldValidator>
                </FooterTemplate>
                <FooterStyle Width="270px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="110px" />
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="110px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please select status"
                        InitialValue="0" ControlToValidate="ddlStatus" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server" Width="110px" Text='<%# Bind("Status") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlStatus_footer" ValidationGroup="Group2" runat="server" Width="110px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                        InitialValue="0" ErrorMessage="Please select status" ControlToValidate="ddlStatus_footer"
                        ValidationGroup="Group2"></asp:RequiredFieldValidator>
                </FooterTemplate>
                <FooterStyle Width="110px" />
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="15px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:LinkButton ID="imgDeletebooking" runat="server" CausesValidation="false" CommandName="Delete"
                        SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                </ItemTemplate>
                <FooterStyle Width="45px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
   
    <br />
<div class="form-group">
        <div class="col-md-12">
           <strong>Other Skills</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
    <div class="form-group">
        <div class="col-md-12">
            <asp:ValidationSummary ID="valUser" runat="server" ValidationGroup="gu" ShowSummary="true" />
            </div>
        </div>
    
    
    <asp:GridView ID="gvUserSkills" runat="server" ShowFooter="True" DataKeyNames="ID"
        AllowPaging="True" EmptyDataText="No Skills/Training exist with this user" OnRowCancelingEdit="gvUserSkills_RowCancelingEdit"
        OnRowCommand="gvUserSkills_RowCommand" OnRowDataBound="gvUserSkills_RowDataBound"
        OnRowDeleting="gvUserSkills_RowDeleting" OnRowEditing="gvUserSkills_RowEditing"
        OnRowUpdating="gvUserSkills_RowUpdating">
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="header_bg_l" ItemStyle-CssClass="100px">
                <ItemTemplate>
                    <asp:LinkButton ID="btnedit" runat="server" CausesValidation="false" CommandName="Edit"
                        SkinID="BtnLinkEdit" ToolTip="Edit" />
                    <asp:Label ID="lblID1" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="btnupdate" runat="server" CommandName="Update" SkinID="BtnLinkUpdate"
                        CommandArgument='<%# Bind("Id") %> ' ToolTip="Update" ValidationGroup="ValEdit" />
                    <asp:LinkButton ID="btncancel" runat="server" CausesValidation="false" CommandName="cancel"
                        SkinID="BtnLinkCancel" ToolTip="Cancel" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Skills" SortExpression="Skills">
                <ItemTemplate>
                    <asp:Label ID="lblSkills" runat="server" Text='<%# Bind("Skills") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtSkills" SkinID="txt_90" runat="server" Text='<%# Bind("Skills") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtfSkills" SkinID="txt_90" runat="server" Text='<%# Bind("Skills") %>'></asp:TextBox>
                </FooterTemplate>
                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Level" SortExpression="SkillLevel">
                <ItemTemplate>
                    <asp:Label ID="lblSkillLevel" runat="server" Text='<%# Bind("SkillLevel") %>' Width="100px"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlSkillLevel" runat="server">
                        <asp:ListItem Value="1" Text="Basic"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Intermediate"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Advanced"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="hfSkillLevel" runat="server" Value='<%# Eval("SkillLevel") %>' />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlfSkillLevel" runat="server">
                        <asp:ListItem Value="1" Text="Basic" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Intermediate"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Advanced"></asp:ListItem>
                    </asp:DropDownList>
                </FooterTemplate>
                <ItemStyle Width="100px" />
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes" SortExpression="Notes">
                <ItemTemplate>
                    <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes")%>' Width="200px"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNotes" runat="server" Text='<%# Bind("Notes") %>' SkinID="txt_90"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtfNotes" runat="server" Text='<%# Bind("Notes") %>' SkinID="txt_90"></asp:TextBox>
                </FooterTemplate>
               
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="15px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:LinkButton  ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete"
                        SkinID="BtnLinkDelete" CommandArgument='<%# Bind("Id") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton CommandName="InsertSkills" ID="ImageButton5" runat="server" SkinID="BtnLinkAdd"
                        ValidationGroup="gu" />
                    <asp:LinkButton CommandName="Insert_Empty" ID="ImageButton20" runat="server" SkinID="BtnLinkCancel" />
                </FooterTemplate>
                <FooterStyle Width="45px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

