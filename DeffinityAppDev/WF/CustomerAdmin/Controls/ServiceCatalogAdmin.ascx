<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ServiceCatalogAdmin_new_1" Codebehind="ServiceCatalogAdmin.ascx.cs" %>

<script language="javascript" type="text/javascript">
    function LBP_Dsc() {
        try {
            var pecent = document.getElementById('<%=txt_labour_LPercent.ClientID %>').value;
            if (pecent == '') {
                alert('Please enter Markup');
                return false;
            }
            var bPrice = document.getElementById('<%=txt_labour_BuyingPrice.ClientID %>').value;
            if (bPrice == '') {
                alert('Please enter Buying Price');
                return false;
            }

            document.getElementById('<%=txt_labour_SellingPrice.ClientID %>').value = floatmyPriceInc(bPrice, pecent);
        }
        catch (err) {
            //Handle errors here
        }
    }

    function LSP_Dsc() {
        try {
            var pecent = document.getElementById('<%=txt_labour_LSPercent.ClientID %>').value;
            if (pecent == '') {
                alert('Please enter Discount');
                return false;
            }
            var sPrice = document.getElementById('<%=txt_labour_SellingPrice.ClientID %>').value;
            if (sPrice != '') {
                var temp = floatmyPriceInc(sPrice, pecent);
                //alert(temp);
                var tempDis = parseFloat(parseFloat(temp) - parseFloat(sPrice)).toFixed(2);
                //alert(tempDis);
                document.getElementById('<%=txt_labour_LDPrice.ClientID %>').value = parseFloat(parseFloat(sPrice) - parseFloat(tempDis)).toFixed(2);

            }
        }
        catch (err) {
            //Handle errors here
        }
    }

    function PBP_Dsc() {
        try {
            var PPecent = document.getElementById('<%=txt_material_PPercent.ClientID %>').value;
            if (PPecent == '') {
                alert('Please enter Markup');
                return false;
            }
            var pBprice = document.getElementById('<%=txt_material_MBuyingPrice.ClientID %>').value;
            if (pBprice == '') {
                alert('Please enter Buying Price');
                return false;
            }
            document.getElementById('<%=txt_material_MSellingPrice.ClientID %>').value = floatmyPriceInc(pBprice, PPecent);
        }
        catch (err) {
            //Handle errors here
        }
    }

    function PSP_Dsc() {
        try {
            var PPecent = document.getElementById('<%=txt_material_PSPercent.ClientID %>').value;
            if (PPecent == '') {
                alert('Please enter Discount');
                return false;
            }
            var pSprice = document.getElementById('<%=txt_material_MSellingPrice.ClientID %>').value;
            if (pSprice != '') {
                var temp = floatmyPriceInc(pSprice, PPecent);
                var tempDis = parseFloat(parseFloat(temp) - parseFloat(pSprice)).toFixed(2);
                document.getElementById('<%=txt_material_PDPrice.ClientID %>').value = parseFloat(parseFloat(pSprice) - parseFloat(tempDis)).toFixed(2);

            }
        }
        catch (err) {
            //Handle errors here
        }

    }

    function floatmyPriceDec(pval, percent) {
        var myval1 = parseFloat(pval) - parseFloat(((pval * (percent / 100))));
        return parseFloat(myval1).toFixed(2);

    }

    function floatmyPriceInc(val, percent) {
        var myval = parseFloat(val) + parseFloat(((val * (percent / 100))));
        return parseFloat(myval).toFixed(2);

    }

    function SerB_Dsc() {
        try {
            var sPecent = document.getElementById('<%=txt_service_SPercent.ClientID %>').value;
            if (sPecent == '') {
                alert('Please enter Markup');
                return false;
            }
            var sbuy = document.getElementById('<%=txt_service_SetupBuy.ClientID %>').value;
            var mbuy = document.getElementById('<%=txt_service_MaterialsBuy.ClientID %>').value;
            var lbuy = document.getElementById('<%=txt_service_LabourBuy.ClientID %>').value;
            if (sbuy != '') {
                document.getElementById('<%=txt_service_SetupSell.ClientID %>').value = floatSell(sbuy, sPecent);

            }
            if (mbuy != '') {
                document.getElementById('<%=txt_service_MaterialsSell.ClientID %>').value = floatSell(mbuy, sPecent);

            }
            if (lbuy != '') {
                document.getElementById('<%=txt_service_LabourSell.ClientID %>').value = floatSell(lbuy, sPecent);

            }
            var ssell = document.getElementById('<%=txt_service_SetupSell.ClientID %>').value;
            var msell = document.getElementById('<%=txt_service_MaterialsSell.ClientID %>').value;
            var lsell = document.getElementById('<%=txt_service_LabourSell.ClientID %>').value;

            document.getElementById('<%=txt_service_TotBuy.ClientID %>').value = parseFloat(parseFloat(sbuy) + parseFloat(mbuy) + parseFloat(lbuy)).toFixed(2);
            document.getElementById('<%=txt_service_TotSell.ClientID %>').value = parseFloat(parseFloat(ssell) + parseFloat(msell) + parseFloat(lsell)).toFixed(2);
        }
        catch (err) {
            //Handle errors here
        }
    }

    function SerS_Dsc() {
        try {
            var sPecent = document.getElementById('<%=txt_service_SDPercent.ClientID %>').value;
            if (sPecent == '') {
                alert('Please enter Discount');
                return false;
            }
            var totSell = document.getElementById('<%=txt_service_TotSell.ClientID %>').value;
            if (totSell != '') {
                var temp = parseFloat(floatSell(totSell, sPecent));
                var tempDisc = parseFloat(parseFloat(temp) - parseFloat(totSell)).toFixed(2);
                document.getElementById('<%=txt_service_SDprice.ClientID %>').value = parseFloat(parseFloat(totSell) - parseFloat(tempDisc)).toFixed(2);
            }

        }
        catch (err) {
            //Handle errors here
        }
    }



    function floatBuy(value2, percent2) {
        var val2 = (parseFloat(value2) - parseFloat(((parseFloat(value2) * (parseFloat(percent2) / 100)))));
        return parseFloat(val2).toFixed(2);
    }

    function floatSell(value1, percent1) {
        var val1 = (parseFloat(value1) + parseFloat(((parseFloat(value1) * (parseFloat(percent1) / 100)))));
        return parseFloat(val1).toFixed(2);

    }
                                
