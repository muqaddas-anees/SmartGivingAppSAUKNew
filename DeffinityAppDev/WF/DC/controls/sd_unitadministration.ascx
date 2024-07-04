<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Servicedesk_sdcontrols_sd_unitadministration" Codebehind="sd_unitadministration.ascx.cs" %>
<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong>  Unit Consumption Configuration </strong>
            <hr class="no-top-margin" />
            </div>
</div>
<div class="form-group row">
        <div class="col-md-12">
             The following table shows automatic unit consumption based on time. The system will
    allocate units to a service request ticket automatically if the time a ticket is
    raised falls into one of the definitions here. Automatic assigning will stop when
    the ticket falls out-side of scope and will resume from the beginning of the defined
    range. It will also stop if the ticket is placed on hold.
        </div>
    </div>

<asp:ValidationSummary ID="IncidentValidations" runat="server" ValidationGroup="Incident"
    Width="100%" />
<asp:Label ID="lblerrormsg" runat="server" Text="" ForeColor="Red"></asp:Label>
<div style="width: 100%">
    <asp:GridView ID="gvunitconsumption" runat="server" Width="100%" AutoGenerateColumns="False"
        OnRowCommand="gvunitconsumption_RowCommand" OnRowDataBound="gvunitconsumption_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:Label ID="lblUnitConsumption" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="btnConsumptionAdd" runat="server" SkinID="BtnLinkAdd" CommandName="Add"
                        ValidationGroup="Incident" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SR Type">
                <ItemTemplate>
                    <asp:Label ID="lblSRType" runat="server" Text='<%# Bind("SRType") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlSRType" runat="server" Width="120px">
                     <asp:ListItem Text="Change Control" Value="Change Control" />
                        <asp:ListItem Text="Fault" Value="Fault" />
                         <asp:ListItem Text="FLS" Value="FLS" />
                          <asp:ListItem Text="Service Request" Value="Service Request" />
                    </asp:DropDownList>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type Of Hours">
                <ItemTemplate>
                    <asp:Label ID="lblTypeOfHours" runat="server" Text='<%# Bind("TypeOfHours") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtTypeOfHours" runat="server" Width="200px"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="From Day">
                <ItemTemplate>
                    <asp:Label ID="lblFromDay" runat="server" Text='<%# Bind("FromDay") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlFromDay" runat="server" Width="100px">
                        <%-- <asp:ListItem Text="Please select.." Value="0"></asp:ListItem>--%>
                        <asp:ListItem>Sunday</asp:ListItem>
                        <asp:ListItem>Monday</asp:ListItem>
                        <asp:ListItem>Tuesday</asp:ListItem>
                        <asp:ListItem>Wednesday</asp:ListItem>
                        <asp:ListItem>Thursday</asp:ListItem>
                        <asp:ListItem>Friday</asp:ListItem>
                        <asp:ListItem>Saturday</asp:ListItem>
                    </asp:DropDownList>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To Day">
                <ItemTemplate>
                    <asp:Label ID="lblToDay" runat="server" Text='<%# Bind("ToDay") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlToDay" runat="server" Width="100px">
                        <%--<asp:ListItem Text="Please select..." Value="0"></asp:ListItem>--%>
                        <asp:ListItem>Sunday</asp:ListItem>
                        <asp:ListItem>Monday</asp:ListItem>
                        <asp:ListItem>Tuesday</asp:ListItem>
                        <asp:ListItem>Wednesday</asp:ListItem>
                        <asp:ListItem>Thursday</asp:ListItem>
                        <asp:ListItem>Friday</asp:ListItem>
                        <asp:ListItem>Saturday</asp:ListItem>
                         
                    </asp:DropDownList>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="From Time">
                <HeaderStyle Width="100px" />
                <ItemStyle Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblFromTime" runat="server" Text='<%# Bind("FromTime","{0:hh}:{0:mm}") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtFromTime" runat="server" Width="50px" Text="00:00"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="rgeTimeJournal" ControlToValidate="txtFromTime"
                        runat="server" ValidationExpression="^(\d{2}):(\d{2})" ValidationGroup="Incident"
                        Display="Dynamic" ErrorMessage="Please enter valid From Time" Text="*" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To Time">
                <HeaderStyle Width="100px" />
                <ItemStyle Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblToTime" runat="server" Text='<%# Bind("ToTime","{0:hh}:{0:mm}") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtToTime" runat="server" Width="50px" Text="00:00"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="rgeTime" ControlToValidate="txtToTime" runat="server"
                        ValidationExpression="^(\d{2}):(\d{2})" ValidationGroup="Incident" Display="Dynamic"
                        ErrorMessage="Please enter valid To Time" Text="*" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Auto Units Allocated">
            <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:Label ID="lblAutoUnitsAllocated" runat="server" Text='<%# Bind("AutoUnitsAllocated") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAutoUnitsAllocated" runat="server" Width="60px"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Per Unit Of Time">
                <ItemTemplate>
                    <asp:Label ID="lblPerUnitOfTime" runat="server" Text='<%# Bind("PerUnitOfTime") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlPerUnitOfTime" runat="server" Width="100px">
                        <asp:ListItem>Minutes</asp:ListItem>
                        <asp:ListItem>Hours</asp:ListItem>
                        <asp:ListItem>Day</asp:ListItem>
                    </asp:DropDownList>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Include Public Holiday">
                <ItemTemplate>
                    <asp:Label ID="lblIncludePublicHoliday" runat="server" Text='<%# Bind("IncludePublicHoliday") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:CheckBox ID="chkIncludePublicHoliday" runat="server" Text="Include Public Holiday" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                <HeaderStyle Width="70px" />
                <ItemTemplate>
                    <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete2"
                        SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="70px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

