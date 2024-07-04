<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectBOM" EnableEventValidation="false" Codebehind="ProjectBOM.aspx.cs" %>

<%@ Register Src="controls/ProjectTabs.ascx" TagName="ProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/ProjectCost.ascx" TagName="ProjectCost" TagPrefix="uc2" %>
<%--<%@ Register Src="controls/BOMUploadCtrl.ascx" TagName="BOMUpload" TagPrefix="uc3" %>--%>
<%@ Register Src="controls/Project_BudgetSubTab.ascx" TagName="BudgetTab" TagPrefix="uc4" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
      <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.BillofMaterials%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        div{
           overflow-y:visible
        }
    </style>
     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
   
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
                            alert("Please Select only one checkbox");
                            document.forms[0].elements[i].checked = false;
                            return false;

                        }
                    }

                }
            }
            if (checked == 0) {
                alert("Please Select atleast one checkbox");
                return false;
            }
            else
                if (checked == 2) {
                    alert("Please Select only one checkbox");
                    return false;
                    document.forms[0].elements[second].checked == false;
                }
        }
        
    </script>

  <%--  <script src="../../Scripts/GridDesingFix.js"></script>--%>

   

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
        $(document).ready(function () {
            $("#<%=gridBOM.ClientID%> input[id*='chkbox']:checkbox").click(CheckUncheckAllCheckBoxAsNeeded);
            $("#<%=gridBOM.ClientID%> input[id*='chkAll']:checkbox").click(function () {
                if ($(this).is(':checked'))
                    $("#<%=gridBOM.ClientID%> input[id*='chkbox']:checkbox").attr('checked', true);
                else
                    $("#<%=gridBOM.ClientID%> input[id*='chkbox']:checkbox").attr('checked', false);
            });

            $("#ddlSnapshot").change(function (e) {
               
            });
        });

     
        function CheckUncheckAllCheckBoxAsNeeded() {
            var totalCheckboxes = $("#<%=gridBOM.ClientID%> input[id*='chkbox']:checkbox").size();
            var checkedCheckboxes = $("#<%=gridBOM.ClientID%> input[id*='chkbox']:checkbox:checked").size();

            if (totalCheckboxes == checkedCheckboxes) {
                $("#<%=gridBOM.ClientID%> input[id*='chkAll']:checkbox").attr('checked', true);
            }
            else {
                $("#<%=gridBOM.ClientID%> input[id*='chkAll']:checkbox").attr('checked', false);
            }
        }
    </script>

 <uc4:BudgetTab ID="BudgetTab1" runat="server" />

     <uc2:ProjectCost ID="ProjectCostCntl" runat="server" />

    <div class="form-group">
          <div class="col-md-12">
              <asp:RequiredFieldValidator ID="rfv_worksheet" runat="server" ControlToValidate="ddlWorksheet"
                                InitialValue="0" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectworksheet%>"
                                ForeColor="Red" ValidationGroup="Group_ws_select"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummary9" runat="server" ValidationGroup="grpser1" />
                            <asp:ValidationSummary ID="ValidationSummary1" ForeColor="Red" runat="server" ValidationGroup="Group1"
                                Visible="true" />
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group2" />
                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ForeColor="Red" ValidationGroup="Group3" />
                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ForeColor="Red" ValidationGroup="Group4" />
                            <asp:ValidationSummary ID="ValidationSummary5" runat="server" ForeColor="Red" ValidationGroup="Group5" />
                            <asp:Label ID="lblError" runat="server" Visible="false" EnableViewState="false"></asp:Label>
                            <asp:Label ID="lblMsg" runat="server" Visible="false" ForeColor="Red" EnableViewState="false"></asp:Label>
	</div>