</script>
 <asp:Panel ID="Panel_fileupdload" runat="server" Width="100%" >
                <div>
                    <table style="width: 700px; float: right;">
                        <tr>
                        <td>
              <asp:LinkButton ID="btnCopyCatalogue" runat="server" Text="Copy Catalogue to all customers"
                                    Font-Bold="true" ForeColor="Black" onclick="btnCopyCatalogue_Click1"></asp:LinkButton>
                        </td>
                            <td>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                                    TargetControlID="PanelCsv" ExpandControlID="PnlTitle" CollapseControlID="PnlTitle"
                                    TextLabelID="Lbl1" CollapsedText="Upload Excel File " ExpandedText="Upload Excel File "
                                    ImageControlID="UploadImg" Collapsed="true" SuppressPostBack="true">
                                </ajaxToolkit:CollapsiblePanelExtender>
                                <asp:Panel Width="100%" ID="PnlTitle1" runat="server" HorizontalAlign="Right">
                                    <div id="PnlTitle" runat="server" style="cursor: pointer">
                                        <asp:Label ID="Lbl1" runat="server" Text="Upload Excel File " Font-Bold="true" ForeColor="Black"
                                            Style="cursor: pointer;"></asp:Label></div>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnDownloadExcel" runat="server" Text="Download Excel File" OnClick="btnDownloadExcel_Click"
                                    Font-Bold="true" ForeColor="Black"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="clr">
                </div>
                <asp:Panel ID="PanelCsv" runat="server" Width="100%" Style="overflow: hidden">
                    <div class="tab_header_Bold">
                        Excel file upload
                    </div>
                    <div style="width: 50%; margin-left: 5px">
                        <iframe id="iframeMpp" height="100px" width="100%" runat="server" scrolling="no"
                            frameborder="0"></iframe>
                    </div>
                </asp:Panel>
            </asp:Panel>
