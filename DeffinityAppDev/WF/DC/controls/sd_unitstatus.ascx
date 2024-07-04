<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Servicedesk_sdcontrols_sd_unitstatus" Codebehind="sd_unitstatus.ascx.cs" %>
<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong> Units Purchase History </strong>
            <hr class="no-top-margin" />
            </div>
</div>
<div class="form-group row">
        <div class="col-md-12">
             The following table displays a list of credit purchased in order of credit type
    (Monthly/Pre-Paid and Credit). The RAG status will change to amber if the expiry
    date reaches 75% of the remaining days and red if it reaches 10%”. Qty Wasted will
    be automatically updated and is dependant on the Expiry Date.
        </div>
    </div>
<asp:ValidationSummary ID="IncidentValidations" runat="server" ValidationGroup="Incident"
    Width="100%" />
<asp:ValidationSummary ID="IncidentGridValidations" runat="server" ValidationGroup="Grid"
    Width="100%" />


    <asp:GridView ID="GridView1" runat="server" Width="100%" OnRowCancelingEdit="GridView1_RowCancelingEdit"
        OnRowEditing="GridView1_RowEditing" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
        OnRowCommand="GridView1_RowCommand" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging"
        PageSize="10">
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Width="60px" />
                <ItemStyle Width="60px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="Edit" CommandArgument='<%# Bind("ID")%>'
                        SkinID="BtnLinkEdit" ToolTip="Edit"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update2" Text="Update"
                        CommandArgument='<%# Bind("ID")%>' ValidationGroup="Grid" SkinID="BtnLinkUpdate"
                        ToolTip="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                        SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="btnAdd" runat="server" SkinID="btnAdd" CommandName="Add" ValidationGroup="Incident" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Purchased" InsertVisible="False">
                <EditItemTemplate>
                    <asp:TextBox ID="txtDatePurchased" runat="server" SkinID="Date" Text='<%# Bind("DatePurchased", "{0:d}") %>'></asp:TextBox>
                    <asp:Label ID="imgDatePurchased" runat="server" SkinID="Calender" />
                    <ajaxToolkit:CalendarExtender ID="calDatePurchased" runat="server" CssClass="MyCalendar"
                         PopupButtonID="imgDatePurchased" TargetControlID="txtDatePurchased">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RegularExpressionValidator ID="regdate" runat="server" ControlToValidate="txtDatePurchased"
                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="Grid" Text="*" ErrorMessage="Please enter valid date"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblDatePurchased" runat="server" Text='<%# Bind("DatePurchased", "{0:d}") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtDatePurchased" runat="server" SkinID="Date"></asp:TextBox>&nbsp
                    <asp:Label ID="imgDatePurchased" runat="server" SkinID="Calender" />
                    <ajaxToolkit:CalendarExtender ID="calDatePurchased" runat="server" CssClass="MyCalendar"
                         PopupButtonID="imgDatePurchased" TargetControlID="txtDatePurchased">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RegularExpressionValidator ID="regpurchasedate" runat="server" ControlToValidate="txtDatePurchased"
                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="Incident" Text="*" ErrorMessage="Please enter valid date"></asp:RegularExpressionValidator>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Units Purchased">
                <EditItemTemplate>
                    <asp:TextBox ID="txtUnitPurchased" runat="server" Text='<%# Bind("UnitsPurchased","{0:F2}") %>'></asp:TextBox>
                    <asp:CompareValidator ID="cmpRate11" runat="server" ControlToValidate="txtUnitPurchased"
                        ErrorMessage="Units puchased  must be an integer" Operator="DataTypeCheck" SetFocusOnError="True"
                        Text="*" Type="Double" ValidationGroup="Grid" />
                </EditItemTemplate>
                
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:Label ID="lblUnitPurchased" runat="server" Text='<%# Bind("UnitsPurchased","{0:F2}") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtUnitPurchased" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="cmpRate11" runat="server" ControlToValidate="txtUnitPurchased"
                        ErrorMessage="Units puchased  must be an integer" Operator="DataTypeCheck" SetFocusOnError="True"
                        Text="*" Type="Double" ValidationGroup="Incident" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Category">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlUnitCategory" runat="server">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblUnitCategory" runat="server" Text='<%# Bind("UnitCategoryName") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlUnitCategory" runat="server" SkinID="ddl_80" OnSelectedIndexChanged="ddlUnitCategory_SelectedIndexChanged"
                        AutoPostBack="True" ValidationGroup="Incident">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rqdDDLunitcategory" runat="server" ControlToValidate="ddlUnitCategory"
                        Display="Dynamic" ErrorMessage="Please select Unit category" InitialValue="0"
                        Text="*" ValidationGroup="Incident" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Purchase Order Number">
                <EditItemTemplate>
                    <asp:TextBox ID="txtPurchaseOrderNo" runat="server" Text='<%# Bind("PurchaseOrderNo") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPurchaseOrderNo" runat="server" Text='<%# Bind("PurchaseOrderNo") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtPurchaseOrderNo" runat="server" Width="171px"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Expiry Date">
                <EditItemTemplate>
                    <asp:TextBox ID="txtExpiryDate" runat="server" SkinID="Date" Text='<%# Bind("ExpiryDate","{0:d}") %>'></asp:TextBox>
                    <asp:Label ID="imgExpiryDate" runat="server" SkinID="Calender" />
                    <ajaxToolkit:CalendarExtender ID="calExpiryDate" runat="server" CssClass="MyCalendar"
                         PopupButtonID="imgExpiryDate" TargetControlID="txtExpiryDate">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RegularExpressionValidator ID="regexpirydate" runat="server" ControlToValidate="txtExpiryDate"
                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="Grid" Text="*" ErrorMessage="Please enter valid Expiry date"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblExpiryDate" runat="server" Text='<%# Bind("ExpiryDate","{0:d}") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtExpiryDate" runat="server" SkinID="Date"></asp:TextBox>&nbsp
                    <asp:Label ID="imgExpiryDate" runat="server" SkinID="Calender" />
                    <ajaxToolkit:CalendarExtender ID="calExpiryDate" runat="server" CssClass="MyCalendar"
                         PopupButtonID="imgExpiryDate" TargetControlID="txtExpiryDate">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RegularExpressionValidator ID="regexdate" runat="server" ControlToValidate="txtExpiryDate"
                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="Incident" Text="*" ErrorMessage="Please enter valid Expiry date"></asp:RegularExpressionValidator>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Age RAG Status" ItemStyle-HorizontalAlign="Center">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlAgeRAGStatus" runat="server" SkinID="ddl_80">
                        <asp:ListItem>Green</asp:ListItem>
                        <asp:ListItem>Amber</asp:ListItem>
                        <asp:ListItem>Red</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="HID" runat="server" Value='<%# Eval("AgeRAGStatus") %>' />
                    <asp:Label ID="Image1" runat="server" />
                </ItemTemplate>
                <%--   <ItemTemplate>
                    <asp:Label ID="lblAgeRAGStatus" runat="server" Text='<%# Bind("AgeRAGStatus") %>'></asp:Label>
                </ItemTemplate>--%>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlAgeRAGStatus" runat="server" SkinID="ddl_80">
                        <asp:ListItem>Green</asp:ListItem>
                        <asp:ListItem>Amber</asp:ListItem>
                        <asp:ListItem>Red</asp:ListItem>
                    </asp:DropDownList>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty Used">
                <ItemStyle HorizontalAlign="Right" Width="70px" />
                <ItemTemplate>
                    <asp:Label ID="lblQtyUsed" runat="server" Text='<%# Bind("QtyUsed","{0:F2}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty Wasted">
                <%--<EditItemTemplate>
                    <asp:TextBox ID="txtQtyWasted" runat="server" Text='<%# Bind("QtyWasted","{0:F2}") %>'></asp:TextBox>
                    <asp:CompareValidator ID="cmpRate13" runat="server" ControlToValidate="txtQtyWasted"
                        ErrorMessage="Qty Wasted  must be an integer" Operator="DataTypeCheck" SetFocusOnError="True"
                        Text="*" Type="Double" ValidationGroup="Grid" />
                </EditItemTemplate>--%>
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:Label ID="lblQtyWasted" runat="server" Text='<%# Bind("QtyWasted","{0:F2}") %>'></asp:Label>
                </ItemTemplate>
                <%--<FooterTemplate>
                    <asp:TextBox ID="txtQtyWasted" runat="server" Width="100px"></asp:TextBox>
                    <asp:CompareValidator ID="cmpRate13" runat="server" ControlToValidate="txtQtyWasted"
                        ErrorMessage="Qty Wasted  must be an integer" Operator="DataTypeCheck" SetFocusOnError="True"
                        Text="*" Type="Double" ValidationGroup="Incident" />
                </FooterTemplate>--%>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                <HeaderStyle Width="40px" />
                <ItemTemplate>
                    <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete2"
                        SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="40px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong>  Chart</strong>
            <hr class="no-top-margin" />
            </div>
