<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="controls_ResourceSearch" Codebehind="ResourceSearch.ascx.cs" %>
<div class="form-group">
        <div class="col-md-12">
           <strong>Search Resources  </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
    <div>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<tr>
<td colspan="9">
<div><asp:ValidationSummary ID="valsum" runat="server" ValidationGroup="Submit" />
    
    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFrom"
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                ValidationGroup="Submit" ErrorMessage="Please enter valid from date"></asp:RegularExpressionValidator>--%>
            <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtTo"
                ControlToValidate="txtFrom" Type="Date" Operator="LessThanEqual"
                ErrorMessage="To date must be greater than From date"
                SetFocusOnError="true" Display="None" ValidationGroup="Submit"></asp:CompareValidator><asp:RegularExpressionValidator ID="RgExpValFrom" runat="server" ControlToValidate="txtFrom"
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                ValidationGroup="Submit" ErrorMessage="Please enter valid from date"></asp:RegularExpressionValidator>
                <asp:CompareValidator
                    ID="CompareValidator3" runat="server" ControlToCompare="txtTo"
                    ControlToValidate="txtFrom" Type="Date" Operator="LessThanEqual"
                    ErrorMessage="From date must be lesser  than To date"
                    SetFocusOnError="true" Display="None" ValidationGroup="Submit"></asp:CompareValidator>--%> </div>
</td>
</tr>
    <tr>
        <td>
            Name
        </td>
        <td>
            <asp:TextBox ID="txtCriteria" runat="server" SkinID="txt_90"></asp:TextBox>
        </td>
        <%--<td>
            Level
        </td>
        <td>
            <asp:DropDownList ID="ddlLevel" runat="server" SkinID="ddl_90">
                <asp:ListItem Value="1" Text="Basic" Enabled="true" Selected="True"></asp:ListItem>
                <asp:ListItem Value="2" Text="Intermediate" Enabled="true"></asp:ListItem>
                <asp:ListItem Value="3" Text="Advanced" Enabled="true"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            From
        </td>
        <td>
            <asp:TextBox ID="txtFrom" runat="server" SkinID="Date"/>
           <asp:Label ID="Image1" runat="server" SkinID="Calender" ToolTip="Pick a date" />
                <ajaxToolkit:CalendarExtender ID="ClndrExtFrom" runat="server" TargetControlID="txtFrom"
                                         CssClass="MyCalendar" PopupButtonID="Image1">
                                    </ajaxToolkit:CalendarExtender>    
                                    
        </td>
        <td>
            To
        </td>
        <td>
            <asp:TextBox ID="txtTo" runat="server" SkinID="Date"></asp:TextBox>
            <asp:Label ID="Image2" runat="server" SkinID="Calender" ToolTip="Pick a date" />
            <ajaxToolkit:CalendarExtender ID="ClndrExtTo" runat="server" TargetControlID="txtTo"
                                         CssClass="MyCalendar" PopupButtonID="Image2">
                                    </ajaxToolkit:CalendarExtender>
        </td>--%>
        <td>
            <asp:Button ID="imgSearch" runat="server" SkinID="btnSearch" 
                onclick="imgSearch_Click" />
        </td>
    </tr>
    <tr>
    <td colspan ="9">
    <asp:ValidationSummary ID="valsumProjResr" runat="server" />
    <asp:Label ID="lblProjResErr" runat="server" Visible="false" EnableViewState="false" ForeColor="Red"></asp:Label>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Visible="False" ></asp:Label>
    </td>
    </tr>
    
</table>
</div>
<div class="clr"></div>
<div>
<asp:Panel ID="UserData" runat="server">
    
    <asp:GridView ID="grdResources" runat="server" AutoGenerateColumns="false" 
                                EmptyDataText="No resource exists" AllowPaging="True" 
                                onpageindexchanging="grdResources_PageIndexChanging" 
                                onrowcommand="grdResources_RowCommand" Width="100%">
    <Columns>
    <asp:TemplateField Visible="false">
        <ItemTemplate>
        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
        <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
        <ItemTemplate>
            <asp:CheckBox ID="chkRsrce" runat="server" Checked="false" />
        </ItemTemplate>
        <ItemStyle Width="40px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Resource">
        <ItemTemplate>
            <asp:Label ID="lblRsrceName" runat="server" Text='<%# Bind("contractorname")%>' Width="200px"></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField >
        <ItemTemplate>
        <asp:LinkButton ID="lnkView" runat="server" Text="View" CommandName="View" CommandArgument='<%# Eval("ID")%>' Visible="false" ></asp:LinkButton>
        <a onclick="javascript:window.open('projectresourceview.aspx?uid=<%#DataBinder.Eval(Container.DataItem,"ID")%>','mywindow','width=850,height=450,scrollbars=yes');" href="#" id="btnview"  >View</a>
        </ItemTemplate>
        <ItemStyle Width="40px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Active Projects">
            <ItemTemplate >
                <asp:Label ID="lblActProj" runat="server" Text='<%# Bind("NoLiveProjects")%>'></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pending Projects">
            <ItemTemplate>
                <asp:Label ID="lblPenProj" runat="server"  Text='<%# Bind("NoPendingProjects")%>'></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText= "Date when Available" ItemStyle-HorizontalAlign="Center">
        <ItemTemplate>
                <%# RetDate(Eval("DateAvailable").ToString())%>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Skills Summary" HeaderStyle-CssClass="header_bg_r">
            <ItemTemplate>
            <asp:Label ID="lblSkSummary" runat="server"  Text='<%# Bind("SkillList")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
    
    </asp:Panel>
</div>
<div class="clr"></div>
<asp:Panel ID="pnlProject" runat="server" >
    <table><tr>
        <td>Project</td>
        <td><asp:DropDownList ID="ddlProject" runat="server" SkinID="ddl_90" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList> </td>
        <td>Task </td>
        <td><asp:DropDownList ID="ddlTask" runat="server" SkinID="ddl_90"></asp:DropDownList> </td>
        <td><asp:Button ID="btnApply" runat="server" Text="Apply Resources to Task" 
                onclick="btnApply_Click" /> </td>
        </tr></table>
    </asp:Panel>

 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 