<div style="width:100%"><asp:Label ID="lblMsgDisplay" runat="server" EnableViewState="false"></asp:Label> </div>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
    <td>Category</td>
    <td><asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
     <ajaxToolkit:CascadingDropDown ID="ccdCategory" runat="server" TargetControlID="ddlCategory"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/ServiceCatalogSrv.asmx"
                                ServiceMethod="GetCategoryByAdmin" LoadingText="[Loading Category...]" BehaviorID="ccdCategory1" ClientIDMode="Static" /> 
        <asp:ImageButton ID="btnCategoryedit" runat="server" SkinID="ImgSymEdit" 
            onclick="btnCategoryedit_Click" ImageAlign="AbsMiddle" /> 
            <asp:ImageButton ID="btnCategoryAdd" 
            runat="server" SkinID="ImgSymAdd" onclick="btnCategoryAdd_Click" ImageAlign="AbsMiddle" /> 
            
            </td>
    <td>Sub Category</td>
    <td><asp:DropDownList ID="ddlSubCategory" runat="server"></asp:DropDownList> 
    <ajaxToolkit:CascadingDropDown ID="ccdSubCategory" runat="server" TargetControlID="ddlSubCategory"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/ServiceCatalogSrv.asmx"
                                ServiceMethod="GetSubCategoryByAdmin" LoadingText="[Loading Sub Category...]" BehaviorID="ccdSubCategory1" ClientIDMode="Static" ParentControlID="ddlCategory" /> 
        <asp:ImageButton ID="btnSubcategoryedit" runat="server" SkinID="ImgSymEdit" 
            onclick="btnSubcategoryedit_Click" ImageAlign="AbsMiddle" />
        <asp:ImageButton ID="btnSubCategoryAdd" runat="server" SkinID="ImgSymAdd" 
            onclick="btnSubCategoryAdd_Click" ImageAlign="AbsMiddle" /></td>
    <td>Type</td>
    <td><asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlType_SelectedIndexChanged"><asp:ListItem Value="1" Text="Labour" Enabled="false"></asp:ListItem>
    <asp:ListItem Value="2" Text="Material" Selected="True"></asp:ListItem>
    <asp:ListItem Value="3" Text="Service" Enabled="false"></asp:ListItem> </asp:DropDownList> </td>
    <td><asp:ImageButton ID="btnView" runat="server" SkinID="ImgView" 
            onclick="btnView_Click" /> <asp:ImageButton ID="btnAddLabour" runat="server" SkinID="ImgNew" /> &nbsp;
            <asp:ImageButton ID="btnAddMaterial" runat="server" SkinID="ImgNew" /> 
            <asp:ImageButton ID="btnAddService" runat="server" SkinID="ImgNew" />
         </td>
    <td> </td>
</tr>
<tr><td colspan="8">
<ajaxToolkit:ModalPopupExtender ID="model_category" runat="server" 
                BackgroundCssClass="modalBackground" CancelControlID="btnCategoryCancel" 
                PopupControlID="pnl_category" TargetControlID="btnCategoryAdd" Enabled="true">
            </ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnl_category" runat="server" Height="150px" Width="300px" style="display:none;background-color:White;">
 
<div class="tab_header_Bold">
                        Category</div>
<div><asp:RequiredFieldValidator ID="reqCategory" runat="server" ControlToValidate="txtCategoryName" ErrorMessage="Please enter Category" Display="Dynamic" ValidationGroup="val_sum_category"></asp:RequiredFieldValidator></div>
<table>
<tr>
<td>Category <asp:HiddenField ID="h_edit_categoryID" runat="server" Value="0" /> </td><td><asp:TextBox ID="txtCategoryName" runat="server"></asp:TextBox> 
 </td> </tr>
<tr><td></td><td><asp:ImageButton ID="btnAddCategory" runat="server" 
        SkinID="ImgSubmit"  ValidationGroup="val_sum_category" 
        onclick="btnAddCategory_Click1" /> <asp:ImageButton ID="btnCategoryCancel" runat="server" SkinID="ImgCancel" CausesValidation="false" /> </td></tr>
</table>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="model_subcategory" runat="server" 
                BackgroundCssClass="modalBackground" CancelControlID="btnSubcategoryCancel" 
                PopupControlID="pnl_subcategory" TargetControlID="btnSubCategoryAdd" Enabled="true" >
            </ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnl_subcategory" runat="server" Height="150px" Width="300px" style="display:none;background-color:White;">
<div class="tab_header_Bold">
                        Sub Category</div>
                        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubcategoryName" ErrorMessage="Please enter Sub Category" Display="Dynamic" ValidationGroup="val_sum_sub_category"></asp:RequiredFieldValidator></div>
<table>
<tr>
<td>Sub Category <asp:HiddenField ID="h_edit_subcategoryID" runat="server" Value="0" /></td><td><asp:TextBox ID="txtSubcategoryName" runat="server"></asp:TextBox> 
 <asp:HiddenField ID="h_categoryID" runat="server" Value="0" /> </td> </tr>
<tr><td></td><td><asp:ImageButton ID="btnAddSubCategory" runat="server" 
        SkinID="ImgSubmit" ValidationGroup="val_sum_sub_category" 
        onclick="btnAddSubCategory_Click1" /> <asp:ImageButton ID="btnSubcategoryCancel" runat="server" SkinID="ImgCancel" /> </td></tr>