<br />
<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong> Minimum Units Assigned Per Call </strong>
            <hr class="no-top-margin" />
            </div>
</div>
<div class="form-group row">
        <div class="col-md-12">
             The following field will allocate a minimum number of units per call if the ticket
    falls outside a defined range in the Automatic Unit Consumption section or if the
    automatic units allocated is below the threshold set here.
        </div>
    </div>

<table style="width: 100%;">
    <tr>
        <td style="width: 18%;">
            Minimum Units Assigned Per Call
        </td>
        <td style="width: 15%;">
            <asp:TextBox ID="txtMinUnits" runat="server"></asp:TextBox>
        </td>
        <td style="width: 67%;">
            <asp:Button ID="btnMinUnitsave" runat="server" SkinID="btnSave" OnClick="btnMinUnitsave_Click" />
        </td>
    </tr>
</table>
<br />
<br />
<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong> Unit Expiry Period</strong>
            <hr class="no-top-margin" />
            </div>
</div>
<div class="form-group row">
        <div class="col-md-12">
             This section will automatically set the expiry date for purchased units based on
    their type.
        </div>
    </div>


    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
        OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="">
                <HeaderStyle Width="60px" />
                <ItemStyle Width="60px" />
                <ItemTemplate>
                    <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="BtnLinkAdd" CommandName="Add" />
                </FooterTemplate>
                <HeaderStyle CssClass="header_bg_l" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Category">
                <ItemTemplate>
                    <asp:Label ID="lblUnitCategory" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlUnitCategory" runat="server" Width="126px">
                    </asp:DropDownList>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Set Expiry Period to">
                <ItemTemplate>
                    <asp:Label ID="lblsetexpiryperiod" runat="server" Text='<%# Bind("PeriodType") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlSetExpiryPeriod" runat="server" Width="126px">
                        <asp:ListItem>Month</asp:ListItem>
                        <asp:ListItem>Days</asp:ListItem>
                    </asp:DropDownList>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Period">
                <ItemTemplate>
                    <asp:Label ID="lblperiod" runat="server" Text='<%# Bind("Period") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtPeriod" runat="server" Width="171px"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                <HeaderStyle Width="70px" />
                <ItemTemplate>
                    <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete1"
                        SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="70px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

<div class="clr">
    &nbsp</div>
<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong> Unit Consumption RAG Alerts</strong>
            <hr class="no-top-margin" />
            </div>
</div>
<div class="form-group row">
        <div class="col-md-12">
             This section allows you to define users that will receive alerts when the total
    number of active units purchased drops below the specified volumes set here.
        </div>
    </div>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="IncidentRAG"
    Width="100%" />
<table style="width: 100%;" class="table table-small-font table-bordered table-striped dataTable">
    <tr>
        <td style="width: 10%;">
            &nbsp;
        </td>
        <td style="width: 25%">
          <strong>  Remaining Units </strong>
        </td>
        <td style="width: 65%">
            <strong> Email Distribution </strong> (Eg: example1@gmail.com,example2@gmail.com,...)
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="Image2" runat="server" SkinID="Green_circle" />
        </td>
        <td>
            <asp:TextBox ID="txtRemainingUnit1" runat="server" SkinID="Price_100px"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtEmail4" runat="server" SkinID="txt_90"></asp:TextBox>&nbsp
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtEmail4"
                Text="*" ErrorMessage="Invalid Email Address" ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                runat="server" ValidationGroup="IncidentRAG" />
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="Image3" runat="server" SkinID="Amber_circle"/>
        </td>
        <td>
            <asp:TextBox ID="txtRemainingUnit2" runat="server" SkinID="Price_100px"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtEmail5" runat="server" SkinID="txt_90"></asp:TextBox>&nbsp
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtEmail5"
                Text="*" ErrorMessage="Invalid Email Address" ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                runat="server" ValidationGroup="IncidentRAG" />
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="Image4" runat="server" SkinID="Red_circle" />
        </td>
        <td>
            <asp:TextBox ID="txtRemainingUnit3" runat="server" SkinID="Price_100px"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtEmail6" runat="server" SkinID="txt_90"></asp:TextBox>&nbsp
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtEmail6"
                Text="*" ErrorMessage="Invalid Email Address" ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                runat="server" ValidationGroup="IncidentRAG" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Button ID="imgbtnRAGAlertSave" runat="server" SkinID="btnSave" OnClick="imgbtnRAGAlertSave_Click"
                ValidationGroup="IncidentRAG" />
        </td>
    </tr>
</table>