</div>
<div class="form-group row">
        <div class="col-md-12">
            The following chart displays the volume of units remaining based on expiry date.
    The RAG status will change according to the expiry date of the units where 25% of
    the days remaining will show as Amber and 10% as Red.
            </div>
    </div>

<div class="form-group row">
    <asp:Chart ID="Chart1" runat="server" Width="800px" Height="400px" ImageLocation="~/ChartImages/ChartPic_#SEQ(300,3)"
        ImageStorageMode="UseImageLocation">
        <Legends>
            <asp:Legend Name="DefaultLegend" Docking="Right" Font="Microsoft Sans Serif, 10pt"
                IsTextAutoFit="False">
                <Position Height="25.5714283" Width="90.0218582" X="70.97814" Y="60.0399437" />
            </asp:Legend>
        </Legends>
        <Series>
            <asp:Series Name="Series1" ChartType="Pie">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
</div>
<div class="form-group row">
<asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></div>

<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong> Bar Chart </strong>
            <hr class="no-top-margin" />
            </div>
</div>
<div class="form-group row">
        <div class="col-md-12">
             The following chart shows the number of units consumed for completed requests between
    a given date range. The bars will show weekly consumption.
            </div>
    </div>

<asp:Panel ID="pnlsearch" runat="server" CssClass="form-group row">
    <div class="col-md-12 form-inline">
      <label>  From Date</label>
        <asp:TextBox ID="txtFromaDate" runat="server" SkinID="Date"></asp:TextBox>
        <asp:Label ID="imgFromDate" runat="server"  SkinID="Calender" />
        <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgFromDate" TargetControlID="txtFromaDate">
        </ajaxToolkit:CalendarExtender>
        <label> To Date</label>
        <asp:TextBox ID="txtToDate" runat="server" SkinID="Date"></asp:TextBox>
        <asp:Label ID="imgToDate" runat="server" SkinID="Calender" />
        <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgToDate" TargetControlID="txtToDate">
        </ajaxToolkit:CalendarExtender>
        <asp:Button ID="btnSearch" runat="server" SkinID="btnSearch" OnClick="btnSearch_Click" />
    </div>
</asp:Panel>

<div class="form-group row">
    <asp:Label ID="lblmsb" runat="server" Text=""></asp:Label></div>
<div class="form-group row">
    <asp:Chart ID="Chart2" runat="server" ImageLocation="~/ChartImages/ChartPic_#SEQ(300,3)"
        ImageStorageMode="UseImageLocation">
        <Legends>
            <asp:Legend Name="DefaultLegend" Alignment="Near" Docking="Right" />
        </Legends>
        <Series>
            <asp:Series Name="MaxTemp" LegendText="Units Purchased">
            </asp:Series>
            <asp:Series Name="MinTemp" Color="Brown" LegendText="Qty Used">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
</div>