</table>
</asp:Panel>
</td>
</tr>
<tr><td colspan="8">
<asp:Panel ID="pnl_Labour" runat="server" Width="100%">
<asp:GridView ID="Grid_Labour" runat="server" Width="100%" 
        onrowcommand="Grid_Labour_RowCommand">
  <Columns>
                                <asp:TemplateField>
                                    <ItemStyle Wrap="false" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnLabourEdit" runat="server" CausesValidation="false" CommandName="edit_labour"
                                            CommandArgument="<%# Bind('ID')%>" ImageUrl="~/media/ico_edit.png" ToolTip="Edit">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="header_bg_l" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("EngineerDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="300px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate Type">
                                    <ItemTemplate>
                                        <asp:Label ID="rtype" runat="server" Text='<%# Bind("RateTypeName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Buying Price" SortExpression="BuyingPrice">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("BuyingPrice", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Selling Price" SortExpression="SellingPrice">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("SellingPrice", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount Price" SortExpression="DiscountPrice">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLdscnt" runat="server" Text='<%# Bind("DiscountPrice", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Units" SortExpression="UnitConsumption">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtuc" runat="server" Text='<%# Bind("UnitConsumption") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbluc" runat="server" Text='<%# Bind("UnitConsumption", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notes" SortExpression="Notes">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <ajaxToolkit:AnimationExtender ID="MyExtender" runat="server" TargetControlID="pnlOriginalImage">
                                            <Animations>
          
            <OnMouseOver>
            <FadeIn Duration=".5" Fps="20" />
            </OnMouseOver>
                                            </Animations>
                                        </ajaxToolkit:AnimationExtender>
                                        <ajaxToolkit:HoverMenuExtender ID="hmeDetails" runat="server" TargetControlID="imgContractor"
                                            PopupControlID="pnlOriginalImage" PopDelay="0" PopupPosition="Left" EnableViewState="false"
                                            OffsetY="26" />
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                            Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        <div id="pnlOriginalImage" runat="server" class="PrepRecipeDetails" style="display: none;">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.OriginalData) %>'
                                                Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="More Options" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgLMOptions" runat="server" CausesValidation="false" CommandName="MoreOptions"
                                            ImageUrl="~/media/ico_more_options.png" CommandArgument='<%# Bind("ID") %>'  />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButtonDelete1" runat="server" CommandName="Delete_labour" CommandArgument="<%# Bind('ID')%>"
                                            SkinID="ImgSymDel" ToolTip="Delete" ImageAlign="Middle"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle CssClass="header_bg_r" />
                                </asp:TemplateField>
                            </Columns>
</asp:GridView>

<ajaxToolkit:ModalPopupExtender ID="model_labour" runat="server" 
                BackgroundCssClass="modalBackground" CancelControlID="btnCancelLabour" 
                PopupControlID="pnl_addupdate_labour" TargetControlID="btnAddLabour" Enabled="true">
            </ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnl_addupdate_labour" runat="server" Height="430px" Width="850px" style="display:none;background-color:White;">
 <div class="tab_header_Bold">
                        Labour</div>
