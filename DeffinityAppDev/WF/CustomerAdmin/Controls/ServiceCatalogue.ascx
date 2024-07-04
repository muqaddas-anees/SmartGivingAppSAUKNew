<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="controls_ServiceCatalogue_ctrl_1" Codebehind="ServiceCatalogue.ascx.cs" %>

  <div class="card shadow-sm">
						<div class="card-header">
							<h3 class="panel-title form-inline"> 

                                 <%= Resources.DeffinityRes.ServiceCatalogue%>
                              </h3>
							<div class="card-toolbar">
								 <asp:Button ID="btnAddnewProduct" OnClick="btnAddnewProduct_Click"
                                    runat="server" ValidationGroup="newrec"  AlternateText="New"
                                    SkinID="btnDefault" Text="New" Visible="false"></asp:Button>
                               
							</div>
						</div>
						<div class="panel-body">
                            <div class="row">

                                

<script type="text/javascript">

    function CheckSelection() {
        var frm = document.forms[0]
        var checked = 0;
        var second = 0;
        for (i = 0; i < document.forms[0].elements.length; i++) {
            if (document.forms[0].elements[i].id.indexOf("chkitem") > -1) {
                if (document.forms[0].elements[i].checked == true) {
                    var checked = checked + 1;
                    second = i;
                    if (checked == 2) {
                        alert("Please select only one checkbox");
                        document.forms[0].elements[i].checked = false;
                        return false;

                    }
                }

            }
        }
        if (checked == 0) {
            alert("Please select one checkbox");
            return false;
        }
        else
            if (checked == 2) {
                alert("Please select only one checkbox");
                return false;
                document.forms[0].elements[second].checked == false;
            }
    }
