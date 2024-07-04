<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" Inherits="SDFieldsConfig" MaintainScrollPositionOnPostback="true" Codebehind="SDFieldsConfig.aspx.cs" %>

<%@ Register Src="~/WF/CustomerAdmin/controls/PortfolioDdlCtr.ascx" TagName="PortfolioDdlCtr" TagPrefix="uc2" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
    Configure Fields  <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" Visible="false" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   
  
    <script type="text/javascript">
        $(document).ready(function () {
            $("#AlignmentOptions").click(function () {
                $("#DivAlignmentOptions").toggle();
            });
        });
        function pageLoad(sender, args) {
            loadConfigField();
        }
    function loadConfigField() {
        $("#btn").click(function () {

            var listbox = document.getElementById("gridlist");
            var selIndex = listbox.selectedIndex;
            if (-1 == selIndex) { alert("Please select an option to move."); return; }
            var increment = -1;
            if ((selIndex + increment) < 0 || (selIndex + increment) > (listbox.options.length - 1)) { return; }

            var selValue = listbox.options[selIndex].value;
            var selText = listbox.options[selIndex].text;
            listbox.options[selIndex].value = listbox.options[selIndex + increment].value;
            listbox.options[selIndex].text = listbox.options[selIndex + increment].text;
            listbox.options[selIndex + increment].value = selValue;
            listbox.options[selIndex + increment].text = selText;
            listbox.selectedIndex = selIndex + increment;
        });
        $("#Btn2").click(function () {

            var listbox = document.getElementById("gridlist");
            var selIndex = listbox.selectedIndex;
            if (-1 == selIndex) { alert("Please select an option to move."); return; }
            var increment = 1;
            if ((selIndex + increment) < 0 || (selIndex + increment) > (listbox.options.length - 1)) { return; }
            var selValue = listbox.options[selIndex].value;
            var selText = listbox.options[selIndex].text;
            listbox.options[selIndex].value = listbox.options[selIndex + increment].value;
            listbox.options[selIndex].text = listbox.options[selIndex + increment].text;
            listbox.options[selIndex + increment].value = selValue;
            listbox.options[selIndex + increment].text = selText;
            listbox.selectedIndex = selIndex + increment;
        });
        $("#btnRight").click(function () {

            var listbox = document.getElementById("RightFieldslistBox");
            var selIndex = listbox.selectedIndex;
            if (-1 == selIndex) { alert("Please select an option to move."); return; }
            var increment = -1;
            if ((selIndex + increment) < 0 || (selIndex + increment) > (listbox.options.length - 1)) { return; }

            var selValue = listbox.options[selIndex].value;
            var selText = listbox.options[selIndex].text;
            listbox.options[selIndex].value = listbox.options[selIndex + increment].value;
            listbox.options[selIndex].text = listbox.options[selIndex + increment].text;
            listbox.options[selIndex + increment].value = selValue;
            listbox.options[selIndex + increment].text = selText;
            listbox.selectedIndex = selIndex + increment;
        });
        $("#Btn2Right").click(function () {

            var listbox = document.getElementById("RightFieldslistBox");
            var selIndex = listbox.selectedIndex;
            if (-1 == selIndex) { alert("Please select an option to move."); return; }
            var increment = 1;
            if ((selIndex + increment) < 0 || (selIndex + increment) > (listbox.options.length - 1)) { return; }
            var selValue = listbox.options[selIndex].value;
            var selText = listbox.options[selIndex].text;
            listbox.options[selIndex].value = listbox.options[selIndex + increment].value;
            listbox.options[selIndex].text = listbox.options[selIndex + increment].text;
            listbox.options[selIndex + increment].value = selValue;
            listbox.options[selIndex + increment].text = selText;
            listbox.selectedIndex = selIndex + increment;
        });
        $("#save").click(function () {

            var listbox = document.getElementById("gridlist");
            var listbox1 = document.getElementById("RightFieldslistBox");
            var index1 = '';


            for (var i = 0; i < listbox.length; i++) {

                var selValue = listbox.options[i].value;

                index1 = index1 + selValue + ",";
            }

            for (var i = 0; i < listbox1.length; i++) {

                var selValue = listbox1.options[i].value;

                index1 = index1 + selValue + ",";
            }



            var url = 'webservices/DCServices.asmx/InsertFieldsPosition'
            //'../DC/webservices/DCServices.asmx/InsertFieldsPosition';
            data = "{value:'" + JSON.stringify(index1) + "'}";
            debugger;
            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: OnCheckUserNameAvailabilitySuccess,
                error: OnCheckUserNameAvailabilityError
            });
            function OnCheckUserNameAvailabilitySuccess(response) {
                debugger;
                $("#lblMessage").text("Configuration saved successfully");
            }
            function OnCheckUserNameAvailabilityError(xhr, ajaxOptions, thrownError) {
                debugger;
                alert(xhr.statusText);
            }
        });
    }