<asp:ValidationSummary ID="Val_labour" runat="server" ValidationGroup="val_pnl_labour" />
<asp:RequiredFieldValidator ID="rf_labour_category" runat="server" ControlToValidate="ddlCategory" InitialValue="0" ErrorMessage="Please Select Category" Display="None" ValidationGroup="val_pnl_labour" ></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rf_labour_subcategory" runat="server" ControlToValidate="ddlSubCategory" InitialValue="0" ErrorMessage="Please Select Sub Category" Display="None" ValidationGroup="val_pnl_labour" ></asp:RequiredFieldValidator>
<table width="100%" cellpadding="0" cellspacing="0">
                        
                            <tr>
                            <td>
                             <asp:HiddenField ID="h_labourID" runat="server" Value="0" />    Description
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txt_labour_Description" runat="server" MaxLength="200" TextMode="MultiLine"
                                    Width="650px" Height="70px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldDescription" runat="server" ControlToValidate="txt_labour_Description"
                                    Display="None" ErrorMessage="Please enter Description" SetFocusOnError="True"
                                    ValidationGroup="val_pnl_labour"></asp:RequiredFieldValidator>
                            </td>
                           
                            </tr>
                                <tr>
                                    <td >
                                        Buying Price
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_labour_BuyingPrice" runat="server" CausesValidation="True" 
                                            Style="text-align: right;" Width="70px"></asp:TextBox>
                                        <asp:CompareValidator ID="cmpRate" runat="server" 
                                            ControlToValidate="txt_labour_BuyingPrice" Display="None" 
                                            ErrorMessage="Please enter valid Buying Price" Operator="DataTypeCheck" 
                                            SetFocusOnError="True" Type="Double" ValidationGroup="val_pnl_labour"></asp:CompareValidator>
                                    </td>
                                    <td >
                                        Mark up %
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_labour_LPercent" runat="server" Style="text-align: right;" 
                                            Width="50px"></asp:TextBox>
                                        <img alt="" onclick="LBP_Dsc()" src="media/ico_indent_increase_1.png" />
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        Selling Price
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txt_labour_SellingPrice" runat="server" Style="text-align: right;" 
                                            Width="60px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                            ControlToValidate="txt_labour_SellingPrice" Display="None" 
                                            ErrorMessage="Please enter valid Selling Price" Operator="DataTypeCheck" 
                                            SetFocusOnError="True" Type="Double" ValidationGroup="val_pnl_labour"></asp:CompareValidator>
                                    </td>
                                    <td >
                                        Discount %
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txt_labour_LSPercent" runat="server" Style="text-align: right;" 
                                            Width="30px"></asp:TextBox>
                                        <img alt="" onclick="LSP_Dsc()" src="media/ico_indent_increase_1.png" />
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        Discount Price to Customer
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_labour_LDPrice" runat="server" Style="text-align: right;" 
                                            Width="60px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidatorLDP" runat="server" 
                                            ControlToValidate="txt_labour_LDPrice" Display="None" 
                                            ErrorMessage="Please enter valid Discount Price" Operator="DataTypeCheck" 
                                            SetFocusOnError="True" Type="Double" ValidationGroup="val_pnl_labour"></asp:CompareValidator>
                                    </td>
                                    <td >
                                        Unit Consumption
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_labour_unitconsumption" runat="server" Style="text-align: right;" 
                                            Width="80px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator16" runat="server" 
                                            ControlToValidate="txt_labour_unitconsumption" Display="None" 
                                            ErrorMessage="Please enter valid Unit Consumption" Operator="DataTypeCheck" 
                                            SetFocusOnError="True" Type="Double" ValidationGroup="val_pnl_labour"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Rate Type
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_labour_RateType" runat="server" Width="150px">
                                        </asp:DropDownList>
                                        <ajaxToolkit:CascadingDropDown ID="ccdRatetype" runat="server" TargetControlID="ddl_labour_RateType"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/ServiceCatalogSrv.asmx"
                                ServiceMethod="GetRateType" LoadingText="[Loading Sub Category...]" BehaviorID="ccdRatetype1" ClientIDMode="Static" /> 
                                    </td>
                                   
                                </tr>
                                <tr>
                                 <td >
                                Notes
                            </td>
                            <td>
                                <asp:TextBox ID="txt_labour_Notes" runat="server" Width="250px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                                    <td>
                                        Upload image
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="FileUploadLabour" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" 
                                            runat="server" ControlToValidate="FileUploadLabour" Display="None" 
                                            ErrorMessage="" 
                                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*.*))+\.(jpg|JPG|gif|GIF|png|PNG)$" 
                                            ValidationGroup="val_pnl_labour">File</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                <td></td>
                                    <td colspan="2">
                                        <asp:ImageButton ID="btn_labour_SaveRecord" runat="server" 
                                             SkinID="ImgSave" ValidationGroup="val_pnl_labour" 
                                            onclick="btn_labour_SaveRecord_Click" />
                                        &nbsp;<asp:ImageButton ID="btnCancelLabour" runat="server" AlternateText="Cancel" 
                                            CausesValidation="False" SkinID="ImgCancel" />
                                    </td>
                                </tr>
                            
                            </table>