</script>
<script type="text/javascript">
    function LBP_Dsc() {
        try {
            var pecent = document.getElementById('<%=txtLPercent.ClientID %>').value;
            if (pecent == '') {
                alert('Please enter Markup');
                return false;
            }
            var bPrice = document.getElementById('<%=txtBuyingPrice.ClientID %>').value;
            if (bPrice == '') {
                alert('Please enter Buying Price');
                return false;
            }

            document.getElementById('<%=txtSellingPrice.ClientID %>').value = floatmyPriceInc(bPrice, pecent);
        }
        catch (err) {
            //Handle errors here
        }
    }

    function LSP_Dsc() {
        try {
            var pecent = document.getElementById('<%=txtLSPercent.ClientID %>').value;
            if (pecent == '') {
                alert('Please enter Discount');
                return false;
            }
            var sPrice = document.getElementById('<%=txtSellingPrice.ClientID %>').value;
            if (sPrice != '') {
                var temp = floatmyPriceInc(sPrice, pecent);
                //alert(temp);
                var tempDis = parseFloat(parseFloat(temp) - parseFloat(sPrice)).toFixed(2);
                //alert(tempDis);
                document.getElementById('<%=txtLDPrice.ClientID %>').value = parseFloat(parseFloat(sPrice) - parseFloat(tempDis)).toFixed(2);

            }
        }
        catch (err) {
            //Handle errors here
        }
    }

    function PBP_Dsc() {
        try {
            var PPecent = document.getElementById('<%=txtPPercent.ClientID %>').value;
            if (PPecent == '') {
                alert('Please enter Markup');
                return false;
            }
            var pBprice = document.getElementById('<%=txtMBuyingPrice.ClientID %>').value;
            if (pBprice == '') {
                alert('Please enter Buying Price');
                return false;
            }
            document.getElementById('<%=txtMSellingPrice.ClientID %>').value = floatmyPriceInc(pBprice, PPecent);
        }
        catch (err) {
            //Handle errors here
        }
        return false;
    }

    function PSP_Dsc() {
        try {
            var PPecent = document.getElementById('<%=txtPSPercent.ClientID %>').value;
            if (PPecent == '') {
                alert('Please enter Discount');
                return false;
            }
            var pSprice = document.getElementById('<%=txtMSellingPrice.ClientID %>').value;
            if (pSprice != '') {
                var temp = floatmyPriceInc(pSprice, PPecent);
                var tempDis = parseFloat(parseFloat(temp) - parseFloat(pSprice)).toFixed(2);
                document.getElementById('<%=txtPDPrice.ClientID %>').value = parseFloat(parseFloat(pSprice) - parseFloat(tempDis)).toFixed(2);

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
            var sPecent = document.getElementById('<%=txtSPercent.ClientID %>').value;
            if (sPecent == '') {
                alert('Please enter Markup');
                return false;
            }
            var sbuy = document.getElementById('<%=txtSetupBuy.ClientID %>').value;
            var mbuy = document.getElementById('<%=txtMaterialsBuy.ClientID %>').value;
            var lbuy = document.getElementById('<%=txtLabourBuy.ClientID %>').value;
            if (sbuy != '') {
                document.getElementById('<%=txtSetupSell.ClientID %>').value = floatSell(sbuy, sPecent);

            }
            if (mbuy != '') {
                document.getElementById('<%=txtMaterialsSell.ClientID %>').value = floatSell(mbuy, sPecent);

            }
            if (lbuy != '') {
                document.getElementById('<%=txtLabourSell.ClientID %>').value = floatSell(lbuy, sPecent);

            }
            var ssell = document.getElementById('<%=txtSetupSell.ClientID %>').value;
            var msell = document.getElementById('<%=txtMaterialsSell.ClientID %>').value;
            var lsell = document.getElementById('<%=txtLabourSell.ClientID %>').value;

            document.getElementById('<%=txtTotBuy.ClientID %>').value = parseFloat(parseFloat(sbuy) + parseFloat(mbuy) + parseFloat(lbuy)).toFixed(2);
            document.getElementById('<%=txtTotSell.ClientID %>').value = parseFloat(parseFloat(ssell) + parseFloat(msell) + parseFloat(lsell)).toFixed(2);
        }
        catch (err) {
            //Handle errors here
        }
    }

    function SerS_Dsc() {
        try {
            var sPecent = document.getElementById('<%=txtSDPercent.ClientID %>').value;
            if (sPecent == '') {
                alert('Please enter Discount');
                return false;
            }
            var totSell = document.getElementById('<%=txtTotSell.ClientID %>').value;
            if (totSell != '') {
                var temp = parseFloat(floatSell(totSell, sPecent));
                var tempDisc = parseFloat(parseFloat(temp) - parseFloat(totSell)).toFixed(2);
                document.getElementById('<%=txtSDprice.ClientID %>').value = parseFloat(parseFloat(totSell) - parseFloat(tempDisc)).toFixed(2);
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


<div class="form-group row" style="display:none;visibility:hidden;" >
          <div class="col-md-12">
	
            <asp:Panel ID="Panel_fileupdload" runat="server" Width="100%"  >
                <div class="form-group pull-right" >
          <div class="col-md-12 form-inline">
               <asp:LinkButton ID="lnkbtncatalog"  runat="server" onclick="lnkbtncatalog_Click" ForeColor="Black" Font-Bold="true" Visible="false" > Search Central Catalogue  </asp:LinkButton>
             <ajaxToolkit:ModalPopupExtender CancelControlID="ImgCancel" ID="mpopBOM" runat="server"
                 PopupControlID="pnlBOM" TargetControlID="imgItemEdit"  
                 BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
              <asp:HyperLink ID="link_serviceAdmin" runat="server" NavigateUrl="ServiceCatalogAdmin.aspx" Text="Central Catalogue" Font-Bold="true" ForeColor="Black" Visible="false"></asp:HyperLink>
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
              <asp:LinkButton ID="btnDownloadExcel" runat="server" Text="Download Excel File" OnClick="btnDownloadExcel_Click"
                                    Font-Bold="true" ForeColor="Black"></asp:LinkButton>
	</div>
</div>
               <asp:Panel runat="server" ID="pnlCopy" Width="350" Height="200px" Style="display: none" BackColor="White">
<asp:Panel ID="pnlCopyHeader" runat="server" Width="350"  BackColor="White">
<div>
<div style="float:left;width:320px;font-weight:bold" >
<div class="tab_subheader" style="border-bottom:solid 1px Silver;width:98%;">Copy Service catalogue items to</div></div>
 <div style="float:right;width:30px" ><asp:LinkButton runat="server" ID="btnCopyCancel" SkinID="BtnLinkClose" /></div></div>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="350px" Height="200px" ScrollBars="Auto"  BackColor="White">
<asp:UpdateProgress AssociatedUpdatePanelID="upCopy" ID="upProgress" runat="server">
<ProgressTemplate>
<asp:Label ID="imgloding" runat="server" SkinID="Loading" />
</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="upCopy" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div>
<asp:Label ID="lblCopymessage" runat="server"></asp:Label>
</div>
<div style="width:300px">
 <label id="lblname" title="Select Customer">Select Customer</label> <asp:DropDownList ID="ddlPortfolio_copyto" runat="server" Width="250px"></asp:DropDownList>
 </div>
<ajaxToolkit:CascadingDropDown ID="casCadPortfolio" runat="server"
    TargetControlID="ddlPortfolio_copyto"
    Category="Title"
    PromptText="Please select..."
    PromptValue="0"
    ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
    ServiceMethod="GetPortfolio_Active"
     LoadingText="Loading..."
    />
   
    <asp:Button ID="btnCopyToCatalogue" runat="server" SkinID="btnSubmit" OnClick="btnCopyToCatalogue_Click" CausesValidation="false" />
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="btnCopyToCatalogue" EventName="Click" />
</Triggers>
</asp:UpdatePanel>

</asp:Panel>
</asp:Panel>
              
                <asp:Panel ID="PanelCsv" runat="server" Width="100%">
                    <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.Excelfileupload%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
                    <div class="form-group row">
        <div class="col-md-12">
             <iframe id="iframeMpp" height="100px" width="100%" runat="server" scrolling="no"
                            frameborder="0"></iframe>
        </div>
                        </div>
                </asp:Panel>
            </asp:Panel>
              </div>
</div>
            <%-- <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>--%>
           <div class="form-group row">
          <div class="col-md-12">
	
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group10">
                </asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Group11">
                </asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Group12">
                </asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary7" runat="server" ValidationGroup="Edit_subcatelog">
                </asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary8" runat="server" ValidationGroup="Edit_catelog">
                </asp:ValidationSummary>
                <asp:Label ID="lblError1" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
           
                <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
                <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary6" runat="server" ></asp:ValidationSummary>
                <asp:Label ID="lblErrMaterial" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary5" runat="server" ></asp:ValidationSummary>
                <asp:Label ID="lblErrService" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
          </div>
</div>
            
            <asp:Panel ID="tblAddData" runat="server" Width="100%">
                <div class="form-group pull-right"  style="display:none;visibility:hidden;">
          <div class="col-md-12">
              <asp:Button ID="btnCopyCatalogue" runat="server" SkinID="btnDefault" Text="Copy Catalogue" 
                                    EnableViewState="false" Visible="false" />
	</div>
</div>
                <div class="form-group row"  style="display:none;visibility:hidden;">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Deffinity.systemdefaults.GetCategoryName() %></label>
           <div class="col-sm-9 form-inline">
                <asp:Panel ID="pnlcategory" runat="server">
                                   
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="Group1"
                                        InitialValue="0" ControlToValidate="ddlCategory" ErrorMessage="Please Select brand"
                                        Display="None"></asp:RequiredFieldValidator>--%>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCategory"
                                        Display="None" ErrorMessage="Please Select brand" InitialValue="0" ValidationGroup="Edit_catelog"></asp:RequiredFieldValidator>--%>
                                    <asp:LinkButton ID="btnaddcategory" OnClick="btnaddcategory_Click" runat="server"
                                        SkinID="BtnLinkAdd" CausesValidation="False" Visible="false" ></asp:LinkButton>
                                    <asp:LinkButton ID="btn_CategoryEdit" runat="server" ValidationGroup="Edit_catelog"
                                        SkinID="BtnLinkEdit" OnClick="btn_CategoryEdit_Click" Visible="false" >
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnDeleteCategory" runat="server" OnClientClick="javascript:alert('Do you want to delete category,associated sub category and item(s)?');"
                                        SkinID="BtnLinkDelete" OnClick="btnDeleteCategory_Click" Visible="false"  />
                                </asp:Panel>
                                <asp:Panel ID="pnladdcategory" runat="server" Visible="false">
                                    <asp:TextBox ID="txtAddCategory" runat="server" SkinID="txt_70" ValidationGroup="cat1"></asp:TextBox>
                                    <asp:LinkButton ID="btnSaveCategory" runat="server" ToolTip="Add Category" ValidationGroup="cat1"
                                        OnClick="btnSaveCategory_Click" SkinID="BtnLinkUpdate" ></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelCategory" runat="server" ToolTip="Cancel" OnClick="btnCancelCategory_Click"
                                        CausesValidation="False" SkinID="BtnLinkCancel" ></asp:LinkButton>
                                    <div>
                                        <asp:RequiredFieldValidator runat="server" ID="ReqCatname" ControlToValidate="txtAddCategory"
                                            SetFocusOnError="true" ErrorMessage="Please enter brand" ForeColor="Red" ValidationGroup="cat1"></asp:RequiredFieldValidator></div>
                                </asp:Panel>
                                <asp:HiddenField ID="HID_Category" runat="server"></asp:HiddenField>
            </div>
	</div>
	<div class="col-md-6" style="margin-top:-15px">
           <label class="col-sm-3 control-label"><%= Deffinity.systemdefaults.GetSubCategoryName() %></label>
           <div class="col-sm-9 form-inline">
                <asp:Panel ID="pnlsubcategory" runat="server">
                                   
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="Group1"
                                        InitialValue="0" ControlToValidate="ddlSubCategory" ErrorMessage="Please Select Type of Equipment"
                                        Display="None"></asp:RequiredFieldValidator>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSubCategory"
                                        Display="None" ErrorMessage="Please Select Sub Category" InitialValue="0" ValidationGroup="Edit_subcatelog"></asp:RequiredFieldValidator>
                                    <asp:LinkButton ID="btnaddsubcategory" OnClick="btnaddsubcategory_Click" runat="server"
                                        SkinID="BtnLinkAdd" ValidationGroup="cat0" Visible="false" ></asp:LinkButton>
                                    <asp:LinkButton ID="btn_editSubCategory" runat="server" ValidationGroup="Edit_subcatelog"
                                        SkinID="BtnLinkEdit" OnClick="btn_editSubCategory_Click" Visible="false" >
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnSubCategory" runat="server" OnClientClick="javascript:alert('Do you want to delete Sub category and associated item(s)?');"
                                        OnClick="btnSubCategory_Click" SkinID="BtnLinkDelete" Visible="false"  />
                                    <div>
                                        <asp:RequiredFieldValidator ID="ReqSelectCat" runat="server" ForeColor="Red" ValidationGroup="cat0"
                                            InitialValue="0" ControlToValidate="ddlCategory" ErrorMessage="Please select type of equipment"
                                            Display="None"></asp:RequiredFieldValidator></div>
                                </asp:Panel>
                                <asp:Panel ID="pnladdsubcategory" runat="server" Visible="false">
                                    <asp:TextBox ID="txtAddSubCategory" runat="server" SkinID="txt_70" ValidationGroup="Subcat1"></asp:TextBox>
                                    <asp:LinkButton ID="btnSaveSubCategory" runat="server" ToolTip="Add SubCategory"
                                        ValidationGroup="Subcat1" OnClick="btnSaveSubCategory_Click" SkinID="BtnLinkUpdate" >
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelSubCategory" runat="server" ToolTip="Cancel" OnClick="btnCancelSubCategory_Click"
                                        SkinID="BtnLinkCancel" ></asp:LinkButton>
                                    <div>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtAddSubCategory"
                                            ErrorMessage="Please enter type of equipment" ForeColor="Red" ValidationGroup="Subcat1"></asp:RequiredFieldValidator></div>
                                </asp:Panel>
                                <asp:HiddenField ID="HID_SubCategory" runat="server"></asp:HiddenField>
            </div>
	</div>
                    
</div>
                <div class="form-group row" style="display:none;visibility:hidden;">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Type%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlSelect" runat="server" Width="70%" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged">
                                    <asp:ListItem Value="1" >Labour</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True" >Material</asp:ListItem>
                                    <asp:ListItem Value="3" >Service</asp:ListItem>
                                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
               <asp:Button ID="btnView" OnClick="btnView_Click"
                                    runat="server" CausesValidation="False" Visible="False" 
                                    SkinID="btnDefault" Text="View"></asp:Button>
                                <asp:Button ID="btnAddnew" OnClick="btnAddnew_Click"
                                    runat="server" ValidationGroup="newrec"  AlternateText="New"
                                    SkinID="btnDefault" Text="New" Visible="false"></asp:Button>
                              
                                <asp:Button ID="btnAddnewService" OnClick="btnAddnewService_Click"
                                    runat="server" ValidationGroup="newrec"  AlternateText="New"
                                    SkinID="btnDefault" Text="New" Visible="false"></asp:Button>
            </div>
	</div>
</div>

                <div  class="form-group row">
                    <div  class="col-md-12">
                         
                    </div>

                </div>
                <div class="form-group row"  style="display:none;visibility:hidden;">
          <div class="col-md-12">
               <ajaxToolkit:ModalPopupExtender ID="mdlCustomers" runat="server" CancelControlID="lnkCancel"
                                    BackgroundCssClass="modalBackground" TargetControlID="btnCopyCatalogue" PopupControlID="pnlCustomers" />
                                <asp:Panel ID="pnlCustomers" runat="server" ScrollBars="Both" BackColor="White" Style="display: none"
                                    Width="300px" Height="500px" BorderStyle="Double" BorderColor="LightSteelBlue">
                                                <asp:GridView ID="grd_Customers" runat="server" DataKeyNames="ID" EmptyDataText="No Customers found"
                                                    DataSourceID="objCustomer" OnRowDataBound="grd_Customers_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkitem" runat="server" Checked="false" onclick="CheckSelection();" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat='server' Text='<%# Eval("ID") %>'></asp:Label></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Customer" DataField="PortFolio" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:ObjectDataSource ID="objCustomer" runat="server" TypeName="Deffinity.PortfolioManager.Portfilio"
                                                    SelectMethod="Portfolio_display"></asp:ObjectDataSource>
                                            <div class="form-group row">
                                  <div class="col-md-12 form-inline">
                                      <asp:Button ID="lnkOk" runat="server" OnClick="lnkOk_Click" Text="Copy" SkinID="btnDefault" />
                                       <asp:Button ID="lnkCancel" runat="server" SkinID="btnClose" />
                                      </div>
                                                </div>
                                  
                                </asp:Panel>
	</div>
</div>
               
                </asp:Panel>
            
            
                <asp:Panel ID="Panel1" runat="server" Width="100%">
                   
                        <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" Width="100%" OnRowEditing="GridView1_RowEditing"
                            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                            OnRowUpdating="GridView1_RowUpdating" OnPageIndexChanging="GridView1_PageIndexChanging"
                            AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle Wrap="false" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonEdit1" runat="server" CausesValidation="false" CommandName="Edit"
                                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButtonUpdate1" runat="server" CommandName="Update" CommandArgument='<%# Bind("ID")%>'
                                            SkinID="BtnLinkUpdate" ToolTip="Update" ValidationGroup="Group10">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonCancel1" runat="server" CausesValidation="false" CommandName="Cancel"
                                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                    </EditItemTemplate>
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" SortExpression="Description"  ItemStyle-CssClass="col-nowrap">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtLDesc1" runat="server" Text='<%# Bind("Description") %>' Width="250px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtLDesc1"
                                            Display="None" ErrorMessage="Please enter description" ValidationGroup="Group10"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate Type">
                                    <ItemTemplate>
                                        <asp:Label ID="rtype" runat="server" Text='<%# Bind("RateType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DropDownList1" DataTextField="RateType" DataValueField="ID"
                                            DataSource='<%# RateTypes() %>' runat="server" SelectedValue='<%# Bind("RateType1") %>'>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ControlStyle Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Buying Price" SortExpression="BuyingPrice">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtLbuyingPrice1" runat="server" Text='<%# Bind("BuyingPrice") %>'
                                            ValidationGroup="Group10" Width="60px"></asp:TextBox>
                                        <asp:CompareValidator ID="cmpRate21" runat="server" ControlToValidate="txtLbuyingPrice1"
                                            Display="None" ErrorMessage="Please enter valid Buying Price" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Text="Invalid Price" Type="Double" ValidationGroup="Group10"></asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLbuyingPrice1"
                                            Display="None" ErrorMessage="Please enter buying price in grid" ValidationGroup="Group10"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("BuyingPrice", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Selling Price" SortExpression="SellingPrice">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtLSellingPrice1" runat="server" Text='<%# Bind("SellingPrice") %>'
                                            ValidationGroup="Group10" Width="60px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLSellingPrice1"
                                            Display="None" ErrorMessage="Please enter selling price in grid" ValidationGroup="Group10"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cmpRate22" runat="server" ControlToValidate="txtLSellingPrice1"
                                            Display="None" ErrorMessage="Please enter valid selling price" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Text="Invalid Price" Type="Double" ValidationGroup="Group10"></asp:CompareValidator>
                                    </EditItemTemplate>
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
                                    <ItemStyle  />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notes" SortExpression="Notes">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtLNotes1" runat="server" Text='<%# Bind("Notes") %>' TextMode="MultiLine"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle  />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                       <%-- <ajaxToolkit:AnimationExtender ID="MyExtender" runat="server" TargetControlID="pnlOriginalImage">
                                            <Animations>
            <OnMouseOver>
            <FadeIn Duration=".5" Fps="20" />
            </OnMouseOver>
                                            </Animations>
                                        </ajaxToolkit:AnimationExtender>--%>
                                      <%--  <ajaxToolkit:HoverMenuExtender ID="hmeDetails" runat="server" TargetControlID="imgContractor"
                                            PopupControlID="pnlOriginalImage" PopDelay="0" PopupPosition="Left" EnableViewState="false"
                                            OffsetY="26" />--%>
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                            Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                      <%--  <div id="pnlOriginalImage" runat="server" class="PrepRecipeDetails" style="display: none;">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.OriginalData) %>'
                                                Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        </div>--%>
                                    </ItemTemplate>
                                    <ItemStyle  />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="More Options">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgLMOptions" runat="server" CausesValidation="false" CommandName="MoreOptions"
                                            SkinID="BtnLinkMore" CommandArgument='<%# Bind("ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"  />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonDelete1" runat="server" CommandName="Delete" CommandArgument='<%# Bind("ID")%>'
                                            SkinID="BtnLinkDelete" ToolTip="Delete" ></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"  />
                                    <HeaderStyle CssClass="header_bg_r" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                   
                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="ID" OnPageIndexChanging="GridView2_PageIndexChanging" Visible="true"
                            OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowCommand="GridView2_RowCommand"
                            OnRowDeleting="GridView2_RowDeleting" OnRowEditing="GridView2_RowEditing" OnRowUpdating="GridView2_RowUpdating"
                            Width="100%">
                            <Columns>
                                                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgSMOptions" runat="server" CausesValidation="false" CommandArgument='<%# Bind("ID") %>'
                                            CommandName="MoreOptions" SkinID="BtnLinkEdit" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                      <%--  <ajaxToolkit:AnimationExtender ID="MyExtender" runat="server" TargetControlID="pnlOriginalMaterial">
                                            <Animations>
          
            <OnMouseOver>
            <FadeIn Duration=".5" Fps="20" />
            </OnMouseOver>
                                            </Animations>
                                        </ajaxToolkit:AnimationExtender>--%>
                                       <%-- <ajaxToolkit:HoverMenuExtender ID="hmeMaterials" runat="server" EnableViewState="false"
                                            OffsetY="26" PopDelay="0" PopupControlID="pnlOriginalMaterial" PopupPosition="Left"
                                            TargetControlID="imgContractor" />--%>
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                             />
                                       <%-- <div id="pnlOriginalMaterial" runat="server" class="PrepRecipeDetails" style="display: none;">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.OriginalData) %>'
                                                Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        </div>--%>
                                    </ItemTemplate>
                                    <ItemStyle  />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <HeaderStyle Width="55px" />
                                    <ItemStyle Width="55px" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandArgument='<%# Bind("ID")%>'
                                            CommandName="Edit" SkinID="BtnLinkEdit" ToolTip="Edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandArgument='<%# Bind("ID")%>'
                                            CommandName="Update" SkinID="BtnLinkUpdate" ToolTip="Update" ValidationGroup="Group11" />
                                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                            SkinID="BtnLinkCancel" ToolTip="Cancel" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" SortExpression="Description" ItemStyle-CssClass="col-nowrap">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDesc1" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDesc1"
                                            Display="None" ErrorMessage="Please enter labour description in grid" ValidationGroup="Group11"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%"  />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbsup" runat="server" Text='<% #Bind("SupplierName")  %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlSupp" runat="server" DataSource="<%# Supplier() %>" DataTextField="SupplierName"
                                            DataValueField="ID" SelectedValue='<%# Bind("Supplier1") %>'>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Part Number" SortExpression="PartNumber" ItemStyle-CssClass="col-nowrap">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPart1" runat="server" Text='<%# Bind("PartNumber") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("PartNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Price" SortExpression="UnitPrice" ItemStyle-HorizontalAlign="Right" Visible="false" >
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUnitPrice1" runat="server" Text='<%# Bind("UnitPrice") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("UnitPrice", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    
                                    <ItemStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stock Level" SortExpression="UnitPrice" ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtGridStockLevel" runat="server" Text='<%# Bind("UnitsinStock") %>'></asp:TextBox>
                                        <asp:CompareValidator ID="CV_GridStockLevel" runat="server" ControlToValidate="txtGridStockLevel"
                                            Display="None" ErrorMessage="Please enter valid Stock Level" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Double" ValidationGroup="Group11" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGridStockLevel" runat="server" Text='<%# Bind("UnitsinStock") %>'></asp:Label>
                                    </ItemTemplate>
                                   
                                    <ItemStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Buying Price" SortExpression="BuyingPrice">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtBuyingPrice1" runat="server" Text='<%# Bind("BuyingPrice") %>'
                                            SkinID="Price_100px" MaxLength="20" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredField30" runat="server" ControlToValidate="txtBuyingPrice1"
                                            Display="None" ErrorMessage="Please enter buying price in grid" SetFocusOnError="True"
                                            ValidationGroup="Group11"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cmpRate10" runat="server" ControlToValidate="txtBuyingPrice1"
                                            Display="None" ErrorMessage="Please enter valid Buying Price" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Text="Invalid Price" Type="Double" ValidationGroup="Group11" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("BuyingPrice", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle/>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Selling Price" SortExpression="Selling Price">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSellingPrice1" runat="server" Text='<%# Bind("SellingPrice") %>'
                                            SkinID="Price_100px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredField11" runat="server" ControlToValidate="txtSellingPrice1"
                                            Display="None" ErrorMessage="Please enter selling price in grid" SetFocusOnError="True"
                                            ValidationGroup="Group11"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cmpRate11" runat="server" ControlToValidate="txtSellingPrice1"
                                            Display="None" ErrorMessage="Please enter valid slling price" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Text="Invalid Price" Type="Double" ValidationGroup="Group11" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("SellingPrice", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount Price" SortExpression="DiscountPrice">
                                 
                                    <ItemTemplate>
                                        <asp:Label ID="lblPDP" runat="server" Text='<%# Bind("DiscountPrice", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Consumption" SortExpression="UnitConsumption" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtucp" runat="server" Text='<%# Bind("UnitConsumption") %>'></asp:TextBox>
                                         <ajaxToolkit:FilteredTextBoxExtender ID="filter_ucp" runat="server" TargetControlID="txtucp" ValidChars="0123456789"  />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblucp" runat="server" Text='<%# Bind("UnitConsumption", "{0:N0}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notes" SortExpression="Notes" ItemStyle-CssClass="col-nowrap">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNotes1" runat="server" Text='<%# Bind("Notes") %>' TextMode="MultiLine"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle  />
                                </asp:TemplateField>
                               

                                <asp:TemplateField>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonDelete1" runat="server" CommandArgument='<%# Bind("ID")%>'
                                            CommandName="Delete" SkinID="BtnLinkDelete" ToolTip="Delete" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"/>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                   
                        <asp:GridView ID="GridView3" runat="server" Width="100%" AllowPaging="True" OnPageIndexChanging="GridView3_PageIndexChanging"
                            OnRowCancelingEdit="GridView3_RowCancelingEdit" OnRowCommand="GridView3_RowCommand"
                            OnRowDeleting="GridView3_RowDeleting" OnRowEditing="GridView3_RowEditing" OnRowUpdating="GridView3_RowUpdating">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderStyle Width="45px" />
                                    <ItemStyle Width="45px" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" CommandArgument='<%# Bind("ID")%>'
                                            SkinID="BtnLinkUpdate" ToolTip="Update" ValidationGroup="Group12">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description"  ItemStyle-CssClass="col-nowrap">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSDesc1" runat="server" Text='<%# Bind("ServiceDescription") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtSDesc1"
                                            Display="None" ErrorMessage="Please enter description" ValidationGroup="Group12"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ServiceDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="250px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Setup Buy">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_SetupBuy" runat="server" Text='<%# Bind("SetupBuy", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtGrid3_SetupBuy" runat="server" Width="50px" Text='<%# Bind("SetupBuy") %>'></asp:TextBox>
                                        <asp:CompareValidator ID="CV_Grid3_SetupBuy" runat="server" ControlToValidate="txtGrid3_SetupBuy"
                                            Display="None" ErrorMessage="Please enter valid Setup buying Price" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Double" ValidationGroup="Group12"></asp:CompareValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Setup Sell">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_SetupSell" runat="server" Text='<%# Bind("SetupSell", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtGrid3_SetupSell" runat="server" Width="50px" Text='<%# Bind("SetupSell") %>'></asp:TextBox>
                                        <asp:CompareValidator ID="CV_Grid3_SetupSell" runat="server" ControlToValidate="txtGrid3_SetupSell"
                                            Display="None" ErrorMessage="Please enter valid Setup selling Price" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Double" ValidationGroup="Group12"></asp:CompareValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Materials Buy">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_MaterialsBuy" runat="server" Text='<%# Bind("MaterialsBuy", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtGrid3_MaterialsBuy" runat="server" Width="50px" Text='<%# Bind("MaterialsBuy") %>'></asp:TextBox>
                                        <asp:CompareValidator ID="CV_Grid3_MaterialsBuy" runat="server" ControlToValidate="txtGrid3_MaterialsBuy"
                                            Display="None" ErrorMessage="Please enter valid Materials buying Price" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Double" ValidationGroup="Group12"></asp:CompareValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Materials Sell">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_MaterialsSell" runat="server" Text='<%# Bind("MaterialsSell", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtGrid3_MaterialsSell" runat="server" Width="50px" Text='<%# Bind("MaterialsSell") %>'></asp:TextBox>
                                        <asp:CompareValidator ID="CV_Grid3_MaterialsSell" runat="server" ControlToValidate="txtGrid3_MaterialsSell"
                                            Display="None" ErrorMessage="Please enter valid Materials selling Price" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Double" ValidationGroup="Group12"></asp:CompareValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Labour Buy">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_LabourBuy" runat="server" Text='<%# Bind("LabourBuy", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtGrid3_LabourBuy" runat="server" Width="50px" Text='<%# Bind("LabourBuy") %>'></asp:TextBox>
                                        <asp:CompareValidator ID="CV_Grid3_LabourBuy" runat="server" ControlToValidate="txtGrid3_LabourBuy"
                                            Display="None" ErrorMessage="Please enter valid Labour buying Price" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Double" ValidationGroup="Group12"></asp:CompareValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Labour Sell">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_LabourSell" runat="server" Text='<%# Bind("LabourSell", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtGrid3_LabourSell" runat="server" Width="50px" Text='<%# Bind("LabourSell") %>'></asp:TextBox>
                                        <asp:CompareValidator ID="CV_Grid3_LabourSell" runat="server" ControlToValidate="txtGrid3_LabourSell"
                                            Display="None" ErrorMessage="Please enter valid Labour selling Price" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Double" ValidationGroup="Group12"></asp:CompareValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount Price" SortExpression="DiscountPrice">
                                    <%--<EditItemTemplate>
                                                <asp:TextBox ID="txtSDPrice1" runat="server" Text='<%# Bind("DiscountPrice") %>'
                                                    Width="60px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldSDP" runat="server" ControlToValidate="txtSDPrice1"
                                                    Display="None" ErrorMessage="Please enter selling price in grid" SetFocusOnError="True"
                                                    ValidationGroup="Group11"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cmpRatePDP" runat="server" ControlToValidate="txtSDPrice1"
                                                    Display="None" ErrorMessage="Please enter valid Discount price" Operator="DataTypeCheck"
                                                    SetFocusOnError="True" Text="Invalid Price" Type="Double" ValidationGroup="Group12" />
                                            </EditItemTemplate>--%>
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
                                    <ItemStyle />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Service Buy">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_TotalBuy" runat="server" Text='<%# Bind("TotalServiceBuy", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Service Sell">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_TotalSell" runat="server" Text='<%# Bind("TotalServiceSell", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GP%">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrid3_GP" runat="server" Text='<%# Bind("GP", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                       <%-- <ajaxToolkit:AnimationExtender ID="MyExtender" runat="server" TargetControlID="pnlOriginalService">
                                            <Animations>
         
            <OnMouseOver>
            <FadeIn Duration=".5" Fps="20" />
            </OnMouseOver>
                                            </Animations>
                                        </ajaxToolkit:AnimationExtender>--%>
                                      <%--  <ajaxToolkit:HoverMenuExtender ID="hmeServices" runat="server" TargetControlID="imgContractor"
                                            PopupControlID="pnlOriginalService" PopDelay="0" PopupPosition="Left" EnableViewState="false"
                                            OffsetY="26" />--%>
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                            Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        
                                      <%--  <div id="pnlOriginalService" runat="server" class="PrepRecipeDetails" style="display: none;">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.OriginalData) %>'
                                                Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        </div>--%>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="More Options">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgSMOptions" runat="server" CausesValidation="false" CommandName="MoreOptions"
                                            SkinID="BtnLinkMore" CommandArgument='<%# Bind("ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonDelete1" runat="server" CommandName="Delete" CommandArgument='<%# Bind("ID") %>'
                                            SkinID="BtnLinkDelete" ToolTip="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                   
                    <asp:HiddenField ID="HD_pageType" runat="server" />
                    <asp:SqlDataSource ID="MyDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="DS_Category" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Deffinity_GetServiceCategory" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="PortfolioID" SessionField="PortfolioID"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="HD_pageType" Name="PageType" DefaultValue="1" Type="Int32" />
                            <asp:QueryStringParameter ConvertEmptyStringToNull="true" QueryStringField="VendorID"
                                Name="VendorID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="DS_SubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Deffinity_GetServiceSubCategory" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCategory" DefaultValue="0" Name="CategoryId"
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:SessionParameter DefaultValue="0" Name="PortfolioID" SessionField="PortfolioID"
                                Type="Int32" />
                            <asp:QueryStringParameter ConvertEmptyStringToNull="true" QueryStringField="VendorID"
                                Name="VendorID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </asp:Panel>
            
          
            <div>
                <asp:Label ID="dummy" runat="server"></asp:Label>
                <asp:Button ID="btnLabour_dummy" runat="server" SkinID="ImgCancel" Style="visibility: hidden" />
                <ajaxToolkit:ModalPopupExtender ID="mdl_Labour" runat="server" CancelControlID="btnCancelrow"
                    BackgroundCssClass="modalBackground" TargetControlID="btnAddnew" PopupControlID="pnl_Labour" />
                <asp:Panel ID="pnl_Labour" runat="server" Width="900px" Height="400px" Visible="false"
                    ScrollBars="Both" BackColor="White" Style="display: none" BorderStyle="Double"
                    BorderColor="LightSteelBlue">
                    
<div class="form-group row">
    <asp:ValidationSummary ID="mdl_lbrValdSum" runat="server" Width="196px" ValidationGroup="Group1">
                    </asp:ValidationSummary>
     <asp:Label ID="Label5" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    </div>
        <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>  Add Labour </strong>
            <hr class="no-top-margin" />
            </div>
    </div>            
           <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Description%></label>
                                      <div class="col-sm-8">
                                           <asp:TextBox ID="txtDescription" runat="server" SkinID="txtMulti"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldDescription" runat="server" ControlToValidate="txtDescription"
                                    Display="None" ErrorMessage="Please enter Description" SetFocusOnError="True"
                                    ValidationGroup="Group1"></asp:RequiredFieldValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Notes%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtNotes" runat="server" SkinID="txtMulti" TextMode="MultiLine"></asp:TextBox>
					</div>
				</div>
</div>    
                    <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.BuyingPrice%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtBuyingPrice" runat="server" CausesValidation="True" 
                                            SkinID="Price_75px"  MaxLength="20"></asp:TextBox>
                                        <asp:CompareValidator ID="cmpRate" runat="server" 
                                            ControlToValidate="txtBuyingPrice" Display="None" 
                                            ErrorMessage="Please enter valid Buying Price" Operator="DataTypeCheck" 
                                            SetFocusOnError="True" Type="Double" ValidationGroup="Group1"></asp:CompareValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Markup%></label>
                                      <div class="col-sm-8 form-inline"> <asp:TextBox ID="txtLPercent" runat="server" SkinID="Price_50px"></asp:TextBox>
                                        <asp:LinkButton SkinID="BtnLinkIndent" ID="btnLindentity" runat="server"  OnClientClick="LBP_Dsc();return false;" />
					</div>
				</div>
</div>    
                    <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.SellingPrice%></label>
                                      <div class="col-sm-8  form-inline"> <asp:TextBox ID="txtSellingPrice" runat="server" SkinID="Price_100px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                            ControlToValidate="txtSellingPrice" Display="None" 
                                            ErrorMessage="Please enter valid Selling Price" Operator="DataTypeCheck" 
                                            SetFocusOnError="True" Type="Double" ValidationGroup="Group1"></asp:CompareValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Discount%></label>
                                      <div class="col-sm-8  form-inline"> <asp:TextBox ID="txtLSPercent" runat="server"  
                                             SkinID="Price_50px"></asp:TextBox>
                                        <asp:LinkButton ID="btnLinkIndent1" runat="server" SkinID="BtnLinkCheckIn" OnClientClick="LSP_Dsc();return false;" />
					</div>
				</div>
</div>
                    <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.DiscountPricetoCustomer%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtLDPrice" runat="server" SkinID="Price_100px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidatorLDP" runat="server" 
                                            ControlToValidate="txtLDPrice" Display="None" 
                                            ErrorMessage="Please enter valid Discount Price" Operator="DataTypeCheck" 
                                            SetFocusOnError="True" Type="Double" ValidationGroup="Group1"></asp:CompareValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.UnitConsumption%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtunitconsumption" runat="server" SkinID="txt_100px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator16" runat="server" 
                                            ControlToValidate="txtunitconsumption" Display="None" 
                                            ErrorMessage="Please enter valid Unit Consumption" Operator="DataTypeCheck" 
                                            SetFocusOnError="True" Type="Double" ValidationGroup="Group1"></asp:CompareValidator>
					</div>
				</div>
</div>
                    <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.RateType%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlRateType" runat="server" Width="150px">
                                        </asp:DropDownList>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.RoutetoServiceTeam%></label>
                                      <div class="col-sm-8">  <asp:DropDownList ID="ddlTeam" runat="server" DataSourceID="obj_team" 
                                            DataTextField="TeamName" DataValueField="ID">
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="obj_team" runat="server" 
                                            SelectMethod="Method_SelectTeam" TypeName="Deffinity.SDTeam_Manager.SDTeam">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="portfolioid" 
                                                    SessionField="PortfolioID" Type="Int32" />
                                                <asp:Parameter DefaultValue="0" Name="AreaID" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
					</div>
				</div>
</div>
                    <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.UploadImage%></label>
                                      <div class="col-sm-8">  <asp:FileUpload ID="FileUploadLabour" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" 
                                            runat="server" ControlToValidate="FileUploadLabour" Display="None" 
                                            ErrorMessage="" 
                                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*.*))+\.(jpg|JPG|gif|GIF|png|PNG)$" 
                                            ValidationGroup="Group1">File</asp:RegularExpressionValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8">
					</div>
				</div>
</div>
                    <div class="form-group row">
                                  <div class="col-md-6 pull-right">
                                      <asp:Button ID="btnSaveRecord" runat="server" 
                                            OnClick="btnSaveRecord_Click" SkinID="btnSave" ValidationGroup="Group1" />
                                        <asp:Button ID="imgbtnUpdateLabour" runat="server" 
                                            OnClick="imgbtnUpdateLabour_Click" SkinID="btnUpdate" ValidationGroup="Group1" 
                                            Visible="False" />
                                        <asp:Button ID="btnCancelrow" runat="server" AlternateText="Cancel" 
                                            CausesValidation="False" OnClick="btnCancelrow_Click" SkinID="btnCancel" />
                                      </div>
                        </div>
                </asp:Panel>
            </div>
            <div>
                <ajaxToolkit:ModalPopupExtender ID="mdl_Products" runat="server" CancelControlID="ImageButton2"
                    BackgroundCssClass="modalBackground" TargetControlID="btnAddnewProduct" PopupControlID="pnl_Product" />
                <asp:Button ID="btnProduct_dummy" runat="server" SkinID="btnCancel" Style="visibility: hidden" />
                <asp:Panel ID="pnl_Product" runat="server" Width="740px" Height="500px" Visible="false"
                    ScrollBars="None" BackColor="White" Style="display: none" BorderStyle="Double"
                    BorderColor="LightSteelBlue" CssClass="panel panel-color panel-info">

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
               <ContentTemplate>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Literal ID="lblMCatelogue" runat="server" Text="Add Catalog Item"></asp:Literal>   </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="btnCancelprodcut" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" OnClick="btnCancelrow_Click"/>
								
							</div>
						</div>
    <div class="panel-body">
       <div class="form-group row">
                        <asp:ValidationSummary ID="mdl_PrdValdSum" runat="server" ValidationGroup="MaterialGroup">
                        </asp:ValidationSummary>
                        <asp:Label ID="Label6" runat="server" ForeColor="Red" Visible="false"></asp:Label></div>

         <asp:Panel ID="pnl_ProductDetails" runat="server" Height="320px" ScrollBars="Vertical" style="overflow-x:hidden">
                 
                        <div class="form-group row">
                                  <div class="col-md-8">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Description%></label>
                                      <div class="col-sm-8">
                                           <asp:TextBox ID="txtItemDesc" runat="server" MaxLength="200" SkinID="txtMulti" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemDesc"
                                        Display="None" ErrorMessage="Please enter Description" SetFocusOnError="True"
                                        ValidationGroup="MaterialGroup"></asp:RequiredFieldValidator>
					</div>
				</div>

</div>
                        <div class="form-group row">
                            <div class="col-sm-8">
                                <label class="col-sm-3 control-label"> <%= Deffinity.systemdefaults.GetCategoryName() %></label>
                                <div class="col-sm-8">  <asp:DropDownList ID="ddlCategory" runat="server" SkinID="ddl_70" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" DataValueField="Id"
                                        DataTextField="CategoryName">
                                    </asp:DropDownList>

                                  
                                </div>
                            </div>
                            <div  class="col-sm-4">
                                  <asp:HyperLink ID="linkAdmin" runat="server" Text="Configure Equipment Brand" NavigateUrl="~/WF/DC/FLSDefault.aspx?tab=fls&type=category" Font-Bold="true"></asp:HyperLink>
                            </div>
                        </div>
                          <div class="form-group row">
                            <div class="col-sm-8">
                                <label class="col-sm-3 control-label"> <%= Deffinity.systemdefaults.GetSubCategoryName() %></label>
                                <div class="col-sm-8">  
                                     <asp:DropDownList ID="ddlSubCategory" runat="server" SkinID="ddl_70" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                                        <asp:ListItem Text=" Please Select..." Value="0"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                    <div class="form-group row">
                      <div class="col-md-8">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Partnumber%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtPartNumber" runat="server" ></asp:TextBox>
					</div>
				</div>
                        </div>
                    <div class="form-group row">
                                  <div class="col-md-8">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.BuyingPrice%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtMBuyingPrice" runat="server" CausesValidation="True" SkinID="Price_100px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtMBuyingPrice"
                                        Display="None" ErrorMessage="Please enter valid Material Buying Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="MaterialGroup"></asp:CompareValidator>
					</div>
				</div>
 
</div>
                    <div class="form-group row">
                                  <div class="col-md-8">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.SellingPrice%></label>
                                      <div class="col-sm-8  form-inline"> <asp:TextBox ID="txtMSellingPrice" runat="server" SkinID="Price_100px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtMSellingPrice"
                                        Display="None" ErrorMessage="Please enter valid Selling Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="MaterialGroup"></asp:CompareValidator>
					</div>
				</div>

</div>
                    <div class="form-group row" style="display:none;visibility:hidden">
                                
 <div class="col-md-8">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.DiscountPricetoCustomer%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtPDPrice" runat="server" SkinID="Price_100px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="txtPDPrice"
                                        Display="None" ErrorMessage="Please enter valid Discount Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="MaterialGroup"></asp:CompareValidator>
					</div>
				</div>
</div>
                    
                      <div class="form-group row" style="display:none;visibility:hidden;">
                                
 <div class="col-md-8">
                                       <label class="col-sm-3 control-label"> Type</label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtMItemType" runat="server" ></asp:TextBox>
                                   
					</div>
				</div>
</div>
                      <div class="form-group row">
                                
 <div class="col-md-8">
                                       <label class="col-sm-3 control-label"> QB Ref ID</label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtMQBRefID" runat="server" ></asp:TextBox>
                                   
					</div>
				</div>
</div>
                    
                    <div class="form-group row">
                                  <div class="col-md-8">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Notes%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtMNotes" runat="server" SkinID="txtMulti"></asp:TextBox>
					</div>
				</div>
 
</div>
                    <div class="form-group row">
                        <div class="col-md-8">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.UploadImage%></label>
                                      <div class="col-sm-8"><asp:FileUpload ID="FileUploadMaterial" runat="server"></asp:FileUpload>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                        ControlToValidate="FileUploadMaterial" Display="None" ErrorMessage="Please select JPG,GIF or PNG."
                                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*.*))+\.(jpg|JPG|gif|GIF|png|PNG)$"
                                        ValidationGroup="MaterialGroup">File</asp:RegularExpressionValidator>
					</div>
				</div>

                        </div>

                    </asp:Panel>

 
        
