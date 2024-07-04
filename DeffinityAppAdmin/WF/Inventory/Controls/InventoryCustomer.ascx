<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_InventoryCustomer" Codebehind="InventoryCustomer.ascx.cs" %>
<%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>--%>
  <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                <ProgressTemplate>
                    <asp:Image ID="imgLoading6" ImageUrl="~/media/ico_loading.gif" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
         <style>
    .roundViolet
    {
        border: 0px solid Silver;
        padding: 5px 5px;
        background: #DBE3F7;
        border-radius: 8px;
    }
      .roundOrange
    {
        border: 0px solid Silver;
        padding: 5px 5px;
        background: #FFF3E8;
        border-radius: 8px;
    }
       .roundGreen
    {
        border: 0px solid Silver;
        padding: 5px 5px;
        background: #E6F7E6;
        border-radius: 8px;
    }
        </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
        <script type="text/javascript">
            function pageLoad(sender, args) {
                debugger;
                $("#PaneltblDeployedDate").hide();
                $("#hlinkExecption").hide();
                $('#button').click(function (e) {
                    debugger;
                    $("#PaneltblDeployedDate").toggle();
                });
                //$('#UploadImg').click(function (e) {
                //    debugger;
                //    $("#hlinkExecption").toggle();
                //});
            };
            function CheckOne(obj) {
                var grid = obj.parentNode.parentNode.parentNode;
                var inputs = grid.getElementsByTagName("input");
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].type == "checkbox") {
                        if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                            inputs[i].checked = false;
                        }
                    }
                }
            }
            $(document).ready(function () {
                debugger;
                $("#PaneltblDeployedDate").hide();
                $("#hlinkExecption").hide();
                debugger;
                $("body").on("click", "#reportspan", function () {
                    $("#PaneltblDeployedDate").toggle();
                });
                $("body").on("click", "#UploadImg", function () {
                    $("#hlinkExecption").toggle();
                    debugger;
                });
            });
    </script>
 <%--   <script type="text/javascript">
        $(document).ready(function () {
            debugger;
            $("#panelQuickSearch").hide();
            $("#lnkbtn").click(function () {
                event.preventDefault();
                $("#panelQuickSearch").toggle();s
                return false;
            });
        });
    </script>--%>
        <div style="float:right">
            <asp:Image ID="UploadImg" ImageUrl="~/media/btn_ico_upload.gif" runat="server" AlternateText="Inventory Upload" ClientIDMode="Static" />
            <asp:HyperLink ID="hlinkExecption" runat="server" Text="View Exception Reports" ClientIDMode="Static" Target="_blank" CssClass="button deffinity medium" style="color:#606060"></asp:HyperLink>
        </div>
        <div class="clr"></div>
        <asp:Panel ID="PanelCsv" runat="server" Width="100%" style="padding-top:5px;">
            <div class="tab_header_Bold">
                Inventory Upload
            </div>
            <%--  <asp:Label ID="lblCustomerErrorMsg" runat="server" ForeColor="red" Width="100%" ></asp:Label>--%>
            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="up" />
            <asp:Label ID="lblUploadErrorMsg" runat="server" ForeColor="red" Width="100%"></asp:Label>
            <asp:Label ID="lblCustomerMsg" runat="server" Width="100%" BackColor="#F9F9F9"></asp:Label>
            <asp:Label ID="lblCustomerSuccessMsg" runat="server" Width="100%" ForeColor="Green"></asp:Label>
            <asp:Label ID="lblCustomerErrorMsg" runat="server" Width="100%" ForeColor="Red"></asp:Label>
            &nbsp;Upload inventory use&nbsp;
            <asp:FileUpload ID="fileUpload1" runat="server" />&nbsp;<asp:ImageButton ID="ImageButton1"
                SkinID="ImgUpload" runat="server" ImageAlign="AbsMiddle" OnClick="ImageButton1_Click"
                ValidationGroup="up" />&nbsp
        </asp:Panel>
        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
            TargetControlID="PanelCsv" ExpandControlID="UploadImg" CollapseControlID="UploadImg"
            TextLabelID="Lbl1" 
           Collapsed="true" SuppressPostBack="true"></ajaxToolkit:CollapsiblePanelExtender>
        <asp:Label ID="lblDummy" runat="server"></asp:Label>
        <ajaxToolkit:ModalPopupExtender ID="mdlPopUpUploadList" runat="server" BackgroundCssClass="modalBackground"
            TargetControlID="lblDummy" PopupControlID="pnlSearch">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlSearch" runat="server" BackColor="White" Style="display: none;"
            Width="600px" Height="500px" BorderStyle="Double" BorderColor="LightSteelBlue"
            ScrollBars="Auto">
            <div style="float: right">
                <asp:ImageButton ID="ImageButton3" runat="server" SkinID="ImgSymCancel" ToolTip="Close"
                    OnClick="ImageButton3_Click" /></div>
            <div class="sec_header">
                Inventory Upload</div>
            <asp:Label ID="lblUploadExceptionMsg" runat="server" ForeColor="Red"></asp:Label>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <asp:Image ID="imgLoading_1" ImageUrl="~/media/ico_loading.gif" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="GvUploadList" runat="server" OnRowCommand="GvUploadList_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Use">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUse" runat="server" Text='<%#Bind("Use") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%#Bind("InventoryID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUseID" runat="server" Text='<%#Bind("UseID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Category" HeaderText="Category" />
                                <asp:BoundField DataField="SubCategory" HeaderText="Sub Category" />
                                <asp:BoundField DataField="Product" HeaderText="Product" />
                                <asp:TemplateField HeaderText="Opening Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOpeningQty" runat="server" Text='<%#Bind("OpeningQty") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Number Deployed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumberDeployed" runat="server" Text='<%#Bind("NumberDeployed") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnConfirm" runat="server" Text="Commit to Database" CommandArgument='<%#Bind("InventoryID") %>'
                                            CommandName="Commit" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="panelQuickSearch" runat="server">
             <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                Search</div>
            <div>
                   <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="export" />
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            Customer
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlcustomerInSearch" runat="server" AutoPostBack="true"
                                 OnSelectedIndexChanged="ddlcustomerInSearch_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqSearchInCustomer" runat="server" ControlToValidate="ddlcustomerInSearch" InitialValue="0"
                                                                 ErrorMessage="Please select customer" ValidationGroup="export">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            Site
                        </td>
                        <td>
                             <asp:DropDownList ID="ddlsiteInSearch" Width="200px" runat="server"></asp:DropDownList>
                              <asp:RequiredFieldValidator ID="ReqddlsiteInSearch" runat="server" ControlToValidate="ddlsiteInSearch" InitialValue="0"
                                                                 ErrorMessage="Please select site" ValidationGroup="export">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                              Text
                        </td>
                        <td>
                               <asp:TextBox ID="txtText" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="button deffinity medium" OnClick="btnsearch_Click" />
                        </td>
                        <td>
                           <%-- <span id="" style="" class="button deffinity medium">Report</span>--%>
                            <asp:Image ID="reportspan" ClientIDMode="Static" ImageUrl="~/media/ico_report.png" ImageAlign="AbsMiddle" AlternateText="Report" runat="server" />
                        </td>
                    </tr>
                   <tr id="PaneltblDeployedDate">
                <td>
                    From Deployed Date:
                </td>
                <td>
                    <asp:TextBox ID="txtFromDeployedDate" runat="server" Width="120px"></asp:TextBox>
                    <asp:Image ID="imgFromDeployedDate" runat="server" SkinID="Calender" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDeployedDate"
                         CssClass="MyCalendar" PopupButtonID="imgFromDeployedDate">
                    </ajaxToolkit:CalendarExtender>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFromDeployedDate"
                        Display="None" ErrorMessage="Please enter valid from deployed date" Operator="DataTypeCheck"
                        Type="Date" ValidationGroup="export"></asp:CompareValidator>
                </td>
                <td>
                    To Deployed Date:
                </td>
                <td>
                    <asp:TextBox ID="txtToDeployedDate" runat="server" Width="120px"></asp:TextBox>
                    <asp:Image ID="imgToDeployedDate" runat="server" SkinID="Calender" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtToDeployedDate"
                         CssClass="MyCalendar" PopupButtonID="imgToDeployedDate">
                    </ajaxToolkit:CalendarExtender>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtToDeployedDate"
                        Display="None" ErrorMessage="Please enter valid to deployed date" Operator="DataTypeCheck"
                        Type="Date" ValidationGroup="export"></asp:CompareValidator>
                </td>
                <td>
                       <asp:ImageButton ID="imgbtnExportToExcel" runat="server" ImageUrl="~/media/ico_excel.png" ToolTip="Export To Excel"
                                OnClick="imgbtnExportToExcel_Click" ValidationGroup="export" ImageAlign="AbsBottom" />&nbsp;
                            <asp:ImageButton ID="imgbtnExportToPdf" runat="server" ImageUrl="~/media/ico_pdf.png" ToolTip="Export To Pdf"
                                OnClick="imgbtnExportToPdf_Click" ValidationGroup="export" ImageAlign="AbsBottom" />
                </td>
            </tr>
        </table>
           
            </div>
        </asp:Panel>
     <%--   pnlAddProduct--%>
        <asp:Panel ID="pnlAddProduct" runat="server" Visible="false">
            <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                Add/Amend Product</div>
            <asp:UpdateProgress ID="uProgress" runat="server">
                <ProgressTemplate>
                    <asp:Image ID="imgloading_0" runat="server" ImageUrl="~/media/ico_loading.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:ValidationSummary ID="InvntValidation" runat="server" Visible="false" ValidationGroup="AddInvnt" />
         
            <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblMsg" runat="server" Visible="false" ForeColor="Green" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblMsgDepartment" runat="server" ForeColor="Green" Visible="false"></asp:Label>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Visible="true" ValidationGroup="Group1" />
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" Visible="true" ValidationGroup="Group2" />
            
            <table class="sec_table2" style="width: 100%">
                    <asp:Panel ID="PnlNewItem" runat="server" Visible="false">
                        <tr>
                    <td>
                        Customer:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustomer" runat="server" Width="150px" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"
                            AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvCustomer" runat="server" ControlToValidate="ddlCustomer"
                            InitialValue="0" ValidationGroup="Group2" ErrorMessage="Please select customer">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCustomer"
                            InitialValue="0" ValidationGroup="up" ErrorMessage="Please select customer">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblSite" runat="server" Text="Site:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSite" runat="server" Width="150px" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged"
                            AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                        &nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSite"
                            InitialValue="0" ValidationGroup="Group2" ErrorMessage="Please select site">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSite"
                            InitialValue="0" ValidationGroup="up" ErrorMessage="Please select site">*</asp:RequiredFieldValidator>
                    </td>
                      <td>
                        <asp:Label ID="lblArea" Text="Area" runat="server"></asp:Label>
                    </td>
                    <td>
                            <asp:DropDownList ID="ddlarea" runat="server" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddlarea_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddllocation" Width="150px" runat="server"
                          AutoPostBack="true" OnSelectedIndexChanged="ddllocation_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td><asp:Label ID="lblShelf" runat="server" Text="Shelf"></asp:Label></td>
                    <td>
                          <asp:DropDownList ID="ddlshelf" runat="server" Width="150px"
                          AutoPostBack="true" OnSelectedIndexChanged="ddlshelf_SelectedIndexChanged1"></asp:DropDownList>
                    </td>
                    <td><asp:Label ID="lblBin" Text="Bin" runat="server"></asp:Label></td>
                    <td>
                         <asp:DropDownList ID="ddlbin" runat="server" Width="150px"></asp:DropDownList>
                             <asp:ImageButton ID="ImageButtonbinAdd" runat="server" SkinID="ImgSymAdd" OnClick="ImageButtonbinAdd_Click" />
                            <asp:ImageButton ID="ImageButtonBinEdit" runat="server" ImageUrl="~/media/ico_edit.png" ImageAlign="AbsMiddle" OnClick="ImageButtonBinEdit_Click" />

                             <asp:Panel ID="paneladd" runat="server" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" Style="display: none"
                               ScrollBars="Auto" ClientIDMode="Static">
                              <asp:UpdateProgress runat="server" ID="PaneladdNew2" DisplayAfter="10" AssociatedUpdatePanelID="panelupdate" ClientIDMode="Static">
                                        <ProgressTemplate>
                                             <asp:Image ID="image1" runat="server" ImageUrl="~/media/ico_loading.gif" />
                                        </ProgressTemplate>
                                 </asp:UpdateProgress>
                                 <asp:UpdatePanel ID="panelupdate" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
                                      <ContentTemplate>
                                             <table>
                                                  <tr>
                                                       <td colspan="3">
                         <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                                                <asp:Label ID="lbladd" runat="server"></asp:Label></div>
                    </td>
                    <td>
                         <div style="text-align: right;">
                            <asp:ImageButton ID="lnkCancel" runat="server" CausesValidation="false"
                                 ImageUrl="~/media/ico_cancel.png" />
                        </div>
                    </td>
                                                  </tr>
                                                     <tr>
                             <td colspan="3">
                                 <asp:Label ID="lblerror1" EnableViewState="false" runat="server"></asp:Label>
                                   <asp:ValidationSummary ID="catSummary" runat="server" ValidationGroup="popup" />
                                   <asp:RequiredFieldValidator ID="rfvtxt" runat="server" ForeColor="Red" ControlToValidate="txtBinName" Display="None"
                          ErrorMessage="Please enter name." ValidationGroup="popup"></asp:RequiredFieldValidator>
                             </td>
                         </tr>
                                                    <tr>
                    <td>
                        Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtBinName" Width="200px" runat="server" ValidationGroup="popup"></asp:TextBox>
                    </td>
                    <td>
                        <asp:HiddenField ID="hdForChecking" runat="server" />
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="button deffinity medium"
                             ValidationGroup="popup" OnClick="btnsubmit_Click" />
                    </td>
                </tr>
                                                  <tr>
                              <td colspan="3">
                        <asp:Label ID="lblspace3" runat="server"></asp:Label>
                       </td>
                         </tr>
                                             </table>
                                      </ContentTemplate>
                                 </asp:UpdatePanel>
                          </asp:Panel>
                         <asp:Label ID="l1" runat="server"></asp:Label>
                               <ajaxToolkit:ModalPopupExtender ID="popupAdd" runat="server" BackgroundCssClass="modalBackground"
                             TargetControlID="l1" PopupControlID="paneladd" CancelControlID="lnkCancel"></ajaxToolkit:ModalPopupExtender>

                    </td>
                </tr>
                <tr>
                    <td>
                        Category:
                    </td>
                    <td>
                        <asp:Panel ID="pnlcategory" runat="server" Width="240px">
                            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" Width="150px"
                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCategory"
                                InitialValue="0" ValidationGroup="Group2" ErrorMessage="Please select category">*</asp:RequiredFieldValidator>--%>
                            <asp:ImageButton ID="btnaddcategory" OnClick="btnaddcategory_Click" runat="server"
                                SkinID="ImgSymAdd" CausesValidation="False"></asp:ImageButton>
                            <asp:ImageButton ID="btn_CategoryEdit" runat="server" ValidationGroup="Edit_catelog"
                                ImageUrl="~/media/ico_edit.png" OnClick="btn_CategoryEdit_Click" ImageAlign="AbsMiddle">
                            </asp:ImageButton>
                            <asp:ImageButton ID="btnDeleteCategory" runat="server" OnClientClick="javascript:alert('Do you want to delete category,associated sub category and item(s)?');"
                                SkinID="ImgSymDel" OnClick="btnDeleteCategory_Click" />
                        </asp:Panel>
                        <asp:Panel ID="pnladdcategory" runat="server" Visible="false">
                            <asp:TextBox ID="txtAddCategory" runat="server" Width="200px" ValidationGroup="cat1"></asp:TextBox>
                            <asp:ImageButton ID="btnSaveCategory" runat="server" ToolTip="Add Category" ValidationGroup="cat1"
                                OnClick="btnSaveCategory_Click" SkinID="ImgSymUpdate"></asp:ImageButton>
                            <asp:ImageButton ID="btnCancelCategory" runat="server" ToolTip="Cancel" OnClick="btnCancelCategory_Click"
                                CausesValidation="False" SkinID="ImgSymCancel"></asp:ImageButton>
                            <div>
                                <asp:RequiredFieldValidator runat="server" ID="ReqCatname" ControlToValidate="txtAddCategory"
                                    SetFocusOnError="true" ErrorMessage="Please enter Category" ForeColor="Red" ValidationGroup="cat1"></asp:RequiredFieldValidator></div>
                        </asp:Panel>
                        <asp:HiddenField ID="HID_Category" runat="server"></asp:HiddenField>
                    </td>
                    <td>
                        Sub Category:
                    </td>
                    <td>
                        <asp:Panel ID="pnlsubcategory" runat="server" Width="250px">
                            <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="True" Width="150px"
                                OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSubCategory"
                                InitialValue="0" ValidationGroup="Group2" ErrorMessage="Please select subcategory">*</asp:RequiredFieldValidator>--%>
                            <asp:ImageButton ID="btnaddsubcategory" OnClick="btnaddsubcategory_Click" runat="server"
                                SkinID="ImgSymAdd" ValidationGroup="cat0"></asp:ImageButton>
                            <asp:ImageButton ID="btn_editSubCategory" runat="server" ValidationGroup="Edit_subcatelog"
                                ImageUrl="~/media/ico_edit.png" OnClick="btn_editSubCategory_Click" ImageAlign="AbsMiddle">
                            </asp:ImageButton>
                            <asp:ImageButton ID="btnSubCategory" runat="server" OnClientClick="javascript:alert('Do you want to delete Sub category and associated item(s)?');"
                                OnClick="btnSubCategory_Click" SkinID="ImgSymDel" />
                        </asp:Panel>
                        <asp:Panel ID="pnladdsubcategory" runat="server" Visible="false">
                            <asp:TextBox ID="txtAddSubCategory" runat="server" Width="200px" ValidationGroup="Subcat1"></asp:TextBox>
                            <asp:ImageButton ID="btnSaveSubCategory" runat="server" ToolTip="Add SubCategory"
                                ValidationGroup="Subcat1" OnClick="btnSaveSubCategory_Click" SkinID="ImgSymUpdate">
                            </asp:ImageButton>
                            <asp:ImageButton ID="btnCancelSubCategory" runat="server" ToolTip="Cancel" OnClick="btnCancelSubCategory_Click"
                                SkinID="ImgSymCancel"></asp:ImageButton>
                            <div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtAddSubCategory"
                                    ErrorMessage="Please enter sub Category" ForeColor="Red" ValidationGroup="Subcat1"></asp:RequiredFieldValidator></div>
                        </asp:Panel>
                        <asp:HiddenField ID="HID_SubCategory" runat="server"></asp:HiddenField>
                    </td>
                    <td>
                        Product:
                    </td>
                    <td style="width: 300px;">
                        <asp:DropDownList ID="ddlProduct" runat="server" Width="200px" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged1"
                            AutoPostBack="true"></asp:DropDownList>
                        &nbsp;
                        <asp:CheckBox ID="chkAddSCMCatalogue" runat="server" Checked="false" />
                        &nbsp;
                        <asp:ImageButton ID="imgAddInventory" runat="server" SkinID="ImgSymAdd" OnClick="imgAddInventory_Click"
                            ToolTip="Add to ServiceCatalogue" />
                    </td>
                    <%--<td><asp:ImageButton ID="imgSearchProducts" runat="server" SkinID="ImgAdd" 
                                onclick="imgSearchProducts_Click" /> </td>--%>
                </tr>
                </asp:Panel>
                <asp:Panel ID="pnlAddItems" runat="server" Visible="false">
                    <tr>
                        <td colspan="2" class="roundGreen">
                            <div class="roundGreen">
                            <table>
                                <tr>
                                       <td style="background-color: #E6F7E6;">
                                                        Opening Stock
                                        </td>
                                       <td style="background-color: #E6F7E6;">
                                             <asp:TextBox ID="txtStock" runat="server" Width="70px" ReadOnly="true"></asp:TextBox>
                                       </td>
                                </tr>
                                <tr>
                              <td style="background-color: #E6F7E6;width:130px;">
                            Stock level (QTY)
                        </td>
                        <td style="background-color: #E6F7E6;">
                            <asp:TextBox ID="txtStockLevel" runat="server" Width="70px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="txtStockLevel"
                                Display="None" ErrorMessage="Please enter valid Stock Level" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Integer" ValidationGroup="Group2"></asp:CompareValidator>
                         
                        </td>
                                </tr>
                                <tr>
                                     <td style="background-color: #E6F7E6;">
                            QTY In Use
                        </td>
                        <td style="background-color: #E6F7E6;">
                            <asp:TextBox ID="txtQtyInUse" runat="server" Enabled="false" Width="70px"></asp:TextBox>
                        </td>
                                </tr>
                                <tr>
                                     <td style="width: 140px;background-color: #E6F7E6;">
                            Quantity on Order
                        </td>
                        <td style="background-color: #E6F7E6;">
                            <asp:TextBox ID="txtQntyOrder" runat="server" Width="70px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator16" runat="server" ControlToValidate="txtQntyOrder"
                                Display="None" ErrorMessage="Please enter valid Quantity" Operator="DataTypeCheck"
                                Type="Integer" ValidationGroup="Group2"></asp:CompareValidator>
                        </td>
                                </tr>
                                <tr>
                                  <td style="background-color: #E6F7E6;">
                            Reorder level
                        </td>
                        <td style="background-color: #E6F7E6;">
                            <asp:TextBox ID="txtReorderlevel" runat="server" Width="70px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtReorderlevel"
                                Display="None" ErrorMessage="Please enter valid Reorder Level" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Integer" ValidationGroup="Group2"></asp:CompareValidator>
                        </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnaddnewstock" runat="server" CausesValidation="false" Text="Add Stock" />
                                    </td>
                                </tr>
                            </table>
                                      </div>
                            </td>
                             <td colspan="2" class="roundOrange">
                                   <div class="roundOrange">
                                <table>
                                    <tr>
                                         <td style="background-color:#FFF3E8;">
                            Barcode
                        </td>
                        <td style="background-color:#FFF3E8;">
                            <asp:TextBox ID="txtBarcode" runat="server"></asp:TextBox>
                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color:#FFF3E8;">
                            Part Number
                        </td>
                        <td>
                            <asp:TextBox ID="txtPartNumber" runat="server"></asp:TextBox>
                        </td>
                                    </tr>
                                    <tr>
                                         <td style="vertical-align: middle;background-color:#FFF3E8;">
                            Notes
                        </td>
                        <td style="vertical-align: middle;background-color:#FFF3E8;">
                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                        </td>
                                    </tr>
                                   
                                    <tr>
                                         <td style="vertical-align: middle;background-color:#FFF3E8;">
                            Upload image
                        </td>
                        <td style="vertical-align: middle;background-color:#FFF3E8;">
                            <asp:FileUpload ID="FileUploadMaterial" runat="server"></asp:FileUpload>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                ControlToValidate="FileUploadMaterial" Display="None" ErrorMessage="" ValidationExpression="^.*([^\.][\.](([gG][iI][fF])|([Jj][pP][Gg])|([Jj][pP][Ee][Gg])|([Bb][mM][pP])|([Pp][nN][Gg])))"
                                ValidationGroup="Group2">File</asp:RegularExpressionValidator>
                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                          <asp:Image ID="ImgProfile" runat="server" Width="50" Height="70" />
                                        </td>
                                    </tr>
                                </table>
                                           </div>
                            </td>
                     
                        <td colspan="2" class="roundViolet">
                             <div class="roundViolet">
                                 <table>
                                     <tr>
                                         <td style="background-color:#DBE3F7;">
                            Supplier
                        </td>
                        <td colspan="1" style="background-color:#DBE3F7;">
                            <asp:DropDownList ID="ddlSupplier" runat="server" Width="170px">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSupplier" runat="server" Visible="False"></asp:TextBox>
                            <asp:ImageButton ID="btnAddSupplier" runat="server" Visible="false" OnClick="btnAddSupplier_Click"
                                SkinID="ImgSymAdd"></asp:ImageButton>
                            <asp:ImageButton ID="btnCancelSupplier" runat="server" OnClick="btnCancelSupplier_Click"
                                Visible="False" SkinID="ImgCancel"></asp:ImageButton>
                        </td>
                                     </tr>
                                     <tr>
                                                 <td style="background-color:#DBE3F7;">
                            Manufacturer
                        </td>
                        <td style="background-color:#DBE3F7;">
                            <asp:DropDownList ID="ddlManufacturer" runat="server">
                            </asp:DropDownList>
                            <asp:ImageButton ID="BtnAddManufacturer" runat="server" SkinID="ImgSymAdd" ImageAlign="AbsMiddle" />
                            <ajaxToolkit:ModalPopupExtender ID="mdlManufacturer" runat="server" CancelControlID="btnManufacturerCancel"
                                BackgroundCssClass="modalBackground" TargetControlID="BtnAddManufacturer" PopupControlID="pnlManufacturer" />
                            <asp:Panel ID="pnlManufacturer" runat="server" BackColor="White" Style="display: none"
                                Width="230px" BorderStyle="Double" BorderColor="LightSteelBlue">
                                <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                                    Manufacturer
                                </div>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtManufacturer" runat="server" Width="210px"></asp:TextBox>
                                            <asp:HiddenField ID="H_Department" runat="server" Value="0" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtManufacturer"
                                                ErrorMessage="Please enter manufacturer" ForeColor="Red" Visible="true" ValidationGroup="Group_Department"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnManufacturerInsert" runat="server" Text="OK" OnClick="btnManufacturer_Click"
                                                SkinID="ImgSubmit" ValidationGroup="Group_Department" />
                                            <asp:ImageButton ID="btnManufacturerCancel" runat="server" Text="Close" SkinID="ImgCancel" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <%--<asp:ImageButton ID="btnManuFacturerInsert" runat="server" Text="OK" OnClick="btnManufacturerInsert_Click" SkinID="ImgSubmit"/>
        <asp:ImageButton ID="btnManufacturerCancel" runat="server" Text="Close" SkinID="ImgCancel" />--%>
                        </td>
                                     </tr>
                                      <tr>
                        <td style="width: 130px;">
                            Lead Time(Days)
                        </td>
                        <td>
                            <asp:TextBox ID="txtLeadTime" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator18" runat="server" ControlToValidate="txtLeadTime"
                                Display="None" ErrorMessage="Please enter valid Lead Time(Days)" Operator="DataTypeCheck"
                                Type="Integer" ValidationGroup="Group2"></asp:CompareValidator>
                        </td>
                                    </tr>


                                     <tr>
                                             <td style="background-color:#DBE3F7;">
                            Date ordered
                        </td>
                        <td style="background-color:#DBE3F7;">
                            <asp:TextBox ID="txtDateOrdered" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                            <asp:Image ID="imgOrderDate" runat="server" SkinID="Calender" />
                            <ajaxToolkit:CalendarExtender ID="ClndrExtDateOrdered" runat="server" TargetControlID="txtDateOrdered"
                                 CssClass="MyCalendar" PopupButtonID="imgOrderDate">
                            </ajaxToolkit:CalendarExtender>
                            <asp:CompareValidator ID="CmpValOrderDate" runat="server" ControlToValidate="txtDateOrdered"
                                Display="None" ErrorMessage="Please enter valid Order date" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="Group2"></asp:CompareValidator>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDateOrdered"
                                        Display="None" ErrorMessage="Please enter OrderDate" SetFocusOnError="True"
                                        ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                        </td>
                                     </tr>
                                     <tr>
                                              
                        <td style="width: 150px;background-color:#DBE3F7;">
                            Expected Arrival Date
                        </td>
                        <td style="background-color:#DBE3F7;">
                            <asp:TextBox ID="txtArrivalDate" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                            <asp:Image ID="imgArrivalDate" runat="server" SkinID="Calender" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtArrivalDate"
                                 CssClass="MyCalendar" PopupButtonID="imgArrivalDate">
                            </ajaxToolkit:CalendarExtender>
                            <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="txtArrivalDate"
                                ControlToCompare="txtDateOrdered" Operator="GreaterThanEqual" ErrorMessage="Arrival date must be later than Order date"
                                Type="Date" ValidationGroup="Group2"></asp:CompareValidator>
                        </td>
                                     </tr>
                                     <tr>
                                                    <td style="background-color:#DBE3F7;">
                            Description <span style="color: Red">*
                        </td>
                        <td style="background-color:#DBE3F7;">
                            <asp:TextBox ID="txtItemDesc" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemDesc"
                                Display="None" ErrorMessage="Please enter Description" SetFocusOnError="True"
                                ValidationGroup="Group2"></asp:RequiredFieldValidator>
                        </td>
                                     </tr>
                                 </table>
                             </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Panel ID="pnlCustomFields" runat="server">
                                  <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                                       <%=Resources.DeffinityRes.CustomFieldsfor %>
                    <asp:Label ID="lblCustomFiledCustomer" runat="server"></asp:Label>
                                  </div>
                                  </div>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updatepanel_additional">
                <ProgressTemplate>
                    <asp:Image ID="imgloading_2" runat="server" ImageUrl="~/media/ico_loading.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
                                <asp:UpdatePanel ID="updatepanel_additional" runat="server">
                                    <ContentTemplate>
                                          <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                                    </ContentTemplate>
                                    <Triggers>

                                       <%-- <asp:PostBackTrigger ControlID="btnSave" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                               
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdnItemID" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hdnImageID" runat="server"></asp:HiddenField>
                            <asp:ImageButton ID="btnSaveMaterial" runat="server" OnClick="btnSaveMaterial_Click"
                                SkinID="ImgSave" ValidationGroup="Group2" />
                            &nbsp;<asp:ImageButton ID="imgbtnUpdateMaterial" runat="server" OnClick="imgbtnUpdateMaterial_Click"
                                SkinID="ImgUpdate" ValidationGroup="Group2" Visible="False" />
                            <asp:ImageButton ID="ImageButton2" runat="server" AlternateText="Cancel" CausesValidation="False"
                                OnClick="btnCancelrow_Click" SkinID="ImgCancel" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <%--   <asp:Panel ID="pnlStorageDetails" runat="server">
                                                    <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                                                        Product by Site</div>
                                                    <asp:GridView ID="GvStorage" runat="server" EmptyDataText="No data found!" OnRowCommand="GvStorage_RowCommand"
                                                        Width="50%">
                                                        <Columns>
                                                            <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-CssClass="header_bg_l" />
                                                            <asp:BoundField DataField="Site" HeaderText="Site"  />
                                                            <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Right" />
                                                            <asp:TemplateField HeaderText="Transfer Items">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgLMOptions" runat="server" CausesValidation="false" CommandName="Transfer"
                                                                        ImageUrl="~/media/ico_more_options.png" CommandArgument='<%# Bind("ID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgHis" runat="server" CausesValidation="false" CommandName="History"
                                                                        ImageUrl="~/media/ico_history.gif" CommandArgument='<%# Bind("ID") %>' Visible="false" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgUse" runat="server" ImageUrl="~/media/btn_view.gif" CommandArgument="<%# Bind('ID') %>"
                                                                        CommandName="View" />
                                                                        
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>--%>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </asp:Panel>
        
        <ajaxToolkit:ModalPopupExtender ID="mdlpopupHisstory" runat="server" BackgroundCssClass="modalBackground"
            TargetControlID="ImgHistory" PopupControlID="pnlHistory" CancelControlID="imgClose">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Label ID="ImgHistory" runat="server" />
        <asp:Panel ID="pnlHistory" runat="server" BackColor="White" Style="display: none;"
            Width="1000px" Height="600px" BorderStyle="Double" BorderColor="LightSteelBlue"
            ScrollBars="Auto">
            <div style="float: right">
                <asp:ImageButton ID="imgClose" runat="server" SkinID="ImgSymCancel" ToolTip="Close" /></div>
            <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                History</div>
            <table width="100%" style="padding-left: 10px;">
                <tr>
                    <td>
                        <asp:GridView ID="GvHistory" runat="server" AutoGenerateColumns="False" GridLines="None"
                            ShowHeaderWhenEmpty="true" HorizontalAlign="Left" BorderStyle="None" CellPadding="2"
                            CellSpacing="2" EmptyDataText="No histoy found!" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="ItemDescription" HeaderText="Product" />
                                <asp:BoundField DataField="OpeningStock" HeaderText="Opening Stock" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoDeployed" HeaderText="Qty Deployed" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="UseCode" HeaderText="Use Code"  />
                                <asp:BoundField DataField="ReasonCode" HeaderText="Reason Code" />
                                <asp:BoundField DataField="TransferQty" HeaderText="Transfer QTY" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Notes" HeaderText="Notes" />
                                <asp:BoundField DataField="Qty" HeaderText="Closing Stock" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By" />
                                <asp:BoundField DataField="MdofiedDate" DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderText="Modified Date" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
         <ajaxToolkit:ModalPopupExtender ID="mdlpopupaddnewstock" runat="server" BackgroundCssClass="modalBackground"
              TargetControlID="btnaddnewstock" PopupControlID="paneladdstock"></ajaxToolkit:ModalPopupExtender>
    
        <asp:Panel ID="paneladdstock" runat="server" BackColor="White"
                        Style="display:none" Width="800px" Height="600px" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="Auto">
             <div style="float: right">
               <asp:ImageButton ID="imgBtnCancel" runat="server" CausesValidation="false" SkinID="ImgSymCancel" ToolTip="Close" OnClick="imgBtnCancel_Click" /></div>
                <div class="sec_header">Adding New Stock "<asp:Label ID="lblproductName" runat="server"></asp:Label>"</div>
             <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="popupStockAdd">
                <ProgressTemplate>
                    <asp:Image ID="imgloading_4" runat="server" ImageUrl="~/media/ico_loading.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="popupStockAdd" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
                <ContentTemplate>
                   <table>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblmsgInpopup" EnableViewState="false" runat="server"></asp:Label>
                         <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="AddStock" />
                        <asp:ValidationSummary ID="VS2" runat="server" ValidationGroup="BinEdit" />
                        
                    </td>
                </tr>
            <tr>
              <td>
                Site
            </td>
            <td>
                <asp:DropDownList ID="ddlsiteStockadd" ValidationGroup="AddStock" runat="server" Width="180px"></asp:DropDownList>
            </td>
                 <td>
                Area
            </td>
            <td>
                <asp:DropDownList ID="ddlareaStockadd" runat="server" ValidationGroup="AddStock" Width="180px" AutoPostBack="true"
                     OnSelectedIndexChanged="ddlareaStockadd_SelectedIndexChanged"></asp:DropDownList>
            </td>
                  <td>
                Location
            </td>
            <td>
                <asp:DropDownList ID="ddllocationStockadd" runat="server" ValidationGroup="AddStock" Width="180px" AutoPostBack="true"
                     OnSelectedIndexChanged="ddllocationStockadd_SelectedIndexChanged"></asp:DropDownList>
            </td>

                <td>
                    <asp:RequiredFieldValidator ID="reqsite" InitialValue="0" runat="server" ControlToValidate="ddlsiteStockadd" ValidationGroup="AddStock"
                         ErrorMessage="Please select site" Display="None"></asp:RequiredFieldValidator>
                     <asp:RequiredFieldValidator ID="reqarea" InitialValue="0" runat="server" ControlToValidate="ddlareaStockadd"
                         ErrorMessage="Please select area" ValidationGroup="AddStock" Display="None"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="Reqlocation" InitialValue="0" runat="server" Display="None"
                              ValidationGroup="AddStock" ControlToValidate="ddllocationStockadd"
                         ErrorMessage="Please select location"></asp:RequiredFieldValidator>
                </td>
            </tr>
                <tr>
            <td>
                Shelf
            </td>
            <td>
                <asp:DropDownList ID="ddlshelfStockadd" ValidationGroup="AddStock" runat="server" Width="180px" AutoPostBack="true"
                     OnSelectedIndexChanged="ddlshelfStockadd_SelectedIndexChanged"></asp:DropDownList>
            </td>
              <td>
                Bin
            </td>
            <td>
                <asp:DropDownList id="ddlbinStockadd" ValidationGroup="AddStock" Width="150px"  runat="server"></asp:DropDownList>
                <asp:TextBox ID="txtBinInPopup" runat="server" Width="180px" Visible="false" ValidationGroup="BinEdit"></asp:TextBox>
                 <asp:ImageButton ID="Imgbtnbinadd" runat="server" ImageUrl="~/media/btn_add.gif"
                       OnClick="Imgbtnbinadd_Click" ImageAlign="AbsMiddle"/> 
                  <asp:Button ID="ImgAddBin" runat="server" Text="Add" Visible="false" OnClick="ImgAddBin_Click" />
                    <asp:Button ID="btnCaneclBin" runat="server" Text="Cancel" Visible="false" OnClick="btnCaneclBin_Click" />
            </td>
                     <td>
                    Quantity
                </td>
                <td>
                    <asp:TextBox ID="txtQuantity" runat="server" ValidationGroup="AddStock" Width="50px"></asp:TextBox>
                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtQuantity"
                            ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
                       <asp:RangeValidator runat="server" id="RangeValidator1" controltovalidate="txtQuantity" type="Integer" ValidationGroup="AddStock" ForeColor="Red"
                                                    minimumvalue="1" maximumvalue="10000000" Display="None" errormessage="Please enter valid Qty"></asp:RangeValidator>

                </td>


                    <td>
                          <asp:RequiredFieldValidator ID="reqshelf" InitialValue="0" ValidationGroup="AddStock"
                               runat="server" ControlToValidate="ddlshelfStockadd" Display="None"
                         ErrorMessage="Please select shelf"></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator ID="reqTxtBin" runat="server" ControlToValidate="txtBinInPopup" Display="None"
                             ValidationGroup="BinEdit" ErrorMessage="Please enter bin name"></asp:RegularExpressionValidator>
                          <asp:RequiredFieldValidator ID="ReqBin" InitialValue="0" ValidationGroup="AddStock"
                               runat="server" ControlToValidate="ddlbinStockadd" Display="None"
                         ErrorMessage="Please select bin"></asp:RequiredFieldValidator>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtQuantity" ValidationGroup="AddStock"
                         ErrorMessage="Please enter quantity" Display="None"></asp:RequiredFieldValidator>
                    </td>
                        </tr>
            <tr>
                <td style="align-content:center">Notes</td>
                <td style="align-content:center"><asp:TextBox ID="txtnotesinpopup" Width="180px" runat="server"></asp:TextBox></td>
                 <td style="align-content:center">
                   <asp:Button ID="btnSubmitInPopUp" runat="server" Text="Submit"
                        ValidationGroup="AddStock" OnClick="btnSubmitInPopUp_Click"/>
               </td>
            </tr>
        </table>
                    <asp:GridView ID="gridNewStockItems" runat="server" AutoGenerateColumns="false" GridLines="None"
                            ShowHeaderWhenEmpty="true" HorizontalAlign="Left" BorderStyle="None" CellPadding="2"
                            CellSpacing="2" EmptyDataText="No histoy found!" Width="100%" 
                        OnRowCommand="gridNewStockItems_RowCommand" OnRowDeleting="gridNewStockItems_RowDeleting">
            <Columns>
                <asp:BoundField Visible="false" DataField="CustomerName" HeaderText="Customer" HeaderStyle-CssClass="header_bg_l" />
               <asp:BoundField Visible="false" DataField="ProductName" HeaderText="Product Name" />
                <asp:BoundField DataField="Sitename" HeaderText="Site" HeaderStyle-CssClass="header_bg_l" />
                <asp:BoundField DataField="AreaName" HeaderText="Area" />
                <asp:BoundField DataField="LocationName" HeaderText="Location" />
                <asp:BoundField DataField="ShelfName" HeaderText="Shelf" />
                <asp:BoundField DataField="BinName" HeaderText="Bin" />
                   <asp:BoundField DataField="Qty" HeaderText="Qty" />
                <asp:BoundField DataField="Notes" HeaderText="notes" />
                <asp:BoundField DataField="UserName" HeaderText="User Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="btndelete" runat="server" SkinID="ImgSymDel" CommandName="Delete" CommandArgument='<%# Bind("Id")%>' />
                    </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle CssClass="header_bg_r" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
        </asp:Panel>


        
        <asp:Label ID="l111" runat="server"></asp:Label>
           <ajaxToolkit:ModalPopupExtender ID="mdlpopupinGrid" runat="server" BackgroundCssClass="modalBackground"
             TargetControlID="l111" PopupControlID="PnlPopUpInGrid" CancelControlID="ImageButton5"></ajaxToolkit:ModalPopupExtender>

          <ajaxToolkit:ModalPopupExtender ID="mdlpopupForNewItem" runat="server" BackgroundCssClass="modalBackground"
             TargetControlID="l11" PopupControlID="PanelForNewItem" CancelControlID="ImageButton4"></ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="PanelForNewItem" runat="server" BackColor="White"
                        Style="display:none" Width="860px" Height="98%" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="None">
             <div style="float: right">
               <asp:ImageButton ID="ImageButton4" runat="server" SkinID="ImgSymCancel" ToolTip="Close" OnClick="imgBtnCancel_Click" /></div>
                <div class="sec_header"><asp:Label ID="Label7" Text="New Inventory" runat="server"></asp:Label></div>
             <asp:UpdateProgress ID="UpdateProgress7" runat="server" AssociatedUpdatePanelID="UpdatePanel_AddItem">
                <ProgressTemplate>
                    <asp:Image ID="imgloading_51" runat="server" ImageUrl="~/media/ico_loading.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel_AddItem" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table>
                          <tr>
                            <td colspan="4">
                                <asp:ValidationSummary ID="val11" runat="server" ValidationGroup="NewItem" />
                            </td>
                        </tr>
                        <tr>
                            <td>Batch Reference</td>
                            <td><asp:TextBox ID="txtBatchreference" runat="server" ReadOnly="true"></asp:TextBox> </td>
                        </tr>
                        <tr id="pnl_add_Customer" runat="server">
                            <td>
                                Customer
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlcustomerInNewItem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcustomerInNewItem_SelectedIndexChanged" Width="200px"></asp:DropDownList>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Please select customer "
                                                                 ControlToValidate="ddlcustomerInNewItem" InitialValue="0" Display="None" ValidationGroup="NewItem"
                                                                           ></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Site
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlsiteInNewItem" Width="200px" runat="server"></asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Please select site"
                                                                 ControlToValidate="ddlsiteInNewItem" InitialValue="0" Display="None" ValidationGroup="NewItem"
                                                                          ></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                          <tr id="pnl_add_Category" runat="server">
                            <td>
                                Category
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlcategoryInNewItem" runat="server" AutoPostBack="True" Width="200px" OnSelectedIndexChanged="ddlcategoryInNewItem_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td>
                                Sub Category
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlSubcategoryInNewItem" runat="server" Width="200px"></asp:DropDownList>
                            </td>
                        </tr>
                          <tr id="pnl_add_Product" runat="server">
                            <td>
                                Product
                            </td>
                            <td>
                                <asp:TextBox ID="TxtproductInNewItem" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Please enter product name"
                                                                 ControlToValidate="TxtproductInNewItem" InitialValue="0" Display="None" ValidationGroup="NewItem"
                                                                          ></asp:RequiredFieldValidator>
                            </td>
                            <td>
                               QTY 
                            </td>
                              <td>
                                   <asp:TextBox ID="txtQtyNewItem" runat="server" Width="75px"></asp:TextBox>
                                  <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtQtyNewItem"></ajaxToolkit:FilteredTextBoxExtender>
                              </td>
                        </tr>
                        <tr id="pnl_add_location" runat="server">
                            <td>Area</td>
                            <td>
                                  <asp:DropDownList ID="ddlareaInNewItem" runat="server" AutoPostBack="true" Width="150px"
                                       OnSelectedIndexChanged="ddlareaInNewItem_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td>Location</td>
                            <td>
                                 <asp:DropDownList ID="ddlLocationInNewItem" Width="150px" runat="server" AutoPostBack="true"
                                      OnSelectedIndexChanged="ddlLocationInNewItem_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                         <tr id="pnl_add_location1" runat="server">
                            <td>Shelf</td>
                            <td>
                                 <asp:DropDownList ID="ddlShelfInNewItem" Width="150px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShelfInNewItem_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                             <td>Bin</td>
                            <td>
                                 <asp:DropDownList ID="ddlBinInNewItem" Width="150px" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="pnl_edit_product" runat="server">
                            <td>
                                Product <asp:HiddenField ID="hid_batchproduct" runat="server" Value="0" /><asp:HiddenField ID="hid_batchid" runat="server" Value="0" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtProductEdit" runat="server" ReadOnly="true" Width="250px"></asp:TextBox>
                            </td>
                            <td>
                                QTY
                            </td>
                            <td>
                                <asp:TextBox ID="txtQtyEdit" runat="server" Width="75px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtQtyEdit"></ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                            </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="pnlBatch" runat="server" CssClass="tab_subheader" Style="border-bottom: solid 1px Silver; width: 100%;font-size:small"> Batch Reference	Details </asp:Panel>
                                <asp:Panel ID="pnlBatchColumns" runat="server" Width="850px">
                                <asp:PlaceHolder ID="ph_batchcolumns" runat="server"></asp:PlaceHolder>
                                    </asp:Panel>
                            </td>
                        </tr>
                        
                    </table>
                    <asp:Panel ID="btnPnlAddButtons" runat="server" Height="75px">
                        <table>
                            <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSaveInNewItem" runat="server" Text="Add" ValidationGroup="NewItem" OnClick="btnSaveInNewItem_Click" />
                                <asp:Button ID="BtncancelInNewItem" runat="server" Text="Cancel" CausesValidation="false" OnClick="BtncancelInNewItem_Click" />
                            </td>
                        </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSaveInNewItem" />
                </Triggers>
            </asp:UpdatePanel>
            </asp:Panel>


        <asp:Panel ID="PnlPopUpInGrid" runat="server" BackColor="White"
                        Style="display:none" Width="800px" Height="560px" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="None">
             <div style="float: right">
               <asp:ImageButton ID="ImageButton5" runat="server" SkinID="ImgSymCancel" ToolTip="Close" OnClick="imgBtnCancel_Click" /></div>
                <div class="sec_header"><asp:Label ID="Label6" runat="server"></asp:Label></div>
             <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePnlPopUpInGrid">
                <ProgressTemplate>
                    <asp:Image ID="imgloading_5" runat="server" ImageUrl="~/media/ico_loading.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePnlPopUpInGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblProductidInPOPUP" runat="server" Visible="false"></asp:Label>
                                  <asp:Label ID="LblListviewMsg" runat="server" ForeColor="Green" EnableViewState="false"></asp:Label>
                                  <asp:ValidationSummary ID="InsertSum1" runat="server" DisplayMode="List" ValidationGroup="InsertSum" />
                                  <asp:ValidationSummary ID="ValidationSummary5" runat="server" DisplayMode="List" ValidationGroup="UpdateSum" />
                            </td>
                        </tr>

                        <tr style="display:none">
                            <td>Batch </td>
                            <td> <asp:DropDownList ID="ddlBatch" runat="server" Width="200px" AutoPostBack="true"
                                 OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList> </td>
                        </tr>
                        <tr>
                            <td>
                                Qty Used
                                <asp:Label ID="lblEid" runat="server" Visible="false"></asp:Label>
                                 <asp:Label ID="LblProductId" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="LblPid" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblQtyUsed" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblBatchId" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td>
                              <asp:TextBox ID="txtQtyUsed" runat="server" Width="50px"></asp:TextBox>
                                   <asp:RangeValidator runat="server" id="rngDate" controltovalidate="txtQtyUsed" type="Integer" ForeColor="Red" ValidationGroup="InsertSum"
                                                                               Display="None" minimumvalue="1" maximumvalue="10000000" errormessage="Please enter valid Qty" />
                                 <asp:RangeValidator runat="server" id="RangeValidator2" controltovalidate="txtQtyUsed" type="Integer" ForeColor="Red" ValidationGroup="UpdateSum"
                                                                               Display="None" minimumvalue="1" maximumvalue="10000000" errormessage="Please enter valid Qty" />

                              <ajaxToolkit:FilteredTextBoxExtender id="txtfilter" runat="server" FilterType="Numbers" TargetControlID="txtQtyUsed"></ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please enter Qty"
                                               ControlToValidate="txtQtyUsed" Display="None" ValidationGroup="UpdateSum" Width="225px">
                                </asp:RequiredFieldValidator>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please enter Qty"
                                               ControlToValidate="txtQtyUsed" Display="None" ValidationGroup="InsertSum" Width="225px"></asp:RequiredFieldValidator>

                            </td>
                            <td>Status</td>
                            <td>
                                 <asp:DropDownList ID="ddlStatus" runat="server" Width="120px"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11112" runat="server" ErrorMessage="Please select status"
                                                                    ControlToValidate="ddlStatus" InitialValue="0" Display="None"
                                                                                 ValidationGroup="UpdateSum" Width="225px"></asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Please select status"
                                                                         ControlToValidate="ddlStatus" InitialValue="0" Display="None"
                                              ValidationGroup="InsertSum" Width="225px"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Condtion
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlcondition" Width="120px" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Requester
                            </td>
                            <td colspan="2">
                                 <asp:TextBox ID="TxtRequester" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator111" runat="server" ErrorMessage="Please enter Requester"
                                               ControlToValidate="TxtRequester" Display="None" ValidationGroup="UpdateSum" Width="225px"></asp:RequiredFieldValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Please enter Requester"
                                               ControlToValidate="TxtRequester" Display="None" ValidationGroup="InsertSum" Width="225px"></asp:RequiredFieldValidator>
                            </td>
                              <td>
                                Notes
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtNote" runat="server" Width="200px"></asp:TextBox>
                            </td>
                           
                        </tr>
                        <tr>
                             <td>
                                Project
                            </td>
                            <td colspan="2">
                                    <asp:DropDownList ID="ddlProject" runat="server" DataValueField="value" DataTextField="name" Width="200px"></asp:DropDownList>
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11213" runat="server" ErrorMessage="Please select project"
                                                                            ControlToValidate="ddlProject" InitialValue="0" Display="None" ValidationGroup="UpdateSum"
                                                                                                 Width="225px"></asp:RequiredFieldValidator>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please select project"
                                                                 ControlToValidate="ddlProject" InitialValue="0" Display="None" ValidationGroup="InsertSum"
                                                                           Width="225px"></asp:RequiredFieldValidator>--%>
                            </td>
                            
                        </tr>
                        <tr>
                          <td>
                              Search for a Batch
                          </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtBatchSearch" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnFind" runat="server" Text="Find" OnClick="btnFind_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                 <asp:Panel ID="PanelFilterdBatchGrid" runat="server" ScrollBars="Auto" Width="760px" Height="250px">
                                <asp:GridView ID="FilterdBatchGrid" runat="server" Width="100%" AutoGenerateColumns="true" ShowHeader="true"
                                     ShowFooter="false" OnRowDataBound="FilterdBatchGrid_RowDataBound">
                                    <Columns>
                                          <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                              <ItemTemplate>
                                                  <asp:CheckBox ID="checkedBatch" ClientIDMode="Static" runat="server" onclick="CheckOne(this)" />
                                                  <asp:Label ID="lblBatchId" runat="server" Visible="false" Text ='<%#Bind("BatchID") %>'></asp:Label>
                                              </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                  </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="pnlUsageColumns" runat="server" ScrollBars="Vertical" Height="200px" Width="590px" Visible="false">
                                <asp:PlaceHolder ID="pUsageCustomer" runat="server"></asp:PlaceHolder>
                                    </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hUsage" runat="server" Value="" />
                                  <asp:ImageButton ID="btnupdate" runat="server" SkinID="ImgUpdate" ValidationGroup="UpdateSum" Visible="false"
                                     CommandName="Update" OnClick="btnupdate_Click" ImageAlign="AbsBottom" />
                                   <asp:ImageButton ID="Btninsert" runat="server" SkinID="ImgAdd" ValidationGroup="InsertSum" Visible="false"
                                             CommandName="Insert" OnClick="Btninsert_Click" ImageAlign="AbsBottom" />
                                  <asp:ImageButton ID="btncancel" runat="server" SkinID="ImgCancel" ImageAlign="AbsBottom"
                                       CommandName="Cancel" OnClick="btncancel_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlBatch" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            </asp:Panel>
        <asp:Label ID="l11" runat="server" EnableViewState="false"></asp:Label>
     
        <ajaxToolkit:ModalPopupExtender ID="mdlPopupTransferItem" runat="server" BackgroundCssClass="modalBackground"
            TargetControlID="imgPopupTransferItem" PopupControlID="pnlTransferItem" CancelControlID="imghistoryCancel">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Label ID="imgPopupTransferItem" runat="server" />
        <asp:Panel ID="pnlTransferItem" runat="server" BackColor="White" Style="display: none;"
            Width="800px" Height="600px" BorderStyle="Double" BorderColor="LightSteelBlue"
            ScrollBars="Auto">
            <div style="float: right">
                <asp:ImageButton ID="imghistoryCancel" runat="server" SkinID="ImgSymCancel" ToolTip="Close" /></div>
            <div class="sec_header">
                Transfer Items</div>
            <br />
            <asp:UpdateProgress ID="uprogress1" runat="server">
                <ProgressTemplate>
                    <asp:Image ID="imgLoad" ImageUrl="~/media/ico_loading.gif" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:HiddenField ID="hfInventoryId" runat="server" Value="0" />
            <asp:HiddenField ID="hfCategoryId" runat="server" Value="0" />
            <asp:HiddenField ID="hfSubcategoryId" runat="server" Value="0" />
            <asp:HiddenField ID="hfPartNumber" runat="server" Value="" />
            <asp:HiddenField ID="hfDescription" runat="server" Value="" />
            <asp:HiddenField ID="hfBarcode" runat="server" Value="" />
            <%--<table style="padding-left: 10px;">
                                    <tr>
                                        <td>
                                            Barcode
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtItemBarcode" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImaSearchBarcode" runat="server" SkinID="ImgSearch" ImageAlign="AbsMiddle"
                                                OnClick="ImaSearchBarcode_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImgViewAllBarcode" runat="server" SkinID="ImgViewAll" ImageAlign="AbsMiddle"
                                                OnClick="ImgViewAllBarcode_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblTransferMsg" runat="server" ForeColor="Green" Font-Size="9pt"></asp:Label>
                                        </td>
                                    </tr>
                                </table>--%>
            <br />
            <asp:Label ID="lblTransferMsg" runat="server" ForeColor="Red" Font-Size="10pt"></asp:Label>
            <asp:ValidationSummary ID="Val1" runat="server" DisplayMode="BulletList" ValidationGroup="tr" />
            <table>
                <tr>
                    <td>
                        <b>Date</b>
                        <br />
                        <br />
                        <asp:Label ID="lblDate" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center">
                        <b>Current</b>
                        <br />
                        <br />
                        <asp:Label ID="lblCurrentQty" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td>
                        <b>Amendment</b>
                        <br />
                        <br />
                        <asp:TextBox ID="txtAmendment" runat="server" Width="60px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please enter amendment"
                            ControlToValidate="txtAmendment" ValidationGroup="tr">*</asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="filterAmendment" runat="server" TargetControlID="txtAmendment"
                            ValidChars="0123456789-+/">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <b>Transfer to Site</b>
                        <br />
                        <br />
                        <asp:DropDownList ID="ddlTransferSite" runat="server">
                        </asp:DropDownList>
                        <ajaxToolkit:CascadingDropDown ID="ccdtransferSite" runat="server" TargetControlID="ddlTransferSite"
                            Category="Site" PromptText="Please select..." PromptValue="0" ServicePath="~/InventoryMgr.asmx"
                            ServiceMethod="GetInventorySite" LoadingText="[Loading...]" />
                    </td>
                    <td>
                        <b>Notes</b>
                        <br />
                        <br />
                        <asp:TextBox ID="txtTransferNotes" runat="server" TextMode="MultiLine">
                        </asp:TextBox>
                    </td>
                    <td style="vertical-align: middle;" valign="middle">
                        <asp:ImageButton ID="imgTransferApply" runat="server" SkinID="ImgApply" ImageAlign="AbsMiddle"
                            OnClick="imgTransferApply_Click" ValidationGroup="tr" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvTransferItems" runat="server" Width="80%" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="EntryDate" HeaderText="Date and Time" ItemStyle-Width="120px" />
                    <asp:TemplateField HeaderText="Change">
                        <ItemTemplate>
                            <asp:Label ID="lblChange" runat="server" Text='<%# FormateQty(DataBinder.Eval(Container.DataItem,"Qty").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ReasonCode" HeaderText="Reason Code" />
                    <asp:BoundField DataField="Notes" HeaderText="Notes" ItemStyle-Width="250px" />
                    <asp:BoundField DataField="ContractorName" HeaderText="Modified By" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <%-- <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
            TargetControlID="pnlUseShow" ExpandControlID="pnlItems" CollapseControlID="pnlItems"
            TextLabelID="lblinven" CollapsedText="Show Summary of Usage" ExpandedText="Hide Summary of Usage"
            ExpandedImage="media/ico_update.png" Collapsed="true" SuppressPostBack="true">
        </ajaxToolkit:CollapsiblePanelExtender>--%>
        <asp:Panel ID="pnlUse" runat="server" Visible="false">
           
            <div style="padding-left: 10px; width: 100%;">
                <%-- <asp:Panel ID="pnlItems" runat="server">
                    <asp:LinkButton ID="lblinven" runat="servmdlopoer"></asp:LinkButton>
                </asp:Panel>--%>
                <div class="clr">
                </div>
                <br />
                <%-- Upload inventory use&nbsp;
                                    <asp:FileUpload ID="fileUpload" runat="server" />&nbsp;<asp:ImageButton ID="imgUpload1"
                                        SkinID="ImgUpload" runat="server" OnClick="imgUpload1_Click" ImageAlign="AbsMiddle" />&nbsp--%>
                <%-- <asp:LinkButton ID="lnkShow" runat="server" Text="Show Inventory Items Table" OnClick="lnkShow_Click"
                                        ClientIDMode="Static"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkHide" runat="server" Text="Hide Inventory Items Table" OnClick="lnkHide_Click"
                                        ClientIDMode="Static" Visible="false"></asp:LinkButton>
                                    <div class="clr">
                                    </div>
                                    <br />--%>
                  <asp:Panel ID="pnlUseShow" runat="server" Width="100%">
                   <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">Batch Summary</div>
                             <asp:Panel ID="pnlBatchGrid" runat="server" ScrollBars="Horizontal" Width="1250px">
                                  <asp:GridView ID="GridBatch" runat="server" AutoGenerateColumns="true" ShowHeader="true" ShowFooter="false"
                                Width="100%" OnPreRender="GridBatch_PreRender" OnRowCommand="GridBatch_RowCommand" OnRowDataBound="GridBatch_RowDataBound" >
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                          <ItemTemplate>
                                              <asp:ImageButton ID="btnEdit" SkinID="ImgSymEdit" runat="server" Text="Edit" ImageAlign="AbsMiddle"
                                                    CommandName="Edit1" CommandArgument='<%#Bind("BatchID") %>'  />
                                                <%--<asp:ImageButton ID="ImgDelete" runat="server" OnClientClick="return confirm('Do you want to delete this record?');"
                                                     Visible="false" CommandArgument='<%#Bind("_id") %>' SkinID="ImgSymDel" ImageAlign="AbsMiddle" CommandName="Delete1" />--%>
                                          </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                                 </asp:Panel>
               <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                Summary of Usage</div>
                    <asp:Label ID="lblItemMsg" runat="server" ForeColor="Green" Visible="false" Width="100%"></asp:Label>
                    <span style="color: Gray;">The following grid displays a number of items deployed for
                        the particular product:<b>&nbsp<asp:Literal ID="ltlProduct" runat="server"></asp:Literal>&nbsp
                            -</b> Site: &nbsp<b><asp:Literal ID="ltlSite" runat="server"></asp:Literal></b><br />
                        <asp:GridView ID="gvUsageSummary" runat="server" EmptyDataText="No items deployed.">
                            <Columns>
                                <asp:TemplateField HeaderText="Use" HeaderStyle-CssClass="header_bg_l">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUse" runat="server" Text='<%#Bind("Use") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Number Deployed" HeaderStyle-CssClass="header_bg_r">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoDeployed" runat="server" Text='<%#Bind("NumberDeployed") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <br /><br />
                        <asp:UpdatePanel ID="upanleUsage" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                        <asp:Panel ID="PnlListview" runat="server" Visible="false">
                           
                              <asp:Label ID="Label5" runat="server" ForeColor="Green" EnableViewState="false"></asp:Label>
                            <div style="float:right;padding-bottom:5px;padding-right:20px">
                                <asp:Button ID="btnadd" runat="server" CssClass="button deffinity medium" Text="Add Usage Entry" OnClick="btnadd_Click" />
                            <asp:Button ID="btnFieldConfigurator" runat="server" Text="Field Configurator" OnClick="btnFieldConfigurator_Click" CssClass="button deffinity medium" Visible="false" />
                                </div>
                            <div style="padding-top:5px;">
                                <asp:Panel ID="pnlGridUsage" runat="server" ScrollBars="Horizontal" Width="1250px">
                            <asp:GridView ID="GridUsage" runat="server" AutoGenerateColumns="true" ShowHeader="true" ShowFooter="false"
                                OnRowDataBound="GridUsage_RowDataBound" Width="100%" OnRowCommand="GridUsage_RowCommand" OnRowEditing="GridUsage_RowEditing"
                                 OnPreRender="GridUsage_PreRender">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                          <ItemTemplate>
                                              <asp:ImageButton ID="btnEdit" SkinID="ImgSymEdit" runat="server" Text="Edit" ImageAlign="AbsMiddle"
                                                   CommandArgument='<%#Bind("_id") %>' CommandName="Edit1" />
                                                <asp:ImageButton ID="ImgDelete" runat="server" OnClientClick="return confirm('Do you want to delete this record?');"
                                                     Visible="false" CommandArgument='<%#Bind("_id") %>' SkinID="ImgSymDel" ImageAlign="AbsMiddle" CommandName="Delete1" />
                                          </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                                    </asp:Panel>
                                </div>
               
            </asp:Panel>
                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </asp:Panel>
         
        <table width="100%">
            <tr>
                <td>
                    <script>
                        //$(document).ready(function () {
                        //    $("#PnlSummary").hide();
                        //    $("body").on("click", "#btnViewInventory", function (event) {
                        //        event.preventDefault();
                        //        $("#pnlInventory").toggle();
                        //        $("#PnlSummary").hide();
                        //    });
                        //    $("body").on("click", "#btnViewUsageSummary0", function (event) {
                        //        event.preventDefault();
                        //        $("#PnlSummary").toggle();
                        //        $("#pnlInventory").hide();
                        //    });

                        //});
                       </script>
                    <asp:Panel ID="pnltab_subheader" runat="server">
                    <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 100%;" runat="server">
                        Inventory by Site
                        <div style="float: right; padding-right: 1px;height:50px">
                            &nbsp;&nbsp;
                          
                            <asp:Label ID="lblbtnViewUsageSummary0" runat="server"></asp:Label>
                            <%--<ajaxToolkit:ModalPopupExtender ID="lblbtnViewUsageSummary0_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton4" PopupControlID="PnlSummary" TargetControlID="lblbtnViewUsageSummary0">
                            </ajaxToolkit:ModalPopupExtender>--%>
                            <asp:Button ID="btnAddNewItem" runat="server" ImageUrl="~/media/btn_add_new_item.gif" CssClass="button deffinity medium" Text="Add New Item"
                                                                 ImageAlign="AbsBottom" OnClick="btnAddNewItem_Click" />
                            <asp:ImageButton ID="imgbtnViewInven" runat="server" OnClick="imgbtnViewInven_Click" ImageAlign="AbsBottom"
                                SkinID="ImgViewAll" ToolTip="View All Inventory" Visible="false" />
                         
                        </div>
                        </div>
                       
                        <table style="width:100%">
                            <tr>
                                <td>
                                      <div style="float: right; padding-right: 1px;height:30px">
                                               <asp:Button ID="btnViewInventory" runat="server" CssClass="button deffinity medium"  Text="View Inventory"
                                                                                     ClientIDMode="Static" OnClick="btnViewInventory_Click" />
                                                <asp:Button ID="btnViewUsageSummary0" runat="server" CssClass="button deffinity medium"
                                                                                     Text="View Usage Summary" ClientIDMode="Static" OnClick="btnViewUsageSummary0_Click" />
                                        </div>
                                    <br /><br />
                                <asp:Panel ID="PnlSummary"  runat="server"
                                    Width="100%" ClientIDMode="Static" Visible="false" >
              <div class="sec_header">Usage Summary Details</div>
            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePnlSummary">
                <ProgressTemplate>
                    <asp:Image ID="imgloading_3" runat="server" ImageUrl="~/media/ico_loading.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
              <asp:UpdatePanel ID="UpdatePnlSummary" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
                  <ContentTemplate>
                    
                        <asp:GridView ID="GridUsageSummary" runat="server" AutoGenerateColumns="false" Width="100%" Visible="false">
                            <Columns>
                               <asp:BoundField DataField="ItemDescription" HeaderText="Product"  HeaderStyle-CssClass="header_bg_l" />
                                <asp:BoundField DataField="OpeningStock" HeaderText="Opening Stock" ItemStyle-HorizontalAlign="Right" />
                                   <asp:BoundField DataField="QtyUsed" HeaderText="Qty Used" ItemStyle-HorizontalAlign="Right" />
                                   <asp:BoundField DataField="Status" HeaderText="Status" />
                                   <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By" />
                                   <asp:BoundField DataField="ProjectName" HeaderText="Project"  HeaderStyle-CssClass="header_bg_r"/>
                            </Columns>
                        </asp:GridView>
                      <asp:Panel ID="pnlUsagesummary" runat="server" ScrollBars="Horizontal" Width="1250px">
                    <asp:GridView ID="GridUsageSummary1" runat="server" AutoGenerateColumns="true" Width="100%" OnRowDataBound="GridUsageSummary1_RowDataBound"></asp:GridView>
                          </asp:Panel>
                  </ContentTemplate>
              </asp:UpdatePanel>
        </asp:Panel>
                                </td></tr>
                            <tr>
                                <td>
                                <asp:Panel ID="pnlInventory" runat="server" ClientIDMode="Static" Width="100%">
                        <asp:ValidationSummary ID="grdValidationSummery" runat="server" ValidationGroup="AddInvnt" />
                        <asp:HiddenField ID="hfID" runat="server" Value="0" />
                        <asp:HiddenField ID="hfSiteID" runat="server" Value="0" />
                        <asp:HiddenField ID="hfSubcatID" runat="server" Value="0" />
                                  
                        <asp:GridView ID="grdInventory" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="ID" Width="100%" OnPageIndexChanging="grdInventory_PageIndexChanging"
                            Visible="true" OnRowCancelingEdit="grdInventory_RowCancelingEdit" OnRowCommand="grdInventory_RowCommand"
                            OnRowEditing="grdInventory_RowEditing" OnRowUpdating="grdInventory_RowUpdating"
                            OnRowDeleting="grdInventory_RowDeleting" 
                            EmptyDataText="No products available" 
                            onrowdatabound="grdInventory_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit">
                                    <HeaderStyle Width="55px"   />
                                    <ItemStyle Width="55px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandArgument='<%# Bind("id")%>'
                                            CommandName="MoreOptions" ImageUrl="~/media/ico_edit.png" ToolTip="Edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="LinkButtonUpdate" runat="server" CommandArgument='<%# Bind("id")%>'
                                            CommandName="Update" ImageUrl="~/media/ico_update.png" ToolTip="Update" ValidationGroup="AddInvnt" />
                                        <asp:ImageButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                            ImageUrl="~/media/ico_cancel.png" ToolTip="Cancel" />
                                    </EditItemTemplate>
                                    <HeaderStyle CssClass="header_bg_l" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <ajaxToolkit:AnimationExtender ID="MyExtender" runat="server" TargetControlID="pnlOriginalMaterial">
                                            <Animations>
            <OnMouseOver>
            <FadeIn Duration=".5" Fps="20" />
            </OnMouseOver>
                                            </Animations>
                                        </ajaxToolkit:AnimationExtender>
                                        <ajaxToolkit:HoverMenuExtender ID="hmeMaterials" runat="server" EnableViewState="false"
                                            OffsetY="26" PopDelay="0" PopupControlID="pnlOriginalMaterial" PopupPosition="Right"
                                            TargetControlID="imgContractor" />
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                            Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        <div id="pnlOriginalMaterial" runat="server" class="PrepRecipeDetails" style="display: none;">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.OriginalData) %>'
                                                Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" SortExpression="CategoryName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Category" SortExpression="SubCategoryName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubCategory" runat="server" Text='<%# Bind("SubCategoryName") %>'> 
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesc" runat="server" Text='<% #Bind("ItemDescription")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manufacturer" SortExpression="ManufacturerName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManufcturer" runat="server" Text='<%# Bind("ManufacturerName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Site">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSite" runat="server" Text='<%# Bind("Sitename") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="120px" />
                                    <ItemStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening Stock" SortExpression="QTY" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQTY" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QTY<br/> Available">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQTYAvailable" runat="server" Text='<%# getActualStackLaevel(Eval("Id").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QTY<br/>Deployed " Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQTYDeployed" runat="server" Text='<%# GetQtyInUseFromIm_PSDProducts(Eval("Id").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Notes" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="150px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Area">
                                    <ItemTemplate>
                                        <asp:Label ID="lblarea" runat="server" Text='<%# Bind("AreaName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Shelf">
                                    <ItemTemplate>
                                        <asp:Label ID="lblShelf" runat="server" Text='<%# Bind("ShelfName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Bin Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBinNumber" runat="server" Text='<%# Bind("BinName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Usage">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="lnkUsage" runat="server" CausesValidation="false" Text="Usage" CommandName="Usage" CommandArgument='<%# Bind("id")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Batch">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="lnkBatch" runat="server" CausesValidation="false" Text="Add Batch" CommandName="Batch" CommandArgument='<%# Bind("id")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transfer Items">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgTransfer" runat="server" CausesValidation="false" CommandName="Transfer"
                                            ImageUrl="~/media/ico_more_options.png" CommandArgument='<%# Bind("ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgHis" runat="server" CausesValidation="false" CommandName="History"
                                            ImageUrl="~/media/ico_history.gif" CommandArgument='<%# Bind("ID") %>' ToolTip="View History" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButtonDelete1" runat="server" OnClientClick="return confirm('Do you want to delete this record?');"
                                            CommandName="Delete" CommandArgument='<%# Bind("ID")%>' SkinID="ImgSymDel" ToolTip="Delete">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle CssClass="header_bg_r" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                         </asp:Panel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="imgbtnUpdateMaterial" />
        <asp:PostBackTrigger ControlID="btnSaveMaterial"  />
        <asp:PostBackTrigger ControlID="ImageButton1" />
        <asp:PostBackTrigger ControlID="imgbtnExportToExcel" />
        <asp:PostBackTrigger ControlID="btnsearch" />
        <asp:PostBackTrigger ControlID="ddlcustomerInSearch" />
        <asp:AsyncPostBackTrigger ControlID="ddlSite" EventName="SelectedIndexChanged" />
     <%--  <asp:AsyncPostBackTrigger ControlID="grdInventory" EventName="grdInventory_RowEditing" />--%>
       <%-- <asp:PostBackTrigger ControlID="grdInventory" />--%>
    </Triggers> 
</asp:UpdatePanel>