</asp:Panel>
</asp:Panel>
<asp:Panel ID="pnl_material" runat="server">
<asp:GridView ID="Grid_Material" runat="server" Width="100%" 
        onrowcommand="Grid_Material_RowCommand">
  <Columns>
                                <asp:TemplateField>
                                    <HeaderStyle Width="55px" />
                                    <ItemStyle Width="55px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandArgument="<%# Bind('ID')%>"
                                            CommandName="edit_material" ImageUrl="~/media/ico_edit.png" ToolTip="Edit" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="header_bg_l" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ItemDescription") %>' Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="250px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier">
                                    <ItemTemplate>
                                        <asp:Label ID="lbsup" runat="server" Text='<% #Bind("SupplierName")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Part Number" SortExpression="PartNumber">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("PartNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit" SortExpression="UnitPrice">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("UnitPrice") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="60px" />
                                    <ItemStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stock Level" SortExpression="UnitPrice">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGridStockLevel" runat="server" Text='<%# Bind("UnitsinStock") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="60px" />
                                    <ItemStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Buying Price" SortExpression="BuyingPrice">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("BuyingPrice", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Selling Price" SortExpression="Selling Price">
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("SellingPrice", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount Price" SortExpression="DiscountPrice">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPDP" runat="server" Text='<%# Bind("DiscountPrice", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Units" SortExpression="UnitConsumption">
                                    <ItemTemplate>
                                        <asp:Label ID="lblucp" runat="server" Text='<%# Bind("UnitConsumption", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notes" SortExpression="Notes">
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="200px" />
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
                                            OffsetY="26" PopDelay="0" PopupControlID="pnlOriginalMaterial" PopupPosition="Left"
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
                                <asp:TemplateField HeaderText="More Options" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgSMOptions" runat="server" CausesValidation="false" CommandArgument='<%# Bind("ID") %>'
                                            CommandName="MoreOptions" ImageUrl="~/media/ico_more_options.png"  />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButtonDelete1" runat="server" CommandArgument="<%# Bind('ID')%>"
                                            CommandName="Delete_material" SkinID="ImgSymDel" ToolTip="Delete" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle CssClass="header_bg_r" />
                                </asp:TemplateField>
                            </Columns>
</asp:GridView>

<ajaxToolkit:ModalPopupExtender ID="model_material" runat="server" 
                BackgroundCssClass="modalBackground" CancelControlID="btnCancelMatertial" 
                PopupControlID="pnl_addupdate_material" TargetControlID="btnAddMaterial" Enabled="true">
            </ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnl_addupdate_material" runat="server" Height="430px" Width="850px" style="display:none;background-color:White;">
  
 <div class="tab_header_Bold">
                        Material</div>
                        <asp:ValidationSummary ID="val_sum_material" runat="server"  ValidationGroup="MaterialGroup">
                        </asp:ValidationSummary>
                        <asp:RequiredFieldValidator ID="rf_material_category" runat="server" ControlToValidate="ddlCategory" InitialValue="0" ErrorMessage="Please Select Category" Display="None" ValidationGroup="MaterialGroup" ></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rf_material_subcategory" runat="server" ControlToValidate="ddlSubCategory" InitialValue="0" ErrorMessage="Please Select Sub Category" Display="None" ValidationGroup="MaterialGroup" ></asp:RequiredFieldValidator>
                    <table style="width: 100%">
                        
                            <tr>
                                <td>
                                 <asp:HiddenField ID="h_materialID" runat="server" Value="0" />    Description
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_material_ItemDesc" runat="server" MaxLength="200" TextMode="MultiLine"
                                        Width="650px" Height="70px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_material_ItemDesc"
                                        Display="None" ErrorMessage="Please enter Description" SetFocusOnError="True"
                                        ValidationGroup="MaterialGroup"></asp:RequiredFieldValidator>
                                </td>
                               
                            </tr>
                            <tr>
                            <td>Buying Price</td>
                            <td><asp:TextBox ID="txt_material_MBuyingPrice" runat="server" CausesValidation="True" Width="70px"
                                        Style="text-align: right;"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_material_MBuyingPrice"
                                        Display="None" ErrorMessage="Please enter valid Material Buying Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="MaterialGroup"></asp:CompareValidator></td>
                            <td>Mark up %</td>
                            <td><asp:TextBox ID="txt_material_PPercent" runat="server" Width="50px" Style="text-align: right;"></asp:TextBox>
                                    <img src="./media/ico_indent_increase_1.png" alt="" onclick="PBP_Dsc()" /></td>
                            </tr>
                            <tr>
                             <td>
                                    Selling Price
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_material_MSellingPrice" runat="server" Width="70px" Style="text-align: right;"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txt_material_MSellingPrice"
                                        Display="None" ErrorMessage="Please enter valid Selling Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="MaterialGroup"></asp:CompareValidator>
                                </td>
                                 <td>
                                    Discount %
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_material_PSPercent" runat="server" Width="30px" Style="text-align: right;"></asp:TextBox>
                                    <img src="./media/ico_indent_increase_1.png" alt="" onclick="PSP_Dsc()" />
                                </td>
                                
                               
                            </tr>
                            <tr>
                               <td>
                                    Supplier
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_material_Supplier" runat="server" Width="170px">
                                    </asp:DropDownList>
                                    <ajaxToolkit:CascadingDropDown ID="ccdSupplier" runat="server" TargetControlID="ddl_material_Supplier"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/ServiceCatalogSrv.asmx"
                                ServiceMethod="GetVendors" LoadingText="[Loading Category...]" BehaviorID="ccdSupplier1" ClientIDMode="Static" /> 
                                </td>
                               <td>
                                    Part Number
                                </td>
                                <td >
                                    <asp:TextBox ID="txt_material_PartNumber" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                
                                <td>
                                    Discount Price to Customer
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_material_PDPrice" runat="server" Width="70px" Style="text-align: right;"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="txt_material_PDPrice"
                                        Display="None" ErrorMessage="Please enter valid Discount Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="MaterialGroup"></asp:CompareValidator>
                                </td>
                                 <td >
                                    Unit Consumption
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_material_ucproduct" runat="server" Style="text-align: right;" Width="80px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator17" runat="server" ControlToValidate="txt_material_ucproduct"
                                        Display="None" ErrorMessage="Please enter valid Unit Consumption" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="MaterialGroup"></asp:CompareValidator>
                                </td>
                            </tr>
                         
                            <tr>
                                <td>
                                    Stock level
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_material_StockLevel" runat="server" Width="70px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="txt_material_StockLevel"
                                        Display="None" ErrorMessage="Please enter valid Stock Level" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="MaterialGroup"></asp:CompareValidator>
                                </td>
                                <td>
                                    Unit
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_material_Unit" runat="server" Width="70px"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td>
                                    Notes
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_material_MNotes" runat="server" Width="250px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    Upload image
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUploadMaterial" runat="server"></asp:FileUpload>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                        ControlToValidate="FileUploadMaterial" Display="None" ErrorMessage="Please select JPG,GIF or PNG."
                                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*.*))+\.(jpg|JPG|gif|GIF|png|PNG)$"
                                        ValidationGroup="MaterialGroup">File</asp:RegularExpressionValidator>
                                </td>
                              
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="3">
                                    <asp:ImageButton ID="btnSaveMaterial" runat="server"
                                        SkinID="ImgSave" ValidationGroup="MaterialGroup" 
                                        onclick="btnSaveMaterial_Click"></asp:ImageButton>
                                    <asp:ImageButton ID="btnCancelMatertial" runat="server" AlternateText="Cancel" CausesValidation="False"
                                         SkinID="ImgCancel"></asp:ImageButton>
                                </td>
                                
                            </tr>
                        
                    </table>