</div>
           <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       <div class="col-sm-12 form-inline">
                      <asp:Button ID="btnSaveMaterial" runat="server" OnClick="btnSaveMaterial_Click"
                                        SkinID="btnSave" ValidationGroup="MaterialGroup"></asp:Button><asp:Button
                                            ID="imgbtnUpdateMaterial" runat="server" OnClick="imgbtnUpdateMaterial_Click"
                                            SkinID="btnUpdate" ValidationGroup="MaterialGroup" Visible="False"></asp:Button>
                                    <asp:Button ID="ImageButton2" runat="server" AlternateText="Cancel" CausesValidation="False"
                                        OnClick="btnCancelrow_Click" SkinID="btnCancel"></asp:Button>
                           </div>
                       </div>
               </div>
                     </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="btnCancelprodcut" />
                   <asp:PostBackTrigger ControlID="btnSaveMaterial" />
                    <asp:PostBackTrigger ControlID="imgbtnUpdateMaterial" />
               </Triggers>
           </asp:UpdatePanel>




                   

                    
                   

                    <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8">
                                          
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8">
					</div>
				</div>
</div>


                    <div>
                         <div class="col-md-6"  style="display:none;visibility:hidden;">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Discount%></label>
                                      <div class="col-sm-8  form-inline">
                                           <asp:TextBox ID="txtPSPercent" runat="server" SkinID="Price_50px"></asp:TextBox>
                                    <asp:LinkButton SkinID="BtnLinkIndent" runat="server" OnClientClick="PSP_Dsc();return false;" />
					</div>
				</div>
                        <div class="col-md-6" style="display:none;visibility:hidden;">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Markup%></label>
                                      <div class="col-sm-8 form-inline"> <asp:TextBox ID="txtPPercent" runat="server" SkinID="Price_50px"></asp:TextBox>
                                    <asp:LinkButton SkinID="BtnLinkIndent" runat="server" OnClientClick="PBP_Dsc();return false;" />
					</div>
				</div>
                        <div class="form-group row"  style="display:none;visibility:hidden;">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Supplier%></label>
                                      <div class="col-sm-8 form-inline"> <asp:DropDownList ID="ddlSupplier" runat="server" SkinID="ddl_80">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtSupplier" runat="server" Visible="False" SkinID="txt_80"></asp:TextBox>
                                    <asp:LinkButton ID="btnAddSupplier" runat="server" Visible="false" OnClick="btnAddSupplier_Click"
                                        SkinID="BtnLinkAdd"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelSupplier" runat="server" OnClick="btnCancelSupplier_Click"
                                        Visible="False" SkinID="BtnLinkCancel"></asp:LinkButton>
					</div>
				</div>
 <div class="col-md-6"  style="display:none;visibility:hidden;">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Stocklevel%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtStockLevel" runat="server" SkinID="Price_100px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="txtStockLevel"
                                        Display="None" ErrorMessage="Please enter valid Stock Level" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="MaterialGroup"></asp:CompareValidator>
					</div>
				</div>