</script>
   
      <div style="float:right;">
                <table>
                    <tr>
                        <td style="vertical-align:middle;">
                           <div id="AlignmentOptions" style="text-align:center;display:none;" class="button deffinity medium">Alignment Options</div>
                        </td>
                        <td>
                             <div style="float:right;">
                                 <asp:Button ID="ImgConfig" Text="Column Alignment" runat="server" ToolTip="Fields position change" OnClick="ImgConfig_Click" />
                                 <asp:Label ID="lblImgConfig" runat="server"></asp:Label>
                                 <ajaxToolkit:ModalPopupExtender ID="popIssues" BackgroundCssClass="modalBackground"
                                            runat="server" PopupControlID="PaneladdNew" TargetControlID="lblImgConfig" />
                             </div>
                        </td>
                    </tr>
                </table>
                    </div>
                <div id="DivAlignmentOptions" style="display:none;">
                    <table>
                        <tr>
                            <td>
                               <span style="width:100px;display:block;"> Alignment</span>
                            </td>
                            <td>
                                  <asp:DropDownList ID="ddlAlignment" runat="server" Width="80px">
                                    <asp:ListItem Value="left" Text="Left"></asp:ListItem>
                                    <asp:ListItem Value="right" Text="Right"></asp:ListItem>
                                  </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="Btnappply" class="button deffinity medium" runat="server" Text="Apply" OnClick="Btnappply_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
               
                <asp:Panel ID="PaneladdNew" runat="server" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" Style="display: none" ScrollBars="Auto" ClientIDMode="Static">
    <asp:UpdateProgress runat="server" ID="PaneladdNew2" DisplayAfter="10" AssociatedUpdatePanelID="panelupdate" ClientIDMode="Static">
        <ProgressTemplate>
            <asp:Label ID="image1" runat="server" SkinID="Loading" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="panelupdate" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <div>
                            <asp:Label ID="lblMessage" ClientIDMode="Static" runat="server" ForeColor="Green"></asp:Label></div>
                        <asp:Label ID="lblscreen" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                    </td>
                    <td></td>
                   
                    <td colspan="2">
                        <div style="text-align: right;">
                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" SkinID="BtnLinkCancel" OnClick="lnkCancel_Click" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Left Fields</b>
                        <br />
                        <asp:ListBox ID="gridlist" runat="server" ClientIDMode="Static" Height="300" Width="200"></asp:ListBox>
                    </td>
                    <td>
                        <br />
                        <button id="btn" type="button" class="button deffinity medium">Up</button><br /><br />

                        <button id="Btn2" type="button" class="button deffinity medium">Down</button><br /><br />
                    </td>
                    <td>
                        <b>Right Fields</b>
                        <br />
                        <asp:ListBox ID="RightFieldslistBox" runat="server" ClientIDMode="Static" Height="300" Width="200"></asp:ListBox>
                    </td>
                    <td>
                        <br />
                        <button id="btnRight" type="button" class="button deffinity medium">Up</button><br /><br />

                        <button id="Btn2Right" type="button" class="button deffinity medium">Down</button><br /><br />

                         <button id="save" type="button" class="button deffinity medium">Save</button>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>

        </Triggers>

    </asp:UpdatePanel>