</div>
      
    <div>
     
       
         
     <%--   <div>--%>
       
                     <asp:Panel ID="pnlSavedWorksheets" runat="server" Width="100%" ClientIDMode="Static" Height="230px" ScrollBars="None">
                         <div class="form-group">
                                     <div class="col-md-12">
                                                    <strong> Saved Worksheets
                                                     <label id="Label1" runat="server" visible="false" /> </strong> 
                                                     <hr class="no-top-margin" />
                                     </div>
                         </div>
                         <div>
                                  <div>
                           <asp:Label ID="lblSavedMsg" runat="server" EnableViewState="false" ForeColor="Green"></asp:Label>
                    </div>
                                 <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.SavedWorkSheets%></label>
                                       <div class="col-sm-3">
                                           <asp:DropDownList ID="ddlSavesWorkSheets" runat="server" OnSelectedIndexChanged="ddlSavesWorkSheets_SelectedIndexChanged"></asp:DropDownList>
                                       </div>
                                       <div class="col-sm-7">
                                            <asp:Button ID="imgWrkSheetID" runat="server" SkinID="btnApply" OnClick="imgWrkSheetID_Click" CausesValidation="false" />
                                    <asp:Button ID="btnEdit_savedworksheet" runat="server" SkinID="btnEdit" ToolTip="Edit selected saved worksheet name"
                                         OnClick="btnEdit_savedworksheet_Click" CausesValidation="false" />
                                    <asp:Button ID="btnUpdateWorksheetData" runat="server" Text="Save to Existing Worksheet Template"
                                         OnClientClick="return confirm('Do you want to update saved worksheet?');" OnClick="btnUpdateWorksheetData_Click" CausesValidation="false" />
                                    <asp:LinkButton ID="btnDel_savedworksheet" runat="server"
                                        SkinID="BtnLinkDelete" ToolTip="Delete selected saved worksheet" OnClientClick="return confirm('Do you want to delete the worksheet?');"
                                        OnClick="btnDel_savedworksheet_Click" />
                                       </div>
                                  </div>
                    </div>
                        </div>
                     </asp:Panel>
      
         
            <asp:Panel ID="pnlUploadtemplate" runat="server" ScrollBars="None">
                  
                        <div class="form-group">
                                    <div class="col-md-12">
                                                   <strong> <%= Resources.DeffinityRes.UploadBOM%> </strong> 
                                                    <hr class="no-top-margin" />
                                    </div>
                        </div>
                        <div>
                         <asp:Label ID="lblUploadErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                        <div class="form-group">
                                    <div class="col-md-3">
                    <asp:FileUpload ID="fileUpload2" runat="server" />
                    </div>
                                    <div class="col-md-2">
                    <asp:Button ID="btnUploadData" CausesValidation="false"
                                                runat="server" SkinID="btnUpload" OnClick="btnUploadData_Click" />
                </div>
                        </div>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
                TargetControlID="pnlUploadtemplate" ExpandControlID="btnUploadTemplate" CollapseControlID="btnUploadTemplate"
                TextLabelID="Lbl1" ImageControlID="btnUploadTemplate" Collapsed="true" SuppressPostBack="true">
            </ajaxToolkit:CollapsiblePanelExtender>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                                                TargetControlID="pnlSavedWorksheets" ExpandControlID="lnkSavesWorkSheets" CollapseControlID="lnkSavesWorkSheets"
                                                TextLabelID="Lbl21" ImageControlID="lnkSavesWorkSheets" Collapsed="true" SuppressPostBack="true">
            </ajaxToolkit:CollapsiblePanelExtender>
                    </asp:Panel>
                



                    <div class="form-group">
                         <div class="col-md-12">
                             <strong> <%= Resources.DeffinityRes.Worksheet%>
                              <label id="lblworksheetname" runat="server" visible="false" /></strong>
                              <hr class="no-top-margin" />
                         </div>
                     </div>
                    
                    
                    

            <div>


                <div class="form-group">
                          <div class="col-md-12 form-inline">
                             <label class="col-sm-1 control-label"><%= Resources.DeffinityRes.Worksheet%></label>
                             <div class="col-sm-3">
                                 <asp:DropDownList ID="ddlWorksheet" runat="server" OnSelectedIndexChanged="ddlWorksheet_SelectedIndexChanged"
                                        AutoPostBack="True"></asp:DropDownList>
                                    <asp:HiddenField ID="HD_Type" runat="server" Value="0" Visible="False" />
                              </div>
                               <div class="col-sm-6">
                                     <asp:Button ID="btnAdd_worksheet" runat="server" SkinID="btnAdd" CausesValidation="false" CssClass="btn btn-secondary"
                                          ToolTip="<%$ Resources:DeffinityRes,Addanewworksheet%>"
                                                OnClick="btnAdd_worksheet_click" />
                                    <asp:Button ID="btnEdit_worksheet" runat="server" SkinID="btnEdit" Text="Edit" CssClass="btn btn-secondary"
                                                ToolTip="<%$ Resources:DeffinityRes,Edittheselectedworksheetname%>" OnClick="btnEdit_worksheet_click"
                                                ValidationGroup="Group_ws_select" />
                                   &nbsp;&nbsp;
                                    <asp:LinkButton ID="linkRpt" ToolTip="<%$ Resources:DeffinityRes,Print%>" runat="server"
                                                 SkinID="BtnLinkPrint" Target="_blank" OnClick="linkRpt_Click"></asp:LinkButton>
                                   &nbsp;&nbsp;
                                      <asp:LinkButton ID="btnDel_worksheet" runat="server" SkinID="BtnLinkDelete"
                                                ToolTip="<%$ Resources:DeffinityRes,Deleteselectedworksheetandassociateditems%>"
                                                OnClick="btnDel_worksheet_click" ValidationGroup="Group_ws_select"
                                                 OnClientClick="return confirm('Do you want to delete the worksheet?');"></asp:LinkButton>
                                       &nbsp;&nbsp;
                            <%--   </div>
                              <div class="col-sm-2">--%>
                                  <div class="btn-group">
									<button typte="button" class="btn btn-gray">More</button>
									<button type="button" class="btn btn-gray dropdown-toggle" data-toggle="dropdown" >
										<span class="caret"></span>
									</button>
									<ul class="dropdown-menu dropdown-green dropdown-menu-right" role="menu">
										<li>
											 <asp:LinkButton ID="lnkSavesWorkSheets" runat="server" Text="Saved Worksheets" CausesValidation="false"></asp:LinkButton> 
										</li>
                                        <li>
                                            <asp:LinkButton ID="BtndeletedList" runat="server" ToolTip="Recover Deleted BoM Worksheet(s)"
                                                           CausesValidation="false" Text="Recover" OnClick="BtndeletedList_Click"  />
                                        </li>
										<li>
											   <asp:LinkButton  ID="btnUploadTemplate" runat="server" Text="Upload BOM" CausesValidation="false" />
										</li>
										<li>
											 <asp:LinkButton ID="btnDownloadBoMTemplate" runat="server" Text="Download BOM Template"
                                                                 OnClick="btnDownloadBoMTemplate_Click" CausesValidation="false" />
										</li>
										<li>
											 <asp:LinkButton ID="lnkbtncatalog" ValidationGroup="Group_ws_select"
                                                                  runat="server" OnClick="lnkbtncatalog_Click" Text="<%$ Resources:DeffinityRes,Searchvendorcatalogue%>" />
										</li>
                                        <li>
                                              <asp:LinkButton  ID="btngenerate" runat="server" Text="Generate Quote" OnClick="btngenerate_Click"
                                                                                          ToolTip="<%$ Resources:DeffinityRes,Generateaquotation%>" CausesValidation="false" />
                                        </li>
                                        <li>
                                             <asp:LinkButton ID="imgsupReq" Text="Supplier Requisition" ToolTip="<%$ Resources:DeffinityRes,Generateasupplier%>"
                                                                                         runat="server" OnClick="imgsupReq_Click" CausesValidation="false" />
                                        </li>
                                         <li>
          <asp:LinkButton ID="BtnSnapShot" runat="server" Text="Take Snapshot" OnClick="BtnSnapShot_Click" CausesValidation="false"></asp:LinkButton>
      </li>
    <li>
         <asp:LinkButton ID="lnkViewSnapshots" runat="server" Text="View Snapshots" CausesValidation="false"></asp:LinkButton>
    </li>
									</ul>
	           </div>
                                  </div>
                          </div>
                </div>
            </div>
               <div style="width: 100%;">
            
                        <asp:GridView ID="gridBOM" runat="server" Width="100%" AutoGenerateColumns="false"
                            DataKeyNames="ID" OnRowCommand="gridBOM_RowCommand" OnRowDataBound="gridBOM_RowDataBound"
                            OnRowDeleting="gridBOM_RowDeleting" OnRowEditing="gridBOM_RowEditing" OnRowUpdating="gridBOM_RowUpdating"
                            OnRowCancelingEdit="gridBOM_RowCancelingEdit" GridLines="None">
                            <Columns>

                                <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" />
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                      <div class="form-group">
                                          <div class="col-md-12 form-inline">
                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible="false"> </asp:Label>
                                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" Enabled="<%#CommandField()%>"
                                                    CommandName="Edit" CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit"
                                                    ToolTip="Edit"></asp:LinkButton>
                                         
                                                <asp:CheckBox ID="chkbox" runat="server" Checked='<%#isChecked(DataBinder.Eval(Container.DataItem,"ID").ToString())%>'
                                                    EnableViewState="true" />
                                              </div>
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        
                                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                    CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate" ToolTip="Update"
                                                    ValidationGroup="Group2"></asp:LinkButton>
                                           
                                                <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                     SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="LinkButtonInsert" runat="server" Enabled="<%#CommandField()%>"
                                            CommandName="Insert" Text="<%$ Resources:DeffinityRes,Insert%>" ValidationGroup="Group1"
                                            SkinID="BtnLinkUpload" ToolTip="<%$ Resources:DeffinityRes,Insert%>"></asp:LinkButton>
                                      
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Receipt<br/>Tracker"><%--Receipt<br/>Tracker--%>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyplink1" runat="server" Text="&lt;img src='media/ico_thumb_grey.png' alt='Gray' border='0'/&gt;"
                                            Target="_self" ToolTip="Items have not been received" 
                                            Visible='<%#SetImageGray(DataBinder.Eval(Container.DataItem,"ID").ToString())%>'
                                            NavigateUrl='<%# GetUrl(DataBinder.Eval(Container.DataItem,"ID").ToString()) %>'></asp:HyperLink>
                                        <asp:HyperLink ID="hyplink2" runat="server" Text="&lt;img src='media/ico_thumb_yellow.png' alt='Yellow' border='0'/&gt;"
                                            Target="_self" ToolTip="Items has been part received" Visible='<%#SetImageAmber(DataBinder.Eval(Container.DataItem,"ID").ToString())%>'
                                            NavigateUrl='<%# GetUrl(DataBinder.Eval(Container.DataItem,"ID").ToString()) %>'></asp:HyperLink>
                                        <asp:HyperLink ID="hyplink3" runat="server" Text="&lt;img src='media/ico_thumb_green.png' alt='Green' border='0'/&gt;"
                                            Target="_self" ToolTip="Items has been received in full" Visible='<%#SetImageGreen(DataBinder.Eval(Container.DataItem,"ID").ToString())%>'
                                            NavigateUrl='<%# GetUrl(DataBinder.Eval(Container.DataItem,"ID").ToString()) %>'></asp:HyperLink>
                                        <%-- <asp:Image ID="Image1" runat="server" Visible='<%#SetImageGray(DataBinder.Eval(Container.DataItem,"ID").ToString())%>'
                                        ImageUrl="~/media/ico_thumb_grey.png" ToolTip="Items have not been received" />
                                    <asp:Image ID="Image2" runat="server" Visible='<%#SetImageAmber(DataBinder.Eval(Container.DataItem,"ID").ToString())%>'
                                        ImageUrl="~/media/ico_thumb_yellow.png" ToolTip="Items has been part received" />
                                    <asp:Image ID="Image3" runat="server" Visible='<%#SetImageGreen(DataBinder.Eval(Container.DataItem,"ID").ToString())%>'
                                        ImageUrl="~/media/ico_thumb_green.png" ToolTip="Items has been received in full"  />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgSave" runat="server" SkinID="BtnLinkSave" CommandName="Edit1"
                                             CommandArgument='<%# GetPurchasedQty(DataBinder.Eval(Container.DataItem,"ID").ToString()) + "/" + DataBinder.Eval(Container.DataItem,"Qty").ToString()+"/"+DataBinder.Eval(Container.DataItem,"ID").ToString()%>'
                                            ToolTip="Saving" Visible='<%#SavingImageVisibility(DataBinder.Eval(Container.DataItem,"ID").ToString())%>' CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,QtyOrdered%>" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurchased" runat="server"
                                             Text='<%# GetPurchasedQty(DataBinder.Eval(Container.DataItem,"ID").ToString()) + "/" + DataBinder.Eval(Container.DataItem,"Qty").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="<%$ Resources:DeffinityRes,Description%>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description")%>' MaxLength="500"></asp:TextBox>
                                        <%-- <asp:ImageButton ID="imgDescription" runat="server" SkinID="ImgSymAdd" ImageAlign="Right" />--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescription"
                                            ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdescription%>" Display="None"
                                            ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <div class="form-group">
                                                      <div class="col-md-12">
                                                           <div class="col-sm-12 form-inline">
                                                               <asp:TextBox ID="txtfoo_service" runat="server" ValidationGroup="grpser1" SkinID="txt_125px" MaxLength="500"></asp:TextBox>
                                                               <asp:LinkButton ID="btnPopUp" runat="server" SkinID="BtnLinkAdd" CommandName="Add"
                                                                              EnableViewState="false" AlternateText="Look up" CausesValidation="false" />
                                                               <asp:LinkButton ID="btnPopUpRate" runat="server" SkinID="BtnLinkBarcode"
                                                                                CausesValidation="false" CommandName="AddRate" ToolTip="Select labour from the rate card"
                                                                                                               EnableViewState="false" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDes" runat="server" ControlToValidate="txtfoo_service"
                                                                                  ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdescription%>" Display="None"
                                                                                                                ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                                           </div>
                                                      </div>
                                        </div>     
                                        <asp:Panel ID="pnlSimpleDetails" runat="server" ScrollBars="Both" BackColor="White"
                                            Style="display: none" Width="670px" Height="500px" BorderStyle="Double" BorderColor="LightSteelBlue">
                                             <div>
                                                 <asp:GridView ID="grd_services" SkinID="gv_nested" runat="server" DataKeyNames="ID" EmptyDataText="No Services found"
                                                            DataSourceID="DS_Services">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkitem" runat="server" Enabled="<%#CommandField()%>" Checked="false"
                                                                            onclick="CheckSelection();" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblID" runat='server' Text='<%# Eval("ID") %>'></asp:Label></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="ID" DataField="ID" Visible="false" />
                                                                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Description%>" DataField="Description" />
                                                                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Partnumber%>" DataField="PartNumber" />
                                                                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Supplier%>" DataField="Company" />
                                                                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Unit%>" DataField="Unit" />
                                                                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Material%>" DataField="Material"
                                                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
                                                                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Labour%>" DataField="Labour"
                                                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
                                                                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Misc%>" DataField="Mics" DataFormatString="{0:F2}"
                                                                    ItemStyle-HorizontalAlign="Right" />
                                                                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Total%>" DataField="Total"
                                                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
                                                            </Columns>
                                                        </asp:GridView>
                                             </div>
                                              <div class="form-group">
                                                      <div class="col-md-12">
                                                            <asp:Button ID="lnkOk" runat="server" Enabled="<%#CommandField()%>" OnClick="lnkOk_Click"
                                                            Text="OK" SkinID="btnSubmit" CausesValidation="false" />
                                                           <asp:Button ID="lnkCancel" runat="server" Text="Close" SkinID="btnCancel" />
                                                      </div>
                                              </div>
                                        </asp:Panel>
                                        <ajaxtoolkit:modalpopupextender id="mdlSimpleDetails" runat="server" cancelcontrolid="lnkCancel"
                                            backgroundcssclass="modalBackground" targetcontrolid="btnPopUp" popupcontrolid="pnlSimpleDetails">
                                        </ajaxtoolkit:modalpopupextender>
                                        <asp:RequiredFieldValidator ID="reqser1" runat="server" Display="None" ErrorMessage="Please enter Service Description"
                                            ForeColor="Red" ValidationGroup="grpser1" ControlToValidate="txtfoo_service"></asp:RequiredFieldValidator></td>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Partnumber%>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartNumber" runat="server" Text='<%#Bind("PartNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPartNumber" runat="server" Text='<%#Bind("PartNumber")%>' MaxLength="200"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtPartNumberf" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Supplier%>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToDate" runat="server" Text='<%#Bind("Company")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlSupplier" runat="server" SkinID="ddl_100px"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequSupplier" runat="server" ErrorMessage="Please select a supplier"
                                            ValidationGroup="Group2" Display="None" InitialValue="0" ControlToValidate="ddlSupplier"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlSupplierf" runat="server"  SkinID="ddl_100px"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="ReqSupplierf" runat="server" ErrorMessage="Please select a supplier"
                                            ValidationGroup="Group1" Display="None" InitialValue="0" ControlToValidate="ddlSupplierf"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Manufacturer%>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManufacturer" runat="server" Text='<%#Bind("Manufacturer")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlManufacturer" runat="server" SkinID="ddl_100px"></asp:DropDownList>
                                      <%--  <asp:RequiredFieldValidator ID="ReqManufacturer" runat="server" ErrorMessage="Please select a manufacturer"
                                            ValidationGroup="Group2" Display="None" InitialValue="0" ControlToValidate="ddlManufacturer"></asp:RequiredFieldValidator>--%>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlManufacturerf" runat="server" SkinID="ddl_100px"></asp:DropDownList>
                                      <%--  <asp:RequiredFieldValidator ID="ReqddlManufacturerf" runat="server" ErrorMessage="Please select a manufacturer"
                                            ValidationGroup="Group1" Display="None" InitialValue="0" ControlToValidate="ddlManufacturerf"></asp:RequiredFieldValidator>--%>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Unit%>" Visible="true">
                                    <ItemStyle  HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%#Bind("Unit")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUnit" runat="server" Text='<%#Bind("Unit")%>' SkinID="txt_75px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtUnitf" runat="server" SkinID="txt_75px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,QTY%>">
                                    <ItemStyle  HorizontalAlign="Right" />
                                    <FooterStyle  HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty" runat="server" Text='<%#Bind("Qty","{0:F2}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtQty" runat="server" Text='<%#Bind("Qty","{0:F2}")%>' SkinID="txt_75px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regex12Qty" runat="server" ControlToValidate="txtQty"
                                            ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ValidationGroup="Group2" Display="None"
                                            ErrorMessage="You have entered incorrect data into fields which only accept numbers"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtQtyf" runat="server" SkinID="txt_75px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regex12Qtyf" runat="server" ControlToValidate="txtQtyf"
                                            ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ValidationGroup="Group1" Display="None"
                                            ErrorMessage="You have entered incorrect data into fields which only accept numbers"></asp:RegularExpressionValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Currency%>">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrency" runat="server" Text='<%#Bind("CurrencyName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlCurrency" runat="server" DataSourceID="sqlds_currency"
                                            DataTextField="CurrencyName" DataValueField="ID" SelectedValue='<%#Bind("CurrencyID")%>' SkinID="ddl_100px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlCurrency_f" runat="server" DataTextField="CurrencyName" SkinID="ddl_100px"
                                            DataValueField="ID">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Material%>">
                                    <ItemStyle  HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterial" runat="server" Text='<%#Bind("Material","{0:F2}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtMaterial" runat="server" Text='<%#Bind("Material","{0:F2}")%>' SkinID="Price_75px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regex12Material" runat="server" ControlToValidate="txtMaterial"
                                            ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ValidationGroup="Group2" Display="None"
                                            ErrorMessage="You have entered incorrect data into fields which only accept numbers"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtMaterialf" runat="server" SkinID="Price_75px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regex12Materialf" runat="server" ControlToValidate="txtMaterialf"
                                            ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ValidationGroup="Group1" Display="None"
                                            ErrorMessage="You have entered incorrect data into fields which only accept numbers"></asp:RegularExpressionValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Labour%>">
                                    <ItemStyle  HorizontalAlign="Right" />
                                    <FooterStyle  HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLabour" runat="server" Text='<%#Bind("Labour","{0:F2}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtLabour" runat="server" Text='<%#Bind("Labour","{0:F2}")%>' SkinID="Price_75px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regex12Labour" runat="server" ControlToValidate="txtLabour"
                                            ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ValidationGroup="Group2" Display="None"
                                            ErrorMessage="You have entered incorrect data into fields which only accept numbers"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtLabourf" runat="server" SkinID="Price_75px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regex12Labourf" runat="server" ControlToValidate="txtLabourf"
                                            ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ValidationGroup="Group1" Display="None"
                                            ErrorMessage="You have entered incorrect data into fields which only accept numbers"></asp:RegularExpressionValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Misc%>">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMisc" runat="server" Text='<%#Bind("Mics","{0:F2}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblTotaldd" runat="server" Text='<%#Bind("Total","{0:F2}")%>' Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtMisc" runat="server" Text='<%#Bind("Mics","{0:F2}")%>' SkinID="Price_75px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regex12Misc" runat="server" ControlToValidate="txtMisc"
                                            ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ValidationGroup="Group2" Display="None"
                                            ErrorMessage="You have entered incorrect data into fields which only accept numbers "></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtMiscf" runat="server" SkinID="Price_75px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regex12Miscf" runat="server" ControlToValidate="txtMiscf"
                                            ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ValidationGroup="Group1" Display="None"
                                            ErrorMessage="You have entered incorrect data into fields which only accept numbers"></asp:RegularExpressionValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Total%>" ItemStyle-HorizontalAlign="Right" DataField="Total" DataFormatString="{0:F2}" />
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,GP%>" Visible="false">
                                    <ItemTemplate>
                                        <div class="form-group">
                                                <div class="col-md-12 form-inline">
                                                       <asp:Label ID="lblTotald" runat="server" Text='<%#Bind("Total","{0:F2}")%>' Visible="false"></asp:Label>
                                                      <asp:TextBox ID="txtGP" runat="server" SkinID="Price_75px"
                                                         Text='<%#CalculateGP_sum(DataBinder.Eval(Container.DataItem,"Total").ToString(),DataBinder.Eval(Container.DataItem,"SellingTotal").ToString())%>'></asp:TextBox>
                                                       <asp:LinkButton ID="imgGP" runat="server" SkinID="BtnLinkNext" OnClick="btn_IndetDecrease_OnClick" CausesValidation="false" />
                                                       <asp:RegularExpressionValidator ID="regex12Labourf111f" runat="server" ControlToValidate="txtGP"
                                                                          ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ValidationGroup="Group4" Display="None"
                                                                          ErrorMessage="You have entered incorrect data into fields which only accept numbers"></asp:RegularExpressionValidator></td>
                                               </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SellingPrice%>" Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSelling" runat="server" Text='<%#Bind("SellingTotal","{0:F2}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSellingPrice" runat="server" Text='<%#Bind("SellingTotal","{0:F2}")%>' SkinID="Price_75px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="txtSellingPrice12" runat="server" ControlToValidate="txtSellingPrice"
                                            ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ValidationGroup="Group2" Display="None"
                                            ErrorMessage="You have entered incorrect data into fields which only accept numbers"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtSelling" runat="server" SkinID="Price_75px" Text="0.00"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regex12Miscf123" runat="server" ControlToValidate="txtSelling"
                                            ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ValidationGroup="Group1" Display="None"
                                            ErrorMessage="You have entered incorrect data into fields which only accept numbers"></asp:RegularExpressionValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImageButton1" runat="server" Enabled="<%#CommandField()%>" CausesValidation="false"
                                            Visible='<%#SetImageGray(DataBinder.Eval(Container.DataItem,"ID").ToString())%>'
                                            CommandName="Delete" SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>'
                                             OnClientClick="return confirm('Do you want to delete the record?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
      
               </div>
               <div class="form-group">
                      <div class="col-md-6">
                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.SaveWorksheetAs%></label>
                        <div class="col-sm-6">
                             <asp:TextBox ID="txtSaveWorksheetName" runat="server" SkinID="txt_90"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter worksheet name to save BOM"
                                    Display="None" ControlToValidate="txtSaveWorksheetName" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-2">
                             <asp:Button ID="imgSaveWorksheet" runat="server" Text="Save" SkinID="btnDefault" OnClick="imgSaveWorksheet_Click" ValidationGroup="Group3" />
                        </div>
                      </div>
               </div>
            <div>
                




                <asp:Panel ID="PnlSnapShot" runat="server">
                    <div class="form-group">
        <div class="col-md-12">
           <strong> Snapshots</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
               
                <div id="DivSnapShot">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdateSnap">
                    <ProgressTemplate>
                        <asp:Label ID="imgLoading_1" SkinID="Loading" runat="server" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdateSnap" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-5 control-label">Snapshot Name</label>
           <div class="col-sm-7">
               <asp:DropDownList ID="ddlSnapshot" SkinID="ddl_90" AutoPostBack="true" ClientIDMode="Static" 
                                      runat="server" OnSelectedIndexChanged="ddlSnapshot_SelectedIndexChanged"></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-5 control-label"> Worksheet Name</label>
           <div class="col-sm-7">
               <asp:DropDownList ID="ddlWorksheetName" SkinID="ddl_90" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWorksheetName_SelectedIndexChanged"></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