</div>
                        <div class="form-group row"  style="display:none;visibility:hidden;">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.UnitConsumption%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtucproduct" runat="server" SkinID="Price_75px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator17" runat="server" ControlToValidate="txtucproduct"
                                        Display="None" ErrorMessage="Please enter valid Unit Consumption" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="MaterialGroup"></asp:CompareValidator>
					</div>
				</div>
 <div class="col-md-6"  style="display:none;visibility:hidden;">
                                       <label class="col-sm-4 control-label"> <asp:Label runat="server" ID="lblUnit" Text="Unit Price"></asp:Label>  </label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtUnit" runat="server" SkinID="Price_100px"></asp:TextBox>
                                           <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtUnit"
                                        Display="None" ErrorMessage="Please enter valid Unit Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="MaterialGroup"></asp:CompareValidator>
					</div>
				</div>
</div>

                    </div>
                </asp:Panel>
            </div>
            <div>
                <ajaxToolkit:ModalPopupExtender ID="mdl_Service" runat="server" CancelControlID="lnkCancel"
                    BackgroundCssClass="modalBackground" TargetControlID="btnAddnewService" PopupControlID="pnl_Service" />
                <asp:Button ID="btnMisc_dummy" runat="server" SkinID="btnCancel" Style="visibility: hidden" />
                <asp:Panel ID="pnl_Service" runat="server" Width="900px" Height="400px" Visible="false"
                    ScrollBars="Both" BackColor="White" Style="display: none" BorderStyle="Double"
                    BorderColor="LightSteelBlue">
                    <div class="form-group row">
                        <asp:ValidationSummary ID="mdl_servValSum" runat="server" Width="196px" ValidationGroup="ServiceGroup">
                        </asp:ValidationSummary>
                        <asp:Label ID="Label7" runat="server" ForeColor="Red" Visible="false"></asp:Label></div>
                    <div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong>  Add Service </strong>
            <hr class="no-top-margin" />
            </div>