</asp:Panel>
    <asp:GridView ID="gvConfig" runat="server" Width="100%" OnRowCancelingEdit="gvConfig_RowCancelingEdit"
                     OnRowCommand="gvConfig_RowCommand" OnRowEditing="gvConfig_RowEditing" OnRowUpdating="gvConfig_RowUpdating" OnRowDataBound="gvConfig_RowDataBound">
                    <Columns>
                        <asp:TemplateField Visible="false">
                             <ItemStyle Width="30px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckboxAlignment" runat="server"/>
                                <asp:Label ID="lblCheckbox" runat="server" Text='<%# Bind("ID")%>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField ItemStyle-CssClass="form-inline">
                                <HeaderStyle Width="54px" />
                                <ItemStyle Width="54px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Linkedit" runat="server" CausesValidation="false"
                                         CommandName="Edit" CommandArgument='<%# Bind("ID")%>'
                                        SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkUpdate" runat="server" CommandName="Update"
                                        Text="<%$ Resources:DeffinityRes,Update%>" CommandArgument='<%# Bind("ID")%>'
                                        SkinID="BtnLinkUpdate" ToolTip="<%$ Resources:DeffinityRes,Update%>"
                                        ValidationGroup="expenseUpdate"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkCancelernalExpenses" runat="server" CausesValidation="false"
                                        CommandName="Cancel" SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>">
                                    </asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Default Field">
                            <ItemTemplate>
                                <asp:Label ID="lblDefaultField" runat="server" Text='<%# Bind("DefaultName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Instance Field">
                            <ItemTemplate>
                                <asp:Label ID="lblInstanceName" runat="server" Text='<%# Bind("InstanceName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtInstanceName" runat="server" Text='<%# Bind("InstanceName") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Visible">
                            <ItemTemplate>
                                <asp:Label ID="lblIsVisible" runat="server" Text='<%# Eval("IsVisible").ToString() == "True"?"Yes":"No" %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:HiddenField ID="hfIsVisible" runat="server" Value='<%# Eval("IsVisible") %>' />
                                <asp:DropDownList ID="ddlIsVisible" runat="server">
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                     <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mandatory">
                            <ItemTemplate>
                                <asp:Label ID="lblIsMandatory" runat="server" Text='<%# Eval("IsMandatory").ToString() == "True"?"Yes":"No" %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                  <asp:HiddenField ID="hfIsMandatory" runat="server" Value='<%# Eval("IsMandatory") %>' />
                                <asp:DropDownList ID="ddlIsMandatory" runat="server">
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                     <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Default Value" >
                            <ItemTemplate>
                                <asp:Label ID="lblDefaultValue" runat="server" Text='<%# Bind("DefaultValue") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                                <asp:TextBox ID="txtDefaultValue" runat="server" Text='<%# Bind("DefaultValue") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Alignment">
                            <ItemTemplate>
                                <asp:Label ID="lblalignment" runat="server" Text='<%#Bind("Alignment") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:HiddenField ID="hfIsalignment" runat="server" Value='<%# Eval("Alignment") %>' />
                                <asp:DropDownList ID="ddlAlignment" runat="server">
                                    <asp:ListItem Value="left" Text="Left"></asp:ListItem>
                                    <asp:ListItem Value="right" Text="Right"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  HeaderStyle-CssClass="header_bg_r" Visible="false">
                            <ItemStyle Width="150px" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCopy" runat="server" Text="Copy to all Customers" CommandArgument='<%# Bind("ID")%>' CommandName="Copy"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
     <asp:HiddenField ID="HiddenFiled1" runat="server" />
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 
</asp:Content>