</asp:Panel>
</asp:Panel>
<asp:Panel ID="pnl_service" runat="server" >
<asp:GridView ID="Grid_Service" runat="server" Width="100%" 
        onrowcommand="Grid_Service_RowCommand">
<Columns>
                                <asp:TemplateField>
                                    <HeaderStyle Width="45px" />
                                    <ItemStyle Width="45px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="edit_service"
                                            CommandArgument="<%# Bind('ID')%>" ImageUrl="~/media/ico_edit.png" ToolTip="Edit">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="header_bg_l" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ServiceDescription") %>' Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="250px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Setup Buy">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_SetupBuy" runat="server" Text='<%# Bind("SetupBuy", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Setup Sell">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_SetupSell" runat="server" Text='<%# Bind("SetupSell", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Materials Buy">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_MaterialsBuy" runat="server" Text='<%# Bind("MaterialsBuy", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Materials Sell">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_MaterialsSell" runat="server" Text='<%# Bind("MaterialsSell", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Labour Buy">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_LabourBuy" runat="server" Text='<%# Bind("LabourBuy", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Labour Sell">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_LabourSell" runat="server" Text='<%# Bind("LabourSell", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount Price" SortExpression="DiscountPrice">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSDP" runat="server" Text='<%# Bind("DiscountPrice", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Units" SortExpression="UnitConsumption">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtucs" runat="server" Text='<%# Bind("UnitConsumption") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblucs" runat="server" Text='<%# Bind("UnitConsumption", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Service Buy">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_TotalBuy" runat="server" Text='<%# Bind("TotalServiceBuy", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Service Sell">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_TotalSell" runat="server" Text='<%# Bind("TotalServiceSell", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GP%">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_GP" runat="server" Text='<%# Bind("GP", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <ajaxToolkit:AnimationExtender ID="MyExtender" runat="server" TargetControlID="pnlOriginalService">
                                            <Animations>
          
            <OnMouseOver>
            <FadeIn Duration=".5" Fps="20" />
            </OnMouseOver>
                                            </Animations>
                                        </ajaxToolkit:AnimationExtender>
                                        <ajaxToolkit:HoverMenuExtender ID="hmeServices" runat="server" TargetControlID="imgContractor"
                                            PopupControlID="pnlOriginalService" PopDelay="0" PopupPosition="Left" EnableViewState="false"
                                            OffsetY="26" />
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                            Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                       
                                        <div id="pnlOriginalService" runat="server" class="PrepRecipeDetails" style="display: none;">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.OriginalData) %>'
                                                Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="More Options" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgSMOptions" runat="server" CausesValidation="false" CommandName="MoreOptions"
                                            ImageUrl="~/media/ico_more_options.png" CommandArgument='<%# Bind("ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButtonDelete1" runat="server" CommandName="Delete_service" CommandArgument="<%# Bind('ID')%>"
                                            SkinID="ImgSymDel" ToolTip="Delete"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                    <HeaderStyle CssClass="header_bg_r" />
                                </asp:TemplateField>
                            </Columns>
</asp:GridView>

<ajaxToolkit:ModalPopupExtender ID="model_service" runat="server" 
                BackgroundCssClass="modalBackground" CancelControlID="btnCancelService" 
                PopupControlID="pnl_addupdate_service" TargetControlID="btnAddService" Enabled="true"></ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnl_addupdate_service" runat="server" Height="440px" Width="850px" style="display:none;background-color:White;">

 <div class="tab_header_Bold">
                        Service</div>
<asp:ValidationSummary ID="val_sum_service" runat="server" ValidationGroup="ServiceGroup">
                        </asp:ValidationSummary>
                        <asp:RequiredFieldValidator ID="rf_service_category" runat="server" ControlToValidate="ddlCategory" InitialValue="0" ErrorMessage="Please Select Category" Display="None" ValidationGroup="ServiceGroup" ></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rf_service_subcategory" runat="server" ControlToValidate="ddlSubCategory" InitialValue="0" ErrorMessage="Please Select Sub Category" Display="None" ValidationGroup="ServiceGroup" ></asp:RequiredFieldValidator>