</div>
                    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Description %></label>
                                      <div class="col-sm-10"><asp:TextBox ID="txtSDescription" runat="server" SkinID="txtMulti"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSDescription"
                                        Display="None" ErrorMessage="Please enter Description" SetFocusOnError="True"
                                        ValidationGroup="ServiceGroup"></asp:RequiredFieldValidator>
					</div>
				</div>

</div>
                    <div class="form-group row">
                                  <div class="col-md-4">
                                       <label class="col-sm-5 control-label"> <%= Resources.DeffinityRes.SetupBuy%></label>
                                      <div class="col-sm-7"> <asp:TextBox ID="txtSetupBuy" runat="server" SkinID="Price_75px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtSetupBuy"
                                        Display="None" ErrorMessage="Please enter valid Setup buying Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
					</div>
				</div>
 <div class="col-md-4">
                                       <label class="col-sm-5 control-label"> <%= Resources.DeffinityRes.MaterialsBuy %></label>
                                      <div class="col-sm-7"> <asp:TextBox ID="txtMaterialsBuy" runat="server" SkinID="Price_75px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtMaterialsBuy"
                                        Display="None" ErrorMessage="Please enter valid Materials buying Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
					</div>
				</div>
<div class="col-md-4">
                                       <label class="col-sm-5 control-label"> <%= Resources.DeffinityRes.LabourBuy%></label>
                                      <div class="col-sm-7"> <asp:TextBox ID="txtLabourBuy" runat="server" SkinID="Price_75px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtLabourBuy"
                                        Display="None" ErrorMessage="Please enter valid Labour buying Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
					</div>
				</div>