</div>
                  
                   <div style="width: 100%; overflow:auto;height:250px;">
                        <asp:GridView ID="GridSnapShots" runat="server" Width="100%" AutoGenerateColumns="false" EmptyDataText="No records found."
                            DataKeyNames="ID" OnRowDataBound="GridSnapShots_RowDataBound">
                            <Columns>
                                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header_bg_l">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCountId" runat="server" Text='<%#Bind("RowDataChanged") %>' Visible="false"></asp:Label>
                                        <asp:LinkButton ID="ImgSave" runat="server" SkinID="LinkRed_circle" CommandName="Edit1" Enabled="false"
                                            ToolTip="Row data modified" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="30px" HeaderText=" <%$ Resources:DeffinityRes,QtyOrdered%>" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurchased" runat="server"
                                             Text='<%# GetPurchasedQtyInSnapGrid(DataBinder.Eval(Container.DataItem,"ID").ToString(),DataBinder.Eval(Container.DataItem,"SnapId").ToString()) + "/" + DataBinder.Eval(Container.DataItem,"Qty").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="<%$ Resources:DeffinityRes,Description%>">
                                    <HeaderStyle Width="180px" />
                                    <ItemStyle Width="180px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Partnumber%>">
                                    <ItemStyle Width="55px" />
                                    <FooterStyle Width="55px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartNumber" runat="server" Text='<%#Bind("PartNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Supplier%>">
                                    <ItemStyle Width="80px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblToDate" runat="server" Text='<%#Bind("Company")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Manufacturer%>">
                                    <ItemStyle Width="80px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblManufacturer" runat="server" Text='<%#Bind("Manufacturer")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Unit%>" Visible="true">
                                    <ItemStyle Width="55px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%#Bind("Unit")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,QTY%>">
                                    <ItemStyle Width="75px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty" runat="server" Text='<%#Bind("Qty","{0:F2}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Currency%>">
                                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrency" runat="server" Text='<%#Bind("CurrencyName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Material%>">
                                    <ItemStyle Width="75px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterial" runat="server" Text='<%#Bind("Material","{0:F2}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Labour%>">
                                    <ItemStyle Width="75px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLabour" runat="server" Text='<%#Bind("Labour","{0:F2}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Misc%>">
                                    <ItemStyle Width="75px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMisc" runat="server" Text='<%#Bind("Mics","{0:F2}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Total%>" DataField="Total"
                                    DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,GP%>" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotald" runat="server" Text='<%#Bind("Total","{0:F2}")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="txtGP" runat="server"
                                             Text='<%#CalculateGP_sum(DataBinder.Eval(Container.DataItem,"Total").ToString(),DataBinder.Eval(Container.DataItem,"SellingTotal").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SellingPrice%>" HeaderStyle-CssClass="header_bg_r">
                                    <ItemStyle Width="75px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSelling" runat="server" Text='<%#Bind("SellingTotal","{0:F2}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                 </ContentTemplate>
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="ddlSnapshot" EventName="" />
                        <asp:AsyncPostBackTrigger ControlID="ddlWorksheetName" EventName="" />
                 </Triggers>
               </asp:UpdatePanel>
                </div>
                </asp:Panel>
                
                <div class="form-group well">
                     <div class="col-md-6">
                                           <strong><%= Resources.DeffinityRes.SummaryofCurrentWorksheet%></strong> 
                                            <hr class="no-top-margin" />
                            </div>
                            <div class="col-md-6">
                                           <strong><%= Resources.DeffinityRes.SummaryofallWorksheets%></strong> 
                                            <hr class="no-top-margin" />
                            </div>
                              <div class="col-md-6">
                                    <div class="form-group">
                                             <label class="col-sm-12 control-label" style="font-weight:bold;text-align:right;"><%= Resources.DeffinityRes.Buying%></label>
                                  </div>
                                    <div class="form-group">
                                             <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.MaterialsProducts%>:</label>
                                             <div class="col-sm-6 control-label" style="text-align:right;">
                                                   <asp:Label ID="lblMaterial_bp_ws" runat="server"></asp:Label>
                                             </div>
                                   </div>
                                    <div class="form-group">
                                             <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.Labour%></label>
                                             <div class="col-sm-6 control-label" style="text-align:right;">
                                                  <asp:Label ID="lblLabour_bp_ws" runat="server"></asp:Label>
                                             </div>
                                   </div>
                                    <div class="form-group">
                                             <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.Miscellaneous%> :</label>
                                              <div class="col-sm-6 control-label" style="text-align:right;">
                                                   <asp:Label ID="lblMisc_bp_ws" runat="server"></asp:Label>
                                             </div>
                                   </div>
                                    <div class="form-group">
                                             <label class="col-sm-6 control-label"><b><%= Resources.DeffinityRes.Total%> :</b></label>
                                              <div class="col-sm-6 control-label" style="text-align:right;">
                                                  <b><asp:Label ID="lblTotal_bp_ws" runat="server"></asp:Label></b>
                                             </div>
                                   </div>
                                    <div class="form-group">
                                             <label class="col-sm-6 control-label"><b><%= Resources.DeffinityRes.SellingTotal%> :</b></label>
                                             <div class="col-sm-6 control-label" style="text-align:right;">
                                                <b>  <asp:Label ID="lblSellingPrice" runat="server"></asp:Label></b>
                                             </div>
                                   </div>
                              </div>
                              <div class="col-md-6">
                                    <div class="form-group">
                                             <label class="col-sm-12 control-label" style="font-weight:bold;text-align:right;"><%= Resources.DeffinityRes.Buying%></label>
                                  </div>
                                    <div class="form-group">
                                             <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.MaterialsProducts%>:</label>
                                             <div class="col-sm-6 control-label" style="text-align:right;">
                                                       <asp:Label ID="lblMaterial_bp" runat="server"></asp:Label>
                                             </div>
                                   </div>
                                    <div class="form-group">
                                             <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.Labour%></label>
                                            <div class="col-sm-6 control-label" style="text-align:right;">
                                                  <asp:Label ID="lblLabour_bp" runat="server"></asp:Label>
                                             </div>
                                   </div>
                                    <div class="form-group">
                                             <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.Miscellaneous%> :</label>
                                            <div class="col-sm-6 control-label" style="text-align:right;">
                                                   <asp:Label ID="lblMisc_bp" runat="server"></asp:Label>
                                             </div>
                                   </div>
                                    <div class="form-group">
                                            <label class="col-sm-6 control-label"><b><%= Resources.DeffinityRes.Total%> :</b></label>
                                             <div class="col-sm-6 control-label" style="text-align:right;">
                                               <b>   <asp:Label ID="lblTotal_bp" runat="server"></asp:Label></b>
                                             </div>
                                   </div>
                                    <div class="form-group">
                                             <label class="col-sm-6 control-label"><b> <%= Resources.DeffinityRes.SellingTotal%> :</b></label>
                                             <div class="col-sm-6 control-label" style="text-align:right;">
                                               <b>  <asp:Label ID="lblWrkSellingPrice" runat="server"></asp:Label></b>
                                             </div>
                                   </div>
                              </div>
                </div>
            </div>
       <%-- </div>--%>





        <div>
              <div style="width: 800px; float: left" id="pnl_rbuttons" runat="server" visible="false">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="<%$ Resources:DeffinityRes,ProjectBudget %>"></asp:ListItem>
                        <asp:ListItem Value="5" Text="<%$ Resources:DeffinityRes,BudgetbyTask %>"></asp:ListItem>
                        <asp:ListItem Value="2" Selected="True" Text="<%$ Resources:DeffinityRes,BillofMaterials %>"></asp:ListItem>
                        <%--  <asp:ListItem Value="4"  >Goods&nbsp;Receipt</asp:ListItem>--%>
                        <asp:ListItem Value="3" Text="<%$ Resources:DeffinityRes,ProjectBenefitBudget %>"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
              <div>
                    <ajaxtoolkit:modalpopupextender id="mdlWorksheet" cancelcontrolid="imgBtnWorksheetCancel"
                        runat="server" backgroundcssclass="modalBackground" targetcontrolid="imgaddcourse"
                        popupcontrolid="pnlWorksheet">
                    </ajaxtoolkit:modalpopupextender>
                    <ajaxtoolkit:modalpopupextender cancelcontrolid="ImgCancel" id="mpopBOM" runat="server"
                        popupcontrolid="pnlBOM" targetcontrolid="imgItemEdit" backgroundcssclass="modalBackground">
                    </ajaxtoolkit:modalpopupextender>
                    <ajaxtoolkit:modalpopupextender id="mdlPopRateCard" runat="server" cancelcontrolid="btnCancel"
                        popupcontrolid="pnlSelectRate" targetcontrolid="btnRTCard">
                    </ajaxtoolkit:modalpopupextender>
                    <ajaxtoolkit:modalpopupextender id="mdlSavedWorksheet" cancelcontrolid="imgsavedworksheetcancel"
                        runat="server" backgroundcssclass="modalBackground" targetcontrolid="imageadd1"
                        popupcontrolid="panel_SavedWorkSheet">
                    </ajaxtoolkit:modalpopupextender>
                    <asp:Button ID="imageadd1" runat="server" Text="txt" Style="display: none" />
                    <%-- <label id="lblworksheetname" runat="server" visible="false" />--%>
                    <asp:Panel ID="pnlWorksheet" runat="server" BackColor="White" Style="display: none" Width="230px" ScrollBars="None" BorderStyle="Double" BorderColor="LightSteelBlue">

                        <div class="form-group">
                                    <div class="col-md-12">
                                             <strong> <%= Resources.DeffinityRes.Worksheet%> </strong>
                                             <hr class="no-top-margin" />
                                    </div>
                        </div>

                        <div class="form-group">
                                      <div class="col-md-10">
                                          <asp:TextBox ID="txtModelWorksheet" runat="server"></asp:TextBox>
                                          <asp:HiddenField ID="H_Worksheet" runat="server" Value="0" />
                                          <asp:Label ID="lblWorksheet" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtModelWorksheet"
                                                                                      ErrorMessage="<%$ Resources:DeffinityRes,PlsEnterworksheet%>" ForeColor="Red"
                                                                                      ValidationGroup="Group_Worksheet"></asp:RequiredFieldValidator>
                                      </div>
                         </div>
                         <div class="form-group">
                                      <div class="col-md-10">
                                          <asp:Button runat="server" ID="imgaddcourse" Style="display: none" />
                                          <asp:Button ID="imgBtnWorksheet" runat="server" Text="OK" SkinID="btnSubmit"
                                                                                      OnClick="imgBtnWorksheet_Click" ValidationGroup="Group_Worksheet" />
                                          <asp:Button ID="imgBtnWorksheetCancel" runat="server" Text="Close" SkinID="btnCancel" />
                                      </div>
                         </div>
                    </asp:Panel>
                    <asp:Panel ID="panel_SavedWorkSheet" runat="server" BackColor="White" Style="display: none"
                        Width="250px" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="None">
                        <div class="form-group">
        <div class="col-md-12">
           <strong><%=Resources.DeffinityRes.SavedWorksheet %></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                         <div class="form-group">
                                      <div class="col-md-10">
                                            <asp:TextBox ID="txtsavedworksheet" runat="server"></asp:TextBox>
                                            <asp:HiddenField ID="hfsavedworksheet" runat="server" Value="0" />
                                            <asp:Label ID="lblsavedworksheet" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvsw" runat="server" ControlToValidate="txtsavedworksheet"
                                                                   ErrorMessage="Please enter saved worksheet name" ForeColor="Red"></asp:RequiredFieldValidator>
                                      </div>
                         </div>
                         <div class="form-group">
                                      <div class="col-md-10">
                                           <asp:Button runat="server" ID="imageadd" Style="display: none" />
                                           <asp:Button ID="imgBtnsavedWorksheet" runat="server" Text="OK" SkinID="btnSubmit"
                                                                                       OnClick="imgBtnsavedWorksheet_Click" />
                                           <asp:Button ID="imgsavedworksheetcancel" runat="server" Text="Close" SkinID="btnCancel" />
                                      </div>
                         </div>
                    </asp:Panel>
                </div>
              <asp:Panel ID="pnlBOM" runat="server" BackColor="White" Style="display: none" Width="850px"
                        Height="555px" BorderStyle="Double" ScrollBars="None" BorderColor="LightSteelBlue">
                        <div style="float: right">
                            <asp:LinkButton ID="ImgCancel" runat="server" SkinID="BtnLinkCancel" Style="display: none" /></div>
                        <div>
                            <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label></div>
                    
                                 <div class="col-md-12">
                                                <strong>  <asp:Label ID="lblTaskName" runat="server" Text="<%$ Resources:DeffinityRes,ServiceCatalogueItems%>"></asp:Label> </strong> 
                                                 <hr class="no-top-margin" />
                                 </div>
                 
                        <iframe id="ifrmCatelog" runat="server" marginheight="0" marginwidth="0" width="840px"
                            height="460px" frameborder="0" scrolling="no"></iframe>
                 
                        <div style="text-align: left">
                            <asp:Button ID="imgUpdate" runat="server" SkinID="btnClose" OnClick="imgUpdate_Click" CausesValidation="false" />
                            <%-- <asp:ImageButton ID="ImgCancel" runat="server" SkinID="ImgCancel" />--%>
                            <asp:Button SkinID="btnEdit" runat="server" ID="imgItemEdit" Style="display: none" />
                        </div>
                    </asp:Panel>
              <div class="ScrollStyle" style="height:2px">
                                             <ajaxtoolkit:modalpopupextender id="DeletedPopUp" CancelControlID="imgCancelBtn"
                                               runat="server" backgroundcssclass="modalBackground" targetcontrolid="lblpopup"
                                                                     popupcontrolid="PnldeletedList"></ajaxtoolkit:modalpopupextender>
                                              <asp:Label ID="lblpopup" runat="server"></asp:Label>
                                       <asp:Panel ID="PnldeletedList" runat="server" BackColor="White" Style="display: none;overflow-y:scroll;"
                                            Height="40%" Width="60%" BorderStyle="Double" BorderColor="LightSteelBlue">
                                                 <div class="form-group">
                                                     <div class="col-md-10">
                                                         <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 100%;">
                                                               <b>  <%= Resources.DeffinityRes.RecoverBoMWorksheets%></b>
                                                             </div>
                                                     </div>
                                                     <div class="col-md-2" style="float:right;text-align:right;">
                                                          <asp:LinkButton ID="imgCancelBtn" runat="server" SkinID="BtnLinkClose"
                                                                                                ToolTip='<%$ Resources:DeffinityRes,cancel%>' Text="Close" />
                                                      </div>
                                                  </div>
                                                 <div class="form-group">
                                                     <div class="col-md-12">
                                                             <asp:GridView ID="griddeletedlist" AutoGenerateColumns="False" GridLines="None" runat="server"
                                                   ShowHeaderWhenEmpty="true" HorizontalAlign="Left" BorderStyle="None" CellPadding="2"
                                                          CellSpacing="2" EmptyDataText="No worksheets found!" Width="100%" OnRowCommand="griddeletedlist_RowCommand">
                                                           <Columns>
                                                     <asp:BoundField DataField="WorksheetName" HeaderText="<%$Resources:DeffinityRes,WorksheetName%>" />
                                                     <asp:BoundField DataField="CreatedBy" HeaderText="<%$Resources:DeffinityRes,CreatedBy%>" />
                                                     <asp:BoundField DataField="DeleteDate" HeaderText="<%$Resources:DeffinityRes,DeletedDate%>" />
                                                     <asp:BoundField DataField="deletedBy" HeaderText="<%$Resources:DeffinityRes,DeletedBy%>" />
                                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Recover%>" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Button ID="Btnrecover" runat="server" Text="<%$Resources:DeffinityRes,Recover%>"
                                                                 CommandArgument='<%# Bind("Wsid") %>' CausesValidation="false" CommandName="Recover"
                                                                 ToolTip='<%$ Resources:DeffinityRes,Bompanlebtn%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                           </asp:GridView>
                                                     </div>
                                                 </div>
                                           </asp:Panel>
                             </div>

              <asp:Button ID="btnaddtoquote" runat="server" SkinID="ImgAddtoQuote" OnClick="btnaddtoquote_Click"
                                        ToolTip="<%$ Resources:DeffinityRes,Addselecteditemstothequote%>" Visible="False"
                                        CausesValidation="false" />   
              <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"
                                         Visible="False"> <%= Resources.DeffinityRes.SupplierRequisition%>
              </asp:LinkButton>
              <asp:Panel ID="PnlPopUpInGrid" runat="server" BackColor="White" Style="display:none" Width="400px" Height="300px"
                  BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="None">
           
                  <div class="form-group">
        <div class="col-md-10">
           <strong><asp:Label ID="Label6" runat="server" Text="Saving"></asp:Label> </strong> 
            <hr class="no-top-margin" />
            </div>
                      <div class="col-md-2 pull-right">
                          <asp:LinkButton ID="ImageButton5" runat="server" SkinID="BtnLinkClose" ToolTip="Close" OnClick="ImageButton5_Click" />
                          </div>
    </div>
               
            
            <asp:UpdatePanel ID="UpdatePnlPopUpInGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <div class="form-group">
                                  <div class="col-md-12">
                                        <asp:Label ID="lblProjectBOMId" runat="server"></asp:Label>
                                        <asp:ValidationSummary ID="ValidationSummaryinPopUP" runat="server" ValidationGroup="Goods" ForeColor="Red" />
                                  </div>
                     </div>
                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.QtyReceivedtoDate%></label>
                                       <div class="col-sm-7">
                                           <asp:Label ID="lblQtyReceivedtoDate" Font-Bold="true" runat="server"></asp:Label>
                                       </div>
                                  </div>
                    </div>
                     <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.BudgetedQty%></label>
                                       <div class="col-sm-7">
                                             <asp:TextBox ID="txtbudgetQty" runat="server" Enabled="false" SkinID="txt_75px"></asp:TextBox>
                                       </div>
                                  </div>
                    </div>
                     <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.ActualRequired%></label>
                                       <div class="col-sm-7">
                                            <asp:TextBox ID="txtActualReq" runat="server" SkinID="txt_75px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                       TargetControlID="txtActualReq"></ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:RangeValidator runat="server" id="RangeValidator2" controltovalidate="txtActualReq" type="Integer" ForeColor="Red" ValidationGroup="Goods"
                                                                Display="None" minimumvalue="1" MaximumValue="100" errormessage="Please enter valid qty" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter required qty"
                                                          ControlToValidate="txtActualReq" Display="None" ValidationGroup="Goods" Width="225px">*</asp:RequiredFieldValidator>
                                       </div>
                                  </div>
                    </div>
                     <div class="form-group">
                                  <div class="col-md-12">
                                       <asp:Button ID="BtnSave" runat="server" Text="Commit Saving to Project" ValidationGroup="Goods" OnClick="BtnSave_Click" />
                                  </div>
                     </div>
                </ContentTemplate>
                </asp:UpdatePanel>
                   <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePnlPopUpInGrid">
                <ProgressTemplate>
                    <asp:Label ID="imgloading_5" runat="server" SkinID="Loading" />
                </ProgressTemplate>
            </asp:UpdateProgress>
                  </asp:Panel>
             <asp:Label ID="l11" runat="server"></asp:Label>
             <ajaxToolkit:ModalPopupExtender ID="mdlpopupinGridToSave" runat="server" BackgroundCssClass="modalBackground"
                                        TargetControlID="l11" PopupControlID="PnlPopUpInGrid" CancelControlID="ImageButton5"></ajaxToolkit:ModalPopupExtender>
              <asp:Panel ID="pnlSelectRate" runat="server" ScrollBars="Both" BackColor="White"
                        Style="display: none" Width="670px" Height="500px" BorderStyle="Double" BorderColor="LightSteelBlue">
                   <div class="form-group">
                              <div class="col-md-12">
                                             <strong>  <%= Resources.DeffinityRes.ResourceSelector%> </strong> 
                                              <hr class="no-top-margin" />
                              </div>
                  </div>
                   <div class="form-group">
                             <div class="col-md-6">
                                            <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Classifications%></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="true"
                                                     OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                             </div>
	                         <div class="col-md-6">
                                            <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Resource%></label>
                                            <div class="col-sm-8">
                                                 <asp:DropDownList ID="ddlResource" runat="server" AutoPostBack="true"
                                                      OnSelectedIndexChanged="ddlResource_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                             </div>
	               </div>
                   <div>
                       <asp:GridView ID="grdRatecard" runat="server" AutoGenerateColumns="False" EmptyDataText="<%$ Resources:DeffinityRes,NoRatescardsavailable%>">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkitem" runat="server" Checked="false" onclick="CheckSelection();" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                        <div style="float: left">
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible="false"> </asp:Label>
                                                        </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Classifications%>">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClassification" runat="server" Text='<%# Bind("ExpClassification") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,RateType%>">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRateType" runat="server" Text='<%# Bind("EntryType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,DailyRate%>">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDailyRate" runat="server" Text='<%# Eval("DailyRate","{0:N2}") %>'
                                                        Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,HourlyRate%>" HeaderStyle-CssClass="header_bg_r">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblECHRate" runat="server" Style="text-align: right" Text='<%# Eval("HourlyRate","{0:N2}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                   </div>
                   <div>
                      <asp:GridView ID="grdRateRes" runat="server" AutoGenerateColumns="False" EmptyDataText="<%$ Resources:DeffinityRes,NoRatescardsavailable%>">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkitem" runat="server" Checked="false" onclick="CheckSelection();" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<% # Bind("ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EntryType%>">
                                              
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEntry" runat="server" Text='<%# Bind("EntryType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,BuyingDailyRate%>">
                                             
                                                <ItemStyle HorizontalAlign="Right"/>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHourlyBuying" runat="server" Text='<%# Bind("Hourlyrate_Buying") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,DailySellingRate%>">
                                             
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHourlyselling" runat="server" SkinID="Price" Text='<%# Bind("Hourlyrate_Selling") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Minimumdailyhours%>" HeaderStyle-CssClass="header_bg_r">
                                              
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMinimumdailyhours" runat="server" 
                                                        Text='<%# ChangeHoues(Eval("Minimumdailyhours").ToString())%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                  </div>
                  <div class="form-group">
                                <div class="col-md-12">
                                        <asp:Button ID="imgbtnClsSel" runat="server" OnClick="imgbtnClsSel_Click" Text="OK"
                                                                                                SkinID="btnSubmit" CausesValidation="false" />
                                                <asp:Button runat="server" ID="btnRTCard" Style="display: none" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Close" SkinID="btnCancel" OnClick="btnCancel_click" />
                                </div>
                   </div>
                    </asp:Panel>
              <asp:HiddenField ID="HD_ServiceType1" runat="server" />
               <asp:SqlDataSource ID="DS_Services" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="DN_GETProjectBOM" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="0" Name="ProjectReferene" QueryStringField="Project"
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
               <asp:SqlDataSource ID="sqlds_currency" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="select ID,CurrencyName from CurrencyList where Display ='Y' union select 0,' Please select..' order by CurrencyName"
                        SelectCommandType="Text"></asp:SqlDataSource>
        </div>

   </div>

    
   <%--   <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
   <script type="text/javascript">
         grid_responsive_parent_display();
         grid_responsive_nested_display();

         $(window).load(function () {
             $(".dropdown-menu li")
           .find("input[type='checkbox']")
           .prop('checked', 'checked').trigger('change');
             $(".btn-toolbar").hide();
         });
    </script>--%>
<script type="text/javascript">
    NestedGridResponsiveCss();
 </script>    
    </label>
  
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
    
    <script type="text/javascript">

    </script>
</asp:Content>