<table style="width: 100%">
                       
                            <tr>
                                <td style="width: 18%">
                                <asp:HiddenField ID="h_serviceid" runat="server" Value="0" />   Description
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txt_service_SDescription" runat="server" MaxLength="200" TextMode="MultiLine"
                                        Width="650px" Height="70px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_service_SDescription"
                                        Display="None" ErrorMessage="Please enter Description" SetFocusOnError="True"
                                        ValidationGroup="ServiceGroup"></asp:RequiredFieldValidator>
                                </td>
                              
                            </tr>
                            <tr>
                                <td>
                                    Setup Buy
                                </td>
                                <td style="width: 15%">
                                    <asp:TextBox ID="txt_service_SetupBuy" runat="server" MaxLength="200" Width="75px" Style="text-align: right;"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txt_service_SetupBuy"
                                        Display="None" ErrorMessage="Please enter valid Setup buying Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
                                </td>
                                <td style="width: 15%">
                                    Materials Buy
                                </td>
                                <td style="width: 15%">
                                    <asp:TextBox ID="txt_service_MaterialsBuy" runat="server" MaxLength="200" Width="75px" Style="text-align: right;"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txt_service_MaterialsBuy"
                                        Display="None" ErrorMessage="Please enter valid Materials buying Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
                                </td>
                                <td>
                                    Labour Buy
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txt_service_LabourBuy" runat="server" MaxLength="200" Width="75px" Style="text-align: right;"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txt_service_LabourBuy"
                                        Display="None" ErrorMessage="Please enter valid Labour buying Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Setup Sell
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_service_SetupSell" runat="server" MaxLength="200" Width="75px" Style="text-align: right;"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txt_service_SetupSell"
                                        Display="None" ErrorMessage="Please enter valid Setup selling Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
                                </td>
                                <td>
                                    Materials Sell
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_service_MaterialsSell" runat="server" MaxLength="200" Width="75px" Style="text-align: right;"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="txt_service_MaterialsSell"
                                        Display="None" ErrorMessage="Please enter valid Materials selling Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
                                </td>
                                <td>
                                    Labour Sell
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_service_LabourSell" runat="server" MaxLength="200" Width="75px" Style="text-align: right;"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txt_service_LabourSell"
                                        Display="None" ErrorMessage="Please enter valid Labour selling Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Total Buy
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_service_TotBuy" runat="server" MaxLength="200" Width="75px" Style="text-align: right;"></asp:TextBox>
                                </td>
                                <td>
                                    Mark up%
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_service_SPercent" runat="server" Width="50px" Style="text-align: right;"></asp:TextBox>
                                    <img src="./media/ico_indent_increase_1.png" alt="" onclick="SerB_Dsc()" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Total Sell
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_service_TotSell" runat="server" MaxLength="200" Width="75px" Style="text-align: right;"></asp:TextBox>
                                </td>
                                <td>
                                    Discount %
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_service_SDPercent" runat="server" Width="30px" Style="text-align: right;"></asp:TextBox>
                                    <img src="./media/ico_indent_increase_1.png" alt="" onclick="SerS_Dsc()" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 80px">
                                    Unit Consumption
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_service_ucservice" runat="server" Style="text-align: right;" Width="80px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator18" runat="server" ControlToValidate="txt_service_ucservice"
                                        Display="None" ErrorMessage="Please enter valid Unit Consumption" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
                                </td>
                                <td>
                                    Discount Price to Customer
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_service_SDprice" runat="server" Width="40px" Style="text-align: right;"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidatorSDPrice" runat="server" ControlToValidate="txt_service_SDprice"
                                        Display="None" ErrorMessage="Please enter valid Discount Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Upload image
                                </td>
                                <td colspan="3">
                                    <asp:FileUpload ID="FileUploadService" runat="server"></asp:FileUpload>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                        ControlToValidate="FileUploadService" Display="None" ErrorMessage="Please select JPG,GIF or PNG."
                                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*.*))+\.(jpg|JPG|gif|GIF|png|PNG)$"
                                        ValidationGroup="ServiceGroup">File</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="2">
                                    <asp:ImageButton ID="btnSaveServices"  runat="server"
                                        SkinID="ImgSave" ValidationGroup="ServiceGroup" 
                                        onclick="btnSaveServices_Click"></asp:ImageButton> &nbsp;
                                    <asp:ImageButton ID="btnCancelService" runat="server"
                                        SkinID="ImgCancel" CausesValidation="False" AlternateText="Cancel"></asp:ImageButton>
                                </td>
                            </tr>
                        
                    </table>
</asp:Panel>

</asp:Panel>

</td> </tr>
</table>