</div>
                    <div class="form-group row">
                                  <div class="col-md-4">
                                       <label class="col-sm-5 control-label"> <%= Resources.DeffinityRes.SetupSell%></label>
                                      <div class="col-sm-7">
                                          <asp:TextBox ID="txtSetupSell" runat="server" SkinID="Price_75px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtSetupSell"
                                        Display="None" ErrorMessage="Please enter valid Setup selling Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
					</div>
				</div>
 <div class="col-md-4">
                                       <label class="col-sm-5 control-label"> <%= Resources.DeffinityRes.MaterialsSell %></label>
                                      <div class="col-sm-7"> <asp:TextBox ID="txtMaterialsSell" runat="server" SkinID="Price_75px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="txtMaterialsSell"
                                        Display="None" ErrorMessage="Please enter valid Materials selling Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
					</div>
				</div>
<div class="col-md-4">
                                       <label class="col-sm-5 control-label"> <%= Resources.DeffinityRes.LabourSell%></label>
                                      <div class="col-sm-7">
                                           <asp:TextBox ID="txtLabourSell" runat="server" MaxLength="200" SkinID="Price_75px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtLabourSell"
                                        Display="None" ErrorMessage="Please enter valid Labour selling Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
					</div>
				</div>
</div>
                    <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">Total Buy</label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtTotBuy" runat="server" SkinID="Price_75px"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Markup%></label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txtSPercent" runat="server" SkinID="Price_75px"></asp:TextBox>
                                    <asp:LinkButton SkinID="BtnLinkIndent" runat="server" OnClientClick="SerB_Dsc();return false;" />
					</div>
				</div>
</div>
                    <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> Total Sell</label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtTotSell" runat="server" SkinID="Price_75px"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Discount%>%</label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txtSDPercent" runat="server" SkinID="Price_75px"></asp:TextBox>
                                    <asp:LinkButton SkinID="BtnLinkIndent" runat="server" OnClientClick="SerS_Dsc();return false;" />
					</div>
				</div>
</div>
                    <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.UnitConsumption%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtucservice" runat="server" SkinID="Price_100px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator18" runat="server" ControlToValidate="txtucservice"
                                        Display="None" ErrorMessage="Please enter valid Unit Consumption" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.DiscountPricetoCustomer%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtSDprice" runat="server" SkinID="Price_100px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidatorSDPrice" runat="server" ControlToValidate="txtSDprice"
                                        Display="None" ErrorMessage="Please enter valid Discount Price" Operator="DataTypeCheck"
                                        SetFocusOnError="True" Type="Double" ValidationGroup="ServiceGroup"></asp:CompareValidator>
					</div>
				</div>
</div>
                    <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.UploadImage%></label>
                                      <div class="col-sm-8"><asp:FileUpload ID="FileUploadService" runat="server"></asp:FileUpload>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                        ControlToValidate="FileUploadService" Display="None" ErrorMessage="Please select JPG,GIF or PNG."
                                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*.*))+\.(jpg|JPG|gif|GIF|png|PNG)$"
                                        ValidationGroup="ServiceGroup">File</asp:RegularExpressionValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8">
					</div>
				</div>
</div>
                    <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8"> <asp:Button ID="btnSaveServices" OnClick="btnSaveServices_Click" runat="server"
                                        SkinID="btnSave" ValidationGroup="ServiceGroup"></asp:Button>
                                    <asp:Button ID="imgbtnUpdateService" runat="server" ValidationGroup="ServiceGroup"
                                        Visible="False" OnClick="imgbtnUpdateService_Click" SkinID="btnUpdate"></asp:Button>
                                    <asp:Button ID="btnCancelServces" OnClick="btnCancelServces_Click" runat="server"
                                        SkinID="btnCancel" CausesValidation="False" AlternateText="Cancel"></asp:Button>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8">
					</div>
				</div>
</div>
                </asp:Panel>
            </div>
           
            <div>
                <asp:HiddenField ID="hdnItemID" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hdnImageID" runat="server"></asp:HiddenField>
            </div>
            <%-- </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSaveRecord" />
                    <asp:PostBackTrigger ControlID="btnSaveMaterial" />
                    <asp:PostBackTrigger ControlID="btnSaveServices" />
                    <asp:PostBackTrigger ControlID="btnSaveServiceMinusLabour" />
                    <asp:PostBackTrigger ControlID="imgbtnUpdateLabour" />
                    <asp:PostBackTrigger ControlID="imgbtnUpdateMaterial" />
                    <asp:PostBackTrigger ControlID="imgbtnUpdateService" />
                    <asp:PostBackTrigger ControlID="btnUpdateServiceMinusLabour" />
                </Triggers>
            </asp:UpdatePanel>--%>
        
<asp:Panel ID="pnlBOM" runat="server" BackColor="White" Style="display: none"  Width="850px"
                        Height="505px" BorderStyle="Double" ScrollBars="None" BorderColor="LightSteelBlue">
                         <div style="float:right" >
    <asp:Button ID="ImgCancel" runat="server"  SkinID="btnCancel"  Style="display: none"/></div>
     <div>
                            <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label></div>
                          <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 98%">
                            <asp:Label ID="lblTaskName" runat="server" Text="<%$ Resources:DeffinityRes,ServiceCatalogueItems%>"></asp:Label>
                        </div>
                       
                        <iframe id="ifrmCatelog" runat="server" marginheight="0" marginwidth="0" width="850px" height="440px" frameborder="0" scrolling="no" ></iframe>
                       
                      
                        <div style="text-align: left">
                            <asp:Button ID="imgUpdate" runat="server" SkinID="btnClose" OnClick="imgUpdate_Click" />
                           <%-- <asp:ImageButton ID="ImgCancel" runat="server" SkinID="ImgCancel" />--%>
                            <asp:Button runat="server" ID="imgItemEdit" Style="display: none" />
                        </div>
                    </asp:Panel>



             
                                </div>
                </div>
            </div>
